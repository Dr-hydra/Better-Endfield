[CmdletBinding()]
param(
    [string]$SourceRoot,
    [string]$OutputDir
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
. (Join-Path $PSScriptRoot 'character_face_resources.ps1')

try {
    if (-not $SourceRoot) { $SourceRoot = Join-Path $PSScriptRoot '..' }
    $root = (Resolve-Path -LiteralPath $SourceRoot).ProviderPath
    $versionHeader = Get-Content -LiteralPath (Join-Path $root 'src\core\version.h') -Raw
    $parts = foreach ($part in @('MAJOR', 'MINOR', 'PATCH')) {
        if ($versionHeader -notmatch ("#define POSER_VERSION_$part\s+(\d+)")) { throw 'Missing source version.' }
        $Matches[1]
    }
    $version = $parts -join '.'
    $faces = @(Get-PoserFaceResources $root)
    $files = @{}
    foreach ($name in @('poser.dll', 'd3dcompiler_47.dll', 'vulkan-1.dll')) {
        $source = Join-Path $root ('plugin\' + $name)
        Assert-PoserResourcePath $source
        $bytes = [IO.File]::ReadAllBytes($source)
        if ($bytes.Length -lt 256 -or [BitConverter]::ToUInt16($bytes, 0) -ne 0x5a4d) { throw "Invalid DLL: $name" }
        $pe = [BitConverter]::ToInt32($bytes, 0x3c)
        if ($pe -lt 64 -or $pe -gt $bytes.Length - 26 -or
            [BitConverter]::ToUInt32($bytes, $pe) -ne 0x4550 -or
            [BitConverter]::ToUInt16($bytes, $pe + 4) -ne 0x8664 -or
            -not ([BitConverter]::ToUInt16($bytes, $pe + 22) -band 0x2000)) { throw "Expected x64 DLL: $name" }
        if ($name -eq 'poser.dll') {
            $info = (Get-Item -LiteralPath $source).VersionInfo
            if ($info.ProductName -ne 'Endfield Poser' -or $info.FileVersion -ne $version) {
                throw 'poser.dll does not match the source version. Run build.bat first.'
            }
            # Check both RC metadata and the C++ marker; never load the DLL to inspect it.
            $binaryText = [Text.Encoding]::ASCII.GetString($bytes)
            if ($info.Comments -ne 'EndfieldPoser:xxmi_bridge=0' -or
                -not $binaryText.Contains('EndfieldPoser:xxmi_bridge=0') -or
                $binaryText.Contains('EndfieldPoser:xxmi_bridge=1')) {
                throw 'Release packages must exclude the XXMI bridge. Rebuild with build.bat (without -EnableXxmiBridge).'
            }
            $files['plugin\poser.dll'] = $source
        } else {
            if (-not [Text.Encoding]::ASCII.GetString($bytes).Contains('[PROXY] plugins loaded via ')) {
                throw "Unexpected proxy: $name"
            }
            $files[$name] = $source
        }
    }
    # Explicit allowlist: never package the developer's whole plugin/ or docs/.
    foreach ($relative in @('安全安装.bat', 'tools\deploy.ps1', 'tools\character_face_resources.ps1',
            'README.md', 'LICENSE', 'docs\tutorial.md', 'docs\mmd-player.md', 'docs\user-agreement.md', 'docs\xxmi-bridge.md',
            'resources\character-faces\README.md', 'tools\blender\endfield_poser_bridge\README.md',
            'tools\blender\endfield_poser_bridge\__init__.py')) {
        $files[$relative] = Join-Path $root $relative
    }
    foreach ($file in Get-ChildItem -LiteralPath (Join-Path $root 'licenses') -Filter '*.txt' -File) {
        $files['licenses\' + $file.Name] = $file.FullName
    }
    $releaseNotes = "docs\releases\v$version.md"
    if (Test-Path -LiteralPath (Join-Path $root $releaseNotes) -PathType Leaf) {
        $files[$releaseNotes] = Join-Path $root $releaseNotes
    }
    foreach ($face in $faces) { $files['resources\character-faces\' + $face.name] = $face.source }
    $hashes = @{}
    foreach ($relative in $files.Keys) {
        Assert-PoserResourcePath $files[$relative]
        $hashes[$relative] = Get-PoserResourceHash $files[$relative]
    }
    foreach ($face in $faces) {
        if ($hashes['resources\character-faces\' + $face.name] -ne $face.hash) { throw 'Face profile changed while packaging.' }
    }

    if (-not $OutputDir) { $OutputDir = Join-Path $root 'dist' }
    $out = [IO.Path]::GetFullPath($OutputDir)
    Assert-PoserResourcePath $out
    $archive = Join-Path $out "Endfield-Poser-v$version-win64.zip"
    if ((Test-Path -LiteralPath $archive) -or (Test-Path -LiteralPath ($archive + '.sha256'))) {
        throw 'Package already exists. Choose another -OutputDir.'
    }
    $stage = Join-Path $out ('package-' + [Guid]::NewGuid().ToString('N'))
    $package = Join-Path $stage "Endfield-Poser-v$version"
    [IO.Directory]::CreateDirectory($package) | Out-Null
    foreach ($relative in $files.Keys) {
        $destination = Join-Path $package $relative
        [IO.Directory]::CreateDirectory((Split-Path -Path $destination -Parent)) | Out-Null
        [IO.File]::Copy($files[$relative], $destination, $false)
        if ((Get-PoserResourceHash $destination) -ne $hashes[$relative]) { throw "Copy verification failed: $relative" }
    }
    $utf8 = [Text.UTF8Encoding]::new($false)
    $instructions = @"
Endfield Poser $version (Windows x64)

1. 完全退出游戏，将本安装包完整解压到游戏目录之外的文件夹。
2. 双击安全安装.bat，选择直接包含 Endfield.exe 的游戏目录，再选择安装或更新。
3. 角色表情校准随包自动安装；自定义校准、设置和姿态保留。同名文件被修改时会提示并保留。
4. 启动游戏，阅读并确认使用协议。按 L 打开面板，按 P 冻结角色。
5. 卸载时退出游戏，运行同一向导并选择卸载；校准、设置、姿态和备份保留。

更新时使用新版安装包里的向导，无需先卸载。向导不会自动下载更新，会显示当前版本和安装包版本。

表情面板的 MMD 模式可直接调节眉毛、眼睛和嘴部。使用说明见 README.md 和 docs/tutorial.md。
MMD 表情参考自茶叶味香皂的终末地 MMD 模型：https://space.bilibili.com/3546783156276148 。相关素材请遵守原作者的使用规则。
不得使用本工具制作或传播违反鹰角官方创作限制的产物。使用后果由用户自行承担；作者等在法律允许的范围内免责。完整约定见 docs/user-agreement.md。
"@
    [IO.File]::WriteAllText((Join-Path $package '安装说明.txt'), $instructions, [Text.UTF8Encoding]::new($true))
    $buildInfo = @{ product = 'Endfield Poser'; version = $version; platform = 'Windows x64';
        face_profiles = $faces.Count; xxmi_bridge = $false; packaged_at = (Get-Date -Format o) }
    if ((Test-Path -LiteralPath (Join-Path $root '.git')) -and (Get-Command git -ErrorAction SilentlyContinue)) {
        $commit = & git -C $root rev-parse HEAD
        if ($LASTEXITCODE -ne 0) { throw 'Cannot read source commit.' }
        $changes = @(& git -C $root status --porcelain)
        if ($LASTEXITCODE -ne 0) { throw 'Cannot read source status.' }
        $buildInfo.source_commit = $commit.Trim()
        $buildInfo.source_dirty = $changes.Count -gt 0
    }
    [IO.File]::WriteAllText((Join-Path $package 'BUILD-INFO.json'), ($buildInfo | ConvertTo-Json), $utf8)
    $lines = @(Get-ChildItem -LiteralPath $package -File -Recurse | Sort-Object FullName | ForEach-Object {
        (Get-PoserResourceHash $_.FullName) + '  ' + $_.FullName.Substring($package.Length + 1).Replace('\', '/')
    })
    [IO.File]::WriteAllText((Join-Path $package 'FILE-SHA256SUMS.txt'), (($lines -join "`n") + "`n"), $utf8)
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    [IO.Compression.ZipFile]::CreateFromDirectory($stage, $archive)
    [IO.File]::WriteAllText(($archive + '.sha256'),
        ((Get-PoserResourceHash $archive) + '  ' + [IO.Path]::GetFileName($archive) + "`n"), $utf8)
    Write-Host "打包完成：$archive"
    Write-Host "包含 $($faces.Count) 份角色表情校准。安装时自动复制；用户设置不打包。"
} catch {
    Write-Verbose $_.ScriptStackTrace
    Write-Error -ErrorAction Continue $_.Exception.Message
    exit 1
}
