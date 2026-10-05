#pragma once
static bool DirectVmdSource_ChoosePath(HWND owner, bool save, bool pmx,
                                       char *utf8, size_t capacity) {
  wchar_t path[4096] = {};
  OPENFILENAMEW dialog = {};
  dialog.lStructSize = sizeof(dialog);
  dialog.hwndOwner = owner;
  dialog.lpstrFile = path;
  dialog.nMaxFile = 4096;
  dialog.lpstrFilter = pmx ? L"PMX skeleton (*.pmx)\0*.pmx\0"
                           : L"EIEM source preset "
                             L"(*.eiemsource)\0*.eiemsource\0All files\0*.*\0";
  dialog.lpstrDefExt = pmx ? L"pmx" : L"eiemsource";
  dialog.Flags = OFN_NOCHANGEDIR | OFN_PATHMUSTEXIST |
                 (save ? OFN_OVERWRITEPROMPT : OFN_FILEMUSTEXIST);
  if (!(save ? GetSaveFileNameW(&dialog) : GetOpenFileNameW(&dialog)))
    return false;
  return WideCharToMultiByte(CP_UTF8, WC_ERR_INVALID_CHARS, path, -1, utf8,
                             int(capacity), nullptr, nullptr) > 0;
}
static void DirectVmdSource_DrawGui(HWND owner) {
  if (!ImGui::CollapsingHeader(u8"源姿态与骨骼映射##source-reference"))
    return;
  static char draft[65537] = {}, presetPath[16385] = {};
  static bool initialized = false;
  static uint64_t loadedDraft = 0;
  const auto state = DirectVmdSource_UiSnapshot();
  if (!initialized || loadedDraft != state.draftRevision) {
    strncpy_s(draft, sizeof(draft), state.draft.c_str(), _TRUNCATE);
    initialized = true;
    loadedDraft = state.draftRevision;
  }
  ImGui::Text(u8"当前：%s  (修订 %llu)", state.activeName.c_str(),
              (unsigned long long)state.activeRevision);
  if (state.activePmxPath.empty())
    ImGui::TextWrapped(u8"当前生效源：规范参考（未使用 PMX）");
  else
    ImGui::TextWrapped(u8"当前生效 PMX：%s", state.activePmxPath.c_str());
  ImGui::TextWrapped(u8"默认无需配置。草稿只在停止且恢复完成后应用；暂停时不可"
                     u8"应用。PMX 仅作骨架参考，不读取模型贴图。");
  ImGui::BeginDisabled(state.busy);
  if (ImGui::Button(u8"默认 Tda 草稿")) {
    const auto text = eiem_source::Serialize(eiem_source::Config());
    strncpy_s(draft, sizeof(draft), text.c_str(), _TRUNCATE);
  }
  ImGui::SameLine();
  if (ImGui::Button(u8"选择 PMX 并更新草稿")) {
    char pmxPath[16385] = {};
    if (DirectVmdSource_ChoosePath(owner, false, true, pmxPath, sizeof(pmxPath))) {
      DirectVmdSource_Request(DirectVmdSourceCommand::SetPmxDraft, draft, pmxPath);
      ImGui::EndDisabled();
      ImGui::TextWrapped(u8"正在将 PMX 写入草稿，已有映射会保留。完成后点击校验并应用。");
      return;
    }
  }
  ImGui::TextWrapped(u8"选择 PMX 会直接更新下方草稿；随后点击“校验并应用源配置”。");
  ImGui::TextWrapped(u8"track \"VMD轨道\" \"源骨名\"：第一级别名；map "
                     u8"\"EIEM语义骨\" \"源骨名\"：第二级映射。右侧 \"-\" "
                     u8"表示忽略轨道/不控制该语义骨。省略规则使用自动匹配。");
  ImGui::InputTextMultiline("##source-draft", draft, sizeof(draft),
                            ImVec2(-1, 220));
  ImGui::SetNextItemWidth(-1);
  ImGui::InputText(u8"##source-preset-path", presetPath, sizeof(presetPath));
  if (ImGui::Button(u8"载入预设草稿")) {
    if (DirectVmdSource_ChoosePath(owner, false, false, presetPath,
                                   sizeof(presetPath)))
      DirectVmdSource_Request(DirectVmdSourceCommand::LoadDraft, nullptr,
                              presetPath);
  }
  ImGui::SameLine();
  if (ImGui::Button(u8"另存草稿")) {
    if (DirectVmdSource_ChoosePath(owner, true, false, presetPath,
                                   sizeof(presetPath)))
      DirectVmdSource_Request(DirectVmdSourceCommand::SaveDraft, draft,
                              presetPath);
  }
  ImGui::BeginDisabled(!DirectVmdSource_CanApply());
  if (ImGui::Button(u8"校验并应用源配置"))
    DirectVmdSource_Request(DirectVmdSourceCommand::Apply, draft, nullptr);
  ImGui::EndDisabled();
  ImGui::EndDisabled();
  ImGui::TextWrapped("%s", state.status.c_str());
}
