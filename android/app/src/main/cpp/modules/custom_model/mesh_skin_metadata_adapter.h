#pragma once

#include "mesh_layout_probe.h"
#include <cstdint>

namespace betterendfield {

class MeshSkinMetadataAdapter final {
public:
    explicit MeshSkinMetadataAdapter(MeshLayoutEvidence evidence)
        : evidence_(evidence) {}

    // A candidate is readable only when the field evidence is unambiguous and
    // the object address can be copied safely. This does not authorize writes.
    bool Read(uintptr_t native_mesh, uint32_t& value) const;
    bool LayoutVerified() const;
    // Requires both named transfer paths plus an owned, unpublished Mesh.
    // The caller supplies the lifetime/ownership assertion; evidence alone
    // never grants permission to modify the original asset.
    bool Write(uintptr_t native_mesh, uint32_t value,
        bool independently_verified) const;
    const MeshLayoutEvidence& evidence() const { return evidence_; }

private:
    bool ValidAddress(uintptr_t native_mesh) const;
    MeshLayoutEvidence evidence_{};
};
}
