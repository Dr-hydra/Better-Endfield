# Better Endfield Next 4.2.5

Windows / Android 软件版本 4.2.5，Android versionCode 40205。

## 启动崩溃修复（Android）

- **修复 Android 端因调用 `Files.readString` / `Files.writeString` 在部分设备（`core-oj.jar` 未实现该 Java 11 API 或 Android 10–13 设备）上启动即抛 `NoSuchMethodError` 闪退的问题。**
  - [`GameResourceStore.ensureBundledCore`](android/app/src/main/java/dev/betterendfield/next/GameResourceStore.java) 改用 `new String(Files.readAllBytes(...), StandardCharsets.UTF_8)` 读取 `.bundled-stamp`。
  - [`DistributionUpdater.resume`](android/app/src/main/java/dev/betterendfield/next/DistributionUpdater.java) 改用 `Files.write(..., active.toString().getBytes(StandardCharsets.UTF_8))` 写回状态文件。
