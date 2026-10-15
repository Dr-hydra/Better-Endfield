#pragma once
#include <array>
#include <cmath>
#include <cstddef>
#include <cstdint>
#include <optional>
#include <span>
#include <string_view>

// Exact Windows Snowshine 1.5.3 -> 1.6.3 donor contract. The original
// resource graph proves this palette permutation, not arbitrary name matching.
// Source: research/custom_model/1.6.3/Windows/snowshine-bem-investigation/
// current/database.json and prior-database.json; bindposes use the actual
// aurora-1_6_3-login-20261015 runtime observation (column-major Unity arrays).
// Offline graph matrices use the transposed representation; do not copy those
// arrays directly into this runtime contract.
namespace BetterEndfieldNext::CustomModel::AuroraLegacy {
inline constexpr uint64_t kLegacyIndices=87120, kCurrentIndices=87288;
inline constexpr uint32_t kCurrentVertices=23141;
inline constexpr size_t kBoneCount=87;
inline constexpr std::string_view kMeshName="S_actor_aurora_cloth_01_lod0";
inline constexpr std::string_view kReceiverPath="Mesh_all/lod0/S_actor_aurora_cloth_01_lod0";
inline constexpr std::string_view kMaterialName="M_actor_aurora_cloth_01";
inline constexpr float kBindposeTolerance=0.00001f;
struct BoneObservation {
    std::string_view name, path;
    std::array<float,16> bindpose;
};
using BoneContract=BoneObservation;
inline constexpr std::array<BoneContract,kBoneCount> kCurrentBones{{
    {"Bip001_LCalfTwist","Root/Bip001/Bip001_Pelvis/Bip001_L_Thigh/Bip001_L_Calf/Bip001_LCalfTwist",
        {-0.0176700596f,0.0225753244f,0.999588788f,0.0f,-0.0814352855f,-0.996455252f,0.0210650191f,0.0f,0.996521592f,-0.0810295716f,0.0194458682f,0.0f,-0.561956704f,0.0476704761f,0.0568270981f,1.0f}},
    {"Bip001_LCalfTwist1","Root/Bip001/Bip001_Pelvis/Bip001_L_Thigh/Bip001_L_Calf/Bip001_LCalfTwist/Bip001_LCalfTwist1",
        {-0.0176697001f,0.047430601f,0.998718083f,0.0f,-0.0814353079f,-0.995623231f,0.0458428711f,0.0f,0.996521652f,-0.0805208758f,0.0214549042f,0.0f,-0.354600042f,0.0490691699f,0.0556240007f,1.0f}},
    {"Bip001_L_Foot","Root/Bip001/Bip001_Pelvis/Bip001_L_Thigh/Bip001_L_Calf/Bip001_L_Foot",
        {1.48834616e-07f,0.0724886283f,0.997369111f,0.0f,4.4408921e-14f,-0.997368515f,0.0724886507f,0.0f,0.999999702f,-1.07887477e-08f,-1.48443092e-07f,0.0f,-0.149488926f,0.0386076309f,0.0578353517f,1.0f}},
    {"Bip001_L_Thigh","Root/Bip001/Bip001_Pelvis/Bip001_L_Thigh",
        {-0.017451169f,-0.0035981203f,0.999841392f,0.0f,-0.00728059886f,-0.999966562f,-0.00372564653f,0.0f,0.999821246f,-0.00734445918f,0.0174243879f,0.0f,-1.01846218f,0.00443052128f,0.0579952225f,1.0f}},
    {"Bip001_LThighTwist1","Root/Bip001/Bip001_Pelvis/Bip001_L_Thigh/Bip001_LThighTwist/Bip001_LThighTwist1",
        {-0.0174511056f,-0.00257916935f,0.999844611f,0.0f,-0.00728059746f,-0.999969244f,-0.00270656915f,0.0f,0.999821067f,-0.00732669514f,0.0174318049f,0.0f,-0.791150391f,0.00448961975f,0.057990741f,1.0f}},
    {"Bip001_LThighTwist","Root/Bip001/Bip001_Pelvis/Bip001_L_Thigh/Bip001_LThighTwist",
        {-0.0174511913f,-0.00156442297f,0.999846578f,0.0f,-0.00728059607f,-0.999971747f,-0.00169169449f,0.0f,0.999820948f,-0.00730900047f,0.017439317f,0.0f,-1.01846194f,0.00454847375f,0.0579860546f,1.0f}},
    {"Bip001_Pelvis","Root/Bip001/Bip001_Pelvis",
        {7.44593578e-08f,9.58670512e-08f,-1.0f,0.0f,0.0f,-0.999999881f,-9.58670512e-08f,0.0f,-0.999999881f,1.11022302e-14f,-7.44593507e-08f,0.0f,1.01730204f,-0.00276841363f,7.5482248e-08f,1.0f}},
    {"Bip001_L_Calf","Root/Bip001/Bip001_Pelvis/Bip001_L_Thigh/Bip001_L_Calf",
        {-0.0176699441f,-0.00229385355f,0.999841452f,0.0f,-0.0814353079f,-0.996671438f,-0.00372576877f,0.0f,0.996521831f,-0.081488207f,0.0174243357f,0.0f,-0.561956882f,0.0462423265f,0.0579952896f,1.0f}},
    {"Bip001_L_Toe0","Root/Bip001/Bip001_Pelvis/Bip001_L_Thigh/Bip001_L_Calf/Bip001_L_Foot/Bip001_L_Toe0",
        {-0.0724885762f,-1.82636683e-07f,0.997368991f,0.0f,0.997368872f,4.45035582e-08f,0.0724885836f,0.0f,-5.76255914e-08f,0.999999225f,1.78930321e-07f,0.0f,0.0345389843f,-0.0293803196f,0.0577850975f,1.0f}},
    {"Bip001_R_Foot","Root/Bip001/Bip001_Pelvis/Bip001_R_Thigh/Bip001_R_Calf/Bip001_R_Foot",
        {1.48509571e-07f,-0.0724886432f,0.997369111f,0.0f,-4.4408921e-14f,-0.997368813f,-0.0724886507f,0.0f,0.999999881f,1.07652554e-08f,-1.481189e-07f,0.0f,-0.149489149f,0.038607873f,-0.0578354448f,1.0f}},
    {"Bip001_R_Toe0","Root/Bip001/Bip001_Pelvis/Bip001_R_Thigh/Bip001_R_Calf/Bip001_R_Foot/Bip001_R_Toe0",
        {0.0724885911f,2.4927283e-07f,0.99736917f,0.0f,0.997368991f,3.79095866e-08f,-0.0724885911f,0.0f,-5.587939e-08f,0.999999583f,-2.45869103e-07f,0.0f,0.0345387645f,-0.0293804891f,-0.0577852428f,1.0f}},
    {"Bip001_RCalfTwist","Root/Bip001/Bip001_Pelvis/Bip001_R_Thigh/Bip001_R_Calf/Bip001_RCalfTwist",
        {0.0176697634f,-0.0225741044f,0.999588788f,0.0f,-0.0814353079f,-0.996455908f,-0.0210638121f,0.0f,0.996521771f,-0.0810296535f,-0.0194454677f,0.0f,-0.561956942f,0.0476706773f,-0.0568273962f,1.0f}},
    {"Bip001_RCalfTwist1","Root/Bip001/Bip001_Pelvis/Bip001_R_Thigh/Bip001_R_Calf/Bip001_RCalfTwist/Bip001_RCalfTwist1",
        {0.0176698528f,-0.047427889f,0.998718083f,0.0f,-0.0814353079f,-0.995623767f,-0.0458401293f,0.0f,0.996521771f,-0.0805209652f,-0.0214548297f,0.0f,-0.354600221f,0.0490692817f,-0.0556241721f,1.0f}},
    {"Bip001_R_Thigh","Root/Bip001/Bip001_Pelvis/Bip001_R_Thigh",
        {0.0174509306f,0.00359818269f,0.999841392f,0.0f,-0.00728059746f,-0.999966621f,0.00372570683f,0.0f,0.999821246f,-0.00734445918f,-0.0174241494f,0.0f,-1.01846218f,0.00443074154f,-0.0579954684f,1.0f}},
    {"Bip001_RThighTwist1","Root/Bip001/Bip001_Pelvis/Bip001_R_Thigh/Bip001_RThighTwist/Bip001_RThighTwist1",
        {0.0174509957f,0.00257783616f,0.999844372f,0.0f,-0.00728059374f,-0.999969542f,0.00270523317f,0.0f,0.999820769f,-0.00732667092f,-0.0174317062f,0.0f,-0.791150272f,0.00448992243f,-0.0579908304f,1.0f}},
    {"Bip001_RThighTwist","Root/Bip001/Bip001_Pelvis/Bip001_R_Thigh/Bip001_RThighTwist",
        {0.0174508635f,0.00155994657f,0.999846399f,0.0f,-0.00728059467f,-0.999971926f,0.00168721436f,0.0f,0.999820888f,-0.00730892224f,-0.0174390208f,0.0f,-1.01846194f,0.00454895757f,-0.05798636f,1.0f}},
    {"Bip001_R_Calf","Root/Bip001/Bip001_Pelvis/Bip001_R_Thigh/Bip001_R_Calf",
        {0.0176697373f,0.00229401258f,0.999841452f,0.0f,-0.0814353079f,-0.996671736f,0.0037259094f,0.0f,0.996521831f,-0.0814882293f,-0.017424114f,0.0f,-0.561957002f,0.0462425575f,-0.0579954907f,1.0f}},
    {"Bip001_L_UpperArm","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm",
        {0.670111477f,0.0479264744f,-0.740711629f,0.0f,-0.0845997706f,0.996341467f,-0.0120695876f,0.0f,0.737423837f,0.0707519725f,0.671714485f,0.0f,-0.961131215f,-0.102698207f,-1.02288699f,1.0f}},
    {"Bip001_LUpArmTwist","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_LUpArmTwist",
        {0.670111597f,-0.0438432358f,-0.740964532f,0.0f,-0.0845997855f,0.987237334f,-0.134925351f,0.0f,0.737424076f,0.153100416f,0.657849789f,0.0f,-0.961131573f,-0.228137016f,-1.00239611f,1.0f}},
    {"Bip001_LUpArmTwist1","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_LUpArmTwist/Bip001_LUpArmTwist1",
        {0.670111477f,0.00204554666f,-0.742257774f,0.0f,-0.0845998079f,0.993690073f,-0.0736382753f,0.0f,0.737424195f,0.112140678f,0.666055799f,0.0f,-0.858048081f,-0.165734574f,-1.01458156f,1.0f}},
    {"Bip001_Spine2","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2",
        {4.26769402e-05f,1.77720622e-07f,-1.0f,0.0f,1.14075408e-06f,-0.999999762f,-1.77671922e-07f,0.0f,-1.00000012f,-1.14074612e-06f,-4.26769257e-05f,0.0f,1.254179f,-0.0305686593f,9.54175193e-05f,1.0f}},
    {"Bip001_R_Clavicle","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle",
        {-0.941806555f,-0.0406440422f,-0.333689302f,0.0f,-0.0380443335f,0.999173224f,-0.0143248541f,0.0f,0.33399573f,-0.000796255132f,-0.942574084f,0.0f,-0.466209024f,-0.00312031386f,1.35896111f,1.0f}},
    {"R_collarA_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/R_collarA_jnt",
        {-0.361992687f,0.678722441f,0.638981223f,0.0f,0.243803114f,0.730543256f,-0.637860775f,0.0f,-0.899734735f,-0.0751153305f,-0.429925561f,0.0f,1.33980596f,0.120995492f,0.551124156f,1.0f}},
    {"Bip001_L_Clavicle","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle",
        {0.941777945f,0.0406493135f,-0.333769858f,0.0f,-0.0380492397f,0.999172986f,0.014326592f,0.0f,0.334076285f,-0.000792780775f,0.942545652f,0.0f,-0.46638912f,-0.00312808668f,-1.35889769f,1.0f}},
    {"L_collarA_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/L_collarA_jnt",
        {0.625255108f,0.417293131f,-0.659486055f,0.0f,0.23922962f,-0.90684092f,-0.346996963f,0.0f,-0.742849231f,0.0591930114f,-0.666836381f,0.0f,1.12596428f,-0.121560417f,0.913841128f,1.0f}},
    {"Bip001_L_Forearm","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm",
        {0.671637177f,-0.0158028975f,-0.740711749f,0.0f,0.0102151502f,0.999874532f,-0.0120695457f,0.0f,0.740810156f,0.000539872621f,0.671714365f,0.0f,-0.761299372f,-0.0306797978f,-1.02288675f,1.0f}},
    {"Bip001_L_ForeTwist","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_ForeTwist",
        {0.671637297f,-0.0159994829f,-0.740707338f,0.0f,0.0102151521f,0.999871373f,-0.0123349102f,0.0f,0.740810037f,0.000718146446f,0.671714187f,0.0f,-0.761299372f,-0.0309512727f,-1.02287865f,1.0f}},
    {"Bip001_L_ForeTwist1","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_ForeTwist/Bip001_L_ForeTwist1",
        {0.671637356f,-0.0161960609f,-0.740702987f,0.0f,0.010215153f,0.999867916f,-0.012600271f,0.0f,0.740810275f,0.00089641806f,0.671713889f,0.0f,-0.63272649f,-0.0312227421f,-1.02287006f,1.0f}},
    {"Bip001_R_Forearm","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm",
        {-0.671636939f,0.0158028901f,-0.740711689f,0.0f,0.0102151399f,0.999874413f,0.0120695392f,0.0f,0.740809798f,0.000539873203f,-0.671714365f,0.0f,-0.761258125f,-0.0306803603f,1.02294576f,1.0f}},
    {"Bip001_R_ForeTwist","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_ForeTwist",
        {-0.671636879f,0.0159994569f,-0.740707576f,0.0f,0.0102151437f,0.999871373f,0.0123348767f,0.0f,0.740809739f,0.000718123163f,-0.671714306f,0.0f,-0.761258125f,-0.0309518203f,1.02293777f,1.0f}},
    {"Bip001_R_ForeTwist1","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_ForeTwist/Bip001_R_ForeTwist1",
        {-0.671636939f,0.0161960032f,-0.740703404f,0.0f,0.0102151455f,0.999867797f,0.0126002003f,0.0f,0.740809798f,0.000896367535f,-0.671714187f,0.0f,-0.632685184f,-0.0312232599f,1.02292967f,1.0f}},
    {"Bip001_L_Finger21","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger2/Bip001_L_Finger21",
        {0.728012085f,0.685444713f,0.0127911884f,0.0f,-0.0157165024f,-0.00196628738f,0.999873996f,0.0f,0.685383797f,-0.728121817f,0.00934130978f,0.0f,-0.315218449f,1.05964994f,-0.0234804135f,1.0f}},
    {"Bip001_L_Finger22","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger2/Bip001_L_Finger21/Bip001_L_Finger22",
        {0.72801125f,0.685445428f,0.0127912108f,0.0f,-0.0157169942f,-0.00196581078f,0.999873996f,0.0f,0.685384512f,-0.728120983f,0.00934200827f,0.0f,-0.288395464f,1.05964947f,-0.0234810747f,1.0f}},
    {"Bip001_L_Hand","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand",
        {0.671637118f,0.740698874f,-0.0163925719f,0.0f,0.0102152396f,0.0128654558f,0.999864638f,0.0f,0.740810215f,-0.671713591f,0.0010745005f,0.0f,-0.50415349f,1.02286172f,-0.031493973f,1.0f}},
    {"Bip001_L_Finger0","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger0",
        {0.36180833f,0.0722169206f,-0.92945087f,0.0f,0.591821432f,0.752537727f,0.288849682f,0.0f,0.720307291f,-0.654577136f,0.22953479f,0.0f,-0.610143304f,0.714050293f,-0.630457401f,1.0f}},
    {"Bip001_L_Finger01","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger0/Bip001_L_Finger01",
        {0.388397247f,0.0657248348f,-0.91914469f,0.0f,0.60602355f,0.73318243f,0.308510512f,0.0f,0.694178283f,-0.676847994f,0.24493511f,0.0f,-0.53944242f,0.733915269f,-0.641618729f,1.0f}},
    {"Bip001_L_Finger1","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger1",
        {0.725784302f,0.685744882f,-0.0546902753f,0.0f,0.063979961f,0.0118684638f,0.99788028f,0.0f,0.684940875f,-0.727744818f,-0.0352599807f,0.0f,-0.351467371f,1.05747902f,0.008097874f,1.0f}},
    {"Bip001_L_Finger02","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger0/Bip001_L_Finger01/Bip001_L_Finger02",
        {0.388397157f,0.0657255277f,-0.919144809f,0.0f,0.606023312f,0.733182728f,0.308510929f,0.0f,0.694178581f,-0.676848114f,0.244934693f,0.0f,-0.507630467f,0.733915746f,-0.641618311f,1.0f}},
    {"Bip001_L_Finger11","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger1/Bip001_L_Finger11",
        {0.716169059f,0.695730031f,-0.0553312488f,0.0f,0.0647125468f,0.0127429496f,0.997822285f,0.0f,0.694920182f,-0.718190312f,-0.0358963497f,0.0f,-0.332618415f,1.05297434f,0.00840981025f,1.0f}},
    {"Bip001_L_Finger12","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger1/Bip001_L_Finger11/Bip001_L_Finger12",
        {0.716168225f,0.695730984f,-0.0553319752f,0.0f,0.0647126734f,0.012743962f,0.997821867f,0.0f,0.694921196f,-0.718189538f,-0.0358957648f,0.0f,-0.310002744f,1.0529741f,0.00840887148f,1.0f}},
    {"Bip001_L_Finger3","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger3",
        {0.72819376f,0.673770368f,0.125568703f,0.0f,-0.119147971f,-0.0559733734f,0.991297305f,0.0f,0.674935937f,-0.736817241f,0.0395189039f,0.0f,-0.340323269f,1.06054354f,-0.0185395889f,1.0f}},
    {"Bip001_L_Finger4","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger4",
        {0.704664648f,0.649456143f,0.285752207f,0.0f,-0.200125739f,-0.204457477f,0.958199501f,0.0f,0.680733263f,-0.732395411f,-0.0141010638f,0.0f,-0.359993339f,1.04676533f,0.0942581743f,1.0f}},
    {"Bip001_L_Finger2","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger2",
        {0.725269616f,0.688346088f,0.0127910851f,0.0f,-0.0157085378f,-0.00202886248f,0.999874353f,0.0f,0.688285828f,-0.725379348f,0.00934143551f,0.0f,-0.3542431f,1.05838275f,-0.0234805942f,1.0f}},
    {"Bip001_L_Finger31","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger3/Bip001_L_Finger31",
        {0.715837955f,0.687619507f,0.121473558f,0.0f,-0.117172383f,-0.0532084554f,0.991684914f,0.0f,0.688365817f,-0.72411871f,0.0424813591f,0.0f,-0.32684201f,1.05462611f,-0.0234872624f,1.0f}},
    {"Bip001_L_Finger32","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger3/Bip001_L_Finger31/Bip001_L_Finger32",
        {0.715838075f,0.687619507f,0.121472634f,0.0f,-0.117172152f,-0.0532073826f,0.991684914f,0.0f,0.688365817f,-0.72411865f,0.0424819589f,0.0f,-0.301216662f,1.05462611f,-0.0234883148f,1.0f}},
    {"Bip001_L_Finger42","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger4/Bip001_L_Finger41/Bip001_L_Finger42",
        {0.686404586f,0.668926895f,0.285282284f,0.0f,-0.188932464f,-0.21478729f,0.958212256f,0.0f,0.702249646f,-0.711619735f,-0.0210489444f,0.0f,-0.346569717f,1.03591371f,0.100941032f,1.0f}},
    {"Bip001_L_Finger41","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/Bip001_L_Finger4/Bip001_L_Finger41",
        {0.686404645f,0.668926299f,0.28528282f,0.0f,-0.188932359f,-0.214788198f,0.958211899f,0.0f,0.702249467f,-0.711619556f,-0.0210498031f,0.0f,-0.362611234f,1.03591323f,0.10094218f,1.0f}},
    {"Bip001_R_Finger21","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger2/Bip001_R_Finger21",
        {-0.728012085f,-0.685444355f,0.0127909789f,0.0f,-0.0157164596f,-0.00196601916f,-0.999874413f,0.0f,0.685383797f,-0.728121519f,-0.00934147928f,0.0f,-0.315172911f,1.05970478f,0.0234793033f,1.0f}},
    {"Bip001_R_Finger22","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger2/Bip001_R_Finger21/Bip001_R_Finger22",
        {-0.728011608f,-0.685445428f,0.0127910124f,0.0f,-0.015716916f,-0.00196560286f,-0.999874532f,0.0f,0.685384929f,-0.728120983f,-0.00934210792f,0.0f,-0.288350224f,1.0597049f,0.0234798994f,1.0f}},
    {"Bip001_R_Hand","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand",
        {-0.67163676f,-0.740698636f,-0.0163927395f,0.0f,0.0102153085f,0.0128656086f,-0.999864638f,0.0f,0.740809739f,-0.671713293f,-0.00107455102f,0.0f,-0.504112124f,1.02292049f,0.031494718f,1.0f}},
    {"Bip001_R_Finger0","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger0",
        {-0.361808002f,-0.072216846f,-0.929450691f,0.0f,0.591821015f,0.752537429f,-0.288849622f,0.0f,0.720306754f,-0.654576659f,-0.229534805f,0.0f,-0.610123515f,0.714061499f,0.63052547f,1.0f}},
    {"Bip001_R_Finger01","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger0/Bip001_R_Finger01",
        {-0.388397098f,-0.0657248795f,-0.91914463f,0.0f,0.606023073f,0.733182132f,-0.308510482f,0.0f,0.694177926f,-0.676847517f,-0.24493511f,0.0f,-0.539420545f,0.733926356f,0.641686201f,1.0f}},
    {"Bip001_R_Finger1","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger1",
        {-0.725783944f,-0.685744524f,-0.054690592f,0.0f,0.0639801621f,0.0118686548f,-0.997880399f,0.0f,0.684940457f,-0.727744699f,0.0352599956f,0.0f,-0.351421833f,1.05753434f,-0.00809473544f,1.0f}},
    {"Bip001_R_Finger11","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger1/Bip001_R_Finger11",
        {-0.716168642f,-0.695729375f,-0.0553315431f,0.0f,0.0647126138f,0.0127432505f,-0.997822464f,0.0f,0.694919884f,-0.718189538f,0.035896197f,0.0f,-0.332573712f,1.0530293f,-0.00840646774f,1.0f}},
    {"Bip001_R_Finger12","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger1/Bip001_R_Finger11/Bip001_R_Finger12",
        {-0.716167748f,-0.695730448f,-0.0553321838f,0.0f,0.0647127852f,0.0127440635f,-0.997822464f,0.0f,0.694920838f,-0.718188822f,0.0358958095f,0.0f,-0.30995816f,1.0530293f,-0.00840576366f,1.0f}},
    {"Bip001_R_Finger3","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger3",
        {-0.728193343f,-0.673770189f,0.125568733f,0.0f,-0.119147949f,-0.0559733249f,-0.991297424f,0.0f,0.674935639f,-0.736817002f,-0.039518971f,0.0f,-0.340277553f,1.06059802f,0.0185304973f,1.0f}},
    {"Bip001_R_Finger4","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger4",
        {-0.704664409f,-0.649455905f,0.285751909f,0.0f,-0.20012565f,-0.20445697f,-0.95819962f,0.0f,0.680732906f,-0.732395172f,0.0141007695f,0.0f,-0.35994935f,1.04681802f,-0.0942787603f,1.0f}},
    {"Bip001_R_Finger2","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger2",
        {-0.725269437f,-0.68834573f,0.0127909472f,0.0f,-0.0157084707f,-0.00202872721f,-0.999874413f,0.0f,0.688285708f,-0.725378931f,-0.00934149139f,0.0f,-0.354197681f,1.05843806f,0.0234793257f,1.0f}},
    {"Bip001_R_Finger31","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger3/Bip001_R_Finger31",
        {-0.715837419f,-0.687618971f,0.121473439f,0.0f,-0.117172264f,-0.0532082915f,-0.991684914f,0.0f,0.6883654f,-0.724118173f,-0.0424814261f,0.0f,-0.326797426f,1.05468082f,0.0234785676f,1.0f}},
    {"Bip001_R_Finger32","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger3/Bip001_R_Finger31/Bip001_R_Finger32",
        {-0.715837419f,-0.68761903f,0.121472552f,0.0f,-0.117171973f,-0.0532073155f,-0.991684854f,0.0f,0.6883654f,-0.724117994f,-0.0424819365f,0.0f,-0.301172078f,1.05468082f,0.0234795138f,1.0f}},
    {"Bip001_R_Finger02","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger0/Bip001_R_Finger01/Bip001_R_Finger02",
        {-0.3883968f,-0.0657255203f,-0.919144511f,0.0f,0.606022894f,0.733182311f,-0.308510751f,0.0f,0.694178164f,-0.676847517f,-0.244934618f,0.0f,-0.507608712f,0.733926535f,0.641685724f,1.0f}},
    {"Bip001_R_Finger42","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger4/Bip001_R_Finger41/Bip001_R_Finger42",
        {-0.686403811f,-0.668926656f,0.285281837f,0.0f,-0.188932166f,-0.214786828f,-0.958212018f,0.0f,0.702248812f,-0.711619437f,0.0210487563f,0.0f,-0.34652704f,1.03596759f,-0.100961745f,1.0f}},
    {"Bip001_R_Finger41","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/Bip001_R_Finger4/Bip001_R_Finger41",
        {-0.68640399f,-0.668926299f,0.285282552f,0.0f,-0.188932061f,-0.214787915f,-0.958211899f,0.0f,0.702248931f,-0.711619437f,0.0210496951f,0.0f,-0.362568855f,1.03596771f,-0.100963011f,1.0f}},
    {"L_sleeve_04_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/L_sleeve_04_jnt",
        {0.719846427f,3.97267286e-06f,0.694133222f,0.0f,6.27405825e-06f,-0.999999583f,-7.79982656e-07f,0.0f,0.694133699f,4.91545507e-06f,-0.719845831f,0.0f,-0.457474351f,-0.0208749697f,1.04281044f,1.0f}},
    {"L_sleeve_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/L_sleeve_01_jnt",
        {0.719846308f,-0.694133341f,4.29572538e-06f,0.0f,6.28861017e-06f,3.33238859e-07f,-0.999999583f,0.0f,0.694133639f,0.71984607f,4.60468073e-06f,0.0f,-0.447212279f,-1.09558856f,0.0270274598f,1.0f}},
    {"L_sleeve_03_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/L_sleeve_03_jnt",
        {0.719846308f,0.694133341f,-4.27826308e-06f,0.0f,6.16928446e-06f,-2.33558239e-07f,0.999999702f,0.0f,0.694133639f,-0.71984601f,-4.44952093e-06f,0.0f,-0.452281952f,0.990837276f,-0.0238566138f,1.0f}},
    {"L_sleeve_02_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_L_Clavicle/Bip001_L_UpperArm/Bip001_L_Forearm/Bip001_L_Hand/L_sleeve_02_jnt",
        {0.719846427f,-4.36702976e-06f,-0.694133222f,0.0f,6.21148502e-06f,0.999999583f,1.48429535e-07f,0.0f,0.69413358f,-4.41914381e-06f,0.71984607f,0.0f,-0.454969168f,-0.0679476932f,-1.03079104f,1.0f}},
    {"R_sleeve_02_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/R_sleeve_02_jnt",
        {-0.71984601f,-3.46626621e-06f,-0.694133461f,0.0f,2.17551133e-07f,0.999999225f,-5.22340997e-06f,0.0f,0.694133461f,-3.91110007e-06f,-0.719845951f,0.0f,-0.454940319f,-0.0679484829f,1.03084433f,1.0f}},
    {"R_sleeve_03_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/R_sleeve_03_jnt",
        {-0.719846487f,-0.694132745f,4.26225597e-06f,0.0f,1.50612323e-07f,-6.29443093e-06f,-0.999999762f,0.0f,0.694133103f,-0.71984601f,4.63523975e-06f,0.0f,-0.452249944f,0.99088794f,0.0238564126f,1.0f}},
    {"R_sleeve_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/R_sleeve_01_jnt",
        {-0.71984601f,0.694132864f,-4.09636414e-06f,0.0f,-6.25732355e-08f,5.83822839e-06f,0.999999583f,0.0f,0.694133043f,0.719845712f,-4.15966497e-06f,0.0f,-0.447183073f,-1.09564209f,-0.0270281062f,1.0f}},
    {"R_sleeve_04_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_R_Forearm/Bip001_R_Hand/R_sleeve_04_jnt",
        {-0.719846606f,-0.10858281f,0.685587585f,0.0f,-1.17870513e-07f,-0.987688363f,-0.156429514f,0.0f,0.694132924f,-0.112605304f,0.710984468f,0.0f,-0.457443953f,0.14252381f,-1.03328967f,1.0f}},
    {"R_collarB_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/R_collarB_jnt",
        {-0.110218734f,-0.0840976164f,0.990342617f,0.0f,1.82910517e-05f,0.996413708f,0.0846151039f,0.0f,-0.993907094f,0.00934429374f,-0.109821908f,0.0f,1.47421753f,-0.0230402909f,0.0641819835f,1.0f}},
    {"M_collarC_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/M_collarC_jnt",
        {1.53780147e-05f,-0.999999881f,4.21343339e-06f,0.0f,-0.256405622f,1.2956987e-07f,0.966569901f,0.0f,-0.966570079f,-1.59442443e-05f,-0.256405562f,0.0f,1.43931127f,2.33235005e-05f,0.301113695f,1.0f}},
    {"L_collarB_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/L_collarB_jnt",
        {0.162793756f,-0.0859938413f,-0.982905388f,0.0f,3.33240387e-05f,-0.996193111f,0.0871620402f,0.0f,-0.986660361f,-0.0142221777f,-0.16217123f,0.0f,1.46975923f,0.0322796442f,0.150385246f,1.0f}},
    {"R_breast_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/R_breast_01_jnt",
        {-0.506137192f,-8.5408501e-05f,0.862452865f,0.0f,0.862453043f,4.33651303e-06f,0.506137013f,0.0f,-4.69684601e-05f,0.999999762f,7.14659473e-05f,0.0f,0.0863033906f,-1.31626987f,0.00906041823f,1.0f}},
    {"R_vestA_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/R_vestA_01_jnt",
        {-8.46385956e-05f,0.939914107f,0.341410071f,0.0f,4.20212609e-06f,-0.341410011f,0.939914227f,0.0f,1.0f,8.09876583e-05f,2.49468249e-05f,0.0f,-1.27865291f,-0.144200087f,0.0443029366f,1.0f}},
    {"L_vestA_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/L_vestA_01_jnt",
        {-8.29697019e-05f,-0.986179829f,0.165678605f,0.0f,2.86102204e-06f,-0.165678591f,-0.986179709f,0.0f,1.00000024f,-8.13489969e-05f,1.65677811e-05f,0.0f,-1.278651f,-0.133510843f,-0.0697153881f,1.0f}},
    {"L_breast_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/L_breast_01_jnt",
        {0.506128669f,-8.53119927e-05f,0.862458169f,0.0f,0.862458229f,4.65946187e-06f,-0.50612849f,0.0f,3.91602553e-05f,0.999999762f,7.59363102e-05f,0.0f,0.0861531869f,-1.31627178f,-0.0093170926f,1.0f}},
    {"Bip001_RUpArmTwist","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_RUpArmTwist",
        {-0.670111239f,0.0438431874f,-0.74096477f,0.0f,-0.0845997632f,0.987237155f,0.134925306f,0.0f,0.737423778f,0.153100356f,-0.657849729f,0.0f,-0.961090446f,-0.228140861f,1.00245512f,1.0f}},
    {"L_vestB_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/L_vestB_01_jnt",
        {-8.22544243e-05f,-0.936255813f,-0.351318866f,0.0f,6.43730482e-06f,0.351318866f,-0.936255693f,0.0f,1.00000012f,-7.92727005e-05f,-2.2870563e-05f,0.0f,-1.26668036f,-0.103165269f,-0.0511360057f,1.0f}},
    {"R_vestC_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/R_vestC_01_jnt",
        {-8.53538659e-05f,8.00460039e-05f,1.0f,0.0f,4.58128625e-06f,-0.999999762f,8.00463968e-05f,0.0f,1.00000012f,4.58811655e-06f,8.53534802e-05f,0.0f,-1.27579033f,-0.158973247f,-0.050136786f,1.0f}},
    {"Bip001_RUpArmTwist1","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm/Bip001_RUpArmTwist/Bip001_RUpArmTwist1",
        {-0.670111358f,-0.00204555644f,-0.742257535f,0.0f,-0.0845997557f,0.993689716f,0.0736382455f,0.0f,0.737423778f,0.112140611f,-0.666055799f,0.0f,-0.858006954f,-0.165734828f,1.01464057f,1.0f}},
    {"R_vestB_01_jnt","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/R_vestB_01_jnt",
        {-8.57114937e-05f,0.933488667f,-0.358606547f,0.0f,4.2319316e-06f,0.358606458f,0.933488667f,0.0f,1.00000012f,7.84930817e-05f,-3.46871493e-05f,0.0f,-1.26667738f,-0.103039488f,0.0520402305f,1.0f}},
    {"Bip001_R_UpperArm","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_R_Clavicle/Bip001_R_UpperArm",
        {-0.670111179f,-0.0479264967f,-0.740711689f,0.0f,-0.0845997483f,0.996341467f,0.0120695531f,0.0f,0.737423599f,0.0707519501f,-0.671714365f,0.0f,-0.961090207f,-0.102694869f,1.022946f,1.0f}},
    {"Bip001_Spine1","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1",
        {1.28320898e-09f,-1.22033716e-08f,-1.00000024f,0.0f,0.0f,-0.999999881f,1.22033716e-08f,0.0f,-1.00000012f,-2.22044605e-14f,-1.28320887e-09f,0.0f,1.17550874f,-0.0307348538f,4.19041353e-05f,1.0f}},
    {"Bip001_Spine","Root/Bip001/Bip001_Pelvis/Bip001_Spine",
        {-4.26769293e-05f,-3.39672255e-07f,-1.0f,0.0f,1.14803527e-06f,-0.999999881f,3.39623227e-07f,0.0f,-1.0f,-1.14802071e-06f,4.26769293e-05f,0.0f,1.09681451f,-0.0307961479f,-8.25312964e-06f,1.0f}},
    {"Bip001_Neck","Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_Neck",
        {8.30888748e-05f,1.33801141e-05f,-1.0f,0.0f,0.210750446f,-0.977539718f,4.4314188e-06f,0.0f,-0.977539897f,-0.210750401f,-8.40425491e-05f,0.0f,1.42232132f,0.316603422f,0.000146722567f,1.0f}},
}};
inline constexpr std::array<uint32_t,kBoneCount> kLegacyToCurrent{{
    0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,
    16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,
    32,33,34,35,36,38,39,40,41,42,43,44,37,45,46,47,
    48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,
    65,64,66,67,68,69,70,71,72,73,74,75,76,77,78,79,
    80,81,82,83,84,85,86,
}};
inline bool IsLegacyTarget(std::string_view character,std::string_view resource,
    uint32_t component_id,std::string_view mesh_name,uint64_t expected_indices,bool explicit_resource) {
    return !explicit_resource && character=="chr_0014_aurora" && component_id==1 &&
        (resource=="chr_0014_aurora_postmodel" || resource=="chr_0014_aurora_uimodel") &&
        mesh_name==kMeshName && expected_indices==kLegacyIndices;
}
inline bool ValidateDonor(std::string_view mesh_name,std::string_view receiver_path,
    uint64_t indices,uint32_t vertices,std::span<const BoneObservation> bones) {
    if (mesh_name!=kMeshName || receiver_path!=kReceiverPath || indices!=kCurrentIndices ||
        vertices!=kCurrentVertices || bones.size()!=kBoneCount) return false;
    for (size_t i=0;i<kBoneCount;++i) {
        if (bones[i].name!=kCurrentBones[i].name || bones[i].path!=kCurrentBones[i].path) return false;
        for (size_t n=0;n<16;++n)
            if (!std::isfinite(bones[i].bindpose[n]) ||
                std::abs(bones[i].bindpose[n]-kCurrentBones[i].bindpose[n])>kBindposeTolerance) return false;
    }
    return true;
}
inline std::optional<uint32_t> RemapBone(uint32_t legacy_index,std::string_view legacy_name) {
    if (legacy_index>=kBoneCount) return std::nullopt;
    const auto current=kLegacyToCurrent[legacy_index];
    if (legacy_name!=kCurrentBones[current].name) return std::nullopt;
    return current;
}
}
