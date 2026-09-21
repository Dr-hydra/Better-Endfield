package dev.betterendfield.android;

import android.content.SharedPreferences;

/**
 * The boot-time module configurations, as the game process sees them.
 *
 * The settings Activity writes each string once, when the user changes something;
 * the injected side only reads. Keeping them in one value means both framework
 * adapters read the same keys and {@link RuntimeBootstrap} does not grow a
 * parameter per module.
 *
 * An empty string means "do not start this module at all", which is how the
 * native runtime keeps an unselected feature out of the game process entirely.
 */
record ModuleConfigurations(
        String voice, String model, String ui, String camera, String actions) {

    static ModuleConfigurations read(SharedPreferences settings) {
        return new ModuleConfigurations(
                string(settings, ModuleSettings.VOICE_RULES),
                string(settings, ModuleSettings.MODEL_CONFIGURATION),
                string(settings, ModuleSettings.UI_CONFIGURATION),
                string(settings, ModuleSettings.CAMERA_CONFIGURATION),
                string(settings, ModuleSettings.ACTIONS_CONFIGURATION));
    }

    static ModuleConfigurations empty() {
        return new ModuleConfigurations("", "", "", "", "");
    }

    boolean none() {
        return voice.isEmpty() && model.isEmpty() && ui.isEmpty()
                && camera.isEmpty() && actions.isEmpty();
    }

    /**
     * The sustained-dash module always drives its loop from the bundled bone-pose
     * banks, so they are materialized whenever that module is configured at all.
     */
    boolean needsActionPoses() {
        return !actions.isEmpty();
    }

    String summary() {
        return "voice=" + !voice.isEmpty() + " model=" + !model.isEmpty()
                + " ui=" + !ui.isEmpty() + " camera=" + !camera.isEmpty()
                + " actions=" + !actions.isEmpty();
    }

    private static String string(SharedPreferences settings, String key) {
        String value = settings.getString(key, "");
        return value == null ? "" : value;
    }
}
