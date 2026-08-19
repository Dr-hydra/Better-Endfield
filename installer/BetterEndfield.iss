#ifndef StageDir
  #error StageDir must be supplied by BuildInstaller.ps1
#endif
#ifndef OutputDir
  #error OutputDir must be supplied by BuildInstaller.ps1
#endif
#ifndef AppVersion
#define AppVersion "2.1.1"
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
VersionInfoDescription=Better Endfield 中文安装程序
VersionInfoProductName={#AppName}
VersionInfoProductVersion={#AppVersion}.0

[Languages]
Name: "chinesesimp"; MessagesFile: "Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#StageDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\Better Endfield"; Filename: "{app}\BetterEndfield.exe"; WorkingDir: "{app}"
Name: "{autodesktop}\Better Endfield"; Filename: "{app}\BetterEndfield.exe"; WorkingDir: "{app}"; Tasks: desktopicon

[Run]
Filename: "{app}\BetterEndfield.exe"; Description: "启动 Better Endfield"; WorkingDir: "{app}"; Flags: nowait postinstall skipifsilent

[UninstallRun]
Filename: "{app}\BetterEndfield.exe"; Parameters: "--uninstall-xinput-silent"; WorkingDir: "{app}"; RunOnceId: "RemoveXInputProxy"; Flags: runhidden waituntilterminated skipifdoesntexist
