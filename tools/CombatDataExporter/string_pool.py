"""Read the game's local USPC string-pool chunks without external packages."""
import struct
from pathlib import Path


def decompress_lz4(data: bytes, expected_size: int) -> bytes:
    out = bytearray()
    pos = 0
    def length(initial):
        nonlocal pos
        size = initial
        if initial == 15:
            while True:
                extra = data[pos]
                pos += 1
                size += extra
                if extra != 255:
                    break
        return size
    while pos < len(data):
        token = data[pos]
        pos += 1
        literals = length(token >> 4)
        if pos+literals > len(data):
            raise ValueError('Truncated LZ4 literals')
        out.extend(data[pos:pos+literals])
        pos += literals
        if pos == len(data):
            break
        offset = struct.unpack_from('<H', data, pos)[0]
        pos += 2
        match = length(token & 15)+4
        if offset == 0 or offset > len(out) or len(out)+match > expected_size:
            raise ValueError('Invalid LZ4 back reference')
        pattern = out[-offset:]
        out.extend((pattern*((match+offset-1)//offset))[:match])
    if len(out) != expected_size:
        raise ValueError(f'LZ4 size mismatch: {len(out)} != {expected_size}')
    return bytes(out)


def read_chunk(path: Path):
    data = path.read_bytes()
    offset = 4 if data[4:8] == b'USPC' else 0
    if data[offset:offset+4] != b'USPC':
        raise ValueError(f'Invalid USPC header: {path}')
    version, flags, count, size, packed_size = struct.unpack_from('<5I', data, offset+4)
    if version != 1 or flags != 1 or offset+24+packed_size != len(data):
        raise ValueError(f'Unsupported USPC layout: {path}')
    raw = decompress_lz4(data[offset+24:], size)
    return count, raw


class StringPool:
    def __init__(self, root: Path):
        self.root = root
        self.chunks = {}
        self.ranges = []
        self.count = 0

    def _load_next(self):
        path = self.root/f'StringPool{len(self.chunks)}.byte'
        try:
            count, raw = read_chunk(path)
        except FileNotFoundError as error:
            raise ValueError(f'String index outside pool: {self.count}') from error
        if len(raw) < 4 or struct.unpack_from('<I', raw)[0] != count:
            raise ValueError(f'Invalid StringPool count: {path}')
        self.ranges.append((self.count, self.count+count))
        self.chunks[len(self.chunks)] = (count, raw)
        self.count += count

    def get(self, index: int):
        if index == -2:
            return None
        if index == -1:
            return ''
        if index < 0:
            raise ValueError(f'Invalid string index {index}')
        while index >= self.count:
            self._load_next()
        chunk = next(i for i, (start, end) in enumerate(self.ranges) if start <= index < end)
        start, _ = self.ranges[chunk]
        local = index-start
        count, raw = self.chunks[chunk]
        if local >= count:
            raise ValueError(f'String index {index} outside pool')
        offset = 4+count*4+struct.unpack_from('<I', raw, 4+local*4)[0]
        size = struct.unpack_from('<I', raw, offset)[0]
        if offset+4+size > len(raw):
            raise ValueError(f'String {index} extends beyond pool')
        return raw[offset+4:offset+4+size].decode('utf-8')
