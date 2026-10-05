#pragma once

// Task 2.1：捕获主角色 Animator/Entity。
// 通过 MinHook 挂 PlayerController.SetMainCharacter，在角色切换时提取 Entity →
// ComplexAnimationComponent → Animator，存入 g_charAnimator 并置 g_charChanged。
// 同时提供 Humanoid 骨骼句柄封装（GetHumanoidBone / GetBoneLocalRot 等），
// 供后续 FK/IK/快照/从骨（skeleton.h / accessory.h / freeze.h）复用。
//
// 参照 {EIEM}/src/init.h L985-L1121 的 SetMainCharacter hook 逻辑精简而来。

#include "base.h"
#include "il2cpp_api.h"
#include "math/quat_math.h"

#include <cstdint>
#include <cstring>

// ---- Unity HumanBodyBones 枚举（55 根，标准顺序；新版 Unity 含 UpperChest=54）----
enum HumanBodyBones {
  Hips = 0, LeftUpperLeg = 1, RightUpperLeg = 2,
  LeftLowerLeg = 3, RightLowerLeg = 4, LeftFoot = 5, RightFoot = 6,
  Spine = 7, Chest = 8, UpperChest = 54,
  Neck = 9, Head = 10,
  LeftShoulder = 11, RightShoulder = 12,
  LeftUpperArm = 13, RightUpperArm = 14,
  LeftLowerArm = 15, RightLowerArm = 16,
  LeftHand = 17, RightHand = 18,
  LeftToes = 19, RightToes = 20,
  LeftEye = 21, RightEye = 22, Jaw = 23,
  LeftThumbProximal = 24, LeftThumbIntermediate = 25, LeftThumbDistal = 26,
  LeftIndexProximal = 27, LeftIndexIntermediate = 28, LeftIndexDistal = 29,
  LeftMiddleProximal = 30, LeftMiddleIntermediate = 31, LeftMiddleDistal = 32,
  LeftRingProximal = 33, LeftRingIntermediate = 34, LeftRingDistal = 35,
  LeftLittleProximal = 36, LeftLittleIntermediate = 37, LeftLittleDistal = 38,
  RightThumbProximal = 39, RightThumbIntermediate = 40, RightThumbDistal = 41,
  RightIndexProximal = 42, RightIndexIntermediate = 43, RightIndexDistal = 44,
  RightMiddleProximal = 45, RightMiddleIntermediate = 46, RightMiddleDistal = 47,
  RightRingProximal = 48, RightRingIntermediate = 49, RightRingDistal = 50,
  RightLittleProximal = 51, RightLittleIntermediate = 52, RightLittleDistal = 53,
  LastBone = 55,
};
static const int kHumanBoneCount = 55;

static const char *HumanBoneName(int b) {
  static const char *const names[kHumanBoneCount] = {
      "Hips", "LeftUpperLeg", "RightUpperLeg", "LeftLowerLeg", "RightLowerLeg",
      "LeftFoot", "RightFoot", "Spine", "Chest",
      "Neck", "Head", "LeftShoulder", "RightShoulder",
      "LeftUpperArm", "RightUpperArm", "LeftLowerArm", "RightLowerArm",
      "LeftHand", "RightHand", "LeftToes", "RightToes",
      "LeftEye", "RightEye", "Jaw",
      "LeftThumbProximal", "LeftThumbIntermediate", "LeftThumbDistal",
      "LeftIndexProximal", "LeftIndexIntermediate", "LeftIndexDistal",
      "LeftMiddleProximal", "LeftMiddleIntermediate", "LeftMiddleDistal",
      "LeftRingProximal", "LeftRingIntermediate", "LeftRingDistal",
      "LeftLittleProximal", "LeftLittleIntermediate", "LeftLittleDistal",
      "RightThumbProximal", "RightThumbIntermediate", "RightThumbDistal",
      "RightIndexProximal", "RightIndexIntermediate", "RightIndexDistal",
      "RightMiddleProximal", "RightMiddleIntermediate", "RightMiddleDistal",
      "RightRingProximal", "RightRingIntermediate", "RightRingDistal",
      "RightLittleProximal", "RightLittleIntermediate", "RightLittleDistal",
      "UpperChest"};
  if (b < 0 || b >= kHumanBoneCount)
    return "?";
  return names[b];
}

// ---- 全局状态 ----
static void *g_playerController = nullptr;
static void *g_pcClass = nullptr; // PlayerController 类（实例补捞用）
static void *g_mainCharEntity = nullptr;
static void *g_charAnimator = nullptr;
static void *g_charAnimComp = nullptr; // Entity animation record; only toggle if verified as Behaviour.
static void *g_animatorClass = nullptr; // UnityEngine.Animator 类（角色捕获字段扫描用）
static volatile bool g_charChanged = false; // hook 捕获新角色后置真，GUI 消费后复位
// 冻结状态：由 freeze.h 维护；低层模块（如 smc_morph.h 的每帧写回）只读它做闸门，
// 避免解冻后仍然覆盖游戏的动画/表情写入。
static bool g_frozen = false;
static volatile LONG g_characterSwitchDepth = 0;
// Observed on the game's character-selection callback, never on our workers.
static volatile LONG g_gameLogicThreadId = 0;
static bool CharacterSwitchInProgress() {
  return InterlockedCompareExchange(&g_characterSwitchDepth, 0, 0) != 0;
}
// Shared ownership flag: web edits must not race the MMD playback writer.
static volatile LONG g_mmdOwnsPose = 0;
static bool MmdOwnsPose() { return InterlockedCompareExchange(&g_mmdOwnsPose, 0, 0) != 0; }
static void (*g_beforeCharacterChange)(void *nextEntity) = nullptr;
static SRWLOCK g_pendingCharacterLock = SRWLOCK_INIT;
static void *g_pendingController = nullptr;
static void *g_pendingEntity = nullptr;
static bool g_pendingSelection = false;
static void *g_captureEntity = nullptr; // Keep retrying while the model loads.
static bool UnityObjAlive(void *obj);
static void *SafeGetComponentTransform(void *component);
static void ConsumeCapturedCharacter();
// The game hook only publishes selection. All pose/session changes stay on the
// serialized logic thread, after SetMainCharacter has finished initializing it.
static void QueueCharacterSelection(void *controller, void *entity) {
  AcquireSRWLockExclusive(&g_pendingCharacterLock);
  g_pendingController = controller;
  g_pendingEntity = entity;
  g_pendingSelection = true;
  ReleaseSRWLockExclusive(&g_pendingCharacterLock);
}


// 已解析的运行时方法指针（对应 {EIEM} globals.h 的 g_animator_*/g_transform_*）
static void *g_animator_GetBoneTransform = nullptr;
static void *g_animator_get_isHuman = nullptr;
static void *g_animator_get_enabled = nullptr;  // 来自 Behaviour.get_enabled
static void *g_animator_set_enabled = nullptr; // 来自 Behaviour.set_enabled
static void *g_transform_get_localRotation = nullptr;
static void *g_transform_set_localRotation = nullptr;
static void *g_transform_get_localPosition = nullptr;
static void *g_transform_get_localScale = nullptr;
static void *g_transform_set_localPosition = nullptr;
static void *g_transform_get_position = nullptr;
static void *g_transform_set_position = nullptr; // 世界平移（自由相机写）
static void *g_transform_get_rotation = nullptr; // 世界旋转（gizmo 相机朝向用）
static void *g_transform_set_rotation = nullptr; // 世界旋转（自由相机写）
static void *g_transform_get_childCount = nullptr;
static void *g_transform_GetChild = nullptr;
static void *g_transform_get_parent = nullptr;
static void *g_object_get_name = nullptr;
static void *g_component_get_transform = nullptr;
static void *g_object_FindObjectOfType = nullptr; // Object.FindObjectOfType(Type)
static void *g_component_get_gameObject = nullptr;
static void *g_componentClass = nullptr; // UnityEngine.Component（GetComponents(Type) 用）
static void *g_behaviourClass = nullptr;
static void *g_gameObjectClass = nullptr; // UnityEngine.GameObject
static void *g_gameObject_GetComponent = nullptr; // GameObject.GetComponent(Type)
static void *g_gameObject_setActive = nullptr;
static void *g_gameObject_get_activeSelf = nullptr;
static void *g_gameObject_GetComponents = nullptr; // GameObject.GetComponents(Type)
static void *g_cameraClass = nullptr;     // UnityEngine.Camera（get_main 用）
static void *g_camera_get_main = nullptr; // Camera.get_main（gizmo 取视锥）
static void *g_camera_get_fieldOfView = nullptr;
static void *g_camera_set_fieldOfView = nullptr; // Camera.set_fieldOfView（FOV 滑条）
static void *g_camera_get_worldToCameraMatrix = nullptr; // Camera.get_worldToCameraMatrix
static void *g_camera_get_projectionMatrix = nullptr;    // Camera.get_projectionMatrix
static void *g_skinnedMeshRendererClass = nullptr; // UnityEngine.SkinnedMeshRenderer
static void *g_smr_get_sharedMesh = nullptr;        // get_sharedMesh
static void *g_smr_get_bones = nullptr;
static void *g_mesh_get_bindposes = nullptr;
static void *g_transform_get_localToWorldMatrix = nullptr;
static void *g_smr_GetBlendShapeWeight = nullptr;   // GetBlendShapeWeight(int)
static void *g_smr_SetBlendShapeWeight = nullptr;   // SetBlendShapeWeight(int,float)
static void *g_mesh_get_blendShapeCount = nullptr;  // Mesh.get_blendShapeCount
static void *g_mesh_GetBlendShapeName = nullptr;    // Mesh.GetBlendShapeName(int)
static void *g_cursor_set_lockState = nullptr;      // Cursor.set_lockState(enum)
static void *g_cursor_set_visible = nullptr;        // Cursor.set_visible(bool)
static void *g_cursor_get_lockState = nullptr;      // Cursor.get_lockState
static void *g_cursor_get_visible = nullptr;        // Cursor.get_visible

// 动态解析的字段偏移（-1 = 未解析，读时走 SafeOff 回退）
static int OFF_pcEntity = -1;            // PlayerController -> Entity
static int OFF_entityComplexAnim = -1;   // Entity -> ComplexAnimationComponent
static int OFF_complexAnimAnimator = -1; // ComplexAnimationComponent -> Animator

// 运行时方法解析（在 IL2CPP Resolve() 之后调用）
static void ResolveGameApi() {
  __try {
    size_t ac = 0;
    void **asms = nullptr;
    void *domain = il2cpp_domain_get();
    if (domain)
      asms = il2cpp_domain_get_assemblies(domain, &ac);

    void *animClass = FindClass("UnityEngine", "Animator", asms, ac);
    if (animClass) {
      g_animatorClass = animClass;
      g_animator_GetBoneTransform = FindMethod(animClass, "GetBoneTransform", 1);
      g_animator_get_isHuman = FindMethod(animClass, "get_isHuman", 0);
      void *behClass = FindClass("UnityEngine", "Behaviour", asms, ac);
      g_behaviourClass = behClass;
      if (behClass) {
        g_animator_get_enabled = FindMethod(behClass, "get_enabled", 0);
        g_animator_set_enabled = FindMethod(behClass, "set_enabled", 1);
      }
    }

    void *trClass = FindClass("UnityEngine", "Transform", asms, ac);
    if (trClass) {
      g_transform_get_localToWorldMatrix = FindMethod(trClass, "get_localToWorldMatrix", 0);
      g_transform_get_localScale = FindMethod(trClass, "get_localScale", 0);
      g_transform_get_localRotation =
          FindMethod(trClass, "get_localRotation", 0);
      g_transform_set_localRotation =
          FindMethod(trClass, "set_localRotation", 1);
      g_transform_get_localPosition =
          FindMethod(trClass, "get_localPosition", 0);
      g_transform_set_localPosition =
          FindMethod(trClass, "set_localPosition", 1);
      g_transform_get_position = FindMethod(trClass, "get_position", 0);
      g_transform_set_position = FindMethod(trClass, "set_position", 1);
      g_transform_get_rotation = FindMethod(trClass, "get_rotation", 0);
      g_transform_set_rotation = FindMethod(trClass, "set_rotation", 1);
      g_transform_get_childCount = FindMethod(trClass, "get_childCount", 0);
      g_transform_GetChild = FindMethod(trClass, "GetChild", 1);
      g_transform_get_parent = FindMethod(trClass, "get_parent", 0);
    }

    void *objClass = FindClass("UnityEngine", "Object", asms, ac);
    if (objClass) {
      g_object_get_name = FindMethod(objClass, "get_name", 0);
      g_object_FindObjectOfType = FindMethod(objClass, "FindObjectOfType", 1);
      if (!g_object_FindObjectOfType)
        g_object_FindObjectOfType =
            FindMethod(objClass, "FindFirstObjectByType", 1);
    }

    void *compClass = FindClass("UnityEngine", "Component", asms, ac);
    if (compClass) {
      g_componentClass = compClass;
      g_component_get_transform = FindMethod(compClass, "get_transform", 0);
      g_component_get_gameObject = FindMethod(compClass, "get_gameObject", 0);
    }

    void *goClass = FindClass("UnityEngine", "GameObject", asms, ac);
    if (goClass) {
      g_gameObjectClass = goClass;
      g_gameObject_setActive = FindMethod(goClass, "SetActive", 1);
      g_gameObject_get_activeSelf = FindMethod(goClass, "get_activeSelf", 0);
      g_gameObject_GetComponent = FindComponentTypeQuery(goClass, "GetComponent", false);
      g_gameObject_GetComponents = FindComponentTypeQuery(goClass, "GetComponents", true);
      Log("[POSER] typed component queries: GetComponent(Type)=%p GetComponents(Type)=%p",
          g_gameObject_GetComponent,g_gameObject_GetComponents);
    }

    void *camClass = FindClass("UnityEngine", "Camera", asms, ac);
    if (camClass) {
      g_cameraClass = camClass;
      g_camera_get_main = FindMethod(camClass, "get_main", 0);
      g_camera_get_fieldOfView = FindMethod(camClass, "get_fieldOfView", 0);
      g_camera_set_fieldOfView = FindMethod(camClass, "set_fieldOfView", 1);
      g_camera_get_worldToCameraMatrix =
          FindMethod(camClass, "get_worldToCameraMatrix", 0);
      g_camera_get_projectionMatrix =
          FindMethod(camClass, "get_projectionMatrix", 0);
      Log("[POSER] Camera: class=%p get_main=%p wtc=%p proj=%p findOfType=%p",
          camClass, g_camera_get_main, g_camera_get_worldToCameraMatrix,
          g_camera_get_projectionMatrix, g_object_FindObjectOfType);
    }

    // Task 4.1：面部/身体 BlendShape 读写
    void *smrClass = FindClass("UnityEngine", "SkinnedMeshRenderer", asms, ac);
    if (smrClass) {
      g_smr_get_bones = FindMethod(smrClass, "get_bones", 0);
      g_skinnedMeshRendererClass = smrClass;
      g_smr_get_sharedMesh = FindMethod(smrClass, "get_sharedMesh", 0);
      g_smr_GetBlendShapeWeight =
          FindMethod(smrClass, "GetBlendShapeWeight", 1);
      g_smr_SetBlendShapeWeight =
          FindMethod(smrClass, "SetBlendShapeWeight", 2);
    }
    void *meshClass = FindClass("UnityEngine", "Mesh", asms, ac);
    if (meshClass) {
      g_mesh_get_bindposes = FindMethod(meshClass, "get_bindposes", 0);
      g_mesh_get_blendShapeCount = FindMethod(meshClass, "get_blendShapeCount", 0);
      g_mesh_GetBlendShapeName = FindMethod(meshClass, "GetBlendShapeName", 1);
    }

    // 面板显示时解锁鼠标（Unity Cursor.lockState=None + visible=true）
    void *cursorClass = FindClass("UnityEngine", "Cursor", asms, ac);
    if (cursorClass) {
      g_cursor_set_lockState = FindMethod(cursorClass, "set_lockState", 1);
      g_cursor_set_visible = FindMethod(cursorClass, "set_visible", 1);
      g_cursor_get_lockState = FindMethod(cursorClass, "get_lockState", 0);
      g_cursor_get_visible = FindMethod(cursorClass, "get_visible", 0);
      Log("[POSER] Cursor: set_lock=%p set_vis=%p get_lock=%p get_vis=%p",
          g_cursor_set_lockState, g_cursor_set_visible, g_cursor_get_lockState,
          g_cursor_get_visible);
    }

    Log("[POSER] Game API resolved: GetBoneTransform=%p set_enabled=%p "
        "SetLocalRot=%p SetLocalPos=%p GetChild=%p GetComponents=%p "
        "cam_main=%p cam_fov=%p smr_setBS=%p",
        g_animator_GetBoneTransform, g_animator_set_enabled,
        g_transform_set_localRotation, g_transform_set_localPosition,
        g_transform_GetChild, g_gameObject_GetComponents,
        g_camera_get_main, g_camera_get_fieldOfView, g_smr_SetBlendShapeWeight);
  } __except (1) {
    Log("[POSER] ResolveGameApi exception");
  }
}

// 从 Entity 解析两级字段偏移（Entity -> ComplexAnimComp -> Animator），懒加载
static void ResolveEntityOffsets(void *entity) {
  if (OFF_entityComplexAnim >= 0 && OFF_complexAnimAnimator >= 0)
    return;
  __try {
    void *entClass = il2cpp_object_get_class(entity);
    if (entClass && OFF_entityComplexAnim < 0) {
      const char *caNames[] = {"<animatorCom>k__BackingField", "animatorCom",
                               "complexAnimationComponent",
                               "m_complexAnimationComponent",
                               "_complexAnimationComponent"};
      const char *matched = nullptr;
      OFF_entityComplexAnim =
          FindFieldInHierarchy(entClass, caNames, 5, &matched);
      if (OFF_entityComplexAnim >= 0)
        Log("[POSER] Entity.%s=0x%X", matched, OFF_entityComplexAnim);
      else
        Log("[WARN] Entity complexAnim field unresolved, fallback 0x110");
    }
    int ecOff = SafeOff(OFF_entityComplexAnim, 0x110, "entityComplexAnim");
    void *cac = *(void **)((char *)entity + ecOff);
    if (cac && OFF_complexAnimAnimator < 0) {
      void *cacClass = il2cpp_object_get_class(cac);
      if (cacClass) {
        const char *animNames[] = {"animator", "m_animator", "_animator",
                                   "<animator>k__BackingField"};
        const char *matched2 = nullptr;
        OFF_complexAnimAnimator =
            FindFieldInHierarchy(cacClass, animNames, 4, &matched2);
        if (OFF_complexAnimAnimator >= 0)
          Log("[POSER] ComplexAnimComp.%s=0x%X", matched2,
              OFF_complexAnimAnimator);
        else
          Log("[WARN] ComplexAnimComp.animator unresolved, fallback 0x148");
      }
    }
  } __except (1) {
  }
}

// Read an Entity without changing the editor selection (also used by squad playback).
static bool ReadCharacterRig(void *entity, void **outAnimator, void **outComponent) {
  if (CharacterSwitchInProgress() || !entity)
    return false;
  ResolveEntityOffsets(entity);
  __try {
    int ecOff = SafeOff(OFF_entityComplexAnim, 0x110, "entityComplexAnim");
    void *cac = *(void **)((char *)entity + ecOff);

    void *animator = nullptr;
    // 1) 先按已知偏移读
    if (cac && OFF_complexAnimAnimator >= 0)
      animator = *(void **)((char *)cac + OFF_complexAnimAnimator);
    // 2) 动态扫描 ComplexAnimationComponent 字段，找值类型为 Animator 的
    //    （不依赖硬编码偏移，角色/场景变化也能捕获）
    if (!UnityObjAlive(animator) && cac && g_animatorClass) {
      void *cacClass = il2cpp_object_get_class(cac);
      void *it = nullptr, *f;
      while ((f = il2cpp_class_get_fields(cacClass, &it))) {
        // Do not reinterpret integer/value-type fields as object pointers.
        if (il2cpp_field_get_flags(f) & 0x10)
          continue;
        void *type = il2cpp_field_get_type(f);
        if (!type || il2cpp_class_from_type(type) != g_animatorClass)
          continue;
        size_t off = il2cpp_field_get_offset(f);
        void *val = *(void **)((char *)cac + off);
        if (UnityObjAlive(val) && il2cpp_object_get_class(val) == g_animatorClass) {
          OFF_complexAnimAnimator = (int)off;
          animator = val;
          Log("[POSER] Animator found via field scan @0x%X", off);
          break;
        }
      }
    }
    if (!UnityObjAlive(animator) ||
        il2cpp_object_get_class(animator) != g_animatorClass ||
        !UnityObjAlive(SafeGetComponentTransform(animator)))
      return false;
    *outAnimator=animator; *outComponent=cac; return true;
  } __except(1) {return false;}
}

static bool SetCharacterEntity(void *entity) {
  void *animator=nullptr,*cac=nullptr;
  if(!ReadCharacterRig(entity,&animator,&cac))return false;
  __try {
    if (animator != g_charAnimator || entity != g_mainCharEntity) {
      if (g_beforeCharacterChange)
        g_beforeCharacterChange(entity); // Restore using OLD actor handles first.
      g_mainCharEntity = entity;
      g_charAnimator = animator;
      g_charChanged = true;
      // 诊断：Animator 是否为 Humanoid + 所在 GameObject 名。Avatar 没映射到的骨
      // GetBoneTransform 会直接返回 null（本项目实测只拿到 22/55 或 0），这两项能
      // 直接区分"抓错 Animator / 不是 Humanoid rig / Avatar 映射不全"。
      int isHuman = -1;
      char goName[128] = "";
      __try {
        if (g_animator_get_isHuman) {
          void *b = Invoke(g_animator_get_isHuman, animator);
          isHuman = b ? (*(bool *)((char *)b + 16) ? 1 : 0) : -1;
        }
        if (g_component_get_gameObject && g_object_get_name) {
          void *go = Invoke(g_component_get_gameObject, animator);
          void *nb = go ? Invoke(g_object_get_name, go) : nullptr;
          if (nb)
            ReadStr(nb, goName, sizeof(goName));
        }
      } __except (1) {
      }
      Log("[POSER] CharAnimator=%p isHuman=%d go='%s'", g_charAnimator,
          isHuman, goName);
    }
    g_charAnimComp = cac;
    return true;
  } __except (1) {
    return false;
  }
}

// 插件加载时游戏可能已就绪：直接从 PlayerController 捞一次当前角色
// 在层级里找指定名字的静态字段（返回 FieldInfo*，非偏移）
static void *FindStaticFieldInfo(void *klass, const char *name) {
  if (!klass || !il2cpp_class_get_parent)
    return nullptr;
  void *cur = klass;
  int depth = 0;
  while (cur && depth < 10) {
    void *it = nullptr, *f;
    while ((f = il2cpp_class_get_fields(cur, &it))) {
      const char *fn = il2cpp_field_get_name(f);
      if (!fn || strcmp(fn, name) != 0)
        continue;
      int flags = il2cpp_field_get_flags(f);
      if (flags & 0x10) // FIELD_ATTRIBUTE_STATIC
        return f;
    }
    cur = il2cpp_class_get_parent(cur);
    depth++;
  }
  return nullptr;
}

// 独立解析 PlayerController 单例（不依赖 SetMainCharacter hook）：
// 1) 静态字段 instance/m_instance/_instance/s_Instance
// 2) 静态属性 get_Instance
static void *FindPlayerControllerInstance() {
  if (!g_pcClass)
    return nullptr;
  __try {
    const char *names[] = {"instance", "m_instance", "_instance", "s_Instance",
                          "<Instance>k__BackingField", "<instance>k__BackingField"};
    for (int i = 0; i < 6; i++) {
      void *fi = FindStaticFieldInfo(g_pcClass, names[i]);
      if (!fi)
        continue;
      void *obj = nullptr;
      il2cpp_field_static_get_value(fi, &obj);
      if (obj) {
        return obj;
      }
    }
    void *m = FindMethodInHierarchy(g_pcClass, "get_Instance", 0);
    if (!m)
      m = FindMethodInHierarchy(g_pcClass, "get_instance", 0);
    if (m) {
      void *exc = nullptr;
      void *obj = il2cpp_runtime_invoke(m, nullptr, nullptr, &exc);
      if (obj && !exc) {
        return obj;
      }
    }
  } __except (1) {
  }
  return nullptr;
}

static void SelectCaptureEntity(void *entity) {
  if (entity != g_captureEntity) {
    if (g_beforeCharacterChange)
      g_beforeCharacterChange(entity); // Stop even if the new Animator is not ready.
    g_captureEntity = entity;
    Log("[POSER] character selection: entity=%p controller=%p", entity,
        g_playerController);
  }
}
static void TryCaptureFromPlayerController() {
  if (CharacterSwitchInProgress()) return;
  // Controllers can themselves be replaced by scene/photography transitions.
  void *current = FindPlayerControllerInstance();
  if (current && current != g_playerController) {
    g_playerController = current;
    g_captureEntity = nullptr;
    Log("[POSER] Resolved PlayerController: %p", current);
  }
  __try {
    if (g_playerController && OFF_pcEntity >= 0) {
      void *entity = *(void **)((char *)g_playerController + OFF_pcEntity);
      // A null mainCharacter during loading must not discard the entity from
      // the latest hook: its Animator may be filled in several frames later.
      if (entity)
        SelectCaptureEntity(entity);
    }
  } __except (1) {
  }
  SetCharacterEntity(g_captureEntity);
}
static void ConsumeCapturedCharacter() {
  if (CharacterSwitchInProgress()) return;
  AcquireSRWLockExclusive(&g_pendingCharacterLock);
  bool pending = g_pendingSelection;
  void *controller = g_pendingController;
  void *entity = g_pendingEntity;
  g_pendingSelection = false;
  ReleaseSRWLockExclusive(&g_pendingCharacterLock);
  if (!pending)
    return;
  if (controller)
    g_playerController = controller; // Every switch, not just the first one.
  SelectCaptureEntity(entity);
  SetCharacterEntity(entity);
}

// Preserve IL2CPP's hidden MethodInfo argument, and stop pose maintenance while
// the game is tearing down/replacing the model inside SetMainCharacter.
using SetMainCharacterFn = void (*)(void *, void *, bool, void *);
static SetMainCharacterFn g_originalSetMainCharacter = nullptr;
static void HookedSetMainCharacter(void *self, void *entity, bool flag, void *method) {
  InterlockedCompareExchange(&g_gameLogicThreadId, LONG(GetCurrentThreadId()), 0);
  InterlockedIncrement(&g_characterSwitchDepth);
  __try {
    if (g_originalSetMainCharacter)
      g_originalSetMainCharacter(self, entity, flag, method);
    if (!RuntimeClosing()) QueueCharacterSelection(self, entity);
  } __finally {
    InterlockedDecrement(&g_characterSwitchDepth);
  }
}

// 安装 PlayerController.SetMainCharacter hook（IL2CPP Resolve() 之后调用）
static void InstallSetMainCharacterHook() {
  __try {
    size_t ac = 0;
    void **asms = nullptr;
    void *domain = il2cpp_domain_get();
    if (domain)
      asms = il2cpp_domain_get_assemblies(domain, &ac);

    void *pcClass =
        FindClass("Beyond.Gameplay.Core", "PlayerController", asms, ac);
    if (!pcClass) {
      Log("[POSER] WARN: PlayerController class not found");
      return;
    }
    g_pcClass = pcClass;

    const char *entNames[] = {"mainCharacter", "m_entity", "_entity",
                              "m_mainCharacter", "entity",
                              "m_controlledEntity", "controlledEntity"};
    const char *matched = nullptr;
    OFF_pcEntity = FindFieldInHierarchy(pcClass, entNames, 7, &matched);
    if (OFF_pcEntity >= 0)
      Log("[POSER] PlayerController.%s=0x%X", matched, OFF_pcEntity);
    else
      Log("[WARN] PlayerController entity field unresolved, fallback 0x70");

    void *setMainChar = FindMethod(pcClass, "SetMainCharacter", 2);
    if (!setMainChar) {
      Log("[POSER] WARN: SetMainCharacter not found");
      return;
    }

    if (Hook(setMainChar, "PlayerController.SetMainCharacter",
             (void *)HookedSetMainCharacter, (void **)&g_originalSetMainCharacter)) {
      Log("[POSER] SetMainCharacter hooked");
      TryCaptureFromPlayerController(); // 尝试补捞当前已就绪角色
    }
  } __except (1) {
  }
}

// 总入口：在 IL2CPP Resolve() 成功后调用一次
static void InitGameHooks() {
  // 程序集枚举与游戏加载存在竞态：UnityEngine 类可能暂未就绪，重试直到解析到 Animator
  for (int i = 0; i < 10; i++) {
    ResolveGameApi();
    if (g_animator_GetBoneTransform)
      break;
    Log("[POSER] Game API not ready, retrying (%d/10)...", i + 1);
    Sleep(1000);
  }
  InstallSetMainCharacterHook();
}

// ---- 骨骼句柄封装（供 FK/IK/快照/从骨层使用）----
static void *GetHumanoidBone(HumanBodyBones bone) {
  if (!g_charAnimator || !g_animator_GetBoneTransform)
    return nullptr;
  __try {
    int b = (int)bone;
    void *params[] = {&b};
    return Invoke(g_animator_GetBoneTransform, g_charAnimator, params);
  } __except (1) {
    return nullptr;
  }
}

// Component.get_transform（获取组件所在 GameObject 的 Transform）
static void *SafeGetComponentTransform(void *component) {
  if (CharacterSwitchInProgress() || !UnityObjAlive(component) || !g_component_get_transform)
    return nullptr;
  __try {
    return Invoke(g_component_get_transform, component);
  } __except (1) {
    return nullptr;
  }
}

// 角色根 Transform（Animator 组件所在 GameObject 的 Transform）
static void *GetCharRootTransform() {
  return g_charAnimator ? SafeGetComponentTransform(g_charAnimator) : nullptr;
}

// Unity 对象被 Destroy 后，托管包装对象还在（指针非空），但 UnityEngine.Object
// 的第一个字段 m_CachedPtr(0x10) 会被清 0。用它判断"这个实例还活着吗"——
// 场景切换/换实例后，插件如果继续抓着旧 Animator 和旧骨骼变换，画面表现就是
// "骨架钉在原地"（所有写入/读取都作用在死对象上）。
static bool UnityObjAlive(void *obj) {
  if (!obj)
    return false;
  __try {
    return *(void **)((char *)obj + 0x10) != nullptr;
  } __except (1) {
    return false;
  }
}

// ECS animation records are not necessarily Unity Behaviour objects. Their
// field at 0x10 must never be interpreted as a native Behaviour pointer.
static bool LiveBehaviour(void *object) {
  if (!object || !g_behaviourClass || !il2cpp_object_get_class ||
      !il2cpp_class_get_parent) return false;
  __try {
    void *klass = il2cpp_object_get_class(object);
    for (int depth = 0; klass && depth < 64; ++depth) {
      if (klass == g_behaviourClass) return UnityObjAlive(object);
      klass = il2cpp_class_get_parent(klass);
    }
  } __except (1) {}
  return false;
}
static bool ReadBehaviourEnabled(void *object, bool &enabled) {
  if (CharacterSwitchInProgress() || !g_animator_get_enabled || !LiveBehaviour(object)) return false;
  __try {
    void *boxed = Invoke(g_animator_get_enabled, object);
    if (!boxed) return false;
    enabled = *reinterpret_cast<bool *>(static_cast<char *>(boxed) + 16);
    return true;
  } __except (1) { return false; }
}
static void WriteBehaviourEnabled(void *object, bool enabled) {
  if (CharacterSwitchInProgress() || !g_animator_set_enabled || !LiveBehaviour(object)) return;
  __try {
    bool current;
    if (ReadBehaviourEnabled(object, current) && current == enabled) return;
    int value = enabled ? 1 : 0;
    void *args[] = {&value};
    Invoke(g_animator_set_enabled, object, args);
  } __except (1) {}
}

static bool CharAnimatorAlive() {
  if (!UnityObjAlive(g_charAnimator))
    return false;
  return UnityObjAlive(GetCharRootTransform());
}

static Quat GetBoneLocalRot(void *t) {
  Quat q{0, 0, 0, 1};
  if (CharacterSwitchInProgress() || !UnityObjAlive(t) || !g_transform_get_localRotation)
    return q;
  __try {
    void *boxed = Invoke(g_transform_get_localRotation, t);
    if (boxed)
      q = *(Quat *)((char *)boxed + 16); // IL2CPP 盒对象数据区偏移 16
  } __except (1) {
  }
  return q;
}

// 骨骼写入钩子：任何"手动写骨"（旋转盘 / WebUI / 滑条）写完局部旋转/位置后会回调
// 一次（参数为被写的骨）。上层用它把新姿势同步进自己的缓存，例如 accessory.h 的
// 冻结快照——否则冻结维持逻辑下一帧就把这次编辑打回去。未注册时为 nullptr，无开销。
static void (*g_boneWriteHook)(void *transform) = nullptr;
// 第二个写骨观察者（撤销栈用）。分开是因为 accessory 需要临时挂起自己的快照同步，
// 而撤销栈必须继续记录编辑；两者互不影响。
static void (*g_boneWriteHook2)(void *transform) = nullptr;

static void SetBoneLocalRot(void *t, Quat q) {
  if (CharacterSwitchInProgress() || !UnityObjAlive(t) || !g_transform_set_localRotation)
    return;
  __try {
    void *params[] = {&q};
    Invoke(g_transform_set_localRotation, t, params);
  } __except (1) {
  }
  if (g_boneWriteHook)
    g_boneWriteHook(t);
  if (g_boneWriteHook2)
    g_boneWriteHook2(t);
}

static Vec3 GetBoneLocalPos(void *t) {
  Vec3 p{0, 0, 0};
  if (CharacterSwitchInProgress() || !UnityObjAlive(t) || !g_transform_get_localPosition)
    return p;
  __try {
    void *boxed = Invoke(g_transform_get_localPosition, t);
    if (boxed)
      p = *(Vec3 *)((char *)boxed + 16);
  } __except (1) {
  }
  return p;
}

static void SetBoneLocalPos(void *t, Vec3 p) {
  if (CharacterSwitchInProgress() || !UnityObjAlive(t) || !g_transform_set_localPosition)
    return;
  __try {
    void *params[] = {&p};
    Invoke(g_transform_set_localPosition, t, params);
  } __except (1) {
  }
  if (g_boneWriteHook)
    g_boneWriteHook(t);
  if (g_boneWriteHook2)
    g_boneWriteHook2(t);
}

// 取骨骼名称（用于日志/面板显示）
static void GetBoneName(void *transform, char *buf, int sz) {
  buf[0] = 0;
  if (!transform || !g_object_get_name || sz <= 0)
    return;
  __try {
    void *nameStr = Invoke(g_object_get_name, transform);
    if (nameStr)
      ReadStrUtf8(nameStr, buf, sz);
  } __except (1) {
  }
}

// ---- 世界空间位姿（IK 求解 / gizmo 定位用）----
static Vec3 GetBoneWorldPos(void *t) {
  Vec3 p{0, 0, 0};
  if (CharacterSwitchInProgress() || !UnityObjAlive(t) || !g_transform_get_position)
    return p;
  __try {
    void *boxed = Invoke(g_transform_get_position, t);
    if (boxed)
      p = *(Vec3 *)((char *)boxed + 16);
  } __except (1) {
  }
  return p;
}

static Quat GetBoneWorldRot(void *t) {
  Quat q{0, 0, 0, 1};
  if (CharacterSwitchInProgress() || !UnityObjAlive(t) || !g_transform_get_rotation)
    return q;
  __try {
    void *boxed = Invoke(g_transform_get_rotation, t);
    if (boxed)
      q = *(Quat *)((char *)boxed + 16);
  } __except (1) {
  }
  return q;
}

// ---- 世界空间写（自由相机/IK 目标移动用）----
static void SetBoneWorldPos(void *t, Vec3 p) {
  if (CharacterSwitchInProgress() || !UnityObjAlive(t) || !g_transform_set_position)
    return;
  __try {
    void *params[] = {&p};
    Invoke(g_transform_set_position, t, params);
  } __except (1) {
  }
}

static void SetBoneWorldRot(void *t, Quat q) {
  if (CharacterSwitchInProgress() || !UnityObjAlive(t) || !g_transform_set_rotation)
    return;
  __try {
    void *params[] = {&q};
    Invoke(g_transform_set_rotation, t, params);
  } __except (1) {
  }
}
