import copy
import unittest

from import_efmi_identities import build,crc32c,texture_identity


class IdentityTests(unittest.TestCase):
    def setUp(self):
        self.texture=dict(id='tex:1',name='texture',width=2048,height=2048,mips=12,
            resource_identity=dict(algorithm='crc32c',format='BC7',color_space=1,
                payload_bytes=5592432,accounted_bytes=5592432,
                levels=[dict(width=max(2048>>m,1),height=max(2048>>m,1),legacy_crc32c='d09efcf6') for m in range(12)]))

    def test_crc32c_reference_and_append(self):
        self.assertEqual(crc32c(0,b'123456789'),0xe3069283)
        self.assertEqual(crc32c(crc32c(0,b'1234'),b'56789'),0xe3069283)

    def test_reviewed_endmin_texture_descriptor_anchor(self):
        h,e=texture_identity(self.texture,{(108,1024,1024)})
        self.assertEqual(h,'4b997e43')
        self.assertEqual(e['descriptor'],[2048,2048,12,1,99,1,0,0,8,0,0])

    def test_unknown_encoding_or_missing_runtime_binding_is_not_guessed(self):
        with self.assertRaisesRegex(ValueError,'no direct'):texture_identity(self.texture,set())
        with self.assertRaisesRegex(ValueError,'differs'):texture_identity(self.texture,{(109,2048,2048)})
        with self.assertRaisesRegex(ValueError,'differs'):texture_identity(self.texture,{(108,123,456)})
        self.texture['resource_identity']['format']='ASTC'
        with self.assertRaisesRegex(ValueError,'unmapped'):texture_identity(self.texture,{(108,2048,2048)})

    def fixture(self):
        obs={'world':dict(renderer_id='renderer',path='world/lod0/body',kind='direct')}
        c=dict(character_id='fixture',source_snapshot={'manifest_version':'v'},components={'0':dict(
            mesh_name='mesh',original_index_count=3,evidence=dict(mesh_id='mesh:1',observations=obs))},entries={},textures={})
        mesh=dict(id='mesh:1',submeshes=[dict(first_byte=0,base_vertex=0,index_count=3)],
            resource_identity=dict(algorithm='crc32c',index_count=3,index_size=2,byte_width=6,data_crc32c='12345678'))
        tex2=copy.deepcopy(self.texture);tex2.update(id='tex:2',name='alias')
        d=dict(source={'snapshot':{'manifest_version':'v'}},renderers=[dict(id='renderer',materials=[dict(textures=[
            dict(name='texture',id='tex:1'),dict(name='alias',id='tex:2')])])])
        r=dict(snapshot={'manifest_version':'v'},objects=[mesh,self.texture,tex2])
        runtime={('world','world/lod0/body'):dict(row={'materials':[dict(textures=[
            dict(name=name,graphics_format=108,width=1024,height=1024) for name in ('texture','alias')])]})}
        return c,d,r,runtime

    def test_shared_hash_binds_all_native_names(self):
        c=build(*self.fixture())
        self.assertEqual(c['entries'],{'12345678':0})
        texture=c['textures']['4b997e43']
        self.assertEqual({texture['name'],*texture['aliases']},{'texture','alias'})

    def test_changed_reviewed_identity_or_snapshot_stops_publication(self):
        c,d,r,o=self.fixture();c['entries']={'00000000':0}
        with self.assertRaisesRegex(ValueError,'no longer agrees'):build(c,d,r,o)
        c['entries']={};r['snapshot']['manifest_version']='different'
        with self.assertRaisesRegex(ValueError,'snapshot differs'):build(c,d,r,o)

    def test_partial_index_region_is_explicitly_excluded(self):
        c,d,r,o=self.fixture();r['objects'][0]['submeshes'][0]['first_byte']=2
        result=build(c,d,r,o)
        self.assertEqual(result['entries'],{})
        self.assertEqual(result['identity_issues'][0]['kind'],'mesh')


if __name__=='__main__':unittest.main()
