import contextlib
import io
import json
from pathlib import Path
import tempfile
import unittest
import zipfile
import bem_projects as projects
import bem_v1 as bem
from bem_tool import main
from test_bem_v1 import fixture


class ProjectTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory(); self.root = Path(self.temp.name)
        self.one = self.root/'one.bem'; self.two = self.root/'two.bem'
        b = fixture(); b.write(self.one); b.m['package_id'] = 'test.second'; b.write(self.two)
    def tearDown(self): self.temp.cleanup()
    def test_unpack_pack_roundtrip(self):
        out = self.root/'editable'; projects.unpack(self.one, out)
        repacked = self.root/'repacked.bem'; projects.pack_project(out/'project.json', repacked)
        self.assertEqual(bem.read_package(self.one), bem.read_package(repacked))
        with self.assertRaises(ValueError): projects.unpack(self.two, out)
    def test_project_edit_and_path_escape(self):
        out = self.root/'project'; projects.unpack(self.one, out)
        path = out/'project.json'; p = bem.load_json(path)
        p['manifest']['appearances'][0]['name'] = '已编辑'
        path.write_text(json.dumps(p), encoding='utf-8')
        projects.pack_project(path, self.root/'edit.bem')
        self.assertEqual(bem.read_package(self.root/'edit.bem')[0]['appearances'][0]['name'], '已编辑')
        p['payload_files'][0] = '../one.bem'; path.write_text(json.dumps(p), encoding='utf-8')
        with self.assertRaises(ValueError): projects.pack_project(path, self.root/'rejected.bem')
    def test_pack_accepts_project_directory(self):
        out = self.root/'editable'; projects.unpack(self.one, out)
        from_dir = self.root/'from-dir.bem'; from_file = self.root/'from-file.bem'
        projects.pack_project(out, from_dir)
        projects.pack_project(out/'project.json', from_file)
        self.assertEqual(from_dir.read_bytes(), from_file.read_bytes())
        self.assertEqual(bem.read_package(from_dir), bem.read_package(self.one))
    def test_pack_reports_unusable_project_input(self):
        empty = self.root/'empty'; empty.mkdir()
        with self.assertRaisesRegex(ValueError, 'contains no project.json'):
            projects.pack_project(empty, self.root/'missing.bem')
        with self.assertRaisesRegex(ValueError, 'does not exist'):
            projects.pack_project(self.root/'nowhere', self.root/'missing.bem')
        binary = self.root/'binary.bem'; binary.write_bytes(b'\x00\xff\xfe not json')
        with self.assertRaisesRegex(ValueError, 'not readable JSON'):
            projects.pack_project(binary, self.root/'missing.bem')
    def test_bundle_stored_members_and_stage(self):
        zip = self.root/'collection.zip'; projects.bundle([self.one,self.two], zip)
        with zipfile.ZipFile(zip) as z:
            self.assertTrue(all(i.compress_type == zipfile.ZIP_STORED for i in z.infolist()))
        out = self.root/'staged'; result = projects.unpack(zip, out)
        self.assertEqual(len(result['packages']),2); self.assertFalse(result['issues'])
        for p in result['packages']: projects.validated(out/p['file'])
    def test_bad_member_does_not_discard_good_package(self):
        zip = self.root/'mixed.zip'
        with zipfile.ZipFile(zip,'w',zipfile.ZIP_DEFLATED) as z:
            z.write(self.one,'subfolder/valid.bem'); z.writestr('bad.bem',b'bad'); z.writestr('readme.txt','information')
        result = projects.inspect_bundle(zip)
        self.assertEqual(len(result['packages']),1); self.assertEqual(len(result['issues']),1)
        self.assertTrue(result['partial']); self.assertEqual(result['ignored_entries'],1)
    def test_duplicate_id_all_competing_members_refused(self):
        zip = self.root/'duplicate.zip'
        with zipfile.ZipFile(zip,'w') as z:
            z.write(self.one,'a.bem'); z.write(self.one,'b.bem'); z.write(self.two,'other.bem')
        result = projects.inspect_bundle(zip)
        self.assertEqual([p['package']['package_id'] for p in result['packages']],['test.second'])
        self.assertEqual(len(result['issues']),2)
    def test_unsafe_paths_and_no_recursive_archives(self):
        zip = self.root/'unsafe.zip'
        with zipfile.ZipFile(zip,'w') as z:
            z.writestr('../outside.bem',self.one.read_bytes()); z.writestr('C:/outside.bem',self.one.read_bytes()); z.writestr('nested.zip',self.one.read_bytes())
        result = projects.inspect_bundle(zip)
        self.assertEqual(result['packages'],[]); self.assertEqual(len(result['issues']),2)
        self.assertFalse((self.root.parent/'outside.bem').exists())
    def test_failed_bundle_preserves_existing_output(self):
        out = self.root/'collection.zip'; out.write_bytes(b'previous')
        with self.assertRaises(ValueError): projects.bundle([self.one,self.one],out)
        self.assertEqual(out.read_bytes(),b'previous')
    def test_cli_version_reports_and_report_collision(self):
        zip = self.root/'collection.zip'
        with contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(main(['bundle',str(self.one),str(self.two),'-o',str(zip)]),0)
            report = self.root/'report.json'
            self.assertEqual(main(['inspect',str(zip),'--report',str(report)]),0)
            self.assertEqual(json.loads(report.read_text(encoding='utf-8'))['format'],'BEM-ZIP')
            before = self.one.read_bytes()
            self.assertEqual(main(['inspect',str(self.one),'--report',str(self.one)]),2)
            self.assertEqual(before,self.one.read_bytes())


if __name__ == '__main__': unittest.main()


class ZipPackageTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory(); self.root = Path(self.temp.name)
        self.source = self.root/'model.bem'; fixture().write(self.source)
    def tearDown(self): self.temp.cleanup()

    def test_pack_zip_roundtrip_is_byte_identical(self):
        package = self.root/'model.zip'
        report = projects.pack_zip(self.source, package)
        self.assertEqual(report['format'], 'BEM-ZIP-PACKAGE')
        self.assertEqual(report['payload_count'], len(bem.read_package(self.source)[1]))
        self.assertTrue(projects.has_package_manifest(package))
        read = projects.read_zip_package(package)
        self.assertEqual(read['prefix'], '')
        self.assertEqual(read['manifest']['format_version'], '1.0')
        # The package must survive a trip through an editable project unchanged.
        project = self.root/'project'; projects.unpack(package, project)
        rebuilt = self.root/'rebuilt.bem'; projects.pack_project(project/'project.json', rebuilt)
        self.assertEqual(self.source.read_bytes(), rebuilt.read_bytes())

    def test_wrapper_folder_is_unwrapped(self):
        inner = self.root/'model.zip'; projects.pack_zip(self.source, inner)
        wrapped = self.root/'wrapped.zip'
        with zipfile.ZipFile(inner) as src, zipfile.ZipFile(wrapped, 'w') as dst:
            for entry in src.infolist():
                dst.writestr('MyModel/' + entry.filename, src.read(entry))
        self.assertTrue(projects.has_package_manifest(wrapped))
        self.assertEqual(projects.read_zip_package(wrapped)['prefix'], 'MyModel/')

    def test_missing_payload_and_bundle_are_rejected(self):
        broken = self.root/'broken.zip'
        with zipfile.ZipFile(broken, 'w') as archive:
            archive.writestr('manifest.json', json.dumps(dict(
                package_id='p', target={}, meshes=[dict(streams=[dict(payload=0)])])))
        with self.assertRaises(ValueError): projects.read_zip_package(broken)
        bundle = self.root/'bundle.zip'; projects.bundle([self.source], bundle)
        self.assertFalse(projects.has_package_manifest(bundle))
        self.assertTrue(projects.has_bem_members(bundle))
