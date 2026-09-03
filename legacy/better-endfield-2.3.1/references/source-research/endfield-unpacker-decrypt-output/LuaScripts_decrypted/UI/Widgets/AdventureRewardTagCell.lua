local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

AdventureRewardTagCell = HL.Class('AdventureRewardTagCell', UIWidgetBase)

AdventureRewardTagCell.m_rewardInfo = HL.Field(HL.Table)

AdventureRewardTagCell.m_onClickFunc = HL.Field(HL.Function)


AdventureRewardTagCell._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_ADVENTURE_REWARD_RECEIVE, function()
        self:_UpdateInfo()
    end)






end

AdventureRewardTagCell.InitAdventureRewardTagCell = HL.Method(HL.Table, HL.Function) << function(self, info, onClickFunc)
    self:_FirstTimeInit()

    self.m_rewardInfo = info
    self.m_onClickFunc = onClickFunc

    self:_UpdateInfo()
end

AdventureRewardTagCell._UpdateInfo = HL.Method() << function(self)
    local adventure = GameInstance.player.adventure
    local reach = adventure.adventureLevelData.lv >= self.m_rewardInfo.level

    self.view.reach.gameObject:SetActiveIfNecessary(reach)
    self.view.unreached.gameObject:SetActiveIfNecessary(not reach)
end

HL.Commit(AdventureRewardTagCell)
return AdventureRewardTagCell

