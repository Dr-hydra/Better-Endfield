#pragma once
// Adapted from Sasye/EIEM 2bd3021, AGPL-3.0; see licenses/cloth-upstream.txt.
static void ClothInstallWeightWriterMainThread(void *method) {
  static bool attempted = false;
  if (attempted || !ClothOnMainThread() || !method) return;
  attempted = true;
  const bool ok = Hook(method, "BeyondBoneCloth.SetClothSimulateWeight(float)",
      reinterpret_cast<void *>(ClothNativeWeightWriter), reinterpret_cast<void **>(&s_clothOriginalWeightWriter));
  Log("[CLOTH-WRITER-HOOK] installed=%d tid=%lu gameAssembly=%p target=%p policy=observe-two-frames-before-owner-only-override fallback=normal-startup-readback",
      ok, GetCurrentThreadId(), hGA, ((MInfo *)method)->mp);
}
static void ClothInstallInputTraceMainThread() {
  static bool attempted = false;
  if (attempted || !ClothOnMainThread() || (!s_cloth.active&&!ClothPrefetchNeedsHooks())) return;
  attempted = true;
  size_t count = 0;
  void **asms = il2cpp_domain_get_assemblies(il2cpp_domain_get(), &count);
  auto manager = FindClass("BeyondDynamicBone", "ClothManager", asms, count);
  auto transforms = FindClass("BeyondDynamicBone", "DynamicBoneTransformManager", asms, count);
  auto job = FindClass("Unity.Jobs", "JobHandle", asms, count);
  s_clothInputManagerClass = FindClass("BeyondDynamicBone", "MagicaManager", asms, count);
  void *update = ClothMethod(manager, "ClothUpdate", "System.Void");
  void *valid = ClothMethod(transforms, "ValidPosition", "Unity.Jobs.JobHandle", "Unity.Jobs.JobHandle");
  void *read = ClothMethod(transforms, "ReadTransform", "Unity.Jobs.JobHandle", "Unity.Jobs.JobHandle");
  void *write = ClothMethod(transforms, "WriteDoubleBufferTransform", "Unity.Jobs.JobHandle", "Unity.Jobs.JobHandle");
  uint32_t alignment = 0;
  if (!update || !valid || !read || !write || !job || !s_clothInputManagerClass ||
      il2cpp_class_value_size(job, &alignment) != sizeof(ClothInputJobHandle) ||
      ClothValueOffset(job, "jobGroup", "System.UInt64", 16, 8) != 0 ||
      ClothValueOffset(job, "jobType", "System.Int32", 16, 4) != 8) {
    ClothInputIssue("native-schedule-signature-or-JobHandle-ABI-unavailable"); return;
  }
  const struct { void *method; size_t bytes; uint64_t hash; } checks[] = {
      {update, ClothInputAuditedBytes, 0x193870b55e754196ULL},
      {read, 606, 0x06f2080631f2bdfdULL},
      {write, 510, 0x1e02e013a66825ddULL},
      {valid, 689, 0xcf3fded511b5d62fULL}};
  for (const auto &check : checks) {
    auto code = (unsigned char *)((MInfo *)check.method)->mp;
    MEMORY_BASIC_INFORMATION region{};
    if (!code || !VirtualQuery(code, &region, sizeof(region)) || region.AllocationBase != hGA ||
        region.State != MEM_COMMIT || code + check.bytes > (unsigned char *)region.BaseAddress + region.RegionSize ||
        eiem_cloth_input::Fingerprint(code, check.bytes) != check.hash) {
      ClothInputIssue("native-completion-path-fingerprint-mismatch"); return;
    }
  }
  auto code = (unsigned char *)((MInfo *)update)->mp;
  auto destination = (unsigned char *)((MInfo *)valid)->mp;
  void *callsite = nullptr;
  for (size_t n = 0; n + 5 <= ClothInputAuditedBytes; ++n) {
    int32_t relative = 0;
    if (code[n] != 0xe8) continue;
    memcpy(&relative, code + n + 1, sizeof(relative));
    if (code + n + 5 + relative != destination) continue;
    if (callsite) { ClothInputIssue("completion-callsite-ambiguous"); return; }
    callsite = code + n + 5;
  }
  if (!callsite) { ClothInputIssue("completion-callsite-missing"); return; }
  auto teamClass = FindClass("BeyondDynamicBone", "TeamManager", asms, count);
  auto teamMethod = ClothMethod(teamClass, "AlwaysTeamUpdate", "System.Void");
  auto teamCode = teamMethod ? (unsigned char *)((MInfo *)teamMethod)->mp : nullptr;
  void *teamCallsite = nullptr;
  MEMORY_BASIC_INFORMATION teamRegion{};
  const bool teamVerified = teamCode && VirtualQuery(teamCode, &teamRegion, sizeof(teamRegion)) &&
      teamRegion.AllocationBase == hGA && teamRegion.State == MEM_COMMIT &&
      teamCode + 12716 <= (unsigned char *)teamRegion.BaseAddress + teamRegion.RegionSize &&
      eiem_cloth_input::Fingerprint(teamCode, 12716) == 0xe078f5c4cb56cbd8ULL;
  if (teamVerified) {
    unsigned matches = 0;
    for (size_t n = 0; n + 5 <= ClothInputAuditedBytes; ++n) {
      if (code[n] != 0xe8) continue;
      int32_t relative = 0; memcpy(&relative, code+n+1, sizeof(relative));
      if (code+n+5+relative == teamCode) { teamCallsite = code+n+5; ++matches; }
    }
    if (matches != 1) teamCallsite = nullptr;
  }
  if (!Hook(valid, "DynamicBoneTransformManager.ValidPosition(input observer)",
            (void *)ClothInputNativeValid, (void **)&s_clothInputOriginalValid) ||
      !Hook(update, "ClothManager.ClothUpdate(input observer)",
            (void *)ClothInputNativeUpdate, (void **)&s_clothInputOriginalUpdate)) {
    ClothInputIssue("native-input-hook-install-failed"); return;
  }
  s_clothInputUpdateCode = code;
  s_clothInputCallsite = callsite;
  s_clothInputPatchedFingerprint = eiem_cloth_input::Fingerprint(code, ClothInputAuditedBytes);
  auto readByteConstant = [&](const char *name) {
    void *field = CollisionFieldInfo(transforms, name, "System.Byte");
    if (!field || (il2cpp_field_get_flags(field) & 0x50) != 0x50) return -1;
    unsigned char value[8]{}; il2cpp_field_static_get_value(field, value); return int(value[0]);
  };
  s_clothInputReadMask = readByteConstant("Flag_Read");
  s_clothInputEnableMask = readByteConstant("Flag_Enable");
  s_clothInputHooks = true;
  if (teamCallsite && Hook(teamMethod, "TeamManager.AlwaysTeamUpdate(private cloth lifetime)",
                         (void *)ClothSurfaceNativeTeamUpdate, (void **)&s_clothSurfaceOriginalTeamUpdate)) {
    s_clothSurfaceTeamCallsite = teamCallsite;
    s_clothSurfaceTeamCode = teamCode;
    s_clothSurfaceTeamFingerprint = eiem_cloth_input::Fingerprint(teamCode, 128);
    s_clothSurfaceHook = true;
  }
  Log("[CLOTH-SURFACE-HOOK] installed=%d teamFingerprint=%d exactCallsite=%d boundary=previous-master-completed-before-AlwaysTeamUpdate crossFrame=1 forcedComplete=0",
      s_clothSurfaceHook, teamVerified, teamCallsite != nullptr);
  Log("[CLOTH-INPUT-HOOK] installed=1 mainThread=%lu branch=Transform-cross-frame boundary=native-completed-read-and-double-buffer-before-ValidPosition traceReadOnly=1 visibleAPose=separate-exclusive-lease workerHook=0 forcedComplete=0",
      GetCurrentThreadId());
}
#include "cloth/collision/cloth_contact_job_install.h"
#include "cloth/collision/cloth_layer_order_install.h"
#include "cloth/collision/cloth_elastic_install.h"
#include "cloth/collision/cloth_display_install.h"
#include "cloth/collision/cloth_contact_finish_install.h"
#include <bcrypt.h>
#include "cloth/resources/cloth_bonecloth_catalog_io.h"
#pragma comment(lib,"bcrypt.lib")
static bool ClothCatalogIndexMatches(const std::wstring &path,const eiem_cloth_cache::Catalog::Source &source) {
  HANDLE file=CreateFileW(path.c_str(),GENERIC_READ,FILE_SHARE_READ,nullptr,OPEN_EXISTING,FILE_ATTRIBUTE_NORMAL,nullptr);
  if(file==INVALID_HANDLE_VALUE) {const DWORD error=GetLastError();return !source.present&&(error==ERROR_FILE_NOT_FOUND||error==ERROR_PATH_NOT_FOUND);}
  if(!source.present) {CloseHandle(file);return false;}
  LARGE_INTEGER size{};BCRYPT_ALG_HANDLE algorithm=nullptr;BCRYPT_HASH_HANDLE hash=nullptr;
  bool ok=GetFileSizeEx(file,&size)&&size.QuadPart>0&&size.QuadPart<=64*1024*1024&&
      BCryptOpenAlgorithmProvider(&algorithm,BCRYPT_SHA256_ALGORITHM,nullptr,0)>=0;
  if(ok)ok=BCryptCreateHash(algorithm,&hash,nullptr,0,nullptr,0,0)>=0;
  unsigned char buffer[65536],digest[32]{};DWORD read=0;uint64_t total=0;
  while(ok) {
    if(!ReadFile(file,buffer,sizeof(buffer),&read,nullptr)){ok=false;break;}
    if(!read)break;total+=read;
    ok=total<=uint64_t(size.QuadPart)&&BCryptHashData(hash,buffer,read,0)>=0;
  }
  if(ok)ok=total==uint64_t(size.QuadPart)&&BCryptFinishHash(hash,digest,sizeof(digest),0)>=0;
  if(hash)BCryptDestroyHash(hash);if(algorithm)BCryptCloseAlgorithmProvider(algorithm,0);CloseHandle(file);
  char hex[65]{};for(int n=0;n<32;++n)sprintf_s(hex+2*n,3,"%02x",unsigned(digest[n]));
  return ok&&source.hash==hex;
}
static void ClothLoadPreparedCatalog() {
  HMODULE module=nullptr;
  if(!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
      reinterpret_cast<LPCWSTR>(&ClothLoadPreparedCatalog),&module)) return;
  DWORD resourceError=0;const auto resourceStart=GetTickCount64();
  const bool resourcesReady=eiem_cloth_resource::Embedded.Prepare(module,resourceError);
  Log("[CLOTH-RESOURCES] embeddedReady=%d elapsedMs=%llu win32=%lu decodedBeforeHooks=1 playbackDecode=0",
      resourcesReady,(unsigned long long)(GetTickCount64()-resourceStart),resourceError);
  std::vector<unsigned char> buffer;DWORD error=0;
  const auto source=eiem_cloth_cache::LoadBytes(module,buffer,error);
  s_clothBoneCatalogSource=eiem_cloth_cache::SourceName(source);
  if(source==eiem_cloth_cache::Source::Rejected || !s_clothBoneCache.Parse(buffer.data(),buffer.size())) {
    Log("[CLOTH-CATALOG] source=%s status=rejected win32=%lu original-components-unchanged=1",s_clothBoneCatalogSource,error);
    s_clothBoneCatalogSource="rejected";s_clothBoneCatalog.clear();return;
  }
  s_clothBoneCatalog.clear();
  wchar_t executable[32768]{};const DWORD length=GetModuleFileNameW(nullptr,executable,DWORD(std::size(executable)));
  auto base=length&&length<std::size(executable)?wcsrchr(executable,L'\\'):nullptr;
  if(!base) {s_clothBoneCatalogSource="executable-path-unavailable";Log("[CLOTH-CATALOG] status=%s",s_clothBoneCatalogSource);return;}base[1]=0;
  for(const auto &source:s_clothBoneCache.sources) {
    std::wstring fileName=executable;fileName.append(source.path.begin(),source.path.end());
    if(!ClothCatalogIndexMatches(fileName,source)) {
      Log("[CLOTH-CATALOG] source=%s status=installed-index-mismatch path=%s original-components-unchanged=1",s_clothBoneCatalogSource,source.path.c_str());
      s_clothBoneCatalogSource="installed-index-mismatch";
      s_clothBoneCatalog.clear();s_clothBoneCache={};return;
    }
  }
  size_t added=0;
  s_clothBoneCatalog.clear();
  for(auto p:s_clothBoneCache.profiles) {
    auto same=std::find_if(std::begin(ClothBoneProfiles),std::end(ClothBoneProfiles),[p](const ClothBoneProfile *b){return !strcmp(b->signature,p->signature);});
    if(same!=std::end(ClothBoneProfiles))s_clothBoneCatalog.push_back(*same);
    else {s_clothBoneCatalog.push_back(p);++added;}
  }
  Log("[CLOTH-CATALOG] source=%s status=loaded key=%s profiles=%zu added=%zu total=%zu liveValidation=required denseSkin=separate visualVerified=0",
      s_clothBoneCatalogSource,s_clothBoneCache.key.c_str(),s_clothBoneCache.profiles.size(),added,s_clothBoneCatalog.size());
}
static void ClothWarmInstalledSource() {
  try {
    const auto start=GetTickCount64();eiem_cloth_asset::WarmInstalled(ClothAutoDataRoot());
    Log("[CLOTH-AUTO] stage=index-warmup elapsedMs=%llu workerUnityCalls=0 beforeHooks=1",(unsigned long long)(GetTickCount64()-start));
  } catch(const std::exception &e) {Log("[CLOTH-AUTO] stage=index-warmup-deferred reason=%s",e.what());}
}

static bool ClothInitializeHost() {
  HMODULE keepAlive=nullptr;
  if(!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_PIN,
      reinterpret_cast<LPCWSTR>(&ClothInitializeHost), &keepAlive)) {
    Log("[CLOTH] module lifetime unavailable; enhancement disabled");
    s_clothAutoEnabled.store(false);s_clothSquadAutoEnabled.store(false);return false;
  }
  s_clothWeightHookInstaller=ClothInstallWeightWriterMainThread;
  s_clothInputHookInstaller=ClothInstallInputTraceMainThread;
  s_clothContactJobInstaller=ClothInstallContactJobsMainThread;
  s_clothLayerInstaller=ClothInstallLayerOrderMainThread;
  s_clothElasticInstaller=ClothInstallElasticMainThread;
  s_clothDisplayInstaller=ClothInstallDisplayMainThread;
  s_clothFinishInstaller=ClothInstallFinishMainThread;
  ClothLoadPreparedCatalog();
  ClothWarmInstalledSource();
  return true;
}
