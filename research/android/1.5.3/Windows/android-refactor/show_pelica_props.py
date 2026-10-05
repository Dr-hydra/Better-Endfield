from pathlib import Path
import json
cat=json.loads(Path('tools/CustomModel/catalog/pelica-pc.json').read_text(encoding='utf-8-sig'))
for cid in ['4','7','8','9']:
 c=cat['components'][cid]; print('C',cid,c['mesh_name'])
 for i,props in enumerate(c['material_texture_properties']): print(i,props)
