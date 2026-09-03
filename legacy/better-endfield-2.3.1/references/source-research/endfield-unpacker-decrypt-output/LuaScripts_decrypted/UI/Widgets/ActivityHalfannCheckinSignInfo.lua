local ActivityCheckInBase = require_ex('UI/Widgets/ActivityCheckInBase')

ActivityHalfannCheckinSignInfo = HL.Class('ActivityHalfannCheckinSignInfo', ActivityCheckInBase)

local stateTable = {
    NotComplete = 1,
    Complete = 2,
    Done = 3,
}

ActivityHalfannCheckinSignInfo.m_fixedCells = HL.Field(HL.Table)


ActivityHalfannCheckinSignInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.m_fixedCells = {
        Utils.wrapLuaNode(self.view.signInCell01),
        Utils.wrapLuaNode(self.view.signInCell02),
        Utils.wrapLuaNode(self.view.signInCell03),
    }


    for i = 1, #self.m_fixedCells do
        local cell = self.m_fixedCells[i]
        if not cell.getRewardKeyHint then
            cell.getRewardKeyHint = { gameObject = { SetActive = function() end, activeSelf = false } }
        end
    end


    self.m_scrollList = {
        Get = function(_, csIndex)
            local luaIdx = LuaIndex(csIndex)
            if luaIdx >= 1 and luaIdx <= #self.m_fixedCells then
                return self.m_fixedCells[luaIdx].gameObject
            end
            return nil
        end,
        ScrollToIndex = function() end,
    }
    self.m_getRewardCell = function(obj)
        for i = 1, #self.m_fixedCells do
            if self.m_fixedCells[i].gameObject == obj then
                return self.m_fixedCells[i]
            end
        end
        return nil
    end
end

ActivityHalfannCheckinSignInfo.Init = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    self:_InitAnim({
    })

    self.m_scrollNaviGroup = self.view.cellNaviGroup

    self:_InitHalfannActivityInfo(args)

    self:_InitReceiveAll({
        receiveAllBtn = self.view.allReceiveBtn,
        receiveRedDot = self.view.receiveRedDot,
    })

    self:_RefreshAllCells()

    self:_InitHalfannController()
end

ActivityHalfannCheckinSignInfo._InitHalfannActivityInfo = HL.Method(HL.Table) << function(self, args)
    self.m_isPopup = args.isPopup or false

    self:RegisterMessage(MessageConst.ON_ACTIVITY_CHECK_IN, function(arg)
        self:_OnHalfannCheckIn(arg)
    end)
    self:RegisterMessage(MessageConst.ON_ACTIVITY_UPDATED, function(arg)
        local modifyId = unpack(arg)
        if modifyId == self.m_activityId then
            self:_OnHalfannCheckIn(arg)
        end
    end)
    self:RegisterMessage(MessageConst.CHECK_IN_REWARD, function(arg)
        self:OnRewardInfo(arg)
    end)

    self.m_activityId = args.activityId
    self.m_Force2digits = Tables.checkInInfoTable[self.m_activityId].forceShowTwoDigits
    self.m_activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    self:_RefreshRewardDays()

    if self.view.descTxt then
        self.view.descTxt:SetAndResolveTextStyle(Tables.checkInInfoTable[self.m_activityId].checkinDes)
    end

    self.m_rewards = Tables.CheckInRewardTable[self.m_activityId].stageList
    self.m_totalDays = #self.m_rewards

    self.m_firstCanReceiveDay = self.m_activity.loginDays
    for index = 1, self.m_totalDays do
        if self:_GetState(index) == stateTable.Complete then
            self.m_firstCanReceiveDay = index
            break
        end
    end

    self.m_canGetReward = self.m_activity.loginDays ~= self.m_activity.rewardDays.Count

    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    if self.m_isPopup then
        self.view.activityCommonInfo.view.infoNode.descriptionLayout.gameObject:SetActive(false)
    end

    if self.m_isPopup then
        ActivityUtils.actionWhenActivityClosed(function()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
            if args.closeCallback then
                args.closeCallback()
            end
        end, self, self.m_activityId)
    else
        ActivityUtils.backToMainHudWhenActivityClosed(self, self.m_activityId)
    end
end



ActivityHalfannCheckinSignInfo._RefreshAllCells = HL.Method() << function(self)
    local cellCount = math.min(self.m_totalDays, #self.m_fixedCells)
    for i = 1, cellCount do
        self:_UpdateFixedCell(self.m_fixedCells[i], i)
    end
end

ActivityHalfannCheckinSignInfo._UpdateFixedCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local state = self:_GetState(index)

    cell.gameObject.name = "Cell" .. tostring(index)

    local rewardId = self.m_rewards[CSIndex(index)].rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    cell.rewardCellCache = cell.rewardCellCache or UIUtils.genCellCache(cell.itemBlack)
    cell.rewardCellCache:Refresh(#rewardBundles, function(innerCell, innerIndex)
        local reward = {
            id = rewardBundles[innerIndex].id,
            count = rewardBundles[innerIndex].count,
            forceHidePotentialStar = true,
        }
        innerCell:InitItem(reward, function()
            if not DeviceInfo.usingController and state == stateTable.Complete then
                self:_GainReward({ index })
            else
                innerCell:ShowTips()
            end
        end)
        innerCell:SetExtraInfo({
            tipsPosTransform = innerCell.view.content,
            isSideTips = true,
        })
        innerCell.view.stateController:SetState(state == stateTable.Done and "Done" or "Nrl")
    end)

    if state == stateTable.NotComplete then
        cell.stateController:SetState("NormalState")
    elseif state == stateTable.Complete then
        cell.stateController:SetState("ReceiveState")
    elseif state == stateTable.Done then
        cell.stateController:SetState("DoneState")
    end

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if state == stateTable.Complete then
            AudioManager.PostEvent("Au_UI_Event_CheckInPanel_Receive")
            self:_GainReward({ index })
        else
            AudioManager.PostEvent("Au_UI_Button_Common")
        end
    end)
    cell.redDot:InitRedDot("ActivityCheckInReward", state == stateTable.Complete)
    cell.naviFrame.gameObject:SetActive(false)

    if DeviceInfo.usingController then
        cell.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
        cell.getRewardKeyHint.gameObject:SetActive(false)
        self:_ToggleCell(index, false)
        cell.detailKeyHint.gameObject:SetActive(false)
        cell.button.onIsNaviTargetChanged = function(isTarget)
            cell.naviFrame.gameObject:SetActive(isTarget)
            self:_ToggleCell(index, isTarget)
        end
    else
        cell.inputBindingGroupMonoTarget.enabled = true
    end
end





ActivityHalfannCheckinSignInfo._OnHalfannCheckIn = HL.Method(HL.Any) << function(self, args)
    self:_RefreshRewardDays()
    self:_RefreshAllCells()

    if self.m_receiveAllBtn then
        self.m_canGetReward = self.m_activity.loginDays ~= self.m_activity.rewardDays.Count
        self.m_receiveAllBtn.gameObject:SetActive(self.m_canGetReward)
    end

    if DeviceInfo.usingController and self.m_focusIndex > 0 then
        self:_SetNaviTarget(self.m_focusIndex)
    end
end





ActivityHalfannCheckinSignInfo._OnEnable = HL.Override() << function(self)
    if self.m_activity and DeviceInfo.usingController then
        self:_RefreshAllCells()
        if not self.luaPanel then
            self:GetLuaPanel()
        end
        self.luaPanel.onAnimationInFinished:AddListener(function()
            self:_SetNaviTarget(self.m_focusIndex)
        end)
    end
end





ActivityHalfannCheckinSignInfo._InitHalfannController = HL.Method() << function(self)
    if DeviceInfo.usingController then
        if self.m_isPopup then
            if not self.luaPanel then
                self:GetLuaPanel()
            end
            self.luaPanel.onAnimationInFinished:AddListener(function()
                self:_SetNaviTarget(self.m_firstCanReceiveDay)
            end)
        else
            local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
                self:OnActivityCenterNaviFailed()
            end)
            self.m_scrollNaviGroup.onIsTopLayerChanged:AddListener(function(active)
                if not active then
                    self.m_focusIndex = 0
                end
                InputManagerInst:ToggleBinding(viewBindingId, not active)
            end)
        end
    end
end

ActivityHalfannCheckinSignInfo.OnActivityCenterNaviFailed = HL.Override() << function(self)
    self:_SetNaviTarget(1)
end



HL.Commit(ActivityHalfannCheckinSignInfo)
return ActivityHalfannCheckinSignInfo
