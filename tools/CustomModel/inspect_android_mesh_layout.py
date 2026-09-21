"""Read-only ELF/AArch64 evidence collector for the Android Mesh port.

Requires capstone. Accepts a raw libunity.so, never a process memory dump.
Reports candidates, NOT a writable layout: local instruction matches are not
proof of whole-function register provenance or independent archive directions.
"""
from __future__ import annotations

import argparse
import bisect
import json
from pathlib import Path
import struct


class Elf:
    def __init__(self, data: bytes):
        self.data = data
        if len(data) < 64 or data[:6] != b'\x7fELF\x02\x01':
            raise ValueError('Expected little-endian ELF64')
        if self.read('<H', 18)[0] != 183:
            raise ValueError('Expected AArch64 ELF')
        offset = self.read('<Q', 32)[0]
        size, count = self.read('<HH', 54)
        if size != 56 or not 0 < count <= 1024:
            raise ValueError('Unsupported program header table')
        self.segments = []
        self.unwind = None
        for index in range(count):
            kind, flags, pos, address, _, filesz, memsz, _ = self.read('<IIQQQQQQ', offset + size * index)
            if pos > len(data) or filesz > len(data) - pos or filesz > memsz:
                raise ValueError('Invalid segment bounds')
            if kind == 1:
                self.segments.append((pos, address, filesz, flags))
            if kind == 0x6474e550:
                self.unwind = (pos, address, filesz)

    def read(self, fmt, offset):
        if offset < 0 or offset + struct.calcsize(fmt) > len(self.data):
            raise ValueError('Truncated ELF')
        return struct.unpack_from(fmt, self.data, offset)

    def functions(self):
        if not self.unwind:
            return []
        pos, address, size = self.unwind
        # v1, pcrel+sdata4 eh_frame pointer, udata4 count, datarel+sdata4 table.
        if size < 12 or self.data[pos:pos + 4] != b'\x01\x1b\x03\x3b':
            return []
        count = self.read('<I', pos + 8)[0]
        if count > (size - 12) // 8:
            raise ValueError('Truncated unwind index')
        starts = [address + self.read('<i', pos + 12 + 8 * n)[0] for n in range(count)]
        if any(a >= b for a, b in zip(starts, starts[1:])):
            raise ValueError('Unsorted unwind index')
        return starts


def signed(value, bits):
    return value - (1 << bits) if value & (1 << (bits - 1)) else value


def inspect(data: bytes):
    from capstone import Cs, CS_ARCH_ARM64, CS_MODE_ARM
    elf = Elf(data)
    names = set()
    needle = b'm_BonesPerVertex\0'
    for pos, address, size, flags in elf.segments:
        if flags & 1 or not flags & 4:
            continue
        cursor = pos
        while (cursor := data.find(needle, cursor, pos + size)) >= 0:
            if cursor == pos or data[cursor - 1] == 0:
                names.add(address + cursor - pos)
            cursor += 1
    functions = elf.functions()
    decoder = Cs(CS_ARCH_ARM64, CS_MODE_ARM)
    references = []
    seen = set()
    for pos, address, size, flags in elf.segments:
        if not flags & 1:
            continue
        end = pos + size
        for offset in range(pos, end - 3, 4):
            word = elf.read('<I', offset)[0]
            opcode = word & 0x9f000000
            if opcode not in (0x90000000, 0x10000000):
                continue
            pc = address + offset - pos
            imm = signed(((word >> 29) & 3) | (((word >> 5) & 0x7ffff) << 2), 21)
            rd = word & 31
            matches = []
            if opcode == 0x10000000:
                if pc + imm in names:
                    matches.append((offset, rd))
            else:
                page = (pc & ~4095) + (imm << 12)
                if not any(page <= name < page + 4096 for name in names):
                    continue
                for after in range(offset + 4, min(offset + 52, end - 3), 4):
                    following = elf.read('<I', after)[0]
                    if (following & 0xffc00000 == 0x91000000 and
                            (following >> 5) & 31 == rd and
                            page + ((following >> 10) & 4095) in names):
                        matches.append((after, following & 31))
            for name_offset, name_register in matches:
                name_pc = address + name_offset - pos
                if name_pc in seen:
                    continue
                seen.add(name_pc)
                function_index = bisect.bisect_right(functions, name_pc) - 1
                function = functions[function_index] if function_index >= 0 else None
                start = max(pos, offset - 32)
                stop = min(end, name_offset + 32)
                if function is not None:
                    start = max(start, pos + function - address)
                    if function_index + 1 < len(functions):
                        stop = min(stop, pos + functions[function_index + 1] - address)
                # Known archive argument convention: x1=name, x3=field address.
                # This is a local candidate only; upstream writes/branches need review.
                candidates = []
                if name_register == 1:
                    for at in range(start, stop - 3, 4):
                        insn = elf.read('<I', at)[0]
                        field = (insn >> 10) & 4095
                        base = (insn >> 5) & 31
                        if (insn & 0xffc0001f == 0x91000003 and
                                19 <= base <= 28 and 0 < field <= 4096 and field % 4 == 0):
                            candidates.append({'offset': field, 'mesh_register': f'x{base}',
                                               'instruction_va': address + at - pos})
                    # Runtime reader forms Mesh+offset after the named lookup,
                    # then passes it to an archive-supplied callback.
                    reader = list(decoder.disasm(data[name_offset:min(name_offset+64,end)],name_pc))
                    if len(reader)>=15:
                        i=reader
                        import re
                        member=re.fullmatch(r'(x(?:19|2[0-8])), \1, #(0x[0-9a-f]+|[0-9]+)',i[8].op_str)
                        if (i[5].mnemonic=='bl' and i[6].mnemonic=='cbz' and
                            i[7].mnemonic=='cmp' and i[7].op_str=='w0, #1' and
                            i[8].mnemonic=='add' and member and i[9].mnemonic=='b.ge' and
                            i[10].mnemonic=='ldr' and i[10].op_str.startswith('x8, [sp,') and
                            i[12].mnemonic=='mov' and i[12].op_str=='x0, '+member[1] and
                            i[14].mnemonic=='blr' and i[14].op_str=='x8'):
                            candidates.append({'offset':int(member[2],0),'mesh_register':member[1],
                                'instruction_va':i[8].address,'path':'archive_callback_reader'})
                            stop=min(end,name_offset+64)
                references.append({'name_reference_va': name_pc, 'function_va': function,
                    'name_register': f'x{name_register}', 'candidates': candidates,
                    'instructions': [f'{i.address:x}: {i.mnemonic} {i.op_str}'
                                     for i in decoder.disasm(data[start:stop], address + start - pos)]})
    offsets = {candidate['offset'] for reference in references for candidate in reference['candidates']}
    field_offset = next(iter(offsets)) if len(offsets) == 1 else None
    read_functions, write_functions = set(), set()
    raw_accesses = []
    offsets_by_function = {}
    if field_offset is not None:
        decoder.detail = True
        decoder.skipdata = True
        for pos, address, size, flags in elf.segments:
            if not flags & 1:
                continue
            for insn in decoder.disasm(data[pos:pos + size], address):
                if insn.id == 0:
                    continue
                for operand in insn.operands:
                    if operand.type != 3 or operand.mem.base == 31:
                        continue
                    if 'sp' in insn.op_str or insn.mnemonic not in ('ldr', 'ldrsw', 'str'):
                        continue
                    function_index = bisect.bisect_right(functions, insn.address) - 1
                    function = functions[function_index] if function_index >= 0 else None
                    if function is None:
                        continue
                    offsets_by_function.setdefault(function, set()).add(operand.mem.disp)
                    kind = 'read' if insn.mnemonic.startswith('ldr') else 'write'
                    raw_accesses.append({'instruction_va': insn.address, 'function_va': function,
                                         'offset': operand.mem.disp, 'kind': kind,
                                         'value_register': insn.operands[0].reg if insn.operands else None,
                                         'base_register': operand.mem.base,
                                         'text': f'{insn.mnemonic} {insn.op_str}'})
        mesh_lanes = {0x68, 0x84, 0xe8, 0x108, 0x118, 0x128, 0x148,
                      0x158, 0x168, 0x178, 0x190, 0x198, 0x1a0, 0x1b8,
                      0x1bc, 0x1c4, 0x1c8, 0x1d0, 0x1d8, 0x1f0, 0x208, 0x218}
        mesh_like = {function for function, offsets_for_function in offsets_by_function.items()
                     if len(offsets_for_function & mesh_lanes) >= 3}
        accesses = [access for access in raw_accesses
                    if access['function_va'] in mesh_like and access['offset'] == field_offset]
        for access in accesses:
            (read_functions if access['kind'] == 'read' else write_functions).add(access['function_va'])
        copy_functions = set()
        accesses_by_function = {}
        for access in raw_accesses:
            if access['function_va'] in mesh_like:
                accesses_by_function.setdefault(access['function_va'], []).append(access)
        for function, function_accesses in accesses_by_function.items():
            for index, source in enumerate(function_accesses):
                if source['kind'] != 'read' or source['offset'] != 0x14:
                    continue
                for target in function_accesses[index + 1:index + 9]:
                    if (target['kind'] == 'write' and target['offset'] == field_offset and
                            target['value_register'] == source['value_register'] and
                            target['base_register'] != source['base_register']):
                        copy_functions.add(function)
                        break
                if function in copy_functions:
                    break
    else:
        mesh_like = set()
        accesses = []
        copy_functions = set()
    independent = set(read_functions) | set(write_functions)
    return {'architecture': 'aarch64', 'field': 'm_BonesPerVertex',
            'name_vas': sorted(names), 'references': references,
            'field_offset': field_offset,
            'field_accesses': accesses,
            'raw_memory_access_count': len(raw_accesses),
            'mesh_like_access_functions': sorted(mesh_like),
            'archive_functions': sorted({r['function_va'] for r in references if r['function_va'] is not None}),
            'copy_functions': sorted(copy_functions),
            'field_read_functions': sorted(read_functions),
            'field_write_functions': sorted(write_functions),
            'distinct_field_access_functions': len(independent),
            'writable_layout_confirmed': False,
            'reason': 'Evidence collection only; function-level consumers are reported but archive direction and setter provenance remain unproven.'}


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('elf', type=Path)
    parser.add_argument('--output', type=Path)
    args = parser.parse_args()
    try:
        report = json.dumps(inspect(args.elf.read_bytes()), ensure_ascii=False, indent=2)
        if args.output:
            args.output.write_text(report + '\n', encoding='utf-8')
        else:
            print(report)
    except (OSError, ValueError) as error:
        parser.exit(1, f'{error}\n')
