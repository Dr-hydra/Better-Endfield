package dev.betterendfield.next;

import android.content.Context;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Build;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.concurrent.TimeUnit;

/**
 * Bounded, read-only collection of the module's own status record and the
 * injected game process logs. The root shell only runs fixed read commands;
 * nothing here writes into the game's directory.
 *
 * <p>One superuser call collects every injected file plus logcat. KernelSU and
 * Magisk both prompt per call when the grant is "this time only", so splitting
 * this into one call per file would queue several prompts and the later reads
 * would time out while the export thread waits.
 */
final class DiagnosticsExport {
    static final String GAME_PACKAGE = "com.hypergryph.endfield";
    static final String DIRECTORY = "diagnostics";
    private static final String MARK = "@@";
    private static final int MAX_REPORT_BYTES = 4 * 1024 * 1024;
    private static final int MAX_GAME_BYTES = 3 * 1024 * 1024;
    private static final int MAX_LOGCAT_BYTES = 768 * 1024;
    private static final int SHELL_TIMEOUT_SECONDS = 60;

    private DiagnosticsExport() {}

    static File export(Context context) throws IOException {
        File directory = new File(context.getCacheDir(), DIRECTORY);
        if (!directory.isDirectory() && !directory.mkdirs())
            throw new IOException(Text.of("无法创建日志目录"));
        String stamp = new SimpleDateFormat("yyyyMMdd-HHmmss", Locale.US).format(new Date());
        File target = new File(directory, "bem-diagnostics-" + stamp + ".txt");
        StringBuilder report = new StringBuilder(200_000);
        appendHeader(context, report);
        appendLocal(context, report);
        appendInjected(report);
        byte[] bytes = report.toString().getBytes(StandardCharsets.UTF_8);
        try (FileOutputStream stream = new FileOutputStream(target)) {
            stream.write(bytes, 0, Math.min(bytes.length, MAX_REPORT_BYTES));
        }
        return target;
    }

    static Uri shareUri(File file) {
        return new Uri.Builder().scheme("content").authority(DiagnosticsProvider.AUTHORITY)
                .appendPath(file.getName()).build();
    }

    private static void appendHeader(Context context, StringBuilder report) {
        report.append("==== Better Endfield Next diagnostics ====\n");
        report.append("exported: ").append(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss Z", Locale.US)
                .format(new Date())).append('\n');
        report.append("app: ").append(BuildConfig.VERSION_NAME).append('\n');
        report.append("android: ").append(Build.VERSION.RELEASE)
                .append(" (SDK ").append(Build.VERSION.SDK_INT).append(")\n");
        report.append("device: ").append(Build.MANUFACTURER).append(' ').append(Build.MODEL).append('\n');
        report.append("framework: connected=").append(FrameworkSettings.isConnected()).append('\n');
        try {
            report.append("game: ").append(context.getPackageManager()
                    .getPackageInfo(GAME_PACKAGE, 0).versionName).append('\n');
        } catch (PackageManager.NameNotFoundException missing) {
            report.append("game: not installed\n");
        }
        report.append('\n');
    }

    private static void appendLocal(Context context, StringBuilder report) {
        report.append("---- module overlay-settings.log ----\n");
        File file = new File(context.getCacheDir(), "betterendfieldnext-overlay-settings.log");
        if (!file.isFile()) {
            report.append("(missing)\n\n");
            return;
        }
        try (BufferedReader reader = new BufferedReader(new InputStreamReader(
                new FileInputStream(file), StandardCharsets.UTF_8))) {
            String line;
            int total = 0;
            while ((line = reader.readLine()) != null && total < 32 * 1024) {
                report.append(line).append('\n');
                total += line.length() + 1;
            }
        } catch (IOException unreadable) {
            report.append("(unreadable: ").append(unreadable.getMessage()).append(")\n");
        }
        report.append('\n');
    }

    private static void appendInjected(StringBuilder report) {
        String[] names = {"betterendfieldnext-diagnostics.log",
                "betterendfieldnext-resources.log", "betterendfieldnext-overlay-settings.log"};
        List<String> packages = targetPackages();
        StringBuilder script = new StringBuilder(2048);
        script.append("echo \"").append(MARK).append("PROBE\"\n").append("id 2>&1\n");
        for (int i = 0; i < packages.size(); ++i) {
            String pkg = packages.get(i);
            String base = "BASE" + i;
            // The application mount namespace hides other packages under
            // /data/user/0 on some ROMs. /proc/1/root resolves through init's
            // namespace, so the injected files stay readable there.
            script.append(base).append("=/proc/1/root/data/user/0/").append(pkg).append("/cache\n");
            script.append("[ -d \"$").append(base).append("\" ] || ").append(base)
                    .append("=/data/user/0/").append(pkg).append("/cache\n");
            script.append("echo \"").append(MARK).append(pkg).append("|@base\"\n");
            script.append("echo \"$").append(base).append("\"\n");
            script.append("[ -d \"$").append(base).append("\" ] && echo present || echo missing\n");
            for (String name : names) {
                script.append("echo \"").append(MARK).append(pkg).append('|').append(name).append("\"\n");
                // Keep the newest bytes: the interesting events are at the end
                // of a rotating log, and a plain cat would keep the stale head.
                script.append("tail -c ").append(MAX_GAME_BYTES / names.length)
                        .append(" \"$").append(base).append('/').append(name).append("\" 2>&1\n");
            }
        }
        script.append("echo \"").append(MARK).append("LOGCAT\"\n");
        script.append("logcat -d -v threadtime -t 4000 -s "
                + "BetterEndfieldNext:V BetterEndfieldNext.Resources:V 2>&1"
                + " | tail -c " + MAX_LOGCAT_BYTES + "\n");
        Map<String, String> sections = splitSections(runShell(script.toString()));

        report.append("---- injected process ----\n");
        String probe = sections.getOrDefault("PROBE", "").trim();
        report.append("identity: ").append(probe.isEmpty() ? "(no output)" : probe).append('\n');
        if (!probe.contains("uid=0")) {
            report.append("(root unavailable; allow the superuser prompt and export again)\n\n");
            return;
        }
        for (String pkg : packages) {
            report.append("== ").append(pkg).append(" ==\n");
            String[] base = sections.getOrDefault(pkg + "|@base", "").trim().split("\n");
            boolean present = base.length > 1 && base[base.length - 1].trim().equals("present");
            if (!present) {
                report.append("(no injected cache directory)\n");
                continue;
            }
            report.append("cache: ").append(base[0].trim()).append('\n');
            for (String name : names) {
                report.append("-- ").append(name).append(" --\n");
                String content = sections.getOrDefault(pkg + "|" + name, "");
                if (content.trim().isEmpty()) report.append("(missing or empty)\n");
                else report.append(bounded(content, MAX_GAME_BYTES / names.length + 1024));
            }
        }
        report.append("-- logcat --\n");
        String logcat = sections.getOrDefault("LOGCAT", "");
        if (logcat.trim().isEmpty())
            report.append("(empty: the game was not running, or the ring buffer already rotated)\n");
        else report.append(bounded(logcat, MAX_LOGCAT_BYTES));
        report.append('\n');
    }

    /**
     * Framework scope first, then the known package. The scope list is the only
     * source that also covers a second installed region or a user-added package.
     */
    private static List<String> targetPackages() {
        List<String> candidates = new ArrayList<>(FrameworkSettings.scope());
        if (!candidates.contains(GAME_PACKAGE)) candidates.add(0, GAME_PACKAGE);
        List<String> valid = new ArrayList<>();
        for (String pkg : candidates) {
            if (pkg == null || !pkg.matches("[a-zA-Z0-9_.]{1,128}") || valid.contains(pkg)) continue;
            valid.add(pkg);
            if (valid.size() >= 8) break;
        }
        if (valid.isEmpty()) valid.add(GAME_PACKAGE);
        return valid;
    }

    private static Map<String, String> splitSections(String output) {
        Map<String, String> sections = new LinkedHashMap<>();
        String[] parts = output.split("(?m)^" + MARK);
        for (int i = 1; i < parts.length; ++i) {
            int newline = parts[i].indexOf('\n');
            if (newline < 0) continue;
            sections.put(parts[i].substring(0, newline).trim(), parts[i].substring(newline + 1));
        }
        return sections;
    }

    private static String bounded(String content, int limit) {
        if (content.length() <= limit) return content.endsWith("\n") ? content : content + "\n";
        return content.substring(0, limit) + "\n(truncated)\n";
    }

    /**
     * Runs one fixed read-only script through the superuser shell. The reader
     * thread is separate so a full pipe cannot deadlock the timeout.
     */
    private static String runShell(String command) {
        Process process;
        try {
            process = new ProcessBuilder("su", "0", "sh", "-c", command)
                    .redirectErrorStream(true).start();
        } catch (IOException unavailable) {
            return "";
        }
        StringBuilder value = new StringBuilder();
        Thread reader = new Thread(() -> {
            try (BufferedReader input = new BufferedReader(new InputStreamReader(
                    process.getInputStream(), StandardCharsets.UTF_8))) {
                String line;
                int total = 0;
                while ((line = input.readLine()) != null && total < MAX_REPORT_BYTES) {
                    synchronized (value) { value.append(line).append('\n'); }
                    total += line.length() + 1;
                }
            } catch (IOException ignored) {
                // The process was destroyed on timeout.
            }
        }, "bem-diagnostics-reader");
        reader.setDaemon(true);
        reader.start();
        try {
            if (!process.waitFor(SHELL_TIMEOUT_SECONDS, TimeUnit.SECONDS)) {
                process.destroy();
                return "";
            }
            reader.join(10_000);
        } catch (InterruptedException interrupted) {
            Thread.currentThread().interrupt();
            process.destroy();
            return "";
        }
        synchronized (value) { return value.toString(); }
    }
}
