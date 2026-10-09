#pragma once
#include <string_view>
#include <vector>

namespace BetterEndfieldNext::CustomModel {
enum class TexturePinStatus { Matched, Missing, Ambiguous };

template<class Slot> struct TexturePinMatch {
    TexturePinStatus status=TexturePinStatus::Missing;
    std::vector<const Slot*> slots;
};

// Several shader properties can refer to the same original Texture object.
// Different objects with the same name are ambiguous in validated mode.
template<class Slot, class Name>
TexturePinMatch<Slot> MatchTexturePins(const std::vector<Slot>& slots,
    std::string_view expected,bool skip_validation,Name&& object_name) {
    TexturePinMatch<Slot> result;
    for (const auto& slot:slots) {
        if (object_name(slot.texture)!=expected) continue;
        if (!skip_validation && !result.slots.empty() && result.slots.front()->texture!=slot.texture) {
            result.status=TexturePinStatus::Ambiguous;
            return result;
        }
        result.slots.push_back(&slot);
    }
    if (!result.slots.empty()) result.status=TexturePinStatus::Matched;
    return result;
}
}
