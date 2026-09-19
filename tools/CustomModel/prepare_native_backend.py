"""Stage a bounded VFS fix without modifying the external AnimeStudio checkout."""
import argparse
import json
import shutil
from pathlib import Path


def patch_vfs(text):
    old = 'if (blockInfo.compressedSize > fileSize || blockInfo.uncompressedSize > fileSize * 20)'
    new = '''// Compression ratio is not a validity check (shared face maps exceed 20x).
                // Keep absolute allocation limits and validate the remaining compressed input.
                if (reader.BaseStream.Position < 0 || reader.BaseStream.Position > fileSize ||
                    blockInfo.compressedSize > fileSize - reader.BaseStream.Position ||
                    blockInfo.uncompressedSize > 512L * 1024 * 1024 ||
                    (blockInfo.flags == 0 && blockInfo.compressedSize != blockInfo.uncompressedSize))'''
    if text.count(old) != 1:
        raise ValueError('Unsupported VFSFile source: expected exactly one legacy ratio check')
    text = text.replace(old, new)
    # Never publish a partial decode or uninitialized pooled bytes after failure.
    old_decode = '''                                Logger.Warning($"Lz4 decompression error, write {numWrite} bytes but expected {uncompressedSize} bytes");'''
    old_catch = '''                            Logger.Error($"Lz4 decompression error : {e.Message}");'''
    old_write = '''                            blocksStream.Write(uncompressedBytesSpan);
                            ArrayPool<byte>.Shared.Return(compressedBytes, true);'''
    for marker in (old_decode, old_catch, old_write, 'reader.Read(compressedBytesSpan);'):
        if text.count(marker) != 1:
            raise ValueError('Unsupported VFSFile decode body')
    text = text.replace('reader.Read(compressedBytesSpan);', 'reader.BaseStream.ReadExactly(compressedBytesSpan);')
    text = text.replace(old_decode, old_decode.replace('Logger.Warning', 'throw new IOException'))
    text = text.replace(old_catch, '                            throw new IOException("VFS block decompression failed", e);')
    text = text.replace(old_write, '                            ArrayPool<byte>.Shared.Return(compressedBytes, true);')
    marker = '''                            }
                        }
                        catch (Exception e)'''
    if text.count(marker) != 1:
        raise ValueError('Unsupported VFSFile success path')
    return text.replace(marker, '''                            }
                            blocksStream.Write(uncompressedBytesSpan);
                        }
                        catch (Exception e)''')


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--source', type=Path, required=True)
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    source, output = args.source.resolve(), args.output.resolve()
    if source == output or source in output.parents or output in source.parents:
        parser.error('source and staging output must be separate trees')
    if output.exists() and any(output.iterdir()):
        parser.error('staging output must be empty')
    patched = patch_vfs((source / 'AnimeStudio/VFSFile.cs').read_text(encoding='utf-8-sig'))
    output.mkdir(parents=True, exist_ok=True)
    shutil.copytree(source / 'AnimeStudio', output / 'AnimeStudio',
                    ignore=shutil.ignore_patterns('bin', 'obj', '.git'))
    shutil.copy2(source / 'Directory.Build.props', output / 'Directory.Build.props')
    (output / 'AnimeStudio.Libraries').mkdir()
    shutil.copy2(source / 'AnimeStudio.Libraries/AnimeStudio.Ooz.dll', output / 'AnimeStudio.Libraries/AnimeStudio.Ooz.dll')
    (output / 'AnimeStudio/VFSFile.cs').write_text(patched, encoding='utf-8')
    (output / 'AnimeStudio/NativeBackendPatch.cs').write_text(
        '[assembly: System.Reflection.AssemblyMetadata("BetterEndfield.NativeBackendPatch", "bounded-vfs-blocks-v1")]\n',
        encoding='utf-8')
    (output / 'backend-patch.json').write_text(json.dumps({
        'schema': 1, 'source': str(source), 'patch': 'bounded-vfs-blocks-v1',
        'max_block_bytes': 512 * 1024 * 1024, 'source_modified': False,
    }, indent=2), encoding='utf-8')
    print(output / 'AnimeStudio/AnimeStudio.csproj')


if __name__ == '__main__':
    main()
