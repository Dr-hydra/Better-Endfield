#pragma once
#include "window_capture.h"
#include "game_input_client.h"
#include <Windows.h>
#include <dwmapi.h>
#include <windowsx.h>
#include <array>
#include <string>
#include <string_view>

namespace BetterEndfieldNext::MediaScreen {
// Ordinary apps use directed messages. Selected game windows use a scoped
// source-process bridge; neither path activates a source or uses global input.
// Each press remembers its HWND so release cannot be redirected after a click.
class WindowInput {
    HWND window_ = nullptr, keyboard_ = nullptr;
    DWORD pid_ = 0;
    std::array<HWND,256> keys_{};
    std::array<int64_t,256> key_data_{};
    std::array<HWND,3> buttons_{};
    POINT last_{};
    std::string error_;
    bool qt_text_window_=false;
    GameInputClient game_input_;
    static bool QtWindow(HWND window) {
        wchar_t name[256]{};
        if (!GetClassNameW(window,name,256)) return false;
        std::wstring_view klass(name);
        return klass.starts_with(L"Qt") && klass.find(L"QWindow")!=klass.npos;
    }
    static bool TextKey(uint32_t key) {
        return (key>='0'&&key<='9') || (key>='A'&&key<='Z') || key==VK_SPACE ||
            (key>=VK_NUMPAD0&&key<=VK_DIVIDE) || (key>=VK_OEM_1&&key<=VK_OEM_3) ||
            (key>=VK_OEM_4&&key<=VK_OEM_8) || key==VK_OEM_102;
    }
    bool ShortcutModifier() const {
        return keys_[VK_CONTROL]||keys_[VK_LCONTROL]||keys_[VK_RCONTROL]||
            keys_[VK_MENU]||keys_[VK_LMENU]||keys_[VK_RMENU]||keys_[VK_LWIN]||keys_[VK_RWIN];
    }
    bool Valid() const {
        DWORD pid=0; return window_ && IsWindow(window_) && GetWindowThreadProcessId(window_,&pid) && pid==pid_;
    }
    bool Owned(HWND hwnd) const { return hwnd && IsWindow(hwnd) && (hwnd==window_ || IsChild(window_,hwnd)); }
    bool Deliver(HWND hwnd, UINT message, WPARAM key, LPARAM data) {
        if (!Valid() || !Owned(hwnd)) { error_="Selected input window closed or changed"; return false; }
        if (qt_text_window_ && message>=WM_KEYFIRST && message<=WM_KEYLAST) {
            // Qt's queue owns key translation. Plain text has only CHAR events;
            // navigation and shortcuts retain their ordered DOWN/UP messages.
            // IME_CHAR uses Qt's standalone text path; a navigation KEYDOWN must
            // not consume a later posted CHAR through Qt's queue look-ahead.
            if (message==WM_CHAR) message=WM_IME_CHAR;
            if (!PostMessageW(hwnd,message,key,data)) {
                error_="Qt window input rejected (Win32 "+std::to_string(GetLastError())+")"; return false;
            }
            return true;
        }
        // Bypass the recipient's TranslateMessage. The game's real CHAR events
        // already contain translated Unicode; queueing KEYDOWN as well would
        // generate duplicate text. Ordered delivery also preserves navigation.
        DWORD_PTR result=0;
        if (!SendMessageTimeoutW(hwnd,message,key,data,SMTO_ABORTIFHUNG|SMTO_BLOCK,100,&result)) {
            error_="Window input rejected or timed out (Win32 "+std::to_string(GetLastError())+")"; return false;
        }
        return true;
    }
    WPARAM Flags() const {
        return (buttons_[0]?MK_LBUTTON:0)|(buttons_[1]?MK_RBUTTON:0)|(buttons_[2]?MK_MBUTTON:0)|
            (keys_[VK_CONTROL]||keys_[VK_LCONTROL]||keys_[VK_RCONTROL]?MK_CONTROL:0)|
            (keys_[VK_SHIFT]||keys_[VK_LSHIFT]||keys_[VK_RSHIFT]?MK_SHIFT:0);
    }
    HWND ChildAt(POINT screen) const {
        HWND target=window_;
        for (int depth=0; depth<16; ++depth) {
            POINT point=screen; ScreenToClient(target,&point);
            HWND child=ChildWindowFromPointEx(target,point,CWP_SKIPINVISIBLE|CWP_SKIPDISABLED|CWP_SKIPTRANSPARENT);
            if (!child || child==target || !Owned(child)) break;
            target=child;
        }
        return target;
    }
public:
    ~WindowInput() { Release(); }
    void Release() {
        game_input_.Release();
        for (size_t key=0; key<keys_.size(); ++key) if (keys_[key]) {
            Deliver(keys_[key],WM_KEYUP,WPARAM(key),LPARAM(key_data_[key])|LPARAM(0xC0000000)); keys_[key]=nullptr;
        }
        constexpr UINT up[]={WM_LBUTTONUP,WM_RBUTTONUP,WM_MBUTTONUP};
        for (size_t i=0; i<buttons_.size(); ++i) if (auto hwnd=buttons_[i]) {
            buttons_[i]=nullptr; POINT p=last_; ScreenToClient(hwnd,&p);
            Deliver(hwnd,up[i],Flags(),MAKELPARAM(p.x,p.y));
        }
        window_=keyboard_=nullptr; pid_=0; qt_text_window_=false;
    }
    bool Bind(uint64_t window, uint32_t pid) {
        Release(); error_.clear(); window_=reinterpret_cast<HWND>(uintptr_t(window)); pid_=pid;
        keyboard_=window_;
        if (!Valid()) { error_="Selected input window is unavailable"; return false; }
        qt_text_window_=QtWindow(window_);
        if(GameInputClient::GameWindow(window_)&&!game_input_.Bind(window_,pid_)){
            error_=game_input_.Error();game_input_.Release();return false;
        }
        return true;
    }
    std::string Error() const { return error_; }
    HWND KeyboardRecipient() const { return keyboard_; }
    bool Heartbeat() { if(game_input_.Heartbeat())return true;error_=game_input_.Error();return false; }
    bool Send(const WindowInputEvent& event, const CaptureGeometry& geometry) {
        if (!Valid()) { error_="Selected input window closed or changed"; return false; }
        if (IsIconic(window_)) { error_="Selected input window is minimized"; return false; }
        if(game_input_.Bound()){bool ok=game_input_.Send(event,geometry);if(!ok)error_=game_input_.Error();return ok;}
        switch (event.message) {
        case WM_KEYDOWN: case WM_SYSKEYDOWN: case WM_KEYUP: case WM_SYSKEYUP: {
            if (event.key>=256 || !event.key) { error_="Invalid forwarded key"; return false; }
            bool up=event.message==WM_KEYUP || event.message==WM_SYSKEYUP;
            // Qt creates text from a sent KEYDOWN even without TranslateMessage.
            // Use the original CHAR for plain text, preserving case/Unicode;
            // keep physical keys for navigation and Ctrl/Alt/Win shortcuts.
            if (qt_text_window_ && !up && TextKey(event.key) && !ShortcutModifier()) return true;
            HWND target=keys_[event.key]?keys_[event.key]:keyboard_;
            if (!Owned(target)) target=window_;
            if (up && !keys_[event.key]) return true;
            bool ok=Deliver(target,event.message,event.key,LPARAM(event.data));
            if (ok) { keys_[event.key]=up?nullptr:target; key_data_[event.key]=event.data; } return ok;
        }
        case WM_CHAR: case WM_SYSCHAR:
            if (qt_text_window_ && (event.key<32 || event.key==127 || ShortcutModifier())) return true;
            return Deliver(Owned(keyboard_)?keyboard_:window_,event.message,event.key,LPARAM(event.data));
        case WM_MOUSEMOVE: case WM_LBUTTONDOWN: case WM_LBUTTONUP: case WM_RBUTTONDOWN: case WM_RBUTTONUP:
        case WM_MBUTTONDOWN: case WM_MBUTTONUP: case WM_MOUSEWHEEL: case WM_MOUSEHWHEEL: break;
        default: error_="Unsupported forwarded input message"; return false;
        }
        if (!geometry.source_width || !geometry.source_height || event.x<0 || event.y<0 ||
            event.x>=int(geometry.source_width) || event.y>=int(geometry.source_height)) return true;
        RECT bounds{};
        if (FAILED(DwmGetWindowAttribute(window_,DWMWA_EXTENDED_FRAME_BOUNDS,&bounds,sizeof(bounds))) || bounds.right<=bounds.left || bounds.bottom<=bounds.top) GetWindowRect(window_,&bounds);
        // WGC captures the extended frame, including title bars. Convert physical
        // capture pixels to the current window bounds, then to child client units.
        POINT screen{bounds.left+LONG(int64_t(event.x)*(bounds.right-bounds.left)/geometry.source_width),
            bounds.top+LONG(int64_t(event.y)*(bounds.bottom-bounds.top)/geometry.source_height)};
        POINT client=screen; ScreenToClient(window_,&client); RECT rect{}; GetClientRect(window_,&rect);
        int button=event.message==WM_LBUTTONDOWN||event.message==WM_LBUTTONUP?0:
            event.message==WM_RBUTTONDOWN||event.message==WM_RBUTTONUP?1:
            event.message==WM_MBUTTONDOWN||event.message==WM_MBUTTONUP?2:-1;
        bool down=event.message==WM_LBUTTONDOWN||event.message==WM_RBUTTONDOWN||event.message==WM_MBUTTONDOWN;
        bool up=event.message==WM_LBUTTONUP||event.message==WM_RBUTTONUP||event.message==WM_MBUTTONUP;
        if (!PtInRect(&rect,client) && !(up && buttons_[button])) return true;
        HWND target=ChildAt(screen);
        if (button>=0 && buttons_[button]) target=buttons_[button];
        if (event.message==WM_MOUSEMOVE) for (auto pressed:buttons_) if (pressed) { target=pressed; break; }
        if (up && !buttons_[button]) return true;
        if (down) { keyboard_=target; if (!Deliver(target,WM_SETFOCUS,0,0)) return false; }
        POINT point=screen; ScreenToClient(target,&point); last_=screen;
        WPARAM flags=Flags();
        if (button>=0) { const WPARAM bit=button==0?MK_LBUTTON:button==1?MK_RBUTTON:MK_MBUTTON; flags=down?flags|bit:flags&~bit; }
        if (event.message==WM_MOUSEWHEEL||event.message==WM_MOUSEHWHEEL) {
            flags=MAKEWPARAM(flags,SHORT(event.key)); point=screen;
        }
        bool ok=Deliver(target,event.message,flags,MAKELPARAM(point.x,point.y));
        if (ok && button>=0) buttons_[button]=down?target:nullptr;
        return ok;
    }
};
}
