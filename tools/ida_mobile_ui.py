import os

import ida_funcs
import ida_hexrays
import ida_lines
import ida_name
import ida_nalt
import idaapi
import idautils


OUTPUT = os.path.join(os.path.dirname(os.path.dirname(__file__)), "state", "mobile-ui-ida-report.txt")

TARGETS = {
    # --- provider dispatch: the layer under every DeviceInfo getter ---
    0x049CE960: "DeviceInfo_Init",
    0x034DEF00: "LocalProvider_SupportsInputType",
    0x067C72F0: "CloudProvider_SupportsInputType",
    0x049CFC50: "LocalProvider_get_initialInputType",
    0x067C7398: "CloudProvider_get_initialInputType",
    0x067C91C4: "CloudGame_InitializeDeviceInfo",
    # --- DeviceInfo getters we already hook ---
    0x047A8C00: "DeviceInfo_get_inputType",
    0x067CC08C: "DeviceInfo_ChangeInputType",
    0x034DED20: "DeviceInfo_get_supportsTouch",
    0x033FFC40: "DeviceInfo_get_usingTouch",
    0x034DFB40: "DeviceInfo_get_isMobile",
    0x04502CD0: "DeviceInfo_get_isPC",
    0x067CC3C0: "DeviceInfo_get_platform",
    # --- UI consumers: what actually decides the layout ---
    0x03CEBEB0: "UIStyleByState_UpdateStyle",
    0x03CEC740: "UIStyleByState_IsValidStyle",
    0x03CECC10: "UIStyleByState_Awake",
    0x03CEC790: "UIWidget_IsValidInput",
    0x040F3500: "UIWidget_IsShowTypeValid",
    0x042EE960: "UIJoystick_CheckShouldActive",
    0x03092BD0: "UIJoystick_Activate",
    0x03968DA0: "HudFadeManager_GetNeedShow",
    0x036737B0: "HudFadeManager_InformAllFadingHud",
    0x0B12299C: "UIHudFadeTouchSkill_CheckNeedShow",
}

# Callers matter most for the dispatch layer: they tell us who selects the provider.
WANT_XREFS = {
    "DeviceInfo_Init",
    "DeviceInfo_ChangeInputType",
    "LocalProvider_SupportsInputType",
    "CloudProvider_SupportsInputType",
    "UIStyleByState_UpdateStyle",
    "HudFadeManager_InformAllFadingHud",
    "UIJoystick_CheckShouldActive",
}


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or f"sub_{ea:X}"


def write_xrefs(stream, ea):
    stream.write("xrefs_to:\n")
    count = 0
    for ref in idautils.XrefsTo(ea, 0):
        stream.write(f"  {ref.frm:016X} {safe_name(ref.frm)} type={ref.type}\n")
        count += 1
        if count >= 60:
            stream.write("  <truncated>\n")
            break
    if count == 0:
        stream.write("  <none>\n")


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


def write_disassembly(stream, ea, limit=120):
    func = ida_funcs.get_func(ea)
    if not func:
        stream.write("disassembly: <no function>\n")
        return
    stream.write("disassembly:\n")
    for index, item in enumerate(idautils.FuncItems(func.start_ea)):
        if index >= limit:
            stream.write("  <truncated>\n")
            break
        line = idaapi.generate_disasm_line(item, 0) or ""
        stream.write(f"  {item:016X}  {ida_lines.tag_remove(line)}\n")


def main():
    base = idaapi.get_imagebase()
    os.makedirs(os.path.dirname(OUTPUT), exist_ok=True)
    for rva, name in TARGETS.items():
        ida_name.set_name(base + rva, name, ida_name.SN_FORCE)
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write(f"input={ida_nalt.get_input_file_path()}\n")
        stream.write(f"imagebase=0x{base:X}\n")
        for rva, name in TARGETS.items():
            ea = base + rva
            stream.write("\n" + "=" * 100 + "\n")
            stream.write(f"{name} rva=0x{rva:X} ea=0x{ea:X}\n")
            if name in WANT_XREFS:
                write_xrefs(stream, ea)
            write_pseudocode(stream, ea)
            write_disassembly(stream, ea)
    idaapi.qexit(0)


main()
