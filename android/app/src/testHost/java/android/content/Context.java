package android.content;

/** Host-only preference context. Not part of any Android source set. */
public final class Context {
    private final SharedPreferences preferences;
    private final java.io.File files;
    public Context(SharedPreferences preferences) { this(preferences, null); }
    public Context(SharedPreferences preferences, java.io.File files) { this.preferences = preferences; this.files = files; }
    public java.io.File getFilesDir() {
        if (files == null) throw new AssertionError("Host context has no files directory");
        return files;
    }
    public SharedPreferences getSharedPreferences(String name, int mode) {
        if (!"module_settings".equals(name) || mode != 0) throw new AssertionError("Wrong preference store");
        return preferences;
    }
}
