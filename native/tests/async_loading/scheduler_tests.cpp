#include "../../modules/custom_model/async_loading.h"
#include "../../shared/third_party/nlohmann/json.hpp"
#include <atomic>
#include <cstring>
#include <fstream>
#include <iostream>
#include <map>
#include <set>
using namespace BetterEndfield::CustomModel;
using namespace std::chrono_literals;
namespace {
int checks = 0;
void Check(bool ok, const std::string& message) { ++checks; if (!ok) throw std::runtime_error(message); }
template<class F> void Await(F condition, const char* message) {
    const auto until = std::chrono::steady_clock::now() + 5s;
    while (!condition()) {
        if (std::chrono::steady_clock::now() >= until) throw std::runtime_error(message);
        std::this_thread::sleep_for(1ms);
    }
}
BemRequest RequestFor(std::string key, uint64_t generation = 1, LoadPriority priority = LoadPriority::Foreground) {
    BemRequest r; r.key = std::move(key); r.revision = "r1"; r.generation = generation; r.priority = priority; return r;
}
struct ControlledBackend {
    std::mutex mutex;
    std::condition_variable cv;
    std::map<std::string, uint64_t> sizes;
    std::map<std::string, int> plans, loads;
    std::set<std::string> released;
    std::vector<std::string> order;
    int active = 0, peak = 0;
    bool all = false, blockPlan = false;
    AsyncBemLoader::Backend Make() {
        return {
            [this](const BemRequest& r, BemLoadPlan& p, std::string&) {
                std::unique_lock lock(mutex);
                ++plans[r.key]; order.push_back("plan:" + r.key);
                if (blockPlan) cv.wait(lock, [&] { return all || released.contains(r.key); });
                p.reservation_bytes = sizes.contains(r.key) ? sizes[r.key] : 40;
                return true;
            },
            [this](const BemRequest& r, uint64_t bound, BemPocData& data, BemLoadStats&, std::string&) {
                std::unique_lock lock(mutex);
                ++loads[r.key]; order.push_back("load:" + r.key); peak = std::max(peak, ++active); cv.notify_all();
                cv.wait(lock, [&] { return all || released.contains(r.key); });
                --active;
                if (!bound) throw std::runtime_error("Missing decode reservation");
                data.textures.resize(1); data.textures[0].data.assign(16, 7);
                return true;
            }
        };
    }
    ~ControlledBackend() { ReleaseAll(); }
    void Release(const std::string& key) { std::lock_guard lock(mutex); released.insert(key); cv.notify_all(); }
    void ReleaseAll() { std::lock_guard lock(mutex); all = true; cv.notify_all(); }
    int Loads(const std::string& key) { std::lock_guard lock(mutex); return loads[key]; }
};
// Ensure a failed assertion never leaves a gated worker waiting during teardown.
struct Unblock { ControlledBackend& b; ~Unblock() { b.ReleaseAll(); } };
void ConcurrencyAndMerge() {
    ControlledBackend backend;
    AsyncBemLoader loader({100, 9, 32}, backend.Make()); Unblock unblock{backend};
    auto world = loader.Request(RequestFor("same", 1));
    auto ui = loader.Request(RequestFor("same", 2));
    auto other = loader.Request(RequestFor("other", 3));
    auto third = loader.Request(RequestFor("third", 4));
    Await([&] { return backend.Loads("same") && backend.Loads("other"); }, "Two small decodes did not start");
    auto counts = loader.Snapshot();
    Check(counts.active_workers == 2 && counts.reserved_decoded == 80, "Worker cap/reservations differ");
    Check(backend.Loads("same") == 1 && backend.Loads("third") == 0, "Duplicate decode or third concurrent decode");
    loader.CancelGeneration(1);
    Check(loader.Poll(world).status == AsyncLoadStatus::Cancelled, "Cancelled generation still polls ready");
    backend.Release("same"); backend.Release("other");
    Await([&] { return loader.Poll(ui).status == AsyncLoadStatus::Ready && loader.Poll(other).status == AsyncLoadStatus::Ready; }, "Small results missing");
    auto uiResult = loader.Poll(ui).result;
    Check(uiResult->data.textures[0].data.size() == 16, "Shared output lifetime lost");
    Check(loader.Snapshot().live_decoded == 80 && backend.Loads("third") == 0, "Held results escaped budget");
    loader.Cancel(ui); ui = {}; world = {};
    Check(loader.Snapshot().live_decoded == 80, "External result reference refunded early");
    uiResult.reset();
    Await([&] { return backend.Loads("third") == 1; }, "Last result release did not wake budget waiter");
    backend.ReleaseAll();
    Await([&] { return loader.Poll(third).status == AsyncLoadStatus::Ready; }, "Third result missing");
    Check(backend.peak <= 2 && loader.Snapshot().peak_decoded <= 100, "Small budget exceeded");
}
void ExclusiveLargeAndLateCancellation() {
    ControlledBackend backend;
    for (int i = 0; i != 4; ++i) backend.sizes["big" + std::to_string(i)] = 400 * kLoadingMiB;
    AsyncBemLoader loader({}, backend.Make()); Unblock unblock{backend};
    std::vector<AsyncBemLoader::Ticket> tickets;
    tickets.push_back(loader.Request(RequestFor("big0", 1)));
    Await([&] { return backend.Loads("big0") == 1; }, "Legal >256MiB package did not progress");
    for (int i = 1; i != 4; ++i) tickets.push_back(loader.Request(RequestFor("big" + std::to_string(i), i + 1)));
    Check(loader.Snapshot().large_exclusive && loader.Snapshot().active_workers == 1, "Large job not exclusive");
    backend.Release("big0");
    Await([&] { return loader.Poll(tickets[0]).status == AsyncLoadStatus::Ready; }, "Large result missing");
    auto result = loader.Poll(tickets[0]).result;
    loader.Cancel(tickets[0]); tickets[0] = {};
    Check(loader.Snapshot().live_decoded == 400 * kLoadingMiB && loader.Snapshot().large_exclusive,
        "Ready large result refunded before consumer release");
    Check(backend.Loads("big1") + backend.Loads("big2") + backend.Loads("big3") == 0, "Four packages decoded together");
    result.reset();
    Await([&] { return backend.Loads("big1") == 1; }, "Next exclusive package starved");
    loader.CancelGeneration(2);
    Check(loader.Snapshot().reserved_decoded == 400 * kLoadingMiB, "Cancellation refunded active decompression");
    backend.Release("big1");
    Await([&] { return backend.Loads("big2") == 1; }, "Stale completion did not free exclusive lease");
    Check(loader.Poll(tickets[1]).status == AsyncLoadStatus::Cancelled && loader.Snapshot().stale_results == 1,
        "Late cancelled result was published");
    loader.CancelGeneration(3); loader.CancelGeneration(4); backend.ReleaseAll();
    Await([&] { return loader.Snapshot().reserved_decoded == 0; }, "Stale reservation leaked");
    Check(backend.peak == 1, "Large workers overlapped");
}
void FairnessAndIdentity() {
    ControlledBackend backend; backend.blockPlan = true;
    AsyncBemLoader loader({1000, 1, 64}, backend.Make()); Unblock unblock{backend};
    std::vector<AsyncBemLoader::Ticket> tickets;
    tickets.push_back(loader.Request(RequestFor("gate")));
    Await([&] { std::lock_guard lock(backend.mutex); return backend.plans["gate"] == 1; }, "Plan gate missing");
    for (int i = 0; i != 16; ++i) tickets.push_back(loader.Request(RequestFor("f" + std::to_string(i))));
    for (int i = 0; i != 3; ++i) tickets.push_back(loader.Request(RequestFor("b" + std::to_string(i), 1,
        i == 0 ? LoadPriority::Prewarm : LoadPriority::Visible)));
    auto changed = RequestFor("f0"); changed.revision = "r2";
    tickets.push_back(loader.Request(changed));
    backend.ReleaseAll();
    Await([&] { for (const auto& t : tickets) if (loader.Poll(t).status != AsyncLoadStatus::Ready) return false; return true; }, "Fair queue did not complete");
    std::lock_guard lock(backend.mutex);
    std::vector<size_t> backgroundOpportunities;
    for (size_t i = 0; i < backend.order.size(); ++i)
        if (backend.order[i].substr(5, 1) == "b") backgroundOpportunities.push_back(i);
    Check(backgroundOpportunities.size() == 6, "Background role work missing");
    Check(backgroundOpportunities[2] <= 11, "Three background roles did not receive a chance in twelve quanta");
    Check(backend.order[backgroundOpportunities[0]].ends_with("b0"), "Prewarm starved behind visible requests");
    Check(backend.loads["f0"] == 2, "Different immutable revision incorrectly merged");
}
void ResourceSelectionIdentity() {
    ControlledBackend backend;backend.blockPlan=true;
    AsyncBemLoader loader({1000,1,64},backend.Make());Unblock unblock{backend};
    auto body=RequestFor("same-package");body.resource_id="body";
    auto weapon=body;weapon.resource_id="weapon";
    auto first=loader.Request(body);auto second=loader.Request(weapon);auto duplicate=loader.Request(body);
    backend.ReleaseAll();
    Await([&]{return loader.Poll(first).status==AsyncLoadStatus::Ready && loader.Poll(second).status==AsyncLoadStatus::Ready &&
        loader.Poll(duplicate).status==AsyncLoadStatus::Ready;},"resource-selected loads did not complete");
    Check(backend.Loads("same-package")==2,"same-package resources shared incompatible decoded component plans");
    Check(loader.Poll(first).result==loader.Poll(duplicate).result && loader.Poll(first).result!=loader.Poll(second).result,
        "resource identity did not isolate decoded results while merging identical subscribers");
}
void FrameAdmission() {
    FrameBudget budget;
    auto first = budget.TryBegin(10, {16 * kLoadingMiB, 1, true, true});
    Check(bool(first), "First texture denied");
    Check(!budget.TryBegin(11, {1, 1, true, true}), "Active permit allowed next frame/reentrant call");
    first.Finish(100us);
    Check(!budget.TryBegin(10, {1, 1, true, true}), "Repeated frame received a second texture permit");
    Check(!budget.TryBegin(9, {}), "Old frame reset budget");
    auto light = budget.TryBegin(10, {0, 1, false, false}); Check(bool(light), "Light step denied"); light.Finish(3ms);
    Check(!budget.TryBegin(10, {}), "Elapsed soft time ignored");
    auto large = budget.TryBegin(11, {64 * kLoadingMiB, 1, true, true}); Check(bool(large), "64MiB progress escape missing"); large.Finish(8ms);
    Check(budget.Snapshot().oversize && budget.Snapshot().byte_debt == 32 * kLoadingMiB, "Oversize debt not recorded");
    Check(!budget.TryBegin(11, {}), "Oversized frame accepted additional work");
    Check(!budget.TryBegin(100, {1, 1, true, true}), "Skipped frame IDs bypassed cooldown");
    Check(!budget.TryBegin(101, {1, 1, true, true}), "Second cooldown frame accepted upload");
    Check(!budget.TryBegin(102, {1, 1, true, true}), "Debt bypassed cooldown");
    auto next = budget.TryBegin(103, {16 * kLoadingMiB, 1, true, true}); Check(bool(next), "Debt never repaid"); next.Finish(100us);
    Check(!budget.TryBegin(104, {65 * kLoadingMiB, 1, true, true}), "Above 64MiB escape accepted");
    auto small = budget.TryBegin(104, {0, 1, false, false}); Check(bool(small), "Next frame light missing"); small.Finish(0ns);
    Check(!budget.TryBegin(104, {64 * kLoadingMiB, 1, true, true}), "Oversize not exclusive on dirty frame");
    FrameBudget steps({16 * kLoadingMiB, 64 * kLoadingMiB, 2, 0, 2ms});
    auto two = steps.TryBegin(1, {0, 2, false, false}); Check(bool(two), "Step budget rejected exact bound"); two.Finish(0ns);
    Check(!steps.TryBegin(1, {}), "Steps exceeded frame cap");
}
void PriorityUpdateAndQueuedCancellation() {
    ControlledBackend backend; backend.blockPlan = true;
    AsyncBemLoader loader({1000,1,8}, backend.Make()); Unblock unblock{backend};
    auto gate = loader.Request(RequestFor("gate"));
    Await([&] { std::lock_guard lock(backend.mutex); return backend.plans["gate"] == 1; }, "Priority plan gate missing");
    auto low = loader.Request(RequestFor("low", 1, LoadPriority::Prewarm));
    auto visible = loader.Request(RequestFor("visible", 2, LoadPriority::Prewarm));
    auto cancel = loader.Request(RequestFor("cancel", 3));
    loader.SetPriority(visible, LoadPriority::Visible); loader.Cancel(cancel);
    backend.ReleaseAll();
    Await([&] { return loader.Poll(gate).status == AsyncLoadStatus::Ready &&
        loader.Poll(low).status == AsyncLoadStatus::Ready && loader.Poll(visible).status == AsyncLoadStatus::Ready; },
        "Priority updated queue did not complete");
    std::lock_guard lock(backend.mutex);
    Check(backend.order.size() == 6 && backend.order[2] == "plan:visible", "Visible priority was ignored without foreground work");
    Check(backend.plans["cancel"] == 0 && backend.loads["cancel"] == 0, "Queued cancellation still executed CPU work");
    Check(loader.Poll(cancel).status == AsyncLoadStatus::Cancelled, "Queued cancellation still publishes");
}
void FailureAndShutdownOwnership() {
    AsyncBemLoader::Backend backend{
        [](const BemRequest& r, BemLoadPlan& p, std::string& error) {
            if (r.key == "plan-fail") { error = "Invalid metadata"; return false; }
            p.reservation_bytes = 40; return true;
        },
        [](const BemRequest& r, uint64_t, BemPocData& data, BemLoadStats&, std::string&) {
            data.textures.resize(1); data.textures[0].data.assign(16, 9);
            if (r.key == "load-fail") throw std::runtime_error("Decoder failure after allocation");
            return true;
        }
    };
    auto loader = std::make_unique<AsyncBemLoader>(AsyncBemLoader::Config{100,1,8}, backend);
    auto planFail = loader->Request(RequestFor("plan-fail"));
    auto loadFail = loader->Request(RequestFor("load-fail"));
    auto good = loader->Request(RequestFor("good"));
    Await([&] { return loader->Poll(planFail).status == AsyncLoadStatus::Failed &&
        loader->Poll(loadFail).status == AsyncLoadStatus::Failed && loader->Poll(good).status == AsyncLoadStatus::Ready; },
        "Failure did not unblock a later successful request");
    Check(loader->Snapshot().reserved_decoded == 0 && loader->Snapshot().live_decoded == 40,
        "Failed decoder leaked reservation or published partial data");
    Check(loader->Poll(loadFail).error == "Decoder failure after allocation", "Worker exception lost diagnostic");
    auto result = loader->Poll(good).result;
    loader->Shutdown();
    Check(loader->Poll(good).status == AsyncLoadStatus::Cancelled, "Shutdown still publishes output");
    Check(loader->Snapshot().live_decoded == 40, "Shutdown invalidated external decoded owner");
    loader.reset();
    Check(result->data.textures[0].data[0] == 9, "Result did not outlive queue shutdown");
    good = {}; planFail = {}; loadFail = {}; result.reset();
}

#pragma pack(push,1)
struct Header { char magic[8]; uint16_t major, minor; uint32_t size; uint64_t file, manifest; uint32_t count, flags; };
struct Entry { uint32_t codec, reserved; uint64_t offset, stored, decoded; };
#pragma pack(pop)
struct Package {
    std::filesystem::path path = std::filesystem::temp_directory_path() /
        ("bem-async-" + std::to_string(std::chrono::steady_clock::now().time_since_epoch().count()) + ".bem");
    std::vector<std::vector<uint8_t>> payloads{std::vector<uint8_t>(36), std::vector<uint8_t>(24), std::vector<uint8_t>(12), {0,0,1,0,2,0}};
    Package() { Write(); }
    ~Package() { std::error_code ignored; std::filesystem::remove(path, ignored); }
    void Write() {
        using J = nlohmann::json;
        J manifest = {{"schema",1},{"package_id","bem.async.test"},{"name","Async"},{"author","Tests"},{"version","1"},
            {"required_capabilities",J::array({"composable-options"})},
            {"target",{{"platform","windows-x64"},{"character_id","chr_test"},{"profile_id","test"},{"revision","r1"},
                {"world_resource","chr_test_postmodel"},{"ui_resource","chr_test_uimodel"},
                {"components",J::array({{{"id",0},{"mesh_name","Body"},{"original_index_count",3},
                    {"bone_names",J::array({"root"})},{"materials",J::array({"body"})}}})}}},
            {"option_groups",J::array({{{"id","base"},{"name","Base"},{"default","on"},
                {"choices",J::array({{{"id","on"},{"name","On"}},{{"id","off"},{"name","Off"}}})}}})},
            {"component_rules",J::array({{{"target",0},{"candidates",J::array({{{"operation","replace"},{"mesh",0}}})}}})},
            {"meshes",J::array({{{"vertex_count",3},{"index_size",2},
                {"streams",J::array({{{"payload",0},{"stride",12}},{{"payload",1},{"stride",8}},{{"payload",2},{"stride",4}}})},
                {"attributes",J::array({J::array({0,0,3,0,0}),J::array({4,0,2,1,0}),J::array({13,6,4,2,0})})},
                {"bones",J::array({{{"component",0},{"index",0},{"name","root"}}})},
                {"draws",J::array({{{"indices",3},{"count",3},{"material_component",0},{"material_slot",0},{"material_name","body"},{"textures",J::array()}}})}}})},
            {"textures",J::array()}};
        const auto json = manifest.dump(); Header h{}; std::memcpy(h.magic, "BEM\0PKG\0", 8);
        h.major = 1; h.minor = 1; h.size = sizeof(h); h.manifest = json.size(); h.count = uint32_t(payloads.size());
        uint64_t offset = sizeof(h) + json.size() + payloads.size() * sizeof(Entry);
        std::vector<Entry> entries;
        for (const auto& p : payloads) { entries.push_back({0,0,offset,p.size(),p.size()}); offset += p.size(); }
        h.file = offset; std::ofstream out(path, std::ios::binary | std::ios::trunc);
        out.write(reinterpret_cast<const char*>(&h), sizeof(h)); out.write(json.data(), json.size());
        out.write(reinterpret_cast<const char*>(entries.data()), entries.size() * sizeof(Entry));
        for (const auto& p : payloads) out.write(reinterpret_cast<const char*>(p.data()), p.size());
        Check(bool(out), "Cannot write BEM fixture");
    }
};
void ProductionCpuIntegration() {
    Package fixture; BemLoadPlan plan; std::string error; BemPocData data;
    Check(ReadBemLoadPlan(fixture.path, plan, error), error);
    Check(plan.decoded_payload_bytes == 78 && plan.reservation_bytes == 156 && plan.payload_ids.size() == 4,
        "Selected backing plan differs");
    Check(!LoadBem(fixture.path, data, error, {}, nullptr, false, true, {}, 155) && data.components.empty(),
        "Bounded load ignored pre-decode reservation");
    Check(LoadBem(fixture.path, data, error, {}, nullptr, false, true, {}, 156), error);
    Check(data.components[0].indices == fixture.payloads[3], "Bounded optimized output changed");
    AsyncBemLoader loader;
    auto r = RequestFor("real", 10); r.path = fixture.path; r.loading_optimization = true;
    auto world = loader.Request(r); r.generation = 11; auto ui = loader.Request(r);
    Await([&] { return loader.Poll(world).status != AsyncLoadStatus::Pending; }, "Production CPU load timed out");
    const auto w = loader.Poll(world), u = loader.Poll(ui);
    Check(w.status == AsyncLoadStatus::Ready, w.error);
    Check(w.result == u.result && w.result->data.components[0].streams[0] == fixture.payloads[0], "Production world/UI did not share CPU output");
    Check(w.result->stats.decoded_cache_remaining_bytes == 0 && loader.Snapshot().live_decoded == 156,
        "Production decoded accounting differs");
    // Structurally valid directory whose declaration disagrees with geometry.
    fixture.payloads[0].resize(1); fixture.Write();
    Check(!ReadBemLoadPlan(fixture.path, plan, error), "Malformed selected extent accepted by metadata plan");
    r.key = "bad"; auto bad = loader.Request(r);
    Await([&] { return loader.Poll(bad).status != AsyncLoadStatus::Pending; }, "Failed metadata plan hung");
    Check(loader.Poll(bad).status == AsyncLoadStatus::Failed, "Malformed package was published");
}
// Package with two selected textures: T0 stored raw, T1 as a Zstd frame
// (hand-built raw block). 4x4 RGBA32, 64 bytes each.
struct TexturePackage {
    std::filesystem::path path = std::filesystem::temp_directory_path() /
        ("bem-texture-" + std::to_string(std::chrono::steady_clock::now().time_since_epoch().count()) + ".bem");
    std::vector<uint8_t> tex0 = std::vector<uint8_t>(64, 0x11), tex1 = std::vector<uint8_t>(64, 0x22);
    uint8_t declared1 = 64;
    std::string name = "Textures";
    TexturePackage() { for (size_t i = 0; i < 64; ++i) { tex0[i] = uint8_t(i); tex1[i] = uint8_t(255 - i); } Write(); }
    ~TexturePackage() { std::error_code ignored; std::filesystem::remove(path, ignored); }
    std::vector<uint8_t> Frame() const {
        std::vector<uint8_t> frame{0x28, 0xB5, 0x2F, 0xFD, 0x20, declared1};
        const uint32_t block = 1u | (uint32_t(tex1.size()) << 3); // last raw block
        frame.push_back(uint8_t(block)); frame.push_back(uint8_t(block >> 8)); frame.push_back(uint8_t(block >> 16));
        frame.insert(frame.end(), tex1.begin(), tex1.end());
        return frame;
    }
    void Write() {
        using J = nlohmann::json;
        std::vector<std::vector<uint8_t>> payloads{std::vector<uint8_t>(36), std::vector<uint8_t>(24), std::vector<uint8_t>(12), {0,0,1,0,2,0}, tex0, Frame()};
        J manifest = {{"schema",1},{"package_id","bem.texture.test"},{"name",name},{"author","Tests"},{"version","1"},
            {"required_capabilities",J::array({"composable-options"})},
            {"target",{{"platform","windows-x64"},{"character_id","chr_test"},{"profile_id","test"},{"revision","r1"},
                {"world_resource","chr_test_postmodel"},{"ui_resource","chr_test_uimodel"},
                {"components",J::array({{{"id",0},{"mesh_name","Body"},{"original_index_count",3},
                    {"bone_names",J::array({"root"})},{"materials",J::array({"body"})}}})}}},
            {"option_groups",J::array({{{"id","base"},{"name","Base"},{"default","on"},
                {"choices",J::array({{{"id","on"},{"name","On"}},{{"id","off"},{"name","Off"}}})}}})},
            {"component_rules",J::array({{{"target",0},{"candidates",J::array({{{"operation","replace"},{"mesh",0}}})}}})},
            {"meshes",J::array({{{"vertex_count",3},{"index_size",2},
                {"streams",J::array({{{"payload",0},{"stride",12}},{{"payload",1},{"stride",8}},{{"payload",2},{"stride",4}}})},
                {"attributes",J::array({J::array({0,0,3,0,0}),J::array({4,0,2,1,0}),J::array({13,6,4,2,0})})},
                {"bones",J::array({{{"component",0},{"index",0},{"name","root"}}})},
                {"draws",J::array({{{"indices",3},{"count",3},{"material_component",0},{"material_slot",0},{"material_name","body"},{"textures",J::array({0,1})}}})}}})},
            {"textures",J::array({{{"width",4},{"height",4},{"mips",1},{"format",4},{"srgb",false},{"original_name","T0"},{"payload",4}},
                {{"width",4},{"height",4},{"mips",1},{"format",4},{"srgb",false},{"original_name","T1"},{"payload",5}}})}};
        const auto json = manifest.dump(); Header h{}; std::memcpy(h.magic, "BEM\0PKG\0", 8);
        h.major = 1; h.minor = 1; h.size = sizeof(h); h.manifest = json.size(); h.count = uint32_t(payloads.size());
        uint64_t offset = sizeof(h) + json.size() + payloads.size() * sizeof(Entry);
        std::vector<Entry> entries;
        for (size_t i = 0; i < payloads.size(); ++i) {
            const auto& p = payloads[i];
            entries.push_back({i == 5 ? 1u : 0u, 0, offset, p.size(), i == 5 ? 64u : p.size()}); offset += p.size();
        }
        h.file = offset; std::ofstream out(path, std::ios::binary | std::ios::trunc);
        out.write(reinterpret_cast<const char*>(&h), sizeof(h)); out.write(json.data(), json.size());
        out.write(reinterpret_cast<const char*>(entries.data()), entries.size() * sizeof(Entry));
        for (const auto& p : payloads) out.write(reinterpret_cast<const char*>(p.data()), p.size());
        Check(bool(out), "Cannot write texture BEM fixture");
    }
};
void DeferredTexturePayloads() {
    TexturePackage fixture; std::string error; BemLoadPlan whole, deferred; BemPocData full, data;
    Check(ReadBemLoadPlan(fixture.path, whole, error), error);
    Check(ReadBemLoadPlan(fixture.path, deferred, error, {}, false, {}, true), error);
    // Each selected texture payload reserves bytes*(consumers+1) = 64*2.
    Check(whole.reservation_bytes == deferred.reservation_bytes + 2 * 128 && deferred.payload_ids.size() == 4,
        "Deferred load plan still reserves texture payloads");
    Check(LoadBem(fixture.path, full, error, {}, nullptr, false, true), error);
    Check(LoadBem(fixture.path, data, error, {}, nullptr, false, true, {}, deferred.reservation_bytes, true), error);
    Check(data.payload_source && data.textures.size() == 2 && data.textures[0].Deferred() && data.textures[1].Deferred() &&
        data.textures[1].payload_codec == 1 && data.components[0].streams == full.components[0].streams,
        "Deferred load decoded textures or changed geometry");
    std::vector<uint8_t> bytes;
    Check(DecodeBemTexturePayload(*data.payload_source, data.textures[0], bytes, error) && bytes == fixture.tex0 && bytes == full.textures[0].data, "Raw texture entry differs: " + error);
    Check(DecodeBemTexturePayload(*data.payload_source, data.textures[1], bytes, error) && bytes == fixture.tex1 && bytes == full.textures[1].data, "Zstd texture entry differs: " + error);
    // A frame whose declared content size disagrees with the entry is refused.
    fixture.declared1 = 63; fixture.Write(); BemPocData lying;
    Check(LoadBem(fixture.path, lying, error, {}, nullptr, false, false, {}, UINT64_MAX, true), error);
    Check(!DecodeBemTexturePayload(*lying.payload_source, lying.textures[1], bytes, error) && bytes.empty(), "Mismatched Zstd content size accepted");
    // A rewritten package (other size/time) is a different generation.
    fixture.declared1 = 64; fixture.name = "Textures changed"; fixture.Write();
    Check(!DecodeBemTexturePayload(*data.payload_source, data.textures[0], bytes, error) && bytes.empty() &&
        error.find("changed") != std::string::npos, "Changed package generation accepted");
}
void TextureStreamerAdmission() {
    std::mutex mutex; std::condition_variable cv; std::set<std::string> open; std::vector<std::string> order;
    auto gate = [&](std::string key, size_t size, bool ok = true) {
        return [&, key, size, ok](std::vector<uint8_t>& out, std::string& error) {
            std::unique_lock lock(mutex);
            order.push_back(key); cv.notify_all();
            cv.wait(lock, [&] { return open.contains(key) || open.contains("*"); });
            if (!ok) { error = "bad " + key; return false; }
            out.assign(size, 7); return true;
        };
    };
    auto release = [&](const std::string& key) { std::lock_guard lock(mutex); open.insert(key); cv.notify_all(); };
    auto started = [&](size_t n) { std::lock_guard lock(mutex); return order.size() >= n; };
    struct OpenAll { std::function<void()> f; ~OpenAll() { f(); } } unblock{[&] { release("*"); }};
    TexturePayloadStreamer streamer(TexturePayloadStreamer::Config{100});
    auto blocker = streamer.Request(10, LoadPriority::Visible, gate("blocker", 10));
    Await([&] { return started(1); }, "Streamer did not start");
    auto a = streamer.Request(60, LoadPriority::Visible, gate("a", 60));
    auto big = streamer.Request(200, LoadPriority::Visible, gate("big", 200));
    auto cancelled = streamer.Request(5, LoadPriority::Visible, gate("cancelled", 5));
    auto b = streamer.Request(60, LoadPriority::Visible, gate("b", 60));
    auto fg = streamer.Request(10, LoadPriority::Foreground, gate("fg", 10));
    auto bad = streamer.Request(1, LoadPriority::Visible, gate("bad", 1, false));
    cancelled.Reset();
    release("blocker"); release("fg"); release("a"); release("b"); release("bad");
    TexturePayloadStreamer::Bytes held;
    Await([&] { auto r = streamer.Take(blocker); if (r.status == AsyncLoadStatus::Ready) held = r.bytes; return bool(held); }, "Blocker not ready");
    Check(streamer.Take(blocker).status == AsyncLoadStatus::Cancelled, "A taken result was handed out twice");
    TexturePayloadStreamer::Bytes fg_bytes, a_bytes;
    Await([&] { if (!fg_bytes) fg_bytes = streamer.Take(fg).bytes; if (!a_bytes) a_bytes = streamer.Take(a).bytes; return fg_bytes && a_bytes; },
        "Foreground/first background texture not decoded");
    {
        std::lock_guard lock(mutex);
        Check(order.size() == 3 && order[1] == "fg" && order[2] == "a", "Foreground texture was not decoded first");
    }
    std::this_thread::sleep_for(20ms);
    auto counts = streamer.Snapshot();
    Check(counts.live == 80 && counts.peak <= 100 && !started(4), "Streamer exceeded its live budget");
    // The oldest waiting entry is larger than the budget: it waits for an empty
    // ledger, and later small entries queue behind it (no starvation).
    a_bytes.reset();
    std::this_thread::sleep_for(20ms);
    Check(!started(4) && streamer.Snapshot().live == 20, "Small texture overtook a waiting exclusive texture");
    held.reset(); fg_bytes.reset();
    Await([&] { return started(4); }, "Oversize texture never got its exclusive turn");
    { std::lock_guard lock(mutex); Check(order[3] == "big", "Exclusive texture did not run next"); }
    release("big");
    TexturePayloadStreamer::Bytes big_bytes;
    Await([&] { big_bytes = streamer.Take(big).bytes; return bool(big_bytes); }, "Oversize texture not delivered");
    std::this_thread::sleep_for(20ms);
    counts = streamer.Snapshot();
    Check(counts.live == 200 && counts.peak == 200 && big_bytes->size() == 200 && !started(5),
        "Exclusive oversize accounting differs or ran beside other bytes");
    big_bytes.reset();
    TexturePayloadStreamer::Bytes b_bytes;
    Await([&] { b_bytes = streamer.Take(b).bytes; return bool(b_bytes); }, "Released budget did not admit the next texture");
    Await([&] { auto r = streamer.Take(bad); return r.status == AsyncLoadStatus::Failed && r.error == "bad bad"; }, "Failed decode not reported");
    {
        std::lock_guard lock(mutex);
        Check(std::find(order.begin(), order.end(), "cancelled") == order.end(), "Cancelled ticket was decoded");
    }
    streamer.Shutdown();
    Check(b_bytes->size() == 60 && (*b_bytes)[0] == 7, "Taken bytes did not outlive the streamer");
    b_bytes.reset();
    Check(streamer.Snapshot().live == 0, "Released bytes were not refunded");
}
}
int main() {
    try {
        ConcurrencyAndMerge(); ExclusiveLargeAndLateCancellation(); FairnessAndIdentity(); ResourceSelectionIdentity(); PriorityUpdateAndQueuedCancellation(); FrameAdmission(); FailureAndShutdownOwnership(); ProductionCpuIntegration();
        DeferredTexturePayloads(); TextureStreamerAdmission();
        std::cout << "PASS: async loading concurrency/merge/fairness/generations/lifetimes/large escape/frame budget/production BEM/deferred texture entries/texture streamer (" << checks << " checks)\n";
        return 0;
    } catch (const std::exception& e) { std::cerr << e.what() << '\n'; return 1; }
}
