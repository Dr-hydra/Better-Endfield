using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

namespace BetterEndfieldNext.UI.Controls;

internal static class BemOptionHeader
{
    public static FrameworkElement Create(string name, IReadOnlyList<BemComponentBinding> components)
    {
        var header = new Grid { ColumnSpacing = 8, MaxWidth = 560, HorizontalAlignment = HorizontalAlignment.Stretch };
        header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        var title = new TextBlock { Text = name, MaxWidth = 360, TextTrimming = TextTrimming.CharacterEllipsis, VerticalAlignment = VerticalAlignment.Center };
        ToolTipService.SetToolTip(title, name); header.Children.Add(title);
        var list = new StackPanel { Spacing = 12, MaxWidth = 480 };
        list.Children.Add(new TextBlock { Text = name, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, TextWrapping = TextWrapping.Wrap });
        list.Children.Add(new TextBlock
        {
            Text = BemText.Get("以下为可能关联的部件；实际生效取决于当前来源与其他选项。"),
            TextWrapping = TextWrapping.Wrap, FontSize = 12, Opacity = 0.7
        });
        if (components.Count == 0) list.Children.Add(new TextBlock { Text = BemText.Get("没有直接关联的部件，可能仅用于组合约束。"), TextWrapping = TextWrapping.Wrap });
        foreach (var component in components)
        {
            var row = new StackPanel { Spacing = 3 };
            row.Children.Add(new TextBlock { Text = component.Name, TextWrapping = TextWrapping.Wrap, IsTextSelectionEnabled = true });
            string detail = string.Join("\n", new[] { component.Resource, component.RendererPath }.Where(s => s.Length > 0));
            if (detail.Length > 0) row.Children.Add(new TextBlock { Text = detail, FontSize = 12, Opacity = 0.7, TextWrapping = TextWrapping.Wrap, IsTextSelectionEnabled = true });
            list.Children.Add(row);
        }
        if (components.Count > 0)
        {
            var copy = new Button { Content = BemText.Get("复制部件清单") };
            copy.Click += (_, _) =>
            {
                var data = new DataPackage(); data.SetText(string.Join("\n\n", components.Select(c => c.FullName)));
                try { Clipboard.SetContent(data); copy.Content = BemText.Get("已复制"); }
                catch { copy.Content = BemText.Get("复制失败，可选中文字复制"); }
            };
            list.Children.Add(copy);
        }
        var button = new Button
        {
            Content = BemText.Format("关联 {0} 个部件", components.Count), FontSize = 12, Padding = new Thickness(7, 2, 7, 2),
            VerticalAlignment = VerticalAlignment.Center,
            Flyout = new Flyout { Content = new ScrollViewer { Content = list, MaxHeight = 380, VerticalScrollBarVisibility = ScrollBarVisibility.Auto } }
        };
        ToolTipService.SetToolTip(button, BemText.Get("查看关联部件的完整名称与资源路径"));
        Grid.SetColumn(button, 1); header.Children.Add(button);
        return header;
    }
}
