from pathlib import Path
import sys
sys.path.insert(0,'tools/CustomModel')
import bem_v1 as b
for p in ['artifacts/android-refactor/pelica-default.bem','artifacts/android-refactor/zhuangfangyi-default.bem','artifacts/BetterEndfield-win-x64/custom-model/gilberta-two-appearances.bem','artifacts/android-refactor/endmin-normal-z-probe.bem']:
 m,pl=b.read_package(Path(p)); print('\n',p)
 for i,t in enumerate(m['textures']):
  n=t['original_name'];
  if any(x in n for x in ['_N','Normal','_HN']): print(i,n,'fmt',t['format'],'mips',t['mips'],'size',len(pl[t['payload']]),'srgb',t['srgb'])
