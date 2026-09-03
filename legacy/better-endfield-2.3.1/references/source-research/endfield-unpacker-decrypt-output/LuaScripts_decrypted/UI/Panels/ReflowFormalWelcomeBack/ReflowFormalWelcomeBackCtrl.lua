local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReflowFormalWelcomeBack

ReflowFormalWelcomeBackCtrl = HL.Class('ReflowFormalWelcomeBackCtrl', uiCtrl.UICtrl)






ReflowFormalWelcomeBackCtrl.s_messages = HL.StaticField(HL.Table) << {

}
ReflowFormalWelcomeBackCtrl.m_activityId = HL.Field(HL.String) << ''

ReflowFormalWelcomeBackCtrl.m_rewardBundles = HL.Field(HL.Table)

ReflowFormalWelcomeBackCtrl.m_isReceived = HL.Field(HL.Boolean) << false

ReflowFormalWelcomeBackCtrl.m_isPopup = HL.Field(HL.Boolean) << false

ReflowFormalWelcomeBackCtrl.m_genRewardCellCacheFunc = HL.Field(HL.Function)

ReflowFormalWelcomeBackCtrl.m_onCompleteRewardFunc = HL.Field(HL.Function)


ReflowFormalWelcomeBackCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs()
end

ReflowFormalWelcomeBackCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.m_onCompleteRewardFunc = arg.completeRewardFunc or function()
        self:PlayAnimationOutAndClose()
    end
    self.m_isPopup = arg.isPopup or false
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local _, table = Tables.activityReflowTable:TryGetValue(self.m_activityId)
    self.m_rewardBundles = UIUtils.getRewardItems(table.reflowCfg.oneTimeRewardId)
    self.m_isReceived = activity.oneTimeRewardReceived
end

ReflowFormalWelcomeBackCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.gameObject:SetActive(not self.m_isPopup)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.receiveBtn.onClick:AddListener(function()
        GameInstance.player.activitySystem:SendGainReflowOneTimeReward(self.m_activityId)
    end)

    self.view.rewardsScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnUpdateRewardCell(go, csIndex)
    end)
    self.m_genRewardCellCacheFunc = UIUtils.genCachedCellFunction(self.view.rewardsScrollList)
end

ReflowFormalWelcomeBackCtrl._RefreshAllUIs = HL.Method() << function(self)

    self.view.rewardsScrollList.gameObject:SetActive(false)
    self.view.rewardsScrollList:UpdateCount(#self.m_rewardBundles, true)
    self.view.stateController:SetState(self.m_isReceived and "Received" or "Normal")
    self.view.receiveBtn.interactable = not self.m_isReceived
    if DeviceInfo.usingController then
        self.view.receiveKeyHint.gameObject:SetActive(not self.m_isReceived)
    end
end

ReflowFormalWelcomeBackCtrl.OnShow = HL.Override() << function(self)
    self.view.focusItemKeyHint.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder.gameObject:SetActive(false)
end

ReflowFormalWelcomeBackCtrl._OnUpdateRewardCell = HL.Method(HL.Any, HL.Number) << function(self, go, csIndex)
    local cell = self.m_genRewardCellCacheFunc(go)
    local index = LuaIndex(csIndex)
    cell.view.rewardedCover.gameObject:SetActive(self.m_isReceived)
    local reward = {
        id = self.m_rewardBundles[index].id,
        count =  self.m_rewardBundles[index].count,
        forceHidePotentialStar = true,
    }
    cell:InitItem(reward, function()
        cell:ShowTips()
    end)
    cell:SetExtraInfo({
        tipsPosTransform = cell.view.content,
        isSideTips = true,
    })
end

ReflowFormalWelcomeBackCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder.gameObject:SetActive(true)
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
        local firstItemGo = self.m_genRewardCellCacheFunc(1)
        if firstItemGo then
            self.view.focusItemKeyHint.gameObject:SetActive(true)
            self.view.focusItemKeyHint.transform.position = firstItemGo.transform.position
            local keyHintPos = self.view.focusItemKeyHint.transform.localPosition
            keyHintPos.x = keyHintPos.x - 50
            keyHintPos.y = keyHintPos.y - 90
            self.view.focusItemKeyHint.transform.localPosition = keyHintPos
        end
    end
end

HL.Commit(ReflowFormalWelcomeBackCtrl)
