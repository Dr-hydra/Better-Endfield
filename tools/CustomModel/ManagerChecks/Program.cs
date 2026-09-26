using BetterEndfield.UI.Services;

static void Check(bool ok, string why) { if (!ok) throw new InvalidOperationException(why); }
var inspect = BemInspectionSummary.Read("""{"format":"component-n","analysis":{"components":[{},{}],"textures":24,"errors":[]}}""");
Check(inspect.CanProvideRecipe && !inspect.CanAutoConvert && inspect.Title.Contains("尚不能自动转换") && inspect.Detail.Contains("24"), "source structure must not be presented as conversion-ready");
var autoReady = BemInspectionSummary.Read("""{"format":"component-n","conversion_ready":true,"matching":{"character_name":"女管理员","warnings":[]}}""");
Check(autoReady.CanAutoConvert && !autoReady.CanProvideRecipe && autoReady.Detail.Contains("女管理员"), "verified automatic conversion must expose direct action without recipe");
var ready = BemInspectionSummary.Read("""{"format":"BEMv1"}""");
Check(ready.AlreadyPackaged && !ready.CanProvideRecipe && ready.NextStep.Contains("导入"), "finished BEM should route to import");
var composable = BemInspectionSummary.Read("""{"format":"BEMv1.1"}""");
Check(composable.AlreadyPackaged, "BEM 1.1 should route to import");
Check(BemReportPresentation.Package("""{"package":{"name":"组合包","option_groups":[{"name":"服装"}]},"size":1000}""").Contains("选项组：服装"),
    "BEM 1.1 creator report should show groups");
var ambiguous = BemInspectionSummary.Read("""{"format":"unknown","entries":[{},{}]}""");
Check(ambiguous.CanProvideRecipe && ambiguous.Title.Contains("多个"), "ambiguous entry guidance");
Check(BemReportPresentation.Failure("""{"issues":[{"message":"Missing verified profile"}]}""") == "Missing verified profile", "failure report should expose message without JSON wrapper");
var manual = BemInspectionSummary.Read("""{"format":"hash-lod","automation":{"status":"manual_only","reasons":["Custom Shader"]}}""");
Check(manual.Title.Contains("不能自动转换") && !manual.AlreadyPackaged, "custom shader must be explicitly manual");
var mapping = BemInspectionSummary.Read("""{"format":"hash-lod","automation":{"status":"requires_mapping","reasons":["Material mapping required"]},"analysis":{"components":[],"textures":0,"errors":[]}}""");
Check(mapping.Title.Contains("暂不能承诺"), "hash/LOD support must not imply full automatic conversion");
if (args is ["--presentation"]) { Console.WriteLine("PASS: source readiness, direct import, ambiguity and readable failure guidance"); return; }
if (args is ["--v11", var packagePath])
{
    var package = BemPackageService.ReadMetadata(packagePath);
    Check(package.IsComposable && package.OptionGroups.Count > 0, "BEM 1.1 metadata/groups not loaded");
    var group = package.OptionGroups[0];
    string alternate = group.Choices.First(c => c.Id != group.Default).Id;
    package.RestoreOptions(group.Id + ":" + alternate);
    Check(package.SelectedOptions[group.Id] == alternate && package.OptionsValid(), "BEM 1.1 selection not restored");
    var service = new BemPackageService(); service.Packages.Add(package);
    await service.SaveAsync();
    string ini = File.ReadAllText(Path.Combine(service.Root, "runtime.ini"));
    Check(ini.Contains("options=" + group.Id + ":" + alternate) && !ini.Contains("appearance="),
        "BEM 1.1 runtime configuration not written");
    string temporary = Path.GetFullPath(ConfigurationService.SettingsDirectory);
    if (temporary.StartsWith(Path.GetFullPath(Path.GetTempPath()), StringComparison.OrdinalIgnoreCase) &&
        Path.GetFileName(temporary).StartsWith("BemManagerChecks-")) Directory.Delete(temporary, true);
    Console.WriteLine("PASS: BEM 1.1 groups, selection restore and runtime options");
    return;
}
if (args.Length != 4) throw new ArgumentException("install-root, original.bem, second-package.bem, updated.bem");
try
{
    var service = new BemPackageService();
    service.Load(); Check(service.Packages.Count == 0 && !service.EffectiveLod, "empty library");
    await service.ImportAsync(args[1], args[0]);
    Check(service.Packages.Count == 1 && !service.Packages[0].Enabled, "new import must be disabled");
    var first = service.Packages[0]; first.SelectedAppearance = "hidden";
    await service.SetEnabledAsync(first, true);
    Check(service.EffectiveLod && !service.StandaloneLod, "mod must force LOD without changing preference");
    service.Load(); Check(service.Packages[0].SelectedAppearance == "hidden" && service.Packages[0].Enabled, "selection persistence");
    await service.ImportAsync(args[2], args[0]);
    var second = service.Packages.Single(p => p.Id == "test.second");
    await service.SetEnabledAsync(second, true);
    Check(service.Packages.Count(p => p.Enabled) == 1, "same-role conflict");
    await service.SetEnabledAsync(second, false); Check(!service.EffectiveLod, "restore independent LOD");
    service.StandaloneLod = true; await service.SaveAsync(); service.Load(); Check(service.EffectiveLod, "standalone persistence");
    first = service.Packages.Single(p => p.Id == "test.package"); await service.SetEnabledAsync(first, true);
    await service.ImportAsync(args[3], args[0]);
    first = service.Packages.Single(p => p.Id == "test.package");
    Check(first.Enabled && first.SelectedAppearance == "default" && service.Notices.Count > 0, "update fallback preserves enabled state");
    var corrupt = Path.Combine(ConfigurationService.SettingsDirectory, "broken.bem");
    File.WriteAllText(corrupt, "broken");
    try { await service.ImportAsync(corrupt, args[0]); throw new InvalidOperationException("bad import accepted"); }
    catch (InvalidDataException) { }
    Check(service.Packages.Count == 2, "failed import changed library");
    await service.RemoveAsync(first); Check(service.Packages.Count == 1 && service.EffectiveLod, "remove/standalone state");
    string zip = Path.Combine(ConfigurationService.SettingsDirectory, "collection.zip");
    using (var archive = System.IO.Compression.ZipFile.Open(zip, System.IO.Compression.ZipArchiveMode.Create))
    {
        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(archive, args[1], "nested/first.bem");
        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(archive, args[2], "second.bem");
        using var bad = new StreamWriter(archive.CreateEntry("broken.bem").Open()); bad.Write("broken");
    }
    string stagedFile;
    using (var bundle = await service.PrepareBundleAsync(zip, args[0]))
    {
        Check(bundle.Packages.Count == 2 && bundle.Issues.Count == 1, "ZIP partial validation");
        var selected = bundle.Packages.Single(p => p.Id == "test.package"); stagedFile = selected.File;
        await service.ImportAsync(selected.File, args[0]);
        Check(service.Packages.Count == 2 && !service.Packages.Single(p => p.Id == selected.Id).Enabled, "selected ZIP import state");
    }
    Check(!File.Exists(stagedFile), "ZIP staging cleanup");
    Console.WriteLine("PASS: staged validation, import/update/remove, appearance fallback, same-role selection and LOD persistence");
}
finally
{
    string root = Path.GetFullPath(ConfigurationService.SettingsDirectory);
    if (root.StartsWith(Path.GetFullPath(Path.GetTempPath()), StringComparison.OrdinalIgnoreCase) && Path.GetFileName(root).StartsWith("BemManagerChecks-"))
        Directory.Delete(root, true);
}

namespace BetterEndfield.UI.Services
{
    internal static class ConfigurationService
    {
        public static string SettingsDirectory { get; } = Path.Combine(Path.GetTempPath(), "BemManagerChecks-" + Guid.NewGuid());
    }
}
