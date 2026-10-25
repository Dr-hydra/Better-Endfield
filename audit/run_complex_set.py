"""Run the complex-package regression set instead of the whole corpus.

Usage:
    python audit/run_complex_set.py            # human table
    python audit/run_complex_set.py --json     # machine output

The set lives in audit/complex-set.json and is chosen to hit every known
failure class plus the format limits. It is the default check after touching
skills/bem-creator/scripts or its references; a full 551-package sweep is only
for release validation.
"""
from __future__ import annotations

import argparse
import json
import sys
import time
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SET = ROOT / "audit" / "complex-set.json"
SCRIPTS = ROOT / "tools" / "CustomModel" / "skills" / "bem-creator" / "scripts"
sys.path.insert(0, str(SCRIPTS))

import bem_triage  # noqa: E402


def run(entry: dict) -> dict:
    started = time.perf_counter()
    try:
        report = bem_triage.triage(entry["source"])
        codes = [b["code"] for b in (report.get("blockers") or [])]
        routing = [b["code"] for b in
                   ((report.get("entry_conflicts") or {}).get("blockers") or [])]
        result = dict(
            id=entry["id"],
            verdict=report.get("verdict"),
            blocked=bool(report.get("blocked_by_source")),
            blockers=codes,
            routing=routing,
            active_inis=report.get("active_ini_count"),
            duplicate_sections=report["section_resolution"]["discarded_duplicate_sections"],
            error=None,
        )
    except Exception as exc:  # a crash is a regression in itself
        result = dict(id=entry["id"], verdict="CRASH", blocked=None,
                      blockers=[], routing=[], active_inis=None,
                      duplicate_sections=None,
                      error=f"{type(exc).__name__}: {exc}")
    result["ms"] = round((time.perf_counter() - started) * 1000)
    return result


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--json", action="store_true")
    args = parser.parse_args()

    spec = json.loads(SET.read_text(encoding="utf-8"))
    rows = [run(p) for p in spec["packages"]]
    total = sum(r["ms"] for r in rows)

    if args.json:
        print(json.dumps(dict(total_ms=total, results=rows), ensure_ascii=False, indent=2))
    else:
        width = max(len(p["exercises"]) for p in spec["packages"])
        print(f"{'id':6} {'verdict':18} {'blk':4} {'route':5} {'ini':4} {'dup':4} {'ms':>5}  exercises")
        for row, pkg in zip(rows, spec["packages"]):
            print(f"{row['id']:6} {str(row['verdict']):18} {str(row['blocked']):4} "
                  f"{','.join(row['routing']) or '-':5} "
                  f"{str(row['active_inis']):4} {str(row['duplicate_sections']):4} "
                  f"{row['ms']:5}  {pkg['exercises'][:width]}")
            if row["error"]:
                print(f"       ! {row['error']}")
            elif row["blockers"]:
                print(f"       → {', '.join(row['blockers'][:4])}")
        print(f"\n{len(rows)} packages, {total} ms total")
    return 1 if any(r["error"] for r in rows) else 0


if __name__ == "__main__":
    raise SystemExit(main())
