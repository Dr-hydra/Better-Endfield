from pathlib import Path
import sys
sys.path.insert(0,'tools/CustomModel');import bem_v1 as b
for p in ['artifacts/BetterEndfield-win-x64/custom-model/gilberta-two-appearances.bem','artifacts/android-refactor/gilberta-two-appearances-normal.bem']:
 m,pay=b.read_package(Path(p)); print(p)
 for t in m['textures']:
  if '_N' in t['original_name']: print(t['original_name'],t['format'],t['width'],t['height'],len(pay[t['payload']]))
