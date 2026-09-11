
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

// The container name identifies the character, not the instance. Requiring
// "(Clone)" excluded the character showcase, whose model is parented under a
// prefab proxy manager, and the prefab templates, which are the same model.
// What actually gets replaced is still decided by the index-count join and the
// declaration check, not by this name.
bool IsGameplayPostModelPath(std::string_view path) {
    return path.find("chr_0003_endminf_postmodel") != std::string_view::npos;
}

bool IsUiModelPath(std::string_view path) {
    return path.find("chr_0003_endminf_uimodel") != std::string_view::npos;
}

bool IsSupportedLiveTargetPath(std::string_view path) {
    return IsGameplayPostModelPath(path) || IsUiModelPath(path);
}

bool IsLod0Path(std::string_view path) {
    return path.find("/Mesh_all/lod0/") != std::string_view::npos;
}

// Whether a live SkinnedMeshRenderer is one of the Endministrator's lod0 draws.
//
// The container name is deliberately not consulted. The world model sits under
// a scene root, the character showcase under a prefab proxy manager, and the
// prefab template under the asset itself, and a whitelist of those names
// silently skipped whatever container the next screen used - which is why the
// showcase kept its original geometry while its materials, shared assets, were
// still retextured. What identifies the draw is the renderer's own object name.
// The shadow proxies carry the same character prefix, so they are excluded by
// name, and lod1+ draws are excluded so the lod0 payload is never dropped onto
// cheaper geometry.
bool IsEndminfLod0Draw(
    std::string_view renderer_name, std::string_view path) {
    if (renderer_name.find(kActorMeshPrefix) != 0) return false;
    if (renderer_name.find("shadowProxy") != std::string_view::npos) {
        return false;
    }
    if (path.find("/lod1/") != std::string_view::npos ||
        path.find("/lod2/") != std::string_view::npos ||
        path.find("/lod3/") != std::string_view::npos) {
        return false;
    }
    return renderer_name.find("_lod0") != std::string_view::npos ||
        IsLod0Path(path);
}

// Everything above "/Mesh_all/": two renderers with the same root are two
// components of one model instance.
std::string PathRoot(std::string_view path) {
    const size_t split = path.find("/Mesh_all/");
    return std::string(
        split == std::string_view::npos ? path : path.substr(0, split));
}

std::string JoinRoots(std::vector<std::string> roots) {
    std::sort(roots.begin(), roots.end());
    roots.erase(std::unique(roots.begin(), roots.end()), roots.end());
    std::string text;
    for (const std::string& root : roots) {
        if (!text.empty()) text += " | ";
        text += root;
    }
    return text;
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

// Block-compressed mip chain size, used to prove the payload's declared
// dimensions agree with the number of bytes actually carried. Nothing here
// decodes or reinterprets the surface; it is uploaded exactly as stored.
size_t BlockCompressedChainSize(
    uint32_t width, uint32_t height, uint32_t mip_count, uint32_t block_bytes) {
    size_t total = 0;
    for (uint32_t level = 0; level < mip_count; ++level) {
        const uint32_t level_width = std::max(width >> level, 1u);
        const uint32_t level_height = std::max(height >> level, 1u);
        total += static_cast<size_t>((level_width + 3) / 4) *
            static_cast<size_t>((level_height + 3) / 4) * block_bytes;
    }
    return total;
}

// Bytes per 4x4 block for the UnityEngine.TextureFormat values the converter is
// allowed to emit. An unlisted format is refused rather than guessed at.
bool BlockBytesForTextureFormat(int32_t format, uint32_t& block_bytes) {
    switch (format) {
    case 10:  // DXT1
    case 26:  // BC4
        block_bytes = 8;
        return true;
    case 12:  // DXT5
    case 25:  // BC7
    case 27:  // BC5
        block_bytes = 16;
        return true;
    default:
        return false;
    }
}

bool LoadBemTextures(
    const std::vector<uint8_t>& bytes, size_t& cursor, BemPocData& output) {
    const uint32_t texture_count = output.header.texture_count;
    if (texture_count == 0) {
        Log("BEM PoC-2.4 carries no textures; geometry only.");
        return true;
    }
    if (texture_count > kMaxBemTextures) {
        Log("BEM PoC-2.4 declares an implausible texture count: " +
            std::to_string(texture_count));
        return false;
    }

    output.textures.reserve(texture_count);
    for (uint32_t index = 0; index < texture_count; ++index) {
        const std::string label =
            "BEM PoC-2.4 texture[" + std::to_string(index) + "]";
        BemTexture texture;
        if (bytes.size() - cursor < sizeof(BemTextureEntryRaw)) {
            Log(label + " header is truncated.");
            return false;
        }
        std::memcpy(
            &texture.info, bytes.data() + cursor, sizeof(BemTextureEntryRaw));
        if (!CheckedAdvance(cursor, sizeof(BemTextureEntryRaw), bytes.size())) {
            return false;
        }

        const BemTextureEntryRaw& info = texture.info;
        uint32_t block_bytes = 0;
        if (!BlockBytesForTextureFormat(info.create_format, block_bytes)) {
            Log(label + " uses unsupported TextureFormat " +
                std::to_string(info.create_format));
            return false;
        }
        if (info.width == 0 || info.height == 0 || info.width % 4 != 0 ||
            info.height % 4 != 0 || info.mip_count == 0 ||
            info.mip_count > 16 || info.name_length > 256 ||
            info.component_mask == 0 || info.data_size == 0 ||
            info.data_size > kMaxBemTextureBytes) {
            Log(label + " has implausible dimensions, sizes or component set.");
            return false;
        }
        const size_t expected = BlockCompressedChainSize(
            info.width, info.height, info.mip_count, block_bytes);
        if (expected != info.data_size) {
            Log(label + " declares " + std::to_string(info.data_size) +
                " bytes but " + std::to_string(info.width) + "x" +
                std::to_string(info.height) + " with " +
                std::to_string(info.mip_count) + " mip level(s) needs " +
                std::to_string(expected));
            return false;
        }
        if (bytes.size() - cursor < info.name_length ||
            bytes.size() - cursor - info.name_length < info.data_size) {
            Log(label + " payload is truncated.");
            return false;
        }

        texture.name.assign(
            reinterpret_cast<const char*>(bytes.data() + cursor),
            info.name_length);
        if (!CheckedAdvance(cursor, info.name_length, bytes.size())) {
            return false;
        }
        texture.data.resize(info.data_size);
        std::memcpy(
            texture.data.data(), bytes.data() + cursor, info.data_size);
        if (!CheckedAdvance(cursor, info.data_size, bytes.size())) {
            return false;
        }
        output.textures.push_back(std::move(texture));
    }
    return true;
}

std::string ComponentMaskText(uint32_t mask) {
    std::string text;
    for (uint32_t i = 0; i < 32; ++i) {
        if ((mask >> i & 1u) == 0) continue;
        if (!text.empty()) text += ",";
        text += std::to_string(i);
    }
    return "{" + text + "}";
}

bool LoadBemComponent(
    const std::vector<uint8_t>& bytes, size_t& cursor, BemComponent& output) {
    if (bytes.size() - cursor < sizeof(BemComponentHeaderRaw)) {
        Log("BEM PoC-2.4 component header is truncated.");
        return false;
    }
    std::memcpy(
        &output.info, bytes.data() + cursor, sizeof(BemComponentHeaderRaw));
    if (!CheckedAdvance(
            cursor, sizeof(BemComponentHeaderRaw), bytes.size())) {
        return false;
    }

    const BemComponentHeaderRaw& info = output.info;
    const std::string label =
        "BEM PoC-2.4 C" + std::to_string(info.component_id);
    if (info.original_index_count == 0 ||
        info.original_index_count % 3 != 0) {
        Log(label + " declares an implausible original index count.");
        return false;
    }
    if ((info.flags & kComponentFlagNoGeometry) != 0) {
        // Carried only so its renderer can be identified while the texture
        // component sets are computed.
        return true;
    }

    if (info.stream_count != kBemStreamCount ||
        info.index_element_size != kIndexElementSize ||
        info.vertex_count == 0 || info.vertex_count > (1u << 20) ||
        info.index_count == 0 || info.index_count % 3 != 0 ||
        info.stride0 == 0 || info.stride1 == 0 || info.stride2 == 0 ||
        info.stride0 > 64 || info.stride1 > 64 || info.stride2 > 64) {
        Log(label + " has an implausible geometry description.");
        return false;
    }

    const std::array<uint32_t, 3> strides{
        info.stride0, info.stride1, info.stride2};
    for (uint32_t stream = 0; stream < kBemStreamCount; ++stream) {
        const size_t size =
            static_cast<size_t>(info.vertex_count) * strides[stream];
        if (bytes.size() - cursor < size) {
            Log(label + " stream " + std::to_string(stream) +
                " is truncated.");
            return false;
        }
        output.streams[stream].resize(size);
        std::memcpy(
            output.streams[stream].data(), bytes.data() + cursor, size);
        if (!CheckedAdvance(cursor, size, bytes.size())) return false;
    }

    const size_t index_bytes =
        static_cast<size_t>(info.index_count) * kIndexElementSize;
    if (bytes.size() - cursor < index_bytes) {
        Log(label + " index buffer is truncated.");
        return false;
    }
    output.indices.resize(index_bytes);
    std::memcpy(output.indices.data(), bytes.data() + cursor, index_bytes);
    if (!CheckedAdvance(cursor, index_bytes, bytes.size())) return false;

    const uint16_t* indices =
        reinterpret_cast<const uint16_t*>(output.indices.data());
    for (uint32_t i = 0; i < info.index_count; ++i) {
        if (indices[i] >= info.vertex_count) {
            Log(label + " index " + std::to_string(indices[i]) +
                " is outside vertexCount " +
                std::to_string(info.vertex_count));
            return false;
        }
    }

    // Bone indices are the last four bytes of every stream 2 element in every
    // declaration this format allows, with or without preceding weights.
    uint32_t max_bone = 0;
    for (uint32_t i = 0; i < info.vertex_count; ++i) {
        const uint8_t* bones = output.streams[2].data() +
            static_cast<size_t>(i) * info.stride2 + (info.stride2 - 4);
        for (int k = 0; k < 4; ++k) {
            max_bone = std::max(max_bone, static_cast<uint32_t>(bones[k]));
        }
    }
    if (max_bone != info.max_bone) {
        Log(label + " stream 2 max bone " + std::to_string(max_bone) +
            " disagrees with the header's " + std::to_string(info.max_bone));
        return false;
    }
    return true;
}

bool LoadBemPoc(const std::filesystem::path& path, BemPocData& output) {
    output = {};
    std::ifstream input(path, std::ios::binary | std::ios::ate);
    if (!input) {
        Log("BEM payload not found: " + path.string());
        return false;
    }

    const std::streamoff end = input.tellg();
    if (end < static_cast<std::streamoff>(sizeof(BemFileHeader)) ||
        end > static_cast<std::streamoff>(512 * 1024 * 1024)) {
        Log("BEM payload size is invalid.");
        return false;
    }

    std::vector<uint8_t> bytes(static_cast<size_t>(end));
    input.seekg(0, std::ios::beg);
    if (!input.read(reinterpret_cast<char*>(bytes.data()),
            static_cast<std::streamsize>(bytes.size()))) {
        Log("Failed to read the BEM payload.");
        return false;
    }

    std::memcpy(&output.header, bytes.data(), sizeof(output.header));
    const std::array<char, 8> expected_magic{
        'B','E','M','P','C','2','4','\0'};
    if (std::memcmp(output.header.magic, expected_magic.data(),
            expected_magic.size()) != 0 ||
        output.header.version != 24) {
        Log("BEM payload is not a PoC-2.4 file; regenerate it with "
            "tools/CustomModel/convert_efmi_poc.py.");
        return false;
    }
    if (output.header.component_count == 0 ||
        output.header.component_count > kMaxBemComponents) {
        Log("BEM payload declares an implausible component count: " +
            std::to_string(output.header.component_count));
        return false;
    }

    size_t cursor = sizeof(BemFileHeader);
    output.components.resize(output.header.component_count);
    for (BemComponent& component : output.components) {
        if (!LoadBemComponent(bytes, cursor, component)) return false;
    }
    if (!LoadBemTextures(bytes, cursor, output)) return false;
    if (cursor != bytes.size()) {
        Log("BEM payload has " + std::to_string(bytes.size() - cursor) +
            " trailing bytes.");
        return false;
    }

    // Two components cannot share an original index count: the runtime would
    // have no way to tell their renderers apart.
    for (size_t i = 0; i < output.components.size(); ++i) {
        for (size_t k = i + 1; k < output.components.size(); ++k) {
            if (output.components[i].info.original_index_count ==
                output.components[k].info.original_index_count) {
                Log("BEM payload components C" +
                    std::to_string(output.components[i].info.component_id) +
                    " and C" +
                    std::to_string(output.components[k].info.component_id) +
                    " share original index count " +
                    std::to_string(
                        output.components[i].info.original_index_count) +
                    "; their renderers cannot be told apart.");
                return false;
            }
        }
    }

    Log("Loaded BEM PoC-2.4: components=" +
        std::to_string(output.components.size()) + " textures=" +
        std::to_string(output.textures.size()));
    for (const BemComponent& component : output.components) {
        const BemComponentHeaderRaw& info = component.info;
        if ((info.flags & kComponentFlagNoGeometry) != 0) {
            Log("  C" + std::to_string(info.component_id) + " origIdx=" +
                std::to_string(info.original_index_count) +
                " not replaced (carried for texture identification)");
            continue;
        }
        Log("  C" + std::to_string(info.component_id) + " origIdx=" +
            std::to_string(info.original_index_count) + " vtx=" +
            std::to_string(info.vertex_count) + " idx=" +
            std::to_string(info.index_count) + " maxBone=" +
            std::to_string(info.max_bone) + " strides=" +
            std::to_string(info.stride0) + "/" +
            std::to_string(info.stride1) + "/" +
            std::to_string(info.stride2) +
            ((info.flags & kComponentFlagHidden) != 0
                ? " HIDDEN_BY_DEFAULT" : ""));
    }
    for (const BemTexture& texture : output.textures) {
        Log("  t=" + texture.name + " " +
            std::to_string(texture.info.width) + "x" +
            std::to_string(texture.info.height) + " mips=" +
            std::to_string(texture.info.mip_count) + " slot=fmt" +
            std::to_string(texture.info.slot_format) + "/srgb" +
            std::to_string(texture.info.slot_srgb) + " components=" +
            ComponentMaskText(texture.info.component_mask) +
            (texture.info.explicit_slot != 0 ? " PINNED" : ""));
    }
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

// `known_path` lets a caller that already walked the transform chain hand the
// result over instead of paying for a second walk.
RendererSummary SummarizeRenderer(
    void* renderer, std::string_view known_path = {}) {
    RendererSummary summary;
    summary.renderer = renderer;
    summary.renderer_name = ObjectName(renderer);
    summary.path = known_path.empty() ? BuildTransformPath(renderer)
                                      : std::string(known_path);
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
    const int count = std::max(raw_count, 0);
