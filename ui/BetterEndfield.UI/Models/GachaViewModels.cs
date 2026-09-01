using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace BetterEndfield.UI.Models;

public sealed class GachaSummaryView
{
    public string Title { get; set; } = "";
    public int TotalPulls { get; set; }
    public int SixStars { get; set; }
    public int UpCount { get; set; }
    public int OffRateCount { get; set; }
    public int PaidPulls { get; set; }
    public string UpOffText => $"{UpCount} / {OffRateCount}";
    public string UpAverage => UpCount == 0 ? "-" : $"{(double)PaidPulls / UpCount:0.0}";
}

public sealed class GachaStarView
{
    public string Name { get; set; } = "";
    public string? ItemId { get; set; }
    public ImageSource? IconSource { get; set; }
    public int Pity { get; set; }
    public int CarryPity { get; set; }
    public bool IsFree { get; set; }
    public bool IsUp { get; set; }
    public bool IsCurrentPity { get; set; }
    public string Badge => IsFree ? "免费" : IsUp ? "UP" : "歪";
    public Brush BadgeBrush => new SolidColorBrush(Windows.UI.Color.FromArgb(255, IsFree ? (byte)100 : IsUp ? (byte)20 : (byte)210, IsFree ? (byte)100 : IsUp ? (byte)160 : (byte)70, IsFree ? (byte)100 : IsUp ? (byte)90 : (byte)80));
    public double BarWidth => Math.Clamp(Pity, 1, 120) / 120d * 240d;
    public double CarryBarWidth => Math.Clamp(CarryPity, 0, 120) / 120d * 240d;
    public double PaidBarWidth => Math.Max(0, BarWidth - CarryBarWidth);
    public Brush BarBrush => new SolidColorBrush(Windows.UI.Color.FromArgb(255, Pity <= 30 ? (byte)40 : Pity <= 60 ? (byte)235 : (byte)225, Pity <= 30 ? (byte)190 : Pity <= 60 ? (byte)180 : (byte)75, Pity <= 30 ? (byte)130 : Pity <= 60 ? (byte)35 : (byte)95));
    public Brush CarryBarBrush => new SolidColorBrush(Windows.UI.Color.FromArgb(255, 45, 130, 220));
    public string Detail => IsFree && string.IsNullOrWhiteSpace(Name) ? "10 抽" : CarryPity > 0 ? $"{Pity - CarryPity} 抽 + 垫 {CarryPity} 抽 = {Pity} 抽" : $"第 {Pity} 抽";
}

public sealed class GachaPoolView
{
    public string PoolName { get; set; } = "";
    public string PoolPeriod { get; set; } = "";
    public int TotalPulls { get; set; }
    public int FreePulls { get; set; }
    public int CurrentPity { get; set; }
    public bool ShowCurrentPity { get; set; }
    public GachaStarView CurrentPityRow { get; set; } = new();
    public Visibility CurrentPityVisibility => ShowCurrentPity ? Visibility.Visible : Visibility.Collapsed;
    public bool HasSixStars => SixStars.Count != 0;
    public ObservableCollection<GachaStarView> SixStars { get; } = [];
    public ObservableCollection<GachaStarView> FreeSixStars { get; } = [];
    public string TotalText => $"合计 {TotalPulls} 抽";
    public string FreeText => FreePulls == 0 ? "" : $"免费 {FreePulls} 抽";
    public string FreeSectionTitle => FreePulls == 0 ? "" : "免费十连";
}
