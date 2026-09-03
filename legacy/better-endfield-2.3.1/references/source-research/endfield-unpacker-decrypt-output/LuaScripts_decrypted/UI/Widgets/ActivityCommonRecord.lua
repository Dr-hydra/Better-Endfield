local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ActivityCommonRecord = HL.Class('ActivityCommonRecord', UIWidgetBase)

ActivityCommonRecord.m_activityId = HL.Field(HL.String) << ""

ActivityCommonRecord.m_rankRelatedId = HL.Field(HL.String) << ""


ActivityCommonRecord._OnFirstTimeInit = HL.Override() << function(self)
    self.view.rankingBtn.onClick:AddListener(function()
        self:_OnClickRankingBtn()
    end)
end

ActivityCommonRecord.InitActivityCommonRecord = HL.Method(HL.String, HL.String) << function(self, activityId, rankRelatedId)
    self:_FirstTimeInit()

    self.m_activityId = activityId
    self.m_rankRelatedId = rankRelatedId

    local hasCfg, rankInfoCfg = Tables.activityRankInfoTable:TryGetValue(rankRelatedId)
    if hasCfg then
        self.view.stateController:SetState(rankInfoCfg.rankValueStyleType:ToString())
    end

    local succ, rankValue = GameInstance.player.activitySystem:TryGetActivityOwnRankValue(activityId, rankRelatedId)
    local floorRankValue = math.floor(rankValue / 1000)
    self.view.timeTxt.text = succ and UIUtils.getLeftTimeToSecond(floorRankValue) or "--:--"
end

ActivityCommonRecord._OnClickRankingBtn = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.ActivityRanking, {
        activityId = self.m_activityId,
        rankRelatedId = self.m_rankRelatedId,
    })
end

HL.Commit(ActivityCommonRecord)
return ActivityCommonRecord

