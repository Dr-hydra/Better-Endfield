using System.Diagnostics;
using BetterEndfieldNext.UI.Models;
using Microsoft.UI.Xaml;

namespace BetterEndfieldNext.UI.Services;

internal sealed class MediaCaptureService : IDisposable
{
    private const string Instance = @"Local\BetterEndfieldNext.MediaCapture.V3.Instance";
    private readonly DispatcherTimer timer = new() { Interval = TimeSpan.FromSeconds(3) };
    private Process? process;
    private bool enabled;
    internal MediaCaptureService() => timer.Tick += (_, _) => EnsureRunning();
    internal void Configure(MediaScreenConfiguration configuration)
    {
        enabled = configuration.Enabled && configuration.Source == "window";
        if (enabled) { EnsureRunning(); timer.Start(); } else timer.Stop();
    }
    private void EnsureRunning()
    {
        if (!enabled) return;
        try {
            if (process is { HasExited: false }) return;
            process?.Dispose(); process = null;
            try { using var instance = Mutex.OpenExisting(Instance); return; }
            catch (WaitHandleCannotBeOpenedException) { }
            string executable = Path.Combine(AppContext.BaseDirectory, "runtime", "BetterEndfieldNext.MediaCapture.exe");
            if (!File.Exists(executable)) return;
            var start = new ProcessStartInfo(executable) { UseShellExecute = false, CreateNoWindow = true, WorkingDirectory = Path.GetDirectoryName(executable)! };
            start.ArgumentList.Add("--owner"); start.ArgumentList.Add(Environment.ProcessId.ToString(System.Globalization.CultureInfo.InvariantCulture));
            process = Process.Start(start);
        } catch (Exception error) { Debug.WriteLine($"Media capture helper: {error.Message}"); }
    }
    public void Dispose() { timer.Stop(); process?.Dispose(); process = null; }
}
