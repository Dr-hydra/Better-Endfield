// One immutable target per module session. Restart to switch; active ownership
// and rollback records must never be reinterpreted as another character.
bool g_target_pelica = false;
const char* g_target_postmodel = "chr_0003_endminf_postmodel";
const char* g_target_uimodel = "chr_0003_endminf_uimodel";

struct TargetComponent {
    const char* mesh_name;
    uint32_t indices;
};
constexpr std::array<TargetComponent, 10> kPelicaComponents{{
    {"S_actor_pelica_hair_01_lod0", 45003},
    {"S_actor_pelica_face_01_lod0", 9186},
    {"S_actor_pelica_body_01_lod0", 5922},
    {"S_actor_pelica_hairshadow_01_lod0", 525},
    {"S_actor_pelica_cloth_01_lod0", 78186},
    {"S_actor_pelica_iris_01_lod0", 480},
    {"S_actor_pelica_eyeshadow_01_lod0", 96},
    {"S_actor_pelica_cloth_02_lod0", 70656},
    {"S_actor_pelica_cloth_04_lod0", 8460},
    {"S_actor_pelica_cloth_03_lod0", 2178},
}};

bool SelectCharacterTarget(std::string_view target) {
    if (target != "endminf" && target != "pelica") return false;
    g_target_pelica = target == "pelica";
    g_current_character = g_target_pelica ? 1 : 0;
    kActorMeshPrefix = g_target_pelica ? "S_actor_pelica_" : "S_actor_endminf_";
    kDefaultBemRelativePath = g_target_pelica
        ? "custom-model/pelica-lod0-native-materials.bempoc"
        : "custom-model/endmin-casualwear-c9.bempoc";
    g_target_postmodel = g_target_pelica ? "chr_0004_pelica_postmodel" : "chr_0003_endminf_postmodel";
    g_target_uimodel = g_target_pelica ? "chr_0004_pelica_uimodel" : "chr_0003_endminf_uimodel";
    return true;
}

bool LoadCharacterTarget() {
    std::array<char, 4096> root{};
    if (g_host->copy_catalog_root(g_host->context, root.data(), root.size()) <= 0)
        return false;
    // Resolve UTF-8 catalog roots without relying on the process ANSI codepage.
    const int size = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, root.data(), -1, nullptr, 0);
    if (size <= 0) return false;
    std::wstring wide(static_cast<size_t>(size), L'\0');
    MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, root.data(), -1, wide.data(), size);
    wide.pop_back();
    const auto config = std::filesystem::path(wide) / L"custom-model" / L"runtime.ini";
    std::array<wchar_t, 64> target{};
    GetPrivateProfileStringW(L"CustomModel", L"target", L"endminf", target.data(),
        static_cast<DWORD>(target.size()), config.c_str());
    std::array<wchar_t, 64> path{};
    GetPrivateProfileStringW(L"CustomModel", L"path", L"instance", path.data(),
        static_cast<DWORD>(path.size()), config.c_str());
    const std::wstring_view mode(path.data());
    if (mode != L"instance" && mode != L"resource") { Log("Unknown replacement path; refused."); return false; }
    g_resource_path = mode == L"resource";
    const std::wstring_view value(target.data());
    g_multi_character = value == L"both";
    if (g_resource_path && !g_multi_character) { Log("Resource validation requires target=both."); return false; }
    if (g_multi_character) return SelectCharacterTarget("endminf");
    if (value == L"pelica") return SelectCharacterTarget("pelica");
    if (value == L"endminf") return SelectCharacterTarget("endminf");
    Log("CustomModel runtime.ini target must be endminf, pelica or both; initialization refused.");
    return false;
}

bool TargetComponentName(uint32_t id, std::string_view renderer_name) {
    if (!g_target_pelica) return renderer_name.find(kActorMeshPrefix) == 0;
    return id < kPelicaComponents.size() && renderer_name == kPelicaComponents[id].mesh_name;
}


bool ValidateTargetPayload(const BemPocData& bem) {
    if (!g_target_pelica) return true;
    if (bem.components.size() != kPelicaComponents.size()) {
        Log("Pelica target requires the complete 10-component payload.");
        return false;
    }
    for (const auto& texture : bem.textures) {
        if (texture.info.explicit_slot == 0 || texture.info.reserved != 2 ||
            (texture.info.component_mask & ~0x3ffu) != 0) {
            Log("Pelica textures require explicit original-name pins and valid component masks.");
            return false;
        }
    }
    std::array<bool, kPelicaComponents.size()> seen{};
    for (const auto& component : bem.components) {
        const auto& info = component.info;
        if (info.component_id >= seen.size() || seen[info.component_id] ||
            info.original_index_count != kPelicaComponents[info.component_id].indices) {
            Log("Pelica payload component identity mismatch; refused.");
            return false;
        }
        seen[info.component_id] = true;
    }
    return true;
}

struct CharacterScope {
    size_t previous = g_current_character;
    explicit CharacterScope(size_t index) { SelectCharacterTarget(index == 1 ? "pelica" : "endminf"); }
    ~CharacterScope() { SelectCharacterTarget(previous == 1 ? "pelica" : "endminf"); }
};
