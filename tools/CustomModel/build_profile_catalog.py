"""Import existing reviewed role layout evidence into the shared native catalog.

Missing EFMI identities remain missing: native index counts only produce
diagnostic candidates, never authority to reuse another character's skeleton.
"""
import argparse
import json
from pathlib import Path
from prepare_native_profile import paired_resource


def build(database, profile, name, profile_id, revision):
    character=profile['character_id']; world=character+'_postmodel'; ui=character+'_uimodel'
    components={}
    for cid, source in profile['components'].items():
        def renderer(root):
            matches=[r for r in database['renderers'] if r['resource_root']==root and '/lod0/' in r['path'] and r['mesh_name']==source['mesh_name']]
            if len(matches)!=1: raise ValueError('Native renderer identity is ambiguous')
            return matches[0]
        native, other=renderer(world),renderer(ui)
        errors=paired_resource(native,other,database['meshes'])
        if errors: raise ValueError(str(errors))
        if native['original_index_count']!=source['original_index_count'] or len(native['bones'])!=source['bone_count']:
            raise ValueError('Current native asset differs from reviewed role evidence')
        if len(database['meshes'][native['mesh_id']]['submeshes'])!=1: raise ValueError('Needs explicit submesh mapping')
        attrs=[]; strides=[]; streams=[]
        for stream,decl in enumerate(source['streams']):
            streams.append({'semantics':decl['semantics']}); strides.append(sum(s['stride'] for s in decl['semantics']))
            for a in decl['source_channels']:
                attrs.append([a['attribute'],a['format'],a['dimension_raw'] & 15,stream])
        attrs.sort()
        components[cid]=dict(mesh_name=native['mesh_name'],original_index_count=native['original_index_count'],
            bone_names=[b['name'] for b in native['bones']],materials=[m['name'] for m in native['materials']],
            material_textures=[[t['name'] for t in m['textures'] if t['name']] for m in native['materials']],
            strides=strides,attributes=attrs,source_streams=streams,layout_verified=True,
            evidence=dict(mesh_id=native['mesh_id'],world_renderer=native['id'],ui_renderer=other['id'],
                layout=source.get('runtime_layout',source.get('mapping_evidence',profile['evidence'])),
                validation='Historical raw-stream replacement confirmed; native full names from current offline graph',
                world_ui='same-mesh-bones-materials'))
    return dict(schema=1,kind='bem-character-catalog',character_id=character,name=name,platform='windows-x64',
        profile_id=profile_id,revision=revision,verified=True,world_resource=world,ui_resource=ui,
        source_snapshot={'manifest_version':database['source']['snapshot']['manifest_version']},
        components=components,entries={},textures={},preserved_globals={},
        mapping_status='source-resource-identities-pending',
        evidence=['CUSTOM_MODEL_RESOURCE_AB_VALIDATION_20260916.md',profile['evidence']],
        note='Native role data only. No per-Mod material approximation or draw correction is imported.')


if __name__=='__main__':
    p=argparse.ArgumentParser(description=__doc__)
    for flag in ('database','profile','output'): p.add_argument('--'+flag,type=Path,required=True)
    for flag in ('name','profile-id','revision'): p.add_argument('--'+flag,required=True)
    a=p.parse_args()
    result=build(json.loads(a.database.read_text(encoding='utf-8-sig')),json.loads(a.profile.read_text(encoding='utf-8-sig')),a.name,a.profile_id,a.revision)
    a.output.parent.mkdir(parents=True,exist_ok=True);a.output.write_text(json.dumps(result,ensure_ascii=False,indent=2),encoding='utf-8')
    print(f'Native catalog: {len(result["components"])} components; source resource identities pending')
