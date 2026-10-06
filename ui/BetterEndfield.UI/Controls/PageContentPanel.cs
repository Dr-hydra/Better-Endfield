using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BetterEndfield.UI.Controls;

// The host fills the scroll viewport; its content column stays on the left.
// Capping a stretched StackPanel itself can offset it using its desired width
// while arranging its children at the larger available width.
public sealed class PageContentPanel : Grid
{
    public double MaxContentWidth
    {
        get => ColumnDefinitions[0].MaxWidth;
        set => ColumnDefinitions[0].MaxWidth = value;
    }

    public PageContentPanel()
    {
        ColumnDefinitions.Add(new ColumnDefinition
        {
            Width = new GridLength(1, GridUnitType.Star),
            MaxWidth = 920
        });
    }
}
