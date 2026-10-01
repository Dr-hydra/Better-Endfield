// MMD overlay companion: a clickable panel over the game window for choosing
// a library work and controlling MMD playback without a numpad. Rendering and
// window following reuse the combat overlay's approach (GDI+ layered window
// owned by the game window). The window never activates, so the game keeps
// keyboard focus; commands go to BetterEndfield.Camera through shared memory.
#include "../mmd_library.h"
#include "../mmd_overlay_protocol.h"

#include <Windows.h>
#include <objidl.h>
#include <gdiplus.h>
#include <shellapi.h>

#include <algorithm>
#include <array>
#include <cmath>
#include <cstdint>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfield::MmdOverlay {
namespace {

using namespace Gdiplus;
namespace Proto = MmdOverlayProtocol;

constexpr wchar_t kWindowClass[] = L"BetterEndfield.MmdOverlay.Window";
constexpr int kWidth = 420;
constexpr int kHeaderHeight = 48;
constexpr int kRowHeight = 30;
constexpr int kVisibleRows = 6;
constexpr int kWorksTop = 84;
constexpr int kProgressTop = kWorksTop + kRowHeight * kVisibleRows + 12;
constexpr int kTransportTop = kProgressTop + 44;
constexpr int kModeTop = kTransportTop + 44;
constexpr int kToolsTop = kModeTop + 40;
constexpr int kToolsTop2 = kToolsTop + 36;
constexpr int kMessageTop = kToolsTop2 + 40;
constexpr int kHeight = kMessageTop + 52;
constexpr ULONGLONG kScanIntervalMs = 3000;

enum ButtonId : int {
    ButtonNone = 0,
    ButtonClose,
    ButtonScrollUp,
    ButtonScrollDown,
    ButtonClearWork,
    ButtonProgress,
    ButtonSeekBack,
    ButtonPlayPause,
    ButtonStop,
    ButtonSeekForward,
    ButtonLoop,
    ButtonModeVmd,
    ButtonModeFree,
    ButtonModeGame,
    ButtonKeyAdd,
    ButtonKeyPlay,
    ButtonKeyClear,
    ButtonKeySave,
    ButtonKeyLoad,
    ButtonMotion,
    ButtonFreeCamera,
    ButtonWorkBase = 1000,
};

struct Button {
    int id = ButtonNone;
    RectF rect;
    bool enabled = true;
};

HINSTANCE g_instance = nullptr;
HWND g_window = nullptr;
DWORD g_game_pid = 0;
HANDLE g_game_process = nullptr;
HANDLE g_mapping = nullptr;
Proto::Shared* g_shared = nullptr;
Proto::Status g_status{};
bool g_status_valid = false;
std::filesystem::path g_library_root;
std::vector<MmdLibrary::Work> g_works;
ULONGLONG g_last_scan = 0;
int g_scroll = 0;
std::vector<Button> g_buttons;
int g_hover = ButtonNone;
int g_pressed = ButtonNone;
bool g_tracking_mouse = false;
bool g_dragging = false;
POINT g_drag_offset{};
int g_offset_x = 24;
int g_offset_y = 120;
bool g_has_saved_position = false;
POINT g_window_position{};
HWND g_game_window = nullptr;
HWND g_owned_game_window = nullptr;
ULONG_PTR g_gdiplus_token = 0;
std::filesystem::path g_log_path;

std::filesystem::path DataDirectory() {
    wchar_t local_app_data[32768]{};
    const DWORD length = GetEnvironmentVariableW(L"LOCALAPPDATA", local_app_data,
        static_cast<DWORD>(std::size(local_app_data)));
    std::filesystem::path directory = length
        ? std::filesystem::path(local_app_data) / L"BetterEndfield"
        : std::filesystem::temp_directory_path() / L"BetterEndfield";
    std::error_code error;
    std::filesystem::create_directories(directory, error);
    return directory;
}

void OverlayLog(std::string_view message) {
    if (g_log_path.empty()) g_log_path = DataDirectory() / L"mmd-overlay.log";
    SYSTEMTIME now{};
    GetLocalTime(&now);
    char prefix[64]{};
    std::snprintf(prefix, sizeof(prefix), "[%02u:%02u:%02u.%03u] ",
        now.wHour, now.wMinute, now.wSecond, now.wMilliseconds);
    std::ofstream output(g_log_path, std::ios::app | std::ios::binary);
    if (output) output << prefix << message << "\r\n";
}

std::wstring Utf8ToWide(std::string_view value) {
    if (value.empty()) return {};
    const int length = MultiByteToWideChar(CP_UTF8, 0, value.data(),
        static_cast<int>(value.size()), nullptr, 0);
    if (length <= 0) return {};
    std::wstring result(static_cast<size_t>(length), L'\0');
    MultiByteToWideChar(CP_UTF8, 0, value.data(), static_cast<int>(value.size()),
        result.data(), length);
    return result;
}

bool IsEnglish() {
    static int cached = -1;
    static ULONGLONG last_check = 0;
    const ULONGLONG now = GetTickCount64();
    if (cached != -1 && now - last_check < 3000) return cached == 1;
    last_check = now;
    const std::filesystem::path ini = DataDirectory() / L"BetterEndfield.ini";
    wchar_t buffer[64]{};
    GetPrivateProfileStringW(L"Launcher", L"Language", L"", buffer,
        static_cast<DWORD>(std::size(buffer)), ini.c_str());
    if (_wcsicmp(buffer, L"en_US") == 0 || _wcsicmp(buffer, L"en-US") == 0 ||
        _wcsicmp(buffer, L"en") == 0 || _wcsicmp(buffer, L"English") == 0) {
        cached = 1;
    } else if (buffer[0]) {
        cached = 0;
    } else {
        cached = PRIMARYLANGID(GetUserDefaultUILanguage()) == LANG_CHINESE ? 0 : 1;
    }
    return cached == 1;
}

const wchar_t* T(const wchar_t* zh, const wchar_t* en) { return IsEnglish() ? en : zh; }

std::wstring FormatTime(double seconds) {
    const int total = static_cast<int>(std::max(0.0, seconds));
    wchar_t text[32]{};
    swprintf_s(text, L"%02d:%02d", total / 60, total % 60);
    return text;
}

void AddRoundedRect(GraphicsPath& path, const RectF& rect, float radius) {
    const float diameter = radius * 2.0f;
    path.AddArc(rect.X, rect.Y, diameter, diameter, 180.0f, 90.0f);
    path.AddArc(rect.GetRight() - diameter, rect.Y, diameter, diameter, 270.0f, 90.0f);
    path.AddArc(rect.GetRight() - diameter, rect.GetBottom() - diameter, diameter, diameter, 0.0f, 90.0f);
    path.AddArc(rect.X, rect.GetBottom() - diameter, diameter, diameter, 90.0f, 90.0f);
    path.CloseFigure();
}

void DrawLabel(Graphics& graphics, const std::wstring& text, const Font& font, const Color& color,
    const RectF& rect, StringAlignment alignment = StringAlignmentNear) {
    SolidBrush brush(color);
    StringFormat format;
    format.SetAlignment(alignment);
    format.SetLineAlignment(StringAlignmentCenter);
    format.SetTrimming(StringTrimmingEllipsisCharacter);
    format.SetFormatFlags(StringFormatFlagsNoWrap);
    graphics.DrawString(text.c_str(), -1, &font, rect, &format, &brush);
}

// ---------------------------------------------------------------------------
// Shared memory
// ---------------------------------------------------------------------------
bool ReadStatus() {
    if (!g_shared) return false;
    for (int attempt = 0; attempt < 4; ++attempt) {
        const LONG before = g_shared->sequence;
        if (before & 1) {
            YieldProcessor();
            continue;
        }
        MemoryBarrier();
        Proto::Status status;
        std::memcpy(&status, &g_shared->status, sizeof(status));
        MemoryBarrier();
        if (before == g_shared->sequence) {
            status.work[sizeof(status.work) - 1] = '\0';
            status.message[sizeof(status.message) - 1] = '\0';
            g_status = status;
            return true;
        }
    }
    return false;
}

void Send(Proto::CommandType type, int32_t argument = 0, double value = 0.0,
    std::string_view text = {}) {
    if (!g_shared) return;
    const LONG write = g_shared->command_write;
    const LONG read = g_shared->command_read;
    if (static_cast<uint32_t>(write - read) >= Proto::kCommandCapacity) {
        OverlayLog("command dropped (queue full) type=" + std::to_string(static_cast<uint32_t>(type)));
        return;
    }
    Proto::Command& slot = g_shared->commands[static_cast<uint32_t>(write) % Proto::kCommandCapacity];
    slot.type = static_cast<uint32_t>(type);
    slot.argument = argument;
    slot.value = value;
    const size_t length = std::min(text.size(), sizeof(slot.text) - 1);
    std::memcpy(slot.text, text.data(), length);
    slot.text[length] = '\0';
    MemoryBarrier();
    InterlockedIncrement(&g_shared->command_write);
    OverlayLog("command sent type=" + std::to_string(static_cast<uint32_t>(type)));
}

// ---------------------------------------------------------------------------
// Game window following (same approach as the combat overlay)
// ---------------------------------------------------------------------------
struct FindWindowContext {
    DWORD pid = 0;
    HWND best = nullptr;
    uint64_t area = 0;
};

BOOL CALLBACK FindWindowCallback(HWND window, LPARAM parameter) {
    auto* context = reinterpret_cast<FindWindowContext*>(parameter);
    DWORD pid = 0;
    GetWindowThreadProcessId(window, &pid);
    if (pid != context->pid || !IsWindowVisible(window) || GetWindow(window, GW_OWNER)) return TRUE;
    RECT client{};
    if (!GetClientRect(window, &client)) return TRUE;
    const uint64_t area = static_cast<uint64_t>(std::max(0L, client.right)) *
        static_cast<uint64_t>(std::max(0L, client.bottom));
    if (area > context->area) {
        context->area = area;
        context->best = window;
    }
    return TRUE;
}

HWND FindGameWindow() {
    FindWindowContext context{g_game_pid};
    EnumWindows(FindWindowCallback, reinterpret_cast<LPARAM>(&context));
    return context.best;
}

bool WindowBelongsToGame(HWND window) {
    if (!window) return false;
    DWORD pid = 0;
    GetWindowThreadProcessId(window, &pid);
    return pid == g_game_pid;
}

void RefreshGameWindowFromForeground() {
    HWND foreground = GetForegroundWindow();
    if (!WindowBelongsToGame(foreground)) return;
    HWND root = GetAncestor(foreground, GA_ROOT);
    if (root && WindowBelongsToGame(root)) foreground = root;
    if (IsWindow(foreground) && IsWindowVisible(foreground)) g_game_window = foreground;
}

bool GameClientRect(RECT& result) {
    if (!g_game_window || !IsWindow(g_game_window)) g_game_window = FindGameWindow();
    if (!g_game_window || IsIconic(g_game_window)) return false;
    RECT client{};
    if (!GetClientRect(g_game_window, &client)) return false;
    POINT origin{};
    if (!ClientToScreen(g_game_window, &origin)) return false;
    result = {origin.x, origin.y, origin.x + client.right, origin.y + client.bottom};
    return client.right > 0 && client.bottom > 0;
}

void BindOverlayToGameWindow() {
    if (!g_window || !g_game_window || !IsWindow(g_game_window) ||
        g_owned_game_window == g_game_window) {
        return;
    }
    SetLastError(ERROR_SUCCESS);
    const LONG_PTR previous = SetWindowLongPtrW(g_window, GWLP_HWNDPARENT,
        reinterpret_cast<LONG_PTR>(g_game_window));
    if (!previous && GetLastError() != ERROR_SUCCESS) return;
    g_owned_game_window = g_game_window;
}

std::filesystem::path SettingsPath() { return DataDirectory() / L"mmd-overlay.ini"; }

void LoadPosition() {
    wchar_t buffer[64]{};
    const std::filesystem::path path = SettingsPath();
    GetPrivateProfileStringW(L"Position", L"X", L"", buffer, static_cast<DWORD>(std::size(buffer)), path.c_str());
    if (!buffer[0]) return;
    g_offset_x = _wtoi(buffer);
    GetPrivateProfileStringW(L"Position", L"Y", L"120", buffer, static_cast<DWORD>(std::size(buffer)), path.c_str());
    g_offset_y = _wtoi(buffer);
    g_has_saved_position = true;
}

void SavePosition() {
    const std::filesystem::path path = SettingsPath();
    WritePrivateProfileStringW(L"Position", L"X", std::to_wstring(g_offset_x).c_str(), path.c_str());
    WritePrivateProfileStringW(L"Position", L"Y", std::to_wstring(g_offset_y).c_str(), path.c_str());
}

void UpdatePosition() {
    RECT game{};
    if (!GameClientRect(game)) return;
    if (g_dragging) {
        POINT cursor{};
        GetCursorPos(&cursor);
        g_offset_x = cursor.x - g_drag_offset.x - game.left;
        g_offset_y = cursor.y - g_drag_offset.y - game.top;
    }
    g_offset_x = std::clamp(g_offset_x, 0, static_cast<int>(std::max(0L, game.right - game.left - kWidth)));
    g_offset_y = std::clamp(g_offset_y, 0, static_cast<int>(std::max(0L, game.bottom - game.top - kHeight)));
    g_window_position = {game.left + g_offset_x, game.top + g_offset_y};
}

// ---------------------------------------------------------------------------
// Rendering
// ---------------------------------------------------------------------------
const Color kAccent(255, 67, 201, 255);
const Color kText(255, 241, 244, 249);
const Color kMuted(200, 170, 180, 196);
const Color kDisabled(120, 140, 148, 162);

void AddButton(int id, const RectF& rect, bool enabled = true) {
    g_buttons.push_back({id, rect, enabled});
}

void DrawButton(Graphics& graphics, int id, const RectF& rect, const std::wstring& text,
    const Font& font, bool enabled = true, bool active = false) {
    AddButton(id, rect, enabled);
    GraphicsPath path;
    AddRoundedRect(path, rect, 7.0f);
    Color fill = active ? Color(210, 40, 132, 176) : Color(150, 52, 60, 76);
    if (enabled && g_hover == id) fill = active ? Color(235, 52, 160, 210) : Color(200, 70, 80, 100);
    if (enabled && g_pressed == id) fill = Color(235, 30, 110, 150);
    SolidBrush brush(fill);
    graphics.FillPath(&brush, &path);
    DrawLabel(graphics, text, font, enabled ? kText : kDisabled, rect, StringAlignmentCenter);
}

std::wstring MessageText() {
    using Code = Proto::MessageCode;
    const std::wstring detail = Utf8ToWide(g_status.message);
    switch (static_cast<Code>(g_status.message_code)) {
    case Code::Loading: return T(L"正在加载…", L"Loading…");
    case Code::Playing: return detail;
    case Code::Paused: return T(L"已暂停", L"Paused");
    case Code::Stopped: return T(L"已停止", L"Stopped");
    case Code::Finished: return T(L"播放结束", L"Finished");
    case Code::NoWork: return T(L"请先选择作品（在管理器里导入）", L"Select a work (import it in the manager)");
    case Code::WorkInvalid: return std::wstring(T(L"作品无效：", L"Invalid work: ")) + detail;
    case Code::NothingPlayable: return T(L"没有可播放的内容（检查动作/镜头/音乐开关）", L"Nothing playable (check motion/camera/music settings)");
    case Code::MotionFailed: return T(L"角色动作绑定失败，详见日志", L"Character motion failed; see the log");
    case Code::CameraFailed: return std::wstring(T(L"镜头 VMD 无效：", L"Camera VMD rejected: ")) + detail;
    case Code::MusicFailed: return std::wstring(T(L"音乐播放失败：", L"Music failed: ")) + detail;
    case Code::MusicModuleMissing: return T(L"播放音乐需要启用音乐模块", L"Music needs the Music module");
    case Code::FreeCameraDisabled: return T(L"请在管理器中启用自由视角", L"Enable the free camera in the manager");
    case Code::CharacterDisabled: return T(L"角色动作未启用，只播放镜头和音乐", L"Character motion off; camera/music only");
    case Code::CameraMode: return detail;
    case Code::WorkSelected: return detail;
    case Code::LoopChanged: return g_status.loop ? T(L"循环：开", L"Loop on") : T(L"循环：关", L"Loop off");
    default: return detail;
    }
}

void Render() {
    UpdatePosition();
    g_buttons.clear();
    BITMAPINFO info{};
    info.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
    info.bmiHeader.biWidth = kWidth;
    info.bmiHeader.biHeight = -kHeight;
    info.bmiHeader.biPlanes = 1;
    info.bmiHeader.biBitCount = 32;
    info.bmiHeader.biCompression = BI_RGB;
    void* bits = nullptr;
    HDC screen = GetDC(nullptr);
    HDC memory = CreateCompatibleDC(screen);
    HBITMAP dib = CreateDIBSection(screen, &info, DIB_RGB_COLORS, &bits, nullptr, 0);
    if (!dib || !bits) {
        if (dib) DeleteObject(dib);
        DeleteDC(memory);
        ReleaseDC(nullptr, screen);
        return;
    }
    HGDIOBJ old_bitmap = SelectObject(memory, dib);
    std::memset(bits, 0, static_cast<size_t>(kWidth) * kHeight * 4);
    {
        Bitmap canvas(kWidth, kHeight, kWidth * 4, PixelFormat32bppPARGB, static_cast<BYTE*>(bits));
        Graphics graphics(&canvas);
        graphics.SetSmoothingMode(SmoothingModeAntiAlias);
        graphics.SetTextRenderingHint(TextRenderingHintClearTypeGridFit);

        GraphicsPath background;
        AddRoundedRect(background, RectF(0.5f, 0.5f, kWidth - 1.0f, kHeight - 1.0f), 14.0f);
        SolidBrush background_brush(Color(232, 17, 20, 27));
        graphics.FillPath(&background_brush, &background);
        Pen border(Color(90, 255, 255, 255), 1.0f);
        graphics.DrawPath(&border, &background);
        SolidBrush accent(kAccent);
        graphics.FillRectangle(&accent, 18.0f, 14.0f, 4.0f, 22.0f);

        Font title(L"Microsoft YaHei UI", 16.0f, FontStyleBold, UnitPixel);
        Font label(L"Microsoft YaHei UI", 12.0f, FontStyleRegular, UnitPixel);
        Font caption(L"Microsoft YaHei UI", 11.0f, FontStyleRegular, UnitPixel);
        Font strong(L"Microsoft YaHei UI", 13.0f, FontStyleBold, UnitPixel);
        DrawLabel(graphics, T(L"MMD 控制台", L"MMD Console"), title, kText, RectF(30, 10, 160, 30));

        using State = Proto::State;
        const State state = static_cast<State>(g_status.state);
        const wchar_t* state_text = state == State::Playing ? T(L"播放中", L"Playing")
            : state == State::Paused ? T(L"已暂停", L"Paused")
            : state == State::Loading ? T(L"加载中", L"Loading") : T(L"空闲", L"Idle");
        const Color state_color = state == State::Playing ? Color(255, 87, 224, 154)
            : state == State::Idle ? Color(255, 163, 171, 186) : Color(255, 255, 206, 82);
        GraphicsPath pill;
        AddRoundedRect(pill, RectF(196, 14, 64, 22), 11.0f);
        SolidBrush pill_brush(Color(52, state_color.GetR(), state_color.GetG(), state_color.GetB()));
        graphics.FillPath(&pill_brush, &pill);
        DrawLabel(graphics, state_text, caption, state_color, RectF(196, 14, 64, 22), StringAlignmentCenter);
        DrawButton(graphics, ButtonClose, RectF(kWidth - 42.0f, 12, 26, 26), L"×", strong);

        // Works
        const std::wstring works_title = std::wstring(T(L"作品库", L"Works")) + L"  (" +
            std::to_wstring(g_works.size()) + L")";
        DrawLabel(graphics, works_title, label, kMuted, RectF(20, 54, 200, 24));
        DrawButton(graphics, ButtonClearWork, RectF(kWidth - 170.0f, 54, 70, 24),
            T(L"不选作品", L"No work"), caption, g_status.work[0] != '\0');
        const int max_scroll = std::max(0, static_cast<int>(g_works.size()) - kVisibleRows);
        g_scroll = std::clamp(g_scroll, 0, max_scroll);
        DrawButton(graphics, ButtonScrollUp, RectF(kWidth - 94.0f, 54, 34, 24), L"▲", caption, g_scroll > 0);
        DrawButton(graphics, ButtonScrollDown, RectF(kWidth - 56.0f, 54, 34, 24), L"▼", caption, g_scroll < max_scroll);
        const std::string selected(g_status.work);
        if (g_works.empty()) {
            DrawLabel(graphics, T(L"还没有作品。请在管理器的“MMD 作品库”里导入。",
                L"No works yet. Import them in the manager's MMD library."),
                label, kMuted, RectF(20, static_cast<float>(kWorksTop), kWidth - 40.0f, 30));
        }
        for (int row = 0; row < kVisibleRows; ++row) {
            const size_t index = static_cast<size_t>(g_scroll + row);
            if (index >= g_works.size()) break;
            const auto& work = g_works[index];
            const RectF rect(18.0f, static_cast<float>(kWorksTop + row * kRowHeight), kWidth - 36.0f, kRowHeight - 4.0f);
            const int id = ButtonWorkBase + static_cast<int>(index);
            AddButton(id, rect);
            const bool is_selected = work.folder_name == selected;
            GraphicsPath path;
            AddRoundedRect(path, rect, 6.0f);
            Color fill = is_selected ? Color(200, 40, 132, 176) : Color(90, 52, 60, 76);
            if (g_hover == id && !is_selected) fill = Color(160, 70, 80, 100);
            SolidBrush brush(fill);
            graphics.FillPath(&brush, &path);
            std::wstring tags;
            if (!work.camera.empty()) tags += T(L" 镜头", L" cam");
            if (!work.music.empty()) tags += T(L" 音乐", L" music");
            if (!work.face.empty()) tags += T(L" 表情", L" face");
            DrawLabel(graphics, Utf8ToWide(work.name), label, kText,
                RectF(rect.X + 10, rect.Y, rect.Width - 130, rect.Height));
            DrawLabel(graphics, tags, caption, kMuted,
                RectF(rect.GetRight() - 124, rect.Y, 116, rect.Height), StringAlignmentFar);
        }

        // Progress
        const bool active = state == State::Playing || state == State::Paused;
        const double duration = std::max(0.0, g_status.duration);
        const double seconds = std::clamp(g_status.seconds, 0.0, duration);
        DrawLabel(graphics, FormatTime(seconds) + L" / " + FormatTime(duration), label, kText,
            RectF(20, static_cast<float>(kProgressTop), 160, 22));
        const RectF bar(20.0f, kProgressTop + 26.0f, kWidth - 40.0f, 8.0f);
        AddButton(ButtonProgress, RectF(bar.X, bar.Y - 8, bar.Width, bar.Height + 16), active);
        GraphicsPath bar_path;
        AddRoundedRect(bar_path, bar, 4.0f);
        SolidBrush bar_background(Color(90, 107, 115, 132));
        graphics.FillPath(&bar_background, &bar_path);
        if (duration > 0.0) {
            const float filled = static_cast<float>(bar.Width * seconds / duration);
            SolidBrush filled_brush(kAccent);
            const GraphicsState saved = graphics.Save();
            graphics.SetClip(&bar_path);
            graphics.FillRectangle(&filled_brush, bar.X, bar.Y, filled, bar.Height);
            graphics.Restore(saved);
        }

        // Transport
        const float gap = 8.0f;
        const float transport_width = (kWidth - 40.0f - gap * 4) / 5.0f;
        const auto transport_rect = [&](int index) {
            return RectF(20.0f + index * (transport_width + gap), static_cast<float>(kTransportTop),
                transport_width, 34.0f);
        };
        DrawButton(graphics, ButtonSeekBack, transport_rect(0), T(L"-5 秒", L"-5 s"), label, active);
        DrawButton(graphics, ButtonPlayPause, transport_rect(1),
            state == State::Playing ? T(L"暂停", L"Pause") : T(L"播放", L"Play"), strong, true,
            state == State::Playing);
        DrawButton(graphics, ButtonStop, transport_rect(2), T(L"停止", L"Stop"), label, state != State::Idle);
        DrawButton(graphics, ButtonSeekForward, transport_rect(3), T(L"+5 秒", L"+5 s"), label, active);
        DrawButton(graphics, ButtonLoop, transport_rect(4), T(L"循环", L"Loop"), label, true, g_status.loop != 0);

        // Camera mode
        DrawLabel(graphics, T(L"镜头", L"Camera"), label, kMuted, RectF(20, static_cast<float>(kModeTop), 50, 30));
        const float mode_width = (kWidth - 90.0f - gap * 2) / 3.0f;
        const wchar_t* modes[]{T(L"VMD 镜头", L"VMD"), T(L"自由视角", L"Free"), T(L"游戏镜头", L"Game")};
        const int mode_ids[]{ButtonModeVmd, ButtonModeFree, ButtonModeGame};
        for (int mode = 0; mode < 3; ++mode) {
            DrawButton(graphics, mode_ids[mode],
                RectF(70.0f + mode * (mode_width + gap), static_cast<float>(kModeTop), mode_width, 30.0f),
                modes[mode], label, true, g_status.camera_mode == static_cast<uint32_t>(mode));
        }

        // Keyframes and free camera tools
        DrawLabel(graphics, T(L"机位", L"Keys"), label, kMuted, RectF(20, static_cast<float>(kToolsTop), 50, 30));
        const float tool_width = (kWidth - 90.0f - gap * 4) / 5.0f;
        const auto tool_rect = [&](int index, int top) {
            return RectF(70.0f + index * (tool_width + gap), static_cast<float>(top), tool_width, 30.0f);
        };
        const bool free_camera = g_status.free_camera_active != 0;
        DrawButton(graphics, ButtonKeyAdd, tool_rect(0, kToolsTop), T(L"记录", L"Add"), caption, free_camera);
        DrawButton(graphics, ButtonKeyPlay, tool_rect(1, kToolsTop), T(L"播放", L"Play"), caption,
            free_camera && g_status.keyframe_count >= 2);
        DrawButton(graphics, ButtonKeyClear, tool_rect(2, kToolsTop), T(L"清空", L"Clear"), caption,
            free_camera && g_status.keyframe_count > 0);
        DrawButton(graphics, ButtonKeySave, tool_rect(3, kToolsTop), T(L"保存", L"Save"), caption,
            g_status.keyframe_count > 0);
        DrawButton(graphics, ButtonKeyLoad, tool_rect(4, kToolsTop), T(L"读取", L"Load"), caption);
        DrawLabel(graphics, std::to_wstring(g_status.keyframe_count) + T(L" 个", L" keys"), caption, kMuted,
            RectF(20, static_cast<float>(kToolsTop2), 50, 30));
        DrawButton(graphics, ButtonFreeCamera, tool_rect(0, kToolsTop2),
            free_camera ? T(L"退出自由", L"Exit free") : T(L"自由视角", L"Free cam"), caption,
            (g_status.available & Proto::FeatureFreeCamera) != 0, free_camera);
        DrawButton(graphics, ButtonMotion, tool_rect(1, kToolsTop2), T(L"运镜", L"Motion"), caption, free_camera);

        DrawLabel(graphics, MessageText(), caption, kMuted,
            RectF(20, static_cast<float>(kMessageTop), kWidth - 40.0f, 22));
        DrawLabel(graphics, T(L"小键盘 - 显示/隐藏 · 拖动标题栏移动 · 按住 Alt 显示鼠标",
            L"Numpad - toggles · drag the title to move · hold Alt for the cursor"),
            caption, Color(140, 153, 163, 181), RectF(20, kMessageTop + 24.0f, kWidth - 40.0f, 22),
            StringAlignmentCenter);
    }
    POINT source{};
    SIZE size{kWidth, kHeight};
    BLENDFUNCTION blend{AC_SRC_OVER, 0, 255, AC_SRC_ALPHA};
    UpdateLayeredWindow(g_window, screen, &g_window_position, &size, memory, &source, 0, &blend, ULW_ALPHA);
    SelectObject(memory, old_bitmap);
    DeleteObject(dib);
    DeleteDC(memory);
    ReleaseDC(nullptr, screen);
}

// ---------------------------------------------------------------------------
// Input
// ---------------------------------------------------------------------------
int HitTest(int x, int y) {
    for (auto it = g_buttons.rbegin(); it != g_buttons.rend(); ++it) {
        if (it->enabled && it->rect.Contains(static_cast<REAL>(x), static_cast<REAL>(y))) return it->id;
    }
    return ButtonNone;
}

void Activate(int id, int x) {
    using Type = Proto::CommandType;
    switch (id) {
    case ButtonClose: Send(Type::Hide); break;
    case ButtonScrollUp: g_scroll = std::max(0, g_scroll - kVisibleRows); break;
    case ButtonScrollDown: g_scroll += kVisibleRows; break;
    case ButtonClearWork: Send(Type::SelectWork, 0, 0.0, ""); break;
    case ButtonProgress: {
        const float left = 20.0f, width = kWidth - 40.0f;
        const double ratio = std::clamp((x - left) / width, 0.0f, 1.0f);
        Send(Type::SeekAbsolute, 0, ratio * g_status.duration);
        break;
    }
    case ButtonSeekBack: Send(Type::SeekRelative, 0, -5.0); break;
    case ButtonPlayPause: Send(Type::PlayPause); break;
    case ButtonStop: Send(Type::Stop); break;
    case ButtonSeekForward: Send(Type::SeekRelative, 0, 5.0); break;
    case ButtonLoop: Send(Type::ToggleLoop); break;
    case ButtonModeVmd: Send(Type::CameraMode, 0); break;
    case ButtonModeFree: Send(Type::CameraMode, 1); break;
    case ButtonModeGame: Send(Type::CameraMode, 2); break;
    case ButtonKeyAdd: Send(Type::KeyframeAdd); break;
    case ButtonKeyPlay: Send(Type::KeyframePlay); break;
    case ButtonKeyClear: Send(Type::KeyframeClear); break;
    case ButtonKeySave: Send(Type::KeyframeSave); break;
    case ButtonKeyLoad: Send(Type::KeyframeLoad); break;
    case ButtonMotion: Send(Type::MotionPreset); break;
    case ButtonFreeCamera: Send(Type::FreeCamera); break;
    default:
        if (id >= ButtonWorkBase) {
            const size_t index = static_cast<size_t>(id - ButtonWorkBase);
            if (index < g_works.size()) Send(Type::SelectWork, 0, 0.0, g_works[index].folder_name);
        }
        break;
    }
}

bool ShouldShow() {
    if (!g_status_valid || !g_shared || !InterlockedCompareExchange(&g_shared->visible, 0, 0)) return false;
    if (!g_game_window || !IsWindowVisible(g_game_window) || IsIconic(g_game_window)) return false;
    if (g_owned_game_window == g_game_window) return true;
    const HWND foreground = GetForegroundWindow();
    return foreground == g_window || WindowBelongsToGame(foreground);
}

void Tick() {
    if (g_game_process && WaitForSingleObject(g_game_process, 0) != WAIT_TIMEOUT) {
        PostQuitMessage(0);
        return;
    }
    if (g_shared && InterlockedCompareExchange(&g_shared->shutdown_requested, 0, 0)) {
        PostQuitMessage(0);
        return;
    }
    g_status_valid = ReadStatus() || g_status_valid;
    RefreshGameWindowFromForeground();
    if (!g_game_window || !IsWindow(g_game_window)) g_game_window = FindGameWindow();
    BindOverlayToGameWindow();
    if (!ShouldShow()) {
        ShowWindow(g_window, SW_HIDE);
        return;
    }
    const ULONGLONG now = GetTickCount64();
    if (!g_last_scan || now - g_last_scan >= kScanIntervalMs) {
        g_last_scan = now;
        g_works = MmdLibrary::Scan(g_library_root);
    }
    if (!IsWindowVisible(g_window)) {
        ShowWindow(g_window, SW_SHOWNOACTIVATE);
        SetWindowPos(g_window, HWND_TOP, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        g_last_scan = 0;
    }
    Render();
}

LRESULT CALLBACK WindowProc(HWND window, UINT message, WPARAM wparam, LPARAM lparam) {
    const int x = static_cast<short>(LOWORD(lparam));
    const int y = static_cast<short>(HIWORD(lparam));
    switch (message) {
    case WM_TIMER:
        Tick();
        return 0;
    case WM_MOUSEACTIVATE:
        return MA_NOACTIVATE;
    case WM_MOUSEMOVE: {
        if (!g_tracking_mouse) {
            TRACKMOUSEEVENT track{sizeof(track), TME_LEAVE, window, 0};
            g_tracking_mouse = TrackMouseEvent(&track) != FALSE;
        }
        const int hover = HitTest(x, y);
        if (hover != g_hover || g_dragging) {
            g_hover = hover;
            Render();
        }
        return 0;
    }
    case WM_MOUSELEAVE:
        g_tracking_mouse = false;
        g_hover = ButtonNone;
        Render();
        return 0;
    case WM_LBUTTONDOWN:
        g_pressed = HitTest(x, y);
        if (g_pressed == ButtonNone && y < kHeaderHeight) {
            g_dragging = true;
            g_drag_offset = {x, y};
        }
        SetCapture(window);
        Render();
        return 0;
    case WM_LBUTTONUP: {
        // ReleaseCapture sends WM_CAPTURECHANGED synchronously, which clears
        // the press; take it first.
        const int pressed = g_pressed;
        const bool dragging = g_dragging;
        g_pressed = ButtonNone;
        g_dragging = false;
        ReleaseCapture();
        if (dragging) {
            SavePosition();
        } else if (pressed != ButtonNone && HitTest(x, y) == pressed) {
            Activate(pressed, x);
        }
        Render();
        return 0;
    }
    case WM_CAPTURECHANGED:
        g_dragging = false;
        g_pressed = ButtonNone;
        return 0;
    case WM_MOUSEWHEEL:
        g_scroll += GET_WHEEL_DELTA_WPARAM(wparam) > 0 ? -1 : 1;
        Render();
        return 0;
    case WM_DESTROY:
        PostQuitMessage(0);
        return 0;
    default:
        return DefWindowProcW(window, message, wparam, lparam);
    }
}

bool ParseArguments(std::wstring& mapping_name) {
    int count = 0;
    LPWSTR* arguments = CommandLineToArgvW(GetCommandLineW(), &count);
    if (!arguments) return false;
    for (int index = 1; index < count; ++index) {
        const std::wstring_view argument(arguments[index]);
        if (argument == L"--game-pid" && index + 1 < count) {
            g_game_pid = wcstoul(arguments[++index], nullptr, 10);
        } else if (argument == L"--mapping" && index + 1 < count) {
            mapping_name = arguments[++index];
        }
    }
    LocalFree(arguments);
    return g_game_pid && !mapping_name.empty();
}

} // namespace

int Run(HINSTANCE instance) {
    g_instance = instance;
    OverlayLog("companion starting pid=" + std::to_string(GetCurrentProcessId()));
    SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
    std::wstring mapping_name;
    if (!ParseArguments(mapping_name)) {
        OverlayLog("argument parsing failed");
        return 2;
    }
    g_game_process = OpenProcess(SYNCHRONIZE, FALSE, g_game_pid);
    g_mapping = OpenFileMappingW(FILE_MAP_READ | FILE_MAP_WRITE, FALSE, mapping_name.c_str());
    if (!g_mapping) {
        OverlayLog("OpenFileMapping failed error=" + std::to_string(GetLastError()));
        return 3;
    }
    g_shared = static_cast<Proto::Shared*>(MapViewOfFile(g_mapping, FILE_MAP_READ | FILE_MAP_WRITE,
        0, 0, sizeof(Proto::Shared)));
    if (!g_shared || g_shared->magic != Proto::kMagic || g_shared->version != Proto::kVersion ||
        g_shared->structure_size != sizeof(Proto::Shared)) {
        OverlayLog("shared memory rejected (missing or version mismatch)");
        return 4;
    }
    g_shared->library_root[Proto::kPathCapacity - 1] = L'\0';
    g_library_root = std::filesystem::path(g_shared->library_root);
    GdiplusStartupInput startup;
    if (GdiplusStartup(&g_gdiplus_token, &startup, nullptr) != Ok) {
        OverlayLog("GDI+ startup failed");
        return 5;
    }
    LoadPosition();
    WNDCLASSEXW window_class{sizeof(window_class)};
    window_class.lpfnWndProc = WindowProc;
    window_class.hInstance = instance;
    window_class.hCursor = LoadCursorW(nullptr, IDC_ARROW);
    window_class.lpszClassName = kWindowClass;
    RegisterClassExW(&window_class);
    g_window = CreateWindowExW(WS_EX_LAYERED | WS_EX_TOOLWINDOW | WS_EX_NOACTIVATE, kWindowClass,
        L"Better Endfield MMD", WS_POPUP, 0, 0, kWidth, kHeight, nullptr, nullptr, instance, nullptr);
    if (!g_window) {
        OverlayLog("CreateWindowEx failed error=" + std::to_string(GetLastError()));
        return 6;
    }
    SetTimer(g_window, 1, 100, nullptr);
    Tick();
    MSG message{};
    while (GetMessageW(&message, nullptr, 0, 0) > 0) {
        TranslateMessage(&message);
        DispatchMessageW(&message);
    }
    KillTimer(g_window, 1);
    UnmapViewOfFile(g_shared);
    CloseHandle(g_mapping);
    if (g_game_process) CloseHandle(g_game_process);
    GdiplusShutdown(g_gdiplus_token);
    OverlayLog("companion stopped normally");
    return 0;
}

} // namespace BetterEndfield::MmdOverlay

int WINAPI wWinMain(HINSTANCE instance, HINSTANCE, PWSTR, int) {
    return BetterEndfield::MmdOverlay::Run(instance);
}
