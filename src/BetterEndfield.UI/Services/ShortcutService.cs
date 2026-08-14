using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace EFStartChange.UI.Services;

internal static class ShortcutService
{
    public static string CreateApplicationShortcut()
    {
        string executablePath = Environment.ProcessPath ??
            Path.Combine(AppContext.BaseDirectory, "BetterEndfield.exe");
        if (!File.Exists(executablePath))
        {
            throw new FileNotFoundException("无法确定 Better Endfield 程序路径。", executablePath);
        }

        string shortcutPath = Path.Combine(GetDesktopDirectory(), "Better Endfield.lnk");
        CreateShortcut(
            shortcutPath,
            executablePath,
            string.Empty,
            AppContext.BaseDirectory,
            executablePath,
            "打开 Better Endfield");
        return shortcutPath;
    }

    public static string CreateGameShortcut(string mapperPath, string gamePath)
    {
        mapperPath = Path.GetFullPath(mapperPath.Trim());
        gamePath = Path.GetFullPath(gamePath.Trim());
        if (!File.Exists(mapperPath))
        {
            throw new FileNotFoundException("未找到注入器。", mapperPath);
        }
        if (!File.Exists(gamePath))
        {
            throw new FileNotFoundException("未找到 Endfield.exe。", gamePath);
        }

        string shortcutPath = Path.Combine(
            GetDesktopDirectory(),
            "终末地（Better Endfield）.lnk");
        CreateShortcut(
            shortcutPath,
            mapperPath,
            QuoteArgument(gamePath),
            Path.GetDirectoryName(mapperPath) ?? AppContext.BaseDirectory,
            gamePath,
            "使用已保存的 Better Endfield 配置启动终末地");
        return shortcutPath;
    }

    private static string GetDesktopDirectory()
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        if (string.IsNullOrWhiteSpace(desktop))
        {
            throw new DirectoryNotFoundException("无法定位当前用户的桌面目录。");
        }
        Directory.CreateDirectory(desktop);
        return desktop;
    }

    private static string QuoteArgument(string value) =>
        $"\"{value.Replace("\"", "\\\"")}\"";

    private static void CreateShortcut(
        string shortcutPath,
        string targetPath,
        string arguments,
        string workingDirectory,
        string iconPath,
        string description)
    {
        object shellLinkObject = new ShellLink();
        try
        {
            var shellLink = (IShellLinkW)shellLinkObject;
            shellLink.SetPath(targetPath);
            shellLink.SetArguments(arguments);
            shellLink.SetWorkingDirectory(workingDirectory);
            shellLink.SetDescription(description);
            shellLink.SetIconLocation(iconPath, 0);
            ((IPersistFile)shellLinkObject).Save(shortcutPath, true);
        }
        finally
        {
            Marshal.FinalReleaseComObject(shellLinkObject);
        }
    }

    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    private sealed class ShellLink
    {
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    private interface IShellLinkW
    {
        void GetPath(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder file,
            int fileLength,
            nint findData,
            uint flags);
        void GetIDList(out nint itemIdList);
        void SetIDList(nint itemIdList);
        void GetDescription(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder name,
            int nameLength);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string name);
        void GetWorkingDirectory(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder directory,
            int directoryLength);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string directory);
        void GetArguments(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder arguments,
            int argumentsLength);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string arguments);
        void GetHotkey(out short hotkey);
        void SetHotkey(short hotkey);
        void GetShowCmd(out int showCommand);
        void SetShowCmd(int showCommand);
        void GetIconLocation(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder iconPath,
            int iconPathLength,
            out int iconIndex);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string iconPath, int iconIndex);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string path, uint reserved);
        void Resolve(nint windowHandle, uint flags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string path);
    }
}
