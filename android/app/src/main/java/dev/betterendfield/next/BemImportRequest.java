package dev.betterendfield.next;

import android.content.ClipData;
import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.os.Parcelable;

/** Parses one external document, without resolving paths or trusting its MIME/name. */
final class BemImportRequest {
    private BemImportRequest() {}

    static Uri fromIntent(Intent intent) {
        if (intent == null || intent.getAction() == null) return null;
        String action = intent.getAction();
        if (Intent.ACTION_MAIN.equals(action)) return null;
        if (!Intent.ACTION_VIEW.equals(action) && !Intent.ACTION_SEND.equals(action)) {
            throw new IllegalArgumentException("请一次打开或分享一个 BEM 或 ZIP 文件。");
        }
        try {
            Uri candidate = intent.getData();
            if (Intent.ACTION_SEND.equals(action) && intent.hasExtra(Intent.EXTRA_STREAM)) {
                Parcelable stream = streamExtra(intent);
                if (!(stream instanceof Uri)) {
                    throw new IllegalArgumentException("分享内容不是文件，请使用文件管理器的「打开方式」。");
                }
                candidate = merge(candidate, (Uri) stream);
            }
            // Some file managers put the grant and URI only in ClipData. Do not
            // coerce text/Intent items into URIs or silently pick from multiple files.
            ClipData clip = intent.getClipData();
            if (clip != null) {
                if (clip.getItemCount() != 1 || clip.getItemAt(0).getUri() == null) {
                    throw new IllegalArgumentException("请一次选择一个 BEM 或 ZIP 文件，不支持文本或批量分享。");
                }
                candidate = merge(candidate, clip.getItemAt(0).getUri());
            }
            return requireContentUri(candidate);
        } catch (IllegalArgumentException error) {
            throw error;
        } catch (RuntimeException malformed) {
            throw new IllegalArgumentException("无法读取文件打开请求，请从文件管理器重新打开。", malformed);
        }
    }

    static Uri requireContentUri(Uri uri) {
        if (uri == null || !"content".equals(uri.getScheme()) ||
                uri.getAuthority() == null || uri.getAuthority().isEmpty()) {
            throw new IllegalArgumentException("请通过文件管理器分享本地 BEM 或 ZIP 文件；不接受文件路径或网络链接。");
        }
        return uri;
    }

    private static Uri merge(Uri first, Uri second) {
        if (first != null && !first.equals(second)) {
            throw new IllegalArgumentException("文件打开请求包含不同的文件地址，请一次选择一个文件。");
        }
        return second;
    }

    @SuppressWarnings("deprecation")
    private static Parcelable streamExtra(Intent intent) {
        if (Build.VERSION.SDK_INT >= 33) {
            return intent.getParcelableExtra(Intent.EXTRA_STREAM, Uri.class);
        }
        return intent.getParcelableExtra(Intent.EXTRA_STREAM);
    }
}
