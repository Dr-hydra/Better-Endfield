using BetterEndfield.UI.Services;
string expectedRoot = Path.GetDirectoryName(Environment.ProcessPath!)!;
string actual = RuntimePathDiscoveryService.BundledInjectorPath;
string expected = Path.Combine(expectedRoot, "loaders", "BetterEndfield.Injector.exe");
Console.WriteLine($"Process directory: {expectedRoot}");
Console.WriteLine($"AppContext.BaseDirectory: {AppContext.BaseDirectory}");
Console.WriteLine($"BundledInjectorPath: {actual}");
bool correct = StringComparer.OrdinalIgnoreCase.Equals(actual, expected);
Console.WriteLine($"Fixed path matches executable directory: {correct}");
Environment.ExitCode = correct ? 0 : 1;
if (args.Length > 0)
{
    var assembly = System.Reflection.Assembly.LoadFrom(args[0]);
    var discovery = assembly.GetType("BetterEndfield.UI.Services.RuntimePathDiscoveryService", true)!;
    string builtPath = (string)discovery.GetProperty("BundledInjectorPath")!.GetValue(null)!;
    if (!StringComparer.OrdinalIgnoreCase.Equals(builtPath, expected) || !File.Exists(builtPath))
        throw new Exception("Published UI injector path check failed.");
    var configuration = assembly.GetType("BetterEndfield.UI.Services.ConfigurationService", true)!;
    var resolve = configuration.GetMethod("ResolveInstallRoot", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, new[] { typeof(string), typeof(string) })!;
    string root = (string)resolve.Invoke(null, new object[] { builtPath, "xinput" })!;
    if (!StringComparer.OrdinalIgnoreCase.Equals(root, expectedRoot))
        throw new Exception("Published UI install root check failed.");
    var xinput = assembly.GetType("BetterEndfield.UI.Services.XInputDeploymentService", true)!;
    var paths = xinput.GetMethod("ResolvePaths", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!.Invoke(null, new object[] { args[1], builtPath })!;
    string source = (string)paths.GetType().GetProperty("Source")!.GetValue(paths)!;
    if (!File.Exists(source) || !StringComparer.OrdinalIgnoreCase.Equals(source, Path.Combine(expectedRoot, "payloads", "xinput1_4.dll")))
        throw new Exception("Published UI XInput source check failed.");
    Console.WriteLine("Published UI checks passed: injector exists, install root resolves, XInput payload exists.");
}
