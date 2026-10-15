"""Targeted Android IL2CPP metadata and ARM64 call inspection; no full dump."""
from __future__ import annotations

import argparse
import bisect
import json
import struct
import sys
from pathlib import Path

import capstone


class Elf64:
    def __init__(self, path: Path):
        self.data = bytearray(path.read_bytes())
        if self.data[:6] != b'\x7fELF\x02\x01':
            raise ValueError('Expected little-endian ELF64')
        h = struct.unpack_from('<16sHHIQQQIHHHHHH', self.data)
        if h[2] != 183:
            raise ValueError('Expected AArch64')
        self.base = 0
        self.segments = []
        for i in range(h[10]):
            kind, flags, offset, va, _, size, memory_size, _ = struct.unpack_from('<II6Q', self.data, h[5] + i*h[9])
            if kind == 1:
                self.segments.append((va, size, offset, flags))
        self.segments.sort()
        self.starts = [row[0] for row in self.segments]
        rows = [struct.unpack_from('<II4QII2Q', self.data, h[6]+i*h[11]) for i in range(h[12])]
        names_row = rows[h[13]]
        names = self.data[names_row[4]:names_row[4]+names_row[5]]
        self.sections = {}
        for row in rows:
            name = bytes(names[row[0]:]).split(b'\0', 1)[0].decode()
            self.sections[name] = row
        self.relocations = 0
        for row in rows:
            if row[1] != 4:
                continue
            for pos in range(row[4], row[4]+row[5], 24):
                va, info, addend = struct.unpack_from('<QQq', self.data, pos)
                if info & 0xffffffff == 1027:  # R_AARCH64_RELATIVE, load bias zero.
                    struct.pack_into('<Q', self.data, self.offset(va), addend)
                    self.relocations += 1

    def offset(self, va):
        index = bisect.bisect_right(self.starts, va)-1
        if index >= 0:
            start, size, raw, _ = self.segments[index]
            if start <= va < start+size:
                return raw+va-start
        raise ValueError(f'Unmapped ELF address {va:#x}')

    def va(self, offset):
        for start, size, raw, _ in self.segments:
            if raw <= offset < raw+size:
                return start+offset-raw
        raise ValueError(f'Unmapped ELF file offset {offset:#x}')

    def unpack(self, fmt, va):
        return struct.unpack_from(fmt, self.data, self.offset(va))

    def references(self, va):
        marker = struct.pack('<Q', va)
        start = 0
        while True:
            pos = self.data.find(marker, start)
            if pos < 0:
                return
            start = pos+1
            if pos % 8 == 0:
                yield pos


def decode_metadata(source: Path, output: Path):
    data = bytearray(source.read_bytes())
    if struct.unpack_from('<II', data) == (0xfab11baf, 29):
        return source
    first = struct.unpack_from('<I', data, 8)[0] ^ 0x66666666
    if first != 0x110:
        raise ValueError(f'Unexpected 1.6.4 Android metadata header size {first:#x}')
    # Android's metadata masking follows file ranges, not table boundaries:
    # the first 4 KiB and offsets [0x1000, 0x5000) in each 64 KiB block.
    for i in range(0, min(0x1000, len(data))):
        data[i] ^= 0x66
    for block in range(0, len(data), 0x10000):
        for i in range(block+0x1000, min(block+0x5000, len(data))):
            data[i] ^= 0x66
    struct.pack_into('<II', data, 0, 0xfab11baf, 29)
    sections = list(struct.iter_unpack('<II', data[8:first]))
    for offset, size in sections:
        if offset < first or offset+size > len(data):
            raise ValueError('Decoded metadata section is outside the file')
    if sections[19][1] % 92:
        raise ValueError('Unexpected metadata type layout')
    output.write_bytes(data)
    return output


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--input', type=Path, required=True)
    parser.add_argument('--output', type=Path, required=True)
    parser.add_argument('--classes', help='Optional comma-separated targeted short type names')
    parser.add_argument('--metadata-only', action='store_true', help='Export selected contracts without method disassembly')
    args = parser.parse_args()
    repo = Path(__file__).resolve().parents[5]
    sys.path.insert(0, str(repo/'tools/HookInlineScan'))
    from extract_targeted_metadata import Metadata

    class AndroidMetadata(Metadata):
        def module(self, assembly):
            try:
                return super().module(assembly)
            except ValueError as error:
                if 'expected one codegen module, got []' not in str(error):
                    raise
            # This APK masks some module-name bytes. The method count remains
            # intact: require a unique codegen table with the image's complete
            # RID range and sample pointers in executable ELF mappings.
            lo, hi, _ = next(row for row in self.images if row[2] == assembly)
            count = sum(self.types[index][17] for index in range(lo, hi))
            matches = []
            for ref in self.pe.references(count):
                if ref < 8:
                    continue
                name, actual_count, pointers = struct.unpack_from('<3Q', self.pe.data, ref-8)
                try:
                    self.pe.offset(name)
                    self.pe.offset(pointers+(count-1)*8)
                    samples = [self.pe.unpack('<Q', pointers+index*8)[0] for index in (0, 1, count//2, count-1)]
                    if not all(pointer == 0 or any(start <= pointer < start+size and flags & 1
                                                  for start, size, _, flags in self.pe.segments) for pointer in samples):
                        continue
                except (ValueError, struct.error):
                    continue
                matches.append((actual_count, pointers))
            matches = list(set(matches))
            if len(matches) != 1:
                raise ValueError(f'{assembly}: masked-name module candidates: {matches}')
            self.modules[assembly] = matches[0]
            print(json.dumps({'module': assembly, 'resolution': 'unique image method count + executable pointers',
                              'method_count': count, 'pointers': hex(matches[0][1])}), flush=True)
            return matches[0]

    decoded = decode_metadata(args.input/'global-metadata.dat', args.input/'global-metadata.target-readable.dat')
    image = Elf64(args.input/'libil2cpp.so')
    print(json.dumps({'relative_relocations': image.relocations, 'metadata': str(decoded)}), flush=True)
    metadata = AndroidMetadata(decoded, image)
    selected = {'CharPerformHandleBase', 'CharacterSpecialDashBrain', 'CharacterAnimationComponent', 'InterruptCommandData',
                'InterruptType', 'InterruptReason', 'MovementComponent', 'GroundedMoveGait',
                'MovementMode', 'FindFloorResult', 'RaycastHit'}
    if args.classes:
        selected = set(args.classes.split(','))
    methods, classes = metadata.extract(selected, include_ifix=False)
    for klass in classes:
        for field in klass['fields']:
            value = field['offset']
            if value is not None and value >= 0x100000:
                decoded_offset = value ^ 0x2f2f2f2f
                if not 0 <= decoded_offset < 0x100000:
                    raise ValueError(f"Unresolved field-offset masking: {klass['cls']}.{field['name']}")
                field['stored_masked_offset'] = value
                field['offset_mask'] = 'xor 0x2f2f2f2f'
                field['offset'] = decoded_offset
    names = {'_TickMainFlow', '_TryGetCurInterruptCommand', '_DoExit', 'ShouldInterruptSpDash',
             '_TryExit', '_CheckTrackEnd', '_UpdateCommandData', 'ComputeFloorDist', 'FindFloor'}
    chosen = [m for m in methods if m['name'] in names or
              m['short'] == 'FindFloorResult' and m['name'] in {'Clear', 'IsWalkableFloor', 'get_floorNormal'} or
              m['short'] == 'CharacterSpecialDashBrain' and m['name'] in {'Update', '_TryToInterruptSpDashPerform'} or
              m['short'] == 'CharacterAnimationComponent' and m['name'] in {'.cctor', '_DoPlaySpDashPerform'}]
    if args.metadata_only:
        chosen = []
    by_address = {}
    bounds = set()
    for count, pointers in metadata.modules.values():
        bounds.update(image.unpack('<'+'Q'*count, pointers))
    bounds = sorted(x for x in bounds if x)
    for method in methods:
        if method['rva']:
            by_address.setdefault(method['rva'], []).append(method['cls']+'.'+method['name'])
    disassembler = capstone.Cs(capstone.CS_ARCH_ARM64, capstone.CS_MODE_ARM)
    scorer = capstone.Cs(capstone.CS_ARCH_ARM64, capstone.CS_MODE_ARM)
    scorer.skipdata = True
    code_pages = {}

    def code_page(page):
        if page in code_pages:
            return code_pages[page]
        raw = image.offset(page)
        data = bytes(image.data[raw:raw+0x1000])
        decoded = bytes(value ^ 0x2f for value in data)
        def score(blob):
            return sum(i.size for i in scorer.disasm(blob, page) if i.mnemonic != '.byte') / len(blob)
        plain_score, decoded_score = score(data), score(decoded)
        use_mask = decoded_score >= 0.98 and plain_score < 0.9
        code_pages[page] = (decoded if use_mask else data, use_mask)
        return code_pages[page]

    args.output.mkdir(parents=True, exist_ok=True)
    for method in chosen:
        start = method['rva']
        if not start:
            continue
        index = bisect.bisect_right(bounds, start)
        end = bounds[index] if index < len(bounds) else start+0x10000
        end = min(end, start+0x10000)
        method['bounded_end'] = end
        method['direct_calls'] = []
        lines = []
        code = bytearray()
        masked_pages = []
        for page in range(start & ~0xfff, (end+0xfff) & ~0xfff, 0x1000):
            page_data, masked = code_page(page)
            code.extend(page_data[max(start-page, 0):min(end-page, 0x1000)])
            if masked:
                masked_pages.append(hex(page))
        if masked_pages:
            method['local_code_mask'] = {'xor': '0x2f', 'pages': masked_pages,
                                         'scope': 'selected bounded method pages; prologues and calls inspected'}
        instructions = list(disassembler.disasm(code, start))
        if sum(i.size for i in instructions) != len(code):
            raise ValueError(f"Unresolved code masking for {method['name']} at {start:#x}")
        for instruction in instructions:
            if instruction.mnemonic in ('bl', 'b') and instruction.op_str.startswith('#'):
                target = int(instruction.op_str[1:], 0)
                method['direct_calls'].append({'site': instruction.address, 'op': instruction.mnemonic,
                                               'target': target, 'names': by_address.get(target, [])})
            lines.append(f'{instruction.address:016x}  {instruction.mnemonic:8s} {instruction.op_str}')
        path = args.output/(method['short']+'.'+method['name']+f'.{start:x}.asm.txt')
        path.write_text('\n'.join(lines)+'\n', encoding='utf-8')
        method['assembly_file'] = path.name
    result = {'source': {'versionName': '1.6.4', 'versionCode': 61, 'architecture': 'arm64-v8a',
                         'input': str(args.input), 'metadata_type_count': metadata.type_count,
                         'scope': 'selected classes and bounded method bodies; no full dump/database'},
              'methods': methods if args.metadata_only else chosen, 'classes': classes,
              'selected_class_method_index': [{'class': m['cls'], 'name': m['name'], 'rva': m['rva']} for m in methods]}
    (args.output/'targeted-contracts.json').write_text(json.dumps(result, ensure_ascii=False, indent=2)+'\n', encoding='utf-8')
    print(json.dumps({'classes': len(classes), 'methods': [{'class': m['cls'], 'method': m['name'], 'rva': hex(m['rva'])} for m in chosen]}), flush=True)


if __name__ == '__main__':
    main()
