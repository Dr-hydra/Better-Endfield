import os

import ida_auto
import ida_bytes
import ida_funcs
import ida_hexrays
import ida_lines
import ida_name
import ida_nalt
import idaapi
import idautils


OUTPUT = os.path.join(os.path.dirname(os.path.dirname(__file__)), "state", "rdps-ida-report.txt")

TARGETS = {
    0x03ED5EA0: "BattleRecorder_RecordDamage",
    0x06D83BC0: "BattleRecorder_RecordDamageDetail",
    0x06D868DC: "DamageDetail_Init",
    0x039382D0: "Modifier_Apply",
    0x03938430: "AbilitySystem_ApplyModifier",
    0x0393A3D0: "AbilitySystem_DoApplyModifier",
    0x0353F4C0: "DamageAction_ProcessDamage",
    0x03B51DC0: "DamageAction_CalculateDamageResultByType",
    0x03B51EE0: "DamageAction_CalculateDamageResultForNormalEntity",
    0x034222D0: "DamagePackData_Create",
    0x03A8FEE0: "DamagePackData_ApplyDamageModifier",
    0x03B54760: "DamagePackData_GetFinalAttackValue",
    0x03B55B80: "DamagePackData_GetFinalDamageScale",
    0x03B53700: "BattleFormula_CalculateDamage",
    0x03B54810: "BattleFormula_GetDefResistanceValue",
    0x03B54E80: "BattleFormula_GetDamageTypeResistanceValue",
    0x0341C580: "Attributes_CalculateFinalAttributeValue",
    0x03419890: "Attributes_CalculateBaseAttributeValue",
    0x06B86DBC: "AttributesCalculator_CalculateFinalAttribute",
    0x06B86C4C: "AttributesCalculator_CalculateBaseAttribute",
    0x06D3F150: "Attributes_GetAllModifierValues",
    0x06D3FFF4: "Attributes_GetRawValue",
    0x03B4FEF0: "DamageModifier_ApplyModifier",
    0x03B50B70: "DamageModifier_DoProcessDamagePackData",
    0x03B50CE0: "DamageProcessorBase_ProcessDamagePackData",
    0x044B14B0: "DamageScaleProcessor_ProcessDamagePackDataInternal",
    0x02F4C7D0: "BattleRecorder_RecordBuffStart",
    0x03ED47E0: "BattleRecorder_RecordBuffFinish",
    0x06D86484: "BuffDetail_Init",
    0x03E98C40: "Buff_AddModifier",
    0x03E98860: "Buff_RemoveModifier",
    0x03E99010: "Buff_GetDamageModifiers",
    0x02F4C850: "Buff_OnEnable",
    0x02F4B1D0: "Buff_MarkFinish",
    0x03ED5F20: "BattleRecorder_get_inDetailRecording",
    0x06D82C54: "BattleRecorder_SetInDetailRecording",
}

FULL_DISASSEMBLY = {
    "DamagePackData_ApplyDamageModifier",
    "DamageModifier_ApplyModifier",
    "DamageAction_CalculateDamageResultForNormalEntity",
    "BattleFormula_CalculateDamage",
}


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or f"sub_{ea:X}"


def write_xrefs(stream, ea):
    stream.write("xrefs_to:\n")
    for ref in idautils.XrefsTo(ea, 0):
        stream.write(f"  {ref.frm:016X} {safe_name(ref.frm)} type={ref.type}\n")
    stream.write("calls_from:\n")
    func = ida_funcs.get_func(ea)
    if not func:
        stream.write("  <no function>\n")
        return
    seen = set()
    for item in idautils.FuncItems(func.start_ea):
        for ref in idautils.CodeRefsFrom(item, False):
            if ref in seen or (func.start_ea <= ref < func.end_ea):
                continue
            seen.add(ref)
            stream.write(f"  {item:016X} -> {ref:016X} {safe_name(ref)}\n")


def write_disassembly(stream, ea, limit=220):
    func = ida_funcs.get_func(ea)
    if not func:
        stream.write("disassembly: <no function>\n")
        return
    stream.write(f"range={func.start_ea:016X}-{func.end_ea:016X} size=0x{func.end_ea-func.start_ea:X}\n")
    stream.write("disassembly:\n")
    for index, item in enumerate(idautils.FuncItems(func.start_ea)):
        if index >= limit:
            stream.write("  <truncated>\n")
            break
        line = idaapi.generate_disasm_line(item, 0) or ""
        stream.write(f"  {item:016X}  {ida_lines.tag_remove(line)}\n")


def write_pseudocode(stream, ea):
    stream.write("pseudocode:\n")
    try:
        cfunc = ida_hexrays.decompile(ea)
        if not cfunc:
            stream.write("  <decompile returned none>\n")
            return
        stream.write(str(cfunc))
        stream.write("\n")
    except Exception as exc:
        stream.write(f"  <decompile failed: {exc}>\n")


def main():
    base = idaapi.get_imagebase()
    os.makedirs(os.path.dirname(OUTPUT), exist_ok=True)
    for rva, name in TARGETS.items():
        ea = base + rva
        ida_name.set_name(ea, name, ida_name.SN_FORCE)
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write(f"input={ida_nalt.get_input_file_path()}\n")
        stream.write(f"imagebase=0x{base:X}\n")
        for rva, name in TARGETS.items():
            ea = base + rva
            stream.write("\n" + "=" * 100 + "\n")
            stream.write(f"{name} rva=0x{rva:X} ea=0x{ea:X}\n")
            write_xrefs(stream, ea)
            write_pseudocode(stream, ea)
            write_disassembly(stream, ea, 2500 if name in FULL_DISASSEMBLY else 220)
    idaapi.qexit(0)


main()
