
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityChallengeDungeon

ActivityChallengeDungeonCtrl = HL.Class('ActivityChallengeDungeonCtrl', uiCtrl.UICtrl)






ActivityChallengeDungeonCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ActivityChallengeDungeonCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityChallengeDungeonCtrl.m_info = HL.Field(HL.Table)

ActivityChallengeDungeonCtrl.m_bgGo = HL.Field(HL.Any)




ActivityChallengeDungeonCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.activityCommonInfo:InitActivityCommonInfo(arg)
    self:_InitData(arg)
    self:_UpdateData()
    self:_RefreshAllUI()

    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityNormalChallengeGotoDetailBtn", self.m_info.activityId)
end



ActivityChallengeDungeonCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.m_info = {
        activityId = arg.activityId,
        perfectCompleteSeriesCount = 0,
        totalSeriesCount = 0,
        seriesBg = "",
    }
end

ActivityChallengeDungeonCtrl._UpdateData = HL.Method() << function(self)


    local activitySystem = GameInstance.player.activitySystem
    local dungeonManager = GameInstance.dungeonManager
    local activityId = self.m_info.activityId

    local activityData = activitySystem:GetActivity(activityId)
    local _, activitySeriesCfg = Tables.activityGameEntranceSeriesTable:TryGetValue(activityId)

    self.m_info.perfectCompleteSeriesCount = 0
    self.m_info.totalSeriesCount = 0

    local seriesCfgList = {}
    for seriesId, seriesCfg in pairs(activitySeriesCfg.seriesMap) do
        local _, activityGameCfg = Tables.activityGameEntranceGameTable:TryGetValue(seriesId)
        local canShow = false

        local hasData, seriesData = activityData.seriesDataMap:TryGetValue(seriesId)
        if seriesData then
            canShow = seriesData.OpenTime <= DateTimeUtils.GetCurrentTimestampBySeconds()
        end
        if canShow then
            self.m_info.totalSeriesCount = self.m_info.totalSeriesCount + 1
            local allPerfectComplete = true
            for _, gameSingleCfg in pairs(activityGameCfg.gameList) do
                local isPerfectComplete = DungeonUtils.isDungeonPerfectComplete(gameSingleCfg.gameId)
                if not isPerfectComplete then
                    allPerfectComplete = false
                    break
                end
            end
            if allPerfectComplete then
                self.m_info.perfectCompleteSeriesCount = self.m_info.perfectCompleteSeriesCount + 1
            end
            table.insert(seriesCfgList, {seriesId = seriesId, seriesCfg = seriesCfg, sortId = seriesCfg.sortId})
        end
    end

    table.sort(seriesCfgList, function(a, b)
        return a.sortId > b.sortId
    end)
    for _, seriesInfo in pairs(seriesCfgList) do

        local isUnlock = activitySystem:IsGameEntranceSeriesUnlock(seriesInfo.seriesId)
        if isUnlock then
            local seriesCfg = seriesInfo.seriesCfg
            self.m_info.seriesBg = seriesCfg.bgPrefab
            break
        end
    end
    local seriesName = activitySeriesCfg.seriesMap[seriesCfgList[1].seriesId].name
    self.view.regionTxt.text = string.format(Language.LUA_ACTIVITY_CHALLENGE_DUNGEON_SERIES_TEXT, seriesName)
end



ActivityChallengeDungeonCtrl._RefreshAllUI = HL.Method() << function(self)
    self.view.completeProgressNode.gameObject:SetActive(ActivityUtils.hasIntroMissionAndComplete(self.m_activityId))
    self.view.completeNumTxt.text = self.m_info.perfectCompleteSeriesCount
    self.view.totalNumTxt.text = '/' .. self.m_info.totalSeriesCount

    if self.m_bgGo then
        CSUtils.ClearUIComponents(self.m_bgGo)
        GameObject.DestroyImmediate(self.m_bgGo)
    end
    local path = string.format(UIConst.UI_ACTIVITY_CHALLENGE_DUNGEON_BG_PREFAB_PATH, self.m_info.seriesBg)
    local prefab = self:LoadGameObject(path)
    self.m_bgGo = CSUtils.CreateObject(prefab, self.view.bgNode)
    self.m_bgGo.gameObject.name = "Bg"
end





HL.Commit(ActivityChallengeDungeonCtrl)
