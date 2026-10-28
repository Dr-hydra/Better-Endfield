#!/usr/bin/env python3
"""Extract current playable characters once; emit BC7 and ASTC preview archives."""
import argparse
import importlib.util
import json
import re
import subprocess
import sys
import types
import zlib
from dataclasses import asdict
from pathlib import Path

REPO=Path(__file__).resolve().parents[1]
sys.path.insert(0,str(REPO/'tools/CustomModel'))
import extract_native_bundles as extract
import RefreshEndfieldResourceInputs as inputs

def run(command, log):
    log.parent.mkdir(parents=True,exist_ok=True)
    with log.open('w',encoding='utf-8') as output:
        subprocess.run([str(x) for x in command],stdout=output,stderr=subprocess.STDOUT,check=True)

def main():
    ap=argparse.ArgumentParser(description=__doc__)
    ap.add_argument('--workspace-config',type=Path)
    ap.add_argument('--game-path',type=Path,required=True)
    ap.add_argument('--manifest',type=Path,required=True)
    ap.add_argument('--prefabs',type=Path,required=True)
    ap.add_argument('--output',type=Path,required=True)
    ap.add_argument('--astcenc',type=Path,required=True)
    ap.add_argument('--character',action='append',help='Limit a diagnostic run; omit for every playable character')
    ap.add_argument('--scope',choices=['head','character'],default='character')
    args=ap.parse_args()
    from workspace_config import load_workspace
    ws=load_workspace(args.workspace_config,REPO)
    game=args.game_path.resolve();root=args.output.resolve();root.mkdir(parents=True,exist_ok=True)
    reader=ws.path('tools.native_asset_reader')
    capability=subprocess.run([str(reader)],capture_output=True,text=True,encoding='utf-8',errors='replace')
    if '--preview-assets' not in capability.stdout+capability.stderr:
        # Workspaces may still point at the older identity-only executable.
        # Rebuild the repository reader against that same provisioned backend.
        target=ws.path('paths.build')/'resource-tools/native-asset-reader'
        run([ws.path('tools.dotnet'),'build',REPO/'tools/CustomModel/NativeAssetReader/NativeAssetReader.csproj',
            '-c','Release','-p:AnimeStudioDir='+str(reader.parent),'-o',target],root/'reader-build.log')
        reader=target/'NativeAssetReader.exe'
    manifest=json.loads(args.manifest.read_text(encoding='utf-8-sig'))
    available={a['path'].lower() for a in manifest['Assets']}
    chars=sorted(p.stem.removeprefix('npc_') for p in args.prefabs.glob('npc_chr_*.json'))
    if args.character:
        if not set(args.character)<=set(chars):raise ValueError('Unknown playable character')
        chars=sorted(set(args.character))
    stub=types.ModuleType('config');stub.get_game_dir=lambda:str(game);sys.modules['config']=stub
    unpacker=inputs.load_module('preview_distribution_vfs',ws.path('tools.endfield_unpacker_root')/'decrypt_vfs.py')
    unpacker.crc32=lambda data:(zlib.crc32(data)+2**31)%2**32-2**31
    roots=[('StreamingAssets',game/'Endfield_Data/StreamingAssets/VFS'),('Persistent',game/'Endfield_Data/Persistent/VFS')]
    records={r.relative_path.lower():r for r in inputs.select_overlay(unpacker,roots,re.compile(r'Bundles/Windows/.*[.]ab',re.I))}
    report=dict(manifestVersion=manifest.get('Version'),
        sourceGameVersion=ws.get('game.version'),
        scope=args.scope,requested=chars,completed=[],failures=[],geometrySource='Windows',
        androidCompatibility='shared Windows geometry; textures transcoded to ASTC; no Android extraction')
    for char in chars:
        folder=root/char;folder.mkdir(exist_ok=True)
        try:
            requested=[f'assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/{char}_postmodel.prefab',
                f'assets/beyond/dynamicassets/gameplay/prefabs/uimodels/{char}_uimodel.prefab']
            present=[p for p in requested if p in available]
            if not present:raise ValueError('No playable world/UI prefab in the current manifest')
            assets,names=extract.select_bundles(manifest,None,present)
            snapshot=dict(schema=1,character=char,manifest_version=manifest.get('Version'),assets=assets,
                bundles=[asdict(records[n.lower()]) for n in names],scope=args.scope)
            source=folder/'source';source.mkdir(exist_ok=True)
            marker=source/'extraction.json'
            previous=json.loads(marker.read_text(encoding='utf-8')) if marker.exists() else None
            if previous is not None and previous!=snapshot:
                raise ValueError('Snapshot changed; choose a new output directory')
            graph=folder/'graph.json';payloads=folder/'payloads';dest=folder/'packs'
            if not graph.exists():
                inputs.extract_overlay(unpacker,roots,source,re.compile(''),[records[n.lower()] for n in names])
                marker.write_text(json.dumps(snapshot,ensure_ascii=False,indent=2),encoding='utf-8')
                run([reader,source,graph,'--preview-assets',payloads],folder/'reader.log')
            pc=dest/(char+'-head-pc-2048.zip');mobile=dest/(char+'-head-android-2048.zip')
            if not (pc.exists() and mobile.exists() and (dest/'size-report.json').exists()):
                if dest.exists():
                    import shutil
                    shutil.rmtree(dest)
                run([sys.executable,'-X','utf8',REPO/'tools/CustomModel/build_native_head_preview.py',
                    '--graph',graph,'--payloads',payloads,'--character',char,'--output',dest,
                    '--astcenc',args.astcenc,'--scope',args.scope,'--variants','pc-2048','android-2048'],folder/'pack.log')
            report['completed'].append(dict(character=char,windows=str(pc),android=str(mobile)))
            print('OK '+char,flush=True)
        except Exception as error:
            report['failures'].append(dict(character=char,error=str(error)))
            print('FAILED '+char+': '+str(error),flush=True)
        (root/'report.json').write_text(json.dumps(report,ensure_ascii=False,indent=2),encoding='utf-8')
    print(json.dumps(dict(requested=len(chars),completed=len(report['completed']),failed=len(report['failures']))))
    return 1 if report['failures'] else 0

if __name__=='__main__':raise SystemExit(main())
