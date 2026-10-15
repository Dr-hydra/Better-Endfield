"""Prepare and control the five login-screen BEM metadata captures.

plan only writes capture-targets.json beside this script. All other commands are
explicit runtime operations; arm requires Endfield.exe to be closed. This tool
never starts/stops a game process, deploys binaries, or changes runtime.ini.
"""
import argparse
import copy
import csv
import datetime as dt
import io
import json
import os
from pathlib import Path
import re
import subprocess
import sys
import uuid

HERE = Path(__file__).resolve().parent
ROOT = HERE.parents[4]
PRESETS = ROOT / "resources/generated/model/character-presets.json"
MANIFEST = ROOT / "inputs/1.6.3/Windows/baseline/Bundles/Windows/manifest.json"
SWEEP = ROOT / "tools/CustomModel/runtime_sweep.py"
MODEL = "betterendfieldnext.model"
CUSTOM = "betterendfieldnext.custom_model"
CHARACTERS = ["chr_0036_jsspsi", "chr_0037_minghe", "chr_0037_minghe02", "chr_0037_minghe03"]
HEALTHY = {"ready", "receiving_resources", "captured"}


def read_json(path):
    return json.loads(path.read_text(encoding="utf-8-sig"))


def atomic_bytes(path, data, previous=None):
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_name(path.name + "." + uuid.uuid4().hex + ".tmp")
    try:
        temporary.write_bytes(data)
        if previous is not None and path.read_bytes() != previous:
            raise ValueError(f"File changed during update; retry: {path}")
        os.replace(temporary, path)
    finally:
        temporary.unlink(missing_ok=True)


def write_json(path, data):
    atomic_bytes(path, (json.dumps(data, ensure_ascii=False, indent=2) + "\n").encode("utf-8"))


def build_targets():
    presets = read_json(PRESETS)
    manifest = read_json(MANIFEST)
    plan = read_json(HERE / "capture-plan.json")
    version = manifest["Version"]
    if presets["manifestVersion"] != version or plan["request"]["manifest_version"] != version:
        raise ValueError("Preset, manifest and capture-plan versions differ")
    characters = {item["id"]: item for item in presets["characters"]}
    targets = []
    for character_id in CHARACTERS:
        character = characters[character_id]
        action = next(a for a in character["actions"] if a["id"] == character["defaultActionId"])
        targets.append(dict(target=character_id + "_postmodel", character_id=character_id,
            variant="world", model=character["model"], sitLoop=character["sitLoop"],
            sitSpecial=character["sitSpecial"], sitToWalk=character["sitToWalk"], defaultAction=action))
    ui_root = "chr_0036_jsspsi_uimodel"
    ui_path = plan["request"]["resources"][ui_root]["asset_path"]
    ui_asset = next(asset for asset in manifest["Assets"] if asset["path"] == ui_path)
    ui_target = copy.deepcopy(targets[0])
    ui_target.update(target=ui_root, variant="ui", model={"path": ui_path,
        "pathHash": f"0x{ui_asset['pathHashHead']:016X}"})
    targets.append(ui_target)
    if {t["target"] for t in targets} != set(plan["request"]["resources"]):
        raise ValueError("Target roots differ from capture plan")
    return dict(schema=1, game_version="1.6.3", manifest_version=version,
        sources={"presets": str(PRESETS), "manifest": str(MANIFEST),
            "capture_plan": str(HERE / "capture-plan.json")}, targets=targets)


def target_values(target):
    values = {"enabled": "true", "character": target["character_id"],
        "model_path": target["model"]["path"], "model_path_hash": target["model"]["pathHash"],
        "final_action": target["defaultAction"]["id"],
        "final_native_loop": str(target["defaultAction"].get("nativeLoop", False)).lower()}
    for prefix, field in (("sit_loop", "sitLoop"), ("sit_special", "sitSpecial"),
            ("sit_to_walk", "sitToWalk"), ("final", "defaultAction")):
        clip = target[field]
        values[prefix + "_path"] = clip["path"]
        values[prefix + "_path_hash"] = clip["pathHash"]
        values[prefix + "_label"] = clip.get("displayName", clip.get("id", ""))
    # The entire INI is atomically replaced: hooks must be enabled before launch.
    values["model_replacement_enabled"] = "true"
    runtime = ROOT / "build/bem-capture-1.6.3"
    return [(MODEL, key, value) for key, value in values.items()] + [
        (CUSTOM, "enabled", "true"),
        ("Loader", "install_root", str(runtime)), ("Loader", "load_host", "true"),
        ("Host", "modules_root", str(runtime / "modules")),
    ]


class IniLines:
    """Patch only requested keys, retaining BOM, comments, ordering and spacing."""
    def __init__(self, data):
        self.bom, self.encoding = b"", "utf-8"
        for bom, encoding in ((b"\xff\xfe", "utf-16-le"), (b"\xfe\xff", "utf-16-be"),
                (b"\xef\xbb\xbf", "utf-8")):
            if data.startswith(bom):
                self.bom, self.encoding = bom, encoding
                break
        self.lines = data[len(self.bom):].decode(self.encoding).splitlines(keepends=True)
        self.newline = "\r\n" if any(line.endswith("\r\n") for line in self.lines) else "\n"

    def locate(self, section, key):
        current = None
        found = []
        for index, line in enumerate(self.lines):
            match = re.match(r"^\s*\[([^]]+)\]", line)
            if match:
                current = match[1].strip().lower()
            elif current == section.lower() and re.match(r"^\s*" + re.escape(key) + r"\s*=", line, re.I):
                found.append(index)
        if len(found) > 1:
            raise ValueError(f"Duplicate INI key: [{section}] {key}")
        return found[0] if found else None

    def get_line(self, section, key):
        index = self.locate(section, key)
        return None if index is None else self.lines[index]

    def section_start(self, section):
        return next((i for i, text in enumerate(self.lines)
            if (match := re.match(r"^\s*\[([^]]+)\]", text))
            and match[1].strip().lower() == section.lower()), None)

    def ensure_section(self, section):
        start = self.section_start(section)
        if start is not None:
            return start
        if self.lines and not self.lines[-1].endswith(("\n", "\r")):
            raise ValueError("Cannot append section after a line without newline")
        self.lines.append("[" + section + "]" + self.newline)
        return len(self.lines) - 1

    def remove_created_empty_section(self, section, header):
        start = self.section_start(section)
        if start is None:
            return
        end = next((i for i in range(start + 1, len(self.lines))
            if re.match(r"^\s*\[", self.lines[i])), len(self.lines))
        # Retain a section if an external writer changed its header or added
        # settings/comments. Only our own now-empty section can be removed.
        if self.lines[start] == header and not any(line.strip() for line in self.lines[start + 1:end]):
            del self.lines[start:end]

    def set_line(self, section, key, line):
        index = self.locate(section, key)
        if index is not None:
            if line is None:
                del self.lines[index]
            else:
                self.lines[index] = line
            return
        if line is None:
            return
        start = self.ensure_section(section)
        end = next((i for i in range(start + 1, len(self.lines))
            if re.match(r"^\s*\[", self.lines[i])), len(self.lines))
        if end and not self.lines[end - 1].endswith(("\n", "\r")):
            raise ValueError("Cannot append key after a line without newline")
        self.lines.insert(end, line)

    def set_value(self, section, key, value):
        old = self.get_line(section, key)
        if old is None:
            line = key + "=" + value + self.newline
        else:
            prefix = re.match(r"^(\s*[^=]+?\s*=\s*)", old)[1]
            body = old[len(prefix):].rstrip("\r\n")
            comment = re.search(r"([ \t]+[;#].*)$", body)
            suffix = comment[1] if comment else ""
            newline = old[len(old.rstrip("\r\n")):]
            line = prefix + value + suffix + newline
        self.set_line(section, key, line)
        return line

    def bytes(self):
        return self.bom + "".join(self.lines).encode(self.encoding)


def game_pids():
    result = subprocess.run(["tasklist", "/FI", "IMAGENAME eq Endfield.exe", "/FO", "CSV", "/NH"],
        capture_output=True, text=True, errors="replace", check=True)
    return {int(row[1]) for row in csv.reader(io.StringIO(result.stdout))
        if len(row) > 1 and row[0].lower() == "endfield.exe"}


def sweep(command, state, extra=()):
    arguments = [sys.executable, str(SWEEP), command, "--catalog", state["catalog"],
        "--run", state["run"], "--output", str(Path(state["state_file"]).parent / "summary.json")]
    subprocess.run(arguments + list(extra), cwd=ROOT, check=True)


def apply_values(state, values):
    ini_path = Path(state["ini"])
    original = ini_path.read_bytes()
    document = IniLines(original)
    changes = state.setdefault("keys", {})
    for section, key, value in values:
        identity = section + "/" + key
        line = document.get_line(section, key)
        entry = changes.get(identity)
        if entry and line != entry["last_applied"]:
            raise ValueError(f"INI changed externally; refusing overwrite: {identity}")
        if entry is None:
            entry = changes[identity] = {"section": section, "key": key, "original": line}
        if document.section_start(section) is None:
            index = document.ensure_section(section)
            state.setdefault("created_sections", {})[section] = document.lines[index]
        entry["last_applied"] = document.set_value(section, key, value)
    # Persist recovery information before touching the live INI.
    write_json(Path(state["state_file"]), state)
    atomic_bytes(ini_path, document.bytes(), previous=original)


def load_state(run):
    if run is None:
        run = read_json(HERE / "active-capture.json")["run"]
    if not re.fullmatch(r"[A-Za-z0-9_-]{1,80}", run):
        raise ValueError("Invalid run ID")
    return read_json(HERE / "runs" / run / "control-state.json")


def restore(state):
    path = Path(state["ini"])
    original = path.read_bytes()
    document = IniLines(original)
    conflicts = []
    for identity, entry in state.get("keys", {}).items():
        if entry.get("restored"):
            continue
        line = document.get_line(entry["section"], entry["key"])
        if line == entry["original"]:
            entry["restored"] = True
        elif line == entry["last_applied"]:
            document.set_line(entry["section"], entry["key"], entry["original"])
            entry["restored"] = True
        else:
            conflicts.append(identity)
    for section, header in state.get("created_sections", {}).items():
        document.remove_created_empty_section(section, header)
    atomic_bytes(path, document.bytes(), previous=original)
    state["phase"] = "restore_conflicts" if conflicts else "restored"
    write_json(Path(state["state_file"]), state)
    print(json.dumps({"run": state["run"], "phase": state["phase"], "preserved_external_edits": conflicts}))


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("command", choices=["plan", "arm", "select", "status", "stop", "restore"])
    parser.add_argument("target", nargs="?", help="Target root or 1-based sequence number for select")
    parser.add_argument("--run", help="Fresh run ID for arm; otherwise defaults to active capture")
    args = parser.parse_args()
    if args.command == "plan":
        targets = build_targets()
        write_json(HERE / "capture-targets.json", targets)
        print(json.dumps({"phase": "prepared-not-armed", "manifest_version": targets["manifest_version"],
            "targets": [t["target"] for t in targets["targets"]]}, ensure_ascii=False))
        return
    if args.command == "arm":
        if game_pids():
            raise ValueError("Endfield.exe is running. Finish shader compilation, then exit normally before arm.")
        if (HERE / "active-capture.json").exists():
            previous = load_state(None)
            if previous["phase"] != "restored":
                raise ValueError("Previous capture still owns INI settings; stop and restore it first")
        run = args.run or "bem-1_6_3-login-" + dt.datetime.now().strftime("%Y%m%d-%H%M%S-") + uuid.uuid4().hex[:6]
        if not re.fullmatch(r"[A-Za-z0-9_-]{1,80}", run):
            raise ValueError("Invalid run ID")
        state_file = HERE / "runs" / run / "control-state.json"
        if state_file.exists():
            raise ValueError("Run already exists; choose a new run ID")
        local = Path(os.environ["LOCALAPPDATA"]) / "BetterEndfieldNext"
        targets = read_json(HERE / "capture-targets.json")
        state = dict(schema=1, run=run, phase="preparing", ini=str(local / "BetterEndfieldNext.ini"),
            catalog=str(local / "catalog/custom-model"), state_file=str(state_file),
            manifest_version=targets["manifest_version"], targets=targets["targets"],
            sources=targets["sources"], selected=targets["targets"][0]["target"], keys={})
        request = Path(state["catalog"]) / "native-probe.request"
        if request.exists():
            raise ValueError("An existing native-probe.request must be stopped before arming a new capture")
        write_json(state_file, state)
        write_json(HERE / "active-capture.json", {"run": run})
        sweep("arm", state, ["--manifest", str(MANIFEST), "--characters", *CHARACTERS, "--persistent"])
        request_meta = read_json(Path(state["catalog"]) / "native-probe" / (run + ".request.json"))
        if request_meta["manifest_version"] != state["manifest_version"]:
            raise ValueError("Manifest changed since plan; stop, restore and regenerate plan")
        apply_values(state, target_values(state["targets"][0]))
        state["phase"] = "armed"
        write_json(state_file, state)
        print(json.dumps({"run": run, "phase": "armed", "first_target": state["selected"],
            "next": "Start the prepared developer runtime normally; wait for probe ready/captured before select."}))
        return
    state = load_state(args.run)
    if args.command == "status":
        status_path = Path(state["catalog"]) / "native-probe.status.json"
        print(json.dumps({"control": {k: state[k] for k in ("run", "phase", "selected")},
            "probe": read_json(status_path) if status_path.exists() else None}, ensure_ascii=False))
        sweep("status", state)
    elif args.command == "stop":
        sweep("stop", state)
        state["phase"] = "stop_requested"
        write_json(Path(state["state_file"]), state)
    elif args.command == "restore":
        if game_pids():
            raise ValueError("Exit Endfield.exe normally before restoring the capture settings")
        request = Path(state["catalog"]) / "native-probe.request"
        if request.exists() and state["run"] in request.read_text(encoding="utf-8").splitlines()[1:2]:
            raise ValueError("Run stop before restore so the persistent request cannot arm again")
        restore(state)
    elif args.command == "select":
        if state["phase"] not in {"armed", "capturing"}:
            raise ValueError("Capture is not armed")
        status = read_json(Path(state["catalog"]) / "native-probe.status.json")
        if status.get("run") != state["run"] or status.get("stage") not in HEALTHY:
            raise ValueError(f"Probe is not healthy for this run: {status.get('run')} / {status.get('stage')}")
        if status.get("process_id") not in game_pids():
            raise ValueError("Probe status is stale; its Endfield.exe process is no longer running")
        target = next((t for index, t in enumerate(state["targets"], 1)
            if args.target in {str(index), t["target"]}), None)
        if target is None:
            raise ValueError("Select a target root or sequence number 1..5 from capture-targets.json")
        apply_values(state, target_values(target))
        state.update(phase="capturing", selected=target["target"])
        write_json(Path(state["state_file"]), state)
        print(json.dumps({"run": state["run"], "selected": state["selected"],
            "next": "Wait for this resource to be captured before selecting the next target."}))


if __name__ == "__main__":
    try:
        main()
    except (ValueError, OSError, KeyError, StopIteration, subprocess.CalledProcessError) as error:
        print(f"capture_control: {error}", file=sys.stderr)
        sys.exit(1)
