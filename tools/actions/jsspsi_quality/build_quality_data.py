#!/usr/bin/env python3
"""Generate native/modules/actions/jsspsi_quality_data.h for the Jsspsi quality option.

Inputs (none are stored in the repository; see README.md for their SHA-256):
  --seam       BESEAM03 body seam table recorded by the full implementation
  --accessory  BEACC001 accessory loop table, 1024 samples per lap
  --capture    BECAP001 captures; the first one supplies Transform paths, the
               right/left ones selected by --compare-* are only used for the
               error report
  --catalog    tools/CustomModel/catalog/chr_0036_jsspsi.json (skinned bones)

The header holds the per-bone seam differences that exceed the thresholds and
a compact accessory table (int16 rotations, one local position per bone).
A JSON report with sizes and errors is written next to the header unless
--report says otherwise.
"""
from __future__ import annotations

import argparse
import hashlib
import json
import struct
import sys
from pathlib import Path

import numpy as np

RIGHT, LEFT = -445642423, 528430122
MOUNT_WINDOW = (.126, .315)  # The mount rewinds with the rider.
BEGIN, TARGET = .315, .126
ROTATION_SCALE = 32767
POSITION_UNIT = 1e-5  # metres per int16 step
ANGLE_THRESHOLD = .1  # degrees
POSITION_THRESHOLD = 1e-4  # metres
SAMPLES = 128
CUTS = {RIGHT: {0: (.385, .275), 1: (.395, .310)}, LEFT: {0: (.390, .265), 1: (.395, .310)}}


def read_seam(path):
    data = Path(path).read_bytes()
    if data[:8] != b'BESEAM03':
        raise SystemExit(f'{path}: not BESEAM03')
    count, = struct.unpack_from('<I', data, 8)
    offset, entries = 12, []
    for _ in range(count):
        state, target, begin, bones = struct.unpack_from('<iffI', data, offset)
        offset += 16
        table = {}
        for _ in range(bones):
            length, = struct.unpack_from('<H', data, offset)
            name = data[offset + 2:offset + 2 + length].decode('utf-8')
            offset += 2 + length
            values = np.array(struct.unpack_from('<7f', data, offset), dtype=np.float64)
            offset += 28
            rotation = values[:4] / np.linalg.norm(values[:4])
            if rotation[3] < 0:
                rotation = -rotation
            table[name] = (rotation, values[4:])
        entries.append(dict(hash=state, target=target, begin=begin, bones=table))
    if offset != len(data):
        raise SystemExit(f'{path}: trailing data')
    return entries


def read_accessory(path):
    data = Path(path).read_bytes()
    if data[:8] != b'BEACC001':
        raise SystemExit(f'{path}: not BEACC001')
    _, count = struct.unpack_from('<II', data, 8)
    offset, entries = 16, []
    for _ in range(count):
        state, begin, target, samples, bones = struct.unpack_from('<iffII', data, offset)
        offset += 20
        names = []
        for _ in range(bones):
            group = data[offset]
            length, = struct.unpack_from('<H', data, offset + 1)
            names.append((group, data[offset + 3:offset + 3 + length].decode('utf-8')))
            offset += 3 + length
        values = np.frombuffer(data, dtype='<f4', count=samples * bones * 7, offset=offset)
        offset += samples * bones * 28
        values = values.reshape(samples, bones, 7).astype(np.float64)
        values[..., :4] /= np.linalg.norm(values[..., :4], axis=-1, keepdims=True)
        entries.append(dict(hash=state, begin=begin, target=target, bones=names, data=values))
    if offset != len(data):
        raise SystemExit(f'{path}: trailing data')
    return entries


def read_capture(path):
    data = Path(path).read_bytes()
    if data[:8] != b'BECAP001':
        raise SystemExit(f'{path}: not BECAP001')
    bones, frames = struct.unpack_from('<II', data, 8)
    offset, groups, paths = 16, [], []
    for _ in range(bones):
        groups.append(data[offset])
        length, = struct.unpack_from('<H', data, offset + 1)
        paths.append(data[offset + 3:offset + 3 + length].decode('utf-8'))
        offset += 3 + length
    stride = 2 + bones * 7
    values = np.frombuffer(data, dtype='<f4', count=frames * stride, offset=offset).reshape(frames, stride)
    poses = values[:, 2:].reshape(frames, bones, 7).astype(np.float64)
    return dict(groups=groups, paths=paths, time=values[:, 0].astype(np.float64),
                rotation=poses[..., :4], position=poses[..., 4:])


def normalize(q):
    return q / np.linalg.norm(q, axis=-1, keepdims=True)


def angle(a, b):
    return np.degrees(2 * np.arccos(np.clip(np.abs(np.sum(a * b, -1)), 0, 1)))


def slerp(a, b, f):
    # Same branch rule as PoseSlerp: shortest arc, nlerp from dot 0.9995.
    dot = np.sum(a * b, -1, keepdims=True)
    b = np.where(dot < 0, -b, b)
    dot = np.abs(dot)
    f = np.asarray(f, dtype=np.float64)[..., None]
    theta = np.arccos(np.clip(dot, -1, 1))
    near = dot >= .9995
    s = np.where(near, 1, np.sin(theta))
    x = np.where(near, 1 - f, np.sin((1 - f) * theta) / s)
    y = np.where(near, f, np.sin(f * theta) / s)
    return normalize(x * a + y * b)


def sample(rotations, phase):
    count = rotations.shape[0]
    frame = (phase % 1.0) * count
    k = np.floor(frame).astype(int) % count
    f = np.broadcast_to((frame - np.floor(frame))[:, None], (len(phase), rotations.shape[1]))
    return slerp(rotations[k], rotations[(k + 1) % count], f)


def rotate(q, v):
    t = 2 * np.cross(q[..., :3], v)
    return v + q[..., 3:] * t + np.cross(q[..., :3], t)


def chain_tip(rotations, positions, chain):
    tip = positions[..., chain[-1], :]
    for j in reversed(chain[:-1]):
        tip = positions[..., j, :] + rotate(rotations[..., j, :], tip)
    return tip


def leaf(path):
    return path.rsplit('/', 1)[-1]


def paths_by_leaf(capture, group):
    table = {}
    for g, path in zip(capture['groups'], capture['paths']):
        if g == group and path != '.':
            table.setdefault(leaf(path), []).append(path)
    return table


class Tree:
    def __init__(self):
        self.nodes, self.index = [], {}

    def add(self, path):
        if path in self.index:
            return self.index[path]
        parent = -1
        if '/' in path:
            parent = self.add(path.rsplit('/', 1)[0])
        self.index[path] = len(self.nodes)
        self.nodes.append((parent, leaf(path)))
        return self.index[path]


def quantize_seam(rotation, position):
    xyz = np.round(rotation[:3] * ROTATION_SCALE).astype(int)
    pos = np.round(position / POSITION_UNIT).astype(int)
    if np.abs(xyz).max() > 32767 or np.abs(pos).max() > 32767:
        raise SystemExit('seam value outside int16 range')
    w = np.sqrt(max(0.0, 1 - np.sum((xyz / ROTATION_SCALE) ** 2)))
    decoded = normalize(np.append(xyz / ROTATION_SCALE, w))
    return xyz, pos, angle(decoded, rotation), np.linalg.norm(pos * POSITION_UNIT - position)


def seam_entry(entry, capture_paths, tree, report_key, report):
    bones, skipped_small, missing, errors = [], 0, [], ([], [])
    for name in sorted(entry['bones']):
        rotation, position = entry['bones'][name]
        degrees = np.degrees(2 * np.arccos(min(1.0, abs(rotation[3]))))
        if degrees <= ANGLE_THRESHOLD and np.linalg.norm(position) <= POSITION_THRESHOLD:
            skipped_small += 1
            continue
        paths = capture_paths.get(name)
        if not paths:
            missing.append(name)
            continue
        xyz, pos, angle_error, position_error = quantize_seam(rotation, position)
        errors[0].append(float(angle_error))
        errors[1].append(float(position_error))
        # Duplicate leaves (two L_skirtA_02_jnt_Collider nodes) share one row,
        # exactly as the leaf-keyed BESEAM03 table did.
        for path in paths:
            bones.append((tree.add(path), xyz, pos))
    bones.sort(key=lambda b: b[0])
    report[report_key] = dict(state=entry['hash'], source_bones=len(entry['bones']), kept_rows=len(bones),
                              below_threshold=skipped_small, missing_in_capture=missing,
                              quantization_max_degrees=max(errors[0], default=0),
                              quantization_max_mm=1000 * max(errors[1], default=0))
    return bones


def accessory_side(entry, capture_paths, tree):
    names = [n for _, n in entry['bones']]
    nodes = []
    for name in names:
        paths = capture_paths.get(name, [])
        if len(paths) != 1:
            raise SystemExit(f'accessory bone {name}: expected one path, got {len(paths)}')
        nodes.append(tree.add(paths[0]))
    step = entry['data'].shape[0] // SAMPLES
    if step * SAMPLES != entry['data'].shape[0]:
        raise SystemExit('accessory table length must be a multiple of the compact sample count')
    rotations = entry['data'][::step, :, :4]  # phi = j/SAMPLES lands exactly on source samples.
    quantized = np.round(rotations * ROTATION_SCALE).astype(int)
    positions = entry['data'][..., 4:].mean(axis=0).astype(np.float32)
    return names, nodes, quantized, positions


def accessory_errors(entry, quantized, positions, capture, skinned):
    names = [n for _, n in entry['bones']]
    rot1024, pos1024 = entry['data'][..., :4], entry['data'][..., 4:]
    compact = normalize(quantized / ROTATION_SCALE)
    phase = np.arange(8192) / 8192.0
    reference = sample(rot1024, phase)
    count = rot1024.shape[0]
    frame = phase * count
    k = np.floor(frame).astype(int)
    f = (frame - k)[:, None, None]
    reference_pos = pos1024[k] + (pos1024[(k + 1) % count] - pos1024[k]) * f
    got = sample(compact, phase)
    got_pos = np.broadcast_to(positions.astype(np.float64), got.shape[:-1] + (3,))
    err = angle(got, reference)
    skin = [i for i, n in enumerate(names) if n in skinned]
    chains = {'tail': [names.index(f'tail_{i:02d}_jnt') for i in range(1, 11)],
              'L_hairE': [names.index(f'L_hairE_{i:02d}_jnt') for i in range(1, 11)],
              'R_hairE': [names.index(f'R_hairE_{i:02d}_jnt') for i in range(1, 11)]}
    tips = {key: float(100 * np.linalg.norm(chain_tip(got, got_pos, c) - chain_tip(reference, reference_pos, c), axis=-1).max())
            for key, c in chains.items()}
    result = dict(vs_1024=dict(max_degrees_skinned=float(err[:, skin].max()), max_degrees_all=float(err.max()),
                               max_local_position_mm=float(1000 * np.linalg.norm(got_pos - reference_pos, axis=-1).max()),
                               max_chain_end_cm=tips))
    if capture is not None:
        index = []
        for n in names:
            match = [i for i, (g, p) in enumerate(zip(capture['groups'], capture['paths'])) if g == 0 and leaf(p) == n]
            index.append(match[0])
        borrowed = {}
        for group in (0, 1):
            third, smooth = CUTS[entry['hash']][group]
            bones = [i for i, (g, _) in enumerate(entry['bones']) if g == group]
            u = capture['time']
            chosen = ((u >= BEGIN) & (u < third)) | ((u >= smooth) & (u < BEGIN))
            phi = np.where(u[chosen] >= BEGIN, (u[chosen] - BEGIN) / (BEGIN - TARGET), (u[chosen] - TARGET) / (BEGIN - TARGET))
            truth = normalize(capture['rotation'][chosen][:, index])
            truth_pos = capture['position'][chosen][:, index]
            row = {}
            for label, table in (('table_1024', rot1024), ('table_compact', compact)):
                values = sample(table, phi)
                angle_error = angle(values, truth)
                if label == 'table_1024':
                    frame = phi * count
                    k = np.floor(frame).astype(int)
                    f = (frame - k)[:, None, None]
                    pos = pos1024[k] + (pos1024[(k + 1) % count] - pos1024[k]) * f
                else:
                    pos = np.broadcast_to(positions.astype(np.float64), values.shape[:-1] + (3,))
                row[label] = dict(max_degrees_skinned=float(angle_error[:, [b for b in bones if b in skin]].max()),
                                  max_degrees_all=float(angle_error[:, bones].max()),
                                  max_local_position_mm=float(1000 * np.linalg.norm(pos - truth_pos, axis=-1)[:, bones].max()))
            borrowed[f'group{group}'] = dict(frames=int(chosen.sum()), **row)
        result['vs_capture_borrowed_segments'] = borrowed
    return result


def c_float(value):
    text = f'{value:.9g}'
    return (text if any(c in text for c in '.e') else text + '.0') + 'f'


def c_name(text):
    return '"' + text.replace('\\', '\\\\').replace('"', '\\"') + '"'


def emit(path, inputs, rider, mount, seams, accessory):
    lines = [
        '// Generated by tools/actions/jsspsi_quality/build_quality_data.py. Do not edit.',
        '// Inputs (SHA-256):',
        *[f'//   {key}: {digest}' for key, digest in inputs],
        '#pragma once',
        '#include "quality_policy.h"',
        '',
        'namespace BetterEndfieldNext::Actions::JsspsiQuality {',
    ]
    for label, tree in (('kRiderNodes', rider), ('kMountNodes', mount)):
        lines.append(f'inline constexpr QualityNode {label}[]{{')
        lines += [f'    {{{parent}, {c_name(name)}}},' for parent, name in tree.nodes]
        lines.append('};')
    for label, rows in seams:
        lines.append(f'inline constexpr QualitySeamBone {label}[]{{')
        lines += [f'    {{{node}, {{{xyz[0]}, {xyz[1]}, {xyz[2]}}}, {{{pos[0]}, {pos[1]}, {pos[2]}}}}},' for node, xyz, pos in rows]
        lines.append('};')
    nodes = accessory['nodes']
    lines.append(f'inline constexpr uint16_t kAccessoryNodes[]{{{", ".join(map(str, nodes))}}};')
    for side, (positions, rotations) in zip(('Right', 'Left'), accessory['sides']):
        lines.append(f'inline constexpr float kAccessoryPositions{side}[][3]{{')
        lines += ['    {' + ', '.join(c_float(v) for v in p) + '},' for p in positions]
        lines.append('};')
        lines.append(f'// {SAMPLES} samples x {len(nodes)} bones; sample j is phase j/{SAMPLES}.')
        lines.append(f'inline constexpr int16_t kAccessoryRotations{side}[][4]{{')
        for sample_rows in rotations:
            lines.append('    ' + ' '.join('{' + ','.join(map(str, q)) + '},' for q in sample_rows))
        lines.append('};')
    lines += [
        'inline constexpr LoopQualityData kData{',
        f'    {BEGIN}f, {TARGET}f,',
        '    {kRiderNodes, static_cast<uint16_t>(std::size(kRiderNodes))},',
        '    {kMountNodes, static_cast<uint16_t>(std::size(kMountNodes))},',
        f'    {{{{{RIGHT}, kRiderRightSeam, static_cast<uint16_t>(std::size(kRiderRightSeam))}},',
        f'     {{{LEFT}, kRiderLeftSeam, static_cast<uint16_t>(std::size(kRiderLeftSeam))}}}},',
        f'    {{{accessory["mount_hash"]}, kMountSeam, static_cast<uint16_t>(std::size(kMountSeam))}},',
        f'    kAccessoryNodes, static_cast<uint16_t>(std::size(kAccessoryNodes)), {SAMPLES},',
        f'    {{{{{RIGHT}, kAccessoryPositionsRight, kAccessoryRotationsRight}},',
        f'     {{{LEFT}, kAccessoryPositionsLeft, kAccessoryRotationsLeft}}}},',
        '};',
        '} // namespace BetterEndfieldNext::Actions::JsspsiQuality',
        '',
    ]
    Path(path).write_text('\n'.join(lines), encoding='ascii', newline='\n')


def digest(path):
    return hashlib.sha256(Path(path).read_bytes()).hexdigest()


def main():
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument('--seam', required=True)
    parser.add_argument('--accessory', required=True)
    parser.add_argument('--capture', required=True, help='BECAP001 whose group 0/1 paths name the Transforms')
    parser.add_argument('--compare-right', help='right capture used for the accessory error report')
    parser.add_argument('--compare-left', help='left capture used for the accessory error report')
    parser.add_argument('--catalog', required=True)
    parser.add_argument('--output', required=True)
    parser.add_argument('--report')
    args = parser.parse_args()

    seam = read_seam(args.seam)
    accessory = read_accessory(args.accessory)
    capture = read_capture(args.capture)
    catalog = json.loads(Path(args.catalog).read_text(encoding='utf-8'))
    skinned = {name for component in catalog['components'].values() for name in component['bone_names']}
    rider_paths, mount_paths = paths_by_leaf(capture, 0), paths_by_leaf(capture, 1)

    def window(entry, target, begin):
        return abs(entry['target'] - target) < 5e-4 and abs(entry['begin'] - begin) < 5e-4

    by_hash = {e['hash']: e for e in seam if window(e, TARGET, BEGIN)}
    mounts = [e for e in seam if window(e, *MOUNT_WINDOW) and e['hash'] not in (RIGHT, LEFT)]
    if RIGHT not in by_hash or LEFT not in by_hash or len(mounts) != 1:
        raise SystemExit('seam table needs both rider sides and exactly one mount entry on the rider window')
    report = dict(thresholds=dict(degrees=ANGLE_THRESHOLD, metres=POSITION_THRESHOLD))
    rider, mount = Tree(), Tree()
    right_rows = seam_entry(by_hash[RIGHT], rider_paths, rider, 'seam_rider_right', report)
    left_rows = seam_entry(by_hash[LEFT], rider_paths, rider, 'seam_rider_left', report)
    mount_rows = seam_entry(mounts[0], mount_paths, mount, 'seam_mount', report)

    sides = {e['hash']: e for e in accessory}
    if set(sides) != {RIGHT, LEFT} or any(abs(e['begin'] - BEGIN) > 1e-4 or abs(e['target'] - TARGET) > 1e-4 for e in accessory):
        raise SystemExit('accessory table must hold the right and left entries on the rider window')
    names_right, nodes, rot_right, pos_right = accessory_side(sides[RIGHT], rider_paths, rider)
    names_left, nodes_left, rot_left, pos_left = accessory_side(sides[LEFT], rider_paths, rider)
    if names_right != names_left or nodes != nodes_left:
        raise SystemExit('accessory sides must list the same bones in the same order')
    compare = {RIGHT: args.compare_right, LEFT: args.compare_left}
    report['accessory'] = dict(samples=SAMPLES, bones=len(nodes), sides={})
    for state, rot, pos in ((RIGHT, rot_right, pos_right), (LEFT, rot_left, pos_left)):
        cap = read_capture(compare[state]) if compare[state] else None
        report['accessory']['sides'][str(state)] = accessory_errors(sides[state], rot, pos, cap, skinned)

    inputs = [('seam', digest(args.seam)), ('accessory', digest(args.accessory)), ('capture', digest(args.capture))]
    emit(args.output, inputs, rider, mount,
         [('kRiderRightSeam', right_rows), ('kRiderLeftSeam', left_rows), ('kMountSeam', mount_rows)],
         dict(nodes=nodes, mount_hash=mounts[0]['hash'], sides=[(pos_right, rot_right), (pos_left, rot_left)]))
    node_bytes = sum(len(n) + 1 + 2 + 8 for tree in (rider, mount) for _, n in tree.nodes)
    seam_bytes = 14 * (len(right_rows) + len(left_rows) + len(mount_rows))
    accessory_bytes = 2 * (SAMPLES * len(nodes) * 8 + len(nodes) * 12) + 2 * len(nodes)
    report['bytes'] = dict(nodes_including_pointers=node_bytes, seam=seam_bytes, accessory=accessory_bytes,
                           total=node_bytes + seam_bytes + accessory_bytes,
                           header_text=Path(args.output).stat().st_size)
    report['nodes'] = dict(rider=len(rider.nodes), mount=len(mount.nodes))
    report['inputs'] = dict(inputs, catalog=digest(args.catalog))
    report['output_sha256'] = digest(args.output)
    Path(args.report or args.output + '.json').write_text(json.dumps(report, indent=2, ensure_ascii=False) + '\n', encoding='utf-8')
    json.dump(report, sys.stdout, indent=2, ensure_ascii=False)
    print()


if __name__ == '__main__':
    main()
