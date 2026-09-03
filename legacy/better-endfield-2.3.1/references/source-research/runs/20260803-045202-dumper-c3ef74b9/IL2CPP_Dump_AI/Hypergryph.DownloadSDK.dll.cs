// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.DownloadSDK.dll
// Classes:  4
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

# AI-FRIENDLY STRUCTURED DUMP
# Optimized for LLM parsing / code generation

CLASS: <Module>
TYPE:  class
TOKEN: 0x2000001
SIZE:  0x1
FIELDS:
METHODS:
END_CLASS

CLASS: Hypergryph.SDK.IHGDownloadSDK
TYPE:  interface
TOKEN: 0x2000002
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000001  System.Int32 init(System.String config)
  RVA=-1  // abstract  token=0x6000002  System.Int64 download(System.String versionId, System.String downloadFiles, System.String decompressPath, System.Boolean useMobileData, System.Boolean needCompress, System.Boolean usePatch, System.String rootPath)
  RVA=-1  // abstract  token=0x6000003  System.Int64 updateV3(System.String versionId, System.String basePath, System.String rootPath, System.String fileUrl, System.String patchUrl, System.String totalFiles, System.String patchInfo, System.Boolean useMobileData, System.Int32 checkType)
  RVA=-1  // abstract  token=0x6000004  System.Int32 clearUselessFiles(System.String rootPath, System.String allFiles)
  RVA=-1  // abstract  token=0x6000005  System.Int32 enableMobileData(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000006  System.Int32 resume(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000007  System.Int32 cancel(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000008  System.Int32 clearAllTasks()
  RVA=-1  // abstract  token=0x6000009  System.Int32 finish(System.Int64 taskId)
  RVA=-1  // abstract  token=0x600000A  System.Int32 getSDKState()
  RVA=-1  // abstract  token=0x600000B  System.Int32 getTaskState(System.Int64 taskId)
  RVA=-1  // abstract  token=0x600000C  System.String getTaskInfo(System.Int64 taskId)
  RVA=-1  // abstract  token=0x600000D  System.Int64 getDownloadSpeed(System.Int64 taskId)
  RVA=-1  // abstract  token=0x600000E  System.Int64 getDownloadedSize(System.Int64 taskId)
  RVA=-1  // abstract  token=0x600000F  System.Int64 getTotalDownloadSize(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000010  System.Int32 getDecompressedProgress(System.Int64 taskId)
  RVA=-1  // abstract  token=0x6000011  System.Int32 setLanguageType(System.Int32 type)
  RVA=-1  // abstract  token=0x6000012  System.Int32 setNotificationTitle(System.String title)
  RVA=-1  // abstract  token=0x6000013  System.Int64 getEstimatedDownloadSize(System.String versionId, System.String downloadFiles)
  RVA=-1  // abstract  token=0x6000014  System.Int64 getEstimatedUpdateSizeV2(System.String versionId, System.String basePath, System.String rootPath, System.String fileUrl, System.String patchUrl, System.String totalFiles, System.String patchInfo)
END_CLASS

CLASS: Hypergryph.SDK.HGDownloadSDK
TYPE:  static class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
  private   static  Hypergryph.SDK.IHGDownloadSDK   s_dl  // static @ 0x0
PROPERTIES:
  downloadSDK  get=0x0480A7A0
METHODS:
  RVA=0x0480A750  token=0x6000016  System.Int32 Init(System.String config)
  RVA=0x09DDC8B4  token=0x6000017  System.Int64 Download(System.String versionId, System.String downloadFiles, System.Boolean useMobileData, System.String decompressPath, System.Boolean needCompress, System.Boolean usePatch, System.String rootPath)
  RVA=0x09DDCE98  token=0x6000018  System.Int64 UpdateV3(System.String versionId, System.String basePath, System.String rootPath, System.String fileUrl, System.String patchUrl, System.String totalFiles, System.String patchInfo, System.Boolean useMobileData, System.Int32 checkType)
  RVA=0x09DDC858  token=0x6000019  System.Int32 ClearUselessFiles(System.String rootPath, System.String allFiles)
  RVA=0x09DDC980  token=0x600001A  System.Int32 EnableMobileData(System.Int64 taskId)
  RVA=0x09DDCDA8  token=0x600001B  System.Int32 Resume(System.Int64 taskId)
  RVA=0x09DDC7B4  token=0x600001C  System.Int32 Cancel(System.Int64 taskId)
  RVA=0x09DDC80C  token=0x600001D  System.Int32 ClearAllTasks()
  RVA=0x09DDC9CC  token=0x600001E  System.Int32 Finish(System.Int64 taskId)
  RVA=0x09DDCC5C  token=0x600001F  System.Int32 GetSDKState()
  RVA=0x09DDCCF8  token=0x6000020  System.Int32 GetTaskState(System.Int64 taskId)
  RVA=0x09DDCCA8  token=0x6000021  System.String GetTaskInfo(System.Int64 taskId)
  RVA=0x09DDCA7C  token=0x6000022  System.Int64 GetDownloadSpeed(System.Int64 taskId)
  RVA=0x09DDCAD4  token=0x6000023  System.Int64 GetDownloadedSize(System.Int64 taskId)
  RVA=0x09DDCD50  token=0x6000024  System.Int64 GetTotalDownloadSize(System.Int64 taskId)
  RVA=0x09DDCA24  token=0x6000025  System.Int32 GetDecompressedProgress(System.Int64 taskId)
  RVA=0x09DDCE00  token=0x6000026  System.Int32 SetLanguageType(System.Int32 type)
  RVA=0x09DDCE4C  token=0x6000027  System.Int32 SetNotificationTitle(System.String title)
  RVA=0x09DDCB2C  token=0x6000028  System.Int64 GetEstimatedDownloadSize(System.String versionId, System.String downloadFiles)
  RVA=0x09DDCB88  token=0x6000029  System.Int64 GetEstimatedUpdateSizeV2(System.String versionId, System.String basePath, System.String rootPath, System.String fileUrl, System.String patchUrl, System.String totalFiles, System.String patchInfo)
END_CLASS

CLASS: Hypergryph.SDK.HGDownloadSDKPluginWindows
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IHGDownloadSDK
FIELDS:
METHODS:
  RVA=0x03826EB0  token=0x600002A  System.Int32 HGDLSDKInit(System.String config)
  RVA=0x09DDBE10  token=0x600002B  System.Int64 HGDLSDKDownloadWithPatch(System.String version_id, System.String download_files, System.String decompress_path, System.Boolean need_decompress, System.Boolean use_patch, System.String root_path)
  RVA=0x09DDBD58  token=0x600002C  System.Int32 HGDLSDKClearAllTasks()
  RVA=0x09DDC2E0  token=0x600002D  System.Int32 HGDLSDKGetSDKState()
  RVA=0x09DDC350  token=0x600002E  System.Int32 HGDLSDKGetTaskState(System.Int64 task_id)
  RVA=0x09DDC404  token=0x600002F  System.Int32 HGDLSDKResume(System.Int64 task_id)
  RVA=0x09DDBD24  token=0x6000030  System.Int32 HGDLSDKCancel(System.Int64 task_id)
  RVA=0x09DDBF28  token=0x6000031  System.Int32 HGDLSDKFinish(System.Int64 task_id)
  RVA=0x09DDC314  token=0x6000032  System.IntPtr HGDLSDKGetTaskInfo(System.Int64 task_id)
  RVA=0x09DDC04C  token=0x6000033  System.Int64 HGDLSDKGetDownloadSpeed(System.Int64 task_id)
  RVA=0x09DDC088  token=0x6000034  System.Int64 HGDLSDKGetDownloadedSize(System.Int64 task_id)
  RVA=0x09DDC3C8  token=0x6000035  System.Int64 HGDLSDKGetTotalDownloadSize(System.Int64 task_id)
  RVA=0x09DDBFD4  token=0x6000036  System.Int32 HGDLSDKGetDecompressedProgress(System.Int64 task_id)
  RVA=0x09DDC0C4  token=0x6000037  System.Int64 HGDLSDKGetEstimatedDownloadSize(System.String version_id, System.String download_files)
  RVA=0x09DDBF5C  token=0x6000038  System.Void HGDLSDKFree(System.IntPtr pointer)
  RVA=0x09DDBD8C  token=0x6000039  System.Int32 HGDLSDKClearUselessFiles(System.String root_path, System.String all_files)
  RVA=0x09DDC180  token=0x600003A  System.Int64 HGDLSDKGetEstimatedUpdateSizeV2(System.String version_id, System.String base_paths, System.String root_path, System.String file_url_prefix, System.String patch_url_prefixm, System.String total_files, System.String patch_info)
  RVA=0x09DDC438  token=0x600003B  System.Int64 HGDLSDKUpdateV3(System.String version_id, System.String base_paths, System.String root_path, System.String file_url_prefix, System.String patch_url_prefix, System.String total_files, System.String patch_info, System.Int32 check_type)
  RVA=0x041E1670  token=0x600003C  System.Void .ctor()
  RVA=0x05397CA8  token=0x600003D  System.Int32 init(System.String config)
  RVA=0x09DDC5CC  token=0x600003E  System.Int64 download(System.String versionId, System.String downloadFiles, System.String decompressPath, System.Boolean useMobileData, System.Boolean needCompress, System.Boolean usePatch, System.String rootPath)
  RVA=0x011EC580  token=0x600003F  System.Int32 enableMobileData(System.Int64 taskId)
  RVA=0x09DDC748  token=0x6000040  System.Int32 resume(System.Int64 taskId)
  RVA=0x09DDC5A4  token=0x6000041  System.Int32 cancel(System.Int64 taskId)
  RVA=0x09DDC5B0  token=0x6000042  System.Int32 clearAllTasks()
  RVA=0x09DDC614  token=0x6000043  System.Int32 finish(System.Int64 taskId)
  RVA=0x09DDC6A8  token=0x6000044  System.Int32 getSDKState()
  RVA=0x09DDC730  token=0x6000045  System.Int32 getTaskState(System.Int64 taskId)
  RVA=0x09DDC6B0  token=0x6000046  System.String getTaskInfo(System.Int64 taskId)
  RVA=0x09DDC62C  token=0x6000047  System.Int64 getDownloadSpeed(System.Int64 taskId)
  RVA=0x09DDC638  token=0x6000048  System.Int64 getDownloadedSize(System.Int64 taskId)
  RVA=0x09DDC73C  token=0x6000049  System.Int64 getTotalDownloadSize(System.Int64 taskId)
  RVA=0x09DDC620  token=0x600004A  System.Int32 getDecompressedProgress(System.Int64 taskId)
  RVA=0x011EC580  token=0x600004B  System.Int32 setLanguageType(System.Int32 type)
  RVA=0x011EC580  token=0x600004C  System.Int32 setNotificationTitle(System.String title)
  RVA=0x09DDC644  token=0x600004D  System.Int64 getEstimatedDownloadSize(System.String versionId, System.String downloadFiles)
  RVA=0x09DDC5B8  token=0x600004E  System.Int32 clearUselessFiles(System.String rootPath, System.String allFiles)
  RVA=0x09DDC658  token=0x600004F  System.Int64 getEstimatedUpdateSizeV2(System.String versionId, System.String basePath, System.String rootPath, System.String fileUrl, System.String patchUrl, System.String totalFiles, System.String patchInfo)
  RVA=0x09DDC754  token=0x6000050  System.Int64 updateV3(System.String versionId, System.String basePath, System.String rootPath, System.String fileUrl, System.String patchUrl, System.String totalFiles, System.String patchInfo, System.Boolean useMobileData, System.Int32 checkType)
END_CLASS

