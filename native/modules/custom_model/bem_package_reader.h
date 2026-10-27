#pragma once
#include "bem.h"
#include "../../shared/third_party/nlohmann/json.hpp"
#include <functional>
#include <ostream>

namespace BetterEndfieldNext::CustomModel {
// Installer/authoring API. Holds the source open, reuses the runtime container
// parser, and never modifies the source. Composition preserves discrete logic;
// Snapshot explicitly resolves it. Payload memory is bounded to one payload.
class BemPackageReader {
public:
    BemPackageReader();
    ~BemPackageReader();
    BemPackageReader(const BemPackageReader&) = delete;
    BemPackageReader& operator=(const BemPackageReader&) = delete;
    bool Open(const std::filesystem::path&, std::string& error);
    const BemPackageInfo& Info() const;
    bool Snapshot(std::string_view selection, nlohmann::json&, std::string& error);
    // Preserve all discrete rules; selection only initializes saved defaults.
    bool Composition(std::string_view selection, nlohmann::json&, std::string& error);
    bool ReadPayload(uint32_t id, uint64_t expected_size, std::vector<uint8_t>&, std::string& error);
    bool CopyPayload(uint32_t id, std::ostream&, uint32_t& codec,
        uint64_t& stored, uint64_t& decoded,
        const std::function<void()>& checkpoint, std::string& error);
private:
    struct Impl;
    std::unique_ptr<Impl> impl_;
};
}
