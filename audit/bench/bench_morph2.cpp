// Headroom measurement: same inputs, four implementations of the morph tick.
// A = shipped (binary search per delta, full gather, full channel upload)
// B = precomputed delta->vertex-slot index (load-time cost, zero per frame)
// C = B + persistent position buffer patched only on touched vertices
// D = C + skip upload when the sampled weight did not change (paused/finished)
#include <algorithm>
#include <chrono>
#include <cmath>
#include <cstdio>
#include <cstring>
#include <cstdint>
#include <random>
#include <string>
#include <vector>

struct D { uint32_t vertex; float dx, dy, dz; uint32_t slot = 0; };
struct F { uint32_t tick = 0; bool neutral = false; std::vector<D> deltas; };
struct C { std::string p; uint32_t min_tick = 0, max_tick = 1000; std::vector<F> frames; };

static double NowMs() {
    using namespace std::chrono;
    return duration<double, std::milli>(steady_clock::now().time_since_epoch()).count();
}

int main() {
    struct Case { const char* name; uint32_t vc; std::vector<uint32_t> deltas; };
    std::vector<Case> cases = {
        {"real mesh1 119.9k v / 6358+1434", 119860, {6358, 1434}},
        {"500k v / 20000", 500000, {20000}},
        {"1M v / 1M", 1000000, {1000000}},
    };
    for (auto& cs : cases) {
        std::mt19937 rng(12345);
        std::vector<uint32_t> verts(cs.vc);
        for (uint32_t i = 0; i < cs.vc; ++i) verts[i] = i;
        std::vector<C> chans;
        std::vector<uint32_t> unionv;
        for (size_t ci = 0; ci < cs.deltas.size(); ++ci) {
            std::shuffle(verts.begin(), verts.end(), rng);
            std::vector<uint32_t> pick(verts.begin(), verts.begin() + std::min<size_t>(cs.deltas[ci], verts.size()));
            std::sort(pick.begin(), pick.end());
            C c; c.p = "p" + std::to_string(ci);
            F n; n.neutral = true; n.tick = 0;
            F h; h.tick = 1000;
            for (uint32_t v : pick) h.deltas.push_back({ v, 0.01f, 0, 0, 0 });
            c.frames = { n, h };
            chans.push_back(std::move(c));
            unionv.insert(unionv.end(), pick.begin(), pick.end());
        }
        std::sort(unionv.begin(), unionv.end());
        unionv.erase(std::unique(unionv.begin(), unionv.end()), unionv.end());
        const size_t V = unionv.size();
        // load-time: delta.vertex -> slot in unionv
        for (auto& c : chans) for (auto& f : c.frames) for (auto& d : f.deltas)
            d.slot = uint32_t(std::lower_bound(unionv.begin(), unionv.end(), d.vertex) - unionv.begin());

        std::vector<uint8_t> base(size_t(cs.vc) * 16, 0);
        for (uint32_t i = 0; i < cs.vc; ++i) {
            float x = float(i), y = float(i) * 0.5f, z = float(i) * 0.25f;
            std::memcpy(base.data() + size_t(i) * 16, &x, 4);
            std::memcpy(base.data() + size_t(i) * 16 + 4, &y, 4);
            std::memcpy(base.data() + size_t(i) * 16 + 8, &z, 4);
        }
        std::vector<float> acc(V * 3), positions(size_t(cs.vc) * 3), prev_acc(V * 3, 0.0f);
        std::vector<uint32_t> touched;

        const int N = 200;
        // A: shipped
        double tA = 0;
        for (int f = 0; f < N + 5; ++f) {
            double s = NowMs();
            acc.assign(V * 3, 0.0f);
            for (auto& c : chans) {
                const auto& fr = c.frames[1];
                for (auto& d : fr.deltas) {
                    auto it = std::lower_bound(unionv.begin(), unionv.end(), d.vertex);
                    if (it == unionv.end() || *it != d.vertex) continue;
                    size_t b = size_t(it - unionv.begin()) * 3;
                    acc[b] += 0.5f * d.dx; acc[b + 1] += 0.5f * d.dy; acc[b + 2] += 0.5f * d.dz;
                }
            }
            touched.clear();
            for (size_t i = 0; i < V; ++i) {
                const float* d = acc.data() + i * 3;
                if (d[0] || d[1] || d[2]) touched.push_back(uint32_t(i));
            }
            positions.resize(size_t(cs.vc) * 3);
            for (size_t v = 0; v < cs.vc; ++v)
                std::memcpy(positions.data() + v * 3, base.data() + v * 16, 12);
            for (size_t i = 0; i < touched.size(); ++i) {
                size_t idx = touched[i], vertex = unionv[idx];
                float* p = positions.data() + vertex * 3;
                const float* d = acc.data() + idx * 3;
                p[0] += d[0]; p[1] += d[1]; p[2] += d[2];
            }
            double e = NowMs();
            if (f >= 5) tA += e - s;
        }
        // B: precomputed slot
        double tB = 0;
        for (int f = 0; f < N + 5; ++f) {
            double s = NowMs();
            acc.assign(V * 3, 0.0f);
            for (auto& c : chans) for (auto& d : c.frames[1].deltas) {
                size_t b = size_t(d.slot) * 3;
                acc[b] += 0.5f * d.dx; acc[b + 1] += 0.5f * d.dy; acc[b + 2] += 0.5f * d.dz;
            }
            touched.clear();
            for (size_t i = 0; i < V; ++i) {
                const float* d = acc.data() + i * 3;
                if (d[0] || d[1] || d[2]) touched.push_back(uint32_t(i));
            }
            positions.resize(size_t(cs.vc) * 3);
            for (size_t v = 0; v < cs.vc; ++v)
                std::memcpy(positions.data() + v * 3, base.data() + v * 16, 12);
            for (size_t i = 0; i < touched.size(); ++i) {
                size_t idx = touched[i], vertex = unionv[idx];
                float* p = positions.data() + vertex * 3;
                const float* d = acc.data() + idx * 3;
                p[0] += d[0]; p[1] += d[1]; p[2] += d[2];
            }
            double e = NowMs();
            if (f >= 5) tB += e - s;
        }
        // C: + persistent positions, patch only touched
        positions.resize(size_t(cs.vc) * 3);
        for (size_t v = 0; v < cs.vc; ++v) std::memcpy(positions.data() + v * 3, base.data() + v * 16, 12);
        double tC = 0;
        for (int f = 0; f < N + 5; ++f) {
            double s = NowMs();
            acc.assign(V * 3, 0.0f);
            for (auto& c : chans) for (auto& d : c.frames[1].deltas) {
                size_t b = size_t(d.slot) * 3;
                acc[b] += 0.5f * d.dx; acc[b + 1] += 0.5f * d.dy; acc[b + 2] += 0.5f * d.dz;
            }
            touched.clear();
            for (size_t i = 0; i < V; ++i) {
                const float* d = acc.data() + i * 3;
                if (d[0] || d[1] || d[2]) touched.push_back(uint32_t(i));
            }
            // write back only vertices whose delta changed
            for (size_t i = 0; i < touched.size(); ++i) {
                size_t idx = touched[i], vertex = unionv[idx];
                float* p = positions.data() + vertex * 3;
                const float* d = acc.data() + idx * 3;
                float* q = prev_acc.data() + idx * 3;
                p[0] += d[0] - q[0]; p[1] += d[1] - q[1]; p[2] += d[2] - q[2];
                q[0] = d[0]; q[1] = d[1]; q[2] = d[2];
            }
            double e = NowMs();
            if (f >= 5) tC += e - s;
        }
        printf("%-34s A %7.3f | B %7.3f | C %7.3f ms/frame  (upload/frame A=B=C %.2f MiB, D 0)\n",
               cs.name, tA / N, tB / N, tC / N, double(cs.vc) * 12.0 / 1048576.0);
    }
    return 0;
}
