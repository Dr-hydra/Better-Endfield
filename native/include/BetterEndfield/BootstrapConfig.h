#pragma once

#include <stdint.h>
#include <wchar.h>

#define BETTER_ENDFIELD_BOOTSTRAP_MAGIC "BENDHOST1"
#define BETTER_ENDFIELD_BOOTSTRAP_PATH_CAPACITY 1024u

typedef struct BE_BootstrapConfigV1 {
    char magic[10];
    uint32_t size;
    wchar_t install_root[BETTER_ENDFIELD_BOOTSTRAP_PATH_CAPACITY];
    wchar_t settings_root[BETTER_ENDFIELD_BOOTSTRAP_PATH_CAPACITY];
    wchar_t catalog_root[BETTER_ENDFIELD_BOOTSTRAP_PATH_CAPACITY];
    wchar_t log_root[BETTER_ENDFIELD_BOOTSTRAP_PATH_CAPACITY];
} BE_BootstrapConfigV1;
