import contextlib
import copy
import io
import json
import os
from pathlib import Path
import shutil
import struct
import subprocess
import tempfile
import unittest
from unittest.mock import patch

import bem_v1 as bem
import bem_projects
import bem_tasks
from bem_export import prepare_export
from bem_tool import convert, main
from test_bem_v1 import fixture
from test_hash_lod_lowering import fixture as source_fixture


class ExportTaskTests(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.TemporaryDirectory()
        self.addCleanup(self.tmp.cleanup)
        self.root = Path(self.tmp.name)

    def native(self, path):
        validator = os.environ.get('BEM_VALIDATOR')
        if validator:
            result = subprocess.run([validator, str(path)], capture_output=True)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)

    def project(self, builder=None):
        builder = builder or fixture()
        root = self.root / 'editable'
        root.mkdir()
        names = []
        for index, raw in enumerate(builder.payloads):
            name = str(index) + '.bin'
            (root / name).write_bytes(raw)
            names.append(name)
        path = root / 'project.json'
        path.write_text(json.dumps(dict(manifest=builder.m, payload_files=names)), encoding='utf-8')
        return path

    def test_portable_reopen_repeated_build_and_stable_id(self):
        source = self.project()
        task = self.root / 'character.bemproj.json'
        bem_tasks.new_project(source, task, mode='pack')
        saved = bem.load_json(task)
        self.assertEqual(saved['source'], 'editable/project.json')
        self.assertEqual(saved['package']['id'], 'test.package')
        moved = self.root / 'moved'
        moved.mkdir()
        shutil.move(str(source.parent), moved / 'editable')
        shutil.move(str(task), moved / task.name)
        task = moved / task.name
        with contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(main(['build', str(task)]), 0)
            self.assertEqual(main(['build', str(task)]), 0)
        self.assertTrue((moved / 'reports/build.json').is_file())
        package = moved / 'dist/project.bem'
        self.assertEqual(bem.package_minor(package), 0)
        self.assertEqual(bem.read_package(package)[0]['package_id'], 'test.package')
        self.assertEqual(bem.load_json(task), saved)
        self.native(package)

    def test_workspace_init_copies_pack_inputs_and_builds(self):
        source = self.project()
        workspace = self.root / 'workspace'
        with contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(main(['workspace', 'init', str(workspace), '--source', str(source), '--mode', 'pack']), 0)
            self.assertEqual(main(['build', str(workspace / 'export.bemproj.json')]), 0)
        for directory in ('source', 'recipe', 'project', 'textures', 'dist', 'reports'):
            self.assertTrue((workspace / directory).is_dir())
        self.assertTrue((workspace / 'project' / 'project.json').is_file())
        self.assertTrue((workspace / 'dist' / 'project.bem').is_file())

    def test_workspace_init_refuses_non_empty_target(self):
        source = self.project()
        workspace = self.root / 'workspace'
        workspace.mkdir(); (workspace / 'keep.txt').write_text('keep', encoding='utf-8')
        with self.assertRaisesRegex(ValueError, 'new or empty'):
            bem_tasks.init_workspace(workspace, source, mode='pack')

    def test_parameters_override_without_modifying_source(self):
        source = self.project()
        original = source.read_bytes()
        task = self.root / 'task.json'
        bem_tasks.new_project(source, task, mode='pack', package=dict(id='creator.stable', name='修改名称', version='2'))
        result = bem_tasks.build_task(bem_tasks.load_task(task))
        self.assertEqual(result['package']['package_id'], 'creator.stable')
        self.assertEqual(result['package']['name'], '修改名称')
        self.assertEqual(result['package']['version'], '2')
        self.assertEqual(source.read_bytes(), original)

    def test_project_and_report_cannot_overwrite_inputs(self):
        source = self.project()
        task = self.root / 'task.json'
        bem_tasks.new_project(source, task, mode='pack')
        payload = source.parent / '0.bin'
        before = payload.read_bytes()
        with contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(main(['build', str(task), '--report', str(payload)]), 2)
        self.assertEqual(payload.read_bytes(), before)
        data = bem.load_json(task)
        data['report'] = 'task.json'
        task.write_text(json.dumps(data), encoding='utf-8')
        with self.assertRaisesRegex(ValueError, 'overwrite'): bem_tasks.load_task(task)

    def test_alias_fixed_project_is_upgraded_with_matching_semantics(self):
        builder = fixture()
        builder.m['target']['components'][0]['bone_name_aliases'] = [dict(index=0, resource='world', name='bone0_typo')]
        source = self.project(builder)
        output = self.root / 'alias.bem'
        bem_projects.pack_project(source, output)
        self.assertEqual(bem.package_minor(output), 2)
        manifest, payloads = bem.read_package(output)
        self.assertIn('resource-bone-aliases', manifest['required_capabilities'])
        self.assertNotIn('fixed-appearances', manifest['required_capabilities'])
        import bem_v11
        first = bem_v11.selection_plan(manifest, {'appearance': 'default'})
        second = bem_v11.selection_plan(manifest, {'appearance': 'hidden'})
        self.assertEqual(first['components'][0]['operation'], 'replace')
        self.assertEqual(second['components'][0]['operation'], 'hide')
        draws = manifest['meshes'][0]['draws']
        self.assertEqual(payloads[draws[0]['indices']], struct.pack('<3H', 0, 1, 2))
        self.native(output)

    def test_alias_reader_does_not_accept_unversioned_fixed_manifest(self):
        builder = fixture()
        builder.m['target']['components'][0]['bone_name_aliases'] = [dict(index=0, resource='world', name='bone0_typo')]
        with self.assertRaisesRegex(ValueError, 'BEM 1.2'): builder.write(self.root / 'broken.bem')

    def conversion(self, float_skin=False):
        source = self.root / 'source'
        profile = source_fixture(source)
        profile['components']['0']['bone_name_aliases'] = [dict(index=0, resource='world', name='bone0_typo')]
        if float_skin:
            for native in profile['components'].values():
                native['strides'][2] = 32
                native['attributes'][-2:] = [[12, 0, 4, 2], [13, 10, 4, 2]]
            profile['entries']['12345678']['skin'] = 'float32x4_uint32x4'
            profile['entries']['12345678']['input_strides'][2] = 32
            (source / 'Meshes/b.buf').write_bytes(struct.pack('<4f4I', .5, .5, 0, 0, 0, 256, 0, 0) * 3)
            ini = source / 'mod.ini'
            ini.write_text(ini.read_text().replace('[ResourceB]\nstride = 16', '[ResourceB]\nstride = 32'))
        (self.root / 'native.json').write_text(json.dumps(profile), encoding='utf-8')
        recipe = dict(schema=1, package=dict(id='creator.fixed-id', name='测试', author='Test', version='1'),
            target=dict(character_id='chr_test', world_resource='world', ui_resource='ui', profile_id='test', revision='1'),
            appearances=[dict(id='default', name='默认', source='source', format='hash-lod', profile='native.json', ini='mod.ini')])
        path = self.root / 'conversion.recipe.json'
        path.write_text(json.dumps(recipe), encoding='utf-8')
        return source, path

    def test_recipe_alias_conversion_matches_native_validator(self):
        source, recipe = self.conversion()
        task = self.root / 'task.bemproj.json'
        bem_tasks.new_project(source, task, recipe=recipe)
        result = bem_tasks.build_task(bem_tasks.load_task(task))
        self.assertEqual(result['format_version'], '1.2')
        self.assertEqual(result['package']['package_id'], 'creator.fixed-id')
        self.native(result['output'])

    def test_automatic_alias_float_skin_task_keeps_its_generated_id(self):
        from test_component_auto import AutoTests
        source = AutoTests()
        source.setUp()
        self.addCleanup(source.doCleanups)
        native = source.catalog['components']['0']
        native['bone_name_aliases'] = [dict(index=0, resource='world', name='root_typo')]
        native['strides'][2] = 32
        native['attributes'][-2:] = [[12, 0, 4, 2], [13, 10, 4, 2]]
        source.ini = source.ini.replace('[ResourceSkin]\nstride = 12', '[ResourceSkin]\nstride = 32')
        (source.root / 'anything.ini').write_text(source.ini, encoding='utf-8')
        (source.root / 'arbitrary-skin.buf').write_bytes(struct.pack('<4f4I', 1, 0, 0, 0, 0, 0, 0, 0) * 3)
        task = self.root / 'automatic.json'
        bem_tasks.new_project(source.root, task)
        identity = bem.load_json(task)['package']['id']
        with patch('component_auto.catalogs', return_value=[source.catalog]):
            for _ in range(2):
                result = bem_tasks.build_task(bem_tasks.load_task(task))
                self.assertEqual(result['format_version'], '1.2')
                self.assertEqual(result['package']['package_id'], identity)
                self.native(result['output'])

    def test_float_skin_conversion_is_native_v12(self):
        source, recipe = self.conversion(float_skin=True)
        output = self.root / 'float.bem'
        result = convert(source, recipe, output)
        self.assertEqual(result['format_version'], '1.2')
        manifest, payloads = bem.read_package(output)
        skin = manifest['meshes'][0]['streams'][2]
        self.assertEqual(skin['stride'], 32)
        self.assertEqual(struct.unpack_from('<4f4I', payloads[skin['payload']]), (.5, .5, 0, 0, 0, 1, 0, 0))
        self.native(output)

    def test_texture_slots_receive_capability_on_project_export(self):
        from test_bem_v12 import Bem12Tests
        from test_bem_v11 import fixture as composable_fixture
        helper = Bem12Tests()
        helper.builder = composable_fixture()
        manifest = helper.slot_manifest()
        manifest['required_capabilities'].remove('texture-slots')
        manifest, payloads = prepare_export(manifest, helper.builder.payloads)
        output = self.root / 'slots.bem'
        bem.write_package(output, manifest, payloads)
        self.assertEqual(bem.package_minor(output), 2)
        self.assertIn('texture-slots', manifest['required_capabilities'])
        self.native(output)


if __name__ == '__main__': unittest.main()
