#include "inventory.h"
#include "il2cpp_api.h"

#include <cctype>
#include <cstdio>
#include <cstring>
#include <string>

namespace ecl {

namespace {

FILE* g_out = nullptr;
int g_lines = 0;
int g_maxLines = 8000;

void Out(const std::string& s) {
    if (!g_out || g_lines >= g_maxLines) return;
    fputs(s.c_str(), g_out);
    fputc('\n', g_out);
    ++g_lines;
}

bool ContainsCI(const char* hay, const char* needle) {
    if (!hay || !needle) return false;
    const size_t n = strlen(needle);
    for (const char* p = hay; *p; ++p) {
        size_t i = 0;
        while (i < n && p[i] &&
               tolower(static_cast<unsigned char>(p[i])) ==
                   tolower(static_cast<unsigned char>(needle[i])))
            ++i;
        if (i == n) return true;
    }
    return false;
}

bool StartsWithCI(const char* s, const char* prefix) {
    if (!s || !prefix) return false;
    for (size_t i = 0; prefix[i]; ++i) {
        if (!s[i]) return false;
        if (tolower(static_cast<unsigned char>(s[i])) !=
            tolower(static_cast<unsigned char>(prefix[i])))
            return false;
    }
    return true;
}

// 关键词: 相机/景深/曝光/镜头/跟随 等
const char* kKeywordsCam[] = {
    "camera", "snapshot", "photo",   "dof",      "depthoffield", "focus",
    "aperture", "exposure", "lens",  "iris",     "fstop",        "freecam",
    "rig",     "follow",   "lookat", "cinematic", "timeline",    "viewctrl",
    "viewcamera", "camctrl", "cameras"};

// v1.0.0ai: UI 档关键词 —— 目标是"能改变 HUD 渲染/显隐的着力点"。
// 依据: 只读渲染层(相机 cullingMask / 图层)才能满足 C1(不进原生相机)C2(键位不受影响)
//       C3(攻击可用); 所以关键词偏向 Canvas/图层/相机/显隐, 而不是输入或逻辑状态。
const char* kKeywordsUi[] = {
    // 名称/层级
    "hud", "panel", "canvas", "widget", "overlay", "uiroot", "uimanager",
    "interface", "layout", "screen", "dialog", "popup", "toast", "banner",
    // 相机/图层/渲染(隐藏 UI 的真正着力点)
    "uicamera", "uicam", "cullingmask", "layer", "rendermode", "sortingorder",
    "graphicraycaster", "canvasgroup", "renderqueue",
    // 显隐
    "visib", "show", "hide", "active", "toggle", "fade",
    // 拍照相关(HUD 与拍照 UI 共用一套显隐开关的可能性)
    "photo", "snapshot", "marks", "crosshair", "reticle", "aim",
    // 设置/调试开关(游戏自己的"隐藏 UI"入口)
    "settings", "option", "debug", "cheat", "gm"};

struct Profile {
    const char* const* keywords;
    int kwCount;
    int maxLines;
    bool uiNamespaceFilter;  // 额外规则: 命名空间含 .UI / 类名以 UI/Hud 开头
};

void FillProfiles(Profile p[2]) {
    p[kInvCamera] = Profile{kKeywordsCam,
                            static_cast<int>(sizeof(kKeywordsCam) /
                                             sizeof(kKeywordsCam[0])),
                            8000, false};
    p[kInvUi] = Profile{kKeywordsUi,
                        static_cast<int>(sizeof(kKeywordsUi) /
                                         sizeof(kKeywordsUi[0])),
                        30000, true};
}

bool MatchKeywords(const char* name, const Profile& p) {
    for (int i = 0; i < p.kwCount; ++i) {
        if (ContainsCI(name, p.keywords[i])) return true;
    }
    return false;
}

// 命名空间判定: ".UI" / "UI." / 结尾 ".UI" / 恰好 "UI"
bool UiNamespace(const char* ns) {
    if (!ns || !*ns) return false;
    if (strcmp(ns, "UI") == 0) return true;
    if (ContainsCI(ns, ".ui.")) return true;
    const size_t n = strlen(ns);
    if (n >= 3 && (ns[n - 3] == '.' || ns[n - 3] == '/') &&
        tolower(static_cast<unsigned char>(ns[n - 2])) == 'u' &&
        tolower(static_cast<unsigned char>(ns[n - 1])) == 'i')
        return true;
    return false;
}

bool Matches(const char* clsName, const char* ns, const Profile& p,
             const char** reason) {
    if (MatchKeywords(clsName, p)) {
        *reason = "类名关键词";
        return true;
    }
    if (MatchKeywords(ns, p)) {
        *reason = "命名空间关键词";
        return true;
    }
    if (p.uiNamespaceFilter) {
        if (UiNamespace(ns)) {
            *reason = "命名空间.UI";
            return true;
        }
        if (StartsWithCI(clsName, "UI") || StartsWithCI(clsName, "Hud")) {
            *reason = "类名前缀UI/Hud";
            return true;
        }
    }
    *reason = "";
    return false;
}

}  // namespace

void RunInventorySet(const std::string& logPath, int set) {
    const Il2CppApi& a = Il2Cpp();
    if (!a.loaded || !a.image_get_class_count || !a.image_get_class ||
        !a.class_get_name || !a.class_get_methods || !a.method_get_name ||
        !a.class_get_fields || !a.field_get_name) {
        return;
    }
    Profile prof[2];
    FillProfiles(prof);
    if (set < 0 || set > 1) set = kInvCamera;
    const Profile& p = prof[set];
    g_lines = 0;
    g_maxLines = p.maxLines;

    g_out = fopen(logPath.c_str(), "w");
    if (!g_out) return;
    {
        char head[256];
        snprintf(head, sizeof(head),
                 "=== EndfieldCamLink 符号清单 (运行时枚举) 档位=%s 行上限=%d ===",
                 set == kInvUi ? "UI" : "相机", p.maxLines);
        Out(head);
    }

    void* domain = a.domain_get();
    if (!domain) { fclose(g_out); g_out = nullptr; return; }
    void* size = nullptr;
    void** assemblies = a.domain_get_assemblies(domain, &size);
    if (!assemblies) { fclose(g_out); g_out = nullptr; return; }
    const size_t asmCount = reinterpret_cast<size_t>(size);

    for (size_t i = 0; i < asmCount && g_lines < g_maxLines; ++i) {
        void* image = a.assembly_get_image(assemblies[i]);
        if (!image) continue;
        const char* imageName = a.image_get_name(image);
        const uint32_t classCount = a.image_get_class_count(image);
        int matched = 0;
        for (uint32_t c = 0; c < classCount && g_lines < g_maxLines; ++c) {
            void* klass = a.image_get_class(image, c);
            if (!klass) continue;
            const char* clsName = a.class_get_name(klass);
            const char* ns = a.class_get_namespace ? a.class_get_namespace(klass)
                                                   : nullptr;
            const char* reason = "";
            if (!Matches(clsName, ns, p, &reason)) continue;
            ++matched;
            char head[512];
            snprintf(head, sizeof(head), "CLASS [%s] %s :: %s.%s", reason,
                     imageName ? imageName : "?", ns ? ns : "",
                     clsName ? clsName : "?");
            Out(head);
            // 方法
            void* iter = nullptr;
            void* method = nullptr;
            int mcount = 0;
            while ((method = a.class_get_methods(klass, &iter)) != nullptr &&
                   mcount < 200 && g_lines < g_maxLines) {
                const char* mn = a.method_get_name(method);
                const uint32_t argc =
                    a.method_get_param_count ? a.method_get_param_count(method) : 0;
                if (mn) {
                    char line[384];
                    snprintf(line, sizeof(line), "    M %s(%u)", mn, argc);
                    Out(line);
                }
                ++mcount;
            }
            // 字段
            void* fiter = nullptr;
            void* field = nullptr;
            int fcount = 0;
            while ((field = a.class_get_fields(klass, &fiter)) != nullptr &&
                   fcount < 200 && g_lines < g_maxLines) {
                const char* fn = a.field_get_name(field);
                if (fn) {
                    std::string line = std::string("    F ") + fn;
                    Out(line);
                }
                ++fcount;
            }
        }
        char summary[256];
        snprintf(summary, sizeof(summary), "--- %s: classes=%u, matched=%d ---",
                 imageName ? imageName : "?", classCount, matched);
        Out(summary);
    }
    Out("=== 枚举结束 ===");
    fclose(g_out);
    g_out = nullptr;
}

}  // namespace ecl
