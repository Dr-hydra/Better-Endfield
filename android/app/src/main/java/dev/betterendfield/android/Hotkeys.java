package dev.betterendfield.android;

/**
 * Windows virtual-key codes the ported desktop modules poll for.
 *
 * The desktop UI and camera modules decide what to do by reading
 * {@code GetAsyncKeyState}. Rather than fork those code paths for a device with
 * no keyboard, the Android build keeps them and lets the in-game panel press the
 * keys through {@link NativeCommandBridge#key}. These are therefore the single
 * source of truth for both sides: the panel buttons press them, and the module
 * configuration written by {@link ModuleSettings} names the same ones.
 */
final class Hotkeys {
    /** Desktop default for the all-HUD toggle in {@code BetterEndfield.UI}. */
    static final int HIDE_HUD = 0x30; // '0'
    /** Desktop defaults for {@code BetterEndfield.Camera}. */
    static final int FREE_CAMERA = 0x39; // '9'
    static final int WORLD_PAUSE = 0x38; // '8'
    static final int FIRST_PERSON = 0xBD; // VK_OEM_MINUS

    /** Free-camera movement, held rather than tapped. */
    static final int MOVE_FORWARD = 0x26; // VK_UP
    static final int MOVE_BACK = 0x28; // VK_DOWN
    static final int MOVE_LEFT = 0x25; // VK_LEFT
    static final int MOVE_RIGHT = 0x27; // VK_RIGHT
    static final int MOVE_UP = 0x21; // VK_PRIOR
    static final int MOVE_DOWN = 0x22; // VK_NEXT

    /**
     * How the configuration files spell each code. The module parsers accept a
     * single alphanumeric character as itself and {@code -} as VK_OEM_MINUS, so
     * these strings resolve back to the constants above.
     */
    static final String HIDE_HUD_NAME = "0";
    static final String FREE_CAMERA_NAME = "9";
    static final String WORLD_PAUSE_NAME = "8";
    static final String FIRST_PERSON_NAME = "-";

    private Hotkeys() {}
}
