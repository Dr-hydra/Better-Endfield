#pragma once
// Included inside the composer's private namespace; no runtime format changes.
J MergeCompositionSegments(Context& c,const Component& receiver,const J& group,const J& defaults,
    const std::vector<std::map<std::string,std::string>>& optionIds,
    const std::function<void(size_t,J&)>& remapComponent,const std::function<void(size_t,J&)>& remapTextures,
    const std::function<uint32_t(const std::vector<uint8_t>&)>& store,J& active,
    const std::function<void()>& checkpoint) {
    J merged,draws=J::array(),conditions=J::array(),palette=J::array();
    std::map<std::string,uint32_t> bones;
    std::map<std::pair<size_t,uint32_t>,uint32_t> bases;
    std::array<std::vector<uint8_t>,3> streams;
    uint32_t vertices=0;bool isStatic=false;
    auto read=[&](size_t si,uint32_t id,uint64_t size) {
        checkpoint();std::vector<uint8_t> bytes;std::string failure;
        Require(c.sources[si].reader->ReadPayload(id,size,bytes,failure),"COMPOSE_SEGMENT_PAYLOAD: "+failure);
        return bytes;
    };
    for(const auto& segment:group.at("segments")) {
        const auto id=segment.at("id").get<std::string>();
        if(defaults.value(id,segment.at("default").get<std::string>())!="on") continue;
        const auto si=segment.at("source_index").get<size_t>();
        const auto mi=segment.at("mesh").get<uint32_t>();
        const auto& mesh=c.sources[si].manifest.at("meshes").at(mi);
        auto draw=mesh.at("draws").at(segment.at("draw").get<size_t>());
        if(segment.contains("triangles") && segment.at("triangles").empty())continue;
        auto found=bases.find({si,mi});
        if(found==bases.end()) {
            const bool meshStatic=mesh.value("renderer_kind",std::string{})=="static";
            if(merged.is_null()) {
                merged=mesh;isStatic=meshStatic;
            } else {
                bool same=meshStatic==isStatic && mesh.at("attributes")==merged.at("attributes") && mesh.at("streams").size()==merged.at("streams").size();
                for(size_t i=0;same && i<mesh.at("streams").size();++i)
                    same=mesh.at("streams")[i].at("stride")==merged.at("streams")[i].at("stride");
                Require(same,"COMPOSE_SEGMENT_LAYOUT: selected meshes have incompatible vertex layouts");
            }
            const auto count=mesh.at("vertex_count").get<uint32_t>();
            Require(count>0 && uint64_t(vertices)+count<=1048576,"COMPOSE_SEGMENT_BUDGET: merged vertices exceed 1048576");
            std::vector<uint32_t> boneMap;
            for(auto bone:mesh.at("bones")) {
                remapComponent(si,bone["component"]);const auto key=bone.dump();
                auto [it,added]=bones.emplace(key,uint32_t(palette.size()));
                if(added) palette.push_back(bone);boneMap.push_back(it->second);
            }
            Require(palette.size()<=256,"COMPOSE_SEGMENT_BUDGET: merged bone palette exceeds 256");
            for(size_t i=0;i<mesh.at("streams").size();++i) {
                const auto stride=mesh.at("streams")[i].at("stride").get<uint32_t>();
                auto bytes=read(si,mesh.at("streams")[i].at("payload").get<uint32_t>(),uint64_t(count)*stride);
                if(i==2 && !isStatic) {
                    Require(stride==4 || stride==12 || stride==32,"COMPOSE_SEGMENT_LAYOUT: unsupported skin layout");
                    for(uint32_t v=0;v<count;++v) for(size_t k=0;k<4;++k) {
                        auto* p=bytes.data()+size_t(v)*stride+(stride==32?16+k*4:stride-4+k);
                        uint32_t index=0;if(stride==32) std::memcpy(&index,p,4);else index=*p;
                        Require(index<boneMap.size(),"COMPOSE_SEGMENT_PAYLOAD: skin index outside source palette");
                        index=boneMap[index];if(stride==32)std::memcpy(p,&index,4);else *p=uint8_t(index);
                    }
                }
                streams[i].insert(streams[i].end(),bytes.begin(),bytes.end());
            }
            found=bases.emplace(std::make_pair(si,mi),vertices).first;vertices+=count;
        }
        const auto width=mesh.at("index_size").get<uint32_t>();const auto sourceCount=draw.at("count").get<uint32_t>();
        const auto count=segment.contains("triangles")?uint32_t(segment.at("triangles").size()*3):sourceCount;
        Require(width==2 || width==4,"COMPOSE_SEGMENT_PAYLOAD: invalid index width");
        auto input=read(si,draw.at("indices").get<uint32_t>(),uint64_t(sourceCount)*width);
        std::vector<uint8_t> output(size_t(count)*4);
        for(uint32_t i=0;i<count;++i) {
            const auto sourceIndex=segment.contains("triangles")?segment.at("triangles").at(i/3).get<uint32_t>()*3+i%3:i;
            Require(sourceIndex<sourceCount,"COMPOSE_BODY: triangle outside source draw");
            uint32_t index=0;std::memcpy(&index,input.data()+size_t(sourceIndex)*width,width);
            Require(index<mesh.at("vertex_count").get<uint32_t>(),"COMPOSE_SEGMENT_PAYLOAD: index outside source mesh");
            index+=found->second;std::memcpy(output.data()+size_t(i)*4,&index,4);
        }
        J operations=J::array();
        for(const auto& op:c.sources[si].manifest.at("component_rules").at(receiver.sources.at(si)).at("candidates"))
            if(op.at("operation")=="replace" && op.at("mesh")==mi)
                operations.push_back(RenameCondition(op.value("when",J(true)),optionIds[si]));
        const auto predicate=And(Any(operations),RenameCondition(draw.value("when",J(true)),optionIds[si]));
        draw["indices"]=store(output);draw["when"]=predicate;draw["composition_segment"]=segment.at("preview_key");
        draw["count"]=count;
        if(segment.contains("body_draw")){draw["composition_body_draw"]=segment.at("body_draw");draw["composition_triangles"]=segment.at("triangles");draw["composition_body_region"]=segment.at("body_region");}
        remapComponent(si,draw["material_component"]);remapTextures(si,draw["textures"]);
        draws.push_back(std::move(draw));conditions.push_back(predicate);
    }
    active=Any(conditions);if(merged.is_null()) return J();
    for(size_t i=0;i<merged["streams"].size();++i) merged["streams"][i]["payload"]=store(streams[i]);
    merged["vertex_count"]=vertices;merged["index_size"]=4;merged["bones"]=palette;merged["draws"]=draws;
    return merged;
}
