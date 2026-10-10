"""Exercise production page routing after extension removal; keep Workshop wiring."""
from pathlib import Path
import argparse
import subprocess
import xml.etree.ElementTree as ET


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--output", type=Path, required=True)
    args = parser.parse_args()
    android = Path(__file__).resolve().parents[3]
    output = args.output.resolve()
    activity = (android / "app/src/main/java/dev/betterendfield/next/MainActivity.java").read_text(encoding="utf-8")
    layout = ET.parse(android / "app/src/main/res/layout/activity_main.xml")
    manifest = ET.parse(android / "app/src/main/AndroidManifest.xml")
    ns = "{http://schemas.android.com/apk/res/android}"
    ids = {node.get(ns + "id") for node in layout.iter()}
    assert "@+id/show_custom_model_button" in ids and "@+id/custom_model_section" in ids
    assert not any("ThirdParty" in node.get(ns + "name", "") for node in manifest.iter("activity"))
    assert 'enhancementButton("创意工坊")' in activity and 'Intent.CATEGORY_BROWSABLE' in activity
    assert 'https://146.235.16.65:8443/endfield/' in activity
    assert 'android.widget.Toast.makeText(this, "无法打开创意工坊"' in activity
    route = activity[activity.index("String requestedPage ="):activity.index("View bemContent =")]
    route = route.replace("getIntent().getStringExtra(EXTRA_PAGE)", "requested")
    route_source = r'''package dev.betterendfield.next;
        import android.os.Bundle;
        public class MainPageRouteHostTest {
            static int CUSTOM_MODEL_PAGE=4,ENHANCEMENT_PAGE=2,ABOUT_PAGE=3;
            static int route(String requested,Bundle savedInstanceState){int currentPage;
        ''' + route + r'''
                return currentPage;
            }
            static void check(String request,Integer saved,int page){Bundle b=null;if(saved!=null){b=new Bundle();b.putInt("page",saved);}if(route(request,b)!=page)throw new AssertionError(request+"/"+saved);}
            public static void main(String[] args){
                check("third_party_modules",null,0);
                check("third_party_modules",2,2);
                check(null,5,2);
                check(null,-1,0);
                check(null,100,2);
                check("custom_model",null,4);
                check("enhancement",null,2);
                check("about",null,3);
                check(null,4,4);
                check(null,null,0);
                System.out.println("MainPageRouteHostTest: 10 removed-entry/restore route checks passed");
            }
        }'''
    sources = {
        "android/os/Bundle.java": "package android.os; import java.util.*; public class Bundle {private Map<String,Integer> v=new HashMap<>(); public void putInt(String k,int x){v.put(k,x);} public int getInt(String k,int d){return v.getOrDefault(k,d);}}",
        "dev/betterendfield/next/MainPageRouteHostTest.java": route_source,
    }
    files = []
    for name, content in sources.items():
        path = output / "src" / name
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_text(content, encoding="utf-8")
        files.append(str(path))
    classes = output / "classes"
    classes.mkdir(parents=True, exist_ok=True)
    subprocess.run(["javac", "-encoding", "UTF-8", "--release", "17", "-d", str(classes), *files], check=True)
    subprocess.run(["java", "-cp", str(classes), "dev.betterendfield.next.MainPageRouteHostTest"], check=True)
    print("Manifest/top-level tabs/workshop destination checks passed")


if __name__ == "__main__":
    main()
