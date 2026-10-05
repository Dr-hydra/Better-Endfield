"""Read-only game location discovery from Windows registration records."""
from __future__ import annotations
import json
import os
from pathlib import Path


def game_directory(candidate: str, executable: str = "Endfield.exe") -> Path | None:
    if not candidate or not isinstance(candidate, str):
        return None
    text = os.path.expandvars(candidate.strip().strip('"'))
    if ".exe," in text.lower():
        text = text.rsplit(",", 1)[0].strip().strip('"')
    path = Path(text)
    if path.name.casefold() == executable.casefold() and path.is_file():
        return path.parent.resolve()
    if path.is_dir() and (path/executable).is_file():
        return path.resolve()
    return None


def registered_candidates(rules: dict):
    if os.name != "nt":
        return
    import winreg
    views = (winreg.KEY_WOW64_64KEY, winreg.KEY_WOW64_32KEY)
    hives = ((winreg.HKEY_CURRENT_USER, "HKCU"), (winreg.HKEY_LOCAL_MACHINE, "HKLM"))
    def values(hive, key_path, view):
        try:
            with winreg.OpenKey(hive, key_path, 0, winreg.KEY_READ | view) as key:
                return [winreg.EnumValue(key, i) for i in range(winreg.QueryInfoKey(key)[1])]
        except OSError:
            return []
    def children(hive, key_path, view):
        try:
            with winreg.OpenKey(hive, key_path, 0, winreg.KEY_READ | view) as key:
                return [winreg.EnumKey(key, i) for i in range(winreg.QueryInfoKey(key)[0])]
        except OSError:
            return []
    for hive, name in hives:
        for view in views:
            for key, value, kind in values(hive, rules["app_paths"], view):
                if isinstance(value, str):
                    yield value, name + "\\" + rules["app_paths"]
            base = rules["uninstall_key"]
            for product in children(hive, base, view):
                entry = {key: value for key, value, _ in values(hive, base+"\\"+product, view)}
                display = str(entry.get("DisplayName", ""))
                if not any(word.casefold() in display.casefold() for word in rules["product_names"]):
                    continue
                for key in (*rules["path_values"], "DisplayIcon"):
                    if isinstance(entry.get(key), str):
                        yield entry[key], name+"\\"+base+"\\"+product
            for publisher in rules["publisher_keys"]:
                keys = [publisher]
                keys += [publisher+"\\"+child for child in children(hive, publisher, view)]
                for key_path in keys:
                    for key, value, _ in values(hive, key_path, view):
                        if key in rules["path_values"] and isinstance(value, str):
                            yield value, name+"\\"+key_path
    for key_path in rules["cached_executables"]:
        for key, _, _ in values(winreg.HKEY_CURRENT_USER, key_path, winreg.KEY_WOW64_64KEY):
            candidate = key
            for suffix in (".FriendlyAppName", ".ApplicationCompany"):
                if candidate.endswith(suffix):
                    candidate = candidate[:-len(suffix)]
            if Path(candidate).name.casefold() == rules["executable"].casefold():
                yield candidate, "HKCU\\"+key_path


def discover_game(repo_root: Path, candidates=None) -> tuple[Path | None, str | None]:
    rules = json.loads((repo_root/"config/game-discovery.json").read_text(encoding="utf-8-sig"))
    if rules.get("schema_version") != 1:
        raise ValueError("Unsupported game discovery rules")
    for value, provenance in (registered_candidates(rules) if candidates is None else candidates):
        directory = game_directory(value, rules["executable"])
        if directory:
            return directory, provenance
    return None, None
