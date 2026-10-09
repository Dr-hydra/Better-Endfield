"""Generate the Windows-31J double-byte lookup used by the Android VMD reader."""
from pathlib import Path
pairs = []
for lead in range(256):
    for trail in range(256):
        try:
            decoded = bytes([lead, trail]).decode("cp932")
        except UnicodeDecodeError:
            continue
        if len(decoded) == 1:
            pairs.append(((lead << 8 | trail) << 16) | ord(decoded))
text = "// Generated from Python cp932 (Windows-31J); regenerate with compat/generate_cp932.py.\n#pragma once\n#include <cstdint>\nnamespace BetterEndfieldNext::EiemAndroid {\ninline constexpr uint32_t kCp932Pairs[] = {\n"
for i in range(0, len(pairs), 8):
    text += "  " + ", ".join(f"0x{x:08x}u" for x in pairs[i:i+8]) + ",\n"
text += "};\n}\n"
Path(__file__).with_name("android_cp932_table.h").write_text(text, encoding="utf-8")
