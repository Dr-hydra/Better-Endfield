"""Single-variable Android normal-map experiment; does not alter the PC package."""
import copy
import json
import sys
from pathlib import Path

import numpy as np
from PIL import Image

ROOT = Path(__file__).resolve().parents[2]
sys.path.insert(0, str(ROOT / 'tools/CustomModel'))
from bem_v1 import read_package, write_package

source = ROOT / 'artifacts/BetterEndfield-win-x64/custom-model/endmin_in_casualwear.bem'
destination = Path(__file__).parent / 'endmin-normal-z-probe.bem'
manifest, payloads = read_package(source)
before = copy.deepcopy(manifest)
texture = next(t for t in manifest['textures'] if t['original_name'] == 'T_actor_endminf_cloth_01_N')
assert texture['format'] == 25 and not texture['srgb'] and texture['mips'] == 1
index = texture['payload']
assert sum(t['payload'] == index for t in manifest['textures']) == 1
image = Image.frombytes('RGBA', (texture['width'], texture['height']), payloads[index], 'bcn', (7, 'BC7'))
rgba = np.array(image)
assert float(rgba[:, :, 2].mean()) < 1, 'Expected the verified RG-only source normal map'
x = rgba[:, :, 0].astype(np.float32) * (2 / 255) - 1
y = rgba[:, :, 1].astype(np.float32) * (2 / 255) - 1
z = np.sqrt(np.maximum(0, 1 - x * x - y * y))
rgba[:, :, 2] = np.rint((z + 1) * 127.5).astype(np.uint8)
# R/G and alpha are preserved byte-for-byte from the decoded source.
texture['format'] = 4  # Unity TextureFormat.RGBA32, linear; no shader/color changes.
original_payloads = list(payloads)
payloads[index] = rgba.tobytes()
write_package(destination, manifest, payloads)
loaded, decoded = read_package(destination)
assert loaded == manifest
assert decoded == payloads
assert all(a == b for i, (a, b) in enumerate(zip(original_payloads, decoded)) if i != index)
expected = copy.deepcopy(before)
next(t for t in expected['textures'] if t['original_name'] == texture['original_name'])['format'] = 4
assert loaded == expected
report = dict(source=str(source), output=str(destination), changed_texture=texture['original_name'],
              rgba_mean=rgba.mean(axis=(0, 1)).tolist(), bytes=destination.stat().st_size,
              verification='Only selected normal texture payload and format changed; all other manifest/payload data identical')
(destination.parent / 'normal-z-probe-report.json').write_text(json.dumps(report, indent=2), encoding='utf-8')
print(json.dumps(report, ensure_ascii=False))
