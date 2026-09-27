import java.net.URLClassLoader;
import java.nio.file.Path;
public class JniIsolationTest {
 public static void main(String[]a)throws Exception{
  try(var game=new URLClassLoader(new java.net.URL[]{Path.of(a[0]).toUri().toURL()},null);
      var module=new URLClassLoader(new java.net.URL[]{Path.of(a[1]).toUri().toURL()},null)){
   var original=Thread.currentThread().getContextClassLoader();
   Thread.currentThread().setContextClassLoader(module);
   try { game.loadClass("game.Loader").getMethod("load",String.class).invoke(null,a[2]); }
   finally { Thread.currentThread().setContextClassLoader(original); }
   var bridge=module.loadClass("bridge.Bridge");
   assert bridge.getClassLoader()!=game;
   assert (int)bridge.getMethod("protocol").invoke(null)==77;
   assert Thread.currentThread().getContextClassLoader()==original;
   System.out.println("PASS explicit JNI registration across two isolated classloaders (host JVM)");
  }
 }
}
