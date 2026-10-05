from pathlib import Path
import json
for p in ['tools/CustomModel/catalog/pelica-pc.json','tools/CustomModel/catalog/chr_0030_zhuangfy.json','tools/CustomModel/catalog/chr_0013_aglina.json']:
 d=json.loads(Path(p).read_text(encoding='utf-8-sig')); print('\n',p)
 pairs={}
 for c in d['components'].values():
  for arr in c.get('material_texture_properties',[]):
   for x in arr:
    if x['property'] in ['_BumpMap','_NormalMap','_SplitNormalMap']:
     pairs[x['name']]=x['property']
 print(pairs)
