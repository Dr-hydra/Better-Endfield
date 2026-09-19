"""Extract shared bundle closure once, then parse each captured role separately."""
import argparse
import json
import os
import re
import subprocess
import sys
import types
import zlib
from pathlib import Path
from extract_native_bundles import load, select_bundles
from runtime_native_probe import read, write


def main():
    p=argparse.ArgumentParser(description=__doc__)
    for flag in ('game','unpacker','resconv','reader','output'):p.add_argument('--'+flag,type=Path,required=True)
    p.add_argument('--summaries',type=Path,nargs='+',required=True)
    a=p.parse_args();out=a.output.resolve();out.mkdir(parents=True,exist_ok=True)
    summaries=[read(s) for s in a.summaries]
    versions={s['manifest_version'] for s in summaries}
    if len(versions)!=1:raise ValueError('cannot join different game snapshots')
    characters=sorted({c for s in summaries for c in s['characters_observed']})
    helpers=load('sweep_vfs_helpers',Path(__file__).resolve().parents[2]/'scripts/RefreshEndfieldResourceInputs.py')
    stub=types.ModuleType('config');stub.get_game_dir=lambda:str(a.game);sys.modules['config']=stub
    unpacker=load('sweep_vfs_reader',a.unpacker/'decrypt_vfs.py')
    unpacker.crc32=lambda data:(zlib.crc32(data)+2**31)%2**32-2**31
    roots=[('StreamingAssets',a.game/'Endfield_Data/StreamingAssets/VFS'),('Persistent',a.game/'Endfield_Data/Persistent/VFS')]
    common=out/'shared-inputs';common.mkdir(exist_ok=True)
    helpers.extract_overlay(unpacker,roots,common,re.compile(r'^Bundles/Windows/manifest\.hgmmap$',re.I))
    manifest_file=common/'Bundles/Windows/manifest.hgmmap'
    subprocess.run([str(a.resconv),str(manifest_file)],check=True,stdout=subprocess.DEVNULL)
    manifest=read(manifest_file.with_suffix('.json'))
    if {manifest.get('Version')}!=versions:raise ValueError('installed game snapshot differs from runtime request')
    plans={c:select_bundles(manifest,c) for c in characters}
    names={n for _,bundles in plans.values() for n in bundles}
    print(f'Extracting {len(names)} unique bundles for {len(characters)} captured characters',flush=True)
    rows=helpers.extract_overlay(unpacker,roots,common,re.compile('^(?:'+'|'.join(map(re.escape,sorted(names)))+')$',re.I))
    by_name={r.relative_path.lower():r for r in rows}
    if {n.lower() for n in names}-set(by_name):raise ValueError('bundle closure incomplete')
    del manifest
    results=[]
    for c,(assets,bundles) in plans.items():
        folder=out/c;inputs=folder/'inputs';inputs.mkdir(parents=True,exist_ok=True)
        snapshot=dict(schema=1,character=c,manifest_version=next(iter(versions)),perforce_cl=summaries[0].get('perforce_cl',''),
            assets=assets,bundles=[dict(path=n,layer=by_name[n.lower()].layer,size=by_name[n.lower()].size) for n in bundles],missing=[])
        write(inputs/'extraction.json',snapshot)
        for name in bundles:
            src=common/Path(by_name[name.lower()].relative_path);dest=inputs/Path(name)
            dest.parent.mkdir(parents=True,exist_ok=True)
            if not dest.exists():os.link(src,dest)
        raw=folder/'raw.json';database=folder/'database.json'
        with (folder/'reader.log').open('w',encoding='utf-8') as log:
            subprocess.run(['dotnet',str(a.reader.resolve()),str(inputs),str(raw)],check=True,stdout=log,stderr=subprocess.STDOUT)
        from parse_native_models import parse
        db=parse(read(raw));write(database,db)
        result=dict(character=c,database=str(database),**db['summary'],missing_resources=db['missing_resources'])
        results.append(result);print(json.dumps(result),flush=True)
    write(out/'summary.json',dict(manifest_version=next(iter(versions)),characters=results))


if __name__=='__main__':main()
