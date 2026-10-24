#pragma once
// BEM 1.5 dynamic-drivers-v1 runtime: waveform clock plus material and morph
// modulation sinks. Format specification is docs/custom_model/BEM_FORMAT_SPEC.md
// section 13; this unit owns no package parsing and no vertex memory.
#include <array>
#include <cstddef>
#include <cstdint>
#include <functional>
#include <string>
#include <string_view>
#include <utility>
#include <vector>

namespace BetterEndfieldNext::CustomModel {

// Section 13 waveform set. Every waveform evaluates to 0 at phase 0 so that a
// driver created with default_playing=false rests on its lower bound instead of
// jumping on the first frame after the model is delivered.
enum class DriverWaveform : uint8_t {
    PingPong = 0,  // p < 0.5 ? 2p : 2 - 2p        triangle
    Sine = 1,      // (1 - cos 2*pi*p) / 2         starts at 0, unlike sin
    Jitter = 2,    // deterministic hash noise      reproducible per phase
    Sawtooth = 3,  // p                            rises then snaps back
    Step = 4,      // p < 0.5 ? 0 : 1              binary
    Count
};

enum class DriverPlayback : uint8_t {
    Loop = 0,  // phase wraps in [0,1) forever
    Once = 1,  // phase advances to 1, stops there and holds the last frame
    Count
};

// Runtime state machine. `Paused` and `Finished` keep delivering samples so the
// pose is held; only `Stopped` releases the parameter back to the package.
enum class DriverState : uint8_t {
    Stopped = 0,   // not delivering; phase reset; parameters handed back
    Playing = 1,   // phase advancing
    Paused = 2,    // phase frozen, still delivering
    Finished = 3,  // once only: phase clamped at 1, still delivering
    Count
};

// True while the driver should push samples into its bindings.
inline bool DriverDelivers(DriverState state) { return state != DriverState::Stopped; }

enum class DriverBindingType : uint8_t {
    MorphParameter = 0,  // alias: blend_shape
    MaterialFloat = 1,
    MaterialColor = 2,
    MaterialKeyword = 3,
    Count
};

// Material colour binding endpoints. `weight` interpolates from -> to.
struct DriverColorRange {
    std::array<float, 4> from{0.0f, 0.0f, 0.0f, 0.0f};
    std::array<float, 4> to{0.0f, 0.0f, 0.0f, 0.0f};
};

struct DriverBinding {
    DriverBindingType type = DriverBindingType::MorphParameter;
    // MorphParameter: parameter id from manifest `parameters`.
    // MaterialFloat/MaterialColor: shader property name, section 12 regex applies.
    // MaterialKeyword: keyword name, section 12 uppercase regex applies.
    std::string target;
    std::string property;
    std::string keyword;
    uint32_t component = 0;
    uint32_t material_slot = 0;
    // MorphParameter: normalised span over the parameter tick domain, so
    // [0,1] means full travel and [0.3,0.7] restricts the sweep.
    // MaterialFloat: output clamp interval.
    float range_low = 0.0f;
    float range_high = 1.0f;
    DriverColorRange colors{};
    float threshold = 0.5f;
};

// Section 13 sound attachment. The audio bytes live in a package payload; the
// loader materialises them next to the model and hands the runtime a file path,
// because both platform backends play from a path.
struct DriverSound {
    uint32_t payload = UINT32_MAX;  // BEM payload id holding the audio bytes
    std::string extension;          // "wav"/"mp3"/... used to name the cache file
    float gain = 1.0f;              // linear, clamped to [0,2]
    bool loop = true;
    double offset_seconds = 0.0;    // delay before the voice starts
    // Resolved by the loader; empty until the payload has been written out.
    std::string path;
};

struct Driver {
    std::string id, name;
    DriverWaveform waveform = DriverWaveform::PingPong;
    DriverPlayback playback = DriverPlayback::Loop;
    bool default_playing = false;
    DriverState state = DriverState::Stopped;
    float base_frequency = 1.0f;
    float speed_multiplier = 1.0f;
    // 0..1. Under `once` it is clamped at 1 when the driver finishes.
    double phase = 0.0;
    std::vector<DriverBinding> bindings;
    DriverSound sound;
};

// Read-only snapshot for the overlay panel. `speed_millis` is the multiplier as
// per-mille so it can cross a process boundary as an integer.
struct DriverSummary {
    std::string id, name;
    DriverState state = DriverState::Stopped;
    DriverPlayback playback = DriverPlayback::Loop;
    bool default_playing = false;
    uint32_t speed_millis = 1000;
    bool has_sound = false;
    // True only while the driver is actually advancing. Paused and finished
    // drivers hold their pose but must not keep a sound voice alive.
    bool sounding = false;
};

// Per-frame dispatch targets. Any channel may be empty; empty channels are
// skipped without affecting the others.
//
// Material channels pass the resting endpoint alongside the sampled value.
// Section 13 defines weight 0 as the lower endpoint, so a driver that stops
// hands the property back at `rest` instead of freezing on its last frame.
struct DriverSink {
    std::function<void(uint32_t component, uint32_t slot, const std::string& property, float value,
                       float rest)>
        on_material_float;
    std::function<void(uint32_t component, uint32_t slot, const std::string& property,
                       const std::array<float, 4>& rgba, const std::array<float, 4>& rest)>
        on_material_color;
    std::function<void(uint32_t component, uint32_t slot, const std::string& keyword, bool enabled)>
        on_material_keyword;
    // range_low/range_high delimit the span of the parameter's tick domain the
    // weight sweeps (section 13), so the consumer can map 0..1 onto real ticks.
    std::function<void(const std::string& parameter, float range_low, float range_high,
                       float weight)>
        on_morph_parameter;
};

class AnimationDriverHost {
public:
    using LogFn = std::function<void(const std::string&)>;

    void SetLogger(LogFn logger) { logger_ = std::move(logger); }

    // Package path the current driver set was loaded from, so the loader can
    // avoid re-parsing on every tick.
    const std::string& LoadedPath() const { return loaded_path_; }

    // Parses the `drivers` array out of an embedded BEM manifest. Returns true
    // when at least one driver was installed. Malformed individual drivers are
    // dropped with a log line rather than rejecting the whole package, because a
    // broken animation must never cost the user their model.
    bool LoadFromManifest(std::string_view manifest_json, const std::string& package_path);

    bool HasActiveDrivers() const;
    std::size_t DriverCount() const { return drivers_.size(); }

    // Runtime control. `Start` begins or resumes: from Stopped or Finished the
    // phase resets to 0, from Paused it continues at the frozen phase.
    void Start(const std::string& id);
    // `Pause` freezes the current phase and keeps delivering it, so the pose is
    // held instead of snapping back.
    void Pause(const std::string& id);
    // `Stop` releases the parameter: nothing is delivered and the phase resets.
    void Stop(const std::string& id);    // Applies a desired state, which is what a control surface sends: the host
    // decides whether that means Start, Pause or Stop.
    void ApplyState(const std::string& id, DriverState state);

    // Legacy boolean entry point kept for runtime.ini `drivers=id:play|pause`.
    // true maps to Start, false maps to Stop (never Pause), which is what that
    // key has always meant.
    void SetPlaying(const std::string& id, bool play);
    void SetAllPlaying(bool play);
    // Per-mille speed multiplier, clamped to [100, 8000] (0.1x .. 8x). The phase
    // is left untouched so a speed change never makes the motion jump.
    void SetSpeedMillis(const std::string& id, uint32_t speed_millis);
    std::vector<DriverSummary> Summaries() const;

    // Drops all drivers and the loaded path.
    void Reset();

    // Advances phases by `dt` seconds and dispatches one sample per binding.
    // Callers clamp dt; this unit does not assume a maximum frame time.
    void Tick(float dt, const DriverSink& sink);

    // Parameter ids targeted by active morph bindings, de-duplicated. Bindings
    // carry no component field, so resolvers match these against their own
    // meshes' deformation channels.
    std::vector<std::string> DrivenParameters() const;

    // Sound attachments declared by the loaded drivers, paired with the driver
    // id that owns each one. The loader materialises the payloads; the tick then
    // starts and stops exactly the voice of the driver that changed state.
    std::vector<std::pair<std::string, DriverSound>> Sounds() const;
    // Records the on-disk path resolved for one sound payload. Matching on the
    // payload id keeps the loader independent of how many drivers share it.
    void SetSoundPathByPayload(uint32_t payload, const std::string& path);

    // Exposed so the waveform table can be unit tested without a clock.
    static float Evaluate(DriverWaveform waveform, double phase);

private:
    void Log(const std::string& message) const;

    LogFn logger_;
    std::string loaded_path_;
    std::vector<Driver> drivers_;
};

}  // namespace BetterEndfieldNext::CustomModel
