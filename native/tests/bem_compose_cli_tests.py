"""Native integration: python bem_compose_cli_tests.py <BemCompose.exe>."""
import copy
import json
from pathlib import Path
import subprocess
import sys
import tempfile
import unittest

ROOT = Path(__file__).resolve().parents[2]
sys.path.insert(0, str(ROOT / 'tools' / 'CustomModel'))
import bem_v1
import bem_projects
from bem_v11 import selection_plan
from bem_tool import check_geometry
from test_bem_v1 import fixture as legacy
from test_bem_v11 import fixture as options
from test_bem_v13 import fixture as morph
from test_bem_v14 import example

EXE = Path(sys.argv.pop(1)).resolve()

class ComposerTests(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.TemporaryDirectory(prefix='bem-compose-组合-')
        self.addCleanup(self.tmp.cleanup)
        self.root = Path(self.tmp.name)
        self.request = dict(schema=1, platform='windows-x64', project_id='test.composition',
                            name='组合测试', author='Test', version='1', sources=[])

    def source(self, builder, selection='', suffix='.bem'):
        path = self.root / ('source-' + str(len(self.request['sources'])) + '.bem')
        builder.m['package_id'] += str(len(self.request['sources']))
        builder.write(path)
        if suffix == '.zip':
            archive = path.with_suffix('.zip')
            bem_projects.pack_zip(path, archive)
            path = archive
        self.request['sources'].append(dict(path=str(path), selection=selection, parameters=''))
        return path

    @staticmethod
    def explicit():
        builder = example.fixture()
        builder.m.pop('parameters', None)
        builder.m.pop('mesh_deformations', None)
        builder.m['required_capabilities'] = [c for c in builder.m['required_capabilities']
            if c not in ('body-parameters', 'mesh-position-deltas')]
        return builder

    def cli(self, *args, error=None):
        result = subprocess.run([str(EXE), *map(str, args)], capture_output=True, encoding='utf-8')
        body = json.loads(result.stdout)
        if error is None:
            self.assertEqual(result.returncode, 0, body)
            return body['result']
        self.assertNotEqual(result.returncode, 0, body)
        self.assertIn(error, body['error'])
        return body

    @classmethod
    def whole(cls, weapon=False, root='ultimate', target_id=None):
        builder = cls.explicit()
        index = 2 if weapon else 1
        target = builder.m['target']
        target['kind'] = 'weapon' if weapon else 'character'
        target['id'] = target_id or ('wpn_test' if weapon else 'chr_test')
        resource = target['resources'][index]
        resource.update(id='whole', name=root, asset_path=f'assets/synthetic/{root}.prefab',
                        platforms=['windows-x64'], lod=0)
        component = target['components'][index]
        component.update(id=0, resource='whole')
        mesh = builder.m['meshes'][index]
        for bone in mesh['bones']:
            bone['component'] = 0
        for draw in mesh['draws']:
            draw['material_component'] = 0
        target['resources'] = [resource]
        target['components'] = [component]
        builder.m['meshes'] = [mesh]
        builder.m['component_rules'] = [dict(target=0, candidates=[dict(operation='replace', mesh=0)])]
        return builder

    def plan(self, error=None):
        request = self.root / 'request.json'
        request.write_text(json.dumps(self.request, ensure_ascii=False), encoding='utf-8')
        return self.cli('plan', '--request', request, error=error)

    def build(self, plan, defaults=None, error=None, directory='output', command='build'):
        plan_file = self.root / 'plan.json'
        plan_file.write_text(json.dumps(plan), encoding='utf-8')
        choices = self.root / 'choices.json'
        choices.write_text(json.dumps(defaults or {}), encoding='utf-8')
        return self.cli(command, '--plan', plan_file, '--choices', choices,
                        '--output-dir', self.root / directory, error=error)

    @staticmethod
    def body_fixture(unknown=False):
        import struct
        builder = options()
        builder.m['target']['components'][0]['mesh_name'] = 'S_actor_test_body_01_lod0'
        names = ['custom0', 'custom1'] if unknown else ['Bip001_Neck', 'Bip001_Spine2']
        for i, name in enumerate(names):
            builder.m['target']['components'][i]['bone_names'] = [name]
            builder.m['meshes'][0]['bones'][i]['name'] = name
        mesh = builder.m['meshes'][0]
        mesh['vertex_count'] = 6
        mesh['streams'][0]['payload'] = builder.payload(struct.pack('<24f', *([0.0] * 24)))
        mesh['streams'][1]['payload'] = builder.payload(bytes(72))
        mesh['streams'][2]['payload'] = builder.payload(b''.join(struct.pack('<4H4B',65535,0,0,0,0 if i < 3 else 1,0,0,0) for i in range(6)))
        for i, draw in enumerate(mesh['draws']):
            draw['when'] = True
            draw['indices'] = builder.payload(struct.pack('<3H', i*3, i*3+1, i*3+2))
        return builder

    def test_body_regions_partition_all_triangles_and_preserve_geometry(self):
        self.source(self.body_fixture()); self.source(self.body_fixture())
        plan = self.plan(); slot = next(slot for slot in plan['slots'] if any('body_region' in s for s in slot['segments']))
        source = [s for s in slot['segments'] if s['source_id'] == 's0']
        self.assertEqual(sum(s['count'] for s in source), 6)
        self.assertEqual({s['body_region'] for s in source if s['count']}, {'neck','torso'})
        self.assertEqual(len({(s['body_draw'], t) for s in source for t in s['triangles']}), 2)
        defaults = {slot['id']: 'split', **{s['id']: 'on' if s['source_id'] == 's0' else 'off' for s in slot['segments']}}
        report = self.build(plan, defaults)
        m, payloads = bem_v1.read_package(Path(report['outputs'][0])); check_geometry(m,payloads,2)
        draws = [d for mesh in m['meshes'] for d in mesh['draws'] if 'composition_body_region' in d]
        self.assertEqual(sum(d['count'] for d in draws), 6)
        self.assertEqual({d['composition_body_region'] for d in draws}, {'neck','torso'})
        self.cli('validate', '--package', report['outputs'][0])

    def test_android_legacy_body_composition(self):
        self.request['platform'] = 'android-arm64'
        self.source(self.body_fixture()); self.source(self.body_fixture())
        plan = self.plan()
        self.assertEqual(plan['platform'], 'android-arm64')
        slot = plan['slots'][0]
        selected = {slot['id']: 'split', **{s['id']: 'on' if s['source_id']=='s0' else 'off' for s in slot['segments']}}
        report = self.build(plan, selected)
        self.assertEqual(report['platform'], 'android-arm64')
        manifest, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(manifest, payloads, 2)

    def test_android_explicit_resources_filtered(self):
        self.request['platform'] = 'android-arm64'
        for i in range(2):
            builder = self.explicit()
            for resource in builder.m['target']['resources']:
                resource['platforms'] = ['windows-x64', 'android-arm64']
            self.source(builder)
        plan = self.plan()
        choices = {a['id']: next(c['id'] for c in a['candidates'] if c['id']!='original') for a in plan['atomic_items']}
        report = self.build(plan, choices)
        self.assertEqual(report['platform'], 'android-arm64')
        for output in report['outputs']:
            manifest, payloads = bem_v1.read_package(Path(output))
            self.assertTrue(all(r['platforms']==['android-arm64'] for r in manifest['target']['resources']))

    def test_android_rejects_windows_only_resources(self):
        self.request['platform'] = 'android-arm64'
        for i in range(2):
            builder = self.explicit()
            for resource in builder.m['target']['resources']: resource['platforms'] = ['windows-x64']
            self.source(builder)
        self.plan(error='COMPOSE_')

    def test_unknown_composition_platform_rejected(self):
        self.request['platform'] = 'unknown-platform'
        self.source(options()); self.source(options())
        self.plan(error='COMPOSE_UNSUPPORTED_PLATFORM')

    def test_body_manual_assignment_is_saved_and_preview_maps_original_faces(self):
        self.source(self.body_fixture()); self.source(self.body_fixture())
        plan=self.plan(); slot=next(slot for slot in plan['slots'] if any('body_region' in s for s in slot['segments']))
        neck=next(s for s in slot['segments'] if s['source_id']=='s0' and s['body_region']=='neck' and s['count'])
        defaults={slot['id']:'split', **{s['id']:'on' if s['source_id']=='s0' else 'off' for s in slot['segments']}, 'body_edits':{neck['body_draw']:{'0':'torso'}}}
        report=self.build(plan,defaults);m,payloads=bem_v1.read_package(Path(report['outputs'][0]));check_geometry(m,payloads,2)
        draws=[d for mesh in m['meshes'] for d in mesh['draws'] if 'composition_body_region' in d]
        self.assertTrue(all(d['composition_body_region']=='torso' for d in draws));self.assertEqual(sum(d['count'] for d in draws),6)
        self.assertEqual(report['defaults']['body_edits'],defaults['body_edits'])
        out=self.root/'body-scene';self.cli('preview','--package',report['outputs'][0],'--output-dir',out,'--gpu',json.dumps({'desktop':True}))
        scene=json.loads((out/'scene.json').read_text());mapped=[d for mesh in scene['meshes'] for d in mesh['draws'] if d.get('bodyDraw')]
        self.assertEqual(sum(len(d['bodyTriangles']) for d in mapped),2)
        self.assertEqual({d['bodyRegion'] for d in mapped},{'torso'})
        # A saved manual assignment remains meaningful when used as a new source.
        again=copy.deepcopy(self.request);again['project_id']='test.body.recomposed'
        again['sources'][0]['path']=report['outputs'][0]
        request=self.root/'body-again.json';request.write_text(json.dumps(again),encoding='utf-8')
        replanned=self.cli('plan','--request',request)
        edited_meshes={i for i,mesh in enumerate(m['meshes']) if any('composition_body_region' in d for d in mesh['draws'])}
        first=[s for g in replanned['slots'] for s in g['segments'] if s['source_id']=='s0' and s['mesh'] in edited_meshes and s.get('body_region') and s['count']]
        self.assertEqual({s['body_region'] for s in first},{'torso'})
        for bad in [{'unknown':{'0':'torso'}},{neck['body_draw']:{'9':'torso'}},{neck['body_draw']:{'0':'invalid'}}]:
            self.build(plan,{**defaults,'body_edits':bad},error='COMPOSE_BODY:',directory='invalid-body')

    def test_unknown_body_bones_are_not_dropped_or_guessed(self):
        self.source(self.body_fixture(True));self.source(self.body_fixture(True));plan=self.plan()
        segments=next(slot['segments'] for slot in plan['slots'] if any('body_region' in s for s in slot['segments']))
        self.assertEqual({s['body_region'] for s in segments if s['count']},{'unclassified'})
        self.assertEqual(sum(s['count'] for s in segments),12)

    def test_body_regions_recognize_limb_twist_names(self):
        builder=self.body_fixture()
        for i,name in enumerate(['Bip001_LThighTwist','Bip001_RUpArmTwist1']):
            builder.m['target']['components'][i]['bone_names']=[name]
            builder.m['meshes'][0]['bones'][i]['name']=name
        self.source(builder);self.source(copy.deepcopy(builder))
        segments=next(g['segments'] for g in self.plan()['slots'] if any('body_region' in s for s in g['segments']))
        self.assertEqual({s['body_region'] for s in segments if s['count']},{'left_leg','right_arm'})

    def test_legacy_index_slices_donor_union_and_all_combinations(self):
        a = self.source(legacy())
        b = self.source(legacy(), 'hidden')
        originals = [a.read_bytes(), b.read_bytes()]
        plan = self.plan()
        self.assertEqual(plan['estimated_components'], 2)
        report = self.build(plan, {plan['slots'][0]['id']: 's1'})
        output = Path(report['outputs'][0])
        manifest, payloads = bem_v1.read_package(output)
        check_geometry(manifest, payloads, 2)
        mesh = manifest['meshes'][0]
        self.assertEqual([len(payloads[d['indices']]) for d in mesh['draws']], [6, 6])
        self.assertEqual([d['material_component'] for d in mesh['draws']], [0, 1])
        self.assertEqual(manifest['option_groups'][0]['default'], 's1')
        self.cli('validate', '--package', output)
        self.assertEqual([a.read_bytes(), b.read_bytes()], originals)
        self.assertNotIn('hash', json.dumps(report).lower())
        self.assertFalse(list(self.root.glob('output.stage-*')))

    def test_source_options_and_conditional_draws_survive_zip(self):
        self.source(options(), 'outfit:b&part:off')
        self.source(options(), 'outfit:a', '.zip')
        report = self.build(self.plan())
        m, p = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, p, 2)
        self.assertTrue(all('when' in d for mesh in m['meshes'] for d in mesh['draws']))
        self.assertEqual([len(mesh['draws']) for mesh in m['meshes']], [2, 2])
        groups = {g['id']: g for g in m['option_groups']}
        self.assertEqual(groups['s0.g0']['default'], 'b')
        self.assertEqual(groups['s0.g1']['default'], 'off')
        initial = selection_plan(m)
        changed = selection_plan(m, {'s0.g0': 'a'})
        self.assertEqual(initial['components'][1]['operation'], 'hide')
        self.assertEqual(changed['components'][1]['operation'], 'keep')
        self.assertNotEqual(initial['components'][0]['draws'][0]['indices'],
                            changed['components'][0]['draws'][0]['indices'])
        self.assertIsNone(changed['effective']['s0.g1'])
        self.assertEqual(changed['saved']['s0.g1'], 'off')

    def test_preview_can_apply_outer_and_imported_source_defaults(self):
        self.source(options(), 'outfit:a')
        self.source(options(), 'outfit:a')
        plan = self.plan()
        defaults = {g['id']: 's1' for g in plan['slots']}
        defaults.update({'s1.g0': 'b', 's1.g1': 'off'})
        report = self.build(plan, defaults)
        manifest, _ = bem_v1.read_package(Path(report['outputs'][0]))
        groups = {g['id']: g for g in manifest['option_groups']}
        self.assertEqual(groups['s1.g0']['default'], 'b')
        self.assertEqual(groups['s1.g1']['default'], 'off')
        self.assertTrue(all(groups[g['id']]['default'] == 's1' for g in plan['slots']))
        self.assertEqual(selection_plan(manifest)['components'][1]['operation'], 'hide')
        self.build(plan, {'s0.g0': 'missing'}, error='COMPOSE_CHOICES', directory='bad-choice')
        self.build(plan, {'s0.g99': 'a'}, error='COMPOSE_CHOICES', directory='bad-group')

    def test_split_preview_defaults_are_scoped_to_output(self):
        self.source(options(), 'outfit:a')
        self.source(self.whole())
        plan = self.plan()
        part = next(p for p in plan['projects'] if p['plan']['slots'])
        prefix = part['id'] + '.'
        report = self.build(plan, {prefix + 's0.g0': 'b', prefix + 's0.g1': 'off'})
        manifest, _ = bem_v1.read_package(Path(report['outputs'][0]))
        groups = {g['id']: g for g in manifest['option_groups']}
        self.assertEqual(groups['s0.g0']['default'], 'b')
        self.assertEqual(groups['s0.g1']['default'], 'off')
        self.build(plan, {prefix + 's0.g99': 'a'}, error='COMPOSE_CHOICES', directory='bad-group')
        self.build(plan, {prefix + 's0.g0': 'missing'}, error='COMPOSE_CHOICES', directory='bad-choice')

    def test_draw_segments_mix_sources_and_preserve_source_variants(self):
        self.source(options())
        self.source(options())
        plan = self.plan()
        group = next(g for g in plan['slots'] if g['segments'])
        segments = group['segments']
        defaults = {group['id']: 'split', **{s['id']: 'off' for s in segments}}
        chosen = [next(s for s in segments if s['source_id'] == 's0' and s['draw'] == 0),
                  next(s for s in segments if s['source_id'] == 's1' and s['draw'] == 1)]
        defaults.update({s['id']: 'on' for s in chosen})
        defaults['s1.g0'] = 'b'
        report = self.build(plan, defaults)
        m, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, payloads, 2)
        selected = selection_plan(m)
        self.assertEqual(len(selected['components'][0]['draws']), 2)
        mesh = next(x for x in m['meshes'] if any(d.get('composition_segment') == chosen[0]['preview_key'] for d in x['draws']) and x['vertex_count'] == 6)
        self.assertEqual(mesh['index_size'], 4)
        self.assertEqual(mesh['vertex_count'], 6)
        self.assertEqual(len(mesh['bones']), 2)
        import struct
        self.assertEqual(struct.unpack('<3I', payloads[mesh['draws'][0]['indices']]), (0, 1, 2))
        self.assertEqual(struct.unpack('<3I', payloads[mesh['draws'][1]['indices']]), (5, 4, 3))
        changed = selection_plan(m, {'s0.g0': 'b', 's1.g0': 'a'})
        self.assertEqual(changed['components'][0]['operation'], 'hide')
        self.assertEqual(len(m['composition_segments']), 4)
        self.assertEqual(sum(s['enabled'] for s in m['composition_segments']), 2)
        # Segment selection is an authored custom preset; it does not consume
        # a runtime option group for every draw or flatten source option logic.
        self.assertFalse(any('.d' in g['id'] for g in m['option_groups']))
        self.cli('preview', '--package', report['outputs'][0], '--output-dir', self.root / 'preview')
        scene = json.loads((self.root / 'preview' / 'scene.json').read_text(encoding='utf-8'))
        self.assertEqual({d['segment'] for x in scene['meshes'] for d in x['draws']}, {s['preview_key'] for s in chosen})

    def test_segment_merge_remaps_reordered_skin_palette(self):
        a, b = options(), options()
        stream = b.m['meshes'][0]['streams'][2]
        raw = bytearray(b.payloads[stream['payload']])
        for v in range(3):
            for k in range(4):
                raw[v * 12 + 8 + k] = 1 - raw[v * 12 + 8 + k]
        b.payloads[stream['payload']] = bytes(raw)
        b.m['meshes'][0]['bones'].reverse()
        self.source(a)
        self.source(b)
        plan = self.plan()
        group = next(g for g in plan['slots'] if g['segments'])
        defaults = {group['id']: 'split', **{s['id']: 'on' for s in group['segments']}}
        report = self.build(plan, defaults)
        m, p = bem_v1.read_package(Path(report['outputs'][0]))
        mesh = next(x for x in m['meshes'] if x['vertex_count'] == 6)
        skin = p[mesh['streams'][2]['payload']]
        self.assertEqual(skin[:36], skin[36:])
        self.assertEqual(len(mesh['bones']), 2)

    def test_segment_merge_rejects_layout_mismatch(self):
        a, b = options(), options()
        b.m['meshes'][0]['attributes'][1][1] = 10  # Valid UInt32 rather than Float32.
        self.source(a)
        self.source(b)
        plan = self.plan()
        group = next(g for g in plan['slots'] if g['segments'])
        self.assertGreater(len({json.dumps(segment['layout'], sort_keys=True) for segment in group['segments']}), 1)
        defaults = {group['id']: 'split', **{s['id']: 'on' for s in group['segments']}}
        self.build(plan, defaults, error='COMPOSE_SEGMENT_LAYOUT')
        self.assertFalse((self.root / 'output').exists())

    def test_segment_merge_checks_indices_before_vertex_rebasing(self):
        import struct
        a, b = options(), options()
        draw = b.m['meshes'][0]['draws'][0]
        b.payloads[draw['indices']] = struct.pack('<3H', 0, 1, 4)
        self.source(a)
        self.source(b)
        plan = self.plan()
        group = next(g for g in plan['slots'] if g['segments'])
        defaults = {group['id']: 'split', **{s['id']: 'on' for s in group['segments']}}
        self.build(plan, defaults, error='COMPOSE_SEGMENT_PAYLOAD')
        self.assertFalse((self.root / 'output').exists())

    def test_empty_segment_selection_hides_receiver_and_bad_choice_rejected(self):
        self.source(options())
        self.source(options())
        plan = self.plan()
        group = next(g for g in plan['slots'] if g['segments'])
        defaults = {group['id']: 'split', **{s['id']: 'off' for s in group['segments']}}
        report = self.build(plan, defaults)
        m, _ = bem_v1.read_package(Path(report['outputs'][0]))
        self.assertEqual(selection_plan(m)['components'][0]['operation'], 'hide')
        self.build(plan, {group['segments'][0]['id']: 'wrong'}, error='COMPOSE_CHOICES', directory='bad-segment')

    def test_segments_support_legacy_sliced_indices_and_split_projects(self):
        self.source(legacy())
        self.source(self.whole())
        plan = self.plan()
        group = next(g for g in plan['slots'] if g['segments'])
        defaults = {group['id']: 'split', **{s['id']: 'on' for s in group['segments']}}
        report = self.build(plan, defaults)
        m, p = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, p, 2)
        self.assertEqual(selection_plan(m)['components'][0]['operation'], 'replace')
        self.assertTrue(m['composition_segments'])
        self.cli('validate', '--package', report['outputs'][0])

    def test_conditional_texture_slot_preserves_null_and_other_choices(self):
        builder = options()
        builder.m['required_capabilities'].append('texture-slots')
        builder.m['texture_slots'] = [dict(id='color', candidates=[
            dict(texture=None, when={'eq': ['outfit', 'a']}),
            dict(texture=0, when={'eq': ['outfit', 'b']})])]
        for draw in builder.m['meshes'][0]['draws']:
            draw['textures'] = [dict(slot='color')]
        self.source(builder)
        self.source(options())
        report = self.build(self.plan())
        m, p = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, p, 2)
        self.assertEqual(selection_plan(m)['components'][0]['draws'][0]['textures'], [])
        selected = selection_plan(m, {'s0.g0': 'b'})
        self.assertEqual(len(selected['components'][0]['draws'][0]['textures']), 1)
        inactive = selection_plan(m, {'part0': 'original', 'part1': 'original'})
        self.assertTrue(all(value is None for value in inactive['texture_slots'].values()))
        self.assertTrue(all(value is None for key, value in inactive['effective'].items() if key.startswith('s0.')))

    def test_resource_budget_counts_only_coexisting_draws_and_texture_slots(self):
        builder = options()
        builder.m['required_capabilities'].append('texture-slots')
        second = copy.deepcopy(builder.m['textures'][0])
        second['payload'] = builder.payload(bytes(builder.payloads[second['payload']]))
        builder.m['textures'].append(second)
        builder.m['texture_slots'] = [dict(id='color', candidates=[
            dict(texture=0, when={'eq': ['outfit', 'a']}),
            dict(texture=1, when={'eq': ['outfit', 'b']})])]
        for draw in builder.m['meshes'][0]['draws']:
            draw['textures'] = [dict(slot='color')]
        self.source(builder)
        self.source(copy.deepcopy(builder))
        report = self.build(self.plan())
        mesh = builder.m['meshes'][0]
        geometry = sum(mesh['vertex_count'] * stream['stride'] for stream in mesh['streams'])
        # One of two draws and one of two texture alternatives can coexist.
        expected = geometry + mesh['draws'][0]['count'] * mesh['index_size']
        expected += len(builder.payloads[builder.m['textures'][0]['payload']])
        self.assertTrue(report['budget_exact'])
        self.assertEqual(report['worst_case_decoded_bytes_bound'], expected)
        self.assertEqual(report['worst_case_texture_count_bound'], 1)
        self.assertEqual(report['decoded_bytes_limit'], 1536 * 1024 * 1024)
        draft = self.build(self.plan(), directory='draft-budget', command='draft')
        self.assertIsNone(draft['worst_case_decoded_bytes_bound'])
        self.assertEqual(draft['validation_samples'], 1)

    def test_segment_variants_use_original_option_names(self):
        self.source(options(), 'outfit:b&part:off')
        self.source(options(), 'outfit:a')
        plan = self.plan()
        for slot in plan['slots']:
            for segment in slot['segments']:
                self.assertEqual(segment['option_bindings'], [dict(id='outfit', name='服装')])
        report = self.build(plan)
        self.assertGreater(report['validation_samples'], 1)

    def test_segment_option_bindings_keep_shared_and_joint_options(self):
        builder = options()
        builder.m['meshes'][0]['draws'][0]['when'] = True
        builder.m['meshes'][0]['draws'][1]['when'] = {'all': [
            {'eq': ['outfit', 'b']}, {'eq': ['part', 'on']}]}
        self.source(builder)
        self.source(copy.deepcopy(builder))
        plan = self.plan()
        segments = next(slot['segments'] for slot in plan['slots'] if slot['segments'])
        self.assertEqual(segments[0]['option_bindings'], [])
        self.assertEqual(segments[1]['option_bindings'], [
            dict(id='outfit', name='服装'), dict(id='part', name='部件')])

    def test_segment_grouping_keeps_cross_component_option_link(self):
        self.source(options(), 'outfit:b&part:off')
        self.source(options(), 'outfit:a')
        # Existing cross-component source options stay namespaced and shared.
        plan = self.plan()
        report = self.build(plan)
        m, _ = bem_v1.read_package(Path(report['outputs'][0]))
        self.assertEqual(selection_plan(m, {'s0.g0': 'b', 's0.g1': 'off'})['components'][1]['operation'], 'hide')

    def test_common_gate_is_displayed_once_without_losing_conditions(self):
        builder = options()
        builder.m['component_rules'][0]['candidates'] = [dict(operation='hide', when={'eq': ['outfit', 'a']}), dict(operation='replace', mesh=0, when={'eq': ['outfit', 'b']})]
        builder.m['meshes'][0]['draws'][0]['when'] = True
        builder.m['meshes'][0]['draws'][1]['when'] = {'all': [
            {'eq': ['outfit', 'b']}, {'eq': ['part', 'on']}]}
        self.source(builder)
        self.source(copy.deepcopy(builder))
        segments = next(slot['segments'] for slot in self.plan()['slots'] if slot['segments'])
        self.assertEqual(segments[0]['option_bindings'], [])
        self.assertEqual(segments[1]['option_bindings'], [dict(id='part', name='部件')])
        self.assertEqual(segments[0]['shared_option_bindings'], [dict(id='outfit', name='服装')])

    def test_source_option_controls_multiple_parts_but_not_other_source(self):
        self.source(options(), 'outfit:b&part:off')
        self.source(options())
        report = self.build(self.plan())
        m, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, payloads, 2)
        self.assertEqual(len([g for g in m['option_groups'] if g['id'] == 's0.g0']), 1)
        mixed = selection_plan(m, {'part1': 's1'})
        changed = selection_plan(m, {'part1': 's1', 's0.g0': 'a'})
        self.assertEqual(mixed['components'][1], changed['components'][1])
        self.assertEqual(mixed['components'][1]['operation'], 'keep')
        self.assertNotEqual(mixed['components'][0]['draws'][0]['indices'],
                            changed['components'][0]['draws'][0]['indices'])

    def test_constraints_retained_and_inactive_source_constraints_ignored(self):
        builder = options()
        builder.m['selection_constraints'] = [{'any': [
            {'not': {'eq': ['outfit', 'b']}}, {'eq': ['part', 'off']}]}]
        self.source(builder)
        self.source(options())
        report = self.build(self.plan())
        m, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, payloads, 2)
        with self.assertRaisesRegex(ValueError, 'Unreachable'):
            selection_plan(m, {'s0.g0': 'b', 's0.g1': 'on'})
        self.assertEqual(selection_plan(m, {'s0.g0': 'b', 's0.g1': 'off'})['components'][1]['operation'], 'hide')
        inactive = selection_plan(m, {'part0': 's1', 'part1': 's1', 's0.g0': 'b', 's0.g1': 'on'})
        self.assertIsNone(inactive['effective']['s0.g0'])

    def test_legacy_appearances_remain_shared_and_switchable(self):
        builder = legacy()
        builder.m['appearances'][1]['components'][1]['operation'] = 'hide'
        self.source(builder, 'hidden')
        self.source(legacy())
        report = self.build(self.plan())
        m, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, payloads, 2)
        self.assertEqual([c['operation'] for c in selection_plan(m)['components']], ['hide', 'hide'])
        self.assertEqual([c['operation'] for c in selection_plan(m, {'s0.g0': 'default'})['components']], ['replace', 'keep'])

    def test_multiple_source_options_can_control_one_part(self):
        builder = options()
        builder.m['component_rules'][0]['candidates'] = [
            dict(operation='hide', when={'all': [{'eq': ['outfit', 'b']}, {'eq': ['part', 'off']}]}),
            dict(operation='replace', mesh=0, when={'not': {'all': [{'eq': ['outfit', 'b']}, {'eq': ['part', 'off']}]}})]
        self.source(builder)
        self.source(options())
        report = self.build(self.plan())
        m, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, payloads, 2)
        self.assertEqual(selection_plan(m, {'s0.g0': 'b', 's0.g1': 'on'})['components'][0]['operation'], 'replace')
        self.assertEqual(selection_plan(m, {'s0.g0': 'b', 's0.g1': 'off'})['components'][0]['operation'], 'hide')
        self.assertEqual(selection_plan(m, {'s0.g0': 'a', 's0.g1': 'off'})['components'][0]['operation'], 'replace')

    def test_explicit_resources_are_atomic_and_platform_filtered(self):
        for _ in range(2):
            self.source(self.explicit())
        plan = self.plan()
        self.assertEqual(plan['bem_minor'], 4)
        self.assertTrue(plan['atomic_items'])
        self.assertEqual(plan['atomic_items'][0]['default'], 'original')
        report = self.build(plan, {plan['atomic_items'][0]['id']: 's0'})
        m, p = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, p, 4)
        self.assertTrue(all(r['platforms'] == ['windows-x64'] for r in m['target']['resources']))
        self.cli('validate', '--package', report['outputs'][0])

    def check_different_revisions(self, factory):
        first = factory()
        first_revision = first.m['target']['revision']
        self.source(first)
        second = factory()
        second.m['target']['revision'] = 'different-revision'
        self.source(second)
        plan = self.plan()
        candidates = plan['slots'] + plan['atomic_items']
        self.assertTrue(candidates)
        report = self.build(plan, {group['id']: 's1' for group in candidates})
        output = Path(report['outputs'][0])
        manifest, payloads = bem_v1.read_package(output)
        self.assertEqual(manifest['target']['revision'], first_revision)
        check_geometry(manifest, payloads, plan['bem_minor'])
        self.cli('validate', '--package', output)

    def test_legacy_different_revisions_are_compatible(self):
        self.check_different_revisions(legacy)

    def test_explicit_different_revisions_are_compatible(self):
        self.check_different_revisions(self.explicit)

    def test_different_target_still_rejected(self):
        self.source(legacy())
        builder = legacy()
        builder.m['target']['character_id'] = 'chr_other'
        self.source(builder)
        self.plan(error='COMPOSE_TARGET_MISMATCH')

    def test_mismatched_identity_rejected(self):
        self.source(legacy())
        builder = legacy()
        builder.m['target']['revision'] = 'different-revision'
        builder.m['target']['components'][0]['original_index_count'] += 3
        self.source(builder)
        self.plan(error='COMPOSE_COMPONENT_IDENTITY_MISMATCH')

    def test_changed_source_and_tampered_plan_rejected(self):
        self.source(options())
        source = self.source(options())
        plan = self.plan()
        changed = copy.deepcopy(plan)
        changed['slots'][0]['default'] = 'original'
        self.build(changed, error='COMPOSE_STALE_PLAN')
        source.touch()
        self.build(plan, error='COMPOSE_STALE_PLAN')
        self.assertFalse((self.root / 'output').exists())

    def test_unsupported_morph_rejected_before_loss(self):
        self.source(morph())
        self.source(morph())
        self.plan(error='COMPOSE_UNSUPPORTED_FEATURE')

    def test_bad_choices_and_existing_output_leave_sources_intact(self):
        self.source(options())
        self.source(options())
        plan = self.plan()
        self.build(plan, {'missing': 'original'}, error='COMPOSE_CHOICES')
        self.assertFalse(list(self.root.glob('output.stage-*')))
        output = self.root / 'output'
        output.mkdir()
        sentinel = output / 'keep.txt'
        sentinel.write_text('keep')
        self.build(plan, error='COMPOSE_OUTPUT_EXISTS')
        self.assertEqual(sentinel.read_text(), 'keep')

    def test_duplicate_source_and_unknown_selection_rejected(self):
        self.source(options())
        self.request['sources'].append(copy.deepcopy(self.request['sources'][0]))
        self.plan(error='COMPOSE_SOURCES')
        self.request['sources'].pop()
        self.source(options(), 'missing:choice')
        self.plan(error='COMPOSE_SELECTION')

    def test_donor_indices_are_remapped_when_source_component_order_differs(self):
        self.source(legacy())
        builder = legacy()
        builder.m['target']['components'].reverse()
        for i, component in enumerate(builder.m['target']['components']):
            component['id'] = i
        for mesh in builder.m['meshes']:
            for bone in mesh['bones']:
                bone['component'] = 1 - bone['component']
            for draw in mesh['draws']:
                draw['material_component'] = 1 - draw['material_component']
        for appearance in builder.m['appearances']:
            appearance['components'].reverse()
            for i, op in enumerate(appearance['components']):
                op['target'] = i
        self.source(builder)
        report = self.build(self.plan())
        m, p = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, p, 2)
        self.assertEqual(len(m['target']['components']), 2)
        for mesh in m['meshes']:
            self.assertEqual([b['component'] for b in mesh['bones']], [0, 1])
            self.assertEqual([d['material_component'] for d in mesh['draws']], [0, 1])

    def test_resource_asset_path_and_renderer_path_mismatches_rejected(self):
        self.source(self.explicit())
        builder = self.explicit()
        builder.m['target']['resources'][0]['asset_path'] = 'assets/other/body.prefab'
        self.source(builder)
        self.plan(error='COMPOSE_RESOURCE_MISMATCH')
        builder.m['target']['resources'][0]['asset_path'] = 'assets/synthetic/body.prefab'
        builder.m['target']['components'][0]['renderer_path'] += '/Changed'
        builder.write(Path(self.request['sources'][1]['path']))
        self.plan(error='COMPOSE_COMPONENT_IDENTITY_MISMATCH')

    def test_atomic_root_mismatch_rejected(self):
        self.source(self.explicit())
        builder = self.explicit()
        builder.m['target']['resources'][1].update(name='other', asset_path='assets/synthetic/other.prefab')
        self.source(builder)
        self.plan(error='COMPOSE_ATOMIC_ROOT_MISMATCH')

    def test_cancel_and_output_id_collision(self):
        self.source(options())
        self.source(options())
        plan = self.plan()
        plan_file = self.root / 'plan.json'
        plan_file.write_text(json.dumps(plan), encoding='utf-8')
        cancel = self.root / 'cancel'
        cancel.touch()
        self.cli('build', '--plan', plan_file, '--output-dir', self.root / 'output',
                 '--cancel-file', cancel, error='COMPOSE_CANCELLED')
        self.assertFalse((self.root / 'output').exists())
        self.assertFalse(list(self.root.glob('output.stage-*')))
        self.request['project_id'] = 'TEST.PACKAGE0'
        self.plan(error='COMPOSE_PROJECT_ID')

    def test_python_cli_delegates_to_same_native_core(self):
        self.source(options())
        self.source(options())
        self.plan()
        import os
        result = subprocess.run([sys.executable, str(ROOT / 'tools/CustomModel/bem_tool.py'),
            'compose', 'plan', '--request', str(self.root / 'request.json')],
            env={**os.environ, 'BEM_COMPOSER': str(EXE)}, capture_output=True, encoding='utf-8')
        self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
        self.assertEqual(json.loads(result.stdout)['result']['estimated_components'], 2)

    def test_explicit_body_stays_granular_while_ability_roots_stay_atomic(self):
        for _ in range(2):
            builder = self.explicit()
            builder.m['target']['resources'][0].update(name='body_postmodel', asset_path='assets/synthetic/body_postmodel.prefab')
            self.source(builder)
        plan = self.plan()
        self.assertEqual(len(plan['slots']), 1)
        self.assertEqual(len(plan['atomic_items']), 1)
        self.assertEqual(len(plan['atomic_items'][0]['resources']), 2)
        self.assertEqual(plan['estimated_components'], 3)
        self.assertNotIn('projects', plan)
        report = self.build(plan, {plan['slots'][0]['id']: 's1', plan['atomic_items'][0]['id']: 'original'})
        self.assertEqual(len(report['outputs']), 1)
        manifest, _ = bem_v1.read_package(Path(report['outputs'][0]))
        self.assertEqual([r['name'] for r in manifest['target']['resources']], ['body_postmodel'])
        self.assertEqual(len(manifest['target']['components']), 1)
        self.cli('validate', '--package', report['outputs'][0])

    def test_legacy_world_ui_and_whole_ability_are_composable(self):
        sources = [self.source(legacy()), self.source(self.whole())]
        originals = [p.read_bytes() for p in sources]
        self.request.pop('version')
        plan = self.plan()
        self.assertEqual(plan['package_version'], '1')
        self.assertEqual(len(plan['slots']), 2)
        self.assertEqual(len(plan['atomic_items']), 1)
        atom = plan['atomic_items'][0]
        self.assertTrue(atom['optional'])
        self.assertEqual(atom['default'], 'original')
        report = self.build(plan, {atom['id']: 's0'})
        self.assertEqual(len(report['outputs']), 2)
        self.assertEqual(len(set(report['output_ids'])), 2)
        manifests = [bem_v1.read_package(Path(p))[0] for p in report['outputs']]
        ordinary = next(m for m in manifests if 'world_resource' in m['target'])
        self.assertEqual(ordinary['target']['world_resource'], 'world')
        self.assertEqual(ordinary['target']['ui_resource'], 'ui')
        whole = next(m for m in manifests if 'resources' in m['target'])
        self.assertEqual(len([g for g in whole['option_groups'] if not g['id'].startswith('s')]), 1)
        self.assertEqual(len(whole['option_groups']), 2)
        self.assertEqual(len(whole['target']['components']), 1)
        for path in report['outputs']:
            self.cli('validate', '--package', path)
        self.assertEqual([p.read_bytes() for p in sources], originals)

    def test_unselected_ability_is_not_emitted(self):
        self.source(legacy())
        self.source(self.whole())
        plan = self.plan()
        report = self.build(plan)
        self.assertEqual(len(report['outputs']), 1)
        self.assertEqual(report['bem_minor'], 2)
        manifest, _ = bem_v1.read_package(Path(report['outputs'][0]))
        self.assertNotIn('resources', manifest['target'])
        self.assertFalse((self.root / 'output' / 'p1').exists())

    def test_character_ability_and_weapon_are_independent_whole_items(self):
        self.source(legacy())
        self.source(self.whole())
        self.source(self.whole(weapon=True, root='weapon'))
        plan = self.plan()
        self.assertEqual(len(plan['atomic_items']), 2)
        self.assertEqual(len(plan['slots']), 2)
        weapon = next(g for g in plan['atomic_items'] if g['display_name'] == 'Weapon')
        report = self.build(plan, {weapon['id']: 's0'})
        self.assertEqual(len(report['outputs']), 2)
        manifests = [bem_v1.read_package(Path(p))[0] for p in report['outputs']]
        self.assertTrue(any(m['target'].get('kind') == 'weapon' for m in manifests))
        self.assertFalse(any(r['name'] == 'ultimate' for m in manifests for r in m['target'].get('resources', [])))
        for path in report['outputs']:
            self.cli('validate', '--package', path)

    def test_same_ability_sources_remain_one_atomic_choice(self):
        self.source(legacy())
        self.source(self.whole())
        self.source(self.whole())
        plan = self.plan()
        self.assertEqual(len(plan['atomic_items']), 1)
        atom = plan['atomic_items'][0]
        self.assertEqual([c['id'] for c in atom['candidates']], ['s0', 's1', 'original'])
        report = self.build(plan, {atom['id']: 's1'})
        self.assertEqual(len(report['outputs']), 2)

    def test_project_invalid_choices_and_stale_plan_leave_no_output(self):
        self.source(legacy())
        source = self.source(self.whole())
        plan = self.plan()
        self.build(plan, {'unknown': 's0'}, error='COMPOSE_CHOICES')
        self.build(plan, {plan['atomic_items'][0]['id']: 'missing'}, error='COMPOSE_CHOICES')
        source.touch()
        self.build(plan, error='COMPOSE_STALE_PLAN')
        self.assertFalse((self.root / 'output').exists())
        self.assertFalse(list(self.root.glob('output.stage-*')))

    def test_selected_companion_failure_rolls_back_completed_ordinary_output(self):
        import struct
        self.source(legacy())
        builder = self.whole()
        indices = builder.m['meshes'][0]['draws'][0]['indices']
        builder.payloads[indices] = struct.pack('<3H', 9, 1, 2)
        source = self.source(builder)
        original = source.read_bytes()
        plan = self.plan()
        self.build(plan, {plan['atomic_items'][0]['id']: 's0'}, error='COMPOSE_VALIDATION_FAILED')
        self.assertFalse((self.root / 'output').exists())
        self.assertFalse(list(self.root.glob('output.stage-*')))
        self.assertEqual(source.read_bytes(), original)

    def test_only_whole_items_require_an_explicit_selection(self):
        self.source(self.whole())
        self.source(self.whole())
        self.build(self.plan(), error='COMPOSE_CHOICES')
        self.assertFalse((self.root / 'output').exists())

    def test_two_distinct_abilities_are_independently_optional(self):
        self.source(legacy())
        self.source(self.whole(root='ultimate_one'))
        self.source(self.whole(root='ability_two'))
        plan = self.plan()
        self.assertEqual(len(plan['atomic_items']), 2)
        report = self.build(plan, {plan['atomic_items'][0]['id']: 's0'})
        self.assertEqual(len(report['outputs']), 2)
        self.assertFalse((self.root / 'output' / 'p2').exists())

    def test_one_explicit_source_option_stays_shared_across_body_and_ability(self):
        builder = self.explicit()
        for i, root in enumerate(builder.m['target']['resources']):
            root['name'] = ['body_postmodel', 'ability', 'mirror'][i]
            root['asset_path'] = f"assets/synthetic/{root['name']}.prefab"
        builder.m['option_groups'] = [dict(id='style', name='联动', default='on',
            choices=[dict(id='on', name='显示'), dict(id='off', name='隐藏')])]
        for rule in builder.m['component_rules']:
            original = rule['candidates'][0]
            original['when'] = {'eq': ['style', 'on']}
            rule['candidates'] = [original, dict(operation='hide', when={'eq': ['style', 'off']})]
        self.source(builder)
        self.source(copy.deepcopy(builder))
        plan = self.plan()
        self.assertNotIn('projects', plan)
        atom = plan['atomic_items'][0]
        report = self.build(plan, {atom['id']: 's0'})
        self.assertEqual(len(report['outputs']), 1)
        m, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, payloads, 4)
        off = selection_plan(m, {'s0.g0': 'off'}, minor=4)
        self.assertTrue(all(c['operation'] == 'hide' for c in off['components']))
        self.assertEqual(len([g for g in m['option_groups'] if g['id'] == 's0.g0']), 1)

    def test_generated_composition_can_be_composed_again_without_flattening(self):
        self.source(options())
        self.source(options())
        report = self.build(self.plan())
        self.request['project_id'] = 'test.recomposed'
        self.request['sources'] = [dict(path=report['outputs'][0], selection='', parameters=''), self.request['sources'][0]]
        report = self.build(self.plan(), directory='recomposed')
        m, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, payloads, 2)
        groups = {g['name'].split(' / ')[-1]: g for g in m['option_groups'] if g['id'].startswith('s0.')}
        self.assertIn('服装', groups)
        self.assertTrue(any(g['choices'] == [dict(id='a', name='A'), dict(id='b', name='B')] for g in m['option_groups']))

    def test_corrupt_nondefault_variant_in_nondefault_source_is_rejected(self):
        import struct
        self.source(legacy(), 'hidden')
        builder = legacy()
        builder.payloads[builder.m['meshes'][0]['indices']] = struct.pack('<6H', 9, 1, 2, 2, 1, 0)
        self.source(builder, 'hidden')
        self.build(self.plan(), error='COMPOSE_VALIDATION_FAILED')
        self.assertFalse((self.root / 'output').exists())

    def test_draft_only_checks_current_selection_but_publish_checks_other_variants(self):
        import struct
        self.source(legacy(), 'hidden')
        builder = legacy()
        builder.payloads[builder.m['meshes'][0]['indices']] = struct.pack('<6H', 9, 1, 2, 2, 1, 0)
        self.source(builder, 'hidden')
        plan = self.plan()
        defaults = {group['id']: 'original' for group in plan['slots']}
        report = self.build(plan, defaults, directory='draft', command='draft')
        self.assertTrue(report['preview_only'])
        self.assertEqual(report['validation_samples'], 1)
        self.build(plan, defaults, error='COMPOSE_VALIDATION_FAILED')
        defaults[plan['slots'][0]['id']] = 's1'
        defaults['s1.g0'] = 'default'
        self.build(plan, defaults, error='COMPOSE_VALIDATION_FAILED', directory='invalid-draft', command='draft')
        self.assertFalse((self.root / 'invalid-draft').exists())

    def test_draft_texture_cache_survives_new_snapshot_but_publish_has_no_cache_provenance(self):
        self.source(legacy())
        self.source(legacy())
        plan = self.plan()
        caps = json.dumps(dict(textureCache=str(self.root / 'cache'), compositionDraft=True))
        reports = []
        for i in range(2):
            report = self.build(plan, directory=f'draft{i}', command='draft')
            reports.append(self.cli('preview', '--package', report['outputs'][0],
                '--output-dir', self.root / f'scene{i}', '--gpu', caps))
        self.assertEqual(reports[0]['texture_cache_hits'], 0)
        self.assertEqual(reports[1]['texture_cache_hits'], 2)
        final = self.build(plan)
        self.assertFalse(final['preview_only'])
        manifest, _ = bem_v1.read_package(Path(final['outputs'][0]))
        self.assertTrue(all('preview_source' not in texture for texture in manifest['textures']))

    def test_constraint_coupled_choices_keep_all_reachable_variants(self):
        builder = options()
        builder.m['option_groups'][1].pop('available_when')
        builder.m['selection_constraints'] = [{'any': [
            {'all': [{'eq': ['outfit', 'a']}, {'eq': ['part', 'on']}]},
            {'all': [{'eq': ['outfit', 'b']}, {'eq': ['part', 'off']}]}]}]
        self.source(builder)
        self.source(options())
        report = self.build(self.plan())
        m, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(m, payloads, 2)
        self.assertEqual(selection_plan(m, {'s0.g0': 'b', 's0.g1': 'off'})['components'][1]['operation'], 'hide')
        self.assertGreater(report['validation_samples'], 1)

    def test_combined_option_group_budget_fails_without_silent_flattening(self):
        builder = options()
        for i in range(31):
            builder.m['option_groups'].append(dict(id=f'extra{i}', name=f'额外 {i}',
                default='a', choices=[dict(id='a', name='A')]))
        self.source(builder)
        self.source(copy.deepcopy(builder))
        self.build(self.plan(), error='COMPOSE_TOO_MANY_GROUPS')
        self.assertFalse((self.root / 'output').exists())

    def test_higher_header_without_new_features_does_not_block_older_sources(self):
        import struct
        source = self.source(options())
        raw = bytearray(source.read_bytes())
        struct.pack_into('<H', raw, 10, 5)
        source.write_bytes(raw)
        self.source(legacy())
        plan = self.plan()
        self.assertEqual(plan['bem_minor'], 2)
        report = self.build(plan)
        self.cli('validate', '--package', report['outputs'][0])

    def test_source_display_versions_are_derived_without_user_input(self):
        first, second = legacy(), legacy()
        first.m['version'], second.m['version'] = '2.1', '3.0'
        self.source(first)
        self.source(second)
        self.request.pop('version')
        plan = self.plan()
        self.assertEqual(plan['package_version'], '2.1 + 3.0')
        report = self.build(plan)
        manifest, _ = bem_v1.read_package(Path(report['outputs'][0]))
        self.assertEqual(manifest['version'], '2.1 + 3.0')

    def test_keep_material_texture_candidate_is_preserved(self):
        builder = options()
        builder.m['required_capabilities'].append('keep-material-textures')
        for op in builder.m['component_rules'][1]['candidates']:
            if op['operation'] == 'keep':
                op['material_overrides'] = [dict(material_slot=0, material_name='material1', textures=[1])]
        self.source(builder)
        self.source(options())
        report = self.build(self.plan())
        manifest, payloads = bem_v1.read_package(Path(report['outputs'][0]))
        check_geometry(manifest, payloads, 2)
        materials = selection_plan(manifest)['components'][1]['material_overrides']
        self.assertEqual(materials[0]['material_name'], 'material1')
        texture = manifest['textures'][materials[0]['textures'][0]]
        self.assertEqual(payloads[texture['payload']], bytes([2]) * 8)

    def test_corrupt_selected_indices_cannot_publish_a_partial_package(self):
        import struct
        builder = legacy()
        builder.payloads[builder.m['meshes'][0]['indices']] = struct.pack('<6H', 9, 1, 2, 2, 1, 0)
        source = self.source(builder)
        original = source.read_bytes()
        self.source(legacy())
        self.build(self.plan(), error='COMPOSE_VALIDATION_FAILED')
        self.assertFalse((self.root / 'output').exists())
        self.assertFalse(list(self.root.glob('output.stage-*')))
        self.assertEqual(source.read_bytes(), original)

if __name__ == '__main__':
    unittest.main()
