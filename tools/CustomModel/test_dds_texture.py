import struct
import unittest

from convert_efmi_poc import parse_dds


def legacy(width,height,kind,pixels,mips=1):
    raw=bytearray(128);raw[:4]=b'DDS '
    bpp=1 if kind=='r8' else 4
    struct.pack_into('<7I',raw,4,124,0x100f,height,width,width*bpp,0,mips)
    if kind=='r8':pf=(32,0x20000,0,8,255,0,0,0)
    elif kind=='bgra':pf=(32,0x41,0,32,0xff0000,0xff00,0xff,0xff000000)
    else:pf=(32,0x41,0,32,0xff,0xff00,0xff0000,0xff000000)
    struct.pack_into('<8I',raw,76,*pf)
    return bytes(raw)+pixels


class DdsTests(unittest.TestCase):
    def test_rgba_with_zero_fourcc_and_non_block_dimensions(self):
        data=bytes(range(24))
        self.assertEqual(parse_dds('rgba.dds',legacy(3,2,'rgba',data)),(3,2,1,4,False,data))

    def test_r8_mips_are_not_expanded_to_luminance_rgba(self):
        data=bytes(range(7))
        self.assertEqual(parse_dds('r8.dds',legacy(3,2,'r8',data,2)),(3,2,2,63,False,data))

    def test_bgra_swizzle_preserves_alpha_and_mips(self):
        data=bytes([1,2,3,4,5,6,7,8,9,10,11,12])
        parsed=parse_dds('bgra.dds',legacy(2,1,'bgra',data,2))
        self.assertEqual(parsed[-1],bytes([3,2,1,4,7,6,5,8,11,10,9,12]))

    def test_dx10_typed_color_and_array_rejection(self):
        raw=bytearray(legacy(1,1,'rgba',b''));raw[84:88]=b'DX10'
        raw+=struct.pack('<5I',29,3,0,1,0)+b'rgba'
        self.assertEqual(parse_dds('dx10.dds',bytes(raw))[3:],(4,True,b'rgba'))
        struct.pack_into('<I',raw,140,2)
        with self.assertRaisesRegex(ValueError,'DDS_FORMAT'):parse_dds('array.dds',bytes(raw))

    def test_truncated_unknown_masks_padded_and_cube_are_rejected(self):
        good=legacy(3,2,'rgba',bytes(24))
        bad_masks=bytearray(good);struct.pack_into('<I',bad_masks,92,0xff000000)
        padded=bytearray(good);struct.pack_into('<I',padded,20,16)
        cube=bytearray(good);struct.pack_into('<I',cube,112,0x200)
        for bad in (good[:-1],bytes(bad_masks),bytes(padded),bytes(cube)):
            with self.subTest(bad=bad[:28]):
                with self.assertRaises(ValueError):parse_dds('bad.dds',bad)


if __name__=='__main__':unittest.main()
