from pathlib import Path
root=Path(__file__).resolve().parent
s=(root.parent/'preview_loop.py').read_text()
s=s.replace('scene.render.fps=60','scene.render.fps=30').replace('resolution_x=1100','resolution_x=720').replace('resolution_y=660','resolution_y=720')
s=s.replace('center,size=bounds(scene,arm,range(1,N*4,8));make_camera(center,size)',
'''center,size=Vector((0,0,1.0)),2.4;make_camera(center,size)
 bpy.ops.mesh.primitive_plane_add(size=200,location=(0,0,-.015));bpy.context.object.color=(.12,.15,.19,1)''')
s=s.replace("if side=='left':video(scene,out/f'{NAME}_loop_4cycles.mp4')",
'''scene.frame_set(N//2+1);scene.render.filepath=str(out/f'{side}_preview.png');bpy.ops.render.render(write_still=True)
 if side=='left':video(scene,out/f'{NAME}_loop_4cycles.mp4')''')
(root/'preview_glide.py').write_text(s)
