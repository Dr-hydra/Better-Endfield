// Private per-WebView disk cache. Identity is source generation and texture
// description, not a file digest; no cache paths come from package metadata.
struct PreviewTextureCache {
    struct Entry { J key, item; std::filesystem::path data; uint64_t size; };
    std::filesystem::path root;
    std::vector<Entry> entries;
    uint64_t bytes=0;
    static constexpr uint64_t Limit=512ull*1024*1024;
    explicit PreviewTextureCache(const std::filesystem::path& directory):root(directory) {
        if(root.empty())return;
        try {
            std::filesystem::create_directories(root);
            // Fixed names keep even a damaged cache index inside its owned directory.
            for(unsigned i=0;i<64;++i) {
                auto stem=root/('t'+std::to_string(i));
                auto meta=stem;meta+=".json";auto data=stem;data+=".bin";
                if(!std::filesystem::exists(meta))break;
                if(std::filesystem::file_size(meta)>65536 || !std::filesystem::exists(data))break;
                std::ifstream input(meta,std::ios::binary);auto record=J::parse(input);
                const auto size=std::filesystem::file_size(data);
                if(size>kMaxBemTextureBytes || size>Limit-bytes || record.at("size")!=size)break;
                entries.push_back({record.at("key"),record.at("item"),data,size});bytes+=size;
            }
        } catch(const std::exception&) { root.clear(); entries.clear(); }
    }
    const Entry* Find(const J& key) const {
        if(key.is_null())return nullptr;
        for(const auto& entry:entries)if(entry.key==key)return &entry;
        return nullptr;
    }
    void Store(const J& key,const J& item,const std::filesystem::path& file) {
        if(root.empty() || key.is_null() || item.at("blob").is_null() || entries.size()>=64 || Find(key))return;
        try {
            const auto size=std::filesystem::file_size(file);
            if(size>Limit-bytes)return;
            auto stem=root/('t'+std::to_string(entries.size()));auto data=stem;data+=".bin";auto meta=stem;meta+=".json";
            std::filesystem::copy_file(file,data,std::filesystem::copy_options::overwrite_existing);
            std::ofstream output(meta,std::ios::binary);output<<J{{"key",key},{"item",item},{"size",size}}.dump();output.close();
            if(output) {entries.push_back({key,item,data,size});bytes+=size;}
        } catch(const std::exception&) { /* A cache failure must not prevent preview. */ }
    }
};
