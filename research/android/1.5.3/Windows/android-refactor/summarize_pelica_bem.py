from pathlib import Path
import sys
sys.path.insert(0,'tools/CustomModel');import bem_v1 as b
m,p=b.read_package(Path('artifacts/android-refactor/pelica-default-normal.bem'))
a=m['appearances'][0]
for op in a['components']:
 if op['operation']!='replace':continue
 mesh=m['meshes'][op['mesh']]; c=m['target']['components'][op['target']]
 print(op['target'],c['mesh_name'],'idx',mesh['index_count'],'draws',len(mesh['draws']))
 for d in mesh['draws']:
  print(' ',d['start'],d['count'],'mat',d['material_name'],'tex',[m['textures'][t]['original_name'] for t in d['textures']])
