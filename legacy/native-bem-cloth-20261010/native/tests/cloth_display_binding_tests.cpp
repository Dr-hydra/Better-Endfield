#include "../modules/custom_model/cloth_display_binding.h"
#include <iostream>
using namespace BetterEndfieldNext::CustomModel::ClothDisplay;
void Check(bool ok){if(!ok)throw std::runtime_error("cloth display binding regression");}
bool Near(V a,V b){return Dot(a-b,a-b)<1e-16;}
int main(){
    M identity{1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1};
    M posed{0,2,0,0,-2,0,0,0,0,0,2,0,7,-3,5,1};
    V desired{8,1,-2};Check(Near(Point(posed,Point(Inverse(posed),desired)),desired));
    auto skin=Skin({identity,posed},{32768,32767,0,0},{0,1,0,0});
    Check(Near(Point(skin,Point(Inverse(skin),desired)),desired));
    V a{0,0,0},b{1,0,0},c{0,1,0};std::array<double,3> bary{.2,.3,.5};V offset{0,0,.04};
    V rest=Follow(a,b,c,bary,offset);Check(Near(rest,{.3,.5,.04}));
    Check(Near(Follow(Point(posed,a),Point(posed,b),Point(posed,c),bary,offset*2),Point(posed,rest)));
    M scaled=posed;scaled[8]=.3;scaled[9]=-.2;scaled[10]=3;
    V follower{.01,-.02,.04};
    Check(Near(Follow(Point(scaled,a),Point(scaled,b),Point(scaled,c),bary,TransportOffset(scaled,a,b,c,follower)),
        Point(scaled,Follow(a,b,c,bary,follower))));
    bool rejected=false;try{Inverse(M{});}catch(...){rejected=true;}Check(rejected);
    rejected=false;try{Skin({identity},{65535,0,0,0},{3,0,0,0});}catch(...){rejected=true;}Check(rejected);
    std::cout<<"PASS cloth display: animated/weighted inverse skinning, triangle follower with scale, singular matrix and invalid palette rejection\n";
}
