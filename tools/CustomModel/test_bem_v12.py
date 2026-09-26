import copy
import os
from pathlib import Path
import subprocess
import tempfile
import unittest

import bem_v1 as bem
import bem_v11
from bem_tool import check_geometry
from test_bem_v11 import fixture


def colour_group(count):
    return dict(id='colour', name='颜色', default='c0',
                choices=[dict(id=f'c{i}', name=f'颜色 {i}') for i in range(count)])


class Bem12Tests(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.TemporaryDirectory()
        self.path = Path(self.tmp.name) / 'test.bem'
        self.builder = fixture()

    def tearDown(self):
        self.tmp.cleanup()

    def native(self, expected, path=None):
        validator = os.environ.get('BEM_VALIDATOR')
        if validator:
            result = subprocess.run([validator, str(path or self.path)], capture_output=True)
            self.assertEqual(result.returncode == 0, expected, result.stdout + result.stderr)

    def write(self, m):
        bem.write_package(self.path, m, self.builder.payloads)
        return bem.package_minor(self.path)

    def slot_manifest(self, colours=3):
        m = copy.deepcopy(self.builder.m)
        m['option_groups'].append(colour_group(colours))
        for i in range(2, colours):
            m['textures'].append(dict(width=4, height=4, mips=1, format=10, srgb=False, original_name='original',
                                      payload=self.builder.payload(bytes([i + 1]) * 8)))
        m['required_capabilities'].append('texture-slots')
        # c0 keeps the native texture, the other colours pick their own texture.
        m['texture_slots'] = [dict(id='dress_d', candidates=[
            dict(when={'eq': ['colour', 'c0']}, texture=None),
            *(dict(when={'eq': ['colour', f'c{i}']}, texture=i) for i in range(1, colours))])]
        m['meshes'][0]['draws'][0]['textures'] = [{'slot': 'dress_d'}]
        return m

    def test_within_v11_limits_stays_v11(self):
        self.assertEqual(self.write(self.builder.m), 1)
        self.native(True)

    def test_more_than_sixteen_choices_needs_v12(self):
        m = copy.deepcopy(self.builder.m)
        m['option_groups'].append(colour_group(20))
        with self.assertRaisesRegex(ValueError, 'Invalid group choices'):
            bem.validate_manifest(m, len(self.builder.payloads), 1)
        self.assertEqual(self.write(m), 2)
        manifest, payloads = bem.read_package(self.path)
        self.assertEqual(check_geometry(manifest, payloads, 2)['required_minor'], 2)
        self.native(True)
        # A 1.1 header must not carry 1.2 content.
        raw = bytearray(self.path.read_bytes())
        raw[10:12] = (1).to_bytes(2, 'little')
        self.path.write_bytes(bytes(raw))
        with self.assertRaisesRegex(ValueError, 'Invalid group choices'):
            bem.read_package(self.path)
        self.native(False)

    def test_sixty_five_choices_rejected(self):
        m = copy.deepcopy(self.builder.m)
        m['option_groups'].append(colour_group(65))
        with self.assertRaisesRegex(ValueError, 'Invalid group choices'):
            bem.validate_manifest(m, len(self.builder.payloads))

    def test_texture_slot_selects_texture_without_duplicating_draws(self):
        m = self.slot_manifest()
        self.assertEqual(self.write(m), 2)
        self.native(True)
        for colour, expected in (('c0', []), ('c1', [1]), ('c2', [2])):
            _, plan, selected = bem_v11.read_selected_payloads(self.path, {'colour': colour})
            draw = plan['components'][0]['draws'][0]
            self.assertEqual(draw['textures'], expected)
            self.assertEqual(plan['texture_slots'], {'dress_d': expected[0] if expected else None})
            for texture in expected:
                self.assertIn(m['textures'][texture]['payload'], selected)
        summary = bem_v11.analyze_selection_space(m, self.builder.payloads)
        plans = list(bem_v11.reachable_plans(m))
        self.assertEqual(summary['max_selected_textures'], max(len(p['textures']) for p in plans))

    def test_texture_slot_rules(self):
        m = self.slot_manifest()
        broken = copy.deepcopy(m)
        broken['required_capabilities'].remove('texture-slots')
        with self.assertRaisesRegex(ValueError, 'Texture slot capability'):
            bem.validate_manifest(broken, len(self.builder.payloads))
        broken = copy.deepcopy(m)
        broken['texture_slots'][0]['candidates'][0]['when'] = True
        with self.assertRaisesRegex(ValueError, 'exactly one selected candidate'):
            bem.validate_manifest(broken, len(self.builder.payloads))
        broken = copy.deepcopy(m)
        del broken['texture_slots'][0]['candidates'][2]
        with self.assertRaisesRegex(ValueError, 'exactly one selected candidate'):
            bem.validate_manifest(broken, len(self.builder.payloads))
        broken = copy.deepcopy(m)
        broken['textures'][2]['original_name'] = 'other'
        with self.assertRaisesRegex(ValueError, 'one original texture'):
            bem.validate_manifest(broken, len(self.builder.payloads))
        broken = copy.deepcopy(m)
        broken['meshes'][0]['draws'][0]['textures'] = [{'slot': 'dress_d'}, 0]
        with self.assertRaisesRegex(ValueError, 'replace one original twice'):
            bem.validate_manifest(broken, len(self.builder.payloads))
        broken = copy.deepcopy(m)
        broken['meshes'][0]['draws'][0]['textures'] = [{'slot': 'missing'}]
        with self.assertRaisesRegex(ValueError, 'texture slot reference'):
            bem.validate_manifest(broken, len(self.builder.payloads))
        with self.assertRaisesRegex(ValueError, 'capabilities'):
            bem.validate_manifest(m, len(self.builder.payloads), 1)

    def test_keep_override_can_use_texture_slot(self):
        m = self.slot_manifest()
        m['required_capabilities'].append('keep-material-textures')
        keep = next(c for c in m['component_rules'][1]['candidates'] if c['operation'] == 'keep')
        keep['material_overrides'] = [dict(material_slot=0, material_name='material1', textures=[{'slot': 'dress_d'}])]
        self.write(m)
        self.native(True)
        _, plan, _ = bem_v11.read_selected_payloads(self.path, {'colour': 'c2'})
        self.assertEqual(plan['components'][1]['material_overrides'][0]['textures'], [2])

    def test_bone_name_alias(self):
        m = copy.deepcopy(self.builder.m)
        m['target']['components'][0]['bone_name_aliases'] = [dict(index=0, resource='world', name='bone0_typo')]
        with self.assertRaisesRegex(ValueError, 'alias capability'):
            bem.validate_manifest(m, len(self.builder.payloads))
        m['required_capabilities'].append('resource-bone-aliases')
        self.assertEqual(self.write(m), 2)
        self.native(True)
        for alias in (dict(index=1, resource='world', name='x'), dict(index=0, resource='phone', name='x'),
                      dict(index=0, resource='world', name='bone0')):
            broken = copy.deepcopy(m)
            broken['target']['components'][0]['bone_name_aliases'] = [alias]
            with self.assertRaises(ValueError):
                bem.validate_manifest(broken, len(self.builder.payloads))

    def test_uncompressed_float_skin_layout_needs_v12(self):
        import struct
        m = copy.deepcopy(self.builder.m)
        mesh = m['meshes'][0]
        mesh['attributes'] = [[0, 0, 3, 0, 0], [1, 0, 3, 0, 12], [2, 0, 4, 0, 24], [4, 0, 2, 1, 0],
                              [6, 0, 2, 1, 8], [12, 0, 4, 2, 0], [13, 10, 4, 2, 16]]
        strides = (40, 16, 32)
        skin = b''.join(struct.pack('<4f4I', 0.75, 0.25, 0, 0, i % 2, 1 - i % 2, 0, 0) for i in range(3))
        for stream, stride, raw in zip(mesh['streams'], strides, (bytes(120), bytes(48), skin)):
            stream.update(stride=stride, payload=self.builder.payload(raw))
        with self.assertRaisesRegex(ValueError, 'Unsupported native declaration'):
            bem.validate_manifest(m, len(self.builder.payloads), 1)
        self.assertEqual(check_geometry(m, self.builder.payloads)['required_minor'], 2)
        self.assertEqual(self.write(m), 2)
        self.native(True)
        bad = b''.join(struct.pack('<4f4I', 0.5, 0.25, 0, 0, 0, 1, 0, 0) for _ in range(3))
        m['meshes'][0]['streams'][2]['payload'] = self.builder.payload(bad)
        with self.assertRaisesRegex(ValueError, 'sum to 1'):
            check_geometry(m, self.builder.payloads)
        bad = b''.join(struct.pack('<4f4I', 1, 0, 0, 0, 7, 0, 0, 0) for _ in range(3))
        m['meshes'][0]['streams'][2]['payload'] = self.builder.payload(bad)
        with self.assertRaisesRegex(ValueError, 'outside palette'):
            check_geometry(m, self.builder.payloads)

    def test_texture_over_64_mib_needs_v12(self):
        m = copy.deepcopy(self.builder.m)
        texture = m['textures'][0]
        texture.update(width=8192, height=8192, mips=2, format=25)
        size = bem_v11.texture_bytes(texture)
        self.assertGreater(size, 64 * 1024**2)
        texture['payload'] = self.builder.payload(bytes(size))
        with self.assertRaisesRegex(ValueError, 'Texture exceeds 64 MiB'):
            bem_v11.check_geometry(m, self.builder.payloads, 1)
        self.assertEqual(check_geometry(m, self.builder.payloads)['required_minor'], 2)
        self.assertEqual(self.write(m), 2)
        self.native(True)


if __name__ == '__main__':
    unittest.main()
