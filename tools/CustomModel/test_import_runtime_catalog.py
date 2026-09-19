import copy
import unittest

from import_runtime_catalog import build_role, validate_observation


class CatalogImportTests(unittest.TestCase):
    def setUp(self):
        self.character='chr_fixture'
        world=self.character+'_postmodel'; ui=self.character+'_uimodel'
        # Non-symmetric affine matrix detects a missed/double transpose.
        self.pose=[1,0,0,2,0,1,0,3,0,0,1,4,0,0,0,1]
        self.mesh=dict(vertex_count=3,bindposes=[self.pose],submeshes=[{}])
        def native(root):
            return dict(id=root+'/renderer',resource_root=root,path=root+'/lod0/body',
                mesh_id='mesh:1',mesh_name='S_actor_fixture_body_lod0',original_index_count=3,
                bones=[dict(name='root',path=root+'/root')],
                materials=[dict(id='material:1',name='body',textures=[])],offline_references_complete=True)
        self.world=native(world);self.ui=native(ui)
        self.observed={k:copy.deepcopy(self.world[k]) for k in ('resource_root','path','mesh_name','original_index_count','bones')}
        self.observed.update(vertex_count=3,materials=[dict(name='body')],
            bindposes=[[self.pose[r*4+c] for c in range(4) for r in range(4)]],
            strides=[16,8,12],attributes=[[0,0,3,0],[1,0,1,0],[4,0,2,1],[12,4,4,2],[13,6,4,2]])
        self.db=dict(source={'snapshot':{'manifest_version':'snapshot'}},
            renderers=[self.world,self.ui],meshes={'mesh:1':self.mesh})
        self.observations={(world,self.world['path']):dict(row=self.observed,run='run',file='run.jsonl',process_id=123)}

    def build(self,old=None):
        return build_role(self.db,self.observations,self.character,'Fixture','snapshot',old)

    def test_equivalent_root_reuses_observation_with_provenance(self):
        c=self.build()['components']['0']
        self.assertTrue(c['layout_verified'] and c['source_layout_supported'])
        self.assertEqual(c['evidence']['observations'][self.ui['resource_root']]['kind'],'equivalent-reuse')
        self.assertEqual(c['evidence']['observations'][self.world['resource_root']]['kind'],'direct')

    def test_matrix_orientation_and_actual_change(self):
        validate_observation(self.world,self.mesh,self.observed)
        for bad in (self.pose,[x+.1 for x in self.observed['bindposes'][0]]):
            self.observed['bindposes']=[bad]
            with self.assertRaisesRegex(ValueError,'bindposes differ'):
                validate_observation(self.world,self.mesh,self.observed)

    def test_world_ui_difference_is_not_reused(self):
        for field in ('bones','materials'):
            with self.subTest(field=field):
                before=copy.deepcopy(self.ui[field])
                self.ui[field][0]['path' if field=='bones' else 'id']='different'
                with self.assertRaisesRegex(ValueError,'no reusable'):self.build()
                self.ui[field]=before

    def test_reviewed_source_mapping_preserved_only_for_matching_contract(self):
        old=self.build();old['entries']={'12345678':0};old['textures']={'87654321':{'name':'texture'}}
        self.assertEqual(self.build(old)['entries'],old['entries'])
        self.assertEqual(self.build(old)['textures'],old['textures'])
        old['components']['0']['bone_names']=['other']
        with self.assertRaisesRegex(ValueError,'native contract changed'):self.build(old)

    def test_unknown_layout_is_recorded_without_automatic_permission(self):
        self.observed['attributes'][1][2]=3
        c=self.build()['components']['0']
        self.assertTrue(c['layout_verified'])
        self.assertFalse(c['source_layout_supported'])

    def test_snapshot_mismatch_rejected(self):
        self.db['source']['snapshot']['manifest_version']='other'
        with self.assertRaisesRegex(ValueError,'snapshot differs'):self.build()


if __name__=='__main__':unittest.main()
