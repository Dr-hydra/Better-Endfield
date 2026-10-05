// Native ACL 2.1 encoding for offline roundtrip experiments.
#include <acl/compression/compress.h>
#include <acl/compression/transform_error_metrics.h>
#include <acl/core/ansi_allocator.h>
#include <acl/compression/track_array.h>
#include <acl/core/compressed_tracks.h>
#include <fstream>
#include <iostream>
#include <vector>
#include <cmath>
#include <string>

int main(int argc,char** argv) {
    if(argc!=6) { std::cerr<<"input.f32 output.acl qvv|float trackCount frameCount\n";return 2; }
    const bool qvv=std::string(argv[3])=="qvv";
    const uint32_t n=std::stoul(argv[4]), frames=std::stoul(argv[5]),width=qvv?10:1;
    if(!n||n>1000||!frames||frames>10000)return 3;
    std::ifstream in(argv[1],std::ios::binary|std::ios::ate);
    size_t size=size_t(n)*frames*width*sizeof(float);
    if(!in||in.tellg()!=std::streamoff(size))return 4;
    std::vector<float> data(size/4);in.seekg(0);in.read(reinterpret_cast<char*>(data.data()),size);
    for(float v:data)if(!std::isfinite(v))return 5;
    acl::ansi_allocator allocator;
    acl::compression_settings settings=acl::get_default_compression_settings();
    // Keep precision high for this compatibility experiment; size is secondary.
    settings.level=acl::compression_level8::lowest;
    // Match the shipped qvv header formats (rotation=3, translation=1, scale=1).
    settings.rotation_format=acl::rotation_format8::quatf_drop_w_variable;
    settings.translation_format=acl::vector_format8::vector3f_variable;
    settings.scale_format=acl::vector_format8::vector3f_variable;
    acl::qvvf_transform_error_metric metric;settings.error_metric=&metric;
    acl::compressed_tracks* result=nullptr;acl::output_stats stats;acl::error_result error;
    if(qvv) {
        acl::track_array_qvvf tracks(allocator,n);
        for(uint32_t i=0;i<n;i++) {
            acl::track_desc_transformf desc;desc.output_index=i;desc.precision=0.000001f;
            tracks[i]=acl::track_qvvf::make_reserve(desc,allocator,frames,60.0f);
            for(uint32_t frame=0;frame<frames;frame++) {
                auto p=data.data()+(size_t(frame)*n+i)*10;
                tracks[i][frame].rotation=rtm::quat_normalize(rtm::quat_load(p));
                tracks[i][frame].translation=rtm::vector_load3(p+4);
                tracks[i][frame].scale=rtm::vector_load3(p+7);
            }
        }
        error=acl::compress_track_list(allocator,tracks,settings,result,stats);
    } else {
        acl::track_array_float1f tracks(allocator,n);
        for(uint32_t i=0;i<n;i++) {
            acl::track_desc_scalarf desc;desc.output_index=i;desc.precision=0.000001f;
            tracks[i]=acl::track_float1f::make_reserve(desc,allocator,frames,60.0f);
            for(uint32_t frame=0;frame<frames;frame++)tracks[i][frame]=data[size_t(frame)*n+i];
        }
        error=acl::compress_track_list(allocator,tracks,settings,result,stats);
    }
    if(!error.empty()||!result) {std::cerr<<error.c_str();return 6;}
    if(!result->is_valid(false).empty()||int(result->get_version())!=10)return 7;
    std::ofstream out(argv[2],std::ios::binary);out.write(reinterpret_cast<const char*>(result),result->get_size());
    std::cout<<"ACL v"<<int(result->get_version())<<" tracks="<<n<<" frames="<<frames<<" bytes="<<result->get_size()<<"\n";
    allocator.deallocate(result,result->get_size());return out?0:8;
}
