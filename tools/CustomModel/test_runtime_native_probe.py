import copy
import unittest
from parse_native_models import parse
from runtime_native_probe import request, collect
from test_native_models import fixture


class RuntimeProbeTests(unittest.TestCase):
    def setUp(self):
        self.db = parse(fixture())
        world = self.db['renderers'][0]
        other = copy.deepcopy(world); other['id'] = 'ui:7'
        other['resource_root'] = 'ui'; other['path'] = 'ui/mesh'
        other['bones'][0]['path'] = 'ui/bone'
        self.db['renderers'].append(other)
        self.db['source']['snapshot'] = {'manifest_version': 'version', 'perforce_cl': ''}
        self.preparation = {'source_snapshot': self.db['source']['snapshot'],
                            'components': [{'world_renderer': world['id'], 'ui_renderer': other['id']}]}
        _, self.metadata = request(self.db, self.preparation, 'fixture')
        self.records = []
        for row in (world, other):
            observation = {key: row[key] for key in ('id', 'path', 'resource_root', 'mesh_name', 'original_index_count')}
            observation.update(complete=True, strides=[16, 8, 12],
                               attributes=[[0, 0, 3, 0], [1, 3, 4, 0], [4, 0, 2, 1], [12, 4, 4, 2], [13, 6, 4, 2]],
                               bones=[{'name': b['name'], 'path': b['path']} for b in row['bones']],
                               materials=[{'name': m['name'], 'shader': 'shader'} for m in row['materials']])
            self.records.append(dict(schema=1, run='fixture', manifest_version='version', perforce_cl='',
                                     process_id=1, complete=True, resource_root=row['resource_root'], renderers=[observation]))

    def test_armed_request_is_exact_and_import_preserves_layout(self):
        text, metadata = request(self.db, self.preparation, 'fixture')
        self.assertIn('character\tcharacter/mesh\tmesh\t3\tscene:7', text)
        self.assertEqual(len(collect(self.db, metadata, self.records)['renderers']), 2)
        self.assertEqual(self.db['renderers'][0]['runtime_layout']['strides'], [16, 8, 12])
        self.assertFalse(self.db['renderers'][0]['conversion_ready'])

    def test_partial_stale_and_mixed_process_rejected(self):
        with self.assertRaisesRegex(ValueError, 'incomplete'):
            collect(self.db, self.metadata, self.records[:1])
        self.records[1]['process_id'] = 2
        with self.assertRaisesRegex(ValueError, 'one game process'):
            collect(self.db, self.metadata, self.records)
        self.records[1]['manifest_version'] = 'old'
        with self.assertRaisesRegex(ValueError, 'provenance'):
            collect(self.db, self.metadata, self.records)

    def test_changed_bone_material_and_duplicate_rejected(self):
        records = copy.deepcopy(self.records); records[0]['renderers'][0]['bones'][0]['name'] = 'wrong'
        with self.assertRaisesRegex(ValueError, 'bone identity'):
            collect(self.db, self.metadata, records)
        records = copy.deepcopy(self.records); records[0]['renderers'][0]['materials'][0]['name'] = 'wrong'
        with self.assertRaisesRegex(ValueError, 'material slot'):
            collect(self.db, self.metadata, records)
        with self.assertRaisesRegex(ValueError, 'duplicate'):
            collect(self.db, self.metadata, self.records+self.records)

    def test_request_limits_and_snapshot(self):
        with self.assertRaisesRegex(ValueError, 'run ID'):
            request(self.db, self.preparation, '../escape')
        self.preparation['source_snapshot'] = {}
        with self.assertRaisesRegex(ValueError, 'snapshot'):
            request(self.db, self.preparation, 'fixture')


if __name__ == '__main__':
    unittest.main()
