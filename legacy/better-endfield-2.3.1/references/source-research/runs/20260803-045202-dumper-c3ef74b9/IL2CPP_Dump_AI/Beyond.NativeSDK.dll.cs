// ========================================================
// Dumped by @desirepro
// Assembly: Beyond.NativeSDK.dll
// Classes:  26
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

CLASS: PROCESS_MEMORY_COUNTERS
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x58
FIELDS:
  public            System.UInt32                   cb  // 0x10
  public            System.UInt32                   PageFaultCount  // 0x14
  public            System.UInt64                   PeakWorkingSetSize  // 0x18
  public            System.UInt64                   WorkingSetSize  // 0x20
  public            System.UInt64                   QuotaPeakPagedPoolUsage  // 0x28
  public            System.UInt64                   QuotaPagedPoolUsage  // 0x30
  public            System.UInt64                   QuotaPeakNonPagedPoolUsage  // 0x38
  public            System.UInt64                   QuotaNonPagedPoolUsage  // 0x40
  public            System.UInt64                   PagefileUsage  // 0x48
  public            System.UInt64                   PeakPagefileUsage  // 0x50
METHODS:
END_CLASS

CLASS: PDH_FMT_COUNTERVALUE
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x20
FIELDS:
  public            System.Int32                    CStatus  // 0x10
  public            System.Int64                    largeValue  // 0x18
METHODS:
END_CLASS

CLASS: STORAGE_PROPERTY_QUERY
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x1C
FIELDS:
  public            System.UInt32                   PropertyId  // 0x10
  public            System.UInt32                   QueryType  // 0x14
  public            System.Byte                     AdditionalParameters  // 0x18
METHODS:
END_CLASS

CLASS: DEVICE_SEEK_PENALTY_DESCRIPTOR
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x1C
FIELDS:
  public            System.UInt32                   Version  // 0x10
  public            System.UInt32                   Size  // 0x14
  public            System.Boolean                  IncursSeekPenalty  // 0x18
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.NativeSDK.NativeWindowsDiskChecker.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,System.Boolean><>9__24_0  // static @ 0x8
  public    static  System.Func<System.String,System.Boolean><>9__28_0  // static @ 0x10
METHODS:
  RVA=0x04D44E30  token=0x6000038  System.Void .cctor()
  RVA=0x041E1670  token=0x6000039  System.Void .ctor()
  RVA=0x04B313B0  token=0x600003A  System.Boolean <StandaloneWinHaveSSd>b__24_0(System.String s)
  RVA=0x04B313B0  token=0x600003B  System.Boolean <GetAllDiskSpaces>b__28_0(System.String s)
END_CLASS

CLASS: SYSTEM_POWER_STATUS
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x1C
FIELDS:
  public            System.Byte                     ACLineStatus  // 0x10
  public            System.Byte                     BatteryFlag  // 0x11
  public            System.Byte                     BatteryLifePercent  // 0x12
  public            System.Byte                     Reserved1  // 0x13
  public            System.UInt32                   BatteryLifeTime  // 0x14
  public            System.UInt32                   BatteryFullLifeTime  // 0x18
METHODS:
END_CLASS

CLASS: PROCESS_MEMORY_COUNTERS
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x58
FIELDS:
  public            System.UInt32                   cb  // 0x10
  public            System.UInt32                   PageFaultCount  // 0x14
  public            System.UInt64                   PeakWorkingSetSize  // 0x18
  public            System.UInt64                   WorkingSetSize  // 0x20
  public            System.UInt64                   QuotaPeakPagedPoolUsage  // 0x28
  public            System.UInt64                   QuotaPagedPoolUsage  // 0x30
  public            System.UInt64                   QuotaPeakNonPagedPoolUsage  // 0x38
  public            System.UInt64                   QuotaNonPagedPoolUsage  // 0x40
  public            System.UInt64                   PagefileUsage  // 0x48
  public            System.UInt64                   PeakPagefileUsage  // 0x50
METHODS:
END_CLASS

CLASS: PDH_FMT_COUNTERVALUE
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x20
FIELDS:
  public            System.Int32                    CStatus  // 0x10
  public            System.Int64                    largeValue  // 0x18
METHODS:
END_CLASS

CLASS: PDH_FMT_COUNTERVALUE_ITEM
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x28
FIELDS:
  public            System.IntPtr                   szName  // 0x10
  public            Beyond.NativeSDK.VideoMemoryStatus.PDH_FMT_COUNTERVALUEFmtValue  // 0x18
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.NativeSDK.VideoMemoryStatus.<>c<>9  // static @ 0x0
  public    static  System.Comparison<System.Collections.Generic.KeyValuePair<System.Int32,System.Int64>><>9__21_0  // static @ 0x8
METHODS:
  RVA=0x011DBB20  token=0x6000055  System.Void .cctor()
  RVA=0x041E1670  token=0x6000056  System.Void .ctor()
  RVA=0x011DB800  token=0x6000057  System.Int32 <GetTopOtherProcessMemory>b__21_0(System.Collections.Generic.KeyValuePair<System.Int32,System.Int64> a, System.Collections.Generic.KeyValuePair<System.Int32,System.Int64> b)
END_CLASS

CLASS: PDH_FMT_COUNTERVALUE_DOUBLE
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x20
FIELDS:
  public            System.Int32                    CStatus  // 0x10
  public            System.Double                   doubleValue  // 0x18
METHODS:
END_CLASS

CLASS: PDH_FMT_COUNTERVALUE_ITEM_DOUBLE
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x28
FIELDS:
  public            System.IntPtr                   szName  // 0x10
  public            Beyond.NativeSDK.WinPerfMonitor.PDH_FMT_COUNTERVALUE_DOUBLEFmtValue  // 0x18
METHODS:
END_CLASS

CLASS: Beyond.NativeSDK.DiskSpaceInfo
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x38
FIELDS:
  public            System.String                   DriveName  // 0x10
  public            System.UInt64                   TotalBytes  // 0x18
  public            System.UInt64                   FreeBytes  // 0x20
  public            System.UInt64                   FreeBytesAvailable  // 0x28
  public            System.Boolean                  IsSSD  // 0x30
PROPERTIES:
  TotalGB  get=0x039BADF0
  FreeGB  get=0x039BAE40
METHODS:
END_CLASS

CLASS: Beyond.NativeSDK.MemoryDimmInfo
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x40
FIELDS:
  public            System.String                   DeviceLocator  // 0x10
  public            System.String                   MemoryType  // 0x18
  public            System.UInt32                   SpeedMTs  // 0x20
  public            System.UInt32                   ConfiguredSpeedMTs  // 0x24
  public            System.UInt64                   SizeBytes  // 0x28
  public            System.String                   Manufacturer  // 0x30
  public            System.String                   PartNumber  // 0x38
PROPERTIES:
  SizeGB  get=0x0638029C
METHODS:
END_CLASS

CLASS: Beyond.NativeSDK.PcPerfInfo
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x1C
FIELDS:
  public            System.Single                   CpuUsagePercent  // 0x10
  public            System.Single                   GpuUsagePercent  // 0x14
  public            System.Int32                    CpuClockMHz  // 0x18
METHODS:
END_CLASS

CLASS: Beyond.NativeSDK.SystemMemoryInfo
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x38
FIELDS:
  public            System.Int32                    DimmCount  // 0x10
  public            System.Int32                    TotalSlots  // 0x14
  public            System.String                   MemoryType  // 0x18
  public            System.UInt32                   MaxSpeedMTs  // 0x20
  public            System.UInt32                   ConfiguredSpeedMTs  // 0x24
  public            System.UInt64                   TotalSizeBytes  // 0x28
  public            Beyond.NativeSDK.MemoryDimmInfo[]Dimms  // 0x30
PROPERTIES:
  TotalSizeGB  get=0x04D56290
METHODS:
END_CLASS

CLASS: Beyond.NativeSDK.NativeUtil
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    s_procId  // static @ 0x0
  public    static  System.ValueTuple<System.Int64,System.Int64>s_DedicatedGpuMem  // static @ 0x8
  public    static  System.ValueTuple<System.Int64,System.Int64>s_SharedMemoryGpuMem  // static @ 0x18
  public    static  System.ValueTuple<System.Int64,System.Int64>s_CommittedMemoryGpuMem  // static @ 0x28
  public    static  System.String                   s_gpuMemQuery  // static @ 0x38
  private   static  System.Text.StringBuilder       s_GetGpuMemoryTopOthersSB  // static @ 0x40
  public    static  System.Int32                    s_frequencyCalcTimes  // static @ 0x48
  private   static  System.Int64                    _calcResult  // static @ 0x50
  private   static readonly System.Diagnostics.Stopwatch    SW  // static @ 0x58
METHODS:
  RVA=0x04D83BA0  token=0x6000005  System.Void _Clear()
  RVA=0x063805A0  token=0x6000006  System.Boolean IsPromotionSupport()
  RVA=0x06380314  token=0x6000007  System.Int64 GetAvailableMemory()
  RVA=0x04D80940  token=0x6000008  System.Int64 ShrinkApplicationMemory()
  RVA=0x04D7AF70  token=0x6000009  System.Int64 GetOOMMemory()
  RVA=0x039B8610  token=0x600000A  System.Boolean GetProcessMemoryInfo(System.IntPtr hProcess, Beyond.NativeSDK.NativeUtil.PROCESS_MEMORY_COUNTERS& counters, System.UInt32 size)
  RVA=0x063807E0  token=0x600000B  System.Int32 PdhOpenQueryA(System.IntPtr szDataSource, System.IntPtr dwUserData, System.IntPtr& phQuery)
  RVA=0x063805DC  token=0x600000C  System.Int32 PdhAddEnglishCounterA(System.IntPtr hQuery, System.String szFullCounterPath, System.UIntPtr dwUserData, System.IntPtr& phCounter)
  RVA=0x063806CC  token=0x600000D  System.Int32 PdhCollectQueryData(System.IntPtr hQuery)
  RVA=0x06380740  token=0x600000E  System.Int32 PdhGetFormattedCounterValue(System.IntPtr hQuery, System.Int32 dwFormat, System.Int32* lpdwType, Beyond.NativeSDK.NativeUtil.PDH_FMT_COUNTERVALUE& pValue)
  RVA=0x06380694  token=0x600000F  System.Int32 PdhCloseQuery(System.IntPtr hQuery)
  RVA=0x063803D0  token=0x6000010  System.Int32 GetProcId()
  RVA=0x063808B0  token=0x6000011  System.Int64 getRSSMemoryByte()
  RVA=0x039B9400  token=0x6000012  System.Int64 GetTotalMemory()
  RVA=0x06380524  token=0x6000013  System.Int64 GetVirtualMemory()
  RVA=0x039B9A80  token=0x6000014  System.Int64 GetGpuMemory()
  RVA=0x011D9F20  token=0x6000015  System.String GetGpuMemoryTopOthersJson()
  RVA=0x011D7C60  token=0x6000016  System.Void AppendGpuTopArray(System.Text.StringBuilder sb, System.String key, System.Collections.Generic.List<Beyond.NativeSDK.GpuProcMemEntry> list)
  RVA=0x011D9080  token=0x6000017  System.String EscapeJson(System.String s)
  RVA=0x039BA960  token=0x6000018  System.Single GetCpuTemperature()
  RVA=0x039BA980  token=0x6000019  System.Single GetGpuTemperature()
  RVA=0x06380394  token=0x600001A  System.Int64 GetCpuFrequency()
  RVA=0x06380354  token=0x600001B  System.Single GetBatteryLevel()
  RVA=0x04D1F4C0  token=0x600001C  System.Boolean IsLapTop()
  RVA=0x039B9A30  token=0x600001D  System.Boolean IsCharge()
  RVA=0x039B9A60  token=0x600001E  System.Int32 GetThermalState()
  RVA=0x06380560  token=0x600001F  System.Boolean IsInSsd()
  RVA=0x03DA15C0  token=0x6000020  Beyond.NativeSDK.DiskSpaceInfo GetRunningDiskInfo()
  RVA=0x04D7CF50  token=0x6000021  Beyond.NativeSDK.DiskSpaceInfo[] GetAllDiskInfo()
  RVA=0x039B73C0  token=0x6000022  System.Boolean HasSsd()
  RVA=0x011DB180  token=0x6000023  System.Boolean TrySamplePcPerf(Beyond.NativeSDK.PcPerfInfo& info)
  RVA=0x03824CC0  token=0x6000024  Beyond.NativeSDK.SystemMemoryInfo GetSystemMemoryInfo()
  RVA=0x039BAB20  token=0x6000025  System.Double GetFrequencyCalculateTime()
  RVA=0x06380874  token=0x6000026  System.Int64 RunCpuFrequencyCalculate(System.Int32 times)
  RVA=0x04B682F0  token=0x6000027  System.Void .cctor()
END_CLASS

CLASS: Beyond.NativeSDK.NativeWindowsDiskChecker
TYPE:  static class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  private   static  System.UInt32                   STORAGEDEVICESEEKPENALTYPROPERTY  // const
  private   static  System.UInt32                   PROPERTYSTANDARDQUERY  // const
  private   static  System.Int32                    MAX_PATH  // const
  private   static  System.UInt32                   FILE_SHARE_READ  // const
  private   static  System.UInt32                   FILE_SHARE_WRITE  // const
  private   static  System.UInt32                   FILE_SHARE_DELETE  // const
  private   static  System.UInt32                   OPEN_EXISTING  // const
  private   static  System.UInt32                   FILE_FLAG_BACKUP_SEMANTICS  // const
  private   static  System.UInt32                   IOCTL_STORAGE_QUERY_PROPERTY  // const
  private   static  System.Nullable<System.Boolean> initOnceIsRunningInSSD  // static @ 0x0
  private   static  System.Nullable<Beyond.NativeSDK.DiskSpaceInfo>initOnceCurrentDiskInfo  // static @ 0x8
  private   static  System.Object                   initOnceStandaloneWinHaveSSd  // static @ 0x38
METHODS:
  RVA=0x039BACF0  token=0x6000028  System.Boolean GetVolumePathName(System.String lpszFileName, System.IntPtr lpszVolumePathName, System.UInt32 cchBufferLength)
  RVA=0x039BAD70  token=0x6000029  System.Boolean GetVolumeNameForVolumeMountPoint(System.String lpszVolumeMountPoint, System.IntPtr lpszVolumeName, System.UInt32 cchBufferLength)
  RVA=0x039BA360  token=0x600002A  System.IntPtr CreateFile(System.String lpFileName, System.UInt32 dwDesiredAccess, System.UInt32 dwShareMode, System.IntPtr lpSecurityAttributes, System.UInt32 dwCreationDisposition, System.UInt32 dwFlagsAndAttributes, System.IntPtr hTemplateFile)
  RVA=0x039BA200  token=0x600002B  System.Boolean CloseHandle(System.IntPtr hObject)
  RVA=0x03824E60  token=0x600002C  System.Int32 GetLogicalDriveStrings(System.UInt32 nBufferLength, System.Char[] lpBuffer)
  RVA=0x039BA2B0  token=0x600002D  System.Boolean DeviceIoControl(System.IntPtr hDevice, System.UInt32 dwIoControlCode, Beyond.NativeSDK.NativeWindowsDiskChecker.STORAGE_PROPERTY_QUERY& lpInBuffer, System.UInt32 nInBufferSize, Beyond.NativeSDK.NativeWindowsDiskChecker.DEVICE_SEEK_PENALTY_DESCRIPTOR& lpOutBuffer, System.UInt32 nOutBufferSize, System.UInt32& lpBytesReturned, System.IntPtr lpOverlapped)
  RVA=0x039BA5D0  token=0x600002E  System.IntPtr GetVolumeHandleForFile(System.String filePath)
  RVA=0x039BA400  token=0x600002F  System.Boolean IsFileOnSsd(System.String filePath)
  RVA=0x06380C54  token=0x6000030  System.Boolean IsRunningInSSD()
  RVA=0x03DA34B0  token=0x6000031  Beyond.NativeSDK.DiskSpaceInfo GetRunningDiskInfo()
  RVA=0x039B7F30  token=0x6000032  System.Boolean StandaloneWinHaveSSd()
  RVA=0x039BA170  token=0x6000033  System.Boolean GetDiskFreeSpaceEx(System.String lpDirectoryName, System.UInt64& lpFreeBytesAvailableToCaller, System.UInt64& lpTotalNumberOfBytes, System.UInt64& lpTotalNumberOfFreeBytes)
  RVA=0x0494FFB0  token=0x6000034  System.Boolean GetDiskSpace(System.String path, Beyond.NativeSDK.DiskSpaceInfo& info)
  RVA=0x06380BF8  token=0x6000035  System.Boolean GetCurrentDiskSpace(Beyond.NativeSDK.DiskSpaceInfo& info)
  RVA=0x039B9D40  token=0x6000036  Beyond.NativeSDK.DiskSpaceInfo[] GetAllDiskSpaces()
  RVA=0x06380D38  token=0x6000037  System.Void Test()
END_CLASS

CLASS: Beyond.NativeSDK.NativePower
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03826730  token=0x600003C  System.Boolean GetSystemPowerStatus(Beyond.NativeSDK.NativePower.SYSTEM_POWER_STATUS& sps)
  RVA=0x04D1F4F0  token=0x600003D  System.Boolean laptop()
END_CLASS

CLASS: Beyond.NativeSDK.GpuProcMemEntry
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x28
FIELDS:
  public            System.Int32                    Pid  // 0x10
  public            System.String                   ProcessName  // 0x18
  public            System.Int64                    Bytes  // 0x20
PROPERTIES:
  MB  get=0x011CDA80
METHODS:
END_CLASS

CLASS: Beyond.NativeSDK.VideoMemoryStatus
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    PDH_FMT_LARGE  // static @ 0x0
  private   static  System.Int64                    PDH_MORE_DATA  // static @ 0x8
  private   static  System.String                   Command  // const
  private   static  System.Int32                    s_currentPid  // static @ 0x10
  private   static  System.UInt32                   PROCESS_QUERY_LIMITED_INFORMATION  // const
  private   static readonly System.Char[]                   s_pathSep  // static @ 0x18
METHODS:
  RVA=0x06380D78  token=0x600003F  System.Boolean GetProcessMemoryInfo(System.IntPtr hProcess, Beyond.NativeSDK.VideoMemoryStatus.PROCESS_MEMORY_COUNTERS& counters, System.UInt32 size)
  RVA=0x0449CC20  token=0x6000040  System.Int64 PdhOpenQuery(System.IntPtr szDataSource, System.IntPtr dwUserData, System.IntPtr& phQuery)
  RVA=0x06380E14  token=0x6000041  System.Int64 PdhAddCounter(System.IntPtr hQuery, System.String szFullCounterPath, System.UIntPtr dwUserData, System.IntPtr& phCounter)
  RVA=0x0449CC90  token=0x6000042  System.Int64 PdhAddEnglishCounter(System.IntPtr hQuery, System.String szFullCounterPath, System.UIntPtr dwUserData, System.IntPtr& phCounter)
  RVA=0x0449CD10  token=0x6000043  System.Int64 PdhCollectQueryData(System.IntPtr hQuery)
  RVA=0x0449CD70  token=0x6000044  System.Int64 PdhGetFormattedCounterArray(System.IntPtr hQuery, System.Int32 dwFormat, System.UInt32& dwBufferSize, System.UInt32& dwItemCount, System.IntPtr itemBuffer)
  RVA=0x0449CDF0  token=0x6000045  System.Int32 PdhCloseQuery(System.IntPtr hQuery)
  RVA=0x039B9CB0  token=0x6000046  System.ValueTuple<System.Int64,System.Int64> GetDedicatedMemoryStatus()
  RVA=0x039B9C20  token=0x6000047  System.ValueTuple<System.Int64,System.Int64> GetSharedMemoryStatus()
  RVA=0x039B9B90  token=0x6000048  System.ValueTuple<System.Int64,System.Int64> GetCommittedMemoryStatus()
  RVA=0x035CADF0  token=0x6000049  System.ValueTuple<System.Int64,System.Int64> GetMemoryStatus(System.String type)
  RVA=0x011DBF60  token=0x600004A  System.Collections.Generic.Dictionary<System.Int32,System.Int64> CollectByPid(System.String type)
  RVA=0x011DF340  token=0x600004B  System.Collections.Generic.List<Beyond.NativeSDK.GpuProcMemEntry> GetDedicatedTopOthers(System.Int32 topN)
  RVA=0x011DFC60  token=0x600004C  System.Collections.Generic.List<Beyond.NativeSDK.GpuProcMemEntry> GetSharedTopOthers(System.Int32 topN)
  RVA=0x011DDEF0  token=0x600004D  System.Collections.Generic.List<Beyond.NativeSDK.GpuProcMemEntry> GetCommittedTopOthers(System.Int32 topN)
  RVA=0x011E0580  token=0x600004E  System.Collections.Generic.List<Beyond.NativeSDK.GpuProcMemEntry> GetTopOtherProcessMemory(System.String type, System.Int32 topN)
  RVA=0x011DE810  token=0x600004F  System.Int32 GetCurrentProcessIdCached()
  RVA=0x011E3410  token=0x6000050  System.IntPtr OpenProcess(System.UInt32 dwDesiredAccess, System.Boolean bInheritHandle, System.UInt32 dwProcessId)
  RVA=0x011E34F0  token=0x6000051  System.Boolean QueryFullProcessImageNameW(System.IntPtr hProcess, System.Int32 dwFlags, System.IntPtr lpExeName, System.Int32& lpdwSize)
  RVA=0x011DBE80  token=0x6000052  System.Boolean CloseHandle(System.IntPtr hObject)
  RVA=0x011E35E0  token=0x6000053  System.String ResolveProcessName(System.Int32 pid)
  RVA=0x04D57860  token=0x6000054  System.Void .cctor()
END_CLASS

CLASS: Beyond.NativeSDK.WinMemoryInfo
TYPE:  static class
TOKEN: 0x2000015
SIZE:  0x10
FIELDS:
  private   static  System.UInt32                   RSMB  // const
  private   static  System.Byte                     SMBIOS_TYPE_MEMORY_DEVICE  // const
  private   static  System.Byte                     SMBIOS_TYPE_END_OF_TABLE  // const
  private   static  System.Nullable<Beyond.NativeSDK.SystemMemoryInfo>s_cachedInfo  // static @ 0x0
METHODS:
  RVA=0x03826090  token=0x6000058  System.UInt32 GetSystemFirmwareTable(System.UInt32 FirmwareTableProviderSignature, System.UInt32 FirmwareTableID, System.IntPtr pFirmwareTableBuffer, System.UInt32 BufferSize)
  RVA=0x03824D10  token=0x6000059  Beyond.NativeSDK.SystemMemoryInfo GetSystemMemoryInfo()
  RVA=0x03824EC0  token=0x600005A  Beyond.NativeSDK.SystemMemoryInfo ParseSmbiosMemoryInfo()
  RVA=0x04D864A0  token=0x600005B  System.UInt16 ReadUInt16(System.Byte* ptr, System.Int32 offset)
  RVA=0x04D864C0  token=0x600005C  System.UInt32 ReadUInt32(System.Byte* ptr, System.Int32 offset)
  RVA=0x03825E90  token=0x600005D  System.String GetSmbiosString(System.Byte* stringArea, System.Byte index, System.Byte* tableEnd)
  RVA=0x03825F70  token=0x600005E  System.String GetMemoryTypeName(System.Byte code)
  RVA=0x06380EC8  token=0x600005F  System.Void Test()
END_CLASS

CLASS: Beyond.NativeSDK.WinPerfMonitor
TYPE:  static class
TOKEN: 0x2000016
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    PDH_FMT_DOUBLE  // const
  private   static  System.Int64                    PDH_MORE_DATA  // const
  private   static  System.Int32                    PDH_CSTATUS_NEW_DATA  // const
  private   static  System.String                   COUNTER_CPU_UTILITY  // const
  private   static  System.String                   COUNTER_CPU_TIME  // const
  private   static  System.String                   COUNTER_CPU_PERFORMANCE  // const
  private   static  System.String                   COUNTER_CPU_FREQUENCY  // const
  private   static  System.IntPtr                   s_query  // static @ 0x0
  private   static  System.IntPtr                   s_cpuUsageCounter  // static @ 0x8
  private   static  System.IntPtr                   s_cpuPerformanceCounter  // static @ 0x10
  private   static  System.IntPtr                   s_cpuFrequencyCounter  // static @ 0x18
  private   static  System.IntPtr                   s_gpuUsageCounter  // static @ 0x20
  private   static  System.Boolean                  s_inited  // static @ 0x28
  private   static  System.Boolean                  s_initFailed  // static @ 0x29
  private   static  System.Boolean                  s_firstCollectDone  // static @ 0x2a
METHODS:
  RVA=0x011E4EF0  token=0x6000060  System.Int64 PdhOpenQuery(System.IntPtr szDataSource, System.IntPtr dwUserData, System.IntPtr& phQuery)
  RVA=0x011E4B90  token=0x6000061  System.Int64 PdhAddEnglishCounter(System.IntPtr hQuery, System.String szFullCounterPath, System.UIntPtr dwUserData, System.IntPtr& phCounter)
  RVA=0x011E4CD0  token=0x6000062  System.Int64 PdhCollectQueryData(System.IntPtr hQuery)
  RVA=0x011E4E10  token=0x6000063  System.Int64 PdhGetFormattedCounterValue(System.IntPtr hCounter, System.Int32 dwFormat, System.IntPtr lpdwType, Beyond.NativeSDK.WinPerfMonitor.PDH_FMT_COUNTERVALUE_DOUBLE& pValue)
  RVA=0x011E4D60  token=0x6000064  System.Int64 PdhGetFormattedCounterArray(System.IntPtr hCounter, System.Int32 dwFormat, System.UInt32& dwBufferSize, System.UInt32& dwItemCount, System.IntPtr itemBuffer)
  RVA=0x011E4C40  token=0x6000065  System.Int32 PdhCloseQuery(System.IntPtr hQuery)
  RVA=0x011E4FD0  token=0x6000066  System.Boolean TrySample(Beyond.NativeSDK.PcPerfInfo& info)
  RVA=0x011E5C90  token=0x6000067  System.Boolean _EnsureInited()
  RVA=0x011E8DC0  token=0x6000068  System.Void _Shutdown()
  RVA=0x011E7290  token=0x6000069  System.Double _ReadCounterValue(System.IntPtr counter)
  RVA=0x011E7BC0  token=0x600006A  System.Single _ReadGpuUsage()
  RVA=0x011E9520  token=0x600006B  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x0637FE6C  token=0x600006C  System.Double __Gen_Wrap_0(Beyond.NativeSDK.DiskSpaceInfo& P0)
  RVA=0x0637FFD8  token=0x600006D  System.Double __Gen_Wrap_1(Beyond.NativeSDK.MemoryDimmInfo& P0)
  RVA=0x06380130  token=0x600006E  System.Double __Gen_Wrap_2(Beyond.NativeSDK.SystemMemoryInfo& P0)
  RVA=0x063761FC  token=0x600006F  System.Void __Gen_Wrap_3()
  RVA=0x063623A8  token=0x6000070  System.Boolean __Gen_Wrap_4()
  RVA=0x0635FE78  token=0x6000071  System.Int64 __Gen_Wrap_5()
  RVA=0x063621C4  token=0x6000072  System.Int32 __Gen_Wrap_6()
  RVA=0x011D6230  token=0x6000073  System.Collections.Generic.Dictionary<System.Int32,System.Int64> __Gen_Wrap_7(System.Object P0)
  RVA=0x011D6B70  token=0x6000074  System.ValueTuple<System.Int64,System.Int64> __Gen_Wrap_8(System.Object P0)
  RVA=0x011D7450  token=0x6000075  System.ValueTuple<System.Int64,System.Int64> __Gen_Wrap_9()
  RVA=0x011CDE50  token=0x6000076  System.String __Gen_Wrap_11(System.Int32 P0)
  RVA=0x011CE6D0  token=0x6000077  System.Collections.Generic.List<Beyond.NativeSDK.GpuProcMemEntry> __Gen_Wrap_12(System.Object P0, System.Int32 P1)
  RVA=0x011CF070  token=0x6000078  System.Collections.Generic.List<Beyond.NativeSDK.GpuProcMemEntry> __Gen_Wrap_13(System.Int32 P0)
  RVA=0x011CF8F0  token=0x6000079  System.String __Gen_Wrap_14(System.Object P0)
  RVA=0x011D0220  token=0x600007A  System.Double __Gen_Wrap_15(Beyond.NativeSDK.GpuProcMemEntry& P0)
  RVA=0x011D0E30  token=0x600007B  System.Void __Gen_Wrap_16(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x011D1280  token=0x600007C  System.String __Gen_Wrap_17()
  RVA=0x011D1AC0  token=0x600007D  System.Single __Gen_Wrap_18()
  RVA=0x011D1C90  token=0x600007E  System.IntPtr __Gen_Wrap_19(System.Object P0)
  RVA=0x011D1F50  token=0x600007F  System.Boolean __Gen_Wrap_20(System.Object P0)
  RVA=0x011D2210  token=0x6000080  System.Boolean __Gen_Wrap_21(System.Object P0, Beyond.NativeSDK.DiskSpaceInfo& P1)
  RVA=0x011D2BF0  token=0x6000081  Beyond.NativeSDK.DiskSpaceInfo __Gen_Wrap_22()
  RVA=0x011D3410  token=0x6000082  Beyond.NativeSDK.DiskSpaceInfo[] __Gen_Wrap_24()
  RVA=0x011D3C50  token=0x6000083  System.Double __Gen_Wrap_25(System.IntPtr P0)
  RVA=0x011D3E50  token=0x6000084  System.Boolean __Gen_Wrap_26(Beyond.NativeSDK.PcPerfInfo& P0)
  RVA=0x011D46E0  token=0x6000085  System.String __Gen_Wrap_27(System.Byte P0)
  RVA=0x011D4F60  token=0x6000086  Beyond.NativeSDK.SystemMemoryInfo __Gen_Wrap_28()
  RVA=0x011D5780  token=0x6000087  System.Double __Gen_Wrap_29()
  RVA=0x011D5950  token=0x6000088  System.Boolean __Gen_Wrap_30(Beyond.NativeSDK.DiskSpaceInfo& P0)
  RVA=0x04D37950  token=0x6000089  System.Void .cctor()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x06380F08  token=0x600008A  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x039BACA0  token=0x600008B  System.Boolean IsPatched(System.Int32 id)
END_CLASS

