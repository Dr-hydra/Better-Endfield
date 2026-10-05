import struct
from pathlib import Path
for p in ['artifacts/BetterEndfield-win-x64/custom-model/pelica-lod0-native-materials.bempoc','artifacts/BetterEndfield-win-x64/custom-model/zhuangfangyi-default-v25.bempoc']:
 raw=Path(p).read_bytes(); pos=28; h=struct.unpack('<8s5I',raw[:28]); print('\n',p,h)
 for i in range(h[2]):
  vals=struct.unpack('<13I',raw[pos:pos+52]); pos+=52; print(i,vals[:7], 'nb',vals[10], 'flags',vals[11],'nd',vals[12]);
  # skip data by parsing approximate impossible; break after headers because data interleaved
  # cannot advance without full parse
  break
