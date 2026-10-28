package dev.betterendfield.next;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Intent;
import android.net.Uri;
import android.os.Handler;
import android.os.Looper;
import android.view.Gravity;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONObject;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/** Desktop About content, with manual update checks for Android APK releases. */
final class AboutPage extends LinearLayout {
    private static final String REPOSITORY = "https://github.com/Dr-hydra/Better-Endfield";
    private static final String RELEASES = REPOSITORY + "/releases";
    private static final String QQ_GROUP = "851586605";
    private static final String DISCORD_COMMUNITY = "https://discord.gg/Hadjs5XvS";
    private static String disclaimer() { return
            Text.of("本软件会将本机代码注入游戏进程，并在运行时修改模型、动画和语音资源选择。\n\n")
            + Text.of("可能的风险包括游戏崩溃、存档或配置异常、更新后失效，以及被游戏安全或反作弊系统识别。使用在线账号可能产生账号限制风险。\n\n")
            + Text.of("本项目为非官方实验工具，与鹰角网络、峘形山工作室及 GRYPHLINE 无关，也不提供任何形式的担保。请自行备份重要数据，遵守游戏服务条款，并自行承担使用后果。\n\n")
            + Text.of("本软件不负责停用或绕过反作弊组件；游戏更新后如签名不匹配，相关 Hook 应停止使用，等待适配。");
    }
    private final Activity activity;
    private final Handler main = new Handler(Looper.getMainLooper());
    private final ExecutorService worker = Executors.newSingleThreadExecutor();
    private final TextView updateResult;
    private final Button checkUpdates, openRelease;
    private volatile boolean closed;
    private String releaseUrl = RELEASES;

    AboutPage(Activity activity) {
        super(activity);
        this.activity = activity;
        setOrientation(VERTICAL);
        LinearLayout header = new LinearLayout(activity);
        header.setGravity(Gravity.CENTER_VERTICAL);
        header.setPadding(dp(4), dp(8), dp(4), dp(12));
        ImageView icon = new ImageView(activity);
        icon.setImageResource(R.drawable.gilberta);
        icon.setScaleType(ImageView.ScaleType.CENTER_CROP);
        icon.setContentDescription("Better Endfield Next");
        header.addView(icon, new LayoutParams(dp(64), dp(64)));
        LinearLayout name = new LinearLayout(activity);
        name.setOrientation(VERTICAL); name.setPadding(dp(16), 0, 0, 0);
        name.addView(text("Better Endfield Next", 24));
        name.addView(text(Text.of("终末地登录场景模型与角色配音控制器"), 14));
        name.addView(text(Text.of("版本 ") + BuildConfig.VERSION_NAME, 14));
        header.addView(name, new LayoutParams(0, -2, 1));
        addView(header);

        SectionCard language = card("语言 / Language");
        String[] languageValues = {"system", "zh-CN", "en"};
        String[] languageLabels = {"跟随系统 / System", "简体中文", "English"};
        String selectedLanguage = UiLanguagePolicy.normalize(
                FrameworkSettings.open(activity).getString(AppLanguage.KEY, "system"));
        int selectedIndex = java.util.Arrays.asList(languageValues).indexOf(selectedLanguage);
        language.add(button(languageLabels[selectedIndex], () -> new AlertDialog.Builder(activity)
                .setTitle("语言 / Language")
                .setSingleChoiceItems(languageLabels, selectedIndex, (dialog, which) -> {
                    dialog.dismiss();
                    if (languageValues[which].equals(selectedLanguage)) return;
                    AppLanguage.select(activity, languageValues[which]);
                    activity.recreate();
                }).setNegativeButton(Text.of("取消"), null).show()));

        SectionCard updates = card(Text.of("软件更新"));
        android.content.SharedPreferences prefs = FrameworkSettings.open(activity);
        SettingRow autoCheckRow = new SettingRow(activity, Text.of("启动时自动检查更新"), Text.of("检查软件版本并自动更新未固定的资源，预览包首次按需下载"), null);
        autoCheckRow.initialize(prefs.getBoolean(ModuleSettings.AUTO_CHECK_UPDATES, true));
        autoCheckRow.onChanged((button, checked) -> prefs.edit().putBoolean(ModuleSettings.AUTO_CHECK_UPDATES, checked).apply());
        updates.add(autoCheckRow);

        checkUpdates = button(Text.of("检查更新"), this::checkUpdates);
        updates.add(checkUpdates);
        updateResult = text("", 14); updateResult.setVisibility(GONE);
        updates.add(updateResult);
        openRelease = button(Text.of("打开下载页"), () -> open(releaseUrl));
        openRelease.setVisibility(GONE); updates.add(openRelease);

        SectionCard project = card(Text.of("项目"));
        project.add(button(Text.of("GitHub 仓库"), () -> open(REPOSITORY)));
        project.add(button(Text.of("全部版本"), () -> open(RELEASES)));
        project.add(button(Text.of("GNU AGPL v3.0 开源许可"), () -> open(REPOSITORY + "/blob/main/LICENSE")));

        SectionCard author = card(Text.of("作者与交流"));
        author.add(button(Text.of("打开 B站主页"), () -> open("https://space.bilibili.com/441133155")));
        author.add(button(Text.of("打开小黑盒主页"), () -> open("https://www.xiaoheihe.cn/app/user/profile/38080236")));
        LinearLayout group = new LinearLayout(activity); group.setGravity(Gravity.CENTER_VERTICAL);
        TextView number = text(Text.of("QQ群  ") + QQ_GROUP, 16); number.setTextIsSelectable(true);
        group.addView(number, new LayoutParams(0, -2, 1));
        group.addView(button(Text.of("复制"), () -> {
            ClipboardManager clipboard = (ClipboardManager) activity.getSystemService(Activity.CLIPBOARD_SERVICE);
            if (clipboard != null) clipboard.setPrimaryClip(ClipData.newPlainText(Text.of("QQ群"), QQ_GROUP));
        }));
        author.add(group);
        author.add(button(Text.of("Discord 社区"), () -> open(DISCORD_COMMUNITY)));

        SectionCard disclaimer = card(Text.of("风险与免责声明"));
        disclaimer.add(text(Text.of("本项目为非官方实验工具，与鹰角网络、峘形山工作室及 GRYPHLINE 无关。注入和运行时修改可能造成游戏崩溃、版本不兼容或账号风险。"), 14));
        disclaimer.add(button(Text.of("查看完整说明"), () -> new AlertDialog.Builder(activity)
                .setTitle(Text.of("风险与免责声明")).setMessage(disclaimer())
                .setPositiveButton(Text.of("关闭"), null).show()));
    }

    private SectionCard card(String title) {
        SectionCard card = new SectionCard(activity, "", title, "");
        addView(card, SectionCard.stacked(activity, 12)); return card;
    }
    private TextView text(String value, float size) {
        TextView view = new TextView(activity); view.setText(value); view.setTextSize(size);
        view.setTextColor(activity.getColor(R.color.text_primary));
        view.setLineSpacing(dp(3), 1); return view;
    }
    private Button button(String title, Runnable action) {
        Button button = new Button(activity); button.setText(title); button.setAllCaps(false);
        button.setTextColor(activity.getColor(R.color.text_primary));
        button.setBackgroundResource(R.drawable.bg_ghost_button); button.setMinimumHeight(dp(48));
        button.setOnClickListener(v -> action.run()); return button;
    }
    private void open(String url) {
        try { activity.startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse(url))); }
        catch (RuntimeException unavailable) { Toast.makeText(activity, Text.of("无法打开链接"), Toast.LENGTH_SHORT).show(); }
    }
    private void checkUpdates() {
        if (closed) return;
        checkUpdates.setEnabled(false); checkUpdates.setText(Text.of("正在检查…"));
        updateResult.setVisibility(GONE); openRelease.setVisibility(GONE);
        worker.execute(() -> {
            String message, link = RELEASES;
            try {
                AppUpdateChecker.Result result = AppUpdateChecker.check();
                message = result.message;
                link = result.releaseUrl;
            } catch (Exception failure) { message = Text.of("检查更新失败 · ") + failure.getMessage(); }
            final String result = message, target = link;
            main.post(() -> {
                if (closed || activity.isDestroyed()) return;
                releaseUrl = target; updateResult.setText(result); updateResult.setVisibility(VISIBLE);
                checkUpdates.setText(Text.of("检查更新")); checkUpdates.setEnabled(true); openRelease.setVisibility(VISIBLE);
            });
        });
    }
    void close() { closed = true; main.removeCallbacksAndMessages(null); worker.shutdownNow(); }
    private int dp(int value) { return Math.round(value * getResources().getDisplayMetrics().density); }
}
