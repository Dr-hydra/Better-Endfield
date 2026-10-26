"""Supply `zstandard` from the standard library when the C extension is absent.

The converter needs zstd frames: payloads are zstandard-compressed and the reader
checks the frame content size before decompressing. The `zstandard` extension is
the only third-party dependency, and installing it on Android means compiling zstd
with clang on the device.

CPython 3.14 added `compression.zstd` (PEP 784), and Termux already ships it, so a
device can run the CLI with no third-party packages at all. Importing this module
before `zstandard` makes `import zstandard` resolve to a stand-in built on the
stdlib when the real extension is missing. When the extension is present this
module does nothing, so Windows and any `pip install zstandard` setup keep using
the C backend.

The stand-in covers the surface the converter uses:

    zstd.ZstdCompressor(level=9).compress(data)
    zstd.ZstdDecompressor().decompress(data, max_output_size=..., allow_extra_data=False)
    zstd.frame_content_size(frame)
    zstd.ZstdError

Two differences are intentional. python-zstandard's compressor object streams, while
the stdlib one is incremental; `compress()` here is one-shot, which is how the
converter calls it. And `max_output_size` is enforced after decompression instead of
during it - the caller has already checked the frame's declared content size, so the
buffer is bounded by the package format before this runs.
"""
from __future__ import annotations

import sys
import types

__all__ = ['backend', 'install']

#: 'extension' when the real zstandard package is importable, 'stdlib' for the
#: stand-in, None when neither backend is available.
backend = None


def _stdlib_module():
    try:
        from compression import zstd  # Python 3.14+
    except ImportError:
        return None
    return zstd


def _install_stdlib_backend(zstd):
    # Alias, not a subclass: errors raised inside compression.zstd are the base
    # class, and callers catch zstandard.ZstdError.
    ZstdError = zstd.ZstdError

    class ZstdCompressor:
        """One-shot compressor; python-zstandard's object is stream-capable, callers here are not."""

        def __init__(self, level=3, dict_data=None, compression_params=None):
            if dict_data is not None or compression_params is not None:
                raise zstd.ZstdError('the stdlib backend does not support dictionaries or compression parameters')
            self.level = level

        def compress(self, data):
            return zstd.compress(bytes(data), level=self.level)

    class ZstdDecompressor:
        def __init__(self, dict_data=None, max_window_size=0, format=0):
            if dict_data is not None:
                raise zstd.ZstdError('the stdlib backend does not support dictionaries')

        def decompress(self, data, max_output_size=0, allow_extra_data=True,
                       read_across_frames=False):
            source = bytes(data)
            if not allow_extra_data:
                frame_size = zstd.get_frame_size(source)
                if frame_size != len(source):
                    raise ZstdError(f'compressed data has {len(source) - frame_size} trailing bytes')
            output = zstd.decompress(source)
            if max_output_size and len(output) > max_output_size:
                raise ZstdError(f'decompressed data is {len(output)} bytes, max_output_size is {max_output_size}')
            return output

    def frame_content_size(frame_buffer):
        size = zstd.get_frame_info(bytes(frame_buffer)).decompressed_size
        return size or 0

    module = types.ModuleType('zstandard')
    module.__doc__ = 'BEM Tools stand-in for python-zstandard, backed by compression.zstd.'
    module.__version__ = '0.0.0+stdlib'
    module.backend = 'stdlib'
    module.ZstdError = ZstdError
    module.ZstdCompressor = ZstdCompressor
    module.ZstdDecompressor = ZstdDecompressor
    module.frame_content_size = frame_content_size
    return module


def install():
    """Register the stand-in when needed; returns the backend name in use."""
    global backend
    if backend is not None:
        return backend
    if 'zstandard' in sys.modules:
        # Someone else already imported a backend; trust it.
        backend = getattr(sys.modules['zstandard'], 'backend', 'extension')
        return backend
    try:
        import zstandard  # noqa: F401
    except ImportError:
        stdlib = _stdlib_module()
        if stdlib is None:
            backend = None
            return None
        sys.modules['zstandard'] = _install_stdlib_backend(stdlib)
        backend = 'stdlib'
    else:
        backend = getattr(sys.modules['zstandard'], 'backend', 'extension')
    return backend


install()
