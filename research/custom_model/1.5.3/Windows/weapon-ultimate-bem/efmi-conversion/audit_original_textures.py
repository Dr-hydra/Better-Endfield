"""Audit original base-level EFMI texture identities without executing source INIs.

This verifies the EFMI CRC32C resource-identity rule, not a cryptographic equality
claim. A roundtrip identifies an unchanged source base mip under that rule; it
does not identify a current native material property or prove full mip equality.
Only the separately recorded rain comparison uses actual native texture bytes.
"""
import json
import re
import struct
from pathlib import Path

from audit_inputs import ROOT, SOURCES, sections, value, crc32c
from convert_efmi_poc import parse_dds


TARGETS = {
    '5e32e452', 'bd0ceafb', '7d330dbd', '7ffdcd29',
    'c5d91ba4', '8d49a988', 'e3a8a8f7', 'fe1d6277',
}
GLOBAL_ROOT = Path('G:/zmd_bem/research/identities')
RAIN_ID = 'cab-5edbf457631197eaf84a8374582f8cca:-4292390226595857988'


def payload_crc32c(data):
    """Table equivalent of the existing EFMI CRC32C implementation, seed zero."""
    table = []
    for entry in range(256):
        for _ in range(8):
            entry = (entry >> 1) ^ (0x82f63b78 if entry & 1 else 0)
        table.append(entry)
    result = 0xffffffff
    for byte in data:
        result = table[(result ^ byte) & 255] ^ (result >> 8)
    return result ^ 0xffffffff


def rain_comparison(identity, width, height, fmt, srgb, base):
    if identity != '7d330dbd':
        return None
    graph_path = GLOBAL_ROOT / 'global-render-native.json'
    if not graph_path.is_file():
        return {'status': 'external_evidence_unavailable', 'graph': str(graph_path)}
    graph = json.loads(graph_path.read_text(encoding='utf-8-sig'))
    texture = next(obj for obj in graph['objects'] if obj['id'] == RAIN_ID)
    raw_path = GLOBAL_ROOT / 'native-global-textures' / texture['raw_texture_file']
    native_size = ((texture['width'] + 3) // 4) * ((texture['height'] + 3) // 4) * 16
    with raw_path.open('rb') as stream:
        native_base = stream.read(native_size)
    return {
        'status': 'compared',
        'graph': str(graph_path), 'raw_texture': str(raw_path),
        'texture_id': RAIN_ID, 'texture_name': texture['name'], 'native_mip': 0,
        'metadata_equal': (width, height, fmt, srgb) ==
                          (texture['width'], texture['height'], 25, False),
        'base_bytes': native_size, 'byte_identical': base == native_base,
    }


def audit():
    # Confirm the fast implementation agrees with the source identity algorithm.
    for probe in (b'', b'123456789', bytes(range(256))):
        assert payload_crc32c(probe) == crc32c(0, probe)
    results = {}
    for task, source in SOURCES.items():
        ini = sections((source / 'mod.ini').read_text(encoding='utf-8-sig'))
        entries = []
        for section, body in ini.items():
            if not section.startswith('Resource_Texture'):
                continue
            filename = value(body, 'filename')
            match = re.search(r't=([0-9a-fA-F]{8})', filename)
            if not match or match[1].lower() not in TARGETS:
                continue
            identity = match[1].lower()
            width, height, mips, fmt, srgb, payload = parse_dds(filename, (source / filename).read_bytes())
            # These selected inputs are BC7 or BC3; no format/color guessing.
            if fmt not in (25, 12):
                raise ValueError(f'{task}/{section}: unsupported audit format {fmt}')
            dxgi = ({25: 99, 12: 78} if srgb else {25: 98, 12: 77})[fmt]
            base_size = ((width + 3) // 4) * ((height + 3) // 4) * 16
            base = payload[:base_size]
            seed = payload_crc32c(base)
            matches = []
            # Exported DDS may contain mip0 only. Check legal original mip counts
            # while keeping size, typed format, and EFMI descriptor policy fixed.
            for original_mips in range(1, max(width, height).bit_length() + 1):
                descriptor = [width, height, original_mips, 1, dxgi, 1, 0, 0, 8, 0, 0]
                roundtrip = f'{crc32c(seed, struct.pack("<11I", *descriptor)):08x}'
                if roundtrip == identity:
                    matches.append({'descriptor': descriptor, 'identity': roundtrip})
            declared_hashes = sorted({value(text, 'hash').lower() for name, text in ini.items()
                                      if name.startswith('TextureOverride_Texture')
                                      and value(text, 'this') == section})
            if declared_hashes and identity not in declared_hashes:
                raise ValueError(f'{task}/{section}: filename identity not declared by source override')
            direct_bindings = [{'section': name, 'slot': match.group(1)}
                               for name, text in ini.items()
                               for line in text.splitlines()
                               if (match := re.fullmatch(r'\s*((?:ps|vs|cs)-t\d+)\s*=\s*(?:ref\s+)?'
                                                        + re.escape(section) + r'\s*', line))]
            row = {
                'resource': section, 'source_file': str(source / filename),
                'identity': identity, 'source_override_hashes': declared_hashes,
                'source_direct_bindings': direct_bindings,
                'width': width, 'height': height, 'source_mips': mips,
                'format': fmt, 'srgb': srgb, 'source_mip0_bytes': len(base),
                'source_mip0_crc32c': f'{seed:08x}', 'descriptor_matches': matches,
                'classification': ('unchanged_source_base_under_efmi_identity_rule'
                                   if matches and identity in declared_hashes
                                   else 'unresolved_by_source_identity_roundtrip'),
                'current_native_property_identified': False,
                'full_native_mip_chain_equivalence_proven': False,
            }
            native = rain_comparison(identity, width, height, fmt, srgb, base)
            if native is not None:
                row['external_native_base_comparison'] = native
            entries.append(row)
        results[task] = entries
    report = {
        'schema': 1,
        'method': 'source-mip0-CRC32C-then-EFMI-11xuint-DX11-descriptor-roundtrip',
        'descriptor_fields': ['Width', 'Height', 'MipLevels', 'ArraySize', 'Format',
                              'SampleCount', 'SampleQuality', 'Usage', 'BindFlags',
                              'CPUAccessFlags', 'MiscFlags'],
        'scope': 'The selected source texture overrides only; no source INI execution.',
        'interpretation': ('Roundtrip supports retaining current native textures to preserve the author\'s '
                           'modification intent for original-base overrides. It does not prove current '
                           'native identity, full mip-chain equality, or live rendering equivalence.'),
        'results': results,
    }
    output = ROOT / 'original-texture-audit.json'
    output.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({task: {row['identity']: row['classification'] for row in entries}
                      for task, entries in results.items()}, ensure_ascii=False))


if __name__ == '__main__':
    audit()
