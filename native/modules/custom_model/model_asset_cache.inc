// Included after StrongReference/WeakObject. Only the confirmed pump may call
// this pool. Keys are exact descriptions; weak donors reject address reuse.
// 2026-10-03: no idle retention. An entry is a WEAK observation of an asset a
// published receiver already uses; it never roots, extends or destroys it, so
// an unreferenced generated asset is released by the game as usual. Textures
// are matched through live published materials instead (see
// IndexLivePublishedTextures); this table serves generated Meshes.
struct ModelAssetEntry {
    std::string key;
    WeakObject object;
    std::vector<WeakObject> donors;
    uint64_t bytes=0,used=0;
};
constexpr size_t kModelAssetEntryLimit=512;
std::vector<ModelAssetEntry> g_model_assets;
uint64_t g_model_asset_serial=0;
template<class T> void AssetKeyValue(std::string& key,const T& value) {
    static_assert(std::is_trivially_copyable_v<T>);
    key.append(reinterpret_cast<const char*>(&value),sizeof(value));
}
void AssetKeyText(std::string& key,std::string_view value) {
    AssetKeyValue(key,uint64_t(value.size())); key.append(value);
}
void PruneModelAssets(uint64_t) {
    std::erase_if(g_model_assets,[](const auto& entry) {
        if (!entry.object.Get()) return true;
        for (const auto& donor:entry.donors) if (!donor.Get()) return true;
        return false;
    });
    while (g_model_assets.size()>kModelAssetEntryLimit) {
        auto oldest=std::min_element(g_model_assets.begin(),g_model_assets.end(),[](const auto& a,const auto& b){return a.used<b.used;});
        g_model_assets.erase(oldest);
    }
}
void* BorrowModelAsset(const std::string& key,const std::vector<void*>& donors) {
    for (auto& entry:g_model_assets) {
        if (entry.key!=key || entry.donors.size()!=donors.size()) continue;
        bool compatible=true;
        for (size_t i=0;i<donors.size();++i) if (entry.donors[i].Get()!=donors[i]) { compatible=false; break; }
        if (!compatible) continue;
        void* object=entry.object.Get();
        if (!object || !RootTemporary(object)) continue;
        entry.used=++g_model_asset_serial;
        return object; // Scope roots the borrow; it is absent from assets[].
    }
    return nullptr;
}
void PublishModelAsset(std::string key,void* object,const std::vector<void*>& donors,uint64_t bytes) {
    if (!object || key.empty()) return;
    ModelAssetEntry entry;entry.key=std::move(key);entry.bytes=bytes;
    if (!entry.object.Set(object)) return;
    for (void* donor:donors) {
        WeakObject weak;if (!weak.Set(donor)) return;entry.donors.push_back(std::move(weak));
    }
    entry.used=++g_model_asset_serial;
    std::erase_if(g_model_assets,[&](const auto& old){return old.key==entry.key;});
    g_model_assets.push_back(std::move(entry));PruneModelAssets(GetTickCount64());
}
