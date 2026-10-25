#!/usr/bin/env python3
"""Loader-faithful section resolution for 3Dmigoto / XXMI source packages.

Every other probe in this folder reads INIs one file at a time and treats the
directory as a shared namespace. That is a guess, and it is wrong in two common
ways:

* ``[Include]`` sections pull other files in, and the loader *renames* what it
  pulls in (see below). Reading the entry file alone therefore misses draw
  entries, or reports them under a name the engine never uses.
* A repeated section inside one file is not merged. The loader parses the first
  one and silently drops the rest, so the *last* copy in the text is dead.

This module reproduces the loader's own rules, taken from the XXMI
``IniHandler.cpp`` snapshot kept as evidence in the reference workspace
(``ParseIniSectionLine`` / ``ParseIncludedIniFiles``), and returns the section
set the engine actually builds.

Rules implemented
-----------------
1. Section name is the text up to the **first** ``]``, whitespace-trimmed;
   anything after the ``]`` is not a command. A line with ``[`` and no ``]``
   uses the rest of the line as the name.
2. A repeated section name inside one parse unit: **first occurrence wins, later
   occurrences are discarded whole** (``ini_sections.emplace`` fails, the entry
   is skipped). Not merged, not appended.
3. Sections in an *included* file are namespaced when their type has a prefix:
   ``[TextureOverride_Foo]`` in ``sub/mod.ini`` becomes
   ``TextureOverride\\sub/mod.ini\\Foo``. Prefix-less global sections
   (``[Constants]``, ``[Present]``, ``[ClearRenderTargetView]`` ...) are
   **not** renamed, and their statements append into the existing global entry.
4. Variables declared in an included file are namespaced the same way
   (``$\\sub/mod.ini\\$var``).
5. ``[Include]`` keys resolve **relative to the package root**, not relative to
   the including file. ``include_recursive`` globs.
6. An include cycle, or the same file pulled in twice, is reported and the
   second pull is ignored.

Only the standard library is used. Nothing is written; sources are read-only.

Usage:
    python bem_merge.py "F:\\zmd\\伊冯\\某包"
    python bem_merge.py <package> --json
    python bem_merge.py <package> --show-ignored --show-includes
    python bem_merge.py <package> --emit merged.txt

Exit codes: 0 clean, 1 findings worth a look, 2 unusable input.
"""
from __future__ import annotations

import argparse
import fnmatch
import json
import re
import sys
from pathlib import Path

from bem_ini import Section, command_lines, is_active_ini

# --- section taxonomy (mirrors the loader's two tables) --------------------
# A prefix section is renamed when it arrives from an included file.
PREFIX_SECTIONS = {
    "textureoverride", "commandlist", "shaderoverride", "customshader",
    "shaderregex", "builtincommandlist", "builtincustomshader",
    "pool", "resource", "key", "include", "preset",
}
# Prefix-less sections stay global across files; their statements append.
GLOBAL_SECTIONS = {
    "constants", "present", "clearrendertargetview", "cleardepthstencilview",
    "clearunorderedaccessviewuint", "clearunorderedaccessviewfloat",
    "hunting", "logging", "system", "input", "device", "rendering",
    "loader", "profile", "stereo", "convergencemap",
}

# --- injected-watermark lines --------------------------------------------
# Scraper/seller sites splice junk lines into shipped INIs. They are not
# commands, they break naive "line with an = is an assignment" parsing, and
# they can sit between a key and its value. Two shapes are common: a bracketed
# banner line and the same banner with random '/' characters inserted.
_WATERMARK = re.compile(r"^[^\w$\[\];]*【[^】]*】[^\w]*_?\d*\s*$")
_WATERMARK_SUFFIX = re.compile(r"[\u4e00-\u9fff]{2,}.*_\d{4,}\s*$")

HEADER_START = "["


class LiveSection:
    """One surviving section, with both the loader name and the written name."""

    __slots__ = ("name", "plain", "relative", "namespace", "section", "appended_from")

    def __init__(self, name: str, plain: str, relative: str, namespace: str,
                 section: Section, appended_from: tuple[str, ...] = ()):
        self.name = name
        self.plain = plain
        self.relative = relative
        self.namespace = namespace
        self.section = section
        self.appended_from = appended_from

    def as_dict(self) -> dict:
        return {
            "name": self.name,
            "plain": self.plain,
            "file": self.relative,
            "namespace": self.namespace,
            "line": self.section.line,
            "appended_from": list(self.appended_from),
        }


class MergeResult:
    def __init__(self) -> None:
        self.live: list[LiveSection] = []
        self.ignored: list[dict] = []
        self.includes: list[dict] = []
        self.scrubbed: list[dict] = []
        self.notes: list[str] = []
        self.entry: str | None = None

    # -- views -------------------------------------------------------------
    def plain_index(self) -> dict[str, LiveSection]:
        """``plain name -> section`` for names that are unique after resolution."""
        index: dict[str, LiveSection] = {}
        for item in self.live:
            index.setdefault(item.plain.casefold(), item)
        return index

    def collisions(self) -> dict[str, list[LiveSection]]:
        """Plain names that survive twice, i.e. in two different namespaces.

        This is not an error: the engine keeps both. It matters to a converter
        because a hand-written reference like ``run = CommandList\\Name`` inside
        one namespace is unambiguous, while a directory-wide scan is not.
        """
        buckets: dict[str, list[LiveSection]] = {}
        for item in self.live:
            buckets.setdefault(item.plain.casefold(), []).append(item)
        return {name: items for name, items in buckets.items() if len(items) > 1}

    def documents(self) -> list[tuple[str, dict[str, Section]]]:
        """Drop-in shape for :mod:`bem_triage` / :mod:`bem_state_probe`.

        Keyed by plain name (the name as written), so existing matchers keep
        working. Sections that lost to a duplicate are absent, which is the
        whole point: the dead copies must not influence a verdict.
        """
        files: dict[str, dict[str, Section]] = {}
        for item in self.live:
            files.setdefault(item.relative, {})[item.plain] = item.section
        return [(relative, sections) for relative, sections in files.items()]

    def to_dict(self) -> dict:
        return {
            "entry": self.entry,
            "live_count": len(self.live),
            "ignored": self.ignored,
            "includes": self.includes,
            "scrubbed": self.scrubbed,
            "notes": self.notes,
            "collisions": {name: [item.name for item in items]
                           for name, items in self.collisions().items()},
        }


# --- text level -----------------------------------------------------------
def scrub(text: str) -> tuple[str, list[dict]]:
    """Remove injected banner lines. Never touches a line that could be a command.

    A removal is only allowed when the line carries the bracketed banner and no
    ``=`` — a real statement always has an ``=`` outside of command-list flow
    control, and flow control never contains a CJK banner.
    """
    out: list[str] = []
    removed: list[dict] = []
    for number, line in enumerate(text.split("\n"), 1):
        stripped = line.strip()
        if stripped and "=" not in stripped and (
            _WATERMARK.match(stripped) or _WATERMARK_SUFFIX.search(stripped)
        ):
            removed.append({"line": number, "text": stripped[:120]})
            continue
        out.append(line)
    return "\n".join(out), removed


def split_sections(text: str) -> tuple[str, list[tuple[str, int, str]]]:
    """Split into ``(preamble, [(name, line, body)])`` using the loader's rules."""
    lines = text.split("\n")
    preamble: list[str] = []
    parts: list[tuple[str, int, list[str]]] = []
    current: list[str] | None = None
    for number, line in enumerate(lines, 1):
        candidate = line.lstrip()
        if candidate.startswith(HEADER_START) and len(candidate) > 1:
            close = candidate.find("]")
            raw = candidate[1:] if close < 0 else candidate[1:close]
            name = raw.strip()
            if name:
                current = []
                parts.append((name, number, current))
                continue
        if current is None:
            preamble.append(line)
        else:
            current.append(line)
    return ("\n".join(preamble),
            [(name, number, "\n".join(body)) for name, number, body in parts])


def _include_keys(body: str) -> tuple[list[str], list[str], list[str]]:
    """Read ``include`` / ``include_recursive`` / ``exclude_recursive`` keys."""
    plain, recursive, excludes = [], [], []
    for statement in command_lines(body):
        if "=" not in statement:
            continue
        key, _, value = statement.partition("=")
        key = key.strip().lower()
        value = value.strip().strip('"').replace("\\", "/")
        if not value:
            continue
        if key == "include":
            plain.append(value)
        elif key == "include_recursive":
            recursive.append(value)
        elif key == "exclude_recursive":
            excludes.append(value)
    return plain, recursive, excludes


def _excluded(relative: str, excludes: list[str]) -> bool:
    """``exclude_recursive`` patterns match either the tail or the basename."""
    name = relative.rsplit("/", 1)[-1]
    for pattern in excludes:
        if fnmatch.fnmatch(name, pattern) or fnmatch.fnmatch(relative, pattern) \
                or fnmatch.fnmatch(relative, f"*/{pattern}"):
            return True
    return False


def _expand_recursive(root: Path, pattern: str, excludes: list[str]) -> list[str]:
    """Resolve one ``include_recursive`` value to relative INI paths.

    The value is a path prefix, not necessarily a glob: the stock template uses
    a bare directory name (``include_recursive = Mods``), meaning every INI
    below it. Wildcards are supported when present.
    """
    matches: list[str] = []
    if any(char in pattern for char in "*?["):
        for candidate in sorted(root.rglob("*")):
            if not candidate.is_file() or not candidate.name.lower().endswith(".ini"):
                continue
            relative = candidate.relative_to(root).as_posix()
            if fnmatch.fnmatch(relative, pattern):
                matches.append(relative)
    else:
        target = root / pattern
        if target.is_dir():
            for candidate in sorted(target.rglob("*.ini")):
                matches.append(candidate.relative_to(root).as_posix())
        elif target.is_file():
            matches.append(target.relative_to(root).as_posix())
    return [item for item in matches if not _excluded(item, excludes)]


def _namespaced(prefix: str, namespace: str, rest: str) -> str:
    return f"{prefix}\\{namespace}\\{rest}"


_PREFIX_BY_LENGTH = sorted(PREFIX_SECTIONS, key=len, reverse=True)


def _prefix_of(name: str) -> tuple[str, str] | None:
    """Split a prefix section into ``(prefix as written, remainder)``.

    The loader matches by *string prefix*, not by a separator: ``[ResourceX]``
    matches ``Resource`` and leaves ``X``; ``[TextureOverride_Foo]`` leaves
    ``_Foo``. Longest match first so a compound name is not split early.
    """
    lowered = name.casefold()
    for prefix in _PREFIX_BY_LENGTH:
        if lowered.startswith(prefix):
            return name[:len(prefix)], name[len(prefix):]
    return None


def _qualify(name: str, namespace: str) -> tuple[str, bool]:
    """Apply rule 3. Returns ``(final name, renamed)``."""
    if not namespace:
        return name, False
    split = _prefix_of(name)
    if split is None:
        return name, False
    prefix, rest = split
    return _namespaced(prefix, namespace, rest), True


# --- file level -----------------------------------------------------------
_NAMESPACE_DIRECTIVE = re.compile(r"^\s*namespace\s*=\s*(.*?)\s*$", re.I)


def _namespace_directive(preamble: str) -> str | None:
    r"""Return the namespace a file declares itself, or ``None`` if it declares none.

    The loader honours ``namespace =`` only on lines that sit outside any
    section, and from there on it replaces the namespace the file inherited.
    In practice it is the first line of a file that ships as a framework: a
    library included as ``Lib/RabbitFX.ini`` would otherwise live under the
    namespace ``Lib/RabbitFX.ini``, but declaring ``namespace = RabbitFX`` is
    what makes ``[ResourceDiffuse]`` answer to ``Resource\RabbitFX\Diffuse`` —
    the exact name the caller writes to fill that slot. An empty value clears
    the namespace instead, so an empty string is returned as-is.
    """
    found: str | None = None
    for line in preamble.split("\n"):
        match = _NAMESPACE_DIRECTIVE.match(line)
        if match:
            found = match.group(1)
    return found


def _parse_unit(path: Path, namespace: str, root: Path,
                isolated: bool = False) -> tuple[list[LiveSection], list[dict], list[dict], list[dict]]:
    """Parse one file. Returns ``(sections, ignored, include keys, scrubbed)``.

    ``isolated`` marks a file the loader never reaches through an include, e.g.
    one of several user-selectable variants shipped side by side. It is parsed
    in its own namespace so its global sections cannot leak into the entry.
    """
    empty: tuple[list, list, list, list] = ([], [], [], [])
    try:
        raw = path.read_text(encoding="utf-8-sig", errors="replace")
    except OSError:
        return empty
    text = raw.replace("\r\n", "\n").replace("\r", "")
    text, scrubbed = scrub(text)
    preamble, parts = split_sections(text)
    declared = _namespace_directive(preamble)
    if declared is not None:
        namespace = declared

    live: list[LiveSection] = []
    ignored: list[dict] = []
    includes: list[dict] = []
    seen: dict[str, int] = {}
    try:
        relative = path.relative_to(root).as_posix()
    except ValueError:
        relative = path.as_posix()

    for name, number, body in parts:
        qualified, renamed = _qualify(name, namespace)
        key = qualified.casefold()
        # The written name, which is what a human and the matchers both see.
        # Namespacing only changes the key the engine stores it under.
        plain = name

        if key in seen:
            # Rule 2, with the loader's one exception: a prefix-less global
            # section arriving from an included file appends instead of dying.
            global_append = (not renamed) and bool(namespace) and not isolated and \
                name.strip().casefold() in GLOBAL_SECTIONS
            if global_append:
                owner = next(item for item in live if item.name.casefold() == key)
                owner.section.body += "\n" + body
                owner.appended_from = owner.appended_from + (f"{relative}:{number}",)
                ignored.append({
                    "section": name, "file": relative, "line": number,
                    "action": "append_to_global",
                    "reason": "prefix-less section from an included file appends "
                              "into the existing global entry",
                })
                continue
            ignored.append({
                "section": qualified, "plain": plain, "file": relative,
                "line": number, "first_line": seen[key],
                "commands": len(command_lines(body)),
                "action": "discard",
                "reason": "the loader keeps only the first occurrence of a "
                          "section name; later copies are parsed and dropped",
            })
            continue

        seen[key] = number
        section = Section(qualified, number, body)
        live.append(LiveSection(qualified, plain, relative, namespace, section))

        if name.strip().casefold().startswith("include"):
            plain_includes, recursive, _ = _include_keys(body)
            for value in plain_includes:
                includes.append({"file": relative, "line": number,
                                 "kind": "include", "target": value})
            for value in recursive:
                includes.append({"file": relative, "line": number,
                                 "kind": "include_recursive", "target": value})

    for item in scrubbed:
        item["file"] = relative
    return live, ignored, includes, scrubbed


def merge(package: str | Path, entry: str | None = None) -> MergeResult:
    """Build the effective section set for one source package."""
    root = Path(package)
    result = MergeResult()
    if not root.is_dir():
        result.notes.append(f"NOT_A_DIRECTORY: {root}")
        return result

    candidates = [path for path in sorted(root.rglob("*.ini"))
                  if is_active_ini(path.relative_to(root).as_posix())]
    if not candidates:
        result.notes.append("NO_ACTIVE_INI")
        return result

    if entry:
        entry_path = (root / entry)
        if not entry_path.is_file():
            result.notes.append(f"ENTRY_NOT_FOUND: {entry}")
            return result
    else:
        # Prefer a root-level mod.ini, then the shallowest file with the most
        # prefixed sections. This only decides the reading order for the
        # report; every active file is still merged.
        def rank(path: Path) -> tuple[int, int, str]:
            depth = len(path.relative_to(root).parts)
            return (depth, -path.stat().st_size, path.as_posix())
        entry_path = sorted(candidates, key=rank)[0]
    result.entry = entry_path.relative_to(root).as_posix()

    # ``exclude_recursive`` is collected once, before recursion starts, exactly
    # as the loader does: an included file cannot change it mid-walk.
    excludes: list[str] = []
    for path in candidates:
        try:
            text = path.read_text(encoding="utf-8-sig", errors="replace")
        except OSError:
            continue
        text, _ = scrub(text.replace("\r\n", "\n").replace("\r", ""))
        _, parts = split_sections(text)
        for name, _, body in parts:
            if name.strip().casefold().startswith("include"):
                excludes.extend(_include_keys(body)[2])

    visited: set[str] = set()
    queue: list[tuple[Path, str]] = [(entry_path, "")]

    while queue:
        path, namespace = queue.pop(0)
        try:
            relative = path.relative_to(root).as_posix()
        except ValueError:
            result.notes.append(f"OUTSIDE_PACKAGE: {path.as_posix()}")
            continue
        if relative in visited:
            result.notes.append(f"INCLUDED_TWICE: {relative}")
            continue
        visited.add(relative)

        live, ignored, includes, scrubbed = _parse_unit(path, namespace, root)
        result.live.extend(live)
        result.ignored.extend(ignored)
        result.includes.extend(includes)
        result.scrubbed.extend(scrubbed)

        for statement in includes:
            target = statement["target"]
            if statement["kind"] == "include":
                resolved = (root / target)
                if resolved.is_file():
                    queue.append((resolved, target))
                else:
                    result.notes.append(f"INCLUDE_NOT_FOUND: {relative} -> {target}")
            else:
                matches = _expand_recursive(root, target, excludes)
                if not matches:
                    result.notes.append(
                        f"INCLUDE_RECURSIVE_EMPTY: {relative} -> {target}")
                for match in matches:
                    queue.append((root / match, match))

    # An active file no include pulled in is still worth reading: it is usually a
    # user-selectable variant shipped next to the entry, and a converter that
    # ignored it would under-report. It is parsed in its own namespace so its
    # globals stay out of the entry's.
    for path in candidates:
        relative = path.relative_to(root).as_posix()
        if relative in visited:
            continue
        result.notes.append(f"SEPARATE_VARIANT: {relative} (parsed alone, not included)")
        live, ignored, _, scrubbed = _parse_unit(path, relative, root, isolated=True)
        result.live.extend(live)
        result.ignored.extend(ignored)
        result.scrubbed.extend(scrubbed)
        visited.add(relative)

    _note_variable_collisions(result)
    return result


# --- known limitation -----------------------------------------------------
_VAR = re.compile(r"(?<![$\w])\$(?:global\s+)?([A-Za-z_][\w.]*)\s*=", re.I)


def _note_variable_collisions(result: MergeResult) -> None:
    """Report variable names declared in more than one namespace.

    The loader renames variables declared in an included file to
    ``$\\<namespace>\\$name`` and every use inside that file resolves with the
    same namespace, so each file stays self-consistent. This module does not
    rewrite the names, which means a *merged* environment would let one file's
    declaration shadow another's. Declarations and uses inside one file still
    line up, so single-namespace analysis is unaffected; only a cross-file
    environment needs the warning.
    """
    owners: dict[str, set[str]] = {}
    for item in result.live:
        if item.section.name.strip().casefold() != "constants":
            continue
        namespace = item.namespace or "(entry)"
        for line in command_lines(item.section.body):
            for name in _VAR.findall(line):
                owners.setdefault(name.casefold(), set()).add(namespace)
    for name, namespaces in owners.items():
        if len(namespaces) > 1:
            result.notes.append(
                f"VARIABLE_IN_SEVERAL_NAMESPACES: ${name} in "
                f"{', '.join(sorted(namespaces))} — each is scoped by the loader, "
                f"so do not merge their values")


# --- presentation ---------------------------------------------------------
def render(result: MergeResult, show_ignored: bool = False,
           show_includes: bool = False) -> str:
    lines = [f"entry: {result.entry}",
             f"live sections: {len(result.live)}  "
             f"discarded: {len(result.ignored)}  "
             f"scrubbed lines: {len(result.scrubbed)}"]

    collisions = result.collisions()
    if collisions:
        lines.append(f"\nplain-name collisions kept by the engine: {len(collisions)}")
        for name, items in list(collisions.items())[:10]:
            lines.append(f"  {name}")
            for item in items[:4]:
                lines.append(f"    {item.name}  ({item.relative}:{item.section.line})")

    if show_ignored and result.ignored:
        lines.append("\ndiscarded duplicates:")
        for item in result.ignored:
            lines.append(f"  {item['action']:<18} [{item['section']}] "
                         f"{item['file']}:{item['line']} "
                         f"({item.get('commands', 0)} cmd, first at "
                         f"{item.get('first_line', '-')})")

    if show_includes and result.includes:
        lines.append("\ninclude statements:")
        for item in result.includes:
            lines.append(f"  {item['kind']:<18} {item['target']}  "
                         f"<- {item['file']}:{item['line']}")

    if result.scrubbed:
        lines.append(f"\ninjected banner lines removed: {len(result.scrubbed)}")
        for item in result.scrubbed[:5]:
            lines.append(f"  {item['file']}:{item['line']}  {item['text'][:80]}")

    if result.notes:
        lines.append("\nnotes:")
        for note in result.notes[:20]:
            lines.append(f"  {note}")
        if len(result.notes) > 20:
            lines.append(f"  ... {len(result.notes) - 20} more")
    return "\n".join(lines)


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(
        description="Resolve the effective section set of a 3Dmigoto source package.")
    parser.add_argument("package", help="source package directory")
    parser.add_argument("--entry", help="entry INI relative to the package root")
    parser.add_argument("--json", action="store_true", help="machine-readable output")
    parser.add_argument("--show-ignored", action="store_true",
                        help="list discarded duplicate sections")
    parser.add_argument("--show-includes", action="store_true",
                        help="list include statements and their resolution")
    parser.add_argument("--emit", metavar="PATH",
                        help="write the merged section set as one INI-shaped file")
    args = parser.parse_args(argv)

    result = merge(args.package, args.entry)
    if not result.live:
        print(render(result))
        return 2

    if args.emit:
        target = Path(args.emit)
        body = [f"; merged view, generated without modifying the source",
                f"; entry: {result.entry}", ""]
        for item in result.live:
            body.append(f"[{item.name}]  ; {item.relative}:{item.section.line}")
            body.append(item.section.body.rstrip("\n"))
            body.append("")
        target.write_text("\n".join(body), encoding="utf-8")
        print(f"wrote {target} ({len(result.live)} sections)")

    if args.json:
        payload = result.to_dict()
        payload["sections"] = [item.as_dict() for item in result.live]
        print(json.dumps(payload, ensure_ascii=False, indent=2))
    else:
        print(render(result, args.show_ignored, args.show_includes))

    findings = bool(result.ignored or result.notes)
    return 1 if findings else 0


if __name__ == "__main__":
    sys.exit(main())
