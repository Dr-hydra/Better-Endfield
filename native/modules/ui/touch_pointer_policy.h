#pragma once

#include "BetterEndfieldNext/ModuleApi.h"

namespace BetterEndfieldNext::UiModule::TouchPointerPolicy {

using LogFn = void (*)(const char*);

// Keep mouse and individual fingers in independent UI pointer records while
// the mobile UI is enabled. All contracts and the enum value use metadata.
bool Start(const BE_HostApiV1* host, LogFn log);
void SetEnabled(bool enabled);
void Stop();

}  // namespace BetterEndfieldNext::UiModule::TouchPointerPolicy
