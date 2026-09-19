"""Derive EFMI source identities from original game resources, not Mod packages.

Policy: XXMI region CRC32C for a complete single-submesh index range;
texture_hash=0, typed DX11 Texture2D, complete native mip chain, DEFAULT/SRV.
The descriptor policy is anchored by 22 reviewed Endmin texture identities.
Other GPU configurations remain outside this offline identity policy.
"""
import argparse
import collections
import copy
import struct
from pathlib import Path

from runtime_native_probe import read, write
from import_runtime_catalog import load_observations

POLICY='efmi-dx11-region-texture0-fullchain-v1'
# (serialized format, serialized color-space flag): (DXGI, Unity GraphicsFormat).
FORMATS={('BC7',1):(99,108),('BC7',0):(98,109),('BC5',0):(83,104),
         ('RGBA32',0):(28,8),('RGBA32',1):(29,4),('DXT1',0):(71,97),('DXT1',1):(72,96)}


def crc32c(seed,data):
    value=seed^0xffffffff
    for b in data:
        value^=b
        for _ in range(8):value=(value>>1)^(0x82f63b78 if value&1 else 0)
    return value^0xffffffff


def texture_identity(texture,observed):
    identity=texture.get('resource_identity')
    if not identity:raise ValueError(texture.get('resource_identity_error','texture bytes unavailable'))
    pair=(identity['format'],identity['color_space'])
    if pair not in FORMATS:raise ValueError('unmapped texture encoding/color space')
    dxgi,graphics=FORMATS[pair]
    levels=identity['levels']
    if (identity.get('algorithm')!='crc32c' or not levels or len(levels)!=texture['mips']
            or identity['payload_bytes']!=identity['accounted_bytes']):
        raise ValueError('incomplete texture mip payload')
    native=levels[0]
    if (native['width'],native['height'])!=(texture['width'],texture['height']):raise ValueError('texture size differs')
    if not observed:raise ValueError('texture has no direct runtime binding observation')
    sizes={(l['width'],l['height']) for l in levels}
    if any(fmt!=graphics or (w,h) not in sizes for fmt,w,h in observed):
        raise ValueError('runtime texture format/size differs from native mip chain')
    # D3D11_TEXTURE2D_DESC: width, height, mip levels, array=1, format,
    # sample count=1/quality=0, DEFAULT usage=0, SRV bind=8, CPU/misc=0.
    descriptor=[native['width'],native['height'],len(levels),1,dxgi,1,0,0,8,0,0]
    source=f"{crc32c(int(native['legacy_crc32c'],16),struct.pack('<11I',*descriptor)):08x}"
    return source,dict(texture_id=texture['id'],policy=POLICY,descriptor=descriptor,
        native_data_crc32c=native['legacy_crc32c'],runtime_graphics_format=graphics,
        runtime_sizes=[list(s) for s in sorted({(w,h) for _,w,h in observed})])


def build(catalog,database,raw,observations):
    snapshot=catalog['source_snapshot']['manifest_version']
    if raw['snapshot']['manifest_version']!=snapshot or database['source']['snapshot']['manifest_version']!=snapshot:
        raise ValueError('source identity snapshot differs from catalog')
    objects={o['id']:o for o in raw['objects']}
    renderers={r['id']:r for r in database['renderers']}
    candidates=collections.defaultdict(list);texture_refs=collections.defaultdict(set)
    observed_textures=collections.defaultdict(set);issues=[];entry_evidence={}
    for cid,c in catalog['components'].items():
        mesh=objects[c['evidence']['mesh_id']];identity=mesh.get('resource_identity',{})
        subs=mesh['submeshes']
        if (len(subs)!=1 or subs[0]['first_byte']!=0 or subs[0]['base_vertex']!=0
                or identity.get('index_count')!=c['original_index_count']
                or subs[0]['index_count']!=identity.get('index_count')
                or identity.get('algorithm')!='crc32c'
                or identity.get('index_size') not in (2,4)
                or identity.get('byte_width')!=identity.get('index_count',0)*identity.get('index_size',0)):
            issues.append(dict(kind='mesh',name=c['mesh_name'],reason='not a complete single-submesh index region'))
        else:
            h=identity['data_crc32c'];candidates[h].append(int(cid))
            entry_evidence[str(cid)]=dict(mesh_id=mesh['id'],policy=POLICY,index_size=identity['index_size'],
                byte_width=identity['byte_width'],index_count=identity['index_count'],source_hash=h)
        for root,evidence in c['evidence']['observations'].items():
            r=renderers[evidence['renderer_id']]
            for m in r['materials']:
                for t in m['textures']:
                    if t['name'] and t['id']:texture_refs[t['name']].add(t['id'])
            direct=observations.get((root,evidence['path']))
            if direct:
                for m in direct['row']['materials']:
                    for t in m['textures']:
                        if t['name']:observed_textures[t['name']].add((t['graphics_format'],t['width'],t['height']))
    entries={}
    for h,ids in candidates.items():
        if len(ids)==1:entries[h]=ids[0]
        else:issues.append(dict(kind='mesh_collision',hash=h,components=ids,reason='same IB identity for multiple native parts'))
    texture_candidates=collections.defaultdict(list)
    for name,ids in sorted(texture_refs.items()):
        try:
            if len(ids)!=1:raise ValueError('same texture name has multiple native objects')
            obj=objects[next(iter(ids))]
            if obj['name']!=name:raise ValueError('texture reference/name mismatch')
            h,evidence=texture_identity(obj,observed_textures[name])
            texture_candidates[h].append(dict(name=name,evidence=evidence))
        except (ValueError,KeyError) as exc:
            issues.append(dict(kind='texture',name=name,reason=str(exc)))
    textures={}
    for h,rows in texture_candidates.items():
        if len(rows)==1:textures[h]=rows[0]
        else:
            # EFMI hash overrides affect every original texture with that hash.
            # Preserve that behavior for all named bindings of this role, even
            # when the game's shared texture has separate body/face names.
            textures[h]=dict(name=rows[0]['name'],aliases=[r['name'] for r in rows[1:]],
                evidence=dict(policy=POLICY,kind='shared-source-hash',targets=rows))
    # Previously reviewed identities serve as independent regression anchors.
    for h,cid in catalog['entries'].items():
        if entries.get(h)!=cid:raise ValueError('reviewed IB identity no longer agrees: '+h)
    for h,t in catalog['textures'].items():
        if textures.get(h,{}).get('name')!=t['name']:raise ValueError('reviewed texture identity no longer agrees: '+h)
    result=copy.deepcopy(catalog)
    result.update(entries=entries,textures=textures,entry_identity_evidence=entry_evidence,
        identity_issues=issues,identity_policy=POLICY,revision='20260920-identities1',
        mapping_status='offline-resource-identities-partial' if issues else 'offline-resource-identities')
    result['note']='Native sweep plus offline identity join. EFMI identities recomputed from original game buffers and texture descriptors; custom source logic remains separate.'
    return result


def main():
    p=argparse.ArgumentParser(description=__doc__)
    for flag in ('catalog','databases','identities','captures'):p.add_argument('--'+flag,type=Path,required=True)
    p.add_argument('--runs',nargs='+',required=True)
    a=p.parse_args();version,observations,_=load_observations(a.captures,a.runs)
    pending=[];rows=[]
    for path in sorted(a.catalog.glob('*.json')):
        catalog=read(path)
        if catalog.get('kind')!='bem-character-catalog':continue
        char=catalog['character_id']
        if version!=catalog['source_snapshot']['manifest_version']:raise ValueError('capture snapshot differs')
        result=build(catalog,read(a.databases/char/'database.json'),read(a.identities/(char+'.json')),observations)
        pending.append((path,result))
        rows.append(dict(character_id=char,name=result['name'],entries=len(result['entries']),textures=len(result['textures']),
            mapping_status=result['mapping_status'],issues=result['identity_issues']))
    # These two globals are source-content checked during every conversion.
    # Same snapshot and original global names make them reusable across roles.
    globals_catalog=next(c for _,c in pending if c['character_id']=='chr_0003_endminf')
    globals_evidence=globals_catalog['preserved_globals']
    for path,result in pending:
        for h,g in globals_evidence.items():
            if 'source_payload_sha256' not in g:raise ValueError('global source content identity missing')
            if h in result['textures']:raise ValueError('global/material identity conflict')
            result['preserved_globals'][h]=copy.deepcopy(g)
    coverage=read(a.catalog/'coverage.json')
    by_id={r['character_id']:r for r in rows}
    for item in coverage['characters']:
        r=by_id[item['character_id']]
        item.update(mapping_status=r['mapping_status'],source_entries=r['entries'],source_textures=r['textures'],identity_issues=r['issues'])
    for path,result in pending:write(path,result)
    write(a.catalog/'coverage.json',coverage)
    write(a.catalog/'source-identities.json',dict(schema=1,kind='bem-source-identity-coverage',policy=POLICY,
        manifest_version=version,characters=rows))
    print(f"roles={len(rows)} entries={sum(r['entries'] for r in rows)} textures={sum(r['textures'] for r in rows)} issues={sum(len(r['issues']) for r in rows)}")


if __name__=='__main__':main()
