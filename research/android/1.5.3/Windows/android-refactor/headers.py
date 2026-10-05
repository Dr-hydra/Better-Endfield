import struct
from pathlib import Path
for p in ['artifacts/BetterEndfield-win-x64/custom-model/pelica-lod0-native-materials.bempoc','artifacts/BetterEndfield-win-x64/custom-model/zhuangfangyi-default-v25.bempoc']:
 b=Path(p).read_bytes(); print(p,struct.unpack('<8s5I',b[:28]))
