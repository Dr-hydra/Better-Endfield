#pragma once

static void LoadAndResolveVmd(HWND hwnd) {
  char filePath[MAX_PATH] = {0};
  OPENFILENAMEA ofn = {};
  ofn.lStructSize = sizeof(ofn);
  ofn.hwndOwner = hwnd;
  ofn.lpstrFilter = "VMD Files (*.vmd)\0*.vmd\0All Files\0*.*\0";
  ofn.lpstrFile = filePath;
  ofn.nMaxFile = MAX_PATH;
  ofn.Flags = OFN_FILEMUSTEXIST | OFN_NOCHANGEDIR;
  ofn.lpstrTitle = "EIEM - Select VMD File";

  if (!GetOpenFileNameA(&ofn)) {
    Log("[VMD] File dialog cancelled");
    return;
  }

  Log("[VMD] Loading: %s", filePath);

  if (g_vmd) {
    FreeVmd(g_vmd);
    g_vmd = nullptr;
  }
  if (!g_resolvedMappings)
    g_resolvedMappings = new std::vector<ResolvedBoneMapping>();
  g_resolvedMappings->clear();

  g_vmd = LoadVmd(filePath);
  if (!g_vmd->loaded) {
    Log("[VMD] ERROR: %s", g_vmd->error.c_str());
    return;
  }

  Log("[VMD] Loaded OK: model=%s, frames=%u (%.1f sec), %zu bone timelines",
      g_vmd->modelName, g_vmd->totalFrames, g_vmd->totalFrames / 30.0f,
      g_vmd->boneTimelines.size());

  FILE *dumpFile = fopen("plugin/eiem_vmd_dump.txt", "w");
  if (dumpFile) {
    DumpVmd(g_vmd, dumpFile);

    fprintf(dumpFile, "\n=== Bone Mapping Resolution ===\n");
    RefreshEntityAnimator();

    void *charRootTransform = nullptr;
    if (g_cachedAnimator && g_component_get_transform) {
      charRootTransform = SafeGetComponentTransform(g_cachedAnimator);
    }
    if (charRootTransform) {
      Log("[VMD] Character root Transform: %p", charRootTransform);
    } else {
      Log("[VMD] WARNING: No character root Transform, finger bones won't "
          "resolve");
    }
    int mapped = 0, unmapped = 0;
    for (const auto &pair : g_vmd->boneTimelines) {
      const std::string &mmdName = pair.first;
      int humanBone = LookupBoneMapping(mmdName);
      bool isPos = IsBonePositionMapped(mmdName);

      ResolvedBoneMapping rm;
      rm.mmdName = mmdName;
      rm.humanBone = humanBone;
      rm.isPositionBone = isPos;
      rm.transform = nullptr;
      rm.valid = false;

      if (humanBone >= 0 && g_cachedAnimator && g_animator_GetBoneTransform) {
        void *t = SafeGetBoneTransform(humanBone);
        if (t) {
          rm.transform = t;
          rm.valid = true;
        }
      }

      if (!rm.valid) {
        const char *fingerName = LookupFingerMapping(mmdName);
        if (fingerName && charRootTransform) {
          void *t = SafeFindChildRecursive(charRootTransform, fingerName, 15);
          if (t) {
            rm.transform = t;
            rm.transformName = fingerName;
            rm.isFingerBone = true;
            rm.valid = true;
          }
        }
      }

      if (rm.valid) {
        char bname[256] = {0};
        if (g_object_get_name)
          SafeGetBoneName(rm.transform, bname, sizeof(bname));
        fprintf(dumpFile, "  [OK] \"%s\" -> %s[%s] -> \"%s\" (%zu keys)%s\n",
                mmdName.c_str(), rm.isFingerBone ? "Finger" : "HumanBone",
                rm.isFingerBone ? rm.transformName.c_str()
                                : std::to_string(humanBone).c_str(),
                bname, pair.second.keys.size(), isPos ? " [POS]" : "");
        mapped++;
      } else if (humanBone >= 0 || LookupFingerMapping(mmdName)) {
        fprintf(dumpFile,
                "  [--] \"%s\" -> mapped but no Transform (%zu keys)\n",
                mmdName.c_str(), pair.second.keys.size());
        unmapped++;
      } else {
        fprintf(dumpFile, "  [??] \"%s\" -> NO MAPPING (%zu keys)\n",
                mmdName.c_str(), pair.second.keys.size());
        unmapped++;
      }

      g_resolvedMappings->push_back(rm);
    }

    fprintf(dumpFile, "\nMapped: %d, Unmapped: %d, Total: %d\n", mapped,
            unmapped, mapped + unmapped);
    fclose(dumpFile);

    Log("[VMD] Bone mapping: %d mapped, %d unmapped. See eiem_vmd_dump.txt",
        mapped, unmapped);
  }
}


#include "camera_control.h"

static bool IsWindowAlive(HWND hwnd) {
  if (!IsWindow(hwnd))
    return false;
  return true;
}

static DWORD WINAPI HotkeyThread(LPVOID) {
  Log("[OK] Hotkey thread started");

  void *domain = il2cpp_domain_get();
  if (domain)
    il2cpp_thread_attach(domain);

  HWND hwnd = nullptr;
  while (!hwnd) {
    hwnd = FindGameWindow();
    if (!hwnd) Sleep(200);
  }
  g_gameHwnd = hwnd;
  Log("[OK] Game window found: %p (pid=%lu)", hwnd, GetCurrentProcessId());

  g_origWndProc =
      (WNDPROC)SetWindowLongPtrW(hwnd, GWLP_WNDPROC, (LONG_PTR)MmdWndProc);
  if (g_origWndProc) {
    Log("[OK] Game window subclassed for main-thread MMD execution");
  } else {
    Log("[WARN] Failed to subclass game window (err=%lu)", GetLastError());
  }

  LoadEiemConfig();

  bool directTimerResolutionAttempted = false;
  bool directTimerResolutionActive = false;
  while (g_guiRunning && IsWindowAlive(hwnd)) {
    static bool togglePressed = false;
    if (g_pluginActive && (GetAsyncKeyState(g_guiToggleVK) & 0x8000)) {
      if (!togglePressed) {
        togglePressed = true;
        ToggleGui();
      }
    } else {
      togglePressed = false;
    }

    AnimationTick();
    MuscleAnimationTick();
    DirectVmdRuntime_WorkerTick();

    if (g_camTestMode && g_cameraActive) {
      ApplyCameraFrame(0.0f);
    }

    const bool directPlaying =
        DirectVmdRuntime_WantsRealtimeWorkerCadence();
    const bool directHoldingPose =
        DirectVmdRuntime_WantsResponsiveWorkerCadence();
    if (directPlaying && !directTimerResolutionAttempted) {
      directTimerResolutionAttempted = true;
      const MMRESULT timerResult = timeBeginPeriod(1);
      directTimerResolutionActive = timerResult == TIMERR_NOERROR;
      Log("[P3-SAMPLE-TIMER] event=begin periodMs=1 result=%u active=%d "
          "tid=%lu",
          static_cast<unsigned>(timerResult),
          directTimerResolutionActive ? 1 : 0, GetCurrentThreadId());
    } else if (!directPlaying && directTimerResolutionAttempted) {
      if (directTimerResolutionActive)
        timeEndPeriod(1);
      Log("[P3-SAMPLE-TIMER] event=end periodMs=1 wasActive=%d tid=%lu",
          directTimerResolutionActive ? 1 : 0, GetCurrentThreadId());
      directTimerResolutionAttempted = false;
      directTimerResolutionActive = false;
    }
    bool activelyPlaying =
        (g_trojanActive && g_musclePlayer && g_musclePlayer->playing) ||
        directPlaying;
    bool holdingPausedOrEndedPose =
        (g_trojanActive && g_musclePlayer &&
         (g_musclePlayer->currentTime > 0.0f || g_musclePlayer->ended)) ||
        directHoldingPose;

    if (activelyPlaying || g_camTestMode) {
      Sleep(1);
    } else if (holdingPausedOrEndedPose) {
      Sleep(2);
    } else {
      Sleep(50);
    }
  }

  if (directTimerResolutionActive) {
    timeEndPeriod(1);
    Log("[P3-SAMPLE-TIMER] event=end-on-thread-exit periodMs=1 tid=%lu",
        GetCurrentThreadId());
  }
  DirectVmdRuntime_WorkerShutdown();
  Log("[INFO] Game window closed, hotkey thread exiting");
  ExitThread(0);
  return 0;
}

static void DumpTransformHierarchy(void *transform, int depth, FILE *dumpFile) {
  if (!transform || depth > 15)
    return;

  __try {
    char name[256] = "?";
    if (g_object_get_name) {
      void *nameStr = Invoke(g_object_get_name, transform);
      if (nameStr)
        ReadStrUtf8(nameStr, name, sizeof(name));
    }

    Vector3 lp = {0, 0, 0};
    if (g_transform_get_localPosition) {
      void *boxed = Invoke(g_transform_get_localPosition, transform);
      if (boxed)
        lp = *(Vector3 *)((char *)boxed + 16);
    }

    Quaternion lr = {0, 0, 0, 1};
    if (g_transform_get_localRotation) {
      void *boxed = Invoke(g_transform_get_localRotation, transform);
      if (boxed)
        lr = *(Quaternion *)((char *)boxed + 16);
    }

    char indent[64] = {};
    for (int i = 0; i < depth && i < 30; i++) {
      indent[i * 2] = ' ';
      indent[i * 2 + 1] = ' ';
    }

    fprintf(dumpFile, "%s[%s] pos(%.3f,%.3f,%.3f) rot(%.4f,%.4f,%.4f,%.4f)\n",
            indent, name, lp.x, lp.y, lp.z, lr.x, lr.y, lr.z, lr.w);

    if (g_transform_get_childCount && g_transform_GetChild) {
      int childCount = UnboxInt(Invoke(g_transform_get_childCount, transform));
      for (int i = 0; i < childCount && i < 200; i++) {
        int32_t idx = i;
        void *params[] = {&idx};
        void *child = Invoke(g_transform_GetChild, transform, params);
        if (child)
          DumpTransformHierarchy(child, depth + 1, dumpFile);
      }
    }
  } __except (1) {
    fprintf(dumpFile, "[EXCEPTION at depth %d]\n", depth);
  }
}

static void DiscoverSkeleton() {
  Log("=== EIEM Phase 2: Bone Discovery ===");

  FILE *df = fopen("plugin\\eiem_skeleton_dump.txt", "w");
  if (!df) {
    Log("[ERROR] Cannot create dump file");
    return;
  }
  fprintf(df, "=== EIEM Phase 2: Bone Discovery ===\n\n");

  void *domain = il2cpp_domain_get();
  il2cpp_thread_attach(domain);

  fprintf(df, "=== Captured Instances ===\n");
  fprintf(df, "PlayerController: %p\n", g_playerController);
  fprintf(df, "MainCharacter Entity: %p\n", g_mainCharEntity);
  fprintf(df, "Cached Animator: %p\n", g_cachedAnimator);

  if (g_playerController && !g_mainCharEntity) {
    __try {
      int pcOff = SafeOff(OFF_pcEntity, 0x70, "pcEntity");
      g_mainCharEntity = *(void **)((char *)g_playerController + pcOff);
      fprintf(df, "Read mainCharacter from PC: %p\n", g_mainCharEntity);
    } __except (1) {
      fprintf(df, "[ERROR] Failed to read mainCharacter\n");
    }
  }

  if (g_mainCharEntity && !g_cachedAnimator) {
    __try {
      int ecOff = SafeOff(OFF_entityComplexAnim, 0x110, "entityComplexAnim");
      void *complexAnimCom = *(void **)((char *)g_mainCharEntity + ecOff);
      fprintf(df, "ComplexAnimatorComponent: %p\n", complexAnimCom);
      if (complexAnimCom) {
        int caOff =
            SafeOff(OFF_complexAnimAnimator, 0x148, "complexAnimAnimator");
        void *animator = *(void **)((char *)complexAnimCom + caOff);
        fprintf(df, "Animator: %p\n", animator);
        g_cachedAnimator = animator;
      }
    } __except (1) {
      fprintf(df, "[ERROR] Failed to navigate animatorCom chain\n");
    }
  }

  if (g_cachedAnimator) {
    fprintf(df, "\n=== Animator Analysis ===\n");
    fprintf(df, "Animator ptr: %p\n", g_cachedAnimator);

    if (g_animator_get_isHuman) {
      void *boxed = Invoke(g_animator_get_isHuman, g_cachedAnimator);
      bool isHuman = UnboxBool(boxed);
      fprintf(df, "isHuman: %s\n", isHuman ? "YES" : "NO");
      Log("Animator.isHuman = %s", isHuman ? "YES" : "NO");
    }

    if (g_animator_get_avatar) {
      void *avatar = Invoke(g_animator_get_avatar, g_cachedAnimator);
      fprintf(df, "Avatar: %p\n", avatar);
      if (avatar && g_object_get_name) {
        char avatarName[256] = "?";
        void *nameStr = Invoke(g_object_get_name, avatar);
        if (nameStr)
          ReadStrUtf8(nameStr, avatarName, sizeof(avatarName));
        fprintf(df, "Avatar name: %s\n", avatarName);
      }
    }

    if (g_animator_GetBoneTransform) {
      fprintf(df, "\n=== HumanBodyBones Enumeration ===\n");
      int foundCount = 0;
      for (int bone = 0; bone < g_humanBoneCount; bone++) {
        __try {
          int32_t boneId = bone;
          void *params[] = {&boneId};
          void *boneTransform =
              Invoke(g_animator_GetBoneTransform, g_cachedAnimator, params);
          if (boneTransform) {
            foundCount++;
            char boneName[256] = "?";
            if (g_object_get_name) {
              void *nameStr = Invoke(g_object_get_name, boneTransform);
              if (nameStr)
                ReadStrUtf8(nameStr, boneName, sizeof(boneName));
            }

            Vector3 lp = {0, 0, 0};
            if (g_transform_get_localPosition) {
              void *boxed =
                  Invoke(g_transform_get_localPosition, boneTransform);
              if (boxed)
                lp = *(Vector3 *)((char *)boxed + 16);
            }
            Quaternion lr = {0, 0, 0, 1};
            if (g_transform_get_localRotation) {
              void *boxed =
                  Invoke(g_transform_get_localRotation, boneTransform);
              if (boxed)
                lr = *(Quaternion *)((char *)boxed + 16);
            }

            fprintf(df,
                    "  [%2d] %-30s —\"%s\" pos(%.3f,%.3f,%.3f) "
                    "rot(%.4f,%.4f,%.4f,%.4f)\n",
                    bone, g_humanBoneNames[bone], boneName, lp.x, lp.y, lp.z,
                    lr.x, lr.y, lr.z, lr.w);
          } else {
            fprintf(df, "  [%2d] %-30s —NULL\n", bone,
                    g_humanBoneNames[bone]);
          }
        } __except (1) {
          fprintf(df, "  [%2d] %-30s —EXCEPTION\n", bone,
                  g_humanBoneNames[bone]);
        }
      }
      fprintf(df, "\nTotal bones found: %d / %d\n", foundCount,
              g_humanBoneCount);
    }

    if (g_component_get_transform) {
      void *animTransform = Invoke(g_component_get_transform, g_cachedAnimator);
      if (animTransform) {
        fprintf(df, "\n=== Character Transform Hierarchy ===\n");
        DumpTransformHierarchy(animTransform, 0, df);
      }
    }

    fprintf(df, "\n=== BlendShape Discovery ===\n");
    if (g_component_get_gameObject && g_smr_get_sharedMesh &&
        g_mesh_get_blendShapeCount && g_mesh_GetBlendShapeName) {
      void *go = nullptr;
      __try {
        go = Invoke(g_component_get_gameObject, g_cachedAnimator);
      } __except (1) {
      }
      if (go) {
        void *animTransform = nullptr;
        __try {
          animTransform = Invoke(g_component_get_transform, g_cachedAnimator);
        } __except (1) {
        }
        if (animTransform) {
          const char *faceMeshNames[] = {"S_actor_endminf_face_01_lod0",
                                         "S_actor_endminf_body_01_lod0",
                                         nullptr};
          for (int mi = 0; faceMeshNames[mi]; mi++) {
            void *meshTransform =
                SafeFindChildRecursive(animTransform, faceMeshNames[mi], 5);
            if (!meshTransform)
              continue;

            void *meshGo = nullptr;
            __try {
              meshGo = Invoke(g_component_get_gameObject, meshTransform);
            } __except (1) {
              continue;
            }
            if (!meshGo)
              continue;

            void *smrType = il2cpp_class_get_type(g_skinnedMeshRendererClass);
            void *smrTypeObj = il2cpp_type_get_object(smrType);
            void *smr = nullptr;
            __try {
              void *params[] = {smrTypeObj};
              smr = Invoke(g_gameObject_GetComponent, meshGo, params);
            } __except (1) {
              continue;
            }
            if (!smr)
              continue;

            void *mesh = nullptr;
            __try {
              mesh = Invoke(g_smr_get_sharedMesh, smr);
            } __except (1) {
              continue;
            }
            if (!mesh)
              continue;

            void *countBoxed = nullptr;
            __try {
              countBoxed = Invoke(g_mesh_get_blendShapeCount, mesh);
            } __except (1) {
              continue;
            }
            int bsCount = countBoxed ? *(int *)((char *)countBoxed + 16) : 0;

            fprintf(df, "\n[%s] %d blend shapes:\n", faceMeshNames[mi],
                    bsCount);
            for (int bs = 0; bs < bsCount; bs++) {
              __try {
                void *params[] = {&bs};
                void *nameStr = Invoke(g_mesh_GetBlendShapeName, mesh, params);
                char bsName[256] = "?";
                if (nameStr)
                  ReadStrUtf8(nameStr, bsName, sizeof(bsName));

                void *wParams[] = {&bs};
                void *wBoxed = Invoke(g_smr_GetBlendShapeWeight, smr, wParams);
                float weight = wBoxed ? *(float *)((char *)wBoxed + 16) : 0.0f;

                fprintf(df, "  [%3d] %s (weight=%.1f)\n", bs, bsName, weight);
              } __except (1) {
                fprintf(df, "  [%3d] EXCEPTION\n", bs);
              }
            }
          }
        }
      }
    } else {
      fprintf(df, "  (SkinnedMeshRenderer API not resolved)\n");
    }

  } else {
    fprintf(df, "\n[WARN] No Animator captured yet.\n");
    fprintf(df, "Make sure you're in-game with a character loaded.\n");
    fprintf(df, "The GetMainCharacter hook needs to fire first.\n");
  }

  fprintf(df, "\n=== Unity API Status ===\n");
  fprintf(df, "Animator.GetBoneTransform: %s\n",
          g_animator_GetBoneTransform ? "YES" : "NO");
  fprintf(df, "Animator.get_isHuman: %s\n",
          g_animator_get_isHuman ? "YES" : "NO");
  fprintf(df, "Transform.set_localRotation: %s\n",
          g_transform_set_localRotation ? "YES" : "NO");
  fprintf(df, "Transform.set_localPosition: %s\n",
          g_transform_set_localPosition ? "YES" : "NO");

  fflush(df);
  fclose(df);
  Log("[OK] Phase 2 bone discovery written to eiem_skeleton_dump.txt");
}


static DWORD WINAPI InitThread(LPVOID) {
  while (!GetModuleHandleW(L"GameAssembly.dll"))
    Sleep(500);
  Sleep(3000);

  InitializeCriticalSection(&g_logLock);
  g_logHandle =
      CreateFileA("plugin\\eiem_log.txt", GENERIC_WRITE, FILE_SHARE_READ, NULL,
                  CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
  Log("=== EIEM Phase 1: Skeleton Discovery ===");

  if (!Resolve()) {
    Log("[FATAL] Failed to resolve IL2CPP API");
    return 1;
  }
  Log("[OK] IL2CPP API resolved");

  InitHardcodedMuscleMap();

  void *domain = il2cpp_domain_get();
  il2cpp_thread_attach(domain);
  size_t ac;
  void **asms = il2cpp_domain_get_assemblies(domain, &ac);
  Log("[OK] Domain attached, %zu assemblies", ac);

  MH_Initialize();


  g_transformClass = FindClass("UnityEngine", "Transform", asms, ac);
  if (g_transformClass) {
    Log("[OK] Transform class found");
    g_transform_get_localRotation =
        FindMethod(g_transformClass, "get_localRotation", 0);
    g_transform_set_localRotation =
        FindMethod(g_transformClass, "set_localRotation", 1);
    g_transform_get_localPosition =
        FindMethod(g_transformClass, "get_localPosition", 0);
    g_transform_set_localPosition =
        FindMethod(g_transformClass, "set_localPosition", 1);
    g_transform_get_childCount =
        FindMethod(g_transformClass, "get_childCount", 0);
    g_transform_GetChild = FindMethod(g_transformClass, "GetChild", 1);
    g_transform_Find = FindMethod(g_transformClass, "Find", 1);
    g_transform_get_parent = FindMethod(g_transformClass, "get_parent", 0);
    g_transform_get_position = FindMethod(g_transformClass, "get_position", 0);
    Log("  get_localRotation: %p", g_transform_get_localRotation);
    Log("  set_localRotation: %p", g_transform_set_localRotation);
    Log("  get_localPosition: %p", g_transform_get_localPosition);
    Log("  set_localPosition: %p", g_transform_set_localPosition);
    Log("  get_childCount: %p", g_transform_get_childCount);
    Log("  GetChild: %p", g_transform_GetChild);
    Log("  Find: %p", g_transform_Find);
    Log("  get_position: %p", g_transform_get_position);
  } else {
    Log("[WARN] Transform class NOT found");
  }

  void *objectClass = FindClass("UnityEngine", "Object", asms, ac);
  if (objectClass) {
    g_object_get_name = FindMethod(objectClass, "get_name", 0);
    Log("[OK] Object.get_name: %p", g_object_get_name);
  }

  void *physicsClass = FindClass("UnityEngine", "Physics", asms, ac);
  if (physicsClass) {
    void *raycast = FindMethod(physicsClass, "Raycast", 6);
    const char *p0 = nullptr;
    const char *p2 = nullptr;
    const char *p5 = nullptr;
    if (raycast && il2cpp_method_get_param && il2cpp_type_get_name) {
      void *t0 = il2cpp_method_get_param(raycast, 0);
      void *t2 = il2cpp_method_get_param(raycast, 2);
      void *t5 = il2cpp_method_get_param(raycast, 5);
      p0 = t0 ? il2cpp_type_get_name(t0) : nullptr;
      p2 = t2 ? il2cpp_type_get_name(t2) : nullptr;
      p5 = t5 ? il2cpp_type_get_name(t5) : nullptr;
    }
    const bool signatureOk = p0 && p2 && p5 &&
        strstr(p0, "Vector3") && strstr(p2, "RaycastHit") &&
        strstr(p5, "QueryTriggerInteraction");
    g_physicsRaycastMethod = signatureOk ? raycast : nullptr;
    Log("[P7-PHYSICS-FALLBACK-API] Physics.Raycast method=%p signatureOk=%d "
        "p0=%s p2=%s p5=%s",
        g_physicsRaycastMethod, signatureOk ? 1 : 0,
        p0 ? p0 : "?", p2 ? p2 : "?", p5 ? p5 : "?");
  } else {
    Log("[P7-PHYSICS-FALLBACK-API] Physics class unavailable");
  }

  g_animatorClass = FindClass("UnityEngine", "Animator", asms, ac);
  if (g_animatorClass) {
    Log("[OK] Animator class found");
    g_animator_GetBoneTransform =
        FindMethod(g_animatorClass, "GetBoneTransform", 1);
    g_animator_get_avatar = FindMethod(g_animatorClass, "get_avatar", 0);
    g_animator_get_isHuman = FindMethod(g_animatorClass, "get_isHuman", 0);
    g_animator_Rebind = FindMethod(g_animatorClass, "Rebind", 0);
    g_animator_Update = FindMethod(g_animatorClass, "Update", 1);
    g_animator_SetBoneLocalRotation =
        FindMethod(g_animatorClass, "SetBoneLocalRotation", 2);
    Log("  GetBoneTransform: %p", g_animator_GetBoneTransform);
    Log("  get_avatar: %p", g_animator_get_avatar);
    Log("  get_isHuman: %p", g_animator_get_isHuman);
    Log("  Rebind: %p  Update: %p", g_animator_Rebind, g_animator_Update);
    Log("  SetBoneLocalRotation: %p", g_animator_SetBoneLocalRotation);

    void *behaviourClass = FindClass("UnityEngine", "Behaviour", asms, ac);
    if (behaviourClass) {
      g_animator_get_enabled = FindMethod(behaviourClass, "get_enabled", 0);
      g_animator_set_enabled = FindMethod(behaviourClass, "set_enabled", 1);
    }
    Log("  get/set_enabled: %p / %p (from Behaviour)", g_animator_get_enabled,
        g_animator_set_enabled);
  } else {
    Log("[WARN] Animator class NOT found");
  }

  g_humanPoseHandlerClass =
      FindClass("UnityEngine", "HumanPoseHandler", asms, ac);
  if (g_humanPoseHandlerClass) {
    void *iter = nullptr;
    void *method;
    Log("[HPH] Enumerating HumanPoseHandler methods:");
    while ((method = il2cpp_class_get_methods(g_humanPoseHandlerClass,
                                              &iter)) != nullptr) {
      const char *name = il2cpp_method_get_name(method);
      int nparams = il2cpp_method_get_param_count(method);
      Log("[HPH]   %s (%d params) = %p", name, nparams, method);

      if (strcmp(name, ".ctor") == 0 && nparams == 2)
        g_humanPoseHandler_ctor = method;
      if (strcmp(name, "SetHumanPose") == 0)
        g_humanPoseHandler_SetHumanPose = method;
      if (strcmp(name, "GetHumanPose") == 0)
        g_humanPoseHandler_GetHumanPose = method;
      if (strcmp(name, "Dispose") == 0)
        g_humanPoseHandler_Dispose = method;
      if (strcmp(name, "GetInternalAvatarPose") == 0) {
        Log("[HPH]   *** Found GetInternalAvatarPose as MethodInfo! ***");
      }
    }

    __try {
      il2cpp_class_get_method_from_name(g_humanPoseHandlerClass,
                                        "SetInternalAvatarPose", 1);
    } __except (1) {
      Log("[HPH]   get_method_from_name crashed for SetInternalAvatarPose");
    }
    __try {
      il2cpp_class_get_method_from_name(g_humanPoseHandlerClass,
                                        "SetInternalHumanPose", 2);
    } __except (1) {
      Log("[HPH]   get_method_from_name crashed for SetInternalHumanPose");
    }

    if (!g_humanPoseHandler_SetHumanPose && il2cpp_resolve_icall) {
      Log("[HPH] SetHumanPose stripped! Resolving native icalls...");

      void *fn1 = il2cpp_resolve_icall(
          "UnityEngine.HumanPoseHandler::SetInternalAvatarPose");
      if (fn1) {
        g_icall_SetInternalAvatarPose = (fn_InternalAvatarPose)fn1;
        Log("[HPH]   SetInternalAvatarPose = %p", fn1);
      }

      void *fn2 = il2cpp_resolve_icall(
          "UnityEngine.HumanPoseHandler::SetInternalHumanPose");
      if (fn2) {
        g_icall_SetInternalHumanPose = (fn_InternalHumanPose)fn2;
        Log("[HPH]   SetInternalHumanPose = %p", fn2);
      }

      void *fn3 =
          il2cpp_resolve_icall("UnityEngine.HumanPoseHandler::SetHumanPose");
      if (fn3) {
        g_icall_SetHumanPose = (fn_SetHumanPose_compiled)fn3;
        Log("[HPH]   SetHumanPose compiled = %p", fn3);
      }

      auto dumpBytes64 = [](const char *name, void *fn) {
        if (!fn)
          return;
        unsigned char *p = (unsigned char *)fn;
        for (int row = 0; row < 4; row++) {
          int o = row * 16;
          Log("[HPH]   %s +%02X: %02X %02X %02X %02X %02X %02X %02X %02X %02X "
              "%02X %02X %02X %02X %02X %02X %02X",
              name, o, p[o + 0], p[o + 1], p[o + 2], p[o + 3], p[o + 4],
              p[o + 5], p[o + 6], p[o + 7], p[o + 8], p[o + 9], p[o + 10],
              p[o + 11], p[o + 12], p[o + 13], p[o + 14], p[o + 15]);
        }
      };
      void *fnGet = il2cpp_resolve_icall(
          "UnityEngine.HumanPoseHandler::GetInternalAvatarPose");
      g_icall_GetInternalAvatarPose = (fn_InternalAvatarPose)fnGet;
      Log("[HPH]   GetInternalAvatarPose = %p", fnGet);

      if (fnGet && g_icall_SetInternalAvatarPose) {
        MH_STATUS st =
            MH_CreateHook(fnGet, (void *)Hooked_GetInternalAvatarPose,
                          (void **)&orig_GetInternalAvatarPose);
        if (st == MH_OK) {
          MH_EnableHook(fnGet);
          g_trojanHookTarget = fnGet;
          Log("[TROJAN] Hooked GetInternalAvatarPose! orig=%p",
              orig_GetInternalAvatarPose);
        } else {
          Log("[TROJAN] Hook failed: MH_STATUS=%d", st);
        }
      }
      dumpBytes64("SetAvatarPose", fn1);
      dumpBytes64("GetAvatarPose", fnGet);
      dumpBytes64("SetHumanPose", fn3);

      void *compiledGet = ((void **)g_humanPoseHandler_GetHumanPose)[0];
      if (compiledGet) {
        Log("[SCAN] Compiled GetHumanPose at %p", compiledGet);
        unsigned char *p = (unsigned char *)compiledGet;

        uintptr_t gaBase = (uintptr_t)GetModuleHandleW(L"GameAssembly.dll");
        int callOffset = -1;
        int movOffset = -1;
        int leaOffset = -1;
        unsigned char *h = nullptr;

        for (int i = 0; i < 128; i++) {
          if (p[i] != 0xE8)
            continue;
          int32_t disp = *(int32_t *)(p + i + 1);
          uintptr_t target = (uintptr_t)(p + i + 5) + disp;
          if (target <= gaBase || target >= gaBase + 0x10000000)
            continue;

          unsigned char *candidate = (unsigned char *)target;
          int foundMov = -1;
          __try {
            for (int j = 0; j < 96; j++) {
              if (candidate[j] == 0x48 && candidate[j+1] == 0x8B && candidate[j+2] == 0x05) {
                foundMov = j;
                break;
              }
            }
          } __except (1) {
            continue;
          }

          if (foundMov >= 0) {
            callOffset = i;
            movOffset = foundMov;
            h = candidate;
            Log("[SCAN] Helper thunk at %p (found CALL at +0x%X, MOV at +0x%X)",
                (void *)target, i, foundMov);

            for (int j = 0; j < 96; j++) {
              if (h[j] == 0x48 && h[j+1] == 0x8D && h[j+2] == 0x0D) {
                leaOffset = j;
                break;
              }
            }
            break;
          }
        }

        if (callOffset >= 0 && movOffset >= 0 && h) {
          int32_t slotDisp = *(int32_t *)(h + movOffset + 3);
          uintptr_t slotAddr = (uintptr_t)(h + movOffset + 7) + slotDisp;
          void *cachedFn = nullptr;
          __try {
            cachedFn = *(void **)slotAddr;
          } __except (1) {
          }
          Log("[SLOT] Cached icall slot at %p fn=%p", (void *)slotAddr,
              cachedFn);

          if (leaOffset >= 0) {
            int32_t nameDisp = *(int32_t *)(h + leaOffset + 3);
            const char *nameStr =
                (const char *)((uintptr_t)(h + leaOffset + 7) + nameDisp);
            char nameBuf[256] = {};
            __try {
              strncpy(nameBuf, nameStr, sizeof(nameBuf) - 1);
              nameBuf[sizeof(nameBuf) - 1] = '\0';
            } __except (1) {
              strncpy(nameBuf, "<unreadable>", sizeof(nameBuf) - 1);
              nameBuf[sizeof(nameBuf) - 1] = '\0';
            }
            Log("[SLOT] icall name: \"%s\"", nameBuf);

            char setName[256] = {};
            strncpy(setName, nameBuf, sizeof(setName) - 1);
            setName[sizeof(setName) - 1] = '\0';
            char *getPos = strstr(setName, "Get");
            if (getPos) {
              memcpy(getPos, "Set", 3);
              Log("[SLOT] Resolving Set version: \"%s\"", setName);
              void *setFn = il2cpp_resolve_icall(setName);
              Log("[SLOT] Set function resolved: %p", setFn);

              if (setFn) {
                g_slotAddr = (void **)slotAddr;
                g_slotOrigGet = cachedFn;
                g_slotSetFn = setFn;
                Log("[SLOT] *** READY TO PATCH! slot=%p get=%p set=%p ***",
                    (void *)slotAddr, cachedFn, setFn);
              }
            }
          } else {
            Log("[SCAN] LEA RCX pattern (48 8D 0D) not found in helper");
          }
        } else {
          Log("[SCAN] No E8 CALL with icall slot pattern found in first 128 "
              "bytes of compiled GetHumanPose");
        }
      }
    }

    Log("[OK] HumanPoseHandler: ctor=%p Get=%p SetHP=%p SetAvatar=%p "
        "SetHuman=%p",
        g_humanPoseHandler_ctor, g_humanPoseHandler_GetHumanPose,
        g_icall_SetHumanPose, g_icall_SetInternalAvatarPose,
        g_icall_SetInternalHumanPose);
  } else {
    Log("[WARN] HumanPoseHandler NOT found");
  }

  g_gameObjectClass = FindClass("UnityEngine", "GameObject", asms, ac);
  if (g_gameObjectClass) {
    g_gameObject_get_transform =
        FindMethod(g_gameObjectClass, "get_transform", 0);
    Log("[OK] GameObject.get_transform: %p", g_gameObject_get_transform);
  }

  g_componentClass = FindClass("UnityEngine", "Component", asms, ac);
  if (g_componentClass) {
    g_component_get_gameObject =
        FindMethod(g_componentClass, "get_gameObject", 0);
    g_component_get_transform =
        FindMethod(g_componentClass, "get_transform", 0);
    Log("[OK] Component.get_transform: %p", g_component_get_transform);
  }

  if (g_gameObjectClass) {
    g_gameObject_GetComponent =
        FindMethod(g_gameObjectClass, "GetComponent", 1);
    Log("[OK] GameObject.GetComponent: %p", g_gameObject_GetComponent);
  }

  GhostRig_ResolveUnityApis(asms, ac);

  g_skinnedMeshRendererClass =
      FindClass("UnityEngine", "SkinnedMeshRenderer", asms, ac);
  if (g_skinnedMeshRendererClass) {
    g_smr_get_sharedMesh =
        FindMethod(g_skinnedMeshRendererClass, "get_sharedMesh", 0);
    g_smr_GetBlendShapeWeight =
        FindMethod(g_skinnedMeshRendererClass, "GetBlendShapeWeight", 1);
    g_smr_SetBlendShapeWeight =
        FindMethod(g_skinnedMeshRendererClass, "SetBlendShapeWeight", 2);
    g_smr_get_bones = FindMethod(g_skinnedMeshRendererClass, "get_bones", 0);
    Log("[OK] SkinnedMeshRenderer: sharedMesh=%p, GetWeight=%p, SetWeight=%p, "
        "get_bones=%p",
        g_smr_get_sharedMesh, g_smr_GetBlendShapeWeight,
        g_smr_SetBlendShapeWeight, g_smr_get_bones);
  } else {
    Log("[WARN] SkinnedMeshRenderer class NOT found");
  }

  void *meshClass = FindClass("UnityEngine", "Mesh", asms, ac);
  if (meshClass) {
    g_mesh_get_blendShapeCount =
        FindMethod(meshClass, "get_blendShapeCount", 0);
    g_mesh_GetBlendShapeName = FindMethod(meshClass, "GetBlendShapeName", 1);
    Log("[OK] Mesh: blendShapeCount=%p, GetBlendShapeName=%p",
        g_mesh_get_blendShapeCount, g_mesh_GetBlendShapeName);
  } else {
    Log("[WARN] Mesh class NOT found");
  }

  g_cameraClass = FindClass("UnityEngine", "Camera", asms, ac);
  if (g_cameraClass) {
    g_camera_get_main = FindMethod(g_cameraClass, "get_main", 0);
    g_camera_get_fieldOfView = FindMethod(g_cameraClass, "get_fieldOfView", 0);
    g_camera_set_fieldOfView = FindMethod(g_cameraClass, "set_fieldOfView", 1);
    Log("[CAM] Camera class found: get_main=%p get_fov=%p set_fov=%p",
        g_camera_get_main, g_camera_get_fieldOfView, g_camera_set_fieldOfView);
  } else {
    Log("[WARN] Camera class NOT found");
  }

  if (!g_camSetPos) {
    g_camSetPos = (void (*)(void *, float *))il2cpp_resolve_icall(
        "UnityEngine.Transform::set_position_Injected(UnityEngine.Vector3&)");
    g_camSetRot = (void (*)(void *, float *))il2cpp_resolve_icall(
        "UnityEngine.Transform::set_rotation_Injected(UnityEngine.Quaternion&)");
    g_camGetPos = (CamGetPosRot_t)il2cpp_resolve_icall(
        "UnityEngine.Transform::get_position_Injected(UnityEngine.Vector3&)");
    g_camGetRot = (CamGetPosRot_t)il2cpp_resolve_icall(
        "UnityEngine.Transform::get_rotation_Injected(UnityEngine.Quaternion&)");
    Log("[CAM] Transform icalls: setPos=%p setRot=%p getPos=%p getRot=%p",
        g_camSetPos, g_camSetRot, g_camGetPos, g_camGetRot);

    void *setLocalPos = il2cpp_resolve_icall(
        "UnityEngine.Transform::set_localPosition_Injected(UnityEngine.Vector3&)");
    void *setLocalRot = il2cpp_resolve_icall(
        "UnityEngine.Transform::set_localRotation_Injected(UnityEngine.Quaternion&)");

    if (g_camSetPos &&
        MH_CreateHook((void *)g_camSetPos, (void *)Hook_SetPos,
                      (void **)&g_origSetPos) == MH_OK)
      MH_EnableHook((void *)g_camSetPos);
    if (g_camSetRot &&
        MH_CreateHook((void *)g_camSetRot, (void *)Hook_SetRot,
                      (void **)&g_origSetRot) == MH_OK)
      MH_EnableHook((void *)g_camSetRot);
    if (setLocalPos &&
        MH_CreateHook(setLocalPos, (void *)Hook_SetLocalPos,
                      (void **)&g_origSetLocalPos) == MH_OK)
      MH_EnableHook(setLocalPos);
    if (setLocalRot &&
        MH_CreateHook(setLocalRot, (void *)Hook_SetLocalRot,
                      (void **)&g_origSetLocalRot) == MH_OK)
      MH_EnableHook(setLocalRot);
    Log("[CAM] Transform write-hooks: setPos=%p setRot=%p setLPos=%p setLRot=%p",
        g_origSetPos, g_origSetRot, g_origSetLocalPos, g_origSetLocalRot);
  }

  {
    uintptr_t gaBase2 = (uintptr_t)GetModuleHandleW(L"GameAssembly.dll");

    void *animatorMonoClass = FindClass(
        "Beyond.Gameplay.View.Animation", "AnimatorMono", asms, ac);
    void *animatorPreLateTick = animatorMonoClass
        ? FindMethod(animatorMonoClass, "PreLateTick", 1)
        : nullptr;
    if (animatorPreLateTick &&
        Hook(animatorPreLateTick, "AnimatorMono.PreLateTick",
             (void *)GhostRig_HookedAnimatorPreLateTick,
             &s_ghostOrigAnimatorPreLateTick)) {
      Log("[P0-ORDER] AnimatorMono.PreLateTick probe installed");
    } else {
      Log("[P0-ORDER] WARN: AnimatorMono.PreLateTick probe unavailable");
    }
    void *animatorMove = animatorMonoClass
        ? FindMethod(animatorMonoClass, "OnAnimatorMove", 0)
        : nullptr;
    if (animatorMove &&
        Hook(animatorMove, "AnimatorMono.OnAnimatorMove",
             (void *)GhostRig_HookedAnimatorMove,
             &s_ghostOrigAnimatorMove)) {
      Log("[P0-ORDER] AnimatorMono.OnAnimatorMove probe installed");
    } else {
      Log("[P0-ORDER] WARN: AnimatorMono.OnAnimatorMove probe unavailable");
    }

    void *moveCompClass = FindClass("Beyond.Gameplay.Core", "MovementComponent", asms, ac);
    if (moveCompClass) {
      void *tickMethod = FindMethod(moveCompClass, "Tick", 1);
      if (tickMethod) {
        if (Hook(tickMethod, "MovementComponent.Tick",
                 (void *)Hooked_MovementComponent_Tick, &s_origMoveTick)) {
          Log("[GF2] MovementComponent.Tick hooked via il2cpp");
        } else {
          Log("[GF2] WARN: Hook() failed for MovementComponent.Tick");
        }
      } else {
        Log("[GF2] WARN: MovementComponent.Tick method not found");
      }

      const char *floorNames[] = {"currentFloor"};
      const char *matchedName = nullptr;
      int off = FindFieldInHierarchy(moveCompClass, floorNames, 1, &matchedName);
      if (off >= 0) {
        g_offCurrentFloor = off;
        Log("[GF2] currentFloor offset = 0x%X", off);
      } else {
        Log("[GF2] WARN: currentFloor field not found, using default 0x2e8");
      }

      g_findFloorMethod = FindMethod(moveCompClass, "FindFloor", 3);
      Log("[GF2] FindFloor method = %p", g_findFloorMethod);

      g_computeFloorDistMethod =
          FindMethod(moveCompClass, "ComputeFloorDist", 7);
      Log("[P7-FINDFLOOR-API] ComputeFloorDist method = %p",
          g_computeFloorDistMethod);

      const char *entityNames[] = {
          "<entity>k__BackingField",
          "m_entity",
          "entity",
          "<m_entity>k__BackingField",
          "_entity"
      };
      const char *entMatch = nullptr;
      int entOff = FindFieldInHierarchy(moveCompClass, entityNames, 5, &entMatch);
      if (entOff >= 0) {
        g_offBaseCompEntity = entOff;
        Log("[GF2] BaseComponent.entity offset = 0x%X (%s)", entOff, entMatch);
      } else {
        Log("[GF2] WARN: entity field not found, using default 0x50");
      }

      const char *grounderNames[] = {"m_bipedIK"};
      const char *grounderMatch = nullptr;
      const int grounderOff = FindFieldInHierarchy(
          moveCompClass, grounderNames, 1, &grounderMatch);
      if (grounderOff >= 0) {
        g_offMovementGrounder = grounderOff;
        Log("[P7-GROUNDER-API] MovementComponent.%s offset=0x%X",
            grounderMatch, grounderOff);
      } else {
        Log("[P7-GROUNDER-API] WARN: MovementComponent.m_bipedIK "
            "not found, using verified fallback 0x%X",
            g_offMovementGrounder);
      }
    } else {
      Log("[GF2] WARN: MovementComponent class not found");
    }

    void *solverMgrClass = FindClass("RootMotion", "SolverManager", asms, ac);
    if (!solverMgrClass)
      solverMgrClass = FindClass("RootMotion.FinalIK", "SolverManager", asms, ac);
    void *lateUpdateMethod = solverMgrClass ? FindMethod(solverMgrClass, "LateUpdate", 0) : nullptr;
    if (lateUpdateMethod && Hook(lateUpdateMethod, "SolverManager.LateUpdate",
                                 (void *)Hooked_SolverManager_LateUpdate, &s_origLateUpdate)) {
      Log("[IK] SolverManager.LateUpdate hooked dynamically via IL2CPP");
    } else {
      void *lateUpdateAddr = (void *)(gaBase2 + 0x035BD200);
      if (MH_CreateHook(lateUpdateAddr, (void *)Hooked_SolverManager_LateUpdate,
                        &s_origLateUpdate) == MH_OK) {
        MH_EnableHook(lateUpdateAddr);
        Log("[IK] SolverManager.LateUpdate hooked via fallback RVA %p", lateUpdateAddr);
      } else {
        Log("[IK] WARN: Failed to hook SolverManager.LateUpdate");
      }
    }

    void *bipedIKClass = FindClass("RootMotion.FinalIK", "BipedIK", asms, ac);
    void *updateSolverMethod = bipedIKClass ? FindMethod(bipedIKClass, "UpdateSolver", 0) : nullptr;
    if (updateSolverMethod && Hook(updateSolverMethod, "BipedIK.UpdateSolver",
                                   (void *)Hooked_IK_UpdateSolver, &s_origUpdateSolver)) {
      Log("[IK] BipedIK.UpdateSolver hooked dynamically via IL2CPP");
    } else {
      void *updateSolverAddr = (void *)(gaBase2 + 0x0326A380);
      if (MH_CreateHook(updateSolverAddr, (void *)Hooked_IK_UpdateSolver,
                        &s_origUpdateSolver) == MH_OK) {
        MH_EnableHook(updateSolverAddr);
        Log("[IK] BipedIK.UpdateSolver hooked via fallback RVA %p", updateSolverAddr);
      } else {
        Log("[IK] WARN: Failed to hook BipedIK.UpdateSolver");
      }
    }

    void *grounderClass = FindClass(
        "RootMotion.FinalIK", "GrounderBipedIK", asms, ac);
    void *grounderOnSolver = grounderClass
        ? FindMethod(grounderClass, "OnSolverUpdate", 0)
        : nullptr;
    void *grounderOnPost = grounderClass
        ? FindMethod(grounderClass, "OnPostSolverUpdate", 0)
        : nullptr;
    const bool grounderSolverHooked = grounderOnSolver &&
        Hook(grounderOnSolver, "GrounderBipedIK.OnSolverUpdate",
             (void *)GhostRig_HookedGrounderOnSolverUpdate,
             &s_ghostOrigGrounderOnSolverUpdate);
    const bool grounderPostHooked = grounderOnPost &&
        Hook(grounderOnPost, "GrounderBipedIK.OnPostSolverUpdate",
             (void *)GhostRig_HookedGrounderOnPostSolverUpdate,
             &s_ghostOrigGrounderOnPostSolverUpdate);
    Log("[P7-GROUNDER-API] class=%p OnSolverUpdate=%p hook=%d "
        "OnPostSolverUpdate=%p hook=%d Update=%p ResetPosition=%p",
        grounderClass, grounderOnSolver,
        grounderSolverHooked ? 1 : 0, grounderOnPost,
        grounderPostHooked ? 1 : 0, s_ghostGrounderUpdate,
        s_ghostGrounderResetPosition);

    void *ikTrigClass = FindClass("RootMotion.FinalIK", "IKSolverTrigonometric", asms, ac);
    void *onUpdateMethod = ikTrigClass ? FindMethod(ikTrigClass, "OnUpdate", 0) : nullptr;
    if (onUpdateMethod && Hook(onUpdateMethod, "IKSolverTrigonometric.OnUpdate",
                               (void *)Hooked_OnUpdate,
                               &g_origIkTrigOnUpdate)) {
      Log("[IK] IKSolverTrigonometric.OnUpdate hooked dynamically via IL2CPP");
    } else {
      void *onUpdateAddr = (void *)(gaBase2 + 0x032759E0);
      if (MH_CreateHook(onUpdateAddr, (void *)Hooked_OnUpdate,
                        &g_origIkTrigOnUpdate) == MH_OK) {
        MH_EnableHook(onUpdateAddr);
        Log("[IK] IKSolverTrigonometric.OnUpdate hooked via fallback RVA %p", onUpdateAddr);
      } else {
        Log("[IK] WARN: Failed to hook IKSolverTrigonometric.OnUpdate");
      }
    }
  }

  void *pcClass =
      FindClass("Beyond.Gameplay.Core", "PlayerController", asms, ac);
  if (pcClass) {
    Log("[OK] PlayerController class found");

    {
      const char *entityNames[] = {"mainCharacter", "m_entity", "_entity",
                                   "m_mainCharacter", "entity",
                                   "m_controlledEntity", "controlledEntity"};
      const char *matchedName = nullptr;
      OFF_pcEntity = FindFieldInHierarchy(pcClass, entityNames, 7, &matchedName);
      if (OFF_pcEntity >= 0) {
        Log("[OFFSET] PlayerController.%s = %d (0x%X)", matchedName,
            OFF_pcEntity, OFF_pcEntity);
      } else {
        Log("[WARN] PlayerController entity field not found in hierarchy, "
            "using fallback 0x70");
        Log("[WARN] PlayerController full hierarchy:");
        DumpFieldsHierarchy(pcClass);
      }
    }


    void *setMainChar = FindMethod(pcClass, "SetMainCharacter", 2);
    if (setMainChar) {
      typedef void (__fastcall *SetMainCharacter_t)(void *self, void *entity, int32_t reason, void *methodInfo);
      static SetMainCharacter_t orig_SetMainCharacter = nullptr;

      struct SetMainCharHook {
        static void __fastcall Hooked(void *self, void *entity, int32_t reason, void *methodInfo) {
          GhostRig_RequestOwnerChange(entity);
          GhostRig_TryImmediateCleanupOnCurrentThread(
              GhostRigCleanupReason::CharacterSwitch);

          if (self && !g_playerController) {
            g_playerController = self;
            Log("[HOOK] Captured PlayerController: %p", self);
          }
          if (entity) {
            g_mainCharEntity = entity;
            Log("[HOOK] SetMainCharacter: Entity=%p reason=%d", entity, reason);

            if (OFF_entityComplexAnim < 0) {
              __try {
                void *entClass = il2cpp_object_get_class(entity);
                if (entClass) {
                  const char *caNames[] = {
                      "<animatorCom>k__BackingField",
                      "animatorCom",
                      "complexAnimationComponent",
                      "m_complexAnimationComponent",
                      "_complexAnimationComponent"};
                  const char *matchedName = nullptr;
                  OFF_entityComplexAnim =
                      FindFieldInHierarchy(entClass, caNames, 5, &matchedName);
                  if (OFF_entityComplexAnim >= 0) {
                    Log("[OFFSET] Entity.%s = %d (0x%X)", matchedName,
                        OFF_entityComplexAnim, OFF_entityComplexAnim);
                  } else {
                    Log("[WARN] Entity complexAnimationComponent not found in "
                        "hierarchy, fallback 0x110");
                    Log("[WARN] Entity full hierarchy:");
                    DumpFieldsHierarchy(entClass);
                  }
                }
              } __except (1) {
              }
            }

            __try {
              int ecOff =
                  SafeOff(OFF_entityComplexAnim, 0x110, "entityComplexAnim");
              void *complexAnimCom = *(void **)((char *)entity + ecOff);
              if (complexAnimCom) {
                if (OFF_complexAnimAnimator < 0) {
                  __try {
                    void *cacClass = il2cpp_object_get_class(complexAnimCom);
                    if (cacClass) {
                      const char *animNames[] = {"animator", "m_animator",
                                                 "_animator",
                                                 "<animator>k__BackingField"};
                      const char *matchedName2 = nullptr;
                      OFF_complexAnimAnimator = FindFieldInHierarchy(
                          cacClass, animNames, 4, &matchedName2);
                      if (OFF_complexAnimAnimator >= 0) {
                        Log("[OFFSET] ComplexAnimComp.%s = %d (0x%X)",
                            matchedName2, OFF_complexAnimAnimator,
                            OFF_complexAnimAnimator);
                      } else {
                        Log("[WARN] ComplexAnimComp animator not found in "
                            "hierarchy, fallback 0x148");
                        Log("[WARN] ComplexAnimComp full hierarchy:");
                        DumpFieldsHierarchy(cacClass);
                      }
                    }
                  } __except (1) {
                  }
                }

                int caOff = (OFF_complexAnimAnimator >= 0)
                                ? OFF_complexAnimAnimator
                                : 0x148;
                void *animator = *(void **)((char *)complexAnimCom + caOff);
                if (animator) {
                  g_cachedAnimator = animator;
                  Log("[HOOK] Got Animator: %p", animator);
                }
              }
            } __except (1) {
            }
          }

          ClearActiveFootIKSolverWeights();
          ResetFootIKRuntimeState();

          if (orig_SetMainCharacter) {
            orig_SetMainCharacter(self, entity, reason, methodInfo);
          }

          s_ikDisabled = false;
          memset(s_bipedIK, 0, sizeof(s_bipedIK));
          s_bipedIKCount = 0;
          memset(s_grounderIK, 0, sizeof(s_grounderIK));
          s_grounderIKCount = 0;
          memset(s_followDamper, 0, sizeof(s_followDamper));
          s_followDamperCount = 0;
          s_animatorMono = nullptr;
          s_bbcCount = 0;
          s_bbcMethodsResolved = false;
          s_skirtBBCIndex = -1;
          memset(s_bbcInstances, 0, sizeof(s_bbcInstances));

          s_cachedMovementComp = nullptr;
          s_cachedEntity = nullptr;
          s_initialRootCaptured = false;

          g_fingerTransformsResolved = false;
          g_fingerRestCaptured = false;
          memset(g_fingerTransforms, 0, sizeof(g_fingerTransforms));

          g_confirmedSMC = nullptr;
          g_skeletalMorphCore = nullptr;
          g_boneMapReady = false;
          ResetFaceCache();
          ResetSkirtState();
          s_firstFrame = true;
          Log("[SWITCH] Full character state reset for new character: Entity=%p Animator=%p",
              g_mainCharEntity, g_cachedAnimator);
        }
      };

      if (Hook(setMainChar, "PlayerController.SetMainCharacter",
               (void *)SetMainCharHook::Hooked,
               (void **)&orig_SetMainCharacter)) {
        Log("[OK] SetMainCharacter hooked (captures Entity on char switch)");
      }
    } else {
      Log("[WARN] SetMainCharacter not found");
    }
  } else {
    Log("[WARN] PlayerController class NOT found");
  }

  void *smcClass = FindClass("Beyond.Gameplay.View.SkeletalMorph",
                             "SkeletalMorphCore", asms, ac);
  if (!smcClass)
    smcClass = FindClass("Beyond.Gameplay.View", "SkeletalMorphCore", asms, ac);
  if (!smcClass)
    smcClass = FindClass("Beyond.Gameplay.Core", "SkeletalMorphCore", asms, ac);
  if (smcClass) {
    g_skeletalMorphCoreClass = smcClass;
    Log("[FACE] SkeletalMorphCore class found");
    ResolveSMCOffsets(smcClass);

    void *updateMethod = nullptr;
    void *miter = nullptr;
    void *m;
    while ((m = il2cpp_class_get_methods(smcClass, &miter))) {
      const char *mn = il2cpp_method_get_name(m);
      if (mn && strcmp(mn, "Update") == 0 &&
          il2cpp_method_get_param_count(m) == 1) {
        updateMethod = m;
        break;
      }
    }
    if (updateMethod) {
      if (Hook(updateMethod, "SkeletalMorphCore.Update",
               (void *)Hooked_SMCUpdate, (void **)&g_origSMCUpdate)) {
        Log("[FACE] SMCUpdate hooked —face expressions ready");
      }
    } else {
      Log("[FACE] Update method not found in SkeletalMorphCore");
    }

    void *jobMethod = nullptr;
    void *specialJobMethod = nullptr;
    miter = nullptr;
    while ((m = il2cpp_class_get_methods(smcClass, &miter))) {
      const char *mn = il2cpp_method_get_name(m);
      if (!mn)
        continue;
      int pc = il2cpp_method_get_param_count(m);
      if (strcmp(mn, "DoEvaluateMorphToBoneJob") == 0 && pc == 2)
        jobMethod = m;
      if (strcmp(mn, "DoEvaluateSpecialMorphToBoneJob") == 0 && pc == 2)
        specialJobMethod = m;
    }

    if (jobMethod) {
      if (Hook(jobMethod, "DoEvaluateMorphToBoneJob",
               (void *)Hooked_MorphToBoneJob, (void **)&g_origMorphToBoneJob)) {
        Log("[JOB] DoEvaluateMorphToBoneJob hooked");
      }
    } else {
      Log("[JOB] DoEvaluateMorphToBoneJob not found");
    }

    if (specialJobMethod) {
      if (Hook(specialJobMethod, "DoEvaluateSpecialMorphToBoneJob",
               (void *)Hooked_SpecialMorphJob,
               (void **)&g_origSpecialMorphJob)) {
        Log("[JOB] DoEvaluateSpecialMorphToBoneJob hooked");
      }
    }

    Log("[FACE] ApplyBoneToTransJob hook DISABLED (unstable ABI)");
  } else {
    Log("[FACE] SkeletalMorphCore class not found");
  }


  Log("\n=== Phase 2 Init Complete ===");
  Log("Hooks installed. Press Numpad1 in-game to dump bone discovery.");

  DumpCursorMethods();

  CreateThread(NULL, 0, HotkeyThread, NULL, 0, NULL);

  g_guiRunning = true;
  CreateThread(NULL, 0, GuiThread, NULL, 0, NULL);

  CreateThread(NULL, 0, UpdateCheckThread, NULL, 0, NULL);

  return 0;
}
