using System.Text;
using BetterEndfield.UI.Models;

namespace BetterEndfield.UI.Services;

/// <summary>
/// 生成客户端目录中的 OptiScaler.ini。
///
/// 生成方式是读取发布包内附注释的模板，只覆写受管键，其余键与全部注释原样保留。
/// 不从零构造该文件：键名与取值域随上游版本变动（0.9.4 的 [Dx11withDx12] 只有
/// UseDelayedInit 与 DontUseNTShared，早期文档中的同步键已不存在），凭记忆构造会
/// 写出无效配置且不报错。模板中找不到受管键时直接失败，使版本漂移可见。
/// </summary>
internal static class OptiScalerConfigurationService
{
    private const string TemplateFileName = "OptiScaler.ini";

    /// <summary>Windows 11 的首个内部版本号，用于区分 Win10 的 FSR4 额外要求。</summary>
    private const int Windows11Build = 22000;

    private static bool IsWindows10 => Environment.OSVersion.Version.Build < Windows11Build;

    public static async Task<IReadOnlyList<string>> ApplyAsync(
        string gameExecutablePath,
        string injectorPath,
        DisplayConfiguration configuration,
        GpuInfo gpu)
    {
        if (!RuntimePathDiscoveryService.IsGameExecutable(gameExecutablePath))
        {
            throw new FileNotFoundException("未找到有效的 Endfield.exe。", gameExecutablePath);
        }
        string installRoot = ConfigurationService.ResolveInstallRoot(injectorPath);
        string template = Path.Combine(
            installRoot, "payloads", "optiscaler", TemplateFileName);
        if (!File.Exists(template))
        {
            throw new FileNotFoundException(
                "组件包缺少 OptiScaler.ini 模板，无法生成配置。", template);
        }
        string gameDirectory = Path.GetDirectoryName(Path.GetFullPath(gameExecutablePath)) ??
            throw new InvalidOperationException("客户端目录无效。");

        Dictionary<(string Section, string Key), string> managed = BuildOverrides(configuration);
        string[] lines = await File.ReadAllLinesAsync(template);
        var applied = new HashSet<(string, string)>();
        var output = new StringBuilder();
        string section = string.Empty;

        foreach (string line in lines)
        {
            string trimmed = line.TrimStart();
            if (trimmed.StartsWith('[') && trimmed.Contains(']'))
            {
                section = trimmed[1..trimmed.IndexOf(']')].Trim();
                output.AppendLine(line);
                continue;
            }

            int separator = trimmed.IndexOf('=');
            if (separator <= 0 || trimmed.StartsWith(';'))
            {
                output.AppendLine(line);
                continue;
            }

            string key = trimmed[..separator].Trim();
            if (managed.TryGetValue((section, key), out string? value))
            {
                output.AppendLine($"{key}={value}");
                applied.Add((section, key));
                continue;
            }
            output.AppendLine(line);
        }

        var missing = managed.Keys
            .Where(entry => !applied.Contains(entry))
            .Select(entry => $"[{entry.Section}] {entry.Key}")
            .OrderBy(entry => entry, StringComparer.Ordinal)
            .ToArray();
        if (missing.Length > 0)
        {
            throw new InvalidDataException(
                "OptiScaler.ini 模板中缺少以下受管键，可能是组件版本与锁定清单不符：" +
                string.Join("、", missing));
        }

        string target = Path.Combine(gameDirectory, TemplateFileName);
        string temporary = target + ".betterendfield.tmp";
        try
        {
            await File.WriteAllTextAsync(temporary, output.ToString(), new UTF8Encoding(false));
            File.Move(temporary, target, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporary))
            {
                File.Delete(temporary);
            }
        }

        await OptiScalerDeploymentService.RegisterGeneratedConfigurationAsync(
            gameExecutablePath, injectorPath);
        return DescribeNotes(configuration, gpu);
    }

    private static Dictionary<(string, string), string> BuildOverrides(
        DisplayConfiguration configuration)
    {
        bool fsr4 = configuration.Backend
            is UpscalerBackend.Fsr4Fp8 or UpscalerBackend.Fsr4Int8;

        // fsr31_12 是 FSR4 的选择值：DX11 走 dx11on12、Vulkan 走 VKon12，两者都经
        // DX12 interop。客户端无 DX12 后端，因此这两个键都要写。
        string upscaler = configuration.Backend switch
        {
            UpscalerBackend.Fsr4Fp8 or UpscalerBackend.Fsr4Int8 => "fsr31_12",
            UpscalerBackend.Fsr31 => "fsr31",
            UpscalerBackend.XeSS => "xess",
            _ => "auto"
        };

        var overrides = new Dictionary<(string, string), string>
        {
            [("Upscalers", "Dx11Upscaler")] = upscaler,
            [("Upscalers", "VulkanUpscaler")] = upscaler,

            // 客户端的 FSR3 为引擎内置，没有 FidelityFX API DLL 可挂，只能拦 DLSS 输入。
            [("Inputs", "EnableDlssInputs")] = "true",

            [("FSR", "Fsr4Update")] = Boolean(fsr4),
            [("FSR", "Fsr4ForceEnableInt8")] =
                Boolean(configuration.Backend == UpscalerBackend.Fsr4Int8),
            // Win10 上 FSR4 需要升级 Agility SDK，依赖随包部署的 D3D12_Optiscaler 目录。
            [("FSR", "FsrAgilitySDKUpgrade")] = Boolean(fsr4 && IsWindows10),

            [("Spoofing", "Dxgi")] = Boolean(configuration.GpuSpoofing),
            // 客户端走完整 Streamline 栈，DLSS 调用不经裸 NGX，需单独放开该层的欺骗。
            [("Spoofing", "StreamlineSpoofing")] = Boolean(configuration.GpuSpoofing),

            // 客户端已有原生 DLSS 帧生成，上游也建议 OptiFG 仅用于无原生 FG 的场景。
            [("FrameGen", "Enabled")] = "false",
            [("FrameGen", "FGInput")] = "nofg",
            [("FrameGen", "FGOutput")] = "nofg",

            [("Dx11withDx12", "UseDelayedInit")] = Boolean(fsr4),

            [("Log", "LogToFile")] = Boolean(configuration.Diagnostics),
            [("Log", "LogLevel")] = configuration.Diagnostics ? "0" : "auto"
        };
        return overrides;
    }

    private static IReadOnlyList<string> DescribeNotes(
        DisplayConfiguration configuration,
        GpuInfo gpu)
    {
        var notes = new List<string>();
        if (configuration.GpuSpoofing)
        {
            notes.Add(
                "已启用 GPU 欺骗。客户端的 FSR3 为引擎内置、无可拦截的 FidelityFX API，" +
                "只有让客户端暴露 DLSS 选项才能接管；请在客户端【画质提升】中选择 NVIDIA DLSS。");
        }
        if (configuration.RequiresInteropTuning)
        {
            notes.Add(
                "FSR4 经 DX12 interop 运行，客户端的 DX11 与 Vulkan 后端都会额外承担一层" +
                "同步开销，实际代价高于原生 DX12 游戏的公开实测数据。");
        }
        if (configuration.Backend == UpscalerBackend.Fsr4Int8 &&
            gpu.Architecture == GpuArchitecture.AmdRdna2)
        {
            notes.Add(
                "已为 RDNA2 强制启用 INT8 模型。该架构无 WMMA 指令，模型以 DP4a 模拟运行，" +
                "建议与客户端原生 FSR3 做等帧率画质对比后再决定是否保留。");
        }
        if (configuration.RequiresInteropTuning && IsWindows10)
        {
            notes.Add(
                "检测到 Windows 10，已启用 Agility SDK 升级；该选项依赖客户端目录下的 " +
                "D3D12_Optiscaler 文件夹，卸载时会一并移除。");
        }
        notes.Add("覆盖层默认由 Insert 键呼出，与战斗统计模块的 F11/F12 不冲突。");
        return notes;
    }

    private static string Boolean(bool value) => value ? "true" : "false";
}
