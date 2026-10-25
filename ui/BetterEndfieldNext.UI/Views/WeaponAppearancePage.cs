using System;
using System.Collections.Generic;
using BetterEndfieldNext.UI.Models;
using BetterEndfieldNext.UI.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BetterEndfieldNext.UI.Views;

public sealed class WeaponAppearancePage : UserControl
{
    internal event EventHandler? ConfigurationChanged;

    private readonly ToggleSwitch enabledToggle = new() { Name = "AppearanceEnabledToggle" };

    // Weapon
    private readonly ToggleSwitch replaceWeaponToggle = new() { Name = "ReplaceWeaponToggle", IsOn = true };
    private readonly ComboBox weaponModeCombo = new() { Name = "WeaponModeCombo", MinWidth = 240 };

    // Character
    private readonly ToggleSwitch replaceCharacterToggle = new() { Name = "ReplaceCharacterToggle", IsOn = true };
    private readonly ComboBox characterModeCombo = new() { Name = "CharacterModeCombo", MinWidth = 240 };

    // Effects
    private readonly ToggleSwitch replaceEffectsToggle = new() { Name = "ReplaceEffectsToggle", IsOn = true };

    // Headers & Labels
    private readonly TextBlock title = new() { FontSize = 28, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold };
    private readonly TextBlock weaponSectionTitle = new() { FontSize = 16, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold };
    private readonly TextBlock characterSectionTitle = new() { FontSize = 16, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold };
    private readonly TextBlock effectsSectionTitle = new() { FontSize = 16, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold };

    private bool _suppressEvents = false;

    private static string T(string zh, string en) =>
        LocalizationService.Instance.IsChinese ? zh : en;

    public WeaponAppearancePage()
    {
        var body = new StackPanel { Spacing = 18, Padding = new Thickness(24) };

        // Title
        body.Children.Add(title);

        // Global master toggle card
        var masterCard = new Border
        {
            Padding = new Thickness(16),
            CornerRadius = new CornerRadius(8),
            Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"]
        };
        masterCard.Child = enabledToggle;
        body.Children.Add(masterCard);

        // Section 1: Weapon Appearance
        var weaponCard = new Border
        {
            Padding = new Thickness(16),
            CornerRadius = new CornerRadius(8),
            Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"]
        };
        var weaponStack = new StackPanel { Spacing = 10 };
        weaponStack.Children.Add(weaponSectionTitle);
        weaponStack.Children.Add(replaceWeaponToggle);

        weaponModeCombo.Items.Add(new ComboBoxItem { Tag = "ink" });
        weaponModeCombo.Items.Add(new ComboBoxItem { Tag = "default" });
        weaponModeCombo.SelectedIndex = 0;
        weaponStack.Children.Add(weaponModeCombo);
        weaponCard.Child = weaponStack;
        body.Children.Add(weaponCard);

        // Section 2: Character Appearance
        var charCard = new Border
        {
            Padding = new Thickness(16),
            CornerRadius = new CornerRadius(8),
            Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"]
        };
        var charStack = new StackPanel { Spacing = 10 };
        charStack.Children.Add(characterSectionTitle);
        charStack.Children.Add(replaceCharacterToggle);

        characterModeCombo.Items.Add(new ComboBoxItem { Tag = "ink" });
        characterModeCombo.Items.Add(new ComboBoxItem { Tag = "unmasked" });
        characterModeCombo.Items.Add(new ComboBoxItem { Tag = "default" });
        characterModeCombo.SelectedIndex = 0;
        charStack.Children.Add(characterModeCombo);
        charCard.Child = charStack;
        body.Children.Add(charCard);

        // Section 3: Effects
        var effectCard = new Border
        {
            Padding = new Thickness(16),
            CornerRadius = new CornerRadius(8),
            Background = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"]
        };
        var effectStack = new StackPanel { Spacing = 10 };
        effectStack.Children.Add(effectsSectionTitle);
        effectStack.Children.Add(replaceEffectsToggle);
        effectCard.Child = effectStack;
        body.Children.Add(effectCard);

        Content = new ScrollViewer { Content = body, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };

        UpdateLanguage();
        UpdateAvailability();

        // Event hooks
        enabledToggle.Toggled += (_, _) => OnChanged();
        replaceWeaponToggle.Toggled += (_, _) => OnChanged();
        weaponModeCombo.SelectionChanged += (_, _) => OnChanged();
        replaceCharacterToggle.Toggled += (_, _) => OnChanged();
        characterModeCombo.SelectionChanged += (_, _) => OnChanged();
        replaceEffectsToggle.Toggled += (_, _) => OnChanged();
    }

    private void OnChanged()
    {
        if (_suppressEvents) return;
        UpdateAvailability();
        ConfigurationChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateAvailability()
    {
        bool master = enabledToggle.IsOn;
        replaceWeaponToggle.IsEnabled = master;
        weaponModeCombo.IsEnabled = master && replaceWeaponToggle.IsOn;
        replaceCharacterToggle.IsEnabled = master;
        characterModeCombo.IsEnabled = master && replaceCharacterToggle.IsOn;
        replaceEffectsToggle.IsEnabled = master;
    }

    internal void UpdateLanguage()
    {
        title.Text = T("管理员外观", "Administrator Appearance");

        enabledToggle.Header = T("启用外观覆写", "Enable Appearance Override");

        weaponSectionTitle.Text = T("武器外观", "Weapon Appearance");
        replaceWeaponToggle.Header = T("替换宏愿外观", "Override Hongyuan Appearance");
        weaponModeCombo.Header = T("样式选择", "Style");
        if (weaponModeCombo.Items.Count >= 2)
        {
            ((ComboBoxItem)weaponModeCombo.Items[0]).Content = T("宏愿 · 剧情水墨流光", "Hongyuan · Story Ink & Glow");
            ((ComboBoxItem)weaponModeCombo.Items[1]).Content = T("原版默认外观", "Vanilla Default");
        }

        characterSectionTitle.Text = T("管理员角色外观", "Administrator Character Appearance");
        replaceCharacterToggle.Header = T("替换角色外观", "Override Character Appearance");
        characterModeCombo.Header = T("形态选择", "Form");
        if (characterModeCombo.Items.Count >= 3)
        {
            ((ComboBoxItem)characterModeCombo.Items[0]).Content = T("剧情水墨专属服装", "Story Ink Attire");
            ((ComboBoxItem)characterModeCombo.Items[1]).Content = T("剧情摘面具形态", "Story Unmasked Form");
            ((ComboBoxItem)characterModeCombo.Items[2]).Content = T("原版默认形态", "Vanilla Default");
        }

        effectsSectionTitle.Text = T("战斗与剑光特效", "Combat & Blade VFX");
        replaceEffectsToggle.Header = T("替换为水墨剑气与打击特效", "Enable Ink Blade Slashing & Hit VFX");
    }

    internal WeaponAppearanceConfiguration ReadConfiguration()
    {
        return new WeaponAppearanceConfiguration
        {
            Enabled = enabledToggle.IsOn,
            ReplaceWeapon = replaceWeaponToggle.IsOn,
            WeaponMode = ((weaponModeCombo.SelectedItem as ComboBoxItem)?.Tag as string) ?? "ink",
            ReplaceCharacter = replaceCharacterToggle.IsOn,
            CharacterMode = ((characterModeCombo.SelectedItem as ComboBoxItem)?.Tag as string) ?? "ink",
            ReplaceEffects = replaceEffectsToggle.IsOn,
            EffectsMode = replaceEffectsToggle.IsOn ? "ink" : "default"
        };
    }

    internal void ApplyConfiguration(WeaponAppearanceConfiguration config)
    {
        _suppressEvents = true;
        try
        {
            enabledToggle.IsOn = config.Enabled;
            replaceWeaponToggle.IsOn = config.ReplaceWeapon;

            for (int i = 0; i < weaponModeCombo.Items.Count; i++)
            {
                if (string.Equals(((ComboBoxItem)weaponModeCombo.Items[i]).Tag as string, config.WeaponMode, StringComparison.OrdinalIgnoreCase))
                {
                    weaponModeCombo.SelectedIndex = i;
                    break;
                }
            }

            replaceCharacterToggle.IsOn = config.ReplaceCharacter;
            for (int i = 0; i < characterModeCombo.Items.Count; i++)
            {
                if (string.Equals(((ComboBoxItem)characterModeCombo.Items[i]).Tag as string, config.CharacterMode, StringComparison.OrdinalIgnoreCase))
                {
                    characterModeCombo.SelectedIndex = i;
                    break;
                }
            }

            replaceEffectsToggle.IsOn = config.ReplaceEffects && !string.Equals(config.EffectsMode, "default", StringComparison.OrdinalIgnoreCase);
        }
        finally
        {
            _suppressEvents = false;
        }
        UpdateAvailability();
    }
}
