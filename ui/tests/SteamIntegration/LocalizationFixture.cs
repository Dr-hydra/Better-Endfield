namespace BetterEndfieldNext.UI.Services;

internal sealed class LocalizationService
{
    public static LocalizationService Instance { get; } = new();
    public bool IsChinese => false;
}
