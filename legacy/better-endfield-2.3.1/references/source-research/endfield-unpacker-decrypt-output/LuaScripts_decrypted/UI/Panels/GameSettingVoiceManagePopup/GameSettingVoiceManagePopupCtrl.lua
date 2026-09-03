local GameSetting = CS.Beyond.GameSetting
local GameSettingHelper = CS.Beyond.Gameplay.GameSettingHelper

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GameSettingVoiceManagePopup

GameSettingVoiceManagePopupCtrl = HL.Class('GameSettingVoiceManagePopupCtrl', uiCtrl.UICtrl)

local MB = 1024 * 1024





GameSettingVoiceManagePopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GameSettingVoiceManagePopupCtrl.m_getVoiceCell = HL.Field(HL.Function)

GameSettingVoiceManagePopupCtrl.m_voiceInfos = HL.Field(HL.Table)

GameSettingVoiceManagePopupCtrl.m_selectedVoices = HL.Field(HL.Table)

GameSettingVoiceManagePopupCtrl.m_selectedVoiceCount = HL.Field(HL.Number) << 0


GameSettingVoiceManagePopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.btnDelete.onClick:AddListener(function()
        self:_OnDeleteBtnClicked()
    end)

    self.m_getVoiceCell = UIUtils.genCachedCellFunction(self.view.voiceList)
    self.view.voiceList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateVoiceCell(self.m_getVoiceCell(object), LuaIndex(csIndex))
    end)

    self.m_voiceInfos = {}
    self:_ResetSelectedVoices()

    self:_InitController()
end

GameSettingVoiceManagePopupCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()
end

GameSettingVoiceManagePopupCtrl._UpdateView = HL.Method() << function(self)
    self:_UpdateVoiceInfos()

    local voiceCount = #self.m_voiceInfos
    self.view.voiceList:UpdateCount(voiceCount)
    self:_UpdateSelectingState()

    self:_SetNaviTarget()
end

GameSettingVoiceManagePopupCtrl._UpdateSelectingState = HL.Method() << function(self)
    local isAnySelected = self.m_selectedVoiceCount > 0
    self.view.btnDelete.gameObject:SetActive(isAnySelected)
    self.view.btnNotSelected.gameObject:SetActive(not isAnySelected)
end

GameSettingVoiceManagePopupCtrl._UpdateVoiceInfos = HL.Method() << function(self)
    lume.clear(self.m_voiceInfos)

    local settingTabExists, settingTabData = Tables.settingTabTable:TryGetValue(GameSettingConst.TAB_ID_LANGUAGE)
    if not settingTabExists then
        return
    end
    local settingItemExists, settingItemData = settingTabData.tabItems:TryGetValue(GameSetting.ID_LANGUAGE_AUDIO)
    if not settingItemExists then
        return
    end

    local optionTextList = settingItemData.dropdownOptionTextList
    for i = 0, optionTextList.Count - 1 do
        local languageName = optionTextList[i]
        if string.isEmpty(languageName) then
            break
        end
        local languageAudio = Utils.intToEnum(typeof(GameSetting.GameSettingLanguageAudio), LuaIndex(i))
        local vfsBlockType = GameSettingUtils.ToVFSBlockType(languageAudio)
        local isDownloaded = GameInstance.resPrefManager:IsVoiceResourceReady(vfsBlockType)
        if isDownloaded then

            local resourceSize = GameInstance.resPrefManager:GetResourceSize(vfsBlockType)
            local voiceInfo = {
                languageAudio = languageAudio,
                languageStr = GameSettingHelper.ToLanguageStr(languageAudio),
                languageName = languageName,
                resourceSize = resourceSize,
            }
            table.insert(self.m_voiceInfos, voiceInfo)
        end
    end
end

GameSettingVoiceManagePopupCtrl._SelectVoice = HL.Method(HL.Number, HL.Boolean) << function(self, luaIndex, isOn)
    local voiceInfo = self.m_voiceInfos[luaIndex]
    self.m_selectedVoices[voiceInfo.languageAudio] = isOn
    self.m_selectedVoiceCount = math.max(0, self.m_selectedVoiceCount + (isOn and 1 or -1))
    self:_UpdateSelectingState()
end

GameSettingVoiceManagePopupCtrl._ResetSelectedVoices = HL.Method() << function(self)
    self.m_selectedVoices = {}
    self.m_selectedVoiceCount = 0
end

GameSettingVoiceManagePopupCtrl._DeleteSelectedVoices = HL.Method() << function(self)
    if self.m_selectedVoiceCount <= 0 then
        return
    end


    local deleteLanguageList = {}
    local currentLanguageList = {}
    for i, voiceInfo in ipairs(self.m_voiceInfos) do
        local languageAudio = voiceInfo.languageAudio
        if self.m_selectedVoices[languageAudio] then

            local vfsBlockType = GameSettingUtils.ToVFSBlockType(languageAudio)
            GameInstance.resPrefManager:DeleteVocResources(vfsBlockType)

            table.insert(deleteLanguageList, voiceInfo.languageStr)
        else

            table.insert(currentLanguageList, voiceInfo.languageStr)
        end
    end


    local currentLanguage = GameSettingHelper.ToLanguageStr(GameSetting.languageAudio)
    EventLogManagerInst:GameEvent_VoiceManage(currentLanguage, currentLanguageList, deleteLanguageList)

    self:_ResetSelectedVoices()
    Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_SETTING_VOICE_DELETE_SUCCESS)
    Notify(MessageConst.GAME_SETTING_VOICE_RESOURCE_STATE_CHANGED)
end

GameSettingVoiceManagePopupCtrl._OnDeleteBtnClicked = HL.Method() << function(self)
    if self.m_selectedVoiceCount <= 0 then
        return
    end

    local voiceNames = {}
    for i, voiceInfo in ipairs(self.m_voiceInfos) do
        if self.m_selectedVoices[voiceInfo.languageAudio] then
            table.insert(voiceNames, string.format(Language.LUA_GAME_SETTING_VOICE_NAME, voiceInfo.languageName))
        end
    end
    if #voiceNames == 0 then
        return
    end

    local voiceName = table.concat(voiceNames)
    Notify(MessageConst.SHOW_POP_UP, {
        content = string.format(Language.LUA_GAME_SETTING_VOICE_DELETE_POP_UP_CONTENT, voiceName),
        onConfirm = function()
            self:_DeleteSelectedVoices()
            self:_UpdateView()
        end
    })
end

GameSettingVoiceManagePopupCtrl._OnUpdateVoiceCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local voiceInfo = self.m_voiceInfos[luaIndex]
    local languageAudio = voiceInfo.languageAudio
    local inUse = languageAudio == GameSetting.languageAudio

    cell.toggle.checkIsValueValid = function(isOn)
        if not inUse then
            return true
        end
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_SETTING_VOICE_SELECT_IN_USE)
        return false
    end
    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.onValueChanged:AddListener(function(isOn)
        self:_SelectVoice(luaIndex, isOn)
    end)

    local isOn = self.m_selectedVoices[languageAudio] == true
    cell.toggle:SetIsOnWithoutNotify(isOn)
    if inUse then
        cell.stateCtrl:SetState("InUse")
    else
        cell.stateCtrl:SetState(isOn and "Selected" or "NotSelected")
    end

    cell.voiceTxt.text = voiceInfo.languageName
    cell.voiceSizeTxt.text = string.format("(%.2fMB)", voiceInfo.resourceSize / MB)
end

GameSettingVoiceManagePopupCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GameSettingVoiceManagePopupCtrl._SetNaviTarget = HL.Method() << function(self)
    local focusIndex
    for i, voiceInfo in ipairs(self.m_voiceInfos) do
        local inUse = voiceInfo.languageAudio == GameSetting.languageAudio
        if not inUse then
            focusIndex = i
            break
        end
    end
    if #self.m_voiceInfos > 0 then
        focusIndex = 1
    end
    if focusIndex then
        local cellObject = self.view.voiceList:Get(CSIndex(focusIndex))
        if cellObject then
            local cell = self.m_getVoiceCell(cellObject)
            if cell then
                self:SetNaviTarget(cell.toggle)
            end
        end
    end
end

HL.Commit(GameSettingVoiceManagePopupCtrl)
