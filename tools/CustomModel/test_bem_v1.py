import copy
import json
import os
from pathlib import Path
import struct
import subprocess
import tempfile
import unittest
import bem_v1 as bem
from bem_tool import check_geometry


def fixture():
    target=dict(character_id='chr_test',platform='windows-x64',profile_id='test',revision='1',snapshot='test',
                world_resource='world',ui_resource='ui',components=[
                    dict(id=0,mesh_name='mesh0',original_index_count=6,bone_names=['bone0'],materials=['material0']),
                    dict(id=1,mesh_name='mesh1',original_index_count=3,bone_names=['bone1'],materials=['material1'])])
    b=bem.Builder('test.package','测试包','Test','1',target)
    streams=[struct.pack('<12f',*([0.0]*12)),bytes(36),
             b''.join(struct.pack('<4H4B',65535,0,0,0,i%2,0,0,0) for i in range(3))]
    m=dict(vertex_count=3,index_count=6,index_size=2,
        streams=[dict(stride=s,payload=b.payload(raw)) for s,raw in zip((16,12,12),streams)],
        indices=b.payload(struct.pack('<6H',0,1,2,2,1,0)),
        attributes=[[0,0,3,0,0],[1,0,1,0,12],[4,0,2,1,0],[6,3,4,1,8],[12,4,4,2,0],[13,6,4,2,8]],
        bones=[dict(component=0,index=0,name='bone0'),dict(component=1,index=0,name='bone1')],
        draws=[dict(start=i*3,count=3,material_component=i,material_slot=0,material_name=f'material{i}',textures=[i]) for i in range(2)])
    b.m['meshes']=[m]
    b.m['textures']=[dict(width=4,height=4,mips=1,format=10,srgb=False,original_name='original',payload=b.payload(bytes([i+1])*8)) for i in range(2)]
    b.m['appearances']=[dict(id='default',name='替换',description='',components=[dict(target=0,operation='replace',mesh=0),dict(target=1,operation='keep')]),
                        dict(id='hidden',name='隐藏',description='',components=[dict(target=0,operation='hide'),dict(target=1,operation='keep')])]
    b.m['default_appearance_id']='default'
    # Section 15 baseline; every writer path now requires it, readers tolerate absence.
    b.m['game_baseline']={'windows-x64':'1.6.3','android-arm64':'1.6.4','hotfix':0}
    return b


class BemTests(unittest.TestCase):
    def test_set_game_baseline_copies_payload_bytes_and_is_idempotent(self):
        # A package built before section 15 looks exactly like this.
        self.b.m.pop('game_baseline'); self.b.write(self.path)
        with self.path.open('rb') as handle:
            _, _, _, header_size, _, manifest_size, count, _ = bem.HEADER.unpack(handle.read(bem.HEADER.size))
            handle.seek(header_size + manifest_size + count * bem.ENTRY.size)
            before = handle.read()
        baseline = {'windows-x64': '1.5.3', 'android-arm64': '1.5.3', 'hotfix': '0'}
        self.assertTrue(bem.set_game_baseline(self.path, baseline))
        manifest, payloads = bem.read_package(self.path)
        # The counter is normalised to its numeric spelling on write.
        self.assertEqual(manifest['game_baseline'],
                         {'windows-x64': '1.5.3', 'android-arm64': '1.5.3', 'hotfix': 0})
        self.assertEqual(payloads, self.b.payloads)
        self.assertEqual({k: v for k, v in manifest.items() if k != 'game_baseline'},
                         {k: v for k, v in self.b.m.items() if k != 'game_baseline'})
        with self.path.open('rb') as handle:
            _, _, _, header_size, _, manifest_size, count, _ = bem.HEADER.unpack(handle.read(bem.HEADER.size))
            handle.seek(header_size + manifest_size + count * bem.ENTRY.size)
            self.assertEqual(handle.read(), before)  # payload blocks are moved verbatim
        self.native(self.path, True)
        # The GUI runs this on every import, so a second pass must change nothing.
        self.assertFalse(bem.set_game_baseline(self.path, baseline))

    def test_backfill_uses_the_batch_table_and_never_guesses(self):
        from bem_tool import backfill_baselines
        self.b.m.pop('game_baseline'); self.b.write(self.path)
        baseline = {'windows-x64': '1.5.3', 'android-arm64': '1.5.3', 'hotfix': 0}
        self.assertEqual(backfill_baselines(self.path, baseline, dry_run=True)['pending'], 1)
        # A revision the table does not name is reported, not mapped to a guess.
        mapping = Path(self.tmp.name) / 'map.json'
        mapping.write_text(json.dumps({'batches': [{'revision': 'other', 'windows-x64': '1.0.0',
                                                    'android-arm64': '1.0.0', 'hotfix': 0}]}), encoding='utf-8')
        report = backfill_baselines(self.path, map_path=mapping)
        self.assertEqual((report['unmatched'], report['written']), (1, 0))
        self.assertNotIn('game_baseline', bem.read_package(self.path)[0])

    def test_game_baseline_absent_is_valid_for_older_packages(self):
        # Read side must accept every package written before section 15 existed.
        bem.validate_game_baseline({'schema': 1})
        b = fixture(); b.m.pop('game_baseline'); b.write(self.path)
        m, payloads = bem.read_package(self.path); bem.validate_manifest(m, len(payloads))
        self.native(self.path, True)

    def test_game_baseline_requires_three_keys_and_free_version_format(self):
        both = {'windows-x64': '1.6.3', 'android-arm64': '1.6.4'}
        bem.validate_game_baseline({'game_baseline': both | {'hotfix': 0}})
        # Versions are free-form; the counter accepts its JSON spelling or a digit string.
        bem.validate_game_baseline({'game_baseline': {'windows-x64': '2026-11-hf1',
                                                      'android-arm64': 'build_2601', 'hotfix': '3'}})
        for broken in (both,                                                        # missing hotfix
                       {'windows-x64': '1.6.3', 'hotfix': 0},                       # missing platform
                       {'windows-x64': '1.6.3', 'android-arm64': '', 'hotfix': 0},
                       {'windows-x64': '1.6.3版', 'android-arm64': '1.6.4', 'hotfix': 0},
                       both | {'hotfix': 0, 'ios': '1'},                            # unknown key
                       {'windows-x64': 'x' * 65, 'android-arm64': '1.6.4', 'hotfix': 0},
                       both | {'hotfix': -1},
                       both | {'hotfix': 'hf2'},
                       both | {'hotfix': True}):
            with self.subTest(broken=broken):
                with self.assertRaises(ValueError): bem.validate_game_baseline({'game_baseline': broken})

    def test_game_baseline_precedence_explicit_then_catalog_then_existing(self):
        catalog = {'game': {'windows-x64': '1.5.3', 'android-arm64': '1.5.3', 'hotfix': 1}}
        m = {}; bem.require_game_baseline(m, None, catalog)
        self.assertEqual(m['game_baseline'], catalog['game'])
        m = {}; bem.require_game_baseline(m, 'windows-x64=1.6.3,android-arm64=1.6.4,hotfix=2', catalog)
        self.assertEqual(m['game_baseline']['windows-x64'], '1.6.3')
        self.assertEqual(m['game_baseline']['hotfix'], '2')
        m = {'game_baseline': {'windows-x64': '1.5.2', 'android-arm64': '1.5.2', 'hotfix': 0}}
        bem.require_game_baseline(m, None)
        self.assertEqual(m['game_baseline']['windows-x64'], '1.5.2')
        with self.assertRaises(ValueError): bem.require_game_baseline({}, None)
        with self.assertRaises(ValueError): bem.require_game_baseline({}, 'windows-x64=1.6.3')
        with self.assertRaises(ValueError): bem.require_game_baseline({}, 'windows-x64=1.6.3,android-arm64=1.6.4')

    def test_rgba32_and_r8_roundtrip_native_validation(self):
        for fmt,bpp in ((4,4),(63,1)):
            with self.subTest(format=fmt):
                b=fixture()
                for t in b.m['textures']:
                    t.update(width=3,height=2,mips=2,format=fmt,srgb=False,payload=b.payload(bytes(7*bpp)))
                check_geometry(b.m,b.payloads);b.write(self.path)
                self.native(self.path,True)
                # Native and Python must reject a payload shorter than its mip chain.
                b.m['textures'][0]['payload']=b.payload(bytes(7*bpp-1))
                with self.assertRaises(ValueError):check_geometry(b.m,b.payloads)
                b.write(self.path);self.native(self.path,False)

    def setUp(self):
        self.tmp=tempfile.TemporaryDirectory(); self.path=Path(self.tmp.name)/'test.bem'; self.b=fixture(); self.b.write(self.path)
    def tearDown(self): self.tmp.cleanup()
    def native(self,path,ok):
        exe=os.environ.get('BEM_VALIDATOR')
        if exe:
            p=subprocess.run([exe,str(path)],capture_output=True)
            self.assertEqual(p.returncode==0,ok,p.stdout+p.stderr)
    def test_roundtrip_and_all_appearances(self):
        m,p=bem.read_package(self.path); check_geometry(m,p)
        self.assertEqual(m,self.b.m); self.assertEqual(p,self.b.payloads); self.native(self.path,True)
    def test_header_extents_and_old_format_rejected(self):
        original=self.path.read_bytes()
        for raw in (original[:-1],original+b'x',b'BEMPC25\0'+original[8:]):
            self.path.write_bytes(raw)
            with self.assertRaises(ValueError): bem.read_package(self.path)
            self.native(self.path,False)
    def test_bad_references_palette_and_draws(self):
        for change in (lambda m:m['meshes'][0]['draws'][1].update(start=0),
                       lambda m:m['meshes'][0]['bones'][0].update(index=-1),
                       lambda m:m['appearances'][1]['components'][0].update(operation='invalid'),
                       lambda m:m['meshes'][0]['streams'][0].update(payload=100)):
            m=copy.deepcopy(self.b.m);change(m)
            with self.assertRaises((ValueError,IndexError)):bem.write_package(self.path,m,self.b.payloads)
    def test_bad_index_native_rejected(self):
        p=copy.deepcopy(self.b.payloads);p[self.b.m['meshes'][0]['indices']]=struct.pack('<6H',9,1,2,2,1,0)
        with self.assertRaises(ValueError):check_geometry(self.b.m,p)
        bem.write_package(self.path,self.b.m,p);self.native(self.path,False)
    def test_directory_overlap_rejected(self):
        raw=bytearray(self.path.read_bytes());h=bem.HEADER.unpack_from(raw)
        struct.pack_into('<Q',raw,bem.HEADER.size+h[5]+8,0);self.path.write_bytes(raw)
        with self.assertRaises(ValueError):bem.read_package(self.path)
        self.native(self.path,False)
    def test_shared_payload_dedup(self):
        m=copy.deepcopy(self.b.m);p=list(self.b.payloads)
        p.append(p[0]);m['meshes'][0]['streams'][0]['payload']=len(p)-1
        bem.write_package(self.path,m,p);out,data=bem.read_package(self.path)
        self.assertEqual(len(data),len(self.b.payloads));self.assertEqual(out['meshes'][0]['streams'][0]['payload'],0)
    def test_unsupported_capability_and_default(self):
        for key,value in [('required_capabilities',['custom-shader']),('default_appearance_id','missing')]:
            m=copy.deepcopy(self.b.m);m[key]=value
            with self.assertRaises(ValueError):bem.write_package(self.path,m,self.b.payloads)
    def test_failed_conversion_preserves_output(self):
        old=self.path.read_bytes()
        cli=Path(__file__).with_name('bem_tool.py')
        p=subprocess.run([os.sys.executable,str(cli),'convert',str(self.path),'--recipe',str(Path(self.tmp.name)/'missing.json'),'-o',str(self.path)],capture_output=True)
        self.assertNotEqual(p.returncode,0);self.assertEqual(self.path.read_bytes(),old)


if __name__=='__main__':unittest.main()
