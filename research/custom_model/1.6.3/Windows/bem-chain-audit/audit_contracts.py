"""BEM-specific reuse of existing 1.6.3 metadata; no full extraction or execution."""
import collections
import json
import re
import struct
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[5]
OUT = Path(__file__).resolve().parent
sys.path.insert(0, str(ROOT / "tools/HookInlineScan"))
from audit_literal_contracts import METHOD, FIELD
from extract_targeted_metadata import Pe

prior = ROOT / "research/host/1.6.3/Windows/hook-update"
metadata = json.loads((prior / "targeted-metadata.json").read_text(encoding="utf-8"))
game = Path(metadata["source"]["game"])
for name, expected in metadata["source"]["files"].items():
    stat = (game / name).stat()
    if stat.st_size != expected["size"] or stat.st_mtime_ns != expected["mtime_ns"]:
        raise SystemExit(f"Metadata evidence is stale: {name}")
methods = collections.defaultdict(list)
for row in metadata["methods"]:
    methods[(row["asm"], row["cls"], row["name"], len(row["types"]))].append(row)
module = (ROOT / "native/modules/custom_model/module.cpp").read_text(encoding="utf-8")
table = module.split("MethodContract g_methods[]{", 1)[1].split("\n};", 1)[0]
# The descriptor table has only __ANDROID__ conditionals; reject unexpected ones.
stack = [True]
lines = []
for line in table.splitlines():
    if line.lstrip().startswith("#if"):
        directive = line.strip()
        if directive not in ("#if defined(__ANDROID__)", "#if !defined(__ANDROID__)", "#ifdef __ANDROID__", "#ifndef __ANDROID__"):
            raise SystemExit("Review new descriptor condition: " + directive)
        stack.append(stack[-1] and ("!defined" in directive or directive.startswith("#ifndef")))
    elif line.lstrip().startswith("#else"):
        stack[-1] = stack[-2] and not stack[-1]
    elif line.lstrip().startswith("#endif"):
        stack.pop()
    elif stack[-1]:
        lines.append(line)
table = "\n".join(lines)
records = []
for match in METHOD.finditer(table):
    asm, ns, cls, name, params, result, count = match.groups()
    full = (ns + "." if ns else "") + cls.replace("/", ".").replace("+", ".")
    prefix = table[:match.start()]
    key = re.search(r'\{\s*"([^"\n]+)"\s*,\s*$', prefix).group(1)
    suffix = table[match.end():]
    required = re.match(r'\s*,\s*(true|false)', suffix).group(1) == "true"
    candidates = methods.get((asm, full, name, int(count)), [])
    expected = params.strip('"').split("|") if params.strip('"') else []
    exact = [m for m in candidates if (params == "nullptr" or m["types"] == expected)
             and (result == "nullptr" or m["ret"] == result.strip('"'))]
    records.append(dict(key=key, required=required, assembly=asm, cls=full, name=name,
                        parameters=params, returns=result, status="matched" if exact else "missing-or-mismatch",
                        rvas=[hex(m["rva"]) for m in exact]))
hooks = [r for r in json.loads((prior / "hook-inline-scan.json").read_text(encoding="utf-8"))
         if r["hook"]["module"] == "custom_model"]
player = Pe(game / "UnityPlayer.dll")
icalls = []
for name in re.findall(r'\{"(UnityEngine\.Mesh::[^"\n]+)"', module):
    offset = player.data.find(name.encode() + b"\0")
    refs = list(player.references(player.va(offset))) if offset >= 0 else []
    icalls.append(dict(name=name, string_rva=hex(player.va(offset)-player.base) if offset >= 0 else None,
                       pointer_reference_rvas=[hex(player.va(p)-player.base) for p in refs],
                       scope="Name registration evidence; no icall execution or ABI inference"))
layouts = json.loads((OUT / "value-layouts.json").read_text(encoding="utf-8"))["classes"]
layouts += [c for c in metadata["classes"] if c["cls"] in (
    "UnityEngine.Object", "UnityEngine.Bounds", "UnityEngine.Matrix4x4", "UnityEngine.Rendering.VertexAttributeDescriptor")]
summary = dict(windows_descriptors=len(records), required=sum(r["required"] for r in records),
               matched=sum(r["status"] == "matched" for r in records), hooks=len(hooks),
               hook_verdicts=dict(collections.Counter(h["verdict"] for h in hooks)),
               icall_names_found=sum(x["string_rva"] is not None and bool(x["pointer_reference_rvas"]) for x in icalls),
               icall_count=len(icalls))
output = dict(scope=__doc__, input_evidence_current=True, summary=summary, descriptors=records,
              hooks=hooks, mesh_icalls=icalls, layouts=layouts)
(OUT / "contracts.json").write_text(json.dumps(output, ensure_ascii=False, indent=2), encoding="utf-8")
print(json.dumps({"summary": summary, "review": [r for r in records if r["status"] != "matched"]}, ensure_ascii=False))
