package dev.betterendfield.android;

final class NativeCommandBridge {
    static native boolean updateThirdPartyRuntime(String index);
    /** Matches betterendfield::VirtualKeyAction in native/shared/android_compat. */
    static final int KEY_RELEASE = 0;
    static final int KEY_PRESS = 1;
    static final int KEY_PULSE = 2;

    private NativeCommandBridge() {}

    static native boolean submit(String payload);

    static native String status();

    /**
     * Presses a Windows virtual-key code in the latch the ported desktop modules
     * poll through GetAsyncKeyState. Returns false when the native library is
     * present but rejected the code.
     */
    static native boolean key(int virtualKey, int action);

    static native void releaseKeys();
    static native int protocolVersion();
    static native String runtimeStatus();
    /** Queues a prepared BEM snapshot; Unity applies it on a later frame. */
    static native boolean updateCustomModelConfig(String configuration);
    static native void frame();
    static native void foreground(boolean visible);
    static native void look(int dx, int dy);
    static native void cameraValues(float speed, float fov);
    static native String mmdStatus();
    private static native boolean mmd(int type, int argument, double value, String text);
    static boolean mmdCommand(String command) {
        try {
            org.json.JSONObject value = new org.json.JSONObject(command);
            return mmd(value.getInt("type"), value.optInt("argument", -1),
                    value.optDouble("value", 0), value.optString("text", ""));
        } catch (org.json.JSONException invalid) { return false; }
    }

    // Called through the explicitly bound module ClassLoader from native.
    static long audioOpen(String path) { return MmdAudio.open(path); }
    static int audioControl(long token, int operation, double value) { return MmdAudio.control(token, operation, value); }
    static double[] audioStatus(long token) { return MmdAudio.status(token); }
    static String audioError(long token) { return MmdAudio.error(token); }
}
