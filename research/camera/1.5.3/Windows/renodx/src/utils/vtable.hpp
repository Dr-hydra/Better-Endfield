/*
 * Copyright (C) 2025 Carlos Lopez
 * SPDX-License-Identifier: MIT
 */

#pragma once

#include <rpc.h>
#include <rpcndr.h>
#include <Windows.h>

#include <concepts>
#include <cstddef>
#include <ranges>
#include <stdexcept>
#include <system_error>
#include <tuple>
#include <type_traits>
#include <vector>

#include "./detour.hpp"
#include "./log.hpp"

namespace renodx::utils::vtable {

template <typename FunctionPointer>
  requires std::is_pointer_v<FunctionPointer>
           && std::is_function_v<std::remove_pointer_t<FunctionPointer>>
struct Slot {
  void* object = nullptr;
  std::size_t index = 0u;
  FunctionPointer* original = nullptr;
  FunctionPointer replacement = nullptr;
  void** installed_address = nullptr;
};

namespace internal {

inline void Replace(void** address, void* expected, void* replacement) {
  DWORD old_protection = 0u;
  if (VirtualProtect(address, sizeof(*address), PAGE_READWRITE, &old_protection) == FALSE) {
    throw std::system_error(static_cast<int>(GetLastError()), std::system_category(), "VirtualProtect");
  }

  void* observed = InterlockedCompareExchangePointer(
      reinterpret_cast<void* volatile*>(address), replacement, expected);

  DWORD ignored = 0u;
  if (VirtualProtect(address, sizeof(*address), old_protection, &ignored) == FALSE) {
    const DWORD restore_error = GetLastError();
    if (observed == expected) {
      (void)InterlockedCompareExchangePointer(
          reinterpret_cast<void* volatile*>(address), expected, replacement);
    }
    (void)VirtualProtect(address, sizeof(*address), old_protection, &ignored);
    throw std::system_error(static_cast<int>(restore_error), std::system_category(), "VirtualProtect");
  }
  if (observed != expected) {
    throw std::runtime_error("vtable slot changed during replacement");
  }
}

}  // namespace internal

/// Replaces one shared vtable slot and captures its original function.
/// @throws std::invalid_argument If the slot is invalid or already installed.
/// @throws std::system_error If the slot's page protection cannot be changed.
/// @throws std::runtime_error If another writer changes the slot concurrently.
template <typename FunctionPointer>
  requires std::is_pointer_v<FunctionPointer>
           && std::is_function_v<std::remove_pointer_t<FunctionPointer>>
void Install(Slot<FunctionPointer>* slot) {
  if (slot == nullptr
      || slot->object == nullptr
      || slot->original == nullptr
      || slot->replacement == nullptr) {
    throw std::invalid_argument("invalid vtable slot");
  }
  if (slot->installed_address != nullptr || *slot->original != nullptr) {
    throw std::invalid_argument("vtable slot is already installed");
  }

  auto** table = *reinterpret_cast<void***>(slot->object);
  if (table == nullptr) throw std::invalid_argument("vtable is null");

  void** address = table + slot->index;
  void* original = *address;
  void* replacement = reinterpret_cast<void*>(slot->replacement);
  if (original == nullptr || original == replacement) {
    throw std::invalid_argument("invalid vtable slot function");
  }

  internal::Replace(address, original, replacement);
  *slot->original = reinterpret_cast<FunctionPointer>(original);
  slot->installed_address = address;
}

/// Restores one shared vtable slot if it still contains this replacement.
/// @throws std::invalid_argument If the slot is invalid or not installed.
/// @throws std::system_error If the slot's page protection cannot be changed.
/// @throws std::runtime_error If another writer has replaced the slot.
template <typename FunctionPointer>
  requires std::is_pointer_v<FunctionPointer>
           && std::is_function_v<std::remove_pointer_t<FunctionPointer>>
void Uninstall(Slot<FunctionPointer>* slot) {
  if (slot == nullptr
      || slot->original == nullptr
      || slot->replacement == nullptr) {
    throw std::invalid_argument("invalid vtable slot");
  }
  if (slot->installed_address == nullptr || *slot->original == nullptr) {
    throw std::invalid_argument("vtable slot is not installed");
  }

  internal::Replace(
      slot->installed_address,
      reinterpret_cast<void*>(slot->replacement),
      reinterpret_cast<void*>(*slot->original));
  *slot->original = nullptr;
  slot->installed_address = nullptr;
}

using HookItem [[deprecated("vtable::HookItem resolves exports and installs detours; use detour::Export")]] =
    std::tuple<const char*, void**, void*>;

template <std::ranges::range Hooks>
  requires std::convertible_to<std::ranges::range_value_t<Hooks>, std::tuple<const char*, void**, void*>>
[[deprecated("vtable::Hook resolves exports and installs detours; use detour::Install")]]
static bool Hook(HMODULE module, const Hooks& hooks) {
  if (module == nullptr) {
    log::e("vtable::Hook(Module not loaded.)");
    return false;
  }

  std::vector<detour::Export> exports;
  for (const auto& [name, original, replacement] : hooks) {
    exports.emplace_back(name, original, replacement);
  }

  try {
    const detour::ExportInstallResult result = detour::Install(module, exports);
    if (result.error) {
      log::e("vtable::Hook(", result.error.message(), ")");
    }
    return result.Active() != 0u;
  } catch (const std::exception& error) {
    log::e("vtable::Hook(", error.what(), ")");
    return false;
  }
}

template <std::ranges::range Hooks>
  requires std::convertible_to<std::ranges::range_value_t<Hooks>, std::tuple<const char*, void**, void*>>
[[deprecated("vtable::Unhook removes export detours; use detour::Uninstall")]]
static void Unhook(HMODULE module, const Hooks& hooks) {
  if (module == nullptr) {
    log::w("vtable::Unhook(DLL already loaded)");
  }

  std::vector<detour::Export> exports;
  for (const auto& [name, original, replacement] : hooks) {
    exports.emplace_back(name, original, replacement);
  }

  try {
    detour::Uninstall(exports);
  } catch (const std::exception& error) {
    log::e("vtable::Unhook(", error.what(), ")");
  }
}

}  // namespace renodx::utils::vtable