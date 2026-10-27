// Local static head data for manager previews only. Never participates in installation.
#pragma once
struct NativePreviewHead {
    J pack;
    std::filesystem::path root;
    std::map<uint32_t,J> renderers;
    std::map<std::string,size_t> textures;
    std::string diagnostic;
    uint64_t loaded=0;
    std::vector<uint8_t> Read(const std::string& name,size_t expected=0) {
        auto relative=std::filesystem::u8path(name);
        Require(!relative.empty() && !relative.is_absolute(),"Invalid native preview path");
        for(const auto& part:relative)Require(part!=".." && part!="." && part.native().find(':')==part.native().npos,"Invalid native preview path");
        const auto base=std::filesystem::canonical(root),path=std::filesystem::canonical(root/relative);
        auto [a,b]=std::mismatch(base.begin(),base.end(),path.begin(),path.end());
        Require(a==base.end() && b!=path.end(),"Native preview path escapes resource directory");
        auto size=std::filesystem::file_size(path);
        Require(size<=32u*1024*1024 && loaded+size<=64u*1024*1024 && (!expected || size==expected),"Invalid native preview payload size");
        loaded+=size;std::vector<uint8_t> bytes(static_cast<size_t>(size));
        std::ifstream input(path,std::ios::binary);input.read(reinterpret_cast<char*>(bytes.data()),std::streamsize(size));
        Require(bool(input),"Truncated native preview payload");return bytes;
    }
    void Load(const std::filesystem::path& directory,const BemPackageInfo& info,const BemResourceInfo* active,
              const std::string& resource,BemPocData& data) {
        if(directory.empty())return;
        root=directory;const auto original=data.textures.size();
        try {
            auto bytes=Read("pack.json");pack=J::parse(bytes);
            Require(pack.at("schema")==1 && pack.at("kind")=="native-head-preview-prototype","Unsupported native preview resources");
            if(pack.at("character")!=info.character_id){diagnostic="unavailable";return;}
            const auto manifest=J::parse(info.manifest_json);
            // An explicit resource must match its full asset identity. Legacy world/UI
            // targets carry their exact prefab root in world_resource/ui_resource.
            std::string legacyRoot=resource.empty()?info.world_resource:resource;
            if(!active && legacyRoot.empty())legacyRoot=info.character_id+"_postmodel";
            const auto& components=manifest.at("target").at("components");
            for(size_t global=0;global<info.component_names.size();++global) {
                if(active && std::find(active->component_ids.begin(),active->component_ids.end(),global)==active->component_ids.end())continue;
                const J* match=nullptr;
                for(const auto& r:pack.at("renderers")) {
                    if(r.at("name")!=info.component_names[global])continue;
                    bool identity=false;
                    if(!info.component_paths.empty()) {
                        const auto path=r.at("path").get<std::string>(),rootName=r.at("resource_root").get<std::string>();
                        const auto relative=path.starts_with(rootName+"/")?path.substr(rootName.size()+1):path;
                        identity=relative==info.component_paths[global];
                        bool asset=false;for(const auto& p:r.at("resource_asset_paths"))if(active && p==active->asset_path)asset=true;
                        identity=identity && asset;
                    } else identity=r.at("resource_root")==legacyRoot;
                    if(!identity)continue;
                    const auto& expected=components.at(global).at("materials");
                    bool materials=expected.size()==r.at("materials").size();
                    for(size_t slot=0;materials && slot<expected.size();++slot)
                        materials=r.at("materials").at(slot).at("slot")==slot && r.at("materials").at(slot).at("name")==expected.at(slot);
                    if(!materials)continue;
                    Require(!match,"Ambiguous native preview renderer");match=&r;
                }
                if(match)renderers.emplace(uint32_t(global),*match);
            }
            if(renderers.empty()){diagnostic="unmatched";return;}
            // Only known formats, exact authored mip extents and local bounded payloads.
            for(const auto& t:pack.at("textures")) {
                BemTexture texture;texture.name=t.at("name");texture.original_name=texture.name;
                auto& h=texture.info;h.width=t.at("width");h.height=t.at("height");h.mip_count=t.at("mips");
                Require(h.width && h.height && h.width<=4096 && h.height<=4096 && h.mip_count && h.mip_count<=13,"Invalid native preview texture extent");
                const std::string format=t.at("format");h.create_format=format=="BC7"?25:format=="RGBA32"?4:format=="ASTC_6x6"?50:0;
                Require(h.create_format!=0,"Unsupported native preview texture");h.create_srgb=t.value("srgb",true)?1:0;
                size_t total=0;uint32_t w=h.width,height=h.height,block=h.create_format==50?6:4;
                for(uint32_t level=0;level<h.mip_count;++level){total+=h.create_format==4?size_t(w)*height*4:size_t((w+block-1)/block)*((height+block-1)/block)*16;w=std::max(1u,w/2);height=std::max(1u,height/2);}
                texture.data=Read(t.at("payload"),total);h.data_size=uint32_t(total);
                const auto id=t.at("id").get<std::string>();Require(!textures.contains(id),"Duplicate native preview texture");
                textures.emplace(id,data.textures.size());data.textures.push_back(std::move(texture));
            }
            diagnostic="ready";
        } catch(const std::exception&) {
            data.textures.resize(original);textures.clear();renderers.clear();diagnostic="invalid";
        }
    }
    const J* Renderer(uint32_t global) const {auto it=renderers.find(global);return it==renderers.end()?nullptr:&it->second;}
    const J* Base(const J& renderer,uint32_t slot) const {
        for(const auto& material:renderer.at("materials"))if(material.at("slot")==slot)
            for(const auto& texture:material.at("textures"))if(texture.at("property")=="_BaseMap" && !texture.at("id").is_null())return &texture;
        return nullptr;
    }
    int Texture(const J* renderer,uint32_t slot,uint64_t mask,const BemPocData& data) const {
        if(!renderer)return -1;
        auto base=Base(*renderer,slot);if(!base)return -1;
        // Exact original texture identity wins over filename scoring and over native fallback.
        for(size_t t=0;t<data.textures.size() && t<64;++t)if((mask&(uint64_t(1)<<t)) && data.textures[t].original_name==base->at("name").get<std::string>())return int(t);
        auto it=textures.find(base->at("id").get<std::string>());return it==textures.end()?-1:int(it->second);
    }
    J Uv(const J* renderer,uint32_t slot) const {
        J result=J::array({1.f,1.f,0.f,0.f});if(!renderer)return result;
        if(const auto* base=Base(*renderer,slot)) {
            const auto scale=base->value("scale",J::object()),offset=base->value("offset",J::object());
            result=J::array({scale.value("X",1.f),scale.value("Y",1.f),offset.value("X",0.f),offset.value("Y",0.f)});
            for(const auto& value:result)Require(std::isfinite(value.get<float>()),"Nonfinite native preview UV transform");
        }
        return result;
    }
    bool Projected(const J& renderer) const {
        for(const auto& material:renderer.at("materials"))for(const auto& path:material.at("shader").value("asset_paths",J::array()))
            if(path.get<std::string>().find("overlayshadow.shader")!=std::string::npos)return true;
        return false;
    }
};
