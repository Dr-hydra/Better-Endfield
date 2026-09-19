import copy
import json
import struct
import tempfile
import unittest
from pathlib import Path
from unittest.mock import patch

import component_auto as auto
from convert_efmi_poc import Source
from efmi_source import sections


class AutoTests(unittest.TestCase):
    def setUp(self):
        self.tmp=tempfile.TemporaryDirectory();self.root=Path(self.tmp.name)
        self.addCleanup(self.tmp.cleanup)
        # Synthetic role and arbitrary source component number; no real Mod
        # filename, material recipe or original Component0 assumption involved.
        seed=json.loads((Path(__file__).parent/'catalog/endminf-pc.json').read_text(encoding='utf-8'))
        native=copy.deepcopy(seed['components']['0'])
        native.update(mesh_name='fixture_mesh',original_index_count=30,bone_names=['root'],
            materials=['opaque','transparent'],material_textures=[[],[]])
        self.catalog=seed|dict(character_id='fixture',name='fixture',profile_id='fixture',
            components={'0':native},entries={'12345678':0},textures={},preserved_globals={})
        self.ini='''[Constants]
global $component_count = 1
global persist $choice = 0
global $draw_component_7_a = 1
[CommandListProcessToggles]
$draw_component_7_a = ($choice == 0)
[TextureOverride_EntryPoint_Component7]
hash = 12345678
match_index_count = 30
$object_detected = 1
if $mod_enabled && DRAW_TYPE == 4
$\\EFMIv1\\component_id = 7
$\\EFMIv1\\gpu_posed = 1
$lod_level = 0
CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandList_Draw
run = CommandList_Dispatch
endif
[CommandList_Dispatch]
handling = skip
$\\EFMIv1\\component_count = $component_count
$\\EFMIv1\\identification_min_components = 3
run = CommandList\\EFMIv1\\Object_ReadConfig
$\\EFMIv1\\lod_level = $lod_level
run = CommandList\\EFMIv1\\Component_ReadConfig
Pool\\EFMIv1\\Input_ObjectSpatialIdentity = ref Pool_ObjectSpatialIdentity
run = CommandList\\EFMIv1\\SpatialIdentity_IdentifyComponentInstances
run = CommandList\\EFMIv1\\Component_DrawInstances
[CommandList_Draw]
ib = ResourceIndex
vb0 = ResourcePosition
vb1 = ResourceUV
vb2 = ResourceSkin
if $draw_component_7_a
drawindexed = 3, 0, 0
endif
[ResourceIndex]
format = R16_UINT
filename = arbitrary-index.buf
[ResourcePosition]
stride = 16
filename = arbitrary-position.buf
[ResourceUV]
stride = 12
filename = arbitrary-uv.buf
[ResourceSkin]
stride = 12
filename = arbitrary-skin.buf
'''
        (self.root/'arbitrary-index.buf').write_bytes(struct.pack('<3H',0,1,2))
        (self.root/'arbitrary-position.buf').write_bytes(struct.pack('<12f',0,0,0,0,1,0,0,0,0,1,0,0))
        (self.root/'arbitrary-uv.buf').write_bytes(bytes(36))
        (self.root/'arbitrary-skin.buf').write_bytes(struct.pack('<4H4B',65535,0,0,0,0,0,0,0)*3)

    def prepare(self):
        (self.root/'anything.ini').write_text(self.ini,encoding='utf-8')
        src=Source(self.root)
        try:return auto.prepare(src,'anything.ini',[self.catalog])
        finally:src.close()

    def test_local_number_is_not_native_part_and_materials_replay(self):
        builder,report=self.prepare()
        self.assertEqual(report['components'][0]['source_component'],7)
        mesh=builder.m['meshes'][0]
        self.assertEqual([d['material_slot'] for d in mesh['draws']],[0,1])
        self.assertEqual(mesh['index_count'],6)
        self.assertEqual(builder.payloads[mesh['indices']],struct.pack('<6H',0,1,2,0,1,2))

    def test_default_off_is_hide(self):
        self.ini=self.ini.replace('$choice = 0','$choice = 1')
        b,_=self.prepare();self.assertEqual(b.m['appearances'][0]['components'][0]['operation'],'hide')

    def test_empty_draw_callback_hides_original(self):
        self.ini=self.ini.replace('drawindexed = 3, 0, 0','; no exported geometry')
        b,_=self.prepare();self.assertEqual(b.m['appearances'][0]['components'][0]['operation'],'hide')

    def test_count_mismatch_and_unknown_identity_rejected(self):
        for old,new in [('match_index_count = 30','match_index_count = 33'),('12345678','87654321')]:
            with self.subTest(new=new):
                sec=sections(self.ini.replace(old,new))
                with self.assertRaises(ValueError):auto.match_catalog(sec,[self.catalog])

    def test_ambiguous_catalog_does_not_choose_first(self):
        with self.assertRaisesRegex(ValueError,'CHARACTER_CATALOG'):
            auto.match_catalog(sections(self.ini),[self.catalog,self.catalog])

    def test_observed_but_unsupported_layout_is_explicitly_blocked(self):
        self.catalog['components']['0']['source_layout_supported']=False
        with self.assertRaisesRegex(ValueError,'NATIVE_LAYOUT_UNSUPPORTED'):self.prepare()

    def test_reimported_native_data_does_not_reapprove_source_hashes(self):
        self.catalog['mapping_status']='source-resource-identities-recheck-required'
        with self.assertRaisesRegex(ValueError,'CHARACTER_CATALOG'):self.prepare()

    def test_uint32_and_partial_draws(self):
        self.ini=self.ini.replace('R16_UINT','R32_UINT').replace('drawindexed = 3, 0, 0','drawindexed = 3, 3, 0')
        (self.root/'arbitrary-index.buf').write_bytes(struct.pack('<6I',2,1,0,0,1,2))
        b,_=self.prepare();mesh=b.m['meshes'][0]
        self.assertEqual(mesh['index_size'],4)
        self.assertEqual(b.payloads[mesh['indices']],struct.pack('<6I',0,1,2,0,1,2))

    def test_modified_entry_program_is_not_ignored(self):
        self.ini=self.ini.replace('$lod_level = 0','$lod_level = 1')
        with self.assertRaisesRegex(ValueError,'ENTRY_PROGRAM'):self.prepare()

    def test_unknown_condition_and_out_of_range_draw_fail(self):
        original=self.ini
        for statement in ['if $unknown','if $draw_component_7_a\ndrawindexed = 6, 0, 0']:
            self.ini=original.replace('if $draw_component_7_a',statement)
            with self.assertRaises(ValueError):self.prepare()

    def test_unknown_texture_identity_fails(self):
        self.ini+='\n[TextureOverride_Texture0]\nhash = abcdefab\nthis = ResourceTex\n'
        with self.assertRaisesRegex(ValueError,'TEXTURE_MAPPING'):self.prepare()

    def test_shared_original_texture_hash_creates_named_bindings_with_one_payload(self):
        from types import SimpleNamespace
        texture_sec=sections('[TextureOverride_Texture0]\nhash = abcdefab\nthis = ResourceTex\n')
        catalog={'textures':{'abcdefab':dict(name='body',aliases=['face'])}}
        payloads=[]
        def payload(data):payloads.append(data);return len(payloads)-1
        builder=SimpleNamespace(m={'textures':[]},payload=payload)
        with patch('component_auto.resource_bytes',return_value=b'dds'),patch('component_auto.parse_dds',return_value=(4,4,1,25,False,b'pixels')):
            mappings,_=auto.texture_table(None,'fixture.ini',texture_sec,catalog,builder)
        self.assertEqual(mappings,{'body':0,'face':1})
        self.assertEqual([t['payload'] for t in builder.m['textures']],[0,0])
        self.assertEqual(payloads,[b'pixels'])

    def test_changed_global_texture_is_not_silently_dropped(self):
        import hashlib
        self.ini+='\n[TextureOverride_Texture0]\nhash = abcdefab\nthis = ResourceTex\n'
        self.catalog['preserved_globals']={'abcdefab':dict(width=4,height=4,mips=1,format=25,
            source_payload_sha256=hashlib.sha256(b'native').hexdigest())}
        with patch('component_auto.resource_bytes',return_value=b'dds'),patch('component_auto.parse_dds',return_value=(4,4,1,25,False,b'changed')):
            with self.assertRaisesRegex(ValueError,'GLOBAL_TEXTURE'):self.prepare()

    def test_material_replay_preserves_full_pass_order(self):
        self.ini=self.ini.replace('drawindexed = 3, 0, 0','drawindexed = 3, 0, 0\ndrawindexed = 3, 0, 0')
        b,_=self.prepare()
        self.assertEqual([d['material_slot'] for d in b.m['meshes'][0]['draws']],[0,0,1,1])

    def test_ui_ready_is_based_on_actual_prepare(self):
        from bem_tool import inspect_source,convert_automatic
        self.prepare()
        with patch('component_auto.catalogs',return_value=[self.catalog]):
            report=inspect_source(self.root)
            self.assertTrue(report['conversion_ready'])
            out=self.root/'result.bem'
            result=convert_automatic(self.root,out)
            self.assertTrue(out.exists());self.assertFalse(result['render_verified'])


if __name__=='__main__':unittest.main()
