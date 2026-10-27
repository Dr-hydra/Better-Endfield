#!/usr/bin/env python3
"""Agent entry point for the native BEM composer. Python standard library only.

plan/build/draft/preview forward to CustomModelCore; summary reads plan metadata.
No source rewrites, automatic installation, artifact hashing, or Python format builder.
Set BEM_COMPOSER to an explicit BetterEndfieldNext.BemCompose.exe when needed.
"""
from __future__ import annotations

import argparse
import json
import os
from pathlib import Path
import subprocess
import sys
import uuid


def engine() -> Path:
    override = os.environ.get('BEM_COMPOSER')
    if override:
        path = Path(override).expanduser()
        if not path.is_file():
            raise ValueError(f'BEM_COMPOSER does not name a file: {path}')
        return path.resolve()
    executable = 'BetterEndfieldNext.BemCompose.exe'
    for root in Path(__file__).resolve().parents:
        for relative in (
            Path('BemCompose') / executable,
            Path('tools/BemCompose') / executable,
            Path('build/bem-compose/stage/Release/tools/BemCompose') / executable,
            Path('build/next/windows/win-x64/Release/publish/tools/BemCompose') / executable,
        ):
            path = root / relative
            if path.is_file():
                return path
    raise ValueError('Native composer missing. Set BEM_COMPOSER to the player application '
                     'tools/BemCompose/BetterEndfieldNext.BemCompose.exe, or build that target in the repository.')


def summary(path: Path) -> dict:
    plan = json.loads(path.read_text(encoding='utf-8-sig'))
    result = dict(platform=plan['platform'], version=plan.get('package_version'),
                  bem_minor=plan['bem_minor'], slots=[], atomic_items=plan.get('atomic_items', []))
    for slot in plan['slots']:
        bundles = {}
        for segment in slot.get('segments', []):
            bindings = segment.get('option_bindings', [])
            key = (segment['source_id'], segment.get('body_region', ''),
                   json.dumps(bindings, sort_keys=True, ensure_ascii=False))
            bundle = bundles.setdefault(key, dict(source=segment['source_id'],
                source_name=segment['source_name'], region=segment.get('body_region'),
                options=[binding['name'] for binding in bindings], segment_ids=[],
                triangles=0, body_draws=[]))
            bundle['segment_ids'].append(segment['id'])
            bundle['triangles'] += segment['count'] // 3
            if segment.get('body_draw') and segment['body_draw'] not in bundle['body_draws']:
                bundle['body_draws'].append(segment['body_draw'])
        result['slots'].append(dict(id=slot['id'], name=slot['display_name'],
            default=slot['default'], candidates=slot['candidates'], bundles=list(bundles.values())))
    result['note'] = 'Counts describe automatic plan partitions, before body_edits; zero triangles means unavailable.'
    return result


def main(argv: list[str] | None = None) -> int:
    args = list(sys.argv[1:] if argv is None else argv)
    if not args or args[0] in ('-h', '--help'):
        print('BEM composition: plan | summary PLAN.json | draft | build | preview\n'
              'plan --source A.bem --source B.bem --platform windows-x64|android-arm64 '
              '--name NAME --json NEW_PLAN.json\n'
              'draft/build --plan PLAN.json [--choices CHOICES.json] --output-dir NEW_DIRECTORY\n'
              'preview --package PACKAGE.bem --output-dir NEW_DIRECTORY [--gpu JSON]\n'
              'Native planning derives the package version from sources. '
              'See references/BEM_COMPOSITION.md for choices and body edits.')
        return 0
    try:
        if args[0] == 'summary':
            parser = argparse.ArgumentParser(description='Compact plan inventory; no payload decoding.')
            parser.add_argument('plan', type=Path)
            options = parser.parse_args(args[1:])
            print(json.dumps(dict(success=True, result=summary(options.plan)), ensure_ascii=False, indent=2))
            return 0
        if args[0] not in ('plan', 'draft', 'build', 'preview'):
            raise ValueError('Use plan, summary, draft, build or preview. --help lists the entry points.')
        if args[0] == 'plan' and '--request' not in args:
            for key, value in (('--project-id', 'compose.' + str(uuid.uuid4())),
                               ('--name', 'BEM composition'), ('--author', 'BEM Composer'),
                               ('--version', 'auto')):
                if key not in args:
                    args.extend((key, value))
        completed = subprocess.run([str(engine()), *args], capture_output=True, encoding='utf-8')
        if completed.stderr:
            print(completed.stderr, file=sys.stderr, end='')
        if completed.returncode == 0 and args[0] == 'plan' and '--json' in args:
            body = json.loads(completed.stdout)['result']
            print(json.dumps(dict(success=True, plan=str(Path(args[args.index('--json')+1]).resolve()),
                platform=body['platform'], version=body.get('package_version'),
                slots=len(body['slots']), atomic_items=len(body['atomic_items'])), ensure_ascii=False))
        else:
            print(completed.stdout, end='')
        return completed.returncode
    except (ValueError, OSError, KeyError, IndexError) as error:
        print(json.dumps(dict(success=False, error=str(error)), ensure_ascii=False))
        return 2


if __name__ == '__main__':
    raise SystemExit(main())
