#!/usr/bin/env python3
"""Independent hash/LOD mod converter with verified native profile lowering.

Accepts an extracted directory or ZIP. Inspect arbitrary-named entry INIs,
record draw programs and validate buffers. BEMPC25 emission requires --profile.
Unsupported draw/shader semantics fail closed without replacing output files.
"""
import argparse
import json
from pathlib import Path, PurePosixPath

from convert_efmi_poc import Source
from efmi_source import sections
from efmi_lod_source import ENTRY, inspect_lod_source


def select_ini(src, explicit=None):
    if explicit:
        if explicit not in src.names:
            raise ValueError(f"selected INI is missing: {explicit}")
        return explicit
    candidates = []
    for name in src.names:
        path = PurePosixPath(name.replace("\\", "/"))
        if path.suffix.lower() != ".ini" or any(
                p.lower().startswith("disabled") or p.lower() in {"backup", "backups"}
                for p in path.parts):
            continue
        parsed = sections(src.read_exact(name).decode("utf-8-sig"))
        if any(ENTRY.fullmatch(section) for section in parsed):
            candidates.append(name)
    if len(candidates) != 1:
        raise ValueError(f"expected one hash/LOD entry INI, found {len(candidates)}; select --ini")
    return candidates[0]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("source", type=Path, help="Extracted mod directory or ZIP")
    parser.add_argument("--ini", help="Exact source-relative entry INI")
    parser.add_argument("--report", required=True, type=Path)
    parser.add_argument("--profile", type=Path, help="Verified schema=2 native layout/bone/material mapping")
    parser.add_argument("-o", "--output", type=Path, help="Output BEMPC25 package")
    parser.add_argument("--geometry-only", action="store_true",
                        help="Assess geometry requirements without replacement material mapping")
    args = parser.parse_args()
    if bool(args.output) != bool(args.profile):
        parser.error("--profile and --output must be supplied together")
    if args.profile and args.geometry_only:
        parser.error("native conversion requires explicit material mappings; --geometry-only is inspection only")
    src = Source(args.source)
    payload = None
    try:
        ini = select_ini(src, args.ini)
        report = inspect_lod_source(src, ini, sections(src.read_exact(ini).decode("utf-8-sig")),
                                    geometry_only=args.geometry_only)
        if args.profile:
            from hash_lod_lowering import convert
            try:
                if report["structural_errors"]:
                    raise ValueError("source structural validation failed")
                profile = json.loads(args.profile.read_text(encoding="utf-8-sig"))
                payload, summary = convert(src, ini, profile)
                report.update(conversion_ready=True, blockers=[], errors=[], native_output=summary)
            except (ValueError, KeyError, IndexError, TypeError) as exc:
                report["errors"].append(str(exc))
                report["conversion_failure"] = str(exc)
    finally:
        src.close()
    args.report.parent.mkdir(parents=True, exist_ok=True)
    args.report.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    if payload is not None:
        args.output.parent.mkdir(parents=True, exist_ok=True)
        temporary = args.output.with_suffix(args.output.suffix + ".tmp")
        temporary.write_bytes(payload)
        temporary.replace(args.output)
    print(f"entries={len(report['components'])} textures={report['textures']} "
          f"structural_errors={len(report['structural_errors'])} conversion_ready={report['conversion_ready']}")
    for issue in report["structural_errors"] + report["blockers"]:
        print(issue)
    if report.get("conversion_failure"): print(report["conversion_failure"])
    return 0 if report["conversion_ready"] else 2


if __name__ == "__main__":
    raise SystemExit(main())
