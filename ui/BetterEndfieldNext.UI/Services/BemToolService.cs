using System.Diagnostics;
using System.Text;

namespace BetterEndfieldNext.UI.Services;

internal static class BemToolService
{
    public static string ResolveExecutable(string installRoot)
    {
        string executableRoot = Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory;
        foreach (string root in new[] { installRoot, executableRoot }.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            foreach (string relative in new[]
            {
                Path.Combine("tools", "BemConverter", "BetterEndfieldNext.BemConverter.exe"),
                "BetterEndfieldNext.BemConverter.exe"
            })
            {
                string tool = Path.Combine(root, relative);
                if (File.Exists(tool)) return Path.GetFullPath(tool);
            }
        }
        throw new FileNotFoundException(BemText.Get("缺少 BEM 转换工具。请使用完整的 BEM Tools 工具包，或包含 tools/BemConverter 的 Better Endfield Next 构建。"));
    }

    public static async Task<string> RunAsync(string installRoot, IEnumerable<string> args, CancellationToken token = default)
    {
        string tool = ResolveExecutable(installRoot);
        var info = new ProcessStartInfo(tool)
        {
            WorkingDirectory = Path.GetDirectoryName(tool)!,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
        foreach (string arg in args) info.ArgumentList.Add(arg);
        using var process = Process.Start(info) ?? throw new InvalidOperationException(BemText.Get("无法启动转换工具。"));
        using var cancel = token.Register(() => { try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } });
        Task<string> stdout = process.StandardOutput.ReadToEndAsync(token), stderr = process.StandardError.ReadToEndAsync(token);
        await process.WaitForExitAsync(token);
        string report = await stdout, errors = await stderr;
        if (process.ExitCode != 0) throw new InvalidDataException(string.IsNullOrWhiteSpace(report) ? errors : report);
        return report;
    }
}
