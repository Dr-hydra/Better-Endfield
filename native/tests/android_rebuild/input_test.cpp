#include "android_virtual_keys.h"
#include "android_frame.h"
#include <cassert>
#include <thread>
#include <iostream>
using namespace betterendfield;
static int frames, suspended;
static void Frame(bool suspend) { assert(OnAndroidFrameThread()); ++frames; suspended += suspend; }
int main() {
    assert(!SetVirtualKey(0, VirtualKeyAction::Press));
    assert(!SetVirtualKey(256, VirtualKeyAction::Press));
    assert(!SetVirtualKey(50, static_cast<VirtualKeyAction>(77)));
    ReleaseAllVirtualKeys();
    for(int i=0;i<32;++i) assert(SetVirtualKey(57,VirtualKeyAction::Pulse));
    assert(!SetVirtualKey(57,VirtualKeyAction::Pulse));
    for(int i=0;i<32;++i) { assert(VirtualKeyDown(57)); assert(!VirtualKeyDown(57)); }
    assert(!VirtualKeyDown(57));
    assert(SetVirtualKey(38,VirtualKeyAction::Press));
    for(int i=0;i<100;++i) assert(VirtualKeyDown(38));
    SetVirtualKey(38,VirtualKeyAction::Release); assert(!VirtualKeyDown(38));
    SetVirtualKey(38,VirtualKeyAction::Press); SetVirtualKey(57,VirtualKeyAction::Pulse);
    SetAndroidForeground(false); assert(!VirtualKeyDown(38)); assert(!VirtualKeyDown(57));
    SetAndroidFrameClient(FrameClient::Camera,Frame);
    DispatchAndroidFrame(); assert(frames==1 && suspended==1);
    SetAndroidForeground(true); DispatchAndroidFrame(); assert(frames==2 && suspended==1);
    std::thread other([]{ assert(!OnAndroidFrameThread()); DispatchAndroidFrame(); });other.join();
    assert(frames==2);
    AddAndroidLook(900,-900); int x=0,y=0;TakeAndroidLook(x,y);assert(x==512&&y==-512);
    TakeAndroidLook(x,y);assert(x==0&&y==0);
    AddAndroidLook(20,20);SetAndroidForeground(false);TakeAndroidLook(x,y);assert(x==0&&y==0);
    SetAndroidForeground(true);
    SetAndroidFrameClient(FrameClient::Camera,nullptr);DispatchAndroidFrame();assert(frames==2);
    std::thread writer([]{for(int i=0;i<10000;++i){SetVirtualKey(38,VirtualKeyAction::Press);SetVirtualKey(38,VirtualKeyAction::Release);}});
    for(int i=0;i<10000;++i) (void)VirtualKeyDown(38);
    writer.join();ReleaseAllVirtualKeys();assert(!VirtualKeyDown(38));
    std::cout<<"PASS input queue, held release, lifecycle, frame affinity, look limits\n";
}
