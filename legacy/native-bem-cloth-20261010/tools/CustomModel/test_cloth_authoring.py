import copy
import unittest
from cloth_authoring import validate


def example():
    p = {'schema': 1, 'kind': 'bem-cloth-authoring-prototype',
         'target': {'character_id': 'example', 'resource': 'world', 'mesh': 'cloth',
                    'vertex_count': 6, 'geometry_sha256': '0'*64}, 'layers': [],
         'contacts': [{'layers': ['inner', 'outer'], 'kind': 'surface-surface'}]}
    for n, name in enumerate(('inner', 'outer')):
        p['layers'].append({'id': name, 'positions': [[0, n*.01, 0], [1, n*.01, 0], [0, n*.01, 1]],
                            'triangles': [[0, 1, 2]], 'fixed_vertices': [2],
                            'anchor_bone': 'Bip001_Pelvis',
                            'display_bindings': [{'vertex': n*3+i, 'proxy_vertex': i} for i in range(3)]})
    return p


class ClothAuthoringTests(unittest.TestCase):
    def test_valid_is_not_runtime_proof(self):
        report = validate(example())
        self.assertTrue(report['success'])
        self.assertFalse(report['native_solver_verified'])
        self.assertFalse(report['wire_format_supported'])

    def test_duplicate_writers_refused(self):
        p = example();p['layers'][1]['display_bindings'][0]['vertex'] = 0
        with self.assertRaisesRegex(ValueError, 'multiple cloth writers'):validate(p)

    def test_unanchored_panel_refused(self):
        p = example();l = p['layers'][0]
        l['positions'] += [[2, 0, 0], [3, 0, 0], [2, 0, 1]];l['triangles'].append([3, 4, 5])
        with self.assertRaisesRegex(ValueError, 'Unanchored'):validate(p)

    def test_invalid_geometry(self):
        for change in ('nan', 'index', 'zero_area', 'duplicate'):
            with self.subTest(change=change):
                p = example();l = p['layers'][0]
                if change == 'nan':l['positions'][0][0] = float('nan')
                elif change == 'index':l['triangles'][0][1] = -1
                elif change == 'zero_area':l['positions'][2] = [2, 0, 0]
                else:l['triangles'].append([2, 1, 0])
                with self.assertRaises(ValueError):validate(p)

    def test_wrong_collision_partner(self):
        p = example();p['contacts'][0]['layers'][1] = 'missing'
        with self.assertRaisesRegex(ValueError, 'Invalid layer'):validate(p)

    def test_decorations_follow_surface_and_bad_barycentric_refused(self):
        p = example();p['layers'][0]['display_bindings'][0] = {
            'vertex': 0, 'triangle': 0, 'barycentric': [.2, .3, .5], 'rest_offset_tbn': [0, 0, .005]}
        self.assertEqual(validate(p)['layers'][0]['surface_bindings'], 1)
        p['layers'][0]['display_bindings'][0]['barycentric'] = [.5, .5, .5]
        with self.assertRaisesRegex(ValueError, 'surface binding weights'):validate(p)

    def test_invalid_binding_and_duplicate_pair(self):
        p = example();p['layers'][0]['display_bindings'][0]['proxy_vertex'] = 9
        with self.assertRaisesRegex(ValueError, 'proxy index'):validate(p)
        p = example();p['contacts'].append(copy.deepcopy(p['contacts'][0]))
        with self.assertRaisesRegex(ValueError, 'Duplicate layer collision'):validate(p)


if __name__ == '__main__':unittest.main()
