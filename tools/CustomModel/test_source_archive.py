import json
from pathlib import Path
import subprocess
import tempfile
import unittest
from unittest.mock import patch

from source_archive import backend, parse_listing, MAX_MEMBER
from convert_efmi_poc import Source
import test_component_auto


class ListingTests(unittest.TestCase):
    def row(self,name='folder/mod.ini',extra=''):
        return f'Path = {name}\nFolder = -\nSize = 3\nEncrypted = -\nSymbolic Link = \nHard Link = \n{extra}'

    def test_regular_rar_metadata_with_empty_link_fields(self):
        self.assertEqual(parse_listing(self.row()),{'folder/mod.ini':3})

    def test_unsafe_names_and_duplicate_case_rejected(self):
        for name in ['../outside','C:/outside','/outside','folder/../../outside','@listfile']:
            with self.subTest(name=name),self.assertRaisesRegex(ValueError,'ARCHIVE_PATH'):
                parse_listing(self.row(name))
        with self.assertRaisesRegex(ValueError,'ARCHIVE_PATH'):
            parse_listing(self.row('A.ini')+'\n'+self.row('a.ini'))

    def test_password_links_and_excess_size_rejected(self):
        for text in [self.row().replace('Encrypted = -','Encrypted = +'),
                     self.row().replace('Hard Link = ','Hard Link = elsewhere'),
                     self.row().replace('Size = 3',f'Size = {MAX_MEMBER+1}')]:
            with self.assertRaises(ValueError):parse_listing(text)


class ArchiveIntegrationTests(unittest.TestCase):
    def test_real_7z_conversion_matches_directory(self):
        try: exe=backend()
        except ValueError:self.skipTest('Build archive backend before integration test')
        fixture=test_component_auto.AutoTests();fixture.setUp()
        try:
            folder,_=fixture.prepare()
            # Include a non-ASCII path to exercise UTF-8 listing and exact reads.
            (fixture.root/'中文 资源.txt').write_bytes(b'archive-test')
            with tempfile.TemporaryDirectory() as tmp:
                archive=Path(tmp)/'mod.7z'
                subprocess.run([str(exe),'a','-t7z',str(archive),'.'],cwd=fixture.root,
                    stdout=subprocess.DEVNULL,stderr=subprocess.DEVNULL,check=True)
                source=Source(archive)
                try:self.assertEqual(source.read_exact('中文 资源.txt'),b'archive-test')
                finally:source.close()
                from bem_tool import convert_automatic
                import bem_v1
                with patch('component_auto.catalogs',return_value=[fixture.catalog]):
                    result=convert_automatic(archive,Path(tmp)/'out.bem')
                manifest,payloads=bem_v1.read_package(Path(tmp)/'out.bem')
                self.assertTrue(result['conversion_ready'])
                self.assertEqual(manifest['meshes'],folder.m['meshes'])
                self.assertEqual(payloads,folder.payloads)
        finally:fixture.doCleanups()

    def test_broken_archive_is_reported_without_external_prompt(self):
        with tempfile.TemporaryDirectory() as tmp:
            p=Path(tmp)/'broken.rar';p.write_bytes(b'not-a-rar')
            with self.assertRaisesRegex(ValueError,'ARCHIVE_FORMAT'):Source(p)


if __name__=='__main__':unittest.main()
