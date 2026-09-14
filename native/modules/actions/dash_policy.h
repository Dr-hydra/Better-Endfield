#pragma once
#include <cmath>
namespace BetterEndfield::Actions {
enum class Decision { Wait, Keep, Cancel };
// Current Liino perform: the material dissolve starts before the mesh hide.
// End particles are separate actions; neither belongs in a sustained flight.
inline bool IsLiinoFlightTeardown(unsigned logic, int actor, float delay) {
    return (logic == 20 && actor == 4 && std::fabs(delay - 1.5667f) < .002f) ||
        ((logic == 31 && (actor == 14 || actor == 15)) ||
         (logic == 42 && (actor == 16 || actor == 17))) && std::fabs(delay - 1.6f) < .002f;
}
struct Frame {
    bool allowed = false;
    bool current_special = false;
    bool entering_special = false;
    bool outgoing = false;
    bool transitioning = false;
    float time = 0;
    float delta = 0;
    float length = 0;
    bool clip_loop = false;
    int next_hash = 0;
    bool returning_to_locomotion = false;
    float next_time = 0;
};
// Animator wrap constants, normalized to each source clip. The wrap keeps the
// native special-dash state alive; the bone overlay runs on its own clock.
struct LoopConstants { float begin, target, blend, period; };
// Aglina: 208 intervals at 60 Hz. Align the two rising branches so the incoming
// first peak (98) reaches full weight at the outgoing second peak (154).
inline constexpr LoopConstants kAglinaLoop{136.0f / 208.0f, 80.0f / 208.0f, 18.0f / 208.0f, 56.0f / 208.0f};
// Liino: 173 intervals at 60 Hz. Keep the native state before its 0.9 exit.
// The low-glide pose bank has an independent 40/60-second clock; this 52/173
// wrap maintains the Animator state and does not select the visual loop window.
inline constexpr LoopConstants kLiinoLoop{107.0f / 173.0f, 55.0f / 173.0f, 15.0f / 173.0f, 52.0f / 173.0f};
enum class LoopAction { None, Blend, Abort };
struct LoopSchedule {
    LoopConstants c = kAglinaLoop;
    bool pending = false;
    float submitted_time = 0;
    float waiting = 0;
    LoopAction Update(bool active, const Frame& f) {
        if (!active) return LoopAction::None;
        if (!f.allowed || f.outgoing || (!f.current_special && !f.entering_special) ||
            !std::isfinite(f.time) || !std::isfinite(f.delta) || f.delta < 0 ||
            !std::isfinite(f.length) || f.length <= 0) return LoopAction::Abort;
        if (!f.current_special) return LoopAction::None; // Native initial entry is still blending.
        if (pending) {
            waiting += f.delta;
            if (!f.transitioning && f.time < submitted_time - c.period * .5f) {
                // v8 returned to ~0.087 instead of the requested ~0.47. A rewind
                // alone is not success: reject an ignored offset/restarted intro.
                const float target = TargetAt(submitted_time);
                if (f.time < target - .04f || f.time > target + c.blend + .08f)
                    return LoopAction::Abort;
                pending = false;
                waiting = 0;
            } else {
                // A rejected or stalled self-transition must not be retried every frame.
                return waiting > f.length * .5f + .5f ? LoopAction::Abort : LoopAction::None;
            }
        }
        if (f.transitioning) return LoopAction::None;
        // Never pull an animation back after its normal sprint exit has begun.
        if (f.time >= .85f) return LoopAction::Abort;
        return f.time >= c.begin ? LoopAction::Blend : LoopAction::None;
    }
    void Submitted(float time) { pending = true; submitted_time = time; waiting = 0; }
    float TargetAt(float time) const { return c.target + (time - c.begin); }
};
// v10 imported clip keeps native entry/tail and a pre-closed middle window.
// Its seam is baked: do not crossfade a second pair of different poses.
struct ImportedLoopSchedule {
    static constexpr float begin = 143.0f / 208.0f;
    static constexpr float target = 40.0f / 208.0f;
    static constexpr float period = 103.0f / 208.0f;
    bool pending = false;
    float submitted_time = 0, waiting = 0;
    static float TargetAt(float time) { return target + (time - begin); }
    void Submitted(float time) { pending = true; submitted_time = time; waiting = 0; }
    LoopAction Update(bool active, const Frame& f) {
        if (!active) return LoopAction::None;
        if (!f.allowed || f.outgoing || (!f.current_special && !f.entering_special) ||
            !std::isfinite(f.time) || !std::isfinite(f.delta) || f.delta < 0 ||
            !std::isfinite(f.length) || f.length <= 0)
            return LoopAction::Abort;
        if (!f.current_special) return LoopAction::None;
        if (pending) {
            waiting += f.delta;
            if (!f.transitioning && f.time < submitted_time - period * .5f) {
                const float expected = TargetAt(submitted_time);
                if (f.time < expected - .03f || f.time > expected + .10f) return LoopAction::Abort;
                pending = false; waiting = 0;
            } else return waiting > .5f ? LoopAction::Abort : LoopAction::None;
        }
        if (f.transitioning) return LoopAction::None;
        if (f.time >= .85f) return LoopAction::Abort;
        return f.time >= begin ? LoopAction::Blend : LoopAction::None;
    }
};
inline bool IsDashOrGroundSprint(bool dash, bool grounded, bool sprint, bool moving, bool airborne) {
    return !airborne && (dash || (grounded && sprint && moving));
}
inline bool CanDeferNaturalEnd(bool same_handle, bool in_main_flow, bool active,
    bool holding, bool has_command, bool alive, bool end_phase) {
    return same_handle && in_main_flow && active && holding && !has_command && alive && end_phase;
}
// A perform action that hides a prop partway through the clip. Deferring it
// keeps the prop visible while the dash repeats; the native Clear still owns
// cleanup. Characters without such an action leave the rule disabled.
struct PropHideRule { bool enabled = false; unsigned logic_id = 0; int object_index = 0; float delay = 0; };
inline constexpr PropHideRule kAglinaPropHide{true, 11, 0, 2.4f};
inline bool IsTargetHide(const PropHideRule& rule, unsigned logic_id, int object_index, bool show, float delay) {
    return rule.enabled && logic_id == rule.logic_id && object_index == rule.object_index && !show &&
        std::isfinite(delay) && std::fabs(delay - rule.delay) < .01f;
}
// The game chooses a side on entry; looping does not reroll that choice.
struct DashPolicy {
    bool active = false;
    float pending_time = 0;
    bool WithinEntryWindow() const { return !active && pending_time <= 1.0f; }
    Decision Update(const Frame& f) {
        if (!f.allowed || !std::isfinite(f.delta) || f.delta < 0) return Decision::Cancel;
        if (!active) {
            pending_time += f.delta;
            if (pending_time > 1.0f) return Decision::Cancel;
            // StartSpDash runs before Dash submits its CrossFade. The preceding
            // ordinary Dash transition is not an exit from the special state.
            if (!f.current_special && !f.entering_special) return Decision::Wait;
            active = true;
        }
        if (f.outgoing || (!f.current_special && !f.entering_special)) return Decision::Cancel;
        return Decision::Keep;
    }
};
}
