// Dynamic driver runtime: phase clock, waveform table and binding dispatch.
// Package parsing stays in bem.cpp; this unit only consumes the embedded
// manifest `drivers` array described by format specification section 13.
#include "animation_driver.h"

#include <algorithm>
#include <cmath>
#include <unordered_set>

#include "../../shared/third_party/nlohmann/json.hpp"

namespace BetterEndfieldNext::CustomModel {
namespace {

constexpr double kPi = 3.14159265358979323846;
// Section 13 quotas, per package and per driver. They exist so a manifest cannot
// make one frame dispatch unbounded reflection calls.
constexpr size_t kMaxDrivers = 16;
constexpr size_t kMaxBindingsPerDriver = 32;

// Section 12 property regex: ^_[a-zA-Z0-9_]{1,95}$
bool ValidPropertyName(const std::string& name) {
    if (name.size() < 2 || name.size() > 96) return false;
    if (name.front() != '_') return false;
    for (unsigned char c : name) {
        if (!(c == '_' || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')))
            return false;
    }
    return true;
}

// Section 12 keyword regex: ^[A-Z0-9_]{2,96}$
bool ValidKeywordName(const std::string& name) {
    if (name.size() < 2 || name.size() > 96) return false;
    for (unsigned char c : name) {
        if (!(c == '_' || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))) return false;
    }
    return true;
}

bool Finite(float v) { return std::isfinite(v); }

// Reads an optional two-element numeric pair. `fallback` is returned untouched
// when the key is absent; a present but malformed pair fails the binding.
bool ReadRange(const nlohmann::json& node, float& low, float& high) {
    if (!node.contains("range")) return true;
    const auto& range = node.at("range");
    if (!range.is_array() || range.size() != 2) return false;
    if (!range[0].is_number() || !range[1].is_number()) return false;
    low = range[0].get<float>();
    high = range[1].get<float>();
    return Finite(low) && Finite(high) && low < high;
}

bool ReadColor(const nlohmann::json& node, std::array<float, 4>& out) {
    if (!node.is_array() || node.size() != 4) return false;
    for (size_t i = 0; i < 4; ++i) {
        if (!node[i].is_number()) return false;
        out[i] = node[i].get<float>();
        // Section 12 colour channel bound.
        if (!Finite(out[i]) || out[i] < 0.0f || out[i] > 8.0f) return false;
    }
    return true;
}

DriverWaveform ParseWaveform(const std::string& name, bool& ok) {
    ok = true;
    if (name == "pingpong") return DriverWaveform::PingPong;
    if (name == "sine") return DriverWaveform::Sine;
    if (name == "jitter") return DriverWaveform::Jitter;
    if (name == "sawtooth") return DriverWaveform::Sawtooth;
    if (name == "step") return DriverWaveform::Step;
    // The format defines a closed set, so an unknown name is a package error
    // rather than an implicit fallback: silently playing the wrong curve is
    // harder to diagnose than a dropped driver with a log line.
    ok = false;
    return DriverWaveform::PingPong;
}

}  // namespace

float AnimationDriverHost::Evaluate(DriverWaveform waveform, double phase) {
    // Wrap defensively; Tick already keeps phase in [0,1).
    double p = phase - std::floor(phase);
    if (!(p >= 0.0) || p >= 1.0) p = 0.0;

    switch (waveform) {
        case DriverWaveform::Sine:
            // Cosine form, not sin: phase 0 must yield 0 so a paused driver
            // rests on its lower bound.
            return static_cast<float>((1.0 - std::cos(2.0 * kPi * p)) * 0.5);
        case DriverWaveform::PingPong:
            return static_cast<float>(p < 0.5 ? 2.0 * p : 2.0 - 2.0 * p);
        case DriverWaveform::Jitter: {
            // Deterministic hash of the quantised phase. Using rand() here would
            // make the same package behave differently between runs and would
            // break any A/B comparison against the sidecar build.
            const double quantised = std::floor(p * 64.0);
            const double raw = std::sin(quantised * 12.9898) * 43758.5453;
            const double frac = raw - std::floor(raw);
            return static_cast<float>(frac);
        }
        case DriverWaveform::Sawtooth:
            return static_cast<float>(p);
        case DriverWaveform::Step:
            return p < 0.5 ? 0.0f : 1.0f;
        default:
            return 0.0f;
    }
}

void AnimationDriverHost::Log(const std::string& message) const {
    if (logger_) logger_(message);
}

bool AnimationDriverHost::LoadFromManifest(std::string_view manifest_json,
                                           const std::string& package_path) {
    drivers_.clear();
    loaded_path_.clear();
    if (manifest_json.empty()) return false;

    nlohmann::json manifest;
    try {
        manifest = nlohmann::json::parse(manifest_json);
    } catch (const std::exception& error) {
        Log(std::string("[AnimDriver] Manifest is not readable JSON: ") + error.what());
        return false;
    }
    if (!manifest.is_object() || !manifest.contains("drivers")) {
        // Record the path even without a `drivers` key: the loader uses it to
        // decide whether a package still needs parsing, and a driverless package
        // must not be re-read on every frame.
        if (manifest.is_object()) loaded_path_ = package_path;
        return false;
    }

    const auto& list = manifest.at("drivers");
    if (!list.is_array()) {
        Log("[AnimDriver] drivers must be an array; none loaded.");
        return false;
    }
    if (list.size() > kMaxDrivers) {
        Log("[AnimDriver] drivers exceeds section 13 quota (" + std::to_string(kMaxDrivers) +
            "); only the first entries are used.");
    }

    size_t dropped = 0;
    for (const auto& entry : list) {
        if (drivers_.size() >= kMaxDrivers) break;
        try {
            if (!entry.is_object()) {
                ++dropped;
                continue;
            }
            Driver driver;
            driver.id = entry.value("id", std::string{});
            driver.name = entry.value("name", std::string{});
            if (driver.id.empty()) {
                Log("[AnimDriver] Dropped a driver without an id.");
                ++dropped;
                continue;
            }
            bool waveform_ok = false;
            driver.waveform = ParseWaveform(entry.value("waveform", std::string{"pingpong"}), waveform_ok);
            if (!waveform_ok) {
                Log("[AnimDriver] Dropped driver '" + driver.id + "': unknown waveform '" +
                    entry.value("waveform", std::string{}) + "'.");
                ++dropped;
                continue;
            }
            driver.default_playing = entry.value("default_playing", false);
            driver.playing = driver.default_playing;
            driver.base_frequency = entry.value("base_frequency", 1.0f);
            driver.speed_multiplier = entry.value("speed_multiplier", 1.0f);
            if (!Finite(driver.base_frequency) || driver.base_frequency < 0.0f ||
                !Finite(driver.speed_multiplier) || driver.speed_multiplier < 0.0f) {
                Log("[AnimDriver] Dropped driver '" + driver.id + "': frequency must be finite and non-negative.");
                ++dropped;
                continue;
            }

            if (entry.contains("sound")) {
                const auto& sound = entry.at("sound");
                if (sound.is_object()) {
                    driver.sound.payload = sound.value("payload", UINT32_MAX);
                    driver.sound.extension = sound.value("format", std::string{});
                    driver.sound.gain = sound.value("gain", 1.0f);
                    driver.sound.loop = sound.value("loop", true);
                    driver.sound.offset_seconds = sound.value("offset_seconds", 0.0);
                    if (driver.sound.payload == UINT32_MAX) {
                        Log("[AnimDriver] Driver '" + driver.id + "': sound needs a payload id.");
                        driver.sound = DriverSound{};
                    } else if (!Finite(driver.sound.gain) || driver.sound.gain < 0.0f ||
                        driver.sound.gain > 2.0f) {
                        Log("[AnimDriver] Driver '" + driver.id + "': sound gain must be inside [0,2].");
                        driver.sound = DriverSound{};
                    } else if (!Finite(driver.sound.offset_seconds) ||
                        driver.sound.offset_seconds < 0.0 || driver.sound.offset_seconds > 60.0) {
                        Log("[AnimDriver] Driver '" + driver.id + "': sound offset must be inside [0,60] s.");
                        driver.sound = DriverSound{};
                    }
                }
            }

            if (entry.contains("bindings")) {
                const auto& bindings = entry.at("bindings");
                if (!bindings.is_array()) {
                    Log("[AnimDriver] Dropped driver '" + driver.id + "': bindings must be an array.");
                    ++dropped;
                    continue;
                }
                for (const auto& node : bindings) {
                    if (driver.bindings.size() >= kMaxBindingsPerDriver) {
                        Log("[AnimDriver] Driver '" + driver.id +
                            "' exceeds the section 13 binding quota (" +
                            std::to_string(kMaxBindingsPerDriver) + "); extra bindings ignored.");
                        break;
                    }
                    DriverBinding binding;
                    const auto type = node.value("type", std::string{"morph_parameter"});
                    if (type == "morph_parameter" || type == "blend_shape") {
                        binding.type = DriverBindingType::MorphParameter;
                        binding.target = node.value("target", std::string{});
                        if (binding.target.empty()) {
                            Log("[AnimDriver] Dropped a morph binding in '" + driver.id + "': missing target.");
                            continue;
                        }
                        if (!ReadRange(node, binding.range_low, binding.range_high) ||
                            binding.range_low < 0.0f || binding.range_high > 1.0f) {
                            Log("[AnimDriver] Dropped a morph binding in '" + driver.id +
                                "': range must be an increasing pair inside [0,1].");
                            continue;
                        }
                    } else if (type == "material_float") {
                        binding.type = DriverBindingType::MaterialFloat;
                        binding.property = node.value("property", std::string{});
                        if (!ValidPropertyName(binding.property)) {
                            Log("[AnimDriver] Dropped a material_float binding in '" + driver.id +
                                "': invalid property name '" + binding.property + "'.");
                            continue;
                        }
                        if (!ReadRange(node, binding.range_low, binding.range_high)) {
                            Log("[AnimDriver] Dropped a material_float binding in '" + driver.id +
                                "': range must be an increasing pair.");
                            continue;
                        }
                        binding.component = node.value("component", 0u);
                        binding.material_slot = node.value("material_slot", 0u);
                    } else if (type == "material_color") {
                        binding.type = DriverBindingType::MaterialColor;
                        binding.property = node.value("property", std::string{});
                        if (!ValidPropertyName(binding.property)) {
                            Log("[AnimDriver] Dropped a material_color binding in '" + driver.id +
                                "': invalid property name '" + binding.property + "'.");
                            continue;
                        }
                        if (!node.contains("colors") || !node.at("colors").is_array() ||
                            node.at("colors").size() != 2 ||
                            !ReadColor(node.at("colors")[0], binding.colors.from) ||
                            !ReadColor(node.at("colors")[1], binding.colors.to)) {
                            Log("[AnimDriver] Dropped a material_color binding in '" + driver.id +
                                "': colors must be two RGBA quads inside [0,8].");
                            continue;
                        }
                        binding.component = node.value("component", 0u);
                        binding.material_slot = node.value("material_slot", 0u);
                    } else if (type == "material_keyword") {
                        binding.type = DriverBindingType::MaterialKeyword;
                        binding.keyword = node.value("keyword", std::string{});
                        if (!ValidKeywordName(binding.keyword)) {
                            Log("[AnimDriver] Dropped a material_keyword binding in '" + driver.id +
                                "': invalid keyword '" + binding.keyword + "'.");
                            continue;
                        }
                        binding.threshold = node.value("threshold", 0.5f);
                        if (!Finite(binding.threshold) || binding.threshold < 0.0f ||
                            binding.threshold > 1.0f) {
                            Log("[AnimDriver] Dropped a material_keyword binding in '" + driver.id +
                                "': threshold must be inside [0,1].");
                            continue;
                        }
                        binding.component = node.value("component", 0u);
                        binding.material_slot = node.value("material_slot", 0u);
                    } else {
                        Log("[AnimDriver] Dropped a binding in '" + driver.id + "': unknown type '" +
                            type + "'.");
                        continue;
                    }
                    driver.bindings.push_back(std::move(binding));
                }
            }

            if (driver.bindings.empty() && driver.sound.payload == UINT32_MAX) {
                Log("[AnimDriver] Dropped driver '" + driver.id + "': no usable bindings.");
                ++dropped;
                continue;
            }
            drivers_.push_back(std::move(driver));
        } catch (const std::exception& error) {
            Log(std::string("[AnimDriver] Dropped a malformed driver: ") + error.what());
            ++dropped;
        }
    }

    if (drivers_.empty()) {
        Log("[AnimDriver] No usable drivers in this package.");
        return false;
    }

    loaded_path_ = package_path;
    size_t bindings = 0;    for (const auto& driver : drivers_) bindings += driver.bindings.size();
    Log("[AnimDriver] Loaded internalized animation config from manifest (" +
        std::to_string(drivers_.size()) + " drivers, " +
        std::to_string(bindings) + " bindings)" +
        (dropped ? ", " + std::to_string(dropped) + " dropped" : std::string{}));
    return true;
}

std::vector<std::pair<std::string, DriverSound>> AnimationDriverHost::Sounds() const {
    std::vector<std::pair<std::string, DriverSound>> out;
    out.reserve(drivers_.size());
    for (const auto& driver : drivers_)
        if (driver.sound.payload != UINT32_MAX) out.emplace_back(driver.id, driver.sound);
    return out;
}

void AnimationDriverHost::SetSoundPathByPayload(uint32_t payload, const std::string& path) {
    for (auto& driver : drivers_)
        if (driver.sound.payload == payload) driver.sound.path = path;
}

bool AnimationDriverHost::HasActiveDrivers() const {
    return std::any_of(drivers_.begin(), drivers_.end(),
                       [](const Driver& driver) { return driver.playing; });
}

void AnimationDriverHost::SetPlaying(const std::string& id, bool play) {
    for (auto& driver : drivers_) {
        if (driver.id == id) driver.playing = play;
    }
}

void AnimationDriverHost::SetAllPlaying(bool play) {
    for (auto& driver : drivers_) driver.playing = play;
}

void AnimationDriverHost::SetSpeedMillis(const std::string& id, uint32_t speed_millis) {
    const uint32_t clamped = std::clamp<uint32_t>(speed_millis, 100, 8000);
    for (auto& driver : drivers_) {
        if (driver.id == id) driver.speed_multiplier = static_cast<float>(clamped) / 1000.0f;
    }
}

std::vector<DriverSummary> AnimationDriverHost::Summaries() const {
    std::vector<DriverSummary> result;
    result.reserve(drivers_.size());
    for (const auto& driver : drivers_) {
        DriverSummary summary;
        summary.id = driver.id;
        summary.name = driver.name.empty() ? driver.id : driver.name;
        summary.playing = driver.playing;
        summary.default_playing = driver.default_playing;
        const long millis = std::lround(driver.speed_multiplier * 1000.0);
        summary.speed_millis = static_cast<uint32_t>(std::clamp<long>(millis, 100, 8000));
        summary.has_sound = driver.sound.payload != UINT32_MAX;
        result.push_back(std::move(summary));
    }
    return result;
}

void AnimationDriverHost::Reset() {
    drivers_.clear();
    loaded_path_.clear();
}

void AnimationDriverHost::Tick(float dt, const DriverSink& sink) {
    if (!(dt > 0.0f) || drivers_.empty()) return;

    for (auto& driver : drivers_) {
        if (!driver.playing) continue;

        driver.phase += static_cast<double>(dt) * driver.base_frequency * driver.speed_multiplier;
        if (!std::isfinite(driver.phase)) driver.phase = 0.0;
        if (driver.phase >= 1.0 || driver.phase < 0.0) driver.phase -= std::floor(driver.phase);

        const float weight = Evaluate(driver.waveform, driver.phase);

        for (const auto& binding : driver.bindings) {
            switch (binding.type) {
                case DriverBindingType::MorphParameter:
                    if (sink.on_morph_parameter)
                        sink.on_morph_parameter(binding.target, binding.range_low,
                                                binding.range_high, weight);
                    break;
                case DriverBindingType::MaterialFloat:
                    if (sink.on_material_float) {
                        const float value =
                            binding.range_low + (binding.range_high - binding.range_low) * weight;
                        sink.on_material_float(binding.component, binding.material_slot,
                                               binding.property, value, binding.range_low);
                    }
                    break;
                case DriverBindingType::MaterialColor:
                    if (sink.on_material_color) {
                        std::array<float, 4> rgba{};
                        for (size_t i = 0; i < 4; ++i) {
                            rgba[i] = binding.colors.from[i] +
                                      (binding.colors.to[i] - binding.colors.from[i]) * weight;
                        }
                        sink.on_material_color(binding.component, binding.material_slot,
                                               binding.property, rgba, binding.colors.from);
                    }
                    break;
                case DriverBindingType::MaterialKeyword:
                    if (sink.on_material_keyword) {
                        sink.on_material_keyword(binding.component, binding.material_slot,
                                                 binding.keyword, weight > binding.threshold);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

std::vector<std::string> AnimationDriverHost::DrivenParameters() const {
    // Morph bindings carry no component field: a parameter id is global, and the
    // loader decides which mesh owns the matching deformation channel.
    std::vector<std::string> result;
    std::unordered_set<std::string> seen;
    for (const auto& driver : drivers_) {
        if (!driver.playing) continue;
        for (const auto& binding : driver.bindings) {
            if (binding.type != DriverBindingType::MorphParameter) continue;
            if (seen.insert(binding.target).second) result.push_back(binding.target);
        }
    }
    return result;
}

}  // namespace BetterEndfieldNext::CustomModel
