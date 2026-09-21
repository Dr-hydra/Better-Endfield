// Included inside CameraModule's anonymous namespace, after the model scanner.
// Engine bindings are resolved by name; no Unity native object layouts are read.
namespace FpMesh = BetterEndfield::FirstPersonMesh;

// Structured exception handling is a Windows-only facility and clang/AArch64 has
// no equivalent. On Android these calls run unguarded, like every other managed
// call in the Android port; the non-null check on the resolved binding is what
// keeps an unresolved contract from being called at all.
template<class Fn,class... Args>
bool FpCall(Fn fn,Args... args) {
    if(!fn) return false;
#if defined(_WIN32)
    __try {fn(args...);return true;} __except(EXCEPTION_EXECUTE_HANDLER) {return false;}
#else
    fn(args...);return true;
#endif
}
template<class T,class Fn,class... Args>
T FpValue(T fallback,Fn fn,Args... args) {
    if(!fn) return fallback;
#if defined(_WIN32)
    __try {return fn(args...);} __except(EXCEPTION_EXECUTE_HANDLER) {return fallback;}
#else
    (void)fallback;return fn(args...);
#endif
}
bool FpCopy(void* destination,const void* source,size_t size) {
    if(!destination||!source) return false;
#if defined(_WIN32)
    __try {std::memcpy(destination,source,size);return true;} __except(EXCEPTION_EXECUTE_HANDLER) {return false;}
#else
    std::memcpy(destination,source,size);return true;
#endif
}
struct FpRoot {
    void* object=nullptr; uint32_t handle=0;
    FpRoot()=default;
    explicit FpRoot(void* p):object(p) {if(p&&g_host&&g_host->gchandle_new) handle=g_host->gchandle_new(g_host->context,p,1);}
    FpRoot(const FpRoot&)=delete; FpRoot& operator=(const FpRoot&)=delete;
    FpRoot(FpRoot&& other) noexcept:object(other.object),handle(other.handle) {other.object=nullptr;other.handle=0;}
    FpRoot& operator=(FpRoot&& other) noexcept {
        if(this!=&other) {Reset();object=other.object;handle=other.handle;other.object=nullptr;other.handle=0;}return *this;
    }
    void Reset() {if(handle&&g_host)g_host->gchandle_free(g_host->context,handle);object=nullptr;handle=0;}
    ~FpRoot(){Reset();}
    explicit operator bool() const {return object&&handle;}
};
struct FpAttribute {int attribute=0,format=0,dimension=0,stream=0;};
struct FpBounds {float center[3]{},extents[3]{};};
struct FpSubmesh {FpBounds bounds;int topology=0,index_start=0,index_count=0,base_vertex=0,first_vertex=0,vertex_count=0;};
static_assert(sizeof(FpSubmesh)==48);
struct FpReadback {void* pointer=nullptr;int version=0;int padding=0;};

struct FpEngine {
    using Resolve=void*(*)(const char*);
    using Query=int(*)(void*);
    using QueryAt=int(*)(void*,int);
    using BufferAt=void*(*)(void*,int);
    Resolve resolve=nullptr;
    Query vertex_count=nullptr,stream_count=nullptr,submesh_count=nullptr,index_format=nullptr;
    QueryAt format=nullptr,dimension=nullptr,stream=nullptr,offset=nullptr,stride=nullptr;
    bool(*has_attribute)(void*,int)=nullptr;
    BufferAt vertex_buffer=nullptr;
    void*(*index_buffer)(void*)=nullptr;
    Query buffer_count=nullptr,buffer_stride=nullptr;
    void(*buffer_destroy)(void*)=nullptr;
    void(*request)(void*,int,int,void*,FpReadback*)=nullptr;
    void(*wait)(FpReadback*)=nullptr;
    bool(*done)(FpReadback*)=nullptr;
    bool(*error)(FpReadback*)=nullptr;
    int(*data_size)(FpReadback*)=nullptr;
    void*(*data)(FpReadback*,int)=nullptr;
    bool(*supports_readback)()=nullptr;
    void*(*clone)(void*)=nullptr;
    void(*get_submesh)(void*,int,FpSubmesh*)=nullptr;
    void(*set_submesh)(void*,int,const FpSubmesh*,int)=nullptr;
    void(*set_index_params)(void*,int,int)=nullptr;
    void(*set_index_data)(void*,const void*,int,int,int,int,int)=nullptr;
    void(*upload)(void*,bool)=nullptr;
    void*(*get_mesh)(void*)=nullptr;
    void(*set_mesh)(void*,void*)=nullptr;
    void*(*get_shadow)(void*)=nullptr;
    void(*set_shadow)(void*,void*)=nullptr;
    bool(*get_offscreen)(void*)=nullptr;
    void(*set_offscreen)(void*,bool)=nullptr;
    size_t(*array_length)(void*)=nullptr;
    uint32_t(*array_bytes)(void*)=nullptr;
    size_t(*array_header)()=nullptr;
    int32_t(*value_size)(const void*,uint32_t*)=nullptr;
    size_t header=0;
    bool attempted=false,ready=false;

    template<class T> bool Bind(T& slot,const char* name) {
        slot=reinterpret_cast<T>(FpValue<void*>(nullptr,resolve,name));
        if(!slot)Log(std::string("First person mesh: missing engine binding ")+name);
        return slot!=nullptr;
    }
    bool CheckField(const char* ns,const char* type,const char* name,int expected) {
        BE_FieldDescriptorV1 desc{"UnityEngine.CoreModule.dll",ns,type,name,nullptr};
        BE_ResolvedFieldV1 field{};
        return g_host->resolve_field(g_host->context,&desc,&field)==BE_Result_Ok && field.field_info && field.offset==expected;
    }
    bool Init() {
        if(attempted)return ready;attempted=true;
        auto game=GetModuleHandleW(L"GameAssembly.dll");if(!game)return false;
        resolve=reinterpret_cast<Resolve>(GetProcAddress(game,"il2cpp_resolve_icall"));
        array_length=reinterpret_cast<decltype(array_length)>(GetProcAddress(game,"il2cpp_array_length"));
        array_bytes=reinterpret_cast<decltype(array_bytes)>(GetProcAddress(game,"il2cpp_array_get_byte_length"));
        array_header=reinterpret_cast<decltype(array_header)>(GetProcAddress(game,"il2cpp_array_object_header_size"));
        value_size=reinterpret_cast<decltype(value_size)>(GetProcAddress(game,"il2cpp_class_value_size"));
        if(!resolve||!array_length||!array_bytes||!array_header||!value_size)return false;
        header=FpValue<size_t>(0,array_header);
        if(header<16||header>128)return false;
        BE_ResolvedFieldV1 first{};
        BE_FieldDescriptorV1 d{"UnityEngine.CoreModule.dll","UnityEngine","Vector3","x","System.Single"};
        if(g_host->resolve_field(g_host->context,&d,&first)!=BE_Result_Ok||first.offset<0)return false;
        const int boxed=first.offset;
        BE_ResolvedClassV1 request_type{};
        uint32_t alignment=0;
        if(g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll","UnityEngine.Rendering","AsyncGPUReadbackRequest",&request_type)!=BE_Result_Ok||
           FpValue<int32_t>(0,value_size,request_type.class_info,&alignment)!=sizeof(FpReadback)||
           !CheckField("UnityEngine.Rendering","AsyncGPUReadbackRequest","m_Ptr",boxed)||
           !CheckField("UnityEngine.Rendering","AsyncGPUReadbackRequest","m_Version",boxed+8)) {
            Log("First person mesh: GPU request ABI mismatch.");return false;
        }
        const char* fields[]={"<bounds>k__BackingField","<topology>k__BackingField","<indexStart>k__BackingField","<indexCount>k__BackingField","<baseVertex>k__BackingField","<firstVertex>k__BackingField","<vertexCount>k__BackingField"};
        const int offsets[]={0,24,28,32,36,40,44};
        for(int i=0;i<7;++i) if(!CheckField("UnityEngine.Rendering","SubMeshDescriptor",fields[i],boxed+offsets[i])) {
            Log("First person mesh: SubMeshDescriptor ABI mismatch.");return false;
        }
        bool ok=true;
#define FP_BIND(slot,name) ok=Bind(slot,name)&&ok
        FP_BIND(vertex_count,"UnityEngine.Mesh::get_vertexCount");
        FP_BIND(stream_count,"UnityEngine.Mesh::get_vertexBufferCount");
        FP_BIND(submesh_count,"UnityEngine.Mesh::get_subMeshCount");
        FP_BIND(index_format,"UnityEngine.Mesh::get_indexFormat");
        FP_BIND(has_attribute,"UnityEngine.Mesh::HasVertexAttribute");
        FP_BIND(format,"UnityEngine.Mesh::GetVertexAttributeFormat");
        FP_BIND(dimension,"UnityEngine.Mesh::GetVertexAttributeDimension");
        FP_BIND(stream,"UnityEngine.Mesh::GetVertexAttributeStream");
        FP_BIND(offset,"UnityEngine.Mesh::GetVertexAttributeOffset");
        FP_BIND(stride,"UnityEngine.Mesh::GetVertexBufferStride");
        FP_BIND(vertex_buffer,"UnityEngine.Mesh::GetVertexBufferImpl");
        FP_BIND(index_buffer,"UnityEngine.Mesh::GetIndexBufferImpl");
        FP_BIND(buffer_count,"UnityEngine.GraphicsBuffer::get_count");
        FP_BIND(buffer_stride,"UnityEngine.GraphicsBuffer::get_stride");
        FP_BIND(buffer_destroy,"UnityEngine.GraphicsBuffer::DestroyBuffer");
        FP_BIND(request,"UnityEngine.Rendering.AsyncGPUReadback::Request_Internal_GraphicsBuffer_2_Injected");
        FP_BIND(wait,"UnityEngine.Rendering.AsyncGPUReadbackRequest::WaitForCompletion_Injected");
        FP_BIND(done,"UnityEngine.Rendering.AsyncGPUReadbackRequest::IsDone_Injected");
        FP_BIND(error,"UnityEngine.Rendering.AsyncGPUReadbackRequest::HasError_Injected");
        FP_BIND(data_size,"UnityEngine.Rendering.AsyncGPUReadbackRequest::GetLayerDataSize_Injected");
        FP_BIND(data,"UnityEngine.Rendering.AsyncGPUReadbackRequest::GetDataRaw_Injected");
        FP_BIND(supports_readback,"UnityEngine.SystemInfo::SupportsAsyncGPUReadback");
        FP_BIND(clone,"UnityEngine.Object::Internal_CloneSingle");
        FP_BIND(get_submesh,"UnityEngine.Mesh::GetSubMesh_Injected");
        FP_BIND(set_submesh,"UnityEngine.Mesh::SetSubMesh_Injected");
        FP_BIND(set_index_params,"UnityEngine.Mesh::SetIndexBufferParams");
        FP_BIND(set_index_data,"UnityEngine.Mesh::InternalSetIndexBufferData");
        FP_BIND(upload,"UnityEngine.Mesh::UploadMeshDataImpl");
        FP_BIND(get_mesh,"UnityEngine.SkinnedMeshRenderer::get_sharedMesh");
        FP_BIND(set_mesh,"UnityEngine.SkinnedMeshRenderer::set_sharedMesh");
        FP_BIND(get_shadow,"UnityEngine.Renderer::get_shadowProxyMesh");
        FP_BIND(set_shadow,"UnityEngine.Renderer::set_shadowProxyMesh");
        FP_BIND(get_offscreen,"UnityEngine.SkinnedMeshRenderer::get_updateWhenOffscreen");
        FP_BIND(set_offscreen,"UnityEngine.SkinnedMeshRenderer::set_updateWhenOffscreen");
#undef FP_BIND
        ready=ok;
        Log(std::string("First person mesh: named GPU readback/clone bindings ")+(ready?"ready.":"unavailable."));
        return ready;
    }
};
FpEngine g_fp_engine;

bool FpReadBuffer(void* buffer,size_t expected,std::vector<uint8_t>& bytes) {
    FpRoot owner(buffer);
    auto& e=g_fp_engine;
    if(!owner||expected==0||expected>128*1024*1024) {if(buffer)FpCall(e.buffer_destroy,buffer);return false;}
    int count=FpValue<int>(-1,e.buffer_count,buffer),stride=FpValue<int>(-1,e.buffer_stride,buffer);
    bool ok=count>0&&stride>0&&uint64_t(count)*stride>=expected;
    FpReadback req{};
    if(ok)ok=FpCall(e.request,buffer,int(expected),0,nullptr,&req)&&FpCall(e.wait,&req)&&
        FpValue<bool>(false,e.done,&req)&&!FpValue<bool>(true,e.error,&req)&&FpValue<int>(-1,e.data_size,&req)==int(expected);
    if(ok) {bytes.resize(expected);ok=FpCopy(bytes.data(),FpValue<void*>(nullptr,e.data,&req,0),expected);}
    FpCall(e.buffer_destroy,buffer);
    return ok;
}
struct FpSnapshot {
    int count=0,index_format=0;
    std::vector<FpAttribute> attributes;
    std::vector<int> strides,offsets;
    std::vector<std::vector<uint8_t>> streams;
    std::vector<uint8_t> indices;
    std::vector<FpSubmesh> submeshes;
};
int FpFormatSize(int format) {const int sizes[]={4,2,1,1,2,2,1,1,2,2,4,4};return format>=0&&format<12?sizes[format]:0;}
bool FpReadMesh(void* mesh,FpSnapshot& snapshot) {
    auto& e=g_fp_engine;
    snapshot.count=FpValue<int>(-1,e.vertex_count,mesh);
    int streams=FpValue<int>(-1,e.stream_count,mesh),subs=FpValue<int>(-1,e.submesh_count,mesh);
    snapshot.index_format=FpValue<int>(-1,e.index_format,mesh);
    if(snapshot.count<=0||snapshot.count>200000||streams<=0||streams>4||subs<=0||subs>64||snapshot.index_format<0||snapshot.index_format>1)return false;
    int managed_count=0;
    if(!GetValue(Contract("unity.mesh.vertex_count.get"),mesh,managed_count)||managed_count!=snapshot.count)return false;
    snapshot.strides.resize(streams);snapshot.streams.resize(streams);
    for(int s=0;s<streams;++s) {
        auto stride=FpValue<int>(-1,e.stride,mesh,s);
        if(stride<=0||stride>512)return false;snapshot.strides[s]=stride;
    }
    for(int attr=0;attr<14;++attr) if(FpValue<bool>(false,e.has_attribute,mesh,attr)) {
        FpAttribute a{attr,FpValue<int>(-1,e.format,mesh,attr),FpValue<int>(-1,e.dimension,mesh,attr),FpValue<int>(-1,e.stream,mesh,attr)};
        int offset=FpValue<int>(-1,e.offset,mesh,attr),size=FpFormatSize(a.format);
        if(!size||a.dimension<1||a.dimension>4||a.stream<0||a.stream>=streams||offset<0||offset+size*a.dimension>snapshot.strides[a.stream])return false;
        snapshot.attributes.push_back(a);snapshot.offsets.push_back(offset);
    }
    for(int s=0;s<streams;++s)
        if(!FpReadBuffer(FpValue<void*>(nullptr,e.vertex_buffer,mesh,s),size_t(snapshot.count)*snapshot.strides[s],snapshot.streams[s]))return false;
    snapshot.submeshes.resize(subs);
    size_t index_count=0;
    for(int s=0;s<subs;++s) {
        auto& sub=snapshot.submeshes[s];
        if(!FpCall(e.get_submesh,mesh,s,&sub)||sub.topology!=0||sub.index_start<0||sub.index_count<0||sub.index_count%3||sub.base_vertex<0)return false;
        index_count=std::max(index_count,size_t(sub.index_start)+size_t(sub.index_count));
    }
    if(!index_count||index_count>6000000)return false;
    return FpReadBuffer(FpValue<void*>(nullptr,e.index_buffer,mesh),index_count*(snapshot.index_format?4:2),snapshot.indices);
}
template<class T> T FpLoad(const uint8_t* p) {T value{};std::memcpy(&value,p,sizeof(T));return value;}
const uint8_t* FpChannel(const FpSnapshot& s,int attr,size_t vertex,FpAttribute& out) {
    for(size_t i=0;i<s.attributes.size();++i)if(s.attributes[i].attribute==attr) {
        out=s.attributes[i];return s.streams[out.stream].data()+vertex*s.strides[out.stream]+s.offsets[i];
    }return nullptr;
}
bool FpDecode(const FpSnapshot& snapshot,std::vector<FpMesh::Vertex>& vertices,std::vector<FpMesh::Part>& parts) {
    vertices.resize(snapshot.count);
    for(int i=0;i<snapshot.count;++i) {
        auto& v=vertices[i];FpAttribute pos,weights,bones;
        auto p=FpChannel(snapshot,0,i,pos),w=FpChannel(snapshot,12,i,weights),b=FpChannel(snapshot,13,i,bones);
        if(!p||pos.format!=0||pos.dimension!=3||!b||bones.dimension<1||bones.dimension>4||
            (bones.format!=6&&bones.format!=8&&bones.format!=10))return false;
        if(w&&(weights.dimension!=bones.dimension||(weights.format!=0&&weights.format!=2&&weights.format!=4)))return false;
        if(!w&&bones.dimension!=1)return false;
        v.position={FpLoad<float>(p),FpLoad<float>(p+4),FpLoad<float>(p+8)};
        for(int j=0;j<bones.dimension;++j) {
            const auto q=b+j*FpFormatSize(bones.format);
            v.bone[j]=bones.format==6?*q:bones.format==8?FpLoad<uint16_t>(q):FpLoad<uint32_t>(q);
            if(!w)v.weight[j]=1;
            else {const auto qweight=w+j*FpFormatSize(weights.format);v.weight[j]=weights.format==0?FpLoad<float>(qweight):weights.format==2?double(*qweight)/255:double(FpLoad<uint16_t>(qweight))/65535;}
        }
    }
    for(const auto& sub:snapshot.submeshes) {
        FpMesh::Part part;part.indices.reserve(sub.index_count);
        for(int i=0;i<sub.index_count;++i) {
            size_t at=size_t(sub.index_start+i)*(snapshot.index_format?4:2);
            uint64_t id=(snapshot.index_format?FpLoad<uint32_t>(&snapshot.indices[at]):FpLoad<uint16_t>(&snapshot.indices[at]))+uint64_t(sub.base_vertex);
            if(id>=vertices.size())return false;part.indices.push_back(uint32_t(id));
        }
        parts.push_back(std::move(part));
    }
    return true;
}
template<class T> bool FpArray(void* array,std::vector<T>& output) {
    auto& e=g_fp_engine;
    size_t n=FpValue<size_t>(0,e.array_length,array);
    if(!n||n>4096||FpValue<uint32_t>(0,e.array_bytes,array)!=n*sizeof(T))return false;
    output.resize(n);return FpCopy(output.data(),static_cast<uint8_t*>(array)+e.header,n*sizeof(T));
}
using FpMatrix=std::array<float,16>;
bool FpBindOrigin(const FpMatrix& m,FpMesh::Point& origin) {
    FpMesh::Point a{m[0],m[1],m[2]},b{m[4],m[5],m[6]},c{m[8],m[9],m[10]},t{m[12],m[13],m[14]};
    double det=FpMesh::Dot(a,FpMesh::Cross(b,c));if(!std::isfinite(det)||std::abs(det)<1e-10)return false;
    origin={-FpMesh::Dot(t,FpMesh::Cross(b,c))/det,-FpMesh::Dot(a,FpMesh::Cross(t,c))/det,-FpMesh::Dot(a,FpMesh::Cross(b,t))/det};
    return FpMesh::Finite(origin);
}
bool FpPalette(void* renderer,void* mesh,std::vector<void*>& bones,std::vector<uint8_t>& kinds,FpMesh::Frame& frame) {
    FpRoot bone_array(Invoke(Contract("unity.skinned_mesh_renderer.bones.get"),renderer,nullptr));
    FpRoot bind_array(Invoke(Contract("unity.mesh.bindposes.get"),mesh,nullptr));
    std::vector<FpMatrix> matrices;
    if(!bone_array||!bind_array||!FpArray(bone_array.object,bones)||!FpArray(bind_array.object,matrices)||matrices.size()!=bones.size())return false;
    kinds.resize(bones.size());int head=-1,neck=-1;
    for(size_t i=0;i<bones.size();++i) {
        if(bones[i]==g_first_person.head)head=int(i);
        if(bones[i]==g_first_person.neck)neck=int(i);
        auto current=bones[i];
        for(int depth=0;current&&depth<32;++depth) {
            if(current==g_first_person.head){kinds[i]=1;break;}
            current=Invoke(Contract("unity.transform.parent.get"),current,nullptr);
        }
        if(bones[i]==g_first_person.neck)kinds[i]=2;
    }
    if(head>=0&&neck>=0) {
        FpMesh::Point h,n;
        if(FpBindOrigin(matrices[head],h)&&FpBindOrigin(matrices[neck],n)) {
            frame.origin=n;frame.axis=h-n;frame.length=FpMesh::Length(frame.axis);
        }
    }
    return true;
}
struct FpPatch {
    FpRoot renderer,source,copy,shadow;
    std::vector<void*> bones;
    bool offscreen=false,assigned=false;
};
struct FpMeshSession {
    std::vector<FpPatch> patches;
    BetterEndfield::FirstPerson::RetryBudget attempts;
    uint64_t last_scan=0;
    bool scanned=false,fill=false;
    float range=1;
};
FpMeshSession g_fp_mesh_session;

void FpDestroy(void* mesh) {if(mesh&&IsObjectAlive(mesh)){void* p[]={mesh};InvokeVoid(Contract("unity.object.destroy"),nullptr,p);}}
bool FpRestore(FpPatch& patch) {
    auto& e=g_fp_engine;
    if(patch.assigned&&patch.renderer&&IsObjectAlive(patch.renderer.object)) {
        void* current=FpValue<void*>(nullptr,e.get_mesh,patch.renderer.object);
        if(current==patch.copy.object) {
            if(!IsObjectAlive(patch.source.object)||!FpCall(e.set_mesh,patch.renderer.object,patch.source.object)||
               FpValue<void*>(nullptr,e.get_mesh,patch.renderer.object)!=patch.source.object)return false;
        }
        if((current==patch.copy.object||current==patch.source.object)&&FpValue<bool>(false,e.get_offscreen,patch.renderer.object))
            FpCall(e.set_offscreen,patch.renderer.object,patch.offscreen);
        if(FpValue<void*>(nullptr,e.get_shadow,patch.renderer.object)==patch.source.object) {
            if(patch.shadow.object&&!IsObjectAlive(patch.shadow.object))return false;
            if(!FpCall(e.set_shadow,patch.renderer.object,patch.shadow.object)||FpValue<void*>(nullptr,e.get_shadow,patch.renderer.object)!=patch.shadow.object)return false;
        }
    }
    FpDestroy(patch.copy.object);patch.copy.Reset();return true;
}
void ReleaseNeckCap() {
    std::vector<FpPatch> retained;
    for(auto& patch:g_fp_mesh_session.patches)if(!FpRestore(patch))retained.push_back(std::move(patch));
    g_fp_mesh_session.patches=std::move(retained);g_fp_mesh_session.attempts.Clear();g_fp_mesh_session.scanned=false;
    if(!g_fp_mesh_session.patches.empty())Log("First person mesh: restoration deferred; keeping assigned meshes alive.");
}
bool FpSameVertices(const FpSnapshot& a,const FpSnapshot& b) {
    return a.count==b.count&&a.strides==b.strides&&a.offsets==b.offsets&&a.attributes.size()==b.attributes.size()&&
        std::equal(a.attributes.begin(),a.attributes.end(),b.attributes.begin(),[](const auto& x,const auto& y){return x.attribute==y.attribute&&x.format==y.format&&x.dimension==y.dimension&&x.stream==y.stream;})&&a.streams==b.streams;
}
bool FpBuildPatch(const HeadPartProbe& part,FpPatch& patch,bool fill,float range) {
    auto& e=g_fp_engine;
    patch.renderer=FpRoot(part.renderer);patch.source=FpRoot(FpValue<void*>(nullptr,e.get_mesh,part.renderer));
    patch.shadow=FpRoot(FpValue<void*>(nullptr,e.get_shadow,part.renderer));
    if(!patch.renderer||!patch.source||(patch.shadow.object&&!patch.shadow))return false;
    patch.offscreen=FpValue<bool>(false,e.get_offscreen,part.renderer);
    FpMesh::Frame frame;std::vector<uint8_t> kinds;
    if(!FpPalette(part.renderer,patch.source.object,patch.bones,kinds,frame))return false;
    if(!part.matched&&!std::any_of(kinds.begin(),kinds.end(),[](auto k){return k!=0;}))return false;
    FpSnapshot original;
    if(!FpReadMesh(patch.source.object,original)) {Log("First person mesh: GPU capture failed for "+part.object_name);return false;}
    std::vector<FpMesh::Vertex> vertices;std::vector<FpMesh::Part> parts;
    if(!FpDecode(original,vertices,parts)){Log("First person mesh: unsupported vertex encoding for "+part.object_name);return false;}
    auto result=FpMesh::Build(vertices,parts,kinds,frame,part.matched,fill&&!part.matched,range);
    if(!result.error.empty()){Log("First person mesh: "+result.error+" in "+part.object_name);return false;}
    if(!result.hidden_triangles&&!result.cap_triangles)return false;
    patch.copy=FpRoot(FpValue<void*>(nullptr,e.clone,patch.source.object));
    if(!patch.copy||patch.copy.object==patch.source.object){patch.copy.Reset();return false;}
    std::vector<uint32_t> indices;std::vector<FpSubmesh> subs=original.submeshes;
    for(size_t s=0;s<result.parts.size();++s) {
        auto& sub=subs[s];const auto& part_indices=result.parts[s].indices;
        sub.index_start=int(indices.size());sub.index_count=int(part_indices.size());sub.base_vertex=0;
        if(!part_indices.empty()) {auto minmax=std::minmax_element(part_indices.begin(),part_indices.end());sub.first_vertex=int(*minmax.first);sub.vertex_count=int(*minmax.second-*minmax.first+1);}
        indices.insert(indices.end(),part_indices.begin(),part_indices.end());
    }
    int index_format=original.index_format;
    if(original.count>65535)index_format=1;
    size_t element=index_format?4:2;
    std::vector<uint8_t> bytes(indices.size()*element);
    for(size_t i=0;i<indices.size();++i) {
        if(index_format)std::memcpy(bytes.data()+i*4,&indices[i],4);
        else {uint16_t v=uint16_t(indices[i]);std::memcpy(bytes.data()+i*2,&v,2);}
    }
    // Force a fresh index allocation on the private clone. Vertex streams stay
    // unchanged, including HG's packed normals, skinning and material channels.
    bool ok=FpCall(e.set_index_params,patch.copy.object,int(indices.size()+3),index_format)&&
        FpCall(e.set_index_params,patch.copy.object,int(indices.size()),index_format)&&
        FpCall(e.set_index_data,patch.copy.object,bytes.data(),0,0,int(indices.size()),int(element),0);
    for(size_t s=0;ok&&s<subs.size();++s)ok=FpCall(e.set_submesh,patch.copy.object,int(s),&subs[s],10);
    ok=ok&&FpCall(e.upload,patch.copy.object,false);
    FpSnapshot verified,source_after;
    ok=ok&&FpReadMesh(patch.copy.object,verified)&&FpSameVertices(original,verified)&&verified.indices==bytes&&
        FpReadMesh(patch.source.object,source_after)&&FpSameVertices(original,source_after)&&source_after.indices==original.indices;
    std::vector<void*> bones_now;std::vector<uint8_t> kinds_now;FpMesh::Frame unused;
    ok=ok&&FpPalette(part.renderer,patch.copy.object,bones_now,kinds_now,unused)&&bones_now==patch.bones&&
        FpValue<void*>(nullptr,e.get_mesh,part.renderer)==patch.source.object&&
        FpValue<void*>(nullptr,e.get_shadow,part.renderer)==patch.shadow.object;
    FpRoot original_bind(Invoke(Contract("unity.mesh.bindposes.get"),patch.source.object,nullptr));
    FpRoot copied_bind(Invoke(Contract("unity.mesh.bindposes.get"),patch.copy.object,nullptr));
    std::vector<FpMatrix> original_matrices,copied_matrices;
    ok=ok&&original_bind&&copied_bind&&FpArray(original_bind.object,original_matrices)&&
        FpArray(copied_bind.object,copied_matrices)&&original_matrices==copied_matrices;
    if(!ok){Log("First person mesh: clone verification failed for "+part.object_name);FpDestroy(patch.copy.object);patch.copy.Reset();return false;}
    patch.assigned=true;
    void* shadow=patch.shadow.object?patch.shadow.object:patch.source.object;
    ok=FpCall(e.set_shadow,part.renderer,shadow)&&FpValue<void*>(nullptr,e.get_shadow,part.renderer)==shadow&&
        FpCall(e.set_offscreen,part.renderer,true)&&FpCall(e.set_mesh,part.renderer,patch.copy.object)&&
        FpValue<void*>(nullptr,e.get_mesh,part.renderer)==patch.copy.object;
    if(!ok){Log("First person mesh: renderer binding failed for "+part.object_name);FpRestore(patch);return false;}
    Log("First person mesh: "+part.object_name+", hidden="+std::to_string(result.hidden_triangles)+
        ", cap="+std::to_string(result.cap_triangles)+", rings="+std::to_string(result.rings)+", GPU verified, original shadow retained.");
    return true;
}
void EnsureNeckCap() {
    if(!g_first_person_hide_head.load()||!g_first_person.head||!g_fp_engine.Init())return;
    auto& state=g_fp_mesh_session;auto& e=g_fp_engine;
    if(!FpValue<bool>(false,e.supports_readback))return;
    const bool fill=g_first_person_fill_neck_hole.load();const float range=g_first_person_neck_plug_scale.load();
    if(state.scanned&&(fill!=state.fill||range!=state.range))ReleaseNeckCap();
    if(state.scanned&&g_first_person_pump_frames-state.last_scan<30)return;
    state.scanned=true;state.last_scan=g_first_person_pump_frames;state.fill=fill;state.range=range;
    for(auto it=state.patches.begin();it!=state.patches.end();) {
        bool stale=!IsObjectAlive(it->renderer.object)||FpValue<void*>(nullptr,e.get_mesh,it->renderer.object)!=it->copy.object;
        if(!stale) {
            FpRoot bones(Invoke(Contract("unity.skinned_mesh_renderer.bones.get"),it->renderer.object,nullptr));
            std::vector<void*> current;stale=!bones||!FpArray(bones.object,current)||current!=it->bones;
        }
        if(stale&&FpRestore(*it)) {
            state.attempts.Forget(it->renderer.object);
            it=state.patches.erase(it);
        } else ++it;
    }
    auto model=FindModelTransform();if(!model)return;
    std::vector<HeadPartProbe> parts;std::string tree;int visited=0,logged=kMaxPartLogEntries;
    ScanPartNodes(model,0,visited,parts,tree,logged);
    state.attempts.Prune([&](void* renderer) {
        return std::any_of(parts.begin(),parts.end(),[&](const auto& part) {
            return part.renderer==renderer;
        });
    });
    for(const auto& part:parts) {
        if(!part.renderer||part.vertex_count<=0)continue;
        if(std::any_of(state.patches.begin(),state.patches.end(),[&](const auto& p){return p.renderer.object==part.renderer;}))continue;
        auto source=FpValue<void*>(nullptr,e.get_mesh,part.renderer);if(!source)continue;
        if(!state.attempts.Try(part.renderer,source))continue;
        FpPatch patch;
        try {
            if(FpBuildPatch(part,patch,fill,range))state.patches.push_back(std::move(patch));
            else if(patch.copy&&patch.assigned)state.patches.push_back(std::move(patch));
        } catch(const std::exception& error) {
            Log(std::string("First person mesh: capture/build aborted: ")+error.what());
            if(!FpRestore(patch))state.patches.push_back(std::move(patch));
        }
        // Process one renderer per frame to avoid capturing every LOD at once.
        state.last_scan=g_first_person_pump_frames-30;
        break;
    }
}
bool ApplyHeadPartHide() {g_first_person.head_hide_applied=true;return true;}
void ReleaseHeadPartHide() {
    g_first_person.head_hide_applied=false;
    ReleaseNeckCap();
}
void ReapplyHeadPartHide() {EnsureNeckCap();}

void ExitGameFirstPersonMode() {
    void* controller=FindSnapshotCameraController();
    bool active=false;
    if(!controller||!ReadBoolField(controller,"snapshot.is_first_person",active)||!active)return;
    bool off=false;void* parameters[]{&off};
    if(InvokeVoid(Contract("snapshot.set_first_person"),controller,parameters))
        InvokeVoid(Contract("snapshot.show_char"),controller,nullptr);
}
