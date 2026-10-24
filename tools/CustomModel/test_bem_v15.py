import copy
import importlib.util
import io
import json
import os
import subprocess
import sys
import tempfile
import unittest
from contextlib import redirect_stdout
from pathlib import Path

import bem_v1 as bem
import bem_v11
import bem_v15
from test_bem_v11 import fixture as composable_fixture
from bem_tool import main

spec = importlib.util.spec_from_file_location(
    'multi_resource_example', Path(__file__).parent / 'examples/multi-resource/create_project.py')
example = importlib.util.module_from_spec(spec)
spec.loader.exec_module(example)


class Bem15Tests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        self.path = self.root / 'material-overrides.bem'
        self.b = example.fixture()
        self.b.m['required_capabilities'].extend(bem_v15.CAPABILITIES)
        self.b.m['meshes'][0]['draws'][0]['material_overrides'] = {
            'floats': {'_Cutoff': 0.25},
            'colors': {'_Color': [1, 1, 1, 0.5]},
            'vectors': {'_Offset': [0, 0, 0, 1]},
            'enable_keywords': ['TRANSPARENT'],
        }
        self.b.m['root_offset'] = {'path': 'root', 'translation': [0, 0.15, 0]}

    def test_roundtrip_uses_minor_5_and_cli_validation(self):
        bem.write_package(self.path, self.b.m, self.b.payloads)
        self.assertEqual(bem.package_minor(self.path), 5)
        manifest, payloads = bem.read_package(self.path)
        self.assertEqual(manifest, self.b.m)
        self.assertEqual(bem_v11.required_minor(
            manifest, bem_v11.analyze_selection_space(manifest, payloads), len(payloads)), 5)
        output = io.StringIO()
        with redirect_stdout(output):
            self.assertEqual(main(['validate', str(self.path)]), 0)
        report = json.loads(output.getvalue())
        self.assertTrue(report['success'])
        self.assertEqual(report['format_version'], '1.5')
        self.assertEqual(report['tool_version'], '1.6.1')

    def test_material_overrides_require_their_capability(self):
        m = copy.deepcopy(self.b.m)
        m['required_capabilities'].remove('material-overrides-v1')
        with self.assertRaises(ValueError):
            bem.write_package(self.root / 'invalid.bem', m, self.b.payloads)

    def test_root_offset_bounds_and_path(self):
        for offset in (
            {'path': 'root', 'translation': [0, 0.3, 0]},
            {'path': '../root', 'translation': [0, 0, 0]},
            {'path': 'root', 'translation_bits': [0, 0]},
        ):
            with self.subTest(offset=offset):
                m = copy.deepcopy(self.b.m)
                m['root_offset'] = offset
                with self.assertRaises(ValueError):
                    bem.write_package(self.root / 'invalid.bem', m, self.b.payloads)

    def test_generic_tree_validation_allows_override_numbers(self):
        manifest = copy.deepcopy(self.b.m)
        overrides = manifest['meshes'][0]['draws'][0]['material_overrides']
        overrides['floats']['_Int'] = 1
        overrides['colors']['_IntegerColor'] = [0, 1, 0, 1]
        overrides['vectors']['_IntegerVector'] = [0, 1, 0, 1]
        bem.write_package(self.root / 'integer-values.bem', manifest, self.b.payloads)

    def test_drivers_roundtrip_uses_minor_5(self):
        m = copy.deepcopy(self.b.m)
        m['drivers'] = [
            {'id': 'thrust', 'name': '往复', 'waveform': 'pingpong',
             'default_playing': False, 'base_frequency': 1.2, 'speed_multiplier': 1.0,
             'bindings': [
                 {'type': 'morph_parameter', 'target': 'length', 'range': [0.2, 0.9]},
                 {'type': 'material_float', 'component': 0, 'material_slot': 0,
                  'property': '_EmissionWeight', 'range': [0.0, 1.5]},
                 {'type': 'material_color', 'component': 1, 'material_slot': 0,
                  'property': '_EmissionColor', 'colors': [[0, 0, 0, 1], [1, 0.5, 0, 1]]},
                 {'type': 'material_keyword', 'component': 2, 'material_slot': 0,
                  'keyword': 'EMISSION_ON', 'threshold': 0.25},
             ]},
        ]
        path = self.root / 'drivers.bem'
        bem.write_package(path, m, self.b.payloads)
        self.assertEqual(bem.package_minor(path), 5)
        manifest, _ = bem.read_package(path)
        self.assertEqual(manifest['drivers'], m['drivers'])
        self.assertEqual(bem_v11.required_minor(
            manifest, bem_v11.analyze_selection_space(manifest, _), len(_)), 5)
        output = io.StringIO()
        with redirect_stdout(output):
            self.assertEqual(main(['validate', str(path)]), 0)
        self.assertTrue(json.loads(output.getvalue())['success'])

    def test_driver_playback_is_a_closed_set(self):
        base = copy.deepcopy(self.b.m)
        base["drivers"] = [{"id": "one", "waveform": "sawtooth", "playback": "once",
                            "bindings": [{"type": "morph_parameter", "target": "length"}]}]
        path = self.root / "once.bem"
        bem.write_package(path, base, self.b.payloads)
        manifest, _ = bem.read_package(path)
        self.assertEqual(manifest["drivers"][0]["playback"], "once")

        # An unknown mode must be rejected at pack time, matching the runtime rule
        # that drops the controller instead of silently looping it.
        for mode in ("bounce", "", 1):
            broken = copy.deepcopy(base)
            broken["drivers"][0]["playback"] = mode
            with self.assertRaises(ValueError):
                bem.write_package(self.root / ("bad-" + str(mode) + ".bem"), broken, self.b.payloads)

    def test_drivers_require_capability_and_declared_parameter(self):
        base = copy.deepcopy(self.b.m)
        base['drivers'] = [{'id': 'pulse', 'waveform': 'sine',
                            'bindings': [{'type': 'morph_parameter', 'target': 'length'}]}]
        missing_capability = copy.deepcopy(base)
        missing_capability['required_capabilities'].remove('dynamic-drivers-v1')
        with self.assertRaises(ValueError):
            bem.write_package(self.root / 'no-cap.bem', missing_capability, self.b.payloads)
        missing_parameter = copy.deepcopy(base)
        missing_parameter['drivers'][0]['bindings'][0]['target'] = 'not_a_parameter'
        with self.assertRaises(ValueError):
            bem.write_package(self.root / 'no-parameter.bem', missing_parameter, self.b.payloads)

    def test_driver_binding_bounds(self):
        def driver(binding=None, **fields):
            entry = {'id': 'd', 'bindings': [binding or {'type': 'material_float',
                                                         'property': '_F', 'range': [0, 1]}]}
            entry.update(fields)
            return [entry]
        invalid = (
            driver(waveform='square'),
            driver(default_playing='yes'),
            driver(base_frequency=25.0),
            driver({'type': 'material_float', 'property': 'NoUnderscore'}),
            driver({'type': 'material_float', 'property': '_F', 'range': [1, 0]}),
            driver({'type': 'material_float', 'property': '_F', 'component': 9}),
            driver({'type': 'material_float', 'property': '_F', 'material_slot': 4}),
            driver({'type': 'material_color', 'property': '_C',
                    'colors': [[0, 0, 0, 1], [0, 0, 0]]}),
            driver({'type': 'material_keyword', 'keyword': 'lower_case'}),
            driver({'type': 'material_keyword', 'keyword': 'OK', 'threshold': 2}),
            driver({'type': 'material_vector', 'property': '_V'}),
            [{'waveform': 'sine', 'bindings': [{'type': 'material_float', 'property': '_F'}]}],
            [{'id': 'd', 'bindings': [{'type': 'material_float', 'property': '_F'}]}] * 2,
        )
        for drivers in invalid:
            with self.subTest(drivers=drivers):
                m = copy.deepcopy(self.b.m)
                m['drivers'] = drivers
                with self.assertRaises(ValueError):
                    bem.write_package(self.root / 'invalid.bem', m, self.b.payloads)

    def test_export_declares_driver_capability(self):
        import bem_export
        m = copy.deepcopy(self.b.m)
        m['required_capabilities'] = [c for c in m['required_capabilities']
                                      if c not in ('material-overrides-v1', 'root-offset-v1',
                                                   'dynamic-drivers-v1')]
        m['drivers'] = [{'id': 'pulse', 'waveform': 'sine',
                         'bindings': [{'type': 'morph_parameter', 'target': 'length'}]}]
        manifest, _ = bem_export.prepare_export(m, self.b.payloads)
        self.assertIn('dynamic-drivers-v1', manifest['required_capabilities'])

    def test_drivers_on_a_legacy_target_round_trip_as_minor_5(self):
        # 1.5 extends 1.3: a package without explicit resource targets is still a
        # valid 1.5 package, so the clock controller does not force a rebuild.
        b = composable_fixture()
        m = copy.deepcopy(b.m)
        m['required_capabilities'] = list(m['required_capabilities']) + ['dynamic-drivers-v1']
        m['drivers'] = [{'id': 'gloss', 'waveform': 'sine', 'base_frequency': 0.5,
                         'bindings': [{'type': 'material_float', 'component': 0,
                                       'material_slot': 0, 'property': '_Smoothness',
                                       'range': [0.2, 0.9]}]}]
        path = self.root / 'legacy-target.bem'
        bem.write_package(path, m, b.payloads)
        self.assertEqual(bem.package_minor(path), 5)
        manifest, payloads = bem.read_package(path)
        self.assertNotIn('resources', manifest['target'])
        self.assertEqual(manifest['drivers'], m['drivers'])
        self.assertEqual(bem_v11.required_minor(
            manifest, bem_v11.analyze_selection_space(manifest, payloads), len(payloads)), 5)
        output = io.StringIO()
        with redirect_stdout(output):
            self.assertEqual(main(['validate', str(path)]), 0)
        self.assertTrue(json.loads(output.getvalue())['success'])

    def test_legacy_target_rejects_resource_only_fields(self):
        b = composable_fixture()

        def legacy(**extra):
            m = copy.deepcopy(b.m)
            m['required_capabilities'] = list(m['required_capabilities']) + [
                'dynamic-drivers-v1', *extra.get('capabilities', ())]
            m['drivers'] = [{'id': 'gloss', 'waveform': 'sine',
                             'bindings': [{'type': 'material_float', 'component': 0,
                                           'material_slot': 0, 'property': '_Smoothness'}]}]
            if 'mutate' in extra:
                extra['mutate'](m)
            return m

        mutations = (
            {'capabilities': ('multi-resource-targets',)},
            {'capabilities': ('static-meshes',)},
            {'mutate': lambda m: m['meshes'][0].__setitem__('renderer_kind', 'skinned')},
            {'mutate': lambda m: m['target']['components'][0].__setitem__('resource', 'body')},
        )
        for extra in mutations:
            with self.subTest(**{k: v for k, v in extra.items() if k != 'mutate'}):
                with self.assertRaises(ValueError):
                    bem.write_package(self.root / 'invalid.bem', legacy(**extra), b.payloads)

    def test_old_packages_do_not_need_the_v15_module(self):
        legacy = example.fixture()
        path = self.root / 'legacy.bem'
        legacy.write(path)
        script = '\n'.join((
            'import builtins, sys',
            'from pathlib import Path',
            'import bem_v1 as bem',
            'original = builtins.__import__',
            'def guarded(name, *args, **kwargs):',
            '    if name == "bem_v15":',
            '        raise AssertionError("BEM 1.4 validation imported bem_v15")',
            '    return original(name, *args, **kwargs)',
            'builtins.__import__ = guarded',
            'bem.read_package(Path(sys.argv[1]))',
        ))
        result = subprocess.run(
            [sys.executable, '-c', script, str(path)],
            cwd=Path(__file__).parent, capture_output=True, text=True,
            env={**os.environ, 'PYTHONDONTWRITEBYTECODE': '1'})
        self.assertEqual(result.returncode, 0, result.stdout + result.stderr)


if __name__ == '__main__':
    unittest.main()
