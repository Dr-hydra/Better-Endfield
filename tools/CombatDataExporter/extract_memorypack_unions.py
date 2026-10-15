"""Recover current reachable MemoryPack union tags from formatter constructors.

Only the constructors identified by the supplied schema are disassembled.
Type.GetTypeFromHandle arguments use IL2CPP encoded type references, and each
Dictionary<Type, ushort>.Add supplies its explicit tag. No ordering inference.
"""
from __future__ import annotations

import argparse
import json
import re
import struct
import sys
from pathlib import Path

import capstone
from capstone import x86_const as x86

sys.path.insert(0, str(Path(__file__).resolve().parents[1] / 'HookInlineScan'))
from extract_targeted_metadata import Metadata, Pe


def extract(game: Path, schema: dict, output: Path | None = None) -> dict:
    pe = Pe(game / 'GameAssembly.dll')
    metadata = Metadata(game / 'Endfield_Data/il2cpp_data/Metadata/global-metadata.dat', pe)
    unions = {key: value for key, value in schema.items() if value.get('union_candidates')}
    wrappers = {value['wrapper']: key for key, value in schema.items() if value.get('wrapper')}
    type_rows = {metadata.typename(i): row for i, row in enumerate(metadata.types)}

    def underlying_type(wrapper):
        if wrapper not in wrappers:
            row = type_rows[wrapper]
            for number in range(row[17]):
                method = struct.unpack_from('<5iI4H', metadata.data,
                                            metadata.sections[5][0] + (row[9] + number) * 32)
                if re.fullmatch(r'get_+instance', metadata.string(method[0])):
                    wrappers[wrapper] = metadata.resolve(method[2])
                    break
        if wrapper not in wrappers:
            raise ValueError(f'No instance getter for {wrapper}')
        return wrappers[wrapper]
    selected = {value['wrapper'].rsplit('.', 1)[-1] + 'Formatter' for value in unions.values()}
    methods, _ = metadata.extract(selected | {'Type'}, include_ifix=False)
    constructors = {value['short']: value for value in methods if value['name'] == '.cctor'}
    get_type = next(value['rva'] for value in methods if value['cls'] == 'System.Type'
                    and value['name'] == 'GetTypeFromHandle')
    # Large constructors contain multiple Windows unwind ranges. Bound them
    # by the next managed method, not the first .pdata fragment.
    count, pointer_table = metadata.module('MemoryPack.Beyond.dll')
    method_starts = sorted({pe.unpack('<Q', pointer_table + index * 8)[0] - pe.base
                            for index in range(count) if pe.unpack('<Q', pointer_table + index * 8)[0]})
    disassembler = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_64)
    disassembler.detail = True

    def register(instruction, number):
        name = instruction.reg_name(number)
        return {'ecx': 'rcx', 'edx': 'rdx', 'eax': 'rax', 'ebx': 'rbx',
                'r8d': 'r8', 'r9d': 'r9', 'edi': 'rdi', 'esi': 'rsi'}.get(name, name)

    result = {}
    known_add_targets = set()
    add_reference = None
    for name, union in unions.items():
        method = constructors[union['wrapper'].rsplit('.', 1)[-1] + 'Formatter']
        start = method['rva']
        end = next(address for address in method_starts if address > start)
        registers = {}
        tags = {}
        add_targets = set()
        expected_capacity = None
        pending_type = None
        for instruction in disassembler.disasm(pe.data[pe.offset(start):pe.offset(end - 1) + 1], start):
            operands = instruction.operands
            if instruction.mnemonic == 'ret' or (instruction.mnemonic == 'jmp' and operands[0].type == capstone.CS_OP_IMM
                                                   and not start <= operands[0].imm < end):
                break
            if instruction.mnemonic == 'mov' and len(operands) == 2 and operands[0].type == capstone.CS_OP_REG:
                destination = register(instruction, operands[0].reg)
                source = operands[1]
                value = None
                if source.type == capstone.CS_OP_IMM:
                    value = source.imm
                    if destination == 'rdx' and expected_capacity is None and value > 0:
                        expected_capacity = value
                elif source.type == capstone.CS_OP_REG:
                    value = registers.get(register(instruction, source.reg))
                elif source.type == capstone.CS_OP_MEM and source.mem.base == x86.X86_REG_RIP:
                    location = instruction.address + instruction.size + source.mem.disp
                    try:
                        encoded = pe.unpack('<Q', location)[0]
                    except ValueError:
                        encoded = 0  # Zero-initialized .bss has no raw PE bytes.
                    if encoded >> 29 == 2:
                        value = ('handle', metadata.resolve((encoded & 0x1fffffff) >> 1))
                        pending_type = value[1]
                    elif encoded >> 29 == 6:
                        value = ('method', encoded)
                registers[destination] = value
            elif instruction.mnemonic == 'xor' and len(operands) == 2 and operands[0].type == capstone.CS_OP_REG and operands[1].type == capstone.CS_OP_REG and operands[0].reg == operands[1].reg:
                registers[register(instruction, operands[0].reg)] = 0
            elif instruction.mnemonic == 'call' and operands[0].type == capstone.CS_OP_IMM:
                target = operands[0].imm
                if target == 0x15e10:  # Current codegen object allocator.
                    registers['rax'] = ('allocated',)
                elif target == get_type:
                    handle = registers.get('rcx')
                    if not isinstance(handle, tuple) or handle[0] != 'handle':
                        raise ValueError(f'{name}: unresolved type handle at {instruction.address:#x}')
                    registers['rax'] = ('type', handle[1])
                else:
                    typ, tag = registers.get('rdx'), registers.get('r8')
                    if registers.get('rcx') == ('allocated',) and isinstance(typ, int):
                        expected_capacity = typ
                    direct_type = isinstance(typ, tuple) and typ[0] == 'type'
                    if isinstance(tag, int) and (direct_type or target in known_add_targets):
                        if direct_type:
                            known_add_targets.add(target)
                            if pending_type != typ[1]:
                                raise ValueError(f'{name}: type-handle flow mismatch')
                            add_reference = registers.get('r9') if add_reference is None else add_reference
                        if registers.get('r9') != add_reference or pending_type is None:
                            raise ValueError(f'{name}: unexpected dictionary call at {instruction.address:#x}')
                        underlying = underlying_type(pending_type)
                        if tag in tags:
                            raise ValueError(f'{name}: unexpected union entry {tag}: {pending_type}')
                        tags[tag] = underlying
                        add_targets.add(target)
                        registers['rdx'] = None
                        pending_type = None
                    registers['rax'] = None
        if (expected_capacity is not None and len(tags) != expected_capacity) or not add_targets:
            raise ValueError(f'{name}: recovered {len(tags)}/{expected_capacity} tags; Add targets={add_targets}; '
                             f'missing={sorted(set(range(expected_capacity or 0))-set(tags))}')
        result[name] = {'constructorRva': start, 'addMethodRvas': sorted(add_targets),
                        'tags': {str(tag): value for tag, value in sorted(tags.items())}}
        if output:
            output.parent.mkdir(parents=True, exist_ok=True)
            output.write_text(json.dumps(result, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
    return result


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--game', required=True, type=Path)
    parser.add_argument('--schema', required=True, type=Path)
    parser.add_argument('--output', required=True, type=Path)
    args = parser.parse_args()
    result = extract(args.game, json.loads(args.schema.read_text(encoding='utf-8-sig')), args.output)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(result, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
    print(json.dumps({key: len(value['tags']) for key, value in result.items()}))
