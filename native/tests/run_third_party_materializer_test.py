import argparse
import os
from pathlib import Path
import subprocess
import tempfile
import urllib.request
def main():
    parser=argparse.ArgumentParser();parser.add_argument('--json-jar',type=Path);args=parser.parse_args()
    root=Path(__file__).resolve().parents[2]
    with tempfile.TemporaryDirectory(prefix='third-party-java-') as directory:
        build=Path(directory);jar=args.json_jar or build/'json.jar'
        if args.json_jar is None:urllib.request.urlretrieve('https://repo.maven.apache.org/maven2/org/json/json/20240303/json-20240303.jar',jar)
        context=build/'android/content/Context.java';context.parent.mkdir(parents=True);context.write_text('package android.content;public abstract class Context {public abstract java.io.File getFilesDir();}',encoding='utf-8')
        system=build/'android/system/Os.java';system.parent.mkdir(parents=True);system.write_text('package android.system; public class Os {public static void rename(String a,String b) throws java.io.IOException {java.nio.file.Files.move(java.nio.file.Path.of(a),java.nio.file.Path.of(b),java.nio.file.StandardCopyOption.REPLACE_EXISTING);}}',encoding='utf-8')
        sources=[context,system,root/'android/app/src/main/java/dev/betterendfield/next/ThirdPartyRuntimeMaterializer.java',Path(__file__).with_name('ThirdPartyMaterializerTest.java')]
        classes=build/'classes'
        subprocess.run(['javac','--release','17','-encoding','UTF-8','-cp',str(jar),'-d',str(classes),*map(str,sources)],check=True)
        subprocess.run(['java','-cp',os.pathsep.join((str(classes),str(jar))),'dev.betterendfield.next.ThirdPartyMaterializerTest'],check=True)
if __name__=='__main__':main()
