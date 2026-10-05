# -*- coding: utf-8 -*-
"""IDAPython: 对已分析的 IDB 查询目标函数 xref 与反编译(NEW/OLD 通用)
用法: idat.exe -A -S"ida_query.py <out.json> <out.txt> <new|old>" <idb 或 dll>
"""
import io
import json
import sys

import idaapi
import idautils
import idc
import ida_funcs

OUT_JSON = sys.argv[1] if len(sys.argv) > 1 else r'E:\Dr.Hydra\Better Endfield\tmp_analysis\ida\query.json'
OUT_TXT = sys.argv[2] if len(sys.argv) > 2 else r'E:\Dr.Hydra\Better Endfield\tmp_analysis\ida\query.txt'
MODE = sys.argv[3] if len(sys.argv) > 3 else 'new'

NEW_TARGETS = {
    0x05F625DC: 'CharacterAnimationComponent::StartSpDash',
    0x05F62C00: 'CharacterAnimationComponent::_TryPlaySpDashPerform',
    0x03128CA0: 'CharacterAnimationComponent::IsPlayingSpDashPerform',
    0x05F60098: 'CharacterAnimationComponent::InterruptSpDashPerform',
    0x05F5FF58: 'CharacterAnimationComponent::ForceStopSpDashPerform',
    0x05FA3178: 'CharacterSpecialDashBrain::ShouldInterruptSpDash',
    0x02D2ADC0: 'CharacterSpecialDashBrain::Update',
    0x05FA3214: 'CharacterSpecialDashBrain::TryStartSpDash',
    0x03A19480: 'CharacterAnimationComponent::Dash',
    0x0472E3D0: 'InitMainPathHash',
    0x036BD820: 'InitInitPathHash',
    0x0364E0E0: 'TryGetVoiceDuration(String,Single&)',
    0x03650240: 'TryGetVoiceDuration(Int32,Single&)',
    0x05DFA9F4: 'SetSpeakerCustomLang',
    0x03FA2C30: 'TryGetSpeakerCustomLang',
    0x05DFA7C4: 'GetCustomLanguageVoicePath',
    0x05DFA8F4: 'GetVoicePath',
    0x04114DF0: 'TryLoadLanguagePck',
    0x04116110: '_DoLoadLanguageAndHotfixPck',
    0x041175B0: '_UnloadPcks',
    0x04114D60: 'SetLanguage_calltarget_04114D60',
    0x03499610: 'GetCurrentLanguage',
}

OLD_TARGETS = {
    0x04994340: 'InitMainPathHash',
    0x0311B400: 'InitInitPathHash',
    0x03ABB800: 'TryGetVoiceDuration(String,Single&)',
    0x03ABCE00: 'TryGetVoiceDuration(Int32,Single&)',
    0x06B02B1C: 'GetVoicePath',
    0x03EB3F70: 'SetLanguage',
    0x03AC13B0: 'GetCurrentLanguage',
    0x03EB4EA0: '_DoLoadLanguageAndHotfixPck',
    0x03EB5D80: '_UnloadPcks',
    0x03EB45A0: 'TryLoadLanguagePck',
}

DECOMPILE_NEW = [
    (0x05F625DC, 'CharacterAnimationComponent::StartSpDash'),
    (0x05F62C00, 'CharacterAnimationComponent::_TryPlaySpDashPerform'),
    (0x03128CA0, 'CharacterAnimationComponent::IsPlayingSpDashPerform'),
    (0x05F60098, 'CharacterAnimationComponent::InterruptSpDashPerform'),
    (0x05F5FF58, 'CharacterAnimationComponent::ForceStopSpDashPerform'),
    (0x05FA3178, 'CharacterSpecialDashBrain::ShouldInterruptSpDash'),
    (0x02D2ADC0, 'CharacterSpecialDashBrain::Update'),
    (0x05FA3214, 'CharacterSpecialDashBrain::TryStartSpDash'),
    (0x03A19480, 'CharacterAnimationComponent::Dash'),
    (0x04116110, '_DoLoadLanguageAndHotfixPck'),
    (0x041175B0, '_UnloadPcks'),
    (0x04114DF0, 'TryLoadLanguagePck'),
    (0x05DFA8F4, 'GetVoicePath'),
    (0x05DFA7C4, 'GetCustomLanguageVoicePath'),
    (0x03FA2C30, 'TryGetSpeakerCustomLang'),
    (0x05DFA9F4, 'SetSpeakerCustomLang'),
    (0x0364E0E0, 'TryGetVoiceDuration(String,Single&)'),
]
# 时长调用方所在函数(经 get_func 由调用点定位)
SITE_DECOMPILE_NEW = [
    (0x06127283, 'DialogManager__PlayVoice'),
    (0x06156D0C, 'DialogTimelineManager__PlayVoice'),
    (0x0614F20C, 'MainFlowHandle__GetDuration'),
]
DECOMPILE_OLD = [
    (0x03EB4EA0, '_DoLoadLanguageAndHotfixPck'),
    (0x03EB5D80, '_UnloadPcks'),
    (0x03EB45A0, 'TryLoadLanguagePck'),
]


def main():
    idaapi.auto_wait()
    base = idaapi.get_imagebase()
    targets = NEW_TARGETS if MODE == 'new' else OLD_TARGETS
    dec = DECOMPILE_NEW if MODE == 'new' else DECOMPILE_OLD

    res = {}
    for rva, name in targets.items():
        ea = base + rva
        f = ida_funcs.get_func(ea)
        sites = []
        for xr in idautils.XrefsTo(ea):
            cf = ida_funcs.get_func(xr.frm)
            sites.append({
                'site_rva': hex(xr.frm - base),
                'func_rva': hex(cf.start_ea - base) if cf else None,
                'func_name': idc.get_func_name(cf.start_ea) if cf else None,
                'type': int(xr.type),
            })
        res[name] = {
            'rva': hex(rva),
            'has_func': bool(f),
            'func_start': hex(f.start_ea - base) if f else None,
            'func_size': int(f.size()) if f else 0,
            'ida_name': idc.get_func_name(ea),
            'xref_count': len(sites),
            'sites': sites[:100],
        }

    try:
        with io.open(OUT_JSON, 'w', encoding='utf-8') as fp:
            json.dump(res, fp, indent=1)
    except Exception as e:
        print('json write failed: %s' % e)

    with io.open(OUT_TXT, 'w', encoding='utf-8') as fp:
        fp.write('imagebase=%s mode=%s\n' % (hex(base), MODE))
        for rva, name in targets.items():
            r = res[name]
            fp.write('=== %s @ %s (func=%s size=%s xrefs=%d) ===\n'
                     % (name, r['rva'], r['has_func'], r['func_size'], r['xref_count']))
            for s in r['sites'][:60]:
                fp.write('  %s %s in %s (%s)\n'
                         % (s['type'], s['site_rva'], s['func_name'], s['func_rva']))
        try:
            import ida_hexrays
            ida_hexrays.init_hexrays_plugin()
            plan = [(base + rva, name) for rva, name in dec]
            if MODE == 'new':
                plan += [(base + site, name) for site, name in SITE_DECOMPILE_NEW]
            done_funcs = set()
            for ea, name in plan:
                fp.write('\n########## DECOMPILE %s @ %s ##########\n' % (name, hex(ea - base)))
                try:
                    cf = ida_funcs.get_func(ea)
                    if not cf:
                        fp.write('(no function)\n')
                        continue
                    start = cf.start_ea
                    fp.write('(function start %s size %d)\n' % (hex(start - base), int(cf.size())))
                    if start in done_funcs:
                        fp.write('(already dumped)\n')
                        continue
                    done_funcs.add(start)
                    src = ida_hexrays.decompile(start)
                    text = str(src)
                    lines = text.splitlines()
                    if len(lines) > 400:
                        lines = lines[:400] + ['... (truncated %d lines)' % (len(lines) - 400)]
                    fp.write('\n'.join(lines) + '\n')
                except Exception as e:
                    fp.write('(decompile failed: %s)\n' % e)
        except Exception as e:
            fp.write('(hexrays unavailable: %s)\n' % e)
    idc.qexit(0)


main()
