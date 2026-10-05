import contextlib
import io
import json
import shutil
import tempfile
from pathlib import Path
import unittest

import bem_v1 as bem
from build_bem14_target import build_profile, main
from test_native_models import fixture
from bem_tool import main as cli


def inputs():
    raw = fixture(); raw['objects'][0]['asset_paths'] = ['assets/test/character.prefab']
    raw['snapshot'] = dict(manifest_version='synthetic', perforce_cl='')
    spec = dict(schema=1, kind='bem-resource-target-spec', target_kind='character', id='chr_test',
                profile_id='draft.test', revision='1', snapshot='synthetic', resources=[
                    dict(id='body', asset_path='assets/test/character.prefab', platforms=['windows-x64'],
                         lod=0, renderer_paths=['mesh'])])
    return raw, spec


class BemTargetTests(unittest.TestCase):
    def test_precise_asset_and_receiver_draft_never_claims_runtime_ready(self):
        raw, spec = inputs(); profile, manifest = build_profile(raw, spec)
        self.assertFalse(profile['conversion_ready']); self.assertFalse(profile['runtime_verified'])
        self.assertEqual(profile['target']['components'][0]['renderer_path'], 'mesh')
        self.assertEqual(profile['target']['components'][0]['bone_names'], ['bone'])
        self.assertEqual(profile['target']['components'][0]['materials'], ['material'])
        bem.validate_manifest(manifest, 0)
        self.assertEqual(manifest['component_rules'][0]['candidates'], [dict(operation='keep')])

    def test_no_name_matching_or_guessed_lod(self):
        raw, spec = inputs()
        spec['resources'][0]['asset_path'] = 'assets/other/character.prefab'
        with self.assertRaisesRegex(ValueError, 'exact prefab'): build_profile(raw, spec)
        raw, spec = inputs(); spec['resources'][0].pop('renderer_paths')
        with self.assertRaisesRegex(ValueError, 'exact LOD branch'): build_profile(raw, spec)
        raw['objects'][1]['name'] = 'lod0'
        self.assertEqual(build_profile(raw, spec)[0]['target']['components'][0]['renderer_path'], 'lod0')
        spec['resources'][0]['renderer_paths'] = ['missing']
        with self.assertRaises(ValueError): build_profile(raw, spec)

    def test_incomplete_bones_or_missing_asset_provenance_rejected(self):
        raw, spec = inputs(); raw['objects'][7]['bones'] = []
        with self.assertRaisesRegex(ValueError, 'Incomplete offline'): build_profile(raw, spec)
        raw, spec = inputs(); raw['objects'][0].pop('asset_paths')
        with self.assertRaisesRegex(ValueError, 'exact prefab'): build_profile(raw, spec)

    def test_snapshot_and_platform_claims_require_matching_evidence(self):
        raw, spec = inputs(); spec['snapshot'] = 'different'
        with self.assertRaisesRegex(ValueError, 'snapshot differs'): build_profile(raw, spec)
        raw, spec = inputs(); raw['snapshot']['bundles'] = [dict(path='Bundles/Windows/test.ab')]
        spec['resources'][0]['platforms'] = ['android-arm64']
        with self.assertRaisesRegex(ValueError, 'platforms lack'): build_profile(raw, spec)

    def test_cli_output_must_not_overwrite_input(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory); raw, spec = inputs()
            graph, request = root / 'graph.json', root / 'spec.json'
            graph.write_text(json.dumps(raw)); request.write_text(json.dumps(spec))
            with self.assertRaisesRegex(ValueError, 'overwrite'):
                main([str(graph), '--spec', str(request), '--output', str(graph)])

    def test_creator_cli_embeds_target_generator_and_protects_report_inputs(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory); raw, spec = inputs()
            graph, request = root / 'graph.json', root / 'spec.json'
            graph.write_text(json.dumps(raw)); request.write_text(json.dumps(spec))
            profile, project = root / 'profile.json', root / 'project.json'
            args = ['target-profile', str(graph), '--spec', str(request), '-o', str(profile), '--project', str(project)]
            with contextlib.redirect_stdout(io.StringIO()) as capture:
                self.assertEqual(cli(args), 0)
            report = json.loads(capture.getvalue())
            self.assertFalse(report['conversion_ready']); self.assertFalse(report['runtime_verified'])
            self.assertEqual(report['format_version'], '1.4')
            self.assertTrue(project.is_file())
            original = request.read_bytes()
            with contextlib.redirect_stdout(io.StringIO()):
                self.assertEqual(cli([*args, '--report', str(request)]), 2)
            self.assertEqual(request.read_bytes(), original)

    def test_packaged_example_builds_after_distribution_move(self):
        from package_toolchain import prepare_bem14_example
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            prepare_bem14_example(Path(__file__).resolve().parents[2], root / 'staged')
            shutil.move(str(root / 'staged'), str(root / 'moved'))
            project = root / 'moved/examples/multi-resource/project'
            with contextlib.redirect_stdout(io.StringIO()):
                self.assertEqual(cli(['build', str(project / 'export.bemproj.json')]), 0)
            self.assertEqual(bem.package_minor(project / 'dist/synthetic.bem'), 4)


if __name__ == '__main__': unittest.main()
