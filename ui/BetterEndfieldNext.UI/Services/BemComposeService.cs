using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;

namespace BetterEndfieldNext.UI.Services;

internal sealed class BemComposeService : IDisposable
{
    private readonly string _installRoot;
    private readonly string _scratch = Path.Combine(Path.GetTempPath(), "BetterEndfieldNext", "compose-" + Guid.NewGuid().ToString("N"));

    public BemComposeService(string installRoot) => _installRoot = installRoot;

    private string Executable()
    {
        foreach (string root in new[] { _installRoot, AppContext.BaseDirectory, Path.GetDirectoryName(Environment.ProcessPath) ?? "" }.Distinct())
            foreach (string relative in new[] { Path.Combine("tools", "BemCompose", "BetterEndfieldNext.BemCompose.exe"), "BetterEndfieldNext.BemCompose.exe" })
            {
                string file = Path.Combine(root, relative);
                if (File.Exists(file)) return Path.GetFullPath(file);
            }
        throw new FileNotFoundException(BemText.Get("缺少 BEM 组合工具，请使用包含 BemCompose 的完整 PC 构建。"));
    }

    public async Task<JsonObject> PlanAsync(JsonObject request, CancellationToken token)
    {
        Directory.CreateDirectory(_scratch);
        string file = Path.Combine(_scratch, "request.json");
        await File.WriteAllTextAsync(file, request.ToJsonString(), new UTF8Encoding(false), token);
        return await RunAsync(["plan", "--request", file], token);
    }

    public Task<JsonObject> BuildAsync(JsonObject plan, JsonObject defaults, string output, CancellationToken token)
        => MaterializeAsync("build", plan, defaults, output, token);

    private async Task<JsonObject> MaterializeAsync(string command, JsonObject plan, JsonObject defaults, string output, CancellationToken token)
    {
        Directory.CreateDirectory(_scratch);
        string planFile = Path.Combine(_scratch, "plan.json"), choicesFile = Path.Combine(_scratch, "choices.json");
        await File.WriteAllTextAsync(planFile, plan.ToJsonString(), new UTF8Encoding(false), token);
        await File.WriteAllTextAsync(choicesFile, defaults.ToJsonString(), new UTF8Encoding(false), token);
        return await RunAsync([command, "--plan", planFile, "--choices", choicesFile, "--output-dir", output], token);
    }

    public Task<JsonObject> PreviewAsync(JsonObject plan, JsonObject defaults, CancellationToken token)
    {
        Directory.CreateDirectory(_scratch);
        return MaterializeAsync("draft", plan, defaults, Path.Combine(_scratch, "preview-" + Guid.NewGuid().ToString("N")), token);
    }

    internal static string ExplainFailure(string message)
    {
        if (message.Contains("COMPOSE_BODY:", StringComparison.Ordinal))
            return BemText.Format("身体区域数据无效：{0}", message);
        if (message.Contains("COMPOSE_CONDITION_BUDGET", StringComparison.Ordinal))
            return BemText.Get("组合条件过于复杂，未能完成容量证明；这不代表当前选择一定超限。请减少来源选项后重试。");
        var usage = System.Text.RegularExpressions.Regex.Match(message, @"bytes=(\d+)/(\d+);textures=(\d+)/(\d+)");
        if (usage.Success)
            return BemText.Format("同时生效的组合最多需要 {0:F1}/{1:F0} MiB、{2}/{3} 张贴图。请减少同时启用的内容。",
                double.Parse(usage.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture) / 1048576,
                double.Parse(usage.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture) / 1048576,
                usage.Groups[3].Value, usage.Groups[4].Value);
        if (message.Contains("COMPOSE_SELECTION_BUDGET", StringComparison.Ordinal))
            return BemText.Get("当前组合超出内存、贴图或绘制数量预算。请停用部分来源的绘制段后重试；多个完整来源不能无限叠加。");
        if (message.Contains("COMPOSE_SEGMENT_LAYOUT", StringComparison.Ordinal))
            return BemText.Get("这些绘制段的顶点布局不兼容，不能合并。请先停用冲突来源，再启用新来源。");
        if (message.Contains("COMPOSE_TOO_MANY_GROUPS", StringComparison.Ordinal))
            return BemText.Get("来源选项组超过 64 组上限，请减少来源包。");
        return message;
    }

    private async Task<JsonObject> RunAsync(IEnumerable<string> arguments, CancellationToken token)
    {
        using var deadline = CancellationTokenSource.CreateLinkedTokenSource(token);
        bool preview = arguments.FirstOrDefault() == "draft";
        if (preview) deadline.CancelAfter(TimeSpan.FromSeconds(25));
        var originalToken = token; token = deadline.Token;
        token.ThrowIfCancellationRequested();
        string executable = Executable(), cancelFile = Path.Combine(_scratch, Guid.NewGuid().ToString("N") + ".cancel");
        var start = new ProcessStartInfo(executable)
        {
            UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true,
            StandardOutputEncoding = Encoding.UTF8, StandardErrorEncoding = Encoding.UTF8,
            WorkingDirectory = Path.GetDirectoryName(executable)!
        };
        foreach (string value in arguments) start.ArgumentList.Add(value);
        start.ArgumentList.Add("--cancel-file"); start.ArgumentList.Add(cancelFile);
        using var process = Process.Start(start) ?? throw new IOException(BemText.Get("无法启动组合工具。"));
        using var cancellation = token.Register(() =>
        {
            try { File.WriteAllText(cancelFile, "cancel"); }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        });
        // Drain both streams while waiting. The CLI owns its stage and removes
        // it on cancellation; killing the process would bypass that cleanup.
        Task<string> stdout = process.StandardOutput.ReadToEndAsync(), stderr = process.StandardError.ReadToEndAsync();
        try { await process.WaitForExitAsync().WaitAsync(token); }
        catch (OperationCanceledException)
        {
            // Give cooperative cancellation a short cleanup window, then enforce
            // the deadline even if a decoder has not reached a checkpoint.
            try { await process.WaitForExitAsync().WaitAsync(TimeSpan.FromSeconds(2)); }
            catch (TimeoutException) { try { process.Kill(true); } catch (InvalidOperationException) { } await process.WaitForExitAsync(); }
            await Task.WhenAll(stdout, stderr);
            if (!originalToken.IsCancellationRequested) throw new TimeoutException(BemText.Get("预览准备超时，请减少启用的绘制段后重试。"));
            throw new OperationCanceledException(originalToken);
        }
        string output = await stdout, errors = await stderr;
        if (preview && token.IsCancellationRequested && !originalToken.IsCancellationRequested)
            throw new TimeoutException(BemText.Get("预览准备超时，请减少启用的绘制段后重试。"));
        JsonObject envelope = JsonNode.Parse(output) as JsonObject ?? throw new InvalidDataException(errors);
        if (process.ExitCode != 0 || envelope["success"]?.GetValue<bool>() != true)
        {
            string message = envelope["error"]?.GetValue<string>() ?? errors;
            if (message.Contains("COMPOSE_CANCELLED", StringComparison.Ordinal)) throw new OperationCanceledException(token);
            throw new InvalidDataException(message);
        }
        return envelope["result"]?.DeepClone() as JsonObject ?? throw new InvalidDataException(BemText.Get("组合工具返回了无效结果。"));
    }

    public void Dispose()
    {
        try { if (Directory.Exists(_scratch)) Directory.Delete(_scratch, true); }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }
}
