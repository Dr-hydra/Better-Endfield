#include "mesh_layout_probe.h"

#include <algorithm>
#include <array>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <link.h>
#include <map>
#include <set>
#include <string>
#include <string_view>
#include <vector>
#include <sys/uio.h>
#include <unistd.h>

namespace betterendfield {
namespace {
constexpr uint32_t kGnuEhFrame = 0x6474e550;

struct Segment {
    uintptr_t start = 0;
    uint32_t flags = 0;
    std::vector<uint8_t> bytes;
};
struct Image {
    uintptr_t base = 0;
    std::vector<Segment> segments;
    std::vector<uintptr_t> functions;
    uintptr_t eh_header = 0;
    size_t eh_size = 0;
    bool failed = false;
    size_t size = 0;
};

MeshLayoutEvidence Failure(const char* message) {
    MeshLayoutEvidence result;
    result.status = message;
    return result;
}
uint32_t Word(const Segment& segment, size_t offset) {
    uint32_t result = 0;
    std::memcpy(&result, segment.bytes.data() + offset, sizeof(result));
    return result;
}
bool IsExecutable(const Image& image, uintptr_t address) {
    return std::any_of(image.segments.begin(), image.segments.end(),
        [address](const Segment& segment) {
            return (segment.flags & PF_X) && address >= segment.start &&
                address - segment.start < segment.bytes.size();
        });
}
uintptr_t FunctionFor(const Image& image, uintptr_t address) {
    const auto it = std::upper_bound(image.functions.begin(), image.functions.end(), address);
    if (it == image.functions.begin()) return 0;
    return *std::prev(it);
}
void ReadEhFrameFunctions(Image& image) {
    if (!image.eh_header || image.eh_size < 12) return;
    std::array<uint8_t, 12> header{};
    if (!ReadOwnMemory(image.eh_header, header.data(), header.size()) ||
            header[0] != 1 || header[1] != 0x1b || header[2] != 0x03 || header[3] != 0x3b) return;
    uint32_t count = 0;
    std::memcpy(&count, header.data() + 8, sizeof(count));
    if (!count || count > 200000 || 12 + static_cast<size_t>(count) * 8 > image.eh_size) return;
    std::vector<uint8_t> table(12 + static_cast<size_t>(count) * 8);
    if (!ReadOwnMemory(image.eh_header, table.data(), table.size())) return;
    image.functions.reserve(count);
    for (uint32_t index = 0; index < count; ++index) {
        int32_t relative = 0;
        std::memcpy(&relative, table.data() + 12 + static_cast<size_t>(index) * 8, sizeof(relative));
        const uintptr_t start = static_cast<uintptr_t>(static_cast<intptr_t>(image.eh_header) + relative);
        if (IsExecutable(image, start)) image.functions.push_back(start);
    }
    std::sort(image.functions.begin(), image.functions.end());
    image.functions.erase(std::unique(image.functions.begin(), image.functions.end()), image.functions.end());
}
struct FieldAccess {
    bool load = false;
    bool store = false;
    uint32_t base = 31;
    uint32_t value_register = 31;
    uint32_t offset = 0;
};
FieldAccess DecodeMemoryAccess(uint32_t instruction) {
    FieldAccess result;
    const uint32_t opcode = instruction & 0xffc00000;
    const bool word = opcode == 0xb9400000 || opcode == 0xb9000000;
    const bool wide = opcode == 0xf9400000 || opcode == 0xf9000000;
    if (!word && !wide) return result;
    const uint32_t scale = word ? 4 : 8;
    result.offset = ((instruction >> 10) & 0xfff) * scale;
    result.load = opcode == 0xb9400000 || opcode == 0xf9400000;
    result.store = opcode == 0xb9000000 || opcode == 0xf9000000;
    result.base = (instruction >> 5) & 31;
    result.value_register = instruction & 31;
    return result;
}
}

bool ReadOwnMemory(uintptr_t address, void* output, size_t size) {
    if (!address || !output || !size || size > UINTPTR_MAX - address) return false;
    iovec local{output, size}, remote{reinterpret_cast<void*>(address), size};
    return process_vm_readv(getpid(), &local, 1, &remote, 1, 0) == static_cast<ssize_t>(size);
}

MeshLayoutEvidence ProbeLoadedUnityMeshLayout() {
    Image image;
    dl_iterate_phdr([](dl_phdr_info* info, size_t, void* opaque) {
        auto& out = *static_cast<Image*>(opaque);
        std::string_view name = info->dlpi_name ? info->dlpi_name : "";
        if (name.substr(name.find_last_of('/') + 1) != "libunity.so") return 0;
        out.base = info->dlpi_addr;
        try {
            for (size_t index = 0; index < info->dlpi_phnum; ++index) {
                const auto& header = info->dlpi_phdr[index];
                if (header.p_type == kGnuEhFrame) {
                    out.eh_header = info->dlpi_addr + header.p_vaddr;
                    out.eh_size = static_cast<size_t>(header.p_memsz);
                    continue;
                }
                if (header.p_type != PT_LOAD || !(header.p_flags & PF_R) || !header.p_filesz) continue;
                if (header.p_filesz > 128 * 1024 * 1024 || out.size + header.p_filesz > 256 * 1024 * 1024 ||
                        header.p_vaddr > UINTPTR_MAX - info->dlpi_addr) { out.failed = true; break; }
                Segment segment{info->dlpi_addr + header.p_vaddr, header.p_flags,
                    std::vector<uint8_t>(static_cast<size_t>(header.p_filesz))};
                if (!ReadOwnMemory(segment.start, segment.bytes.data(), segment.bytes.size())) {
                    out.failed = true; break;
                }
                out.size += segment.bytes.size();
                out.segments.push_back(std::move(segment));
            }
        } catch (...) { out.failed = true; }
        return 1;
    }, &image);
    if (image.failed || image.segments.empty()) return Failure("Unity ELF snapshot unavailable");
    // Debug-only bootstrap supplies this path. Export executable bytes only;
    // no writable segments or game object/asset memory are captured.
    const char* config = std::getenv("BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG");
    const char* diagnostics = std::getenv("BETTER_ENDFIELD_DIAGNOSTICS_PATH");
    if (config && diagnostics && std::string_view(config).find("code_probe=1") != std::string_view::npos) {
        for (const auto& segment : image.segments) {
            if (!(segment.flags & PF_X)) continue;
            const std::string path = std::string(diagnostics) + ".text-" +
                std::to_string(segment.start - image.base) + ".bin";
            if (FILE* output = std::fopen(path.c_str(), "wb")) {
                std::fwrite(segment.bytes.data(), 1, segment.bytes.size(), output);
                std::fclose(output);
            }
        }
    }
    ReadEhFrameFunctions(image);

    constexpr char field[] = "m_BonesPerVertex";
    std::set<uintptr_t> names;
    for (auto& segment : image.segments) {
        if (segment.flags & PF_X) continue;
        auto at = segment.bytes.begin();
        while ((at = std::search(at, segment.bytes.end(), std::begin(field), std::end(field))) != segment.bytes.end()) {
            if (at == segment.bytes.begin() || *(at - 1) == 0)
                names.insert(segment.start + static_cast<size_t>(at - segment.bytes.begin()));
            ++at;
        }
    }
    if (names.empty()) return Failure("named field unavailable");

    std::map<uint32_t, std::set<uintptr_t>> candidates;
    std::set<uintptr_t> descriptor_paths, reader_paths;
    for (auto& segment : image.segments) {
        if (!(segment.flags & PF_X) || segment.start % 4) continue;
        for (size_t at = 0; at + 24 <= segment.bytes.size(); at += 4) {
            const uint32_t page = Word(segment, at), member = Word(segment, at + 4),
                name = Word(segment, at + 8), archive = Word(segment, at + 12);
            if ((page & 0x9f00001f) != 0x90000001 || (member & 0xffc0001f) != 0x91000003 ||
                    (name & 0xffc003ff) != 0x91000021 || (archive & 0xffe0ffff) != 0xaa0003e0 ||
                    Word(segment, at + 16) != 0x2a1f03e4 || (Word(segment, at + 20) & 0xfc000000) != 0x94000000) continue;
            const auto mesh = (member >> 5) & 31, context = (archive >> 16) & 31;
            if (mesh < 19 || mesh > 28 || context < 19 || context > 28 || mesh == context) continue;
            int64_t displacement = ((page >> 29) & 3) | (((page >> 5) & 0x7ffff) << 2);
            if (displacement & (1 << 20)) displacement -= 1 << 21;
            const auto target = static_cast<uintptr_t>(static_cast<int64_t>((segment.start + at) & ~uintptr_t{4095}) +
                displacement * 4096 + ((name >> 10) & 4095));
            const uint32_t field_offset = (member >> 10) & 4095;
            if (names.contains(target) && field_offset && field_offset % 4 == 0) {
                candidates[field_offset].insert(segment.start + at);
                descriptor_paths.insert(segment.start + at);
            }
        }
    }
    // The second named transfer uses a callback slot on the stack; the field
    // address is formed AFTER the archive lookup. It is not a setter icall.
    for (const auto& segment : image.segments) {
        if (!(segment.flags & PF_X) || segment.start % 4) continue;
        for (size_t at = 0; at + 64 <= segment.bytes.size(); at += 4) {
            const auto w = [&](size_t delta) { return Word(segment, at + delta); };
            if ((w(0)&0x9f00001f)!=0x90000001 || (w(4)&0xffc003ff)!=0x91000021 ||
                    (w(8)&0xffc003ff)!=0x910003e3 || (w(12)&0xffe0ffff)!=0xaa0003e0 ||
                    (w(16)&0xffe0ffff)!=0xaa0003e2 || w(20)!=0x2a1f03e4 ||
                    (w(24)&0xfc000000)!=0x94000000 || (w(28)&0xff00001f)!=0x34000000 ||
                    w(32)!=0x7100041f || (w(36)&0xffc00000)!=0x91000000 ||
                    (w(40)&0xff00001f)!=0x5400000a || (w(44)&0xffc003ff)!=0xf94003e8 ||
                    (w(48)&0xff00001f)!=0xb4000008 || (w(52)&0xffe0ffff)!=0xaa0003e0 ||
                    (w(56)&0xffe0ffff)!=0xaa0003e1 || w(60)!=0xd63f0100) continue;
            const uint32_t mesh=w(36)&31, archive=(w(12)>>16)&31;
            const uint32_t offset=(w(36)>>10)&4095;
            if (mesh<19 || mesh>28 || archive<19 || archive>28 || mesh==archive ||
                    ((w(36)>>5)&31)!=mesh || ((w(52)>>16)&31)!=mesh ||
                    ((w(56)>>16)&31)!=archive || !offset || offset%4 ||
                    ((w(8)>>10)&4095)!=(((w(44)>>10)&4095)*8)) continue;
            int64_t delta=((w(0)>>29)&3)|(((w(0)>>5)&0x7ffff)<<2);
            if (delta&(1<<20)) delta-=1<<21;
            const auto target=static_cast<uintptr_t>(static_cast<int64_t>((segment.start+at)&~uintptr_t{4095})+
                delta*4096+((w(4)>>10)&4095));
            const uintptr_t function=FunctionFor(image,segment.start+at);
            if (!names.contains(target) || !function || function!=FunctionFor(image,segment.start+at+60)) continue;
            // Confirm the same function takes Mesh in x0 and archive in x1.
            bool mesh_argument=false, archive_argument=false;
            if (function<segment.start) continue;
            for (size_t p=function-segment.start; p<at && p<function-segment.start+64; p+=4) {
                const uint32_t instruction=Word(segment,p);
                mesh_argument |= instruction==(0xaa0003e0|mesh);
                archive_argument |= instruction==(0xaa0103e0|archive);
            }
            if (!mesh_argument || !archive_argument) continue;
            candidates[offset].insert(segment.start+at);
            reader_paths.insert(segment.start+at);
        }
    }
    if (candidates.size() != 1) return Failure("named archive candidate missing or ambiguous");

    MeshLayoutEvidence result;
    result.image_base = image.base;
    result.candidate_offset = candidates.begin()->first;
    result.named_references = static_cast<uint32_t>(candidates.begin()->second.size());
    result.descriptor_paths = static_cast<uint32_t>(descriptor_paths.size());
    result.reader_paths = static_cast<uint32_t>(reader_paths.size());
    std::set<uintptr_t> archive_functions, read_functions, write_functions;
    for (const auto address : candidates.begin()->second) {
        const uintptr_t function = FunctionFor(image, address);
        if (function) archive_functions.insert(function);
    }
    struct AccessSummary {
        bool load = false;
        bool store = false;
        std::map<uint32_t, std::set<uint32_t>> offsets_by_base;
        std::set<uint32_t> candidate_bases;
    };
    std::map<uintptr_t, AccessSummary> access_by_function;
    std::map<uintptr_t, std::vector<FieldAccess>> ordered_accesses;
    for (auto& segment : image.segments) {
        if (!(segment.flags & PF_X) || segment.start % 4) continue;
        for (size_t at = 0; at + 4 <= segment.bytes.size(); at += 4) {
            const FieldAccess any_access = DecodeMemoryAccess(Word(segment, at));
            if ((!any_access.load && !any_access.store) || any_access.base == 31) continue;
            const uintptr_t function = FunctionFor(image, segment.start + at);
            if (!function) continue;
            auto& summary = access_by_function[function];
            summary.offsets_by_base[any_access.base].insert(any_access.offset);
            ordered_accesses[function].push_back(any_access);
            if (any_access.offset == result.candidate_offset) {
                summary.load = summary.load || any_access.load;
                summary.store = summary.store || any_access.store;
                summary.candidate_bases.insert(any_access.base);
            }
        }
    }
    // A single displacement is common across unrelated native classes. Keep
    // only functions that also touch the surrounding Mesh storage lanes; this
    // is a function-level consistency check, not a writable-layout proof.
    const std::set<uint32_t> mesh_lanes{0x68, 0x84, 0xe8, 0x108, 0x118,
        0x128, 0x148, 0x158, 0x168, 0x178, 0x190, 0x198, 0x1a0,
        0x1b8, 0x1bc, 0x1c4, 0x1c8, 0x1d0, 0x1d8, 0x1f0, 0x208, 0x218};
    for (const auto& [function, summary] : access_by_function) {
        size_t lane_count = 0;
        for (const auto base : summary.candidate_bases) {
            const auto& offsets = summary.offsets_by_base.at(base);
            size_t base_lanes = 0;
            for (const auto lane : mesh_lanes) if (offsets.contains(lane)) ++base_lanes;
            lane_count = std::max(lane_count, base_lanes);
        }
        if (lane_count < 3) continue;
        if (summary.load) read_functions.insert(function);
        if (summary.store) write_functions.insert(function);
    }
    result.archive_functions = static_cast<uint32_t>(archive_functions.size());
    std::set<uintptr_t> copy_functions;
    for (const auto& [function, accesses] : ordered_accesses) {
        const auto summary = access_by_function.find(function);
        if (summary == access_by_function.end()) continue;
        size_t lane_count = 0;
        for (const auto base : summary->second.candidate_bases) {
            const auto& offsets = summary->second.offsets_by_base.at(base);
            size_t base_lanes = 0;
            for (const auto lane : mesh_lanes) if (offsets.contains(lane)) ++base_lanes;
            lane_count = std::max(lane_count, base_lanes);
        }
        if (lane_count < 3) continue;
        for (size_t index = 0; index < accesses.size(); ++index) {
            const auto& source = accesses[index];
            if (!source.load || source.offset != 0x14 || source.value_register == 31) continue;
            const size_t end = std::min(accesses.size(), index + size_t{8});
            for (size_t next = index + 1; next < end; ++next) {
                const auto& target = accesses[next];
                if (target.store && target.offset == result.candidate_offset &&
                        target.value_register == source.value_register && target.base != source.base) {
                    copy_functions.insert(function);
                    break;
                }
            }
            if (copy_functions.contains(function)) break;
        }
    }
    result.copy_functions = static_cast<uint32_t>(copy_functions.size());
    for (const auto function : copy_functions) {
        if (function >= image.base) result.copy_function_offsets.push_back(function - image.base);
    }
    result.field_read_functions = static_cast<uint32_t>(read_functions.size());
    result.field_write_functions = static_cast<uint32_t>(write_functions.size());
    for (auto& segment : image.segments) {
        if (!(segment.flags & PF_X) || segment.start % 4) continue;
        for (size_t at = 0; at + 20 <= segment.bytes.size(); at += 4) {
            const uint32_t load = Word(segment, at);
            if ((load & 0xffc00000) != 0xb9400000 || ((load >> 10) & 4095) * 4 != result.candidate_offset ||
                    ((load >> 5) & 31) == 31) continue;
            const uint32_t value = load & 31;
            for (size_t gap : {size_t{4}, size_t{8}}) {
                const uint32_t four = Word(segment, at + gap), compare = Word(segment, at + gap + 4),
                    select = Word(segment, at + gap + 8);
                if ((four & 0xffffffe0) != 0x52800080 || compare != (0x7100101f | (value << 5))) continue;
                const uint32_t constant = four & 31;
                if (constant != value && select == (0x1a803000 | (constant << 16) | (value << 5) | value)) {
                    ++result.clamp_consumers;
                    break;
                }
            }
        }
    }
    result.status = "candidate only; archive_functions=" + std::to_string(result.archive_functions) +
        " descriptor_paths=" + std::to_string(result.descriptor_paths) +
        " reader_paths=" + std::to_string(result.reader_paths) +
        " copy_functions=" + std::to_string(result.copy_functions) +
        " field_read_functions=" + std::to_string(result.field_read_functions) +
        " field_write_functions=" + std::to_string(result.field_write_functions) +
        " clamp_sites=" + std::to_string(result.clamp_consumers) + "; renderer publication gated";
    if (!result.copy_function_offsets.empty()) {
        result.status += " copy_offsets=";
        for (size_t index = 0; index < result.copy_function_offsets.size(); ++index) {
            char buffer[32]{};
            std::snprintf(buffer, sizeof(buffer), "0x%zx", result.copy_function_offsets[index]);
            if (index) result.status += ",";
            result.status += buffer;
        }
    }
    return result;
}
}
