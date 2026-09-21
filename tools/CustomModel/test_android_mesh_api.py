"""Offline rejection/encoding cases for registration evidence; no game needed."""
import struct
import unittest
from inspect_android_mesh_api import Elf, decode_name, registration


class RegistrationTests(unittest.TestCase):
    def test_real_registration_stub(self):
        words = [0x90ffe0c0, 0xb0fffe41, 0x91013c00, 0x913be021, 0x140e1f15]
        self.assertEqual(registration(words, 0x5511a4), (0x16904f, 0x51aef8, 0x8d8e08))

    def test_wrong_register_and_next_stub_are_rejected(self):
        words = [0x90ffe0c0, 0xb0fffe41, 0x91013c00, 0x913be021, 0x140e1f15]
        for position in (0, 1, 2, 3):
            damaged = words.copy()
            damaged[position] ^= 2
            self.assertIsNone(registration(damaged, 0x5511a4))
        words[4] = 0xd65f03c0  # No registrar branch: don't scan into next stub.
        self.assertIsNone(registration(words, 0x5511a4))

    def test_plain_and_encoded_names(self):
        name = b"UnityEngine.Mesh/MeshDataArray::CreateNewMeshDatas"
        for key in (0, 0x52, 0xa7):
            data = bytes(x ^ key for x in name + b"\0")
            self.assertEqual(decode_name(data, 0, len(data)), (name.decode(), key))

    def test_name_must_end_in_segment(self):
        data = b"UnityEngine.Mesh::get_vertexCount\0"
        self.assertIsNone(decode_name(data, 0, len(data) - 1))
        self.assertIsNone(decode_name(data, len(data), len(data)))
        self.assertIsNone(decode_name(b"UnityEngine.MeshRenderer::test\0", 0, 99))

    def test_elf_header_and_segment_bounds(self):
        with self.assertRaises(ValueError):
            Elf(b"\x7fELF\x02\x01")
        data = bytearray(120)
        data[:6] = b"\x7fELF\x02\x01"
        struct.pack_into("<H", data, 18, 183)
        struct.pack_into("<Q", data, 32, 64)
        struct.pack_into("<HH", data, 54, 56, 1)
        struct.pack_into("<IIQQQQQQ", data, 64, 1, 5, 0, 0, 0, 120, 120, 4096)
        self.assertEqual(len(Elf(data).segments), 1)
        struct.pack_into("<Q", data, 64 + 32, 121)
        with self.assertRaises(ValueError):
            Elf(data)


if __name__ == "__main__":
    unittest.main()
