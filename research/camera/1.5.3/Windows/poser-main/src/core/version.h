// Single source of truth for the version number: both the C++ code and the
// resource file (src/poser.rc VERSIONINFO) read it. Bump the version here only.
//
// NOTE: keep this file ASCII-only and do not use #pragma once -- rc.exe does not
// support #pragma once, and its preprocessor does not handle UTF-8 comments.
// Plain include guards work for both C++ and RC.
#ifndef POSER_VERSION_MAJOR
#define POSER_VERSION_MAJOR 0
#define POSER_VERSION_MINOR 4
#define POSER_VERSION_PATCH 2
#endif
