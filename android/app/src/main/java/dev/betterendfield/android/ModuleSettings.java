package dev.betterendfield.android;

import android.content.Context;
import android.content.SharedPreferences;

final class ModuleSettings {
    private static final String PREFERENCES = "module_settings";
    private static final String VOICE_CATALOGS = "voice_catalogs";
    private static final String VOICE_RULES = "voice_language_rules";
    private static final String MODEL_ENABLED = "model_replacement_enabled";
    private static final String MODEL_CHARACTER = "model_character";
    private static final String MODEL_ACTION = "model_action";
    private static final String MODEL_FINAL_LOOP = "model_final_loop";
    private static final String MODEL_SCALE = "model_scale";
    private static final String LOGO_ENABLED = "logo_theme_enabled";
    private static final String LOGO_COLOR = "logo_theme_color";
    private static final String MODEL_CONFIGURATION = "model_configuration";

    private ModuleSettings() {}

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
            String scale,
            boolean logoEnabled,
            String logoColor,
            String configuration) {
        preferences(context)
                .edit()
                .putBoolean(MODEL_ENABLED, enabled)
                .putString(MODEL_CHARACTER, character)
                .putString(MODEL_ACTION, action)
                .putBoolean(MODEL_FINAL_LOOP, finalLoop)
                .putString(MODEL_SCALE, scale)
                .putBoolean(LOGO_ENABLED, logoEnabled)
                .putString(LOGO_COLOR, logoColor)
                .putString(MODEL_CONFIGURATION, configuration)
                .commit();
    }

    private static SharedPreferences preferences(Context context) {
        try {
            // LSPosed redirects this module-only mode to its protected shared
            // preference store when xposedsharedprefs is declared.
            return context.getSharedPreferences(
                    PREFERENCES,
                    Context.MODE_WORLD_READABLE);
        } catch (SecurityException unavailableOutsideLsposed) {
            return context.getSharedPreferences(PREFERENCES, Context.MODE_PRIVATE);
        }
    }
}
