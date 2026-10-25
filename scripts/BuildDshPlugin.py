"""Build the DeepSeek Harness plugin bundle for the bem-creator skill.

Stages the plugin sources together with a copy of the packaged skill, runs
`npm pack`, verifies what landed in the archive, and copies the tarball into the
release directory.

The bundle version tracks BEM Tools. A mismatch fails the build instead of
shipping two version numbers that claim to be the same thing.
"""
from __future__ import annotations

import argparse
import json
import re
import shutil
import subprocess
import sys
import tarfile
import tempfile
from pathlib import Path

REPO = Path(__file__).resolve().parents[1]
PLUGIN = REPO/'tools/CustomModel/dsh-plugin'
SKILL = REPO/'tools/CustomModel/skills/bem-creator'
TOOL_SOURCE = REPO/'tools/CustomModel/bem_tool.py'
IGNORE = shutil.ignore_patterns('__pycache__', '*.pyc')
STAGED_SKILL = Path('skills')/'bem-creator'


def tool_version():
    text = TOOL_SOURCE.read_text(encoding='utf-8')
    match = re.search(r'^TOOL_VERSION = "([^"]+)"', text, re.MULTILINE)
    if not match:
        raise SystemExit(f'TOOL_VERSION not found in {TOOL_SOURCE}')
    return match.group(1)


def plugin_manifest():
    return json.loads((PLUGIN/'package.json').read_text(encoding='utf-8'))


def node_executable():
    node = shutil.which('node')
    if not node:
        raise SystemExit('node was not found on PATH; the plugin build needs Node.js 22 or newer.')
    return node


def npm_command():
    """Return argv prefix that runs npm, or raise with an actionable message."""
    node = node_executable()
    npm_cli = Path(node).resolve().parent/'node_modules/npm/bin/npm-cli.js'
    if not npm_cli.is_file():
        raise SystemExit(f'npm-cli.js not found next to node ({npm_cli}); install npm or pass a full Node.js distribution.')
    return [node, str(npm_cli)]


def release_directory(config_path=None):
    sys.path.insert(0, str(REPO/'scripts'))
    from workspace_config import load_workspace
    workspace = load_workspace(config_path, REPO)
    props = (REPO/'Directory.Build.props').read_text(encoding='utf-8')
    match = re.search(r'<Version>([^<]+)</Version>', props)
    if not match:
        raise SystemExit('Version not found in Directory.Build.props')
    return Path(workspace.path('paths.releases'))/match.group(1).strip()


def stage(destination):
    shutil.copytree(PLUGIN, destination, ignore=shutil.ignore_patterns('__pycache__', '*.pyc', 'skills'))
    if not (destination/'lib'/'index.js').is_file():
        raise SystemExit(f'{PLUGIN}/lib/index.js is missing')
    shutil.copytree(SKILL, destination/STAGED_SKILL, ignore=IGNORE)


def smoke(stage_directory):
    """Run the plugin against a stub context inside the staged package.

    The staged tree is the only place where `lib/` and `skills/` sit beside each
    other the way they will after installation, so the plugin's own path
    resolution is exercised for real.
    """
    result = subprocess.run([node_executable(), 'test/smoke.mjs'], cwd=stage_directory, capture_output=True, text=True)
    if result.returncode != 0:
        raise SystemExit(f'plugin smoke test failed:\n{result.stdout}{result.stderr}')
    return result.stdout.strip().splitlines()[0] if result.stdout.strip() else ''


def pack(stage_directory):
    argv = [*npm_command(), 'pack', '--pack-destination', str(stage_directory)]
    result = subprocess.run(argv, cwd=stage_directory, capture_output=True, text=True)
    if result.returncode != 0:
        raise SystemExit(f'npm pack failed:\n{result.stdout}{result.stderr}')
    archives = sorted(stage_directory.glob('*.tgz'))
    if len(archives) != 1:
        raise SystemExit(f'expected exactly one tarball, found {[a.name for a in archives]}')
    return archives[0]


def verify(archive, version):
    with tarfile.open(archive, 'r:gz') as tar:
        names = [member.name for member in tar.getmembers() if member.isfile()]
        if not names:
            raise SystemExit(f'{archive.name} contains no files')
        expected = {
            'package/package.json',
            'package/cordis.patch.yml',
            'package/lib/index.js',
            f'package/{STAGED_SKILL.as_posix()}/SKILL.md',
            f'package/{STAGED_SKILL.as_posix()}/scripts/bem_repair.py',
        }
        missing = sorted(expected - set(names))
        if missing:
            raise SystemExit(f'{archive.name} is missing {missing}')
        references = [n for n in names if n.startswith(f'package/{STAGED_SKILL.as_posix()}/references/')]
        if len(references) < 8:
            raise SystemExit(f'{archive.name} carries only {len(references)} reference documents')
        stray = [n for n in names if '__pycache__' in n or n.endswith('.pyc')]
        if stray:
            raise SystemExit(f'{archive.name} carries bytecode: {stray[:5]}')
        manifest = json.loads(tar.extractfile('package/package.json').read().decode('utf-8'))
    if manifest.get('version') != version:
        raise SystemExit(f'packed version {manifest.get("version")} does not match BEM Tools {version}')
    patch = (manifest.get('dsh') or {}).get('bundle', {}).get('patch')
    if patch != './cordis.patch.yml':
        raise SystemExit('packed manifest does not declare dsh.bundle.patch; dsh refuses to install such a package')
    return len(names), len(references)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--output', type=Path, help='Directory for the tarball; defaults to the release directory.')
    parser.add_argument('--workspace-config', type=Path, help='Workspace configuration used to resolve the release directory.')
    parser.add_argument('--keep-stage', action='store_true', help='Leave the staging directory in place for inspection.')
    args = parser.parse_args()

    version = tool_version()
    manifest = plugin_manifest()
    if manifest['version'] != version:
        raise SystemExit(f"{PLUGIN}/package.json version {manifest['version']} does not match BEM Tools {version}; bump both together")
    output = (args.output or release_directory(args.workspace_config)).resolve()

    with tempfile.TemporaryDirectory(prefix='dsh-bem-creator-') as temporary:
        stage_directory = Path(temporary)/'package'
        stage(stage_directory)
        smoke_summary = smoke(stage_directory)
        archive = pack(stage_directory)
        file_count, reference_count = verify(archive, version)
        output.mkdir(parents=True, exist_ok=True)
        destination = output/archive.name
        shutil.copyfile(archive, destination)
        if args.keep_stage:
            kept = REPO/'build/dsh-plugin-stage'
            shutil.rmtree(kept, ignore_errors=True)
            shutil.copytree(stage_directory, kept)

    size = destination.stat().st_size
    print(f'DSH plugin bundle: {destination} ({size} bytes)')
    print(f'  version {version}, {file_count} files, {reference_count} reference documents')
    if smoke_summary:
        print(f'  {smoke_summary}')


if __name__ == '__main__':
    main()
