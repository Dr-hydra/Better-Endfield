#!/usr/bin/env python3
"""Rank peer sources that carry a payload this package is missing.

A missing DDS or BUF is the largest single failure class in the reference
corpus, and it is usually a *distribution* problem, not a conversion problem:
the author shipped a switchable variant, a re-upload dropped a folder, or a
scraper stripped the Textures directory. The same file frequently exists in a
sibling package for the same character.

This tool answers one question: *which sibling is worth opening first?* It does
not answer "is this the same asset", and must not be treated as if it did.

What it actually establishes
----------------------------
    * a package contains a file with exactly this name
    * the byte count, and with ``--hash`` the sha256 of that copy
    * whether peers disagree about the size of that name

What it does not establish
--------------------------
    * that the copy is the one the author used, or that it is unmodified
    * that the missing file's identity (the ``t=<hash>`` in its name) matches
      the copy's contents — a texture can be replaced by a same-named edit
    * that shipping it makes the package convertible

The distinction matters because a texture that merely looks right produces a
package that builds, loads, and renders the wrong thing — strictly worse than a
package that refuses to build. So there is no "close match" tier here: a file
with a different name is not a candidate at all.

Read-only. Nothing is copied, nothing is promoted automatically.

Usage:
    python bem_peer.py --need "Textures/Components-3 t=be225048.dds" \\
        --root "F:\\zmd\\伊冯"
    python bem_peer.py --target "F:\\zmd\\伊冯\\某包" --root "F:\\zmd" --hash
    python bem_peer.py --need a.dds --need b.buf --root "F:\\zmd" --json

Exit codes: 0 candidates found, 1 none found, 2 bad input.
"""
from __future__ import annotations

import argparse
import hashlib
import json
import sys
from collections import defaultdict
from pathlib import Path

PAYLOAD_SUFFIXES = (".dds", ".buf", ".png", ".jpg", ".tga", ".bmp")


def _sha256(path: Path, chunk: int = 1 << 20) -> str:
    digest = hashlib.sha256()
    with path.open("rb") as handle:
        while True:
            block = handle.read(chunk)
            if not block:
                break
            digest.update(block)
    return digest.hexdigest()


def index_package(root: Path) -> dict[str, list[tuple[int, str]]]:
    """``lowercase basename -> [(size, relative path)]`` for one package."""
    index: dict[str, list[tuple[int, str]]] = {}
    for path in root.rglob("*"):
        if not path.is_file() or path.suffix.lower() not in PAYLOAD_SUFFIXES:
            continue
        try:
            size = path.stat().st_size
        except OSError:
            continue
        index.setdefault(path.name.casefold(), []).append(
            (size, path.relative_to(root).as_posix()))
    return index


def target_packages(root: Path) -> list[Path]:
    """Directories that look like a package: contain at least one active INI."""
    found: set[Path] = set()
    for ini in root.rglob("*.ini"):
        parts = [part.lower() for part in ini.relative_to(root).parts]
        if any(part.startswith("disabled") for part in parts):
            continue
        found.add(ini.parent)
    # Collapse nesting: an INI under Meshes/ does not start a new package.
    kept: list[Path] = []
    for candidate in sorted(found, key=lambda item: len(item.parts)):
        if not any(candidate.is_relative_to(existing) for existing in kept):
            kept.append(candidate)
    return kept


def missing_from_target(target: Path) -> list[str]:
    """Declared-but-absent payload paths, using the triage probe when available."""
    try:
        import bem_triage
    except ImportError:
        return []
    try:
        report = bem_triage.triage(target)
    except SystemExit:
        return []
    needs = []
    for bucket in ("blockers", "warnings"):
        for item in report.get(bucket, []):
            declared = item.get("declared")
            if declared and str(item.get("code", "")).startswith("MISSING"):
                needs.append(declared)
    return sorted(set(needs))


def _basename(need: str) -> str:
    return need.strip().strip('"').replace("\\", "/").rsplit("/", 1)[-1]


def search(needs: list[str], root: Path, exclude: Path | None = None,
           verify_hash: bool = False) -> dict:
    packages = [item for item in target_packages(root)
                if exclude is None or item.resolve() != exclude.resolve()]
    wanted = {_basename(item).casefold(): item for item in needs if item}

    candidates: list[dict] = []
    sizes_seen: dict[str, dict[int, list[str]]] = defaultdict(lambda: defaultdict(list))

    for package in packages:
        index = index_package(package)
        hits: list[dict] = []
        for key, original in wanted.items():
            for size, relative in index.get(key, []):
                entry = {"need": original, "found": relative, "bytes": size}
                if verify_hash:
                    try:
                        entry["sha256"] = _sha256(package / relative)
                    except OSError:
                        continue
                hits.append(entry)
                sizes_seen[key][size].append(package.as_posix())
        if hits:
            candidates.append({
                "package": package.as_posix(),
                "hits": hits,
                "hit_count": len(hits),
                "covers": sorted({item["need"] for item in hits}),
            })

    candidates.sort(key=lambda item: (-item["hit_count"], item["package"].casefold()))

    conflicts = []
    for key, by_size in sizes_seen.items():
        if len(by_size) > 1:
            conflicts.append({
                "name": _basename(wanted[key]),
                "sizes": {str(size): sorted(set(paths)) for size, paths in by_size.items()},
            })

    return {
        "root": root.as_posix(),
        "needs": sorted(wanted.values()),
        "packages_scanned": len(packages),
        "candidates": candidates,
        "size_conflicts": conflicts,
        "coverage": {
            "needs_total": len(wanted),
            "needs_found": len({item["need"] for row in candidates for item in row["hits"]}),
        },
    }


def render(report: dict) -> str:
    coverage = report["coverage"]
    lines = [f"root            : {report['root']}",
             f"packages scanned: {report['packages_scanned']}",
             f"payloads needed : {coverage['needs_total']}"]
    if not report["candidates"]:
        lines.append("")
        lines.append("No peer carries a file with any of these names. That is a "
                     "distribution gap, not a conversion problem: record it and stop.")
        return "\n".join(lines)

    lines.append(f"needs with a copy: {coverage['needs_found']} of {coverage['needs_total']}")
    lines.append("")
    lines.append("Candidates, best first. A hit means the same file name exists there.")
    lines.append("It is a place to look, not proof the author's asset is that copy.")
    for rank, candidate in enumerate(report["candidates"][:12], 1):
        lines.append("")
        lines.append(f"{rank:>2}. {candidate['package']}")
        lines.append(f"    covers {candidate['hit_count']} of "
                     f"{coverage['needs_total']} needed payloads")
        for item in candidate["hits"][:8]:
            digest = f"  sha256={item['sha256'][:16]}" if "sha256" in item else ""
            lines.append(f"    = {item['found']}  ({item['bytes']} B){digest}")
        if len(candidate["hits"]) > 8:
            lines.append(f"    ... {len(candidate['hits']) - 8} more")

    if report["size_conflicts"]:
        lines.append("")
        lines.append("Same name, different size across peers — the name does not "
                     "identify the asset here:")
        for conflict in report["size_conflicts"][:8]:
            lines.append(f"  {conflict['name']}")
            for size, paths in sorted(conflict["sizes"].items(), key=lambda kv: -int(kv[0])):
                lines.append(f"    {size:>10} B  {len(paths)} copies, e.g. {paths[0]}")
        lines.append("  Resolve by identity, not by name: that is what --hash and the "
                     "character catalog are for.")
    return "\n".join(lines)


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(
        description="Find peer sources that carry a missing payload.")
    parser.add_argument("--root", required=True,
                        help="directory to search for peer packages")
    parser.add_argument("--need", action="append", default=[],
                        help="missing payload path or file name; repeatable")
    parser.add_argument("--target",
                        help="package whose missing payloads are used as --need")
    parser.add_argument("--hash", action="store_true",
                        help="compute sha256 for every hit (slower, stronger)")
    parser.add_argument("--json", action="store_true", help="machine-readable output")
    args = parser.parse_args(argv)

    root = Path(args.root)
    if not root.is_dir():
        print(f"not a directory: {root}", file=sys.stderr)
        return 2
    needs = list(args.need)
    exclude = None
    if args.target:
        target = Path(args.target)
        if not target.is_dir():
            print(f"not a directory: {target}", file=sys.stderr)
            return 2
        exclude = target.resolve()
        derived = missing_from_target(target)
        if not derived and not needs:
            print("that package has no recorded missing payload; nothing to search for",
                  file=sys.stderr)
            return 1
        needs.extend(derived)
    needs = sorted({item for item in needs if item})
    if not needs:
        print("nothing to search for: pass --need or --target", file=sys.stderr)
        return 2

    report = search(needs, root, exclude, args.hash)
    if args.json:
        print(json.dumps(report, ensure_ascii=False, indent=2))
    else:
        print(render(report))
    return 0 if report["candidates"] else 1


if __name__ == "__main__":
    sys.exit(main())
