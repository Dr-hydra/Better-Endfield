"""Catalog-backed ComponentN conversion. No per-Mod recipe or code execution.

Source component numbers are local labels. Only EFMI resource identities plus
native counts select catalog components. Unknown material/program semantics fail.
"""
import json
import hashlib
import re
import sys
import uuid
from pathlib import Path, PurePosixPath

import bem_v1 as bem
from convert_efmi_poc import parse_dds, parse_ini_constants
from efmi_source import sections, value
from hash_lod_lowering import condition, selected_draws

BASE = re.compile(r'TextureOverride_(?:EntryPoint_)?Component(\d+)$')


def catalogs():
    root = Path(sys.executable).parent if getattr(sys, 'frozen', False) else Path(__file__).parent
    return [c for p in sorted((root / 'catalog').glob('*.json'))
            if (c := bem.load_json(p)).get('kind') == 'bem-character-catalog']


def match_catalog(sec, database):
    entries = []
    for name, body in sec.items():
        if m := BASE.fullmatch(name):
            identity = (value(body, 'hash') or '').lower()
            if not re.fullmatch('[0-9a-f]{8}', identity): raise ValueError(f'SOURCE_IDENTITY: {name} 缺少有效 hash')
            count = value(body, 'match_index_count')
            first = value(body, 'match_first_index')
            if first not in (None, '0'): raise ValueError(f'SUBMESH_MAPPING: {name} 非零 first_index 需要专门映射')
            entries.append(dict(section=name, source_component=int(m[1]), hash=identity,
                                count=int(count) if count else None))
    if not entries: raise ValueError('SOURCE_IDENTITY: 没有 ComponentN 基础入口')
    if len({e['source_component'] for e in entries}) != len(entries): raise ValueError('SOURCE_IDENTITY: 重复组件入口')
    found = []
    for catalog in database:
        if catalog.get('schema') != 1 or catalog.get('kind') != 'bem-character-catalog' or not catalog.get('verified'):
            continue
        if catalog.get('platform') != 'windows-x64': continue
        if catalog.get('mapping_status')=='source-resource-identities-recheck-required': continue
        if all(e['hash'] in catalog['entries'] for e in entries): found.append(catalog)
    if len(found) != 1:
        incomplete=[c['name'] for c in database if c.get('mapping_status') in ('source-resource-identities-pending','source-resource-identities-recheck-required')]
        detail=(f' 已有 {len(incomplete)} 个角色的原生资料尚缺 EFMI 资源身份对应（如：'
                +'、'.join(incomplete[:3])+'）。这不是运行时采集缺失，需补齐游戏原资源与 EFMI hash 的对应。') if incomplete else ''
        if not incomplete:
            count=sum(c.get('kind')=='bem-character-catalog' and bool(c.get('entries')) for c in database)
            detail=f' 已载入 {count} 个角色的原资源对应；请核对游戏资源版本、来源 hash 规则或未支持的部件。'
        raise ValueError('CHARACTER_CATALOG: '+('没有覆盖这些资源身份的角色资料' if not found else '角色/资源版本匹配不唯一')+'；无需手选 JSON。'+detail)
    catalog = found[0]
    used = set()
    for e in entries:
        cid = catalog['entries'][e['hash']]
        if cid in used: raise ValueError('SOURCE_IDENTITY: 多个入口竞争同一原生部件')
        used.add(cid); e['target'] = cid
        native = catalog['components'][str(cid)]
        if e['count'] is not None and e['count'] != native['original_index_count']:
            raise ValueError(f'CHARACTER_REVISION: {e["section"]} hash 相同但原生索引数不同，需更新角色资料')
    return catalog, entries


def default_constants(sec):
    constants = {'$'+k: v for k, v in parse_ini_constants(sec.get('Constants', '')).items()}
    constants['$lod_level'] = 0
    for raw in sec.get('CommandListProcessToggles', '').splitlines():
        line = raw.split(';', 1)[0].strip()
        if not line: continue
        m = re.fullmatch(r'(\$draw_component_\w+)\s*=\s*(.+)', line)
        if not m: raise ValueError('STATIC_STATE: 无法静态解析开关逻辑: '+line)
        constants[m[1]] = int(condition(m[2], constants))
    return constants


def validate_entry(sec, entry):
    """Only the ordinary local-skeleton EFMI entry wrapper is understood."""
    lines=[l.split(';',1)[0].strip() for l in sec[entry['section']].splitlines()]
    lines=[l for l in lines if l]
    callback=None; opened=False; ended=False; dispatcher=None
    for line in lines:
        if re.fullmatch(r'(hash|match_index_count|match_first_index)\s*=\s*[0-9a-fA-F]+',line): continue
        if line=='$object_detected = 1': continue
        if re.fullmatch(r'if\s+\$mod_enabled\s*&&\s*DRAW_TYPE\s*==\s*4',line) and not opened:
            opened=True;continue
        if line=='endif' and opened and not ended: ended=True;continue
        if not opened or ended: raise ValueError('ENTRY_PROGRAM: 非标准入口语句 '+line)
        if re.fullmatch(r'\$\\EFMIv1\\component_id\s*=\s*'+str(entry['source_component']),line): continue
        if re.fullmatch(r'\$\\EFMIv1\\gpu_posed\s*=\s*1',line): continue
        if re.fullmatch(r'\$lod_level\s*=\s*0',line): continue
        if m:=re.fullmatch(r'CommandList\\EFMIv1\\Callback_Component_DrawCustom\s*=\s*ref\s+(CommandList\w+)',line):
            if callback: raise ValueError('DRAW_CALLBACK: 重复回调')
            callback=m[1];continue
        if m:=re.fullmatch(r'run\s*=\s*(CommandList\w+)',line):
            if dispatcher: raise ValueError('ENTRY_PROGRAM: 重复分发')
            dispatcher=m[1];continue
        raise ValueError('ENTRY_PROGRAM: 不支持的入口语句 '+line)
    if not (opened and ended and callback and dispatcher): raise ValueError('ENTRY_PROGRAM: 不完整的标准入口')
    program=[]
    for line in sec.get(dispatcher,'').splitlines():
        line=re.sub(r'\s+','',line.split(';',1)[0])
        if line: program.append(line)
    expected=['handling=skip',r'$\EFMIv1\component_count=$component_count',
        r'$\EFMIv1\identification_min_components=3',r'run=CommandList\EFMIv1\Object_ReadConfig',
        r'$\EFMIv1\lod_level=$lod_level',r'run=CommandList\EFMIv1\Component_ReadConfig',
        r'Pool\EFMIv1\Input_ObjectSpatialIdentity=refPool_ObjectSpatialIdentity',
        r'run=CommandList\EFMIv1\SpatialIdentity_IdentifyComponentInstances',r'run=CommandList\EFMIv1\Component_DrawInstances']
    if program!=expected: raise ValueError('ENTRY_PROGRAM: 分发流程超出已支持的 ComponentN 模板')
    return callback


def resource_bytes(src, ini, sec, name):
    if name not in sec: raise ValueError('RESOURCE: 缺少资源声明 '+name)
    filename = value(sec[name], 'filename')
    if not filename: raise ValueError('RESOURCE: 动态/空资源不能自动转换 '+name)
    path = PurePosixPath(filename.replace('\\', '/'))
    if path.is_absolute() or '..' in path.parts or ':' in filename:
        raise ValueError('RESOURCE: 资源路径越界')
    return src.read_exact(str(PurePosixPath(ini).parent / path))


def texture_table(src, ini, sec, catalog, builder):
    result, warnings, seen = {}, [], set()
    for name, body in sec.items():
        if not name.startswith('TextureOverride') or BASE.fullmatch(name) or re.fullmatch(r'TextureOverride_(?:EntryPoint_)?Component\d+_LOD\d+', name):
            continue
        identity = (value(body, 'hash') or '').lower()
        if identity in seen: raise ValueError('TEXTURE_MAPPING: 重复贴图覆盖 '+identity)
        seen.add(identity)
        # Only object-detection guard + a single static replacement is standard.
        resource = None; depth = 0
        for raw in body.splitlines():
            line = raw.split(';', 1)[0].strip()
            if not line: continue
            if re.fullmatch(r'(hash|match_priority)\s*=\s*[0-9a-fA-F-]+', line): continue
            if line == 'if $object_detected' and depth == 0: depth = 1; continue
            if line == 'endif' and depth == 1: depth = 0; continue
            if m := re.fullmatch(r'this\s*=\s*(?:ref\s+)?(Resource\S+)', line):
                if resource: raise ValueError('TEXTURE_STATE: 多个贴图状态不能自动选择')
                resource = m[1]; continue
            raise ValueError('TEXTURE_STATE: 不支持的贴图语句 '+line)
        if depth or not resource: raise ValueError('TEXTURE_STATE: 不完整的贴图覆盖 '+name)
        if identity in catalog.get('preserved_globals', {}):
            native=catalog['preserved_globals'][identity]
            w,h,mips,fmt,_,data=parse_dds(resource,resource_bytes(src,ini,sec,resource))
            if ((w,h,mips,fmt)!=(native['width'],native['height'],native['mips'],native['format']) or
                    hashlib.sha256(data).hexdigest()!=native['source_payload_sha256']):
                raise ValueError('GLOBAL_TEXTURE: 源包修改了雨雪全局贴图，超出 BEM 材质替换范围：'+identity)
            warnings.append(dict(code='PRESERVED_GLOBAL', message=f'{identity}: 源覆盖与已核实原生雨雪贴图一致，沿用游戏资源。'))
            continue
        mapping = catalog['textures'].get(identity)
        if mapping is None: raise ValueError('TEXTURE_MAPPING: 角色资料中没有贴图身份 '+identity)
        targets = [mapping['name'],*mapping.get('aliases',[])]
        if len(targets)!=len(set(targets)): raise ValueError('TEXTURE_MAPPING: 角色资料中贴图别名重复 '+identity)
        for target in targets:
            if target in result: raise ValueError('TEXTURE_MAPPING: 多个覆盖指向同一贴图 '+target)
        w,h,mips,fmt,srgb,data = parse_dds(resource, resource_bytes(src,ini,sec,resource))
        payload=builder.payload(data)
        for target in targets:
            result[target] = len(builder.m['textures'])
            builder.m['textures'].append(dict(width=w,height=h,mips=mips,format=fmt,srgb=srgb,
                original_name=target,payload=payload))
    return result, warnings


def prepare(src, ini, database=None):
    sec = sections(src.read_exact(ini).decode('utf-8-sig'))
    catalog, entries = match_catalog(sec, catalogs() if database is None else database)
    target = bem.target_from_profile(catalog,catalog['character_id'],catalog['world_resource'],
        catalog['ui_resource'],catalog['profile_id'],catalog['revision'])
    label = PurePosixPath(ini).parent.name or src.path.stem
    builder = bem.Builder('converted-'+uuid.uuid4().hex,label[:256], '未填写', '1.0',target)
    textures, warnings = texture_table(src,ini,sec,catalog,builder)
    constants = default_constants(sec)
    # Metadata belongs to this INI, not an arbitrary same-named file elsewhere.
    metadata_path = str(PurePosixPath(ini).parent/'Meshes/Components.buf')
    meta = json.loads(src.read_exact(metadata_path).decode('utf-8-sig')) if metadata_path in src.names else {}
    operations = [dict(target=i,operation='keep') for i in range(len(target['components']))]
    for entry in entries:
        cid = entry['target']; source_id = entry['source_component']; native = catalog['components'][str(cid)]
        callback = validate_entry(sec,entry)
        # Explicit original draw means keep, an empty callback means hide.
        lines = [x.split(';',1)[0].strip() for x in sec.get(callback,'').splitlines()]
        lines = [x for x in lines if x]
        if lines == ['drawindexed = INDEX_COUNT, FIRST_INDEX, 0']: continue
        draws = selected_draws(sec,callback,static_constants=constants)
        if not draws:
            operations[cid]['operation'] = 'hide'; continue
        if not native.get('layout_verified'): raise ValueError(f'NATIVE_LAYOUT: {native["mesh_name"]} 缺少运行时布局证据')
        if native.get('source_layout_supported') is False:
            raise ValueError(f'NATIVE_LAYOUT_UNSUPPORTED: {native["mesh_name"]} 已采集，但其顶点布局或多子网格尚不支持自动转换')
        streams = []; buffers = draws[0]['buffers']
        if set(buffers) not in ({'ib','vb0','vb1','vb2'}, {'ib','vb0','vb1','vb2','vb3'}):
            raise ValueError('VERTEX_BINDINGS: 需要三路完整原生顶点流')
        if buffers.get('vb3',buffers['vb0']) != buffers['vb0']: raise ValueError('VERTEX_BINDINGS: 非标准 vb3')
        for i, expected in enumerate(native['source_streams']):
            declaration = meta.get(f'Component{source_id}_VB{i}')
            allowed = [expected, *native.get('source_stream_aliases',{}).get(str(i),[])]
            if declaration is not None and declaration not in allowed:
                raise ValueError(f'VERTEX_LAYOUT: Component{source_id}/VB{i} 声明与角色资料不同')
            resource = buffers[f'vb{i}']; stride = value(sec[resource],'stride')
            if stride is None or int(stride) != native['strides'][i]:
                raise ValueError(f'VERTEX_LAYOUT: {resource} stride 与角色资料不同')
            streams.append(resource_bytes(src,ini,sec,resource))
        counts = [len(raw)//stride for raw,stride in zip(streams,native['strides'])]
        if len(set(counts)) != 1 or any(len(raw)%stride for raw,stride in zip(streams,native['strides'])):
            raise ValueError('VERTEX_LAYOUT: 三路顶点数不一致')
        fmt = value(sec[buffers['ib']],'format')
        size = {'DXGI_FORMAT_R16_UINT':2,'R16_UINT':2,'DXGI_FORMAT_R32_UINT':4,'R32_UINT':4}.get(fmt)
        if not size: raise ValueError('INDEX_FORMAT: 不支持的索引格式 '+str(fmt))
        indices = resource_bytes(src,ini,sec,buffers['ib'])
        if len(indices)%size: raise ValueError('INDEX_FORMAT: 索引缓冲截断')
        output_ib = bytearray(); output_draws = []; selected_chunks=[]
        for draw in draws:
            if draw['bindings'] or draw['buffers'] != buffers or draw['base'] != 0:
                raise ValueError('DRAW_STATE: 材质、缓冲切换或非零 base vertex 需要专门映射')
            start,count = draw['start'],draw['count']
            if count <= 0 or count%3 or start+count > len(indices)//size:
                raise ValueError('DRAW_RANGE: 绘制范围越界或不是三角形')
            chunk = indices[start*size:(start+count)*size]
            selected_chunks.append((count,chunk))
        # Unity repeats the final submesh for surplus materials. Replay all
        # selected triangles for slot 0 before slot 1 to preserve blending order.
        for slot, material in enumerate(native['materials']):
            for count,chunk in selected_chunks:
                output_draws.append(dict(start=len(output_ib)//size,count=count,material_component=cid,
                    material_slot=slot,material_name=material,
                    textures=list(dict.fromkeys(textures[n] for n in native['material_textures'][slot] if n in textures))))
                output_ib.extend(chunk)
        offsets = [0,0,0]; attributes = []; sizes = [4,2,1,1,2,2,1,1,2,2,4,4]
        for a in native['attributes']:
            attributes.append([*a,offsets[a[3]]]); offsets[a[3]] += sizes[a[1]]*a[2]
        operations[cid].update(operation='replace',mesh=len(builder.m['meshes']))
        builder.m['meshes'].append(dict(vertex_count=counts[0],index_count=len(output_ib)//size,index_size=size,
            indices=builder.payload(output_ib),streams=[dict(stride=s,payload=builder.payload(b)) for s,b in zip(native['strides'],streams)],
            attributes=attributes,bones=[dict(component=cid,index=i,name=n) for i,n in enumerate(native['bone_names'])],draws=output_draws))
    builder.m['default_appearance_id']='default'
    builder.m['appearances']=[dict(id='default',name='源包默认外观',description='按 INI 初始开关生成；不执行源脚本。',components=operations)]
    bem.validate_manifest(builder.m,len(builder.payloads))
    from bem_tool import check_geometry
    check_geometry(builder.m,builder.payloads)
    return builder, dict(character_id=catalog['character_id'],character_name=catalog['name'],
        profile_id=catalog['profile_id'],revision=catalog['revision'],snapshot=target['snapshot'],
        components=entries,operations=operations,warnings=warnings,
        appearance='源包默认外观',render_verified=False)
