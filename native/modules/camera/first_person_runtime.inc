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
    bool attempted=false,ready=false,gpu_ready=false;

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
        const bool request_abi=g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll","UnityEngine.Rendering","AsyncGPUReadbackRequest",&request_type)==BE_Result_Ok&&
           FpValue<int32_t>(0,value_size,request_type.class_info,&alignment)==sizeof(FpReadback)&&
           CheckField("UnityEngine.Rendering","AsyncGPUReadbackRequest","m_Ptr",boxed)&&
           CheckField("UnityEngine.Rendering","AsyncGPUReadbackRequest","m_Version",boxed+8);
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
        bool gpu=ready&&request_abi;
#define FP_GPU(slot,name) gpu=Bind(slot,name)&&gpu
        FP_GPU(vertex_buffer,"UnityEngine.Mesh::GetVertexBufferImpl");
        FP_GPU(index_buffer,"UnityEngine.Mesh::GetIndexBufferImpl");
        FP_GPU(buffer_count,"UnityEngine.GraphicsBuffer::get_count");
        FP_GPU(buffer_stride,"UnityEngine.GraphicsBuffer::get_stride");
        FP_GPU(buffer_destroy,"UnityEngine.GraphicsBuffer::DestroyBuffer");
        FP_GPU(request,"UnityEngine.Rendering.AsyncGPUReadback::Request_Internal_GraphicsBuffer_2_Injected");
        FP_GPU(wait,"UnityEngine.Rendering.AsyncGPUReadbackRequest::WaitForCompletion_Injected");
        FP_GPU(done,"UnityEngine.Rendering.AsyncGPUReadbackRequest::IsDone_Injected");
        FP_GPU(error,"UnityEngine.Rendering.AsyncGPUReadbackRequest::HasError_Injected");
        FP_GPU(data_size,"UnityEngine.Rendering.AsyncGPUReadbackRequest::GetLayerDataSize_Injected");
        FP_GPU(data,"UnityEngine.Rendering.AsyncGPUReadbackRequest::GetDataRaw_Injected");
        FP_GPU(supports_readback,"UnityEngine.SystemInfo::SupportsAsyncGPUReadback");
#undef FP_GPU
        gpu_ready=gpu&&FpValue<bool>(false,supports_readback);
        Log(std::string("First person mesh: clone/index bindings ")+(ready?"ready":"unavailable")+
            ", GPU readback "+(gpu_ready?"ready.":"unavailable; CPU geometry/palette fallback remains available."));
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
bool FpReadMeshLayout(void* mesh,FpSnapshot& snapshot) {
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
    snapshot.submeshes.resize(subs);
    size_t index_count=0;
    for(int s=0;s<subs;++s) {
        auto& sub=snapshot.submeshes[s];
        if(!FpCall(e.get_submesh,mesh,s,&sub)||sub.topology!=0||sub.index_start<0||sub.index_count<0||sub.index_count%3||sub.base_vertex<0)return false;
        index_count=std::max(index_count,size_t(sub.index_start)+size_t(sub.index_count));
    }
    return index_count>0&&index_count<=6000000;
}
bool FpReadMesh(void* mesh,FpSnapshot& snapshot) {
    auto& e=g_fp_engine;
    if(!e.gpu_ready||!FpReadMeshLayout(mesh,snapshot))return false;
    for(size_t s=0;s<snapshot.streams.size();++s)
        if(!FpReadBuffer(FpValue<void*>(nullptr,e.vertex_buffer,mesh,int(s)),size_t(snapshot.count)*snapshot.strides[s],snapshot.streams[s]))return false;
    size_t index_count=0;
    for(const auto& sub:snapshot.submeshes)index_count=std::max(index_count,size_t(sub.index_start)+size_t(sub.index_count));
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
        if(!w&&bones.dimension!=1)return false; // Other GPU layouts need explicit weights.
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
bool FpTailName(const std::string& name) {
    auto lower=LowerAscii(name);
    return lower=="tail"||lower.rfind("tail_",0)==0||lower.rfind("bip001_tail",0)==0||
        lower.rfind("bip001 tail",0)==0;
}
uint8_t FpBoneKind(void* bone) {
    if(!bone||!IsObjectAlive(bone))return 0;
    if(bone==g_first_person.neck)return 2;
    bool tail=false;
    for(int depth=0;bone&&depth<48;++depth) {
        if(bone==g_first_person.head)return 1;
        if(bone==g_first_person.body)return tail?1:0;
        if(!IsObjectAlive(bone))return 0;
        tail=tail||FpTailName(ObjectName(bone));
        bone=Invoke(Contract("unity.transform.parent.get"),bone,nullptr);
    }
    return 0; // A detached or foreign tail name cannot hide body geometry.
}
bool FpRendererBones(void* renderer,std::vector<void*>& bones) {
    void* array=Invoke(Contract("unity.skinned_mesh_renderer.bones.get"),renderer,nullptr);
    const int count=ManagedArrayLength(array);
    if(count<=0||count>4096)return false;
    bones.resize(count);
    for(int i=0;i<count;++i)bones[i]=ManagedArrayValue(array,i);
    return true;
}

struct FpCpuGeometry {
    std::vector<FpMesh::Vertex> vertices;
    std::vector<FpMesh::Part> parts;
    std::vector<uint32_t> indices;
    std::vector<BE_CustomModelDrawV1> draws;
};
BE_Result BE_CALL FpCaptureCpuGeometry(void* context,const BE_CustomModelGeometryV1* view) {
    if(!context||!view||view->struct_size<sizeof(*view)||view->version!=1||
        !view->positions_xyz||!view->skin||!view->indices||!view->draws||
        !view->vertex_count||view->vertex_count>200000||!view->index_count||view->index_count>6000000||
        !view->draw_count||view->draw_count>64||
        (view->skin_stride!=4&&view->skin_stride!=12&&view->skin_stride!=32))return BE_Result_InvalidArgument;
    try {
        FpCpuGeometry copy;
        copy.vertices.resize(view->vertex_count);
        copy.indices.assign(view->indices,view->indices+view->index_count);
        copy.draws.assign(view->draws,view->draws+view->draw_count);
        for(uint32_t i=0;i<view->vertex_count;++i) {
            auto& v=copy.vertices[i];const auto* p=view->positions_xyz+i*3;
            v.position={p[0],p[1],p[2]};
            const auto* skin=view->skin+size_t(i)*view->skin_stride;
            for(int j=0;j<(view->skin_stride==4?1:4);++j) {
                v.bone[j]=view->skin_stride==32?FpLoad<uint32_t>(skin+16+j*4):skin[view->skin_stride-4+j];
                v.weight[j]=view->skin_stride==4?1:view->skin_stride==12?
                    double(FpLoad<uint16_t>(skin+j*2))/65535:FpLoad<float>(skin+j*4);
                if(!std::isfinite(v.weight[j])||v.weight[j]<0||v.weight[j]>1.001)return BE_Result_InvalidArgument;
            }
            if(!FpMesh::Finite(v.position))return BE_Result_InvalidArgument;
        }
        for(const auto& draw:copy.draws) {
            if(draw.index_count%3||draw.first_index>view->index_count||
                draw.index_count>view->index_count-draw.first_index)return BE_Result_InvalidArgument;
            FpMesh::Part part;
            part.indices.assign(copy.indices.begin()+draw.first_index,copy.indices.begin()+draw.first_index+draw.index_count);
            for(auto id:part.indices)if(id>=view->vertex_count)return BE_Result_InvalidArgument;
            copy.parts.push_back(std::move(part));
        }
        *static_cast<FpCpuGeometry*>(context)=std::move(copy);
        return BE_Result_Ok;
    }catch(...){return BE_Result_Failed;}
}
BE_QueryCustomModelGeometryV1 FpGeometryQuery() {
#if defined(__ANDROID__)
    return BetterEndfield_QueryCustomModelGeometryV1;
#else
    const auto module=GetModuleHandleW(L"BetterEndfield.CustomModel.dll");
    return module?reinterpret_cast<BE_QueryCustomModelGeometryV1>(GetProcAddress(module,"BetterEndfield_QueryCustomModelGeometryV1")):nullptr;
#endif
}
bool FpReadCpuGeometry(void* mesh,FpSnapshot& snapshot,std::vector<FpMesh::Vertex>& vertices,
    std::vector<FpMesh::Part>& parts,BE_Result* query_result=nullptr) {
    const auto query=FpGeometryQuery();if(!query){if(query_result)*query_result=BE_Result_NotFound;return false;}
    FpCpuGeometry copy;
    const auto status=query(mesh,FpCaptureCpuGeometry,&copy);if(query_result)*query_result=status;
    if(status!=BE_Result_Ok||!FpReadMeshLayout(mesh,snapshot)||
        snapshot.count!=int(copy.vertices.size())||snapshot.submeshes.size()!=copy.draws.size())return false;
    for(size_t i=0;i<copy.draws.size();++i) {
        const auto& sub=snapshot.submeshes[i];const auto& draw=copy.draws[i];
        if(sub.base_vertex||sub.index_start!=int(draw.first_index)||sub.index_count!=int(draw.index_count))return false;
    }
    snapshot.indices.resize(copy.indices.size()*(snapshot.index_format?4:2));
    for(size_t i=0;i<copy.indices.size();++i) {
        if(snapshot.index_format)std::memcpy(snapshot.indices.data()+i*4,&copy.indices[i],4);
        else {
            if(copy.indices[i]>65535)return false;
            const uint16_t id=uint16_t(copy.indices[i]);std::memcpy(snapshot.indices.data()+i*2,&id,2);
        }
    }
    vertices=std::move(copy.vertices);parts=std::move(copy.parts);return true;
}
bool FpShadowProxy(const HeadPartProbe& part) {
    for(const auto* name:{&part.object_name,&part.renderer_name,&part.mesh_name}) {
        auto lower=LowerAscii(*name);
        if(lower.find("shadowproxy")!=std::string::npos||lower.find("hairshadow")!=std::string::npos)return true;
    }
    return false;
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
        kinds[i]=FpBoneKind(bones[i]);
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
    std::vector<std::string> bone_names;
    void* assigned_shadow=nullptr;
    bool offscreen=false,assigned=false,direct_hidden=false,direct_disabled=false;
    int shadow_mode=0;
};
struct FpMeshSession {
    std::vector<FpPatch> patches;
    BetterEndfield::FirstPerson::RetryBudget attempts;
    std::unordered_map<void*,std::vector<void*>> observed_bones;
    uint64_t last_scan=0;
    size_t next_renderer=0;
    bool scanned=false,fill=false;
    bool restoration_deferred=false;
    float range=1;
};
FpMeshSession g_fp_mesh_session;

void FpDestroy(void* mesh) {if(mesh&&IsObjectAlive(mesh)){void* p[]={mesh};InvokeVoid(Contract("unity.object.destroy"),nullptr,p);}}
bool FpRestore(FpPatch& patch) {
    if (patch.direct_hidden) {
        if (patch.renderer && IsObjectAlive(patch.renderer.object)) {
            if(patch.direct_disabled) {
                std::vector<void*> current_bones;
                if(Invoke(Contract("unity.skinned_mesh_renderer.shared_mesh.get"),patch.renderer.object,nullptr)!=patch.source.object||
                    !FpRendererBones(patch.renderer.object,current_bones)||current_bones!=patch.bones) {
                    // BEM/game explicitly owns visibility after a mesh/palette
                    // handoff; a hidden replacement must remain hidden.
                    patch.renderer.Reset();return true;
                }
                bool enabled=true;
                if(!GetValue(Contract("unity.renderer.enabled.get"),patch.renderer.object,enabled))return false;
                if(!enabled) {
                    enabled=true;void* parameters[1]{&enabled};
                    if(!InvokeVoid(Contract("unity.renderer.enabled.set"),patch.renderer.object,parameters)||
                        !GetValue(Contract("unity.renderer.enabled.get"),patch.renderer.object,enabled)||!enabled)return false;
                }
                patch.renderer.Reset();return true;
            }
            int current=0;
            if(!GetValue(Contract("unity.renderer.shadow_mode.get"),patch.renderer.object,current))return false;
            // A game/BEM write supersedes our ShadowsOnly value. Restore only
            // the state this patch still owns, never overwrite an external mode.
            if(current==3) {
                void* parameters[1]{&patch.shadow_mode};
                if(!InvokeVoid(Contract("unity.renderer.shadow_mode.set"),patch.renderer.object,parameters)||
                    !GetValue(Contract("unity.renderer.shadow_mode.get"),patch.renderer.object,current)||current!=patch.shadow_mode)return false;
            }
        }
        patch.renderer.Reset();
        return true;
    }
    auto& e=g_fp_engine;
    if(patch.assigned&&patch.renderer&&IsObjectAlive(patch.renderer.object)) {
        void* current=FpValue<void*>(nullptr,e.get_mesh,patch.renderer.object);
        if(current==patch.copy.object) {
            std::vector<void*> current_bones;
            if(!FpRendererBones(patch.renderer.object,current_bones))return false;
            if(current_bones!=patch.bones) {
                // Object replacement with the same semantic palette is valid.
                // A reordered/new palette must not receive old bind poses.
                if(current_bones.size()!=patch.bone_names.size())return false;
                for(size_t i=0;i<current_bones.size();++i)
                    if(!current_bones[i]||!IsObjectAlive(current_bones[i])||patch.bone_names[i].empty()||
                        ObjectName(current_bones[i])!=patch.bone_names[i])return false;
            }
            if(!IsObjectAlive(patch.source.object)||!FpCall(e.set_mesh,patch.renderer.object,patch.source.object)||
               FpValue<void*>(nullptr,e.get_mesh,patch.renderer.object)!=patch.source.object)return false;
        }
        if(!patch.offscreen&&FpValue<bool>(false,e.get_offscreen,patch.renderer.object)) {
            if(!FpCall(e.set_offscreen,patch.renderer.object,false)||FpValue<bool>(true,e.get_offscreen,patch.renderer.object))return false;
        }
        if(patch.assigned_shadow!=patch.shadow.object&&
            FpValue<void*>(nullptr,e.get_shadow,patch.renderer.object)==patch.assigned_shadow) {
            // If another package/LOD now owns the visible mesh, use that mesh's
            // complete silhouette instead of resurrecting an old package proxy.
            void* restore=(current==patch.copy.object||current==patch.source.object)?patch.shadow.object:current;
            if(restore&&!IsObjectAlive(restore))return false;
            if(!FpCall(e.set_shadow,patch.renderer.object,restore)||FpValue<void*>(nullptr,e.get_shadow,patch.renderer.object)!=restore)return false;
        }
    }
    FpDestroy(patch.copy.object);patch.copy.Reset();return true;
}
void ReleaseNeckCap() {
    std::vector<FpPatch> retained;
    for(auto& patch:g_fp_mesh_session.patches)if(!FpRestore(patch))retained.push_back(std::move(patch));
    g_fp_mesh_session.patches=std::move(retained);g_fp_mesh_session.attempts.Clear();g_fp_mesh_session.observed_bones.clear();g_fp_mesh_session.next_renderer=0;g_fp_mesh_session.scanned=false;
    const bool deferred=!g_fp_mesh_session.patches.empty();
    if(deferred&&!g_fp_mesh_session.restoration_deferred)
        Log("First person mesh: restoration deferred; keeping assigned meshes alive.");
    g_fp_mesh_session.restoration_deferred=deferred;
}
bool FpTryDirectNamedHide(const HeadPartProbe& part, FpPatch& patch) {
    if (!part.renderer || FpShadowProxy(part) || !g_first_person.head ||
        !Contract("unity.renderer.shadow_mode.get")->resolved ||
        !Contract("unity.renderer.shadow_mode.set")->resolved) return false;
    if(!FpRendererBones(part.renderer,patch.bones)||
        !std::all_of(patch.bones.begin(),patch.bones.end(),[](auto bone){return FpBoneKind(bone)==1;}))return false;
    bool enabled = true;
    if (!GetValue(Contract("unity.renderer.enabled.get"), part.renderer, enabled)||!enabled)return false;
    if(!GetValue(Contract("unity.renderer.shadow_mode.get"),part.renderer,patch.shadow_mode)||
        patch.shadow_mode<0||patch.shadow_mode>2)return false;
    patch.renderer = FpRoot(part.renderer);
    patch.source = FpRoot(Invoke(Contract("unity.skinned_mesh_renderer.shared_mesh.get"),part.renderer,nullptr));
    if(!patch.renderer||!patch.source)return false;
    patch.direct_hidden = true;
    patch.direct_disabled = patch.shadow_mode==0;
    patch.assigned = true;
    if(patch.direct_disabled) {
        if(!Contract("unity.renderer.enabled.set")->resolved){patch.assigned=false;return false;}
        bool hidden=false;void* parameters[1]{&hidden};
        if(!InvokeVoid(Contract("unity.renderer.enabled.set"),part.renderer,parameters)||
            !GetValue(Contract("unity.renderer.enabled.get"),part.renderer,enabled)||enabled) {
            if(FpRestore(patch))patch.assigned=false;
            return false;
        }
        Log("First person mesh: standalone noncasting head/tail renderer hidden; shadow proxies retained: "+part.object_name);
        return true;
    }
    int mode=3,current=0;
    void* parameters[1]{&mode};
    if (!InvokeVoid(Contract("unity.renderer.shadow_mode.set"), part.renderer, parameters)||
        !GetValue(Contract("unity.renderer.shadow_mode.get"),part.renderer,current)||current!=mode) {
        if(FpRestore(patch))patch.assigned=false;
        return false;
    }
    Log("First person mesh: ShadowsOnly head/tail renderer, object=" + part.object_name +
        ", renderer=" + part.renderer_name + ", mesh=" + part.mesh_name +
        ", original casting mode="+std::to_string(patch.shadow_mode));
    return true;
}
bool FpSameVertices(const FpSnapshot& a,const FpSnapshot& b) {
    return a.count==b.count&&a.strides==b.strides&&a.offsets==b.offsets&&a.attributes.size()==b.attributes.size()&&
        std::equal(a.attributes.begin(),a.attributes.end(),b.attributes.begin(),[](const auto& x,const auto& y){return x.attribute==y.attribute&&x.format==y.format&&x.dimension==y.dimension&&x.stream==y.stream;})&&a.streams==b.streams;
}
bool FpBuildPatch(const HeadPartProbe& part,FpPatch& patch,bool fill,float range,bool* retry_later=nullptr) {
    auto& e=g_fp_engine;
    patch.renderer=FpRoot(part.renderer);patch.source=FpRoot(FpValue<void*>(nullptr,e.get_mesh,part.renderer));
    patch.shadow=FpRoot(FpValue<void*>(nullptr,e.get_shadow,part.renderer));
    if(!patch.renderer||!patch.source||(patch.shadow.object&&!patch.shadow))return false;
    patch.offscreen=FpValue<bool>(false,e.get_offscreen,part.renderer);
    FpMesh::Frame frame;std::vector<uint8_t> kinds;
    if(!FpPalette(part.renderer,patch.source.object,patch.bones,kinds,frame)) {
        if(retry_later)*retry_later=true;return false;
    }
    for(auto bone:patch.bones)patch.bone_names.push_back(bone&&IsObjectAlive(bone)?ObjectName(bone):std::string{});
    if(!part.matched&&!std::any_of(kinds.begin(),kinds.end(),[](auto k){return k!=0;}))return false;
    FpSnapshot original;
    std::vector<FpMesh::Vertex> vertices;std::vector<FpMesh::Part> parts;
    BE_Result query_status=BE_Result_NotFound;
    const bool cpu=FpReadCpuGeometry(patch.source.object,original,vertices,parts,&query_status);
    if(!cpu) {
        original={};
        if(!FpReadMesh(patch.source.object,original)) {
            if(retry_later)*retry_later=query_status==BE_Result_NotReady;return false;
        }
        if(!FpDecode(original,vertices,parts)){Log("First person mesh: unsupported vertex encoding for "+part.object_name);return false;}
    }
    auto result=FpMesh::Build(vertices,parts,kinds,frame,false,fill,range);
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
    if(cpu) {
        ok=ok&&FpReadMeshLayout(patch.copy.object,verified)&&FpSameVertices(original,verified)&&
            verified.index_format==index_format&&verified.submeshes.size()==subs.size()&&
            FpReadMeshLayout(patch.source.object,source_after)&&FpSameVertices(original,source_after);
        for(size_t i=0;ok&&i<subs.size();++i) {
            const auto& a=verified.submeshes[i];const auto& b=subs[i];
            ok=a.topology==b.topology&&a.index_start==b.index_start&&a.index_count==b.index_count&&a.base_vertex==b.base_vertex;
        }
    }else {
        ok=ok&&FpReadMesh(patch.copy.object,verified)&&FpSameVertices(original,verified)&&verified.indices==bytes&&
            FpReadMesh(patch.source.object,source_after)&&FpSameVertices(original,source_after)&&source_after.indices==original.indices;
    }
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
    // A BEM replacement owns a new silhouette. Its complete source mesh is the
    // shadow source; an old world/mobile proxy belongs to the original model.
    void* shadow=cpu?patch.source.object:patch.shadow.object?patch.shadow.object:patch.source.object;
    patch.assigned_shadow=shadow;
    ok=FpCall(e.set_shadow,part.renderer,shadow)&&FpValue<void*>(nullptr,e.get_shadow,part.renderer)==shadow&&
        FpCall(e.set_offscreen,part.renderer,true)&&FpCall(e.set_mesh,part.renderer,patch.copy.object)&&
        FpValue<void*>(nullptr,e.get_mesh,part.renderer)==patch.copy.object;
    if(!ok){Log("First person mesh: renderer binding failed for "+part.object_name);FpRestore(patch);return false;}
    Log("First person mesh: "+part.object_name+", renderer="+part.renderer_name+
        ", mesh="+part.mesh_name+", hidden="+std::to_string(result.hidden_triangles)+
        ", cap="+std::to_string(result.cap_triangles)+", rings="+std::to_string(result.rings)+
        (cpu?", BEM CPU geometry, clone layout verified, complete replacement shadow retained.":", GPU bytes verified, original shadow retained."));
    return true;
}
void EnsureNeckCap() {
    if(!g_first_person_hide_head.load()||!g_first_person.head)return;
    auto& state=g_fp_mesh_session;auto& e=g_fp_engine;
    const bool engine_ready=g_fp_engine.Init();
    const bool fill=g_first_person_fill_neck_hole.load();const float range=g_first_person_neck_plug_scale.load();
    if(state.scanned&&(fill!=state.fill||range!=state.range))ReleaseNeckCap();
    if(state.scanned&&g_first_person_pump_frames-state.last_scan<30)return;
    state.scanned=true;state.last_scan=g_first_person_pump_frames;state.fill=fill;state.range=range;
    for(auto it=state.patches.begin();it!=state.patches.end();) {
        bool stale=!IsObjectAlive(it->renderer.object);
        if(!stale&&it->direct_hidden) {
            int mode=0;bool enabled=true;
            stale=Invoke(Contract("unity.skinned_mesh_renderer.shared_mesh.get"),it->renderer.object,nullptr)!=it->source.object||
                (it->direct_disabled?(!GetValue(Contract("unity.renderer.enabled.get"),it->renderer.object,enabled)||enabled):
                    (!GetValue(Contract("unity.renderer.shadow_mode.get"),it->renderer.object,mode)||mode!=3));
        }
        if(!stale&&!it->direct_hidden) stale=e.get_mesh==nullptr||
            FpValue<void*>(nullptr,e.get_mesh,it->renderer.object)!=it->copy.object;
        if(!stale) {
            std::vector<void*> current;stale=!FpRendererBones(it->renderer.object,current)||current!=it->bones;
        }
        void* renderer=it->renderer.object;
        if(stale&&FpRestore(*it)) {
            state.attempts.Forget(renderer);
            it=state.patches.erase(it);
        } else ++it;
    }
    auto model=g_first_person.body;if(!model||!g_first_person.target_valid||!IsObjectAlive(model))return;
    std::vector<HeadPartProbe> parts;std::string tree;int visited=0,logged=kMaxPartLogEntries;
    ScanPartNodes(model,0,visited,parts,tree,logged);
    if (!tree.empty() && g_diagnostics_enabled.load(std::memory_order_relaxed)) {
        Log("First person mesh scan: visited=" + std::to_string(visited) + tree);
    }
    state.attempts.Prune([&](void* renderer) {
        return std::any_of(parts.begin(),parts.end(),[&](const auto& part) {
            return part.renderer==renderer;
        });
    });
    for(auto it=state.observed_bones.begin();it!=state.observed_bones.end();) {
        if(std::none_of(parts.begin(),parts.end(),[&](const auto& part){return part.renderer==it->first;}))it=state.observed_bones.erase(it);
        else ++it;
    }
    const size_t start=parts.empty()?0:state.next_renderer%parts.size();
    for(size_t scan=0;scan<parts.size();++scan) {
        const size_t part_index=(start+scan)%parts.size();const auto& part=parts[part_index];
        if(!part.renderer||FpShadowProxy(part))continue;
        if(part.vertex_count<=0&&!part.matched)continue;
        if(std::any_of(state.patches.begin(),state.patches.end(),[&](const auto& p){return p.renderer.object==part.renderer;}))continue;
        bool enabled=false;int mode=0;
        if(!GetValue(Contract("unity.renderer.enabled.get"),part.renderer,enabled)||!enabled)continue;
        if(GetValue(Contract("unity.renderer.shadow_mode.get"),part.renderer,mode)&&mode==3)continue;
        auto source=Invoke(Contract("unity.skinned_mesh_renderer.shared_mesh.get"),part.renderer,nullptr);if(!source)continue;
        std::vector<void*> current_bones;FpRendererBones(part.renderer,current_bones);
        auto [observed,inserted]=state.observed_bones.try_emplace(part.renderer,current_bones);
        if(!inserted&&observed->second!=current_bones){state.attempts.Forget(part.renderer);observed->second=std::move(current_bones);}
        if(!state.attempts.Try(part.renderer,source))continue;
        state.next_renderer=(part_index+1)%parts.size();
        FpPatch direct;
        if(FpTryDirectNamedHide(part,direct)||direct.assigned) {
            state.patches.push_back(std::move(direct));
            state.last_scan=g_first_person_pump_frames-30;
            break;
        }
        if(!engine_ready) {
            if(part.matched) Log("First person mesh: clone bindings unavailable; retaining uncertain mixed geometry: "+part.object_name);
            continue;
        }
        FpPatch patch;bool retry_later=false;
        try {
            if(FpBuildPatch(part,patch,fill,range,&retry_later))state.patches.push_back(std::move(patch));
            else if(patch.copy&&patch.assigned)state.patches.push_back(std::move(patch));
            else if(part.matched&&!e.gpu_ready)Log("First person mesh: no usable CPU/GPU geometry; retaining uncertain mixed part: "+part.object_name);
        } catch(const std::exception& error) {
            Log(std::string("First person mesh: capture/build aborted: ")+error.what());
            if(!FpRestore(patch))state.patches.push_back(std::move(patch));
        }
        // Process one renderer per frame to avoid capturing every LOD at once.
        if(retry_later)state.attempts.Forget(part.renderer);
        else state.last_scan=g_first_person_pump_frames-30;
        break;
    }
}
bool ApplyHeadPartHide() {g_first_person.head_hide_applied=true;return true;}
void ReleaseHeadPartHide() {
    g_first_person.head_hide_applied=false;
    ReleaseNeckCap();
}
void ReapplyHeadPartHide() {EnsureNeckCap();}

void ReleaseSnapshotController() {
    if(g_first_person.snapshot_handle&&g_host&&g_host->gchandle_free)
        g_host->gchandle_free(g_host->context,g_first_person.snapshot_handle);
    g_first_person.snapshot_handle=0;
    g_first_person.snapshot_controller=nullptr;
    g_first_person.snapshot_exit_owned=false;
}

// SetFirstPerson(false) schedules post-rotation cleanup for AfterTransition.
// A paused camera can miss that stage. Complete only the exit this session
// requested, through the game's own method and with live required components.
void FinishGameFirstPersonExit() {
    if(!g_first_person.snapshot_exit_owned)return;
    void* controller=g_first_person.snapshot_controller;
    if(!controller||!IsObjectAlive(controller)) {
        ReleaseSnapshotController();
        return;
    }
    bool active=false,pending=false;
    if(!ReadBoolField(controller,"snapshot.is_first_person",active)||
       !ReadBoolField(controller,"snapshot.pending_first_person_exit",pending))return;
    if(active||!pending) {
        // A subsequent user-owned photo session must not be cleared here.
        g_first_person.snapshot_exit_owned=false;
        return;
    }
    void* rotation=ReadObjectField(controller,"snapshot.post_rotation");
    void* offset=ReadObjectField(controller,"snapshot.camera_offset");
    if(!rotation||!offset||!IsObjectAlive(rotation)||!IsObjectAlive(offset))return;
    if(InvokeVoid(Contract("snapshot.clear_first_person"),controller,nullptr)&&
       ReadBoolField(controller,"snapshot.pending_first_person_exit",pending)&&!pending) {
        g_first_person.snapshot_exit_owned=false;
        Log("First person: completed owned Snapshot post-rotation exit.");
    }
}

void ExitGameFirstPersonMode() {
    FinishGameFirstPersonExit();
    void* controller=FindSnapshotCameraController();
    bool active=false;
    if(!controller||!ReadBoolField(controller,"snapshot.is_first_person",active)||!active)return;
    bool off=false;void* parameters[]{&off};
    if(InvokeVoid(Contract("snapshot.set_first_person"),controller,parameters)) {
        g_first_person.snapshot_exit_owned=true;
        FinishGameFirstPersonExit();
    }
}
