#include "native_mesh_layout.h"
#include <Windows.h>
#include <algorithm>
#include <array>
#include <cstring>
#include <map>
#include <set>
#include <vector>
#include "../../shared/third_party/minhook/src/hde/hde64.h"

namespace BetterEndfield::CustomModel {
namespace {
template<class T> bool Read(std::span<const uint8_t> image, size_t offset, T& out) {
    if (offset > image.size() || sizeof(T) > image.size() - offset) return false;
    std::memcpy(&out, image.data() + offset, sizeof(T));
    return true;
}
bool Range(std::span<const uint8_t> image, size_t start, size_t length) {
    return start <= image.size() && length <= image.size() - start;
}
struct Instruction { uint32_t rva; hde64s decoded; };
bool ArchiveArgument(const hde64s& instruction) {
    return instruction.opcode == 0x8b && instruction.rex_w && !instruction.rex_r &&
        instruction.modrm_mod == 3 && instruction.modrm_reg == 1;
}
bool ZeroFlags(const hde64s& instruction) {
    return instruction.opcode == 0x33 && !instruction.rex_w && instruction.rex_r &&
        instruction.rex_b && instruction.modrm_mod == 3 &&
        instruction.modrm_reg == 1 && instruction.modrm_rm == 1;
}

// Require a decoded LEA RDX,[nonvolatile Mesh register + displacement],
// an unmodified RDX until LEA R8,[field name], then the serialization call.
bool FieldAddress(const std::array<Instruction, 4>& previous, size_t count, uint32_t& offset) {
    if (count < 2) return false;
    const auto& address = previous[(count - 2) % previous.size()].decoded;
    const auto& context = previous[(count - 1) % previous.size()].decoded;
    if (address.opcode != 0x8d || !address.rex_w || address.rex_r ||
        address.modrm_reg != 2 || address.modrm_rm == 4 ||
        (address.modrm_mod != 1 && address.modrm_mod != 2)) return false;
    const auto base = address.modrm_rm + (address.rex_b ? 8 : 0);
    if (base != 3 && base != 6 && base != 7 && base != 13 && base != 14 && base != 15) return false;
    // Read/write serializers arrange the archive and flags arguments in
    // different orders; neither permitted instruction modifies RDX.
    if (!ArchiveArgument(context) && !ZeroFlags(context)) return false;
    const int32_t displacement = address.modrm_mod == 1
        ? static_cast<int8_t>(address.disp.disp8) : static_cast<int32_t>(address.disp.disp32);
    if (displacement <= 0 || displacement > 4096 || displacement % alignof(uint32_t)) return false;
    offset = static_cast<uint32_t>(displacement);
    return true;
}
}

bool ResolveNativeMeshLayout(std::span<const uint8_t> image,
    NativeMeshLayout& layout, std::string& error) {
    layout = {}; error.clear();
    const auto fail = [&](const char* why) { error = why; return false; };
    IMAGE_DOS_HEADER dos{};
    IMAGE_NT_HEADERS64 nt{};
    if (!Read(image, 0, dos) || dos.e_magic != IMAGE_DOS_SIGNATURE || dos.e_lfanew <= 0 ||
        !Read(image, static_cast<size_t>(dos.e_lfanew), nt) || nt.Signature != IMAGE_NT_SIGNATURE ||
        nt.FileHeader.Machine != IMAGE_FILE_MACHINE_AMD64 ||
        nt.OptionalHeader.Magic != IMAGE_NT_OPTIONAL_HDR64_MAGIC ||
        nt.OptionalHeader.SizeOfImage > image.size() || nt.FileHeader.NumberOfSections > 96 ||
        nt.OptionalHeader.NumberOfRvaAndSizes <= IMAGE_DIRECTORY_ENTRY_EXCEPTION)
        return fail("Unsupported or truncated UnityPlayer PE image.");
    const auto directory = nt.OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXCEPTION];
    if (!directory.VirtualAddress || directory.Size % sizeof(RUNTIME_FUNCTION) ||
        !Range(image, directory.VirtualAddress, directory.Size))
        return fail("Missing native function boundaries.");

    constexpr char field_name[] = "m_BonesPerVertex";
    std::set<uint32_t> names;
    std::vector<IMAGE_SECTION_HEADER> executable;
    const size_t section_start = static_cast<size_t>(dos.e_lfanew) + sizeof(DWORD) +
        sizeof(IMAGE_FILE_HEADER) + nt.FileHeader.SizeOfOptionalHeader;
    for (uint16_t n = 0; n < nt.FileHeader.NumberOfSections; ++n) {
        IMAGE_SECTION_HEADER section{};
        if (!Read(image, section_start + n * sizeof(section), section) ||
            !Range(image, section.VirtualAddress, section.Misc.VirtualSize))
            return fail("Invalid PE section bounds.");
        if (section.Characteristics & IMAGE_SCN_MEM_EXECUTE) executable.push_back(section);
        else if (section.Characteristics & IMAGE_SCN_MEM_READ) {
            const size_t start = section.VirtualAddress;
            const size_t end = start + section.Misc.VirtualSize;
            for (size_t at = start; at + sizeof(field_name) <= end; ++at) {
                if ((at == start || image[at - 1] == 0) &&
                    std::memcmp(image.data() + at, field_name, sizeof(field_name)) == 0)
                    names.insert(static_cast<uint32_t>(at));
            }
        }
    }
    if (names.empty()) return fail("Mesh serialization field name is unavailable.");

    // First find RIP-relative name references. Only then decode their enclosing
    // functions, so normal startup does not disassemble the entire player.
    std::set<uint32_t> references;
    for (const auto& section : executable) {
        const size_t end = static_cast<size_t>(section.VirtualAddress) + section.Misc.VirtualSize;
        for (size_t at = section.VirtualAddress; at + 7 <= end; ++at) {
            if (image[at] != 0x4c || image[at + 1] != 0x8d || image[at + 2] != 0x05) continue;
            int32_t displacement = 0;
            Read(image, at + 3, displacement);
            const int64_t target = static_cast<int64_t>(at + 7) + displacement;
            if (target >= 0 && target < static_cast<int64_t>(image.size()) &&
                names.contains(static_cast<uint32_t>(target))) references.insert(static_cast<uint32_t>(at));
        }
    }
    std::map<uint32_t, std::set<uint32_t>> candidates;
    for (size_t at = directory.VirtualAddress; at < static_cast<size_t>(directory.VirtualAddress) + directory.Size;
            at += sizeof(RUNTIME_FUNCTION)) {
        RUNTIME_FUNCTION function{};
        if (!Read(image, at, function) || function.EndAddress <= function.BeginAddress ||
            !Range(image, function.BeginAddress, function.EndAddress - function.BeginAddress))
            return fail("Invalid native function boundary.");
        const auto first = references.lower_bound(function.BeginAddress);
        if (first == references.end() || *first >= function.EndAddress) continue;
        if (function.EndAddress - function.BeginAddress > 65536) continue;
        if (std::none_of(executable.begin(), executable.end(), [&](const auto& section) {
                return function.BeginAddress >= section.VirtualAddress &&
                    static_cast<uint64_t>(function.EndAddress) <=
                        static_cast<uint64_t>(section.VirtualAddress) + section.Misc.VirtualSize;
            })) continue;
        std::array<Instruction, 4> previous{};
        size_t count = 0;
        for (uint32_t ip = function.BeginAddress; ip < function.EndAddress;) {
            std::array<uint8_t, 16> bytes{};
            const auto available = std::min<size_t>(bytes.size(), function.EndAddress - ip);
            std::memcpy(bytes.data(), image.data() + ip, available);
            hde64s decoded{};
            hde64_disasm(bytes.data(), &decoded);
            if (!decoded.len || decoded.flags & F_ERROR || decoded.len > available) break;
            if (references.contains(ip) && decoded.len == 7) {
                uint32_t call = ip + decoded.len;
                if (Range(image, call, 16) && call + 3 < function.EndAddress) {
                    hde64s context{};
                    hde64_disasm(image.data() + call, &context);
                    if (!(context.flags & F_ERROR) && ArchiveArgument(context)) call += context.len;
                }
                uint32_t offset = 0;
                if (call + 5 <= function.EndAddress && image[call] == 0xe8 &&
                    FieldAddress(previous, count, offset)) candidates[offset].insert(function.BeginAddress);
            }
            previous[count++ % previous.size()] = {ip, decoded};
            ip += decoded.len;
        }
    }
    if (candidates.size() != 1 || candidates.begin()->second.size() < 2)
        return fail("Mesh field layout lacks two agreeing serializers or is ambiguous.");
    layout.bones_per_vertex_offset = candidates.begin()->first;
    layout.agreeing_serializers = static_cast<uint32_t>(candidates.begin()->second.size());
    return true;
}
}
