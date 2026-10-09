#pragma once
#include <algorithm>
#include <cmath>
#include <cstdint>
#include <mutex>

namespace betterendfieldnext {

struct AndroidPcMouseSnapshot {
    bool requested = false, captured = false;
    uint64_t motion_events = 0, axis_reads = 0;
    float x = 0.0f, y = 0.0f;
    uint64_t absolute_events = 0, position_reads = 0;
    bool absolute_valid = false;
};

// UI-thread pointer events and game-thread axis reads meet here. A frame holds
// one snapshot so repeated X/Y reads do not consume one another's movement.
class AndroidPcMouseState {
public:
    void Publish(bool enabled, bool ready) {
        std::lock_guard lock(mutex_);
        const bool changed = enabled_ != enabled || ready_ != ready;
        enabled_ = enabled;
        ready_ = ready;
        if (changed || !enabled_ || !ready_) { direct_touch_ = false; ClearCapture(); }
    }
    void CursorRequest(bool show) {
        std::lock_guard lock(mutex_);
        const bool changed = !cursor_known_ || cursor_show_ != show;
        cursor_known_ = true;
        cursor_show_ = show;
        if (changed || (captured_ && !Requested())) ClearCapture();
    }
    void Foreground(bool visible) {
        std::lock_guard lock(mutex_);
        foreground_ = visible;
        if (!visible) { direct_touch_ = false; ClearCapture(); }
    }
    bool CaptureRequested() {
        std::lock_guard lock(mutex_);
        return Requested();
    }
    int CursorMode() {
        std::lock_guard lock(mutex_);
        if (!enabled_ || !ready_ || !foreground_ || !cursor_known_) return 0;
        return cursor_show_ ? 2 : 1;
    }
    void Absolute(float x, float y) {
        if (!std::isfinite(x) || !std::isfinite(y)) return;
        std::lock_guard lock(mutex_);
        if (!enabled_ || !ready_ || !foreground_ || !cursor_known_ || !cursor_show_ || captured_ || direct_touch_) return;
        absolute_x_ = x;
        absolute_y_ = y;
        have_absolute_ = true;
        ++absolute_events_;
    }
    void DirectTouch(bool active) {
        std::lock_guard lock(mutex_);
        direct_touch_ = active;
        // A real finger keeps Unity's touch-derived position authoritative.
        have_absolute_ = false;
    }
    bool ReadAbsolute(float& x, float& y) {
        std::lock_guard lock(mutex_);
        if (!enabled_ || !ready_ || !foreground_ || !cursor_known_ || !cursor_show_ ||
            captured_ || direct_touch_ || !have_absolute_) return false;
        x = absolute_x_;
        y = absolute_y_;
        ++position_reads_;
        return true;
    }
    void Captured(bool captured) {
        std::lock_guard lock(mutex_);
        const bool active = captured && Requested();
        if (captured_ == active) return;
        ClearCapture();
        captured_ = active;
    }
    void Motion(float x, float y) {
        if (!std::isfinite(x) || !std::isfinite(y)) return;
        std::lock_guard lock(mutex_);
        if (!captured_ || !Requested()) return;
        ++motion_events_;
        pending_x_ = std::clamp(pending_x_ + x, -1000000.0f, 1000000.0f);
        pending_y_ = std::clamp(pending_y_ + y, -1000000.0f, 1000000.0f);
    }
    void NextFrame() {
        std::lock_guard lock(mutex_);
        ++frame_;
    }
    bool Read(float& x, float& y) {
        std::lock_guard lock(mutex_);
        if (!captured_ || !Requested()) return false;
        ++axis_reads_;
        if (!have_snapshot_ || sampled_frame_ != frame_) {
            snapshot_x_ = pending_x_;
            snapshot_y_ = pending_y_;
            pending_x_ = pending_y_ = 0.0f;
            sampled_frame_ = frame_;
            have_snapshot_ = true;
        }
        x = snapshot_x_;
        y = snapshot_y_;
        return true;
    }
    AndroidPcMouseSnapshot Inspect() {
        std::lock_guard lock(mutex_);
        return {Requested(), captured_, motion_events_, axis_reads_, snapshot_x_, snapshot_y_,
            absolute_events_, position_reads_, have_absolute_};
    }
    void Reset() {
        std::lock_guard lock(mutex_);
        enabled_ = ready_ = cursor_known_ = false;
        direct_touch_ = false;
        cursor_show_ = true;
        motion_events_ = axis_reads_ = 0;
        absolute_events_ = position_reads_ = 0;
        ClearCapture();
    }
private:
    bool Requested() const {
        return enabled_ && ready_ && foreground_ && cursor_known_ && !cursor_show_;
    }
    void ClearCapture() {
        captured_ = have_snapshot_ = false;
        have_absolute_ = false;
        pending_x_ = pending_y_ = snapshot_x_ = snapshot_y_ = 0.0f;
    }
    std::mutex mutex_;
    bool enabled_ = false, ready_ = false, foreground_ = true;
    bool cursor_known_ = false, cursor_show_ = true, captured_ = false;
    bool have_snapshot_ = false;
    bool have_absolute_ = false, direct_touch_ = false;
    float absolute_x_ = 0.0f, absolute_y_ = 0.0f;
    uint64_t frame_ = 0, sampled_frame_ = 0;
    uint64_t motion_events_ = 0, axis_reads_ = 0;
    uint64_t absolute_events_ = 0, position_reads_ = 0;
    float pending_x_ = 0.0f, pending_y_ = 0.0f;
    float snapshot_x_ = 0.0f, snapshot_y_ = 0.0f;
};

void PublishAndroidPcMouse(bool enabled, bool ready);
void SetAndroidPcCursorRequest(bool show);
bool AndroidPcMouseCaptureRequested();
int AndroidPcCursorMode();
void AddAndroidPcMouseAbsolute(float x, float y);
void SetAndroidPcDirectTouch(bool active);
bool ReadAndroidPcMouseAbsolute(float& x, float& y);
void SetAndroidPcMouseCaptured(bool captured);
void AddAndroidPcMouseMotion(float x, float y);
bool ReadAndroidPcMouseMotion(float& x, float& y);
void ResetAndroidPcMouse();
AndroidPcMouseSnapshot InspectAndroidPcMouse();

} // namespace betterendfieldnext
