import os

import ida_funcs
import ida_lines
import ida_nalt
import ida_ua
import idaapi
import idautils


OUTPUT = r"C:\Users\28377\AppData\Local\BetterEndfield\research\hud-key-field-xrefs.txt"
TARGET_OFFSETS = {0x4DC, 0xEC0, 0xEC4, 0xEFC, 0xF00}


def main():
    base = idaapi.get_imagebase()
    matches = []
    for func_ea in idautils.Functions():
        func = ida_funcs.get_func(func_ea)
        if not func:
            continue
        for ea in idautils.FuncItems(func.start_ea):
            insn = ida_ua.insn_t()
            if ida_ua.decode_insn(insn, ea) <= 0:
                continue
            hit = False
            for op in insn.ops:
                if op.type == ida_ua.o_void:
                    break
                if op.type == ida_ua.o_displ and op.addr in TARGET_OFFSETS:
                    hit = True
                    matched_offset = op.addr
                    break
            if hit:
                line = idaapi.generate_disasm_line(ea, 0) or ""
                matches.append((matched_offset, func.start_ea, ea, ida_lines.tag_remove(line)))

    os.makedirs(os.path.dirname(OUTPUT), exist_ok=True)
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write("input=%s imagebase=0x%X matches=%d\n" %
                     (ida_nalt.get_input_file_path(), base, len(matches)))
        for field_offset, func_ea, ea, line in matches:
            stream.write("field=0x%X func=0x%X rva=0x%X insn=0x%X +0x%X %s %s\n" %
                         (field_offset, func_ea, func_ea - base, ea, ea - func_ea,
                          ida_funcs.get_func_name(func_ea), line))
    idaapi.qexit(0)


main()
