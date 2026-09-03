import os

import ida_bytes
import ida_funcs
import ida_hexrays
import ida_lines
import ida_nalt
import ida_ua
import idaapi
import idautils


OUTPUT = r"C:\Users\28377\AppData\Local\BetterEndfield\research\camera-mask-ida-report.txt"
TARGETS = {
    0x0394D540: "CameraManager_AddUICamCullingMaskConfig",
    0x0394D950: "CameraManager_RemoveUICamCullingMaskConfig",
    0x0394D880: "CameraManager_UpdateUICamCullingMask",
    0x06BDBAB8: "CameraUtils_get_cameraManager",
}


def force_function(ea):
    func = ida_funcs.get_func(ea)
    if func:
        return func
    ida_bytes.del_items(ea, ida_bytes.DELIT_EXPAND, 32)
    ida_ua.create_insn(ea)
    ida_funcs.add_func(ea)
    return ida_funcs.get_func(ea)


def main():
    base = idaapi.get_imagebase()
    os.makedirs(os.path.dirname(OUTPUT), exist_ok=True)
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write("input=%s imagebase=0x%X\n" %
                     (ida_nalt.get_input_file_path(), base))
        for rva, label in TARGETS.items():
            func = force_function(base + rva)
            stream.write("\n%s rva=0x%X ea=0x%X size=%d\n" %
                         (label, rva, func.start_ea,
                          func.end_ea - func.start_ea))
            try:
                cfunc = ida_hexrays.decompile(func.start_ea)
                stream.write((str(cfunc) if cfunc else "<none>") + "\n")
            except Exception as exc:
                stream.write("<decompile failed: %s>\n" % exc)
            stream.write("DISASSEMBLY\n")
            for index, ea in enumerate(idautils.FuncItems(func.start_ea)):
                if index >= 300:
                    stream.write("<truncated>\n")
                    break
                line = idaapi.generate_disasm_line(ea, 0) or ""
                stream.write("  0x%X %s\n" %
                             (ea, ida_lines.tag_remove(line)))
    idaapi.qexit(0)


main()
