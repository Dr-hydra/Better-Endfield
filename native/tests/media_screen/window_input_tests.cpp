#include "../../modules/media_screen/window_input.h"
#include <commctrl.h>
#include <chrono>
#include <iostream>
#include <thread>
#include <vector>
using namespace BetterEndfieldNext::MediaScreen;
namespace {
void Check(bool value,const char* message) { if (!value) throw std::runtime_error(message); }
struct Message { UINT message; WPARAM key; };
std::vector<Message> seen;
std::wstring qt_text;
int qt_letter_keys=0,qt_shortcuts=0,qt_navigation=0;
LRESULT CALLBACK QtFixture(HWND hwnd,UINT message,WPARAM key,LPARAM data) {
    if (message==WM_KEYDOWN) {
        if (key>='A' && key<='Z') { ++qt_letter_keys; qt_text+=wchar_t(key); }
        if (key==VK_LEFT || key==VK_BACK || key==VK_RETURN) ++qt_navigation;
        if (key=='C') ++qt_shortcuts;
    }
    if (message==WM_CHAR || message==WM_IME_CHAR) { qt_text+=wchar_t(key); return 0; }
    return DefWindowProcW(hwnd,message,key,data);
}
LRESULT CALLBACK Monitor(HWND hwnd,UINT message,WPARAM key,LPARAM data,UINT_PTR,DWORD_PTR) {
    if (message==WM_KEYDOWN||message==WM_KEYUP||message==WM_LBUTTONDOWN||message==WM_LBUTTONUP||message==WM_MOUSEWHEEL) seen.push_back({message,key});
    return DefSubclassProc(hwnd,message,key,data);
}
void Pump() { for (int i=0;i<20;++i) { MSG m{}; while (PeekMessageW(&m,nullptr,0,0,PM_REMOVE)) DispatchMessageW(&m); std::this_thread::sleep_for(std::chrono::milliseconds(2)); } }
}
int main(int argc,char** argv) {
    try {
        SetThreadDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
        std::string operation=argc>1?argv[1]:"";
        if (argc==6 && (operation=="--send" || operation=="--send-backspace" || operation=="--send-return")) {
            HWND target=reinterpret_cast<HWND>(uintptr_t(std::stoull(argv[2]))); DWORD pid=0; GetWindowThreadProcessId(target,&pid);
            RECT bounds{}; if (FAILED(DwmGetWindowAttribute(target,DWMWA_EXTENDED_FRAME_BOUNDS,&bounds,sizeof(bounds))) || bounds.right<=bounds.left || bounds.bottom<=bounds.top) GetWindowRect(target,&bounds);
            CaptureGeometry geometry{uint32_t(bounds.right-bounds.left),uint32_t(bounds.bottom-bounds.top),0,0,uint32_t(bounds.right-bounds.left),uint32_t(bounds.bottom-bounds.top)};
            std::cout<<"Capture bounds: "<<bounds.left<<','<<bounds.top<<' '<<geometry.source_width<<'x'<<geometry.source_height<<'\n';
            WindowInput input; Check(input.Bind(uintptr_t(target),pid),"target unavailable");
            int x=std::stoi(argv[3]), y=std::stoi(argv[4]);
            Check(input.Send({WM_LBUTTONDOWN,0,0,x,y},geometry)&&input.Send({WM_LBUTTONUP,0,0,x,y},geometry),"click failed");
            wchar_t klass[128]{}; GetClassNameW(input.KeyboardRecipient(),klass,128); std::wcout<<L"Input recipient: "<<klass<<L" hwnd="<<uintptr_t(input.KeyboardRecipient())<<L'\n';
            Pump();
            auto type=[&](char c) {
                UINT vk=LOBYTE(VkKeyScanA(c)); auto scan=MapVirtualKeyW(vk,MAPVK_VK_TO_VSC_EX);
                int64_t data=1|(int64_t(scan&255)<<16)|((scan&0xFF00)?int64_t(1)<<24:0);
                for (auto event:{WindowInputEvent{WM_KEYDOWN,vk,data,0,0},WindowInputEvent{WM_CHAR,uint32_t(c),data,0,0},WindowInputEvent{WM_KEYUP,vk,data|0xC0000000LL,0,0}})
                    if (!input.Send(event,geometry)) throw std::runtime_error("Text fixture message "+std::to_string(event.message)+": "+input.Error());
            };
            for (char c:std::string(argv[5])) type(c);
            int64_t arrow=1|(int64_t(0x4B)<<16)|(int64_t(1)<<24);
            Check(input.Send({WM_KEYDOWN,VK_LEFT,arrow,0,0},geometry)&&input.Send({WM_KEYUP,VK_LEFT,arrow|0xC0000000LL,0,0},geometry),"arrow key failed");
            type('X');
            if (operation!="--send") {
                UINT key=operation=="--send-backspace"?VK_BACK:VK_RETURN;
                auto scan=MapVirtualKeyW(key,MAPVK_VK_TO_VSC_EX);
                int64_t data=1|(int64_t(scan&255)<<16);
                Check(input.Send({WM_KEYDOWN,key,data,0,0},geometry) &&
                    input.Send({WM_CHAR,operation=="--send-backspace"?8u:13u,data,0,0},geometry) &&
                    input.Send({WM_KEYUP,key,data|0xC0000000LL,0,0},geometry),"navigation command failed");
            }
            Check(input.Send({WM_MOUSEMOVE,0,0,x,y},geometry),"hover failed");
            Check(input.Send({WM_MOUSEWHEEL,uint32_t(int32_t(-120)),0,x,y},geometry),"wheel failed");
            Pump(); std::cout<<"Directed input sent; verify target state separately\n"; return 0;
        }
        HWND root=CreateWindowW(L"STATIC",L"BE.WindowInput.Fixture",WS_POPUP,100,100,400,300,nullptr,nullptr,GetModuleHandleW(nullptr),nullptr);
        Check(root!=nullptr,"fixture creation failed");
        HWND edit=CreateWindowW(L"EDIT",L"",WS_CHILD|WS_VISIBLE|ES_MULTILINE,10,10,300,200,root,nullptr,GetModuleHandleW(nullptr),nullptr);
        Check(edit && SetWindowSubclass(edit,Monitor,1,0),"input monitor unavailable");
        HWND foreground=GetForegroundWindow();
        CaptureGeometry geometry{400,300,0,0,400,300};
        WindowInput input; Check(input.Bind(uintptr_t(root),GetCurrentProcessId()),"fixture binding failed");
        Check(input.Send({WM_LBUTTONDOWN,0,0,30,30},geometry)&&input.Send({WM_LBUTTONUP,0,0,30,30},geometry),"background click rejected");
        Check(input.Send({WM_CHAR,'H',1,0,0},geometry)&&input.Send({WM_CHAR,'i',1,0,0},geometry),"background text rejected");
        Check(input.Send({WM_KEYDOWN,VK_LEFT,1,0,0},geometry)&&input.Send({WM_LBUTTONDOWN,0,0,30,30},geometry),"owned key/button press rejected");
        Check(input.Send({WM_MOUSEWHEEL,120,0,30,30},geometry),"background wheel rejected");
        input.Release(); Pump();
        wchar_t text[32]{}; GetWindowTextW(edit,text,32);
        Check(std::wstring(text)==L"Hi","directed text did not reach actual EDIT control");
        int downs=0,ups=0,key_downs=0,key_ups=0,wheels=0;
        for (const auto& m:seen) { downs+=m.message==WM_LBUTTONDOWN; ups+=m.message==WM_LBUTTONUP; key_downs+=m.message==WM_KEYDOWN; key_ups+=m.message==WM_KEYUP; wheels+=m.message==WM_MOUSEWHEEL; }
        Check(downs==2&&ups==2&&key_downs==1&&key_ups==1&&wheels==1,"exit failed to release owned key/button or wheel missing");
        Check(GetForegroundWindow()==foreground,"background input stole foreground");
        Check(!input.Bind(uintptr_t(root),GetCurrentProcessId()+1),"PID mismatch accepted");
        Check(input.Bind(uintptr_t(root),GetCurrentProcessId()),"re-entry failed");
        DestroyWindow(root); Check(!input.Send({WM_CHAR,'X',1,0,0},geometry),"closed source accepted input");
        WNDCLASSW klass{}; klass.lpfnWndProc=QtFixture; klass.hInstance=GetModuleHandleW(nullptr); klass.lpszClassName=L"Qt51514QWindowIcon";
        Check(RegisterClassW(&klass)!=0,"Qt behavior fixture class unavailable");
        HWND qt=CreateWindowW(klass.lpszClassName,L"BE.Qt.Input.Fixture",WS_POPUP,100,100,400,300,nullptr,nullptr,klass.hInstance,nullptr);
        Check(qt && input.Bind(uintptr_t(qt),GetCurrentProcessId()),"Qt fixture binding failed");
        for (wchar_t character:{L'a',L'A',L'\u4e2d'}) {
            Check(input.Send({WM_KEYDOWN,'A',1,0,0},geometry) && input.Send({WM_CHAR,uint32_t(character),1,0,0},geometry) &&
                input.Send({WM_KEYUP,'A',0xC0000001LL,0,0},geometry),"Qt text sequence rejected");
        }
        Pump(); Check(qt_text==L"aA\u4e2d" && qt_letter_keys==0,"Qt text duplicated or changed case/Unicode");
        qt_text.clear();
        Check(input.Send({WM_KEYDOWN,VK_CONTROL,1,0,0},geometry) && input.Send({WM_KEYDOWN,'C',1,0,0},geometry) &&
            input.Send({WM_CHAR,3,1,0,0},geometry) && input.Send({WM_KEYUP,'C',0xC0000001LL,0,0},geometry) &&
            input.Send({WM_KEYUP,VK_CONTROL,0xC0000001LL,0,0},geometry),"Qt shortcut messages rejected");
        Pump(); Check(qt_shortcuts==1 && qt_text==L"C","Qt shortcut lost its key or received duplicate text");
        qt_text.clear();
        for (auto key:{VK_LEFT,VK_BACK,VK_RETURN}) {
            Check(input.Send({WM_KEYDOWN,uint32_t(key),1,0,0},geometry) && input.Send({WM_KEYUP,uint32_t(key),0xC0000001LL,0,0},geometry),"Qt navigation rejected");
        }
        Check(input.Send({WM_CHAR,'\r',1,0,0},geometry) && input.Send({WM_CHAR,'\b',1,0,0},geometry),"Qt control-char filtering failed");
        Pump(); Check(qt_navigation==3 && qt_text.empty(),"Qt navigation repeated its action as text");
        input.Release(); DestroyWindow(qt); UnregisterClassW(klass.lpszClassName,klass.hInstance);
        std::cout<<"PASS window input: real background EDIT text/click/wheel, release, foreground preservation, PID/closure rejection\n"; return 0;
    } catch (const std::exception& error) { std::cerr<<"FAIL "<<error.what()<<'\n'; return 1; }
}
