"""Pure fixture/mocked-process regressions for the registry's execution boundaries.

These checks never execute an existing suite, compiler, game, device or binary.
"""
from __future__ import annotations

import copy
import json
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch

ROOT = Path(__file__).resolve().parents[2]
sys.path.insert(0, str(ROOT / "scripts"))
sys.path.insert(0, str(Path(__file__).resolve().parent))
import run_workspace_tests as runner
from run_python_suite import summarize
from workspace_config import load_workspace


class FixtureWorkspace:
    def __init__(self, root):
        self.root = root.resolve()
        self.values = {"paths": {name: name for name in ("build", "temp", "inputs", "research", "releases", "toolchains", "generated")},
                       "legacy": {"root": "../retained-legacy"}, "test": {"be_install_dir": None, "android_serial": None},
                       "tools": {"python": sys.executable}, "game": {"version": "1.5.3", "snapshot": "fixture"}}

    def get(self, key, default=None):
        value = self.values
        for segment in key.split("."):
            if not isinstance(value, dict) or segment not in value:
                return default
            value = value[segment]
        return value

    def path(self, key, *parts, required=True):
        value = self.get(key)
        if not value:
            if required:
                raise ValueError(key)
            return None
        return (self.root / value).joinpath(*parts).resolve()

    def command(self, key):
        return self.get(key, sys.executable)

    def env(self):
        raise AssertionError("Pure simulations must not prepare or launch real suites")


def suite(entry=None, **changes):
    item = {"id": "fixture.sample", "module": "workspace", "kind": "simulation", "platform": ["any"], "game_version": ["any"],
            "input": {"id": "synthetic", "type": "synthetic"}, "prerequisite": [], "status": "active", "replaced_by": None,
            "purpose": "Boundary fixture", "entry": entry or {"type": "python_unittest", "cwd": ".", "modules": ["fixture"]}}
    item.update(changes)
    return item


class RegistryBoundaryTests(unittest.TestCase):
    def setUp(self):
        workspace = load_workspace()
        area = runner.safe_output(workspace, workspace.path("paths.temp", "workspace-tests", "registry-fixtures"), "paths.temp")
        area.mkdir(parents=True, exist_ok=True)
        self.temporary = tempfile.TemporaryDirectory(prefix="registry-", dir=area)
        self.addCleanup(self.temporary.cleanup)
        fixture_root = Path(self.temporary.name) / "repo"
        fixture_root.mkdir()
        self.ws = FixtureWorkspace(fixture_root)
        self.source = {"commit": "fixture-commit", "code_revision": "fixture-dirty-revision"}
        self.entry = {"type": "native", "cwd": ".", "receipt": "tests/receipts/fixture.json", "artifacts": ["native/fixture.exe"],
                      "executable": "native/fixture.exe", "configuration": "Release"}

    def make_receipt(self):
        binary = self.ws.path("paths.build", "native/fixture.exe")
        binary.parent.mkdir(parents=True)
        binary.write_bytes(b"fixture data, deliberately not an executable")
        stat = binary.stat()
        value = {"schema_version": 1, "suite_id": "fixture.sample", "source_commit": self.source["commit"],
                 "code_revision": self.source["code_revision"], "build_succeeded": True, "configuration": "Release",
                 "tool_revision": "fixture compiler", "build_command": ["fixture-only"], "built_at": "fixture-time",
                 "artifacts": [{"path": "native/fixture.exe", "size": stat.st_size, "mtime_ns": stat.st_mtime_ns}]}
        path = self.ws.path("paths.build", self.entry["receipt"])
        path.parent.mkdir(parents=True)
        path.write_text(json.dumps(value), encoding="utf-8")
        return path, value, binary

    def test_catalog_is_unique_and_all_original_creator_modules_are_registered(self):
        data = json.loads((ROOT / "config/test-suites.json").read_text(encoding="utf-8"))
        items = runner.validate_registry(data, ROOT)
        registered = {source for item in items for source in item["entry"].get("sources", [])}
        # This is a bounded one-directory inventory; adding an existing test must
        # not silently remove it from the formal suite catalog.
        original = {path.relative_to(ROOT).as_posix() for path in (ROOT / "tools/CustomModel").glob("test_*.py")}
        self.assertTrue(original)
        self.assertEqual(original - registered, set())
        for relationship in data.get("validation_catalog", []):
            self.assertTrue(set(relationship["replaced_by"]).issubset({item["id"] for item in items}))

    def test_duplicate_id_and_replacement_cycle_are_rejected(self):
        a = suite()
        with self.assertRaisesRegex(ValueError, "duplicate"):
            runner.validate_registry({"schema_version": 1, "suites": [a, copy.deepcopy(a)]}, self.ws.root)
        a.update(status="replaced", replaced_by="fixture.other")
        b = suite(id="fixture.other", status="replaced", replaced_by="fixture.sample")
        with self.assertRaisesRegex(ValueError, "cyclic"):
            runner.validate_registry({"schema_version": 1, "suites": [a, b]}, self.ws.root)

    def test_source_and_receipt_paths_cannot_escape(self):
        with self.assertRaisesRegex(ValueError, "escapes"):
            runner.source_path(self.ws.root, "../retained-legacy/script.py")
        entry = {**self.entry, "receipt": "../retained-legacy/receipt.json"}
        with self.assertRaisesRegex(ValueError, "relative to paths.build"):
            runner.validate_registry({"schema_version": 1, "suites": [suite(entry)]}, self.ws.root)

    def test_game_and_device_gate_precedes_any_process_preparation(self):
        with patch.object(runner.subprocess, "run", side_effect=AssertionError("No process may start")):
            for kind in ("game", "device"):
                item = runner.plan_suite(self.ws, suite(kind=kind), self.source)
                self.assertEqual(item["status"], "blocked")
                self.assertIn("--allow-game", item["reason"])
                self.assertFalse(item["command"])
                result = runner.execute_suite(self.ws, suite(kind=kind), item, self.ws.path("paths.build", "fixture-run"), self.source)
                self.assertEqual(result["status"], "blocked")

    def test_device_requires_install_target_and_explicit_serial_even_with_opt_in(self):
        target = suite(kind="device", platform=["Android"])
        item = runner.plan_suite(self.ws, target, self.source, allow_game=True)
        self.assertIn("test.be_install_dir", item["reason"])
        self.ws.values["test"]["be_install_dir"] = str(self.ws.root.parent / "fixture-install")
        (self.ws.root.parent / "fixture-install").mkdir()
        item = runner.plan_suite(self.ws, target, self.source, allow_game=True)
        self.assertIn("test.android_serial", item["reason"])
        self.ws.values["test"]["android_serial"] = "fixture-device"
        item = runner.plan_suite(self.ws, target, self.source, allow_game=True)
        self.assertEqual(item["android_serial"], "fixture-device")
        self.assertEqual(item["game_target"], str(self.ws.root.parent / "fixture-install"))
        self.assertEqual(item["status"], "not_run")

    def test_game_target_cannot_be_source_or_legacy(self):
        for value in (str(self.ws.root), str(self.ws.path("legacy.root"))):
            self.ws.values["test"]["be_install_dir"] = value
            item = runner.plan_suite(self.ws, suite(kind="game"), self.source, allow_game=True)
            self.assertEqual(item["status"], "blocked")
            self.assertIn("overlaps", item["reason"])

    def test_opt_in_does_not_bypass_unadapted_legacy_scripts(self):
        self.ws.values["test"].update(be_install_dir=str(self.ws.root.parent / "fixture-install"), android_serial="fixture-device")
        (self.ws.root.parent / "fixture-install").mkdir()
        entry = {"type": "catalog_only", "cwd": ".", "blocked_reason": "Output adapter missing"}
        item = runner.plan_suite(self.ws, suite(entry, kind="device", platform=["Android"]), self.source, allow_game=True)
        self.assertEqual(item["status"], "blocked")
        self.assertEqual(item["reason"], "Output adapter missing")

    def test_existing_native_file_without_receipt_is_blocked(self):
        binary = self.ws.path("paths.build", "native/fixture.exe")
        binary.parent.mkdir(parents=True)
        binary.write_bytes(b"old binary")
        _, error = runner.receipt_check(self.ws, suite(self.entry), self.source)
        self.assertIn("Missing build receipt", error)

    def test_native_receipt_requires_exact_commit_dirty_revision_and_artifacts(self):
        path, receipt, _ = self.make_receipt()
        evidence, error = runner.receipt_check(self.ws, suite(self.entry), self.source)
        self.assertIsNone(error)
        self.assertTrue(evidence["sha256"])
        for field, invalid in (("source_commit", "old-commit"), ("code_revision", "clean-but-stale"), ("suite_id", "other.suite"),
                               ("build_succeeded", False), ("artifacts", []), ("configuration", "Debug")):
            changed = {**receipt, field: invalid}
            path.write_text(json.dumps(changed), encoding="utf-8")
            with self.subTest(field=field):
                _, error = runner.receipt_check(self.ws, suite(self.entry), self.source)
                self.assertIsNotNone(error)

    def test_receipted_binary_changed_after_build_is_blocked_without_large_hash(self):
        _, _, binary = self.make_receipt()
        binary.write_bytes(b"different-sized replacement")
        _, error = runner.receipt_check(self.ws, suite(self.entry), self.source)
        self.assertIn("changed after", error)

    def test_native_revision_changed_after_plan_never_launches(self):
        self.make_receipt()
        target = suite(self.entry)
        item = runner.plan_suite(self.ws, target, self.source)
        self.assertTrue(item["ready"])
        with patch.object(runner, "source_record", return_value={**self.source, "code_revision": "changed-after-plan"}), \
                patch.object(runner.subprocess, "run", side_effect=AssertionError("Stale native test cannot launch")):
            result = runner.execute_suite(self.ws, target, item, self.ws.path("paths.build", "fixture-run"), self.source)
        self.assertEqual(result["status"], "blocked")

    def test_build_output_cannot_overlap_inputs_source_install_or_legacy(self):
        for value in ("inputs", "scripts", str(self.ws.path("legacy.root")), "."):
            self.ws.values["paths"]["build"] = value
            with self.subTest(value=value), self.assertRaises(ValueError):
                runner.safe_output(self.ws, self.ws.path("paths.build", "results.json"))
        self.ws.values["paths"]["build"] = "build"
        self.ws.values["test"]["be_install_dir"] = str(self.ws.path("paths.build"))
        with self.assertRaisesRegex(ValueError, "retained"):
            runner.safe_output(self.ws, self.ws.path("paths.build", "results.json"))

    def test_output_escape_and_overlapping_temp_are_rejected(self):
        with self.assertRaises(ValueError):
            runner.safe_output(self.ws, self.ws.root / "results.json")
        self.ws.values["paths"]["temp"] = "build/tmp"
        with self.assertRaisesRegex(ValueError, "distinct"):
            runner.safe_output(self.ws, self.ws.path("paths.build", "tests/results.json"))

    def test_plan_is_not_a_passing_test_and_incompatible_hosts_are_skipped(self):
        with patch.object(runner.subprocess, "run", side_effect=AssertionError("Plan cannot launch")):
            planned = runner.plan_suite(self.ws, suite(), self.source)
            self.assertTrue(planned["ready"])
            self.assertEqual(planned["status"], "not_run")
            skipped = runner.plan_suite(self.ws, suite(host_platform=["Linux"]), self.source, host="Windows")
            self.assertEqual(skipped["status"], "skipped")

    def test_ctest_cannot_discover_old_or_unreceipted_binaries(self):
        binary = self.ws.path("paths.build", "native/fixture.exe")
        runner.validate_ctest_discovery({"tests": [{"command": [str(binary)]}]}, [binary])
        for tests in ([], [{"command": [str(self.ws.root.parent / "old.exe")]}],
                      [{"command": [str(binary), "--unregistered-argument"]}]):
            with self.subTest(tests=tests), self.assertRaises(ValueError):
                runner.validate_ctest_discovery({"tests": tests}, [binary])

    def test_current_and_old_game_versions_are_distinct_from_bem_formats(self):
        item = runner.plan_suite(self.ws, suite(status="version_limited", game_version=["pre-1.5.3"]), self.source)
        self.assertEqual(item["status"], "blocked")
        self.ws.values["game"]["version"] = "pre-1.5.3"
        item = runner.plan_suite(self.ws, suite(status="version_limited", game_version=["pre-1.5.3"]), self.source)
        self.assertEqual(item["status"], "not_run")
        self.assertEqual(item["input"]["game_version"], "pre-1.5.3")
        generic = runner.plan_suite(self.ws, suite(game_version=["any"]), self.source)
        self.assertEqual(generic["status"], "not_run")

    def test_empty_skipped_failed_and_partial_unittest_results_are_distinct(self):
        class Empty(unittest.TestCase):
            def runTest(self):
                pass
        case = Empty()
        result = unittest.TestResult()
        self.assertEqual(summarize(result)["status"], "blocked")
        result.testsRun = 1
        result.addSkip(case, "dependency absent")
        self.assertEqual(summarize(result)["status"], "skipped")
        result.testsRun = 2
        report = summarize(result)
        self.assertEqual(report["status"], "passed")
        self.assertEqual(report["skipped"], 1)
        result.unexpectedSuccesses.append(case)
        self.assertEqual(summarize(result)["status"], "failed")


if __name__ == "__main__":
    unittest.main()
