#!/usr/bin/env python3
"""Flatten the per-job payloads into the single payload the release host wants.

Each build job uploads its own artifact directory containing one payload.json
and its files. The signing host takes one flat directory plus one manifest, so
this merges them and refuses to continue if the jobs disagree about the version.
"""
import json
import shutil
import sys
from pathlib import Path


def main():
    root = Path(sys.argv[1] if len(sys.argv) > 1 else "payload")
    if not root.is_dir():
        raise SystemExit("payload directory is missing: " + str(root))

    version = None
    components = []
    seen = set()
    notes = None

    parts = sorted(path for path in root.iterdir() if path.is_dir())
    if not parts:
        raise SystemExit("no job payloads found under " + str(root))

    for part in parts:
        manifest_path = part / "payload.json"
        if not manifest_path.is_file():
            raise SystemExit("missing payload.json in " + part.name)
        manifest = json.loads(manifest_path.read_text(encoding="utf-8"))

        part_version = manifest.get("version")
        if version is None:
            version = part_version
        elif part_version != version:
            raise SystemExit(
                "jobs disagree about the version: {} vs {}".format(version, part_version))

        for component in manifest.get("components", []):
            name = component["name"]
            if Path(name).name != name:
                raise SystemExit("component name must be a bare filename: " + name)
            if name in seen:
                raise SystemExit("two jobs produced the same asset: " + name)
            source = part / name
            if not source.is_file():
                raise SystemExit("{} declares {} but it is not there".format(part.name, name))
            shutil.move(str(source), str(root / name))
            seen.add(name)
            components.append(component)

        candidate = part / "notes.md"
        if candidate.is_file() and notes is None:
            notes = candidate

    if notes is None:
        raise SystemExit("no job supplied notes.md")
    shutil.move(str(notes), str(root / "notes.md"))

    merged = {"version": version, "components": components}
    (root / "payload.json").write_text(
        json.dumps(merged, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    for part in parts:
        shutil.rmtree(part)

    print(json.dumps({"version": version,
                      "components": [c["name"] for c in components]},
                     ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
