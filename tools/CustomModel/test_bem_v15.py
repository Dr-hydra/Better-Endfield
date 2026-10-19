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
