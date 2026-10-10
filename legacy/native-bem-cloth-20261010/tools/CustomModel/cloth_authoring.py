"""Experimental cloth authoring data, not a supported BEM wire-format version.

This validates proxy topology and display correspondence before native solver
work. It deliberately cannot mark a project or package render/physics verified.
"""
from __future__ import annotations
import argparse
import json
import math
from pathlib import Path


def need(condition, message):
    if not condition:
        raise ValueError(message)


def index(value, count, message):
    need(type(value) is int and 0 <= value < count, message)


def validate(project):
    need(project.get('schema') == 1 and project.get('kind') == 'bem-cloth-authoring-prototype',
         'Not a cloth authoring prototype')
    target = project['target']
    need(target['character_id'] and target['resource'] and target['mesh'], 'Missing target identity')
    need(type(target['vertex_count']) is int and 0 < target['vertex_count'] <= 1048576,
         'Invalid display vertex count')
    digest = target['geometry_sha256']
    need(isinstance(digest, str) and len(digest) == 64 and all(c in '0123456789abcdef' for c in digest),
         'Missing exact display geometry identity')
    layers = project['layers']
    need(1 <= len(layers) <= 8, 'Expected 1..8 cloth layers')
    ids = set()
    bound = set()
    total_points = total_faces = 0
    summaries = []
    for layer in layers:
        name = layer['id']
        need(isinstance(name, str) and name and name not in ids, 'Duplicate or empty layer ID')
        ids.add(name)
        positions = layer['positions']
        triangles = layer['triangles']
        need(3 <= len(positions) <= 4096, 'Proxy point budget exceeded')
        need(1 <= len(triangles) <= 8192, 'Proxy triangle budget exceeded')
        for p in positions:
            need(isinstance(p, list) and len(p) == 3 and
                 all(type(v) in (int, float) and math.isfinite(v) and abs(v) <= 10 for v in p),
                 'Invalid mesh-local proxy position')
        unique = set()
        adjacent = [set() for _ in positions]
        used = set()
        for triangle in triangles:
            need(isinstance(triangle, list) and len(triangle) == 3, 'Expected triangular proxy')
            for v in triangle:
                index(v, len(positions), 'Proxy triangle index out of bounds')
            need(len(set(triangle)) == 3, 'Collapsed proxy triangle')
            key = tuple(sorted(triangle))
            need(key not in unique, 'Duplicate proxy triangle')
            unique.add(key)
            a, b, c = [positions[i] for i in triangle]
            u = [b[i]-a[i] for i in range(3)]
            v = [c[i]-a[i] for i in range(3)]
            cross = [u[1]*v[2]-u[2]*v[1], u[2]*v[0]-u[0]*v[2], u[0]*v[1]-u[1]*v[0]]
            need(sum(x*x for x in cross) > 1e-16, 'Degenerate proxy triangle')
            used.update(triangle)
            for i in triangle:
                adjacent[i].update(j for j in triangle if i != j)
        need(len(used) == len(positions), 'Unreferenced proxy points')
        fixed = layer['fixed_vertices']
        need(fixed and len(fixed) == len(set(fixed)), 'Fixed vertices missing or duplicated')
        for v in fixed:
            index(v, len(positions), 'Fixed vertex index out of bounds')
        need(len(fixed) < len(positions), 'All proxy points fixed')
        visited = set()
        components = 0
        for start in range(len(positions)):
            if start in visited:
                continue
            components += 1
            todo, component = [start], set()
            while todo:
                current = todo.pop()
                if current in component:
                    continue
                component.add(current)
                todo.extend(adjacent[current]-component)
            need(component.intersection(fixed), 'Unanchored disconnected cloth panel')
            visited.update(component)
        binding = layer['display_bindings']
        need(binding, 'Missing display bindings')
        for row in binding:
            index(row['vertex'], target['vertex_count'], 'Display binding index out of bounds')
            if 'proxy_vertex' in row:
                need('triangle' not in row, 'Ambiguous display binding')
                index(row['proxy_vertex'], len(positions), 'Display proxy index out of bounds')
            else:
                index(row['triangle'], len(triangles), 'Display triangle index out of bounds')
                bary = row['barycentric']
                offset = row['rest_offset_tbn']
                need(len(bary) == 3 and all(type(v) in (int, float) and math.isfinite(v) and 0 <= v <= 1 for v in bary)
                     and abs(sum(bary)-1) < 1e-5, 'Invalid surface binding weights')
                need(len(offset) == 3 and all(type(v) in (int, float) and math.isfinite(v) for v in offset)
                     and sum(v*v for v in offset) <= .1**2, 'Invalid surface binding rest offset')
            need(row['vertex'] not in bound, 'Display vertex has multiple cloth writers')
            bound.add(row['vertex'])
        need(layer['anchor_bone'] == 'Bip001_Pelvis', 'Prototype supports only pelvis anchors')
        total_points += len(positions)
        total_faces += len(triangles)
        summaries.append({'id': name, 'points': len(positions), 'triangles': len(triangles),
                          'panels': components, 'fixed_points': len(fixed), 'display_vertices': len(binding),
                          'surface_bindings': sum('triangle' in b for b in binding)})
    need(total_points <= 8192 and total_faces <= 16384, 'Total proxy budget exceeded')
    pairs = set()
    for contact in project['contacts']:
        a, b = contact['layers']
        need(a in ids and b in ids and a != b, 'Invalid layer collision pair')
        pair = tuple(sorted((a, b)))
        need(pair not in pairs, 'Duplicate layer collision pair')
        pairs.add(pair)
        need(contact['kind'] == 'surface-surface', 'Unsupported contact description')
    return {'success': True, 'layers': summaries, 'collision_pairs': len(pairs),
            'wire_format_supported': False, 'native_solver_verified': False,
            'scope': 'Authoring topology and correspondence only; not a BEM physics execution check.'}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('project', type=Path)
    parser.add_argument('--report', type=Path)
    args = parser.parse_args()
    result = validate(json.loads(args.project.read_text(encoding='utf-8-sig')))
    text = json.dumps(result, ensure_ascii=False, indent=2)
    if args.report:
        args.report.write_text(text, encoding='utf-8')
    print(text)


if __name__ == '__main__':
    main()
