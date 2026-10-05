#pragma once

// SkeletalMorph（SMC）游戏原生表情驱动。
//
// 参照 {EIEM}/src/smc_face.h + globals.h + init.h（Sasye/EIEM, AGPL-3.0）
// 精简移植：hook 游戏的 SkeletalMorphCore.Update / DoEvaluateMorphToBoneJob，
// 动态解析 SMC 字段偏移，从 NativeHashMap + AvatarData.morphMappingNames 把
// morph 名映射到大列表（morph→骨骼增量）区间，再把面板滑条权重换算成
// 骨骼局部位姿增量，在 SMC.Update 之后覆盖写回。经典 BlendShape 路径见 morph.h。
//
// 单一 TU（poser.cpp）设计：全部 static 全局在该 TU 内共享。
// 版本敏感：偏移优先动态解析，失败走 SafeOff 回退（见下方各 0x?? 常量）。

#include <cmath>
#include <cstdio>
#include <cstring>
#include <cstdint>
#include <map>
#include <functional>

#include "core/base.h"
#include "core/il2cpp_api.h"
#include "core/game_hooks.h"
#include "core/user_agreement.h"
#include "math/character_face.h"
#include "math/mmd_face_controls.h"
#include "game/smc_automation.h"
#include "game/eye_gaze.h"

// ---- 常量 ----
#define SMC_MAX_BIGLIST 8192
#define SMC_MAX_FACE_BONES 256
#define SMC_BONE_MAP_SIZE 512
#define SMC_NUM_MOUTH 5
#define SMC_MAX_EXTRA_TARGETS 4

// morph→骨骼增量条目（游戏 SkeletalMorphCore 大列表元素，packed 44 字节）
#pragma pack(push, 1)
struct SMCMorphBoneEntry {
  int32_t boneNameHash;
  int32_t boneID;
  float deltaPosX, deltaPosY, deltaPosZ;
  float deltaRotX, deltaRotY, deltaRotZ;
  float pad[3];
};
#pragma pack(pop)
static_assert(sizeof(SMCMorphBoneEntry) == 44,
              "SMCMorphBoneEntry must be 44 bytes");

// SMC 维护的面部骨骼快照（局部位姿 + transform 引用）
struct SMCFaceBone {
  float px, py, pz;
  float rx, ry, rz, rw;
  void *transform;
};

struct SMCMouthShape {
  const char *name;
  int nameHash;
  int morphId;
  int startIdx;
  int count;
  int jobStartIdx;
  int jobCount;
  bool resolved;
};

struct SMCExtraTarget {
  const char *endfieldName;
  int nameHash;
  int startIdx;
  int count;
  int jobStartIdx;
  int jobCount;
  bool resolved;
  int morphId; // 解析时记录：用于把游戏当前的 morph 权重读回滑条
};

struct SMCExtraMorph {
  const char *vmdNameUtf8;
  const char *label;
  SMCExtraTarget targets[SMC_MAX_EXTRA_TARGETS];
  int targetCount;
  float weight;
  float prevWeight;
};

// ---- SMC 字段偏移（-1=未解析，读时走 SafeOff 回退）----
static int s_offAllMorphs = -1;
static int s_offBigList = -1; // m_morphNameHashToMorphDataBoneBigList
static int s_offNativeHashMap = -1;
static int s_offMorphBSDirty = -1;
static int s_offAllMorphBoneDirty = -1;
static int s_offAvatarData = -1;
static int s_offAllBonesTransforms = -1;
static int s_offBoneIDToIdx = -1;
static int s_offMorphMappingNames = -1; // AvatarData 上
static int s_offSmcEyeLookAt = -1;
static int s_offPhonemeWeights = -1;    // m_phonemesWeights（口型权重）
static int s_offMicroExprWeights = -1;  // m_microExpressionWeights（微表情权重）
static int s_offMainEmotion = -1;       // m_mainEmotion（当前表情 Pose）

#include "game/smc_actor_state.h"

// ---- 读回游戏当前表情状态（输出到面板滑条）----



 // m_allMorphs 元素内的 float 权重偏移


// ---- 运行时状态 ----
static void *s_smcClass = nullptr;


// Optional read-only scene query on the game's update thread. Set/clear and
// invocation all use g_poseMutex; never call from the independent frame worker.

 // 面板启用 SMC 表情驱动











 // 本帧是否成功按权重重算过 s_faceBones





static void SMCFaceSelectProfile(std::shared_ptr<const character_face::Profile> profile,const std::string &model);






static void SMCFaceInvalidate() {
  poser_gaze::Reset(true,SMCGazeContext());
  s_manualFace={};
  s_faceNodes.clear();s_characterBinding={};s_characterProfile.reset();s_characterModel.clear();s_characterBindingGeneration=0;
  s_faceHierarchy={};s_faceBindingRevision=-1;++s_faceGeneration;
  s_faceRegions.fill(-1);
  s_faceBoneEvalOk=false;
}


// 中性脸基线（等价于身体的 A-pose 基准）：
// 不能拿"锁定时游戏正在演的那张脸"当默认值——那样滑条就变成在表情上叠表情。
// 做法：让 job hook 把 morph 增量整表清零若干帧，游戏会把骨骼写回中性位姿，
// 这时读到的才是真正的默认值；之后所有表情都是"默认值 + 权重×增量"。



// 驱动用的基准位姿。默认 = 中性脸；冻结时改成"当前脸 − 拟合出的权重增量"，
// 这样冻结瞬间脸完全不变（表情被冻住），而滑条仍表示"相对默认值"的权重。












// 口型（A/I/U/E/O）+ 表情表（名称/哈希来自 EIEM 逆向，版本敏感）





static const int s_extraMorphCount =
    19;
static_assert(SMC_NUM_MOUTH + s_extraMorphCount <= SMC_MAX_SLIDERS,
              "SMC weight buffers must cover every declared expression");


// ---- hook 类型 ----
typedef void(__fastcall *SMCSMCUpdate_t)(void *, float, void *);
// Native x64 ABI for a 16-byte job value returned by MorphToBoneJob(uint, job).
// The 16-byte return value adds a hidden result pointer before the instance;
// the callee must return that same pointer in RAX. MethodInfo is argument five.
// Keep the native result/dependency storage opaque and forward it unchanged.
typedef void *(__fastcall *SMCMorphJob_t)(void *result, void *smc, uint32_t count,
                                         void *dependency, void *methodInfo);
static SMCSMCUpdate_t s_origSMCUpdate = nullptr;
static SMCMorphJob_t s_origMorphJob = nullptr;

// ---- 前向声明 ----
static void ResolveSMCOffsets(void *cls);
static void ResolveSMCMouthShapes(void *smcBase);
static void SMCCaptureBigList(void *smcBase);
static void SMCRestoreBigList();
static void SMCWriteTouchedBones();

// ---- SMC 实例归属判定 ----
// 摄影模式里场上每个角色都有自己的 SkeletalMorphCore，而且都会调用
// DoEvaluateMorphToBoneJob。"谁先跑谁被锁"会锁到别的角色：滑条算出来的面部增量
// 写到别人的骨头上，当前编辑的角色就"怎么拖都没反应"。这里用两重判据确认实例
// 属于当前编辑角色：①面部骨指针与当前角色骨骼列表有交集；②面部骨挂在当前角色
// 根 Transform 下。判不出（-1）按"属于"处理，避免误伤能用的角色。
// Bone snapshots come from the captured rig. Job-side matching uses only cached
// pointers, never Unity calls; an unrelated face is never a fallback candidate.
static bool SMCIsRejected(void *smc) {
  for (int i = 0; i < s_smcRejectedCount; i++)
    if (s_smcRejected[i] == smc)
      return true;
  return false;
}
static void SMCRememberRejected(void *smc) {
  if (!smc || SMCIsRejected(smc)) return;
  if (s_smcRejectedCount < SMC_MAX_REJECTED)
    s_smcRejected[s_smcRejectedCount++] = smc;
}

// 读 SMC 的 m_allBonesTransforms（面部骨 Transform 数组），纯裸内存读，SEH 兜底
static bool SMCReadBoneArrayRaw(void *smc, void ***outBones, int *outLen) {
  *outBones = nullptr;
  *outLen = 0;
  if (!smc)
    return false;
  __try {
    int btOff =
        SafeOff(s_offAllBonesTransforms, 0x60, "smc.allBonesTransforms");
    void *arr = *(void **)((char *)smc + btOff);
    if (!arr)
      return false;
    int n = *(int *)((char *)arr + IL2CPP_ARRAY_LEN);
    if (n <= 0 || n > SMC_MAX_FACE_BONES)
      return false;
    *outBones = (void **)((char *)arr + IL2CPP_ARRAY_DATA);
    *outLen = n;
    return true;
  } __except (1) {
    return false;
  }
}

static void SMCRefreshCharBoneSnapshot() {
  if (s_charBoneXformRev == SMCRevision())
    return;
  s_charBoneXformCount = 0;
  for (size_t i = 0;
       i < SMCBones().size() && s_charBoneXformCount < SMC_SNAPSHOT_MAX; i++) {
    if (SMCBones()[i].transform)
      s_charBoneXforms[s_charBoneXformCount++] = SMCBones()[i].transform;
  }
  s_charBoneXformRev = SMCRevision();
  s_smcRejectedCount = 0;
  s_smcRejectStrikes = 0;
  Log("[SMC] char bone snapshot: %d transforms (rev=%d)",
      s_charBoneXformCount, SMCRevision());
}
static int SMCBoneOverlapRaw(void *smc) {
  __try {
    void **bones=nullptr;int count=0;
    if(!SMCReadBoneArrayRaw(smc,&bones,&count)||!s_charBoneXformCount)return -1;
    int hits=0;
    for(int i=0;i<count;++i)if(bones[i])
      for(int j=0;j<s_charBoneXformCount;++j)if(bones[i]==s_charBoneXforms[j]){++hits;break;}
    return hits;
  } __except(1) {return -1;}
}

// 1=属于当前角色 0=不属于 -1=判不出
static int SMCOwnershipTest(void *smc, char *detail, int detailSz) {
  detail[0] = 0;
  void **bones = nullptr;
  int len = 0;
  if (!SMCReadBoneArrayRaw(smc, &bones, &len)) {
    snprintf(detail, detailSz, "no face bone array");
    return -1;
  }
  SMCRefreshCharBoneSnapshot();
  int hits = 0;
  int firstIdx = -1;
  for (int i = 0; i < len; i++) {
    if (!bones[i])
      continue;
    if (firstIdx < 0)
      firstIdx = i;
    for (int j = 0; j < s_charBoneXformCount; j++) {
      if (s_charBoneXforms[j] == bones[i]) {
        hits++;
        break;
      }
    }
  }
  bool underRoot = false;
  if (firstIdx >= 0 && g_transform_get_parent) {
    void *root = SMCRoot();
    if (root) {
      __try {
        void *cur = bones[firstIdx];
        for (int d = 0; d < 16 && cur; d++) {
          if (cur == root) {
            underRoot = true;
            break;
          }
          cur = Invoke(g_transform_get_parent, cur);
        }
      } __except (1) {
      }
    }
  }
  if (hits > 0) {
    snprintf(detail, detailSz, "bone overlap %d/%d", hits, len);
    return 1;
  }
  if (underRoot) {
    snprintf(detail, detailSz,
             "face bone under char root (overlap 0/%d)", len);
    return 1;
  }
  if (s_charBoneXformCount < 100) {
    snprintf(detail, detailSz, "inconclusive: char bone list=%d",
             s_charBoneXformCount);
    return -1;
  }
  snprintf(detail, detailSz,
           "no bone overlap 0/%d, not under char root (char bones=%d)", len,
           s_charBoneXformCount);
  return 0;
}

// 取该实例第一根面部骨的名字——比组件所在 GO 名更能说明"这是谁的脸"
static void SMCFirstBoneName(void *smc, char *buf, int sz) {
  buf[0] = 0;
  void **bones = nullptr;
  int len = 0;
  if (!SMCReadBoneArrayRaw(smc, &bones, &len))
    return;
  for (int i = 0; i < len; i++) {
    if (!bones[i])
      continue;
    GetBoneName(bones[i], buf, sz);
    return;
  }
}

// ---- 读回游戏当前表情状态（输出到面板滑条）----
// 游戏把每个 morph 的当前权重放在 SkeletalMorphCore 的 m_allMorphs（按 morphId 索引，
// morphId 就是 hashmap 里的 vi[0]）。这里一次性探明元素结构（float[] 或对象数组 +
// 其中的 float 权重字段），之后按 morphId 取值，把"当前脸上的表情"填进滑条。
// 字段名 + 声明类型（只调反射，不解引用，绝对不会崩）
static void SMCDumpFieldsTyped(void *klass, const char *label) {
  if (!klass || !il2cpp_class_get_fields)
    return;
  const char *cn =
      il2cpp_class_get_name ? il2cpp_class_get_name(klass) : "?";
  Log("[SMC] fields of %s (%s):", cn ? cn : "?", label);
  void *it = nullptr, *f;
  int n = 0;
  while ((f = il2cpp_class_get_fields(klass, &it)) && n < 80) {
    const char *fn = il2cpp_field_get_name ? il2cpp_field_get_name(f) : "?";
    int fo = (int)il2cpp_field_get_offset(f);
    const char *ftn = "?";
    if (il2cpp_field_get_type && il2cpp_type_get_name) {
      void *ft = il2cpp_field_get_type(f);
      if (ft)
        ftn = il2cpp_type_get_name(ft);
    }
    Log("[SMC]   [0x%X] %s : %s", fo, fn ? fn : "?", ftn ? ftn : "?");
    n++;
  }
}

// 一个字段可能是 managed 数组/List（指针指向对象），也可能是内联 native buffer
// （ptr,len,cap 三连）。两种解释分别兜底，任何一种崩掉都不影响另一种和后续步骤。
static void SMCDumpBufferCandidates(void *smc, int off, const char *label) {
  if (!smc || off < 0) {
    Log("[SMC] %s: offset unresolved", label);
    return;
  }
  void *p = nullptr;
  int inlineLen = 0;
  __try {
    p = *(void **)((char *)smc + off);
    inlineLen = *(int *)((char *)smc + off + 8);
    Log("[SMC] %s off=0x%X ptr=%p inline(len=%d cap=%d)", label, off, p,
        inlineLen, *(int *)((char *)smc + off + 12));
  } __except (1) {
    Log("[SMC] %s: inline read failed", label);
    return;
  }
  if (!p)
    return;
  // 解释 1：native 缓冲（buf 就是数据）
  if (inlineLen > 0 && inlineLen <= 64) {
    __try {
      float *fl = (float *)p;
      char line[192] = {};
      int pos = 0;
      for (int i = 0; i < inlineLen && i < 14 && pos < 176; i++)
        pos += snprintf(line + pos, sizeof(line) - pos, "%.3f ", fl[i]);
      Log("[SMC] %s native floats: %s", label, line);
    } __except (1) {
      Log("[SMC] %s: native float read failed", label);
    }
  }
  // 解释 2：managed 数组/List（ptr 指向 IL2CPP 对象）
  __try {
    void *cls =
        il2cpp_object_get_class ? il2cpp_object_get_class(p) : nullptr;
    if (!cls) {
      Log("[SMC] %s managed: no class", label);
      return;
    }
    const char *cn = il2cpp_class_get_name ? il2cpp_class_get_name(cls) : "?";
    int len = *(int *)((char *)p + IL2CPP_ARRAY_LEN);
    Log("[SMC] %s managed: class=%s len=%d", label, cn ? cn : "?", len);
  } __except (1) {
    Log("[SMC] %s: not a managed object (native buffer?)", label);
  }
}

// MainEmotion._pose → Pose 里的 MorphCtrlValue 列表（_ctrlName + _value），
// 这是"角色当前表情"最直接的来源：带名字，能直接对上滑条目标。
static void SMCProbeEmotionPose(void *smc) {
  if (!smc)
    return;
  void *mainEmo = nullptr;
  int meOff = -1;
  __try {
    meOff =
        SafeOff(s_offMainEmotion, 0x3C0, "smc.mainEmotion");
    mainEmo = *(void **)((char *)smc + meOff);
  } __except (1) {
    Log("[SMC] emotion: mainEmotion read failed");
    return;
  }
  Log("[SMC] emotion: mainEmotion off=0x%X ptr=%p", meOff, mainEmo);
  if (!mainEmo)
    return;
  void *ec = nullptr;
  __try {
    ec = il2cpp_object_get_class(mainEmo);
  } __except (1) {
  }
  if (!ec)
    return;
  __try {
    SMCDumpFieldsTyped(ec, "MainEmotion");
  } __except (1) {
  }
  const char *poseNames[] = {"_pose", "m_pose", "pose"};
  int poseOff = FindFieldInHierarchy(ec, poseNames, 3, nullptr);
  Log("[SMC] emotion: _pose=0x%X", poseOff);
  if (poseOff < 0)
    return;
  void *pose = nullptr;
  __try {
    pose = *(void **)((char *)mainEmo + poseOff);
  } __except (1) {
    return;
  }
  if (!pose)
    return;
  void *pc = nullptr;
  __try {
    pc = il2cpp_object_get_class(pose);
  } __except (1) {
  }
  if (!pc)
    return;
  __try {
    SMCDumpFieldsTyped(pc, "EmotionPose");
  } __except (1) {
  }
  // 逐个字段：凡是看起来像 List 的，就把元素里的 名字+数值 打出来
  __try {
    void *it = nullptr, *f;
    int listed = 0;
    while ((f = il2cpp_class_get_fields(pc, &it)) && listed < 4) {
      const char *fn = il2cpp_field_get_name(f);
      int fo = (int)il2cpp_field_get_offset(f);
      const char *ftn = "?";
      if (il2cpp_field_get_type && il2cpp_type_get_name) {
        void *ft = il2cpp_field_get_type(f);
        if (ft)
          ftn = il2cpp_type_get_name(ft);
      }
      if (!ftn || !fn || !strstr(ftn, "List"))
        continue;
      void *lst = *(void **)((char *)pose + fo);
      if (!lst)
        continue;
      int len = *(int *)((char *)lst + IL2CPP_LIST_SIZE);
      void *items = *(void **)((char *)lst + IL2CPP_LIST_ITEMS);
      Log("[SMC] emotion: %s (%s) len=%d", fn, ftn, len);
      if (!items || len <= 0 || len > 200)
        continue;
      listed++;
      for (int i = 0; i < len && i < 24; i++) {
        void *elem =
            *(void **)((char *)items + IL2CPP_ARRAY_DATA + i * 8);
        if (!elem)
          continue;
        void *elemCls = il2cpp_object_get_class(elem);
        if (!elemCls)
          continue;
        const char *nameFields[] = {"_ctrlName", "m_ctrlName", "ctrlName",
                                    "_name"};
        const char *valFields[] = {"_value", "m_value", "value"};
        int no = FindFieldInHierarchy(elemCls, nameFields, 4, nullptr);
        int vo = FindFieldInHierarchy(elemCls, valFields, 3, nullptr);
        char nm[96] = "";
        float val = -1.0f;
        if (no > 0) {
          void *so = *(void **)((char *)elem + no);
          if (so)
            ReadStr(so, nm, sizeof(nm));
        }
        if (vo > 0)
          val = *(float *)((char *)elem + vo);
        Log("[SMC] emotion:   [%d] '%s' = %.3f", i, nm, val);
      }
    }
  } __except (1) {
    Log("[SMC] emotion: list dump exception");
  }
}

static void SMCProbeMorphWeights(void *smc) {
  if (s_morphWeightProbed || !smc)
    return;
  s_morphWeightProbed = true;
  Log("[SMC] ==== weight source probe start (smc=%p) ====", smc);
  // 1) 类字段的名字 + 声明类型（纯反射，不解引用）
  if (!s_fieldsDumped && s_smcClass) {
    s_fieldsDumped = true;
    SMCDumpFieldsTyped(s_smcClass, "SkeletalMorphCore");
  }
  int amOff = -1, phOff = -1, mxOff = -1;
  __try {
    amOff = SafeOff(s_offAllMorphs, 0x210, "smc.allMorphs");
  } __except (1) {
  }
  __try {
    phOff = SafeOff(s_offPhonemeWeights, 0x320, "smc.phonemesWeights");
  } __except (1) {
  }
  __try {
    mxOff = SafeOff(s_offMicroExprWeights, 0x330, "smc.microExprWeights");
  } __except (1) {
  }
  // 2) 三个候选：managed 对象 / 内联 native buffer 两种解释分别打印
  SMCDumpBufferCandidates(smc, amOff, "allMorphs");
  SMCDumpBufferCandidates(smc, phOff, "phonemes");
  SMCDumpBufferCandidates(smc, mxOff, "microExpr");
  // 3) managed 解释成立时：元素类字段 + 挑一个 float 权重字段
  __try {
    void *am = *(void **)((char *)smc + amOff);
    if (am) {
      void *amCls = il2cpp_object_get_class(am);
      const char *acn = amCls ? il2cpp_class_get_name(amCls) : "";
      int len = *(int *)((char *)am + IL2CPP_ARRAY_LEN);
      if (len > 0 && len < 4096) {
        void *elem0 = *(void **)((char *)am + IL2CPP_ARRAY_DATA);
        void *ec = elem0 ? il2cpp_object_get_class(elem0) : nullptr;
        if (ec) {
          const char *ecn = il2cpp_class_get_name(ec);
          if (ecn) {
            strncpy(s_allMorphsClass, ecn, sizeof(s_allMorphsClass) - 1);
            s_allMorphsClass[sizeof(s_allMorphsClass) - 1] = 0;
          }
          Log("[SMC] allMorphs elem class=%s", ecn ? ecn : "?");
          SMCDumpFieldsTyped(ec, "allMorphs elem");
          void *it = nullptr, *f;
          int firstFloat = -1;
          while ((f = il2cpp_class_get_fields(ec, &it))) {
            int fo = (int)il2cpp_field_get_offset(f);
            if (fo < 0x10 || fo > 0x80)
              continue;
            const char *fn = il2cpp_field_get_name(f);
            const char *ftn = "?";
            if (il2cpp_field_get_type && il2cpp_type_get_name) {
              void *ft = il2cpp_field_get_type(f);
              if (ft)
                ftn = il2cpp_type_get_name(ft);
            }
            if (!ftn || !strstr(ftn, "Single"))
              continue;
            if (firstFloat < 0)
              firstFloat = fo;
            if (fn && (strstr(fn, "weight") || strstr(fn, "Weight") ||
                       strstr(fn, "value") || strstr(fn, "Value")))
              s_allMorphsWeightOff = fo;
          }
          if (s_allMorphsWeightOff < 0)
            s_allMorphsWeightOff = firstFloat;
        }
        if (acn && (strstr(acn, "Single") || strstr(acn, "Float"))) {
          s_allMorphsIsFloatArray = true;
          float *d = (float *)((char *)am + IL2CPP_ARRAY_DATA);
          char line[192] = {};
          int pos = 0;
          for (int i = 0; i < len && i < 12 && pos < 176; i++)
            pos += snprintf(line + pos, sizeof(line) - pos, "%.3f ", d[i]);
          Log("[SMC] allMorphs float[]: %s", line);
        }
        Log("[SMC] allMorphs managed: class=%s len=%d weightField=0x%X",
            acn ? acn : "?", len, s_allMorphsWeightOff);
      }
    }
  } __except (1) {
    Log("[SMC] allMorphs managed interpretation failed");
  }
  // 4) MainEmotion → EmotionPose 链路（带名字，最直接的来源）
  SMCProbeEmotionPose(smc);
  Log("[SMC] ==== weight source probe end ====");
}

static bool SMCReadMorphWeight(void *smc, int morphId, float *out) {
  *out = 0.0f;
  if (!smc || morphId < 0 || !s_morphWeightProbed)
    return false;
  float v = 0.0f;
  __try {
    int amOff = SafeOff(s_offAllMorphs, 0x210, "smc.allMorphs");
    void *am = *(void **)((char *)smc + amOff);
    if (!am)
      return false;
    int len = *(int *)((char *)am + IL2CPP_ARRAY_LEN);
    if (morphId >= len)
      return false;
    if (s_allMorphsIsFloatArray) {
      v = ((float *)((char *)am + IL2CPP_ARRAY_DATA))[morphId];
    } else {
      if (s_allMorphsWeightOff < 0)
        return false;
      void *elem = *(void **)((char *)am + IL2CPP_ARRAY_DATA + morphId * 8);
      if (!elem)
        return false;
      v = *(float *)((char *)elem + s_allMorphsWeightOff);
    }
  } __except (1) {
    return false;
  }
  // 明显不是权重就丢掉，避免把垃圾值当成表情填进滑条
  if (!(v == v) || v < -0.05f || v > 1.5f)
    return false;
  *out = v;
  return true;
}

static void CharGoName(char *buf, int sz) {
  buf[0] = 0;
  __try {
    void *root = SMCRoot();
    void *go = (root && g_component_get_gameObject)
                   ? Invoke(g_component_get_gameObject, root)
                   : nullptr;
    void *nb = (go && g_object_get_name) ? Invoke(g_object_get_name, go)
                                         : nullptr;
    if (nb)
      ReadStr(nb, buf, sz);
  } __except (1) {
  }
}

// 放弃当前锁定的实例（把它的大列表还回游戏），保留面板权重与驱动开关
static void SMCDropLockKeepWeights() {
  SMCAutomation().release();
  s_wasFrozen = false;
  SMCFaceInvalidate();
  SMCRestoreBigList();
  s_smcCore = nullptr;
  s_confirmedSMC = nullptr;
  s_frame = 0;
  s_bigListCaptured = false;
  s_capturedLen = 0;
  s_faceBoneRefs = nullptr;
  s_faceBoneCount = 0;
  s_faceBonesCaptured = false;
  s_captureNeutral = false;
  s_neutralFrames = 0;
  s_driveBaseReady = false;
  s_lastFacePoseValid = false;
  s_holdApplied = false;
  for (int i = 0; i < SMC_MAX_SLIDERS; i++)
    s_fitW[i] = 0.0f;
  s_boneMapReady = false;
  s_boneIDMapCount = 0;
  s_smcOwnershipVerified = false;
  s_smcCheckedInstance = nullptr;
  memset(s_boneIDToIdx, -1, sizeof(s_boneIDToIdx));
  memset(s_faceBoneTouched, 0, sizeof(s_faceBoneTouched));
  for (int i = 0; i < SMC_NUM_MOUTH; i++)
    s_mouthShapes[i].resolved = false;
  for (int i = 0; i < s_extraMorphCount; i++)
    for (int t = 0; t < s_extraMorphs[i].targetCount; t++)
      s_extraMorphs[i].targets[t].resolved = false;
  s_mouthResolved = false;
  s_extraMorphsResolved = false;
}

static void SMCRejectInstanceAndDropLock(void *smc) {
  SMCRememberRejected(smc);
  SMCDropLockKeepWeights();
}

// ---- 表情位姿 ↔ 权重 ----
// 冻结时必须"保持当前表情"，所以要在冻结瞬间把当前脸 P 反解成滑条权重：
// 用捕获到的 morph 增量做基向量，坐标下降拟合 w（滑条是相对中性默认值的权重），
// 再把拟合不出来的残差折进 s_driveBase —— 这样 w = w₀ 时脸精确等于 P。

// 把某个滑条（0..4 口型，5.. 表情）的"单位权重增量"按骨累加；返回触及骨骼数
static int SMCCollectSliderUnit(int idx, float (*uPos)[3], float (*uRot)[3],
                                int *touched, int maxTouched) {
  int n = 0;
  if (idx < 0 || idx >= SMC_MAX_SLIDERS)
    return 0;
  if (idx < SMC_NUM_MOUTH) {
    if (!s_mouthShapes[idx].resolved)
      return 0;
    int start = s_mouthShapes[idx].jobStartIdx;
    int cnt = s_mouthShapes[idx].jobCount;
    if (start < 0 || cnt <= 0 || start + cnt > s_capturedLen)
      return 0;
    for (int i = start; i < start + cnt; i++) {
      const SMCMorphBoneEntry &e = s_capturedExpression[i];
      int b = (e.boneID >= 0 && e.boneID < SMC_BONE_MAP_SIZE)
                  ? s_boneIDToIdx[e.boneID]
                  : -1;
      if (b < 0 || b >= s_faceBoneCount)
        continue;
      if (fabsf(e.deltaPosX) > 1.0f || fabsf(e.deltaPosY) > 1.0f ||
          fabsf(e.deltaPosZ) > 1.0f)
        continue;
      bool first = (uPos[b][0] == 0.0f && uPos[b][1] == 0.0f &&
                    uPos[b][2] == 0.0f && uRot[b][0] == 0.0f &&
                    uRot[b][1] == 0.0f && uRot[b][2] == 0.0f);
      uPos[b][0] += e.deltaPosX;
      uPos[b][1] += e.deltaPosY;
      uPos[b][2] += e.deltaPosZ;
      if (fabsf(e.deltaRotX) < 30.0f && fabsf(e.deltaRotY) < 30.0f &&
          fabsf(e.deltaRotZ) < 30.0f) {
        uRot[b][0] += e.deltaRotX;
        uRot[b][1] += e.deltaRotY;
        uRot[b][2] += e.deltaRotZ;
      }
      if (first && n < maxTouched)
        touched[n++] = b;
    }
    return n;
  }
  int em = idx - SMC_NUM_MOUTH;
  if (em >= s_extraMorphCount)
    return 0;
  for (int t = 0; t < s_extraMorphs[em].targetCount; t++) {
    const SMCExtraTarget &tgt = s_extraMorphs[em].targets[t];
    if (!tgt.resolved || tgt.startIdx < 0 ||
        tgt.startIdx + tgt.count > s_capturedLen)
      continue;
    bool isEye = (strncmp(tgt.endfieldName, "eye_", 4) == 0);
    for (int i = tgt.startIdx; i < tgt.startIdx + tgt.count; i++) {
      const SMCMorphBoneEntry &e = s_capturedExpression[i];
      int b = (e.boneID >= 0 && e.boneID < SMC_BONE_MAP_SIZE)
                  ? s_boneIDToIdx[e.boneID]
                  : -1;
      if (b < 0 || b >= s_faceBoneCount)
        continue;
      if (fabsf(e.deltaPosX) > 1.0f || fabsf(e.deltaPosY) > 1.0f ||
          fabsf(e.deltaPosZ) > 1.0f)
        continue;
      bool first = (uPos[b][0] == 0.0f && uPos[b][1] == 0.0f &&
                    uPos[b][2] == 0.0f && uRot[b][0] == 0.0f &&
                    uRot[b][1] == 0.0f && uRot[b][2] == 0.0f);
      uPos[b][0] += e.deltaPosX;
      uPos[b][1] += e.deltaPosY;
      uPos[b][2] += e.deltaPosZ;
      if (!isEye && fabsf(e.deltaRotX) < 30.0f && fabsf(e.deltaRotY) < 30.0f &&
          fabsf(e.deltaRotZ) < 30.0f) {
        uRot[b][0] += e.deltaRotX;
        uRot[b][1] += e.deltaRotY;
        uRot[b][2] += e.deltaRotZ;
      }
      if (first && n < maxTouched)
        touched[n++] = b;
    }
  }
  return n;
}

// 目标总增量（相对中性脸）：位移向量 + 欧拉角向量
static void SMCTargetDelta(int b, const SMCFaceBone *target, const SMCFaceBone *base,
                           float *outPos, float *outEuler) {
  outPos[0] = target[b].px - base[b].px;
  outPos[1] = target[b].py - base[b].py;
  outPos[2] = target[b].pz - base[b].pz;
  Quat bq(base[b].rx, base[b].ry, base[b].rz, base[b].rw);
  Quat tq(target[b].rx, target[b].ry, target[b].rz, target[b].rw);
  Quat d = NormQ(tq * Conj(bq));
  Vec3 e = d.ToEulerDeg();
  outEuler[0] = e.x;
  outEuler[1] = e.y;
  outEuler[2] = e.z;
}

static float SMCPoseResidual(const SMCFaceBone *a, const SMCFaceBone *b, int count) {
  float worst = 0.0f;
  for (int i = 0; i < count; i++) {
    float dp = fabsf(a[i].px - b[i].px) + fabsf(a[i].py - b[i].py) +
               fabsf(a[i].pz - b[i].pz);
    Quat aq(a[i].rx, a[i].ry, a[i].rz, a[i].rw);
    Quat bq(b[i].rx, b[i].ry, b[i].rz, b[i].rw);
    float da = Quat::Angle(aq, bq);
    float e = dp + da;
    if (e > worst)
      worst = e;
  }
  return worst;
}

// 把当前脸 P 反解成权重（相对中性脸），并把残差折进 s_driveBase
static void SMCHoldPoseAsWeights(const SMCFaceBone *target) {
  const int nSliders = SMC_NUM_MOUTH + s_extraMorphCount;
  if (s_faceBoneCount <= 0 || nSliders > SMC_MAX_SLIDERS || s_capturedLen <= 0)
    return;
  static float uPos[SMC_MAX_FACE_BONES][3];
  static float uRot[SMC_MAX_FACE_BONES][3];
  static int touched[SMC_MAX_FACE_BONES];
  static float sumPos[SMC_MAX_FACE_BONES][3];
  static float sumRot[SMC_MAX_FACE_BONES][3];
  static float reqPos[SMC_MAX_FACE_BONES][3];
  static float reqRot[SMC_MAX_FACE_BONES][3];
  memset(sumPos, 0, sizeof(sumPos));
  memset(sumRot, 0, sizeof(sumRot));
  for (int b = 0; b < s_faceBoneCount; b++)
    SMCTargetDelta(b, target, s_faceRestPose, reqPos[b], reqRot[b]);
  for (int i = 0; i < nSliders; i++)
    s_fitW[i] = 0.0f;
  // 坐标下降：每次把残差投影到该滑条的影响方向上
  for (int pass = 0; pass < 3; pass++) {
    for (int idx = 0; idx < nSliders; idx++) {
      memset(uPos, 0, sizeof(uPos));
      memset(uRot, 0, sizeof(uRot));
      int nt = SMCCollectSliderUnit(idx, uPos, uRot, touched, SMC_MAX_FACE_BONES);
      if (nt <= 0)
        continue;
      float num = 0.0f, den = 0.0f;
      for (int k = 0; k < nt; k++) {
        int b = touched[k];
        if (b < 0 || b >= s_faceBoneCount)
          continue;
        for (int c = 0; c < 3; c++) {
          float othersP = sumPos[b][c] - s_fitW[idx] * uPos[b][c];
          float othersR = sumRot[b][c] - s_fitW[idx] * uRot[b][c];
          num += (reqPos[b][c] - othersP) * uPos[b][c];
          num += (reqRot[b][c] - othersR) * uRot[b][c];
          den += uPos[b][c] * uPos[b][c] + uRot[b][c] * uRot[b][c];
        }
      }
      if (den < 1e-8f)
        continue;
      float nw = s_fitW[idx] + num / den;
      if (nw < 0.0f)
        nw = 0.0f;
      if (nw > 1.0f)
        nw = 1.0f;
      float dw = nw - s_fitW[idx];
      s_fitW[idx] = nw;
      if (dw != 0.0f) {
        for (int k = 0; k < nt; k++) {
          int b = touched[k];
          if (b < 0 || b >= s_faceBoneCount)
            continue;
          for (int c = 0; c < 3; c++) {
            sumPos[b][c] += dw * uPos[b][c];
            sumRot[b][c] += dw * uRot[b][c];
          }
        }
      }
    }
  }
  // 残差折进基准位姿：base = P − Σ w₀×增量（旋转用逆作用）
  int nz = 0;
  for (int b = 0; b < s_faceBoneCount; b++) {
    s_driveBase[b] = target[b];
    s_driveBase[b].px -= sumPos[b][0];
    s_driveBase[b].py -= sumPos[b][1];
    s_driveBase[b].pz -= sumPos[b][2];
    Quat back = Conj(Quat::FromEulerDeg(
        Vec3(sumRot[b][0], sumRot[b][1], sumRot[b][2])));
    Quat tq(target[b].rx, target[b].ry, target[b].rz, target[b].rw);
    Quat bq = NormQ(back * tq);
    s_driveBase[b].rx = bq.x;
    s_driveBase[b].ry = bq.y;
    s_driveBase[b].rz = bq.z;
    s_driveBase[b].rw = bq.w;
  }
  s_driveBaseReady = true;
  // 滑条 = 拟合出的权重
  for (int i = 0; i < SMC_NUM_MOUTH; i++)
    s_mouthWeights[i] = s_fitW[i];
  for (int em = 0; em < s_extraMorphCount; em++) {
    s_extraMorphs[em].weight = s_fitW[SMC_NUM_MOUTH + em];
    s_extraMorphs[em].prevWeight = s_fitW[SMC_NUM_MOUTH + em];
    if (s_fitW[SMC_NUM_MOUTH + em] > 0.001f)
      nz++;
  }
  // 拟合质量：把反解结果再演算一遍，看和当前脸差多少
  float residual = 0.0f;
  if (s_faceBonesCaptured)
    residual = SMCPoseResidual(target, s_driveBase, s_faceBoneCount);
  s_driving = true;
  Log("[SMC] hold current face: sliders fitted (%d expr nonzero), "
      "residual(base↔face)=%.4f",
      nz, residual);
}

// 归零时把基准与权重一起复位到中性默认脸
static void SMCClearHold() {
  if (s_faceBonesCaptured)
    memcpy(s_driveBase, s_faceRestPose, sizeof(s_driveBase));
  s_driveBaseReady = s_faceBonesCaptured;
  for (int i = 0; i < SMC_MAX_SLIDERS; i++)
    s_fitW[i] = 0.0f;
}

static void SMCSnapshotFacePose() {
  if (!s_faceBoneRefs || s_faceBoneCount <= 0)
    return;
  for (int i = 0; i < s_faceBoneCount; i++) {
    if (!s_faceBoneRefs[i]) {
      s_lastFacePoseBuf[i].transform = nullptr;
      continue;
    }
    Vec3 p = GetBoneLocalPos(s_faceBoneRefs[i]);
    Quat r = GetBoneLocalRot(s_faceBoneRefs[i]);
    s_lastFacePoseBuf[i] = {p.x, p.y, p.z, r.x, r.y, r.z, r.w,
                            s_faceBoneRefs[i]};
  }
  s_lastFacePoseValid = true;
}

// 动态解析 SMC 类字段偏移（优先字段名，失败回退 SafeOff 常量）
static void ResolveSMCOffsets(void *cls) {
  if (!cls)
    return;
  auto getOff = [&](const char *name) -> int {
    void *fiter = nullptr;
    void *field;
    while ((field = il2cpp_class_get_fields(cls, &fiter))) {
      const char *fn = il2cpp_field_get_name(field);
      if (fn && strcmp(fn, name) == 0)
        return (int)il2cpp_field_get_offset(field);
    }
    return -1;
  };
  __try {
    s_offAllMorphs = getOff("m_allMorphs");
    s_offBigList = getOff("m_morphNameHashToMorphDataBoneBigList");
    s_offNativeHashMap = getOff("m_morphNameHashToMorphData");
    s_offMorphBSDirty = getOff("m_morphBSDirty");
    s_offAllMorphBoneDirty = getOff("m_allMorphBoneDirty");
    s_offAvatarData = getOff("m_avatarData");
    if (s_offAvatarData < 0)
      s_offAvatarData = getOff("morphData");
    s_offAllBonesTransforms = getOff("m_allBonesTransforms");
    s_offBoneIDToIdx = getOff("m_boneIDToIdx");
    s_offSmcEyeLookAt = getOff("m_isEyeLookAtIKEnable");
    s_offPhonemeWeights = getOff("m_phonemesWeights");
    s_offMicroExprWeights = getOff("m_microExpressionWeights");
    s_offMainEmotion = getOff("m_mainEmotion");
    if (s_offSmcEyeLookAt < 0)
      s_offSmcEyeLookAt = getOff("enableEyeLookAtIK");
    if (s_offSmcEyeLookAt < 0)
      s_offSmcEyeLookAt = getOff("m_enableEyeLookAtIK");
    if (s_offSmcEyeLookAt < 0)
      s_offSmcEyeLookAt = getOff("_enableEyeLookAtIK");
    Log("[SMC] offsets: allMorphs=%d bigList=%d hashMap=%d bsDirty=%d "
        "boneDirty=%d avatarData=%d bones=%d boneIDToIdx=%d eyeLookAt=%d "
        "phonemes=%d microExpr=%d mainEmotion=%d",
        s_offAllMorphs, s_offBigList, s_offNativeHashMap, s_offMorphBSDirty,
        s_offAllMorphBoneDirty, s_offAvatarData, s_offAllBonesTransforms,
        s_offBoneIDToIdx, s_offSmcEyeLookAt, s_offPhonemeWeights,
        s_offMicroExprWeights, s_offMainEmotion);
  } __except (1) {
    Log("[SMC] ResolveSMCOffsets exception");
  }
}

// 从 SMC 大列表字段复制当前评估结果（job hook 或 Update 首帧调用）
static void SMCCaptureBigList(void *smcBase) {
  if (!smcBase || s_bigListCaptured)
    return;
  __try {
    int blOff = SafeOff(s_offBigList, 0x120, "smc.bigList");
    void *bigBuf = *(void **)((char *)smcBase + blOff);
    int bigLen = *(int *)((char *)smcBase + blOff + 8);
    if (bigBuf && bigLen > 0 && bigLen < SMC_MAX_BIGLIST) {
      if (bigLen > SMC_MAX_BIGLIST)
        bigLen = SMC_MAX_BIGLIST;
      memcpy(s_capturedExpression, bigBuf,
             bigLen * sizeof(SMCMorphBoneEntry));
      s_capturedLen = bigLen;
      s_bigListCaptured = true;
      int nonZero = 0;
      for (int i = 0; i < bigLen; i++) {
        const SMCMorphBoneEntry &e = s_capturedExpression[i];
        if (e.deltaPosX || e.deltaPosY || e.deltaPosZ || e.deltaRotX ||
            e.deltaRotY || e.deltaRotZ)
          nonZero++;
      }
      Log("[SMC] BigList captured: %d entries (%d nonzero)", bigLen, nonZero);
      for (int i = 0; i < 3 && i < bigLen; i++) {
        const SMCMorphBoneEntry &e = s_capturedExpression[i];
        Log("[SMC]   [%d] hash=%d boneID=%d dP=(%.3f,%.3f,%.3f) "
            "dR=(%.3f,%.3f,%.3f)",
            i, e.boneNameHash, e.boneID, e.deltaPosX, e.deltaPosY,
            e.deltaPosZ, e.deltaRotX, e.deltaRotY, e.deltaRotZ);
      }
    } else {
      Log("[SMC] BigList invalid: buf=%p len=%d", bigBuf, bigLen);
    }
  } __except (1) {
    Log("[SMC] SMCCaptureBigList exception");
  }
}

// 把大列表数据还回游戏（停止驱动/换角色时避免留脏数据）
static void SMCRestoreBigList() {
  if (!s_confirmedSMC || s_capturedLen <= 0)
    return;
  __try {
    int blOff = SafeOff(s_offBigList, 0x120, "smc.bigList");
    void *bigBuf = *(void **)((char *)s_confirmedSMC + blOff);
    int bigLen = *(int *)((char *)s_confirmedSMC + blOff + 8);
    if (bigBuf && bigLen == s_capturedLen) {
      memcpy(bigBuf, s_capturedExpression,
             bigLen * sizeof(SMCMorphBoneEntry));
      Log("[SMC] BigList restored (%d entries)", bigLen);
    }
  } __except (1) {
    Log("[SMC] BigList restore failed");
  }
}

static void SMCReleaseFreeze() {
  poser_gaze::Release(true,nullptr,SMCGazeContext());
  SMCAutomation().release();
  if (s_wasFrozen && CharAnimatorAlive()) SMCRestoreBigList();
  s_wasFrozen=false;
}

// NativeHashMap 探测 + morph 名 → 大列表区间映射（A/I/U/E/O 用硬编码哈希，
// 表情目标按名字在 morphMappingNames 里反查哈希）
static void ResolveSMCMouthShapes(void *smcBase) {
  if (s_mouthResolved)
    return;
  // morphId 每次重新解析：读回当前表情权重时按它索引 m_allMorphs
  for (int em = 0; em < s_extraMorphCount; em++)
    for (int t = 0; t < s_extraMorphs[em].targetCount; t++)
      s_extraMorphs[em].targets[t].morphId = -1;
  __try {
    int hmOff = SafeOff(s_offNativeHashMap, 0xF8, "smc.nativeHashMap");
    void *hmBuffer = *(void **)((char *)smcBase + hmOff);
    if (!hmBuffer) {
      Log("[SMC] No hashmap at SMC+0x%X", hmOff);
      return;
    }
    int *hmData = (int *)hmBuffer;
    int keyCapacity = hmData[8];
    int bucketCap = hmData[9];
    int allocLen = hmData[10];
    if (keyCapacity <= 0 || keyCapacity > 1000 || bucketCap <= 0 ||
        allocLen <= 0 || allocLen > keyCapacity) {
      Log("[SMC] Invalid hashmap: keyCap=%d bucketCap=%d allocLen=%d",
          keyCapacity, bucketCap, allocLen);
      return;
    }
    if ((bucketCap & (bucketCap + 1)) != 0)
      Log("[SMC] WARN: bucketCap=%d not power-of-2 bitmask, layout may differ",
          bucketCap);
    char *values = *(char **)(&hmData[0]);
    int *keys = *(int **)(&hmData[2]);
    int *nextArr = *(int **)(&hmData[4]);
    int *buckets = *(int **)(&hmData[6]);
    if (!values || !keys || !nextArr || !buckets) {
      Log("[SMC] Null pointers in hashmap");
      return;
    }
    int valStride = (int)(((uintptr_t)keys - (uintptr_t)values) /
                          keyCapacity);
    if (valStride < 8 || valStride > 200) {
      Log("[SMC] Invalid valStride=%d", valStride);
      return;
    }
    if (valStride != 40)
      Log("[SMC] WARN: valStride=%d (expected 40), layout may differ",
          valStride);

    for (int m = 0; m < SMC_NUM_MOUTH; m++) {
      int targetHash = s_mouthShapes[m].nameHash;
      int bucket = ((unsigned)targetHash) & ((unsigned)bucketCap);
      int entryIdx = buckets[bucket];
      int depth = 0;
      while (entryIdx >= 0 && entryIdx < keyCapacity && depth < 100) {
        if (keys[entryIdx] == targetHash) {
          int *vi = (int *)(values + entryIdx * valStride);
          s_mouthShapes[m].morphId = vi[0];
          s_mouthShapes[m].startIdx = vi[4];
          s_mouthShapes[m].count = vi[5];
          s_mouthShapes[m].jobStartIdx = vi[4];
          s_mouthShapes[m].jobCount = vi[5];
          s_mouthShapes[m].resolved = true;
          Log("[SMC] Mouth '%s': morphId=%d start=%d count=%d partType=%d",
              s_mouthShapes[m].name, vi[0], vi[4], vi[5], vi[3]);
          if (vi[4] < 0 || vi[4] > 10000 || vi[5] <= 0 || vi[5] > 500) {
            Log("[SMC] WARN: '%s' suspicious start/count, layout may differ",
                s_mouthShapes[m].name);
            s_mouthShapes[m].resolved = false;
          }
          break;
        }
        entryIdx = nextArr[entryIdx];
        depth++;
      }
      if (!s_mouthShapes[m].resolved)
        Log("[SMC] Mouth '%s' not found in hashmap", s_mouthShapes[m].name);
    }

    // AvatarData.morphMappingNames：名字数组，供表情目标反查
    int adOff = SafeOff(s_offAvatarData, 0x58, "smc.avatarData");
    void *avatarData = *(void **)((char *)smcBase + adOff);
    if (avatarData && s_offMorphMappingNames < 0) {
      void *adClass = il2cpp_object_get_class(avatarData);
      if (adClass) {
        void *fiter = nullptr;
        void *field;
        while ((field = il2cpp_class_get_fields(adClass, &fiter))) {
          const char *fn = il2cpp_field_get_name(field);
          if (fn && strcmp(fn, "morphMappingNames") == 0) {
            s_offMorphMappingNames = (int)il2cpp_field_get_offset(field);
            Log("[SMC] AvatarData.morphMappingNames=0x%X",
                s_offMorphMappingNames);
            break;
          }
        }
      }
    }
    int mnOff = SafeOff(s_offMorphMappingNames, 0x38, "smc.morphMappingNames");
    void *nameArr =
        avatarData ? *(void **)((char *)avatarData + mnOff) : nullptr;
    int nameArrLen = nameArr ? *(int *)((char *)nameArr + IL2CPP_ARRAY_LEN) : 0;
    if (nameArr && nameArrLen > 0 && nameArrLen <= 500) {
      int resolvedTargets = 0;
      for (int e = 0; e < allocLen && e < keyCapacity; e++) {
        int *vi = (int *)(values + e * valStride);
        int morphId = vi[0];
        int smcStart = vi[4];
        int smcCount = vi[5];
        if (morphId < 0 || morphId >= nameArrLen || smcCount <= 0)
          continue;
        char nn[128] = {};
        void *strObj = *(void **)((char *)nameArr + IL2CPP_ARRAY_DATA +
                                  morphId * 8);
        if (!strObj)
          continue;
        ReadStr(strObj, nn, sizeof(nn));
        for (int em = 0; em < s_extraMorphCount; em++) {
          for (int t = 0; t < s_extraMorphs[em].targetCount; t++) {
            SMCExtraTarget &tgt = s_extraMorphs[em].targets[t];
            if (!tgt.resolved && strcmp(nn, tgt.endfieldName) == 0) {
              tgt.nameHash = keys[e];
              tgt.morphId = morphId;
              tgt.startIdx = smcStart;
              tgt.count = smcCount;
              tgt.jobStartIdx = smcStart;
              tgt.jobCount = smcCount;
              tgt.resolved = (smcStart >= 0 && smcStart <= 10000 &&
                              smcCount > 0 && smcCount <= 500);
              if (tgt.resolved) {
                resolvedTargets++;
                Log("[SMC] %s -> %s: start=%d count=%d", nn,
                    s_extraMorphs[em].label, smcStart, smcCount);
              } else {
                Log("[SMC] WARN: %s suspicious start=%d count=%d", nn,
                    smcStart, smcCount);
              }
            }
          }
        }
      }
      s_extraMorphsResolved = (resolvedTargets > 0);
      int totalTargets = 0;
      for (int em = 0; em < s_extraMorphCount; em++)
        totalTargets += s_extraMorphs[em].targetCount;
      Log("[SMC] Resolved %d/%d extra morph targets (morph names=%d)",
          resolvedTargets, totalTargets, nameArrLen);
    } else {
      Log("[SMC] morphMappingNames empty: arr=%p len=%d", nameArr,
          nameArrLen);
    }
    s_mouthResolved = true;
  } __except (1) {
    Log("[SMC] ResolveSMCMouthShapes exception");
    s_mouthResolved = true; // 防止每帧重试打日志
  }
}

static void SMCSelectCandidate(void *param1) {
  if (!s_confirmedSMC && param1) {
    SMCRefreshCharBoneSnapshot();
    // Retry changing/loading instances periodically, without ever accepting a
    // foreign face merely because many NPCs or callbacks were rejected.
    if (++s_smcRejectStrikes > 240) {
      s_smcRejectedCount = 0;
      s_smcRejectStrikes = 0;
    }
    if (!SMCIsRejected(param1)) {
      int overlap=SMCBoneOverlapRaw(param1);
      if(overlap>0) {
        s_confirmedSMC=param1;s_smcOwnershipVerified=true;
        Log("[SMC] Candidate matched actor bones: core=%p overlap=%d revision=%d",param1,overlap,SMCRevision());
      } else if(overlap==0&&s_charBoneXformCount>=100)SMCRememberRejected(param1);
    }
  }
}
// DoEvaluateMorphToBoneJob：确认 SMC 实例、抓大列表、驱动时清零游戏自身增量
static void SMCMorphJobBefore(void *param1) {
  SMCSelectCandidate(param1);
  if (!s_bigListCaptured && param1 && param1 == s_confirmedSMC)
    SMCCaptureBigList(param1);

  // 中性基线采样：把大列表增量"整表"清零（不只我们映射到的那批骨），
  // 让游戏本帧把脸写回中性位姿，主线程据此抓基准
  if (s_captureNeutral && s_bigListCaptured && param1 &&
      param1 == s_confirmedSMC) {
    __try {
      int blOff = SafeOff(s_offBigList, 0x120, "smc.bigList");
      void *bigBuf = *(void **)((char *)param1 + blOff);
      int bigLen = *(int *)((char *)param1 + blOff + 8);
      if (bigBuf && bigLen > 0 && bigLen == s_capturedLen) {
        SMCMorphBoneEntry *live = (SMCMorphBoneEntry *)bigBuf;
        for (int i = 0; i < bigLen; i++) {
          live[i].deltaPosX = live[i].deltaPosY = live[i].deltaPosZ = 0;
          live[i].deltaRotX = live[i].deltaRotY = live[i].deltaRotZ = 0;
        }
      }
    } __except (1) {
    }
  }

  // 面板驱动中：清零大列表增量，防止游戏按原始 morph 权重改写骨骼
  // （同样限定冻结态：解冻后继续清零会让角色的脸一直不动）
  if (s_driving && SMCFrozen() && s_faceBonesCaptured && s_boneMapReady && param1 &&
      param1 == s_confirmedSMC) {
    __try {
      int blOff = SafeOff(s_offBigList, 0x120, "smc.bigList");
      void *bigBuf = *(void **)((char *)param1 + blOff);
      int bigLen = *(int *)((char *)param1 + blOff + 8);
      if (bigBuf && bigLen > 0 && bigLen == s_capturedLen) {
        SMCMorphBoneEntry *live = (SMCMorphBoneEntry *)bigBuf;
        for (int i = 0; i < bigLen; i++) {
          int boneID = live[i].boneID;
          int arrIdx =
              (boneID >= 0 && boneID < SMC_BONE_MAP_SIZE)
                  ? s_boneIDToIdx[boneID]
                  : -1;
          if (arrIdx < 0 || arrIdx >= s_faceBoneCount)
            continue;
          live[i].deltaPosX = live[i].deltaPosY = live[i].deltaPosZ = 0;
          live[i].deltaRotX = live[i].deltaRotY = live[i].deltaRotZ = 0;
        }
      }
    } __except (1) {
    }
  }
}

// 写回面部骨骼（局部位姿）。
// 注意：写的是"全部"面部骨，而不是只写被 morph 命中的那些——因为 s_faceBones 每帧
// 都以静息位姿为底再叠加增量，没被命中的骨就是静息位姿。只写"命中"的骨会导致：
// 权重调回 0 时没有任何骨被标记 → 上一帧的表情被留在骨上（"重置无效"的根因）。
// The GUI publishes one complete frame. Only the SMC owner thread changes weights.











static void SMCMotionPublish(const SMCMotionFrame& f) {
  AcquireSRWLockExclusive(&s_motionFaceLock); s_motionFaceMailbox=f; ReleaseSRWLockExclusive(&s_motionFaceLock);
}

static std::vector<mmd_face_controls::Native> SMCManualCatalog() {
  std::vector<mmd_face_controls::Native> fixed;
  const char *vowels[]={u8"あ",u8"い",u8"う",u8"え",u8"お"};
  for(int i=0;i<5;++i)fixed.push_back({vowels[i],i,3});
  for(int i=0;i<s_extraMorphCount;++i) {
    const char *target=s_extraMorphs[i].targets[0].endfieldName;
    int panel=!strncmp(target,"brow_",5)?1:!strncmp(target,"eye_",4)?2:!strncmp(target,"mouth_",6)?3:4;
    fixed.push_back({s_extraMorphs[i].vmdNameUtf8,5+i,panel});
  }
  return fixed;
}
static void SMCManualPrepare() {
  if(s_manualFace.owner!=SMCAnimator()||s_manualFace.generation!=s_faceGeneration||s_manualFace.profile!=s_characterProfile)
    s_manualFace.bind(SMCAnimator(),s_faceGeneration,s_characterProfile,SMCManualCatalog());
}
static bool SMCNativeChannelReady(int channel) {
  if(channel<0||channel>=SMC_NUM_MOUTH+s_extraMorphCount||!s_boneMapReady)return false;
  if(channel<SMC_NUM_MOUTH)return s_mouthResolved&&s_mouthShapes[channel].resolved&&s_mouthShapes[channel].jobCount>0;
  const auto &m=s_extraMorphs[channel-SMC_NUM_MOUTH];
  for(int i=0;i<m.targetCount;++i)if(m.targets[i].resolved&&m.targets[i].count>0)return true;
  return false;
}
// 0 unavailable, 1 character calibration, 2 optional fixed mapping.
static int SMCManualSource(const mmd_face_controls::Control &control) {
  int id=control.morph;
  if(s_manualFace.profile&&s_manualFace.profile==s_characterProfile&&s_characterBinding.ready&&
      s_characterBindingGeneration==s_faceGeneration&&id>=0&&id<int(s_characterBinding.usable.size())&&s_characterBinding.usable[id])return 1;
  return s_manualFace.fallback&&SMCNativeChannelReady(control.native)?2:0;
}
static SMCMotionFrame SMCManualFrame() {
  SMCMotionFrame frame;
  if(!s_mmdFaceMode||!SMCFrozen()||!s_manualFace.applied||s_manualFace.owner!=SMCAnimator()||
      s_manualFace.generation!=s_faceGeneration)return frame;
  frame.active=true;frame.animator=SMCAnimator();frame.generation=s_faceGeneration;
  frame.profile=s_manualFace.profile;frame.settings.strength=s_manualFace.strength;frame.settings.fallback=s_manualFace.fallback;
  for(int i=0;i<int(s_manualFace.controls.size());++i) {
    const auto &c=s_manualFace.controls[i];float value=s_manualFace.weights[i];
    int source=SMCManualSource(c);
    if(source==1)frame.expressions[c.morph]=value;
    if(source==2)frame.fallbackWeights[c.native]=face_geometry::Clamp(frame.fallbackWeights[c.native]+value,0,1);
  }
  return frame;
}

static void *__fastcall HookedSMCMorphJob(void *result, void *smc, uint32_t count,
                                        void *dependency, void *method) {
  if (RuntimeClosing()) return s_origMorphJob?s_origMorphJob(result,smc,count,dependency,method):result;
  std::unique_lock<std::recursive_mutex> lock(g_poseMutex, std::try_to_lock);
  if (!RuntimeClosing() && poser_agreement::Allowed() && g_editorEnabled.load() && lock.owns_lock() &&
      !CharacterSwitchInProgress() && !CharacterSelectionPending())
    { SMCActorScope actor(SMCFindActor(smc)); SMCMorphJobBefore(smc); }
  // Returning explicitly preserves RAX across the lock destructor as well as
  // busy/switching paths; a tail-call accidentally preserving RAX is insufficient.
  return s_origMorphJob
             ? s_origMorphJob(result, smc, count, dependency, method)
             : result;
}
static void SMCMotionConsume() {
  AcquireSRWLockShared(&s_motionFaceLock); s_motionFaceCurrent=s_motionFaceMailbox; ReleaseSRWLockShared(&s_motionFaceLock);
  if(s_mmdFaceMode)SMCManualPrepare();
  // Explicit VMD expressions take priority. The manual face remains available
  // after Stop and while the panel is hidden; it never publishes into the VMD mailbox.
  if(!s_motionFaceCurrent.active||s_motionFaceCurrent.animator!=SMCAnimator()||s_motionFaceCurrent.generation!=s_faceGeneration)
    s_motionFaceCurrent=SMCManualFrame();
  bool active=s_motionFaceCurrent.active && s_motionFaceCurrent.animator==SMCAnimator() &&
      s_motionFaceCurrent.generation==s_faceGeneration;
  s_motionFaceCurrent.active=active;
  if(s_motionFaceSaved && (!active || s_motionSavedCore!=s_smcCore || s_motionSavedGeneration!=s_faceGeneration)) {
    if(s_motionSavedCore==s_smcCore && s_motionSavedGeneration==s_faceGeneration) {
      s_driving=s_motionSavedDriving; s_driveBaseReady=s_motionSavedBaseReady;
      memcpy(s_driveBase,s_motionSavedBase,sizeof(s_driveBase));
      for(int i=0;i<SMC_NUM_MOUTH;i++)s_mouthWeights[i]=s_motionSavedWeights[i];
      for(int i=0;i<s_extraMorphCount;i++)s_extraMorphs[i].weight=s_extraMorphs[i].prevWeight=s_motionSavedWeights[i+SMC_NUM_MOUTH];
    }
    s_motionFaceSaved=false;
  }
  if(!active || !s_faceBonesCaptured || !s_driveBaseReady || s_captureNeutral)return;
  if(!s_motionFaceSaved) {
    s_motionFaceSaved=true;s_motionSavedCore=s_smcCore;s_motionSavedDriving=s_driving;s_motionSavedBaseReady=s_driveBaseReady;
    s_motionSavedGeneration=s_faceGeneration;
    memcpy(s_motionSavedBase,s_driveBase,sizeof(s_driveBase));
    for(int i=0;i<SMC_NUM_MOUTH;i++)s_motionSavedWeights[i]=s_mouthWeights[i];
    for(int i=0;i<s_extraMorphCount;i++)s_motionSavedWeights[i+SMC_NUM_MOUTH]=s_extraMorphs[i].weight;
  }
  // Evaluate motion weights against the neutral face, not the frozen expression.
  memcpy(s_driveBase,s_faceRestPose,sizeof(s_driveBase));s_driving=true;
  for(int i=0;i<SMC_NUM_MOUTH;i++)s_mouthWeights[i]=s_motionFaceCurrent.weights[i];
  for(int i=0;i<s_extraMorphCount;i++)s_extraMorphs[i].weight=s_extraMorphs[i].prevWeight=s_motionFaceCurrent.weights[i+SMC_NUM_MOUTH];
}
static void SMCManualMode(bool enabled) {
  s_mmdFaceMode=enabled;
  SMCManualPrepare();
  // Restore fixed sliders before the game-mode panel accepts another edit.
  SMCMotionConsume();
}
static bool SMCFaceMatrix(void *transform,mmd::Matrix *out) {
  __try {
    if(!UnityObjAlive(transform)||!g_transform_get_localToWorldMatrix)return false;
    void *value=Invoke(g_transform_get_localToWorldMatrix,transform);
    if(!value)return false;
    memcpy(out,static_cast<char *>(value)+16,sizeof(*out));
    for(float v:out->m)if(!std::isfinite(v))return false;
    return true;
  } __except(1) {return false;}
}
static Vec3 SMCFaceScale(void *transform) {
  __try {
    if(UnityObjAlive(transform)&&g_transform_get_localScale) {
      void *v=Invoke(g_transform_get_localScale,transform);
      if(v)return *reinterpret_cast<Vec3 *>(static_cast<char *>(v)+16);
    }
  } __except(1) {}
  return {1,1,1};
}
static bool SMCFaceIdentity(void *transform,char *name,void **parent) {
  __try {
    if(!UnityObjAlive(transform)||!g_transform_get_parent||!g_object_get_name)return false;
    ReadStr(Invoke(g_object_get_name,transform),name,128);
    *parent=Invoke(g_transform_get_parent,transform);return *parent!=nullptr;
  } __except(1) {return false;}
}
// Called once per neutral capture / skeleton revision on the SMC owner thread.
// No game objects are accessed by Evaluate(), nor by background file loaders.
static void SMCGazeBind(Vec3 origin,const std::vector<void *> &parents) {
  poser_gaze::Binding b;b.owner=SMCAnimator();b.generation=s_faceGeneration;
  for(const auto &bone:SMCBones()) {
    auto name=face_geometry::Canonical(bone.name);
    if(name=="bip001_head"||name=="head") {b.head=bone.transform;break;}
  }
  if(!b.head)return;
  Vec3 headPos;Quat headRot;
  if(!poser_gaze::Read(g_transform_get_position,b.head,&headPos,sizeof(headPos))||
     !poser_gaze::Read(g_transform_get_rotation,b.head,&headRot,sizeof(headRot))||
     !std::isfinite(QuatLen(headRot))||QuatLen(headRot)<.5f)return;
  Vec3 eyes[2],iris[2],mouth;bool haveIris[2]={};int mouthCount=0;
  auto inverse=Conj(NormQ(headRot));
  b.neutralHeadInverse=inverse;
  for(int i=0;i<int(s_faceNodes.size());++i) {
    auto name=face_geometry::Canonical(s_faceNodes[i].name);
    auto point=inverse*(s_faceNodes[i].neutral.position()+origin-headPos);
    int eye=name=="eyelfjoint"||name=="bip001_l_eye"?0:name=="eyertjoint"||name=="bip001_r_eye"?1:-1;
    if(eye>=0) {
      b.eyes[eye]=s_faceRestPose[i].transform;b.parents[eye]=parents[i];eyes[eye]=point;
      b.eyeInHead[eye]=NormQ(inverse*mmd::Rotation(s_faceNodes[i].neutral));
    }
    int ir=name=="facelfirisjoint"?0:name=="facertirisjoint"?1:-1;
    if(ir>=0){iris[ir]=point;haveIris[ir]=true;}
    if(name.find("lipmd")==0){mouth=mouth+point;++mouthCount;}
  }
  if(!b.eyes[0]||!b.eyes[1]||!b.parents[0]||!b.parents[1]||!mouthCount)return;
  b.opticalReady=haveIris[0]&&haveIris[1]&&Len(iris[0]-eyes[0])>1e-5f&&Len(iris[1]-eyes[1])>1e-5f;
  Vec3 optical;
  if(b.opticalReady) {
    // Only the symmetric average estimates face forward. Keep the authored
    // lateral iris offsets instead of treating each offset as a sight axis.
    optical=Norm(Norm(iris[0]-eyes[0])+Norm(iris[1]-eyes[1]));
    b.opticalReady=Len(optical)>.9f;
  }
  b.basis=eye_gaze::Calibrate(eyes[0],eyes[1],mouth*(1.f/mouthCount),{},optical);
  b.fallbackBasis=b.basis;
  SMCGazeContext().binding=b;
  Log("[GAZE] neutral eyes ready=%d optical=%d generation=%llu",b.basis.ready,b.opticalReady,(unsigned long long)b.generation);
}
static void SMCGazeTick() {
  auto &gaze=SMCGazeContext();
  Quat fallback[2];bool haveFallback=gaze.lease.active;
  for(int e=0;e<2;++e) {
    fallback[e]=gaze.lease.original[e];
    if(s_driving&&SMCFrozen()&&s_faceBoneEvalOk)for(int i=0;i<s_faceBoneCount;++i)
      if(s_faceBones[i].transform==gaze.binding.eyes[e]) {
        const auto &b=s_faceBones[i];fallback[e]={b.rx,b.ry,b.rz,b.rw};break;
      }
    if(s_motionFaceCurrent.active&&s_motionFaceCurrent.animator==SMCAnimator()&&
       s_motionFaceCurrent.generation==s_faceGeneration&&s_motionFaceCurrent.eyeDriven[e]&&
       s_motionFaceCurrent.eyes[e]==gaze.binding.eyes[e])fallback[e]=s_motionFaceCurrent.eyeRotation[e];
  }
  auto settings=gaze.settings;
  settings.profile=poser_gaze::ProfileFor(gaze);
  if(s_motionFaceCurrent.active&&s_motionFaceCurrent.gazeCamera&&s_motionFaceCurrent.animator==SMCAnimator()&&
     s_motionFaceCurrent.generation==s_faceGeneration) {
    settings.mode=eye_gaze::Mode::Camera;settings.strength=s_motionFaceCurrent.gazeStrength;
  }
  poser_gaze::Update(SMCFrozen()&&s_faceBonesCaptured&&!s_captureNeutral&&s_smcOwnershipVerified,
                     s_faceGeneration,haveFallback?fallback:nullptr,gaze,SMCAnimator(),&settings);
}
static void SMCGazeCameraTick(void *camera) {
  poser_gaze::SetCamera(camera);
  {SMCActorScope scope(&s_editorSMC);SMCGazeTick();}
  for(auto actor:s_squadSMC)if(actor&&actor!=&s_editorSMC) {SMCActorScope scope(actor);SMCGazeTick();}
}
static void SMCFaceBind() {
  if(!s_faceBonesCaptured||s_captureNeutral||s_faceBoneCount<=0||
      SMCBones().empty()||s_faceBindingRevision==SMCRevision())return;
  s_faceBindingRevision=SMCRevision();s_faceNodes.clear();s_characterBinding={};s_characterBindingGeneration=0;s_faceHierarchy={};s_faceRegions.fill(-1);
  try {
    const int count=s_faceBoneCount;
    std::vector<face_geometry::Bone> nodes(count);
    std::vector<Vec3> scales(count);
    std::map<void *,int> slots,all;
    std::map<void *,mmd::Matrix> external;
    for(int i=0;i<count;++i)if(s_faceRestPose[i].transform)slots[s_faceRestPose[i].transform]=i;
    for(int i=0;i<int(SMCBones().size());++i)all[SMCBones()[i].transform]=i;
    std::vector<void *> parents(count,nullptr);
    for(int i=0;i<count;++i) {
      auto t=s_faceRestPose[i].transform;auto found=all.find(t);
      void *parent=nullptr;
      if(found!=all.end()) {
        const auto &bone=SMCBones()[found->second];nodes[i].name=bone.name;
        parent=bone.parent;
      } else {
        char name[128]={};
        if(!SMCFaceIdentity(t,name,&parent)) {
          // Virtual or absent channels do not invalidate the real facial bones.
          nodes[i].name.clear();scales[i]={1,1,1};continue;
        }
        nodes[i].name=name;
      }
      s_faceRegions[i]=face_mixing::BoneRegion(nodes[i].name);
      parents[i]=parent;
    }
    // Native regional ownership needs names only, even if template geometry
    // cannot be built. Finish classification before attempting matrix reads.
    Vec3 origin;bool haveOrigin=false;
    for(int i=0;i<count;++i) {
      void *parent=parents[i],*t=s_faceRestPose[i].transform;
      if(!parent){nodes[i].name.clear();scales[i]={1,1,1};continue;}
      auto slot=slots.find(parent);
      if(slot!=slots.end())nodes[i].parent=slot->second;
      else {
        auto it=external.find(parent);
        if(it==external.end()) {
          mmd::Matrix mat;if(!SMCFaceMatrix(parent,&mat))return;
          if(!haveOrigin){origin=mat.position();haveOrigin=true;}
          mat.m[12]-=origin.x;mat.m[13]-=origin.y;mat.m[14]-=origin.z;
          it=external.emplace(parent,mat).first;
        }
        nodes[i].parentNeutral=it->second;
      }
      scales[i]=SMCFaceScale(t);
    }
    std::vector<int> state(count,0);
    std::function<bool(int)> visit=[&](int i) {
      if(state[i]==2)return true;
      if(state[i]==1)return false;
      state[i]=1;
      int p=nodes[i].parent;
      if(p>=0){if(!visit(p))return false;nodes[i].parentNeutral=nodes[p].neutral;}
      const auto &v=s_faceRestPose[i];
      nodes[i].neutral=nodes[i].parentNeutral*mmd::TRS({v.px,v.py,v.pz},{v.rx,v.ry,v.rz,v.rw},scales[i]);
      state[i]=2;return true;
    };
    for(int i=0;i<count;++i)if(!visit(i))return;
    s_faceNodes=nodes;
    face_mixing::Pose rest;
    for(int i=0;i<count;++i) {
      const auto &v=s_faceRestPose[i];
      rest[i]={{v.px,v.py,v.pz},{v.rx,v.ry,v.rz,v.rw}};
    }
    s_faceHierarchy=face_mixing::BindHierarchy(nodes,rest,scales);
    if(s_faceHierarchy.ready)SMCGazeBind(origin,parents);
    if(s_faceHierarchy.ready)s_faceRegions=s_faceHierarchy.region;
    Log("[FACE] neutral hierarchy: ready=%d bones=%d revision=%d",s_faceHierarchy.ready,count,SMCRevision());
    SMCFaceSelectProfile(s_characterProfile,s_characterModel);
  } catch(...) {s_faceNodes.clear();s_characterBinding={};s_faceHierarchy={};Log("[FACE] neutral binding failed");}
}
static void SMCFaceSelectProfile(std::shared_ptr<const character_face::Profile> profile,const std::string &model) {
  auto &gaze=SMCGazeContext();gaze.modelKey=character_face::ModelKey(model);
  if(profile==s_characterProfile&&model==s_characterModel&&s_characterBindingGeneration==s_faceGeneration)return;
  s_characterProfile=std::move(profile);s_characterModel=model;s_characterBinding={};
  if(!s_faceHierarchy.ready){s_characterBindingGeneration=0;return;}
  s_characterBindingGeneration=s_faceGeneration;
  if(s_characterProfile) {
    s_characterBinding=character_face::Bind(*s_characterProfile,s_characterModel,s_faceNodes,s_faceHierarchy);
    Log("[FACE] character calibration: model=%s ready=%d matched=%d usable=%d error=%.5f",
      s_characterProfile->key.c_str(),s_characterBinding.ready,s_characterBinding.matched,
      s_characterBinding.usableCount,s_characterBinding.error);
  }
  const auto *reference=s_characterProfile&&s_characterBinding.ready?&s_characterProfile->gaze:nullptr;
  auto forward=reference?gaze.binding.neutralHeadInverse*face_geometry::Vector(s_characterBinding.basis,reference->forward):Vec3{};
  poser_gaze::ApplyReference(gaze,reference,forward);
}

static bool SMCMotionActive() {
  return s_motionFaceCurrent.active&&s_motionFaceSaved&&
      s_motionFaceCurrent.generation==s_faceGeneration;
}
// Same fixed EIEM channel table as the manual SMC panel. Normalize the raw
// vowel mix first, then apply regional strength to the resulting bone deltas;
// otherwise 200% mouth strength would be normalized back to 100%.
struct SMCMotionNativeDelta {Vec3 position,rotation;};
using SMCMotionNativeDeltaArray=std::array<SMCMotionNativeDelta,SMC_MAX_FACE_BONES>;
static bool SMCMotionNativeDeltas(const float *weights,SMCMotionNativeDeltaArray &deltas) {
  if(!s_boneMapReady||s_boneIDMapCount<=0||s_capturedLen<=0||!s_mouthResolved)return false;
  __try {
    float total=0;for(int c=0;c<SMC_NUM_MOUTH;++c)total+=face_geometry::Clamp(weights[c],0,1);
    float norm=1.f/(std::max)(1.f,total);
    Vec3 positions[SMC_MAX_FACE_BONES]={},rotations[SMC_MAX_FACE_BONES]={};
    for(int c=0;c<SMC_NUM_MOUTH+s_extraMorphCount;++c) {
      bool mouth=c<SMC_NUM_MOUTH;
      float weight=face_geometry::Clamp(weights[c],0,1)*(mouth?norm:1.f);
      if(weight<.001f||(!mouth&&!s_extraMorphsResolved))continue;
      int targets=mouth?1:s_extraMorphs[c-SMC_NUM_MOUTH].targetCount;
      for(int t=0;t<targets;++t) {
        int start,count;bool eye=false;
        if(mouth) {start=s_mouthShapes[c].jobStartIdx;count=s_mouthShapes[c].jobCount;}
        else {
          const auto &target=s_extraMorphs[c-SMC_NUM_MOUTH].targets[t];
          if(!target.resolved)continue;
          start=target.startIdx;count=target.count;eye=strncmp(target.endfieldName,"eye_",4)==0;
        }
        if(start<0||count<0||start>s_capturedLen||count>s_capturedLen-start)continue;
        for(int j=start;j<start+count;++j) {
          const auto &entry=s_capturedExpression[j];
          int i=entry.boneID>=0&&entry.boneID<SMC_BONE_MAP_SIZE?s_boneIDToIdx[entry.boneID]:-1;
          if(i<0||i>=s_faceBoneCount)continue;
          Vec3 p{entry.deltaPosX,entry.deltaPosY,entry.deltaPosZ},r{entry.deltaRotX,entry.deltaRotY,entry.deltaRotZ};
          if(!std::isfinite(Len(p))||fabsf(p.x)>1||fabsf(p.y)>1||fabsf(p.z)>1)continue;
          positions[i]=positions[i]+p*weight;
          if(!eye&&std::isfinite(Len(r))&&(!mouth||(fabsf(r.x)<30&&fabsf(r.y)<30&&fabsf(r.z)<30)))
            rotations[i]=rotations[i]+r*weight;
        }
      }
    }
    for(int i=0;i<s_faceBoneCount;++i) {
      deltas[i].position=positions[i];
      // Preserve Euler increments until regional strength is applied, as in
      // the original game mapping (not a quaternion component scale).
      deltas[i].rotation=rotations[i];
    }
    return true;
  } __except(1) {return false;}
}
static void SMCMotionEvaluate() {
  s_faceBoneEvalOk=false;
  if(!SMCMotionActive()||!s_faceBonesCaptured||s_captureNeutral)return;
  const auto &frame=s_motionFaceCurrent;const auto &settings=frame.settings;
  face_mixing::Pose rest;
  SMCMotionNativeDeltaArray nativeDeltas{},fallbackDeltas{};
  for(int i=0;i<s_faceBoneCount;++i) {
    const auto &v=s_faceRestPose[i];rest[i]={{v.px,v.py,v.pz},{v.rx,v.ry,v.rz,v.rw}};
  }
  if(settings.uses(face_mixing::Driver::Game))SMCMotionNativeDeltas(frame.weights,nativeDeltas);
  if(settings.fallback&&settings.uses(face_mixing::Driver::Character))SMCMotionNativeDeltas(frame.fallbackWeights,fallbackDeltas);
  bool characterReady=frame.profile&&frame.profile==s_characterProfile&&s_characterBinding.ready&&
      s_characterBindingGeneration==s_faceGeneration;
  std::array<face_mixing::Pose,face_mixing::RegionCount> complete;
  bool identical=true;
  for(int r=0;r<face_mixing::RegionCount;++r) {
    float amount=settings.amount(r);int reuse=-1;
    for(int j=0;j<r;++j)if(settings.driver[j]==settings.driver[r]&&settings.amount(j)==amount){reuse=j;break;}
    if(reuse>=0){complete[r]=complete[reuse];continue;}
    if(r)identical=false;
    complete[r]=rest;
    if(settings.driver[r]==face_mixing::Driver::Disabled)continue;
    if(settings.driver[r]==face_mixing::Driver::Character&&characterReady)
      if(!character_face::Evaluate(*frame.profile,s_characterBinding,s_faceHierarchy,frame.expressions,amount,complete[r]))return;
    const auto &deltas=settings.driver[r]==face_mixing::Driver::Game?nativeDeltas:fallbackDeltas;
    for(int i=0;i<s_faceBoneCount;++i) {
      const auto &d=deltas[i];complete[r][i].position=complete[r][i].position+d.position*amount;
      complete[r][i].rotation=NormQ(Quat::FromEulerDeg(d.rotation*amount)*complete[r][i].rotation);
    }
  }
  face_mixing::Pose output;
  if(identical)output=complete[0];
  else {
    auto fallback=rest;float amount=settings.amount(-1);
    for(int i=0;i<s_faceBoneCount;++i) {
      fallback[i].position=rest[i].position+nativeDeltas[i].position*amount;
      fallback[i].rotation=NormQ(Quat::FromEulerDeg(nativeDeltas[i].rotation*amount)*rest[i].rotation);
    }
    if(!face_mixing::Compose(s_faceHierarchy,complete,fallback,output))return;
  }
  memcpy(s_faceBones,s_faceRestPose,sizeof(s_faceBones));
  for(int i=0;i<s_faceBoneCount;++i) {
    const auto &v=output[i];auto &bone=s_faceBones[i];bone.px=v.position.x;bone.py=v.position.y;bone.pz=v.position.z;
    bone.rx=v.rotation.x;bone.ry=v.rotation.y;bone.rz=v.rotation.z;bone.rw=v.rotation.w;
  }
  s_faceBoneEvalOk=true;
}
static void SMCWriteTouchedBones() {
  if (!s_faceBoneEvalOk)
    return; // 本帧没成功重算，别写回陈旧值
  __try {
    for (int i = 0; i < s_faceBoneCount; i++) {
      if (!s_faceBones[i].transform)
        continue;
      bool motionEye=false;
      if(s_motionFaceCurrent.active && s_motionFaceCurrent.animator==SMCAnimator())
        for(int e=0;e<2;e++) if(s_motionFaceCurrent.eyeDriven[e] && s_motionFaceCurrent.eyes[e]==s_faceBones[i].transform) {
          void *args[] = {&s_motionFaceCurrent.eyeRotation[e]};
          if(UnityObjAlive(s_faceBones[i].transform))
            Invoke(g_transform_set_localRotation,s_faceBones[i].transform,args);
          motionEye=true;break;
        }
      if(motionEye)continue;
      // This hook runs on the game's SMC thread. Automatic facial writes must
      // not invoke the editor's manual-write observers: those traverse and edit
      // the GUI-owned humanoid/accessory vectors while Stop can replace them.
      if (!UnityObjAlive(s_faceBones[i].transform)) continue;
      Vec3 p{s_faceBones[i].px,s_faceBones[i].py,s_faceBones[i].pz};
      Quat q{s_faceBones[i].rx,s_faceBones[i].ry,s_faceBones[i].rz,s_faceBones[i].rw};
      void *pp[] = {&p};
      void *qp[] = {&q};
      Invoke(g_transform_set_localPosition,s_faceBones[i].transform,pp);
      Invoke(g_transform_set_localRotation,s_faceBones[i].transform,qp);
    }
  } __except (1) {
  }
}

// SkeletalMorphCore.Update：初始化（偏移/口型/大列表/骨骼静息位姿/骨映射），
// 之后每帧按面板权重累加增量，在原始 Update 之后覆盖写回
static void __fastcall SMCUpdateBody(void *__this, float deltaTime,
                                       void *methodInfo) {
  // A paused/photo actor may not schedule a new morph job immediately after
  // switching. Its Update can identify the same exact bone owner as the job.
  SMCSelectCandidate(__this);
  if (!s_smcCore) {
    if (s_confirmedSMC && __this == s_confirmedSMC) {
      s_smcCore = __this;
      s_frame = 0;
      s_faceBoneRefs = nullptr;
      s_faceBonesCaptured = false;
      Log("[SMC] Locked SMC core: %p", __this);
    } else {
      if (s_origSMCUpdate)
        s_origSMCUpdate(__this, deltaTime, methodInfo);
      return;
    }
  }
  if (__this != s_smcCore) {
    if (s_origSMCUpdate)
      s_origSMCUpdate(__this, deltaTime, methodInfo);
    return;
  }
  s_frame++;
  if (!SMCFrozen() && s_wasFrozen) SMCReleaseFreeze();
  s_wasFrozen=SMCFrozen();

  // Verify before neutral capture or any face write, including squad contexts.
  if (s_smcCheckedInstance != __this) {
    s_smcCheckedInstance = __this;
      char detail[160];
      char owner[128];
      char current[128];
      int verdict = SMCOwnershipTest(__this, detail, sizeof(detail));
      s_smcOwnershipVerified = verdict == 1;
      if (verdict != 1) {
        SMCFirstBoneName(__this, owner, sizeof(owner));
        CharGoName(current, sizeof(current));
        Log("[SMC] reject instance %p bone0='%s' current='%s': %s", __this,
            owner, current, detail);
        if(verdict==0)SMCRejectInstanceAndDropLock(__this);
        else SMCDropLockKeepWeights();
        if (s_origSMCUpdate)
          s_origSMCUpdate(__this, deltaTime, methodInfo);
        return; // 本帧不驱动，等新候选
      }
      if (verdict == 1) {
        SMCFirstBoneName(__this, owner, sizeof(owner));
        CharGoName(current, sizeof(current));
        Log("[SMC] accepted instance %p bone0='%s' current='%s': %s", __this,
            owner, current, detail);
      }
  }
  if (s_frame == 1) {
    ResolveSMCOffsets(s_smcClass);
    ResolveSMCMouthShapes((char *)__this);
    SMCCaptureBigList(__this);
  }

  // 驱动中：先写上一帧结果，抵消游戏本帧改写。
  // 必须限定在冻结态：hook 是启动时就装上的，解冻后若继续写，会永久盖住游戏的面部动画。
  if (s_driving && SMCFrozen() && s_faceBonesCaptured && s_frame > 5)
    SMCWriteTouchedBones();
  SMCGazeTick();

  // 面部骨骼引用（m_allBonesTransforms，一次性）
  if (!s_faceBoneRefs && s_frame >= 1) {
    __try {
      int btOff =
          SafeOff(s_offAllBonesTransforms, 0x60, "smc.allBonesTransforms");
      void *bonesArr = *(void **)((char *)__this + btOff);
      if (bonesArr) {
        int boneLen = *(int *)((char *)bonesArr + IL2CPP_ARRAY_LEN);
        if (boneLen > 0 && boneLen <= SMC_MAX_FACE_BONES) {
          s_faceBoneRefs = (void **)((char *)bonesArr + IL2CPP_ARRAY_DATA);
          s_faceBoneCount = boneLen;
          Log("[SMC] Bone refs resolved: %d", boneLen);
        }
      }
    } __except (1) {
    }
  }

  // 中性基线：不再"首帧即静息"（那会把锁定时正在演的表情当成默认值）。
  // 冻结后先记下当前脸，再让 job hook 把 morph 增量清零几帧，游戏会把骨骼写回
  // 中性位姿，那时抓到的才是默认值；滑条 0 = 中性脸（对应身体的 A-pose 基准）。
  if (s_faceBoneRefs && !s_faceBonesCaptured && s_bigListCaptured &&
      !s_captureNeutral) {
    s_captureNeutral = true;
    s_neutralFrames = 0;
    Log("[SMC] neutral baseline: sampling (morph deltas zeroed for a few "
        "frames) ...");
  }

  // 冻结瞬间：保住当前表情。用上一帧（未冻结）的脸反解成滑条权重，
  // 拟合不出来的部分折进基准位姿 —— 冻结不当场改变脸，滑条也仍是"相对默认值"。
  if (!SMCFrozen()) {
    s_holdApplied = false;
  } else if (s_faceBonesCaptured && s_driveBaseReady && !s_holdApplied &&
             !s_captureNeutral) {
    s_holdApplied = true;
    if (!s_lastFacePoseValid)
      SMCSnapshotFacePose();
    if (s_lastFacePoseValid)
      SMCHoldPoseAsWeights(s_lastFacePoseBuf);
  }

  // 骨 ID → 骨骼数组下标映射（第 20 帧后游戏数据就绪）
  if (s_bigListCaptured && !s_boneMapReady && s_frame >= 20) {
    memset(s_boneIDToIdx, -1, sizeof(s_boneIDToIdx));
    __try {
      int biOff = SafeOff(s_offBoneIDToIdx, 0xE0, "smc.boneIDToIdx");
      void *buf = *(void **)((char *)__this + biOff);
      int len = *(int *)((char *)__this + biOff + 8);
      if (buf && len > 0 && len < 500) {
        int *data = (int *)buf;
        bool allNeg = true;
        for (int i = 0; i < 8 && i < len * 2; i++) {
          if (data[i] != -1) {
            allNeg = false;
            break;
          }
        }
        int mapped = 0;
        if (!allNeg) { // 成对数组：boneID, arrIdx
          for (int i = 0; i < len; i++) {
            int boneID = data[i * 2];
            int arrIdx = data[i * 2 + 1];
            if (boneID >= 0 && boneID < SMC_BONE_MAP_SIZE && arrIdx >= 0 &&
                arrIdx < SMC_MAX_FACE_BONES) {
              s_boneIDToIdx[boneID] = arrIdx;
              mapped++;
            }
          }
        } else { // 扁平查找数组：idx → arrIdx
          for (int i = 0; i < len; i++) {
            if (data[i] >= 0 && data[i] < SMC_MAX_FACE_BONES) {
              s_boneIDToIdx[i] = data[i];
              mapped++;
            }
          }
        }
        s_boneIDMapCount = mapped;
        s_boneMapReady = (mapped > 0);
        Log("[SMC] Bone map: mapped %d bones (%s)", mapped,
            allNeg ? "flat" : "pair");
        if (!s_boneMapReady) {
          if (s_offAllMorphBoneDirty > 0)
            *(bool *)((char *)__this + s_offAllMorphBoneDirty) = true;
        }
      }
    } __except (1) {
    }
  }

  SMCFaceBind();
  SMCMotionConsume();
  s_faceBoneEvalOk=false;
  if(SMCMotionActive())SMCMotionEvaluate();
  // 按面板权重累加 morph 增量到静息位姿
  else if (s_driving && s_boneMapReady && s_boneIDMapCount > 0 &&
      s_capturedLen > 0 && s_mouthResolved) {
    s_faceBoneEvalOk = false;
    __try {
      float totalMouth = 0;
      for (int s = 0; s < SMC_NUM_MOUTH; s++)
        totalMouth += s_mouthWeights[s];
      if (totalMouth > 1.0f) {
        float scale = 1.0f / totalMouth;
        for (int s = 0; s < SMC_NUM_MOUTH; s++)
          s_mouthWeights[s] *= scale;
      }

      const SMCFaceBone *basePose = s_driveBaseReady ? s_driveBase : s_faceRestPose;
      memcpy(s_faceBones, basePose, sizeof(s_faceBones));
      float deltaPosAccum[SMC_MAX_FACE_BONES][3] = {};
      float deltaRotAccum[SMC_MAX_FACE_BONES][3] = {};
      memset(s_faceBoneTouched, 0, sizeof(s_faceBoneTouched));
      int applied = 0;

      for (int s = 0; s < SMC_NUM_MOUTH; s++) {
        float w = s_mouthWeights[s];
        if (w < 0.001f)
          continue;
        int start = s_mouthShapes[s].jobStartIdx;
        int cnt = s_mouthShapes[s].jobCount;
        if (start < 0 || start + cnt > s_capturedLen)
          continue;
        for (int i = start; i < start + cnt; i++) {
          const SMCMorphBoneEntry &e = s_capturedExpression[i];
          int arrIdx =
              (e.boneID >= 0 && e.boneID < SMC_BONE_MAP_SIZE)
                  ? s_boneIDToIdx[e.boneID]
                  : -1;
          if (arrIdx < 0 || arrIdx >= s_faceBoneCount)
            continue;
          if (fabsf(e.deltaPosX) > 1.0f || fabsf(e.deltaPosY) > 1.0f ||
              fabsf(e.deltaPosZ) > 1.0f)
            continue;
          deltaPosAccum[arrIdx][0] += e.deltaPosX * w;
          deltaPosAccum[arrIdx][1] += e.deltaPosY * w;
          deltaPosAccum[arrIdx][2] += e.deltaPosZ * w;
          if (fabsf(e.deltaRotX) < 30.0f && fabsf(e.deltaRotY) < 30.0f &&
              fabsf(e.deltaRotZ) < 30.0f) {
            deltaRotAccum[arrIdx][0] += e.deltaRotX * w;
            deltaRotAccum[arrIdx][1] += e.deltaRotY * w;
            deltaRotAccum[arrIdx][2] += e.deltaRotZ * w;
          }
          s_faceBoneTouched[arrIdx] = true;
          applied++;
        }
      }

      if (s_extraMorphsResolved) {
        for (int em = 0; em < s_extraMorphCount; em++) {
          float w = s_extraMorphs[em].weight;
          if (w < 0.001f)
            continue;
          for (int t = 0; t < s_extraMorphs[em].targetCount; t++) {
            const SMCExtraTarget &tgt = s_extraMorphs[em].targets[t];
            if (!tgt.resolved || tgt.startIdx < 0 ||
                tgt.startIdx + tgt.count > s_capturedLen)
              continue;
            bool isEyeMorph = (strncmp(tgt.endfieldName, "eye_", 4) == 0);
            for (int i = tgt.startIdx; i < tgt.startIdx + tgt.count; i++) {
              const SMCMorphBoneEntry &e = s_capturedExpression[i];
              int arrIdx =
                  (e.boneID >= 0 && e.boneID < SMC_BONE_MAP_SIZE)
                      ? s_boneIDToIdx[e.boneID]
                      : -1;
              if (arrIdx < 0 || arrIdx >= s_faceBoneCount)
                continue;
              if (fabsf(e.deltaPosX) > 1.0f || fabsf(e.deltaPosY) > 1.0f ||
                  fabsf(e.deltaPosZ) > 1.0f)
                continue;
              deltaPosAccum[arrIdx][0] += e.deltaPosX * w;
              deltaPosAccum[arrIdx][1] += e.deltaPosY * w;
              deltaPosAccum[arrIdx][2] += e.deltaPosZ * w;
              if (!isEyeMorph) { // 眼部 morph 只做位移，避免眼珠旋转打架
                deltaRotAccum[arrIdx][0] += e.deltaRotX * w;
                deltaRotAccum[arrIdx][1] += e.deltaRotY * w;
                deltaRotAccum[arrIdx][2] += e.deltaRotZ * w;
              }
              s_faceBoneTouched[arrIdx] = true;
              applied++;
            }
          }
        }
      }

      for (int b = 0; b < s_faceBoneCount; b++) {
        if (!s_faceBoneTouched[b])
          continue;
        s_faceBones[b].px = basePose[b].px + deltaPosAccum[b][0];
        s_faceBones[b].py = basePose[b].py + deltaPosAccum[b][1];
        s_faceBones[b].pz = basePose[b].pz + deltaPosAccum[b][2];
        Quat dq = Quat::FromEulerDeg(
            Vec3(deltaRotAccum[b][0], deltaRotAccum[b][1],
                 deltaRotAccum[b][2]));
        Quat rq(basePose[b].rx, basePose[b].ry, basePose[b].rz,
                basePose[b].rw);
        Quat res = dq * rq;
        float len = sqrtf(res.x * res.x + res.y * res.y + res.z * res.z +
                          res.w * res.w);
        if (len > 1e-6f) {
          res.x /= len;
          res.y /= len;
          res.z /= len;
          res.w /= len;
        }
        s_faceBones[b].rx = res.x;
        s_faceBones[b].ry = res.y;
        s_faceBones[b].rz = res.z;
        s_faceBones[b].rw = res.w;
      }
      if (applied > 0 && s_frame % 600 == 0)
        Log("[SMC] Applied %d bone deltas (frame %d)", applied, s_frame);
      s_faceBoneEvalOk = true;
    } __except (1) {
      Log("[SMC] delta accumulation exception");
    }
  }

  // Native auto-blinks can also drive eye-fold BlendShapes/material channels.
  // Pausing their source avoids a frozen eyelid with an animated surround.
  SMCAutomation().update(__this,SMCAnimator(),
      SMCFrozen() && s_faceBonesCaptured && !s_captureNeutral,s_smcOwnershipVerified);
  if (s_origSMCUpdate)
    s_origSMCUpdate(__this, deltaTime, methodInfo);

  // 采样到中性脸：把当前（零 morph）位姿定为默认值，权重归零并接管驱动，
  // 这样"滑条全 0 = 中性脸"，不会再出现"滑条 0 但脸还在笑"
  if (s_captureNeutral) {
    s_neutralFrames++;
    if (s_neutralFrames >= 3) {
      int captured = 0;
      for (int i = 0; i < s_faceBoneCount; i++) {
        if (!s_faceBoneRefs[i])
          continue;
        Vec3 p = GetBoneLocalPos(s_faceBoneRefs[i]);
        Quat r = GetBoneLocalRot(s_faceBoneRefs[i]);
        s_faceBones[i] = {p.x, p.y, p.z, r.x, r.y, r.z, r.w,
                          s_faceBoneRefs[i]};
        captured++;
      }
      memcpy(s_faceRestPose, s_faceBones, sizeof(s_faceBones));
      // 默认脸 = 中性脸；驱动基准复位到默认（「全部归零」的目标就是它）
      memcpy(s_driveBase, s_faceRestPose, sizeof(s_driveBase));
      s_driveBaseReady = true;
      float vsFace = s_lastFacePoseValid
                         ? SMCPoseResidual(s_lastFacePoseBuf, s_faceRestPose,
                                           s_faceBoneCount)
                         : -1.0f;
      s_faceBonesCaptured = true;
      s_captureNeutral = false;
      SMCFaceInvalidate();
      for (int i = 0; i < SMC_NUM_MOUTH; i++)
        s_mouthWeights[i] = 0.0f;
      for (int i = 0; i < s_extraMorphCount; i++) {
        s_extraMorphs[i].weight = 0.0f;
        s_extraMorphs[i].prevWeight = 0.0f;
      }
      s_driving = true;
      SMCRestoreBigList();
      Log("[SMC] neutral baseline captured: %d bones (distance to current "
          "face=%.4f) -> default face = neutral",
          captured, vsFace);
    }
  }

  // 覆盖写回（原始 Update 之后）
  if (s_driving && SMCFrozen() && s_faceBonesCaptured && s_frame > 5)
    SMCWriteTouchedBones();

  // 未冻结时隔帧记录"当前脸"，供冻结瞬间反解使用（解冻后脸由游戏/动画驱动）
  if (!SMCFrozen() && !s_captureNeutral) {
    static int s_snapTick = 0;
    if ((s_snapTick++ & 1) == 0)
      SMCSnapshotFacePose();
  }
}

// Reset/capture uses the pose lock too. Never block a Unity callback waiting
// for a worker's IL2CPP invocation; busy callbacks run the original game code.
static void (*g_characterFramePulse)() = nullptr;
static void __fastcall HookedSMCUpdate(void *self, float dt, void *method) {
  if (RuntimeClosing()) {if(s_origSMCUpdate)s_origSMCUpdate(self,dt,method);return;}
  std::unique_lock<std::recursive_mutex> lock(g_poseMutex, std::try_to_lock);
  if (RuntimeClosing() || !poser_agreement::Allowed() || !g_editorEnabled.load() || !lock.owns_lock() ||
      CharacterSwitchInProgress() || CharacterSelectionPending()) {
    if (s_origSMCUpdate) s_origSMCUpdate(self, dt, method);
    return;
  }
  // This callback keeps running when Camera.onPreCull/SRP callbacks are absent.
  // The selected SMC identity is read while holding the pose lock.
  if (self && self==s_smcCore && g_characterFramePulse) g_characterFramePulse();
  SMCActorScope actor(SMCFindActor(self));
  SMCUpdateBody(self, dt, method);
}

static bool SMCJobValueType(void *type) {
  if (!type || !il2cpp_type_get_type || !il2cpp_class_from_type ||
      !il2cpp_class_value_size || il2cpp_type_get_type(type) != 0x11)
    return false; // IL2CPP_TYPE_VALUETYPE
  void *klass = il2cpp_class_from_type(type);
  if (!klass)
    return false;
  uint32_t alignment = 0;
  // The engine can wrap Unity's job handle. We never inspect its fields: the
  // value kind/size determines this native ABI, not its managed name/namespace.
  return il2cpp_class_value_size(klass, &alignment) == 16;
}

static bool SMCValidateHookSignatures(void *update, void *job) {
  if (!update || !job || sizeof(void *) != 8 ||
      !il2cpp_method_get_flags || !il2cpp_method_get_param_count ||
      !il2cpp_method_get_return_type || !il2cpp_method_get_param ||
      !il2cpp_type_get_type)
    return false;
  uint32_t flags = 0;
  if ((il2cpp_method_get_flags(update, &flags) & 0x10) ||
      (il2cpp_method_get_flags(job, &flags) & 0x10) ||
      il2cpp_method_get_param_count(update) != 1 ||
      il2cpp_method_get_param_count(job) != 2)
    return false; // both must be instance methods
  void *updateReturn = il2cpp_method_get_return_type(update);
  void *deltaTime = il2cpp_method_get_param(update, 0);
  void *count = il2cpp_method_get_param(job, 0);
  return updateReturn && deltaTime && count &&
         il2cpp_type_get_type(updateReturn) == 1 && // void
         il2cpp_type_get_type(deltaTime) == 0xc &&  // float
         (il2cpp_type_get_type(count) == 8 ||       // int32 / uint32 share
          il2cpp_type_get_type(count) == 9) &&      // the same integer ABI
         SMCJobValueType(il2cpp_method_get_return_type(job)) &&
         SMCJobValueType(il2cpp_method_get_param(job, 1));
}

static void SMCLogHookMethod(void *method, const char *label) {
  if (!method || !il2cpp_method_get_flags || !il2cpp_method_get_param_count ||
      !il2cpp_method_get_return_type || !il2cpp_method_get_param ||
      !il2cpp_type_get_type || !il2cpp_class_from_type ||
      !il2cpp_class_get_name || !il2cpp_class_get_namespace)
    return;
  uint32_t impl = 0;
  uint32_t count = il2cpp_method_get_param_count(method);
  Log("[SMC] ABI %s: flags=0x%x params=%u", label,
      il2cpp_method_get_flags(method, &impl), count);
  for (int i = -1; i < static_cast<int>(count) && i < 4; ++i) {
    void *type = i < 0 ? il2cpp_method_get_return_type(method)
                      : il2cpp_method_get_param(method, i);
    if (!type)
      continue;
    int kind = il2cpp_type_get_type(type);
    void *klass = il2cpp_class_from_type(type);
    const char *name = klass ? il2cpp_class_get_name(klass) : nullptr;
    const char *space = klass ? il2cpp_class_get_namespace(klass) : nullptr;
    uint32_t alignment = 0;
    int size = klass && kind == 0x11 && il2cpp_class_value_size
                   ? il2cpp_class_value_size(klass, &alignment)
                   : -1;
    Log("[SMC] ABI %s slot=%d type=0x%x %s.%s valueBytes=%d", label, i,
        kind, space ? space : "?", name ? name : "?", size);
  }
}

// 安装 SMC hook（IL2CPP Resolve 成功后调用一次）
static void InstallSMCFaceHooks() {
  __try {
    size_t ac = 0;
    void **asms = nullptr;
    void *domain = il2cpp_domain_get();
    if (domain)
      asms = il2cpp_domain_get_assemblies(domain, &ac);

    void *smcClass =
        FindClass("Beyond.Gameplay.View.SkeletalMorph", "SkeletalMorphCore",
                  asms, ac);
    if (!smcClass)
      smcClass = FindClass("Beyond.Gameplay.View", "SkeletalMorphCore", asms,
                           ac);
    if (!smcClass)
      smcClass = FindClass("Beyond.Gameplay.Core", "SkeletalMorphCore", asms,
                           ac);
    if (!smcClass) {
      Log("[SMC] SkeletalMorphCore class not found (SMC face morph disabled)");
      return;
    }
    s_smcClass = smcClass;
    Log("[SMC] SkeletalMorphCore class found");

    void *updateMethod = FindMethod(smcClass, "Update", 1);
    void *jobMethod = FindMethod(smcClass, "DoEvaluateMorphToBoneJob", 2);
    SMCLogHookMethod(updateMethod, "Update");
    SMCLogHookMethod(jobMethod, "MorphJob");
    if (!SMCValidateHookSignatures(updateMethod, jobMethod)) {
      Log("[SMC] Callback signature mismatch; facial hooks disabled");
      return;
    }
    Log("[SMC] Callback ABI verified: instance Update(float), "
        "value16 MorphJob(count32, value16)");
    Hook(updateMethod, "SkeletalMorphCore.Update", (void *)HookedSMCUpdate,
         (void **)&s_origSMCUpdate);
    Hook(jobMethod, "DoEvaluateMorphToBoneJob", (void *)HookedSMCMorphJob,
         (void **)&s_origMorphJob);
    // SpecialMorphJob was a no-op interceptor. Leave its native ABI untouched.
  } __except (1) {
    Log("[SMC] InstallSMCFaceHooks exception");
  }
}

// 角色切换 / 停止驱动时重置（把大列表还回游戏）
static void ResetSMCState(bool restoreOriginal = true) {
  poser_gaze::Reset(restoreOriginal,SMCGazeContext());
  SMCAutomation().release(restoreOriginal);
  s_wasFrozen = false;
  SMCFaceInvalidate();
  if (restoreOriginal) SMCRestoreBigList();
  s_smcCore = nullptr;
  s_confirmedSMC = nullptr;
  s_frame = 0;
  s_bigListCaptured = false;
  s_capturedLen = 0;
  s_faceBoneRefs = nullptr;
  s_faceBoneCount = 0;
  s_faceBonesCaptured = false;
  s_charBoneXformCount = 0;
  s_charBoneXformRev = -1;
  s_captureNeutral = false;
  s_neutralFrames = 0;
  s_driveBaseReady = false;
  s_lastFacePoseValid = false;
  s_holdApplied = false;
  for (int i = 0; i < SMC_MAX_SLIDERS; i++)
    s_fitW[i] = 0.0f;
  s_boneMapReady = false;
  s_boneIDMapCount = 0;
  s_smcOwnershipVerified = false;
  s_smcCheckedInstance = nullptr;
  s_smcRejectedCount = 0;
  s_smcRejectStrikes = 0;
  memset(s_boneIDToIdx, -1, sizeof(s_boneIDToIdx));
  memset(s_faceBoneTouched, 0, sizeof(s_faceBoneTouched));
  for (int i = 0; i < SMC_NUM_MOUTH; i++) {
    s_mouthShapes[i].resolved = false;
    s_mouthWeights[i] = 0.0f;
  }
  for (int i = 0; i < s_extraMorphCount; i++) {
    s_extraMorphs[i].weight = 0.0f;
    s_extraMorphs[i].prevWeight = 0.0f;
    for (int t = 0; t < s_extraMorphs[i].targetCount; t++)
      s_extraMorphs[i].targets[t].resolved = false;
  }
  s_mouthResolved = false;
  s_extraMorphsResolved = false;
  s_driving = false;
  // 上次没探明权重布局的话，换角色后再探一次（探明过就沿用，避免重复刷日志）
  if (!s_allMorphsIsFloatArray && s_allMorphsWeightOff < 0)
    s_morphWeightProbed = false;
  Log("[SMC] state reset");
}

// ---- 面板 API ----
static bool SMCSectionReady() {
  return s_smcCore && s_smcOwnershipVerified && s_bigListCaptured && s_mouthResolved;
}

static bool SMCFaceDriving() { return s_driving; }
static void SMCFaceSetDriving(bool on) {
  if (s_driving == on)
    return;
  if (!on) {
    s_driving = false;
    for (int i = 0; i < SMC_NUM_MOUTH; i++)
      s_mouthWeights[i] = 0.0f;
    for (int i = 0; i < s_extraMorphCount; i++)
      s_extraMorphs[i].weight = 0.0f;
    SMCRestoreBigList();
    Log("[SMC] driving disabled");
  } else {
    s_driving = true;
    Log("[SMC] driving enabled");
  }
}

static int SMCSliderCount() {
  return SMC_NUM_MOUTH + s_extraMorphCount;
}

static const char *SMCSliderLabel(int i) {
  if (i < SMC_NUM_MOUTH)
    return s_mouthShapes[i].name;
  return s_extraMorphs[i - SMC_NUM_MOUTH].label;
}

static float SMCSliderValue(int i) {
  if (i < SMC_NUM_MOUTH)
    return s_mouthWeights[i];
  return s_extraMorphs[i - SMC_NUM_MOUTH].weight;
}

static void SMCSliderSet(int i, float v) {
  if (v < 0.0f)
    v = 0.0f;
  if (v > 1.0f)
    v = 1.0f;
  if (i < SMC_NUM_MOUTH) {
    s_mouthWeights[i] = v;
  } else {
    s_extraMorphs[i - SMC_NUM_MOUTH].weight = v;
    s_extraMorphs[i - SMC_NUM_MOUTH].prevWeight = v;
  }
  s_driving = true;
  Log("[SMC] slider %d '%s' = %.2f (driving=1, frozen=%d, bones=%d)", i,
      SMCSliderLabel(i), v, (int)SMCFrozen(), s_faceBoneCount);
}

// 面板按钮：把游戏当前的 morph 权重读进滑条
// （原来滑条一律从 0 开始，角色脸上正演着的表情看不到；读一次就能接着调）
// 从 MainEmotion._pose 的 MorphCtrlValue 列表读"当前表情"：元素带 _ctrlName/_value，
// 名字就是角色的 morph 控制名，能直接对上滑条目标（多个同名取最大）。返回命中数。
static int SMCReadPoseValues(void *smc, float *exprVals, float *mouthVals) {
  int hits = 0;
  __try {
    int meOff = SafeOff(s_offMainEmotion, 0x3C0, "smc.mainEmotion");
    void *mainEmo = *(void **)((char *)smc + meOff);
    if (!mainEmo)
      return 0;
    void *ec = il2cpp_object_get_class(mainEmo);
    if (!ec)
      return 0;
    const char *poseNames[] = {"_pose", "m_pose", "pose"};
    int poseOff = FindFieldInHierarchy(ec, poseNames, 3, nullptr);
    if (poseOff <= 0)
      return 0;
    void *pose = *(void **)((char *)mainEmo + poseOff);
    if (!pose)
      return 0;
    void *pc = il2cpp_object_get_class(pose);
    if (!pc)
      return 0;
    void *it = nullptr, *f;
    while ((f = il2cpp_class_get_fields(pc, &it))) {
      const char *fn = il2cpp_field_get_name(f);
      int fo = (int)il2cpp_field_get_offset(f);
      const char *ftn = "?";
      if (il2cpp_field_get_type && il2cpp_type_get_name) {
        void *ft = il2cpp_field_get_type(f);
        if (ft)
          ftn = il2cpp_type_get_name(ft);
      }
      if (!fn || !ftn || !strstr(ftn, "List"))
        continue;
      void *lst = *(void **)((char *)pose + fo);
      if (!lst)
        continue;
      int len = *(int *)((char *)lst + IL2CPP_LIST_SIZE);
      void *items = *(void **)((char *)lst + IL2CPP_LIST_ITEMS);
      if (!items || len <= 0 || len > 300)
        continue;
      for (int i = 0; i < len; i++) {
        void *elem = *(void **)((char *)items + IL2CPP_ARRAY_DATA + i * 8);
        if (!elem)
          continue;
        void *ec2 = il2cpp_object_get_class(elem);
        if (!ec2)
          continue;
        const char *nameFields[] = {"_ctrlName", "m_ctrlName", "ctrlName",
                                    "_name"};
        const char *valFields[] = {"_value", "m_value", "value"};
        int no = FindFieldInHierarchy(ec2, nameFields, 4, nullptr);
        int vo = FindFieldInHierarchy(ec2, valFields, 3, nullptr);
        if (no <= 0 || vo <= 0)
          continue;
        void *so = *(void **)((char *)elem + no);
        char nm[96] = "";
        if (so)
          ReadStr(so, nm, sizeof(nm));
        float val = *(float *)((char *)elem + vo);
        if (!nm[0] || !(val == val) || val < -0.05f || val > 1.5f)
          continue;
        bool used = false;
        for (int em = 0; em < s_extraMorphCount; em++) {
          for (int t = 0; t < s_extraMorphs[em].targetCount; t++) {
            const SMCExtraTarget &tgt = s_extraMorphs[em].targets[t];
            if (tgt.resolved && strcmp(nm, tgt.endfieldName) == 0) {
              if (val > exprVals[em])
                exprVals[em] = val;
              used = true;
            }
          }
        }
        for (int m = 0; m < SMC_NUM_MOUTH; m++) {
          if (_stricmp(nm, s_mouthShapes[m].name) == 0) {
            mouthVals[m] = val;
            used = true;
          }
        }
        if (used)
          hits++;
      }
    }
  } __except (1) {
    Log("[SMC] read current: pose list exception");
  }
  return hits;
}



static const char *SMCReadStatusText() { return s_smcReadStatus; }

static void SMCReadCurrentToSliders() {
  void *smc = s_smcCore ? s_smcCore : s_confirmedSMC;
  if (!smc) {
    snprintf(s_smcReadStatus, sizeof(s_smcReadStatus), "%s", "no SMC core");
    Log("[SMC] read current: no locked SMC core");
    return;
  }
  SMCProbeMorphWeights(smc);
  // 游戏里的权重本来就是"相对中性默认值"的，所以先把基准复位再填，
  // 这样读进来等价于"把脸设成游戏当前的表情"
  SMCClearHold();
  int mouths = 0, exprs = 0;
  float exprVals[32] = {};
  float mouthVals[SMC_NUM_MOUTH] = {};
  int poseHits = 0;
  if (s_extraMorphCount <= 32)
    poseHits = SMCReadPoseValues(smc, exprVals, mouthVals);
  const char *src = "pose";
  if (poseHits > 0) {
    for (int i = 0; i < SMC_NUM_MOUTH; i++) {
      if (mouthVals[i] <= 0.0f)
        continue;
      s_mouthWeights[i] = mouthVals[i];
      mouths++;
    }
    for (int em = 0; em < s_extraMorphCount; em++) {
      if (exprVals[em] <= 0.0f)
        continue;
      s_extraMorphs[em].weight = exprVals[em];
      s_extraMorphs[em].prevWeight = exprVals[em];
      exprs++;
    }
  } else {
    // 退回：按 morphId 直接读 m_allMorphs
    src = "allMorphs";
    for (int i = 0; i < SMC_NUM_MOUTH; i++) {
      float w = 0.0f;
      if (s_mouthShapes[i].morphId < 0 ||
          !SMCReadMorphWeight(smc, s_mouthShapes[i].morphId, &w))
        continue;
      if (w < 0.0f)
        w = 0.0f;
      if (w > 1.0f)
        w = 1.0f;
      s_mouthWeights[i] = w;
      mouths++;
    }
    for (int em = 0; em < s_extraMorphCount; em++) {
      float best = 0.0f;
      bool got = false;
      for (int t = 0; t < s_extraMorphs[em].targetCount; t++) {
        const SMCExtraTarget &tgt = s_extraMorphs[em].targets[t];
        if (!tgt.resolved || tgt.morphId < 0)
          continue;
        float w = 0.0f;
        if (!SMCReadMorphWeight(smc, tgt.morphId, &w))
          continue;
        if (w > best)
          best = w;
        got = true;
      }
      if (!got)
        continue;
      if (best < 0.0f)
        best = 0.0f;
      if (best > 1.0f)
        best = 1.0f;
      s_extraMorphs[em].weight = best;
      s_extraMorphs[em].prevWeight = best;
      exprs++;
    }
  }
  char line[192] = {};
  int pos = 0;
  for (int i = 0; i < SMC_NUM_MOUTH && pos < (int)sizeof(line) - 16; i++)
    pos += snprintf(line + pos, sizeof(line) - pos, "%s=%.2f ",
                    s_mouthShapes[i].name, s_mouthWeights[i]);
  Log("[SMC] read current -> src=%s poseHits=%d mouth=%d expr=%d "
      "(allMorphs=%s weightOff=0x%X) mouth values: %s",
      src, poseHits, mouths, exprs,
      s_allMorphsClass[0] ? s_allMorphsClass : "?", s_allMorphsWeightOff,
      line);
  if (mouths + exprs > 0) {
    s_driving = true;
    snprintf(s_smcReadStatus, sizeof(s_smcReadStatus), "%s: %d mouth, %d expr",
             src, mouths, exprs);
  } else {
    snprintf(s_smcReadStatus, sizeof(s_smcReadStatus),
             "%s", "no usable source (see log)");
  }
}

static void SMCRestoreWeights() {
  for (int i = 0; i < SMC_NUM_MOUTH; i++)
    s_mouthWeights[i] = 0.0f;
  for (int i = 0; i < s_extraMorphCount; i++)
    s_extraMorphs[i].weight = 0.0f;
  // 基准也一起复位：归零 = 回到中性默认脸（不是回到当前位置）
  SMCClearHold();
}

// Find by exact actor bones, never by whichever SMC callback arrived first.
static SMCActorState *SMCFindActor(void *core) {
  for(auto state:s_squadSMC) if(state && state->confirmedSMC==core) return state;
  void **bones=nullptr;int count=0;
  bool needed=false;for(auto state:s_squadSMC)needed|=state && !state->confirmedSMC;
  if(!needed || !SMCReadBoneArrayRaw(core,&bones,&count)) return &s_editorSMC;
  for(auto state:s_squadSMC) if(state && !state->confirmedSMC && UnityObjAlive(state->actor)) {
    int matches=0;
    for(int n=0;n<count;++n) for(const auto &bone:state->bones)
      if(bone.transform && bone.transform==bones[n]) {++matches;break;}
    if(matches<3)continue;
    uint32_t handle=il2cpp_gchandle_new?il2cpp_gchandle_new(core,false):0;
    if(!handle)return &s_editorSMC;
    state->retainedCore=handle;state->confirmedSMC=core;
    return state;
  }
  return &s_editorSMC;
}
