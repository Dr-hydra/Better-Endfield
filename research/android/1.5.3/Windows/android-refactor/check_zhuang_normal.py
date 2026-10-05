from pathlib import Path
import sys
sys.path.insert(0,'tools/CustomModel');import bem_v1 as b
m,p=b.read_package(Path('artifacts/android-refactor/zhuangfangyi-default-normal.bem'))
for i,t in enumerate(m['textures']): print(i,t['original_name'],t['width'],t['height'],t['mips'],t['format'],t['payload'],len(p[t['payload']]))
