from pathlib import Path
import sys
sys.path.insert(0,'tools/CustomModel');import bem_v1 as b
m,p=b.read_package(Path('artifacts/android-refactor/pelica-default-normal.bem'))
print([t['original_name'] for t in m['textures']])
