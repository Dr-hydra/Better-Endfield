#include "window_capture.h"
#include "output_limits.h"
#include <Windows.h>
#include <d3d11.h>
#include <dxgi1_2.h>
#include <windows.graphics.capture.interop.h>
#include <windows.graphics.directx.direct3d11.interop.h>
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Graphics.Capture.h>
#include <winrt/Windows.Graphics.DirectX.h>
#include <winrt/Windows.Graphics.DirectX.Direct3D11.h>
#include <algorithm>
#include <atomic>
#include <chrono>
#include <condition_variable>
#include <cstdio>
#include <mutex>
#include <stdexcept>
#include <thread>

namespace BetterEndfieldNext::MediaScreen {
using namespace winrt::Windows::Graphics::Capture;
using namespace winrt::Windows::Graphics::DirectX;
using namespace winrt::Windows::Graphics::DirectX::Direct3D11;
using winrt::Windows::Graphics::SizeInt32;
namespace {
std::string CaptureFailure(const char* step, const winrt::hresult_error& error) {
    char code[11]{};
    std::snprintf(code, sizeof(code), "0x%08X", static_cast<uint32_t>(static_cast<int32_t>(error.code())));
    return std::string("Window capture ") + step + " failed (" + code + "): " + winrt::to_string(error.message());
}
}
struct WindowCapture::State {
    std::thread worker;
    std::atomic_bool stop{false};
    std::mutex mutex;
    std::condition_variable wake;
    std::string error;
    FrameData frame;
    void Fail(const std::string& value) { std::lock_guard lock(mutex); if (error.empty()) error = value; }
};
WindowCapture::WindowCapture() = default;
WindowCapture::~WindowCapture() { Stop(); }
void WindowCapture::Stop() {
    auto state = state_;
    if (!state) return;
    state->stop.store(true); state->wake.notify_all();
    if (state->worker.joinable()) state->worker.join();
    state_.reset();
}
bool WindowCapture::Take(uint64_t last, FrameData& out) {
    auto state = state_; if (!state) return false;
    std::lock_guard lock(state->mutex);
    if (!state->frame.serial || state->frame.serial == last) return false;
    out = state->frame; return true;
}
std::string WindowCapture::Error() {
    auto state = state_; if (!state) return {};
    std::lock_guard lock(state->mutex); return state->error;
}
void WindowCapture::Start(uint64_t window, uint32_t pid, int width, int height, int fps) {
    Stop();
    if (!ValidOutput(width, height, fps)) throw std::runtime_error("Capture output dimensions unsupported");
    HWND hwnd = reinterpret_cast<HWND>(static_cast<uintptr_t>(window)); DWORD actual = 0;
    if (!IsWindow(hwnd) || !GetWindowThreadProcessId(hwnd, &actual) || !pid || actual != pid || actual == GetCurrentProcessId())
        throw std::runtime_error("Selected capture window is no longer available");
    auto state = std::make_shared<State>(); state_ = state;
    state->worker = std::thread([state, hwnd, width, height, fps] {
        const char* step = "initialize_apartment";
        try {
            winrt::init_apartment(winrt::apartment_type::multi_threaded);
            step = "check_support";
            if (!GraphicsCaptureSession::IsSupported()) throw std::runtime_error("Window capture unsupported");
            winrt::com_ptr<ID3D11Device> device; winrt::com_ptr<ID3D11DeviceContext> context;
            step = "create_device";
            winrt::check_hresult(D3D11CreateDevice(nullptr, D3D_DRIVER_TYPE_HARDWARE, nullptr,
                D3D11_CREATE_DEVICE_BGRA_SUPPORT, nullptr, 0, D3D11_SDK_VERSION, device.put(), nullptr, context.put()));
            auto dxgi = device.as<IDXGIDevice>(); winrt::com_ptr<IInspectable> inspectable;
            step = "create_winrt_device";
            winrt::check_hresult(CreateDirect3D11DeviceFromDXGIDevice(dxgi.get(), inspectable.put()));
            auto direct = inspectable.as<IDirect3DDevice>();
            step = "create_capture_item";
            auto interop = winrt::get_activation_factory<GraphicsCaptureItem, IGraphicsCaptureItemInterop>();
            GraphicsCaptureItem item{nullptr};
            winrt::check_hresult(interop->CreateForWindow(hwnd, winrt::guid_of<GraphicsCaptureItem>(), winrt::put_abi(item)));
            auto size = item.Size();
            step = "create_frame_pool";
            auto pool = Direct3D11CaptureFramePool::CreateFreeThreaded(direct, DirectXPixelFormat::B8G8R8A8UIntNormalized, 2, size);
            step = "create_session";
            auto session = pool.CreateCaptureSession(item);
            // A queued delegate can outlive revocation or an exception during
            // StartCapture. It owns its context and mutex rather than stack references.
            struct CallbackData {
                winrt::com_ptr<ID3D11Device> device;
                winrt::com_ptr<ID3D11DeviceContext> context;
                IDirect3DDevice direct{nullptr};
                winrt::com_ptr<ID3D11Texture2D> staging;
                D3D11_TEXTURE2D_DESC staging_desc{};
                SizeInt32 size{};
                uint64_t serial = 0;
                std::chrono::steady_clock::time_point last{};
                std::mutex mutex;
            };
            auto data = std::make_shared<CallbackData>();
            data->device = device; data->context = context; data->direct = direct; data->size = size;
            auto closed = item.Closed(winrt::auto_revoke, [state](auto const&, auto const&) { state->Fail("Capture window closed"); });
            auto arrived = pool.FrameArrived(winrt::auto_revoke, [state, data, width, height, fps](auto const& sender, auto const&) {
                std::lock_guard callback_lock(data->mutex);
                if (state->stop.load()) return;
                auto& device = data->device; auto& context = data->context;
                auto& staging = data->staging; auto& staging_desc = data->staging_desc;
                auto& size = data->size; auto& serial = data->serial; auto& last = data->last;
                try {
                    auto frame = sender.TryGetNextFrame(); if (!frame) return;
                    auto content = frame.ContentSize();
                    if (content.Width <= 0 || content.Height <= 0) return;
                    if (content.Width > 8192 || content.Height > 8192 || uint64_t(content.Width) * content.Height > 16777216)
                        throw std::runtime_error("Capture source dimensions unsupported");
                    const auto now = std::chrono::steady_clock::now();
                    if (now - last < std::chrono::duration<double>(1.0 / fps)) return;
                    last = now;
                    auto access = frame.Surface().as<::Windows::Graphics::DirectX::Direct3D11::IDirect3DDxgiInterfaceAccess>();
                    winrt::com_ptr<ID3D11Texture2D> texture;
                    winrt::check_hresult(access->GetInterface(__uuidof(ID3D11Texture2D), texture.put_void()));
                    D3D11_TEXTURE2D_DESC desc{}; texture->GetDesc(&desc);
                    if (!staging || desc.Width != staging_desc.Width || desc.Height != staging_desc.Height) {
                        staging = nullptr; staging_desc = desc;
                        staging_desc.BindFlags = staging_desc.MiscFlags = 0;
                        staging_desc.Usage = D3D11_USAGE_STAGING; staging_desc.CPUAccessFlags = D3D11_CPU_ACCESS_READ;
                        winrt::check_hresult(device->CreateTexture2D(&staging_desc, nullptr, staging.put()));
                    }
                    context->CopyResource(staging.get(), texture.get());
                    D3D11_MAPPED_SUBRESOURCE mapped{};
                    winrt::check_hresult(context->Map(staging.get(), 0, D3D11_MAP_READ, 0, &mapped));
                    FrameData output;
                    try {
                        output.rgba.assign(size_t(width) * height * 4, 0);
                        for (size_t i = 3; i < output.rgba.size(); i += 4) output.rgba[i] = 255;
                        int source_w = std::min(content.Width, static_cast<int>(desc.Width));
                        int source_h = std::min(content.Height, static_cast<int>(desc.Height));
                        double fit = std::min(double(width) / source_w, double(height) / source_h);
                        int w = std::max(1, int(source_w * fit)), h = std::max(1, int(source_h * fit));
                        int left = (width - w) / 2, top = (height - h) / 2;
                        output.geometry = {uint32_t(source_w), uint32_t(source_h), uint32_t(left), uint32_t(top), uint32_t(w), uint32_t(h)};
                        for (int y = 0; y < h; ++y) {
                            const auto* row = static_cast<const unsigned char*>(mapped.pData) + size_t(y * source_h / h) * mapped.RowPitch;
                            auto* destination = output.rgba.data() + (size_t(height - 1 - top - y) * width + left) * 4;
                            for (int x = 0; x < w; ++x) {
                                const auto* source = row + size_t(x * source_w / w) * 4;
                                destination[x * 4] = source[2]; destination[x * 4 + 1] = source[1];
                                destination[x * 4 + 2] = source[0]; destination[x * 4 + 3] = 255;
                            }
                        }
                    } catch (...) { context->Unmap(staging.get(), 0); throw; }
                    context->Unmap(staging.get(), 0);
                    output.serial = ++serial;
                    { std::lock_guard lock(state->mutex); state->frame = std::move(output); }
                    frame.Close();
                    if (content.Width != size.Width || content.Height != size.Height) {
                        size = content; sender.Recreate(data->direct, DirectXPixelFormat::B8G8R8A8UIntNormalized, 2, size);
                    }
                } catch (const winrt::hresult_error& error) { state->Fail(CaptureFailure("read_frame", error)); }
                catch (const std::exception& error) { state->Fail(error.what()); }
            });
            step = "start_capture"; session.StartCapture();
            { std::unique_lock lock(state->mutex); state->wake.wait(lock,[&] { return state->stop.load(); }); }
            arrived.revoke(); closed.revoke();
            // Wait for the callback to finish before releasing its captured objects.
            { std::lock_guard lock(data->mutex); }
            session.Close(); pool.Close();
        } catch (const winrt::hresult_error& error) { state->Fail(CaptureFailure(step, error)); }
        catch (const std::exception& error) { state->Fail(error.what()); }
        winrt::uninit_apartment();
    });
}
}
