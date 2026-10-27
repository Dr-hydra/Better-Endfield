"""Read-only manager preview integration tests: pass BemCompose.exe."""
import copy,json,struct,subprocess,sys,tempfile,unittest
from pathlib import Path
sys.path.insert(0,str(Path(__file__).resolve().parents[2]/'tools/CustomModel'))
from test_bem_v1 import fixture
from test_bem_v11 import fixture as options
from test_bem_v13 import fixture as morph
from test_bem_v14 import example
import bem_projects
EXE=Path(sys.argv.pop(1)).resolve()
class PreviewTests(unittest.TestCase):
 def setUp(self):
  self.temp=tempfile.TemporaryDirectory(prefix='bem-preview-');self.addCleanup(self.temp.cleanup);self.root=Path(self.temp.name)
 def preview(self,builder,selection='',parameters='',resource='',zip=False,error=None,gpu=None):
  path=self.root/'source.bem';builder.write(path)
  if zip:
   archive=self.root/'source.zip';bem_projects.pack_zip(path,archive);path=archive
  before=path.read_bytes();output=self.root/'scene'
  run=subprocess.run([str(EXE),'preview','--package',str(path),'--output-dir',str(output),'--options',selection,'--parameters',parameters,'--resource',resource,*(['--gpu',json.dumps(gpu)] if gpu is not None else [])],capture_output=True,text=True,encoding='utf-8')
  report=json.loads(run.stdout);self.assertEqual(before,path.read_bytes())
  if error:
   self.assertNotEqual(run.returncode,0,report);self.assertIn(error,report['error']);self.assertFalse(output.exists());return
  self.assertEqual(run.returncode,0,report);scene=json.loads((output/'scene.json').read_text(encoding='utf-8'))
  self.last_report=report['result']
  for mesh in scene['meshes']:
   self.assertEqual((output/mesh['positionsBlob']).stat().st_size,mesh['vertexCount']*12)
   for draw in mesh['draws']:
    self.assertLessEqual((draw['first']+draw['count'])*mesh['indexSize'],(output/draw['indicesBlob']).stat().st_size)
  return scene,output
 def native(self,b,**changes):
  root=self.root/'native';root.mkdir()
  (root/'p.bin').write_bytes(struct.pack('<9f',0,0,0,1,0,0,0,0,1));(root/'u.bin').write_bytes(struct.pack('<6f',0,0,1,0,0,1));(root/'i.bin').write_bytes(struct.pack('<3I',0,1,2));(root/'t.bin').write_bytes(bytes([255,0,0,255])*16)
  pack=dict(schema=1,kind='native-head-preview-prototype',character='chr_test',meshes=[dict(vertex_count=3,payloads=dict(positions='p.bin',uvs='u.bin',indices='i.bin'),submeshes=[dict(index_count=3)])],textures=[dict(id='native',name='original',payload='t.bin',width=4,height=4,mips=1,format='RGBA32',srgb=False)],renderers=[])
  for c in b.m['target']['components']:
   pack['renderers'].append(dict(name=c['mesh_name'],path='world/'+c['mesh_name'],resource_root='world',resource_asset_paths=[],previewMesh=0,materials=[dict(slot=i,name=name,shader=dict(asset_paths=[]),textures=[dict(property='_BaseMap',id='native',name='original')]) for i,name in enumerate(c['materials'])]))
  pack.update(changes);(root/'pack.json').write_text(json.dumps(pack));return dict(nativeAssets=str(root))
 def test_desktop_uses_memory_budget_without_changing_mobile_default(self):
  self.preview(fixture())
  self.assertEqual(self.last_report['scene_budget_bytes'],192*1024*1024)
  run=subprocess.run([str(EXE),'preview','--package',str(self.root/'source.bem'),'--output-dir',str(self.root/'desktop'),'--gpu',json.dumps(dict(desktop=True))],capture_output=True,text=True,encoding='utf-8')
  report=json.loads(run.stdout);self.assertEqual(run.returncode,0,report)
  self.assertGreaterEqual(report['result']['scene_budget_bytes'],256*1024*1024)
  self.assertLessEqual(report['result']['scene_budget_bytes'],4*1024*1024*1024)
 def test_native_keep_and_replace(self):
  b=options();scene,_=self.preview(b,gpu=self.native(b));self.assertEqual(scene['nativeCompletion']['status'],'ready');self.assertEqual([m['component'] for m in scene['meshes'] if m.get('native')],[1]);self.assertEqual(len(scene['meshes']),2)
 def test_native_hide(self):
  b=options();scene,_=self.preview(b,'outfit:b&part:off',gpu=self.native(b));self.assertEqual(scene['nativeCompletion']['meshes'],0)
 def test_native_texture_fallback(self):
  b=options();b.m['meshes'][0]['draws'][0]['textures']=[];scene,_=self.preview(b,gpu=self.native(b));self.assertEqual(scene['meshes'][0]['draws'][0]['textures'],[0]);self.assertEqual(scene['textures'][0]['family'],'rgba32')
 def test_native_override_wins(self):
  b=options();scene,out=self.preview(b,gpu=self.native(b));self.assertEqual(scene['meshes'][0]['draws'][0]['textures'],[0]);self.assertEqual(scene['textures'][0]['name'],'original');self.assertNotEqual((out/scene['textures'][0]['blob']).read_bytes()[:4],bytes([255,0,0,255]))
 def test_native_wrong_character(self):
  b=options();scene,_=self.preview(b,gpu=self.native(b,character='other'));self.assertEqual(len(scene['meshes']),1);self.assertEqual(scene['nativeCompletion']['status'],'unavailable')
 def test_native_invalid_path_graceful(self):
  b=options();caps=self.native(b);path=Path(caps['nativeAssets'])/'pack.json';p=json.loads(path.read_text());p['textures'][0]['payload']='../source.bem';path.write_text(json.dumps(p));scene,_=self.preview(b,gpu=caps);self.assertEqual(scene['nativeCompletion']['status'],'invalid');self.assertEqual(len(scene['meshes']),1)
 def test_native_bad_geometry_graceful(self):
  b=options();caps=self.native(b);(Path(caps['nativeAssets'])/'i.bin').write_bytes(struct.pack('<3I',0,1,9));scene,_=self.preview(b,gpu=caps);self.assertEqual(scene['nativeCompletion']['status'],'partial');self.assertEqual(len(scene['meshes']),1)
 def test_native_keep_override(self):
  b=options();b.m['required_capabilities'].append('keep-material-textures');b.m['component_rules'][1]['candidates']=[dict(operation='keep',material_overrides=[dict(material_slot=0,material_name='material1',textures=[0])])];scene,out=self.preview(b,gpu=self.native(b));native=next(m for m in scene['meshes'] if m.get('native'));texture=scene['textures'][native['draws'][0]['textures'][0]];self.assertNotEqual((out/texture['blob']).read_bytes()[:4],bytes([255,0,0,255]))
 def test_native_selection_on(self):
  b=options();scene,_=self.preview(b,'outfit:b&part:on',gpu=self.native(b));self.assertEqual(scene['nativeCompletion']['meshes'],1)
 def test_native_ui_root(self):
  b=options();caps=self.native(b);p=Path(caps['nativeAssets'])/'pack.json';pack=json.loads(p.read_text());
  for r in pack['renderers']:r['resource_root']='ui';r['path']='ui/'+r['name']
  p.write_text(json.dumps(pack));scene,_=self.preview(b,gpu=caps);self.assertEqual(scene['nativeCompletion']['status'],'unmatched')
 def test_native_explicit_resource(self):
  b=example.fixture();caps=self.native(b,character=b.m['target']['id']);p=Path(caps['nativeAssets'])/'pack.json';pack=json.loads(p.read_text());c=b.m['target']['components'][0];pack['renderers']=[dict(name=c['mesh_name'],path='root/'+c['renderer_path'],resource_root='root',resource_asset_paths=[b.m['target']['resources'][0]['asset_path']],previewMesh=0,materials=[dict(slot=0,name='material',shader=dict(asset_paths=[]),textures=[dict(property='_BaseMap',id='native',name='original')])])];p.write_text(json.dumps(pack));scene,_=self.preview(b,resource='body',gpu=caps);self.assertEqual(scene['nativeCompletion']['status'],'ready')
 def test_native_texture_budget_not_starved(self):
  b=options();b.m['textures'][0].update(width=4096,height=4096,mips=1,payload=b.payload(bytes([1])*8388608));caps=self.native(b);caps['maxTextureSize']=4096;scene,_=self.preview(b,gpu=caps);native=next(m for m in scene['meshes'] if m.get('native'));self.assertIsNotNone(scene['textures'][native['draws'][0]['textures'][0]]['blob']);self.assertFalse(scene['warnings'])
 def test_legacy(self):
  scene,_=self.preview(fixture());self.assertEqual(len(scene['meshes']),1)
 def test_controls_legacy(self):
  scene,_=self.preview(fixture());c=scene['controls'];self.assertFalse(c['composable']);self.assertTrue(c['appearances']);self.assertEqual(c['optionGroups'],[])
 def test_controls_hidden_memory(self):
  scene,_=self.preview(options(),'outfit:a&part:off');c=scene['controls']
  self.assertTrue(c['composable']);self.assertEqual(len(c['optionGroups']),2);self.assertEqual(c['availableGroups'],['outfit']);self.assertIn('part:off',scene['selection'])
 def test_controls_reactivated_memory(self):
  scene,_=self.preview(options(),'outfit:b&part:off');self.assertEqual(scene['controls']['availableGroups'],['outfit','part']);self.assertIn('part:off',scene['selection'])
 def test_controls_parameter_memory(self):
  b=morph();b.m['parameters'][0]['available_when']={'eq':['outfit','b']}
  scene,_=self.preview(b,'outfit:a&part:off','body:700&future:250')
  self.assertEqual(scene['controls']['availableParameters'],[]);self.assertIn('body:700',scene['parameters']);self.assertIn('future:250',scene['parameters']);self.assertEqual(scene['controls']['parameters'][0]['step'],1)
 def test_removed_slider_legacy(self):
  scene,_=self.preview(fixture(),parameters='old:250');self.assertEqual(scene['parameters'],'old:250')
 def test_bad_remembered_tick(self):self.preview(morph(),parameters='old:1001',error='Invalid parameter tick')
 def test_duplicate_remembered_tick(self):self.preview(morph(),parameters='old:250&old:500',error='Duplicate parameter')
 def test_controls_morph_draft_geometry(self):
  scene,out=self.preview(morph(),'outfit:b&part:on','body:500')
  self.assertEqual(scene['controls']['availableParameters'],['body']);self.assertIn('body:500',scene['parameters'])
  self.assertEqual(struct.unpack_from('<3f',(out/scene['meshes'][0]['positionsBlob']).read_bytes()),(1,2,-1))
 def test_session_texture_cache_reuses_data_and_invalidates_changed_package(self):
  import os
  caps=dict(textureCache=str(self.root/'cache'))
  scene,output=self.preview(fixture(),gpu=caps)
  def again(name):
   out=self.root/name
   run=subprocess.run([str(EXE),'preview','--package',str(self.root/'source.bem'),'--output-dir',str(out),'--gpu',json.dumps(caps)],capture_output=True,text=True,encoding='utf-8')
   body=json.loads(run.stdout);self.assertEqual(run.returncode,0,body)
   return body['result'],json.loads((out/'scene.json').read_text()),out
  report,second,out=again('second')
  self.assertEqual(report['texture_cache_hits'],2)
  for a,b in zip(scene['textures'],second['textures']):
   self.assertEqual((output/a['blob']).read_bytes(),(out/b['blob']).read_bytes())
  path=self.root/'source.bem';stamp=path.stat().st_mtime_ns
  builder=fixture();payload=builder.m['textures'][0]['payload'];builder.payloads[payload]=bytes([77])*8;builder.write(path)
  os.utime(path,ns=(stamp+1_000_000_000,stamp+1_000_000_000))
  report,changed,out=again('changed')
  self.assertEqual(report['texture_cache_hits'],0)
  self.assertNotEqual(scene['textures'][0]['cacheKey'],changed['textures'][0]['cacheKey'])
  (self.root/'cache'/'t0.json').write_text('{',encoding='utf-8')
  report,_,_=again('damaged-cache')
  self.assertEqual(report['texture_cache_hits'],0)

 def test_zip(self): self.preview(fixture(),zip=True)
 def test_current_options(self):
  scene,_=self.preview(options(),'outfit:b&part:off');self.assertIn('outfit:b',scene['selection'])
 def test_boolean_false(self):
  b=options();b.m['component_rules'][0]['candidates']=[dict(operation='hide',when=False),dict(operation='replace',mesh=0,when=True)]
  scene,_=self.preview(b);self.assertEqual(len(scene['meshes']),1)
 def test_hidden_configuration(self):
  b=options();b.m['component_rules'][0]['candidates']=[dict(operation='hide')]
  scene,_=self.preview(b);self.assertFalse(scene['meshes'])
 def test_invalid_combination(self):
  b=options();b.m['selection_constraints']=[{'eq':['outfit','a']}]
  self.preview(b,'outfit:b',error='Unreachable')
 def test_invalid_resource(self):self.preview(example.fixture(),resource='missing',error='resource')
 def test_explicit_resource(self):
  scene,_=self.preview(example.fixture(),resource='ultimate');self.assertEqual(scene['resource'],'ultimate');self.assertEqual(len(scene['resources']),3)
 def test_no_overwrite(self):
  (self.root/'scene').mkdir();(self.root/'scene'/'marker').write_text('keep')
  b=fixture();p=self.root/'source.bem';b.write(p)
  r=subprocess.run([str(EXE),'preview','--package',str(p),'--output-dir',str(self.root/'scene')],capture_output=True)
  self.assertNotEqual(r.returncode,0);self.assertEqual((self.root/'scene'/'marker').read_text(),'keep')
 def test_astc(self):
  b=fixture();pixels=b'\xfc\xfd\xff\xff\xff\xff\xff\xff'+struct.pack('<4H',65535,0,0,65535)
  for texture in b.m['textures']:
   texture.update(format=48,payload=b.payload(pixels))
  scene,output=self.preview(b);textures=[t for t in scene['textures'] if t['blob']]
  self.assertTrue(textures);self.assertEqual(textures[0]['family'],'rgba32');rgba=(output/textures[0]['blob']).read_bytes();self.assertEqual(rgba[:4],bytes([255,0,0,255]))
 def test_large_single_mip(self):
  b=fixture();red=struct.pack('<HHI',0xf800,0,0);payload=b.payload(red*(512*256))
  for texture in b.m['textures']:texture.update(width=2048,height=1024,mips=1,format=10,payload=payload)
  scene,out=self.preview(b);texture=next(t for t in scene['textures'] if t['blob']);self.assertEqual((texture['width'],texture['height']),(1024,512));self.assertEqual(texture['family'],'rgba32');self.assertEqual((out/texture['blob']).read_bytes()[:4],bytes([255,0,0,255]))
 def test_streamed_12k_atlas(self):
  b=options();colors=[struct.pack('<HHI',c,0,0)*3072 for c in [0xf800,0x07e0,0x001f]]
  payload=b.payload(b''.join(colors)*1024)
  for texture in b.m['textures']:texture.update(width=12288,height=12288,mips=1,format=10,payload=payload)
  scene,out=self.preview(b);texture=next(t for t in scene['textures'] if t['blob'])
  self.assertEqual((texture['width'],texture['height']),(768,768));self.assertFalse(scene['warnings'])
  rgba=(out/texture['blob']).read_bytes()
  for y,color in enumerate([bytes([255,0,0,255]),bytes([0,255,0,255]),bytes([0,0,255,255])]):
   self.assertEqual(rgba[y*768*4:y*768*4+4],color)
   self.assertEqual(rgba[(y*768+767)*4:(y*768+768)*4],color)
 def test_streamed_odd_mips(self):
  b=fixture();red=struct.pack('<HHI',0xf800,0,0);green=struct.pack('<HHI',0x07e0,0,0)
  data=red*((2052+3)//4)*((1028+3)//4)+green*((1026+3)//4)*((514+3)//4)+red*((513+3)//4)*((257+3)//4)
  payload=b.payload(data)
  for texture in b.m['textures']:texture.update(width=2052,height=1028,mips=3,format=10,payload=payload)
  scene,out=self.preview(b);texture=next(t for t in scene['textures'] if t['blob'])
  self.assertEqual((texture['width'],texture['height']),(513,257));self.assertEqual((out/texture['blob']).read_bytes()[:4],bytes([255,0,0,255]))
 def test_gpu_bc7_preserves_blocks(self):
  b=fixture();pixels=bytes([64])+bytes(range(1,16));payload=b.payload(pixels)
  for texture in b.m['textures']:texture.update(width=4,height=4,mips=1,format=25,payload=payload)
  scene,out=self.preview(b,gpu={'bc7':True,'maxTextureSize':16384});t=next(t for t in scene['textures'] if t['blob'])
  self.assertTrue(t['gpuCompressed']);self.assertEqual(t['family'],'bc7');self.assertEqual((out/t['blob']).read_bytes(),pixels);self.assertEqual(t['sourceWidth'],4)
 def test_gpu_bc7_authored_mip(self):
  b=fixture();pixels=b'\x40'+bytes(15);mip=b'\x40'+bytes([33])*15;payload=b.payload(pixels*4+mip)
  for texture in b.m['textures']:texture.update(width=8,height=8,mips=2,format=25,payload=payload)
  scene,out=self.preview(b,gpu={'bc7':True,'maxTextureSize':4});t=next(t for t in scene['textures'] if t['blob'])
  self.assertEqual((t['width'],t['height'],t['mip']),(4,4,1));self.assertEqual((out/t['blob']).read_bytes(),mip)
 def test_gpu_bc7_unsupported_falls_back(self):
  b=fixture();payload=b.payload(b'\x40'+bytes(15))
  for texture in b.m['textures']:texture.update(width=4,height=4,mips=1,format=25,payload=payload)
  scene,_=self.preview(b,gpu={'bc7':False,'maxTextureSize':4096});t=next(t for t in scene['textures'] if t['blob'])
  self.assertEqual(t['family'],'rgba32');self.assertFalse(t.get('gpuCompressed',False))
 def test_gpu_bc7_no_mip_exceeds_device(self):
  b=fixture();payload=b.payload((b'\x40'+bytes(15))*4)
  for texture in b.m['textures']:texture.update(width=8,height=8,mips=1,format=25,payload=payload)
  scene,_=self.preview(b,gpu={'bc7':True,'maxTextureSize':4});t=next(t for t in scene['textures'] if t['blob'])
  self.assertEqual((t['width'],t['height']),(4,4));self.assertEqual(t['family'],'rgba32')
 def test_gpu_astc_preserves_blocks(self):
  for fmt,block in [(48,4),(49,5),(50,6)]:
   with self.subTest(format=fmt):
    b=fixture();pixels=b'\xfc\xfd\xff\xff\xff\xff\xff\xff'+struct.pack('<4H',65535,0,0,65535)
    for t in b.m['textures']:t.update(width=block,height=block,mips=1,format=fmt,payload=b.payload(pixels))
    scene,out=self.preview(b,gpu={'astc':True,'maxTextureSize':16384});t=next(t for t in scene['textures'] if t['blob'])
    self.assertTrue(t['gpuCompressed']);self.assertEqual(t['family'],'astc');self.assertEqual(t['block'],block);self.assertEqual((out/t['blob']).read_bytes(),pixels)
    # Each helper invocation needs a fresh output directory.
    self.root=self.root/'next';self.root.mkdir()
 def test_gpu_astc_authored_mip(self):
  b=fixture();red=b'\xfc\xfd\xff\xff\xff\xff\xff\xff'+struct.pack('<4H',65535,0,0,65535);green=red[:8]+struct.pack('<4H',0,65535,0,65535)
  for t in b.m['textures']:t.update(width=12,height=12,mips=2,format=50,payload=b.payload(red*4+green))
  scene,out=self.preview(b,gpu={'astc':True,'maxTextureSize':6});t=next(t for t in scene['textures'] if t['blob'])
  self.assertEqual((t['width'],t['height'],t['mip']),(6,6,1));self.assertEqual((out/t['blob']).read_bytes(),green)
 def test_astc_streamed_large_fallback(self):
  b=fixture();red=b'\xfc\xfd\xff\xff\xff\xff\xff\xff'+struct.pack('<4H',65535,0,0,65535)
  for t in b.m['textures']:t.update(width=8192,height=8192,mips=1,format=50,payload=b.payload(red*((8192+5)//6)**2))
  scene,out=self.preview(b,gpu={'astc':False,'maxTextureSize':1024});t=next(t for t in scene['textures'] if t['blob'])
  self.assertEqual((t['width'],t['height']),(1024,1024));self.assertEqual(t['family'],'rgba32');self.assertEqual((out/t['blob']).read_bytes()[:4],bytes([255,0,0,255]));self.assertFalse(scene['warnings'])
 def test_astc_no_mip_exceeds_device(self):
  b=fixture();red=b'\xfc\xfd\xff\xff\xff\xff\xff\xff'+struct.pack('<4H',65535,0,0,65535)
  for t in b.m['textures']:t.update(width=12,height=12,mips=1,format=50,payload=b.payload(red*4))
  scene,out=self.preview(b,gpu={'astc':True,'maxTextureSize':6});t=next(t for t in scene['textures'] if t['blob'])
  self.assertEqual((t['width'],t['height']),(6,6));self.assertEqual(t['family'],'rgba32');self.assertEqual((out/t['blob']).read_bytes()[:4],bytes([255,0,0,255]))
 def test_index32(self):
  scene,_=self.preview(fixture());self.assertIn(scene['meshes'][0]['indexSize'],[2,4])
 def test_morph(self):self.preview(morph())
if __name__=='__main__':unittest.main()
