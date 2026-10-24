// Section 13 clock controller: waveform table, manifest parsing and dispatch.
// The Python validator asserts the same manifest rules, so these checks pin the
// native side of that contract.
#include "../modules/custom_model/animation_driver.h"
#include "../shared/third_party/nlohmann/json.hpp"
#include <array>
#include <cmath>
#include <iostream>
#include <stdexcept>
#include <string>
#include <vector>
using namespace BetterEndfieldNext::CustomModel;
using J = nlohmann::json;
namespace {
size_t checks = 0;
void Check(bool condition, const std::string& reason) {
    ++checks;
    if (!condition) throw std::runtime_error(reason);
}
bool Near(float a, float b, float epsilon = 1e-5f) { return std::fabs(a - b) <= epsilon; }

std::string Manifest(const J& drivers) { return J{{"package_id", "bem.driver.test"}, {"drivers", drivers}}.dump(); }
const std::string kPackage = "D:/models/test.bem";

// Collects everything the host dispatches into one structure so a single tick
// can be asserted without counting lambda invocations.
struct Capture {
    std::vector<std::string> morphs;
    std::vector<float> morph_weight, morph_low, morph_high;
    std::vector<float> float_value, float_rest;
    std::vector<std::array<float, 4>> color_value, color_rest;
    std::vector<bool> keyword;
    DriverSink Sink() {
        DriverSink sink;
        sink.on_morph_parameter = [this](const std::string& p, float low, float high, float w) {
            morphs.push_back(p); morph_low.push_back(low); morph_high.push_back(high);
            morph_weight.push_back(w);
        };
        sink.on_material_float = [this](uint32_t, uint32_t, const std::string&, float v, float rest) {
            float_value.push_back(v); float_rest.push_back(rest);
        };
        sink.on_material_color = [this](uint32_t, uint32_t, const std::string&,
                                        const std::array<float, 4>& rgba,
                                        const std::array<float, 4>& rest) {
            color_value.push_back(rgba); color_rest.push_back(rest);
        };
        sink.on_material_keyword = [this](uint32_t, uint32_t, const std::string&, bool on) {
            keyword.push_back(on);
        };
        return sink;
    }
};

void Waveforms() {
    // Section 13: every waveform rests on 0 at phase 0 so a paused driver does
    // not jump on delivery.
    for (uint8_t raw = 0; raw < static_cast<uint8_t>(DriverWaveform::Count); ++raw) {
        const auto waveform = static_cast<DriverWaveform>(raw);
        Check(Near(AnimationDriverHost::Evaluate(waveform, 0.0), 0.0f),
              "Waveform " + std::to_string(raw) + " must output 0 at phase 0");
        Check(Near(AnimationDriverHost::Evaluate(waveform, 1.0), 0.0f),
              "Waveform " + std::to_string(raw) + " must wrap phase 1.0 to 0");
    }
    Check(Near(AnimationDriverHost::Evaluate(DriverWaveform::PingPong, 0.25), 0.5f), "pingpong quarter");
    Check(Near(AnimationDriverHost::Evaluate(DriverWaveform::PingPong, 0.75), 0.5f), "pingpong three quarters");
    Check(Near(AnimationDriverHost::Evaluate(DriverWaveform::Sine, 0.5), 1.0f), "sine peak");
    Check(Near(AnimationDriverHost::Evaluate(DriverWaveform::Sawtooth, 0.3), 0.3f), "sawtooth rises");
    Check(Near(AnimationDriverHost::Evaluate(DriverWaveform::Step, 0.49), 0.0f), "step low half");
    Check(Near(AnimationDriverHost::Evaluate(DriverWaveform::Step, 0.51), 1.0f), "step high half");
    const float jitter = AnimationDriverHost::Evaluate(DriverWaveform::Jitter, 0.37);
    Check(jitter >= 0.0f && jitter <= 1.0f, "jitter stays in [0,1]");
    Check(Near(jitter, AnimationDriverHost::Evaluate(DriverWaveform::Jitter, 0.37)),
          "jitter must be a pure function of phase");
    Check(!Near(jitter, AnimationDriverHost::Evaluate(DriverWaveform::Jitter, 0.71), 1e-6f),
          "jitter must differ between neighbouring bands");
}

void Parsing() {
    {
        AnimationDriverHost host;
        J drivers = J::array({J{{"id", "d"}, {"waveform", "square"},
            {"bindings", J::array({J{{"type", "material_float"}, {"property", "_F"}}})}}});
        Check(!host.LoadFromManifest(Manifest(drivers), kPackage), "unknown waveform drops the driver");
        Check(host.DriverCount() == 0, "unknown waveform leaves no driver");
    }
    {
        AnimationDriverHost host;
        J drivers = J::array({J{{"waveform", "sine"},
            {"bindings", J::array({J{{"type", "material_float"}, {"property", "_F"}}})}}});
        Check(!host.LoadFromManifest(Manifest(drivers), kPackage), "a driver without an id is dropped");
    }
    {
        AnimationDriverHost host;
        J bindings = J::array();
        bindings.push_back(J{{"type", "material_vector"}, {"property", "_V"}});
        bindings.push_back(J{{"type", "material_float"}, {"property", "NoUnderscore"}});
        bindings.push_back(J{{"type", "material_keyword"}, {"keyword", "lower_case"}});
        bindings.push_back(J{{"type", "morph_parameter"}, {"target", "p"}, {"range", J::array({0.4, 0.2})}});
        bindings.push_back(J{{"type", "morph_parameter"}, {"target", "p"}, {"range", J::array({-0.5, 0.5})}});
        bindings.push_back(J{{"type", "blend_shape"}, {"target", "p"}, {"range", J::array({0.25, 0.75})}});
        J drivers = J::array();
        drivers.push_back(J{{"id", "d"}, {"bindings", bindings}});
        Check(host.LoadFromManifest(Manifest(drivers), kPackage), "one valid binding keeps the driver");
        Check(host.DriverCount() == 1, "single driver loaded");
        Capture capture; host.SetPlaying("d", true);
        host.Tick(0.016f, capture.Sink());
        Check(capture.morphs.size() == 1 && capture.morphs[0] == "p",
              "only the blend_shape alias binding survives");
        Check(Near(capture.morph_low[0], 0.25f) && Near(capture.morph_high[0], 0.75f),
              "morph range is forwarded verbatim");
    }
    {
        AnimationDriverHost host;
        Check(!host.LoadFromManifest(J{{"package_id", "x"}}.dump(), kPackage), "no drivers key");
        Check(host.LoadedPath() == kPackage,
              "a driverless package still records its path so it is not re-read every frame");
    }
    {
        AnimationDriverHost host;
        Check(!host.LoadFromManifest("{not json", kPackage), "malformed json");
        Check(host.LoadedPath().empty(), "a malformed manifest is retried later");
    }
    {
        AnimationDriverHost host;
        J list = J::array();
        for (int i = 0; i < 20; ++i)
            list.push_back(J{{"id", "d" + std::to_string(i)},
                {"bindings", J::array({J{{"type", "material_float"}, {"property", "_F"}}})}});
        host.LoadFromManifest(Manifest(list), kPackage);
        Check(host.DriverCount() == 16, "the 16-driver quota is enforced");
        J wide = J::array({J{{"id", "wide"}, {"bindings", J::array()}}});
        for (int i = 0; i < 40; ++i)
            wide[0]["bindings"].push_back(J{{"type", "material_float"}, {"property", "_F"}});
        AnimationDriverHost bounded;
        bounded.LoadFromManifest(Manifest(wide), kPackage);
        Capture capture; bounded.SetPlaying("wide", true);
        bounded.Tick(0.5f, capture.Sink());
        Check(capture.float_value.size() == 32, "the 32-binding quota is enforced");
    }
}

void Dispatch() {
    AnimationDriverHost host;
    const J drivers = J::array({
        J{{"id", "clock"}, {"waveform", "pingpong"}, {"base_frequency", 1.0}, {"default_playing", false},
          {"bindings", J::array({
              J{{"type", "morph_parameter"}, {"target", "depth"}, {"range", J::array({0.0, 1.0})}},
              J{{"type", "material_float"}, {"component", 2}, {"material_slot", 1},
                {"property", "_EmissionWeight"}, {"range", J::array({0.5, 1.5})}},
              J{{"type", "material_color"}, {"component", 2}, {"material_slot", 1},
                {"property", "_EmissionColor"},
                {"colors", J::array({J::array({0, 0, 0, 1}), J::array({1, 1, 1, 0})})}},
              J{{"type", "material_keyword"}, {"keyword", "EMISSION_ON"}, {"threshold", 0.25}}})}},
        J{{"id", "idle"}, {"waveform", "sine"},
          {"bindings", J::array({J{{"type", "material_float"}, {"property", "_Idle"}}})}}});
    Check(host.LoadFromManifest(Manifest(drivers), kPackage), "valid manifest loads");
    Check(host.DriverCount() == 2, "both drivers loaded");
    Check(!host.HasActiveDrivers(), "default_playing=false starts paused");

    Capture idle; host.Tick(0.25f, idle.Sink());
    Check(idle.morphs.empty() && idle.float_value.empty(), "paused drivers dispatch nothing");

    host.SetPlaying("clock", true);
    Check(host.HasActiveDrivers(), "playing driver is active");
    Capture capture;
    host.Tick(0.25f, capture.Sink());  // phase 0.25 -> pingpong weight 0.5
    Check(capture.morph_weight.size() == 1 && Near(capture.morph_weight[0], 0.5f), "morph weight");
    Check(capture.float_value.size() == 1 && Near(capture.float_value[0], 1.0f), "float value maps the range");
    Check(Near(capture.float_rest[0], 0.5f), "float rest is the lower endpoint");
    Check(capture.color_value.size() == 1 && Near(capture.color_value[0][3], 0.5f), "colour alpha interpolates");
    Check(Near(capture.color_rest[0][3], 1.0f), "colour rest is the from endpoint");
    Check(capture.keyword.size() == 1 && capture.keyword[0], "keyword is on above the threshold");

    host.SetSpeedMillis("clock", 4000);
    std::vector<DriverSummary> summaries = host.Summaries();
    Check(summaries.size() == 2 && summaries[0].speed_millis == 4000, "speed is published as per-mille");
    Check(summaries[0].name == "clock", "name falls back to id");
    host.SetSpeedMillis("clock", 1);
    Check(host.Summaries()[0].speed_millis == 100, "speed is clamped to 0.1x");

    host.SetPlaying("clock", false);
    Check(!host.HasActiveDrivers(), "stopping releases the driver");
    Check(host.DrivenParameters().empty(), "no active morph parameter while paused");
    host.SetPlaying("clock", true);
    const auto driven = host.DrivenParameters();
    Check(driven.size() == 1 && driven[0] == "depth", "driven parameter is reported once");
}
// Playback modes and the four-state control surface. The important guarantees
// are that `once` clamps instead of wrapping, that a finished driver keeps
// delivering its last frame, and that only stop releases the parameter.
void PlaybackModes() {
    {
        AnimationDriverHost host;
        const J drivers = J::array({J{{"id", "one"}, {"waveform", "sawtooth"}, {"playback", "once"},
            {"base_frequency", 1.0},
            {"bindings", J::array({J{{"type", "morph_parameter"}, {"target", "depth"}}})}}});
        Check(host.LoadFromManifest(Manifest(drivers), kPackage), "once driver loads");
        Check(!host.HasActiveDrivers(), "once driver starts stopped");

        host.Start("one");
        Capture first; host.Tick(0.25f, first.Sink());
        Check(first.morph_weight.size() == 1 && Near(first.morph_weight[0], 0.25f),
              "once advances like loop before the end");

        // Overshooting the end must clamp, not wrap back to a small phase.
        Capture past; host.Tick(1.0f, past.Sink());
        Check(past.morph_weight.size() == 1 && Near(past.morph_weight[0], 1.0f),
              "once clamps the end weight at 1");
        Check(host.Summaries()[0].state == DriverState::Finished, "once reports finished");
        Check(host.HasActiveDrivers(), "finished driver still delivers its pose");
        Check(!host.Summaries()[0].sounding, "finished driver has no sound voice");

        // Holding must survive an arbitrary number of further frames.
        Capture held; host.Tick(0.5f, held.Sink());
        Check(held.morph_weight.size() == 1 && Near(held.morph_weight[0], 1.0f),
              "finished driver keeps delivering the last frame");

        host.Start("one");
        Check(host.Summaries()[0].state == DriverState::Playing, "start restarts a finished driver");
        Capture restarted; host.Tick(0.5f, restarted.Sink());
        Check(Near(restarted.morph_weight[0], 0.5f), "restart resets the phase to 0");
    }
    {
        AnimationDriverHost host;
        const J drivers = J::array({J{{"id", "loop"}, {"waveform", "sawtooth"},
            {"bindings", J::array({J{{"type", "morph_parameter"}, {"target", "depth"}}})}}});
        Check(host.LoadFromManifest(Manifest(drivers), kPackage), "loop driver loads");
        Check(host.Summaries()[0].playback == DriverPlayback::Loop, "playback defaults to loop");
        host.Start("loop");
        Capture wrapped; host.Tick(1.25f, wrapped.Sink());
        Check(Near(wrapped.morph_weight[0], 0.25f), "loop wraps instead of clamping");

        // Pause freezes the phase but keeps delivering it.
        host.Pause("loop");
        Check(host.Summaries()[0].state == DriverState::Paused, "pause is a distinct state");
        Check(host.HasActiveDrivers(), "paused driver is still active");
        Check(!host.Summaries()[0].sounding, "paused driver has no sound voice");
        Capture frozen; host.Tick(0.5f, frozen.Sink());
        Check(frozen.morph_weight.size() == 1 && Near(frozen.morph_weight[0], 0.25f),
              "pause holds the frozen phase");

        host.Start("loop");
        Capture resumed; host.Tick(0.25f, resumed.Sink());
        Check(Near(resumed.morph_weight[0], 0.5f), "start resumes a paused driver");

        // Stop is the only path that releases the parameter.
        host.Stop("loop");
        Check(host.Summaries()[0].state == DriverState::Stopped, "stop is reported");
        Check(!host.HasActiveDrivers(), "stopped driver is inactive");
        Capture stopped; host.Tick(0.5f, stopped.Sink());
        Check(stopped.morph_weight.empty(), "stopped driver dispatches nothing");
    }
    {
        // The legacy boolean entry point must map to start/stop, never pause, so
        // the existing runtime.ini key keeps its documented meaning.
        AnimationDriverHost host;
        const J drivers = J::array({J{{"id", "d"},
            {"bindings", J::array({J{{"type", "morph_parameter"}, {"target", "depth"}}})}}});
        Check(host.LoadFromManifest(Manifest(drivers), kPackage), "legacy driver loads");
        host.SetPlaying("d", true);
        Check(host.Summaries()[0].state == DriverState::Playing, "legacy true starts");
        host.SetPlaying("d", false);
        Check(host.Summaries()[0].state == DriverState::Stopped, "legacy false stops, not pauses");
    }
    {
        AnimationDriverHost host;
        const J drivers = J::array({J{{"id", "bad"}, {"playback", "bounce"},
            {"bindings", J::array({J{{"type", "morph_parameter"}, {"target", "depth"}}})}}});
        Check(!host.LoadFromManifest(Manifest(drivers), kPackage),
              "unknown playback drops the driver instead of falling back");
        Check(host.DriverCount() == 0, "unknown playback leaves no driver");
    }
}

}  // namespace

int main() {
    try {
        Waveforms();
        Parsing();
        Dispatch();
        PlaybackModes();
        std::cout << "PASS: BEM 1.5 animation driver waveforms, manifest quotas, dispatch and speed ("
                  << checks << " checks)\n";
        return 0;
    } catch (const std::exception& error) {
        std::cout << "FAIL: " << error.what() << "\n";
        return 1;
    }
}
