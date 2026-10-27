import tempfile
import unittest
from pathlib import Path

from build_native_head_preview import bc7_image, mips, read_payload


class NativeHeadPreviewTests(unittest.TestCase):
    def test_authored_mip_is_preserved_without_reencoding(self):
        texture = dict(format='BC7', width=8, height=8, mips=4)
        data = b'a' * 64 + b'b' * 16 + b'c' * 16 + b'd' * 16
        first, levels = mips(texture, data, 4)
        self.assertEqual(first, 1)
        self.assertEqual([(w, h) for w, h, _ in levels], [(4, 4), (2, 2), (1, 1)])
        self.assertEqual(b''.join(p for _, _, p in levels), data[64:])

    def test_odd_dimensions_use_rounded_block_extents(self):
        texture = dict(format='BC7', width=9, height=5, mips=2)
        first, levels = mips(texture, b'a' * 96 + b'b' * 16, 4)
        self.assertEqual((first, levels), (1, [(4, 2, b'b' * 16)]))

    def test_rgba_original_material_texture_is_retained(self):
        texture = dict(format='RGBA32', width=2, height=2, mips=1)
        self.assertEqual(mips(texture, bytes(16), 2), (0, [(2, 2, bytes(16))]))

    def test_truncation_trailing_data_and_missing_mip_are_rejected(self):
        texture = dict(format='BC7', width=8, height=8, mips=1)
        for data, limit in [(bytes(63), 8), (bytes(65), 8), (bytes(64), 4)]:
            with self.subTest(length=len(data), limit=limit), self.assertRaises(ValueError):
                mips(texture, data, limit)

    def test_payload_path_escape_is_rejected(self):
        with tempfile.TemporaryDirectory() as directory:
            for name in ['../a.bin', 'sub/a.bin', 'sub\\a.bin']:
                with self.subTest(name=name), self.assertRaises(ValueError):
                    read_payload(Path(directory), name)

    def test_bc7_dds_decoder_preserves_requested_dimensions(self):
        image = bc7_image(4, 4, bytes([64]) + bytes(15))
        self.assertEqual(image.size, (4, 4))
        self.assertEqual(image.mode, 'RGBA')


if __name__ == '__main__':
    unittest.main()
