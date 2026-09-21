package dev.betterendfield.android;

import android.content.Context;

import java.io.File;
import java.io.FileOutputStream;
import java.nio.charset.StandardCharsets;

/** Atomic, versioned command drop for the game process. Native consumes it on its Unity pump. */
final class ModuleCommandRouter {
    private ModuleCommandRouter() {}

    static boolean issue(Context context, String command, String value) {
        if (command == null || !command.matches("[a-z][a-z0-9_]{1,31}") || value == null
                || value.length() > 512) return false;
        File directory = new File(context.getFilesDir(), "betterendfield/commands");
        if (!directory.isDirectory() && !directory.mkdirs()) return false;
        // Persist the generation across service restarts.  The native pump
        // rejects stale generations, and an in-memory counter would reset to
        // zero whenever Android recreates the overlay service.
        long generation = ModuleSettings.nextCommandGeneration(context);
        String payload = "BE_COMMAND_V1\n" + generation + "\n" + command + "\n" + value + "\n";
        if (FrameworkSettings.writeRemoteCommand(payload)) return true;
        File temporary = new File(directory, "next.tmp");
        File target = new File(directory, "next.command");
        try (FileOutputStream stream = new FileOutputStream(temporary, false)) {
            stream.write(payload.getBytes(StandardCharsets.UTF_8));
            stream.getFD().sync();
        } catch (Exception error) {
            // The overlay must never crash the host app when its command path is unavailable.
            return false;
        }
        return temporary.renameTo(target);
    }

    static String readStatus() { return FrameworkSettings.readRemoteStatus(); }
}
