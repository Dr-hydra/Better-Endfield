#pragma once
// 相机链路: UDP 接收 Blender 位姿 → 在游戏每帧回调里覆盖 Unity 相机。
#include "config.h"
#include "pose.h"
#include <cstdint>

namespace ecl {

struct UnityHandles {
    void* camera_main = nullptr;      // Camera.get_main
    void* comp_transform = nullptr;   // Component.get_transform
    void* tr_position_get = nullptr;  // Transform.get_position (基线/校准用)
    void* tr_position_set = nullptr;  // Transform.set_position
    void* tr_rotation_get = nullptr;  // Transform.get_rotation
    void* tr_rotation_set = nullptr;  // Transform.set_rotation
    void* cam_fov_set = nullptr;      // Camera.set_fieldOfView
    void* cam_fov_get = nullptr;      // Camera.get_fieldOfView
    // Unity 物理相机(真实镜头参数): 焦距/光圈/对焦距离/传感器/近远裁剪面
    void* cam_focal_get = nullptr;
    void* cam_focal_set = nullptr;
    void* cam_aperture_get = nullptr;
    void* cam_aperture_set = nullptr;
    void* cam_focusdist_get = nullptr;
    void* cam_focusdist_set = nullptr;
    void* cam_phys_get = nullptr;     // Camera.get_usePhysicalProperties
    void* cam_phys_set = nullptr;
    void* cam_sensor_get = nullptr;   // Camera.get_sensorSize
    void* cam_sensor_set = nullptr;
    void* cam_near_get = nullptr;
    void* cam_near_set = nullptr;
    void* cam_far_get = nullptr;
    void* cam_far_set = nullptr;
    bool lens_ok = false;             // 物理镜头句柄是否齐全
    bool ok = false;
};

// 启动 UDP 监听(后台线程)。重复调用安全。
void StartLink(const LinkConfig& cfg);
// 由游戏每帧回调调用(Unity 主线程): 有新帧则覆盖相机位姿, 否则不动。
void OnGameTick();
// 由 CinemachineBrain.LateUpdate 之后的钩子调用(帧末最后时机, 不会被 Brain 覆盖)
void OnCameraBrainTick();
// 位姿写入是否挂在 Brain 钩子上(供启动日志说明)
bool PoseWriteActive();
// 排队一条运行时指令(下一 Unity 帧执行; 托管调用必须在 Unity 线程)
void QueueCommand(int id, float arg);
// 排队一条带 4 个浮点参数的高级指令(ECM2 包, 用于镜头参数/模拟输入)
void QueueCommand4(int id, float a0, float a1, float a2, float a3);
// 注入解析出的方法句柄(启动线程调用一次)。
void SetHandles(const UnityHandles& h);
bool HandlesReady();

}  // namespace ecl
