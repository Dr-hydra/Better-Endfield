#!/usr/bin/env python3
"""Stage external core / preview packs. No game extraction, upload, or hashes."""
import argparse
import json
import re
import shutil
import tempfile
import zipfile
from pathlib import Path, PurePosixPath

REPO = Path(__file__).resolve().parents[1]

def read(path):
    return json.loads(Path(path).read_text(encoding='utf-8-sig'))

def write(path, value):
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2)+'\n', encoding='utf-8')

def relative(name):
    p = PurePosixPath(name)
    if not name or '\\' in name or ':' in name or p.is_absolute() or any(x in ('', '.', '..') for x in name.split('/')):
        raise ValueError('Invalid resource path: '+name)
    return p

def verify(root):
    m = read(root/'manifest.json')
    if m['schemaVersion'] != 1 or m['kind'] != 'betterendfield-resource-pack' or m['platform'] not in ('windows','android') or m['packageType'] not in ('core','preview') or not re.fullmatch(r'[0-9]{1,9}([.][0-9]{1,9}){3}',m['version']):
        raise ValueError('Unsupported resource manifest')
    actual = {}
    for f in root.rglob('*'):
        if f.is_symlink(): raise ValueError('Resource links are not allowed')
        if f.is_file() and f.relative_to(root).as_posix() != 'manifest.json': actual[f.relative_to(root).as_posix()] = f.stat().st_size
    for name, size in m['files'].items():
        relative(name)
        if not isinstance(size,int) or size < 0: raise ValueError('Invalid file size')
    if actual != m['files']: raise ValueError('Resource inventory mismatch')
    if m['packageType']=='preview':
        index=read(root/'characters.json')
        if index['schemaVersion'] != 1: raise ValueError('Unsupported character index')
        identities=set()
        for entry in index['characters']:
            folder=root/relative(entry['path']); pack=read(folder/'pack.json')
            if pack['character']!=entry['character']: raise ValueError('Wrong character')
            for binding in entry['assetPaths']+entry['resourceRoots']:
                key=(entry['character'],binding)
                if key in identities: raise ValueError('Ambiguous preview identity')
                identities.add(key)
            for mesh in pack['meshes']:
                for name in mesh['payloads'].values():
                    if not (folder/relative(name)).is_file(): raise ValueError('Missing mesh payload')
            for t in pack['textures']:
                if not (folder/relative(t['payload'])).is_file(): raise ValueError('Missing texture payload')
                if m['platform']=='android' and t['format'] not in ('ASTC_6x6','RGBA32'): raise ValueError('Expected ASTC Android textures')
                if m['platform']=='windows' and t['format'] not in ('BC7','RGBA32'): raise ValueError('Expected BC7 Windows textures')
    return m

def stage(output, platform, package_type, version, files, source, preview_archives=()):
    if not re.fullmatch(r'[0-9]{1,9}([.][0-9]{1,9}){3}',version): raise ValueError('Four-part resource version required')
    parent=output/package_type/platform; parent.mkdir(parents=True,exist_ok=True)
    with tempfile.TemporaryDirectory(prefix='.staging-', dir=parent) as temporary:
        root=Path(temporary)
        for name, origin in files.items():
            target=root/relative(name);target.parent.mkdir(parents=True,exist_ok=True)
            shutil.copyfile(origin,target)
        entries=[]
        for archive in preview_archives:
            with zipfile.ZipFile(archive) as z:
                pack=json.loads(z.read('pack.json')); char=pack['character']
                if not re.fullmatch(r'[A-Za-z0-9_-]+',char): raise ValueError('Invalid character ID')
                folder=root/'characters'/char/'default'
                if folder.exists(): raise ValueError('Duplicate character; use explicit variant indices')
                for item in z.infolist():
                    if item.is_dir(): continue
                    target=folder/relative(item.filename);target.parent.mkdir(parents=True,exist_ok=True)
                    if target.exists(): raise ValueError('Duplicate preview payload')
                    with z.open(item) as src,target.open('wb') as dst: shutil.copyfileobj(src,dst)
                entries.append(dict(character=char,variant='default',path=folder.relative_to(root).as_posix(),sourceSnapshot=pack.get('snapshot'),
                    resourceRoots=sorted({r['resource_root'] for r in pack['renderers']}),
                    assetPaths=sorted({p for r in pack['renderers'] for p in r.get('resource_asset_paths',[])})))
        if package_type=='preview':
            if not entries: raise ValueError('No preview entries')
            write(root/'characters.json',dict(schemaVersion=1,characters=entries))
        inventory={f.relative_to(root).as_posix():f.stat().st_size for f in sorted(root.rglob('*')) if f.is_file()}
        write(root/'manifest.json',dict(schemaVersion=1,kind='betterendfield-resource-pack',packageType=package_type,
            platform=platform,version=version,gameVersion='.'.join(version.split('.')[:3]),source=source,files=inventory))
        verify(root)
        target=parent/version
        if target.exists():
            verify(target)
            if any((target/p).read_bytes()!=f.read_bytes() for f in root.rglob('*') if f.is_file() for p in [f.relative_to(root)]) or set(p.relative_to(target) for p in target.rglob('*') if p.is_file())!=set(p.relative_to(root) for p in root.rglob('*') if p.is_file()):
                raise ValueError('Existing resource version differs; increment its revision: '+str(target))
        else: root.rename(target)
    return target

def core_files(platform, outputs):
    def output(key, fallback): return Path(outputs.get(key,REPO/fallback))
    preset=output('character_presets','resources/generated/model/character-presets.json')
    voice=output('voice_index','resources/generated/voice/voice-catalog-index.json')
    if platform=='android':
        preset=REPO/'android/resources/character-presets.json';voice=REPO/'android/resources/voice-catalog-index.json'
    files={'model/character-presets.json':preset,'voice/voice-catalog-index.json':voice,
        'model/character-names.json':REPO/('android/resources/character-names.json' if platform=='android' else 'ui/BetterEndfieldNext.UI/Assets/model/character-names.json'),
        }
    for file in (REPO/'native/modules/actions/assets').glob('pose_*.bin'):files['actions/'+file.name]=file
    if platform=='windows':
        files['model/character-names-en.json']=REPO/'ui/BetterEndfieldNext.UI/Assets/model/character-names-en.json'
        files['combat/combat-dictionary.json']=output('combat_dictionary','resources/generated/combat/combat-dictionary.json')
        manifests=Path(outputs.get('manifests',REPO/'manifests'))
        for name in ('combat-semantics.besem','buff-sources.bemap'): files['combat/'+name]=manifests/'combat'/name
        for file in (REPO/'native/modules/combat_stats/assets/avatars').glob('*.png'):files['icons/avatars/'+file.name]=file
        for file in (REPO/'web/public/icons/items').glob('wpn_*.png'):files['icons/items/'+file.name]=file
    else:
        files['model/android-normal-rules.json']=REPO/'android/resources/android-normal-rules.json'
    return files

def main():
    ap=argparse.ArgumentParser(description=__doc__)
    ap.add_argument('--platform',choices=['windows','android'],required=True)
    ap.add_argument('--type',choices=['core','preview'],required=True)
    ap.add_argument('--output',type=Path,required=True)
    ap.add_argument('--version',help='Four parts; defaults to the confirmed platform input version plus .1')
    ap.add_argument('--workspace-config',type=Path)
    ap.add_argument('--character-presets',type=Path)
    ap.add_argument('--voice-index',type=Path)
    ap.add_argument('--combat-dictionary',type=Path)
    ap.add_argument('--preview-archive',action='append',type=Path,default=[])
    ap.add_argument('--preview-report',type=Path,help='Complete BuildPreviewResources.py report; fails closed on missing characters')
    ap.add_argument('--archives',type=Path,help='Optionally write a distributable ZIP alongside staged resources')
    args=ap.parse_args()
    from workspace_config import load_workspace
    ws=load_workspace(args.workspace_config,REPO)
    outputs={k:ws.path('resource_update.outputs.'+k) for k in ('character_presets','voice_index','combat_dictionary','manifests')}
    for key,value in [('character_presets',args.character_presets),('voice_index',args.voice_index),('combat_dictionary',args.combat_dictionary)]:
        if value is not None: outputs[key]=value
    source=read(REPO/'android/resources/resource-source.json') if args.platform=='android' else dict(gameVersion=ws.get('game.version'),platform='Windows')
    game=source.get('provenance',{}).get('gameVersion') or source.get('gameVersion')
    if not game or not re.fullmatch(r'[0-9]+[.][0-9]+[.][0-9]+',game): raise ValueError('Confirmed source game version required')
    revisions=read(REPO/'config/resource-packs.json')
    if revisions['schemaVersion']!=1: raise ValueError('Unsupported resource revision registry')
    revision=revisions['revisions'].get(args.type,{}).get(args.platform,{}).get(game,1)
    version=args.version or game+'.'+str(revision)
    files=core_files(args.platform,outputs) if args.type=='core' else {}
    archives=args.preview_archive
    preview_report=None
    if args.preview_report:
        if args.type!='preview' or archives:raise ValueError('Use one preview input mode')
        preview_report=read(args.preview_report)
        completed=preview_report['completed']
        if preview_report['failures'] or not completed or {e['character'] for e in completed}!=set(preview_report['requested']):
            raise ValueError('Preview generation is incomplete; resolve every character before packaging')
        archives=[Path(e[args.platform]) for e in completed]
    if args.type=='preview' and not archives:archives=[REPO/'resources/bem-preview'/('native-head-pc.zip' if args.platform=='windows' else 'native-head-android.zip')]
    # Preserve complete provenance on disk, but never ship local machine paths.
    provenance=dict(gameVersion=game,platform=args.platform,generator='BuildResourcePacks.py',coverage='existing inputs')
    if args.type=='preview':
        provenance=dict(generator='BuildResourcePacks.py',coverage='explicit archive contents; defaults to Liino only',
            sourceSnapshots='recorded per character',targetCompatibility='exact identity matching required; not a full-platform validation claim')
        if preview_report:
            provenance.update(coverage='all playable characters in current source manifest',characterCount=len(preview_report['requested']),
                geometrySource='Windows',sourceGameVersion=preview_report.get('sourceGameVersion'),
                manifestVersion=preview_report['manifestVersion'],geometryScope=preview_report['scope'],
                textureEncoding='BC7 authored mips' if args.platform=='windows' else 'offline ASTC 6x6 transcoding from Windows textures')
    target=stage(args.output,args.platform,args.type,version,files,provenance,archives)
    if args.archives:
        args.archives.mkdir(parents=True,exist_ok=True)
        dest=args.archives/('BE-'+args.type.title()+'-'+args.platform.title()+'-'+version+'.zip')
        with zipfile.ZipFile(dest,'w',zipfile.ZIP_DEFLATED,compresslevel=9) as z:
            for file in sorted(target.rglob('*')):
                if file.is_file():z.write(file,file.relative_to(target).as_posix())
    print(json.dumps(dict(path=str(target),platform=args.platform,packageType=args.type,version=version,hashes_calculated=False)))

if __name__=='__main__':main()
