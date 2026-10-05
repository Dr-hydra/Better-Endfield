"""Extract exact weapon prefab closures and compare native EFMI identities.

Only native game resource CRC32C identities are calculated by the existing
reader. No output-artifact hashes, game edits, installs or source-mod scripts.
"""
from __future__ import annotations

import argparse
import json
import os
from pathlib import Path
import subprocess
import sys

HERE = Path(__file__).resolve().parent
REPO = HERE.parents[5]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--families', nargs='+', default=['sword', 'funnel', 'claym'])
    parser.add_argument('--game', type=Path, default=Path('E:/Endfield Game'))
    args = parser.parse_args()
    source_manifest = HERE.parent / 'inputs/Bundles/Windows/manifest.json'
    manifest = json.loads(source_manifest.read_text(encoding='utf-8-sig'))
    selected = sorted(a['path'] for a in manifest['Assets'] if '/prefabs/weapons/' in a['path']
                      and a['path'].endswith('.prefab') and any(Path(a['path']).stem.startswith('wpn_' + family + '_') for family in args.families))
    if not selected:
        raise ValueError('No exact prefab targets selected')
    name = '-'.join(args.families)
    inputs = HERE / ('inputs-' + name)
    native = HERE / ('native-' + name + '.json')
    env = dict(os.environ, PYTHONPATH=str(REPO / 'scripts'), PYTHONUTF8='1')
    if not (inputs / 'extraction.json').is_file():
        command = [sys.executable, str(REPO / 'tools/CustomModel/extract_native_bundles.py'), '--game', str(args.game),
                   '--asset-only', '--unpacker', str(REPO / 'toolchains/EndfieldUnpacker'),
                   '--resconv', str(REPO / 'toolchains/FkArkEnd/ResConv/bin/Release/net10.0/ResConv.exe'), '--output', str(inputs)]
        for path in selected:
            command += ['--extra-asset', path]
        with (HERE / ('extract-' + name + '.log')).open('w', encoding='utf-8') as log:
            subprocess.run(command, env=env, stdout=log, stderr=subprocess.STDOUT, check=True, cwd=REPO)
    extraction = json.loads((inputs / 'extraction.json').read_text(encoding='utf-8'))
    if extraction['manifest_version'] != manifest['Version']:
        raise ValueError('Snapshot changed; review source manifest and target selection before continuing')
    if not native.is_file():
        reader = REPO / 'build/dotnet/NativeAssetReader/AnyCPU/bin/Release/net8.0-windows/NativeAssetReader.exe'
        if not reader.is_file():
            reader = REPO / 'toolchains/native-asset-reader/identities/NativeAssetReader.exe'
        with (HERE / ('reader-' + name + '.log')).open('w', encoding='utf-8') as log:
            subprocess.run([str(reader), str(inputs), str(native), '--resource-identities'],
                           stdout=log, stderr=subprocess.STDOUT, check=True, cwd=REPO)
    graph = json.loads(native.read_text(encoding='utf-8-sig'))
    source = json.loads((HERE / 'source-inventory.json').read_text(encoding='utf-8'))
    matches, meshes, missing_identities = [], [], []
    for obj in graph['objects']:
        if obj['type'] != 'Mesh' or not obj.get('name', '').startswith('S_wpn_'):
            continue
        identity = obj.get('resource_identity', {})
        row = dict(id=obj['id'], name=obj['name'], vertex_count=obj['vertex_count'],
                   submeshes=obj['submeshes'], identity=identity, asset_paths=obj.get('asset_paths', []))
        meshes.append(row)
        if not identity:
            missing_identities.append(dict(id=obj['id'], name=obj['name'], error=obj.get('resource_identity_error')))
        token = identity.get('data_crc32c')
        if token in source['source_hash_references']:
            matches.append(dict(row, source_refs=source['source_hash_references'][token]))
    result = dict(schema=1, platform='windows-x64', snapshot=extraction['manifest_version'], graph=str(native),
                  selected_assets=selected, bundle_count=len(extraction['bundles']), mesh_count=len(meshes), meshes=meshes,
                  source_matches=matches, missing_identities=missing_identities, backend_errors=graph['backend_errors'],
                  artifact_hashes_computed=False, runtime_verified=False)
    output = HERE / ('identity-index-' + name + '.json')
    output.write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps(dict(report=str(output), assets=len(selected), bundles=result['bundle_count'], meshes=len(meshes),
                          matched_meshes=len(matches), matches=[dict(mesh=m['name'], hash=m['identity']['data_crc32c'],
                            sources=[r['ini'] for r in m['source_refs']]) for m in matches],
                          missing_identity_count=len(missing_identities)), ensure_ascii=False))


if __name__ == '__main__':
    main()
