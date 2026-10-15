"""Small binary fixtures for the 1.6.3 VFS/PCK format changes."""
from __future__ import annotations

import re
import struct
import sys
import types
import unittest
from pathlib import Path

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import GenerateResourceManifests as manifests
import RefreshEndfieldResourceInputs as refresh


class ResourceFormatVersionTests(unittest.TestCase):
    def test_current_vfs_v5_keeps_file_boundaries_and_encryption_seed(self):
        def string(value):
            encoded = value.encode("ascii")
            return struct.pack("<H", len(encoded)) + encoded

        def reader(fmt):
            return lambda data, offset: (struct.unpack_from(fmt, data, offset)[0], offset + struct.calcsize(fmt))

        for version in (5,):
            with self.subTest(code_version=version):
                header = struct.pack("<i", 42) if version == 3 else struct.pack("<ii", version, 42)
                data = header + string("Table") + struct.pack("<qiqBi", 0, 2, 100, 7, 1)
                data += b"C" * 16 + b"D" * 16 + struct.pack("<qB", 100, 7)
                if version >= 4:
                    data += struct.pack("<i", 1)
                if version >= 5:
                    data += struct.pack("<i", 0x11223344)
                data += struct.pack("<i", 2)
                for index, name in enumerate(("AudioDialog", "CharacterTable")):
                    data += string(f"Data/TableCfg/{name}.bytes") + struct.pack("<q", 0)
                    data += b"C" * 16 + b"F" * 16 + struct.pack("<qqBB", index * 40, 40, 7, index == 0)
                    if index == 0:
                        data += struct.pack("<q", 1234567)
                    if version >= 4:
                        data += struct.pack("<i", 1)
                    if version >= 5:
                        data += struct.pack("<i", 0x55667788)
                unpacker = types.SimpleNamespace(
                    decrypt_blc=lambda _: data,
                    read_i32=reader("<i"), read_i64=reader("<q"),
                    read_u16=reader("<H"), read_u8=reader("<B"),
                    read_u128=lambda value, offset: (value[offset:offset + 16], offset + 16),
                    read_string=lambda value, offset, size: (value[offset:offset + size].decode("ascii"), offset + size),
                )
                rows = refresh.parse_blc_records(unpacker, Path("fixture.blc"), "Persistent", re.compile(r"TableCfg/.*"))
                self.assertEqual([row.relative_path for row in rows], ["TableCfg/AudioDialog.bytes", "TableCfg/CharacterTable.bytes"])
                self.assertEqual([(row.offset, row.size, row.iv_seed) for row in rows], [(0, 40, 1234567), (40, 40, 0)])

    def test_pck_language_names_support_both_current_encodings(self):
        for encoding, terminator in (("utf-8", b"\0"), ("utf-16-le", b"\0\0")):
            with self.subTest(encoding=encoding):
                chinese = "chinese".encode(encoding) + terminator
                sector = struct.pack("<IIIII", 2, 20, 1, 20 + len(chinese), 0)
                sector += chinese + "sfx".encode(encoding) + terminator
                self.assertEqual(manifests.parse_languages(sector, 0, len(sector)), {1: "chinese", 0: "sfx"})

    def test_pck_language_rejects_unterminated_wide_name(self):
        sector = struct.pack("<III", 1, 12, 1) + "chinese".encode("utf-16-le")
        with self.assertRaisesRegex(ValueError, "unterminated"):
            manifests.parse_languages(sector, 0, len(sector))


if __name__ == "__main__":
    unittest.main()
