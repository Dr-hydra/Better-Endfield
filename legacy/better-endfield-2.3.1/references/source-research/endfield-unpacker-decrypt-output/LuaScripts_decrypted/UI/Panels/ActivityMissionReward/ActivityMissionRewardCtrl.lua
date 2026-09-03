
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityMissionReward

ActivityMissionRewardCtrl = HL.Class('ActivityMissionRewardCtrl', uiCtrl.UICtrl)

ActivityMissionRewardCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnActivityUpdate',
    [MessageConst.ON_LEVEL_REWARD_UPDATE] = '_OnActivityUpdate',
}

ActivityMissionRewardCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityMissionRewardCtrl.m_getCell = HL.Field(HL.Function)

ActivityMissionRewardCtrl.m_completeStageList = HL.Field(HL.Table)

ActivityMissionRewardCtrl.m_receiveStageList = HL.Field(HL.Table)

ActivityMissionRewardCtrl.m_listCells = HL.Field(HL.Table)

ActivityMissionRewardCtrl.m_gainRewardIndex = HL.Field(HL.Number) << 0


ActivityMissionRewardCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)

    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    local _,missionDesc = Utils.getCurMissionIdAndDesc("activity")
    self.view.pcMissionProgressText.text = missionDesc
    self.view.mobileMissionProgressText.text = missionDesc
    self.m_receiveStageList = {}
    self.m_completeStageList = {}


    self.m_getCell = UIUtils.genCachedCellFunction(self.view.rewardNode)
    self.view.rewardList.onUpdateCell:RemoveAllListeners()
    self.view.rewardList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
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

ActivityMissionRewardCtrl._SetAsNaviTarget = HL.Method(HL.Number) << function(self, index)
    self:SetNaviTarget(self.m_getCell(self.view.rewardList:Get(CSIndex(index))).inputBindingGroupNaviDecorator)
end

ActivityMissionRewardCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    self:_SetAsNaviTarget(1)
end

ActivityMissionRewardCtrl.GetAllCanReceiveStageIds = HL.Method().Return(HL.Table) << function(self)
    local stageIds = {}
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    for index = 1, Tables.ActivityLevelRewardsTable[self.m_activityId].stageList.length do
        if self.m_completeStageList[index] and not self.m_receiveStageList[index] then
            table.insert(stageIds, index)
        end
    end
    return stageIds
end

ActivityMissionRewardCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    self.m_listCells[index] = self.m_listCells[index] or UIUtils.genCellCache(cell.reward)


    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local stageData = Tables.ActivityLevelRewardsTable[self.m_activityId].stageList[CSIndex(index)]
    local condition = stageData.conditions[0]
    cell.titleText.text = condition.desc
    cell.descText.text = condition.tips


    cell.completeBtn.onClick:RemoveAllListeners()
    cell.completeBtn.onClick:AddListener(function()
        if DeviceInfo.usingController then
            self.m_gainRewardIndex = index
        end
        activity:GainReward(self:GetAllCanReceiveStageIds())
    end)
    cell.notCompleteButton.onClick:RemoveAllListeners()
    cell.notCompleteButton.onClick:AddListener(function()
        Utils.jumpToSystem(condition.jumpId)
    end)


    local state = self.m_receiveStageList[index] and "Received" or (self.m_completeStageList[index] and "Completed" or "NotCompleted")
    cell.stateController:SetState(state)
    cell.redDot:InitRedDot("ActivityBaseMultiStageReward",state == "Completed")


    local reward = cell.reward
    local rewardId = stageData.rewardId
    local item = UIUtils.getRewardItems(rewardId)[1]
    reward.gameObject:SetActive(true)
    reward:InitItem(item, function()
        reward:ShowTips()
    end)
    cell.countTxt.text = item.count
    reward:SetExtraInfo({
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
        tipsPosTransform = self.view.controllerHintRect,
        isSideTips = true,
    })
    reward.view.rewardedCover.gameObject:SetActive(state == "Received")

    if DeviceInfo.usingController then
        cell.keyHint.gameObject:SetActive(false)
        cell.inputBindingGroupNaviDecorator.onIsNaviTargetChanged = function(isTarget)
            cell.keyHint.gameObject:SetActive(isTarget)
        end
    end
end

ActivityMissionRewardCtrl._RefreshRewards = HL.Method() << function(self)
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local completeStageList = activity.completeStageList
    for i = 1, completeStageList.Count do
        self.m_completeStageList[completeStageList[CSIndex(i)]] = true
    end
    local receiveStageList = activity.receiveStageList
    for i = 1, receiveStageList.Count do
        self.m_receiveStageList[receiveStageList[CSIndex(i)]] = true
    end
    self.view.rewardList:UpdateCount(Tables.ActivityLevelRewardsTable[self.m_activityId].stageList.length)
end

ActivityMissionRewardCtrl._OnActivityUpdate = HL.Method(HL.Table) << function(self,args)
    local id = unpack(args)
    if id == self.m_activityId and GameInstance.player.activitySystem:GetActivity(id) then
        self:_RefreshRewards()
    end
end

HL.Commit(ActivityMissionRewardCtrl)
