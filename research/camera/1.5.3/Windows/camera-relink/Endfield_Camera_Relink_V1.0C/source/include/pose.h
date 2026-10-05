#pragma once
// UDP 位姿协议与坐标换算
#include <cstdint>

namespace ecl {

// 协议 v1 定长 36 字节 (网络字节序一律小端, 与 x86 一致)
#pragma pack(push, 1)
struct PoseFrame {
    uint32_t seq;   // 递增序号
    float px, py, pz;      // 位置
    float qx, qy, qz, qw;  // 旋转(四元数, qw 最后)
    float fov;             // 视场角(度)
};
#pragma pack(pop)
static_assert(sizeof(PoseFrame) == 36, "PoseFrame must be 36 bytes");

// ---- 坐标换算 (仅当发送端给的是 Blender 坐标时使用) ----
// Blender: 右手系, Z 向上。 Unity: 左手系, Y 向上。
// 位置映射: U = (Bx, -Bz, By)   (轴交换矩阵 det=+1, 保定向)
inline void BlenderToUnityPos(float bx, float by, float bz,
                              float& ux, float& uy, float& uz) {
    ux = bx;
    uy = -bz;
    uz = by;
}

// 四元数乘法 q = a * b  (Hamilton, w,x,y,z)
inline void QuatMul(float aw, float ax, float ay, float az,
                    float bw, float bx, float by, float bz,
                    float& w, float& x, float& y, float& z) {
    w = aw * bw - ax * bx - ay * by - az * bz;
    x = aw * bx + ax * bw + ay * bz - az * by;
    y = aw * by - ax * bz + ay * bw + az * bx;
    z = aw * bz + ax * by - ay * bx + az * bw;
}

// 与位置映射对应的旋转: 先绕 U 系 X 轴转 +90° 再乘 Blender 四元数。
// 注意: 该映射需用"特征方向测试"实测校准, 见 README。
// 【仅遗留 UDP 位姿路径使用】现行路径是共享内存: 插件发 forward/up, 模块用
// LookRotation 重建四元数(见 src/shmem.cpp)。本条是"左乘常量"而非"共轭变换",
// 组合多个旋转时不是同态, 因此只作遗留兼容, 不建议再用 (ini: shmem=true)。
inline void BlenderToUnityQuat(float bw, float bx, float by, float bz,
                               float& uw, float& ux, float& uy, float& uz) {
    const float s = 0.7071067811865476f;  // cos45/sin45
    // qR = 绕 X 轴 +90° = (w=cos45, x=sin45, 0, 0)
    QuatMul(s, s, 0.f, 0.f, bw, bx, by, bz, uw, ux, uy, uz);
}

}  // namespace ecl
