#include "../../modules/camera/module.cpp"
#include "eiem_body_fake.h"
#include "test_support.h"
#include <memory>
using namespace BetterEndfield::CameraModule;
namespace {
struct Mesh {
    int count=6,format=1;
    std::vector<FpAttribute> attributes{{0,0,3,0},{4,0,2,1},{12,4,4,2},{13,6,4,2}};
    std::vector<std::vector<uint32_t>> parts{{0,1,2},{3,4,5}};
};
std::array<uint8_t,64> boxed_value{};
std::unordered_map<void*,std::pair<std::unique_ptr<uint8_t[]>,size_t>> arrays;
std::vector<std::unique_ptr<Mesh>> clones;
int roots=0;bool reject_indices=false;
template<class T> void* Box(T value) {std::memcpy(boxed_value.data(),&value,sizeof(value));return boxed_value.data();}
void* Array(size_t count) {
    auto storage=std::make_unique<uint8_t[]>(32+count*4);void* result=storage.get();
    arrays.emplace(result,std::make_pair(std::move(storage),count));return result;
}
void* InvokeFake(void*,const void* method,void* object,void** args,void** exception) {
    *exception=nullptr;auto* mesh=static_cast<Mesh*>(object);
    const std::string_view key=static_cast<const char*>(method);
    if(key=="unity.mesh.vertex_count.get")return Box(mesh->count);
    if(key=="fp.mesh.attributes.count")return Box(int(mesh->attributes.size()));
    if(key=="fp.mesh.attribute")return Box(mesh->attributes.at(*static_cast<int*>(args[0])));
    if(key=="fp.mesh.submeshes.get")return Box(int(mesh->parts.size()));
    if(key=="fp.mesh.submeshes.set"){mesh->parts.resize(*static_cast<int*>(args[0]));return nullptr;}
    if(key=="fp.mesh.index_format.get")return Box(mesh->format);
    if(key=="fp.mesh.index_format.set"){mesh->format=*static_cast<int*>(args[0]);return nullptr;}
    if(key=="fp.mesh.submesh") {
        const int id=*static_cast<int*>(args[0]);FpSubmesh sub{};
        for(int i=0;i<id;++i)sub.index_start+=int(mesh->parts[i].size());
        sub.index_count=int(mesh->parts.at(id).size());sub.vertex_count=mesh->count;return Box(sub);
    }
    if(key=="fp.mesh.index_start"||key=="fp.mesh.index_count"||key=="fp.mesh.base_vertex") {
        const int id=*static_cast<int*>(args[0]);uint32_t start=0;
        for(int i=0;i<id;++i)start+=uint32_t(mesh->parts[i].size());
        return Box(key=="fp.mesh.index_start"?start:key=="fp.mesh.index_count"?uint32_t(mesh->parts.at(id).size()):0u);
    }
    if(key=="fp.array.create")return Array(*static_cast<int*>(args[1]));
    if(key=="fp.mesh.indices.get") {
        const auto& indices=mesh->parts.at(*static_cast<int*>(args[0]));void* array=Array(indices.size());
        if(!indices.empty())std::memcpy(static_cast<uint8_t*>(array)+32,indices.data(),indices.size()*4);return array;
    }
    if(key=="fp.mesh.indices.set") {
        if(reject_indices){*exception=reinterpret_cast<void*>(1);return nullptr;}
        CHECK(*static_cast<int*>(args[1])==0&&!*static_cast<bool*>(args[3])&&*static_cast<int*>(args[4])==0);
        const auto count=arrays.at(args[0]).second;std::vector<uint32_t> indices(count);
        if(count)std::memcpy(indices.data(),static_cast<uint8_t*>(args[0])+32,count*4);
        mesh->parts.at(*static_cast<int*>(args[2]))=std::move(indices);return nullptr;
    }
    if(key=="fp.mesh.upload"){CHECK(!*static_cast<bool*>(args[0]));return nullptr;}
    if(key=="fp.object.clone") {
        clones.push_back(std::make_unique<Mesh>(*static_cast<Mesh*>(args[0])));return clones.back().get();
    }
    CHECK(false);return nullptr;
}
}
int main() {
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=[](void*,void* value){return value;};
    host.gchandle_new=[](void*,void*,int){++roots;return uint32_t(roots);};
    host.gchandle_free=[](void*,uint32_t){--roots;};g_host=&host;
    for(auto& method:g_contracts){method.resolved=true;method.method_info=method.key;}
    g_fp_index_type.type_object=reinterpret_cast<void*>(1);
    auto& engine=g_fp_engine;engine.attempted=true;engine.ready=false;engine.managed_ready=true;engine.header=32;
    engine.array_length=[](void* a){return arrays.at(a).second;};
    engine.array_bytes=[](void* a){return uint32_t(arrays.at(a).second*4);};
    CHECK(engine.Init()&&FpManagedContractsAvailable());
    Mesh original;FpSnapshot before;CHECK(FpReadMeshLayout(&original,before));
    CHECK(before.count==6&&before.strides==std::vector<int>({12,8,12})&&before.offsets==std::vector<int>({0,0,0,8}));
    const std::vector<FpMesh::Part> original_parts{{{0,1,2}},{{3,4,5}}};
    CHECK(FpManagedIndicesMatch(&original,original_parts));
    void* clone_args[]{&original};auto* copy=static_cast<Mesh*>(Invoke(Contract("fp.object.clone"),nullptr,clone_args));
    const std::vector<FpMesh::Part> filtered{{{}},{{3,4,5}}};
    CHECK(copy!=&original&&FpWriteManagedIndices(copy,1,filtered));
    CHECK(FpManagedIndicesMatch(&original,original_parts)&&FpManagedIndicesMatch(copy,filtered));
    FpSnapshot after;CHECK(FpReadMeshLayout(copy,after)&&FpSameVertices(before,after));
    CHECK(after.submeshes[0].index_count==0&&after.submeshes[1].index_start==0&&after.submeshes[1].index_count==3);
    CHECK(FpWriteManagedIndices(copy,1,std::vector<FpMesh::Part>(2)));
    after={};CHECK(!FpReadMeshLayout(copy,after));after={};CHECK(FpReadMeshLayout(copy,after,true));
    reject_indices=true;CHECK(!FpWriteManagedIndices(copy,1,original_parts));reject_indices=false;
    CHECK(FpManagedIndicesMatch(&original,original_parts));
    Mesh invalid=original;invalid.attributes.push_back(invalid.attributes[0]);after={};CHECK(!FpReadMeshLayout(&invalid,after));
    // Android IL2CPP strips Mesh.GetSubMesh: the managed path stays available
    // through GetIndexStart/Count/BaseVertex, then through GetIndices lengths.
    auto* submesh=Contract("fp.mesh.submesh");submesh->resolved=false;submesh->method_info=nullptr;
    CHECK(FpManagedContractsAvailable());
    for(int pass=0;pass<2;++pass) {
        if(pass==1)for(const char* key:{"fp.mesh.index_start","fp.mesh.index_count","fp.mesh.base_vertex"})
            {Contract(key)->resolved=false;Contract(key)->method_info=nullptr;}
        FpSnapshot layout;CHECK(FpReadMeshLayout(&original,layout)&&FpSameVertices(before,layout));
        CHECK(layout.submeshes.size()==2&&layout.submeshes[1].index_start==3&&layout.submeshes[1].index_count==3);
        CHECK(layout.submeshes[0].base_vertex==0&&layout.submeshes[1].topology==0);
        auto* filtered_copy=static_cast<Mesh*>(Invoke(Contract("fp.object.clone"),nullptr,clone_args));
        CHECK(FpWriteManagedIndices(filtered_copy,1,filtered)&&FpManagedIndicesMatch(filtered_copy,filtered));
        FpSnapshot cropped;CHECK(FpReadMeshLayout(filtered_copy,cropped));
        CHECK(cropped.submeshes[0].index_count==0&&cropped.submeshes[1].index_start==0&&cropped.submeshes[1].index_count==3);
    }
    CHECK(roots==0);g_host=nullptr;
    std::cout<<"PASS production managed Mesh fallback: "<<checks<<" checks\n";
}
