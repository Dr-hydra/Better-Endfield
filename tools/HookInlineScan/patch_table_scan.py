"""Recognize Endfield's direct hot-patch table checks without decompiling a PE.

The 1.6.3 compiler inlines IFix dispatch into three pointer loads: one global
table root, a table within that root, and the method's function/context pair.
Only dispatches with a matching null check, adjacent context slot, and an
indirect call/jump are accepted. Sites whose root cannot be established from
the nearby instructions are left unclassified.
"""
import collections
import re

import capstone
from capstone.x86_const import X86_OP_MEM, X86_OP_REG, X86_REG_RAX, X86_REG_RIP


# mov r64,[rax+disp8/disp32]; test r64,r64; je/jne rel8/rel32
TABLE = re.compile(
    rb'[\x48\x4c]\x8b(?:[\x40\x48\x50\x58\x60\x68\x70\x78].|'
    rb'[\x80\x88\x90\x98\xa0\xa8\xb0\xb8]....)'
    rb'[\x48-\x4f]\x85.'
    rb'(?:[\x74\x75].|\x0f[\x84\x85]....)', re.S)
ROOT = re.compile(rb'\x48\x8b\x05(....)', re.S)


def scan_patch_tables(image):
    cs = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_64)
    cs.detail = True
    site_list = []
    roots = collections.Counter()
    for begin, end in image.code_ranges():
        data = image.read(begin, end - begin)
        for match in TABLE.finditer(data):
            address = begin + match.start()
            instructions = list(cs.disasm(match.group(), address))
            if len(instructions) != 3:
                continue
            load, test, branch = instructions
            dst, src = load.operands
            if dst.type != X86_OP_REG or src.type != X86_OP_MEM or src.mem.base != X86_REG_RAX:
                continue
            if any(op.type != X86_OP_REG or op.reg != dst.reg for op in test.operands):
                continue
            patch_address = branch.address + branch.size if branch.mnemonic == 'je' else branch.operands[0].imm
            if not any(b <= patch_address < e for b, e in image.code_ranges()):
                continue
            patch = list(cs.disasm(image.read(patch_address, 160), patch_address))
            if not patch or patch[0].mnemonic != 'mov' or len(patch[0].operands) != 2:
                continue
            target, slot = patch[0].operands
            if (target.type != X86_OP_REG or slot.type != X86_OP_MEM or
                    slot.mem.base != dst.reg or slot.mem.index or slot.mem.disp < 0 or slot.mem.disp % 16):
                continue
            context_seen, dispatch_seen = False, False
            for ins in patch[1:]:
                if ins.mnemonic == 'mov' and len(ins.operands) == 2:
                    op = ins.operands[1]
                    if op.type == X86_OP_MEM and op.mem.base == dst.reg and op.mem.disp == slot.mem.disp + 8:
                        context_seen = True
                if ins.mnemonic in ('call', 'jmp') and ins.operands[0].type == X86_OP_REG:
                    dispatch_seen = context_seen and ins.operands[0].reg == target.reg
                    break
                if ins.mnemonic in ('ret', 'int3'):
                    break
            if not dispatch_seen:
                continue
            # The preceding root load must be in the same unwind owner and
            # resolve to a data section. Avoid assigning a root from a nearby
            # unrelated function or accepting an arbitrary object vtable.
            window_start = max(0, match.start() - 80)
            candidates = list(ROOT.finditer(data[window_start:match.start()]))
            root = None
            for candidate in reversed(candidates):
                root_site = begin + window_start + candidate.start()
                if image.owner(root_site) != image.owner(address):
                    continue
                root_rva = root_site + 7 + int.from_bytes(candidate.group(1), 'little', signed=True)
                if any(b <= root_rva < e and not flags & 0x20000000 for b, e, _, flags in image.sections):
                    root = root_rva
                    break
            if root is None:
                continue
            roots[root] += 1
            site_list.append((address, f'patch-table@0x{root:X}+0x{src.mem.disp:X}', slot.mem.disp))
    # A real hot-patch root is shared by many methods. This also excludes
    # unrelated object/delegate patterns with a coincidentally similar shape.
    common = {f'patch-table@0x{r:X}+' for r, count in roots.items() if count >= 8}
    return sorted(s for s in site_list if any(s[1].startswith(root) for root in common))
