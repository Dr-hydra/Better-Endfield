package dev.betterendfield.android;

import android.app.Activity;
import android.os.Bundle;
import android.graphics.Insets;
import android.os.Build;
import android.view.Gravity;
import android.view.WindowInsets;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.ScrollView;

/** Standalone third-party module manager opened from the enhancement page. */
public final class ThirdPartyModulesActivity extends Activity {
    private ThirdPartyModulesPage page;
    private ScrollView scroll;

    @Override protected void onCreate(Bundle state) {
        super.onCreate(state);
        setTitle("第三方模块");
        scroll = new ScrollView(this);
        scroll.setFillViewport(true);
        scroll.setBackgroundColor(getColor(R.color.app_background));
        LinearLayout content = new LinearLayout(this);
        content.setOrientation(LinearLayout.VERTICAL);
        content.setPadding(dp(16), dp(16), dp(16), dp(24));
        scroll.addView(content, new FrameLayout.LayoutParams(-1, -2, Gravity.CENTER_HORIZONTAL));
        Button back = new Button(this);
        back.setText("‹ 返回增强功能");
        back.setAllCaps(false);
        back.setOnClickListener(view -> finish());
        content.addView(back, SectionCard.stacked(this, 0));
        LinearLayout root = new LinearLayout(this);
        content.addView(root, SectionCard.stacked(this, 12));
        page = new ThirdPartyModulesPage(this, root);
        setContentView(scroll);
        scroll.setOnApplyWindowInsetsListener((view, insets) -> {
            if (Build.VERSION.SDK_INT >= 30) {
                Insets safe = insets.getInsets(WindowInsets.Type.systemBars()
                        | WindowInsets.Type.displayCutout() | WindowInsets.Type.ime());
                view.setPadding(safe.left, safe.top, safe.right, safe.bottom);
            } else {
                view.setPadding(insets.getSystemWindowInsetLeft(), insets.getSystemWindowInsetTop(),
                        insets.getSystemWindowInsetRight(), insets.getSystemWindowInsetBottom());
            }
            return insets;
        });
        scroll.addOnLayoutChangeListener((view, left, top, right, bottom,
                oldLeft, oldTop, oldRight, oldBottom) -> {
            FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) content.getLayoutParams();
            int width = Math.min(Math.max(0, right - left - scroll.getPaddingLeft()
                    - scroll.getPaddingRight()), dp(860));
            if (params.width != width) { params.width = width; content.setLayoutParams(params); }
        });
        int restoredY = state == null ? 0 : state.getInt("scroll_y", 0);
        scroll.post(() -> scroll.post(() -> scroll.scrollTo(0, restoredY)));
        scroll.requestApplyInsets();
    }

    @Override protected void onResume() {
        super.onResume();
        if (page != null) page.render();
    }

    @Override protected void onActivityResult(int request, int result, android.content.Intent data) {
        super.onActivityResult(request, result, data);
        if (page != null) page.onActivityResult(request, result, data);
    }

    @Override protected void onDestroy() {
        if (page != null) page.close();
        super.onDestroy();
    }

    @Override protected void onSaveInstanceState(Bundle state) {
        if (scroll != null) state.putInt("scroll_y", scroll.getScrollY());
        super.onSaveInstanceState(state);
    }

    private int dp(int value) { return Math.round(value * getResources().getDisplayMetrics().density); }
}
