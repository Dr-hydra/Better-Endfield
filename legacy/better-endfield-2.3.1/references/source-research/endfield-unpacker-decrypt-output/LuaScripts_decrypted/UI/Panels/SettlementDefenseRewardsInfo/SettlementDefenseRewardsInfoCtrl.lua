local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefenseRewardsInfo
local PHASE_ID = PhaseId.SettlementDefenseRewardsInfo
SettlementDefenseRewardsInfoCtrl = HL.Class('SettlementDefenseRewardsInfoCtrl', uiCtrl.UICtrl)

SettlementDefenseRewardsInfoCtrl.m_isCompleted = HL.Field(HL.Boolean) << false

SettlementDefenseRewardsInfoCtrl.m_firstRewardCells = HL.Field(HL.Forward('UIListCache'))





SettlementDefenseRewardsInfoCtrl.s_messages = HL.StaticField(HL.Table) << {

}


SettlementDefenseRewardsInfoCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.fullScreenBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.m_firstRewardCells = UIUtils.genCellCache(self.view.firstRewardCell)

    self.m_isCompleted = args.isCompleted
    self:_RefreshFirstRewardItemList(args.firstRewardId)
    self:_RefreshBuildingRewardContent(args.bandwidthReward, args.battleLimitReward)
end

SettlementDefenseRewardsInfoCtrl._RefreshFirstRewardItemList = HL.Method(HL.String) << function(self, rewardId)
    if string.isEmpty(rewardId) then
        return
    end

    local rewardSuccess, rewardData = Tables.rewardTable:TryGetValue(rewardId)
    if not rewardSuccess then
        return
    end

    local rewardItems = rewardData.itemBundles
    local rewardItemDataList = UIUtils.convertRewardItemBundlesToDataList(rewardItems, false)

    self.m_firstRewardCells:Refresh(rewardItems.Count, function(cell, luaIndex)
        local itemData = rewardItemDataList[luaIndex]
        cell.item:InitItem({
            id = itemData.id,
            count = itemData.count,
        }, true)
        cell.gameObject.name = itemData.id
        cell.getNode.gameObject:SetActive(self.m_isCompleted)
    end)
end

SettlementDefenseRewardsInfoCtrl._RefreshBuildingRewardContent = HL.Method(HL.Number, HL.Number) << function(self, bandwidth, battleLimit)
    self.view.bandwidthNode.rewardBandwidthCount.text = string.format("+%d", bandwidth)
    self.view.bandwidthNode.completeImage.gameObject:SetActive(self.m_isCompleted)
    self.view.bandwidthNode.completeShadeImage.gameObject:SetActive(self.m_isCompleted)

    self.view.battleNode.rewardBattleCount.text = string.format("+%d", battleLimit)
    self.view.battleNode.completeImage.gameObject:SetActive(self.m_isCompleted)
    self.view.battleNode.completeShadeImage.gameObject:SetActive(self.m_isCompleted)
end

HL.Commit(SettlementDefenseRewardsInfoCtrl)
