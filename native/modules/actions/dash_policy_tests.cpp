#include "dash_policy.h"
#include "../../shared/host/type_name_contract.h"
#include <cstdio>
#include <cstdlib>
#include <limits>
using namespace BetterEndfield::Actions;
#define CHECK(x) do { if (!(x)) { std::fprintf(stderr, "line %d: %s\n", __LINE__, #x); std::exit(1); } } while (false)
int main() {
    Frame replay;
    replay.allowed = replay.current_special = true;
    replay.length = 208.0f / 60.0f;
    replay.delta = 1.0f / 60.0f;
    LoopSchedule schedule;
    replay.time = .4f;
    CHECK(schedule.Update(true, replay) == LoopAction::None); // Preserve initial entry.
    replay.time = kLoopBegin;
    CHECK(schedule.Update(false, replay) == LoopAction::None);
    CHECK(schedule.Update(true, replay) == LoopAction::Blend);
    CHECK(std::fabs(LoopSchedule::TargetAt(replay.time) * replay.length - 80.0f / 60.0f) < 1e-5f);
    CHECK(std::fabs((LoopSchedule::TargetAt(replay.time) + kLoopBlend) * replay.length - 98.0f / 60.0f) < 1e-5f);
    CHECK(std::fabs(kLoopPeriod * replay.length - 56.0f / 60.0f) < 1e-5f);
    for (int cycle = 0; cycle < 100; ++cycle) {
        replay.time = kLoopBegin + .003f; // Different frame pacing preserves phase difference.
        CHECK(schedule.Update(true, replay) == LoopAction::Blend);
        CHECK(std::fabs(replay.time - LoopSchedule::TargetAt(replay.time) - kLoopPeriod) < 1e-5f);
        schedule.Submitted(replay.time);
        CHECK(schedule.Update(true, replay) == LoopAction::None); // No immediate acknowledgment.
        replay.transitioning = replay.entering_special = true;
        for (int i = 0; i < 18; ++i) {
            replay.time += replay.delta / replay.length;
            CHECK(schedule.Update(true, replay) == LoopAction::None);
        }
        replay.time -= kLoopPeriod;
        replay.transitioning = replay.entering_special = false;
        CHECK(schedule.Update(true, replay) == LoopAction::None);
        CHECK(!schedule.pending);
    }
    replay.time = kLoopBegin;
    schedule.Submitted(replay.time);
    replay.delta = .25f;
    LoopAction action = LoopAction::None;
    for (int i = 0; i < 12 && action != LoopAction::Abort; ++i) action = schedule.Update(true, replay);
    CHECK(action == LoopAction::Abort); // Rejected native self-fade cannot spam/hold forever.
    schedule = {};
    replay.time = kLoopBegin;
    schedule.Submitted(replay.time);
    replay.time = .087f; // Actual v8 log: intro restart falsely accepted as a loop.
    CHECK(schedule.Update(true, replay) == LoopAction::Abort);
    schedule = {};
    replay.time = kLoopBegin;
    schedule.Submitted(replay.time);
    replay.time = (98.0f / 208.0f) + .003f;
    CHECK(schedule.Update(true, replay) == LoopAction::None);
    CHECK(!schedule.pending); // Correct mid-clip landing is acknowledged.
    schedule = {};
    replay.delta = .016f;
    replay.outgoing = replay.transitioning = replay.returning_to_locomotion = true;
    CHECK(schedule.Update(true, replay) == LoopAction::Abort); // Even Sprint exits win once started.
    replay.outgoing = replay.transitioning = false;
    replay.allowed = false;
    CHECK(schedule.Update(true, replay) == LoopAction::Abort);
    replay.allowed = true; replay.current_special = false; replay.entering_special = true;
    CHECK(schedule.Update(true, replay) == LoopAction::None); // Old Dash still blending into SpDash.
    replay.entering_special = false;
    CHECK(schedule.Update(true, replay) == LoopAction::Abort);
    replay.current_special = true; replay.time = .9f;
    CHECK(schedule.Update(true, replay) == LoopAction::Abort); // Never restart a missed/outro clip.
    replay.time = std::numeric_limits<float>::quiet_NaN();
    CHECK(schedule.Update(true, replay) == LoopAction::Abort);
    replay.time = kLoopBegin; replay.length = 0;
    CHECK(schedule.Update(true, replay) == LoopAction::Abort);
    CHECK(IsTargetHide(11, 0, false, 2.4f));
    CHECK(!IsTargetHide(4, 0, true, 0));
    CHECK(!IsTargetHide(11, 1, false, 2.4f));
    CHECK(!IsTargetHide(11, 0, true, 2.4f));
    CHECK(!IsTargetHide(11, 0, false, 1.4f));
    CHECK(!IsTargetHide(11, 0, false, std::numeric_limits<float>::quiet_NaN()));
    using BetterEndfield::Host::SameTypeText;
    CHECK(SameTypeText("Outer/Inner", "Outer.Inner"));
    CHECK(!SameTypeText("Outer+Inner", "Outer.Inner&"));
    CHECK(!SameTypeText(nullptr, "System.Int32"));
    // Only an owned, active perform's natural End track completion may be deferred.
    CHECK(CanDeferNaturalEnd(true, true, true, true, false, true, true));
    CHECK(!CanDeferNaturalEnd(false, true, true, true, false, true, true));
    CHECK(!CanDeferNaturalEnd(true, false, true, true, false, true, true));
    CHECK(!CanDeferNaturalEnd(true, true, false, true, false, true, true));
    CHECK(!CanDeferNaturalEnd(true, true, true, false, false, true, true));
    CHECK(!CanDeferNaturalEnd(true, true, true, true, true, true, true));
    CHECK(!CanDeferNaturalEnd(true, true, true, true, false, false, true));
    CHECK(!CanDeferNaturalEnd(true, true, true, true, false, true, false));
    // Regression: user's log showed ordinary Dash_L/R, outgoing=1, before entry.
    DashPolicy policy;
    Frame f{true, false, false, true, true, .366f, .016f, 2.333f};
    CHECK(policy.Update(f) == Decision::Wait);
    CHECK(policy.WithinEntryWindow());
    f.entering_special = true; f.outgoing = false;
    CHECK(policy.Update(f) == Decision::Keep);
    CHECK(policy.active);
    // A selected-side clip must not expire, seek, or get interrupted by progress wrap.
    for (int i = 0; i < 10000; ++i) {
        f.current_special = true;
        f.transitioning = i % 8 == 0;
        f.entering_special = f.transitioning;
        f.time = (i % 50) * .02f;
        CHECK(policy.Update(f) == Decision::Keep);
    }
    f.delta = 0;
    CHECK(policy.Update(f) == Decision::Keep);
    f.allowed = false;
    CHECK(policy.Update(f) == Decision::Cancel);
    f.allowed = true; f.outgoing = true;
    CHECK(policy.Update(f) == Decision::Cancel);
    f.outgoing = false; f.current_special = false; f.entering_special = false;
    CHECK(policy.Update(f) == Decision::Cancel);
    policy = {}; f.delta = .25f;
    for (int i = 0; i < 4; ++i) CHECK(policy.Update(f) == Decision::Wait);
    CHECK(policy.Update(f) == Decision::Cancel);
    CHECK(!policy.WithinEntryWindow());
    f.delta = std::numeric_limits<float>::quiet_NaN();
    CHECK(policy.Update(f) == Decision::Cancel);
    CHECK(IsDashOrGroundSprint(true, false, false, true, false));
    CHECK(IsDashOrGroundSprint(false, true, true, true, false));
    CHECK(!IsDashOrGroundSprint(false, true, true, false, false));
    CHECK(!IsDashOrGroundSprint(false, false, true, true, false));
    CHECK(!IsDashOrGroundSprint(true, false, true, true, true));
    CHECK(!IsDashOrGroundSprint(false, true, false, true, false));
    std::puts("Actions native-lifetime policy: all checks passed.");
}
