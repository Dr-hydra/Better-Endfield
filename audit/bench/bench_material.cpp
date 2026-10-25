// Floor cost of one driver material write, excluding the Unity call itself:
// Contract() linear scan + FNV key + unordered_map lookup + the managed-string
// stand-in allocation. Measures the bookkeeping the module adds per property.
#include <chrono>
#include <cstdio>
#include <cstdint>
#include <string>
#include <unordered_map>
#include <vector>

struct MethodContract { std::string key; int resolved = 1; };
static std::vector<MethodContract> g_methods;

static double NowUs() {
    using namespace std::chrono;
    return duration<double, std::micro>(steady_clock::now().time_since_epoch()).count();
}

static MethodContract* Contract(const std::string& key) {
    for (auto& m : g_methods) if (m.key == key) return &m;
    return nullptr;
}

static uint64_t Key(uint8_t kind, uint32_t comp, uint32_t slot, const std::string& name) {
    uint64_t h = 1469598103934665603ull;
    auto mix = [&h](uint64_t v) { for (int i = 0; i < 8; ++i) { h ^= (v >> (i * 8)) & 0xffu; h *= 1099511628211ull; } };
    mix(kind); mix(comp); mix(slot);
    for (unsigned char c : name) { h ^= c; h *= 1099511628211ull; }
    return h;
}

int main() {
    for (int i = 0; i < 135; ++i) g_methods.push_back({ "contract.key.number." + std::to_string(i) });
    g_methods.push_back({ "material.set_float" });

    std::unordered_map<uint64_t, int> writes;
    const int kN = 512;   // worst case: 16 drivers x 32 bindings
    const int kFrames = 2000;
    std::vector<std::string> names;
    for (int i = 0; i < kN; ++i) names.push_back("_Property" + std::to_string(i));

    double t = 0;
    for (int f = 0; f < kFrames; ++f) {
        double s = NowUs();
        for (int i = 0; i < kN; ++i) {
            auto* c = Contract("material.set_float");        // 136 string compares
            const uint64_t k = Key(0, 0, 0, names[i]);
            auto [it, ins] = writes.try_emplace(k);
            std::string managed = names[i];                   // NewString stand-in
            if (c && it != writes.end()) it->second += 1;
        }
        t += NowUs() - s;
    }
    printf("bookkeeping per frame (512 writes): %.1f us  => %.2f us/write (excludes MethodInfo.Invoke)\n",
           t / kFrames, t / kFrames / kN);
    return 0;
}
