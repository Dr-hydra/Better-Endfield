#pragma once
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <windows.h>
#include "MinHook.h"

void Log(const char *fmt, ...);

#define D(ret, name, ...)                                                      \
  typedef ret (*t_##name)(__VA_ARGS__);                                        \
  static t_##name name = nullptr
D(void *, il2cpp_domain_get);
D(void *, il2cpp_thread_attach, void *);
D(void **, il2cpp_domain_get_assemblies, void *, size_t *);
D(void *, il2cpp_assembly_get_image, void *);
D(const char *, il2cpp_image_get_name, void *);
D(size_t, il2cpp_image_get_class_count, void *);
D(void *, il2cpp_image_get_class, void *, size_t);
D(void *, il2cpp_class_get_methods, void *, void **);
D(const char *, il2cpp_method_get_name, void *);
D(uint32_t, il2cpp_method_get_param_count, void *);
D(const char *, il2cpp_class_get_name, void *);
D(const char *, il2cpp_class_get_namespace, void *);
D(void *, il2cpp_object_get_class, void *);
D(void *, il2cpp_class_from_name, void *, const char *, const char *);
D(void *, il2cpp_method_get_param, void *, uint32_t);
D(const char *, il2cpp_type_get_name, void *);
D(void *, il2cpp_class_get_fields, void *, void **);
D(const char *, il2cpp_field_get_name, void *);
D(size_t, il2cpp_field_get_offset, void *);
D(int, il2cpp_field_get_flags, void *);
D(void *, il2cpp_class_get_method_from_name, void *, const char *, int);
D(void *, il2cpp_runtime_invoke, void *, void *, void **, void **);
D(void *, il2cpp_class_get_parent, void *);
D(int32_t, il2cpp_class_value_size, void *, uint32_t *);
D(void, il2cpp_field_static_get_value, void *, void *);
D(void *, il2cpp_field_get_type, void *);
D(int, il2cpp_type_get_type, void *);
D(void *, il2cpp_method_get_return_type, void *);
D(void *, il2cpp_class_from_type, void *);
D(void *, il2cpp_resolve_icall, const char *);
D(void *, il2cpp_string_new, const char *);
D(void *, il2cpp_class_get_type, void *);
D(void *, il2cpp_type_get_object, void *);
D(void *, il2cpp_object_new, void *);
D(void *, il2cpp_array_new_specific, void *, size_t);
D(void *, il2cpp_array_new, void *, size_t);
D(uint32_t, il2cpp_gchandle_new, void *, bool);
D(void *, il2cpp_gchandle_get_target, uint32_t);
D(void, il2cpp_gchandle_free, uint32_t);
#undef D

static HMODULE hGA = nullptr;
struct MInfo {
  void *mp;
};

struct Vector3 { float x, y, z; };
struct Quaternion { float x, y, z, w; };

static int ReadStr(void *s, char *b, int sz) {
  __try {
    if (!s) {
      b[0] = 0;
      return -1;
    }
    int32_t l = *(int32_t *)((char *)s + 16);
    if (l <= 0 || l > 2048) {
      b[0] = 0;
      return -1;
    }
    wchar_t *c = (wchar_t *)((char *)s + 20);
    int j = 0;
    for (int i = 0; i < l && j < sz - 1; i++)
      b[j++] = (c[i] < 128) ? (char)c[i] : '?';
    b[j] = 0;
    return j;
  } __except (1) {
    b[0] = 0;
    return -2;
  }
}

static int ReadStrUtf8(void *s, char *b, int sz) {
  __try {
    if (!s) { b[0] = 0; return -1; }
    int32_t l = *(int32_t *)((char *)s + 16);
    if (l <= 0 || l > 2048) { b[0] = 0; return -1; }
    wchar_t *c = (wchar_t *)((char *)s + 20);
    int written = WideCharToMultiByte(CP_UTF8, 0, c, l, b, sz - 1, NULL, NULL);
    b[written] = 0;
    return written;
  } __except (1) {
    b[0] = 0;
    return -2;
  }
}

static bool Resolve() {
  hGA = GetModuleHandleW(L"GameAssembly.dll");
  if (!hGA)
    return false;
#define R(n) n = (t_##n)GetProcAddress(hGA, #n)
  R(il2cpp_domain_get);
  R(il2cpp_thread_attach);
  R(il2cpp_domain_get_assemblies);
  R(il2cpp_assembly_get_image);
  R(il2cpp_image_get_name);
  R(il2cpp_image_get_class_count);
  R(il2cpp_image_get_class);
  R(il2cpp_class_get_methods);
  R(il2cpp_method_get_name);
  R(il2cpp_method_get_param_count);
  R(il2cpp_class_get_name);
  R(il2cpp_class_get_namespace);
  R(il2cpp_object_get_class);
  R(il2cpp_class_from_name);
  R(il2cpp_method_get_param);
  R(il2cpp_type_get_name);
  R(il2cpp_class_get_fields);
  R(il2cpp_field_get_name);
  R(il2cpp_field_get_offset);
  R(il2cpp_runtime_invoke);
  R(il2cpp_class_get_parent);
  R(il2cpp_class_value_size);
  R(il2cpp_field_static_get_value);
  R(il2cpp_field_get_type);
  R(il2cpp_type_get_type);
  R(il2cpp_method_get_return_type);
  R(il2cpp_class_from_type);
  R(il2cpp_resolve_icall);
  R(il2cpp_string_new);
  R(il2cpp_class_get_type);
  R(il2cpp_type_get_object);
  R(il2cpp_object_new);
  R(il2cpp_array_new_specific);
  R(il2cpp_array_new);
  R(il2cpp_gchandle_new);
  R(il2cpp_gchandle_get_target);
  R(il2cpp_gchandle_free);
#undef R
  return il2cpp_domain_get && il2cpp_class_get_methods &&
         il2cpp_method_get_name;
}

static void *FindMethod(void *k, const char *n, int pc) {
  if (!k)
    return nullptr;
  void *it = nullptr, *m;
  while ((m = il2cpp_class_get_methods(k, &it))) {
    const char *mn = il2cpp_method_get_name(m);
    if (mn && strcmp(mn, n) == 0 && (int)il2cpp_method_get_param_count(m) == pc)
      return m;
  }
  return nullptr;
}

static void *FindMethodInHierarchy(void *k, const char *n, int pc) {
  void *cur = k;
  int depth = 0;
  while (cur && depth < 10) {
    void *m = FindMethod(cur, n, pc);
    if (m)
      return m;
    cur = il2cpp_class_get_parent ? il2cpp_class_get_parent(cur) : nullptr;
    depth++;
  }
  return nullptr;
}

static void *FindClass(const char *ns, const char *n, void **a, size_t c) {
  for (size_t i = 0; i < c; i++) {
    void *img = il2cpp_assembly_get_image(a[i]);
    if (!img)
      continue;
    size_t cc = il2cpp_image_get_class_count(img);
    for (size_t j = 0; j < cc; j++) {
      void *k = il2cpp_image_get_class(img, j);
      if (!k)
        continue;
      const char *cn = il2cpp_class_get_name(k);
      if (!cn || strcmp(cn, n) != 0)
        continue;
      const char *kns =
          il2cpp_class_get_namespace ? il2cpp_class_get_namespace(k) : "";
      if (strcmp(kns ? kns : "", ns) == 0)
        return k;
    }
  }
  return nullptr;
}

static bool Hook(void *mi, const char *l, void *d, void **o) {
  if (!mi)
    return false;
  void *t = ((MInfo *)mi)->mp;
  if (!t)
    return false;
  if (MH_CreateHook(t, d, o) != MH_OK || MH_EnableHook(t) != MH_OK)
    return false;
  Log("[OK] %s hooked", l);
  return true;
}

static void *Invoke(void *method, void *obj, void **params = nullptr) {
  if (!method)
    return nullptr;
  __try {
    void *exc = nullptr;
    return il2cpp_runtime_invoke(method, obj, params, &exc);
  } __except (1) {
    return nullptr;
  }
}

static void DumpClassMethods(void *klass, const char *label) {
  if (!klass) return;
  const char *cn = il2cpp_class_get_name(klass);
  Log("=== Methods of %s (%s) ===", cn ? cn : "?", label);
  void *it = nullptr, *m;
  while ((m = il2cpp_class_get_methods(klass, &it))) {
    const char *mn = il2cpp_method_get_name(m);
    uint32_t pc = il2cpp_method_get_param_count(m);
    Log("  %s(%d params)", mn ? mn : "?", pc);
  }
}

static void DumpClassFields(void *klass, const char *label) {
  if (!klass) return;
  const char *cn = il2cpp_class_get_name(klass);
  Log("=== Fields of %s (%s) ===", cn ? cn : "?", label);
  void *it = nullptr, *f;
  while ((f = il2cpp_class_get_fields(klass, &it))) {
    const char *fn = il2cpp_field_get_name(f);
    size_t fo = il2cpp_field_get_offset(f);
    Log("  [0x%X] %s", (int)fo, fn ? fn : "?");
  }
}

static int FindFieldInHierarchy(void *klass, const char **names, int nameCount,
                                const char **outFieldName) {
  if (!klass || !il2cpp_class_get_parent)
    return -1;
  void *cur = klass;
  int depth = 0;
  while (cur && depth < 10) {
    void *it = nullptr, *f;
    while ((f = il2cpp_class_get_fields(cur, &it))) {
      const char *fn = il2cpp_field_get_name(f);
      if (!fn)
        continue;
      for (int i = 0; i < nameCount; i++) {
        if (strcmp(fn, names[i]) == 0) {
          if (outFieldName)
            *outFieldName = fn;
          return (int)il2cpp_field_get_offset(f);
        }
      }
    }
    cur = il2cpp_class_get_parent(cur);
    depth++;
  }
  return -1;
}

static void DumpFieldsHierarchy(void *klass) {
  if (!klass || !il2cpp_class_get_parent)
    return;
  void *cur = klass;
  int depth = 0;
  while (cur && depth < 10) {
    const char *cn = il2cpp_class_get_name(cur);
    Log("[WARN]   --- %s ---", cn ? cn : "?");
    void *it = nullptr, *f;
    while ((f = il2cpp_class_get_fields(cur, &it))) {
      const char *fn = il2cpp_field_get_name(f);
      int fo = (int)il2cpp_field_get_offset(f);
      if (fn)
        Log("[WARN]   [0x%X] %s", fo, fn);
    }
    cur = il2cpp_class_get_parent(cur);
    depth++;
  }
}
