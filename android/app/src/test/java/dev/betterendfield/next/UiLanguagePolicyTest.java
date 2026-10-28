package dev.betterendfield.next;

public final class UiLanguagePolicyTest {
    public static void main(String[] args) {
        check("zh-CN", "system", "zh");
        check("en", "system", "en");
        check("en", "system", "ja");
        check("en", "system", "ko");
        check("en", "system", "fr");
        check("zh-CN", "zh-CN", "en");
        check("zh-CN", "zh-CN", "ja");
        check("en", "en", "zh");
        check("zh-CN", null, "zh");
        check("en", "invalid", "fr");
        if (!"system".equals(UiLanguagePolicy.normalize(null))) throw new AssertionError();
        if (!"system".equals(UiLanguagePolicy.normalize("invalid"))) throw new AssertionError();
        System.out.println("UiLanguagePolicyTest: 12 checks passed");
    }

    private static void check(String expected, String preference, String systemLanguage) {
        String actual = UiLanguagePolicy.resolve(preference, systemLanguage);
        if (!expected.equals(actual)) throw new AssertionError(expected + " != " + actual);
    }
}
