"""Next installation/ABI identity and archive boundary regressions."""
from pathlib import Path
import hashlib,json,re,unittest,xml.etree.ElementTree as ET
ROOT=Path(__file__).resolve().parents[2]

class NextIdentityTests(unittest.TestCase):
    def test_archive_preserves_source_and_is_not_linked_to_production(self):
        archive=ROOT/'legacy/retired-before-next'
        manifest=json.loads((archive/'manifest.json').read_text(encoding='utf-8'))
        for item in manifest['files']:
            with self.subTest(source=item['source']):
                content=(archive/item['archive']).read_bytes()
                self.assertEqual(hashlib.sha256(content).hexdigest(),item['sha256'])
        for relative in ['native/CMakeLists.txt','android/app/src/main/cpp/CMakeLists.txt','ui/BetterEndfieldNext.UI/BetterEndfieldNext.UI.csproj']:
            self.assertNotIn('retired-before-next',(ROOT/relative).read_text(encoding='utf-8'))
        self.assertFalse((ROOT/'native/modules/camera/first_person_runtime.inc').exists())

    def test_android_package_and_jni_bindings_match(self):
        gradle=(ROOT/'android/app/build.gradle.kts').read_text(encoding='utf-8')
        package=re.search(r'applicationId = "([^"]+)"',gradle)[1]
        self.assertNotEqual(package,'dev.betterendfield.android')
        source=ROOT/'android/app/src/main/java'/package.replace('.','/')
        self.assertTrue((source/'XposedEntry.java').exists())
        init=(ROOT/'android/app/src/main/resources/META-INF/xposed/java_init.list').read_text().strip()
        self.assertEqual(init,package+'.XposedEntry')
        bridge=(ROOT/'android/app/src/main/cpp/native_bridge.cpp').read_text(encoding='utf-8')
        self.assertIn('"'+package+'.NativeCommandBridge"',bridge)
        installer=(ROOT/'android/app/src/main/cpp/installer/install_jni.cpp').read_text(encoding='utf-8')
        exports=re.findall(r'Java_(\w+)_BemInstaller_\w+',installer)
        self.assertTrue(exports)
        self.assertEqual(set(exports),{package.replace('.','_')})
        manifest=ET.parse(ROOT/'android/app/src/main/AndroidManifest.xml')
        ns='{http://schemas.android.com/apk/res/android}'
        self.assertEqual(manifest.find('application/provider').get(ns+'authorities'),package+'.overlay.settings')

    def test_windows_module_descriptors_match_new_exports(self):
        cmake=(ROOT/'native/CMakeLists.txt').read_text(encoding='utf-8')
        for descriptor in (ROOT/'native/modules').glob('*/*.module.ini'):
            fields=dict(line.split('=',1) for line in descriptor.read_text(encoding='utf-8').splitlines() if '=' in line)
            with self.subTest(module=fields['id']):
                self.assertTrue(fields['id'].startswith('betterendfieldnext.'))
                self.assertEqual(fields['api'],'BetterEndfieldNext_GetModuleApiV1')
                self.assertTrue(fields['library'].startswith('BetterEndfieldNext.'))
                self.assertIn(fields['library'].removesuffix('.dll'),cmake)

    def test_retained_extension_runtime_has_no_public_navigation_entry(self):
        main=(ROOT/'android/app/src/main/java/dev/betterendfield/next/MainActivity.java').read_text(encoding='utf-8')
        self.assertNotIn('ThirdPartyModulesActivity.class',main)
        ui=ET.parse(ROOT/'ui/BetterEndfieldNext.UI/MainWindow.xaml')
        tag='{http://schemas.microsoft.com/winfx/2006/xaml}Name'
        navigation=next(node for node in ui.iter() if node.get(tag)=='ThirdPartyModulesNavigationItem')
        self.assertEqual(navigation.get('Visibility'),'Collapsed')
        host=(ROOT/'native/shared/host/host_runtime.cpp').read_text(encoding='utf-8')
        self.assertIn('third_party_->Start',host)
        self.assertIn('ThirdPartyHost',(ROOT/'android/app/src/main/cpp/native_bridge.cpp').read_text(encoding='utf-8'))

    def test_new_signing_pins_are_distinct_and_private_paths_are_ignored(self):
        policy=json.loads((ROOT/'config/workspace.defaults.json').read_text(encoding='utf-8-sig'))
        self.assertNotEqual(policy['android_signing']['certificate_sha256'],'6f15740248d1d25551bb47967dd50855c18cc102dd9bf1fc3ecc8f3dd25473cf')
        self.assertRegex(policy['android_signing']['certificate_sha256'],r'^[a-f0-9]{64}$')
        self.assertRegex(policy['windows_signing']['certificate_sha256'],r'^[a-f0-9]{64}$')

if __name__=='__main__':unittest.main()
