local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerAllScore
local PHASE_ID = PhaseId.SeasonTowerAllScore
local system = GameInstance.player.seasonTowerSystem
SeasonTowerAllScoreCtrl = HL.Class('SeasonTowerAllScoreCtrl', uiCtrl.UICtrl)






SeasonTowerAllScoreCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SEASON_TOWER_NEW] = '_OnSeasonUpdate',
    [MessageConst.ON_SEASON_TOWER_UPDATE] = '_OnSeasonUpdate',
}

SeasonTowerAllScoreCtrl.m_selectedSeasonRecord = HL.Field(HL.Any)

SeasonTowerAllScoreCtrl.m_genAllRecordListCellFunc = HL.Field(HL.Function)

SeasonTowerAllScoreCtrl.m_allRecordDungeonCacheList = HL.Field(HL.Table)


SeasonTowerAllScoreCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs(true)
end

SeasonTowerAllScoreCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_selectedSeasonRecord = arg.seasonRecord
    self.m_allRecordDungeonCacheList = {}
end

SeasonTowerAllScoreCtrl._InitUI = HL.Method() << function(self)

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)


    self.view.seasonIconNode:InitSeasonTowerSeasonInfo(self.m_selectedSeasonRecord, false)
    self.view.weekScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnUpdateAllRecordCell(go, csIndex)
    end)
    self.m_genAllRecordListCellFunc = UIUtils.genCachedCellFunction(self.view.weekScrollList)


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId})

end

SeasonTowerAllScoreCtrl._RefreshAllUIs = HL.Method(HL.Boolean) << function(self, isInit)
    local seasonId = self.m_selectedSeasonRecord.seasonId
    local weekCount = seasonId == system.currentSeasonId and system.currentWeekId or #Tables.seasonTowerTable[seasonId].weeks
    self.view.weekScrollList:UpdateCount(weekCount)
end

SeasonTowerAllScoreCtrl._OnUpdateAllRecordCell = HL.Method(HL.Any, HL.Number) << function(self, go, csIndex)

    local cell = self.m_genAllRecordListCellFunc(go)
    local seasonId = self.m_selectedSeasonRecord.seasonId
    local weekId = LuaIndex(csIndex)
    cell.numTxt.text = string.format("%02d",weekId)
    cell.nameTxt.text = Tables.seasonTowerTable[seasonId].weeks[weekId].weekShowName
    local dungeonCellCache = self.m_allRecordDungeonCacheList[go] or UIUtils.genCellCache(cell.dungeonNode)
    self.m_allRecordDungeonCacheList[go] = dungeonCellCache


    local _, weekRecord = self.m_selectedSeasonRecord.weekRecords:TryGetValue(weekId)
    if not weekRecord then
        weekRecord = {
            seasonId = seasonId,
            weekId = weekId,
            isEmpty = true,
        }
    end
    local levelIdLists = Tables.seasonTowerTable[seasonId].weeks[weekId].includeGameIdList
    dungeonCellCache:Refresh(3, function(dungeonCell, index)
        local csIndex = CSIndex(index)
        local levelId = levelIdLists[csIndex]
        local levelRecord = not weekRecord.isEmpty and weekRecord:GetLevelRecord(levelId) or nil
        dungeonCell:InitSeasonTowerDungeonRecord(levelId, levelRecord)
    end)
end

SeasonTowerAllScoreCtrl._OnSeasonUpdate = HL.Method() << function(self)
    self:_RefreshAllUIs(false)
end

HL.Commit(SeasonTowerAllScoreCtrl)
