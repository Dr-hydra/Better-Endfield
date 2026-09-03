import os

import ida_funcs
import ida_hexrays
import ida_lines
import ida_name
import ida_nalt
import idaapi
import idautils


OUTPUT = r"C:\Users\28377\AppData\Local\BetterEndfield\research\hud-callchain-ida-names.txt"
TERMS = (
    "showhideentirehud",
    "toggleclearscreen",
    "disablehudfade",
    "dramaticperformanceforbidlevelui",
    "toggleui",
    "clear_screen",
    "hidehud",
    "hidemainhud",
    "mainhud",
    "snapshot",
    "photo",
)


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or "sub_%X" % ea


def relevant(text):
    lowered = (text or "").lower()
    return any(term in lowered for term in TERMS)


def dump_function(stream, ea):
    func = ida_funcs.get_func(ea)
    if not func:
        return
    stream.write("\nFUNCTION 0x%X %s size=%d\n" %
                 (func.start_ea, safe_name(func.start_ea), func.end_ea - func.start_ea))
    stream.write("CALLERS\n")
    for ref in list(idautils.XrefsTo(func.start_ea, 0))[:80]:
        owner = ida_funcs.get_func(ref.frm)
        owner_ea = owner.start_ea if owner else ref.frm
        stream.write("  0x%X in 0x%X %s type=%d\n" %
                     (ref.frm, owner_ea, safe_name(owner_ea), ref.type))
    stream.write("CALLEES\n")
    seen = set()
    for item in idautils.FuncItems(func.start_ea):
        for ref in idautils.XrefsFrom(item, 0):
            target = ida_funcs.get_func(ref.to)
            if target and target.start_ea not in seen:
                seen.add(target.start_ea)
                stream.write("  0x%X from 0x%X %s type=%d\n" %
                             (target.start_ea, item, safe_name(target.start_ea), ref.type))
    stream.write("PSEUDOCODE\n")
    try:
        cfunc = ida_hexrays.decompile(func.start_ea)
        stream.write((str(cfunc) if cfunc else "<none>") + "\n")
    except Exception as exc:
        stream.write("<failed: %s>\n" % exc)
    stream.write("DISASSEMBLY\n")
    for index, item in enumerate(idautils.FuncItems(func.start_ea)):
        if index >= 180:
            stream.write("  <truncated>\n")
            break
        line = idaapi.generate_disasm_line(item, 0) or ""
        stream.write("  0x%X %s\n" % (item, ida_lines.tag_remove(line)))


def main():
    os.makedirs(os.path.dirname(OUTPUT), exist_ok=True)
    matches = []
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write("input=%s\n" % ida_nalt.get_input_file_path())
        stream.write("imagebase=0x%X\n" % idaapi.get_imagebase())
        stream.write("MATCHING NAMES\n")
        for ea, name in idautils.Names():
            if relevant(name):
                stream.write("  0x%X %s\n" % (ea, name))
                if ida_funcs.get_func(ea):
                    matches.append(ida_funcs.get_func(ea).start_ea)
        for ea in sorted(set(matches)):
            dump_function(stream, ea)
    idaapi.qexit(0)


main()
