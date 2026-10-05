#pragma once
#include <windows.h>
#include <d3d11.h>
#include <cwchar>

// Owns one loader reference on success. Keep it while the overlay uses the
// returned entry point. Never substitute the application's D3D11 import here.
struct OverlaySystemD3D11 {
  HMODULE module = nullptr;
  PFN_D3D11_CREATE_DEVICE create = nullptr;
  DWORD error = ERROR_SUCCESS;
};

static bool OverlaySystemPath(wchar_t (&path)[MAX_PATH]) {
  UINT count = GetSystemDirectoryW(path, MAX_PATH);
  return count && count < MAX_PATH &&
         wcscat_s(path, MAX_PATH, L"\\d3d11.dll") == 0;
}

static HMODULE OverlayCodeModule(const void *address) {
  HMODULE module = nullptr;
  GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                    GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                    reinterpret_cast<LPCWSTR>(address), &module);
  return module;
}

static OverlaySystemD3D11 LoadOverlaySystemD3D11() {
  OverlaySystemD3D11 result;
  wchar_t expected[MAX_PATH] = {}, actual[MAX_PATH] = {};
  if (!OverlaySystemPath(expected)) {
    result.error = ERROR_PATH_NOT_FOUND;
    return result;
  }
  // EFMI's load_library_redirect=2 intercepts even an absolute System32
  // LoadLibraryExW request. Query the already loaded system module first.
  HMODULE module = nullptr;
  if (!GetModuleHandleExW(0, expected, &module))
    module = LoadLibraryExW(expected, nullptr, LOAD_LIBRARY_SEARCH_SYSTEM32);
  if (!module) {
    result.error = GetLastError();
    return result;
  }
  DWORD count = GetModuleFileNameW(module, actual, MAX_PATH);
  if (!count || count >= MAX_PATH || _wcsicmp(expected, actual) != 0) {
    // A redirected request is a compatibility failure, not a usable fallback.
    result.error = ERROR_INVALID_DLL;
    FreeLibrary(module);
    return result;
  }
  auto create = reinterpret_cast<PFN_D3D11_CREATE_DEVICE>(
      GetProcAddress(module, "D3D11CreateDevice"));
  if (!create || OverlayCodeModule(reinterpret_cast<const void *>(create)) != module) {
    result.error = ERROR_PROC_NOT_FOUND;
    FreeLibrary(module);
    return result;
  }
  result.module = module;
  result.create = create;
  return result;
}

static bool OverlaySystemCode(const void *address) {
  wchar_t path[MAX_PATH] = {}, system[MAX_PATH] = {};
  HMODULE module = OverlayCodeModule(address);
  UINT length = GetSystemDirectoryW(system, MAX_PATH);
  DWORD count = module ? GetModuleFileNameW(module, path, MAX_PATH) : 0;
  return length && length < MAX_PATH && count > length && count < MAX_PATH &&
         _wcsnicmp(path, system, length) == 0 && path[length] == L'\\';
}

// An inline hook on the system factory can still return a proxy. Check the
// actual COM methods before passing objects to ImGui or submitting a draw.
static bool OverlayDeviceIsUnwrapped(ID3D11Device *device,
                                     ID3D11DeviceContext *context) {
  if (!device || !context) return false;
  auto deviceMethods = *reinterpret_cast<void ***>(device);
  auto contextMethods = *reinterpret_cast<void ***>(context);
  // Slots from the SDK's ID3D11Device / ID3D11DeviceContext interfaces:
  // CreateBuffer, CreateTexture2D; PSSetShaderResources, DrawIndexed, Map.
  return OverlaySystemCode(deviceMethods[3]) && OverlaySystemCode(deviceMethods[5]) &&
         OverlaySystemCode(contextMethods[8]) && OverlaySystemCode(contextMethods[12]) &&
         OverlaySystemCode(contextMethods[14]);
}
