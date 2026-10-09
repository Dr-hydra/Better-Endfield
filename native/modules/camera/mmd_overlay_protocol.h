#pragma once
// Shared memory between BetterEndfieldNext.Camera (game) and the MMD overlay
// companion. The game writes Status under a sequence lock; the overlay appends
// Commands to a small ring the game drains on its main thread.
#include <Windows.h>

#include <cstddef>
#include <cstdint>
#include <string>

namespace BetterEndfieldNext::MmdOverlayProtocol {

inline constexpr uint32_t kMagic = 0x444D4D42; // "BMMD"
inline constexpr uint32_t kVersion = 1;
inline constexpr uint32_t kCommandCapacity = 16;
inline constexpr size_t kTextCapacity = 256;
inline constexpr size_t kPathCapacity = 520;

enum class State : uint32_t { Idle = 0, Loading = 1, Playing = 2, Paused = 3 };
enum class CameraMode : uint32_t { Vmd = 0, Free = 1, Game = 2 };

enum class CommandType : uint32_t {
    None = 0,
    PlayPause = 1,
    Stop = 2,
    SeekRelative = 3,  // value = seconds
    SeekAbsolute = 4,  // value = seconds
    CameraMode = 5,    // argument = CameraMode, -1 = next
    SelectWork = 6,    // text = work folder name (UTF-8), empty = none
    ToggleLoop = 7,
    KeyframeAdd = 8,
    KeyframePlay = 9,
    KeyframeClear = 10,
    KeyframeSave = 11,
    KeyframeLoad = 12,
    MotionPreset = 13, // start/stop the configured camera motion preset
    FreeCamera = 14,   // toggle the free camera
    Hide = 15,
};

enum class MessageCode : uint32_t {
    None = 0,
    Loading = 1,
    Playing = 2,
    Paused = 3,
    Stopped = 4,
    Finished = 5,
    NoWork = 6,           // nothing selected / configured
    WorkInvalid = 7,      // set.ini unreadable
    NothingPlayable = 8,
    MotionFailed = 9,
    CameraFailed = 10,
    MusicFailed = 11,
    MusicModuleMissing = 12,
    FreeCameraDisabled = 13,
    CharacterDisabled = 14,
    CameraMode = 15,      // argument in status.camera_mode
    WorkSelected = 16,
    LoopChanged = 17,
};

enum Feature : uint32_t {
    FeatureBody = 1u << 0,
    FeatureCamera = 1u << 1,
    FeatureMusic = 1u << 2,
    FeatureFreeCamera = 1u << 3,
    FeatureMusicModule = 1u << 4,
};

#pragma pack(push, 8)
struct Command {
    uint32_t type = 0;
    int32_t argument = 0;
    double value = 0.0;
    char text[kTextCapacity]{};
};

struct Status {
    uint32_t state = 0;       // State
    uint32_t camera_mode = 0; // CameraMode
    uint32_t loop = 0;
    uint32_t features = 0;    // Feature bits active in the current session
    uint32_t available = 0;   // Feature bits usable at all
    uint32_t keyframe_count = 0;
    uint32_t free_camera_active = 0;
    uint32_t message_code = 0; // MessageCode; message holds the detail
    double seconds = 0.0;
    double duration = 0.0;
    char work[kTextCapacity]{};    // selected work folder (UTF-8)
    char message[kTextCapacity]{}; // last status line (UTF-8)
};

struct Shared {
    uint32_t magic = kMagic;
    uint32_t version = kVersion;
    uint32_t structure_size = 0;
    uint32_t game_pid = 0;
    volatile LONG sequence = 0; // odd while Status is being written
    volatile LONG visible = 0;  // owned by the game (hotkey / Hide command)
    volatile LONG shutdown_requested = 0;
    uint32_t reserved = 0;
    wchar_t library_root[kPathCapacity]{};
    Status status;
    volatile LONG command_write = 0; // overlay: slot filled, then incremented
    volatile LONG command_read = 0;  // game: incremented after draining a slot
    Command commands[kCommandCapacity];
};
#pragma pack(pop)

inline std::wstring MappingName(DWORD game_pid) {
    return L"Local\\BetterEndfieldNext.Mmd." + std::to_wstring(game_pid);
}

} // namespace BetterEndfieldNext::MmdOverlayProtocol
