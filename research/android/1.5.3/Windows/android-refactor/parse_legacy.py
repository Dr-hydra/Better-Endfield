import struct
from pathlib import Path
for p in ['artifacts/BetterEndfield-win-x64/custom-model/pelica-lod0-native-materials.bempoc','artifacts/BetterEndfield-win-x64/custom-model/zhuangfangyi-default-v25.bempoc']:
 raw=Path(p).read_bytes(); magic,ver,nc,nt,_,_=struct.unpack('<8s5I',raw[:28]); pos=28; print('\n',p,'v',ver,'nc',nc,'nt',nt)
 comps=[]
 for i in range(nc):
  vals=struct.unpack('<13I',raw[pos:pos+52]); pos+=52
  cid,orig,vc,ic,maxbone,sc,s0,s1,s2,iz,nb,flags,nd=vals
  print('comp',i,'cid',cid,'orig',orig,'vc',vc,'ic',ic,'strides',s0,s1,s2,'iz',iz,'nb',nb,'flags',flags,'nd',nd,'pos',pos)
  if flags & 2: continue
  pos += vc*(s0+s1+s2)+ic*iz
  if ver==25:
   layout=struct.unpack('<I',raw[pos:pos+4])[0]; pos+=4
   pos += nb*12 + nd*24
  else:
   # v24 no extra; data mapping unknown but likely draws not serialized
   pass
 print('after comps pos',pos,'remaining',len(raw)-pos)
