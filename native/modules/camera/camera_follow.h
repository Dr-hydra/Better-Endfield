#pragma once

#include <cmath>
#include <cstdint>

namespace BetterEndfieldNext::CameraFollow {

// Identities are compared only. Never retain or dereference a game object here.
template<class Point> class Anchor {
public:
    void Reset() { character_ = model_ = 0; valid_ = false; }

    bool Sample(bool enabled, bool manual, uintptr_t character, uintptr_t model,
        bool pose_valid, Point position, Point& delta) {
        delta = {};
        if (!enabled || !manual || !character || !model || !pose_valid ||
            !std::isfinite(position.x) || !std::isfinite(position.y) ||
            !std::isfinite(position.z)) {
            Reset();
            return false;
        }
        if (!valid_ || character != character_ || model != model_) {
            character_ = character;
            model_ = model;
            position_ = position;
            valid_ = true;
            return false;
        }
        const Point step{position.x - position_.x, position.y - position_.y,
            position.z - position_.z};
        position_ = position;
        const double distance_squared = static_cast<double>(step.x) * step.x +
            static_cast<double>(step.y) * step.y + static_cast<double>(step.z) * step.z;
        // A discontinuity rebases the anchor and leaves the user's camera still.
        if (!std::isfinite(distance_squared) || distance_squared > 50.0 * 50.0)
            return false;
        delta = step;
        return true;
    }

private:
    uintptr_t character_ = 0, model_ = 0;
    bool valid_ = false;
    Point position_{};
};

} // namespace BetterEndfieldNext::CameraFollow
