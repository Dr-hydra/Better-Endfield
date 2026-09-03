local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

AdventureRewardShortInfoCell = HL.Class('AdventureRewardShortInfoCell', UIWidgetBase)

AdventureRewardShortInfoCell.m_rewardInfo = HL.Field(HL.Table)

AdventureRewardShortInfoCell.m_luaIndex = HL.Field(HL.Number) << -1

AdventureRewardShortInfoCell.m_onClickFunc = HL.Field(HL.Function)


AdventureRewardShortInfoCell._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_ADVENTURE_REWARD_RECEIVE, function()
        self:_UpdateInfo()
    end)

    self.view.button.onClick:AddListener(function()
        if self.m_onClickFunc then
            self.m_onClickFunc(self.m_luaIndex)
        end
    end)
end

AdventureRewardShortInfoCell.InitAdventureRewardShortInfoCell = HL.Method(HL.Table, HL.Number, HL.Function)
        << function(self, info, luaIndex, onClickFunction)
    self:_FirstTimeInit()

    self.m_rewardInfo = info
    self.m_luaIndex = luaIndex
    self.m_onClickFunc = onClickFunction

    self.view.levelTxt.text = info.level
    self.view.shadowLevelTxt.text = info.level

    self:_UpdateInfo()
end

AdventureRewardShortInfoCell.SampleCellEffect = HL.Method(HL.Number) << function(self, effectVal)
    self.view.animationWrapper:SampleClipAtPercent("adv_reward_level_view_effect", effectVal)
end

AdventureRewardShortInfoCell._UpdateInfo = HL.Method() << function(self)
    self.view.stateCtrl:SetState(self.m_rewardInfo.hideReward and "CantReachState" or (self.m_rewardInfo.gainReward and "ReachState" or "NotReachState"))
end

HL.Commit(AdventureRewardShortInfoCell)
return AdventureRewardShortInfoCell

