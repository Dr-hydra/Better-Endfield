#include "mesh_skin_metadata_adapter.h"
#include <cstring>
#include <sys/uio.h>
#include <unistd.h>

namespace betterendfield {
namespace {
bool ValidValue(uint32_t value) { return value == 1 || value == 2 || value == 4; }
}

bool MeshSkinMetadataAdapter::ValidAddress(uintptr_t native_mesh) const {
    return native_mesh != 0 && evidence_.candidate_offset != 0 &&
        native_mesh <= UINTPTR_MAX - evidence_.candidate_offset;
}

bool MeshSkinMetadataAdapter::Read(uintptr_t native_mesh, uint32_t& value) const {
    value = 0;
    if (!ValidAddress(native_mesh) || evidence_.named_references == 0) return false;
    if (!ReadOwnMemory(native_mesh + evidence_.candidate_offset, &value, sizeof(value))) return false;
    return ValidValue(value);
}

bool MeshSkinMetadataAdapter::LayoutVerified() const {
    return evidence_.candidate_offset && evidence_.archive_functions >= 2 &&
        evidence_.descriptor_paths >= 1 && evidence_.reader_paths >= 1;
}

bool MeshSkinMetadataAdapter::Write(uintptr_t native_mesh, uint32_t value,
        bool independently_verified) const {
    if (!independently_verified || !LayoutVerified() ||
            !ValidValue(value) || !ValidAddress(native_mesh)) return false;
    const uintptr_t address = native_mesh + evidence_.candidate_offset;
    if (address > UINTPTR_MAX-sizeof(value)) return false;
    iovec local{&value, sizeof(value)}, remote{reinterpret_cast<void*>(address), sizeof(value)};
    if (process_vm_writev(getpid(), &local, 1, &remote, 1, 0) != sizeof(value)) return false;
    uint32_t readback=0;
    return Read(native_mesh, readback) && readback==value;
}
}
