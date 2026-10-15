"""Read-only decoding of local OneChannelLC constants; no client execution."""
import json
import struct
from pathlib import Path
import pefile

ROOT = Path(__file__).resolve().parent
DLL = Path(r"G:\Arknights Endfield\OneChannelLC.dll")
pe = pefile.PE(str(DLL))


def packed(fmt, *numbers):
    sizes = {"B": 8, "H": 16, "I": 32, "Q": 64}
    return struct.pack("<" + fmt, *(n & ((1 << sizes[k]) - 1) for k, n in zip(fmt, numbers)))


def decode(data, count, start, key0, key1):
    return bytes((data[i] ^ (start - 125 * i) ^ (key0 >> (8 * (i & 3)))
                  ^ (key1 >> (8 * ((i + 1) & 3)))) & 255 for i in range(count)).decode("ascii")


keys = [
    (packed("QH", 0x263261618660B12, 2052), 10, 96, 0x83401645, 0x25965411),
    (packed("I", 180827616), 4, -6, 0x38BDE77E, 0x17CE0AD4),
    (packed("QI", 0xA803D8D050CB39C2, -583875289), 11, 113, 0x931E4236, 0xD0EEF55C),
    (packed("QI", 0xAA6B8FABA36980A4, -1621385293), 12, -126, 0x19C07BF0, 0x4F97B0D6),
]
apis = [
    (packed("QI", 0x1BD0DCF5B96F69CE, 562204433), 11, 113, 0x780A7983, 0x6B967F4B),
    (packed("QI", 0xB6AA15EDAAAB1DC4, -1702819675), 12, -126, 0xFEBC2D2F, 0xF15C3E2B),
    (packed("QIH", 0x43023D386719321E, 745346856, 17238), 14, -92, 0x2AAE184C, 0x7364A136),
    (pe.get_data(0x4E2B0, 16), 16, -58, 0xAFD014B9, 0xC660295D),
    (pe.get_data(0x4E240, 16) + packed("IIH", -905597488, -268400941, -23333), 26, 112, 0xEA5478D6, 0x9282C740),
    (pe.get_data(0x4E190, 16) + packed("IIH", -1284628460, -1989462506, 14357), 26, 112, 0x6C402641, 0xED5194B5),
    (pe.get_data(0x4E0A0, 16), 16, -58, 0xD994B47E, 0xBE4832BC),
    (packed("QIH", 0x950BB0DEB63DBBE5, -1660707363, -6681), 13, -109, 0x30A9E514, 0x743A21EA),
    (packed("QIH", 0x843A32A9973B2FBD, -464191101, 17376), 14, -92, 0xC620D9CE, 0xC3B49408),
]
result = {"config_keys": [decode(*row) for row in keys], "dynamic_apis": [decode(*row) for row in apis]}
(ROOT / "allowlist-decoded.json").write_text(json.dumps(result, indent=2), encoding="utf-8")
print(json.dumps(result, indent=2))
