#pragma once
#include <atomic>
namespace poser_agreement {
inline constexpr int kRevision = 1;
// Published after reading/writing upstream's terms_version configuration.
inline std::atomic<bool> accepted{false};
inline bool Allowed() { return accepted.load(); }
}
