#pragma once
#define ANDROID_LOG_WARN 5
#define ANDROID_LOG_ERROR 6
extern "C" int __android_log_print(int,const char*,const char*,...);
