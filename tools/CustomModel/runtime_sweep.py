"""Arm and summarize a clean-session, all-character resource metadata probe."""
import argparse
import datetime
import json
import os
import re
from pathlib import Path

from runtime_native_probe import write, read


def make_request(manifest, run, characters=None, persistent=False):
    if not re.fullmatch(r'[A-Za-z0-9_-]{1,80}',run): raise ValueError('invalid run ID')
    version=manifest.get('Version')
    if not isinstance(version,str) or not version: raise ValueError('manifest version missing')
    cl=str(manifest.get('perforceCL',''))
    selected=set(characters or [])
    if any(not re.fullmatch(r'chr_\d{4}_[a-z0-9]+',c) for c in selected):
        raise ValueError('invalid selected character ID')
    resources={}
    pattern=re.compile(r'assets/beyond/dynamicassets/gameplay/(?:prefabs/uimodels/(chr_\d{4}_[a-z0-9]+)_uimodel|actors/postmodels/characters/(chr_\d{4}_[a-z0-9]+)_postmodel)\.prefab')
    for asset in manifest['Assets']:
        path=asset['path'].lower()
        if not (m:=pattern.fullmatch(path)): continue
        if selected and (m[1] or m[2]) not in selected: continue
        root=path.rsplit('/',1)[1][:-7]
        if root in resources: raise ValueError('duplicate resource root')
        resources[root]=dict(character_id=m[1] or m[2],variant='ui' if m[1] else 'world',asset_path=path)
    if not 0<len(resources)<=512: raise ValueError('expected 1..512 character resources')
    if selected-{r['character_id'] for r in resources.values()}: raise ValueError('selected character missing from manifest')
    if any(c in version+cl for c in '\r\n\t'): raise ValueError('invalid provenance')
    header='BE_NATIVE_PROBE_SWEEP_PERSIST_V1' if persistent else 'BE_NATIVE_PROBE_SWEEP_V1'
    text='\n'.join([header,run,version,cl,*sorted(resources)])+'\n'
    return text,dict(schema=1,mode='sweep',run=run,manifest_version=version,perforce_cl=cl,
                     resources=resources,platform='windows-x64',persistent=persistent)


def records(path):
    if not path.exists(): return []
    # The native append is flushed per resource. Ignore only an in-flight final
    # line; malformed committed lines are real failures, not silently discarded.
    with path.open('rb') as f:
        result=[]
        for line in f:
            if not line.endswith(b'\n'): break
            if line.strip(): result.append(json.loads(line))
        return result


def summarize(metadata, rows):
    expected=metadata['resources']; results={};processes=set()
    for record in rows:
        if any(record.get(k)!=metadata[k] for k in ('run','manifest_version','perforce_cl')):
            raise ValueError('capture provenance mismatch')
        if record.get('mode')!='sweep' or record['resource_root'] not in expected:
            raise ValueError('unexpected resource/mode')
        processes.add(record['process_id'])
        if len(processes)>1: raise ValueError('cannot merge game processes in one session')
        root=record['resource_root']
        previous=results.get(root)
        if previous and previous['complete']: continue
        entries=record['renderers']
        paths=[r['path'] for r in entries]
        if len(set(paths))!=len(paths): raise ValueError('ambiguous renderer paths')
        if any(r['resource_root']!=root or not r['path'].startswith(root+'/') for r in entries):
            raise ValueError('renderer escaped resource root')
        complete=record['complete'] and len(entries)==record['renderer_count'] and bool(entries) and all(r['complete'] for r in entries)
        results[root]=dict(**expected[root],complete=bool(complete),renderers=len(entries),
            expected_renderers=record['renderer_count'],
            complete_renderers=sum(bool(r['complete']) for r in entries),
            incomplete_meshes=[r['mesh_name'] for r in entries if not r['complete']])
    return dict(run=metadata['run'],manifest_version=metadata['manifest_version'],process_ids=sorted(processes),
        captured=results,complete_resources=sum(r['complete'] for r in results.values()),
        incomplete_resources=[root for root,r in results.items() if not r['complete']],
        unobserved_resources=sorted(set(expected)-set(results)),
        characters_observed=sorted({r['character_id'] for r in results.values()}),
        note='未观察到不等于失败或未拥有；运行时元数据不包含 EFMI GPU hash，也不自动批准转换。')


def contract_signature(record):
    # Unity property IDs are process-local. Compare the reusable contract, keep
    # raw textures/property IDs in each original segment for later offline joins.
    return sorted([dict((k,r[k]) for k in ('path','mesh_name','original_index_count','vertex_count',
        'submesh_count','strides','attributes','bones','bindposes')) | {
        'materials':[(m['name'],m['shader']) for m in r['materials']]}
        for r in record['renderers']],key=lambda r:r['path'])


def summarize_campaign(metadata, sessions):
    result=summarize(metadata,[]);captured={};signatures={};conflicts=set();segments=[];processes=set()
    for filename,rows in sessions:
        part=summarize(metadata,rows)  # Still reject mixed processes within a file.
        segments.append(dict(file=filename,process_ids=part['process_ids'],resources=len(part['captured'])))
        processes.update(part['process_ids'])
        for root,entry in part['captured'].items():
            if root not in captured or entry['complete']: captured[root]=entry|{'session_file':filename}
        for record in rows:
            if not record['complete']:continue
            root=record['resource_root'];signature=contract_signature(record)
            if root in signatures and signatures[root]!=signature:conflicts.add(root)
            else:signatures[root]=signature
    for root in conflicts:captured[root]['complete']=False
    result.update(captured=captured,process_ids=sorted(processes),segments=segments,conflicting_resources=sorted(conflicts),
        complete_resources=sum(r['complete'] for r in captured.values()),
        incomplete_resources=sorted(root for root,r in captured.items() if not r['complete']),
        unobserved_resources=sorted(set(metadata['resources'])-set(captured)),
        characters_observed=sorted({r['character_id'] for r in captured.values()}))
    return result


def main():
    p=argparse.ArgumentParser(description=__doc__)
    p.add_argument('command',choices=['arm','status','stop'])
    p.add_argument('--manifest',type=Path)
    p.add_argument('--characters',nargs='+',help='Optional character IDs for a fresh supplement session')
    p.add_argument('--persistent',action='store_true',help='Keep collection armed across game restarts until stop')
    p.add_argument('--run')
    p.add_argument('--catalog',type=Path,default=Path(os.environ.get('LOCALAPPDATA','.'))/'BetterEndfield/catalog/custom-model')
    p.add_argument('--output',type=Path)
    a=p.parse_args();folder=a.catalog/'native-probe'
    if a.command=='arm':
        if not a.manifest:p.error('--manifest required')
        run=a.run or datetime.datetime.now().strftime('characters-%Y%m%d-%H%M%S-%f')
        if (folder/(run+'.request.json')).exists() or (folder/(run+'.jsonl')).exists():raise ValueError('run already exists')
        text,meta=make_request(read(a.manifest),run,a.characters,a.persistent)
        meta['manifest_file']=str(a.manifest.resolve())
        write(folder/(run+'.request.json'),meta)
        request=a.catalog/'native-probe.request';temp=request.with_suffix('.tmp')
        temp.write_text(text,encoding='utf-8');temp.replace(request)
        write(a.catalog/'native-probe.status.json',dict(schema=1,run=run,mode='sweep',persistent=a.persistent,stage='awaiting_game',process_id=0,expected_resources=len(meta['resources'])))
        print(json.dumps(dict(run=run,resources=len(meta['resources']),persistent=a.persistent,output=str(folder/(run+('.*' if a.persistent else '')+'.jsonl')))))
        return
    run=a.run or read(a.catalog/'native-probe.status.json')['run']
    if not re.fullmatch(r'[A-Za-z0-9_-]{1,80}',run):raise ValueError('invalid run ID')
    meta=read(folder/(run+'.request.json'))
    if meta.get('mode')!='sweep':raise ValueError('not a sweep session')
    if a.command=='stop':
        (a.catalog/'native-probe.stop').write_text(run,encoding='utf-8')
        request=a.catalog/'native-probe.request'
        if request.exists():
            lines=request.read_text(encoding='utf-8').splitlines()
            if len(lines)>1 and lines[1]==run:request.replace(a.catalog/(run+'.stopped'))
        print('Stop requested; wait for native status stopped. Saved Mod settings remain unchanged.')
    if meta.get('persistent'):
        paths=sorted(folder.glob(run+'.*.jsonl'))
        summary=summarize_campaign(meta,((p.name,records(p)) for p in paths))
    else:summary=summarize(meta,records(folder/(run+'.jsonl')))
    destination=a.output or folder/(run+'.summary.json');write(destination,summary)
    print(json.dumps(dict(run=run,characters=len(summary['characters_observed']),complete=summary['complete_resources'],
        incomplete=summary['incomplete_resources'],unobserved=len(summary['unobserved_resources']),summary=str(destination)),ensure_ascii=False))


if __name__=='__main__':main()
