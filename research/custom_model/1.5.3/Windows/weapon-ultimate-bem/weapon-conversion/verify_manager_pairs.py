"""Read real packages through the compiled production C# metadata service.

No installed library, runtime.ini, game process, device or release is changed.
The LOD1 probe participates only in metadata-conflict checks, not activation.
"""
from __future__ import annotations

import json
from pathlib import Path
import subprocess

HERE = Path(__file__).resolve().parent
REPO = HERE.parents[5]


def quote(value):
    return "'" + str(value).replace("'", "''") + "'"


def main():
    assembly = REPO / 'build/dotnet/ManagerChecks/AnyCPU/bin/Debug/net9.0/ManagerChecks.dll'
    if not assembly.is_file():
        raise FileNotFoundError('Build tools/CustomModel/ManagerChecks first')
    files = [Path('G:/zmd_bem/成品/庄方宜/庄方宜-大招状态-终极状态 by 幽魂小猫.bem'),
             Path('G:/zmd_bem/成品/庄方宜/庄方宜-心灵+大招形态.bem'),
             HERE.parent / 'efmi-conversion/outputs/M0178-zhuangfy-ultimate-extension.bem',
             HERE.parent / 'efmi-conversion/outputs/M0184-zhuangfy-ultimate-extension.bem',
             REPO / 'build/bem14/samples/Nait3D-GaeBolg-weapon-only-Windows-LOD0.bem',
             REPO / 'build/bem14/samples/probes/Nait3D-GaeBolg-weapon-only-Windows-LOD1.bem']
    for path in files:
        if not path.is_file():
            raise FileNotFoundError(path)
    output = HERE / 'actual-manager-pairs.json'
    script = r'''
$ErrorActionPreference = 'Stop'
$assembly = [Reflection.Assembly]::LoadFrom(ASSEMBLY)
$method = $assembly.GetType('BetterEndfield.UI.Services.BemPackageService').GetMethod('ReadMetadata', [Reflection.BindingFlags]'Public,Static')
$files = @(FILES)
$packages = @($files | ForEach-Object { $method.Invoke($null, @((Resolve-Path -LiteralPath $_).Path, $false)) })
$pairs = @()
for ($i = 0; $i -lt $packages.Count; $i++) {
    for ($j = $i + 1; $j -lt $packages.Count; $j++) {
        $value = $packages[$i].GetType().GetMethod('ConflictsWith').Invoke($packages[$i], @($packages[$j]))
        $expected = ($i -eq 0 -and $j -eq 1) -or ($i -eq 2 -and $j -eq 3) -or ($i -eq 4 -and $j -eq 5)
        if ($value -ne $expected) { throw "Unexpected conflict for pair $i,$j" }
        $pairs += [ordered]@{ first = $packages[$i].Id; second = $packages[$j].Id; conflicts = $value }
    }
}
[ordered]@{ parser = 'production BemPackageService'; package_count = $packages.Count; pairs = $pairs; passed = $true; user_library_modified = $false } | ConvertTo-Json -Depth 6 | Set-Content -LiteralPath OUTPUT -Encoding utf8
'''.replace('ASSEMBLY', quote(assembly)).replace('FILES', ','.join(quote(p) for p in files)).replace('OUTPUT', quote(output))
    subprocess.run(['pwsh', '-NoProfile', '-NonInteractive', '-Command', script], check=True, cwd=REPO)
    result = json.loads(output.read_text(encoding='utf-8-sig'))
    summary_path = HERE / 'summary.json'
    summary = json.loads(summary_path.read_text(encoding='utf-8'))
    summary['actual_manager_validation'] = dict(package_count=result['package_count'], pair_count=len(result['pairs']),
        passed=result['passed'], ordinary_and_ultimate_coexist=True, distinct_ultimate_packages_conflict=True,
        weapon_and_character_coexist=True, weapon_lod_variants_conflict=True, activated=False, user_library_modified=False)
    summary_path.write_text(json.dumps(summary, ensure_ascii=False, indent=2), encoding='utf-8')
    print('PASS production C# metadata: 6 real packages, 15 conflict pairs; no activation')


if __name__ == '__main__':
    main()
