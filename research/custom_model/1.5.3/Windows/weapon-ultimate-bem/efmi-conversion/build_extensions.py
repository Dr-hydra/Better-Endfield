"""Rebuild two explicitly reviewed ultimate-only EFMI extensions as BEM 1.4.

No source command list is executed. Only fixed draws, finite equality switches,
LOD0 stream references and identity-proven texture replacements are lowered.
"""
import copy
import itertools
import json
from pathlib import Path
import re
import struct
import subprocess
import sys

from audit_inputs import REPO, ROOT, SOURCES, load, sections, value, texture_identities
import bem_v1 as bem
import bem_v11
import bem_v14
from bem_tool import check_geometry
from convert_efmi_poc import parse_dds
from build_bem14_target import build_profile
from audit_original_textures import payload_crc32c


def source_file(root, filename):
    path = (root / filename).resolve()
    bem.require(path.is_relative_to(root.resolve()) and path.is_file(), 'Source reference leaves Mod directory')
    return path


def all_of(items):
    items = [x for x in items if x is not True]
    if False in items: return False
    return True if not items else items[0] if len(items) == 1 else {'all': items}


def any_of(items):
    if True in items: return True
    items = [x for x in items if x is not False]
    return False if not items else items[0] if len(items) == 1 else {'any': items}


def negate(item):
    return not item if isinstance(item, bool) else {'not': item}


def condition(text, groups):
    match = re.fullmatch(r'\$(\w+)\s*==\s*(\d+)', text.strip())
    bem.require(match is not None, 'Unreviewed draw condition: ' + text)
    name, number = match[1], int(match[2])
    if name == 'lod_detected': return number == 0
    bem.require(name in groups and str(number) in groups[name], 'Unknown finite draw condition: ' + text)
    return {'eq': [name, str(number)]}


def draw_program(body, groups):
    stack, draws, direct = [], [], {}
    for raw_line in body.splitlines():
        line = raw_line.split(';', 1)[0].strip()
        if not line or line.startswith('【'): continue
        if line.startswith('if '): stack.append(condition(line[3:], groups)); continue
        if line == 'else':
            bem.require(bool(stack), 'Unmatched else'); stack[-1] = negate(stack[-1]); continue
        if line == 'endif':
            bem.require(bool(stack), 'Unmatched endif'); stack.pop(); continue
        if line.startswith('drawindexedinstanced'):
            match = re.fullmatch(r'drawindexedinstanced\s*=\s*(\d+),\s*INSTANCE_COUNT,\s*(\d+),\s*0,\s*FIRST_INSTANCE', line)
            bem.require(match is not None, 'Unreviewed draw arguments: ' + line)
            when = all_of(stack)
            if when is not False: draws.append(dict(count=int(match[1]), start=int(match[2]), when=when))
        elif re.match(r'ps-t\d+\s*=', line):
            bem.require(not stack, 'Conditional shader-slot replacement needs separate review')
            slot, binding = line.split('=', 1); direct[slot.strip()] = binding.strip().removeprefix('ref ')
        else:
            bem.require(line.startswith(('run = CommandList\\EFMIv1\\OverrideTextures', 'ib =', 'vb0 =', 'vb1 =',
                                         'vb2 =', 'vb3 =', '$active =')), 'Unreviewed draw statement: ' + line)
    bem.require(not stack, 'Unclosed draw condition')
    return draws, direct


def make_groups(ini):
    defaults = {m[1]: int(m[2]) for m in re.finditer(r'^global\s+persist\s+\$(\w+)\s*=\s*(\d+)', ini['Constants'], re.M)}
    groups = []
    for name, default in defaults.items():
        keys = [body for section, body in ini.items() if section.startswith('Key') and value(body, '$' + name)]
        bem.require(len(keys) == 1 and value(keys[0], 'type') == 'cycle', 'Unreviewed persistent option ' + name)
        choices = [int(x.strip()) for x in value(keys[0], '$' + name).rstrip(',').split(',')]
        bem.require(default in choices and len(choices) == len(set(choices)), 'Invalid source option cycle')
        groups.append(dict(id=name, name=name, default=str(default), choices=[dict(id=str(n), name=str(n)) for n in choices]))
    return groups


def compare_resource_spaces(objects, rows_a, rows_b):
    by_go = {o['game_object']['id']: o for o in objects.values() if o['type'] == 'Transform'}
    def relative_pose(ref):
        pose = []
        while ref:
            node = objects[ref]
            pose.append([node[k] for k in ('position', 'rotation', 'scale')])
            ref = node.get('parent', {}).get('id')
        return pose
    summary = []
    for a,b in zip(sorted(rows_a,key=lambda r:r['mesh_name']),sorted(rows_b,key=lambda r:r['mesh_name'])):
        mesh = objects[a['mesh_id']]
        same = dict(mesh_identity=a['mesh_id'] == b['mesh_id'],
                    bone_names=[v['name'] for v in a['bones']] == [v['name'] for v in b['bones']],
                    renderer_space=relative_pose(a['transform_ids'][-1]) == relative_pose(b['transform_ids'][-1]),
                    bone_spaces=[relative_pose(x['id']) for x in a['bones']] == [relative_pose(x['id']) for x in b['bones']],
                    material_objects=[m['id'] for m in a['materials']] == [m['id'] for m in b['materials']])
        bem.require(all(same.values()), 'Ultimate/ability donor spaces differ: '+a['mesh_name'])
        summary.append(dict(mesh=a['mesh_name'], bone_count=len(a['bones']), bindpose_count=len(mesh['bindposes']), **same))
    return summary


def build(task, raw, graph, objects):
    root = SOURCES[task]; ini = sections((root/'mod.ini').read_text(encoding='utf-8-sig'))
    spec = bem.load_json(REPO/'tools/CustomModel/profiles/bem14-drafts/zhuangfy-ultimate.spec.json')
    spec['resources'] = spec['resources'][:2]
    profile, scaffold = build_profile(graph, spec)
    target = profile['target']; target['profile_id'] = f'dev.{task.lower()}.ultimate-native'
    b = bem.Builder(f'dev.bem14.{task.lower()}.ultimate',
                    ('庄方宜-终极状态' if task=='M0178' else '庄方宜-心灵')+'-大招扩展（开发验证）',
                    '原作者素材；BEM 1.4 独立扩展', '1.4.0-dev', target)
    m = b.m; m.pop('appearances'); m.pop('default_appearance_id')
    m['required_capabilities'] = ['native-materials','palette-u8','indices-u32','composable-options','multi-resource-targets']
    m['option_groups'] = make_groups(ini)
    groups = {g['id']: {c['id'] for c in g['choices']} for g in m['option_groups']}
    m['component_rules'] = [dict(target=c['id'], candidates=[dict(operation='keep')]) for c in target['components']]
    root_names = {r['id']:r['name'] for r in target['resources']}
    native = {rid:[r for r in graph['renderers'] if r['resource_root']==name and '/lod0/' in r['path']]
              for rid,name in root_names.items()}
    spaces = compare_resource_spaces(objects, native['ultimate'], native['ability'])
    texture_names = texture_identities(objects)
    bindings, global_resources, unresolved, retained_originals = {}, set(), [], []
    original_proofs={row['resource']:row for row in bem.load_json(ROOT/'original-texture-audit.json')['results'][task]}
    for section, body in ini.items():
        if section.startswith('TextureOverride_Texture'):
            ref = value(body,'this')
            if ref: global_resources.add(ref.removeprefix('ref '))
    texture_cache, source_resources = {}, {}
    for section, body in ini.items():
        if not section.startswith('Resource_Texture'): continue
        filename = value(body,'filename'); match = re.search(r't=([0-9a-fA-F]{8})',filename)
        bem.require(match is not None,'Missing source texture identity')
        identity=match[1].lower(); names=sorted(texture_names.get(identity,[]))
        source_resources[section]=dict(file=filename, identity=identity, names=names)
        bindings[section] = names

    def texture(ref, original):
        key=ref,original
        if key not in texture_cache:
            filename=source_resources[ref]['file']
            w,h,mips,fmt,srgb,data=parse_dds(filename, source_file(root,filename).read_bytes())
            texture_cache[key]=len(m['textures'])
            m['textures'].append(dict(width=w,height=h,mips=mips,format=fmt,srgb=srgb,original_name=original,payload=b.payload(data)))
        return texture_cache[key]

    def material_textures(row, slot, direct):
        originals = {p['name'] for p in row['materials'][slot]['textures'] if p['name']}
        selected = {}
        for ref in sorted(global_resources) + list(direct.values()):
            for original in bindings.get(ref,[]):
                if original in originals: selected[original] = texture(ref, original)
        return list(selected.values())

    draws_by_source, source_mesh_audit, component_operations = {}, [], {}
    for section, body in ini.items():
        match=re.fullmatch(r'TextureOverride_Component(\d+)',section)
        if not match: continue
        cid=int(match[1]); identity=value(body,'hash'); count=int(value(body,'match_index_count'))
        command=value(body,'run')
        matched = {}
        for rid, rows in native.items():
            candidates=[r for r in rows if objects[r['mesh_id']].get('resource_identity',{}).get('data_crc32c')==identity
                        and r['original_index_count']==count]
            bem.require(len(candidates)==1,'Source component does not uniquely match native IB identity')
            matched[rid]=candidates[0]
        if not command:
            bem.require(value(body,'handling')=='skip','Source entry is neither replace nor hide')
            for rid,row in matched.items():
                target_id=next(c['id'] for c in target['components'] if c['resource']==rid and c['mesh_name']==row['mesh_name'])
                m['component_rules'][target_id]['candidates']=[dict(operation='hide')]
            component_operations[cid]='hide'; continue
        source_draws,direct=draw_program(ini[command],groups)
        bem.require(source_draws,'No source draws')
        draws_by_source[cid]=dict(draws=source_draws,direct=direct)
        streams=[]; stream_bytes=[]
        for stream in range(3):
            ref=f'Resource_Component{cid}_VB{stream}'; declaration=ini[ref]
            data=source_file(root,value(declaration,'filename')).read_bytes(); stride=int(value(declaration,'stride'))
            bem.require(len(data)%stride==0,'Source stream length/stride mismatch')
            stream_bytes.append(data); streams.append(dict(stride=stride,payload=b.payload(data)))
        vertices=len(stream_bytes[0])//streams[0]['stride']
        bem.require(all(len(data)//stream['stride']==vertices for data,stream in zip(stream_bytes,streams)), 'Source stream vertex counts differ')
        ib=ini[f'Resource_Component{cid}_IB']; fmt=value(ib,'format')
        bem.require(fmt in ('DXGI_FORMAT_R16_UINT','DXGI_FORMAT_R32_UINT'),'Unknown source index format')
        index_size=2 if fmt.endswith('R16_UINT') else 4
        index_bytes=source_file(root,value(ib,'filename')).read_bytes()
        attrs=[[c['attribute'],c['format'],c['dimension_raw']&15,c['stream'],c['offset']]
               for c in objects[matched['ultimate']['mesh_id']]['serialized_channels'] if c['dimension_raw']]
        coverage=[]
        for draw in source_draws:
            begin,end=draw['start']*index_size,(draw['start']+draw['count'])*index_size
            bem.require(0<=begin<end<=len(index_bytes),'Draw outside source index buffer')
            draw['payload']=b.payload(index_bytes[begin:end]); coverage.append([begin,end])
        bem.require(coverage[0][0]==0 and all(a[1]==bb[0] for a,bb in zip(coverage,coverage[1:])) and coverage[-1][1]==len(index_bytes),
                    'Source draw ranges do not cover index buffer')
        visible=any_of([d['when'] for d in source_draws])
        for rid,row in matched.items():
            component=next(c for c in target['components'] if c['resource']==rid and c['mesh_name']==row['mesh_name'])
            target_id=component['id']; mesh_id=len(m['meshes']); draws=[]
            for draw in source_draws:
                for slot,material in enumerate(row['materials']):
                    draws.append(dict(indices=draw['payload'],count=draw['count'],when=draw['when'],
                                      material_component=target_id,material_slot=slot,material_name=material['name'],
                                      textures=material_textures(row,slot,direct)))
            mesh=dict(renderer_kind='skinned',vertex_count=vertices,index_size=index_size,streams=copy.deepcopy(streams),
                      attributes=attrs,bones=[dict(component=target_id,index=i,name=name) for i,name in enumerate(component['bone_names'])],draws=draws)
            m['meshes'].append(mesh)
            replace=dict(operation='replace',mesh=mesh_id)
            if visible is not True:
                replace['when']=visible
                m['component_rules'][target_id]['candidates']=[replace,dict(operation='hide',when=negate(visible))]
            else:m['component_rules'][target_id]['candidates']=[replace]
        bone_count=len(matched['ultimate']['bones']); skin_stride=streams[2]['stride']; skin=stream_bytes[2]
        slots=[v for offset in range(skin_stride-4,len(skin),skin_stride) for v in skin[offset:offset+4]]
        bem.require(all(i<bone_count for i in slots),'Source bone indices exceed true donor palette')
        if skin_stride==12:
            bem.require(all(sum(struct.unpack_from('<4H',skin,i))==65535 for i in range(0,len(skin),12)), 'Source skin weights not normalized')
        component_operations[cid]='replace'
        source_mesh_audit.append(dict(source_component=cid,source_hash=identity,original_index_count=count,
             native_mesh=matched['ultimate']['mesh_name'],vertices=vertices,bones=bone_count,max_bone_index=max(slots),
             source_strides=[s['stride'] for s in streams],native_material_slots=len(matched['ultimate']['materials']),
             base_lod_streams=True,raw_stream_bytes_preserved=True,raw_index_ranges_preserved=True,
             skin_weights_exact_sum_65535=True if skin_stride==12 else None,
             skin_kind='ushort4-weights-u8x4-indices' if skin_stride==12 else 'native-implicit-weight-u8x4-indices',
             source_draws=[{k:d[k] for k in ('start','count','when')} for d in source_draws],draw_count=len(source_draws)))

    for rid,rows in native.items():
        for row in rows:
            component=next(c for c in target['components'] if c['resource']==rid and c['mesh_name']==row['mesh_name'])
            rule=m['component_rules'][component['id']]
            if rule['candidates'] != [dict(operation='keep')]:continue
            overrides=[]
            for slot,material in enumerate(row['materials']):
                tex=material_textures(row,slot,{})
                if tex:overrides.append(dict(material_slot=slot,material_name=material['name'],textures=tex))
            if overrides:
                rule['candidates'][0]['material_overrides']=overrides
                if 'keep-material-textures' not in m['required_capabilities']:m['required_capabilities'].append('keep-material-textures')
    direct_use={ref for d in draws_by_source.values() for ref in d['direct'].values()}
    for ref, entry in source_resources.items():
        used=ref in global_resources or ref in direct_use
        mapped=any(k[0]==ref for k in texture_cache)
        if used and not mapped:
            proof=original_proofs.get(ref)
            if proof and proof['classification']=='unchanged_source_base_under_efmi_identity_rule':
                w,h,mips,fmt,srgb,data=parse_dds(entry['file'],source_file(root,entry['file']).read_bytes())
                bem.require((w,h,mips,fmt,srgb)==tuple(proof[k] for k in ('width','height','source_mips','format','srgb')),
                            'Original-base texture metadata evidence changed')
                bem.require(f"{payload_crc32c(data[:proof['source_mip0_bytes']]):08x}"==proof['source_mip0_crc32c'],
                            'Original-base texture identity evidence changed')
                retained_originals.append(dict(resource=ref,**entry,classification=proof['classification'],
                    reason='Source base mip reconstructs the captured original EFMI identity; retain current game texture. No current material-property or full mip-chain equivalence is claimed.',
                    descriptor_matches=proof['descriptor_matches'],
                    external_native_base_comparison=proof.get('external_native_base_comparison'),
                    full_native_mip_chain_equivalence_proven=False))
                continue
            reason='No identity-proven native material consumer'; classification='unresolved-native-texture-identity'
            if task=='M0178' and ref=='Resource_Texture0':
                classification='unsupported-global-shadow-binding'
                reason='Component0 ps-t2 is a Global shadow resource in all compatible native shader records, not a material property; see shadow-slot-audit.json.'
            elif task=='M0184' and ref=='Resource_Texture0':
                reason='Source global this override has no direct slot evidence; the same hash is ps-t2 shadow data in M0178, which is corroboration rather than a material binding proof.'
            unresolved.append(dict(resource=ref,**entry,classification=classification,reason=reason))
    bem.validate_manifest(m,len(b.payloads)); summary=check_geometry(m,b.payloads,4)
    combinations=list(itertools.product(*[[c['id'] for c in g['choices']] for g in m['option_groups']]))
    sampled=[]
    for combo in combinations:
        choices=dict(zip([g['id'] for g in m['option_groups']],combo))
        for rid in native:
            plan=bem_v14.selection_plan(m,choices,resource=rid,platform='windows-x64')
            bem.require(all(target['components'][c['target']]['resource']==rid for c in plan['components']),'Resource plan escaped domain')
    output=ROOT/'outputs'; output.mkdir(exist_ok=True)
    path=output/f'{task}-zhuangfy-ultimate-extension.bem'; b.write(path)
    written,payloads=bem.read_package(path); check_geometry(written,payloads,4)
    report=dict(task=task,output=str(path),package_id=m['package_id'],source=str(root),native_snapshot=raw['snapshot']['manifest_version'],
                resources=target['resources'],target_components=len(target['components']),source_component_operations=component_operations,
                meshes=len(m['meshes']),textures=len(m['textures']),option_groups=len(m['option_groups']),
                all_option_combinations_checked=len(combinations),resource_plans_checked=len(combinations)*len(native),
                geometry=source_mesh_audit,donor_space_proof=spaces,selection_summary=summary,
                texture_resources=[dict(resource=r,**entry,active=r in global_resources or r in direct_use,
                                        mapped=any(k[0]==r for k in texture_cache)) for r,entry in source_resources.items()],
                unmapped_active_textures=unresolved,retained_original_base_textures=retained_originals,
                source_coverage_complete=not unresolved,
                renderer_verified=False,runtime_verified=False,render_verified=False,artifact_hashes_computed=False,
                exclusions=['Mirror resource is structurally different and untouched.',
                            'Low-LOD VB2 alternatives are not used by these Windows LOD0 resources.',
                            'EFMI registration/help UI is not executed.',
                            'Unresolved texture overrides retain the original game texture; listed individually.'])
    (ROOT/f'{task}-conversion-report.json').write_text(json.dumps(report,ensure_ascii=False,indent=2),encoding='utf-8')
    return report


if __name__=='__main__':
    raw,graph,objects=load()
    for task in SOURCES:
        result=build(task,raw,graph,objects)
        print(json.dumps({k:result[k] for k in ('task','output','target_components','meshes','textures','option_groups',
                                              'all_option_combinations_checked','source_coverage_complete')},ensure_ascii=False))
