package dev.betterendfield.next;

import android.app.Activity;
import android.content.Context;

/** Applies the module locale without changing the game's process-wide Locale. */
abstract class ModuleActivity extends Activity {
    private String displayedLanguage;

    @Override protected void attachBaseContext(Context base) {
        Context localized = AppLanguage.wrap(base);
        displayedLanguage = AppLanguage.tag();
        super.attachBaseContext(localized);
    }

    @Override protected void onResume() {
        super.onResume();
        if (!AppLanguage.tag().equals(displayedLanguage)) recreate();
    }
}
