package dev.betterendfield.android;

import android.content.Context;
import android.content.SharedPreferences;

import java.util.Locale;

final class ModuleSettings {
    private static final String VOICE_CATALOGS = "voice_catalogs";
    static final String VOICE_RULES = "voice_language_rules";
    private static final String MODEL_ENABLED = "model_replacement_enabled";
    private static final String MODEL_RUNTIME_ENABLED = "model_runtime_enabled";
    private static final String MODEL_CHARACTER = "model_character";
    private static final String MODEL_ACTION = "model_action";
    private static final String MODEL_FINAL_LOOP = "model_final_loop";
    private static final String MODEL_FORCE_LOOP = "model_force_loop";
    private static final String MODEL_CROSSFADE = "model_crossfade";
    private static final String MODEL_LOOP_START = "model_loop_start";
    private static final String MODEL_LOOP_END = "model_loop_end";
    private static final String MODEL_CROSSFADE_DURATION = "model_crossfade_duration";
    private static final String MODEL_SCALE = "model_scale";
    private static final String LOGO_ENABLED = "logo_theme_enabled";
    private static final String LOGO_COLOR = "logo_theme_color";
    static final String MODEL_CONFIGURATION = "model_configuration";

    // betterendfield.ui — the desktop interface module.
    private static final String UI_HIDE_UID = "ui_hide_uid";
    static final String UI_HIDE_HUD = "ui_hide_hud";
    static final String UI_CONFIGURATION = "ui_configuration";

    // betterendfield.camera — the desktop camera module.
    private static final String CAMERA_DITHER = "camera_disable_dither";
    static final String CAMERA_FREE = "camera_free_enabled";
    static final String CAMERA_PAUSE = "camera_pause_enabled";
    static final String CAMERA_FIRST_PERSON = "camera_first_person_enabled";
    private static final String CAMERA_FP_HIDE_HEAD = "camera_first_person_hide_head";
    private static final String CAMERA_FP_FILL_NECK = "camera_first_person_fill_neck";
    private static final String CAMERA_SPEED = "camera_movement_speed";
    private static final String CAMERA_FOV = "camera_field_of_view";
    private static final String CAMERA_FP_FOV = "camera_first_person_fov";
    static final String CAMERA_CONFIGURATION = "camera_configuration";

    // betterendfield.actions — the desktop sustained special dash.
    private static final String DASH_ENABLED = "dash_enabled";
    private static final String DASH_LIINO_CLEAN = "dash_liino_clean";
    private static final String DASH_AGLINA = "dash_character_aglina";
    private static final String DASH_LIINO = "dash_character_liino";
    static final String ACTIONS_CONFIGURATION = "actions_configuration";

    // Superseded by UI_HIDE_UID and CAMERA_DITHER. Version 3.2.2 and earlier
    // implemented those two switches in a separate Android-only enhancement
    // module; they now run through the shared desktop sources. The old keys are
    // still read once so an upgrade keeps the user's choice.
    private static final String LEGACY_HIDE_UID = "enhancement_hide_uid";
    private static final String LEGACY_DISABLE_DITHER = "enhancement_disable_dither";

    static final String OVERLAY_ENABLED = "overlay_enabled";
    private static final String COMMAND_GENERATION = "command_generation";

    static final float SPEED_MINIMUM = 0.2f;
    static final float SPEED_MAXIMUM = 60.0f;
    static final float FOV_MINIMUM = 20.0f;
    static final float FOV_MAXIMUM = 120.0f;

    private ModuleSettings() {}

    static boolean isOverlayEnabled(Context context) {
        return preferences(context).getBoolean(OVERLAY_ENABLED, false);
    }

    static void setOverlayEnabled(Context context, boolean enabled) {
        preferences(context).edit().putBoolean(OVERLAY_ENABLED, enabled).commit();
    }

    static long nextCommandGeneration(Context context) {
        SharedPreferences settings = preferences(context);
        long next = settings.getLong(COMMAND_GENERATION, 0L) + 1L;
        settings.edit().putLong(COMMAND_GENERATION, next).commit();
        return next;
    }

    // ---------------------------------------------------------------- interface

    static boolean isHideUidEnabled(Context context) {
        SharedPreferences settings = preferences(context);
        return settings.contains(UI_HIDE_UID)
                ? settings.getBoolean(UI_HIDE_UID, false)
                : settings.getBoolean(LEGACY_HIDE_UID, false);
    }

    static boolean isHideHudEnabled(Context context) {
        return preferences(context).getBoolean(UI_HIDE_HUD, false);
    }

    static void setInterfaceSettings(Context context, boolean hideUid, boolean hideHud) {
        // An empty configuration is what keeps the module out of the game process,
        // so it has to be empty exactly when nothing is selected.
        String configuration = hideUid || hideHud
                ? "schema_version=1\n"
                        + "enabled=true\n"
                        + "diagnostics=true\n"
                        + "hide_uid_enabled=" + hideUid + "\n"
                        + "hide_hud_enabled=" + hideHud + "\n"
                        + "hide_hud_hotkey=" + Hotkeys.HIDE_HUD_NAME + "\n"
                // The touch layout and the Android/cloud platform claim only exist
                // to make a desktop client look like a phone. This is a phone.
                        + "mobile_ui_enabled=false\n"
                        + "platform_spoof_enabled=false\n"
                : "";
        preferences(context)
                .edit()
                .putBoolean(UI_HIDE_UID, hideUid)
                .putBoolean(UI_HIDE_HUD, hideHud)
                .putString(UI_CONFIGURATION, configuration)
                .commit();
    }

    // ------------------------------------------------------------------- camera

    static boolean isDisableDitherEnabled(Context context) {
        SharedPreferences settings = preferences(context);
        return settings.contains(CAMERA_DITHER)
                ? settings.getBoolean(CAMERA_DITHER, false)
                : settings.getBoolean(LEGACY_DISABLE_DITHER, false);
    }

    static boolean isFreeCameraEnabled(Context context) {
        return preferences(context).getBoolean(CAMERA_FREE, false);
    }

    static boolean isWorldPauseEnabled(Context context) {
        return preferences(context).getBoolean(CAMERA_PAUSE, false);
    }

    static boolean isFirstPersonEnabled(Context context) {
        return preferences(context).getBoolean(CAMERA_FIRST_PERSON, false);
    }

    static boolean isFirstPersonHideHead(Context context) {
        return preferences(context).getBoolean(CAMERA_FP_HIDE_HEAD, true);
    }

    static boolean isFirstPersonFillNeck(Context context) {
        return preferences(context).getBoolean(CAMERA_FP_FILL_NECK, true);
    }

    static String getCameraSpeed(Context context) {
        return preferences(context).getString(CAMERA_SPEED, "5");
    }

    static String getCameraFieldOfView(Context context) {
        return preferences(context).getString(CAMERA_FOV, "60");
    }

    static String getFirstPersonFieldOfView(Context context) {
        return preferences(context).getString(CAMERA_FP_FOV, "75");
    }

    static void setCameraSettings(
            Context context,
            boolean disableDither,
            boolean freeCamera,
            boolean worldPause,
            boolean firstPerson,
            boolean hideHead,
            boolean fillNeck,
            double movementSpeed,
            double fieldOfView,
            double firstPersonFov) {
        // World pause is a free-camera sub-mode on desktop: its hotkey is only
        // read while the free camera is armed, so offering it alone would be a
        // switch that does nothing.
        boolean pause = worldPause && freeCamera;
        boolean any = disableDither || freeCamera || firstPerson;
        String configuration = any
                ? "schema_version=2\n"
                        + "enabled=true\n"
                        + "diagnostics=true\n"
                        + "disable_dither_enabled=" + disableDither + "\n"
                        + "free_camera_enabled=" + freeCamera + "\n"
                        + "pause_enabled=" + pause + "\n"
                        + "first_person_camera_enabled=" + firstPerson + "\n"
                        + "first_person_hide_head=" + hideHead + "\n"
                        + "first_person_fill_neck_hole=" + fillNeck + "\n"
                        + "movement_speed=" + number(movementSpeed) + "\n"
                        + "field_of_view=" + number(fieldOfView) + "\n"
                        + "first_person_fov=" + number(firstPersonFov) + "\n"
                        + "toggle_hotkey=" + Hotkeys.FREE_CAMERA_NAME + "\n"
                        + "pause_hotkey=" + Hotkeys.WORLD_PAUSE_NAME + "\n"
                        + "first_person_hotkey=" + Hotkeys.FIRST_PERSON_NAME + "\n"
                : "";
        preferences(context)
                .edit()
                .putBoolean(CAMERA_DITHER, disableDither)
                .putBoolean(CAMERA_FREE, freeCamera)
                .putBoolean(CAMERA_PAUSE, pause)
                .putBoolean(CAMERA_FIRST_PERSON, firstPerson)
                .putBoolean(CAMERA_FP_HIDE_HEAD, hideHead)
                .putBoolean(CAMERA_FP_FILL_NECK, fillNeck)
                .putString(CAMERA_SPEED, number(movementSpeed))
                .putString(CAMERA_FOV, number(fieldOfView))
                .putString(CAMERA_FP_FOV, number(firstPersonFov))
                .putString(CAMERA_CONFIGURATION, configuration)
                .commit();
    }

    // ----------------------------------------------------------- sustained dash

    static boolean isSustainedDashEnabled(Context context) {
        return preferences(context).getBoolean(DASH_ENABLED, false);
    }

    static boolean isLiinoCleanDashEnabled(Context context) {
        return preferences(context).getBoolean(DASH_LIINO_CLEAN, false);
    }

    static boolean isDashCharacterEnabled(Context context, String codename) {
        return preferences(context).getBoolean(
                "liino".equals(codename) ? DASH_LIINO : DASH_AGLINA, true);
    }

    static void setSustainedDashSettings(
            Context context,
            boolean enabled,
            boolean liinoClean,
            boolean aglina,
            boolean liino) {
        StringBuilder characters = new StringBuilder();
        if (aglina) characters.append("aglina");
        if (liino) {
            if (characters.length() > 0) characters.append(',');
            characters.append("liino");
        }
        // With no character selected the desktop module would arm every one of
        // them, because an absent key means "all" for older configurations.
        boolean active = enabled && characters.length() > 0;
        String configuration = active
                ? "schema_version=3\n"
                        + "enabled=true\n"
                        + "diagnostics=true\n"
                // The bone-pose overlay is not optional on Android: the banks ship
                // in the APK and are materialized before the module starts, so the
                // looping segment always runs from them rather than the native-only hold.
                        + "external_loop=true\n"
                        + "liino_clean=" + liinoClean + "\n"
                        + "characters=" + characters + "\n"
                : "";
        preferences(context)
                .edit()
                .putBoolean(DASH_ENABLED, enabled)
                .putBoolean(DASH_LIINO_CLEAN, liinoClean)
                .putBoolean(DASH_AGLINA, aglina)
                .putBoolean(DASH_LIINO, liino)
                .putString(ACTIONS_CONFIGURATION, configuration)
                .commit();
    }

    /**
     * Rewrites every configuration string from the stored switches. Used once per
     * launch of the settings screen so an upgrade that introduced a new key, or a
     * carried-over pre-3.3 enhancement choice, reaches the game without the user
     * having to touch each page.
     */
    static void republishConfigurations(Context context) {
        setInterfaceSettings(context, isHideUidEnabled(context), isHideHudEnabled(context));
        setCameraSettings(
                context,
                isDisableDitherEnabled(context),
                isFreeCameraEnabled(context),
                isWorldPauseEnabled(context),
                isFirstPersonEnabled(context),
                isFirstPersonHideHead(context),
                isFirstPersonFillNeck(context),
                parse(getCameraSpeed(context), 5.0),
                parse(getCameraFieldOfView(context), 60.0),
                parse(getFirstPersonFieldOfView(context), 75.0));
        setSustainedDashSettings(
                context,
                isSustainedDashEnabled(context),
                isLiinoCleanDashEnabled(context),
                isDashCharacterEnabled(context, "aglina"),
                isDashCharacterEnabled(context, "liino"));
    }

    static double parse(String value, double fallback) {
        try {
            double parsed = Double.parseDouble(value.trim());
            return Double.isFinite(parsed) ? parsed : fallback;
        } catch (NumberFormatException | NullPointerException invalid) {
            return fallback;
        }
    }

    static String number(double value) {
        String text = String.format(Locale.ROOT, "%.4f", value);
        return text.contains(".")
                ? text.replaceFirst("0+$", "").replaceFirst("\\.$", "") : text;
    }

    // -------------------------------------------------------------------- voice

    static String getVoiceCatalogs(Context context) {
        return preferences(context).getString(VOICE_CATALOGS, "");
    }

    static void setVoiceCatalogs(Context context, String catalogs) {
        preferences(context)
                .edit()
                .putString(VOICE_CATALOGS, catalogs)
                .remove("voice_language")
                .commit();
    }

    static String getVoiceRules(Context context) {
        SharedPreferences preferences = preferences(context);
        String rules = preferences.getString(VOICE_RULES, "");
        if (rules != null && !rules.isEmpty()) {
            return rules;
        }
        String legacy = preferences.getString(VOICE_CATALOGS, "");
        if (legacy == null || legacy.isEmpty()) {
            return "";
        }
        StringBuilder migrated = new StringBuilder();
        for (String item : legacy.split(",")) {
            String speaker = switch (item) {
                case "aglina" -> "chr_0013_aglina";
                case "liino" -> "chr_0035_liino";
                default -> "";
            };
            if (!speaker.isEmpty()) {
                if (migrated.length() > 0) migrated.append(';');
                migrated.append(speaker).append(":Japanese");
            }
        }
        return migrated.toString();
    }

    static void setVoiceRules(Context context, String rules) {
        preferences(context)
                .edit()
                .putString(VOICE_RULES, rules == null ? "" : rules)
                .remove(VOICE_CATALOGS)
                .remove("voice_language")
                .commit();
    }

    // -------------------------------------------------------------- login model

    static void setModelEnabled(Context context, boolean enabled) {
        preferences(context).edit().putBoolean(MODEL_ENABLED, enabled).commit();
    }

    static boolean isModelRuntimeEnabled(Context context) {
        SharedPreferences settings = preferences(context);
        return settings.contains(MODEL_RUNTIME_ENABLED)
                ? settings.getBoolean(MODEL_RUNTIME_ENABLED, false)
                : settings.getBoolean(MODEL_ENABLED, false);
    }

    static void setModelRuntimeEnabled(Context context, boolean enabled) {
        preferences(context).edit().putBoolean(MODEL_RUNTIME_ENABLED, enabled).commit();
    }

    static boolean isModelEnabled(Context context) {
        return preferences(context).getBoolean(MODEL_ENABLED, false);
    }

    static String getModelCharacter(Context context) {
        return preferences(context).getString(MODEL_CHARACTER, "chr_0013_aglina");
    }

    static String getModelAction(Context context) {
        return preferences(context).getString(MODEL_ACTION, "");
    }

    static boolean isModelFinalLoop(Context context) {
        return preferences(context).getBoolean(MODEL_FINAL_LOOP, true);
    }

    static boolean isModelForceLoop(Context context) {
        return preferences(context).getBoolean(MODEL_FORCE_LOOP, false);
    }

    static boolean isModelCrossfade(Context context) {
        return preferences(context).getBoolean(MODEL_CROSSFADE, false);
    }

    static String getModelLoopStart(Context context) {
        return preferences(context).getString(MODEL_LOOP_START, "0.968");
    }

    static String getModelLoopEnd(Context context) {
        return preferences(context).getString(MODEL_LOOP_END, "2.3760002");
    }

    static String getModelCrossfadeDuration(Context context) {
        return preferences(context).getString(MODEL_CROSSFADE_DURATION, "0.20");
    }

    static String getModelScale(Context context) {
        return preferences(context).getString(MODEL_SCALE, "1.0");
    }

    static boolean isLogoEnabled(Context context) {
        return preferences(context).getBoolean(LOGO_ENABLED, false);
    }

    static String getLogoColor(Context context) {
        return preferences(context).getString(LOGO_COLOR, "#FFC928");
    }

    static void setModelSettings(
            Context context,
            boolean enabled,
            String character,
            String action,
            boolean finalLoop,
            boolean forceLoop,
            boolean crossfade,
            String loopStart,
            String loopEnd,
            String crossfadeDuration,
            String scale,
            boolean logoEnabled,
            String logoColor,
            String configuration) {
        preferences(context)
                .edit()
                .putBoolean(MODEL_ENABLED, enabled)
                .putBoolean(MODEL_RUNTIME_ENABLED, enabled)
                .putString(MODEL_CHARACTER, character)
                .putString(MODEL_ACTION, action)
                .putBoolean(MODEL_FINAL_LOOP, finalLoop)
                .putBoolean(MODEL_FORCE_LOOP, forceLoop)
                .putBoolean(MODEL_CROSSFADE, crossfade)
                .putString(MODEL_LOOP_START, loopStart)
                .putString(MODEL_LOOP_END, loopEnd)
                .putString(MODEL_CROSSFADE_DURATION, crossfadeDuration)
                .putString(MODEL_SCALE, scale)
                .putBoolean(LOGO_ENABLED, logoEnabled)
                .putString(LOGO_COLOR, logoColor)
                .putString(MODEL_CONFIGURATION, configuration)
                .commit();
    }

    private static SharedPreferences preferences(Context context) {
        return FrameworkSettings.open(context);
    }
}
