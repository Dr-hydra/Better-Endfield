#pragma once

#include <Windows.h>

#include <cstddef>
#include <cstdint>
#include <string>
#include <unordered_map>
#include <vector>

namespace BetterEndfield::Host::HookDiagnostics {

// Static shape of a hook target, read before it is patched. Name resolution
// proves the method exists; it does not prove the game still calls this copy.
// The compiler may inline a thin wrapper into its callers (the PC InitMainPathHash
// regression) or fold identical bodies into one address.
struct NativeShape {
    HMODULE module = nullptr;
    std::string module_name;
    uintptr_t rva = 0;
    uint32_t size = 0;        // Primary unwind range; 0 when unknown.
    bool leaf = false;        // No unwind entry: a leaf function or a thunk.
    void* tail_target = nullptr; // Function entered by a tail jump, if any.
};

NativeShape Describe(void* target);
std::string FormatAddress(void* address);

// Counts rel32 call/jmp instructions (E8/E9) in [code, code + size) whose
// destination is one of targets. The byte scan does not decode instructions,
// so it is an estimate; a zero for a non-virtual method is still a strong sign
// that every caller uses an inlined copy. code_address is the runtime address
// of code[0].
std::unordered_map<uintptr_t, uint32_t> CountRel32References(const uint8_t* code,
    size_t size, uintptr_t code_address, const std::vector<uintptr_t>& targets);
// The same scan over every executable section of module.
std::unordered_map<uintptr_t, uint32_t> CountDirectReferences(HMODULE module,
    const std::vector<uintptr_t>& targets);

} // namespace BetterEndfield::Host::HookDiagnostics
