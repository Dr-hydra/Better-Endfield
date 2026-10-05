"""Validate a loop FBX against its baked samples and render a four-cycle preview.
blender -b -P preview_loop.py -- <outDir> <codename> <periodFrames> [<fullDir> <fullFrames>]"""
import bpy,sys,json,struct
from pathlib import Path
from mathutils import Matrix,Vector,Quaternion
argv=sys.argv[sys.argv.index('--')+1:]
out=Path(argv[0]).resolve();NAME=argv[1];N=int(argv[2])
full=Path(argv[3]).resolve() if len(argv)>3 else None;FULL=int(argv[4]) if len(argv)>4 else 0
inp=json.loads((out/'bake-input.json').read_text());bones=inp['bones'];nb=len(bones)
report={}
def curves(action):
 if hasattr(action,'fcurves'):return list(action.fcurves)
 return [f for layer in action.layers for strip in layer.strips for bag in strip.channelbags for f in bag.fcurves]
def cycle(arm):
 for c in curves(arm.animation_data.action):
  if not any(m.type=='CYCLES' for m in c.modifiers):c.modifiers.new('CYCLES')
 arm.animation_data.action.update_tag();arm.update_tag();bpy.context.view_layer.update()
def style(end):
 scene=bpy.context.scene;scene.render.fps=60;scene.frame_start=1;scene.frame_end=end
 scene.unit_settings.system='METRIC';scene.unit_settings.scale_length=1
 scene.render.engine='BLENDER_WORKBENCH';scene.render.resolution_x=720;scene.render.resolution_y=720;scene.render.resolution_percentage=100
 if scene.world is None:scene.world=bpy.data.worlds.new('Review World')
 scene.world.color=(.065,.075,.09);sh=scene.display.shading;sh.light='STUDIO';sh.color_type='OBJECT';sh.show_shadows=True;sh.show_cavity=True;sh.cavity_type='BOTH';sh.background_type='WORLD'
 for ob in scene.objects:
  if ob.type=='MESH':
   n=ob.name.lower();ob.color=(.4,.5,.6,1) if 'cloth' in n else ((.22,.25,.32,1) if 'hair' in n else (.75,.64,.54,1))
   for p in ob.data.polygons:p.use_smooth=True
 return scene
def make_camera(center,size):
 scene=bpy.context.scene;cam=bpy.data.objects.new('Review Camera',bpy.data.cameras.new('Review Camera'));scene.collection.objects.link(cam);scene.camera=cam
 cam.location=center+Vector((1.2,-2,.5))*size;cam.rotation_euler=(center-cam.location).to_track_quat('-Z','Y').to_euler()
 cam.data.type='ORTHO';cam.data.ortho_scale=size*1.25
 return cam
def bounds(scene,arm,frames):
 points=[]
 for frame in frames:
  scene.frame_set(frame);dg=bpy.context.evaluated_depsgraph_get()
  for ob in scene.objects:
   if ob.type=='MESH':
    ev=ob.evaluated_get(dg);points.extend(ev.matrix_world@Vector(v) for v in ev.bound_box)
 lo=Vector([min(v[i] for v in points) for i in range(3)]);hi=Vector([max(v[i] for v in points) for i in range(3)])
 return (lo+hi)*.5,max(hi-lo)
def video(scene,path):
 if hasattr(scene.render.image_settings,'media_type'):scene.render.image_settings.media_type='VIDEO'
 scene.render.image_settings.file_format='FFMPEG';scene.render.ffmpeg.format='MPEG4';scene.render.ffmpeg.codec='H264';scene.render.ffmpeg.constant_rate_factor='MEDIUM'
 scene.render.filepath=str(path);scene.frame_set(1);bpy.ops.render.render(animation=True)
for side in ['left','right']:
 bpy.ops.wm.read_factory_settings(use_empty=True)
 bpy.ops.import_scene.fbx(filepath=str(out/f'{NAME}_spdash_{side}_loop_test.fbx'))
 scene=style(N*4);arm=next(o for o in scene.objects if o.type=='ARMATURE')
 assert tuple(arm.animation_data.action.frame_range)==(1.,float(N+1)),arm.animation_data.action.frame_range
 raw=(out/f'{side}-baked.f32').read_bytes();data=struct.unpack('<'+str(len(raw)//4)+'f',raw)
 maxerr=0;poses=[]
 for frame in range(N+1):
  scene.frame_set(frame+1);world=[]
  for i,b in enumerate(bones):
   k=(frame*nb+i)*10;a=data[k:k+10]
   local=Matrix.LocRotScale(Vector(a[4:7]),Quaternion((a[3],*a[:3])),Vector(a[7:10]))
   w=world[b['parent']]@local if b['parent']>=0 else local;world.append(w)
   if b['name'] not in arm.pose.bones:continue
   p=w.translation;expected=Vector((-p.x,-p.z,p.y));actual=(arm.matrix_world@arm.pose.bones[b['name']].matrix).translation
   maxerr=max(maxerr,(actual-expected).length)
  if frame in [0,N]:poses.append({b.name:(arm.matrix_world@b.matrix).copy() for b in arm.pose.bones})
 gap=max((poses[0][b].translation-poses[1][b].translation).length for b in poses[0])
 assert maxerr<.0005 and gap<.00001,(side,maxerr,gap)
 cycle(arm)
 periodic=0.;worst={}
 for frame in [1,2,N//5,N//2,N]:
  scene.frame_set(frame);a={b.name:(arm.matrix_world@b.matrix).translation.copy() for b in arm.pose.bones}
  scene.frame_set(frame+N*3)
  for b in arm.pose.bones:
   e=(a[b.name]-(arm.matrix_world@b.matrix).translation).length
   if e>periodic:periodic=e;worst={'frame':frame,'bone':b.name}
 assert periodic<.00001,(side,'periodic',periodic,worst)
 report[side]={'keys':N+1,'periodFrames':N,'meshCount':sum(o.type=='MESH' for o in scene.objects),'fbxPositionErrorM':maxerr,'endpointPositionGapM':gap,'cycleRepeatPositionErrorM':periodic}
 center,size=Vector((0,0,1.0)),2.4;make_camera(center,size)
 bpy.ops.mesh.primitive_plane_add(size=200,location=(0,0,-.015));bpy.context.object.color=(.12,.15,.19,1)
 for i in range(4):scene.timeline_markers.new('Cycle '+str(i+1),frame=1+i*N)
 scene.frame_set(1);bpy.ops.wm.save_as_mainfile(filepath=str(out/f'{NAME}_spdash_{side}_loop_test.blend'))
 scene.frame_set(N//2+1);scene.render.filepath=str(out/f'{side}_preview.png');bpy.ops.render.render(write_still=True)
 if side=='left':video(scene,out/f'{NAME}_loop_4cycles.mp4')
(out/'blender-loop-validation.json').write_text(json.dumps(report,indent=2));print('LOOP_VALIDATION',report)
if full:
 bpy.ops.wm.read_factory_settings(use_empty=True)
 bpy.ops.import_scene.fbx(filepath=str(full/f'{NAME}_spdash_left_full.fbx'))
 scene=style(FULL);arm=next(o for o in scene.objects if o.type=='ARMATURE')
 center,size=bounds(scene,arm,range(1,FULL,8));make_camera(center,size)
 bpy.ops.wm.save_as_mainfile(filepath=str(full/f'{NAME}_spdash_left_full.blend'))
 video(scene,full/f'{NAME}_full_clip.mp4')
print('LOOP_PREVIEW_OK')
