#pragma once
namespace poser {
inline bool HotkeyAllowed(bool ownFocus,bool overlayFocus,bool panelVisible,
                          bool wantsText,int key,bool control) {
  if(!ownFocus)return false;
  const bool typing=overlayFocus&&panelVisible&&wantsText;
  return !typing || control || (key>=0x70 && key<=0x87); // F1..F24
}
struct HotkeyEdge {
  bool down=false;
  bool sample(bool pressed,bool allowed) {
    bool fire=pressed&&!down&&allowed;down=pressed;return fire;
  }
};
}
