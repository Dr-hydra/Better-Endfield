import importlib.util
import json
from pathlib import Path
import tempfile
import unittest
import zipfile

SPEC=importlib.util.spec_from_file_location('resource_packs',Path(__file__).resolve().parents[1]/'BuildResourcePacks.py')
packs=importlib.util.module_from_spec(SPEC);SPEC.loader.exec_module(packs)

class ResourcePackTests(unittest.TestCase):
    def setUp(self):
        self.temp=tempfile.TemporaryDirectory();self.addCleanup(self.temp.cleanup);self.root=Path(self.temp.name)
        self.source=self.root/'source';self.source.write_bytes(b'ab')
    def stage(self,version='1.6.3.1'):
        return packs.stage(self.root/'out','windows','core',version,{'model/data.json':self.source},{'fixture':True})
    def test_inventory_and_idempotent_staging(self):
        target=self.stage();self.assertEqual(packs.verify(target)['files'],{'model/data.json':2});self.assertEqual(self.stage(),target)
    def test_same_length_change_requires_new_revision(self):
        original=self.stage();self.source.write_bytes(b'cd')
        with self.assertRaisesRegex(ValueError,'increment'):self.stage()
        self.assertEqual((original/'model/data.json').read_bytes(),b'ab');self.assertEqual((self.stage('1.6.3.2')/'model/data.json').read_bytes(),b'cd')
    def test_truncation_is_rejected(self):
        target=self.stage();(target/'model/data.json').write_bytes(b'a')
        with self.assertRaises(ValueError):packs.verify(target)
    def test_unlisted_file_is_rejected(self):
        target=self.stage();(target/'extra').write_bytes(b'x')
        with self.assertRaises(ValueError):packs.verify(target)
    def test_unsafe_paths_are_rejected(self):
        for name in ('../a','/a','C:/a','a//b','a/./b','a'+chr(92)+'b'):
            with self.subTest(name=name),self.assertRaises(ValueError):packs.relative(name)
    def test_four_numeric_components_required(self):
        for value in ('1.6.3','1x6x3x1','1.6.3.-1','v1.6.3.1'):
            with self.subTest(version=value),self.assertRaises(ValueError):self.stage(value)
    def preview(self,fmt):
        archive=self.root/'preview.zip'
        pack=dict(schema=1,kind='native-head-preview-prototype',character='chr_test',snapshot={'manifestVersion':'fixture'},
            meshes=[],renderers=[dict(resource_root='world',resource_asset_paths=['prefab'])],textures=[dict(payload='t.bin',format=fmt)])
        with zipfile.ZipFile(archive,'w') as z:z.writestr('pack.json',json.dumps(pack));z.writestr('t.bin',b'a')
        return archive
    def test_platform_encoding_and_character_index(self):
        target=packs.stage(self.root/'out','android','preview','1.6.4.1',{}, {},[self.preview('ASTC_6x6')])
        self.assertEqual(packs.verify(target)['platform'],'android');index=packs.read(target/'characters.json');self.assertEqual(index['characters'][0]['character'],'chr_test')
    def test_wrong_android_texture_format_is_rejected(self):
        with self.assertRaisesRegex(ValueError,'ASTC'):packs.stage(self.root/'out','android','preview','1.6.4.1',{}, {},[self.preview('BC7')])

if __name__=='__main__':unittest.main()
