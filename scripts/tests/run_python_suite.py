#!/usr/bin/env python3
"""Reuse unittest discovery while reporting real skips and empty suites to the runner."""
from __future__ import annotations

import argparse
import json
import os
from pathlib import Path
import sys
import unittest

sys.dont_write_bytecode = True


def summarize(result):
    count = result.testsRun
    failures = len(result.failures) + len(result.errors) + len(result.unexpectedSuccesses)
    skipped = len(result.skipped)
    if failures:
        status, reason = "failed", "unittest failures/errors/unexpected successes"
    elif not count:
        status, reason = "blocked", "No unittest cases were discovered"
    elif skipped == count:
        status, reason = "skipped", "All discovered cases were skipped"
    else:
        status, reason = "passed", "unittest passed; inspect skipped/expected_failure counts for coverage"
    return {"status": status, "reason": reason, "tests_run": count,
            "passed": count - failures - skipped - len(result.expectedFailures),
            "failures": len(result.failures), "errors": len(result.errors), "unexpected_successes": len(result.unexpectedSuccesses),
            "expected_failures": len(result.expectedFailures), "skipped": skipped,
            "skips": [{"case": test.id(), "reason": reason} for test, reason in result.skipped]}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--module", action="append", required=True)
    parser.add_argument("--result", type=Path, required=True)
    args = parser.parse_args()
    sys.path.insert(0, os.getcwd())
    loader = unittest.TestLoader()
    suite = unittest.TestSuite(loader.loadTestsFromName(name) for name in args.module)
    result = unittest.TextTestRunner(verbosity=2).run(suite)
    report = summarize(result)
    args.result.parent.mkdir(parents=True, exist_ok=True)
    args.result.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    return 1 if report["status"] == "failed" else 0


if __name__ == "__main__":
    raise SystemExit(main())
