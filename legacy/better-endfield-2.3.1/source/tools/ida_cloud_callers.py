import ida_funcs
import ida_hexrays
import ida_lines
import ida_name
import idaapi
import idautils


OUTPUT = r"E:\Dr.Hydra\Better Endfield\state\cloud-callers-ida-report.txt"

TARGETS = {
    0x0120A200: "isCloudGame_caller_A",
    0x000B57B0: "isCloudGame_caller_B",
    0x02FA8060: "ChangeInputType_gate",
}


def safe_name(ea):
    return ida_funcs.get_func_name(ea) or ida_name.get_name(ea) or f"sub_{ea:X}"


def main():
    base = idaapi.get_imagebase()
    with open(OUTPUT, "w", encoding="utf-8") as stream:
        stream.write(f"imagebase=0x{base:X}\n")
        for rva, label in TARGETS.items():
            ea = base + rva
            stream.write("\n" + "=" * 90 + "\n")
            stream.write(f"{label} rva=0x{rva:X} ea=0x{ea:X}\n")
            stream.write("xrefs_to:\n")
            n = 0
            for ref in idautils.XrefsTo(ea, 0):
                stream.write(f"  {ref.frm:016X} {safe_name(ref.frm)}\n")
                n += 1
                if n >= 40:
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
                stream.write(f"  <failed: {exc}>\n")
            stream.write("disassembly:\n")
            func = ida_funcs.get_func(ea)
            if func:
                for i, item in enumerate(idautils.FuncItems(func.start_ea)):
                    if i >= 70:
                        stream.write("  <truncated>\n")
                        break
                    line = idaapi.generate_disasm_line(item, 0) or ""
                    stream.write(f"  {item:016X}  {ida_lines.tag_remove(line)}\n")
            else:
                stream.write("  <no function>\n")
    idaapi.qexit(0)


main()
