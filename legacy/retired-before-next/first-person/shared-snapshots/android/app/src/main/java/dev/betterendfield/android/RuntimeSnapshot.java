package dev.betterendfield.android;

import java.util.Collections;
import java.util.LinkedHashMap;
import java.util.Map;

/** Native observations, deliberately separate from persisted settings. */
final class RuntimeSnapshot {
    final boolean connected;
    final Map<String, String> values;
    private RuntimeSnapshot(boolean connected, Map<String, String> values) {
        this.connected = connected;
        this.values = Collections.unmodifiableMap(values);
    }
    static RuntimeSnapshot offline() { return new RuntimeSnapshot(false, Map.of()); }
    static RuntimeSnapshot parse(String text) {
        if (text == null || text.length() > 8192 || !text.startsWith("BE_RUNTIME_V1\n")) return offline();
        Map<String, String> values = new LinkedHashMap<>();
        for (String line : text.substring(14).split("\n")) {
            int equals = line.indexOf('=');
            if (equals <= 0 || equals == line.length() - 1) continue;
            values.put(line.substring(0, equals), line.substring(equals + 1));
        }
        return new RuntimeSnapshot(true, values);
    }
    boolean ready(String id) { return "ready".equals(values.get(id)); }
    int number(String name) {
        try { return Integer.parseInt(values.getOrDefault(name, "0")); }
        catch (NumberFormatException invalid) { return 0; }
    }
    boolean cameraAvailable(int bit) {
        return ready("betterendfield.camera") && (number("camera.capabilities") & bit) != 0;
    }
    boolean cameraActive(int bit) { return (number("camera.active") & bit) != 0; }
    boolean hudHidden() { return "1".equals(values.get("ui.hud_hidden")); }
    boolean failed() { return values.values().stream().anyMatch(value -> value.startsWith("failed")); }
    String summary() {
        if (!connected) return "等待运行时连接";
        if (failed()) return "部分模块启动失败 · 长按查看";
        if (!"startup_complete".equals(values.get("runtime"))) return "模块准备中 · 长按查看";
        if (cameraActive(8)) return "时间已冻结 · 再次点按恢复";
        if (cameraActive(1)) return "自由镜头 · 按住移动，划动转向";
        if (cameraActive(2)) return "第一人称视角";
        return "已连接 · 点按切换，按住持续";
    }
}
