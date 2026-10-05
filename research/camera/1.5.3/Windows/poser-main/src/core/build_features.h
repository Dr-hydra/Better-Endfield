#ifndef POSER_BUILD_FEATURES_H
#define POSER_BUILD_FEATURES_H

// Shared by C++ and rc.exe. Source builds must explicitly opt into this backend.
#ifndef POSER_ENABLE_LAYERED_OVERLAY
#define POSER_ENABLE_LAYERED_OVERLAY 0
#endif

#if POSER_ENABLE_LAYERED_OVERLAY != 0 && POSER_ENABLE_LAYERED_OVERLAY != 1
#error POSER_ENABLE_LAYERED_OVERLAY must be 0 or 1
#endif

#if POSER_ENABLE_LAYERED_OVERLAY
#define POSER_BUILD_DESCRIPTION "Endfield Poser - in-game posing plugin [layered-overlay=1]"
#else
#define POSER_BUILD_DESCRIPTION "Endfield Poser - in-game posing plugin [layered-overlay=0]"
#endif

#endif
