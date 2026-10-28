package dev.betterendfield.next;

/** One language policy shared by resources, dynamic text, and the preview. */
final class UiLanguagePolicy {
    static String normalize(String value) {
        return "zh-CN".equals(value) || "en".equals(value) ? value : "system";
    }

    static String resolve(String preference, String systemLanguage) {
        String value = normalize(preference);
        if (!"system".equals(value)) return value;
        return "zh".equals(systemLanguage) ? "zh-CN" : "en";
    }
}
