#include "BetterEndfieldNext/PoseLease.h"
#include "../motion/pose_lease_registry.h"
namespace {
BetterEndfieldNext::Motion::PoseLeaseRegistry registry;
uint64_t BE_CALL Acquire(const void* root,const char* owner) {
    try { return owner ? registry.Acquire(root,owner) : 0; } catch(...) {return 0;}
}
int BE_CALL Owns(const void* root,const char* owner,uint64_t token) {
    try { return owner && registry.Owns(root,owner,token); } catch(...) {return 0;}
}
int BE_CALL Release(const void* root,const char* owner,uint64_t token) {
    try { return owner && registry.Release(root,owner,token); } catch(...) {return 0;}
}
const BE_PoseLeaseApiV1 api{1,&Acquire,&Owns,&Release};
}
BE_EXPORT const BE_PoseLeaseApiV1* BE_CALL BetterEndfieldNext_GetPoseLeaseApiV1() {return &api;}
