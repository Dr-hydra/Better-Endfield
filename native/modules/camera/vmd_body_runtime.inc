// VMD body playback for the camera module.
//
// This is a deliberately small, contract-driven retargeting layer.  It uses
// the current model hierarchy and Transform accessors discovered at startup;
// it does not depend on EIEM's IL2CPP field offsets, GhostRig layout, or a
// particular character prefab.

namespace {

struct BodyBinding {
    std::string vmd_name;
    void* transform = nullptr;
    uint32_t handle = 0;
    Vector3 base_position{};
    Quaternion base_rotation{};
};

struct BodyPlayback {
    BetterEndfield::Vmd::Motion motion;
    std::vector<BodyBinding> bindings;
    uint64_t started_ms = 0;
    double last_frame = 0.0;
    uint32_t frame_count = 0;
    bool active = false;
};

BodyPlayback g_vmd_body;

std::string DecodeVmdName(std::string_view bytes) {
    if (bytes.empty()) return {};
    int wide_count = MultiByteToWideChar(932, MB_ERR_INVALID_CHARS,
        bytes.data(), static_cast<int>(bytes.size()), nullptr, 0);
    if (wide_count <= 0) {
        wide_count = MultiByteToWideChar(932, 0, bytes.data(),
            static_cast<int>(bytes.size()), nullptr, 0);
    }
    if (wide_count <= 0) return {};
    std::wstring wide(static_cast<size_t>(wide_count), L'\0');
    if (MultiByteToWideChar(932, 0, bytes.data(), static_cast<int>(bytes.size()),
        wide.data(), wide_count) != wide_count) return {};
    const int utf8_count = WideCharToMultiByte(CP_UTF8, 0, wide.data(), wide_count,
        nullptr, 0, nullptr, nullptr);
    if (utf8_count <= 0) return {};
    std::string utf8(static_cast<size_t>(utf8_count), '\0');
    if (WideCharToMultiByte(CP_UTF8, 0, wide.data(), wide_count, utf8.data(),
        utf8_count, nullptr, nullptr) != utf8_count) return {};
    return utf8;
}

Quaternion NormalizeBodyQuaternion(Quaternion value) {
    const float length = std::sqrt(value.x * value.x + value.y * value.y +
        value.z * value.z + value.w * value.w);
    if (!std::isfinite(length) || length <= 1.0e-6f) return {};
    const float inverse = 1.0f / length;
    value.x *= inverse;
    value.y *= inverse;
    value.z *= inverse;
    value.w *= inverse;
    return value;
}

Quaternion MultiplyBodyQuaternion(Quaternion a, Quaternion b) {
    return NormalizeBodyQuaternion({
        a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
        a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
        a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
        a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z});
}

struct BodyNode {
    std::string name;
    void* transform = nullptr;
};

void ScanBodyNodes(void* transform, int depth, int& visited,
    std::vector<BodyNode>& nodes) {
    if (!transform || depth > 18 || visited >= 4096) return;
    ++visited;
    nodes.push_back({ObjectName(transform), transform});
    int children = 0;
    if (!GetValue(Contract("unity.transform.child_count.get"), transform, children)) return;
    for (int index = 0; index < children; ++index) {
        void* parameters[1]{&index};
        ScanBodyNodes(Invoke(Contract("unity.transform.get_child"), transform,
            parameters), depth + 1, visited, nodes);
    }
}

void* FindBodyNode(const std::vector<BodyNode>& nodes, std::string_view raw_name) {
    const std::string decoded = DecodeVmdName(raw_name);
    for (const BodyNode& node : nodes) {
        if (!node.transform) continue;
        if (node.name == raw_name || (!decoded.empty() && node.name == decoded)) {
            return node.transform;
        }
    }
    return nullptr;
}

void ReleaseBodyBindings() {
    for (BodyBinding& binding : g_vmd_body.bindings) {
        if (binding.handle && g_host && g_host->gchandle_free) {
            g_host->gchandle_free(g_host->context, binding.handle);
        }
    }
    g_vmd_body.bindings.clear();
}

void StopVmdBody(const char* reason) {
    if (!g_vmd_body.active) return;
    for (BodyBinding& binding : g_vmd_body.bindings) {
        if (!binding.transform || !IsObjectAlive(binding.transform)) continue;
        SetValue(Contract("unity.transform.local_position.set"), binding.transform,
            binding.base_position);
        SetValue(Contract("unity.transform.local_rotation.set"), binding.transform,
            binding.base_rotation);
    }
    ReleaseBodyBindings();
    g_vmd_body.motion = {};
    g_vmd_body.active = false;
    g_vmd_body.last_frame = 0.0;
    g_vmd_body.frame_count = 0;
    if (reason) Log(std::string("VMD body playback stopped: ") + reason + ".");
}

bool LoadVmdBody() {
    if (!g_vmd_body_contract_ready) {
        Log("VMD body: required Transform contracts are unavailable.");
        return false;
    }
    std::string path;
    {
        std::lock_guard<std::mutex> lock(g_vmd_body_path_mutex);
        path = g_vmd_body_file;
    }
    if (path.empty()) {
        Log("VMD body: no vmd_body_file is configured.");
        return false;
    }
    std::ifstream file(path, std::ios::binary | std::ios::ate);
    if (!file) {
        Log("VMD body: could not open the configured file.");
        return false;
    }
    const std::streamoff size = file.tellg();
    if (size <= 0 || size > 256 * 1024 * 1024) {
        Log("VMD body: file is empty or exceeds the 256 MiB limit.");
        return false;
    }
    file.seekg(0, std::ios::beg);
    std::vector<uint8_t> bytes(static_cast<size_t>(size));
    if (!file.read(reinterpret_cast<char*>(bytes.data()), size)) {
        Log("VMD body: file read failed.");
        return false;
    }
    BetterEndfield::Vmd::Document document;
    std::string error;
    if (!BetterEndfield::Vmd::Parse(bytes, document, error) || document.bones.empty()) {
        Log("VMD body: parse failed: " + error + ".");
        return false;
    }
    void* root = FindModelTransform();
    if (!root) {
        Log("VMD body: the main character model is not loaded yet.");
        return false;
    }
    std::vector<BodyNode> nodes;
    int visited = 0;
    ScanBodyNodes(root, 0, visited, nodes);
    if (nodes.empty()) {
        Log("VMD body: model hierarchy is empty.");
        return false;
    }

    StopVmdBody("reloading");
    g_vmd_body.motion = BetterEndfield::Vmd::BuildMotion(document);
    for (const auto& entry : g_vmd_body.motion.bones) {
        void* transform = FindBodyNode(nodes, entry.first);
        if (!transform) continue;
        BodyBinding binding;
        binding.vmd_name = entry.first;
        binding.transform = transform;
        binding.handle = g_host && g_host->gchandle_new
            ? g_host->gchandle_new(g_host->context, transform, 0) : 0;
        if (!GetValue(Contract("unity.transform.local_position.get"), transform,
                binding.base_position) ||
            !GetValue(Contract("unity.transform.local_rotation.get"), transform,
                binding.base_rotation)) {
            if (binding.handle && g_host && g_host->gchandle_free)
                g_host->gchandle_free(g_host->context, binding.handle);
            continue;
        }
        binding.base_rotation = NormalizeBodyQuaternion(binding.base_rotation);
        g_vmd_body.bindings.push_back(std::move(binding));
    }
    if (g_vmd_body.bindings.empty()) {
        g_vmd_body.motion = {};
        Log("VMD body: no VMD bone names matched the live model hierarchy.");
        return false;
    }
    for (const auto& entry : g_vmd_body.motion.bones) {
        if (!entry.second.empty()) {
            g_vmd_body.frame_count = (std::max)(g_vmd_body.frame_count,
                entry.second.back().frame);
        }
    }
    g_vmd_body.started_ms = GetTickCount64();
    g_vmd_body.last_frame = 0.0;
    g_vmd_body.active = true;
    Log("VMD body playback started: " + std::to_string(g_vmd_body.bindings.size()) +
        " matched bones, " + std::to_string(g_vmd_body.frame_count) + " frames.");
    return true;
}

void ApplyVmdBody() {
    if (!g_vmd_body.active) return;
    const double elapsed = static_cast<double>(GetTickCount64() - g_vmd_body.started_ms) / 1000.0;
    const double duration = static_cast<double>(g_vmd_body.frame_count) / 30.0;
    if (duration <= 0.0) {
        StopVmdBody("empty duration");
        return;
    }
    double frame = elapsed * 30.0;
    const bool loop = g_vmd_body_loop.load(std::memory_order_relaxed);
    if (frame > g_vmd_body.frame_count) {
        if (!loop) {
            StopVmdBody("finished");
            return;
        }
        frame = std::fmod(frame, static_cast<double>(g_vmd_body.frame_count));
    }
    g_vmd_body.last_frame = frame;
    const float scale = g_vmd_body_scale.load(std::memory_order_relaxed);
    for (BodyBinding& binding : g_vmd_body.bindings) {
        if (!binding.transform || !IsObjectAlive(binding.transform)) continue;
        BetterEndfield::Vmd::BoneSample sample;
        if (!BetterEndfield::Vmd::SampleBone(g_vmd_body.motion, binding.vmd_name,
                frame, sample)) continue;
        const Vector3 position{
            binding.base_position.x + sample.position.x * scale,
            binding.base_position.y + sample.position.y * scale,
            binding.base_position.z + sample.position.z * scale};
        const Quaternion rotation = MultiplyBodyQuaternion(binding.base_rotation,
            {sample.rotation.x, sample.rotation.y, sample.rotation.z, sample.rotation.w});
        SetValue(Contract("unity.transform.local_position.set"), binding.transform, position);
        SetValue(Contract("unity.transform.local_rotation.set"), binding.transform, rotation);
    }
}

void PumpVmdBody() {
    if (g_vmd_body_play_request.exchange(false, std::memory_order_acq_rel)) {
        if (g_vmd_body.active) StopVmdBody("toggle");
        else LoadVmdBody();
    }
    ApplyVmdBody();
}

} // namespace
