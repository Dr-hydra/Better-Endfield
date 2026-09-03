local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.CharFormation
local OVERRIDE_CAMERA_PATH = "OverrideCameras/"
local MODEL_CACHE_TOTAL_NUM = 6
local MODEL_LOADING_TAG = -1
local VIRTUAL_MOUSE_HINT_KEY = "virtual_mouse_hint_edit"

local PHASE_CHAR_FORMATION_GAME_OBJECT = "CharFormation"
PhaseCharFormation = HL.Class('PhaseCharFormation', phaseBase.PhaseBase)
local Panels = { PanelId.CharFormation, }

local CameraIndex = {
    Team = 1,
    TeamEx = 2,
    Single = 3,
    Multi = 4,
    OverrideSingle = 5,
}



local AUDIO_EVENT_CHAR_GLITCH = "Au_UI_Event_CharFormation_Glitch"
local AUDIO_EVENT_CHAR_GLITCH_INTERVAL = 0.1







PhaseCharFormation.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.SQUAD_CHANGE] = { '_OnSquadChange', true },
    [MessageConst.SQUAD_NAME_CHANGE] = { '_OnSquadNameChange', true },
    [MessageConst.ON_CHAR_TEAM_MEMBER_COUNT_CHANGED] = { 'OnSlotLockChanged', true },
    [MessageConst.TEAM_NORMAL_SKILL_CHANGE] = { '_OnCharTeamNormalSkillChange', true },
    [MessageConst.ON_PUT_ON_WEAPON] = { 'OnPutOnWeapon', true },
    [MessageConst.ON_GEM_ATTACH] = { 'OnGemChanged', true },
    [MessageConst.ON_GEM_DETACH] = { 'OnGemChanged', true },
    [MessageConst.ON_WEAPON_ATTACH_GEM_ENHANCE_MAX] = { 'OnGemChanged', true },
    [MessageConst.ON_CHAR_POTENTIAL_UNLOCK] = { 'OnCharPotentialUnlock', true },


    [MessageConst.PRE_LEVEL_START] = { 'OnPreLevelStart', false },
    [MessageConst.ON_SCENE_LOAD_START] = { 'OnSceneLoadStart', false },
    [MessageConst.ON_SWITCH_LANGUAGE] = { '_OnSwitchLanguage', false },
    [MessageConst.ON_CAMPFIRE_OPEN_FORMATION] = { 'OnCampfireOpenFormation', false },


    [MessageConst.ON_CHAR_FORMATION_LIST_MULTI_SELECT] = { 'OnCharListMultiSelect', true },
    [MessageConst.ON_CHAR_FORMATION_LIST_SINGLE_SELECT] = { 'OnCharListSingleSelect', true },


    [MessageConst.ON_CHAR_FORMATION_SELECT_TEAM_CHANGE] = { 'OnCurSelectTeamChange', true },
    [MessageConst.ON_CHAR_FORMATION_TEAM_SET] = { 'OnCharListTeamSet', true },

    [MessageConst.ON_CHAR_FORMATION_ENTER_MULTI_SELECT] = { 'OnEnterMultiSelect', true },
    [MessageConst.ON_CHAR_FORMATION_LIST_CONFIRM] = { 'OnCharListConfirm', true },

    [MessageConst.ON_CHAR_FORMATION_CONFIRM_SINGLE_CHAR] = { 'OnConfirmSingleChar', true },
    [MessageConst.ON_CHAR_FORMATION_UNEQUIP_INDEX] = { 'OnUnEquipIndex', true },

    [MessageConst.ON_CHAR_FORMATION_REFRESH_SLOT] = { 'OnRefreshSlot', true },

    [MessageConst.ON_CHAR_FORMATION_CHANGE_HOVER_INDEX] = { 'OnChangeSlotHoverIndex', true },
    [MessageConst.ON_CHAR_FORMATION_CONFIRM_HOVER] = { 'OnConfirmSelectedSlotHover', true },
    [MessageConst.ON_CHAR_MAX_POTENTIAL_EFFECT_TOGGLED] = { 'OnCharMaxPotentialEffectToggled', true },
}



PhaseCharFormation.m_curTeam = HL.Field(HL.Table)

PhaseCharFormation.m_tmpTeam = HL.Field(HL.Table)

PhaseCharFormation.m_tempMultiSelectCharInfoList = HL.Field(HL.Table)

PhaseCharFormation.m_curTeamIndex = HL.Field(HL.Number) << 1

PhaseCharFormation.m_teamSkillCache = HL.Field(HL.Table)

PhaseCharFormation.m_charFormation = HL.Field(HL.Forward("PhasePanelItem"))

PhaseCharFormation.m_skillTip = HL.Field(HL.Forward("PhasePanelItem"))

PhaseCharFormation.m_sceneObject = HL.Field(HL.Forward("PhaseGameObjectItem"))

PhaseCharFormation.m_singleCharIndex = HL.Field(HL.Number) << -1

PhaseCharFormation.m_navigatedCharIndex = HL.Field(HL.Number) << -1

PhaseCharFormation.m_inited = HL.Field(HL.Boolean) << false

PhaseCharFormation.m_virtualMouseInited = HL.Field(HL.Boolean) << false

PhaseCharFormation.m_clearedKey = HL.Field(HL.Number) << -1

PhaseCharFormation.m_updateModelsCounter = HL.Field(HL.Number) << 0








PhaseCharFormation.m_charModelCache = HL.Field(HL.Table)

PhaseCharFormation.m_charModelInUse = HL.Field(HL.Table)

PhaseCharFormation.m_templateId2LightGroup = HL.Field(HL.Table)

PhaseCharFormation.m_templateId2TrackGroup = HL.Field(HL.Table)

PhaseCharFormation.m_templateId2CameraGroup = HL.Field(HL.Table)

PhaseCharFormation.m_templateId2VolumeGroup = HL.Field(HL.Table)

PhaseCharFormation.m_charVolumeTween = HL.Field(HL.Userdata)

PhaseCharFormation.m_overrideSingleCamera = HL.Field(HL.Userdata)

PhaseCharFormation.m_cacheNum = HL.Field(HL.Number) << 0

PhaseCharFormation.m_tickKey = HL.Field(HL.Number) << -1

PhaseCharFormation.m_waitModels = HL.Field(HL.Table)

PhaseCharFormation.m_suppressModelUpdate = HL.Field(HL.Boolean) << false

PhaseCharFormation.m_preCharInfoList = HL.Field(HL.Table)

PhaseCharFormation.m_lastPostGlitchEventTime = HL.Field(HL.Number) << 0

PhaseCharFormation.m_hideCamCor = HL.Field(HL.Thread)







































PhaseCharFormation._OnInit = HL.Override() << function(self)
    self:_ProcessArgs()
    self.m_curTeam = {}
    self.m_charModelCache = {}
    self.m_charModelInUse = {}
    self.m_templateId2LightGroup = {}
    self.m_templateId2TrackGroup = {}
    self.m_templateId2CameraGroup = {}
    self.m_templateId2VolumeGroup = {}
    self.m_teamSkillCache = {}
    self.m_waitModels = nil

    self.m_tickKey = LuaUpdate:Add("Tick", function(deltaTime)
        self:_TryConsumeWaitModels()
    end)

    PhaseCharFormation.Super._OnInit(self)
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterCinematic()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(false)
end

PhaseCharFormation._OnRefresh = HL.Override() << function(self)
    self:_ProcessArgs()
    self:_BackToTeamSet()
    PhaseCharFormation.Super._OnRefresh(self)
    self.m_charFormation.uiCtrl:InitSelectTeam()
end

PhaseCharFormation.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionBackToTop then
        if not PhaseManager:IsPhaseHaveSceneCamera(anotherPhaseId) then
            CameraManager:EnableUIModelCullingMask(true, "charFormation")
        end
    end

    if transitionType == PhaseConst.EPhaseState.TransitionIn and not fastMode then

        UIManager:PreloadPanelAsset(PanelId.CharFormation, PHASE_ID)
    end
    if not fastMode and (transitionType == PhaseConst.EPhaseState.TransitionIn or transitionType == PhaseConst.EPhaseState.TransitionOut) then
        coroutine.waitCondition(function()
            return true
        end, coroutine.TailTick)


        Notify(MessageConst.PREPARE_BLOCK_GLITCH_TRANSITION)
        coroutine.waitForRenderDone()
        coroutine.step()
    end

    if transitionType == PhaseConst.EPhaseState.TransitionBackToTop then
        self.m_hideCamCor = self:_ClearCoroutine(self.m_hideCamCor)
        self.m_sceneObject.view.light.gameObject:SetActive(true)
    end
end


PhaseCharFormation._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if not fastMode then
        Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
    end
    self:_InitGameObject()
    if self.m_sceneObject then
        self.m_sceneObject.go:SetActive(true)
    end
end


PhaseCharFormation._OnActivated = HL.Override() << function(self)

    self:_InitPhaseItems()

    UIManager:Hide(PanelId.Touch)
    self:_PlayTeamVoice()
    self:_HideSkillTip()
    self:_InitControllerSlotSelectedState()

    self.m_hideCamCor = self:_ClearCoroutine(self.m_hideCamCor)
    self.m_sceneObject.view.light.gameObject:SetActive(true)
    CameraManager:EnableUIModelCullingMask(true, "charFormation")
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterCinematic()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(false)
end


PhaseCharFormation._OnDeActivated = HL.Override() << function(self)
    self:_HideSkillTip()
end


PhaseCharFormation._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if not fastMode then
        Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
    end
end


PhaseCharFormation._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if PhaseManager:IsPhaseHaveSceneCamera(args and args.anotherPhaseId) then
        CameraManager:EnableUIModelCullingMask(false, "charFormation")
    end

    self.m_hideCamCor = self:_ClearCoroutine(self.m_hideCamCor)
    local isRecovering = PhaseManager.isRecovering
    self.m_hideCamCor = self:_StartCoroutine(function()
        if not isRecovering then
            coroutine.wait(1)
        end
        CameraManager:EnableUIModelCullingMask(false, "charFormation")
        self.m_sceneObject.view.light.gameObject:SetActive(false)
    end)
end

PhaseCharFormation._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    CameraManager:EnableUIModelCullingMask(true, "charFormation")
end

PhaseCharFormation._OnDestroy = HL.Override() << function(self)
    CameraManager:EnableUIModelCullingMask(false, "charFormation")
    if self.m_tickKey > 0 then
        LuaUpdate:Remove(self.m_tickKey)
        self.m_tickKey = -1
    end

    self:_ClearUIComp()


    UIManager:Show(PanelId.Touch)
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterNormal()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(true)


    GameInstance.player.charBag:ClearAllClientCharAndItemData()
    AudioAdapter.PostEvent("au_ui_close_team_panel")
end

PhaseCharFormation.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    arg.teamIndex = self.m_curTeamIndex
    arg.charListState = nil
    arg.commonPopUpArg = nil
    if self.m_charFormation then
        local uiCtrl = self.m_charFormation.uiCtrl
        if uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
            local selectedCharInfo = self.m_curTeam[self.m_singleCharIndex]
            arg.charListState = {
                state = uiCtrl.state,
                mode = uiCtrl.m_charListMode,
                singleCharIndex = self.m_singleCharIndex,
                selectedCharInstId = selectedCharInfo and selectedCharInfo.charInstId or nil,
                sortFilterState = uiCtrl:CollectCharListSortFilterState(),
            }
        elseif uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.CharChange then
            local selectedCharInstIds = {}
            for _, charItem in ipairs(self.m_tmpTeam or {}) do
                table.insert(selectedCharInstIds, charItem.instId)
            end
            arg.charListState = {
                state = uiCtrl.state,
                mode = uiCtrl.m_charListMode,
                selectedCharInstIds = selectedCharInstIds,
                sortFilterState = uiCtrl:CollectCharListSortFilterState(),
            }
        end
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if isOpen and UIManager:IsShow(PanelId.CommonPopUp) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local commonPopUpArg = ctrl:GetCurPhaseStateArg()
        if commonPopUpArg then
            arg.commonPopUpArg = commonPopUpArg
        end
    end
    return arg
end





PhaseCharFormation._ProcessArgs = HL.Method() << function(self)

    self.arg = self.arg or {}

    if type(self.arg) == "string" then
        local teamConfigId = self.arg
        self.arg = {}
        self.arg.lockedTeamData = self:_GenerateLockedFormationData(teamConfigId)
    end


    local specialTeamType = DungeonUtils.getSpecialTeamType(self.arg.dungeonId)
    if specialTeamType then
        local TeamInfo = GameInstance.player.charBag:GetSpecialTeamByType(specialTeamType)
        self.arg.initCharInstIdList = {}

        if TeamInfo then
            for _, charInstId in pairs(TeamInfo.memberList) do
                table.insert(self.arg.initCharInstIdList, charInstId)
            end
        end

        self.arg.customSetTeamCallback = function(charInstIds)
            GameInstance.player.charBag:SetSpecialTeam(specialTeamType, charInstIds)
        end
    end

    if not string.isEmpty(self.arg.presetTeamId) or self.arg.initCharInstIdList ~= nil then
        self.arg.lockedTeamData = self:_GenerateLockedFormationData(self.arg.presetTeamId)
    elseif not string.isEmpty(self.arg.dungeonId) then
        local hasValue


        local subGameData
        hasValue, subGameData = DataManager.subGameInstDataTable:TryGetValue(self.arg.dungeonId)
        if hasValue and not string.isEmpty(subGameData.teamConfigId) then
            self.arg.lockedTeamData = self:_GenerateLockedFormationData(subGameData.teamConfigId)
        end
    end
    self.m_lockedTeamData = self.arg.lockedTeamData

    self.m_customTeamIndex = -1
    if self.arg.customTeamIndex then
        if self.arg.customTeamIndex < Tables.globalConst.totalSquadNum then
            logger.error(string.format("自定义编队索引非法: %d，小于正常最大编队数量: %d", self.arg.customTeamIndex, Tables.globalConst.totalSquadNum))
        else
            self.m_customTeamIndex = self.arg.customTeamIndex
        end
    end
end

PhaseCharFormation._InitPhaseItems = HL.Method() << function(self)
    self:_InitGameObject()
    if not self.m_inited then
        for _, panelId in pairs(Panels) do
            self:CreatePhasePanelItem(panelId, self.arg)
        end

        self.m_charFormation = self:_GetPanelPhaseItem(PanelId.CharFormation)
        local recoverIndex
        if self.arg and self.arg.teamIndex then
            recoverIndex = self.arg.teamIndex
            self.arg.teamIndex = nil
        end
        local hasCharListState = self.arg and self.arg.charListState ~= nil
        self.m_suppressModelUpdate = hasCharListState
        self.m_charFormation.uiCtrl:InitSelectTeam(recoverIndex)
        self:_TryRecoverCharListState()
        if hasCharListState then
            self.m_suppressModelUpdate = false
            self:_TryConsumeWaitModels()
        end
        if self.arg and self.arg.commonPopUpArg then
            local uiCtrl = self.m_charFormation and self.m_charFormation.uiCtrl
            if uiCtrl then
                uiCtrl:_TryRecoverCommonPopUp(self.arg.commonPopUpArg)
            end
            self.arg.commonPopUpArg = nil
        end
    end
    self.m_inited = true
end

PhaseCharFormation._TryRecoverCharListState = HL.Method() << function(self)
    if not self.arg or not self.arg.charListState or not self.m_charFormation then
        return
    end
    local recoverState = self.arg.charListState
    self.arg.charListState = nil
    if recoverState.state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        local singleCharIndex = recoverState.singleCharIndex
        if type(singleCharIndex) ~= "number" or singleCharIndex <= 0 or singleCharIndex > Const.BATTLE_SQUAD_MAX_CHAR_NUM then
            return
        end
        self:OnEnterSingleChar(singleCharIndex)
        self.m_charFormation.uiCtrl:ApplyCharListSortFilterState(recoverState.sortFilterState or {})
        local selectedCharInstId = recoverState.selectedCharInstId
        local selectedCharInfo = self.m_curTeam[singleCharIndex]
        local curSelectedInstId = selectedCharInfo and selectedCharInfo.charInstId or nil
        if selectedCharInstId and selectedCharInstId ~= curSelectedInstId then
            local cell = self.m_charFormation.uiCtrl.view.charList:GetCellByIndex(singleCharIndex)
            if cell then
                cell:SetSingleSelect(false)
            end
            local charItem = self.m_charFormation.uiCtrl:GetCharListItemByInstId(selectedCharInstId)
            if charItem then
                self.m_charFormation.uiCtrl:SetCharListMode(UIConst.CharListMode.Single, selectedCharInstId)
                self:OnCharListSingleSelect({ 0, charItem })
            end
        end
    elseif recoverState.state == UIConst.UI_CHAR_FORMATION_STATE.CharChange then
        self.m_charFormation.uiCtrl:OpenCharList(recoverState.mode or UIConst.CharListMode.MultiSelect)
        self.m_charFormation.uiCtrl:SetState(UIConst.UI_CHAR_FORMATION_STATE.CharChange)
        self:OnEnterMultiSelect()
        self.m_charFormation.uiCtrl:ApplyCharListSortFilterState(recoverState.sortFilterState or {})
        local charItemList, charInfoList = self.m_charFormation.uiCtrl:RecoverMultiCharListSelection(recoverState.selectedCharInstIds or {})
        self:OnCharListMultiSelect({ charItemList, charInfoList })
    end
end

PhaseCharFormation._InitGameObject = HL.Method() << function(self)
    if not self.m_sceneObject then
        self.m_sceneObject = self:CreatePhaseGOItem(PHASE_CHAR_FORMATION_GAME_OBJECT)
        self:RefreshCameraController(CameraIndex.Team)

        self.m_sceneObject.view.cameraAnimWrapper:ClearTween()
        self.m_sceneObject.view.cameraAnimWrapper:PlayInAnimation()
    end
    self:_InitSlots()
    self:_Refresh3DUIVisible(true)
end





PhaseCharFormation.OnCampfireOpenFormation = HL.StaticMethod(HL.Table) << function(msgArg)
    local arg = {}
    PhaseManager:GoToPhase(PhaseId.CharFormation, arg)
end

PhaseCharFormation.OnCommonBackClicked = HL.Method() << function(self)
    if self.m_charFormation.uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.CharChange or
        self.m_charFormation.uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        self.m_tempMultiSelectCharInfoList = nil
        self:_BackToTeamSet()
    else
        self:CloseSelf()
    end
end

PhaseCharFormation.OnShowSkillTips = HL.Method(HL.Table) << function(self, arg)
    local skillInfo, showTypeAll = unpack(arg)
    if not self.m_skillTip then
        self.m_skillTip = self:CreatePhasePanelItem(PanelId.CharFormationSkillTips, {
            isSkillTipSelectable = showTypeAll,
        })
    end
    self.m_skillTip.uiCtrl:SetTeamIndex(self.m_curTeamIndex)
    self.m_skillTip.uiCtrl:ShowTip(skillInfo, showTypeAll, self.m_teamSkillCache)
end

PhaseCharFormation.OnCharListSingleSelect = HL.Method(HL.Any) << function(self, arg)
    local arg1, arg2  = unpack(arg)

    local cellInfo = arg2
    local curCharItem = self.m_tmpTeam[self.m_singleCharIndex]
    local curInstId = curCharItem and curCharItem.instId or nil
    local instId = cellInfo.instId
    local curSelectedInstId = nil
    if self.m_curTeam and self.m_singleCharIndex and self.m_curTeam[self.m_singleCharIndex] then
        curSelectedInstId = self.m_curTeam[self.m_singleCharIndex].charInstId
    end

    local slotIndex = cellInfo.slotIndex

    local info = {
        charInstId = instId,
        charId = cellInfo.templateId,
        isLocked = cellInfo.isLocked,
        isTrail = cellInfo.isTrail,
        isReplaceable = cellInfo.isReplaceable,
    }

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    local isDead = charInfo.isDead

    local singleState

    if instId == curInstId then
        singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.Current
        if self.m_lockedTeamData and self.m_singleCharIndex <= self.m_lockedTeamData.lockedTeamMemberCount then
            singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.CurrentLocked
        end
    elseif isDead then
        singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherDead

    elseif slotIndex and slotIndex <= Const.BATTLE_SQUAD_MAX_CHAR_NUM then
        if cellInfo.isLocked then
            singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherInTeamLocked
        else
            singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherInTeam
        end
    else
        if self.m_lockedTeamData and self.m_singleCharIndex <= self.m_lockedTeamData.lockedTeamMemberCount and
            (not self.m_lockedTeamData.chars[self.m_singleCharIndex].isReplaceable
            or (self.m_lockedTeamData.chars[self.m_singleCharIndex].isReplaceable and
                cellInfo.templateId ~= self.m_curTeam[self.m_singleCharIndex].charId)) then
            singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherUnavailable
        else
            singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherAvailable
        end
    end

    self.m_charFormation.uiCtrl:RefreshCharInformation(info, self.m_teamSkillCache)
    self.m_charFormation.uiCtrl:RefreshSingleBtns(singleState, info)

    self:UpdateChar(self.m_singleCharIndex, info)
    self:_RefreshEmpty()
    Utils.triggerVoice("chrbark_squad", info.charId)

    local cameraData = {
        cameraIndex = CameraIndex.Single,
        singleIndex = self.m_singleCharIndex,
        charInfo = info,
    }

    self:_RefreshCamera(cameraData)

    self:_HideSkillTip()

    local slot = self.m_sceneObject.view["slot" .. string.format("%d", self.m_singleCharIndex)]
    slot.slotCharDisableFx.gameObject:SetActive(isDead)
end

PhaseCharFormation.OnCharListMultiSelect = HL.Method(HL.Any) << function(self, arg)
    local arg1, arg2 = unpack(arg)

    local charItemList = arg1

    local charInfoList = arg2

    self.m_tmpTeam = charItemList

    self.m_tempMultiSelectCharInfoList = charInfoList

    self:_TryUpdateModels(charInfoList)

    self:_RefreshEmpty()

    for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        local slot = self.m_sceneObject.view["slot" .. string.format("%d", i)]
        local isDead = false
        local charInfo
        local instId = charItemList[i] and charItemList[i].instId or nil
        if instId then
            charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
            isDead = charInfo.isDead
        end
        slot.slotCharDisable.gameObject:SetActive(isDead)
        slot.slotCharDisableFx.gameObject:SetActive(isDead)
        self:_SetActiveCharMark(i, charInfo == nil)
    end
end

PhaseCharFormation._RefreshEmpty = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceEmpty)
    if not self.m_charFormation then
        return
    end
    local empty
    if forceEmpty ~= nil then
        empty = forceEmpty
    else
        empty = self.m_charFormation.uiCtrl:GetCharListEmpty()
    end
    self.m_charFormation.uiCtrl:RefreshEmpty(empty)
end

PhaseCharFormation.OnEnterMultiSelect = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    local cameraData = {
        cameraIndex = CameraIndex.Multi,
    }
    self:_RefreshCamera(cameraData)

    self.m_tmpTeam = self.m_charFormation.uiCtrl:GetCurCharList()
    self:_RefreshEmpty()
    self:_Refresh3DUIVisible(false)
    self:_HideAllControllerSlotSelected()
end

PhaseCharFormation.OnCharListConfirm = HL.Method(HL.Any) << function(self, teamNum)
    local res = self:_TrySendSetSquad()
    if res then
        self:_PlayCharListVoice()
        self:_BackToTeamSet(true)
    end
end

PhaseCharFormation.OnConfirmSingleChar = HL.Method(HL.Any) << function(self, teamNum)
    self.m_tmpTeam = nil
    local res = self:_TrySendSetSquad()
    if res then
        self:_PlaySingleCharVoice()
        self:_BackToTeamSet(true)
    end
end


PhaseCharFormation.OnUnEquipIndex = HL.Method() << function(self)

    if #self.m_tmpTeam == 1 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CAN_NOT_UNEQUIP)
        return
    end
    table.remove(self.m_tmpTeam, self.m_singleCharIndex)

    table.remove(self.m_curTeam, self.m_singleCharIndex)

    local res = self:_TrySendSetSquad()
    if res then
        self:_BackToTeamSet(true)
    end
end

PhaseCharFormation.OnCharListTeamSet = HL.Method(HL.Opt(HL.Boolean)) << function(self, ignoreCheck)
    self:_SendChangeActiveSquad(ignoreCheck)
end


PhaseCharFormation.OnEnterSingleChar = HL.Method(HL.Number) << function(self, index)
    self.m_singleCharIndex = index
    local curIndexInfo = self.m_curTeam[self.m_singleCharIndex]
    local charInstId
    if curIndexInfo then
        charInstId = curIndexInfo.charInstId
        Utils.triggerVoice("chrbark_squad", curIndexInfo.charId)
    end

    self.m_charFormation.uiCtrl:SetSingleCharIndex(index)
    self.m_charFormation.uiCtrl:OpenCharList(UIConst.CharListMode.Single, charInstId)
    self.m_charFormation.uiCtrl:SetState(UIConst.UI_CHAR_FORMATION_STATE.SingleChar)
    self:RefreshSlotVisible(index)

    local cameraData = {
        cameraIndex = CameraIndex.Single,
        singleIndex = index,
        charInfo = curIndexInfo,
    }

    self:_RefreshCamera(cameraData)

    self.m_charFormation.uiCtrl:SetCharListMode(UIConst.CharListMode.Single, charInstId)


    local charInfoList = {}
    charInfoList[self.m_singleCharIndex] = self.m_curTeam[self.m_singleCharIndex]
    self:_TryUpdateModels(charInfoList)

    self.m_tmpTeam = self.m_charFormation.uiCtrl:GetCurCharList()
    self:_RefreshEmpty()
    self:_Refresh3DUIVisible(false)
    self:_HideAllControllerSlotSelected()
end

PhaseCharFormation.OnCurSelectTeamChange = HL.Method(HL.Number) << function(self, teamIndex)
    self.m_curTeamIndex = teamIndex

    local showTeamIndex = self.m_lockedTeamData == nil and self.m_customTeamIndex <= 0
    self.m_sceneObject.view.formationDeco.gameObject:SetActive(showTeamIndex)
    if showTeamIndex then
        self.m_sceneObject.view.formationDeco.textNum.text = string.format("0%d", teamIndex)
        self.m_sceneObject.view.formationDeco.animationWrapper:PlayInAnimation()
    end
    self:_RefreshTeamChars()
    if self.m_skillTip then
        self.m_skillTip.uiCtrl:SetTeamIndex(self.m_curTeamIndex)
    end
end





PhaseCharFormation._CheckCurModelsReady = HL.Method().Return(HL.Boolean) << function(self)
    for _, phaseCharItem in pairs(self.m_charModelInUse) do
        if phaseCharItem and phaseCharItem == -1 then
            return false
        end
    end

    return true
end

PhaseCharFormation._TryUpdateModels = HL.Method(HL.Table) << function(self, charInfoList)
    if self.m_suppressModelUpdate then

        self.m_waitModels = charInfoList
        return
    end
    self.m_updateModelsCounter = self.m_updateModelsCounter + 1
    self.m_waitModels = charInfoList
    self:_TryConsumeWaitModels()
end

PhaseCharFormation._TryConsumeWaitModels = HL.Method() << function(self)

    if not self:_CheckCurModelsReady() then
        return
    end


    if not self.m_waitModels then
        return
    end

    self:_DoRefreshAllModels(self.m_waitModels)
    self.m_waitModels = nil
end

PhaseCharFormation.UpdateChar = HL.Method(HL.Number, HL.Table) << function(self, index, charInfo)

    local charInfoList = {}
    charInfoList[index] = charInfo
    self:_TryUpdateModels(charInfoList)
    local curIndex = nil


    if self.m_charFormation.uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        self.m_curTeam = {}
        for _, char in pairs(self.m_tmpTeam) do

            local newCharInfo = {
                charInstId = char.instId,
                charId = char.templateId,
                isLocked = char.isLocked,
                isTrail = char.isTrail,
                isReplaceable = char.isReplaceable,
            }
            table.insert(self.m_curTeam, newCharInfo)
        end
    end

    for i, info in pairs(self.m_curTeam) do
        if info and info.charId == charInfo.charId and info.charInstId == charInfo.charInstId then
            curIndex = i
        end
    end

    if curIndex then
        self.m_curTeam[curIndex] = nil
    end

    self.m_curTeam[index] = charInfo
end

PhaseCharFormation.UpdateCharList = HL.Method(HL.Table) << function(self, charInfoList)
    self:_TryUpdateModels(charInfoList)
    self.m_curTeam = charInfoList
end

PhaseCharFormation._MoveCharModel2Index = HL.Method(HL.Table, HL.Number) << function(self,
                                                                                     cacheData,
                                                                                     desSlotIndex)
    if cacheData then
        local phaseCharItem = cacheData.phaseCharItem
        local srcSlotIndex = cacheData.slotIndex

        local animStateInt = desSlotIndex
        if self.m_singleCharIndex > 0 and self.m_singleCharIndex == desSlotIndex then
            animStateInt = 0
        end

        if srcSlotIndex ~= desSlotIndex then
            local parent = self.m_sceneObject.view[string.format("charContainer%d", desSlotIndex)]
            phaseCharItem:SetParent(parent)
            phaseCharItem:SetPos(Vector3.zero)
            PhaseCharFormation._UpdateModelName(phaseCharItem, srcSlotIndex, desSlotIndex)
        end

        self:_RefreshPhaseCharItem(phaseCharItem, animStateInt, desSlotIndex)
    end
end

PhaseCharFormation._RefreshPhaseCharItem = HL.Method(HL.Forward("PhaseCharItem"), HL.Number, HL.Number) << function(
    self,phaseCharItem,animStateInt, desSlotIndex)
    local weaponState = animStateInt > 0 and CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.FORCE_SHOW or CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.FORCE_HIDE
    phaseCharItem:SetVisible(true)
    phaseCharItem:SetTrigger(UIConst.PHASE_CHAR_ITEM_ENABLE_SWITCH_FORMATION)
    phaseCharItem:SetInteger(PhaseConst.PHASE_CHAR_ITEM_FORMATION_PARAM_NAME, animStateInt)
    phaseCharItem:ReloadWeapon(false)
    phaseCharItem:SwitchWeaponState(weaponState)

    phaseCharItem.uiModelMono:ForceUpdateAnimator()
    local isModelChanged = self.m_preCharInfoList == nil or self.m_preCharInfoList[desSlotIndex] == nil or
        self.m_preCharInfoList[desSlotIndex].charInstId ~= phaseCharItem.charInstId
    if isModelChanged or animStateInt == 0 then
        self:_PlayModelEffect(desSlotIndex, phaseCharItem.charId)
        if Time.realtimeSinceStartup - self.m_lastPostGlitchEventTime > AUDIO_EVENT_CHAR_GLITCH_INTERVAL then
            self.m_lastPostGlitchEventTime = Time.realtimeSinceStartup
            if self.m_updateModelsCounter > 1 then
                AudioAdapter.PostEvent(AUDIO_EVENT_CHAR_GLITCH)
            end
        end
    end
end

PhaseCharFormation._StopModelEffect = HL.Method(HL.Number) << function(self, index)
    local effect = self.m_sceneObject.view["charEffect" .. string.format("%d", index)]
    effect.gameObject:SetActive(false)
    effect:Stop()
end

PhaseCharFormation._PlayModelEffect = HL.Method(HL.Number, HL.Any) << function(self, index, charId)
    local effectParent
    local parent
    local height
    if index <= 0 or string.isEmpty(charId) or not self.m_sceneObject then
        return
    end
    local charDisplayData = CharInfoUtils.getCharDisplayData(charId)
    if charDisplayData then
        height = LuaIndex(charDisplayData.height:ToInt())
    end
    if self.m_charFormation and self.m_charFormation.uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        effectParent = self.m_sceneObject.view[string.format("effectSingle%d", index)]
    else
        effectParent = self.m_sceneObject.view[string.format("effectList%d", index)]
    end

    effectParent.gameObject:SetActive(true)
    parent = effectParent[string.format("effect%d", height)]

    local effect = self.m_sceneObject.view["charEffect" .. string.format("%d", index)]
    effect.transform:SetParent(parent)
    effect.transform.localPosition = Vector3.zero
    effect.transform.localEulerAngles = Vector3.zero
    effect.transform.localScale = Vector3.one
    effect.gameObject:SetActive(true)
    effect:Play()
end

PhaseCharFormation._DoRefreshAllModels = HL.Method(HL.Table) << function(self, models)
    local curInUse = {}


    for slotIndex, phaseCharItem in pairs(self.m_charModelInUse) do
        local charInstId = phaseCharItem.charInstId
        curInUse[charInstId] = {
            phaseCharItem = phaseCharItem,
            slotIndex = slotIndex,
        }
    end

    self.m_charModelInUse = {}


    for slotIndex, charInfo in pairs(models) do
        local instId = charInfo.charInstId
        local cacheData = curInUse[instId]

        if cacheData then
            self:_MoveCharModel2Index(cacheData, slotIndex)
            curInUse[instId] = nil
            self.m_charModelInUse[slotIndex] = cacheData.phaseCharItem
            self:_SetPreCharInfo(slotIndex, charInfo)
        else
            local animStateInt = slotIndex
            if self.m_singleCharIndex > 0 and self.m_singleCharIndex == slotIndex then
                animStateInt = 0
            end

            self:_TryGetCharModel(charInfo, slotIndex, function(phaseItem)
                if not phaseItem then
                    self.m_charModelInUse[slotIndex] = nil
                    return
                end
                self.m_charModelInUse[slotIndex] = phaseItem
                self:_RefreshPhaseCharItem(phaseItem, animStateInt, slotIndex)
                self:_SetPreCharInfo(slotIndex, charInfo)
            end)
        end
    end


    for charInstId, cacheData in pairs(curInUse) do
        self:_TryCacheCharModel(cacheData)
        if cacheData then
            self:_ClearPreCharInfo(charInstId)
        end
    end

    local lightEnableTemplateId

    if self.m_singleCharIndex > 0 and models[self.m_singleCharIndex] then
        lightEnableTemplateId = models[self.m_singleCharIndex].charId

        local light = self:_TryGetLight(lightEnableTemplateId, self.m_singleCharIndex)
        if light ~= nil then
            light.gameObject:SetActive(true)
            light:TweenLightGroupAlpha(0, 1, 1)
        end
        self.m_sceneObject.view.lightTeam.gameObject:SetActive(false)


        local volume = self:_TryGetVolume(lightEnableTemplateId, self.m_singleCharIndex)
        if volume ~= nil then
            self.m_sceneObject.view.charOverrideVolume.gameObject:SetActive(true)
            local tween = volume:GetMainLightBiasTween(self.m_sceneObject.view.charOverrideVolume, volume.tweenDuration)
            if self.m_charVolumeTween then
                self.m_charVolumeTween:Kill()
            end

            self.m_charVolumeTween = tween
            tween:Play()
        end
    else

        self.m_sceneObject.view.lightTeam.gameObject:SetActive(true)

        self.m_sceneObject.view.charOverrideVolume.gameObject:SetActive(false)
    end

    for templateId, lightGroup in pairs(self.m_templateId2LightGroup) do
        local light = PhaseCharFormation._GetLight(lightGroup)
        if lightEnableTemplateId then
            if lightEnableTemplateId ~= templateId then
                if light.gameObject.activeSelf then
                    light:TweenLightGroupAlpha(1, 0, 1)
                else
                    light.gameObject:SetActive(false)
                end
            end
        else
            light.gameObject:SetActive(false)
        end
    end
end

PhaseCharFormation._GetLight = HL.StaticMethod(HL.Forward("PhaseGameObjectItem")).Return(HL.Any) << function(lightGroup)
    local light
    if lightGroup then
        local lightName = string.format("light_%s", UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.OVERVIEW)
        light = lightGroup.view[lightName]
    end
    return light
end

PhaseCharFormation._GetSingleCamera = HL.StaticMethod(HL.Any).Return(HL.Any) << function(cameraGroup)
    local camera
    if cameraGroup then
        local cameraName = string.format("vcam_%s", UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.FORMATION)
        camera = cameraGroup[cameraName]
    end
    return camera
end

PhaseCharFormation._GetVolume = HL.StaticMethod(HL.Any).Return(HL.Any) << function(volumeGroup)
    local volume
    if volumeGroup then
        local volumeName = string.format("volume_%s", UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.OVERVIEW)
        volume = volumeGroup[volumeName]
    end
    return volume

end

PhaseCharFormation._TryGetLightGroup = HL.Method(HL.String).Return(HL.Forward("PhaseGameObjectItem")) << function(self, templateId)
    local charDisplayData = CharInfoUtils.getCharDisplayData(templateId)
    local lightGroup = self.m_templateId2LightGroup[templateId]
    if not lightGroup and not string.isEmpty(charDisplayData.charInfoLightGroup) then
        local success, result = xpcall(self.CreatePhaseGOItem, debug.traceback, self, charDisplayData.charInfoLightGroup, nil, nil, "CharInfo")
        if success then
            lightGroup = result
            lightGroup.go:SetAllChildrenActiveIfNecessary(false)
            lightGroup.go:SetLayerRecursive(UIConst.UI_MODEL_LAYER)
        end
        self.m_templateId2LightGroup[templateId] = lightGroup
    end
    return lightGroup
end

PhaseCharFormation._TryGetTrackGroup = HL.Method(HL.String).Return(HL.Forward("PhaseGameObjectItem")) << function(self, templateId)
    local charDisplayData = CharInfoUtils.getCharDisplayData(templateId)
    local trackGroup = self.m_templateId2TrackGroup[templateId]
    if not trackGroup and not string.isEmpty(charDisplayData.charInfoCameraGroup) then
        local success, result = xpcall(self.CreatePhaseGOItem, debug.traceback, self, charDisplayData.charInfoCameraGroup, nil, nil, "CharInfo")
        if success then
            trackGroup = result
            trackGroup.go:SetLayerRecursive(UIConst.UI_MODEL_LAYER)
            if UNITY_EDITOR then

                local cinemachineWaypointGroup = trackGroup.go:AddComponent(typeof(CS.Beyond.DevTools.CinemachineWaypointGroup))
                if cinemachineWaypointGroup then
                    cinemachineWaypointGroup:SyncWaypointChange()
                end
            end

            local extraCams = trackGroup.view.extraCams
            extraCams.extra_cam_equip_second.gameObject:SetActive(false)
            extraCams.extra_cam_weapon_second.gameObject:SetActive(false)
        end
        self.m_templateId2TrackGroup[templateId] = trackGroup
    end
    return trackGroup
end

PhaseCharFormation._TryGetCameraGroup = HL.Method(HL.String).Return(HL.Any) << function(self, templateId)
    local cameraGroup = self.m_templateId2CameraGroup[templateId]
    if not cameraGroup then
        local trackGroup = self:_TryGetTrackGroup(templateId)
        if trackGroup then
            cameraGroup = trackGroup.view.cameraGroup
            cameraGroup.gameObject:SetAllChildrenActiveIfNecessary(false)
            self.m_templateId2CameraGroup[templateId] = cameraGroup
        end
    end
    return cameraGroup
end

PhaseCharFormation._TryGetVolumeGroup = HL.Method(HL.String).Return(HL.Any) << function(self, templateId)
    local volumeGroup = self.m_templateId2VolumeGroup[templateId]
    if not volumeGroup then
        local trackGroup = self:_TryGetTrackGroup(templateId)
        if trackGroup then
            volumeGroup = trackGroup.view.volumeModifiers
            self.m_templateId2VolumeGroup[templateId] = volumeGroup
        end
    end
    return volumeGroup
end

PhaseCharFormation._MoveGoToCharContainer = HL.Method(HL.Userdata, HL.Number) << function(self, go, slotIndex)
    local parent = self.m_sceneObject.view[string.format("charContainer%d", slotIndex)]
    go.transform:SetParent(parent.transform)
    go.transform.localPosition = Vector3.zero
    go.transform.localEulerAngles = Vector3.zero
end

PhaseCharFormation._TryGetLight = HL.Method(HL.String, HL.Number).Return(HL.Any) << function(self, templateId, slotIndex)
    local lightGroup = self:_TryGetLightGroup(templateId)

    if lightGroup ~= nil then
        lightGroup:SetActive(true)
        self:_MoveGoToCharContainer(lightGroup.go, slotIndex or 1)
    end

    local light = PhaseCharFormation._GetLight(lightGroup)
    return light
end

PhaseCharFormation._TryGetSingleCamera = HL.Method(HL.String, HL.Number).Return(HL.Any) << function(self, templateId, slotIndex)
    local cameraGroup = self:_TryGetCameraGroup(templateId)

    if cameraGroup ~= nil then
        cameraGroup.gameObject:SetActive(true)
        self:_MoveGoToCharContainer(self:_TryGetTrackGroup(templateId).go, slotIndex or 1)
    end

    local camera = PhaseCharFormation._GetSingleCamera(cameraGroup)
    return camera
end

PhaseCharFormation._TryGetVolume = HL.Method(HL.String, HL.Number).Return(HL.Any) << function(self, templateId, slotIndex)
    local volumeGroup = self:_TryGetVolumeGroup(templateId)

    if volumeGroup ~= nil then
        volumeGroup.gameObject:SetActive(true)
        self:_MoveGoToCharContainer(self:_TryGetTrackGroup(templateId).go, slotIndex or 1)
    end

    local volume = PhaseCharFormation._GetVolume(volumeGroup)
    return volume
end

PhaseCharFormation._TryGetCharModel = HL.Method(HL.Table, HL.Opt(HL.Number, HL.Function)) << function(self,
                                                                                                      data,
                                                                                                      slotIndex,
                                                                                                      callback)
    local charInstId = data.charInstId
    local cacheData = self.m_charModelCache[charInstId]
    if cacheData then
        self.m_charModelCache[charInstId] = nil
        self.m_cacheNum = self.m_cacheNum - 1
        local phaseItem = cacheData.phaseCharItem
        self:_MoveCharModel2Index(cacheData, slotIndex)
        if callback then
            callback(phaseItem)
        end
    else

        self.m_charModelInUse[slotIndex] = MODEL_LOADING_TAG
        self:_SetPreCharInfo(slotIndex, nil)

        local index = slotIndex or 1
        local go = self.m_sceneObject.view[string.format("charContainer%d", index)]
        self:CreatePhaseCharItem(data, go, function(phaseItem)
            if not phaseItem then
                callback(nil)
                return
            end
            PhaseCharFormation._UpdateModelName(phaseItem, -1, index)
            local lightGroup = self:_TryGetLightGroup(data.charId)
            if lightGroup then
                phaseItem.uiModelMono:InitLightFollower(lightGroup.go.transform)
            end
            local lookAtIk = phaseItem.go:GetComponent("LookAtIK")
            if lookAtIk then
                lookAtIk.solver:SetLookAtWeight(0)
            end
            callback(phaseItem)
        end, nil, UIConst.UI_MODEL_LAYER)

    end
end

PhaseCharFormation._UpdateModelName = HL.StaticMethod(HL.Forward("PhaseCharItem"), HL.Number, HL.Number) << function(phaseCharItem, srcIndex, dstIndex)
    local srcName = phaseCharItem:GetName()
    local dstName
    if not srcIndex or srcIndex <= 0 then
        dstName = srcName .. string.format("_slot%d", dstIndex)
    else
        local srcSlot = string.format("_slot%d", srcIndex)
        local dstSlot = string.format("_slot%d", dstIndex)
        dstName = string.gsub(srcName, srcSlot, dstSlot)
    end

    phaseCharItem:SetName(dstName)

end

PhaseCharFormation._TryCacheCharModel = HL.Method(HL.Table) << function(self, cacheData)
    if cacheData then
        local phaseCharItem = cacheData.phaseCharItem
        local charId = phaseCharItem.charId
        if self.m_charModelCache[cacheData.phaseCharItem.charInstId] then
            logger.error("PhaseCharFormation._CacheCharModel Error: "
                .. charId .. ", already in m_charModelCache!!!")
        else
            self:_DoCacheCharModel(cacheData)
        end
    end
end

PhaseCharFormation._DoCacheCharModel = HL.Method(HL.Table) << function(self, cacheData)
    local phaseCharItem = cacheData.phaseCharItem

    if self.m_cacheNum >= MODEL_CACHE_TOTAL_NUM then
        self:RemovePhaseCharItemByInstId(phaseCharItem.charInstId)
        return
    end


    phaseCharItem:SetVisible(false)
    phaseCharItem:SwitchWeaponState(CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE)


    self.m_charModelCache[phaseCharItem.charInstId] = cacheData
    self.m_cacheNum = self.m_cacheNum + 1
end




PhaseCharFormation._GetCharTeamIndex = HL.Method(HL.Int, HL.Table).Return(HL.Number) << function(self, charInstId,
                                                                                                 charList)
    for i = 1, #charList do
        local charInfo = charList[i]
        if charInfo and charInstId == charInfo.charInstId then
            return i
        end
    end

    return -1
end





PhaseCharFormation._SquadToTeamInfo = HL.Method(HL.Userdata).Return(HL.Table) << function(self, squad)

    local teamInfo = {}
    teamInfo.slots = {}

    if squad ~= nil then
        for i = 0, squad.memberList.Count - 1 do
            local charInstId = squad.memberList[i]
            if charInstId > 0 then
                local templateId = CharInfoUtils.getPlayerCharInfoByInstId(charInstId).templateId
                table.insert(teamInfo.slots, {
                    charInstId = charInstId,
                    charId = templateId
                })
            end
        end

        teamInfo.leaderIndex = LuaIndex(squad.leaderIndex)
    end

    return teamInfo
end

PhaseCharFormation._LockedTeamDataToTeamInfo = HL.Method(HL.Table).Return(HL.Table) << function(self, lockedTeamData)

    local teamInfo = {}
    teamInfo.slots = {}
    teamInfo.leaderIndex = 1
    if lockedTeamData then
        for _, char in pairs(lockedTeamData.chars) do

            local charInfo = {
                charInstId = char.charInstId,
                charId = char.charId,
                isLocked = char.isLocked,
                isTrail = char.isTrail,
                isReplaceable = char.isReplaceable,
            }
            table.insert(teamInfo.slots, charInfo)
        end
    end
    return teamInfo
end

PhaseCharFormation._GetCharModelPos = HL.Method().Return(HL.Table) << function(self)
    local table = {}
    for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        local go = self.m_sceneObject.view["charContainer" .. tostring(i)]
        local pos = go.position
        local uiPos, _ = UIUtils.objectPosToUI(pos, self.m_charFormation.uiCtrl.uiCamera)
        table[i] = uiPos
    end

    return table
end

PhaseCharFormation._GetMaxCharTeamMemberCount = HL.Method().Return(HL.Number, HL.String) << function(self)
    if self.m_lockedTeamData then
        return self.m_lockedTeamData.maxTeamMemberCount, Language.LUA_CHAR_TEAM_MEMBER_COUNT_MAX
    else
        return GameInstance.player.charBag.maxCharTeamMemberCount, Language.LUA_CHAR_TEAM_MEMBER_COUNT_LOCKED
    end
end




PhaseCharFormation._RefreshTeamChars = HL.Method() << function(self)

    local teamInfo
    if self.m_lockedTeamData then
        teamInfo = self:_LockedTeamDataToTeamInfo(self.m_lockedTeamData)
    else
        local teamIndex = CSIndex(self.m_curTeamIndex)
        local squad = GameInstance.player.charBag:GetCurrentMainTeamByIndex(teamIndex)
        teamInfo = self:_SquadToTeamInfo(squad)
    end

    if self.m_charFormation then
        self.m_charFormation.uiCtrl:RefreshTeamCharInfo(teamInfo)
    end
    self:UpdateCharList(teamInfo.slots)
end

PhaseCharFormation._SetPreCharInfo = HL.Method(HL.Number, HL.Table) << function(self, slotIndex, charInfo)
    if self.m_charFormation and self.m_charFormation.uiCtrl.state ~= UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        if not self.m_preCharInfoList then
            self.m_preCharInfoList = {}
        end

        if charInfo then
            self:_ClearPreCharInfo(charInfo.charInstId)
        end

        self.m_preCharInfoList[slotIndex] = charInfo
    end
end

PhaseCharFormation._ClearPreCharInfo = HL.Method(HL.Number) << function(self, charInstId)
    if not self.m_preCharInfoList or self.m_charFormation.uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        return
    end
    for index, charInfo in pairs(self.m_preCharInfoList) do
        if charInfo and charInfo.charInstId == charInstId then
            self.m_preCharInfoList[index] = nil
        end
    end
end

PhaseCharFormation._BackToTeamSet = HL.Method(HL.Opt(HL.Boolean)) << function(self, setSquad)
    self.m_teamSkillCache = {}
    self.m_singleCharIndex = 0
    if self.m_curTeamIndex == LuaIndex(GameInstance.player.charBag.curTeamIndex) then
        self.m_charFormation.uiCtrl:SetState(UIConst.UI_CHAR_FORMATION_STATE.TeamHasSet)
    else
        self.m_charFormation.uiCtrl:SetState(UIConst.UI_CHAR_FORMATION_STATE.TeamWaitSet)
    end
    self:RefreshSlotVisible(-1, true)
    self.m_charFormation.uiCtrl:SetSingleCharIndex(0)
    self.m_charFormation.uiCtrl:CloseCharList()

    local cameraData = {
        cameraIndex = CameraIndex.Team,
    }

    self:_RefreshCamera(cameraData)


    if not setSquad then
        self:_RefreshTeamChars()
    else

        local charInfoList = {}
        if self.m_tmpTeam then
            for _, charItem in pairs(self.m_tmpTeam) do

                local info = {
                    charId = charItem.templateId,
                    charInstId = charItem.instId,
                }
                table.insert(charInfoList, info)
            end
        end

        self:_TryUpdateModels(charInfoList)
    end

    self:_HideSkillTip()

    self:_RefreshEmpty(false)
    self.m_tmpTeam = nil
    self:_Refresh3DUIVisible(true)
    self:_RefreshControllerSlotSelectedState(self.m_navigatedCharIndex, true)
end

PhaseCharFormation._HideSkillTip = HL.Method() << function(self)
    if self.m_skillTip then
        self.m_skillTip.uiCtrl:Hide()
    end
end





local CameraNames = {
    [CameraIndex.Team] = "teamCamera",
    [CameraIndex.TeamEx] = "teamExtraCamera",
    [CameraIndex.Single] = "singleCamera",
    [CameraIndex.Multi] = "multiCamera",
}

PhaseCharFormation.RefreshCameraController = HL.Method(HL.Number) << function(self, index)
    local cameraController = self.m_sceneObject.view.cameraController

    for camIndex, cameraName in pairs(CameraNames) do
        local camera = cameraController[cameraName]
        if camera == nil then
            logger.error("PhaseFormation {0} camera is nil ", cameraName)
        else
            camera.gameObject:SetActive(camIndex == index)
        end
    end
    if self.m_overrideSingleCamera then
        self.m_overrideSingleCamera.gameObject:SetActive(index == CameraIndex.OverrideSingle)
    end
end






PhaseCharFormation._RefreshCamera = HL.Method(HL.Table) << function(self, cameraData)
    local cameraIndex = cameraData.cameraIndex
    local charInfo = cameraData.charInfo
    local index = cameraData.singleIndex
    local virtualCamera

    if self.m_overrideSingleCamera then
        self.m_overrideSingleCamera.gameObject:SetActive(false)
    end

    if cameraIndex == CameraIndex.Team then
        if self:_CheckCameraEx() then
            cameraIndex = CameraIndex.TeamEx
        end
    elseif cameraIndex == CameraIndex.Single then
        local overrideSingleCamera = charInfo and self:_TryGetSingleCamera(charInfo.charId, index)
        if overrideSingleCamera then
            self.m_overrideSingleCamera = overrideSingleCamera
            cameraIndex = CameraIndex.OverrideSingle
        else
            local cameras = self.m_sceneObject.view["singleCameras" .. string.format("%d", index)]
            if charInfo then
                local charId = charInfo.charId
                local charDisplayData = CharInfoUtils.getCharDisplayData(charId)
                if charDisplayData then
                    local overridePath = charDisplayData.cameraConfig.charFormationOverride
                    if string.isEmpty(overridePath) then
                        virtualCamera = cameras["camera" .. string.format("%d", LuaIndex(charDisplayData.height:ToInt()))]
                    else
                        virtualCamera = self:_GetOverrideCamera(overridePath, cameras.transform)
                    end
                else
                    logger.error("PhaseCharFormation._RefreshCamera getCharDisplayData nil: " .. charId .. "!!!")
                end
            else
                virtualCamera = cameras["camera" .. string.format("%d", 1)]
            end
        end
    end

    if virtualCamera then
        local singleCamera = self.m_sceneObject.view.cameraController[CameraNames[CameraIndex.Single]]
        local transform = virtualCamera.transform
        local fov = virtualCamera.m_Lens.FieldOfView

        singleCamera:SetFieldOfView(fov)
        singleCamera.transform.position = transform.position
        singleCamera.transform.rotation = transform.rotation
    end

    self:RefreshCameraController(cameraIndex)
end

PhaseCharFormation._GetOverrideCamera = HL.Method(HL.String, Transform).Return(HL.Userdata) << function(self, name,
                                                                                                        parent)
    local path = OVERRIDE_CAMERA_PATH .. name
    local phaseItem = self:_GetGOPhaseItem(path)
    if not phaseItem then
        phaseItem = self:CreatePhaseGOItem(path)
        phaseItem.go:SetLayerRecursive(UIConst.UI_MODEL_LAYER)
        phaseItem.go:SetActive(false)
    end

    phaseItem.go.transform:SetParent(parent)
    return phaseItem.go:GetComponent("CinemachineVirtualCamera")
end

PhaseCharFormation._CheckCameraEx = HL.Method().Return(HL.Boolean) << function(self)
    local check = false
    if self.m_curTeam[1] then
        local charId = self.m_curTeam[1].charId
        local charDisplayData = CharInfoUtils.getCharDisplayData(charId)
        if charDisplayData and charDisplayData.height == Const.CharHeightEnum.Male then
            check = true
        end
    end
    return check
end





PhaseCharFormation._OnSquadChange = HL.Method(HL.Table) << function(self, args)
    local teamIndex = unpack(args)
    local teamIndex = LuaIndex(teamIndex)
    if self.m_curTeamIndex == teamIndex then
        if self.m_charFormation then
            local squad = GameInstance.player.charBag:GetCurrentMainTeamByIndex(CSIndex(teamIndex))
            local teamInfo = self:_SquadToTeamInfo(squad)
            self.m_charFormation.uiCtrl:RefreshTeamCharInfo(teamInfo)
        end

        self:_RefreshTeamChars()
    end
    if teamIndex == LuaIndex(GameInstance.player.charBag.curTeamIndex) then
        FilterUtils.setCharInstIdIndexDirty()
    end
end

PhaseCharFormation.OnSlotLockChanged = HL.Method(HL.Table) << function(self, arg)
end

PhaseCharFormation._OnCharTeamNormalSkillChange = HL.Method(HL.Table) << function(self, arg)
    local csTeamIndex, charInstId, normalSkillId = unpack(arg)
    local teamIndex = LuaIndex(csTeamIndex)
    if self.m_curTeamIndex == teamIndex then
        local templateId = CharInfoUtils.getPlayerCharInfoByInstId(charInstId).templateId
        local slotIndex = CharInfoUtils.checkCharInTeam(charInstId, teamIndex)
        if slotIndex > 0 then
            local slot = self.m_sceneObject.view["slot" .. string.format("%d", slotIndex)]
            self:_SetSlotCharInfo(slot, {
                charInstId = charInstId,
                charId = templateId,
            })
        end
    end
end

PhaseCharFormation.OnPutOnWeapon = HL.Method(HL.Table) << function(self, arg)
    local charInstId, weaponInstId, lastWeaponInstId = unpack(arg)

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if charInfo then
        local phaseCharItem = self:_GetCharPhaseItem(charInstId)
        if phaseCharItem then
            phaseCharItem:ReloadWeapon()
        end
    end


    local weaponInstData = CharInfoUtils.getWeaponInstInfo(lastWeaponInstId)
    local weaponInst = weaponInstData.weaponInst
    local equippedCharServerId = weaponInst.equippedCharServerId
    charInfo = CharInfoUtils.getPlayerCharInfoByInstId(equippedCharServerId)
    if charInfo then
        local phaseCharItem = self:_GetCharPhaseItem(equippedCharServerId)
        if phaseCharItem then
            phaseCharItem:ReloadWeapon()
        end
    end
end

PhaseCharFormation.OnGemChanged = HL.Method(HL.Table) << function(self, arg)
    local weaponInstId, _ = unpack(arg)
    local weaponInstData = CharInfoUtils.getWeaponInstInfo(weaponInstId)
    local weaponInst = weaponInstData.weaponInst
    local equippedCharServerId = weaponInst.equippedCharServerId
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(equippedCharServerId)
    if charInfo then
        local phaseCharItem = self:_GetCharPhaseItem(equippedCharServerId)
        if phaseCharItem then
            phaseCharItem:ReloadWeaponDecoEffect(weaponInstId)
        end
    end
end

PhaseCharFormation.OnCharPotentialUnlock = HL.Method(HL.Table) << function(self, args)
    local charInstId, level = unpack(args)
    local charPhaseItem = self:_GetCharPhaseItem(charInstId)
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if charPhaseItem and charInfo and charInfo.potentialLevel >= UIConst.CHAR_MAX_POTENTIAL then
        charPhaseItem:LoadPotentialEffects()
    end
end

PhaseCharFormation.OnCharMaxPotentialEffectToggled = HL.Method(HL.Table) << function(self, args)
    local charId, isEnabled = unpack(args)
    local charInfo = CharInfoUtils.getPlayerCharInfoByTemplateId(charId, GEnums.CharType.Default)
    if not charInfo then
        return
    end

    local charPhaseItem = self:_GetCharPhaseItem(charInfo.instId)
    if not charPhaseItem then
        return
    end
    if isEnabled then
        if charInfo.potentialLevel >= UIConst.CHAR_MAX_POTENTIAL then
            charPhaseItem:LoadPotentialEffects()
        end
    else
        charPhaseItem:UnloadPotentialEffects()
    end
end

PhaseCharFormation._OnSquadNameChange = HL.Method(HL.Table) << function(self, arg)
    local teamIndex, teamName = unpack(arg)
    if self.m_curTeamIndex == LuaIndex(teamIndex) and self.m_charFormation then
        self.m_charFormation.uiCtrl:RefreshTeamName(teamName)
    end
    Notify(MessageConst.HIDE_POP_UP)
end

PhaseCharFormation._TrySendSetSquad = HL.Method().Return(HL.Boolean, HL.Number) << function(self)
    if self.m_lockedTeamData then
        self:_StartCoroutine(function()
            coroutine.step()
            local chars = self.m_curTeam
            if self.m_tempMultiSelectCharInfoList then
                chars = self.m_tempMultiSelectCharInfoList
                self.m_tempMultiSelectCharInfoList = nil
            end
            self.m_lockedTeamData.chars = {}
            for _, charInfo in pairs(chars) do
                table.insert(self.m_lockedTeamData.chars, charInfo)
            end
            self:_RefreshTeamChars()
            if self.arg.customSetTeamCallback then
                local charInstIds = {}
                for _, charInfo in pairs(self.m_lockedTeamData.chars) do
                    table.insert(charInstIds, charInfo.charInstId)
                end
                self.arg.customSetTeamCallback(charInstIds)
            end
        end)
        return true, 0
    end

    local charInstIds = {}
    local aliveCharInstIds = {}
    local normalSkillIds = {}
    if self.m_tmpTeam then
        for _, charItem in pairs(self.m_tmpTeam) do
            local charInstId = charItem.instId
            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            local normalSkillId = charInfo.normalSkillId
            if not charInfo.isDead then
                table.insert(aliveCharInstIds, charInstId)
            end
            local cacheNormalSkillId = self.m_teamSkillCache[charInstId]
            if cacheNormalSkillId then
                table.insert(normalSkillIds, cacheNormalSkillId)
            else
                table.insert(normalSkillIds, normalSkillId)
            end

            table.insert(charInstIds, charInstId)

        end
    else
        for _, charData in pairs(self.m_curTeam) do
            local charInstId = charData.charInstId
            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            local isDead = charInfo.isDead
            local normalSkillId = charInfo.normalSkillId
            if not isDead then
                table.insert(aliveCharInstIds, charInstId)
            end

            local cacheNormalSkillId = self.m_teamSkillCache[charInstId]
            if cacheNormalSkillId then
                table.insert(normalSkillIds, cacheNormalSkillId)
            else
                table.insert(normalSkillIds, normalSkillId)
            end

            table.insert(charInstIds, charInstId)
        end
    end

    if #aliveCharInstIds == 0 then
        local toast
        if self.m_curTeamIndex == LuaIndex(GameInstance.player.charBag.curTeamIndex) then
            toast = Language.LUA_CAN_NOT_SET_CUR_SQUAD_NO_CHAR
        else
            toast = Language.LUA_CAN_NOT_SET_SQUAD_ALL_DEAD
        end
        Notify(MessageConst.SHOW_TOAST, toast)
        return false, 0
    else
        GameInstance.player.charBag:SetTeamWithSkill(CSIndex(self.m_curTeamIndex), charInstIds, normalSkillIds)
        return true, aliveCharInstIds[1]
    end
end

PhaseCharFormation._CheckTeamCanFight = HL.Method().Return(HL.Boolean, HL.Any) << function(self)
    local toast
    if #self.m_curTeam == 0 then
        toast = Language.LUA_CAN_NOT_SET_CUR_SQUAD_NO_CHAR
        return false, toast
    end

    local hasAlive = false
    toast = Language.LUA_CAN_NOT_SET_CUR_SQUAD_ALL_DEAD
    for _, info in pairs(self.m_curTeam) do
        local charInstId = info.charInstId
        local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
        if not charInfo.isDead then
            hasAlive = true
            toast = nil
            break
        end
    end

    return hasAlive, toast

end

PhaseCharFormation._SendChangeActiveSquad = HL.Method(HL.Opt(HL.Boolean)) << function(self, ignoreCheck)
    if not ignoreCheck then
        local res, toast = self:_CheckTeamCanFight()
        if not res then
            Notify(MessageConst.SHOW_TOAST, toast)
            return
        end
    end
    GameInstance.player.charBag:SendSetActiveTeam(CSIndex(self.m_curTeamIndex))
end

PhaseCharFormation.SetMemberAndActiveTeam = HL.Method() << function(self)
    local res, firstAliveInstId = self:_TrySendSetSquad()
    if res then
        self:_PlayCharListVoice()
        self:_BackToTeamSet(true)
        GameInstance.player.charBag:SendSetActiveTeam(CSIndex(self.m_curTeamIndex), firstAliveInstId)
    end
end




PhaseCharFormation._InitSlots = HL.Method() << function(self)
    for index = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        local slot = self.m_sceneObject.view["slot" .. string.format("%d", index)]
        slot.canvas.worldCamera = CameraManager.mainCamera

        slot.btnSet.clickHintTextId = VIRTUAL_MOUSE_HINT_KEY
        slot.btnAdd.clickHintTextId = VIRTUAL_MOUSE_HINT_KEY

        slot.btnSet.onClick:RemoveAllListeners()
        slot.btnSet.onClick:AddListener(function()
            self:_OnSingleCharClicked(index)
        end)

        slot.btnAdd.onClick:RemoveAllListeners()
        slot.btnAdd.onClick:AddListener(function()
            self:_OnSingleCharClicked(index)
        end)
        slot.gameObject:SetActive(true)
    end
end

PhaseCharFormation._ClearUIComp = HL.Method() << function(self)
    for index = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        local slot = self.m_sceneObject.view["slot" .. string.format("%d", index)]
        CSUtils.ClearUIComponents(slot.gameObject)
    end
end

PhaseCharFormation._Refresh3DUIVisible = HL.Method(HL.Boolean) << function(self, allVisible)

    if self.m_charFormation and self.m_charFormation.uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        allVisible = false
    end
    local maxCharTeamMemberCount = self:_GetMaxCharTeamMemberCount()

    for index = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        local slot = self.m_sceneObject.view["slot" .. string.format("%d", index)]
        local root = slot.root
        local slotEmpty = slot.slotEmpty

        if not allVisible then
            if index > maxCharTeamMemberCount then
                slot.slotEmpty.gameObject:SetActive(false)
                slot.btnSet.gameObject:SetActive(true)
                slot.slotChar.gameObject:SetActive(false)
                slot.slotLock.gameObject:SetActive(true)

            elseif slotEmpty.gameObject.activeSelf then
                slot.slotEmpty:PlayOutAnimation(function()
                    root.gameObject:SetActive(allVisible)
                end)
            else
                root.gameObject:SetActive(allVisible)
            end
        else
            root.gameObject:SetActive(allVisible)
        end
    end

    if self.m_lockedTeamData or self.m_customTeamIndex > 0 then
        self.m_sceneObject.view.formationDeco.gameObject:SetActive(false)
    else
        if not allVisible and self.m_sceneObject.view.formationDeco.gameObject.activeSelf then
            self.m_sceneObject.view.formationDeco.animationWrapper:PlayOutAnimation(function()
                self.m_sceneObject.view.formationDeco.gameObject:SetActive(allVisible)
            end)
        else
            self.m_sceneObject.view.formationDeco.gameObject:SetActive(allVisible)
        end
    end
end

PhaseCharFormation._OnSingleCharClicked = HL.Method(HL.Number) << function(self, index)
    local maxCharTeamMemberCount, toast = self:_GetMaxCharTeamMemberCount()
    if index > maxCharTeamMemberCount then
        Notify(MessageConst.SHOW_TOAST, toast)
        return
    end

    local state = self.m_charFormation.uiCtrl.state
    if state ~= UIConst.UI_CHAR_FORMATION_STATE.CharChange and state ~= UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        self:OnEnterSingleChar(index)
    end
end

PhaseCharFormation.RefreshSlotVisible = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, allVisible)
    for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        local slot = self.m_sceneObject.view["slot" .. string.format("%d", i)]
        local root = slot.root
        local slotEmpty = slot.slotEmpty

        if not allVisible then
            if slotEmpty.gameObject.activeSelf then
                slot.slotEmpty:PlayOutAnimation(function()
                    root.gameObject:SetActive(allVisible)
                end)
            else
                root.gameObject:SetActive(allVisible)
            end
        else
            root.gameObject:SetActive(allVisible)
        end
        slot.slotCharDisable.gameObject:SetActive(false)
    end
end

PhaseCharFormation._SetActiveCharMark = HL.Method(HL.Number, HL.Boolean) << function(self, index, active)
    local charMark = self.m_sceneObject.view[string.format("charMark%d", index)]
    charMark.decoCross.gameObject:SetActive(active)
end

PhaseCharFormation._RefreshSlot = HL.Method(HL.Table, HL.Number, HL.Table) << function(self, slot, index, charInfo)
    local maxCharTeamMemberCount = self:_GetMaxCharTeamMemberCount()
    if self.m_lockedTeamData then
        maxCharTeamMemberCount = self.m_lockedTeamData.maxTeamMemberCount
    end
    if slot then
        slot.isLocked = index > maxCharTeamMemberCount
        if slot.isLocked then
            slot.slotLock.gameObject:SetActive(true)
            slot.slotEmpty.gameObject:SetActive(false)
            slot.slotChar.gameObject:SetActive(false)
            slot.slotCharDisable.gameObject:SetActive(false)
            slot.slotCharDisableFx.gameObject:SetActive(false)
            slot.btnSet.gameObject:SetActive(true)
            return
        end

        local hasChar = charInfo and charInfo.charId ~= ""
        local isDead = false
        if hasChar then
            local csCharInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInfo.charInstId)
            isDead = csCharInfo.isDead
        end

        if hasChar then
            self:_SetSlotCharInfo(slot, charInfo)
        end

        if slot.empty == nil then
            slot.empty = slot.slotEmpty.gameObject.activeSelf
        end

        if slot.empty and hasChar then
            slot.slotEmpty:PlayOutAnimation(function()
                slot.slotEmpty.gameObject:SetActive(false)
            end)
            slot.btnSet.gameObject:SetActive(true)
        elseif not slot.empty and not hasChar then
            slot.slotEmpty.gameObject:SetActive(true)
            slot.slotEmpty:PlayInAnimation()
            slot.btnSet.gameObject:SetActive(false)
        end

        slot.empty = not hasChar

        slot.slotChar.gameObject:SetActive(hasChar)
        slot.slotCharDisable.gameObject:SetActive(isDead)
        slot.slotCharDisableFx.gameObject:SetActive(isDead)
        slot.slotLock.gameObject:SetActive(false)
    end
end

PhaseCharFormation._SetSlotCharInfo = HL.Method(HL.Table, HL.Table) << function(self, slot, info)
    local characterTable = Tables.characterTable
    local templateId = info.charId
    local data = characterTable:GetValue(templateId)
    local instId = info.charInstId

    local charInfo = nil

    if instId and instId > 0 then
        charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    end

    if charInfo then
        slot.textLv.text = string.format("%02d", charInfo.level)
    end

    slot.textName.text = data.name

    slot.charElementIcon:InitCharTypeIcon(data.charTypeId)

    local proSpriteName = CharInfoUtils.getCharProfessionIconName(data.profession, true)
    slot.imagePro:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, proSpriteName)

    local dungeonId = self.arg.dungeonId

    local tacticalItemArgs = {
        itemId = charInfo.tacticalItemId,
        isLocked = info.isTrail,
        isForbidden = (not string.isEmpty(dungeonId) and
            UIUtils.isItemForbidden(dungeonId, charInfo.tacticalItemId)) or self.arg.weekRaidArg ~= nil,
        isClickable = false,
        charTemplateId = templateId,
        charInstId = instId,
    }
    slot.charFormationTacticalItem:InitCharFormationTacticalItem(tacticalItemArgs)


    local isFixed, isTrail = CharInfoUtils.getLockedFormationCharTipsShow(info)
    slot.fixedTips.gameObject:SetActive(isFixed)
    slot.tryoutTips.gameObject:SetActive(isTrail)

    if slot.charId ~= templateId and not string.isEmpty(templateId) then
        slot.slotChar:PlayInAnimation()
    end

    slot.charId = templateId
end

PhaseCharFormation.GetCurNaviSlot = HL.Method().Return(HL.Table) << function(self)
    return self.m_sceneObject.view["slot" .. string.format("%d", self.m_navigatedCharIndex)]
end

PhaseCharFormation.OnRefreshSlot = HL.Method(HL.Table) << function(self, args)
    local arg1, arg2 = unpack(args)

    local slotIndex = arg1

    local charInfo = arg2

    local slot = self.m_sceneObject.view["slot" .. string.format("%d", slotIndex)]
    self:_RefreshSlot(slot, slotIndex, charInfo)
    self:_SetActiveCharMark(slotIndex, charInfo == nil)
end





PhaseCharFormation._PlayTeamVoice = HL.Method() << function(self)
    if not self.m_curTeam or self.m_charFormation.uiCtrl.state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar then
        return
    end
    local memberCount = #self.m_curTeam
    if memberCount == 0 then
        return
    end
    local index = math.random(1, memberCount)
    local charId = self.m_curTeam[index].charId
    Utils.triggerVoice("chrbark_squad", charId)
end


PhaseCharFormation._PlaySingleCharVoice = HL.Method() << function(self)
    local squad = GameInstance.player.charBag:GetCurrentMainTeam()
    local serverTeam = self:_SquadToTeamInfo(squad).slots
    local maxCharTeamMemberCount = self:_GetMaxCharTeamMemberCount()
    local changedChars = {}
    for i = 1, maxCharTeamMemberCount do
        if i > #serverTeam and self.m_curTeam[i] ~= nil then
            table.insert(changedChars, self.m_curTeam[i].charId)
        end
        if i <= #serverTeam and self.m_curTeam[i] ~= nil and
            serverTeam[i].charId ~= self.m_curTeam[i].charId then
            table.insert(changedChars, self.m_curTeam[i].charId)
        end
    end
    if #changedChars > 0 then
        local randomIndex = math.random(1, #changedChars)
        Utils.triggerVoice("chrbark_join", changedChars[randomIndex])
    end
end


PhaseCharFormation._PlayCharListVoice = HL.Method() << function(self)
    local curTeam = {}
    if self.m_tmpTeam then
        for _, charItem in pairs(self.m_tmpTeam) do

            local charInfo = {
                charId = charItem.templateId,
                charInstId = charItem.instId,
            }
            table.insert(curTeam, charInfo)
        end
    else
        curTeam = self.m_curTeam
    end
    local squad = GameInstance.player.charBag:GetCurrentMainTeam()
    local serverTeam = self:_SquadToTeamInfo(squad).slots
    local changedChars = {}
    for i = 1, #curTeam do
        local isChangedChar = true
        for j = 1, #serverTeam do
            if curTeam[i].charId == serverTeam[j].charId then
                isChangedChar = false
            end
        end
        if isChangedChar then
            table.insert(changedChars, curTeam[i].charId)
        end
    end
    if #changedChars > 0 then

        local randomIndex = math.random(1, #changedChars)
        Utils.triggerVoice("chrbark_join", changedChars[randomIndex])
    else

        local randomIndex = math.random(1, #curTeam)
        Utils.triggerVoice("chrbark_join", curTeam[randomIndex].charId)
    end
end



PhaseCharFormation.OnPreLevelStart = HL.StaticMethod() << function()
    PhaseManager:TryCacheGOByName(PHASE_ID, PHASE_CHAR_FORMATION_GAME_OBJECT)
end

PhaseCharFormation.OnSceneLoadStart = HL.StaticMethod() << function()

end

PhaseCharFormation._OnSwitchLanguage = HL.StaticMethod() << function()
    PhaseManager:ReleaseCache(PHASE_ID, PHASE_CHAR_FORMATION_GAME_OBJECT)
    PhaseManager:TryCacheGOByName(PHASE_ID, PHASE_CHAR_FORMATION_GAME_OBJECT)
end





PhaseCharFormation.m_lockedTeamData = HL.Field(HL.Table)

local _GenerateLockedCharInfo  = function(charInstId)
    local csCharInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if csCharInfo then
        return {
            charInstId = charInstId,
            charId = csCharInfo.templateId,
        }
    end
    return nil
end


PhaseCharFormation._GenerateLockedFormationData = HL.Method(HL.Any).Return(HL.Table) << function(self, teamConfigId)

    local lockedTeamData = CharInfoUtils.getLockedFormationData(teamConfigId, true)
    if self.arg.initCharInstIdList then
        lockedTeamData.chars = {}
        if type(self.arg.initCharInstIdList) == "table" then
            for _, charInstId in pairs(self.arg.initCharInstIdList) do
                local charInfo = _GenerateLockedCharInfo(charInstId)
                if charInfo then
                    table.insert(lockedTeamData.chars, charInfo)
                end
            end
        else
            for _, charInstId in cs_pairs(self.arg.initCharInstIdList) do
                local charInfo = _GenerateLockedCharInfo(charInstId)
                if charInfo then
                    table.insert(lockedTeamData.chars, charInfo)
                end
            end
        end
    elseif lockedTeamData.lockedTeamMemberCount < lockedTeamData.maxTeamMemberCount then

        local teamInfo = GameInstance.player.charBag:GetCurrentMainTeam()
        local curTeamMemberCount = lockedTeamData.lockedTeamMemberCount
        local curSquadSlotCSIndex = curTeamMemberCount
        for i = curTeamMemberCount + 1, lockedTeamData.maxTeamMemberCount do
            while curSquadSlotCSIndex < teamInfo.memberList.Count do
                local charInstId = teamInfo.memberList[curSquadSlotCSIndex]
                local csCharInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
                local isInLockedTeam = false
                for _, char in ipairs(lockedTeamData.chars) do
                    if char.charId == csCharInfo.templateId then
                        isInLockedTeam = true
                        break
                    end
                end
                local isDead = csCharInfo and csCharInfo.isDead
                if isDead or isInLockedTeam then
                    curSquadSlotCSIndex  = curSquadSlotCSIndex + 1
                else
                    break
                end
            end

            if curSquadSlotCSIndex >= teamInfo.memberList.Count then
                break
            end

            local charInstId = teamInfo.memberList[curSquadSlotCSIndex]
            local csCharInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            local charInfo = {
                charInstId = charInstId,
                charId = csCharInfo.templateId,
            }
            table.insert(lockedTeamData.chars, charInfo)

            curSquadSlotCSIndex = curSquadSlotCSIndex + 1
        end
    end
    return lockedTeamData
end





PhaseCharFormation.m_customTeamIndex = HL.Field(HL.Number) << -1






PhaseCharFormation._InitControllerSlotSelectedState = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self:_HideAllControllerSlotSelected()
    self.m_navigatedCharIndex = 1
    self:_RefreshControllerSlotSelectedState(self.m_navigatedCharIndex, true)
end

PhaseCharFormation._HideAllControllerSlotSelected = HL.Method() << function(self)
    for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        self:_RefreshControllerSlotSelectedState(i, false)
    end
end

PhaseCharFormation._RefreshControllerSlotSelectedState = HL.Method(HL.Number, HL.Boolean) << function(self, index, isSelected)
    local selectEffect = self.m_sceneObject.view["selectEffect" .. string.format("%d", index)]
    if selectEffect == nil then
        return
    end

    selectEffect.gameObject:SetActive(isSelected)
end

PhaseCharFormation.OnChangeSlotHoverIndex = HL.Method(HL.Boolean) << function(self, isNext)
    self:_RefreshControllerSlotSelectedState(self.m_navigatedCharIndex, false)
    self.m_navigatedCharIndex = isNext and self.m_navigatedCharIndex + 1 or self.m_navigatedCharIndex - 1
    if self.m_navigatedCharIndex > Const.BATTLE_SQUAD_MAX_CHAR_NUM then
        self.m_navigatedCharIndex = 1
    elseif self.m_navigatedCharIndex < 1 then
        self.m_navigatedCharIndex = Const.BATTLE_SQUAD_MAX_CHAR_NUM
    end
    self:_RefreshControllerSlotSelectedState(self.m_navigatedCharIndex, true)
    AudioAdapter.PostEvent("Au_UI_Toggle_CharSelect")
end

PhaseCharFormation.OnConfirmSelectedSlotHover = HL.Method() << function(self)
    self:_OnSingleCharClicked(self.m_navigatedCharIndex)
end



HL.Commit(PhaseCharFormation)