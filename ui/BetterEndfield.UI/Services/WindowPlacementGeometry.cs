namespace BetterEndfield.UI.Services;

internal readonly record struct WindowPlacementBounds(int X, int Y, int Width, int Height);

internal static class WindowPlacementGeometry
{
    public static WindowPlacementBounds Calculate(
        int desiredWidthDip, int desiredHeightDip, uint dpi,
        int workX, int workY, int workWidth, int workHeight)
    {
        double scale = (dpi == 0 ? 96 : dpi) / 96.0;
        int availableWidth = Math.Max(1, workWidth);
        int availableHeight = Math.Max(1, workHeight);
        int inset = (int)Math.Round(16 * scale);
        int horizontalInset = Math.Min(inset, Math.Max(0, (availableWidth - 1) / 2));
        int verticalInset = Math.Min(inset, Math.Max(0, (availableHeight - 1) / 2));
        int width = (int)Math.Clamp(Math.Round(desiredWidthDip * scale), 1, availableWidth - horizontalInset * 2);
        int height = (int)Math.Clamp(Math.Round(desiredHeightDip * scale), 1, availableHeight - verticalInset * 2);
        return new WindowPlacementBounds(
            workX + (availableWidth - width) / 2,
            workY + (availableHeight - height) / 2,
            width, height);
    }
}
