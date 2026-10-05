#pragma once
static const char *CollisionModeType = "BeyondDynamicBone.ColliderCollisionConstraint.Mode";
static const char *CollisionAngleType =
    "BeyondDynamicBone.AngleConstraint.RestorationSerializeData";
static bool CollisionAngleConfig(void *serialize, void *&data, int &enabled) {
  bool value = false;
  if (!CollisionField(serialize, "angleRestorationConstraint", CollisionAngleType, data) || !data ||
      !ClothField(data, "useAngleRestoration", "System.Boolean", value))
    return false;
  enabled = value ? 1 : 0;
  return true;
}
static bool CollisionProcessAngle(void *process, int &value) {
  void *box = nullptr;
  if (!il2cpp_class_from_type || !il2cpp_class_value_size || !il2cpp_field_get_flags || !process ||
      !ClothInvoke(ClothMethod(il2cpp_object_get_class(process), "get_parameters",
                               "BeyondDynamicBone.ClothParameters"),
                   process, nullptr, box) ||
      !box)
    return false;
  auto cls = il2cpp_object_get_class(box);
  auto f = CollisionFieldInfo(cls, "angleConstraint",
                              "BeyondDynamicBone.AngleConstraint.AngleConstraintParams");
  if (!f || (il2cpp_field_get_flags(f) & 0x10))
    return false;
  auto nested = il2cpp_class_from_type(il2cpp_field_get_type(f));
  auto flag =
      nested ? CollisionFieldInfo(nested, "useAngleRestoration", "System.Boolean") : nullptr;
  if (!flag || (il2cpp_field_get_flags(flag) & 0x10))
    return false;
  uint32_t align = 0;
  int size = il2cpp_class_value_size(cls, &align),
      innerSize = il2cpp_class_value_size(nested, &align);
  size_t offset = il2cpp_field_get_offset(f), inner = il2cpp_field_get_offset(flag);
  if (size <= 0 || size > 65536 || innerSize <= 0 || offset < 16 || inner < 16 ||
      offset + innerSize > size_t(size) + 16 || inner + 1 > size_t(innerSize) + 16)
    return false;
  unsigned char b = 0;
  memcpy(&b, static_cast<char *>(box) + offset + inner - 16, 1);
  if (b > 1)
    return false;
  value = b;
  return true;
}
static bool CollisionReferenceAssign(void *object, const char *name, const char *type,
                                     void *expected, void *replacement, const char *phase) {
  if (!ClothOnMainThread() || !object || !expected || !replacement ||
      !il2cpp_field_set_value_object || !il2cpp_field_get_flags)
    return false;
  bool completed = false, readable = false;
  void *current = nullptr;
  __try {
    auto field = CollisionFieldInfo(il2cpp_object_get_class(object), name, type);
    if (!field || (il2cpp_field_get_flags(field) & 0x10) ||
        !CollisionField(object, name, type, current) || current != expected)
      return false;
    il2cpp_field_set_value_object(object, field, replacement);
    completed = true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
  }
  readable = CollisionField(object, name, type, current);
  const bool matched = readable && current == replacement;
  Log("[CLOTH-CONTACT-REFERENCE] phase=%s session=%llu frame=%d object=%p field=%s expected=%p "
      "target=%p actual=%p apiCompleted=%d slotReadable=%d matched=%d",
      phase, (unsigned long long)s_cloth.owner.session, ClothFrame(), object, name, expected,
      replacement, current, completed, readable, matched);
  return completed && matched;
}
struct CollisionTopology {
  bool known = false;
  int vertices = -1, edges = -1, lines = -1, triangles = -1;
  void *container = nullptr, *mesh = nullptr;
};
static CollisionTopology CollisionReadTopology(void *process) {
  CollisionTopology t{};
  if (!process ||
      !ClothInvoke(ClothMethod(il2cpp_object_get_class(process), "get_ProxyMeshContainer",
                               "BeyondDynamicBone.VirtualMeshContainer"),
                   process, nullptr, t.container) ||
      !t.container ||
      !CollisionField(t.container, "shareVirtualMesh", "BeyondDynamicBone.VirtualMesh", t.mesh) ||
      !t.mesh)
    return t;
  auto cls = il2cpp_object_get_class(t.mesh);
  bool proxy = false;
  t.known =
      ClothValue(ClothMethod(cls, "get_IsProxy", "System.Boolean"), t.mesh, proxy) && proxy &&
      ClothValue(ClothMethod(cls, "get_VertexCount", "System.Int32"), t.mesh, t.vertices) &&
      ClothValue(ClothMethod(cls, "get_EdgeCount", "System.Int32"), t.mesh, t.edges) &&
      ClothValue(ClothMethod(cls, "get_LineCount", "System.Int32"), t.mesh, t.lines) &&
      ClothValue(ClothMethod(cls, "get_TriangleCount", "System.Int32"), t.mesh, t.triangles) &&
      t.vertices > 0 && t.vertices <= 65535 && t.edges >= 0 && t.edges <= 262144 && t.lines >= 0 &&
      t.lines <= 262144 && t.triangles >= 0 && t.triangles <= 262144;
  return t;
}
static bool CollisionModeMetadata(void *cls, void *&field, int &point, int &edge) {
  field = CollisionFieldInfo(cls, "mode", CollisionModeType);
  if (!field || !il2cpp_class_from_type || !il2cpp_field_get_flags ||
      (il2cpp_field_get_flags(field) & 0x10))
    return false;
  auto type = il2cpp_class_from_type(il2cpp_field_get_type(field));
  return CollisionEnumValue(type, "Point", point) && CollisionEnumValue(type, "Edge", edge) &&
         point != edge;
}
static bool CollisionParameterMode(void *box, int &value) {
  if(!box || !il2cpp_class_from_type || !il2cpp_class_value_size) return false;
  auto cls = il2cpp_object_get_class(box);
  auto f = CollisionFieldInfo(
      cls, "colliderCollisionConstraint",
      "BeyondDynamicBone.ColliderCollisionConstraint.ColliderCollisionConstraintParams");
  if (!f || !il2cpp_field_get_flags || (il2cpp_field_get_flags(f) & 0x10))
    return false;
  auto nested = il2cpp_class_from_type(il2cpp_field_get_type(f));
  void *mode = nullptr;
  int point = 0, edge = 0;
  if (!nested || !CollisionModeMetadata(nested, mode, point, edge))
    return false;
  uint32_t align = 0;
  const int size = il2cpp_class_value_size(cls, &align);
  const int nestedSize = il2cpp_class_value_size(nested, &align);
  const size_t offset = il2cpp_field_get_offset(f), inner = il2cpp_field_get_offset(mode);
  if (size <= 0 || size > 65536 || nestedSize < 4 || offset < 16 || inner < 16 ||
      offset + nestedSize > size_t(size) + 16 || inner + 4 > size_t(nestedSize) + 16)
    return false;
  memcpy(&value, static_cast<char *>(box) + offset + inner - 16, sizeof(value));
  return true;
}
static bool CollisionProcessMode(void *process, int &value) {
  void *box=nullptr;
  return process && ClothInvoke(ClothMethod(il2cpp_object_get_class(process),"get_parameters",
      "BeyondDynamicBone.ClothParameters"),process,nullptr,box) && CollisionParameterMode(box,value);
}
