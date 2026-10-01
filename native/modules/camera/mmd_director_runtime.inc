// MMD playback director (PC). One clock drives the character motion
// (character_motion_runtime.inc), the VMD camera (free_camera_runtime.inc) and
// local music through the Music module's LocalMusic capability. The director
// runs on the game thread observed at CameraManager.TailLateTick; the overlay
// host runs on the input thread and never calls Unity.
void RequestCharacterDirectorLoad(const std::string& motion, const std::string& face);

namespace Mmd {
namespace Proto = MmdOverlayProtocol;

constexpr double kLoadTimeoutSeconds = 20.0;
constexpr double kMusicSnapSeconds = 0.25;
constexpr double kPublishSeconds = 0.05;
constexpr uint64_t kOverlayRelaunchMs = 3000;
constexpr char kMusicOwner[] = "betterendfield.camera.mmd";

enum class State { Idle, Loading, Playing, Paused };
enum class CameraMode { Vmd = 0, Free = 1, Game = 2 };

struct Sources {
    std::string work, name;
    std::string motion, camera, face, music; // UTF-8
    // Squad playback: motion/face for the 2nd..4th dancer (may be empty).
    std::string extra_motion[MmdLibrary::kExtraDancers], extra_face[MmdLibrary::kExtraDancers];
    double audio_offset = 0.0;
    bool camera_from_motion = false;
};

struct Director {
    State state = State::Idle;
    CameraMode camera_mode = CameraMode::Vmd;
    Sources sources;
    double time = 0.0, last = 0.0, duration = 0.0, load_started = 0.0;
    bool loop = false, loop_initialized = false;
    bool want_body = false, body_done = false, body_ok = false;
    double body_duration = 0.0;
    bool want_camera = false, camera_submitted = false, camera_done = false;
    uint64_t camera_generation = 0;
    std::vector<Vmd::CameraKey> camera_keys;
    double camera_duration = 0.0;
    Vector3 anchor{};
    Quaternion anchor_rotation{};
    bool entered_free_camera = false;
    const BE_LocalMusicApiV1* music = nullptr;
    uint64_t music_token = 0;
    bool want_music = false, music_done = false, music_ok = false, music_started = false;
    double music_duration = 0.0;
    std::string selected_work;
    uint64_t work_generation = UINT64_MAX;
    Proto::MessageCode message_code = Proto::MessageCode::None;
    std::string message;
    double last_publish = 0.0;
    uint32_t available = 0;
    double last_available = -10.0;
};

Director g;
DWORD g_thread = 0;
thread_local bool t_in_director = false;
std::filesystem::path g_library_root;

#if defined(__ANDROID__)
std::mutex android_mutex;
std::deque<Proto::Command> android_commands;
std::string android_status = "{}";
std::string android_library = "[]";
std::string JsonString(std::string_view text) {
    std::string result = "\"";
    for (unsigned char c : text) {
        if (c == '"' || c == '\\') { result += '\\'; result += char(c); }
        else if (c < 32) { char code[7]; std::snprintf(code, sizeof(code), "\\u%04x", c); result += code; }
        else result += char(c);
    }
    return result + "\"";
}
#else
struct OverlayHost {
    HANDLE mapping = nullptr;
    Proto::Shared* view = nullptr;
    HANDLE process = nullptr;
    uint64_t last_launch = 0;
    bool initial_visibility_applied = false;
};
OverlayHost g_overlay;

#endif

void Say(Proto::MessageCode code, const std::string& detail, bool log = true) {
    g.message_code = code;
    g.message = detail;
    if (log && !detail.empty()) Log("MMD: " + detail);
}

std::filesystem::path ModuleDirectory() {
#if defined(__ANDROID__)
    const char* root = std::getenv("BETTER_ENDFIELD_MMD_ROOT");
    return root && *root ? MmdLibrary::FromUtf8(root).parent_path() / "modules" : std::filesystem::path();
#else
    HMODULE module = nullptr;
    if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
            GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(&ModuleDirectory), &module)) {
        return {};
    }
    std::wstring path(32768, L'\0');
    const DWORD length = GetModuleFileNameW(module, path.data(), static_cast<DWORD>(path.size()));
    if (!length || length >= path.size()) return {};
    path.resize(length);
    return std::filesystem::path(path).parent_path();
#endif
}

const BE_LocalMusicApiV1* ResolveMusic() {
#if defined(__ANDROID__)
    return betterendfield::AndroidLocalMusicApi();
#else
    HMODULE module = GetModuleHandleW(L"BetterEndfield.Music.dll");
    if (!module) return nullptr;
    const auto get = reinterpret_cast<BE_GetLocalMusicApiV1Fn>(
        GetProcAddress(module, "BetterEndfield_GetLocalMusicApiV1"));
    const BE_LocalMusicApiV1* api = get ? get() : nullptr;
    if (!api || api->version < 1 || api->size < sizeof(BE_LocalMusicApiV1) || !api->open ||
        !api->play || !api->pause || !api->seek || !api->close || !api->status || !api->set_gain) {
        return nullptr;
    }
    return api;
#endif
}

bool FreeCameraAllowed() {
    return g_free_camera_enabled.load(std::memory_order_acquire) && g_free_camera_contract_ready;
}

// The VMD camera borrows the free camera machinery only; it does not need the
// user's free camera feature, whose hotkeys and controls stay off.
bool VmdCameraAllowed() { return g_free_camera_contract_ready; }

double Seconds() {
    if (g.state == State::Playing) return g.time + std::max(0.0, NowSeconds() - g.last);
    return g.time;
}

bool Active() { return g.state == State::Playing || g.state == State::Paused; }

// ---------------------------------------------------------------------------
// Sources: a library work (<install>\mmd\<work>\set.ini) or the loose files of
// the [betterendfield.camera] section.
// ---------------------------------------------------------------------------
bool ResolveSources(Sources& out) {
    Sources next;
    std::string motion, camera, face, music;
    {
        std::lock_guard<std::mutex> lock(g_vmd_path_mutex);
        motion = g_vmd_motion_file;
        camera = g_vmd_camera_file;
        face = g_mmd_face_file;
        music = g_mmd_music_file;
    }
    if (!g.selected_work.empty()) {
        if (!MmdLibrary::PlainFolderName(g.selected_work) || g_library_root.empty()) {
            Say(Proto::MessageCode::WorkInvalid, "invalid work folder name: " + g.selected_work);
            return false;
        }
        MmdLibrary::Work work;
        std::string error;
        if (!MmdLibrary::LoadWork(g_library_root / MmdLibrary::FromUtf8(g.selected_work), work, error)) {
            Say(Proto::MessageCode::WorkInvalid, g.selected_work + ": " + error);
            return false;
        }
        next.work = g.selected_work;
        next.name = work.name;
        next.motion = work.motion.empty() ? std::string() : MmdLibrary::ToUtf8(work.motion);
        next.camera = work.camera.empty() ? std::string() : MmdLibrary::ToUtf8(work.camera);
        next.face = work.face.empty() ? std::string() : MmdLibrary::ToUtf8(work.face);
        next.music = work.music.empty() ? std::string() : MmdLibrary::ToUtf8(work.music);
        for (size_t i = 0; i < MmdLibrary::kExtraDancers; ++i) {
            if (!work.extra_motion[i].empty()) next.extra_motion[i] = MmdLibrary::ToUtf8(work.extra_motion[i]);
            if (!work.extra_face[i].empty()) next.extra_face[i] = MmdLibrary::ToUtf8(work.extra_face[i]);
        }
        next.audio_offset = work.audio_offset;
#if defined(__ANDROID__)
        next.audio_offset += g_mmd_audio_offset.load(std::memory_order_acquire);
#endif
    } else {
        next.name = "ini";
        next.motion = motion;
        next.camera = camera;
        next.face = face;
        next.music = music;
        next.audio_offset = g_mmd_audio_offset.load(std::memory_order_acquire);
    }
    if (next.camera.empty() && !next.motion.empty()) {
        next.camera = next.motion; // MMD motion files often carry the camera too
        next.camera_from_motion = true;
    }
    if (next.motion.empty() && next.camera.empty()) {
        Say(Proto::MessageCode::NoWork, "select a work in the overlay or set vmd_motion_file");
        return false;
    }
    out = std::move(next);
    return true;
}

// ---------------------------------------------------------------------------
// Camera
// ---------------------------------------------------------------------------
bool EnsureFreeCamera() {
    if (g_free_camera_active) return true;
    if (!VmdCameraAllowed()) {
        Say(Proto::MessageCode::CameraFailed, "camera hooks are unavailable on this game build");
        return false;
    }
    if (!EnterFreeCamera()) return false;
    g.entered_free_camera = true;
    return true;
}

void ReleaseDirectorCamera(const char* reason) {
    if (g_vmd_director) StopPlayback(reason);
    g_vmd_director = false;
}

void ApplyCameraMode() {
    switch (g.camera_mode) {
    case CameraMode::Vmd:
        if (g.camera_keys.empty()) {
            ReleaseDirectorCamera("MMD work has no camera");
            if (g_free_camera_active && g.entered_free_camera) ExitFreeCamera("MMD work has no camera");
            g.entered_free_camera = false;
            g.camera_mode = CameraMode::Game;
            return;
        }
        if (!EnsureFreeCamera()) {
            g.camera_mode = CameraMode::Game;
            return;
        }
        if (!g_vmd_director) {
            StopPlayback("MMD VMD camera");
            ++g_file_generation;
            g_vmd_load_pending = false;
            g_vmd.keys = g.camera_keys;
            g_vmd.anchor = g.anchor;
            g_vmd.anchor_rotation = g.anchor_rotation;
            g_vmd.duration = g.camera_duration;
            g_playback = FreePlayback::Vmd;
            g_vmd_director = true;
        }
        return;
    case CameraMode::Free:
        if (!FreeCameraAllowed()) {
            Say(Proto::MessageCode::FreeCameraDisabled, "enable the free camera to use free camera mode");
            g.camera_mode = CameraMode::Game;
            ApplyCameraMode();
            return;
        }
        if (!EnsureFreeCamera()) {
            g.camera_mode = CameraMode::Game;
            return;
        }
        ReleaseDirectorCamera("MMD free camera");
        return;
    case CameraMode::Game:
        ReleaseDirectorCamera("MMD game camera");
        if (g_free_camera_active) ExitFreeCamera("MMD game camera");
        g.entered_free_camera = false;
        return;
    }
}

// The user can leave the director's camera at any time (free camera hotkey,
// camera change, another playback); follow what actually happened.
void TrackCameraMode() {
    if (!Active()) return;
    if (g.camera_mode == CameraMode::Vmd && !g_vmd_director) {
        g.camera_mode = g_free_camera_active ? CameraMode::Free : CameraMode::Game;
    } else if (g.camera_mode == CameraMode::Free && !g_free_camera_active) {
        g.camera_mode = CameraMode::Game;
    }
    if (!g_free_camera_active) g.entered_free_camera = false;
}

bool SubmitCamera() {
    CameraFiles::Job job;
    job.kind = CameraFiles::Kind::LoadVmd;
    job.path = g.sources.camera;
    job.generation = kMmdGenerationBit | ++g.camera_generation;
    if (!g_camera_files.Submit(std::move(job))) return false;
    g.camera_submitted = true;
    return true;
}

void CaptureAnchor() {
    float yaw = 0.0f;
    if (!CharacterPose(g.anchor, yaw)) {
        g.anchor = g_free_camera_active ? g_free_view.position : Vector3{};
        float pitch = 0.0f, roll = 0.0f;
        if (g_free_camera_active) ToEuler(g_free_view.rotation, pitch, yaw, roll);
        Log("MMD camera: main character unavailable; anchoring at the current camera.");
    }
    g.anchor_rotation = AxisAngle({0.0f, 1.0f, 0.0f}, yaw);
}

// ---------------------------------------------------------------------------
// Music
// ---------------------------------------------------------------------------
double MusicTarget() { return g.time + g.sources.audio_offset; }

void StartMusicIfDue() {
    if (!g.music_ok || g.music_started || g.state != State::Playing || MusicTarget() < 0.0) return;
    g.music->play(g.music_token, MusicTarget());
    g.music_started = true;
}

void SyncMusic() {
    if (!g.music_ok || !g.music_started || g.state != State::Playing) return;
    BE_LocalMusicStatusV1 status{};
    status.size = sizeof(status);
    if (!g.music->status(g.music_token, &status)) return;
    if (status.state == BE_LocalMusic_Error || status.state == BE_LocalMusic_Idle) {
        Say(Proto::MessageCode::MusicFailed, std::string("music stopped: ") + status.error);
        g.music_ok = false;
        return;
    }
    if (status.state != BE_LocalMusic_Playing || !status.output_active) return;
    // The audible music is the master clock; small drift is eased out.
    const double target = status.position_seconds - g.sources.audio_offset;
    const double drift = target - g.time;
    g.time += std::abs(drift) > kMusicSnapSeconds ? drift : drift * 0.1;
}

void CloseMusic() {
    if (g.music && g.music_token) g.music->close(g.music_token);
    g.music_token = 0;
    g.music_ok = g.music_started = false;
}

// ---------------------------------------------------------------------------
// Transport
// ---------------------------------------------------------------------------
void Stop(const char* reason, Proto::MessageCode code = Proto::MessageCode::Stopped) {
    if (g.state == State::Idle) return;
    RequestCharacterMotion(16);
    ReleaseDirectorCamera("MMD stopped");
    if (g.entered_free_camera && g_free_camera_active) ExitFreeCamera("MMD stopped");
    g.entered_free_camera = false;
    CloseMusic();
    ++g.camera_generation;
    g.state = State::Idle;
    g.time = g.duration = 0.0;
    g.camera_keys.clear();
    g.camera_keys.shrink_to_fit();
    g.want_body = g.want_camera = g.want_music = false;
    Say(code, std::string("stopped: ") + reason);
}

void BeginLoad() {
    Sources sources;
    if (!ResolveSources(sources)) return;
    g.sources = std::move(sources);
    g.body_done = g.body_ok = g.camera_submitted = g.camera_done = false;
    g.music_done = g.music_ok = g.music_started = false;
    g.body_duration = g.camera_duration = g.music_duration = 0.0;
    g.camera_keys.clear();
    const bool body_features = g_character_preview_enabled.load(std::memory_order_acquire);
    g.want_body = !g.sources.motion.empty() && body_features;
    g.want_camera = !g.sources.camera.empty() && VmdCameraAllowed();
    const bool music_wanted = !g.sources.music.empty() && g_mmd_music_enabled.load(std::memory_order_acquire);
    g.music = music_wanted ? ResolveMusic() : nullptr;
    g.want_music = g.music != nullptr;
    if (!g.sources.motion.empty() && !body_features) {
        Say(Proto::MessageCode::CharacterDisabled,
            "character motion is off (enable body motion in the manager); playing camera/music only");
    }
    if (music_wanted && !g.music) {
        Say(Proto::MessageCode::MusicModuleMissing,
            "music needs the Music module (BetterEndfield.Music.dll with local playback)");
    }
    if (!g.want_body && !g.want_camera && !g.want_music) {
        Say(Proto::MessageCode::NothingPlayable, "nothing playable in " + g.sources.name);
        return;
    }
    if (g.want_body) {
        std::vector<CharacterMotion::Part> parts{{g.sources.motion, g.sources.face}};
        for (size_t i = 0; i < MmdLibrary::kExtraDancers; ++i) {
            if (!g.sources.extra_motion[i].empty())
                parts.push_back({g.sources.extra_motion[i], g.sources.extra_face[i]});
        }
        RequestCharacterDirectorLoad(parts);
    }
    if (g.want_camera) SubmitCamera();
    if (g.want_music) {
        g.music_token = g.music->open(g.sources.music.c_str(), kMusicOwner);
        if (!g.music_token) {
            g.music_done = true;
            Say(Proto::MessageCode::MusicFailed, "music could not be opened");
        } else {
            g.music->set_gain(g.music_token, g_mmd_music_gain.load(std::memory_order_acquire));
        }
    }
    g.state = State::Loading;
    g.load_started = NowSeconds();
    Say(Proto::MessageCode::Loading, "loading " + g.sources.name);
}

void Start() {
    double duration = 0.0;
    if (g.body_ok) duration = std::max(duration, g.body_duration);
    if (!g.camera_keys.empty()) duration = std::max(duration, g.camera_duration);
    if (duration <= 0.0 && g.music_ok) duration = g.music_duration;
    if (duration <= 0.0) {
        g.state = State::Paused; // lets Stop() release what was acquired
        Stop("nothing playable", Proto::MessageCode::NothingPlayable);
        return;
    }
    if (!g.loop_initialized) {
        g.loop = g_mmd_loop_default.load(std::memory_order_acquire);
        g.loop_initialized = true;
    }
    g.duration = duration;
    g.time = 0.0;
    g.last = NowSeconds();
    g.state = State::Playing;
    if (!g.camera_keys.empty()) CaptureAnchor();
    ApplyCameraMode();
    StartMusicIfDue();
    Say(Proto::MessageCode::Playing, "playing " + g.sources.name + " (" +
        std::to_string(static_cast<int>(duration)) + " s; body=" +
        (g.body_ok ? std::to_string(CharacterMotion::director_dancers.load()) + " dancer(s)" : std::string("no")) +
        ", camera=" + (g.camera_keys.empty() ? "no" : "yes") + ", music=" +
        (g.music_ok ? "yes" : "no") + ")");
}

void PollLoading() {
    if (g.want_camera && !g.camera_submitted) SubmitCamera();
    if (g.want_body && !g.body_done) {
        const int phase = CharacterMotion::director_phase.load(std::memory_order_acquire);
        if (phase == CharacterMotion::DirectorBound) {
            g.body_done = g.body_ok = true;
            g.body_duration = CharacterMotion::director_duration.load(std::memory_order_acquire);
        } else if (phase == CharacterMotion::DirectorFailed) {
            g.body_done = true;
            Say(Proto::MessageCode::MotionFailed, "character motion could not be bound (see log)");
        }
    }
    if (g.want_music && !g.music_done) {
        BE_LocalMusicStatusV1 status{};
        status.size = sizeof(status);
        if (g.music->status(g.music_token, &status)) {
            if (status.state == BE_LocalMusic_Ready || status.state == BE_LocalMusic_Paused) {
                g.music_done = g.music_ok = true;
                g.music_duration = status.duration_seconds;
            } else if (status.state == BE_LocalMusic_Error || status.state == BE_LocalMusic_Idle) {
                g.music_done = true;
                Say(Proto::MessageCode::MusicFailed, std::string("music: ") + status.error);
            }
        }
    }
    const bool ready = (!g.want_body || g.body_done) && (!g.want_camera || g.camera_done) &&
        (!g.want_music || g.music_done);
    if (ready || NowSeconds() - g.load_started > kLoadTimeoutSeconds) Start();
}

void SeekTo(double seconds) {
    if (!Active()) return;
    g.time = std::clamp(seconds, 0.0, g.duration);
    g.last = NowSeconds();
    if (!g.music_ok) return;
    if (MusicTarget() < 0.0) {
        g.music->pause(g.music_token);
        g.music_started = false;
    } else if (g.state == State::Playing) {
        g.music->play(g.music_token, MusicTarget());
        g.music_started = true;
    } else {
        g.music->seek(g.music_token, MusicTarget());
    }
}

void PlayPause() {
    switch (g.state) {
    case State::Idle:
        BeginLoad();
        return;
    case State::Loading:
        Stop("load cancelled");
        return;
    case State::Playing:
        g.time = Seconds();
        g.state = State::Paused;
        if (g.music_ok && g.music_started) g.music->pause(g.music_token);
        Say(Proto::MessageCode::Paused, "paused");
        return;
    case State::Paused:
        g.state = State::Playing;
        g.last = NowSeconds();
        if (g.music_ok && MusicTarget() >= 0.0) {
            g.music->play(g.music_token, MusicTarget());
            g.music_started = true;
        }
        Say(Proto::MessageCode::Playing, "resumed");
        return;
    }
}

void SetCameraMode(CameraMode mode) {
    g.camera_mode = mode;
    if (Active()) ApplyCameraMode();
    static const char* const names[]{"VMD", "free", "game"};
    Say(Proto::MessageCode::CameraMode,
        std::string("camera: ") + names[static_cast<int>(g.camera_mode)], false);
}

void Tick() {
    const double now = NowSeconds();
    if (g.state == State::Playing) g.time += std::max(0.0, now - g.last);
    g.last = now;
    SyncMusic();
    if (g.time >= g.duration) {
        if (g.loop) {
            SeekTo(0.0);
            if (g.music_ok && MusicTarget() < 0.0) g.music_started = false;
        } else {
            Stop("finished", Proto::MessageCode::Finished);
            return;
        }
    }
    StartMusicIfDue();
    TrackCameraMode();
}

// ---------------------------------------------------------------------------
// Overlay shared memory
// ---------------------------------------------------------------------------
void CopyText(char* destination, size_t capacity, const std::string& text) {
    const size_t length = std::min(text.size(), capacity - 1);
    std::memcpy(destination, text.data(), length);
    destination[length] = '\0';
}

void Handle(const Proto::Command& command) {
    using Type = Proto::CommandType;
    switch (static_cast<Type>(command.type)) {
    case Type::PlayPause: PlayPause(); break;
    case Type::Stop: Stop("overlay"); break;
    case Type::SeekRelative:
        if (std::isfinite(command.value)) SeekTo(Seconds() + command.value);
        break;
    case Type::SeekAbsolute:
        if (std::isfinite(command.value)) SeekTo(command.value);
        break;
    case Type::CameraMode:
        if (command.argument >= 0 && command.argument <= 2) {
            SetCameraMode(static_cast<CameraMode>(command.argument));
        } else {
            SetCameraMode(static_cast<CameraMode>((static_cast<int>(g.camera_mode) + 1) % 3));
        }
        break;
    case Type::SelectWork: {
        const std::string work(command.text, strnlen(command.text, sizeof(command.text)));
        if (!work.empty() && !MmdLibrary::PlainFolderName(work)) break;
        if (g.state != State::Idle) Stop("work changed");
        g.selected_work = work;
        Say(Proto::MessageCode::WorkSelected, work.empty() ? "work cleared" : "work: " + work);
        break;
    }
    case Type::ToggleLoop:
        g.loop = !g.loop;
        g.loop_initialized = true;
        Say(Proto::MessageCode::LoopChanged, g.loop ? "loop on" : "loop off", false);
        break;
    case Type::KeyframeAdd: g_keyframe_add_request.store(true, std::memory_order_release); break;
    case Type::KeyframePlay: g_keyframe_play_request.store(true, std::memory_order_release); break;
    case Type::KeyframeClear: g_keyframe_clear_request.store(true, std::memory_order_release); break;
    case Type::KeyframeSave: g_keyframe_save_request.store(true, std::memory_order_release); break;
    case Type::KeyframeLoad: g_keyframe_load_request.store(true, std::memory_order_release); break;
    case Type::MotionPreset: g_motion_request.store(true, std::memory_order_release); break;
    case Type::FreeCamera: g_toggle_request.store(true, std::memory_order_release); break;
    case Type::Hide:
#if defined(_WIN32)
        if (g_overlay.view) InterlockedExchange(&g_overlay.view->visible, 0);
#endif
        break;
    default: break;
    }
}

void DrainOverlayCommands() {
#if defined(__ANDROID__)
    std::deque<Proto::Command> commands;
    { std::lock_guard lock(android_mutex); commands.swap(android_commands); }
    for (const auto& command : commands) Handle(command);
#else
    Proto::Shared* shared = g_overlay.view;
    if (!shared) return;
    LONG read = InterlockedCompareExchange(&shared->command_read, 0, 0);
    const LONG write = InterlockedCompareExchange(&shared->command_write, 0, 0);
    if (static_cast<uint32_t>(write - read) > Proto::kCommandCapacity) {
        InterlockedExchange(&shared->command_read, write); // overlay overran the ring
        return;
    }
    while (read != write) {
        Proto::Command command;
        std::memcpy(&command, &shared->commands[static_cast<uint32_t>(read) % Proto::kCommandCapacity],
            sizeof(command));
        command.text[sizeof(command.text) - 1] = '\0';
        ++read;
        InterlockedExchange(&shared->command_read, read);
        Log("MMD overlay command type=" + std::to_string(command.type));
        Handle(command);
    }
#endif
}

void Publish(bool force) {
#if defined(_WIN32)
    Proto::Shared* shared = g_overlay.view;
    if (!shared) return;
#endif
    const double now = NowSeconds();
    if (!force && now - g.last_publish < kPublishSeconds) return;
    g.last_publish = now;
    if (now - g.last_available > 2.0) {
        g.last_available = now;
        uint32_t available = 0;
        if (g_character_preview_enabled.load(std::memory_order_acquire)) available |= Proto::FeatureBody;
        if (VmdCameraAllowed()) available |= Proto::FeatureCamera;
        if (FreeCameraAllowed()) available |= Proto::FeatureFreeCamera;
        if (ResolveMusic()) available |= Proto::FeatureMusicModule | Proto::FeatureMusic;
        g.available = available;
    }
    Proto::Status status;
    status.state = static_cast<uint32_t>(g.state);
    status.camera_mode = static_cast<uint32_t>(g.camera_mode);
    status.loop = g.loop_initialized ? g.loop : g_mmd_loop_default.load(std::memory_order_acquire);
    status.features = (g.body_ok ? Proto::FeatureBody : 0u) |
        (!g.camera_keys.empty() ? Proto::FeatureCamera : 0u) | (g.music_ok ? Proto::FeatureMusic : 0u);
    status.available = g.available;
    status.keyframe_count = static_cast<uint32_t>(g_keyframes.size());
    // The free camera tools only work with the feature on, not under a VMD camera alone.
    status.free_camera_active = g_free_camera_active && FreeCameraAllowed() ? 1u : 0u;
    status.message_code = static_cast<uint32_t>(g.message_code);
    status.seconds = Seconds();
    status.duration = g.duration;
    CopyText(status.work, sizeof(status.work), g.selected_work);
    CopyText(status.message, sizeof(status.message), g.message);
#if defined(__ANDROID__)
    const bool terrain_requested = CharacterMotion::runtime.applied && CharacterMotion::runtime.applied->terrain;
    const bool terrain_available = EiemBody::TerrainAvailable(0);
    std::ostringstream json;
    json << "{\"state\":" << status.state << ",\"camera_mode\":" << status.camera_mode
         << ",\"loop\":" << status.loop << ",\"features\":" << status.features
         << ",\"available\":" << status.available << ",\"keyframe_count\":" << status.keyframe_count
         << ",\"free_camera_active\":" << status.free_camera_active << ",\"message_code\":" << status.message_code
         << ",\"seconds\":" << status.seconds << ",\"duration\":" << status.duration
         << ",\"work\":" << JsonString(status.work) << ",\"message\":" << JsonString(status.message)
         << ",\"library\":" << android_library
         << ",\"terrain_requested\":" << (terrain_requested ? "true" : "false")
         << ",\"terrain_available\":" << (terrain_available ? "true" : "false")
         << ",\"terrain_reason\":" << JsonString(terrain_available ? "" : EiemBody::TerrainUnavailableReason(0)) << "}";
    std::lock_guard lock(android_mutex);
    android_status = json.str();
#else
    InterlockedIncrement(&shared->sequence);
    MemoryBarrier();
    std::memcpy(&shared->status, &status, sizeof(status));
    MemoryBarrier();
    InterlockedIncrement(&shared->sequence);
#endif
}

void Pump() {
    const uint64_t work_generation = g_mmd_work_generation.load(std::memory_order_acquire);
    if (work_generation != g.work_generation) {
        g.work_generation = work_generation;
        std::lock_guard<std::mutex> lock(g_vmd_path_mutex);
        g.selected_work = g_mmd_work;
    }
    const uint32_t requests = g_mmd_requests.exchange(0, std::memory_order_acq_rel);
    const int seek_steps = g_mmd_seek_steps.exchange(0, std::memory_order_acq_rel);
    DrainOverlayCommands();
    if (!g_mmd_enabled.load(std::memory_order_acquire)) {
        if (g.state != State::Idle) Stop("MMD disabled");
        Publish(false);
        return;
    }
    if (requests & MmdRequestStop) Stop("stop key");
    if (requests & MmdRequestPlayPause) PlayPause();
    if (requests & MmdRequestCameraMode) {
        SetCameraMode(static_cast<CameraMode>((static_cast<int>(g.camera_mode) + 1) % 3));
    }
    if (seek_steps && Active()) {
        SeekTo(Seconds() + seek_steps * static_cast<double>(g_mmd_seek_seconds.load()));
    }
    if (g.state == State::Loading) PollLoading();
    else if (Active()) Tick();
    Publish(requests != 0 || seek_steps != 0);
}

#if defined(_WIN32)
// Input thread: visibility and the companion process. No Unity calls.
std::filesystem::path OverlayExecutable() {
    const std::filesystem::path directory = ModuleDirectory();
    return directory.empty() ? std::filesystem::path() : directory / L"BetterEndfield.MmdOverlay.exe";
}

void EnsureOverlayProcess() {
    if (g_overlay.process) {
        if (WaitForSingleObject(g_overlay.process, 0) == WAIT_TIMEOUT) return;
        CloseHandle(g_overlay.process);
        g_overlay.process = nullptr;
    }
    const uint64_t now = GetTickCount64();
    if (g_overlay.last_launch && now - g_overlay.last_launch < kOverlayRelaunchMs) return;
    g_overlay.last_launch = now;
    const std::filesystem::path executable = OverlayExecutable();
    std::error_code status;
    if (executable.empty() || !std::filesystem::is_regular_file(executable, status)) {
        Log("MMD overlay: BetterEndfield.MmdOverlay.exe not found beside the Camera DLL.");
        g_overlay.last_launch = now + 60000; // do not spam the log
        return;
    }
    std::wstring command = L"\"" + executable.wstring() + L"\" --game-pid " +
        std::to_wstring(GetCurrentProcessId()) + L" --mapping \"" +
        Proto::MappingName(GetCurrentProcessId()) + L"\"";
    STARTUPINFOW startup{};
    startup.cb = sizeof(startup);
    PROCESS_INFORMATION process{};
    const std::wstring directory = executable.parent_path().wstring();
    if (!CreateProcessW(executable.c_str(), command.data(), nullptr, nullptr, FALSE,
            CREATE_NO_WINDOW, nullptr, directory.c_str(), &startup, &process)) {
        Log("MMD overlay: launch failed error=" + std::to_string(GetLastError()));
        return;
    }
    CloseHandle(process.hThread);
    g_overlay.process = process.hProcess;
    Log("MMD overlay companion started pid=" + std::to_string(process.dwProcessId));
}

#endif

} // namespace Mmd

double MmdDirectorSeconds() { return Mmd::Seconds(); }
bool MmdDirectorPlaying() { return Mmd::g.state == Mmd::State::Playing; }
bool MmdDirectorCameraActive() { return g_vmd_director; }

// While the director plays an EIEM body, the VMD camera follows that body's
// stage: EIEM's root anchor (first-frame placement included), its terrain
// offset and the dancer's size. As in EIEM, camera keys taken from the motion
// file use the body's motion scale; a separate camera VMD uses 0.07 scaled by
// the character's standing height (reference 1.245 m). vmd_camera_scale acts
// as a multiplier relative to its 0.07 default.
bool MmdCameraReference(Vector3& anchor, Quaternion& anchor_rotation, float& scale, Vector3& offset) {
    using namespace Mmd;
    if (!Active() || !g.body_ok || !CharacterMotion::runtime.actors[0].active) return false;
    EiemBody::CameraReference reference;
    if (!EiemBody::GetCameraReference(0, reference)) return false;
    constexpr float kMmdCameraScale = 0.07f, kReferenceHeight = 1.245f;
    float base = kMmdCameraScale;
    if (g.sources.camera_from_motion) {
        base = reference.motion_scale;
    } else if (reference.natural_height > 0.1f && reference.natural_height < 5.0f) {
        base = kMmdCameraScale * reference.natural_height / kReferenceHeight;
    }
    const float next = base * g_vmd_camera_scale.load(std::memory_order_relaxed) / kMmdCameraScale;
    const Vector3 position{reference.position[0], reference.position[1], reference.position[2]};
    const Vector3 terrain{reference.offset[0], reference.offset[1], reference.offset[2]};
    const Quaternion rotation{reference.rotation[0], reference.rotation[1], reference.rotation[2],
        reference.rotation[3]};
    if (!std::isfinite(next) || next <= 0.0f || !IsFinite(position) || !IsFinite(terrain) ||
        !IsUnitQuaternion(rotation)) {
        return false;
    }
    anchor = position;
    anchor_rotation = rotation;
    offset = terrain;
    scale = next;
    return true;
}

void MmdCameraLoaded(CameraFiles::Result&& result) {
    using namespace Mmd;
    if (g.state != State::Loading || result.generation != (kMmdGenerationBit | g.camera_generation)) return;
    g.camera_done = true;
    if (!result.error.empty() || result.cameras.empty()) {
        // A motion file without camera keys is normal; only a named camera file is an error.
        if (!g.sources.camera_from_motion) {
            Say(Proto::MessageCode::CameraFailed, "camera VMD rejected: " + result.error);
        }
        return;
    }
    g.camera_keys = std::move(result.cameras);
    g.camera_duration = g.camera_keys.back().frame / kVmdFramesPerSecond;
}

// Game thread. The heartbeat only pumps on the thread TailLateTick confirmed,
// and never re-enters while the director itself is calling into Unity.
void PumpMmdDirector(bool allow_capture = true) {
    using namespace Mmd;
    if (t_in_director) return;
    const DWORD thread = GetCurrentThreadId();
    if (!g_thread) {
        if (!allow_capture) return;
        g_thread = thread;
    }
    if (g_thread != thread) return;
    t_in_director = true;
    try {
        Pump();
    } catch (const std::exception& error) {
        Log(std::string("MMD director error: ") + error.what());
        try { Stop("exception"); } catch (...) {}
    } catch (...) {
        Log("MMD director error: unknown exception.");
    }
    t_in_director = false;
}

void PumpMmdOverlayHost(bool mmd_enabled) {
#if defined(_WIN32)
    using namespace Mmd;
    Proto::Shared* view = g_overlay.view;
    if (!view) return;
    const bool enabled = mmd_enabled && g_mmd_overlay_enabled.load(std::memory_order_acquire);
    const bool toggle = g_mmd_overlay_toggle_request.exchange(false, std::memory_order_acq_rel);
    if (!enabled) {
        InterlockedExchange(&view->visible, 0);
        return;
    }
    if (!g_overlay.initial_visibility_applied) {
        g_overlay.initial_visibility_applied = true;
        InterlockedExchange(&view->visible,
            g_mmd_overlay_initial_visible.load(std::memory_order_acquire) ? 1 : 0);
    }
    if (toggle) InterlockedXor(&view->visible, 1);
    if (InterlockedCompareExchange(&view->visible, 0, 0)) EnsureOverlayProcess();
#else
    (void)mmd_enabled;
#endif
}

// Initialize: shared memory for the overlay and the library root. Threads that
// use them start afterwards.
void StartMmd() {
    using namespace Mmd;
#if defined(__ANDROID__)
    const char* root = std::getenv("BETTER_ENDFIELD_MMD_ROOT");
    g_library_root = root && *root ? MmdLibrary::FromUtf8(root) : std::filesystem::path();
    std::string library = "[";
    for (const auto& work : MmdLibrary::Scan(g_library_root)) {
        if (library.size() > 1) library += ',';
        library += "{\"folder\":" + JsonString(work.folder_name) + ",\"name\":" + JsonString(work.name) + "}";
    }
    android_library = library + "]";
    Log("Android MMD library: " + MmdLibrary::ToUtf8(g_library_root));
#else
    const std::filesystem::path modules = ModuleDirectory();
    g_library_root = modules.empty() ? std::filesystem::path() : modules.parent_path() / L"mmd";
    if (g_overlay.view) return;
    const std::wstring name = Proto::MappingName(GetCurrentProcessId());
    g_overlay.mapping = CreateFileMappingW(INVALID_HANDLE_VALUE, nullptr, PAGE_READWRITE, 0,
        static_cast<DWORD>(sizeof(Proto::Shared)), name.c_str());
    if (!g_overlay.mapping) {
        Log("MMD overlay: shared memory unavailable error=" + std::to_string(GetLastError()));
        return;
    }
    void* memory = MapViewOfFile(g_overlay.mapping, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(Proto::Shared));
    if (!memory) {
        CloseHandle(g_overlay.mapping);
        g_overlay.mapping = nullptr;
        return;
    }
    g_overlay.view = new (memory) Proto::Shared();
    g_overlay.view->structure_size = sizeof(Proto::Shared);
    g_overlay.view->game_pid = GetCurrentProcessId();
    const std::wstring root = g_library_root.wstring();
    wcsncpy_s(g_overlay.view->library_root, root.c_str(), _TRUNCATE);
    Log("MMD library: " + MmdLibrary::ToUtf8(g_library_root));
#endif
}

// Shutdown runs on Host's thread after the input thread has joined: release
// the music channel and the overlay, but never touch Unity here.
void StopMmd() {
    using namespace Mmd;
    CloseMusic();
    g.state = State::Idle;
    g_vmd_director = false;
#if defined(_WIN32)
    if (g_overlay.view) {
        InterlockedExchange(&g_overlay.view->shutdown_requested, 1);
        InterlockedExchange(&g_overlay.view->visible, 0);
        UnmapViewOfFile(g_overlay.view);
        g_overlay.view = nullptr;
    }
    if (g_overlay.mapping) CloseHandle(g_overlay.mapping);
    g_overlay.mapping = nullptr;
    if (g_overlay.process) CloseHandle(g_overlay.process);
    g_overlay.process = nullptr;
    g_overlay.initial_visibility_applied = false;
#else
    std::lock_guard lock(android_mutex);
    android_commands.clear();
    android_status = "{}";
#endif
}
