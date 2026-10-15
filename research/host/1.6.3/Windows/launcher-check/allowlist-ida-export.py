import json
from pathlib import Path
import ida_auto
import ida_funcs
import ida_hexrays
import ida_nalt
import idautils
import idc

OUT = Path(r"G:\Better Endfield\research\host\1.6.3\Windows\launcher-check")
OUT.mkdir(parents=True, exist_ok=True)
ida_auto.auto_wait()
ida_hexrays.init_hexrays_plugin()
base = ida_nalt.get_imagebase()
seeds = [0x89A0, 0x8B60, 0x8C70, 0x8D00, 0xDB10, 0xEA70, 0x13AA0, 0x13B70, 0x140C0, 0x141D0, 0x151F0, 0x15330, 0x15470, 0x2D420, 0x2D9D0, 0x2DC60, 0x2DC80]
targets = set(base + rva for rva in seeds)
for ea in list(targets):
    for x in idautils.XrefsTo(ea):
        f = ida_funcs.get_func(x.frm)
        if f:
            targets.add(f.start_ea)
result = []
for ea in sorted(targets):
    callers = []
    for x in idautils.XrefsTo(ea):
        f = ida_funcs.get_func(x.frm)
        callers.append({"from": hex(x.frm), "function": hex(f.start_ea) if f else None})
    try:
        code = str(ida_hexrays.decompile(ea))
    except Exception as exc:
        code = "ERROR: " + repr(exc)
    name = idc.get_func_name(ea)
    (OUT / ("allowlist-function-" + hex(ea - base)[2:] + ".c")).write_text(code, encoding="utf-8")
    result.append({"address": hex(ea), "name": name, "callers": callers, "chars": len(code)})
(OUT / "allowlist-ida-index.json").write_text(json.dumps(result, indent=2), encoding="utf-8")
idc.qexit(0)
