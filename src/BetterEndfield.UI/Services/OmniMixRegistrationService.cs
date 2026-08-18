using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace BetterEndfield.UI.Services;

internal sealed record OmniMixRegistrationStatus(
    int SchemaVersion,
    bool Registered,
    string BackendExe,
    string ClientId,
    string BackendVersion,
    bool Valid,
    string Reason);

internal sealed class OmniMixRegistrationException : Exception
{
    public OmniMixRegistrationException(int exitCode, string reason, string message)
        : base(message)
    {
        ExitCode = exitCode;
        Reason = reason;
    }

    public int ExitCode { get; }

    public string Reason { get; }
}

internal static class OmniMixRegistrationService
{
    private const string Section = "betterendfield.music";
    private const string ExpectedFileName = "OmniMixPlayer.Backend.exe";
    private const string SdkFileName = "OmniPcmShared.dll";
    private const uint SupportedAbiMajor = 2;
    private const uint SupportedSharedProtocol = 2;
    private const uint Float32InterleavedFormat = 1;
    private static readonly SemaphoreSlim FileGate = new(1, 1);

    public static async Task<OmniMixRegistrationStatus> RegisterAsync(string backendPath)
    {
        string normalized = NormalizeBackendPath(backendPath);
        BackendValidation validation = ValidateBackend(normalized);
        if (!validation.Valid)
        {
            throw new OmniMixRegistrationException(
                validation.ExitCode,
                validation.Reason,
                validation.Message);
        }

        await FileGate.WaitAsync();
        try
        {
            Dictionary<string, string> values = await ReadSectionAsync();
            string clientId = values.TryGetValue("client_id", out string? existing) &&
                !string.IsNullOrWhiteSpace(existing)
                    ? existing.Trim()
                    : CreateClientId();
            await UpdateSectionAsync(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["backend_exe"] = normalized,
                ["client_id"] = clientId
            });
            return new OmniMixRegistrationStatus(
                1, true, normalized, clientId, validation.Version, true, "ok");
        }
        finally
        {
            FileGate.Release();
        }
    }

    public static async Task<OmniMixRegistrationStatus> QueryAsync()
    {
        await FileGate.WaitAsync();
        try
        {
            Dictionary<string, string> values = await ReadSectionAsync();
            string backend = values.TryGetValue("backend_exe", out string? path)
                ? path.Trim()
                : string.Empty;
            string clientId = values.TryGetValue("client_id", out string? id)
                ? id.Trim()
                : string.Empty;
            if (string.IsNullOrWhiteSpace(backend))
            {
                return new OmniMixRegistrationStatus(
                    1, false, string.Empty, string.Empty, string.Empty, false,
                    "not_registered");
            }

            BackendValidation validation = ValidateBackend(backend);
            return new OmniMixRegistrationStatus(
                1,
                true,
                backend,
                clientId,
                validation.Version,
                validation.Valid,
                validation.Reason);
        }
        finally
        {
            FileGate.Release();
        }
    }

    public static async Task<OmniMixRegistrationStatus> UnregisterAsync()
    {
        await FileGate.WaitAsync();
        try
        {
            await UpdateSectionAsync(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["backend_exe"] = string.Empty,
                ["client_id"] = string.Empty,
                ["enabled"] = "false",
                ["music_replacement_enabled"] = "false"
            });
            return new OmniMixRegistrationStatus(
                1, false, string.Empty, string.Empty, string.Empty, false,
                "not_registered");
        }
        finally
        {
            FileGate.Release();
        }
    }

    public static bool IsValidBackendPath(string path) =>
        ValidateBackend(path).Valid;

    private static string NormalizeBackendPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new OmniMixRegistrationException(
                2, "path_missing", "缺少 OmniMix 后端路径。");
        }

        try
        {
            string candidate = path.Trim().Trim('"');
            if (!Path.IsPathFullyQualified(candidate))
            {
                throw new ArgumentException("Path is not fully qualified.");
            }
            string normalized = Path.GetFullPath(candidate);
            return normalized;
        }
        catch (Exception exception) when (
            exception is ArgumentException or NotSupportedException or PathTooLongException)
        {
            throw new OmniMixRegistrationException(
                2, "path_invalid", "OmniMix 后端路径不是有效的绝对路径。");
        }
    }

    private static BackendValidation ValidateBackend(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return new(false, 3, "path_missing", "缺少 OmniMix 后端路径。", string.Empty);
        }
        if (!Path.GetFileName(path).Equals(ExpectedFileName, StringComparison.OrdinalIgnoreCase))
        {
            return new(
                false,
                3,
                "wrong_filename",
                $"后端文件名必须为 {ExpectedFileName}。",
                string.Empty);
        }
        if (!File.Exists(path))
        {
            return new(false, 3, "file_missing", "OmniMix 后端文件不存在。", string.Empty);
        }

        try
        {
            using var stream = new FileStream(
                path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true);
            if (stream.Length < 0x100 || reader.ReadUInt16() != 0x5A4D)
            {
                return new(false, 3, "not_pe", "目标不是有效的 Windows PE 文件。", string.Empty);
            }
            stream.Position = 0x3C;
            int peOffset = reader.ReadInt32();
            if (peOffset < 0x40 || peOffset > stream.Length - 26)
            {
                return new(false, 3, "not_pe", "目标的 PE 头无效。", string.Empty);
            }
            stream.Position = peOffset;
            if (reader.ReadUInt32() != 0x00004550)
            {
                return new(false, 3, "not_pe", "目标的 PE 签名无效。", string.Empty);
            }
            ushort machine = reader.ReadUInt16();
            stream.Position = peOffset + 24;
            ushort optionalMagic = reader.ReadUInt16();
            if (machine != 0x8664 || optionalMagic != 0x20B)
            {
                return new(false, 3, "not_x64_pe", "OmniMix 后端必须是 Windows x64 程序。", string.Empty);
            }

            FileVersionInfo version = FileVersionInfo.GetVersionInfo(path);
            string displayVersion = version.ProductVersion ?? version.FileVersion ?? string.Empty;
            BackendValidation sdkValidation = ValidateSdk(path, displayVersion);
            return sdkValidation;
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or InvalidDataException)
        {
            return new(false, 3, "file_unreadable", exception.Message, string.Empty);
        }
    }

    private static BackendValidation ValidateSdk(
        string backendPath,
        string backendVersion)
    {
        string backendDirectory = Path.GetDirectoryName(backendPath)!;
        string[] candidates =
        [
            Path.Combine(backendDirectory, "native", "x64", SdkFileName),
            Path.Combine(backendDirectory, SdkFileName)
        ];
        string? sdkPath = candidates.FirstOrDefault(File.Exists);
        if (sdkPath is null)
        {
            return new(
                false,
                5,
                "sdk_missing",
                $"后端目录中缺少 native\\x64\\{SdkFileName}。",
                backendVersion);
        }

        nint library = 0;
        try
        {
            library = NativeLibrary.Load(sdkPath);
            string[] requiredExports =
            [
                "OmniPcm_GetAbiVersion",
                "OmniPcm_GetAbiInfo",
                "OmniPcm_OpenInstanceUtf8",
                "OmniPcm_Close",
                "OmniPcm_IsOpen",
                "OmniPcm_GetVersion",
                "OmniPcm_GetLastError",
                "OmniPcm_GetSnapshotV2",
                "OmniPcm_BindCurrentStream",
                "OmniPcm_ReadFrames",
                "OmniPcm_SetAudibleCursor",
                "OmniPcmClient_Create",
                "OmniPcmClient_Destroy",
                "OmniPcmClient_GetLastError",
                "OmniPcmClient_ConnectInstance",
                "OmniPcmClient_Heartbeat",
                "OmniPcmClient_DisconnectInstance",
                "OmniPcmClient_SetTargetLatency"
            ];
            var exports = new Dictionary<string, nint>(StringComparer.Ordinal);
            foreach (string exportName in requiredExports)
            {
                if (!NativeLibrary.TryGetExport(
                        library, exportName, out nint exportAddress))
                {
                    return new(
                        false, 5, "sdk_contract_missing",
                        $"OmniPcmShared.dll 缺少接口 {exportName}。", backendVersion);
                }
                exports[exportName] = exportAddress;
            }

            GetAbiVersion getVersion = Marshal.GetDelegateForFunctionPointer<GetAbiVersion>(
                exports["OmniPcm_GetAbiVersion"]);
            GetAbiInfo getInfo = Marshal.GetDelegateForFunctionPointer<GetAbiInfo>(
                exports["OmniPcm_GetAbiInfo"]);
            uint encodedVersion = getVersion();
            uint encodedMajor = encodedVersion >> 16;
            uint encodedMinor = encodedVersion & 0xFFFF;
            if (encodedMajor != SupportedAbiMajor)
            {
                return new(
                    false,
                    5,
                    "sdk_abi_incompatible",
                    $"OmniPcmShared ABI {encodedMajor}.{encodedMinor} 与当前版本不兼容。",
                    backendVersion);
            }
            var info = new OmniPcmAbiInfo
            {
                Size = (uint)Marshal.SizeOf<OmniPcmAbiInfo>()
            };
            int result = getInfo(ref info);
            uint requiredFormat = 1u << (int)Float32InterleavedFormat;
            bool compatible = result == 0 &&
                info.AbiMajor == SupportedAbiMajor &&
                info.MinSharedProtocol <= SupportedSharedProtocol &&
                info.MaxSharedProtocol >= SupportedSharedProtocol &&
                (info.SampleFormatMask & requiredFormat) != 0;
            if (!compatible)
            {
                return new(
                    false,
                    5,
                    "sdk_abi_incompatible",
                    $"OmniPcmShared ABI {info.AbiMajor}.{info.AbiMinor} 与当前版本不兼容。",
                    backendVersion);
            }
            return new(true, 0, "ok", string.Empty, backendVersion);
        }
        catch (Exception exception) when (
            exception is BadImageFormatException or DllNotFoundException or
            EntryPointNotFoundException or SEHException)
        {
            return new(
                false, 5, "sdk_load_failed",
                $"无法验证 OmniPcmShared.dll：{exception.Message}", backendVersion);
        }
        finally
        {
            if (library != 0)
            {
                NativeLibrary.Free(library);
            }
        }
    }

    private static string CreateClientId()
    {
        Span<byte> random = stackalloc byte[8];
        RandomNumberGenerator.Fill(random);
        return "better-endfield-" + Convert.ToHexString(random).ToLowerInvariant();
    }

    private static async Task<Dictionary<string, string>> ReadSectionAsync()
    {
        string path = ConfigurationService.GetNativeConfigurationPath(string.Empty);
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (!File.Exists(path))
        {
            return result;
        }

        bool inSection = false;
        foreach (string source in await File.ReadAllLinesAsync(path))
        {
            string line = source.Trim();
            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                inSection = line[1..^1].Equals(Section, StringComparison.OrdinalIgnoreCase);
                continue;
            }
            if (!inSection || line.Length == 0 || line.StartsWith(';') || line.StartsWith('#'))
            {
                continue;
            }
            int separator = line.IndexOf('=');
            if (separator > 0)
            {
                result[line[..separator].Trim()] = line[(separator + 1)..].Trim();
            }
        }
        return result;
    }

    private static async Task UpdateSectionAsync(IReadOnlyDictionary<string, string> changes)
    {
        string path = ConfigurationService.GetNativeConfigurationPath(string.Empty);
        string[] existing = File.Exists(path)
            ? await File.ReadAllLinesAsync(path)
            : [];
        var output = new List<string>(existing.Length + changes.Count + 4);
        var pending = new Dictionary<string, string>(changes, StringComparer.OrdinalIgnoreCase);
        bool sectionFound = false;
        bool inSection = false;

        void FlushPending()
        {
            foreach ((string key, string value) in pending)
            {
                output.Add($"{key}={value}");
            }
            pending.Clear();
        }

        foreach (string source in existing)
        {
            string line = source.Trim();
            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                if (inSection)
                {
                    FlushPending();
                }
                inSection = line[1..^1].Equals(Section, StringComparison.OrdinalIgnoreCase);
                sectionFound |= inSection;
                output.Add(source);
                continue;
            }

            if (inSection)
            {
                int separator = line.IndexOf('=');
                if (separator > 0)
                {
                    string key = line[..separator].Trim();
                    if (pending.Remove(key, out string? replacement))
                    {
                        output.Add($"{key}={replacement}");
                        continue;
                    }
                }
            }
            output.Add(source);
        }

        if (inSection)
        {
            FlushPending();
        }
        if (!sectionFound)
        {
            if (output.Count > 0 && output[^1].Length != 0)
            {
                output.Add(string.Empty);
            }
            output.Add($"[{Section}]");
            FlushPending();
        }

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        string temporary = path + ".omnimix.tmp";
        await File.WriteAllLinesAsync(
            temporary, output, Encoding.Unicode);
        File.Move(temporary, path, overwrite: true);
    }

    private sealed record BackendValidation(
        bool Valid,
        int ExitCode,
        string Reason,
        string Message,
        string Version);

    [StructLayout(LayoutKind.Sequential)]
    private struct OmniPcmAbiInfo
    {
        public uint Size;
        public uint AbiVersion;
        public uint AbiMajor;
        public uint AbiMinor;
        public uint MinSharedProtocol;
        public uint MaxSharedProtocol;
        public uint SampleFormatMask;
        public uint Reserved;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate uint GetAbiVersion();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int GetAbiInfo(ref OmniPcmAbiInfo info);
}
