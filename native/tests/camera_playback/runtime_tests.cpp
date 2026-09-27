// Include the production TU to exercise actual request handling and end states.
// No game is loaded; host callbacks and Win32 on non-Windows are test fixtures.
#include "../../modules/camera/module.cpp"
#include "test_support.h"
#include <filesystem>
#include <fstream>
using namespace BetterEndfield;
using namespace BetterEndfield::CameraModule;
static void Drain() {
    auto until=std::chrono::steady_clock::now()+std::chrono::seconds(5);
    while(g_camera_files.Busy() && std::chrono::steady_clock::now()<until) {
        PollCameraFileResults();
        std::this_thread::sleep_for(std::chrono::milliseconds(1));
    }
    CHECK(!g_camera_files.Busy());
}
int main() {
    g_free_camera_enabled=true;g_free_camera_contract_ready=true;g_free_camera_active=true;
    g_keyframes={{{0,0,0},{0,0,0,1},60},{{2,3,4},{0,1,0,0},90}};
    StartKeyframes();CHECK(g_playback==FreePlayback::Keyframes);
    g_keyframe_clear_request=true;PumpFreeCameraRequests();
    CHECK(g_playback==FreePlayback::None && g_keyframes.empty());
    StepKeyframes(g_playback_start+1);CHECK(std::isfinite(g_free_view.fov)); // immutable snapshot remains valid
    g_keyframes={{{0,0,0},{0,0,0,1},60},{{2,3,4},{0,1,0,0},90}};
    StartKeyframes();g_keyframe_segment_seconds=60;g_keyframe_loop=true; // no mid-play warp
    StepKeyframes(g_playback_start+4);CHECK(g_playback==FreePlayback::None && g_free_view.fov==90);
    g_vmd={}; VmdCameraKey a,b;a.frame=0;b.frame=30;b.fov=80;b.distance=-10;
    g_vmd.keys={a,b};g_vmd.duration=1;g_playback=FreePlayback::Vmd;g_playback_start=0;
    StepVmd(3);CHECK(g_playback==FreePlayback::None);CHECK(near(g_free_view.fov,85));
    g_vmd.keys={b};g_vmd.keys[0].frame=0;g_vmd.duration=0;g_playback=FreePlayback::Vmd;g_playback_start=0;
    StepVmd(0);CHECK(g_playback==FreePlayback::None && near(g_free_view.fov,85));
    // Configuration keys are consumed by the actual native parser.
    auto config=ParseConfiguration("enabled=true\nkeyframe_file=\"C:/camera path/test.becamera\"\nkeyframe_save_hotkey=F10\nkeyframe_load_hotkey=F11\n");
    CHECK(config.keyframe_file=="C:/camera path/test.becamera");
    CHECK(config.keyframe_save_key==VK_F1+9 && config.keyframe_load_key==VK_F1+10);
    g_camera_files.Start();
    const auto dir=std::filesystem::temp_directory_path()/
        ("be-camera-runtime-"+std::to_string(std::chrono::steady_clock::now().time_since_epoch().count()));
    CHECK(std::filesystem::create_directory(dir));
    const auto file=dir/"path.becamera"; const auto u8=file.u8string();
    g_keyframe_file=std::string(u8.begin(),u8.end());
    CameraPath::Path source{2,false,{{{8,0,0},{0,0,0,1},50},{{10,0,0},{0,0,0,1},70}}};
    CHECK(CameraFiles::Process({CameraFiles::Kind::SavePath,g_keyframe_file,0,source}).error.empty());
    QueueCameraFile(CameraFiles::Kind::LoadPath);Drain();CHECK(g_keyframes[0].position.x==8);
    CHECK(g_keyframe_segment_seconds==2 && !g_keyframe_loop);
    g_keyframes[0].position.x=42;
    QueueCameraFile(CameraFiles::Kind::LoadPath);++g_file_generation;Drain();
    CHECK(g_keyframes[0].position.x==42); // cancelled load cannot overwrite editor state
    std::ofstream(file)<<"broken";
    StartKeyframes();QueueCameraFile(CameraFiles::Kind::LoadPath);Drain();
    CHECK(g_keyframes[0].position.x==42 && g_playback==FreePlayback::Keyframes);
    g_vmd_camera_file=g_keyframe_file;StartVmd();Drain();
    CHECK(g_playback==FreePlayback::Keyframes && !g_vmd_load_pending); // invalid VMD doesn't stop playback
    QueueCameraFile(CameraFiles::Kind::SavePath);Drain();
    auto read=CameraFiles::Process({CameraFiles::Kind::LoadPath,g_keyframe_file,0,{}});
    CHECK(read.error.empty() && read.path.keys[0].position.x==42);
    // Disabled camera cannot publish a file result queued while enabled.
    g_keyframes[0].position.x=44;QueueCameraFile(CameraFiles::Kind::LoadPath);g_free_camera_enabled=false;Drain();
    CHECK(g_keyframes[0].position.x==44);
    g_free_camera_enabled=true;
    g_keyframes[0].position.x=45;QueueCameraFile(CameraFiles::Kind::LoadPath);
    g_asset_config_generation.fetch_add(1);Drain();CHECK(g_keyframes[0].position.x==45);
    // Overflowing VMD world coordinates must not be published to a transform.
    g_vmd.keys[0].interest.x=std::numeric_limits<float>::max();
    g_vmd_camera_scale=10;g_playback=FreePlayback::Vmd;
    const Vector3 before=g_free_view.position;StepVmd(5);
    CHECK(g_playback==FreePlayback::None && g_free_view.position.x==before.x);
    g_free_camera_active=false;g_free_camera_enabled=false;
    g_keyframe_save_request=true;g_keyframe_load_request=true;
    PumpFreeCameraControl();CHECK(!g_keyframe_save_request && !g_keyframe_load_request);
    g_camera_files.Stop();g_playback=FreePlayback::None;
    std::filesystem::remove_all(dir);
    std::cout<<"PASS production camera callbacks: "<<checks<<" checks\n";
}
