#pragma once

#define DUMPER_CONFIG_MAGIC "IL2DUMP"

struct DumperConfig {
    char magic [ 8 ];
    char outputDir [ 260 ];
};
