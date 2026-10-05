from pathlib import Path
import json
for p in ['tools/CustomModel/catalog/pelica-pc.json','tools/CustomModel/catalog/chr_0030_zhuangfy.json','tools/CustomModel/catalog/chr_0013_aglina.json']:
 d=json.loads(Path(p).read_text(encoding='utf-8-sig')); print('\n',p)
 for cid,c in d['components'].items():
  print(cid, {k:(len(v) if isinstance(v,list) else v) for k,v in c.items() if k in ['mesh_name','original_index_count','strides','attributes','bone_names','materials','v24_draws']})
 print('texture sample')
 for k,v in list(d['textures'].items())[:3]: print(k,v)
 print('entry sample')
 for k,v in list(d['entries'].items())[:2]: print(k,v)
