#!/usr/bin/env python3
"""List, plan, or run registered workspace suites without building or deploying."""
from __future__ import annotations

import argparse
from collections import Counter
from datetime import datetime, timezone
import hashlib
import json
import os
from pathlib import Path
import platform
import re
import shutil
import subprocess
import sys
import time

sys.dont_write_bytecode = True
from workspace_config import load_workspace, producer_key, write_if_changed

KINDS = {"static", "offline", "simulation", "game", "device"}
CATALOG_STATUSES = {"active", "version_limited", "replaced", "retired"}
RESULT_STATUSES = ("passed", "failed", "skipped", "blocked", "not_run")
ENTRY_TYPES = {"python_unittest", "python_script", "native", "ctest", "catalog_only"}
SOURCE_SCOPES = ("native", "android/app/src", "tools/CustomModel", "tools/FirstPersonProfiles", "scripts", "config", "ui")
SOURCE_SUFFIXES = {".c", ".cc", ".cpp", ".h", ".hpp", ".java", ".cs", ".py", ".ps1", ".sh", ".js", ".cmake", ".csproj", ".props"}


def utc_now():
    return datetime.now(timezone.utc).isoformat()


def inside(path: Path, area: Path) -> bool:
    return path == area or area in path.parents


def overlaps(a: Path, b: Path) -> bool:
    return inside(a, b) or inside(b, a)


def source_path(root: Path, value: str) -> Path:
    """Source references cannot escape into a sibling checkout or install."""
    if not isinstance(value, str) or not value or Path(value).is_absolute():
        raise ValueError(f"Expected repository-relative source path: {value!r}")
    result = (root / value).resolve()
    if not inside(result, root) or result == root:
        raise ValueError(f"Source path escapes repository: {value}")
    return result


def safe_output(ws, path: Path, key="paths.build") -> Path:
    """Validate resolved paths (including links) before mkdir or writing."""
    area = ws.path(key).resolve()
    target = path.resolve()
    if area == ws.root or not inside(area, ws.root) or not inside(target, area) or target == area:
        raise ValueError(f"Test output must be below {key} in the active workspace: {target}")
    protected = [ws.root / name for name in ("native", "android", "ui", "web", "config", "docs", "scripts", "tools", "resources", "manifests", ".git", "legacy")]
    for field in ("paths.inputs", "paths.research", "paths.releases", "paths.toolchains", "paths.generated", "legacy.root", "game.install_dir", "test.be_install_dir"):
        configured = ws.path(field, required=False)
        if configured:
            protected.append(configured)
    if any(overlaps(area, retained.resolve()) for retained in protected):
        raise ValueError(f"Test output overlaps retained data: {area}")
    other = ws.path("paths.temp" if key == "paths.build" else "paths.build")
    if overlaps(area, other.resolve()):
        raise ValueError("Configured test build and temp roots must be distinct")
    return target


def validate_registry(data, root: Path):
    if not isinstance(data, dict) or data.get("schema_version") != 1 or not isinstance(data.get("suites"), list):
        raise ValueError("Expected test catalog schema_version=1 and suites array")
    ids = set()
    required = {"id", "module", "kind", "platform", "game_version", "input", "prerequisite", "entry", "status", "replaced_by", "purpose"}
    for suite in data["suites"]:
        if not isinstance(suite, dict) or required - suite.keys():
            raise ValueError(f"Suite is missing fields: {suite}")
        sid = suite["id"]
        if not isinstance(sid, str) or not re.fullmatch(r"[a-z][a-z0-9_]*(?:\.[a-z0-9_]+)+", sid) or sid in ids:
            raise ValueError(f"Invalid or duplicate stable suite ID: {sid}")
        ids.add(sid)
        if not isinstance(suite["module"], str) or not suite["module"] or suite["kind"] not in KINDS or suite["status"] not in CATALOG_STATUSES:
            raise ValueError(f"Invalid module/kind/status: {sid}")
        for field in ("platform", "game_version", "host_platform"):
            values = suite.get(field, ["any"])
            if not isinstance(values, list) or not values or not all(isinstance(v, str) and v for v in values):
                raise ValueError(f"{sid}: {field} must be a nonempty string array")
        if not isinstance(suite["input"], dict) or not suite["input"].get("id") or not suite["input"].get("type"):
            raise ValueError(f"{sid}: input must identify its ID and type")
        if not isinstance(suite["prerequisite"], list):
            raise ValueError(f"{sid}: prerequisite must be an array")
        for dep in suite["prerequisite"]:
            if not isinstance(dep, dict) or dep.get("type") not in {"tool", "config_path", "config_value", "source", "manual"}:
                raise ValueError(f"{sid}: invalid prerequisite")
            if dep["type"] in {"tool", "config_path", "config_value"} and not isinstance(dep.get("key"), str):
                raise ValueError(f"{sid}: prerequisite needs a configuration key")
            if dep["type"] == "source":
                source_path(root, dep["path"])
        entry = suite["entry"]
        if not isinstance(entry, dict) or entry.get("type") not in ENTRY_TYPES:
            raise ValueError(f"{sid}: unsupported entry")
        if entry.get("cwd", ".") != ".":
            source_path(root, entry["cwd"])
        for value in entry.get("sources", []):
            source_path(root, value)
        if entry["type"] == "python_unittest":
            if not entry.get("modules") or not all(isinstance(m, str) and re.fullmatch(r"[a-zA-Z_]\w*(?:\.[a-zA-Z_]\w*)*", m) for m in entry["modules"]):
                raise ValueError(f"{sid}: unittest entry needs module names")
        if entry["type"] == "python_script":
            source_path(root, entry["script"])
        if entry["type"] == "catalog_only" and not entry.get("blocked_reason"):
            raise ValueError(f"{sid}: catalog_only needs a concrete blocked_reason")
        if entry["type"] in {"native", "ctest"} or entry.get("native_environment"):
            if not entry.get("receipt") or not entry.get("artifacts"):
                raise ValueError(f"{sid}: native execution needs receipt and exact artifact list")
            for field in ("receipt", "build_dir"):
                if entry.get(field) and (Path(entry[field]).is_absolute() or ".." in Path(entry[field]).parts):
                    raise ValueError(f"{sid}: {field} must be relative to paths.build")
            for artifact in entry["artifacts"]:
                if Path(artifact).is_absolute() or ".." in Path(artifact).parts:
                    raise ValueError(f"{sid}: artifact must be relative to paths.build")
            if entry["type"] == "native" and entry.get("executable") not in entry["artifacts"]:
                raise ValueError(f"{sid}: executable must be one of the receipted artifacts")
            if not set(entry.get("native_environment", {}).values()).issubset(entry["artifacts"]):
                raise ValueError(f"{sid}: native environment must use receipted artifacts")
        if suite["replaced_by"] is not None and (not isinstance(suite["replaced_by"], str) or suite["status"] != "replaced"):
            raise ValueError(f"{sid}: replaced_by is only valid for replaced suites")
        if suite["status"] == "replaced" and not suite["replaced_by"]:
            raise ValueError(f"{sid}: replaced suite needs successor ID")
    lookup = {suite["id"]: suite for suite in data["suites"]}
    for suite in data["suites"]:
        seen = {suite["id"]}
        next_id = suite["replaced_by"]
        while next_id:
            if next_id not in lookup or next_id in seen:
                raise ValueError(f"Missing or cyclic replaced_by: {suite['id']}")
            seen.add(next_id)
            next_id = lookup[next_id]["replaced_by"]
    return data["suites"]


def git_bytes(root, *arguments):
    result = subprocess.run(["git", "-C", str(root), *arguments], capture_output=True, check=True)
    return result.stdout


def source_record(root: Path):
    """Hash only changed small code/config files; never hash input assets/binaries."""
    record = {"commit": None, "code_revision": None, "revision_scheme": "workspace-tests-source-v1", "scopes": list(SOURCE_SCOPES), "changes": []}
    try:
        record["commit"] = git_bytes(root, "rev-parse", "HEAD").decode().strip()
        changed = git_bytes(root, "diff", "--no-ext-diff", "--no-renames", "--name-only", "-z", "HEAD", "--", *SOURCE_SCOPES)
        untracked = git_bytes(root, "ls-files", "--others", "--exclude-standard", "-z", "--", *SOURCE_SCOPES)
        names = sorted(set(os.fsdecode(name) for name in (changed + untracked).split(b"\0") if name))
        # scripts/* was historically ignored. Include current workspace helpers
        # explicitly until the parent adds tracking exceptions, without scanning
        # input or toolchain trees. These small files remain part of the revision.
        helpers = [*root.joinpath("scripts").glob("*.py"), *root.joinpath("scripts/tests").glob("*.py"),
                   root / "scripts/Workspace.ps1", root / "scripts/Test-Workspace.ps1"]
        names = sorted(set(names) | {path.relative_to(root).as_posix() for path in helpers if path.is_file()})
        for name in names:
            relative = Path(name)
            if relative.suffix.lower() not in SOURCE_SUFFIXES and relative.name != "CMakeLists.txt" and not (relative.parts[0] == "config" and relative.suffix == ".json"):
                continue
            path = source_path(root, name)
            if path.is_file() and path.stat().st_size > 2 * 1024 * 1024:
                raise ValueError(f"Changed code exceeds bounded revision check: {name}")
            digest = hashlib.sha256(path.read_bytes()).hexdigest() if path.is_file() else "deleted"
            record["changes"].append({"path": name, "sha256": digest})
        record["code_revision"] = producer_key({"commit": record["commit"], "changes": record["changes"]}, record["revision_scheme"], {})
    except (OSError, ValueError, subprocess.CalledProcessError) as exc:
        record["error"] = str(exc)
    return record


def config_record(ws):
    files = [ws.root / "config/workspace.defaults.json"]
    if getattr(ws, "config_file", None):
        files.append(ws.config_file)
    return {"files": [{"path": str(path), "sha256": hashlib.sha256(path.read_bytes()).hexdigest()} for path in files],
            "effective_key": producer_key("workspace-config", "1", ws.values),
            "build": str(ws.path("paths.build")), "temp": str(ws.path("paths.temp")),
            "be_install_dir": str(ws.path("test.be_install_dir", required=False)) if ws.get("test.be_install_dir") else None,
            "android_serial": ws.get("test.android_serial"), "game_version": ws.get("game.version"),
            "snapshot": ws.get("game.snapshot")}


def receipt_check(ws, suite, source):
    entry = suite["entry"]
    receipt_path = safe_output(ws, ws.path("paths.build", entry["receipt"]))
    if not source.get("commit") or not source.get("code_revision"):
        return None, "Current source revision is unavailable; native binaries cannot be trusted"
    if not receipt_path.is_file():
        return None, f"Missing build receipt: {receipt_path}"
    try:
        receipt = json.loads(receipt_path.read_text(encoding="utf-8-sig"))
        expected = {"schema_version": 1, "suite_id": suite["id"], "source_commit": source["commit"], "code_revision": source["code_revision"], "build_succeeded": True}
        if any(receipt.get(key) != value for key, value in expected.items()):
            return None, "Build receipt does not match this suite/current commit/code revision/success"
        if not receipt.get("tool_revision") or not receipt.get("build_command") or not receipt.get("built_at"):
            return None, "Build receipt needs tool_revision, build_command and built_at"
        if entry.get("configuration") and receipt.get("configuration") != entry["configuration"]:
            return None, "Build receipt configuration differs from catalog"
        artifacts = receipt.get("artifacts")
        if not isinstance(artifacts, list) or len(artifacts) != len(entry["artifacts"]) or {a.get("path") for a in artifacts if isinstance(a, dict)} != set(entry["artifacts"]):
            return None, "Build receipt must identify the exact registered artifacts"
        for artifact in artifacts:
            path = safe_output(ws, ws.path("paths.build", artifact["path"]))
            if not path.is_file():
                return None, f"Missing receipted artifact: {path}"
            stat = path.stat()
            if artifact.get("size") != stat.st_size or artifact.get("mtime_ns") != stat.st_mtime_ns:
                return None, f"Artifact changed after receipted build: {path}"
        return {"path": str(receipt_path), "sha256": hashlib.sha256(receipt_path.read_bytes()).hexdigest(), "receipt": receipt}, None
    except (OSError, ValueError, TypeError, KeyError, AttributeError) as exc:
        return None, f"Invalid build receipt: {exc}"


def tool_path(ws, key):
    if key == "tools.ctest" and not ws.get(key):
        cmake = Path(ws.command("tools.cmake"))
        adjacent = cmake.with_name("ctest.exe" if platform.system() == "Windows" else "ctest")
        return str(adjacent) if adjacent.is_file() else shutil.which("ctest")
    command = ws.command(key)
    return command if Path(command).is_file() else shutil.which(command)


def input_record(ws, suite):
    value = dict(suite["input"])
    if value.get("config_key"):
        configured = ws.path(value["config_key"], required=False)
        value["path"] = str(configured) if configured else None
    elif value.get("path"):
        value["path"] = str(source_path(ws.root, value["path"]))
    value["game_version"] = ws.get("game.version")
    value["snapshot"] = ws.get("game.snapshot")
    return value


def plan_suite(ws, suite, source, *, allow_game=False, host=None, result_dir=None):
    host = host or platform.system()
    entry = suite["entry"]
    item = {"id": suite["id"], "module": suite["module"], "kind": suite["kind"], "catalog_status": suite["status"],
            "status": "not_run", "ready": False, "reason": "Plan only; no suite process started", "command": [],
            "cwd": str(ws.root if entry.get("cwd", ".") == "." else source_path(ws.root, entry["cwd"])),
            "input": input_record(ws, suite), "entry": entry, "receipt": None, "case_result": None}
    def stop(status, reason):
        item.update(status=status, reason=reason)
        return item
    if suite["status"] in {"retired", "replaced"}:
        return stop("skipped", f"Catalog status {suite['status']}; replaced_by={suite['replaced_by']}")
    if host not in suite.get("host_platform", ["Windows", "Linux", "Darwin"]) and "any" not in suite.get("host_platform", []):
        return stop("skipped", f"Suite cannot execute on host {host}")
    if suite["kind"] in {"game", "device"}:
        if not allow_game:
            return stop("blocked", "Real game/device execution requires explicit --allow-game")
        if not ws.get("test.be_install_dir"):
            return stop("blocked", "Configure test.be_install_dir; source root is never a game target")
        install = ws.path("test.be_install_dir")
        if overlaps(install, ws.root) or (ws.path("legacy.root", required=False) and overlaps(install, ws.path("legacy.root"))):
            return stop("blocked", "test.be_install_dir overlaps source/legacy")
        if not install.is_dir():
            return stop("blocked", "Configured test.be_install_dir does not exist")
        item["game_target"] = str(install)
        if suite["kind"] == "device" or "Android" in suite["platform"]:
            if not ws.get("test.android_serial"):
                return stop("blocked", "Configure explicit test.android_serial; no implicit adb device")
            item["android_serial"] = ws.get("test.android_serial")
    if entry["type"] == "catalog_only":
        item["reference_entry"] = entry.get("reference")
        return stop("blocked", entry["blocked_reason"])
    for dep in suite["prerequisite"]:
        kind = dep["type"]
        if kind == "tool" and not tool_path(ws, dep["key"]):
            return stop("blocked", f"Missing tool {dep['key']}")
        if kind == "config_value" and not ws.get(dep["key"]):
            return stop("blocked", f"Missing configuration {dep['key']}")
        if kind == "config_path":
            path = ws.path(dep["key"], required=False)
            if path is None or not path.exists() or dep.get("is_file") and not path.is_file():
                return stop("blocked", f"Missing configured input/dependency {dep['key']}")
        if kind == "source" and not source_path(ws.root, dep["path"]).is_file():
            return stop("blocked", f"Missing source/fixture {dep['path']}")
        if kind == "manual":
            return stop("blocked", dep.get("reason", "Manual precondition is not verified"))
    for name in entry.get("sources", []):
        if not source_path(ws.root, name).is_file():
            return stop("blocked", f"Missing test entry source {name}")
    if not Path(item["cwd"]).is_dir():
        return stop("blocked", f"Missing working directory: {item['cwd']}")
    if suite["status"] == "version_limited" and ws.get("game.version") not in suite["game_version"]:
        return stop("blocked", "Configured game.version does not match this version-limited suite")
    if entry["type"] in {"native", "ctest"} or entry.get("native_environment"):
        item["receipt"], error = receipt_check(ws, suite, source)
        if error:
            return stop("blocked", error)
    result_dir = result_dir or safe_output(ws, ws.path("paths.build", "tests", "preview"))
    if entry["type"] == "python_unittest":
        result = safe_output(ws, result_dir / (suite["id"] + ".cases.json"))
        item["case_result"] = str(result)
        item["command"] = [ws.command("tools.python"), "-B", str(ws.root / "scripts/tests/run_python_suite.py"), "--result", str(result)]
        for module in entry["modules"]:
            item["command"] += ["--module", module]
    elif entry["type"] == "python_script":
        item["command"] = [ws.command("tools.python"), "-B", str(source_path(ws.root, entry["script"])), *entry.get("args", [])]
    elif entry["type"] == "native":
        item["command"] = [str(safe_output(ws, ws.path("paths.build", entry["executable"]))), *entry.get("args", [])]
    elif entry["type"] == "ctest":
        item["command"] = [tool_path(ws, "tools.ctest"), "--test-dir", str(safe_output(ws, ws.path("paths.build", entry["build_dir"]))), "-C", entry["configuration"], "--output-on-failure", "--no-tests=error", "-R", entry["test_regex"]]
    item["ready"] = True
    return item


def validate_ctest_discovery(document, artifact_paths):
    tests = document.get("tests")
    if not isinstance(tests, list) or not tests:
        raise ValueError("CTest discovered no registered tests")
    discovered = []
    for test in tests:
        command = test.get("command")
        if not isinstance(command, list) or len(command) != 1:
            raise ValueError("CTest command differs from the registered no-argument native tests")
        discovered.append(str(Path(command[0]).resolve()))
    expected = [str(path.resolve()) for path in artifact_paths]
    if Counter(discovered) != Counter(expected):
        raise ValueError("CTest discovery does not match the exact receipted artifact set")


def execute_suite(ws, suite, item, run_dir, source):
    if not item["ready"]:
        return item
    # Recheck immediately before launch so a stale receipt cannot pass via a plan.
    if item["receipt"]:
        current = source_record(ws.root)
        checked, error = receipt_check(ws, suite, current)
        if error or checked["sha256"] != item["receipt"]["sha256"] or current["code_revision"] != source["code_revision"]:
            item.update(status="blocked", ready=False, reason=error or "Source/receipt changed since plan")
            return item
    temp = safe_output(ws, ws.path("paths.temp", "workspace-tests", run_dir.name, suite["id"]), "paths.temp")
    temp.mkdir(parents=True, exist_ok=True)
    env = ws.env()
    env.update(TEMP=str(temp), TMP=str(temp), TMPDIR=str(temp), PYTHONDONTWRITEBYTECODE="1",
               BE_TEST_OUTPUT_ROOT=str(run_dir), BE_TEST_INSTALL_DIR=str(ws.path("test.be_install_dir", required=False) or ""),
               BE_TEST_ANDROID_SERIAL=str(ws.get("test.android_serial") or ""))
    # Optional native hooks may not inherit an unreceipted executable from a shell.
    env.pop("BEM_VALIDATOR", None)
    for name, artifact in suite["entry"].get("native_environment", {}).items():
        env[name] = str(safe_output(ws, ws.path("paths.build", artifact)))
    logfile = safe_output(ws, run_dir / (suite["id"] + ".log"))
    started = time.monotonic()
    try:
        if suite["entry"]["type"] == "ctest":
            discovery = subprocess.run([*item["command"], "--show-only=json-v1"], cwd=item["cwd"], env=env,
                                       capture_output=True, timeout=30, check=False)
            if discovery.returncode:
                item.update(status="blocked", reason="CTest discovery could not verify receipted tests")
                return item
            artifacts = [safe_output(ws, ws.path("paths.build", name)) for name in suite["entry"]["artifacts"]]
            try:
                validate_ctest_discovery(json.loads(discovery.stdout), artifacts)
            except (ValueError, TypeError, KeyError) as exc:
                item.update(status="blocked", reason=f"CTest discovery blocked: {exc}")
                return item
        with logfile.open("wb") as stream:
            result = subprocess.run(item["command"], cwd=item["cwd"], env=env, stdout=stream, stderr=subprocess.STDOUT,
                                    timeout=suite.get("timeout_seconds", 300), check=False)
        item.update(exit_code=result.returncode, status="passed" if result.returncode == 0 else "failed", reason="Suite process exited")
        if item["case_result"]:
            cases = json.loads(Path(item["case_result"]).read_text(encoding="utf-8"))
            if cases.get("status") not in RESULT_STATUSES:
                raise ValueError("Unrecognized Python worker result")
            item["cases"] = cases
            item["status"] = cases["status"] if result.returncode == 0 or cases["status"] == "failed" else "failed"
            item["reason"] = cases.get("reason", "Python unittest result")
    except subprocess.TimeoutExpired:
        item.update(status="failed", reason="Suite timeout (no passing result)")
    except OSError as exc:
        item.update(status="failed" if item.get("exit_code") is not None else "blocked", reason=f"Suite launch/result I/O failed: {exc}")
    except (ValueError, KeyError) as exc:
        item.update(status="failed", reason=f"Invalid/missing worker result: {exc}")
    item.update(log=str(logfile), duration_seconds=round(time.monotonic() - started, 3))
    return item


def parser():
    value = argparse.ArgumentParser(description=__doc__)
    value.add_argument("--workspace-config", type=Path)
    value.add_argument("--registry", type=Path, help="Alternate catalog (primarily for pure simulations)")
    mode = value.add_mutually_exclusive_group()
    mode.add_argument("--list", action="store_true")
    mode.add_argument("--plan", action="store_true")
    for name in ("module", "platform", "kind", "suite", "game-version"):
        value.add_argument("--" + name, action="append", help="Repeatable exact filter; different filters are combined")
    value.add_argument("--allow-game", action="store_true")
    value.add_argument("--output", type=Path, help="Result JSON path below configured paths.build")
    return value


def matches(suite, args):
    for field in ("module", "kind"):
        selected = getattr(args, field)
        if selected and suite[field] not in selected:
            return False
    if args.suite and suite["id"] not in args.suite:
        return False
    for field, selected in (("platform", args.platform), ("game_version", args.game_version)):
        if selected and "any" not in suite[field] and not set(selected).intersection(suite[field]):
            return False
    return True


def main(argv=None):
    args = parser().parse_args(argv)
    try:
        ws = load_workspace(args.workspace_config)
        # Validate both writable roots even in list/plan; neither mode creates temp.
        safe_output(ws, ws.path("paths.build", "tests", "probe"))
        safe_output(ws, ws.path("paths.temp", "workspace-tests", "probe"), "paths.temp")
        catalog = args.registry.resolve() if args.registry else ws.root / "config/test-suites.json"
        catalog_bytes = catalog.read_bytes()
        suites = validate_registry(json.loads(catalog_bytes.decode("utf-8-sig")), ws.root)
        selected = [suite for suite in suites if matches(suite, args)]
        source = source_record(ws.root)
        configuration = config_record(ws)
        run_id = datetime.now(timezone.utc).strftime("%Y%m%dT%H%M%S%fZ") + f"-{os.getpid()}"
        mode = "list" if args.list else "plan" if args.plan else "run"
        output = args.output
        if output:
            output = output if output.is_absolute() else ws.root / output
        else:
            output = ws.path("paths.build", "tests", "runs", run_id, "results.json")
        output = safe_output(ws, output)
        run_dir = output.parent
        items = []
        for suite in selected:
            if mode == "list":
                items.append({**suite, "catalog_status": suite["status"], "status": "not_run", "reason": "Catalog listing; no prerequisite or suite execution"})
            else:
                try:
                    item = plan_suite(ws, suite, source, allow_game=args.allow_game, result_dir=run_dir)
                except (OSError, ValueError, TypeError, KeyError) as exc:
                    item = {"id": suite["id"], "module": suite["module"], "kind": suite["kind"], "catalog_status": suite["status"],
                            "status": "blocked", "ready": False, "reason": f"Invalid suite dependency/configuration: {exc}",
                            "command": [], "input": input_record(ws, suite)}
                if mode == "run":
                    run_dir.mkdir(parents=True, exist_ok=True)
                    item = execute_suite(ws, suite, item, run_dir, source)
                items.append(item)
        counts = Counter(item["status"] for item in items)
        report = {"schema_version": 1, "mode": mode, "recorded_at": utc_now(), "repo_root": str(ws.root),
                  "source": source, "config": configuration, "registry": {"path": str(catalog), "sha256": hashlib.sha256(catalog_bytes).hexdigest()},
                  "host": platform.system(), "allow_game": args.allow_game,
                  "filters": {name: getattr(args, name) for name in ("module", "platform", "kind", "suite", "game_version")},
                  "summary": {state: counts[state] for state in RESULT_STATUSES}, "suites": items}
        report["selection_empty"] = not selected
        # List/plan are successful inspections even when dependencies are blocked.
        code = 2 if not selected else (1 if counts["failed"] else 2 if counts["blocked"] else 0) if mode == "run" else 0
        report["exit_code"] = code
        payload = json.dumps(report, ensure_ascii=False, indent=2) + "\n"
        write_if_changed(output, payload)
        print(payload)
        print(f"Result JSON: {output}", file=sys.stderr)
        return code
    except (OSError, ValueError, TypeError, KeyError, subprocess.CalledProcessError) as exc:
        print(f"Workspace tests blocked: {exc}", file=sys.stderr)
        return 2


if __name__ == "__main__":
    raise SystemExit(main())
