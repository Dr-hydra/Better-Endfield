#include "../modules/custom_model/cloth_layer_order.h"
#include <cstring>
#include <iostream>
namespace L=BetterEndfieldNext::CustomModel::ClothLayer;
void Check(bool ok,const char* why){if(!ok)throw std::runtime_error(why);}
int main() {
    L::Policy p;p.list=0x1000;
    p.inner={10,100,{{.8,-1,0},{.8,1,0},{.8,-1,-1}},{0,0,0},{0,0,1}};
    p.outer={11,200,{{1,-1,0},{1,1,0},{1,-1,-1}},{0,0,0},{0,0,1}};
    Check(L::Valid(p),"valid pair refused");
    L::Contact c;c.flags[0]=0x80000000|10;c.flags[1]=11;c.point=100;
    c.triangle[0]=200;c.triangle[1]=201;c.triangle[2]=202;c.sign=0xbc00;c.thickness=0x1800;c.point_mass=0x3c00;
    L::Contact copy;
    Check(L::Correct(p,c,copy)==L::Result::Changed&&copy.sign==0x3c00,"inner pushed outside");
    auto before=c;before.sign=copy.sign;Check(std::memcmp(&before,&copy,sizeof(copy))==0,"contact fields changed beyond sign");
    Check(c.sign==0xbc00,"shared source contact changed");
    std::swap(c.triangle[0],c.triangle[1]);c.sign=0x3c00;
    Check(L::Correct(p,c,copy)==L::Result::Changed&&copy.sign==0xbc00,"triangle winding not respected");
    c.flags[0]=11;c.flags[1]=10;c.point=200;c.triangle[0]=100;c.triangle[1]=101;c.triangle[2]=102;c.sign=0x3c00;
    Check(L::Correct(p,c,copy)==L::Result::Changed&&copy.sign==0xbc00,"outer not kept outside");
    c.flags[0]=12;Check(L::Correct(p,c,copy)==L::Result::Foreign,"foreign team altered");
    c.flags[0]=11;c.point=999;Check(L::Correct(p,c,copy)==L::Result::Invalid,"foreign particle accepted");
    c.point=200;c.triangle[2]=101;Check(L::Correct(p,c,copy)==L::Result::Invalid,"degenerate face accepted");
    std::cout<<"PASS layer order: inner/outer, winding, sign-only local copy, foreign teams and bounds\n";
}
