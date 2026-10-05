// BEM 1.4 resource contracts. Included after the container's checked JSON helpers.
// These identity boundaries are mandatory even when geometry validation is disabled.
std::string CheckedRelativeReceiver(const J& value) {
    auto path=value.get<std::string>();
    Check(path.size()<=1024 && path.find('\0')==path.npos && path.find('\\')==path.npos &&
        path.find(':')==path.npos,"Invalid receiver path");
    if(path.empty()) return path; // Renderer on the resource root itself.
    Check(path.front()!='/' && path.back()!='/',"Invalid receiver path");
    std::string_view rest=path;
    while(!rest.empty()) {
        const auto slash=rest.find('/'); const auto part=rest.substr(0,slash);
        Check(!part.empty() && part!="." && part!="..","Invalid receiver path segment");
        rest=slash==rest.npos?std::string_view{}:rest.substr(slash+1);
    }
    return path;
}
bool RendererStatic(const J& node) {
    const auto kind=S(node.at("renderer_kind"));
    Check(kind=="skinned" || kind=="static","Invalid renderer kind");
    return kind=="static";
}
void ReadResourceTargets(const J& target,BemPackageInfo& info) {
    info.target_kind=S(target.at("kind"));
    Check(info.target_kind=="character" || info.target_kind=="weapon","Invalid target kind");
    info.target_id=Id(target.at("id")); info.character_id=info.target_id;
    S(target.at("snapshot"));
    Check(!target.contains("character_id") && !target.contains("world_resource") && !target.contains("ui_resource"),
        "BEM 1.4 must use explicit resource targets");
    const auto& resources=target.at("resources");
    Check(resources.is_array() && !resources.empty() && resources.size()<=32,"Invalid resource targets");
    std::set<std::string> ids,keys;
    for(const auto& row:resources) {
        Check(row.is_object() && row.size()==5,"Invalid resource fields");
        BemResourceInfo resource; resource.id=Id(row.at("id")); resource.name=Id(row.at("name"));
        Check(ids.insert(resource.id).second,"Duplicate resource ID");
        resource.asset_path=CheckedRelativeReceiver(row.at("asset_path"));
        Check(resource.asset_path.starts_with("assets/") && resource.asset_path.ends_with(".prefab") &&
            std::all_of(resource.asset_path.begin(),resource.asset_path.end(),[](unsigned char c) {return !(c>='A'&&c<='Z');}),
            "Invalid resource asset path");
        const auto leaf=resource.asset_path.substr(resource.asset_path.rfind('/')+1);
        Check(leaf==resource.name+".prefab","Resource path/name differs");
        resource.lod=U(row.at("lod")); Check(resource.lod<=3,"Invalid resource LOD");
        const auto& platforms=row.at("platforms");
        Check(platforms.is_array() && !platforms.empty() && platforms.size()<=2,"Invalid resource platforms");
        std::set<std::string> distinct;
        for(const auto& item:platforms) {
            const auto platform=S(item);
            Check((platform=="windows-x64" || platform=="android-arm64") && distinct.insert(platform).second,
                "Invalid/duplicate resource platform");
            const auto key=platform+":"+resource.name;
            Check(keys.insert(key).second,"Duplicate resource root for platform");
            resource.platforms.push_back(platform); info.resource_keys.push_back(key);
        }
        info.resources.push_back(std::move(resource));
    }
    const auto& components=target.at("components");
    Check(components.is_array() && !components.empty() && components.size()<=64,"Invalid target components");
    std::set<std::pair<std::string,std::string>> paths,names;
    for(size_t index=0;index<components.size();++index) {
        const auto& component=components[index];
        Check(U(component.at("id"))==index,"Invalid target component ID");
        const auto id=Id(component.at("resource"));
        auto found=std::find_if(info.resources.begin(),info.resources.end(),[&](const auto& r){return r.id==id;});
        Check(found!=info.resources.end(),"Component resource is missing");
        const auto path=CheckedRelativeReceiver(component.at("renderer_path"));
        constexpr std::string_view lodPrefix="Mesh_all/lod";
        if(path.starts_with(lodPrefix) && path.size()>lodPrefix.size()) {
            const auto level=path[lodPrefix.size()];
            if(level>='0' && level<='3' &&
                (path.size()==lodPrefix.size()+1 || path[lodPrefix.size()+1]=='/'))
                Check(static_cast<uint32_t>(level-'0')==found->lod,"Receiver path/resource LOD differs");
        }
        Check(paths.emplace(id,path).second && names.emplace(id,S(component.at("mesh_name"))).second,
            "Duplicate resource receiver/mesh identity");
        const bool isStatic=RendererStatic(component);
        Check(component.at("bone_names").is_array() && component.at("materials").is_array(),"Invalid donor tables");
        Check(component.at("bone_names").size()<=65536 && component.at("materials").size()<=256,"Invalid donor table size");
        for(const auto& bone:component.at("bone_names")) S(bone);
        for(const auto& material:component.at("materials")) S(material);
        Check(!isStatic || component.at("bone_names").empty(),"Static component must not declare bones");
        Check(!component.contains("bone_name_aliases"),"BEM 1.4 uses resource-local bone identities");
        found->component_ids.push_back(static_cast<uint32_t>(index));
        info.component_resources.push_back(id); info.component_paths.push_back(path); info.component_static.push_back(isStatic);
    }
    for(const auto& resource:info.resources) Check(!resource.component_ids.empty(),"Resource has no components");
}
// Validate all mesh descriptors, including unselected options. A resource may
// share immutable payload bytes, never borrow another resource's live donors.
void ValidateResourceMeshes(const J& manifest,const BemPackageInfo& info) {
    const auto& meshes=manifest.at("meshes");
    const auto& components=manifest.at("target").at("components");
    std::vector<std::string> owners;
    std::vector<bool> staticMeshes;
    Check(meshes.is_array() && meshes.size()<=4096,"Invalid mesh table");
    for(const auto& mesh:meshes) {
        const bool isStatic=RendererStatic(mesh); staticMeshes.push_back(isStatic);
        const auto& streams=mesh.at("streams");
        Check(streams.is_array() && streams.size()>=1 && streams.size()<=3 &&
              (isStatic || streams.size()==3),"Invalid stream count for renderer kind");
        const auto& bones=mesh.at("bones");
        Check(bones.is_array() && (isStatic?bones.empty():!bones.empty()) && bones.size()<=256,"Invalid palette for renderer kind");
        std::string owner;
        auto donor=[&](const J& value) {
            const auto id=U(value); Check(id<components.size(),"Donor component outside target table");
            const auto& resource=info.component_resources.at(id);
            if(owner.empty()) owner=resource;
            Check(owner==resource,"Cross-resource donor reference");
            return id;
        };
        for(const auto& bone:bones) {
            const auto id=donor(bone.at("component")),index=U(bone.at("index"));
            const auto& names=components[id].at("bone_names");
            Check(!info.component_static[id] && index<names.size() && names[index]==bone.at("name"),"Invalid resource bone donor");
        }
        const auto& draws=mesh.at("draws");
        Check(draws.is_array() && !draws.empty(),"Mesh has no draw candidates");
        for(const auto& draw:draws) {
            const auto id=donor(draw.at("material_component")),slot=U(draw.at("material_slot"));
            const auto& materials=components[id].at("materials");
            Check(slot<materials.size() && materials[slot]==draw.at("material_name"),"Invalid resource material donor");
        }
        const auto& attributes=mesh.at("attributes");
        Check(attributes.is_array() && !attributes.empty() && attributes.size()<=16,"Invalid attributes");
        for(const auto& attribute:attributes) {
            Check(attribute.is_array() && attribute.size()==5,"Invalid attribute declaration");
            if(isStatic) Check(U(attribute[0])!=12 && U(attribute[0])!=13,"Static mesh has skin attributes");
        }
        owners.push_back(std::move(owner));
    }
    const auto& rules=manifest.at("component_rules");
    Check(rules.is_array() && rules.size()==components.size(),"Incomplete component rules");
    for(size_t i=0;i<rules.size();++i) for(const auto& candidate:rules[i].at("candidates")) {
        if(candidate.at("operation")!="replace") continue;
        const auto mesh=U(candidate.at("mesh")); Check(mesh<meshes.size(),"Missing mesh reference");
        Check(owners[mesh]==info.component_resources[i],"Replacement uses cross-resource donors");
        Check(staticMeshes[mesh]==info.component_static[i],"Replacement renderer kind differs");
    }
}
