#pragma once

// Private backend implementation; included after the shared state in gui_overlay.h.
#include "build_features.h"
#if POSER_ENABLE_LAYERED_OVERLAY
#include "layered_readback.h"
#include "overlay_device.h"
#include <cmath>

// ---- 分层窗口（UpdateLayeredWindow）覆盖层 ----
// XXMI / 3DMigoto 会把自己的 d3d11.dll 注入游戏进程，并给 IDXGIFactory 的
// CreateSwapChain* 打全局 vtable 钩子。DCompositionCreateDevice 内部会走到这些钩子，
// 此时钩子拿到的不是它包装过的设备，最终在 dxgi 里访问无效地址，整个游戏进程崩掉
// （表现为黑屏卡在开屏页）。
// 故此采用 ImGui 画到离屏纹理，拷贝到 staging再由CPU 读回，最后 UpdateLayeredWindow 逐像素 alpha 渲染。
static ID3D11Texture2D *g_pLayerTex = nullptr;      // 离屏渲染目标
static LayeredReadback g_layerReadback;
static HDC g_layerDC = nullptr;                     // 与 DIB 关联的内存 DC
static HBITMAP g_layerBmp = nullptr;                // 32bpp 顶朝下 DIB
static HGDIOBJ g_layerOldBmp = nullptr;
static void *g_layerBits = nullptr;
static int g_layerW = 0, g_layerH = 0;
static int g_prevDirtyX = 0, g_prevDirtyY = 0;      // 上一帧上传到分层表面的矩形
static int g_prevDirtyW = 0, g_prevDirtyH = 0;
// 分层路径：内容没变就不回读/不上传（面板静止时能省掉绝大部分 GPU→CPU 等待）
static bool g_layerForcePresent = true;
static unsigned long long g_layerLastHash = 0;
static int g_layerLastX = -1, g_layerLastY = -1, g_layerLastW = 0,
           g_layerLastH = 0;
static double QpcMs(LARGE_INTEGER a, LARGE_INTEGER b) {
  static double freq = 0.0;
  if (freq == 0.0) {
    LARGE_INTEGER f;
    QueryPerformanceFrequency(&f);
    freq = (double)f.QuadPart;
  }
  return (double)(b.QuadPart - a.QuadPart) * 1000.0 / freq;
}

// Use the verified, already loaded system module: LoadLibraryExW may be redirected.
static PFN_D3D11_CREATE_DEVICE GetSystemD3D11CreateDevice() {
  static OverlaySystemD3D11 system;
  if (!system.create) {
    system = LoadOverlaySystemD3D11();
    if (!system.create)
      Log("[GUI] system D3D11 verification failed (error=%lu); overlay disabled", system.error);
    else
      Log("[GUI] verified System32 D3D11 factory (module=%p)", system.module);
  }
  return system.create;
}

static void ReleaseLayerResources() {
  if (g_pMainRenderTargetView) { g_pMainRenderTargetView->Release(); g_pMainRenderTargetView = nullptr; }
  g_layerReadback.Reset();
  if (g_pLayerTex) { g_pLayerTex->Release(); g_pLayerTex = nullptr; }
  if (g_layerDC) {
    if (g_layerOldBmp) SelectObject(g_layerDC, g_layerOldBmp);
    DeleteDC(g_layerDC);
    g_layerDC = nullptr;
    g_layerOldBmp = nullptr;
  }
  if (g_layerBmp) { DeleteObject(g_layerBmp); g_layerBmp = nullptr; }
  g_layerBits = nullptr;
  g_prevDirtyW = g_prevDirtyH = 0;
  g_layerW = g_layerH = 0;
  g_layerForcePresent = true;
}

static bool CreateLayerResources(int w, int h) {
  ReleaseLayerResources();
  if (w <= 0) w = 1;
  if (h <= 0) h = 1;
  D3D11_TEXTURE2D_DESC td = {};
  td.Width = w;
  td.Height = h;
  td.MipLevels = 1;
  td.ArraySize = 1;
  td.Format = DXGI_FORMAT_B8G8R8A8_UNORM;
  td.SampleDesc.Count = 1;
  td.Usage = D3D11_USAGE_DEFAULT;
  td.BindFlags = D3D11_BIND_RENDER_TARGET;
  if (FAILED(g_pd3dDevice->CreateTexture2D(&td, nullptr, &g_pLayerTex)))
    return false;
  if (FAILED(g_pd3dDevice->CreateRenderTargetView(g_pLayerTex, nullptr,
                                                   &g_pMainRenderTargetView)))
    return false;
  // DIB 必须是**整窗大小**：分层窗口的位图源要覆盖整个窗口，
  // 脏矩形只通过 UpdateLayeredWindowIndirect 的 prcDirty 指定
  BITMAPINFO bi = {};
  bi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
  bi.bmiHeader.biWidth = w;
  bi.bmiHeader.biHeight = -h;
  bi.bmiHeader.biPlanes = 1;
  bi.bmiHeader.biBitCount = 32;
  bi.bmiHeader.biCompression = BI_RGB;
  HDC screen = GetDC(nullptr);
  g_layerDC = CreateCompatibleDC(screen);
  g_layerBmp = CreateDIBSection(screen, &bi, DIB_RGB_COLORS, &g_layerBits, nullptr, 0);
  ReleaseDC(nullptr, screen);
  if (!g_layerDC || !g_layerBmp || !g_layerBits)
    return false;
  g_layerOldBmp = SelectObject(g_layerDC, g_layerBmp);
  g_layerW = w;
  g_layerH = h;
  return true;
}

static bool CreateDeviceLayered(HWND hWnd) {
  D3D_FEATURE_LEVEL featureLevel;
  const D3D_FEATURE_LEVEL featureLevelArray[] = {D3D_FEATURE_LEVEL_11_0};
  PFN_D3D11_CREATE_DEVICE create = GetSystemD3D11CreateDevice();
  if (!create) return false;
  // EFMI shares hardware-driver hooks with the game. Keep this small overlay
  // on the software device; never silently fall back to the conflicting path.
  D3D_DRIVER_TYPE driver = g_xxmiDetected ? D3D_DRIVER_TYPE_WARP : D3D_DRIVER_TYPE_HARDWARE;
  HRESULT hr = create(nullptr, driver, nullptr, 0,
                      featureLevelArray, 1, D3D11_SDK_VERSION,
                      &g_pd3dDevice, &featureLevel, &g_pd3dDeviceContext);
  if (hr == DXGI_ERROR_UNSUPPORTED && driver != D3D_DRIVER_TYPE_WARP) {
    driver = D3D_DRIVER_TYPE_WARP;
    hr = create(nullptr, D3D_DRIVER_TYPE_WARP, nullptr, 0,
                featureLevelArray, 1, D3D11_SDK_VERSION,
                &g_pd3dDevice, &featureLevel, &g_pd3dDeviceContext);
  }
  if (FAILED(hr)) {
    Log("[GUI] layered: D3D11CreateDevice failed: 0x%08X", hr);
    return false;
  }
  Log("[GUI] layered device: %s", driver == D3D_DRIVER_TYPE_WARP ? "WARP (software)" : "hardware");
  if (!OverlayDeviceIsUnwrapped(g_pd3dDevice, g_pd3dDeviceContext)) {
    Log("[GUI] layered device is still wrapped by a graphics proxy; overlay disabled before drawing");
    return false;
  }
  Log("[GUI] layered device/context methods verified: no proxy wrapper");
  RECT rc;
  GetClientRect(hWnd, &rc);
  if (!CreateLayerResources(rc.right - rc.left, rc.bottom - rc.top)) {
    Log("[GUI] layered: offscreen resources failed");
    return false;
  }
  // 分层窗口常驻 WS_EX_LAYERED 是 UpdateLayeredWindow 的前提
  LONG ex = GetWindowLongW(hWnd, GWL_EXSTYLE);
  SetWindowLongW(hWnd, GWL_EXSTYLE, ex | WS_EX_LAYERED);
  g_layeredOverlay = true;
  Log("[GUI] Layered (UpdateLayeredWindow) overlay created %dx%d", g_layerW, g_layerH);
  return true;
}

// 本帧要上传的矩形 = 本帧 ImGui 顶点包围盒 ∪ 上一帧已上传的矩形
// 内容指纹：面板静止时它不变 —— 用来跳过整轮回读+上传（给"mod 多、GPU 挤"的机器省时间）
static unsigned long long ImGuiContentHash() {
  ImDrawData *dd = ImGui::GetDrawData();
  unsigned long long h = 1469598103934665603ull;
  if (!dd)
    return h;
  for (int n = 0; n < dd->CmdListsCount; n++) {
    const ImDrawList *cl = dd->CmdLists[n];
    const unsigned char *p = (const unsigned char *)cl->VtxBuffer.Data;
    size_t words = (size_t)cl->VtxBuffer.Size * sizeof(ImDrawVert) / 8;
    const unsigned long long *q = (const unsigned long long *)p;
    for (size_t i = 0; i < words; i++) {
      h ^= q[i];
      h *= 1099511628211ull;
    }
  }
  return h;
}

// （后者必须并进来，否则"被擦掉"的像素会残留在分层表面上）
static bool LayeredDirtyRect(int &ox, int &oy, int &ow, int &oh) {
  ImDrawData *dd = ImGui::GetDrawData();
  float minx = 1e30f, miny = 1e30f, maxx = -1e30f, maxy = -1e30f;
  if (dd) {
    for (int n = 0; n < dd->CmdListsCount; n++) {
      const ImDrawList *cl = dd->CmdLists[n];
      for (int v = 0; v < cl->VtxBuffer.Size; v++) {
        const ImVec2 &p = cl->VtxBuffer.Data[v].pos;
        if (p.x < minx) minx = p.x;
        if (p.y < miny) miny = p.y;
        if (p.x > maxx) maxx = p.x;
        if (p.y > maxy) maxy = p.y;
      }
    }
  }
  int x0 = 0, y0 = 0, x1 = 0, y1 = 0;
  if (maxx > minx && maxy > miny && minx < 1e29f) {
    x0 = (int)floorf(minx) - 2;
    y0 = (int)floorf(miny) - 2;
    x1 = (int)ceilf(maxx) + 2;
    y1 = (int)ceilf(maxy) + 2;
  }
  if (g_prevDirtyW > 0 && g_prevDirtyH > 0) {
    if (g_prevDirtyX < x0) x0 = g_prevDirtyX;
    if (g_prevDirtyY < y0) y0 = g_prevDirtyY;
    if (g_prevDirtyX + g_prevDirtyW > x1) x1 = g_prevDirtyX + g_prevDirtyW;
    if (g_prevDirtyY + g_prevDirtyH > y1) y1 = g_prevDirtyY + g_prevDirtyH;
  }
  // 裁到窗口范围
  if (x0 < 0) x0 = 0;
  if (y0 < 0) y0 = 0;
  if (x1 > g_layerW) x1 = g_layerW;
  if (y1 > g_layerH) y1 = g_layerH;
  if (x1 - x0 <= 0 || y1 - y0 <= 0) {
    g_prevDirtyW = g_prevDirtyH = 0;
    return false;
  }
  ox = x0;
  oy = y0;
  ow = x1 - x0;
  oh = y1 - y0;
  g_prevDirtyX = ox;
  g_prevDirtyY = oy;
  g_prevDirtyW = ow;
  g_prevDirtyH = oh;
  return true;
}

static void LayeredLogTiming(double copyMs, double mapMs, double ulwMs, int w,
                             int h, int skipped) {
  static int frames = 0, skips = 0;
  static double accC = 0, accM = 0, accU = 0;
  static double maxC = 0, maxM = 0, maxU = 0;
  static ULONGLONG windowStart = 0;
  accC += copyMs;
  accM += mapMs;
  accU += ulwMs;
  if (copyMs > maxC) maxC = copyMs;
  if (mapMs > maxM) maxM = mapMs;
  if (ulwMs > maxU) maxU = ulwMs;
  frames++;
  skips += skipped;
  ULONGLONG now = GetTickCount64();
  if (windowStart == 0) windowStart = now;
  if (now - windowStart >= 1000) {
    double sec = (double)(now - windowStart) / 1000.0;
    Log("[GUI] layered present %.0f fps: rect %dx%d copy avg=%.1f max=%.1f | "
        "map avg=%.1f max=%.1f | ulw avg=%.1f max=%.1f | skipped=%d",
        frames / sec, w, h, accC / frames, maxC, accM / frames, maxM,
        accU / frames, maxU, skips);
    frames = 0;
    skips = 0;
    accC = accM = accU = 0;
    maxC = maxM = maxU = 0;
    windowStart = now;
  }
}

static bool PollLayeredFrame() {
  if (!g_layerReadback.Pending()) return true;
  const LayeredFrame frame = g_layerReadback.Frame();
  const int dx=frame.x, dy=frame.y, dw=frame.width, dh=frame.height;
  LARGE_INTEGER t0, t1, t2, t3;
  QueryPerformanceCounter(&t0);
  TraceGuiStage(5, "poll readback");
  D3D11_MAPPED_SUBRESOURCE map = {};
  HRESULT hr = g_layerReadback.TryMap(g_pd3dDeviceContext, map);
  if (hr == DXGI_ERROR_WAS_STILL_DRAWING) return false;
  if (FAILED(hr)) {
    static HRESULT lastError=S_OK;
    if (hr != lastError) Log("[GUI] layered: readback failed 0x%08X", hr);
    lastError=hr;
    g_layerForcePresent=true;
    return true;
  }
  QueryPerformanceCounter(&t1);
  const size_t srcPitch = (size_t)map.RowPitch;
  const size_t dstPitch = (size_t)g_layerW * 4; // DIB 是整窗宽度
  const size_t rowBytes = (size_t)dw * 4;
  for (int y = 0; y < dh; y++)
    memcpy((char *)g_layerBits + dstPitch * (dy + y) + (size_t)dx * 4,
           (const char *)map.pData + srcPitch * y, rowBytes);
  g_layerReadback.Finish();
  QueryPerformanceCounter(&t2);

  RECT wr;
  GetWindowRect(g_guiHwnd, &wr);
  POINT dst = {wr.left, wr.top};
  SIZE sz = {g_layerW, g_layerH}; // 必须是整窗尺寸：psize 语义是"窗口的新尺寸"
  POINT src = {0, 0};
  BLENDFUNCTION bf = {AC_SRC_OVER, 0, 255, AC_SRC_ALPHA};
  RECT dirty = {dx, dy, dx + dw, dy + dh};
  UPDATELAYEREDWINDOWINFO info = {};
  info.cbSize = sizeof(info);
  info.pptDst = &dst;
  info.psize = &sz;
  info.hdcSrc = g_layerDC;
  info.pptSrc = &src;
  info.crKey = 0;
  info.pblend = &bf;
  info.dwFlags = ULW_ALPHA;
  info.prcDirty = &dirty; // 只更新这块区域（位置/尺寸不受影响）
  TraceGuiStage(6, "upload layered window");
  if (UpdateLayeredWindowIndirect(g_guiHwnd, &info)) {
    // Only a successful upload may suppress a subsequent retry.
    g_layerLastHash = frame.hash;
    g_layerLastX = dx; g_layerLastY = dy;
    g_layerLastW = dw; g_layerLastH = dh;
    g_layerForcePresent = false;
    TraceGuiStage(7, "layered upload complete");
  } else {
    static DWORD lastError=ERROR_SUCCESS;
    DWORD error=GetLastError();
    if (lastError != error) Log("[GUI] layered: upload failed error=%lu", error);
    lastError=error;
    g_layerForcePresent=true;
  }
  QueryPerformanceCounter(&t3);
  LayeredLogTiming(QpcMs(t0, t1), QpcMs(t1, t2), QpcMs(t2, t3), dw, dh, 0);
  return true;
}

static void PresentLayered() {
  if (!g_layerBits || !g_pLayerTex || g_layerReadback.Pending()) return;
  LayeredFrame frame;
  if (!LayeredDirtyRect(frame.x, frame.y, frame.width, frame.height)) return;
  frame.hash = ImGuiContentHash();
  if (!g_layerForcePresent && frame.hash == g_layerLastHash &&
      frame.x == g_layerLastX && frame.y == g_layerLastY &&
      frame.width == g_layerLastW && frame.height == g_layerLastH) {
    LayeredLogTiming(0, 0, 0, frame.width, frame.height, 1);
    return;
  }
  TraceGuiStage(4, "submit readback");
  HRESULT hr = g_layerReadback.Queue(g_pd3dDeviceContext, g_pLayerTex, frame);
  if (FAILED(hr)) {
    static HRESULT lastError=S_OK;
    if (lastError != hr) Log("[GUI] layered: queue failed 0x%08X", hr);
    lastError=hr;
    return;
  }
  PollLayeredFrame();
}

// 覆盖层尺寸跟随游戏窗口后，分层纹理需要同步重建。
static void LayeredSyncSize() {
  RECT rc;
  GetClientRect(g_guiHwnd, &rc);
  int w = rc.right - rc.left, h = rc.bottom - rc.top;
  if (w > 0 && h > 0 && (w != g_layerW || h != g_layerH)) {
    if (!CreateLayerResources(w, h))
      Log("[GUI] layered: resize to %dx%d failed", w, h);
    g_layerForcePresent = true; // 资源重建过：下一帧必须重画/重传
  }
}

#endif
