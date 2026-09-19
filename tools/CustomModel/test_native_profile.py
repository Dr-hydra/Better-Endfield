import copy
import unittest

from parse_native_models import parse
from prepare_native_profile import remap_bones, paired_resource, prepare
from test_native_models import fixture
from efmi_source import sections


class NativeProfileTests(unittest.TestCase):
    def setUp(self):
        self.database = parse(fixture())
        self.renderer = self.database['renderers'][0]

    def test_remap_uses_identity_and_bindpose(self):
        target = self.renderer
        source = copy.deepcopy(target)
        self.assertEqual(remap_bones(source, target, self.database['meshes']), [0])
        source['bones'][0]['id'] = 'different:1'
        with self.assertRaisesRegex(ValueError, 'absent'):
            remap_bones(source, target, self.database['meshes'])
        source = copy.deepcopy(target)
        source['mesh_id'] = 'other:1'
        self.database['meshes']['other:1'] = copy.deepcopy(self.database['meshes'][target['mesh_id']])
        self.database['meshes']['other:1']['bindposes'][0][0] = 2
        with self.assertRaisesRegex(ValueError, 'bindpose differs'):
            remap_bones(source, target, self.database['meshes'])

    def test_duplicate_bone_identity_rejected(self):
        source = copy.deepcopy(self.renderer)
        self.renderer['bones'].append(copy.deepcopy(self.renderer['bones'][0]))
        with self.assertRaisesRegex(ValueError, 'ambiguous'):
            remap_bones(source, self.renderer, self.database['meshes'])

    def test_ui_join_preserves_material_slots_and_bone_paths(self):
        other = copy.deepcopy(self.renderer)
        other['bones'][0]['path'] = 'ui/bone'
        self.assertEqual(paired_resource(self.renderer, other, self.database['meshes']), [])
        other['materials'][0]['id'] = 'other:1'
        self.assertIn('world/UI material slots differ',
                      paired_resource(self.renderer, other, self.database['meshes']))

    def test_prepare_never_approves_offline_candidates(self):
        native = self.renderer
        native.update(resource_root='world', path='world/lod0/mesh_lod0', mesh_name='mesh_lod0')
        other = copy.deepcopy(native)
        other.update(id='ui:1', resource_root='ui', path='ui/lod0/mesh_lod0')
        self.database['renderers'].append(other)
        self.database['resources'] = {'world': [native['id']], 'ui': ['ui:1']}
        sec = sections('''[TextureOverride_LOD0.12345678_3_0]
hash = 12345678
match_index_count = 3
match_first_index = 0
$\\EFMIv1\\component_id = 0
CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandListDraw
[CommandListInitializeMergedSkeleton]
$component_id = 0
$Pool_MergedSkeleton_Component_VertexGroupOffsets[$component_id] = 0
$Pool_MergedSkeleton_Component_VertexGroupCounts[$component_id] = 1
[CommandListDraw]
vb0 = ResourceP
vb1 = ResourceT
vb2 = ResourceB
ib = ResourceI
drawindexed = 3,0,0
[ResourceP]
stride = 16
[ResourceT]
stride = 8
[ResourceB]
stride = 16
''')
        report = prepare(self.database, sec, 'world', 'ui')
        self.assertFalse(report['conversion_ready'])
        self.assertFalse(report['profile_draft']['verified'])
        self.assertEqual(report['profile_draft']['skeleton_groups']['0']['indices'], [0])
        self.assertEqual(report['profile_draft']['components']['0']['attributes'], [])
        self.database['renderers'].append(copy.deepcopy(native))
        with self.assertRaisesRegex(ValueError, 'unique native'):
            prepare(self.database, sec, 'world', 'ui')


if __name__ == '__main__':
    unittest.main()
