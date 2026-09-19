"""Arm one bounded native probe, or validate its JSONL against the offline graph."""
import argparse
import datetime
import json
import os
import re
from pathlib import Path

from parse_native_models import apply_observations


def read(path):
    return json.loads(path.read_text(encoding='utf-8-sig'))


def write(path, value):
    path.parent.mkdir(parents=True, exist_ok=True)
    temp = path.with_suffix(path.suffix + '.tmp')
    temp.write_text(json.dumps(value, ensure_ascii=False, indent=2, allow_nan=False), encoding='utf-8')
    temp.replace(path)


def request(database, preparation, run):
    if not re.fullmatch(r'[A-Za-z0-9_-]{1,80}', run):
        raise ValueError('invalid run ID')
    snapshot = database['source']['snapshot']
    if not snapshot or not snapshot.get('manifest_version'):
        raise ValueError('current extraction snapshot required')
    if preparation['source_snapshot'] != snapshot:
        raise ValueError('preparation snapshot differs from database')
    ids = [component[field] for component in preparation['components'] for field in ('world_renderer', 'ui_renderer')]
    rows = [r for r in database['renderers'] if r['id'] in ids]
    if len(rows) != len(ids) or len(set(ids)) != len(ids) or not 0 < len(rows) <= 16:
        raise ValueError('missing, duplicate or excessive probe targets')
    if len({r['resource_root'] for r in rows}) > 2:
        raise ValueError('probe supports at most two resource roots')
    lines = ['BE_NATIVE_PROBE_V1', run, snapshot['manifest_version'], snapshot.get('perforce_cl', '')]
    for row in rows:
        if not row['offline_references_complete']:
            raise ValueError('offline references incomplete')
        values = [row['resource_root'], row['path'], row['mesh_name'], str(row['original_index_count']), row['id']]
        if any(not v or any(c in v for c in '\t\r\n') for v in values):
            raise ValueError('invalid request identity')
        lines.append('\t'.join(values))
    return '\n'.join(lines)+'\n', {'schema': 1, 'run': run, 'manifest_version': snapshot['manifest_version'],
                                   'perforce_cl': snapshot.get('perforce_cl', ''), 'expected_ids': ids}


def collect(database, metadata, records):
    expected = set(metadata['expected_ids'])
    rows, processes = {}, set()
    for record in records:
        for field in ('run', 'manifest_version', 'perforce_cl'):
            if record.get(field) != metadata[field]:
                raise ValueError(f'probe provenance mismatch: {field}')
        processes.add(record['process_id'])
        if not record.get('complete'): continue
        for row in record['renderers']:
            identity = row['id']
            if identity not in expected or identity in rows or not row.get('complete'):
                raise ValueError('unexpected/duplicate/incomplete renderer observation')
            if row['resource_root'] != record['resource_root']:
                raise ValueError('resource record identity mismatch')
            offline = next(r for r in database['renderers'] if r['id'] == identity)
            if row['bones'] != [{'name': b['name'], 'path': b['path']} for b in offline['bones']]:
                raise ValueError(f"native bone identity/order changed: {row['mesh_name']}")
            if [m['name'] for m in row['materials']] != [m['name'] for m in offline['materials']]:
                raise ValueError('native material slot identity/order changed')
            for material in row['materials']:
                if not material.get('shader'):
                    raise ValueError('native shader identity unavailable')
            rows[identity] = row
    if len(processes) != 1:
        raise ValueError('observations must come from one game process')
    if set(rows) != expected:
        raise ValueError(f'capture incomplete: {len(rows)}/{len(expected)} renderer observations')
    result = {'schema': 1, 'evidence': f"native resource-delivery probe {metadata['run']}; process {next(iter(processes))}",
              'manifest_version': metadata['manifest_version'], 'perforce_cl': metadata['perforce_cl'],
              'renderers': list(rows.values())}
    apply_observations(database, result)  # Includes snapshot, identities and descriptor/stride validation.
    return result


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('action', choices=('arm', 'collect'))
    parser.add_argument('--database', type=Path, required=True)
    parser.add_argument('--preparation', type=Path)
    parser.add_argument('--catalog', type=Path, default=Path(os.environ.get('LOCALAPPDATA', '.'))/'BetterEndfield/catalog/custom-model')
    parser.add_argument('--run')
    parser.add_argument('--output', type=Path)
    args = parser.parse_args()
    database = read(args.database)
    try:
        if args.action == 'arm':
            if not args.preparation: parser.error('--preparation required when arming')
            run = args.run or 'zhuangfy-'+datetime.datetime.now().strftime('%Y%m%d-%H%M%S-%f')
            text, metadata = request(database, read(args.preparation), run)
            folder = args.catalog/'native-probe'
            if (folder/(run+'.jsonl')).exists() or (folder/(run+'.request.json')).exists():
                raise ValueError('run ID already used; choose a fresh ID')
            write(folder/(run+'.request.json'), metadata)
            path = args.catalog/'native-probe.request'
            temp = path.with_suffix('.tmp'); temp.write_text(text, encoding='utf-8'); temp.replace(path)
            write(args.catalog/'native-probe.status.json', {'schema': 1, 'run': run,
                  'stage': 'awaiting_game', 'process_id': 0, 'expected_renderers': len(metadata['expected_ids'])})
            print(f'Armed {run}: {len(metadata["expected_ids"])} renderers; output {folder/(run+".jsonl")}')
        else:
            if not args.run or not args.output: parser.error('--run and --output required when collecting')
            if not re.fullmatch(r'[A-Za-z0-9_-]{1,80}', args.run): raise ValueError('invalid run ID')
            folder = args.catalog/'native-probe'
            metadata = read(folder/(args.run+'.request.json'))
            records = [json.loads(line) for line in (folder/(args.run+'.jsonl')).read_text(encoding='utf-8').splitlines() if line.strip()]
            observations = collect(database, metadata, records)
            write(args.output, observations)
            print(f'Validated {len(observations["renderers"])} renderer observations: {args.output}')
    except (ValueError, KeyError, StopIteration, OSError) as error:
        print(f'Native probe: {error}'); return 2
    return 0


if __name__ == '__main__':
    raise SystemExit(main())
