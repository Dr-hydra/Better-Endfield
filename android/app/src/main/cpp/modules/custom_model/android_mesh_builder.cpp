#include "android_mesh_builder.h"
#include "core/log.h"
#include <array>
#include <cstring>
#include <stdexcept>

namespace betterendfield {
namespace {
constexpr const char* kAssembly = "UnityEngine.CoreModule.dll";
constexpr const char* kLog = "android_mesh_builder";
Il2CppRuntime* runtime = nullptr;
bool rollback_test = false;
bool pipeline_lod_enabled = false;
bool npc_parameters_enabled = false, inspection_enabled=false;
struct Api {
    ResolvedMethod allocate, acquire, acquire_unchecked, item, dispose, apply, set_vertex, set_index,
        set_sub_count, set_sub, vertex_ptr, vertex_size, index_ptr, index_size,
        vertex_count, buffer_count, sub_count, sub, mesh_vertex_count, attribute_count, attribute,
        array_create, array_set, array_get, array_length;
    ResolvedField data_ptr;
    ResolvedClass descriptor, data_array;
    bool ready = false;
} api;

struct Calls {
    std::vector<uint32_t> roots;
    ~Calls() { for (auto root : roots) runtime->FreeGcHandle(root); }
    void* Root(void* value) {
        if (!value) throw std::runtime_error("null managed result");
        uint32_t handle = runtime->NewGcHandle(value, false);
        if (!handle) throw std::runtime_error("GC root allocation failed");
        roots.push_back(handle);
        return value;
    }
    void* Call(ResolvedMethod method, void* instance, void** args = nullptr) {
        if (!method.info) throw std::runtime_error("missing method");
        void* exception = nullptr;
        void* result = runtime->Invoke(method.info, instance, args, &exception);
        if (exception) throw std::runtime_error("managed invocation failed");
        return result ? Root(result) : nullptr;
    }
    template<class T> T Value(ResolvedMethod method, void* instance, void** args = nullptr) {
        void* raw = runtime->Unbox(Call(method, instance, args));
        if (!raw) throw std::runtime_error("missing boxed value");
        T value{}; std::memcpy(&value, raw, sizeof(value)); return value;
    }
};

struct MeshDataScope {
    Calls& calls;
    void* box = nullptr;
    uintptr_t data = 0;
    bool consumed = false;
    ~MeshDataScope() {
        if (box && !consumed) {
            void* exception = nullptr;
            runtime->Invoke(api.dispose.info, runtime->Unbox(box), nullptr, &exception);
            if (exception) LogError(kLog, "MeshData Dispose failed");
        }
    }
    void Init(void* mesh = nullptr, bool metadata_only = false) {
        int32_t one = 1, zero = 0;
        void* args[]{mesh ? mesh : &one};
        if (mesh && metadata_only) {
            // Source assets can have Read/Write disabled. This internal
            // constructor is used only for layout sizes, never CPU data reads.
            // The owned destination readback still uses the checked public API.
            box = calls.Root(runtime->NewObject(api.data_array.info));
            bool check_read_write = false;
            void* ctor_args[]{mesh,&check_read_write};
            calls.Call(api.acquire_unchecked,runtime->Unbox(box),ctor_args);
        } else box = calls.Call(mesh ? api.acquire : api.allocate, nullptr, args);
        void* item_args[]{&zero};
        void* item = calls.Call(api.item, runtime->Unbox(box), item_args);
        void* pointer = calls.Root(runtime->ReadFieldObject(api.data_ptr, item));
        void* raw = runtime->Unbox(pointer);
        if (!raw) throw std::runtime_error("MeshData pointer missing");
        std::memcpy(&data, raw, sizeof(data));
        if (!data) throw std::runtime_error("null native MeshData");
    }
};

struct SubMesh {
    float bounds[6]{};
    int32_t topology = 0, start = 0, count = 0, base = 0, first = 0, vertices = 0;
};
static_assert(sizeof(SubMesh) == 48);

void Transfer(Calls& calls, uintptr_t data, int stream,
        const std::vector<uint8_t>& bytes, bool write) {
    void* args[]{&data, &stream};
    uint64_t size = calls.Value<uint64_t>(stream < 0 ? api.index_size : api.vertex_size, nullptr, args);
    uintptr_t ptr = calls.Value<uintptr_t>(stream < 0 ? api.index_ptr : api.vertex_ptr, nullptr, args);
    if (!ptr || size != bytes.size() || size > 64u * 1024u * 1024u)
        throw std::runtime_error("buffer size differs from BEM");
    if (write) std::memcpy(reinterpret_cast<void*>(ptr), bytes.data(), bytes.size());
    else if (std::memcmp(reinterpret_cast<void*>(ptr), bytes.data(), bytes.size()) != 0)
        throw std::runtime_error("submitted buffer differs from BEM");
}
}

void ConfigureAndroidMeshBuilder(Il2CppRuntime& value, bool test, bool pipeline, bool npc, bool inspect) { runtime = &value; rollback_test = test; pipeline_lod_enabled=pipeline; npc_parameters_enabled=npc; inspection_enabled=inspect; api = {}; }
bool AndroidMeshRollbackTest() { return rollback_test; }
bool AndroidPipelineLodEnabled() { return pipeline_lod_enabled; }
bool AndroidNpcParametersEnabled() { return npc_parameters_enabled; }
bool AndroidInspectionEnabled() { return inspection_enabled; }

bool AndroidAuditMaterialCopy(void* original,void* copy) {
    try {
        Calls calls;
        auto method=[&](const char* name,const char* args,const char* result,int count) {
            return runtime->ResolveMethodExact(kAssembly,"UnityEngine","Material",name,args,result,count);
        };
        auto shader=method("get_shader","","UnityEngine.Shader",0);
        auto keywords=method("get_shaderKeywords","","System.String[]",0);
        auto queue=method("get_renderQueue","","System.Int32",0);
        if (calls.Call(shader,original)!=calls.Call(shader,copy) ||
            calls.Value<int32_t>(queue,original)!=calls.Value<int32_t>(queue,copy)) return false;
        void* before=calls.Call(keywords,original); void* after=calls.Call(keywords,copy);
        const int count=calls.Value<int32_t>(api.array_length,before);
        if (count!=calls.Value<int32_t>(api.array_length,after)) return false;
        std::string list;
        for(int32_t i=0;i<count;++i) {
            void* args[]{&i};
            auto left=runtime->CopyString(calls.Call(api.array_get,before,args));
            auto right=runtime->CopyString(calls.Call(api.array_get,after,args));
            if (left!=right) return false;
            if (!list.empty()) list+=",";
            list+=left;
        }
        auto has=method("HasProperty","System.String","System.Boolean",1);
        auto scalar=method("GetFloat","System.String","System.Single",1);
        auto color=method("GetColor","System.String","UnityEngine.Color",1);
        auto object_name=runtime->ResolveMethodExact(kAssembly,"UnityEngine","Object","get_name","","System.String",0);
        std::string name=runtime->CopyString(calls.Call(object_name,original));
        unsigned checked=0;
        for (const char* property:{"_Metallic","_Smoothness","_BumpScale","_OcclusionStrength","_NormalScale"}) {
            void* key=calls.Root(runtime->NewString(property)); void* args[]{key};
            if (!calls.Value<bool>(has,original,args)) continue;
            const auto left=calls.Value<float>(scalar,original,args),right=calls.Value<float>(scalar,copy,args);
            if (left!=right) return false;
            ++checked;
        }
        for (const char* property:{"_BaseColor","_Color","_EmissionColor"}) {
            void* key=calls.Root(runtime->NewString(property)); void* args[]{key};
            if (!calls.Value<bool>(has,original,args)) continue;
            auto left=calls.Value<std::array<float,4>>(color,original,args);
            auto right=calls.Value<std::array<float,4>>(color,copy,args);
            if (left!=right) return false;
            ++checked;
        }
        LogInfo(kLog,("material copy audit PASS "+name+" shader/queue/keywords; known properties="+
            std::to_string(checked)+" keywords="+list).c_str());
        return true;
    } catch (const std::exception& error) { LogError(kLog,error.what()); return false; }
}
void AndroidAuditTextureColorSpace(void* original,void* replacement,const std::string& name) {
    try {
        Calls calls;
        auto format=runtime->ResolveMethodExact(kAssembly,"UnityEngine","Texture","get_graphicsFormat","",
            "UnityEngine.Experimental.Rendering.GraphicsFormat",0);
        auto srgb=runtime->ResolveMethodExact(kAssembly,"UnityEngine.Experimental.Rendering","GraphicsFormatUtility",
            "IsSRGBFormat","UnityEngine.Experimental.Rendering.GraphicsFormat","System.Boolean",1);
        auto left=calls.Value<int32_t>(format,original),right=calls.Value<int32_t>(format,replacement);
        void* a[]{&left};void* b[]{&right};
        bool before=calls.Value<bool>(srgb,nullptr,a),after=calls.Value<bool>(srgb,nullptr,b);
        LogInfo(kLog,("texture colorspace "+name+" original="+std::to_string(left)+" replacement="+std::to_string(right)+
            " srgb="+std::to_string(before)+"/"+std::to_string(after)+(before==after?" PASS":" MISMATCH")).c_str());
    } catch (const std::exception& error) { LogError(kLog,error.what()); }
}

void AndroidAuditNormalTexture(void* texture,const std::string& name) {
    if (!inspection_enabled || name!="T_actor_endminf_cloth_01_N") return;
    static bool sampled=false;
    if (sampled) return;
    sampled=true;
    void* temporary=nullptr; void* cpu=nullptr; void* previous=nullptr;
    auto resolve=[&](const char* type,const char* method,const char* args,const char* result,int count) {
        return runtime->ResolveMethodExact(kAssembly,"UnityEngine",type,method,args,result,count);
    };
    auto set_active=resolve("RenderTexture","set_active","UnityEngine.RenderTexture","System.Void",1);
    auto release=resolve("RenderTexture","ReleaseTemporary","UnityEngine.RenderTexture","System.Void",1);
    auto destroy=resolve("Object","Destroy","UnityEngine.Object","System.Void",1);
    Calls calls;
    try {
        previous=calls.Call(resolve("RenderTexture","get_active","","UnityEngine.RenderTexture",0),nullptr);
        int32_t size=64,depth=0,rt_format=0,linear=1;
        void* rt_args[]{&size,&size,&depth,&rt_format,&linear};
        temporary=calls.Call(resolve("RenderTexture","GetTemporary",
            "System.Int32|System.Int32|System.Int32|UnityEngine.RenderTextureFormat|UnityEngine.RenderTextureReadWrite",
            "UnityEngine.RenderTexture",5),nullptr,rt_args);
        void* blit[]{texture,temporary};
        calls.Call(resolve("Graphics","Blit","UnityEngine.Texture|UnityEngine.RenderTexture","System.Void",2),nullptr,blit);
        auto type=runtime->ResolveClass(kAssembly,"UnityEngine","Texture2D");
        cpu=calls.Root(runtime->NewObject(type.info));
        int32_t format=4,mips=1; bool linear_tex=true;
        void* ctor[]{&size,&size,&format,&mips,&linear_tex};
        calls.Call(resolve("Texture2D",".ctor","System.Int32|System.Int32|UnityEngine.TextureFormat|System.Int32|System.Boolean","System.Void",5),cpu,ctor);
        void* active_args[]{temporary}; calls.Call(set_active,nullptr,active_args);
        std::array<float,4> rect{0,0,64,64}; int32_t zero=0; bool recalc=false;
        void* read_args[]{rect.data(),&zero,&zero,&recalc};
        calls.Call(resolve("Texture2D","ReadPixels","UnityEngine.Rect|System.Int32|System.Int32|System.Boolean","System.Void",4),cpu,read_args);
        auto pixel=resolve("Texture2D","GetPixel","System.Int32|System.Int32","UnityEngine.Color",2);
        std::array<float,4> sum{};
        for (int32_t y:{16,32,48}) for (int32_t x:{16,32,48}) {
            void* args[]{&x,&y}; auto rgba=calls.Value<std::array<float,4>>(pixel,cpu,args);
            for (int i=0;i<4;++i) sum[i]+=rgba[i]/9;
        }
        LogInfo(kLog,("original normal GPU sample "+name+" RGBA="+std::to_string(sum[0])+","+
            std::to_string(sum[1])+","+std::to_string(sum[2])+","+std::to_string(sum[3])).c_str());
    } catch (const std::exception& error) { LogError(kLog,(std::string("normal GPU sample unavailable: ")+error.what()).c_str()); }
    void* exception=nullptr; void* active_args[]{previous};
    if (set_active.info) runtime->Invoke(set_active.info,nullptr,active_args,&exception);
    if (temporary && release.info) { void* args[]{temporary}; runtime->Invoke(release.info,nullptr,args,&exception); }
    if (cpu && destroy.info) { void* args[]{cpu}; runtime->Invoke(destroy.info,nullptr,args,&exception); }
}

bool AndroidMeshBuilderReady() {
    if (!runtime) return false;
    if (api.ready) return true;
    bool ok = true;
    auto resolve = [&](ResolvedMethod& out, const char* type, const char* name,
            const char* args, const char* result, int count) {
        out = runtime->ResolveMethodExact(kAssembly, "UnityEngine", type, name, args, result, count);
        if (!out.info) {
            LogError(kLog, runtime->DescribeMethod(kAssembly, "UnityEngine", type, name).c_str());
            ok = false;
        }
    };
    resolve(api.allocate,"Mesh","AllocateWritableMeshData","System.Int32","UnityEngine.Mesh.MeshDataArray",1);
    resolve(api.acquire,"Mesh","AcquireReadOnlyMeshData","UnityEngine.Mesh","UnityEngine.Mesh.MeshDataArray",1);
    resolve(api.acquire_unchecked,"Mesh.MeshDataArray",".ctor","UnityEngine.Mesh|System.Boolean","System.Void",2);
    resolve(api.item,"Mesh.MeshDataArray","get_Item","System.Int32","UnityEngine.Mesh.MeshData",1);
    resolve(api.dispose,"Mesh.MeshDataArray","Dispose","","System.Void",0);
    resolve(api.apply,"Mesh","ApplyAndDisposeWritableMeshData","UnityEngine.Mesh.MeshDataArray|UnityEngine.Mesh|UnityEngine.Rendering.MeshUpdateFlags","System.Void",3);
    resolve(api.set_vertex,"Mesh.MeshData","SetVertexBufferParamsFromArray","System.IntPtr|System.Int32|UnityEngine.Rendering.VertexAttributeDescriptor[]","System.Void",3);
    resolve(api.set_index,"Mesh.MeshData","SetIndexBufferParamsImpl","System.IntPtr|System.Int32|UnityEngine.Rendering.IndexFormat","System.Void",3);
    resolve(api.set_sub_count,"Mesh.MeshData","SetSubMeshCount","System.IntPtr|System.Int32","System.Void",2);
    resolve(api.set_sub,"Mesh.MeshData","SetSubMeshImpl_Injected","System.IntPtr|System.Int32|UnityEngine.Rendering.SubMeshDescriptor&|UnityEngine.Rendering.MeshUpdateFlags","System.Void",4);
    resolve(api.vertex_ptr,"Mesh.MeshData","GetVertexDataPtr","System.IntPtr|System.Int32","System.IntPtr",2);
    resolve(api.vertex_size,"Mesh.MeshData","GetVertexDataSize","System.IntPtr|System.Int32","System.UInt64",2);
    resolve(api.index_ptr,"Mesh.MeshData","GetIndexDataPtr","System.IntPtr","System.IntPtr",1);
    resolve(api.index_size,"Mesh.MeshData","GetIndexDataSize","System.IntPtr","System.UInt64",1);
    resolve(api.vertex_count,"Mesh.MeshData","GetVertexCount","System.IntPtr","System.Int32",1);
    resolve(api.buffer_count,"Mesh.MeshData","GetVertexBufferCount","System.IntPtr","System.Int32",1);
    resolve(api.sub_count,"Mesh.MeshData","GetSubMeshCount","System.IntPtr","System.Int32",1);
    resolve(api.sub,"Mesh.MeshData","GetSubMesh_Injected","System.IntPtr|System.Int32|UnityEngine.Rendering.SubMeshDescriptor&","System.Void",3);
    resolve(api.mesh_vertex_count,"Mesh","get_vertexCount","","System.Int32",0);
    resolve(api.attribute_count,"Mesh","get_vertexAttributeCount","","System.Int32",0);
    resolve(api.attribute,"Mesh","GetVertexAttribute","System.Int32","UnityEngine.Rendering.VertexAttributeDescriptor",1);
    api.array_create = runtime->ResolveMethodExact("mscorlib.dll","System","Array","CreateInstance","System.Type|System.Int32","System.Array",2);
    api.array_set = runtime->ResolveMethodExact("mscorlib.dll","System","Array","SetValue","System.Object|System.Int32","System.Void",2);
    api.array_get = runtime->ResolveMethodExact("mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1);
    api.array_length = runtime->ResolveMethodExact("mscorlib.dll","System","Array","get_Length","","System.Int32",0);
    api.descriptor = runtime->ResolveClass(kAssembly,"UnityEngine.Rendering","VertexAttributeDescriptor");
    api.data_array = runtime->ResolveClass(kAssembly,"UnityEngine","Mesh.MeshDataArray");
    api.data_ptr = runtime->ResolveField(kAssembly,"UnityEngine","Mesh.MeshData","m_Ptr");
    api.ready = ok && api.array_create.info && api.array_set.info && api.array_get.info &&
        api.array_length.info && api.descriptor.info && api.data_array.info && api.data_ptr.info;
    return api.ready;
}

bool AndroidReadMeshStrides(void* mesh, std::vector<int32_t>& strides) {
    strides.clear();
    if (!mesh || !AndroidMeshBuilderReady()) return false;
    try {
        Calls calls;
        MeshDataScope data{calls}; data.Init(mesh,true);
        void* count_args[]{&data.data};
        int32_t vertices = calls.Value<int32_t>(api.vertex_count, nullptr, count_args);
        int32_t streams = calls.Value<int32_t>(api.buffer_count, nullptr, count_args);
        if (vertices <= 0 || streams <= 0 || streams > 4) return false;
        for (int32_t stream = 0; stream < streams; ++stream) {
            void* args[]{&data.data, &stream};
            uint64_t bytes = calls.Value<uint64_t>(api.vertex_size, nullptr, args);
            if (!bytes || bytes % vertices || bytes / vertices > 1024) return false;
            strides.push_back(static_cast<int32_t>(bytes / vertices));
        }
        return true;
    } catch (const std::exception& error) { LogError(kLog,error.what()); strides.clear(); return false; }
}

bool AndroidSubmitMesh(void* mesh, const BetterEndfield::CustomModel::BemComponent& component) {
    if (!mesh || !AndroidMeshBuilderReady()) return false;
    try {
        Calls calls;
        MeshDataScope writable{calls}; writable.Init();
        int32_t vertices = static_cast<int32_t>(component.info.vertex_count);
        int32_t indices = static_cast<int32_t>(component.info.index_count);
        int32_t attr_count = static_cast<int32_t>(component.attributes.size());
        if (vertices <= 0 || indices <= 0 || attr_count <= 0 || attr_count > 16) return false;
        void* array_args[]{api.descriptor.type_object, &attr_count};
        void* descriptors = calls.Call(api.array_create, nullptr, array_args);
        if (calls.Value<int32_t>(api.array_length,descriptors) != attr_count) return false;
        for (int32_t i = 0; i < attr_count; ++i) {
            void* boxed = calls.Root(runtime->BoxValue(api.descriptor.info, component.attributes[i].data()));
            void* args[]{boxed, &i}; calls.Call(api.array_set, descriptors, args);
            void* get[]{&i};
            if (calls.Value<std::array<int32_t,4>>(api.array_get,descriptors,get) != component.attributes[i])
                throw std::runtime_error("descriptor array round trip failed");
        }
        void* vertex_args[]{&writable.data, &vertices, descriptors};
        calls.Call(api.set_vertex, nullptr, vertex_args);
        for (int stream = 0; stream < 3; ++stream) Transfer(calls,writable.data,stream,component.streams[stream],true);
        int32_t format = component.info.index_element_size == 4 ? 1 : 0;
        void* index_args[]{&writable.data,&indices,&format}; calls.Call(api.set_index,nullptr,index_args);
        Transfer(calls,writable.data,-1,component.indices,true);
        int32_t subs = component.draws.empty() ? 1 : static_cast<int32_t>(component.draws.size());
        void* count_args[]{&writable.data,&subs}; calls.Call(api.set_sub_count,nullptr,count_args);
        int32_t flags = 0;
        for (int32_t i = 0; i < subs; ++i) {
            SubMesh sub; sub.vertices = vertices;
            sub.start = component.draws.empty() ? 0 : component.draws[i].start;
            sub.count = component.draws.empty() ? indices : component.draws[i].count;
            void* args[]{&writable.data,&i,&sub,&flags}; calls.Call(api.set_sub,nullptr,args);
        }
        void* apply_args[]{runtime->Unbox(writable.box),mesh,&flags};
        calls.Call(api.apply,nullptr,apply_args); writable.consumed = true;
        if (calls.Value<int32_t>(api.attribute_count,mesh) != attr_count)
            throw std::runtime_error("submitted attribute count differs");
        for (int32_t i = 0; i < attr_count; ++i) {
            void* args[]{&i};
            if (calls.Value<std::array<int32_t,4>>(api.attribute,mesh,args) != component.attributes[i])
                throw std::runtime_error("submitted vertex layout differs");
        }
        // Acquire a fresh read-only snapshot from the submitted Mesh, not the
        // writable staging storage. Verify bytes and every draw before publish.
        MeshDataScope readback{calls}; readback.Init(mesh);
        void* get_args[]{&readback.data};
        if (calls.Value<int32_t>(api.vertex_count,nullptr,get_args) != vertices ||
            calls.Value<int32_t>(api.sub_count,nullptr,get_args) != subs)
            throw std::runtime_error("submitted counts differ");
        for (int stream = 0; stream < 3; ++stream) Transfer(calls,readback.data,stream,component.streams[stream],false);
        Transfer(calls,readback.data,-1,component.indices,false);
        for (int32_t i = 0; i < subs; ++i) {
            SubMesh sub;
            void* args[]{&readback.data,&i,&sub}; calls.Call(api.sub,nullptr,args);
            const int32_t start = component.draws.empty() ? 0 : component.draws[i].start;
            const int32_t count = component.draws.empty() ? indices : component.draws[i].count;
            if (sub.start != start || sub.count != count || sub.topology != 0 || sub.base != 0)
                throw std::runtime_error("submitted draw differs");
        }
        LogInfo(kLog,("submit/readback PASS C"+std::to_string(component.info.component_id)+
            " vertices="+std::to_string(vertices)+" indices="+std::to_string(indices)+
            " submeshes="+std::to_string(subs)).c_str());
        return true;
    } catch (const std::exception& error) { LogError(kLog,error.what()); return false; }
}

void AndroidReleaseUiDonor(void* handle, uint32_t root) {
    if (handle) {
        auto dispose = runtime->ResolveMethodExact("Common.Beyond.dll","Beyond.Resource",
            "FAssetProxyHandle","Dispose","","System.Void",0);
        void* exception = nullptr;
        runtime->Invoke(dispose.info,runtime->Unbox(handle),nullptr,&exception);
        if (exception) LogError(kLog,"UI donor handle Dispose failed");
    }
    if (root) runtime->FreeGcHandle(root);
}

void* AndroidLoadUiDonor(const std::string& resource, void*& handle, uint32_t& root) {
    handle=nullptr; root=0;
    if (!runtime || !resource.starts_with("chr_") || !resource.ends_with("_uimodel") ||
        resource.find_first_not_of("abcdefghijklmnopqrstuvwxyz0123456789_") != resource.npos) return nullptr;
    try {
        auto resolve = [&](const char* type,const char* name,const char* args,const char* result,int count) {
            return runtime->ResolveMethodExact("Common.Beyond.dll","Beyond.Resource",type,name,args,result,count);
        };
        auto initialized=resolve("ResourceManager","IsInitialized","","System.Boolean",0);
        auto pending=resolve("ResourceManager","IsAsyncIniting","","System.Boolean",0);
        auto hash=resolve("StringPathHash","CreateStringPathHash","System.String","Beyond.Resource.StringPathHash",1);
        auto load=resolve("I18NAssetLoader","Load","Beyond.Resource.StringPathHash|System.Type|Beyond.Resource.RootCategory","Beyond.Resource.FAssetProxyHandle",3);
        auto get=resolve("FAssetProxyHandle","Get","","UnityEngine.Object",0);
        auto dispose=resolve("FAssetProxyHandle","Dispose","","System.Void",0);
        auto game_object=runtime->ResolveClass(kAssembly,"UnityEngine","GameObject");
        if (!dispose.info || !game_object.type_object) return nullptr;
        Calls calls;
        if (!calls.Value<bool>(initialized,nullptr) || calls.Value<bool>(pending,nullptr)) return nullptr;
        const std::string path="assets/beyond/dynamicassets/gameplay/prefabs/uimodels/"+resource+".prefab";
        void* name=calls.Root(runtime->NewString(path.c_str()));
        void* hash_args[]{name};
        void* hash_box=calls.Call(hash,nullptr,hash_args);
        uint8_t category=0;
        void* load_args[]{runtime->Unbox(hash_box),game_object.type_object,&category};
        handle=calls.Call(load,nullptr,load_args);
        root=runtime->NewGcHandle(handle,false);
        if (!root) throw std::runtime_error("donor handle root failed");
        void* asset=calls.Call(get,runtime->Unbox(handle));
        if (!asset) throw std::runtime_error("UI donor resource unavailable");
        LogInfo(kLog,("UI donor loaded: "+resource).c_str());
        return asset;
    } catch (const std::exception& error) {
        LogError(kLog,error.what());
        AndroidReleaseUiDonor(handle,root); handle=nullptr; root=0; return nullptr;
    }
}
}
