import copy
import tempfile
import unittest
from pathlib import Path
from runtime_sweep import make_request, summarize, summarize_campaign, records


class SweepTests(unittest.TestCase):
    def setUp(self):
        manifest={'Version':'snapshot','Assets':[
            {'path':'assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0004_pelica_uimodel.prefab'},
            {'path':'assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/chr_0004_pelica_postmodel.prefab'},
            {'path':'assets/beyond/dynamicassets/gameplay/actors/postmodels/npc/chr_0004_pelica_postmodel.prefab'}]}
        self.text,self.meta=make_request(manifest,'sweep-test')
        self.root='chr_0004_pelica_uimodel'
        self.row=dict(run='sweep-test',manifest_version='snapshot',perforce_cl='',mode='sweep',process_id=123,
            resource_root=self.root,complete=True,renderer_count=1,
            renderers=[dict(resource_root=self.root,path=self.root+'/mesh',mesh_name='mesh',complete=True)])

    def test_manifest_roots_exclude_npc_and_no_offline_renderer_needed(self):
        self.assertEqual(len(self.meta['resources']),2)
        self.assertTrue(self.text.startswith('BE_NATIVE_PROBE_SWEEP_V1\n'))
        with self.assertRaises(ValueError):make_request({'Version':'v','Assets':[]},'empty')

    def test_progress_keeps_missing_variant_and_deduplicates(self):
        s=summarize(self.meta,[self.row,self.row])
        self.assertEqual(s['complete_resources'],1)
        self.assertEqual(s['characters_observed'],['chr_0004_pelica'])
        self.assertEqual(s['unobserved_resources'],['chr_0004_pelica_postmodel'])

    def test_supplement_selects_only_requested_character(self):
        manifest={'Version':'v','Assets':[
            {'path':'assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0004_pelica_uimodel.prefab'},
            {'path':'assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0003_endminf_uimodel.prefab'}]}
        _,meta=make_request(manifest,'supplement',['chr_0004_pelica'])
        self.assertEqual(list(meta['resources']),['chr_0004_pelica_uimodel'])
        with self.assertRaisesRegex(ValueError,'missing'):
            make_request(manifest,'supplement',['chr_0004_pelica','chr_0017_yvonne'])

    def test_partial_retry_and_renderer_count_not_mislabeled_complete(self):
        partial=copy.deepcopy(self.row);partial['complete']=False;partial['renderers'][0]['complete']=False
        self.assertEqual(summarize(self.meta,[partial])['incomplete_resources'],[self.root])
        self.assertEqual(summarize(self.meta,[partial,self.row])['complete_resources'],1)
        missing=copy.deepcopy(self.row);missing['renderer_count']=2
        self.assertEqual(summarize(self.meta,[missing])['complete_resources'],0)

    def test_cross_process_and_stale_snapshot_rejected(self):
        for key,value in [('process_id',234),('manifest_version','stale')]:
            row=copy.deepcopy(self.row);row[key]=value
            with self.assertRaises(ValueError):summarize(self.meta,[self.row,row])

    def test_persistent_request_and_separate_process_aggregation(self):
        manifest={'Version':'v','Assets':[{'path':'assets/beyond/dynamicassets/gameplay/prefabs/uimodels/chr_0004_pelica_uimodel.prefab'}]}
        text,meta=make_request(manifest,'persistent',persistent=True)
        self.assertTrue(text.startswith('BE_NATIVE_PROBE_SWEEP_PERSIST_V1\n'))
        self.assertTrue(meta['persistent'])
        first=copy.deepcopy(self.row)
        first['renderers'][0].update(original_index_count=3,vertex_count=3,submesh_count=1,strides=[12],
            attributes=[[0,0,3,0]],bones=[],bindposes=[],materials=[{'name':'m','shader':'s','textures':[{'property_id':1}]}])
        second=copy.deepcopy(first);second['process_id']=234
        second['renderers'][0]['materials'][0]['textures'][0]['property_id']=9
        result=summarize_campaign(self.meta,[('one.jsonl',[first]),('two.jsonl',[second])])
        self.assertEqual(result['process_ids'],[123,234]);self.assertEqual(result['complete_resources'],1)
        self.assertFalse(result['conflicting_resources'])
        second['renderers'][0]['strides']=[16]
        conflict=summarize_campaign(self.meta,[('one.jsonl',[first]),('two.jsonl',[second])])
        self.assertEqual(conflict['conflicting_resources'],[self.root]);self.assertEqual(conflict['complete_resources'],0)
        with self.assertRaises(ValueError):summarize_campaign(self.meta,[('mixed.jsonl',[first,second])])

    def test_inflight_tail_only_is_ignored(self):
        with tempfile.TemporaryDirectory() as tmp:
            path=Path(tmp)/'probe.jsonl';path.write_bytes(b'{"row":1}\n{"inflight":')
            self.assertEqual(records(path),[{'row':1}])
            path.write_bytes(b'{broken}\n')
            with self.assertRaises(ValueError):records(path)


if __name__=='__main__':unittest.main()
