        "m_Weight0", "System.Single"}, kBoxedValueHeaderSize + 0},
    {"weight1", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_Weight1", "System.Single"}, kBoxedValueHeaderSize + 4},
    {"weight2", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_Weight2", "System.Single"}, kBoxedValueHeaderSize + 8},
    {"weight3", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_Weight3", "System.Single"}, kBoxedValueHeaderSize + 12},
    {"index0", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_BoneIndex0", "System.Int32"}, kBoxedValueHeaderSize + 16},
    {"index1", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_BoneIndex1", "System.Int32"}, kBoxedValueHeaderSize + 20},
    {"index2", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_BoneIndex2", "System.Int32"}, kBoxedValueHeaderSize + 24},
    {"index3", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_BoneIndex3", "System.Int32"}, kBoxedValueHeaderSize + 28},
}};

MethodContract* Contract(std::string_view key) {
    for (auto& method : g_methods) {
        if (method.key == key) return &method;
    }
    return nullptr;
}

void Log(const std::string& message) {
    if (g_host && g_host->log) {
        g_host->log(g_host->context, kModuleId, message.c_str());
    }
}

void* Invoke(const MethodContract* method, void* instance, void** parameters,
    bool log_exception = true) {
    if (!method || !method->method_info || !g_host || !g_host->runtime_invoke) {
        return nullptr;
    }
    void* exception = nullptr;
    void* result = g_host->runtime_invoke(
        g_host->context, method->method_info, instance, parameters, &exception);
    if (exception && log_exception) {
        Log(std::string("Managed exception from ") + method->key);
    }
    return exception ? nullptr : result;
}

template <typename T>
bool Unbox(void* boxed, T& value) {
    if (!boxed || !g_host || !g_host->object_unbox) return false;
    void* raw = g_host->object_unbox(g_host->context, boxed);
    if (!raw) return false;
    std::memcpy(&value, raw, sizeof(T));
    return true;
}

template <typename T>
bool InvokeValue(const MethodContract* method, void* instance, void** parameters,
    T& value) {
    return Unbox(Invoke(method, instance, parameters), value);
}

bool InvokeVoid(const MethodContract* method, void* instance, void** parameters) {
    if (!method || !method->resolved || !g_host || !g_host->runtime_invoke) {
        return false;
    }
    void* exception = nullptr;
    g_host->runtime_invoke(
        g_host->context, method->method_info, instance, parameters, &exception);
    if (exception) {
        Log(std::string("Managed exception from ") + method->key);
        return false;
    }
    return true;
}

std::string ManagedString(void* value) {
    if (!value || !g_host || !g_host->copy_managed_string) return {};
    std::array<char, 1024> buffer{};
    const int copied = g_host->copy_managed_string(
        g_host->context, value, buffer.data(), buffer.size());
    return copied > 0 ? std::string(buffer.data(), static_cast<size_t>(copied))
                      : std::string{};
}

std::string ObjectName(void* object) {
    return object
        ? ManagedString(Invoke(Contract("object.get_name"), object, nullptr, false))
        : std::string{};
}

int ArrayLength(void* array) {
    int dimension = 0;
    int length = 0;
    void* parameters[1]{&dimension};
    return array &&
            InvokeValue(Contract("array.get_length"), array, parameters, length)
        ? length
        : 0;
}

void* ArrayValue(void* array, int index) {
    void* parameters[1]{&index};
    return array
        ? Invoke(Contract("array.get_value"), array, parameters, false)
        : nullptr;
}

std::string BuildTransformPath(void* component) {
    std::vector<std::string> names;
    void* transform = Invoke(Contract("component.get_transform"), component, nullptr, false);
    for (int depth = 0; transform && depth < 24; ++depth) {
        std::string name = ObjectName(transform);
        names.push_back(name.empty() ? "<unnamed>" : std::move(name));
        transform = Invoke(Contract("transform.get_parent"), transform, nullptr, false);
    }
    std::reverse(names.begin(), names.end());

    std::string path;
    for (const auto& name : names) {
        if (!path.empty()) path.push_back('/');
        path += name;
    }
    return path;
}

bool IsLiveClonePath(std::string_view path) {
    return path.find("(Clone)") != std::string_view::npos;
}

bool IsGameplayPostModelPath(std::string_view path) {
    return path.find("chr_0003_endminf_postmodel(Clone)") !=
        std::string_view::npos;
}

bool IsUiModelPath(std::string_view path) {
    return path.find("chr_0003_endminf_uimodel(Clone)") !=
        std::string_view::npos;
}

bool IsSupportedLiveTargetPath(std::string_view path) {
    return IsGameplayPostModelPath(path) || IsUiModelPath(path);
}

int PathLod(std::string_view path) {
    if (path.find("/Mesh_all/lod0/") != std::string_view::npos) return 0;
    if (path.find("/Mesh_all/lod1/") != std::string_view::npos) return 1;
    return -1;
}

std::filesystem::path Utf8Path(std::string_view value) {
    if (value.empty()) return {};
    const int wide_count = MultiByteToWideChar(
        CP_UTF8, MB_ERR_INVALID_CHARS, value.data(),
        static_cast<int>(value.size()), nullptr, 0);
    if (wide_count <= 0) return std::filesystem::path(std::string(value));
    std::wstring wide(static_cast<size_t>(wide_count), L'\0');
    MultiByteToWideChar(
        CP_UTF8, MB_ERR_INVALID_CHARS, value.data(),
        static_cast<int>(value.size()), wide.data(), wide_count);
    return std::filesystem::path(wide);
}

std::filesystem::path BemPath() {
    if (!g_host || !g_host->copy_catalog_root) return {};
    std::array<char, 4096> root{};
    if (g_host->copy_catalog_root(
            g_host->context, root.data(), root.size()) <= 0) {
        return {};
    }
    return Utf8Path(root.data()) / Utf8Path(kDefaultBemRelativePath);
}

bool CheckedAdvance(size_t& cursor, size_t amount, size_t limit) {
    if (amount > limit || cursor > limit - amount) return false;
    cursor += amount;
    return true;
}

bool LoadBemPoc(const std::filesystem::path& path, BemPocData& output) {
    output = {};
    std::ifstream input(path, std::ios::binary | std::ios::ate);
    if (!input) {
        Log("BEM PoC file not found: " + path.string());
        return false;
    }

    const std::streamoff end = input.tellg();
    if (end < static_cast<std::streamoff>(sizeof(BemPocHeader)) ||
        end > static_cast<std::streamoff>(256 * 1024 * 1024)) {
        Log("BEM PoC file size is invalid.");
        return false;
    }

    std::vector<uint8_t> bytes(static_cast<size_t>(end));
    input.seekg(0, std::ios::beg);
    if (!input.read(reinterpret_cast<char*>(bytes.data()),
            static_cast<std::streamsize>(bytes.size()))) {
        Log("Failed to read BEM PoC file.");
        return false;
    }

    std::memcpy(&output.header, bytes.data(), sizeof(output.header));
    const std::array<char, 8> expected_magic{'B','E','M','P','O','C','3','\0'};
    if (std::memcmp(output.header.magic, expected_magic.data(), expected_magic.size()) != 0 ||
        output.header.version != 3 || output.header.component_id != 9 ||
        output.header.vertex_count != kExpectedBemVertices ||
        output.header.index_count != kExpectedBemIndices ||
        output.header.max_bone_lod0 != kExpectedBemMaxBoneLod0 ||
        output.header.max_bone_lod1 != kExpectedBemMaxBoneLod1 ||
        output.header.original_index_count_lod0 != kComponent9OriginalIndexCountLod0 ||
        output.header.original_index_count_lod1 != kComponent9OriginalIndexCountLod1 ||
        output.header.vb0_stride != kExpectedVb0Stride ||
        output.header.vb1_lod0_stride != kExpectedVb1Lod0Stride ||
        output.header.vb2_lod0_stride != kExpectedVb2Lod0Stride ||
        output.header.vb1_lod1_stride != kExpectedVb1Lod1Stride ||
        output.header.vb2_lod1_stride != kExpectedVb2Lod1Stride) {
        Log("BEM PoC-3 header does not match validated Endmin Casualwear C9 raw layout.");
        return false;
    }

    size_t cursor = sizeof(BemPocHeader);
    auto take = [&](std::vector<uint8_t>& dst, size_t size) -> bool {
        if (cursor > bytes.size() || size > bytes.size() - cursor) return false;
        dst.assign(bytes.begin() + static_cast<std::ptrdiff_t>(cursor),
                   bytes.begin() + static_cast<std::ptrdiff_t>(cursor + size));
        cursor += size;
        return true;
    };

    const size_t vertices = output.header.vertex_count;
    if (!take(output.vb0, vertices * output.header.vb0_stride) ||
        !take(output.vb1_lod0, vertices * output.header.vb1_lod0_stride) ||
        !take(output.vb2_lod0, vertices * output.header.vb2_lod0_stride) ||
        !take(output.vb1_lod1, vertices * output.header.vb1_lod1_stride) ||
        !take(output.vb2_lod1, vertices * output.header.vb2_lod1_stride)) {
        Log("BEM PoC-3 vertex stream payload is truncated.");
        return false;
    }

    const size_t index_bytes = static_cast<size_t>(output.header.index_count) * sizeof(uint16_t);
    if (cursor > bytes.size() || index_bytes != bytes.size() - cursor) {
        Log("BEM PoC-3 index payload size mismatch.");
        return false;
    }
    output.indices.resize(output.header.index_count);
    std::memcpy(output.indices.data(), bytes.data() + cursor, index_bytes);
    for (uint16_t index : output.indices) {
        if (index >= output.header.vertex_count) {