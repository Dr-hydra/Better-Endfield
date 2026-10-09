#pragma once
#include "bem.h"
#include <algorithm>
#include <chrono>
#include <condition_variable>
#include <deque>
#include <functional>
#include <memory>
#include <mutex>
#include <optional>
#include <stdexcept>
#include <thread>
#include <unordered_map>
#include <utility>

namespace BetterEndfieldNext::CustomModel {
constexpr uint64_t kLoadingMiB = 1024ull * 1024;
enum class LoadPriority { Prewarm = 0, Visible = 1, Foreground = 2 };
enum class AsyncLoadStatus { Pending, Ready, Failed, Cancelled };
struct BemRequest {
    // Immutable content identity supplied by runtime; generations are subscribers.
    std::string key, revision;
    std::filesystem::path path;
    uint64_t generation = 0;
    LoadPriority priority = LoadPriority::Foreground;
    std::string appearance, parameters, resource_id;
    bool skip_validation = false, loading_optimization = false;
    // Texture payloads stay in the package (BemTexture::Deferred) and are
    // decoded one at a time by TexturePayloadStreamer; excluded from the plan.
    bool defer_texture_payloads = false;
    // Optional immutable FILE lease only. Never put Unity objects/GC roots here.
    std::shared_ptr<const void> file_lease;
};
struct BemResult {
    // Declared first so accounting releases AFTER the decoded vectors destruct.
    std::shared_ptr<const void> decoded_lease;
    BemPocData data;
    BemPackageInfo package;
    BemLoadStats stats;
    std::string key, revision;
    uint64_t reservation_bytes = 0;
};

// Pure CPU queue. Own ONE instance for all world/UI/role requests. Poll never
// waits for workers. No callbacks into runtime, host, Unity or live objects.
class AsyncBemLoader {
public:
    struct Config {
        uint64_t decoded_budget = 256 * kLoadingMiB;
        size_t workers = 2, max_pending = 64;
    };
    // Injectable pure CPU backend for deterministic scheduler/ownership tests.
    struct Backend {
        std::function<bool(const BemRequest&, BemLoadPlan&, std::string&)> plan;
        std::function<bool(const BemRequest&, uint64_t, BemPocData&, BemLoadStats&, std::string&)> load;
    };
private:
    struct State;
    struct Job;
    struct Subscriber {
        std::weak_ptr<State> state;
        std::shared_ptr<Job> job;
        uint64_t generation = 0;
        LoadPriority priority = LoadPriority::Foreground;
        bool cancelled = false;
        ~Subscriber();
    };
public:
    class Ticket {
        friend class AsyncBemLoader;
        std::shared_ptr<Subscriber> sub_;
        explicit Ticket(std::shared_ptr<Subscriber> sub) : sub_(std::move(sub)) {}
    public:
        Ticket() = default;
        explicit operator bool() const { return bool(sub_); }
    };
    struct PollResult {
        AsyncLoadStatus status = AsyncLoadStatus::Pending;
        std::shared_ptr<const BemResult> result;
        std::string error;
    };
    struct Counters {
        uint64_t reserved_decoded = 0, live_decoded = 0, peak_decoded = 0;
        size_t active_workers = 0, pending = 0;
        bool large_exclusive = false;
        uint64_t started = 0, merged = 0, stale_results = 0;
    };
    AsyncBemLoader() : AsyncBemLoader(Config{}, Backend{}) {}
    explicit AsyncBemLoader(Config config) : AsyncBemLoader(config, Backend{}) {}
    AsyncBemLoader(Config config, Backend backend);
    ~AsyncBemLoader() { Shutdown(); }
    AsyncBemLoader(const AsyncBemLoader&) = delete;
    AsyncBemLoader& operator=(const AsyncBemLoader&) = delete;
    Ticket Request(BemRequest request);
    PollResult Poll(const Ticket& ticket) const;
    void Cancel(const Ticket& ticket);
    // Cancels existing subscriptions, not unrelated consumers of the same key.
    void CancelGeneration(uint64_t generation);
    void SetPriority(const Ticket& ticket, LoadPriority priority);
    Counters Snapshot() const;
    // Join only on module teardown, never in a Finish hook/pump. An in-progress
    // LoadBem is not forcibly interrupted; its stale output is discarded.
    void Shutdown();
private:
    enum class Phase { Plan, Decode, RunningPlan, RunningDecode, Done };
    struct Lease {
        std::shared_ptr<State> state;
        uint64_t bytes;
        bool large, live = false;
        ~Lease();
    };
    struct Job {
        BemRequest request;
        std::vector<std::weak_ptr<Subscriber>> subscribers;
        Phase phase = Phase::Plan;
        BemLoadPlan plan;
        std::shared_ptr<Lease> lease;
        std::shared_ptr<const BemResult> result;
        std::string error;
        uint64_t order = 0;
    };
    struct State : std::enable_shared_from_this<State> {
        // Result release can occur while cancelling under this lock.
        mutable std::recursive_mutex mutex;
        std::condition_variable_any cv;
        Config config;
        Backend backend;
        std::vector<std::shared_ptr<Job>> jobs;
        std::unordered_map<std::string, std::weak_ptr<Job>> by_key;
        Counters counts;
        uint64_t order = 0;
        unsigned foreground_streak = 0;
        bool stopping = false;
    };
    std::shared_ptr<State> state_;
    std::vector<std::thread> workers_;
    static std::optional<LoadPriority> Priority(Job& job);
    static std::shared_ptr<Job> Pick(State& state);
    static void Worker(std::shared_ptr<State> state);
    static std::string Identity(const BemRequest& request);
    static void Prune(State& state);
};

inline AsyncBemLoader::Subscriber::~Subscriber() {
    if (auto s = state.lock()) {
        std::lock_guard lock(s->mutex);
        s->cv.notify_all();
    }
}
inline AsyncBemLoader::Lease::~Lease() {
    std::lock_guard lock(state->mutex);
    if (live) state->counts.live_decoded -= bytes;
    else state->counts.reserved_decoded -= bytes;
    if (large) state->counts.large_exclusive = false;
    state->cv.notify_all();
}
inline std::string AsyncBemLoader::Identity(const BemRequest& r) {
    std::string key;
    auto add = [&](const std::string& field) { key += std::to_string(field.size()) + ':' + field; };
    add(r.key); add(r.revision); add(r.path.lexically_normal().generic_string());
    add(r.appearance); add(r.parameters); add(r.resource_id);
    key += r.skip_validation ? '1' : '0';
    key += r.loading_optimization ? '1' : '0';
    key += r.defer_texture_payloads ? '1' : '0';
    return key;
}
inline std::optional<LoadPriority> AsyncBemLoader::Priority(Job& j) {
    std::optional<LoadPriority> priority;
    std::erase_if(j.subscribers, [](const auto& sub) { return sub.expired(); });
    for (const auto& weak : j.subscribers) if (auto sub = weak.lock(); sub && !sub->cancelled)
        if (!priority || sub->priority > *priority) priority = sub->priority;
    return priority;
}
inline void AsyncBemLoader::Prune(State& s) {
    std::erase_if(s.jobs, [](const auto& j) {
        return j->phase == Phase::Done ||
            ((!Priority(*j)) && (j->phase == Phase::Plan || j->phase == Phase::Decode));
    });
    std::erase_if(s.by_key, [](const auto& entry) { return entry.second.expired(); });
    s.counts.pending = s.jobs.size();
}
inline std::shared_ptr<AsyncBemLoader::Job> AsyncBemLoader::Pick(State& s) {
    Prune(s);
    if (s.stopping || s.counts.large_exclusive) return {};
    std::shared_ptr<Job> foreground, background, oldestBackground;
    std::optional<LoadPriority> bestPriority;
    for (const auto& j : s.jobs) {
        if (j->phase != Phase::Plan && j->phase != Phase::Decode) continue;
        const auto priority = Priority(*j);
        if (!priority) continue;
        if (*priority == LoadPriority::Foreground) {
            if (!foreground || j->order < foreground->order) foreground = j;
        } else {
            if (!oldestBackground || j->order < oldestBackground->order) oldestBackground = j;
            if (!background || *priority > *bestPriority ||
                (*priority == *bestPriority && j->order < background->order)) {
                background = j; bestPriority = priority;
            }
        }
    }
    // Three foreground opportunities, then one background opportunity. Rotate
    // served jobs to the tail. A blocked exclusive job drains existing holders;
    // it never opens four monolithic decodes just to make workers busy.
    auto job = foreground;
    if (background && s.foreground_streak >= 3) job = oldestBackground;
    else if (!job) job = background;
    if (!job) return {};
    if (job->phase == Phase::Decode) {
        const auto bytes = job->plan.reservation_bytes;
        const bool large = bytes > s.config.decoded_budget;
        const auto held = s.counts.live_decoded + s.counts.reserved_decoded;
        if (large ? (held != 0 || s.counts.active_workers != 0) : (bytes > s.config.decoded_budget - held)) return {};
        auto lease = std::make_shared<Lease>();
        lease->state = s.shared_from_this();
        lease->bytes = bytes; lease->large = large;
        s.counts.reserved_decoded += bytes;
        s.counts.large_exclusive = large;
        s.counts.peak_decoded = std::max(s.counts.peak_decoded, held + bytes);
        job->lease = std::move(lease);
        job->phase = Phase::RunningDecode;
    } else job->phase = Phase::RunningPlan;
    if (Priority(*job) != LoadPriority::Prewarm) ++s.foreground_streak;
    else s.foreground_streak = 0;
    job->order = ++s.order;
    ++s.counts.active_workers; ++s.counts.started;
    return job;
}
inline AsyncBemLoader::AsyncBemLoader(Config config, Backend backend) : state_(std::make_shared<State>()) {
    if (!config.decoded_budget || !config.max_pending) throw std::invalid_argument("Invalid loading budget");
    config.workers = std::clamp<size_t>(config.workers, 1, 2);
    if (!backend.plan) backend.plan = [](const BemRequest& r, BemLoadPlan& p, std::string& error) {
        return ReadBemLoadPlan(r.path, p, error, r.appearance, r.skip_validation, r.parameters,
            r.defer_texture_payloads,r.resource_id);
    };
    if (!backend.load) backend.load = [](const BemRequest& r, uint64_t bytes, BemPocData& data, BemLoadStats& stats, std::string& error) {
        return LoadBem(r.path, data, error, r.appearance, &stats, r.skip_validation,
            r.loading_optimization, r.parameters, bytes, r.defer_texture_payloads,r.resource_id);
    };
    state_->config = config; state_->backend = std::move(backend);
    try {
        for (size_t i = 0; i < config.workers; ++i) workers_.emplace_back(&Worker, state_);
    } catch (...) { Shutdown(); throw; }
}
inline AsyncBemLoader::Ticket AsyncBemLoader::Request(BemRequest request) {
    if (request.key.empty() || request.revision.empty()) throw std::invalid_argument("Immutable loading key/revision required");
    const auto identity = Identity(request);
    auto sub = std::make_shared<Subscriber>();
    auto s = state_;
    std::lock_guard lock(s->mutex);
    if (s->stopping) return {};
    Prune(*s);
    std::shared_ptr<Job> job;
    if (auto found = s->by_key.find(identity); found != s->by_key.end()) {
        job = found->second.lock();
        if (job && !Priority(*job)) job.reset(); // cancelled work cannot be resurrected
    }
    if (!job) {
        if (s->jobs.size() >= s->config.max_pending) return {};
        job = std::make_shared<Job>();
        job->request = request; job->order = ++s->order;
        s->jobs.push_back(job); s->by_key[identity] = job;
    } else ++s->counts.merged;
    sub->state = s; sub->job = job; sub->generation = request.generation; sub->priority = request.priority;
    job->subscribers.push_back(sub);
    s->cv.notify_all();
    return Ticket(std::move(sub));
}
inline AsyncBemLoader::PollResult AsyncBemLoader::Poll(const Ticket& ticket) const {
    std::lock_guard lock(state_->mutex);
    if (!ticket.sub_ || ticket.sub_->state.lock() != state_ || ticket.sub_->cancelled || state_->stopping)
        return {AsyncLoadStatus::Cancelled, {}, {}};
    const auto& job = *ticket.sub_->job;
    if (job.phase != Phase::Done) return {};
    if (job.result) return {AsyncLoadStatus::Ready, job.result, {}};
    return {AsyncLoadStatus::Failed, {}, job.error};
}
inline void AsyncBemLoader::Cancel(const Ticket& ticket) {
    std::lock_guard lock(state_->mutex);
    if (!ticket.sub_ || ticket.sub_->state.lock() != state_) return;
    ticket.sub_->cancelled = true;
    if (!Priority(*ticket.sub_->job)) ticket.sub_->job->result.reset();
    state_->cv.notify_all();
}
inline void AsyncBemLoader::CancelGeneration(uint64_t generation) {
    std::lock_guard lock(state_->mutex);
    for (auto& [key, weak] : state_->by_key) if (auto j = weak.lock()) {
        for (auto& weakSub : j->subscribers) if (auto sub = weakSub.lock(); sub && sub->generation == generation) sub->cancelled = true;
        if (!Priority(*j)) j->result.reset();
    }
    state_->cv.notify_all();
}
inline void AsyncBemLoader::SetPriority(const Ticket& ticket, LoadPriority priority) {
    std::lock_guard lock(state_->mutex);
    if (ticket.sub_ && ticket.sub_->state.lock() == state_ && !ticket.sub_->cancelled) ticket.sub_->priority = priority;
    state_->cv.notify_all();
}
inline AsyncBemLoader::Counters AsyncBemLoader::Snapshot() const {
    std::lock_guard lock(state_->mutex);
    auto counts = state_->counts; counts.pending = state_->jobs.size(); return counts;
}
inline void AsyncBemLoader::Shutdown() {
    {
        std::lock_guard lock(state_->mutex);
        state_->stopping = true;
        for (auto& [key, weak] : state_->by_key) if (auto j = weak.lock()) j->result.reset();
        state_->cv.notify_all();
    }
    for (auto& thread : workers_) if (thread.joinable()) thread.join();
    workers_.clear();
    std::lock_guard lock(state_->mutex);
    state_->jobs.clear(); state_->by_key.clear();
}
inline void AsyncBemLoader::Worker(std::shared_ptr<State> s) {
    for (;;) {
        std::shared_ptr<Job> job;
        {
            std::unique_lock lock(s->mutex);
            s->cv.wait(lock, [&] { if (s->stopping) return true; job = Pick(*s); return bool(job); });
            if (s->stopping) return;
        }
        const bool planning = job->phase == Phase::RunningPlan;
        bool ok = false;
        std::string error;
        BemLoadPlan plan;
        std::shared_ptr<BemResult> result;
        try {
            bool wanted;
            {
                std::lock_guard lock(s->mutex);
                wanted = !s->stopping && Priority(*job).has_value();
            }
            if (wanted && planning) ok = s->backend.plan(job->request, plan, error);
            else if (wanted) {
                result = std::make_shared<BemResult>();
                result->decoded_lease = job->lease;
                result->key = job->request.key; result->revision = job->request.revision;
                result->reservation_bytes = job->plan.reservation_bytes;
                result->package = job->plan.package;
                ok = s->backend.load(job->request, result->reservation_bytes, result->data, result->stats, error);
            }
        } catch (const std::exception& e) { error = e.what(); }
        catch (...) { error = "CPU model loader threw an unknown exception"; }
        {
            std::lock_guard lock(s->mutex);
            --s->counts.active_workers;
            if (s->stopping || !Priority(*job)) {
                if (!planning) ++s->counts.stale_results;
                // Destroy vectors before refunding the reservation.
                result.reset(); job->lease.reset(); job->phase = Phase::Done;
            } else if (!ok) {
                result.reset(); job->lease.reset(); job->error = std::move(error); job->phase = Phase::Done;
            } else if (planning) { job->plan = std::move(plan); job->phase = Phase::Decode; }
            else {
                job->lease->live = true;
                s->counts.reserved_decoded -= job->lease->bytes;
                s->counts.live_decoded += job->lease->bytes;
                job->result = std::move(result); job->lease.reset(); job->phase = Phase::Done;
            }
            Prune(*s); s->cv.notify_all();
        }
    }
}

// One background decoder for deferred texture entries (all roles share it).
// A consumer requests ONE texture, takes the decoded bytes, hands them to
// LoadRawTextureData and drops them; the live-byte ledger refunds on release.
// Admission keeps decoded-but-unconsumed bytes under `live_budget`; a single
// larger texture runs only when nothing else is held (exclusive). Foreground
// requests are served first; equal priority is FIFO. Pure CPU: no Unity calls.
class TexturePayloadStreamer {
public:
    struct Config { uint64_t live_budget = 96 * kLoadingMiB; };
    using Decode = std::function<bool(std::vector<uint8_t>&, std::string&)>;
    using Bytes = std::shared_ptr<const std::vector<uint8_t>>;
    struct Counters {
        uint64_t reserved = 0, live = 0, peak = 0, decoded_total = 0, decoded_count = 0;
        size_t pending = 0;
    };
private:
    struct State;
    struct Work {
        uint64_t bytes = 0, order = 0;
        LoadPriority priority = LoadPriority::Visible;
        Decode decode;
        bool running = false, done = false, cancelled = false, taken = false;
        Bytes result;
        std::string error;
    };
    struct Holder {
        std::vector<uint8_t> bytes;
        std::shared_ptr<State> state;
        uint64_t charge = 0;
        ~Holder();
    };
    struct State {
        std::mutex mutex;
        std::condition_variable cv;
        Config config;
        std::deque<std::shared_ptr<Work>> queue;
        Counters counts;
        uint64_t order = 0;
        bool stopping = false;
    };
    std::shared_ptr<State> state_;
    std::thread worker_;
    static std::shared_ptr<Work> Pick(State& s) {
        std::erase_if(s.queue, [](const auto& r) { return r->cancelled; });
        s.counts.pending = s.queue.size();
        std::shared_ptr<Work> best;
        for (const auto& r : s.queue)
            if (!best || r->priority > best->priority || (r->priority == best->priority && r->order < best->order)) best = r;
        if (!best) return {};
        const auto held = s.counts.live + s.counts.reserved;
        const bool fits = best->bytes <= s.config.live_budget && held <= s.config.live_budget - best->bytes;
        if (!fits && held != 0) return {}; // larger than budget: exclusive once nothing is held
        std::erase(s.queue, best);
        s.counts.pending = s.queue.size();
        s.counts.reserved += best->bytes;
        s.counts.peak = std::max(s.counts.peak, held + best->bytes);
        best->running = true;
        return best;
    }
    static void Worker(std::shared_ptr<State> s) {
        for (;;) {
            std::shared_ptr<Work> request;
            {
                std::unique_lock lock(s->mutex);
                s->cv.wait(lock, [&] { if (s->stopping) return true; request = Pick(*s); return bool(request); });
                if (s->stopping) return;
            }
            auto holder = std::make_shared<Holder>();
            std::string error;
            bool ok = false;
            try { ok = request->decode(holder->bytes, error); }
            catch (const std::exception& e) { error = e.what(); }
            catch (...) { error = "texture payload decode threw"; }
            std::lock_guard lock(s->mutex);
            s->counts.reserved -= request->bytes;
            if (ok && !request->cancelled && !s->stopping) {
                holder->state = s; holder->charge = holder->bytes.size();
                s->counts.live += holder->charge;
                s->counts.peak = std::max(s->counts.peak, s->counts.live + s->counts.reserved);
                s->counts.decoded_total += holder->charge; ++s->counts.decoded_count;
                request->result = Bytes(holder, &holder->bytes);
            } else if (!ok) request->error = error.empty() ? "texture payload decode failed" : error;
            holder.reset(); // a discarded result is freed before workers pick again
            request->running = false; request->done = true; request->decode = nullptr;
            s->cv.notify_all();
        }
    }
public:
    class Ticket {
        friend class TexturePayloadStreamer;
        std::shared_ptr<Work> request_;
        std::weak_ptr<State> state_;
    public:
        Ticket() = default;
        Ticket(const Ticket&) = delete;
        Ticket& operator=(const Ticket&) = delete;
        Ticket(Ticket&&) noexcept = default;
        Ticket& operator=(Ticket&& other) noexcept {
            if (this != &other) { Reset(); request_ = std::move(other.request_); state_ = std::move(other.state_); }
            return *this;
        }
        ~Ticket() { Reset(); }
        explicit operator bool() const { return bool(request_); }
        // Dropping a ticket cancels queued work and discards a finished result.
        void Reset() {
            if (!request_) return;
            Bytes discarded; // released after the lock: its ledger refund locks too
            if (auto s = state_.lock()) {
                std::lock_guard lock(s->mutex);
                request_->cancelled = true; discarded = std::move(request_->result);
                s->cv.notify_all();
            }
            request_.reset();
        }
    };
    struct PollResult {
        AsyncLoadStatus status = AsyncLoadStatus::Pending;
        Bytes bytes;
        std::string error;
    };
    TexturePayloadStreamer() : TexturePayloadStreamer(Config{}) {}
    explicit TexturePayloadStreamer(Config config) : state_(std::make_shared<State>()) {
        if (!config.live_budget) throw std::invalid_argument("Invalid texture streaming budget");
        state_->config = config;
        worker_ = std::thread(&Worker, state_);
    }
    ~TexturePayloadStreamer() { Shutdown(); }
    TexturePayloadStreamer(const TexturePayloadStreamer&) = delete;
    TexturePayloadStreamer& operator=(const TexturePayloadStreamer&) = delete;
    Ticket Request(uint64_t bytes, LoadPriority priority, Decode decode) {
        Ticket ticket;
        if (!decode || !bytes) return ticket;
        std::lock_guard lock(state_->mutex);
        if (state_->stopping) return ticket;
        auto request = std::make_shared<Work>();
        request->bytes = bytes; request->priority = priority; request->decode = std::move(decode);
        request->order = ++state_->order;
        state_->queue.push_back(request);
        state_->counts.pending = state_->queue.size();
        ticket.request_ = std::move(request); ticket.state_ = state_;
        state_->cv.notify_all();
        return ticket;
    }
    // Never waits. A Ready result is MOVED out: the caller becomes its only owner.
    PollResult Take(Ticket& ticket) {
        std::lock_guard lock(state_->mutex);
        if (!ticket.request_ || ticket.request_->cancelled || state_->stopping) return {AsyncLoadStatus::Cancelled, {}, {}};
        auto& request = *ticket.request_;
        if (!request.done) return {};
        if (request.taken) return {AsyncLoadStatus::Cancelled, {}, {}};
        if (request.result) { request.taken = true; return {AsyncLoadStatus::Ready, std::move(request.result), {}}; }
        return {AsyncLoadStatus::Failed, {}, request.error};
    }
    void SetPriority(const Ticket& ticket, LoadPriority priority) {
        std::lock_guard lock(state_->mutex);
        if (ticket.request_) ticket.request_->priority = priority;
        state_->cv.notify_all();
    }
    Counters Snapshot() const {
        std::lock_guard lock(state_->mutex);
        auto counts = state_->counts; counts.pending = state_->queue.size(); return counts;
    }
    // Teardown only; an in-progress decode finishes and is discarded.
    void Shutdown() {
        {
            std::lock_guard lock(state_->mutex);
            state_->stopping = true;
            for (auto& r : state_->queue) r->cancelled = true;
            state_->queue.clear();
            state_->cv.notify_all();
        }
        if (worker_.joinable()) worker_.join();
    }
};
inline TexturePayloadStreamer::Holder::~Holder() {
    if (!state) return;
    std::lock_guard lock(state->mutex);
    state->counts.live -= charge;
    state->cv.notify_all();
}

struct StepCost {
    uint64_t bytes = 0;
    uint32_t steps = 1;
    bool heavy = false;
    bool texture = false; // implies heavy; ctor/raw/Apply each take one permit
};
// Main-thread admission ledger. Frames, debt and cooldown measure submission
// density ONLY; none is a GPU completion fence or residency measurement.
class FrameBudget {
public:
    using Clock = std::chrono::steady_clock;
    struct Config {
        uint64_t bytes = 32 * kLoadingMiB, max_exclusive_bytes = 64 * kLoadingMiB;
        uint32_t steps = 8, cooldown_frames = 2;
        std::chrono::nanoseconds time = std::chrono::milliseconds(2);
    };
    struct Counters {
        uint64_t frame_id = 0, bytes = 0, byte_debt = 0;
        uint32_t steps = 0, heavy = 0, textures = 0, cooldown_remaining = 0;
        std::chrono::nanoseconds elapsed{};
        bool oversize = false;
    };
    class Permit {
        friend class FrameBudget;
        FrameBudget* budget_ = nullptr;
        Clock::time_point started_{};
        explicit Permit(FrameBudget* b) : budget_(b), started_(Clock::now()) {}
    public:
        Permit() = default;
        Permit(const Permit&) = delete;
        Permit& operator=(const Permit&) = delete;
        Permit(Permit&& other) noexcept : budget_(std::exchange(other.budget_, nullptr)), started_(other.started_) {}
        Permit& operator=(Permit&& other) noexcept {
            if (this != &other) { Finish(); budget_ = std::exchange(other.budget_, nullptr); started_ = other.started_; }
            return *this;
        }
        ~Permit() { Finish(); }
        explicit operator bool() const { return budget_ != nullptr; }
        // Failed/cancelled calls still count. Explicit elapsed aids deterministic tests.
        void Finish(std::optional<std::chrono::nanoseconds> elapsed = std::nullopt) {
            if (auto b = std::exchange(budget_, nullptr)) b->Finish(elapsed.value_or(Clock::now() - started_));
        }
    };
    FrameBudget() : FrameBudget(Config{}) {}
    explicit FrameBudget(Config config) : config_(config) {
        if (!config.bytes || !config.steps || config.time <= std::chrono::nanoseconds::zero() || config.max_exclusive_bytes < config.bytes)
            throw std::invalid_argument("Invalid frame budget");
    }
    Permit TryBegin(uint64_t frameId, StepCost cost) {
        // A live permit prevents frame rollover/reentrancy while its call runs.
        if (active_ || (initialized_ && frameId < counts_.frame_id)) return {};
        if (!initialized_ || frameId != counts_.frame_id) {
            if (initialized_) {
                // Only distinct observed frames repay debt/cooldown; skipped frame
                // numbers are not evidence of pump opportunities or GPU progress.
                if (cooldown_) --cooldown_;
                else debt_ = debt_ > config_.bytes ? debt_ - config_.bytes : 0;
            }
            counts_ = {}; counts_.frame_id = frameId; initialized_ = true;
            blocked_heavy_ = cooldown_ != 0 || debt_ != 0;
            counts_.cooldown_remaining = cooldown_; counts_.byte_debt = debt_;
        }
        const bool heavy = cost.heavy || cost.texture || cost.bytes != 0;
        if (!cost.steps || cost.steps > config_.steps || counts_.oversize ||
            counts_.elapsed >= config_.time || cost.steps > config_.steps - counts_.steps ||
            (heavy && (blocked_heavy_ || counts_.heavy != 0))) return {};
        const bool oversized = cost.bytes > config_.bytes;
        if (oversized) {
            if (counts_.steps || cost.bytes > config_.max_exclusive_bytes) return {};
        } else if (cost.bytes > config_.bytes - counts_.bytes) return {};
        counts_.bytes += cost.bytes; counts_.steps += cost.steps;
        counts_.heavy += heavy ? 1 : 0; counts_.textures += cost.texture ? 1 : 0;
        counts_.oversize = oversized;
        if (oversized) {
            debt_ += cost.bytes - config_.bytes;
            // +1 ensures the next N observed frames are fully upload-free.
            cooldown_ = config_.cooldown_frames + 1;
            counts_.byte_debt = debt_;
        }
        active_ = true;
        return Permit(this);
    }
    Counters Snapshot() const { return counts_; }
private:
    Config config_;
    Counters counts_;
    bool initialized_ = false, active_ = false, blocked_heavy_ = false;
    uint64_t debt_ = 0;
    uint32_t cooldown_ = 0;
    void Finish(std::chrono::nanoseconds elapsed) {
        counts_.elapsed += std::max(elapsed, std::chrono::nanoseconds::zero()); active_ = false;
    }
};
} // namespace BetterEndfieldNext::CustomModel
