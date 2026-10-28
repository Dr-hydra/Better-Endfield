#!/usr/bin/env python3
"""Read current Android metadata and voice Bank indexes through rooted ADB.

Stream audio payloads and installed game files are never modified or copied.
Source hashes are required by the existing audio snapshot input protocol.
"""
import argparse
import hashlib
import json
import re
import shlex
import struct
import subprocess
import sys
import types
import zlib
from dataclasses import asdict
from datetime import datetime,timezone
from pathlib import Path
import RefreshEndfieldResourceInputs as helpers

def main():
    ap=argparse.ArgumentParser(description=__doc__)
    ap.add_argument('--workspace-config',type=Path)
    ap.add_argument('--serial')
    ap.add_argument('--package',default='com.hypergryph.endfield')
    ap.add_argument('--output',type=Path,required=True)
    args=ap.parse_args()
    if not re.fullmatch(r'[a-zA-Z0-9_.]+',args.package):raise ValueError('Invalid package')
    from workspace_config import load_workspace
    ws=load_workspace(args.workspace_config);out=args.output.resolve();out.mkdir(parents=True,exist_ok=True)
    adb=[str(ws.path('tools.adb'))]+(['-s',args.serial] if args.serial else [])
    def shell(command):return subprocess.check_output(adb+['exec-out','su','-c',command],timeout=180)
    def read(path,offset,size):
        data=shell('dd if='+shlex.quote(path)+' bs=65536 skip='+str(offset)+' count='+str(size)+' iflag=skip_bytes,count_bytes 2>/dev/null')
        if len(data)!=size:raise IOError('Short device read: '+path+' '+str(len(data))+'/'+str(size))
        return data
    details=subprocess.check_output(adb+['shell','dumpsys','package',args.package],text=True)
    version=re.search(r'versionName=([0-9]+[.][0-9]+[.][0-9]+)',details)
    if not version:raise ValueError('Cannot identify installed Android game version')
    version=version.group(1);remote='/sdcard/Android/data/'+args.package+'/files/VFS'
    blcs=shell('find '+remote+" -name '*.blc'").decode().splitlines()
    if not blcs:raise ValueError('Current Android VFS is unavailable')
    local=out/'persistent-vfs';total=0
    for device in blcs:
        if not re.fullmatch(re.escape(remote)+r'/[0-9A-Fa-f]{8}/[0-9A-Fa-f]{8}[.]blc',device):raise ValueError('Unexpected VFS path')
        target=local/Path(device).parent.name/Path(device).name;target.parent.mkdir(parents=True,exist_ok=True)
        data=shell('cat '+shlex.quote(device));target.write_bytes(data);total+=len(data)
    stub=types.ModuleType('config');stub.get_game_dir=lambda:str(out);sys.modules['config']=stub
    unpacker=helpers.load_module('device_resource_vfs',ws.path('tools.endfield_unpacker_root')/'decrypt_vfs.py')
    unpacker.crc32=lambda data:(zlib.crc32(data)+2**31)%2**32-2**31
    records=helpers.select_overlay(unpacker,[('Persistent',local)],helpers.target_pattern('Android'))
    chunks=shell('find '+remote+" -name '*.chk'").decode().splitlines()
    active_chunks=set()
    for blc in local.glob('*/*.blc'):helpers.parse_blc_records(unpacker,blc,'Persistent',re.compile(''),active_chunks)
    chunks=[p for p in chunks if Path(p).stem.upper() in active_chunks]
    chunk_paths={Path(p).stem.upper():p for p in chunks}
    for record in records:
        device=chunk_paths[record.chunk];data=read(device,record.offset,record.size);total+=len(data)
        if record.encrypted:data=unpacker.per_file_decrypt(data,record.iv_seed)
        target=out/record.relative_path;target.parent.mkdir(parents=True,exist_ok=True);target.write_bytes(data)
    counts,excluded=helpers.convert_inputs(ws.path('tools.endfield_unpacker_root'),ws.path('tools.resconv'),out,'Android',ws.env())
    files=[dict(path=p.relative_to(out).as_posix(),size=p.stat().st_size) for p in out.rglob('*') if p.is_file() and 'persistent-vfs' not in p.parts]
    snapshot=dict(schemaVersion=1,platform='Android',gameVersion=version,overlayOrder=['Persistent'],counts=counts,
        device=args.serial,package=args.package,records=[asdict(r) for r in records if r.relative_path not in excluded],files=files,
        retrievedAtUtc=datetime.now(timezone.utc).isoformat())
    (out/'input-snapshot.json').write_text(json.dumps(snapshot,indent=2),encoding='utf-8')
    import GenerateResourceManifests as audio
    packages=[]
    for number,device in enumerate(chunks):
        prefix=read(device,0,12)
        if prefix[:4] not in audio.PCK_MAGICS:continue
        size=int(shell('stat -c %s '+shlex.quote(device)).decode().strip())
        length=struct.unpack_from('<I',prefix,4)[0]+8
        if not 24<=length<=64*1024*1024 or length>size:raise ValueError('Invalid device PCK header')
        data=read(device,0,length);total+=len(data)
        path=out/'audio'/(str(number)+'.pck');path.parent.mkdir(exist_ok=True);path.write_bytes(data)
        index=audio.parse_pck(path,None)
        if index.banks:
            if size>128*1024*1024:raise ValueError('Unexpected Bank payload size; selective capture required')
            data=read(device,0,size);total+=len(data);path.write_bytes(data)
        packages.append(dict(source=device,path=path.relative_to(out).as_posix(),size=size,readSize=len(data),
            sha256=hashlib.sha256(data).hexdigest().upper()))
    if not packages:raise ValueError('No current Android audio indexes found')
    (out/'pck-snapshot.json').write_text(json.dumps(dict(schemaVersion=1,platform='Android',gameVersion=version,
        packages=packages,missingLanguages=[]),indent=2),encoding='utf-8')
    print(json.dumps(dict(version=version,records=len(records),pcks=len(packages),readBytes=total,output=str(out))))

if __name__=='__main__':main()
