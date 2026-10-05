from pathlib import Path
import json,sys,struct
sys.path.insert(0,'tools/CustomModel')
import bem_v1 as bem

def texture_masks(raw):
    magic,ver,nc,nt,_,_=struct.unpack('<8s5I',raw[:28]); pos=28; comps=[]
    for i in range(nc):
        cid,orig,vc,ic,maxbone,sc,s0,s1,s2,iz,nb,flags,nd=struct.unpack('<13I',raw[pos:pos+52]); pos+=52
        comps.append((cid,ic,flags))
        if flags & 2: continue
        pos += vc*(s0+s1+s2)+ic*iz
        if ver==25: pos += 4+nb*12+nd*24
    masks=[]; names=[]
    for _ in range(nt):
        w,h,mips,size,fmt,srgb,sfmt,ssrgb,mask,pin,nl,kind=struct.unpack('<4I4iIi2I',raw[pos:pos+48]); pos+=48
        names.append(raw[pos:pos+nl].decode('ascii')); pos += nl+size
        masks.append(mask)
    return comps,masks,names

def profile_for(cat, order, legacy):
    p=dict(cat); p['components']={str(i):dict(cat['components'][str(src)]) for i,src in enumerate(order)}
    p['texture_names']=[v['name'] for v in cat['textures'].values()]; p['entries']={}; p['verified']=True
    raw=Path(legacy).read_bytes(); comps,masks,legacy_names=texture_masks(raw)
    # v24 stores the source texture slot token (the catalog hash key), while
    # BEMv1 material declarations use the exact native texture name.
    legacy_names=[cat.get('textures',{}).get(token,{}).get('name',token) for token in legacy_names]
    for cid,ic,flags in comps:
        if flags & 2: continue
        native=p['components'][str(cid)]
        slots=native.get('material_texture_properties', [])
        draws=[]
        for slot, props in enumerate(slots or [[]]):
            # The reviewed v24 material profile records shared source slots
            # (for example body D/N reused by cloth C7/C9). The explicit
            # component mask is the authoritative source-to-draw relation;
            # do not filter it by the target component's native texture name.
            textures=[t for t, name in enumerate(legacy_names) if masks[t] & (1<<cid)]
            draws.append(dict(start=0,count=ic,material_component=cid,material_slot=slot,textures=textures))
        native['v24_draws']=draws
    return p

def make(cat_path, legacy_path, out_path, order, package_id, name):
    cat=json.loads(Path(cat_path).read_text(encoding='utf-8-sig'))
    profile=profile_for(cat,order,legacy_path)
    target=bem.target_from_profile(profile,cat['character_id'],cat['world_resource'],cat['ui_resource'],cat['profile_id'],cat['revision'])
    b=bem.Builder(package_id,name,'BetterEndfield','1.0.0-android',target)
    legacy=Path(legacy_path).read_bytes()
    version=struct.unpack_from('<I',legacy,8)[0]
    b.add_legacy(legacy,profile,'default','默认外观')
    # BEMv1 draws must partition the index buffer. ComponentN's reviewed
    # route replays the source triangles once per native material slot; apply
    # the same explicit replay here for legacy v24 packages.
    for mesh in b.m['meshes']:
        if version != 24 or len(mesh['draws']) <= 1:
            continue
        raw=b.payloads[mesh['indices']]
        original_count=mesh['index_count']; mesh['indices']=b.payload(raw*len(mesh['draws']))
        mesh['index_count']=original_count*len(mesh['draws'])
        for slot, draw in enumerate(mesh['draws']):
            draw['start']=slot*original_count; draw['count']=original_count
    b.write(out_path)
    m,p=bem.read_package(out_path)
    print(out_path,Path(out_path).stat().st_size,'components',len(m['target']['components']),'textures',len(m['textures']))
    print('mesh names',[c['mesh_name'] for c in m['target']['components']])
    print('normal textures',[(t['original_name'],t['format'],t['width'],t['height']) for t in m['textures'] if '_N' in t['original_name'] or 'Normal' in t['original_name'] or '_HN' in t['original_name']][:20])

make('tools/CustomModel/catalog/pelica-pc.json','artifacts/BetterEndfield-win-x64/custom-model/pelica-lod0-native-materials.bempoc','artifacts/android-refactor/pelica-default.bem',list(range(10)),'android.chr0004.pelica','佩丽卡默认')
make('tools/CustomModel/catalog/chr_0030_zhuangfy.json','artifacts/BetterEndfield-win-x64/custom-model/zhuangfangyi-default-v25.bempoc','artifacts/android-refactor/zhuangfangyi-default.bem',[6,0,1,2],'android.chr0030.zhuangfy','庄方宜默认')
