from pathlib import Path
import json,sys
sys.path.insert(0,'tools/CustomModel'); import bem_v1 as b
from importlib.machinery import SourceFileLoader
migr=SourceFileLoader('m','artifacts/android-refactor/migrate_roles.py').load_module()
cat=json.loads(Path('tools/CustomModel/catalog/pelica-pc.json').read_text(encoding='utf-8-sig'))
raw=Path('artifacts/BetterEndfield-win-x64/custom-model/pelica-lod0-native-materials.bempoc').read_bytes(); comps,masks,toks=migr.texture_masks(raw)
for i,(tok,mask) in enumerate(zip(toks,masks)): print(i,tok,cat['textures'].get(tok,{}).get('name'),hex(mask))
