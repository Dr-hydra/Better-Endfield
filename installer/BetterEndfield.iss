#ifndef StageDir
  #error StageDir must be supplied by BuildInstaller.ps1
#endif
#ifndef OutputDir
  #error OutputDir must be supplied by BuildInstaller.ps1
#endif
#ifndef AppVersion
#define AppVersion "3.1.1"
#endif

#define AppName "Better Endfield"
#define Publisher "Dr.Hydra"

[Setup]
AppId={{EA68D31F-18E7-48EA-9262-0E7A198EFAC1}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#Publisher}
AppPublisherURL=https://github.com/Dr-hydra/Better-Endfield
AppSupportURL=https://github.com/Dr-hydra/Better-Endfield/issues
AppUpdatesURL=https://github.com/Dr-hydra/Better-Endfield/releases
DefaultDirName={localappdata}\Programs\Better Endfield
DefaultGroupName=Better Endfield
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir={#OutputDir}
OutputBaseFilename=BetterEndfield-{#AppVersion}-Setup
SetupIconFile=..\ui\BetterEndfield.UI\Assets\shared\gilberta.ico
WizardSmallImageFile=..\ui\BetterEndfield.UI\Assets\shared\gilberta.png
UninstallDisplayIcon={app}\BetterEndfield.exe
InfoBeforeFile=DISCLAIMER.zh-CN.txt
LicenseFile=..\LICENSE
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
CloseApplications=force
RestartApplications=no
VersionInfoVersion={#AppVersion}.0
VersionInfoCompany={#Publisher}
VersionInfoDescription=Better Endfield Installer
VersionInfoProductName={#AppName}
VersionInfoProductVersion={#AppVersion}.0

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"; InfoBeforeFile: "DISCLAIMER.en.txt"
Name: "chinesesimp"; MessagesFile: "Languages\ChineseSimplified.isl"; InfoBeforeFile: "DISCLAIMER.zh-CN.txt"

[CustomMessages]
en.LaunchProgram=Launch Better Endfield
chinesesimp.LaunchProgram=启动 Better Endfield

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#StageDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\Better Endfield"; Filename: "{app}\BetterEndfield.exe"; WorkingDir: "{app}"
Name: "{autodesktop}\Better Endfield"; Filename: "{app}\BetterEndfield.exe"; WorkingDir: "{app}"; Tasks: desktopicon

[Run]
Filename: "{app}\BetterEndfield.exe"; Description: "{cm:LaunchProgram}"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent

[UninstallRun]
; 清理写入游戏目录的全部文件：XInput 代理与显示增强组件。旧安装记录的是
; --uninstall-xinput-silent，程序仍接受该参数。
Filename: "{app}\BetterEndfield.exe"; Parameters: "--uninstall-game-files-silent"; WorkingDir: "{app}"; RunOnceId: "RemoveGameDirectoryFiles"; Flags: runhidden waituntilterminated skipifdoesntexist
