
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityLevelRewards

ActivityLevelRewardsCtrl = HL.Class('ActivityLevelRewardsCtrl', uiCtrl.UICtrl)






ActivityLevelRewardsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_LEVEL_REWARD_UPDATE] = '_OnLevelRewardUpdate',
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnActivityUpdate',
}


ActivityLevelRewardsCtrl.m_rewardCount = HL.Field(HL.Number) << 0

ActivityLevelRewardsCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityLevelRewardsCtrl.m_rewardedID = HL.Field(HL.String) << ""

ActivityLevelRewardsCtrl.m_getRewardCell = HL.Field(HL.Function)

ActivityLevelRewardsCtrl.m_completeStageList = HL.Field(HL.Table)

ActivityLevelRewardsCtrl.m_receiveStageList = HL.Field(HL.Table)

ActivityLevelRewardsCtrl.m_gainRewardIndex = HL.Field(HL.Number) << 0

ActivityLevelRewardsCtrl.MAX_REWARD_COUNT = HL.Field(HL.Number) << 2

ActivityLevelRewardsCtrl.m_genCellFunc = HL.Field(HL.Function)

ActivityLevelRewardsCtrl.m_listCells = HL.Field(HL.Table)

ActivityLevelRewardsCtrl.m_rewardList = HL.Field(HL.Table)


ActivityLevelRewardsCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.mainStateController:SetState(Utils.getPlayerGender() == CS.Proto.GENDER.GenMale and "Boy" or "Girl")

    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.m_receiveStageList = {}
    self.m_completeStageList = {}
    self.m_rewardList = {}
    self.m_rewardCount = Tables.ActivityLevelRewardsTable[self.m_activityId].stageList.length
    self.view.nowLevelNumberText.text = GameInstance.player.adventure.adventureLevelData.lv


    self.m_getRewardCell = UIUtils.genCachedCellFunction(self.view.rewardList)
    self.view.rewardList.onUpdateCell:RemoveAllListeners()
    self.view.rewardList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getRewardCell(obj), LuaIndex(csIndex))
    end)


    self.m_listCells = {}
    self:_RefreshRewards()


    if DeviceInfo.usingController then
        self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
            if isTopLayer and self.m_gainRewardIndex > 0 then
                self:_SetAsNaviTarget(self.m_gainRewardIndex)
                self.m_gainRewardIndex = 0
            end
        end)
        local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
            self:_SetAsNaviTarget(1)
        end)
        self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(active)
            InputManagerInst:ToggleBinding(viewBindingId, not active)
        end)
    end
end
ActivityLevelRewardsCtrl._SetAsNaviTarget = HL.Method(HL.Number) << function(self, index)
    local csIndex = CSIndex(index)
    self.m_genCellFunc = self.m_genCellFunc or UIUtils.genCachedCellFunction(self.view.rewardNode)
    self:SetNaviTarget(self.m_genCellFunc(self.view.rewardList:Get(self.view.config.REVERSE_REWARDS and CSIndex(self.m_rewardCount) - csIndex or csIndex)).focusRect)
end

ActivityLevelRewardsCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    self:_SetAsNaviTarget(1)
end

ActivityLevelRewardsCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)

    if self.view.config.REVERSE_REWARDS then
        index = self.m_rewardCount + 1 - index
    end


    local isBigReward = index == self.m_rewardCount


    if self.m_listCells[index] then
        self.m_listCells[index]:Refresh(0,function()  end)
    end
    if isBigReward then
        cell.styleState:SetState((not self.m_receiveStageList[index] and self.m_completeStageList[index]) and "HighLevelChoose" or "HighLevel")
        self.m_listCells[index] =  UIUtils.genCellCache(cell.itemSmallBlack)
    else
        cell.styleState:SetState("Normal")
        self.m_listCells[index] =  UIUtils.genCellCache(cell.reward)
    end


    local stageData = Tables.ActivityLevelRewardsTable[self.m_activityId].stageList[CSIndex(index)]
    cell.levelNumberText.text = stageData.conditions[0].progressToCompare
    cell.completeBtn.onClick:RemoveAllListeners()
    cell.completeBtn.onClick:AddListener(function()
        self:_LevelReward(index)
    end)


    local state = self.m_receiveStageList[index] and "Received" or (self.m_completeStageList[index] and "Completed" or "NotCompleted")
    cell.stateController:SetState(state)
    cell.redDot:InitRedDot("ActivityBaseMultiStageReward",state == "Completed")
    if state == "Completed" then
        table.insert(self.m_rewardList, index)
    end


    local rewardId = stageData.rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    self.m_listCells[index]:Refresh(#rewardBundles, function(innerCell, innerIndex)
        innerCell:InitItem(rewardBundles[innerIndex], function()
            innerCell:ShowTips()
        end)
        innerCell:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.controllerHintRect,
            isSideTips = true,
        })
        innerCell.view.rewardedCover.gameObject:SetActive(state == "Received")
    end)


    if DeviceInfo.usingController then
        if state == "Completed" then
            cell.focusRect:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.PressConfirmTriggerOnClick)
            cell.focusRect.onClick:RemoveAllListeners()
            cell.focusRect.onClick:AddListener(function()
                self:_LevelReward(index)
            end)
        else
            cell.focusRect:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
        end

        InputManagerInst:ToggleGroup(cell.rewards.groupId,false)
        cell.keyHintNormal.gameObject:SetActive(false)
        cell.keyHintHigh.gameObject:SetActive(false)
        cell.focusRect.onIsNaviTargetChanged = function(isTarget)
            InputManagerInst:ToggleGroup(cell.rewards.groupId,isTarget)
            cell.keyHintNormal.gameObject:SetActive(isTarget)
            cell.keyHintHigh.gameObject:SetActive(isTarget)
            cell.keyHintTrans1:SetSiblingIndex(self.MAX_REWARD_COUNT + 1);
            cell.keyHintTrans2:SetSiblingIndex(self.MAX_REWARD_COUNT + 1);
        end
    end
end

ActivityLevelRewardsCtrl._RefreshRewards = HL.Method() << function(self)
    local activityLevelRewardsCS = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local completeStageList = activityLevelRewardsCS.completeStageList
    for i = 1, completeStageList.Count do
        self.m_completeStageList[completeStageList[CSIndex(i)]] = true
    end
    local receiveStageList = activityLevelRewardsCS.receiveStageList
    for i = 1, receiveStageList.Count do
        self.m_receiveStageList[receiveStageList[CSIndex(i)]] = true
    end
    self.view.rewardList:UpdateCount(self.m_rewardCount)
end

ActivityLevelRewardsCtrl._OnLevelRewardUpdate= HL.Method(HL.Table) << function(self,args)
    self:_RefreshRewards()
end
ActivityLevelRewardsCtrl._OnActivityUpdate = HL.Method(HL.Table) << function(self,args)
    local id = unpack(args)
    if id == self.m_activityId and GameInstance.player.activitySystem:GetActivity(id) then
        local old = GameInstance.player.activitySystem:GetActivity(self.m_activityId).completeStageList.Count
        local new = #self.m_completeStageList
        if old ~= new then
            self:_RefreshRewards()
        end
    end
end

ActivityLevelRewardsCtrl._LevelReward = HL.Method(HL.Number) << function(self,level)
    activityLevelRewardsCS = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    activityLevelRewardsCS:GainReward(self.m_rewardList)
    self.m_gainRewardIndex = level
end

HL.Commit(ActivityLevelRewardsCtrl)