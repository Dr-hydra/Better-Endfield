using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BetterEndfieldNext.UI.Services;

internal static class GameLaunchService
{
    public static string SelectHelperPath(string loaderMode, string injectorPath, string launcherPath) =>
        loaderMode.ToLowerInvariant() switch
        {
            "injector" => injectorPath,
            "xinput" => launcherPath,
            _ => throw new InvalidOperationException("不支持的加载方式。")
        };

    public static async Task EnsureLoaderReadyAsync(string loaderMode, string gamePath, Func<Task<bool>> isXInputInstalled)
    {
        if (loaderMode.Equals("injector", StringComparison.OrdinalIgnoreCase))
        {
            InjectorLaunchGuard.EnsureAllowed(gamePath);
            return;
        }
        if (!loaderMode.Equals("xinput", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("不支持的加载方式。");
        if (!await isXInputInstalled())
            throw new InvalidOperationException("XInput 尚未安装或需要更新，请先在“XInput 自启动”区域点击“安装或更新”。 / Use Install / Update in the XInput Auto-load section before launching in this mode.");
    }

    public static bool AreArgumentsValid(string arguments)
    {
        if (arguments.Length > 2048 || arguments.Any(c => c is '\0' or '\r' or '\n') ||
            Regex.IsMatch(arguments, "(^|[\\s\"])--steam-command($|[\\s\"])", RegexOptions.IgnoreCase)) return false;
        // An unfinished quoted argument would swallow the Steam separator.
        bool quoted = false;
        int backslashes = 0;
        foreach (char character in arguments)
        {
            if (character == '"' && backslashes % 2 == 0) quoted = !quoted;
            backslashes = character == '\\' ? backslashes + 1 : 0;
        }
        return !quoted;
    }

    public static string BuildArguments(string gamePath, string gameArguments)
    {
        string game = Path.GetFullPath(gamePath.Trim());
        if (!File.Exists(game) || !Path.GetFileName(game).Equals("Endfield.exe", StringComparison.OrdinalIgnoreCase))
            throw new FileNotFoundException("未找到有效的 Endfield.exe。", game);
        if (!AreArgumentsValid(gameArguments))
            throw new ArgumentException("游戏启动参数无效。", nameof(gameArguments));

        // Keep all saved game flags after the helper's option separator.
        string arguments = "--game \"" + game + "\" --";
        return string.IsNullOrWhiteSpace(gameArguments) ? arguments : arguments + " " + gameArguments.Trim();
    }

    public static ProcessStartInfo CreateStartInfo(
        string loaderMode,
        string injectorPath,
        string launcherPath,
        string gamePath,
        string gameArguments)
    {
        string arguments = BuildArguments(gamePath, gameArguments);
        string helperPath = SelectHelperPath(loaderMode, injectorPath, launcherPath);
        if (loaderMode.Equals("injector", StringComparison.OrdinalIgnoreCase)) InjectorLaunchGuard.EnsureAllowed(gamePath);

        string executable = Path.GetFullPath(helperPath.Trim());
        if (!File.Exists(executable))
            throw new FileNotFoundException("启动组件缺失，请重新解压完整的 Better Endfield Next。", executable);
        return new ProcessStartInfo
        {
            FileName = executable,
            WorkingDirectory = Path.GetDirectoryName(executable) ?? AppContext.BaseDirectory,
            Arguments = arguments,
            UseShellExecute = true,
            Verb = "runas"
        };
    }
}
