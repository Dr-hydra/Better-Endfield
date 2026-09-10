
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

bool IsLod0Path(std::string_view path) {
    return path.find("/Mesh_all/lod0/") != std::string_view::npos;
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
    const std::array<char, 8> expected_magic{
        'B','E','M','P','O','C','2','\0'};
    const uint32_t required_flags =
        kBemFlagUv0 | kBemFlagSkin4 | kBemFlagIndex16 |
        kBemFlagNormal | kBemFlagColor0;

    if (std::memcmp(output.header.magic, expected_magic.data(),
            expected_magic.size()) != 0 ||
        output.header.version != 2 ||
        output.header.component_id != 9 ||
        output.header.original_index_count != kComponent9OriginalIndexCount ||
        output.header.vertex_count != kExpectedBemVertices ||
        output.header.index_count != kExpectedBemIndices ||
        output.header.max_bone != kExpectedBemMaxBone ||
        (output.header.flags & required_flags) != required_flags) {
        Log("BEM PoC header does not match validated Endmin Casualwear C9.");
        return false;
    }

    size_t cursor = sizeof(BemPocHeader);
    const size_t position_bytes =
        static_cast<size_t>(output.header.vertex_count) * sizeof(Float3);
    const size_t uv_bytes =
        static_cast<size_t>(output.header.vertex_count) * sizeof(Float2);
    const size_t normal_bytes =
        static_cast<size_t>(output.header.vertex_count) * sizeof(Float3);
    const size_t color_bytes =
        static_cast<size_t>(output.header.vertex_count) * sizeof(Float4);
    const size_t weight_bytes =
        static_cast<size_t>(output.header.vertex_count) * sizeof(float) * 4;
    const size_t bone_index_bytes =
        static_cast<size_t>(output.header.vertex_count) * 4;
    const size_t index_bytes =
        static_cast<size_t>(output.header.index_count) * sizeof(uint16_t);
    const size_t expected_size = cursor + position_bytes + uv_bytes +
        normal_bytes + color_bytes + weight_bytes + bone_index_bytes +
        index_bytes;

    if (expected_size != bytes.size()) {
        Log("BEM PoC payload size mismatch.");
        return false;
    }

    output.positions.resize(output.header.vertex_count);
    std::memcpy(
        output.positions.data(), bytes.data() + cursor, position_bytes);
    if (!CheckedAdvance(cursor, position_bytes, bytes.size())) return false;

    output.uvs.resize(output.header.vertex_count);
    std::memcpy(output.uvs.data(), bytes.data() + cursor, uv_bytes);
    if (!CheckedAdvance(cursor, uv_bytes, bytes.size())) return false;

    output.normals.resize(output.header.vertex_count);
    std::memcpy(output.normals.data(), bytes.data() + cursor, normal_bytes);
    if (!CheckedAdvance(cursor, normal_bytes, bytes.size())) return false;

    output.colors.resize(output.header.vertex_count);
    std::memcpy(output.colors.data(), bytes.data() + cursor, color_bytes);
    if (!CheckedAdvance(cursor, color_bytes, bytes.size())) return false;

    const uint8_t* weights = bytes.data() + cursor;
    if (!CheckedAdvance(cursor, weight_bytes, bytes.size())) return false;
    const uint8_t* bones = bytes.data() + cursor;
    if (!CheckedAdvance(cursor, bone_index_bytes, bytes.size())) return false;

    output.bone_weights.resize(output.header.vertex_count);
    for (uint32_t i = 0; i < output.header.vertex_count; ++i) {
        BoneWeightPoc item{};
        std::memcpy(
            &item.weight0, weights + static_cast<size_t>(i) * 16, 16);
        const uint8_t* bi = bones + static_cast<size_t>(i) * 4;
        item.bone_index0 = bi[0];
        item.bone_index1 = bi[1];
        item.bone_index2 = bi[2];
        item.bone_index3 = bi[3];
        output.bone_weights[i] = item;
    }

    output.indices.resize(output.header.index_count);
    std::memcpy(output.indices.data(), bytes.data() + cursor, index_bytes);
    if (!CheckedAdvance(cursor, index_bytes, bytes.size()) ||
        cursor != bytes.size()) {
        return false;
    }

    for (uint16_t index : output.indices) {
        if (index >= output.header.vertex_count) {
            Log("BEM PoC index references a vertex outside vertexCount.");
            return false;
        }
    }

    Log("Loaded BEM PoC-2: vertices=" +
        std::to_string(output.header.vertex_count) +
        " indices=" + std::to_string(output.header.index_count) +
        " maxBone=" + std::to_string(output.header.max_bone) +
        " streams=POSITION+UV0+NORMAL+COLOR0+SKIN4");
    return true;
}

void* CreateManagedArray(
    const BE_ResolvedClassV1& element_class, int count) {
    if (!element_class.type_object || count < 0) return nullptr;
    void* parameters[2]{element_class.type_object, &count};
    void* array = Invoke(
        Contract("array.create_instance"), nullptr, parameters);
    if (!array || ArrayLength(array) != count) return nullptr;
    return array;
}

template <typename T>
T* ManagedArrayData(void* array, size_t expected_count) {
    if (!array) return nullptr;
    __try {
        const uintptr_t length = *reinterpret_cast<const uintptr_t*>(
            static_cast<const uint8_t*>(array) + kIl2CppArrayLengthOffset);
        if (length != expected_count) return nullptr;
        return reinterpret_cast<T*>(
            static_cast<uint8_t*>(array) + kIl2CppArrayDataOffset);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

bool ValidateBoneWeightLayout() {
    for (const auto& field : kBoneWeightFields) {
        BE_ResolvedFieldV1 result{};
        const BE_Result status = g_host->resolve_field(
            g_host->context, &field.descriptor, &result);
        if (status != BE_Result_Ok || !result.field_info ||
            result.offset != field.expected_offset) {
            Log(std::string("BoneWeight layout mismatch: ") + field.key +
                " expected=" + std::to_string(field.expected_offset) +
                " actual=" + std::to_string(result.offset));
            return false;
        }
    }
    Log("Validated UnityEngine.BoneWeight 32-byte PoC layout.");
    return true;
}

struct RendererSummary {
    void* renderer = nullptr;
    void* mesh = nullptr;
    std::string renderer_name;
    std::string mesh_name;
    std::string path;
    uint64_t total_indices = 0;
    int bones_count = 0;
    int bindpose_count = 0;
    bool c9_signature = false;
    bool exact_name = false;
    bool live_clone = false;
    bool lod0_path = false;
};

RendererSummary SummarizeRenderer(void* renderer) {
    RendererSummary summary;
    summary.renderer = renderer;
    summary.renderer_name = ObjectName(renderer);
    summary.path = BuildTransformPath(renderer);
    summary.mesh = Invoke(
        Contract("skinned.get_shared_mesh"), renderer, nullptr, false);
    summary.mesh_name = ObjectName(summary.mesh);
    summary.bones_count = ArrayLength(Invoke(
        Contract("skinned.get_bones"), renderer, nullptr, false));

    if (summary.mesh) {
        int sub_mesh_count = 0;
        if (InvokeValue(Contract("mesh.get_sub_mesh_count"),
                summary.mesh, nullptr, sub_mesh_count)) {
            const int count = std::min(
                std::max(sub_mesh_count, 0), kMaxSubMeshes);
            for (int submesh = 0; submesh < count; ++submesh) {
                uint32_t index_count = 0;
                void* parameters[1]{&submesh};
                if (InvokeValue(Contract("mesh.get_index_count"),
                        summary.mesh, parameters, index_count)) {
                    summary.total_indices += index_count;
                }
            }
        }
        summary.bindpose_count = ArrayLength(Invoke(
            Contract("mesh.get_bindposes"), summary.mesh, nullptr, false));
    }

    summary.c9_signature =
        summary.total_indices == kComponent9OriginalIndexCount;
    summary.exact_name =
        summary.renderer_name == kTargetRendererName &&
        summary.mesh_name == kTargetMeshName;
    summary.live_clone = IsLiveClonePath(summary.path);
    summary.lod0_path = IsLod0Path(summary.path);
    return summary;
}

void LogRenderer(const RendererSummary& item, int ordinal) {
    std::string line =
        "  candidate[" + std::to_string(ordinal) + "] renderer=\"" +
        item.renderer_name + "\" mesh=\"" + item.mesh_name +
        "\" totalIndices=" + std::to_string(item.total_indices) +
        " bones=" + std::to_string(item.bones_count) +
        " bindposes=" + std::to_string(item.bindpose_count);
    if (item.c9_signature) line += " C9_SIGNATURE";
    if (item.exact_name) line += " EXACT_NAME";
    if (item.live_clone) line += " LIVE_CLONE";
    if (item.lod0_path) line += " LOD0_PATH";
    Log(line);
    if (!item.path.empty()) Log("    path=\"" + item.path + "\"");
}

std::vector<RendererSummary> EnumerateRenderers() {
    std::vector<RendererSummary> summaries;
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(
        Contract("resources.find_objects_of_type_all"), nullptr, parameters);
    const int raw_count = ArrayLength(objects);
    const int count = std::min(std::max(raw_count, 0), kMaxRenderers);
