#pragma once
#include <string>
#include "BetterEndfieldNext/LocalMusic.h"
namespace betterendfieldnext {
// JNI only queues commands. All Unity work is consumed on the observed frame thread.
bool AndroidMmdCommand(unsigned type, int argument, double value, const std::string& text);
std::string AndroidMmdStatus();
void AndroidCameraValues(float speed, float fov);
std::string AndroidCameraValuesStatus();
const BE_LocalMusicApiV1* AndroidLocalMusicApi();
}
