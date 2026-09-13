"""Build Liino's low glide from the validated 174-frame Unity bone bake.

Usage: python build_liino_glide.py --source <liino-fbx-validation> --output <dir>
Requires NumPy. Outputs BEPOSE12 data, full-skeleton preview samples and metadata.
Source frame numbers are zero based at 60 Hz. No game resources are modified.
"""
import argparse
import json
from pathlib import Path
import re
import shutil
import struct

import numpy as np

START, END, OVERLAP, ENTRY = 46, 66, 8, 56
SOURCE_FRAMES, SOURCE_INTERVALS, FPS, STRETCH = 174, 173, 60, 2


def normalize(q):
    return q / np.linalg.norm(q, axis=-1, keepdims=True)


def slerp(a, b, weight):
    b = np.where(np.sum(a * b, axis=-1, keepdims=True) < 0, -b, b)
    dot = np.clip(np.sum(a * b, axis=-1, keepdims=True), -1, 1)
    angle = np.arccos(dot)
    sine = np.maximum(np.sin(angle), 1e-12)
    spherical = (np.sin((1 - weight) * angle) * a + np.sin(weight * angle) * b) / sine
    return normalize(np.where(dot > .9995, a + (b - a) * weight, spherical))


def blend(a, b, weight):
    result = a + (b - a) * weight
    result[..., :4] = slerp(a[..., :4], b[..., :4], weight)
    return result


def close_and_slow(source):
    period = END - START
    loop = source[START:END + 1].copy()
    for j in range(-OVERLAP, OVERLAP + 1):
        u = (j + OVERLAP) / (2 * OVERLAP)
        weight = 1 - u ** 3 * (u * (6 * u - 15) + 10)
        loop[j % period] = blend(source[START + j], source[END + j], weight)
    loop[-1] = loop[0]
    # Half speed, still sampled at 60 Hz. Midpoints use quaternion slerp.
    result = np.empty((period * STRETCH + 1, *loop.shape[1:]))
    result[::STRETCH] = loop
    result[1::STRETCH] = blend(loop[:-1], loop[1:], .5)
    for i in range(1, len(result)):
        flip = np.sum(result[i - 1, :, :4] * result[i, :, :4], axis=-1) < 0
        result[i, flip, :4] *= -1
    entry = result[(ENTRY - START) * STRETCH]
    assert np.max(np.abs(entry[:, 4:] - source[ENTRY, :, 4:])) < 1e-7
    assert np.max(np.abs(np.abs(np.sum(entry[:, :4] * source[ENTRY, :, :4], axis=-1)) - 1)) < 1e-7
    return result.astype('<f4')


def world_positions(samples, bones):
    """Forward kinematics, including scale, in the model's Unity coordinates."""
    q = normalize(samples[:, :, :4]).astype(float)
    x, y, z, w = np.moveaxis(q, -1, 0)
    matrices = np.zeros((*q.shape[:2], 4, 4))
    matrices[:, :, :3, :3] = np.stack([
        1-2*(y*y+z*z), 2*(x*y-z*w), 2*(x*z+y*w),
        2*(x*y+z*w), 1-2*(x*x+z*z), 2*(y*z-x*w),
        2*(x*z-y*w), 2*(y*z+x*w), 1-2*(x*x+y*y),
    ], axis=-1).reshape(*q.shape[:2], 3, 3) * samples[:, :, None, 7:10]
    matrices[:, :, :3, 3] = samples[:, :, 4:7]
    matrices[:, :, 3, 3] = 1
    for i, bone in enumerate(bones):
        if bone['parent'] >= 0:
            assert bone['parent'] < i
            matrices[:, i] = matrices[:, bone['parent']] @ matrices[:, i]
    return matrices[:, :, :3, 3]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--source', type=Path, required=True)
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    src, out = args.source.resolve(), args.output.resolve()
    if src == out:
        raise ValueError('Output must differ from the validated source bake')
    out.mkdir(parents=True, exist_ok=True)
    data = json.loads((src / 'bake-input.json').read_text())
    bones = data['bones']
    human = {h['boneName'] for h in data['description']['human']}
    originals = {side: np.fromfile(src / f'{side}-baked.f32', dtype='<f4')
                 .reshape(SOURCE_FRAMES, len(bones), 10).astype(float)
                 for side in ('left', 'right')}
    for source in originals.values():
        assert np.isfinite(source).all()
        assert np.max(np.abs(np.linalg.norm(source[:, :, :4], axis=-1) - 1)) < 1e-3
        source[:, :, :4] = normalize(source[:, :, :4])
    full = np.concatenate(list(originals.values()))
    face = re.compile(r'^(lip|jaw|face|eye|brow|Tongue|Nose|line_jnt|head_(up|mid|dn)_jnt|Head_Lookat_Joints|Head_Local)', re.I)
    selected = []
    for i, bone in enumerate(bones):
        if not bone['path'] or face.match(bone['name']):
            continue
        q = full[:, i, :4]
        aligned = q * np.where(np.sum(q * q[0], axis=1) < 0, -1, 1)[:, None]
        mean = normalize(aligned.mean(axis=0))
        angle = np.degrees(2 * np.arccos(np.clip(np.abs(q @ mean), -1, 1))).max()
        position = np.linalg.norm(full[:, i, 4:7] - full[:, i, 4:7].mean(axis=0), axis=-1).max()
        if bone['name'] in human or angle >= .05 or position >= 5e-5:
            selected.append(i)
    required = sum(bones[i]['name'] in human for i in selected)
    assert len(selected) == 339 and required == 22, 'Unexpected Liino skeleton contract'
    period = (END - START) * STRETCH
    phase = (ENTRY - START) * STRETCH
    bank = bytearray(b'BEPOSE12' + struct.pack('<IIIfIff', 1, len(selected), period + 1,
                                            FPS, period, phase, ENTRY / SOURCE_INTERVALS))
    for i in selected:
        path = bones[i]['path'].encode()
        bank += struct.pack('<II', len(path), int(bones[i]['name'] in human)) + path
    report = {'source': str(src), 'sourceFrames': [START, END], 'sourceSupport': [START-OVERLAP, END+OVERLAP],
              'overlapFramesEachSide': OVERLAP, 'sourcePeriodFrames': END-START,
              'periodFrames': period, 'fps': FPS, 'periodSeconds': period/FPS, 'speed': 1/STRETCH,
              'entrySourceFrame': ENTRY, 'entryNormalized': ENTRY/SOURCE_INTERVALS, 'phase': phase,
              'bones': len(selected), 'required': required, 'sides': {},
              'method': 'Real-source quintic overlap, then half-speed slerp resampling. No height offset or root lock.'}
    index = {b['name']: i for i, b in enumerate(bones)}
    limits = {'Bip001': .045, 'Bip001_Head': .06, 'Bip001_L_Foot': .07, 'Bip001_R_Foot': .07}
    for side, source in originals.items():
        loop = close_and_slow(source)
        assert np.isfinite(loop).all() and np.min(loop[:, :, 7:]) > 0
        pose = loop[:, selected, :7].copy()
        assert np.max(np.abs(pose)) < 50
        assert np.max(np.abs(np.linalg.norm(pose[:, :, :4], axis=-1) - 1)) < 1e-6
        assert np.max(np.abs(pose[0, :, 4:] - pose[-1, :, 4:])) == 0
        assert np.max(np.abs(np.abs(np.sum(pose[0, :, :4] * pose[-1, :, :4], axis=-1)) - 1)) < 1e-6
        positions = world_positions(loop, bones)
        heights = {name: float(np.ptp(positions[:, index[name], 1])) for name in limits}
        assert all(heights[name] < limit for name, limit in limits.items()), heights
        bank += pose.astype('<f4').tobytes()
        loop.tofile(out / f'{side}-baked.f32')
        report['sides'][side] = {'heightRangeM': heights, 'entryMatchesSource': True,
                                'maxEndpointPositionGapM': float(np.max(np.linalg.norm(positions[0]-positions[-1], axis=-1)))}
    for name in ('bake-input.json', 'unity-bake-report.json', 'avatar.json', 'hierarchy.json'):
        shutil.copy2(src / name, out / name)
    (out / 'pose_liino.bin').write_bytes(bank)
    report['bytes'] = len(bank)
    (out / 'pose_liino.json').write_text(json.dumps(report, indent=2) + '\n')
    (out / 'loop-edit-report.json').write_text(json.dumps(report, indent=2) + '\n')
    print(json.dumps({'output': str(out), 'bytes': len(bank), 'periodSeconds': period/FPS,
                      'sides': report['sides']}, indent=2))


if __name__ == '__main__':
    main()
