#include "../modules/custom_model/cloth_proxy_validation.h"
#include <iostream>
#include <limits>
using namespace BetterEndfieldNext::CustomModel::ClothPrototype;
int main() {
    const std::vector<Point> points{{0,0,0},{1,0,0},{0,1,0},{2,0,0},{3,0,0},{2,1,0}};
    const std::vector<Face> faces{{0,1,2},{3,4,5}};
    Validate(points,faces,{0,3});
    int refused=0;
    auto reject=[&](auto p,auto f,std::vector<uint32_t> fixed){
        try{Validate(p,f,fixed);}catch(const std::invalid_argument&){++refused;return;}
        throw std::runtime_error("malformed cloth was accepted");
    };
    reject(points,faces,{0});
    auto wrong=faces;wrong[1][2]=UINT32_MAX;reject(points,wrong,{0,3});
    auto duplicate=faces;duplicate.push_back({2,1,0});reject(points,duplicate,{0,3});
    auto degenerate=points;degenerate[2]={2,0,0};reject(degenerate,faces,{0,3});
    auto nan=points;nan[0][0]=std::numeric_limits<float>::quiet_NaN();reject(nan,faces,{0,3});
    reject(points,faces,{0,0});
    auto extra=points;extra.push_back({0,0,1});reject(extra,faces,{0,3});
    if(refused!=7)return 1;
    std::cout<<"Cloth proxy runtime preflight: valid anchored islands and 7 malformed cases passed\n";
}
