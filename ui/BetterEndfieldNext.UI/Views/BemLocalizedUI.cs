using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace BetterEndfieldNext.UI.Views;

// Relabel existing controls instead of rebuilding editors on a language change.
// Weak ownership lets discarded cards and task steps be collected normally.
internal static class BemLocalizedUI
{
    private static readonly ConditionalWeakTable<DependencyObject,
        Dictionary<DependencyProperty, Func<string>>> Bindings = new();

    public static void Set(DependencyObject target, DependencyProperty property, Func<string> text)
    {
        Bindings.GetOrCreateValue(target)[property] = text;
        target.SetValue(property, text());
    }

    public static void Refresh(DependencyObject root)
        => Refresh(root, new HashSet<DependencyObject>(ReferenceEqualityComparer.Instance));

    private static void Refresh(DependencyObject root, HashSet<DependencyObject> visited)
    {
        if (!visited.Add(root)) return;
        if (Bindings.TryGetValue(root, out var properties))
            foreach (var (property, text) in properties) root.SetValue(property, text());
        // Collapsed expanders and unopened combo-box choices may not have
        // visual children yet, but their app-owned captions still need updating.
        if (root is ContentControl { Content: DependencyObject content }) Refresh(content, visited);
        if (root is Panel panel)
            foreach (var child in panel.Children) Refresh(child, visited);
        if (root is ItemsControl items)
            foreach (var item in items.Items)
                if (item is DependencyObject choice) Refresh(choice, visited);
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); ++i)
            Refresh(VisualTreeHelper.GetChild(root, i), visited);
    }
}
