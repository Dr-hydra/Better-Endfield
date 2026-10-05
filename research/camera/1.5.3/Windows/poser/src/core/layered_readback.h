#pragma once
#include <d3d11.h>
#include <cstdint>

// One outstanding copy: a busy device never stalls the window/message thread
// or accumulates queued frames. The caller keeps the last displayed bitmap.
struct LayeredFrame {
  int x = 0, y = 0, width = 0, height = 0;
  uint64_t hash = 0;
};

class LayeredReadback {
  ID3D11Texture2D *staging_ = nullptr;
  ID3D11DeviceContext *mappedContext_ = nullptr;
  int width_ = 0, height_ = 0;
  bool pending_ = false;
  LayeredFrame frame_;
public:
  LayeredReadback() = default;
  LayeredReadback(const LayeredReadback &) = delete;
  LayeredReadback &operator=(const LayeredReadback &) = delete;
  ~LayeredReadback() { Reset(); }
  bool Pending() const { return pending_; }
  const LayeredFrame &Frame() const { return frame_; }

  void Finish() {
    if (mappedContext_) {
      mappedContext_->Unmap(staging_, 0);
      mappedContext_->Release();
      mappedContext_ = nullptr;
    }
    pending_ = false;
  }
  void Reset() {
    Finish();
    if (staging_) { staging_->Release(); staging_ = nullptr; }
    width_ = height_ = 0;
    frame_ = {};
  }
  HRESULT Queue(ID3D11DeviceContext *context, ID3D11Texture2D *source,
                const LayeredFrame &frame) {
    if (pending_) return DXGI_ERROR_WAS_STILL_DRAWING;
    if (!context || !source || frame.x < 0 || frame.y < 0 ||
        frame.width <= 0 || frame.height <= 0) return E_INVALIDARG;
    D3D11_TEXTURE2D_DESC desc = {};
    source->GetDesc(&desc);
    if (uint64_t(frame.x) + frame.width > desc.Width ||
        uint64_t(frame.y) + frame.height > desc.Height ||
        desc.Format != DXGI_FORMAT_B8G8R8A8_UNORM || desc.SampleDesc.Count != 1)
      return E_INVALIDARG;
    if (!staging_ || width_ != frame.width || height_ != frame.height) {
      Reset();
      desc.Width = frame.width;
      desc.Height = frame.height;
      desc.MipLevels = desc.ArraySize = 1;
      desc.Usage = D3D11_USAGE_STAGING;
      desc.BindFlags = desc.MiscFlags = 0;
      desc.CPUAccessFlags = D3D11_CPU_ACCESS_READ;
      ID3D11Device *device = nullptr;
      source->GetDevice(&device);
      HRESULT hr = device->CreateTexture2D(&desc, nullptr, &staging_);
      device->Release();
      if (FAILED(hr)) return hr;
      width_ = frame.width; height_ = frame.height;
    }
    frame_ = frame;
    D3D11_BOX box = {UINT(frame.x), UINT(frame.y), 0,
                    UINT(frame.x + frame.width), UINT(frame.y + frame.height), 1};
    context->CopySubresourceRegion(staging_, 0, 0, 0, 0, source, 0, &box);
    // There is no swap chain/present on this path to submit the commands.
    context->Flush();
    pending_ = true;
    return S_OK;
  }
  HRESULT TryMap(ID3D11DeviceContext *context, D3D11_MAPPED_SUBRESOURCE &map) {
    map = {};
    if (!pending_) return S_FALSE;
    if (!context || mappedContext_) return E_UNEXPECTED;
    HRESULT hr = context->Map(staging_, 0, D3D11_MAP_READ,
                              D3D11_MAP_FLAG_DO_NOT_WAIT, &map);
    if (SUCCEEDED(hr)) {
      mappedContext_ = context;
      mappedContext_->AddRef();
    } else if (hr != DXGI_ERROR_WAS_STILL_DRAWING) {
      Finish();
    }
    return hr;
  }
};
