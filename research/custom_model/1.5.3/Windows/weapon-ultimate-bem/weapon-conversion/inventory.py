"""Read local weapon candidates and saved EFMI identities; never mutate inputs."""
from __future__ import annotations

import collections
import json
from pathlib import Path
import re
import struct


HERE = Path(__file__).resolve().parent
SOURCE = Path('G:/zmd')
BEM_ROOT = Path('G:/zmd_bem')
WEAPON = re.compile(r'weapon|sword|funnel|claym|wpn_|武器|剑鞘|武器壳|武器剑', re.I)


def read_text(path):
    raw = path.read_bytes()
    for encoding in ('utf-8-sig', 'utf-16', 'gb18030'):
        try:
            return raw.decode(encoding)
        except UnicodeError:
            pass
    return raw.decode('utf-8', errors='replace')


def sections(text):
    current, rows = None, []
    for number, line in enumerate(text.splitlines(), 1):
        section = re.fullmatch(r'\s*\[([^]]+)\]\s*', line)
        if section:
            current = dict(name=section[1], line=number, assignments=[])
            rows.append(current)
        elif current:
            assignment = re.match(r'\s*([A-Za-z0-9_\-]+)\s*=\s*(.*?)\s*$', line)
            if assignment:
                current['assignments'].append([assignment[1].lower(), assignment[2]])
    return rows


def run():
    identity = json.loads((BEM_ROOT / 'research/identities/native-mesh-hash-index.json').read_text(encoding='utf-8-sig'))
    inis, candidates, all_hashes = list(SOURCE.rglob('*.ini')), [], collections.defaultdict(list)
    for ini in inis:
        parsed = sections(read_text(ini))
        by_name = {row['name'].lower(): row for row in parsed}
        for row in parsed:
            values = dict(row['assignments'])
            token = values.get('hash', '').lower()
            if not re.fullmatch('[0-9a-f]{8}', token):
                continue
            all_hashes[token].append(dict(ini=str(ini), section=row['name']))
            known = identity.get(token, [])
            if not (WEAPON.search(str(ini)) or WEAPON.search(row['name']) or any('S_wpn_' in str(match) for match in known)):
                continue
            bindings = {key: value for key, value in row['assignments'] if re.fullmatch(r'vb\d|ib|ps-t\d+', key)}
            resources = {}
            for name in bindings.values():
                resource = by_name.get(name.lower())
                if resource:
                    fields = dict(resource['assignments'])
                    if 'filename' in fields:
                        file = ini.parent / fields['filename'].replace('\\', '/')
                        resources[name] = dict(fields, resolved=str(file), exists=file.is_file(), bytes=file.stat().st_size if file.is_file() else None)
            candidates.append(dict(ini=str(ini), section=row['name'], line=row['line'], source_hash=token,
                                   native_index_matches=known, bindings=bindings, resources=resources,
                                   draws=[value for key, value in row['assignments'] if key == 'drawindexed']))
    packages, attached_weapons, failures = [], [], []
    for path in (BEM_ROOT / '成品').rglob('*.bem'):
        try:
            with path.open('rb') as stream:
                header = stream.read(40)
                if header[:8] != b'BEM\0PKG\0':
                    raise ValueError('wrong BEM header')
                minor = struct.unpack_from('<H', header, 10)[0]
                length = struct.unpack_from('<Q', header, 24)[0]
                if length > 4 * 1024 * 1024:
                    raise ValueError('oversized manifest')
                manifest = json.loads(stream.read(length))
            target = manifest['target']
            item = dict(path=str(path), package_id=manifest['package_id'], minor=minor,
                        target_id=target.get('id', target.get('character_id')), target_kind=target.get('kind', 'character'))
            packages.append(item)
            names = [c.get('mesh_name', '') for c in target['components'] if WEAPON.search(c.get('mesh_name', ''))]
            if names:
                attached_weapons.append(dict(item, meshes=names))
        except Exception as error:
            failures.append(dict(path=str(path), error=str(error)))
    report = dict(schema=1, source=str(SOURCE), ini_count=len(inis), unique_source_hashes=len(all_hashes),
                  candidates=candidates, source_hash_references=dict(all_hashes),
                  finished_bem_count=len(packages), standalone_weapon_bems=[p for p in packages if p['target_kind'] == 'weapon'],
                  attached_weapon_bems=attached_weapons, bem_read_errors=failures,
                  platform_evidence='Windows EFMI source and archived Windows catalogs only; no Android donor inferred.',
                  artifact_hashes_computed=False)
    output = HERE / 'source-inventory.json'
    output.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps(dict(report=str(output), ini_count=len(inis), candidates=len(candidates),
                          weapon_candidate_inis=len({c['ini'] for c in candidates}), bem_count=len(packages),
                          standalone_weapon_bems=len(report['standalone_weapon_bems']), attached_weapon_bems=len(attached_weapons),
                          known_matches=sum(bool(c['native_index_matches']) for c in candidates)), ensure_ascii=False))


if __name__ == '__main__':
    run()
