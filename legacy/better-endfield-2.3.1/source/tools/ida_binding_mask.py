import ida_bytes
import ida_funcs
import ida_hexrays
import ida_name
import ida_ua
import idaapi
import idautils


OUTPUT = r"E:\Dr.Hydra\Better Endfield\state\binding-mask-ida-report.txt"

# Metadata RVAs from the IL2CPP dump. Several of these land on iFix hotfix
# stubs that break IDA's function boundaries, so each one is force-defined
# before use, and the live copy is found by walking callers rather than by
# trusting the address the metadata published.
TARGETS = {
    0x09E2F04C: "InputManager__SetInputBindingMask",
    0x09E2FAD4: "InputManager__UpdateInputBindingMask",
    0x09E279BC: "InputManager_ChangeInputTypeAndSyncState",
    0x09E2F920: "InputManager__TryChangeInputDeviceType",
    0x034DEF90: "InputManager__CheckInput",
    0x067CC08C: "DeviceInfo_ChangeInputType",
}


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or "sub_%X" % ea


def force_function(ea):
    func = ida_funcs.get_func(ea)
    if func and func.start_ea == ea and (func.end_ea - func.start_ea) > 16:
        return func
    if func:
        ida_funcs.del_func(func.start_ea)
    ida_bytes.del_items(ea, ida_bytes.DELIT_EXPAND, 64)
    ida_ua.create_insn(ea)
    ida_funcs.add_func(ea)
    return ida_funcs.get_func(ea)


def describe(stream, ea, label, depth=0):
    pad = "  " * depth
    func = ida_funcs.get_func(ea)
    start = func.start_ea if func else ea
    size = (func.end_ea - func.start_ea) if func else 0
    stream.write("%s%s @0x%X (func start 0x%X, size %d)\n"
                 % (pad, label, ea, start, size))
    callers = []
    for ref in idautils.XrefsTo(start, 0):
        callers.append(ref.frm)
    if not callers:
        stream.write("%s  callers: <none>\n" % pad)
    for caller in callers[:20]:
        owner = ida_funcs.get_func(caller)
        owner_start = owner.start_ea if owner else caller
        stream.write("%s  caller 0x%016X in %s (start 0x%X)\n"
                     % (pad, caller, safe_name(caller), owner_start))
    return [ida_funcs.get_func(c).start_ea
            for c in callers if ida_funcs.get_func(c)]


def main():
    base = idaapi.get_imagebase()
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write("imagebase=0x%X\n" % base)

        for rva, name in TARGETS.items():
            ea = base + rva
            force_function(ea)
            ida_name.set_name(ea, name, ida_name.SN_FORCE)

        # Walk up from _SetInputBindingMask: every caller is a copy of
        # _UpdateInputBindingMask, and only one of those copies is live.
        stream.write("\n" + "=" * 90 + "\n")
        stream.write("CALLER CHAIN UP FROM _SetInputBindingMask\n")
        stream.write("=" * 90 + "\n")
        set_mask = base + 0x09E2F04C
        level1 = describe(stream, set_mask, "_SetInputBindingMask", 0)
        seen = set()
        for owner in level1:
            if owner in seen:
                continue
            seen.add(owner)
            stream.write("\n")
            level2 = describe(stream, owner, "caller-of-SetInputBindingMask", 1)
            for grand in level2:
                if grand in seen:
                    continue
                seen.add(grand)
                describe(stream, grand, "caller-of-that", 2)

        for rva, name in TARGETS.items():
            ea = base + rva
            stream.write("\n" + "=" * 90 + "\n")
            stream.write("%s rva=0x%X ea=0x%X\n" % (name, rva, ea))
            describe(stream, ea, name, 0)
            stream.write("pseudocode:\n")
            try:
                cfunc = ida_hexrays.decompile(ea)
                stream.write(str(cfunc) if cfunc else "  <none>")
                stream.write("\n")
            except Exception as exc:
                stream.write("  <failed: %s>\n" % exc)

    idaapi.qexit(0)


main()
