local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SeasonTowerSeasonInfo = HL.Class('SeasonTowerSeasonInfo', UIWidgetBase)

SeasonTowerSeasonInfo.m_countdownCor = HL.Field(HL.Any)


SeasonTowerSeasonInfo._OnFirstTimeInit = HL.Override() << function(self)

end

SeasonTowerSeasonInfo.InitSeasonTowerSeasonInfo = HL.Method(CS.Beyond.Gameplay.SeasonTowerSystem.SeasonTowerSeasonRecord, HL.Boolean)
    << function(self, seasonRecord, isFinalWeek)
    self:_FirstTimeInit()
    self:_StopCountdownCor()

    self.view.seasonNameTxt.text = Tables.seasonTowerTable[seasonRecord.seasonId].name
    if isFinalWeek then
        self.view.seasonTimeTxt.text = string.format(Language.LUA_SEASON_TOWER_REMAIN_SEASON_TIME, SeasonTowerUtils.getRemainTimeText())
        self.m_countdownCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(1)
                self.view.seasonTimeTxt.text = string.format(Language.LUA_SEASON_TOWER_REMAIN_SEASON_TIME, SeasonTowerUtils.getRemainTimeText())
            end
        end)
    else
        self.view.seasonTimeTxt.text = SeasonTowerUtils.getStartEndDateString(seasonRecord.startTime, seasonRecord.endTime)
    end
    self.view.rankTag:SetState(SeasonTowerUtils.getRankStateName(seasonRecord.rank))
end

SeasonTowerSeasonInfo._StopCountdownCor = HL.Method() << function(self)
    if self.m_countdownCor then
        self:_ClearCoroutine(self.m_countdownCor)
        self.m_countdownCor = nil
    end
end

HL.Commit(SeasonTowerSeasonInfo)
return SeasonTowerSeasonInfo

