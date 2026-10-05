#include "math/ik_two_bone.h"
#include <cmath>
#include <cstdio>
static int fails=0;
#define CHECK(c,m) do{ if(!(c)){fails++;std::printf("FAIL: %s\n",m);} }while(0)

int main(){
    // 上臂/前臂各 0.5，初始平伸 (1,0,0)；目标 (0.7,0.6,0) 距原点 0.92 < 1.0，可解
    Vec3 a{0,0,0}, b{0.5f,0,0}, c{1.0f,0,0};
    Vec3 target{0.7f, 0.6f, 0.0f};
    SolveTwoBone(a,b,c,target,{0,0,1}, true);
    float err = Len(c - target);
    CHECK(err < 1e-3f, "end effector reaches target");
    CHECK(b.z > 0.0f, "elbow bends toward pole side (+Z)");

    // 不可达目标（3.0 远超臂长 1.0）→ 应完全伸直朝向目标
    Vec3 far{3.0f,0,0};
    SolveTwoBone(a,b,c,far,{0,0,1}, true);
    CHECK(fabsf(Len(c) - 1.0f) < 1e-2f, "unreachable: fully extended (len==lab+lbc)");
    CHECK(c.x > 0.99f, "unreachable: pointing toward target");

    // pole 与 target 平行（退化）→ 不应崩溃，仍能命中目标
    Vec3 b2{0.5f,0,0}, c2{1.0f,0,0};
    SolveTwoBone(a,b2,c2,{1.0f,0,0},{0,0,1}, true);
    CHECK(Len(c2 - Vec3{1.0f,0,0}) < 1e-3f, "degenerate pole: still reaches target");

    // ---- 模拟 ik_driver 的"旋转差写回"链路（与游戏无关的纯数学验证）----
    // 两骨链：根 a 在中点, 末端 c；局部方向均为 +X，初始世界旋转为单位。
    // 驱动方式 = SolveTwoBone 求新 b'/c' → FromTo(旧方向,新方向) 作根/中骨旋转。
    {
        Vec3 pa{0,0,0}, pb{0.5f,0,0}, pc{1.0f,0,0};      // 当前世界坐标
        Vec3 ta{0.7f,0.6f,0.0f};
        Vec3 b0 = pb, c0 = pc, b = pb, c = pc;
        SolveTwoBone(pa, b, c, ta, b0, true);             // 求新 b'/c'
        // 根骨：上臂方向对齐
        Quat dRoot = Quat::FromTo(Norm(b0 - pa), Norm(b - pa));
        // 中骨：前臂方向对齐（c 已命中 target）
        Quat dMid = Quat::FromTo(Norm(c0 - b0), Norm(c - b));
        // 世界旋转：dRoot/dMid 叠加到初始单位旋转，再重构坐标
        Vec3 nb = pa + (dRoot * Vec3{0.5f,0,0});
        Vec3 nc = nb + (dMid * Vec3{0.5f,0,0});
        CHECK(Len(nc - ta) < 1e-3f, "driver deltas rotate chain to reach target");
        CHECK(Len(nb - b) < 1e-3f, "driver root rotation matches solver mid");
        // 旋转差应为单位四元数（可安全写回）
        CHECK(QuatLen(dRoot) > 0.99f && QuatLen(dMid) > 0.99f, "driver deltas normalized");
    }

    std::printf(fails?"%d FAILURES\n":"ik_two_bone OK\n", fails);
    return fails?1:0;
}
