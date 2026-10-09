"""Behavioral fixtures for portable paths, precedence, retention and reuse."""
import json
import os
import sys
import tempfile
import unittest
from pathlib import Path
from unittest.mock import patch

sys.path.insert(0,str(Path(__file__).resolve().parents[1]))
from workspace_config import load_workspace, producer_key, write_if_changed
from game_discovery import discover_game, game_directory


class WorkspaceTests(unittest.TestCase):
    def setUp(self):
        temp_root=load_workspace().ensure('paths.temp')
        self.temp=tempfile.TemporaryDirectory(dir=temp_root,prefix='workspace-fixture-')
        self.addCleanup(self.temp.cleanup)
        self.root=Path(self.temp.name)
        (self.root/'config').mkdir()
        source=Path(__file__).resolve().parents[2]/'config/workspace.defaults.json'
        (self.root/'config/workspace.defaults.json').write_bytes(source.read_bytes())
        self.env=patch.dict(os.environ,{'BE_WORKSPACE_CONFIG':''})
        self.env.start();self.addCleanup(self.env.stop)

    def local(self,values):
        (self.root/'config/workspace.local.json').write_text(json.dumps(values),encoding='utf-8')

    def test_relative_paths_follow_checkout(self):
        ws=load_workspace(repo_root=self.root)
        self.assertEqual(ws.path('paths.inputs'),self.root/'inputs')
        self.assertEqual(ws.path('resource_update.input_root'),self.root/'inputs/1.5.3/Windows/baseline')

    def test_local_override_merges_without_dropping_defaults(self):
        self.local({'paths':{'build':'scratch/compiled'}})
        ws=load_workspace(repo_root=self.root)
        self.assertEqual(ws.path('paths.build'),self.root/'scratch/compiled')
        self.assertEqual(ws.get('game.version'),'1.5.3')

    def test_explicit_configuration_wins_over_environment(self):
        first=self.root/'first.json';second=self.root/'second.json'
        first.write_text(json.dumps({'paths':{'build':'first-build'}}))
        second.write_text(json.dumps({'paths':{'build':'second-build'}}))
        with patch.dict(os.environ,{'BE_WORKSPACE_CONFIG':str(first)}):
            self.assertEqual(load_workspace(second,self.root).path('paths.build'),self.root/'second-build')

    def test_missing_explicit_config_is_not_silently_ignored(self):
        with self.assertRaises(FileNotFoundError):load_workspace(self.root/'missing.json',self.root)

    def test_cleanup_cannot_point_at_input_or_git(self):
        for target in ['inputs','.git','legacy','docs','..']:
            self.local({'paths':{'build':target}})
            with self.assertRaises(ValueError,msg=target):load_workspace(repo_root=self.root)

    def test_cleanup_cannot_erase_install_or_rollback(self):
        self.local({'test':{'be_install_dir':str(self.root/'build')}})
        with self.assertRaises(ValueError):load_workspace(repo_root=self.root)
        self.local({'legacy':{'root':str(self.root/'temp/archive')}})
        with self.assertRaises(ValueError):load_workspace(repo_root=self.root)

    def test_clean_roots_must_not_overlap(self):
        self.local({'paths':{'temp':'build/tmp'}})
        with self.assertRaises(ValueError):load_workspace(repo_root=self.root)

    def test_producer_identity_is_order_independent_and_version_sensitive(self):
        self.assertEqual(producer_key('snapshot','tool',{'a':1,'b':2}),producer_key('snapshot','tool',{'b':2,'a':1}))
        self.assertNotEqual(producer_key('snapshot','tool',{}),producer_key('snapshot','new-tool',{}))

    def test_identical_write_preserves_file_time(self):
        p=self.root/'output.json';self.assertTrue(write_if_changed(p,b'original'))
        before=p.stat().st_mtime_ns
        self.assertFalse(write_if_changed(p,b'original'));self.assertEqual(p.stat().st_mtime_ns,before)
        self.assertTrue(write_if_changed(p,b'changed'));self.assertEqual(p.read_bytes(),b'changed')

    def test_game_discovery_rejects_be_launcher(self):
        p=self.root/'BetterEndfieldNext.exe';p.write_bytes(b'fixture')
        self.assertIsNone(game_directory(str(p)))
        game=self.root/'game';game.mkdir();(game/'Endfield.exe').write_bytes(b'fixture')
        self.assertEqual(game_directory(str(game/'Endfield.exe')),game)

    def test_registry_candidate_validation_and_provenance(self):
        rules={'schema_version':1,'executable':'Endfield.exe'}
        (self.root/'config/game-discovery.json').write_text(json.dumps(rules))
        game=self.root/'game';game.mkdir();(game/'Endfield.exe').write_bytes(b'fixture')
        location,source=discover_game(self.root,[('not-a-game','stale'),(str(game/'Endfield.exe'),'mock-registry')])
        self.assertEqual(location,game);self.assertEqual(source,'mock-registry')


if __name__=='__main__':unittest.main()
