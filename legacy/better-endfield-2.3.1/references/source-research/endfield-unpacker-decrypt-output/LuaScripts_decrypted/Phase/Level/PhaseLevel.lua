local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.Level
local HGCamera = CS.HG.Rendering.Runtime.HGCamera
local PhaseLevelConfig = require_ex("Phase/Level/PhaseLevelConfig")

PhaseLevel = HL.Class('PhaseLevel', phaseBase.PhaseBase)







PhaseLevel.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.OPEN_LEVEL_PHASE] = { 'OpenLevelPhase', false },
    [MessageConst.ON_SCENE_LOAD_START] = { 'onSceneLoadStart', true },
    [MessageConst.ON_SQUAD_INFIGHT_CHANGED] = {'OnSquadInfightChanged', true },

    [MessageConst.ON_EXIT_TRAVEL_MODE] = {'OnExitTravelMode', true },

    [MessageConst.SET_PHASE_LEVEL_TRANSITION_RESERVE_PANELS] = {'SetPhaseLevelTransitionReservePanels', true },

    [MessageConst.RECOVER_PHASE_LEVEL] = {'RecoverPhaseLevel', true },
    [MessageConst.ON_SCENE_REPATRIATE_NEED_UNSTUCK] = {'OnSceneRepatriateNeedUnstuck', true },


    [MessageConst.ON_ENTER_TOWER_DEFENSE_PREPARING_PHASE ] = { 'OnEnterTowerDefensePreparingPhase', true },
    [MessageConst.ON_LEAVE_TOWER_DEFENSE_PREPARING_PHASE] = { 'OnLeaveTowerDefensePreparingPhase', true },
    [MessageConst.ON_ENTER_TOWER_DEFENSE_DEFENDING_PHASE] = { 'OnEnterTowerDefenseDefendingPhase', true },
    [MessageConst.ON_TOWER_DEFENSE_TRANSIT_FINISHED] = { 'OnTowerDefenseDefendingTransitFinished', true },
    [MessageConst.ON_LEAVE_TOWER_DEFENSE_DEFENDING_PHASE] = { 'OnLeaveTowerDefenseDefendingPhase', true },
    [MessageConst.ON_TOWER_DEFENSE_LEVEL_REWARDS_FINISHED] = { 'OnTowerDefenseDefendingRewardsFinished', true },



    [MessageConst.ON_START_DOMAIN_DEPOT_DELIVER] = { 'OnStartDomainDepotDeliver', true },
    [MessageConst.ON_FINISH_DOMAIN_DEPOT_DELIVER] = { 'OnFinishDomainDepotDeliver', true },


    [MessageConst.GAME_MODE_ENABLE] = { 'OnGameModeEnable', true },
    [MessageConst.GAME_MODE_DISABLE] = { '_OnGameModeDisable', true },

    [MessageConst.SET_FAC_TOP_VIEW_CUSTOM_RANGE] = { 'SetFacTopViewCustomRange', true },

    [MessageConst.FAC_ON_MODIFY_CHAPTER_SCENE] = { 'ForceUpdateMainRegionInfo', true },

    [MessageConst.ON_RESET_BLACKBOX] = { 'OnResetBlackbox', true },
    [MessageConst.FORBID_SYSTEM_CHANGED] = { 'OnForbidSystemChanged', true },

    [MessageConst.FORCE_ENABLE_UI_SCENE_BLUR] = { 'OnForceEnableUISceneBlur', true },

    [MessageConst.TOGGLE_IN_MAIN_HUD_STATE] = { 'OnToggleInMainHudMessageNotified', true },

    [MessageConst.ON_INPUT_DEVICE_TYPE_CHANGED] = { 'OnInputDeviceTypeChanged', true },
    [MessageConst.ON_REPATRIATE] = { 'OnRepatriate', true },

    [MessageConst.CURRENT_LEVEL_CHANGE] = { 'OnCurrentLevelChanged', true },
    [MessageConst.ON_GAME_LEVEL_LOADING_FINISH] = { 'OnGameLevelLoadingFinish', true },
    [MessageConst.ON_GACHA_POOL_ALL_REWARDS_SHOWN] = { '_OnGachaPoolAllRewardsShown', true },

    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = { '_OnSystemUnlockForImportantActivity', true },
}


PhaseLevel.OpenLevelPhase = HL.StaticMethod() << function()
    if LuaSystemManager.uiRestoreSystem:HasValidAction() then
        LuaSystemManager.uiRestoreSystem:TryRestore()
    else
        if not PhaseManager:IsOpen(PHASE_ID) then
            PhaseManager:OpenPhaseFast(PHASE_ID)
        elseif GameWorld.levelLoader.isSeamlessLoading then
            Notify(MessageConst.ON_OPEN_LEVEL_PHASE_SEAMLESS_LOADING)
        end
    end
end






PhaseLevel.m_updateKey = HL.Field(HL.Number) << -1

PhaseLevel.m_headLabelCtrl = HL.Field(HL.Forward("HeadLabelCtrl"))

PhaseLevel.m_missionTrackerPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseLevel.m_generalTrackerPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseLevel.s_forceTransitionBehindFastMode = HL.StaticField(HL.Boolean) << false





PhaseLevel._OnInit = HL.Override() << function(self)
    PhaseLevel.Super._OnInit(self)
    self:_InitInMainHudMessageList()
end

PhaseLevel.onSceneLoadStart = HL.Method(HL.Any) << function(self, arg)
end

PhaseLevel.OpenLevelPanels = HL.Method() << function(self)

    UIManager:Open(PanelId.LevelCamera)

    local config = PhaseLevelConfig.GetCurrentConfig()

    for _, panelId in ipairs(config.open) do
        if panelId == PanelId.MissionHud and (GameInstance.mode.hideMissionHud or
                UIManager:IsShow(PanelId.CommonTaskTrackHud) or UIManager:IsShow(PanelId.SimulationTrainingTrackHud)) then


        else
            if not UIManager:IsOpen(panelId) then
                UIManager:Open(panelId)
            end
        end
    end

    for _, panelId in ipairs(config.preload) do
        UIManager:PreloadPersistentPanelAsset(panelId)
    end

    for _, panelId in ipairs(config.specialPanels) do
        if panelId == PanelId.GeneralTracker then
            self.m_generalTrackerPanel = self:CreatePhasePanelItem(PanelId.GeneralTracker)
        end
        if panelId == PanelId.GeneralTracker then
            local radio = UIManager:AutoOpen(PanelId.Radio)
            radio:Hide()
        end
    end


    self.m_headLabelCtrl = UIManager:AutoOpen(PanelId.HeadLabel)

    self:_UpdateFactoryMode(true)
    self:_ShowDomainDepotPackHudPanelIfNeed()

    self:OnGameModeEnable({ GameInstance.mode.modeType, GameInstance.mode })
    self:_RefreshUIForbidState()

    if config.preOpen and not InputManagerInst.inChangingInputDevice then

        for _, panelId in ipairs(config.preOpen) do
            if not UIManager:IsOpen(panelId) then
                UIManager:Open(panelId)
                UIManager:Hide(panelId)
            end
        end
    end


    if Utils.isSystemUnlocked(GEnums.UnlockSystemType.Activity) then
        UIManager:AutoOpen(PanelId.ActivityStartReminder)
    end
end

local BlackBoxGuideNeedClosePanel = {
    PanelId.DungeonInfoPopup,
    PanelId.BlackBoxTargetAndReward,
    PanelId.CommonPopUp,
    PanelId.FacUnloaderSelect,
    PanelId.RewardsPopUpForBlackBox,
    PanelId.Formula,
    PanelId.CommonShare,
    PanelId.ControllerSideMenu,
}

PhaseLevel.RecoverPhaseLevel = HL.Method() << function(self)
    if PhaseManager.m_transCor then




        local key = CS.Beyond.Network.NetworkMask.instance:AddMask("PhaseLevel.RecoverPhaseLevel")
        CoroutineManager:StartCoroutine(function()
            while true do
                coroutine.step()
                if not PhaseManager.m_transCor then
                    CS.Beyond.Network.NetworkMask.instance:RemoveMask(key)
                    self:RecoverPhaseLevel()
                    return
                end
            end
        end)
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        GameWorld.dialogManager:Clear()
        PhaseManager:ExitPhaseFastTo(PhaseId.Level, true)
    end

    for _, panelId in pairs(BlackBoxGuideNeedClosePanel) do
        if UIManager:IsOpen(panelId) then
            UIManager:Close(panelId)
        end
    end
end




PhaseLevel.m_lastLevelIdNum = HL.Field(HL.Number) << -1

PhaseLevel.m_lastInFacMainRegion = HL.Field(HL.Boolean) << false

PhaseLevel.mainRegionPanelIndex = HL.Field(HL.Number) << -1

PhaseLevel.mainRegionLocalRect = HL.Field(CS.UnityEngine.Rect)

PhaseLevel.mainRegionLocalRectWithMovePadding = HL.Field(CS.UnityEngine.Rect)

PhaseLevel.customFacTopViewRangeInWorld = HL.Field(CS.UnityEngine.Rect)


PhaseLevel.SetFacTopViewCustomRange = HL.Method(HL.Table) << function(self, args)
    local customRangeRect = args[1]
    if customRangeRect.width == 0 or customRangeRect.height == 0 then
        self.customFacTopViewRangeInWorld = nil
        return
    end
    self.customFacTopViewRangeInWorld = customRangeRect
end

PhaseLevel.ForceUpdateMainRegionInfo = HL.Method() << function(self)
    logger.info("PhaseLevel.ForceUpdateMainRegionInfo")
    local inMainRegion, panelIndex = Utils.isInFacMainRegionAndGetIndex()
    self:_UpdateCurMainRegionInfo(panelIndex)
end

PhaseLevel.OnExitTravelMode = HL.Method() << function(self)
    local inMainRegion, panelIndex = Utils.isInFacMainRegionAndGetIndex()
    if self.m_lastInFacMainRegion == inMainRegion and (not inMainRegion or self.mainRegionPanelIndex == panelIndex) then
        return
    end
    self:_UpdateCurMainRegionInfo(panelIndex)
    self:_TryAutoToggleFacMode(inMainRegion)
end

PhaseLevel.m_enterFacMainRegionCamState = HL.Field(HL.Any)

PhaseLevel.m_waitInitFacMode = HL.Field(HL.Boolean) << true

PhaseLevel._UpdateFactoryMode = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    if isInit then
        local inMainRegion, panel = GameInstance.remoteFactoryManager:IsPlayerPositionInMainRegionAndGetIndex()
        local panelIndex = -1
        if inMainRegion and panel then
            panelIndex = panel.index
        end
        local curLevelIdNum = GameWorld.worldInfo.curLevelIdNum
        self.m_waitInitFacMode = false

        self:_UpdateCurMainRegionInfo(panelIndex)


        local enterFactoryModeOnSceneLoaded = false
        local bData = GameWorld.worldInfo.curLevel.levelData.blackbox
        if bData then
            enterFactoryModeOnSceneLoaded = bData.basic.enterFactoryModeOnSceneLoaded
        end

        local inFacMode
        local inMainWorld = not UIUtils.inDungeon() and not Utils.isInSpaceShip()
        if LuaSystemManager.factory.lastMapIsDungeon and inMainWorld and LuaSystemManager.factory.inFacModeBeforeEnterDungeon ~= nil then

            inFacMode = LuaSystemManager.factory.inFacModeBeforeEnterDungeon
            LuaSystemManager.factory.inFacModeBeforeEnterDungeon = nil
        else
            inFacMode = inMainRegion or enterFactoryModeOnSceneLoaded
        end
        LuaSystemManager.factory:ClearAndSetFactoryMode(inFacMode, true)

        self.m_lastLevelIdNum = curLevelIdNum
        self.m_lastInFacMainRegion = inMainRegion
        GameWorld.worldInfo.inFacMainRegion = inMainRegion




        UIManager:AutoOpen(PanelId.FacMiniPowerHud)

        if inMainRegion then
            Notify(MessageConst.ON_ENTER_FAC_MAIN_REGION, panel)
            Notify(MessageConst.ON_IN_FAC_MAIN_REGION_CHANGE, inMainRegion)


            GameAction.SetInSafeZone(0, true)

            self.m_enterFacMainRegionCamState = FactoryUtils.enterFacCamera(FacConst.MAIN_REGION_CAM_STATE)
        else
            Notify(MessageConst.ON_EXIT_FAC_MAIN_REGION)
            if not inFacMode then
                UIManager:Hide(PanelId.FacMiniPowerHud)
            end
        end


        local otherPanels = GameWorld.worldInfo.inFactoryMode and Const.BATTLE_MODE_ONLY_PANELS or Const.FACTORY_MODE_ONLY_PANELS
        for _, panelId in pairs(otherPanels) do
            UIManager:PreloadPanelAsset(panelId, PHASE_ID)
        end

        UIManager:PreloadPersistentPanelAsset(PanelId.FacMachineCrafter)
        return
    end

    if self.m_waitInitFacMode then
        return
    end

    self:_UpdatePlayerPosFacInfo()


    self:_RefreshFacModeAndMainRegion(nil)
end

PhaseLevel._RefreshFacModeAndMainRegion = HL.Method(HL.Opt(HL.Any)) << function(self, reason)
    local inMainRegion, panel = GameInstance.remoteFactoryManager:IsPlayerPositionInMainRegionAndGetIndex()
    local panelIndex = -1
    if inMainRegion and panel then
        panelIndex = panel.index
    end
    local curLevelIdNum = GameWorld.worldInfo.curLevelIdNum

    local levelChanged = self.m_lastLevelIdNum ~= curLevelIdNum
    local mainRegionChanged = self.m_lastInFacMainRegion ~= inMainRegion or self.mainRegionPanelIndex ~= panelIndex
    if not levelChanged and not mainRegionChanged then
        return
    end

    self.m_lastLevelIdNum = curLevelIdNum
    self.m_lastInFacMainRegion = inMainRegion
    self:_UpdateCurMainRegionInfo(panelIndex)


    local isMovement = reason == CS.Beyond.Gameplay.Core.LevelChangeReason.Movement

    if not GameWorld.gameMechManager.travelPoleBrain.inFastTravelMode
        and not Utils.isSwitchModeDisabled()
        and not isMovement then
        self:_TryAutoToggleFacMode(inMainRegion)
    end
    if inMainRegion then
        Notify(MessageConst.ON_ENTER_FAC_MAIN_REGION, panel)
        if not self.m_enterFacMainRegionCamState then
            self.m_enterFacMainRegionCamState = FactoryUtils.enterFacCamera(FacConst.MAIN_REGION_CAM_STATE)
        end
    else
        Notify(MessageConst.ON_EXIT_FAC_MAIN_REGION)
        if self.m_enterFacMainRegionCamState then
            self.m_enterFacMainRegionCamState = FactoryUtils.exitFacCamera(self.m_enterFacMainRegionCamState)
        end
    end

    GameAction.SetInSafeZone(0, inMainRegion)

    Notify(MessageConst.ON_IN_FAC_MAIN_REGION_CHANGE, inMainRegion)


    GameWorld.worldInfo.inFacMainRegion = inMainRegion
end

PhaseLevel.OnCurrentLevelChanged = HL.Method(HL.Any) << function(self, arg)
    if self.m_waitInitFacMode then
        return
    end
    local reason = unpack(arg)
    self:_RefreshFacModeAndMainRegion(reason)
end

PhaseLevel._UpdateCurMainRegionInfo = HL.Method(HL.Opt(HL.Number)) << function(self, panelIndex)
    if panelIndex and panelIndex >= 0 then
        self.mainRegionPanelIndex = panelIndex
        self.mainRegionLocalRect = GameInstance.remoteFactoryManager:GetMainRegionLocalRect(panelIndex, 0)
        local padding = Vector2(FacConst.FAC_TOP_VIEW_MOVE_PADDING, FacConst.FAC_TOP_VIEW_MOVE_PADDING)
        self.mainRegionLocalRectWithMovePadding = Unity.Rect(self.mainRegionLocalRect.min + padding, self.mainRegionLocalRect.size - padding * 2)
    else
        self.mainRegionPanelIndex = -1
        self.mainRegionLocalRect = nil
        self.mainRegionLocalRectWithMovePadding = nil
    end
end

PhaseLevel._TryAutoToggleFacMode = HL.Method(HL.Boolean) << function(self, inMainRegion)
    if inMainRegion then
        if FactoryUtils.canPlayerEnterFacMode() then

            LuaSystemManager.factory:AddFactoryModeRequest({ true, "Player" })
        end
    else

        if not FactoryUtils.isInBuildMode() then
            LuaSystemManager.factory:RemoveFactoryModeRequest("Player" )
        end
    end
end

PhaseLevel.OnSquadInfightChanged = HL.Method(HL.Opt(HL.Any)) << function(self)
    local inFight = Utils.isInFight()
    if inFight then
        LuaSystemManager.factory:AddFactoryModeRequest({ false, "InFight" })
    else
        LuaSystemManager.factory:RemoveFactoryModeRequest("InFight")
    end
end

PhaseLevel.isPlayerOutOfRangeManual = HL.Field(HL.Boolean) << false

PhaseLevel._UpdatePlayerPosFacInfo = HL.Method() << function(self)
    local succ, outOfRangeManual = GameInstance.remoteFactoryManager:TrySampleCurrentSceneGridStatusWithPlayerPosition()
    if succ then
        if outOfRangeManual ~= self.isPlayerOutOfRangeManual then
            self.isPlayerOutOfRangeManual = outOfRangeManual




            local disableSwitchMode = GameInstance.player.forbidSystem:IsForbidden(ForbidType.DisableSwitchMode)
            if outOfRangeManual and not disableSwitchMode then
                if FactoryUtils.isInBuildMode() then
                    Notify(MessageConst.FAC_BUILD_EXIT_CUR_MODE, true)
                end
                LuaSystemManager.factory:ClearAndSetFactoryMode(false, true)
            end
            Notify(MessageConst.FAC_ON_PLAYER_POS_INFO_CHANGED)
        end
    end
end






PhaseLevel._AddRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Add("Tick", function()
        self:_Update()
    end, true)
end

PhaseLevel._ClearRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
end

PhaseLevel._Update = HL.Method() << function(self)
    self:_UpdateFactoryMode()
end







PhaseLevel._OnActivated = HL.Override() << function(self)
    self:_AddRegisters()
    GameWorld.ppEffectLoader:ResumeTick()
    self:_OnInternalInMainHudStateChanged(true)
end


PhaseLevel._OnDeActivated = HL.Override() << function(self)
    self:_ClearRegisters()
    GameWorld.ppEffectLoader:PauseTick()
    self:_OnInternalInMainHudStateChanged(false)
    Notify(MessageConst.FORCE_ENABLE_UI_SCENE_BLUR, { key = self, enabled = true})
end

PhaseLevel._OnDestroy = HL.Override() << function(self)
    if not InputManagerInst.inChangingInputDevice then
        for _, panelId in pairs(Const.FACTORY_MODE_ONLY_PANELS) do
            UIManager:Close(panelId)
        end
        for _, panelId in pairs(Const.BATTLE_MODE_ONLY_PANELS) do
            UIManager:Close(panelId)
        end
    end

    if self.m_hidePanelKey > 0 then
        self.m_hidePanelKey = UIManager:RecoverScreen(self.m_hidePanelKey)
    end
    if self.m_defenseInGameClearScreenKey > 0 then
        self.m_defenseInGameClearScreenKey = UIManager:RecoverScreen(self.m_defenseInGameClearScreenKey)
    end
    if self.m_defenseFinishClearScreenKey > 0 then
        self.m_defenseFinishClearScreenKey = UIManager:RecoverScreen(self.m_defenseFinishClearScreenKey)
    end
    self:_ClearRegisters()
    self:_ClearInMainHudMessageList()

    if self.m_enterFacMainRegionCamState then
        self.m_enterFacMainRegionCamState = FactoryUtils.exitFacCamera(self.m_enterFacMainRegionCamState)
    end

    local inDungeon = UIUtils.inDungeon()
    LuaSystemManager.factory.lastMapIsDungeon = inDungeon
    if not inDungeon then

        LuaSystemManager.factory.inFacModeBeforeEnterDungeon = LuaSystemManager.factory:GetFactoryModeOfRequest("Player")
    end

    Notify(MessageConst.ON_PHASE_LEVEL_DESTROYED)
end

PhaseLevel._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, transArgs)
    Notify(MessageConst.FORCE_ENABLE_UI_SCENE_BLUR, { key = self, enabled = false})
    if DeviceInfo.isMobile then
        Notify(MessageConst.FORCE_ENABLE_UI_SCENE_BLUR, { key = "MobileController", enabled = DeviceInfo.usingController})
    end
    self:TryRestoreSimulationTrainingTrackHud()
    self:OpenLevelPanels()


    self:PerformLoginCheck()

    logger.info("ON_PHASE_LEVEL_ON_TOP")
    Notify(MessageConst.ON_PHASE_LEVEL_ON_TOP)


    if not LuaSystemManager.uiRestoreSystem:HasValidAction() then
        CS.Beyond.Gameplay.Conditions.OnEnterMainHud.Trigger()
    end

    self:TryRestoreTowerDefense()

    if GameWorld.battle.seasonTowerState ~= CS.Beyond.Gameplay.Core.BattleManager.SeasonTowerState.None then
        UIManager:AutoOpen(PanelId.SeasonTowerBuff)
    end


    if not Utils.isInDungeon() then
        Notify(MessageConst.ON_WORLD_LEVEL_CHANGED, {GameInstance.player.adventure.currentMaxWorldLevel - 1, GameInstance.player.adventure.currentMaxWorldLevel, false})
    end

    if self.arg then
        local buildModeInfos = self.arg.buildModeInfos
        local mode, buildArgs, buildRecoverState
        if buildModeInfos then
            mode, buildArgs, buildRecoverState = unpack(buildModeInfos)
        end

        if self.arg.inFacMode ~= nil then
            LuaSystemManager.factory:AddFactoryModeRequest({ self.arg.inFacMode, "Player" })
        end
        if self.arg.isInTopView then
            local topViewArg = self.arg.topViewArg
            LuaSystemManager.factory:ToggleTopView(true, true)
            local succ, topViewCtrl = UIManager:IsOpen(PanelId.FacTopView)
            if succ then
                LuaSystemManager.factory.topViewCamTarget.transform.position = topViewArg.targetPos
                LuaSystemManager.factory.topViewCamTarget.transform.eulerAngles = topViewArg.targetRot
                LuaSystemManager.factory:SetTopViewCamZoomValue(topViewArg.zoomValue)
                topViewCtrl:RecoverStateOnChangeDevice(topViewArg.selectedTypeIndex, topViewArg.selectedFilters)
                if buildRecoverState and buildRecoverState.controllerMouseWorldPos and DeviceInfo.usingController then
                    topViewCtrl:RecoverControllerMouseOnChangeDevice(buildRecoverState.controllerMouseWorldPos)
                    buildArgs.initMousePos = InputManager.mousePosition
                end
            end
        end
        if buildModeInfos or self.arg.inDestroyMode then
            local hideKey = UIManager:ClearScreen()
            TimerManager:StartTimer(0, function()
                if buildModeInfos then
                    buildArgs.isFromChangeInputDevice = true
                    buildArgs.initDir = buildRecoverState and buildRecoverState.initDir or nil
                    Notify(FacConst.FAC_BUILD_MODE_MSG_MAP[mode], buildArgs)
                else
                    Notify(MessageConst.FAC_ENTER_DESTROY_MODE, { isFromChangeInputDevice = true, openSaveBP = self.arg.isSavingBP })
                end
                UIManager:RecoverScreen(hideKey)
            end)
        end
    end
end


PhaseLevel.m_hidePanelKey = HL.Field(HL.Number) << -1

PhaseLevel._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    Notify(MessageConst.FORCE_ENABLE_UI_SCENE_BLUR, { key = self, enabled = false})

    self.m_hidePanelKey = UIManager:RecoverScreen(self.m_hidePanelKey)

    logger.info("ON_PHASE_LEVEL_ON_TOP")
    Notify(MessageConst.ON_PHASE_LEVEL_ON_TOP)
    CS.Beyond.Gameplay.Conditions.OnEnterMainHud.Trigger()

    GameInstance.remoteFactoryManager:ForceCullingExecute()

    self:_OnInternalInMainHudStateChanged(true)
end

PhaseLevel.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionBehind then

        self:_OnInternalInMainHudStateChanged(false)
    end
end

PhaseLevel.m_transitionReservePanelIds = HL.Field(HL.Table)

PhaseLevel.SetPhaseLevelTransitionReservePanels = HL.Method(HL.Table) << function(self, ids)



    self.m_transitionReservePanelIds = ids
end

PhaseLevel._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local usingBT = UIUtils.usingBlockTransition()
    if args.anotherPhaseId == PhaseId.CharInfo or args.anotherPhaseId == PhaseId.CharFormation then


    elseif fastMode or usingBT or PhaseLevel.s_forceTransitionBehindFastMode then
        self.m_hidePanelKey = UIManager:ClearScreen(self.m_transitionReservePanelIds)
    else
        self.m_inTransition = true
        UIManager:ClearScreenWithOutAnimation(function(key)
            self.m_hidePanelKey = key
            if self.m_completeOnDestroy and self.m_hidePanelKey > 0 then
                self.m_hidePanelKey = UIManager:RecoverScreen(self.m_hidePanelKey)
            end
            self.m_inTransition = false
        end, self.m_transitionReservePanelIds)
    end
    self.m_transitionReservePanelIds = nil

    self:_OnInternalInMainHudStateChanged(false)


    GameInstance.playerController.commandController:ConsumeAllCommand()

    logger.info("ON_PHASE_LEVEL_NOT_ON_TOP")
    Notify(MessageConst.ON_PHASE_LEVEL_NOT_ON_TOP)
end

PhaseLevel._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:_OnInternalInMainHudStateChanged(false)


    GameInstance.playerController.commandController:ConsumeAllCommand()

    logger.info("ON_PHASE_LEVEL_NOT_ON_TOP")
    Notify(MessageConst.ON_PHASE_LEVEL_NOT_ON_TOP)
end






local defenseExpectedPanels = {
    PanelId.MainHud,
    PanelId.Joystick,
    PanelId.LevelCamera,
    PanelId.FacHudBottomMask,
    PanelId.FacBuildMode,
    PanelId.FacDestroyMode,
    PanelId.FacBuildingInteract,
    PanelId.CommonItemToast,
    PanelId.CommonNewToast,
    PanelId.CommonHudToast,
    PanelId.Radio,
    PanelId.CommonTaskTrackHud,
    PanelId.FacTopViewBuildingInfo,
    PanelId.InteractOption,
    PanelId.GuideLimited,
    PanelId.AIBark,
    PanelId.SettlementDefenseTransit,
    PanelId.MissionHud,
    PanelId.HeadBar,
}

local defenseFinishExpectedPanels = {
    PanelId.Joystick,
    PanelId.LevelCamera,
    PanelId.CommonTaskTrackHud,
}

local DEFENSE_TASK_TRACK_HUD_OFFSET = Vector2(0, -120)
local DEFENSE_CLEAR_DELAY_TIMER = 1.5
local DEFENSE_MAIN_CHAR_EFFECT_NAME = "P_fxfac_interactive_holocast_2101"

PhaseLevel.m_defensePrepareCtrl = HL.Field(HL.Forward("SettlementDefensePrepareHudCtrl"))

PhaseLevel.m_defenseTrackerCtrl = HL.Field(HL.Forward("SettlementDefenseTrackerCtrl"))

PhaseLevel.m_defenseInGamePanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseLevel.m_defenseTrackerPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseLevel.m_defenseMiniMapPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseLevel.m_defenseInGameClearScreenKey = HL.Field(HL.Number) << -1

PhaseLevel.m_defenseFinishClearScreenKey = HL.Field(HL.Number) << -1

PhaseLevel.m_defenseClearTimer = HL.Field(HL.Number) << -1

PhaseLevel.m_defenseMainCharEffect = HL.Field(HL.Userdata)

PhaseLevel.TryRestoreTowerDefense = HL.Method() << function(self)
    if GameInstance.player.towerDefenseSystem.hudState == CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.Preparing then
        UIManager:Close(PanelId.MissionHud)
        self:OnEnterTowerDefensePreparingPhase()
    end
    if GameInstance.player.towerDefenseSystem.hudState == CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.Defending then
        UIManager:Close(PanelId.MissionHud)
        self:OnEnterTowerDefenseDefendingPhase(true)
        self:OnTowerDefenseDefendingTransitFinished()
    end
end

PhaseLevel.TryRestoreSimulationTrainingTrackHud = HL.Method() << function(self)

    if not GameInstance.player.simulationTrainingSystem.inSubGame then
        return
    end

    if UIManager:IsOpen(PanelId.SimulationTrainingTrackHud) then
        return
    end

    local SIMULATION_TRAINING_SUB_GAME_ID = "world_poi_simulation_training_01"

    local SIMULATION_TRAINING_PHASE_NORMAL = 1

    Notify(MessageConst.ON_OPEN_SUB_GAME_SIMULATION_TRAINING, { SIMULATION_TRAINING_SUB_GAME_ID, SIMULATION_TRAINING_PHASE_NORMAL })
end


PhaseLevel.OnEnterTowerDefensePreparingPhase = HL.Method() << function(self)
    GameInstance.player.towerDefenseSystem.hudState = CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.Preparing
    LuaSystemManager.factory:AddFactoryModeRequest({ true, "TowerDefensePrepare" })
    self.m_defensePrepareCtrl = UIManager:AutoOpen(PanelId.SettlementDefensePrepareHud)
    self.m_defenseTrackerCtrl = UIManager:AutoOpen(PanelId.SettlementDefenseTracker)
end

PhaseLevel.OnLeaveTowerDefensePreparingPhase = HL.Method(HL.Any) << function(self, args)
    GameInstance.player.towerDefenseSystem.hudState = CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.None
    LuaSystemManager.factory:RemoveFactoryModeRequest("TowerDefensePrepare")
    local onLeavingArea, startLeave = unpack(args)
    if self.m_defensePrepareCtrl:IsShow() then
        self.m_defensePrepareCtrl:CloseDefensePrepareHud(onLeavingArea, startLeave)
    else
        self.m_defensePrepareCtrl:Close()
    end
    self.m_defensePrepareCtrl = nil
    self.m_defenseTrackerCtrl:Close()
    self.m_defenseTrackerCtrl = nil
end

PhaseLevel.OnEnterTowerDefenseDefendingPhase = HL.Method(HL.Opt(HL.Boolean)) << function(self, isRestore)
    self.m_defenseInGameClearScreenKey = UIManager:ClearScreen(lume.concat(defenseExpectedPanels, Const.BATTLE_MODE_ONLY_PANELS))
    Notify(MessageConst.TOGGLE_HIDE_INTERACT_OPTION_LIST, { "TowerDefense", true })
    GameInstance.player.towerDefenseSystem.systemInDefense = true

    local isOpen, taskTrackHudCtrl = UIManager:IsOpen(PanelId.CommonTaskTrackHud)
    if isOpen and not isRestore then
        taskTrackHudCtrl:AddPositionOffset(DEFENSE_TASK_TRACK_HUD_OFFSET, false)
    end
end

PhaseLevel.OnTowerDefenseDefendingTransitFinished = HL.Method() << function(self)
    GameInstance.player.towerDefenseSystem.hudState = CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.Defending
    self.m_defenseInGamePanelItem = self:CreatePhasePanelItem(PanelId.SettlementDefenseInGameHud)
    self.m_defenseTrackerPanelItem = self:CreatePhasePanelItem(PanelId.SettlementDefenseTracker)
    self.m_defenseMiniMapPanelItem = self:CreatePhasePanelItem(PanelId.SettlementDefenseMiniMap)

    AudioManager.PostAudioCue("au_cue_music_base_mode_defense_main_start")
end

PhaseLevel.OnLeaveTowerDefenseDefendingPhase = HL.Method() << function(self)
    GameInstance.player.towerDefenseSystem.hudState = CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.WaitingFinished
    local waitCloseItemList = {
        self.m_defenseInGamePanelItem,
        self.m_defenseTrackerPanelItem,
        self.m_defenseMiniMapPanelItem,
    }

    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        PhaseManager:ExitPhaseFastTo(PHASE_ID)
    end

    self.m_defenseFinishClearScreenKey = UIManager:ClearScreen(defenseFinishExpectedPanels)

    local waitCount = #waitCloseItemList
    for _, item in ipairs(waitCloseItemList) do
        item.uiCtrl:PlayAnimationOutWithCallback(function()
            self:RemovePhasePanelItem(item)
            waitCount = waitCount - 1
            if waitCount == 0 then
                if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
                    PhaseManager:ExitPhaseFastTo(PHASE_ID)
                end
                self.m_defenseClearTimer = TimerManager:StartTimer(DEFENSE_CLEAR_DELAY_TIMER, function()
                    TimerManager:ClearTimer(self.m_defenseClearTimer)
                    GameInstance.player.towerDefenseSystem.hudState = CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.None
                    Notify(MessageConst.ON_TOWER_DEFENSE_LEVEL_HUD_CLEARED)
                end)
            end
        end)
    end

    local isOpen, taskTrackHudCtrl = UIManager:IsOpen(PanelId.CommonTaskTrackHud)
    if isOpen then
        taskTrackHudCtrl:ClearPositionOffset()
    end

    self:ClearTowerDefenseMainCharEffect()
end

PhaseLevel.OnTowerDefenseDefendingRewardsFinished = HL.Method() << function(self)
    Notify(MessageConst.TOGGLE_HIDE_INTERACT_OPTION_LIST, { "TowerDefense", false })
    self.m_defenseInGameClearScreenKey = UIManager:RecoverScreen(self.m_defenseInGameClearScreenKey)
    self.m_defenseFinishClearScreenKey = UIManager:RecoverScreen(self.m_defenseFinishClearScreenKey)
    if Utils.needMissionHud() then
        UIManager:AutoOpen(PanelId.MissionHud)
    end
end

PhaseLevel.PlayTowerDefenseMainCharEffect = HL.Method() << function(self)
    local activeTdId = GameInstance.player.towerDefenseSystem.activeTdId
    if string.isEmpty(activeTdId) then
        return
    end
    local _, towerDefenseData = Tables.towerDefenseTable:TryGetValue(activeTdId)
    if not towerDefenseData or towerDefenseData.tdType ~= GEnums.TowerDefenseLevelType.Auto then
        return
    end
    self:ClearTowerDefenseMainCharEffect()
    local mainModel
    if GameInstance.playerController.mainCharacter ~= nil and GameInstance.playerController.mainCharacter.modelCom ~= nil then
        mainModel = GameInstance.playerController.mainCharacter.modelCom.model
    end
    local entityRenderHelper = mainModel and mainModel:GetComponent(typeof(CS.Beyond.Gameplay.View.EntityRenderHelper))
    if entityRenderHelper then
        self.m_defenseMainCharEffect = GameInstance.effectManager:CreateVFXEffectOnTransform(
            DEFENSE_MAIN_CHAR_EFFECT_NAME, entityRenderHelper)
        self.m_defenseMainCharEffect:Lock():LoadImmediately()
    end
end

PhaseLevel.ClearTowerDefenseMainCharEffect = HL.Method() << function(self)
    if self.m_defenseMainCharEffect and self.m_defenseMainCharEffect:Lock() ~= nil then
        self.m_defenseMainCharEffect:Lock():Finish()
        self.m_defenseMainCharEffect = nil
    end
end

PhaseLevel.OnRepatriate = HL.Method() << function(self)

    self:PlayTowerDefenseMainCharEffect()
end

PhaseLevel.OnSceneRepatriateNeedUnstuck = HL.Method(HL.Table) << function(self, args)
    local sceneNumId = unpack(args)
    local content = Language.LUA_GAME_SETTING_REPATRIATE_NEED_UNSTUCK_POPUP_CONTENT
    local confirmText = Language.LUA_GAME_SETTING_REPATRIATE_NEED_UNSTUCK_POPUP_CONFIRM_TEXT
    local cancelText = Language.LUA_GAME_SETTING_REPATRIATE_NEED_UNSTUCK_POPUP_CANCEL_TEXT
    local popupConfig = GameInstance.dataManager.repatriateConfig.needUnstuckPopupConfig
    if popupConfig ~= nil then
        if not popupConfig.content.isEmpty then
            content = popupConfig.content:GetText()
        end
        if not popupConfig.confirmText.isEmpty then
            confirmText = popupConfig.confirmText:GetText()
        end
        if not popupConfig.cancelText.isEmpty then
            cancelText = popupConfig.cancelText:GetText()
        end
    end
    Notify(MessageConst.SHOW_POP_UP, {
        content = content,
        confirmText = confirmText,
        cancelText = cancelText,
        freezeWorld = true,
        onConfirm = function()
            GameInstance.player.gameSettingSystem:RequestGetUnstuck(sceneNumId)
        end,
        onCancel = function()
            GameWorld.repatriateManager:SendRepatriateFromSelfRescuePanel()
        end,
    })
end






PhaseLevel._ShowDomainDepotPackHudPanelIfNeed = HL.Method() << function(self)
    if GameInstance.player.domainDepotSystem:IsDomainDepotDeliveringCargo() then
        self:_ShowDomainDepotPackHudPanel(false)
    end
end

PhaseLevel._ShowDomainDepotPackHudPanel = HL.Method(HL.Boolean) << function(self, needShowAllInfo)
    UIManager:AutoOpen(PanelId.DomainDepotPackHud, { needShowAllInfo = needShowAllInfo })
end

PhaseLevel._HideDomainDepotPackHudPanel = HL.Method() << function(self)
    UIManager:Close(PanelId.DomainDepotPackHud)
end

PhaseLevel._RefreshDomainDepotPackHudOnGameModeChange = HL.Method() << function(self)
    local isOpen = UIManager:IsOpen(PanelId.DomainDepotPackHud)
    if not isOpen then
        return
    end
    local needShow = GameInstance.mode.modeType == GEnums.GameModeType.Default
    if needShow then
        local isShow = UIManager:IsShow(PanelId.DomainDepotPackHud)
        if not isShow then
            UIManager:Show(PanelId.DomainDepotPackHud)
        end
    else
        local isHide = UIManager:IsHide(PanelId.DomainDepotPackHud)
        if not isHide then
            UIManager:Hide(PanelId.DomainDepotPackHud)
        end
    end
end

PhaseLevel.OnStartDomainDepotDeliver = HL.Method() << function(self)
    self:_ShowDomainDepotPackHudPanel(false)
end

PhaseLevel.OnFinishDomainDepotDeliver = HL.Method(HL.Any) << function(self, args)
    local selfComplete = unpack(args)
    self:_HideDomainDepotPackHudPanel()
end






local GameModeHideUIKey = "GameMode"

PhaseLevel.OnGameModeEnable = HL.Method(HL.Table) << function(self, args)
    local modeType, mode = unpack(args)
    logger.info("PhaseLevel.OnGameModeEnable", tostring(modeType), tostring(mode))
    if mode.hideSquadIcon then
        UIManager:HideWithKey(PanelId.SquadIcon, GameModeHideUIKey)
    else
        UIManager:ShowWithKey(PanelId.SquadIcon, GameModeHideUIKey)
    end

    if mode.forbidAttack then
        UIManager:HideWithKey(PanelId.BattleAction, GameModeHideUIKey)
    else
        UIManager:ShowWithKey(PanelId.BattleAction, GameModeHideUIKey)
    end
    Notify(MessageConst.TOGGLE_FORBID_ATTACK, { GameModeHideUIKey, mode.forbidAttack })

    if mode.hideMissionHud then
        UIManager:HideWithKey(PanelId.MissionHud, GameModeHideUIKey)
    else
        UIManager:ShowWithKey(PanelId.MissionHud, GameModeHideUIKey)
    end

    if mode.hideSNSHud then
        UIManager:HideWithKey(PanelId.SNSHud, GameModeHideUIKey)
    else
        UIManager:ShowWithKey(PanelId.SNSHud, GameModeHideUIKey)
    end

    self:_RefreshDomainDepotPackHudOnGameModeChange()

    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidSprint, GameModeHideUIKey, mode.forbidSprint)
    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidLockTarget, GameModeHideUIKey, mode.forbidLockTarget)
end






local ForbidSystemHideUIKey = "PhaseLevelForbid"

PhaseLevel.OnForbidSystemChanged = HL.Method(HL.Table) << function(self, args)
    local forbidType, isForbidden = unpack(args)
    if forbidType == ForbidType.HideSquadIcon then
        if isForbidden then
            UIManager:HideWithKey(PanelId.SquadIcon, ForbidSystemHideUIKey)
        else
            UIManager:ShowWithKey(PanelId.SquadIcon, ForbidSystemHideUIKey)
        end
    elseif forbidType == ForbidType.ForbidAttack then
        if isForbidden then
            UIManager:HideWithKey(PanelId.BattleAction, ForbidSystemHideUIKey)
        else
            UIManager:ShowWithKey(PanelId.BattleAction, ForbidSystemHideUIKey)
        end
    elseif forbidType == ForbidType.HideSNSHud then
        if isForbidden then
            UIManager:HideWithKey(PanelId.SNSHud, ForbidSystemHideUIKey)
        else
            UIManager:ShowWithKey(PanelId.SNSHud, ForbidSystemHideUIKey)
        end
    end
end

PhaseLevel._RefreshUIForbidState = HL.Method() << function(self)

    local forbidSys = GameInstance.player.forbidSystem

    if forbidSys:IsForbidden(ForbidType.HideSquadIcon) then
        UIManager:HideWithKey(PanelId.SquadIcon, ForbidSystemHideUIKey)
    else
        UIManager:ShowWithKey(PanelId.SquadIcon, ForbidSystemHideUIKey)
    end

    if forbidSys:IsForbidden(ForbidType.ForbidAttack) then
        UIManager:HideWithKey(PanelId.BattleAction, ForbidSystemHideUIKey)
    else
        UIManager:ShowWithKey(PanelId.BattleAction, ForbidSystemHideUIKey)
    end

    if forbidSys:IsForbidden(ForbidType.HideSNSHud) then
        UIManager:HideWithKey(PanelId.SNSHud, ForbidSystemHideUIKey)
    else
        UIManager:ShowWithKey(PanelId.SNSHud, ForbidSystemHideUIKey)
    end
end





local INTERNAL_OUT_MAIN_HUD_KEY = "otherPhase"

PhaseLevel.m_inMainHudMessageConfig = HL.Field(HL.Table)

PhaseLevel.m_inMainHudMessageDataList = HL.Field(HL.Table)

PhaseLevel.m_outMainHudKeyList = HL.Field(HL.Table)

PhaseLevel._InitInMainHudMessageList = HL.Method() << function(self)


    self.m_inMainHudMessageConfig = {
        ["loading"] = {
            inMessage = MessageConst.ON_LOADING_PANEL_CLOSED,
            outMessage = MessageConst.ON_LOADING_PANEL_OPENED,
            earlyCheckPanelId = PanelId.Loading
        },
        ["teleport"] = {
            inMessage = MessageConst.ON_TELEPORT_LOADING_PANEL_CLOSED,
            outMessage = MessageConst.ON_TELEPORT_LOADING_PANEL_OPENED,
            earlyCheckPanelId = PanelId.TeleportLoading
        },
        ["blackScreen"] = {
            inMessage = MessageConst.NOTIFY_MAIN_HUD_BLACK_SCREEN_END,
            outMessage = MessageConst.NOTIFY_MAIN_HUD_BLACK_SCREEN_BEGIN,
            earlyCheckPanelId = PanelId.CommonMask
        },
        ["blockedReward"] = {
            inMessage = MessageConst.ON_EXIT_BLOCKED_REWARD_POP_UP_PANEL,
            outMessage = MessageConst.ON_ENTER_BLOCKED_REWARD_POP_UP_PANEL,
        },
        ["dungeonSettlement"] = {
            inMessage = MessageConst.ON_DUNGEON_SETTLEMENT_CLOSED,
            outMessage = MessageConst.ON_DUNGEON_SETTLEMENT_OPENED,
        },
        ["forceSNS"] = {
            inMessage = MessageConst.ON_SNS_FORCE_DIALOG_END,
            outMessage = MessageConst.ON_SNS_FORCE_DIALOG_START,
        },
    }

    self.m_inMainHudMessageDataList = {}
    self.m_outMainHudKeyList = {}

    for index, configInfo in pairs(self.m_inMainHudMessageConfig) do
        local inKey = MessageManager:Register(configInfo.inMessage, function()
            self:_OnInMainHudMessageNotified(index, true)
        end, self)

        local outKey = MessageManager:Register(configInfo.outMessage, function()
            self:_OnInMainHudMessageNotified(index, false)
        end, self)

        self.m_inMainHudMessageDataList[index] = {
            inKey = inKey,
            outKey = outKey,
        }




        if configInfo.earlyCheckPanelId ~= nil and UIManager:IsShow(configInfo.earlyCheckPanelId) then
            self:_OnInMainHudMessageNotified(index, false)
        end
    end
end

PhaseLevel._ClearInMainHudMessageList = HL.Method() << function(self)
    for _, info in ipairs(self.m_inMainHudMessageDataList) do
        MessageManager:Unregister(info.inKey)
        MessageManager:Unregister(info.outKey)
    end
    self.m_inMainHudMessageConfig = {}
    self.m_inMainHudMessageDataList = {}
    self.m_outMainHudKeyList = {}
end

PhaseLevel._GetIsOutMainHud = HL.Method().Return(HL.Boolean) << function(self)
    return next(self.m_outMainHudKeyList) ~= nil
end

PhaseLevel.OnToggleInMainHudMessageNotified = HL.Method(HL.Table) << function(self, args)
    local key, isInMainHud
    if lume.isarray(args) then
        key, isInMainHud = unpack(args)
    else
        key, isInMainHud = args.key, args.isInMainHud
    end

    self:_OnInMainHudMessageNotified(key, isInMainHud)
end

PhaseLevel._OnInMainHudMessageNotified = HL.Method(HL.String, HL.Boolean) << function(self, key, isInMainHud)
    if isInMainHud then
        self.m_outMainHudKeyList[key] = nil
    else
        self.m_outMainHudKeyList[key] = true
    end

    self:_OnInMainHudStateChanged()
    logger.important(CS.Beyond.EnableLogType.DevOnly, "当前有其他行为导致是否处于MainHud状态发生改变, 来源", key, ", isIn", isInMainHud, ", 当前是否MainHud", GameWorld.worldInfo.inMainHud)
end

PhaseLevel._OnInternalInMainHudStateChanged = HL.Method(HL.Boolean) << function(self, isIn)
    if isIn then
        self.m_outMainHudKeyList[INTERNAL_OUT_MAIN_HUD_KEY] = nil
    else
        self.m_outMainHudKeyList[INTERNAL_OUT_MAIN_HUD_KEY] = true
    end

    if InputManagerInst.inChangingInputDevice then


        return
    end

    self:_OnInMainHudStateChanged()
    logger.important(CS.Beyond.EnableLogType.DevOnly, "当前因为打开了其他Phase导致是否处于MainHud状态发生改变, isIn", isIn, ", 当前是否MainHud", GameWorld.worldInfo.inMainHud)
end

PhaseLevel._OnInMainHudStateChanged = HL.Method() << function(self)
    GameWorld.worldInfo.inMainHud = not self:_GetIsOutMainHud()
end




PhaseLevel.OnResetBlackbox = HL.Method() << function(self)
    if FactoryUtils.isInTopView() then
        LuaSystemManager.factory:ToggleTopView(false, true)
    end
    self:_UpdateFactoryMode(true)
end




PhaseLevel.m_forceEnableUISceneBlurKeys = HL.Field(HL.Table)

PhaseLevel._SetUISceneBlurEnabled = HL.Method(HL.Boolean) << function(self, enabled)
    local hgCamera = HGCamera.GetOrCreate(GameInstance.cameraManager.mainCamera)
    hgCamera:SetEnableUpdatingSceneFrostedGlass(enabled)
end

PhaseLevel.OnForceEnableUISceneBlur = HL.Method(HL.Table) << function(self, args)
    if not args.key then
        return
    end
    self.m_forceEnableUISceneBlurKeys = self.m_forceEnableUISceneBlurKeys or {}
    self.m_forceEnableUISceneBlurKeys[args.key] = args.enabled
    local shouldEnable = false
    for _, enabled in pairs(self.m_forceEnableUISceneBlurKeys) do
        if enabled then
            shouldEnable = true
            break
        end
    end
    self:_SetUISceneBlurEnabled(shouldEnable)
end

PhaseLevel.OnInputDeviceTypeChanged = HL.Method(HL.Table) << function(self, args)
    if DeviceInfo.isMobile then
        Notify(MessageConst.FORCE_ENABLE_UI_SCENE_BLUR, { key = "MobileController", enabled = DeviceInfo.usingController})
    end
end






PhaseLevel.s_LoginCheckFinishedInfo = HL.StaticField(HL.Table)

local Name_LoginCheck_CashShopOrderSettle = "LoginCheck_CashShopOrderSettle"
local Name_LoginCheck_MonthlypassPopup = "LoginCheck_MonthlypassPopup"
local Name_LoginCheck_StartGuide = "LoginCheck_StartGuide"
local Name_LoginCheck_ForceSNS = "LoginCheck_ForceSNS"
local Name_LoginCheck_Reflow = "LoginCheck_Reflow"
local Name_LoginCheck_ActivityCheckIn = "LoginCheck_ActivityCheckIn"
local Name_LoginCheck_ImportantActivity_WaitCinematic = "LoginCheck_ImportantActivity_WaitCinematic"
local Name_LoginCheck_ImportantActivity_Top = "LoginCheck_ImportantActivity_Top"
local Name_LoginCheck_ImportantActivity_Fallback = "LoginCheck_ImportantActivity_Fallback"



PhaseLevel._CanTopImportantActivity = HL.Method().Return(HL.Boolean) << function(self)

    if GameInstance.player.guide:HasPendingForceGuide() then
        return false
    end

    if GameInstance.player.sns:HasPendingForceSNS() then
        return false
    end

    local q = LuaSystemManager.mainHudActionQueue
    if q:HasRequest("Cinematic") or q:HasRequest("CinematicBlocker") then
        return false
    end
    return true
end

PhaseLevel.OnGameLevelLoadingFinish = HL.Method() << function(self)
    local q = LuaSystemManager.mainHudActionQueue
    if not q:HasRequest(Name_LoginCheck_ImportantActivity_WaitCinematic) then
        return
    end



    local requestKey = self:_CanTopImportantActivity()
            and Name_LoginCheck_ImportantActivity_Top
            or Name_LoginCheck_ImportantActivity_Fallback
    LuaSystemManager.mainHudActionQueue:AddRequest(requestKey, function(_)
        if not ActivityUtils.findImportantCheckinActivity() then
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, requestKey)
            return
        end
        PhaseManager:OpenPhaseFast(PhaseId.ActivityImportantPopup, { requestKey = requestKey })
    end)

    Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_ImportantActivity_WaitCinematic)
end

PhaseLevel.PerformLoginCheck = HL.Method() << function(self)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "PhaseLevel.PerformLoginCheck")


    if not PhaseLevel.s_LoginCheckFinishedInfo then
        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "Init PhaseLevel.s_LoginCheckFinishedInfo")
        PhaseLevel.s_LoginCheckFinishedInfo = {}
        LuaSystemManager.mainHudActionQueue:ToggleActionPlayIgnoreMainHud(Name_LoginCheck_CashShopOrderSettle, true)
        LuaSystemManager.mainHudActionQueue:ToggleActionPlayIgnoreMainHud(Name_LoginCheck_MonthlypassPopup, true)
        LuaSystemManager.mainHudActionQueue:ToggleActionPlayIgnoreMainHud(Name_LoginCheck_StartGuide, true)
        LuaSystemManager.mainHudActionQueue:ToggleActionPlayIgnoreMainHud(Name_LoginCheck_ForceSNS, true)
        LuaSystemManager.mainHudActionQueue:ToggleActionPlayIgnoreMainHud(Name_LoginCheck_ImportantActivity_WaitCinematic, true)
        LuaSystemManager.mainHudActionQueue:ToggleActionPlayIgnoreMainHud(Name_LoginCheck_ImportantActivity_Top, true)


    end


    if PhaseManager:IsPhaseUnlocked(PhaseId.ActivityPopup) then
        PhaseLevel.s_LoginCheckFinishedInfo.activityPopupAlreadyUnlocked = true
    end




    local importantActivityId = nil
    if not (UNITY_EDITOR and BEYOND_DEBUG and CS.Beyond.DebugDefines.disableCheckInLoginCheck)
            and not Utils.isInDungeon() and not Utils.isInFocusMode() then
        importantActivityId = ActivityUtils.findImportantCheckinActivity()
    end
    if not PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ImportantActivity_Top]
            and not PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ImportantActivity_Fallback]
            and importantActivityId then
        PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ImportantActivity_Top] = true
        PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ImportantActivity_Fallback] = true


        LuaSystemManager.mainHudActionQueue:AddRequest(Name_LoginCheck_ImportantActivity_WaitCinematic, function(_) end)
    end


    GameInstance.player.cashShopSystem.isRemindOrderChecked = true
    if not PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_CashShopOrderSettle] and CashShopUtils.haveRemainOrders()
            and GameInstance.player.mission:IsMissionCompleted("e0m0")  then
        PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_CashShopOrderSettle] = true
        LuaSystemManager.mainHudActionQueue:AddRequest(Name_LoginCheck_CashShopOrderSettle, function(_)
            local interrupt = {
                interruptMessage = { MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE },
                onInterrupt = function()
                    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = CashShopConst.RemainOrderMainHudKey, isInMainHud = true })
                    CoroutineManager:StartCoroutine(function()

                        coroutine.step()
                        local requestKey = "CashShopOrderSettleInterrupt"
                        if LuaSystemManager.mainHudActionQueue:HasRequest(requestKey) then
                            return
                        end
                        LuaSystemManager.mainHudActionQueue:AddRequest(requestKey, function(_)
                            CashShopUtils.tryShowRemainOrderList(function()
                                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, requestKey)
                            end)
                        end)
                    end)
                end
            }
            CashShopUtils.tryShowRemainOrderList(function()
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_CashShopOrderSettle)
            end, interrupt)
        end, nil, true)
    end


    if not PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_MonthlypassPopup] and not Utils.isInDungeon() then
        PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_MonthlypassPopup] = true
        local needShowTimeStamps = GameInstance.player.monthlyPassSystem:GetNeedShowDailyPopupTimestamps()
        if needShowTimeStamps.Count > 0 then
            local needShowTimeStampsTable = {}
            for _, ts in pairs(needShowTimeStamps) do
                table.insert(needShowTimeStampsTable, ts)
            end
            LuaSystemManager.mainHudActionQueue:AddRequest(Name_LoginCheck_MonthlypassPopup, function(_)
                local ret = PhaseManager:OpenPhaseFast(PhaseId.ShopMonthlyPassPopUp, {
                    ShowTimeStamps = needShowTimeStampsTable,
                    EndCallback = function()
                        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_MonthlypassPopup)
                    end
                })
                if not ret then
                    logger.error("LoginCheck时打开PhaseId.ShopMonthlyPassPopUp失败!")
                    Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_MonthlypassPopup)
                end
            end, nil, true)
        end
    end


    if not PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_StartGuide] then
        PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_StartGuide] = true
        LuaSystemManager.mainHudActionQueue:AddRequest(Name_LoginCheck_StartGuide, function(_)
            GameInstance.player.guide:TryCheckAndStartGuideGroup()
        end, nil, true)
    end


    if not PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ForceSNS] then
        PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ForceSNS] = true
        LuaSystemManager.mainHudActionQueue:AddRequest(Name_LoginCheck_ForceSNS, function(_)
            GameInstance.player.sns:TryCheckAndStartSNSForceDialog()
        end, nil, true)
    end


    self:_PerformReflowPopup()


    self:_PerformLoginActivityCheck()

    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "PhaseLevel.PerformLoginCheck END")
end

PhaseLevel._PerformLoginActivityCheck = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipFocusModeCheck)

    if UNITY_EDITOR and BEYOND_DEBUG and CS.Beyond.DebugDefines.disableCheckInLoginCheck then
        return
    end

    if Utils.isInDungeon() or (not skipFocusModeCheck and Utils.isInFocusMode()) then
        return
    end


    if PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ActivityCheckIn] then
        return
    end
    PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ActivityCheckIn] = true


    if not PhaseManager:IsPhaseUnlocked(PhaseId.ActivityPopup) then
        return
    end

    if #ActivityUtils.getPopUpIds() == 0 then
        return
    end


    LuaSystemManager.mainHudActionQueue:AddRequest(Name_LoginCheck_ActivityCheckIn, function(_)


        if #ActivityUtils.getPopUpIds() == 0 then
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_ActivityCheckIn)
            return
        end
        local success = PhaseManager:OpenPhaseFast(PhaseId.ActivityPopup, {
            closeCallback = function()
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_ActivityCheckIn)
            end
        })
        if not success then
            logger.error("PhaseLevel.PerformActivityCheck 时打开 PhaseId.ActivityPopup 失败!")
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_ActivityCheckIn)
        end
    end, nil, true)
end



PhaseLevel._PerformImportantActivityPopup = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipFocusModeCheck)
    if PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ImportantActivity_Top]
            or PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ImportantActivity_Fallback] then
        return
    end
    if Utils.isInDungeon() or (not skipFocusModeCheck and Utils.isInFocusMode()) then
        return
    end
    if UNITY_EDITOR and BEYOND_DEBUG and CS.Beyond.DebugDefines.disableCheckInLoginCheck then
        return
    end
    local importantActivityId = ActivityUtils.findImportantCheckinActivity()
    if not importantActivityId then
        return
    end

    PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ImportantActivity_Top] = true
    PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_ImportantActivity_Fallback] = true

    LuaSystemManager.mainHudActionQueue:AddRequest(Name_LoginCheck_ImportantActivity_Fallback, function(_)
        if not ActivityUtils.findImportantCheckinActivity() then
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_ImportantActivity_Fallback)
            return
        end
        PhaseManager:OpenPhaseFast(PhaseId.ActivityImportantPopup, { requestKey = Name_LoginCheck_ImportantActivity_Fallback })
    end)
end



PhaseLevel._PerformReflowPopup = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipFocusModeCheck)
    if UNITY_EDITOR and BEYOND_DEBUG and CS.Beyond.DebugDefines.disableCheckInLoginCheck then
        return
    end
    if Utils.isInDungeon() or (not skipFocusModeCheck and Utils.isInFocusMode()) then
        return
    end

    if PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_Reflow] then
        return
    end
    PhaseLevel.s_LoginCheckFinishedInfo[Name_LoginCheck_Reflow] = true
    for id, _ in pairs(Tables.activityReflowTable) do
        local activity = GameInstance.player.activitySystem:GetActivity(id)
        if activity and ActivityUtils.shouldPopup(id) then
            LuaSystemManager.mainHudActionQueue:AddRequest(Name_LoginCheck_Reflow, function(_)
                ActivityUtils.recordPopup(id)
                local activity = GameInstance.player.activitySystem:GetActivity(id)
                if not activity or activity.oneTimeRewardReceived then
                    Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_Reflow)
                    return
                end
                local success = PhaseManager:OpenPhaseFast(PhaseId.ReflowPopup, {
                    activityId = id,
                    closeCallback = function()
                        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_Reflow)
                    end
                })
                if not success then
                    logger.error("PhaseLevel._PerformReflowPopup: 打开回流Popup失败!")
                    Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Name_LoginCheck_Reflow)
                end
            end, nil, true)
            break
        end
    end
end



PhaseLevel._OnSystemUnlockForImportantActivity = HL.Method(HL.Table) << function(self, _)
    if not PhaseLevel.s_LoginCheckFinishedInfo then
        return
    end
    if PhaseLevel.s_LoginCheckFinishedInfo.activityPopupAlreadyUnlocked then
        return
    end
    if not PhaseManager:IsPhaseUnlocked(PhaseId.ActivityPopup) then
        return
    end
    PhaseLevel.s_LoginCheckFinishedInfo.activityPopupAlreadyUnlocked = true
    self:_PerformImportantActivityPopup()
end

PhaseLevel._OnGameModeDisable = HL.Method(HL.Table) << function(self, args)
    local modeType, _ = unpack(args)


    if modeType == GEnums.GameModeType.Focus then
        self:_PerformImportantActivityPopup(true)
        self:_PerformReflowPopup(true)
        self:_PerformLoginActivityCheck(true)
    end
end






PhaseLevel._OnGachaPoolAllRewardsShown = HL.Method() << function(self)
    if not PhaseLevel.s_LoginCheckFinishedInfo or not PhaseLevel.s_LoginCheckFinishedInfo.CashShopOrderSettleDeferredToGachaPool then
        return
    end
    PhaseLevel.s_LoginCheckFinishedInfo.CashShopOrderSettleDeferredToGachaPool = nil
    if CashShopUtils.haveRemainOrders() then
        CashShopUtils.tryShowRemainOrderList()
    end
end




PhaseLevel.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}

    local opened, buildCtrl = UIManager:IsOpen(PanelId.FacBuildMode)
    if opened and buildCtrl.m_mode ~= FacConst.FAC_BUILD_MODE.Normal then
        arg.buildModeInfos = { buildCtrl.m_mode, buildCtrl.m_buildArgs, buildCtrl:GetRecoverBuildStateOnChangeDevice() }
    else
        arg.buildModeInfos = nil
    end

    arg.inDestroyMode = LuaSystemManager.factory.inDestroyMode
    arg.isSavingBP = arg.inDestroyMode and UIManager:IsShow(PanelId.FacSaveBlueprint)

    arg.isInTopView = FactoryUtils.isInTopView()
    arg.inFacMode = GameWorld.worldInfo.inFactoryMode
    if arg.isInTopView then
        arg.topViewArg = {
            targetPos = LuaSystemManager.factory.topViewCamTarget.transform.position,
            targetRot = LuaSystemManager.factory.topViewCamTarget.transform.eulerAngles,
            zoomValue = LuaSystemManager.factory:GetTopViewCamZoomValue(),
        }
        local _, topViewCtrl = UIManager:IsOpen(PanelId.FacTopView)
        arg.topViewArg.selectedTypeIndex = topViewCtrl.m_selectedTypeIndex
        arg.topViewArg.selectedFilters = topViewCtrl.m_selectedFilters
    else
        arg.topViewArg = nil
    end

    return arg
end

HL.Commit(PhaseLevel)
