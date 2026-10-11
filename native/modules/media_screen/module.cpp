#include <BetterEndfieldNext/ModuleApi.h>
#include <BetterEndfieldNext/MediaScreen.h>
#include <BetterEndfieldNext/LocalMusic.h>
#include <BetterEndfieldNext/MusicControl.h>
#include <Windows.h>
#ifdef __ANDROID__
#include "android_frame.h"
#include "android_camera.h"
#endif
#include "configuration.h"
#include "control_hotkey.h"
#include "file_reader.h"
#include "aspect_fit.h"
#ifdef _WIN32
#include "window_capture.h"
#include <windowsx.h>
#endif
#include <atomic>
#include <array>
#include <climits>
#include <limits>
#include <chrono>
#include <condition_variable>
#include <cstring>
#include <deque>
#include <mutex>
#include <stdexcept>

namespace BetterEndfieldNext::MediaScreen {
namespace {
constexpr char kId[] = "betterendfieldnext.media_screen";
constexpr char kCore[] = "UnityEngine.CoreModule.dll";
constexpr char kGame[] = "Gameplay.Beyond.dll";
constexpr char kVideo[] = "UnityEngine.VideoModule.dll";
// SpaceShipReceptionRoomImagePoster._EnqueueImagePoster passes this property to
// Helper.Enqueue. targetPosterSlot belongs to EnqueueCharPoster instead.
constexpr char kPosterTextureProperty[] = "_MainTex";
const BE_HostApiV1* host = nullptr;
struct Method { BE_ResolvedMethodV1 resolved{}; };
enum MethodId {
    Find, GetHelper, Alive, Destroy, GetName, AddComponent,
    MaterialCtor, MaterialHasProperty, MaterialGetTexture, MaterialSetTexture, MaterialShader, RendererMaterial, RendererSetMaterial, PropertyId,
    TextureCtor, LoadRaw, Apply, ImageLoad, ArrayCreate, EnumParse, TextureWidth, TextureHeight,
    RtCtor, RtCreate, RtGetActive, RtSetActive, ClearRt, Blit, CopyTexture,
    VideoUrl, VideoSource, VideoRenderMode, VideoTarget, VideoAspect, VideoLoop, VideoAwake,
    VideoPrepare, VideoPrepared, VideoPlay, VideoPause, VideoStop, VideoTime, VideoSetTime,
    VideoLength, VideoPlaying, VideoAudio, VideoReference, VideoExternal, FramePump,
    HelperEnqueue, HelperDequeue, HelperRelease, PosterEnqueue, PosterDequeue, PosterUpdate,
    ControlTransform, ControlArrayLength, ControlArrayValue, InspectFind, InspectMaterials, InspectParent,
    DeskEnabled, DeskSetEnabled, DeskFloat, DeskColor,
    DeskLocalBounds, DeskScale, BlitCrop, VideoFrameIndex, VideoWidth, VideoHeight,
#ifdef _WIN32
    ControlComponent, ControlMesh, ControlReadable, ControlVertices, ControlUvs, ControlTriangles,
    ControlCamera, ControlRay, ControlInversePoint, ControlScreenWidth, ControlScreenHeight,
    ControlCursorVisible, ControlCursorSetVisible, ControlCursorLock, ControlCursorSetLock, ControlCheckInput, ControlCursorRequest,
    ControlTextureScale, ControlTextureOffset,
    ControlAxisString, ControlAxisInt, ControlRawString, ControlRawInt,
    ControlButtonString, ControlButtonInt, ControlDownString, ControlDownInt, ControlUpString, ControlUpInt,
    ControlAxis2String, ControlAxis2Int, ControlRaw2String, ControlRaw2Int,
    ControlKeyboardKey,ControlKeyboardDown,ControlKeyboardUp,ControlMouseButton,ControlMouseDown,ControlMouseUp,
    ControlUnityKey,ControlUnityDown,ControlUnityUp,ControlUnityMouse,ControlUnityMouseDown,ControlUnityMouseUp,
    ControlUnityAxis,ControlUnityRawAxis,ControlUnityButton,ControlUnityButtonDown,ControlUnityButtonUp,ControlUnityAny,ControlUnityAnyDown,ControlUnityReset,
    InspectBounds, InspectPosition, InspectInstance,
#endif
    Count
};
Method methods[Count];
BE_ResolvedClassV1 helper_class{}, material_class{}, texture_class{}, byte_class{}, rt_class{}, video_class{};
BE_ResolvedFieldV1 top_field{}, bottom_field{};
#ifdef _WIN32
BE_ResolvedFieldV1 poster_helper_field{};
#endif
std::mutex config_mutex, status_mutex, command_mutex;
std::recursive_mutex frame_mutex;
Configuration desired, running;
std::string status_text = "idle";
std::atomic_bool initialized{false}, stopping{false}, shutdown_ack{false};
std::condition_variable shutdown_cv;
std::mutex shutdown_mutex;
std::atomic<DWORD> frame_thread{0};
uint64_t applied_revision = 0;
std::atomic<uint64_t> config_revision{0};
struct Command { unsigned action; double value; };
std::deque<Command> commands;
FileReader reader;
uint64_t image_generation = 0, slide_at = 0;
size_t slide_index = 0;
std::vector<std::string> slides;
bool key_was_down = false, paused = false;
bool pause_key_was_down = false, background_paused = false;
bool keys_armed = false;
bool control_key_was_down = false;
int consumed_hotkey_base = 0;
std::atomic_bool bound{false};
const BE_LocalMusicApiV1* music = nullptr;
std::atomic_uint64_t audio_token{0};
const BE_MusicControlApiV1* music_control = nullptr;
uint64_t bgm_pause_token = 0;
void* helper = nullptr;
void* root = nullptr;
int32_t texture_property = 0;
void* texture_property_name = nullptr;
bool paint_confirmed = false;
bool poster_suppression_logged = false;
struct Asset { void* object = nullptr; uint32_t handle = 0; bool owned = false; };
std::vector<Asset> assets;
struct Receiver {
    void* renderer; void* material; void* saved_material;
    bool desk = false, failed = false;
    double aspect = 0;
    void* fitted = nullptr; void* resized = nullptr;
    uint64_t fit_revision = 0;
    int fit_width = 0, fit_height = 0;
};
std::vector<Receiver> receivers;
void* image_texture = nullptr;
void* canvas_texture = nullptr;
void* scaled_texture = nullptr;
void* player_go = nullptr;
void* player = nullptr;
bool video_started = false;
uint64_t canvas_revision=1;
int64_t fitted_video_frame=-1;
DisplayContent canvas_content{};
uint64_t prepare_started = 0;
#ifdef _WIN32
WindowCapture capture;
uint64_t last_frame_serial = 0;
std::vector<unsigned char> window_pixels;
#endif

uint64_t Now() { return static_cast<uint64_t>(std::chrono::duration_cast<std::chrono::milliseconds>(
    std::chrono::steady_clock::now().time_since_epoch()).count()); }
void Log(const std::string& text) { if (host) host->log(host->context, kId, text.c_str()); }
void Status(const std::string& value) {
    bool changed;
    { std::lock_guard lock(status_mutex); changed = status_text != value; status_text = value; }
    if (changed) Log(value);
}
void* Invoke(MethodId id, void* self, void** args = nullptr) {
    if (!host || !methods[id].resolved.method_info) throw std::runtime_error("Required media interface unavailable");
    void* exception = nullptr;
    void* result = host->runtime_invoke(host->context, methods[id].resolved.method_info, self, args, &exception);
    if (exception) throw std::runtime_error("Media interface rejected call " + std::to_string(id));
    return result;
}
template<class T> T Value(MethodId id, void* self, void** args = nullptr) {
    void* boxed = Invoke(id, self, args); auto* value = boxed ? host->object_unbox(host->context, boxed) : nullptr;
    if (!value) throw std::runtime_error("Media interface returned no value " + std::to_string(id));
    T result; std::memcpy(&result, value, sizeof(result)); return result;
}
bool IsAlive(void* object) {
    if (!object || !host || !methods[Alive].resolved.method_info) return false;
    void* args[]{object};
    try { return Value<uint8_t>(Alive, nullptr, args) != 0; } catch (...) { return false; }
}
std::string String(void* object) {
    if (!object) return {};
    char buffer[2048]{};
    int length = host->copy_managed_string(host->context, object, buffer, sizeof(buffer));
    if (length <= 0 || length >= static_cast<int>(sizeof(buffer))) return {};
    return buffer;
}
void* Keep(void* object, bool owned) {
    if (!object) throw std::runtime_error("Media object allocation failed");
    auto handle = host->gchandle_new(host->context, object, 0);
    if (!handle) throw std::runtime_error("Media object could not be rooted");
    assets.push_back({object, handle, owned}); return object;
}
void* New(const BE_ResolvedClassV1& type, bool owned = true) {
    return Keep(host->object_new(host->context, type.class_info), owned);
}
void* Field(const BE_ResolvedFieldV1& field, void* self) {
    return field.field_info ? host->field_get_value_object(host->context, field.field_info, self) : nullptr;
}
bool Resolve(MethodId id, const char* assembly, const char* ns, const char* klass,
             const char* name, const char* parameters, const char* result, uint32_t count, bool required = true) {
    BE_MethodDescriptorV1 desc{assembly, ns, klass, name, parameters, result, count};
    const bool ok = host->resolve_method(host->context, &desc, &methods[id].resolved) == BE_Result_Ok;
    if (!ok && required) Log(std::string("Missing media contract: ") + klass + "." + name);
    return ok || !required;
}
bool Class(const char* assembly, const char* ns, const char* name, BE_ResolvedClassV1& out, bool required = true) {
    bool ok = host->resolve_class(host->context, assembly, ns, name, &out) == BE_Result_Ok;
    if (!ok && required) Log(std::string("Missing media class: ") + name);
    return ok || !required;
}
bool ResolveField(const char* klass, const char* name, const char* type, BE_ResolvedFieldV1& out) {
    BE_FieldDescriptorV1 desc{kGame, "HG.Rendering.Runtime", klass, name, type};
    return host->resolve_field(host->context, &desc, &out) == BE_Result_Ok;
}
int32_t VideoEnum(const char* klass, const char* member) {
    BE_ResolvedClassV1 type{};
    if (!Class(kVideo, "UnityEngine.Video", klass, type)) throw std::runtime_error("Video enum unavailable");
    void* args[]{type.type_object, host->string_new(host->context, member)};
    auto boxed = Invoke(EnumParse, nullptr, args); auto* data = boxed ? host->object_unbox(host->context, boxed) : nullptr;
    if (!data) throw std::runtime_error("Video enum could not be read");
    int32_t result; std::memcpy(&result, data, sizeof(result)); return result;
}
void SetObject(MethodId id, void* self, void* value) { void* args[]{value}; Invoke(id, self, args); }
template<class T> void SetValue(MethodId id, void* self, T value) { void* args[]{&value}; Invoke(id, self, args); }

void* ReceiverCanvas(Receiver& receiver);
#include "desk_receivers.h"
#ifdef _WIN32
bool Hook(MethodId id, void* detour, void** original);
#include "window_control.h"
#include "receiver_inspection.h"
#endif

void Stop(bool restore = true) {
    CancelDeskReceivers();
    RestoreDeskLayers(restore);
#ifdef _WIN32
    EndControl();
    CancelReceiverInspection();
    capture.Stop(); last_frame_serial = 0;
    window_pixels.clear(); control_geometry={};
#endif
    reader.Cancel(); image_generation = 0;
    if (bgm_pause_token && music_control) music_control->release_bgm_pause(bgm_pause_token);
    bgm_pause_token = 0; music_control = nullptr;
    if (audio_token && music) music->close(audio_token);
    audio_token = 0; music = nullptr;
    if (IsAlive(player)) { try { Invoke(VideoStop, player); } catch (...) {} }
    if (restore) {
        for (auto& receiver : receivers) {
            if (!IsAlive(receiver.renderer)) continue;
            try {
                if (Invoke(RendererMaterial, receiver.renderer) == receiver.material) {
                    void* restore_args[]{receiver.saved_material}; Invoke(RendererSetMaterial, receiver.renderer, restore_args);
                }
            } catch (...) { Log("Could not restore a media receiver"); }
        }
    }
    bound = false; helper = root = nullptr; receivers.clear();
    for (auto it = assets.rbegin(); it != assets.rend(); ++it) {
        if (it->owned && IsAlive(it->object)) {
            try { void* args[]{it->object}; Invoke(Destroy, nullptr, args); } catch (...) {}
        }
        host->gchandle_free(host->context, it->handle);
    }
    assets.clear(); image_texture = canvas_texture = scaled_texture = player_go = player = nullptr;
    texture_property_name = nullptr; paint_confirmed = poster_suppression_logged = false;
    video_started = paused = background_paused = false; slides.clear(); slide_index = 0;
    canvas_revision=1; fitted_video_frame=-1; canvas_content={};
}
void Bind(const Configuration& config) {
    auto property=host->string_new(host->context,kPosterTextureProperty);
    std::string name=kPosterTextureProperty;
    texture_property_name=Keep(property,false);
    void* id_args[]{property}; texture_property=Value<int32_t>(PropertyId,nullptr,id_args);
    if (!config.wall_screen_enabled) {
        bound=true; Log("Manual bind: desk screens only; wall cycle remains active"); return;
    }
    std::string matched_target = config.target;
    void* target_string = host->string_new(host->context, config.target.c_str());
    void* find_args[]{target_string};
    root = Invoke(Find, nullptr, find_args);
    if (!IsAlive(root) && config.target == "ReceptionRoomImagePoster") {
        // Both known prefab names are tried only in this explicit activation.
        matched_target += "(Clone)";
        find_args[0] = host->string_new(host->context, matched_target.c_str());
        root = Invoke(Find, nullptr, find_args);
    }
    if (!IsAlive(root)) throw std::runtime_error("Screen object not found: " + config.target);
    Keep(root, false);
    uint8_t include_inactive = 1;
    void* helper_args[]{helper_class.type_object, &include_inactive};
    helper = Invoke(GetHelper, root, helper_args);
    if (!IsAlive(helper)) throw std::runtime_error("Selected object has no reception-room screen helper");
    Keep(helper, false);
    for (auto renderer : {Field(top_field, helper), Field(bottom_field, helper)}) {
        if (!IsAlive(renderer)) continue;
        if (!receivers.empty() && receivers[0].renderer == renderer) continue;
        Keep(renderer, false);
        auto saved = Invoke(RendererMaterial, renderer);
        if (!IsAlive(saved)) throw std::runtime_error("Screen renderer material is unavailable");
        Keep(saved, false);
        void* property_args[]{&texture_property};
        if (!Value<uint8_t>(MaterialHasProperty, saved, property_args))
            throw std::runtime_error("Screen material has no image-poster texture property: " + name);
        auto original_texture = Invoke(MaterialGetTexture, saved, property_args);
        std::string original_texture_name = IsAlive(original_texture) ? String(Invoke(GetName, original_texture)) : "none";
        auto material = New(material_class); void* copy_args[]{saved}; Invoke(MaterialCtor, material, copy_args);
        receivers.push_back({renderer, material, saved});
        std::string shader_name;
        if (methods[MaterialShader].resolved.method_info) {
            auto shader = Invoke(MaterialShader, material);
            if (IsAlive(shader)) shader_name = String(Invoke(GetName, shader));
        }
        Log("Receiver: " + String(Invoke(GetName, renderer)) + " material=" + String(Invoke(GetName, material)) + " shader=" + shader_name + " previous_texture=" + original_texture_name);
    }
    if (receivers.empty()) throw std::runtime_error("Screen renderers are not ready");
    Log("Manual bind: " + matched_target + " texture=" + name + " receivers=" + std::to_string(receivers.size()));
    bound = true;
}
void Paint() {
    if (!canvas_texture || !IsAlive(canvas_texture)) return;
    if (player && video_started && !desk_displays.empty()) {
        try {
        if (methods[VideoFrameIndex].resolved.method_info) {
            auto frame=Value<int64_t>(VideoFrameIndex,player);
            if (frame!=fitted_video_frame) { fitted_video_frame=frame; ++canvas_revision; }
        } else if (!paused && !background_paused) ++canvas_revision;
        if (methods[VideoWidth].resolved.method_info && methods[VideoHeight].resolved.method_info) {
            uint32_t w=Value<uint32_t>(VideoWidth,player),h=Value<uint32_t>(VideoHeight,player);
            if (w && h) {
                double factor=std::min(double(running.width)/w,double(running.height)/h);
                float sx=float(w*factor/running.width),sy=float(h*factor/running.height);
                canvas_content={{sx,sy},{(1-sx)/2,(1-sy)/2}};
            }
        }
        } catch (...) { ++canvas_revision; } // Optional video sizing cannot stop wall playback.
    }
    for (auto& receiver : receivers) {
        if (receiver.failed) continue;
        try {
        if (!IsAlive(receiver.renderer)) {
            if (receiver.desk) continue;
            throw std::runtime_error("Screen renderer released");
        }
        if (!IsAlive(receiver.material)) throw std::runtime_error("Screen material released");
        auto output=ReceiverCanvas(receiver);
        void* args[]{&texture_property, output}; Invoke(MaterialSetTexture, receiver.material, args);
        // Own the material actually assigned to this renderer, independently of
        // the game's cached material instances and animation writes.
        if (!paint_confirmed || Invoke(RendererMaterial, receiver.renderer) != receiver.material) {
            void* assign_args[]{receiver.material}; Invoke(RendererSetMaterial, receiver.renderer, assign_args);
        }
        if (!paint_confirmed) {
            void* property_args[]{&texture_property};
            if (Invoke(MaterialGetTexture, receiver.material, property_args) != output ||
                Invoke(RendererMaterial, receiver.renderer) != receiver.material)
                throw std::runtime_error("Screen renderer did not retain the media material/texture");
        }
        } catch (const std::exception& error) {
            if (!receiver.desk) throw;
            receiver.failed=true;
            try {
                if (IsAlive(receiver.renderer) && IsAlive(receiver.saved_material) && Invoke(RendererMaterial,receiver.renderer)==receiver.material)
                    SetObject(RendererSetMaterial,receiver.renderer,receiver.saved_material);
            } catch (...) {}
            Log(std::string("Desk screen update stopped; wall projection continues: ")+error.what());
        }
    }
    PaintDeskLayers();
    if (!paint_confirmed) { paint_confirmed = true; Log("Renderer material/texture verified: property=" + String(texture_property_name) + " receivers=" + std::to_string(receivers.size())); }
}
void* RenderTexture(int width, int height) {
    auto texture = New(rt_class); int32_t w = width, h = height, depth = 0;
    void* args[]{&w, &h, &depth}; Invoke(RtCtor, texture, args);
    if (!Value<uint8_t>(RtCreate, texture)) throw std::runtime_error("Cannot create media render texture");
    return texture;
}
void* ReceiverCanvas(Receiver& receiver) {
    if (!receiver.desk) return canvas_texture;
    const auto plan=LayoutDisplay(running.width,running.height,receiver.aspect,running.desk_fit_mode,canvas_content);
    if (!receiver.fitted) {
        receiver.fitted=RenderTexture(plan.width,plan.height);
        Log("Desk display canvas: "+std::to_string(plan.width)+"x"+std::to_string(plan.height)+" image="+
            std::to_string(plan.image_width)+"x"+std::to_string(plan.image_height)+" mode="+running.desk_fit_mode);
    }
    if (receiver.fit_width!=plan.image_width || receiver.fit_height!=plan.image_height) {
        if (receiver.resized) {
            void* args[]{receiver.resized}; Invoke(Destroy,nullptr,args);
            auto it=std::find_if(assets.begin(),assets.end(),[&](const Asset& asset) { return asset.object==receiver.resized; });
            if (it!=assets.end()) { host->gchandle_free(host->context,it->handle); assets.erase(it); }
            receiver.resized=nullptr;
        }
        if (plan.image_width!=plan.width || plan.image_height!=plan.height)
            receiver.resized=RenderTexture(plan.image_width,plan.image_height);
        receiver.fit_width=plan.image_width; receiver.fit_height=plan.image_height;
        receiver.fit_revision=0;
    }
    if (receiver.fit_revision==canvas_revision) return receiver.fitted;
    auto destination=receiver.resized?receiver.resized:receiver.fitted;
    if (plan.source_scale.x!=1 || plan.source_scale.y!=1 || plan.source_offset.x!=0 || plan.source_offset.y!=0) {
        auto scale=plan.source_scale,offset=plan.source_offset;
        void* args[]{canvas_texture,destination,&scale,&offset}; Invoke(BlitCrop,nullptr,args);
    } else { void* args[]{canvas_texture,destination}; Invoke(Blit,nullptr,args); }
    if (receiver.resized) {
        auto previous=Invoke(RtGetActive,nullptr); SetObject(RtSetActive,nullptr,receiver.fitted);
        struct Color { float r,g,b,a; } black{0,0,0,1}; uint8_t depth=0,color=1; float depth_value=1;
        void* clear[]{&depth,&color,&black,&depth_value};
        try { Invoke(ClearRt,nullptr,clear); }
        catch (...) { SetObject(RtSetActive,nullptr,previous); throw; }
        SetObject(RtSetActive,nullptr,previous);
        int32_t zero=0,w=plan.image_width,h=plan.image_height,x=plan.x,y=plan.y;
        void* copy[]{receiver.resized,&zero,&zero,&zero,&zero,&w,&h,receiver.fitted,&zero,&zero,&x,&y}; Invoke(CopyTexture,nullptr,copy);
    }
    receiver.fit_revision=canvas_revision; return receiver.fitted;
}
void* Texture(int width, int height) {
    auto texture = New(texture_class); int32_t w = width, h = height, format = 4, mip_count = 1;
    uint8_t linear = 0; void* args[]{&w, &h, &format, &mip_count, &linear};
    Invoke(TextureCtor, texture, args); return texture;
}
void FitImage() {
    int w = Value<int32_t>(TextureWidth, image_texture), h = Value<int32_t>(TextureHeight, image_texture);
    if (w <= 0 || h <= 0 || uint64_t(w) * h > 16777216) throw std::runtime_error("Image dimensions unsupported");
    double scale = std::min(double(running.width) / w, double(running.height) / h);
    int32_t sw = std::max(1, int(std::round(w * scale))), sh = std::max(1, int(std::round(h * scale)));
    // Temporary fit surface is reused when possible and disposed on the Unity frame.
    if (!scaled_texture || Value<int32_t>(TextureWidth, scaled_texture) != sw || Value<int32_t>(TextureHeight, scaled_texture) != sh) {
        if (scaled_texture) {
            void* args[]{scaled_texture}; Invoke(Destroy, nullptr, args);
            auto it = std::find_if(assets.begin(), assets.end(), [&](const Asset& asset) { return asset.object == scaled_texture; });
            if (it != assets.end()) { host->gchandle_free(host->context, it->handle); assets.erase(it); }
        }
        scaled_texture = RenderTexture(sw, sh);
    }
    void* blit[]{image_texture, scaled_texture}; Invoke(Blit, nullptr, blit);
    auto previous = Invoke(RtGetActive, nullptr); SetObject(RtSetActive, nullptr, canvas_texture);
    struct Color { float r, g, b, a; } black{0, 0, 0, 1};
    uint8_t depth = 0, color = 1; float depth_value = 1;
    void* clear[]{&depth, &color, &black, &depth_value};
    try { Invoke(ClearRt, nullptr, clear); }
    catch (...) { SetObject(RtSetActive, nullptr, previous); throw; }
    SetObject(RtSetActive, nullptr, previous);
    int32_t zero = 0, x = (running.width - sw) / 2, y = (running.height - sh) / 2;
    void* copy[]{scaled_texture, &zero, &zero, &zero, &zero, &sw, &sh, canvas_texture, &zero, &zero, &x, &y};
    Invoke(CopyTexture, nullptr, copy);
    canvas_content={{float(sw)/running.width,float(sh)/running.height},{float(x)/running.width,float(y)/running.height}};
}
void LoadImageResult(FileReader::Result& result) {
    if (!result.error.empty()) throw std::runtime_error(result.error);
    if (result.bytes.empty()) throw std::runtime_error("Image read was cancelled");
    int32_t length = static_cast<int32_t>(result.bytes.size());
    void* create[]{byte_class.type_object, &length};
    void* array = Invoke(ArrayCreate, nullptr, create);
    if (!array) throw std::runtime_error("Cannot allocate image byte array");
    auto handle = host->gchandle_new(host->context, array, 0);
    if (!handle) throw std::runtime_error("Cannot root image data");
    // Unity 2021 IL2CPP arrays on the supported x64/ARM64 targets have the
    // object header, bounds and native-sized max_length before element storage.
    static_assert(sizeof(void*) == 8);
    std::memcpy(static_cast<unsigned char*>(array) + 4 * sizeof(void*), result.bytes.data(), result.bytes.size());
    uint8_t unreadable = 1; void* load[]{image_texture, array, &unreadable};
    bool loaded = false;
    try { loaded = Value<uint8_t>(ImageLoad, nullptr, load) != 0; }
    catch (...) { host->gchandle_free(host->context, handle); throw; }
    host->gchandle_free(host->context, handle);
    if (!loaded) throw std::runtime_error("Cannot decode image (PNG/JPEG required)");
    FitImage(); ++canvas_revision; Paint(); slide_at = Now(); Status("playing:image");
}
void StartAudio() {
    if (!running.audio) return;
#ifdef __ANDROID__
    music = betterendfieldnext::AndroidLocalMusicApi();
#elif defined(_WIN32)
    auto library = GetModuleHandleW(L"BetterEndfieldNext.Music.dll");
    auto get = library ? reinterpret_cast<BE_GetLocalMusicApiV1Fn>(GetProcAddress(library, "BetterEndfieldNext_GetLocalMusicApiV1")) : nullptr;
    music = get ? get() : nullptr;
#endif
    if (!music || music->version != 1 || music->size < sizeof(BE_LocalMusicApiV1)) throw std::runtime_error("Local audio interface unavailable");
    audio_token = music->open(running.path.c_str(), kId);
    if (!audio_token) throw std::runtime_error("Cannot open video audio");
    music->set_gain(audio_token, static_cast<float>(running.gain));
}
#ifdef _WIN32
void PauseWindowBgm() {
    if (running.source != "window" || !running.pause_window_bgm) return;
    if (!music_control) {
        auto library = GetModuleHandleW(L"BetterEndfieldNext.Music.dll");
        auto get = library ? reinterpret_cast<BE_GetMusicControlApiV1Fn>(GetProcAddress(library, "BetterEndfieldNext_GetMusicControlApiV1")) : nullptr;
        music_control = get ? get() : nullptr;
    }
    if (!music_control || music_control->version != 1 || music_control->size < sizeof(BE_MusicControlApiV1) ||
        !music_control->acquire_bgm_pause || !music_control->release_bgm_pause)
        throw std::runtime_error("Background music control unavailable");
    bgm_pause_token = music_control->acquire_bgm_pause(kId);
    if (!bgm_pause_token) throw std::runtime_error("Cannot pause background music");
    Log("Window projection requested BGM pause");
}
#endif
void Start(const Configuration& config) {
    running = config;
    if (!running.enabled) throw std::runtime_error("Media screen disabled");
    if (!running.wall_screen_enabled && !running.desk_screen_enabled) { Status("idle:no_screens_selected"); return; }
#ifdef __ANDROID__
    if (running.source == "window") throw std::runtime_error("Application capture is not available in this build");
#endif
    if (running.source != "window" && running.source != "slideshow" && running.path.empty()) throw std::runtime_error("No media source selected");
    Bind(running);
    if (running.source == "image" || running.source == "slideshow") {
        slides = ImagePaths(running);
        if (slides.empty()) throw std::runtime_error("No images selected");
        canvas_texture = RenderTexture(running.width, running.height); image_texture = Texture(2, 2);
        image_generation = reader.Read(slides[0]); Status("loading:image");
    } else if (running.source == "video") {
        if (!video_class.class_info || !methods[VideoPrepared].resolved.method_info) throw std::runtime_error("VideoPlayer is unavailable on this client");
        canvas_texture = RenderTexture(running.width, running.height);
        BE_ResolvedClassV1 go_class{};
        if (!Class(kCore, "UnityEngine", "GameObject", go_class)) throw std::runtime_error("Cannot create video owner");
        player_go = New(go_class);
        BE_ResolvedMethodV1 ctor{};
        BE_MethodDescriptorV1 desc{kCore, "UnityEngine", "GameObject", ".ctor", "System.String", "System.Void", 1};
        if (host->resolve_method(host->context, &desc, &ctor) != BE_Result_Ok) throw std::runtime_error("Video owner constructor unavailable");
        void* go_name = host->string_new(host->context, "BetterEndfieldNext.MediaScreen"); void* args[]{go_name}; void* exception = nullptr;
        host->runtime_invoke(host->context, ctor.method_info, player_go, args, &exception);
        if (exception) throw std::runtime_error("Cannot construct video owner");
        void* add[]{video_class.type_object}; player = Invoke(AddComponent, player_go, add); Keep(player, false);
        SetValue<uint8_t>(VideoAwake, player, 0);
        SetValue<int32_t>(VideoSource, player, VideoEnum("VideoSource", "Url"));
        SetValue<int32_t>(VideoRenderMode, player, VideoEnum("VideoRenderMode", "RenderTexture"));
        SetValue<int32_t>(VideoAudio, player, VideoEnum("VideoAudioOutputMode", "None"));
        SetValue<int32_t>(VideoAspect, player, VideoEnum("VideoAspectRatio", "FitInside"));
        SetValue<uint8_t>(VideoLoop, player, running.loop ? 1 : 0);
        SetObject(VideoTarget, player, canvas_texture);
        // File URLs are resolved by the system video backend, not a command shell.
        auto utf8_path = Utf8Path(running.path).generic_u8string();
        std::string media_path(reinterpret_cast<const char*>(utf8_path.data()), utf8_path.size());
        SetObject(VideoUrl, player, host->string_new(host->context, media_path.c_str()));
        StartAudio(); Invoke(VideoPrepare, player); prepare_started = Now(); Status("loading:video");
    }
#ifdef _WIN32
    else {
        PauseWindowBgm();
        canvas_texture = Texture(running.width, running.height);
        capture.Start(running.window, running.window_pid, running.width, running.height, running.fps);
        Log("Window capture backend: external helper; source_pid=" + std::to_string(running.window_pid));
        Status("loading:window");
    }
#endif
    BeginDeskReceivers();
#ifdef _WIN32
    BeginReceiverInspection();
#endif
}
void VideoFrame() {
    if (!video_started) {
        if (Now() - prepare_started > 30000) throw std::runtime_error("Video preparation timed out");
        if (!Value<uint8_t>(VideoPrepared, player)) return;
        BE_LocalMusicStatusV1 audio{}; audio.size = sizeof(audio);
        if (audio_token) {
            if (!music->status(audio_token, &audio) || audio.state == BE_LocalMusic_Error || audio.state == BE_LocalMusic_Idle) throw std::runtime_error("Video audio failed to prepare or changed owner");
            if (audio.state == BE_LocalMusic_Loading) return;
            if (!music->play(audio_token, 0)) throw std::runtime_error("Cannot start video audio");
            if (methods[VideoReference].resolved.method_info && methods[VideoExternal].resolved.method_info)
                SetValue<int32_t>(VideoReference, player, VideoEnum("VideoTimeReference", "ExternalTime"));
        }
        Invoke(VideoPlay, player); video_started = true; Status("playing:video");
    }
    if (audio_token && !paused) {
        BE_LocalMusicStatusV1 audio{}; audio.size = sizeof(audio);
        if (!music->status(audio_token, &audio) || audio.state == BE_LocalMusic_Error || audio.state == BE_LocalMusic_Idle) throw std::runtime_error("Video audio stopped or changed owner");
        if (audio.state == BE_LocalMusic_Ended) {
            if (running.loop) {
                SetValue<double>(VideoSetTime, player, 0);
                if (!music->play(audio_token, 0)) throw std::runtime_error("Cannot restart video audio");
                Invoke(VideoPlay, player);
            } else { Invoke(VideoPause, player); paused = true; Status("ended:video"); }
        } else if (methods[VideoExternal].resolved.method_info) SetValue<double>(VideoExternal, player, audio.position_seconds);
    }
    if (!audio_token && !running.loop && methods[VideoPlaying].resolved.method_info &&
        !Value<uint8_t>(VideoPlaying, player)) {
        const double duration = Value<double>(VideoLength, player), position = Value<double>(VideoTime, player);
        if (duration > 0 && position >= duration - 0.1) { paused = true; Status("ended:video"); }
    }
    Paint();
}
void PauseToggle() {
    if (!bound) return;
    if (running.source == "video" && !video_started) return;
    paused = !paused;
    if (player) {
        Invoke(paused ? VideoPause : VideoPlay, player);
        if (audio_token) { if (paused) music->pause(audio_token); else music->play(audio_token, -1); }
    }
    if (!paused) slide_at = Now();
    Status(paused ? "paused:" + running.source : "playing:" + running.source);
}
void Seek(double position) {
    if (!bound || !player || !video_started || !std::isfinite(position) || position < 0) return;
    double duration = Value<double>(VideoLength, player);
    if (duration > 0) position = std::min(position, duration);
    SetValue<double>(VideoSetTime, player, position);
    if (audio_token && !music->seek(audio_token, position)) throw std::runtime_error("Audio seek failed");
}
#ifdef _WIN32
void WindowFrame(bool receive=true) {
    auto error=capture.Error(); if (!error.empty()) throw std::runtime_error(error);
    WindowCapture::FrameData data;
    bool fresh=receive && capture.Take(last_frame_serial,data);
    if (fresh) {
        if (data.rgba.size()!=size_t(running.width)*running.height*4 || !data.geometry.source_width || !data.geometry.source_height)
            throw std::runtime_error("Invalid captured window frame");
        if (!last_frame_serial) {
            size_t colored=0;
            for (size_t pixel=0;pixel+3<data.rgba.size();pixel+=4)
                if (data.rgba[pixel]||data.rgba[pixel+1]||data.rgba[pixel+2]) ++colored;
            Log("First window frame uploaded: bytes="+std::to_string(data.rgba.size())+" non_black_pixels="+std::to_string(colored));
        }
        window_pixels.swap(data.rgba);
        control_geometry=data.geometry; last_frame_serial=data.serial;
        const auto& geometry=data.geometry;
        if (!geometry.width || !geometry.height || uint64_t(geometry.left)+geometry.width>uint32_t(running.width) ||
            uint64_t(geometry.top)+geometry.height>uint32_t(running.height)) throw std::runtime_error("Invalid captured content rectangle");
        canvas_content={{float(geometry.width)/running.width,float(geometry.height)/running.height},
            {float(geometry.left)/running.width,float(running.height-geometry.top-geometry.height)/running.height}};
    }
    if (fresh && !window_pixels.empty()) {
        void* pointer=window_pixels.data(); int32_t size=int32_t(window_pixels.size());
        void* upload[]{&pointer,&size}; Invoke(LoadRaw,canvas_texture,upload);
        uint8_t mip=0,unreadable=0; void* apply[]{&mip,&unreadable}; Invoke(Apply,canvas_texture,apply);
        ++canvas_revision;
    }
    Paint(); if (fresh) Status("playing:window");
}
#endif
void Frame(bool suspended = false) {
    std::lock_guard frame_lock(frame_mutex);
    if (!host || (!initialized.load() && !stopping.load())) return;
    DWORD unset = 0;
    frame_thread.compare_exchange_strong(unset, GetCurrentThreadId());
    if (frame_thread != GetCurrentThreadId()) return;
#ifdef _WIN32
    AdvanceControlDrain();
#endif
    try {
        if (stopping.load()) {
            Stop(); shutdown_ack.store(true); shutdown_cv.notify_all(); return;
        }
        Configuration config;
        uint64_t revision;
        { std::lock_guard lock(config_mutex); config = desired; revision = config_revision.load(); }
        if (revision != applied_revision) {
            // Editing source/configuration never implicitly starts a session.
            if (bound && config != running) { Stop(); Status("idle:settings_changed"); }
            applied_revision = revision;
            keys_armed = false;
        }
        bool down = false, pause_down = false, control_down = false;
#ifdef _WIN32
        DWORD pid = 0; GetWindowThreadProcessId(GetForegroundWindow(), &pid);
        down = pid == GetCurrentProcessId() && ExactKeyDown(config.hotkey);
        pause_down = pid == GetCurrentProcessId() && config.source=="video" && ExactKeyDown(config.pause_hotkey);
        control_down = pid == GetCurrentProcessId() && config.source=="window" && ExactKeyDown(config.control_hotkey);
#elif defined(__ANDROID__)
        down = Input::IsDown(config.hotkey);
        pause_down = Input::IsDown(config.pause_hotkey);
#endif
        std::deque<Command> pending;
        { std::lock_guard lock(command_mutex); pending.swap(commands); }
        if (suspended) keys_armed = false;
        if (consumed_hotkey_base && !RealKeyDown(consumed_hotkey_base)) consumed_hotkey_base=0;
        auto pressed=[&](bool current, bool previous, int binding, unsigned action) {
            if (!suspended && keys_armed && current && !previous && Input::BaseKey(binding)!=consumed_hotkey_base) {
                pending.push_back({action,0}); consumed_hotkey_base=Input::BaseKey(binding);
            }
        };
        pressed(down,key_was_down,config.hotkey,0);
        pressed(pause_down,pause_key_was_down,config.pause_hotkey,2);
        pressed(control_down,control_key_was_down,config.control_hotkey,4);
        if (!suspended && !down && !pause_down && !control_down) keys_armed = true;
        key_was_down = down;
        pause_key_was_down = pause_down;
        control_key_was_down = control_down;
        for (auto command : pending) {
            if (command.action == 1) { Stop(); Status("idle"); }
            else if (!suspended && command.action == 0) {
                if (bound) { Stop(); Status("idle"); } else Start(config);
            } else if (!suspended && command.action == 2) PauseToggle();
            else if (!suspended && command.action == 3) Seek(command.value);
#ifdef _WIN32
            else if (!suspended && command.action == 4) ToggleControl();
#endif
        }
        if (!bound) return;
        if (running.wall_screen_enabled && (!IsAlive(root) || !IsAlive(helper))) { Stop(); Status("idle:screen_released"); return; }
#ifdef _WIN32
        if (suspended || pid!=GetCurrentProcessId()) EndControl();
        else ControlFrame();
        if (!suspended && pid==GetCurrentProcessId()) ReceiverInspectionFrame();
#endif
        if (suspended) {
            if (player && video_started && !paused && !background_paused) {
                Invoke(VideoPause, player); if (audio_token) music->pause(audio_token); background_paused = true;
            }
            return;
        }
        DeskReceiversFrame();
        if (!running.wall_screen_enabled && !desk_array) {
            bool available=false;
            for (const auto& receiver:receivers) if (!receiver.failed && IsAlive(receiver.renderer)) { available=true; break; }
            if (!available) { Stop(); Status("idle:desk_screens_unavailable"); return; }
        }
        if (background_paused) {
            if (!paused) { Invoke(VideoPlay, player); if (audio_token) music->play(audio_token, -1); }
            background_paused = false;
        }
        if (paused) {
#ifdef _WIN32
            if (running.source=="window") WindowFrame(false); else
#endif
            Paint(); return;
        }
        if (player) { VideoFrame(); return; }
        if (image_texture) {
            auto result = reader.Take();
            if (result && result->generation == image_generation) { LoadImageResult(*result); image_generation = 0; }
            if (!image_generation && slides.size() > 1 && Now() - slide_at >= uint64_t(running.interval * 1000)) {
                if (slide_index + 1 >= slides.size() && !running.loop) { paused = true; Status("ended:slideshow"); }
                else { slide_index = (slide_index + 1) % slides.size(); image_generation = reader.Read(slides[slide_index]); }
            }
            Paint();
        }
#ifdef _WIN32
        else {
            WindowFrame();
        }
#endif
    } catch (const std::exception& error) { Stop(); Status(std::string("error:") + error.what()); }
    catch (...) { Stop(); Status("error:media operation failed"); }
}

#ifdef _WIN32
using PumpFn = void (*)(void*);
PumpFn original_pump = nullptr;
#endif
using EnqueueFn = void (*)(void*, void*, void*, void*);
using HelperFn = void (*)(void*, void*);
EnqueueFn original_enqueue = nullptr;
HelperFn original_dequeue = nullptr, original_release = nullptr;
#ifdef _WIN32
using PosterEnqueueFn = void (*)(void*, void*, uint8_t, void*);
PosterEnqueueFn original_poster_enqueue = nullptr;
HelperFn original_poster_dequeue = nullptr, original_poster_update = nullptr;
#endif
#ifdef _WIN32
void Pump(void* method) { original_pump(method); Frame(); }
#endif
bool OwnsHelper(void* self) {
    std::lock_guard frame_lock(frame_mutex);
    return bound && helper && self == helper;
}
#ifdef _WIN32
bool OwnsPoster(void* self) {
    std::lock_guard frame_lock(frame_mutex);
    if (!bound || !helper || !poster_helper_field.field_info || Field(poster_helper_field, self) != helper) return false;
    if (!poster_suppression_logged) { poster_suppression_logged = true; Log("Original poster cycle suppressed for the bound screen"); }
    return true;
}
void PosterEnqueueCall(void* self, void* name, uint8_t peek, void* method) { if (!OwnsPoster(self)) original_poster_enqueue(self, name, peek, method); }
void PosterDequeueCall(void* self, void* method) { if (!OwnsPoster(self)) original_poster_dequeue(self, method); }
void PosterUpdateCall(void* self, void* method) { if (!OwnsPoster(self)) original_poster_update(self, method); }
#endif
void Enqueue(void* self, void* texture, void* property, void* method) {
    if (!OwnsHelper(self)) original_enqueue(self, texture, property, method);
}
void Dequeue(void* self, void* method) { if (!OwnsHelper(self)) original_dequeue(self, method); }
void Release(void* self, void* method) {
    std::lock_guard frame_lock(frame_mutex);
    if (frame_thread == GetCurrentThreadId() && bound && self == helper) { Stop(); Status("idle:screen_released"); }
    original_release(self, method);
}
bool Hook(MethodId id, void* detour, void** original) {
    if (!methods[id].resolved.method_pointer) return false;
    return host->create_hook(host->context, kId, methods[id].resolved.method_pointer, detour, original) == BE_Result_Ok;
}
bool Contracts() {
    bool ok = true;
    auto core = [&](MethodId id, const char* cls, const char* name, const char* args, const char* result, int count) {
        ok = Resolve(id, kCore, "UnityEngine", cls, name, args, result, count) && ok;
    };
    core(Find, "GameObject", "Find", "System.String", "UnityEngine.GameObject", 1);
    core(GetHelper, "GameObject", "GetComponentInChildren", "System.Type|System.Boolean", "UnityEngine.Component", 2);
    core(Alive, "Object", "op_Implicit", "UnityEngine.Object", "System.Boolean", 1);
    core(Destroy, "Object", "Destroy", "UnityEngine.Object", "System.Void", 1);
    core(GetName, "Object", "get_name", nullptr, "System.String", 0);
    core(AddComponent, "GameObject", "AddComponent", "System.Type", "UnityEngine.Component", 1);
    core(MaterialCtor, "Material", ".ctor", "UnityEngine.Material", "System.Void", 1);
    core(MaterialHasProperty, "Material", "HasProperty", "System.Int32", "System.Boolean", 1);
    core(MaterialGetTexture, "Material", "GetTexture", "System.Int32", "UnityEngine.Texture", 1);
    core(MaterialSetTexture, "Material", "SetTexture", "System.Int32|UnityEngine.Texture", "System.Void", 2);
    Resolve(MaterialShader, kCore, "UnityEngine", "Material", "get_shader", nullptr, "UnityEngine.Shader", 0, false);
    core(RendererMaterial, "Renderer", "get_sharedMaterial", nullptr, "UnityEngine.Material", 0);
    core(RendererSetMaterial, "Renderer", "set_sharedMaterial", "UnityEngine.Material", "System.Void", 1);
    core(PropertyId, "Shader", "PropertyToID", "System.String", "System.Int32", 1);
    core(TextureCtor, "Texture2D", ".ctor", "System.Int32|System.Int32|UnityEngine.TextureFormat|System.Int32|System.Boolean", "System.Void", 5);
    core(LoadRaw, "Texture2D", "LoadRawTextureData", "System.IntPtr|System.Int32", "System.Void", 2);
    core(Apply, "Texture2D", "Apply", "System.Boolean|System.Boolean", "System.Void", 2);
    core(TextureWidth, "Texture", "get_width", nullptr, "System.Int32", 0);
    core(TextureHeight, "Texture", "get_height", nullptr, "System.Int32", 0);
    core(RtCtor, "RenderTexture", ".ctor", "System.Int32|System.Int32|System.Int32", "System.Void", 3);
    core(RtCreate, "RenderTexture", "Create", nullptr, "System.Boolean", 0);
    core(RtGetActive, "RenderTexture", "get_active", nullptr, "UnityEngine.RenderTexture", 0);
    core(RtSetActive, "RenderTexture", "set_active", "UnityEngine.RenderTexture", "System.Void", 1);
    core(ClearRt, "GL", "Clear", "System.Boolean|System.Boolean|UnityEngine.Color|System.Single", "System.Void", 4);
    core(Blit, "Graphics", "Blit", "UnityEngine.Texture|UnityEngine.RenderTexture", "System.Void", 2);
    core(CopyTexture, "Graphics", "CopyTexture", "UnityEngine.Texture|System.Int32|System.Int32|System.Int32|System.Int32|System.Int32|System.Int32|UnityEngine.Texture|System.Int32|System.Int32|System.Int32|System.Int32", "System.Void", 12);
    ok = Resolve(ImageLoad, "UnityEngine.ImageConversionModule.dll", "UnityEngine", "ImageConversion", "LoadImage", "UnityEngine.Texture2D|System.Byte[]|System.Boolean", "System.Boolean", 3) && ok;
    ok = Resolve(ArrayCreate, "mscorlib.dll", "System", "Array", "CreateInstance", "System.Type|System.Int32", "System.Array", 2) && ok;
    ok = Resolve(EnumParse, "mscorlib.dll", "System", "Enum", "Parse", "System.Type|System.String", "System.Object", 2) && ok;
    ok = Class(kGame, "HG.Rendering.Runtime", "ReceptionRoomCharPosterHelper", helper_class) && ok;
    ok = Class(kCore, "UnityEngine", "Material", material_class) && ok;
    ok = Class(kCore, "UnityEngine", "Texture2D", texture_class) && ok;
    ok = Class(kCore, "UnityEngine", "RenderTexture", rt_class) && ok;
    ok = Class("mscorlib.dll", "System", "Byte", byte_class) && ok;
    ok = ResolveField("ReceptionRoomCharPosterHelper", "m_topTargetRenderer", "UnityEngine.Renderer", top_field) && ok;
    ok = ResolveField("ReceptionRoomCharPosterHelper", "m_botTargetRenderer", "UnityEngine.Renderer", bottom_field) && ok;
    Resolve(HelperEnqueue, kGame, "HG.Rendering.Runtime", "ReceptionRoomCharPosterHelper", "Enqueue", "UnityEngine.Texture2D|System.String", "System.Void", 2, false);
    Resolve(HelperDequeue, kGame, "HG.Rendering.Runtime", "ReceptionRoomCharPosterHelper", "Dequeue", nullptr, "System.Void", 0, false);
    Resolve(HelperRelease, kGame, "HG.Rendering.Runtime", "ReceptionRoomCharPosterHelper", "OnRelease", nullptr, "System.Void", 0, false);
#ifdef _WIN32
    BE_FieldDescriptorV1 poster_field{kGame, "", "SpaceShipReceptionRoomImagePoster", "m_posterHelper", "HG.Rendering.Runtime.ReceptionRoomCharPosterHelper"};
    if (host->resolve_field(host->context, &poster_field, &poster_helper_field) == BE_Result_Ok) {
        Resolve(PosterEnqueue, kGame, "", "SpaceShipReceptionRoomImagePoster", "_EnqueueImagePoster", "System.String|System.Boolean", "System.Void", 2, false);
        Resolve(PosterDequeue, kGame, "", "SpaceShipReceptionRoomImagePoster", "_DequeueImagePoster", nullptr, "System.Void", 0, false);
        Resolve(PosterUpdate, kGame, "", "SpaceShipReceptionRoomImagePoster", "UpdateImagePoster", nullptr, "System.Void", 0, false);
    }
#endif
    Class(kVideo, "UnityEngine.Video", "VideoPlayer", video_class, false);
    auto video = [&](MethodId id, const char* name, const char* args, const char* result, int count) {
        Resolve(id, kVideo, "UnityEngine.Video", "VideoPlayer", name, args, result, count, false);
    };
    video(VideoUrl, "set_url", "System.String", "System.Void", 1);
    video(VideoSource, "set_source", "UnityEngine.Video.VideoSource", "System.Void", 1);
    video(VideoRenderMode, "set_renderMode", "UnityEngine.Video.VideoRenderMode", "System.Void", 1);
    video(VideoTarget, "set_targetTexture", "UnityEngine.RenderTexture", "System.Void", 1);
    video(VideoAspect, "set_aspectRatio", "UnityEngine.Video.VideoAspectRatio", "System.Void", 1);
    video(VideoLoop, "set_isLooping", "System.Boolean", "System.Void", 1);
    video(VideoAwake, "set_playOnAwake", "System.Boolean", "System.Void", 1);
    video(VideoPrepare, "Prepare", nullptr, "System.Void", 0);
    video(VideoPrepared, "get_isPrepared", nullptr, "System.Boolean", 0);
    video(VideoPlay, "Play", nullptr, "System.Void", 0);
    video(VideoPause, "Pause", nullptr, "System.Void", 0);
    video(VideoStop, "Stop", nullptr, "System.Void", 0);
    video(VideoTime, "get_time", nullptr, "System.Double", 0);
    video(VideoSetTime, "set_time", "System.Double", "System.Void", 1);
    video(VideoLength, "get_length", nullptr, "System.Double", 0);
    video(VideoPlaying, "get_isPlaying", nullptr, "System.Boolean", 0);
    video(VideoAudio, "set_audioOutputMode", "UnityEngine.Video.VideoAudioOutputMode", "System.Void", 1);
    video(VideoReference, "set_timeReference", "UnityEngine.Video.VideoTimeReference", "System.Void", 1);
    video(VideoExternal, "set_externalReferenceTime", "System.Double", "System.Void", 1);
    DeskContracts();
#ifdef _WIN32
    ok = Resolve(FramePump, "UnityEngine.UIModule.dll", "UnityEngine", "Canvas", "SendWillRenderCanvases", nullptr, "System.Void", 0) && ok;
    ControlContracts();
    InspectionContracts();
#endif
    return ok;
}
BE_Result BE_CALL Initialize(const BE_HostApiV1* api) {
    if (!api || api->abi_version != BETTER_ENDFIELD_NEXT_MODULE_ABI_V1) return BE_Result_InvalidArgument;
    host = api; stopping.store(false); shutdown_ack.store(false); frame_thread = 0;
    keys_armed = key_was_down = pause_key_was_down = control_key_was_down = false; consumed_hotkey_base=0;
#ifdef _WIN32
    inspection_done=false;
#endif
    std::memset(methods, 0, sizeof(methods));
    if (!Contracts()) return BE_Result_ContractMismatch;
#ifdef _WIN32
    if (!Hook(FramePump, reinterpret_cast<void*>(&Pump), reinterpret_cast<void**>(&original_pump))) return BE_Result_Failed;
#else
    betterendfieldnext::SetAndroidFrameClient(betterendfieldnext::FrameClient::MediaScreen, &Frame);
#endif
    // Enqueue/dequeue suppression is scoped to the explicitly bound helper only.
    if (!Hook(HelperEnqueue, reinterpret_cast<void*>(&Enqueue), reinterpret_cast<void**>(&original_enqueue)) ||
        !Hook(HelperDequeue, reinterpret_cast<void*>(&Dequeue), reinterpret_cast<void**>(&original_dequeue))) return BE_Result_ContractMismatch;
    if (methods[HelperRelease].resolved.method_pointer)
        Hook(HelperRelease, reinterpret_cast<void*>(&Release), reinterpret_cast<void**>(&original_release));
#ifdef _WIN32
    InstallControlHooks();
    if (methods[PosterEnqueue].resolved.method_pointer &&
        !Hook(PosterEnqueue, reinterpret_cast<void*>(&PosterEnqueueCall), reinterpret_cast<void**>(&original_poster_enqueue))) return BE_Result_Failed;
    if (methods[PosterDequeue].resolved.method_pointer &&
        !Hook(PosterDequeue, reinterpret_cast<void*>(&PosterDequeueCall), reinterpret_cast<void**>(&original_poster_dequeue))) return BE_Result_Failed;
    if (methods[PosterUpdate].resolved.method_pointer &&
        !Hook(PosterUpdate, reinterpret_cast<void*>(&PosterUpdateCall), reinterpret_cast<void**>(&original_poster_update))) return BE_Result_Failed;
#endif
    reader.Start(); initialized.store(true); Status("idle"); return BE_Result_Ok;
}
BE_Result BE_CALL Configure(const char* text) {
    Configuration next; std::string error;
    if (!ParseConfiguration(text ? text : "", next, error)) { Log(error); return BE_Result_InvalidArgument; }
    { std::lock_guard lock(config_mutex); desired = std::move(next); config_revision.fetch_add(1); }
    return BE_Result_Ok;
}
void BE_CALL Shutdown() {
    initialized.store(false); stopping.store(true);
    reader.Stop();
    if (host && (frame_thread == GetCurrentThreadId() || !frame_thread)) Frame();
    if (host && bound) {
        std::unique_lock lock(shutdown_mutex);
        shutdown_cv.wait_for(lock, std::chrono::seconds(2), [] { return shutdown_ack.load(); });
    }
#ifdef __ANDROID__
    betterendfieldnext::SetAndroidFrameClient(betterendfieldnext::FrameClient::MediaScreen, nullptr);
#endif
#ifdef _WIN32
    EndControl(false);
    control_draining=false;RestoreControlWindow();
    capture.Stop();
#endif
    if (host) host->release_module_hooks(host->context, kId);
    std::lock_guard frame_lock(frame_mutex);
    if (!assets.empty()) {
        // A terminating Player may have no remaining frames. Never invoke Unity
        // from the polling thread; release managed roots and let Player teardown
        // reclaim native resources instead of destroying them off-thread.
        for (auto& asset : assets) host->gchandle_free(host->context, asset.handle);
        assets.clear(); receivers.clear(); bound = false;
    }
    { std::lock_guard lock(command_mutex); commands.clear(); }
    host = nullptr;
}
const BE_ModuleApiV1 api{{kId, "Guest Room Media Screen", "0.1.0", BETTER_ENDFIELD_NEXT_MODULE_ABI_V1}, Initialize, Configure, Shutdown};
}
}

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfieldNext_GetModuleApiV1() { return &BetterEndfieldNext::MediaScreen::api; }
BE_EXPORT int BE_CALL BetterEndfieldNext_MediaScreenCommandV1(unsigned action, double value) {
    using namespace BetterEndfieldNext::MediaScreen;
    if (!initialized.load() || stopping.load() || action > 3 || !std::isfinite(value)) return 0;
    std::lock_guard lock(command_mutex); if (commands.size() >= 16) return 0;
    commands.push_back({action, value}); return 1;
}
BE_EXPORT int BE_CALL BetterEndfieldNext_MediaScreenStatusV1(char* destination, size_t capacity) {
    using namespace BetterEndfieldNext::MediaScreen;
    if (!destination || !capacity) return 0;
    std::lock_guard lock(status_mutex); size_t count = std::min(capacity - 1, status_text.size());
    std::memcpy(destination, status_text.data(), count); destination[count] = 0;
    return static_cast<int>(count);
}

BE_EXPORT void BE_CALL BetterEndfieldNext_MediaScreenForegroundV1(int visible) {
#ifdef __ANDROID__
    // Unity may stop delivering nativeRender as soon as the Activity pauses.
    // Pause file audio immediately through its thread-safe interface; the next
    // suspended/resumed frame handles VideoPlayer and the resume decision.
    if (!visible) {
        const auto token = BetterEndfieldNext::MediaScreen::audio_token.load();
        if (token) if (const auto* audio = betterendfieldnext::AndroidLocalMusicApi()) audio->pause(token);
    }
#else
    (void)visible;
#endif
}
