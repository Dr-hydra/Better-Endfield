using BetterEndfield.UI.Services;

namespace BetterEndfield.UI.Models;

internal enum UpscalerBackend
{
    /// <summary>不部署显示增强，客户端使用自带的画质提升选项。</summary>
    Disabled,
    /// <summary>FSR4，硬件加速路径（RDNA4）。</summary>
    Fsr4Fp8,
    /// <summary>FSR4，INT8 模型（RDNA3 官方支持，RDNA2 为 DP4a 模拟）。</summary>
    Fsr4Int8,
    /// <summary>FSR 3.1，经 OptiScaler 提供 preset 与锐化调整。</summary>
    Fsr31,
    /// <summary>XeSS 2.x，Intel Arc 的原生 DX11 后端。</summary>
    XeSS
}

internal enum RenderBackend
{
    Unknown,
    DirectX11,
    Vulkan
}

internal sealed class DisplayConfiguration
{
    public bool Enabled { get; set; }

    public UpscalerBackend Backend { get; set; } = UpscalerBackend.Disabled;

    /// <summary>
    /// 客户端无 FidelityFX API DLL，FSR3 输入不可拦截，因此非 NVIDIA 硬件必须启用
    /// GPU 欺骗才能让客户端暴露 DLSS 选项供 OptiScaler 接管。详见
    /// docs/DISPLAY_PIPELINE.md。
    /// </summary>
    public bool GpuSpoofing { get; set; } = true;

    /// <summary>用户在客户端启动器中选定的渲染后端，用于决定是否写入 interop 参数。</summary>
    public RenderBackend RenderBackend { get; set; } = RenderBackend.Unknown;

    public bool Diagnostics { get; set; }

    /// <summary>
    /// 按检测到的硬件给出初始建议。这是建议值而非强制值：RDNA2 上 FSR4 与 FSR3.1
    /// 的取舍需要逐机验证，因此默认不选 FSR4。
    /// </summary>
    public static UpscalerBackend SuggestBackend(GpuInfo gpu) => gpu.Architecture switch
    {
        GpuArchitecture.AmdRdna4 => UpscalerBackend.Fsr4Fp8,
        GpuArchitecture.AmdRdna3 => UpscalerBackend.Fsr4Int8,
        GpuArchitecture.AmdRdna2 => UpscalerBackend.Fsr31,
        GpuArchitecture.IntelArc => UpscalerBackend.XeSS,
        // 客户端原生 DLSS 已是 N 卡上的最佳路径，没有引入代理的理由。
        GpuArchitecture.Nvidia => UpscalerBackend.Disabled,
        _ => UpscalerBackend.Disabled
    };

    public static string DescribeBackend(UpscalerBackend backend) => backend switch
    {
        UpscalerBackend.Fsr4Fp8 => "FSR 4（FP8，硬件加速）",
        UpscalerBackend.Fsr4Int8 => "FSR 4（INT8）",
        UpscalerBackend.Fsr31 => "FSR 3.1 增强",
        UpscalerBackend.XeSS => "XeSS 2.x",
        _ => "不启用"
    };

    /// <summary>
    /// 说明所选后端在该硬件上的已知代价，供 UI 直接展示。数据来源见
    /// docs/DISPLAY_PIPELINE.md 的策略矩阵。
    /// </summary>
    public static string? DescribeTradeoff(UpscalerBackend backend, GpuInfo gpu)
    {
        if (backend is not (UpscalerBackend.Fsr4Fp8 or UpscalerBackend.Fsr4Int8))
        {
            return null;
        }
        return gpu.Architecture switch
        {
            GpuArchitecture.AmdRdna4 => "硬件加速路径，帧率代价约 3–5%。",
            GpuArchitecture.AmdRdna3 => "官方 INT8 支持，帧率代价约 7–13%。",
            GpuArchitecture.AmdRdna2 =>
                "该架构无 WMMA 指令，INT8 模型以 DP4a 模拟运行：帧率代价约 9–20%，" +
                "画质也低于 FP8 版本。建议与客户端原生 FSR3 做等帧率对比后再决定。",
            _ => "该硬件不在 FSR4 的已验证范围内，结果未知。"
        };
    }

    /// <summary>FSR4 经 DX12 interop 运行，两种客户端后端都需要写入同步参数。</summary>
    public bool RequiresInteropTuning =>
        Backend is UpscalerBackend.Fsr4Fp8 or UpscalerBackend.Fsr4Int8;
}
