using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace BetterEndfield.UI.Controls;

public sealed class WrapPanel : Panel
{
    public double HorizontalSpacing
    {
        get => (double)GetValue(HorizontalSpacingProperty);
        set => SetValue(HorizontalSpacingProperty, value);
    }

    public static readonly DependencyProperty HorizontalSpacingProperty =
        DependencyProperty.Register(
            nameof(HorizontalSpacing),
            typeof(double),
            typeof(WrapPanel),
            new PropertyMetadata(0.0, OnSpacingChanged));

    public double VerticalSpacing
    {
        get => (double)GetValue(VerticalSpacingProperty);
        set => SetValue(VerticalSpacingProperty, value);
    }

    public static readonly DependencyProperty VerticalSpacingProperty =
        DependencyProperty.Register(
            nameof(VerticalSpacing),
            typeof(double),
            typeof(WrapPanel),
            new PropertyMetadata(0.0, OnSpacingChanged));

    private static void OnSpacingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is WrapPanel panel)
        {
            panel.InvalidateMeasure();
            panel.InvalidateArrange();
        }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        double curLineX = 0;
        double curLineY = 0;
        double maxLineHeight = 0;
        double maxWidth = 0;

        double hSpacing = HorizontalSpacing;
        double vSpacing = VerticalSpacing;

        foreach (UIElement child in Children)
        {
            child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Size sz = child.DesiredSize;

            if (curLineX + sz.Width > availableSize.Width && curLineX > 0)
            {
                maxWidth = Math.Max(maxWidth, curLineX - hSpacing);
                curLineY += maxLineHeight + vSpacing;
                curLineX = 0;
                maxLineHeight = 0;
            }

            curLineX += sz.Width + hSpacing;
            maxLineHeight = Math.Max(maxLineHeight, sz.Height);
        }

        maxWidth = Math.Max(maxWidth, curLineX > 0 ? curLineX - hSpacing : 0);
        double totalHeight = curLineY + maxLineHeight;

        double finalWidth = double.IsInfinity(availableSize.Width) ? maxWidth : Math.Min(availableSize.Width, maxWidth);
        return new Size(finalWidth, totalHeight);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        double curLineX = 0;
        double curLineY = 0;
        double maxLineHeight = 0;

        double hSpacing = HorizontalSpacing;
        double vSpacing = VerticalSpacing;

        var lineChildren = new List<(UIElement element, Rect rect)>();

        foreach (UIElement child in Children)
        {
            Size sz = child.DesiredSize;

            if (curLineX + sz.Width > finalSize.Width && curLineX > 0)
            {
                foreach (var item in lineChildren)
                {
                    item.element.Arrange(item.rect);
                }
                lineChildren.Clear();

                curLineY += maxLineHeight + vSpacing;
                curLineX = 0;
                maxLineHeight = 0;
            }

            lineChildren.Add((child, new Rect(curLineX, curLineY, sz.Width, sz.Height)));
            curLineX += sz.Width + hSpacing;
            maxLineHeight = Math.Max(maxLineHeight, sz.Height);
        }

        foreach (var item in lineChildren)
        {
            item.element.Arrange(item.rect);
        }

        return finalSize;
    }
}
