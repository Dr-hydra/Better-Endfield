#include "../modules/camera/first_person_mesh.h"
#include "../modules/camera/first_person_retry.h"
#include <iostream>
#include <stdexcept>
using namespace BetterEndfield::FirstPersonMesh;
void Check(bool ok,const char* message) {if(!ok)throw std::runtime_error(message);}
Vertex V(double x,double y,double z,uint32_t bone=0) {Vertex v;v.position={x,y,z};v.bone[0]=bone;v.weight[0]=1;return v;}
int main() {
    try {
        BetterEndfield::FirstPerson::RetryBudget retries;
        int renderer_a=0,renderer_b=0,source_a=0,source_b=0;
        Check(retries.Try(&renderer_a,&source_a)&&retries.Try(&renderer_a,&source_a)&&
            !retries.Try(&renderer_a,&source_a),"unchanged binding exceeded retry limit");
        Check(retries.Try(&renderer_b,&source_a),"shared source consumed another renderer's retries");
        retries.Forget(&renderer_a);
        Check(retries.Try(&renderer_a,&source_a),"stale patch did not renew retries");
        Check(retries.Try(&renderer_a,&source_b)&&retries.Try(&renderer_a,&source_a),
            "source replacement or return did not renew retries");
        retries.Prune([&](void* renderer){return renderer==&renderer_b;});
        Check(retries.Try(&renderer_a,&source_a)&&retries.Try(&renderer_a,&source_a),
            "removed renderer retained retry history");
        retries.Clear();
        Check(retries.Try(&renderer_a,&source_a),"new session retained retry history");
        std::vector<Vertex> v;
        for(int layer=0;layer<2;++layer)for(int j=0;j<8;++j) {
            double a=j*6.283185307179586/8;v.push_back(V(.05*std::cos(a),layer?0:-.3,.05*std::sin(a)));
        }
        Part sides;
        for(uint32_t j=0;j<8;++j){uint32_t n=(j+1)%8;sides.indices.insert(sides.indices.end(),{j,j+8,n+8,j,n+8,n});}
        Frame neck{{0,0,0},{0,1,0},.1};
        auto r=Build(v,{sides},{2},neck,false,true);
        Check(r.error.empty()&&r.rings==1&&r.cap_triangles==6,"open neck must produce n-2 triangles");
        Check(std::equal(sides.indices.begin(),sides.indices.end(),r.parts[0].indices.begin()),"existing triangles changed");
        for(auto id:r.parts[0].indices)Check(id<v.size(),"cap introduced a new vertex");
        auto off=Build(v,{sides},{2},neck,false,false);
        Check(off.parts[0].indices==sides.indices,"disabled cap changed geometry");
        // UV seam: same position and weights, different index. Topology must weld it.
        auto seam=v;seam.push_back(v[8]);auto seam_part=sides;
        for(size_t i=0;i<6;++i)if(seam_part.indices[i]==8)seam_part.indices[i]=16;
        auto welded=Build(seam,{seam_part},{2},neck,false,true);
        Check(welded.rings==1&&welded.cap_triangles==6,"UV seam mistaken for an extra hole");
        seam.back().bone[0]=1;
        auto map=Weld(seam,1e-5);Check(map.back()!=8,"different skin weights welded");
        // Two disconnected components: collapse only the head-weighted component.
        std::vector<Vertex> split{V(0,0,0,0),V(1,0,0,0),V(0,1,0,0),V(2,0,0,1),V(3,0,0,1),V(2,1,0,1)};
        Part components{{0,1,2,3,4,5}};
        auto hidden=Build(split,{components},{0,1},{},false,false);
        Check(hidden.hidden_triangles==1&&hidden.parts[0].indices==std::vector<uint32_t>({0,1,2,3,3,3}),"component hide damaged body");
        auto all=Build(split,{components},{0,1},{},true,false);Check(all.hidden_triangles==2,"named head renderer not hidden");
        // Concave neck outlines require ear clipping, not a centre triangle fan.
        std::vector<Vertex> concave{V(0,0,0),V(2,0,0),V(2,2,0),V(1,1,0),V(0,2,0)};
        std::vector<uint32_t> triangles;
        Check(Triangulate({0,1,2,3,4},concave,triangles)&&triangles.size()==9,"concave triangulation failed");
        double area=0;for(size_t i=0;i<triangles.size();i+=3)area+=Length(Cross(concave[triangles[i+1]].position-concave[triangles[i]].position,concave[triangles[i+2]].position-concave[triangles[i]].position))*.5;
        Check(std::abs(area-3)<1e-10,"concave cap overlaps or covers outside polygon");
        std::vector<Vertex> bow{V(0,0,0),V(1,1,0),V(0,1,0),V(1,0,0)};
        Check(!Triangulate({0,1,2,3},bow,triangles),"self-intersection accepted");
        auto invalid=components;invalid.indices[2]=999;
        Check(!Build(split,{invalid},{0,1},{},false,true).error.empty(),"invalid index accepted");
        split[0].weight[0]=std::numeric_limits<double>::quiet_NaN();
        Check(!Build(split,{components},{0,1},{},false,true).error.empty(),"invalid weight accepted");
        std::cout<<"first_person_mesh: retry lifecycle, geometry, seam, skin, concavity, and invalid-input tests passed\n";
    }catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}
}
