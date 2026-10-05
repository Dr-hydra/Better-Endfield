from pathlib import Path
import json
paths=[
 'tools/CustomModel/catalog/pelica-pc.json',
 'tools/CustomModel/catalog/chr_0030_zhuangfy.json',
 'tools/CustomModel/catalog/chr_0013_aglina.json',
 'tools/CustomModel/profiles/pelica-mod_7b260.source.json',
 'tools/CustomModel/profiles/pelica-mod_7b260.materials.json',
 'tools/CustomModel/profiles/pelica-mod_7b260.mapping.json',
 'tools/CustomModel/profiles/zhuangfangyi-061fe0.materials.json',
 'tools/CustomModel/profiles/gilberta-12.reviewed.json',
 'tools/CustomModel/profiles/gilberta-12.outfit-b.reviewed.json'
]
for p in paths:
    d=json.loads(Path(p).read_text(encoding='utf-8-sig'))
    print('\n===',p)
    print('keys=',list(d.keys()))
    for k in ['character_id','name','profile_id','revision','verified','world_resource','ui_resource','source','source_path','legacy_source','components','texture_names','appearances','material_rules','mapping','v24_draws','entries','textures','mapping_status','excluded_components']:
      if k in d:
        v=d[k]
        if isinstance(v,dict): print(k,'dict keys',list(v)[:20], 'count',len(v))
        elif isinstance(v,list): print(k,'list count',len(v), 'first',v[:2])
        else: print(k,repr(v)[:700])
