#pragma once
#include <algorithm>
#include <functional>
#include <map>
#include <set>
#include <stdexcept>
#include <vector>
#include "../../shared/third_party/nlohmann/json.hpp"

namespace BetterEndfieldNext::CustomModel {
// Bounded multi-valued decision diagram over saved choices. Hidden groups have
// no effective value. This matches the runtime and avoids Cartesian expansion.
class ComposeConditions {
    using J=nlohmann::json;
    struct Node {size_t level;std::vector<int> children;};
    std::vector<Node> nodes{{0,{}},{0,{}}};
    std::map<std::pair<size_t,std::vector<int>>,int> unique;
    std::map<std::tuple<bool,int,int>,int> binary;
    std::map<int,int> negated{{0,1},{1,0}};
    std::map<std::pair<std::string,std::string>,int> atoms;
    const J& groups;
    const std::function<void()>& checkpoint;
    size_t Top(int n) const {return n<2?groups.size():nodes[n].level;}
    int Child(int n,size_t level,size_t choice) const {return Top(n)==level?nodes[n].children[choice]:n;}
    int Make(size_t level,std::vector<int> children) {
        if(std::all_of(children.begin(),children.end(),[&](int n){return n==children[0];})) return children[0];
        const auto key=std::make_pair(level,children);
        if(const auto found=unique.find(key);found!=unique.end()) return found->second;
        if(nodes.size()>=250002) throw std::runtime_error("COMPOSE_CONDITION_BUDGET: symbolic graph exceeds 250000 nodes");
        if(nodes.size()%1024==0) checkpoint();
        const auto id=int(nodes.size());unique.emplace(key,id);nodes.push_back({level,std::move(children)});return id;
    }
public:
    ComposeConditions(const J& g,const std::function<void()>& check):groups(g),checkpoint(check) {
        for(size_t i=0;i<groups.size();++i) {
            const auto& group=groups[i];const auto active=Condition(group.value("available_when",J(true)));
            for(size_t j=0;j<group.at("choices").size();++j) {
                std::vector<int> children(group.at("choices").size(),0);children[j]=1;
                atoms.emplace(std::make_pair(group.at("id").get<std::string>(),group.at("choices")[j].at("id").get<std::string>()),Both(active,Make(i,std::move(children))));
            }
        }
    }
    int Not(int n) {
        if(negated.contains(n)) return negated.at(n);
        const auto node=nodes[n];std::vector<int> children;
        for(int c:node.children) children.push_back(Not(c));
        const int result=Make(node.level,std::move(children));negated[n]=result;return result;
    }
    int Combine(bool conjunction,int a,int b) {
        if(conjunction) {if(!a||!b)return 0;if(a==1)return b;if(b==1)return a;}
        else {if(a==1||b==1)return 1;if(!a)return b;if(!b)return a;}
        if(a==b)return a;if(a>b)std::swap(a,b);const auto key=std::make_tuple(conjunction,a,b);
        if(binary.contains(key))return binary.at(key);
        const auto level=std::min(Top(a),Top(b));std::vector<int> children;
        for(size_t i=0;i<groups[level].at("choices").size();++i) children.push_back(Combine(conjunction,Child(a,level,i),Child(b,level,i)));
        const auto result=Make(level,std::move(children));binary.emplace(key,result);return result;
    }
    int Both(int a,int b) {return Combine(true,a,b);}
    int Either(int a,int b) {return Combine(false,a,b);}
    int Condition(const J& condition) {
        if(condition.is_boolean())return condition.get<bool>()?1:0;
        if(condition.contains("eq"))return atoms.at({condition.at("eq")[0].get<std::string>(),condition.at("eq")[1].get<std::string>()});
        if(condition.contains("not"))return Not(Condition(condition.at("not")));
        const bool all=condition.contains("all");int n=all?1:0;
        for(const auto& child:condition.at(all?"all":"any")) n=Combine(all,n,Condition(child));
        return n;
    }
    std::string Witness(int domain) const {
        std::string result;
        for(size_t i=0;i<groups.size();++i) {
            const auto& group=groups[i];size_t choice=0;
            for(size_t j=0;j<group.at("choices").size();++j)if(group["choices"][j]["id"]==group["default"])choice=j;
            if(Top(domain)==i) {
                if(!Child(domain,i,choice))for(size_t j=0;j<group.at("choices").size();++j)if(Child(domain,i,j)){choice=j;break;}
                domain=Child(domain,i,choice);
            }
            if(!result.empty())result+='&';result+=group.at("id").get<std::string>()+":"+group.at("choices")[choice].at("id").get<std::string>();
        }
        return result;
    }
    using Term=std::pair<int,uint64_t>;
    uint64_t Maximum(int domain,const std::vector<Term>& terms,bool* exact=nullptr) {
        if(exact)*exact=true;
        struct Overflow {};
        std::map<std::pair<int,std::vector<Term>>,int64_t> cache;
        size_t visited=0;
        std::function<int64_t(int,const std::vector<Term>&)> solve=[&](int d,const std::vector<Term>& input)->int64_t {
            if(!d)return -1;if(++visited%1024==0)checkpoint();if(visited>50000)throw Overflow{};
            uint64_t base=0;std::vector<Term> rest;size_t level=Top(d);
            for(const auto& [n,w]:input)if(n==1)base+=w;else if(n&&w){rest.emplace_back(n,w);level=std::min(level,Top(n));}
            if(rest.empty())return int64_t(base);
            std::sort(rest.begin(),rest.end());const auto key=std::make_pair(d,rest);
            if(cache.contains(key))return int64_t(base)+cache.at(key);
            int64_t best=-1;
            for(size_t j=0;j<groups[level].at("choices").size();++j) {
                std::vector<Term> next;for(const auto& [n,w]:rest)next.emplace_back(Child(n,level,j),w);
                best=std::max(best,solve(Child(d,level,j),next));
            }
            cache.emplace(key,best);return int64_t(base)+best;
        };
        try {return uint64_t(std::max<int64_t>(0,solve(domain,terms)));}
        catch(const Overflow&) {if(exact)*exact=false;uint64_t bound=0;for(const auto& [n,w]:terms)if(n)bound+=w;return bound;}
    }
};

struct ComposeResourceBudget {
    uint64_t bytes=0,textures=0;
    bool exact=true;
};

// Count resources only under predicates where they coexist. A texture slot's
// mutually exclusive alternatives and inactive source draws are not additive.
inline ComposeResourceBudget ComposeResourceBounds(const nlohmann::json& manifest,
    const std::vector<uint64_t>& payloadBytes,const std::function<void()>& checkpoint) {
    using J=nlohmann::json;
    ComposeConditions logic(manifest.at("option_groups"),checkpoint);int domain=1;
    for(const auto& condition:manifest.at("selection_constraints"))domain=logic.Both(domain,logic.Condition(condition));
    if(!domain)throw std::runtime_error("COMPOSE_VALIDATION_FAILED: no reachable option combination");
    std::map<std::string,std::vector<ComposeConditions::Term>> geometry;
    std::map<std::string,std::map<uint32_t,int>> textures;
    auto addTexture=[&](const std::string& resource,uint32_t id,int predicate) {
        auto& active=textures[resource][id];active=logic.Either(active,predicate);
    };
    auto addTextures=[&](const std::string& resource,const J& refs,int predicate) {
        for(const auto& ref:refs) {
            if(!ref.is_object()) {if(!ref.is_null())addTexture(resource,ref.get<uint32_t>(),predicate);continue;}
            for(const auto& slot:manifest.at("texture_slots"))if(slot.at("id")==ref.at("slot"))
                for(const auto& candidate:slot.at("candidates"))if(!candidate.at("texture").is_null())
                    addTexture(resource,candidate.at("texture").get<uint32_t>(),logic.Both(predicate,logic.Condition(candidate.value("when",J(true)))));
        }
    };
    for(const auto& rule:manifest.at("component_rules")) {
        checkpoint();const auto& component=manifest.at("target").at("components").at(rule.at("target").get<size_t>());
        const auto resource=component.value("resource",std::string{});
        for(const auto& op:rule.at("candidates")) {
            const int predicate=logic.Condition(op.value("when",J(true)));
            if(op.contains("material_overrides"))for(const auto& material:op.at("material_overrides"))addTextures(resource,material.at("textures"),predicate);
            if(op.at("operation")!="replace")continue;
            const auto& mesh=manifest.at("meshes").at(op.at("mesh").get<size_t>());
            for(const auto& stream:mesh.at("streams"))geometry[resource].emplace_back(predicate,
                mesh.at("vertex_count").get<uint64_t>()*stream.at("stride").get<uint64_t>());
            for(const auto& draw:mesh.at("draws")) {
                const int active=logic.Both(predicate,logic.Condition(draw.value("when",J(true))));
                geometry[resource].emplace_back(active,draw.at("count").get<uint64_t>()*mesh.at("index_size").get<uint64_t>());
                addTextures(resource,draw.at("textures"),active);
            }
        }
    }
    for(const auto& [resource,items]:textures)geometry.try_emplace(resource);
    ComposeResourceBudget result;
    for(const auto& [resource,terms]:geometry) {
        auto bytes=terms;std::vector<ComposeConditions::Term> count;
        for(const auto& [id,active]:textures[resource]) {
            count.emplace_back(active,1);
            bytes.emplace_back(active,payloadBytes.at(manifest.at("textures").at(id).at("payload").get<size_t>()));
        }
        // Aggregate identical predicates before optimization. Most variant
        // atlases share a small number of gates, even when they have many draws.
        auto aggregate=[](const std::vector<ComposeConditions::Term>& input) {
            std::map<int,uint64_t> weights;for(const auto& [node,weight]:input)weights[node]+=weight;
            return std::vector<ComposeConditions::Term>(weights.begin(),weights.end());
        };
        bool exactBytes=true,exactCount=true;
        result.bytes=std::max(result.bytes,logic.Maximum(domain,aggregate(bytes),&exactBytes));
        result.textures=std::max(result.textures,logic.Maximum(domain,aggregate(count),&exactCount));
        result.exact&=exactBytes&&exactCount;
    }
    return result;
}

inline std::set<std::string> ComposeValidationSelections(const nlohmann::json& manifest,const std::function<void()>& checkpoint) {
    using J=nlohmann::json;ComposeConditions logic(manifest.at("option_groups"),checkpoint);int domain=1;
    for(const auto& constraint:manifest.at("selection_constraints"))domain=logic.Both(domain,logic.Condition(constraint));
    auto require=[](bool ok,const char* why){if(!ok)throw std::runtime_error(why);};
    require(domain!=0,"COMPOSE_VALIDATION_FAILED: no reachable option combination");
    std::set<std::string> samples;
    auto sample=[&](int predicate){const auto legal=logic.Both(domain,predicate);if(legal)samples.insert(logic.Witness(legal));};
    std::vector<int> meshes(manifest.at("meshes").size(),0);
    std::map<std::string,std::vector<ComposeConditions::Term>> drawCosts,indexCosts;
    for(const auto& rule:manifest.at("component_rules")) {
        checkpoint();int covered=0;const auto& component=manifest.at("target").at("components").at(rule.at("target").get<size_t>());
        const auto resource=component.value("resource",std::string{});
        for(const auto& op:rule.at("candidates")) {
            const int predicate=logic.Condition(op.value("when",J(true)));
            require(!logic.Both(domain,logic.Both(covered,predicate)),"COMPOSE_VALIDATION_FAILED: overlapping component rules");
            covered=logic.Either(covered,predicate);sample(predicate);
            if(op.at("operation")=="replace") {
                const auto id=op.at("mesh").get<size_t>();meshes.at(id)=logic.Either(meshes.at(id),predicate);
                for(const auto& draw:manifest.at("meshes")[id].at("draws")) {
                    const int active=logic.Both(predicate,logic.Condition(draw.value("when",J(true))));
                    drawCosts[resource].emplace_back(active,1);indexCosts[resource].emplace_back(active,draw.at("count").get<uint64_t>());
                }
            }
        }
        require(!logic.Both(domain,logic.Not(covered)),"COMPOSE_VALIDATION_FAILED: missing component rule");
    }
    for(size_t i=0;i<meshes.size();++i) {
        int covered=0;for(const auto& draw:manifest.at("meshes")[i].at("draws")) {
            const int predicate=logic.Condition(draw.value("when",J(true)));covered=logic.Either(covered,predicate);sample(logic.Both(meshes[i],predicate));
        }
        require(!logic.Both(domain,logic.Both(meshes[i],logic.Not(covered))),"COMPOSE_VALIDATION_FAILED: selected mesh has no draw");
    }
    for(const auto& slot:manifest.value("texture_slots",J::array())) {
        int covered=0;for(const auto& candidate:slot.at("candidates")) {
            const int predicate=logic.Condition(candidate.value("when",J(true)));
            require(!logic.Both(domain,logic.Both(covered,predicate)),"COMPOSE_VALIDATION_FAILED: overlapping texture slot rules");
            covered=logic.Either(covered,predicate);sample(predicate);
        }
        require(!logic.Both(domain,logic.Not(covered)),"COMPOSE_VALIDATION_FAILED: missing texture slot rule");
    }
    for(const auto& group:manifest.at("option_groups"))for(const auto& choice:group.at("choices"))sample(logic.Condition(J{{"eq",J::array({group.at("id"),choice.at("id")})}}));
    for(const auto& [resource,terms]:drawCosts)require(logic.Maximum(domain,terms)<=256,"COMPOSE_SELECTION_BUDGET: reachable draw count exceeds 256");
    for(const auto& [resource,terms]:indexCosts)require(logic.Maximum(domain,terms)<=16777216,"COMPOSE_SELECTION_BUDGET: reachable index count exceeds limit");
    return samples;
}
}
