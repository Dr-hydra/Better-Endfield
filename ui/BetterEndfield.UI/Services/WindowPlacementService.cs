using System.Runtime.InteropServices;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;

namespace BetterEndfield.UI.Services;

internal static class WindowPlacementService
{
    public static void SetInitialSize(Window window, int widthDip, int heightDip, Window? owner = null)
    {
        try
        {
            Window displayWindow = owner ?? window;
            DisplayArea? display = DisplayArea.GetFromWindowId(displayWindow.AppWindow.Id, DisplayAreaFallback.Nearest);
            if (display is null) return;
            RectInt32 area = display.WorkArea;
            // Move an owned window onto the intended monitor before reading its DPI.
            if (owner is not null) window.AppWindow.Move(new PointInt32(area.X, area.Y));
            uint dpi = GetDpiForWindow(WindowNative.GetWindowHandle(window));
            WindowPlacementBounds bounds = WindowPlacementGeometry.Calculate(
                widthDip, heightDip, dpi, area.X, area.Y, area.Width, area.Height);
            window.AppWindow.MoveAndResize(new RectInt32(bounds.X, bounds.Y, bounds.Width, bounds.Height));
        }
        catch (InvalidOperationException)
        {
            // Window creation must remain usable if the display is unavailable.
        }
    }

    [DllImport("user32.dll")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern uint GetDpiForWindow(nint hwnd);
}
