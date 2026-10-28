package dev.betterendfield.next;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.os.LocaleList;
import java.util.Locale;

final class AppLanguage {
    static final String KEY = "ui_language";
    private static volatile String preference = "system";

    static void read(SharedPreferences settings) {
        preference = UiLanguagePolicy.normalize(settings.getString(KEY, "system"));
    }

    static String tag() {
        return UiLanguagePolicy.resolve(preference,
                Resources.getSystem().getConfiguration().getLocales().get(0).getLanguage());
    }

    static boolean isEnglish() { return "en".equals(tag()); }

    static Context wrap(Context base) {
        read(FrameworkSettings.open(base));
        Configuration config = new Configuration(base.getResources().getConfiguration());
        Locale locale = Locale.forLanguageTag(tag());
        config.setLocales(new LocaleList(locale));
        config.setLayoutDirection(locale);
        return base.createConfigurationContext(config);
    }

    static void select(Context context, String value) {
        preference = UiLanguagePolicy.normalize(value);
        FrameworkSettings.open(context).edit().putString(KEY, preference).apply();
    }
}
