import os

import ida_bytes
import ida_funcs
import ida_hexrays
import ida_lines
import ida_name
import ida_nalt
import ida_ua
import idaapi
import idautils


OUTPUT = r"C:\Users\28377\AppData\Local\BetterEndfield\research\hud-callchain-ida-report.txt"
TARGETS = {
    0x075E0414: "GameAction_DisableHudFade",
    0x075E0A88: "GameAction_DramaticPerformanceForbidLevelUI",
    0x03C200C0: "GameAction_ToggleUI",
    0x0423A270: "LuaEventSystem_DispatchEvent_String",
    0x0768FCEC: "ShowHideEntireHUD_Execute",
    0x06AAC61C: "ToggleClearScreen_Execute",
    0x06AAC43C: "ToggleClearScreenButRadio_Execute",
    0x03F0CCE0: "ToggleUI_Core",
    0x03B9D810: "ToggleUI_StateDictionarySet",
    0x038E8F70: "ToggleUI_StateLookup",
    0x0769C884: "ToggleUI_StateApply",
    0x03BE2A40: "ToggleUI_CallbackFilterContains",
    0x03192670: "ToggleUI_CallbackLookup",
    0x03195AF0: "ToggleUI_CallbackTarget",
    0x000995D0: "ToggleUI_CallbackInvoke",
    0x01852060: "ToggleUI_HotfixPath",
    0x036D0BB0: "DisableHudFade_Core",
    0x06375088: "DisableHudFade_HotfixPath",
    0x03CD8810: "DramaticPerformance_SetCategory",
    0x032F1D80: "ToggleUI_GetController",
    0x03124F10: "LuaEventSystem_QueueOrDispatch",
    0x0329E9F0: "LuaEventSystem_PCall",
}


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or "sub_%X" % ea


def force_function(ea):
    func = ida_funcs.get_func(ea)
    if func:
        return func
    ida_bytes.del_items(ea, ida_bytes.DELIT_EXPAND, 32)
    ida_ua.create_insn(ea)
    ida_funcs.add_func(ea)
    return ida_funcs.get_func(ea)


def direct_callers(ea):
    callers = []
    func = ida_funcs.get_func(ea)
    target = func.start_ea if func else ea
    for ref in idautils.XrefsTo(target, 0):
        owner = ida_funcs.get_func(ref.frm)
        if owner and owner.start_ea not in callers:
            callers.append(owner.start_ea)
    return callers


def direct_callees(ea):
    callees = []
    func = ida_funcs.get_func(ea)
    if not func:
        return callees
    for item in idautils.FuncItems(func.start_ea):
        for ref in idautils.XrefsFrom(item, 0):
            target = ida_funcs.get_func(ref.to)
            if target and target.start_ea not in callees:
                callees.append(target.start_ea)
    return callees


def dump_function(stream, ea, include_disassembly=True):
    func = force_function(ea)
    if not func:
        stream.write("FUNCTION 0x%X <not defined>\n" % ea)
        return
    stream.write("\n" + "=" * 100 + "\n")
    stream.write("FUNCTION 0x%X rva=0x%X %s size=%d\n" %
                 (func.start_ea, func.start_ea - idaapi.get_imagebase(),
                  safe_name(func.start_ea), func.end_ea - func.start_ea))
    stream.write("CALLERS\n")
    for caller in direct_callers(func.start_ea)[:80]:
        stream.write("  0x%X rva=0x%X %s\n" %
                     (caller, caller - idaapi.get_imagebase(), safe_name(caller)))
    stream.write("CALLEES\n")
    for callee in direct_callees(func.start_ea)[:80]:
        stream.write("  0x%X rva=0x%X %s\n" %
                     (callee, callee - idaapi.get_imagebase(), safe_name(callee)))
    stream.write("PSEUDOCODE\n")
    try:
        cfunc = ida_hexrays.decompile(func.start_ea)
        stream.write((str(cfunc) if cfunc else "<none>") + "\n")
    except Exception as exc:
        stream.write("<failed: %s>\n" % exc)
    if include_disassembly:
        stream.write("DISASSEMBLY\n")
        for index, item in enumerate(idautils.FuncItems(func.start_ea)):
            if index >= 220:
                stream.write("  <truncated>\n")
                break
            line = idaapi.generate_disasm_line(item, 0) or ""
            stream.write("  0x%X %s\n" % (item, ida_lines.tag_remove(line)))


def main():
    base = idaapi.get_imagebase()
    os.makedirs(os.path.dirname(OUTPUT), exist_ok=True)
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write("input=%s\nimagebase=0x%X\n" %
                     (ida_nalt.get_input_file_path(), base))
        target_eas = []
        for rva, label in TARGETS.items():
            ea = base + rva
            force_function(ea)
            stream.write("TARGET %s rva=0x%X ea=0x%X existing_name=%s\n" %
                         (label, rva, ea, safe_name(ea)))
            target_eas.append(ea)
        dumped = set()
        for target in target_eas:
            dump_function(stream, target)
            dumped.add(ida_funcs.get_func(target).start_ea)
            for caller in direct_callers(target)[:40]:
                if caller not in dumped:
                    dump_function(stream, caller)
                    dumped.add(caller)
    idaapi.qexit(0)


main()
