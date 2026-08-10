#ifndef StageDir
  #error StageDir must be supplied by BuildInstaller.ps1
#endif
#ifndef OutputDir
  #error OutputDir must be supplied by BuildInstaller.ps1
#endif
#ifndef AppVersion
  #define AppVersion "1.0.0"
#endif

#define AppName "EF Start Change"
#define Publisher "Dr.Hydra"

[Setup]
AppId={{EA68D31F-18E7-48EA-9262-0E7A198EFAC1}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#Publisher}
AppPublisherURL=https://github.com/Dr-hydra/EF-Start-Change
AppSupportURL=https://github.com/Dr-hydra/EF-Start-Change/issues
AppUpdatesURL=https://github.com/Dr-hydra/EF-Start-Change/releases
DefaultDirName={localappdata}\Programs\EF Start Change
DefaultGroupName=EF Start Change
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir={#OutputDir}
OutputBaseFilename=EFStartChange-{#AppVersion}-Setup
SetupIconFile=..\src\EFStartChange.UI\Assets\gilberta.ico
WizardSmallImageFile=..\src\EFStartChange.UI\Assets\gilberta.png
UninstallDisplayIcon={app}\EFStartChange.exe
LicenseFile=DISCLAIMER.zh-CN.txt
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
CloseApplications=force
RestartApplications=no
VersionInfoVersion=1.0.0.0
VersionInfoCompany={#Publisher}
VersionInfoDescription=EF Start Change 中文安装程序
VersionInfoProductName={#AppName}
VersionInfoProductVersion=1.0.0.0

[Languages]
Name: "chinesesimp"; MessagesFile: "Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#StageDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\EF Start Change"; Filename: "{app}\EFStartChange.exe"; WorkingDir: "{app}"
Name: "{autodesktop}\EF Start Change"; Filename: "{app}\EFStartChange.exe"; WorkingDir: "{app}"; Tasks: desktopicon

[Run]
Filename: "{app}\EFStartChange.exe"; Description: "启动 EF Start Change"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent
