#!/usr/bin/env python3
"""Enumerate reachable appearance states and resolve what each one actually draws.

``bem_tool inspect`` answers "is this package manual-only?". It cannot answer the
next question, which is the one that actually blocks work: *for each reachable
state, which draws run and which texture lands in each slot?* Without that, the
manual review starts from zero every time.

This probe answers it statically:

* walks the key sections to the reachable state set (BFS, with a budget)
* folds in persisted variable initial values — a persisted default is frequently
  *not* inside the key cycle, which silently costs one missing appearance
* evaluates every ``TextureOverride`` branch chain under each state and reports the
  one resource that wins, plus the branches that lost
* flags zero-index draws (they emit no triangles and can be dropped, but the
  surrounding texture/buffer state must stay)
* flags branches whose outcome depends on a value the source never defines
  (``$object_detected``, ``$lod_level``, instance counts) as assumptions

Zero third-party dependencies. Read-only.

Usage:
    python bem_state_probe.py "F:\\zmd\\伊冯\\某包"
    python bem_state_probe.py <package> --limit 256 --json
    python bem_state_probe.py <package> --ini mod.ini --state '{"$swapkey0":1}'
"""
from __future__ import annotations

import argparse
import itertools
import json
import re
import sys
from pathlib import Path

from bem_ini import Scenario, iter_active_inis, parse_constants

KEY_SECTION = re.compile(r"^(?:Key|Preset)\w*$", re.I)
TEXTURE_OVERRIDE = re.compile(r"^TextureOverride_(?:Texture|.*)$", re.I)
ASSIGN_VALUE = re.compile(r"^\s*(\$[\w.]+)\s*=\s*(.+)$")
CYCLE_LIST = re.compile(r"([^,]+)")
DRAW_ZERO = re.compile(r"^draw(?:indexed)?(?:instanced)?\s*=\s*0\s*,", re.I)
THIS_RESOURCE = re.compile(r"^this\s*=\s*(?:ref\s+)?(Resource\S+)$", re.I)

# Values the game supplies and the source never declares. Any branch that reads one
# of these is an assumption, and the report says so instead of guessing silently.
EXTERNAL = ("$object_detected", "$lod_detected", "$lod_level", "$mod_enabled",
            "$active", "$credits", "$costume", "$DRAW_TYPE")

# Interaction variables live next to appearance switches in the same INI, driven by
# KeyZoomIn / KeyMouseDrag rather than by an appearance toggle. Including them in the
# cross product inflates a 2-state package into thousands of meaningless states.
UI_VARIABLE = re.compile(
    r"^\$(?:zoom|img|cursor|mouse|rel_[xy]|freq|drag|pan|hud|win_[xy]|screen_)\w*$", re.I
)


def _clean(value: str) -> str:
    return value.split(";", 1)[0].strip()


def _is_ui_ini(relative: str) -> bool:
    """Author HUD scripts carry interaction variables, not appearance switches.

    A typical package mixes a mesh INI with a `快捷键/draw_image.ini` overlay that
    declares `$zoom0..6`, `$img0_x`, `$mouse_clicked` and friends. Feeding those into
    the appearance cross product multiplies the state space by thousands and buries
    the switches that actually change what the mesh looks like.
    """
    lowered = relative.replace("\\", "/").lower()
    return any(marker in lowered for marker in
               ("draw_image", "快捷键", "hud", "/ui/", "ui_", "menu.ini", "panel.ini"))


def parse_key_sections(documents, exclude_ui: bool = True) -> tuple[dict[str, list[int]], list[dict], list[str]]:
    """Return ``{variable: [reachable values]}`` from every appearance key section."""
    domain: dict[str, list[int]] = {}
    sections: list[dict] = []
    excluded: list[str] = []
    for relative, table in documents:
        if exclude_ui and _is_ui_ini(relative):
            if any(KEY_SECTION.match(name) for name in table):
                excluded.append(relative)
            continue
        for name, section in table.items():
            if not KEY_SECTION.match(name):
                continue
            variables: dict[str, list[int]] = {}
            for line in section.lines:
                match = ASSIGN_VALUE.match(line)
                if not match:
                    continue
                variable = match.group(1)
                raw = match.group(2)
                values = [int(part) for part in re.findall(r"-?\d+", raw)]
                if values:
                    variables.setdefault(variable, [])
                    for value in values:
                        if value not in variables[variable]:
                            variables[variable].append(value)
            if not variables:
                continue
            ui_named = [name for name in variables if UI_VARIABLE.match(name)]
            for name in ui_named:
                variables.pop(name)
                if name not in excluded:
                    excluded.append(name)
            if not variables:
                continue
            for variable, values in variables.items():
                merged = domain.setdefault(variable, [])
                for value in values:
                    if value not in merged:
                        merged.append(value)
            sections.append({
                "ini": relative,
                "section": name,
                "type": (section.value("type") or "cycle").strip(),
                "variables": {key: sorted(values) for key, values in variables.items()},
            })
    for variable in domain:
        domain[variable].sort()
    return domain, sections, excluded


def build_states(domain: dict[str, list[int]], persisted: dict[str, str],
                 budget: int) -> tuple[list[dict[str, int]], bool]:
    """Cross product of every switch domain, plus any persisted value not covered."""
    variables = sorted(domain)
    if not variables:
        base = {}
        for name, raw in persisted.items():
            try:
                base[name] = int(float(raw))
            except ValueError:
                continue
        return [base] if base else [{}], False

    total = 1
    for variable in variables:
        total *= max(1, len(domain[variable]))
    truncated = total > budget

    states: list[dict[str, int]] = []
    pools = [domain[variable][: max(1, len(domain[variable]))] for variable in variables]
    for combination in itertools.islice(itertools.product(*pools), budget):
        states.append(dict(zip(variables, combination)))

    # A persisted initial value outside the listed cycle is a real extra state: the
    # game starts there and the author may never return to it through the UI.
    extra = {}
    for name, raw in persisted.items():
        try:
            value = int(float(raw))
        except ValueError:
            continue
        if name in domain and value not in domain[name]:
            extra[name] = value
    if extra:
        template = dict(states[0]) if states else {}
        template.update(extra)
        if template not in states:
            states.append(template)
    return states, truncated


def walk(section_lines: list[str], scenario: Scenario) -> tuple[list[str], list[dict]]:
    """Return the statements that execute under ``scenario`` plus unresolved branches."""
    executed: list[str] = []
    assumptions: list[dict] = []
    stack: list[dict] = []
    active = True

    for line in section_lines:
        lowered = line.lower()
        if lowered.startswith("if "):
            result = scenario.resolve(line[3:])
            stack.append({"parent": active, "taken": bool(result), "known": result is not None})
            if result is None:
                assumptions.append({"condition": line[3:].strip(), "effect": "branch taken as unknown"})
            active = active and bool(result)
            continue
        if lowered.startswith("elif ") or lowered.startswith("else if "):
            if not stack:
                continue
            frame = stack[-1]
            expression = line.split(" ", 1)[1]
            if expression.lower().startswith("if "):
                expression = expression[3:]
            result = scenario.resolve(expression) if lowered.startswith("elif") else True
            active = frame["parent"] and not frame["taken"] and bool(result)
            frame["taken"] = frame["taken"] or bool(result)
            if result is None:
                assumptions.append({"condition": expression.strip(), "effect": "elif treated as unknown"})
            continue
        if lowered == "else":
            if not stack:
                continue
            frame = stack[-1]
            active = frame["parent"] and not frame["taken"]
            frame["taken"] = True
            continue
        if lowered == "endif":
            if stack:
                active = stack.pop()["parent"]
            continue
        if active:
            executed.append(line)
    return executed, assumptions


def resolve_textures(documents, states: list[dict[int, int]], assumptions_base: dict[str, float]):
    """For each state, resolve every TextureOverride chain to its winning resource."""
    resolved: list[dict] = []
    for index, state in enumerate(states):
        values = dict(assumptions_base)
        values.update({key: float(value) for key, value in state.items()})
        scenario = Scenario(values, assume=assumptions_base)
        entry = {"state_index": index, "assignments": state, "textures": [], "assumptions": []}
        for relative, table in documents:
            for name, section in table.items():
                if not TEXTURE_OVERRIDE.match(name) or SECTION_NOT_TEXTURE(name):
                    continue
                executed, assumptions = walk(section.lines, scenario)
                chosen, seen = None, []
                for statement in executed:
                    match = THIS_RESOURCE.match(statement)
                    if match:
                        chosen = match.group(1)
                        seen.append(statement)
                if assumed := [item for item in assumptions if item not in entry["assumptions"]]:
                    entry["assumptions"].extend(assumed)
                if chosen:
                    entry["textures"].append({
                        "section": name,
                        "identity": section.value("hash"),
                        "resource": chosen,
                        "branch_statements": seen[-3:],
                    })
        resolved.append(entry)
    return resolved


def SECTION_NOT_TEXTURE(name: str) -> bool:
    """Entry sections carry `this` only through texture replacement, not as a chain."""
    return bool(re.match(r"^TextureOverride_(?:EntryPoint_)?Component\d+", name, re.I))


def find_zero_draws(documents) -> list[dict]:
    found = []
    for relative, table in documents:
        for name, section in table.items():
            if not name.startswith(("CommandList", "TextureOverride")):
                continue
            for line in section.lines:
                if DRAW_ZERO.match(line):
                    found.append({"section": f"{relative} :: {name}", "statement": line,
                                  "semantics": "Zero index count: emits no triangles. Dropping the "
                                               "call is safe; keep the surrounding texture and "
                                               "buffer state statements."})
    return found


def _load_documents(root: Path, ini_filter: str | None):
    """Effective sections, preferring the loader-faithful merger.

    A dead duplicate section can declare its own ``Key`` block with a different
    variable domain, so resolving duplicates is not cosmetic here: it decides
    how many states the probe thinks exist.
    """
    try:
        from bem_merge import merge as merge_sources
    except ImportError:
        merge_sources = None
    if merge_sources is not None:
        result = merge_sources(root)
        if result.live:
            pairs = result.documents()
        else:
            pairs = list(iter_active_inis(root))
    else:
        pairs = list(iter_active_inis(root))
    if ini_filter is None:
        return pairs
    suffix = ini_filter.replace("\\", "/")
    return [(rel, table) for rel, table in pairs
            if rel.replace("\\", "/").endswith(suffix)]


def probe(package: str | Path, ini_filter: str | None = None, state_filter: dict | None = None,
          limit: int = 512) -> dict:
    root = Path(package).resolve()
    if not root.is_dir():
        raise SystemExit(f"Not a directory: {root}")

    documents = _load_documents(root, ini_filter)
    if not documents:
        raise SystemExit("No active INI matched.")

    domain, key_sections, ui_inis_excluded = parse_key_sections(documents)
    runtime, persisted = parse_constants(documents[0][1]) if len(documents) == 1 else ({}, {})
    for _, table in documents[1:]:
        extra_runtime, extra_persisted = parse_constants(table)
        runtime = {**extra_runtime, **runtime}
        persisted = {**extra_persisted, **persisted}

    states, truncated = build_states(domain, persisted, limit)
    if state_filter:
        merged = dict(states[0]) if states else {}
        merged.update({key: int(value) for key, value in state_filter.items()})
        states = [merged]

    # The probe fixes the values the game would supply, and records that it did.
    # `DRAW_TYPE` is a bare pseudo-variable (no `$`), unlike the author variables.
    baseline = {"$object_detected": 1.0, "$lod_detected": 0.0, "$lod_level": 0.0,
                "$mod_enabled": 1.0, "$DRAW_TYPE": 4.0, "DRAW_TYPE": 4.0,
                "$active": 0.0, "$costume": 0.0, "$credits": 0.0}
    resolved = resolve_textures(documents, states, baseline)

    risks = []
    if truncated:
        risks.append({"code": "STATE_SPACE_TRUNCATED",
                      "detail": f"Reachable space exceeds the {limit}-state budget; "
                                f"the listing covers the first {limit} combinations only."})
    unique = {json.dumps(item["textures"], sort_keys=True, ensure_ascii=False) for item in resolved}
    if len(unique) == 1 and len(resolved) > 1:
        risks.append({"code": "TEXTURE_PLAN_INVARIANT",
                      "detail": "Every reachable state resolves to the same texture overrides. The "
                                "switches drive draw selection or visibility, not material output; "
                                "keep them as draw-level options, not material parameters."})
    if persisted:
        outside = {name: value for name, value in persisted.items()
                   if name in domain and value.isdigit() and int(value) not in domain[name]}
        if outside:
            risks.append({"code": "PERSISTED_DEFAULT_OUTSIDE_CYCLE", "detail": outside,
                          "note": "A saved value the key cycle never reaches; the first launch "
                                  "state differs from every UI-reachable state."})

    return {
        "source": str(root),
        "inis": [rel for rel, _ in documents],
        "switch_variables": {name: values for name, values in domain.items()},
        "key_sections": key_sections[:32],
        "ui_inis_excluded": [item for item in ui_inis_excluded if not item.startswith("$")],
        "ui_variables_excluded": sorted(item for item in ui_inis_excluded if item.startswith("$")),
        "persisted_defaults": persisted,
        "state_count": len(states),
        "state_space_truncated": truncated,
        "distinct_texture_plans": len(unique),
        "assumed_values": baseline,
        "states": resolved[: min(len(resolved), 64)],
        "zero_draws": find_zero_draws(documents),
        "risks": risks,
        "note": "Texture resolution is static. It reports which resource a branch selects, not "
                "whether the game actually reaches that draw.",
    }


def render(report: dict) -> str:
    lines = [
        f"source        : {report['source']}",
        f"active INIs   : {', '.join(report['inis'])}",
        f"switch vars   : {len(report['switch_variables'])}",
        f"reachable     : {report['state_count']} states "
        f"({'TRUNCATED' if report['state_space_truncated'] else 'complete'}), "
        f"{report['distinct_texture_plans']} distinct texture plans",
        f"assumed       : " + ", ".join(f"{k}={int(v)}" for k, v in report["assumed_values"].items()),
    ]
    if report["switch_variables"]:
        lines.append("domains       :")
        for name, values in list(report["switch_variables"].items())[:12]:
            shown = ", ".join(str(value) for value in values[:12])
            more = "" if len(values) <= 12 else f", ... (+{len(values) - 12})"
            lines.append(f"  {name:24s} {shown}{more}")
    if report["persisted_defaults"]:
        lines.append("persisted     : " + ", ".join(
            f"{k}={v}" for k, v in report["persisted_defaults"].items()))
    if report.get("ui_inis_excluded"):
        lines.append("ui INIs skipped: " + ", ".join(report["ui_inis_excluded"]))
    if report.get("ui_variables_excluded"):
        lines.append("ui vars skipped: " + ", ".join(report["ui_variables_excluded"][:12]))
    if report["zero_draws"]:
        lines.append("")
        lines.append(f"ZERO-INDEX DRAWS ({len(report['zero_draws'])}) — safe to drop")
        for item in report["zero_draws"][:6]:
            lines.append(f"  {item['section']}")
            lines.append(f"      {item['statement']}")
    if report["risks"]:
        lines.append("")
        lines.append("RISKS")
        for risk in report["risks"]:
            lines.append(f"  [{risk['code']}] {str(risk.get('detail'))[:170]}")
    lines.append("")
    lines.append("FIRST STATES")
    for item in report["states"][:8]:
        assignment = ", ".join(f"{k}={v}" for k, v in item["assignments"].items()) or "(default)"
        lines.append(f"  #{item['state_index']}: {assignment}")
        for texture in item["textures"][:6]:
            lines.append(f"      {texture['section']:34s} -> {texture['resource']}")
        if item["assumptions"]:
            lines.append(f"      ! assumptions: {len(item['assumptions'])} unresolved branch(es)")
    return "\n".join(lines)


def _parse_state(raw: str | None) -> dict | None:
    if not raw:
        return None
    parsed = json.loads(raw)
    return {str(key): int(value) for key, value in parsed.items()}


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(description="Enumerate appearance states and resolve textures.")
    parser.add_argument("package", help="source package directory")
    parser.add_argument("--ini", help="only this INI (path suffix)")
    parser.add_argument("--state", help='force one state as JSON, e.g. {"$swapkey0":1}')
    parser.add_argument("--limit", type=int, default=512, help="state budget (default 512)")
    parser.add_argument("--json", action="store_true")
    args = parser.parse_args(argv)

    report = probe(args.package, args.ini, _parse_state(args.state), args.limit)
    print(json.dumps(report, ensure_ascii=False, indent=2) if args.json else render(report))
    return 0


if __name__ == "__main__":
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8")
    raise SystemExit(main())
