#pragma once
// 读取 Phase-1 的 128 字节共享内存包 (EndfieldCameraBridgeV1), 由 Blender 插件写入。
#include <cstdint>

namespace ecl {

struct BridgePose {
    float pos[3];    // Unity 世界坐标
    float quat[4];   // Unity 四元数(x,y,z,w), 由 forward/up 构造
    float fovDeg;    // 由焦距与传感器高度换算
    float focalLength;
    float sensorW, sensorH;
    float clipStart, clipEnd;
    float focusDistance;
    float fStop;
    float fps;
    uint32_t flags;
    int frame;
    uint64_t sequence;
    // v1.1: Blender 参考物件(基线原点, 就是场景里那个"初始方块")的 Unity 坐标。
    // 有效时(hasRef)位置换算改为"以参考物件为原点"的绝对映射, 见 link.cpp。
    float refPos[3];
    bool hasRef;
};

// 打开共享内存; 返回 false 表示映射不存在(Blender 尚未开始发送)
bool ShmemStart();
void ShmemStop();

// 读取最新包: 返回 true 表示拿到了新序列且校验通过
bool ShmemLatest(BridgePose& out);

// 包内 flag 位
constexpr uint32_t kFlagEnabled = 1u << 0;
constexpr uint32_t kFlagTransform = 1u << 1;
constexpr uint32_t kFlagLens = 1u << 2;
constexpr uint32_t kFlagSuppressController = 1u << 3;
// 包内 5 个备用浮点(偏移 100/104/108)携带了参考物件位置
constexpr uint32_t kFlagReference = 1u << 4;
// v1.6: 插件面板选的"镜头模式" = 随角色(偏移驱动)。位 5 起是空闲的, 因此不需要改包长/版本号:
//   置位   = 随角色镜头: 模块切 pose_mode=4(每帧只发相机偏移, 位置交给游戏跟随), 并关掉自由相机
//   不置位 = 定镜头(定机位): 模块切 pose_mode=3(每帧直接写 transform), 并保持自由相机
// 老插件不置该位 → 行为与之前完全一致(向后兼容)。
constexpr uint32_t kFlagFollow = 1u << 5;
// v1.7(模块 1.0.0ak / 插件 0.3.8): "隐藏游戏 UI(拍摄用)"。位 6。
//   置位   = 请求隐藏 HUD: 模块用游戏自己的 AddUICamCullingMaskConfig("ECLHideUI", 0)
//            + _UpdateUICamCullingMask() 把**只渲染层 5 的 UICamera** 的遮罩压成 0
//            (实测: 世界由 MainCamera 渲染且它不含位 5 → 动它碰不到世界, 也不涉及任何输入状态);
//   不置位 = 请求显示: RemoveUICamCullingMaskConfig("ECLHideUI"), 让游戏自己重算遮罩。
// 1.0.0aj 指令 65 实测: UI 整体消失, 移动/普通攻击/技能全部照常, 且可逆(遮罩自己回到 0x20)。
// 注意: 游戏在 UI 状态变化时会**重建整个配置栈**(例如按 ESC 呼出界面) —— 模块对此"放行"
// (策略见 ui_hide 相关日志与指令 66), 等界面关掉、遮罩回到常规值后自动重新隐藏。
// 老插件不置该位 → 行为与之前完全一致(向后兼容)。
constexpr uint32_t kFlagHideUi = 1u << 6;

}  // namespace ecl
