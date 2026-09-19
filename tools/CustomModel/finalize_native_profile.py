"""Combine observed native layouts with an explicit, reviewed material recipe.

No material intent is inferred here. Global replacements may be preserved only
after exact mip-byte and live graphics-format checks against native textures.
"""
import argparse
import copy
import json
from pathlib import Path, PurePosixPath

from convert_efmi_poc import Source, parse_dds
from convert_hash_lod import select_ini
from efmi_source import sections, value
from hash_lod_lowering import texture_overrides
from prepare_native_profile import prepare
from parse_native_models import apply_observations


def load(path): return json.loads(path.read_text(encoding='utf-8-sig'))


def exact_native_mips(dds, native, raw, graphics_formats):
    width,height,mips,fmt,srgb,data=dds
    formats={'BC7':(25,16,108,109), 'BC5':(27,16,None,104),
             'BC4':(26,8,None,102), 'DXT1':(10,8,96,97), 'DXT5':(12,16,98,99)}
    spec=formats.get(native['format'])
    if spec is None or fmt!=spec[0]: raise ValueError('native/DDS compression differs')
    if (width,height)!=(native['width'],native['height']) or not 0<mips<=native['mips']:
        raise ValueError('native/DDS dimensions or mip range differs')
    if graphics_formats!={spec[2] if srgb else spec[3]}:
        raise ValueError('native/DDS live graphics format differs')
    count=0
    for _ in range(mips):
        count+=max(1,(width+3)//4)*max(1,(height+3)//4)*spec[1]
        width=max(1,width//2); height=max(1,height//2)
    if count!=len(data) or raw[:count]!=data: raise ValueError('DDS differs from native mip bytes')
    return count


def finalize(src, database, observations, native_textures, texture_dir, recipe):
    if not recipe.get('evidence') or not recipe.get('semantic_notes'):
        raise ValueError('reviewed recipe evidence and semantic notes required')
    snapshot=database['source']['snapshot']
    if native_textures.get('snapshot')!=snapshot:
        raise ValueError('native texture snapshot differs')
    for field in ('manifest_version','perforce_cl'):
        if observations.get(field)!=snapshot.get(field,''): raise ValueError('observation snapshot differs')
    apply_observations(database,observations)
    ini=select_ini(src,recipe.get('ini')); sec=sections(src.read_exact(ini).decode('utf-8-sig'))
    root=PurePosixPath(ini).parent
    def resource(name):
        filename=value(sec.get(name,''),'filename')
        if not filename: raise ValueError(f'missing resource {name}')
        path=PurePosixPath(filename.replace('\\','/'))
        if path.is_absolute() or '..' in path.parts or ':' in str(path): raise ValueError('unsafe resource path')
        return src.read_exact(str(root/path))
    preparation=prepare(database,sec,recipe['world_resource'],recipe['ui_resource'])
    profile=copy.deepcopy(preparation['profile_draft'])
    by_id={r['id']:r for r in database['renderers']}
    observed={r['id']:r for r in observations['renderers']}
    natives={row['component']:by_id[row['world_renderer']] for row in preparation['components']}
    for row in preparation['components']:
        world,ui=(by_id[row[key]] for key in ('world_renderer','ui_renderer'))
        for native in (world,ui):
            if native['id'] not in observed or not native.get('runtime_layout'):
                raise ValueError('both world and UI need runtime observations')
        if world['runtime_layout']['attributes']!=ui['runtime_layout']['attributes'] or world['runtime_layout']['strides']!=ui['runtime_layout']['strides']:
            raise ValueError('world/UI runtime declarations differ')
        profile['components'][str(row['component'])]['evidence']=observations['evidence']+'; '+world['id']+'; matching world/UI identities'
    if set(recipe['entries'])!=set(profile['entries']): raise ValueError('recipe entry inventory differs')
    material_evidence=[]
    for identity,mapping in profile['entries'].items():
        rules=recipe['entries'][identity]
        if [r['bindings'] for r in rules]!=[r['bindings'] for r in mapping['material_rules']]:
            raise ValueError('recipe does not cover exact selected material states')
        mapped=[]
        for rule in rules:
            donor=rule.get('component',mapping['component']);slot=rule.get('slot',0)
            material=natives[donor]['materials'][slot]
            if not rule.get('evidence'): raise ValueError('material rule evidence missing')
            translated={k:copy.deepcopy(rule[k]) for k in ('bindings','evidence')}
            translated.update(component=donor,slot=slot,textures=[],preserve_resources={})
            if 'repeat_material_slots' in rule: translated['repeat_material_slots']=rule['repeat_material_slots']
            for texture in rule.get('textures',[]):
                slots=[t for t in material['textures'] if t['property']==texture['property'] and t['name']]
                if len(slots)!=1: raise ValueError('material property has no unique original texture')
                original=slots[0]['name']
                if sum(t['name']==original for t in material['textures'])!=1:
                    raise ValueError('texture name pin would bind multiple material properties')
                resource(texture['resource'])
                translated['textures'].append({'resource':texture['resource'],'original_texture':original})
            for alias,bound in rule.get('duplicate_resources',{}).items():
                if bound not in {t['resource'] for t in translated['textures']} or resource(alias)!=resource(bound):
                    raise ValueError('preserved draw resource is not an exact duplicate of a bound texture')
                translated['preserve_resources'][alias]='Exact DDS alias of already-bound '+bound
            mapped.append(translated)
            material_evidence.append({'entry':identity,'bindings':rule['bindings'],'native_material':material['name'],
                                      'textures':translated['textures'],'evidence':rule['evidence']})
        mapping['material_rules']=mapped
    original_by_name={}
    for texture in native_textures['objects']:
        if texture['type']=='Texture2D': original_by_name.setdefault(texture['name'],[]).append(texture)
    global_evidence=[]; profile['texture_overrides']={}
    overrides=texture_overrides(sec)
    if set(recipe['global_preserve'])!={o['hash'] for o in overrides}: raise ValueError('global texture inventory differs')
    for override in overrides:
        name=recipe['global_preserve'][override['hash']]
        candidates=original_by_name.get(name,[])
        if len(candidates)!=1 or 'raw_texture_file' not in candidates[0]: raise ValueError('native texture identity/bytes unavailable')
        native=candidates[0]; path=(texture_dir/native['raw_texture_file']).resolve()
        if path.parent!=texture_dir.resolve(): raise ValueError('native texture file outside export directory')
        formats={t['graphics_format'] for r in observations['renderers'] for m in r['materials'] for t in m['textures'] if t['name']==name}
        dds=parse_dds(override['resource'],resource(override['resource']))
        count=exact_native_mips(dds,native,path.read_bytes(),formats)
        evidence=f"Exact native mip bytes ({count}); {native['id']}; matching live graphics format"
        profile['texture_overrides'][override['hash']]={'resource':override['resource'],'disposition':'preserve_native',
            'reason':'Source DDS equals the native texture already bound to this character; retain native mip chain',
            'evidence':evidence}
        global_evidence.append({'hash':override['hash'],'native':name,'bytes_compared':count,'mips_compared':dds[2],
                                'native_id':native['id'],'exact_native_bytes':True})
    profile.update(verified=True,evidence=recipe['evidence']+'; '+observations['evidence'],
                   render_verified=False,semantic_notes=recipe['semantic_notes'],source_snapshot=snapshot)
    return profile,{'global_textures':global_evidence,'materials':material_evidence,
                    'semantic_notes':recipe['semantic_notes'],'render_verified':False}


def main():
    p=argparse.ArgumentParser(description=__doc__)
    p.add_argument('source',type=Path)
    for name in ('database','observations','native-textures','texture-dir','recipe','output','evidence-output'):
        p.add_argument('--'+name,type=Path,required=True)
    a=p.parse_args();src=Source(a.source)
    try: profile,evidence=finalize(src,load(a.database),load(a.observations),load(a.native_textures),a.texture_dir,load(a.recipe))
    finally: src.close()
    for path,data in ((a.output,profile),(a.evidence_output,evidence)):
        path.parent.mkdir(parents=True,exist_ok=True);temp=path.with_suffix(path.suffix+'.tmp')
        temp.write_text(json.dumps(data,ensure_ascii=False,indent=2,allow_nan=False),encoding='utf-8');temp.replace(path)
    print(json.dumps({'components':len(profile['components']),'verified_native_globals':len(evidence['global_textures']),
                      'render_verified':False}))


if __name__=='__main__': main()
