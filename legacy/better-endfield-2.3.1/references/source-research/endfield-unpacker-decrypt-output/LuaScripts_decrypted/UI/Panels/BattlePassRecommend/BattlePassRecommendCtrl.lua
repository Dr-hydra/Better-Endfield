
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePassRecommend

BattlePassRecommendCtrl = HL.Class('BattlePassRecommendCtrl', uiCtrl.UICtrl)






BattlePassRecommendCtrl.s_messages = HL.StaticField(HL.Table) << {

}

BattlePassRecommendCtrl.m_arg = HL.Field(HL.Table)


BattlePassRecommendCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg == nil then
        return
    end
    self.m_arg = arg

    self.view.rewardsScrollList.gameObject:SetActive(false)
    self:_InitViews()
    self:_RenderViews()
end








BattlePassRecommendCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.btnCommonCancel.onClick:RemoveAllListeners()
    self.view.btnCommonCancel.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            if self.m_arg.onClose then
                self.m_arg.onClose()
            end
        end)
    end)

    if self.m_arg.fromBuyPlan then
        self.view.btnCommonCancel.onClick:ChangeBindingPlayerAction("common_confirm_close")
    end

    self.view.mainStateController:SetState(self.m_arg.fromBuyPlan and "One" or "Two")
    if not self.m_arg.fromBuyPlan then
        self.view.btnCommon.onClick:RemoveAllListeners()
        if self.m_arg.onConfirm ~= nil then
            self.view.btnCommon.onClick:AddListener(function()
                self:Close()
                self.m_arg.onConfirm()
            end)
        else
            self.view.btnCommon.onClick:AddListener(function()
                self:PlayAnimationOutWithCallback(function()
                    self:Close()
                	PhaseManager:GoToPhase(PhaseId.BattlePassAdvancedPlanBuy)
                end)
            end)
        end
    end
end

BattlePassRecommendCtrl._RenderViews = HL.Method() << function(self)
    local bundles = {}

    local arg = self.m_arg
    local rewardId = arg.rewardId
    local rewardBundle = arg.rewardBundle

    if string.isEmpty(rewardId) and rewardBundle == nil then
        return
    end
    if arg.rewardId then
        local hasReward, rewardData = Tables.rewardTable:TryGetValue(rewardId)
        if not hasReward then
            return
        end
        for _, bundle in pairs(rewardData.itemBundles) do
            table.insert(bundles, {
                id = bundle.id,
                count = bundle.count,
            })
        end
    else
        bundles = rewardBundle
    end
    self.view.rewardsScrollList:InitRewardItems(bundles, false, {
        onPostInitItem = function(itemCell, itemBundle)
            itemCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        end,
    })
    self.view.titleText.text = arg.desc
    local firstItemGo = self.view.rewardsScrollList.view.scrollList:Get(0)
    if firstItemGo then
        self.view.focusItemKeyHint.transform.position = firstItemGo.transform.position
        local keyHintPos = self.view.focusItemKeyHint.transform.localPosition
        keyHintPos.x = keyHintPos.x - 50
        keyHintPos.y = keyHintPos.y - 80
        self.view.focusItemKeyHint.transform.localPosition = keyHintPos
    end
end

HL.Commit(BattlePassRecommendCtrl)
