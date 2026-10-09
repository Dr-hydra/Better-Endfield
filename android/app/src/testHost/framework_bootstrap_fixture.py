"""Execute the real bootstrap components with a bounded Android host lifecycle.

The host supplies clocks, callback queues, and Binder connection events. The
production Activity, Receiver, and connection wait implement all lifecycle rules.
"""
from pathlib import Path
import argparse
import subprocess
import xml.etree.ElementTree as ET


STUBS = {
    "android/content/Context.java": "package android.content; public class Context {}",
    "android/os/Bundle.java": "package android.os; public class Bundle {}",
    "android/os/Looper.java": "package android.os; public class Looper { public static Looper getMainLooper(){return new Looper();} }",
    "android/os/SystemClock.java": "package android.os; public class SystemClock { public static long time; public static long uptimeMillis(){return time;} }",
    "android/os/Handler.java": """package android.os;
        import java.util.ArrayList;
        public class Handler {
            private record Task(Runnable action, long due) {}
            private static final ArrayList<Task> tasks = new ArrayList<>();
            public Handler(Looper looper) {}
            public boolean postDelayed(Runnable action, long delay) {
                tasks.add(new Task(action, SystemClock.time + delay)); return true;
            }
            public void removeCallbacks(Runnable action) { tasks.removeIf(t -> t.action()==action); }
            public static int pending() { return tasks.size(); }
            public static void reset() { tasks.clear(); SystemClock.time=0; }
            public static void advance(long millis) {
                long target=SystemClock.time + millis;
                while (true) {
                    Task next=null;
                    for (Task task:tasks) if(task.due()<=target && (next==null || task.due()<next.due())) next=task;
                    if(next==null) break;
                    tasks.remove(next); SystemClock.time=next.due(); next.action().run();
                }
                SystemClock.time=target;
            }
        }""",
    "android/content/Intent.java": """package android.content;
        public class Intent {
            public static final String ACTION_MY_PACKAGE_REPLACED="android.intent.action.MY_PACKAGE_REPLACED";
            private final String action;
            public int payloadReads;
            public Intent(String action){this.action=action;}
            public String getAction(){return action;}
            public String getStringExtra(String key){payloadReads++;return "hostile-setting-patch";}
        }""",
    "android/content/BroadcastReceiver.java": """package android.content;
        import java.util.concurrent.CountDownLatch;
        import java.util.concurrent.atomic.AtomicInteger;
        public abstract class BroadcastReceiver {
            public static int asyncCalls;
            public static PendingResult latest;
            public abstract void onReceive(Context context, Intent intent);
            public final PendingResult goAsync(){asyncCalls++;latest=new PendingResult();return latest;}
            public static final class PendingResult {
                public final CountDownLatch done=new CountDownLatch(1);
                public final AtomicInteger finishes=new AtomicInteger();
                public void finish(){finishes.incrementAndGet();done.countDown();}
            }
        }""",
    "android/app/Activity.java": """package android.app;
        import android.os.Bundle;
        import android.content.Intent;
        public class Activity extends android.content.Context {
            public int creates, resumes, finishes, intentReads;
            private boolean finishing,destroyed;
            protected void onCreate(Bundle saved){creates++;}
            protected void onResume(){resumes++;}
            protected void onPause(){}
            protected void onDestroy(){destroyed=true;}
            public boolean isFinishing(){return finishing;}
            public boolean isDestroyed(){return destroyed;}
            public void finish(){finishing=true;finishes++;}
            public Intent getIntent(){intentReads++;return new Intent("hostile-setting-patch");}
        }""",
    "android/util/Log.java": "package android.util; public class Log { public static int w(String tag,String message){return 0;} }",
    "dev/betterendfield/next/OverlaySettingsDiagnostics.java": """package dev.betterendfield.next;
        import android.content.Context;
        import java.util.ArrayList;
        import java.util.List;
        final class OverlaySettingsDiagnostics {
            private static final List<String> entries=new ArrayList<>();
            static synchronized void record(Context context,String status){entries.add(status);}
            static synchronized void reset(){entries.clear();}
            static synchronized String statuses(){return String.join(",",entries);}
        }""",
    "dev/betterendfield/next/FrameworkSettings.java": """package dev.betterendfield.next;
        import java.util.concurrent.CountDownLatch;
        final class FrameworkSettings {
            static volatile boolean connected,throwOnWorker;
            static volatile int queries;
            static volatile Thread worker;
            static volatile CountDownLatch entered=new CountDownLatch(1);
            static synchronized boolean isConnected() {
                queries++;
                if(Thread.currentThread().getName().equals("BE-update-framework")) {
                    worker=Thread.currentThread();entered.countDown();
                    if(throwOnWorker) throw new IllegalStateException("connection-unavailable");
                }
                return connected;
            }
            static synchronized void reset(boolean next) {
                connected=next;throwOnWorker=false;queries=0;worker=null;entered=new CountDownLatch(1);
            }
            static synchronized void connect(){connected=true;FrameworkSettings.class.notifyAll();}
        }""",
}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--repo", type=Path, default=Path(__file__).resolve().parents[4])
    parser.add_argument("--output", type=Path)
    parser.add_argument("--javac", default="javac")
    parser.add_argument("--java", default="java")
    args = parser.parse_args()
    repo = args.repo.resolve()
    output = (args.output or repo / "build/android-issues-25-26/framework-bootstrap-host").resolve()
    output.mkdir(parents=True, exist_ok=True)
    namespace = "{http://schemas.android.com/apk/res/android}"
    app = ET.parse(repo / "android/app/src/main/AndroidManifest.xml").getroot().find("application")
    assert app is not None and app.get(namespace + "name") == ".ModuleApplication"
    activity = app.find("activity[@" + namespace + "name='.FrameworkBootstrapActivity']")
    receiver = app.find("receiver[@" + namespace + "name='.ModulePackageReplacedReceiver']")
    assert activity is not None and receiver is not None
    for name, value in {"exported": "true", "excludeFromRecents": "true", "noHistory": "true",
                        "taskAffinity": "", "theme": "@android:style/Theme.Translucent.NoTitleBar"}.items():
        assert activity.get(namespace + name) == value, f"Bootstrap activity changed {name} boundary"
    assert not activity.findall("intent-filter"), "Bootstrap is explicit-only, without external data routing"
    assert receiver.get(namespace + "exported") == "false", "Update receiver must not accept external callers"
    assert [action.get(namespace + "name") for action in receiver.findall("intent-filter/action")] == [
        "android.intent.action.MY_PACKAGE_REPLACED"], "Update receiver accepted an unrelated broadcast"
    assert activity.get(namespace + "process") is None and receiver.get(namespace + "process") is None
    sources = []
    for relative, source in STUBS.items():
        path = output / "stub-src" / relative
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_text(source, encoding="utf-8")
        sources.append(str(path))
    main_sources = repo / "android/app/src/main/java/dev/betterendfield/next"
    sources += [str(main_sources / name) for name in (
        "FrameworkBootstrapActivity.java", "ModulePackageReplacedReceiver.java", "FrameworkServiceWait.java")]
    sources.append(str(repo / "android/app/src/testHost/java/dev/betterendfield/next/FrameworkBootstrapHostTest.java"))
    classes = output / "classes"
    classes.mkdir(exist_ok=True)
    subprocess.run([args.javac, "-encoding", "UTF-8", "--release", "17", "-proc:none", "-Xlint:all", "-Werror",
                    "-d", str(classes), *sources], check=True)
    subprocess.run([args.java, "-cp", str(classes), "dev.betterendfield.next.FrameworkBootstrapHostTest"], check=True)
    print("PASS bootstrap manifest: same Application process, explicit transparent bounded Activity, non-exported update-only receiver")


if __name__ == "__main__":
    main()
