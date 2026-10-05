"""Derive compact gaze references from local PMX iris surfaces and eye openings.

Run after compile_character_faces.py. Only matching source hashes are updated;
meshes, textures and local source paths are never stored in the output profiles.
Limits are conservative neutral-face estimates, not authored PMX constraints.
"""
import argparse
import json
import math
import re
from pathlib import Path

import numpy as np
from compile_character_faces import read_pmx, game_bone


def unit(v):
    n = np.linalg.norm(v)
    if not np.isfinite(n) or n < 1e-8:
        raise ValueError('Degenerate eye reference')
    return v / n


def material_faces(model, name):
    parts = [model['faces'][m['start']:m['start']+m['count']]
             for m in model['materials'] if m['name'] == name or
             (name == '目' and re.fullmatch(r'目[12]', m['name']))]
    return np.concatenate(parts) if parts else np.empty((0, 3), dtype=np.int32)


def boundaries(model):
    # Weld split material/UV vertices only for this offline boundary query.
    points, remap = np.unique(np.round(model['vertices'], 5), axis=0, return_inverse=True)
    faces = remap[material_faces(model, '面')]
    if not len(faces):
        return []
    edges = np.sort(np.concatenate([faces[:, [0, 1]], faces[:, [1, 2]], faces[:, [2, 0]]]), axis=1)
    edges, counts = np.unique(edges, axis=0, return_counts=True)
    adjacency = {}
    for a, b in edges[counts == 1]:
        adjacency.setdefault(int(a), set()).add(int(b))
        adjacency.setdefault(int(b), set()).add(int(a))
    seen, loops = set(), []
    for start in adjacency:
        if start in seen:
            continue
        stack, component = [start], []
        while stack:
            node = stack.pop()
            if node in seen:
                continue
            seen.add(node)
            component.append(node)
            stack.extend(adjacency[node]-seen)
        if len(component) < 8 or any(len(adjacency[n]) != 2 for n in component):
            continue
        ordered, previous, current = [], None, start
        while current not in ordered:
            ordered.append(current)
            nxt = next(n for n in sorted(adjacency[current]) if n != previous)
            previous, current = current, nxt
        if current == start and len(ordered) == len(component):
            loops.append(points[ordered])
    return loops


def clearance(point, polygon):
    # Signed distance to an ordered, possibly concave aperture in the iris plane.
    a, b = polygon, np.roll(polygon, -1, axis=0)
    edge = b-a
    t = np.clip(np.sum((point-a)*edge, axis=1)/np.maximum(np.sum(edge*edge, axis=1), 1e-16), 0, 1)
    distance = float(np.min(np.linalg.norm(point-a-edge*t[:, None], axis=1)))
    inside = False
    for p, q in zip(a, b):
        if (p[1] > point[1]) != (q[1] > point[1]):
            x = p[0]+(point[1]-p[1])*(q[0]-p[0])/(q[1]-p[1])
            if point[0] < x:
                inside = not inside
    return distance if inside else -distance


def rotation_from_to(a, b):
    a, b = unit(a), unit(b)
    v, c = np.cross(a, b), np.dot(a, b)
    if c < -.99:
        raise ValueError('Opposite gaze reference')
    k = np.array([[0, -v[2], v[1]], [v[2], 0, -v[0]], [-v[1], v[0], 0]])
    return np.eye(3)+k+(k@k)/(1+c)


def derive(model):
    bones = {}
    # Prefer native pivots over extra MMD controls named 左目/右目.
    for b in sorted(model['bones'], key=lambda b: b['name'].lower().startswith('eye')):
        bones[game_bone(b['name']).lower()] = np.array(b['rest'])
    triangles = material_faces(model, '目')
    loops = boundaries(model)
    sides = []
    for side in ('lf', 'rt'):
        pivot = bones.get('eye'+side+'joint')
        if pivot is None:
            raise ValueError('Missing eye pivot')
        ids = np.unique(triangles)
        shape = model['vertices'][ids]
        shape = shape[shape[:, 0]*pivot[0] > 0]
        if len(shape) < 12:
            raise ValueError('No isolated iris surface')
        mean = shape.mean(0)
        _, spread, axes = np.linalg.svd(shape-mean, full_matrices=False)
        normal = axes[-1]
        if normal[2] > 0:
            normal = -normal
        if spread[-1]/spread[1] > .3 or normal[2] > -.7:
            raise ValueError('Ambiguous iris surface direction')
        plane = axes[:2]
        points2 = (shape-mean)@plane.T
        center = mean+((points2.max(0)+points2.min(0))*.5)@plane
        radius = float(np.min(np.ptp(points2, axis=0))*.5)
        candidates = []
        for loop in loops:
            polygon = (loop-center)@plane.T
            size = np.ptp(polygon, axis=0)
            if not (np.all(size > radius*.8) and np.all(size < radius*6)):
                continue
            distance = clearance(np.zeros(2), polygon)
            if distance > 0 and np.linalg.norm(loop.mean(0)-center) < radius:
                candidates.append((np.linalg.norm(loop.mean(0)-center), polygon, distance))
        candidates.sort(key=lambda x: x[0])
        aperture, neutral_clearance = None, 0
        if len(candidates) == 1:
            _, aperture, neutral_clearance = candidates[0]
        sides.append(dict(pivot=pivot, center=center, normal=normal, plane=plane, aperture=aperture,
                          margin=min(radius*.18, neutral_clearance*.5), radius=radius))
    forward = unit(sides[0]['normal']+sides[1]['normal'])
    # Keep the native left/right neutral spacing, rather than independently
    # steering each stylized iris toward a different surface normal.
    right = unit(bones['eyertjoint']-bones['eyelfjoint'])
    forward = unit(forward-right*np.dot(forward, right))
    up = unit(np.cross(forward, right))
    if up[1] < 0:
        up = -up
    result = {'version': 1, 'method': 'pmx-iris-aperture', 'estimated': False,
              'forward': np.round(forward, 7).tolist(),
              'limits': {'left': 20, 'right': 20, 'up': 10, 'down': 15}}
    if any(s['aperture'] is None for s in sides):
        result['fallback'] = 'No unambiguous eye opening'
        return result

    def safe(yaw_degrees, pitch_degrees):
        yaw, pitch = np.radians([yaw_degrees, pitch_degrees])
        target = forward*(math.cos(pitch)*math.cos(yaw))+right*(math.cos(pitch)*math.sin(yaw))+up*math.sin(pitch)
        rotation = rotation_from_to(forward, target)
        return all(clearance(((s['pivot']+rotation@(s['center']-s['pivot']))-s['center'])@s['plane'].T,
                             s['aperture']) >= s['margin'] for s in sides)

    limits = {}
    for direction, yaw_sign, pitch_sign, maximum in [('left', -1, 0, 25), ('right', 1, 0, 25),
                                                     ('up', 0, 1, 15), ('down', 0, -1, 18)]:
        angle = 0
        # Include both eyes; stop at the FIRST boundary crossing, not a later
        # re-entry. A further 10% margin absorbs the discrete scan resolution.
        for degrees in np.arange(.25, maximum+.01, .25):
            if not safe(degrees*yaw_sign, degrees*pitch_sign):
                break
            angle = float(degrees)
        if angle < 2:
            result['fallback'] = 'Insufficient neutral eye clearance'
            return result
        limits[direction] = round(angle*.9, 2)
    # Apertures need not be elliptical. Check the combined runtime envelope,
    # shrinking if a diagonal encounters a slanted eyelid/corner earlier.
    factor = 1.
    while factor > .3:
        passed = True
        for theta in np.linspace(0, math.tau, 73):
            x, y = math.cos(theta), math.sin(theta)
            for radius in (.25, .5, .75, 1):
                if not safe(x*limits['right' if x >= 0 else 'left']*factor*radius,
                            y*limits['up' if y >= 0 else 'down']*factor*radius):
                    passed = False
                    break
            if not passed:
                break
        if passed:
            result.update(estimated=True, limits={k: math.floor(v*factor*100)/100 for k, v in limits.items()})
            return result
        factor *= .9
    result['fallback'] = 'Irregular eye opening'
    return result


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('models', type=Path)
    parser.add_argument('--profiles', type=Path, default=Path('resources/character-faces'))
    parser.add_argument('--report', type=Path)
    args = parser.parse_args()
    profiles = {}
    for path in args.profiles.glob('*.face.json'):
        data = json.loads(path.read_text(encoding='utf-8-sig'))
        profiles[data['source_hash']] = (path, data)
    report = []
    for path in sorted(args.models.rglob('*.pmx')):
        model = read_pmx(path)
        match = profiles.get(model['hash'])
        if not match:
            continue
        dest, profile = match
        row = {'model': profile['model'], 'label': profile['label'], 'source_hash': model['hash']}
        try:
            profile['gaze'] = derive(model)
            row.update(profile['gaze'])
        except ValueError as e:
            profile.pop('gaze', None)
            row['fallback'] = str(e)
        # Preserve the compact format of existing reusable face profiles.
        dest.write_text(json.dumps(profile, ensure_ascii=False, separators=(',', ':'))+'\n', encoding='utf-8')
        report.append(row)
        print(json.dumps(row, ensure_ascii=False), flush=True)
    if args.report:
        args.report.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')


if __name__ == '__main__':
    main()
