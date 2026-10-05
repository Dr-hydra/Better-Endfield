#pragma once

#include <d3d11.h>
#include <dxgi1_2.h>
#include <dwmapi.h>
#include <dcomp.h>
#pragma comment(lib, "d3d11.lib")
#pragma comment(lib, "dxgi.lib")
#pragma comment(lib, "dwmapi.lib")
#pragma comment(lib, "dcomp.lib")

#include "imgui.h"
#include "imgui_impl_win32.h"
#include "imgui_impl_dx11.h"
#include "direct_vmd_source_gui.h"

extern IMGUI_IMPL_API LRESULT ImGui_ImplWin32_WndProcHandler(
    HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

#define WM_MMD_GUI_PLAY   (WM_USER + 100)
#define WM_MMD_GUI_PAUSE  (WM_USER + 101)
#define WM_MMD_GUI_STOP   (WM_USER + 102)
#define WM_MMD_GUI_LOAD   (WM_USER + 103)
#define WM_MMD_GUI_RECAPTURE (WM_USER + 106)
#define WM_MMD_GUI_LOAD_AUDIO (WM_USER + 107)
#define WM_MMD_GUI_SEEK_AUDIO (WM_USER + 108)
#define WM_MMD_GUI_SET_VOLUME (WM_USER + 109)
#define WM_MMD_GUI_UNLOAD_ANIM   (WM_USER + 110)
#define WM_MMD_GUI_UNLOAD_CAMERA (WM_USER + 111)
#define WM_MMD_GUI_UNLOAD_FOOTIK (WM_USER + 112)
#define WM_MMD_GUI_UNLOAD_MORPH  (WM_USER + 113)
#define WM_MMD_GUI_UNLOAD_AUDIO  (WM_USER + 114)
#define WM_MMD_GUI_LOAD_CAMERA   (WM_USER + 115)
#define WM_MMD_GUI_LOAD_FOOTIK   (WM_USER + 116)
#define WM_MMD_GUI_LOAD_MORPH    (WM_USER + 117)
#define WM_MMD_GUI_PHASE0_GHOST  (WM_USER + 118)
#define WM_MMD_GUI_DIRECT_CONTROL (WM_USER + 119)
#define WM_MMD_GUI_SELECT_MODE (WM_USER + 120)

static const char *GuiFileName(const char *path) {
  if (!path || !path[0])
    return "";
  const char *slash = strrchr(path, '\\');
  const char *forwardSlash = strrchr(path, '/');
  if (!slash || (forwardSlash && forwardSlash > slash))
    slash = forwardSlash;
  return slash ? slash + 1 : path;
}




typedef void* (*tCreateBindingByActionId)(void* __this, void* actionId,
                                          void* callback, int priority,
                                          void* methodInfo);
static tCreateBindingByActionId g_origCreateBinding = nullptr;


static void ResolveActionInvoke(void* actionObj) {
  if (g_actionInvokeMethod || !actionObj) return;
  void* klass = il2cpp_object_get_class(actionObj);
  if (!klass) return;
  void* it = nullptr;
  void* m;
  while ((m = il2cpp_class_get_methods(klass, &it))) {
    if (strcmp(il2cpp_method_get_name(m), "Invoke") == 0 &&
        il2cpp_method_get_param_count(m) == 0) {
      g_actionInvokeMethod = m;
      Log("[CURSOR] Resolved Action.Invoke()");
      return;
    }
  }
}

static void InvokeAction(void* actionObj) {
  if (!actionObj) return;
  if (!g_actionInvokeMethod) ResolveActionInvoke(actionObj);
  if (!g_actionInvokeMethod) return;
  __try {
    void* exc = nullptr;
    il2cpp_runtime_invoke(g_actionInvokeMethod, actionObj, nullptr, &exc);
  } __except (1) {
    Log("[CURSOR] SEH in InvokeAction (delegate may be GC'd)");
  }
}

static void* hkCreateBindingByActionId(void* __this, void* actionId,
                                        void* callback, int priority,
                                        void* methodInfo) {
  void* result = g_origCreateBinding(__this, actionId, callback, priority,
                                      methodInfo);
  char buf[256];
  if (ReadStr(actionId, buf, sizeof(buf)) > 0) {
    if (strcmp(buf, "common_show_cursor_start") == 0) {
      g_cursorShowAction = callback;
      if (il2cpp_gchandle_new) il2cpp_gchandle_new(callback, true);
      if (!g_actionInvokeMethod) ResolveActionInvoke(callback);
      Log("[CURSOR] CAPTURED show-cursor action: %p (invoke=%p)", callback,
          g_actionInvokeMethod);
    } else if (strcmp(buf, "common_show_cursor_end") == 0) {
      g_cursorHideAction = callback;
      if (il2cpp_gchandle_new) il2cpp_gchandle_new(callback, true);
      Log("[CURSOR] CAPTURED hide-cursor action: %p", callback);
    }
  }
  return result;
}

static void DumpCursorMethods() {
  if (!il2cpp_domain_get || !il2cpp_domain_get_assemblies) return;

  void *domain = il2cpp_domain_get();
  size_t asmCount = 0;
  void **assemblies = il2cpp_domain_get_assemblies(domain, &asmCount);

  for (size_t i = 0; i < asmCount; i++) {
    void *img = il2cpp_assembly_get_image(assemblies[i]);
    if (!img) continue;
    void *klass = il2cpp_class_from_name(img, "Beyond.Input", "InputManager");
    if (!klass) continue;

    Log("[CURSOR] Found Beyond.Input.InputManager");

    void *it = nullptr, *m;
    while ((m = il2cpp_class_get_methods(klass, &it))) {
      const char *mn = il2cpp_method_get_name(m);
      if (!mn) continue;
      uint32_t pc = il2cpp_method_get_param_count(m);

      if (strcmp(mn, "CreateBindingByActionId") == 0 && pc == 3) {
        void *mp = ((MInfo *)m)->mp;
        if (mp && MH_CreateHook(mp, (void *)hkCreateBindingByActionId,
                                (void **)&g_origCreateBinding) == MH_OK) {
          MH_EnableHook(mp);
          Log("[CURSOR] Hooked CreateBindingByActionId: %p", mp);
        }
      }
    }
    break;
  }
}

#define WM_MMD_CURSOR_SHOW  (WM_USER + 104)
#define WM_MMD_CURSOR_HIDE  (WM_USER + 105)

static void ReleaseCursorToGui() {
  if (g_gameHwnd && g_cursorShowAction)
    PostMessageW(g_gameHwnd, WM_MMD_CURSOR_SHOW, 0, 0);
}

static void ReturnCursorToGame() {
  if (g_gameHwnd && g_cursorHideAction)
    PostMessageW(g_gameHwnd, WM_MMD_CURSOR_HIDE, 0, 0);
}


static ID3D11Device *g_pd3dDevice = nullptr;
static ID3D11DeviceContext *g_pd3dDeviceContext = nullptr;
static IDXGISwapChain1 *g_pSwapChain = nullptr;
static ID3D11RenderTargetView *g_pMainRenderTargetView = nullptr;
static IDCompositionDevice *g_pDCompDevice = nullptr;
static IDCompositionTarget *g_pDCompTarget = nullptr;
static IDCompositionVisual *g_pDCompVisual = nullptr;

static void CreateRenderTarget() {
  ID3D11Texture2D *pBackBuffer = nullptr;
  g_pSwapChain->GetBuffer(0, IID_PPV_ARGS(&pBackBuffer));
  if (pBackBuffer) {
    g_pd3dDevice->CreateRenderTargetView(pBackBuffer, nullptr,
                                          &g_pMainRenderTargetView);
    pBackBuffer->Release();
  }
}

static void CleanupRenderTarget() {
  if (g_pMainRenderTargetView) {
    g_pMainRenderTargetView->Release();
    g_pMainRenderTargetView = nullptr;
  }
}

static bool CreateDeviceD3D(HWND hWnd) {
  UINT createDeviceFlags = 0;
  D3D_FEATURE_LEVEL featureLevel;
  const D3D_FEATURE_LEVEL featureLevelArray[] = {D3D_FEATURE_LEVEL_11_0};
  HRESULT hr = D3D11CreateDevice(
      nullptr, D3D_DRIVER_TYPE_HARDWARE, nullptr, createDeviceFlags,
      featureLevelArray, 1, D3D11_SDK_VERSION,
      &g_pd3dDevice, &featureLevel, &g_pd3dDeviceContext);
  if (hr == DXGI_ERROR_UNSUPPORTED) {
    hr = D3D11CreateDevice(
        nullptr, D3D_DRIVER_TYPE_WARP, nullptr, createDeviceFlags,
        featureLevelArray, 1, D3D11_SDK_VERSION,
        &g_pd3dDevice, &featureLevel, &g_pd3dDeviceContext);
  }
  if (FAILED(hr)) return false;

  IDXGIDevice *pDxgiDevice = nullptr;
  g_pd3dDevice->QueryInterface(IID_PPV_ARGS(&pDxgiDevice));
  IDXGIAdapter *pAdapter = nullptr;
  pDxgiDevice->GetAdapter(&pAdapter);
  IDXGIFactory2 *pFactory = nullptr;
  pAdapter->GetParent(IID_PPV_ARGS(&pFactory));

  RECT rc;
  GetClientRect(hWnd, &rc);
  DXGI_SWAP_CHAIN_DESC1 sd = {};
  sd.Width = rc.right - rc.left;
  sd.Height = rc.bottom - rc.top;
  sd.Format = DXGI_FORMAT_B8G8R8A8_UNORM;
  sd.SampleDesc.Count = 1;
  sd.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
  sd.BufferCount = 2;
  sd.SwapEffect = DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL;
  sd.AlphaMode = DXGI_ALPHA_MODE_PREMULTIPLIED;
  hr = pFactory->CreateSwapChainForComposition(g_pd3dDevice, &sd, nullptr,
                                                &g_pSwapChain);
  pFactory->Release();
  pAdapter->Release();

  if (FAILED(hr)) {
    Log("[GUI] CreateSwapChainForComposition failed: 0x%08X", hr);
    pDxgiDevice->Release();
    return false;
  }

  hr = DCompositionCreateDevice(pDxgiDevice, IID_PPV_ARGS(&g_pDCompDevice));
  pDxgiDevice->Release();
  if (FAILED(hr)) {
    Log("[GUI] DCompositionCreateDevice failed: 0x%08X", hr);
    return false;
  }
  g_pDCompDevice->CreateTargetForHwnd(hWnd, TRUE, &g_pDCompTarget);
  g_pDCompDevice->CreateVisual(&g_pDCompVisual);

  if (g_pSwapChain) {
    __try {
      void *vtable = *(void **)g_pSwapChain;
      HMODULE hModule = NULL;
      if (GetModuleHandleExA(
              GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                  GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
              (LPCSTR)vtable, &hModule)) {
        char moduleName[MAX_PATH] = {};
        GetModuleFileNameA(hModule, moduleName, MAX_PATH);
        for (int i = 0; moduleName[i]; i++)
          moduleName[i] = (char)tolower((unsigned char)moduleName[i]);
        if (strstr(moduleName, "d3d11.dll")) {
          Log("[GUI] DETECTED 3DMigoto/SSMT wrapper on SwapChain!");
          IDXGISwapChain1 *candidate =
              *(IDXGISwapChain1 **)((char *)g_pSwapChain + 8);
          IDXGISwapChain1 *verified = nullptr;
          if (candidate &&
              SUCCEEDED(candidate->QueryInterface(IID_PPV_ARGS(&verified)))) {
            Log("[GUI] Unwrapped real swap chain: %p", verified);
            g_pSwapChain->Release();
            g_pSwapChain = verified;
          } else {
            Log("[GUI] WARN: +8 offset is not a valid IDXGISwapChain1, "
                "skipping unwrap");
          }
        }
      }
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      Log("[GUI] WARN: Exception during 3DMigoto detection, skipping");
    }
  }

  g_pDCompVisual->SetContent(g_pSwapChain);
  g_pDCompTarget->SetRoot(g_pDCompVisual);
  g_pDCompDevice->Commit();

  CreateRenderTarget();
  Log("[GUI] DComp transparent swap chain created");
  return true;
}

static void CleanupDeviceD3D() {
  CleanupRenderTarget();
  if (g_pDCompVisual) { g_pDCompVisual->Release(); g_pDCompVisual = nullptr; }
  if (g_pDCompTarget) { g_pDCompTarget->Release(); g_pDCompTarget = nullptr; }
  if (g_pDCompDevice) { g_pDCompDevice->Release(); g_pDCompDevice = nullptr; }
  if (g_pSwapChain) { g_pSwapChain->Release(); g_pSwapChain = nullptr; }
  if (g_pd3dDeviceContext) { g_pd3dDeviceContext->Release(); g_pd3dDeviceContext = nullptr; }
  if (g_pd3dDevice) { g_pd3dDevice->Release(); g_pd3dDevice = nullptr; }
}

static LRESULT CALLBACK GuiWndProc(HWND hWnd, UINT msg, WPARAM wParam,
                                    LPARAM lParam) {
  if (ImGui_ImplWin32_WndProcHandler(hWnd, msg, wParam, lParam))
    return true;

  switch (msg) {
  case WM_MOUSEACTIVATE:
    return MA_ACTIVATE;
  case WM_SIZE:
    if (g_pd3dDevice && wParam != SIZE_MINIMIZED) {
      CleanupRenderTarget();
      g_pSwapChain->ResizeBuffers(0, (UINT)LOWORD(lParam),
                                   (UINT)HIWORD(lParam), DXGI_FORMAT_UNKNOWN,
                                   0);
      CreateRenderTarget();
    }
    return 0;
  case WM_MOVING:
    if (g_gameHwnd) {
      RECT gr;
      GetWindowRect(g_gameHwnd, &gr);
      RECT *pr = (RECT *)lParam;
      int pw = pr->right - pr->left;
      int ph = pr->bottom - pr->top;
      if (pr->left < gr.left) { pr->left = gr.left; pr->right = pr->left + pw; }
      if (pr->top < gr.top) { pr->top = gr.top; pr->bottom = pr->top + ph; }
      if (pr->right > gr.right) { pr->right = gr.right; pr->left = pr->right - pw; }
      if (pr->bottom > gr.bottom) { pr->bottom = gr.bottom; pr->top = pr->bottom - ph; }
    }
    return TRUE;
  case WM_CLOSE:
    ShowWindow(hWnd, SW_HIDE);
    g_guiVisible = false;
    return 0;
  case WM_DESTROY:
    return 0;
  }
  return DefWindowProcW(hWnd, msg, wParam, lParam);
}

static void DrawMainPanel() {
  ImGui::SetNextWindowPos(ImVec2(0, 0));
  ImGui::SetNextWindowSize(ImGui::GetIO().DisplaySize);
  ImGui::PushStyleVar(ImGuiStyleVar_WindowPadding, ImVec2(0, 0));
  ImGui::Begin("EIEM", nullptr,
               ImGuiWindowFlags_NoTitleBar | ImGuiWindowFlags_NoResize |
                   ImGuiWindowFlags_NoMove | ImGuiWindowFlags_NoCollapse |
                   ImGuiWindowFlags_NoScrollbar);
  ImGui::PopStyleVar();

  const float titleH = 36.0f;
  const float btnSize = 22.0f;
  ImDrawList *dl = ImGui::GetWindowDrawList();
  ImVec2 winPos = ImGui::GetWindowPos();
  ImVec2 winSize = ImGui::GetWindowSize();

  dl->AddRectFilled(winPos, ImVec2(winPos.x + winSize.x, winPos.y + titleH),
                    IM_COL32(24, 24, 24, 180));
  dl->AddRectFilled(winPos, ImVec2(winPos.x + 4, winPos.y + titleH),
                    IM_COL32(255, 218, 0, 255));
  dl->AddLine(ImVec2(winPos.x, winPos.y + titleH),
              ImVec2(winPos.x + winSize.x, winPos.y + titleH),
              IM_COL32(100, 100, 105, 80), 1.0f);

  dl->AddText(ImVec2(winPos.x + 14, winPos.y + 9), IM_COL32(255, 218, 0, 255),
              "EIEM");
  if (!g_updateAvailable) {
    char verBuf[64];
    snprintf(verBuf, sizeof(verBuf), "v%s", EIEM_VERSION);
    dl->AddText(ImVec2(winPos.x + 52, winPos.y + 9),
                IM_COL32(100, 100, 105, 200), verBuf);
    dl->AddText(ImVec2(winPos.x + 130, winPos.y + 9),
                IM_COL32(80, 80, 85, 120), "Priestess is watching you");
  }
  if (g_updateAvailable) {
    float hue = fmodf((float)GetTickCount() / 1500.0f, 1.0f);
    float s = 0.9f, l = 0.6f;
    auto hue2rgb = [](float p, float q, float t) -> float {
      if (t < 0.0f) t += 1.0f;
      if (t > 1.0f) t -= 1.0f;
      if (t < 1.0f/6.0f) return p + (q - p) * 6.0f * t;
      if (t < 1.0f/2.0f) return q;
      if (t < 2.0f/3.0f) return p + (q - p) * (2.0f/3.0f - t) * 6.0f;
      return p;
    };
    float q = l < 0.5f ? l * (1.0f + s) : l + s - l * s;
    float p = 2.0f * l - q;
    float r = hue2rgb(p, q, hue + 1.0f/3.0f);
    float g = hue2rgb(p, q, hue);
    float b = hue2rgb(p, q, hue - 1.0f/3.0f);
    ImU32 rainbowCol = IM_COL32((int)(r*255), (int)(g*255), (int)(b*255), 255);

    char upgBuf[64];
    snprintf(upgBuf, sizeof(upgBuf), "v%s -> v%s", EIEM_VERSION, g_latestVersion);
    dl->AddText(ImVec2(winPos.x + 52, winPos.y + 9), rainbowCol, upgBuf);
  }

  ImGui::SetCursorPos(ImVec2(0, 0));
  ImGui::InvisibleButton("##titlebar_drag",
                         ImVec2(winSize.x - btnSize - 16, titleH));
  static bool s_dragging = false;
  static POINT s_dragAnchor = {0, 0};
  static RECT s_winAtDrag = {0, 0, 0, 0};
  if (ImGui::IsItemActivated()) {
    s_dragging = true;
    GetCursorPos(&s_dragAnchor);
    GetWindowRect(g_guiHwnd, &s_winAtDrag);
  }
  if (s_dragging && ImGui::IsMouseDown(ImGuiMouseButton_Left)) {
    POINT cur;
    GetCursorPos(&cur);
    int nx = s_winAtDrag.left + (cur.x - s_dragAnchor.x);
    int ny = s_winAtDrag.top + (cur.y - s_dragAnchor.y);
    if (g_gameHwnd) {
      RECT gr;
      GetWindowRect(g_gameHwnd, &gr);
      int w = s_winAtDrag.right - s_winAtDrag.left;
      int h = s_winAtDrag.bottom - s_winAtDrag.top;
      if (nx < gr.left) nx = gr.left;
      if (ny < gr.top) ny = gr.top;
      if (nx + w > gr.right) nx = gr.right - w;
      if (ny + h > gr.bottom) ny = gr.bottom - h;
    }
    SetWindowPos(g_guiHwnd, nullptr, nx, ny, 0, 0,
                 SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE);
  } else {
    s_dragging = false;
  }

  ImVec2 closePos = ImVec2(winSize.x - btnSize - 8, (titleH - btnSize) * 0.5f);
  ImGui::SetCursorPos(closePos);
  ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, btnSize * 0.5f);
  ImGui::PushStyleColor(ImGuiCol_Button, IM_COL32(55, 58, 70, 255));
  ImGui::PushStyleColor(ImGuiCol_ButtonHovered, IM_COL32(210, 60, 60, 255));
  ImGui::PushStyleColor(ImGuiCol_ButtonActive, IM_COL32(160, 35, 35, 255));
  bool closeClicked = ImGui::Button("##close", ImVec2(btnSize, btnSize));
  ImVec2 bMin = ImGui::GetItemRectMin();
  ImVec2 bMax = ImGui::GetItemRectMax();
  ImVec2 c = ImVec2((bMin.x + bMax.x) * 0.5f, (bMin.y + bMax.y) * 0.5f);
  float r = 5.0f;
  ImU32 xcol = IM_COL32(220, 220, 230, 255);
  dl->AddLine(ImVec2(c.x - r, c.y - r), ImVec2(c.x + r, c.y + r), xcol, 1.6f);
  dl->AddLine(ImVec2(c.x - r, c.y + r), ImVec2(c.x + r, c.y - r), xcol, 1.6f);
  ImGui::PopStyleColor(3);
  ImGui::PopStyleVar();
  if (closeClicked) {
    ShowWindow(g_guiHwnd, SW_HIDE);
    g_guiVisible = false;
    ReturnCursorToGame();
  }

  {
    ImVec2 gradTop = ImVec2(winPos.x, winPos.y + titleH);
    ImVec2 gradBot = ImVec2(winPos.x + winSize.x, winPos.y + winSize.y);
    ImU32 colTop = IM_COL32(255, 255, 255, 0);
    ImU32 colBot = IM_COL32(255, 255, 255, 65);
    dl->AddRectFilledMultiColor(gradTop, gradBot,
                                colTop, colTop,
                                colBot, colBot);
  }

  ImGui::SetCursorPos(ImVec2(10, titleH + 8));
  ImGui::BeginChild("##body", ImVec2(winSize.x - 20, winSize.y - titleH - 16),
                    false);

  ImGui::BeginTabBar("##tabs", ImGuiTabBarFlags_None);

  ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
  bool tabCtrl = ImGui::BeginTabItem(u8"\u63a7\u5236");
  ImGui::PopStyleColor();
  if (tabCtrl) {

  ImGui::Spacing();
  bool isPlaying = false;
  bool isPaused = false;
  bool isEnded = false;
  float curTime = 0.0f;
  float totalTime = 0.0f;
  int curFrame = 0;
  int totalFrames = 0;
  bool animLoaded = false;
  MotionBackend selectedMotionBackend = GuiSelectedMotionBackend();
  bool directModeSelected =
      selectedMotionBackend == MotionBackend::DirectVmd;
  bool directBackendActive =
      g_motionBackend.Is(MotionBackend::DirectVmd);

  if (directModeSelected) {
    animLoaded = DirectVmdRuntime_IsLoaded();
    const DirectVmdPlaybackState directPlayback =
        DirectVmdRuntime_PublicPlayback();
    const double directFrame = DirectVmdRuntime_PublicFrame();
    const double directDuration =
        DirectVmdRuntime_PublicDurationFrames();
    curFrame = static_cast<int>(directFrame);
    totalFrames = static_cast<int>(directDuration);
    curTime = static_cast<float>(directFrame / kVmdFramesPerSecond);
    totalTime =
        static_cast<float>(directDuration / kVmdFramesPerSecond);
    isPlaying = directPlayback == DirectVmdPlaybackState::Playing;
    isPaused = directPlayback == DirectVmdPlaybackState::Paused;
    isEnded = directPlayback == DirectVmdPlaybackState::Ended;
  } else {
    if (g_muscleAnim && g_muscleAnim->loaded) {
      animLoaded = true;
      totalTime = g_muscleAnim->Duration();
      totalFrames = g_muscleAnim->frameCount;
    }
    if (g_musclePlayer) {
      isPlaying = g_musclePlayer->playing;
      curTime = g_musclePlayer->currentTime;
      curFrame = (int)(curTime * 30.0f);
      isPaused = !isPlaying && curTime > 0.0f &&
                 !g_musclePlayer->ended;
      isEnded = g_musclePlayer->ended;
    }
  }

  const bool clothHeld=g_clothPlaybackGate.Holding(uint32_t(selectedMotionBackend),g_motionBackend.Generation());
  const bool clothFailed=clothHeld && g_clothPlaybackGate.State()==eiem_playback::Preparation::Failed;
  if (clothFailed) {
    ImGui::TextColored(ImVec4(0.85f,0.25f,0.15f,1.f),u8"\u670d\u88c5\u51c6\u5907\u5931\u8d25\uff0c\u8bf7\u505c\u6b62\u540e\u91cd\u8bd5");
  } else if (clothHeld) {
    ImGui::TextColored(ImVec4(0.80f,0.55f,0.f,1.f),
        isPlaying?u8"\u6b63\u5728\u51c6\u5907\u670d\u88c5\uff0c\u5b8c\u6210\u540e\u5f00\u59cb\u64ad\u653e":u8"\u6b63\u5728\u51c6\u5907\u670d\u88c5\uff08\u64ad\u653e\u5df2\u6682\u505c\uff09");
  } else if (isPlaying) {
    ImGui::TextColored(ImVec4(0.10f, 0.55f, 0.25f, 1.0f), u8"\u64ad\u653e\u4e2d");
  } else if (isPaused) {
    ImGui::TextColored(ImVec4(0.80f, 0.55f, 0.00f, 1.0f), u8"\u5df2\u6682\u505c");
  } else if (isEnded) {
    ImGui::TextColored(ImVec4(0.45f, 0.45f, 0.48f, 1.0f),
                       u8"\u5df2\u7ed3\u675f");
  } else {
    ImGui::TextColored(ImVec4(0.45f, 0.45f, 0.48f, 1.0f), u8"\u5df2\u505c\u6b62");
  }

  ImGui::Text(u8"\u65f6\u95f4: %.1f / %.1f \u79d2", curTime, totalTime);
  ImGui::Text(u8"\u5e27: %d / %d", curFrame, totalFrames);

  if (animLoaded && totalTime > 0.0f &&
      (directModeSelected || g_musclePlayer)) {
    float seekTime = curTime;
    ImGui::SetNextItemWidth(-1);
    static bool s_wasDragging = false;
    static float s_dragTarget = 0.0f;
    ImGui::BeginDisabled(clothHeld);
    if (ImGui::SliderFloat("##seek", &seekTime, 0.0f, totalTime, u8"%.1f \u79d2")) {
      if (directModeSelected) {
        DirectVmdRuntime_RequestSeekFrame(
            static_cast<double>(seekTime) * kVmdFramesPerSecond);
      } else {
        g_musclePlayer->currentTime = seekTime;
        QueryPerformanceCounter(&g_musclePlayer->lastTick);
      }
      s_wasDragging = true;
      s_dragTarget = seekTime;
    }
    ImGui::EndDisabled();
    if (s_wasDragging && !ImGui::IsItemActive()) {
      s_wasDragging = false;
      if (g_audioPlayer && g_audioPlayer->loaded && g_audioEnabled) {
        int ms = (int)(s_dragTarget * 1000.0f);
        Log("[AUDIO] Seek bar released -> posting seek %d ms", ms);
        PostMessageW(g_gameHwnd, WM_MMD_GUI_SEEK_AUDIO, (WPARAM)ms, 0);
      }
    }
  } else {
    float zero = 0.0f;
    ImGui::ProgressBar(zero, ImVec2(-1, 0), "");
  }

  ImGui::Spacing();
  ImGui::Separator();
  ImGui::Spacing();

  ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 14.0f);
  ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
  float btnW = 76.0f;

  const bool hasSelectedAction = directModeSelected
                                     ? g_directVmdPath[0] != '\0'
                                     : g_muscleAnimPath[0] != '\0';
  const bool canPlay = hasSelectedAction &&
                       (!directModeSelected || animLoaded);
  if (isPlaying || !canPlay) {
    ImGui::BeginDisabled();
    ImGui::Button(u8"\u64ad\u653e", ImVec2(btnW, 0));
    ImGui::EndDisabled();
  } else {
    if (ImGui::Button(u8"\u64ad\u653e", ImVec2(btnW, 0))) {
      PostMessageW(g_gameHwnd, WM_MMD_GUI_PLAY, 0, 0);
    }
  }
  ImGui::SameLine();

  if (!isPlaying) {
    ImGui::BeginDisabled();
    ImGui::Button(u8"\u6682\u505c", ImVec2(btnW, 0));
    ImGui::EndDisabled();
  } else {
    if (ImGui::Button(u8"\u6682\u505c", ImVec2(btnW, 0))) {
      PostMessageW(g_gameHwnd, WM_MMD_GUI_PAUSE, 0, 0);
    }
  }
  ImGui::SameLine();

  if (g_motionBackend.Is(MotionBackend::Native) && !isPaused && !isEnded) {
    ImGui::BeginDisabled();
    ImGui::Button(u8"\u505c\u6b62", ImVec2(btnW, 0));
    ImGui::EndDisabled();
  } else {
    if (ImGui::Button(u8"\u505c\u6b62", ImVec2(btnW, 0))) {
      PostMessageW(g_gameHwnd, WM_MMD_GUI_STOP, 0, 0);
    }
  }
  ImGui::PopStyleColor();
  ImGui::PopStyleVar();

  ImGui::Spacing();

  if (!directModeSelected && g_musclePlayer) {
    g_playbackSpeed = g_musclePlayer->speed;
    g_playbackLoop  = g_musclePlayer->loop;
  }
  ImGui::SetNextItemWidth(140);
  ImGui::SliderFloat(u8"\u64ad\u653e\u901f\u5ea6", &g_playbackSpeed, 0.1f, 3.0f, u8"%.1fx");
  ImGui::SameLine();
  ImGui::Checkbox(u8"\u5faa\u73af", &g_playbackLoop);
  ImGui::SameLine();
  ImGui::Checkbox(u8"\u955c\u5934", &g_cameraEnabled);

  if (directModeSelected) {
    bool directTerrainEnabled = GhostRig_IsTerrainEnabledForGui();
    if (ImGui::Checkbox(
            u8"VMD\u5730\u5f62\u8ddf\u968f (\u8d70\u697c\u68af/\u8d34\u5730)",
            &directTerrainEnabled)) {
      GhostRig_SetTerrainEnabled(directTerrainEnabled);
    }
    if (ImGui::IsItemHovered()) {
      ImGui::SetTooltip(
          u8"\u4ec5\u540e\u5904\u7406\u6700\u7ec8\u811a\u90e8\u76ee\u6807\u548c Root \u8865\u507f\uff0c"
          u8"\u4e0d\u4fee\u6539 VMD \u6837\u672c\u6216\u5e7d\u7075\u9aa8\u67b6\u3002");
    }
  } else {
    ImGui::Checkbox(
        u8"\u539f\u751f\u4e0b\u534a\u8eabIK (\u8d70\u697c\u68af/\u8d34\u5730\u8ddf\u968f)",
        &g_footIKEnabled);
    if (ImGui::IsItemHovered()) {
      ImGui::SetTooltip(
          u8"\u52fe\u9009: \u4f7f\u7528 VMD \u811a\u90e8IK + FinalIK + FindFloor\n"
          u8"\u53d6\u6d88: \u808c\u8089\u6570\u636e\u76f4\u63a5\u63a7\u5236\u4e0b\u534a\u8eab");
    }
  }
  if (directModeSelected) {
    DirectVmdRuntime_SetSpeed(g_playbackSpeed);
    DirectVmdRuntime_SetLoop(g_playbackLoop);
  } else if (g_musclePlayer) {
    g_musclePlayer->speed = g_playbackSpeed;
    g_musclePlayer->loop  = g_playbackLoop;
  }

  ImGui::SetNextItemWidth(-1);
  ImGui::SliderFloat(u8"\u97f3\u9891\u504f\u79fb##offset", &g_audioOffset, -10.0f, 10.0f, u8"%.1f \u79d2");
  ImGui::TextDisabled(u8"\u6b63 = \u8df3\u8fc7\u97f3\u9891\u524d\u594f\uff0c\u8d1f = \u5ef6\u8fdf\u97f3\u9891");
  {
    int volPercent = g_audioVolume / 10;
    ImGui::SetNextItemWidth(-1);
    if (ImGui::SliderInt(u8"\u97f3\u91cf##vol", &volPercent, 0, 100, u8"%d%%")) {
      g_audioVolume = volPercent * 10;
      PostMessageW(g_gameHwnd, WM_MMD_GUI_SET_VOLUME, (WPARAM)g_audioVolume, 0);
    }
  }

  ImGui::Spacing();
  ImGui::Separator();
  ImGui::Spacing();

  ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f), u8"\u52a8\u753b\u4fe1\u606f");
  if (animLoaded) {
    ImGui::Text(u8"\u6587\u4ef6: %s",
                GuiFileName(directModeSelected ? g_directVmdPath
                                               : g_muscleAnimPath));
    if (directModeSelected) {
      ImGui::Text(u8"\u5e27: %d | FPS: 30 | clip generation %llu",
                  totalFrames,
                  (unsigned long long)
                      DirectVmdRuntime_PublicClipGeneration());
    } else {
      ImGui::Text(u8"Muscles: %d", g_muscleAnim->muscleCount);
      ImGui::Text(u8"FPS: %.0f", g_muscleAnim->fps);
      if (g_muscleAnim->hasFingerBones)
        ImGui::Text(u8"\u624b\u6307\u9aa8\u9abc: %d",
                    g_muscleAnim->fingerBoneCount);
      else
        ImGui::TextDisabled(u8"\u624b\u6307\u9aa8\u9abc: \u65e0");
    }
  } else {
    const bool hasPath = directModeSelected ? g_directVmdPath[0] != '\0'
                                            : g_muscleAnimPath[0] != '\0';
    if (!hasPath)
      ImGui::TextDisabled(u8"\u672a\u9009\u62e9\u52a8\u4f5c\u6587\u4ef6");
    else if (directModeSelected)
      ImGui::TextDisabled(u8"\u6b63\u5728\u540e\u53f0\u52a0\u8f7d DirectVmd...");
    else
      ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d (\u64ad\u653e\u65f6\u81ea\u52a8\u52a0\u8f7d)");
  }

  ImGui::Spacing();
  ImGui::Separator();
  ImGui::Spacing();

  ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
  const bool clothingOpen = ImGui::CollapsingHeader(u8"服装碰撞");
  ImGui::PopStyleColor();
  if (clothingOpen) {
    s_collisionInspect.store(true, std::memory_order_release);
    const auto ui = CollisionGetUi();
    bool enabled = s_clothAutoEnabled.load(std::memory_order_acquire);
    if (ImGui::Checkbox(u8"服装碰撞增强", &enabled))
      ClothBoneQueueCommand(ui.session, !enabled);
    ImGui::TextWrapped(u8"自动匹配当前服装，可在播放前或播放中开关；停止播放后保留设置。");
    const int applied = ui.authoredApplied + ui.autoConnectionsApplied + ui.autoSkinApplied + ui.autoPartialApplied;
    if (ui.boneRestoring)
      ImGui::TextUnformatted(enabled ? u8"正在恢复原配置，完成后重新启用。" : u8"正在恢复原配置…");
    else if (!enabled)
      ImGui::TextUnformatted(u8"已关闭。");
    else if (!ui.session)
      ImGui::TextUnformatted(u8"已开启，将在播放时自动应用。");
    else if (ui.failed)
      ImGui::TextWrapped(u8"本次未能应用增强，详见诊断信息。");
    else if (ui.autoPreparing)
      ImGui::TextUnformatted(u8"正在准备当前服装…");
    else if (applied)
      ImGui::Text(u8"已增强 %d 个服装部件。", applied);
    else if (!ui.boneBusy)
      ImGui::TextUnformatted(u8"暂无适用增强，保持原布料效果。");
    else
      ImGui::TextUnformatted(u8"正在确认服装状态…");

    int geometry = s_collisionGeometry.load(std::memory_order_acquire);
    if (ImGui::Combo(u8"原生碰撞体策略", &geometry, u8"半径补偿\0原生原值\0")) {
      s_collisionGeometry.store(geometry, std::memory_order_release);
      s_skirtDirty.store(true, std::memory_order_release);
    }
    if (geometry == 0) {
      float radius = s_skirtHipRadiusDelta.load(std::memory_order_acquire);
      if (ImGui::SliderFloat(u8"大腿胶囊半径补偿", &radius, 0.0f, 0.4f, "%.3f")) {
        s_skirtHipRadiusDelta.store(radius, std::memory_order_release);
        s_skirtDirty.store(true, std::memory_order_release);
      }
      ImGui::TextWrapped(u8"半径补偿可能撑开服装；通常使用原生原值即可。");
    }
    if (ImGui::TreeNode(u8"诊断信息")) {
      if (ui.failed) ImGui::TextWrapped("%s", ui.issue);
      for (int n=0; n<std::clamp(ui.boneCount,0,8); ++n) {
        if (!ui.boneNames[n][0]) continue;
        ImGui::Text(u8"%s：%s", ui.boneNames[n], ui.boneApplied[n] ? u8"已应用" : u8"未应用 / 正在恢复");
        ImGui::TextWrapped("%s", ui.boneIssues[n]);
      }
      if (ui.autoPreserved) ImGui::Text(u8"保持原模拟的部件：%d", ui.autoPreserved);
      ImGui::BeginDisabled(!ui.session || ui.failed);
      if (ImGui::Button(u8"导出服装碰撞快照")) {
        CollisionQueueExport(ui.session);
        if (g_gameHwnd) PostMessage(g_gameHwnd, WM_NULL, 0, 0);
      }
      ImGui::EndDisabled();
      const int exportStatus = s_collisionExportStatus.load(std::memory_order_acquire);
      if (exportStatus == 2) ImGui::TextUnformatted(u8"等待当前帧快照…");
      else if (exportStatus != 0) {
        const auto note = CollisionGetExportNote();
        if (exportStatus == 1) ImGui::TextWrapped(u8"已导出：%s", note.path);
        else ImGui::TextWrapped(u8"导出失败：%s（%lu）", note.phase, note.error);
      }
      ImGui::TreePop();
    }
  }
  ImGui::Spacing();
  ImGui::Separator();
  ImGui::Spacing();

  ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f), u8"\u7cfb\u7edf\u72b6\u6001");
  ImGui::Text(u8"\u5f53\u524d\u6a21\u5f0f: %s | \u8fd0\u884c\u540e\u7aef: %s",
              MotionBackendName(selectedMotionBackend),
              MotionBackendName(g_motionBackend.Current()));
  ImGui::Text("Backend generation: %llu",
              (unsigned long long)g_motionBackend.Generation());
  ImGui::Text(u8"Trojan: %s",
              g_trojanActive ? u8"\u6d3b\u52a8" : u8"\u7a7a\u95f2");
  ImGui::Text(u8"\u76f8\u673a: %s",
              g_cameraActive ? u8"\u6d3b\u52a8" : u8"\u5173\u95ed");
  if (directModeSelected || directBackendActive) {
    ImGui::Text(u8"\u5e7d\u7075\u9aa8\u67b6: %s (generation %llu)",
                GhostRig_IsAliveForGui() ? u8"\u5df2\u521b\u5efa"
                                         : u8"\u672a\u521b\u5efa",
                (unsigned long long)GhostRig_PublicGeneration());
    ImGui::Text("Bind: %s | motionScale %.8f | terrain %s",
                GhostRig_PublicBindStateName(),
                GhostRig_PublicMotionScale(),
                GhostRig_IsTerrainEnabledForGui() ? "on" : "off");
  }
  ImGui::Spacing();
  ImGui::Separator();
  ImGui::Spacing();

  ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 14.0f);
  ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
  if (ImGui::Button(u8"\u91cd\u65b0\u83b7\u53d6\u89d2\u8272", ImVec2(-1, 0))) {
    PostMessageW(g_gameHwnd, WM_MMD_GUI_RECAPTURE, 0, 0);
  }
  ImGui::PopStyleColor();
  ImGui::PopStyleVar();

  ImGui::EndTabItem();
  }

  ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
  bool tabFile = ImGui::BeginTabItem(u8"\u6587\u4ef6");
  ImGui::PopStyleColor();
  if (tabFile) {
    ImGui::Spacing();
    ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 14.0f);

    float availW = ImGui::GetContentRegionAvail().x;
    float itemSpacing = ImGui::GetStyle().ItemSpacing.x;
    float btnW2 = (availW - itemSpacing) * 0.5f;

    MotionBackend fileMode = GuiSelectedMotionBackend();
    auto drawModeButton = [&](const char *label, MotionBackend backend,
                              const char *id) {
      const bool selected = fileMode == backend;
      if (selected) {
        ImGui::PushStyleColor(ImGuiCol_Button,
                              ImVec4(0.95f, 0.76f, 0.05f, 1.0f));
        ImGui::PushStyleColor(ImGuiCol_ButtonHovered,
                              ImVec4(1.00f, 0.82f, 0.12f, 1.0f));
        ImGui::PushStyleColor(ImGuiCol_ButtonActive,
                              ImVec4(0.82f, 0.64f, 0.02f, 1.0f));
        ImGui::PushStyleColor(ImGuiCol_Text,
                              ImVec4(0.08f, 0.08f, 0.09f, 1.0f));
      } else {
        ImGui::PushStyleColor(ImGuiCol_Text,
                              ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
      }
      char buttonId[96] = {};
      snprintf(buttonId, sizeof(buttonId), "%s##%s", label, id);
      const bool clicked = ImGui::Button(buttonId, ImVec2(btnW2, 0));
      ImGui::PopStyleColor(selected ? 4 : 1);
      if (clicked && !selected) {
        fileMode = backend;
        GuiSelectMotionBackend(backend);
        PostMessageW(g_gameHwnd, WM_MMD_GUI_SELECT_MODE,
                     static_cast<WPARAM>(backend), 0);
      }
    };

    ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f),
                       u8"\u64ad\u653e\u6a21\u5f0f");
    drawModeButton(u8"VMD\u76f4\u63a5\u6a21\u5f0f",
                   MotionBackend::DirectVmd, "mode_direct");
    ImGui::SameLine();
    drawModeButton(u8"\u808c\u8089\u6a21\u5f0f", MotionBackend::Muscle,
                   "mode_muscle");
    const bool fileDirectMode = fileMode == MotionBackend::DirectVmd;

    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    if (!fileDirectMode) {
    ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f),
                       u8"\u808c\u8089\u6570\u636e\u6587\u4ef6 (.bin)");
    ImGui::SetNextItemWidth(-1);
    ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 2.0f);
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    ImGui::InputText("##animpath", g_muscleAnimPath, sizeof(g_muscleAnimPath));
    ImGui::PopStyleColor();
    ImGui::PopStyleVar();
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    if (ImGui::Button(u8"\u6d4f\u89c8##anim", ImVec2(btnW2, 0))) {
      OPENFILENAMEA ofn = {};
      char filePath[512] = "";
      ofn.lStructSize = sizeof(ofn);
      ofn.hwndOwner = g_guiHwnd;
      ofn.lpstrFilter = "Muscle Anim (*.bin)\0*.bin\0All Files\0*.*\0";
      ofn.lpstrFile = filePath;
      ofn.nMaxFile = sizeof(filePath);
      ofn.Flags = OFN_FILEMUSTEXIST | OFN_NOCHANGEDIR;
      if (GetOpenFileNameA(&ofn)) {
        strncpy(g_muscleAnimPath, filePath, sizeof(g_muscleAnimPath) - 1);
        g_muscleAnimPath[sizeof(g_muscleAnimPath) - 1] = '\0';
        Log("[GUI] Anim selected: %s", g_muscleAnimPath);
        if (g_muscleAnim) g_muscleAnim->loaded = false;
        if (g_vmd) { FreeVmd(g_vmd); g_vmd = nullptr; g_bsIndicesResolved = false; }
        if (g_footIkVmd) { FreeVmd(g_footIkVmd); g_footIkVmd = nullptr; }
        g_footIkResolved = false;
        if (g_cameraActive) RestoreCinemachine();
        ResetCameraState();
        if (g_musclePlayer) {
          g_musclePlayer->Stop();
          g_musclePlayer->currentTime = 0;
        }
      }
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"\u6e05\u9664##anim", ImVec2(btnW2, 0))) {
      g_muscleAnimPath[0] = '\0';
      PostMessageW(g_gameHwnd, WM_MMD_GUI_UNLOAD_ANIM, 0, 0);
    }
    ImGui::PopStyleColor();

    if (g_muscleAnim && g_muscleAnim->loaded) {
      ImGui::TextColored(ImVec4(0.10f, 0.55f, 0.25f, 1.0f),
                         u8"%d \u5e27 / %.1f \u79d2",
                         g_muscleAnim->frameCount, g_muscleAnim->Duration());
    } else {
      if (g_muscleAnimPath[0] == '\0') {
        ImGui::TextDisabled(u8"\u5df2\u6e05\u9664");
      } else {
        ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d (\u64ad\u653e\u65f6\u81ea\u52a8\u52a0\u8f7d)");
      }
    }
    } else {
    DirectVmdSource_DrawGui(g_guiHwnd);
    ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f),
                       u8"\u52a8\u4f5c VMD (.vmd)");
    ImGui::SetNextItemWidth(-1);
    ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 2.0f);
    ImGui::PushStyleColor(ImGuiCol_Text,
                          ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    ImGui::InputText("##directvmdpath", g_directVmdPath,
                     sizeof(g_directVmdPath));
    if (ImGui::IsItemDeactivatedAfterEdit()) {
      GhostRig_RequestVmdReload(g_directVmdPath[0]
                                    ? g_directVmdPath
                                    : nullptr);
      Log("[P3-GUI] DirectVmd path edited; worker reload requested: %s",
          g_directVmdPath);
    }
    ImGui::PopStyleColor();
    ImGui::PopStyleVar();
    ImGui::PushStyleColor(ImGuiCol_Text,
                          ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    if (ImGui::Button(u8"\u6d4f\u89c8##directvmd", ImVec2(btnW2, 0))) {
      OPENFILENAMEA ofn = {};
      char filePath[512] = "";
      ofn.lStructSize = sizeof(ofn);
      ofn.hwndOwner = g_guiHwnd;
      ofn.lpstrFilter = "VMD Files (*.vmd)\0*.vmd\0All Files\0*.*\0";
      ofn.lpstrFile = filePath;
      ofn.nMaxFile = sizeof(filePath);
      ofn.Flags = OFN_FILEMUSTEXIST | OFN_NOCHANGEDIR;
      if (GetOpenFileNameA(&ofn)) {
        strncpy_s(g_directVmdPath, sizeof(g_directVmdPath), filePath,
                  _TRUNCATE);
        Log("[P3-GUI] DirectVmd selected: %s", g_directVmdPath);
        GhostRig_RequestVmdReload(g_directVmdPath);
        Log("[P3-GUI] DirectVmd worker load requested automatically: %s",
            g_directVmdPath);
      }
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"\u6e05\u9664##directvmd", ImVec2(btnW2, 0))) {
      if (g_motionBackend.Is(MotionBackend::DirectVmd))
        PostMessageW(g_gameHwnd, WM_MMD_GUI_STOP, 0, 0);
      g_directVmdPath[0] = '\0';
      GhostRig_RequestVmdReload(nullptr);
    }
    ImGui::PopStyleColor();
    if (DirectVmdRuntime_IsLoaded()) {
      ImGui::TextColored(
          ImVec4(0.10f, 0.55f, 0.25f, 1.0f),
          u8"%.0f \u5e27 | clip generation %llu",
          DirectVmdRuntime_PublicDurationFrames(),
          (unsigned long long)DirectVmdRuntime_PublicClipGeneration());
    } else {
      ImGui::TextDisabled(
          g_directVmdPath[0]
              ? u8"\u540e\u53f0\u52a0\u8f7d\u4e2d\u6216\u52a0\u8f7d\u5931\u8d25 (\u67e5\u770b\u65e5\u5fd7)"
              : u8"\u672a\u9009\u62e9");
    }
    }

    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f),
                       u8"\u955c\u5934 VMD (.vmd)");
    ImGui::SetNextItemWidth(-1);
    ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 2.0f);
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    ImGui::InputText("##vmdpath", g_cameraVmdPath, sizeof(g_cameraVmdPath));
    if (ImGui::IsItemDeactivatedAfterEdit()) {
      g_cameraOverrideExplicit = g_cameraVmdPath[0] != '\0';
      PostMessageW(g_gameHwnd, WM_MMD_GUI_LOAD_CAMERA, 0, 0);
    }
    ImGui::PopStyleColor();
    ImGui::PopStyleVar();
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    if (ImGui::Button(u8"\u6d4f\u89c8##vmd", ImVec2(btnW2, 0))) {
      OPENFILENAMEA ofn = {};
      char filePath[512] = "";
      ofn.lStructSize = sizeof(ofn);
      ofn.hwndOwner = g_guiHwnd;
      ofn.lpstrFilter = "VMD Files (*.vmd)\0*.vmd\0All Files\0*.*\0";
      ofn.lpstrFile = filePath;
      ofn.nMaxFile = sizeof(filePath);
      ofn.Flags = OFN_FILEMUSTEXIST | OFN_NOCHANGEDIR;
      if (GetOpenFileNameA(&ofn)) {
        strncpy(g_cameraVmdPath, filePath, sizeof(g_cameraVmdPath) - 1);
        g_cameraVmdPath[sizeof(g_cameraVmdPath) - 1] = '\0';
        g_cameraOverrideExplicit = true;
        Log("[GUI] VMD selected: %s", g_cameraVmdPath);
        if (fileDirectMode) {
          PostMessageW(g_gameHwnd, WM_MMD_GUI_LOAD_CAMERA, 0, 0);
        } else {
          if (g_cameraVmd) {
            FreeVmd(g_cameraVmd);
            g_cameraVmd = nullptr;
          }
          if (g_cameraActive) RestoreCinemachine();
          ResetCameraState();
          if (g_musclePlayer) {
            g_musclePlayer->Stop();
            g_musclePlayer->currentTime = 0;
          }
        }
      }
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"\u6e05\u9664##vmd", ImVec2(btnW2, 0))) {
      g_cameraVmdPath[0] = '\0';
      g_cameraOverrideExplicit = false;
      PostMessageW(g_gameHwnd, WM_MMD_GUI_UNLOAD_CAMERA, 0, 0);
    }
    ImGui::PopStyleColor();

    if (fileDirectMode &&
        g_cameraOverrideExplicit &&
        DirectVmdRuntime_CameraOverrideLoaded()) {
      ImGui::TextColored(ImVec4(0.10f, 0.55f, 0.25f, 1.0f),
                         u8"DirectVmd \u76f8\u673a\u8986\u76d6\u5df2\u52a0\u8f7d");
    } else if (fileDirectMode &&
               !g_cameraOverrideExplicit) {
      ImGui::TextDisabled(u8"\u4f7f\u7528\u4e3b Direct VMD \u7684 Camera section");
    } else if (g_cameraVmd && g_cameraVmd->loaded && !g_cameraVmd->cameraKeys.empty()) {
      ImGui::TextColored(ImVec4(0.10f, 0.55f, 0.25f, 1.0f),
                         u8"%zu \u5173\u952e\u5e27",
                         g_cameraVmd->cameraKeys.size());
    } else {
      if (g_cameraVmdPath[0] == '\0') {
        ImGui::TextDisabled(u8"\u5df2\u6e05\u9664");
      } else {
        ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d (\u64ad\u653e\u65f6\u81ea\u52a8\u52a0\u8f7d)");
      }
    }

    if (!fileDirectMode) {
    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();
    ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f),
                       u8"\u4e0b\u534a\u8eab IK VMD (.vmd)");
    ImGui::SetNextItemWidth(-1);
    ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 2.0f);
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    ImGui::InputText("##footikpath", g_footIkVmdPath, sizeof(g_footIkVmdPath));
    ImGui::PopStyleColor();
    ImGui::PopStyleVar();
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    if (ImGui::Button(u8"\u6d4f\u89c8##footik", ImVec2(btnW2, 0))) {
      OPENFILENAMEA ofn = {};
      char filePath[512] = "";
      ofn.lStructSize = sizeof(ofn);
      ofn.hwndOwner = g_guiHwnd;
      ofn.lpstrFilter = "VMD Files (*.vmd)\0*.vmd\0All Files\0*.*\0";
      ofn.lpstrFile = filePath;
      ofn.nMaxFile = sizeof(filePath);
      ofn.Flags = OFN_FILEMUSTEXIST | OFN_NOCHANGEDIR;
      if (GetOpenFileNameA(&ofn)) {
        strncpy(g_footIkVmdPath, filePath, sizeof(g_footIkVmdPath) - 1);
        g_footIkVmdPath[sizeof(g_footIkVmdPath) - 1] = '\0';
        Log("[GUI] Foot IK VMD selected: %s", g_footIkVmdPath);
        if (g_footIkVmd) { FreeVmd(g_footIkVmd); g_footIkVmd = nullptr; }
        g_footIkResolved = false;
        s_footIKFirstCaptured = false;
        s_initialRootCaptured = false;
        s_footIKCalibrated = false;
      }
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"\u6e05\u9664##footik", ImVec2(btnW2, 0))) {
      g_footIkVmdPath[0] = '\0';
      PostMessageW(g_gameHwnd, WM_MMD_GUI_UNLOAD_FOOTIK, 0, 0);
    }
    ImGui::PopStyleColor();

    VmdFile *curFootVmd = g_footIkVmd ? g_footIkVmd : g_vmd;
    if (curFootVmd && curFootVmd->loaded && !curFootVmd->boneTimelines.empty()) {
      ImGui::TextColored(ImVec4(0.10f, 0.55f, 0.25f, 1.0f),
                         u8"%zu \u9aa8\u9abc\u8f68\u9053 (\u542b\u8db3IK/\u4e2d\u5fc3)",
                         curFootVmd->boneTimelines.size());
    } else {
      if (g_footIkVmdPath[0] == '\0') {
        ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d");
      } else {
        ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d (\u64ad\u653e\u65f6\u52a0\u8f7d)");
      }
    }
    }

    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f),
                       u8"\u9762\u90e8 VMD (.vmd)");
    ImGui::SetNextItemWidth(-1);
    ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 2.0f);
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    ImGui::InputText("##morphpath", g_morphVmdPath, sizeof(g_morphVmdPath));
    if (ImGui::IsItemDeactivatedAfterEdit())
      PostMessageW(g_gameHwnd, WM_MMD_GUI_LOAD_MORPH, 0, 0);
    ImGui::PopStyleColor();
    ImGui::PopStyleVar();
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    if (ImGui::Button(u8"\u6d4f\u89c8##morph", ImVec2(btnW2, 0))) {
      OPENFILENAMEA ofn = {};
      char filePath[512] = "";
      ofn.lStructSize = sizeof(ofn);
      ofn.hwndOwner = g_guiHwnd;
      ofn.lpstrFilter = "VMD Files (*.vmd)\0*.vmd\0All Files\0*.*\0";
      ofn.lpstrFile = filePath;
      ofn.nMaxFile = sizeof(filePath);
      ofn.Flags = OFN_FILEMUSTEXIST | OFN_NOCHANGEDIR;
      if (GetOpenFileNameA(&ofn)) {
        strncpy(g_morphVmdPath, filePath, sizeof(g_morphVmdPath) - 1);
        g_morphVmdPath[sizeof(g_morphVmdPath) - 1] = '\0';
        Log("[GUI] Morph VMD selected: %s", g_morphVmdPath);
        PostMessageW(g_gameHwnd, WM_MMD_GUI_LOAD_MORPH, 0, 0);
      }
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"\u6e05\u9664##morph", ImVec2(btnW2, 0))) {
      g_morphVmdPath[0] = '\0';
      PostMessageW(g_gameHwnd, WM_MMD_GUI_UNLOAD_MORPH, 0, 0);
    }
    ImGui::PopStyleColor();

    if (fileDirectMode && g_morphVmdPath[0] &&
        DirectVmdRuntime_MorphOverrideLoaded()) {
      ImGui::TextColored(ImVec4(0.10f, 0.55f, 0.25f, 1.0f),
                         u8"DirectVmd \u9762\u90e8\u8986\u76d6\u5df2\u52a0\u8f7d");
    } else if (fileDirectMode && !g_morphVmdPath[0]) {
      ImGui::TextDisabled(
          u8"\u4f7f\u7528\u4e3b Direct VMD \u7684 Morph section");
    } else if (fileDirectMode) {
      ImGui::TextDisabled(
          u8"\u672a\u52a0\u8f7d (\u9009\u62e9\u6216\u64ad\u653e\u65f6\u81ea\u52a8\u52a0\u8f7d)");
    } else if (g_vmd && g_vmd->loaded &&
               !g_vmd->morphTimelines.empty()) {
      ImGui::TextColored(ImVec4(0.10f, 0.55f, 0.25f, 1.0f),
                         u8"%d \u8868\u60c5\u8f68\u9053",
                         (int)g_vmd->morphTimelines.size());
    } else {
      if (g_morphVmdPath[0] == '\0') {
        ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d");
      } else {
        ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d (\u64ad\u653e\u65f6\u52a0\u8f7d)");
      }
    }

    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.20f, 0.20f, 0.24f, 1.0f), u8"\u97f3\u9891 (.wav/.mp3)");
    ImGui::SetNextItemWidth(-1);
    ImGui::PushStyleVar(ImGuiStyleVar_FrameRounding, 2.0f);
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    ImGui::InputText("##audiopath", g_audioPath, sizeof(g_audioPath));
    ImGui::PopStyleColor();
    ImGui::PopStyleVar();
    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    if (ImGui::Button(u8"\u6d4f\u89c8##audio", ImVec2(btnW2, 0))) {
      OPENFILENAMEW ofn = {};
      wchar_t filePath[512] = L"";
      ofn.lStructSize = sizeof(ofn);
      ofn.hwndOwner = g_guiHwnd;
      ofn.lpstrFilter = L"Audio (*.wav;*.mp3)\0*.wav;*.mp3\0All Files\0*.*\0";
      ofn.lpstrFile = filePath;
      ofn.nMaxFile = 512;
      ofn.Flags = OFN_FILEMUSTEXIST | OFN_NOCHANGEDIR;
      if (GetOpenFileNameW(&ofn)) {
        wcsncpy(g_audioPathW, filePath, 511);
        g_audioPathW[511] = L'\0';
        WideCharToMultiByte(CP_UTF8, 0, filePath, -1,
                            g_audioPath, sizeof(g_audioPath), nullptr, nullptr);
        Log("[GUI] Audio selected: %s", g_audioPath);
        PostMessageW(g_gameHwnd, WM_MMD_GUI_LOAD_AUDIO, 0, 0);
      }
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"\u6e05\u9664##audio", ImVec2(btnW2, 0))) {
      g_audioPath[0] = '\0';
      g_audioPathW[0] = L'\0';
      PostMessageW(g_gameHwnd, WM_MMD_GUI_UNLOAD_AUDIO, 0, 0);
    }
    ImGui::PopStyleColor();

    ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
    ImGui::Checkbox(u8"\u97f3\u9891\u540c\u6b65", &g_audioEnabled);
    ImGui::PopStyleColor();
    ImGui::SameLine();
    if (g_audioPath[0] == '\0') {
      ImGui::TextDisabled(u8"\u672a\u9009\u62e9\u97f3\u9891");
    } else if (fileDirectMode) {
      ImGui::TextDisabled("DirectVmd clock follower (MCI rate control)");
    } else {
      ImGui::TextDisabled(u8"\u53d8\u901f\u65f6\u81ea\u52a8\u5173\u95ed");
    }

    if (g_audioPlayer && g_audioPlayer->loaded) {
      ImGui::TextColored(ImVec4(0.10f, 0.55f, 0.25f, 1.0f),
                         u8"\u5df2\u52a0\u8f7d: %.1f \u79d2",
                         g_audioPlayer->GetLengthMs() / 1000.0f);
    } else {
      if (g_audioPath[0] == '\0') {
        ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d");
      } else {
        ImGui::TextDisabled(u8"\u672a\u52a0\u8f7d");
      }
    }

    ImGui::PopStyleVar();
    ImGui::EndTabItem();
  }

  ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
  bool tabOffset = ImGui::BeginTabItem(u8"\u504f\u79fb");
  ImGui::PopStyleColor();
  if (tabOffset) {
    ImGui::Spacing();
    const bool offsetDirectMode =
        GuiSelectedMotionBackend() == MotionBackend::DirectVmd;

    if (!offsetDirectMode) {
    ImGui::TextColored(ImVec4(0.90f, 0.75f, 0.20f, 1.0f),
                       u8"\u4f4d\u7f6e\u504f\u79fb");
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat("X##pos", &g_posOffsetX, -10.0f, 10.0f, "%.2f");
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat("Y##pos", &g_posOffsetY, -10.0f, 10.0f, "%.2f");
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat("Z##pos", &g_posOffsetZ, -10.0f, 10.0f, "%.2f");

    ImGui::SameLine();
    if (ImGui::SmallButton(u8"\u91cd\u7f6e##pos")) {
      g_posOffsetX = 0.0f;
      g_posOffsetY = 0.0f;
      g_posOffsetZ = 0.0f;
    }

    ImGui::Spacing();
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.90f, 0.75f, 0.20f, 1.0f),
                       u8"\u65cb\u8f6c\u504f\u79fb");
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"Yaw (\u00b0)##yaw", &g_yawOffsetDeg,
                       -180.0f, 180.0f, u8"%.1f\u00b0");
    ImGui::SameLine();
    if (ImGui::SmallButton(u8"\u91cd\u7f6e##yaw")) {
      g_yawOffsetDeg = 0.0f;
    }

    ImGui::Spacing();
    ImGui::Spacing();
    }

    ImGui::TextColored(ImVec4(0.90f, 0.75f, 0.20f, 1.0f),
                       u8"\u955c\u5934\u9ad8\u5ea6\u8865\u507f");
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::TextDisabled(u8"\u81ea\u52a8: %.3f  \u89d2\u8272\u8eab\u9ad8: %.3fm",
                        g_camHeightScale, g_charHeight);
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u8865\u507f##hb", &g_camHeightBias,
                       -0.5f, 0.5f, "%.3f");
    ImGui::SameLine();
    if (ImGui::SmallButton(u8"\u91cd\u7f6e##hb")) {
      g_camHeightBias = 0.0f;
    }

    ImGui::Spacing();
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.90f, 0.75f, 0.20f, 1.0f),
                       u8"\u52a8\u4f5c\u5e45\u5ea6");
    ImGui::Separator();
    ImGui::Spacing();

    if (offsetDirectMode) {
      float directMultiplier = DirectVmdRuntime_GetMotionMultiplier();
      ImGui::SetNextItemWidth(-60);
      if (ImGui::SliderFloat(u8"VMD\u4f4d\u79fb\u500d\u7387##directscale",
                             &directMultiplier, 0.25f, 2.0f, "%.3f")) {
        DirectVmdRuntime_SetMotionMultiplier(directMultiplier);
      }
      ImGui::SameLine();
      if (ImGui::SmallButton(u8"\u91cd\u7f6e##directscale"))
        DirectVmdRuntime_SetMotionMultiplier(1.0f);
      bool kneeMix = s_directVmdKneeMixEnabled.load(std::memory_order_acquire);
      if (ImGui::Checkbox(u8"膝盖弯曲方向混合##directknee", &kneeMix))
        s_directVmdKneeMixEnabled.store(kneeMix, std::memory_order_release);
      float kneeWeight = s_directVmdKneeMixWeight.load(std::memory_order_acquire);
      ImGui::SetNextItemWidth(-60);
      if (ImGui::SliderFloat(u8"混合强度##directknee", &kneeWeight, 0.0f, 1.0f, "%.2f"))
        s_directVmdKneeMixWeight.store(kneeWeight, std::memory_order_release);
      ImGui::SameLine();
      if (ImGui::SmallButton(u8"重置##directknee"))
        s_directVmdKneeMixWeight.store(DIRECT_VMD_KNEE_DEFAULT_WEIGHT, std::memory_order_release);
      ImGui::TextDisabled(u8"缺少有效膝盖 FK 时沿用原参考；不改变脚目标");
      ImGui::TextDisabled(
          u8"\u4e0e Root\u3001Center\u3001Groove \u548c\u8db3 IK \u5171\u7528\u540c\u4e00\u500d\u7387");
    } else {
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u6574\u4f53##scale", &g_motionScale,
                       0.10f, 2.00f, "%.2f");
    ImGui::SameLine();
    if (ImGui::SmallButton(u8"\u91cd\u7f6e##scale")) {
      g_motionScale = 1.0f;
    }

    ImGui::Spacing();
    ImGui::TextDisabled(u8"\u90e8\u4f4d\u7f29\u653e (\u00d7 \u6574\u4f53)");

    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u8eaf\u5e72##sp", &g_scaleSpine, 0.0f, 2.0f, "%.2f");
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u5934\u90e8##hd", &g_scaleHead, 0.0f, 2.0f, "%.2f");
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u5de6\u81c2##la", &g_scaleLArm, 0.0f, 2.0f, "%.2f");
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u53f3\u81c2##ra", &g_scaleRArm, 0.0f, 2.0f, "%.2f");
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u817f\u90e8##lg", &g_scaleLegs, 0.0f, 2.0f, "%.2f");
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u624b\u6307##fg", &g_scaleFingers, 0.0f, 2.0f, "%.2f");
    ImGui::SetNextItemWidth(-60);
    ImGui::SliderFloat(u8"\u624b\u6307\u95f4\u8ddd##sp", &g_splayBlend, 0.0f, 1.0f, "%.2f");

    ImGui::SameLine();
    if (ImGui::SmallButton(u8"\u91cd\u7f6e##parts")) {
      g_scaleSpine = 1.0f; g_scaleHead = 1.0f;
      g_scaleLArm = 1.0f; g_scaleRArm = 1.0f;
      g_scaleLegs = 1.0f; g_scaleFingers = 1.0f;
      g_splayBlend = 0.7f;
    }
    }

    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    if (ImGui::Button(u8"\u5168\u90e8\u91cd\u7f6e", ImVec2(-1, 0))) {
      g_camHeightBias = 0.0f;
      if (offsetDirectMode) {
        DirectVmdRuntime_SetMotionMultiplier(1.0f);
      } else {
        g_posOffsetX = 0.0f;
        g_posOffsetY = 0.0f;
        g_posOffsetZ = 0.0f;
        g_yawOffsetDeg = 0.0f;
        g_motionScale = 1.0f;
        g_scaleSpine = 1.0f; g_scaleHead = 1.0f;
        g_scaleLArm = 1.0f; g_scaleRArm = 1.0f;
        g_scaleLegs = 1.0f; g_scaleFingers = 1.0f;
        g_splayBlend = 0.7f;
      }
    }

    ImGui::EndTabItem();
  }

  ImGui::PushStyleColor(ImGuiCol_Text, ImVec4(0.95f, 0.95f, 0.97f, 1.0f));
  bool tabAbout = ImGui::BeginTabItem(u8"\u5173\u4e8e");
  ImGui::PopStyleColor();
  if (tabAbout) {
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(1.00f, 0.85f, 0.00f, 1.0f), "EIEM");
    ImGui::SameLine();
    ImGui::TextDisabled("v%s", EIEM_VERSION);
    ImGui::TextDisabled(u8"\u660e\u65e5\u65b9\u821f\uff1a\u7ec8\u672b\u5730 \u52a8\u4f5c\u4e0e\u8868\u60c5\u63a7\u5236\u63d2\u4ef6");
    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.90f, 0.75f, 0.20f, 1.0f), u8"\u9879\u76ee\u5f00\u6e90\u5730\u5740");
    ImGui::PushStyleColor(ImGuiCol_Button, IM_COL32(35, 38, 48, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonHovered, IM_COL32(50, 55, 70, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonActive, IM_COL32(25, 28, 38, 255));
    ImGui::PushStyleColor(ImGuiCol_Text, IM_COL32(80, 170, 255, 255));
    if (ImGui::Button("https://github.com/Sasye/EIEM", ImVec2(-60, 0))) {
      ShellExecuteA(NULL, "open", "https://github.com/Sasye/EIEM", NULL, NULL, SW_SHOWNORMAL);
    }
    if (ImGui::IsItemHovered()) {
      ImGui::SetTooltip(u8"\u70b9\u51fb\u5728\u6d4f\u89c8\u5668\u4e2d\u6253\u5f00 GitHub \u4ed3\u5e93");
    }
    ImGui::PopStyleColor(4);
    ImGui::SameLine();
    if (ImGui::Button(u8"\u590d\u5236##repo", ImVec2(-1, 0))) {
      ImGui::SetClipboardText("https://github.com/Sasye/EIEM");
    }

    ImGui::Spacing();
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.90f, 0.75f, 0.20f, 1.0f), u8"\u4ea4\u6d41\u7fa4");
    ImGui::PushStyleColor(ImGuiCol_FrameBg, IM_COL32(35, 38, 48, 255));
    ImGui::PushStyleColor(ImGuiCol_Text, IM_COL32(255, 218, 0, 255));
    static char s_qqGroupBuf[] = "1036919766";
    ImGui::SetNextItemWidth(-60);
    ImGui::InputText("##qqgroup", s_qqGroupBuf, sizeof(s_qqGroupBuf), ImGuiInputTextFlags_ReadOnly);
    ImGui::PopStyleColor(2);
    ImGui::SameLine();
    if (ImGui::Button(u8"\u590d\u5236##group", ImVec2(-1, 0))) {
      ImGui::SetClipboardText("1036919766");
    }

    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::TextDisabled(u8"\u5f00\u6e90\u8bb8\u53ef: AGPL-3.0 with an additional User Agreement.");
    ImGui::TextDisabled(u8"\u4f5c\u8005: Sasye");

    ImGui::EndTabItem();
  }

  ImGui::EndTabBar();

  ImGui::Separator();
  ImGui::Spacing();

  bool resultExpired = g_updateResultTime &&
                       (GetTickCount() - g_updateResultTime > 5000);
  if (resultExpired && !g_updateAvailable) {
    g_updateIsLatest = false;
    g_updateCheckFailed = false;
  }

  ImGui::TextDisabled("v%s", EIEM_VERSION);
  ImGui::SameLine();

  if (g_updateChecking) {
    ImGui::TextDisabled(u8"\u68c0\u67e5\u4e2d...");
  } else if (g_updateAvailable) {
    char updateLabel[128];
    snprintf(updateLabel, sizeof(updateLabel),
             u8"\u65b0\u7248\u672c v%s \u53ef\u7528 - \u70b9\u51fb\u4e0b\u8f7d", g_latestVersion);
    ImGui::PushStyleColor(ImGuiCol_Button, IM_COL32(80, 60, 0, 200));
    ImGui::PushStyleColor(ImGuiCol_ButtonHovered, IM_COL32(120, 90, 0, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonActive, IM_COL32(60, 45, 0, 255));
    ImGui::PushStyleColor(ImGuiCol_Text, IM_COL32(255, 200, 50, 255));
    if (ImGui::SmallButton(updateLabel)) {
      ShellExecuteA(NULL, "open", g_updateUrl, NULL, NULL, SW_SHOWNORMAL);
    }
    ImGui::PopStyleColor(4);
  } else if (g_updateIsLatest) {
    ImGui::TextColored(ImVec4(0.3f, 0.85f, 0.4f, 1.0f),
                       u8"\u5df2\u662f\u6700\u65b0\u7248\u672c");
  } else if (g_updateCheckFailed) {
    ImGui::TextColored(ImVec4(0.9f, 0.35f, 0.3f, 1.0f),
                       u8"\u68c0\u67e5\u5931\u8d25");
  } else {
    ImGui::PushStyleColor(ImGuiCol_Button, IM_COL32(50, 53, 65, 200));
    ImGui::PushStyleColor(ImGuiCol_ButtonHovered, IM_COL32(70, 73, 90, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonActive, IM_COL32(40, 42, 55, 255));
    if (ImGui::SmallButton(u8"\u68c0\u67e5\u66f4\u65b0")) {
      CreateThread(NULL, 0,
                   [](LPVOID) -> DWORD { CheckForUpdates(); return 0; },
                   NULL, 0, NULL);
    }
    ImGui::PopStyleColor(3);
  }

  ImGui::EndChild();
  ImGui::End();

  if (!g_disclaimerAccepted) {
    ImGui::SetNextWindowSize(ImVec2(340, 0), ImGuiCond_Always);
    ImVec2 displaySize = ImGui::GetIO().DisplaySize;
    ImGui::SetNextWindowPos(
        ImVec2(displaySize.x * 0.5f, displaySize.y * 0.5f),
        ImGuiCond_Appearing, ImVec2(0.5f, 0.5f));

    ImGui::PushStyleVar(ImGuiStyleVar_WindowPadding, ImVec2(16, 14));
    ImGui::PushStyleVar(ImGuiStyleVar_WindowRounding, 6.0f);
    ImGui::PushStyleColor(ImGuiCol_WindowBg, IM_COL32(20, 22, 30, 245));
    ImGui::PushStyleColor(ImGuiCol_Border, IM_COL32(255, 218, 0, 150));

    ImGui::Begin(u8"##disclaimer_window", nullptr,
                 ImGuiWindowFlags_AlwaysAutoResize |
                 ImGuiWindowFlags_NoTitleBar |
                 ImGuiWindowFlags_NoResize |
                 ImGuiWindowFlags_NoCollapse |
                 ImGuiWindowFlags_NoSavedSettings |
                 ImGuiWindowFlags_NoMove);

    ImGui::TextColored(ImVec4(1.0f, 0.85f, 0.0f, 1.0f),
                       u8"\u7528\u6237\u534f\u8bae\u4e0e\u514d\u8d23\u58f0\u660e");
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::PushTextWrapPos(ImGui::GetCursorPosX() + 310.0f);
    ImGui::PushStyleColor(ImGuiCol_Text, ImGui::GetStyleColorVec4(ImGuiCol_TextDisabled));
    ImGui::TextWrapped(
        u8"\u5728\u4e0b\u8f7d\u3001\u5b89\u88c5\u6216\u4f7f\u7528\u672c\u63d2\u4ef6\uff08EIEM\uff09"
        u8"\u4e4b\u524d\uff0c\u8bf7\u60a8\u4ed4\u7ec6\u9605\u8bfb\u672c\u534f\u8bae\u3002"
        u8"\u4f7f\u7528\u672c\u63d2\u4ef6\u5373\u4ee3\u8868\u60a8\u5df2\u5b8c\u6574\u9605\u8bfb\u3001"
        u8"\u5145\u5206\u7406\u89e3\u5e76\u540c\u610f\u9075\u5b88\u4ee5\u4e0b\u6240\u6709\u6761\u6b3e\u3002");
    ImGui::PopStyleColor();
    ImGui::PopTextWrapPos();
    ImGui::Spacing();

    ImGui::BeginChild("##tos_scroll", ImVec2(0, 280), true);
    ImGui::PushStyleColor(ImGuiCol_Text, ImGui::GetStyleColorVec4(ImGuiCol_TextDisabled));

    ImGui::TextColored(ImVec4(0.9f, 0.8f, 0.3f, 1.0f),
                       u8"1. \u5f00\u6e90\u8bb8\u53ef\u4e0e\u6700\u7ec8\u7528\u6237\u6743\u5229");
    ImGui::TextWrapped(
        u8"\u2022 \u672c\u63d2\u4ef6\u57fa\u4e8e AGPL-3.0 \u8bb8\u53ef\u8bc1\u5728 GitHub "
        u8"\u5e73\u53f0\u5b8c\u5168\u5f00\u6e90\u3002\u7528\u6237\u53ef\u5728\u9075\u5b88"
        u8"\u8be5\u8bb8\u53ef\u8bc1\u7684\u524d\u63d0\u4e0b\u81ea\u7531\u4f7f\u7528\u3001"
        u8"\u4fee\u6539\u548c\u5206\u53d1\u672c\u63d2\u4ef6\u7684\u6e90\u4ee3\u7801\u3002");
    ImGui::TextWrapped(
        u8"\u2022 \u6700\u7ec8\u7528\u6237\uff08End User\uff09\u5728\u4e0d\u5bf9\u672c"
        u8"\u63d2\u4ef6\u8fdb\u884c\u4fee\u6539\u7684\u524d\u63d0\u4e0b\uff0c\u4f7f\u7528"
        u8"\u548c\u5206\u53d1\u672c\u63d2\u4ef6\u4e0d\u53d7\u4efb\u4f55\u9650\u5236\u3002"
        u8"\u6b64\u6743\u5229\u4e0d\u56e0\u7528\u6237\u662f\u5426\u8fdd\u53cd\u672c\u534f"
        u8"\u8bae\u800c\u6539\u53d8\u3002");
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.9f, 0.8f, 0.3f, 1.0f),
                       u8"2. \u53cd\u6b3a\u8bc8\u58f0\u660e");
    ImGui::TextWrapped(
        u8"\u2022 \u60a8\u4e0d\u5f97\u5728\u7f51\u7edc\u9500\u552e\u5e73\u53f0\u516c\u7136"
        u8"\u552e\u5356\u672c\u63d2\u4ef6\u8f6f\u4ef6\u672c\u4f53\u4e14\u672a\u63d0\u4f9b "
        u8"GitHub \u4ed3\u5e93\u5730\u5740\u4e0e\u552e\u540e\u670d\u52a1\u3002");
    ImGui::TextWrapped(
        u8"\u2022 \u672c\u63d2\u4ef6\u5728 GitHub \u5e73\u53f0\u5b8c\u5168\u514d\u8d39"
        u8"\u5f00\u6e90\uff0c\u5982\u679c\u60a8\u662f\u4ed8\u8d39\u8d2d\u4e70\u83b7\u53d6"
        u8"\u7684\uff0c\u8bf7\u77e5\u6089\u672c\u63d2\u4ef6\u53ef\u4ece GitHub \u514d\u8d39"
        u8"\u83b7\u53d6\u3002");
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.9f, 0.8f, 0.3f, 1.0f),
                       u8"3. \u5185\u5bb9\u5408\u89c4\u4e0e\u884c\u4e3a\u7ea6\u675f");
    ImGui::TextWrapped(
        u8"\u2022 \u672c\u63d2\u4ef6\u672c\u8eab\u4e0d\u5305\u542b\u4efb\u4f55\u6e38\u620f"
        u8"\u7f8e\u672f\u8d44\u4ea7\u3002\u7528\u6237\u77e5\u6089\u5e76\u540c\u610f\uff0c"
        u8"\u300a\u660e\u65e5\u65b9\u821f\uff1a\u7ec8\u672b\u5730\u300b\u6e38\u620f\u5185"
        u8"\u7f6e\u7684\u5b98\u65b9\u52a8\u753b\u3001\u573a\u666f\u3001\u6a21\u578b\u7b49"
        u8"\u8d44\u4ea7\u5176\u7248\u6743\u5b8c\u5168\u96b6\u5c5e\u4e8e\u9e70\u89d2\u7f51"
        u8"\u7edc\uff0c\u5e76\u4e0d\u9002\u7528 AGPL-3.0 \u534f\u8bae\u3002");
    ImGui::TextWrapped(
        u8"\u2022 \u60a8\u4e0d\u5e94\u8be5\u5229\u7528\u672c\u63d2\u4ef6"
        u8"\uff0c\u6216\u5229\u7528\u6e38\u620f\u5185\u7f6e\u7684\u5b98\u65b9\u52a8\u753b"
        u8"\u3001\u573a\u666f\u3001\u6a21\u578b\u7b49\u6e38\u620f\u8d44\u4ea7\uff0c\u5236"
        u8"\u4f5c\u3001\u64ad\u653e\u6216\u4f20\u64ad\u4efb\u4f55\u4e0d\u5408\u9002\u7684"
        u8"\u52a8\u4f5c/\u52a8\u753b\uff08\u5305\u62ec\u4f46\u4e0d\u9650\u4e8e\u8272\u60c5"
        u8"\u3001\u66b4\u529b\u3001\u653f\u6cbb\u654f\u611f\u7b49\u8fdd\u53cd\u6cd5\u5f8b"
        u8"\u6cd5\u89c4\u6216\u5f15\u8d77\u793e\u533a\u4e0d\u9002\u7684\u5185\u5bb9\uff09\u3002");
    ImGui::TextWrapped(
        u8"\u2022 \u4f7f\u7528\u8005\u5e94\u81ea\u884c\u8d1f\u8d23\u786e\u4fdd\u5176\u62e5\u6709"
        u8"\u4efb\u4f55\u6c47\u5165\u8d44\u6e90\u7684\u7248\u6743\uff0c\u5e76\u9075\u5b88"
        u8"\u5176\u8bb8\u53ef\u548c\u4f7f\u7528\u6761\u6b3e\u3002");
    ImGui::Spacing();

    ImGui::TextColored(ImVec4(0.9f, 0.8f, 0.3f, 1.0f),
                       u8"4. \u98ce\u9669\u4e0e\u514d\u8d23\u58f0\u660e");
    ImGui::TextWrapped(
        u8"\u2022 \u672c\u9879\u76ee\u4ec5\u4f9b\u5b66\u4e60\u3001\u6280\u672f\u7814\u7a76"
        u8"\u548c\u4ea4\u6d41\u76ee\u7684\u3002\u4f5c\u8005\u4e0d\u5bf9\u4f7f\u7528\u8005"
        u8"\u5982\u4f55\u4f7f\u7528\u672c\u63d2\u4ef6\u8d1f\u8d23\uff0c\u5305\u62ec\u4efb"
        u8"\u4f55\u8bef\u7528\u60c5\u51b5\u3002\u672c\u63d2\u4ef6\u4e2d\u4f7f\u7528\u7684"
        u8"\u660e\u65e5\u65b9\u821f\u6e38\u620f\u6570\u636e\u8d44\u4ea7\u7248\u6743\u5747"
        u8"\u96b6\u5c5e\u4e8e\u9e70\u89d2\u7f51\u7edc\u3002");
    ImGui::TextWrapped(
        u8"\u2022 \u4f7f\u7528\u672c\u5de5\u5177\u53ef\u80fd\u8fdd\u53cd\u6e38\u620f\u670d"
        u8"\u52a1\u6761\u6b3e\uff0c\u5b58\u5728\u8d26\u53f7\u5c01\u7981\u7684\u98ce\u9669"
        u8"\u3002\u56e0\u4f7f\u7528\u672c\u63d2\u4ef6\u800c\u76f4\u63a5\u6216\u95f4\u63a5"
        u8"\u5bfc\u81f4\u7684\u4efb\u4f55\u635f\u5931\uff08\u5305\u62ec\u4f46\u4e0d\u9650"
        u8"\u4e8e\u8d26\u53f7\u5c01\u7981\u3001\u6e38\u620f\u6570\u636e\u635f\u574f\u7b49"
        u8"\uff09\uff0c\u672c\u9879\u76ee\u4e0d\u627f\u62c5\u4efb\u4f55\u6cd5\u5f8b\u6216"
        u8"\u7ecf\u6d4e\u8d23\u4efb\u3002\u7528\u6237\u9700\u81ea\u884c\u627f\u62c5\u6240"
        u8"\u6709\u98ce\u9669\uff0c\u5f3a\u70c8\u5efa\u8bae\u60a8\u5728\u6d4b\u8bd5\u8d26"
        u8"\u53f7\u4e0a\u8fd0\u884c\u3002");

    ImGui::PopStyleColor();
    ImGui::EndChild();

    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    float btnW = 200.0f;
    float avail = ImGui::GetContentRegionAvail().x;
    ImGui::SetCursorPosX(ImGui::GetCursorPosX() + (avail - btnW) * 0.5f);

    ImGui::PushStyleColor(ImGuiCol_Button, IM_COL32(255, 218, 0, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonHovered, IM_COL32(255, 230, 80, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonActive, IM_COL32(200, 170, 0, 255));
    ImGui::PushStyleColor(ImGuiCol_Text, IM_COL32(20, 20, 20, 255));
    if (ImGui::Button(
            u8"\u6211\u5df2\u9605\u8bfb\u5e76\u540c\u610f",
            ImVec2(btnW, 32))) {
      g_disclaimerAccepted = true;
    }
    ImGui::PopStyleColor(4);

    ImGui::End();
    ImGui::PopStyleColor(2);
    ImGui::PopStyleVar(2);
  }

  if (g_disclaimerAccepted && g_updateAvailable && !g_updateDismissed) {
    ImGui::SetNextWindowSize(ImVec2(280, 0), ImGuiCond_Always);
    ImVec2 displaySize = ImGui::GetIO().DisplaySize;
    ImGui::SetNextWindowPos(
        ImVec2(displaySize.x * 0.5f, displaySize.y * 0.5f),
        ImGuiCond_Appearing, ImVec2(0.5f, 0.5f));

    ImGui::PushStyleVar(ImGuiStyleVar_WindowPadding, ImVec2(16, 12));
    ImGui::PushStyleVar(ImGuiStyleVar_WindowRounding, 6.0f);
    ImGui::PushStyleColor(ImGuiCol_WindowBg, IM_COL32(30, 32, 40, 240));
    ImGui::PushStyleColor(ImGuiCol_Border, IM_COL32(255, 218, 0, 120));

    ImGui::Begin(u8"##update_window", nullptr,
                 ImGuiWindowFlags_AlwaysAutoResize |
                 ImGuiWindowFlags_NoTitleBar |
                 ImGuiWindowFlags_NoResize |
                 ImGuiWindowFlags_NoCollapse |
                 ImGuiWindowFlags_NoSavedSettings);

    ImGui::TextColored(ImVec4(1.0f, 0.85f, 0.0f, 1.0f),
                       u8"\u53d1\u73b0\u65b0\u7248\u672c");
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::Text(u8"\u5f53\u524d\u7248\u672c: v%s", EIEM_VERSION);
    ImGui::TextColored(ImVec4(0.3f, 1.0f, 0.5f, 1.0f),
                       u8"\u6700\u65b0\u7248\u672c: v%s", g_latestVersion);

    if (g_updateChangelog[0]) {
      ImGui::Spacing();
      ImGui::Separator();
      ImGui::Spacing();
      ImGui::TextColored(ImVec4(0.7f, 0.7f, 0.75f, 1.0f),
                         u8"\u66f4\u65b0\u5185\u5bb9:");
      char clBuf[512];
      strncpy(clBuf, g_updateChangelog, sizeof(clBuf) - 1);
      clBuf[sizeof(clBuf) - 1] = '\0';
      ImGui::PushTextWrapPos(ImGui::GetCursorPosX() + 250.0f);
      ImGui::TextDisabled("%s", clBuf);
      ImGui::PopTextWrapPos();
    }

    ImGui::Spacing();
    ImGui::Separator();
    ImGui::Spacing();

    ImGui::PushStyleColor(ImGuiCol_Button, IM_COL32(255, 218, 0, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonHovered, IM_COL32(255, 230, 80, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonActive, IM_COL32(200, 170, 0, 255));
    ImGui::PushStyleColor(ImGuiCol_Text, IM_COL32(20, 20, 20, 255));
    if (ImGui::Button(u8"\u524d\u5f80\u4e0b\u8f7d", ImVec2(120, 0))) {
      ShellExecuteA(NULL, "open", g_updateUrl, NULL, NULL, SW_SHOWNORMAL);
      g_updateDismissed = true;
    }
    ImGui::PopStyleColor(4);

    ImGui::SameLine();

    ImGui::PushStyleColor(ImGuiCol_Button, IM_COL32(55, 58, 70, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonHovered, IM_COL32(80, 83, 95, 255));
    ImGui::PushStyleColor(ImGuiCol_ButtonActive, IM_COL32(40, 42, 55, 255));
    if (ImGui::Button(u8"\u7a0d\u540e\u518d\u8bf4", ImVec2(120, 0))) {
      g_updateDismissed = true;
    }
    ImGui::PopStyleColor(3);

    ImGui::End();
    ImGui::PopStyleColor(2);
    ImGui::PopStyleVar(2);
  }
}

static DWORD WINAPI GuiThread(LPVOID) {
  Log("[GUI] Thread started, waiting for game window...");

  void *domain = il2cpp_domain_get ? il2cpp_domain_get() : nullptr;
  if (domain && il2cpp_thread_attach)
    il2cpp_thread_attach(domain);

  while (g_guiRunning && !g_gameHwnd) {
    g_gameHwnd = FindGameWindow();
    if (!g_gameHwnd) Sleep(200);
  }
  if (!g_guiRunning || !g_gameHwnd) return 0;

  Log("[GUI] Game window found: %p (pid=%lu)", g_gameHwnd, GetCurrentProcessId());

  WNDCLASSEXW wc = {};
  wc.cbSize = sizeof(wc);
  wc.style = CS_CLASSDC;
  wc.lpfnWndProc = GuiWndProc;
  wc.hInstance = GetModuleHandle(nullptr);
  wc.lpszClassName = L"EIEM_GUI";
  wc.hCursor = LoadCursor(nullptr, IDC_ARROW);
  RegisterClassExW(&wc);

  RECT gr;
  GetWindowRect(g_gameHwnd, &gr);
  int panelW = 380;
  int panelH = 580;
  int posX = gr.right - panelW - 20;
  int posY = gr.top + 40;

  g_guiHwnd = CreateWindowExW(
      WS_EX_TOPMOST | WS_EX_TOOLWINDOW, wc.lpszClassName, L"EIEM",
      WS_POPUP, posX, posY, panelW, panelH,
      nullptr, nullptr, wc.hInstance, nullptr);

  if (!g_guiHwnd) {
    Log("[GUI] ERROR: CreateWindowExW failed! err=%d", GetLastError());
    return 0;
  }

  {
    const DWORD attr = 33;
    const DWORD pref = 2;
    DwmSetWindowAttribute(g_guiHwnd, attr, &pref, sizeof(pref));
  }

  {
    enum ACCENT_STATE {
      ACCENT_DISABLED = 0,
      ACCENT_ENABLE_GRADIENT = 1,
      ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
      ACCENT_ENABLE_BLURBEHIND = 3,
      ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
    };
    struct ACCENT_POLICY {
      DWORD AccentState;
      DWORD AccentFlags;
      DWORD GradientColor;
      DWORD AnimationId;
    };
    struct WINCOMPATTRDATA {
      DWORD Attrib;
      PVOID pvData;
      SIZE_T cbData;
    };
    typedef BOOL(WINAPI * pSetWindowCompositionAttribute)(HWND,
                                                          WINCOMPATTRDATA *);

    HMODULE hUser = GetModuleHandleW(L"user32.dll");
    auto SetWCA = (pSetWindowCompositionAttribute)GetProcAddress(
        hUser, "SetWindowCompositionAttribute");
    if (SetWCA) {
      ACCENT_POLICY accent = {};
      accent.AccentState = ACCENT_ENABLE_ACRYLICBLURBEHIND;
      accent.AccentFlags = 0;
      accent.GradientColor = 0x00FFFFFF;
      WINCOMPATTRDATA data = {};
      data.Attrib = 19;
      data.pvData = &accent;
      data.cbData = sizeof(accent);
      SetWCA(g_guiHwnd, &data);
      Log("[GUI] Acrylic blur-behind enabled");
    } else {
      Log("[GUI] SetWindowCompositionAttribute unavailable");
    }
  }


  if (!CreateDeviceD3D(g_guiHwnd)) {
    Log("[GUI] ERROR: CreateDeviceD3D failed!");
    CleanupDeviceD3D();
    DestroyWindow(g_guiHwnd);
    UnregisterClassW(wc.lpszClassName, wc.hInstance);
    return 0;
  }

  Log("[GUI] DX11 device created successfully");

  IMGUI_CHECKVERSION();
  ImGui::CreateContext();
  ImGuiIO &io = ImGui::GetIO();
  io.IniFilename = nullptr;
  io.ConfigFlags |= ImGuiConfigFlags_NavEnableKeyboard;
  io.MouseDrawCursor = false;

  ImGui::StyleColorsDark();
  ImGuiStyle &style = ImGui::GetStyle();
  style.WindowRounding = 6.0f;
  style.FrameRounding = 2.0f;
  style.GrabRounding = 2.0f;
  style.TabRounding = 0.0f;
  style.WindowBorderSize = 1.0f;
  style.FrameBorderSize = 1.0f;
  style.FramePadding = ImVec2(8, 4);
  style.ItemSpacing = ImVec2(8, 6);
  style.WindowPadding = ImVec2(10, 0);
  style.ScrollbarSize = 12.0f;
  style.ScrollbarRounding = 0.0f;
  style.GrabMinSize = 10.0f;
  style.PopupRounding = 0.0f;

  ImVec4 *c = style.Colors;
  c[ImGuiCol_WindowBg]       = ImVec4(1.00f, 1.00f, 1.00f, 0.02f);
  c[ImGuiCol_ChildBg]        = ImVec4(0.12f, 0.12f, 0.12f, 0.00f);
  c[ImGuiCol_PopupBg]        = ImVec4(0.10f, 0.10f, 0.10f, 0.95f);
  c[ImGuiCol_Border]         = ImVec4(0.55f, 0.55f, 0.58f, 0.40f);
  c[ImGuiCol_BorderShadow]   = ImVec4(0.00f, 0.00f, 0.00f, 0.00f);
  c[ImGuiCol_FrameBg]        = ImVec4(0.18f, 0.18f, 0.18f, 1.00f);
  c[ImGuiCol_FrameBgHovered] = ImVec4(0.25f, 0.25f, 0.25f, 1.00f);
  c[ImGuiCol_FrameBgActive]  = ImVec4(0.30f, 0.30f, 0.30f, 1.00f);
  c[ImGuiCol_Button]         = ImVec4(0.22f, 0.22f, 0.22f, 1.00f);
  c[ImGuiCol_ButtonHovered]  = ImVec4(0.30f, 0.30f, 0.30f, 1.00f);
  c[ImGuiCol_ButtonActive]   = ImVec4(0.16f, 0.16f, 0.16f, 1.00f);
  c[ImGuiCol_Header]         = ImVec4(0.22f, 0.22f, 0.22f, 1.00f);
  c[ImGuiCol_HeaderHovered]  = ImVec4(0.32f, 0.32f, 0.30f, 1.00f);
  c[ImGuiCol_HeaderActive]   = ImVec4(1.00f, 0.85f, 0.00f, 0.80f);
  c[ImGuiCol_Text]           = ImVec4(0.08f, 0.08f, 0.10f, 1.00f);
  c[ImGuiCol_TextDisabled]   = ImVec4(0.35f, 0.35f, 0.38f, 1.00f);
  c[ImGuiCol_Separator]      = ImVec4(0.60f, 0.60f, 0.62f, 0.50f);
  c[ImGuiCol_SeparatorHovered]= ImVec4(0.40f, 0.40f, 0.40f, 0.80f);
  c[ImGuiCol_SeparatorActive]= ImVec4(1.00f, 0.85f, 0.00f, 1.00f);
  c[ImGuiCol_SliderGrab]     = ImVec4(0.90f, 0.75f, 0.00f, 0.90f);
  c[ImGuiCol_SliderGrabActive]= ImVec4(1.00f, 0.85f, 0.00f, 1.00f);
  c[ImGuiCol_CheckMark]      = ImVec4(1.00f, 0.85f, 0.00f, 1.00f);
  c[ImGuiCol_PlotHistogram]  = ImVec4(0.85f, 0.70f, 0.00f, 1.00f);
  c[ImGuiCol_ScrollbarBg]    = ImVec4(0.10f, 0.10f, 0.10f, 0.40f);
  c[ImGuiCol_ScrollbarGrab]  = ImVec4(0.35f, 0.35f, 0.35f, 1.00f);
  c[ImGuiCol_ScrollbarGrabHovered]= ImVec4(0.45f, 0.45f, 0.45f, 1.00f);
  c[ImGuiCol_ScrollbarGrabActive] = ImVec4(1.00f, 0.85f, 0.00f, 1.00f);
  c[ImGuiCol_Tab]            = ImVec4(0.18f, 0.18f, 0.18f, 1.00f);
  c[ImGuiCol_TabHovered]     = ImVec4(0.35f, 0.35f, 0.30f, 1.00f);
  c[ImGuiCol_TabActive]      = ImVec4(1.00f, 0.85f, 0.00f, 0.85f);
  c[ImGuiCol_ResizeGrip]     = ImVec4(0.30f, 0.30f, 0.30f, 0.40f);
  c[ImGuiCol_ResizeGripHovered]= ImVec4(0.50f, 0.50f, 0.50f, 0.60f);
  c[ImGuiCol_ResizeGripActive]= ImVec4(1.00f, 0.85f, 0.00f, 0.90f);

  {
    ImFontConfig fontCfg;
    fontCfg.OversampleH = 2;
    fontCfg.OversampleV = 1;
    fontCfg.PixelSnapH = true;
    const char *fontPath = "C:\\Windows\\Fonts\\msyh.ttc";
    bool loaded = false;
    if (GetFileAttributesA(fontPath) != INVALID_FILE_ATTRIBUTES) {
      ImFont *f = io.Fonts->AddFontFromFileTTF(
          fontPath, 18.0f, &fontCfg,
          io.Fonts->GetGlyphRangesChineseSimplifiedCommon());
      loaded = (f != nullptr);
    }
    if (!loaded) {
      io.Fonts->AddFontDefault();
      Log("[GUI] WARN: msyh.ttc not found, Chinese text may not render");
    }
  }

  ImGui_ImplWin32_Init(g_guiHwnd);
  ImGui_ImplDX11_Init(g_pd3dDevice, g_pd3dDeviceContext);

  Log("[GUI] ImGui initialized, panel ready");



  g_guiVisible = false;
  ShowWindow(g_guiHwnd, SW_HIDE);

  MSG msg;
  ZeroMemory(&msg, sizeof(msg));
  while (g_guiRunning) {
    while (PeekMessage(&msg, nullptr, 0U, 0U, PM_REMOVE)) {
      TranslateMessage(&msg);
      DispatchMessage(&msg);
      if (msg.message == WM_QUIT) {
        g_guiRunning = false;
        break;
      }
    }
    if (!g_guiRunning) break;

    if (!IsWindow(g_gameHwnd)) {
      Log("[GUI] Game window gone, shutting down");
      g_guiRunning = false;
      break;
    }

    static bool s_panelShown = false;

    if (!g_guiVisible) {
      if (s_panelShown) {
        SetWindowPos(g_guiHwnd, HWND_NOTOPMOST, 0, 0, 0, 0,
                     SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        ShowWindow(g_guiHwnd, SW_HIDE);
        s_panelShown = false;
      }
      Sleep(60);
      continue;
    }

    if (g_gameHwnd && IsIconic(g_gameHwnd)) {
      if (s_panelShown) {
        SetWindowPos(g_guiHwnd, HWND_NOTOPMOST, 0, 0, 0, 0,
                     SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        ShowWindow(g_guiHwnd, SW_HIDE);
        s_panelShown = false;
      }
      Sleep(100);
      continue;
    }

    HWND fg = GetForegroundWindow();
    bool shouldShow = true;
    if (fg && fg != g_gameHwnd && fg != g_guiHwnd) {
      DWORD fgPid = 0, gamePid = 0;
      GetWindowThreadProcessId(fg, &fgPid);
      if (g_gameHwnd) GetWindowThreadProcessId(g_gameHwnd, &gamePid);
      if (fgPid != 0 && gamePid != 0 && fgPid != gamePid) {
        shouldShow = false;
      }
    }

    if (shouldShow) {
      if (!s_panelShown) {
        SetWindowPos(g_guiHwnd, HWND_TOPMOST, 0, 0, 0, 0,
                     SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        ShowWindow(g_guiHwnd, SW_SHOWNOACTIVATE);
        s_panelShown = true;
      } else if (fg == g_gameHwnd) {
        SetWindowPos(g_guiHwnd, HWND_TOPMOST, 0, 0, 0, 0,
                     SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
      }
    } else {
      if (s_panelShown) {
        SetWindowPos(g_guiHwnd, HWND_NOTOPMOST, 0, 0, 0, 0,
                     SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        ShowWindow(g_guiHwnd, SW_HIDE);
        s_panelShown = false;
      }
    }

    if (!s_panelShown) {
      Sleep(80);
      continue;
    }

    ImGui_ImplDX11_NewFrame();
    ImGui_ImplWin32_NewFrame();
    ImGui::NewFrame();

    DrawMainPanel();

    ImGui::Render();
    const float clear_color[4] = {0.0f, 0.0f, 0.0f, 0.0f};
    g_pd3dDeviceContext->OMSetRenderTargets(1, &g_pMainRenderTargetView,
                                             nullptr);
    g_pd3dDeviceContext->ClearRenderTargetView(g_pMainRenderTargetView,
                                                clear_color);
    ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());

    g_pSwapChain->Present(0, 0);
  }

  Log("[GUI] Shutting down...");
  ImGui_ImplDX11_Shutdown();
  ImGui_ImplWin32_Shutdown();
  ImGui::DestroyContext();

  CleanupDeviceD3D();
  DestroyWindow(g_guiHwnd);
  UnregisterClassW(wc.lpszClassName, wc.hInstance);
  g_guiHwnd = nullptr;

  Log("[GUI] Thread exited cleanly");
  return 0;
}

static void ToggleGui() {
  if (!g_guiHwnd) return;
  g_guiVisible = !g_guiVisible;
  if (g_guiVisible) {
    static bool s_firstShow = true;
    if (s_firstShow && g_gameHwnd) {
      RECT gr;
      GetWindowRect(g_gameHwnd, &gr);
      RECT wr;
      GetWindowRect(g_guiHwnd, &wr);
      int pw = wr.right - wr.left;
      int posX = gr.right - pw - 20;
      int posY = gr.top + 40;
      SetWindowPos(g_guiHwnd, HWND_TOPMOST, posX, posY, 0, 0,
                   SWP_NOSIZE | SWP_NOACTIVATE);
      s_firstShow = false;
    } else {
      SetWindowPos(g_guiHwnd, HWND_TOPMOST, 0, 0, 0, 0,
                   SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
    }
    ShowWindow(g_guiHwnd, SW_SHOW);
    SetForegroundWindow(g_guiHwnd);
    ReleaseCursorToGui();
  } else {
    SetWindowPos(g_guiHwnd, HWND_NOTOPMOST, 0, 0, 0, 0,
                 SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
    ShowWindow(g_guiHwnd, SW_HIDE);
    ReturnCursorToGame();
    if (g_gameHwnd) SetForegroundWindow(g_gameHwnd);
  }
  Log("[GUI] Toggled visibility: %s", g_guiVisible ? "VISIBLE" : "HIDDEN");
}
