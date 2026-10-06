"""Check the migrated Android module manager's real Activity wiring on a JVM."""
from pathlib import Path
import argparse
import subprocess
import xml.etree.ElementTree as ET


STUBS = {
    "android/content/Intent.java": "package android.content; public class Intent {}",
    "android/util/DisplayMetrics.java": "package android.util; public class DisplayMetrics {public float density=1;} ",
    "android/content/res/Resources.java": "package android.content.res; public class Resources {public android.util.DisplayMetrics getDisplayMetrics(){return new android.util.DisplayMetrics();}}",
    "android/os/Bundle.java": "package android.os; import java.util.*; public class Bundle {private Map<String,Integer> v=new HashMap<>(); public void putInt(String k,int x){v.put(k,x);} public int getInt(String k,int d){return v.getOrDefault(k,d);}}",
    "android/os/Build.java": "package android.os; public class Build {public static class VERSION {public static int SDK_INT=37;}}",
    "android/graphics/Insets.java": "package android.graphics; public class Insets {public int left,top,right,bottom;}",
    "android/view/Gravity.java": "package android.view; public class Gravity {public static int CENTER_HORIZONTAL=1;}",
    "android/view/WindowInsets.java": "package android.view; public class WindowInsets {public static class Type {public static int systemBars(){return 1;}public static int displayCutout(){return 2;}public static int ime(){return 4;}}public android.graphics.Insets getInsets(int v){return new android.graphics.Insets();}public int getSystemWindowInsetLeft(){return 0;}public int getSystemWindowInsetTop(){return 0;}public int getSystemWindowInsetRight(){return 0;}public int getSystemWindowInsetBottom(){return 0;}}",
    "android/view/View.java": r"""package android.view;
        public class View {
            public interface Click {void onClick(View v);}
            public interface Apply {WindowInsets onApplyWindowInsets(View v,WindowInsets i);}
            public interface Layout {void onLayout(View v,int l,int t,int r,int b,int ol,int ot,int or,int ob);}
            public Object params; public Click click;
            public void setPadding(int l,int t,int r,int b){}
            public void setBackgroundColor(int c){}
            public void setOnClickListener(Click c){click=c;}
            public void setOnApplyWindowInsetsListener(Apply a){}
            public void addOnLayoutChangeListener(Layout l){}
            public void setLayoutParams(Object p){params=p;}public Object getLayoutParams(){return params;}
            public int getPaddingLeft(){return 0;}public int getPaddingRight(){return 0;}
            public boolean post(Runnable r){r.run();return true;}
            public void requestApplyInsets(){}
        }""",
    "android/widget/LinearLayout.java": r"""package android.widget;
        import java.util.*;import android.view.View;
        public class LinearLayout extends View {
            public static int VERTICAL=1;
            public final List<View> children=new ArrayList<>();
            public LinearLayout(android.app.Activity a){}
            public void setOrientation(int v){}
            public void addView(View v,Object p){v.params=p;children.add(v);}
        }""",
    "android/widget/FrameLayout.java": "package android.widget; public class FrameLayout {public static class LayoutParams {public int width,height,gravity;public LayoutParams(int w,int h,int g){width=w;height=h;gravity=g;}}}",
    "android/widget/ScrollView.java": r"""package android.widget;
        import android.view.View;
        public class ScrollView extends View {
            public View child;public int y;
            public ScrollView(android.app.Activity a){}
            public void setFillViewport(boolean b){}
            public void addView(View v,Object p){v.params=p;child=v;}
            public void scrollTo(int x,int y){this.y=y;}public int getScrollY(){return y;}
        }""",
    "android/widget/Button.java": "package android.widget; public class Button extends android.view.View {public String text; public Button(android.app.Activity a){}public void setText(String v){text=v;}public void setAllCaps(boolean b){}}",
    "android/app/Activity.java": r"""package android.app;
        public class Activity {
            public android.view.View content;public boolean finished;public String title;
            protected void onCreate(android.os.Bundle b){}protected void onResume(){}
            protected void onActivityResult(int request,int result,android.content.Intent data){}
            protected void onDestroy(){}protected void onSaveInstanceState(android.os.Bundle b){}
            public void setTitle(String s){title=s;}public void setContentView(android.view.View v){content=v;}
            public int getColor(int id){return id;}public void finish(){finished=true;}
            public android.content.res.Resources getResources(){return new android.content.res.Resources();}
        }""",
    "dev/betterendfield/android/R.java": "package dev.betterendfield.android; public class R {public static class color {public static int app_background=7;}}",
    "dev/betterendfield/android/SectionCard.java": "package dev.betterendfield.android; public class SectionCard {public static Object stacked(android.app.Activity a,int m){return new Object();}}",
    "dev/betterendfield/android/ThirdPartyModulesPage.java": r"""package dev.betterendfield.android;
        public class ThirdPartyModulesPage {
            static ThirdPartyModulesPage last;public android.app.Activity owner;
            public int renders,results,request,result,closed;public android.content.Intent data;
            ThirdPartyModulesPage(android.app.Activity a,android.widget.LinearLayout root){owner=a;last=this;}
            void render(){renders++;}
            void onActivityResult(int request,int result,android.content.Intent data){this.request=request;this.result=result;this.data=data;results++;}
            void close(){closed++;}
        }""",
    "dev/betterendfield/android/EnhancementNavigationHostTest.java": r"""package dev.betterendfield.android;
        import android.os.Bundle;import android.content.Intent;import android.widget.*;
        public class EnhancementNavigationHostTest {
            static int checks;static void check(boolean b,String m){checks++;if(!b)throw new AssertionError(m);}
            public static void main(String[] args){
                ThirdPartyModulesActivity activity=new ThirdPartyModulesActivity();Bundle state=new Bundle();state.putInt("scroll_y",120);
                activity.onCreate(state);ThirdPartyModulesPage page=ThirdPartyModulesPage.last;
                check(page.owner==activity,"module picker is owned by manager Activity");
                ScrollView scroll=(ScrollView)activity.content;
                check(scroll.getScrollY()==120,"manager restores scroll position");
                activity.onResume();activity.onResume();check(page.renders==2,"manager refreshes on every resume, including return from module UI");
                Intent picked=new Intent();activity.onActivityResult(107,-1,picked);
                check(page.results==1&&page.request==107&&page.result==-1&&page.data==picked,"original import result forwarded unchanged");
                activity.onActivityResult(107,0,null);check(page.results==2&&page.data==null,"cancelled picker forwarded to existing page filtering");
                Bundle saved=new Bundle();scroll.scrollTo(0,200);activity.onSaveInstanceState(saved);
                check(saved.getInt("scroll_y",0)==200,"manager saves current scroll position");
                LinearLayout body=(LinearLayout)scroll.child;Button back=(Button)body.children.get(0);
                check(back.text.equals("‹ 返回增强功能"),"manager return identifies enhancement page");
                back.click.onClick(back);check(activity.finished,"manager return finishes onto existing enhancement page");
                activity.onDestroy();check(page.closed==1,"manager closes worker on destruction");
                System.out.println("EnhancementNavigationHostTest: "+checks+" manager navigation/import/lifecycle checks passed");
            }
        }""",
}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--output", type=Path, required=True)
    args = parser.parse_args()
    android = Path(__file__).resolve().parents[3]
    output = args.output.resolve()
    main_java = android / "app/src/main/java/dev/betterendfield/android"
    activity = (main_java / "MainActivity.java").read_text(encoding="utf-8")
    layout = ET.parse(android / "app/src/main/res/layout/activity_main.xml")
    manifest = ET.parse(android / "app/src/main/AndroidManifest.xml")
    ns = "{http://schemas.android.com/apk/res/android}"
    ids = {node.get(ns + "id") for node in layout.iter()}
    assert "@+id/show_third_party_button" not in ids and "@+id/third_party_section" not in ids
    assert "@+id/show_custom_model_button" in ids and "@+id/custom_model_section" in ids
    managers = [n for n in manifest.iter("activity") if n.get(ns + "name") == ".ThirdPartyModulesActivity"]
    assert len(managers) == 1 and managers[0].get(ns + "exported") == "false"
    assert 'new Intent(this, ThirdPartyModulesActivity.class)' in activity
    assert 'enhancementButton("创意工坊")' in activity and 'Intent.CATEGORY_BROWSABLE' in activity
    assert 'https://146.235.16.65:8443/endfield/' in activity
    assert 'android.widget.Toast.makeText(this, "无法打开创意工坊"' in activity

    # Compile and exercise the production page-resolution block itself, including
    # old saved page 5 and activity recreation after a consumed legacy deep link.
    route = activity[activity.index("String requestedPage ="):activity.index("View bemContent =")]
    route = route.replace("getIntent().getStringExtra(EXTRA_PAGE)", "requested")
    route_source = r"""package dev.betterendfield.android;
        import android.os.Bundle;
        public class MainPageRouteHostTest {
            static int CUSTOM_MODEL_PAGE=4,ENHANCEMENT_PAGE=2,ABOUT_PAGE=3;
            static int[] route(String requested,Bundle savedInstanceState){int currentPage;
        """ + route + r"""
                return new int[]{currentPage,openThirdParty?1:0};
            }
            static void check(String request,Integer saved,int page,boolean open){Bundle b=null;if(saved!=null){b=new Bundle();b.putInt("page",saved);}int[] r=route(request,b);if(r[0]!=page||r[1]!=(open?1:0))throw new AssertionError(request+"/"+saved);}
            public static void main(String[] args){
                check("third_party_modules",null,2,true);
                check("third_party_modules",2,2,false);
                check(null,5,2,true);
                check(null,-1,0,false);
                check(null,100,2,false);
                check("custom_model",null,4,false);
                check("enhancement",null,2,false);
                check("about",null,3,false);
                check(null,4,4,false);
                check(null,null,0,false);
                System.out.println("MainPageRouteHostTest: 10 legacy/deep-link/restore route checks passed");
            }
        }"""
    stubs = dict(STUBS)
    stubs["dev/betterendfield/android/MainPageRouteHostTest.java"] = route_source
    files = []
    for name, code in stubs.items():
        path = output / "sources" / name
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_text(code, encoding="utf-8")
        files.append(str(path))
    classes = output / "classes"
    classes.mkdir(parents=True, exist_ok=True)
    files.append(str(main_java / "ThirdPartyModulesActivity.java"))
    subprocess.run(["javac", "-encoding", "UTF-8", "--release", "17", "-d", str(classes), *files], check=True)
    for test in ("MainPageRouteHostTest", "EnhancementNavigationHostTest"):
        subprocess.run(["java", "-cp", str(classes), "dev.betterendfield.android." + test], check=True)
    print("Manifest/top-level tabs/workshop destination checks passed")


if __name__ == "__main__":
    main()
