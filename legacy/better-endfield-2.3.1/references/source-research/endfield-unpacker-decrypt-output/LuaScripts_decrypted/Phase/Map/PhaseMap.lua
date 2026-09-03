local phaseBase = require_ex('Phase/Core/PhaseBase')
local MapSpaceshipNode = require_ex('UI/Widgets/MapSpaceshipNode')
local PHASE_ID = PhaseId.Map
local MarkType = GEnums.MarkType
PhaseMap = HL.Class('PhaseMap', phaseBase.PhaseBase)

local MAP_PANEL_ID = PanelId.Map
local MAP_MASK_PANEL_ID = PanelId.MapTransitionMask






local DETAIL_PANEL_MAP = {
    [MarkType.TrackingMission] = PanelId.MapMarkDetailMission,
    [MarkType.CampFire] = PanelId.MapMarkDetailCampFire,
    [MarkType.BlackBox] = PanelId.MapMarkDetailBlackBox,
    [MarkType.DoodadGroup] = PanelId.MapMarkDetailDoodadGroup,
    [MarkType.EnemySpawner] = PanelId.MapMarkDetailEnemySpawner,
    [MarkType.MinePointTeam] = PanelId.MapMarkDetailMinePointTeam,
    [MarkType.Recycler] = PanelId.MapMarkDetailRecycleBin,
    [MarkType.AvailableMission] = PanelId.MapMarkDetailAvailableMission,
    [MarkType.SSControlCenter] = PanelId.MapMarkDetailSSControlCenter,
    [MarkType.NpcRacingDungeon] = PanelId.MapMarkDetailRacingDungeon,
    [MarkType.HUB] = PanelId.MapMarkDetailHub,
    [MarkType.Settlement] = PanelId.MapMarkDetailSettlement,
    [MarkType.General] = PanelId.MapMarkDetailDefault,
    [MarkType.FixableRobot] = PanelId.MapMarkDetailDefault,
    [MarkType.NpcSSTrainingRoom] = PanelId.MapMarkDetailDefault,
    [MarkType.CustomMark] = PanelId.MapCustomMarkDetail,
    [MarkType.CustomMarkSelect] = PanelId.MapCustomMarkDetail,
    [MarkType.EquipFormulaChest] = PanelId.MapMarkDetailEquipFormulaChest,
    [MarkType.UdPipeLoader] = PanelId.MapMarkDetailUndergroundPipe,
    [MarkType.UdPipeUnloader] = PanelId.MapMarkDetailUndergroundPipe,
    [MarkType.DomainShop] = PanelId.MapMarkDetailDomainShop,
    [MarkType.DomainDepot] = PanelId.MapMarkDetailDomainDepot,
    [MarkType.KiteStation] = PanelId.MapMarkDetailKiteStation,
    [MarkType.SettlementDefenseTerminal] = PanelId.MapMarkDetailSettlementDefenseTerminal,
    [MarkType.NpcCommonShop] = PanelId.MapMarkDetailDefault,
    [MarkType.SocialBuilding] = PanelId.MapMarkDetailSocialBuilding,
    [MarkType.WeekRaid] = PanelId.MapMarkDetailWeekRaid,
    [MarkType.SimulationTraining] = PanelId.MapMarkDetailSimulationTraining,
    [MarkType.SSReceptionTeleport] = PanelId.MapMarkDetailCampFire,
    [MarkType.SnapshotActivity] = PanelId.MapMarkDetailActivitySnapShot,
    [MarkType.SnapshotActivityNew] = PanelId.MapMarkDetailActivitySnapShot,
    [MarkType.ActivityCleaning] = PanelId.MapMarkDetailActivityCleaning,
    [MarkType.ActivityRacingDungeon] = PanelId.MapMarkDetailCoin,
    [MarkType.DungeonActmonsterActivity] = PanelId.MapMarkDetailActivityActmonsterCtrl,
    [MarkType.DungeonDoubleBattleActivity] = PanelId.MapMarkDetailActivityActmonsterCtrl,
    [MarkType.ActivityContingencyContract] = PanelId.MapMarkDetailContingencyContract,
    [MarkType.GasMinePointTeam] = PanelId.MapMarkDetailGasMinePointTeam,
    [MarkType.SeasonTower] = PanelId.MapMarkDetailSeasonTower,


    [MarkType.DungeonPuzzle] = PanelId.MapMarkDetailDungeon,
    [MarkType.DungeonResource] = PanelId.MapMarkDetailDungeon,
    [MarkType.DungeonWorldLevel] = PanelId.MapMarkDetailDungeon,
    [MarkType.BossRush] = PanelId.MapMarkDetailBossRush,
    [MarkType.DungeonSS] = PanelId.MapMarkDetailDungeonSS,


    [MarkType.SewageTreatPlant] = PanelId.MapMarkDetailSewageTreatPlant,
    [MarkType.InvalidBuilding] = PanelId.MapMarkDetailInvalidBuilding,
    [MarkType.CommonBuilding] = PanelId.MapMarkDetailCommonBuilding,
}

local FILTER_PANEL_ID = PanelId.MapMarkFilter

PhaseMap.m_needGlitch = HL.Field(HL.Boolean) << false

PhaseMap.m_needMask = HL.Field(HL.Boolean) << false

PhaseMap.m_mapPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseMap.m_mapPanelShown = HL.Field(HL.Boolean) << false

PhaseMap.m_detailPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseMap.m_detailPanelShownId = HL.Field(HL.String) << ""
PhaseMap.m_multiDeletePanelShow = HL.Field(HL.Boolean) << false

PhaseMap.m_multiDeletePanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseMap.m_waitDetailPanelHide = HL.Field(HL.Boolean) << false

PhaseMap.m_isDetailPanelDoingOut = HL.Field(HL.Boolean) << false

PhaseMap.m_detailPanelCloseCallback = HL.Field(HL.Function)

PhaseMap.m_filterPanel = HL.Field(HL.Forward("PhasePanelItem"))





PhaseMap.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_LOADING_PANEL_CLOSED] = { 'OnLoadingPanelClosed', false },
    [MessageConst.SHOW_LEVEL_MAP_MARK_DETAIL] = { '_OnShowMarkDetail', true },
    [MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL] = { '_OnHideMarkDetail', true },
    [MessageConst.SHOW_LEVEL_MAP_FILTER] = { '_OnShowFilter', true },
    [MessageConst.HIDE_LEVEL_MAP_FILTER] = { '_OnHideFilter', true },
    [MessageConst.SHOW_CUSTOM_MARK_MULTI_DELETE] = { '_OnShowMarkMultiDelete', true },
    [MessageConst.HIDE_CUSTOM_MARK_MULTI_DELETE] = { '_OnHideMarkMultiDelete', true },
    [MessageConst.ON_CLICK_MAP_CLOSE_BTN] = { '_OnClickCloseMapBtn', true },
}


PhaseMap._OnInit = HL.Override() << function(self)
    PhaseMap.Super._OnInit(self)
end


PhaseMap._InitAllPhaseItems = HL.Override() << function(self)
    PhaseMap.Super._InitAllPhaseItems(self)
end

PhaseMap._OnClickCloseMapBtn = HL.Method() << function(self)
    if self.m_isDetailPanelDoingOut then
        return
    end
    MapUtils.closeMapRelatedPhase()
end




PhaseMap.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    self.m_needGlitch = false
    self.m_needMask = false
    if transitionType == PhaseConst.EPhaseState.TransitionIn then
        UIManager:PreloadPanelAsset(MAP_PANEL_ID, PHASE_ID)
        if not fastMode then
            if anotherPhaseId == PhaseId.Watch then
                if self.arg ~= nil and MapUtils.isSpaceshipRelatedLevel(self.arg.levelId) then
                    self.m_needGlitch = true
                    Notify(MessageConst.PREPARE_BLOCK_GLITCH_TRANSITION)
                    coroutine.waitForRenderDone()
                end
            else
                if DeviceInfo.usingTouch then
                    self.m_needMask = true
                    UIManager:Open(MAP_MASK_PANEL_ID)
                end
            end
        end
    end
end

PhaseMap._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_mapPanelShown = true
    self.m_mapPanel = self:CreatePhasePanelItem(MAP_PANEL_ID, self.arg)

    if self.m_needGlitch then
        Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
    end

    if self.m_needMask then
        local _, ctrl = UIManager:IsOpen(MAP_MASK_PANEL_ID)
        ctrl:PlayAnimationOutAndClose()
    end

    if self.arg ~= nil and self.arg.infoPopupArg ~= nil then
        UIManager:Open(PanelId.MapInfoPopup, self.arg.infoPopupArg)
        self.arg.infoPopupArg = nil
    end

    if self.arg ~= nil and self.arg.initRemindTipTabIndex ~= nil then
        self.m_mapPanel.uiCtrl:RecoverRemindTip(self.arg.initRemindTipTabIndex)
        self.arg.initRemindTipTabIndex = nil
    end

    if self.arg ~= nil and self.arg.isFilterPanelOpen then
        self.m_mapPanel.uiCtrl:OpenFilterPanel()
        self.arg.isFilterPanelOpen = nil
    end

    if self.arg ~= nil and self.arg.invalidBuildingArg then
        UIManager:Open(PanelId.MapWaitDeleteBuildingList, self.arg.invalidBuildingArg)
        self.arg.invalidBuildingArg = nil
    end
end

PhaseMap._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
end

PhaseMap._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseMap._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseMap._OnActivated = HL.Override() << function(self)
end

PhaseMap._OnDeActivated = HL.Override() << function(self)
end

PhaseMap._OnDestroy = HL.Override() << function(self)
end

PhaseMap._OnRefresh = HL.Override() << function(self)
    if self.m_mapPanel == nil then
        return
    end
    self.m_mapPanel.uiCtrl:ResetMapStateToTargetLevel(self.arg)
end

PhaseMap.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    local currState = self.m_mapPanel.uiCtrl:GetCurrState()
    local currRemindState = self.m_mapPanel.uiCtrl:GetRemindTipState()
    arg.levelId = currState.currLevelId
    local selectedInstId = currState.selectedMarkInstId
    if not string.isEmpty(selectedInstId) and MapUtils.isTemporaryCustomMark(selectedInstId) then
        arg.instId = nil
    else
        arg.instId = selectedInstId
    end



    if string.isEmpty(arg.instId) then
        arg.templateId = nil
        arg.resourceTemplateId = nil
        arg.resourceItemId = nil
    end

    local isInfoPopupOpen = UIManager:IsOpen(PanelId.MapInfoPopup)
    if isInfoPopupOpen then
        arg.infoPopupArg = self.m_mapPanel.uiCtrl:GetPopupInfoState()
    else
        arg.infoPopupArg = nil
    end

    if currRemindState.isShowing then
        arg.initRemindTipTabIndex = currRemindState.tabIndex
    else
        arg.initRemindTipTabIndex = nil
    end

    local isInvalidBuildingOpen = UIManager:IsOpen(PanelId.MapWaitDeleteBuildingList)
    if isInvalidBuildingOpen then
        local chapterId = ScopeUtil.GetChapterId(self.m_mapPanel.uiCtrl.m_currLevelId)
        arg.invalidBuildingArg = chapterId
    else
        arg.invalidBuildingArg = nil
    end

    arg.isRecover = true

    arg.bigRectState = self.m_mapPanel.uiCtrl:GetBigRectRecoverStateArg()

    arg.tierId = self.m_mapPanel.uiCtrl:GetTierRecoverId()

    arg.isFilterPanelOpen = self.m_filterPanel ~= nil and self.m_filterPanel.uiCtrl ~= nil

    local detailPanelCtrl = self.m_detailPanel and self.m_detailPanel.uiCtrl
    if not string.isEmpty(arg.instId) and detailPanelCtrl and
            HL.TryGet(detailPanelCtrl, "GetRecoverPopupStateArg") and
            PhaseManager:GetTopPhaseId() == PHASE_ID then
        arg.detailPopupState = detailPanelCtrl:GetRecoverPopupStateArg()
    else
        arg.detailPopupState = nil
    end

    return arg
end

PhaseMap._TryRecoverDetailPopupState = HL.Method() << function(self)
    if self.arg == nil or self.arg.detailPopupState == nil then
        return
    end
    local popupState = self.arg.detailPopupState
    self.arg.detailPopupState = nil
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    if string.isEmpty(self.arg.instId) then
        return
    end
    local detailPanelCtrl = self.m_detailPanel and self.m_detailPanel.uiCtrl
    if detailPanelCtrl == nil or not HL.TryGet(detailPanelCtrl, "TryRecoverPopupState") then
        return
    end
    detailPanelCtrl:TryRecoverPopupState(popupState)
end






PhaseMap.OnLoadingPanelClosed = HL.StaticMethod() << function()
    MapSpaceshipNode.ClearStaticFromData()
end






PhaseMap._OnShowMarkDetail = HL.Method(HL.Table) << function(self, args)
    if not self.m_mapPanelShown then
        return
    end

    local markInstId = args.markInstId
    local markSuccess, markData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if not markSuccess then
        return
    end

    local templateSuccess, templateData = Tables.mapMarkTempTable:TryGetValue(markData.templateId)
    if not templateSuccess then
        return
    end

    if self.m_multiDeletePanelShow then
        return
    end
    self.m_waitDetailPanelHide = false
    if not string.isEmpty(self.m_detailPanelShownId) then
        if self.m_detailPanelShownId == markInstId and MapUtils.isTemporaryCustomMark(self.m_detailPanelShownId) then
            return
        end
        self:RemovePhasePanelItem(self.m_detailPanel)
    end

    local panelId = DETAIL_PANEL_MAP[templateData.markType] or PanelId.MapMarkDetailDefault
    self.m_detailPanelCloseCallback = args.onClosedCallback

    local panelArgs = { markInstId = markInstId }
    self.m_detailPanel = self:CreatePhasePanelItem(panelId, panelArgs)
    self.m_detailPanel.uiCtrl:ChangePanelCfg("blockKeyboardEvent", DeviceInfo.usingController)
    self.m_detailPanelShownId = markInstId
    self:_TryRecoverDetailPopupState()
end

PhaseMap._OnHideMarkDetail = HL.Method(HL.Opt(HL.Boolean)) << function(self, closeDirectly)
    if closeDirectly then
        self:_HideMarkDetail(true)
        return
    end




    self.m_waitDetailPanelHide = true
    self:_StartCoroutine(function()
        coroutine.step()
        if self.m_waitDetailPanelHide then
            self:_HideMarkDetail()
        end
    end)
end

PhaseMap._HideMarkDetail = HL.Method(HL.Opt(HL.Boolean)) << function(self, closeDirectly)
    if self.m_detailPanelCloseCallback ~= nil then
        self.m_detailPanelCloseCallback()
    end

    if not self.m_detailPanel.uiCtrl then
        self.m_isDetailPanelDoingOut = false
        return
    end

    local closeFunc = function()
        self.m_isDetailPanelDoingOut = false
        self.m_detailPanelShownId = ""
        self:RemovePhasePanelItem(self.m_detailPanel)
    end

    if closeDirectly then
        closeFunc()
    else
        if not self.m_isDetailPanelDoingOut then
            self.m_isDetailPanelDoingOut = true
            self.m_detailPanel.uiCtrl:PlayAnimationOutWithCallback(function()
                closeFunc()
            end)
        end
    end
end





PhaseMap._OnShowMarkMultiDelete = HL.Method(HL.Table) << function(self, args)
    local trackingMark
    if args.instId == GameInstance.player.mapManager.trackingMarkInstId then
        trackingMark = self.m_mapPanel.uiCtrl.view.levelMapController.view.levelMapLoader:GetGeneralTrackingMark()
    end
    local args =
    {
        levelId = args.levelId,
        mark = self.m_mapPanel.uiCtrl.view.levelMapController.view.levelMapLoader:GetLoadedMarkByInstId(args.instId),
        trackMark = trackingMark
    }
    self.m_multiDeletePanel = self:CreatePhasePanelItem(PanelId.MapCustomMarkDelete, args)
    self.m_multiDeletePanelShow = true
    Notify(MessageConst.TOGGLE_CUSTOM_MARK_MULTI_DELETE_STATE, { isShow = true })
end

PhaseMap._OnHideMarkMultiDelete = HL.Method() << function(self)
    self.m_multiDeletePanel.uiCtrl:PlayAnimationOutWithCallback(function()
        Notify(MessageConst.TOGGLE_CUSTOM_MARK_MULTI_DELETE_STATE, { isShow = false })
        self.m_multiDeletePanelShow = false
        self:RemovePhasePanelItem(self.m_multiDeletePanel)
    end)
end






PhaseMap._OnShowFilter = HL.Method(HL.Any) << function(self, arg)
    self.m_filterPanel = self:CreatePhasePanelItem(FILTER_PANEL_ID, arg)
end

PhaseMap._OnHideFilter = HL.Method() << function(self)
    self.m_filterPanel.uiCtrl:PlayAnimationOutWithCallback(function()
        self:RemovePhasePanelItem(self.m_filterPanel)
    end)
end




HL.Commit(PhaseMap)
