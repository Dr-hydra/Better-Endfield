"""Creator CLI: inspect, convert, pack, validate BEM 1.0. Never executes source INI/shaders."""
from __future__ import annotations
import argparse
import contextlib
import io
import json
import sys
import tempfile
from pathlib import Path

import bem_v1 as bem
import bem_projects

TOOL_VERSION = "1.1.4"
from convert_efmi_poc import Source
from efmi_source import sections, analyze_source
from efmi_lod_source import ENTRY, inspect_lod_source
from source_classification import entry_family, classify


def inspect_source(path, explicit=None, *, prepared=None):
    path = Path(path)
    if path.suffix.lower() == '.bem':
        m, _ = bem.read_package(path, decode=False)
        return dict(format='BEMv1', package=m, entries=[], issues=[], conversion_ready=False)
    if path.is_file() and path.suffix.lower() not in ('.zip','.rar','.7z'):
        raise ValueError('ARCHIVE_FORMAT: 源 Mod 支持目录、ZIP、RAR 和 7z。')
    if path.suffix.lower() == '.zip' and bem_projects.has_bem_members(path):
        return bem_projects.inspect_bundle(path)
    src = Source(path)
    try:
        candidates=[]
        documents=[]
        for name in src.names:
            if not name.lower().endswith('.ini') or any(p.lower().startswith('disabled') or p.lower() in ('backup','backups') for p in Path(name).parts):
                continue
            sec=sections(src.read_exact(name).decode('utf-8-sig'))
            documents.append((name,sec))
            family=entry_family(sec)
            if family: candidates.append((name,family,sec))
        if explicit:
            candidates=[c for c in candidates if c[0]==explicit]
        result=dict(format='unknown', entries=[dict(ini=n,format=f) for n,f,_ in candidates], issues=[], conversion_ready=False)
        if len(candidates)!=1:
            result['issues'].append(dict(code='ENTRY_SELECTION',message=f'找到 {len(candidates)} 个入口；请在配方指定 ini。'))
            return result
        name,kind,sec=candidates[0]; result.update(format=kind,ini=name)
        result['automation']=classify(kind,documents)
        if kind=='mixed':
            result['issues'].append(dict(code='MIXED_ENTRY',message='混合入口需要专门适配。'))
            return result
        if kind=='hash-lod':
            result['analysis']=inspect_lod_source(src,name,sec)
            # The older inspector labels implementation requirements as blockers.
            # The unified tool supports these via profiles; only unresolved source
            # semantics should be described as unsupported.
            analysis=result['analysis']
            analysis['requirements']=analysis.pop('blockers')
            analysis['errors']=analysis['structural_errors']
            result['issues'].extend(dict(code='SOURCE_STRUCTURE',message=e) for e in analysis['structural_errors'])
        else:
            result['analysis']=analyze_source(src,name)
        if result['automation']['status']=='manual_only':
            result['issues'].extend(dict(code='MANUAL_ADAPTATION',message=r) for r in result['automation']['reasons'])
        elif kind=='component-n' and result['automation']['status']=='standard_candidate':
            from component_auto import prepare
            try:
                if any(n!=name and any(k.startswith(('TextureOverride','CommandList','Resource')) for k in s) for n,s in documents):
                    raise ValueError('COMPANION_INI: 多个活动 INI 共同定义资源或逻辑，需专门审阅。')
                builder, matching=prepare(src,name)
                result.update(conversion_ready=True,matching=matching)
                result['automation'].update(status='ready',conversion_ready=True,
                    note='已自动匹配角色资料并完成默认外观的完整结构校验；显示效果需实机确认。')
                result['analysis']['errors']=[]
                result['issues'].extend(matching['warnings'])
                if prepared is not None: prepared.append(builder)
            except (ValueError, KeyError, OSError) as exc:
                message=str(exc)
                result['issues'].append(dict(code=message.split(':',1)[0] if ':' in message else 'AUTO_CONVERSION',message=message))
        else:
            result['issues'].append(dict(code='AUTO_MAPPING_PENDING',message='此来源需要骨架与材质映射，暂未接入自动转换。'))
        return result
    finally:
        src.close()


def check_geometry(m, payloads):
    """Creator-side data checks; runtime independently checks selected dependencies."""
    import struct
    for mesh in m['meshes']:
        for s in mesh['streams']:
            bem.require(len(payloads[s['payload']]) == mesh['vertex_count']*s['stride'], 'Vertex stream length mismatch')
        raw=payloads[mesh['indices']]; size=mesh['index_size']
        bem.require(len(raw)==mesh['index_count']*size, 'Index buffer length mismatch')
        bem.require(all(i[0]<mesh['vertex_count'] for i in struct.iter_unpack('<H' if size==2 else '<I',raw)), 'Index outside vertices')
        skin=mesh['streams'][2]; stride=skin['stride']; buf=payloads[skin['payload']]
        bem.require(stride in (4,12), 'Unsupported skin stride')
        bem.require(all(b<len(mesh['bones']) for p in range(stride-4,len(buf),stride) for b in buf[p:p+4]), 'Bone index outside palette')
        if stride==12:
            bem.require(all(abs(sum(struct.unpack_from('<4H',buf,p))-65535)<=655
                            for p in range(0,len(buf),stride)), 'Skin weights must sum to 1 (tolerance 0.01)')
    for t in m['textures']:
        w,h,n=t['width'],t['height'],t['mips']
        pixel={4:4,63:1}.get(t['format'],0)
        astc = t['format'] in (48,49,50)
        bem.require(0<w<=32768 and 0<h<=32768 and (pixel or astc or (w%4==0 and h%4==0)) and 0<n<=16, 'Invalid texture dimensions')
        block=8 if t['format'] in (10,26) else 16
        bw=t['format']-44 if astc else 4
        size=(sum(max(w>>i,1)*max(h>>i,1)*pixel for i in range(n)) if pixel else
              sum(((max(w>>i,1)+bw-1)//bw)*((max(h>>i,1)+bw-1)//bw)*block for i in range(n)))
        bem.require(size<=64*1024**2 and len(payloads[t['payload']])==size, 'Texture mip payload mismatch')
    for a in m['appearances']:
        if 'preview' in a:
            raw=payloads[a['preview']]
            bem.require(len(raw)<=8*1024**2 and raw.startswith(b'\x89PNG\r\n\x1a\n'), 'Invalid PNG preview')
        textures=set(); resident=0
        for c in a['components']:
            if c['operation']!='replace': continue
            mesh=m['meshes'][c['mesh']]
            resident+=sum(len(payloads[s['payload']]) for s in mesh['streams'])+len(payloads[mesh['indices']])
            for d in mesh['draws']: textures.update(d['textures'])
        resident+=sum(len(payloads[m['textures'][t]['payload']]) for t in textures)
        bem.require(len(textures)<=32 and resident<=bem.LIMIT, 'Appearance exceeds 32 texture bindings / 512 MiB budget')


def convert(source, recipe_path, output):
    recipe_path=Path(recipe_path); recipe=bem.load_json(recipe_path); root=recipe_path.parent
    bem.require(recipe['schema']==1, 'Unsupported conversion recipe schema')
    def path(v):
        p=Path(v); return p if p.is_absolute() else root/p
    builder=None; evidence=[]
    for appearance in recipe['appearances']:
        source_path=path(appearance['source']) if appearance.get('source') else Path(source)
        profile=bem.load_json(path(appearance['profile'])) if appearance.get('profile') else None
        kind=appearance.get('format','auto')
        if kind=='auto':
            detected=inspect_source(source_path,appearance.get('ini'))
            bem.require(detected['format']!='unknown', 'ENTRY_SELECTION: 配方需选择入口 ini')
            bem.require(detected.get('automation',{}).get('status')!='manual_only',
                        'MANUAL_ADAPTATION: 源包含自定义 Shader、外部材质或其他特殊逻辑，不能自动转换；请使用经审阅的专用转换路线。')
            kind=detected['format']
        if kind=='reviewed-draws':
            from convert_reviewed_draws import prepare
            from hash_lod_lowering import convert as lower
            src=Source(source_path)
            try:
                reviewed=appearance['reviewed']
                normalized, ini, profile, ev=prepare(src,
                    bem.load_json(path(reviewed['recipe'])),bem.load_json(path(reviewed['database'])),
                    bem.load_json(path(reviewed['observations'])),bem.load_json(path(reviewed['native_textures'])),
                    path(reviewed['texture_dir']))
                raw, summary=lower(normalized,ini,profile)
                evidence.append(dict(appearance=appearance['id'],native_output=summary,evidence=ev))
            finally: src.close()
        elif kind=='hash-lod':
            from hash_lod_lowering import convert as lower
            from convert_hash_lod import select_ini
            bem.require(profile is not None, 'TARGET_PROFILE: 缺少已验证原生 profile')
            src=Source(source_path)
            try:
                raw, summary=lower(src,select_ini(src,appearance.get('ini')),profile)
                evidence.append(dict(appearance=appearance['id'],native_output=summary))
            finally: src.close()
        elif kind=='component-n':
            import convert_efmi_poc
            bem.require(profile is not None, 'TARGET_PROFILE: ComponentN 需要原生 profile 和显式 v24_draws')
            with tempfile.TemporaryDirectory(prefix='bem-convert-') as tmp:
                dest=Path(tmp)/'intermediate.bin'
                argv=[str(source_path),'-o',str(dest)]
                for key in ('ini','source_profile','material_profile'):
                    if appearance.get(key):
                        argv += ['--'+key.replace('_','-'), appearance[key] if key=='ini' else str(path(appearance[key]))]
                old=sys.argv
                try:
                    sys.argv=['convert_efmi_poc',*argv]
                    with contextlib.redirect_stdout(io.StringIO()): convert_efmi_poc.main()
                finally: sys.argv=old
                raw=dest.read_bytes()
        else:
            raise ValueError(f'UNSUPPORTED_FORMAT: {kind}')
        t=recipe['target']
        target=bem.target_from_profile(profile,t['character_id'],t['world_resource'],t['ui_resource'],t['profile_id'],t['revision'])
        if builder is None:
            p=recipe['package']; builder=bem.Builder(p['id'],p['name'],p['author'],p['version'],target)
        else:
            bem.require(builder.m['target']==target,'APPEARANCE_TARGET: 外观的目标契约不同，不能放入同包')
        builder.add_legacy(raw,profile,appearance['id'],appearance['name'])
        builder.m['appearances'][-1]['description']=appearance.get('description','')
        if appearance.get('preview'):
            data=path(appearance['preview']).read_bytes()
            bem.require(len(data)<=8*1024**2 and data.startswith(b'\x89PNG\r\n\x1a\n'), 'Preview must be PNG <= 8 MiB')
            builder.m['appearances'][-1]['preview']=builder.payload(data)
    bem.require(builder is not None,'No appearances')
    builder.m['default_appearance_id']=recipe.get('default_appearance_id',builder.m['default_appearance_id'])
    bem.validate_manifest(builder.m,len(builder.payloads)); check_geometry(builder.m,builder.payloads)
    builder.write(output)
    return dict(package=builder.m,evidence=evidence,size=Path(output).stat().st_size,
                conversion_ready=True,render_verified=False,issues=[])


def convert_automatic(source, output, ini=None):
    prepared=[]
    inspection=inspect_source(source,ini,prepared=prepared)
    bem.require(inspection.get('conversion_ready') and len(prepared)==1,
        '\n'.join(i['message'] for i in inspection.get('issues',[])) or 'AUTO_CONVERSION: 此来源暂不能自动转换')
    builder=prepared[0]; builder.write(output)
    return dict(package=builder.m,matching=inspection['matching'],issues=inspection['issues'],
        size=Path(output).stat().st_size,conversion_ready=True,render_verified=False)


def main(argv=None):
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('command',choices=['inspect','convert','validate','pack','unpack','bundle'])
    parser.add_argument('--version', action='version', version='BEM Tools '+TOOL_VERSION+' / BEM 1.0')
    parser.add_argument('source',type=Path)
    parser.add_argument('additional',type=Path,nargs='*',help='Additional BEM files for bundle only')
    parser.add_argument('--recipe',type=Path)
    parser.add_argument('--ini')
    parser.add_argument('-o','--output',type=Path)
    parser.add_argument('--report',type=Path)
    args=parser.parse_args(argv)
    result=dict(tool_version=TOOL_VERSION,format_version='1.0',command=args.command,source=str(args.source),success=False,conversion_ready=False,render_verified=False,issues=[])
    try:
        bem.require(not args.additional or args.command=='bundle', 'Additional inputs are only valid for bundle')
        if args.report:
            paths=[args.source,*args.additional]+([args.output] if args.output else [])+([args.recipe] if args.recipe else [])
            bem.require(all(args.report.resolve()!=p.resolve() for p in paths), 'Report cannot overwrite input/output/recipe')
        if args.command=='inspect': result.update(inspect_source(args.source,args.ini))
        elif args.command=='validate':
            m,payloads=bem.read_package(args.source); check_geometry(m,payloads)
            result.update(package=m,size=args.source.stat().st_size)
        elif args.command in ('pack','unpack','bundle'):
            bem.require(args.output, 'OUTPUT: 缺少输出路径')
            if args.command=='pack': result.update(bem_projects.pack_project(args.source,args.output))
            elif args.command=='unpack': result.update(bem_projects.unpack(args.source,args.output))
            else: result.update(bem_projects.bundle([args.source,*args.additional],args.output))
        else:
            bem.require(args.output,'OUTPUT: 请选择输出文件')
            bem.require(args.source.resolve()!=args.output.resolve(),'OUTPUT: 不能覆盖源文件')
            result.update(convert(args.source,args.recipe,args.output) if args.recipe else convert_automatic(args.source,args.output,args.ini))
        result['success']=True
    except Exception as exc:
        result['issues'].append(dict(code='CONVERSION_FAILED',message=str(exc),
            hint='核对入口、已验证角色 profile、骨骼/材质映射和源文件版本；特殊 Shader 需要审阅配方。'))
    encoded=json.dumps(result,ensure_ascii=False,indent=2,allow_nan=False)
    if args.report and all(args.report.resolve()!=p.resolve() for p in [args.source,*args.additional]+([args.output] if args.output else [])+([args.recipe] if args.recipe else [])):
        bem.atomic_write(args.report,encoded.encode('utf-8'))
    print(encoded)
    return 0 if result['success'] else 2


if __name__=='__main__':
    if hasattr(sys.stdout,'reconfigure'): sys.stdout.reconfigure(encoding='utf-8')
    raise SystemExit(main())
