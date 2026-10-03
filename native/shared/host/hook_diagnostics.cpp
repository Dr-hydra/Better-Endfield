#include "hook_diagnostics.h"

#include "../third_party/minhook/src/hde/hde64.h"

#include <algorithm>
#include <cstdio>
#include <cstring>

namespace BetterEndfield::Host::HookDiagnostics {
namespace {

constexpr size_t kLeafScanLimit = 128;

// A jump destination counts as a function only if it is not inside another
// function and not a chained (cold-split) fragment of the current one. Leaf
// functions have no unwind data; accept those only after int3 padding, which
// MSVC and clang place between functions but not before a jump label.
bool IsFunctionEntry(uintptr_t address) {
    DWORD64 image_base = 0;
    const auto* entry = RtlLookupFunctionEntry(address, &image_base, nullptr);
    if (!entry) {
        return *reinterpret_cast<const uint8_t*>(address - 1) == 0xCC;
    }
    if (image_base + entry->BeginAddress != address || (entry->UnwindData & 1) != 0) {
        return false;
    }
    const auto* info = reinterpret_cast<const uint8_t*>(image_base + entry->UnwindData);
    return ((info[0] >> 3) & UNW_FLAG_CHAININFO) == 0;
}

// Returns the first jmp leaving [begin, end) for a function entry. A leaf has
// no unwind range: end is only a scan limit, the scan stops at the first ret,
// int3 or unconditional jmp, and that jmp must leave the bytes scanned so far.
uintptr_t FindTailTarget(uintptr_t begin, uintptr_t end, bool leaf) {
    __try {
        for (uintptr_t at = begin; at < end;) {
            hde64s instruction{};
            const unsigned length = hde64_disasm(reinterpret_cast<const void*>(at), &instruction);
            if (length == 0 || (instruction.flags & F_ERROR) != 0) {
                return 0;
            }
            const uint8_t opcode = instruction.opcode;
            if (opcode == 0xE9 || opcode == 0xEB) {
                const intptr_t displacement = opcode == 0xE9
                    ? static_cast<int32_t>(instruction.imm.imm32)
                    : static_cast<int8_t>(instruction.imm.imm8);
                const uintptr_t destination = at + length + displacement;
                const uintptr_t limit = leaf ? at + length : end;
                if ((destination < begin || destination >= limit) && IsFunctionEntry(destination)) {
                    return destination;
                }
            }
            const bool indirect_jump = opcode == 0xFF && instruction.modrm_reg == 4;
            if (leaf && (opcode == 0xC3 || opcode == 0xC2 || opcode == 0xCC ||
                    opcode == 0xE9 || opcode == 0xEB || indirect_jump)) {
                return 0;
            }
            at += length;
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
    }
    return 0;
}

std::string ModuleName(HMODULE module) {
    char path[MAX_PATH]{};
    const DWORD length = GetModuleFileNameA(module, path, MAX_PATH);
    if (length == 0 || length >= MAX_PATH) {
        return {};
    }
    const char* name = std::max(std::strrchr(path, '\\'), std::strrchr(path, '/'));
    return name ? name + 1 : path;
}

} // namespace

NativeShape Describe(void* target) {
    NativeShape shape;
    if (!target) {
        return shape;
    }
    const auto address = reinterpret_cast<uintptr_t>(target);
    if (GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
            GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            static_cast<LPCWSTR>(target), &shape.module)) {
        shape.module_name = ModuleName(shape.module);
        shape.rva = address - reinterpret_cast<uintptr_t>(shape.module);
    }
    DWORD64 image_base = 0;
    const auto* entry = RtlLookupFunctionEntry(address, &image_base, nullptr);
    uintptr_t end = address + kLeafScanLimit;
    if (entry) {
        end = image_base + entry->EndAddress;
        shape.size = entry->EndAddress - entry->BeginAddress;
    } else {
        shape.leaf = true;
    }
    shape.tail_target = reinterpret_cast<void*>(FindTailTarget(address, end, shape.leaf));
    return shape;
}

std::string FormatAddress(void* address) {
    HMODULE module = nullptr;
    char text[160]{};
    if (address && GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
            GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            static_cast<LPCWSTR>(address), &module)) {
        std::snprintf(text, sizeof(text), "%s+0x%llX", ModuleName(module).c_str(),
            static_cast<unsigned long long>(reinterpret_cast<uintptr_t>(address) -
                reinterpret_cast<uintptr_t>(module)));
    } else {
        std::snprintf(text, sizeof(text), "0x%llX",
            static_cast<unsigned long long>(reinterpret_cast<uintptr_t>(address)));
    }
    return text;
}

std::unordered_map<uintptr_t, uint32_t> CountRel32References(const uint8_t* code,
    size_t size, uintptr_t code_address, const std::vector<uintptr_t>& targets) {
    std::unordered_map<uintptr_t, uint32_t> counts;
    if (targets.empty()) {
        return counts;
    }
    std::vector<uintptr_t> sorted = targets;
    std::sort(sorted.begin(), sorted.end());
    for (const uintptr_t target : sorted) {
        counts[target] = 0;
    }
    const uintptr_t lowest = sorted.front();
    const uintptr_t highest = sorted.back();
    for (size_t index = 0; index + 5 <= size; ++index) {
        if (code[index] != 0xE8 && code[index] != 0xE9) {
            continue;
        }
        int32_t displacement = 0;
        std::memcpy(&displacement, code + index + 1, sizeof(displacement));
        const uintptr_t destination = code_address + index + 5 + displacement;
        if (destination >= lowest && destination <= highest &&
            std::binary_search(sorted.begin(), sorted.end(), destination)) {
            ++counts[destination];
        }
    }
    return counts;
}

std::unordered_map<uintptr_t, uint32_t> CountDirectReferences(HMODULE module,
    const std::vector<uintptr_t>& targets) {
    std::unordered_map<uintptr_t, uint32_t> totals;
    for (const uintptr_t target : targets) {
        totals[target] = 0;
    }
    if (!module || targets.empty()) {
        return totals;
    }
    const auto* base = reinterpret_cast<const uint8_t*>(module);
    const auto* dos = reinterpret_cast<const IMAGE_DOS_HEADER*>(base);
    if (dos->e_magic != IMAGE_DOS_SIGNATURE) {
        return totals;
    }
    const auto* nt = reinterpret_cast<const IMAGE_NT_HEADERS*>(base + dos->e_lfanew);
    if (nt->Signature != IMAGE_NT_SIGNATURE) {
        return totals;
    }
    const auto* section = IMAGE_FIRST_SECTION(nt);
    for (WORD index = 0; index < nt->FileHeader.NumberOfSections; ++index, ++section) {
        if ((section->Characteristics & IMAGE_SCN_MEM_EXECUTE) == 0) {
            continue;
        }
        const uint8_t* at = base + section->VirtualAddress;
        const uint8_t* end = at + section->Misc.VirtualSize;
        // Only scan committed, readable pages of the section.
        while (at < end) {
            MEMORY_BASIC_INFORMATION region{};
            if (VirtualQuery(at, &region, sizeof(region)) == 0) {
                break;
            }
            const auto* region_end = std::min(end,
                static_cast<const uint8_t*>(region.BaseAddress) + region.RegionSize);
            const DWORD protect = region.Protect & 0xFF;
            const bool readable = region.State == MEM_COMMIT &&
                (region.Protect & PAGE_GUARD) == 0 && protect != PAGE_NOACCESS &&
                protect != PAGE_EXECUTE;
            if (readable) {
                for (const auto& [target, count] : CountRel32References(at,
                         static_cast<size_t>(region_end - at),
                         reinterpret_cast<uintptr_t>(at), targets)) {
                    totals[target] += count;
                }
            }
            at = region_end;
        }
    }
    return totals;
}

} // namespace BetterEndfield::Host::HookDiagnostics
