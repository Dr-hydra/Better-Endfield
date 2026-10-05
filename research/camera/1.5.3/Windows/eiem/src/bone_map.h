#pragma once
#include <cstring>
#include <string>

enum HumanBone {
  HB_Hips = 0,
  HB_LeftUpperLeg = 1,    HB_RightUpperLeg = 2,
  HB_LeftLowerLeg = 3,    HB_RightLowerLeg = 4,
  HB_LeftFoot = 5,        HB_RightFoot = 6,
  HB_Spine = 7,
  HB_Chest = 8,
  HB_Neck = 9,
  HB_Head = 10,
  HB_LeftShoulder = 11,   HB_RightShoulder = 12,
  HB_LeftUpperArm = 13,   HB_RightUpperArm = 14,
  HB_LeftLowerArm = 15,   HB_RightLowerArm = 16,
  HB_LeftHand = 17,       HB_RightHand = 18,
  HB_LeftToes = 19,       HB_RightToes = 20,
  HB_LeftIndexProximal = 27,
  HB_LeftMiddleProximal = 30,
  HB_LeftLittleProximal = 36,
  HB_RightIndexProximal = 42,
  HB_RightMiddleProximal = 45,
  HB_RightLittleProximal = 51,
  HB_UpperChest = 54,
  HB_None = -1,
};

struct BoneMapEntry {
  const char *mmdName;
  int humanBone;
  bool isPositionBone;
};

static const BoneMapEntry g_boneMap[] = {
  {"\xe3\x82\xbb\xe3\x83\xb3\xe3\x82\xbf\xe3\x83\xbc",         HB_Hips,          true},
  {"\xe4\xb8\x8a\xe5\x8d\x8a\xe8\xba\xab",                       HB_Spine,         false},
  {"\xe4\xb8\x8a\xe5\x8d\x8a\xe8\xba\xab\x32",                   HB_Chest,         false},
  {"\xe9\xa6\x96",                                                 HB_Neck,          false},
  {"\xe9\xa0\xad",                                                 HB_Head,          false},

  {"\xe5\xb7\xa6\xe8\x82\xa9",                                     HB_LeftShoulder,  false},
  {"\xe5\xb7\xa6\xe8\x85\x95",                                     HB_LeftUpperArm,  false},
  {"\xe5\xb7\xa6\xe3\x81\xb2\xe3\x81\x98",                         HB_LeftLowerArm,  false},
  {"\xe5\xb7\xa6\xe6\x89\x8b\xe9\xa6\x96",                         HB_LeftHand,      false},

  {"\xe5\x8f\xb3\xe8\x82\xa9",                                     HB_RightShoulder, false},
  {"\xe5\x8f\xb3\xe8\x85\x95",                                     HB_RightUpperArm, false},
  {"\xe5\x8f\xb3\xe3\x81\xb2\xe3\x81\x98",                         HB_RightLowerArm, false},
  {"\xe5\x8f\xb3\xe6\x89\x8b\xe9\xa6\x96",                         HB_RightHand,     false},

  {"\xe5\xb7\xa6\xe8\xb6\xb3",                                     HB_LeftUpperLeg,  false},
  {"\xe5\xb7\xa6\xe3\x81\xb2\xe3\x81\x96",                         HB_LeftLowerLeg,  false},
  {"\xe5\xb7\xa6\xe8\xb6\xb3\xe9\xa6\x96",                         HB_LeftFoot,      false},
  {"\xe5\xb7\xa6\xe3\x81\xa4\xe3\x81\xbe\xe5\x85\x88",             HB_LeftToes,      false},

  {"\xe5\x8f\xb3\xe8\xb6\xb3",                                     HB_RightUpperLeg, false},
  {"\xe5\x8f\xb3\xe3\x81\xb2\xe3\x81\x96",                         HB_RightLowerLeg, false},
  {"\xe5\x8f\xb3\xe8\xb6\xb3\xe9\xa6\x96",                         HB_RightFoot,     false},
  {"\xe5\x8f\xb3\xe3\x81\xa4\xe3\x81\xbe\xe5\x85\x88",             HB_RightToes,     false},

  {"\xe4\xb8\x8a\xe5\x8d\x8a\xe8\xba\xab\x33",                   HB_UpperChest,    false},
};

static const int g_boneMapCount = sizeof(g_boneMap) / sizeof(g_boneMap[0]);

struct FingerMapEntry {
  const char *mmdName;
  const char *transformName;
};

static const FingerMapEntry g_fingerMap[] = {
  {"\xe5\x8f\xb3\xe8\xa6\xaa\xe6\x8c\x87\xef\xbc\x90", "Bip001_R_Finger0"},
  {"\xe5\x8f\xb3\xe8\xa6\xaa\xe6\x8c\x87\xef\xbc\x91", "Bip001_R_Finger01"},
  {"\xe5\x8f\xb3\xe8\xa6\xaa\xe6\x8c\x87\xef\xbc\x92", "Bip001_R_Finger02"},
  {"\xe5\x8f\xb3\xe4\xba\xba\xe6\x8c\x87\xef\xbc\x91", "Bip001_R_Finger1"},
  {"\xe5\x8f\xb3\xe4\xba\xba\xe6\x8c\x87\xef\xbc\x92", "Bip001_R_Finger11"},
  {"\xe5\x8f\xb3\xe4\xba\xba\xe6\x8c\x87\xef\xbc\x93", "Bip001_R_Finger12"},
  {"\xe5\x8f\xb3\xe4\xb8\xad\xe6\x8c\x87\xef\xbc\x91", "Bip001_R_Finger2"},
  {"\xe5\x8f\xb3\xe4\xb8\xad\xe6\x8c\x87\xef\xbc\x92", "Bip001_R_Finger21"},
  {"\xe5\x8f\xb3\xe4\xb8\xad\xe6\x8c\x87\xef\xbc\x93", "Bip001_R_Finger22"},
  {"\xe5\x8f\xb3\xe8\x96\xac\xe6\x8c\x87\xef\xbc\x91", "Bip001_R_Finger3"},
  {"\xe5\x8f\xb3\xe8\x96\xac\xe6\x8c\x87\xef\xbc\x92", "Bip001_R_Finger31"},
  {"\xe5\x8f\xb3\xe8\x96\xac\xe6\x8c\x87\xef\xbc\x93", "Bip001_R_Finger32"},
  {"\xe5\x8f\xb3\xe5\xb0\x8f\xe6\x8c\x87\xef\xbc\x91", "Bip001_R_Finger4"},
  {"\xe5\x8f\xb3\xe5\xb0\x8f\xe6\x8c\x87\xef\xbc\x92", "Bip001_R_Finger41"},
  {"\xe5\x8f\xb3\xe5\xb0\x8f\xe6\x8c\x87\xef\xbc\x93", "Bip001_R_Finger42"},

  {"\xe5\xb7\xa6\xe8\xa6\xaa\xe6\x8c\x87\xef\xbc\x90", "Bip001_L_Finger0"},
  {"\xe5\xb7\xa6\xe8\xa6\xaa\xe6\x8c\x87\xef\xbc\x91", "Bip001_L_Finger01"},
  {"\xe5\xb7\xa6\xe8\xa6\xaa\xe6\x8c\x87\xef\xbc\x92", "Bip001_L_Finger02"},
  {"\xe5\xb7\xa6\xe4\xba\xba\xe6\x8c\x87\xef\xbc\x91", "Bip001_L_Finger1"},
  {"\xe5\xb7\xa6\xe4\xba\xba\xe6\x8c\x87\xef\xbc\x92", "Bip001_L_Finger11"},
  {"\xe5\xb7\xa6\xe4\xba\xba\xe6\x8c\x87\xef\xbc\x93", "Bip001_L_Finger12"},
  {"\xe5\xb7\xa6\xe4\xb8\xad\xe6\x8c\x87\xef\xbc\x91", "Bip001_L_Finger2"},
  {"\xe5\xb7\xa6\xe4\xb8\xad\xe6\x8c\x87\xef\xbc\x92", "Bip001_L_Finger21"},
  {"\xe5\xb7\xa6\xe4\xb8\xad\xe6\x8c\x87\xef\xbc\x93", "Bip001_L_Finger22"},
  {"\xe5\xb7\xa6\xe8\x96\xac\xe6\x8c\x87\xef\xbc\x91", "Bip001_L_Finger3"},
  {"\xe5\xb7\xa6\xe8\x96\xac\xe6\x8c\x87\xef\xbc\x92", "Bip001_L_Finger31"},
  {"\xe5\xb7\xa6\xe8\x96\xac\xe6\x8c\x87\xef\xbc\x93", "Bip001_L_Finger32"},
  {"\xe5\xb7\xa6\xe5\xb0\x8f\xe6\x8c\x87\xef\xbc\x91", "Bip001_L_Finger4"},
  {"\xe5\xb7\xa6\xe5\xb0\x8f\xe6\x8c\x87\xef\xbc\x92", "Bip001_L_Finger41"},
  {"\xe5\xb7\xa6\xe5\xb0\x8f\xe6\x8c\x87\xef\xbc\x93", "Bip001_L_Finger42"},

  {"\xe5\xb7\xa6\xe7\x9b\xae",     "eyeLfJoint"},
  {"\xe5\x8f\xb3\xe7\x9b\xae",     "eyeRtJoint"},

  {"\xe3\x81\x82\xe3\x81\x94", "jawJoint"},

  {"\xe4\xb8\x8b\xe5\x8d\x8a\xe8\xba\xab", "Bip001_Pelvis"},

  {"\xe5\x85\xa8\xe3\x81\xa6\xe3\x81\xae\xe8\xa6\xaa", "Bip001"},
};

static const int g_fingerMapCount = sizeof(g_fingerMap) / sizeof(g_fingerMap[0]);

struct ResolvedBoneMapping {
  std::string mmdName;
  int humanBone;
  std::string transformName;
  void *transform;
  bool isPositionBone;
  bool isFingerBone;
  bool valid;

  float bindRot[4];
  float bindPos[3];
  bool hasBind;

  Quat mmdStance;
  Quat endfieldStance;
  bool hasStance;

  ResolvedBoneMapping()
    : humanBone(HB_None), transform(nullptr), isPositionBone(false),
      isFingerBone(false), valid(false), hasBind(false), hasStance(false) {
    bindRot[0] = 0; bindRot[1] = 0; bindRot[2] = 0; bindRot[3] = 1;
    bindPos[0] = 0; bindPos[1] = 0; bindPos[2] = 0;
    mmdStance = {0,0,0,1};
    endfieldStance = {0,0,0,1};
  }
};

static int LookupBoneMapping(const std::string &mmdBoneName) {
  for (int i = 0; i < g_boneMapCount; i++) {
    if (mmdBoneName == g_boneMap[i].mmdName) {
      return g_boneMap[i].humanBone;
    }
  }
  return HB_None;
}

static bool IsBonePositionMapped(const std::string &mmdBoneName) {
  for (int i = 0; i < g_boneMapCount; i++) {
    if (mmdBoneName == g_boneMap[i].mmdName) {
      return g_boneMap[i].isPositionBone;
    }
  }
  return false;
}

static const char *LookupFingerMapping(const std::string &mmdBoneName) {
  for (int i = 0; i < g_fingerMapCount; i++) {
    if (mmdBoneName == g_fingerMap[i].mmdName) {
      return g_fingerMap[i].transformName;
    }
  }
  return nullptr;
}
