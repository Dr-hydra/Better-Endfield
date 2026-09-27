#pragma once
#include <cstdint>
namespace betterendfield {
enum class FrameClient : unsigned { Camera, Ui, Count };
using FrameCallback = void (*)(bool suspend);
// Only DispatchAndroidFrame calls clients, on the nativeRender thread. No timer
// thread is permitted to mutate Unity objects or Time.timeScale.
void DispatchAndroidFrame();
bool OnAndroidFrameThread();
bool HasAndroidFrameBridge();
void SetAndroidFrameClient(FrameClient client, FrameCallback callback);
void SetAndroidForeground(bool foreground);
bool AndroidForeground();
void PublishAndroidCameraState(unsigned capabilities, unsigned active);
unsigned AndroidCameraCapabilities();
unsigned AndroidCameraActive();
void PublishAndroidHudState(bool hidden);
bool AndroidHudHidden();
void AddAndroidLook(int dx, int dy);
void TakeAndroidLook(int& dx, int& dy);
}
