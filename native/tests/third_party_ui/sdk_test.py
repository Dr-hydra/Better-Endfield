"""Verify public SDK contents and build Echo without any repository header path."""
import argparse
import ctypes
import json
from pathlib import Path
import subprocess
import tempfile
import zipfile


def run(*arguments):
    result = subprocess.run(arguments, text=True, encoding='utf-8', errors='replace', capture_output=True)
    if result.returncode:
        raise RuntimeError(f'{arguments[0]} failed ({result.returncode})\n{result.stdout[-6000:]}\n{result.stderr[-6000:]}')


def inspect_zip(path):
    with zipfile.ZipFile(path) as archive:
        names = archive.namelist()
        assert len(names) == len(set(names)), 'duplicate archive entries'
        assert len(names) == len({name.lower() for name in names}), 'case-colliding archive entries'
        assert all(not name.startswith('/') and '\\' not in name and ':' not in name and '..' not in name.split('/') for name in names), 'unsafe archive path'
        assert all(Path(name).name.lower() != 'index.json' for name in names), 'private installation index distributed'
        return {name: archive.read(name) for name in names}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('sdk', type=Path)
    parser.add_argument('module', type=Path)
    parser.add_argument('--cmake', default='cmake')
    args = parser.parse_args()
    sdk = inspect_zip(args.sdk)
    package = inspect_zip(args.module)
    manifest = json.loads(package['module.json'])
    assert manifest['format'] == manifest['abi'] == 1 and manifest['id'] == 'example.echo'
    assert set(manifest['libraries']) == {'windows-x64', 'android-arm64'}
    assert all(path in package for path in manifest['libraries'].values())
    assert manifest['ui'] in package
    headers = {'ModuleApi.h', 'HookChain.h', 'ThirdPartyModule.h'}
    assert {Path(name).name for name in sdk if name.startswith('include/BetterEndfieldNext/')} == headers
    assert sdk['packages/' + args.module.name] == args.module.read_bytes(), 'embedded import ZIP differs'
    assert sdk['docs/host/THIRD_PARTY_MODULE_CREATOR_GUIDE.md'].startswith(b'# ')
    for platform_path in manifest['libraries'].values():
        assert sdk['examples/echo/' + platform_path] == package[platform_path]
    # The UUID temp root is outside the repository; cleanup targets only this exact directory.
    with tempfile.TemporaryDirectory(prefix='better-endfield-sdk-') as temporary:
        temp_root = Path(temporary).resolve()
        assert temp_root.parent == Path(tempfile.gettempdir()).resolve()
        for relative, content in sdk.items():
            target = (temp_root / relative).resolve()
            assert target.is_relative_to(temp_root)
            target.parent.mkdir(parents=True, exist_ok=True)
            target.write_bytes(content)
        source = temp_root / 'examples/echo'
        build = temp_root / 'build'
        run(args.cmake, '-S', str(source), '-B', str(build), '-A', 'x64')
        run(args.cmake, '--build', str(build), '--config', 'Release')
        cache = (build / 'CMakeCache.txt').read_text(encoding='utf-8')
        include_value = next(line.split('=', 1)[1] for line in cache.splitlines() if line.startswith('BETTER_ENDFIELD_NEXT_SDK_INCLUDE:PATH='))
        assert Path(include_value).resolve() == temp_root / 'include', 'sample used headers outside extracted SDK'
        built_manifest = json.loads((build / 'package/module.json').read_text(encoding='utf-8'))
        assert set(built_manifest['libraries']) == {'windows-x64'}, 'single-platform output contains unsupported platform'
        library = build / 'package' / built_manifest['libraries']['windows-x64']
        class Module(ctypes.Structure):
            _fields_ = [('struct_size', ctypes.c_uint32), ('version', ctypes.c_uint32), ('id', ctypes.c_char_p),
                        ('initialize', ctypes.c_void_p), ('configuration_changed', ctypes.c_void_p),
                        ('on_message', ctypes.c_void_p), ('shutdown', ctypes.c_void_p)]
        loaded = ctypes.CDLL(str(library))
        entry = loaded.BetterEndfieldNext_GetThirdPartyModuleV1
        entry.argtypes = []
        entry.restype = ctypes.POINTER(Module)
        module = entry().contents
        assert module.struct_size >= ctypes.sizeof(Module) and module.version == 1 and module.id == b'example.echo'
        assert module.initialize and module.on_message
        # This diagnostic process never initializes the module or installs hooks.
        # Release its own test-only DLL handle so Windows can clean the temporary build.
        import _ctypes
        _ctypes.FreeLibrary(loaded._handle)
    print('PASS SDK archive allowlist, embedded dual ZIP, native byte preservation, independent CMake build and exported ABI')


if __name__ == '__main__':
    main()
