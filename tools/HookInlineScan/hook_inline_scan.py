"""Find hook targets the game no longer calls through their entry.

Name resolution proves a method exists; it does not prove the game calls that
copy. MSVC inlines small or single-caller methods into their callers (the PC
InitMainPathHash regression), and a hook on the original entry never sees
those calls. This scans GameAssembly.dll for each hooked method:

* direct rel32 call/jmp sites to its entry;
* a tail jump out of its body (thin wrapper);
* inlined copies, found through IFix: every instrumented method checks its
  own id on entry, either `mov ecx, id; call IsPatched/GetPatch` or, with
  IsPatched inlined, `cmp dword [reg+0x18], id; jg cold` whose cold path calls
  GetPatch(id). IsPatched/GetPatch exist once per assembly, so (assembly, id)
  names one method, and every other function holding that check holds a copy.
  A copy is confirmed when it also calls most of the method's own callees.

Inputs: the game's GameAssembly.dll, an IL2CPP dump of the same build
(IL2CPP_Dump_AI and IL2CPP_Dump_Normal), and the hooked methods, either as a
descriptor list (hooked_methods.json) or straight from a BetterEndfieldNext.log
written by a Host with hook diagnostics ("Hook installed ... at
GameAssembly.dll+0x...").
"""
import argparse
import bisect
import collections
import json
import mmap
import os
import pickle
import re
import struct
import sys

try:
    import capstone
    import numpy as np
    import pefile
except ImportError as error:
    sys.exit(f"{error}. Install requirements: python -m pip install -r requirements.txt")

HERE = os.path.dirname(os.path.abspath(__file__))
HELPER_CALLERS = 3000  # Callees with more call sites are runtime helpers.


# --------------------------------------------------------------------------- PE

class Image:
    def __init__(self, path):
        self.pe = pefile.PE(path, fast_load=True)
        self.pe.parse_data_directories(directories=[pefile.DIRECTORY_ENTRY['IMAGE_DIRECTORY_ENTRY_EXCEPTION']])
        self.file = open(path, 'rb')
        self.map = mmap.mmap(self.file.fileno(), 0, access=mmap.ACCESS_READ)
        self.sections = [(s.VirtualAddress, s.VirtualAddress + max(s.Misc_VirtualSize, s.SizeOfRawData),
                          s.PointerToRawData, s.Characteristics) for s in self.pe.sections]
        pdata = self.pe.OPTIONAL_HEADER.DATA_DIRECTORY[3]
        raw = self.read(pdata.VirtualAddress, pdata.Size)
        self.funcs = sorted(struct.unpack_from('<III', raw, i) for i in range(0, len(raw) - 11, 12))
        self.begins = [f[0] for f in self.funcs]
        self.cs = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_64)
        self.key = f'{self.pe.FILE_HEADER.TimeDateStamp:08x}-{self.pe.OPTIONAL_HEADER.SizeOfImage:x}'

    def read(self, rva, size):
        for begin, end, raw, _ in self.sections:
            if begin <= rva < end:
                offset = raw + rva - begin
                return self.map[offset:offset + size]
        raise ValueError(f'RVA 0x{rva:X} is outside the image')

    def code_ranges(self):
        return [(begin, end) for begin, end, _, flags in self.sections if flags & 0x20000000]

    def func_at(self, rva):
        index = bisect.bisect_right(self.begins, rva) - 1
        if index >= 0 and self.funcs[index][0] <= rva < self.funcs[index][1]:
            return self.funcs[index]
        return None

    def chained(self, entry):
        return bool((self.read(entry[2], 1)[0] >> 3) & 4)

    def primary(self, entry):
        """Follow chained (cold-split) unwind info back to the function entry."""
        for _ in range(8):
            info = self.read(entry[2], 4)
            if not (info[0] >> 3) & 4:
                return entry
            entry = struct.unpack('<III', self.read(entry[2] + 4 + 2 * ((info[2] + 1) & ~1), 12))
        return entry

    def owner(self, rva):
        entry = self.func_at(rva)
        return self.primary(entry)[0] if entry else None

    def disasm(self, rva, size):
        return list(self.cs.disasm(self.read(rva, size), rva))


# ------------------------------------------------------------------------- dump

method_re = re.compile(r'^\s+RVA=0x([0-9A-F]+)\s+token=0x([0-9A-F]+)\s+(.*)$')
property_re = re.compile(r'^\s+(\S+)\s+get=0x([0-9A-F]+)(?:\s+set=0x([0-9A-F]+))?|^\s+(\S+)\s+set=0x([0-9A-F]+)')
normal_rva_re = re.compile(r'^\s*// RVA: 0x([0-9A-F]+)\s+token: 0x([0-9A-F]+)')
MODIFIERS = ('virtual', 'override', 'abstract', 'static', 'extern', 'sealed')


def split_parameters(text):
    out, depth, current = [], 0, ''
    for ch in text:
        depth += ch in '<[' and 1 or 0
        depth -= ch in '>]' and 1 or 0
        if ch == ',' and depth == 0:
            out.append(current.strip())
            current = ''
        else:
            current += ch
    if current.strip():
        out.append(current.strip())
    return out


def parse_signature(signature):
    head, _, rest = signature.partition('(')
    returns, _, name = head.strip().rpartition(' ')
    types = [p.rsplit(' ', 1)[0] for p in split_parameters(rest.rsplit(')', 1)[0])]
    return returns, name, types


def load_dump(root):
    """Methods (with property accessors) and modifiers from both dump flavours."""
    methods = []
    ai = os.path.join(root, 'IL2CPP_Dump_AI')
    for file_name in sorted(os.listdir(ai)):
        assembly, cls, section = file_name[:-3], None, None
        with open(os.path.join(ai, file_name), encoding='utf-8', errors='replace') as stream:
            for line in stream:
                if line.startswith('CLASS: '):
                    cls, section = line[7:].strip(), None
                    continue
                if line.rstrip().endswith(':') and not line.startswith(' '):
                    section = line.strip()
                    continue
                if not cls:
                    continue
                namespace, _, short = cls.rpartition('.')
                base = dict(asm=assembly, cls=cls, ns=namespace, short=short)
                if section == 'PROPERTIES:':
                    match = property_re.match(line)
                    if match:
                        name = match.group(1) or match.group(4)
                        for kind, value, count in (('get', match.group(2), 0), ('set', match.group(3) or match.group(5), 1)):
                            if value:
                                methods.append(dict(base, name=f'{kind}_{name}', ret=None, types=[None] * count,
                                                    rva=int(value, 16), token=None))
                    continue
                match = method_re.match(line)
                if match:
                    returns, name, types = parse_signature(match.group(3))
                    methods.append(dict(base, name=name, ret=returns, types=types,
                                        rva=int(match.group(1), 16), token=int(match.group(2), 16)))
    modifiers = {}
    normal = os.path.join(root, 'IL2CPP_Dump_Normal')
    if os.path.isdir(normal):
        for file_name in os.listdir(normal):
            assembly, token = file_name[:-3], None
            with open(os.path.join(normal, file_name), encoding='utf-8', errors='replace') as stream:
                for line in stream:
                    match = normal_rva_re.match(line)
                    if match:
                        token = int(match.group(2), 16)
                    elif token is not None:
                        words = line.split('(')[0].split()
                        modifiers[(assembly, token)] = sorted(w for w in words if w in MODIFIERS)
                        token = None
    return methods, modifiers


# ---------------------------------------------------------------------- indexes

class Index:
    def __init__(self, image, methods, cache):
        self.image = image
        os.makedirs(cache, exist_ok=True)
        self.names = collections.defaultdict(list)
        self.ifix = {}
        for m in methods:
            if m['rva']:
                self.names[m['rva']].append(f"{m['cls']}::{m['name']}")
            if m['cls'] == 'IFix.WrappersManagerImpl' and m['name'] in ('IsPatched', 'GetPatch') and m['rva']:
                self.ifix[m['rva']] = m['asm']
        self.src, self.dst, self.op = self._edges(os.path.join(cache, 'edges.npz'))
        self.fragment_map = self._fragments(os.path.join(cache, 'fragments.pkl'))
        self.sites, self.site_list = self._ifix_sites(os.path.join(cache, 'ifix_sites.pkl'))
        self.site_addresses = [s[0] for s in self.site_list]

    def name(self, rva):
        names = self.names.get(rva)
        return ' / '.join(names[:2]) if names else f'sub_{rva:X}'

    def _edges(self, path):
        if os.path.exists(path):
            z = np.load(path)
            return z['src'], z['dst'], z['op']
        sources, targets, opcodes = [], [], []
        for begin, end in self.image.code_ranges():
            raw = np.frombuffer(self.image.read(begin, end - begin), dtype=np.uint8)
            index = np.nonzero((raw[:-5] == 0xE8) | (raw[:-5] == 0xE9))[0]
            rel = (raw[index + 1].astype(np.int64) | (raw[index + 2].astype(np.int64) << 8) |
                   (raw[index + 3].astype(np.int64) << 16) | (raw[index + 4].astype(np.int64) << 24))
            rel = np.where(rel >= 1 << 31, rel - (1 << 32), rel)
            sources.append(begin + index)
            targets.append(begin + index + 5 + rel)
            opcodes.append(raw[index])
        src, dst, op = np.concatenate(sources), np.concatenate(targets), np.concatenate(opcodes)
        order = np.argsort(dst, kind='stable')
        np.savez(path, src=src[order], dst=dst[order], op=op[order])
        return src[order], dst[order], op[order]

    def _fragments(self, path):
        if os.path.exists(path):
            return pickle.load(open(path, 'rb'))
        fragments = collections.defaultdict(list)
        for entry in self.image.funcs:
            if self.image.chained(entry):
                fragments[self.image.primary(entry)[0]].append((entry[0], entry[1]))
        fragments = dict(fragments)
        pickle.dump(fragments, open(path, 'wb'))
        return fragments

    def callers(self, target):
        low = np.searchsorted(self.dst, target)
        high = np.searchsorted(self.dst, target, side='right')
        return [(int(s), 'call' if o == 0xE8 else 'jmp') for s, o in zip(self.src[low:high], self.op[low:high])]

    def fragments(self, rva):
        entry = self.image.func_at(rva)
        return [(entry[0], entry[1]) if entry else (rva, rva + 128)] + list(self.fragment_map.get(rva, []))

    def callees(self, rva):
        parts = self.fragments(rva)
        out = []
        for begin, end in parts:
            for ins in self.image.disasm(begin, end - begin):
                if ins.mnemonic in ('call', 'jmp') and ins.op_str.startswith('0x'):
                    target = int(ins.op_str, 16)
                    if not any(b <= target < e for b, e in parts):
                        out.append(target)
        return out

    def distinctive(self, rva):
        return {c for c in self.callees(rva) if c not in self.ifix and len(self.callers(c)) < HELPER_CALLERS}

    def size(self, rva):
        return sum(end - begin for begin, end in self.fragments(rva))

    # IFix checks ------------------------------------------------------------
    MOV_ID = re.compile(rb'([\xb8-\xbf])(....)(.{0,10}?)\xe8(....)', re.S)  # mov r32, id ... call
    LEA_ID = re.compile(rb'\x33\xd2\x8d\x4a(.)\xe8(....)', re.S)          # xor edx,edx; lea ecx,[rdx+i8]; call
    CMP_ID32 = re.compile(rb'\x81[\x78-\x7f]\x18(....)(\x7f(.)|\x0f\x8f(....))', re.S)
    CMP_ID8 = re.compile(rb'\x83[\x78-\x7f]\x18(.)(\x7f(.)|\x0f\x8f(....))', re.S)

    @staticmethod
    def _reaches_ecx(match):
        register, gap = match.group(1)[0] - 0xB8, match.group(3)
        return register == 1 or bytes([0x8B, 0xC8 + register]) in gap or bytes([0x89, 0xC1 + (register << 3)]) in gap

    def _ifix_call(self, base, match, rel_group):
        target = base + match.start(rel_group) + 4 + int.from_bytes(match.group(rel_group), 'little', signed=True)
        return self.ifix.get(target)

    def _cold_assembly(self, target, ident):
        data = self.image.read(target, 192)
        for match in self.MOV_ID.finditer(data):
            if self._reaches_ecx(match) and int.from_bytes(match.group(2), 'little') == ident:
                assembly = self._ifix_call(target, match, 4)
                if assembly:
                    return assembly
        for match in self.LEA_ID.finditer(data):
            if match.group(1)[0] == ident:
                assembly = self._ifix_call(target, match, 2)
                if assembly:
                    return assembly
        return None

    def _ifix_sites(self, path):
        if os.path.exists(path):
            return pickle.load(open(path, 'rb'))
        site_list = []
        for begin, end in self.image.code_ranges():
            data = self.image.read(begin, end - begin)
            for match in self.MOV_ID.finditer(data):
                assembly = self._ifix_call(begin, match, 4)
                if assembly and self._reaches_ecx(match):
                    site_list.append((begin + match.start(), assembly, int.from_bytes(match.group(2), 'little')))
            for match in self.LEA_ID.finditer(data):
                assembly = self._ifix_call(begin, match, 2)
                if assembly:
                    site_list.append((begin + match.start(), assembly, match.group(1)[0]))
            for pattern in (self.CMP_ID32, self.CMP_ID8):
                for match in pattern.finditer(data):
                    ident = int.from_bytes(match.group(1), 'little')
                    rel = match.group(3) if match.group(3) is not None else match.group(4)
                    cold = begin + match.end() + int.from_bytes(rel, 'little', signed=True)
                    assembly = self._cold_assembly(cold, ident)
                    if assembly:
                        site_list.append((begin + match.start(), assembly, ident))
        site_list.sort()
        sites = collections.defaultdict(list)
        for address, assembly, ident in site_list:
            sites[(assembly, ident)].append(address)
        result = (dict(sites), site_list)
        pickle.dump(result, open(path, 'wb'))
        return result

    def entry_check(self, rva):
        """The first IFix check on the entry path. Shrink-wrapping can split
        that path into back-to-back unwind ranges, so they are merged."""
        begin, end = self.fragments(rva)[0]
        for b, e in sorted(self.fragments(rva)):
            if b == end:
                end = e
        index = bisect.bisect_left(self.site_addresses, begin)
        if index < len(self.site_addresses) and self.site_addresses[index] < end:
            return self.site_list[index][1], self.site_list[index][2]
        return None


# --------------------------------------------------------------------- analysis

def is_function_entry(image, rva):
    entry = image.func_at(rva)
    if not entry:
        return image.read(rva - 1, 1)[0] == 0xCC  # leaf after int3 padding
    return entry[0] == rva and not image.chained(entry)


def tail_target(image, rva):
    entry = image.func_at(rva)
    leaf = entry is None
    end = entry[1] if entry else rva + 128
    for ins in image.disasm(rva, end - rva):
        if ins.mnemonic == 'jmp' and ins.op_str.startswith('0x'):
            target = int(ins.op_str, 16)
            limit = ins.address + ins.size if leaf else end
            if (target < rva or target >= limit) and is_function_entry(image, target):
                return target
        if leaf and ins.mnemonic in ('ret', 'int3', 'jmp'):
            break
    return None


def analyze(index, rva):
    image = index.image
    entry = image.func_at(rva)
    callers = index.callers(rva)
    result = dict(rva=rva, size=(entry[1] - entry[0]) if entry else None, leaf=entry is None,
                  calls=sum(1 for _, k in callers if k == 'call'), jmps=sum(1 for _, k in callers if k == 'jmp'),
                  tail=None, ifix=None, borrowed=None, copies=[])
    tail = tail_target(image, rva)
    if tail and not index.name(tail).startswith('IFix.'):  # IFix patch path, not a forward
        result['tail'] = dict(rva=tail, name=index.name(tail))
    key = index.entry_check(rva)
    if not key:
        return result
    owners = collections.Counter(image.owner(a) for a in index.sites.get(key, []))
    result['ifix'] = dict(assembly=key[0], id=key[1])
    mine = index.distinctive(rva)
    for other, count in owners.items():
        if other is None or other == rva:
            continue
        # A smaller function starting with the same check is the original:
        # this method only inlined it at its own entry.
        if index.entry_check(other) == key and index.size(other) < index.size(rva):
            result['borrowed'] = dict(rva=other, name=index.name(other))
        shared = len(mine & set(index.callees(other)))
        result['copies'].append(dict(rva=other, name=index.name(other), checks=count,
                                     shared_callees=shared, own_callees=len(mine),
                                     confirmed=bool(mine) and shared * 2 >= len(mine)))
    return result


def verdict(hook, result):
    virtual = bool(set(hook.get('mods') or []) & {'virtual', 'override', 'abstract'})
    confirmed = [c for c in result['copies'] if c['confirmed']]
    direct = result['calls'] + result['jmps']
    if confirmed and direct == 0 and not virtual:
        return 'never', 'every caller uses an inlined copy; the hook does not fire'
    if confirmed:
        return 'partial', 'some callers use an inlined copy and bypass the hook'
    if direct == 0 and not virtual:
        return 'no-direct-callers', ('no direct call site and no IFix copy: delegate, Unity message, '
                                     'reflection/Lua, or inlined without an IFix check')
    if result['tail']:
        return 'wrapper', 'forwards to another method; callers may inline it in a later build'
    return 'ok', ''


# ------------------------------------------------------------------------ input

def normalize_type(text):
    return (text or '').replace('&', '').replace(' ', '').strip()


def match_descriptor(methods, hook):
    parts = re.split(r'[./+]', hook['class'])
    wanted_types = None
    if hook.get('parameter_types'):
        text = hook['parameter_types']
        wanted_types = [normalize_type(t) for t in (text.split('|') if '|' in text else split_parameters(text))]

    def accept(m, check_namespace):
        if m['name'] != hook['method'] or m['short'] != parts[-1]:
            return False
        if hook.get('parameter_count') is not None and len(m['types']) != hook['parameter_count']:
            return False
        if hook.get('assembly') and m['asm'] != hook['assembly']:
            return False
        if check_namespace and len(parts) == 1 and hook.get('namespace') is not None and m['ns'] != (hook['namespace'] or ''):
            return False
        if wanted_types and len(wanted_types) == len(m['types']) and None not in m['types'] and \
                [normalize_type(t) for t in m['types']] != wanted_types:
            return False
        if hook.get('return_type') and m['ret'] is not None and normalize_type(hook['return_type']) != normalize_type(m['ret']):
            return False
        return True

    found = [m for m in methods if accept(m, True)]
    if not found and hook.get('namespace'):
        found = [m for m in methods if accept(m, False)]  # e.g. EIEM tries several namespaces
    return found


installed_re = re.compile(r'\[host\.hooks\] Hook installed for ([^:]+): (.*) at GameAssembly\.dll\+0x([0-9A-Fa-f]+)')


def hooks_from_log(path):
    hooks = {}
    with open(path, encoding='utf-8', errors='replace') as stream:
        for line in stream:
            match = installed_re.search(line)
            if match:
                rva = int(match.group(3), 16)
                hooks[rva] = dict(module=match.group(1), label=match.group(2), rva=rva)
    return list(hooks.values())


# ----------------------------------------------------------------------- report

def main():
    parser = argparse.ArgumentParser(description=__doc__.split('\n\n')[0])
    parser.add_argument('--game', required=True, help='game directory containing GameAssembly.dll')
    parser.add_argument('--dump', required=True, help='IL2CPP dump directory (IL2CPP_Dump_AI, IL2CPP_Dump_Normal) of the same build')
    source = parser.add_mutually_exclusive_group()
    source.add_argument('--hooks', default=os.path.join(HERE, 'hooked_methods.json'), help='hook descriptor list (default: hooked_methods.json)')
    source.add_argument('--log', help='BetterEndfieldNext.log with "Hook installed" lines')
    source.add_argument('--rva', nargs='+', help='explicit GameAssembly RVAs, e.g. 0x472E3D0')
    parser.add_argument('--json', help='write the full result as JSON')
    parser.add_argument('--all', action='store_true', help='also list hooks without findings')
    parser.add_argument('--cache', default=os.path.join(HERE, '.cache'))
    args = parser.parse_args()

    image = Image(os.path.join(args.game, 'GameAssembly.dll'))
    cache = os.path.join(args.cache, image.key)
    os.makedirs(cache, exist_ok=True)
    dump_cache = os.path.join(cache, 'dump.pkl')
    if os.path.exists(dump_cache):
        methods, modifiers = pickle.load(open(dump_cache, 'rb'))
    else:
        print('Parsing the IL2CPP dump...', file=sys.stderr)
        methods, modifiers = load_dump(args.dump)
        pickle.dump((methods, modifiers), open(dump_cache, 'wb'))
    by_rva = collections.defaultdict(list)
    for m in methods:
        by_rva[m['rva']].append(m)
    print('Indexing GameAssembly.dll (cached per build)...', file=sys.stderr)
    index = Index(image, methods, cache)

    hooks = []
    if args.rva:
        hooks = [dict(module='-', label=index.name(int(v, 16)), rva=int(v, 16)) for v in args.rva]
    elif args.log:
        hooks = hooks_from_log(args.log)
        if not hooks:
            sys.exit('No "Hook installed ... at GameAssembly.dll+0x..." lines in the log.')
    else:
        for hook in json.load(open(args.hooks, encoding='utf-8'))['managed']:
            found = match_descriptor(methods, hook)
            hook = dict(hook)
            if len(found) == 1 and found[0]['rva']:
                hook['rva'] = found[0]['rva']
            else:
                hook['unresolved'] = [f"{m['cls']}::{m['name']} @0x{m['rva']:X}" for m in found] or 'not in dump'
            hooks.append(hook)

    rows = []
    for hook in hooks:
        if 'rva' not in hook:
            rows.append(dict(hook=hook, verdict='unresolved'))
            continue
        records = by_rva.get(hook['rva'], [])
        hook['method'] = ' / '.join(f"{m['cls']}::{m['name']}" for m in records[:2]) or f"sub_{hook['rva']:X}"
        mods = set()
        for m in records:
            mods |= set(modifiers.get((m['asm'], m['token']), []))
        hook['mods'] = sorted(mods)
        result = analyze(index, hook['rva'])
        result['folded_with'] = [f"{m['cls']}::{m['name']}" for m in records[1:]]
        kind, note = verdict(hook, result)
        rows.append(dict(hook=hook, result=result, verdict=kind, note=note))

    order = {'never': 0, 'partial': 1, 'unresolved': 2, 'no-direct-callers': 3, 'wrapper': 4, 'ok': 5}
    counts = collections.Counter(r['verdict'] for r in rows)
    print(f'GameAssembly.dll {image.key}: {len(rows)} hooks; ' +
          ', '.join(f'{k} {counts[k]}' for k in order if counts[k]))
    for row in sorted(rows, key=lambda r: order[r['verdict']]):
        hook, kind = row['hook'], row['verdict']
        if kind == 'ok' and not args.all:
            continue
        if kind == 'unresolved':
            print(f"\n[unresolved] {hook.get('module')} {hook.get('label')}: {hook.get('unresolved')}")
            continue
        result = row['result']
        print(f"\n[{kind}] {hook.get('module')} {hook.get('label')}: {hook['method']} @0x{hook['rva']:X}"
              f"{' ' + ','.join(hook['mods']) if hook['mods'] else ''}")
        if row['note']:
            print(f"    {row['note']}")
        size = 'leaf' if result['leaf'] else f"{result['size']} bytes"
        print(f"    {size}, direct call sites {result['calls']}, tail-call sites {result['jmps']}"
              + (f", IFix {result['ifix']['assembly']}#{result['ifix']['id']:#x}" if result['ifix'] else ''))
        if result['tail']:
            print(f"    tail-jumps to {result['tail']['name']} @0x{result['tail']['rva']:X}")
        if result['folded_with']:
            print(f"    shares its entry with {', '.join(result['folded_with'][:4])}")
        if result['borrowed']:
            print(f"    entry check also starts the smaller {result['borrowed']['name']}; copies below are doubtful")
        for copy in result['copies']:
            mark = 'inlined copy' if copy['confirmed'] else 'unconfirmed '
            print(f"    {mark} in {copy['name']} @0x{copy['rva']:X} (callees {copy['shared_callees']}/{copy['own_callees']})")
    if args.json:
        json.dump(rows, open(args.json, 'w', encoding='utf-8'), ensure_ascii=False, indent=1)


if __name__ == '__main__':
    main()
