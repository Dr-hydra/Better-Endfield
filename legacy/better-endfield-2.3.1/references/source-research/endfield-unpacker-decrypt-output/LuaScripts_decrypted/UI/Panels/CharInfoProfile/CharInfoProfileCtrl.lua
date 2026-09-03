local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoProfile
CharInfoProfileCtrl = HL.Class('CharInfoProfileCtrl', uiCtrl.UICtrl)


local GameSettingLanguageAudio = CS.Beyond.GameSetting.GameSettingLanguageAudio

local MESSAGE_STATE_NAME = {
    SIMPLE = "simple",
    COLLAPSED = "collapsed",
    EXPANDED = "expanded",
}

local RECORD_STATE_NAME = {
    LOCKED = "locked",
    COLLAPSED = "collapsed",
    EXPANDED = "expanded",
    FIXED = "fixed",
}

local VOICE_STATE_NAME = {
    LOCKED = "locked",
    NORMAL = "normal",
}

local LOCK_DESC_FORMAT_KEY = {
    [GEnums.CharDocUnlockType.DefaultUnlock] = "LUA_CHAR_PROFILE_LOCK_NONE_FORMAT",
    [GEnums.CharDocUnlockType.ReachLevel] = "LUA_CHAR_PROFILE_LOCK_REACH_LEVEL_FORMAT",
    [GEnums.CharDocUnlockType.ReachBreakStage] = "LUA_CHAR_PROFILE_LOCK_REACH_BREAK_STAGE_FORMAT",
    [GEnums.CharDocUnlockType.ReachPotentialLevel] = "LUA_CHAR_PROFILE_LOCK_REACH_POTENTIAL_FORMAT",
    [GEnums.CharDocUnlockType.ReachFavorability] = "LUA_CHAR_PROFILE_LOCK_REACH_FAVORABILITY_FORMAT",
    [GEnums.CharDocUnlockType.MetCondition] = "LUA_CHAR_PROFILE_LOCK_MET_CONDITION_FORMAT",
}

local TAB_STATE_NAME = {
    [0] = "none",
    [1] = "message",
    [2] = "record",
    [3] = "voice",
}

local TAB_RED_DOT_KEY = {
    [2] = "CharDoc",
    [3] = "CharVoice",
}

local TAB_ANIM_NAME = {
    OPEN = "btnprofiletab_open",
    CLOSE = "btnprofiletab_close",
}

local TAB_COUNT = 3







CharInfoProfileCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE] = '_OnSelectedCharChange',

    [MessageConst.ON_CHAR_FRIENDSHIP_CHANGED] = '_OnCharFriendshipChanged',
}

CharInfoProfileCtrl.m_charTemplateId = HL.Field(HL.String) << ''

CharInfoProfileCtrl.m_charInstId = HL.Field(HL.Number) << -1

CharInfoProfileCtrl.m_charBagSystem = HL.Field(CS.Beyond.Gameplay.CharBagSystem)

CharInfoProfileCtrl.m_watchingFriendship = HL.Field(HL.Boolean) << false

CharInfoProfileCtrl.m_frienshipBindingID = HL.Field(HL.Number) << -1

CharInfoProfileCtrl.m_dungeonId = HL.Field(HL.String) << ""



CharInfoProfileCtrl.m_viewStartTs = HL.Field(HL.Number) << 0

CharInfoProfileCtrl.m_viewEndTs = HL.Field(HL.Number) << 0



CharInfoProfileCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_charTemplateId = args.initCharInfo.templateId
    self.m_charInstId = args.initCharInfo.instId
    self.m_charBagSystem = GameInstance.player.charBag
    self.m_starCellCache = UIUtils.genCellCache(self.view.starCell)

    self:RefreshAll()

    self.view.btnBack.onClick:AddListener(function()
        self:_Return()
    end)

    self:BindInputPlayerAction("char_profile_tab_previous", function()
        self:_ChangeTab(self.m_tabSelectedIndex == 1 and TAB_COUNT or self.m_tabSelectedIndex - 1 )
    end)
    self:BindInputPlayerAction("char_profile_tab_next", function()
        self:_ChangeTab(self.m_tabSelectedIndex % TAB_COUNT + 1)
    end)

    local isEndmin = CharInfoUtils.isEndmin(self.m_charTemplateId)
    if not isEndmin then
        self.m_frienshipBindingID =  self:BindInputPlayerAction("char_profile_friendship", function()
            self:_OpenFriendship()
        end)
    end
    self:BindInputPlayerAction("char_profile_back", function()
        if self.m_watchingFriendship then
            AudioManager.PostEvent("Au_UI_Toast_Common_Small_Close")
            self:_CloseFriendship()
        else
            self:_Return()
        end
    end)

    self.view.dungeonRedDot:InitRedDot("CharInfoDungeon", CS.Beyond.Gameplay.CharUtils.GetVirtualCharTemplateId(self.m_charTemplateId))
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    local pageType = UIConst.CHAR_INFO_PAGE_TYPE.PROFILE
    self.view.textTitle.text = CharInfoUtils.getCharInfoTitle(self.m_charTemplateId, pageType)

    local dungeonId = CS.Beyond.Gameplay.CharUtils.GetVirtualCharTemplateId(self.m_charTemplateId)
    local success, _ = pcall(function()
        self.m_dungeonId = Tables.CharId2DungeonIdTable[dungeonId]
    end)
    if success then
        self.view.messageBtn.onClick:AddListener(function()
            PhaseManager:GoToPhase(PhaseId.DungeonEntry, { dungeonId = self.m_dungeonId })
        end)
    else
        self.m_dungeonId = ""
        self.view.messageBtn.gameObject:SetActive(false)
    end

    self.m_viewStartTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    EventLogManagerInst:GameEvent_CharInfoProfileView(
        self.m_charTemplateId,
       "enter",
        0)
    self:_ProcessStateArg(args.stateArg)
    args.stateArg = nil
end

CharInfoProfileCtrl.OnClose = HL.Override() << function(self)
    self:_SendProfileRead()
    self:_ClearVoice()

    self.m_viewEndTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    local viewTime = math.max(self.m_viewEndTs - self.m_viewStartTs, 0)
    EventLogManagerInst:GameEvent_CharInfoProfileView(
        self.m_charTemplateId,
        "exit",
        viewTime)
end




CharInfoProfileCtrl._OpenFriendship = HL.Method() << function(self)
    if not self.m_watchingFriendship and not self.m_playingVoice then
        self.m_watchingFriendship = true
        self.view.reliabilityTips.gameObject:SetActive(true)
        InputManagerInst:ToggleGroup(self.view.content.groupId, false)
        self.view.messageBtn.gameObject:SetActive(false)
        InputManagerInst:ToggleBinding(self.m_frienshipBindingID,false)
    end
end

CharInfoProfileCtrl._CloseFriendship = HL.Method() << function(self)
    self.m_watchingFriendship = false
    self.view.reliabilityTips.gameObject:SetActive(false)
    InputManagerInst:ToggleGroup(self.view.content.groupId, true)
    if self.m_tabSelectedIndex == 1 then
        self.view.messageBtn.gameObject:SetActive(self.m_dungeonId ~= "")
    end
    InputManagerInst:ToggleBinding(self.m_frienshipBindingID,true)
end

CharInfoProfileCtrl._Return = HL.Method() << function(self)
    if not self.m_phase.m_charItemInitComplete then
        return
    end
    self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW,
    })
    self.view.bgAnimWrapper:PlayOutAnimation()
end
CharInfoProfileCtrl._OnSelectedCharChange = HL.Method(HL.Table) << function(self, charInfo)
    self.m_charTemplateId = charInfo.templateId
    self.m_charInstId = charInfo.instId

    self.view.simpleStateController:SetState(TAB_STATE_NAME[0])
    self:RefreshAll()
end

CharInfoProfileCtrl._OnCharFriendshipChanged = HL.Method() << function(self)
    self:_RefreshFriendship()
end





CharInfoProfileCtrl.RefreshAll = HL.Method() << function(self)
    self:_InitTab()
    self:_RefreshCharData()
    self:_RefreshFriendship()
    self:_RefreshCurrentTab()
end





CharInfoProfileCtrl.m_tabSelectedIndex = HL.Field(HL.Number) << 1

CharInfoProfileCtrl.m_tabRefreshFunction = HL.Field(HL.Table)

CharInfoProfileCtrl._InitTab = HL.Method() << function(self)
    for i = 1, TAB_COUNT do
        local index = i
        local tab = self.view[string.format("tab%02d", index)]
        if TAB_RED_DOT_KEY[i] then
            tab.redDot:InitRedDot(TAB_RED_DOT_KEY[i], self.m_charTemplateId)
        else
            tab.redDot.gameObject:SetActive(false)
        end

        tab.btn.onClick:RemoveAllListeners()
        tab.btn.onClick:AddListener(function()
            self:_ChangeTab(index)
        end)
    end

    self.m_tabRefreshFunction = {

        [1] = "_RefreshMessage",

        [2] = "_RefreshRecord",

        [3] = "_RefreshVoice",
    }
end

CharInfoProfileCtrl._ChangeTab = HL.Method(HL.Number) << function(self,index)
    local tab = self.view[string.format("tab%02d", index)]
    if index == self.m_tabSelectedIndex then
        return
    end
    AudioManager.PostEvent("Au_UI_Toggle_Tab_On")
    self:_CloseFriendship()
    local oldtab = self.view[string.format("tab%02d", self.m_tabSelectedIndex)]
    oldtab.selectNodeAnim:PlayOutAnimation()
    oldtab.normalNode.gameObject:SetActive(true)

    self.m_tabSelectedIndex = index
    tab.selectNode.gameObject:SetActive(true)
    tab.selectNodeAnim:PlayInAnimation()
    tab.normalNode.gameObject:SetActive(false)
    local MESSAGE_TAB_INDEX = 1
    if index == MESSAGE_TAB_INDEX then
        self.view.messageBtn.gameObject:SetActive(self.m_dungeonId ~= "")
    else
        self.view.messageBtn.gameObject:SetActive(false)
    end
    self:_RefreshCurrentTab()
end

CharInfoProfileCtrl._RefreshCurrentTab = HL.Method() << function(self)
    self:_SendProfileRead()
    self:_ClearVoice()
    self.view.simpleStateController:SetState(TAB_STATE_NAME[self.m_tabSelectedIndex])
    local refreshFunction = self.m_tabRefreshFunction[self.m_tabSelectedIndex]
    if refreshFunction then
        self[refreshFunction](self)
    end
end





CharInfoProfileCtrl.m_profile = HL.Field(HL.Table)

CharInfoProfileCtrl.m_charTagDescData = HL.Field(HL.Userdata)






CharInfoProfileCtrl._RefreshCharData = HL.Method() << function(self)
    self.m_profile = CharInfoUtils.getCharInfoProfile(self.m_charTemplateId)
    local profileMessage = {}

    local success, charTagDescData = Tables.characterTagDesTable:TryGetValue(self.m_charTemplateId)
    if success then
        self.m_charTagDescData = charTagDescData
    end

    local hasValue

    local characterTagData
    hasValue, characterTagData = Tables.characterTagTable:TryGetValue(self.m_charTemplateId)
    if hasValue then
        table.insert(profileMessage, self:_GetTagShowData(characterTagData.blocTagId))
        table.insert(profileMessage, self:_GetTagShowData(characterTagData.raceTagId))
        self:_AddTagShowData(characterTagData.expertTagIds, profileMessage)
        self:_AddTagShowData(characterTagData.hobbyTagIds, profileMessage)
    else
        logger.error("没有找到档案配置" .. self.m_charTemplateId)
    end

    self.m_profile.profileMessage = profileMessage
end

CharInfoProfileCtrl._AddTagShowData = HL.Method(HL.Userdata, HL.Table) << function(self, tagIds, profileMessage)
    local count = #tagIds
    for i = 1, count do

        local tagShowData = self:_GetTagShowData(tagIds[CSIndex(i)])
        table.insert(profileMessage, tagShowData)
        if count > 1 then
            local numText = Language["LUA_CHAR_PROFILE_TITLE_NO_" .. i]
            if not numText then
                numText = string.format(" %d", i)
            end
            tagShowData.tageType = string.format("%s%s", tagShowData.tageType, numText)
        end
    end
end

CharInfoProfileCtrl._GetTagShowData = HL.Method(HL.String).Return(HL.Table) << function(self, tagId)

    local tagShowData = {}

    local tagData = Tables.tagDataTable[tagId]

    local tagGroupData = Tables.tagGroupDataTable[tagData.tagGroupId]
    tagShowData.id = tagId
    tagShowData.tageType = tagGroupData.tagGroupName
    tagShowData.tageName = tagData.tagName
    if self.m_charTagDescData then
        local success, tagDescData = self.m_charTagDescData.tagDesc:TryGetValue(tagId)
        if success then
            tagShowData.tageDesc = tagDescData.desc
        end
    end

    return tagShowData
end



CharInfoProfileCtrl._GetLockedDesc = HL.Method(GEnums.CharDocUnlockType, HL.Number).Return(HL.String) << function(self, unlockType, unlockValue)
    if unlockType == GEnums.CharDocUnlockType.ReachFavorability then
        local friendshipLv = CSPlayerDataUtil.GetFriendshipLevel(unlockValue)
        unlockValue = Tables.spaceshipCharRelationLevelTable[friendshipLv].favorDesc
    end
    return string.format(Language[LOCK_DESC_FORMAT_KEY[unlockType]], unlockValue)
end



CharInfoProfileCtrl.m_messageCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoProfileCtrl._RefreshMessage = HL.Method() << function(self)
    if not self.m_messageCellCache then
        self.m_messageCellCache = UIUtils.genCellCache(self.view.messageNode)
    end
    local profileMessage = self.m_profile.profileMessage
    self.m_messageCellCache:Refresh(#profileMessage, function(cell, luaIndex)

        local data = profileMessage[luaIndex]
        cell.textTitle.text = data.tageType
        cell.textDataSimple.text = data.tageName
        cell.textData.text = data.tageName
        cell.textDes.text = data.tageDesc
        local isSimple = string.isEmpty(data.tageDesc)
        local stateName = isSimple and MESSAGE_STATE_NAME.SIMPLE or (DeviceInfo.usingController and MESSAGE_STATE_NAME.EXPANDED or MESSAGE_STATE_NAME.COLLAPSED)
        cell.simpleStateController:SetState(stateName)
        cell.togTitle.isOn = false
        cell.togTitle.gameObject:GetComponent(typeof(CS.Beyond.UI.NonDrawingGraphic)).raycastTarget = not isSimple
        cell.togTitle.onValueChanged:RemoveAllListeners()
        cell.togTitle.onValueChanged:AddListener(function(isOn)
            if isSimple then
                return
            end
            if isOn then
                cell.simpleStateController:SetState(MESSAGE_STATE_NAME.EXPANDED)
                EventLogManagerInst:GameEvent_CharInfoProfileRead(
                    self.m_charTemplateId,
                    "char_info",
                    data.id)
            else
                cell.simpleStateController:SetState(MESSAGE_STATE_NAME.COLLAPSED)
            end
        end)
    end)

    self.view.recordList:ScrollTo(Vector2(0, 0), false)
end





CharInfoProfileCtrl.m_recordCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoProfileCtrl._RefreshRecord = HL.Method() << function(self)
    if not self.m_recordCellCache then
        self.m_recordCellCache = UIUtils.genCellCache(self.view.recordNode)
        self.view.recordList.onValueChanged:AddListener(function()
            self.m_readDocs = self.m_readDocs or {}
            for i = 1, self.m_recordCellCache:GetCount() do
                local data = self.m_profile.profileRecord[CSIndex(i)]
                if data and self.m_charBagSystem:IsCharDocUnread(data.id) and not self.m_readDocs[data.id] then
                    local cell = self.m_recordCellCache:GetItem(i)
                    if self.view.recordList:IsCellViewed(cell.transform) then
                        self.m_readDocs[data.id] = true
                    end
                end
            end
        end)
    end
    local profileRecord = self.m_profile.profileRecord
    self.m_recordCellCache:Refresh(profileRecord.Count, function(cell, luaIndex)

        local data = profileRecord[CSIndex(luaIndex)]
        local isLocked = not self.m_charBagSystem:IsCharDocUnlocked(data.id)

        local stateName = isLocked and RECORD_STATE_NAME.LOCKED or
            (luaIndex <= 2 and RECORD_STATE_NAME.FIXED or
                (DeviceInfo.usingController and RECORD_STATE_NAME.EXPANDED or RECORD_STATE_NAME.COLLAPSED))
        cell.simpleStateController:SetState(stateName)
        if isLocked then
            cell.redDot:Stop()
            cell.textUnlockPrompt.text = self:_GetLockedDesc(data.unlockType, data.unlockValue)
            cell.lockedBtn.onClick:RemoveAllListeners()
            cell.lockedBtn.onClick:AddListener(function()
                Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_PROFILE_DOC_LOCKED)
            end)
        else
            if luaIndex <= 2 then
                cell.redDot:Stop()
                if self.m_charBagSystem:IsCharDocUnread(data.id) then
                    self.m_readDocs = self.m_readDocs or {}
                    self.m_readDocs[data.id] = true
                end
            else
                cell.redDot:InitRedDot("CharDocEntry", data.id)
            end
            cell.textRecordTitle.text = data.recordTitle
            cell.textRecordDetail:SetAndResolveTextStyle(data.recordDesc)
            cell.btnOpen.onClick:RemoveAllListeners()
            cell.btnOpen.onClick:AddListener(function()
                cell.simpleStateController:SetState(RECORD_STATE_NAME.EXPANDED)
                if self.m_charBagSystem:IsCharDocUnread(data.id) then
                    self.m_charBagSystem:SetCharDocRead({ data.id })
                end

                EventLogManagerInst:GameEvent_CharInfoProfileRead(
                    self.m_charTemplateId,
                    "char_profile",
                    data.id)
            end)
            cell.btnPackUp.onClick:RemoveAllListeners()
            cell.btnPackUp.onClick:AddListener(function()
                cell.simpleStateController:SetState(RECORD_STATE_NAME.COLLAPSED)
            end)
        end
    end)

    self.view.recordList:ScrollTo(Vector2(0, 0), true)
end





CharInfoProfileCtrl.m_getVoiceCell = HL.Field(HL.Function)

CharInfoProfileCtrl.m_voiceSelectIndex = HL.Field(HL.Number) << -1

CharInfoProfileCtrl._RefreshVoice = HL.Method() << function(self)
    if not self.m_getVoiceCell then
        self.m_getVoiceCell = UIUtils.genCachedCellFunction(self.view.voiceNode)
        self.view.voiceList.onUpdateCell:AddListener(function(object, csIndex)
            self:_VoiceOnUpdateCell(self.m_getVoiceCell(object), LuaIndex(csIndex))

        end)
    end
    self.m_voiceSelectIndex = -1
    self.view.voiceList:UpdateCount(self.m_profile.profileVoice.Count, true, true)
    local firstCell = self.m_getVoiceCell(self.view.voiceList:Get(0))
    self:SetNaviTarget(firstCell.btnOpenVoice)
end

CharInfoProfileCtrl._VoiceOnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local isSelected = self.m_voiceSelectIndex == luaIndex

    local data = self.m_profile.profileVoice[CSIndex(luaIndex)]
    cell.textNormalTitle.text = data.voiceTitle
    cell.textPlayingTitle.text = data.voiceTitle
    cell.redDot:InitRedDot("CharVoiceEntry", data.id)

    local isLocked = not self.m_charBagSystem:IsCharVoiceUnlocked(data.id)

    local stateName = isLocked and VOICE_STATE_NAME.LOCKED or VOICE_STATE_NAME.NORMAL
    cell.simpleStateController:SetState(stateName)
    cell.isOnNode.gameObject:SetActive(false)
    if isSelected then
        self:_SetVoiceCellIsPlaying(cell, self.m_voiceHandleId > 0)
    end
    cell.btnOpenVoice.onClick:RemoveAllListeners()
    cell.lockNodeBtn.onClick:RemoveAllListeners()

    if isLocked then
        cell.textLockedTitle.text = self:_GetLockedDesc(data.unlockType, data.unlockValue)
        cell.lockNodeBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_PROFILE_VOICE_LOCKED)
        end)
    else
        cell.btnOpenVoice.onClick:AddListener(function()
            self:_ClearVoice()
            self.m_voiceSelectIndex = luaIndex
            self:_PlayVoice()
            if self.m_charBagSystem:IsCharVoiceUnread(data.id) then
                self.m_charBagSystem:SetCharVoiceRead({ data.id })
            end

            EventLogManagerInst:GameEvent_CharInfoProfileRead(
                self.m_charTemplateId,
                "char_voice",
                data.id)
        end)
        if self.m_charBagSystem:IsCharVoiceUnread(data.id) then
            self.m_readVoices = self.m_readVoices or {}
            self.m_readVoices[data.id] = true
        end
    end
end

CharInfoProfileCtrl._SetVoiceCellIsPlaying = HL.Method(HL.Table, HL.Boolean) << function(self, cell, isPlaying)







    UIUtils.PlayAnimationAndToggleActive(cell.isOnNode, isPlaying)
end

CharInfoProfileCtrl.m_stopVoiceTimerId = HL.Field(HL.Number) << -1

CharInfoProfileCtrl.m_voiceHandleId = HL.Field(HL.Number) << -1

CharInfoProfileCtrl.m_playingVoice = HL.Field(HL.Boolean) << false

CharInfoProfileCtrl._PlayVoice = HL.Method() << function(self)
    self.m_playingVoice = true
    local data = self.m_profile.profileVoice[CSIndex(self.m_voiceSelectIndex)]
    local voiceId = data.voId
    local res, duration = VoiceUtils.TryGetVoiceDuration(voiceId)
    if res then

        duration = math.max(duration, self.view.config.VOICE_MIN_DURATION)
        self.m_voiceHandleId = VoiceManager:SpeakNarrative(voiceId, nil, CS.Beyond.Gameplay.Audio.NarrativeVoiceConfig.CHAR_PROFILE_INFO_CONFIG)
        self.m_stopVoiceTimerId = self:_StartTimer(duration, function()
            self:_StopVoice()
        end)

        if self.m_voiceSelectIndex > 0 then
            local object = self.view.voiceList:Get(CSIndex(self.m_voiceSelectIndex))
            if object then
                local cell = self.m_getVoiceCell(object)
                self:_SetVoiceCellIsPlaying(cell, true)
            end
        end

        self.view.voiceInformation.gameObject:SetActive(true)
        self.view.roleInformation.gameObject:SetActive(false)
        self.view.textVoiceInformation.text = data.voiceDesc

        InputManagerInst:ToggleBinding(self.m_frienshipBindingID,false)
    else
        logger.error("no such voiceId: " .. voiceId, "!!!")
    end
end

CharInfoProfileCtrl._ClearVoice = HL.Method() << function(self)
    self:_StopVoice()

    if self.m_voiceSelectIndex > 0 then
        local object = self.view.voiceList:Get(CSIndex(self.m_voiceSelectIndex))
        if object then
            local cell = self.m_getVoiceCell(object)
            cell.simpleStateController:SetState(VOICE_STATE_NAME.NORMAL)
        end
        self.m_voiceSelectIndex = -1
    end

    self.view.voiceInformation.gameObject:SetActive(false)
    self.view.roleInformation.gameObject:SetActive(true)
end

CharInfoProfileCtrl._StopVoice = HL.Method() << function(self)
    self.m_playingVoice = false
    self.view.voiceInformation.gameObject:SetActive(false)
    self.view.roleInformation.gameObject:SetActive(true)

    InputManagerInst:ToggleBinding(self.m_frienshipBindingID,true)

    if self.m_stopVoiceTimerId >= 0 then
        self:_ClearTimer(self.m_stopVoiceTimerId)
        self.m_stopVoiceTimerId = -1
    end

    if self.m_voiceHandleId >= 0 then
        VoiceManager:StopVoice(self.m_voiceHandleId)
        self.m_voiceHandleId = -1
    end

    if self.m_voiceSelectIndex > 0 then
        local object = self.view.voiceList:Get(CSIndex(self.m_voiceSelectIndex))
        if object then
            local cell = self.m_getVoiceCell(object)
            self:_SetVoiceCellIsPlaying(cell, false)
        end
    end
end





CharInfoProfileCtrl.m_starCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoProfileCtrl._RefreshFriendship = HL.Method() << function(self)

    local charCfg = Tables.characterTable[self.m_charTemplateId]
    self.view.charNameTextShadow.text = charCfg.name

    local languageAudio = CS.Beyond.GameSetting.languageAudio
    if languageAudio == GameSettingLanguageAudio.Chinese and charCfg.cvName.ChiCVName ~= "" then
        self.view.cvTxtContent.text = charCfg.cvName.ChiCVName
    elseif languageAudio == GameSettingLanguageAudio.English and charCfg.cvName.EngCVName ~= ""then
        self.view.cvTxtContent.text = charCfg.cvName.EngCVName
    elseif languageAudio == GameSettingLanguageAudio.Japanese and charCfg.cvName.JapCVName ~= "" then
        self.view.cvTxtContent.text = charCfg.cvName.JapCVName
    elseif languageAudio == GameSettingLanguageAudio.Korean and charCfg.cvName.KorCVName ~= "" then
        self.view.cvTxtContent.text = charCfg.cvName.KorCVName
    else
        self.view.cvTxtContent.gameObject:SetActive(false)
        self.view.textCV.gameObject:SetActive(false)
    end

    self.m_starCellCache:Refresh(charCfg.rarity)

    local isEndmin = CharInfoUtils.isEndmin(self.m_charTemplateId)
    self.view.lowerNode.gameObject:SetActive(not isEndmin)
    if isEndmin then
        return
    end
    self.view.friendshipNode:InitFriendshipNode(self.m_charInstId)
    local friendshipLevel = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charTemplateId)
    self.view.friendshipTxt.text = Tables.spaceshipCharRelationLevelTable[friendshipLevel].favorDesc
end





CharInfoProfileCtrl.m_readVoices = HL.Field(HL.Table)

CharInfoProfileCtrl.m_readDocs = HL.Field(HL.Table)

CharInfoProfileCtrl._SendProfileRead = HL.Method() << function(self)
    if self.m_readVoices then
        local voiceIdList = {}
        for voiceId, _ in pairs(self.m_readVoices) do
            table.insert(voiceIdList, voiceId)
        end
        if #voiceIdList > 0 then
            self.m_charBagSystem:SetCharVoiceRead(voiceIdList)
        end
        self.m_readVoices = nil
    end

    if self.m_readDocs then
        local docIdList = {}
        for docId, _ in pairs(self.m_readDocs) do
            table.insert(docIdList, docId)
        end
        if #docIdList > 0 then
            self.m_charBagSystem:SetCharDocRead(docIdList)
        end
        self.m_readDocs = nil
    end
end



CharInfoProfileCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg = {
        tabSelectedIndex = self.m_tabSelectedIndex,
    }
    return arg
end

CharInfoProfileCtrl._ProcessStateArg = HL.Method(HL.Table) << function(self, arg)
    if not arg then
        return
    end
    local tabSelectedIndex = arg.tabSelectedIndex
    if tabSelectedIndex and type(tabSelectedIndex) == "number" then
        self:_ChangeTab(tabSelectedIndex)
    end
end

HL.Commit(CharInfoProfileCtrl)
