#pragma once
// 运行时符号清单: 枚举所有程序集/类/方法/字段, 输出相关符号(用于定位相机 Rig / UI 层)
#include <string>

namespace ecl {

// 清单"档位"(v1.0.0ai): 不同目标用不同的关键词表与过滤规则
enum InventorySet {
    kInvCamera = 0,  // 相机(Rig/景深/曝光/镜头/跟随…) —— 原行为
    kInvUi = 1,      // UI(HUD/面板/Canvas/显隐/图层…) —— 隐藏 UI 探测用
};

// logPath: 输出文件(模块目录 inventory.log / EndfieldCamLink_ui_inventory.log)
void RunInventorySet(const std::string& logPath, int set);

// 旧入口(相机档)
inline void RunInventory(const std::string& logPath) {
    RunInventorySet(logPath, kInvCamera);
}
}  // namespace ecl
