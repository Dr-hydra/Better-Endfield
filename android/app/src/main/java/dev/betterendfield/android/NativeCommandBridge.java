package dev.betterendfield.android;

final class NativeCommandBridge {
    private NativeCommandBridge() {}
    static native boolean submit(String payload);
    static native String status();
}
