local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.CharInfo

local OVERRIDE_CAMERA_PATH = "OverrideCameras/"
PhaseCharInfo = HL.Class('PhaseCharInfo', phaseBase.PhaseBase)
local PHASE_CHAR_INFO_GAME_OBJECT = "CharInfoChar"
local PHASE_CHAR_INFO_CAM_ATTACHMENT = "CharInfoCamAttachment"
local PHASE_CHAR_INFO_POTENTIAL_SCENE = "CharInfoPotential"
local PAGE_TYPE_2_PANEL_ID = {
    [UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW] = {
        PanelId.CharInfo,
    },
    [UIConst.CHAR_INFO_PAGE_TYPE.WEAPON] = {
        PanelId.CharInfo,
        PanelId.CharInfoWeapon,
    },
    [UIConst.CHAR_INFO_PAGE_TYPE.EQUIP] = {
        PanelId.CharInfo,
        PanelId.CharInfoEquipSlot,
        PanelId.CharInfoEquip,
    },
    [UIConst.CHAR_INFO_PAGE_TYPE.TALENT] = {
        PanelId.CharInfoTalentUpgrade,
        PanelId.CharInfoTalent,
    },
    [UIConst.CHAR_INFO_PAGE_TYPE.PROFILE] = {
        PanelId.CharInfoProfile,
    },
    [UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE] = {
        PanelId.CharUpgrade,
    },
    [UIConst.CHAR_INFO_PAGE_TYPE.PROFILE_SHOW] = {
        PanelId.CharInfoProfileShow,
    },
    [UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL] = {
        PanelId.CharInfo,
        PanelId.CharInfoPotential,
    }
}

local PANEL_IN_SCENE = {
    [PanelId.CharInfoTalent] = true,
}


local PAGE_CREATE_IN_ADVANCE = {
    [UIConst.CHAR_INFO_PAGE_TYPE.TALENT] = true,
}

local CLOSE_WHEN_ANIMATION_OUT = {

    [PanelId.CharUpgrade] = true,
    [PanelId.CharInfoFullAttribute] = true,
    [PanelId.CharInfoProfileShow] = true,


    [PanelId.CharInfoEquipSlot] = true,
    [PanelId.CharInfoEquip] = true,
    [PanelId.CharInfoWeapon] = true,
    [PanelId.CharInfoPotential] = true,
    [PanelId.CharInfoProfile] = true,
}

local PHASE_ITEMS = {
    "CharInfoChar",
    "CharInfoCamAttachment",
}

local CAMERA_FORCE_FADE_TAB = {
    [UIConst.CHAR_INFO_PAGE_TYPE.WEAPON] = true,
}

local CHARACTER_ANIMATOR_SKIP_IN_TAB = {
    [UIConst.CHAR_INFO_PAGE_TYPE.WEAPON] = true,
    [UIConst.CHAR_INFO_PAGE_TYPE.EQUIP] = true,
    [UIConst.CHAR_INFO_PAGE_TYPE.TALENT] = true,
    [UIConst.CHAR_INFO_PAGE_TYPE.PROFILE] = true,
    [UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE] = true,
    [UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL] = true,
}

local HIDE_GRID_PAGE_TYPE = {
    [UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE] = true,
    [UIConst.CHAR_INFO_PAGE_TYPE.TALENT] = true,
    [UIConst.CHAR_INFO_PAGE_TYPE.PROFILE] = true,
}


local PANEL_PRELOAD_ORDER = {
    PanelId.CharInfoEquip,
    PanelId.CharInfoWeapon,
    PanelId.CharInfoTalent,
    PanelId.WeaponExhibitUpgrade,
    PanelId.WeaponExhibitGem,
}

local SCENE_POS = Vector3(-300, 300, -300)





PhaseCharInfo.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE] = { 'OnSelectCharChange', true },
    [MessageConst.CHAR_INFO_PAGE_CHANGE] = { 'OnPageChange', true },
    [MessageConst.CHAR_INFO_SHOW_ROTATE_CHAR] = { 'OnCharInfoShowRotateChar', true },
    [MessageConst.CHAR_INFO_SHOW_ZOOMING] = { 'OnCharInfoShowZooming', true },
    [MessageConst.CHAR_INFO_PROFILE_CLOSE] = { 'OnCharInfoProfileClose', true },
    [MessageConst.CHAR_INFO_EQUIP_SECOND_OPEN] = { 'OnCharInfoEquipSecondEnter', true },
    [MessageConst.CHAR_INFO_EQUIP_SECOND_CLOSE] = { 'OnCharInfoEquipSecondClose', true },
    [MessageConst.CHAR_INFO_WEAPON_SECOND_OPEN] = { 'OnCharInfoWeaponSecondEnter', true },
    [MessageConst.CHAR_INFO_WEAPON_SECOND_CLOSE] = { 'OnCharInfoWeaponSecondClose', true },
    [MessageConst.CHAR_INFO_PREVIEW_WEAPON] = { 'OnPreviewWeaponChange', true },
    [MessageConst.CHAR_INFO_BLEND_EXIT] = { '_BlendExitPhase', true },
    [MessageConst.CHAR_TALENT_FOCUS] = { 'OnCharTalentFocus', true },
    [MessageConst.CHAR_TALENT_LEAVE_FOCUS] = { 'OnCharTalentLeaveFocus', true },
    [MessageConst.PRE_LEVEL_START] = { 'OnPreLevelStart', false },
    [MessageConst.ON_SWITCH_LANGUAGE] = { '_OnSwitchLanguage', false },
    [MessageConst.ON_GEM_ATTACH] = { "OnGemAttach", true },
    [MessageConst.ON_GEM_DETACH] = { 'OnGemDetach', true },
    [MessageConst.ON_WEAPON_ATTACH_GEM_ENHANCE_MAX] = { 'OnWeaponAttachGemEnhanceMax', true },
    [MessageConst.ON_PUT_ON_WEAPON] = { 'OnPutOnWeapon', true },
    [MessageConst.ON_WEAPON_REFINE] = { 'OnPutOnWeapon', true },
    [MessageConst.ON_CHAR_LEVEL_UP] = { 'OnCharLevelUp', true },
    [MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE] = { 'ToggleWeaponFocusMode', true },
    [MessageConst.DEBUG_CHAR_INFO_SHOW_CHAR_SP] = { '_DebugShowCharSPMotion', true },
    [MessageConst.ON_WEAPON_REFINE] = { 'OnWeaponRefine', true },
    [MessageConst.ON_CHAR_POTENTIAL_UNLOCK] = { 'OnCharPotentialUnlock', true },
    [MessageConst.ON_CHAR_MAX_POTENTIAL_EFFECT_TOGGLED] = { 'OnCharMaxPotentialEffectToggled', true },
    [MessageConst.ON_PLAYER_CHAR_BAG_ADD_NEW] = { 'OnPlayerCharBagAddNew', false },
}

do

    PhaseCharInfo.m_cachedPanels = HL.Field(HL.Table)

    PhaseCharInfo.m_charInfo = HL.Field(HL.Table)

    PhaseCharInfo.m_charInfoList = HL.Field(HL.Table)

    PhaseCharInfo.m_curPage = HL.Field(HL.Number) << UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

    PhaseCharInfo.m_beforePage = HL.Field(HL.Number) << UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

    PhaseCharInfo.m_skillTip = HL.Field(HL.Forward("PhasePanelItem"))

    PhaseCharInfo.m_charItem = HL.Field(HL.Forward("PhaseCharItem"))

    PhaseCharInfo.m_toggleUIKey = HL.Field(HL.Number) << -1

    PhaseCharInfo.m_isInit = HL.Field(HL.Boolean) << false

    PhaseCharInfo.m_templateId2DollyTrackPathGroup = HL.Field(HL.Table)

    PhaseCharInfo.m_templateId2LightGroup = HL.Field(HL.Table)

    PhaseCharInfo.m_templateId2LightFollowers = HL.Field(HL.Table)

    PhaseCharInfo.m_templateId2VolumeGroup = HL.Field(HL.Table)

    PhaseCharInfo.m_charVolumeTween = HL.Field(HL.Userdata)

    PhaseCharInfo.m_trackDollyTween = HL.Field(HL.Userdata)

    PhaseCharInfo.m_zoomCache = HL.Field(HL.Any)

    PhaseCharInfo.m_curPreviewWeaponInstId = HL.Field(HL.Number) << 0

    PhaseCharInfo.m_lookAtTickKey = HL.Field(HL.Number) << -1

    PhaseCharInfo.m_isBlendExit = HL.Field(HL.Boolean) << false

    PhaseCharInfo.m_lastCamAnimName = HL.Field(HL.String) << ""

    PhaseCharInfo.m_lookAtTween = HL.Field(HL.Userdata)

    PhaseCharInfo.m_uiEffectCor = HL.Field(HL.Thread)

    PhaseCharInfo.m_sceneEffectCor = HL.Field(HL.Thread)

    PhaseCharInfo.m_weaponDecoEffectCor = HL.Field(HL.Thread)

    PhaseCharInfo.m_blendTransitionCor = HL.Field(HL.Thread)

    PhaseCharInfo.m_spMotionCor = HL.Field(HL.Thread)

    PhaseCharInfo.m_spMotionUpdateKey = HL.Field(HL.Any)

    PhaseCharInfo.m_voiceCor = HL.Field(HL.Thread)

    PhaseCharInfo.m_charItemInitComplete = HL.Field(HL.Boolean) << false

    PhaseCharInfo.m_actionsAfterCharItemInitCompleted = HL.Field(HL.Forward("Queue"))

    PhaseCharInfo.m_preloadCor = HL.Field(HL.Thread)

    PhaseCharInfo.m_hideCamCor = HL.Field(HL.Thread)

    PhaseCharInfo.m_camTransitionCache = HL.Field(HL.Table)

    PhaseCharInfo.m_curCamPostfix = HL.Field(HL.String) << ""

    PhaseCharInfo.m_isInitPreview = HL.Field(HL.Boolean) << false
end

PhaseCharInfo._OnInit = HL.Override() << function(self)
    PhaseCharInfo.Super._OnInit(self)
    self.m_actionsAfterCharItemInitCompleted = require_ex("Common/Utils/DataStructure/Queue")()
    if self.arg and self.arg.initCharInfoCreator then
        local initCharTemplateId
        if self.arg.initCharInfo then
            initCharTemplateId = self.arg.initCharInfo.templateId
        end
        self.arg.initCharInfo = self.arg.initCharInfoCreator(initCharTemplateId)
    end
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterCinematic()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(false)
end

PhaseCharInfo.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionBackToTop then
        if not PhaseManager:IsPhaseHaveSceneCamera(anotherPhaseId) then
            CameraManager:EnableUIModelCullingMask(true, "charInfo")
        end
    end

    if transitionType == PhaseConst.EPhaseState.TransitionIn then
        if not fastMode then

            UIManager:PreloadPanelAsset(PanelId.CharInfo, PHASE_ID)
            UIManager:PreloadPanelAsset(PanelId.CharInfoCursor, PHASE_ID)
            UIManager:PreloadPanelAsset(PanelId.CharInfoEmpty, PHASE_ID)
        end
        self.m_hideCamCor = self:_ClearCoroutine(self.m_hideCamCor)
    end
    if not fastMode and (transitionType == PhaseConst.EPhaseState.TransitionIn or transitionType == PhaseConst.EPhaseState.TransitionOut) then
        coroutine.waitCondition(function()
            return true
        end, coroutine.TailTick)



        if transitionType == PhaseConst.EPhaseState.TransitionOut then

            for i, phasePanelItem in pairs(self.m_panel2Item) do
                local wrapper = phasePanelItem.uiCtrl.animationWrapper
                wrapper:ClearTween()
            end
        end

        Notify(MessageConst.PREPARE_BLOCK_GLITCH_TRANSITION)
        coroutine.waitForRenderDone()
        coroutine.step()
    end

    if transitionType == PhaseConst.EPhaseState.TransitionBackToTop then

        self.m_hideCamCor = self:_ClearCoroutine(self.m_hideCamCor)
        self:_ToggleSceneLight(true)

        if self.m_templateId2DollyTrackPathGroup and self.m_charInfo then
            local targetGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]
            targetGroup.go:SetActive(true)
        end
        if self.m_charItem and self.m_charItem.uiModelMono then
            self.m_charItem.uiModelMono:PauseAnimator(false)
        end
        if self.m_charItem and self.m_charItem.animator then
            local overviewIndex = UIConst.PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.OVERVIEW
            local isInOverviewTransition = self.m_charItem.animator:GetInteger(UIConst.PHASE_CHAR_ITEM_TO_INDEX) == overviewIndex
            if isInOverviewTransition then
                self:_SwitchCharacterControllerState(self.m_charItem, overviewIndex, overviewIndex, true)
            end
        end
    end
end

PhaseCharInfo.OnPreLevelStart = HL.StaticMethod() << function()
    PhaseManager:TryCacheGOByName(PHASE_ID, PHASE_CHAR_INFO_GAME_OBJECT)
end

PhaseCharInfo._OnSwitchLanguage = HL.StaticMethod() << function()
    PhaseManager:ReleaseCache(PHASE_ID, PHASE_CHAR_INFO_GAME_OBJECT)
    PhaseManager:TryCacheGOByName(PHASE_ID, PHASE_CHAR_INFO_GAME_OBJECT)
end

PhaseCharInfo._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:ActiveRenderScaleLock(true)


    UIManager:Open(PanelId.CharInfoEmpty)
    if not fastMode then
        Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
    end
end

PhaseCharInfo._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:ActiveRenderScaleLock(true)
    CameraManager:EnableUIModelCullingMask(true, "charInfo")
end

PhaseCharInfo._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if args and args.anotherPhaseId == PhaseId.WorldLevelPopup then
        return
    end

    self:ActiveRenderScaleLock(false)

    self:_ClearAudioCor()

    if self.m_charItem and self.m_charItem.uiModelMono then
        self.m_charItem.uiModelMono:PauseAnimator(true)
    end

    if PhaseManager:IsPhaseHaveSceneCamera(args and args.anotherPhaseId) then
        CameraManager:EnableUIModelCullingMask(false, "charInfo")
    end

    self.m_hideCamCor = self:_ClearCoroutine(self.m_hideCamCor)
    local isRecovering = PhaseManager.isRecovering
    self.m_hideCamCor = self:_StartCoroutine(function()
        if not isRecovering then
            coroutine.wait(1)
        end
        CameraManager:EnableUIModelCullingMask(false, "charInfo")
        self:_CloseAllCamGroup()



        self:_ToggleSceneLight(false)
        Utils.disableCameraDOF()
    end)
end

PhaseCharInfo._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:ActiveRenderScaleLock(false)

    if not fastMode then
        Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
    end
end


PhaseCharInfo._OnActivated = HL.Override() << function(self)
    CameraManager:EnableUIModelCullingMask(true, "charInfo")
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterCinematic()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(false)


    UIManager:Hide(PanelId.Touch)




    local arg = self.arg or {}
    local initCharInfo = arg.initCharInfo or self.m_charInfo or CharInfoUtils.getLeaderCharInfo()
    local pageType = arg.pageType or self.m_curPage or UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW
    local extraArg = arg.extraArg or {}
    local forceSkipIn = arg.forceSkipIn
    if forceSkipIn then

        arg.forceSkipIn = nil
    end
    arg.phase = self

    self:_InitPhase(initCharInfo, pageType, forceSkipIn)

    self:_SetListCameraDOF()
    self:_ToggleSceneLight(true)

    if UIManager:IsOpen(PanelId.CharInfoEmpty) then
        UIManager:Show(PanelId.CharInfoEmpty)
    else
        UIManager:Open(PanelId.CharInfoEmpty)
    end

    if self.m_isBlendExit then
        self.m_isBlendExit = false
        self:_BlendBackPhase()
    end

    if extraArg.slotType then
        Notify(MessageConst.ON_SELECT_SLOT_CHANGE, extraArg.slotType)
        extraArg.slotType = nil
    end


    self:_StartPreloadCor()
end


PhaseCharInfo._OnDeActivated = HL.Override() << function(self)
    self:_ToggleCamAttachment(false)
    self:_ClearAudioCor()
    if self.m_preloadCor then
        self.m_preloadCor = PhaseManager:_ClearCoroutine(self.m_preloadCor)
    end

    if self.m_spMotionUpdateKey then
        LuaUpdate:Remove(self.m_spMotionUpdateKey)
        self.m_spMotionUpdateKey = nil
    end

    Utils.disableCameraDOF()

    UIManager:Hide(PanelId.CharInfoCursor)
    UIManager:Hide(PanelId.CharInfoEmpty)
end

PhaseCharInfo.CreateCharInfoPanel = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, panelId, arg)
    local initPanel = self:CreatePhasePanelItem(panelId, arg)
    self.m_panel2Item[panelId] = initPanel
end

PhaseCharInfo.CloseCharInfoPanel = HL.Method(HL.Number) << function(self, panelId)
    if not self.m_panel2Item[panelId] then
        return
    end
    self:RemovePhasePanelItemById(panelId)
end

PhaseCharInfo._ResetCam = HL.Method() << function(self)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    if not sceneObject then
        return
    end

    sceneObject.view.charInfoWeaponCam.gameObject:SetActive(false)
    sceneObject.view.charInfoProfileCam.gameObject:SetActive(false)
    sceneObject.view.charInfoTalentFocusCam.gameObject:SetActive(false)
    sceneObject.view.charInfoBlendCam.gameObject:SetActive(false)

    self:_ClearShowCam()
end

PhaseCharInfo._OnDestroy = HL.Override() << function(self)
    CameraManager:EnableUIModelCullingMask(false, "charInfo")
    GameInstance.player.charBag:SetPhaseCharInfoInstId(0)
    UIManager:Show(PanelId.Touch)
    if self.m_skillTip then
        self.m_skillTip.uiCtrl:ClearTips()
    end

    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local potentialObject = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    self:_ResetCam()
    self:_CleanupLookAtIK()
    self:_SetActivePotentialItems(false)

    UIManager:Close(PanelId.CharInfoCursor)
    UIManager:Close(PanelId.CharInfoEmpty)

    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterNormal()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(true)
    CSUtils.ClearUIComponents(sceneObject.view.gameObject)
    if potentialObject then
        CSUtils.ClearUIComponents(potentialObject.view.gameObject)
    end

    if self.arg and self.arg.onClose then
        self.arg.onClose()
    end
    if CS.Beyond.BeyondMemoryUtility.IsLowMemoryDevice() then
        for i, panelId in ipairs(PANEL_PRELOAD_ORDER) do
            UIManager:_UpdatePanelAssetLRU(panelId, false)
        end


        if not UIManager:IsOpen(PanelId.CharInfoCursor) then
            UIManager:_UpdatePanelAssetLRU(PanelId.CharInfoCursor, false)
        end
    end
    AudioAdapter.PostEvent("au_ui_close_char_panel")
end

PhaseCharInfo._InitPanels = HL.Method(HL.Table, HL.Number, HL.Opt(HL.Boolean)) << function(self, initCharInfo, pageType, forceSkipIn)
    local stateArg = self.arg and self.arg.stateArg
    local initPanels = PAGE_TYPE_2_PANEL_ID[pageType]
    if initPanels then
        for _, panelId in ipairs(initPanels) do
            local phasePanelItem = self:CreatePhasePanelItem(panelId, {
                initCharInfo = initCharInfo,
                pageType = pageType,
                phase = self,
                forceSkipIn = forceSkipIn,
                stateArg = stateArg,
            })

            self:_ProcessPanelInScene(phasePanelItem)
            self:_TryShowFinal(phasePanelItem, pageType, nil, stateArg)
        end
    end


    self:_ProcessStateArg(stateArg)
    if self.arg and self.arg.stateArg then
        self.arg.stateArg = nil
    end
end

PhaseCharInfo._InitGo = HL.Method(HL.Table, HL.Number) << function(self, charInfo, tabType)
    for _, name in ipairs(PHASE_ITEMS) do
        self:CreatePhaseGOItem(name)
    end

    self.m_templateId2DollyTrackPathGroup = {}
    self.m_templateId2LightGroup = {}
    self.m_templateId2LightFollowers = {}
    self.m_templateId2VolumeGroup = {}
    self.m_charInfo = charInfo
    self.m_beforePage = tabType
    self.m_curPage = tabType

    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    sceneObject.view.transform.position = SCENE_POS
    local weaponDecoNode = sceneObject.view.weaponDecoNode
    weaponDecoNode.gameObject:SetActive(false)
    sceneObject.view.charUpgradeEffect.gameObject:SetActive(false)

    if self.m_curPage == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
        self:_InitPotentialScene()
        self:_SetActivePotentialItems(true)
    end
end

PhaseCharInfo._InitCharacter = HL.Method(HL.Table, HL.Number, HL.Opt(HL.Boolean)) << function(self, charInfo, pageType, forceSkipIn)
    local templateId = charInfo.templateId
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local curWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
    self.m_curPreviewWeaponInstId = curWeaponInstId
    GameInstance.player.charBag:SetPhaseCharInfoInstId(charInfo.instId)

    self:_RefreshCharModel(sceneObject, charInfo, pageType, forceSkipIn)
    self:_RefreshCamGroup(sceneObject, true, templateId, pageType)
    self:_RefreshAddonLight(sceneObject, true, templateId, pageType)
    self:_RefreshWeaponDeco({
        pageType = pageType,
    })
    self:_RefreshProfileShowCam({
        pageType = pageType,
    })
    self:_RefreshVoiceTriggerVo(pageType)

    self:_TriggerCharBarkSwitch(pageType)

    if CharInfoUtils.getPlayerCharInfoByTemplateId(templateId, GEnums.CharType.Default)
        and RedDotManager:GetRedDotState("CharNew", templateId) then
        GameInstance.player.charBag:Send_RemoveCharNewTag(templateId)
    end
end

PhaseCharInfo._InitPhase = HL.Method(HL.Table, HL.Number, HL.Opt(HL.Boolean)) << function(self, initCharInfo, pageType, skipInAnim)
    if self.m_isInit then
        return
    end

    self.m_charInfoList = self:_GenerateCharInfoList(initCharInfo)
    self:_ProcessPreviewData(initCharInfo)
    self:_InitGo(initCharInfo, pageType)
    self:_InitCharacter(initCharInfo, pageType, skipInAnim)
    self:_InitPanels(initCharInfo, pageType, skipInAnim)

    self:_ToggleCamAttachment(pageType == UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW)

    self.m_isInit = true
end

PhaseCharInfo._GenerateCharInfoList = HL.Method(HL.Table).Return(HL.Table) << function(self, initCharInfo)
    local charInfoList
    if initCharInfo.isSingleChar then
        charInfoList = CharInfoUtils.getSingleCharInfoList(initCharInfo.instId)
        local singleCharInfo = charInfoList[1]
        if singleCharInfo then
            singleCharInfo.isShowFixed = initCharInfo.isShowFixed
            singleCharInfo.isShowTrail = initCharInfo.isShowTrail
        end
    elseif initCharInfo.charInstIdList then
        charInfoList = CharInfoUtils.getCharInfoListByInstIdList(initCharInfo.charInstIdList, initCharInfo.isShowPreview)
    else
        local dungeonId = GameInstance.dungeonManager.curDungeonId
        local specialTeamType = DungeonUtils.getSpecialTeamType(dungeonId)
        local isFullLockedTeam, formationData = CharInfoUtils.IsFullLockedTeam()
        if isFullLockedTeam or specialTeamType ~= nil then
            charInfoList = {}

            local curSquad = GameInstance.player.squadManager.curSquad
            for i = 1, curSquad.slots.Count do
                local slot = curSquad.slots[CSIndex(i)]
                local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(slot.charInstId)
                if charInfo then
                    local templateId = charInfo.templateId
                    local charCfg = Tables.characterTable:GetValue(templateId)
                    local item = {
                        instId = charInfo.instId,
                        templateId = templateId,
                        ownTime = charInfo.ownTime,
                        level = charInfo.level,
                        rarity = charCfg.rarity,
                        slotIndex = Const.BATTLE_SQUAD_MAX_CHAR_NUM + 1,
                        slotReverseIndex = -1,
                        sortOrder = charCfg.sortOrder,
                        isShowFixed = true,
                        isShowTrail = false,
                    }
                    table.insert(charInfoList, item)
                end
            end
        else
            if Utils.isInMainScope() then
                local csTeamIndex
                local dungeonId = GameInstance.dungeonManager.curDungeonId
                if DungeonUtils.isDungeonContract(dungeonId) then
                    csTeamIndex = Tables.globalConst.contingencyContractCharTeamIndex
                end
                charInfoList = CharInfoUtils.getCharInfoList(csTeamIndex)
            else
                charInfoList = CharInfoUtils.getCurScopeCharInfoList()
            end
            if formationData then
                for _, charFormationInfo in pairs(formationData.chars) do
                    local isShowFixed, isShowTrail = CharInfoUtils.getLockedFormationCharTipsShow(charFormationInfo)
                    for _, charInfo in pairs(charInfoList) do
                        if charInfo.templateId == charFormationInfo.charId and CharInfoUtils.checkIsCardInTrail(charInfo.instId) then
                            charInfo.isShowTrail = isShowTrail
                            charInfo.isShowFixed = isShowFixed
                        end
                    end
                end
            end
        end
    end

    return charInfoList
end

PhaseCharInfo.OnCommonBackClicked = HL.Method() << function(self)
    self.m_uiEffectCor = PhaseManager:_ClearCoroutine(self.m_uiEffectCor)
    self.m_sceneEffectCor = PhaseManager:_ClearCoroutine(self.m_sceneEffectCor)
    AudioAdapter.PostEvent("Au_UI_Menu_CharInfoPanel_Close")
    self:CloseSelf()
end

PhaseCharInfo._RefreshCharModel = HL.Method(HL.Userdata, HL.Table, HL.Number, HL.Opt(HL.Boolean)) << function(self, sceneObject, initCharInfo, mainControlTab, forceSkipIn)
    self:RemoveAllPhaseCharItems()
    local templateId = initCharInfo.templateId
    local data = {
        charInstId = initCharInfo.instId,
        charId = templateId,
        pos = Vector3.zero,
    }
    local shadowPlane = sceneObject.view.shadowPlane
    local shadowMat = shadowPlane.material
    local charDisplayData = CharInfoUtils.getCharDisplayData(templateId)
    local charHeight = charDisplayData.height
    local charHeightData = CharInfoUtils.getCharHeightData(charHeight)
    local shadowFadeConfig = charHeightData.charInfoShadowFadeConfig
    local skipIn = forceSkipIn == true or CHARACTER_ANIMATOR_SKIP_IN_TAB[mainControlTab]

    self.m_charItemInitComplete = false
    self:_RefreshCharModelAddon(sceneObject, initCharInfo)
    self:CreatePhaseCharItem(data, sceneObject.view.charContainer, function(phaseItem)
        self.m_charItemInitComplete = true
        self.m_charItem = phaseItem

        if shadowMat and shadowFadeConfig then
            shadowMat:SetFloat("_CircleFade", charHeightData.isShadowFadeInCharInfo and 1 or 0)
            shadowMat:SetFloat("_CircleFadeDistance", shadowFadeConfig.circleFadeDistance)
            shadowMat:SetFloat("_CircleFadeSmoothness", shadowFadeConfig.circleFadeSmoothness)
        end

        local fromStateIndex, toStateIndex = self:_GetControllerStateIndex(self.m_curPage, self.m_curPage)
        local weaponState = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE

        phaseItem:ReloadWeapon()
        phaseItem:SwitchWeaponState(weaponState)
        if fromStateIndex and toStateIndex then
            self:_SwitchCharacterControllerState(phaseItem, fromStateIndex, toStateIndex, skipIn)
        end

        phaseItem.uiModelMono:ForceUpdateAnimator()


        local targetLightGroup = self.m_templateId2LightGroup[templateId]
        phaseItem.uiModelMono:InitLightFollower(targetLightGroup.go.transform)
        if forceSkipIn ~= true then
            self:_PlayModelEffect(sceneObject, templateId)
        end

        while self.m_actionsAfterCharItemInitCompleted:Count() > 0 do
            local action = self.m_actionsAfterCharItemInitCompleted:Pop()
            if action then
                action()
            end
        end

        CS.Beyond.Gameplay.Conditions.OnCharInfoModelInitFinish.Trigger()
    end, true, UIConst.UI_MODEL_LAYER)
end

PhaseCharInfo._RefreshCharModelAddon = HL.Method(HL.Userdata, HL.Table) << function(self, sceneObject, initCharInfo)
    local templateId = initCharInfo.templateId
    local charDisplayData = CharInfoUtils.getCharDisplayData(templateId)

    if not self.m_templateId2DollyTrackPathGroup[templateId] and not string.isEmpty(charDisplayData.charInfoCameraGroup) then
        local cameraGroup = self:CreatePhaseGOItem(charDisplayData.charInfoCameraGroup)

        if cameraGroup ~= nil then
            cameraGroup.go.transform:SetParent(sceneObject.view.charContainer)
            cameraGroup.go.transform.localPosition = Vector3.zero
            cameraGroup.go.transform.localEulerAngles = Vector3.zero
            cameraGroup.go:SetLayerRecursive(UIConst.UI_MODEL_LAYER)

            if UNITY_EDITOR then

                local cinemachineWaypointGroup = cameraGroup.go:AddComponent(typeof(CS.Beyond.DevTools.CinemachineWaypointGroup))
                if cinemachineWaypointGroup then
                    cinemachineWaypointGroup:SyncWaypointChange()
                end
            end

            local extraCams = cameraGroup.view.extraCams
            extraCams.extra_cam_equip_second.gameObject:SetActive(false)
            extraCams.extra_cam_weapon_second.gameObject:SetActive(false)

            self.m_templateId2DollyTrackPathGroup[templateId] = cameraGroup
            cameraGroup.go:SetActive(false)
            local volumeGroup = cameraGroup.view.volumeModifiers
            if volumeGroup then
                self.m_templateId2VolumeGroup[templateId] = volumeGroup
            end

            local _, fitAspectRatioByFOV = cameraGroup.view.cameraGroup.vcam_overview.gameObject:TryGetComponent(typeof(CS.Beyond.UI.FitAspectRatioByFOV))
            if fitAspectRatioByFOV and fitAspectRatioByFOV.maxFov <= 0 then
                local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
                fitAspectRatioByFOV.maxFov = sceneObject.view.config.OVERVIEW_CAM_MAX_FOV
            end
        end
    end

    if not self.m_templateId2LightGroup[templateId] and not string.isEmpty(charDisplayData.charInfoLightGroup) then
        local lightGroup = self:CreatePhaseGOItem(charDisplayData.charInfoLightGroup)

        if lightGroup ~= nil then
            if UNITY_EDITOR then

                local additionLightGroup = lightGroup.go:AddComponent(typeof(CS.Beyond.DevTools.AdditionalLightGroup))
            end
            lightGroup.go.transform:SetParent(sceneObject.view.charContainer)
            lightGroup.go.transform.localPosition = Vector3.zero
            lightGroup.go.transform.localEulerAngles = Vector3.zero
            lightGroup.go:SetLayerRecursive(UIConst.UI_MODEL_LAYER)
            self.m_templateId2LightGroup[templateId] = lightGroup
        end
    end

    self:_RefreshCamAttachment(templateId)
end

PhaseCharInfo._PlayModelEffect = HL.Method(HL.Any, HL.String) << function(self, sceneObject, charId)
    if string.isEmpty(charId) then
        return
    end
    local parent = sceneObject.view.singleEffects

    local charDisplayData = CharInfoUtils.getCharDisplayData(charId)
    if charDisplayData then
        local height = LuaIndex(charDisplayData.height:ToInt())
        parent = parent[string.format("effect%d", height)]
    end

    local effect = sceneObject.view.charEffect
    effect.transform:SetParent(parent)
    effect.transform.localPosition = Vector3.zero
    effect.transform.localEulerAngles = Vector3.zero
    effect.transform.localScale = Vector3.one

    effect.gameObject:SetActive(true)
    effect:Play()
end

PhaseCharInfo._RefreshCamAttachment = HL.Method(HL.String) << function(self, templateId)

    local pathGroup = self.m_templateId2DollyTrackPathGroup[templateId]
    local camPostfix = UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX[UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW]
    local targetCam = pathGroup.view.cameraGroup["vcam_" .. camPostfix]
    local lookAtTarget = pathGroup.view.lookAtGroup["lookat_" .. camPostfix]
    local attachmentObj = self.m_gameObject2Item[PHASE_CHAR_INFO_CAM_ATTACHMENT]

    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]



    attachmentObj.go.transform:SetParent(lookAtTarget.transform.parent)
    attachmentObj.go.transform.localPosition = lookAtTarget.transform.localPosition
    attachmentObj.go.transform.localEulerAngles = lookAtTarget.transform.localEulerAngles
    attachmentObj.go.transform.localRotation = targetCam.transform.localRotation
    attachmentObj.go.transform:SetParent(sceneObject.view.charContainer.transform)

    attachmentObj.view.charTexture:LoadSprite(UIConst.UI_SPRITE_CHAR_INFO, UIConst.UI_CHAR_INFO_CHAR_BG_PREFIX .. templateId)

    local charDisplayData = CharInfoUtils.getCharDisplayData(templateId)
    if charDisplayData then
        attachmentObj.view.offsetRoot.localPosition =charDisplayData.overviewImgOffset
    end
end

PhaseCharInfo._ToggleCamAttachment = HL.Method(HL.Boolean) << function(self, isOn)
    local attachmentObj = self.m_gameObject2Item[PHASE_CHAR_INFO_CAM_ATTACHMENT]

    if attachmentObj then
        attachmentObj.go:SetActive(isOn)
    end
end

PhaseCharInfo.OnSelectCharChange = HL.Method(HL.Table) << function(self, charInfo)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local templateId = charInfo.templateId
    local curControlTab = self.m_curPage
    local curWeaponInstId = CharInfoUtils.getCharCurWeapon(charInfo.instId).weaponInstId
    GameInstance.player.charBag:SetPhaseCharInfoInstId(charInfo.instId)

    self.m_curPreviewWeaponInstId = curWeaponInstId
    self.m_charInfo = charInfo
    self.m_lastCamAnimName = ""

    local isFastCam = true
    if CAMERA_FORCE_FADE_TAB[curControlTab] then
        isFastCam = false
    end

    self:_CleanupLookAtIK()
    self:_RefreshCharModel(sceneObject, charInfo, curControlTab)
    self:_RefreshCamGroup(sceneObject, isFastCam, templateId, self.m_curPage)
    self:_RefreshAddonLight(sceneObject, true, templateId, self.m_curPage, self.m_curPage)

    self:_RefreshVoiceTriggerVo(self.m_curPage)

    self:_TriggerCharBarkSwitch(self.m_curPage)

    self:_RefreshWeaponDeco({
        pageType = self.m_curPage,
    })
    self:_RefreshShowCamTargetGroup()

    if self.m_curPage ~= UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
        AudioAdapter.PostEvent("Au_UI_Event_CharFormation_Glitch")
    end
    if DeviceInfo.usingController then
        AudioAdapter.PostEvent("Au_UI_Toggle_CharSelect")
    end
end

PhaseCharInfo.OnPageChange = HL.Method(HL.Any) << function(self, arg)
    if not self.m_charItemInitComplete then
        logger.info(ELogChannel.GamePlay, "PhaseCharItemLoading, forbid page change")
        return
    end

    local pageType = arg.pageType
    local isFast = arg.isFast == true
    local extraArg = arg.extraArg

    local phaseItem = self.m_charItem
    local beforePage = self.m_curPage

    self.m_beforePage = beforePage
    self.m_curPage = pageType

    local showGlitch = arg.showGlitch
    if showGlitch then
        isFast = true
    end

    local isAnimatorFast = isFast
    local isCamFast = isFast
    local pauseAnimator = false

    if pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL or beforePage == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
        isAnimatorFast = true
        isCamFast = true
    end


    if pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
        pauseAnimator = true
    end

    if self.m_trackDollyTween then
        self.m_trackDollyTween:Kill()
    end

    if self.m_blendPotentialTween then
        self.m_blendPotentialTween:Kill(true)
        self.m_blendPotentialTween = nil
    end

    self.m_uiEffectCor = self:_ClearCoroutine(self.m_uiEffectCor)
    self.m_uiEffectCor = self:_StartCoroutine(function()
        local neededPanels = PAGE_TYPE_2_PANEL_ID[pageType]
        if PAGE_CREATE_IN_ADVANCE[pageType] then


            for _, panelId in ipairs(neededPanels) do
                self:_CreateCharInfoPanel(panelId, pageType, extraArg)
            end
        end

        local neededPanelsBefore = PAGE_TYPE_2_PANEL_ID[beforePage]
        for i, v in pairs(neededPanelsBefore) do
            self:_Trigger_OnPageChange(v, pageType)
        end

        local waitOutDuration = self:_CloseOrHidePanel(neededPanels)

        if pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
            local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
            waitOutDuration = math.max(waitOutDuration, sceneObject.view.config.BLEND_BLACK_SCREEN_TIME)
        end

        coroutine.wait(waitOutDuration)

        if pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
            self:_InitPotentialScene()
        end
        if pageType == UIConst.CHAR_INFO_PAGE_TYPE.PROFILE_SHOW then
            AudioAdapter.PostEvent("au_global_ui_magnifier_panel_enter")
        end
        self:_ShowPanel(neededPanels, pageType, extraArg)
    end)

    self.m_sceneEffectCor = self:_ClearCoroutine(self.m_sceneEffectCor)
    self.m_sceneEffectCor = self:_StartCoroutine(function()
        local templateId = self.m_charInfo.templateId
        local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]

        if showGlitch then
            Notify(MessageConst.PREPARE_BLOCK_GLITCH_TRANSITION)
            coroutine.waitForRenderDone()
            Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
            coroutine.wait(0.2)
        end

        if pageType == UIConst.CHAR_INFO_PAGE_TYPE.PROFILE_SHOW then
            self:_PlayModelEffect(sceneObject, templateId)
            AudioAdapter.PostEvent("Au_UI_Event_CharFormation_Glitch")
        end

        if pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL or beforePage == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
            if beforePage == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
                self:ActivePotentialBlendCamera(true)
            end

            local cameraOffset
            if pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
                cameraOffset = self:BlendCameraPotential(CameraManager.curVirtualCam,true,
                    sceneObject.view.config.POTENTIAL_BLEND_CAM_DELTA_POS, sceneObject.view.config.BLEND_BLACK_SCREEN_TIME)
            end

            local maskData = CS.Beyond.Gameplay.UICommonMaskData()
            maskData.notHideCursor = true
            maskData.fadeInTime = sceneObject.view.config.BLEND_BLACK_SCREEN_TIME
            maskData.fadeWaitTime = 0.01
            maskData.fadeOutTime = sceneObject.view.config.BLEND_BLACK_SCREEN_TIME
            maskData.fadeInCallback = function()
                if self.m_blendPotentialTween then
                    self.m_blendPotentialTween:Kill(true)
                    self.m_blendPotentialTween = nil
                end
                if cameraOffset then
                    cameraOffset.m_Offset = Vector3.zero
                end
            end
            if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
                maskData.extraData = CS.Beyond.Gameplay.CommonMaskExtraData()
                maskData.extraData.desc = "CharInfo"
            end

            GameAction.ShowBlackScreen(maskData)
            coroutine.wait(sceneObject.view.config.BLEND_BLACK_SCREEN_TIME)
            if pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
                self:_InitPotentialScene()
            end

            if beforePage == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
                self:ActivePotentialBlendCamera(false)
            end
        end

        self:_ResetCam()

        local fromStateIndex, toStateIndex = self:_GetControllerStateIndex(beforePage, pageType)
        if (toStateIndex ~= nil and fromStateIndex ~= nil) and (isAnimatorFast or (toStateIndex ~= fromStateIndex)) then
            if phaseItem and phaseItem.go then
                self:_SwitchCharacterControllerState(phaseItem, fromStateIndex, toStateIndex, isAnimatorFast)
            end
        end

        local uiModelMono = phaseItem.uiModelMono
        if uiModelMono then
            if pauseAnimator then
                uiModelMono:PauseAnimator(true)
            else
                uiModelMono:PauseAnimator(false)
            end
        end

        self:_RefreshCamGroup(sceneObject, isCamFast, templateId, pageType, beforePage)
        self:_RefreshAddonLight(sceneObject, isCamFast, templateId, pageType, beforePage)
        self:_RefreshVoiceTriggerVo(pageType)

        self:_ToggleCamAttachment(pageType == UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW)
        self:_ToggleChrSPMotionCor(pageType == UIConst.CHAR_INFO_PAGE_TYPE.PROFILE_SHOW)
        self:_ToggleTalentFloorEffect(pageType == UIConst.CHAR_INFO_PAGE_TYPE.TALENT)

        self:_RefreshWeaponDeco({
            pageType = pageType,
            beforePage = beforePage
        })
        local isWeaponDecoOn = pageType == UIConst.CHAR_INFO_PAGE_TYPE.WEAPON
        self:_ToggleWeaponDeco(isWeaponDecoOn, beforePage)

        self:_RefreshGridDeco({
            pageType = pageType,
        })
        self:_RefreshCharUpgradeDeco(pageType)
        self:_RefreshShowCamTargetGroup()
        self:_RefreshProfileShowCam({
            pageType = pageType,
        })
        self:_SetActivePotentialItems(pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL)

        if self.m_camTransitionCache ~= nil then
            self:_SetCamWithTrack(self.m_camTransitionCache.isFast, self.m_camTransitionCache.pathGroup, self.m_camTransitionCache.toCameraPostfix, self.m_camTransitionCache.fromCameraPostfix)
            self.m_camTransitionCache = nil
        end

        if pageType == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
            self:ActivePotentialBlendCamera(true)
            coroutine.wait(0.1)
            self:ActivePotentialBlendCamera(false)
        end

        if beforePage == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
            coroutine.step()
            coroutine.step()
            self:BlendCameraPotential(CameraManager.curVirtualCam, false,
                sceneObject.view.config.POTENTIAL_BLEND_CAM_DELTA_POS, sceneObject.view.config.BLEND_BLACK_SCREEN_TIME)
        end

        self.m_charItem.uiModelMono.jumpWeaponAnimHideToHide = false
        self.m_charItem.uiModelMono.jumpStaticWeaponAnimHideToHide = false
        if pageType == UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE or pageType == UIConst.CHAR_INFO_PAGE_TYPE.TALENT then
            self.m_charItem.uiModelMono.jumpWeaponAnimHideToHide = true
            self.m_charItem.uiModelMono.jumpStaticWeaponAnimHideToHide = true
        end

        if beforePage == UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW and pageType == UIConst.CHAR_INFO_PAGE_TYPE.WEAPON then
            local weaponState = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE
            self.m_charItem.uiModelMono.jumpWeaponAnimHideToHide = true
            self.m_charItem.uiModelMono.jumpStaticWeaponAnimHideToHide = true
            self.m_charItem.uiModelMono:SwitchWeaponState(weaponState, true)
            self.m_charItem.uiModelMono:SwitchStaticWeaponState(weaponState)
        end
    end)
end


PhaseCharInfo._CloseOrHidePanel = HL.Method(HL.Table).Return(HL.Number) << function(self, neededPanels)
    local waitOutDuration = 0.2
    for panelId, panelItem in pairs(self.m_panel2Item) do
        if panelItem.uiCtrl:IsShow() and (not lume.find(neededPanels, panelId)) then
            local outDuration = panelItem.uiCtrl:GetAnimationOutDuration()
            waitOutDuration = math.max(waitOutDuration, outDuration)

            if CLOSE_WHEN_ANIMATION_OUT[panelId] then
                panelItem.uiCtrl:PlayAnimationOutWithCallback(function()
                    self:CloseCharInfoPanel(panelId)
                end)
            else
                panelItem.uiCtrl:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Hide)
            end
        end
    end

    return waitOutDuration
end
PhaseCharInfo._ShowPanel = HL.Method(HL.Table, HL.Number, HL.Opt(HL.Table)) << function(self, neededPanels, pageType, extraArg)
    for _, panelId in ipairs(neededPanels) do
        if not self.m_panel2Item[panelId] then
            self:_CreateCharInfoPanel(panelId, pageType, extraArg)
        else
            local panelItem = self.m_panel2Item[panelId]
            self:_TryShowFinal(panelItem, pageType, extraArg)
        end
    end
end

PhaseCharInfo._TryShowFinal = HL.Method(HL.Userdata, HL.Number, HL.Opt(HL.Table, HL.Table)) << function(self, panelItem, pageType, extraArg, stateArg)
    if HL.TryGet(panelItem.uiCtrl, "PhaseCharInfoPanelShowFinal") then
        panelItem.uiCtrl:PhaseCharInfoPanelShowFinal({
            initCharInfo = self.m_charInfo,
            pageType = pageType,
            phase = self,
            lastMainControlTab = self.m_beforePage,
            extraArg = extraArg,
            stateArg = stateArg,
        })
    else
        panelItem.uiCtrl:Show()
    end
end

PhaseCharInfo._CreateCharInfoPanel = HL.Method(HL.Number, HL.Number, HL.Opt(HL.Any)) << function(self, panelId, pageType, extraArg)
    if self.m_panel2Item[panelId] then
        return
    end

    local phasePanelItem = self:CreatePhasePanelItem(panelId, {
        initCharInfo = self.m_charInfo,
        pageType = pageType,
        phase = self,
        lastMainControlTab = self.m_beforePage,
        extraArg = extraArg,
    })

    self:_ProcessPanelInScene(phasePanelItem)
end

PhaseCharInfo._ProcessPanelInScene = HL.Method(HL.Userdata) << function(self, panelItem)
    if PANEL_IN_SCENE[panelItem.uiCtrl.panelId] == true then
        local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
        local panelRoot = sceneObject.view.panelRoot

        panelItem.uiCtrl.view.transform:SetParent(panelRoot)
        panelItem.uiCtrl.view.transform:Reset()
        panelItem.uiCtrl.view.canvas.worldCamera = CameraManager.mainCamera
    end
end

PhaseCharInfo._GetControllerStateIndex = HL.Method(HL.Opt(HL.Number, HL.Number)).Return(HL.Opt(HL.Number, HL.Number)) << function(self, pageTypeBefore, pageTypeNow)
    if not pageTypeBefore or not pageTypeNow then
        return nil, nil
    end

    local fromStateIndex = UIConst.CHAR_INFO_PAGE_2_ANIMATOR_INDEX_DICT[pageTypeBefore] or -1
    local toStateIndex = UIConst.CHAR_INFO_PAGE_2_ANIMATOR_INDEX_DICT[pageTypeNow] or -1


    if pageTypeNow == UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL then
        toStateIndex = UIConst.PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.OVERVIEW
    end

    return fromStateIndex, toStateIndex
end

PhaseCharInfo._ToggleTalentFloorEffect = HL.Method(HL.Boolean) << function(self, isOn)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    sceneObject.view.charFloorEffect:ClearTween()
    if isOn  then
        sceneObject.view.charFloorEffect:PlayInAnimation()
    else
        sceneObject.view.charFloorEffect:PlayOutAnimation()
    end
end

PhaseCharInfo._ToggleChrSPMotionCor = HL.Method(HL.Boolean) << function(self, isOn)
    local phaseItem = self.m_charItem
    if phaseItem.go == nil then
        return
    end
    local animator = phaseItem.go:GetComponent("Animator")
    self.m_charItem.uiModelMono:ActiveRotationRootMotion(isOn)

    local idleWaitTime = 0
    local isTriggerRelax = false

    if self.m_spMotionUpdateKey then
        LuaUpdate:Remove(self.m_spMotionUpdateKey)
    end
    if isOn then
        local charDisplayData = CharInfoUtils.getCharDisplayData(self.m_charInfo.templateId)

        local relaxSpIdleConfig = DataManager.characterDisplayConfig.defaultRelaxSpIdleConfig
        if charDisplayData.overrideSpIdleConfig then
            relaxSpIdleConfig = charDisplayData.charRelaxSpIdleConfig
        end
        local minIdleTime = relaxSpIdleConfig.minIdleTime
        local spWeight = {}
        spWeight[UIConst.PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.SP_1] = relaxSpIdleConfig.sp1IdleWeight
        spWeight[UIConst.PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.SP_2] = relaxSpIdleConfig.sp2IdleWeight

        self.m_spMotionUpdateKey = LuaUpdate:Add("LateTick", function(deltaTime)
            local isInRelaxIdle = animator:GetCurrentAnimatorStateInfo():IsName("RelaxIdle")
            local isInTransition = animator:IsInTransition(0)
            local isEnableSwitch = animator:GetBool(UIConst.PHASE_CHAR_ITEM_ENABLE_SWITCH)
            if isEnableSwitch or not isInRelaxIdle then
                return
            end


            local charReactCfg = charDisplayData.charRelaxReactConfig
            if not isInTransition and not isEnableSwitch and (not isTriggerRelax or not charReactCfg.triggerOnce) then
                local cameraZoomScale = self.m_showCamController.virtualCamera.ActiveZoomScale

                local relativeAngle = self.m_charItem.uiModelMono:GetCameraRelativeAngle()
                local isAngleInRange = false
                if charReactCfg.invertRange then
                    isAngleInRange = relativeAngle < charReactCfg.relativeAngleDegreeRange.x or relativeAngle > charReactCfg.relativeAngleDegreeRange.y
                else
                    isAngleInRange = relativeAngle >= charReactCfg.relativeAngleDegreeRange.x and relativeAngle <= charReactCfg.relativeAngleDegreeRange.y
                end
                local isZoomInRange = cameraZoomScale >= charReactCfg.cameraZoomScaleRange.x and cameraZoomScale <= charReactCfg.cameraZoomScaleRange.y

                if isAngleInRange and isZoomInRange then
                    local fromIndex = UIConst.CHAR_INFO_PAGE_2_ANIMATOR_INDEX_DICT[self.m_curPage]
                    local toIndex = UIConst.PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.SP_RELAX
                    self:_SwitchCharacterControllerState(phaseItem, fromIndex, toIndex, nil, true)
                    isTriggerRelax = true
                    return
                end
            end

            idleWaitTime = idleWaitTime + deltaTime
            if idleWaitTime < minIdleTime then
                return
            end

            local fromIndex = UIConst.CHAR_INFO_PAGE_2_ANIMATOR_INDEX_DICT[self.m_curPage]
            local toIndex = lume.weightedchoice(spWeight)

            self:_SwitchCharacterControllerState(phaseItem, fromIndex, toIndex, nil, true)
            isTriggerRelax = false
            idleWaitTime = 0
        end)
    end
end

PhaseCharInfo._DebugShowCharSPMotion = HL.Method(HL.Number) << function(self, toIndex)
    self:_SwitchCharacterControllerState(self.m_charItem, UIConst.PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.PROFILE_SHOW, toIndex)
end

PhaseCharInfo._SwitchCharacterControllerState = HL.Method(HL.Any, HL.Number, HL.Number, HL.Opt(HL.Boolean, HL.Boolean)) << function(
    self, charItem, fromIndex, toIndex, skipIn, ignoreResetEffect)
    if not ignoreResetEffect then
        charItem.uiModelMono:ResetAllEntityRenderHelper()
        if charItem.uiModelMono.animatorPlayEffectHelper then
            charItem.uiModelMono.animatorPlayEffectHelper:ClearAllEffects()
        end
    end

    charItem:SetInteger(UIConst.PHASE_CHAR_ITEM_FROM_INDEX, fromIndex)
    charItem:SetInteger(UIConst.PHASE_CHAR_ITEM_TO_INDEX, toIndex)
    charItem:SetTrigger(UIConst.PHASE_CHAR_ITEM_ENABLE_SWITCH)

    if skipIn then
        charItem:SetTrigger(UIConst.PHASE_CHAR_ITEM_SKIP_PARAM_NAME)
    end


    if skipIn then
        local weaponState = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE
        if UIConst.CHAR_INFO_ANIMATOR_INDEX_2_WEAPON_STATE[toIndex] then
            weaponState = UIConst.CHAR_INFO_ANIMATOR_INDEX_2_WEAPON_STATE[toIndex]
        end

        charItem:SwitchWeaponState(weaponState)
    end

    self:_HandleLookAtIK(charItem, toIndex, skipIn)
end
PhaseCharInfo._HandleLookAtIK = HL.Method(HL.Any, HL.Number, HL.Opt(HL.Boolean)) << function(self, charItem, toIndex, skipIn)
    local lookAtIKTarget = CameraManager.mainCamera.transform:Find("LookAtIkTarget")
    if lookAtIKTarget == nil then
        lookAtIKTarget = GameObject("LookAtIkTarget").transform
        lookAtIKTarget:SetParent(CameraManager.mainCamera.transform)
        lookAtIKTarget:Reset()
    end

    self:_CleanupLookAtIK()
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local lookAtIk = charItem.go:GetComponent("LookAtIK")
    lookAtIKTarget.localPosition = sceneObject.view.config.LOOK_AT_OFFSET
    if skipIn then
        lookAtIk.solver:SetLookAtWeight(0)
    else
        if lookAtIk.solver.IKPositionWeight > 0 then
            self.m_lookAtTween = DOTween.To(function()
                return lookAtIk.solver.IKPositionWeight
            end, function(value)
                lookAtIk.solver:SetLookAtWeight(value)
            end, 0, sceneObject.view.config.LOOK_AT_TWEEN_REST_DURATION)
        end
    end
    lookAtIk.solver.target = lookAtIKTarget.transform

    if toIndex == UIConst.PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.OVERVIEW then
        local animator = charItem.go:GetComponent("Animator")
        if skipIn == true then
            lookAtIk.solver:SetLookAtWeight(1)
        else
            self.m_lookAtTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
                if not NotNull(animator) then
                    LuaUpdate:Remove(self.m_lookAtTickKey)
                    self.m_lookAtTickKey = -1
                    return
                end
                local state = animator:GetCurrentAnimatorStateInfo()
                local isToOverviewState = false
                for _, stateName in pairs(UIConst.CHAR_INFO_TO_OVERVIEW_STATE_NAME) do
                    if state:IsName(stateName) then
                        isToOverviewState = true
                        break
                    end
                end
                if isToOverviewState and (state.normalizedTime > sceneObject.view.config.LOOK_AT_CROSS_FADE_STATE_NORMALIZED_TIME) then
                    self.m_lookAtTween = DOTween.To(function()
                        return lookAtIk.solver.IKPositionWeight
                    end, function(value)
                        lookAtIk.solver:SetLookAtWeight(value)
                    end, 1, sceneObject.view.config.LOOK_AT_TWEEN_DURATION)
                    LuaUpdate:Remove(self.m_lookAtTickKey)
                    self.m_lookAtTickKey = -1
                end
            end)
        end
    end
end

PhaseCharInfo._CleanupLookAtIK = HL.Method() << function(self)
    if self.m_lookAtTickKey > 0 then
        LuaUpdate:Remove(self.m_lookAtTickKey)
    end
    if self.m_lookAtTween ~= nil then
        self.m_lookAtTween:Kill()
    end
end

PhaseCharInfo._Trigger_OnPageChange = HL.Method(HL.Number, HL.Number) << function(self, panelId, pageType)
    local panelItem = self.m_panel2Item[panelId]
    if not panelItem then
        return
    end

    if HL.TryGet(panelItem.uiCtrl, "OnPageChange") then
        panelItem.uiCtrl:OnPageChange(pageType)
    end

    panelItem.uiCtrl:Show()
end

PhaseCharInfo._RefreshShowCamTargetGroup = HL.Method() << function(self)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local templateId = self.m_charInfo.templateId
    local pathGroup = self.m_templateId2DollyTrackPathGroup[templateId]
    local lookAtGroup = pathGroup.view.lookAtGroup
    if lookAtGroup then
        sceneObject.view.nearTarget.transform.position = lookAtGroup.lookat_show_near.position
        sceneObject.view.farTarget.transform.position = lookAtGroup.lookat_show_far.position
    end

    sceneObject.view.targetGroup:DoUpdate()
end

PhaseCharInfo.OnCharInfoShowRotateChar = HL.Method(HL.Number) << function(self, deltaAngle)
    if self.m_charItem == nil then
        return
    end
    self.m_charItem:RotateChar(deltaAngle)
end

PhaseCharInfo.OnCharInfoShowZooming = HL.Method(HL.Number) << function(self, delta)
    self.m_zoomCache = delta
end

PhaseCharInfo._CloseAllCamGroup = HL.Method() << function(self)
    for i, disableGroup in pairs(self.m_templateId2DollyTrackPathGroup) do
        disableGroup.go:SetActive(false)
    end
end

PhaseCharInfo._ClearAudioCor = HL.Method() << function(self)
    self.m_voiceCor = PhaseManager:_ClearCoroutine(self.m_voiceCor)
end

PhaseCharInfo._RefreshCamGroup = HL.Method(HL.Userdata, HL.Boolean, HL.String, HL.Number, HL.Opt(HL.Number))
        << function(self, sceneObject, isFast, charTemplateId, pageType, pageTypeBefore)
    self:_CloseAllCamGroup()
    local pathGroup = self.m_templateId2DollyTrackPathGroup[charTemplateId]
    sceneObject.view.cameraScene.gameObject:SetActive(pathGroup == nil)

    if pathGroup then
        local toCameraPostfix = UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX[pageType]
        local fromCameraPostfix = pageTypeBefore and UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX[pageTypeBefore]

        self:_SetCamWithTrack(isFast, pathGroup, toCameraPostfix, fromCameraPostfix)
    end

    local volumeGroup = self.m_templateId2VolumeGroup[charTemplateId]
    if volumeGroup then
        self:_SetOverrideVolume(isFast, sceneObject, volumeGroup, pageType, pageTypeBefore)
    end
end

PhaseCharInfo._RefreshAddonLight = HL.Method(HL.Userdata, HL.Boolean, HL.String, HL.Number, HL.Opt(HL.Number)) << function(self, sceneObject, isFast, charTemplateId, pageType, pageTypeBefore)
    local newLightPostfix = pageType and UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX[pageType]
    local lastLightPostfix = pageTypeBefore and UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX[pageTypeBefore]

    for _, lightGroup in pairs(self.m_templateId2LightGroup) do
        lightGroup.go:SetActive(false)
    end

    local targetLightGroup = self.m_templateId2LightGroup[charTemplateId]
    if not targetLightGroup then
        return
    end

    for _, lightIndex in pairs(UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX) do
        local lightName = string.format("light_%s", lightIndex)
        local light = targetLightGroup.view[lightName]
        if light then
            light.gameObject:SetActive(false)
        end
    end

    targetLightGroup.go:SetActive(true)

    local newLight
    local lastLight
    local tweenDuration = isFast and 0 or 1
    local newLightName = pageType and string.format("light_%s", newLightPostfix) or ""
    local lastLightName = pageTypeBefore and string.format("light_%s", lastLightPostfix) or ""
    newLight = targetLightGroup.view[newLightName]
    lastLight = targetLightGroup.view[lastLightName]

    if lastLight then
        lastLight.gameObject:SetActive(true)
        lastLight:TweenLightGroupAlpha(1, 0, tweenDuration)
    end

    if newLight then
        newLight.gameObject:SetActive(true)
        newLight:TweenLightGroupAlpha(0, 1, tweenDuration)
    end
end

PhaseCharInfo._SetOverrideVolume = HL.Method(HL.Boolean, HL.Userdata, HL.Table, HL.Number, HL.Opt(HL.Number)) << function(self, isInit, sceneObject, volumeGroup, tabType, tabTypeBefore)
    local overrideVolume = sceneObject.view.charOverrideVolume
    if not overrideVolume then
        return
    end

    local toVolumePostfix = UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX[tabType]
    local fromVolumePostfix = UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX[tabTypeBefore]
    if toVolumePostfix == fromVolumePostfix then
        return
    end

    local toVolumeModifierName = string.format("volume_%s", toVolumePostfix)
    local toVolumeModifier = volumeGroup[toVolumeModifierName]

    if toVolumeModifier then
        local tweenDuration = isInit and 0 or toVolumeModifier.tweenDuration
        local tween = toVolumeModifier:GetMainLightBiasTween(overrideVolume, tweenDuration)
        if self.m_charVolumeTween then
            self.m_charVolumeTween:Kill()
        end

        self.m_charVolumeTween = tween
        tween:Play()
    end
end

PhaseCharInfo._GetTargetVCam = HL.Method(HL.String, HL.Number).Return(HL.Userdata) << function(self, charTemplateId, pageType)
    local pathGroup = self.m_templateId2DollyTrackPathGroup[charTemplateId]
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]

    sceneObject.view.cameraScene.gameObject:SetActive(pathGroup == nil)

    local targetVCam
    local toCameraPostfix = UIConst.CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX[pageType]
    for _, camPostfix in pairs(UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT) do
        local cam = pathGroup.view.cameraGroup["vcam_" .. camPostfix]
        if cam then
            local isTargetCam = toCameraPostfix == camPostfix
            if isTargetCam then
                targetVCam = cam
            end
        end
    end

    return targetVCam
end

PhaseCharInfo._GetLookAtTarget = HL.Method(HL.String, HL.Number).Return(HL.Userdata) << function(self, charTemplateId, pageType)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local targetVCam = self:_GetTargetVCam(charTemplateId, pageType)
    if targetVCam.transform:Find("CharInfoLookAtTarget") then
        return targetVCam.transform:Find("CharInfoLookAtTarget").gameObject
    end

    local lookAtTarget = GameObject("CharInfoLookAtTarget")

    lookAtTarget.transform:SetParent(targetVCam.transform)
    lookAtTarget.transform:Reset()
    lookAtTarget.transform.localPosition = sceneObject.view.config.LOOK_AT_OFFSET
    return lookAtTarget
end


PhaseCharInfo._SetCamWithTrack = HL.Method(HL.Boolean, HL.Userdata, HL.String, HL.Opt(HL.String)) << function(self, isFast, pathGroup, toCameraPostfix, fromCameraPostfix)
    pathGroup.go:SetActive(true)
    local targetVCam
    if not toCameraPostfix then
        return
    end

    for _, camPostfix in pairs(UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT) do
        local cam = pathGroup.view.cameraGroup["vcam_" .. camPostfix]
        if cam then
            local isTargetCam = toCameraPostfix == camPostfix
            cam.gameObject:SetActive(isTargetCam)
            if isTargetCam then
                targetVCam = cam
            end
        end
    end

    if not targetVCam then
         return
    end

    self.m_curCamPostfix = toCameraPostfix

    local dollyPathName = string.format("%s_%s", fromCameraPostfix, toCameraPostfix)
    local dollyPathTransform = pathGroup.view.paths[dollyPathName]
    if not dollyPathTransform then
        return
    end

    if self.m_trackDollyTween then
        self.m_trackDollyTween:Kill()
    end
    if self.m_lastCamAnimName ~= nil and not string.isEmpty(self.m_lastCamAnimName) then
        pathGroup.view.animation:SeekToPercent(self.m_lastCamAnimName, 1)
    end

    local useCustomAnim = pathGroup.view.config:HasValue(string.format("CUSTOM_ANIM_%s_%s", fromCameraPostfix, toCameraPostfix))
    if useCustomAnim then
        local customAnimName = pathGroup.view.config[string.format("CUSTOM_ANIM_%s_%s", fromCameraPostfix, toCameraPostfix)]
        self.m_lastCamAnimName = customAnimName
        if isFast then
            pathGroup.view.animation:SeekToPercent(customAnimName, 1)
        else
            pathGroup.view.animation:Play(customAnimName)
        end
        return
    end

    local addonAnim = pathGroup.view.config:HasValue(string.format("ADDON_ANIM_%s_%s", fromCameraPostfix, toCameraPostfix))
    if addonAnim then
        local addonAnimName = pathGroup.view.config[string.format("ADDON_ANIM_%s_%s", fromCameraPostfix, toCameraPostfix)]
        self.m_lastCamAnimName = addonAnimName
        if isFast then
            pathGroup.view.animation:SeekToPercent(addonAnimName, 1)
        else
            pathGroup.view.animation:Play(addonAnimName)
        end
    end

    local trackedDolly = targetVCam:GetCinemachineComponent(CS.Cinemachine.CinemachineCore.Stage.Body)
    if trackedDolly then
        trackedDolly.m_PathPosition = 1
        if isFast then
            CameraManager:SetNextBlendOverride(0, CS.Cinemachine.CinemachineBlendDefinition.Style.Cut)
            return
        end

        if not fromCameraPostfix then
            return
        end

        local dollyTrackPath = dollyPathTransform.gameObject:GetComponent("CinemachineSmoothPath")
        pathGroup.go:SetActive(true)
        trackedDolly.m_Path = dollyTrackPath
        trackedDolly.m_PathPosition = 0
    end

    local tweenSpeed = pathGroup.view.config.CAMERA_SPEED
    if pathGroup.view.config:HasValue(string.format("SPEED_%s", dollyPathName)) then
        tweenSpeed = pathGroup.view.config[string.format("SPEED_%s", dollyPathName)]
    end

    local tween = CSUtils.TweenTo(0, 1, tweenSpeed, function(x)
        if NotNull(trackedDolly) then
            trackedDolly.m_PathPosition = x
        end
    end)

    self.m_trackDollyTween = tween
    self.m_trackDollyTween:Play()
end

PhaseCharInfo._ToggleSceneLight = HL.Method(HL.Boolean) << function(self, isOn)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    sceneObject.view.light.gameObject:SetActive(isOn)
    local potentialScene = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    if potentialScene then
        potentialScene.view.light.gameObject:SetActive(isOn)
    end
end

PhaseCharInfo._SetListCameraDOF = HL.Method() << function(self)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]

    local listDOFParams = Utils.stringJsonToTable(sceneObject.view.config.LIST_DOF_PARAM)
    local data = CS.HG.Rendering.Runtime.HGDepthOfFieldData(
            listDOFParams.type,
            listDOFParams.nearFocusStart,
            listDOFParams.nearFocusEnd,
            listDOFParams.nearRadius,
            listDOFParams.farFocusStart,
            listDOFParams.farFocusEnd,
            listDOFParams.farRadius
    )
    Utils.enableCameraDOF(data)
end

PhaseCharInfo.OnCharTalentFocus = HL.Method(HL.Boolean) << function(self, isFast)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local pathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]



    if self.m_curPage ~= UIConst.CHAR_INFO_PAGE_TYPE.TALENT or self.m_curCamPostfix ~= UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.TALENT then
        self.m_camTransitionCache = {
            isFast = isFast,
            pathGroup = pathGroup,
            fromCameraPostfix = UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.TALENT,
            toCameraPostfix = UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.TALENT_FOCUS
        }
        return
    end
    self:_SetCamWithTrack(isFast, pathGroup, UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.TALENT_FOCUS, UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.TALENT)
end

PhaseCharInfo.OnCharTalentLeaveFocus = HL.Method(HL.Boolean) << function(self, isFast)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local pathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]

    self:_SetCamWithTrack(isFast, pathGroup, UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.TALENT, UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.TALENT_FOCUS)
end

PhaseCharInfo._BlendExitPhase = HL.Method(HL.Table) << function(self, arg)
    local curActiveCam = CameraManager.curVirtualCam
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local blendCamera = sceneObject.view.charInfoBlendCam

    blendCamera.transform.position = curActiveCam.State.RawPosition + sceneObject.view.config.BLEND_CAM_DELTA_POS
    blendCamera.transform.rotation = curActiveCam.State.RawOrientation
    self.m_blendTransitionCor = self:_ClearCoroutine(self.m_blendTransitionCor)
    self.m_blendTransitionCor = self:_StartCoroutine(function()
        blendCamera.gameObject:SetActive(true)

        Notify(MessageConst.BLOCK_LUA_UI_INPUT, {true, "CharInfo"})
        coroutine.wait(sceneObject.view.config.BLEND_BLACK_SCREEN_WAIT_TIME)
        Notify(MessageConst.BLOCK_LUA_UI_INPUT, {false, "CharInfo"})

        local maskData = CS.Beyond.Gameplay.UICommonMaskData()
        maskData.notHideCursor = true
        maskData.fadeInTime = sceneObject.view.config.BLEND_BLACK_SCREEN_TIME
        maskData.fadeBeforeTime = 0
        maskData.fadeOutTime = sceneObject.view.config.BLEND_BLACK_SCREEN_TIME
        maskData.fadeInCallback = function()
            if arg.finishCallback then
                arg.finishCallback()
            end
            blendCamera.gameObject:SetActive(false)
        end
        if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
            maskData.extraData = CS.Beyond.Gameplay.CommonMaskExtraData()
            maskData.extraData.desc = "CharInfo"
        end

        GameAction.ShowBlackScreen(maskData)
    end)

    self.m_isBlendExit = true
end

PhaseCharInfo._BlendBackPhase = HL.Method() << function(self)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local blendCamera = sceneObject.view.charInfoBlendCam

    blendCamera.gameObject:SetActive(true)
    self.m_blendTransitionCor = self:_ClearCoroutine(self.m_blendTransitionCor)
    self.m_blendTransitionCor = self:_StartCoroutine(function()

        coroutine.wait(0.1)

        blendCamera.gameObject:SetActive(false)
    end)
end



PhaseCharInfo.m_profileShow = HL.Field(HL.Forward("PhasePanelItem"))

PhaseCharInfo.m_updateKey = HL.Field(HL.Number) << -1

PhaseCharInfo.m_updateTime= HL.Field(HL.Number) << 0

PhaseCharInfo.m_targetWeight = HL.Field(HL.Number) << 1


PhaseCharInfo.m_showCamController = HL.Field(CS.Beyond.Gameplay.View.CustomFreeLookCameraController)


PhaseCharInfo._RefreshProfileShowCam = HL.Method(HL.Table) << function(self, arg)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local pageType = arg.pageType

    sceneObject.view.charInfoShowCam.gameObject:SetActive(pageType == UIConst.CHAR_INFO_PAGE_TYPE.PROFILE_SHOW)

    if pageType == UIConst.CHAR_INFO_PAGE_TYPE.PROFILE then
        local pathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]
        local cam = pathGroup.view.cameraGroup["vcam_" .. UIConst.PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.DOCUMENT]
        local desPosition = cam.transform.position + sceneObject.view.config.PROFILE_CAM_DELTA_POS
        local desRot = cam.transform.eulerAngles + sceneObject.view.config.PROFILE_CAM_DELTA_ROT
        local desFov = cam.m_Lens.FieldOfView

        sceneObject.view.charInfoProfileCam.transform.position = desPosition
        sceneObject.view.charInfoProfileCam.transform.eulerAngles = desRot
        sceneObject.view.charInfoProfileCam.m_Lens.FieldOfView = desFov
    elseif pageType == UIConst.CHAR_INFO_PAGE_TYPE.PROFILE_SHOW then
        if not self.m_showCamController then
            self.m_showCamController = CameraManager:CreateOrGetTemporaryController(sceneObject.view.charInfoShowCam)
        end
        self.m_showCamController:SetTarget(sceneObject.view.targetGroup.transform)
        self.m_showCamController:SetCameraHorizontalAngle(180, false)
        self.m_showCamController:SetZoomScale(1, false)
        local pathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]
        local lookAtGroup = pathGroup.view.lookAtGroup.lookat_show_group
        if lookAtGroup then
            sceneObject.view.charInfoShowCam.LookAt = lookAtGroup.transform
        end
        self.m_showCamController:ForceFlush()

        if self.m_updateKey < 0 then
            self.m_updateKey = LuaUpdate:Add("LateTick", function(deltaTime)
                if self.m_zoomCache then
                    CameraManager:Zoom(self.m_zoomCache, false)
                end
                self.m_zoomCache = nil
                self:_UpdateTargetWeight(deltaTime)
            end)
        end
    end
end

PhaseCharInfo._UpdateTargetWeight = HL.Method(HL.Opt(HL.Number, HL.Number)) << function(self, deltaTime, weight)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local nearTarget = sceneObject.view.targetGroup.m_Targets[0]
    local farTarget = sceneObject.view.targetGroup.m_Targets[1]
    if weight == nil then
        local minZoom = self.m_showCamController.minZoom
        local maxZoom = self.m_showCamController.maxZoom
        local curZoom = self.m_showCamController.freeLookVirtualCamera:GetCurZoomScale(deltaTime)
        local amount = (curZoom - minZoom) / (maxZoom - minZoom)
        local targetWeight = sceneObject.view.config.TARGET_GROUP_WEIGHT_CURVE:Evaluate(amount)
        weight = targetWeight
    end

    local pathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]
    local lookAtGroup = pathGroup.view.lookAtGroup.lookat_show_group
    if lookAtGroup then
        local nearLookAt = lookAtGroup.m_Targets[0]
        local farLookAt = lookAtGroup.m_Targets[1]
        nearLookAt.weight = weight
        farLookAt.weight = 1 - weight
        lookAtGroup.m_Targets[0] = nearLookAt
        lookAtGroup.m_Targets[1] = farLookAt
        lookAtGroup:DoUpdate()
    end

    if UNITY_EDITOR then
        self:_RefreshShowCamTargetGroup()
    end

    nearTarget.weight = weight
    farTarget.weight = 1 - weight
    sceneObject.view.targetGroup.m_Targets[0] = nearTarget
    sceneObject.view.targetGroup.m_Targets[1] = farTarget
    sceneObject.view.targetGroup:DoUpdate()
    self.m_showCamController:ForceFlush()
end

PhaseCharInfo.OnCharInfoProfileClose = HL.Method() << function(self)
    if not self.m_charItemInitComplete then
        logger.info(ELogChannel.GamePlay, "PhaseCharItemLoading, forbid profile close")
        return
    end
    self:_ClearShowCam()
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    sceneObject.view.charInfoProfileCam.gameObject:SetActive(false)

    if self.m_charItem then
        self.m_charItem:ResetChar()
        self:_PlayModelEffect(sceneObject, self.m_charItem.charId)
    end
end

PhaseCharInfo._ClearShowCam = HL.Method() << function(self)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    if self.m_showCamController then
        CameraManager:RemoveCameraController(self.m_showCamController)
        sceneObject.view.charInfoShowCam.gameObject:SetActive(false)
        self.m_showCamController = nil
    end

    LuaUpdate:Remove(self.m_updateKey)
    self.m_updateKey = -1
    self.m_zoomCache = nil
end





PhaseCharInfo.OnCharInfoEquipSecondEnter = HL.Method() << function(self)
    local trackPathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]
    local extraCams = trackPathGroup.view.extraCams

    extraCams.extra_cam_equip_second.gameObject:SetActive(true)

    local panel = self:_GetPanelPhaseItem(PanelId.CharInfoEquipSlot).uiCtrl
    panel:SetState(UIConst.CHAR_INFO_EQUIP_STATE.Detail)
end

PhaseCharInfo.OnCharInfoEquipSecondClose = HL.Method() << function(self)
    local trackPathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]
    local extraCams = trackPathGroup.view.extraCams

    extraCams.extra_cam_equip_second.gameObject:SetActive(false)

    local panel = self:_GetPanelPhaseItem(PanelId.CharInfoEquipSlot).uiCtrl
    panel:SetState(UIConst.CHAR_INFO_EQUIP_STATE.Normal)
end





PhaseCharInfo.OnCharInfoWeaponSecondEnter = HL.Method() << function(self)
    local trackPathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]
    local extraCams = trackPathGroup.view.extraCams

    extraCams.extra_cam_weapon_second.gameObject:SetActive(true)
end

PhaseCharInfo.OnCharInfoWeaponSecondClose = HL.Method() << function(self)
    local trackPathGroup = self.m_templateId2DollyTrackPathGroup[self.m_charInfo.templateId]
    local extraCams = trackPathGroup.view.extraCams

    extraCams.extra_cam_weapon_second.gameObject:SetActive(false)
end

PhaseCharInfo.OnPreviewWeaponChange = HL.Method(HL.Number) << function(self, weaponInstId)
    if self.m_curPreviewWeaponInstId == weaponInstId then
        return
    end

    self.m_curPreviewWeaponInstId = weaponInstId
    if self.m_charItemInitComplete then
        self:ReloadPreviewWeapon()
    else
        self.m_actionsAfterCharItemInitCompleted:Push(function()
            self:ReloadPreviewWeapon()
        end)
    end
end

PhaseCharInfo.ReloadPreviewWeapon = HL.Method() << function(self)
    local phaseItem = self.m_charItem
    local weaponState = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE

    phaseItem:LoadTargetWeapon(self.m_curPreviewWeaponInstId)
    phaseItem:SwitchWeaponState(weaponState, true)
    phaseItem.uiModelMono:PlayWeaponChangeEffect()
end

PhaseCharInfo.OnGemAttach = HL.Method(HL.Table) << function(self, arg)
    local phaseItem = self.m_charItem
    local weaponState = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE

    phaseItem:ReloadWeaponDecoEffect(self.m_curPreviewWeaponInstId)
    phaseItem:SwitchWeaponState(weaponState, true)
end

PhaseCharInfo.OnGemDetach = HL.Method(HL.Table) << function(self, arg)
    local phaseItem = self.m_charItem
    local weaponState = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE
    local detachWeaponInstId = unpack(arg)
    if detachWeaponInstId ~= self.m_curPreviewWeaponInstId then
        return
    end

    phaseItem:ReloadWeaponDecoEffect(self.m_curPreviewWeaponInstId)
    phaseItem:SwitchWeaponState(weaponState, true)
end

PhaseCharInfo.OnWeaponAttachGemEnhanceMax = HL.Method(HL.Table) << function(self, arg)
    local weaponInstId = unpack(arg)
    if weaponInstId ~= self.m_curPreviewWeaponInstId then
        return
    end

    local phaseItem = self.m_charItem
    local weaponState = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE
    phaseItem:ReloadWeaponDecoEffect(self.m_curPreviewWeaponInstId)
    phaseItem:SwitchWeaponState(weaponState, true)
end

PhaseCharInfo.OnWeaponRefine = HL.Method(HL.Table) << function(self, args)
    local weaponInstId, refineLv = unpack(args)
    if weaponInstId == self.m_curPreviewWeaponInstId then
        local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)
        local maxRefineLv = CS.Beyond.Gameplay.WeaponUtil.GetWeaponMaxRefineLv(weaponInst.templateId)
        if maxRefineLv == refineLv then
            self:ReloadPreviewWeapon()
        end
        local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
        local weaponDecoNode = sceneObject.view.weaponDecoNode
        weaponDecoNode.potentialStar:InitWeaponPotentialStar(weaponInst.refineLv)
    end
end

PhaseCharInfo.OnPutOnWeapon = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshWeaponDeco({
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.WEAPON
    })
end

PhaseCharInfo.OnCharLevelUp = HL.Method(HL.Table) << function(self, arg)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]

    UIUtils.PlayAnimationAndToggleActive(sceneObject.view.charUpgradeEffect, true)
    sceneObject.view.charUpgradeEffect:PlayInAnimation()
end

PhaseCharInfo.OnCharPotentialUnlock = HL.Method(HL.Table) << function(self, args)
    local charInstId, level = unpack(args)
    local charPhaseItem = self:_GetCharPhaseItem(charInstId)
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if charPhaseItem and charInfo and charInfo.potentialLevel >= UIConst.CHAR_MAX_POTENTIAL then
        charPhaseItem:LoadPotentialEffects()
    end
end

PhaseCharInfo.OnCharMaxPotentialEffectToggled = HL.Method(HL.Table) << function(self, args)
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

PhaseCharInfo.OnPlayerCharBagAddNew = HL.StaticMethod() << function()
    FilterUtils.setCharInstIdIndexDirty()
end

PhaseCharInfo.ToggleWeaponFocusMode = HL.Method(HL.Boolean) << function(self, isOn)
    if self.m_curPage ~= UIConst.CHAR_INFO_PAGE_TYPE.WEAPON then
        return
    end
    self:_ToggleWeaponDeco(not isOn)
end


PhaseCharInfo._RefreshCharUpgradeDeco = HL.Method(HL.Number) << function(self, pageType)
    local isInUpgrade = pageType == UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]

    UIUtils.PlayAnimationAndToggleActive(sceneObject.view.charUpgradeDeco, isInUpgrade)
end

PhaseCharInfo._RefreshGridDeco = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    local pageType = arg.pageType
    local isOn = HIDE_GRID_PAGE_TYPE[pageType] == nil
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]

    UIUtils.PlayAnimationAndToggleActive(sceneObject.view.gridDeco, isOn)
end


PhaseCharInfo._RefreshWeaponDeco = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    local weaponInstId = arg.weaponInstId
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local weaponDecoNode = sceneObject.view.weaponDecoNode

    local weaponInfo
    local itemCfg, weaponTypeInt
    if not weaponInstId then
        weaponInfo = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId)
        itemCfg = weaponInfo.itemCfg
        local weaponCfg = weaponInfo.weaponCfg
        weaponTypeInt = weaponCfg.weaponType:ToInt()
    else
        weaponInfo = CharInfoUtils.getWeaponInstInfo(weaponInstId)
        itemCfg = weaponInfo.itemCfg
        local weaponCfg = weaponInfo.weaponCfg
        weaponTypeInt = weaponCfg.weaponType:ToInt()
    end

    weaponDecoNode.weaponName.text = itemCfg.name
    UIUtils.setItemRarityImage(weaponDecoNode.rarityColor, itemCfg.rarity)

    local spriteName = UIConst.WEAPON_EXHIBIT_WEAPON_TYPE_ICON_PREFIX .. weaponTypeInt
    weaponDecoNode.typeIcon:LoadSprite(UIConst.UI_SPRITE_WEAPON_EXHIBIT, spriteName)

    weaponDecoNode.potentialStar:InitWeaponPotentialStar(weaponInfo.weaponInst.refineLv)
    CSUtils.UIContainerResize(weaponDecoNode.starGroup.transform, itemCfg.rarity)
end

PhaseCharInfo._ToggleWeaponDeco = HL.Method(HL.Boolean, HL.Opt(HL.Number)) << function(self, isOn, beforePage)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local weaponDecoNode = sceneObject.view.weaponDecoNode

    self.m_weaponDecoEffectCor = PhaseManager:_ClearCoroutine(self.m_weaponDecoEffectCor)
    if isOn then
        if beforePage and beforePage == UIConst.CHAR_INFO_PAGE_TYPE.EQUIP then

            self.m_weaponDecoEffectCor = self:_StartCoroutine(function()
                coroutine.wait(0.5)
                UIUtils.PlayAnimationAndToggleActive(weaponDecoNode.animationWrapper, true)
            end)
        else
            if not weaponDecoNode.gameObject.activeSelf then
                UIUtils.PlayAnimationAndToggleActive(weaponDecoNode.animationWrapper, true)
            end
        end
    else
        UIUtils.PlayAnimationAndToggleActive(weaponDecoNode.animationWrapper, false)
    end
end

PhaseCharInfo._StartPreloadCor = HL.Method() << function(self)
    if self.m_preloadCor then
        return
    end


    self.m_preloadCor = self:_StartCoroutine(function()
        for i, panelId in ipairs(PANEL_PRELOAD_ORDER) do
            if UIManager:CheckPanelAssetHadLoaded(panelId) then
                logger.info(string.format("CharInfo->Panel[%s] already preloaded, skip", panelId))
            else
                logger.info(string.format("CharInfo->Preload panel [%s]", panelId))
                UIManager:PreloadPanelAsset(panelId, PHASE_ID)
                coroutine.wait(0.5)
            end
        end
    end)
end

PhaseCharInfo._RefreshVoiceTriggerVo = HL.Method(HL.Number) << function(self, pageType)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    local charInfo = self.m_charInfo
    if pageType == UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW then
        self.m_voiceCor = self:_ClearCoroutine(self.m_voiceCor)
        self.m_voiceCor = self:_StartCoroutine(function()
            coroutine.wait(sceneObject.view.config.VOICE_IDLE_TRIGGER_DURATION)
            Utils.triggerVoice("chrbark_idle", charInfo.templateId)
        end)
    else
        self.m_voiceCor = PhaseManager:_ClearCoroutine(self.m_voiceCor)
    end
end


PhaseCharInfo._TriggerCharBarkSwitch = HL.Method(HL.Number) << function(self, pageType)
    local charInfo = self.m_charInfo
    if pageType == UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW then
        Utils.stopDefaultChannelVoice()
        Utils.triggerVoice("chrbark_switch", charInfo.templateId)
    end
end




PhaseCharInfo.m_isPotentialSceneInited = HL.Field(HL.Boolean) << false

PhaseCharInfo.m_isPotentialCameraFocused = HL.Field(HL.Boolean) << false

PhaseCharInfo.m_potentialStarEffects = HL.Field(HL.Table)

PhaseCharInfo.m_potentialMaxEffects = HL.Field(HL.Table)

PhaseCharInfo.m_potentialPhotoEffects = HL.Field(HL.Table)

PhaseCharInfo.m_potentialPicIds = HL.Field(HL.Table)

PhaseCharInfo.m_potentialResLoader = HL.Field(HL.Forward("LuaResourceLoader"))

PhaseCharInfo.s_debugPotentialFx = HL.StaticField(HL.Boolean) << false

PhaseCharInfo._InitPotentialScene = HL.Method() << function(self)
    if self.m_isPotentialSceneInited then
        return
    end
    self.m_isPotentialSceneInited = true
    self:CreatePhaseGOItem(PHASE_CHAR_INFO_POTENTIAL_SCENE)
    self.m_potentialResLoader = require_ex('Common/Utils/LuaResourceLoader').LuaResourceLoader()

    self.m_isPotentialCameraFocused = false
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    sceneObject.view.camera.root.gameObject:SetAllChildrenActiveIfNecessary(false)
    sceneObject.view.light.root.gameObject:SetActive(false)


    local sceneRoot = sceneObject.view.scene.transform

    local decoCfg
    for _, cfg in pairs(Tables.charPotentialDecoTable) do
        if not string.isEmpty(cfg.missionId) and GameInstance.player.mission:GetMissionState(cfg.missionId) ==
            CS.Beyond.Gameplay.MissionSystem.MissionState.Processing then
            decoCfg = cfg
            break
        end
    end
    if decoCfg then
        for _, replaceCfg in ipairs(CharPotentialConst.DecoItemReplaceCfg) do
            local newDecoName = decoCfg[replaceCfg.cfgName]
            local newAsset = self.m_resourceLoader:LoadGameObject(string.format(CharPotentialConst.DecoItemPathFormat, newDecoName))
            if newAsset then
                local newItem = CSUtils.CreateObject(newAsset, sceneRoot)
                newItem.name = newDecoName
                newItem:SetLayerRecursive(UIConst.UI_MODEL_LAYER)
                local oldItem = sceneRoot:Find(replaceCfg.decoName)
                if oldItem then
                    local posOffset = Vector3.zero
                    if replaceCfg.posOffset and replaceCfg.posOffset[newDecoName] then
                        posOffset = replaceCfg.posOffset[newDecoName]
                    end
                    newItem.transform.localPosition = oldItem.localPosition + posOffset
                    if not replaceCfg.ignoreRotate then
                        newItem.transform.localRotation = oldItem.localRotation
                    end
                    newItem.transform.localScale = oldItem.localScale
                    GameObject.Destroy(oldItem.gameObject)
                end
            end
        end
        local newAC = self.m_resourceLoader:LoadAnimatorController(string.format(CharPotentialConst.DecoACPathFormat, decoCfg.animatorId))
        if newAC then
            sceneObject.view.scene.animator.runtimeAnimatorController = newAC
        end





    end

    local sceneDeco = sceneObject.view.deco
    sceneDeco.canvas.worldCamera = CameraManager.mainCamera
    for _, level in ipairs(UIConst.CHAR_PHOTO_POTENTIAL_LEVELS) do
        local photoNode = sceneDeco[string.format("photoNode%d", level)]
        photoNode.btnZoom.onClick:AddListener(function()
            if UNITY_EDITOR and PhaseCharInfo.s_debugPotentialFx then
                self:RefreshPotentialPhoto(self.m_charInfo.instId, level)
                return
            end
            self:_ShowPhoto(level)
        end)
        photoNode.btnView.onClick:AddListener(function()
            if UNITY_EDITOR and PhaseCharInfo.s_debugPotentialFx then
                self:RefreshPotentialPhoto(self.m_charInfo.instId, level)
                return
            end
            self:_ShowPhoto(level)
        end)
        photoNode.btnView.gameObject:SetActive(false)
        local photoMeshRenderer = sceneObject.view.scene[string.format("photo%d", level)]
        if not IsNull(photoMeshRenderer) then
            photoMeshRenderer.sharedMaterial = photoMeshRenderer:GetInstantiatedMaterial()
        end
        local photoMeshRenderMulti = sceneObject.view.scene[string.format("photo%d_multi", level)]
        if not IsNull(photoMeshRenderMulti) then
            photoMeshRenderMulti.sharedMaterial = photoMeshRenderer:GetInstantiatedMaterial()
        end
    end
    sceneDeco.btnViewDetails.onClick:AddListener(function()

        local potentialCtrl = self:_GetPanelPhaseItem(PanelId.CharInfoPotential).uiCtrl
        potentialCtrl:_ActiveLevelUp(true)
    end)

    for btnName, trigger in pairs(CharPotentialConst.TriggerConfig) do
        local btn = sceneDeco.itemBtnNode[btnName]
        if btn then
            btn.onClick:AddListener(function()
                sceneObject.view.scene.animator:SetTrigger(trigger)
            end)
        end
    end
    self.m_potentialStarEffects = {}
    for i = 1, UIConst.CHAR_MAX_POTENTIAL do
        self.m_potentialStarEffects[i] = {}
    end
    self.m_potentialMaxEffects = {}
    self.m_potentialPhotoEffects = {}
    for i, _ in pairs(CharPotentialConst.PhotoEffectConfig) do
        self.m_potentialPhotoEffects[i] = {}
    end
    self.m_potentialPicIds = {}
end

PhaseCharInfo.RefreshPotentialStar = HL.Method(HL.Number, HL.Number) << function(self, curLv, maxLv)
    local isMax = curLv == maxLv

    for i = 1, maxLv do
        local cfg = CharPotentialConst.StarEffectConfig[i]
        local effects = self.m_potentialStarEffects[i]
        self:_ClearPotentialEffects(effects)
        if i <= curLv then
            local mountPoint = self:_GetPotentialMountPoint(cfg.mountPoint)
            local effectName = isMax and cfg.loopMaxEffectName or cfg.loopEffectName
            local effect = GameInstance.effectManager:CreateEffectOnTransform(effectName, mountPoint)
            if effect:Lock() then
                effect:Lock():LoadImmediately()
                effect:Lock():SetEffectOriginalLayer(UIConst.UI_MODEL_LAYER)
            end
            table.insert(effects, effect)
        end
    end

    self:_ClearPotentialEffects(self.m_potentialMaxEffects)
    if isMax then
        for _, cfg in pairs(CharPotentialConst.StarEffectConfig.MaxLoop) do
            local mountPoint = self:_GetPotentialMountPoint(cfg.mountPoint)
            local effect = GameInstance.effectManager:CreateEffectOnTransform(cfg.effectName, mountPoint)
            if effect:Lock() then
                effect:Lock():LoadImmediately()
                effect:Lock():SetEffectOriginalLayer(UIConst.UI_MODEL_LAYER)
            end
            table.insert(self.m_potentialMaxEffects, effect)
        end
    end
end

PhaseCharInfo.UnlockPotentialStar = HL.Method(HL.Number, HL.Number) << function(self, unlockedLv, maxLv)
    local isMax = unlockedLv == maxLv
    if isMax then
        for i = 1, maxLv do
            local cfg = CharPotentialConst.StarEffectConfig[i]
            local effects = self.m_potentialStarEffects[i]
            self:_ClearPotentialEffects(effects)
            local mountPoint = self:_GetPotentialMountPoint(cfg.mountPoint)
            local effectName = cfg.unlockMaxEffectName
            local effect = GameInstance.effectManager:CreateEffectOnTransform(effectName, mountPoint)
            if effect:Lock() then
                effect:Lock():LoadImmediately()
                effect:Lock():SetEffectOriginalLayer(UIConst.UI_MODEL_LAYER)
            end
            table.insert(effects, effect)
        end
        self:_ClearPotentialEffects(self.m_potentialMaxEffects)
        for _, cfg in pairs(CharPotentialConst.StarEffectConfig.Max) do
            local mountPoint = self:_GetPotentialMountPoint(cfg.mountPoint)
            local effect = GameInstance.effectManager:CreateEffectOnTransform(cfg.effectName, mountPoint)
            if effect:Lock() then
                effect:Lock():LoadImmediately()
                effect:Lock():SetEffectOriginalLayer(UIConst.UI_MODEL_LAYER)
            end
            table.insert(self.m_potentialMaxEffects, effect)
        end
    else
        local cfg = CharPotentialConst.StarEffectConfig[unlockedLv]
        local effects = self.m_potentialStarEffects[unlockedLv]
        self:_ClearPotentialEffects(effects)
        local mountPoint = self:_GetPotentialMountPoint(cfg.mountPoint)
        local effectName = cfg.unlockEffectName
        local effect = GameInstance.effectManager:CreateEffectOnTransform(effectName, mountPoint)
        if effect:Lock() then
            effect:Lock():LoadImmediately()
            effect:Lock():SetEffectOriginalLayer(UIConst.UI_MODEL_LAYER)
        end
        table.insert(effects, effect)
    end
end

PhaseCharInfo._GetPotentialMountPoint = HL.Method(HL.String).Return(HL.Userdata) << function(self, name)
    if name == "Camera" then
        return CameraManager.mainCamera.transform
    else
        return self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE].view.scene[name]
    end
end

PhaseCharInfo._ClearPotentialEffects = HL.Method(HL.Table) << function(self, effects)
    for i, effect in pairs(effects) do

        if effect:Lock() then
            effect:Lock():DestroyImmediate()
        end
        effects[i] = nil
    end
end

PhaseCharInfo.RefreshPotentialSceneDeco = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, charInstId, playInAnim)

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if not charInfo then
        return
    end

    local templateId = charInfo.templateId
    local _, charData = Tables.characterTable:TryGetValue(templateId)
    local sceneView = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE].view
    local sceneDeco = sceneView.deco
    sceneDeco.txtCharName.text = charData.engName
    sceneDeco.txtCardTitle.text = string.format("// %s", charData.department)
    local spriteName = UIConst.UI_CHAR_HEAD_SQUARE_PREFIX .. templateId
    sceneDeco.imgChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD_RECTANGLE, spriteName)

    for _, effects in pairs(self.m_potentialPhotoEffects) do
        self:_ClearPotentialEffects(effects)
    end
    self.m_potentialResLoader:DisposeAllHandles(true)
    self:RefreshPotentialPhoto(charInstId)
    self:RefreshPotentialDecoBtn()

    if playInAnim then
        sceneDeco.cardNode:ClearTween()
        sceneDeco.cardNode:PlayInAnimation()
    end
end

PhaseCharInfo.RefreshPotentialPhoto = HL.Method(HL.Number, HL.Opt(HL.Number)).Return(HL.Any) << function(self, charInstId, targetLv)

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if not charInfo then
        return false
    end
    local templateId = charInfo.templateId
    local _, potentialData = Tables.characterPotentialTable:TryGetValue(templateId)
    if not potentialData then
        return false
    end
    local isDevAvailable = CharInfoUtils.isCharDevAvailable(charInfo.instId)
    local isChanged = false
    local sceneView = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE].view
    local sceneDeco = sceneView.deco
    for _, level in ipairs(UIConst.CHAR_PHOTO_POTENTIAL_LEVELS) do
        local photoNode = sceneDeco[string.format("photoNode%d", level)]

        local pictureItemList = potentialData.potentialUnlockBundle[CSIndex(level)].unlockCharPictureItemList
        local isMulti = #pictureItemList > 1
        local suffix = isMulti and "_multi" or ""
        local photoMRSingle = sceneView.scene[string.format("photo%d", level)]
        local photoMRMulti = sceneView.scene[string.format("photo%d_multi", level)]
        photoMRSingle.gameObject:SetActive(not isMulti)
        photoMRMulti.gameObject:SetActive(isMulti)
        local photoMeshRenderer = isMulti and photoMRMulti or photoMRSingle

        if not IsNull(photoMeshRenderer) then
            local pictureId
            if charInfo.potentialCgIds then
                _, pictureId  = charInfo.potentialCgIds:TryGetValue(level)
            end
            if string.isEmpty(pictureId) and #pictureItemList > 0 then
                local itemId = pictureItemList[0]
                _, pictureId = Tables.pictureItemTable:TryGetValue(itemId)
            end
            local isUnlocked = charInfo.potentialLevel >= level and not string.isEmpty(pictureId)
            photoNode.gameObject:SetActive(isUnlocked and isDevAvailable)
            photoMeshRenderer.gameObject:SetActive(isUnlocked)

            if isUnlocked then
                photoNode.redDot:InitRedDot("CharInfoPotentialPicture", {
                    charInstId = charInstId,
                    potentialLevel = level,
                })

                local _, posterData = Tables.pictureTable:TryGetValue(pictureId)
                if not posterData then
                    logger.error("立绘数据不存在:"..pictureId)
                    return
                end

                local effects = self.m_potentialPhotoEffects[level]
                local effectConfig = CharPotentialConst.PhotoEffectConfig[level]

                local isPicChanged = self.m_potentialPicIds[level] ~= pictureId
                if UNITY_EDITOR and PhaseCharInfo.s_debugPotentialFx then
                    isPicChanged = true
                end

                if effectConfig then
                    if targetLv then
                        if level == targetLv and isPicChanged then
                            self:_ClearPotentialEffects(effects)
                            AudioAdapter.PostEvent("Au_UI_Event_CharPotentialPhotoActivate")
                            local mountPoint = sceneView.scene[effectConfig.mountPoint]
                            local renderHelper = sceneView.scene.photoRenderHelper
                            if mountPoint and renderHelper then
                                for _, effectName in ipairs(effectConfig.effectNames) do
                                    local effectInst = GameInstance.effectManager:CreateEffectOnTransform(effectName, mountPoint)
                                    if effectInst:Lock() then
                                        effectInst:Lock():LoadImmediately()
                                        effectInst:Lock():SetEffectOriginalLayer(UIConst.UI_MODEL_LAYER)
                                    end
                                    table.insert(effects, effectInst)
                                end
                                for _, vfxName in ipairs(effectConfig.vfxNames) do
                                    local effectInst = GameInstance.effectManager:CreateVFXEffectOnTransform(vfxName, renderHelper)
                                    if effectInst:Lock() then
                                        effectInst:Lock():LoadImmediately()
                                        effectInst:Lock():SetEffectOriginalLayer(UIConst.UI_MODEL_LAYER)
                                    end
                                    table.insert(effects, effectInst)
                                end
                            end
                            isChanged = true
                        end
                    else
                        local mountPoint = sceneView.scene[effectConfig.mountPoint]
                        local effectInst = GameInstance.effectManager:CreateEffectOnTransform(effectConfig.shadowEffectName, mountPoint)
                        if effectInst:Lock() then
                            effectInst:Lock():LoadImmediately()
                            effectInst:Lock():SetEffectOriginalLayer(UIConst.UI_MODEL_LAYER)
                        end
                        table.insert(effects, effectInst)
                    end
                end

                local texture = self.m_potentialResLoader:LoadTexture(string.format(UIConst.POSTER_TEXTURE_PATH, posterData.imgId))
                photoMeshRenderer.sharedMaterial:SetTexture("_BaseColorMap", texture)
            end
            self.m_potentialPicIds[level] = isUnlocked and pictureId or nil
        end
    end
    return isChanged
end

PhaseCharInfo._SetActivePotentialItems = HL.Method(HL.Boolean) << function(self, active)
    local potentialScene = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    if not potentialScene or IsNull(potentialScene.view.gameObject) then
        return
    end
    potentialScene.view.light.root.gameObject:SetActive(active)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_GAME_OBJECT]
    sceneObject.view.lightRoot.gameObject:SetActive(not active)
    local cameraView = potentialScene.view.camera
    cameraView.vcam_char_potential_normal.gameObject:SetActive(active)
    cameraView.vcam_char_potential_top.gameObject:SetActive(false)
    if active then
        local sceneDeco = potentialScene.view.deco
        sceneDeco.animationWrapper:ClearTween()
        sceneDeco.animationWrapper:PlayInAnimation()
    else
        for _, effects in pairs(self.m_potentialStarEffects) do
            self:_ClearPotentialEffects(effects)
        end
        self:_ClearPotentialEffects(self.m_potentialMaxEffects)
        for _, effects in pairs(self.m_potentialPhotoEffects) do
            self:_ClearPotentialEffects(effects)
        end
        self.m_potentialResLoader:DisposeAllHandles(true)
    end
end

PhaseCharInfo._ShowPhoto = HL.Method(HL.Number) << function(self, potentialLevel)

    local potentialCtrl = self:_GetPanelPhaseItem(PanelId.CharInfoPotential).uiCtrl
    potentialCtrl:ShowPhotoByLevel(potentialLevel)
end

PhaseCharInfo.m_blendPotentialTween = HL.Field(HL.Userdata)

PhaseCharInfo.BlendCameraPotential = HL.Method(HL.Userdata, HL.Boolean, Vector3, HL.Number).Return(HL.Userdata) << function(
    self, curActiveCam, isIn, offset, duration)
    local cameraOffset = CSUtils.GetOrAddCinemachineCameraOffset(curActiveCam)
    cameraOffset.m_Offset = isIn and Vector3.zero or offset
    self.m_blendPotentialTween = DOTween.To(function()
        return cameraOffset.m_Offset
    end, function(value)
        cameraOffset.m_Offset = value
    end, isIn and offset or Vector3.zero, duration)
    return cameraOffset
end

PhaseCharInfo.ActivePotentialBlendCamera = HL.Method(HL.Boolean) << function(self, active)
    local potentialScene = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    potentialScene.view.camera.vcam_char_potential_blend.gameObject:SetActive(active)
end

PhaseCharInfo.m_potentialCameraBlendTween = HL.Field(HL.Userdata)

PhaseCharInfo.ActivePotentialFocusCamera = HL.Method(HL.Boolean) << function(self, isActive)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    if sceneObject == nil or IsNull(sceneObject.view.gameObject) then
        return
    end
    sceneObject.view.camera.vcam_char_potential_top.gameObject:SetActive(isActive)
    self.m_isPotentialCameraFocused = isActive
    self:RefreshPotentialDecoBtn()
end

PhaseCharInfo.ActivePotentialPhotoCamera = HL.Method(HL.Number, HL.Boolean) << function(self, potentialLevel, isActive)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    local photoCam = sceneObject.view.camera[string.format("vcam_char_potential_photo%d", potentialLevel)]
    if photoCam then
        photoCam.gameObject:SetActive(isActive)
    end
end

PhaseCharInfo.RefreshPotentialDecoBtn = HL.Method() << function(self)
    local isActive = self.m_isPotentialCameraFocused
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInfo.instId)
    local isDevAvailable = CharInfoUtils.isCharDevAvailable(self.m_charInfo.instId)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    sceneObject.view.deco.btnViewDetails.gameObject:SetActive(not isActive)
    sceneObject.view.deco.btnViewPhoto.gameObject:SetActive(DeviceInfo.usingController and not isActive and
        charInfo.potentialLevel >= UIConst.CHAR_PHOTO_POTENTIAL_LEVELS[1] and isDevAvailable)
    self:RefreshFocusPhotoBtn()
end

PhaseCharInfo.RefreshFocusPhotoBtn = HL.Method() << function(self)
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInfo.instId)
    local isDevAvailable = CharInfoUtils.isCharDevAvailable(self.m_charInfo.instId)

    local potentialPanelItem = self:_GetPanelPhaseItem(PanelId.CharInfoPotential)
    local sceneObject = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE]
    sceneObject.view.deco.btnFocusPhoto.gameObject:SetActive(DeviceInfo.usingController and
        self.m_isPotentialCameraFocused and potentialPanelItem and not potentialPanelItem.uiCtrl.isPhotoMode and
        charInfo.potentialLevel >= UIConst.CHAR_PHOTO_POTENTIAL_LEVELS[1] and isDevAvailable)
end

PhaseCharInfo.NaviToPotentialPhoto = HL.Method(HL.Number) << function(self, potentialLevel)
    local decoView = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE].view.deco
    local photoNode = decoView[string.format("photoNode%d", potentialLevel)]
    if not photoNode then
        photoNode = decoView.photoNode1
    end
    self:_GetPanelPhaseItem(PanelId.CharInfoPotential):SetNaviTarget(photoNode.btnZoom)
end

PhaseCharInfo.StopNaviPotentialPhoto = HL.Method() << function(self)
    local decoView = self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE].view.deco
    InputManagerInst.controllerNaviManager:TryRemoveLayer(decoView.viewPhotoNaviGroup)
end

PhaseCharInfo.GetPotentialDecoView = HL.Method().Return(HL.Table) << function(self)
    return self.m_gameObject2Item[PHASE_CHAR_INFO_POTENTIAL_SCENE].view.deco
end





PhaseCharInfo.ShowCharExpandList = HL.Method(HL.Table) << function(self, args)
    self:CreateOrShowPhasePanelItem(PanelId.CharExpandList, args)
    UIManager:SetTopOrder(PanelId.CharExpandList)
end

PhaseCharInfo.HideCharExpandList = HL.Method() << function(self)
    local panelItem = self:_GetPanelPhaseItem(PanelId.CharExpandList)
    if panelItem then
        panelItem.uiCtrl:PlayAnimationOutAndHide()
    end
end

PhaseCharInfo.RefreshCharExpandList = HL.Method(HL.Table, HL.Table) << function(self, charInfo, charInfoList)
    local panelItem = self:_GetPanelPhaseItem(PanelId.CharExpandList)
    if panelItem then
        panelItem.uiCtrl:RefreshCharExpandList(charInfo, charInfoList, true)
    end
end





PhaseCharInfo.m_initCharInfoList = HL.Field(HL.Table)

PhaseCharInfo.m_maxCharInfoList = HL.Field(HL.Table)

PhaseCharInfo._ProcessPreviewData = HL.Method(HL.Table) << function(self, initCharInfo)
    if initCharInfo.maxCharInstIdList then
        self.m_initCharInfoList = self.m_charInfoList
        self.m_maxCharInfoList = CharInfoUtils.getCharInfoListByInstIdList(initCharInfo.maxCharInstIdList, initCharInfo.isShowPreview)
        if self.arg.stateArg and self.arg.stateArg.isInitPreview then
            return
        end

        self.m_charInfoList = self.m_maxCharInfoList
        for _, charInfo in pairs(self.m_maxCharInfoList) do
            if charInfo.templateId == initCharInfo.templateId then
                initCharInfo.instId = charInfo.instId
                break
            end
        end
    end
end

PhaseCharInfo.ToggleInitMaxState = HL.Method(HL.Boolean) << function(self, isInit)
    self.m_isInitPreview = isInit
    if isInit then
        self.m_charInfoList = self.m_initCharInfoList
    else
        self.m_charInfoList = self.m_maxCharInfoList
    end

    local charInstIdList = {}
    for _, charInfo in pairs(self.m_charInfoList) do
        if charInfo.templateId == self.m_charInfo.templateId then
            self.m_charInfo = charInfo
        end
        table.insert(charInstIdList, charInfo.instId)
    end

    self.m_charInfo.charInstIdList = charInstIdList

    local function reloadWeapon()
        self.m_charItem.charInstId = self.m_charInfo.instId
        self.m_curPreviewWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
        self.m_charItem:ReloadWeapon()
    end

    if self.m_charItemInitComplete then
        reloadWeapon()
    else
        self.m_actionsAfterCharItemInitCompleted:Push(reloadWeapon)
    end
end





local LOCK_RESOLUTION_HEIGHT = 720
local RENDERING_SCALE_NAME = "renderingScale"

PhaseCharInfo.ActiveRenderScaleLock = HL.Method(HL.Boolean) << function(self, isLocked)
    if DeviceInfo.usingTouch then

        local settingHub = CS.HG.Rendering.Runtime.HGRenderPipelineSettingHub.instance
        if isLocked then

            local settingParameters = CS.HG.Rendering.Runtime.HGRenderPipeline.currentPipeline.settingParameters
            local currentScale = settingParameters.renderingScale.paramValue
            local lockedScale = LOCK_RESOLUTION_HEIGHT / settingParameters.taauResolveResolutionHeight.paramValue
            if currentScale < lockedScale then
                settingHub:OverrideSettingParameter(RENDERING_SCALE_NAME, lockedScale)
            end
        else
            settingHub:ResetSettingParameter(RENDERING_SCALE_NAME)
        end
        CS.HG.Rendering.Runtime.HGRenderPipeline.currentPipeline.needRenderTerrain = not isLocked
    end
end





PhaseCharInfo._ActiveEquipPageNavi = HL.Method(HL.Userdata, HL.Boolean) << function(self, equipCtrl, isActive)
    local charInfoPanelItem = self:_GetPanelPhaseItem(PanelId.CharInfo)
    if not charInfoPanelItem or not charInfoPanelItem.uiCtrl then
        return
    end

    local charInfoCtrl = charInfoPanelItem.uiCtrl
    charInfoCtrl.view.menuListNodeNaviGroup:TryChangeNaviPartnerOnLeft(equipCtrl.view.centerNodeNaviGroup, isActive)
    local pageTabCellCache = charInfoCtrl.m_tabCellCache
    equipCtrl.view.equipEDC_1.button:SetExplicitSelectOnRight(isActive and pageTabCellCache:Get(1).button or nil)
    equipCtrl.view.equipEDC_2.button:SetExplicitSelectOnRight(isActive and pageTabCellCache:Get(2).button or nil)
    equipCtrl.view.equipTactical.button:SetExplicitSelectOnRight(isActive and pageTabCellCache:Get(3).button or nil)


    local equipTab = pageTabCellCache:Get(3).button
    equipTab.useExplicitNaviSelect = isActive
    equipTab.banExplicitOnDown = isActive
    equipTab.banExplicitOnUp = isActive
    equipTab.banExplicitOnRight = isActive
    equipTab.banExplicitOnLeft = false
    equipTab:SetExplicitSelectOnLeft(isActive and equipCtrl.view.equipEDC_2.button or nil)
end

PhaseCharInfo._ActiveWeaponPageNavi = HL.Method(HL.Userdata, HL.Boolean) << function(self, weaponCtrl, isActive)
    local charInfoPanelItem = self:_GetPanelPhaseItem(PanelId.CharInfo)
    if not charInfoPanelItem or not charInfoPanelItem.uiCtrl then
        return
    end

    local charInfoCtrl = charInfoPanelItem.uiCtrl
    charInfoCtrl.view.menuListNodeNaviGroup:TryChangeNaviPartnerOnLeft(weaponCtrl.view.focusMasteryNaviGroup, isActive)
    weaponCtrl.view.focusMasteryNaviGroup:TryChangeNaviPartnerOnRight(charInfoCtrl.view.menuListNodeNaviGroup, isActive)
end



PhaseCharInfo.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    arg.pageType = self.m_curPage

    if not arg.initCharInfo then
        arg.initCharInfo = {}
    end
    arg.initCharInfo.instId = self.m_charInfo.instId
    arg.initCharInfo.templateId = self.m_charInfo.templateId

    arg.forceSkipIn = true
    local stateArg = {}
    arg.stateArg = stateArg
    stateArg.isInitPreview = self.m_isInitPreview
    stateArg.openProAndElement = UIManager:IsOpen(PanelId.CharInfoProAndElement)
    stateArg.openFullAttr = UIManager:IsOpen(PanelId.CharInfoFullAttribute)
    stateArg.isExpandNodeOn = UIManager:IsShow(PanelId.CharExpandList)
    for _, panelItem in pairs(self.m_panel2Item) do
        if HL.TryGet(panelItem.uiCtrl, "GetCurStateArg") then
            local arg = panelItem.uiCtrl:GetCurStateArg()
            lume.extend(stateArg, arg)
        end
    end
    return arg
end

PhaseCharInfo._ProcessStateArg = HL.Method(HL.Table) << function(self, arg)
    if not arg then
        return
    end
    if arg.openProAndElement then
        UIManager:Open(PanelId.CharInfoProAndElement)
    end
    if arg.openFullAttr then
        local charInfoItem = self:_GetPanelPhaseItem(PanelId.CharInfo)
        if charInfoItem then
            charInfoItem.uiCtrl.view.charInfoBasicNodeLeft.view.attributeTitle.detailButton.onClick:Invoke()
        end
    end
    if arg.isExpandNodeOn then
        local charInfoItem = self:_GetPanelPhaseItem(PanelId.CharInfo)
        if charInfoItem then
            charInfoItem.uiCtrl.animationWrapper:SkipInAnimation()
            charInfoItem.uiCtrl:_ToggleExpandNode(true)
        end
    end

    self.m_actionsAfterCharItemInitCompleted:Push(function()

        AudioAdapter.PostEvent("au_global_ui_magnifier_panel_enter")
    end)
end


HL.Commit(PhaseCharInfo)