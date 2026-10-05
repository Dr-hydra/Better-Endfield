from pathlib import Path
import json,pprint
for p in ['tools/CustomModel/catalog/pelica-pc.json','tools/CustomModel/catalog/chr_0030_zhuangfy.json','tools/CustomModel/catalog/chr_0013_aglina.json']:
 d=json.loads(Path(p).read_text(encoding='utf-8-sig')); print(p); pprint.pp(d['components']['0']); print('texture name count',len([v['name'] for v in d['textures'].values()]))
