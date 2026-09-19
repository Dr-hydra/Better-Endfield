import struct
import unittest
from inspect_shader_bindings import lz4_block, reflect_dxbc, descriptor_groups
from finalize_native_profile import exact_native_mips


class ShaderAndMaterialEvidenceTests(unittest.TestCase):
    def test_lz4_overlap_and_bounds(self):
        self.assertEqual(lz4_block(b'\x40test',4),b'test')
        self.assertEqual(lz4_block(b'\x14x\x01\x00',9),b'x'*9)
        for source,size in ((b'\x40tes',4),(b'\x00\x01\x00',4),(b'\x40test',3)):
            with self.assertRaises(ValueError): lz4_block(source,size)

    def test_dxbc_checks_chunk_bounds(self):
        blob=bytearray(b'DXBC'+bytes(16)+struct.pack('<4I',1,48,1,36))
        blob.extend(b'SHEX'+struct.pack('<2I',4,0x50))
        row=reflect_dxbc(bytes(blob))[0]
        self.assertEqual(row['stage'],0)
        self.assertFalse(row['reflection_present'])
        struct.pack_into('<I',blob,32,47)
        with self.assertRaises(ValueError): reflect_dxbc(bytes(blob))

    def test_descriptor_tail_is_bounded_and_versioned(self):
        def text(s):
            b=s.encode();return struct.pack('<I',len(b))+b+bytes((-len(b))%4)
        data=struct.pack('<6I',202506210,9,0,0,0,0)+struct.pack('<I',1)+text('Global')
        data+=struct.pack('<3I',0,1,0)+text('_BaseMap')+struct.pack('<4I',0,2,0xff0dff02,0x102ff)
        group=descriptor_groups(data)[0]
        self.assertEqual(group['bindings'][0]['pixel_slot'],13)
        self.assertIsNone(descriptor_groups(data+b'junk'))
        self.assertIsNone(descriptor_groups(bytes(8)+data[8:]))

    def test_native_texture_preservation_requires_bytes_and_format(self):
        native={'width':4,'height':4,'mips':2,'format':'BC7'}
        dds=(4,4,1,25,True,bytes(range(16)))
        self.assertEqual(exact_native_mips(dds,native,bytes(range(16))+bytes(16),{108}),16)
        with self.assertRaisesRegex(ValueError,'mip bytes'):
            exact_native_mips(dds,native,bytes(32),{108})
        with self.assertRaisesRegex(ValueError,'graphics format'):
            exact_native_mips(dds,native,bytes(range(16)),{109})
        with self.assertRaisesRegex(ValueError,'compression'):
            exact_native_mips(dds,{**native,'format':'BC5'},bytes(range(16)),{108})


if __name__=='__main__':unittest.main()
