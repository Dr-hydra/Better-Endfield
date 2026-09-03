local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonTaskTrackCountdown

local EGameMechanicCountDownComponentType = CS.Beyond.Gameplay.Core.EGameMechanicCountDownComponentType
local EGameMechanicCountingComponentType = CS.Beyond.Gameplay.Core.EGameMechanicCountingComponentType

local Component = {
    CountdownCenter = 1,
    CountdownTopLeft = 2,

    Switch = 50,

    CountingCenter = 51,
    CountingTopLeft = 52,
}

local CountdownTypeTable = {
    [EGameMechanicCountDownComponentType.Center] = {
        componentType = Component.CountdownCenter,
        prepareAction = "_CountdownCenterPrepareAction",
        tickAction = "_CountdownCenterTickAction",
        node = "countdownCenter",
    },
    [EGameMechanicCountDownComponentType.TopLeft] = {
        componentType = Component.CountdownTopLeft,
        prepareAction = "_CountdownTopLeftPrepareAction",
        tickAction = "_CountdownTopLeftTickAction",
        nodeFollowAction = "_CountdownTopLeftPosFollowTickAction",
        node = "countdownTopLeft",
    },
}

local CountingTypeTable = {
    [EGameMechanicCountingComponentType.Center] = {
        componentType = Component.CountingCenter,
        prepareAction = "_CountingCenterPrepareAction",
        tickAction = "_CountingCenterTickAction",
        node = "countingCenter",
    },
    [EGameMechanicCountingComponentType.TopLeft] = {
        componentType = Component.CountingTopLeft,
        prepareAction = "_CountingTopLeftPrepareAction",
        tickAction = "_CountingTopLeftTickAction",
        nodeFollowAction = "_CountingTopLeftPosFollowTickAction",
        node = "countingTopLeft",
    },
 }

local maintainPanelCategoryList = {
    DungeonConst.DUNGEON_CATEGORY.BossRush,
}

CommonTaskTrackCountdownCtrl = HL.Class('CommonTaskTrackCountdownCtrl', uiCtrl.UICtrl)

CommonTaskTrackCountdownCtrl.m_countDownTickId = HL.Field(HL.Number) << -1

CommonTaskTrackCountdownCtrl.m_isInAlter = HL.Field(HL.Boolean) << false

CommonTaskTrackCountdownCtrl.m_countingTickId = HL.Field(HL.Number) << -1

CommonTaskTrackCountdownCtrl.m_typeTable = HL.Field(HL.Table)

CommonTaskTrackCountdownCtrl.m_originalAnchoredPos = HL.Field(Vector2)

CommonTaskTrackCountdownCtrl.m_nodeFollowTickId = HL.Field(HL.Number) << -1





CommonTaskTrackCountdownCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CLOSE_COMMON_TASK_COUNTDOWN] = "OnCloseCommonTaskCountdown",
    [MessageConst.ON_FINISH_COMMON_TASK_COUNTING] = "OnFinishCommonTaskCounting",

    [MessageConst.ON_DUNGEON_COMPLETE] = "OnDungeonComplete",



}

CommonTaskTrackCountdownCtrl.OnShowCommonTaskCountdown = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = CommonTaskTrackCountdownCtrl.AutoOpen(PANEL_ID, args, true)
    ctrl:ShowCountdown(args)
end

CommonTaskTrackCountdownCtrl.OnStartCommonTaskCounting = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = CommonTaskTrackCountdownCtrl.AutoOpen(PANEL_ID, args, true)
    ctrl:StartCounting(args)
end


CommonTaskTrackCountdownCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_originalAnchoredPos = self.view.main.anchoredPosition
end

CommonTaskTrackCountdownCtrl.OnClose = HL.Override() << function(self)
    if self.m_countDownTickId > 0 then
        self.m_countDownTickId = LuaUpdate:Remove(self.m_countDownTickId)
    end

    if self.m_countingTickId > 0 then
        self.m_countingTickId = LuaUpdate:Remove(self.m_countingTickId)
    end

    if self.m_nodeFollowTickId > 0 then
        self.m_nodeFollowTickId = LuaUpdate:Remove(self.m_nodeFollowTickId)
    end
end

CommonTaskTrackCountdownCtrl._ToggleComponentOn = HL.Method(HL.Number) << function(self, component)
    self.view.countingCenter.gameObject:SetActiveIfNecessary(component == Component.CountingCenter)
    self.view.countdownCenter.gameObject:SetActiveIfNecessary(component == Component.CountdownCenter)
    self.view.countingTopLeft.gameObject:SetActiveIfNecessary(component == Component.CountingTopLeft)
    self.view.countdownTopLeft.gameObject:SetActiveIfNecessary(component == Component.CountdownTopLeft)
end



CommonTaskTrackCountdownCtrl._CountdownCenterPrepareAction = HL.Method() << function(self)
    local node = self.view.countdownCenter
    node.stateController:SetState("Normal")
end

CommonTaskTrackCountdownCtrl._CountdownCenterTickAction = HL.Method(HL.Number, HL.Boolean) << function(self, leftTime, needAlterAudio)
    local node = self.view.countdownCenter
    node.timeTxt.text = UIUtils.getLeftTimeToSecond(math.max(0, leftTime))

    if not self.m_isInAlter and leftTime <= self.view.config.COUNTDOWN_ALERT_TIME_THRESHOLD then
        self.m_isInAlter = true
        node.stateController:SetState("Alert")
    end

    if needAlterAudio then
        AudioAdapter.PostEvent("Au_UI_Toast_TickDown_OS")
    end
end

CommonTaskTrackCountdownCtrl._CountdownTopLeftPrepareAction = HL.Method() << function(self)
    local node = self.view.countdownTopLeft
    node.stateController:SetState("Normal")
end

CommonTaskTrackCountdownCtrl._CountdownTopLeftTickAction = HL.Method(HL.Number, HL.Boolean) << function(self, leftTime, needAlterAudio)
    local node = self.view.countdownTopLeft
    node.timeTxt.text = UIUtils.getLeftTimeToSecond(math.max(0, leftTime))

    if not self.m_isInAlter and leftTime <= self.view.config.COUNTDOWN_ALERT_TIME_THRESHOLD then
        self.m_isInAlter = true
        node.stateController:SetState("Alert")
        node.animationWrapper:Play("tasktrackcountdown_topleft_red")
    end

    if needAlterAudio then
        AudioAdapter.PostEvent("Au_UI_Toast_DungeonNormalTick_OS")
    end

end

CommonTaskTrackCountdownCtrl._CountdownTopLeftPosFollowTickAction = HL.Method(HL.Number) << function(self, deltaTime)
    local node = self.view.countdownTopLeft
    self:_UpdateTopLeftPos(node)
end

CommonTaskTrackCountdownCtrl.ShowCountdown = HL.Method(HL.Any) << function(self, arg)
    local countdownType, countdownDurationMs, gameId, cb = unpack(arg)
    local countDownCfg = CountdownTypeTable[countdownType]
    local prepareAction = self[countDownCfg.prepareAction]
    if prepareAction then
        prepareAction(self)
    end
    self:_ToggleComponentOn(countDownCfg.componentType)
    self.m_typeTable = countDownCfg

    local lastLeftTime = math.floor(countdownDurationMs / 1000)
    self.m_countDownTickId = LuaUpdate:Remove(self.m_countDownTickId)
    self.m_countDownTickId = LuaUpdate:Add("Tick", function(deltaTime)
        local succ, game = GameWorld.subGameManager:TryGetSubGameById(gameId)
        if not succ then
            return
        end
        if game.waitingSrvResume or not game.activeCountDown then
            return
        end

        local leftTime = math.floor(game.activeCountDown:GetLeftTimeMs() / 1000)
        if leftTime > lastLeftTime then


            leftTime = lastLeftTime
        end

        local needAlterAudio = leftTime <= self.view.config.COUNTDOWN_ALERT_TIME_THRESHOLD and lastLeftTime ~= leftTime
        lastLeftTime = leftTime

        local tickAction = self[countDownCfg.tickAction]
        if tickAction then
            tickAction(self, leftTime, needAlterAudio)
        end

        if leftTime <= 0 then
            if cb then
                cb()
            end
            self.m_countDownTickId = LuaUpdate:Remove(self.m_countDownTickId)
        end
    end)

    local nodeFollowAction = countDownCfg.nodeFollowAction
    if string.isEmpty(nodeFollowAction) then
        return
    end

    local followFunc = self[countDownCfg.nodeFollowAction]
    if followFunc == nil then
        return
    end

    self.m_nodeFollowTickId = LuaUpdate:Remove(self.m_nodeFollowTickId)
    self.m_nodeFollowTickId = LuaUpdate:Add("Tick", function(deltaTime)
        followFunc(self, deltaTime)
    end)
end





CommonTaskTrackCountdownCtrl.StartCounting = HL.Method(HL.Any) << function(self, arg)
    local countingType, gameId = unpack(arg)
    local countingTypeCfg = CountingTypeTable[countingType]
    local prepareAction = self[countingTypeCfg.prepareAction]
    if prepareAction then
        prepareAction(self)
    end
    self:_ToggleComponentOn(countingTypeCfg.componentType)
    self.m_typeTable = countingTypeCfg

    local lastCounting = 0
    self.m_countingTickId = LuaUpdate:Remove(self.m_countingTickId)
    self.m_countingTickId = LuaUpdate:Add("Tick", function(deltaTime)
        local succ, game = GameWorld.subGameManager:TryGetSubGameById(gameId)
        if not succ then
            return
        end
        if game.waitingSrvResume or not game.activeCounting then
            return
        end

        local timePassed = math.floor(game.activeCounting:GetTimePassedInMs() / 1000)

        timePassed = timePassed < 0 and 0 or timePassed

        if timePassed < lastCounting then
            return
        end

        lastCounting = timePassed
        local tickAction = self[countingTypeCfg.tickAction]
        if tickAction then
            tickAction(self, timePassed)
        end
    end)

    local nodeFollowAction = countingTypeCfg.nodeFollowAction
    if string.isEmpty(nodeFollowAction) then
        return
    end

    local followFunc = self[countingTypeCfg.nodeFollowAction]
    if followFunc == nil then
        return
    end

    self.m_nodeFollowTickId = LuaUpdate:Remove(self.m_nodeFollowTickId)
    self.m_nodeFollowTickId = LuaUpdate:Add("Tick", function(deltaTime)
        followFunc(self, deltaTime)
    end)
end

CommonTaskTrackCountdownCtrl._CountingCenterPrepareAction = HL.Method() << function(self)
end

CommonTaskTrackCountdownCtrl._CountingCenterTickAction = HL.Method(HL.Number) << function(self, timePassed)
    local node = self.view.countingCenter
    node.timeTxt.text = UIUtils.getLeftTimeToSecond(timePassed)
end

CommonTaskTrackCountdownCtrl._CountingTopLeftPrepareAction = HL.Method() << function(self)
end

CommonTaskTrackCountdownCtrl._CountingTopLeftTickAction = HL.Method(HL.Number) << function(self, timePassed)
    local node = self.view.countingTopLeft
    node.timeTxt.text = UIUtils.getLeftTimeToSecond(timePassed)
end

CommonTaskTrackCountdownCtrl._CountingTopLeftPosFollowTickAction = HL.Method(HL.Number) << function(self, deltaTime)
    local node = self.view.countingTopLeft
    self:_UpdateTopLeftPos(node)
end




CommonTaskTrackCountdownCtrl.OnCloseCommonTaskCountdown = HL.Method() << function(self)
    self.m_countDownTickId = LuaUpdate:Remove(self.m_countDownTickId)
    if self:_MaintainPanel() then
        return
    end
    self:_CommonClosePanel()
end

CommonTaskTrackCountdownCtrl.OnFinishCommonTaskCounting = HL.Method() << function(self)
    self.m_countingTickId = LuaUpdate:Remove(self.m_countingTickId)
    if self:_MaintainPanel() then
        return
    end
    self:_CommonClosePanel()
end

CommonTaskTrackCountdownCtrl._MaintainPanel = HL.Method().Return(HL.Boolean) << function(self)
    local subGameId = GameWorld.worldInfo.curSubGameId
    if string.isEmpty(subGameId) then
        return false
    end

    local succ, gameMechanicsCfg = Tables.gameMechanicTable:TryGetValue(subGameId)
    if not succ then
        return false
    end

    return lume.find(maintainPanelCategoryList, gameMechanicsCfg.gameCategory) ~= nil

            and GameWorld.worldInfo.subGame.state == CS.Beyond.Gameplay.Core.ISubGame.SubGameState.Completed
end

CommonTaskTrackCountdownCtrl.OnDungeonComplete = HL.Method(HL.Table) << function(self, args)

    if self.m_typeTable.componentType < Component.Switch then
        return
    end

    local nodeName = self.m_typeTable.node
    local node = self.view[nodeName]

    local isNewTimeRecord, curGameTimeRecord = unpack(args)
    if node and node.timeTxt then
        node.timeTxt.text = UIUtils.getLeftTimeToSecond(math.floor(curGameTimeRecord / 1000))
    end
end



CommonTaskTrackCountdownCtrl._CommonClosePanel = HL.Method() << function(self)
    local nodeStr = self.m_typeTable.node
    if not self.m_typeTable or string.isEmpty(nodeStr) or not self.view[nodeStr]
            or not self.view[nodeStr].animationWrapper then
        self:Close()
        return
    end

    self.view[nodeStr].animationWrapper:PlayOutAnimation(function()
        self:_StartCoroutine(function()
            coroutine.waitForRenderDone()
            self:Close()
        end)
    end)
end

CommonTaskTrackCountdownCtrl._UpdateTopLeftPos = HL.Method(HL.Any) << function(self, node)
    local success, mainHudCtrl = UIManager:IsOpen(PanelId.MainHud)
    if success then
        mainHudCtrl.view.topLeftBtns.topLeftBtnFollowerPositionNode.gameObject:SetActiveIfNecessary(true)
        local targetPos = mainHudCtrl.view.topLeftBtns.topLeftBtnFollowerPositionNode.anchoredPosition
        if mainHudCtrl.view.topLeftBtns.expandBtn.gameObject.activeSelf then
            local width = mainHudCtrl.view.topLeftBtns.expandNode.transform.rect.width
            local localScale = mainHudCtrl.view.topLeftBtns.expandNode.transform.localScale
            node.transform.anchoredPosition = Vector2(targetPos.x - width * (1 - localScale.x), targetPos.y)
        else
            node.transform.anchoredPosition = targetPos
        end
    end
end





CommonTaskTrackCountdownCtrl.OnAddHeadBar = HL.Method(HL.Table) << function(self, args)
    local succ, ctrl = UIManager:IsOpen(PanelId.BattleBossInfo)
    if not succ then
        return
    end

    local targetAbilitySystem = unpack(args)
    if targetAbilitySystem and targetAbilitySystem.showBigHeadBar then
        if targetAbilitySystem.alive then
            self:_StartCoroutine(function()
                coroutine.step()
                local active, pos = ctrl:GetFollowPointPosition()
                if active then
                    DOTween.To(function()
                        return self.view.main.position
                    end, function(value)
                        self.view.main.position = value
                    end, pos, self.view.config.COUNTDOWN_TWEEN_TO_TARGET_DURATION)
                end
            end)
        else
            DOTween.To(function()
                return self.view.main.anchoredPosition
            end, function(value)
                self.view.main.anchoredPosition = value
            end, self.m_originalAnchoredPos, self.view.config.COUNTDOWN_TWEEN_TO_TARGET_DURATION)
        end
    end
end

CommonTaskTrackCountdownCtrl.OnRemoveHeadBar = HL.Method(HL.Table) << function(self, args)
    local targetAbilitySystem = unpack(args)
    if targetAbilitySystem and targetAbilitySystem.showBigHeadBar then
        DOTween.To(function()
            return self.view.main.anchoredPosition
        end, function(value)
            self.view.main.anchoredPosition = value
        end, self.m_originalAnchoredPos, self.view.config.COUNTDOWN_TWEEN_TO_TARGET_DURATION)
    end
end



HL.Commit(CommonTaskTrackCountdownCtrl)
