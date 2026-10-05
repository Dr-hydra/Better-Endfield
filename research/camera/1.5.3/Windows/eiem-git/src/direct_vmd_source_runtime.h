#pragma once
#include <filesystem>

enum class DirectVmdSourceCommand { None, Apply, LoadDraft, SaveDraft, SetPmxDraft };
struct DirectVmdSourceUiState {
  uint64_t response = 0, draftRevision = 0, activeRevision = 1;
  bool busy = false;
  std::string draft = eiem_source::Serialize(eiem_source::Config());
  std::string activeName = "Tda Miku (default)",
              status = "Default source ready.";
  std::string activePmxPath;
};
struct DirectVmdSourceRequest {
  DirectVmdSourceCommand kind = DirectVmdSourceCommand::None;
  std::string text, path;
  uint64_t serial = 0;
};
static DirectVmdSourceUiState s_directVmdSourceUi;
static DirectVmdSourceRequest s_directVmdSourceRequest;
static bool DirectVmdSource_CanApplyLocked() {
  return !s_directVmdSourceLease && g_motionBackend.Is(MotionBackend::Native) &&
         !s_directVmdRuntimeActive.load(std::memory_order_acquire) &&
         DirectVmdRuntime_PublicPlayback() == DirectVmdPlaybackState::Stopped &&
         s_directVmdPlaybackRequest.load(std::memory_order_acquire) ==
             uint32_t(DirectVmdRuntimeCommand::Stopped);
}
static bool DirectVmdSource_CanApply() {
  AcquireSRWLockShared(&s_directVmdSourceLock);
  const bool ok = DirectVmdSource_CanApplyLocked();
  ReleaseSRWLockShared(&s_directVmdSourceLock);
  return ok;
}
static DirectVmdSourceUiState DirectVmdSource_UiSnapshot() {
  AcquireSRWLockShared(&s_directVmdSourceLock);
  auto state = s_directVmdSourceUi;
  ReleaseSRWLockShared(&s_directVmdSourceLock);
  return state;
}
static void DirectVmdSource_Request(DirectVmdSourceCommand kind,
                                    const char *text, const char *path) {
  AcquireSRWLockExclusive(&s_directVmdSourceLock);
  if (!s_directVmdSourceUi.busy) {
    s_directVmdSourceRequest.kind = kind;
    s_directVmdSourceRequest.text = text ? text : "";
    s_directVmdSourceRequest.path = path ? path : "";
    ++s_directVmdSourceRequest.serial;
    s_directVmdSourceUi.busy = true;
    s_directVmdSourceUi.status = "Preparing on worker...";
  }
  ReleaseSRWLockExclusive(&s_directVmdSourceLock);
}
static void DirectVmdSource_Finish(const DirectVmdSourceRequest &request,
                                   const std::string &status,
                                   const std::string *draft = nullptr) {
  AcquireSRWLockExclusive(&s_directVmdSourceLock);
  s_directVmdSourceUi.status = status;
  s_directVmdSourceUi.response = request.serial;
  s_directVmdSourceUi.busy = false;
  if (draft) {
    s_directVmdSourceUi.draft = *draft;
    ++s_directVmdSourceUi.draftRevision;
  }
  ReleaseSRWLockExclusive(&s_directVmdSourceLock);
  Log("[SOURCE-CONFIG] request=%llu %s", (unsigned long long)request.serial,
      status.c_str());
}
static bool DirectVmdSource_BindClipWorker(const VmdFile &clip) {
  try {
    auto binding = eiem_source::BindClip(s_directVmdSourcePrepared, clip);
    s_directVmdSourceClip = std::move(binding);
    s_directVmdSourceClipReady = true;
    Log("[SOURCE-CLIP] revision=%llu %s",
        (unsigned long long)s_directVmdSourcePrepared.revision,
        s_directVmdSourceClip.report.c_str());
    return true;
  } catch (const std::exception &e) {
    s_directVmdSourceClipReady = false;
    AcquireSRWLockExclusive(&s_directVmdSourceLock);
    s_directVmdSourceUi.status =
        std::string("Clip mapping rejected: ") + e.what();
    ReleaseSRWLockExclusive(&s_directVmdSourceLock);
    Log("[SOURCE-CLIP] rejected: %s", e.what());
    return false;
  }
}
static void DirectVmdSource_SaveWorker(const std::string &path,
                                       const std::string &text) {
  using namespace eiem_source;
  Require(!path.empty(), "Choose a preset file path");
  const std::wstring target = Wide(path);
  const std::wstring temporary = target + L".eiem-source-" +
                                 std::to_wstring(GetCurrentProcessId()) + L"-" +
                                 std::to_wstring(GetTickCount64()) + L".tmp";
  HANDLE file = CreateFileW(temporary.c_str(), GENERIC_WRITE, 0, nullptr,
                            CREATE_NEW, FILE_ATTRIBUTE_NORMAL, nullptr);
  Require(file != INVALID_HANDLE_VALUE, "Cannot create preset temporary file");
  DWORD wrote = 0;
  const bool ok =
      WriteFile(file, text.data(), DWORD(text.size()), &wrote, nullptr) &&
      wrote == text.size() && FlushFileBuffers(file);
  CloseHandle(file);
  if (!ok || !MoveFileExW(temporary.c_str(), target.c_str(),
                          MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH)) {
    DeleteFileW(temporary.c_str());
    Require(false, "Preset save failed; old destination retained");
  }
}
static void DirectVmdSource_HandleCommandWorker() {
  DirectVmdSourceRequest request;
  AcquireSRWLockExclusive(&s_directVmdSourceLock);
  request = std::move(s_directVmdSourceRequest);
  s_directVmdSourceRequest.kind = DirectVmdSourceCommand::None;
  s_directVmdSourceRequest.serial = request.serial;
  ReleaseSRWLockExclusive(&s_directVmdSourceLock);
  if (request.kind == DirectVmdSourceCommand::None)
    return;
  using namespace eiem_source;
  try {
    if (request.kind == DirectVmdSourceCommand::LoadDraft) {
      auto bytes = ReadBytes(request.path, 65536);
      auto config = ParseConfig(std::string(bytes.begin(), bytes.end()));
      if (!config.pmxPath.empty()) {
        auto pmx = std::filesystem::path(Wide(config.pmxPath));
        if (pmx.is_relative())
          pmx = std::filesystem::absolute(
                    std::filesystem::path(Wide(request.path)))
                    .parent_path() /
                pmx;
        config.pmxPath = pmx.lexically_normal().u8string();
      }
      const auto draft = Serialize(config);
      DirectVmdSource_Finish(
          request, "Preset loaded into draft; Apply while stopped to activate.",
          &draft);
      return;
    }
    auto config = ParseConfig(request.text);
    if (request.kind == DirectVmdSourceCommand::SetPmxDraft) {
      Require(!request.path.empty(), "Choose a PMX file");
      const auto path = std::filesystem::path(Wide(request.path)).lexically_normal();
      config.pmxPath = path.u8string();
      if (config.name == Config().name || config.name == "User PMX skeleton")
        config.name = "PMX: " + path.stem().u8string();
      const auto draft = Serialize(config);
      Require(draft.size() <= 65536, "Updated preset exceeds 64 KiB");
      DirectVmdSource_Finish(request,
          "PMX written into draft: " + config.pmxPath +
          "; click Apply while stopped to activate.", &draft);
      return;
    }
    if (request.kind == DirectVmdSourceCommand::SaveDraft) {
      DirectVmdSource_SaveWorker(request.path, Serialize(config));
      DirectVmdSource_Finish(
          request, "Preset draft saved; active configuration unchanged.");
      return;
    }
    Require(DirectVmdSource_CanApply(),
            "Stop playback and wait for ownership restoration before Apply "
            "(pause is not stop)");
    Require(s_directVmdWorker.handledClipRevision ==
                s_directVmdClipRequestRevision.load(std::memory_order_acquire),
            "A clip load is pending; retry Apply after it finishes");
    const uint64_t backend = g_motionBackend.Generation();
    const uint64_t target =
        s_directVmdTargetGeneration.load(std::memory_order_acquire);
    const uintptr_t owner =
        s_directVmdTargetOwner.load(std::memory_order_acquire);
    const uint64_t clipRequest =
        s_directVmdClipRequestRevision.load(std::memory_order_acquire);
    const uint64_t playbackRequest =
        s_directVmdPlaybackRequestRevision.load(std::memory_order_acquire);
    auto prepared =
        Prepare(config);
    ClipBinding binding;
    const VmdFile *clip = s_directVmdWorker.resource.Get();
    if (clip && clip->loaded) {
      binding = BindClip(prepared, *clip);
      DirectVmdSampleFrame probe;
      Sample(prepared, binding, *clip, 0.0, &probe);
      Sample(prepared, binding, *clip, double(clip->totalFrames), &probe);
    }
    auto name = config.name;
    auto pmxPath = config.pmxPath;
    const auto message =
        std::string("Applied. source=") + (prepared.pmx ? "PMX" : "canonical") +
        " name=\"" + name + "\" pmx=\"" + pmxPath + "\". " +
        prepared.report + " " + binding.report;
    AcquireSRWLockShared(&s_directVmdLifecycleLock);
    AcquireSRWLockExclusive(&s_directVmdSourceLock);
    const bool valid =
        DirectVmdSource_CanApplyLocked() &&
        backend == g_motionBackend.Generation() &&
        target == s_directVmdTargetGeneration.load(std::memory_order_acquire) &&
        owner == s_directVmdTargetOwner.load(std::memory_order_acquire) &&
        clipRequest ==
            s_directVmdClipRequestRevision.load(std::memory_order_acquire) &&
        playbackRequest ==
            s_directVmdPlaybackRequestRevision.load(std::memory_order_acquire);
    if (valid) {
      prepared.revision =
          s_directVmdSourceRevision.load(std::memory_order_relaxed) + 1;
      s_directVmdSourceReference = prepared.reference;
      std::swap(s_directVmdSourcePrepared, prepared);
      std::swap(s_directVmdSourceClip, binding);
      s_directVmdSourceClipReady = clip && clip->loaded;
      s_directVmdSourceUi.activeName.swap(name);
      s_directVmdSourceUi.activePmxPath.swap(pmxPath);
      s_directVmdSourceUi.activeRevision = s_directVmdSourcePrepared.revision;
      s_directVmdSourceRevision.store(s_directVmdSourcePrepared.revision,
                                      std::memory_order_release);
      if (s_directVmdSourceClipReady) {
        s_directVmdWorker.clock.Reset(double(clip->totalFrames));
        s_directVmdLoadedPublic.store(true, std::memory_order_release);
        s_directVmdDurationPublic.store(double(clip->totalFrames),
                                        std::memory_order_release);
      }
    }
    ReleaseSRWLockExclusive(&s_directVmdSourceLock);
    ReleaseSRWLockShared(&s_directVmdLifecycleLock);
    Require(valid, "Playback/owner/backend/clip changed during preparation; "
                   "previous source retained");
    DirectVmdRuntime_ResetQpcWorker();
    DirectVmdRuntime_PublishInvalidWorkerFrame();
    DirectVmdSource_Finish(request, message);
  } catch (const std::exception &e) {
    DirectVmdSource_Finish(request,
                           std::string("Rejected; previous source retained: ") +
                               e.what());
  }
}
