"""Android host self-check and wheel installer for the BEM Tools CLI.

`bem-tools doctor` prints one JSON document describing whether this device can run
the converter. Agents should call it once before the first conversion: the tool
needs a CPython 3.10+ interpreter and a zstd backend - either the `zstandard`
extension or the standard library's `compression.zstd` (Python 3.14+).

`bem-tools install-wheel` puts the bundled zstandard wheel into site-packages by
copying files rather than by asking pip, because the wheel's platform tag cannot
match every Android interpreter: Termux reports ``android-<api>-arm64_v8a`` and pip
therefore wants ``android_*_arm64_v8a`` tags, while Linux-flavoured builds such as
Chaquopy use ``linux_aarch64``. The copy path works on both and needs no compiler.

This module deliberately avoids importing `bem_tool`, because a missing zstd
backend would make that import fail - and reporting that failure is exactly what
doctor is for.
"""
from __future__ import annotations

import json
import os
import platform
import re
import subprocess
import sys
import sysconfig
import zipfile
from pathlib import Path

APP = Path(__file__).resolve().parent
WHEEL_DIR = APP.parent/'wheels'


def _prop(name):
    """Read an Android system property; returns None off-device or when blocked."""
    try:
        out = subprocess.run(['getprop', name], capture_output=True, text=True, timeout=3)
    except Exception:
        return None
    value = out.stdout.strip()
    return value or None


def _tool_version():
    try:
        text = (APP / 'bem_tool.py').read_text(encoding='utf-8')
    except OSError:
        return None
    match = re.search(r"TOOL_VERSION\s*=\s*['\"]([^'\"]+)", text)
    return match.group(1) if match else None


def _zstd_backend():
    """Return (backend, version, error) without importing the converter."""
    try:
        import zstd_compat
    except Exception as exc:  # pragma: no cover - zstd_compat ships with the package
        return None, None, f'{type(exc).__name__}: {exc}'
    backend = zstd_compat.backend
    if backend is None:
        return None, None, 'neither zstandard nor compression.zstd is importable'
    try:
        import zstandard
    except Exception as exc:
        return backend, None, f'{type(exc).__name__}: {exc}'
    return backend, getattr(zstandard, '__version__', 'unknown'), None


def _writable(path):
    return bool(path) and os.path.isdir(path) and os.access(path, os.W_OK)


def _is_android():
    """Termux 3.14 reports sys.platform == 'android'; older builds report 'linux'."""
    if sys.platform == 'android':
        return True
    return sys.platform == 'linux' and bool(_prop('ro.build.version.release'))


def doctor():
    backend, zstd_version, zstd_error = _zstd_backend()
    archive_backend = (APP / 'source_archive.py').is_file()
    tmpdir = os.environ.get('TMPDIR') or None

    release = _prop('ro.build.version.release')
    host = {
        'platform': sys.platform,
        'is_android': _is_android(),
        'machine': platform.machine(),
        'android_release': release,
        'sdk': _prop('ro.build.version.sdk'),
        'abi': _prop('ro.product.cpu.abi'),
        'uid': os.geteuid() if hasattr(os, 'geteuid') else None,
        'root': bool(hasattr(os, 'geteuid') and os.geteuid() == 0),
    }

    issues = []
    if sys.version_info < (3, 10):
        issues.append({
            'code': 'PYTHON_TOO_OLD',
            'message': f'BEM Tools needs Python 3.10+, found {platform.python_version()}.',
            'hint': 'pkg install python (Termux) or point BEM_PYTHON at a newer interpreter.',
        })
    if backend is None:
        issues.append({
            'code': 'ZSTANDARD_MISSING',
            'message': f'No zstd backend is importable ({zstd_error}).',
            'hint': 'bem-tools install-wheel (bundled wheel), or upgrade to Python 3.14+ for compression.zstd.',
        })
    if not _writable(tmpdir):
        issues.append({
            'code': 'TMPDIR_NOT_WRITABLE',
            'message': f'TMPDIR is not set to a writable directory (got {tmpdir!r}).',
            'hint': 'export TMPDIR=/data/local/tmp, or set BEM_TOOLS_TMP.',
        })

    return {
        'tool': 'bem-tools',
        'tool_version': _tool_version(),
        'command': 'doctor',
        'success': not issues,
        'host': host,
        'python': {
            'version': platform.python_version(),
            'implementation': platform.python_implementation(),
            'executable': sys.executable,
            'ext_suffix': sysconfig.get_config_var('EXT_SUFFIX'),
            'platform_tag': sysconfig.get_platform(),
        },
        'deps': {'zstandard': zstd_version, 'zstd_backend': backend},
        'paths': {
            'app': str(APP),
            'tmpdir': tmpdir,
            'tmpdir_writable': _writable(tmpdir),
            'site_packages': sysconfig.get_paths().get('platlib'),
            'wheels': str(WHEEL_DIR) if WHEEL_DIR.is_dir() else None,
        },
        'capabilities': {
            'source_formats': ['zip', 'directory'] + (['rar', '7z'] if archive_backend else []),
            'archive_backend': archive_backend,
            'gui': False,
            'native_dump': False,
        },
        'issues': issues,
    }


def _wheel_tags(name):
    """Split `zstandard-0.25.0-cp314-cp314-android_24_arm64_v8a.whl` into tags."""
    parts = name[:-4].split('-') if name.endswith('.whl') else []
    if len(parts) < 5:
        return None
    return {'distribution': parts[0], 'version': parts[1],
            'python': parts[2], 'abi': parts[3], 'platform': '-'.join(parts[4:])}


def _select_wheel(source):
    """Pick the wheel matching this interpreter, or explain why none fits."""
    path = Path(source) if source else WHEEL_DIR
    candidates = sorted(path.glob('*.whl')) if path.is_dir() else ([path] if path.is_file() else [])
    if not candidates:
        return None, {'code': 'WHEEL_NOT_FOUND', 'message': f'No wheel found in {path}',
                      'hint': 'unpack the distribution again; wheels/ ships beside bem-tools.'}
    wanted = f'cp{sys.version_info[0]}{sys.version_info[1]}'
    for wheel in candidates:
        tags = _wheel_tags(wheel.name)
        if tags and tags['python'] in (wanted, 'py3', f'cp{sys.version_info[0]}'):
            return wheel, None
    listed = ', '.join(wheel.name for wheel in candidates)
    return None, {'code': 'WHEEL_UNSUPPORTED_PYTHON',
                  'message': f'No wheel matches Python {platform.python_version()} (need {wanted}); have {listed}.',
                  'hint': 'rebuild with android/build_zstandard_wheel.py against this Python, '
                          'or use Python 3.14+ where the standard library provides zstd.'}


def _extension_member(names):
    """The compiled module inside the wheel: zstandard/backend_c.<suffix>."""
    for name in names:
        if name.startswith('zstandard/') and not name.endswith(('.py', '.pyi', '/')):
            if Path(name).suffix in ('.so', '.pyd') or '.so.' in Path(name).name:
                return name
    return None


def install_wheel(source=None, target=None):
    issues = []
    wheel, issue = _select_wheel(source)
    if issue:
        return {'tool': 'bem-tools', 'command': 'install-wheel', 'success': False,
                'wheel': None, 'issues': [issue]}

    destination = Path(target) if target else Path(sysconfig.get_paths()['platlib'])
    ext_suffix = sysconfig.get_config_var('EXT_SUFFIX') or ''
    if not _writable(str(destination)):
        try:
            destination.mkdir(parents=True, exist_ok=True)
        except OSError as exc:
            return {'tool': 'bem-tools', 'command': 'install-wheel', 'success': False,
                    'wheel': str(wheel),
                    'issues': [{'code': 'SITE_PACKAGES_NOT_WRITABLE',
                                'message': f'{destination} is not writable ({exc}).',
                                'hint': 'run as root, or set BEM_PYTHON to an interpreter you own.'}]}

    written = []
    with zipfile.ZipFile(wheel) as archive:
        names = archive.namelist()
        binary = _extension_member(names)
        if binary is None:
            return {'tool': 'bem-tools', 'command': 'install-wheel', 'success': False,
                    'wheel': str(wheel),
                    'issues': [{'code': 'WHEEL_NO_EXTENSION',
                                'message': f'{wheel.name} carries no compiled module.',
                                'hint': 'the wheel is corrupt; rebuild it.'}]}
        module = Path(binary).name.split('.')[0]
        for name in names:
            if not name.startswith('zstandard/') or name.endswith('/'):
                continue
            payload = archive.read(name)
            if name == binary and ext_suffix:
                filename = f'{module}{ext_suffix}'
            else:
                filename = Path(name).name
            (destination/'zstandard').mkdir(parents=True, exist_ok=True)
            (destination/'zstandard'/filename).write_bytes(payload)
            written.append(f'zstandard/{filename}')

    # A previous install built against another Python leaves a dead copy behind;
    # the import machinery would pick whichever suffix it looks for first.
    for stale in (destination/'zstandard').glob(f'{module}.*'):
        if stale.name != f'{module}{ext_suffix}' and stale.suffix in ('.so', '.pyd'):
            stale.unlink(missing_ok=True)

    probe = (f'import sys; sys.path.insert(0, {str(destination)!r}); '
             'import zstandard; print(zstandard.backend, zstandard.__version__)')
    check = subprocess.run([sys.executable, '-c', probe], capture_output=True, text=True)
    if check.returncode != 0:
        detail = (check.stderr or check.stdout).strip().splitlines()
        issues.append({'code': 'IMPORT_FAILED',
                       'message': detail[-1] if detail else 'import failed',
                       'hint': f'check that {ext_suffix or "EXT_SUFFIX"} matches this interpreter '
                               f'({platform.python_version()}).'})
    output = check.stdout.split() if check.returncode == 0 else []
    backend = output[0] if output else None
    version = output[1] if len(output) > 1 else None
    return {
        'tool': 'bem-tools', 'command': 'install-wheel',
        'success': not issues,
        'wheel': str(wheel),
        'target': str(destination),
        'python': {'version': platform.python_version(), 'ext_suffix': ext_suffix},
        'files': written,
        'verification': {'importable': check.returncode == 0, 'backend': backend or None,
                         'version': version or None},
        'issues': issues,
    }


def main(argv=None):
    argv = list(sys.argv[1:] if argv is None else argv)
    command = argv[0] if argv else 'doctor'
    if command in ('doctor', 'self-check', 'selftest'):
        result = doctor()
    elif command in ('install-wheel', 'install-deps'):
        source = None
        target = None
        rest = argv[1:]
        while rest:
            item = rest.pop(0)
            if item == '--target' and rest:
                target = rest.pop(0)
            elif item.startswith('--target='):
                target = item.split('=', 1)[1]
            elif not item.startswith('-'):
                source = item
        result = install_wheel(source, target)
    else:
        print(json.dumps({
            'tool': 'bem-tools',
            'success': False,
            'issues': [{'code': 'UNKNOWN_COMMAND', 'message': f'Unknown subcommand: {command}'}],
        }, ensure_ascii=False, indent=2))
        return 2
    print(json.dumps(result, ensure_ascii=False, indent=2, allow_nan=False))
    return 0 if result['success'] else 2


if __name__ == '__main__':
    raise SystemExit(main())
