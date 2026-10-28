// Optional loop quality (jsspsi_quality): a compiled-in seam split for the
// rider and her mount, so their rewinds become hard cuts, and a phase-locked
// accessory lap for the tail and back hair. Writes share the pose overlay's
// TailLate slot and host pose lease; no hook, file or recording of its own.
// Off, the session never creates an owner and every entry returns at once.
struct QualityBone {
    void* transform = nullptr;
    uint32_t pin = 0;
    const QualitySeamBone* seam = nullptr; // Seam bones only.
    uint16_t accessory = 0;                // Accessory bones only.
    BonePose baseline{}, last{}, held{};
    bool wrote = false;
};
struct QualityRoot {
    void* animator = nullptr;
    void* root = nullptr;
    uint32_t animator_pin = 0, root_pin = 0;
    uint64_t lease = 0;
    std::vector<QualityBone> seam, accessory;
    float last_time = -1;
    bool applying = false, from_rewind = false;
};
struct QualityOwner {
    void* component = nullptr;
    void* handle = nullptr;
    const LoopQualityData* data = nullptr;
    bool diagnostics = false;
    QualityRoot rider, mount;
    int side = -1;
    bool rider_failed = false, seam_failed = false, seam_armed = false;
    int last_frame = -1;
    uint32_t seam_frames = 0, accessory_frames = 0;
    double microseconds = 0, max_microseconds = 0;
    bool fading = false;
    float fade = 0;
    uint32_t sound_event = 0;
    uint64_t sound_object = 0;
    bool sound_resolved = false;
} g_quality;
uint64_t g_quality_generation = 0;
// il2cpp_string_new, resolved by Initialize; Transform.Find takes managed paths.
void* (*g_quality_string_new)(const char*) = nullptr;
void QualityLog(const char* text) {
    if (g_quality.diagnostics) Log(text);
}
void FreeQualityRoot(QualityRoot& root) {
    if (root.lease && g_pose_leases) g_pose_leases->release(root.root, kId, root.lease);
    for (auto* bones : {&root.seam, &root.accessory})
        for (auto& bone : *bones) if (bone.pin) g_host->gchandle_free(g_host->context, bone.pin);
    for (auto pin : {root.animator_pin, root.root_pin}) if (pin) g_host->gchandle_free(g_host->context, pin);
    root = {};
}
bool QualityRootOwned(const QualityRoot& root) {
#if defined(_WIN32) || defined(BE_TEST_PC_POSE)
    return root.root && g_pose_leases && g_pose_leases->owns(root.root, kId, root.lease);
#else
    return root.root != nullptr;
#endif
}
bool QualityRootCurrent(const QualityRoot& root) {
    if (!QualityRootOwned(root) || !UnityObjectAlive(root.animator) || !UnityObjectAlive(root.root)) return false;
    bool ok = true;
    return Object(PoseComponentTransform, root.animator, ok) == root.root && ok;
}
// Put back native values for channels that still hold this module's write;
// native animation that rewrote a channel keeps its own value.
// Index loops: a reentrant release empties the vectors instead of leaving
// iterators behind.
void RestoreQualityBones(QualityRoot& root, std::vector<QualityBone>& bones, uint64_t generation) {
    for (size_t i = 0; i < bones.size(); ++i) {
        auto& bone = bones[i];
        if (generation != g_quality_generation) return;
        if (!bone.wrote || !UnityObjectAlive(bone.transform)) continue;
        bool ok = true;
        void* args[]{root.root};
        const bool child = Value<bool>(PoseIsChild, bone.transform, ok, args);
        if (generation != g_quality_generation) return;
        if (!child || !ok) continue;
        BonePose current;
        const bool read = PoseReadLocal(bone.transform, current);
        if (generation != g_quality_generation) return;
        if (!read) continue;
        const BonePose clean = PoseNativeBaseline(current, bone.last, bone.baseline, true);
        if (!PoseSame(clean.position, current.position) || !PoseSame(clean.rotation, current.rotation)) {
            PoseWriteLocal(bone.transform, clean);
            if (generation != g_quality_generation) return;
        }
        bone.wrote = false;
    }
}
void ReleaseLoopQuality(bool restore) {
    if (!g_quality.component) return;
    const uint64_t generation = ++g_quality_generation;
    QualityOwner old = std::move(g_quality);
    g_quality = {};
    for (auto* root : {&old.rider, &old.mount}) {
        if (!restore || !root->root || !QualityRootCurrent(*root)) continue;
        RestoreQualityBones(*root, root->seam, generation);
        RestoreQualityBones(*root, root->accessory, generation);
    }
    if (old.diagnostics && (old.seam_frames || old.accessory_frames)) {
        char text[192];
        std::snprintf(text, sizeof(text), "Sustained dash Jsspsi quality: released seam_frames=%u accessory_frames=%u mean_apply_us=%.1f max_apply_us=%.1f restored=%d.",
            old.seam_frames, old.accessory_frames, old.microseconds / std::max(1u, std::max(old.seam_frames, old.accessory_frames)),
            old.max_microseconds, restore);
        Log(text);
    }
    FreeQualityRoot(old.rider);
    FreeQualityRoot(old.mount);
}
// The owner exists only for a session whose profile carries quality data and
// whose configuration enables it; binding waits for the selected side.
void BeginLoopQuality(void* component, const CharacterProfile* profile) {
    ReleaseLoopQuality(true);
    g_quality = {};
    const auto config = g_session.config;
    if (!g_pose_contract || !profile || !profile->quality || !config || !config->jsspsi_quality ||
        g_session.component != component) return;
    g_quality.component = component;
    g_quality.handle = g_session.handle;
    g_quality.data = profile->quality;
    g_quality.diagnostics = config->diagnostics;
}
// Called from Cancel: no Unity calls here; the next TailLate restores the seam
// bones and fades the accessory bones it last wrote.
void EndLoopQuality() {
    if (!g_quality.component || g_quality.fading) return;
    if (g_stopping || GetCurrentThreadId() != g_game_thread.load(std::memory_order_relaxed)) {
        ReleaseLoopQuality(false);
        return;
    }
    g_quality.fading = true;
    g_quality.fade = g_quality.accessory_frames ? 1.f : 0.f;
}
bool QualitySeamArmed() {
    return g_quality.component && !g_quality.fading && g_quality.component == g_session.component &&
        g_quality.handle == g_session.handle && g_quality.seam_armed && !g_quality.seam_failed &&
        g_quality.rider.applying && g_quality.mount.applying &&
        QualityRootOwned(g_quality.rider) && QualityRootOwned(g_quality.mount);
}
void QualitySeamFailed(const char* reason) {
    if (g_quality.seam_failed) return;
    g_quality.seam_failed = true;
    g_quality.seam_armed = false;
    if (g_quality.diagnostics) {
        char text[192];
        std::snprintf(text, sizeof(text), "Sustained dash Jsspsi quality: seam unavailable (%s); lightweight crossfade kept.", reason);
        Log(text);
    }
}
// Check the current components before submitting a hard cut in PreLate. Old
// objects and their leases can survive a replacement until after TailLate.
float LoopQualityBlend(float blend) {
    if (!QualitySeamArmed()) return blend;
    const uint64_t generation = g_quality_generation;
    bool ok = true;
    const bool current = Object(GetAnimator, g_quality.component, ok) == g_quality.rider.animator && ok &&
        QualityRootCurrent(g_quality.rider) && TmpObjectAnimator(0) == g_quality.mount.animator &&
        QualityRootCurrent(g_quality.mount);
    if (generation != g_quality_generation) return blend;
    if (!current) {
        QualitySeamFailed("animator or root changed before rewind");
        return blend;
    }
    return 0.0f;
}
// The destination of a transition is what this frame renders; a zero-length
// rewind may still report one.
bool QualityReadState(void* animator, int& hash, float& time) {
    bool ok = true;
    int layer = 0;
    void* args[]{&layer};
    const bool transitioning = Value<bool>(InTransition, animator, ok, args);
    void* boxed = Invoke(transitioning ? NextState : CurrentState, animator, args, ok);
    void* state = boxed ? g_host->object_unbox(g_host->context, boxed) : nullptr;
    hash = Value<int>(StateHash, state, ok);
    time = Value<float>(StateTime, state, ok);
    return ok && std::isfinite(time);
}
bool BindQualityRoot(QualityRoot& root, void* animator, const QualityTree& tree, const QualitySeamEntry* seam,
    const LoopQualityData* accessory_data, size_t& missing, uint64_t generation) {
    const auto string_new = g_quality_string_new;
    bool ok = true;
    root.animator = animator;
    root.root = Object(PoseComponentTransform, animator, ok);
    if (!string_new || !ok || generation != g_quality_generation || !UnityObjectAlive(root.root)) return false;
    root.animator_pin = g_host->gchandle_new(g_host->context, animator, 1);
    root.root_pin = g_host->gchandle_new(g_host->context, root.root, 1);
    if (!root.animator_pin || !root.root_pin) return false;
#if defined(_WIN32) || defined(BE_TEST_PC_POSE)
    // Same non-preemptive ownership as the pose overlay: a VMD or another
    // visual writer on this root keeps it, and this dash stays lightweight.
    if (!g_pose_leases || !(root.lease = g_pose_leases->acquire(root.root, kId))) {
        Log("Sustained dash Jsspsi quality: pose ownership unavailable or another visual writer is active; lightweight loop kept.");
        return false;
    }
#endif
    std::string path;
    auto find = [&](uint16_t node, QualityBone& bone) {
        if (!QualityPath(tree, node, path)) return false;
        bool found = true;
        void* args[]{string_new(path.c_str())};
        if (!args[0]) return false;
        void* transform = Invoke(PoseFind, root.root, args, found);
        if (!found || !UnityObjectAlive(transform) || !PoseReadLocal(transform, bone.baseline)) return false;
        bone.transform = transform;
        bone.pin = g_host->gchandle_new(g_host->context, transform, 1);
        return bone.pin != 0;
    };
    // Accessory bones belong to the accessory lap for the whole dash, so the
    // seam never touches them, not even on the first lap.
    std::vector<uint16_t> claimed;
    if (accessory_data) {
        for (uint16_t i = 0; i < accessory_data->accessory_count; ++i) {
            QualityBone bone;
            bone.accessory = i;
            claimed.push_back(accessory_data->accessory_nodes[i]);
            const bool found = find(accessory_data->accessory_nodes[i], bone);
            if (generation != g_quality_generation) {
                if (bone.pin) g_host->gchandle_free(g_host->context, bone.pin);
                return false;
            }
            if (found) root.accessory.push_back(bone);
            else ++missing;
        }
    }
    for (uint16_t i = 0; i < seam->count; ++i) {
        if (std::find(claimed.begin(), claimed.end(), seam->bones[i].node) != claimed.end()) continue;
        QualityBone bone;
        bone.seam = &seam->bones[i];
        const bool found = find(seam->bones[i].node, bone);
        if (generation != g_quality_generation) {
            if (bone.pin) g_host->gchandle_free(g_host->context, bone.pin);
            return false;
        }
        if (found) root.seam.push_back(bone);
        else ++missing;
    }
    return true;
}
void BindQualityRider(void* animator, int hash, uint64_t generation) {
    const auto* data = g_quality.data;
    for (int side = 0; side < 2; ++side) if (data->rider_seam[side].state_hash == hash) g_quality.side = side;
    if (g_quality.side < 0 || data->accessory[g_quality.side].state_hash != hash) return; // Not the dash state yet.
    size_t missing = 0;
    const auto& entry = data->rider_seam[g_quality.side];
    const bool bound = BindQualityRoot(g_quality.rider, animator, data->rider, &entry, data, missing, generation);
    if (generation != g_quality_generation) return;
    const size_t matched = g_quality.rider.seam.size() + g_quality.rider.accessory.size();
    // The window must also be the one the data was cut for.
    const auto& loop = g_session.loop.c;
    const bool window = std::fabs(loop.begin - data->begin) < 5e-4f && std::fabs(loop.target - data->target) < 5e-4f;
    if (!bound || !window || matched * 5 < (matched + missing) * 4) {
        char text[192];
        std::snprintf(text, sizeof(text), "Sustained dash Jsspsi quality: rider binding rejected: matched=%zu missing=%zu window=%d; lightweight loop kept.",
            matched, missing, window);
        Log(text);
        FreeQualityRoot(g_quality.rider);
        g_quality.rider_failed = true;
        return;
    }
    if (g_quality.diagnostics) {
        char text[192];
        std::snprintf(text, sizeof(text), "Sustained dash Jsspsi quality: rider bound side=%s seam=%zu accessory=%zu missing=%zu.",
            g_quality.side == 0 ? "right" : "left", g_quality.rider.seam.size(), g_quality.rider.accessory.size(), missing);
        Log(text);
    }
}
void BindQualityMount(uint64_t generation) {
    void* animator = TmpObjectAnimator(0);
    if (generation != g_quality_generation || !animator) return;
    int hash = 0;
    float time = 0;
    if (!QualityReadState(animator, hash, time) || generation != g_quality_generation) return;
    if (hash != g_quality.data->mount_seam.state_hash) return; // Not spawned into its dash state yet.
    size_t missing = 0;
    const bool bound = BindQualityRoot(g_quality.mount, animator, g_quality.data->mount, &g_quality.data->mount_seam, nullptr, missing, generation);
    if (generation != g_quality_generation) return;
    if (!bound || g_quality.mount.seam.size() * 5 < (g_quality.mount.seam.size() + missing) * 4) {
        FreeQualityRoot(g_quality.mount);
        QualitySeamFailed("mount binding rejected");
        return;
    }
    if (g_quality.diagnostics) {
        char text[128];
        std::snprintf(text, sizeof(text), "Sustained dash Jsspsi quality: mount bound seam=%zu missing=%zu.", g_quality.mount.seam.size(), missing);
        Log(text);
    }
}
// Seam phase per root: the first natural lap enters at its target without the
// start half; every later lap is a rewind and takes both halves.
void UpdateQualitySeamState(QualityRoot& root, int hash, float time, int32_t expected) {
    const float target = g_quality.data->target, begin = g_quality.data->begin;
    if (hash != expected) {
        if (root.applying) QualitySeamFailed("state changed");
        root.applying = false; root.last_time = -1;
        return;
    }
    const bool first = !root.applying && root.last_time < target && time >= target;
    const bool rewound = root.applying && root.last_time >= 0 && time < root.last_time - kQualityRewindThreshold;
    root.last_time = time;
    if (first) {
        // Arming after the end half has started would add the half in one step.
        if ((time - target) / (begin - target) > 1 - kQualitySeamWindow) QualitySeamFailed("bound after the first half lap");
        root.applying = true; root.from_rewind = false;
    } else if (rewound) root.from_rewind = true;
}
// false: stop this frame (owner replaced or a bone left its root).
bool WriteQualitySeam(QualityRoot& root, float time, uint64_t generation) {
    const auto* data = g_quality.data;
    const float phase = std::clamp((time - data->target) / (data->begin - data->target), 0.f, 1.f);
    const float share = QualitySeamShare(phase, root.from_rewind);
    for (size_t i = 0; i < root.seam.size(); ++i) {
        auto& bone = root.seam[i];
        if (share == 0 && !bone.wrote) continue;
        bool ok = true;
        void* args[]{root.root};
        const bool child = Value<bool>(PoseIsChild, bone.transform, ok, args);
        if (generation != g_quality_generation) return false;
        BonePose current;
        const bool read = ok && child && PoseReadLocal(bone.transform, current);
        if (generation != g_quality_generation) return false;
        if (!read) {
            QualitySeamFailed("bone left its root");
            return false;
        }
        bone.baseline = PoseNativeBaseline(current, bone.last, bone.baseline, bone.wrote);
        const BonePose output = QualitySeamApply(bone.baseline, *bone.seam, share);
        const bool wrote = PoseWriteLocal(bone.transform, output);
        if (generation != g_quality_generation) return false;
        if (!wrote) {
            QualitySeamFailed("bone write failed");
            return false;
        }
        bone.last = output; bone.wrote = share != 0;
    }
    return true;
}
// fade < 0 samples the lap at phase; otherwise blend the held pose toward
// native with the remaining fade weight.
bool WriteQualityAccessory(QualityRoot& root, double phase, float fade, uint64_t generation) {
    const float w = fade * fade * (3 - 2 * fade);
    size_t written = 0;
    for (size_t i = 0; i < root.accessory.size(); ++i) {
        auto& bone = root.accessory[i];
        if (fade >= 0 && !bone.wrote) continue;
        bool ok = true;
        void* args[]{root.root};
        const bool child = Value<bool>(PoseIsChild, bone.transform, ok, args);
        if (generation != g_quality_generation) return false;
        BonePose current;
        const bool read = ok && child && PoseReadLocal(bone.transform, current);
        if (generation != g_quality_generation || !read) return false;
        bone.baseline = PoseNativeBaseline(current, bone.last, bone.baseline, bone.wrote);
        if (fade < 0) bone.held = QualityAccessorySample(*g_quality.data, g_quality.side, bone.accessory, phase);
        const BonePose output = fade < 0 ? bone.held : PoseBlend(bone.baseline, bone.held, w);
        if (!PoseWriteLocal(bone.transform, output) || generation != g_quality_generation) return false;
        bone.last = output; bone.wrote = true; ++written;
    }
    return written != 0;
}
void QualityFadeFrame(void* component, float delta, uint64_t generation) {
    const auto config = g_config.load();
    auto& rider = g_quality.rider;
    bool ok = true;
    const bool current = !g_stopping && rider.root && QualityRootCurrent(rider) &&
        Object(GetAnimator, component, ok) == rider.animator && ok;
    if (generation != g_quality_generation) return;
    // Turning the option off ends the fade, but still hands back the channels
    // native animation does not rewrite.
    if (!current || !config->enabled || !config->jsspsi_quality || !std::isfinite(delta) || delta < 0) {
        ReleaseLoopQuality(current);
        return;
    }
    const int frame = Unbox<int>(Invoke(PoseFrameCount, nullptr, nullptr, ok), ok);
    if (generation != g_quality_generation) return;
    if (!ok) { ReleaseLoopQuality(true); return; }
    if (frame == g_quality.last_frame) return;
    g_quality.last_frame = frame;
    // The body seam drops at once, as in the lightweight loop's own exit.
    if (g_quality.mount.root) {
        if (QualityRootCurrent(g_quality.mount)) RestoreQualityBones(g_quality.mount, g_quality.mount.seam, generation);
        if (generation != g_quality_generation) return;
        FreeQualityRoot(g_quality.mount);
    }
    RestoreQualityBones(rider, rider.seam, generation);
    if (generation != g_quality_generation) return;
    g_quality.fade = std::max(0.f, g_quality.fade - delta / kQualityFadeSeconds);
    if (g_quality.fade <= 0 || !WriteQualityAccessory(rider, 0, g_quality.fade, generation)) {
        if (generation == g_quality_generation) ReleaseLoopQuality(true);
    }
}
void ApplyLoopQuality(void* component, float delta) {
    if (!g_quality.component || component != g_quality.component ||
        GetCurrentThreadId() != g_game_thread.load(std::memory_order_relaxed)) return;
    const uint64_t generation = g_quality_generation;
    if (g_stopping) { ReleaseLoopQuality(false); return; }
    if (g_quality.fading) { QualityFadeFrame(component, delta, generation); return; }
    // Cancel ends the owner; until then a mismatched frame only skips writes.
    const auto config = g_config.load();
    if (g_session.component != component || g_session.handle != g_quality.handle || g_session.config != config ||
        !config->enabled || !config->jsspsi_quality || !g_session.policy.active || g_quality.rider_failed) return;
    bool ok = true;
    const int frame = Unbox<int>(Invoke(PoseFrameCount, nullptr, nullptr, ok), ok);
    if (generation != g_quality_generation || !ok || frame == g_quality.last_frame) return;
    g_quality.last_frame = frame;
    LARGE_INTEGER begin{}, end{}, frequency{};
    QueryPerformanceFrequency(&frequency); QueryPerformanceCounter(&begin);
    void* animator = Object(GetAnimator, component, ok);
    int hash = 0;
    float time = 0;
    if (!ok || !QualityReadState(animator, hash, time) || generation != g_quality_generation) return;
    auto& rider = g_quality.rider;
    if (!rider.root) {
        BindQualityRider(animator, hash, generation);
        if (generation != g_quality_generation || !rider.root) return;
    }
    if (rider.animator != animator || !QualityRootCurrent(rider)) {
        if (generation != g_quality_generation) return;
        Log("Sustained dash Jsspsi quality: rider root or pose ownership changed; lightweight loop kept.");
        ReleaseLoopQuality(false);
        return;
    }
    const int32_t rider_hash = g_quality.data->rider_seam[g_quality.side].state_hash;
    if (!g_quality.seam_failed) {
        if (!g_quality.mount.root) BindQualityMount(generation);
        if (generation != g_quality_generation) return;
        UpdateQualitySeamState(rider, hash, time, rider_hash);
        int mount_hash = 0;
        float mount_time = 0;
        if (g_quality.mount.root) {
            if (TmpObjectAnimator(0) != g_quality.mount.animator || !QualityRootCurrent(g_quality.mount) ||
                !QualityReadState(g_quality.mount.animator, mount_hash, mount_time))
                QualitySeamFailed("mount root or pose ownership changed");
            else UpdateQualitySeamState(g_quality.mount, mount_hash, mount_time, g_quality.data->mount_seam.state_hash);
            if (generation != g_quality_generation) return;
        } else if (rider.applying && (time - g_quality.data->target) / (g_quality.data->begin - g_quality.data->target) > 1 - kQualitySeamWindow)
            QualitySeamFailed("mount not bound before the first half lap");
        // The pair arms once, while both are still in the first half of the same
        // natural lap and in step; arming later would add up to half of D at once.
        if (!g_quality.seam_armed && !g_quality.seam_failed && rider.applying && g_quality.mount.applying) {
            auto progress = [&](float u) { return (u - g_quality.data->target) / (g_quality.data->begin - g_quality.data->target); };
            const float rider_progress = progress(time), mount_progress = progress(mount_time);
            if (rider.from_rewind || g_quality.mount.from_rewind || rider_progress > 1 - kQualitySeamWindow ||
                mount_progress > 1 - kQualitySeamWindow || std::fabs(rider_progress - mount_progress) > kQualityArmTolerance) {
                char reason[128];
                std::snprintf(reason, sizeof(reason), "rider and mount out of step: rider=%.3f mount=%.3f", rider_progress, mount_progress);
                QualitySeamFailed(reason);
            } else {
                g_quality.seam_armed = true;
                QualityLog("Sustained dash Jsspsi quality: seam armed for rider and mount; rewinds are hard cuts.");
            }
        }
        if (QualitySeamArmed()) {
            if (WriteQualitySeam(rider, time, generation) && WriteQualitySeam(g_quality.mount, mount_time, generation)) ++g_quality.seam_frames;
            if (generation != g_quality_generation) return;
        }
    }
    if (g_quality.seam_failed) {
        // PreLate can reject the pair before this TailLate starts. Hand back
        // constant channels on unchanged roots on that same frame as well.
        RestoreQualityBones(rider, rider.seam, generation);
        if (generation != g_quality_generation) return;
        if (g_quality.mount.root && QualityRootCurrent(g_quality.mount)) RestoreQualityBones(g_quality.mount, g_quality.mount.seam, generation);
        if (generation != g_quality_generation) return;
        FreeQualityRoot(g_quality.mount);
    }
    // The lap starts with the first rewind request: until then the native
    // first lap plays, including its one swing on mounting.
    double phase = 0;
    if (g_session.animation_replays && hash == rider_hash && QualityPhase(time, g_quality.data->begin, g_quality.data->target, phase)) {
        if (WriteQualityAccessory(rider, phase, -1, generation)) {
            if (++g_quality.accessory_frames == 1 && g_quality.diagnostics) {
                char text[160];
                std::snprintf(text, sizeof(text), "Sustained dash Jsspsi quality: accessory lap started u=%.3f phi=%.3f bones=%zu.", time, phase, rider.accessory.size());
                Log(text);
            }
        } else if (generation == g_quality_generation && !rider.accessory.empty()) {
            Log("Sustained dash Jsspsi quality: accessory bone left its root; quality ended for this dash.");
            ReleaseLoopQuality(true);
            return;
        }
        if (generation != g_quality_generation) return;
    }
    QueryPerformanceCounter(&end);
    // Per-frame cost of the write pass; the peak matters more than the mean
    // because one long frame is what a hitch is made of.
    const double used = (end.QuadPart - begin.QuadPart) * 1e6 / frequency.QuadPart;
    g_quality.microseconds += used;
    if (used > g_quality.max_microseconds) g_quality.max_microseconds = used;
}
// Game 1.6.4 global-metadata.dat, AK.Wwise.Unity.API.dll enum fields:
// AkActionOnEventType_Stop = 0, AkCurveInterpolation_Linear = 4,
// AKRESULT.AK_Success = 1 (offline metadata audit, 2026-10-26).
constexpr int32_t kQualitySoundStop = 0, kQualitySoundLinear = 4, kQualitySoundSuccess = 1;
// AnimatorMono posts directly to AudioAdapter, bypassing the Entity PostEvent
// hook. Capture its entity's audio object and stop only this event on it.
void CaptureQualitySound(void* mono, uint32_t hash) {
    if (GetCurrentThreadId() != g_game_thread.load(std::memory_order_relaxed)) return;
    if (!g_quality.component || g_quality.fading || g_quality.sound_object || g_quality.sound_resolved ||
        !g_session.component || g_quality.component != g_session.component || g_session.animation_replays ||
        !g_session.profile || !g_session.profile->start_sound_event || hash != g_session.profile->start_sound_event ||
        !g_methods[JsspsiAudioObjectId].resolved.method_info || !g_methods[JsspsiStopSound].resolved.method_info) return;
    const uint64_t generation = g_quality_generation;
    bool ok = true;
    void* component = Object(AudioMonoComponent, mono, ok);
    if (generation != g_quality_generation || !ok || component != g_session.component) return;
    void* args[]{g_session.entity};
    const uint64_t object = Unbox<uint64_t>(Invoke(JsspsiAudioObjectId, nullptr, args, ok), ok);
    if (generation != g_quality_generation || !ok || !object || object == UINT64_MAX) return;
    g_quality.sound_event = hash;
    g_quality.sound_object = object;
}
void StopQualitySound() {
    if (!g_quality.component || g_quality.sound_resolved || g_quality.component != g_session.component) return;
    g_quality.sound_resolved = true;
    if (!g_quality.sound_object) {
        QualityLog("Sustained dash Jsspsi quality: dash sound object not captured; native sound kept.");
        return;
    }
    uint32_t event = g_quality.sound_event, playing = 0;
    uint64_t object = g_quality.sound_object;
    int32_t action = kQualitySoundStop, fade = 300, curve = kQualitySoundLinear;
    const bool diagnostics = g_quality.diagnostics;
    void* args[]{&event, &action, &object, &fade, &curve, &playing};
    bool ok = true;
    const int32_t result = Unbox<int32_t>(Invoke(JsspsiStopSound, nullptr, args, ok), ok);
    if (diagnostics) {
        char text[192];
        std::snprintf(text, sizeof(text), "Sustained dash Jsspsi quality: dash sound stop requested event=%u object=%llu result=%d ok=%d.",
            event, static_cast<unsigned long long>(object), result, ok && result == kQualitySoundSuccess);
        Log(text);
    }
}
