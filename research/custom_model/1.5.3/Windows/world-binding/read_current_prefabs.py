"""One-off bounded, read-only VFS capture: exact world/UI prefab bundles only."""
import dataclasses
import importlib.util
import json
import re
import struct
import subprocess
import sys
import types
import zlib
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
OUT = Path(__file__).resolve().parent
sys.path.insert(0, str(OUT / 'python-deps'))
sys.stdout.reconfigure(encoding='utf-8')
spec = importlib.util.spec_from_file_location('wb_helpers', ROOT / 'scripts/RefreshEndfieldResourceInputs.py')
h = importlib.util.module_from_spec(spec)
sys.modules[spec.name] = h
spec.loader.exec_module(h)
stub = types.ModuleType('config')
stub.get_game_dir = lambda: 'D:/Arknights Endfield'
sys.modules['config'] = stub
u = h.load_module('wb_vfs', ROOT / 'tools/EndfieldUnpacker/decrypt_vfs.py')
u.crc32 = lambda data: (zlib.crc32(data) + 2**31) % 2**32 - 2**31
ADB = str(ROOT / 'tools/android-toolchain/sdk/platform-tools/adb.exe')
DEVICE_ROOT = '/sdcard/Android/data/com.hypergryph.endfield/files/VFS'
CACHE = ROOT / 'android/resources/.sources/20261001-wireless-192.168.31.12-36477'
CHARACTERS = ['chr_0006_wolfgd', 'chr_0034_typhoea']
INDEX_ROOT = OUT / 'android-indices'

def remote(command):
    return subprocess.check_output([ADB, 'exec-out', 'sh', '-c', command], timeout=30)

def block_name(blc):
    plain = u.decrypt_blc(blc)
    version = struct.unpack_from('<i', plain)[0]
    pos = 8 if version < 11 else 4
    length = struct.unpack_from('<H', plain, pos)[0]
    return plain[pos + 2:pos + 2 + length].decode('ascii')

def select(manifest, platform):
    wanted = {f'assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/{c}_postmodel.prefab' for c in CHARACTERS}
    wanted.update(f'assets/beyond/dynamicassets/gameplay/prefabs/uimodels/{c}_uimodel.prefab' for c in CHARACTERS)
    assets = [a for a in manifest['Assets'] if a['path'] in wanted]
    assert len(assets) == len(wanted), (wanted - {a['path'] for a in assets})
    return assets, ['Bundles/' + platform + '/' + manifest['Bundles'][a['bundleIndex']]['name'] for a in assets]

def android():
    indices = INDEX_ROOT
    indices.mkdir(exist_ok=True)
    # Confirm the cached manifest's index still exactly matches this device.
    manifest_blc = CACHE / 'persistent-vfs/1CDDBF1F/1CDDBF1F.blc'
    current = remote(f'cat {DEVICE_ROOT}/1CDDBF1F/1CDDBF1F.blc')
    assert current == manifest_blc.read_bytes(), 'cached Android manifest is stale'
    manifest = json.loads((CACHE / 'current-inputs/Bundles/Android/manifest.json').read_text(encoding='utf-8-sig'))
    assets, names = select(manifest, 'Android')
    target = re.compile('(?:' + '|'.join(map(re.escape, names)) + ')', re.I)
    # The main Bundle index is metadata; never download a whole CHK file.
    block = '7064D8E2'
    blc = indices / f'{block}.blc'
    if not blc.exists():
        blc.write_bytes(remote(f'cat {DEVICE_ROOT}/{block}/{block}.blc'))
    assert block_name(blc) == 'Bundle'
    records = h.parse_blc_records(u, blc, 'Persistent', target)
    assert len(records) == len(names), 'exact Android bundles not all in current Persistent index'
    out = OUT / 'android-prefabs'
    out.mkdir(exist_ok=True)
    for record in records:
        assert 0 < record.size <= 4 * 1024**2 and record.offset >= 0
        raw = remote(f'toybox dd if={DEVICE_ROOT}/{block}/{record.chunk}.chk bs=1 skip={record.offset} count={record.size} 2>/dev/null')
        assert len(raw) == record.size
        data = u.per_file_decrypt(raw, record.iv_seed) if record.encrypted else raw
        path = out / record.relative_path
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_bytes(data)
    receipt = dict(platform='Android', manifest_version=manifest['Version'], manifest_hash=manifest['Hash'],
        manifest_index_matches_device=True, assets=assets, records=[dataclasses.asdict(r) for r in records],
        captured_bytes=sum(r.size for r in records))
    (out / 'extraction.json').write_text(json.dumps(receipt, indent=2), encoding='utf-8')
    print('Android', len(records), 'prefab bundles', receipt['captured_bytes'], 'bytes')

def pc():
    manifest = json.loads((ROOT / 'research/current-inputs/Bundles/Windows/manifest.json').read_text(encoding='utf-8-sig'))
    assets, names = select(manifest, 'Windows')
    target = re.compile('(?:' + '|'.join(map(re.escape, names)) + ')', re.I)
    game = Path('D:/Arknights Endfield/Endfield_Data')
    roots = [('StreamingAssets', game / 'StreamingAssets/VFS'), ('Persistent', game / 'Persistent/VFS')]
    # Reuse the manifest only if its exact VFS record is present in this installation.
    manifest_records = {}
    selected = {}
    manifest_target = re.compile(r'Bundles/Windows/manifest\.hgmmap', re.I)
    for layer, root in roots:
        for block in ('1CDDBF1F', '7064D8E2'):
            blc = root / block / f'{block}.blc'
            if blc.exists():
                for record in h.parse_blc_records(u, blc, layer, target if block == '7064D8E2' else manifest_target):
                    (selected if block == '7064D8E2' else manifest_records)[record.relative_path.lower()] = record
    assert len(selected) == len(names), 'PC bundles do not match the cached manifest'
    record = next(iter(manifest_records.values()))
    chunks = h.build_chunk_index(roots)
    with chunks[record.chunk].open('rb') as f:
        f.seek(record.offset)
        raw = f.read(record.size)
    data = u.per_file_decrypt(raw, record.iv_seed) if record.encrypted else raw
    assert data == (ROOT / 'research/current-inputs/Bundles/Windows/manifest.hgmmap').read_bytes(), 'PC cached manifest is stale'
    out = OUT / 'pc-prefabs'
    out.mkdir(exist_ok=True)
    for record in selected.values():
        assert 0 < record.size <= 4 * 1024**2 and record.offset >= 0
        with chunks[record.chunk].open('rb') as f:
            f.seek(record.offset)
            raw = f.read(record.size)
        assert len(raw) == record.size
        data = u.per_file_decrypt(raw, record.iv_seed) if record.encrypted else raw
        path = out / record.relative_path
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_bytes(data)
    receipt = dict(platform='Windows', manifest_version=manifest['Version'], manifest_hash=manifest['Hash'],
        manifest_bytes_match_installation=True, assets=assets, records=[dataclasses.asdict(r) for r in selected.values()],
        captured_bytes=sum(r.size for r in selected.values()))
    (out / 'extraction.json').write_text(json.dumps(receipt, indent=2), encoding='utf-8')
    print('PC', len(selected), 'prefab bundles', receipt['captured_bytes'], 'bytes')

if __name__ == '__main__':
    if len(sys.argv) == 3:
        assert re.fullmatch(r'chr_\d{4}_[a-z0-9]+', sys.argv[2])
        CHARACTERS = [sys.argv[2]]
        OUT = OUT / sys.argv[2]
        OUT.mkdir(exist_ok=True)
    {'android': android, 'pc': pc}[sys.argv[1]]()
