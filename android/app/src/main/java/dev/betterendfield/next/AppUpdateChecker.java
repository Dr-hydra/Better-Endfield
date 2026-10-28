package dev.betterendfield.next;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.util.Locale;

final class AppUpdateChecker {
    static final String REPOSITORY = "https://github.com/Dr-hydra/Better-Endfield";
    static final String RELEASES = REPOSITORY + "/releases";

    static final class Result {
        final boolean isUpdateAvailable;
        final String latestVersion;
        final String currentVersion;
        final String releaseUrl;
        final String message;

        Result(boolean isUpdateAvailable, String latestVersion, String currentVersion, String releaseUrl, String message) {
            this.isUpdateAvailable = isUpdateAvailable;
            this.latestVersion = latestVersion;
            this.currentVersion = currentVersion;
            this.releaseUrl = releaseUrl;
            this.message = message;
        }
    }

    static Result check() throws Exception {
        JSONObject distribution=DistributionUpdater.index();
        if(distribution!=null) {
            JSONObject software=distribution.getJSONObject("software");
            String version=software.getString("version"),link=software.getString("releaseUrl");
            if(!link.startsWith(RELEASES+"/"))throw new java.io.IOException("Invalid release URL");
            boolean available=compareVersions(version,BuildConfig.VERSION_NAME)>0;
            return new Result(available,version,BuildConfig.VERSION_NAME,link,available?Text.of("发现新版本 ")+version:Text.of("已是最新版本 · ")+BuildConfig.VERSION_NAME);
        }
        HttpURLConnection connection = null;
        try {
            connection = (HttpURLConnection) new URL("https://api.github.com/repos/Dr-hydra/Better-Endfield/releases?per_page=30").openConnection();
            connection.setConnectTimeout(12000);
            connection.setReadTimeout(12000);
            connection.setRequestProperty("User-Agent", "BetterEndfieldNext-Android/" + BuildConfig.VERSION_NAME);
            connection.setRequestProperty("Accept", "application/vnd.github+json");
            int code = connection.getResponseCode();
            if (code != 200) throw new java.io.IOException("HTTP " + code);
            ByteArrayOutputStream bytes = new ByteArrayOutputStream();
            try (InputStream input = connection.getInputStream()) {
                byte[] buffer = new byte[8192];
                int count;
                while ((count = input.read(buffer)) != -1) {
                    if (bytes.size() + count > 2 * 1024 * 1024) throw new java.io.IOException(Text.of("更新数据过大"));
                    bytes.write(buffer, 0, count);
                }
            }
            JSONArray releases = new JSONArray(new String(bytes.toByteArray(), StandardCharsets.UTF_8));
            JSONObject latest = null;
            for (int i = 0; i < releases.length() && latest == null; i++) {
                JSONObject release = releases.getJSONObject(i);
                if (release.optBoolean("draft") || release.optBoolean("prerelease")) continue;
                JSONArray assets = release.optJSONArray("assets");
                if (assets == null) continue;
                for (int j = 0; j < assets.length(); j++) {
                    if (assets.getJSONObject(j).optString("name").toLowerCase(Locale.ROOT).endsWith(".apk")) {
                        latest = release;
                        break;
                    }
                }
            }
            if (latest == null) {
                return new Result(false, null, BuildConfig.VERSION_NAME, RELEASES, Text.of("暂无 Android 发布版本"));
            }
            String version = latest.getString("tag_name");
            String link = RELEASES;
            String candidate = latest.optString("html_url", RELEASES);
            if (candidate.startsWith(RELEASES + "/")) link = candidate;
            boolean available = compareVersions(version, BuildConfig.VERSION_NAME) > 0;
            String message = available ? Text.of("发现新版本 ") + version : Text.of("已是最新版本 · ") + BuildConfig.VERSION_NAME;
            return new Result(available, version, BuildConfig.VERSION_NAME, link, message);
        } finally {
            if (connection != null) connection.disconnect();
        }
    }

    static int compareVersions(String left, String right) {
        String[] a = left.replaceFirst("^[vV]", "").split("[.+-]");
        String[] b = right.replaceFirst("^[vV]", "").split("[.+-]");
        for (int i = 0; i < 3; i++) {
            int comparison = Long.compare(i < a.length ? Long.parseLong(a[i]) : 0, i < b.length ? Long.parseLong(b[i]) : 0);
            if (comparison != 0) return comparison;
        }
        return 0;
    }
}
