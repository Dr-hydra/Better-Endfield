using System.Text.Json;

namespace BetterEndfield.UI.Services;

internal sealed record BemInspectionSummary(string Title, string Detail, string NextStep, bool CanProvideRecipe, bool AlreadyPackaged)
{
    public bool CanAutoConvert { get; init; }
    public static BemInspectionSummary Read(string report)
    {
        using var document = JsonDocument.Parse(report);
        var root = document.RootElement;
        string format = root.TryGetProperty("format", out var f) ? f.GetString() ?? "unknown" : "unknown";
        if (root.TryGetProperty("conversion_ready", out var ready) && ready.GetBoolean() && root.TryGetProperty("matching", out var matched))
        {
            string warnings = string.Join("\n", matched.GetProperty("warnings").EnumerateArray().Select(w => w.GetProperty("message").GetString()));
            return new("已匹配角色，可以转换", $"{matched.GetProperty("character_name").GetString()} · ComponentN · 源包默认外观\n骨骼、顶点流、材质和绘制范围已校验。" +
                (warnings.Length > 0 ? "\n" + warnings : ""), "点击“转换为 BEM”，完成后选择保存位置。游戏内切换键不随包转换。", false, false) { CanAutoConvert = true };
        }
        if (format == "BEMv1")
            return new("这是可直接导入的 BEM 包", "无需再做格式转换。", "回到“角色外观”，点击“导入 BEM / ZIP”安装此文件。", false, true);
        if (format == "BEM-ZIP")
        {
            int count = root.GetProperty("packages").GetArrayLength();
            int issues = root.GetProperty("issues").GetArrayLength();
            return new(count > 0 ? "这是 BEM 分发合集" : "合集中没有通过校验的 BEM",
                $"找到 {count} 个有效包，{issues} 项问题。",
                count > 0 ? "无需转换。回到“角色外观”导入该 ZIP，即可勾选需要的包。" : "请展开检查详情，确认包是否损坏或存在重复 ID。", false, true);
        }
        if (root.TryGetProperty("automation", out var automation) && automation.GetProperty("status").GetString() == "manual_only")
            return new("此包不能自动转换", string.Join("\n", automation.GetProperty("reasons").EnumerateArray().Select(r => r.GetString())),
                "这类源包需要开发者专门审阅与适配。补齐角色资料也不能自动解决其自定义逻辑；保存报告后交给维护者。", true, false);
        if (format is not ("component-n" or "hash-lod"))
        {
            int count = root.TryGetProperty("entries", out var entries) ? entries.GetArrayLength() : 0;
            return new(count > 1 ? "发现多个 Mod 入口，需要指定要转换的内容" : "未识别到可转换的 Mod 入口",
                count > 1 ? $"共 {count} 个入口。不能自动选择其中一个。" : "请确认选择的是包含 INI 和模型资源的完整 Mod 目录、ZIP、RAR 或 7z。",
                count > 1 ? "源 Mod 转换规则需要明确入口；可保存报告交给作者或维护者。" : "换一个源文件重试，或保存报告交给维护者。", count > 1, false);
        }
        var analysis = root.GetProperty("analysis");
        int components = analysis.TryGetProperty("components", out var cs) ? cs.GetArrayLength() : 0;
        int textures = analysis.TryGetProperty("textures", out var ts) && ts.ValueKind == JsonValueKind.Number ? ts.GetInt32() : 0;
        var errors = analysis.TryGetProperty("errors", out var es) ? es.EnumerateArray().Select(x => x.GetString() ?? "").Where(x => x.Length > 0).ToList() : [];
        string kind = format == "component-n" ? "ComponentN" : "Hash / LOD";
        if (root.TryGetProperty("automation", out automation) && automation.GetProperty("status").GetString() == "requires_mapping")
            return new("格式可解析，暂不能承诺自动转换",
                $"{kind} · {components} 个部件 · {textures} 条纹理记录\n" +
                string.Join("\n", automation.GetProperty("reasons").EnumerateArray().Select(r => r.GetString())),
                "标准骨架与绘制声明可以通用处理；材质槽和骨骼来源需要明确映射。已知样本的专门转换成功不代表同类所有包可直接自动转换。", true, false);
        string blockers = root.TryGetProperty("issues", out var problems) ? string.Join("\n", problems.EnumerateArray().Select(p => p.GetProperty("message").GetString())) : "";
        return new(errors.Count == 0 ? "源包已识别，尚不能自动转换" : "源包已识别，有待处理的问题",
            $"{kind} · {components} 个部件 · {textures} 条纹理记录" +
            (errors.Count == 0 ? "\n源包结构已读取；自动转换未通过。" : $"\n发现 {errors.Count} 项待处理问题：\n" + string.Join("\n", errors.Take(3))),
            blockers + "\n角色资料由工具自动匹配，无需手选。已采集原生资料的角色仍需有 EFMI 资源身份对应；采集完成不代表所有源包均可转换。具体缺项见上方，可保存报告交给维护者。",
            true, false);
    }
}

internal static class BemReportPresentation
{
    public static string Failure(string raw)
    {
        try
        {
            using var document = JsonDocument.Parse(raw);
            if (document.RootElement.TryGetProperty("issues", out var issues))
                return string.Join("\n", issues.EnumerateArray().Select(i => i.TryGetProperty("message", out var m) ? m.GetString() : i.ToString()));
        }
        catch (JsonException) { }
        return raw;
    }

    public static string Package(string raw)
    {
        using var document = JsonDocument.Parse(raw);
        var root = document.RootElement;
        var lines = new List<string>();
        if (root.TryGetProperty("package", out var p))
        {
            lines.Add(p.GetProperty("name").GetString() ?? "BEM");
            lines.Add("外观：" + string.Join("、", p.GetProperty("appearances").EnumerateArray().Select(a => a.GetProperty("name").GetString())));
        }
        if (root.TryGetProperty("size", out var size)) lines.Add($"大小：{size.GetInt64() / 1_000_000.0:F1} MB");
        if (root.TryGetProperty("packages", out var packages)) lines.Add($"包含 {packages.GetArrayLength()} 个有效包");
        if (root.TryGetProperty("issues", out var issues) && issues.GetArrayLength() > 0)
            lines.Add("部分条目未通过：\n" + string.Join("\n", issues.EnumerateArray().Select(i => i.ToString())));
        return string.Join("\n", lines);
    }
}
