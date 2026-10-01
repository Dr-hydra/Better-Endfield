#include "BetterEndfield/PoseLease.h"
#include "test_support.h"
extern "C" const BE_PoseLeaseApiV1* BE_CALL BetterEndfield_GetPoseLeaseApiV1();
int main() {
    auto api=BetterEndfield_GetPoseLeaseApiV1();CHECK(api&&api->version==1);
    auto root=reinterpret_cast<void*>(1234);
    auto token=api->acquire(root,"camera");CHECK(token);
    CHECK(!api->acquire(root,"actions"));CHECK(api->owns(root,"camera",token));
    CHECK(!api->owns(root,"actions",token));CHECK(!api->release(root,"actions",token));
    CHECK(api->release(root,"camera",token));auto other=api->acquire(root,"actions");CHECK(other&&other!=token);
    CHECK(!api->release(root,"camera",token));CHECK(api->owns(root,"actions",other));
    CHECK(api->release(root,"actions",other));CHECK(!api->release(root,"actions",other));
    CHECK(!api->acquire(nullptr,"camera"));CHECK(!api->acquire(root,nullptr));CHECK(!api->owns(root,nullptr,1));CHECK(!api->release(root,nullptr,1));
    std::cout<<"PASS production Host pose-lease API: "<<checks<<" checks\n";
}
