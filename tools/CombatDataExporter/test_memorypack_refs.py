import struct
import unittest
import sys
from pathlib import Path
sys.path.insert(0, str(Path(__file__).resolve().parent))
from memorypack_refs import Reader
from string_pool import decompress_lz4


class Pool:
    def __init__(self): self.requested = []
    def get(self, index):
        self.requested.append(index)
        return f'buff_{index}'


class MemoryPackReferenceTests(unittest.TestCase):
    def test_only_string_fields_are_pool_references(self):
        pool = Pool()
        schema = {'Root': {'wrapper':'Root', 'fields':[['number','System.Int32'],['id','System.String']]}}
        reader = Reader(bytes([2])+struct.pack('<ii', 66403, 17), schema, pool)
        reader.read('Root')
        self.assertEqual(pool.requested, [17])
        self.assertEqual(reader.strings, {'buff_17'})
        self.assertEqual(reader.pos, len(reader.data))

    def test_struct_array_is_raw_but_list_uses_formatter(self):
        schema = {
            'Tag': {'wrapper':'Tag', 'raw_size':4, 'fields':[['id','System.Int32']]},
            'Root': {'wrapper':'Root', 'fields':[['array','Tag[]'],['list','System.Collections.Generic.List`1<Tag>'],['id','System.String']]},
        }
        data = bytes([3])+struct.pack('<iii',1,11,1)+bytes([1])+struct.pack('<ii',12,13)
        pool = Pool(); reader = Reader(data, schema, pool)
        reader.read('Root')
        self.assertEqual(reader.pos, len(data))
        self.assertEqual(pool.requested, [13])
        with self.assertRaises(ValueError): Reader(data[:-1], schema, Pool()).read('Root')

    def test_lz4_overlapping_copy_and_size_validation(self):
        packed = b'\x32abc\x03\x00\x10!'
        self.assertEqual(decompress_lz4(packed,10), b'abcabcabc!')
        with self.assertRaises(ValueError): decompress_lz4(packed,9)


if __name__ == '__main__': unittest.main()
