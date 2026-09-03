local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureTraining

AdventureTrainingCtrl = HL.Class('AdventureTrainingCtrl', uiCtrl.UICtrl)






AdventureTrainingCtrl.s_messages = HL.StaticField(HL.Table) << {


    [MessageConst.ON_DUNGEON_DIRECTLY_GET_REWARD] = 'OnDirectlyGetReward',
}

AdventureTrainingCtrl.m_genLevelCells = HL.Field(HL.Forward("UIListCache"))

AdventureTrainingCtrl.m_levelInfos = HL.Field(HL.Table)

AdventureTrainingCtrl.m_tableDataList = HL.Field(HL.Table)


AdventureTrainingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData()
    self:_InitUI()
end

AdventureTrainingCtrl.OnShow = HL.Override() << function(self)
    local firstCell = self.m_genLevelCells:Get(1)
    if firstCell then
        self:SetNaviTarget(firstCell.view.naviDecorator)
    end
end

AdventureTrainingCtrl._InitData = HL.Method() << function(self)

    self.m_tableDataList = {}
    for _, v in pairs(Tables.dungeonSeriesTable) do
        if v.dungeonCategory == GEnums.DungeonCategoryType.Train then
            table.insert(self.m_tableDataList, v)
        end
    end
    table.sort(self.m_tableDataList, Utils.genSortFunction({ "sortId" }, true))
end

AdventureTrainingCtrl._InitUI = HL.Method() << function(self)
    self.m_genLevelCells = UIUtils.genCellCache(self.view.levelCell)
    self:_RefreshAllUI()
end

AdventureTrainingCtrl._RefreshAllUI = HL.Method() << function(self)
    self.m_genLevelCells:Refresh(#self.m_tableDataList, function(cell, luaIndex)
        self:_OnRefreshLevelCell(cell, luaIndex)
    end)


    if self:IsHide() then
        return
    end

    local firstCell = self.m_genLevelCells:Get(1)
    if firstCell then
        self:SetNaviTarget(firstCell.view.naviDecorator)
    end
end

AdventureTrainingCtrl._OnRefreshLevelCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    if luaIndex > #self.m_tableDataList then
        cell:InitTrainingEntryTab(true)
    else
        cell:InitTrainingEntryTab(false, self.m_tableDataList[luaIndex].id, luaIndex)
    end
end

AdventureTrainingCtrl.OnDirectlyGetReward = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshAllUI()
end

HL.Commit(AdventureTrainingCtrl)
