#include <Windows.h>
#include <array>
static std::array<bool, 256> media_keys{};
static bool media_foreground=true;
static SHORT MediaTestKeyState(int key) { return key >= 0 && key < 256 && media_keys[key] ? SHORT(0x8000) : 0; }
static HWND MediaTestForeground() { return reinterpret_cast<HWND>(1); }
static DWORD MediaTestWindowProcess(HWND, DWORD* pid) { if (pid) *pid = GetCurrentProcessId() + (media_foreground?0:1); return 1; }
#define GetAsyncKeyState MediaTestKeyState
#define GetForegroundWindow MediaTestForeground
#define GetWindowThreadProcessId MediaTestWindowProcess
#include "../../modules/media_screen/module.cpp"
#undef GetAsyncKeyState
#undef GetForegroundWindow
#undef GetWindowThreadProcessId
#include <fstream>
#include <iostream>
#include <memory>
#include <unordered_map>
#include "../../modules/music/bgm_pause.h"

// WGC is built by the production DLL. The host tests need no desktop capture session.
bool fixture_capture_available = false;
BetterEndfieldNext::MediaScreen::WindowCapture::FrameData fixture_frame;
std::vector<BetterEndfieldNext::MediaScreen::WindowInputEvent> fixture_input;
namespace BetterEndfieldNext::MediaScreen {
WindowCapture::WindowCapture() = default;
WindowCapture::~WindowCapture() = default;
void WindowCapture::Start(uint64_t, uint32_t, int, int, int) { if (!fixture_capture_available) throw std::runtime_error("capture fixture unavailable"); }
void WindowCapture::Stop() { fixture_frame={}; }
bool WindowCapture::Take(uint64_t serial, FrameData& out) { if (!fixture_frame.serial || serial==fixture_frame.serial) return false; out=fixture_frame; return true; }
std::string WindowCapture::Error() { return {}; }
bool WindowCapture::Control(bool) { return true; }
bool WindowCapture::Input(const WindowInputEvent& event) { fixture_input.push_back(event); return true; }
std::string WindowCapture::InputError() { return {}; }
}
using namespace BetterEndfieldNext::MediaScreen;
namespace Input = BetterEndfieldNext::Input;
namespace {
void Check(bool value, const char* message) { if (!value) throw std::runtime_error(message); }
enum Kind { Plain, Tex, RenderTex, Byte, Mat };
struct Object {
    Kind kind = Plain; bool alive = true; int width = 2, height = 2, extra = 7;
    void* texture = nullptr; void* emissive_texture = nullptr;
    Object* material = nullptr; Object* poster_helper = nullptr; std::string text;
    std::vector<Object*> items;
    Object *transform=nullptr,*parent=nullptr,*material_array=nullptr;
    Object* shader=nullptr;
    bool enabled=true;
    std::unordered_map<int32_t,float> floats;
    std::array<float,4> tint{};
    Vec3 position{};
    Bounds bounds{};
    Bounds local_bounds{{0,0,0},{.05f,.05f,0}};
    Vec3 lossy_scale{1,1,1};
};
std::vector<std::unique_ptr<Object>> objects;
std::vector<std::unique_ptr<unsigned char[]>> arrays;
std::unordered_map<uint32_t, void*> roots;
uint32_t next_root = 1;
int finds = 0, enqueue_calls = 0, dequeue_calls = 0, poster_calls = 0;
BetterEndfieldNext::Music::BgmPauseRequests bgm_requests;
int bgm_acquires = 0, bgm_releases = 0;
int fail_call = -1;
bool video_playing = false;
double video_position = 0, external_clock = 0;
int video_plays = 0;
int window_uploads=0;
int display_blits=0;
int fixture_cursor_lock=0, fixture_cursor_visible=1;
std::vector<unsigned char> uploaded_window;
BE_LocalMusicStatusV1 audio_state{};
int audio_closes = 0;
bool has_screen = true, has_renderer = true;
bool refuse_material_write = false, has_image_property = true;
Object *screen, *screen_helper, *renderer, *screen_material, *other_helper, *original_texture, *emissive_texture, *property;
std::array<unsigned char, 64> boxed{};
Object *fixture_camera=nullptr,*fixture_renderer_array=nullptr;
int receiver_surveys=0;
std::vector<std::string> survey_logs;
Object* Make(Kind kind = Plain) { auto value = std::make_unique<Object>(); value->kind = kind;
    auto* raw = value.get(); objects.push_back(std::move(value)); return raw; }
template<class T> void* Box(T value) { std::memcpy(boxed.data(), &value, sizeof(value)); return boxed.data(); }
void* BE_CALL MockInvoke(void*, const void* info, void* self, void** args, void** exception) {
    int id = int(reinterpret_cast<uintptr_t>(info)) - 1;
    if (id == fail_call) { *exception = screen; return nullptr; }
    auto* object = static_cast<Object*>(self);
    switch (id) {
    case Find: ++finds; return has_screen ? screen : nullptr;
    case GetHelper: return screen_helper;
    case Alive: return Box<uint8_t>(args[0] && static_cast<Object*>(args[0])->alive);
    case Destroy: static_cast<Object*>(args[0])->alive = false; return nullptr;
    case GetName: { auto* name = Make(); name->text = object->text; return name; }
    case MaterialShader: return object->shader;
    case MaterialCtor:
        object->texture = static_cast<Object*>(args[0])->texture;
        object->emissive_texture = static_cast<Object*>(args[0])->emissive_texture;
        object->shader = static_cast<Object*>(args[0])->shader;
        object->floats = static_cast<Object*>(args[0])->floats;
        object->tint = static_cast<Object*>(args[0])->tint;
        object->text = "media-material"; return nullptr;
    case MaterialHasProperty: return Box<uint8_t>(has_image_property && (*static_cast<int32_t*>(args[0])==42 || *static_cast<int32_t*>(args[0])>=44));
    case RendererMaterial: return object->material;
    case RendererSetMaterial: object->material = static_cast<Object*>(args[0]); return nullptr;
    case MaterialGetTexture: return *static_cast<int32_t*>(args[0]) == 42 ? object->texture : object->emissive_texture;
    case MaterialSetTexture:
        if (!refuse_material_write) {
            if (*static_cast<int32_t*>(args[0]) == 42) object->texture = args[1];
            else object->emissive_texture = args[1];
        }
        return nullptr;
    case PropertyId: {
        auto name=static_cast<Object*>(args[0])->text;
        constexpr const char* names[]{"_TintColor","_DisableVertColor","_TintColorIntensity","_ProcedureAlpha","_UseMainTexAsAlpha"};
        for (int i=0;i<5;++i) if (name==names[i]) return Box<int32_t>(44+i);
        return Box<int32_t>(name=="_MainTex"?42:43);
    }
    case DeskEnabled: return Box<uint8_t>(object->enabled);
    case DeskSetEnabled: object->enabled=*static_cast<uint8_t*>(args[0])!=0; return nullptr;
    case DeskFloat: object->floats[*static_cast<int32_t*>(args[0])]=*static_cast<float*>(args[1]); return nullptr;
    case DeskColor: std::memcpy(object->tint.data(),args[1],sizeof(float)*4); return nullptr;
    case TextureCtor: case RtCtor: object->width = *static_cast<int32_t*>(args[0]); object->height = *static_cast<int32_t*>(args[1]); return nullptr;
    case RtCreate: return Box<uint8_t>(1);
    case TextureWidth: return Box<int32_t>(object->width);
    case TextureHeight: return Box<int32_t>(object->height);
    case RtGetActive: return nullptr;
    case RtSetActive: case ClearRt: return nullptr;
    case Blit: case BlitCrop: ++display_blits; static_cast<Object*>(args[1])->texture=args[0]; return nullptr;
    case CopyTexture: static_cast<Object*>(args[7])->texture=static_cast<Object*>(args[0])->texture; return nullptr;
    case ArrayCreate: {
        auto allocation = std::make_unique<unsigned char[]>(32 + *static_cast<int32_t*>(args[1]));
        auto* data = allocation.get(); arrays.push_back(std::move(allocation)); return data;
    }
    case ImageLoad: return Box<uint8_t>(1);
    case Apply: return nullptr;
    case ControlCursorSetLock: fixture_cursor_lock=*static_cast<int32_t*>(args[0]); return nullptr;
    case ControlCursorSetVisible: fixture_cursor_visible=*static_cast<uint8_t*>(args[0]); return nullptr;
    case LoadRaw: {
        auto* pixels=*static_cast<unsigned char**>(args[0]); int size=*static_cast<int32_t*>(args[1]);
        uploaded_window.assign(pixels,pixels+size); ++window_uploads; return nullptr;
    }
    case EnumParse: return Box<int32_t>(2);
    case VideoPrepared: return Box<uint8_t>(1);
    case VideoPlay: ++video_plays; video_playing = true; return nullptr;
    case VideoPause: case VideoStop: video_playing = false; return nullptr;
    case VideoPlaying: return Box<uint8_t>(video_playing);
    case VideoReference: return nullptr;
    case VideoExternal: external_clock = *static_cast<double*>(args[0]); return nullptr;
    case VideoSetTime: video_position = *static_cast<double*>(args[0]); return nullptr;
    case VideoTime: return Box<double>(video_position);
    case VideoLength: return Box<double>(60);
    case VideoFrameIndex: return Box<int64_t>(int64_t(video_position*30));
    case VideoWidth: return Box<uint32_t>(16);
    case VideoHeight: return Box<uint32_t>(9);
    case ControlCamera: return fixture_camera;
    case ControlScreenWidth: return Box<int32_t>(200);
    case ControlScreenHeight: return Box<int32_t>(100);
    case ControlRay: return Box<Ray>({*static_cast<Vec3*>(args[0]),{0,0,1}});
    case ControlInversePoint: return Box<Vec3>(*static_cast<Vec3*>(args[0]));
    case ControlTransform: return object->transform;
    case ControlComponent: return nullptr;
    case ControlArrayLength: return Box<int32_t>(int32_t(object->items.size()));
    case ControlArrayValue: return object->items.at(*static_cast<int32_t*>(args[0]));
    case InspectFind: ++receiver_surveys; return fixture_renderer_array;
    case InspectPosition: return Box<Vec3>(object->position);
    case InspectBounds: return Box<Bounds>(object->bounds);
    case DeskLocalBounds: return Box<Bounds>(object->local_bounds);
    case DeskScale: return Box<Vec3>(object->lossy_scale);
    case InspectParent: return object->parent;
    case InspectMaterials: return object->material_array;
    case InspectInstance: return Box<int32_t>(object->extra);
    default: throw std::runtime_error("Unexpected fixture method " + std::to_string(id));
    }
}
void* BE_CALL MockField(void*, const void* info, void* self) {
    switch (reinterpret_cast<uintptr_t>(info)) {
    case 1: case 2: return has_renderer ? renderer : nullptr;
    case 5: return static_cast<Object*>(self)->poster_helper;
    default: return nullptr;
    }
}
BE_HostApiV1 MockHost() {
    BE_HostApiV1 result{}; result.abi_version = 1;
    result.log = [](void*, const char*, const char* text) { if (std::string_view(text).starts_with("Screen ")) survey_logs.emplace_back(text); };
    result.runtime_invoke = &MockInvoke;
    result.object_unbox = [](void*, void* value) { return value; };
    result.object_new = [](void*, const void* kind) -> void* { return Make(static_cast<Kind>(reinterpret_cast<uintptr_t>(kind))); };
    result.string_new = [](void*, const char* value) -> void* { auto* string = Make(); string->text = value; return string; };
    result.copy_managed_string = [](void*, const void* value, char* output, size_t size) {
        auto& text = static_cast<const Object*>(value)->text;
        if (size <= text.size()) return -1; std::memcpy(output, text.c_str(), text.size() + 1); return int(text.size());
    };
    result.gchandle_new = [](void*, void* value, int) { auto id = next_root++; roots[id] = value; return id; };
    result.gchandle_free = [](void*, uint32_t id) { roots.erase(id); };
    result.field_get_value_object = &MockField;
    result.release_module_hooks = [](void*, const char*) { return BE_Result_Ok; };
    result.resolve_class = [](void*, const char*, const char*, const char*, BE_ResolvedClassV1* out) {
        out->class_info = reinterpret_cast<void*>(1); out->type_object = property; return BE_Result_Ok;
    };
    return result;
}
bool list_contracts = false;
int getter_requests = 0, installed_hooks = 0, next_method = 1;
BE_Result BE_CALL MockResolve(void*, const BE_MethodDescriptorV1* desc, BE_ResolvedMethodV1* out) {
    if (list_contracts) {
        std::cout << "METHOD\t" << desc->assembly_name << '\t' << desc->namespace_name << '\t'
            << desc->class_name << '\t' << desc->method_name << '\t'
            << (desc->parameter_types ? desc->parameter_types : "") << '\t' << desc->return_type << '\n';
    }
    *out = {};
    // The current PC client has SetTexture, but strips this getter entirely.
    if (std::string(desc->class_name) == "MaterialPropertyBlock" && std::string(desc->method_name) == "GetTexture") {
        ++getter_requests; return BE_Result_NotFound;
    }
    out->method_info = out->method_pointer = reinterpret_cast<void*>(uintptr_t(next_method++));
    return BE_Result_Ok;
}
void InitializationContracts() {
    auto api = MockHost();
    api.resolve_method = &MockResolve;
    api.resolve_field = [](void*, const BE_FieldDescriptorV1* desc, BE_ResolvedFieldV1* out) {
        if (list_contracts) std::cout << "FIELD\t" << desc->assembly_name << '\t' << desc->namespace_name
            << '\t' << desc->class_name << '\t' << desc->field_name << '\t' << desc->field_type << '\n';
        out->field_info = reinterpret_cast<void*>(1); return BE_Result_Ok;
    };
    api.create_hook = [](void*, const char*, void* target, void*, void** original) {
        ++installed_hooks;
        *original = target;
        return BE_Result_Ok;
    };
    Check(Initialize(&api) == BE_Result_Ok && getter_requests == 0,
        "client without MaterialPropertyBlock.GetTexture failed to initialize");
    Check(installed_hooks == 46, "frame/helper/poster/control hooks and native/device/UI input shields were not installed during initialization");
    Shutdown();
    Check(!initialized && !BetterEndfieldNext_MediaScreenCommandV1(0, 0), "shutdown left startup fixture active");
}
void Setup(const BE_HostApiV1* api) {
    desk_renderer_class={};
    host = api; initialized = true; stopping = false; frame_thread = 0;
    for (int i = 0; i < Count; ++i) methods[i].resolved.method_info = reinterpret_cast<void*>(uintptr_t(i + 1));
    texture_class.class_info = reinterpret_cast<void*>(Tex);
    material_class.class_info = reinterpret_cast<void*>(Mat);
    rt_class.class_info = reinterpret_cast<void*>(RenderTex);
    top_field.field_info = reinterpret_cast<void*>(1); bottom_field.field_info = reinterpret_cast<void*>(2);
    poster_helper_field.field_info = reinterpret_cast<void*>(5);
    screen = Make(); screen_helper = Make(); renderer = Make(); screen_material = Make(); other_helper = Make(); original_texture = Make(Tex);
    renderer->material = screen_material; renderer->text = "screen-renderer"; screen_material->text = "screen-material";
    property = Make(); property->text = "_EmissiveMap";
    emissive_texture = Make(Tex); screen_material->emissive_texture = emissive_texture;
    screen_material->texture = original_texture;
    original_enqueue = [](void*, void*, void*, void*) { ++enqueue_calls; };
    original_dequeue = [](void*, void*) { ++dequeue_calls; };
    original_poster_enqueue = [](void*, void*, uint8_t, void*) { ++poster_calls; };
    original_poster_dequeue = original_poster_update = [](void*, void*) { ++poster_calls; };
    reader.Start();
}
std::string Utf8(const std::filesystem::path& path) {
    auto text = path.generic_u8string(); return std::string(reinterpret_cast<const char*>(text.data()), text.size());
}
std::vector<unsigned char> Png(uint32_t width = 1, uint32_t height = 1) {
    std::vector<unsigned char> bytes{137,80,78,71,13,10,26,10,0,0,0,13,'I','H','D','R',0,0,0,0,0,0,0,0};
    for (int i = 0; i < 4; ++i) { bytes[16 + i] = (width >> (24 - i * 8)) & 255; bytes[20 + i] = (height >> (24 - i * 8)) & 255; }
    return bytes;
}
void AwaitImage() {
    for (int i = 0; i < 500 && image_generation; ++i) { std::this_thread::sleep_for(std::chrono::milliseconds(2)); Frame(); }
    Check(!image_generation && bound, "image load did not complete");
}
void Controls(const std::filesystem::path& source) {
    std::string config = "enabled=true\nsource=image\nsource_path=" + Utf8(source) + "\n";
    Check(Configure(config.c_str()) == BE_Result_Ok, "configuration rejected");
    for (int i = 0; i < 8; ++i) Frame();
    Check(finds == 0 && !bound, "configuration must never search/bind automatically");
    has_screen = false; Check(BetterEndfieldNext_MediaScreenCommandV1(0, 0), "start command rejected"); Frame();
    Check(!bound && roots.empty() && screen_material->texture == original_texture, "failed target must preserve screen and roots");
    has_screen = true; has_renderer = false; BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    Check(!bound && roots.empty(), "partial binding leaked GC roots");
    has_renderer = true; fail_call = RendererMaterial; BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); fail_call = -1;
    Check(!bound && roots.empty() && screen_material->texture == original_texture, "failed material lookup changed screen or retained roots");
    has_image_property = false; BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    Check(!bound && roots.empty() && renderer->material == screen_material && status_text.find("no image-poster texture property") != std::string::npos,
        "a material without the image-poster property was accepted");
    has_image_property = true;
    has_renderer = true; fail_call = RtCtor; BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); fail_call = -1;
    Check(!bound && roots.empty() && screen_material->texture == original_texture, "source allocation failure did not roll back");
    refuse_material_write = true; BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    Check(!bound && roots.empty() && screen_material->texture == original_texture && status_text.find("did not retain") != std::string::npos,
        "a material setter that did not change the texture was reported as playing");
    refuse_material_write = false;
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); AwaitImage();
    Check(renderer->material != screen_material && renderer->material->texture == canvas_texture && screen_material->texture == original_texture &&
        !renderer->texture && renderer->extra == 7 && receivers.size() == 1,
        "screen must display an owned material while preserving the original material and renderer properties");
    Check(texture_property == 42 && String(texture_property_name) == "_MainTex" &&
        renderer->material->emissive_texture == emissive_texture && screen_material->emissive_texture == emissive_texture,
        "image posters must replace _MainTex while preserving the separate emissive texture");
    Enqueue(screen_helper, nullptr, nullptr, nullptr); Dequeue(screen_helper, nullptr);
    Enqueue(other_helper, nullptr, nullptr, nullptr); Dequeue(other_helper, nullptr);
    std::thread other_thread([] { Enqueue(screen_helper, nullptr, nullptr, nullptr); Dequeue(screen_helper, nullptr); }); other_thread.join();
    Check(enqueue_calls == 1 && dequeue_calls == 1, "takeover must filter one helper only");
    auto* owner = Make(); owner->poster_helper = screen_helper;
    auto* unrelated_owner = Make(); unrelated_owner->poster_helper = other_helper;
    PosterEnqueueCall(owner, nullptr, 0, nullptr); PosterDequeueCall(owner, nullptr); PosterUpdateCall(owner, nullptr);
    Check(poster_calls == 0, "the bound poster controller continued cycling images");
    PosterEnqueueCall(unrelated_owner, nullptr, 0, nullptr); PosterDequeueCall(unrelated_owner, nullptr); PosterUpdateCall(unrelated_owner, nullptr);
    Check(poster_calls == 3, "poster suppression affected a different screen");
    renderer->material = screen_material; Frame();
    Check(renderer->material == receivers[0].material, "a game material reassignment hid the active media material");
    BetterEndfieldNext_MediaScreenCommandV1(1, 0); Frame();
    Check(renderer->material == screen_material && screen_material->texture == original_texture && renderer->extra == 7 && roots.empty(), "stop failed to restore original material");
    PosterUpdateCall(owner, nullptr); Check(poster_calls == 4, "poster controller did not resume after stop");
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); AwaitImage();
    auto* replacement = Make(Tex); auto* foreign_material = Make(Mat); foreign_material->texture = replacement; renderer->material = foreign_material;
    BetterEndfieldNext_MediaScreenCommandV1(1, 0); Frame();
    Check(renderer->material == foreign_material && foreign_material->texture == replacement, "stop overwrote another owner's material"); renderer->material = screen_material;
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); AwaitImage(); screen->alive = false; Frame();
    int count = finds; screen->alive = true; for (int i = 0; i < 5; ++i) Frame();
    Check(!bound && finds == count && roots.empty(), "scene loss must stop and never rebind");
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); AwaitImage();
    Configure((config + "width=640\n").c_str()); Frame(); count = finds; Frame();
    Check(!bound && finds == count && roots.empty(), "settings change must stop without starting");
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    BetterEndfieldNext_MediaScreenCommandV1(1, 0); Frame();
    for (int i = 0; i < 30; ++i) { Frame(); std::this_thread::sleep_for(std::chrono::milliseconds(2)); }
    Check(renderer->material == screen_material && screen_material->texture == original_texture && roots.empty(), "cancelled image request repainted after stop");
    Configure("enabled=false\n"); BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    Check(!bound && finds == count + 1, "disabled source performed a bind");
    Check(!BetterEndfieldNext_MediaScreenCommandV1(4, 0) && !BetterEndfieldNext_MediaScreenCommandV1(3, NAN), "invalid control accepted");
}
void ConfigurationCases() {
    Configuration config, before; std::string error;
    Check(ParseConfiguration("", config, error) && !config.enabled && config.hotkey == 0xbd, "media screen must default to disabled/main-keyboard minus");
    Check(config.wall_screen_enabled && config.desk_screen_enabled && config.desk_fit_mode=="fit","existing configs lost screen targets or default aspect fit");
    Check(ParseConfiguration("wall_screen_enabled=false\ndesk_screen_enabled=true\ndesk_fit_mode=crop",config,error) &&
        !config.wall_screen_enabled && config.desk_screen_enabled && config.desk_fit_mode=="crop","independent display settings were rejected");
    Check(ParseConfiguration("",config,error),"defaults could not be restored");
    Check(config.pause_hotkey == (Input::kCtrl|VK_OEM_MINUS) && config.control_hotkey == (Input::kAlt|VK_OEM_MINUS) && config.pause_window_bgm && config.width == 1920 && config.height == 1080 && config.fps == 60,
        "new defaults must use video Ctrl+- / control Alt+- and 1080p/60");
    Check(ParseConfiguration("pause_hotkey=\npause_window_bgm=false\nwidth=3840\nheight=2160\nfps=120", config, error) &&
        config.pause_hotkey == 0 && !config.pause_window_bgm && config.width == 3840 && config.height == 2160 && config.fps == 120,
        "4K/120/custom empty pause binding was rejected or changed");
    Check(ParseConfiguration("hotkey_vk=119", config, error) && config.hotkey == 0xbd, "old F8 prototype default must migrate to main-keyboard minus");
    Check(ParseConfiguration("toggle_hotkey=CTRL+ALT+SHIFT+-\npause_hotkey=WIN+P", config, error) &&
        config.hotkey == (0xbd | Input::kCtrl | Input::kAlt | Input::kShift) && config.pause_hotkey == ('P' | Input::kWin), "modifier bindings lost their encoding");
    Check(ParseConfiguration("toggle_hotkey=SUBTRACT\npause_hotkey=NONE", config, error) &&
        config.hotkey == VK_SUBTRACT && config.hotkey != VK_OEM_MINUS && !config.pause_hotkey, "main-keyboard minus and numpad subtract must remain distinct");
    Check(ParseConfiguration("source_path=G:/图片/a=b.png\ngain=1.25\nslide_interval=2e1\n", config, error) &&
        config.path == "G:/图片/a=b.png" && config.gain == 1.25 && config.interval == 20, "UTF8/equal/decimal round trip failed");
    before = config;
    for (const char* invalid : {"enabled=yes", "width=999999999999999", "window_pid=-1", "gain=nan", "gain=inf",
        "gain=1,2", "gain=1e999", "fps=0", "fps=121", "width=3840\nheight=3840", "pause_window_bgm=yes", "hotkey_vk=256", "slide_interval=0", "source=unknown",
        "wall_screen_enabled=yes", "desk_screen_enabled=yes", "desk_fit_mode=zoom",
        "toggle_hotkey=CTRL", "toggle_hotkey=invalid", "toggle_hotkey=F9\npause_hotkey=F9", "control_hotkey=CTRL", "toggle_hotkey=ALT+-", "pause_hotkey=ALT+-"}) {
        Check(!ParseConfiguration(invalid, config, error) && config == before, "invalid configuration mutated accepted settings");
    }
    auto png = Png(); Check(SupportedImage(png), "PNG header rejected");
    Check(!SupportedImage(Png(8192, 8192)) && !SupportedImage(Png(0, 1)), "huge/empty image accepted before decode");
    png.resize(20); Check(!SupportedImage(png), "truncated PNG header accepted");
    std::vector<unsigned char> jpeg{255,216,255,192,0,8,8,0,1,0,2,0}; Check(SupportedImage(jpeg), "JPEG SOF header rejected");
    jpeg[5] = 255; Check(!SupportedImage(jpeg), "invalid JPEG segment accepted");
}
void InputMappingCases() {
    float nearest=100; Vec2 uv{};
    Check(HitTriangle({{0.25f,0.25f,1},{0,0,-1}},{0,0,0},{1,0,0},{0,1,0},{0,0},{1,0},{0,1},nearest,uv) &&
        std::abs(uv.x-0.25f)<1e-6 && std::abs(uv.y-0.25f)<1e-6 && nearest==1, "screen ray UV interpolation failed");
    Check(!HitTriangle({{1,1,1},{0,0,-1}},{0,0,0},{1,0,0},{0,1,0},{0,0},{1,0},{0,1},nearest,uv), "off-screen ray mapped to input");
    Check(!HitTriangle({{0.25f,0.25f,1},{0,0,1}},{0,0,0},{1,0,0},{0,1,0},{0,0},{1,0},{0,1},nearest,uv), "screen behind camera mapped to input");
    CaptureGeometry geometry{1000,500,0,250,1000,500}; int x=-1,y=-1;
    Check(SourcePoint({0.25f,0.625f},1000,1000,geometry,x,y) && x==250 && y==125, "blackbar/Y-flip source mapping failed");
    Check(!SourcePoint({0.5f,0.9f},1000,1000,geometry,x,y) && !SourcePoint({1,0.5f},1000,1000,geometry,x,y), "blackbars/right edge forwarded a click");
    geometry.width=2000; Check(!SourcePoint({0.5f,0.5f},1000,1000,geometry,x,y), "corrupt capture geometry accepted");
    media_keys[VK_OEM_MINUS]=true;
    Check(ExactKeyDown(VK_OEM_MINUS), "plain minus binding failed");
    media_keys[VK_CONTROL]=true;
    Check(!ExactKeyDown(VK_OEM_MINUS) && ExactKeyDown(VK_OEM_MINUS|Input::kCtrl), "Ctrl+- collided with plain minus");
    media_keys[VK_MENU]=true;
    Check(!ExactKeyDown(VK_OEM_MINUS|Input::kCtrl) && !ExactKeyDown(VK_OEM_MINUS|Input::kAlt), "extra modifiers activated media chords");
    media_keys[VK_CONTROL]=false;
    Check(ExactKeyDown(VK_OEM_MINUS|Input::kAlt) && !ExactKeyDown(VK_OEM_MINUS), "Alt+- collided with plain minus");
    media_keys.fill(false);
    using AxisRead=ControlRead<ControlAxisInt,float,uintptr_t>;
    AxisRead::original=[](void*,uintptr_t,void*) { return 5.0f; };
    control_active=true;
    Check(AxisRead::Call(nullptr,1,nullptr)==0, "game axes leaked into control mode");
    media_foreground=false;
    Check(AxisRead::Call(nullptr,1,nullptr)==0, "active control leaked game input before foreground-loss teardown");
    media_foreground=true; control_active=false;
    Check(AxisRead::Call(nullptr,1,nullptr)==5, "control exit retained game input suppression");
}
void LegacyControlFrames() {
    fixture_capture_available=true;
    Configure("enabled=true\nsource=window\npause_window_bgm=false\nwidth=200\nheight=100\n");
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame();
    fixture_frame={1,{100,50,25,25,150,50},std::vector<unsigned char>(200*100*4,77)}; Frame();
    HWND window=CreateWindowW(L"STATIC",L"BE.GameInput.Fixture",WS_POPUP,100,100,200,100,nullptr,nullptr,GetModuleHandleW(nullptr),nullptr);
    Check(window!=nullptr,"game input fixture window unavailable");
    control_game_window=window; control_active=true; fixture_camera=Make();
    ControlSurface surface{}; surface.transform=Make(); surface.scale={1,1};
    surface.vertices={{0,0,1},{200,0,1},{200,100,1},{0,100,1}};
    surface.uvs={{0,0},{1,0},{1,1},{0,1}}; surface.triangles={0,1,2,0,2,3}; control_surfaces.push_back(surface);
    fixture_input.clear();
    ControlWindowProc(window,WM_MOUSEMOVE,0,MAKELPARAM(100,50));
    ControlWindowProc(window,WM_LBUTTONDOWN,0,MAKELPARAM(100,50)); Frame();
    Check(control_active && fixture_cursor_lock==0 && fixture_cursor_visible==1,"original cursor became locked or hidden");
    Check(fixture_input.size()==2 && fixture_input[0].x==50 && fixture_input[0].y==25 && fixture_input[1].x==50 && fixture_input[1].y==25,
        "original screen ray did not map to source content");
    Check(uploaded_window==fixture_frame.rgba,"original input changed captured pixels");
    fixture_input.clear();
    ControlWindowProc(window,WM_MOUSEMOVE,0,MAKELPARAM(199,99));
    ControlWindowProc(window,WM_LBUTTONUP,0,MAKELPARAM(199,99));
    ControlWindowProc(window,WM_LBUTTONDOWN,0,MAKELPARAM(199,99)); Frame();
    Check(fixture_input.size()==1 && fixture_input[0].message==WM_LBUTTONUP && fixture_input[0].x==50 && fixture_input[0].y==25,
        "original off-screen release was lost or off-screen click was forwarded");
    fixture_input.clear();
    ControlWindowProc(window,WM_KEYDOWN,'A',LPARAM(0x001E0001));
    ControlWindowProc(window,WM_CHAR,'a',LPARAM(0x001E0001));
    ControlWindowProc(window,WM_KEYUP,'A',LPARAM(0xC01E0001)); Frame();
    Check(fixture_input.size()==3 && fixture_input[0].message==WM_KEYDOWN && fixture_input[1].message==WM_CHAR &&
        fixture_input[2].message==WM_KEYUP,"original keyboard message order changed");
    fixture_input.clear();
    ControlWindowProc(window,WM_KEYDOWN,VK_ESCAPE,LPARAM(0x00010001));
    ControlWindowProc(window,WM_KEYUP,VK_ESCAPE,LPARAM(0xC0010001));Frame();
    Check(control_active&&fixture_input.size()==2&&fixture_input[0].key==VK_ESCAPE&&fixture_input[1].message==WM_KEYUP,"Escape exited control or leaked instead of reaching source");
    fixture_input.clear();
    ControlWindowProc(window,WM_INPUT,RIM_INPUT,0); Frame();
    Check(fixture_input.empty(),"original mode synthesized raw input or polled a virtual pointer");
    control_queue_failed=true; Frame();
    Check(bound && !control_active && uploaded_window==fixture_frame.rgba,"control failure stopped projection or changed source pixels");
    DestroyWindow(window); fixture_camera=nullptr; Stop(); fixture_input.clear(); fixture_capture_available=false;
}
void ControlShieldCases(){
    media_keys.fill(false);media_keys[VK_ESCAPE]=true;
    control_real_async=&MediaTestKeyState;control_real_key_state=&MediaTestKeyState;
    control_real_keyboard=[](PBYTE keys)->BOOL{for(int i=0;i<256;++i)keys[i]=0x81;return TRUE;};
    control_active=true;control_draining=false;
    Check(ControlAsync(VK_ESCAPE)==0&&ControlKeyState(VK_ESCAPE)==0,"game OS key polling leaked Escape");
    BYTE keyboard[256]{};Check(ControlKeyboard(keyboard)&&keyboard[VK_ESCAPE]==1,"game keyboard-state pressed bits leaked");
    {RealControlKeys allow;Check((ControlAsync(VK_ESCAPE)&0x8000)!=0,"controller hotkey cannot read physical keys through shield");}
    RAWINPUT key{};key.header.dwType=RIM_TYPEKEYBOARD;key.header.dwSize=sizeof(RAWINPUTHEADER)+sizeof(RAWKEYBOARD);key.data.keyboard.VKey=VK_ESCAPE;key.data.keyboard.MakeCode=1;
    NeutralizeControlRaw(key,key.header.dwSize);Check(key.data.keyboard.VKey==255&&key.data.keyboard.MakeCode==0,"raw Escape make reached game");
    key.data.keyboard.VKey=VK_ESCAPE;key.data.keyboard.Flags=RI_KEY_BREAK;NeutralizeControlRaw(key,key.header.dwSize);Check(key.data.keyboard.VKey==VK_ESCAPE,"raw release lost while clearing stale keys");
    RAWINPUT mouse{};mouse.header.dwType=RIM_TYPEMOUSE;mouse.header.dwSize=sizeof(RAWINPUTHEADER)+sizeof(RAWMOUSE);mouse.data.mouse.lLastX=14;mouse.data.mouse.usButtonFlags=RI_MOUSE_LEFT_BUTTON_DOWN|RI_MOUSE_RIGHT_BUTTON_UP|RI_MOUSE_WHEEL;mouse.data.mouse.usButtonData=120;
    NeutralizeControlRaw(mouse,mouse.header.dwSize);Check(mouse.data.mouse.lLastX==0&&mouse.data.mouse.usButtonFlags==RI_MOUSE_RIGHT_BUTTON_UP&&mouse.data.mouse.usButtonData==0,"raw game mouse movement/click/wheel leaked");
    using UnityRead=ControlStaticRead<ControlUnityKey,uint8_t,uintptr_t>;
    UnityRead::original=[](uintptr_t,void*)->uint8_t{return 1;};Check(UnityRead::Call(27,nullptr)==0,"Unity UI key read leaked");
    HWND window=CreateWindowW(L"STATIC",L"BE.ReleaseBarrier",WS_POPUP,0,0,100,100,nullptr,nullptr,GetModuleHandleW(nullptr),nullptr);
    control_game_window=window;control_original_proc=nullptr;EndControl(false);
    AdvanceControlDrain();Check(ControlBlocks()&&UnityRead::Call(27,nullptr)==0,"held exit key reached game after control ended");
    media_keys.fill(false);AdvanceControlDrain();Check(ControlBlocks(),"input resumed before stale frame drained");
    AdvanceControlDrain();Check(!ControlBlocks()&&UnityRead::Call(27,nullptr)==1&&(ControlAsync(VK_ESCAPE)&0x8000)==0,"released input did not restore");DestroyWindow(window);
}
void ReceiverInspectionCases(const std::filesystem::path& source) {
    auto room=Make(); room->text="ReceptionRoom";
    auto transform=Make(); transform->text="Computer_EndfieldLogo"; transform->parent=room;
    fixture_camera=Make(); fixture_camera->transform=Make();
    auto monitor=Make(); monitor->extra=123; monitor->transform=transform; monitor->bounds={{0,0,3},{0.5f,0.4f,0.1f}};
    auto material=Make(Mat); material->text="Logo_screen_material"; material->texture=Make(Tex); static_cast<Object*>(material->texture)->text="EndfieldLogo";
    monitor->material_array=Make(); monitor->material_array->items={material}; monitor->material=material;
    auto distant=Make(); distant->bounds={{100,0,0},{1,1,1}};
    fixture_renderer_array=Make(); fixture_renderer_array->items={monitor,distant};
    inspection_renderer_class.type_object=Make(); inspection_done=false;
    const auto config="enabled=true\nsource=image\nsource_path="+Utf8(source)+"\n";
    Configure(config.c_str()); Frame(); Frame(); int surveys=receiver_surveys;
    Check(!inspection_array && receiver_surveys==surveys,"configuration triggered receiver scan without explicit activation");
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); AwaitImage();
    Check(bound && inspection_done && !inspection_handle && receiver_surveys==surveys+1 && inspection_nearby==1,
        "explicit receiver scan failed to finish/filter distant objects/release its root");
    Check(monitor->material==material && material->texture && static_cast<Object*>(material->texture)->text=="EndfieldLogo",
        "read-only survey modified a computer material");
    bool path=false,logo=false;
    for (const auto& log:survey_logs) { path|=log.find("ReceptionRoom/Computer_EndfieldLogo")!=std::string::npos; logo|=log.find("receiver=123 slot=0")!=std::string::npos && log.find("_MainTex=EndfieldLogo")!=std::string::npos; }
    Check(path && logo,"survey omitted receiver path/material slot/original logo texture");
    Stop(); BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); AwaitImage();
    Check(receiver_surveys==surveys+1,"completed survey repeated on projection toggles");
    Stop(); inspection_done=false; fixture_renderer_array->items.assign(1000,distant);
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame();
    Check(bound && inspection_handle && inspection_index<=64,"survey ignored its per-frame batch budget");
    Stop(); Check(!inspection_array && !inspection_handle,"stopping projection retained a survey root/job");
    inspection_done=false; fail_call=InspectFind;
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); fail_call=-1;
    Check(bound && !inspection_handle,"unavailable diagnostics tore down projection");
    Stop(); inspection_done=false; fixture_camera=fixture_renderer_array=nullptr;
}
void DeskReceiverCases(const std::filesystem::path& source) {
    Stop(); fixture_camera=nullptr; desk_renderer_class.type_object=Make(); inspection_done=true;
    auto named=[](const char* name) { auto value=Make(); value->text=name; return value; };
    auto terminal=named("int_system_spaceship_guest_terminal(Clone)#87");
    auto mesh=named("mesh"); mesh->parent=terminal;
    auto fx=named("P_fxint_s_interactive_screen_table_2501"); fx->parent=mesh;
    auto internal=named("Internal"); internal->parent=fx;
    auto shader=named("HGRP/Effect/VFXBaseV2");
    auto make=[&](const char* name,const char* material_name,const char* texture_name,Object* parent) {
        auto value=named(name); value->transform=named(name); value->transform->parent=parent;
        value->material=Make(Mat); value->material->text=material_name; value->material->shader=shader;
        if (texture_name) { value->material->texture=named(texture_name); }
        value->material->tint={.464f,.464f,.464f,1};
        value->material_array=Make(); value->material_array->items={value->material}; return value;
    };
    std::vector<Object*> logos,displays,layers;
    for (const char* name:{"logo_bd","logo_bd_02","logo_bd_03"})
        logos.push_back(make(name,"M_fx_base01_lv001_ship_monitor_1144","T_fx_ui_1103_UI",internal));
    for (const char* name:{"wenli","wenli_02","wenli_03"})
        displays.push_back(make(name,"M_fx_s_interactive_screen_table_2501","T_fx_flow_1104_M",internal));
    layers=logos;
    displays[0]->transform->lossy_scale={10.25f,4,1}; displays[1]->transform->lossy_scale={6.5f,3.15f,1}; displays[2]->transform->lossy_scale={7.15f,4.65f,1};
    for (const char* name:{"glow_ad","glow_ad_02","glow_ad_03"})
        layers.push_back(make(name,"M_fx_s_interactive_screen_table_2502",nullptr,internal));
    layers.back()->enabled=false;
    auto unrelated=make("logo_bd","M_fx_base01_lv001_ship_monitor_1144","T_fx_ui_1103_UI",named("Internal"));
    auto wrong_texture=make("logo_bd","M_fx_base01_lv001_ship_monitor_1144","OtherLogo",internal);
    auto wrong_shader=make("logo_bd","M_fx_base01_lv001_ship_monitor_1144","T_fx_ui_1103_UI",internal);
    wrong_shader->material->shader=named("HGRP/Lit");
    auto multi=make("logo_bd","M_fx_base01_lv001_ship_monitor_1144","T_fx_ui_1103_UI",internal);
    multi->material_array->items.push_back(Make(Mat));
    auto decoration=make("logo_01","M_fxmap_base01_lv001_ship_credit_table_01_fj_01","T_fx_ui_816_UI",internal);
    std::vector<Object*> originals; for (auto display:displays) originals.push_back(display->material);
    fixture_renderer_array=Make(); fixture_renderer_array->items=displays;
    fixture_renderer_array->items.insert(fixture_renderer_array->items.end(),layers.begin(),layers.end());
    fixture_renderer_array->items.insert(fixture_renderer_array->items.end(),{unrelated,wrong_texture,wrong_shader,multi,decoration});
    auto config="enabled=true\nsource=image\nsource_path="+Utf8(source)+"\n";
    Configure(config.c_str()); Frame(); Frame(); int before=receiver_surveys;
    Check(receiver_surveys==before && !desk_array,"desk screen scanned implicitly");
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); AwaitImage();
    Check(bound && receivers.size()==4 && desk_layers.size()==6 && receiver_surveys==before+1 && !desk_array_handle,
        "manual desk binding missed exact candidates or retained its search root");
    for (size_t i=0;i<displays.size();++i) {
        auto copy=displays[i]->material;
        auto fit=static_cast<Object*>(copy->texture);
        Check(copy!=originals[i] && copy->texture!=canvas_texture && fit->texture==canvas_texture &&
            std::abs(double(fit->width)/fit->height-double(displays[i]->transform->lossy_scale.x)/displays[i]->transform->lossy_scale.y)<.003 &&
            copy->tint==std::array<float,4>{1,1,1,1} && copy->floats[45]==1,
            "desk screen did not receive the shared canvas with a readable material");
        Check(originals[i]->tint[0]==.464f && static_cast<Object*>(originals[i]->texture)->text=="T_fx_flow_1104_M",
            "desk binding mutated a shared original material");
    }
    for (auto layer:layers) Check(!layer->enabled,"desk decoration obscures projected video");
    int blits=display_blits; Paint(); Paint(); Check(display_blits==blits,"unchanged source frame repeated desk GPU scaling");
    for (auto ignored:{unrelated,wrong_texture,wrong_shader,multi,decoration})
        Check(ignored->material==ignored->material_array->items[0] && ignored->enabled,"desk binding changed an unrelated renderer");
    // An animator reenables a known decoration: repaint suppresses it again.
    layers[0]->enabled=true; Paint(); Check(!layers[0]->enabled,"animated desk layer was not suppressed");
    auto external=Make(Mat); displays[0]->material=external;
    layers[1]->material=external; layers[1]->enabled=true;
    Stop();
    Check(displays[0]->material==external && displays[1]->material==originals[1] && displays[2]->material==originals[2],
        "desk restore failed or overwrote another material owner");
    Check(layers[0]->enabled && layers[1]->enabled && !layers.back()->enabled && roots.empty(),
        "desk layer state or GC roots were not restored");
    displays[0]->material=originals[0]; layers[1]->material=layers[1]->material_array->items[0];
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); AwaitImage();
    displays[0]->alive=false; Frame();
    Check(bound && displays[1]->material!=originals[1] && layers[0]->enabled,"one released desk screen stopped other receivers or retained its hidden logo");
    auto lost_material=displays[1]->material; lost_material->alive=false; Frame();
    Check(bound && displays[1]->material==originals[1] && displays[2]->material!=originals[2] && layers[1]->enabled,
        "lost desk material stopped walls or was not restored");
    Stop(); displays[0]->alive=true; displays[0]->material=originals[0];
    // Targets are independent: desks need no wall helper, and disabled walls
    // retain their normal game animation hooks/materials.
    Configure((config+"wall_screen_enabled=false\n").c_str()); Frame(); Frame(); has_screen=false; int old_finds=finds;
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); AwaitImage();
    Check(bound && receivers.size()==3 && finds==old_finds && !helper && !OwnsHelper(screen_helper) && !OwnsPoster(screen) &&
        renderer->material==screen_material,"desk-only projection still found/owned or changed wall screens");
    Stop(); has_screen=true;
    Configure((config+"desk_screen_enabled=false\n").c_str()); Frame(); Frame(); int surveys=receiver_surveys;
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); AwaitImage();
    Check(bound && receivers.size()==1 && !desk_array && receiver_surveys==surveys && desk_displays.empty(),
        "wall-only projection scanned or adopted desk screens"); Stop();
    Configure((config+"wall_screen_enabled=false\ndesk_screen_enabled=false\n").c_str()); Frame(); Frame(); old_finds=finds; surveys=receiver_surveys;
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame();
    Check(!bound && !desk_array && finds==old_finds && receiver_surveys==surveys && assets.empty(),"all targets off still started a projection session");
    Configure(config.c_str()); Frame(); Frame();
    auto distant=named("unrelated"); fixture_renderer_array->items.assign(1000,distant);
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame();
    Check(bound && desk_array_handle && desk_index<=64,"desk search exceeded its frame budget");
    Stop(); Check(!desk_array_handle && desk_candidates.empty() && roots.empty(),"cancelled desk search retained objects");
    // Missing interfaces and a failed per-screen material adaptation leave walls working.
    fixture_renderer_array->items={displays[0],logos[0]}; fail_call=DeskFloat;
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); AwaitImage(); fail_call=-1;
    Check(bound && receivers.size()==1 && desk_displays.empty(),"failed desk material preparation broke wall projection"); Stop();
    fail_call=InspectFind; BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); fail_call=-1;
    Check(bound && !desk_array_handle,"failed desk search broke wall projection"); Stop();
    auto duplicate=make("logo_bd","M_fx_base01_lv001_ship_monitor_1144","T_fx_ui_1103_UI",internal);
    fixture_renderer_array->items={displays[0],logos[0],duplicate};
    BetterEndfieldNext_MediaScreenCommandV1(0,0); Frame(); AwaitImage();
    Check(receivers.size()==1 && desk_layers.empty(),"ambiguous desk planes were adopted"); Stop();
    desk_renderer_class={}; fixture_renderer_array=nullptr; inspection_done=false;
}
void DisplayLayoutCases() {
    auto close=[](double a,double b) { return std::abs(a-b)<.003; };
    for (double aspect:{10.25/4,6.5/3.15,7.15/4.65}) {
        auto fit=LayoutDisplay(1920,1080,aspect,"fit");
        Check(fit.width<=1920 && fit.height<=1080 && close(double(fit.width)/fit.height,aspect) &&
            close(double(fit.image_width)/fit.image_height,16.0/9) && fit.x>=0 && fit.y>=0 &&
            fit.x+fit.image_width<=fit.width && fit.y+fit.image_height<=fit.height,"desk fit distorted aspect or exceeded the canvas budget");
        auto crop=LayoutDisplay(1920,1080,aspect,"crop");
        Check(crop.image_width==crop.width && crop.image_height==crop.height &&
            close((1920*crop.source_scale.x)/(1080*crop.source_scale.y),double(crop.width)/crop.height) &&
            crop.source_offset.x>=0 && crop.source_offset.y>=0,"desk crop failed to fill at the correct aspect");
        auto stretch=LayoutDisplay(1920,1080,aspect,"stretch");
        Check(stretch.image_width==stretch.width && stretch.image_height==stretch.height &&
            stretch.source_scale.x==1 && stretch.source_scale.y==1,"desk stretch unexpectedly cropped or letterboxed");
    }
    // A 4:3 source was letterboxed once in the common 16:9 capture canvas.
    // Its content rectangle must be used for fitting and pointer coordinates.
    DisplayContent content{{.75f,1},{.125f,0}};
    auto fit=LayoutDisplay(1920,1080,10.25/4,"fit",content);
    Check(close(double(fit.image_width)/fit.image_height,4.0/3) && fit.source_scale.x==.75f && fit.source_offset.x==.125f,
        "desk fit fitted preexisting source black bars instead of the source content");
    Vec2 monitor_uv{float(fit.x+fit.image_width*.25)/fit.width,float(fit.y+fit.image_height*.75)/fit.height};
    Vec2 canvas_uv{monitor_uv.x*fit.canvas_scale.x+fit.canvas_offset.x,monitor_uv.y*fit.canvas_scale.y+fit.canvas_offset.y};
    int x=0,y=0; CaptureGeometry geometry{800,600,240,0,1440,1080};
    Check(SourcePoint(canvas_uv,1920,1080,geometry,x,y) && std::abs(x-200)<=1 && std::abs(y-150)<=1,
        "desk pointer mapping did not follow the fitted source rectangle");
    auto wide=LayoutDisplay(1920,1080,21.0/9,"crop",{{1,float(16.0/21)},{0,float((1-16.0/21)/2)}});
    Check(wide.source_scale.y<.77f && wide.source_scale.y>.75f && wide.source_offset.y>.11f,
        "ultrawide source kept its canvas letterbox during fill");
    bool rejected=false; try { LayoutDisplay(1920,1080,NAN,"fit"); } catch (...) { rejected=true; }
    Check(rejected,"invalid monitor aspect was accepted");
    rejected=false; try { LayoutDisplay(1920,1080,2,"fit",{{0,1},{0,0}}); } catch (...) { rejected=true; }
    Check(rejected,"empty source content was accepted");
}
void WindowBgmControls() {
    bgm_requests.Start();
    static BE_MusicControlApiV1 api{1, sizeof(BE_MusicControlApiV1),
        [](const char*) { ++bgm_acquires; return bgm_requests.Acquire(); },
        [](uint64_t token) { ++bgm_releases; bgm_requests.Release(token); }};
    const std::string config = "enabled=true\nsource=window\n";
    Configure(config.c_str()); Frame();
    Check(!bgm_requests.Active() && !bound, "configuration alone paused BGM or bound the screen");
    fixture_capture_available = true; music_control = &api;
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    Check(bound && bgm_pause_token && bgm_requests.Active() && !audio_token,
        "window projection did not acquire BGM pause independently of file audio");
    const auto token = bgm_pause_token;
    control_hooks_ready=false; last_frame_serial=1;
    ToggleControl();
    Check(bound && !control_active && bgm_pause_token==token && bgm_requests.Active(), "unsupported input mode tore down projection or BGM ownership");
    BetterEndfieldNext_MediaScreenCommandV1(2, 0); Frame();
    Check(paused && bgm_pause_token == token && bgm_requests.Active(), "pausing window frames unexpectedly resumed BGM");
    BetterEndfieldNext_MediaScreenCommandV1(1, 0); Frame();
    Check(!bound && !bgm_pause_token && !bgm_requests.Active() && bgm_releases == bgm_acquires, "stop failed to release BGM pause");
    Configure((config + "pause_window_bgm=false\n").c_str());
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    Check(bound && !bgm_pause_token && !bgm_requests.Active(), "BGM pause acquired while its switch was off");
    Stop(); Configure(config.c_str()); music_control = &api; fixture_capture_available = false;
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    Check(!bound && !bgm_requests.Active() && bgm_acquires == bgm_releases, "failed capture retained a BGM pause lease");
    fixture_capture_available = true; music_control = &api;
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); screen->alive = false; Frame(); screen->alive = true;
    Check(!bound && !bgm_requests.Active(), "leaving the room retained BGM pause");
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame();
    Check(!bound && status_text.find("music control unavailable") != std::string::npos, "missing BGM API was silently accepted");
    bgm_requests.Stop(); fixture_capture_available = false;
}
void BgmPauseOwnership() {
    using BetterEndfieldNext::Music::BgmPauseRequests;
    BgmPauseRequests requests;
    Check(!requests.Acquire(), "uninitialized music accepted a pause request");
    requests.Start(); const auto first = requests.Acquire(), second = requests.Acquire();
    Check(first && second && first != second && requests.Active(), "pause owners shared a token");
    requests.Release(first); requests.Release(first);
    Check(requests.Active(), "duplicate release resumed another owner's BGM");
    requests.Stop(); Check(!requests.Active() && !requests.Acquire(), "shutdown retained pause requests");
    requests.Start(); const auto resumed = requests.Acquire(); requests.Release(second);
    Check(resumed > second && requests.Active(), "stale token released a new session's pause");
    requests.Release(resumed); Check(!requests.Active(), "last owner did not restore music policy");
}
void KeyboardControls(const std::filesystem::path& source) {
    const std::string config = "enabled=true\nsource_path=" + Utf8(source) + "\ntoggle_hotkey=CTRL+-\npause_hotkey=ALT+P\n";
    Configure(config.c_str()); Frame(); const int initial_finds = finds;
    media_keys[VK_OEM_MINUS] = true; Frame();
    Check(!bound && finds == initial_finds, "minus activated a Ctrl+minus binding without Ctrl");
    media_keys[VK_CONTROL] = true; Frame(); AwaitImage();
    Check(bound && finds == initial_finds + 1, "Ctrl+minus did not activate the screen");
    Frame(); Check(bound, "held chord repeatedly toggled the screen");
    media_keys.fill(false); Frame(); media_keys['P'] = true; Frame();
    Check(!paused, "pause binding ignored required Alt"); media_keys[VK_MENU] = true; Frame();
    Check(!paused, "video pause hotkey must not freeze an image source");
    media_keys.fill(false); Frame(); media_keys[VK_CONTROL] = media_keys[VK_OEM_MINUS] = true; Frame();
    Check(!bound && roots.empty() && renderer->material == screen_material && screen_material->texture == original_texture, "Ctrl+minus failed to stop and restore");
    Configure((config + "width=640\n").c_str()); Frame(); Frame();
    Check(!bound, "configuration update activated a held chord");
    media_keys.fill(false); Frame();
    Check(Configure((config + "toggle_hotkey=NONE\npause_hotkey=NONE\n").c_str()) == BE_Result_Ok, "disabled keyboard configuration rejected");
    Frame(); media_keys[VK_CONTROL] = media_keys[VK_OEM_MINUS] = true; Frame();
    Check(!bound, "disabled keyboard binding activated the screen");
    BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); AwaitImage();
    Check(bound, "disabled keyboard binding blocked explicit button activation");
    BetterEndfieldNext_MediaScreenCommandV1(1, 0); Frame(); media_keys.fill(false);
}
void VideoControls(const std::filesystem::path& source) {
    auto config = "enabled=true\nsource_path=" + Utf8(source) + "\n";
    Configure(config.c_str()); BetterEndfieldNext_MediaScreenCommandV1(0, 0); Frame(); AwaitImage();
    player = Keep(Make(), false); running.source = "video"; running.loop = false; prepare_started = Now();
    static BE_LocalMusicApiV1 audio_api{1, sizeof(BE_LocalMusicApiV1), nullptr,
        [](uint64_t, double position) { audio_state.state = BE_LocalMusic_Playing; if (position >= 0) audio_state.position_seconds = position; return 1; },
        [](uint64_t) { audio_state.state = BE_LocalMusic_Paused; return 1; },
        [](uint64_t, double position) { audio_state.position_seconds = position; return 1; },
        [](uint64_t) { ++audio_closes; },
        [](uint64_t, BE_LocalMusicStatusV1* out) { *out = audio_state; return 1; }, nullptr};
    audio_token = 42; music = &audio_api; audio_state.state = BE_LocalMusic_Loading;
    Frame(); Check(!video_started && video_plays == 0, "video started before audio preparation");
    audio_state.state = BE_LocalMusic_Ready; Frame();
    Check(video_started && video_playing && audio_state.state == BE_LocalMusic_Playing, "prepared video/audio did not start together");
    audio_state.position_seconds = 3.75; Frame(); Check(external_clock == 3.75, "video did not follow audio clock");
    int bound_finds = finds; Frame(true);
    Check(background_paused && !paused && !video_playing && audio_state.state == BE_LocalMusic_Paused, "background suspend did not pause both channels");
    Frame(); Check(!background_paused && video_playing && finds == bound_finds, "foreground resume performed a new bind");
    desired.source="video";
    media_keys[VK_CONTROL]=media_keys[VK_OEM_MINUS]=true; Frame();
    Check(bound && paused && !video_playing && audio_state.state==BE_LocalMusic_Paused, "Ctrl+- did not pause both video channels without stopping projection");
    media_keys[VK_CONTROL]=false; Frame();
    Check(bound && paused, "releasing Ctrl before minus toggled projection");
    media_keys.fill(false); Frame(); media_keys[VK_CONTROL]=media_keys[VK_OEM_MINUS]=true; Frame();
    Check(bound && !paused && video_playing && audio_state.state==BE_LocalMusic_Playing, "Ctrl+- did not resume both video channels");
    media_keys.fill(false); Frame();
    BetterEndfieldNext_MediaScreenCommandV1(2, 0); Frame();
    Check(paused && !video_playing && audio_state.state == BE_LocalMusic_Paused, "pause did not affect both channels");
    BetterEndfieldNext_MediaScreenCommandV1(3, 18); Frame();
    Check(video_position == 18 && audio_state.position_seconds == 18 && paused, "seek lost audio synchronization/pause state");
    BetterEndfieldNext_MediaScreenCommandV1(2, 0); Frame();
    audio_state.state = BE_LocalMusic_Ended; Frame();
    Check(paused && !video_playing && status_text == "ended:video", "non-looping video failed to end");
    BetterEndfieldNext_MediaScreenCommandV1(2, 0); Frame(); running.loop = true;
    audio_state.state = BE_LocalMusic_Ended; Frame();
    Check(!paused && video_playing && video_position == 0 && audio_state.position_seconds == 0, "video/audio loop did not restart both channels");
    audio_state.state = BE_LocalMusic_Idle; Frame();
    Check(!bound && roots.empty() && audio_closes == 1 && renderer->material == screen_material && screen_material->texture == original_texture, "audio ownership change failed to release screen");
}
}
int main(int argc, char** argv) {
    try {
        Check(argc <= 2, "at most one fixture directory");
        list_contracts = argc == 2 && std::string(argv[1]) == "--contracts";
        InitializationContracts();
        if (list_contracts) return 0;
        std::filesystem::path fixture = argc == 2 ? std::filesystem::path(argv[1]) : std::filesystem::current_path() / "fixtures";
        std::filesystem::create_directories(fixture); auto path = fixture / Utf8Path("图片=a.png");
        auto png = Png(); { std::ofstream file(path, std::ios::binary); file.write(reinterpret_cast<const char*>(png.data()), png.size()); }
        ConfigurationCases(); InputMappingCases(); DisplayLayoutCases(); BgmPauseOwnership(); auto api = MockHost(); Setup(&api); Controls(path); VideoControls(path); KeyboardControls(path); LegacyControlFrames(); ControlShieldCases(); WindowBgmControls(); ReceiverInspectionCases(path); DeskReceiverCases(path); Shutdown();
        Check(roots.empty() && !BetterEndfieldNext_MediaScreenCommandV1(0, 0), "shutdown retained roots/commands");
        std::cout << "PASS media screen: stripped-client initialization, manual activation, rollback, ownership, restore, scene loss, cancellation, video/audio controls, configuration and image bounds\n";
        return 0;
    } catch (const std::exception& error) { std::cerr << "FAIL " << error.what() << '\n'; reader.Stop(); return 1; }
}
