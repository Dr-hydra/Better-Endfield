#include "math/pose_file.h"
#include <cstdio>
#include <string>
static int fails=0;
#define CHECK(c,m) do{ if(!(c)){fails++;std::printf("FAIL: %s\n",m);} }while(0)

int main(){
    PoseDoc doc;
    doc.name="test_pose";
    doc.morphs.push_back({"Mouth_A", 0.6f});
    doc.bones.push_back({"spine_01", {0,0,0}, {0,0,0,1}});
    doc.bones.push_back({"arm_R_01", {0.2f,1,0}, {0,0,0.707f,0.707f}});

    std::string json = PoseToJson(doc);
    PoseDoc back = PoseFromJson(json);
    CHECK(back.name==doc.name, "name round trip");
    CHECK(back.bones.size()==2, "bone count round trip");
    CHECK(back.bones[1].name=="arm_R_01", "bone name round trip");
    CHECK(fabsf(back.bones[1].rot.z-0.707f)<1e-3f, "rotation round trip");
    CHECK(back.morphs[0].value==0.6f, "morph round trip");
    std::printf(fails?"%d FAILURES\n":"pose_file OK\n", fails);
    return fails?1:0;
}
