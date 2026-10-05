from pathlib import Path
import sys
sys.path.insert(0,'tools/CustomModel');import bem_v1 as b
m,p=b.read_package(Path('artifacts/android-refactor/gilberta-two-appearances-normal.bem')); print([(a['id'],a['name']) for a in m['appearances']],m['default_appearance_id'])
