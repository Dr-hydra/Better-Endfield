[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$repo = (Resolve-Path (Join-Path $PSScriptRoot '../../../../..')).Path
. (Join-Path $repo 'scripts/Workspace.ps1')
$workspace = Get-BEWorkspace
$controller = Join-Path $PSScriptRoot 'capture_control.py'
if (Get-Process Endfield -ErrorAction SilentlyContinue) {
    throw 'Exit the game normally before removing the loaded temporary proxy and restoring settings.'
}
$cleanup = @'
from pathlib import Path
import json, sys
runtime=Path(sys.argv[1]).resolve(); game=Path(sys.argv[2]).resolve()
source=runtime/'payloads/xinput1_4.dll'; target=game/'xinput1_4.dll'; manifest=game/'BetterEndfieldNext.xinput.install.json'
if target.exists() or manifest.exists():
    if not target.is_file() or not manifest.is_file(): raise SystemExit('Incomplete ownership evidence; proxy preserved')
    record=json.loads(manifest.read_text(encoding='utf-8-sig'))
    if record.get('Product')!='BetterEndfieldNext.XInputProxy' or Path(record.get('InstallRoot','')).resolve()!=runtime:
        raise SystemExit('Proxy ownership changed; files preserved')
    if target.read_bytes()!=source.read_bytes(): raise SystemExit('Proxy bytes changed; files preserved')
    target.unlink(); manifest.unlink()
    for name in ['BetterEndfieldNext-xinput1_4-proxy.loaded','BetterEndfieldNext-xinput1_4-host.status']:
        (game/name).unlink(missing_ok=True)
print('Temporary capture proxy removed; game executable/assets unchanged')
'@
& $workspace.tools.python -c $cleanup (Join-Path $repo 'build/bem-capture-1.6.3') $workspace.game.install_dir
if ($LASTEXITCODE -ne 0) { throw 'Capture proxy cleanup failed; inspect the retained files.' }
& $workspace.tools.python $controller stop
if ($LASTEXITCODE -ne 0) { throw 'Capture stop failed.' }
& $workspace.tools.python $controller restore
if ($LASTEXITCODE -ne 0) { throw 'Settings restoration requires review.' }
