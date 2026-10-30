"""Build the DeepSeek Harness plugin bundle for the bem-creator skill.

Stages the plugin sources together with a copy of the packaged skill, folds in
the platform CLIs, runs `npm pack`, verifies what landed in the archive, and
copies the tarball into the release directory.

The CLI half is the point of this script's second act. The plugin used to rely
on a BEM Tools installation being present, which meant `convert` worked only on
a machine that already had the toolchain. Shipping the CLI is what removes that
dependency; what stays out is the CLI's own runtime, which is either already
inside the PyInstaller build (Windows) or fetched by the device (zstandard).

The bundle version tracks BEM Tools. A mismatch fails the build instead of
shipping two version numbers that claim to be the same thing.
"""
from __future__ import annotations

import argparse
import json
import os
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
ANDROID_ARCHIVE = REPO/'build/tools/bem-android/dist/BEM-Tools-android-arm64.tar.gz'
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


def directory_size(path):
    return sum(item.stat().st_size for item in path.rglob('*') if item.is_file())


def stage_cli(destination):
    """Unpack the Python source tree that both platforms run.

    Not the PyInstaller build: its 33 MB is almost entirely the CPython runtime,
    and a machine that runs a mod toolchain already has one. Shipping sources
    also collapses the two platforms into a single tree — `python` on Windows,
    `python3` on Android — instead of a Windows-only binary beside a POSIX copy.

    Left out on purpose, because each is easy to fetch and none is needed to
    read or write a BEM package:

      * the CPython runtime (the host provides it, or it reports PYTHON_MISSING),
      * the prebuilt zstandard wheel (Python 3.14+ has `compression.zstd`),
      * the 7-Zip backend (only .zip/.rar/.7z sources need it).

    The source tree still carries `catalog/` and `profiles/`. `component_auto`
    resolves `catalog/` from the entry point's directory, so dropping the data
    would not save a meaningful number of bytes — it is 1.3 MB compressed for
    10 MB of JSON — and would break every conversion that matches components.
    """
    if not ANDROID_ARCHIVE.is_file():
        return None
    target = destination/'lib/bin/cli'
    target.mkdir(parents=True, exist_ok=True)
    with tarfile.open(ANDROID_ARCHIVE, 'r:gz') as tar:
        members = []
        for member in tar.getmembers():
            parts = Path(member.name).parts[1:]  # drop BEM-Tools-<version>-android-arm64/
            if not parts or parts[0] != 'app':
                continue
            rest = parts[1:]
            if not rest:
                continue
            if '__pycache__' in rest or member.name.endswith('.pyc'):
                continue
            member.name = Path(*rest).as_posix()
            members.append(member)
        try:
            tar.extractall(target, members=members, filter='data')
        except TypeError:  # Python < 3.12 has no extraction filter
            tar.extractall(target, members=members)
    return directory_size(target)


def stage(destination, with_cli=True):
    shutil.copytree(PLUGIN, destination, ignore=shutil.ignore_patterns('__pycache__', '*.pyc', 'skills', 'bin'))
    if not (destination/'lib'/'index.js').is_file():
        raise SystemExit(f'{PLUGIN}/lib/index.js is missing')
    shutil.copytree(SKILL, destination/STAGED_SKILL, ignore=IGNORE)
    if not with_cli:
        return {}
    size = stage_cli(destination)
    if size is None:
        raise SystemExit(
            'no CLI source tree found; run scripts/BuildBemCliAndroid.ps1 first '
            '(it produces the Python tree both platforms use), or pass --no-cli'
        )
    return {'python': size}


def smoke(stage_directory):
    """Run the plugin's Node tests inside the staged package.

    The staged tree is the only place where `lib/` and `skills/` sit beside each
    other the way they will after installation, so the plugin's own path
    resolution is exercised for real. The two package paths are handed over as
    environment variables because the staged tree lives in a temp directory and
    cannot derive the repository root.
    """
    environment = dict(os.environ)
    # The host test invokes the staged Python CLI. Without this, CPython writes
    # __pycache__/*.pyc into package/lib/bin/cli and `npm pack` ships them; the
    # archive check below then rejects the build for carrying bytecode.
    environment.setdefault('PYTHONDONTWRITEBYTECODE', '1')
    for variable, relative in (
        ('BEM_TEST_PACKAGE_10', 'artifacts/native-bem-tohsaka-aglina-20261008/dist/TohsakaRin-aglina-by-Dr-hydra.bem'),
        ('BEM_TEST_PACKAGE_15', 'temp/v3-check.bem'),
        # The ASTC fixture is the only package where every texture is a format
        # the browser cannot decode; without it the "ASTC is skipped" assertions
        # pass vacuously on the 1.0 package, which carries none.
        ('BEM_TEST_PACKAGE_ASTC', 'artifacts/native-bem-tohsaka-aglina-20261008/dist/TohsakaRin-aglina-native-test-0.3.2-android-astc.bem'),
    ):
        environment[variable] = str(REPO/relative)
    summaries = []
    for suite in ('test/smoke.mjs', 'test/host.test.mjs', 'test/client.test.mjs', 'test/resources.test.mjs', 'test/astc.test.mjs'):
        result = subprocess.run(
            [node_executable(), suite],
            cwd=stage_directory,
            capture_output=True,
            text=True,
            encoding='utf-8',
            errors='replace',
            env=environment,
        )
        if result.returncode != 0:
            raise SystemExit(f'{suite} failed:\n{result.stdout}{result.stderr}')
        lines = [line for line in result.stdout.strip().splitlines() if line.strip()]
        failures = [line for line in lines if 'FAIL' in line or 'skip' in line]
        summaries.extend(['    ' + line for line in failures])
        summaries.append(f'  {suite}: ' + (lines[-1] if lines else 'ok'))
    return summaries


def purge_bytecode(directory):
    """Drop Python bytecode a staged test run may have written.

    `PYTHONDONTWRITEBYTECODE` covers the normal path, but the cleanup keeps the
    archive correct even when an existing cache is copied in or a test starts a
    child that does not inherit the environment.
    """
    for cache in list(directory.rglob('__pycache__')):
        shutil.rmtree(cache, ignore_errors=True)
    for stale in list(directory.rglob('*.pyc')):
        try:
            stale.unlink()
        except FileNotFoundError:
            pass


def pack(stage_directory):
    purge_bytecode(stage_directory)
    argv = [*npm_command(), 'pack', '--pack-destination', str(stage_directory)]
    result = subprocess.run(argv, cwd=stage_directory, capture_output=True, text=True)
    if result.returncode != 0:
        raise SystemExit(f'npm pack failed:\n{result.stdout}{result.stderr}')
    archives = sorted(stage_directory.glob('*.tgz'))
    if len(archives) != 1:
        raise SystemExit(f'expected exactly one tarball, found {[a.name for a in archives]}')
    return archives[0]


def verify(archive, version, with_cli=True):
    with tarfile.open(archive, 'r:gz') as tar:
        names = [member.name for member in tar.getmembers() if member.isfile()]
        if not names:
            raise SystemExit(f'{archive.name} contains no files')
        expected = {
            'package/package.json',
            'package/cordis.patch.yml',
            'package/lib/index.js',
            # Browser half: the entry, the vendored decoder it serves, and the
            # worker that runs it off the main thread.
            'package/lib/client.js',
            'package/lib/vendor/bcn.js',
            'package/lib/assets/decode-worker.js',
            f'package/{STAGED_SKILL.as_posix()}/SKILL.md',
            f'package/{STAGED_SKILL.as_posix()}/scripts/bem_repair.py',
        }
        if with_cli:
            # The whole reason the bundle carries code at all: `convert` must not
            # depend on a toolchain the machine happens to have installed.
            expected |= {
                'package/lib/bin/cli/bem_tool.py',
                'package/lib/bin/cli/bem_v1.py',
                'package/lib/bin/cli/catalog/chr_0013_aglina.json',
            }
        missing = sorted(expected - set(names))
        if missing:
            raise SystemExit(f'{archive.name} is missing {missing}')
        if with_cli:
            # "Do not ship what the machine can fetch" is only true if the
            # archive actually leaves it out; a stray .exe/.dll means the
            # PyInstaller build crept back in.
            runtime = [n for n in names if n.endswith(('.exe', '.dll', '.pyd', '.so'))]
            if runtime:
                raise SystemExit(f'{archive.name} ships a native runtime it should let the host provide: {runtime[:3]}')
            wheels = [n for n in names if '/wheels/' in n]
            if wheels:
                raise SystemExit(f'{archive.name} ships a wheel the device can fetch: {wheels[:3]}')
        references = [n for n in names if n.startswith(f'package/{STAGED_SKILL.as_posix()}/references/')]
        if len(references) < 8:
            raise SystemExit(f'{archive.name} carries only {len(references)} reference documents')
        stray = [n for n in names if '__pycache__' in n or n.endswith('.pyc')]
        if stray:
            raise SystemExit(f'{archive.name} carries bytecode: {stray[:5]}')
        manifest = json.loads(tar.extractfile('package/package.json').read().decode('utf-8'))
        client_entry = tar.extractfile('package/lib/client.js').read().decode('utf-8')
    if manifest.get('version') != version:
        raise SystemExit(f'packed version {manifest.get("version")} does not match BEM Tools {version}')
    patch = (manifest.get('dsh') or {}).get('bundle', {}).get('patch')
    if patch != './cordis.patch.yml':
        raise SystemExit('packed manifest does not declare dsh.bundle.patch; dsh refuses to install such a package')
    # dsh-client-modules throws when a package declares dsh.client without an
    # exports["./client"] entry, so both halves are checked together.
    client = (manifest.get('dsh') or {}).get('client') or {}
    if client.get('platform') != 'web':
        raise SystemExit('packed manifest does not declare dsh.client.platform = web; the preview tab would never load')
    exported = (manifest.get('exports') or {}).get('./client')
    if exported != './lib/client.js':
        raise SystemExit(f'packed manifest exports["./client"] is {exported!r}, not "./lib/client.js"')
    if '__ModuleLoader__.load' not in client_entry:
        raise SystemExit('packed lib/client.js is not a module-loader payload; run node tools/build-client.mjs')
    return len(names), len(references)


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--output', type=Path, help='Directory for the tarball; defaults to the release directory.')
    parser.add_argument('--workspace-config', type=Path, help='Workspace configuration used to resolve the release directory.')
    parser.add_argument('--keep-stage', action='store_true', help='Leave the staging directory in place for inspection.')
    parser.add_argument('--no-cli', action='store_true', help='Build the code-only bundle, without the platform CLIs (fast iteration).')
    args = parser.parse_args()
    # Honor the configured build root, including fresh release workspaces.
    if args.workspace_config:
        sys.path.insert(0, str(REPO/'scripts'))
        from workspace_config import load_workspace
        global ANDROID_ARCHIVE
        ANDROID_ARCHIVE = load_workspace(args.workspace_config, REPO).path('paths.build')/'tools/bem-android/dist/BEM-Tools-android-arm64.tar.gz'

    version = tool_version()
    manifest = plugin_manifest()
    if manifest['version'] != version:
        raise SystemExit(f"{PLUGIN}/package.json version {manifest['version']} does not match BEM Tools {version}; bump both together")
    output = (args.output or release_directory(args.workspace_config)).resolve()

    with tempfile.TemporaryDirectory(prefix='dsh-bem-creator-') as temporary:
        stage_directory = Path(temporary)/'package'
        stage(stage_directory)
        test_summary = smoke(stage_directory)
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
    print('\n'.join(test_summary))


if __name__ == '__main__':
    main()
