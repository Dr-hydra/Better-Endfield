// Automatic activation of the already-proven live-renderer replacement path.
//
// Important ownership rule:
//   This layer never loads, gets, disposes or otherwise touches the game's
//   chr_0003_endminf_postmodel.prefab resource.  The game owns that resource
//   lifecycle completely.  We wait until the game has naturally instantiated
//   the Endminf SkinnedMeshRenderers, then start the same BEM replacement path
//   that F12 already uses.  Once active, the production refresh and AI coverage
//   layers keep new instances / scene transitions reconciled.

constexpr uint64_t kLiveAutoApplyScanIntervalMs = 100;
constexpr uint64_t kLiveAutoApplyStableMs = 500;
constexpr uint64_t kLiveAutoApplyRetryMs = 2000;
constexpr size_t kLiveAutoApplyMinRenderers = 8;
constexpr int kLiveAutoApplyMaxAttempts = 4;

const BE_HostApiV1* g_live_auto_real_host = nullptr;
BE_HostApiV1 g_live_auto_host_proxy{};
bool g_live_auto_pump_intercepted = false;
uint64_t g_live_auto_next_scan_tick = 0;
uint64_t g_live_auto_stable_since = 0;
uint64_t g_live_auto_next_attempt_tick = 0;
size_t g_live_auto_last_count = 0;
int g_live_auto_attempts = 0;
bool g_live_auto_queued = false;
bool g_live_auto_completed = false;

void PumpLiveAutoApply() {
    if (g_live_auto_completed) return;

    // The first successful application is the hand-off point.  From here the
    // production UpdateReplacement/RefreshActiveReplacement path owns all
    // future renderer churn.  Keeping this flag set also means a later manual
    // F11 rollback stays rolled back instead of being immediately auto-applied.
    if (g_replacement_active) {
        g_live_auto_completed = true;
        Log("[LIVE-AUTO] replacement active; startup hand-off complete. Future scene/renderer changes are owned by the existing refresh path.");
        return;
    }

    const uint64_t now = GetTickCount64();

    // A queued ApplyOrRefreshReplacement is executed by the normal production
    // pump on a following frame.  Give it time to finish before deciding that
    // another startup attempt is needed.
    if (g_live_auto_queued) {
        if (g_pending_apply_frame >= 0 ||
            g_operation_in_progress.load(std::memory_order_acquire)) {
            return;
        }
        g_live_auto_queued = false;
        if (g_replacement_active) {
            g_live_auto_completed = true;
            Log("[LIVE-AUTO] replacement became active after queued startup apply.");
            return;
        }
        g_live_auto_next_attempt_tick = now + kLiveAutoApplyRetryMs;
        Log("[LIVE-AUTO] startup apply did not become active; waiting before retry.");
        return;
    }

    if (g_live_auto_attempts >= kLiveAutoApplyMaxAttempts) {
        g_live_auto_completed = true;
        Log("[LIVE-AUTO] automatic startup stopped after " +
            std::to_string(g_live_auto_attempts) +
            " attempts. F12 remains available for manual apply/refresh.");
        return;
    }

    if (now < g_live_auto_next_scan_tick || now < g_live_auto_next_attempt_tick)
        return;
    g_live_auto_next_scan_tick = now + kLiveAutoApplyScanIntervalMs;

    // This is the same discovery seam the working F12 path already trusts.  No
    // ResourceManager/I18NAssetLoader call is made here.
    const std::vector<RendererSummary> summaries = EnumerateLiveEndminfRenderers();
    const size_t count = summaries.size();
    if (count < kLiveAutoApplyMinRenderers) {
        if (g_live_auto_stable_since != 0) {
            Log("[LIVE-AUTO] live Endminf set became incomplete before startup: renderers=" +
                std::to_string(count) + "; stability window reset.");
        }
        g_live_auto_stable_since = 0;
        g_live_auto_last_count = count;
        return;
    }

    if (g_live_auto_stable_since == 0 || count != g_live_auto_last_count) {
        g_live_auto_stable_since = now;
        g_live_auto_last_count = count;
        Log("[LIVE-AUTO] live Endminf renderers discovered: count=" +
            std::to_string(count) + "; waiting " +
            std::to_string(kLiveAutoApplyStableMs) +
            " ms for a stable game-owned instance set.");
        return;
    }

    if (now - g_live_auto_stable_since < kLiveAutoApplyStableMs) return;
    if (g_operation_in_progress.load(std::memory_order_acquire) ||
        g_pending_apply_frame >= 0) {
        return;
    }

    ++g_live_auto_attempts;
    Log("[LIVE-AUTO] starting existing F12 replacement path automatically: attempt=" +
        std::to_string(g_live_auto_attempts) +
        " liveRenderers=" + std::to_string(count) +
        ". No native character prefab resource was acquired.");
    ApplyOrRefreshReplacement();
    g_live_auto_queued = true;
}

void __fastcall DetourPumpWithLiveAutoApply(void* method_info) {
    DetourPumpWithAiShadowCoverage(method_info);
    PumpLiveAutoApply();
}

BE_Result BE_CALL LiveAutoApplyCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_live_auto_real_host || !g_live_auto_real_host->create_hook)
        return BE_Result_NotReady;
    if (detour == reinterpret_cast<void*>(&DetourPumpWithAiShadowCoverage)) {
        g_live_auto_pump_intercepted = true;
        return g_live_auto_real_host->create_hook(context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithLiveAutoApply), original);
    }
    return g_live_auto_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithLiveAutoApply(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;

    g_live_auto_real_host = host;
    g_live_auto_host_proxy = *host;
    g_live_auto_host_proxy.create_hook = &LiveAutoApplyCreateHook;
    g_live_auto_pump_intercepted = false;
    g_live_auto_next_scan_tick = 0;
    g_live_auto_stable_since = 0;
    g_live_auto_next_attempt_tick = 0;
    g_live_auto_last_count = 0;
    g_live_auto_attempts = 0;
    g_live_auto_queued = false;
    g_live_auto_completed = false;

    const BE_Result result = InitializeWithAiShadowCoverage(&g_live_auto_host_proxy);
    if (result != BE_Result_Ok) {
        g_live_auto_real_host = nullptr;
        g_live_auto_host_proxy = {};
        return result;
    }
    if (!g_live_auto_pump_intercepted) {
        Log("[LIVE-AUTO] failed to intercept CustomModel main-thread pump.");
        ShutdownWithAiShadowCoverage();
        g_live_auto_real_host = nullptr;
        g_live_auto_host_proxy = {};
        return BE_Result_Failed;
    }

    Log("[LIVE-AUTO] armed: wait for game-owned Endminf renderers, then automatically start the existing F12 BEM replacement path.");
    Log("[LIVE-AUTO] native postmodel loading is disabled; I18NAssetLoader/FAssetProxyHandle are not used by this layer.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithLiveAutoApply() {
    g_live_auto_completed = true;
    g_live_auto_queued = false;
    g_live_auto_next_scan_tick = 0;
    g_live_auto_stable_since = 0;
    g_live_auto_next_attempt_tick = 0;
    g_live_auto_last_count = 0;
    g_live_auto_attempts = 0;

    ShutdownWithAiShadowCoverage();

    g_live_auto_pump_intercepted = false;
    g_live_auto_real_host = nullptr;
    g_live_auto_host_proxy = {};
}
