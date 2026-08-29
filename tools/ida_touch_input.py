import ida_funcs
import ida_hexrays
import ida_lines
import ida_name
import idaapi
import idautils


OUTPUT = r"E:\Dr.Hydra\Better Endfield\state\touch-input-ida-report.txt"

TARGETS = {
    0x03798D80: "InputManager_GetTouchId",
    0x042EE640: "InputManager_GetPressPosByTouchId",
    0x0B09019C: "UIUtils_TryGetTouch",
    0x03091360: "UIJoystick_OnPointerDown",
    0x037913E0: "UIJoystick__Tick",
    0x042EE960: "UIJoystick_CheckShouldActive",
    0x03092BD0: "UIJoystick__Activate",
    0x03792E40: "UIJoystick_get_shouldCheckInput",
    0x033FEA50: "UITouchPanel_Tick",
    0x045D0D60: "UITouchPanel_OnPointerDown",
    0x037D6B90: "UITouchPanel__IsSameTouchFromPress",
    0x0B12299C: "UIHudFadeTouchSkill__CheckNeedShow",
}


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or "sub_%X" % ea


def main():
    base = idaapi.get_imagebase()
    for rva, name in TARGETS.items():
        ida_name.set_name(base + rva, name, ida_name.SN_FORCE)
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write("imagebase=0x%X\n" % base)
        for rva, name in TARGETS.items():
            ea = base + rva
            stream.write("\n" + "=" * 90 + "\n")
            stream.write("%s rva=0x%X ea=0x%X\n" % (name, rva, ea))
            stream.write("xrefs_to:\n")
            n = 0
            for ref in idautils.XrefsTo(ea, 0):
                stream.write("  %016X %s\n" % (ref.frm, safe_name(ref.frm)))
                n += 1
                if n >= 30:
                    stream.write("  <truncated>\n")
                    break
            if n == 0:
                stream.write("  <none>\n")
            stream.write("pseudocode:\n")
            try:
                cfunc = ida_hexrays.decompile(ea)
                stream.write(str(cfunc) if cfunc else "  <none>")
                stream.write("\n")
            except Exception as exc:
                stream.write("  <failed: %s>\n" % exc)
    idaapi.qexit(0)


main()
