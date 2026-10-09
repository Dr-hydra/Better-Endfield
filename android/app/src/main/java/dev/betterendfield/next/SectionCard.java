package dev.betterendfield.next;

import android.content.Context;
import android.graphics.Typeface;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;

/** A compact settings card. Legacy description arguments do not create views. */
final class SectionCard extends LinearLayout {
    private final LinearLayout body;

    SectionCard(Context context, String eyebrow, String title, String subtitle) {
        super(context);
        setOrientation(VERTICAL);
        setBackgroundResource(R.drawable.bg_card);
        setPadding(dp(16), dp(16), dp(16), dp(16));

        TextView titleView = new TextView(context);
        titleView.setText(title);
        titleView.setTextSize(19);
        titleView.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        titleView.setTextColor(context.getColor(R.color.text_primary));
        LayoutParams titleParams = new LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);
        addView(titleView, titleParams);

        body = new LinearLayout(context);
        body.setOrientation(VERTICAL);
        LayoutParams bodyParams = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
        bodyParams.topMargin = dp(14);
        addView(body, bodyParams);


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
