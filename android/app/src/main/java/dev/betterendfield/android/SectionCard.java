package dev.betterendfield.android;

import android.content.Context;
import android.graphics.Typeface;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;

/**
 * A titled card: small accent eyebrow, title, one sentence of context, then the
 * rows the caller adds.
 *
 * The three ported feature modules each get one of these on the enhancement page,
 * so the page reads as three features rather than one undifferentiated list of
 * switches.
 */
final class SectionCard extends LinearLayout {
    private final LinearLayout body;
    private final TextView status;

    SectionCard(Context context, String eyebrow, String title, String subtitle) {
        super(context);
        setOrientation(VERTICAL);
        setBackgroundResource(R.drawable.bg_card);
        setPadding(dp(16), dp(16), dp(16), dp(16));

        TextView eyebrowView = new TextView(context);
        eyebrowView.setText(eyebrow);
        eyebrowView.setTextSize(11);
        eyebrowView.setLetterSpacing(0.1f);
        eyebrowView.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        eyebrowView.setTextColor(context.getColor(R.color.accent));
        addView(eyebrowView);

        TextView titleView = new TextView(context);
        titleView.setText(title);
        titleView.setTextSize(19);
        titleView.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        titleView.setTextColor(context.getColor(R.color.text_primary));
        LayoutParams titleParams = new LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);
        titleParams.topMargin = dp(4);
        addView(titleView, titleParams);

        TextView subtitleView = new TextView(context);
        subtitleView.setText(subtitle);
        subtitleView.setTextSize(12);
        subtitleView.setLineSpacing(dp(3), 1f);
        subtitleView.setTextColor(context.getColor(R.color.text_muted));
        LayoutParams subtitleParams = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
        subtitleParams.topMargin = dp(6);
        addView(subtitleView, subtitleParams);

        body = new LinearLayout(context);
        body.setOrientation(VERTICAL);
        LayoutParams bodyParams = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
        bodyParams.topMargin = dp(14);
        addView(body, bodyParams);

        status = new TextView(context);
        status.setTextSize(11);
        status.setTypeface(Typeface.MONOSPACE);
        status.setLineSpacing(dp(3), 1f);
        status.setTextColor(context.getColor(R.color.text_muted));
        status.setBackgroundResource(R.drawable.bg_status);
        status.setPadding(dp(12), dp(10), dp(12), dp(10));
        status.setVisibility(GONE);
        LayoutParams statusParams = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
        statusParams.topMargin = dp(12);
        addView(status, statusParams);
    }

    /** Adds a row, spacing it from the previous one. */
    void add(View row) {
        LayoutParams params = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
        if (body.getChildCount() > 0) params.topMargin = dp(8);
        body.addView(row, params);
    }

    /** A small all-caps divider inside the card, for a group of related rows. */
    void addGroupLabel(String label) {
        TextView view = new TextView(getContext());
        view.setText(label);
        view.setTextSize(11);
        view.setLetterSpacing(0.06f);
        view.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        view.setTextColor(getContext().getColor(R.color.text_muted));
        LayoutParams params = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
        params.topMargin = dp(body.getChildCount() > 0 ? 16 : 0);
        params.bottomMargin = dp(2);
        params.leftMargin = dp(2);
        body.addView(view, params);
    }

    void setStatus(String text) {
        status.setText(text);
        status.setVisibility(text == null || text.isEmpty() ? GONE : VISIBLE);
    }

    static LinearLayout.LayoutParams stacked(Context context, int topMarginDp) {
        LinearLayout.LayoutParams params =
                new LinearLayout.LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
        params.topMargin =
                Math.round(topMarginDp * context.getResources().getDisplayMetrics().density);
        return params;
    }

    private int dp(int amount) {
        return Math.round(amount * getResources().getDisplayMetrics().density);
    }
}
