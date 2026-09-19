import unittest
from efmi_source import sections
from source_classification import entry_family, classify


class ClassificationTests(unittest.TestCase):
    def test_component_name_must_be_an_entry(self):
        self.assertEqual(entry_family({'TextureOverride_EntryPoint_Component10':''}),'component-n')
        self.assertEqual(entry_family({'TextureOverride_Component2_LOD1':''}),'component-n')
        self.assertIsNone(entry_family({'TextureOverride_NotAComponentTexture':''}))
    def test_standard_component_is_candidate_not_ready(self):
        p=classify('component-n',[('mod.ini',{'TextureOverride_Component0':'hash = 12345678'})])
        self.assertEqual(p['status'],'standard_candidate'); self.assertFalse(p['conversion_ready'])
    def test_merged_efmi_shader_is_not_author_shader(self):
        sec=sections('[CommandListSkin]\nrun = CustomShader\\EFMIv1\\MergedSkeleton\nvb2->ElementFormat(BLENDINDICES, 0) = R16G16B16A16_UINT\n')
        p=classify('hash-lod',[('mod.ini',sec)])
        self.assertEqual(p['status'],'requires_mapping'); self.assertIn('merged_skeleton',p['features']);self.assertIn('bone_indices_u16',p['features'])
    def test_custom_shader_on_companion_ini_blocks_automation(self):
        p=classify('hash-lod',[('mod.ini',{}),('res/effect.ini',{'CustomShaderRedirect':'cs = redirect.hlsl'})])
        self.assertEqual(p['status'],'manual_only');self.assertEqual(p['evidence'][0]['file'],'res/effect.ini')
    def test_external_material_and_shape_keys(self):
        for program in ['run = CommandList\\RabbitFX\\SetTextures','run = CommandList_ShapeKeys_Apply']:
            self.assertEqual(classify('component-n',[('mod.ini',{'CommandListDraw':program})])['status'],'manual_only')
    def test_texture_slot_data_needs_mapping(self):
        p=classify('component-n',[('mod.ini',{'CommandListDraw':'ps-t14 = ResourceTexture'})])
        self.assertEqual(p['status'],'requires_mapping')
        self.assertIn('ps-t*',''.join(p['reasons']))
    def test_comments_do_not_trigger_shader_calls(self):
        p=classify('component-n',[('mod.ini',{'CommandListDraw':'; run = CustomShaderFake'})])
        self.assertEqual(p['status'],'standard_candidate')
    def test_mixed_entries_need_review(self):
        family=entry_family({'TextureOverride_Component0':'','TextureOverride_LOD0.12345678_30_0':''})
        self.assertEqual(family,'mixed');self.assertEqual(classify(family,[])['status'],'manual_only')


if __name__=='__main__':unittest.main()
