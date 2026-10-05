[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [string]$GameDir,
    [ValidateSet('Install', 'Uninstall')][string]$Action = 'Install',
    [string]$SourceRoot
)

# This script never starts/stops the game or copies a developer's runtime data.
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

function Get-Sha([string]$Path) {
    # PowerShell 5.1 Get-FileHash inherits WhatIf into its internal pipeline.
    # Hashing is read-only and must also work while planning an installation.
    $stream = [IO.File]::OpenRead($Path)
    $sha = [Security.Cryptography.SHA256]::Create()
    try { return [BitConverter]::ToString($sha.ComputeHash($stream)).Replace('-', '') }
    finally { $sha.Dispose(); $stream.Dispose() }
}

function Assert-NoLink([string]$Path) {
    $node = $Path
    while ($node) {
        if (Test-Path -LiteralPath $node) {
            if ((Get-Item -LiteralPath $node -Force).Attributes -band [IO.FileAttributes]::ReparsePoint) {
                throw "Symbolic links / junctions are not supported: $node"
            }
        }
        $node = Split-Path -Path $node -Parent
    }
}

function Get-Target([string]$Relative) {
    if ([IO.Path]::IsPathRooted($Relative)) { throw 'Expected a relative installation path.' }
    $path = [IO.Path]::GetFullPath((Join-Path $script:GameRoot $Relative))
    if (-not $path.StartsWith($script:GameRoot + '\', [StringComparison]::OrdinalIgnoreCase)) {
        throw "Path escapes the game directory: $Relative"
    }
    Assert-NoLink $path
    return $path
}

function Assert-GameStopped {
    $running = @(Get-Process -Name Endfield -ErrorAction SilentlyContinue)
    if ($running.Count -gt 0) {
        $ids = ($running | ForEach-Object { $_.Id }) -join ', '
        throw "Endfield 仍在运行（进程 ID：$ids）。请完全退出游戏后重试；若窗口已关闭，请在任务管理器中确认游戏进程已退出。"
    }
}

function Assert-Dll([string]$Path) {
    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) { throw "Missing DLL: $Path" }
    $bytes = [IO.File]::ReadAllBytes($Path)
    if ($bytes.Length -lt 256 -or $bytes[0] -ne 0x4d -or $bytes[1] -ne 0x5a) { throw "Invalid DLL: $Path" }
    $pe = [BitConverter]::ToInt32($bytes, 0x3c)
    if ($pe -lt 64 -or $pe -gt ($bytes.Length - 26) -or
        [BitConverter]::ToUInt32($bytes, $pe) -ne 0x4550 -or
        [BitConverter]::ToUInt16($bytes, $pe + 4) -ne 0x8664 -or
        -not ([BitConverter]::ToUInt16($bytes, $pe + 22) -band 0x2000)) {
        throw "Expected a Windows x64 DLL: $Path"
    }
}

function Test-OurProxy([string]$Path) {
    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) { return $false }
    return [Text.Encoding]::ASCII.GetString([IO.File]::ReadAllBytes($Path)).Contains('[PROXY] plugins loaded via ')
}

function Test-OurDll([string]$Path, [string]$Relative) {
    # A manual upgrade can leave the old receipt behind. Recognize our actual
    # x64 binaries before repairing it; an unrelated replacement must survive.
    try {
        Assert-Dll $Path
        if ($Relative -eq 'plugin\poser.dll') {
            return (Get-Item -LiteralPath $Path).VersionInfo.ProductName -eq 'Endfield Poser'
        }
        return Test-OurProxy $Path
    } catch { return $false }
}

function Set-FileAtomically([string]$Source, [string]$Destination) {
    $parent = Split-Path -Path $Destination -Parent
    [IO.Directory]::CreateDirectory($parent) | Out-Null
    $staged = $Destination + '.poser-stage-' + [Guid]::NewGuid().ToString('N')
    try {
        [IO.File]::Copy($Source, $staged, $false)
        if (Test-Path -LiteralPath $Destination -PathType Leaf) {
            [IO.File]::Replace($staged, $Destination, [NullString]::Value)
        } else {
            [IO.File]::Move($staged, $Destination)
        }
    } finally {
        if (Test-Path -LiteralPath $staged) { Remove-Item -LiteralPath $staged -Force }
    }
}

try {
    . (Join-Path $PSScriptRoot 'character_face_resources.ps1')
    if (-not $SourceRoot) { $SourceRoot = Join-Path $PSScriptRoot '..' }
    if (-not $GameDir) {
        Write-Host 'Endfield Poser - 安装 / 更新 / 卸载'
        $GameDir = (Read-Host '输入含 Endfield.exe 的游戏目录（可拖入文件夹）').Trim().Trim('"')
        $choice = Read-Host '1 = 安装或更新（默认）；2 = 卸载并保留配置与姿态'
        if ($choice -eq '2') { $Action = 'Uninstall' }
        elseif ($choice -and $choice -ne '1') { throw 'Invalid choice.' }
    }
    if (-not $GameDir) { throw 'Game directory is required.' }
    $script:GameRoot = (Resolve-Path -LiteralPath $GameDir).ProviderPath.TrimEnd('\')
    Assert-NoLink $script:GameRoot
    foreach ($name in @('Endfield.exe', 'GameAssembly.dll')) {
        if (-not (Test-Path -LiteralPath (Get-Target $name) -PathType Leaf)) {
            throw "Not an Endfield game directory (missing $name): $script:GameRoot"
        }
    }
    Assert-GameStopped

    $ownedNames = @('plugin\poser.dll', 'd3dcompiler_47.dll', 'vulkan-1.dll')
    $manifestRel = 'plugin\poser-install.json'
    $manifestPath = Get-Target $manifestRel
    $oldFiles = @{}
    if (Test-Path -LiteralPath $manifestPath) {
        $oldManifest = Get-Content -LiteralPath $manifestPath -Raw -Encoding UTF8 | ConvertFrom-Json
        if ($oldManifest.product -ne 'Endfield Poser' -or $oldManifest.schema -ne 1) { throw 'Unrecognized installation record.' }
        foreach ($entry in $oldManifest.files) {
            if ($entry.path -notin $ownedNames -or $oldFiles.ContainsKey($entry.path)) { throw 'Invalid installation record paths.' }
            if ($entry.installed_sha256 -notmatch '^[A-Fa-f0-9]{64}$') { throw 'Invalid installed hash.' }
            if ($entry.original) {
                if ($entry.original -notmatch '^plugin\\poser-backups\\[a-zA-Z0-9-]+\\(plugin\\poser\.dll|d3dcompiler_47\.dll|vulkan-1\.dll)$' -and
                    $entry.original -notin @('d3dcompiler_47.dll.backup', 'vulkan-1.dll.backup')) {
                    throw 'Invalid original backup path.'
                }
                $originalPath = Get-Target $entry.original
                if (-not (Test-Path -LiteralPath $originalPath -PathType Leaf) -or
                    (Get-Sha $originalPath) -ne $entry.original_sha256) { throw "Missing or modified original backup: $originalPath" }
            }
            $oldFiles[$entry.path] = $entry
        }
    }

    $stamp = (Get-Date -Format 'yyyyMMdd-HHmmss') + '-' + [Guid]::NewGuid().ToString('N').Substring(0, 8)
    $backupRel = 'plugin\poser-backups\' + $stamp
    $operations = [Collections.Generic.List[object]]::new()
    $newFiles = [Collections.Generic.List[object]]::new()
    $payload = [Collections.Generic.List[object]]::new()
    $resourceManifestRel = 'plugin\mmd\character-faces-install.json'
    $resourceFiles = @{}
    $resourceWrites = 0
    $resourceKept = 0
    $dllWrites = 0

    if ($Action -eq 'Install') {
        $sourceDir = (Resolve-Path -LiteralPath $SourceRoot).ProviderPath
        # A separate receipt survives uninstall along with calibration data.
        # Never adopt a different existing file or overwrite user edits.
        $resources = @(Get-PoserFaceResources $sourceDir)
        $resourceManifestPath = Get-Target $resourceManifestRel
        if (Test-Path -LiteralPath $resourceManifestPath) {
            $receipt = Get-Content -LiteralPath $resourceManifestPath -Raw -Encoding UTF8 | ConvertFrom-Json
            if ($receipt.product -ne 'Endfield Poser character faces' -or $receipt.schema -ne 1) {
                throw 'Unrecognized face resource installation record.'
            }
            foreach ($entry in $receipt.files) {
                if ($entry.name -cnotmatch '^[a-z0-9_]{1,128}-[a-f0-9]{12}\.face\.json$' -or
                    $entry.installed_sha256 -notmatch '^[A-Fa-f0-9]{64}$' -or $resourceFiles.ContainsKey($entry.name)) {
                    throw 'Invalid face resource installation record.'
                }
                $resourceFiles[$entry.name] = $entry
            }
        }
        foreach ($relative in $ownedNames) {
            $source = Join-Path $sourceDir $relative
            if ($relative -ne 'plugin\poser.dll' -and -not (Test-Path -LiteralPath $source)) {
                $source = Join-Path $sourceDir ('plugin\' + $relative)
            }
            if ($relative -eq 'vulkan-1.dll' -and -not (Test-Path -LiteralPath $source)) { continue }
            Assert-Dll $source
            if ($relative -eq 'plugin\poser.dll') {
                if ((Get-Item -LiteralPath $source).VersionInfo.ProductName -ne 'Endfield Poser') { throw 'Unexpected poser.dll product.' }
            } elseif (-not (Test-OurProxy $source)) { throw "Unexpected proxy DLL: $source" }
            if ([IO.Path]::GetFullPath($source) -eq (Get-Target $relative)) {
                throw '安装包和游戏目录重合。请将完整安装包解压到游戏目录之外，再运行其中的安全安装.bat 并选择游戏目录。'
            }
            $payload.Add(@{ path = $relative; source = $source; hash = (Get-Sha $source) })
        }
        $incomingPoser = $payload | Where-Object { $_.path -eq 'plugin\poser.dll' }
        $incomingVersion = (Get-Item -LiteralPath $incomingPoser.source).VersionInfo.FileVersion
        $currentPoser = Get-Target 'plugin\poser.dll'
        $currentVersion = '未安装或无法识别'
        if (Test-OurDll $currentPoser 'plugin\poser.dll') {
            $currentVersion = (Get-Item -LiteralPath $currentPoser).VersionInfo.FileVersion
        }
        Write-Host "当前版本：$currentVersion；安装包版本：$incomingVersion。"
        Write-Host '本向导使用当前安装包中的文件，不会自动下载新版。'
        foreach ($item in $payload) {
            $destination = Get-Target $item.path
            $exists = Test-Path -LiteralPath $destination -PathType Leaf
            $currentHash = if ($exists) { Get-Sha $destination } else { $null }
            $original = $null
            $originalHash = $null
            if ($oldFiles.ContainsKey($item.path)) {
                $prior = $oldFiles[$item.path]
                if ($exists -and $currentHash -ne $prior.installed_sha256) {
                    if ($currentHash -ne $item.hash -and -not (Test-OurDll $destination $item.path)) {
                        throw "文件已被其他程序替换，无法确认为本插件，已保留：$destination"
                    }
                    Write-Warning "检测到手动更新的插件文件，将同步安装记录并保留原始备份：$($item.path)"
                }
                $original = $prior.original
                $originalHash = $prior.original_sha256
            } elseif ($exists) {
                if ($item.path -ne 'plugin\poser.dll' -and (Test-OurProxy $destination)) {
                    $legacy = $item.path + '.backup'
                    if (Test-Path -LiteralPath (Get-Target $legacy) -PathType Leaf) {
                        if (Test-OurProxy (Get-Target $legacy)) { throw "Legacy backup contains another proxy: $legacy" }
                        $original = $legacy
                        $originalHash = Get-Sha (Get-Target $legacy)
                    }
                } else {
                    $original = $backupRel + '\' + $item.path
                    $originalHash = Get-Sha $destination
                }
            }
            $newFiles.Add(@{ path = $item.path; installed_sha256 = $item.hash; original = $original; original_sha256 = $originalHash })
            # Back up even an identical old DLL if it is the recorded original.
            if (-not $exists -or $currentHash -ne $item.hash -or ($original -and $original.StartsWith($backupRel))) {
                $operations.Add(@{ path = $item.path; source = $item.source; hash = $item.hash })
                ++$dllWrites
            }
        }
        # A package without the optional Vulkan proxy must retain its old record.
        foreach ($relative in $oldFiles.Keys) {
            if ($relative -notin @($newFiles | ForEach-Object { $_.path })) { $newFiles.Add($oldFiles[$relative]) }
        }
        foreach ($item in $resources) {
            $relative = 'plugin\mmd\character-faces\' + $item.name
            $destination = Get-Target $relative
            if ([IO.Path]::GetFullPath($item.source) -eq $destination) { throw 'Source and destination face profile must differ.' }
            $exists = Test-Path -LiteralPath $destination -PathType Leaf
            $currentHash = if ($exists) { Get-Sha $destination } else { $null }
            $managed = $resourceFiles.ContainsKey($item.name) -and
                $currentHash -eq $resourceFiles[$item.name].installed_sha256
            if ($exists -and $currentHash -ne $item.hash -and -not $managed) {
                ++$resourceKept
                Write-Warning "保留自定义角色表情校准，未覆盖：$($item.name)"
                continue
            }
            if (-not $exists -or $currentHash -ne $item.hash) {
                $operations.Add(@{ path = $relative; source = $item.source; hash = $item.hash })
                ++$resourceWrites
            }
            $resourceFiles[$item.name] = @{ name = $item.name; installed_sha256 = $item.hash }
        }
        $operations.Add(@{ path = $resourceManifestRel; source = $null; manifest = $true })
        $operations.Add(@{ path = $manifestRel; source = $null; manifest = $true })
    } else {
        if ($oldFiles.Count -eq 0) {
            # Legacy installs did not record proxy ownership. Do not guess which
            # other mods still need those loaders; disabling poser.dll is enough.
            $poserPath = Get-Target 'plugin\poser.dll'
            if (Test-Path -LiteralPath $poserPath -PathType Leaf) {
                if ((Get-Item -LiteralPath $poserPath).VersionInfo.ProductName -ne 'Endfield Poser') { throw 'Unrecognized poser.dll; preserved.' }
                $operations.Add(@{ path = 'plugin\poser.dll'; source = $null })
            }
            Write-Warning '旧安装没有归属记录：仅移除 poser.dll，保留代理 DLL 和所有用户数据。'
        } else {
            # Proxies load all plugin DLLs. Keep them when another plugin needs them.
            $otherPlugins = @(Get-ChildItem -LiteralPath (Get-Target 'plugin') -Filter '*.dll' -File | Where-Object { $_.Name -ne 'poser.dll' })
            foreach ($relative in $oldFiles.Keys) {
                $prior = $oldFiles[$relative]
                $destination = Get-Target $relative
                if ($relative -ne 'plugin\poser.dll' -and $otherPlugins.Count -gt 0) {
                    Write-Warning "Other plugin DLLs exist; keeping loader: $relative"
                    continue
                }
                if (Test-Path -LiteralPath $destination -PathType Leaf) {
                    if ((Get-Sha $destination) -ne $prior.installed_sha256) { throw "File changed outside this installer; preserved: $destination" }
                }
                $restore = $null
                # Uninstall must not resurrect a backed-up older poser.dll.
                if ($relative -ne 'plugin\poser.dll' -and $prior.original) { $restore = Get-Target $prior.original }
                $operations.Add(@{ path = $relative; source = $restore })
            }
            if ($otherPlugins.Count -eq 0) { $operations.Add(@{ path = $manifestRel; source = $null }) }
        }
    }

    foreach ($op in $operations) { Write-Host ("{0}: {1}" -f $(if ($op.source -or $op.ContainsKey('manifest')) { 'Write' } else { 'Remove' }), (Get-Target $op.path)) }
    if ($operations.Count -eq 0) { Write-Host 'Nothing to change.'; return }
    if (-not $PSCmdlet.ShouldProcess($script:GameRoot, "$Action Endfield Poser; preserve user configuration and poses")) { return }
    Assert-GameStopped

    # Copy all previous files before modifying any destination. Keep backups even
    # after success/rollback; each run uses a unique directory.
    $snapshots = @{}
    foreach ($op in $operations) {
        $destination = Get-Target $op.path
        if (Test-Path -LiteralPath $destination) {
            if (-not (Test-Path -LiteralPath $destination -PathType Leaf)) { throw "Destination is not a file: $destination" }
            $backup = Get-Target ($backupRel + '\' + $op.path)
            [IO.Directory]::CreateDirectory((Split-Path -Path $backup -Parent)) | Out-Null
            [IO.File]::Copy($destination, $backup, $false)
            if ((Get-Sha $destination) -ne (Get-Sha $backup)) { throw "Backup verification failed: $destination" }
            $snapshots[$op.path] = $backup
        } else { $snapshots[$op.path] = $null }
    }
    if ($Action -eq 'Install') {
        $resourceRecord = @{ product = 'Endfield Poser character faces'; schema = 1;
            files = @($resourceFiles.Values | Sort-Object name) }
        $resourceRecordPath = Get-Target ($backupRel + '\new-character-faces-install.json')
        [IO.Directory]::CreateDirectory((Split-Path -Path $resourceRecordPath -Parent)) | Out-Null
        $resourceRecord | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $resourceRecordPath -Encoding UTF8
        $operations[$operations.Count - 2].source = $resourceRecordPath
        $record = @{ product = 'Endfield Poser'; schema = 1; files = @($newFiles.ToArray()); installed_at = (Get-Date -Format o) }
        $recordPath = Get-Target ($backupRel + '\new-install.json')
        [IO.Directory]::CreateDirectory((Split-Path -Path $recordPath -Parent)) | Out-Null
        $record | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $recordPath -Encoding UTF8
        $operations[$operations.Count - 1].source = $recordPath
    }
    $written = [Collections.Generic.List[object]]::new()
    try {
        foreach ($op in $operations) {
            Assert-GameStopped
            $destination = Get-Target $op.path
            if ($op.source) {
                if ($op.ContainsKey('hash') -and (Get-Sha $op.source) -ne $op.hash) { throw 'Source changed during installation.' }
                Set-FileAtomically $op.source $destination
                $written.Add($op)
                if ((Get-Sha $op.source) -ne (Get-Sha $destination)) { throw "Write verification failed: $destination" }
            } elseif (Test-Path -LiteralPath $destination) {
                Remove-Item -LiteralPath $destination -Force
                $written.Add($op)
            }
        }
    } catch {
        $failure = $_
        for ($i = $written.Count - 1; $i -ge 0; --$i) {
            $relative = $written[$i].path
            try {
                $destination = Get-Target $relative
                if ($snapshots[$relative]) { Set-FileAtomically $snapshots[$relative] $destination }
                elseif (Test-Path -LiteralPath $destination) { Remove-Item -LiteralPath $destination -Force }
            } catch { Write-Warning "Rollback incomplete for $relative. Recover from $backupRel. $($_.Exception.Message)" }
        }
        throw $failure
    }
    Write-Host "完成：$Action。配置、布局、自定义校准、预设和姿态均保留。"
    Write-Host "备份目录：$(Get-Target $backupRel)"
    if ($Action -eq 'Install') {
        if ($dllWrites -eq 0) { Write-Host '插件 DLL 已与本安装包一致，无需替换；安装记录已同步。' }
        Write-Host "角色表情校准：内置 $($resources.Count) 份，复制或更新 $resourceWrites 份，保留冲突文件 $resourceKept 份。"
        Write-Host '安装完成后启动游戏；启动方式见 README。L 面板；P 冻结；Ctrl+F5/F6/F7/F8 播放/暂停/停止/重置。'
    }
} catch {
    Write-Verbose $_.ScriptStackTrace
    Write-Error -ErrorAction Continue $_.Exception.Message
    exit 1
}
