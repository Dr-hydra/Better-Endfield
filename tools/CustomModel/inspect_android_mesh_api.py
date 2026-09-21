"""Locate Mesh/MeshData icall-name references in an Android Unity ELF.

This is an evidence collector only. It does not call or hook any native entry.
It reports the ARM64 function containing each name reference and nearby calls;
the result is not an ABI declaration.
"""
from __future__ import annotations
import argparse
import bisect
import json
import struct
from pathlib import Path


class Elf:
    def __init__(self, data: bytes):
        self.data = data
        if len(data) < 64 or data[:6] != b"\x7fELF\x02\x01":
            raise ValueError("expected ELF64")
        if struct.unpack_from("<H", data, 18)[0] != 183:
            raise ValueError("expected AArch64")
        phoff = struct.unpack_from("<Q", data, 32)[0]
        phentsz = struct.unpack_from("<H", data, 54)[0]
        phnum = struct.unpack_from("<H", data, 56)[0]
        if phentsz != 56 or not 0 < phnum <= 1024 or phoff + phentsz * phnum > len(data):
            raise ValueError("invalid program header table")
        self.segments = []
        self.functions = []
        for n in range(phnum):
            off = phoff + n * phentsz
            kind, flags, pos, va, _, filesz, memsz, _ = struct.unpack_from("<IIQQQQQQ", data, off)
            if pos + filesz > len(data) or filesz > memsz:
                raise ValueError("invalid segment bounds")
            if kind == 1:
                self.segments.append((pos, va, filesz, memsz, flags))
            if kind == 0x6474E550:
                self.unwind = (pos, va, filesz)
        unwind = getattr(self, "unwind", None)
        if unwind:
            pos, va, size = unwind
            if size >= 12 and data[pos:pos + 4] == b"\x01\x1b\x03\x3b":
                count = struct.unpack_from("<I", data, pos + 8)[0]
                if count > (size - 12) // 8:
                    raise ValueError("truncated unwind table")
                for n in range(count):
                    self.functions.append(va + struct.unpack_from("<i", data, pos + 12 + n * 8)[0])
        self.functions.sort()

    def va_to_file(self, va):
        for pos, base, filesz, memsz, _ in self.segments:
            if base <= va < base + filesz:
                return pos + va - base
        return None

    def executable(self):
        return [(pos, va, filesz) for pos, va, filesz, _, flags in self.segments if flags & 1]


def signed(value, bits):
    return value - (1 << bits) if value & (1 << (bits - 1)) else value


def adrp_target(word, pc):
    if word & 0x9F000000 != 0x90000000:
        return None
    imm = signed(((word >> 29) & 3) | (((word >> 5) & 0x7FFFF) << 2), 21)
    return (pc & ~0xFFF) + (imm << 12)


def add_target(word, base):
    if word & 0xFFC00000 != 0x91000000:
        return None
    if ((word >> 5) & 31) != (word & 31):
        return None
    return base + ((word >> 10) & 0xFFF)


def decode_name(data, offset, limit):
    # Infer a candidate single-byte XOR from the known prefix. This is offline
    # evidence, not proof that the runtime has decrypted/registered the name.
    prefix = b"UnityEngine.Mesh"
    if not 0 <= offset < min(limit, len(data)):
        return None
    key = data[offset] ^ prefix[0]
    raw = data[offset:min(offset + 256, limit)]
    decoded = bytes(v ^ key for v in raw)
    end = decoded.find(b"\0")
    if end < 0 or not decoded.startswith((prefix + b"::", prefix + b"/")):
        return None
    name = decoded[:end]
    if not all(32 <= v <= 126 for v in name):
        return None
    return name.decode("ascii"), key


def instruction_window(elf, entry, maximum=256):
    """Bounded disassembly, stopping at the next unwind function. No recursion."""
    from capstone import Cs, CS_ARCH_ARM64, CS_MODE_ARM
    for pos, base, size in elf.executable():
        if not base <= entry < base + size:
            continue
        end = min(entry + maximum, base + size)
        next_index = bisect.bisect_right(elf.functions, entry)
        if next_index < len(elf.functions):
            end = min(end, elf.functions[next_index])
        offset = pos + entry - base
        instructions, edges = [], []
        for ins in Cs(CS_ARCH_ARM64, CS_MODE_ARM).disasm(elf.data[offset:offset + end - entry], entry):
            instructions.append(f"{ins.address:x}: {ins.mnemonic} {ins.op_str}")
            word = int.from_bytes(ins.bytes, "little")
            if word & 0x7c000000 == 0x14000000:
                target = ins.address + signed(word & 0x03ffffff, 26) * 4
                edges.append({"at": ins.address, "target": target, "kind": ins.mnemonic})
            if ins.mnemonic in ("ret", "b", "br"):
                break
        return {"instructions": instructions, "direct_edges": edges,
                "complete_function": False}
    return {"instructions": [], "direct_edges": [], "complete_function": False}


def registration(words, pc):
    # Exact five-instruction stub; never borrow x1 from the next function.
    a, b, c, d, e = words
    name_page = adrp_target(a, pc)
    entry_page = adrp_target(b, pc + 4)
    if (name_page is None or entry_page is None or a & 31 != 0 or b & 31 != 1
            or c & 0xffc003ff != 0x91000000
            or d & 0xffc003ff != 0x91000021
            or e & 0xfc000000 != 0x14000000):
        return None
    return (add_target(c, name_page), add_target(d, entry_page),
            pc + 16 + signed(e & 0x03ffffff, 26) * 4)


def inspect(data, disassemble=False):
    elf = Elf(data)
    refs = []
    for pos, va, size in elf.executable():
        for at in range(pos, pos + size - 19, 4):
            first = struct.unpack_from("<I", data, at)[0]
            if first & 0x9f00001f != 0x90000000:
                continue
            pc = va + at - pos
            match = registration(struct.unpack_from("<5I", data, at), pc)
            if match is None:
                continue
            name_va, entry, registrar = match
            if not any(base <= entry < base + length for _, base, length in elf.executable()):
                continue
            if not any(base <= registrar < base + length for _, base, length in elf.executable()):
                continue
            for segment_pos, base, length, _, flags in elf.segments:
                if flags & 4 and base <= name_va < base + length:
                    decoded = decode_name(data, segment_pos + name_va - base, segment_pos + length)
                    if decoded:
                        name, key = decoded
                        refs.append({"name": name, "name_va": name_va,
                                     "name_xor_key": key, "registration_va": pc,
                                     "native_entry_va": entry, "registrar_va": registrar})
                    break
    if disassemble:
        for ref in refs:
            ref["entry_window"] = instruction_window(elf, ref["native_entry_va"])
    return {"architecture": "aarch64", "references": refs,
            "target_names": sorted({ref["name"] for ref in refs}),
            "abi_verified": False,
            "note": "Exact registration stubs; encoded-name decoding is an offline hypothesis. No calls made."}


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("elf", type=Path)
    parser.add_argument("--output", type=Path, required=True)
    parser.add_argument("--disassemble", action="store_true")
    args = parser.parse_args()
    report = inspect(args.elf.read_bytes(), args.disassemble)
    args.output.write_text(json.dumps(report, indent=2), encoding="utf-8")
    print(json.dumps({"names": len(report["target_names"]), "references": len(report["references"])}, indent=2))
