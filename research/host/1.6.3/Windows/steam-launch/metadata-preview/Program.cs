using System.Text;
using System.Text.Json;
using BetterEndfieldNext.UI.Services;

if (args.Length != 2)
{
    Console.Error.WriteLine("Usage: SteamMetadataPreview <appinfo-or-normalized.json> <research-output-directory>");
    return 2;
}

try
{
    var metadata = SteamMetadata.Parse(File.ReadAllText(args[0]));
    metadata.Validate();
    var normalized = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });
    var reloaded = SteamMetadata.Parse(normalized);
    if (reloaded.Identity() != metadata.Identity())
        throw new InvalidDataException("Normalized metadata changed during round-trip.");

    // Only these static production methods are used. Never create an integration
    // service or call ApplyAsync, CacheAsync, process launch or Steam discovery.
    var acf = SteamIntegrationService.GenerateAcf(metadata);
    var state = SteamKeyValues.Parse(acf).Object("AppState")
        ?? throw new InvalidDataException("ACF AppState is absent.");
    if (state.String("appid") != metadata.AppId ||
        state.String("installdir") != metadata.InstallDirectory ||
        state.String("buildid") != metadata.BuildId)
        throw new InvalidDataException("ACF identity differs from parsed metadata.");
    foreach (var depot in metadata.Depots)
        if (state.Object("InstalledDepots")?.Object(depot.Id)?.String("manifest") != depot.Manifest)
            throw new InvalidDataException("ACF depot manifest differs from parsed metadata.");

    string output = Path.GetFullPath(args[1]);
    Directory.CreateDirectory(output);
    File.WriteAllText(Path.Combine(output, "normalized-metadata.json"), normalized + Environment.NewLine, new UTF8Encoding(false));
    File.WriteAllText(Path.Combine(output, "preview-only.acf"), acf, new UTF8Encoding(false));
    Console.WriteLine(JsonSerializer.Serialize(new
    {
        result = "PASS",
        metadata.AppId,
        metadata.InstallDirectory,
        metadata.Executable,
        metadata.LaunchArguments,
        metadata.BuildId,
        metadata.Depots,
        missingFields = metadata.MissingFields.Count,
        normalizedRoundTrip = true,
        acfIdentityAndManifestRoundTrip = true,
        output
    }, new JsonSerializerOptions { WriteIndented = true }));
    return 0;
}
catch (Exception exception)
{
    Console.Error.WriteLine(exception.Message);
    return 1;
}
