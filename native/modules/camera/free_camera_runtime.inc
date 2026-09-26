// Included inside CameraModule's anonymous namespace, after the first-person
// runtime. The free camera owns its full pose (position, yaw/pitch/roll, FOV)
// and advances it on the real-time clock, so freezing Time.timeScale only stops
// the world. The pose is written into the Cinemachine CameraState while the
// Brain pushes; when the Brain stops pushing (CameraManager no longer ticks
// once the world is frozen) the pose goes straight to the Unity camera
// transform from the unscaled time heartbeat.
//
// The VMD camera sampling below follows EIEM (https://github.com/Sasye/EIEM,
// AGPL-3.0): camera_player.h (Bezier evaluation, Euler signs, 180 degree basis)
// and vmd_parser.h (camera record layout).

constexpr float kDegToRad = 0.017453292519943295f;
constexpr float kRadToDeg = 57.29577951308232f;
constexpr float kFreePitchLimit = 89.0f;
constexpr float kFreeRollSpeed = 45.0f;       // degrees per second
constexpr float kFreeFovKeySpeed = 20.0f;     // degrees per second
constexpr float kFreeFovWheelStep = 2.0f;     // degrees per wheel notch
constexpr float kFreeMinFov = 5.0f;
constexpr float kFreeMaxFov = 150.0f;
constexpr double kPushStateStaleSeconds = 0.05;
constexpr double kCameraValidateSeconds = 0.1;
constexpr float kMotionEaseSeconds = 0.6f;
constexpr size_t kMaxCameraKeyframes = 64;
constexpr size_t kMaxVmdFileBytes = 64u << 20;
constexpr double kVmdFramesPerSecond = 30.0;

double NowSeconds() {
    static const double frequency = [] {
        LARGE_INTEGER value{};
        QueryPerformanceFrequency(&value);
        return static_cast<double>(value.QuadPart);
    }();
    LARGE_INTEGER counter{};
    QueryPerformanceCounter(&counter);
    return static_cast<double>(counter.QuadPart) / frequency;
}

// ---------------------------------------------------------------------------
// Math (Unity conventions: left-handed, Y up, Quaternion.Euler = Z, X, then Y)
// ---------------------------------------------------------------------------

Quaternion Multiply(Quaternion a, Quaternion b) {
    return {
        a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
        a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
        a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
        a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z};
}

Quaternion Conjugate(Quaternion q) {
    return {-q.x, -q.y, -q.z, q.w};
}

Quaternion NormalizeRotation(Quaternion q) {
    const float length = std::sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
    if (!(length > 0.0001f) || !std::isfinite(length)) {
        return {};
    }
    return {q.x / length, q.y / length, q.z / length, q.w / length};
}

Quaternion AxisAngle(Vector3 axis, float degrees) {
    const float half = degrees * kDegToRad * 0.5f;
    const float s = std::sin(half);
    return {axis.x * s, axis.y * s, axis.z * s, std::cos(half)};
}

Quaternion FromEuler(float pitch, float yaw, float roll) {
    return Multiply(Multiply(AxisAngle({0.0f, 1.0f, 0.0f}, yaw),
        AxisAngle({1.0f, 0.0f, 0.0f}, pitch)), AxisAngle({0.0f, 0.0f, 1.0f}, roll));
}

float WrapDegrees(float degrees) {
    degrees = std::fmod(degrees + 180.0f, 360.0f);
    if (degrees < 0.0f) degrees += 360.0f;
    return degrees - 180.0f;
}

void ToEuler(Quaternion q, float& pitch, float& yaw, float& roll) {
    const Vector3 forward = RotateVector(q, {0.0f, 0.0f, 1.0f});
    yaw = std::atan2(forward.x, forward.z) * kRadToDeg;
    pitch = -std::asin(std::clamp(forward.y, -1.0f, 1.0f)) * kRadToDeg;
    const Quaternion rest = Multiply(Conjugate(FromEuler(pitch, yaw, 0.0f)), q);
    roll = WrapDegrees(2.0f * std::atan2(rest.z, rest.w) * kRadToDeg);
}

bool LookAt(Vector3 from, Vector3 to, float& pitch, float& yaw) {
    const Vector3 direction = Subtract(to, from);
    const float length = Magnitude(direction);
    if (!(length > 0.001f)) {
        return false;
    }
    yaw = std::atan2(direction.x, direction.z) * kRadToDeg;
    pitch = -std::asin(std::clamp(direction.y / length, -1.0f, 1.0f)) * kRadToDeg;
    return true;
}

Quaternion Slerp(Quaternion a, Quaternion b, float t) {
    float dot = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    if (dot < 0.0f) {
        b = {-b.x, -b.y, -b.z, -b.w};
        dot = -dot;
    }
    if (dot > 0.9995f) {
        return NormalizeRotation({a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t,
            a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t});
    }
    const float theta = std::acos(std::clamp(dot, -1.0f, 1.0f));
    const float sin_theta = std::sin(theta);
    const float wa = std::sin((1.0f - t) * theta) / sin_theta;
    const float wb = std::sin(t * theta) / sin_theta;
    return {a.x * wa + b.x * wb, a.y * wa + b.y * wb, a.z * wa + b.z * wb,
        a.w * wa + b.w * wb};
}

float Lerp(float a, float b, float t) {
    return a + (b - a) * t;
}

Vector3 Lerp(Vector3 a, Vector3 b, float t) {
    return {Lerp(a.x, b.x, t), Lerp(a.y, b.y, t), Lerp(a.z, b.z, t)};
}

float SmoothStep(float t) {
    t = std::clamp(t, 0.0f, 1.0f);
    return t * t * (3.0f - 2.0f * t);
}

float CatmullRom(float p0, float p1, float p2, float p3, float t) {
    const float t2 = t * t;
    const float t3 = t2 * t;
    return 0.5f * (2.0f * p1 + (p2 - p0) * t +
        (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3) * t2 +
        (3.0f * p1 - p0 - 3.0f * p2 + p3) * t3);
}

Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
    return {CatmullRom(p0.x, p1.x, p2.x, p3.x, t),
        CatmullRom(p0.y, p1.y, p2.y, p3.y, t),
        CatmullRom(p0.z, p1.z, p2.z, p3.z, t)};
}

bool GetValue(const MethodContract* method, void* instance, Quaternion& value) {
    return Unbox(Invoke(method, instance, nullptr), value);
}

bool SetValue(const MethodContract* method, void* instance, Quaternion value) {
    void* parameters[1]{&value};
    return InvokeVoid(method, instance, parameters);
}

// ---------------------------------------------------------------------------
// State
// ---------------------------------------------------------------------------

struct FreeCameraControl {
    Vector3 position{};
    float pitch = 0.0f;
    float yaw = 0.0f;
    float roll = 0.0f;
    float fov = 60.0f;
};

struct FreeCameraView {
    Vector3 position{};
    Quaternion rotation{};
    float fov = 60.0f;
};

enum class FreePlayback : uint8_t { None, Motion, Keyframes, Vmd };
enum class MotionPreset : int { Orbit = 0, DollyZoom = 1, Crane = 2, Truck = 3 };

struct MotionSession {
    MotionPreset preset = MotionPreset::Orbit;
    Vector3 anchor{};
    Vector3 dolly_direction{};
    Vector3 truck_right{};
    float orbit_radius = 0.0f;
    float orbit_angle = 0.0f;
    float orbit_height = 0.0f;
    float dolly_width = 0.0f;
    float dolly_distance = 0.0f;
};

struct CameraKeyframe {
    Vector3 position{};
    Quaternion rotation{};
    float fov = 60.0f;
};

struct VmdCameraKey {
    uint32_t frame = 0;
    float distance = 0.0f;
    Vector3 interest{};
    Vector3 euler{};
    uint8_t interp[24]{};
    float fov = 30.0f;
};

struct VmdPlayback {
    std::vector<VmdCameraKey> keys;
    Vector3 anchor{};
    Quaternion anchor_rotation{};
    double duration = 0.0;
};

FreeCameraControl g_free_target;
FreeCameraControl g_free_smoothed;
FreeCameraView g_free_view;
Quaternion g_original_rotation{};
bool g_original_rotation_valid = false;
double g_free_last_step = 0.0;
double g_free_last_push = -1.0;
double g_free_last_validate = 0.0;
double g_free_enter_time = 0.0;
double g_free_last_heartbeat = 0.0;
bool g_free_transform_fallback_logged = false;
void* g_free_brain = nullptr;
bool g_free_brain_matches = false;

FreePlayback g_playback = FreePlayback::None;
double g_playback_start = 0.0;
MotionSession g_motion;
std::vector<CameraKeyframe> g_keyframes;
VmdPlayback g_vmd;

// ---------------------------------------------------------------------------
// Mouse look: a low-level mouse hook on the input thread. The delta is the hook
// position minus the current cursor position (the cursor has not moved yet when
// the hook runs), which keeps working while the game locks and recentres the
// cursor. The hook only exists while the free camera runs in a focused window.
// ---------------------------------------------------------------------------

LRESULT CALLBACK FreeCameraMouseHook(int code, WPARAM message, LPARAM data) {
    if (code == HC_ACTION && g_mouse_capture.load(std::memory_order_relaxed)) {
        const auto* info = reinterpret_cast<const MSLLHOOKSTRUCT*>(data);
        if (info && (info->flags & LLMHF_INJECTED) == 0) {
            if (message == WM_MOUSEMOVE) {
                POINT cursor{};
                if (GetCursorPos(&cursor)) {
                    g_mouse_dx.fetch_add(info->pt.x - cursor.x, std::memory_order_relaxed);
                    g_mouse_dy.fetch_add(info->pt.y - cursor.y, std::memory_order_relaxed);
                }
            } else if (message == WM_MOUSEWHEEL) {
                g_mouse_wheel.fetch_add(static_cast<short>(HIWORD(info->mouseData)),
                    std::memory_order_relaxed);
            }
        }
    }
    return CallNextHookEx(nullptr, code, message, data);
}

void ClearMouseInput() {
    g_mouse_dx.store(0, std::memory_order_relaxed);
    g_mouse_dy.store(0, std::memory_order_relaxed);
    g_mouse_wheel.store(0, std::memory_order_relaxed);
}

// ---------------------------------------------------------------------------
// Pose output
// ---------------------------------------------------------------------------

void ViewFromControl(const FreeCameraControl& control) {
    g_free_view.position = control.position;
    g_free_view.rotation = FromEuler(control.pitch, control.yaw, control.roll);
    g_free_view.fov = control.fov;
}

// Adopts the rendered view as the controllable pose, so the free camera
// continues from where a playback stopped.
void ControlFromView() {
    FreeCameraControl control;
    control.position = g_free_view.position;
    ToEuler(g_free_view.rotation, control.pitch, control.yaw, control.roll);
    control.pitch = std::clamp(control.pitch, -kFreePitchLimit, kFreePitchLimit);
    control.fov = std::clamp(g_free_view.fov, kFreeMinFov, kFreeMaxFov);
    g_free_target = control;
    g_free_smoothed = control;
}

void WriteFreeCameraState(void* state) {
    const Vector3 no_correction{};
    const Quaternion no_rotation_correction{};
    const float dutch = 0.0f;
    const int32_t lens = g_state_layout.lens;
    WriteBytes(state, g_state_layout.raw_position, &g_free_view.position,
        sizeof(g_free_view.position));
    WriteBytes(state, g_state_layout.raw_orientation, &g_free_view.rotation,
        sizeof(g_free_view.rotation));
    WriteBytes(state, g_state_layout.position_correction, &no_correction,
        sizeof(no_correction));
    WriteBytes(state, g_state_layout.orientation_correction,
        &no_rotation_correction, sizeof(no_rotation_correction));
    WriteBytes(state, lens + g_state_layout.lens_field_of_view, &g_free_view.fov,
        sizeof(g_free_view.fov));
    WriteBytes(state, lens + g_state_layout.lens_dutch, &dutch, sizeof(dutch));
}

void WriteFreeCameraTransform() {
    void* transform = Invoke(Contract("unity.component.transform"),
        g_active_camera, nullptr);
    if (!transform) {
        return;
    }
    SetValue(Contract("unity.transform.position.set"), transform,
        g_free_view.position);
    SetValue(Contract("unity.transform.rotation.set"), transform,
        g_free_view.rotation);
    SetValue(Contract("unity.camera.fov.set"), g_active_camera, g_free_view.fov);
}

// ---------------------------------------------------------------------------
// Anchors
// ---------------------------------------------------------------------------

bool CharacterPose(Vector3& position, float& yaw) {
    void* transform = FindModelTransform();
    Vector3 forward{};
    if (!transform ||
        !GetValue(Contract("unity.transform.position.get"), transform, position) ||
        !IsFinite(position)) {
        return false;
    }
    yaw = 0.0f;
    if (GetValue(Contract("unity.transform.forward"), transform, forward) &&
        IsFinite(forward) && (forward.x * forward.x + forward.z * forward.z) > 0.0001f) {
        yaw = std::atan2(forward.x, forward.z) * kRadToDeg;
    }
    return true;
}

Vector3 MotionAnchor() {
    Vector3 position{};
    float yaw = 0.0f;
    if (CharacterPose(position, yaw)) {
        position.y += g_motion_target_height.load(std::memory_order_relaxed);
        return position;
    }
    const Vector3 forward = RotateVector(g_free_view.rotation, {0.0f, 0.0f, 1.0f});
    return Add(g_free_view.position, Scale(forward, 5.0f));
}

// ---------------------------------------------------------------------------
// Playback
// ---------------------------------------------------------------------------

void StopPlayback(const char* reason) {
    if (g_playback == FreePlayback::None) {
        return;
    }
    g_playback = FreePlayback::None;
    ControlFromView();
    ClearMouseInput();
    Log(std::string("Free camera playback stopped: ") + reason);
}

void StartMotion() {
    const int preset = std::clamp(g_motion_preset.load(std::memory_order_relaxed), 0, 3);
    MotionSession motion;
    motion.preset = static_cast<MotionPreset>(preset);
    motion.anchor = MotionAnchor();
    const Vector3 start = g_free_target.position;
    const Vector3 offset = Subtract(start, motion.anchor);
    switch (motion.preset) {
    case MotionPreset::Orbit:
        motion.orbit_radius = std::sqrt(offset.x * offset.x + offset.z * offset.z);
        if (motion.orbit_radius < 0.5f) {
            motion.orbit_radius = 3.0f;
        }
        motion.orbit_angle = std::atan2(offset.x, offset.z) * kRadToDeg;
        motion.orbit_height = offset.y;
        break;
    case MotionPreset::DollyZoom: {
        motion.dolly_distance = Magnitude(offset);
        if (motion.dolly_distance < 0.5f) {
            Log("Free camera motion: dolly zoom needs the camera at least 0.5 m from the target.");
            return;
        }
        motion.dolly_direction = Scale(offset, 1.0f / motion.dolly_distance);
        motion.dolly_width = 2.0f * motion.dolly_distance *
            std::tan(g_free_target.fov * 0.5f * kDegToRad);
        LookAt(start, motion.anchor, g_free_target.pitch, g_free_target.yaw);
        break;
    }
    case MotionPreset::Crane:
        break;
    case MotionPreset::Truck:
        motion.truck_right = RotateVector(
            FromEuler(0.0f, g_free_target.yaw, 0.0f), {1.0f, 0.0f, 0.0f});
        break;
    }
    g_motion = motion;
    g_free_smoothed = g_free_target;
    g_playback = FreePlayback::Motion;
    g_playback_start = NowSeconds();
    static const char* const names[]{"orbit", "dolly_zoom", "crane", "truck"};
    Log(std::string("Free camera motion started: ") + names[preset]);
}

void StepMotion(double now, float dt) {
    const float elapsed = static_cast<float>(now - g_playback_start);
    const float duration = g_motion_duration.load(std::memory_order_relaxed);
    if (duration > 0.0f && elapsed >= duration) {
        StopPlayback("motion finished");
        return;
    }
    float ease = SmoothStep(elapsed / kMotionEaseSeconds);
    if (duration > 0.0f) {
        ease = std::min(ease, SmoothStep((duration - elapsed) / kMotionEaseSeconds));
    }
    const float speed = g_motion_speed.load(std::memory_order_relaxed) * ease * dt;
    FreeCameraControl& control = g_free_target;
    switch (g_motion.preset) {
    case MotionPreset::Orbit: {
        g_motion.orbit_angle += g_orbit_speed.load(std::memory_order_relaxed) * ease * dt;
        const float angle = g_motion.orbit_angle * kDegToRad;
        control.position = Add(g_motion.anchor, {std::sin(angle) * g_motion.orbit_radius,
            g_motion.orbit_height, std::cos(angle) * g_motion.orbit_radius});
        LookAt(control.position, g_motion.anchor, control.pitch, control.yaw);
        break;
    }
    case MotionPreset::DollyZoom: {
        g_motion.dolly_distance -= speed;
        const float fov = 2.0f * std::atan(g_motion.dolly_width /
            (2.0f * std::max(g_motion.dolly_distance, 0.01f))) * kRadToDeg;
        if (g_motion.dolly_distance < 0.3f || fov < kFreeMinFov || fov > kFreeMaxFov) {
            StopPlayback("dolly zoom reached its limit");
            return;
        }
        control.position = Add(g_motion.anchor,
            Scale(g_motion.dolly_direction, g_motion.dolly_distance));
        control.fov = fov;
        break;
    }
    case MotionPreset::Crane:
        control.position.y += speed;
        LookAt(control.position, g_motion.anchor, control.pitch, control.yaw);
        break;
    case MotionPreset::Truck:
        control.position = Add(control.position, Scale(g_motion.truck_right, speed));
        break;
    }
    control.pitch = std::clamp(control.pitch, -kFreePitchLimit, kFreePitchLimit);
    g_free_smoothed = control;
    ViewFromControl(control);
}

void AddKeyframe() {
    if (g_keyframes.size() >= kMaxCameraKeyframes) {
        Log("Free camera keyframes: the limit of 64 keyframes is reached.");
        return;
    }
    g_keyframes.push_back({g_free_view.position, g_free_view.rotation, g_free_view.fov});
    Log("Free camera keyframe " + std::to_string(g_keyframes.size()) + " recorded.");
}

void StartKeyframes() {
    if (g_keyframes.size() < 2) {
        Log("Free camera keyframes: record at least two keyframes before playing.");
        return;
    }
    g_playback = FreePlayback::Keyframes;
    g_playback_start = NowSeconds();
    Log("Free camera keyframe playback started (" +
        std::to_string(g_keyframes.size()) + " keyframes).");
}

void StepKeyframes(double now) {
    const size_t count = g_keyframes.size();
    const float segment = g_keyframe_segment_seconds.load(std::memory_order_relaxed);
    const float total = segment * static_cast<float>(count - 1);
    float elapsed = static_cast<float>(now - g_playback_start);
    const bool loop = g_keyframe_loop.load(std::memory_order_relaxed);
    if (!loop && elapsed >= total) {
        const CameraKeyframe& last = g_keyframes.back();
        g_free_view = {last.position, last.rotation, last.fov};
        StopPlayback("keyframes finished");
        return;
    }
    if (loop) {
        elapsed = std::fmod(elapsed, total);
    }
    // One-shot paths ease in and out over the whole path; loops keep a constant pace.
    const float progress = loop ? elapsed / total : SmoothStep(elapsed / total);
    const float scaled = progress * static_cast<float>(count - 1);
    const size_t index = std::min(static_cast<size_t>(scaled), count - 2);
    const float t = scaled - static_cast<float>(index);
    const auto at = [&](ptrdiff_t i) -> const CameraKeyframe& {
        return g_keyframes[static_cast<size_t>(std::clamp<ptrdiff_t>(i, 0,
            static_cast<ptrdiff_t>(count - 1)))];
    };
    const ptrdiff_t i = static_cast<ptrdiff_t>(index);
    g_free_view.position = CatmullRom(at(i - 1).position, at(i).position,
        at(i + 1).position, at(i + 2).position, t);
    g_free_view.rotation = Slerp(at(i).rotation, at(i + 1).rotation, t);
    g_free_view.fov = std::clamp(CatmullRom(at(i - 1).fov, at(i).fov, at(i + 1).fov,
        at(i + 2).fov, t), kFreeMinFov, kFreeMaxFov);
}

// VMD camera section: header "Vocaloid Motion Data 0002" (20 byte model name) or
// "Vocaloid Motion Data file" (10 byte name), then bone (111 bytes each), morph
// (23 bytes each) and camera (61 bytes each) records. Missing trailing sections
// simply mean there is no camera data.
bool LoadVmdCamera(const std::string& path, std::vector<VmdCameraKey>& keys,
    std::string& error) {
    keys.clear();
    const int wide_length = MultiByteToWideChar(CP_UTF8, 0, path.c_str(), -1, nullptr, 0);
    if (wide_length <= 1) {
        error = "no VMD camera file is configured";
        return false;
    }
    std::wstring wide(static_cast<size_t>(wide_length), L'\0');
    MultiByteToWideChar(CP_UTF8, 0, path.c_str(), -1, wide.data(), wide_length);
    HANDLE file = CreateFileW(wide.c_str(), GENERIC_READ, FILE_SHARE_READ, nullptr,
        OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (file == INVALID_HANDLE_VALUE) {
        error = "the VMD file could not be opened";
        return false;
    }
    LARGE_INTEGER size{};
    std::vector<uint8_t> data;
    bool read_ok = GetFileSizeEx(file, &size) && size.QuadPart > 0 &&
        static_cast<uint64_t>(size.QuadPart) <= kMaxVmdFileBytes;
    if (read_ok) {
        data.resize(static_cast<size_t>(size.QuadPart));
        DWORD read = 0;
        read_ok = ReadFile(file, data.data(), static_cast<DWORD>(data.size()), &read,
            nullptr) && read == data.size();
    }
    CloseHandle(file);
    if (!read_ok) {
        error = "the VMD file is empty, larger than 64 MiB or unreadable";
        return false;
    }

    BetterEndfield::Vmd::Document document;
    if (!BetterEndfield::Vmd::Parse(data, document, error)) return false;
    keys.reserve(document.cameras.size());
    for (const auto& source : document.cameras) {
        VmdCameraKey key;
        key.frame = source.frame;
        key.distance = source.distance;
        key.interest = {source.interest[0], source.interest[1], source.interest[2]};
        key.euler = {source.euler[0], source.euler[1], source.euler[2]};
        std::copy(source.interpolation.begin(), source.interpolation.end(), key.interp);
        key.fov = source.fov;
        keys.push_back(key);
    }
    if (keys.empty()) {
        error = "the VMD file has no valid camera keyframes";
        return false;
    }
    return true;
}

// MMD interpolation: a cubic Bezier from (0,0) to (1,1) with control points in
// 0..127, solved for x with Newton iterations (EIEM CamBezierEval).
float VmdBezier(uint8_t bx1, uint8_t bx2, uint8_t by1, uint8_t by2, float t) {
    const float x1 = bx1 / 127.0f, x2 = bx2 / 127.0f;
    const float y1 = by1 / 127.0f, y2 = by2 / 127.0f;
    if (std::fabs(x1 - y1) < 1e-4f && std::fabs(x2 - y2) < 1e-4f) return t;
    if (t <= 0.0f) return 0.0f;
    if (t >= 1.0f) return 1.0f;
    const auto curve = [](float p1, float p2, float s) {
        const float u = 1.0f - s;
        return 3.0f * u * u * s * p1 + 3.0f * u * s * s * p2 + s * s * s;
    };
    float s = t;
    for (int iteration = 0; iteration < 12; ++iteration) {
        const float x = curve(x1, x2, s) - t;
        if (std::fabs(x) < 1e-5f) break;
        const float u = 1.0f - s;
        const float dx = 3.0f * u * u * x1 + 6.0f * u * s * (x2 - x1) +
            3.0f * s * s * (1.0f - x2);
        if (std::fabs(dx) < 1e-6f) break;
        s = std::clamp(s - x / dx, 0.0f, 1.0f);
    }
    return curve(y1, y2, s);
}

void SampleVmdCamera(double frame, Vector3& position, Quaternion& rotation, float& fov) {
    const std::vector<VmdCameraKey>& keys = g_vmd.keys;
    const auto next = std::upper_bound(keys.begin(), keys.end(), frame,
        [](double value, const VmdCameraKey& key) { return value < key.frame; });
    const VmdCameraKey* k0 = nullptr;
    const VmdCameraKey* k1 = nullptr;
    if (next == keys.begin()) {
        k0 = k1 = &keys.front();
    } else if (next == keys.end()) {
        k0 = k1 = &keys.back();
    } else {
        k1 = &*next;
        k0 = &*(next - 1);
    }
    Vector3 interest = k0->interest;
    Vector3 euler = k0->euler;
    float distance = k0->distance;
    float view_angle = k0->fov;
    // Adjacent keyframes are a camera cut in MMD: hold the first one instead of
    // blending across the cut at render rates above 30 fps.
    if (k1 != k0 && k1->frame - k0->frame > 1) {
        const float t = static_cast<float>((frame - k0->frame) /
            static_cast<double>(k1->frame - k0->frame));
        const uint8_t* ip = k1->interp;
        const auto curve = [&](int group) {
            return VmdBezier(ip[group * 4], ip[group * 4 + 1], ip[group * 4 + 2],
                ip[group * 4 + 3], t);
        };
        const float rotation_t = curve(3);
        interest = {Lerp(k0->interest.x, k1->interest.x, curve(0)),
            Lerp(k0->interest.y, k1->interest.y, curve(1)),
            Lerp(k0->interest.z, k1->interest.z, curve(2))};
        euler = Lerp(k0->euler, k1->euler, rotation_t);
        distance = Lerp(k0->distance, k1->distance, curve(4));
        view_angle = Lerp(k0->fov, k1->fov, curve(5));
    }
    // MMD camera: Euler angles in radians with all three signs flipped, composed
    // Y * X * Z; the eye sits `distance` along the camera's local Z from the
    // interest point. The MMD space is turned 180 degrees about Y so the model
    // front faces the character's forward direction.
    const Quaternion source = FromEuler(-euler.x * kRadToDeg, -euler.y * kRadToDeg,
        -euler.z * kRadToDeg);
    const Vector3 eye = Add(interest, RotateVector(source, {0.0f, 0.0f, distance}));
    const Quaternion basis{0.0f, 1.0f, 0.0f, 0.0f};
    const float scale = g_vmd_camera_scale.load(std::memory_order_relaxed);
    const Vector3 local = Scale(RotateVector(basis, eye), scale);
    position = Add(g_vmd.anchor, RotateVector(g_vmd.anchor_rotation, local));
    rotation = NormalizeRotation(Multiply(g_vmd.anchor_rotation, Multiply(basis, source)));
    fov = std::clamp(view_angle + g_vmd_camera_fov_bias.load(std::memory_order_relaxed),
        1.0f, 179.0f);
}

void StartVmd() {
    std::string path;
    {
        std::lock_guard<std::mutex> lock(g_vmd_path_mutex);
        path = g_vmd_camera_file;
    }
    VmdPlayback playback;
    std::string error;
    if (!LoadVmdCamera(path, playback.keys, error)) {
        Log("VMD camera: " + error + ".");
        return;
    }
    float yaw = 0.0f;
    if (!CharacterPose(playback.anchor, yaw)) {
        playback.anchor = g_free_target.position;
        yaw = g_free_target.yaw;
        Log("VMD camera: the main character is unavailable; anchoring at the camera.");
    }
    playback.anchor_rotation = AxisAngle({0.0f, 1.0f, 0.0f}, yaw);
    playback.duration = playback.keys.back().frame / kVmdFramesPerSecond;
    g_vmd = std::move(playback);
    g_playback = FreePlayback::Vmd;
    g_playback_start = NowSeconds();
    char buffer[160];
    std::snprintf(buffer, sizeof(buffer),
        "VMD camera playback started: %zu keyframes, %.1f s.",
        g_vmd.keys.size(), g_vmd.duration);
    Log(buffer);
}

void StepVmd(double now) {
    double elapsed = now - g_playback_start;
    if (elapsed > g_vmd.duration) {
        if (!g_vmd_camera_loop.load(std::memory_order_relaxed) || g_vmd.duration <= 0.0) {
            StopPlayback("VMD camera finished");
            return;
        }
        elapsed = std::fmod(elapsed, g_vmd.duration);
    }
    SampleVmdCamera(elapsed * kVmdFramesPerSecond, g_free_view.position,
        g_free_view.rotation, g_free_view.fov);
}

// ---------------------------------------------------------------------------
// Per-frame control
// ---------------------------------------------------------------------------

void StepFreeCamera() {
    const double now = NowSeconds();
    const float dt = static_cast<float>(std::clamp(now - g_free_last_step, 0.0, 0.1));
    g_free_last_step = now;
    if (dt <= 0.0f) {
        return;
    }
    switch (g_playback) {
    case FreePlayback::Motion: StepMotion(now, dt); break;
    case FreePlayback::Keyframes: StepKeyframes(now); break;
    case FreePlayback::Vmd: StepVmd(now); break;
    case FreePlayback::None: break;
    }
    if (g_playback != FreePlayback::None) {
        ClearMouseInput();
        return;
    }

    FreeCameraControl& control = g_free_target;
    const float sensitivity = g_mouse_sensitivity.load(std::memory_order_relaxed);
    const int dx = g_mouse_dx.exchange(0, std::memory_order_relaxed);
    const int dy = g_mouse_dy.exchange(0, std::memory_order_relaxed);
    const int wheel = g_mouse_wheel.exchange(0, std::memory_order_relaxed);
    control.yaw += static_cast<float>(dx) * sensitivity;
    control.pitch += static_cast<float>(g_mouse_invert_y.load(std::memory_order_relaxed)
        ? -dy : dy) * sensitivity;
    control.pitch = std::clamp(control.pitch, -kFreePitchLimit, kFreePitchLimit);
    control.fov -= static_cast<float>(wheel) / WHEEL_DELTA * kFreeFovWheelStep;

    Vector3 direction{};
    float speed = g_movement_speed.load(std::memory_order_relaxed);
    if (GameWindowHasFocus()) {
        if (KeyDown(g_roll_left_key.load(std::memory_order_relaxed))) control.roll -= kFreeRollSpeed * dt;
        if (KeyDown(g_roll_right_key.load(std::memory_order_relaxed))) control.roll += kFreeRollSpeed * dt;
        if (KeyDown(g_fov_wide_key.load(std::memory_order_relaxed))) control.fov += kFreeFovKeySpeed * dt;
        if (KeyDown(g_fov_narrow_key.load(std::memory_order_relaxed))) control.fov -= kFreeFovKeySpeed * dt;
        if (KeyDown(g_view_reset_key.load(std::memory_order_relaxed))) {
            control.roll = 0.0f;
            control.fov = g_field_of_view.load(std::memory_order_relaxed);
        }
        const Quaternion heading = FromEuler(control.pitch, control.yaw, 0.0f);
        const Vector3 forward = RotateVector(heading, {0.0f, 0.0f, 1.0f});
        const Vector3 right = RotateVector(heading, {1.0f, 0.0f, 0.0f});
        const Vector3 up{0.0f, 1.0f, 0.0f};
        if (KeyDown(VK_UP)) direction = Add(direction, forward);
        if (KeyDown(VK_DOWN)) direction = Add(direction, Scale(forward, -1.0f));
        if (KeyDown(VK_RIGHT)) direction = Add(direction, right);
        if (KeyDown(VK_LEFT)) direction = Add(direction, Scale(right, -1.0f));
        if (KeyDown(VK_PRIOR)) direction = Add(direction, up);
        if (KeyDown(VK_NEXT)) direction = Add(direction, Scale(up, -1.0f));
        if (KeyDown(VK_SHIFT)) speed *= 4.0f;
        if (KeyDown(VK_CONTROL)) speed *= 0.25f;
    }
    control.fov = std::clamp(control.fov, kFreeMinFov, kFreeMaxFov);
    control.roll = WrapDegrees(control.roll);
    control.position = Add(control.position, Scale(Normalize(direction), speed * dt));

    // Exponential smoothing towards the controlled pose; 0 disables it.
    const float smoothing = g_free_smoothing.load(std::memory_order_relaxed);
    if (smoothing <= 0.0f) {
        g_free_smoothed = control;
    } else {
        const float rate = Lerp(30.0f, 1.5f, smoothing);
        const float a = 1.0f - std::exp(-rate * dt);
        FreeCameraControl& view = g_free_smoothed;
        view.position = Lerp(view.position, control.position, a);
        view.pitch = Lerp(view.pitch, control.pitch, a);
        view.yaw = Lerp(view.yaw, control.yaw, a);
        view.roll += WrapDegrees(control.roll - view.roll) * a;
        view.fov = Lerp(view.fov, control.fov, a);
    }
    ViewFromControl(g_free_smoothed);
}

// Called from CinemachineBrain::PushStateToUnityCamera. Only the Brain on the
// captured Unity camera is rewritten.
bool BrainDrivesActiveCamera(void* brain) {
    if (brain != g_free_brain) {
        g_free_brain = brain;
        const MethodContract* game_object = Contract("unity.component.game_object");
        if (!game_object || !game_object->resolved) {
            g_free_brain_matches = true;
        } else {
            void* brain_object = Invoke(game_object, brain, nullptr);
            void* camera_object = Invoke(game_object, g_active_camera, nullptr);
            g_free_brain_matches = brain_object && brain_object == camera_object;
        }
    }
    return g_free_brain_matches;
}

void ApplyFreeCameraState(void* state) {
    StepFreeCamera();
    WriteFreeCameraState(state);
    g_free_last_push = NowSeconds();
    if (g_free_transform_fallback_logged) {
        g_free_transform_fallback_logged = false;
        Log("Free camera: Cinemachine pushes again; writing the CameraState.");
    }
}

void ExitFreeCamera(const char* reason) {
    if (!g_free_camera_active) {
        return;
    }
    g_playback = FreePlayback::None;
    g_free_camera_running.store(false, std::memory_order_release);
    ClearMouseInput();
    RestoreWorldPause("free camera exit");

    void* transform = Invoke(Contract("unity.component.transform"),
        g_active_camera, nullptr);
    if (transform) {
        SetValue(Contract("unity.transform.position.set"), transform,
            g_original_position);
        if (g_original_rotation_valid) {
            SetValue(Contract("unity.transform.rotation.set"), transform,
                g_original_rotation);
        }
    }
    SetValue(Contract("unity.camera.fov.set"), g_active_camera, g_original_fov);

    g_free_camera_active = false;
    g_changed_time_scale = false;
    g_free_brain = nullptr;
    ReleaseCameraRoot();
    Log(std::string("Free camera disabled: ") + reason);
}

bool EnterFreeCamera() {
    if (g_first_person_active.load(std::memory_order_acquire)) {
        ExitFirstPerson("switching to free camera");
    }
    void* camera = Invoke(Contract("unity.camera.main"), nullptr, nullptr);
    void* transform = Invoke(Contract("unity.component.transform"), camera, nullptr);
    Vector3 forward{};
    if (!camera || !transform ||
        !GetValue(Contract("unity.transform.position.get"), transform,
            g_original_position) ||
        !GetValue(Contract("unity.camera.fov.get"), camera, g_original_fov) ||
        !GetValue(Contract("unity.transform.forward"), transform, forward)) {
        Log("Free camera could not capture the active Unity camera.");
        return false;
    }
    g_original_rotation_valid =
        GetValue(Contract("unity.transform.rotation.get"), transform, g_original_rotation) &&
        IsUnitQuaternion(g_original_rotation);

    g_active_camera = camera;
    g_active_camera_root = g_host && g_host->gchandle_new
        ? g_host->gchandle_new(g_host->context, camera, 0)
        : 0;

    FreeCameraControl control;
    control.position = g_original_position;
    if (g_original_rotation_valid) {
        ToEuler(g_original_rotation, control.pitch, control.yaw, control.roll);
    } else {
        LookAt({}, forward, control.pitch, control.yaw);
    }
    control.pitch = std::clamp(control.pitch, -kFreePitchLimit, kFreePitchLimit);
    control.fov = g_field_of_view.load(std::memory_order_relaxed);
    g_free_target = control;
    g_free_smoothed = control;
    ViewFromControl(control);
    g_playback = FreePlayback::None;
    g_free_last_step = NowSeconds();
    g_free_last_push = -1.0;
    g_free_last_validate = g_free_last_step;
    g_free_enter_time = g_free_last_step;
    g_free_transform_fallback_logged = false;
    g_free_brain = nullptr;
    ClearMouseInput();

    g_free_camera_active = true;
    g_free_camera_running.store(true, std::memory_order_release);
    Log("Free camera enabled (mouse looks, arrow keys move, PageUp/PageDown change "
        "height, Shift/Ctrl change speed).");
    return true;
}

// The first push can lag the hotkey by a frame, so a fresh session is given a
// short grace period before falling back to transform writes.
bool BrainIsPushing(double now) {
    if (!g_push_state_hook_ready || !g_state_layout.ready) {
        return false;
    }
    if (g_free_last_push < 0.0) {
        return now - g_free_enter_time < 0.2;
    }
    return now - g_free_last_push <= kPushStateStaleSeconds;
}

// Driven by CameraManager::TailLateTick, and by the unscaled time heartbeat
// while the Brain does not push (for example while the world is frozen).
void ApplyFreeCamera() {
    const double now = NowSeconds();
    if (now - g_free_last_validate >= kCameraValidateSeconds) {
        g_free_last_validate = now;
        void* current_camera = Invoke(Contract("unity.camera.main"), nullptr, nullptr);
        if (!current_camera || current_camera != g_active_camera) {
            ExitFreeCamera("active camera changed");
            return;
        }
    }
    StepFreeCamera();
    if (!BrainIsPushing(now)) {
        if (!g_free_transform_fallback_logged) {
            g_free_transform_fallback_logged = true;
            Log("Free camera: Cinemachine is not pushing; writing the camera transform.");
        }
        WriteFreeCameraTransform();
    }
}

// Time.unscaledDeltaTime can be read many times per frame; a 3 ms floor keeps
// the fallback to about one transform write per frame.
void ApplyFreeCameraHeartbeat() {
    const double now = NowSeconds();
    if (!BrainIsPushing(now) && now - g_free_last_heartbeat >= 0.003) {
        g_free_last_heartbeat = now;
        ApplyFreeCamera();
    }
}

// Edge-triggered playback hotkeys. The VMD hotkey also enters the free camera.
void PumpFreeCameraRequests() {
    const bool motion = g_motion_request.exchange(false, std::memory_order_acq_rel);
    const bool add = g_keyframe_add_request.exchange(false, std::memory_order_acq_rel);
    const bool play = g_keyframe_play_request.exchange(false, std::memory_order_acq_rel);
    const bool clear = g_keyframe_clear_request.exchange(false, std::memory_order_acq_rel);
    const bool vmd = g_vmd_play_request.exchange(false, std::memory_order_acq_rel);
    if (vmd && !g_free_camera_active && !EnterFreeCamera()) {
        return;
    }
    if (!g_free_camera_active) {
        return;
    }
    if (clear) {
        g_keyframes.clear();
        Log("Free camera keyframes cleared.");
    }
    if (add) {
        AddKeyframe();
    }
    const auto toggle = [](FreePlayback kind, void (*start)()) {
        if (g_playback == kind) {
            StopPlayback("hotkey");
        } else {
            StopPlayback("switching playback");
            start();
        }
    };
    if (motion) toggle(FreePlayback::Motion, &StartMotion);
    if (play) toggle(FreePlayback::Keyframes, &StartKeyframes);
    if (vmd) toggle(FreePlayback::Vmd, &StartVmd);
}
