from pathlib import Path
import re
import xml.etree.ElementTree as ET

root = Path(__file__).resolve().parents[3]
android = '{http://schemas.android.com/apk/res/android}'
xml = ET.parse(root / 'android/app/src/main/res/layout/activity_main.xml')
navigation = next(node for node in xml.iter() if node.get(android + 'id') == '@+id/page_navigation')
assert len(navigation) == 5
assert all(node.get(android + 'layout_width') != '0dp' and node.get(android + 'layout_weight') is None for node in navigation)
source = (root / 'android/app/src/main/java/dev/betterendfield/next/MainActivity.java').read_text(encoding='utf-8')
assert 'CUSTOM_MODEL_PAGE = 4' in source and 'ENHANCEMENT_PAGE = 2' in source and 'ABOUT_PAGE = 3' in source
assert 'THIRD_PARTY_PAGE = 5' not in source
assert 'new Intent(this, ThirdPartyModulesActivity.class)' not in source
sections = re.search(r'View\[\] sections = \{(.*?)\};', source, re.S).group(1)
assert re.findall(r'R.id.(\w+)', sections) == ['model_section', 'voice_section', 'enhancement_section', 'about_section', 'custom_model_section']
assert 'navigationScroll.addView(navigation' in source and 'tab.setMinimumWidth(dp(92))' in source
assert 'new LinearLayout.LayoutParams(0, -2, 1)' not in source[source.index('private void applyResponsiveShell()'):]
manifest = ET.parse(root / 'android/app/src/main/AndroidManifest.xml')
activity = next(node for node in manifest.iter('activity') if node.get(android + 'name') == '.ThirdPartyModuleActivity')
assert activity.get(android + 'exported') == 'false'
network = ET.parse(root / 'android/app/src/main/res/xml/module_network_security.xml')
assert network.getroot().find('base-config').get('cleartextTrafficPermitted') == 'false'
assert {node.text for node in network.iter('domain')} == {'127.0.0.1', 'localhost'}
sample = root / 'tools/ThirdPartyModules/echo/ui'
for filename in ('index.html', 'style.css', 'app.js'):
    assert (sample / filename).is_file()
print('PASS hidden third-party navigation: five scrollable tabs, retained internal Activity/runtime, loopback-only cleartext and complete web sample')
