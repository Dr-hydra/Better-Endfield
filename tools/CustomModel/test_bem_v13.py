import contextlib
import copy
import importlib.util
import io
import json
import os
import struct
import subprocess
import tempfile
import unittest
from pathlib import Path

import bem_v1 as bem
import bem_v11
import bem_v13
import bem_tasks
from bem_tool import check_geometry, main
from efmi_shapes import decode_official, decode_binding, require_shape_binding
from test_bem_v11 import fixture as old_fixture


def fixture():
    b = old_fixture(); m = b.m
    m['parameters'] = [dict(id='body', name='体型', min=0, max=1000, neutral=0, default=0, step=1)]
    m['required_capabilities'] += list(bem_v13.CAPABILITIES)
    raw = bem_v13.encode_deltas([[0, 2, 4, -2], [2, -1, 0, 3]], 3)
    m['mesh_deformations'] = [dict(mesh=0, parameter='body', frames=[dict(value=0, neutral=True),
             dict(value=1000, payload=b.payload(raw), count=2, encoding=bem_v13.ENCODING)])]
    return b


def official_buffers(records, shape_key=0):
    offsets = [0] + [len(records) if i >= shape_key else 0 for i in range(127)]
    config = struct.pack('<8f', *([1.0] * 8)) + struct.pack('<132I', 0, 0, 0, 0, *offsets)
    return config, b''.join(struct.pack('<I', r[0]) for r in records), b''.join(struct.pack('<3e', *r[1:]) for r in records)


class Bem13Tests(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.TemporaryDirectory(); self.addCleanup(self.tmp.cleanup)
        self.root = Path(self.tmp.name); self.b = fixture(); self.path = self.root / 'shape.bem'

    def native(self, path=None):
        validator = os.environ.get('BEM_VALIDATOR')
        if validator:
            result = subprocess.run([validator, str(path or self.path)], capture_output=True)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)

    def test_roundtrip_lazy_interpolation_neutral_and_tbn(self):
        self.b.write(self.path); self.assertEqual(bem.package_minor(self.path), 3)
        m, p = bem.read_package(self.path); check_geometry(m, p, 3); self.native()
        result = bem_v13.morph_streams(m, p, 0, {'body': 500})
        base = p[m['meshes'][0]['streams'][0]['payload']]
        self.assertEqual(struct.unpack_from('<3f', result[0]), (1, 2, -1))
        self.assertEqual(struct.unpack_from('<3f', result[0], 32), (-.5, 0, 1.5))
        self.assertEqual(result[0][12:16], base[12:16]); self.assertEqual(result[1:], [p[s['payload']] for s in m['meshes'][0]['streams'][1:]])
        self.assertEqual(bem_v13.morph_streams(m, p, 0, {'body': 0})[0], base)
        delta_id = m['mesh_deformations'][0]['frames'][1]['payload']
        accessed = []; bem_v11.read_selected_payloads(self.path, on_payload=accessed.append)
        self.assertNotIn(delta_id, accessed)
        _, plan, decoded = bem_v11.read_selected_payloads(self.path, parameters={'body': 501})
        self.assertIn(delta_id, decoded); self.assertEqual(plan['parameters']['body'], 501)

    def test_multiple_parameters_sum_absolute_deltas(self):
        m = self.b.m; second = copy.deepcopy(m['parameters'][0]); second['id'] = 'height'; m['parameters'].append(second)
        channel = copy.deepcopy(m['mesh_deformations'][0]); channel['parameter'] = 'height'; m['mesh_deformations'].append(channel)
        result = bem_v13.morph_streams(m, self.b.payloads, 0, {'body': 500, 'height': 250})
        self.assertEqual(struct.unpack_from('<3f', result[0]), (1.5, 3, -1.5))
        bem.validate_manifest(m, len(self.b.payloads)); self.b.write(self.path); self.native()

    def test_unavailable_parameter_uses_neutral_but_preserves_saved_value(self):
        self.b.m['parameters'][0]['available_when'] = {'eq': ['outfit', 'b']}
        saved = {'body': 1000}; before = copy.deepcopy(saved)
        plan = bem_v11.selection_plan(self.b.m, {'outfit': 'a'}, parameters=saved)
        self.assertEqual(plan['parameters']['body'], 0); self.assertEqual(saved, before)
        self.assertEqual(bem_v11.selection_plan(self.b.m, {'outfit': 'b'}, parameters=saved)['parameters']['body'], 1000)

    def test_interior_neutral_and_multiframe_piecewise_interpolation(self):
        p = self.b.m['parameters'][0]; p.update(neutral=500, default=500, step=10)
        frame = self.b.m['mesh_deformations'][0]['frames'][1]
        self.b.m['mesh_deformations'][0]['frames'] = [dict(frame, value=0), dict(value=500, neutral=True), dict(frame, value=1000)]
        bem.validate_manifest(self.b.m, len(self.b.payloads))
        result = bem_v13.morph_streams(self.b.m, self.b.payloads, 0, {'body': 250})
        self.assertEqual(struct.unpack_from('<3f', result[0]), (1, 2, -1))
        with self.assertRaises(ValueError): bem_v13.resolve_parameters(self.b.m, {'body': 251})

    def test_structural_rejections(self):
        changes = [lambda m: m['parameters'][0].update(step=3), lambda m: m['parameters'][0].update(max=1001),
                   lambda m: m['parameters'][0].update(default=1001), lambda m: m['parameters'].append(copy.deepcopy(m['parameters'][0])),
                   lambda m: m['mesh_deformations'].append(copy.deepcopy(m['mesh_deformations'][0])),
                   lambda m: m['mesh_deformations'][0]['frames'].reverse(),
                   lambda m: m['mesh_deformations'][0]['frames'][0].update(payload=0),
                   lambda m: m['mesh_deformations'][0]['frames'][1].update(encoding='program'),
                   lambda m: m['mesh_deformations'][0]['frames'][1].update(count=4),
                   lambda m: m['required_capabilities'].remove('mesh-position-deltas')]
        for change in changes:
            with self.subTest(change=change):
                m = copy.deepcopy(self.b.m); change(m)
                with self.assertRaises(ValueError): bem.validate_manifest(m, len(self.b.payloads))
        with self.assertRaisesRegex(ValueError, '1.3'): bem.validate_manifest(self.b.m, len(self.b.payloads), 2)

    def test_delta_validation_and_empty_endpoint(self):
        raw = bem_v13.encode_deltas([], 3); self.assertEqual(bem_v13.decode_deltas(raw, 0, 3), [])
        for records in ([[0, 1, 2, float('nan')]], [[3, 0, 0, 0]], [[0, 1, 2, 3], [0, 2, 3, 4]], [[0, 1e99, 0, 0]]):
            with self.subTest(records=records), self.assertRaises(ValueError): bem_v13.encode_deltas(records, 3)
        raw = bem_v13.encode_deltas([[0, 1, 2, 3]], 3)
        for data, count in ((raw[:-1], 1), (raw + b'x', 1), (raw, 2)):
            with self.assertRaises(ValueError): bem_v13.decode_deltas(data, count, 3)

    def test_dedup_remaps_delta_payload_references(self):
        self.b.payloads.append(self.b.payloads[-1]); self.b.m['mesh_deformations'][0]['frames'][1]['payload'] = len(self.b.payloads) - 1
        self.b.write(self.path); m, payloads = bem.read_package(self.path)
        self.assertLess(m['mesh_deformations'][0]['frames'][1]['payload'], len(payloads)); check_geometry(m, payloads)

    def test_legacy_formats_unchanged(self):
        from test_bem_v1 import fixture as old
        for builder, minor in ((old(), 0), (old_fixture(), 1)):
            builder.write(self.path); self.assertEqual(bem.package_minor(self.path), minor); check_geometry(*bem.read_package(self.path))

    def test_official_fp16_buffers_and_explicit_binding(self):
        records = [[0, .5, -1, 2], [2, 1, 0, -.25]]; buffers = official_buffers(records, 1)
        self.assertEqual(decode_official(*buffers, 1, 3), [tuple(r) for r in records])
        self.assertEqual(decode_official(*buffers, 1, 3, [2, 0, 2]), [(0, 1, 0, -.25), (1, .5, -1, 2), (2, 1, 0, -.25)])
        mod = self.root / 'mod'; mod.mkdir(); texts = []
        for suffix, raw, fmt in zip(('ShapeKeyBatchConfigs', 'ShapeKeyVertexIds', 'ShapeKeyVertexOffsets'), buffers,
                                    ('R32G32B32A32_UINT', 'R32_UINT', 'R16_FLOAT')):
            (mod / (suffix + '.buf')).write_bytes(raw)
            texts.append(f'[Resource_Component0_{suffix}]\nformat = {fmt}\nfilename = {suffix}.buf\n')
        (mod / 'mod.ini').write_text('\n'.join(texts))
        binding = dict(source='mod', ini='mod.ini', component=0, shape_key=1, vertex_order='exported')
        actual, report = decode_binding(binding, self.root, 3)
        self.assertEqual(actual, [tuple(r) for r in records]); self.assertEqual(report['binding_mode'], 'explicit-author-map')
        with self.assertRaisesRegex(ValueError, 'SHAPE_BINDING_REQUIRED'): require_shape_binding(mod, None)
        binding['vertex_order'] = 'unknown'
        with self.assertRaises(ValueError): decode_binding(binding, self.root, 3)
        bad = bytearray(buffers[0]); struct.pack_into('<I', bad, 48, 1)
        with self.assertRaises(ValueError): decode_official(bytes(bad), buffers[1], buffers[2], 1, 3)

    def test_official_buffer_to_existing_task_end_to_end(self):
        mod = self.root / 'efmi'; mod.mkdir(); texts = []
        for suffix, raw, fmt in zip(('ShapeKeyBatchConfigs', 'ShapeKeyVertexIds', 'ShapeKeyVertexOffsets'),
                official_buffers([[1, .5, 0, 0]], 1), ('R32G32B32A32_UINT', 'R32_UINT', 'R16_FLOAT')):
            (mod / (suffix + '.buf')).write_bytes(raw)
            texts.append(f'[Resource_Component0_{suffix}]\nformat = {fmt}\nfilename = {suffix}.buf\n')
        (mod / 'mod.ini').write_text('\n'.join(texts))
        demo = Path(__file__).parent / 'examples/body-slider/create_project.py'
        spec = importlib.util.spec_from_file_location('efmi_demo', demo); module = importlib.util.module_from_spec(spec); spec.loader.exec_module(module)
        task = module.create(self.root / 'demo'); shape_path = task.parent / 'body-morphs.json'
        authored = bem.load_json(shape_path)
        authored['mesh_deformations'][0]['frames'][1] = dict(value=1000,
            efmi=dict(source='../efmi', ini='mod.ini', component=0, shape_key=1, vertex_order='exported'))
        shape_path.write_text(json.dumps(authored))
        with contextlib.redirect_stdout(io.StringIO()): self.assertEqual(main(['build', str(task)]), 0)
        output = task.parent / 'dist/synthetic.bem'; m, payloads = bem.read_package(output)
        self.assertEqual(struct.unpack_from('<3f', bem_v13.morph_streams(m, payloads, 0, {'body': 500})[0], 16), (1.25, 0, 0))
        report = bem.load_json(task.parent / 'reports/build.json')
        self.assertEqual(report['deformations']['efmi_bindings'][0]['shape_key'], 1)
        self.native(output)

    def test_slider_only_manifest_empty_finite_groups(self):
        m = self.b.m; m['option_groups'] = []
        for draw in m['meshes'][0]['draws']: draw.pop('when', None)
        m['component_rules'][1]['candidates'] = [dict(operation='keep')]
        self.b.write(self.path); check_geometry(*bem.read_package(self.path), 3); self.native()

    def test_empty_non_neutral_frame_roundtrip(self):
        m = self.b.m; empty = self.b.payload(bem_v13.encode_deltas([], 3))
        m['mesh_deformations'][0]['frames'][1].update(payload=empty, count=0)
        self.b.write(self.path); check_geometry(*bem.read_package(self.path), 3); self.native()

    def test_end_to_end_existing_export_task_target_positions_and_roundtrip(self):
        demo = Path(__file__).parent / 'examples/body-slider/create_project.py'
        module = importlib.util.spec_from_file_location('body_slider_demo', demo); loaded = importlib.util.module_from_spec(module); module.loader.exec_module(loaded)
        task_path = loaded.create(self.root / 'demo')
        with contextlib.redirect_stdout(io.StringIO()): self.assertEqual(main(['build', str(task_path)]), 0)
        output = task_path.parent / 'dist/synthetic.bem'; self.assertEqual(bem.package_minor(output), 3); self.native(output)
        m, p = bem.read_package(output); stream = bem_v13.morph_streams(m, p, 0, {'body': 500})[0]
        self.assertEqual(struct.unpack_from('<3f', stream, 16), (1.25, 0, 0))
        task = bem_tasks.load_task(task_path); task['output'] = task['deformations']
        data = bem.load_json(task_path); data['output'] = 'target-positions.json'; task_path.write_text(json.dumps(data))
        with self.assertRaises(ValueError): bem_tasks.load_task(task_path)

    def test_recipe_inherited_shape_inputs_protected(self):
        spec = self.root / 'body-morphs.json'
        spec.write_text(json.dumps(dict(schema=1, kind='bem-position-morphs', parameters=[], mesh_deformations=[])))
        source = self.root / 'mod'; source.mkdir()
        recipe = self.root / 'recipe.json'
        recipe.write_text(json.dumps(dict(schema=1, package=dict(id='creator.test', name='test', author='test', version='1'), deformations=spec.name)))
        task_path = self.root / 'export.bemproj.json'
        bem_tasks.new_project(source, task_path, recipe=recipe)
        task = bem_tasks.load_task(task_path)
        self.assertIn(spec, task['input_paths']); self.assertEqual(task['deformations'], spec)
        before = spec.read_bytes()
        with contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(main(['convert', str(source), '--recipe', str(recipe), '-o', str(self.path), '--report', str(spec)]), 2)
        self.assertEqual(spec.read_bytes(), before)
        with self.assertRaisesRegex(ValueError, 'overwrite'): bem_tasks.new_project(source, spec, recipe=recipe)


if __name__ == '__main__': unittest.main()
