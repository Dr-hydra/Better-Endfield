local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerScoreReview
local PHASE_ID = PhaseId.SeasonTowerScoreReview
local system = GameInstance.player.seasonTowerSystem

SeasonTowerScoreReviewCtrl = HL.Class('SeasonTowerScoreReviewCtrl', uiCtrl.UICtrl)

local reviewType = {
    self = 1,
    friend = 2,
}





SeasonTowerScoreReviewCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SEASON_TOWER_NEW] = '_OnSeasonUpdate',
    [MessageConst.ON_SEASON_TOWER_UPDATE] = '_OnSeasonUpdate',
}

SeasonTowerScoreReviewCtrl.m_historyRecord = HL.Field(HL.Any)

SeasonTowerScoreReviewCtrl.m_selectedSeasonIndex = HL.Field(HL.Number) << -1

SeasonTowerScoreReviewCtrl.m_selectedSeasonRecord = HL.Field(HL.Any)

SeasonTowerScoreReviewCtrl.m_genSeasonRecordListCellFunc = HL.Field(HL.Function)

SeasonTowerScoreReviewCtrl.m_genAllRecordListCellFunc = HL.Field(HL.Function)

SeasonTowerScoreReviewCtrl.m_dungeonRecordListCellCache = HL.Field(HL.Any)

SeasonTowerScoreReviewCtrl.m_allRecordDungeonCacheList = HL.Field(HL.Table)

SeasonTowerScoreReviewCtrl.m_reviewType = HL.Field(HL.Number) << -1


SeasonTowerScoreReviewCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs(true)
end

SeasonTowerScoreReviewCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_reviewType = arg and arg.reviewType or reviewType.self
    self.m_historyRecord = {}
    self.m_allRecordDungeonCacheList = {}
    if self.m_reviewType == reviewType.friend then
        if arg and arg.seasonHistoryRecord then
            for _, record in pairs(arg.seasonHistoryRecord) do
                table.insert(self.m_historyRecord, record)
            end
        end
    else
        table.insert(self.m_historyRecord, system.seasonRecord)
        for _, record in pairs(system.seasonHistoryRecord) do
            table.insert(self.m_historyRecord, record)
        end
    end
    self.m_selectedSeasonIndex = 1
end

SeasonTowerScoreReviewCtrl._InitUI = HL.Method() << function(self)
    self.view.tipsTxt.text = string.format(Language.LUA_SEASON_TOWER_SCORE_REVIEW_TIPS, Tables.SeasonTowerConst.maxSeasonRecord)

    if self.m_reviewType == reviewType.friend then
        self.view.seasonInfoNode.allBtn.gameObject:SetActive(false)
    else
        self.view.seasonInfoNode.allBtn.gameObject:SetActive(true)
        self.view.seasonInfoNode.allBtn.onClick:AddListener(function()
            PhaseManager:OpenPhase(PhaseId.SeasonTowerAllScore, { seasonRecord = self.m_selectedSeasonRecord })
        end)
    end

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)


    self.view.seasonScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnUpdateSeasonRecordCell(go, csIndex)
    end)
    self.view.seasonScrollList.onCenterIndexChanged:AddListener(function(oldIndex, newIndex)
        self:_OnSeasonRecordClicked(LuaIndex(newIndex), false)
    end)
    self.m_genSeasonRecordListCellFunc = UIUtils.genCachedCellFunction(self.view.seasonScrollList)
    self.m_dungeonRecordListCellCache = UIUtils.genCellCache(self.view.seasonInfoNode.dungeonNode)


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

SeasonTowerScoreReviewCtrl._UpdateData = HL.Method() << function(self)
    if self.m_reviewType == reviewType.friend then
        return
    end
    self.m_historyRecord = {}
    table.insert(self.m_historyRecord, system.seasonRecord)
    for _, record in pairs(system.seasonHistoryRecord) do
        table.insert(self.m_historyRecord, record)
    end
end

SeasonTowerScoreReviewCtrl._RefreshAllUIs = HL.Method(HL.Boolean) << function(self, isInit)
    local showSeasonRecordCount = math.min(Tables.SeasonTowerConst.maxSeasonRecord, #self.m_historyRecord)
    self.view.seasonScrollList:UpdateCount(showSeasonRecordCount)
    if isInit then
        if DeviceInfo.usingController then
            self:SetNaviTarget(self.m_genSeasonRecordListCellFunc(self.m_selectedSeasonIndex).seasonBtn)
        end
    end
end

SeasonTowerScoreReviewCtrl._OnUpdateSeasonRecordCell = HL.Method(HL.Any, HL.Number) << function(self, go, csIndex)
    local cell = self.m_genSeasonRecordListCellFunc(go)
    local luaIndex = LuaIndex(csIndex)
    local isSelected = luaIndex == self.m_selectedSeasonIndex
    self:_UpdateSeasonRecordSelectState(isSelected, luaIndex, true)
    cell.seasonBtn.onClick:AddListener(function()
        self:_OnSeasonRecordClicked(luaIndex, true)
    end)
end

SeasonTowerScoreReviewCtrl._OnSeasonRecordClicked = HL.Method(HL.Number, HL.Boolean) << function(self, luaIndex, needScroll)
    if needScroll then
        self.view.seasonScrollList:ScrollToIndex(CSIndex(luaIndex))
    end
    self:_UpdateSeasonRecordSelectState(true, luaIndex)
end

SeasonTowerScoreReviewCtrl._UpdateSeasonRecordSelectState = HL.Method(HL.Boolean, HL.Number, HL.Opt(HL.Boolean)) << function(self, isSelected, luaIndex, forceUpdate)
    local preSelectedSeasonIndex = self.m_selectedSeasonIndex
    if not forceUpdate and isSelected and preSelectedSeasonIndex == luaIndex then
        return
    end

    local seasonRecord = self.m_historyRecord[luaIndex]
    local seasonId = seasonRecord.seasonId
    local rank = SeasonTowerUtils.getRankStateName(seasonRecord.rank)
    if isSelected then
        self:_UpdateSeasonRecordSelectState(false, preSelectedSeasonIndex)
        self.m_selectedSeasonIndex = luaIndex
        self.m_selectedSeasonRecord = seasonRecord
        local bestWeekRecord = self.m_selectedSeasonRecord.bestWeekRecord
        if not bestWeekRecord then
            bestWeekRecord = {
                seasonId = seasonId,
                weekId = 1,
                isEmpty = true,
            }
        end
        self:_RefreshWeekRecord(bestWeekRecord)
    end

    local cell = self.m_genSeasonRecordListCellFunc(luaIndex)
    local infoNode = isSelected and cell.selectNode or cell.unselectNode
    cell.stateController:SetState(isSelected and "Select" or "Unselect")
    cell.newTag.gameObject:SetActive(seasonId == system.currentSeasonId)
    infoNode.seasonNameTxt.text = Tables.seasonTowerTable[seasonId].name
    local seasonTimeText = ""
    if seasonRecord.startTime > 0 and seasonRecord.endTime > 0 then
        seasonTimeText = SeasonTowerUtils.getStartEndDateString(seasonRecord.startTime, seasonRecord.endTime)
    end
    infoNode.seasonTimeTxt.text = seasonTimeText
    infoNode.rankTag:SetState(isSelected and rank or rank.."Gray")
end

SeasonTowerScoreReviewCtrl._RefreshWeekRecord = HL.Method(HL.Any) << function(self, weekRecord)
    local seasonId = weekRecord.seasonId
    local weekId = weekRecord.weekId
    self.view.seasonInfoNode.roundNameTxt.text = Tables.seasonTowerTable[seasonId].weeks[weekId].weekShowName

    local levelIdLists = Tables.seasonTowerTable[seasonId].weeks[weekId].includeGameIdList
    self.m_dungeonRecordListCellCache:Refresh(3, function(cell, luaIndex)
        local csIndex = CSIndex(luaIndex)
        local levelId = levelIdLists[csIndex]
        local levelRecord = not weekRecord.isEmpty and weekRecord:GetLevelRecord(levelId) or nil
        cell:InitSeasonTowerDungeonRecord(levelId, levelRecord)
    end)
end

SeasonTowerScoreReviewCtrl._OnSeasonUpdate = HL.Method() << function(self)

    if self.m_reviewType == reviewType.friend then
        return
    end
    self:_UpdateData()
    self:_RefreshAllUIs(false)
end


HL.Commit(SeasonTowerScoreReviewCtrl)
