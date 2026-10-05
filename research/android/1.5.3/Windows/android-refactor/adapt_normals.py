from pathlib import Path
import json, sys
import numpy as np
from PIL import Image
sys.path.insert(0, 'tools/CustomModel')
import bem_v1 as bem

def decode_bc7_to_rgba(raw, w, h, mips):
    out = bytearray()
    pos = 0
    for level in range(mips):
        lw, lh = max(w >> level, 1), max(h >> level, 1)
        size = ((lw + 3) // 4) * ((lh + 3) // 4) * 16
        part = raw[pos:pos + size]
        pos += size
        im = Image.frombytes('RGBA', (lw, lh), part, 'bcn', (7, 'BC7'))
        arr = np.asarray(im, dtype=np.float32)
        x, y = arr[:, :, 0] * 2 / 255 - 1, arr[:, :, 1] * 2 / 255 - 1
        z = np.sqrt(np.maximum(0, 1 - x * x - y * y))
        fixed = np.empty_like(arr, dtype=np.uint8)
        fixed[:, :, 0] = np.rint(arr[:, :, 0]).astype(np.uint8)
        fixed[:, :, 1] = np.rint(arr[:, :, 1]).astype(np.uint8)
        fixed[:, :, 2] = np.rint((z + 1) * 127.5).astype(np.uint8)
        fixed[:, :, 3] = np.rint(arr[:, :, 3]).astype(np.uint8)
        out.extend(fixed.tobytes())
    if pos != len(raw):
        raise ValueError(f'BC7 payload trailing bytes: {len(raw) - pos}')
    return bytes(out)

def bump_names(catalog):
    data = json.loads(Path(catalog).read_text(encoding='utf-8-sig'))
    names = set()
    for comp in data['components'].values():
        for props in comp.get('material_texture_properties', []):
            for item in props:
                if item.get('property') == '_BumpMap':
                    names.add(item['name'])
    return names

def adapt(src, dst, catalog):
    manifest, payloads = bem.read_package(Path(src))
    names = bump_names(catalog)
    changed = []
    for texture in manifest['textures']:
        if texture['original_name'] not in names or texture['format'] != 25:
            continue
        old = payloads[texture['payload']]
        new = decode_bc7_to_rgba(old, texture['width'], texture['height'], texture['mips'])
        # BEM's per-texture resident cap is 64 MiB. Keep oversized normal maps
        # in their verified BC7 form; smaller maps use explicit RGBA32 so the
        # Android sampler receives a reconstructed positive Z channel.
        if len(new) > 64 * 1024 * 1024:
            continue
        texture['format'] = 4
        texture['srgb'] = False
        try:
            payload_id = payloads.index(new)
        except ValueError:
            payloads.append(new)
            payload_id = len(payloads) - 1
        texture['payload'] = payload_id
        changed.append((texture['original_name'], len(old), len(new)))
    bem.write_package(Path(dst), manifest, payloads)
    print(dst, 'changed', changed)

adapt('artifacts/android-refactor/pelica-default.bem', 'artifacts/android-refactor/pelica-default-normal.bem', 'tools/CustomModel/catalog/pelica-pc.json')
adapt('artifacts/android-refactor/zhuangfangyi-default.bem', 'artifacts/android-refactor/zhuangfangyi-default-normal.bem', 'tools/CustomModel/catalog/chr_0030_zhuangfy.json')
adapt('artifacts/BetterEndfield-win-x64/custom-model/gilberta-two-appearances.bem', 'artifacts/android-refactor/gilberta-two-appearances-normal.bem', 'tools/CustomModel/catalog/chr_0013_aglina.json')
