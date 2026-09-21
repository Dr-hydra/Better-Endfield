#pragma once
#include <cstddef>
#include <cstdint>
#include <string>
#include <vector>

namespace betterendfield {
struct MeshLayoutEvidence {
    uintptr_t image_base = 0;
    uint32_t candidate_offset = 0;
    uint32_t named_references = 0;
    uint32_t clamp_consumers = 0;
    uint32_t archive_functions = 0;
    uint32_t descriptor_paths = 0;
    uint32_t reader_paths = 0;
    uint32_t copy_functions = 0;
    std::vector<uintptr_t> copy_function_offsets;
    uint32_t field_read_functions = 0;
    uint32_t field_write_functions = 0;
    std::string status;
    // This probe never authorizes writes. A production adapter must separately
    // verify object provenance, independent paths and lifecycle requirements.
};
MeshLayoutEvidence ProbeLoadedUnityMeshLayout();
bool ReadOwnMemory(uintptr_t address, void* output, size_t size);
}
