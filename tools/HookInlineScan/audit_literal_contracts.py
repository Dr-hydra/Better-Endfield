"""Compare literal native method/field descriptors with targeted metadata.

This deliberately reports only literal descriptor initializers, not computed
descriptors or runtime behavior. Those require the module and hook audits.
"""
import argparse
import collections
import json
import re
from pathlib import Path

METHOD = re.compile(r'\{\s*"([^"\n]+\.dll)"\s*,\s*"([^"\n]*)"\s*,\s*"([^"\n]+)"\s*,\s*"([^"\n]+)"\s*,\s*(nullptr|"[^"\n]*")\s*,\s*(nullptr|"[^"\n]*")\s*,\s*(\d+)\s*\}', re.S)
FIELD = re.compile(r'\{\s*"([^"\n]+\.dll)"\s*,\s*"([^"\n]*)"\s*,\s*"([^"\n]+)"\s*,\s*"([^"\n]+)"\s*,\s*(nullptr|"[^"\n]*")\s*\}', re.S)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--metadata', required=True, type=Path)
    parser.add_argument('--repo', type=Path, default=Path(__file__).resolve().parents[2])
    parser.add_argument('--output', required=True, type=Path)
    args = parser.parse_args()
    data = json.loads(args.metadata.read_text(encoding='utf-8'))
    methods = collections.defaultdict(list)
    for method in data['methods']:
        methods[(method['asm'], method['cls'], method['name'], len(method['types']))].append(method)
    classes = {(cls['asm'], cls['cls']): cls for cls in data['classes']}
    results = []
    for base in ('native/modules', 'native/shared'):
        for path in (args.repo/base).rglob('*'):
            if path.suffix not in ('.cpp', '.h', '.hpp', '.inc'):
                continue
            source = path.read_text(encoding='utf-8', errors='replace')
            for kind, regex in (('method', METHOD), ('field', FIELD)):
                for match in regex.finditer(source):
                    asm, ns, cls, name, *rest = match.groups()
                    full = (ns+'.' if ns else '')+cls.replace('/', '.').replace('+', '.')
                    record = dict(kind=kind, source=f'{path.relative_to(args.repo).as_posix()}:{source.count(chr(10), 0, match.start())+1}', assembly=asm, cls=full, name=name)
                    if kind == 'method':
                        params, ret, count = rest
                        found = methods.get((asm, full, name, int(count)), [])
                        expected = params.strip('"').split('|') if params.strip('"') else []
                        exact = [m for m in found if (params == 'nullptr' or m['types'] == expected) and (ret == 'nullptr' or m['ret'] == ret.strip('"'))]
                        record['status'] = 'matched' if exact else 'signature-mismatch' if found else 'missing'
                        if exact:
                            record['rvas'] = [hex(m['rva']) for m in exact]
                        elif found:
                            record['candidates'] = [dict(types=m['types'], ret=m['ret']) for m in found]
                    else:
                        found = [f for f in classes.get((asm, full), {}).get('fields', []) if f['name'] == name]
                        record['status'] = 'matched' if found and (rest[0] == 'nullptr' or found[0]['type'] == rest[0].strip('"')) else 'field-review'
                        if found:
                            record['offset'] = found[0]['offset']
                    results.append(record)
    summary = dict(collections.Counter(f"{r['kind']}:{r['status']}" for r in results))
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(dict(scope=__doc__, summary=summary, results=results), ensure_ascii=False, indent=2)+'\n', encoding='utf-8')
    print(json.dumps(dict(summary=summary, review=[r for r in results if r['status'] != 'matched']), ensure_ascii=False))


if __name__ == '__main__':
    main()
