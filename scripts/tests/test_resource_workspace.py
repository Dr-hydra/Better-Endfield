"""Offline resource-chain regressions; no game, network or compiled tools."""
from __future__ import annotations

import contextlib
import io
import json
import os
import subprocess
import sys
import tempfile
import types
import unittest
from pathlib import Path
from unittest.mock import patch

ROOT = Path(__file__).resolve().parents[2]
sys.path.insert(0, str(ROOT / "scripts"))
sys.path.insert(0, str(ROOT / "tools/CombatDataExporter"))
import RefreshEndfieldResourceInputs as refresh
import ScanCharacterAssets as scanner
import GenerateModCharacterPresets as presets
import BuildVoiceCatalog as voice_catalog
import export_combat_data as combat
from workspace_config import load_workspace, producer_key, write_if_changed


class ResourceWorkspaceTests(unittest.TestCase):
    def setUp(self):
        temp = ROOT / "temp"
        temp.mkdir(exist_ok=True)
        self.temporary = tempfile.TemporaryDirectory(prefix="resource-test-", dir=temp)
        self.addCleanup(self.temporary.cleanup)
        self.root = Path(self.temporary.name)
        self.config = self.root / "workspace.json"
        self.config.write_text(json.dumps({
            "game": {"install_dir": None},
            "paths": {name: str(self.root / name) for name in
                      ("temp", "build", "cache", "inputs", "toolchains")},
            "resource_update": {
                "input_root": str(self.root / "inputs"),
                "catalog_root": str(self.root / "catalog"),
                "outputs": {"character_presets": str(self.root / "presets.json")},
            },
        }), encoding="utf-8")
        self.ws = load_workspace(self.config)
        self.ws.env()

    def quiet(self):
        return contextlib.redirect_stdout(io.StringIO())

    def test_config_and_explicit_cli_precedence_without_game(self):
        explicit = self.root / "explicit.json"
        with patch.object(sys, "argv", ["presets", "--workspace-config", str(self.config),
                                        "--output", str(explicit)]):
            args = presets.parse_args()
        self.assertEqual(args.output, explicit)
        self.assertEqual(args.catalog, self.root / "catalog/characters.json")
        configured_lookup = self.ws.path

        def without_game(key, *parts, **options):
            return None if key == "game.install_dir" else configured_lookup(key, *parts, **options)

        with patch.dict(os.environ, {"BE_WORKSPACE_CONFIG": str(self.config)}), \
                patch.object(sys, "argv", ["refresh"]), \
                patch.object(refresh, "load_workspace", return_value=self.ws), \
                patch.object(self.ws, "path", side_effect=without_game):
            args = refresh.parse_args()
        self.assertIsNone(args.game_path)
        self.assertEqual(args.output, self.root / "inputs")

    def test_check_reports_missing_tools_without_requiring_game(self):
        configured = json.loads(self.config.read_text(encoding="utf-8"))
        configured["tools"] = {"resconv": str(self.root / "missing/ResConv.exe"),
                               "endfield_unpacker_root": str(self.root / "missing/unpacker")}
        self.config.write_text(json.dumps(configured), encoding="utf-8")
        output = io.StringIO()
        with patch.object(sys, "argv", ["refresh", "--workspace-config", str(self.config), "--check"]), \
                patch.object(refresh, "load_module", side_effect=AssertionError("check must not load unpacker")), \
                contextlib.redirect_stdout(output):
            self.assertEqual(refresh.main(), 0)
        report = json.loads(output.getvalue())
        self.assertFalse(report["refreshPerformed"])
        self.assertTrue(all(item["status"] == "missing" for item in report["prerequisites"]))

    def test_identity_does_not_reuse_missing_files_or_new_parameters(self):
        output = self.root / "snapshot"
        write_if_changed(output / "record.json", "{}")
        key = producer_key([{"vfsId": "A"}], "tool-1", {"platform": "Windows"})
        metadata = {"producerKey": key, "files": [{"path": "record.json", "size": 2}]}
        self.assertTrue(refresh.snapshot_complete(output, metadata, key))
        changed = producer_key([{"vfsId": "A"}], "tool-2", {"platform": "Windows"})
        self.assertFalse(refresh.snapshot_complete(output, metadata, changed))
        (output / "record.json").unlink()
        self.assertFalse(refresh.snapshot_complete(output, metadata, key))

    def test_publication_reuses_equal_files_and_retains_overrides(self):
        output, staging = self.root / "output", self.root / "staging"
        write_if_changed(output / "same.json", "{}")
        write_if_changed(output / "manual-override.json", '{"manual":true}')
        stamp = (output / "same.json").stat().st_mtime_ns
        write_if_changed(staging / "same.json", "{}")
        refresh.publish_snapshot(staging, output, self.ws.path("paths.temp"))
        self.assertEqual((output / "same.json").stat().st_mtime_ns, stamp)
        self.assertTrue((output / "manual-override.json").is_file())

    def test_publish_failure_restores_current_snapshot(self):
        output, staging = self.root / "output", self.root / "staging"
        for name in ("a.json", "b.json", "input-snapshot.json"):
            write_if_changed(output / name, "old")
            write_if_changed(staging / name, "new")
        original = refresh.write_if_changed
        failed = False

        def fail_once(path, content):
            nonlocal failed
            if Path(path) == output / "b.json" and not failed:
                failed = True
                raise OSError("simulated disk failure")
            return original(path, content)

        with patch.object(refresh, "write_if_changed", side_effect=fail_once):
            with self.assertRaises(OSError):
                refresh.publish_snapshot(staging, output, self.ws.path("paths.temp"))
        for name in ("a.json", "b.json", "input-snapshot.json"):
            self.assertEqual((output / name).read_text(), "old")

    def test_native_table_cache_missing_file_tool_and_vfs_changes(self):
        game = self.root / "fake-game"
        blc = game / "Endfield_Data/StreamingAssets/VFS/Table/Table.blc"
        write_if_changed(blc, "vfs-A")
        extractor = self.root / "extractor.exe"
        write_if_changed(extractor, b"fake-tool")
        output = self.root / "tables/Table"
        calls = []

        def extract(command, **kwargs):
            calls.append(command)
            self.assertEqual(kwargs["env"]["TEMP"], str(self.ws.path("paths.temp")))
            self.assertTrue(self.ws.path("paths.temp") in Path(command[command.index("--out") + 1]).parents)
            staged = Path(command[command.index("--out") + 1]) / "Table"
            for name in ("CharacterTable", "CharGrowthTable", "I18nTextTable_CN", "ItemTable",
                         "WeaponBasicTable", "EquipTable", "EquipSuitTable", "DungeonTable"):
                write_if_changed(staged / (name + ".json"), "{}")

        with patch.object(combat.subprocess, "run", side_effect=extract), self.quiet():
            first = combat.refresh_game_tables(game, extractor, output, 2, self.ws)
            write_if_changed(output / "manual-override.json", "{}")
            stamp = (output / "ItemTable.json").stat().st_mtime_ns
            self.assertEqual(combat.refresh_game_tables(game, extractor, output, 2, self.ws), first)
            self.assertEqual(len(calls), 1)
            self.assertEqual((output / "ItemTable.json").stat().st_mtime_ns, stamp)
            (output / "ItemTable.json").unlink()
            combat.refresh_game_tables(game, extractor, output, 2, self.ws)
            self.assertEqual(len(calls), 2)
            self.assertTrue((output / "ItemTable.json").is_file())
            write_if_changed(blc, "vfs-B")
            combat.refresh_game_tables(game, extractor, output, 2, self.ws)
            self.assertEqual(len(calls), 3)
            write_if_changed(extractor, b"fake-tool-new")
            combat.refresh_game_tables(game, extractor, output, 2, self.ws)
            self.assertEqual(len(calls), 4)
            self.assertTrue((output / "manual-override.json").is_file())
        metadata = (output.parent / "source-metadata.json").read_bytes()
        write_if_changed(blc, "vfs-C")
        with patch.object(combat.subprocess, "run", side_effect=subprocess.CalledProcessError(1, "fake")):
            with self.assertRaises(subprocess.CalledProcessError):
                combat.refresh_game_tables(game, extractor, output, 2, self.ws)
        self.assertEqual((output.parent / "source-metadata.json").read_bytes(), metadata)
        self.assertEqual((output / "ItemTable.json").read_text(), "{}")

    def test_scanner_repeat_run_keeps_timestamp_and_repairs_missing_output(self):
        inputs = self.root / "inputs"
        manifest = inputs / "Bundles/Windows/manifest.json"
        write_if_changed(manifest, json.dumps({"Version": 1, "Hash": "fake", "Bundles": [], "Assets": []}))
        prefabs = inputs / "Json_decrypted/NPC/PrefabInfo"
        write_if_changed(prefabs / "npc_chr_0001_fake.json", json.dumps({
            "correspondingCharId": "chr_0001_fake",
            "cpuAnimationTempletName": "NPC/AnimationConfig/Humanoid/m/fake",
            "partNameIdList": [],
        }))
        output = self.root / "catalog"
        argv = ["scanner", "--workspace-config", str(self.config)]
        with patch.object(sys, "argv", argv), self.quiet():
            scanner.main()
            before = {p.name: (p.read_bytes(), p.stat().st_mtime_ns) for p in output.iterdir() if p.is_file()}
            scanner.main()
            after = {p.name: (p.read_bytes(), p.stat().st_mtime_ns) for p in output.iterdir() if p.is_file()}
            self.assertEqual(before, after)
            (output / "selection-options.json").unlink()
            scanner.main()
            self.assertEqual((output / "characters.json").stat().st_mtime_ns, before["characters.json"][1])
            self.assertTrue((output / "selection-options.json").is_file())

    def test_voice_catalog_cache_repairs_missing_binary_and_failed_read_keeps_output(self):
        package = self.root / ("A" * 32 + ".chk")
        write_if_changed(package, b"fake package")
        manifest = self.root / "voice-manifest.json"
        write_if_changed(manifest, json.dumps({
            "kind": "endfield-voice-event-media-manifest",
            "pckPackages": [{"mappingLanguage": "English", "source": str(package)}],
            "voices": [{"characterId": "chr_fake", "voiceId": "voice_fake",
                        "languageMappings": [{"language": "English",
                                             "soundSlots": [{"soundObjectId": 1, "mediaId": 42}]}]}],
        }))
        args = types.SimpleNamespace(manifest=manifest, language="English", character_id="chr_fake",
                                     package_path=None, game_path=None, output=self.root / "voice.bevcat",
                                     workspace=self.ws)
        index = types.SimpleNamespace(header_sha256="header", size=package.stat().st_size,
                                      media=[types.SimpleNamespace(file_id=42)])
        calls = []

        def payload(*_):
            calls.append(True)
            return b"fake audio payload"

        generator = types.SimpleNamespace(parse_pck=lambda *_: index, read_pck_payload=payload)
        with patch.object(voice_catalog, "load_generator", return_value=generator):
            report = voice_catalog.build_catalog(args)
            stamp = args.output.stat().st_mtime_ns
            self.assertEqual(voice_catalog.build_catalog(args), report)
            self.assertEqual(len(calls), 1)
            self.assertEqual(args.output.stat().st_mtime_ns, stamp)
            args.output.unlink()
            voice_catalog.build_catalog(args)
            self.assertEqual(len(calls), 2)
            previous = args.output.read_bytes()
            write_if_changed(manifest, manifest.read_text() + "\n")
            generator.read_pck_payload = lambda *_: (_ for _ in ()).throw(OSError("simulated read failure"))
            with self.assertRaises(OSError):
                voice_catalog.build_catalog(args)
            self.assertEqual(args.output.read_bytes(), previous)

    def test_combat_offline_repeat_preserves_registry_overrides_and_outputs(self):
        tables, json_root = self.root / "tables", self.root / "json"
        write_if_changed(tables / "I18nTextTable_CN.json", "{}")
        for directory in ("SkillData", "BuffData"):
            (json_root / directory).mkdir(parents=True)
        output = self.root / "combat/dictionary.json"
        registry = self.root / "id-registry.json"
        write_if_changed(registry, '{"version":1,"ids":["manual_keep"]}')
        write_if_changed(output, '{"manualOverrides":{"manual_keep":{"name":"Manual"}}}')
        argv = ["combat", "--workspace-config", str(self.config), "--no-refresh-tables",
                "--no-refresh-json-data", "--table-dir", str(tables), "--json-data-dir", str(json_root),
                "--output", str(output), "--min-output", str(self.root / "web/dict.min.json"),
                "--buff-source-output", str(self.root / "combat/buff-sources.bemap"),
                "--id-registry", str(registry), "--stage-output", str(self.root / "web/stages.json"),
                "--stage-map-output", str(self.root / "web/stage-map.json")]
        with patch.object(sys, "argv", argv), patch.object(combat, "export_web_icons", return_value={"missing": {}}), self.quiet():
            combat.main()
            files = [output, registry, self.root / "web/dict.min.json", self.root / "web/combat-ids.min.json",
                     self.root / "web/stages.json", self.root / "web/stage-map.json", self.root / "combat/buff-sources.bemap"]
            before = [(p.read_bytes(), p.stat().st_mtime_ns) for p in files]
            combat.main()
            self.assertEqual(before, [(p.read_bytes(), p.stat().st_mtime_ns) for p in files])
        self.assertEqual(json.loads(output.read_text(encoding="utf-8"))["manualOverrides"], {"manual_keep": {"name": "Manual"}})
        self.assertIn("manual_keep", json.loads(registry.read_text(encoding="utf-8"))["ids"])

    def test_input_refresh_cache_and_conversion_failure_keep_valid_snapshot(self):
        game = self.root / "fake-game"
        (game / "Endfield_Data/StreamingAssets/VFS").mkdir(parents=True)
        unpacker = self.root / "unpacker"
        for name in ("decrypt_vfs.py", "decode_sparkbuffer.py", "decode_json_other.py"):
            write_if_changed(unpacker / name, "# mock tool")
        resconv = self.root / "ResConv.exe"
        write_if_changed(resconv, b"mock tool")
        record = refresh.VfsRecord("TableCfg/AudioDialog.bytes", "StreamingAssets", "Table",
                                   "A" * 32, 0, 1, False, 0, "B" * 32, "C" * 32)
        selected = [record]
        output = self.root / "inputs"
        calls = []

        def extract(_unpacker, _roots, staging, _pattern, records):
            calls.append(True)
            write_if_changed(staging / "TableCfg/AudioDialog.bytes", b"x")
            return records

        def convert(_root, _tool, staging, _platform, _env):
            write_if_changed(staging / "Table/AudioDialog.json", "{}")
            write_if_changed(staging / "Bundles/Windows/manifest.json", "{}")
            return {"prefabInfoCount": 30, "audioDialogRows": 1, "manifestAssets": 1}, set()

        argv = ["refresh", "--workspace-config", str(self.config), "--game-path", str(game), "--resconv", str(resconv)]
        configured = json.loads(self.config.read_text(encoding="utf-8"))
        configured["tools"] = {"endfield_unpacker_root": str(unpacker)}
        self.config.write_text(json.dumps(configured), encoding="utf-8")
        with patch.object(sys, "argv", argv), patch.object(sys, "path", list(sys.path)), \
                patch.dict(sys.modules, {"Crypto": types.ModuleType("Crypto")}), \
                patch.object(refresh, "load_module", return_value=types.SimpleNamespace()), \
                patch.object(refresh, "select_overlay", return_value=selected), \
                patch.object(refresh, "extract_overlay", side_effect=extract), \
                patch.object(refresh, "convert_inputs", side_effect=convert) as conversion, self.quiet():
            refresh.main()
            stamp = (output / "input-snapshot.json").stat().st_mtime_ns
            refresh.main()
            self.assertEqual(len(calls), 1)
            self.assertEqual((output / "input-snapshot.json").stat().st_mtime_ns, stamp)
            (output / "Table/AudioDialog.json").unlink()
            refresh.main()
            self.assertEqual(len(calls), 2)
            before = (output / "input-snapshot.json").read_bytes()
            selected.append(refresh.VfsRecord("TableCfg/other.bytes", "Persistent", "Table",
                                             "D" * 32, 0, 1, False, 0))
            conversion.side_effect = ValueError("simulated conversion failure")
            with self.assertRaises(ValueError):
                refresh.main()
            self.assertEqual((output / "input-snapshot.json").read_bytes(), before)
            self.assertEqual((output / "Table/AudioDialog.json").read_text(), "{}")

    def test_icon_cache_reuses_selection_and_extracts_only_missing_file(self):
        streaming = self.root / "fake-game/Endfield_Data/StreamingAssets"
        write_if_changed(streaming / "VFS/Bundles/Bundles.blc", "vfs-A")
        extractor = self.root / "extractor.exe"
        write_if_changed(extractor, b"mock tool")
        cache = self.root / "icon-cache"
        calls = []

        def extract(command, **_):
            calls.append(command)
            pattern = command[command.index("--asset-name") + 1]
            output = Path(command[command.index("--out") + 1])
            for name in ("icon_a", "icon_b"):
                if name in pattern:
                    write_if_changed(output / f"{name}.png", b"mock PNG")

        with patch.object(combat.subprocess, "run", side_effect=extract):
            combat.extract_named_icons({"icon_a", "icon_b"}, streaming, None, extractor, cache, 2, self.ws)
            stamp = (cache / "icon_a.png").stat().st_mtime_ns
            combat.extract_named_icons({"icon_a", "icon_b"}, streaming, None, extractor, cache, 2, self.ws)
            self.assertEqual(len(calls), 1)
            (cache / "icon_b.png").unlink()
            combat.extract_named_icons({"icon_a", "icon_b"}, streaming, None, extractor, cache, 2, self.ws)
            self.assertEqual(len(calls), 2)
            self.assertNotIn("icon_a", calls[-1][calls[-1].index("--asset-name") + 1])
            self.assertEqual((cache / "icon_a.png").stat().st_mtime_ns, stamp)


if __name__ == "__main__":
    unittest.main()
