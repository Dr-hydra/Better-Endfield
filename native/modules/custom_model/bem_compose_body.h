#pragma once
// Body regions partition existing triangle indices; positions/UVs/skin stay intact.
const std::array<std::string,9> BodyRegions={"neck","head","torso","pelvis","left_arm","right_arm","left_leg","right_leg","unclassified"};
std::string BodyBoneRegion(std::string name) {
    std::transform(name.begin(),name.end(),name.begin(),[](unsigned char c){return char(std::tolower(c));});
    auto has=[&](const char* text){return name.find(text)!=name.npos;};
    const bool left=has("left")||has("_l_")||has("_lthigh")||has("_lcalf")||has("_lfoot")||has("_ltoe")||has("_lupper")||has("_luparm")||has("_lfore")||has("_lhand")||has("_lclav")||has("_lfinger");
    const bool right=has("right")||has("_r_")||has("_rthigh")||has("_rcalf")||has("_rfoot")||has("_rtoe")||has("_rupper")||has("_ruparm")||has("_rfore")||has("_rhand")||has("_rclav")||has("_rfinger");
    if(has("neck"))return "neck";
    if(has("head")||has("jaw")||has("eye")||has("face"))return "head";
    if(has("thigh")||has("calf")||has("foot")||has("toe")||has("leg"))return left?"left_leg":right?"right_leg":"unclassified";
    if(has("clav")||has("shoulder")||has("arm")||has("hand")||has("finger"))return left?"left_arm":right?"right_arm":"unclassified";
    if(has("pelvis")||has("hip"))return "pelvis";
    if(has("spine")||has("chest")||has("abdomen")||has("breast")||has("xiong_"))return "torso";
    return "unclassified";
}
bool BodyReceiver(const J& component) {
    auto name=component.at("mesh_name").get<std::string>();
    std::transform(name.begin(),name.end(),name.begin(),[](unsigned char c){return char(std::tolower(c));});
    return name=="body" || name.find("_body_")!=name.npos;
}
J BodyPartition(Source& source,const J& mesh,const J& draw) {
    J regions=J::object();for(const auto& region:BodyRegions)regions[region]=J::array();
    const auto vertices=mesh.at("vertex_count").get<uint32_t>(),width=mesh.at("index_size").get<uint32_t>();
    const auto count=draw.at("count").get<uint32_t>();
    Require((width==2||width==4)&&count%3==0,"COMPOSE_BODY: invalid triangle indices");
    if(draw.contains("composition_body_region")) {
        const auto region=draw.at("composition_body_region").get<std::string>();
        Require(regions.contains(region),"COMPOSE_BODY: invalid saved region");
        for(uint32_t tri=0;tri<count/3;++tri)regions[region].push_back(tri);
        return regions;
    }
    std::vector<uint8_t> indices,skin;std::string error;
    Require(source.reader->ReadPayload(draw.at("indices"),uint64_t(count)*width,indices,error),"COMPOSE_BODY: "+error);
    std::vector<std::array<double,9>> scores(vertices);
    if(mesh.value("renderer_kind",std::string{})!="static" && mesh.at("streams").size()==3) {
        const auto& stream=mesh.at("streams")[2];const auto stride=stream.at("stride").get<uint32_t>();
        Require(stride==4||stride==12||stride==32,"COMPOSE_BODY: unsupported skin layout");
        Require(source.reader->ReadPayload(stream.at("payload"),uint64_t(vertices)*stride,skin,error),"COMPOSE_BODY: "+error);
        std::vector<size_t> bones;for(const auto& bone:mesh.at("bones")) {
            const auto region=BodyBoneRegion(bone.value("name",std::string{}));
            bones.push_back(size_t(std::find(BodyRegions.begin(),BodyRegions.end(),region)-BodyRegions.begin()));
        }
        for(uint32_t v=0;v<vertices;++v)for(size_t k=0;k<4;++k) {
            uint32_t bone=0;double weight=0;const auto* p=skin.data()+size_t(v)*stride;
            if(stride==32) {float w;std::memcpy(&w,p+k*4,4);std::memcpy(&bone,p+16+k*4,4);weight=w;}
            else {bone=p[stride-4+k];if(stride==12){uint16_t w;std::memcpy(&w,p+k*2,2);weight=double(w)/65535;}else weight=k==0?1:0;}
            Require(bone<bones.size() && std::isfinite(weight) && weight>=0,"COMPOSE_BODY: invalid skin weights or palette");
            scores[v][bones[bone]]+=weight;
        }
    } else for(auto& score:scores)score.back()=1;
    for(uint32_t tri=0;tri<count/3;++tri) {
        std::array<double,9> total{};
        std::array<uint32_t,3> corners{};
        for(size_t corner=0;corner<3;++corner) {
            uint32_t index=0;std::memcpy(&index,indices.data()+(size_t(tri)*3+corner)*width,width);
            Require(index<vertices,"COMPOSE_BODY: index outside source mesh");
            corners[corner]=index;
            for(size_t r=0;r<total.size();++r)total[r]+=scores[index][r];
        }
        auto best=std::max_element(total.begin(),total.end());
        const bool degenerate=corners[0]==corners[1]||corners[1]==corners[2]||corners[0]==corners[2];
        const auto region=*best>0 && !degenerate?BodyRegions[size_t(best-total.begin())]:std::string("unclassified");
        regions[region].push_back(tri);
    }
    return regions;
}
void ApplyBodyEdits(J& groups,const J& edits) {
    Require(edits.is_object(),"COMPOSE_BODY: expected an edit map");std::set<std::string> known;
    for(auto& group:groups) {
        std::map<std::string,std::vector<J*>> draws;
        for(auto& segment:group["segments"])if(segment.contains("body_region"))draws[segment.at("body_draw")].push_back(&segment);
        for(auto& [key,segments]:draws) {
            known.insert(key);if(!edits.contains(key))continue;
            const auto& changes=edits.at(key);Require(changes.is_object(),"COMPOSE_BODY: expected triangle assignments");
            std::map<std::string,J> triangles;uint64_t count=0;
            for(const auto* segment:segments){triangles[segment->at("body_region")]=J::array();count+=segment->at("triangles").size();}
            for(const auto& [tri,region]:changes.items()) {
                Require(!tri.empty() && tri.find_first_not_of("0123456789")==tri.npos && tri.size()<10,"COMPOSE_BODY: invalid triangle ID");
                const auto index=std::stoull(tri);Require(std::to_string(index)==tri && index<count && region.is_string() && triangles.contains(region.get<std::string>()),"COMPOSE_BODY: invalid triangle assignment");
            }
            for(const auto* segment:segments)for(const auto& tri:segment->at("triangles")) {
                const auto id=std::to_string(tri.get<uint32_t>());
                const auto region=changes.value(id,segment->at("body_region").get<std::string>());triangles[region].push_back(tri);
            }
            for(auto* segment:segments){(*segment)["triangles"]=triangles.at(segment->at("body_region"));(*segment)["count"]=segment->at("triangles").size()*3;}
        }
    }
    for(const auto& [key,value]:edits.items())Require(known.contains(key),"COMPOSE_BODY: unknown source draw");
}
