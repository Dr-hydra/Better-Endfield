#pragma once
#include <algorithm>
#include <cmath>
#include <cstdint>
#include <mutex>

namespace betterendfield {

struct AndroidPcMouseSnapshot {
    bool requested = false, captured = false;
    uint64_t motion_events = 0, axis_reads = 0;
    float x = 0.0f, y = 0.0f;
};

// UI-thread pointer events and game-thread axis reads meet here. A frame holds
// one snapshot so repeated X/Y reads do not consume one another's movement.
class AndroidPcMouseState {
public:
    void Publish(bool enabled, bool ready) {
        std::lock_guard lock(mutex_);
        enabled_ = enabled;
        ready_ = ready;
        if (!Requested()) ClearCapture();
    }
    void CursorRequest(bool show) {
        std::lock_guard lock(mutex_);
        cursor_known_ = true;
        cursor_show_ = show;
        if (!Requested()) ClearCapture();
    }
    void Foreground(bool visible) {
        std::lock_guard lock(mutex_);
        foreground_ = visible;
        if (!visible) ClearCapture();
    }
    bool CaptureRequested() {
        std::lock_guard lock(mutex_);
        return Requested();
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
        return {Requested(), captured_, motion_events_, axis_reads_, snapshot_x_, snapshot_y_};
    }
    void Reset() {
        std::lock_guard lock(mutex_);
        enabled_ = ready_ = cursor_known_ = false;
        cursor_show_ = true;
        motion_events_ = axis_reads_ = 0;
        ClearCapture();
    }
private:
    bool Requested() const {
        return enabled_ && ready_ && foreground_ && cursor_known_ && !cursor_show_;
    }
    void ClearCapture() {
        captured_ = have_snapshot_ = false;
        pending_x_ = pending_y_ = snapshot_x_ = snapshot_y_ = 0.0f;
    }
    std::mutex mutex_;
    bool enabled_ = false, ready_ = false, foreground_ = true;
    bool cursor_known_ = false, cursor_show_ = true, captured_ = false;
    bool have_snapshot_ = false;
    uint64_t frame_ = 0, sampled_frame_ = 0;
    uint64_t motion_events_ = 0, axis_reads_ = 0;
    float pending_x_ = 0.0f, pending_y_ = 0.0f;
    float snapshot_x_ = 0.0f, snapshot_y_ = 0.0f;
};

void PublishAndroidPcMouse(bool enabled, bool ready);
void SetAndroidPcCursorRequest(bool show);
bool AndroidPcMouseCaptureRequested();
void SetAndroidPcMouseCaptured(bool captured);
void AddAndroidPcMouseMotion(float x, float y);
bool ReadAndroidPcMouseMotion(float& x, float& y);
void ResetAndroidPcMouse();
AndroidPcMouseSnapshot InspectAndroidPcMouse();

} // namespace betterendfield
