"""Shared developer workspace configuration. Does not change runtime settings."""
from __future__ import annotations
import argparse
import copy
import hashlib
import json
import os
import shutil
import subprocess
import sys
import tempfile
from pathlib import Path
from typing import Any
sys.dont_write_bytecode = True

REPO_ROOT = Path(__file__).resolve().parents[1]
PATH_PREFIXES = ("paths.", "resource_update.", "documents.")
COMMAND_KEYS = {"tools.python", "tools.cmake", "tools.dotnet", "tools.iscc", "tools.node", "tools.npm"}


def merge(base: dict, override: dict) -> dict:
    result = copy.deepcopy(base)
    for key, value in override.items():
        if isinstance(value, dict) and isinstance(result.get(key), dict):
            result[key] = merge(result[key], value)
        else:
            result[key] = copy.deepcopy(value)
    return result


class Workspace:
    def __init__(self, root: Path, values: dict, config_file: Path | None):
        self.root = root.resolve()
        self.values = values
        self.config_file = config_file
        if values.get("schema_version") != 1:
            raise ValueError("Unsupported workspace configuration schema")

    def get(self, key: str, default: Any = None):
        value = self.values
        for part in key.split("."):
            if not isinstance(value, dict) or part not in value:
                return default
            value = value[part]
        return value

    def path(self, key: str, *parts: str, required: bool = True) -> Path | None:
        value = self.get(key)
        if value is None or value == "":
            if required:
                raise ValueError(f"Workspace path is not configured: {key}")
            return None
        if not isinstance(value, str):
            raise ValueError(f"Workspace path must be a string: {key}")
        path = Path(os.path.expandvars(os.path.expanduser(value)))
        path = path if path.is_absolute() else self.root / path
        return path.joinpath(*parts).resolve()

    def command(self, key: str, required: bool = True) -> str | None:
        value = self.get(key)
        if not value:
            if required:
                raise ValueError(f"Workspace command is not configured: {key}")
            return None
        if not isinstance(value, str):
            raise ValueError(f"Workspace command must be a string: {key}")
        if any(char in value for char in ("/", "\\")):
            return str(self.path(key))
        return shutil.which(value) or value

    def ensure(self, key: str) -> Path:
        path = self.path(key)
        path.mkdir(parents=True, exist_ok=True)
        return path

    def env(self) -> dict[str, str]:
        env = os.environ.copy()
        temp = str(self.ensure("paths.temp"))
        env.update(TEMP=temp, TMP=temp, TMPDIR=temp,
                   PYTHONDONTWRITEBYTECODE="1",
                   BE_WORKSPACE_ROOT=str(self.root),
                   BE_WORKSPACE_BUILD_ROOT=str(self.path("paths.build")),
                   BE_WORKSPACE_TEMP_ROOT=temp,
                   BE_WORKSPACE_ANDROID_SDK=str(self.path("tools.android_sdk")),
                   BE_WORKSPACE_DOBBY_ROOT=str(self.path("tools.android_dobby")))
        if self.config_file:
            env["BE_WORKSPACE_CONFIG"] = str(self.config_file)
        return env

    def document(self, filename: str) -> Path:
        catalog = self.root / "config/documents.json"
        mapping = json.loads(catalog.read_text(encoding="utf-8-sig")) if catalog.exists() else {}
        if isinstance(mapping, dict) and "documents" in mapping:
            mapping = mapping["documents"]
        entry = mapping.get(filename) if isinstance(mapping, dict) else None
        if isinstance(entry, dict):
            entry = entry.get("path")
        return (self.root / entry).resolve() if entry else self.root / "docs" / filename

    def clean_targets(self) -> list[Path]:
        protected = [self.root / name for name in (".git", "legacy", "native", "ui", "android", "web", "config", "docs", "scripts", "tools", "resources", "manifests")]
        if self.config_file:
            protected.append(self.config_file)
        for key in ("paths.inputs", "paths.research", "paths.releases", "paths.toolchains", "paths.generated", "legacy.root", "game.install_dir", "test.be_install_dir"):
            value = self.path(key, required=False)
            if value:
                protected.append(value)
        targets = []
        allowed = {"paths.temp", "paths.build", "paths.cache"}
        for key in self.get("cleanup.keys", []):
            if key not in allowed:
                raise ValueError(f"Not a cleanable configuration key: {key}")
            target = self.path(key)
            if target == self.root or self.root not in target.parents:
                raise ValueError(f"Cleanup target must be inside the active workspace: {target}")
            for area in protected:
                area = area.resolve()
                if target == area or target in area.parents or area in target.parents:
                    raise ValueError(f"Cleanup target overlaps retained data: {target}")
            targets.append(target)
        if len(set(targets)) != len(targets) or any(a in b.parents or b in a.parents for i, a in enumerate(targets) for b in targets[i+1:]):
            raise ValueError("Cleanup directories must be distinct and non-overlapping")
        return targets

    def resolved(self) -> dict:
        result = copy.deepcopy(self.values)
        def visit(obj, prefix=""):
            for key, value in list(obj.items()):
                dotted = f"{prefix}.{key}".strip(".")
                if isinstance(value, dict):
                    visit(value, dotted)
                elif isinstance(value, str) and (dotted.startswith(PATH_PREFIXES) or dotted.startswith("tools.") and dotted not in COMMAND_KEYS or dotted in {"legacy.root", "game.install_dir", "test.be_install_dir"}):
                    obj[key] = str(self.path(dotted))
                elif dotted in COMMAND_KEYS and value:
                    obj[key] = self.command(dotted)
        visit(result)
        result["repo_root"] = str(self.root)
        result["config_file"] = str(self.config_file) if self.config_file else None
        return result


def load_workspace(config_path: str | Path | None = None, repo_root: str | Path | None = None) -> Workspace:
    root = Path(repo_root or REPO_ROOT).resolve()
    defaults = root / "config/workspace.defaults.json"
    values = json.loads(defaults.read_text(encoding="utf-8-sig"))
    selected = config_path or os.environ.get("BE_WORKSPACE_CONFIG")
    local = Path(selected).expanduser() if selected else root / "config/workspace.local.json"
    if not local.is_absolute():
        local = root / local
    if local.exists():
        values = merge(values, json.loads(local.read_text(encoding="utf-8-sig")))
    elif selected:
        raise FileNotFoundError(f"Explicit workspace configuration does not exist: {local}")
    if not values.get("game", {}).get("install_dir") and (root / "config/game-discovery.json").exists():
        from game_discovery import discover_game
        directory, provenance = discover_game(root)
        if directory:
            values.setdefault("game", {})["install_dir"] = str(directory)
            values["game"]["discovery_source"] = provenance
    workspace = Workspace(root, values, local.resolve() if local.exists() else None)
    workspace.clean_targets()
    return workspace


def write_if_changed(path: str | Path, content: bytes | str) -> bool:
    path = Path(path)
    data = content.encode("utf-8") if isinstance(content, str) else content
    if path.is_file() and path.read_bytes() == data:
        return False
    path.parent.mkdir(parents=True, exist_ok=True)
    with tempfile.NamedTemporaryFile(dir=path.parent, prefix=".workspace-write-", delete=False) as stream:
        temporary = Path(stream.name)
        stream.write(data)
    try:
        os.replace(temporary, path)
    finally:
        temporary.unlink(missing_ok=True)
    return True


def producer_key(snapshot: Any, tool_revision: str, parameters: dict) -> str:
    return hashlib.sha256(json.dumps({"snapshot": snapshot, "tool_revision": tool_revision, "parameters": parameters}, ensure_ascii=False, sort_keys=True, separators=(",", ":")).encode()).hexdigest()


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("command", choices=("resolve", "path", "clean-plan", "check", "test-target"))
    parser.add_argument("key", nargs="?")
    parser.add_argument("--config", type=Path)
    args = parser.parse_args()
    workspace = load_workspace(args.config)
    if args.command == "path":
        if not args.key:
            parser.error("path requires a configuration key")
        print(workspace.path(args.key))
    elif args.command == "clean-plan":
        print(json.dumps({"repo_root": str(workspace.root), "targets": [str(path) for path in workspace.clean_targets()]}, ensure_ascii=False))
    elif args.command == "test-target":
        target = workspace.path("test.be_install_dir")
        print(json.dumps({"be_install_dir": str(target), "exists": target.is_dir(), "deployment_performed": False}, ensure_ascii=False))
    elif args.command == "check":
        print(json.dumps({"schema_version": 1, "repo_root": str(workspace.root), "cleanup_targets": [str(path) for path in workspace.clean_targets()], "configuration_valid": True}, ensure_ascii=False))
    else:
        print(json.dumps(workspace.resolved(), ensure_ascii=False))


if __name__ == "__main__":
    main()
