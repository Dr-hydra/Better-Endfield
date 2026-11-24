package dev.betterendfield.next;

import android.content.ContentProvider;
import android.content.ContentValues;
import android.database.Cursor;
import android.net.Uri;
import android.os.ParcelFileDescriptor;

import java.io.File;
import java.io.FileNotFoundException;

/**
 * Read-only access to exported diagnostics. The authority is not exported; the
 * About page grants a one-shot read URI to the share target instead.
 */
public final class DiagnosticsProvider extends ContentProvider {
    static final String AUTHORITY = "dev.betterendfield.next.diagnostics";

    @Override public boolean onCreate() {
        return true;
    }

    @Override public ParcelFileDescriptor openFile(Uri uri, String mode) throws FileNotFoundException {
        if (!"r".equals(mode)) throw new FileNotFoundException("read only");
        String name = uri.getLastPathSegment();
        if (name == null || name.isEmpty() || name.contains("..")
                || name.contains("/") || name.contains("\\"))
            throw new FileNotFoundException("invalid diagnostics name");
        if (getContext() == null) throw new FileNotFoundException("no context");
        File file = new File(new File(getContext().getCacheDir(), DiagnosticsExport.DIRECTORY), name);
        if (!file.isFile()) throw new FileNotFoundException(name);
        return ParcelFileDescriptor.open(file, ParcelFileDescriptor.MODE_READ_ONLY);
    }

    @Override public String getType(Uri uri) {
        return "text/plain";
    }

    @Override public Cursor query(Uri uri, String[] projection, String selection,
            String[] selectionArgs, String sortOrder) {
        return null;
    }

    @Override public Uri insert(Uri uri, ContentValues values) {
        return null;
    }

    @Override public int delete(Uri uri, String selection, String[] selectionArgs) {
        return 0;
    }

    @Override public int update(Uri uri, ContentValues values, String selection, String[] selectionArgs) {
        return 0;
    }
}
