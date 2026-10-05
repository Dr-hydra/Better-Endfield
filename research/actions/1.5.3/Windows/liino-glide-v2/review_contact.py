from pathlib import Path
import cv2
from PIL import Image, ImageDraw
r=Path(__file__).resolve().parent
canvas=Image.new('RGB',(1440,800),'#111923')
d=ImageDraw.Draw(canvas)
rows=[('final',[0,10,20,30],'LOW - selected: pelvis range 3.9 cm; 0.67 s loop'),
      ('high',[0,5,10,15],'HIGH - comparison: pelvis range 11.6 cm; 0.67 s loop')]
for row,(name,frames,label) in enumerate(rows):
    d.text((12,row*400+8),label,fill='white')
    cap=cv2.VideoCapture(str(r/name/'liino_loop_4cycles.mp4'))
    for col,frame in enumerate(frames):
        cap.set(cv2.CAP_PROP_POS_FRAMES,frame)
        ok,img=cap.read()
        assert ok
        canvas.paste(Image.fromarray(cv2.cvtColor(img,cv2.COLOR_BGR2RGB)).resize((360,360)),(col*360,row*400+32))
    cap.release()
canvas.save(r/'low_high_comparison.png')
