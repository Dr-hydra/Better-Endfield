local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureRacingDungeon

AdventureRacingDungeonCtrl = HL.Class('AdventureRacingDungeonCtrl', uiCtrl.UICtrl)





AdventureRacingDungeonCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_WEEK_RAID_BATTLE_PASS_UPDATE] = "_RefreshUIScoreTxt",
}


AdventureRacingDungeonCtrl.m_genRewardCells = HL.Field(HL.Forward("UIListCache"))

AdventureRacingDungeonCtrl.m_rewardInfos = HL.Field(HL.Table)



AdventureRacingDungeonCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_genRewardCells = UIUtils.genCellCache(self.view.rewardCell)

    self.view.gotoBtn.onClick:RemoveAllListeners()
    self.view.gotoBtn.onClick:AddListener(AdventureRacingDungeonCtrl.GoToRacingDungeonEntry)
    self.view.gotoRedDot:InitRedDot("AdventureBookTabWeekRaid")

    self:_Init()
end

AdventureRacingDungeonCtrl.OnShow = HL.Override() << function(self)
    AudioAdapter.PostEvent("Au_UI_Menu_WeekDungeonPanelsmall_Open")
end


AdventureRacingDungeonCtrl._Init = HL.Method() << function(self)


    local dungeonId
    local curScore = GameInstance.player.weekRaidSystem.battlePassScore
    local maxScore = GameInstance.player.weekRaidSystem.battlePassMaxScore


    local list = Tables.globalConst.adventureRacingDugeonRewards
    self.m_rewardInfos = {}
    for _, rewardId in pairs(list) do
        table.insert(self.m_rewardInfos, { id = rewardId })
    end

    self.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)

    self:_RefreshUITimeTxt()
    self:_RefreshUIScoreTxt()
    self:_InitUIRewardList()
end

AdventureRacingDungeonCtrl._RefreshUITimeTxt = HL.Method() << function(self)
    local targetTime = Utils.getNextWeeklyServerRefreshTime()
    self.view.timeTxt:InitCountDownText(
        targetTime,
        function()
            self:_RefreshUITimeTxt()
        end,
        function(leftTime)
            return string.format(Language.LUA_ADVENTURE_RACING_DUNGEON_COUNT_DOWN_FORMAT, UIUtils.getLeftTime(leftTime))
        end
    )
end

AdventureRacingDungeonCtrl._RefreshUIScoreTxt = HL.Method() << function(self)
    local curScore = GameInstance.player.weekRaidSystem.battlePassScore
    local maxScore = GameInstance.player.weekRaidSystem.battlePassMaxScore
    self.view.curScoreTxt.text = curScore
    self.view.maxScoreTxt.text = string.format(Language.LUA_ADVENTURE_RACING_DUNGEON_MAX_SCORE_FORMAT, maxScore)
end

AdventureRacingDungeonCtrl._InitUIRewardList = HL.Method() << function(self)
    local count = #self.m_rewardInfos
    self.m_genRewardCells:Refresh(count, function(cell, luaIndex)
        self:_RefreshUIRewardList(cell, luaIndex)
    end)
end

AdventureRacingDungeonCtrl._RefreshUIRewardList = HL.Method(HL.Userdata, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_rewardInfos[luaIndex]
    cell:InitItem(info, function()
        UIUtils.showItemSideTips(cell)
    end)
    cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    cell.view.rewardedCover.gameObject:SetActive(false)
end

AdventureRacingDungeonCtrl.GoToRacingDungeonEntry = HL.StaticMethod() << function()
    PhaseManager:OpenPhase(PhaseId.DungeonWeeklyRaid)
end



HL.Commit(AdventureRacingDungeonCtrl)
