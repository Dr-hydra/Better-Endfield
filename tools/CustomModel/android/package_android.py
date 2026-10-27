"""Assemble the Android (arm64) slim CLI distribution of BEM Tools.

The output is a tarball that an agent unpacks on the device, nothing more:

    BEM-Tools-<version>-android-arm64/
    ├── bem-tools      POSIX sh entry point
    ├── install.sh     deploy + dependency check
    ├── README.md
    └── app/
        ├── *.py       runtime modules, source_archive.py excluded
        ├── bem_android.py
        └── catalog/ profiles/ skills/ examples/

Two deliberate omissions keep this build honest about what it can do:

- `source_archive.py` is excluded even though `convert_efmi_poc` references it, so
  RAR/7z sources fail with ARCHIVE_BACKEND instead of a missing executable.
- Only modules reachable from `bem_tool` are shipped; repository maintenance
  scripts (catalog importers, offline sweeps, native probes) stay out.

Layout matters: `component_auto.catalogs()` resolves `catalog/` next to the module
that imports it, so the data directories must sit beside the .py files exactly as
they do in the Windows dist directory.
"""
from __future__ import annotations

import argparse
import ast
import hashlib
import json
import os
import shutil
import subprocess
import sys
import tarfile
import tempfile
import time
from pathlib import Path

IGNORE = shutil.ignore_patterns('__pycache__', '*.pyc', '*.pyo')
DATA_DIRS = ('catalog', 'profiles', 'skills', 'examples')
ENTRY_FILES = ('bem-tools', 'install.sh', 'README.md')
ENTRY_MODULES = ('bem_tool', 'bem_android')
# convert_efmi_poc imports this inside a try block on purpose: the slim build must
# reach the ARCHIVE_BACKEND refusal rather than ship a Windows-only 7-Zip backend.
EXCLUDE_MODULES = {'source_archive'}
DIST_TEMPLATE = 'BEM-Tools-{version}-android-arm64'
PYTHON_MIN = (3, 10)


def reachable_modules(source, entries=ENTRY_MODULES):
    """Follow `import` statements from the entry points to the local modules they need.

    Repository maintenance scripts (catalog importers, offline sweeps, native
    probes) are never reachable from the CLI, so a whitelist keeps them out
    instead of shipping a hand-maintained exclusion list that rots.
    """
    seen = set()
    queue = list(entries)
    while queue:
        name = queue.pop()
        if name in seen or name in EXCLUDE_MODULES:
            continue
        path = source / f'{name}.py'
        if not path.is_file():
            continue
        seen.add(name)
        tree = ast.parse(path.read_text(encoding='utf-8-sig'))
        for node in ast.walk(tree):
            if isinstance(node, ast.Import):
                queue.extend(alias.name.split('.')[0] for alias in node.names)
            elif isinstance(node, ast.ImportFrom) and node.level == 0 and node.module:
                queue.append(node.module.split('.')[0])
    return seen


def runtime_modules(source):
    for name in sorted(reachable_modules(source)):
        path = source / f'{name}.py'
        if path.is_file():
            yield path


def stage(repo, staging, version, wheels=None):
    source = repo / 'tools/CustomModel'
    android = source / 'android'
    if not android.is_dir():
        raise SystemExit(f'Android sources missing: {android}')

    root = staging / DIST_TEMPLATE.format(version=version)
    if root.exists():
        shutil.rmtree(root)
    app = root / 'app'
    app.mkdir(parents=True)

    modules = list(runtime_modules(source))
    for path in modules:
        shutil.copy2(path, app / path.name)
    shutil.copy2(android / 'bem_android.py', app / 'bem_android.py')

    for name in DATA_DIRS:
        directory = source / name
        if directory.is_dir():
            shutil.copytree(directory, app / name, ignore=IGNORE)

    for name in ENTRY_FILES:
        shutil.copy2(android / name, root / name)

    if wheels:
        wheel_files = sorted(Path(wheels).glob('*.whl'))
        if not wheel_files:
            raise SystemExit(f'No wheels found in {wheels}')
        target = root / 'wheels'
        target.mkdir()
        for path in wheel_files:
            shutil.copy2(path, target / path.name)

    return root, modules


def verify(root, python):
    """Import every shipped module and prove the catalog still resolves."""
    app = root / 'app'
    script = f'''
import importlib, json, pathlib, sys
app = pathlib.Path({str(app)!r})
sys.path.insert(0, str(app))
failures = []
modules = [p for p in sorted(app.glob("*.py")) if not p.name.startswith("test_")]
for path in modules:
    try:
        importlib.import_module(path.stem)
    except Exception as exc:
        failures.append("{{}}: {{}}: {{}}".format(path.name, type(exc).__name__, exc))
import component_auto
catalogs = len(component_auto.catalogs())
print(json.dumps({{"modules": len(modules), "catalogs": catalogs,
                  "failures": failures}}, ensure_ascii=False))
sys.exit(1 if failures or catalogs < 1 else 0)
'''
    with tempfile.TemporaryDirectory(prefix='bem-android-verify-') as tmp:
        check = Path(tmp) / 'verify.py'
        check.write_text(script, encoding='utf-8')
        env = dict(os.environ, PYTHONDONTWRITEBYTECODE='1')
        result = subprocess.run([str(python), str(check)], capture_output=True, text=True, env=env)
    payload = {}
    for line in result.stdout.splitlines():
        line = line.strip()
        if line.startswith('{'):
            payload = json.loads(line)
    if result.returncode != 0 or payload.get('failures'):
        detail = payload.get('failures') or [result.stderr.strip() or 'verification failed']
        raise SystemExit('Android package verification failed:\n  ' + '\n  '.join(detail))
    if payload.get('catalogs', 0) < 1:
        raise SystemExit('Android package verification failed: no character catalog loaded.')
    return payload


def archive(root, output, calculate_digest=False):
    output.parent.mkdir(parents=True, exist_ok=True)
    files = sorted(path for path in root.rglob('*')
                   if path.is_file() and '__pycache__' not in path.parts and path.suffix not in ('.pyc', '.pyo'))
    stamp = int(time.time())
    with tarfile.open(output, 'w:gz', compresslevel=9, format=tarfile.PAX_FORMAT) as tar:
        for path in files:
            arcname = path.relative_to(root.parent).as_posix()
            info = tar.gettarinfo(str(path), arcname=arcname)
            info.uid = info.gid = 0
            info.uname = info.gname = 'root'
            info.mode = 0o755 if path.name == 'bem-tools' or path.suffix == '.sh' else 0o644
            info.mtime = stamp
            with path.open('rb') as handle:
                tar.addfile(info, handle)
    digest = hashlib.sha256(output.read_bytes()).hexdigest() if calculate_digest else None
    return len(files), sum(path.stat().st_size for path in files), digest


def tool_version(repo, python):
    script = repo / 'tools/CustomModel/bem_tool.py'
    result = subprocess.run([str(python), str(script), '--version'], capture_output=True, text=True)
    text = ' '.join(result.stdout.split())
    if result.returncode != 0 or not text.startswith('BEM Tools '):
        raise SystemExit(f'Could not resolve the BEM Tools version: {text or result.stderr.strip()}')
    version = text.split()[2]
    return version, text


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--repo', type=Path, required=True)
    parser.add_argument('--staging', type=Path, required=True)
    parser.add_argument('--output', type=Path, required=True)
    parser.add_argument('--python', type=Path, default=Path(sys.executable))
    parser.add_argument('--wheels', type=Path, default=None,
                        help='Optional directory of prebuilt .whl files to bundle for offline pip install.')
    parser.add_argument('--skip-verify', action='store_true')
    parser.add_argument('--calculate-digest', action='store_true', help='Explicitly calculate the archive SHA-256.')
    args = parser.parse_args(argv)

    repo = args.repo.resolve()
    version, banner = tool_version(repo, args.python)
    if sys.version_info < PYTHON_MIN:
        raise SystemExit(f'Build with Python {PYTHON_MIN[0]}.{PYTHON_MIN[1]}+, this is {sys.version.split()[0]}')

    root, modules = stage(repo, args.staging.resolve(), version, wheels=args.wheels)
    check = {'skipped': True} if args.skip_verify else verify(root, args.python)
    files, raw_bytes, digest = archive(root, args.output.resolve(), args.calculate_digest)

    print(json.dumps({
        'version': version,
        'banner': banner,
        'package': str(args.output.resolve()),
        'staging': str(root),
        'modules': len(modules),
        'files': files,
        'raw_bytes': raw_bytes,
        'package_bytes': args.output.resolve().stat().st_size,
        'sha256': digest,
        'verification': check,
    }, ensure_ascii=False, indent=2))
    return 0


if __name__ == '__main__':
    raise SystemExit(main())
