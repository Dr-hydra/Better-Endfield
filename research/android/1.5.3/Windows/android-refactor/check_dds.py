from PIL import Image
import io,struct
im=Image.new('RGBA',(4,4),(128,128,255,255)); b=io.BytesIO(); im.save(b,format='DDS',compression='bc5'); d=b.getvalue(); print(len(d),d[84:88],struct.unpack_from('<I',d,80)[0])
