using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using BetterEndfield.UI.Models;
using BetterEndfield.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace BetterEndfield.UI.Views;

public sealed partial class GachaPage : UserControl
{
    private const string GachaWebUrl = "https://www.bilibili.com/toy/endfield/index.html";
    private readonly GachaSessionClient _sessions = new();
    private readonly GachaApiClient _api = new();
    private readonly GachaRecordStore _store = new();
    private List<GachaRecord> _characters = [];
    private List<GachaRecord> _weapons = [];
    private Dictionary<string, GachaPoolInfo> _poolInfos = new(StringComparer.Ordinal);
    private string _selectedCategory = "限定";
    public ObservableCollection<GachaRecord> Records { get; } = [];
    public ObservableCollection<GachaSummaryView> Summaries { get; } = [];
    public ObservableCollection<GachaPoolView> Pools { get; } = [];

    public GachaPage()
    {
        InitializeComponent();
        LimitedButton.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 30, 30, 30));
        Loaded += async (_, _) =>
        {
            if (Records.Count != 0) return;
            _poolInfos = GachaStaticPoolCatalog.Entries.ToDictionary(x => x.PoolId, StringComparer.Ordinal);
            foreach (KeyValuePair<string, GachaPoolInfo> item in await _store.LoadPoolInfosAsync())
            {
                GachaPoolInfo value = item.Value;
                if (_poolInfos.TryGetValue(item.Key, out GachaPoolInfo? bundled) &&
                    !string.IsNullOrWhiteSpace(bundled.PityCarryGroup) &&
                    string.IsNullOrWhiteSpace(value.PityCarryGroup))
                {
                    value = new GachaPoolInfo
                    {
                        PoolId = value.PoolId,
                        PoolType = value.PoolType,
                        PoolName = value.PoolName,
                        Category = value.Category,
                        CategoryId = value.CategoryId ?? bundled.CategoryId,
                        UpIds = value.UpIds,
                        PityCarryGroup = bundled.PityCarryGroup
                    };
                }
                _poolInfos[item.Key] = value;
            }
            List<GachaRecord> cached = await _store.LoadAsync();
            _characters = cached.Where(x => x.CharName is not null).ToList();
            _weapons = cached.Where(x => x.WeaponName is not null).ToList();
            foreach (GachaRecord record in _characters.Concat(_weapons))
                if (record.PoolId is not null && _poolInfos.TryGetValue(record.PoolId, out GachaPoolInfo? info))
                    record.PoolCategory = info.Category;
            ShowCurrent();
        };
    }

    private async void SyncButton_Click(object sender, RoutedEventArgs e)
    {
        SyncButton.IsEnabled = false;
        StatusText.Text = "正在连接游戏...";
        try
        {
            GachaSession? session = await _sessions.TryGetSessionAsync();
            if (session is null) { StatusText.Text = "未连接：请启动游戏并完成登录"; return; }
            StatusText.Text = "正在同步寻访记录...";
            _characters = await _api.FetchCharactersAsync(session, _characters);
            _weapons = await _api.FetchWeaponsAsync(session, _weapons);
            _poolInfos = await _api.FetchPoolInfosAsync(session, _characters.Concat(_weapons), _poolInfos);
            await _store.SavePoolInfosAsync(_poolInfos.Values);
            foreach (GachaRecord record in _characters.Concat(_weapons))
                if (record.PoolId is not null && _poolInfos.TryGetValue(record.PoolId, out GachaPoolInfo? info))
                {
                    record.PoolName ??= info.PoolName;
                    record.PoolCategory = info.Category;
                }
            await _store.SaveAsync(_characters.Concat(_weapons));
            StatusText.Text = string.IsNullOrWhiteSpace(session.Uid) ? "同步完成" : $"账号 {session.Uid} · 同步完成";
            ShowCurrent();
        }
        catch (Exception ex) when (ex is HttpRequestException or IOException or InvalidOperationException)
        {
            StatusText.Text = ex.Message.Contains("token", StringComparison.OrdinalIgnoreCase) ? "同步失败：会话无效" : $"同步失败：{ex.Message}";
        }
        finally { SyncButton.IsEnabled = true; }
    }

    private void CategoryButton_Click(object sender, RoutedEventArgs e)
    {
        _selectedCategory = (sender as Button)?.Tag as string ?? "限定";
        foreach (Button button in new[] { LimitedButton, WeaponButton, FestivalButton, StandardButton, BeginnerButton, OtherButton })
            button.BorderBrush = string.Equals(button.Tag as string, _selectedCategory, StringComparison.Ordinal)
                ? new SolidColorBrush(Windows.UI.Color.FromArgb(255, 30, 30, 30))
                : new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
        ShowCurrent();
    }

    private async void ExportButton_Click(object sender, RoutedEventArgs e)
    {
        ExportButton.IsEnabled = false;
        try
        {
            string path = await _store.ExportAsync(_characters.Concat(_weapons), _poolInfos.Values);
            StatusText.Text = $"已导出：{path}";
        }
        catch (IOException ex) { StatusText.Text = $"导出失败：{ex.Message}"; }
        finally { ExportButton.IsEnabled = true; }
    }

    private void OpenWebButton_Click(object sender, RoutedEventArgs e)
    {
        OpenWebButton.IsEnabled = false;
        try
        {
            GachaWebSnapshot snapshot = GachaWebSnapshotBuilder.Build(_characters.Concat(_weapons), _poolInfos);
            string url = GachaSnapshotLink.BuildUrl(GachaWebUrl, snapshot);
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            StatusText.Text = $"已打开网页 · {snapshot.Pools.Count} 个卡池";
        }
        catch (Exception ex) when (ex is InvalidDataException or NotSupportedException or InvalidOperationException or Win32Exception)
        {
            StatusText.Text = $"打开网页失败：{ex.Message}";
        }
        finally { OpenWebButton.IsEnabled = true; }
    }

    private async void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        ImportButton.IsEnabled = false;
        try
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".json");
            if (App.MainWindowInstance is null) return;
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindowInstance));
            Windows.Storage.StorageFile? file = await picker.PickSingleFileAsync();
            if (file is null) return;
            GachaRecordStore.ImportData imported = await _store.ImportAsync(file.Path);
            foreach (GachaRecord item in imported.Records.Where(x => !string.IsNullOrWhiteSpace(x.SeqId)))
                if (!_characters.Concat(_weapons).Any(x => x.SeqId == item.SeqId))
                    (item.WeaponName is null ? _characters : _weapons).Add(item);
            foreach (GachaPoolInfo info in imported.Pools.Where(x => !string.IsNullOrWhiteSpace(x.PoolId))) _poolInfos[info.PoolId] = info;
            await _store.SaveAsync(_characters.Concat(_weapons));
            await _store.SavePoolInfosAsync(_poolInfos.Values);
            ShowCurrent();
            StatusText.Text = $"已导入 {imported.Records.Count} 条记录";
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
        {
            StatusText.Text = $"导入失败：{ex.Message}";
        }
        finally { ImportButton.IsEnabled = true; }
    }

    private void ShowCurrent()
    {
        string category = _selectedCategory;
        IEnumerable<GachaRecord> source = _characters.Concat(_weapons)
            .Where(x => string.Equals(x.PoolCategory, category, StringComparison.Ordinal));
        BuildSummary(source);
        Pools.Clear();
        IReadOnlyList<GachaPoolStatistics> poolStats = GachaStatisticsCalculator.GroupByPool(source, _poolInfos);
        for (int index = 0; index < poolStats.Count; index++)
        {
            GachaPoolStatistics stats = poolStats[index];
            bool isLatestPool = index == 0;
            int currentCarry = stats.SixStars.Count == 0 ? stats.StartingPity : 0;
            var pool = new GachaPoolView { PoolName = stats.PoolName, TotalPulls = stats.PaidPulls, FreePulls = stats.FreePulls, CurrentPity = stats.CurrentPity, ShowCurrentPity = isLatestPool && stats.CurrentPity > 0, CurrentPityRow = new GachaStarView { Name = "", Pity = stats.CurrentPity, CarryPity = currentCarry, IsCurrentPity = true } };
            foreach (GachaSixStarResult star in stats.SixStars)
                pool.SixStars.Add(CreateStarView(star));
            foreach (GachaSixStarResult star in stats.FreeSixStars)
                pool.FreeSixStars.Add(CreateStarView(star));
            if (stats.FreePulls > 0 && pool.FreeSixStars.Count == 0)
                pool.FreeSixStars.Add(new GachaStarView { Name = "", Pity = stats.FreePulls, IsFree = true });
            Pools.Add(pool);
        }
        Records.Clear();
        foreach (GachaRecord item in source) Records.Add(item);
    }

    private static GachaStarView CreateStarView(GachaSixStarResult star) => new()
    {
        Name = star.Name,
        ItemId = star.ItemId,
        IconSource = GachaIconService.Load(star.ItemId),
        Pity = star.Pity,
        CarryPity = star.CarryPity,
        IsFree = star.IsFree,
        IsUp = star.IsUp
    };

    private void BuildSummary(IEnumerable<GachaRecord> source)
    {
        Summaries.Clear();
        foreach (IGrouping<string?, GachaRecord> group in _characters.Concat(_weapons).GroupBy(x => x.PoolCategory))
        {
            var ids = group.SelectMany(x => x.PoolId is not null && _poolInfos.TryGetValue(x.PoolId, out GachaPoolInfo? info) ? info.UpIds : []).ToHashSet(StringComparer.Ordinal);
            GachaPoolStatistics stats = GachaStatisticsCalculator.Calculate(group, ids);
            Summaries.Add(new GachaSummaryView { Title = group.Key ?? "其他", TotalPulls = stats.PaidPulls, PaidPulls = stats.PaidPulls, SixStars = stats.SixStarCount, UpCount = stats.UpCount, OffRateCount = stats.OffRateCount });
        }
    }
}
