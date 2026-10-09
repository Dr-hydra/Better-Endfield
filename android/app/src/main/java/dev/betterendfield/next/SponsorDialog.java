package dev.betterendfield.next;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.ContentResolver;
import android.content.ContentValues;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Environment;
import android.provider.MediaStore;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.Toast;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

/** Local channel chooser; payment is handled by each provider. */
final class SponsorDialog {
    private static final String AFDIAN = "https://afdian.com/u/e9a7e6ac6fa411ed980752540025c377";
    private static final String PAYPAL = "https://paypal.me/hydra405";

    private SponsorDialog() {}

    static void show(Activity activity) {
        LinearLayout channels = new LinearLayout(activity);
        channels.setOrientation(LinearLayout.VERTICAL);
        channels.setPadding(dp(activity, 20), dp(activity, 8), dp(activity, 20), 0);
        addChannel(activity, channels, R.string.sponsor_wechat, () -> showWechat(activity));
        addChannel(activity, channels, R.string.sponsor_afdian, () -> open(activity, AFDIAN));
        addChannel(activity, channels, R.string.sponsor_paypal, () -> open(activity, PAYPAL));
        ScrollView scroll = new ScrollView(activity);
        scroll.addView(channels);
        new AlertDialog.Builder(activity).setTitle(R.string.sponsor).setView(scroll)
                .setNegativeButton(R.string.sponsor_close, null).show();
    }

    private static void addChannel(Activity activity, LinearLayout parent, int title, Runnable action) {
        Button button = new Button(activity);
        button.setText(title);
        button.setTextSize(16);
        button.setAllCaps(false);
        button.setTextColor(activity.getColor(R.color.text_primary));
        button.setBackgroundResource(R.drawable.bg_ghost_button);
        button.setBackgroundTintList(null);
        button.setMinimumHeight(dp(activity, 52));
        LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(-1, -2);
        params.bottomMargin = dp(activity, 10);
        parent.addView(button, params);
        button.setOnClickListener(view -> action.run());
    }

    private static void showWechat(Activity activity) {
        ImageView image = new ImageView(activity);
        try (InputStream input = activity.getResources().openRawResource(R.raw.sponsor_wechat)) {
            Bitmap bitmap = BitmapFactory.decodeStream(input);
            if (bitmap == null) throw new IOException("Image decoding failed");
            image.setImageBitmap(bitmap);
        } catch (IOException | RuntimeException failure) {
            Toast.makeText(activity, R.string.sponsor_load_failed, Toast.LENGTH_SHORT).show();
            return;
        }
        image.setContentDescription(activity.getString(R.string.sponsor_wechat));
        image.setAdjustViewBounds(true);
        image.setScaleType(ImageView.ScaleType.FIT_CENTER);
        image.setPadding(dp(activity, 16), dp(activity, 8), dp(activity, 16), dp(activity, 8));
        ScrollView scroll = new ScrollView(activity);
        scroll.addView(image, new ViewGroup.LayoutParams(-1, -2));
        AlertDialog dialog = new AlertDialog.Builder(activity).setTitle(R.string.sponsor_wechat)
                .setView(scroll).setPositiveButton(R.string.sponsor_save, null)
                .setNegativeButton(R.string.sponsor_close, null).create();
        dialog.setOnShowListener(ignored -> dialog.getButton(AlertDialog.BUTTON_POSITIVE)
                .setOnClickListener(view -> saveWechat(activity, dialog)));
        dialog.show();
    }

    private static void saveWechat(Activity activity, AlertDialog dialog) {
        Button save = dialog.getButton(AlertDialog.BUTTON_POSITIVE);
        save.setEnabled(false);
        save.setText(R.string.sponsor_saving);
        Context context = activity.getApplicationContext();
        new Thread(() -> {
            boolean saved = writeImage(context);
            activity.runOnUiThread(() -> {
                if (activity.isDestroyed() || activity.isFinishing()) return;
                Toast.makeText(context, saved ? R.string.sponsor_saved : R.string.sponsor_save_failed,
                        Toast.LENGTH_LONG).show();
                if (dialog.isShowing()) {
                    save.setEnabled(true);
                    save.setText(R.string.sponsor_save);
                }
            });
        }, "SponsorImageSave").start();
    }

    private static boolean writeImage(Context context) {
        ContentResolver resolver = context.getContentResolver();
        Uri image = null;
        try {
            ContentValues values = new ContentValues();
            values.put(MediaStore.Images.Media.DISPLAY_NAME, "Hydra-WeChat-Support.png");
            values.put(MediaStore.Images.Media.MIME_TYPE, "image/png");
            values.put(MediaStore.Images.Media.RELATIVE_PATH,
                    Environment.DIRECTORY_PICTURES + "/BetterEndfieldNext");
            values.put(MediaStore.Images.Media.IS_PENDING, 1);
            image = resolver.insert(MediaStore.Images.Media.EXTERNAL_CONTENT_URI, values);
            if (image == null) throw new IOException("Image destination unavailable");
            try (InputStream input = context.getResources().openRawResource(R.raw.sponsor_wechat);
                 OutputStream output = resolver.openOutputStream(image)) {
                if (output == null) throw new IOException("Image output unavailable");
                byte[] buffer = new byte[8192];
                int count;
                while ((count = input.read(buffer)) != -1) output.write(buffer, 0, count);
            }
            values.clear();
            values.put(MediaStore.Images.Media.IS_PENDING, 0);
            if (resolver.update(image, values, null, null) != 1)
                throw new IOException("Image publication failed");
            return true;
        } catch (IOException | RuntimeException failure) {
            if (image != null) {
                try { resolver.delete(image, null, null); }
                catch (RuntimeException ignored) {}
            }
            return false;
        }
    }

    private static void open(Activity activity, String url) {
        try { activity.startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse(url))); }
        catch (RuntimeException unavailable) {
            Toast.makeText(activity, R.string.sponsor_open_failed, Toast.LENGTH_SHORT).show();
        }
    }

    private static int dp(Context context, int value) {
        return Math.round(value * context.getResources().getDisplayMetrics().density);
    }
}
