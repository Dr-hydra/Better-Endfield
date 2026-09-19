import bisect
import json
import re
import struct
from pathlib import Path
import ida_auto
import ida_bytes
import ida_idaapi
import ida_idp
import ida_nalt
import ida_ua
import idc

ROOT = Path(r'E:\Dr.Hydra\Better Endfield\.worktrees\dev-custom-model-design\research\postmodel-focused')
DUMP = Path(r'E:\Dr.Hydra\Better Endfield\research\il2cpp-dumps\20260903-pc-current\IL2CPP_Dump_Normal')
TARGETS = {
    ('SquadManager', '_PreloadEndmin'),
    ('PreloadGroupChar', '_Preload_Mesh'),
    ('PrefabInstantiateProxy', '_PreloadAsset'),
    ('PrefabInstantiateProxy', '_PreloadPrefab'),
    ('PrefabInstantiateProxy', '_PreloadEnd'),
    ('PrefabInstantiateProxy', 'OnCompleted'),
    ('NPCAvatarManager', 'CreateNpcComponentByPostModel'),
    ('NPCAvatarManager', '_RefreshAllPartsBind'),
}

def main():
    ida_auto.enable_auto(False)
    names, selected = {}, {}
    for file in DUMP.glob('*.cs'):
        cls, rva = '', None
        for line in file.open(encoding='utf-8-sig'):
            m = re.search(r'\b(?:class|struct) ([^ :]+)', line)
            if m:
                cls = m.group(1).strip()
            prop = re.search(r'([^\s]+) \{ get; /\* RVA: (0x[0-9A-Fa-f]+)', line)
            if prop:
                names.setdefault(int(prop.group(2), 16), []).append(cls + '.get_' + prop.group(1))
            m = re.search(r'// RVA: (0x[0-9A-Fa-f]+)', line)
            if m:
                rva = int(m.group(1), 16)
                continue
            if rva is not None:
                m = re.search(r'([^\s(]+)\(', line)
                if m:
                    method = m.group(1)
                    names.setdefault(rva, []).append(cls + '.' + method)
                    if (cls, method) in TARGETS:
                        selected[rva] = cls + '.' + method
                rva = None
    selected = {0x03C178E0: 'PostModel.get_callee', 0x03BD3510: 'PostModel.clone_callee', 0x03DC4D50: 'NPCAvatarCreatorUtils.CreateMeshAssetsInfoForPostModel', 0x03038BC0: 'BundleResourceManager.LoadAsync', 0x03944C00: 'FAssetProxyHandle.Get', 0x00058F70: 'CacheFallback.indirect_dispatch'}
    selected = {0x03825490: 'AssetProxy._FinishWithAsset', 0x03825380: 'AssetProxy._FinishAsyncRequest', 0x03170B60: 'AssetProxy._OnAsyncCompleted', 0x03824030: 'AssetProxy._LoadImmediate', 0x0303A610: 'BundleResourceManager._LoadAssetInternal', 0x03944E50: 'AssetProxy.Get', 0x03824850: 'AssetProxy.LoadAsync'}
    selected = {0x02C8DF00: 'AssetProxy.UpdateLoading', 0x0303A9D0: 'Manager.LoadAsset', 0x03DC51F0: 'NPCAvatarCreatorUtils.CreateSMSInfoForPostModel'}
    selected = {0x0370B9D0: 'BaseModelViewPart._OnLoadModelFinish', 0x0370B730: 'BaseModelViewPart._OnLoadUseHandleFinish', 0x0370BAC0: 'BaseModelViewPart.OnLoadFinish', 0x0372E700: 'BaseModelViewPart.PostDealLoadedModel', 0x0372E200: 'ComplexModelViewPart.PostDealLoadedModel'}
    binary = Path(ida_nalt.get_input_file_path())
    data = binary.read_bytes()
    pe = struct.unpack_from('<I', data, 0x3c)[0]
    nsects = struct.unpack_from('<H', data, pe + 6)[0]
    optsize = struct.unpack_from('<H', data, pe + 20)[0]
    opt = pe + 24
    sections = []
    for i in range(nsects):
        p = opt + optsize + i * 40
        vs, va, rs, raw = struct.unpack_from('<IIII', data, p + 8)
        sections.append((va, max(vs, rs), raw))
    def offset(r):
        return next(raw + r - va for va, size, raw in sections if va <= r < va + size)
    pdata, size = struct.unpack_from('<II', data, opt + 112 + 3 * 8)
    ranges = [struct.unpack_from('<III', data, offset(pdata) + i)[:2] for i in range(0, size, 12)]
    ranges.sort()
    starts = [p[0] for p in ranges]
    base = ida_nalt.get_imagebase()
    results = []
    for rva, label in sorted(selected.items()):
        idx = bisect.bisect_right(starts, rva) - 1
        begin, end = ranges[idx]
        result = {'name': label, 'rva': hex(rva), 'pdata_begin': hex(begin), 'pdata_end': hex(end), 'calls': [], 'asm': []}
        if begin != rva or end - begin > 0x10000:
            result['error'] = 'entry mismatch or size cap'
            results.append(result)
            continue
        queue, seen = [base + rva], set()
        while queue and len(seen) < 8000:
            ea = queue.pop()
            if ea in seen:
                continue
            seen.add(ea)
            insn = ida_ua.insn_t()
            length = ida_ua.decode_insn(insn, ea)
            if not length:
                result['error'] = 'decode failed at ' + hex(ea - base)
                continue
            ida_ua.create_insn(ea)
            line = idc.generate_disasm_line(ea, 0) or ''
            if insn.get_canon_mnem() in ('call', 'jmp'):
                target = insn.ops[0].addr if insn.ops[0].type == ida_ua.o_near else None
                aliases = names.get(target - base, []) if target is not None else []
                result['calls'].append({'at': hex(ea - base), 'target': hex(target - base) if target is not None else None, 'names': aliases, 'instruction': line})
                if aliases:
                    line += ' ; ' + ' | '.join(aliases[:4])
            result['asm'].append(hex(ea - base) + ' ' + line)
            mnemonic = insn.get_canon_mnem()
            if mnemonic.startswith('j') and insn.ops[0].type == ida_ua.o_near:
                target = insn.ops[0].addr
                if target - base not in names or target == base + rva:
                    queue.append(target)
            if not (insn.get_canon_feature() & ida_idp.CF_STOP) and ea + length - base not in names:
                queue.append(ea + length)
        result['asm'].sort(key=lambda line: int(line.split()[0], 16))
        results.append(result)
    (ROOT / 'model-consumer.json').write_text(json.dumps({'input': str(binary), 'selected_count': len(selected), 'functions': results}, ensure_ascii=False, indent=2), encoding='utf-8')
    ida_auto.enable_auto(False)
    idc.qexit(0)

try:
    main()
except Exception:
    import traceback
    (ROOT / 'error.txt').write_text(traceback.format_exc(), encoding='utf-8')
    idc.qexit(1)


