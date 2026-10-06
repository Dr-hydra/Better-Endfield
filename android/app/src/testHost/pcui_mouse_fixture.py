"""Run the real PC mouse lifecycle/transport bridge against a small Android host.

The fixture models asynchronous capture grants. It deliberately never implements
the application's lifecycle or event routing rules; those come from main Java.
"""
from pathlib import Path
import argparse
import subprocess


STUBS = {
    "android/os/Bundle.java": "package android.os; public class Bundle {}",
    "android/os/Looper.java": "package android.os; public class Looper { public static Looper getMainLooper(){return new Looper();} }",
    "android/os/SystemClock.java": "package android.os; public class SystemClock { public static long time; public static long uptimeMillis(){return time;} }",
    "android/os/Handler.java": r"""package android.os;
        import java.util.ArrayList;
        public class Handler {
            private static final ArrayList<Runnable> tasks = new ArrayList<>();
            public Handler(Looper looper) {}
            public boolean post(Runnable r){tasks.add(r);return true;}
            public boolean postDelayed(Runnable r,long delay){tasks.add(r);return true;}
            public void removeCallbacks(Runnable r){tasks.removeIf(t->t==r);}
            public static void runNext(){if(!tasks.isEmpty())tasks.remove(0).run();}
            public static void reset(){tasks.clear();}
        }""",
    "android/app/Application.java": r"""package android.app;
        import android.os.Bundle;
        public class Application {
            public interface ActivityLifecycleCallbacks {
                void onActivityResumed(Activity a); void onActivityPaused(Activity a);
                void onActivityDestroyed(Activity a); void onActivityCreated(Activity a,Bundle b);
                void onActivityStarted(Activity a); void onActivityStopped(Activity a);
                void onActivitySaveInstanceState(Activity a,Bundle b);
            }
            public void registerActivityLifecycleCallbacks(ActivityLifecycleCallbacks callbacks){}
        }""",
    "android/app/Activity.java": r"""package android.app;
        import android.view.Window;
        public class Activity {
            public final Window window = new Window(); public boolean focus=true;
            public Window getWindow(){return window;} public boolean hasWindowFocus(){return focus;}
        }""",
    "android/view/Window.java": r"""package android.view;
        public class Window {public View decor; public View getDecorView(){return decor;} }""",
    "android/view/View.java": r"""package android.view;
        public class View {
            public boolean attached=true,shown=true,captured,throwRequest;
            public int requests,releases,width=1920,height=1080;
            public View focus=this;
            public boolean hasPointerCapture(){return captured;}
            public boolean isAttachedToWindow(){return attached;}
            public boolean isShown(){return shown;}
            public View findFocus(){return focus;}
            public int getWidth(){return width;} public int getHeight(){return height;}
            public void requestPointerCapture(){requests++;if(throwRequest)throw new IllegalStateException("capture unavailable");}
            // Release is asynchronous, like ViewRootImpl's actual platform request.
            public void releasePointerCapture(){releases++;}
        }""",
    "android/view/ViewGroup.java": r"""package android.view;
        import java.util.ArrayList;
        public class ViewGroup extends View {
            public final ArrayList<View> children=new ArrayList<>();
            public int getChildCount(){return children.size();}
            public View getChildAt(int i){return children.get(i);}
        }""",
    "android/view/InputEvent.java": "package android.view; public class InputEvent {}",
    "android/view/InputDevice.java": r"""package android.view;
        public class InputDevice {
            public static final int SOURCE_MOUSE=0x2002,SOURCE_MOUSE_RELATIVE=0x20004;
            public static boolean mouse=true;
            public static int[] getDeviceIds(){return mouse?new int[]{7}:new int[0];}
            public static InputDevice getDevice(int id){return new InputDevice();}
            public int getSources(){return SOURCE_MOUSE;}
        }""",
    "android/view/MotionEvent.java": r"""package android.view;
        public class MotionEvent extends InputEvent {
            public static final int ACTION_DOWN=0,ACTION_UP=1,ACTION_MOVE=2,ACTION_CANCEL=3,
                ACTION_HOVER_MOVE=7,ACTION_SCROLL=8,ACTION_BUTTON_PRESS=11,ACTION_BUTTON_RELEASE=12;
            public static class PointerProperties { public int id,toolType; }
            public static class PointerCoords { public float x,y; }
            public int source=InputDevice.SOURCE_MOUSE_RELATIVE,action,buttons,actionButton,
                meta=5,device=7,edge=3,flags=4;
            public float x,y,scrollX,scrollY,xp=1.5f,yp=2.5f;
            public float[][] history=new float[0][];
            public boolean recycled;
            public long down=10,time=20;
            public static MotionEvent obtain(MotionEvent v){
                MotionEvent e=new MotionEvent();e.source=v.source;e.action=v.action;
                e.buttons=v.buttons;e.actionButton=v.actionButton;e.meta=v.meta;e.device=v.device;
                e.edge=v.edge;e.flags=v.flags;e.x=v.x;e.y=v.y;e.scrollX=v.scrollX;e.scrollY=v.scrollY;
                e.xp=v.xp;e.yp=v.yp;e.history=v.history;e.down=v.down;e.time=v.time;return e;
            }
            public static MotionEvent obtain(long down,long time,int action,int count,
                PointerProperties[] p,PointerCoords[] c,int meta,int buttons,float xp,float yp,
                int device,int edge,int source,int flags){
                MotionEvent e=new MotionEvent();e.down=down;e.time=time;e.action=action;
                e.meta=meta;e.buttons=buttons;e.xp=xp;e.yp=yp;e.device=device;e.edge=edge;
                e.source=source;e.flags=flags;e.x=c[0].x;e.y=c[0].y;return e;
            }
            public boolean isFromSource(int mask){return (source&mask)==mask;}
            public int getActionMasked(){return action;}
            public int getHistorySize(){return history.length;}
            public float getHistoricalX(int i){return history[i][0];}
            public float getHistoricalY(int i){return history[i][1];}
            public float getX(){return x;} public float getY(){return y;}
            public int getButtonState(){return buttons;}
            public void setSource(int value){source=value;}
            public void setLocation(float x,float y){this.x=x;this.y=y;}
            public void recycle(){recycled=true;}
            public int getPointerCount(){return 1;}
            public void getPointerProperties(int i,PointerProperties p){p.id=1;p.toolType=3;}
            public void getPointerCoords(int i,PointerCoords p){p.x=x;p.y=y;}
            public int getMetaState(){return meta;}public int getDeviceId(){return device;}
            public int getEdgeFlags(){return edge;}public int getFlags(){return flags;}
            public long getDownTime(){return down;}public float getXPrecision(){return xp;}
            public float getYPrecision(){return yp;}
        }""",
    "com/unity3d/player/UnityPlayer.java": r"""package com.unity3d.player;
        import android.view.*;
        import java.util.ArrayList;
        public class UnityPlayer extends ViewGroup {
            public boolean accepts=true;
            public final ArrayList<MotionEvent> injected=new ArrayList<>();
            public boolean injectEvent(InputEvent event){injected.add(MotionEvent.obtain((MotionEvent)event));return accepts;}
        }""",
    "dev/betterendfield/android/RuntimeBootstrap.java": r"""package dev.betterendfield.android;
        final class RuntimeBootstrap {static boolean loaded(){return false;} }""",
    "dev/betterendfield/android/NativeCommandBridge.java": r"""package dev.betterendfield.android;
        final class NativeCommandBridge {
            static boolean pcMouseCaptureRequested(){return false;}
            static void pcMouseCaptured(boolean capture){}
            static void pcMouseMotion(float x,float y){}
        }""",
}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--output", type=Path, required=True)
    args = parser.parse_args()
    output = args.output.resolve()
    sources = output / "sources"
    classes = output / "classes"
    classes.mkdir(parents=True, exist_ok=True)
    files = []
    for name, content in STUBS.items():
        target = sources / name
        target.parent.mkdir(parents=True, exist_ok=True)
        target.write_text(content, encoding="utf-8")
        files.append(str(target))
    android = Path(__file__).resolve().parents[3]
    files.extend([
        str(android / "app/src/main/java/dev/betterendfield/android/PcUiMouseBridge.java"),
        str(android / "app/src/testHost/java/dev/betterendfield/android/PcUiMouseBridgeHostTest.java"),
    ])
    subprocess.run(["javac", "-encoding", "UTF-8", "--release", "17", "-d", str(classes), *files], check=True)
    subprocess.run(["java", "-cp", str(classes), "dev.betterendfield.android.PcUiMouseBridgeHostTest"], check=True)


if __name__ == "__main__":
    main()
