package dev.betterendfield.next;

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

    // betterendfieldnext.ui — the desktop interface module.
    private static final String UI_HIDE_UID = "ui_hide_uid";
    static final String UI_HIDE_HUD = "ui_hide_hud";
    static final String UI_CONFIGURATION = "ui_configuration";

    // betterendfieldnext.camera — the desktop camera module.
    private static final String CAMERA_DITHER = "camera_disable_dither";
    static final String CAMERA_FREE = "camera_free_enabled";
    static final String CAMERA_PAUSE = "camera_pause_enabled";
    private static final String CAMERA_SPEED = "camera_movement_speed";
    private static final String CAMERA_FOV = "camera_field_of_view";
    static final String CAMERA_GLOBAL_FOV_ENABLED = "camera_global_fov_enabled";
    static final String CAMERA_GLOBAL_FOV = "camera_global_fov";
    private static final String CAMERA_FOLLOW_CHARACTER = "camera_follow_character";
    static final String MMD_ENABLED = "mmd_enabled";
    static final String MMD_WORK = "mmd_work";
    static final String PC_UI_ENABLED = "pc_ui_enabled";
    static final String CAMERA_CONFIGURATION = "camera_configuration";
    static final String[] MOTION_PRESETS = {"orbit", "dolly_zoom", "crane", "truck"};
    static final String[] CLOTH_MODES = {"game", "stable", "freeze"};

    record CameraExtras(boolean body, boolean face, boolean terrain, double motionScale,
            int clothMode, String preset, double smoothing, double motionSpeed,
            double orbitSpeed, double duration, double targetHeight,
            double segmentSeconds, boolean keyframeLoop) {
        CameraExtras {
            motionScale = bounded(motionScale, 0.05, 5, 1);
            clothMode = clothMode >= 0 && clothMode < CLOTH_MODES.length ? clothMode : 1;
            preset = MOTION_PRESETS[choice(MOTION_PRESETS, preset, 0)];
            smoothing = bounded(smoothing, 0, 0.95, 0.3);
            motionSpeed = bounded(motionSpeed, -20, 20, 1);
            orbitSpeed = bounded(orbitSpeed, -180, 180, 20);
            duration = bounded(duration, 0, 600, 0);
            targetHeight = bounded(targetHeight, -5, 5, 1.2);
            segmentSeconds = bounded(segmentSeconds, 0.2, 60, 3);
        }
    }

    // betterendfieldnext.actions — the desktop sustained special dash.
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
    private static final String OVERLAY_TRANSPARENCY = "overlay_transparency";
    private static final String OVERLAY_AUTO_SNAP = "overlay_auto_snap";
    private static final String COMMAND_GENERATION = "command_generation";

    static final float OVERLAY_TRANSPARENCY_MAXIMUM = 80f;
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

    record OverlayAppearance(float transparency, boolean autoSnap) {
        OverlayAppearance {
            transparency = Float.isFinite(transparency)
                    ? Math.max(0f, Math.min(OVERLAY_TRANSPARENCY_MAXIMUM, transparency)) : 0f;
        }

        float alpha() { return 1f - transparency / 100f; }
    }

    static OverlayAppearance getOverlayAppearance(SharedPreferences settings) {
        return new OverlayAppearance(settings.getFloat(OVERLAY_TRANSPARENCY, 0f),
                settings.getBoolean(OVERLAY_AUTO_SNAP, false));
    }

    static float getOverlayTransparency(Context context) {
        return getOverlayAppearance(preferences(context)).transparency();
    }

    static boolean isOverlayAutoSnap(Context context) {
        return getOverlayAppearance(preferences(context)).autoSnap();
    }

    static void setOverlayTransparency(Context context, float transparency) {
        float bounded = new OverlayAppearance(transparency, false).transparency();
        preferences(context).edit().putFloat(OVERLAY_TRANSPARENCY, bounded).commit();
    }

    static void setOverlayAutoSnap(Context context, boolean enabled) {
        preferences(context).edit().putBoolean(OVERLAY_AUTO_SNAP, enabled).commit();
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

    static boolean isPcUiEnabled(Context context) {
        return preferences(context).getBoolean(PC_UI_ENABLED, false);
    }

    static void setPcUiEnabled(Context context, boolean enabled) {
        preferences(context).edit().putBoolean(PC_UI_ENABLED, enabled).commit();
        setInterfaceSettings(context, isHideUidEnabled(context), isHideHudEnabled(context));
    }

    static void setInterfaceSettings(Context context, boolean hideUid, boolean hideHud) {
        // An empty configuration is what keeps the module out of the game process,
        // so it has to be empty exactly when nothing is selected.
        String configuration = hideUid || hideHud || isPcUiEnabled(context)
                ? "schema_version=1\n"
                        + "enabled=true\n"
                        + "diagnostics=true\n"
                        + "hide_uid_enabled=" + hideUid + "\n"
                        + "hide_hud_enabled=" + hideHud + "\n"
                        + "pc_ui_enabled=" + isPcUiEnabled(context) + "\n"
                        + "hide_hud_hotkey=" + Hotkeys.HIDE_HUD_NAME + "\n"
                // PC UI changes only input type; it never spoofs the platform.
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







    static String getCameraSpeed(Context context) {
        return preferences(context).getString(CAMERA_SPEED, "5");
    }

    static String getCameraFieldOfView(Context context) {
        return preferences(context).getString(CAMERA_FOV, "60");
    }

    static boolean isGlobalFovEnabled(Context context) {
        return preferences(context).getBoolean(CAMERA_GLOBAL_FOV_ENABLED, false);
    }

    static String getGlobalFieldOfView(Context context) {
        return preferences(context).getString(CAMERA_GLOBAL_FOV, "60");
    }

    static boolean isCameraFollowCharacter(Context context) {
        return preferences(context).getBoolean(CAMERA_FOLLOW_CHARACTER, false);
    }

    static synchronized void setGlobalFovEnabled(Context context, boolean enabled) {
        try { saveGlobalFov(context, enabled, null); }
        catch (java.io.IOException error) { throw new IllegalStateException(error); }
    }

    static synchronized void setGlobalFieldOfView(Context context, double value) {
        if (!Double.isFinite(value)) return;
        try { saveGlobalFov(context, null, Math.max(5, Math.min(150, value))); }
        catch (java.io.IOException error) { throw new IllegalStateException(error); }
    }

    static synchronized void saveGlobalFov(Context context, Boolean enabled, Double value) throws java.io.IOException {
        SharedPreferences prefs = preferences(context);
        boolean nextEnabled = enabled == null ? isGlobalFovEnabled(context) : enabled;
        String nextValue = value == null ? getGlobalFieldOfView(context) : number(value);
        String config = cameraConfiguration(context, isDisableDitherEnabled(context), isFreeCameraEnabled(context),
                isWorldPauseEnabled(context), parse(getCameraSpeed(context), 5), parse(getCameraFieldOfView(context), 60),
                nextEnabled, nextValue);
        java.util.Map<String, ?> before = prefs.getAll();
        boolean committed = prefs.edit().putBoolean(CAMERA_GLOBAL_FOV_ENABLED, nextEnabled)
                .putString(CAMERA_GLOBAL_FOV, nextValue).putString(CAMERA_CONFIGURATION, config).commit();
        if (!committed || prefs.getBoolean(CAMERA_GLOBAL_FOV_ENABLED, false) != nextEnabled
                || !nextValue.equals(prefs.getString(CAMERA_GLOBAL_FOV, "60"))
                || !config.equals(prefs.getString(CAMERA_CONFIGURATION, ""))) {
            SharedPreferences.Editor rollback = prefs.edit();
            for (String key : new String[]{CAMERA_GLOBAL_FOV_ENABLED, CAMERA_GLOBAL_FOV, CAMERA_CONFIGURATION}) {
                Object old = before.get(key);
                if (old == null) rollback.remove(key);
                else if (old instanceof Boolean) rollback.putBoolean(key, (Boolean) old);
                else rollback.putString(key, (String) old);
            }
            rollback.commit(); throw new java.io.IOException("FOV 保存失败");
        }
    }

    static void setCameraFollowCharacter(Context context, boolean enabled) {
        preferences(context).edit().putBoolean(CAMERA_FOLLOW_CHARACTER, enabled).commit();
        republishCameraConfiguration(context);
    }







    static boolean isMmdEnabled(Context context) {
        return preferences(context).getBoolean(MMD_ENABLED, false);
    }

    static void setMmdEnabled(Context context, boolean enabled) {
        preferences(context).edit().putBoolean(MMD_ENABLED, enabled).commit();
        republishCameraConfiguration(context);
    }

    static boolean isMmdLoop(Context context) {
        return preferences(context).getBoolean("mmd_loop", false);
    }

    static boolean isMmdMusicEnabled(Context context) {
        return preferences(context).getBoolean("mmd_music_enabled", true);
    }

    static String getMmdWork(Context context) {
        return preferences(context).getString(MMD_WORK, "");
    }

    static String getMmdSeekSeconds(Context context) {
        return preferences(context).getString("mmd_seek_seconds", "5");
    }

    static String getMmdMusicGain(Context context) {
        return preferences(context).getString("mmd_music_gain", "1");
    }

    static String getMmdAudioOffset(Context context) {
        return preferences(context).getString("mmd_audio_offset", "0");
    }

    static CameraExtras getCameraExtras(Context context) {
        SharedPreferences settings = preferences(context);
        return new CameraExtras(
                settings.getBoolean("vmd_body_enabled", true),
                settings.getBoolean("vmd_face_enabled", true),
                settings.getBoolean("vmd_terrain_enabled", false),
                parse(settings.getString("vmd_motion_scale", "1"), 1),
                choice(CLOTH_MODES, settings.getString("vmd_cloth_mode", "stable"), 1),
                settings.getString("motion_preset", "orbit"),
                parse(settings.getString("free_camera_smoothing", "0.3"), 0.3),
                parse(settings.getString("motion_speed", "1"), 1),
                parse(settings.getString("orbit_speed", "20"), 20),
                parse(settings.getString("motion_duration", "0"), 0),
                parse(settings.getString("motion_target_height", "1.2"), 1.2),
                parse(settings.getString("keyframe_segment_seconds", "3"), 3),
                settings.getBoolean("keyframe_loop", false));
    }

    static int choice(String[] choices, String value, int fallback) {
        for (int i = 0; i < choices.length; i++) if (choices[i].equals(value)) return i;
        return fallback;
    }

    private static double bounded(double value, double minimum, double maximum, double fallback) {
        return Double.isFinite(value) ? Math.max(minimum, Math.min(maximum, value)) : fallback;
    }

    private static void writeCameraExtras(SharedPreferences.Editor edit, CameraExtras extras) {
        edit.putBoolean("vmd_body_enabled", extras.body())
                .putBoolean("vmd_face_enabled", extras.face())
                .putBoolean("vmd_terrain_enabled", extras.terrain())
                .putString("vmd_motion_scale", number(extras.motionScale()))
                .putString("vmd_cloth_mode", CLOTH_MODES[extras.clothMode()])
                .putString("motion_preset", extras.preset())
                .putString("free_camera_smoothing", number(extras.smoothing()))
                .putString("motion_speed", number(extras.motionSpeed()))
                .putString("orbit_speed", number(extras.orbitSpeed()))
                .putString("motion_duration", number(extras.duration()))
                .putString("motion_target_height", number(extras.targetHeight()))
                .putString("keyframe_segment_seconds", number(extras.segmentSeconds()))
                .putBoolean("keyframe_loop", extras.keyframeLoop());
    }

    private static String cameraExtrasConfiguration(Context context) {
        CameraExtras extras = getCameraExtras(context);
        boolean mmd = isMmdEnabled(context);
        return "vmd_body_enabled=" + (mmd && extras.body()) + "\n"
                + "vmd_face_enabled=" + (mmd && extras.face()) + "\n"
                + "vmd_terrain_enabled=" + (mmd && extras.terrain()) + "\n"
                + "vmd_motion_scale=" + number(extras.motionScale()) + "\n"
                + "vmd_motion_weight=1\n"
                // Native parses names; its internal enum values are 0/1/2.
                + "vmd_cloth_mode=" + CLOTH_MODES[extras.clothMode()] + "\n"
                + "vmd_motion_loop=" + isMmdLoop(context) + "\n"
                + "vmd_camera_loop=" + isMmdLoop(context) + "\n"
                + "free_camera_smoothing=" + number(extras.smoothing()) + "\n"
                + "motion_preset=" + extras.preset() + "\n"
                + "motion_speed=" + number(extras.motionSpeed()) + "\n"
                + "orbit_speed=" + number(extras.orbitSpeed()) + "\n"
                + "motion_duration=" + number(extras.duration()) + "\n"
                + "motion_target_height=" + number(extras.targetHeight()) + "\n"
                + "keyframe_segment_seconds=" + number(extras.segmentSeconds()) + "\n"
                + "keyframe_loop=" + extras.keyframeLoop() + "\n";
        // keyframe_file is deliberately omitted: native supplies its persistent
        // game-files default, rather than receiving the module app's private path.
    }

    static void setMmdSettings(Context context, String work, boolean loop, boolean music,
            double seek, double gain, double offset) {
        setMmdSettings(context, work, loop, music, seek, gain, offset, null);
    }

    static void setMmdSettings(Context context, String work, boolean loop, boolean music,
            double seek, double gain, double offset, CameraExtras extras) {
        if (!work.isEmpty() && !work.matches("[a-f0-9-]{36}"))
            throw new IllegalArgumentException("无效的作品编号");
        SharedPreferences.Editor edit = preferences(context).edit().putString(MMD_WORK, work)
                .putBoolean("mmd_loop", loop).putBoolean("mmd_music_enabled", music)
                .putString("mmd_seek_seconds", number(Math.max(0.5, Math.min(60, seek))))
                .putString("mmd_music_gain", number(Math.max(0, Math.min(1, gain))))
                .putString("mmd_audio_offset", number(Math.max(-600, Math.min(600, offset))));
        if (extras != null) writeCameraExtras(edit, extras);
        edit.commit();
        republishCameraConfiguration(context);
    }

    static synchronized void setCameraSettings(Context context, boolean disableDither, boolean freeCamera,
            boolean worldPause, double movementSpeed, double fieldOfView) {
        String configuration = cameraConfiguration(context, disableDither, freeCamera, worldPause,
                movementSpeed, fieldOfView, isGlobalFovEnabled(context), getGlobalFieldOfView(context));
        preferences(context).edit()
                .putBoolean(CAMERA_DITHER, disableDither).putBoolean(CAMERA_FREE, freeCamera)
                .putBoolean(CAMERA_PAUSE, worldPause)
                .putString(CAMERA_SPEED, number(movementSpeed)).putString(CAMERA_FOV, number(fieldOfView))
                .putString(CAMERA_CONFIGURATION, configuration).commit();
    }

    private static String cameraConfiguration(Context context, boolean disableDither, boolean freeCamera,
            boolean worldPause, double movementSpeed, double fieldOfView, boolean globalFovEnabled, String globalFov) {
        boolean pause = worldPause;
        boolean any = disableDither || freeCamera || worldPause
                || isMmdEnabled(context) || globalFovEnabled;
        String configuration = any
                ? "schema_version=2\n"
                        + "enabled=true\n"
                        + "diagnostics=true\n"
                        + "disable_dither_enabled=" + disableDither + "\n"
                        + "free_camera_enabled=" + freeCamera + "\n"
                        + "pause_enabled=" + pause + "\n"
                        + "movement_speed=" + number(movementSpeed) + "\n"
                        + "field_of_view=" + number(fieldOfView) + "\n"
                        + "global_fov_enabled=" + globalFovEnabled + "\n"
                        + "global_fov=" + globalFov + "\n"
                        + "free_camera_follow_character=" + isCameraFollowCharacter(context) + "\n"
                        + "hotkey_layout=2\n"
                        + "mmd_enabled=" + isMmdEnabled(context) + "\n"
                        + "mmd_overlay_enabled=false\n"
                        + "mmd_work=" + getMmdWork(context) + "\n"
                        + "mmd_loop=" + isMmdLoop(context) + "\n"
                        + "mmd_music_enabled=" + isMmdMusicEnabled(context) + "\n"
                        + "mmd_seek_seconds=" + getMmdSeekSeconds(context) + "\n"
                        + "mmd_music_gain=" + getMmdMusicGain(context) + "\n"
                        + "mmd_audio_offset=" + getMmdAudioOffset(context) + "\n"
                        + cameraExtrasConfiguration(context)
                        + "toggle_hotkey=" + Hotkeys.FREE_CAMERA_NAME + "\n"
                        + "pause_hotkey=" + Hotkeys.WORLD_PAUSE_NAME + "\n"
                : "";
        return configuration;
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
        republishCameraConfiguration(context);
        setSustainedDashSettings(
                context,
                isSustainedDashEnabled(context),
                isLiinoCleanDashEnabled(context),
                isDashCharacterEnabled(context, "aglina"),
                isDashCharacterEnabled(context, "liino"));
    }

    static synchronized void republishCameraConfiguration(Context context) {
        setCameraSettings(
                context,
                isDisableDitherEnabled(context),
                isFreeCameraEnabled(context),
                isWorldPauseEnabled(context),
                parse(getCameraSpeed(context), 5.0),
                parse(getCameraFieldOfView(context), 60.0));
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
