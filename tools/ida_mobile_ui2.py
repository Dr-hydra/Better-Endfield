import os

import ida_funcs
import ida_hexrays
import ida_lines
import ida_name
import ida_nalt
import idaapi
import idautils


OUTPUT = r"E:\Dr.Hydra\Better Endfield\state\mobile-ui-ida-report2.txt"

TARGETS = {
    0x033FDF80: "get_inputType_COPY2",
    0x047A8C00: "get_inputType_META",
    0x049CFC80: "set_inputType_A",
    0x067CC5C0: "set_inputType_B",
    0x01206F80: "DeviceInfo_get_provider",
    0x047C4D30: "DeviceInfo_cctor",
    0x049CE960: "DeviceInfo_Init",
    0x034DFC60: "DeviceInfo_get_isAndroid",
    0x067CC3C0: "DeviceInfo_get_platform",
    0x067CC614: "DeviceInfo_set_platform",
}

WANT_XREFS = {
    "get_inputType_COPY2",
    "get_inputType_META",
    "set_inputType_A",
    "set_inputType_B",
    "DeviceInfo_Init",
    "DeviceInfo_set_platform",
    "DeviceInfo_get_platform",
}


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or f"sub_{ea:X}"


def main():
    base = idaapi.get_imagebase()
    for rva, name in TARGETS.items():
        ida_name.set_name(base + rva, name, ida_name.SN_FORCE)
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write(f"imagebase=0x{base:X}\n")
        for rva, name in TARGETS.items():
            ea = base + rva
            stream.write("\n" + "=" * 100 + "\n")
            stream.write(f"{name} rva=0x{rva:X} ea=0x{ea:X}\n")
            func = ida_funcs.get_func(ea)
            if func:
                stream.write(f"func_range={func.start_ea:016X}-{func.end_ea:016X}\n")
            if name in WANT_XREFS:
                stream.write("xrefs_to:\n")
                n = 0
                for ref in idautils.XrefsTo(ea, 0):
                    stream.write(f"  {ref.frm:016X} {safe_name(ref.frm)} type={ref.type}\n")
                    n += 1
                    if n >= 80:
                        stream.write("  <truncated>\n")
                        break
                if n == 0:
                    stream.write("  <none>\n")
            stream.write("disassembly:\n")
            if func:
                for i, item in enumerate(idautils.FuncItems(func.start_ea)):
                    if i >= 90:
                        stream.write("  <truncated>\n")
                        break
                    line = idaapi.generate_disasm_line(item, 0) or ""
                    stream.write(f"  {item:016X}  {ida_lines.tag_remove(line)}\n")
            else:
                stream.write("  <no function>\n")
    idaapi.qexit(0)


main()
