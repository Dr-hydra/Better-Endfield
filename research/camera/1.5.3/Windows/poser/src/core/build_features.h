// Shared by C++ and rc.exe. Keep this header ASCII-only.
#ifndef POSER_BUILD_FEATURES_H
#define POSER_BUILD_FEATURES_H

#ifndef POSER_ENABLE_XXMI_BRIDGE
#define POSER_ENABLE_XXMI_BRIDGE 0
#endif
#if POSER_ENABLE_XXMI_BRIDGE != 0 && POSER_ENABLE_XXMI_BRIDGE != 1
#error POSER_ENABLE_XXMI_BRIDGE must be 0 or 1
#endif

#if POSER_ENABLE_XXMI_BRIDGE
#define POSER_BUILD_FEATURES "EndfieldPoser:xxmi_bridge=1"
#else
#define POSER_BUILD_FEATURES "EndfieldPoser:xxmi_bridge=0"
#endif

#endif
