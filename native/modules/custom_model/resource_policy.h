#pragma once
#include <cstddef>
#include <span>
#include <string_view>

namespace BetterEndfieldNext::CustomModel {
constexpr bool EffectiveLodEnabled(bool has_enabled_mod, bool standalone_enabled) {
    return has_enabled_mod || standalone_enabled;
}

// Kept for source compatibility with diagnostics; matching uses the actual
// sharedMesh/pristine identity and a checked ReceiverKey, never a role alias.
constexpr std::string_view ComponentRendererName(std::string_view,std::string_view mesh_name) {
    return mesh_name;
}

enum class CommitResult { Committed, Restored, RestoreFailed };

// A transaction begins only after every asset and binding has been prepared.
// Include the failing receiver in rollback: an engine setter may mutate and
// then raise an exception or fail its readback. Always attempt every restoration.
template<class Binding, class Apply, class Restore>
CommitResult CommitResource(std::span<Binding> bindings, Apply&& apply, Restore&& restore) {
    size_t attempted = 0;
    try {
        for (;attempted<bindings.size();++attempted) {
            if (!apply(bindings[attempted])) break;
        }
        if (attempted == bindings.size()) return CommitResult::Committed;
    } catch (...) {
        // C++ exceptions during publication have the same restore semantics.
    }
    bool restored = true;
    size_t remaining = attempted < bindings.size() ? attempted + 1 : bindings.size();
    while (remaining) {
        try { if (!restore(bindings[--remaining])) restored = false; }
        catch (...) { restored = false; }
    }
    return restored ? CommitResult::Restored : CommitResult::RestoreFailed;
}
}
