local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MainHud





local TopBtnPosType = {
    AlwaysOutside = 1,
    TopViewInside = 2,
    AlwaysInside = 3,
}

local ControllerTopBtnPosTypes = {
    Fixed = 1,
    Dynamic = 2,
}

local ControllerDynamicTopBtnBaseOrderWhitRedDot = 1000
local ControllerDynamicTopBtnBaseOrder = 100000


MainHudCtrl = HL.Class('MainHudCtrl', uiCtrl.UICtrl)

local PhaseForbidStyle = CS.Beyond.Gameplay.PhaseForbidStyle
local DisableSwitchModeForbidStyle = CS.Beyond.Gameplay.DisableSwitchModeForbidParams.ForbidStyle





MainHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SQUAD_INFIGHT_CHANGED] = 'OnSquadInfightChanged',
    [MessageConst.ON_SET_IN_SAFE_ZONE] = 'OnSetInSafeZone',
    [MessageConst.ON_IN_FAC_MAIN_REGION_CHANGE] = 'OnInFacMainRegionChange',
    [MessageConst.ON_FAC_MODE_CHANGE] = 'OnFacModeChange',
    [MessageConst.ON_FAC_TOP_VIEW_HIDE_UI_MODE_CHANGE] = 'OnFacTopViewHideUIModeChange',
    [MessageConst.FAC_ON_PLAYER_POS_INFO_CHANGED] = 'OnFacPlayerPosInfoChanged',
    [MessageConst.FAC_SET_ENABLE_EXIT_FACTORY_MODE] = 'SetEnableExitFactoryMode',
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = 'OnSystemUnlock',
    [MessageConst.GAME_MODE_ENABLE] = 'OnGameModeChange',
    [MessageConst.ON_CHANGE_THROW_MODE] = 'OnThrowModeChange',
    [MessageConst.SHOW_WATER_DRONE_AIM] = 'OnShowWaterDroneAim',
    [MessageConst.HIDE_WATER_DRONE_AIM] = 'OnHideWaterDroneAim',

    [MessageConst.FAC_TOGGLE_TOP_VIEW] = 'FacToggleTopView',
    [MessageConst.ON_TOGGLE_FAC_TOP_VIEW] = 'OnToggleFacTopView',
    [MessageConst.DISABLE_BATTLE_INDICATOR_CONTROLLER] = 'OnDisableBattleIndicatorController',
    [MessageConst.ON_EXIT_FACTORY_MODE] = 'OnExitFactoryMode',
    [MessageConst.ON_TOGGLE_SPRINT] = 'OnToggleSprint',
    [MessageConst.ON_DASH_ENABLE_CONFIG_CHANGED] = 'OnDashEnableChanged',
    [MessageConst.ON_APPLICATION_FOCUS] = 'OnApplicationFocus',
    [MessageConst.ON_DOMAIN_DEVELOPMENT_UNLOCK] = 'OnDomainDevelopmentUnlock',
    [MessageConst.ON_SUB_GAME_STAGE_CHANGE] = "OnSubGameStageChange",

    [MessageConst.ON_ENTER_TOWER_DEFENSE_DEFENDING_PHASE] = 'OnEnterTowerDefenseDefendingPhase',
    [MessageConst.ON_TOWER_DEFENSE_LEVEL_REWARDS_FINISHED] = 'OnTowerDefenseDefendingRewardsFinished',

    [MessageConst.ON_BUILD_MODE_CHANGE] = 'OnBuildModeChange',
    [MessageConst.BEFORE_ENTER_BUILD_MODE] = 'BeforeEnterBuildMode',
    [MessageConst.ON_FAC_DESTROY_MODE_CHANGE] = 'OnFacDestroyModeChange',
    [MessageConst.BEFORE_ENTER_DESTROY_MODE] = 'BeforeEnterDestroyMode',

    [MessageConst.ON_GET_NEW_MAILS] = 'OnGetNewMails',
    [MessageConst.ON_GET_LOST_AND_FOUND] = 'OnLostAndFoundRefresh',
    [MessageConst.ON_ADD_LOST_AND_FOUND] = 'OnLostAndFoundRefresh',

    [MessageConst.OVERRIDE_JUMP_ACTION] = 'OverrideJump',
    [MessageConst.FAC_ON_FLUID_IN_BUILDING_REMOVED] = 'OnFluidInBuildingRemoved',

    [MessageConst.SET_MAIN_HUD_CAN_AUTO_STOP_EXPAND] = 'OnSetMainHudCanAutoStopExpand',

    [MessageConst.FORBID_SYSTEM_CHANGED] = 'TryUpdateAllTopBtnsVisible',
    [MessageConst.ON_TOGGLE_PHASE_FORBID] = 'TryUpdateAllTopBtnsVisible',

    [MessageConst.ON_LIMITED_GUIDE_WIKI_ENTRY_READ_STATE_CHANGE] = 'TryUpdateAllTopBtnsVisible',

    [MessageConst.AFTER_TOGGLE_UI_ACTION] = 'AfterToggleUiAction',
    [MessageConst.BLOCK_LUA_UI_INPUT] = 'OnBlockUIInput',
    [MessageConst.ON_QUEST_OBJECTIVE_UPDATE] = '_OnQuestObjectiveUpdate',

    [MessageConst.TOGGLE_FORBID_CHAR_FOOT_BAR] = 'ForbidCharFootBar',
    [MessageConst.TRY_SWITCH_FAC_MODE] = 'TrySwitchMode',

    [MessageConst.ON_OPEN_LEVEL_PHASE_SEAMLESS_LOADING] = '_OnOpenLevelPhaseSeamlessLoading',
}

MainHudCtrl.m_indicatorControllerGroupId = HL.Field(HL.Number) << 1

MainHudCtrl.m_indicatorControllerDisableKeys = HL.Field(HL.Table)

MainHudCtrl.m_characterFootBar = HL.Field(HL.Table)

MainHudCtrl.m_quickMenuBindingId = HL.Field(HL.Number) << -1


MainHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_hideJumpKeys = {}
    self.m_indicatorControllerDisableKeys = {}

    self:_InitOneTopNodeExpand(self.view.topRightBtns)
    self:_InitOneTopNodeExpand(self.view.topLeftBtns)

    self:_InitTopBtns()
    self:_InitMainHudBinding()
    self:_UpdateInventoryState(true)
    self:_UpdateSwitchModeState()
    self:_InitDebugAction()
    self.view.attackButton:InitAttackButton()

    self.m_characterFootBar = Utils.wrapLuaNode(CSUtils.CreateObject(self.view.config.CHARACTER_FOOT_BAR, UIManager.worldObjectRoot))

    if Utils.isSwitchModeDisabled() then




        self:BindInputPlayerAction("common_disable_switch_mode", function()
            if Utils.isInBlackbox() then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_BLACK_BOX_SWITCH_MODE_DISABLED)
            elseif Utils.isInSpaceShip() then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SPASCESHIP_SWITCH_MODE_DISABLED)
            end
        end)
    end
end

MainHudCtrl.OnShow = HL.Override() << function(self)
    self.m_forceBtnOutside = Utils.isInDungeon()
    self.m_forceBtnInside = GameWorld.battle.isSquadInFight

    self.view.jumpAnimNode:SampleClipAtPercent("mobile_mainhud_jumpbtn_pressedring", 0)
    self.view.sprintAnimNode:SampleClipAtPercent("mobile_mainhud_jumpbtn_pressedring", 0)
    self.view.sprintLightSuccess:SampleClipAtPercent("mobile_mainhud_jumpbtn_release_success", 0)

    self.view.topRightBtns.controllerBtnList.gameObject:SetActive(DeviceInfo.usingController)
    self:UpdateAllTopBtnsVisible()

    RedDotManager:TriggerUpdate("Mail")
    self:_CheckShowMailBtnBubble()

    if Utils.isInFactoryMode() then
        self.view.topLeftBtns.animationWrapper:SampleToInAnimationEnd()
    else
        self.view.topLeftBtns.animationWrapper:SampleToOutAnimationEnd()
    end
    if not Utils.isInFactoryMode() and CS.Beyond.UI.BattleControllerInputController.instance:GetControllerIndicatorState() then
        self:_ToggleControllerIndicator(true)
    end
    self.view.attackButton:OnShow()
    self.m_characterFootBar.mainCharFootBar:SetUIDisable("MainHudActive", false)
    self:OnToggleSprint({ GameInstance.playerController.isMainCharacterSprinting })
    self:_UpdateWatchBtnBinding()
    self:OnThrowModeChange(nil)
    ActivityUtils.queryAllWebActivityPortalStates()
end

MainHudCtrl.OnHide = HL.Override() << function(self)
    self:_ToggleControllerIndicator(false)
    self:CheckNormalAttackBtn(false)
    self.view.topLeftBtns.expandNode:SetExpanded(false, true)
    self.view.topRightBtns.expandNode:SetExpanded(false, true)
    self.view.attackButton:OnHide()
    if self.m_mailBubbleShowingState ~= 0 then
        self.m_mailBubbleShowingState = 0
        self.m_mainBubbleCor = self:_ClearCoroutine(self.m_mainBubbleCor)
        self.view.topLeftBtns.mailBubbleImg.gameObject:SetActive(false)
    end
    self.m_characterFootBar.mainCharFootBar:SetUIDisable("MainHudActive", true)
end

MainHudCtrl.OnClose = HL.Override() << function(self)
    self.view.attackButton:ReleaseNormalAttackBtn()
    GameInstance.playerController:ToggleIndicatorAttack(false)
    if self.m_characterFootBar then
        GameObject.Destroy(self.m_characterFootBar.gameObject)
    end
    self.m_characterFootBar = nil
end

MainHudCtrl.ExtractHotSwitchRuntimeState = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    self:_ResetTopBtnLayoutForHotSwitchReuse()
    return nil
end

MainHudCtrl._ResetTopBtnLayoutForHotSwitchReuse = HL.Method() << function(self)
    if not self.m_topBtnDataList then
        return
    end
    for _, info in ipairs(self.m_topBtnDataList) do
        if info.oriParentTrans and info.viewNode then
            if info.viewNode.transform.parent ~= info.oriParentTrans then
                info.viewNode.transform:SetParent(info.oriParentTrans)
                info.viewNode.transform.localScale = Vector3.one
            end
            if info.posType then
                local canvasGroup = info.viewNode.transform:GetComponent("CanvasGroup")
                if NotNull(canvasGroup) then
                    canvasGroup:DOKill()
                    canvasGroup.alpha = 1
                end
            end
        end
    end
    self.view.topRightBtns.controllerBtnList.moreDeco.gameObject:SetActive(false)
    self.view.topRightBtns.controllerBtnList.gameObject:SetActive(false)
end



MainHudCtrl.m_topBtnDataMap = HL.Field(HL.Table)

MainHudCtrl.m_topBtnDataList = HL.Field(HL.Table)

MainHudCtrl._BuildTopBtnData = HL.Method() << function(self)
    self.m_topBtnDataMap = {
        top = {
            viewNode = self.view.topNode,
            checkVisible = function()
                if Utils.isForbidden(ForbidType.ForbidMainHudTopBtns) then
                    return false
                end
                if GameWorld.worldInfo.curLevelId == Tables.spaceshipConst.visitSceneName then
                    return false
                end
                if FactoryUtils.isInBuildMode() then
                    return false
                end
                if LuaSystemManager.factory.inDestroyMode then
                    return false
                end
                return true
            end,
            canStayInTowerDefenseDefending = true,
            canStayInFocusMode = true,
        },
        bottomRight = {
            viewNode = self.view.bottomRightNode,
            checkVisible = function()
                if LuaSystemManager.factory.inTopView then
                    return false
                end
                return true
            end,
            canStayInTowerDefenseDefending = true,
            canStayInFocusMode = true,
        },
        exitDungeon = {
            button = self.view.topLeftBtns.exitDungeonBtn,
            checkVisible = function()
                return DungeonUtils.showExitDungeonBtn()
            end,
            onClick = function()
                DungeonUtils.onClickExitDungeonBtn()
            end
        },
        exitFocusMode = {
            button = self.view.topLeftBtns.exitFocusModeBtn,
            checkVisible = function()
                return FocusModeUtils.isInFocusMode
            end,
            onClick = function()
                Notify(MessageConst.SHOW_POP_UP, {
                    content = Language.LUA_EXIT_FOCUS_MODE_POP_UP,
                    freezeWorld = true,
                    onConfirm = function()
                        if PhaseManager:IsOpen(PhaseId.CharInfo) then
                            PhaseManager:ExitPhaseFast(PhaseId.CharInfo)
                        end
                        CS.Beyond.Gameplay.FocusModeUtils.OnBtnLeaveFocusMode()
                    end,
                    showGameSettingBtn = true,
                })
            end,
            canStayInFocusMode = true,
        },
        switchMode = {
            viewNode = self.view.topLeftBtns.switchModeNode,
            toggle = self.view.topLeftBtns.switchModeNode.toggle,
            checkVisible = function()
                if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacMode) then
                    return false
                end
                local isForbid, forbidParams = Utils.isForbiddenWithReason(ForbidType.DisableSwitchMode)
                if isForbid and forbidParams and forbidParams.forbidStyle ~= DisableSwitchModeForbidStyle.ShowInvalidIcon then
                    return false
                end
                if not Utils.isCurrentMapHasFactoryGrid() then
                    return false
                end
                return true
            end,
            checkIsValueValid = function(isOn)
                local valid, toast = self:_CheckSwitchModeValueValid(isOn)
                if not valid then
                    Notify(MessageConst.SHOW_TOAST, toast)
                    AudioAdapter.PostEvent("au_ui_fac_mode_fail")
                end
                return valid
            end,
            onValueChanged = function(isOn)
                self:_SwitchMode(isOn)
            end,
            getCurValue = function()
                return Utils.isInFactoryMode()
            end,
        },
        hubData = {
            button = self.view.topLeftBtns.hubDataBtn,
            checkVisible = function()
                if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacYieldStats) then
                    return false
                end
                if not Utils.isCurrentMapHasFactoryGrid() or Utils.isInBlackbox() then
                    return false
                end
                return true
            end,
            onClick = function()
                PhaseManager:OpenPhase(PhaseId.FacHUBData, { tabIndex = 1 })
            end,
        },
        emptySwitch = {
            viewNode = self.view.topLeftBtns.emptySwitchNode,
            checkVisible = function()
                local isForbid, forbidParams = Utils.isForbiddenWithReason(ForbidType.DisableSwitchMode)
                if isForbid and forbidParams and forbidParams.forbidStyle == DisableSwitchModeForbidStyle.ShowEmptyBtn then
                    return true
                end
                return false
            end,
        },
        techTree = {
            button = self.view.topLeftBtns.techTreeBtn,
            redDotView = self.view.topLeftBtns.techTreeRedDot,
            phaseId = PhaseId.FacTechTree,
            checkVisible = function()
                return not Utils.isInDungeon()
            end,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 4,
        },
        dungeonInfo = {
            button = self.view.topLeftBtns.dungeonInfoBtn,
            checkVisible = function()
                return DungeonUtils.checkVisibilityDungeonInfoBtn()
            end,
            onClick = function()
                DungeonUtils.onClickDungeonInfoBtn()
            end,
            controllerPosType = ControllerTopBtnPosTypes.Fixed,
            controllerPosOrder = 1,
        },
        ContingencyContractInfoBtn={
            button = self.view.topLeftBtns.contingencyContractInfoBtn,
            checkVisible = function()
                return ContingencyContractUtils.CheckVisibilityDungeonInfoBtn()
            end,
            onClick = function()
                ContingencyContractUtils.OnClickDungeonInfoBtnContingencyContract()
            end,
            customUpdateBtnInfo = function()
                ContingencyContractUtils.UpdateMainHudInfoBtn(self.view)
            end,
        },
        domain = {
            button = self.view.topLeftBtns.domainBtn,
            redDotView = self.view.topLeftBtns.domainRedDot,
            phaseId = PhaseId.DomainMain,
            checkVisible = function()
                if Utils.isInDungeon() then
                    return
                end
                local isUnlock = Utils.isSystemUnlocked(GEnums.UnlockSystemType.DomainDevelopment) and GameInstance.player.domainDevelopmentSystem.domainDevDataDic.Count > 0
                return isUnlock
            end,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 3,
        },
        hub = {
            button = self.view.topLeftBtns.hubBtn,
            checkVisible = function()
                return Utils.isInDungeonFactory()
            end,
            onClick = function()
                Notify(MessageConst.FAC_OPEN_NEAREST_BUILDING_PANEL, { FacConst.HUB_DATA_ID, true })
            end,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Fixed,
            controllerPosOrder = 5,
        },
        controlCenter = {
            button = self.view.topLeftBtns.controlCenterBtn,
            checkVisible = function()
                return Utils.isInSpaceShip()
            end,
            phaseId = PhaseId.SpaceshipControlCenter,
            phaseArgs = { fromMainHud = true },
            posType = TopBtnPosType.AlwaysOutside,
            redDotView = self.view.topLeftBtns.controlCenterRedDot,
            controllerPosType = ControllerTopBtnPosTypes.Fixed,
            controllerPosOrder = 4,
        },

        watch = {
            button = self.view.topRightBtns.watchBtn,
            redDotView = self.view.topRightBtns.watchRedDot,
            redDotName = "WatchBtn",
            phaseId = PhaseId.Watch,
            onClick = function()
                PhaseManager:OpenPhase(PhaseId.Watch)
            end,
        },
        simpleMenu = {
            button = self.view.topRightBtns.simpleMenuBtn,
            phaseId = PhaseId.SimpleSystem,
            checkVisible = function()
                local show = not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Watch)
                if Utils.isInDungeon() then
                    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Watch) then
                        show = false
                    end
                end
                return show
            end,
        },
        simpleMenuForForbidden = {
            button = self.view.simpleMenuBtnForForbidden,
            phaseId = PhaseId.SimpleSystem,
            checkVisible = function()
                local forbidParams = Utils.getForbiddenReason(ForbidType.ForbidMainHudTopBtns)
                return forbidParams and forbidParams.showSettingBtn
            end,
        },
        inventory = {
            button = self.view.topRightBtns.inventoryBtn,
            redDotView = self.view.topRightBtns.inventoryRedDot,
            phaseId = PhaseId.Inventory,
            posType = TopBtnPosType.AlwaysOutside,

            getControllerPosInfo = function()
                if Utils.isInBlackbox() then
                    return ControllerTopBtnPosTypes.Fixed, 4
                else
                    return ControllerTopBtnPosTypes.Dynamic, 6
                end
            end,

            icon = self.view.topRightBtns.inventoryBtnNormalIcon,
            iconSpriteGetter = function()
                if WeeklyRaidUtils.IsInWeeklyRaid() or WeeklyRaidUtils.IsInWeeklyRaidIntro() then
                    return "btn_week_raid_backpack"
                end
                return "btn_backpack"
            end
        },
        valuableDepot = {
            button = self.view.topRightBtns.valuableDepotBtn,
            redDotName = "ValuableDepotInMainHud",
            redDotView = self.view.topRightBtns.valuableDepotRedDot,
            phaseId = PhaseId.ValuableDepot,
            posType = TopBtnPosType.AlwaysInside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 8,
        },
        character = {
            button = self.view.topRightBtns.characterBtn,
            phaseId = PhaseId.CharInfo,
            canStayInFocusMode = true,
            redDotView = self.view.topRightBtns.charRedDot,
            posType = TopBtnPosType.TopViewInside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 2,
        },
        formation = {
            button = self.view.topRightBtns.formationBtn,
            phaseId = PhaseId.CharFormation,
            posType = TopBtnPosType.AlwaysInside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 10,
            checkVisible = function()
                return not Utils.isForbidden(ForbidType.ForbidSetSquad)
            end,
            onClick = function()
                if Utils.isForbidden(ForbidType.ForbidSetSquad) then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
                    return
                end
                PhaseManager:OpenPhase(PhaseId.CharFormation)
            end
        },
        mail = {
            button = self.view.topLeftBtns.mailBtn,
            redDotView = self.view.topLeftBtns.mailRedDot,
            phaseId = PhaseId.Mail,
            checkVisible = function()
                return RedDotManager:GetRedDotState("Mail") and not Utils.isInDungeon()
            end,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 0,
        },
        adventureBook = {
            button = self.view.topRightBtns.adventureBookBtn,
            phaseId = PhaseId.AdventureBook,
            redDotView = self.view.topRightBtns.adventureBookRedDot,
            checkVisible = function()
                return not Utils.isInDungeon()
            end,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Fixed,
            controllerPosOrder = 1,
        },
        gacha = {
            button = self.view.topRightBtns.gachaBtn,
            phaseId = PhaseId.GachaPool,
            redDotView = self.view.topRightBtns.gachaRedDot,
            posType = TopBtnPosType.TopViewInside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 1,
            checkVisible = function()
                return not (WeeklyRaidUtils.IsInWeeklyRaid() or WeeklyRaidUtils.IsInWeeklyRaidIntro())
            end,
        },
        weekRaid = {
            button = self.view.topLeftBtns.weekRaidBtn,
            checkVisible = function()
                return WeeklyRaidUtils.IsInWeeklyRaid()
            end,
            onClick = function()
                PhaseManager:OpenPhase(PhaseId.DungeonWeeklyRaid,{strPanelId = "DungeonWeeklyRaid" , isPreview = true})
            end,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 4,
        },
        weekRaidTipInfo = {
            button = self.view.topLeftBtns.weekRaidInfoBtn,

            checkVisible = function()
                return WeeklyRaidUtils.IsInWeeklyRaid() or WeeklyRaidUtils.IsInWeeklyRaidIntro()
                    or GameInstance.mode.modeType == GEnums.GameModeType.Racing
            end,
            onClick = function()
                if WeeklyRaidUtils.IsInWeeklyRaid() or WeeklyRaidUtils.IsInWeeklyRaidIntro() then
                    Notify(MessageConst.SHOW_INTRO, "week_raid")
                elseif GameInstance.mode.modeType == GEnums.GameModeType.Racing then
                    ActivityUtils.RacingDungeonShowIntro()
                end
            end,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 3,
        },

        dungeonRacing = {
            button = self.view.topLeftBtns.coinTaskBtn,
            checkVisible = function()
                return GameInstance.mode.modeType == GEnums.GameModeType.Racing
            end,
            onClick = function()
                local activityData = GameInstance.player.activitySystem:GetActivityOfCertainType(CS.Beyond.GEnums.ActivityType.RacingDungeon)
                if activityData.Count > 0 then
                    PhaseManager:OpenPhase(PhaseId.ActivityCoinReward, {
                        activityId = activityData[0],
                        openInDungeon = true,
                    })
                end
            end,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 4,
        },

        controllerMission = {
            button = self.view.topRightBtns.controllerBtnList.controllerMissionBtn,
            phaseId = PhaseId.Mission,
            redDotView = self.view.topRightBtns.controllerBtnList.controllerMissionRedDot,
            checkVisible = function()
                if Utils.isForbidden(ForbidType.ForbidMissionHudShowNonTracking) then
                    return false
                end
                if Utils.isForbidden(ForbidType.ForbidJumpToMissionPanelFromHud) then
                    return false
                end
                return DeviceInfo.usingController
            end,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 9,
        },
        controllerSNS = {
            button = self.view.topRightBtns.controllerBtnList.controllerSNSBtn,
            phaseId = PhaseId.SNS,
            redDotView = self.view.topRightBtns.controllerBtnList.controllerSNSRedDot,
            checkVisible = function()
                return DeviceInfo.usingController and not GameInstance.mode.hideSNSHud
            end,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 7,
        },

        activityCenter = {
            button = self.view.topRightBtns.activityBtn,
            phaseId = PhaseId.ActivityCenter,
            phaseArgs = {
                openFrom = "MainHud"
            },
            redDotView = self.view.topRightBtns.activityRedDot,
            posType = TopBtnPosType.TopViewInside,
            controllerPosType = ControllerTopBtnPosTypes.Fixed,
            controllerPosOrder = 2,
            checkVisible = function()
                return Utils.isSystemUnlocked(GEnums.UnlockSystemType.Activity)
            end,



            onClick = function()
                local args = { openFrom = "MainHud" }
                local isOpen, reminderCtrl = UIManager:IsOpen(PanelId.ActivityStartReminder)
                if isOpen and reminderCtrl then
                    local bubbleActivityId = reminderCtrl:GetShowingActivityId()
                    if not string.isEmpty(bubbleActivityId) then
                        args.gotoCenter = true
                        args.activityId = bubbleActivityId
                    end
                end
                PhaseManager:OpenPhase(PhaseId.ActivityCenter, args)
            end,
        },

        wikiGuide = {
            button = self.view.topLeftBtns.wikiGuideBtn,
            redDotName = "WikiLimitedGuide",
            redDotView = self.view.topLeftBtns.wikiGuideRedDot,
            posType = TopBtnPosType.AlwaysOutside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 10,
            checkVisible = function()
                local guideLimitedCtrl = require_ex('UI/Panels/GuideLimited/GuideLimitedCtrl')

                local isDefaultGameMode = GameInstance.mode.modeType == GEnums.GameModeType.Default
                return isDefaultGameMode and not string.isEmpty(guideLimitedCtrl.GuideLimitedCtrl.s_waitReadGuideWikiEntry)
            end,
            onClick = function()
                local guideLimitedCtrl = require_ex('UI/Panels/GuideLimited/GuideLimitedCtrl')
                Notify(MessageConst.SHOW_WIKI_ENTRY, {
                    wikiEntryId = guideLimitedCtrl.GuideLimitedCtrl.s_waitReadGuideWikiEntry,
                })
            end
        },
        battlePass = {
            button = self.view.topRightBtns.battlePassBtn,
            phaseId = PhaseId.BattlePass,
            redDotView = self.view.topRightBtns.battlePassRedDot,
            posType = TopBtnPosType.AlwaysInside,
            controllerPosType = ControllerTopBtnPosTypes.Fixed,
            controllerPosOrder = 3,
            onClick = function()
                if GameInstance.player.gameSettingSystem.forbiddenBp then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_SWITCH_TYPE_FORBIDDEN_TOAST)
                    return
                end
                if not BattlePassUtils.CheckBattlePassSeasonValid() then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_BATTLEPASS_CANNOT_OPEN_TOAST)
                    return
                end
                PhaseManager:OpenPhase(PhaseId.BattlePass)
            end,
        },

        cashShop = {
            button = self.view.topRightBtns.cashShopBtn,
            phaseId = PhaseId.CashShop,
            redDotView = self.view.topRightBtns.cashShopBtnRedDot,
            posType = TopBtnPosType.AlwaysInside,
            controllerPosType = ControllerTopBtnPosTypes.Dynamic,
            controllerPosOrder = 3.1,
        },







    }
end

MainHudCtrl._InitTopBtns = HL.Method() << function(self)
    self:_BuildTopBtnData()

    self.m_topBtnDataList = {}
    for k, info in pairs(self.m_topBtnDataMap) do
        info.id = k
        self:_InitSingleTopBtn(info)
        table.insert(self.m_topBtnDataList, info)
    end
    table.sort(self.m_topBtnDataList, Utils.genSortFunction({ "sortId" }, true))

    self:UpdateAllTopBtnsIcon()
end

MainHudCtrl._InitSingleTopBtn = HL.Method(HL.Table) << function(self, info)
    if not info.viewNode then
        info.viewNode = info.button or info.toggle
    end
    if info.phaseId then
        if not info.redDotName then
            info.redDotName = PhaseManager:GetPhaseRedDotName(info.phaseId)
        end
    end
    local hasPosType = info.posType or info.controllerPosType or info.getControllerPosInfo
    if info.redDotView then
        if hasPosType then
            local isInit = true
            info.redDotView:InitRedDot(info.redDotName, nil, function(redDot, active, rdType)
                if not isInit then
                    self:_OnAfterApplyRedDotSate(info, active)
                end
            end)
            isInit = false
        else
            info.redDotView:InitRedDot(info.redDotName)
        end
    end
    if info.button then
        info.button.onClick:RemoveAllListeners()
        info.button.onClick:AddListener(function()
            self:OnMainHudBtnClick(info)
        end)
    end
    if info.toggle then
        info.toggle.onValueChanged:RemoveAllListeners()
        info.toggle.isOn = info.getCurValue()
        info.toggle.onValueChanged:AddListener(function(isOn)
            info.onValueChanged(isOn)
        end)
        if info.checkIsValueValid then
            info.toggle.checkIsValueValid = function(isOn)
                return info.checkIsValueValid(isOn)
            end
        end
    end

    if hasPosType then

        info.oriParentTrans = info.viewNode.transform.parent
        if info.posType then
            if info.viewNode.transform:IsChildOf(self.view.topLeftBtns.transform) then
                info.belongNode = self.view.topLeftBtns
            elseif info.viewNode.transform:IsChildOf(self.view.topRightBtns.transform) then
                info.belongNode = self.view.topRightBtns
            else
                logger.error("No Valid Belong Node", info.viewNode.transform:PathFromRoot())
            end

            info.sortId = info.viewNode.transform:GetSiblingIndex()
        end
    end
end

MainHudCtrl.OnMainHudBtnClick = HL.Method(HL.Table) << function(self, info)
    if info.onClick then
        info.onClick()
    else
        PhaseManager:OpenPhase(info.phaseId, info.phaseArgs)
    end
end

MainHudCtrl.GetMainHudBtnInfo = HL.Method(HL.String).Return(HL.Table) << function(self, infoId)
    return self.m_topBtnDataMap[infoId]
end

MainHudCtrl.IsMainHudBtnVisible = HL.Method(HL.Table).Return(HL.Boolean) << function(self, info)
    return self:_GetSingleTopBtnVisible(info)
end

MainHudCtrl.m_updateTimerId = HL.Field(HL.Number) << -1

MainHudCtrl.TryUpdateAllTopBtnsVisible = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    if self.m_updateTimerId > 0 then

        return
    end
    self.m_updateTimerId = self:_StartTimer(0, function()
        if IsNull(self.view.gameObject) then
            return
        end
        self.m_updateTimerId = -1
        self:UpdateAllTopBtnsVisible()
        self:OnForbidSystemChanged()
    end)
end

MainHudCtrl.OnForbidSystemChanged = HL.Method() << function(self)
    if Utils.isForbidden(ForbidType.ForbidMove) then
        GameInstance.playerController:UpdateMoveCommand(Vector2.zero)
    end

    if Utils.isForbidden(ForbidType.ForbidSprint) then
        self.view.sprintBtn.gameObject:SetActive(false)
        self:_OnReleaseSprint()
    else
        self.view.sprintBtn.gameObject:SetActive(true)
    end
    if Utils.isForbidden(ForbidType.ForbidMainHudTopBtns) then
        self:UpdateAllTopBtnsVisible()
    end

    local forbidJump = Utils.isForbidden(ForbidType.ForbidJump)
    self:TogglePlayerJump({"ForbidSystem", forbidJump})
end

MainHudCtrl.UpdateAllTopBtnsVisible = HL.Method() << function(self)
    if DeviceInfo.usingController then
        self:_UpdateAllTopBtnsVisibleInController()
    else
        self:_ResetTopNodePosInfo(self.view.topLeftBtns)
        self:_ResetTopNodePosInfo(self.view.topRightBtns)

        for _, info in ipairs(self.m_topBtnDataList) do
            self:_UpdateSingleTopBtnVisible(info)
        end
        self:_UpdateTopNodeExpandState(self.view.topLeftBtns)
        self:_UpdateTopNodeExpandState(self.view.topRightBtns)
    end
end

MainHudCtrl._UpdateSingleTopBtnVisible = HL.Method(HL.Table, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, info, playAnimation, visible)
    if visible == nil then
        visible = self:_GetSingleTopBtnVisible(info)
    end
    if info.viewNode then
        if playAnimation then
            UIUtils.PlayAnimationAndToggleActive(info.viewNode, visible)
        else
            info.viewNode.gameObject:SetActive(visible)
        end
    else
        logger.error("No viewNode on", info)
    end
    if visible then
        self:_UpdateBtnPos(info)
        if info.customUpdateBtnInfo then
            info.customUpdateBtnInfo()
        end
    end
end

MainHudCtrl._UpdateBtnPos = HL.Method(HL.Table) << function(self, info)
    if not info.posType then
        return
    end
    local isOutside
    if self.m_forceBtnOutside then
        isOutside = true
    elseif self.m_forceBtnInside then
        isOutside = false
    elseif info.posType == TopBtnPosType.AlwaysOutside then
        isOutside = true
    else
        if info.posType == TopBtnPosType.TopViewInside then
            isOutside = not LuaSystemManager.factory.inTopView
        elseif info.posType == TopBtnPosType.AlwaysInside then
            isOutside = false
        end
        if not isOutside and not LuaSystemManager.factory.inTopView then

            if info.redDotView and info.redDotView.curIsActive then
                if not info.belongNode.m_flexibleOutsideBtnInfo then
                    info.belongNode.m_flexibleOutsideBtnInfo = info
                    isOutside = true
                else
                    info.belongNode.expandRedDot:ApplyState(true)
                end
            end
        end
    end
    info.isOutside = isOutside
    if isOutside then
        info.viewNode.transform:SetParent(info.oriParentTrans)
        local canvasGroup = info.viewNode.transform:GetComponent("CanvasGroup")
        if NotNull(canvasGroup) then
            canvasGroup:DOKill()
            canvasGroup.alpha = 1
        end
    else
        info.viewNode.transform:SetParent(info.belongNode.expandNode.transform)
        if info.belongNode.m_curInsideBtnCount then
            info.belongNode.m_curInsideBtnCount = info.belongNode.m_curInsideBtnCount + 1
        end
    end
    info.viewNode.transform.localScale = Vector3.one
    info.viewNode.transform:SetAsLastSibling()
end

MainHudCtrl.UpdateAllTopBtnsIcon = HL.Method() << function(self)
    for _, info in ipairs(self.m_topBtnDataList) do
        if info.icon and info.iconSpriteGetter then
            local spriteName = info.iconSpriteGetter()
            if info.icon.sprite.name ~= spriteName then
                info.icon:LoadSprite(UIConst.UI_SPRITE_MAIN_HUD, spriteName)
            end
        end
    end
end

MainHudCtrl._GetSingleTopBtnVisible = HL.Method(HL.Table).Return(HL.Boolean) << function(self, info)

    if Utils.isInSettlementDefenseDefending() then
        if not info.canStayInTowerDefenseDefending then
            return false
        end
    end

    if FocusModeUtils.isInFocusMode then
        if not info.canStayInFocusMode then
            return false
        end
    end


    if info.onlyInFacMode then
        if not Utils.isInFactoryMode() then
            return false
        end
    end
    if info.hideInFacMode then
        if Utils.isInFactoryMode() then
            return false
        end
    end

    if info.phaseId then
        if not PhaseManager:IsPhaseUnlocked(info.phaseId) then
            return false
        end
        local isPhaseForbidden, forbidStyle = PhaseManager:IsPhaseForbidden(info.phaseId)
        if isPhaseForbidden and forbidStyle == PhaseForbidStyle.HideEntrance then
            return false
        end
    end

    if info.checkVisible then
        local rst = info.checkVisible()
        return rst == true
    end
    return true
end






MainHudCtrl._InitMainHudBinding = HL.Method() << function(self)

    self.view.sprintBtn.onPressStart:AddListener(function()
        self:_OnPressSprint()
    end)
    self.view.sprintBtn.onPressEnd:AddListener(function()
        self:_OnReleaseSprint()
    end)
    self.view.jumpBtn.onPressStart:AddListener(function()
        self:_OnPressJump()
    end)
    self.view.sprintBtnDragHandler.onDrag:AddListener(function(eventData)
        self:_OnDragSprint(eventData)
    end)
    self.view.attackButtonDragHandler.onDrag:AddListener(function(eventData)
        self:_OnDragAttack(eventData)
    end)
    self.view.jumpBtnDragHandler.onDrag:AddListener(function(eventData)
        self:_OnDragJump(eventData)
    end)







    self.m_quickMenuBindingId = self:BindInputPlayerAction("common_open_quick_menu_start", function()
        UIManager:Open(PanelId.QuickMenu)
    end, self.view.topNodeInputGroup.groupId)


    self.m_indicatorControllerGroupId = self:CreateInputGroup(self.view.inputGroup.groupId)
    self:BindInputPlayerAction("common_indicator_start", function()
        self:_ToggleControllerIndicator(true)
    end, self.m_indicatorControllerGroupId)
    self:BindInputPlayerAction("common_indicator_end", function()
        self:_ToggleControllerIndicator(false)
    end, self.m_indicatorControllerGroupId)


    if not UNITY_EDITOR and DeviceInfo.isAndroid then
        self:BindInputPlayerAction("common_quit_game", function()
            self:Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_QUIT_GAME_CONFIRM,
                hideBlur = true,
                onConfirm = function()
                    logger.info("[MainHud] QuitGame triggered")
                    CSUtils.QuitGame(0)
                end,
            })
        end)
    end


    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidSprint, "Unlock", not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Dash));
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Jump) then
        self:TogglePlayerJump({"system_unlock", true})
    end
    self:OnDashEnableChanged({GameWorld.battle.dashEnable})
end

MainHudCtrl.OnBlockUIInput = HL.Method(HL.Table) << function(self, arg)
    local active = InputManagerInst:IsGroupEnabled(self.view.inputGroup.groupId)
    self:_OnPanelInputBlocked(active)
end

MainHudCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    self:_UpdateSprintInfo(active)

    if active then
        self:_CheckControllerIndicatorEnabled()
    else
        self:_ToggleControllerIndicator(false)
    end
    self:CheckNormalAttackBtn(active)
end

MainHudCtrl.AfterToggleUiAction = HL.Method(HL.Table) << function(self, arg)
    local isShow, isUltimate = unpack(arg)

    self:_UpdateSprintInfo(isShow)
    if isShow then
        self:_CheckControllerIndicatorEnabled()
        if isUltimate then
            InputManagerInst:ChangeParent(true, self.view.attackButton.view.button.groupId, self.view.inputGroup.groupId)
        end
        self:CheckNormalAttackBtn(true)
    else
        if isUltimate then
            InputManagerInst:ChangeParent(true, self.view.attackButton.view.button.groupId, InputManagerInst.rootGroupId)
            if not DeviceInfo.usingTouch then

                self:CheckNormalAttackBtn(false)
            end
        else
            self:CheckNormalAttackBtn(false)
        end
        self:_ToggleControllerIndicator(false)
    end
end

MainHudCtrl.CheckNormalAttackBtn = HL.Method(HL.Boolean) << function(self, active)
    if active and InputManagerInst:IsGroupEnabled(self.view.attackButton.view.button.groupId) then
        if CS.Beyond.UI.BattleControllerInputController.instance:CheckNormalAttackBtn() then
            self.view.attackButton:StartPressAttackBtn()
        else
            if not DeviceInfo.usingTouch then

                self.view.attackButton:ReleaseNormalAttackBtn()
            end
        end
    else
        self.view.attackButton:ReleaseNormalAttackBtn()
    end
end

MainHudCtrl._InitDebugAction = HL.Method() << function(self)
    if not BEYOND_DEBUG_COMMAND then
        return
    end
    self:BindInputPlayerAction("battle_debug_flying_mode", function()
        CS.Beyond.Gameplay.Core.PlayerController.ToggleFlyingMode()
    end)
    self:BindInputPlayerAction("battle_debug_get_debug_info", function()
        CS.Beyond.Gameplay.Core.PlayerController.GetDebugInfo()
    end)
end

MainHudCtrl.OnExitFactoryMode = HL.Method() << function(self)
end






MainHudCtrl._OnQuestObjectiveUpdate = HL.Method(HL.Any) << function(self, arg)
    local questId = unpack(arg)
    if not WeeklyRaidUtils.IsInWeeklyRaid() then
        self.view.topLeftBtns.weekRaidBubbleImg.gameObject:SetActiveIfNecessary(false)
        return
    end
    local missionId = GameInstance.player.mission:GetMissionIdByQuestId(questId)
    local completed = GameInstance.player.weekRaidSystem:IsMissionCompleted(missionId)
    if not completed then
        return
    end
    for i = 0, GameInstance.player.weekRaidSystem.scheduledMission.Count - 1 do
        local delegateId = GameInstance.player.weekRaidSystem.scheduledMission[i]
        local _, delegateCfg = Tables.weekRaidDelegateTable:TryGetValue(delegateId)
        if delegateCfg then
            local displayQuestIds = GameInstance.player.mission:GetDisplayQuestIdsByMissionId(delegateCfg.missionId)
            if displayQuestIds ~= nil and displayQuestIds.Count ~= 0 then
                if questId == displayQuestIds[0] then


                    if not GameInstance.player.weekRaidSystem.weekRaidGame.ShowToastDelegates:Contains(delegateId) then
                        GameInstance.player.weekRaidSystem.weekRaidGame.ShowToastDelegates:Add(delegateId)
                        self.view.topLeftBtns.weekRaidBubbleImg.gameObject:SetActiveIfNecessary(true)
                        self.view.topLeftBtns.weekRaidBubbleImg:SetState(delegateCfg.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission and "MainMission" or "Normal")
                        self.view.topLeftBtns.weekRaidBubbleTxt.text = WeeklyRaidUtils.GetWeeklyRaidMissionText(missionId).name
                        coroutine.start(function()

                            coroutine.wait(4)
                            self.view.topLeftBtns.weekRaidBubbleImg.gameObject:SetActiveIfNecessary(false)
                        end)
                    end
                    return
                end
            end
        end
    end
end


MainHudCtrl.s_clearScreenIdExceptSomePanel = HL.StaticField(HL.Number) << 0

MainHudCtrl.s_waitingToClearScreenExceptPanels = HL.StaticField(HL.Table) << nil

MainHudCtrl.s_clearScreenId = HL.StaticField(HL.Number) << 0

MainHudCtrl.s_waitingToClearScreen = HL.StaticField(HL.Boolean) << false

MainHudCtrl._OnClearScreenOn = HL.StaticMethod() << function()

    if MainHudCtrl.s_clearScreenId and MainHudCtrl.s_clearScreenId > 0 then
        print("MainHudCtrl._OnClearScreenOn: try clear screen while screen is being cleared.")
        return
    end

    if MainHudCtrl.s_waitingToClearScreen then
        print("MainHudCtrl._OnClearScreenOn: try clear screen while waiting for clear screen.")
        return
    end

    if not LuaSystemManager.mainHudActionQueue:IsInLoginCheck() then

        MainHudCtrl._PerformClearScreen()
        return
    end

    MainHudCtrl.s_waitingToClearScreen = true

    LuaSystemManager.mainHudActionQueue:AddRequest(Const.LevelScriptClearScreenQueueType, function(_)


        if MainHudCtrl.s_waitingToClearScreen then
            MainHudCtrl._PerformClearScreen()
        end

        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Const.LevelScriptClearScreenQueueType)
    end, nil, true, function()

        MainHudCtrl.s_waitingToClearScreen = false
    end)
end

MainHudCtrl._PerformClearScreen = HL.StaticMethod() << function()
    MainHudCtrl.s_clearScreenId = UIManager:ClearScreen()
    Notify(MessageConst.ON_CLEAR_SCREEN_STATE_CHANGED, true)
    MainHudCtrl.s_waitingToClearScreen = false
end

MainHudCtrl._OnClearScreenOff = HL.StaticMethod() << function()
    MainHudCtrl.s_waitingToClearScreen = false
    UIManager:RecoverScreen(MainHudCtrl.s_clearScreenId)
    MainHudCtrl.s_clearScreenId = 0
    Notify(MessageConst.ON_CLEAR_SCREEN_STATE_CHANGED, false)
end

MainHudCtrl._OnClearScreenOnExceptSomePanel = HL.StaticMethod(HL.Table) << function(arg)

    if MainHudCtrl.s_clearScreenIdExceptSomePanel and MainHudCtrl.s_clearScreenIdExceptSomePanel > 0 then
        print("MainHudCtrl._OnClearScreenOnExceptSomePanel: try clear screen while screen is being cleared.")
        return
    end

    if MainHudCtrl.s_waitingToClearScreenExceptPanels then
        print("MainHudCtrl._OnClearScreenOnExceptSomePanel: try clear screen while waiting for exceptPanel version of clear screen.")
        return
    end

    MainHudCtrl.s_waitingToClearScreenExceptPanels = {}
    local panels = unpack(arg)


    for _, panelId in pairs(panels) do
        table.insert(MainHudCtrl.s_waitingToClearScreenExceptPanels, PanelId[panelId])
    end

    if not LuaSystemManager.mainHudActionQueue:IsInLoginCheck() then

        MainHudCtrl._PerformClearScreenExceptSomePanel()
        return
    end


    LuaSystemManager.mainHudActionQueue:AddRequest(Const.LevelScriptClearScreenQueueType, function(_)

        if MainHudCtrl.s_waitingToClearScreenExceptPanels then

            MainHudCtrl._PerformClearScreenExceptSomePanel()
        end

        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Const.LevelScriptClearScreenQueueType)
    end, nil, true, function(_)

        MainHudCtrl.s_waitingToClearScreenExceptPanels = nil
    end)
end

MainHudCtrl._PerformClearScreenExceptSomePanel = HL.StaticMethod() << function()
    MainHudCtrl.s_clearScreenIdExceptSomePanel = UIManager:ClearScreen(MainHudCtrl.s_waitingToClearScreenExceptPanels)
    Notify(MessageConst.ON_CLEAR_SCREEN_STATE_CHANGED, true)
    MainHudCtrl.s_waitingToClearScreenExceptPanels = nil
end

MainHudCtrl._OnClearScreenOffExceptSomePanel = HL.StaticMethod() << function()
    MainHudCtrl.s_waitingToClearScreenExceptPanels = nil
    UIManager:RecoverScreen(MainHudCtrl.s_clearScreenIdExceptSomePanel)
    MainHudCtrl.s_clearScreenIdExceptSomePanel = 0
    Notify(MessageConst.ON_CLEAR_SCREEN_STATE_CHANGED, false)
end

MainHudCtrl._OnOpenLevelPhaseSeamlessLoading = HL.Method() << function()
    if MainHudCtrl.s_clearScreenId ~= 0 then
        Notify(MessageConst.CLEAR_SCREEN_OFF);
    end
end




MainHudCtrl.OnSquadInfightChanged = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    local inFight = unpack(args)
    self:_UpdateSwitchModeState()
    self:_UpdateInventoryState(false)
    self.m_forceBtnInside = inFight
    self:TryUpdateAllTopBtnsVisible()
end

MainHudCtrl.OnSetInSafeZone = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    self:_UpdateInventoryState(false)
end

MainHudCtrl.OnInFacMainRegionChange = HL.Method(HL.Boolean) << function(self, inFacMain)
    self:TryUpdateAllTopBtnsVisible()
    self:_UpdateInventoryState(false)
end

MainHudCtrl.OnFacPlayerPosInfoChanged = HL.Method() << function(self)
    self:_UpdateSwitchModeState()
end

MainHudCtrl.m_inFacModeTagHandle = HL.Field(HL.Userdata)

MainHudCtrl.OnFacModeChange = HL.Method(HL.Boolean) << function(self, inFacMode)
    self:TryUpdateAllTopBtnsVisible()
    self:_UpdateSwitchModeState()
    if inFacMode then
        if not self.m_inFacModeTagHandle then
            self.m_inFacModeTagHandle = GameInstance.player.globalTagsSystem:AddGlobalTag(
                CS.Beyond.Gameplay.Core.GameplayTag(CS.Beyond.GlobalTagConsts.TAG_FAC_MODE))
        end
        self.view.topLeftBtns.animationWrapper:PlayInAnimation()
        self.m_indicatorControllerDisableKeys["inFacMode"] = true
    else
        if self.m_inFacModeTagHandle then
            self.m_inFacModeTagHandle:RemoveTag()
            self.m_inFacModeTagHandle = nil
        end
        self.view.topLeftBtns.animationWrapper:PlayOutAnimation()
        self.m_indicatorControllerDisableKeys["inFacMode"] = nil
    end
    self:_CheckControllerIndicatorEnabled()
end

MainHudCtrl.OnFacTopViewHideUIModeChange = HL.Method(HL.Boolean) << function(self, isTopViewHideUIMode)
    self:TryUpdateAllTopBtnsVisible()
end

MainHudCtrl.OnToggleFacTopView = HL.Method(HL.Boolean) << function(self, active)
    self:TryUpdateAllTopBtnsVisible()
    self:_UpdateSwitchModeState()




    self.m_characterFootBar.mainCharFootBar:SetUIDisable("facTopView", active)
    self:_UpdateWatchBtnBinding()
end

MainHudCtrl.FacToggleTopView = HL.Method(HL.Any) << function(self, arg)
    local active = false
    local fastMode
    if type(arg) == "table" then
        active, fastMode = unpack(arg)
    else
        active = arg
    end
    if active then
        self.m_characterFootBar.mainCharFootBar:SetUIDisable("facTopView", active)
    end
end

MainHudCtrl.ForbidCharFootBar = HL.Method(HL.Any) << function(self, arg)
    local key, isForbid = unpack(arg)
    self.m_characterFootBar.mainCharFootBar:SetUIDisable(key, isForbid)
end

MainHudCtrl.OnDisableBattleIndicatorController = HL.Method(HL.Table) << function(self, arg)
    if not DeviceInfo.usingController then
        return
    end
    local disable, key = unpack(arg)
    if disable then
        self.m_indicatorControllerDisableKeys[key] = true
    else
        self.m_indicatorControllerDisableKeys[key] = nil
    end
    self:_CheckControllerIndicatorEnabled()
end

MainHudCtrl.OnSystemUnlock = HL.Method(HL.Table) << function(self, arg)
    self:TryUpdateAllTopBtnsVisible()

    local system = unpack(arg)
    system = GEnums.UnlockSystemType.__CastFrom(system)
    if system == GEnums.UnlockSystemType.FacMode then
        self:_UpdateSwitchModeState()
    elseif system == GEnums.UnlockSystemType.Dash then
        GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidSprint, "Unlock", false);
    elseif system == GEnums.UnlockSystemType.Jump then
        self:TogglePlayerJump({"system_unlock", false})
    elseif system == GEnums.UnlockSystemType.Activity then

        UIManager:AutoOpen(PanelId.ActivityStartReminder)
    end
end

MainHudCtrl.OnGameModeChange = HL.Method(HL.Table) << function(self, mode)
    self:TryUpdateAllTopBtnsVisible()
    self:_UpdateSwitchModeState()
end

MainHudCtrl.OnDomainDevelopmentUnlock = HL.Method(HL.Opt(HL.Any)) << function(self)
    self:TryUpdateAllTopBtnsVisible()
end

MainHudCtrl.OnSubGameStageChange = HL.Method() << function(self)
    self:TryUpdateAllTopBtnsVisible()
end

MainHudCtrl.BeforeEnterBuildMode = HL.Method(HL.Boolean) << function(self, skipMainHudAnim)

    self:_UpdateSingleTopBtnVisible(self.m_topBtnDataMap.top, not skipMainHudAnim, false)
end

MainHudCtrl.OnBuildModeChange = HL.Method(HL.Number) << function(self, mode)
    local isNormal = mode == FacConst.FAC_BUILD_MODE.Normal
    self:_UpdateSingleTopBtnVisible(self.m_topBtnDataMap.top, true)
    if isNormal or LuaSystemManager.factory.inTopView then
        self.view.disabledFakeAttackButton.gameObject:SetActive(false)
    else
        self.view.disabledFakeAttackButton.gameObject:SetActive(true)
    end
end

MainHudCtrl.BeforeEnterDestroyMode = HL.Method() << function(self)

    self:_UpdateSingleTopBtnVisible(self.m_topBtnDataMap.top, true, false)
end

MainHudCtrl.OnFacDestroyModeChange = HL.Method(HL.Boolean) << function(self, inDestroyMode)
    self:_UpdateSingleTopBtnVisible(self.m_topBtnDataMap.top, true)
    self.view.disabledFakeAttackButton.gameObject:SetActive(inDestroyMode and not LuaSystemManager.factory.inTopView)
end






MainHudCtrl._UpdateSwitchModeState = HL.Method() << function(self)
    local node = self.view.topLeftBtns.switchModeNode
    local inFacMode = Utils.isInFactoryMode()
    node.toggle:SetIsOnWithoutNotify(inFacMode)
    local inFight = Utils.isInFight()
    local isOutOfRangeManual = FactoryUtils.isPlayerOutOfRangeManual()
    node.invalidIcon.gameObject:SetActiveIfNecessary(inFight or isOutOfRangeManual)
    local forbidParams = Utils.getForbiddenReason(ForbidType.DisableSwitchMode)
    node.invalidIconRight.gameObject:SetActiveIfNecessary(forbidParams and forbidParams.forbidStyle == DisableSwitchModeForbidStyle.ShowInvalidIcon)
end

MainHudCtrl._CheckSwitchModeValueValid = HL.Method(HL.Boolean).Return(HL.Boolean, HL.Opt(HL.String)) << function(self, inFacMode)
    if GameInstance.player.systemActionConflictManager.curProcessingSystemAction then
        return
    end
    if Utils.isForbidden(ForbidType.DisableSwitchMode) then
        return false, Language.LUA_SWITCH_MODE_FAIL_WHEN_DISABLED
    end
    if inFacMode then
        if Utils.isInFight() then
            return false, Language.LUA_SWITCH_MODE_FAIL_WHEN_FIGHT
        end
        if FactoryUtils.isPlayerOutOfRangeManual() then
            return false, Language.LUA_SWITCH_MODE_FAIL_WHEN_OUT_OF_RANGE_MANUAL
        end
        if Utils.isForbidden(ForbidType.ForbidFactoryMode) then
            return false, Language.LUA_GAME_MODE_FORBID_FACTORY_BUILD
        end
        if Utils.isInThrowMode() then
            return false, Language.LUA_SWITCH_MODE_FAIL_WHEN_THROW
        end
    end
    return true
end

MainHudCtrl._SwitchMode = HL.Method(HL.Boolean) << function(self, toFactory)
    if toFactory then
        LuaSystemManager.factory:ClearAndSetFactoryMode(true)
    else
        if self.m_enableExitFactoryMode then
            LuaSystemManager.factory:ClearAndSetFactoryMode(false)
        end
    end
end


MainHudCtrl.m_enableExitFactoryMode = HL.Field(HL.Boolean) << true

MainHudCtrl.SetEnableExitFactoryMode = HL.Method(HL.Table) << function(self, args)
    local enable = unpack(args)
    self.m_enableExitFactoryMode = enable
end

MainHudCtrl.TrySwitchMode = HL.Method(HL.Boolean) << function(self, isFacMode)
    if not self.m_topBtnDataMap.switchMode.checkVisible() or
        not self:_CheckSwitchModeValueValid(isFacMode) or
        isFacMode == self.m_topBtnDataMap.switchMode.getCurValue() then
        return
    end
    self:_SwitchMode(isFacMode)
end






MainHudCtrl.m_lastIsInSafeZone = HL.Field(HL.Boolean) << false

MainHudCtrl._UpdateInventoryState = HL.Method(HL.Boolean) << function(self, skipAnim)
    local inSafeZone = Utils.isInSafeZone()
    local wrapper = self.view.topRightBtns.inventoryBtnAnimationWrapper
    if skipAnim then
        if inSafeZone then
            wrapper:SampleToInAnimationEnd()
        else
            wrapper:SampleToOutAnimationEnd()
        end
    else
        if inSafeZone ~= self.m_lastIsInSafeZone then
            if inSafeZone then
                wrapper:PlayInAnimation()
            else
                wrapper:PlayOutAnimation()
            end
        end
    end
    self.m_lastIsInSafeZone = inSafeZone
end







MainHudCtrl.m_onPressJumpOverride = HL.Field(HL.Any)

MainHudCtrl.OverrideJump = HL.Method(HL.Any) << function(self, arg)
    if arg then
        self.m_onPressJumpOverride = arg[1]
    else
        self.m_onPressJumpOverride = nil
    end
end

MainHudCtrl.OnFluidInBuildingRemoved = HL.Method(HL.Any) << function(self, arg)
    local nodeList = unpack(arg)
    for i = 0, nodeList.Count - 1 do
        local node = nodeList[i]
        local buildingData = GameInstance.remoteFactoryManager.staticData:QueryBuildingData(node.templateId)
        if buildingData then
            local toastContent = string.format(Language.LUA_FACTORY_FLUID_IN_NODE_REMOVED, buildingData.name)
            Notify(MessageConst.SHOW_TOAST, toastContent)
        end
    end
end

MainHudCtrl._OnPressJump = HL.Method() << function(self)
    self.view.jumpAnimNode:PlayWithTween("mobile_mainhud_jumpbtn_pressedring")
    if self.m_onPressJumpOverride then
        self.m_onPressJumpOverride()
    else
        GameInstance.playerController:Jump()
    end
end

MainHudCtrl.m_startPressSprintTime = HL.Field(HL.Number) << 0

MainHudCtrl._OnPressSprint = HL.Method() << function(self)
    if BEYOND_DEBUG and UNITY_EDITOR then
        if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
            if CS.Beyond.DebugDefines.disableRightClickSprintEvenNoCursor then
                return
            end
            if CS.Beyond.DebugDefines.disableRightClickSprint and InputManager.cursorVisible then
                return
            end
        end
    end

    if Utils.isForbidden(ForbidType.ForbidMove) or not GameWorld.battle.dashEnable then
        return
    end
    if GameInstance.playerController:OnSprintPressed() then
        self.view.sprintLightSuccess:PlayWithTween("mobile_mainhud_jumpbtn_release_success")
    end
    self.view.sprintAnimNode:PlayWithTween("mobile_mainhud_jumpbtn_pressedring")
    self.m_startPressSprintTime = Time.unscaledTime
end

MainHudCtrl._OnReleaseSprint = HL.Method() << function(self)
    GameInstance.playerController:OnSprintReleased()
end

MainHudCtrl._OnDragSprint = HL.Method(HL.Userdata) << function(self, eventData)
    if Time.unscaledTime - self.m_startPressSprintTime >= self.view.config.SPRINT_BTN_DRAG_DELAY then
        Notify(MessageConst.ON_DRAG_SPRINT_BTN, eventData.delta)
    end
end

MainHudCtrl._OnDragAttack = HL.Method(HL.Userdata) << function(self, eventData)
    Notify(MessageConst.ON_DRAG_SPRINT_BTN, eventData.delta)
end

MainHudCtrl._OnDragJump = HL.Method(HL.Userdata) << function(self, eventData)
    Notify(MessageConst.MOVE_LEVEL_CAMERA, eventData.delta)
end

MainHudCtrl._UpdateSprintInfo = HL.Method(HL.Boolean) << function(self, inputEnabled)
    if inputEnabled then
        if self.view.sprintBtn.groupEnabled then


            if DeviceInfo.usingKeyboard and InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.LeftShift) then
                self:_OnPressSprint()
            end
        end
    else
        self:_OnReleaseSprint()
    end
end

MainHudCtrl._CheckControllerIndicatorEnabled = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    if next(self.m_indicatorControllerDisableKeys) ~= nil then
        InputManagerInst:ToggleGroup(self.m_indicatorControllerGroupId, false)
        self:_ToggleControllerIndicator(false)
    else
        InputManagerInst:ToggleGroup(self.m_indicatorControllerGroupId, true)

        if InputManagerInst:IsGroupEnabled(self.m_indicatorControllerGroupId) and CS.Beyond.UI.BattleControllerInputController.instance:GetControllerIndicatorState() then
            self:_ToggleControllerIndicator(true)
        end
    end
end

MainHudCtrl._ToggleControllerIndicator = HL.Method(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingController then
        return
    end
    Notify(MessageConst.ON_CONTROLLER_INDICATOR_CHANGE, active)
    Notify(MessageConst.TOGGLE_HIDE_INTERACT_OPTION_LIST, { "CONTROLLER_INDICATOR", active })
    self.view.attackButton:ToggleControllerIndicator(active)
    self:CheckNormalAttackBtn(not active)
    if active then
        UIManager:HideWithKey(PanelId.GeneralAbility, "CONTROLLER_INDICATOR")
    else
        UIManager:ShowWithKey(PanelId.GeneralAbility, "CONTROLLER_INDICATOR")
    end
end

MainHudCtrl.m_hideJumpKeys = HL.Field(HL.Table)

MainHudCtrl.OnToggleSprint = HL.Method(HL.Table) << function(self, args)
    local active = unpack(args)
    self.view.sprintTypeNode.gameObject:SetActive(active)
    self.view.sprintNormalNode.gameObject:SetActive(not active)
end

MainHudCtrl.TogglePlayerJump = HL.Method(HL.Table) << function(self, args)
    local reason, forbid = unpack(args)
    if forbid then
        self.m_hideJumpKeys[reason] = true
    else
        self.m_hideJumpKeys[reason] = nil
    end
    if next(self.m_hideJumpKeys) then
        self.view.jumpBtn.gameObject:SetActive(false)
    else
        self.view.jumpBtn.gameObject:SetActive(true)
    end
end

MainHudCtrl.OnDashEnableChanged = HL.Method(HL.Table) << function(self, args)
    if self.view.sprintDisableNode == nil then
        return
    end
    local enable = unpack(args)
    self.view.sprintDisableNode.gameObject:SetActive(not enable)
end

MainHudCtrl.OnApplicationFocus = HL.Method(HL.Table) << function(self, arg)

    if DeviceInfo.usingKeyboard and not InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.LeftShift) then
        GameInstance.playerController:OnSprintReleased()
    end
    local hasFocus = unpack(arg)
    if not hasFocus then
        self:_ToggleControllerIndicator(false)
        self:CheckNormalAttackBtn(false)
    end
end






MainHudCtrl.OnEnterTowerDefenseDefendingPhase = HL.Method() << function(self)
    LuaSystemManager.factory:AddFactoryModeRequest({ false, "TowerDefense" })
    LuaSystemManager.factory:RemoveFactoryModeRequest("TowerDefensePrepare")
    self:TryUpdateAllTopBtnsVisible()
end

MainHudCtrl.OnTowerDefenseDefendingRewardsFinished = HL.Method() << function(self)
    LuaSystemManager.factory:RemoveFactoryModeRequest("TowerDefense")
    self:_UpdateSwitchModeState()
end






MainHudCtrl.m_canAutoStopExpand = HL.Field(HL.Boolean) << true

MainHudCtrl.m_forceBtnOutside = HL.Field(HL.Boolean) << false

MainHudCtrl.m_forceBtnInside = HL.Field(HL.Boolean) << false

MainHudCtrl._InitOneTopNodeExpand = HL.Method(HL.Table) << function(self, node)
    node.expandBtn.onClick:AddListener(function(eventData)
        local expandAll = DeviceInfo.usingKeyboard
        if expandAll then
            self:_OnClickExpand(self.view.topLeftBtns)
            self:_OnClickExpand(self.view.topRightBtns)
        else
            self:_OnClickExpand(node)
        end
    end)

    node.expandNode:InitMainHudExpandNode(node)
    node.expandNode.view.closeExpandBtn.onClick:AddListener(function(eventData)
        local expandAll = DeviceInfo.usingKeyboard
        if expandAll then
            self.view.topLeftBtns.expandNode:SetExpanded(false)
            self.view.topRightBtns.expandNode:SetExpanded(false)
        else
            node.expandNode:SetExpanded(false)
        end
    end)

    node.expandRedDot:InitRedDot("")
end

MainHudCtrl._OnClickExpand = HL.Method(HL.Table) << function(self, node)
    node.expandNode:SetExpanded(true)
    if self.m_canAutoStopExpand then
        node.expandNode:StartAutoCloseTimer()
    end
end

MainHudCtrl.OnSetMainHudCanAutoStopExpand = HL.Method(HL.Any) << function(self, args)
    self.m_canAutoStopExpand = unpack(args)
end

MainHudCtrl._ResetTopNodePosInfo = HL.Method(HL.Table) << function(self, node)
    node.m_flexibleOutsideBtnInfo = nil
    node.m_curInsideBtnCount = 0
    node.expandRedDot:ApplyState(false)
end

MainHudCtrl._UpdateTopNodeExpandState = HL.Method(HL.Table) << function(self, node)
    if node.m_curInsideBtnCount and node.m_curInsideBtnCount > 0 then
        node.expandBtn.gameObject:SetActive(not node.expandNode.m_isExpanded)
        for k = 1, 4 do
            local img = node.expandDotNode["dot" .. k]
            if node.m_curInsideBtnCount >= k then
                UIUtils.changeAlpha(img, 1)
            else
                UIUtils.changeAlpha(img, 0.3)
            end
        end
    else
        node.expandNode:SetExpanded(false, true)
    end
    node.expandNode.view.closeExpandBtn.transform:SetAsLastSibling()
    node.expandBtn.transform:SetAsLastSibling()
end

MainHudCtrl._OnAfterApplyRedDotSate = HL.Method(HL.Table, HL.Boolean) << function(self, info, active)
    if self.m_updateTimerId > 0 then

        return
    end

    if DeviceInfo.usingController then
        self:_OnOneTopBtnRedDotChangedInController(info, active)
        return
    end

    if self.m_forceBtnInside or self.m_forceBtnOutside then
        return
    end
    local canBeFlexibleOutside = info.posType == TopBtnPosType.AlwaysInside or (info.posType == TopBtnPosType.TopViewInside and LuaSystemManager.factory.inTopView)
    if not canBeFlexibleOutside then
        return
    end
    local shouldUpdate
    local belongNode = info.belongNode
    if info.isOutside then
        if not active then

            shouldUpdate = true
        end
    else
        if active then
            if not belongNode.m_flexibleOutsideBtnInfo or info.sortId < belongNode.m_flexibleOutsideBtnInfo.sortId then

                shouldUpdate = true
            end
        end
    end
    if shouldUpdate then
        self:_ResetTopNodePosInfo(belongNode)

        for _, otherInfo in ipairs(self.m_topBtnDataList) do
            if otherInfo.belongNode == belongNode and otherInfo.viewNode.gameObject.activeSelf then
                self:_UpdateBtnPos(otherInfo)
            end
        end
        self:_UpdateTopNodeExpandState(belongNode)
    end
end






local MAIL_BUBBLE_STATE_NONE = 0
local MAIL_BUBBLE_STATE_LOST_AND_FOUND = 1
local MAIL_BUBBLE_STATE_QUESTIONNAIRE = 2

MainHudCtrl.m_mailBubbleCacheState = HL.Field(HL.Number) << 0

MainHudCtrl.m_mailBubbleShowingState = HL.Field(HL.Number) << 0

MainHudCtrl.m_mainBubbleCor = HL.Field(HL.Thread)

MainHudCtrl.OnGetNewMails = HL.Method() << function(self)

    self:_UpdateSingleTopBtnVisible(self.m_topBtnDataMap.mail)
    self:_CheckShowMailBtnBubble()
end

MainHudCtrl.OnLostAndFoundRefresh = HL.Method(HL.Opt(HL.Table)) << function(self, args)

    self:_UpdateSingleTopBtnVisible(self.m_topBtnDataMap.mail)
    self:_CheckShowMailBtnBubble()
end

MainHudCtrl._CheckShowMailBtnBubble = HL.Method() << function(self)
    self.m_mailBubbleCacheState = MAIL_BUBBLE_STATE_NONE
    if GameInstance.player.mail.needShowNewQuestionnaire then
        self.m_mailBubbleCacheState = MAIL_BUBBLE_STATE_QUESTIONNAIRE
    elseif GameInstance.player.inventory.lostAndFoundHasNew and not GameInstance.player.inventory.lostAndFound:IsEmpty() then
        self.m_mailBubbleCacheState = MAIL_BUBBLE_STATE_LOST_AND_FOUND
    end
    if self:IsShow() then
        self:_ShowMainBtnBubble()
    end
end

MainHudCtrl._ShowMainBtnBubble = HL.Method() << function(self)
    if self.m_mailBubbleShowingState >= self.m_mailBubbleCacheState then
        return
    end
    if self.m_mainBubbleCor then
        self.m_mainBubbleCor = self:_ClearCoroutine(self.m_mainBubbleCor)
    end
    self.m_mainBubbleCor = self:_StartCoroutine(function()
        self.view.topLeftBtns.mailBubbleImg.gameObject:SetActive(true)
        self.m_mailBubbleShowingState = self.m_mailBubbleCacheState
        self.m_mailBubbleCacheState = MAIL_BUBBLE_STATE_NONE
        if self.m_mailBubbleShowingState == MAIL_BUBBLE_STATE_QUESTIONNAIRE then
            GameInstance.player.mail.needShowNewQuestionnaire = false
            GameInstance.player.inventory.lostAndFoundHasNew = false
            self.view.topLeftBtns.mailBubbleTxt.text = Language.LUA_MAIL_HUD_BUBBLE_QUESTIONNAIRE
        elseif self.m_mailBubbleShowingState == MAIL_BUBBLE_STATE_LOST_AND_FOUND then
            GameInstance.player.inventory.lostAndFoundHasNew = false
            self.view.topLeftBtns.mailBubbleTxt.text = Language.LUA_MAIL_HUD_BUBBLE_LOST_AND_FOUND
        end
        coroutine.wait(self.view.config.MAIL_BUBBLE_STAY_TIME)
        self.view.topLeftBtns.mailBubbleImg.gameObject:SetActive(false)
        self.m_mailBubbleShowingState = MAIL_BUBBLE_STATE_NONE
    end)
end






MainHudCtrl.OnThrowModeChange = HL.Method(HL.Any) << function(self, args)
    local inThrowMode = GameWorld.battle.inThrowMode

    self:TogglePlayerJump({"throw_mode", inThrowMode})
    if inThrowMode then
        self.m_indicatorControllerDisableKeys["inThrowMode"] = true
        self:_CheckControllerIndicatorEnabled()

        self.view.attackButton:ReleaseNormalAttackBtn()
    else
        self.m_indicatorControllerDisableKeys["inThrowMode"] = nil
        self:_CheckControllerIndicatorEnabled()
    end
end





MainHudCtrl.OnShowWaterDroneAim = HL.Method() << function(self)
    self.m_indicatorControllerDisableKeys["inWaterDroneAim"] = true
    self:_CheckControllerIndicatorEnabled()

    self.view.attackButton:ReleaseNormalAttackBtn()
end

MainHudCtrl.OnHideWaterDroneAim = HL.Method() << function(self)
    self.m_indicatorControllerDisableKeys["inWaterDroneAim"] = nil
    self:_CheckControllerIndicatorEnabled()
end




MainHudCtrl.m_curControllerVisibleTopBtns = HL.Field(HL.Table)

MainHudCtrl._UpdateAllTopBtnsVisibleInController = HL.Method() << function(self)
    local visibleBtnInfos = {}
    for _, info in ipairs(self.m_topBtnDataList) do
        local visible = self:_GetSingleTopBtnVisible(info)
        if visible then
            local controllerPosType, controllerPosOrder
            if info.getControllerPosInfo then
                controllerPosType, controllerPosOrder = info.getControllerPosInfo()
            else
                controllerPosType, controllerPosOrder = info.controllerPosType, info.controllerPosOrder
            end
            if controllerPosType then
                if controllerPosType == ControllerTopBtnPosTypes.Fixed then
                    info.curControllerPosSortId = controllerPosOrder
                else
                    if info.redDotView and info.redDotView.curIsActive then
                        info.curControllerPosSortId = ControllerDynamicTopBtnBaseOrderWhitRedDot + controllerPosOrder
                    else
                        info.curControllerPosSortId = ControllerDynamicTopBtnBaseOrder + controllerPosOrder
                    end
                end
                table.insert(visibleBtnInfos, info)
            else
                info.viewNode.gameObject:SetActive(true)
            end

            if info.customUpdateBtnInfo then
                info.customUpdateBtnInfo()
            end
        else
            info.viewNode.gameObject:SetActive(false)
        end
    end
    table.sort(visibleBtnInfos, Utils.genSortFunction({ "curControllerPosSortId" }, true))
    self.m_curControllerVisibleTopBtns = visibleBtnInfos
    self:_ChooseShowingTopBtnsInController()
    self:_RefreshQuickMenuWithTopBtnsVisibleStateInController()
end

MainHudCtrl._ChooseShowingTopBtnsInController = HL.Method() << function(self)
    local maxShowCount = self.view.config.CONTROLLER_TOP_BTN_MAX_SHOW_COUNT
    local listNode = self.view.topRightBtns.controllerBtnList
    if #self.m_curControllerVisibleTopBtns == 0 then
        listNode.gameObject:SetActive(false)
        return
    end

    for k, info in ipairs(self.m_curControllerVisibleTopBtns) do
        if k <= maxShowCount then
            if info.viewNode.transform.parent ~= listNode.transform then
                info.viewNode.transform:SetParent(listNode.transform)
                info.viewNode.transform.localScale = Vector3.one
            end
            info.viewNode.transform:SetAsLastSibling()
            info.viewNode.gameObject:SetActive(true)
        else
            info.viewNode.gameObject:SetActive(false)
        end
    end
    if #self.m_curControllerVisibleTopBtns > maxShowCount then
        listNode.moreDeco.gameObject:SetActive(true)
        listNode.moreDeco.transform:SetAsLastSibling()
    else
        listNode.moreDeco.gameObject:SetActive(false)
    end
    listNode.gameObject:SetActive(true)
end

MainHudCtrl._OnOneTopBtnRedDotChangedInController = HL.Method(HL.Table, HL.Boolean) << function(self, changedInfo, active)
    local controllerPosType, controllerPosOrder
    if changedInfo.getControllerPosInfo then
        controllerPosType, controllerPosOrder = changedInfo.getControllerPosInfo()
    else
        controllerPosType, controllerPosOrder = changedInfo.controllerPosType, changedInfo.controllerPosOrder
    end
    if controllerPosType == ControllerTopBtnPosTypes.Fixed then
        return
    end
    local newSortId
    if active then
        newSortId = ControllerDynamicTopBtnBaseOrderWhitRedDot + controllerPosOrder
    else
        newSortId = ControllerDynamicTopBtnBaseOrder + controllerPosOrder
    end
    changedInfo.curControllerPosSortId = newSortId
    local maxShowCount = self.view.config.CONTROLLER_TOP_BTN_MAX_SHOW_COUNT
    local count = #self.m_curControllerVisibleTopBtns
    local needRefresh
    if count > maxShowCount then
        local lastShowingBtnInfo = self.m_curControllerVisibleTopBtns[maxShowCount]
        needRefresh = newSortId > lastShowingBtnInfo.curControllerPosSortId
    else
        needRefresh = true
    end
    if needRefresh then
        table.sort(self.m_curControllerVisibleTopBtns, Utils.genSortFunction({ "curControllerPosSortId" }, true))
        self:_ChooseShowingTopBtnsInController()
    end
end

MainHudCtrl._RefreshQuickMenuWithTopBtnsVisibleStateInController = HL.Method() << function(self)
    InputManagerInst:ToggleBinding(self.m_quickMenuBindingId, #self.m_curControllerVisibleTopBtns > 0)
end








MainHudCtrl._UpdateWatchBtnBinding = HL.Method() << function(self)
    if DeviceInfo.usingKeyboard and LuaSystemManager.factory.inTopView and GameSettingUtils.GetSettingValueBool(GameSettingConst.SETTING_ID_FAC_TOP_VIEW_ESC_EXIT) then
        self.view.topRightBtns.watchBtn.onClick:StopUseBinding()
    else
        self.view.topRightBtns.watchBtn.onClick:ChangeBindingPlayerAction("common_open_watch")
    end
end


HL.Commit(MainHudCtrl)
