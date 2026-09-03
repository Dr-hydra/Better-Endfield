using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace BetterEndfield.UI.Services;

internal enum GpuVendor
{
    Unknown,
    Amd,
    Nvidia,
    Intel
}

internal enum GpuArchitecture
{
    Unknown,
    AmdPreRdna,
    AmdRdna1,
    AmdRdna2,
    AmdRdna3,
    AmdRdna4,
    Nvidia,
    IntelArc
}

internal sealed record GpuInfo(
    GpuVendor Vendor,
    GpuArchitecture Architecture,
    string Description)
{
    public static GpuInfo Unknown { get; } =
        new(GpuVendor.Unknown, GpuArchitecture.Unknown, "未识别的显示适配器");
}

/// <summary>
/// 识别当前接驳桌面的显示适配器。厂商取自 PnP 设备 ID 的 VEN_ 字段，架构代次取自
/// 适配器描述串。不使用 DeviceId 白名单：新硬件会持续出现，落在白名单外的设备
/// 应回退到 Unknown 由用户手工选择，而不是被错误归类。
/// </summary>
internal static partial class GpuDetectionService
{
    private const int DisplayDeviceAttachedToDesktop = 0x1;
    private const int DisplayDevicePrimaryDevice = 0x4;

    private const int VendorAmd = 0x1002;
    private const int VendorNvidia = 0x10DE;
    private const int VendorIntel = 0x8086;

    [GeneratedRegex(@"VEN_([0-9A-Fa-f]{4})", RegexOptions.CultureInvariant)]
    private static partial Regex VendorPattern();

    // "AMD Radeon RX 6750 XT" -> 6, "Radeon RX 9070 XT" -> 9
    [GeneratedRegex(@"\bRX\s*(\d)\d{3}\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex RadeonSeriesPattern();

    /// <summary>
    /// 按可信度择一：先取能识别出架构的适配器，其次取厂商可识别的适配器，最后才
    /// 回退到第一个。串流与远程桌面软件会注册 ROOT\DISPLAY 下的虚拟适配器，它们
    /// 没有 VEN_ 字段，必须排在真实 GPU 之后，否则会盖掉真实硬件。
    /// </summary>
    public static GpuInfo Detect()
    {
        GpuInfo? vendorOnly = null;
        GpuInfo? first = null;
        foreach (GpuInfo adapter in EnumerateAdapters())
        {
            if (adapter.Architecture != GpuArchitecture.Unknown)
            {
                return adapter;
            }
            if (adapter.Vendor != GpuVendor.Unknown)
            {
                vendorOnly ??= adapter;
            }
            first ??= adapter;
        }
        return vendorOnly ?? first ?? GpuInfo.Unknown;
    }

    public static IReadOnlyList<GpuInfo> EnumerateAdapters()
    {
        var adapters = new List<GpuInfo>();
        var device = new DisplayDevice { cb = Marshal.SizeOf<DisplayDevice>() };
        for (uint index = 0; EnumDisplayDevicesW(null, index, ref device, 0); index++)
        {
            device.cb = Marshal.SizeOf<DisplayDevice>();
            bool attached =
                (device.StateFlags & (DisplayDeviceAttachedToDesktop | DisplayDevicePrimaryDevice)) != 0;
            if (!attached)
            {
                continue;
            }
            GpuVendor vendor = ParseVendor(device.DeviceID);
            adapters.Add(new GpuInfo(
                vendor,
                ClassifyArchitecture(vendor, device.DeviceString),
                string.IsNullOrWhiteSpace(device.DeviceString)
                    ? GpuInfo.Unknown.Description
                    : device.DeviceString));
        }
        return adapters;
    }

    private static GpuVendor ParseVendor(string deviceId)
    {
        Match match = VendorPattern().Match(deviceId ?? string.Empty);
        if (!match.Success ||
            !int.TryParse(
                match.Groups[1].ValueSpan,
                System.Globalization.NumberStyles.HexNumber,
                System.Globalization.CultureInfo.InvariantCulture,
                out int vendorId))
        {
            return GpuVendor.Unknown;
        }
        return vendorId switch
        {
            VendorAmd => GpuVendor.Amd,
            VendorNvidia => GpuVendor.Nvidia,
            VendorIntel => GpuVendor.Intel,
            _ => GpuVendor.Unknown
        };
    }

    private static GpuArchitecture ClassifyArchitecture(GpuVendor vendor, string description)
    {
        description ??= string.Empty;
        switch (vendor)
        {
            case GpuVendor.Nvidia:
                return GpuArchitecture.Nvidia;

            case GpuVendor.Intel:
                return description.Contains("Arc", StringComparison.OrdinalIgnoreCase)
                    ? GpuArchitecture.IntelArc
                    : GpuArchitecture.Unknown;

            case GpuVendor.Amd:
                Match series = RadeonSeriesPattern().Match(description);
                if (!series.Success)
                {
                    // APU 与工作站卡的命名不含 RX 四位型号，交给用户手工选择。
                    return GpuArchitecture.Unknown;
                }
                return series.Groups[1].ValueSpan[0] switch
                {
                    '9' => GpuArchitecture.AmdRdna4,
                    '7' => GpuArchitecture.AmdRdna3,
                    '6' => GpuArchitecture.AmdRdna2,
                    '5' => GpuArchitecture.AmdRdna1,
                    _ => GpuArchitecture.AmdPreRdna
                };

            default:
                return GpuArchitecture.Unknown;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct DisplayDevice
    {
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;
        public int StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumDisplayDevicesW(
        string? device,
        uint deviceIndex,
        ref DisplayDevice displayDevice,
        uint flags);
}
