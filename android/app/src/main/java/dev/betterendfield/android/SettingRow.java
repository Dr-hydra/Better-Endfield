package dev.betterendfield.android;

import android.content.Context;
import android.content.res.ColorStateList;
import android.graphics.Typeface;
import android.view.Gravity;
import android.view.View;
import android.widget.CompoundButton;
import android.widget.LinearLayout;
import android.widget.Switch;
import android.widget.TextView;

/** Title and switch; the whole row is a touch target. */
final class SettingRow extends LinearLayout {
    private final TextView title;
    private final Switch toggle;
    private boolean silent;

    SettingRow(Context context, String titleText, String descriptionText, String badgeText) {
        super(context);
        setOrientation(HORIZONTAL);
        setGravity(Gravity.CENTER_VERTICAL);
        setBackgroundResource(R.drawable.bg_setting_row);
        setPadding(dp(14), dp(12), dp(10), dp(12));
        setMinimumHeight(dp(64));

        LinearLayout text = new LinearLayout(context);
        text.setOrientation(VERTICAL);

        LinearLayout heading = new LinearLayout(context);
        heading.setOrientation(HORIZONTAL);
        heading.setGravity(Gravity.CENTER_VERTICAL);
        title = new TextView(context);
        title.setText(titleText);
        title.setTextSize(15);
        title.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        title.setTextColor(context.getColor(R.color.text_primary));
        heading.addView(title);

        text.addView(heading);
        addView(text, new LayoutParams(0, LayoutParams.WRAP_CONTENT, 1f));

        toggle = new Switch(context);
        // State lists, not flat colours: an on switch has to be distinguishable
        // from an off one by more than the thumb having slid 20dp to the right.
        toggle.setThumbTintList(context.getColorStateList(R.color.switch_thumb));
        toggle.setTrackTintList(context.getColorStateList(R.color.switch_track));
        toggle.setTrackTintMode(android.graphics.PorterDuff.Mode.SRC_IN);
        // The row carries the label, so the switch itself must not be a second
        // focus stop announcing the same thing twice.
        toggle.setImportantForAccessibility(IMPORTANT_FOR_ACCESSIBILITY_NO);
        toggle.setClickable(false);
        LayoutParams toggleParams = new LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);
        toggleParams.leftMargin = dp(12);
        addView(toggle, toggleParams);

        setOnClickListener(view -> toggle.setChecked(!toggle.isChecked()));
        setContentDescription(titleText);
    }

    boolean isChecked() {
        return toggle.isChecked();
    }

    /** Sets the state without notifying the listener, for initial population. */
    void initialize(boolean checked) {
        silent = true;
        toggle.setChecked(checked);
        silent = false;
    }

    void setChecked(boolean checked) {
        toggle.setChecked(checked);
    }

    void onChanged(CompoundButton.OnCheckedChangeListener listener) {
        toggle.setOnCheckedChangeListener((button, checked) -> {
            refreshEnabledLook();
            if (!silent) listener.onCheckedChanged(button, checked);
        });
        refreshEnabledLook();
    }

    /**
     * Greys out a row whose parent switch is off, so a dependent option reads as
     * unavailable instead of looking like a setting that is being ignored.
     */
    void setAvailable(boolean available) {
        setEnabled(available);
        setAlpha(available ? 1f : 0.42f);
        toggle.setEnabled(available);
    }

    private void refreshEnabledLook() {
        title.setTextColor(getContext().getColor(
                toggle.isChecked() ? R.color.text_primary : R.color.text_secondary));
    }

    @Override
    public void setEnabled(boolean enabled) {
        super.setEnabled(enabled);
        setClickable(enabled);
    }

    private int dp(int value) {
        return Math.round(value * getResources().getDisplayMetrics().density);
    }

    static View spacer(Context context, int heightDp) {
        View view = new View(context);
        view.setLayoutParams(new LayoutParams(LayoutParams.MATCH_PARENT,
                Math.round(heightDp * context.getResources().getDisplayMetrics().density)));
        return view;
    }
}
