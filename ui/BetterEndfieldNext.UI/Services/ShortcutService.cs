using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace BetterEndfieldNext.UI.Services;

internal static class ShortcutService
{
    public static string CreateApplicationShortcut()
    {
        string executablePath = Environment.ProcessPath ??
            Path.Combine(AppContext.BaseDirectory, "BetterEndfieldNext.exe");
        if (!File.Exists(executablePath))
        {
            throw new FileNotFoundException("无法确定 Better Endfield Next 程序路径。", executablePath);
        }

        string shortcutPath = Path.Combine(GetDesktopDirectory(), "Better Endfield Next.lnk");
        CreateShortcut(
            shortcutPath,
            executablePath,
            string.Empty,
            Path.GetDirectoryName(executablePath) ?? AppContext.BaseDirectory,
            executablePath,
            "打开 Better Endfield Next");
        return shortcutPath;
    }

    public static string CreateGameShortcut(
        string loaderMode,
        string injectorPath,
        string launcherPath,
        string gamePath,
        string gameArguments,
        string? officialLauncherPath = null,
        bool bypassOfficialLauncher = false)
    {
        gamePath = Path.GetFullPath(gamePath.Trim());
        var startInfo = GameLaunchService.CreateStartInfo(
            loaderMode, injectorPath, launcherPath, gamePath, gameArguments, officialLauncherPath, bypassOfficialLauncher);

        string shortcutPath = Path.Combine(
            GetDesktopDirectory(),
            "终末地（Better Endfield Next）.lnk");
        CreateShortcut(
            shortcutPath,
            startInfo.FileName,
            startInfo.Arguments,
            startInfo.WorkingDirectory,
            gamePath,
            "使用已保存的 Better Endfield Next 配置启动终末地",
            runAsAdmin: true);
        return shortcutPath;
    }

    public static string? ResolveShortcutTarget(string shortcutPath)
    {
        if (string.IsNullOrWhiteSpace(shortcutPath) || !File.Exists(shortcutPath))
        {
            return null;
        }

        object shellLinkObject = new ShellLink();
        try
        {
            var persistFile = (IPersistFile)shellLinkObject;
            persistFile.Load(shortcutPath, 0);
            var shellLink = (IShellLinkW)shellLinkObject;
            var targetBuilder = new StringBuilder(1024);
            shellLink.GetPath(targetBuilder, targetBuilder.Capacity, nint.Zero, 0);
            string target = targetBuilder.ToString();
            return string.IsNullOrWhiteSpace(target) ? null : target;
        }
        catch
        {
            return null;
        }
        finally
        {
            Marshal.FinalReleaseComObject(shellLinkObject);
        }
    }

    private static string GetDesktopDirectory()
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        if (string.IsNullOrWhiteSpace(desktop))
        {
            throw new DirectoryNotFoundException("无法定位当前用户的桌面目录。");
        }
        Directory.CreateDirectory(desktop);
        return desktop;
    }

    private static void CreateShortcut(
        string shortcutPath,
        string targetPath,
        string arguments,
        string workingDirectory,
        string iconPath,
        string description,
        bool runAsAdmin = false)
    {
        if (File.Exists(shortcutPath))
        {
            try { File.Delete(shortcutPath); } catch { }
        }

        object shellLinkObject = new ShellLink();
        try
        {
            var shellLink = (IShellLinkW)shellLinkObject;
            shellLink.SetPath(targetPath);
            shellLink.SetArguments(arguments);
            shellLink.SetWorkingDirectory(workingDirectory);
            shellLink.SetDescription(description);
            shellLink.SetIconLocation(iconPath, 0);

            if (runAsAdmin && shellLinkObject is IShellLinkDataList dataList)
            {
                dataList.GetFlags(out uint flags);
                dataList.SetFlags(flags | 0x00002000); // SLDF_RUNAS_USER
            }

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
    [Guid("45E2B4AE-B1C3-11D0-B92F-00A0C90312E1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IShellLinkDataList
    {
        void AddDataBlock(nint pDataBlock);
        void CopyDataBlock(uint dwSig, out nint ppDataBlock);
        void RemoveDataBlock(uint dwSig);
        void GetFlags(out uint pdwFlags);
        void SetFlags(uint dwFlags);
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
