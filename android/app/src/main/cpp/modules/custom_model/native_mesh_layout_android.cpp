#include "native_mesh_layout.h"
#include "mesh_layout_probe.h"

namespace BetterEndfield::CustomModel {

bool ResolveNativeMeshLayout(std::span<const uint8_t>, NativeMeshLayout& layout,
    std::string& error) {
    const betterendfield::MeshLayoutEvidence evidence =
        betterendfield::ProbeLoadedUnityMeshLayout();
    layout = {};
    if (evidence.candidate_offset == 0 || evidence.archive_functions < 2 ||
            evidence.descriptor_paths == 0 || evidence.reader_paths == 0) {
        error = evidence.status;
        return false;
    }
    layout.bones_per_vertex_offset = evidence.candidate_offset;
    layout.agreeing_serializers = evidence.archive_functions;
    error = evidence.status;
    return true;
}

}
