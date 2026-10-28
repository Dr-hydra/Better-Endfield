import importlib.util
import json
import sys
import tempfile
import unittest
import zipfile
from pathlib import Path
ROOT=Path(__file__).resolve().parents[2]
sys.path.insert(0,str(ROOT/'scripts'))
import PublishResourceRelease as publish

class DistributionTests(unittest.TestCase):
    def test_archive_identity_and_inventory(self):
        with tempfile.TemporaryDirectory() as temporary:
            root=Path(temporary);archive=root/'BE-Core-Windows-1.6.3.2.zip'
            manifest=dict(schemaVersion=1,kind='betterendfield-resource-pack',packageType='core',platform='windows',version='1.6.3.2',gameVersion='1.6.3',files={'model/a.json':2})
            with zipfile.ZipFile(archive,'w') as z:z.writestr('manifest.json',json.dumps(manifest));z.writestr('model/a.json','{}')
            index=publish.build_index([archive],{'version':'4.1.3'})
            entry=index['resources'][0]
            self.assertEqual(entry['bytes'],archive.stat().st_size)
            self.assertEqual(entry['expandedBytes'],2)
            self.assertFalse(entry['optional'])
            self.assertNotIn('sha256',entry)
            with self.assertRaisesRegex(ValueError,'Duplicate'):publish.build_index([archive,archive],{})
            wrong=root/'BE-Core-Windows-2.0.0.1.zip';archive.rename(wrong)
            with self.assertRaisesRegex(ValueError,'Expected archive'):publish.build_index([wrong],{})

    def test_traversal_rejected(self):
        with tempfile.TemporaryDirectory() as temporary:
            path=Path(temporary)/'BE-Core-Windows-1.6.3.2.zip'
            with zipfile.ZipFile(path,'w') as z:z.writestr('../escape','x')
            with self.assertRaisesRegex(ValueError,'Invalid resource path'):publish.build_index([path],{})

if __name__=='__main__':unittest.main()
