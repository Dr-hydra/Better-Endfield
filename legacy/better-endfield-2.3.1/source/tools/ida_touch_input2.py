import ida_bytes
import ida_funcs
import ida_hexrays
import ida_name
import ida_ua
import idaapi
import idautils


OUTPUT = r"E:\Dr.Hydra\Better Endfield\state\touch-input-ida-report2.txt"

TARGETS = {
    0x03798D80: "InputManager_GetTouchId",
    0x042EE640: "InputManager_GetPressPosByTouchId",
    0x0B09019C: "UIUtils_TryGetTouch",
    0x03091360: "UIJoystick_OnPointerDown",
    0x037913E0: "UIJoystick__Tick",
    0x045D0D60: "UITouchPanel_OnPointerDown",
    0x09E2FAD4: "InputManager__UpdateInputBindingMask",
    0x09E2F04C: "InputManager__SetInputBindingMask",
    0x09E279BC: "InputManager_ChangeInputTypeAndSyncState",
    0x09E2F920: "InputManager__TryChangeInputDeviceType",
    0x042EEF60: "unknown_activeTouches_candidate",
}


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or "sub_%X" % ea


def follow_thunk(ea):
    """A metadata RVA often lands on a jmp stub; walk to the real body."""
    seen = set()
    cur = ea
    for _ in range(8):
        if cur in seen:
            break
        seen.add(cur)
        insn = ida_ua.insn_t()
        if ida_ua.decode_insn(insn, cur) <= 0:
            break
        mnem = ida_ua.print_insn_mnem(cur)
        if mnem != "jmp":
            break
        target = insn.Op1.addr if insn.Op1.type in (idaapi.o_near, idaapi.o_far) else 0
        if not target:
            break
        cur = target
    return cur


def main():
    base = idaapi.get_imagebase()
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write("imagebase=0x%X\n" % base)
        for rva, name in TARGETS.items():
            ea = base + rva
            real = follow_thunk(ea)
            stream.write("\n" + "=" * 90 + "\n")
            stream.write("%s rva=0x%X ea=0x%X real=0x%X%s\n"
                         % (name, rva, ea, real, " [THUNK]" if real != ea else ""))
            stream.write("callers_of_real:\n")
            n = 0
            for ref in idautils.XrefsTo(real, 0):
                stream.write("  %016X %s\n" % (ref.frm, safe_name(ref.frm)))
                n += 1
                if n >= 30:
                    stream.write("  <truncated>\n")
                    break
            if n == 0:
                stream.write("  <none>\n")
            stream.write("pseudocode:\n")
            try:
                cfunc = ida_hexrays.decompile(real)
                stream.write(str(cfunc) if cfunc else "  <none>")
                stream.write("\n")
            except Exception as exc:
                stream.write("  <failed: %s>\n" % exc)
    idaapi.qexit(0)


main()
