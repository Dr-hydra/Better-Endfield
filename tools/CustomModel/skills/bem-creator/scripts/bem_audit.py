#!/usr/bin/env python3
"""Audit the three-way agreement between ledger, report and artifact.

The reference workspace treats ``tasks.json`` as the single source of truth, and
``task_ledger.finish`` re-reads the package before accepting a task. That gate only
covers the fields it checks. Everything it does not check drifts, and this audit
measures that drift:

* declared appearance counts that cannot be true
* disclosure fields (``excluded_features``, ``limitations``) that are silently absent
* ledger names/ids that no longer match the artifact on disk
* artifacts that vanished or were moved

Reads BEM manifests with the standard library only: the manifest block is stored
uncompressed, so no zstd is needed to inspect metadata. Payload verification is out
of scope here; run the tool's own validator for that.

Usage:
    python bem_audit.py --tasks "F:\\zmd_bem\\进度\\tasks.json"
    python bem_audit.py --tasks ... --reports "F:\\zmd_bem\\reports" --json
    python bem_audit.py --package "F:\\zmd_bem\\成品\\伊冯\\某包.bem"
"""
from __future__ import annotations

import argparse
import json
import math
import struct
import sys
from pathlib import Path

HEADER = struct.Struct("<8sHHIQQII")
MAGIC = b"BEM\x00PKG\x00"
SUPPORTED_MINOR = (0, 1, 2, 3, 4, 5)
# A combination count above this cannot be materialised in a session; reporting it as
# a plain integer hides that it is an estimate, not a stored figure.
SELECTION_CAP = 10 ** 9
DISCLOSURE = ("excluded_features", "limitations")


def read_manifest(path: Path) -> tuple[dict, int]:
    """Read the manifest block. Only the header and the JSON body are touched."""
    with path.open("rb") as stream:
        head = stream.read(HEADER.size)
        if len(head) != HEADER.size:
            raise ValueError("truncated BEM header")
        magic, major, minor, header_size, file_size, manifest_size, count, flags = HEADER.unpack(head)
        if magic != MAGIC or major != 1 or minor not in SUPPORTED_MINOR:
            raise ValueError(f"unsupported container magic/version ({magic!r}, {major}.{minor})")
        if header_size != HEADER.size:
            raise ValueError("unexpected header size")
        if flags:
            raise ValueError("unexpected header flags")
        if file_size != path.stat().st_size:
            raise ValueError(f"header file_size {file_size} != on-disk {path.stat().st_size}")
        if not 0 < manifest_size <= 4 * 1024 * 1024:
            raise ValueError("implausible manifest size")
        manifest = json.loads(stream.read(manifest_size).decode("utf-8"))
        if ("option_groups" in manifest) != (minor >= 1):
            raise ValueError("manifest/version mismatch on option_groups")
        return manifest, minor


def selection_space(manifest: dict) -> tuple[int | None, bool]:
    """Return ``(count, exact)`` for the number of distinct appearances.

    ``exact`` is False when the product exceeds ``SELECTION_CAP``: the number is then
    a lower bound with a magnitude convention, not a stored quantity. Reporting such a
    value as if it were the appearance count is how 9.9e22 ended up in a ledger.
    """
    groups = manifest.get("option_groups") or []
    if not groups:
        return len(manifest.get("appearances") or []), True
    total = 1
    for group in groups:
        # 1.1+ spells the member list `choices`; `options` appears in some early
        # drafts. Accept both rather than silently counting zero.
        options = group.get("choices") or group.get("options") or []
        total *= max(1, len(options))
        if total > SELECTION_CAP:
            return None, False
    return total, True


def saved_assignments(manifest: dict) -> int:
    """How many states the file actually stores — the honest 'size' figure."""
    return len(manifest.get("appearances") or [])


def audit_package(path: Path, expected: dict | None = None, report: dict | None = None) -> list[dict]:
    issues: list[dict] = []
    if not path.is_file():
        return [{"code": "ARTIFACT_MISSING", "file": str(path),
                 "detail": "Ledger points at a file that is not on disk."}]
    try:
        manifest, minor = read_manifest(path)
    except (ValueError, OSError, json.JSONDecodeError) as exc:
        return [{"code": "ARTIFACT_UNREADABLE", "file": str(path), "detail": str(exc)}]

    if expected:
        if manifest.get("package_id") != expected.get("package_id"):
            issues.append({"code": "PACKAGE_ID_DRIFT", "file": str(path),
                           "detail": f"{manifest.get('package_id')} != ledger {expected.get('package_id')}"})
        if manifest.get("name") != expected.get("name"):
            issues.append({"code": "PACKAGE_NAME_DRIFT", "file": str(path),
                           "detail": f"{manifest.get('name')!r} != ledger {expected.get('name')!r}"})
        declared = expected.get("format_version")
        if declared and declared != f"1.{minor}":
            issues.append({"code": "FORMAT_VERSION_DRIFT", "file": str(path),
                           "detail": f"manifest 1.{minor} != ledger {declared}"})
        recorded = expected.get("appearance_count")
        count, exact = selection_space(manifest)
        stored = saved_assignments(manifest)
        if count is None:
            if isinstance(recorded, int) and recorded > SELECTION_CAP:
                issues.append({
                    "code": "APPEARANCE_COUNT_UNBOUNDED", "file": str(path),
                    "detail": f"ledger records {recorded}, which exceeds the {SELECTION_CAP} "
                              f"selection cap; the file stores {stored} appearances. The count is "
                              f"a runaway product, not a measurement.",
                })
        elif isinstance(recorded, int) and recorded != count:
            issues.append({
                "code": "APPEARANCE_COUNT_MISMATCH", "file": str(path),
                "detail": f"ledger {recorded} != manifest {count} (stored appearances: {stored})",
            })

    if report is not None:
        for field in DISCLOSURE:
            if field not in report:
                issues.append({
                    "code": "DISCLOSURE_ABSENT", "file": str(path), "field": field,
                    "detail": f"Report omits '{field}'. Nothing forces it, so a reader cannot "
                              f"tell whether the package hides something or simply forgot to say.",
                })
        for field, manifest_key in (("offline_validated", "offline_validated"),
                                    ("appearance_review_complete", "appearance_review_complete")):
            if report.get(field) is not True:
                issues.append({"code": "CLAIM_NOT_TRUE", "file": str(path), "field": field,
                               "detail": f"Report does not assert {field}=true."})
    return issues


def audit_ledger(tasks_path: Path, reports_dir: Path | None) -> dict:
    state = json.loads(tasks_path.read_text(encoding="utf-8-sig"))
    tasks = state.get("tasks") or state
    summary = {"tasks": len(tasks), "success": 0, "failure": 0, "other": 0,
               "artifacts_checked": 0, "issues": []}
    by_code: dict[str, int] = {}

    for task in tasks:
        status = task.get("status")
        if status == "成功":
            summary["success"] += 1
        elif status == "失败":
            summary["failure"] += 1
        else:
            summary["other"] += 1
            continue
        if status != "成功":
            continue
        output = task.get("output_path")
        if not output:
            summary["issues"].append({"code": "LEDGER_NO_OUTPUT", "task_id": task.get("id"),
                                      "detail": "Successful task without an output path."})
            continue
        report = None
        report_path = task.get("report_path")
        if report_path and Path(report_path).is_file():
            try:
                report = json.loads(Path(report_path).read_text(encoding="utf-8-sig"))
            except json.JSONDecodeError:
                report = None
        elif reports_dir is not None:
            candidate = reports_dir / f"{task.get('id')}.final-result.json"
            if candidate.is_file():
                try:
                    report = json.loads(candidate.read_text(encoding="utf-8-sig"))
                except json.JSONDecodeError:
                    report = None
        for issue in audit_package(Path(output), task, report):
            issue["task_id"] = task.get("id")
            summary["issues"].append(issue)
        summary["artifacts_checked"] += 1

    for issue in summary["issues"]:
        by_code[issue["code"]] = by_code.get(issue["code"], 0) + 1
    summary["issues_by_code"] = dict(sorted(by_code.items(), key=lambda item: -item[1]))
    summary["clean"] = not summary["issues"]
    return summary


def render(summary: dict) -> str:
    lines = [
        f"tasks         : {summary['tasks']} "
        f"({summary['success']} success, {summary['failure']} failure, {summary['other']} open)",
        f"artifacts     : {summary['artifacts_checked']} checked",
        f"issues        : {len(summary['issues'])}",
    ]
    if summary["issues_by_code"]:
        lines.append("")
        lines.append("BY CODE")
        for code, count in summary["issues_by_code"].items():
            lines.append(f"  {count:4d}  {code}")
    if summary["issues"]:
        lines.append("")
        lines.append("DETAIL (first 40)")
        for issue in summary["issues"][:40]:
            tag = issue.get("task_id") or "-"
            lines.append(f"  [{tag}] {issue['code']}: {str(issue.get('detail'))[:150]}")
        if len(summary["issues"]) > 40:
            lines.append(f"  ... {len(summary['issues']) - 40} more")
    else:
        lines.append("")
        lines.append("Ledger, reports and artifacts agree.")
    return "\n".join(lines)


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(description="Audit ledger/report/artifact agreement.")
    parser.add_argument("--tasks", help="path to tasks.json")
    parser.add_argument("--reports", help="directory holding Mxxxx.final-result.json")
    parser.add_argument("--package", help="audit a single .bem file")
    parser.add_argument("--json", action="store_true")
    args = parser.parse_args(argv)

    if args.package:
        manifest, minor = read_manifest(Path(args.package))
        count, exact = selection_space(manifest)
        payload = {
            "file": args.package,
            "format_version": f"1.{minor}",
            "package_id": manifest.get("package_id"),
            "name": manifest.get("name"),
            "appearances_stored": saved_assignments(manifest),
            "option_groups": len(manifest.get("option_groups") or []),
            "selection_space": count,
            "selection_exact": exact,
            "capabilities": manifest.get("required_capabilities"),
        }
        print(json.dumps(payload, ensure_ascii=False, indent=2) if args.json else
              "\n".join(f"{key:20s}: {value}" for key, value in payload.items()))
        return 0

    if not args.tasks:
        parser.error("provide --tasks or --package")
    summary = audit_ledger(Path(args.tasks), Path(args.reports) if args.reports else None)
    if args.json:
        print(json.dumps(summary, ensure_ascii=False, indent=2))
    else:
        print(render(summary))
    return 0 if summary["clean"] else 1


if __name__ == "__main__":
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8")
    raise SystemExit(main())
