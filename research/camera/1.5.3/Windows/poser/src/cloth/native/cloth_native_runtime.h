#pragma once
#include "cloth_native_math.h"
#include "../resources/cloth_surface_file.h"

static void *SurfaceClass(const char *ns, const char *name) {
  if (!il2cpp_domain_get || !il2cpp_domain_get_assemblies) return nullptr;
  using namespace poser_cloth_metadata;
  auto domain=il2cpp_domain_get();
  const auto key=Lookup(domain,Kind::Class,name,ns);
  if(auto hit=cache.Find(key)) return reinterpret_cast<void *>(hit);
  size_t count = 0; auto asms = il2cpp_domain_get_assemblies(domain, &count);
  return reinterpret_cast<void *>(cache.Remember(key,uintptr_t(FindClass(ns, name, asms, count))));
}

static void *SurfaceMethod(void *cls, const char *name, const char *ret,
                           const char *a = nullptr, const char *b = nullptr) {
  if (!s_clothMethodFlags) return nullptr;
  using namespace poser_cloth_metadata;
  const auto key=Lookup(cls,Kind::NormalizedMethod,name,ret,a,b);
  if(auto hit=cache.Find(key)) return reinterpret_cast<void *>(hit);
  for (int depth = 0; cls && depth < 16; ++depth, cls = il2cpp_class_get_parent(cls)) {
    for (void *it = nullptr, *m = nullptr; (m = il2cpp_class_get_methods(cls, &it));) {
      uint32_t flags = 0;
      if (strcmp(il2cpp_method_get_name(m), name) ||
          il2cpp_method_get_param_count(m) != (b ? 2u : a ? 1u : 0u) ||
          (s_clothMethodFlags(m, &flags) & 0x10) ||
          !CollisionType(il2cpp_method_get_return_type(m), ret) ||
          (a && !CollisionType(il2cpp_method_get_param(m, 0), a)) ||
          (b && !CollisionType(il2cpp_method_get_param(m, 1), b))) continue;
      return reinterpret_cast<void *>(cache.Remember(key,uintptr_t(m)));
    }
  }
  Log("[CLOTH-SURFACE-ABI] missingMethod=%s return=%s arg0=%s arg1=%s", name, ret, a ? a : "none", b ? b : "none");
  return nullptr;
}

static bool SurfaceCall(void *obj, const char *name, const char *arg = nullptr, void *value = nullptr) {
  void *unused = nullptr, *args[]{value};
  return obj && ClothInvoke(SurfaceMethod(il2cpp_object_get_class(obj), name, "System.Void", arg),
                           obj, arg ? args : nullptr, unused);
}

static bool SurfaceReference(void *object, const char *field, const char *type, void *value) {
  auto f = object ? CollisionFieldInfo(il2cpp_object_get_class(object), field, type) : nullptr;
  if (!f || !il2cpp_field_set_value_object) return false;
  il2cpp_field_set_value_object(object, f, value);
  void *read = nullptr;
  return CollisionField(object, field, type, read) && read == value;
}

template<class T> static bool SurfaceScalar(void *object, const char *field, const char *type, T value) {
  auto f = object ? CollisionFieldInfo(il2cpp_object_get_class(object), field, type) : nullptr;
  auto cls = f ? il2cpp_class_from_type(il2cpp_field_get_type(f)) : nullptr;
  uint32_t align = 0;
  if (!cls || !il2cpp_class_value_size || il2cpp_class_value_size(cls, &align) != sizeof(T)) return false;
  auto offset = il2cpp_field_get_offset(f);
  if (offset < 16 || offset > 65536-sizeof(T)) return false;
  memcpy((char *)object+offset, &value, sizeof(T));
  T read{}; return CollisionField(object, field, type, read) && !memcmp(&read, &value, sizeof(T));
}

static bool SurfaceEnum(void *obj, const char *field, const char *type, const char *name) {
  auto f = obj ? CollisionFieldInfo(il2cpp_object_get_class(obj), field, type) : nullptr;
  auto cls = f ? il2cpp_class_from_type(il2cpp_field_get_type(f)) : nullptr;
  int value = 0;
  return cls && CollisionEnumValue(cls, name, value) && SurfaceScalar(obj, field, type, value);
}

static bool SurfaceList(void *data, const char *field, const char *type, const char *element,
                        const std::vector<void *> &items) {
  void *list = nullptr;
  if (!CollisionField(data, field, type, list) || !list || !SurfaceCall(list, "Clear")) return false;
  for (auto item : items) if (!SurfaceCall(list, "Add", element, item)) return false;
  if (CollisionCount(list) != int(items.size())) return false;
  for (int n = 0; n < int(items.size()); ++n)
    if (CollisionItem(list, n, element) != items[n]) return false;
  return true;
}

static bool SurfaceCloneField(void *dst, void *src, const char *name, const char *type) {
  void *original = nullptr, *copy = nullptr;
  const bool ok = CollisionField(src, name, type, original) && original &&
      ClothInvoke(SurfaceMethod(il2cpp_object_get_class(original), "Clone", type), original, nullptr, copy) &&
      copy && copy != original && SurfaceReference(dst, name, type, copy);
  if (!ok) Log("[CLOTH-SURFACE-ABI] cloneField=%s type=%s source=%p copy=%p confirmed=0", name, type, original, copy);
  return ok;
}

static bool SurfaceCopyConfiguration(void *dst, void *src) {
  bool deep = true; void *unused = nullptr, *args[]{src, &deep};
  if (!dst || dst == src || !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(dst), "Import", "System.Void",
          "BeyondDynamicBone.ClothSerializeData", "System.Boolean"), dst, args, unused)) return false;
  int mode = 0;
  if (!CollisionField(src, "meshWriteMode", "BeyondDynamicBone.ClothMeshWriteMode", mode) ||
      !SurfaceScalar(dst, "meshWriteMode", "BeyondDynamicBone.ClothMeshWriteMode", mode) ||
      !SurfaceCloneField(dst, src, "springConstraint", "BeyondDynamicBone.SpringConstraint.SerializeData")) return false;
  const char *fields[]{"sourceRenderers", "rootBones", "ignoreFromRootBones", "paintMaps",
      "customSkinningSetting", "colliderCollisionConstraint"};
  const char *types[]{"System.Collections.Generic.List<UnityEngine.Renderer>",
      "System.Collections.Generic.List<UnityEngine.Transform>", "System.Collections.Generic.List<UnityEngine.Transform>",
      "System.Collections.Generic.List<UnityEngine.Texture2D>", "BeyondDynamicBone.CustomSkinningSettings",
      "BeyondDynamicBone.ColliderCollisionConstraint.SerializeData"};
  for (int n = 0; n < 6; ++n) {
    void *a = nullptr, *b = nullptr;
    if (!CollisionField(src, fields[n], types[n], a) || !CollisionField(dst, fields[n], types[n], b) || !a || !b || a == b) return false;
  }
  void *oldConstraint = nullptr, *constraint = nullptr, *oldList = nullptr, *list = nullptr;
  if (!CollisionList(src, oldConstraint, oldList) || !CollisionList(dst, constraint, list) || list == oldList ||
      !SurfaceCloneField(constraint, oldConstraint, "limitDistance", "BeyondDynamicBone.CurveSerializeData")) return false;
  if (!SurfaceList(constraint, "colliderList", "System.Collections.Generic.List<BeyondDynamicBone.ColliderComponent>",
                   "BeyondDynamicBone.ColliderComponent", {})) return false;
  return SurfaceList(dst, "sourceRenderers", "System.Collections.Generic.List<UnityEngine.Renderer>", "UnityEngine.Renderer", {}) &&
      SurfaceList(dst, "rootBones", "System.Collections.Generic.List<UnityEngine.Transform>", "UnityEngine.Transform", {}) &&
      SurfaceList(dst, "ignoreFromRootBones", "System.Collections.Generic.List<UnityEngine.Transform>", "UnityEngine.Transform", {}) &&
      SurfaceList(dst, "paintMaps", "System.Collections.Generic.List<UnityEngine.Texture2D>", "UnityEngine.Texture2D", {});
}

static bool SurfaceTRS(void *t, void *parent, Vector3 p, Quaternion q, Vector3 scale) {
  return t && SurfaceCall(t, "set_parent", "UnityEngine.Transform", parent) &&
      SurfaceCall(t, "set_localPosition", "UnityEngine.Vector3", &p) &&
      SurfaceCall(t, "set_localRotation", "UnityEngine.Quaternion", &q) &&
      SurfaceCall(t, "set_localScale", "UnityEngine.Vector3", &scale);
}

static std::vector<ClothRef> s_clothOwnedRoots;
static bool ClothOwnedRoot(void *transform) {
  for(const auto &r:s_clothOwnedRoots)
    if(transform && r.handle && uintptr_t(transform)==r.id.managed) return true;
  return false;
}
static bool SurfaceRenderArrayLength(void *array, const char *type, size_t expected) {
  if (!array || !expected || expected > 65536 ||
      !ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(array)),type)) return false;
  uintptr_t count = 0; memcpy(&count,(char *)array+24,sizeof(count));
  return count == expected;
}

static bool SurfaceRenderArray(void *object, const char *getter, const char *arrayType,
                               const char *ns, const char *element, int stride,
                               size_t expected, std::vector<unsigned char> &out) {
  out.clear();
  if (!object || !expected || expected > 65536 || stride <= 0 || stride > 64) return false;
  auto cls = SurfaceClass(ns, element); uint32_t alignment = 0;
  if (!cls || !il2cpp_class_value_size || il2cpp_class_value_size(cls,&alignment) != stride) return false;
  if ((!strcmp(element,"Vector3") && !ClothInputLayout(cls,"UnityEngine.Vector3",12)) ||
      (!strcmp(element,"Matrix4x4") && !ClothInputLayout(cls,"UnityEngine.Matrix4x4",64))) return false;
  void *array = nullptr;
  if (!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(object),getter,arrayType),object,nullptr,array) || !array) return false;
  const auto hold = il2cpp_gchandle_new(array,false);
  if (!hold) return false;
  const bool ok = SurfaceRenderArrayLength(array,arrayType,expected);
  if (ok) out.assign((unsigned char *)array+32,(unsigned char *)array+32+expected*stride);
  il2cpp_gchandle_free(hold); return ok;
}
struct SurfaceRenderDescriptor { int attribute, format, dimension, stream; };
static bool SurfaceRenderDescriptors(void *mesh, std::vector<SurfaceRenderDescriptor> &out) {
  out.clear();
  auto cls = SurfaceClass("UnityEngine.Rendering","VertexAttributeDescriptor");
  const char *names[]{"<attribute>k__BackingField","<format>k__BackingField","<dimension>k__BackingField","<stream>k__BackingField"};
  const char *types[]{"UnityEngine.Rendering.VertexAttribute","UnityEngine.Rendering.VertexAttributeFormat","System.Int32","System.Int32"};
  for (int n = 0; n < 4; ++n) if (!cls || ClothValueOffset(cls,names[n],types[n],16,4) != n*4) return false;
  int count = 0;
  if (!ClothValue(SurfaceMethod(il2cpp_object_get_class(mesh),"get_vertexAttributeCount","System.Int32"),mesh,count) || count < 1 || count > 14) return false;
  auto get = SurfaceMethod(il2cpp_object_get_class(mesh),"GetVertexAttribute","UnityEngine.Rendering.VertexAttributeDescriptor","System.Int32");
  for (int n = 0; n < count; ++n) {
    void *box = nullptr, *args[]{&n}; SurfaceRenderDescriptor d{};
    if (!ClothInvoke(get,mesh,args,box) || !box || il2cpp_object_get_class(box) != cls) return false;
    memcpy(&d,(char *)box+16,sizeof(d)); out.push_back(d);
  }
  return true;
}
static bool SurfaceSameDescriptors(void *mesh, const std::vector<SurfaceRenderDescriptor> &expected) {
  std::vector<SurfaceRenderDescriptor> actual;
  return SurfaceRenderDescriptors(mesh,actual) && actual.size() == expected.size() &&
      !memcmp(actual.data(),expected.data(),actual.size()*sizeof(SurfaceRenderDescriptor));
}
static bool SurfaceRenderReferences(void *object, const char *getter, const char *type,
                                    size_t minCount, size_t maxCount, std::vector<ClothRef> &out) {
  void *array = nullptr; uintptr_t count = 0;
  if (!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(object),getter,type),object,nullptr,array) || !array) return false;
  const auto hold = il2cpp_gchandle_new(array,false);
  if (!hold) return false;
  bool ok = ClothArray(array,type,count) && count >= minCount && count <= maxCount;
  for (size_t n = 0; ok && n < count; ++n) {
    auto ref = ClothProtect(reinterpret_cast<void **>((char *)array+32)[n]);
    if (!ref.handle) ok = false;
    else out.push_back(ref);
  }
  il2cpp_gchandle_free(hold); return ok;
}
static bool SurfaceRenderSameReferences(void *object, const char *getter, const char *type,
                                        const std::vector<ClothRef> &expected) {
  void *array = nullptr; uintptr_t count = 0;
  if (!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(object),getter,type),object,nullptr,array) || !array) return false;
  const auto hold = il2cpp_gchandle_new(array,false);
  if (!hold) return false;
  bool ok = ClothArray(array,type,count) && count == expected.size();
  for (size_t n = 0; ok && n < count; ++n) {
    auto actual = reinterpret_cast<void **>((char *)array+32)[n];
    ok = actual && actual == ClothTarget(expected[n]);
  }
  il2cpp_gchandle_free(hold); return ok;
}
static bool SurfaceVisiblePose(void *t, Vector3 &p, Quaternion &q, Vector3 &scale) {
  void *box=nullptr;
  return t &&
      ClothInvoke(SurfaceMethod(g_transformClass,"get_localPosition","UnityEngine.Vector3"),t,nullptr,box) && ClothInputCopyBox(box,"UnityEngine.Vector3",&p,12) &&
      ClothInvoke(SurfaceMethod(g_transformClass,"get_localRotation","UnityEngine.Quaternion"),t,nullptr,box) && ClothInputCopyBox(box,"UnityEngine.Quaternion",&q,16) &&
      ClothInvoke(SurfaceMethod(g_transformClass,"get_localScale","UnityEngine.Vector3"),t,nullptr,box) && ClothInputCopyBox(box,"UnityEngine.Vector3",&scale,12) &&
      std::isfinite(p.x) && std::isfinite(p.y) && std::isfinite(p.z) &&
      std::isfinite(q.x) && std::isfinite(q.y) && std::isfinite(q.z) && std::isfinite(q.w) &&
      fabsf(q.x*q.x+q.y*q.y+q.z*q.z+q.w*q.w-1)<.001f &&
      std::isfinite(scale.x) && std::isfinite(scale.y) && std::isfinite(scale.z);
}
static bool SurfaceVisibleSame(Vector3 a, Vector3 b) {
  return fabsf(a.x-b.x)<.000001f && fabsf(a.y-b.y)<.000001f && fabsf(a.z-b.z)<.000001f;
}
static bool SurfaceVisibleSame(Quaternion a, Quaternion b) {
  const float d=a.x*b.x+a.y*b.y+a.z*b.z+a.w*b.w;
  if (d<0) b={-b.x,-b.y,-b.z,-b.w};
  return fabsf(a.x-b.x)<.000001f && fabsf(a.y-b.y)<.000001f && fabsf(a.z-b.z)<.000001f && fabsf(a.w-b.w)<.000001f;
}
static constexpr size_t SurfaceBundleRegistryLimit = 65536;
static constexpr size_t SurfaceBundleRegistryBatch = 512;
static const char *SurfaceBundleRegistryAcquire(void *cls, uint32_t &handle, size_t &count) {
  if (handle) return "private-input-registry-duplicate-snapshot";
  count = 0;
  if (!cls) return "private-input-registry-class-unavailable";
  auto method = ClothMethod(cls,"GetAllLoadedAssetBundles",
      "System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle>",nullptr,true);
  if (!method) {
    for (void *it = nullptr, *m = nullptr; (m = il2cpp_class_get_methods(cls,&it));) {
      if (strcmp(il2cpp_method_get_name(m),"GetAllLoadedAssetBundles")) continue;
      auto type = il2cpp_method_get_return_type(m);
      const char *actual = type ? il2cpp_type_get_name(type) : nullptr;
      uint32_t impl = 0;
      Log("[CLOTH-SURFACE-INPUT-REGISTRY] methodMismatch=1 return=%s args=%u flags=%u",
          actual ? actual : "null",il2cpp_method_get_param_count(m),s_clothMethodFlags(m,&impl));
      if (actual) s_clothFreeName(const_cast<char *>(actual));
    }
    return "private-input-registry-signature-unavailable";
  }
  void *array = nullptr;
  if (!ClothInvoke(method,nullptr,nullptr,array)) return "private-input-registry-invoke-failed";
  if (!array) return "private-input-registry-null-result";
  const auto hold = il2cpp_gchandle_new(array,false);
  if (!hold) return "private-input-registry-protection-failed";
  auto type = il2cpp_class_get_type(il2cpp_object_get_class(array));
  if (!ClothTypeIs(type,"UnityEngine.AssetBundle[]")) {
    const char *actual = type ? il2cpp_type_get_name(type) : nullptr;
    Log("[CLOTH-SURFACE-INPUT-REGISTRY] arrayTypeMismatch=1 actual=%s",actual ? actual : "null");
    if (actual) s_clothFreeName(const_cast<char *>(actual));
    il2cpp_gchandle_free(hold);
    return "private-input-registry-result-not-bundle-array";
  }
  memcpy(&count,(char *)array+24,sizeof(count));
  if (count > SurfaceBundleRegistryLimit) {
    Log("[CLOTH-SURFACE-INPUT-REGISTRY] count=%llu limit=%zu complete=0 reason=private-input-registry-count-over-budget",
        (unsigned long long)count,SurfaceBundleRegistryLimit);
    il2cpp_gchandle_free(hold);
    return "private-input-registry-count-over-budget";
  }
  handle = hold;
  return nullptr;
}

template<class MatchName>
static const char *SurfaceBundleRegistryAdvanceNames(uint32_t handle, size_t count, size_t &cursor,
    std::vector<int> &ids, MatchName matchName, size_t itemBudget=SurfaceBundleRegistryBatch, unsigned timeBudgetMs=2) {
  void *array = CollisionGc(handle);
  if (!array || count > SurfaceBundleRegistryLimit || cursor > count || ids.size() != cursor)
    return "private-input-registry-snapshot-invalid";
  LARGE_INTEGER start{}, frequency{}, now{};
  if (!QueryPerformanceFrequency(&frequency) || !QueryPerformanceCounter(&start))
    return "private-input-registry-clock-unavailable";
  const size_t end = cursor+(std::min)(count-cursor,itemBudget);
  for (; cursor < end;) {
    auto bundle = reinterpret_cast<void **>((char *)array+32)[cursor]; int id = 0; char name[128]{};
    if (!bundle) return "private-input-registry-null-entry";
    if (!ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(bundle)),"UnityEngine.AssetBundle")) {
      return "private-input-registry-entry-type-mismatch";
    }
    if (!ClothValue(s_clothUnity.instance,bundle,id) || !id) {
      void *alive=nullptr,*args[]{bundle};
      if (!ClothInvoke(s_clothUnity.alive,nullptr,args,alive) || !alive || ClothUnboxBool(alive))
        return "private-input-registry-instance-unreadable";
      ids.push_back(0); ++cursor;
      Log("[CLOTH-SURFACE-INPUT-REGISTRY] destroyedEntry=%zu count=%zu nativeAlive=0 scanContinues=1",cursor-1,count);
      QueryPerformanceCounter(&now);
      if ((now.QuadPart-start.QuadPart)*1000 >= frequency.QuadPart*timeBudgetMs) break;
      continue;
    }
    void *str = nullptr;
    if (!ClothInvoke(s_clothUnity.name,bundle,nullptr,str) || !str) return "private-input-registry-name-unreadable";
    ReadStrUtf8(str,name,int(sizeof(name)));
    ids.push_back(id); matchName(name); ++cursor;
    QueryPerformanceCounter(&now);
    if ((now.QuadPart-start.QuadPart)*1000 >= frequency.QuadPart*timeBudgetMs) break;
  }
  return nullptr;
}

static bool SurfaceBundleSnapshotHas(uint32_t handle, size_t count, void *object, bool &present) {
  present = true;
  void *array = CollisionGc(handle);
  if (!array || !object || count > SurfaceBundleRegistryLimit) return false;
  auto entries = reinterpret_cast<void **>((char *)array+32);
  present = std::find(entries,entries+count,object) != entries+count;
  return true;
}

static const char *SurfaceBundleRegistryContains(void *cls, void *owned, bool &present) {
  present = true;
  if (!owned) return "private-input-unload-identity-unavailable";
  uint32_t hold = 0; size_t count = 0;
  if (const char *issue = SurfaceBundleRegistryAcquire(cls,hold,count)) return issue;
  const bool read = SurfaceBundleSnapshotHas(hold,count,owned,present);
  il2cpp_gchandle_free(hold);
  return read ? nullptr : "private-input-registry-snapshot-invalid";
}
struct SurfaceBuildResult {
  bool known = false, error = false, cancelled = false;
  int code = 0, warning = 0;
  char name[96]{"unavailable"};
};
static SurfaceBuildResult SurfaceReadBuildResult(void *process) {
  SurfaceBuildResult r; void *box = nullptr;
  if (!process || !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(process), "get_Result", "BeyondDynamicBone.ResultCode"), process, nullptr, box) || !box) return r;
  auto cls = il2cpp_object_get_class(box); uint32_t alignment = 0;
  auto code = CollisionFieldInfo(cls,"result","BeyondDynamicBone.Define.Result");
  auto warning = CollisionFieldInfo(cls,"warning","BeyondDynamicBone.Define.Result");
  if (!il2cpp_class_value_size || il2cpp_class_value_size(cls,&alignment) != 8 ||
      !code || !warning || il2cpp_field_get_offset(code) != 16 || il2cpp_field_get_offset(warning) != 20) return r;
  if (!CollisionField(box,"result","BeyondDynamicBone.Define.Result",r.code) ||
      !CollisionField(box,"warning","BeyondDynamicBone.Define.Result",r.warning) ||
      !ClothValue(SurfaceMethod(cls,"IsError","System.Boolean"),(char *)box+16,r.error) ||
      !ClothValue(SurfaceMethod(cls,"IsCancel","System.Boolean"),(char *)box+16,r.cancelled)) return r;
  r.known = true;
  void *name = nullptr;
  if (ClothInvoke(SurfaceMethod(cls,"GetResultString","System.String"),(char *)box+16,nullptr,name) && name)
    ReadStrUtf8(name,r.name,sizeof(r.name));
  return r;
}
static bool SurfaceReadArray(void *vm, const char *field, const char *type, int stride, std::vector<unsigned char> &out, int maximum=256) {
  char container[192]{}; _snprintf_s(container, _TRUNCATE, "BeyondDynamicBone.ExSimpleNativeArray<%s>", type);
  void *array = nullptr; int count = -1;
  if (!CollisionField(vm, field, container, array) || !array ||
      !ClothValue(ClothMethod(il2cpp_object_get_class(array), "get_Count", "System.Int32"), array, count) || count < 0 || maximum<1 || maximum>1024 || count > maximum) return false;
  auto item = SurfaceMethod(il2cpp_object_get_class(array), "get_Item", type, "System.Int32");
  if (!item) return false;
  out.resize(size_t(count)*stride);
  for (int n = 0; n < count; ++n) {
    void *box = nullptr, *args[]{&n};
    if (!ClothInvoke(item, array, args, box) || !box) return false;
    if (!strcmp(type, "Unity.Mathematics.int2") || !strcmp(type, "Unity.Mathematics.int3")) {
      auto cls = il2cpp_object_get_class(box); uint32_t align = 0;
      if (!CollisionType(il2cpp_class_get_type(cls), type) || il2cpp_class_value_size(cls, &align) != stride) return false;
      const char *fields[]{"x", "y", "z"};
      for (int k = 0; k < stride/4; ++k)
        if (ClothValueOffset(cls, fields[k], "System.Int32", stride, 4) != k*4) return false;
      memcpy(out.data()+size_t(n)*stride, (char *)box+16, stride);
    } else if (!strcmp(type,"BeyondDynamicBone.VirtualMeshBoneWeight")) {
      auto cls = il2cpp_object_get_class(box); uint32_t align = 0;
      if (stride != 32 || !CollisionType(il2cpp_class_get_type(cls),type) ||
          il2cpp_class_value_size(cls,&align) != 32 ||
          ClothValueOffset(cls,"weights","Unity.Mathematics.float4",32,16) != 0 ||
          ClothValueOffset(cls,"boneIndices","Unity.Mathematics.int4",32,16) != 16) return false;
      const char *fields[]{"weights","boneIndices"}, *types[]{"Unity.Mathematics.float4","Unity.Mathematics.int4"};
      for (int k = 0; k < 2; ++k) {
        auto field = CollisionFieldInfo(cls,fields[k],types[k]);
        auto nested = field ? il2cpp_class_from_type(il2cpp_field_get_type(field)) : nullptr;
        if (!nested || il2cpp_class_value_size(nested,&align) != 16) return false;
        const char *components[]{"x","y","z","w"};
        for (int c = 0; c < 4; ++c)
          if (ClothValueOffset(nested,components[c],k ? "System.Int32" : "System.Single",16,4) != c*4) return false;
      }
      memcpy(out.data()+size_t(n)*32,(char *)box+16,32);
    } else if (!ClothInputCopyBox(box, type, out.data()+size_t(n)*stride, stride)) return false;
  }
  return true;
}
static bool SurfaceUnregistered(void *process, int team) {
  void *manager = nullptr, *dictionary = nullptr;
  if (!ClothContactManager("get_Team", "BeyondDynamicBone.TeamManager", manager) ||
      !CollisionField(manager, "clothProcessDict", "System.Collections.Generic.Dictionary<System.Int32,BeyondDynamicBone.ClothProcess>", dictionary) || !dictionary) return false;
  auto cls = il2cpp_object_get_class(dictionary);
  void *contains = nullptr, *args[]{&team};
  if (!ClothInvoke(SurfaceMethod(cls, "ContainsKey", "System.Boolean", "System.Int32"), dictionary, args, contains) || !contains) return false;
  if (!ClothUnboxBool(contains)) return true;
  void *registered = nullptr;
  return ClothInvoke(SurfaceMethod(cls, "get_Item", "BeyondDynamicBone.ClothProcess", "System.Int32"), dictionary, args, registered) &&
      registered && registered != process;
}
static bool SurfaceOutputMatrix(void *t, eiem_cloth_surface::OutputMatrix &out) {
  void *box=nullptr; float value[16]{};
  if (!t || !ClothInvoke(SurfaceMethod(g_transformClass,"get_localToWorldMatrix","UnityEngine.Matrix4x4"),t,nullptr,box) ||
      !ClothInputCopyBox(box,"UnityEngine.Matrix4x4",value,64)) return false;
  for (int c=0;c<16;++c) out.v[c]=value[c];
  return eiem_cloth_surface::OutputAffine(out);
}
