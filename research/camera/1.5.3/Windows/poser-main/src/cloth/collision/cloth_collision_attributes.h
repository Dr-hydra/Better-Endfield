#pragma once
struct CollisionBoneAttribute {
  bool queried = false, present = false, known = false;
  unsigned char value = 0;
};
struct CollisionAttributeReport {
  bool dictionaryKnown = false, flagsKnown = false, prebuildKnown = false, prebuild = false;
  bool rawKnown = false;
  int dictionaryCount = -1, rawCount = -1, rawLength = -1;
  unsigned char fixedFlag = 0, moveFlag = 0, disableFlag = 0;
  std::vector<unsigned char> rawBytes;
  bool selectionKnown = false;
  std::vector<unsigned char> selectionValues;
};
struct CollisionAttributeReader {
  eiem_cloth::Owner owner{};
  void *dictionary = nullptr, *contains = nullptr, *item = nullptr;
  void *attributeClass = nullptr;
};
static bool CollisionByteFlag(void *cls, const char *name, unsigned char &value) {
  if (!il2cpp_field_get_flags || !il2cpp_field_static_get_value)
    return false;
  auto f = CollisionFieldInfo(cls, name, "System.Byte");
  if (!f || (il2cpp_field_get_flags(f) & 0x50) != 0x50)
    return false;
  il2cpp_field_static_get_value(f, &value);
  return value && !(value & (value - 1));
}
static CollisionAttributeReader CollisionReadAttributes(void *bbc, CollisionAttributeReport &r) {
  CollisionAttributeReader reader{};
  r = {};
  if (!bbc || !ClothOnMainThread() || !ClothOwns(s_cloth.owner))
    return reader;
  reader.owner = s_cloth.owner;
  void *data = nullptr;
  if (!ClothInvoke(ClothMethod(il2cpp_object_get_class(bbc), "GetSerializeData2",
                               "BeyondDynamicBone.ClothSerializeData2"),
                   bbc, nullptr, data) ||
      !data)
    return reader;
  if (CollisionField(data, "boneAttributeDict",
                     "System.Collections.Generic.Dictionary<UnityEngine.Transform,"
                     "BeyondDynamicBone.VertexAttribute>",
                     reader.dictionary) &&
      reader.dictionary) {
    r.dictionaryCount = CollisionCount(reader.dictionary);
    auto cls = il2cpp_object_get_class(reader.dictionary);
    reader.contains = ClothMethod(cls, "ContainsKey", "System.Boolean", "UnityEngine.Transform");
    reader.item =
        ClothMethod(cls, "get_Item", "BeyondDynamicBone.VertexAttribute", "UnityEngine.Transform");
    r.dictionaryKnown =
        r.dictionaryCount >= 0 && r.dictionaryCount <= 2048 && reader.contains && reader.item;
    if (r.dictionaryKnown && il2cpp_class_from_type && il2cpp_class_value_size) {
      reader.attributeClass = il2cpp_class_from_type(il2cpp_method_get_return_type(reader.item));
      uint32_t align = 0;
      r.flagsKnown =
          reader.attributeClass && il2cpp_class_value_size(reader.attributeClass, &align) == 1 &&
          CollisionByteFlag(reader.attributeClass, "Flag_Fixed", r.fixedFlag) &&
          CollisionByteFlag(reader.attributeClass, "Flag_Move", r.moveFlag) &&
          CollisionByteFlag(reader.attributeClass, "Flag_DisableCollision", r.disableFlag) &&
          r.fixedFlag != r.moveFlag && r.fixedFlag != r.disableFlag && r.moveFlag != r.disableFlag;
    }
    if (!r.dictionaryKnown)
      reader.contains = reader.item = nullptr;
  }
  void *selection = nullptr, *selectionArray = nullptr;
  uintptr_t selectionCount = 0;
  uint32_t attributeAlign = 0;
  if (reader.attributeClass && il2cpp_class_value_size &&
      il2cpp_class_value_size(reader.attributeClass, &attributeAlign) == 1 &&
      CollisionField(data, "selectionData", "BeyondDynamicBone.SelectionData", selection) &&
      selection &&
      ClothField(selection, "attributes", "BeyondDynamicBone.VertexAttribute[]", selectionArray) &&
      ClothArray(selectionArray, "BeyondDynamicBone.VertexAttribute[]", selectionCount) &&
      selectionCount <= 2048) {
    const auto begin = static_cast<const unsigned char *>(selectionArray) + 32;
    r.selectionValues.assign(begin, begin + selectionCount);
    r.selectionKnown = true;
  }
  void *pre = nullptr, *share = nullptr, *proxy = nullptr, *attributes = nullptr, *bytes = nullptr;
  if (!CollisionField(data, "preBuildData", "BeyondDynamicBone.PreBuildSerializeData", pre) || !pre)
    return reader;
  r.prebuildKnown = ClothValue(
      ClothMethod(il2cpp_object_get_class(pre), "UsePreBuild", "System.Boolean"), pre, r.prebuild);
  if (!r.prebuildKnown || !r.prebuild ||
      !ClothInvoke(ClothMethod(il2cpp_object_get_class(pre), "GetSharePreBuildData",
                               "BeyondDynamicBone.SharePreBuildData"),
                   pre, nullptr, share) ||
      !share ||
      !CollisionField(share, "proxyMesh", "BeyondDynamicBone.VirtualMesh.ShareSerializationData",
                      proxy) ||
      !proxy ||
      !CollisionField(proxy, "attributes",
                      "BeyondDynamicBone.ExSimpleNativeArray.SerializationData<BeyondDynamicBone."
                      "VertexAttribute>",
                      attributes) ||
      !attributes || !ClothField(attributes, "count", "System.Int32", r.rawCount) ||
      !ClothField(attributes, "length", "System.Int32", r.rawLength) ||
      !ClothField(attributes, "arrayBytes", "System.Byte[]", bytes))
    return reader;
  uintptr_t byteCount = 0;
  if (r.rawCount < 0 || r.rawCount > 2048 || r.rawLength < r.rawCount || r.rawLength > 8192 ||
      !ClothArray(bytes, "System.Byte[]", byteCount) || byteCount > 4096)
    return reader;
  const auto begin = static_cast<const unsigned char *>(bytes) + 32;
  r.rawBytes.assign(begin, begin + byteCount);
  r.rawKnown = true;
  return reader;
}
static CollisionBoneAttribute CollisionReadBoneAttribute(const CollisionAttributeReader &reader,
                                                         void *bone) {
  CollisionBoneAttribute a{};
  if (!ClothOnMainThread() || !ClothOwns(reader.owner) || !reader.dictionary || !reader.contains ||
      !reader.item || !bone)
    return a;
  void *args[] = {bone}, *box = nullptr;
  if (!ClothInvoke(reader.contains, reader.dictionary, args, box) || !box)
    return a;
  a.queried = true;
  a.present = ClothUnboxBool(box);
  if (!a.present)
    return a;
  if (!ClothInvoke(reader.item, reader.dictionary, args, box) || !box || !reader.attributeClass ||
      il2cpp_object_get_class(box) != reader.attributeClass)
    return a;
  uint32_t align = 0;
  a.known = il2cpp_class_value_size &&
            il2cpp_class_value_size(reader.attributeClass, &align) == 1 &&
            ClothValueOffset(reader.attributeClass, "Value", "System.Byte", 1, 1) == 0 &&
            ClothField(box, "Value", "System.Byte", a.value);
  return a;
}
