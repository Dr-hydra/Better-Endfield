import ida_funcs
import ida_lines
import ida_name
import idaapi
import idautils


OUTPUT = r"E:\Dr.Hydra\Better Endfield\state\login-path-ida-report.txt"

TARGETS = {
    0x03C005C0: "DeviceInfo_get_userPlatform",
    0x04513960: "DeviceInfo_get_isMobileUser",
    0x045139F0: "DeviceInfo_get_isAndroidUser",
    0x04513A60: "DeviceInfo_get_isIOSUser",
    0x03C00570: "DeviceInfo_get_isPCUser",
    0x03C00520: "DeviceInfo_get_isPCorConsoleUser",
    0x03C00660: "Application_get_isCloudGame",
    0x03C00630: "CloudGame_get_enabled",
    0x067CAD20: "CloudGame_get_isMobilePlatform",
    0x067CAD9C: "CloudGame_get_isPCPlatform",
    0x0366DCC0: "Application_get_isMobilePlatform",
    0x037978C0: "Application_get_platform",
    0x034DFB40: "DeviceInfo_get_isMobile",
    0x034DFC60: "DeviceInfo_get_isAndroid",
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
            stream.write("\n" + "=" * 90 + "\n")
            stream.write(f"{name} rva=0x{rva:X}\n")
            stream.write("xrefs_to:\n")
            n = 0
            for ref in idautils.XrefsTo(ea, 0):
                stream.write(f"  {ref.frm:016X} {safe_name(ref.frm)}\n")
                n += 1
                if n >= 60:
                    stream.write("  <truncated>\n")
                    break
            if n == 0:
                stream.write("  <none>\n")
    idaapi.qexit(0)


main()
