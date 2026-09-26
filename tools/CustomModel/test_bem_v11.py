import copy
import json
import os
from pathlib import Path
import struct
import subprocess
import tempfile
import unittest
from unittest import mock

import bem_v1 as bem
import bem_v11
from bem_tool import check_geometry
from test_bem_v1 import fixture as v10_fixture


def fixture():
    builder = v10_fixture()
    manifest = builder.m
    del manifest['appearances']
    del manifest['default_appearance_id']
    manifest['required_capabilities'] = ['native-materials', 'palette-u8',
                                         'indices-u32', 'composable-options']
    manifest['option_groups'] = [
        dict(id='outfit', name='服装', default='a',
             choices=[dict(id='a', name='A'), dict(id='b', name='B')]),
        dict(id='part', name='部件', default='on', available_when={'eq': ['outfit', 'b']},
             choices=[dict(id='on', name='显示'), dict(id='off', name='隐藏')]),
    ]
    manifest['component_rules'] = [
        dict(target=0, candidates=[dict(operation='replace', mesh=0)]),
        dict(target=1, candidates=[
            dict(operation='hide', when={'all': [{'eq': ['outfit', 'b']},
                                                  {'eq': ['part', 'off']}]}),
            dict(operation='keep', when={'not': {'all': [{'eq': ['outfit', 'b']},
                                                          {'eq': ['part', 'off']}]}}),
        ]),
    ]
    mesh = manifest['meshes'][0]
    del mesh['indices']
    del mesh['index_count']
    for index, draw in enumerate(mesh['draws']):
        del draw['start']
        draw['indices'] = builder.payload(struct.pack('<3H', *(0, 1, 2) if index == 0 else (2, 1, 0)))
        draw['when'] = {'eq': ['outfit', 'a' if index == 0 else 'b']}
    return builder


class Bem11Tests(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.TemporaryDirectory()
        self.path = Path(self.tmp.name) / 'test.bem'
        self.builder = fixture()
        self.builder.write(self.path)

    def tearDown(self):
        self.tmp.cleanup()

    def native(self, expected):
        validator = os.environ.get('BEM_VALIDATOR')
        if validator:
            result = subprocess.run([validator, str(self.path)], capture_output=True)
            self.assertEqual(result.returncode == 0, expected, result.stdout + result.stderr)

    def test_roundtrip_and_selected_only(self):
        manifest, payloads = bem.read_package(self.path)
        self.assertEqual(manifest, self.builder.m)
        self.assertEqual(payloads, self.builder.payloads)
        check_geometry(manifest, payloads)
        self.native(True)
        accessed = []
        _, a, pa = bem_v11.read_selected_payloads(self.path, {'outfit': 'a', 'part': 'off'}, accessed.append)
        first_access = set(accessed)
        accessed.clear()
        _, b, pb = bem_v11.read_selected_payloads(self.path, {'outfit': 'b', 'part': 'off'}, accessed.append)
        second_access = set(accessed)
        accessed.clear()
        _, a2, pa2 = bem_v11.read_selected_payloads(self.path, {'outfit': 'a', 'part': 'off'}, accessed.append)
        self.assertEqual(a, a2)
        self.assertEqual(pa, pa2)
        self.assertEqual(first_access, set(accessed))
        self.assertNotIn(manifest['meshes'][0]['draws'][1]['indices'], first_access)
        self.assertNotIn(manifest['textures'][1]['payload'], first_access)
        self.assertNotIn(manifest['meshes'][0]['draws'][0]['indices'], second_access)
        self.assertNotIn(manifest['textures'][0]['payload'], second_access)
        self.assertEqual(a['effective']['part'], None)
        self.assertEqual(b['effective']['part'], 'off')
        self.assertEqual(b['components'][1]['operation'], 'hide')
        self.assertEqual(set(pa), first_access)
        self.assertEqual(set(pb), second_access)
        validator=os.environ.get('BEM_VALIDATOR')
        if validator:
            native=subprocess.run([validator,str(self.path)],capture_output=True,text=True)
            self.assertEqual(native.returncode,0,native.stdout+native.stderr)
            lines={line.split(': components=',1)[0]:line for line in native.stdout.splitlines() if ': components=' in line}
            native_a={int(i) for i in lines['outfit:a&part:off'].split(' payloads=',1)[1].split(',')}
            native_b={int(i) for i in lines['outfit:b&part:off'].split(' payloads=',1)[1].split(',')}
            self.assertEqual(native_a,first_access)
            self.assertEqual(native_b,second_access)

    def test_keep_material_textures_selected_and_capability_required(self):
        m = copy.deepcopy(self.builder.m)
        m['required_capabilities'].append('keep-material-textures')
        m['textures'].append(dict(width=4, height=4, mips=1, format=10, srgb=False,
                                  original_name='kept-original',
                                  payload=self.builder.payload(bytes([3]) * 8)))
        keep = next(c for c in m['component_rules'][1]['candidates'] if c['operation'] == 'keep')
        keep['material_overrides'] = [dict(material_slot=0, material_name='material1', textures=[2])]
        bem.write_package(self.path, m, self.builder.payloads)
        check_geometry(m, self.builder.payloads)
        self.native(True)
        _, kept, kept_payloads = bem_v11.read_selected_payloads(self.path, {'outfit': 'a'})
        _, hidden, hidden_payloads = bem_v11.read_selected_payloads(self.path,
                                                                    {'outfit': 'b', 'part': 'off'})
        self.assertIn(2, kept['textures'])
        self.assertIn(m['textures'][2]['payload'], kept_payloads)
        self.assertEqual(kept['components'][1]['material_overrides'], keep['material_overrides'])
        self.assertNotIn(2, hidden['textures'])
        self.assertNotIn(m['textures'][2]['payload'], hidden_payloads)
        broken = copy.deepcopy(m)
        broken['required_capabilities'].remove('keep-material-textures')
        with self.assertRaisesRegex(ValueError, 'capability mismatch'):
            bem.validate_manifest(broken, len(self.builder.payloads))
        broken = copy.deepcopy(m)
        broken['component_rules'][1]['candidates'][1]['material_overrides'][0]['material_name'] = 'wrong'
        with self.assertRaisesRegex(ValueError, 'Keep material identity'):
            bem.validate_manifest(broken, len(self.builder.payloads))

    def test_keep_only_texture_package(self):
        m = copy.deepcopy(self.builder.m)
        m['required_capabilities'].append('keep-material-textures')
        m['meshes'] = []
        m['component_rules'] = [dict(target=0, candidates=[dict(operation='keep',
            material_overrides=[dict(material_slot=0, material_name='material0', textures=[0])])]),
            dict(target=1, candidates=[dict(operation='keep')])]
        bem.write_package(self.path, m, self.builder.payloads)
        self.native(True)
        _, plan, selected = bem_v11.read_selected_payloads(self.path)
        self.assertEqual(plan['textures'], [0])
        self.assertEqual(set(selected), {m['textures'][0]['payload']})

    def test_invalid_conditions_and_ambiguous_operation(self):
        for change in (
            lambda m: m['component_rules'][1]['candidates'][1].update(when=True),
            lambda m: m['option_groups'][0].update(available_when={'eq': ['part', 'on']}),
            lambda m: m['meshes'][0]['draws'][0].update(indices=9999),
        ):
            m = copy.deepcopy(self.builder.m)
            change(m)
            with self.assertRaises((ValueError, KeyError, IndexError)):
                bem.write_package(self.path, m, self.builder.payloads)

    def test_v10_reader_stays_compatible(self):
        legacy = v10_fixture()
        legacy.write(self.path)
        manifest, _ = bem.read_package(self.path, decode=False)
        self.assertNotIn('option_groups', manifest)
        self.native(True)

    def test_seven_independent_keys_make_128_distinct_selections(self):
        builder=fixture();manifest=builder.m
        manifest['option_groups']=[dict(id=f'key{i}',name=f'局部 {i}',default='off',
                                        choices=[dict(id='off',name='关闭'),dict(id='on',name='开启')])
                                   for i in range(7)]
        manifest['component_rules']=[dict(target=0,candidates=[dict(operation='replace',mesh=0)]),
                                     dict(target=1,candidates=[dict(operation='keep')])]
        old=manifest['meshes'][0]['draws'][0]
        index_payload=old['indices']
        draws=[]
        for i in range(8):
            while len(manifest['textures'])<=i:
                index=len(manifest['textures'])
                manifest['textures'].append(dict(width=4,height=4,mips=1,format=10,srgb=False,
                    original_name=f'texture{index}',payload=builder.payload(bytes([index+1])*8)))
            draws.append(dict(indices=index_payload,count=3,material_component=0,material_slot=0,
                              material_name='material0',textures=[i],
                              **({'when':{'eq':[f'key{i-1}','on']}} if i else {})))
        manifest['meshes'][0]['draws']=draws
        signatures={tuple(plan['textures']) for plan in bem_v11.reachable_plans(manifest)}
        self.assertEqual(len(signatures),128)
        check_geometry(manifest,builder.payloads)
        builder.write(self.path)
        self.native(True)

    def test_thirty_three_independent_keys_validate_without_enumeration(self):
        m = copy.deepcopy(self.builder.m)
        m['option_groups'] = [dict(id=f'key{i}', name=f'局部 {i}', default='off',
                                   choices=[dict(id='off', name='关闭'), dict(id='on', name='开启')])
                              for i in range(33)]
        m['component_rules'] = [dict(target=0, candidates=[dict(operation='replace', mesh=0)]),
                                dict(target=1, candidates=[dict(operation='keep')])]
        for index, draw in enumerate(m['meshes'][0]['draws']):
            draw['when'] = {'eq': [f'key{index}', 'on']}
        # The first draw must always be present; the other 32 controls may vary.
        m['meshes'][0]['draws'][0]['when'] = True
        for i in range(2, 33):
            m['meshes'][0]['draws'].append({**copy.deepcopy(m['meshes'][0]['draws'][1]),
                                          'when': {'eq': [f'key{i}', 'on']}})
        bem.validate_manifest(m, len(self.builder.payloads))
        summary = bem_v11.check_geometry(m, self.builder.payloads)
        self.assertEqual(summary['saved_assignments'], 1 << 33)
        self.assertEqual(summary['effective_selections'], 1 << 33)
        self.assertEqual(len(bem_v11.selection_plan(m, {'key32': 'on'})['components'][0]['draws']), 2)

    def test_symbolic_rejects_rare_ambiguity_and_budget_violation(self):
        m = copy.deepcopy(self.builder.m)
        m['option_groups'] = [dict(id=f'key{i}', name=f'局部 {i}', default='off',
                                   choices=[dict(id='off', name='关闭'), dict(id='on', name='开启')])
                              for i in range(33)]
        m['meshes'][0]['draws'][0]['when'] = True
        m['meshes'][0]['draws'][1]['when'] = False
        rare = {'all': [{'eq': [f'key{i}', 'on']} for i in range(32)]}
        m['component_rules'][1]['candidates'] = [dict(operation='keep'),
                                                  dict(operation='hide', when=rare)]
        with self.assertRaisesRegex(ValueError, 'exactly one'):
            bem.validate_manifest(m, len(self.builder.payloads))
        m['component_rules'][1]['candidates'] = [dict(operation='keep')]
        m['meshes'][0]['draws'][0]['when'] = True
        m['meshes'][0]['draws'][1]['when'] = rare
        m['meshes'][0]['draws'][0]['count'] = 9000000
        m['meshes'][0]['draws'][1]['count'] = 9000000
        with self.assertRaisesRegex(ValueError, 'Selected index limit'):
            bem.validate_manifest(m, len(self.builder.payloads))

    def test_symbolic_resident_budget_and_texture_union(self):
        m = copy.deepcopy(self.builder.m)
        m['option_groups'] = [dict(id=f'key{i}', name=f'局部 {i}', default='off',
                                   choices=[dict(id='off', name='关闭'), dict(id='on', name='开启')])
                              for i in range(33)]
        m['component_rules'][1]['candidates'] = [dict(operation='keep')]
        mesh = m['meshes'][0]
        mesh['draws'][0]['when'] = True
        mesh['draws'][1]['when'] = {'eq': ['key32', 'on']}
        bem.validate_manifest(m, len(self.builder.payloads))
        default = bem_v11.selection_plan(m)
        extra = bem_v11.selection_plan(m, {'key32': 'on'})
        self.assertGreater(len(extra['payloads']), len(default['payloads']))
        # Lower the limit only for this test, so no large allocation is required.
        with mock.patch.object(bem_v11, 'MAX_SELECTED_RESIDENT_V12', 50):
            with self.assertRaisesRegex(ValueError, 'Selected state exceeds'):
                bem_v11.check_geometry(m, self.builder.payloads)

    def test_exact_effective_count_collapses_unavailable_choices(self):
        summary = bem_v11.analyze_selection_space(self.builder.m, self.builder.payloads)
        self.assertEqual(summary['saved_assignments'], 4)
        self.assertEqual(summary['effective_selections'], 3)
        self.assertEqual(summary['max_selected_draws'], 1)
        self.assertEqual(summary['max_selected_indices'], 3)

    def test_symbolic_summary_agrees_with_exhaustive_small_domain(self):
        m = copy.deepcopy(self.builder.m)
        m['option_groups'].append(dict(id='extra', name='额外', default='off',
                                       choices=[dict(id='off', name='关闭'),
                                                dict(id='on', name='开启')]))
        m['selection_constraints'] = [{'not': {'all': [
            {'eq': ['outfit', 'b']}, {'eq': ['extra', 'on']}]}}]
        bem.validate_manifest(m, len(self.builder.payloads))
        summary = bem_v11.analyze_selection_space(m, self.builder.payloads)
        plans = list(bem_v11.reachable_plans(m))
        self.assertEqual(summary['saved_assignments'], len(plans))
        self.assertEqual(summary['effective_selections'], len({
            tuple(plan['effective'].items()) for plan in plans}))
        self.assertEqual(summary['max_selected_textures'], max(len(p['textures']) for p in plans))
        self.assertEqual(summary['max_selected_indices'], max(
            max((op.get('index_count', 0) for op in p['components']), default=0)
            for p in plans))
        self.assertEqual(summary['max_selected_decoded_bytes'], max(
            sum(len(self.builder.payloads[index]) for index in p['payloads'])
            for p in plans))

    def test_creator_pack_validate_and_unpack(self):
        project=Path(self.tmp.name)/'project';project.mkdir()
        files=[]
        for index,data in enumerate(self.builder.payloads):
            name=f'{index:04d}.bin';(project/name).write_bytes(data);files.append(name)
        project_file=project/'project.json'
        project_file.write_text(json.dumps({'manifest':self.builder.m,'payload_files':files},ensure_ascii=False),encoding='utf-8')
        cli=Path(__file__).with_name('bem_tool.py')
        for command in (["pack",str(project_file),"-o",str(self.path)],
                        ["validate",str(self.path)],
                        ["unpack",str(self.path),"-o",str(Path(self.tmp.name)/'unpacked')]):
            result=subprocess.run([os.sys.executable,str(cli),*command],capture_output=True)
            self.assertEqual(result.returncode,0,result.stdout+result.stderr)
            report=json.loads(result.stdout)
            self.assertEqual(report['format_version'],'1.1')
            if command[0] == 'validate':
                self.assertEqual(report['selection_space']['effective_selections'], 3)

    def test_android_identity_rewrite_keeps_all_candidate_resources(self):
        validator=os.environ.get('BEM_VALIDATOR')
        if not validator:
            self.skipTest('Native validator path not configured')
        rewritten=Path(self.tmp.name)/'android-installed.bem'
        result=subprocess.run([validator,str(self.path),str(rewritten)],capture_output=True)
        self.assertEqual(result.returncode,0,result.stdout+result.stderr)
        manifest,payloads=bem.read_package(rewritten)
        self.assertEqual(manifest['option_groups'],self.builder.m['option_groups'])
        self.assertEqual(len(manifest['meshes'][0]['draws']),2)
        check_geometry(manifest,payloads)
        self.native(True)
        result=subprocess.run([validator,str(rewritten)],capture_output=True)
        self.assertEqual(result.returncode,0,result.stdout+result.stderr)


if __name__ == '__main__':
    unittest.main()
