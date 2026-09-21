package dev.betterendfield.android;

final class NativeCommandBridge {
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
}
