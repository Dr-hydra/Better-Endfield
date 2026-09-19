// Endfield's stripped UnityEngine.CoreModule does not expose the public
// Mesh.vertexBufferCount / GetVertexBufferStride / InternalSetVertexBufferData
// metadata used by the first PoC-3 attempt. The same runtime does expose
// Mesh.GetVertexBufferImpl(int) and GraphicsBuffer count/stride/raw upload.
// Reuse the existing contract slots before BetterEndfield calls Initialize().

namespace BetterEndfield::CustomModelModule {
namespace {

void ReplacePoC3Contract(
    std::string_view old_key,
    const char* new_key,
    const BE_MethodDescriptorV1& descriptor) {
    for (auto& method : g_methods) {
        if (std::string_view(method.key) != old_key) continue;
        method.key = new_key;
        method.descriptor = descriptor;
        method.required = true;
        method.pointer = nullptr;
        method.method_info = nullptr;
        method.resolved = false;
        return;
    }
}

struct PoC3StrippedContractRedirect {
    PoC3StrippedContractRedirect() {
        ReplacePoC3Contract(
            "mesh.get_vertex_buffer_count",
            "mesh.get_vertex_buffer_impl",
            {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
                "GetVertexBufferImpl", "System.Int32",
                "UnityEngine.GraphicsBuffer", 1});

        ReplacePoC3Contract(
            "mesh.get_vertex_buffer_stride",
            "graphics_buffer.get_stride",
            {"UnityEngine.CoreModule.dll", "UnityEngine", "GraphicsBuffer",
                "get_stride", nullptr, "System.Int32", 0});

        ReplacePoC3Contract(
            "mesh.internal_set_vertex_buffer_data",
            "graphics_buffer.internal_set_native_data",
            {"UnityEngine.CoreModule.dll", "UnityEngine", "GraphicsBuffer",
                "InternalSetNativeData",
                "System.IntPtr|System.Int32|System.Int32|System.Int32|System.Int32",
                "System.Void", 5});

        // Mesh::.ctor is not used by PoC-3 because custom meshes are cloned
        // from the original. Reuse that slot for GraphicsBuffer.count.
        ReplacePoC3Contract(
            "mesh.ctor",
            "graphics_buffer.get_count",
            {"UnityEngine.CoreModule.dll", "UnityEngine", "GraphicsBuffer",
                "get_count", nullptr, "System.Int32", 0});
    }
};

PoC3StrippedContractRedirect g_poc3_stripped_contract_redirect;

} // namespace
} // namespace BetterEndfield::CustomModelModule
