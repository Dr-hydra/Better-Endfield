"""Estimate selected upload bytes from BEM metadata, without decoding payloads.

These are requested resource sizes and reference scenarios, not GPU residency
measurements. Source Texture identities and driver staging require runtime data.
"""
from __future__ import annotations

import argparse
import json
from collections import Counter
from pathlib import Path

import bem_v1 as v1
import bem_v11 as v11


def chain_bytes(width, height, mips, block=None, channels=4):
    total = 0
    for level in range(mips):
        w, h = max(width >> level, 1), max(height >> level, 1)
        total += ((w + block - 1) // block) * ((h + block - 1) // block) * 16 if block else w * h * channels
    return total


def report(path, options=None, appearance=None):
    manifest, _ = v1.read_package(path, decode=False)
    with Path(path).open('rb') as stream:
        header = v1.HEADER.unpack(stream.read(v1.HEADER.size))
        stream.seek(v1.HEADER.size + header[5])
        directory = list(v1.ENTRY.iter_unpack(stream.read(header[6] * v1.ENTRY.size)))
    return report_metadata(Path(path).resolve(), manifest, header, directory, options, appearance)


def report_metadata(path, manifest, header, directory, options=None, appearance=None):
    """Also accepts a separately captured manifest/directory from a device.

    The caller must validate the original header/file extent when only metadata
    is available; a truncated prefix is not a standalone valid BEM package.
    """
    if header[2]:
        if appearance:
            raise ValueError('Use --options for BEM 1.1 and later')
        plan = v11.selection_plan(manifest, options, minor=header[2])
        components = plan['components']
        selected = plan['textures']
        payloads = set(plan['payloads'])
        selection = plan['saved']
    else:
        if options:
            raise ValueError('Use --appearance for BEM 1.0')
        selection = appearance or manifest['default_appearance_id']
        components = next(a['components'] for a in manifest['appearances'] if a['id'] == selection)
        selected, payloads = set(), set()
    component_refs, material_refs = Counter(), set()
    geometry = 0
    for component in components:
        draws = component.get('material_overrides', [])
        if component['operation'] == 'replace':
            mesh = manifest['meshes'][component['mesh']]
            draws = component.get('draws', mesh['draws'])
            geometry += mesh['vertex_count'] * sum(s['stride'] for s in mesh['streams'])
            geometry += (component['index_count'] if header[2] else mesh['index_count']) * mesh['index_size']
            payloads.update(s['payload'] for s in mesh['streams'])
            if not header[2]:
                payloads.add(mesh['indices'])
        refs = set()
        for draw in draws:
            for texture in draw['textures']:
                refs.add(texture)
                material_refs.add((component['target'], draw.get('material_component', component['target']),
                                   draw['material_slot'], texture))
        component_refs.update(refs)
        if not header[2]:
            selected.update(refs)
    selected = sorted(selected)
    textures = []
    for index in selected:
        texture = manifest['textures'][index]
        payloads.add(texture['payload'])
        size = directory[texture['payload']][4]
        w, h, mips = texture['width'], texture['height'], texture['mips']
        fmt = texture['format']
        # Follow the current installer's format policy. ASTC/R8 passthroughs
        # stay unchanged; the 2K column is a proposed, uniformly capped mode.
        block = 6 if texture['srgb'] else 4
        skip = 0
        while max(w >> skip, h >> skip) > 2048:
            skip += 1
        capped_mips = max(mips - skip, 1)
        capped_w, capped_h = max(w >> skip, 1), max(h >> skip, 1)
        converted = size if fmt in (48, 49, 50, 63) else chain_bytes(w, h, mips, block)
        textures.append(dict(index=index, name=texture['original_name'], width=w, height=h, mips=mips,
                             format=fmt, bytes=size, component_references=component_refs[index],
                             material_references=sum(key[3] == index for key in material_refs),
                             rgba32_equivalent_bytes=chain_bytes(w, h, mips),
                             astc_policy_bytes_before_install_limits=converted,
                             proposed_2k_bytes=chain_bytes(capped_w, capped_h, capped_mips,
                                 None if fmt == 63 else ({48: 4, 49: 5, 50: 6}.get(fmt, block)),
                                 1 if fmt == 63 else 4)))
    return dict(path=str(path), selection=selection, file_bytes=header[4],
                selected_decoded_payload_bytes=sum(directory[p][4] for p in payloads),
                geometry_upload_bytes=geometry, unique_selected_texture_bytes=sum(t['bytes'] for t in textures),
                per_component_texture_reference_bytes=sum(t['bytes'] * t['component_references'] for t in textures),
                per_material_texture_reference_bytes=sum(t['bytes'] * t['material_references'] for t in textures),
                per_component_astc_policy_bytes_before_install_limits=sum(
                    t['astc_policy_bytes_before_install_limits'] * t['component_references'] for t in textures),
                per_component_proposed_2k_bytes=sum(t['proposed_2k_bytes'] * t['component_references'] for t in textures),
                textures=textures)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('packages', nargs='+', type=Path)
    parser.add_argument('--options', help='group:choice&group:choice')
    parser.add_argument('--appearance')
    parser.add_argument('--output', type=Path)
    args = parser.parse_args()
    options = dict(item.split(':', 1) for item in args.options.split('&')) if args.options else None
    results = [report(path, options, args.appearance) for path in args.packages]
    serialized = json.dumps(results, ensure_ascii=False, indent=2)
    if args.output:
        args.output.parent.mkdir(parents=True, exist_ok=True)
        args.output.write_text(serialized + '\n', encoding='utf-8')
    else:
        print(serialized)


if __name__ == '__main__':
    main()
