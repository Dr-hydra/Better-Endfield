#include "../../shared/motion/vmd.h"
#include "test_support.h"
#include <random>
using namespace BetterEndfield;
struct Writer {
    std::vector<uint8_t> bytes;
    void u8(uint8_t x) { bytes.push_back(x); }
    void u32(uint32_t x) { for (int i=0;i<4;++i) u8(uint8_t(x>>(i*8))); }
    void f(float x) { u32(std::bit_cast<uint32_t>(x)); }
    void name(std::string_view value,size_t count) { for(size_t i=0;i<count;++i) u8(i<value.size()?uint8_t(value[i]):0); }
    void header(bool old=false) { name(old?"Vocaloid Motion Data file":"Vocaloid Motion Data 0002",30); name("model",old?10:20); }
    void bone(std::string_view name_,uint32_t frame,float x=0) {
        name(name_,15); u32(frame); f(x); f(0); f(0); f(0); f(0); f(0); f(1);
        for(int i=0;i<64;++i) u8(i<8?20:(i<16?107:0));
    }
    void morph(std::string_view name_,uint32_t frame,float weight) { name(name_,15); u32(frame); f(weight); }
    void camera(uint32_t frame,float x=0,uint8_t ortho=0) {
        u32(frame); f(-10); f(x); f(1); f(2); f(0); f(0); f(0);
        for(int i=0;i<6;++i) { u8(20);u8(107);u8(20);u8(107); } // x1,x2,y1,y2
        u32(60);u8(ortho);
    }
};
int main() {
    Vmd::Motion motion; std::string error;
    Writer all; all.header();
    const std::string center="\x83\x5A\x83\x93\x83\x5E\x81\x5B";
    all.u32(4);all.bone(center,10,1);all.bone(center,0);all.bone(center,10,2);all.bone("other",20);
    const size_t bone_end=all.bytes.size();
    all.u32(2);all.morph("mouth",0,0);all.morph("mouth",10,1);
    const size_t morph_end=all.bytes.size();
    all.u32(3);all.camera(10,10);all.camera(0,0);all.camera(10,20);
    const size_t camera_end=all.bytes.size();
    all.u32(1);all.u32(12);for(int i=0;i<6;++i)all.f(0);
    const size_t light_end=all.bytes.size();
    all.u32(1);all.u32(12);all.u8(1);all.f(0);
    const size_t shadow_end=all.bytes.size();
    all.u32(2);
    all.u32(3);all.u8(1);all.u32(2);all.name("left",20);all.u8(0);all.name("right",20);all.u8(1);
    all.u32(3);all.u8(0);all.u32(1);all.name("left",20);all.u8(1);
    CHECK(Vmd::Parse(all.bytes,motion,error));CHECK(error.empty());
    CHECK(motion.version==2 && motion.bones.size()==2);
    CHECK(motion.bones.contains(center) && motion.bones.at(center).size()==2);
    CHECK(motion.bones.at(center).back().position.x==2);
    CHECK(motion.cameras.size()==2 && motion.cameras.back().interest.x==20);
    CHECK(motion.duplicate_keys==4);CHECK(motion.last_frame==20);
    CHECK(motion.ik.at("right").size()==1 && motion.ik.at("left").back().enabled);
    CHECK(Vmd::SampleSwitch(motion.visibility,2));CHECK(!Vmd::SampleSwitch(motion.visibility,3));
    CHECK(near(Vmd::SampleMorph(motion.morphs.at("mouth"),5),0.5));
    Vmd::BoneSample bone;CHECK(Vmd::SampleBone(motion.bones.at(center),5,bone));CHECK(near(bone.position.x,1));
    Vmd::CameraSample camera;CHECK(Vmd::SampleCamera(motion.cameras,5,camera));CHECK(near(camera.interest.x,10));
    // Only complete section boundaries may omit trailing sections.
    for(size_t i=0;i<all.bytes.size();++i) {
        bool valid=i==bone_end||i==morph_end||i==camera_end||i==light_end||i==shadow_end;
        Vmd::Motion trial;
        CHECK(Vmd::Parse(std::span(all.bytes).first(i),trial,error)==valid);
    }
    Writer old;old.header(true);old.u32(0);CHECK(Vmd::Parse(old.bytes,motion,error));CHECK(motion.version==1);
    auto bad=all.bytes;bad.push_back(0);CHECK(!Vmd::Parse(bad,motion,error));CHECK(motion.version==1);
    bad=all.bytes;bad[50]=bad[51]=bad[52]=bad[53]=255;CHECK(!Vmd::Parse(bad,motion,error));
    bad=all.bytes; // first bone position is at header + count + name + frame
    const uint32_t nan=0x7fc00000; for(int i=0;i<4;++i)bad[73+i]=uint8_t(nan>>(8*i));
    CHECK(!Vmd::Parse(bad,motion,error));CHECK(error.find("non-finite")!=std::string::npos);
    Vmd::Limits small;small.file_bytes=10;CHECK(!Vmd::Parse(all.bytes,motion,error,small));
    small={};small.tracks=1;CHECK(!Vmd::Parse(all.bytes,motion,error,small));
    small={};small.ik_per_frame=1;CHECK(!Vmd::Parse(all.bytes,motion,error,small));
    small={};small.ik_entries=2;CHECK(!Vmd::Parse(all.bytes,motion,error,small));
    small={};small.last_frame=10;CHECK(!Vmd::Parse(all.bytes,motion,error,small));
    Writer names;names.header();names.u32(2);names.bone("\xFF",0);names.bone("\xFE",0);
    CHECK(Vmd::Parse(names.bytes,motion,error));CHECK(motion.bones.size()==2); // no lossy-name collision
    Writer zero;zero.header();zero.u32(1);zero.bone("zero",0);
    for(size_t i=85;i<101;++i)zero.bytes[i]=0;
    CHECK(Vmd::Parse(zero.bytes,motion,error));CHECK(motion.zero_quaternions==1);
    CHECK(motion.bones.at("zero")[0].rotation.w==1);
    Writer invalid;invalid.header();invalid.u32(0);invalid.u32(0);invalid.u32(1);invalid.camera(0);
    invalid.bytes.back()=2;CHECK(!Vmd::Parse(invalid.bytes,motion,error));
    invalid.bytes.back()=1;CHECK(Vmd::Parse(invalid.bytes,motion,error));CHECK(motion.cameras[0].orthographic);
    invalid.bytes[62+32]=128;CHECK(!Vmd::Parse(invalid.bytes,motion,error));
    // Asymmetric curve verifies camera byte order; endpoints are exact cuts.
    Vmd::CameraKey a,b;a.frame=0;b.frame=10;b.interest={10,20,30};b.fov=90;
    for(size_t i=0;i<6;++i){ b.interpolation[i*4]=12;b.interpolation[i*4+1]=100;b.interpolation[i*4+2]=110;b.interpolation[i*4+3]=120; }
    CHECK(Vmd::SampleCamera({a,b},5,camera));
    CHECK(near(camera.interest.x,10*Vmd::Bezier(12,110,100,120,0.5)));
    CHECK(!near(camera.interest.x,10*Vmd::Bezier(12,100,110,120,0.5),0.1));
    b.frame=1;CHECK(Vmd::SampleCamera({a,b},0.9,camera));CHECK(camera.interest.x==0);
    CHECK(Vmd::SampleCamera({a,b},1,camera));CHECK(camera.interest.x==10);
    CHECK(Vmd::SampleCamera({a,b},std::numeric_limits<double>::infinity(),camera));CHECK(camera.interest.x==10);
    CHECK(Vmd::SampleCamera({a,b},std::numeric_limits<double>::quiet_NaN(),camera));CHECK(camera.interest.x==0);
    CHECK(!Vmd::SampleCamera({},0,camera));CHECK(!Vmd::SampleBone({},0,bone));
    auto q=Vmd::Slerp({0,0,0,1},{0,0,0,-1},0.5);CHECK(near(std::abs(q.w),1));
    for(int i=0;i<=100;++i) {
        const double t=i/100.0;
        CHECK(near(Vmd::Bezier(20,20,107,107,t),t));
        double lo=0,hi=1;
        for(int j=0;j<60;++j){const double m=(lo+hi)/2;if(m*m*m<t)lo=m;else hi=m;}
        const double s=(lo+hi)/2;CHECK(near(Vmd::Bezier(0,127,0,127,t),1-std::pow(1-s,3),2e-5));
    }
    std::mt19937 rng(42);
    for(int i=0;i<3000;++i) {
        auto bytes=all.bytes;
        bytes.resize(rng()%(bytes.size()+1));
        for(int j=0;j<4 && !bytes.empty();++j) bytes[rng()%bytes.size()]=uint8_t(rng());
        Vmd::Motion trial;Vmd::Parse(bytes,trial,error);
    }
    std::cout<<"PASS VMD parser/samplers: "<<checks<<" checks + 3000 deterministic mutations\n";
}
