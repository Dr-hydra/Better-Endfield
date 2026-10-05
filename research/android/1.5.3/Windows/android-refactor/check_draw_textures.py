from pathlib import Path
import sys,struct
sys.path.insert(0,'tools/CustomModel'); import bem_v1 as b
# print v24 raw names
from importlib.machinery import SourceFileLoader
mod=SourceFileLoader('m','artifacts/android-refactor/migrate_roles.py').load_module()
raw=Path('artifacts/BetterEndfield-win-x64/custom-model/pelica-lod0-native-materials.bempoc').read_bytes(); print(mod.texture_masks(raw)[2])
m,p=b.read_package(Path('artifacts/android-refactor/pelica-default.bem')); print([t['original_name'] for t in m['textures']]);
for d in m['meshes'][0]['draws']: print(d)
