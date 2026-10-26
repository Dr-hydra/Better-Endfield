"""Cross-compile the `zstandard` C extension for Android/arm64 and ship it as a wheel.

The Android CLI deliberately depends on the host interpreter instead of bundling
one, so `zstandard` has to be installed on the device. Termux has no `zstandard`
package and PyPI's wheels are glibc or musl only, which leaves `pip install
zstandard` compiling zstd from source on-device with a full clang toolchain.

This script builds the wheel ahead of time with the Android NDK. It needs:

* the zstandard sdist (the C extension is plain C and included in it),
* the CPython headers of the target interpreter (Termux ships them in the
  `python` package under `$PREFIX/include/python3.X`),
* an Android NDK with `llvm-strip`.

Everything is verified before the wheel is written: the object must be an
ELF64/AArch64 shared object that exports `PyInit_backend_c` and needs nothing
outside `libc.so`, `libm.so` and `libdl.so`.

Wheel tags deserve a note. Termux's Python reports ``sys.platform == 'android'``
and ``sysconfig.get_platform() == 'android-<api>-arm64_v8a'``, so pip resolves
platform tags as ``android_<api>_arm64_v8a`` (packaging yields every API level
from the device's down to 16). A ``linux_aarch64`` tag would be rejected there.
Python builds that pretend to be Linux (Chaquopy, for instance) use
``linux_aarch64`` instead; `bem_android.py install-wheel` copies the extension
into site-packages directly and therefore works on both.

    python build_zstandard_wheel.py \\
        --sdist zstandard-0.25.0.tar.gz \\
        --python-include /path/to/include/python3.14 \\
        --ndk /path/to/android-ndk \\
        --output wheels/
"""

from __future__ import annotations

import argparse
import base64
import hashlib
import json
import os
import re
import shutil
import struct
import subprocess
import sys
import tarfile
import tempfile
import zipfile
from pathlib import Path

ZSTANDARD_VERSION = '0.25.0'
MODULE_NAME = 'backend_c'
DEFAULT_EXT_SUFFIX = '.cpython-314-aarch64-linux-android.so'
DEFAULT_PLATFORM_TAG = 'android_24_arm64_v8a'
# The vendored single-file zstd build uses no C++ runtime and no liblog.
ALLOWED_NEEDED = {'libc.so', 'libm.so', 'libdl.so', 'libz.so'}
PACKAGE_FILES = ('__init__.py', '__init__.pyi', 'py.typed', 'backend_cffi.py')
# The linker emits a handful of bookkeeping symbols into every shared object.
LINKER_SYMBOLS = {'_init', '_fini', '_edata', '_end', '__bss_start'}


def log(message):
    print(message, flush=True)


def tool(ndk, name):
    for candidate in (ndk/'toolchains/llvm/prebuilt/windows-x86_64/bin'/f'{name}.exe',
                      ndk/'toolchains/llvm/prebuilt/linux-x86_64/bin'/name,
                      ndk/'toolchains/llvm/prebuilt/darwin-x86_64/bin'/name):
        if candidate.is_file():
            return candidate
    raise SystemExit(f'{name} not found under {ndk}; pass --ndk pointing at an Android NDK')


def python_version(include):
    header = include/'Python.h'
    if not header.is_file():
        raise SystemExit(f'Python.h not found in {include}')
    # PY_MAJOR_VERSION lives in patchlevel.h on 3.14+ and in Python.h before that.
    for candidate in (include/'patchlevel.h', header):
        if not candidate.is_file():
            continue
        text = candidate.read_text(encoding='utf-8', errors='replace')
        major = re.search(r'#define\s+PY_MAJOR_VERSION\s+(\d+)', text)
        minor = re.search(r'#define\s+PY_MINOR_VERSION\s+(\d+)', text)
        if major and minor:
            return int(major.group(1)), int(minor.group(1))
    raise SystemExit(f'cannot read the Python version from {include}')


def unpack_sdist(sdist, destination):
    with tarfile.open(sdist, 'r:*') as archive:
        archive.extractall(destination, filter='data')
    roots = [entry for entry in destination.iterdir() if entry.is_dir()]
    if len(roots) != 1:
        raise SystemExit(f'expected one top-level directory in {sdist}, found {[p.name for p in roots]}')
    return roots[0]


def build(ndk, api, source, include, output):
    clang = tool(ndk, 'clang')
    sysroot = ndk/'toolchains/llvm/prebuilt/windows-x86_64/sysroot'
    if not sysroot.is_dir():
        sysroot = ndk/'toolchains/llvm/prebuilt/linux-x86_64/sysroot'
    argv = [
        str(clang),
        f'--target=aarch64-linux-android{api}',
        f'--sysroot={sysroot}',
        '-shared', '-fPIC', '-O2', '-DNDEBUG',
        # Same defines python-zstandard's own setup.py uses for the vendored zstd.
        '-DZSTD_SINGLE_FILE',
        '-DZSTDLIB_VISIBLE=',
        '-DZDICTLIB_VISIBLE=',
        '-DZSTDERRORLIB_VISIBLE=',
        '-fvisibility=hidden',
        f'-I{include}',
        f'-I{source/"c-ext"}',
        f'-I{source/"zstd"}',
        str(source/'c-ext/backend_c.c'),
        '-o', str(output),
        '-lm',
    ]
    log('  ' + ' '.join(argv[:6]) + ' ...')
    result = subprocess.run(argv, capture_output=True, text=True)
    if result.returncode != 0:
        raise SystemExit(f'compile failed:\n{result.stdout}{result.stderr}')
    warnings = [line for line in result.stderr.splitlines() if 'warning:' in line]
    if warnings:
        log(f'  {len(warnings)} compiler warning(s), first: {warnings[0].strip()}')
    return output


def strip(ndk, path):
    stripper = tool(ndk, 'llvm-strip')
    result = subprocess.run([str(stripper), '--strip-unneeded', str(path)], capture_output=True, text=True)
    if result.returncode != 0:
        raise SystemExit(f'llvm-strip failed:\n{result.stdout}{result.stderr}')
    return path


def verify_elf(ndk, path):
    """Reject anything that is not a self-contained AArch64 extension module."""
    data = path.read_bytes()
    if data[:4] != b'\x7fELF':
        raise SystemExit(f'{path.name} is not an ELF object')
    if data[4] != 2 or data[5] != 1:
        raise SystemExit(f'{path.name} is not a 64-bit little-endian object')
    machine = struct.unpack_from('<H', data, 18)[0]
    if machine != 183:
        raise SystemExit(f'{path.name} targets machine {machine}, expected 183 (AArch64)')
    e_type = struct.unpack_from('<H', data, 16)[0]
    if e_type != 3:
        raise SystemExit(f'{path.name} is not ET_DYN (shared object)')

    readelf = tool(ndk, 'llvm-readelf')
    dynamic = subprocess.run([str(readelf), '-d', str(path)], capture_output=True, text=True).stdout
    needed = set(re.findall(r'Shared library: \[([^\]]+)\]', dynamic))
    unexpected = needed - ALLOWED_NEEDED
    if unexpected:
        raise SystemExit(f'{path.name} needs {sorted(unexpected)}, which Android does not provide')

    symbols = tool(ndk, 'llvm-nm')
    exported = subprocess.run([str(symbols), '-D', '--defined-only', str(path)],
                              capture_output=True, text=True).stdout
    if f'PyInit_{MODULE_NAME}' not in exported:
        raise SystemExit(f'{path.name} does not export PyInit_{MODULE_NAME}')
    names = [line.split()[-1] for line in exported.strip().splitlines() if line.split()]
    extra = sorted(set(names) - LINKER_SYMBOLS - {f'PyInit_{MODULE_NAME}'})
    if extra:
        raise SystemExit(f'{path.name} exports unexpected symbols: {extra[:5]}')

    undefined = subprocess.run([str(symbols), '-D', '--undefined-only', str(path)],
                               capture_output=True, text=True).stdout
    libc = [line.split()[-1] for line in undefined.splitlines()
            if line.split() and '@' in line.split()[-1] and 'LIBC' not in line.split()[-1]
            and 'LIBM' not in line.split()[-1]]
    if libc:
        raise SystemExit(f'{path.name} references non-bionic symbols: {libc[:5]}')
    return sorted(needed)


def record_hash(data):
    digest = hashlib.sha256(data).digest()
    return 'sha256=' + base64.urlsafe_b64encode(digest).decode('ascii').rstrip('=')


def write_wheel(source, extension, destination, *, dist_version, python_tag, abi_tag, platform_tag, ext_suffix):
    """Assemble the wheel by hand: it is a zip with a RECORD and a WHEEL file."""
    name = f'zstandard-{dist_version}-{python_tag}-{abi_tag}-{platform_tag}.whl'
    wheel_path = destination/name
    members = []
    for filename in PACKAGE_FILES:
        path = source/'zstandard'/filename
        if path.is_file():
            members.append((f'zstandard/{filename}', path.read_bytes()))
    members.append((f'zstandard/{MODULE_NAME}{ext_suffix}', extension.read_bytes()))

    metadata = (source/'PKG-INFO').read_bytes()
    if b'Metadata-Version' not in metadata:
        raise SystemExit('PKG-INFO does not look like core metadata')
    wheel = (
        'Wheel-Version: 1.0\n'
        'Generator: bem-android-zstandard 1.0\n'
        'Root-Is-Purelib: false\n'
        f'Tag: {python_tag}-{abi_tag}-{platform_tag}\n'
    ).encode('utf-8')
    dist_info = f'zstandard-{dist_version}.dist-info'
    members.append((f'{dist_info}/METADATA', metadata))
    members.append((f'{dist_info}/WHEEL', wheel))
    members.append((f'{dist_info}/top_level.txt', b'zstandard\n'))
    license_path = source/'LICENSE'
    if license_path.is_file():
        members.append((f'{dist_info}/licenses/LICENSE', license_path.read_bytes()))

    rows = []
    for name_in_wheel, data in members:
        rows.append(f'{name_in_wheel},{record_hash(data)},{len(data)}')
    rows.append(f'{dist_info}/RECORD,,')
    members.append((f'{dist_info}/RECORD', ('\n'.join(rows) + '\n').encode('utf-8')))

    destination.mkdir(parents=True, exist_ok=True)
    with zipfile.ZipFile(wheel_path, 'w', compression=zipfile.ZIP_DEFLATED, compresslevel=9) as archive:
        for name_in_wheel, data in members:
            info = zipfile.ZipInfo(name_in_wheel, date_time=(1980, 1, 1, 0, 0, 0))
            info.compress_type = zipfile.ZIP_DEFLATED
            info.external_attr = 0o644 << 16
            archive.writestr(info, data)
    return wheel_path


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument('--sdist', type=Path, required=True, help='zstandard source archive')
    parser.add_argument('--python-include', type=Path, required=True,
                        help='directory holding the target Python.h (Termux: $PREFIX/include/python3.X)')
    parser.add_argument('--ndk', type=Path, required=True, help='Android NDK root')
    parser.add_argument('--api', type=int, default=24, help='minimum Android API level (default 24)')
    parser.add_argument('--output', type=Path, help='directory for the wheel; defaults to ./wheels')
    parser.add_argument('--ext-suffix', default=DEFAULT_EXT_SUFFIX,
                        help=f'extension suffix the target interpreter uses (default {DEFAULT_EXT_SUFFIX})')
    parser.add_argument('--platform-tag', default=DEFAULT_PLATFORM_TAG,
                        help=f'wheel platform tag (default {DEFAULT_PLATFORM_TAG})')
    parser.add_argument('--keep-work', action='store_true', help='leave the build directory in place')
    args = parser.parse_args(argv)

    include = args.python_include.resolve()
    major, minor = python_version(include)
    if (major, minor) < (3, 9):
        raise SystemExit(f'zstandard needs Python 3.9+, headers are {major}.{minor}')
    python_tag = f'cp{major}{minor}'
    abi_tag = python_tag
    if f'cpython-{major}{minor}' not in args.ext_suffix:
        raise SystemExit(f'--ext-suffix {args.ext_suffix} does not match Python {major}.{minor}')
    output = (args.output or Path('wheels')).resolve()

    ndk = args.ndk.resolve()
    work = Path(tempfile.mkdtemp(prefix='zstandard-android-'))
    try:
        log(f'zstandard {ZSTANDARD_VERSION} -> {python_tag}/{args.platform_tag} (API {args.api})')
        source = unpack_sdist(args.sdist.resolve(), work)
        raw = build(ndk, args.api, source, include, work/f'{MODULE_NAME}.so')
        log(f'  linked {raw.stat().st_size/1024:.0f} KB')
        strip(ndk, raw)
        needed = verify_elf(ndk, raw)
        log(f'  stripped {raw.stat().st_size/1024:.0f} KB, needs {", ".join(needed)}, exports PyInit_{MODULE_NAME}')
        wheel = write_wheel(source, raw, output,
                            dist_version=ZSTANDARD_VERSION, python_tag=python_tag, abi_tag=abi_tag,
                            platform_tag=args.platform_tag, ext_suffix=args.ext_suffix)
        log(f'wheel {wheel} ({wheel.stat().st_size/1024:.0f} KB)')
        log(json.dumps({'wheel': wheel.name, 'bytes': wheel.stat().st_size,
                        'sha256': hashlib.sha256(wheel.read_bytes()).hexdigest()}, indent=2))
    finally:
        if args.keep_work:
            log(f'work directory kept at {work}')
        else:
            shutil.rmtree(work, ignore_errors=True)
    return 0


if __name__ == '__main__':
    raise SystemExit(main())
