"""Join sweep observations to offline identities and publish reusable role data.

Does not infer EFMI resource hashes from vertex/index counts or component labels.
Existing reviewed source identities survive only when the native contract matches.
"""
import argparse
import copy
import json
import re
from pathlib import Path

from runtime_native_probe import read,write
from runtime_sweep import records,summarize,summarize_campaign
from prepare_native_profile import paired_resource,equal_matrix,bone_name_aliases

RULES={
    (0,0,3,0):dict(name='POSITION',index=0,format='R32G32B32_FLOAT',stride=12),
    (1,0,1,0):dict(name='ENCODEDDATA',index=0,format='R32_UINT',stride=4),
    (4,0,2,1):dict(name='TEXCOORD',index=0,format='R32G32_FLOAT',stride=8),
    (6,3,4,1):dict(name='COLOR',index=0,format='R8G8B8A8_SNORM',stride=4),
    (12,4,4,2):dict(name='BLENDWEIGHTS',index=0,format='R16G16B16A16_UNORM',stride=8),
    (13,6,4,2):dict(name='BLENDINDICES',index=0,format='R8G8B8A8_UINT',stride=4),
}


def load_observations(folder,runs):
    all_rows={};version=None;provenance=[]
    for run in runs:
        if not re.fullmatch(r'[A-Za-z0-9_-]{1,80}',run):raise ValueError('invalid run ID')
        meta=read(folder/(run+'.request.json'))
        if version is not None and version!=meta['manifest_version']:raise ValueError('mixed snapshot')
        version=meta['manifest_version']
        paths=sorted(folder.glob(run+'.*.jsonl')) if meta.get('persistent') else [folder/(run+'.jsonl')]
        sessions=[(p.name,records(p)) for p in paths]
        report=summarize_campaign(meta,sessions) if meta.get('persistent') else summarize(meta,sessions[0][1])
        if report.get('conflicting_resources'):raise ValueError('conflicting captures require review')
        for filename,rows in sessions:
            for record in rows:
                if not record['complete']:continue
                if len(record['renderers'])!=record['renderer_count'] or not all(r['complete'] for r in record['renderers']):
                    raise ValueError('record marked complete contains incomplete renderer data')
                for row in record['renderers']:
                    if not row['complete']:continue
                    key=(row['resource_root'],row['path'])
                    value=dict(row=row,run=run,file=filename,process_id=record['process_id'])
                    if key in all_rows:
                        before=all_rows[key]['row']
                        for field in ('mesh_name','original_index_count','strides','attributes','bones','bindposes'):
                            if before[field]!=row[field]:raise ValueError(f'cross-run native conflict: {key}/{field}')
                        slots=lambda r:[(m['name'],m.get('shader')) for m in r['materials']]
                        if slots(before)!=slots(row):raise ValueError(f'cross-run native material conflict: {key}')
                    all_rows[key]=value
        provenance.append(dict(run=run,process_ids=report['process_ids']))
    return version,all_rows,provenance


def validate_observation(native,mesh,observed):
    for field in ('resource_root','path','mesh_name','original_index_count'):
        if native[field]!=observed[field]:raise ValueError('runtime/offline identity differs: '+field)
    if observed['vertex_count']!=mesh['vertex_count']:raise ValueError('native vertex count changed')
    if observed['bones']!=[dict(name=b['name'],path=b['path']) for b in native['bones']]:raise ValueError('native bone order/path changed')
    if [m['name'] for m in observed['materials']]!=[m['name'] for m in native['materials']]:raise ValueError('native material slots changed')
    # AnimeStudio preserves the serialized matrix float order. In this backend
    # that order is row-major; Mesh.get_bindposes returns Unity column-major.
    # Normalize explicitly, never accept either orientation opportunistically.
    offline_poses=[[p[row*4+col] for col in range(4) for row in range(4)]
                   for p in mesh['bindposes'] if len(p)==16]
    if len(offline_poses)!=len(mesh['bindposes']) or len(observed['bindposes'])!=len(offline_poses) or not all(equal_matrix(a,b) for a,b in zip(observed['bindposes'],offline_poses)):
        raise ValueError('bindposes differ from offline Mesh: '+native['resource_root']+'/'+native['mesh_name'])


def source_layout(observed):
    streams=[{'semantics':[]} for _ in range(3)]
    for a in observed['attributes']:
        if tuple(a) not in RULES:return [],False
        streams[a[3]]['semantics'].append(copy.deepcopy(RULES[tuple(a)]))
    if [sum(s['stride'] for s in b['semantics']) for b in streams]!=observed['strides']:return [],False
    return streams,True


def build_role(database,observations,character,name,version,old=None):
    if database['source']['snapshot']['manifest_version']!=version:raise ValueError('offline snapshot differs')
    world,ui=character+'_postmodel',character+'_uimodel'
    native_rows=database['renderers'];meshes=database['meshes']
    primary=[r for r in native_rows if r['resource_root']==world and '/lod0/' in (r['path'] or '')
             and r['mesh_name'].startswith('S_actor_') and '_vfxpart_' not in r['mesh_name']]
    candidates={};excluded=[]
    for w in primary:
        label=w['mesh_name'];peers=[r for r in native_rows if r['resource_root']==ui and '/lod0/' in (r['path'] or '') and r['mesh_name']==label]
        errors=paired_resource(w,peers[0],meshes) if len(peers)==1 else ['UI counterpart absent/ambiguous']
        aliases=[]
        if errors==['world/UI bone path/order differs']:
            # Same Mesh, only a leaf bone name differs (e.g. a world-skeleton typo):
            # BEM 1.2 keeps the UI name and declares the world name as an alias.
            aliases=bone_name_aliases(w,peers[0])
            if aliases:errors=[]
        if sum(r['mesh_name']==label for r in primary)!=1:errors.append('world renderer identity ambiguous')
        if errors:
            excluded.append(dict(mesh_name=label,reasons=errors));continue
        u=peers[0];direct=[];evidence={}
        for native in (w,u):
            source=observations.get((native['resource_root'],native['path']))
            if source:
                validate_observation(native,meshes[native['mesh_id']],source['row'])
                direct.append(source)
                evidence[native['resource_root']]=dict(kind='direct',run=source['run'],file=source['file'],
                    process_id=source['process_id'],renderer_id=native['id'],path=native['path'])
        if not direct:
            reviewed=[c for c in (old or {}).get('components',{}).values() if c['mesh_name']==label and
                      c.get('evidence',{}).get('mesh_id')==w['mesh_id'] and c.get('bone_name_aliases')==aliases]
            if aliases and reviewed:
                # Offline-reviewed BEM 1.2 component (layout from serialized channels).
                candidates[label]=copy.deepcopy(reviewed[0]);continue
            excluded.append(dict(mesh_name=label,reasons=['no runtime observation for either equivalent root']));continue
        observed=direct[0]['row']
        if any(d['row']['attributes']!=observed['attributes'] or d['row']['strides']!=observed['strides'] for d in direct[1:]):
            excluded.append(dict(mesh_name=label,reasons=['world/UI runtime vertex layout differs']));continue
        for native in (w,u):
            if native['resource_root'] not in evidence:
                evidence[native['resource_root']]=dict(kind='equivalent-reuse',from_resource=observed['resource_root'],
                    renderer_id=native['id'],path=native['path'],reason='identical Mesh identity, relative bone paths/order and materials')
        streams,supported=source_layout(observed)
        candidates[label]=dict(mesh_name=label,original_index_count=w['original_index_count'],
            bone_names=[b['name'] for b in (u['bones'] if aliases else w['bones'])],materials=[m['name'] for m in w['materials']],
            material_textures=[list(dict.fromkeys(t['name'] for t in m['textures'] if t['name'])) for m in w['materials']],
            material_texture_properties=[[dict(property=t['property'],name=t['name']) for t in m['textures'] if t['name']] for m in w['materials']],
            strides=observed['strides'],attributes=observed['attributes'],source_streams=streams,
            layout_verified=True,source_layout_supported=supported and len(meshes[w['mesh_id']]['submeshes'])==1,
            vertex_count=observed['vertex_count'],submeshes=meshes[w['mesh_id']]['submeshes'],
            evidence=dict(mesh_id=w['mesh_id'],world_ui='offline-equivalent',observations=evidence))
        if aliases:candidates[label]['bone_name_aliases']=aliases
    # Preserve IDs used by reviewed resource mappings; new native parts append.
    ordered=[]
    if old:
        for key,c in sorted(old['components'].items(),key=lambda kv:int(kv[0])):
            if c['mesh_name'] not in candidates:raise ValueError('existing reviewed component cannot be reused: '+c['mesh_name'])
            fresh=candidates[c['mesh_name']]
            for field in ('original_index_count','bone_names','materials','strides','attributes'):
                if fresh[field]!=c[field]:raise ValueError('existing native contract changed: '+c['mesh_name']+'/'+field)
            for field in ('source_stream_aliases',):
                if field in c:fresh[field]=c[field]
            if c.get('source_streams'):fresh['source_streams']=c['source_streams']
            ordered.append(c['mesh_name'])
    ordered+=sorted(set(candidates)-set(ordered))
    if not ordered:raise ValueError('no reusable native components')
    result=dict(schema=1,kind='bem-character-catalog',character_id=character,name=name,platform='windows-x64',
        profile_id=old['profile_id'] if old else character+'-pc',revision='20260919-sweep1',verified=True,
        world_resource=world,ui_resource=ui,source_snapshot={'manifest_version':version},
        components={str(i):candidates[n] for i,n in enumerate(ordered)},
        entries=old.get('entries',{}) if old else {},textures=old.get('textures',{}) if old else {},
        preserved_globals=old.get('preserved_globals',{}) if old else {},
        mapping_status='reviewed-resource-identities' if old and old.get('entries') else 'source-resource-identities-pending',
        excluded_components=excluded,evidence=['CUSTOM_MODEL_RUNTIME_SWEEP_20260919.md'],
        note='Runtime observation plus offline identity join; excluded components stay outside the shared BEM contract. Source resource mappings remain separate.')
    if old and old.get('identity_policy'):
        for field in ('identity_policy','entry_identity_evidence','identity_issues'):
            if field in old:result[field]=copy.deepcopy(old[field])
        # Re-importing runtime data does not recompute source buffer identities.
        # Keep old identities as regression anchors for the next offline join,
        # but do not expose them to automatic conversion before that succeeds.
        result['mapping_status']='source-resource-identities-recheck-required'
    return result


def main():
    p=argparse.ArgumentParser(description=__doc__)
    for flag in ('databases','captures','names','output'):p.add_argument('--'+flag,type=Path,required=True)
    p.add_argument('--runs',nargs='+',required=True)
    a=p.parse_args();version,observations,provenance=load_observations(a.captures,a.runs)
    names=read(a.names);existing={}
    for path in a.output.glob('*.json'):
        c=read(path)
        if c.get('kind')=='bem-character-catalog':existing[c['character_id']]=(path,c)
    pending=[];coverage=[]
    for path in sorted(a.databases.glob('chr_*/database.json')):
        character=path.parent.name;old=existing.get(character)
        catalog=build_role(read(path),observations,character,names.get(character,character),version,old[1] if old else None)
        destination=old[0] if old else a.output/(character+'.json')
        pending.append((destination,catalog))
        coverage.append(dict(character_id=character,name=catalog['name'],components=len(catalog['components']),
            mapping_status=catalog['mapping_status'],excluded=catalog['excluded_components'],
            unsupported_source_layouts=[c['mesh_name'] for c in catalog['components'].values() if not c['source_layout_supported']],
            observations={root:sum(c['evidence']['observations'][root]['kind']=='direct' for c in catalog['components'].values()) for root in (catalog['world_resource'],catalog['ui_resource'])}))
    # Finish validation before changing the published catalog.
    for path,catalog in pending:write(path,catalog)
    write(a.output/'coverage.json',dict(schema=1,kind='bem-catalog-coverage',manifest_version=version,runs=provenance,characters=coverage))
    print(json.dumps(dict(characters=len(coverage),components=sum(c['components'] for c in coverage),excluded=sum(len(c['excluded']) for c in coverage),unsupported_source_layouts=sum(len(c['unsupported_source_layouts']) for c in coverage))))


if __name__=='__main__':main()
