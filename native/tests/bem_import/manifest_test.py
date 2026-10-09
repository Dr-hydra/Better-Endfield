"""Structural assertions, not an emulation of Android's Intent resolver."""
from pathlib import Path
import xml.etree.ElementTree as ET

ROOT = Path(__file__).resolve().parents[3]
ANDROID = '{http://schemas.android.com/apk/res/android}'
manifest = ET.parse(ROOT / 'android/app/src/main/AndroidManifest.xml').getroot()
activity = next(a for a in manifest.findall('application/activity')
                if a.get(ANDROID + 'name') == '.BemInstallActivity')
assert activity.get(ANDROID + 'exported') == 'true'
assert activity.get(ANDROID + 'launchMode') == 'singleTop'
filters = activity.findall('intent-filter')
views = [f for f in filters if f.find('action').get(ANDROID + 'name') == 'android.intent.action.VIEW']
sends = [f for f in filters if f.find('action').get(ANDROID + 'name') == 'android.intent.action.SEND']
assert len(views) == 2 and len(sends) == 1
for f in filters:
    assert any(c.get(ANDROID + 'name') == 'android.intent.category.DEFAULT' for c in f.findall('category'))
    assert not any(d.get(ANDROID + 'mimeType') in ('*/*', 'application/*') for d in f.findall('data'))
for f in views:
    assert {d.get(ANDROID + 'scheme') for d in f.findall('data') if d.get(ANDROID + 'scheme')} == {'content'}
    assert not any(d.get(ANDROID + 'pathPattern') for d in f.findall('data'))
expected_types = {'application/x-bem', 'application/vnd.betterendfield.bem', 'application/octet-stream', 'application/x-binary',
                  'application/zip', 'application/x-zip-compressed', 'application/x-zip'}
assert {d.get(ANDROID + 'mimeType') for d in sends[0].findall('data')} == expected_types
assert {d.get(ANDROID + 'mimeType') for d in views[0].findall('data') if d.get(ANDROID + 'mimeType')} == expected_types
assert not any(d.get(ANDROID + 'scheme') for d in sends[0].findall('data'))
assert not any(d.get(ANDROID + 'mimeType') for d in views[1].findall('data'))
assert {p.get(ANDROID + 'name') for p in manifest.findall('uses-permission')} == {'android.permission.INTERNET'}
main = next(a for a in manifest.findall('application/activity') if a.get(ANDROID + 'name') == '.MainActivity')
assert any(c.get(ANDROID+'name') == 'android.intent.category.LAUNCHER' for c in main.findall('intent-filter/category'))
# Both new layout and label must be well-formed; Android resource linking is a separate check.
ET.parse(ROOT / 'android/app/src/main/res/layout/activity_bem_install.xml')
strings = ET.parse(ROOT / 'android/app/src/main/res/values/strings.xml').getroot()
assert any(s.get('name') == 'bem_import_activity_label' for s in strings)
print('PASS BEM/ZIP manifest/XML structure: VIEW/SEND, MIME scopes, content-only, singleTop, launcher and no new storage permissions')
