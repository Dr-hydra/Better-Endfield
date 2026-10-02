using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BetterEndfield.UI.Services;

internal sealed class BemExportProject
{
    [JsonPropertyName("schema")] public int Schema { get; set; } = 1;
    [JsonPropertyName("kind")] public string Kind { get; set; } = "bem-export-task";
    [JsonPropertyName("mode")] public string Mode { get; set; } = "convert";
    [JsonPropertyName("source")] public string Source { get; set; } = "";
    [JsonPropertyName("recipe")] public string Recipe { get; set; } = "";
    [JsonPropertyName("deformations")] public string Deformations { get; set; } = "";
    [JsonPropertyName("output")] public string Output { get; set; } = "dist/appearance.bem";
    [JsonPropertyName("report")] public string Report { get; set; } = "reports/build.json";
    [JsonPropertyName("package")] public Dictionary<string, string> Package { get; set; } = new()
    {
        ["id"] = "creator." + Guid.NewGuid().ToString("N"), ["name"] = "新模型包", ["author"] = "未填写", ["version"] = "1.0.0"
    };

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    public static BemExportProject Load(string file)
    {
        var project = JsonSerializer.Deserialize<BemExportProject>(File.ReadAllText(file, Encoding.UTF8), JsonOptions)
            ?? throw new InvalidDataException("工程文件为空。");
        using var json = JsonDocument.Parse(File.ReadAllText(file, Encoding.UTF8));
        if (new[] { "schema", "kind", "mode", "source", "output", "package" }.Any(key => !json.RootElement.TryGetProperty(key, out _)))
            throw new InvalidDataException("请选择导出任务工程（.bemproj.json）；BEM 的 project.json 应作为打包输入。");
        project.ValidateMetadata();
        return project;
    }

    public static string Resolve(string path, string projectFile) => Path.GetFullPath(path, Path.GetDirectoryName(Path.GetFullPath(projectFile))!);
    public static string PortablePath(string path, string projectFile) => Path.GetRelativePath(Path.GetDirectoryName(Path.GetFullPath(projectFile))!, path).Replace('\\', '/');

    private void ValidateMetadata()
    {
        if (Schema != 1 || Kind != "bem-export-task" || Mode is not ("convert" or "pack"))
            throw new InvalidDataException("不支持的导出工程格式或模式。");
        if (Package == null || !Package.TryGetValue("id", out var id) || string.IsNullOrWhiteSpace(id) || !Regex.IsMatch(id, @"^[A-Za-z0-9][A-Za-z0-9_.-]{0,95}$"))
            throw new InvalidDataException("包 ID 只能含英文字母、数字、点、横线和下划线，长度不超过 96。");
        foreach (string key in new[] { "name", "author", "version" })
            if (!Package.TryGetValue(key, out var value) || string.IsNullOrWhiteSpace(value) || Encoding.UTF8.GetByteCount(value) > 256)
                throw new InvalidDataException("请填写有效的包名称、作者和版本（各不超过 256 字节）。");
    }

    public void Save(string file, string? previousFile)
    {
        ValidateMetadata();
        if (string.IsNullOrWhiteSpace(Source) || string.IsNullOrWhiteSpace(Output)) throw new InvalidDataException("请填写输入和 BEM 输出路径。");
        file = Path.GetFullPath(file);
        string pathBase = previousFile ?? file;
        string source = Resolve(Source, pathBase), output = Resolve(Output, pathBase);
        string recipe = string.IsNullOrWhiteSpace(Recipe) ? "" : Resolve(Recipe, pathBase);
        string deformations = string.IsNullOrWhiteSpace(Deformations) ? "" : Resolve(Deformations, pathBase);
        string report = string.IsNullOrWhiteSpace(Report) ? "" : Resolve(Report, pathBase);
        if (string.IsNullOrWhiteSpace(Source) || !(File.Exists(source) || Directory.Exists(source)))
            throw new InvalidDataException("请选择存在的源 Mod 或 BEM project.json。");
        if (!output.EndsWith(".bem", StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("输出文件必须使用 .bem 扩展名。");
        if (recipe.Length > 0 && (Mode != "convert" || !File.Exists(recipe))) throw new InvalidDataException("转换配方必须是存在的 JSON；打包模式请清空配方。");
        if (deformations.Length > 0 && !File.Exists(deformations)) throw new InvalidDataException("形态参数配置必须是存在的 JSON 文件。");
        var comparer = StringComparer.OrdinalIgnoreCase;
        if (new[] { source, recipe, deformations, output, report }.Where(p => p.Length > 0).Any(p => comparer.Equals(p, file)) ||
            new[] { source, recipe, deformations, report }.Where(p => p.Length > 0).Any(p => comparer.Equals(p, output)) ||
            report.Length > 0 && new[] { source, recipe, deformations }.Where(p => p.Length > 0).Any(p => comparer.Equals(p, report)))
            throw new InvalidDataException("工程、输入、配方、形态配置、输出和报告不能相互覆盖。");
        var stored = new BemExportProject
        {
            Mode = Mode, Package = new(Package), Source = PortablePath(source, file), Output = PortablePath(output, file),
            Recipe = recipe.Length == 0 ? "" : PortablePath(recipe, file), Report = report.Length == 0 ? "" : PortablePath(report, file),
            Deformations = deformations.Length == 0 ? "" : PortablePath(deformations, file)
        };
        Directory.CreateDirectory(Path.GetDirectoryName(file)!);
        string temporary = file + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try { File.WriteAllText(temporary, JsonSerializer.Serialize(stored, JsonOptions), new UTF8Encoding(false)); File.Move(temporary, file, true); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
        Source = stored.Source; Output = stored.Output; Recipe = stored.Recipe; Report = stored.Report; Deformations = stored.Deformations;
    }

    public void ReadPackMetadata(string source)
    {
        using var doc = JsonDocument.Parse(File.ReadAllText(source, Encoding.UTF8));
        var manifest = doc.RootElement.GetProperty("manifest");
        Package = new() { ["id"] = manifest.GetProperty("package_id").GetString()!, ["name"] = manifest.GetProperty("name").GetString()!,
            ["author"] = manifest.GetProperty("author").GetString()!, ["version"] = manifest.GetProperty("version").GetString()! };
    }
}
