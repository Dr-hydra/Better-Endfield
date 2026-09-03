
local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')

MainHudActionQueueSystem = HL.Class('MainHudActionQueueSystem', LuaSystemBase.LuaSystemBase)


MainHudActionQueueSystem._InitConfigs = HL.Method() << function(self)






























































    self.configs = {



        LoginCheck_ImportantActivity_WaitCinematic = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST - 40,
            needWait = true,
            ignoreCinematicInterrupt = true,
            ignoreMainHudInterrupt = true,
            finishWhenInterrupt = true,
            dropWhenChangeScene = true,
        },




        LoginCheck_ImportantActivity_Top = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST - 40,
            needWait = true,
            ignoreCinematicInterrupt = true,
            ignoreMainHudInterrupt = true,
            finishWhenInterrupt = true,
            dropWhenChangeScene = true,
        },


        LoginCheck_CashShopOrderSettle = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST - 30,
            needWait = true,
            ignoreCinematicInterrupt = true,
            ignoreMainHudInterrupt = true,
            finishWhenInterrupt = true,
        },

        LoginCheck_MonthlypassPopup = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST - 20,
            needWait = true,
            ignoreCinematicInterrupt = true,
            ignoreMainHudInterrupt = true,
        },

        LoginCheck_StartGuide = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST - 10,
            needWait = false,
            ignoreCinematicInterrupt = true,
            ignoreMainHudInterrupt = true,
        },

        LoginCheck_ForceSNS = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST - 10,
            needWait = false,
            ignoreCinematicInterrupt = true,
            ignoreMainHudInterrupt = true,
        },


        LoginCheck_PreventedClearScreenForLevelScripts = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST - 1,
            needWait = false,
            dropWhenChangeScene = true,
            ignoreCinematicInterrupt = true,
            ignoreMainHudInterrupt = true,
        },


        CinematicBlocker = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST - 1,
            needWait = true,
            isCinematic = true,
            dropWhenChangeScene = true,
        },


        Cinematic = {
            order = CS.Beyond.MainHudActionQueueConsts.CINEMATIC_ORDER_FIRST,
            needWait = true,
            isCinematic = true,
            dropWhenChangeScene = true,
        },



        ChapterPanelWithoutMissionHud =
        {
            order = CS.Beyond.MainHudActionQueueConsts.BLOCKER_ORDER,
            needWait = true,
            finishWhenInterrupt = true,
            dropWhenChangeScene = true,
        },



        MissionHudCompleteBlocker = {
            order = CS.Beyond.MainHudActionQueueConsts.BLOCKER_ORDER,
            needWait = true,
            finishWhenInterrupt = false,
            dropWhenChangeScene = true,
        },


        CashShopOrderSettleInterrupt = {
            order = -0.1,
            needWait = true,
            waitForceGuideEnd = true,
            ignoreMainHudInterrupt = true,
        },


        CashShopOrderSettle = {
            order = 0,
            needWait = true,
            waitForceGuideEnd = true,
            ignoreMainHudInterrupt = true,
        },


        MonthlyPassPopup = {
            order = 0.1,
            needWait = true,
            finishWhenInterrupt = true,
            waitForceGuideEnd = true,
            ignoreMainHudInterrupt = true,
        },



        LoginCheck_ImportantActivity_Fallback = {
            order = 0.15,
            needWait = true,
            finishWhenInterrupt = true,
            waitForceGuideEnd = true,
            ignoreMainHudInterrupt = true,
            ignoreCinematicInterrupt = true,
            dropWhenChangeScene = true,
        },


        LoginCheck_Reflow = {
            order = 0.18,
            needWait = true,
            finishWhenInterrupt = true,
            waitForceGuideEnd = true,
            ignoreMainHudInterrupt = true,
        },


        LoginCheck_ActivityCheckIn = {
            order = 0.2,
            needWait = true,
            finishWhenInterrupt = true,
            waitForceGuideEnd = true,
            ignoreMainHudInterrupt = true,
        },


        DashBarUpgrade = {
            order = 0.5,
            needWait = true,
            ignoreCinematicInterrupt = true,
            waitForceGuideEnd = true,
        },


        FacTechPointGained = {
            order = 1,
            needWait = true,
            finishWhenInterrupt = true,
            preloadPanelId = PanelId.FacTechPointGainedToast,
        },


        CenterRewards = {
            order = 2,
            needWait = true,
            preloadPanelId = PanelId.RewardsPopupCenter,
        },


        EquipFormulaRewardPopup = {
            order = 2,
            needWait = true,
            finishWhenInterrupt = true,
            preloadPanelId = PanelId.EquipFormulaRewardPopup,
        },


        WorldLevelPreview = {
            order = 2.1,
            needWait = true,
            finishWhenInterrupt = true,
            dropWhenChangeScene = true,
            waitForceGuideEnd = true,
            ignoreMainHudInterrupt = true,
        },


        SettlementToast = {
            order = 2.5,
            needWait = true,
            finishWhenInterrupt = true,
        },


        ImportantReward = {
            order = 3,
            needWait = true,
            ignoreCinematicInterrupt = true,
            preloadPanelId = PanelId.ImportantRewardPopup,
            waitForceGuideEnd = true,
        },


        AdventureLevelUp = {
            order = 4,
            needWait = true,
            finishWhenInterrupt = false,
            preloadPanelId = PanelId.AdventureLevelUp,
            waitForceGuideEnd = true,
        },


        AfterLevelUpSNS = {
            order = 4.1,
            needWait = true,
            dropWhenChangeScene = true,
            ignoreMainHudInterrupt = true,
        },


        PuzzlePickup = {
            order = 6,
            needWait = true,
            finishWhenInterrupt = true,
            preloadPanelId = PanelId.PuzzlePickupToast,
        },

        EndingToast = {
            order = 6,
            needWait = true,
            finishWhenInterrupt = true,
        },


        MissionHudResumeInfo = {
            order = 7,
            needWait = false,
        },


        CommonPOIUpgradeToast = {
            order = 8,
            needWait = true,
            finishWhenInterrupt = true,
            preloadPanelId = PanelId.CommonPOIUpgradeToast,
        },


        DomainDepotDeliverToast = {
            order = 9,
            needWait = true,
            finishWhenInterrupt = true,
            preloadPanelId = PanelId.DomainDepotDeliverToast,
        },


        DomainUpgrade = {
            order = 10,
            needWait = true,
            finishWhenInterrupt = true,
            preloadPanelId = PanelId.DomainUpgrade,
        },


        AppStoreRatingPopup = {
            order = 11,
            needWait = false,
            finishWhenInterrupt = true,
        },


        BackupPowerCheck = {
            order = 97,
            needWait = false,
            ignoreMainHudInterrupt = true,
        },


        MapRegionToast = {
            order = 98,
            needWait = true,
            finishWhenInterrupt = true,
            dropWhenChangeScene = true,
            preloadPanelId = PanelId.MapRegionToast,
        },


        SpaceshipHudTips = {
            order = 99,
            needWait = true,
            dropWhenChangeScene = true,
        },


        SNSNormalNotice = {
            order = 100,
            needWait = false,
            dropWhenChangeScene = true,
        },


        GetItemToast = {
            order = 100,
            needWait = false,
        },


        FirstGotItem = {
            order = 100,
            needWait = false,
        },
        UnlockPRTS = {
            order = 100,
            needWait = false,
        },
        DomainUpgradeToast = {
            order = 100,
            needWait = false,
        },
        WeeklyRaidEnter = {
            order = 100,
            needWait = true,
            finishWhenInterrupt = false,
            dropWhenChangeScene = true,
        },
        UnlockMusic = {
            order = 100,
            needWait = false,
        },
    }

    for k, v in pairs(self.configs) do
        v.name = k
    end
end


MainHudActionQueueSystem.m_pendingRequests = HL.Field(HL.Table)

MainHudActionQueueSystem.configs = HL.Field(HL.Table)

MainHudActionQueueSystem.m_nextRequestId = HL.Field(HL.Number) << 1

MainHudActionQueueSystem.m_isShowing = HL.Field(HL.Boolean) << false

MainHudActionQueueSystem.m_startupDelayCor = HL.Field(HL.Thread)

MainHudActionQueueSystem.m_curShowingActionOrder = HL.Field(HL.Number) << math.mininteger


MainHudActionQueueSystem.MainHudActionQueueSystem = HL.Constructor() << function(self)
    self:_InitConfigs()
    self.m_pendingRequests = {}
    self.m_playIgnoreMainHudActionTypes = {}
    self:RegisterMessage(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, function(arg)
        local name
        if type(arg) == "table" then
            name = unpack(arg)
        else
            name = arg
        end
        self:OnOneMainHudActionFinish(name)
    end)

    self:RegisterMessage(MessageConst.ON_IN_MAIN_HUD_CHANGED, function(arg)
        local inMainHud = unpack(arg)
        self:_OnInMainHudChanged(inMainHud)
    end)

    self:RegisterMessage(MessageConst.ON_DO_CLOSE_MAP, function(arg)
        self:Interrupt(true)
        self:_DropRequestWhenChangeScene()
    end)

    self:RegisterMessage(MessageConst.ON_GUIDE_STOPPED, function(arg)
        local isForce = unpack(arg)
        if isForce then
            self:_TryAddStartActionCor()
        end
    end)

    self:RegisterMessage(MessageConst.ON_ONE_COMMON_TASK_REQUEST_FINISH, function()
        self:_TryAddStartActionCor()
    end)


    self:RegisterMessage(MessageConst.INTERRUPT_MAIN_HUD_ACTION_ON_COMMON_BLEND_IN, function()
        self:Interrupt()
    end)
    self:RegisterMessage(MessageConst.ON_COMMON_BLEND_OUT, function()
        self:_TryAddStartActionCor()
    end)

    self:RegisterMessage(MessageConst.COMMON_START_BLOCK_MAIN_HUD_ACTION_QUEUE, function()
        self:Interrupt()
    end)
    self:RegisterMessage(MessageConst.COMMON_END_BLOCK_MAIN_HUD_ACTION_QUEUE, function()
        self:_TryAddStartActionCor()
    end)

    self:RegisterMessage(MessageConst.ON_LEAVE_TOWER_DEFENSE_DEFENDING_PHASE, function()
        self:Interrupt()
    end)
    self:RegisterMessage(MessageConst.ON_TOWER_DEFENSE_LEVEL_REWARDS_FINISHED, function()
        self:_TryAddStartActionCor()
    end)
    self:RegisterMessage(MessageConst.MAIN_HUD_ACTION_QUEUE_TOGGLE_IGNORE_MAIN_HUD_CS, function(args)
        local actionType, active = unpack(args)
        self:ToggleActionPlayIgnoreMainHud(actionType, active)
    end)
    self:RegisterMessage(MessageConst.PRE_LEVEL_START, function()
        if Utils.isInBlackbox() or Utils.isInDungeonTrain() then
            self:ManuallyDropAllRequests()
        end
    end)

    self:RegisterMessage(MessageConst.ON_CONFIRM_CHANGE_INPUT_DEVICE_TYPE, function()
        self:Interrupt()
    end)
    self:RegisterMessage(MessageConst.ON_SWITCH_LANGUAGE, function()
        self:Interrupt()
    end)

    if BEYOND_DEBUG_COMMAND then
        self:_StartDebugCheck()
    end
end


MainHudActionQueueSystem.AddRequest = HL.Method(HL.String, HL.Function, HL.Opt(HL.Function, HL.Boolean, HL.Function)) << function(self, type, action, getOrder, skipStartupDelay, onDrop)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.AddRequest", type, skipStartupDelay)
    local requestId = self.m_nextRequestId
    self.m_nextRequestId = self.m_nextRequestId + 1
    local cfg = self.configs[type]
    local request = {
        id = requestId,
        type = type,
        action = action,
        getOrder = getOrder,
        onDrop = onDrop,
        cfg = cfg,
    }
    setmetatable(request, { __index = cfg })
    table.insert(self.m_pendingRequests, request)

    if self:_TryCutIn(request) then
        return
    end

    self:_SortRequest(true)

    request.skipStartupDelay = skipStartupDelay
    if skipStartupDelay then
        if self.m_isShowing or self.m_pendingRequests[1] ~= request then
            logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.AddRequest skipStartupDelay 失败", type, "当前：", self.m_isShowing, self.m_pendingRequests[1].type)
        else
            self:_TryStartAction()
            return
        end
    end
    self:_TryAddStartActionCor()
end





MainHudActionQueueSystem._TryCutIn = HL.Method(HL.Table).Return(HL.Boolean) << function(self, request)
    if not request.isCinematic then
        return false
    end

    FactoryUtils.exitFactoryRelatedMode()

    if not (GameWorld.worldInfo.inMainHud or self.m_playIgnoreMainHudActionTypes[request.type]) then
        return false
    end

    local curRequest = self.m_pendingRequests[1]
    if not (curRequest and not curRequest.isCinematic and not curRequest.ignoreCinematicInterrupt) then
        return false
    end

    if self.m_isShowing then
        self:Interrupt()
        curRequest.order = nil
        local nextRequestIndex = self:_GetNextRequestIndex(true)
        self:_MoveRequestToTop(nextRequestIndex)
        self:_StartFirstAction()
        return true
    else

        curRequest.order = nil
        return false
    end
end

MainHudActionQueueSystem._SortRequest = HL.Method(HL.Boolean) << function(self, forceSort)
    local needSort = forceSort
    for k, request in ipairs(self.m_pendingRequests) do
        if request.getOrder and (not self.m_isShowing or k > 1) then
            needSort = true
            request.order = request.getOrder()
        end
    end
    if not needSort then
        return
    end
    table.sort(self.m_pendingRequests, Utils.genSortFunction({ "order", "id" }, true))
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._SortRequest")
end

MainHudActionQueueSystem.IsShowing = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_isShowing
end

MainHudActionQueueSystem.GetCurQueueFirstRequest = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    return self.m_pendingRequests[1]
end

MainHudActionQueueSystem.GetCurQueueFirstRequestType = HL.Method().Return(HL.Opt(HL.String)) << function(self)
    local request = self.m_pendingRequests[1]
    if request then
        return request.type
    end
end

MainHudActionQueueSystem._TryAddStartActionCor = HL.Method() << function(self)
    if self.m_isShowing or self.m_pendingRequests[1] == nil then
        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._TryAddStartActionCor Skipped")
        return
    end
    if self.m_startupDelayCor then
        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._TryAddStartActionCor Duplicated")
        return
    end
    if self.m_lastFinishShowingFrameCount == Time.frameCount then
        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._TryAddStartActionCor 当帧刚结束，所以直接开始，确保播放之间是无缝的")
        self:_TryStartAction()
    elseif self.m_pendingRequests[1].skipStartupDelay then
        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._TryAddStartActionCor skipStartupDelay")
        self:_TryStartAction()
    else
        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._TryAddStartActionCor Succ")
        self.m_startupDelayCor = self:_StartCoroutine(function()



            coroutine.step()
            coroutine.step()
            self:_TryStartAction()
        end)
    end
end

MainHudActionQueueSystem._TryStartAction = HL.Method() << function(self)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._TryStartAction")

    self.m_startupDelayCor = self:_ClearCoroutine(self.m_startupDelayCor)

    if GameInstance.player.guide.preCheckFinished and GameInstance.player.guide.isInForceGuide and not Utils.isInBlackbox() then

        return
    end

    local nextRequestIndex = self:_GetNextRequestIndex(false)
    if not nextRequestIndex then
        return
    end

    if GameInstance.player.inventory.isProcessingRewardToastData then

        return
    end

    if UIManager:IsShow(PanelId.MissionCompletePop) then
        return
    end

    if LuaSystemManager.commonTaskTrackSystem:HasRequest() then
        return
    end

    if Utils.isInSettlementDefense() then
        return
    end

    self:_MoveRequestToTop(nextRequestIndex)
    self:_StartFirstAction()
end

MainHudActionQueueSystem.m_curPreloadId = HL.Field(HL.Any)

MainHudActionQueueSystem.m_nextPreloadId = HL.Field(HL.Number) << 1

MainHudActionQueueSystem._StartFirstAction = HL.Method() << function(self)
    self.m_isShowing = true
    self.m_curActionStartTime = Time.unscaledTime
    local request = self.m_pendingRequests[1]
    request.order = self.m_curShowingActionOrder
    local cfg = self.configs[request.type]

    if cfg.waitForceGuideEnd and GameInstance.player.guide.isInForceGuide and not Utils.isInBlackbox() then

        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._StartFirstAction Failed Because Of Guide", request.type, request)


        request.order = nil
        self:Interrupt(false, true)
        return
    else
        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._StartFirstAction", request.type, request)
    end

    local isResume = request.haveBeenInterrupted
    if request.preloadPanelId then
        local pId = self.m_nextPreloadId
        self.m_nextPreloadId = self.m_nextPreloadId + 1
        self.m_curPreloadId = pId
        UIManager:PreloadPanelAsset(request.preloadPanelId, "MainHudActionQueue", function()
            if self.m_curPreloadId == pId then
                request.action(isResume)
                if not cfg.needWait then
                    self:OnOneMainHudActionFinish(request.type)
                end
            end
        end)
    else
        self.m_curPreloadId = nil
        request.action(isResume)
        if not cfg.needWait then
            if not self.m_isShowing then


                if self.m_pendingRequests[1] == request then
                    table.remove(self.m_pendingRequests, 1)
                end
                return
            end
            self:OnOneMainHudActionFinish(request.type)
        end
    end
end

MainHudActionQueueSystem._NotifyIfAllFinished = HL.Method() << function(self)
    if self.m_pendingRequests[1] ~= nil then
        return
    end

    Notify(MessageConst.ALL_MAIN_HUD_ACTION_FINISH)
end

MainHudActionQueueSystem.m_lastFinishShowingFrameCount = HL.Field(HL.Number) << -1

MainHudActionQueueSystem.OnOneMainHudActionFinish = HL.Method(HL.String) << function(self, type)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.OnOneMainHudActionFinish", type)
    if not self.m_isShowing then
        logger.error("OnOneMainHudActionFinish: 当前不在播放中", type)
        return
    end
    local request = self.m_pendingRequests[1]
    if request.type ~= type then
        logger.error("OnOneMainHudActionFinish: 并不是正在播放的类型", type, request)
        return
    end
    table.remove(self.m_pendingRequests, 1)
    if self.m_pendingRequests[1] then
        local nextRequestIndex = self:_GetNextRequestIndex(false)
        if nextRequestIndex then
            self:_MoveRequestToTop(nextRequestIndex)
            self:_StartFirstAction()
            return
        end
    end
    self.m_isShowing = false
    self.m_curActionStartTime = -1
    self.m_lastFinishShowingFrameCount = Time.frameCount
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem showing finished")
    self:_NotifyIfAllFinished()
    CS.Beyond.Gameplay.Conditions.OnMainHudActionFinished.Trigger(false)
end

MainHudActionQueueSystem.Interrupt = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, includeCinematic, keepInQueue)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.Interrupt TRY", includeCinematic, keepInQueue)
    if not includeCinematic then
        if not self.m_isShowing and self.m_startupDelayCor == nil then
            self.m_curPreloadId = nil
            return
        end
        local request = self.m_pendingRequests[1]
        if request and request.isCinematic then
            return
        end
    end
    self.m_startupDelayCor = self:_ClearCoroutine(self.m_startupDelayCor)
    if not self.m_isShowing then
        self.m_curPreloadId = nil
        return
    end
    local request = self.m_pendingRequests[1]
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.Interrupt", includeCinematic, request.type)
    self.m_curPreloadId = nil
    self.m_isShowing = false
    self.m_curActionStartTime = -1
    local needRemove = request.finishWhenInterrupt and not keepInQueue
    if needRemove then
        table.remove(self.m_pendingRequests, 1)
    end
    Notify(MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE)
    if needRemove then
        self:_NotifyIfAllFinished()
    else
        request.haveBeenInterrupted = true
    end
    CS.Beyond.Gameplay.Conditions.OnMainHudActionFinished.Trigger(true)
end

MainHudActionQueueSystem._DropRequestWhenChangeScene = HL.Method() << function(self)
    local newRequests = {}
    for _, request in ipairs(self.m_pendingRequests) do
        if not request.dropWhenChangeScene then
            table.insert(newRequests, request)
        else
            logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._DropRequestWhenChangeScene", request.name)
            if request.onDrop then
                request.onDrop()
            end
        end
    end
    self.m_pendingRequests = newRequests
end

MainHudActionQueueSystem.RemoveActionsOfType = HL.Method(HL.String) << function(self, actionType)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.RemoveActionsOfType", actionType)
    local needRestart = false
    local requestCnt = #self.m_pendingRequests
    if requestCnt == 0 then

        return
    end

    if self.m_isShowing then
        local currentAction = self.m_pendingRequests[1]
        if currentAction.type == actionType then

            if currentAction.finishWhenInterrupt then

                requestCnt = requestCnt - 1
            end

            self:Interrupt(true)
            needRestart = true
        end
    end


    local leftCount = 0
    local newPendingRequests = {}

    for i = 1, requestCnt do
        local request = self.m_pendingRequests[i]
        if request.type == actionType then
            if request.onDrop then
                request.onDrop()
            end
        else

            leftCount = leftCount + 1
            newPendingRequests[leftCount] = request
        end
    end
    self.m_pendingRequests = newPendingRequests

    if leftCount == 0 then
        self:Interrupt(true)
    elseif needRestart then
        self:_TryStartAction()
    end
end

MainHudActionQueueSystem.HasRequest = HL.Method(HL.String).Return(HL.Boolean) << function(self, actionType)
    for _, request in ipairs(self.m_pendingRequests) do
        if request.type == actionType then
            return true
        end
    end
    return false
end



MainHudActionQueueSystem.m_playIgnoreMainHudActionTypes = HL.Field(HL.Table)

MainHudActionQueueSystem.ToggleActionPlayIgnoreMainHud = HL.Method(HL.String, HL.Boolean) << function(self, actionType, ignoreMainHud)
    local curIsActive = self.m_playIgnoreMainHudActionTypes[actionType] == true
    if curIsActive == ignoreMainHud then
        return
    end
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.ToggleActionPlayIgnoreMainHud", actionType, ignoreMainHud)
    if ignoreMainHud then
        self.m_playIgnoreMainHudActionTypes[actionType] = true
    else
        self.m_playIgnoreMainHudActionTypes[actionType] = nil
    end
    if ignoreMainHud and not self.m_isShowing and self.m_pendingRequests[1] then

        self:_TryStartAction()
    end
end

MainHudActionQueueSystem._GetNextRequestIndex = HL.Method(HL.Boolean).Return(HL.Opt(HL.Number)) << function(self, forceSort)
    self:_SortRequest(forceSort)
    if GameWorld.worldInfo.inMainHud and not CameraManager:IsCommonTempControllerActive() then
        return 1
    else

        if next(self.m_playIgnoreMainHudActionTypes) then
            for k, v in ipairs(self.m_pendingRequests) do
                if self.m_playIgnoreMainHudActionTypes[v.type] then
                    return k
                end
            end
        end
    end
end

MainHudActionQueueSystem._MoveRequestToTop = HL.Method(HL.Opt(HL.Number)) << function(self, index)
    if index and index ~= 1 then
        local request = self.m_pendingRequests[index]
        for k = index, 2, -1 do
            self.m_pendingRequests[k] = self.m_pendingRequests[k - 1]
        end
        self.m_pendingRequests[1] = request
    end
end



MainHudActionQueueSystem.ManuallyDropAllRequests = HL.Method() << function(self)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.ManuallyDropAllRequests")
    if self:IsShowing() then


        logger.error(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.ManuallyDropAllRequests: interrupting current showing action")
        self:Interrupt(true, true)
    end
    for _, request in ipairs(self.m_pendingRequests) do
        logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem.ManuallyDropAllRequests", request.name)
        if request.onDrop then
            request.onDrop()
        end
    end
    self.m_pendingRequests = {}
end

MainHudActionQueueSystem._OnInMainHudChanged = HL.Method(HL.Boolean) << function(self, inMainHud)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "MainHudActionQueueSystem._OnInMainHudChanged", inMainHud)
    if inMainHud then
        self:_TryAddStartActionCor()
        return
    end
    if next(self.m_playIgnoreMainHudActionTypes) then
        local nextRequestIndex = self:_GetNextRequestIndex(false)
        if nextRequestIndex then
            if nextRequestIndex == 1 then
                if self.m_isShowing then

                    return
                else

                    self:_TryStartAction()
                    return
                end
            else

                self:Interrupt()
                self:_TryStartAction()
                return
            end
        end
    end

    if self.m_isShowing then
        local request = self.m_pendingRequests[1]
        if request and request.ignoreMainHudInterrupt then
            return
        end
    end
    self:Interrupt()
end

MainHudActionQueueSystem.HasRequestWaiting = HL.Method(HL.String).Return(HL.Boolean) << function(self, actionType)
    for k, request in ipairs(self.m_pendingRequests) do
        if request.type == actionType then
            if k ~= 1 or not self.m_isShowing then
                return true
            end
        end
    end
    return false
end

MainHudActionQueueSystem.IsInLoginCheck = HL.Method().Return(HL.Boolean) << function(self)
    return self:HasRequest("LoginCheck_StartGuide")
end





MainHudActionQueueSystem.m_curActionStartTime = HL.Field(HL.Number) << -1

MainHudActionQueueSystem.GetDebugInfos = HL.Method(HL.Opt(HL.Boolean)).Return(HL.String) << function(self, shouldPrint)
    local infos = {"<mark>--------------- MainHudActionQueueSystem DebugInfos ---------------\n"}
    table.insert(infos, string.format("--------------- 当前Time.unscaledTime：%s ---------------\n", Time.unscaledTime))


    table.insert(infos, "isShowing=")
    table.insert(infos, tostring(self.m_isShowing))
    if self.m_isShowing then
        table.insert(infos, "\t")
        table.insert(infos, Time.unscaledTime - self.m_curActionStartTime)
        local asyncCount, syncCount = self:_GetPendingActionCount()
        table.insert(infos, string.format("\nGuide: asyncCount=%d, syncCount=%d\n", asyncCount, syncCount))
    end


    table.insert(infos, "\n--------------- 当前队列 m_pendingRequests ---------------\n")
    table.insert(infos, "序号\t类型\t\tID\tskipStartupDelay\t优先级\n")
    for k, v in ipairs(self.m_pendingRequests) do
        table.insert(infos, string.format("%d\t%s\t\t%d\t%s\t%f\n", k, v.type, v.id, v.skipStartupDelay, v.getOrder and v.getOrder() or v.order))
    end


    table.insert(infos, "\n--------------- m_playIgnoreMainHudActionTypes ---------------\n")
    for k, _ in pairs(self.m_playIgnoreMainHudActionTypes) do
        table.insert(infos, k)
        table.insert(infos, ", ")
    end


    table.insert(infos, "\n--------------- 其他相关信息 ---------------\n")
    table.insert(infos, string.format("Guide: preCheckFinished=%s, isInForceGuide=%s, isInBlackbox=%s\n",
            GameInstance.player.guide.preCheckFinished,
            GameInstance.player.guide.isInForceGuide,
            Utils.isInBlackbox()
    ))
    table.insert(infos, string.format("isProcessingRewardToastData=%s, MissionCompletePopPanel=%s, commonTaskTrackSystem:HasRequest=%s, isInSettlementDefense=%s\n",
            GameInstance.player.inventory.isProcessingRewardToastData,
            UIManager:IsShow(PanelId.MissionCompletePop),
            LuaSystemManager.commonTaskTrackSystem:HasRequest(),
            Utils.isInSettlementDefense()
    ))

    table.insert(infos, "</mark>")

    local rst = table.concat(infos)
    if shouldPrint then
        logger.info(rst)
    end
    return rst
end

MainHudActionQueueSystem._GetPendingActionCount = HL.Method().Return(HL.Number, HL.Number) << function(self)
    local asyncCount, syncCount = 0, 0
    for _, request in ipairs(self.m_pendingRequests) do
        if request.needWait then
            if BEYOND_DEBUG_COMMAND and request.type == "ImportantReward" then

            else
                asyncCount = asyncCount + 1
            end
        else
            syncCount = syncCount + 1
        end
    end
    return asyncCount, syncCount
end

MainHudActionQueueSystem.m_lastCheckPendingAsyncActionCount = HL.Field(HL.Number) << 0

MainHudActionQueueSystem.m_firstIsGrowingCheckTime = HL.Field(HL.Number) << 0

local AbnormalWaitingAsyncMinCount = 15
local AbnormalWaitingTime = 60 * 2

MainHudActionQueueSystem._DebugCheckState = HL.Method() << function(self)
    local curAsyncCount = self:_GetPendingActionCount()
    if curAsyncCount >= AbnormalWaitingAsyncMinCount and curAsyncCount >= self.m_lastCheckPendingAsyncActionCount then

        if self.m_firstIsGrowingCheckTime == 0 then
            self.m_firstIsGrowingCheckTime = Time.unscaledTime
        else
            if Time.unscaledTime - self.m_firstIsGrowingCheckTime > AbnormalWaitingTime then
                local str = string.format("MainHudActionQueueSystem._DebugCheckState 发现主界面队列中有 %d 个异步行为，数量异常，可能是队列卡死了", curAsyncCount)
                logger.critical(str)
                Notify(MessageConst.SHOW_TOAST, str)
                logger.critical(self:GetDebugInfos(true))
                self.m_firstIsGrowingCheckTime = 0

            end
        end
    else
        self.m_firstIsGrowingCheckTime = 0
    end
    self.m_lastCheckPendingAsyncActionCount = curAsyncCount
end

MainHudActionQueueSystem._StartDebugCheck = HL.Method() << function(self)
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(1)
            self:_DebugCheckState()
        end
    end)
end





HL.Commit(MainHudActionQueueSystem)
return MainHudActionQueueSystem
