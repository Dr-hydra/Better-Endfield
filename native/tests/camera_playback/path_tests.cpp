#include "../../modules/camera/camera_file_worker.h"
#include "test_support.h"
using namespace BetterEndfieldNext;
static CameraPath::Path MakePath() {
    return {3,false,{{{0,0,0},{0,0,0,1},60},{{2,3,4},{0,1,0,0},90}}};
}
static CameraFiles::Result Wait(CameraFiles::Worker& w) {
    auto end=std::chrono::steady_clock::now()+std::chrono::seconds(5);
    while(std::chrono::steady_clock::now()<end) {
        if(auto r=w.Poll())return std::move(*r);
        std::this_thread::sleep_for(std::chrono::milliseconds(1));
    }
    CHECK(false);return {};
}
int main() {
    auto path=MakePath();std::string text,error;
    CHECK(CameraPath::Encode(path,text,error));CameraPath::Path copy;
    CHECK(CameraPath::Decode(text,copy,error));CHECK(copy.keys.size()==2 && copy.segment_seconds==3 && !copy.loop);
    CHECK(CameraPath::Decode("\xEF\xBB\xBF"+text,copy,error));
    CameraPath::Key key;bool ended=false;
    CHECK(!CameraPath::Sample({},0,key,ended));
    CHECK(CameraPath::Sample(path,1.5,key,ended));CHECK(nearly(key.position.x,1) && nearly(key.fov,75) && !ended);
    CHECK(CameraPath::Sample(path,100,key,ended));CHECK(key.position.z==4 && ended);
    CHECK(CameraPath::Sample(path,-1,key,ended));CHECK(key.position.x==0);
    CHECK(CameraPath::Sample(path,std::numeric_limits<double>::quiet_NaN(),key,ended));CHECK(key.position.x==0);
    path.loop=true;CHECK(CameraPath::Sample(path,3,key,ended));CHECK(key.position.x==0 && !ended);
    path.keys.resize(1);CHECK(CameraPath::Sample(path,100,key,ended));CHECK(ended);
    for(const auto& invalid: {std::string(""),text+"junk",text.substr(0,text.size()/2),std::string(65537,'x')}) {
        auto previous=copy.keys.size();CHECK(!CameraPath::Decode(invalid,copy,error));CHECK(copy.keys.size()==previous);
    }
    for(const std::string bad: {"BE_CAMERA_PATH 9\n", "BE_CAMERA_PATH 1\nsegment_seconds 0\nloop 0\nkeys 1\n0 0 0 0 0 0 1 60\n",
        "BE_CAMERA_PATH 1\nsegment_seconds 3\nloop 2\nkeys 1\n0 0 0 0 0 0 1 60\n",
        "BE_CAMERA_PATH 1\nsegment_seconds 3\nloop 0\nkeys 1\n0 0 0 0 0 0 0 60\n"})CHECK(!CameraPath::Decode(bad,copy,error));
    path=MakePath();path.keys[0].fov=180;CHECK(!CameraPath::Encode(path,text,error));path=MakePath();
    path.keys[0].position.x=std::numeric_limits<float>::infinity();CHECK(!CameraPath::Encode(path,text,error));
    path=MakePath();path.keys.resize(65);CHECK(!CameraPath::Encode(path,text,error));path=MakePath();
    const std::string suffix=std::to_string(std::chrono::steady_clock::now().time_since_epoch().count());
    const auto dir=std::filesystem::temp_directory_path()/std::filesystem::path(
        std::u8string(u8"be-camera-路径-")+std::u8string(suffix.begin(),suffix.end()));
    CHECK(std::filesystem::create_directory(dir));
    const auto file=dir/"roundtrip.becamera";
    const auto utf8=file.u8string(); const std::string name(utf8.begin(),utf8.end());
    CameraFiles::Job save{CameraFiles::Kind::SavePath,name,7,path};
    auto result=CameraFiles::Process(save);CHECK(result.error.empty());
    auto read=CameraFiles::Process({CameraFiles::Kind::LoadPath,name,8,{}});
    CHECK(read.error.empty() && read.path.keys.size()==2 && read.generation==8);
    save.snapshot.keys[0].fov=70;CHECK(CameraFiles::Process(save).error.empty());
    save.snapshot.keys[0].fov=180;CHECK(!CameraFiles::Process(save).error.empty());
    read=CameraFiles::Process({CameraFiles::Kind::LoadPath,name,9,{}});CHECK(read.path.keys[0].fov==70);
    CHECK(!CameraFiles::Process({CameraFiles::Kind::LoadVmd,name,0,{}}).error.empty());
    CHECK(!CameraFiles::Process({CameraFiles::Kind::LoadPath,"",0,{}}).error.empty());
    CameraFiles::Worker worker;CHECK(!worker.Submit(save));worker.Start();worker.Start();
    CHECK(worker.Submit({CameraFiles::Kind::LoadPath,name,22,{}}));CHECK(!worker.Submit(save));
    read=Wait(worker);CHECK(read.error.empty() && read.generation==22);CHECK(!worker.Busy());
    worker.Stop();CHECK(!worker.Submit(save));worker.Start();
    CHECK(worker.Submit({CameraFiles::Kind::LoadPath,name,23,{}}));read=Wait(worker);CHECK(read.generation==23);
    worker.Stop();worker.Stop();
    // A minimal complete morph-only VMD exercises the actual body file path,
    // which must not require a camera section or strip bone/morph data.
    std::vector<uint8_t> vmd(50,0);
    const char magic[]="Vocaloid Motion Data 0002";
    std::copy(magic,magic+sizeof(magic)-1,vmd.begin());
    auto u32=[&](uint32_t n){for(int i=0;i<4;++i)vmd.push_back(uint8_t(n>>(8*i)));};
    u32(0);u32(1);for(int i=0;i<15;++i)vmd.push_back(i==0?'A':0);
    u32(30);u32(std::bit_cast<uint32_t>(.5f));
    const auto motionFile=dir/"motion.vmd";
    {std::ofstream out(motionFile,std::ios::binary);out.write(reinterpret_cast<const char*>(vmd.data()),std::streamsize(vmd.size()));}
    auto motionU8=motionFile.u8string();const std::string motionName(motionU8.begin(),motionU8.end());
    auto motionResult=CameraFiles::Process({CameraFiles::Kind::LoadMotion,motionName,42,{}});
    CHECK(motionResult.error.empty());CHECK(motionResult.motion.morphs.size()==1);CHECK(motionResult.motion.cameras.empty());
    CHECK(!CameraFiles::Process({CameraFiles::Kind::LoadVmd,motionName,42,{}}).error.empty());
    std::error_code ec;std::filesystem::remove_all(dir,ec);CHECK(!ec);
    std::cout<<"PASS camera path/atomic files/owned worker: "<<checks<<" checks\n";
}
