#pragma once

#include <cstdint>

namespace BetterEndfield::Voice {

#pragma pack(push, 1)
struct VoiceCatalogHeaderV1 {
    char magic[8];
    std::uint16_t version;
    std::uint16_t language;
    std::uint32_t entry_count;
    std::uint32_t reserved;
    std::uint64_t entry_offset;
    std::uint64_t data_offset;
};

struct VoiceCatalogEntryV1 {
    std::uint32_t source_media_id;
    std::uint32_t target_media_id;
    std::uint64_t data_offset;
    std::uint32_t data_size;
    std::uint32_t reserved;
};
#pragma pack(pop)

static_assert(sizeof(VoiceCatalogHeaderV1) == 36);
static_assert(sizeof(VoiceCatalogEntryV1) == 24);

} // namespace BetterEndfield::Voice
