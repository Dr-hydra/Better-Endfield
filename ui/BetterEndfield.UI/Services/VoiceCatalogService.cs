using System.Buffers.Binary;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BetterEndfield.UI.Services;

internal sealed record VoiceCatalogRequest(
    string Speaker,
    string? CharacterId,
    string Language);

internal sealed record VoiceCatalogPreparation(IReadOnlyList<string> FileNames);

internal static class VoiceCatalogService
{
    private const string CatalogResourceName =
        "BetterEndfield.UI.Assets.voice.voice-catalog-index.json";
    private const string CatalogRegistryFileName = "generated-catalogs.json";
    private const uint XorConstant = 0x9C5A0B29;
    private const uint MultiplyConstant = 81861667;
    private const int CatalogHeaderSize = 36;
    private const int CatalogEntrySize = 24;

    private static readonly string[] LanguageNames =
        ["Chinese", "English", "Japanese", "Korean"];

    private static readonly Lazy<LoadedCatalogIndex> CatalogIndex = new(LoadIndex);

    public static Task<VoiceCatalogPreparation> PrepareAsync(
        string gameExecutablePath,
        IReadOnlyCollection<VoiceCatalogRequest> requests,
        CancellationToken cancellationToken = default) =>
        Task.Run(
            () => Prepare(gameExecutablePath, requests, cancellationToken),
            cancellationToken);

    public static async Task CommitAsync(
        VoiceCatalogPreparation preparation,
        CancellationToken cancellationToken = default)
    {
        string root = GetCatalogRoot();
        Directory.CreateDirectory(root);
        string registryPath = Path.Combine(root, CatalogRegistryFileName);
        CatalogRegistry previous = await ReadRegistryAsync(registryPath, cancellationToken);
        var desired = preparation.FileNames.ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (string fileName in previous.FileNames)
        {
            if (desired.Contains(fileName) || !IsManagedCatalogFileName(fileName))
            {
                continue;
            }

            DeleteIfPresent(Path.Combine(root, fileName));
            DeleteIfPresent(Path.Combine(root, fileName + ".json"));
        }

        var next = new CatalogRegistry
        {
            FileNames = desired.OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .ToList()
        };
        await WriteJsonAtomicallyAsync(registryPath, next, cancellationToken);
    }

    private static VoiceCatalogPreparation Prepare(
        string gameExecutablePath,
        IReadOnlyCollection<VoiceCatalogRequest> requests,
        CancellationToken cancellationToken)
    {
        var selected = requests
            .Where(request => !request.Language.Equals(
                "FollowGlobal",
                StringComparison.OrdinalIgnoreCase))
            .GroupBy(
                request => $"{request.CharacterId ?? "*"}\0{request.Language}",
                StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .ToArray();
        if (selected.Length == 0)
        {
            return new VoiceCatalogPreparation([]);
        }

        string executable = Path.GetFullPath(gameExecutablePath);
        if (!File.Exists(executable) ||
            !Path.GetFileName(executable).Equals(
                "Endfield.exe",
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "生成配音 catalog 前需要选择有效的 Endfield.exe。");
        }
        string gameRoot = Path.GetDirectoryName(executable)
            ?? throw new InvalidOperationException("游戏路径没有有效的父目录。");

        LoadedCatalogIndex index = CatalogIndex.Value;
        string catalogRoot = GetCatalogRoot();
        Directory.CreateDirectory(catalogRoot);
        var packages = new Dictionary<int, IReadOnlyList<PckIndex>>();
        var fileNames = new List<string>(selected.Length);

        foreach (VoiceCatalogRequest request in selected)
        {
            cancellationToken.ThrowIfCancellationRequested();
            int language = LanguageIndex(request.Language);
            string characterId = request.CharacterId ?? "*";
            CatalogRouteSet routeSet = index.Value.Catalogs.FirstOrDefault(catalog =>
                catalog.Language == language &&
                catalog.CharacterId.Equals(characterId, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException(
                    $"内置语音映射中没有 {characterId} / {request.Language}，请更新资源清单。");
            if (routeSet.Routes.Count == 0 || routeSet.Routes.Count % 3 != 0)
            {
                throw new InvalidDataException("内置语音映射表损坏。");
            }

            if (!packages.TryGetValue(language, out IReadOnlyList<PckIndex>? languagePackages))
            {
                CatalogPackage[] descriptors = index.Value.Packages
                    .Where(item => item.Language == language)
                    .ToArray();
                if (descriptors.Length == 0)
                {
                    throw new InvalidDataException("内置语音映射缺少语言包描述。");
                }
                languagePackages = descriptors
                    .Select(descriptor => LocateAndReadPackage(
                        gameRoot,
                        descriptor,
                        cancellationToken))
                    .ToArray();
                packages.Add(language, languagePackages);
            }

            string fileName = CatalogFileName(language, characterId);
            string outputPath = Path.Combine(catalogRoot, fileName);
            if (!IsCurrentCatalog(outputPath, index.Identity, languagePackages, routeSet))
            {
                BuildCatalog(outputPath, index.Identity, languagePackages, routeSet,
                    cancellationToken);
            }
            fileNames.Add(fileName);
        }

        return new VoiceCatalogPreparation(fileNames);
    }

    private static LoadedCatalogIndex LoadIndex()
    {
        using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
            CatalogResourceName) ?? throw new InvalidOperationException(
                $"Embedded voice catalog index was not found: {CatalogResourceName}");
        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        byte[] bytes = memory.ToArray();
        VoiceCatalogIndex value = JsonSerializer.Deserialize<VoiceCatalogIndex>(
            bytes,
            JsonOptions) ?? throw new InvalidDataException("Voice catalog index is invalid.");
        if (value.SchemaVersion != 2 ||
            value.Kind != "betterendfield-voice-catalog-index" ||
            Enumerable.Range(0, LanguageNames.Length).Any(language =>
                !value.Packages.Any(package => package.Language == language)) ||
            value.Catalogs.Count == 0)
        {
            throw new InvalidDataException("Voice catalog index schema is unsupported.");
        }
        return new LoadedCatalogIndex(
            value,
            Convert.ToHexString(SHA256.HashData(bytes)));
    }

    private static PckIndex LocateAndReadPackage(
        string gameRoot,
        CatalogPackage descriptor,
        CancellationToken cancellationToken)
    {
        string expectedPath = Path.GetFullPath(
            Path.Combine(gameRoot, descriptor.Source.Replace('/', Path.DirectorySeparatorChar)));
        PckIndex? package = TryReadMatchingPackage(expectedPath, descriptor);
        if (package is not null)
        {
            return package;
        }

        string[] roots =
        [
            Path.Combine(gameRoot, "Endfield_Data", "Persistent", "VFS"),
            Path.Combine(gameRoot, "Endfield_Data", "StreamingAssets", "VFS")
        ];
        foreach (string root in roots.Where(Directory.Exists))
        {
            foreach (string path in EnumerateFilesSafely(root, "*.chk"))
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (path.Equals(expectedPath, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                try
                {
                    if (new FileInfo(path).Length != descriptor.Size)
                    {
                        continue;
                    }
                }
                catch (IOException)
                {
                    continue;
                }

                package = TryReadMatchingPackage(path, descriptor);
                if (package is not null)
                {
                    return package;
                }
            }
        }

        throw new InvalidOperationException(
            $"未找到与当前清单匹配的 {LanguageNames[descriptor.Language]} 语言包。" +
            "请先在游戏中下载该语言包；若游戏已更新，请重建资源清单。");
    }

    private static PckIndex? TryReadMatchingPackage(
        string path,
        CatalogPackage descriptor)
    {
        try
        {
            if (!File.Exists(path) || new FileInfo(path).Length != descriptor.Size)
            {
                return null;
            }
            PckIndex package = ReadPck(path);
            return package.HeaderSize == descriptor.HeaderSize &&
                package.HeaderSha256.Equals(
                    descriptor.HeaderSha256,
                    StringComparison.OrdinalIgnoreCase)
                ? package
                : null;
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or InvalidDataException)
        {
            return null;
        }
    }

    private static PckIndex ReadPck(string path)
    {
        using var stream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite,
            1024 * 1024,
            FileOptions.RandomAccess);
        Span<byte> prefix = stackalloc byte[12];
        stream.ReadExactly(prefix);
        if (!HasMagic(prefix, "AKPK"u8) && !HasMagic(prefix, ":)xD"u8))
        {
            throw new InvalidDataException("File is not an Endfield PCK.");
        }
        uint encodedHeaderSize = BinaryPrimitives.ReadUInt32LittleEndian(prefix[4..]);
        if (encodedHeaderSize < 16 || encodedHeaderSize > 64 * 1024 * 1024)
        {
            throw new InvalidDataException("PCK header size is unsupported.");
        }
        int headerLength = checked((int)encodedHeaderSize + 8);
        byte[] header = new byte[headerLength];
        stream.Position = 0;
        stream.ReadExactly(header);
        if (HasMagic(header, ":)xD"u8))
        {
            DecryptVfs(header.AsSpan(12, checked((int)encodedHeaderSize - 4)),
                encodedHeaderSize);
            "AKPK"u8.CopyTo(header);
            BinaryPrimitives.WriteUInt32LittleEndian(header.AsSpan(8), 1);
        }
        if (!HasMagic(header, "AKPK"u8))
        {
            throw new InvalidDataException("PCK header decryption failed.");
        }

        int position = 4;
        uint parsedHeaderSize = ReadUInt32(header, ref position);
        _ = ReadUInt32(header, ref position);
        uint languagesSize = ReadUInt32(header, ref position);
        uint banksSize = ReadUInt32(header, ref position);
        uint soundsSize = ReadUInt32(header, ref position);
        uint externalsSize = 0;
        if ((ulong)languagesSize + banksSize + soundsSize + 0x10 < parsedHeaderSize)
        {
            externalsSize = ReadUInt32(header, ref position);
        }

        int languagesStart = position;
        int banksStart = checked(languagesStart + (int)languagesSize);
        int soundsStart = checked(banksStart + (int)banksSize);
        int externalsStart = checked(soundsStart + (int)soundsSize);
        EnsureSector(header, languagesStart, languagesSize);
        EnsureSector(header, banksStart, banksSize);
        EnsureSector(header, soundsStart, soundsSize);
        EnsureSector(header, externalsStart, externalsSize);

        var media = new Dictionary<uint, PckMedia>();
        ParseMediaSector(header, soundsStart, soundsSize, external: false, media);
        ParseMediaSector(header, externalsStart, externalsSize, external: true, media);
        var info = new FileInfo(path);
        return new PckIndex(
            path,
            info.Length,
            info.LastWriteTimeUtc.Ticks,
            header.Length,
            Convert.ToHexString(SHA256.HashData(header)),
            media);
    }

    private static void ParseMediaSector(
        byte[] header,
        int start,
        uint size,
        bool external,
        Dictionary<uint, PckMedia> media)
    {
        if (size == 0)
        {
            return;
        }
        int position = start;
        uint count = ReadUInt32(header, ref position);
        if (count == 0)
        {
            return;
        }
        uint payloadSize = size - sizeof(uint);
        if (payloadSize % count != 0)
        {
            throw new InvalidDataException("PCK media sector is not aligned.");
        }
        uint entrySize = payloadSize / count;
        if (entrySize is not 20 and not 24)
        {
            throw new InvalidDataException("PCK media entry size is unsupported.");
        }

        for (uint index = 0; index < count; index++)
        {
            int entryStart = position;
            ulong fileId = ReadUInt32(header, ref position);
            if (entrySize == 24 && external)
            {
                fileId |= (ulong)ReadUInt32(header, ref position) << 32;
            }
            uint blockSize = ReadUInt32(header, ref position);
            ulong fileSize = entrySize == 24 && !external
                ? ReadUInt64(header, ref position)
                : ReadUInt32(header, ref position);
            ulong fileOffset = ReadUInt32(header, ref position);
            _ = ReadUInt32(header, ref position);
            if (blockSize != 0)
            {
                fileOffset = checked(fileOffset * blockSize);
            }
            if (fileId <= uint.MaxValue && fileSize <= uint.MaxValue)
            {
                media.TryAdd((uint)fileId, new PckMedia(fileOffset, (uint)fileSize));
            }
            position = checked(entryStart + (int)entrySize);
        }
    }

    private static void BuildCatalog(
        string outputPath,
        string indexIdentity,
        IReadOnlyList<PckIndex> packages,
        CatalogRouteSet routeSet,
        CancellationToken cancellationToken)
    {
        var routes = new SortedDictionary<(uint SourceLanguage, uint SourceId), uint>();
        for (int index = 0; index < routeSet.Routes.Count; index += 3)
        {
            routes.TryAdd(
                (routeSet.Routes[index], routeSet.Routes[index + 1]),
                routeSet.Routes[index + 2]);
        }
        var targets = new SortedSet<uint>(routes.Values);
        var mediaSources = new Dictionary<uint, PckMediaSource>();
        foreach (PckIndex package in packages)
        {
            foreach ((uint mediaId, PckMedia media) in package.Media)
            {
                mediaSources.TryAdd(mediaId, new PckMediaSource(package, media));
            }
        }
        foreach (uint target in targets)
        {
            if (!mediaSources.ContainsKey(target))
            {
                throw new InvalidOperationException(
                    $"当前语言包缺少 Media ID {target}，请更新游戏语言包或资源清单。");
            }
        }

        var payloads = new Dictionary<uint, byte[]>(targets.Count);
        var sources = new Dictionary<string, FileStream>(
            StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (PckIndex package in packages)
            {
                sources.Add(package.Path, new FileStream(
                    package.Path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite,
                    1024 * 1024,
                    FileOptions.RandomAccess));
            }
            foreach (uint target in targets)
            {
                cancellationToken.ThrowIfCancellationRequested();
                PckMediaSource mediaSource = mediaSources[target];
                PckMedia media = mediaSource.Media;
                FileStream source = sources[mediaSource.Package.Path];
                if (media.Size > int.MaxValue ||
                    media.Offset + media.Size > (ulong)source.Length)
                {
                    throw new InvalidDataException($"PCK Media ID {target} 越界。");
                }
                byte[] payload = new byte[(int)media.Size];
                source.Position = checked((long)media.Offset);
                source.ReadExactly(payload);
                DecryptVfs(payload, target);
                payloads[target] = payload;
            }
        }
        finally
        {
            foreach (FileStream source in sources.Values)
            {
                source.Dispose();
            }
        }

        ulong dataOffset = checked(
            (ulong)CatalogHeaderSize + (ulong)CatalogEntrySize * (uint)routes.Count);
        ulong cursor = dataOffset;
        var payloadOffsets = new Dictionary<uint, ulong>(targets.Count);
        foreach (uint target in targets)
        {
            payloadOffsets[target] = cursor;
            cursor = checked(cursor + (ulong)payloads[target].Length);
        }
        uint durationTableOffset = checked((uint)cursor);

        var durations = new SortedDictionary<string, float>(StringComparer.Ordinal);
        var targetsBySourceId = routes
            .GroupBy(pair => pair.Key.SourceId)
            .ToDictionary(group => group.Key, group => group.First().Value);
        foreach ((string identity, List<uint> sourceMediaIds) in routeSet.VoiceSources)
        {
            double longest = 0;
            foreach (uint sourceMediaId in sourceMediaIds)
            {
                if (targetsBySourceId.TryGetValue(sourceMediaId, out uint target) &&
                    payloads.TryGetValue(target, out byte[]? payload))
                {
                    longest = Math.Max(longest, WemDurationSeconds(payload));
                }
            }
            if (longest > 0)
            {
                durations.TryAdd(identity, (float)longest);
            }
        }

        string temporary = outputPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            using (var output = new FileStream(
                temporary,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                1024 * 1024,
                FileOptions.SequentialScan))
            {
                using var writer = new BinaryWriter(output, Encoding.UTF8, leaveOpen: true);
                writer.Write("BEVCAT01"u8);
                writer.Write((ushort)2);
                writer.Write((ushort)routeSet.Language);
                writer.Write((uint)routes.Count);
                writer.Write(durationTableOffset);
                writer.Write((ulong)CatalogHeaderSize);
                writer.Write(dataOffset);
                foreach (((uint sourceLanguage, uint sourceMediaId), uint target) in routes)
                {
                    byte[] payload = payloads[target];
                    writer.Write(sourceMediaId);
                    writer.Write(target);
                    writer.Write(payloadOffsets[target]);
                    writer.Write((uint)payload.Length);
                    writer.Write(sourceLanguage);
                }
                foreach (uint target in targets)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    output.Write(payloads[target]);
                }
                writer.Write((uint)durations.Count);
                foreach ((string identity, float seconds) in durations)
                {
                    byte[] identityBytes = Encoding.UTF8.GetBytes(identity);
                    writer.Write((uint)identityBytes.Length);
                    writer.Write(identityBytes);
                    writer.Write(seconds);
                }
                output.Flush(flushToDisk: true);
            }
            File.Move(temporary, outputPath, overwrite: true);

            var info = new FileInfo(outputPath);
            var report = new CatalogReport
            {
                TargetLanguage = LanguageNames[routeSet.Language],
                CharacterId = routeSet.CharacterId,
                EntryCount = routes.Count,
                DurationIdentityCount = durations.Count,
                UniqueTargetMediaCount = targets.Count,
                PayloadBytes = checked((long)(durationTableOffset - dataOffset)),
                CatalogLength = info.Length,
                CatalogLastWriteUtcTicks = info.LastWriteTimeUtc.Ticks,
                CatalogSha256 = HashFile(outputPath),
                CatalogIndexSha256 = indexIdentity,
                SourcePackages = packages.Select(package => new CatalogSourcePackageReport
                {
                    SourcePackage = package.Path,
                    SourcePackageSize = package.Size,
                    SourcePackageLastWriteUtcTicks = package.LastWriteUtcTicks,
                    SourcePackageHeaderSha256 = package.HeaderSha256
                }).ToList()
            };
            WriteJsonAtomicallyAsync(
                outputPath + ".json",
                report,
                cancellationToken).GetAwaiter().GetResult();
        }
        finally
        {
            DeleteIfPresent(temporary);
        }
    }

    private static double WemDurationSeconds(ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 32 ||
            !payload[..4].SequenceEqual("RIFF"u8) ||
            !payload.Slice(8, 4).SequenceEqual("WAVE"u8))
        {
            return 0;
        }
        uint sampleRate = 0;
        uint averageBytesPerSecond = 0;
        int position = 12;
        while (position + 8 <= payload.Length)
        {
            uint chunkSize = BinaryPrimitives.ReadUInt32LittleEndian(
                payload.Slice(position + 4, 4));
            int available = payload.Length - position - 8;
            int boundedSize = (int)Math.Min(chunkSize, (uint)Math.Max(0, available));
            if (payload.Slice(position, 4).SequenceEqual("fmt "u8) && boundedSize >= 16)
            {
                ReadOnlySpan<byte> format = payload.Slice(position + 8, boundedSize);
                sampleRate = BinaryPrimitives.ReadUInt32LittleEndian(format.Slice(4, 4));
                averageBytesPerSecond = BinaryPrimitives.ReadUInt32LittleEndian(
                    format.Slice(8, 4));
                if (boundedSize >= 28 && sampleRate > 0)
                {
                    uint sampleCount = BinaryPrimitives.ReadUInt32LittleEndian(
                        format.Slice(24, 4));
                    if (sampleCount > 0)
                    {
                        return (double)sampleCount / sampleRate;
                    }
                }
            }
            else if (payload.Slice(position, 4).SequenceEqual("data"u8) &&
                averageBytesPerSecond > 0)
            {
                return (double)boundedSize / averageBytesPerSecond;
            }
            position = checked(position + 8 + (int)((chunkSize + 1) & ~1u));
        }
        return 0;
    }

    private static bool IsCurrentCatalog(
        string path,
        string indexIdentity,
        IReadOnlyList<PckIndex> packages,
        CatalogRouteSet routeSet)
    {
        try
        {
            if (!File.Exists(path) || !File.Exists(path + ".json"))
            {
                return false;
            }
            CatalogReport? report = JsonSerializer.Deserialize<CatalogReport>(
                File.ReadAllText(path + ".json"),
                JsonOptions);
            var info = new FileInfo(path);
            return report is not null &&
                report.SchemaVersion == 3 &&
                report.Kind == "betterendfield-voice-catalog" &&
                report.CharacterId.Equals(
                    routeSet.CharacterId,
                    StringComparison.OrdinalIgnoreCase) &&
                report.TargetLanguage.Equals(
                    LanguageNames[routeSet.Language],
                    StringComparison.OrdinalIgnoreCase) &&
                report.EntryCount == routeSet.Routes.Count / 3 &&
                report.CatalogIndexSha256.Equals(
                    indexIdentity,
                    StringComparison.OrdinalIgnoreCase) &&
                report.CatalogVersion == 3 &&
                report.DurationIdentityCount == CountDurationIdentities(routeSet) &&
                report.SourcePackages.Count == packages.Count &&
                report.SourcePackages.Zip(packages).All(pair =>
                    pair.First.SourcePackageHeaderSha256.Equals(
                        pair.Second.HeaderSha256,
                        StringComparison.OrdinalIgnoreCase) &&
                    pair.First.SourcePackageSize == pair.Second.Size &&
                    pair.First.SourcePackageLastWriteUtcTicks ==
                        pair.Second.LastWriteUtcTicks) &&
                report.CatalogLength == info.Length &&
                report.CatalogLastWriteUtcTicks == info.LastWriteTimeUtc.Ticks;
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or JsonException)
        {
            return false;
        }
    }

    private static int CountDurationIdentities(CatalogRouteSet routeSet)
    {
        var sourceIds = new HashSet<uint>();
        for (int index = 0; index + 2 < routeSet.Routes.Count; index += 3)
        {
            sourceIds.Add(routeSet.Routes[index + 1]);
        }
        return routeSet.VoiceSources.Count(pair => pair.Value.Any(sourceIds.Contains));
    }

    private static int LanguageIndex(string language)
    {
        for (int index = 0; index < LanguageNames.Length; index++)
        {
            if (LanguageNames[index].Equals(language, StringComparison.OrdinalIgnoreCase))
            {
                return index;
            }
        }
        throw new InvalidOperationException($"不支持配音语言“{language}”。");
    }

    private static string CatalogFileName(int language, string characterId)
    {
        string prefix = "voice." + LanguageNames[language].ToLowerInvariant();
        return characterId == "*"
            ? prefix + ".becat"
            : prefix + "." + characterId.ToLowerInvariant() + ".becat";
    }

    private static string GetCatalogRoot() => Path.Combine(
        ConfigurationService.SettingsDirectory,
        "catalog");

    private static bool IsManagedCatalogFileName(string value) =>
        value.Equals(Path.GetFileName(value), StringComparison.Ordinal) &&
        value.StartsWith("voice.", StringComparison.OrdinalIgnoreCase) &&
        value.EndsWith(".becat", StringComparison.OrdinalIgnoreCase);

    private static IEnumerable<string> EnumerateFilesSafely(
        string root,
        string pattern)
    {
        var pending = new Stack<string>();
        pending.Push(root);
        while (pending.Count > 0)
        {
            string current = pending.Pop();
            string[] directories;
            string[] files;
            try
            {
                directories = Directory.GetDirectories(current);
                files = Directory.GetFiles(current, pattern);
            }
            catch (Exception exception) when (
                exception is IOException or UnauthorizedAccessException)
            {
                continue;
            }
            foreach (string directory in directories)
            {
                pending.Push(directory);
            }
            foreach (string file in files)
            {
                yield return file;
            }
        }
    }

    private static void EnsureSector(byte[] data, int start, uint size)
    {
        if (start < 0 || (ulong)start + size > (ulong)data.Length)
        {
            throw new InvalidDataException("PCK header sector is out of bounds.");
        }
    }

    private static bool HasMagic(ReadOnlySpan<byte> value, ReadOnlySpan<byte> magic) =>
        value.Length >= magic.Length && value[..magic.Length].SequenceEqual(magic);

    private static uint ReadUInt32(byte[] value, ref int position)
    {
        if (position < 0 || position + sizeof(uint) > value.Length)
        {
            throw new InvalidDataException("PCK header is truncated.");
        }
        uint result = BinaryPrimitives.ReadUInt32LittleEndian(value.AsSpan(position));
        position += sizeof(uint);
        return result;
    }

    private static ulong ReadUInt64(byte[] value, ref int position)
    {
        if (position < 0 || position + sizeof(ulong) > value.Length)
        {
            throw new InvalidDataException("PCK header is truncated.");
        }
        ulong result = BinaryPrimitives.ReadUInt64LittleEndian(value.AsSpan(position));
        position += sizeof(ulong);
        return result;
    }

    private static void DecryptVfs(Span<byte> data, uint seed)
    {
        int position = 0;
        uint keyIndex = seed;
        while (data.Length - position >= sizeof(uint))
        {
            uint value = BinaryPrimitives.ReadUInt32LittleEndian(data[position..]);
            BinaryPrimitives.WriteUInt32LittleEndian(
                data[position..],
                value ^ DeriveKey(keyIndex));
            position += sizeof(uint);
            keyIndex++;
        }
        if (position < data.Length)
        {
            uint key = DeriveKey(keyIndex);
            for (int index = 0; position + index < data.Length; index++)
            {
                data[position + index] ^= (byte)(key >> (index * 8));
            }
        }
    }

    private static uint DeriveKey(uint seed)
    {
        uint key = unchecked(((seed & 0xFF) ^ XorConstant) * MultiplyConstant);
        key = unchecked((key ^ ((seed >> 8) & 0xFF)) * MultiplyConstant);
        key = unchecked((key ^ ((seed >> 16) & 0xFF)) * MultiplyConstant);
        return unchecked((key ^ ((seed >> 24) & 0xFF)) * MultiplyConstant);
    }

    private static string HashFile(string path)
    {
        using FileStream stream = File.OpenRead(path);
        return Convert.ToHexString(SHA256.HashData(stream));
    }

    private static async Task<CatalogRegistry> ReadRegistryAsync(
        string path,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!File.Exists(path))
            {
                return new CatalogRegistry();
            }
            await using FileStream stream = File.OpenRead(path);
            CatalogRegistry? registry = await JsonSerializer.DeserializeAsync<CatalogRegistry>(
                stream,
                JsonOptions,
                cancellationToken);
            return registry is { SchemaVersion: 1, Kind: "betterendfield-generated-catalogs" }
                ? registry
                : new CatalogRegistry();
        }
        catch (Exception exception) when (exception is IOException or JsonException)
        {
            return new CatalogRegistry();
        }
    }

    private static async Task WriteJsonAtomicallyAsync<T>(
        string path,
        T value,
        CancellationToken cancellationToken)
    {
        string temporary = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            await using (var stream = new FileStream(
                temporary,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                64 * 1024,
                FileOptions.SequentialScan))
            {
                await JsonSerializer.SerializeAsync(
                    stream,
                    value,
                    JsonOptions,
                    cancellationToken);
                await stream.FlushAsync(cancellationToken);
            }
            File.Move(temporary, path, overwrite: true);
        }
        finally
        {
            DeleteIfPresent(temporary);
        }
    }

    private static void DeleteIfPresent(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    private sealed record LoadedCatalogIndex(VoiceCatalogIndex Value, string Identity);

    private sealed record PckMedia(ulong Offset, uint Size);

    private sealed record PckMediaSource(PckIndex Package, PckMedia Media);

    private sealed record PckIndex(
        string Path,
        long Size,
        long LastWriteUtcTicks,
        int HeaderSize,
        string HeaderSha256,
        Dictionary<uint, PckMedia> Media);

    private sealed class VoiceCatalogIndex
    {
        public int SchemaVersion { get; set; }
        public string Kind { get; set; } = string.Empty;
        public string SourceManifestSha256 { get; set; } = string.Empty;
        public List<CatalogPackage> Packages { get; set; } = [];
        public List<CatalogRouteSet> Catalogs { get; set; } = [];
    }

    private sealed class CatalogPackage
    {
        public int Language { get; set; }
        public string Source { get; set; } = string.Empty;
        public long Size { get; set; }
        public int HeaderSize { get; set; }
        public string HeaderSha256 { get; set; } = string.Empty;
    }

    private sealed class CatalogRouteSet
    {
        public string CharacterId { get; set; } = string.Empty;
        public int Language { get; set; }
        public int VoiceCount { get; set; }
        public List<uint> Routes { get; set; } = [];
        public Dictionary<string, List<uint>> VoiceSources { get; set; } = [];
    }

    private sealed class CatalogReport
    {
        public int SchemaVersion { get; set; } = 3;
        public string Kind { get; set; } = "betterendfield-voice-catalog";
        public int CatalogVersion { get; set; } = 3;
        public string TargetLanguage { get; set; } = string.Empty;
        public string CharacterId { get; set; } = string.Empty;
        public int EntryCount { get; set; }
        public int DurationIdentityCount { get; set; }
        public int UniqueTargetMediaCount { get; set; }
        public long PayloadBytes { get; set; }
        public long CatalogLength { get; set; }
        public long CatalogLastWriteUtcTicks { get; set; }
        public string CatalogSha256 { get; set; } = string.Empty;
        public string CatalogIndexSha256 { get; set; } = string.Empty;
        public List<CatalogSourcePackageReport> SourcePackages { get; set; } = [];
    }

    private sealed class CatalogSourcePackageReport
    {
        public string SourcePackage { get; set; } = string.Empty;
        public long SourcePackageSize { get; set; }
        public long SourcePackageLastWriteUtcTicks { get; set; }
        public string SourcePackageHeaderSha256 { get; set; } = string.Empty;
    }

    private sealed class CatalogRegistry
    {
        public int SchemaVersion { get; set; } = 1;
        public string Kind { get; set; } = "betterendfield-generated-catalogs";
        public List<string> FileNames { get; set; } = [];
    }
}
