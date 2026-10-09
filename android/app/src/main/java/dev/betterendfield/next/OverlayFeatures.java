package dev.betterendfield.next;

import android.content.SharedPreferences;

/**
 * Which panel controls are worth showing.
 *
 * A control is only offered when its module was actually configured to load.
 * Pressing "free camera" when {@code betterendfieldnext.camera} is not in the process
 * would press a key that nothing reads, and a button that silently does nothing
 * is worse than a button that is not there.
 */
record OverlayFeatures(
        boolean panel,
        boolean hideHud,
        boolean freeCamera,
        boolean worldPause,
        boolean mmd) {

    OverlayFeatures(boolean panel, boolean hideHud, boolean freeCamera,
            boolean worldPause) {
        this(panel, hideHud, freeCamera, worldPause, false);
    }

    static OverlayFeatures read(SharedPreferences settings) {
        boolean freeCamera = settings.getBoolean(ModuleSettings.CAMERA_FREE, false);
        return new OverlayFeatures(
                settings.getBoolean(ModuleSettings.OVERLAY_ENABLED, false),
                settings.getBoolean(ModuleSettings.UI_HIDE_HUD, false),
                freeCamera,
                settings.getBoolean(ModuleSettings.CAMERA_PAUSE, false),
                settings.getBoolean(ModuleSettings.MMD_ENABLED, false));
    }

    static OverlayFeatures off() {
        return new OverlayFeatures(false, false, false, false, false);
    }

    boolean anyControl() {
        return hideHud || freeCamera || worldPause || mmd;
    }
}
