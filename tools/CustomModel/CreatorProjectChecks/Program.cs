using System.Text.Json;
using BetterEndfield.UI.Services;

static void Check(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}
static void Reject(Action action, string message)
{
    try { action(); }
    catch (InvalidDataException) { return; }
    throw new InvalidOperationException(message);
}

string root = Path.Combine(Path.GetTempPath(), "BemCreatorProjectChecks-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);
try
{
    string input = Path.Combine(root, "editable", "project.json");
    Directory.CreateDirectory(Path.GetDirectoryName(input)!);
    File.WriteAllText(input, """{"manifest":{"package_id":"creator.existing","name":"原始名称","author":"作者","version":"1"},"payload_files":[]}""");
    string first = Path.Combine(root, "export.bemproj.json");
    var project = new BemExportProject { Mode = "pack", Source = input };
    project.ReadPackMetadata(input);
    project.Save(first, null);
    var reopened = BemExportProject.Load(first);
    Check(reopened.Source == "editable/project.json", "input should use a portable relative path");
    Check(reopened.Package["id"] == "creator.existing", "pack project should preserve package identity");
    reopened.Package["name"] = "修改后的名称";
    reopened.Package["version"] = "2";
    reopened.Save(first, first);
    reopened = BemExportProject.Load(first);
    Check(reopened.Package["name"] == "修改后的名称" && reopened.Package["version"] == "2", "edited parameters should survive reopen");
    Check(reopened.Package["id"] == "creator.existing", "repeated save should preserve package identity");
    string second = Path.Combine(root, "other", "export.bemproj.json");
    reopened.Save(second, first);
    var moved = BemExportProject.Load(second);
    Check(BemExportProject.Resolve(moved.Source, second) == input, "save as should retain the same input");
    Check(BemExportProject.Resolve(moved.Output, second) == Path.Combine(root, "dist", "appearance.bem"), "save as should retain the selected output");
    Check(moved.Source == "../editable/project.json", "save as should rebase relative paths");
    string invalid = Path.Combine(root, "invalid.json");
    File.WriteAllText(invalid, "{}");
    Reject(() => BemExportProject.Load(invalid), "a BEM manifest must not be mistaken for an export task");
    moved.Output = input;
    Reject(() => moved.Save(second, second), "output must not overwrite input");
    Check(File.ReadAllText(input).Contains("creator.existing"), "rejected operation must preserve the input");
    Console.WriteLine("Creator project checks passed: reopen, edit, identity, save as, paths and collisions.");
}
finally { Directory.Delete(root, true); }
