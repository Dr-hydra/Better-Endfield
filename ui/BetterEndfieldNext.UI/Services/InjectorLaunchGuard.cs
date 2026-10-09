namespace BetterEndfieldNext.UI.Services;

internal static class InjectorLaunchGuard
{
    public static void EnsureAllowed(string gamePath)
    {
        string proxy = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(gamePath))!, "xinput1_4.dll");
        if (File.Exists(proxy) || Directory.Exists(proxy))
            throw new InvalidOperationException(LocalizationService.Instance.IsChinese
                ? "游戏目录已有 xinput1_4.dll，不能再使用内置注入器。BE 代理请改用 XInput 启动，或先由 BE 卸载；其他来源的文件请先处理冲突。"
                : "xinput1_4.dll is already in the game directory. Do not use the built-in injector alongside it. Use XInput for a BE proxy or uninstall it through BE first; resolve other loaders separately.");
    }
}
