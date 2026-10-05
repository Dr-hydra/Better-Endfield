#pragma once

// 场上角色扫描 —— 多角色编辑的第一步（探针，只读）。
//
// 探针演进（都是实测踩出来的）：
//   v1 扫"所有 Animator" → 场景里 60+ 个全是道具/特效（脚印、围栏、鸽子、无人机…），
//      64 个名额没排到角色就满了。
//   v2 改用 SkeletalMorphCore 实例枚举 → 类解析到了，但 FindObjectsOfTypeAll 返回 0 个，
//      说明游戏里真正在跑的组件不是那个类。
//   v3（本版）改回扫 Animator（这条路是通的，实测 619~796 个），用**名字特征**过滤：
//      角色模型 GameObject 形如 `chr_0009_azrila_postmodel(Clone)#27`，祖先里还有一级
//      `#4670024589793050624_chr_0009_azrila`（实体根），组件里有 HGCharacterHelper /
//      BipedIK / AnimatorMono。于是判据 = 自己名字以 chr_ 开头，或父链任意一级含 chr_。
// 命中的角色连同父链、组件和当前编辑状态一起保存到本地诊断文件。

#include "core/game_hooks.h"
#include "core/il2cpp_api.h"

#define ROSTER_MAX_CHARS  32
#define ROSTER_MAX_COMPS  16
#define ROSTER_COMP_NAME  40
#define ROSTER_PARENT_MAX 5

struct RosterChar {
  void *animator;    // 角色模型上的 Animator（冻结/摆姿的目标）
  void *go;          // Animator 所在 GameObject
  char name[96];
  char display[64];  // 显示名（从名字里截出 chr_0014_aurora）
  int active;
  int isCurrent;     // 就是当前已捕获的那个角色
  int compCount;
  char comps[ROSTER_MAX_COMPS][ROSTER_COMP_NAME];
  int parentCount;
  char parents[ROSTER_PARENT_MAX][96];       // [0] = 自己所在 GO，往上依次
  void *parentGos[ROSTER_PARENT_MAX];        // 对应父链的 GameObject 指针
  int hasAnimator[ROSTER_PARENT_MAX];        // 该级是否挂着 Animator
  void *entityGo;                            // 实体根（#<id>_chr_xxx 那一级）
  void *animComp;                            // 实体根上的 CharacterAnimationComponent（冻结用）
};

static RosterChar g_rosterChars[ROSTER_MAX_CHARS];
static int g_rosterCharCount = 0;
static int g_rosterAnimatorTotal = 0;

// 懒解析
static void *g_rosterFindAll = nullptr;      // Resources.FindObjectsOfTypeAll(Type)
static void *g_rosterAnimatorClass = nullptr;
static void *g_rosterGetComponent = nullptr; // GameObject.GetComponent(Type)
static void *g_rosterGoActive = nullptr;     // GameObject.get_activeInHierarchy
static void *g_rosterCharAnimCompClass = nullptr; // CharacterAnimationComponent
static bool g_rosterApiTried = false;

// 按类名在所有程序集里找（CharacterAnimationComponent 的命名空间可能变）
static void *RosterFindClassByName(const char *want, void **asms, size_t ac) {
  if (!il2cpp_image_get_class_count || !il2cpp_image_get_class ||
      !il2cpp_class_get_name)
    return nullptr;
  for (size_t i = 0; i < ac; i++) {
    void *img = il2cpp_assembly_get_image(asms[i]);
    if (!img)
      continue;
    size_t n = il2cpp_image_get_class_count(img);
    for (size_t k = 0; k < n; k++) {
      void *cls = il2cpp_image_get_class(img, k);
      if (!cls)
        continue;
      const char *cn = il2cpp_class_get_name(cls);
      if (cn && strcmp(cn, want) == 0)
        return cls;
    }
  }
  return nullptr;
}

static void *RosterFindClassAny(const char *ns[], const char *name, void **asms,
                                size_t ac) {
  for (int i = 0; ns[i]; i++) {
    void *c = FindClass(ns[i], name, asms, ac);
    if (c)
      return c;
  }
  return nullptr;
}

static void ResolveRosterApi() {
  if (g_rosterApiTried)
    return;
  g_rosterApiTried = true;
  size_t ac = 0;
  void **asms = nullptr;
  void *domain = il2cpp_domain_get ? il2cpp_domain_get() : nullptr;
  if (domain)
    asms = il2cpp_domain_get_assemblies(domain, &ac);

  void *resClass = FindClass("UnityEngine", "Resources", asms, ac);
  if (resClass)
    g_rosterFindAll = FindMethod(resClass, "FindObjectsOfTypeAll", 1);

  g_rosterAnimatorClass = FindClass("UnityEngine", "Animator", asms, ac);
  g_rosterCharAnimCompClass =
      RosterFindClassByName("CharacterAnimationComponent", asms, ac);

  void *goClass = FindClass("UnityEngine", "GameObject", asms, ac);
  if (goClass) {
    g_rosterGetComponent = FindComponentTypeQuery(goClass, "GetComponent", false);
    g_rosterGoActive = FindMethod(goClass, "get_activeInHierarchy", 0);
  }
  Log("[ROSTER] api: FindObjectsOfTypeAll=%p Animator=%p CharacterAnimationComponent=%p",
      g_rosterFindAll, g_rosterAnimatorClass, g_rosterCharAnimCompClass);
}

static void RosterGoName(void *go, char *buf, int sz) {
  buf[0] = 0;
  if (!go || !g_object_get_name)
    return;
  __try {
    void *ns = Invoke(g_object_get_name, go);
    if (ns)
      ReadStr(ns, buf, sz);
  } __except (1) {
  }
}

static int RosterGoActive(void *go) {
  if (!go || !g_rosterGoActive)
    return -1;
  __try {
    void *boxed = Invoke(g_rosterGoActive, go);
    if (boxed)
      return *(bool *)((char *)boxed + 16) ? 1 : 0;
  } __except (1) {
  }
  return -1;
}

// 该 GameObject 上是否挂了 cls（用 GetComponent(class) 判断，避开泛型）
static void *RosterGetComponentOfClass(void *go, void *cls) {
  if (!go || !cls || !g_rosterGetComponent || !il2cpp_class_get_type ||
      !il2cpp_type_get_object)
    return nullptr;
  __try {
    void *type = il2cpp_class_get_type(cls);
    void *typeObj = type ? il2cpp_type_get_object(type) : nullptr;
    if (!typeObj)
      return nullptr;
    void *args[] = {typeObj};
    return Invoke(g_rosterGetComponent, go, args);
  } __except (1) {
    return nullptr;
  }
}

static void RosterCollectComps(void *go, RosterChar &e) {
  e.compCount = 0;
  if (!go || !g_gameObject_GetComponents || !g_componentClass)
    return;
  __try {
    void *type = il2cpp_class_get_type(g_componentClass);
    void *typeObj = type ? il2cpp_type_get_object(type) : nullptr;
    if (!typeObj)
      return;
    void *args[] = {typeObj};
    void *arr = Invoke(g_gameObject_GetComponents, go, args);
    if (!arr)
      return;
    int cnt = *(int *)((char *)arr + IL2CPP_ARRAY_LEN);
    void **data = (void **)((char *)arr + IL2CPP_ARRAY_DATA);
    for (int i = 0; i < cnt && e.compCount < ROSTER_MAX_COMPS; i++) {
      if (!data[i])
        continue;
      void *cls = il2cpp_object_get_class(data[i]);
      const char *cn = cls ? il2cpp_class_get_name(cls) : nullptr;
      if (!cn || !cn[0])
        continue;
      snprintf(e.comps[e.compCount], ROSTER_COMP_NAME, "%s", cn);
      e.compCount++;
    }
  } __except (1) {
  }
}

// 从某个组件出发，沿父链记录 GameObject 名字 + 该级是否有 Animator
static void RosterWalkParents(void *component, RosterChar &e) {
  e.parentCount = 0;
  if (!component || !g_component_get_transform || !g_transform_get_parent ||
      !g_component_get_gameObject)
    return;
  __try {
    void *tf = Invoke(g_component_get_transform, component);
    for (int i = 0; i < ROSTER_PARENT_MAX && tf; i++) {
      void *go = Invoke(g_component_get_gameObject, tf);
      RosterGoName(go, e.parents[e.parentCount], sizeof(e.parents[0]));
      e.parentGos[e.parentCount] = go;
      e.hasAnimator[e.parentCount] =
          RosterGetComponentOfClass(go, g_rosterAnimatorClass) ? 1 : 0;
      e.parentCount++;
      tf = Invoke(g_transform_get_parent, tf);
    }
  } __except (1) {
  }
}

static void RosterDumpCurrentCapture(FILE *f) {
  fprintf(f, "\n# ===== B. 当前已捕获的角色（参考样本）=====\n");
  if (!g_charAnimator) {
    fprintf(f, "# (还没有捕获到角色)\n");
    return;
  }
  void *go = Invoke(g_component_get_gameObject, g_charAnimator);
  char name[96] = "";
  RosterGoName(go, name, sizeof(name));
  const char *smcName = g_charAnimComp
                            ? il2cpp_class_get_name(il2cpp_object_get_class(g_charAnimComp))
                            : "(none)";
  fprintf(f, "# animator=%p go=%p name=\"%s\" entityComp=%s\n", g_charAnimator, go,
          name, smcName ? smcName : "?");
  RosterChar tmp;
  memset(&tmp, 0, sizeof(tmp));
  RosterWalkParents(g_charAnimator, tmp);
  fprintf(f, "# 父链：");
  for (int i = 0; i < tmp.parentCount; i++)
    fprintf(f, "%s%s%s", i ? " <- " : "", tmp.parents[i],
            tmp.hasAnimator[i] ? "[Animator]" : "");
  fprintf(f, "\n");
  RosterCollectComps(go, tmp);
  fprintf(f, "# 组件：");
  for (int i = 0; i < tmp.compCount; i++)
    fprintf(f, "%s%s", i ? "," : "", tmp.comps[i]);
  fprintf(f, "\n");
  Log("[ROSTER] current: name=\"%s\" entityComp=%s", name, smcName ? smcName : "?");
}

// 名字里是否含 "chr_"（大小写不敏感）——角色模型 / 实体根都带这个片段
static bool RosterNameHasChr(const char *s) {
  if (!s)
    return false;
  for (const char *p = s; p[0] && p[1] && p[2] && p[3]; p++) {
    if ((p[0] == 'c' || p[0] == 'C') && (p[1] == 'h' || p[1] == 'H') &&
        (p[2] == 'r' || p[2] == 'R') && p[3] == '_')
      return true;
  }
  return false;
}

// 从 `chr_0014_aurora_postmodel(Clone)#152` 里截出显示名 `chr_0014_aurora`
static void RosterMakeDisplay(const char *goName, char *out, int sz) {
  out[0] = 0;
  const char *p = nullptr;
  for (const char *q = goName; q[0]; q++) {
    if ((q[0] == 'c' || q[0] == 'C') && (q[1] == 'h' || q[1] == 'H') &&
        (q[2] == 'r' || q[2] == 'R') && q[3] == '_') {
      p = q;
      break;
    }
  }
  if (!p) {
    snprintf(out, sz, "%s", goName);
    return;
  }
  const char *end = strstr(p, "_postmodel");
  int n = end ? (int)(end - p) : (int)strlen(p);
  if (n > sz - 1)
    n = sz - 1;
  memcpy(out, p, n);
  out[n] = 0;
}

static void RosterDump() {
  FILE *f = OpenPoserFile(L"poser_roster.txt", L"wb");
  if (!f) {
    Log("[ROSTER] WARN: cannot write plugin/poser_roster.txt");
    return;
  }
  fprintf(f, "# 场上角色扫描（plugin\\poser_roster.txt）\n");
  fprintf(f, "# ===== A. 角色（Animator 名字/父链含 chr_）=====\n");
  fprintf(f, "# idx\tactive\tcurrent\tname\t父链(带[Animator]的层级)\t组件\n");
  for (int i = 0; i < g_rosterCharCount; i++) {
    RosterChar &e = g_rosterChars[i];
    fprintf(f, "%d\t%d\t%d\t%s\t", i, e.active, e.isCurrent,
            e.name[0] ? e.name : "(unnamed)");
    for (int p = 0; p < e.parentCount; p++)
      fprintf(f, "%s%s%s", p ? " <- " : "", e.parents[p],
              e.hasAnimator[p] ? "[Animator]" : "");
    fprintf(f, "\t");
    for (int c = 0; c < e.compCount; c++)
      fprintf(f, "%s%s", c ? "," : "", e.comps[c]);
    fprintf(f, "\n");
    Log("[ROSTER] char#%d active=%d current=%d name=\"%s\" parents=%d comps=%d", i,
        e.active, e.isCurrent, e.name, e.parentCount, e.compCount);
    for (int c = 0; c < e.compCount; c++)
      Log("[ROSTER]   char#%d comp: %s", i, e.comps[c]);
  }
  RosterDumpCurrentCapture(f);
  fprintf(f, "\n# ===== C. 统计 =====\n");
  fprintf(f, "# 角色数（名字含 chr_ 的 Animator）= %d\n", g_rosterCharCount);
  fprintf(f, "# Animator 总数（含道具/特效）= %d\n", g_rosterAnimatorTotal);
  fclose(f);
  Log("[ROSTER] scanned: %d characters, %d animators total -> plugin/poser_roster.txt",
      g_rosterCharCount, g_rosterAnimatorTotal);
}

// 面板里点「扫描角色」时调用。只读。
// Requests are consumed on a normal game callback. Rendering only uses the
// cached list and never blocks on a scene scan or replaces actor handles.
static bool g_rosterScanPending = false;
static int g_rosterSelectPending = -1;
static std::shared_ptr<GripReferences> g_rosterReferences;
static std::shared_ptr<GripReferences> g_selectedRosterReferences;
static void ScanSceneForCharacters() { g_rosterScanPending = true; }
static void ScanSceneForCharactersNow() {
  g_rosterReferences = std::make_shared<GripReferences>();

  ResolveRosterApi();
  g_rosterCharCount = 0;
  g_rosterAnimatorTotal = 0;
  if (!g_rosterFindAll || !g_rosterAnimatorClass || !il2cpp_class_get_type ||
      !il2cpp_type_get_object) {
    Log("[ROSTER] scan aborted: FindObjectsOfTypeAll=%p Animator=%p",
        g_rosterFindAll, g_rosterAnimatorClass);
    return;
  }
  void *t = il2cpp_class_get_type(g_rosterAnimatorClass);
  void *to = t ? il2cpp_type_get_object(t) : nullptr;
  if (!to) {
    Log("[ROSTER] scan aborted: cannot build a Type object for Animator");
    return;
  }
  void *args[] = {to};
  void *arr = Invoke(g_rosterFindAll, nullptr, args);
  if (!arr) {
    Log("[ROSTER] FindObjectsOfTypeAll returned null");
    return;
  }
  int n = *(int *)((char *)arr + IL2CPP_ARRAY_LEN);
  if (n < 0 || n > 65536 || !il2cpp_gchandle_new) return;
  void **data = (void **)((char *)arr + IL2CPP_ARRAY_DATA);
  g_rosterAnimatorTotal = n;
  Log("[ROSTER] animators total = %d, filtering by name pattern 'chr_'", n);

  for (int i = 0; i < n && g_rosterCharCount < ROSTER_MAX_CHARS; i++) {
    void *an = data[i];
    if (!UnityObjAlive(an))
      continue;
    void *go = Invoke(g_component_get_gameObject, an);
    if (!go)
      continue;
    RosterChar e;
    memset(&e, 0, sizeof(e));
    char nm[96] = "";
    RosterGoName(go, nm, sizeof(nm));
    RosterWalkParents(an, e); // 顺便拿到父链（含 [Animator] 标记）
    // 判据（实测）：
    //   真角色 = 名字以 chr_ 开头，且是**场景实例**（父链 自己 <- mesh <- #<id>_chr_xxx 共 3 级）
    //   要排除：prefab 资产（同样以 chr_ 开头，但没有父节点，parentCount==1）、
    //           技能特效（abilityentity_chr_...）、武器（wpn_...，父链里带 chr_ 但那不是角色）
    bool startsChr = (nm[0] && (nm[0] == 'c' || nm[0] == 'C') &&
                      (nm[1] == 'h' || nm[1] == 'H') && (nm[2] == 'r' || nm[2] == 'R') &&
                      nm[3] == '_');
    if (!startsChr || e.parentCount < 3)
      continue;
    e.animator = an;
    e.go = go;
    snprintf(e.name, sizeof(e.name), "%s", nm);
    RosterMakeDisplay(nm, e.display, sizeof(e.display));
    e.active = RosterGoActive(go);
    e.isCurrent = (an == g_charAnimator) ? 1 : 0;
    e.entityGo = e.parentGos[2]; // #<id>_chr_xxx（实体根）
    if (e.entityGo && g_rosterCharAnimCompClass)
      e.animComp = RosterGetComponentOfClass(e.entityGo, g_rosterCharAnimCompClass);
    RosterCollectComps(go, e);
    uint32_t handle = il2cpp_gchandle_new(an, false);
    if (!handle) continue;
    g_rosterReferences->handles.push_back(handle);
    g_rosterChars[g_rosterCharCount++] = e;
  }
  RosterDump();
}

// ---- 点谁编辑谁 ----
// 把"编辑目标"切到列表里的第 idx 个角色：存旧角色状态 → 换目标 → 重建骨骼/从骨/形态 →
// 恢复该角色自己的冻结状态（没冻过就保持游戏默认）。
// 注意：只换"我们在编辑谁"，**不改变游戏自己操控的角色**（那个还是游戏说了算）。
static void RosterSwitchEditTarget(int idx) { g_rosterSelectPending = idx; }
static void RosterSwitchEditTargetNow(int idx) {
  if (idx < 0 || idx >= g_rosterCharCount) return;
  const RosterChar &e = g_rosterChars[idx];
  if (!UnityObjAlive(e.animator) || e.animator == g_charAnimator) return;
  if (!UnityObjAlive(SafeGetComponentTransform(e.animator))) return;
  g_preserveGripOnHandoff = true;
  try { PrepareCharacterHandoff(); }
  catch (...) { g_preserveGripOnHandoff = false; throw; }
  g_preserveGripOnHandoff = false;
  g_selectedRosterReferences = g_rosterReferences;
  g_charAnimator = e.animator;
  g_charAnimComp = UnityObjAlive(e.animComp) ? e.animComp : nullptr;
  g_mainCharEntity = g_captureEntity = nullptr;
  g_editSelection.selectManual();
  g_charChanged = true;
  RebuildCapturedCharacter();
  Log("[ROSTER] edit target = %s bones=%d frozen=%d", e.display,
      s_humanBoneCount, int(g_frozen));
}
static void RosterService() {
  if (GetCurrentThreadId() != g_frameGameThreadId.load()) return;
  if (g_rosterSelectPending >= 0) {
    const int target = g_rosterSelectPending;
    g_rosterSelectPending = -1;
    RosterSwitchEditTargetNow(target);
  }
  if (g_rosterScanPending) {
    g_rosterScanPending = false;
    ScanSceneForCharactersNow();
  }
}

static void DrawRosterPanel() {
  if (!g_showRoster)
    return;
  ImGui::SetNextWindowSize(ImVec2(300.0f, 260.0f), ImGuiCond_FirstUseEver);
  if (!ImGui::Begin(u8"\u89d2\u8272", &g_showRoster)) {
    ImGui::End();
    return;
  }
  if (ImGui::Button(u8"\u5237\u65b0\u5217\u8868"))
    ScanSceneForCharacters();
  ImGui::SameLine();
  ImGui::TextDisabled(g_rosterScanPending ? u8"等待游戏更新… (%d)" : "(%d)", g_rosterCharCount);
  ImGui::Separator();
  if (g_rosterCharCount == 0)
    ImGui::TextDisabled("(empty - press the button above)");
  for (int i = 0; i < g_rosterCharCount; i++) {
    RosterChar &e = g_rosterChars[i];
    ImGui::PushID(i);
    bool cur = (e.animator && e.animator == g_charAnimator);
    if (cur)
      ImGui::TextColored(ImVec4(0.45f, 1.0f, 0.45f, 1.0f), "%s",
                         e.display[0] ? e.display : e.name);
    else
      ImGui::Text("%s", e.display[0] ? e.display : e.name);
    ImGui::SameLine();
    if (cur)
      ImGui::TextDisabled(u8"\uff08\u5f53\u524d\uff09");
    else if (ImGui::SmallButton(u8"\u7f16\u8f91"))
      RosterSwitchEditTarget(i);
    ImGui::PopID();
  }
  ImGui::End();
}
