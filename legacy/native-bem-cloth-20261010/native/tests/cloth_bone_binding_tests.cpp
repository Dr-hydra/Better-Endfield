#include "../modules/custom_model/cloth_bone_binding.h"
#include <iostream>
using namespace BetterEndfieldNext::CustomModel;
namespace CD=BetterEndfieldNext::CustomModel::ClothDisplay;using CD::M;using CD::V;
void Check(bool ok,const char* what){if(!ok)throw std::runtime_error(what);}
bool Near(const M& a,const M& b){for(int i=0;i<16;++i)if(std::abs(a[i]-b[i])>1e-9)return false;return true;}
int main(){
    // Non-trivial pelvis: rotated, scaled 0.01 (FBX-style), translated.
    M pelvis_world{0,0.01,0,0,-0.01,0,0,0,0,0,0.01,0,3,-2,1.5,1};
    M pelvis_bind=CD::Inverse(M{0,0,1,0,0,1,0,0,-1,0,0,0,0,1.02,0.01,1});
    std::vector<ClothBone::Joint> joints{{"a",-1,{0.1,-0.1,1.05}},{"b",0,{0.12,-0.11,0.95}},{"c",1,{0.15,-0.12,0.85}}};
    auto placed=ClothBone::Place(joints,pelvis_bind);
    // Rest invariant: joint world * joint bindpose == pelvis world * pelvis bindpose.
    M chain=pelvis_world;
    for(size_t i=0;i<joints.size();++i){
        chain=CD::Multiply(chain,ClothBone::Translation(placed.local_positions[i]));
        Check(Near(CD::Multiply(chain,placed.bindposes[i]),CD::Multiply(pelvis_world,pelvis_bind)),"rest skin differs from pelvis");
    }
    // A joint's bind position maps to its own origin.
    V origin=CD::Point(placed.bindposes[2],joints[2].mesh_position);
    Check(CD::Dot(origin,origin)<1e-18,"joint bindpose origin mismatch");
    // Authored bone frames keep the bind surface invariant through rotations.
    std::vector<ClothBone::Joint> authored{{"root",-1,{0.1,-0.1,1.05}}, {"child",0,{0.12,-0.11,0.95}}};
    M r0{0,1,0,0,-1,0,0,0,0,0,1,0,.1,-.1,1.05,1};
    M r1{1,0,0,0,0,0,1,0,0,-1,0,0,.12,-.11,.95,1};
    authored[0].mesh_frame=r0;authored[1].mesh_frame=r1;
    auto fitted=ClothBone::Place(authored,pelvis_bind);chain=pelvis_world;
    for(size_t i=0;i<authored.size();++i) {
        chain=CD::Multiply(chain,CD::Multiply(ClothBone::Translation(fitted.local_positions[i]),fitted.local_rotations[i]));
        Check(Near(CD::Multiply(chain,fitted.bindposes[i]),CD::Multiply(pelvis_world,pelvis_bind)),"authored rotation changed rest skin");
        const auto q=ClothBone::Quaternion(fitted.local_rotations[i]);
        Check(std::abs(q[0]*q[0]+q[1]*q[1]+q[2]*q[2]+q[3]*q[3]-1)<1e-9,"authored quaternion not normalized");
    }
    uint8_t vertex[12]{};
    ClothBone::EncodeSkin(vertex,{{3,0.2},{200,0.5},{7,0.2},{9,0.05},{11,0.05}});
    uint16_t w[4];std::memcpy(w,vertex,8);
    Check(w[0]+w[1]+w[2]+w[3]==65535&&vertex[8]==200,"skin encoding not normalized/sorted");
    bool rejected=false;try{ClothBone::EncodeSkin(vertex,{{256,1}});}catch(...){rejected=true;}Check(rejected,"palette overflow accepted");
    rejected=false;try{ClothBone::Place({{"x",0,{0,0,0}}},pelvis_bind);}catch(...){rejected=true;}Check(rejected,"self parent accepted");
    std::cout<<"PASS cloth bone binding: rest invariant through scaled pelvis chain, joint origin, skin encoding/limits\n";
}
