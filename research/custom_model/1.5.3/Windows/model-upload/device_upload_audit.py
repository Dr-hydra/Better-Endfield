import json
import re
import subprocess
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
sys.path.insert(0, str(ROOT / 'tools/CustomModel'))
import bem_v1 as v1
import bem_upload_report

adb = ROOT / 'tools/android-toolchain/sdk/platform-tools/adb.exe'
remote = '/data/user/0/dev.betterendfield.android/files/bem-installed/728e0f84-93f5-49c1-8671-0e99961003cd/installed.bem'
def read_prefix(count):
    return subprocess.check_output([str(adb), 'exec-out', 'su', '-c', f'head -c {count} {remote}'])
raw_header = read_prefix(v1.HEADER.size)
header = v1.HEADER.unpack(raw_header)
assert header[0] == v1.MAGIC and header[1] == 1 and header[5] <= 4 * 1024**2 and header[6] <= 16384
extent = int(subprocess.check_output([str(adb), 'exec-out', 'su', '-c', f'stat -c %s {remote}']))
assert extent == header[4]
total = v1.HEADER.size + header[5] + header[6] * v1.ENTRY.size
raw = read_prefix(total)
assert len(raw) == total and raw[:v1.HEADER.size] == raw_header
manifest = json.loads(raw[v1.HEADER.size:v1.HEADER.size + header[5]].decode('utf-8'))
v1.validate_manifest(manifest, header[6], header[2])
directory = list(v1.ENTRY.iter_unpack(raw[v1.HEADER.size + header[5]:]))
Path(__file__).with_name('device-aglina-metadata-prefix.bin').write_bytes(raw)
report = bem_upload_report.report_metadata('android:' + remote, manifest, header, directory)
Path(__file__).with_name('device-aglina-default-report.json').write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
print(json.dumps({k: v for k, v in report.items() if k not in ('textures', 'selection', 'path')}, indent=2))

log = Path(__file__).with_name('device-diagnostics.log').read_text(encoding='utf-8-sig')
# Only the first world/UI pair is audited here. Format identifiers in the
# captured package establish the byte count; no driver residency is inferred.
pattern = re.compile(r'built t=(\S+) (\d+)x(\d+) mips=(\d+) (\S+) (sRGB|linear) graphicsFormat=(\d+)')
start = log.index('UI donor loaded: chr_0013_aglina_uimodel')
world_end = log.index('Resource committed: chr_0013_aglina_postmodel', start)
ui_end = log.index('Resource committed: chr_0013_aglina_uimodel', world_end)
segments = [log[start:world_end], log[world_end:ui_end]]
totals = []
for segment in segments:
    uploads = []
    for name, w, h, mips, _, color, _ in pattern.findall(segment):
        matches = [t for t in manifest['textures'] if t['original_name'] == name and
                   (t['width'], t['height'], t['mips'], t['srgb']) == (int(w), int(h), int(mips), color == 'sRGB')]
        sizes = {directory[t['payload']][4] for t in matches}
        assert len(sizes) == 1, (name, sizes)
        uploads.append(dict(name=name, bytes=sizes.pop()))
    totals.append(dict(texture_submissions=len(uploads), submitted_payload_bytes=sum(t['bytes'] for t in uploads), uploads=uploads))
assert totals[0] == totals[1]
Path(__file__).with_name('device-world-ui-duplicate-audit.json').write_text(json.dumps(totals, indent=2), encoding='utf-8')
print('Captured world/UI upload batches:', [(r['texture_submissions'], r['submitted_payload_bytes']) for r in totals])
