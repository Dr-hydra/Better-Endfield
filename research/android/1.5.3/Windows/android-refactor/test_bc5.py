from PIL import Image
import PIL
print(PIL.__version__)
print(Image.SAVE.keys())
im=Image.new('RGBA',(4,4),(128,128,255,255))
for kw in [{},{'compression':'bc5'},{'compression':'BC5'}]:
 try:
  import io
  b=io.BytesIO(); im.save(b,format='DDS',**kw); print('ok',kw,len(b.getvalue()),b.getvalue()[:32])
 except Exception as e: print('err',kw,type(e).__name__,e)
