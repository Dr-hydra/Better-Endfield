#pragma once
// Small layered-window shell following camera/overlay's Win32MmdOverlay conventions.
// All model controls live in main.cpp; this helper owns only GDI+ and game following.
#if defined(_WIN32)
#include <Windows.h>
#include <windowsx.h>
#include <objidl.h>
#include <gdiplus.h>
#include <algorithm>
#include <cmath>
#include <cstring>
#include <cstdint>
#include <functional>
#include <string>
#include <string_view>

namespace BetterEndfieldNext::CustomModel::Win32Overlay {
inline std::wstring Wide(std::string_view text) {
    if(text.empty()) return {};const int count=MultiByteToWideChar(CP_UTF8,0,text.data(),static_cast<int>(text.size()),nullptr,0);
    std::wstring out(static_cast<size_t>(count),L'\0');
    MultiByteToWideChar(CP_UTF8,0,text.data(),static_cast<int>(text.size()),out.data(),count);return out;
}
inline void Rounded(Gdiplus::GraphicsPath& path,const Gdiplus::RectF& r,float radius) {
    const float d=radius*2;path.AddArc(r.X,r.Y,d,d,180,90);path.AddArc(r.GetRight()-d,r.Y,d,d,270,90);
    path.AddArc(r.GetRight()-d,r.GetBottom()-d,d,d,0,90);path.AddArc(r.X,r.GetBottom()-d,d,d,90,90);path.CloseFigure();
}
inline void Label(Gdiplus::Graphics& g,const std::wstring& text,const Gdiplus::RectF& rect,
    const Gdiplus::Color& color=Gdiplus::Color(255,241,244,249),float size=14,bool bold=false) {
    Gdiplus::Font font(L"Microsoft YaHei UI",size,bold?Gdiplus::FontStyleBold:Gdiplus::FontStyleRegular,Gdiplus::UnitPixel);
    Gdiplus::SolidBrush brush(color);Gdiplus::StringFormat format;
    format.SetLineAlignment(Gdiplus::StringAlignmentCenter);format.SetTrimming(Gdiplus::StringTrimmingEllipsisCharacter);
    format.SetFormatFlags(Gdiplus::StringFormatFlagsNoWrap);
    g.DrawString(text.c_str(),-1,&font,rect,&format,&brush);
}
class Window {
public:
    std::function<void()> tick;
    std::function<void(Gdiplus::Graphics&)> paint;
    std::function<bool(UINT,float,float,WPARAM)> mouse;
    DWORD game_pid=0;
    HWND handle=nullptr;
    float width=620,height=660;
    bool visible=false;
    bool Create(HINSTANCE instance) {
        Gdiplus::GdiplusStartupInput input;
        if(Gdiplus::GdiplusStartup(&token_,&input,nullptr)!=Gdiplus::Ok) return false;
        WNDCLASSEXW wc{sizeof(wc)};wc.hInstance=instance;wc.lpfnWndProc=Proc;
        wc.hCursor=LoadCursorW(nullptr,IDC_ARROW);wc.lpszClassName=L"BetterEndfieldNext.ModelOverlay.Window";
        RegisterClassExW(&wc);
        handle=CreateWindowExW(WS_EX_LAYERED|WS_EX_TOOLWINDOW|WS_EX_NOACTIVATE,wc.lpszClassName,
            L"Better Endfield Next Models",WS_POPUP,0,0,static_cast<int>(width),static_cast<int>(height),nullptr,nullptr,instance,this);
        if(!handle) return false;SetTimer(handle,1,50,nullptr);return true;
    }
    ~Window() {
        if(handle&&IsWindow(handle)) DestroyWindow(handle);
        if(token_) Gdiplus::GdiplusShutdown(token_);
    }
    bool ForegroundGame() const {
        DWORD pid=0;GetWindowThreadProcessId(GetForegroundWindow(),&pid);return pid==game_pid;
    }
    void Follow() {
        DWORD pid=0;const HWND fg=GetForegroundWindow();GetWindowThreadProcessId(fg,&pid);
        if(pid==game_pid) {const auto root=GetAncestor(fg,GA_ROOT);game_=root?root:fg;}
        if(!game_||!IsWindow(game_)) {Find context{game_pid};EnumWindows(FindProc,reinterpret_cast<LPARAM>(&context));game_=context.window;}
        if(game_&&owner_!=game_) {
            SetLastError(0);const auto previous=SetWindowLongPtrW(handle,GWLP_HWNDPARENT,reinterpret_cast<LONG_PTR>(game_));
            if(previous||GetLastError()==0) owner_=game_;
        }
        if(!visible||!ForegroundGame()||!game_||!IsWindowVisible(game_)||IsIconic(game_)) {ShowWindow(handle,SW_HIDE);return;}
        RECT client{};POINT origin{};if(!GetClientRect(game_,&client)||!ClientToScreen(game_,&origin)) {ShowWindow(handle,SW_HIDE);return;}
        const UINT dpi=GetDpiForWindow(game_);scale_=dpi?static_cast<float>(dpi)/96:1;
        // Fit the content viewport to short game windows; scroll remains available.
        height=std::max(180.0f,std::min(660.0f,static_cast<float>(client.bottom)/scale_));
        width=std::max(360.0f,std::min(620.0f,static_cast<float>(client.right)/scale_));
        const int w=static_cast<int>(std::lround(width*scale_)),h=static_cast<int>(std::lround(height*scale_));
        if(dragging_) {POINT cursor{};GetCursorPos(&cursor);offset_x_=static_cast<float>(cursor.x-origin.x)/scale_-drag_x_;offset_y_=static_cast<float>(cursor.y-origin.y)/scale_-drag_y_;}
        offset_x_=std::clamp(offset_x_,0.0f,std::max(0.0f,static_cast<float>(client.right)/scale_-width));
        offset_y_=std::clamp(offset_y_,0.0f,std::max(0.0f,static_cast<float>(client.bottom)/scale_-height));
        position_={origin.x+static_cast<LONG>(offset_x_*scale_),origin.y+static_cast<LONG>(offset_y_*scale_)};
        SetWindowPos(handle,HWND_TOP,position_.x,position_.y,w,h,SWP_NOACTIVATE);
        if(!IsWindowVisible(handle)) ShowWindow(handle,SW_SHOWNOACTIVATE);
        Render();
    }
    void Render() {
        if(!handle||!IsWindowVisible(handle)||!paint) return;
        const int w=static_cast<int>(std::lround(width*scale_)),h=static_cast<int>(std::lround(height*scale_));
        BITMAPINFO info{};info.bmiHeader.biSize=sizeof(BITMAPINFOHEADER);info.bmiHeader.biWidth=w;info.bmiHeader.biHeight=-h;
        info.bmiHeader.biPlanes=1;info.bmiHeader.biBitCount=32;info.bmiHeader.biCompression=BI_RGB;
        HDC screen=GetDC(nullptr),memory=CreateCompatibleDC(screen);void* pixels=nullptr;
        HBITMAP dib=CreateDIBSection(screen,&info,DIB_RGB_COLORS,&pixels,nullptr,0);
        if(!dib||!pixels) {if(dib) DeleteObject(dib);DeleteDC(memory);ReleaseDC(nullptr,screen);return;}
        const auto old=SelectObject(memory,dib);std::memset(pixels,0,static_cast<size_t>(w)*h*4);
        {
            Gdiplus::Bitmap bitmap(w,h,w*4,PixelFormat32bppPARGB,static_cast<BYTE*>(pixels));Gdiplus::Graphics g(&bitmap);
            g.ScaleTransform(scale_,scale_);g.SetSmoothingMode(Gdiplus::SmoothingModeAntiAlias);
            g.SetTextRenderingHint(Gdiplus::TextRenderingHintAntiAliasGridFit);
            Gdiplus::GraphicsPath path;Rounded(path,{0.5f,0.5f,width-1,height-1},14);
            Gdiplus::SolidBrush background(Gdiplus::Color(240,17,20,27));Gdiplus::Pen border(Gdiplus::Color(90,255,255,255));
            g.FillPath(&background,&path);g.DrawPath(&border,&path);paint(g);
        }
        POINT source{};SIZE size{w,h};BLENDFUNCTION blend{AC_SRC_OVER,0,255,AC_SRC_ALPHA};
        UpdateLayeredWindow(handle,screen,&position_,&size,memory,&source,0,&blend,ULW_ALPHA);
        SelectObject(memory,old);DeleteObject(dib);DeleteDC(memory);ReleaseDC(nullptr,screen);
    }
private:
    struct Find {DWORD pid;HWND window=nullptr;uint64_t area=0;};
    static BOOL CALLBACK FindProc(HWND hwnd,LPARAM data) {
        auto& f=*reinterpret_cast<Find*>(data);DWORD pid=0;GetWindowThreadProcessId(hwnd,&pid);
        if(pid!=f.pid||!IsWindowVisible(hwnd)||GetWindow(hwnd,GW_OWNER)) return TRUE;
        RECT r{};GetClientRect(hwnd,&r);const auto area=static_cast<uint64_t>(std::max(0L,r.right))*std::max(0L,r.bottom);
        if(area>f.area) {f.area=area;f.window=hwnd;}return TRUE;
    }
    static LRESULT CALLBACK Proc(HWND hwnd,UINT message,WPARAM wp,LPARAM lp) {
        auto* self=reinterpret_cast<Window*>(GetWindowLongPtrW(hwnd,GWLP_USERDATA));
        if(message==WM_NCCREATE) {self=static_cast<Window*>(reinterpret_cast<CREATESTRUCTW*>(lp)->lpCreateParams);SetWindowLongPtrW(hwnd,GWLP_USERDATA,reinterpret_cast<LONG_PTR>(self));}
        if(!self) return DefWindowProcW(hwnd,message,wp,lp);
        const float x=static_cast<float>(GET_X_LPARAM(lp))/self->scale_,y=static_cast<float>(GET_Y_LPARAM(lp))/self->scale_;
        switch(message) {
        case WM_TIMER: if(self->tick) self->tick();return 0;
        case WM_MOUSEACTIVATE: return MA_NOACTIVATE;
        case WM_LBUTTONDOWN:
            SetCapture(hwnd);
            if(self->mouse&&self->mouse(message,x,y,wp)) return 0;
            if(y<48) {self->dragging_=true;self->drag_x_=x;self->drag_y_=y;}return 0;
        case WM_LBUTTONUP:
            if(self->mouse) self->mouse(message,x,y,wp);
            self->dragging_=false;ReleaseCapture();return 0;
        case WM_CAPTURECHANGED:
            self->dragging_=false;if(self->mouse) self->mouse(message,x,y,wp);return 0;
        case WM_MOUSEMOVE: if(self->mouse) self->mouse(message,x,y,wp);return 0;
        case WM_MOUSEWHEEL: if(self->mouse) self->mouse(message,0,0,wp);return 0;
        case WM_DPICHANGED: self->Follow();return 0;
        case WM_DESTROY: PostQuitMessage(0);return 0;
        default: return DefWindowProcW(hwnd,message,wp,lp);
        }
    }
    ULONG_PTR token_=0;
    HWND game_=nullptr,owner_=nullptr;
    float scale_=1,offset_x_=24,offset_y_=100,drag_x_=0,drag_y_=0;
    bool dragging_=false;
    POINT position_{};
};
} // namespace BetterEndfieldNext::CustomModel::Win32Overlay
#endif
