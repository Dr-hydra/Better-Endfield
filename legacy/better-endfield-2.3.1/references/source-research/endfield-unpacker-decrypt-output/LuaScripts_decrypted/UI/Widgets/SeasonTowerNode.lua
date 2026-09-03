local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SeasonTowerNode = HL.Class('SeasonTowerNode', UIWidgetBase)

SeasonTowerNode.m_roleId = HL.Field(HL.Number) << 0


SeasonTowerNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.seasonTowerBtn.onClick:AddListener(function()
        if self.m_roleId ~= 0 then
            if self.m_roleId == GameInstance.player.roleId then
                PhaseManager:OpenPhase(PhaseId.SeasonTowerScoreReview)
                return
            end
            local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_roleId)
            if success and FriendUtils.hasSeasonTowerHistoryRecord(playerInfo) then
                PhaseManager:OpenPhase(PhaseId.SeasonTowerScoreReview, {
                    reviewType = 2,
                    seasonHistoryRecord = playerInfo.seasonTowerHistoryRecord,
                })
            end
        end
    end)
end

SeasonTowerNode.InitSeasonTowerNode = HL.Method(HL.Number) << function(self, roleId)
    self:_FirstTimeInit()
    self.m_roleId = roleId

    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    local displayRecord = success and FriendUtils.getSeasonTowerDisplayRecord(playerInfo)
    local rank = displayRecord and displayRecord.rank or 0
    if displayRecord and FriendUtils.SEASON_TOWER_RANK_NAMES[rank] then
        self.view.main.gameObject:SetActiveIfNecessary(true)
        self.view.seasonTxt.text = Tables.seasonTowerTable:GetValue(displayRecord.seasonId).name
        self.view.seasonTowerTag:SetState(FriendUtils.SEASON_TOWER_RANK_NAMES[rank])
        local res, rankData = Tables.seasonTowerRankTable:TryGetValue(rank)
        self.view.tagTxt.text = res and rankData.rankName or FriendUtils.SEASON_TOWER_RANK_NAMES[rank]
    else
        self.view.main.gameObject:SetActiveIfNecessary(false)
    end
end

HL.Commit(SeasonTowerNode)
return SeasonTowerNode

