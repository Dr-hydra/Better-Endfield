import struct
from pathlib import Path
for p in ['artifacts/BetterEndfield-win-x64/custom-model/pelica-lod0-native-materials.bempoc','artifacts/BetterEndfield-win-x64/custom-model/pelica-lod0-geometry.bempoc']:
 raw=Path(p).read_bytes(); magic,v,nc,nt,*_=struct.unpack('<8s5I',raw[:28]); pos=28; print(p,magic,v,nc,nt)
 for i in range(nc):
  cid,orig,vc,ic,maxbone,sc,s0,s1,s2,iz,nb,flags,nd=struct.unpack('<13I',raw[pos:pos+52]); pos+=52; print(i,orig,vc,ic,s0,s1,s2,flags)
  if flags&2:continue
  pos+=vc*(s0+s1+s2)+ic*iz
  if v==25:pos+=4+nb*12+nd*24
