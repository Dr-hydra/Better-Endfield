#pragma once
#include "ModuleApi.h"
// Optional named Host capability. BE_HostApiV1 stays byte-for-byte unchanged.
// Key = Animator.transform, pinned by the caller; token is never an address.
// Non-preemptive: an active dash wins until it ends, an active VMD wins until stop.
typedef struct BE_PoseLeaseApiV1 {
    uint32_t version;
    uint64_t (BE_CALL* acquire)(const void* root, const char* owner);
    int (BE_CALL* owns)(const void* root, const char* owner, uint64_t token);
    int (BE_CALL* release)(const void* root, const char* owner, uint64_t token);
} BE_PoseLeaseApiV1;
typedef const BE_PoseLeaseApiV1* (BE_CALL* BE_GetPoseLeaseApiV1Fn)(void);
#if defined(__ANDROID__)
// Android compiles all modules into one library, so resolve the shared service
// directly rather than pretending desktop DLL exports exist in libil2cpp.
extern "C" const BE_PoseLeaseApiV1* BE_CALL BetterEndfieldNext_GetPoseLeaseApiV1();
#endif
