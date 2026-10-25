// Micro-benchmark of the BEM 1.3/1.5 morph hot path.
// Copies SampleMorphChannel and the CPU half of UploadMeshPositions verbatim
// from native/modules/custom_model/{bem.cpp,module.cpp} so the numbers describe
// the shipped algorithm, not a rewrite of it.
#include <algorithm>
#include <chrono>
#include <cmath>
#include <cstdio>
#include <cstring>
#include <cstdint>
#include <random>
#include <string>
#include <vector>

struct BemMorphDelta { uint32_t vertex; float dx, dy, dz; };
struct BemMorphFrame { uint32_t tick = 0; bool neutral = false; std::vector<BemMorphDelta> deltas; };
struct BemMorphChannel {
    std::string parameter;
    uint32_t min_tick = 0, max_tick = 0, step = 1, neutral_tick = 0;
    std::vector<BemMorphFrame> frames;
    std::vector<std::pair<uint32_t, float>> settled;
};
struct BemMorphSet {
    std::vector<BemMorphChannel> channels;
    std::vector<uint32_t> vertices;
    uint32_t stream = 0, offset = 0, stride = 0;
    std::vector<uint8_t> base;
};

// ---- verbatim from bem.cpp:1361 ----
void SampleMorphChannel(const BemMorphChannel& channel, const BemMorphSet& set,
                        float range_low, float range_high, float weight,
                        std::vector<float>& accumulated) {
    if (accumulated.size() != set.vertices.size() * 3 || channel.frames.empty()) return;
    std::pair<uint32_t, float> resolved[2]; size_t count = 0;
    if (weight < 0.0f) {
        for (const auto& entry : channel.settled) if (count < 2) resolved[count++] = entry;
    } else {
        const double span = double(channel.max_tick - channel.min_tick);
        const float position = range_low + (range_high - range_low) * (weight < 0.0f ? 0.0f : weight);
        long long tick = static_cast<long long>(channel.min_tick) + std::llround(span * double(position));
        tick = std::clamp<long long>(tick, channel.frames.front().tick, channel.frames.back().tick);
        auto right = std::lower_bound(channel.frames.begin(), channel.frames.end(), static_cast<uint32_t>(tick),
            [](const BemMorphFrame& frame, uint32_t probe) { return frame.tick < probe; });
        if (right == channel.frames.end()) right = std::prev(channel.frames.end());
        if (right->tick == static_cast<uint32_t>(tick)) resolved[count++] = { uint32_t(right - channel.frames.begin()), 1.0f };
        else if (right != channel.frames.begin()) {
            const auto& left = *(right - 1);
            const double blend = double(tick - left.tick) / double(right->tick - left.tick);
            if (blend < 1.0) resolved[count++] = { uint32_t((right - 1) - channel.frames.begin()), float(1.0 - blend) };
            if (blend > 0.0) resolved[count++] = { uint32_t(right - channel.frames.begin()), float(blend) };
        }
    }
    for (size_t i = 0; i < count; ++i) {
        if (resolved[i].second == 0.0f || resolved[i].first >= channel.frames.size()) continue;
        const auto& frame = channel.frames[resolved[i].first];
        if (frame.neutral) continue;
        for (const auto& delta : frame.deltas) {
            const auto local = std::lower_bound(set.vertices.begin(), set.vertices.end(), delta.vertex);
            if (local == set.vertices.end() || *local != delta.vertex) continue;
            const size_t base = size_t(local - set.vertices.begin()) * 3;
            accumulated[base] += resolved[i].second * delta.dx;
            accumulated[base + 1] += resolved[i].second * delta.dy;
            accumulated[base + 2] += resolved[i].second * delta.dz;
        }
    }
}

// ---- CPU half of module.cpp:1044 UploadMeshPositions ----
void GatherPositions(const BemMorphSet& set, const std::vector<float>& accumulated,
                     const std::vector<uint32_t>& touched, std::vector<float>& positions) {
    const size_t vertex_count = set.base.size() / size_t(set.stride);
    positions.resize(vertex_count * 3);
    const uint8_t* base = set.base.data();
    for (size_t v = 0; v < vertex_count; ++v)
        std::memcpy(positions.data() + v * 3, base + v * size_t(set.stride) + set.offset, 12);
    for (size_t i = 0; i < touched.size(); ++i) {
        const size_t index = touched[i];
        if (index >= set.vertices.size()) continue;
        const size_t vertex = set.vertices[index];
        if (vertex >= vertex_count) continue;
        float* position = positions.data() + vertex * 3;
        const float* delta = accumulated.data() + index * 3;
        for (int axis = 0; axis < 3; ++axis) position[axis] += delta[axis];
    }
}

static double NowMs() {
    using namespace std::chrono;
    return duration<double, std::milli>(steady_clock::now().time_since_epoch()).count();
}

struct Case { const char* name; uint32_t vertex_count; std::vector<uint32_t> delta_counts; };

int main() {
    std::mt19937 rng(12345);
    std::vector<Case> cases = {
        {"real mesh0 17.5k v, 1637+180 deltas", 17517, {1637, 180}},
        {"real mesh1 119.9k v, 6358+1434 deltas", 119860, {6358, 1434}},
        {"cap-ish 500k v, 20000 deltas", 500000, {20000}},
        {"worst 1M v, 1M deltas", 1000000, {1000000}},
    };

    for (auto& c : cases) {
        BemMorphSet set;
        set.stride = 16; set.offset = 0; set.stream = 0;
        set.base.assign(size_t(c.vertex_count) * set.stride, 0);
        // union of touched vertices, ascending, deduplicated
        std::vector<uint32_t> all;
        for (auto n : c.delta_counts) {
            std::vector<uint32_t> v(c.vertex_count);
            for (uint32_t i = 0; i < c.vertex_count; ++i) v[i] = i;
            std::shuffle(v.begin(), v.end(), rng);
            v.resize(std::min<size_t>(n, c.vertex_count));
            all.insert(all.end(), v.begin(), v.end());
        }
        std::sort(all.begin(), all.end());
        all.erase(std::unique(all.begin(), all.end()), all.end());
        set.vertices = all;

        for (size_t ci = 0; ci < c.delta_counts.size(); ++ci) {
            BemMorphChannel ch;
            ch.parameter = "p" + std::to_string(ci);
            ch.min_tick = 0; ch.max_tick = 1000;
            BemMorphFrame neutral; neutral.tick = 0; neutral.neutral = true;
            BemMorphFrame hi; hi.tick = 1000;
            std::vector<uint32_t> pick = set.vertices;
            std::shuffle(pick.begin(), pick.end(), rng);
            pick.resize(std::min<size_t>(c.delta_counts[ci], pick.size()));
            std::sort(pick.begin(), pick.end());
            hi.deltas.reserve(pick.size());
            for (uint32_t v : pick) hi.deltas.push_back({ v, 0.01f, 0.0f, 0.0f });
            ch.frames.push_back(std::move(neutral));
            ch.frames.push_back(std::move(hi));
            ch.settled.push_back({ 1, 1.0f });
            set.channels.push_back(std::move(ch));
        }

        std::vector<float> accumulated;
        std::vector<uint32_t> touched;
        std::vector<float> positions;

        const int kFrames = 200;
        // warm up
        for (int f = 0; f < 10; ++f) {
            accumulated.assign(set.vertices.size() * 3, 0.0f);
            for (auto& ch : set.channels) SampleMorphChannel(ch, set, 0.0f, 1.0f, 0.5f, accumulated);
            touched.clear();
            for (size_t i = 0; i < set.vertices.size(); ++i) {
                const float* d = accumulated.data() + i * 3;
                if (d[0] != 0.0f || d[1] != 0.0f || d[2] != 0.0f) touched.push_back(uint32_t(i));
            }
            GatherPositions(set, accumulated, touched, positions);
        }

        double t_accum = 0, t_sample = 0, t_scan = 0, t_gather = 0;
        for (int f = 0; f < kFrames; ++f) {
            double a = NowMs();
            accumulated.assign(set.vertices.size() * 3, 0.0f);
            double b = NowMs(); t_accum += b - a;
            for (auto& ch : set.channels) SampleMorphChannel(ch, set, 0.0f, 1.0f, 0.5f, accumulated);
            double d = NowMs(); t_sample += d - b;
            touched.clear();
            for (size_t i = 0; i < set.vertices.size(); ++i) {
                const float* dd = accumulated.data() + i * 3;
                if (dd[0] != 0.0f || dd[1] != 0.0f || dd[2] != 0.0f) touched.push_back(uint32_t(i));
            }
            double e = NowMs(); t_scan += e - d;
            GatherPositions(set, accumulated, touched, positions);
            double g = NowMs(); t_gather += g - e;
        }
        const double n = kFrames;
        const double upload_mb = double(c.vertex_count) * 12.0 / 1048576.0;
        printf("%-42s touched=%-7zu | accum %6.3f | sample %8.3f | scan %6.3f | gather %7.3f | total %8.3f ms/frame | upload %6.2f MiB/frame (%5.0f MiB/s @60fps)\n",
               c.name, set.vertices.size(), t_accum / n, t_sample / n, t_scan / n, t_gather / n,
               (t_accum + t_sample + t_scan + t_gather) / n, upload_mb, upload_mb * 60.0);
    }
    return 0;
}
