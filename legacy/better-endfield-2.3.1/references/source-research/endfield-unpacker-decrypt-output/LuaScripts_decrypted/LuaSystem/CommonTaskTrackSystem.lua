local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')


local PowerPoleFastTravelAllowedRequestTypes = {
    TrackStartCountdown = true,
    TrackEndToast = true,
    DungeonSettlement = true,
}

CommonTaskTrackSystem = HL.Class('CommonTaskTrackSystem', LuaSystemBase.LuaSystemBase)


CommonTaskTrackSystem.m_pendingRequests = HL.Field(HL.Table)

CommonTaskTrackSystem.configs = HL.Field(HL.Table)

CommonTaskTrackSystem.m_nextRequestId = HL.Field(HL.Number) << 1

CommonTaskTrackSystem.m_isShowing = HL.Field(HL.Boolean) << false

CommonTaskTrackSystem.m_tryStartPanelTimerId = HL.Field(HL.Number) << -1


CommonTaskTrackSystem.CommonTaskTrackSystem = HL.Constructor() << function(self)
    self:_InitConfigs()
    self.m_pendingRequests = {}

    self:RegisterMessage(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, function(type)
        self:OnOneCommonTaskPanelFinish(type)
    end)

    self:RegisterMessage(MessageConst.ON_PHASE_LEVEL_ON_TOP, function()
        self:_TryAddStartPanelTimer()
    end)
    self:RegisterMessage(MessageConst.ON_PHASE_LEVEL_NOT_ON_TOP, function()
        self:Interrupt()
    end)


    self:RegisterMessage(MessageConst.ALL_MAIN_HUD_ACTION_FINISH, function()
        self:_TryAddStartPanelTimer()
    end)

    self:RegisterMessage(MessageConst.ON_DO_CLOSE_MAP, function()
        self:_ForceClearRequest()
    end)


    self:RegisterMessage(MessageConst.ON_ENTER_DUNGEON, function()
        self:_ForceClearRequest()
    end)
    self:RegisterMessage(MessageConst.ON_LEAVE_DUNGEON, function()
        self:_ForceClearRequest()
    end)
    self:RegisterMessage(MessageConst.ON_SUB_GAME_RESET, function()
        self:_ForceClearRequest()
    end)
end

CommonTaskTrackSystem.OnInit = HL.Override() << function(self)
end

CommonTaskTrackSystem._InitConfigs = HL.Method() << function(self)
    self.configs = {


        DungeonInfo = {
            needWait = true,
            order = 1,
        },


        SeasonTowerEnemyBuffPopup = {
            needWait = true,
            order = 1,
        },


        TrackStartCountdown = {
            needWait = false,
            order = 5,
        },




        ForceClearTrackHud = {
            needWait = false,
            order = 5,
            forceClear = true,
        },


        TrackHud = {
            needWait = false,
            order = 5,
        },

        CoinActivityIntro = {
            needWait = true,
            order = 5,
        },


        TrackStartToast = {
            needWait = true,
            order = 5,
        },


        TrackStateFinish = {
            needWait = true,
            order = 5,
        },


        TrackStateChange = {
            needWait = false,
            order = 5,
        },


        TrackHudShowEndEffect = {
            needWait = true,
            order = 5,
        },


        TrackEndToast = {
            needWait = true,
            order = 5,
        },


        ContingencyContractHudTimer = {
            needWait = false,
            order = 10,
        },



        SeasonTowerRankUp = {
            needWait = true,
            order = 15,
        },


        DungeonSettlement = {
            needWait = false,
            order = 20,
        },


        ContingencyContractSettlement = {
            needWait = false,
            order = 20,
        },


        DeathInfo = {
            needWait = false,
            order = 20,
        },




        BlackboxDiff = {
            needWait = false,
            order = 100,
        },


        ContingencyContractHud = {
            needWait = false,
            order = 100,
        },


        CoinActivityBuffToast = {
            needWait = false,
            order = 100,
        },


        CoinTaskFinishToast = {
            needWait = false,
            order = 100,
        },

    }

    for k, v in pairs(self.configs) do
        v.name = k
    end
end

CommonTaskTrackSystem.AddRequest = HL.Method(HL.String, HL.Function, HL.Opt(HL.Function))
        << function(self, type, action, interruptAction)
    local requestId = self.m_nextRequestId
    self.m_nextRequestId = self.m_nextRequestId + 1
    local cfg = self.configs[type]
    if cfg.forceClear then
        for _, request in ipairs(self.m_pendingRequests) do
            if request.interruptAction then
                request.interruptAction()
            end
        end
        self.m_pendingRequests = {}
        self.m_isShowing = false
    end

    local request = {
        id = requestId,
        type = type,
        action = action,
        order = cfg.order,
        noRemoveRequests = cfg.noRemoveRequests,
        interruptAction = interruptAction,
    }
    table.insert(self.m_pendingRequests, request)
    table.sort(self.m_pendingRequests, Utils.genSortFunction({ "order", "id" }, true))
    logger.info("CommonTaskTrackSystem.AddRequest type:", type)
    self:_TryAddStartPanelTimer()
end

CommonTaskTrackSystem._TryAddStartPanelTimer = HL.Method() << function(self)
    if self.m_isShowing or self.m_pendingRequests[1] == nil then
        logger.info("CommonTaskTrackSystem._TryAddStartPanelTimer Skipped")
        return
    end
    if self.m_tryStartPanelTimerId > 0 then
        logger.info("CommonTaskTrackSystem._TryAddStartPanelTimer Duplicated")
        return
    end
    logger.info("CommonTaskTrackSystem._TryAddStartPanelTimer Succ")
    self.m_tryStartPanelTimerId = self:_StartTimer(0, function()
        self:_TryStartPanel()
    end)
end

CommonTaskTrackSystem._CanStartOnTopPhase = HL.Method(HL.Number).Return(HL.Boolean) << function(self, topPhase)
    if topPhase == PhaseId.Level then
        return true
    end

    if topPhase ~= PhaseId.PowerPoleFastTravel then
        return false
    end

    local request = self.m_pendingRequests[1]
    return request ~= nil and PowerPoleFastTravelAllowedRequestTypes[request.type] == true
end


CommonTaskTrackSystem._TryStartPanel = HL.Method() << function(self)
    logger.info("CommonTaskTrackSystem._TryStartPanel")

    self.m_tryStartPanelTimerId = -1

    local hasPhaseLevel = PhaseManager:IsOpen(PhaseId.Level)
    if not hasPhaseLevel then
        logger.info("CommonTaskTrackSystem._TryStartPanel fail, no phase level")
        return
    end

    local topPhase = PhaseManager:GetTopPhaseId()
    if not self:_CanStartOnTopPhase(topPhase) then
        logger.info("CommonTaskTrackSystem._TryStartPanel fail, invalid top phase", topPhase)
        return
    end

    if PhaseManager:CheckIsInTransition() then
        logger.info("CommonTaskTrackSystem._TryStartPanel fail, phase is in transition")
        self:_TryAddStartPanelTimer()
        return
    end

    if LuaSystemManager.mainHudActionQueue.m_isShowing then
        logger.info("CommonTaskTrackSystem._TryStartPanel fail, mainHudActionQueue is showing")
        return
    end

    self.m_isShowing = true
    self:_StartFirstRequest()
end

CommonTaskTrackSystem._StartFirstRequest = HL.Method() << function(self)
    local request = self.m_pendingRequests[1]
    request.order = 0
    local type = request.type
    local cfg = self.configs[type]
    logger.info("CommonTaskTrackSystem._StartFirstRequest", type, request)
    Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_START, type)
    request.action()
    if not cfg.needWait then
        self:OnOneCommonTaskPanelFinish(request.type)
    end
end

CommonTaskTrackSystem._ForceClearRequest = HL.Method() << function(self)
    self.m_pendingRequests = {}
    self.m_isShowing = false

    if self.m_tryStartPanelTimerId > 0 then
        self:_ClearTimer(self.m_tryStartPanelTimerId)
    end
end

CommonTaskTrackSystem.OnOneCommonTaskPanelFinish = HL.Method(HL.String) << function(self, type)
    logger.info("CommonTaskTrackSystem.OnOneCommonTaskPanelFinish", type)
    if not self.m_isShowing then
        logger.error("OnOneCommonTaskPanelFinish: Not isShowing", type)
        return
    end
    local request = self.m_pendingRequests[1]
    if request.type ~= type then
        logger.error("OnOneCommonTaskPanelFinish: Type Not Match", type, request)
        return
    end
    table.remove(self.m_pendingRequests, 1)
    Notify(MessageConst.ON_ONE_COMMON_TASK_REQUEST_FINISH)
    if self.m_pendingRequests[1] then
        self:_StartFirstRequest()
    else
        self.m_isShowing = false
        logger.info("CommonTaskTrackSystem showing finished")
    end
end

CommonTaskTrackSystem.Interrupt = HL.Method() << function(self)
    self.m_tryStartPanelTimerId = self:_ClearTimer(self.m_tryStartPanelTimerId)
    if not self.m_isShowing then
        return
    end
    logger.info("CommonTaskTrackSystem.Interrupt")
    self.m_isShowing = false

    local request = self.m_pendingRequests[1]
    if request.interruptAction then
        request.interruptAction()
    end
    if not request.noRemoveRequests then
        table.remove(self.m_pendingRequests, 1)
    end
end

CommonTaskTrackSystem.NeedPendingManiHudToast = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_pendingRequests[1] == nil then
        return false
    end


    if self.m_pendingRequests[1].type == "TrackEndToast" then
        return false
    end

    return true
end

CommonTaskTrackSystem.HasRequest = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_pendingRequests[1] ~= nil and self.configs[self.m_pendingRequests[1].type].needWait == true
end

CommonTaskTrackSystem.HasRequestType = HL.Method(HL.String).Return(HL.Boolean) << function(self, actionType)
    for _, request in ipairs(self.m_pendingRequests) do
        if request.type == actionType then
            return true
        end
    end
    return false
end

HL.Commit(CommonTaskTrackSystem)
return CommonTaskTrackSystem
