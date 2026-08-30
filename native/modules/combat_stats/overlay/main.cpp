#include "../combat_overlay_protocol.h"
#include "character_assets.generated.h"

#include <Windows.h>
#include <objidl.h>
#include <gdiplus.h>
#include <shellapi.h>
#include <shlwapi.h>

#include <algorithm>
#include <array>
#include <chrono>
#include <cmath>
#include <cstdint>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <iomanip>
#include <memory>
#include <sstream>
#include <string>
#include <unordered_map>
#include <vector>

namespace BetterEndfield::CombatOverlay {
namespace {

using namespace Gdiplus;
using Protocol = CombatOverlayProtocol::SharedSnapshot;

constexpr wchar_t kWindowClass[] = L"BetterEndfield.CombatOverlay.Window";
constexpr int kWindowWidth = 480;
constexpr int kHeaderHeight = 96;
constexpr int kRowHeight = 76;
constexpr int kFooterHeight = 54;
constexpr int kRdpsFooterHeight = 76;
constexpr int kMinimumHeight = 176;
constexpr int kRdpsMinimumHeight = 198;
constexpr int kMaximumVisibleRows = 8;

const std::array<Color, CombatOverlayProtocol::kDamageCategoryCount> kCategoryColors{
    Color(255, 211, 216, 225), // Basic attack
    Color(255, 67, 201, 255),  // Skill
    Color(255, 255, 103, 65),  // Ultimate
    Color(255, 170, 130, 255), // Combo
    Color(255, 91, 214, 139),  // Passive
    Color(255, 143, 152, 170), // Other
};

constexpr std::array<const wchar_t*, CombatOverlayProtocol::kDamageCategoryCount>
    kCategoryNamesZh{L"普攻", L"战技", L"终结技", L"连携", L"被动", L"其他"};
constexpr std::array<const wchar_t*, CombatOverlayProtocol::kDamageCategoryCount>
    kCategoryNamesEn{L"Basic", L"Skill", L"Ultimate", L"Combo", L"Passive", L"Other"};

const std::array<Color, 9> kRdpsColors{
    Color(255, 211, 216, 225), // Direct damage
    Color(255, 255, 206, 82),  // Attack
    Color(255, 67, 201, 255),  // Damage increase
    Color(255, 255, 145, 72),  // Amplification
    Color(255, 255, 122, 103), // Fragile
    Color(255, 255, 79, 130),  // Vulnerability taken
    Color(255, 87, 217, 155),  // Resistance/defense reduction
    Color(255, 84, 179, 255),  // Arts strength
    Color(255, 143, 152, 170), // Other
};

constexpr std::array<const wchar_t*, 9> kRdpsNamesZh{
    L"直伤", L"攻击力", L"增伤", L"增幅", L"脆弱", L"承伤易伤",
    L"减防/减抗", L"法术强度", L"其他"
};
constexpr std::array<const wchar_t*, 9> kRdpsNamesEn{
    L"Direct", L"ATK", L"DMG Bonus", L"Amplify", L"Fragile", L"Vulnerability",
    L"DEF/RES Down", L"Arts Power", L"Other"
};

HINSTANCE g_instance = nullptr;
HWND g_window = nullptr;
DWORD g_game_pid = 0;
HANDLE g_game_process = nullptr;
HANDLE g_mapping = nullptr;
const Protocol* g_shared = nullptr;
Protocol g_snapshot{};
bool g_demo = false;
bool g_dragging = false;
POINT g_drag_offset{};
POINT g_window_position{};
int g_window_height = kMinimumHeight;
int g_offset_x = 0;
int g_offset_y = 84;
bool g_has_saved_position = false;
HWND g_game_window = nullptr;
HWND g_owned_game_window = nullptr;
std::filesystem::path g_settings_path;
std::filesystem::path g_log_path;
ULONG_PTR g_gdiplus_token = 0;
std::unordered_map<std::string, std::unique_ptr<Bitmap>> g_avatar_cache;
ULONGLONG g_last_diagnostic_tick = 0;
LONG g_last_snapshot_before = 0;
LONG g_last_snapshot_after = 0;
uint32_t g_last_snapshot_magic = 0;
uint32_t g_last_snapshot_version = 0;
uint32_t g_last_snapshot_size = 0;
bool g_snapshot_read_succeeded = false;
bool g_render_succeeded = false;
int g_last_show_state = -1;
bool g_game_was_foreground = false;

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
    if (g_log_path.empty()) g_log_path = DataDirectory() / L"combat-overlay.log";
    SYSTEMTIME now{};
    GetLocalTime(&now);
    char prefix[64]{};
    std::snprintf(prefix, sizeof(prefix), "[%02u:%02u:%02u.%03u] ",
        now.wHour, now.wMinute, now.wSecond, now.wMilliseconds);
    std::ofstream output(g_log_path, std::ios::app | std::ios::binary);
    if (!output) return;
    output << prefix << message << "\r\n";
}

std::string HexHandle(HWND window) {
    std::ostringstream output;
    output << "0x" << std::hex << std::uppercase
        << reinterpret_cast<uintptr_t>(window);
    return output.str();
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
    static int cached_lang = -1;
    static ULONGLONG last_check = 0;
    const ULONGLONG now = GetTickCount64();
    if (cached_lang != -1 && (now - last_check < 3000)) {
        return cached_lang == 1;
    }
    last_check = now;

    std::filesystem::path ini_path = DataDirectory() / L"BetterEndfield.ini";
    wchar_t buffer[64]{};
    GetPrivateProfileStringW(L"Launcher", L"Language", L"", buffer,
        static_cast<DWORD>(std::size(buffer)), ini_path.c_str());
    if (!buffer[0]) {
        GetPrivateProfileStringW(L"CombatStats", L"Language", L"", buffer,
            static_cast<DWORD>(std::size(buffer)), ini_path.c_str());
    }

    if (_wcsicmp(buffer, L"en_US") == 0 || _wcsicmp(buffer, L"en-US") == 0 || _wcsicmp(buffer, L"en") == 0 || _wcsicmp(buffer, L"English") == 0) {
        cached_lang = 1;
    } else if (_wcsicmp(buffer, L"zh_CN") == 0 || _wcsicmp(buffer, L"zh-CN") == 0 || _wcsicmp(buffer, L"zh") == 0 || _wcsicmp(buffer, L"Chinese") == 0) {
        cached_lang = 0;
    } else {
        LANGID langId = GetUserDefaultUILanguage();
        cached_lang = (PRIMARYLANGID(langId) == LANG_CHINESE) ? 0 : 1;
    }
    return cached_lang == 1;
}

std::wstring FormatValue(double value) {
    const double absolute = std::abs(value);
    const bool is_en = IsEnglish();

    if (is_en) {
        int exponent = 0;
        double divisor = 1.0;
        std::wstring suffix = L"";
        if (absolute >= 1.0e12) {
            divisor = 1.0e12;
            suffix = L"T";
            exponent = 12;
        } else if (absolute >= 1.0e9) {
            divisor = 1.0e9;
            suffix = L"B";
            exponent = 9;
        } else if (absolute >= 1.0e6) {
            divisor = 1.0e6;
            suffix = L"M";
            exponent = 6;
        } else if (absolute >= 1.0e3) {
            divisor = 1.0e3;
            suffix = L"K";
            exponent = 3;
        }

        const int decimals = exponent ? 2 : 0;
        std::wostringstream output;
        output << std::fixed << std::setprecision(decimals) << value / divisor;
        std::wstring result = output.str();
        if (decimals) {
            while (!result.empty() && result.back() == L'0') result.pop_back();
            if (!result.empty() && result.back() == L'.') result.pop_back();
        }
        return result + suffix;
    }

    static constexpr std::array<const wchar_t*, 12> kDecimalUnits{
        L"万", L"×10万", L"×100万", L"×1000万",
        L"亿", L"×10亿", L"×100亿", L"×1000亿",
        L"万亿", L"×10万亿", L"×100万亿", L"×1000万亿",
    };
    int exponent = absolute >= 1.0e4
        ? std::max(4, static_cast<int>(std::floor(std::log10(absolute))))
        : 0;
    const double divisor = exponent ? std::pow(10.0, exponent) : 1.0;
    const std::wstring suffix = !exponent
        ? L""
        : exponent <= 15
            ? kDecimalUnits[static_cast<size_t>(exponent - 4)]
            : L"×10^" + std::to_wstring(exponent);
    const int decimals = exponent ? 2 : 0;
    std::wostringstream output;
    output << std::fixed << std::setprecision(decimals) << value / divisor;
    std::wstring result = output.str();
    if (decimals) {
        while (!result.empty() && result.back() == L'0') result.pop_back();
        if (!result.empty() && result.back() == L'.') result.pop_back();
    }
    return result + suffix;
}

std::wstring FormatDuration(double seconds) {
    const uint64_t total = static_cast<uint64_t>(std::max(0.0, seconds));
    std::wostringstream output;
    output << std::setfill(L'0') << std::setw(2) << total / 60 << L":"
        << std::setw(2) << total % 60;
    return output.str();
}

void AddRoundedRect(GraphicsPath& path, const RectF& rect, float radius) {
    const float diameter = radius * 2.0f;
    path.AddArc(rect.X, rect.Y, diameter, diameter, 180.0f, 90.0f);
    path.AddArc(rect.GetRight() - diameter, rect.Y, diameter, diameter, 270.0f, 90.0f);
    path.AddArc(rect.GetRight() - diameter, rect.GetBottom() - diameter,
        diameter, diameter, 0.0f, 90.0f);
    path.AddArc(rect.X, rect.GetBottom() - diameter, diameter, diameter, 90.0f, 90.0f);
    path.CloseFigure();
}

const CharacterAsset* FindCharacter(std::string_view id) {
    if (id == "chr_0002_endminm" || id == "chr_0003_endminf")
        id = "chr_9000_endmin";
    for (const auto& asset : kCharacterAssets) {
        if (id == asset.id) return &asset;
    }
    return nullptr;
}

std::unique_ptr<Bitmap> LoadPngResource(int resource_id) {
    const HRSRC resource = FindResourceW(g_instance, MAKEINTRESOURCEW(resource_id), L"PNG");
    if (!resource) return {};
    const HGLOBAL loaded = LoadResource(g_instance, resource);
    const DWORD size = SizeofResource(g_instance, resource);
    const void* bytes = LockResource(loaded);
    if (!bytes || !size) return {};
    IStream* stream = SHCreateMemStream(static_cast<const BYTE*>(bytes), size);
    if (!stream) return {};
    std::unique_ptr<Bitmap> source(Bitmap::FromStream(stream));
    stream->Release();
    if (!source || source->GetLastStatus() != Ok) return {};
    Bitmap* clone = source->Clone(0, 0, source->GetWidth(), source->GetHeight(),
        PixelFormat32bppPARGB);
    return std::unique_ptr<Bitmap>(clone);
}

Bitmap* AvatarFor(std::string_view id) {
    const std::string key(id);
    if (const auto found = g_avatar_cache.find(key); found != g_avatar_cache.end())
        return found->second.get();
    const CharacterAsset* asset = FindCharacter(id);
    if (!asset) return nullptr;
    auto image = LoadPngResource(asset->resource_id);
    Bitmap* result = image.get();
    g_avatar_cache.emplace(key, std::move(image));
    return result;
}

std::wstring DisplayName(std::string_view id) {
    const bool is_en = IsEnglish();
    if (const CharacterAsset* asset = FindCharacter(id)) {
        return (is_en && asset->name_en && asset->name_en[0]) ? asset->name_en : asset->name;
    }
    if (id == "<unknown>") return is_en ? L"Unknown" : L"未知来源";
    return Utf8ToWide(id);
}

void DrawText(Graphics& graphics, const std::wstring& text, const Font& font,
    const Color& color, const RectF& rect, StringAlignment alignment = StringAlignmentNear) {
    SolidBrush brush(color);
    StringFormat format;
    format.SetAlignment(alignment);
    format.SetLineAlignment(StringAlignmentCenter);
    format.SetTrimming(StringTrimmingEllipsisCharacter);
    format.SetFormatFlags(StringFormatFlagsNoWrap);
    graphics.DrawString(text.c_str(), -1, &font, rect, &format, &brush);
}

void DrawAvatar(Graphics& graphics, const RectF& rect, std::string_view id) {
    GraphicsPath clip;
    clip.AddEllipse(rect);
    const GraphicsState state = graphics.Save();
    graphics.SetClip(&clip);
    if (Bitmap* avatar = AvatarFor(id)) {
        graphics.DrawImage(avatar, rect);
    } else {
        SolidBrush fallback(Color(255, 70, 77, 91));
        graphics.FillEllipse(&fallback, rect);
        Font font(L"Microsoft YaHei UI", 17.0f, FontStyleBold, UnitPixel);
        const std::wstring name = DisplayName(id);
        DrawText(graphics, name.empty() ? L"?" : name.substr(0, 1), font,
            Color(255, 235, 238, 244), rect, StringAlignmentCenter);
    }
    graphics.Restore(state);
    Pen border(Color(170, 255, 255, 255), 1.2f);
    graphics.DrawEllipse(&border, rect);
}

void FillDemoSnapshot() {
    std::memset(&g_snapshot, 0, sizeof(g_snapshot));
    g_snapshot.magic = CombatOverlayProtocol::kMagic;
    g_snapshot.version = CombatOverlayProtocol::kVersion;
    g_snapshot.structure_size = sizeof(g_snapshot);
    g_snapshot.overlay_enabled = 1;
    g_snapshot.overlay_visible = 1;
    g_snapshot.session_active = 1;
    g_snapshot.duration_seconds = 83.0;
    struct DemoRow { const char* id; double total; int type; };
    constexpr DemoRow rows[]{
        {"chr_0030_zhuangfy", 59704.8, 1},
        {"chr_0032_lizhiyan", 37728.8, 2},
        {"chr_0007_ikut", 26432.9, 3},
        {"chr_0004_pelica", 15133.4, 4},
    };
    g_snapshot.character_count = static_cast<uint32_t>(std::size(rows));
    for (size_t index = 0; index < std::size(rows); ++index) {
        auto& destination = g_snapshot.characters[index];
        std::snprintf(destination.id, sizeof(destination.id), "%s", rows[index].id);
        destination.total_damage = rows[index].total;
        destination.dps = rows[index].total / g_snapshot.duration_seconds;
        destination.damage_by_category[rows[index].type] = rows[index].total * 0.82;
        destination.damage_by_category[0] = rows[index].total * 0.18;
        destination.hits = 12 + index * 7;
        destination.critical_hits = 2 + index;
        g_snapshot.total_damage += rows[index].total;
        g_snapshot.hit_count += destination.hits;
        g_snapshot.critical_count += destination.critical_hits;
    }
    g_snapshot.dps = g_snapshot.total_damage / g_snapshot.duration_seconds;
    g_snapshot.rdps = g_snapshot.dps;
}

bool ReadSharedSnapshot() {
    if (g_demo) {
        FillDemoSnapshot();
        g_snapshot_read_succeeded = true;
        return true;
    }
    if (!g_shared) {
        g_snapshot_read_succeeded = false;
        return false;
    }
    for (int attempt = 0; attempt < 4; ++attempt) {
        const LONG before = g_shared->sequence;
        g_last_snapshot_before = before;
        if (before & 1) {
            YieldProcessor();
            continue;
        }
        MemoryBarrier();
        std::memcpy(&g_snapshot, g_shared, sizeof(g_snapshot));
        MemoryBarrier();
        const LONG after = g_shared->sequence;
        g_last_snapshot_after = after;
        g_last_snapshot_magic = g_snapshot.magic;
        g_last_snapshot_version = g_snapshot.version;
        g_last_snapshot_size = g_snapshot.structure_size;
        if (before == after && !(after & 1) &&
            g_snapshot.magic == CombatOverlayProtocol::kMagic &&
            g_snapshot.version == CombatOverlayProtocol::kVersion &&
            g_snapshot.structure_size == sizeof(g_snapshot)) {
            g_snapshot_read_succeeded = true;
            return true;
        }
        YieldProcessor();
    }
    g_snapshot_read_succeeded = false;
    return false;
}

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
    const uint64_t area = static_cast<uint64_t>(std::max(0L, client.right - client.left)) *
        static_cast<uint64_t>(std::max(0L, client.bottom - client.top));
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

std::filesystem::path SettingsPath() {
    return DataDirectory() / L"combat-overlay.ini";
}

void LoadPosition() {
    g_settings_path = SettingsPath();
    wchar_t buffer[64]{};
    GetPrivateProfileStringW(L"Position", L"X", L"", buffer,
        static_cast<DWORD>(std::size(buffer)), g_settings_path.c_str());
    if (buffer[0]) {
        g_offset_x = _wtoi(buffer);
        GetPrivateProfileStringW(L"Position", L"Y", L"84", buffer,
            static_cast<DWORD>(std::size(buffer)), g_settings_path.c_str());
        g_offset_y = _wtoi(buffer);
        g_has_saved_position = true;
    }
}

void SavePosition() {
    const std::wstring x = std::to_wstring(g_offset_x);
    const std::wstring y = std::to_wstring(g_offset_y);
    WritePrivateProfileStringW(L"Position", L"X", x.c_str(), g_settings_path.c_str());
    WritePrivateProfileStringW(L"Position", L"Y", y.c_str(), g_settings_path.c_str());
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
    if (IsWindow(foreground) && IsWindowVisible(foreground)) {
        g_game_window = foreground;
    }
}

bool BindOverlayToGameWindow() {
    if (g_demo) return true;
    if (!g_window || !g_game_window || !IsWindow(g_game_window)) return false;
    if (g_owned_game_window == g_game_window) return true;

    SetLastError(ERROR_SUCCESS);
    const LONG_PTR previous = SetWindowLongPtrW(g_window, GWLP_HWNDPARENT,
        reinterpret_cast<LONG_PTR>(g_game_window));
    const DWORD error = GetLastError();
    if (!previous && error != ERROR_SUCCESS) {
        OverlayLog("owner binding failed gameWindow=" + HexHandle(g_game_window) +
            " error=" + std::to_string(error));
        return false;
    }
    g_owned_game_window = g_game_window;
    OverlayLog("owner binding succeeded gameWindow=" + HexHandle(g_game_window));
    return true;
}

void UpdatePositionAndDrag() {
    RECT game{};
    if (!g_demo && !GameClientRect(game)) return;
    if (g_demo) {
        game = {0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN)};
    }
    if (!g_has_saved_position) {
        g_offset_x = std::max(16L, game.right - game.left - kWindowWidth - 24L);
        g_offset_y = 84;
        g_has_saved_position = true;
    }
    const bool drag_down = (GetAsyncKeyState(VK_CONTROL) & 0x8000) != 0 &&
        (GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0;
    POINT cursor{};
    GetCursorPos(&cursor);
    RECT window_rect{};
    GetWindowRect(g_window, &window_rect);
    if (!g_dragging && drag_down && PtInRect(&window_rect, cursor)) {
        g_dragging = true;
        g_drag_offset = {cursor.x - window_rect.left, cursor.y - window_rect.top};
    } else if (g_dragging && !drag_down) {
        g_dragging = false;
        SavePosition();
    }
    if (g_dragging) {
        g_offset_x = cursor.x - g_drag_offset.x - game.left;
        g_offset_y = cursor.y - g_drag_offset.y - game.top;
    }
    g_offset_x = std::clamp(g_offset_x, 0,
        static_cast<int>(std::max(0L, game.right - game.left - kWindowWidth)));
    g_offset_y = std::clamp(g_offset_y, 0,
        static_cast<int>(std::max(0L, game.bottom - game.top - g_window_height)));
    g_window_position = {game.left + g_offset_x, game.top + g_offset_y};
}

void Render() {
    const bool rdps_mode = g_snapshot.metric_mode == 1;
    const uint32_t active_count = g_snapshot.active_category_count;
    const size_t segment_count = (active_count > 0 && active_count <= CombatOverlayProtocol::kDisplaySegmentCount)
        ? active_count
        : (rdps_mode ? CombatOverlayProtocol::kRdpsContributionCount : CombatOverlayProtocol::kDamageCategoryCount);
    const auto segment_color = [rdps_mode](size_t index) -> Color {
        if (index < CombatOverlayProtocol::kDisplaySegmentCount &&
            g_snapshot.categories[index].color_argb != 0) {
            return Color(g_snapshot.categories[index].color_argb);
        }
        if (rdps_mode) {
            return index < kRdpsColors.size() ? kRdpsColors[index] : Color(255, 143, 152, 170);
        }
        return index < kCategoryColors.size() ? kCategoryColors[index] : Color(255, 143, 152, 170);
    };
    const bool is_en = IsEnglish();
    const auto segment_name = [rdps_mode, is_en](size_t index) -> std::wstring {
        if (rdps_mode) {
            if (index < kRdpsNamesZh.size()) {
                return is_en ? kRdpsNamesEn[index] : kRdpsNamesZh[index];
            }
        } else {
            if (index < kCategoryNamesZh.size()) {
                return is_en ? kCategoryNamesEn[index] : kCategoryNamesZh[index];
            }
        }
        if (index < CombatOverlayProtocol::kDisplaySegmentCount &&
            g_snapshot.categories[index].name[0] != '\0') {
            const std::string utf8_name(g_snapshot.categories[index].name,
                strnlen_s(g_snapshot.categories[index].name, sizeof(g_snapshot.categories[index].name)));
            if (is_en) {
                if (utf8_name == "直伤" || utf8_name == "direct" || utf8_name == "direct_damage") return L"Direct";
                if (utf8_name == "攻击力" || utf8_name == "attack") return L"ATK";
                if (utf8_name == "增伤" || utf8_name == "damage" || utf8_name == "damage_boost") return L"DMG Bonus";
                if (utf8_name == "增幅" || utf8_name == "amplification") return L"Amplify";
                if (utf8_name == "脆弱" || utf8_name == "vulnerability") return L"Fragile";
                if (utf8_name == "承伤易伤" || utf8_name == "vulnerability_taken" || utf8_name == "vuln_taken") return L"Vulnerability";
                if (utf8_name == "减防/减抗" || utf8_name == "defense" || utf8_name == "resistance" || utf8_name == "def_res") return L"DEF/RES Down";
                if (utf8_name == "法术强度" || utf8_name == "spell_intensity" || utf8_name == "intensity") return L"Arts Power";
                if (utf8_name == "其他" || utf8_name == "other") return L"Other";
                if (utf8_name == "普攻" || utf8_name == "basic") return L"Basic";
                if (utf8_name == "战技" || utf8_name == "skill") return L"Skill";
                if (utf8_name == "终结技" || utf8_name == "ultimate") return L"Ultimate";
                if (utf8_name == "连携" || utf8_name == "combo") return L"Combo";
                if (utf8_name == "被动" || utf8_name == "passive") return L"Passive";
            }
            return Utf8ToWide(utf8_name);
        }
        return is_en ? L"Other" : L"其他";
    };
    const uint32_t row_count = std::min<uint32_t>(g_snapshot.character_count,
        kMaximumVisibleRows);
    const size_t legend_columns = segment_count <= 4 ? (segment_count > 0 ? segment_count : 1) : (segment_count <= 8 ? 4 : 5);
    const size_t legend_rows = (segment_count + legend_columns - 1) / legend_columns;
    const int footer_height = static_cast<int>(32 + legend_rows * 22);
    const int minimum_height = static_cast<int>(kHeaderHeight + footer_height + 26);
    g_window_height = row_count
        ? kHeaderHeight + static_cast<int>(row_count) * kRowHeight + footer_height
        : minimum_height;
    UpdatePositionAndDrag();

    BITMAPINFO bitmap_info{};
    bitmap_info.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
    bitmap_info.bmiHeader.biWidth = kWindowWidth;
    bitmap_info.bmiHeader.biHeight = -g_window_height;
    bitmap_info.bmiHeader.biPlanes = 1;
    bitmap_info.bmiHeader.biBitCount = 32;
    bitmap_info.bmiHeader.biCompression = BI_RGB;
    void* bits = nullptr;
    HDC screen = GetDC(nullptr);
    HDC memory = CreateCompatibleDC(screen);
    HBITMAP dib = CreateDIBSection(screen, &bitmap_info, DIB_RGB_COLORS, &bits, nullptr, 0);
    HGDIOBJ old_bitmap = SelectObject(memory, dib);
    std::memset(bits, 0, static_cast<size_t>(kWindowWidth) * g_window_height * 4);
    Bitmap canvas(kWindowWidth, g_window_height, kWindowWidth * 4,
        PixelFormat32bppPARGB, static_cast<BYTE*>(bits));
    Graphics graphics(&canvas);
    graphics.SetSmoothingMode(SmoothingModeAntiAlias);
    graphics.SetTextRenderingHint(TextRenderingHintClearTypeGridFit);
    graphics.SetInterpolationMode(InterpolationModeHighQualityBicubic);

    GraphicsPath background;
    AddRoundedRect(background, RectF(0.5f, 0.5f,
        kWindowWidth - 1.0f, g_window_height - 1.0f), 16.0f);
    SolidBrush background_brush(Color(224, 17, 20, 27));
    graphics.FillPath(&background_brush, &background);
    Pen border(Color(90, 255, 255, 255), 1.0f);
    graphics.DrawPath(&border, &background);
    SolidBrush accent(Color(255, 67, 201, 255));
    graphics.FillRectangle(&accent, 20.0f, 18.0f, 4.0f, 24.0f);

    Font title_font(L"Microsoft YaHei UI", 17.0f, FontStyleBold, UnitPixel);
    Font label_font(L"Microsoft YaHei UI", 11.0f, FontStyleRegular, UnitPixel);
    Font value_font(L"Microsoft YaHei UI", 15.0f, FontStyleBold, UnitPixel);
    Font row_name_font(L"Microsoft YaHei UI", 14.0f, FontStyleBold, UnitPixel);
    DrawText(graphics, is_en ? L"Combat Stats" : L"战斗数据", title_font,
        Color(255, 244, 247, 252), RectF(34, 12, 130, 36));

    const wchar_t* status = g_snapshot.session_active
        ? (is_en ? L"Recording" : L"记录中")
        : (is_en ? L"Paused" : L"已停止");
    const Color status_color = g_snapshot.session_active
        ? Color(255, 87, 224, 154) : Color(255, 163, 171, 186);
    SolidBrush status_background(Color(52, status_color.GetR(), status_color.GetG(),
        status_color.GetB()));
    GraphicsPath status_path;
    AddRoundedRect(status_path, RectF(168, 17, is_en ? 78.0f : 62.0f, 24.0f), 12.0f);
    graphics.FillPath(&status_background, &status_path);
    DrawText(graphics, status, label_font, status_color,
        RectF(168, 16, is_en ? 78.0f : 62.0f, 25.0f), StringAlignmentCenter);

    const std::wstring time = FormatDuration(g_snapshot.duration_seconds);
    DrawText(graphics, time, label_font, Color(220, 190, 198, 212),
        RectF(400, 13, 54, 30), StringAlignmentFar);
    DrawText(graphics, is_en ? L"Total DMG" : L"总伤害", label_font,
        Color(190, 174, 183, 199), RectF(22, 52, 65, 28));
    DrawText(graphics, FormatValue(g_snapshot.total_damage), value_font,
        Color(255, 248, 250, 253), RectF(88, 49, 116, 34));
    const wchar_t* metric_label = g_snapshot.metric_mode == 1 ? L"rDPS" : L"DPS";
    DrawText(graphics, metric_label, label_font, Color(190, 174, 183, 199),
        RectF(218, 52, 46, 28));
    DrawText(graphics, FormatValue(g_snapshot.dps), value_font,
        Color(255, 248, 250, 253), RectF(264, 49, 112, 34));
    DrawText(graphics, std::to_wstring(g_snapshot.hit_count) + (is_en ? L" Hits" : L" 次"),
        label_font, Color(210, 186, 195, 211), RectF(386, 51, 68, 30),
        StringAlignmentFar);

    Pen divider(Color(40, 255, 255, 255), 1.0f);
    graphics.DrawLine(&divider, 20.0f, 90.0f, 460.0f, 90.0f);
    if (!row_count) {
        DrawText(graphics, is_en ? L"Press F11 to start recording" : L"按 F11 开始记录战斗数据",
            value_font, Color(210, 186, 195, 211), RectF(24, 94, 432, 48), StringAlignmentCenter);
    }

    double maximum_damage = 1.0;
    for (uint32_t index = 0; index < row_count; ++index)
        maximum_damage = std::max(maximum_damage, g_snapshot.characters[index].total_damage);
    for (uint32_t index = 0; index < row_count; ++index) {
        const auto& row = g_snapshot.characters[index];
        const float top = static_cast<float>(kHeaderHeight + index * kRowHeight);
        const std::string id(row.id, strnlen_s(row.id, sizeof(row.id)));
        DrawAvatar(graphics, RectF(22, top + 10, 54, 54), id);
        DrawText(graphics, DisplayName(id), row_name_font, Color(255, 241, 244, 249),
            RectF(88, top + 5, 210, 27));
        DrawText(graphics, FormatValue(row.total_damage), value_font,
            Color(255, 246, 248, 252), RectF(324, top + 4, 132, 29),
            StringAlignmentFar);
        const std::wstring hit_info = is_en
            ? (std::to_wstring(row.hits) + L" Hits · " + std::to_wstring(row.critical_hits) + L" Crits")
            : (std::to_wstring(row.hits) + L" 命中 · " + std::to_wstring(row.critical_hits) + L" 暴击");
        DrawText(graphics, hit_info, label_font,
            Color(175, 164, 174, 191), RectF(88, top + 30, 210, 21));

        const RectF bar_rect(88, top + 55, 368, 8);
        GraphicsPath bar_path;
        AddRoundedRect(bar_path, bar_rect, 4.0f);
        SolidBrush bar_background(Color(75, 107, 115, 132));
        graphics.FillPath(&bar_background, &bar_path);
        const float filled_width = static_cast<float>(bar_rect.Width *
            std::clamp(row.total_damage / maximum_damage, 0.0, 1.0));
        const GraphicsState state = graphics.Save();
        graphics.SetClip(&bar_path);
        float x = bar_rect.X;
        double typed_total = 0.0;
        for (size_t type = 0; type < segment_count; ++type)
            typed_total += std::max(0.0, row.damage_by_category[type]);
        if (typed_total <= 0.0) typed_total = row.total_damage;
        for (size_t type = 0; type < segment_count; ++type) {
            const double type_damage = std::max(0.0, row.damage_by_category[type]);
            if (type_damage <= 0.0 || typed_total <= 0.0) continue;
            const float width = filled_width * static_cast<float>(type_damage / typed_total);
            SolidBrush type_brush(segment_color(type));
            graphics.FillRectangle(&type_brush, x, bar_rect.Y, width + 0.5f, bar_rect.Height);
            x += width;
        }
        if (x == bar_rect.X && filled_width > 0.0f) {
            SolidBrush neutral(Color(255, 148, 158, 177));
            graphics.FillRectangle(&neutral, bar_rect.X, bar_rect.Y, filled_width, bar_rect.Height);
        }
        graphics.Restore(state);
    }

    const float legend_step = 440.0f / static_cast<float>(legend_columns);
    const float legend_top = static_cast<float>(g_window_height - footer_height + 9);
    for (size_t category = 0; category < segment_count; ++category) {
        const size_t row = category / legend_columns;
        const size_t column = category % legend_columns;
        const float x = 20.0f + static_cast<float>(column) * legend_step;
        const float y = legend_top + static_cast<float>(row) * 22.0f;
        SolidBrush dot(segment_color(category));
        graphics.FillEllipse(&dot, x, y + 7.0f, 8.0f, 8.0f);
        DrawText(graphics, segment_name(category), label_font,
            Color(185, 178, 187, 203),
            RectF(x + 11.0f, y, legend_step - 11.0f, 22.0f));
    }
    DrawText(graphics, is_en ? L"Ctrl + Left Drag to move  ·  F12 to toggle" : L"Ctrl + 鼠标左键拖动位置  ·  F12 显示/隐藏",
        label_font, Color(145, 153, 163, 181),
        RectF(20.0f, static_cast<float>(g_window_height - 27), 440.0f, 21.0f),
        StringAlignmentCenter);

    POINT source{};
    SIZE size{kWindowWidth, g_window_height};
    BLENDFUNCTION blend{AC_SRC_OVER, 0, 255, AC_SRC_ALPHA};
    SetLastError(ERROR_SUCCESS);
    const BOOL layered = UpdateLayeredWindow(g_window, screen, &g_window_position, &size,
        memory, &source, 0, &blend, ULW_ALPHA);
    if (!layered) {
        OverlayLog("UpdateLayeredWindow failed error=" + std::to_string(GetLastError()) +
            " position=" + std::to_string(g_window_position.x) + "," +
            std::to_string(g_window_position.y) + " size=" +
            std::to_string(kWindowWidth) + "x" + std::to_string(g_window_height));
    } else if (!g_render_succeeded) {
        g_render_succeeded = true;
        OverlayLog("first frame rendered position=" +
            std::to_string(g_window_position.x) + "," +
            std::to_string(g_window_position.y) + " size=" +
            std::to_string(kWindowWidth) + "x" + std::to_string(g_window_height));
    }
    SelectObject(memory, old_bitmap);
    DeleteObject(dib);
    DeleteDC(memory);
    ReleaseDC(nullptr, screen);
}

bool ShouldShow() {
    if (g_demo) return true;
    if (!g_snapshot.overlay_enabled || !g_snapshot.overlay_visible || !g_game_window ||
        !IsWindowVisible(g_game_window) || IsIconic(g_game_window)) return false;
    if (g_owned_game_window == g_game_window) return true;
    return WindowBelongsToGame(GetForegroundWindow());
}

void Tick() {
    if (!g_demo && g_game_process && WaitForSingleObject(g_game_process, 0) != WAIT_TIMEOUT) {
        OverlayLog("game process exited; companion stopping");
        PostQuitMessage(0);
        return;
    }
    if (!ReadSharedSnapshot()) {
        const ULONGLONG now = GetTickCount64();
        if (now - g_last_diagnostic_tick >= 1000) {
            g_last_diagnostic_tick = now;
            OverlayLog("snapshot rejected before=" + std::to_string(g_last_snapshot_before) +
                " after=" + std::to_string(g_last_snapshot_after) +
                " magic=0x" + [&] {
                    std::ostringstream value;
                    value << std::hex << std::uppercase << g_last_snapshot_magic;
                    return value.str();
                }() + " version=" + std::to_string(g_last_snapshot_version) +
                " size=" + std::to_string(g_last_snapshot_size) +
                " expectedSize=" + std::to_string(sizeof(g_snapshot)));
        }
        return;
    }
    if (g_snapshot.shutdown_requested) {
        OverlayLog("shutdown requested by module");
        PostQuitMessage(0);
        return;
    }
    // The companion can start while Unity still owns a splash/bootstrap HWND.
    // Once any Endfield window becomes foreground, promote its top-level root
    // instead of retaining the early cached handle for the whole process.
    RefreshGameWindowFromForeground();
    if (!g_demo && (!g_game_window || !IsWindow(g_game_window))) g_game_window = FindGameWindow();
    BindOverlayToGameWindow();
    const HWND foreground = GetForegroundWindow();
    const bool game_is_foreground = foreground == g_window || WindowBelongsToGame(foreground);
    const bool should_show = ShouldShow();
    const int show_state = should_show ? 1 : 0;
    const bool became_visible = show_state == 1 && g_last_show_state != 1;
    const bool game_became_foreground = game_is_foreground && !g_game_was_foreground;
    g_game_was_foreground = game_is_foreground;
    const ULONGLONG now = GetTickCount64();
    if (show_state != g_last_show_state || now - g_last_diagnostic_tick >= 1000) {
        g_last_show_state = show_state;
        g_last_diagnostic_tick = now;
        DWORD foreground_pid = 0;
        if (foreground) GetWindowThreadProcessId(foreground, &foreground_pid);
        RECT client{};
        const bool has_client = GameClientRect(client);
        OverlayLog("tick snapshot=ok seq=" + std::to_string(g_snapshot.sequence) +
            " enabled=" + std::to_string(g_snapshot.overlay_enabled) +
            " visible=" + std::to_string(g_snapshot.overlay_visible) +
            " session=" + std::to_string(g_snapshot.session_active) +
            " characters=" + std::to_string(g_snapshot.character_count) +
            " gameWindow=" + HexHandle(g_game_window) +
            " valid=" + std::to_string(g_game_window && IsWindow(g_game_window)) +
            " iconic=" + std::to_string(g_game_window && IsIconic(g_game_window)) +
            " foreground=" + HexHandle(foreground) +
            " foregroundPid=" + std::to_string(foreground_pid) +
            " expectedPid=" + std::to_string(g_game_pid) +
            " ownerBound=" + std::to_string(g_owned_game_window == g_game_window) +
            " gameForeground=" + std::to_string(game_is_foreground) +
            " client=" + (has_client
                ? std::to_string(client.right - client.left) + "x" +
                    std::to_string(client.bottom - client.top)
                : std::string("unavailable")) +
            " shouldShow=" + std::to_string(should_show));
    }
    if (!should_show) {
        ShowWindow(g_window, SW_HIDE);
        return;
    }
    ShowWindow(g_window, SW_SHOWNOACTIVATE);
    if (g_demo || game_became_foreground || (became_visible && game_is_foreground)) {
        SetLastError(ERROR_SUCCESS);
        const HWND insert_after = g_demo ? HWND_TOPMOST : HWND_TOP;
        const BOOL promoted = SetWindowPos(g_window, insert_after, 0, 0, 0, 0,
            SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
        if (!promoted) {
            OverlayLog("owner z-order promotion failed error=" +
                std::to_string(GetLastError()));
        } else {
            OverlayLog(std::string(g_demo ? "demo topmost" : "owner z-order") +
                " promotion succeeded");
        }
    }
    Render();
}

LRESULT CALLBACK WindowProc(HWND window, UINT message, WPARAM wparam, LPARAM lparam) {
    switch (message) {
    case WM_TIMER:
        Tick();
        return 0;
    case WM_NCHITTEST:
        return HTTRANSPARENT;
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
        if (argument == L"--demo") {
            g_demo = true;
        } else if (argument == L"--game-pid" && index + 1 < count) {
            g_game_pid = wcstoul(arguments[++index], nullptr, 10);
        } else if (argument == L"--mapping" && index + 1 < count) {
            mapping_name = arguments[++index];
        }
    }
    LocalFree(arguments);
    return g_demo || (g_game_pid && !mapping_name.empty());
}

} // namespace

int Run(HINSTANCE instance) {
    g_instance = instance;
    g_log_path = DataDirectory() / L"combat-overlay.log";
    OverlayLog("companion starting pid=" + std::to_string(GetCurrentProcessId()));
    SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
    std::wstring mapping_name;
    if (!ParseArguments(mapping_name)) {
        OverlayLog("argument parsing failed");
        return 2;
    }
    OverlayLog(std::string("mode=") + (g_demo ? "demo" : "game") +
        " gamePid=" + std::to_string(g_game_pid));
    if (!g_demo) {
        g_game_process = OpenProcess(SYNCHRONIZE, FALSE, g_game_pid);
        g_mapping = OpenFileMappingW(FILE_MAP_READ, FALSE, mapping_name.c_str());
        if (!g_mapping) {
            OverlayLog("OpenFileMapping failed error=" + std::to_string(GetLastError()));
            return 3;
        }
        g_shared = static_cast<const Protocol*>(MapViewOfFile(g_mapping, FILE_MAP_READ,
            0, 0, sizeof(Protocol)));
        if (!g_shared) {
            OverlayLog("MapViewOfFile failed error=" + std::to_string(GetLastError()));
            return 4;
        }
        OverlayLog("shared mapping opened expectedSize=" +
            std::to_string(sizeof(Protocol)) + " processHandle=" +
            std::to_string(g_game_process != nullptr));
    }
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
    g_window = CreateWindowExW(WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW |
            WS_EX_NOACTIVATE, kWindowClass, L"Better Endfield 战斗数据",
        WS_POPUP, 0, 0, kWindowWidth, kMinimumHeight, nullptr, nullptr, instance, nullptr);
    if (!g_window) {
        OverlayLog("CreateWindowEx failed error=" + std::to_string(GetLastError()));
        return 6;
    }
    OverlayLog("overlay window created hwnd=" + HexHandle(g_window));
    SetWindowPos(g_window, g_demo ? HWND_TOPMOST : HWND_TOP, 0, 0,
        kWindowWidth, kMinimumHeight,
        SWP_NOMOVE | SWP_NOACTIVATE | SWP_HIDEWINDOW);
    SetTimer(g_window, 1, 100, nullptr);
    Tick();

    MSG message{};
    while (GetMessageW(&message, nullptr, 0, 0) > 0) {
        TranslateMessage(&message);
        DispatchMessageW(&message);
    }
    KillTimer(g_window, 1);
    if (g_shared) UnmapViewOfFile(g_shared);
    if (g_mapping) CloseHandle(g_mapping);
    if (g_game_process) CloseHandle(g_game_process);
    g_avatar_cache.clear();
    GdiplusShutdown(g_gdiplus_token);
    OverlayLog("companion stopped normally");
    return 0;
}

} // namespace BetterEndfield::CombatOverlay

int WINAPI wWinMain(HINSTANCE instance, HINSTANCE, PWSTR, int) {
    return BetterEndfield::CombatOverlay::Run(instance);
}
