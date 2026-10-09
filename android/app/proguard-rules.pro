# Framework entry is named by META-INF/xposed/java_init.list.
-keep class dev.betterendfield.next.XposedEntry { *; }
# Native registration and native-to-Java audio callbacks use these exact names.
-keep class dev.betterendfield.next.NativeCommandBridge { *; }
-keep class dev.betterendfield.next.BemInstaller {
    native <methods>;
    static void conversionProgress(java.lang.String,int,int,int,int,float);
}
-keepclasseswithmembernames,includedescriptorclasses class * {
    native <methods>;
}
# WebView invokes annotated methods by name.
-keepclassmembers class * {
    @android.webkit.JavascriptInterface <methods>;
}
-keepattributes Signature,InnerClasses,EnclosingMethod,*Annotation*
-renamesourcefileattribute SourceFile
# Commons Compress references an optional desktop zstd-jni codec. This app
# does not ship that native codec; MmdImportArchive rejects Zstandard entries
# before asking ZipFile for a stream. ZIP/7z codecs in use remain packaged.
-dontwarn com.github.luben.zstd.ZstdInputStream
