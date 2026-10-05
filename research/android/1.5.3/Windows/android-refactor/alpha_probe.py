from pathlib import Path
import sys,numpy as np
from PIL import Image
sys.path.insert(0,'tools/CustomModel');import bem_v1 as b
m,p=b.read_package(Path('artifacts/android-refactor/pelica-default-normal.bem'))
for t in m['textures']:
 if t['original_name']=='T_actor_pelica_hair_01_D':
  raw=p[t['payload']]; im=Image.frombytes('RGBA',(t['width'],t['height']),raw,'bcn',(7,'BC7')); a=np.asarray(im)[:,:,3]; print('alpha',a.min(),a.max(),np.mean(a),np.percentile(a,[1,5,25,50,75,95,99]),'zero',np.mean(a==0))
