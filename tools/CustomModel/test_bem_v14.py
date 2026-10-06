import contextlib
import copy
import importlib.util
import io
import json
import os
from pathlib import Path
import struct
import subprocess
import tempfile
import unittest

import bem_v1 as bem
import bem_v11
import bem_v13
import bem_v14
import bem_projects
from bem_tool import check_geometry, main

spec = importlib.util.spec_from_file_location('multi_resource_example',
    Path(__file__).parent / 'examples/multi-resource/create_project.py')
example = importlib.util.module_from_spec(spec); spec.loader.exec_module(example)


class Bem14Tests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory(); self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name); self.path = self.root / 'multi.bem'
        self.b = example.fixture()

    def native(self, path, expected=True):
        validator = os.environ.get('BEM_VALIDATOR')
        if validator:
            result = subprocess.run([validator, str(path)], capture_output=True)
            self.assertEqual(result.returncode == 0, expected, result.stdout + result.stderr)

    def test_roundtrip_shared_payloads_distinct_resource_donors(self):
        self.b.write(self.path)
        self.assertEqual(bem.package_minor(self.path), 4)
        m, payloads = bem.read_package(self.path)
        self.assertEqual(m, self.b.m)
        self.assertEqual(check_geometry(m, payloads, 4)['required_minor'], 4)
        self.assertEqual(m['target']['components'][0]['mesh_name'], m['target']['components'][1]['mesh_name'])
        self.assertEqual(m['meshes'][0]['streams'][0], m['meshes'][1]['streams'][0])
        self.assertNotEqual(m['meshes'][0]['bones'], m['meshes'][1]['bones'])
        self.native(self.path)

    def test_resource_filter_rebuilds_morph_payload_closure(self):
        self.b.write(self.path)
        seen = []
        m, plan, payloads = bem_v11.read_selected_payloads(self.path, on_payload=seen.append,
            resource='weapon', parameters={'length': 500})
        self.assertEqual([c['target'] for c in plan['components']], [2])
        self.assertEqual(plan['resources'], ['weapon'])
        deltas = [c['frames'][1]['payload'] for c in m['mesh_deformations']]
        self.assertIn(deltas[2], seen)
        self.assertTrue(all(p not in seen for p in deltas[:2]))
        self.assertNotIn(m['meshes'][0]['streams'][2]['payload'], seen)
        vertices = bem_v13.morph_streams(m, payloads, 2, {'length': 500})
        self.assertEqual(struct.unpack_from('<3f', vertices[0], 12), (1.375, 0, 0))
        _, hidden, decoded = bem_v11.read_selected_payloads(self.path, {'detail': 'hide'}, resource='weapon',
                                                           parameters={'length': 1000})
        self.assertEqual(hidden['components'][0]['operation'], 'hide')
        self.assertEqual(decoded, {})
        _, platform, _ = bem_v11.read_selected_payloads(self.path, platform='android-arm64')
        self.assertEqual(platform['resources'], ['body', 'weapon'])
        with self.assertRaisesRegex(ValueError, 'unavailable'):
            bem_v14.selection_plan(m, resource='ultimate', platform='android-arm64')

    def test_resource_contract_rejections(self):
        changes = [
            lambda m: m['required_capabilities'].remove('multi-resource-targets'),
            lambda m: m['required_capabilities'].remove('static-meshes'),
            lambda m: m['target'].update(character_id='chr_old'),
            lambda m: m['target']['resources'][0].update(lod=4),
            lambda m: m['target']['resources'][0].update(platforms=['other']),
            lambda m: m['target']['resources'][0].update(asset_path='assets/synthetic/../body.prefab'),
            lambda m: m['target']['resources'][0].update(asset_path='assets/synthetic/other.prefab'),
            lambda m: m['target']['resources'][0].update(name='Body'),
            lambda m: m['target']['resources'][1].update(id='body'),
            lambda m: m['target']['resources'][1].update(name='body', asset_path='assets/synthetic/body.prefab'),
            lambda m: m['target']['components'][2].update(resource='absent'),
            lambda m: m['target']['components'][2].update(renderer_path='../blade'),
            lambda m: m['target']['components'][0].update(bone_name_aliases=[dict(index=0, resource='world', name='alias')]),
            lambda m: m['target']['components'][2].update(bone_names=['root']),
            lambda m: m['target']['components'][1].update(resource='body'),
        ]
        for mutate in changes:
            m = copy.deepcopy(self.b.m); mutate(m)
            with self.subTest(mutate=mutate), self.assertRaises(ValueError):
                bem.validate_manifest(m, len(self.b.payloads))

    def test_duplicate_mesh_names_allowed_only_between_resources(self):
        m = self.b.m
        m['target']['components'][1]['resource'] = 'body'
        m['target']['resources'].pop(1)
        with self.assertRaisesRegex(ValueError, 'Duplicate mesh identity'):
            bem.validate_manifest(m, len(self.b.payloads))

    def test_resource_root_renderer_and_disjoint_platform_roots(self):
        m = self.b.m; m['target']['components'][2]['renderer_path'] = ''
        m['target']['resources'][0]['platforms'] = ['android-arm64']
        m['target']['resources'][1].update(name='body', asset_path='assets/synthetic/body.prefab')
        bem.validate_manifest(m, len(self.b.payloads))
        self.b.write(self.path); self.native(self.path)

    def test_native_schema_target_boundaries(self):
        self.b.write(self.path); raw = self.path.read_bytes()
        mutations = {
            'duplicate-platform-root': lambda m: m['target']['resources'][1].update(name='body', asset_path='assets/synthetic/body.prefab'),
            'unknown-resource': lambda m: m['target']['components'][0].update(resource='absent'),
            'renderer-kind-mismatch': lambda m: m['component_rules'][0]['candidates'][0].update(mesh=2),
            'empty-alias-field': lambda m: m['target']['components'][0].update(bone_name_aliases=[]),
            'extra-resource-field': lambda m: m['target']['resources'][0].update(extra=True),
            'empty-snapshot': lambda m: m['target'].update(snapshot=''),
            'snapshot-nul': lambda m: m['target'].update(snapshot='bad\0snapshot'),
            'mesh-name-nul': lambda m: m['target']['components'][0].update(mesh_name='bad\0name'),
            'bone-name-nul': lambda m: m['target']['components'][0]['bone_names'].append('bad\0name'),
            'material-name-nul': lambda m: m['target']['components'][0]['materials'].append('bad\0name'),
            'path-nul': lambda m: m['target']['components'][0].update(renderer_path='bad\0path'),
            'path-traversal': lambda m: m['target']['components'][0].update(renderer_path='Mesh/../body'),
            'component-id-bool': lambda m: m['target']['components'][0].update(id=False),
            'resource-lod-mismatch': lambda m: m['target']['components'][0].update(renderer_path='Mesh_all/lod1/shared_mesh'),
        }
        for name, mutate in mutations.items():
            m = copy.deepcopy(self.b.m); mutate(m)
            with self.subTest(boundary=name):
                with self.assertRaises(ValueError): bem.validate_manifest(m, len(self.b.payloads))
                path = self.root / (name + '.bem'); example._rewrite_manifest(raw, m, path)
                with self.assertRaises(ValueError): bem.read_package(path)
                self.native(path, False)

    def test_exact_mesh_all_lod_contract_without_guessing_custom_paths(self):
        for path in ('Mesh_all/lod1/body', 'Mesh_all/lod1'):
            m = copy.deepcopy(self.b.m); m['target']['components'][0]['renderer_path'] = path
            with self.subTest(path=path), self.assertRaisesRegex(ValueError, 'LOD differs'):
                bem.validate_manifest(m, len(self.b.payloads))
        for path in ('Mesh_all/lod0/body', 'Mesh_all/lod0', '', 'Custom/lod1/body',
                     'Shadow_Proxy/SP_Desktop/body', 'Mesh_all/lod10/body', 'Mesh_all/lod1_custom/body'):
            m = copy.deepcopy(self.b.m); m['target']['components'][0]['renderer_path'] = path
            with self.subTest(path=path):
                bem.validate_manifest(m, len(self.b.payloads))
                bem.write_package(self.path, m, self.b.payloads); self.native(self.path)

    def test_cross_resource_bone_material_and_replacement_rejected(self):
        changes = [
            lambda m: m['meshes'][1]['bones'][0].update(component=0),
            lambda m: m['meshes'][1]['draws'][0].update(material_component=0),
            lambda m: m['component_rules'][1]['candidates'][0].update(mesh=0),
            lambda m: m['component_rules'][0]['candidates'][0].update(mesh=2),
        ]
        for mutate in changes:
            m = copy.deepcopy(self.b.m); mutate(m)
            with self.subTest(mutate=mutate), self.assertRaises(ValueError):
                bem.validate_manifest(m, len(self.b.payloads))

    def test_static_one_two_and_three_streams_and_invalid_skin(self):
        for count in (1, 2, 3):
            m = copy.deepcopy(self.b.m); mesh = m['meshes'][2]
            if count == 1:
                mesh['streams'] = mesh['streams'][:1]; mesh['attributes'] = mesh['attributes'][:1]
            if count == 3:
                mesh['streams'].append(dict(stride=8, payload=mesh['streams'][1]['payload']))
                mesh['attributes'].append([5, 0, 2, 2, 0])
            bem.validate_manifest(m, len(self.b.payloads)); check_geometry(m, self.b.payloads)
        for mutate in (lambda mesh: mesh.update(bones=[dict(component=0, index=0, name='root')]),
                       lambda mesh: mesh['attributes'][1].__setitem__(0, 12),
                       lambda mesh: mesh['streams'].clear(),
                       lambda mesh: mesh['streams'].append(dict(stride=4, payload=2))):
            m = copy.deepcopy(self.b.m); mutate(m['meshes'][2])
            with self.assertRaises(ValueError): bem.validate_manifest(m, len(self.b.payloads))

    def test_static_bad_index_and_stream_lengths_rejected(self):
        m = self.b.m; p = list(self.b.payloads)
        index = m['meshes'][2]['draws'][0]['indices']; p[index] = struct.pack('<3H', 0, 1, 9)
        with self.assertRaisesRegex(ValueError, 'Index outside'): check_geometry(m, p)
        p = list(self.b.payloads); p[m['meshes'][2]['streams'][0]['payload']] = bytes(35)
        with self.assertRaisesRegex(ValueError, 'stream length'): check_geometry(m, p)

    def test_old_headers_cannot_silently_accept_14_fields(self):
        for minor in (0, 1, 2, 3):
            with self.subTest(minor=minor), self.assertRaisesRegex(ValueError, '1.4'):
                bem.validate_manifest(self.b.m, len(self.b.payloads), minor)
        valid = example.native_fixtures(self.root)
        self.native(valid)
        for path in self.root.glob('*invalid.bem'):
            with self.assertRaises(ValueError): bem.read_package(path)
            self.native(path, False)
        from test_bem_v11 import fixture
        for change in (lambda m: m['meshes'][0].update(renderer_kind='skinned'),
                       lambda m: m['target']['components'][0].update(renderer_path='Mesh/body'),
                       lambda m: m['target'].update(id='new-style-id')):
            old = fixture(); old.write(self.path); old_raw = self.path.read_bytes(); change(old.m)
            with self.assertRaisesRegex(ValueError, '1.4'): bem.validate_manifest(old.m, len(old.payloads), 1)
            example._rewrite_manifest(old_raw, old.m, self.path)
            self.native(self.path, False)

    def test_build_unpack_pack_validate_and_cli_resource_output(self):
        task = example.create(self.root / 'creator')
        with contextlib.redirect_stdout(io.StringIO()): self.assertEqual(main(['build', str(task)]), 0)
        package = task.parent / 'dist/synthetic.bem'
        self.assertEqual(bem.package_minor(package), 4)
        unpack = self.root / 'unpacked'; bem_projects.unpack(package, unpack)
        repacked = self.root / 'repacked.bem'; bem_projects.pack_project(unpack / 'project.json', repacked)
        self.assertEqual(bem.read_package(package), bem.read_package(repacked))
        result = io.StringIO()
        with contextlib.redirect_stdout(result):
            self.assertEqual(main(['validate', str(repacked), '--resource', 'weapon']), 0)
        output = json.loads(result.getvalue())
        self.assertEqual(output['format_version'], '1.4')
        self.assertEqual(output['selection_plan']['resources'], ['weapon'])

    def test_optionless_14_without_morphs_stays_14(self):
        m = self.b.m; m['option_groups'] = []; m['parameters'] = []; m['mesh_deformations'] = []
        m['component_rules'][2]['candidates'] = [dict(operation='replace', mesh=2)]
        m['required_capabilities'] = [c for c in m['required_capabilities'] if c not in bem_v13.CAPABILITIES]
        self.b.write(self.path)
        self.assertEqual(bem.package_minor(self.path), 4)
        self.assertEqual(check_geometry(*bem.read_package(self.path), 4)['required_minor'], 4)

    def test_cli_bundle_mixed_legacy_character_and_static_weapon_roundtrip(self):
        from test_bem_v1 import fixture
        legacy = fixture(); legacy_path = self.root / 'legacy.bem'; legacy.write(legacy_path)
        self.b.write(self.path)
        weapon = copy.deepcopy(self.b); m = weapon.m
        m['package_id'] = 'synthetic.static-weapon'
        m['target'].update(kind='weapon', id='wpn_synthetic')
        m['target']['resources'] = [m['target']['resources'][2]]
        component = m['target']['components'][2]; component['id'] = 0
        m['target']['components'] = [component]
        mesh = m['meshes'][2]; mesh['draws'][0]['material_component'] = 0
        m['meshes'] = [mesh]
        deformation = m['mesh_deformations'][2]; deformation['mesh'] = 0
        m['mesh_deformations'] = [deformation]
        m['component_rules'] = [dict(target=0, candidates=[dict(operation='replace', mesh=0)])]
        weapon_path = self.root / 'weapon.bem'; weapon.write(weapon_path)
        sources = [legacy_path, self.path, weapon_path]
        archive = self.root / 'mixed.zip'
        output = io.StringIO()
        with contextlib.redirect_stdout(output):
            self.assertEqual(main(['bundle', *map(str, sources), '-o', str(archive)]), 0)
        summaries = {p['package_id']: p for p in json.loads(output.getvalue())['packages']}
        for manifest in (legacy.m, self.b.m):
            target_id = manifest['target'].get('id', manifest['target'].get('character_id'))
            summary = summaries[manifest['package_id']]
            self.assertEqual((summary['target_kind'], summary['target_id'], summary['character_id']),
                             ('character', target_id, target_id))
        summary = summaries[m['package_id']]
        self.assertEqual((summary['target_kind'], summary['target_id']), ('weapon', 'wpn_synthetic'))
        self.assertNotIn('character_id', summary)
        expected = {bem.read_package(p)[0]['package_id']: bem.read_package(p) for p in sources}
        for command, extra in [('inspect', []), ('unpack', ['-o', str(self.root / 'staged')])]:
            output = io.StringIO()
            with contextlib.redirect_stdout(output):
                self.assertEqual(main([command, str(archive), *extra]), 0)
            result = json.loads(output.getvalue())
            self.assertFalse(result['issues'])
            self.assertEqual({p['package']['package_id'] for p in result['packages']}, set(expected))
            for package in result['packages']:
                package_id = package['package']['package_id']
                self.assertEqual(package['package'], expected[package_id][0])
                if command == 'unpack':
                    staged = self.root / 'staged' / package['file']
                    self.assertEqual(bem.read_package(staged), expected[package_id])
                    self.assertEqual(bem.package_minor(staged), 0 if package_id == legacy.m['package_id'] else 4)


if __name__ == '__main__': unittest.main()
