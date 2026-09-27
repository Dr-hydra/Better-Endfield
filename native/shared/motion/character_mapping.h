#pragma once
// Semantic name mappings reviewed against Sasye/EIEM bone_map.h at 8b46b76.
// No HumanBodyBones numeric assumptions, addresses, field offsets or native layouts.
// Portions derived from EIEM (https://github.com/Sasye/EIEM), AGPL-3.0.
#include <vector>
#include <string>
namespace BetterEndfield::CharacterPose {
struct RigName { std::vector<std::string> sources; const char* target; bool eye=false; };
inline const std::vector<RigName>& DefaultRig() {
    static const std::vector<RigName> names{
        {{"\x83\x5a\x83\x93\x83\x5e\x81\x5b","\x89\xba\x94\xbc\x90\x67"},"Bip001_Pelvis"},
        {{"\x8f\xe3\x94\xbc\x90\x67"},"Bip001_Spine"},
        {{"\x8f\xe3\x94\xbc\x90\x67\x32"},"Bip001_Spine1"},
        {{"\x8f\xe3\x94\xbc\x90\x67\x33"},"Bip001_Spine2"},
        {{"\x8e\xf1"},"Bip001_Neck"},
        {{"\x93\xaa"},"Bip001_Head"},
        {{"\x8d\xb6\x8c\xa8"},"Bip001_L_Clavicle"},
        {{"\x8d\xb6\x98\x72"},"Bip001_L_UpperArm"},
        {{"\x8d\xb6\x82\xd0\x82\xb6"},"Bip001_L_Forearm"},
        {{"\x8d\xb6\x8e\xe8\x8e\xf1"},"Bip001_L_Hand"},
        {{"\x8d\xb6\x90\x65\x8e\x77\x82\x4f"},"Bip001_L_Finger0"},
        {{"\x8d\xb6\x90\x65\x8e\x77\x82\x50"},"Bip001_L_Finger01"},
        {{"\x8d\xb6\x90\x65\x8e\x77\x82\x51"},"Bip001_L_Finger02"},
        {{"\x8d\xb6\x90\x6c\x8e\x77\x82\x50"},"Bip001_L_Finger1"},
        {{"\x8d\xb6\x90\x6c\x8e\x77\x82\x51"},"Bip001_L_Finger11"},
        {{"\x8d\xb6\x90\x6c\x8e\x77\x82\x52"},"Bip001_L_Finger12"},
        {{"\x8d\xb6\x92\x86\x8e\x77\x82\x50"},"Bip001_L_Finger2"},
        {{"\x8d\xb6\x92\x86\x8e\x77\x82\x51"},"Bip001_L_Finger21"},
        {{"\x8d\xb6\x92\x86\x8e\x77\x82\x52"},"Bip001_L_Finger22"},
        {{"\x8d\xb6\x96\xf2\x8e\x77\x82\x50"},"Bip001_L_Finger3"},
        {{"\x8d\xb6\x96\xf2\x8e\x77\x82\x51"},"Bip001_L_Finger31"},
        {{"\x8d\xb6\x96\xf2\x8e\x77\x82\x52"},"Bip001_L_Finger32"},
        {{"\x8d\xb6\x8f\xac\x8e\x77\x82\x50"},"Bip001_L_Finger4"},
        {{"\x8d\xb6\x8f\xac\x8e\x77\x82\x51"},"Bip001_L_Finger41"},
        {{"\x8d\xb6\x8f\xac\x8e\x77\x82\x52"},"Bip001_L_Finger42"},
        {{"\x89\x45\x8c\xa8"},"Bip001_R_Clavicle"},
        {{"\x89\x45\x98\x72"},"Bip001_R_UpperArm"},
        {{"\x89\x45\x82\xd0\x82\xb6"},"Bip001_R_Forearm"},
        {{"\x89\x45\x8e\xe8\x8e\xf1"},"Bip001_R_Hand"},
        {{"\x89\x45\x90\x65\x8e\x77\x82\x4f"},"Bip001_R_Finger0"},
        {{"\x89\x45\x90\x65\x8e\x77\x82\x50"},"Bip001_R_Finger01"},
        {{"\x89\x45\x90\x65\x8e\x77\x82\x51"},"Bip001_R_Finger02"},
        {{"\x89\x45\x90\x6c\x8e\x77\x82\x50"},"Bip001_R_Finger1"},
        {{"\x89\x45\x90\x6c\x8e\x77\x82\x51"},"Bip001_R_Finger11"},
        {{"\x89\x45\x90\x6c\x8e\x77\x82\x52"},"Bip001_R_Finger12"},
        {{"\x89\x45\x92\x86\x8e\x77\x82\x50"},"Bip001_R_Finger2"},
        {{"\x89\x45\x92\x86\x8e\x77\x82\x51"},"Bip001_R_Finger21"},
        {{"\x89\x45\x92\x86\x8e\x77\x82\x52"},"Bip001_R_Finger22"},
        {{"\x89\x45\x96\xf2\x8e\x77\x82\x50"},"Bip001_R_Finger3"},
        {{"\x89\x45\x96\xf2\x8e\x77\x82\x51"},"Bip001_R_Finger31"},
        {{"\x89\x45\x96\xf2\x8e\x77\x82\x52"},"Bip001_R_Finger32"},
        {{"\x89\x45\x8f\xac\x8e\x77\x82\x50"},"Bip001_R_Finger4"},
        {{"\x89\x45\x8f\xac\x8e\x77\x82\x51"},"Bip001_R_Finger41"},
        {{"\x89\x45\x8f\xac\x8e\x77\x82\x52"},"Bip001_R_Finger42"},
        {{"\x97\xbc\x96\xda","\x8d\xb6\x96\xda"},"eyeLfJoint",true},
        {{"\x97\xbc\x96\xda","\x89\x45\x96\xda"},"eyeRtJoint",true},
    };
    return names;
}
struct MorphName { std::vector<std::string> sources; const char* target; };
inline const std::vector<MorphName>& DefaultMorphs() {
    // These are names, not SMC hashes. A renderer without these BlendShapes is
    // reported unsupported; SMC's internal native arrays are never guessed.
    static const std::vector<MorphName> names{
        {{"\x82\xa0"},"A"},
        {{"\x82\xa2"},"I"},
        {{"\x82\xa4"},"U"},
        {{"\x82\xa6"},"E"},
        {{"\x82\xa8"},"O"},
        {{"\x82\xdc\x82\xce\x82\xbd\x82\xab","\x83\x45\x83\x42\x83\x93\x83\x4e","\x83\x45\x83\x42\x83\x93\x83\x4e\x82\x51"},"eye_thinkcloseeyes_a_L_ctrl"},
        {{"\x82\xdc\x82\xce\x82\xbd\x82\xab","\x83\x45\x83\x42\x83\x93\x83\x4e\x89\x45","\x83\x45\x83\x42\x83\x93\x83\x4e\x82\x51\x89\x45"},"eye_thinkcloseeyes_a_R_ctrl"},
        {{"\x8f\xce\x82\xa2"},"eye_relax_a_L_ctrl"},
        {{"\x8f\xce\x82\xa2"},"eye_relax_a_R_ctrl"},
    };
    return names;
}
}
