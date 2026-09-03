
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivitySeasonTower

ActivitySeasonTowerCtrl = HL.Class('ActivitySeasonTowerCtrl', uiCtrl.UICtrl)






ActivitySeasonTowerCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SEASON_TOWER_NEW] = '_OnRefreshWeek',

}


ActivitySeasonTowerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.activityCommonInfo:InitActivityCommonInfo(arg)
    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("SeasonTowerMain")
    local gender = Utils.getPlayerGender()
    if gender == CS.Proto.GENDER.GenMale then
        self.view.bgStateController:SetState("AdministratorA")
    else
        self.view.bgStateController:SetState("AdministratorB")
    end
    self.view.newSeasonRedDot:InitRedDot("SeasonTowerNew")
end


ActivitySeasonTowerCtrl._OnRefreshWeek = HL.Method() << function(self)

    local seasonTowerSystem = GameInstance.player.seasonTowerSystem
    if seasonTowerSystem.currentSeasonId <= 0 or seasonTowerSystem.currentWeekId <= 0 then
        self.view.regionTxt.text = Language.LUA_SEASON_TOWER_ACTIVITY_DEFAULT
        self.view.rankTag:SetState(SeasonTowerUtils.getRankStateName(0))
        return
    end
    local seasonCfg = Tables.seasonTowerTable[seasonTowerSystem.currentSeasonId]

    self.view.regionTxt.text = string.format(Language.LUA_SEASON_TOWER_ACTIVITY_TITLE, seasonCfg.name)
    self.view.activityCommonInfo.view.infoNode.countDownWidget:InitCountDownText(seasonTowerSystem.seasonRecord.endTime, nil, function(leftTime)
        return string.format(Language.LUA_SEASON_TOWER_REMAIN_SEASON_TIME, UIUtils.getLeftTime(leftTime))
    end)
    self.view.rankTag:SetState(SeasonTowerUtils.getRankStateName(seasonTowerSystem.seasonRecord.rank))
    if SeasonTowerUtils.isNewSeason() then
        self.view.redDotText.text = Language.LUA_SEASON_TOWER_NEW_SEASON
    else
        self.view.redDotText.text = Language.LUA_SEASON_TOWER_NEW_WEEK
    end
    self.view.newSeasonRedDot:UpdateState()
end

ActivitySeasonTowerCtrl.OnShow = HL.Override() << function(self)
    self:_OnRefreshWeek()
end










HL.Commit(ActivitySeasonTowerCtrl)
