#!/usr/bin/env python3
"""Optional source/API check, NOT a Gradle/AAPT/DEX/APK build.

Usage: python3 native/tests/bem_import/check_java.py --android-jar /path/to/android.jar
Uses real resource names and explicit compile-only FrameworkSettings signatures.
"""
import argparse
from pathlib import Path
import subprocess
import tempfile
import xml.etree.ElementTree as ET


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--android-jar', type=Path, required=True)
    args = parser.parse_args()
    if not args.android_jar.is_file():
        parser.error('android.jar does not exist')
    root = Path(__file__).resolve().parents[3]
    resources = root / 'android/app/src/main/res'
    symbols = {}
    for path in resources.rglob('*'):
        if not path.is_file():
            continue
        kind = path.parent.name.split('-')[0]
        if kind != 'values':
            symbols.setdefault(kind, set()).add(path.stem.replace('.', '_'))
        if path.suffix != '.xml':
            continue
        xml = ET.parse(path).getroot()
        if xml.tag == 'resources':
            for child in xml:
                name = child.get('name')
                if name and child.tag not in ('public', 'declare-styleable'):
                    symbols.setdefault(child.get('type', child.tag), set()).add(name.replace('.', '_'))
        for item in xml.iter():
            for value in item.attrib.values():
                if value.startswith('@+id/'):
                    symbols.setdefault('id', set()).add(value[5:])
    java = root / 'android/app/src/main/java/dev/betterendfield/android'
    names = ['BemInstallActivity', 'BemInstallPage', 'BemInstaller', 'BemImportRequest', 'BemImportStream',
             'BemOptions', 'BemParameters', 'AstcSupport', 'BemInstalledResources',
             'ThirdPartyModulePackage', 'ThirdPartyModuleStore', 'ThirdPartyModuleActivity', 'ThirdPartyModulesPage']
    sources = [java / (name + '.java') for name in names]
    sources.append(root / 'android/app/src/androidTest/java/dev/betterendfield/android/BemInstallerTest.java')
    with tempfile.TemporaryDirectory(prefix='bem-java-check-') as temporary:
        build = Path(temporary)
        stub = build / 'stubs/dev/betterendfield/android'
        stub.mkdir(parents=True)
        text = 'package dev.betterendfield.android; public final class R {\n'
        for kind, values in sorted(symbols.items()):
            text += 'public static final class ' + kind + ' {\n'
            for index, name in enumerate(sorted(values), 1):
                text += f'public static final int {name}={index};\n'
            text += '}\n'
        text += '}\n'
        (stub / 'R.java').write_text(text, encoding='utf-8')
        (stub / 'FrameworkSettings.java').write_text('''package dev.betterendfield.android;
// Compile-only fixture, never packaged or executed.
final class FrameworkSettings {
 static android.content.SharedPreferences open(android.content.Context c) {throw new UnsupportedOperationException();}
 static boolean publishBem(java.io.File f,String n) {throw new UnsupportedOperationException();}
 static boolean removeBem(String n) {throw new UnsupportedOperationException();}
 static boolean isConnected() {throw new UnsupportedOperationException();}
 static void awaitConnection() {throw new UnsupportedOperationException();}
 static void awaitThirdPartyConnection() {throw new UnsupportedOperationException();}
 static void publishThirdParty(java.io.File file,String name) throws java.io.IOException {throw new UnsupportedOperationException();}
 static boolean removeThirdParty(String name) {throw new UnsupportedOperationException();}
}
''', encoding='utf-8')
        subprocess.run(['javac', '--release', '17', '-encoding', 'UTF-8', '-Xlint:unchecked',
                        '-cp', str(args.android_jar.resolve()), '-d', str(build / 'classes'),
                        *map(str, sources), str(stub / 'R.java'), str(stub / 'FrameworkSettings.java')], check=True)
    print('PASS BEM Java/API source compile (real resource names, compile-only FrameworkSettings; NOT an APK build)')


if __name__ == '__main__':
    main()
