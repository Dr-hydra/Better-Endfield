#!/usr/bin/env python3
"""Execute actual BEM state code on the host; adapters cover Android storage APIs.

This is not an Android UI, native codec or APK execution test. Requires JDK 17+
and org.json (--json-jar, otherwise downloaded into the temporary build folder).
"""
import argparse
from pathlib import Path
import subprocess
import tempfile
import urllib.request


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--json-jar', type=Path)
    args = parser.parse_args()
    root = Path(__file__).resolve().parents[3]
    java = root / 'android/app/src/main/java/dev/betterendfield/android'
    stubs = {
        'android/content/SharedPreferences.java': '''package android.content;
public interface SharedPreferences {String getString(String key,String fallback);Editor edit();
interface Editor {Editor putString(String key,String value);boolean commit();}}''',
        'android/content/Context.java': '''package android.content;
public abstract class Context {public static final int MODE_PRIVATE=0;
public abstract java.io.File getFilesDir();public abstract Context getApplicationContext();
public abstract SharedPreferences getSharedPreferences(String name,int mode);
public ContentResolver getContentResolver(){throw new UnsupportedOperationException();}
public android.content.res.AssetManager getAssets(){throw new UnsupportedOperationException();}}''',
        'android/content/ContentResolver.java': '''package android.content;
public class ContentResolver {public java.io.InputStream openInputStream(android.net.Uri uri){throw new UnsupportedOperationException();}}''',
        'android/content/res/AssetManager.java': '''package android.content.res;
public class AssetManager {public java.io.InputStream open(String name){throw new UnsupportedOperationException();}}''',
        'android/net/Uri.java': 'package android.net; public class Uri {}',
        'android/os/SystemClock.java': '''package android.os;
public class SystemClock {public static long elapsedRealtime(){return System.nanoTime()/1000000;}}''',
        'android/util/Log.java': '''package android.util;
public class Log {public static int e(String tag,String message,Throwable error){error.printStackTrace();return 0;}}''',
        'android/util/AtomicFile.java': '''package android.util;
public class AtomicFile {
private final java.io.File base,temporary;
public AtomicFile(java.io.File file){base=file;temporary=new java.io.File(file+".new");}
public java.io.FileOutputStream startWrite() throws java.io.IOException {return new java.io.FileOutputStream(temporary);}
public void finishWrite(java.io.FileOutputStream stream) throws java.io.IOException {stream.close();java.nio.file.Files.move(temporary.toPath(),base.toPath(),java.nio.file.StandardCopyOption.REPLACE_EXISTING);}
public void failWrite(java.io.FileOutputStream stream) throws java.io.IOException {stream.close();temporary.delete();}
public byte[] readFully() throws java.io.IOException {return java.nio.file.Files.readAllBytes(base.toPath());}}''',
        'android/system/Os.java': '''package android.system;
public class Os {public static void rename(String from,String to) throws java.io.IOException {
java.nio.file.Files.move(java.nio.file.Path.of(from),java.nio.file.Path.of(to),java.nio.file.StandardCopyOption.REPLACE_EXISTING);}}''',
        'dev/betterendfield/android/FrameworkSettings.java': '''package dev.betterendfield.android;
final class FrameworkSettings {
static android.content.SharedPreferences open(android.content.Context app){return app.getSharedPreferences("module_settings",0);}
static boolean publishBem(java.io.File file,String name){throw new UnsupportedOperationException("Native import is outside host test scope");}
static boolean removeBem(String name){return true;}}''',
        'dev/betterendfield/android/AstcSupport.java': '''package dev.betterendfield.android;
final class AstcSupport {static boolean available(){throw new UnsupportedOperationException();}}''',
        'dev/betterendfield/android/BemImportRequest.java': '''package dev.betterendfield.android;
final class BemImportRequest {static android.net.Uri requireContentUri(android.net.Uri uri){throw new UnsupportedOperationException();}}''',
    }
    with tempfile.TemporaryDirectory(prefix='bem-state-build-') as directory:
        build = Path(directory)
        jar = args.json_jar
        if jar is None:
            jar = build / 'json.jar'
            urllib.request.urlretrieve('https://repo.maven.apache.org/maven2/org/json/json/20240303/json-20240303.jar', jar)
        if not jar.is_file():
            parser.error('org.json jar does not exist')
        sources = [java / f'{name}.java' for name in ('BemInstaller', 'BemOptions', 'BemInstalledResources', 'BemImportStream')]
        sources.append(Path(__file__).with_name('BemPackageStateTest.java'))
        for name, text in stubs.items():
            file = build / 'stubs' / name
            file.parent.mkdir(parents=True, exist_ok=True)
            file.write_text(text, encoding='utf-8')
            sources.append(file)
        classes = build / 'classes'
        subprocess.run(['javac', '--release', '17', '-encoding', 'UTF-8', '-cp', str(jar.resolve()), '-d', str(classes), *map(str, sources)], check=True)
        import os
        subprocess.run(['java', '-cp', os.pathsep.join((str(classes), str(jar.resolve()))), 'dev.betterendfield.android.BemPackageStateTest'], check=True)


if __name__ == '__main__':
    main()
