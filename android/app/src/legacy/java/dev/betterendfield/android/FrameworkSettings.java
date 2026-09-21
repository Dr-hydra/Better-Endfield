package dev.betterendfield.android;

import android.content.Context;
import android.content.SharedPreferences;

final class FrameworkSettings {
    static void initialize(Context context) {}
    static SharedPreferences open(Context context) {
        try { return context.getSharedPreferences("module_settings", Context.MODE_WORLD_READABLE); }
        catch (SecurityException unavailable) {
            return context.getSharedPreferences("module_settings", Context.MODE_PRIVATE);
        }
    }
    static boolean writeRemoteCommand(String payload) { return false; }
    static String readRemoteStatus() { return ""; }
}
