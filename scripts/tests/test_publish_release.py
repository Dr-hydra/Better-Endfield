"""Release transport tests use synthetic files and fake HTTP only."""
import contextlib
import copy
import importlib.util
import io
import json
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import Mock, patch

SCRIPT = Path(__file__).resolve().parents[1]/'PublishRelease.py'
spec = importlib.util.spec_from_file_location('publish_release_under_test', SCRIPT)
publisher = importlib.util.module_from_spec(spec)
spec.loader.exec_module(publisher)


class Response(io.BytesIO):
    def __init__(self, value, status=200):
        super().__init__(json.dumps(value).encode() if value is not None else b'')
        self.status = status


class FakeGitHub:
    def __init__(self, size, draft=True, wrong_uploaded_size=False):
        self.release = dict(id=1, tag_name='v4.0.3', draft=draft,
                            url='https://api.github.com/repos/test/project/releases/1',
                            upload_url='https://uploads.github.com/repos/test/project/releases/1/assets{?name}',
                            html_url='https://github.com/test/project/releases/tag/v4.0.3',
                            assets=[dict(name='fixture.bin', size=size, state='uploaded', digest='sha256:fixture',
                                         url='https://api.github.com/repos/test/project/releases/assets/2')])
        self.methods = []
        self.connections = 0
        self.uploaded = bytearray()
        self.wrong_uploaded_size = wrong_uploaded_size
        self.published = False

    def urlopen(self, request, timeout):
        method = request.get_method()
        self.methods.append(method)
        if method == 'DELETE':
            self.release['assets'] = []
            return Response(None, 204)
        if method == 'PATCH':
            body = json.loads(request.data)
            if body.get('draft') is False:
                self.published = True
            self.release.update(body)
        return Response(copy.deepcopy(self.release))

    def connection(self, host, timeout):
        assert host == 'uploads.github.com'
        self.connections += 1
        outer = self

        class Connection:
            def putrequest(self, method, path):
                assert method == 'POST' and path.endswith('?name=fixture.bin')
            def putheader(self, key, value):
                pass
            def endheaders(self):
                pass
            def send(self, chunk):
                outer.uploaded.extend(chunk)
            def getresponse(self):
                size = len(outer.uploaded)+(1 if outer.wrong_uploaded_size else 0)
                asset = dict(name='fixture.bin', size=size, state='uploaded', digest='sha256:fixture')
                outer.release['assets'] = [asset]
                return Response(asset, 201)
            def close(self):
                pass

        return Connection()


class PublishReleaseTests(unittest.TestCase):
    def setUp(self):
        self.directory = tempfile.TemporaryDirectory()
        self.addCleanup(self.directory.cleanup)
        root = Path(self.directory.name)
        self.asset = root/'fixture.bin'
        self.asset.write_bytes(b'new synthetic payload')
        self.notes = root/'notes.md'
        self.notes.write_text('Synthetic release only.\n', encoding='utf-8')
        self.argv = ['PublishRelease.py', '--repo', 'test/project', '--tag', 'v4.0.3',
                     '--target', 'main', '--name', 'Fixture', '--notes-file', str(self.notes),
                     '--asset', 'fixture.bin', str(self.asset)]

    def run_publisher(self, github, extra=(), digest=None):
        digest = digest or Mock(side_effect=AssertionError('Default publication must not hash files'))
        output = io.StringIO()
        with patch.object(publisher, 'credentials', return_value='synthetic-not-a-credential'), \
                patch.object(publisher.subprocess, 'run', side_effect=AssertionError('No credential process allowed')), \
                patch.object(publisher.urllib.request, 'urlopen', github.urlopen), \
                patch.object(publisher.http.client, 'HTTPSConnection', github.connection), \
                patch.object(publisher.hashlib, 'file_digest', digest), \
                patch.object(sys, 'argv', self.argv+list(extra)), \
                contextlib.redirect_stdout(output):
            publisher.main()
        return output.getvalue(), digest

    def test_default_reuploads_same_size_draft_without_hashing(self):
        github = FakeGitHub(self.asset.stat().st_size)
        output, digest = self.run_publisher(github)
        digest.assert_not_called()
        self.assertIn('DELETE', github.methods)
        self.assertEqual(github.connections, 1)
        self.assertEqual(github.uploaded, self.asset.read_bytes())
        self.assertNotIn('Verified existing', output)
        self.assertNotIn('"digest"', output)
        self.assertFalse(github.published)

    def test_explicit_digest_reuses_uploaded_matching_asset(self):
        github = FakeGitHub(self.asset.stat().st_size)
        digest = Mock(return_value=Mock(hexdigest=Mock(return_value='fixture')))
        output, digest = self.run_publisher(github, ['--verify-digest'], digest)
        digest.assert_called_once()
        self.assertNotIn('DELETE', github.methods)
        self.assertEqual(github.connections, 0)
        self.assertIn('Verified existing draft asset', output)

    def test_default_size_mismatch_stops_before_publication(self):
        github = FakeGitHub(self.asset.stat().st_size, wrong_uploaded_size=True)
        with self.assertRaisesRegex(RuntimeError, 'Uploaded asset verification failed'):
            self.run_publisher(github, ['--publish'])
        self.assertFalse(github.published)

    def test_published_release_remains_protected(self):
        github = FakeGitHub(self.asset.stat().st_size, draft=False)
        with self.assertRaisesRegex(RuntimeError, 'already published release'):
            self.run_publisher(github)
        self.assertEqual(github.methods, ['GET'])
        self.assertEqual(github.connections, 0)


if __name__ == '__main__':
    unittest.main()
