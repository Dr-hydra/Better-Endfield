local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DungeonCommonSelectionGroupCell = HL.Class('DungeonCommonSelectionGroupCell', UIWidgetBase)

DungeonCommonSelectionGroupCell.m_dungeonIds = HL.Field(HL.Table)

DungeonCommonSelectionGroupCell.m_clickFunc = HL.Field(HL.Function)

DungeonCommonSelectionGroupCell.m_cellCache = HL.Field(HL.Forward("UIListCache"))


DungeonCommonSelectionGroupCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_cellCache = UIUtils.genCellCache(self.view.subTrainCell)
end

DungeonCommonSelectionGroupCell.InitDungeonCommonSelectionGroupCell = HL.Method(HL.Table, HL.Function)
    << function(self, dungeonIds, clickFunc)
    self.m_dungeonIds = dungeonIds
    self.m_clickFunc = clickFunc

    self:_FirstTimeInit()

    self.m_cellCache:Refresh(#self.m_dungeonIds, function(cell, luaIndex)
        cell:InitDungeonCommonSelectionGroupSubCell(self.m_dungeonIds[luaIndex], self.m_clickFunc)
        cell.gameObject.name = self.m_dungeonIds[luaIndex]
    end)

    local completeNum = self:_GetCompleteNum()
    local maxNum = #dungeonIds
    self.view.numTxt.text = string.format(Language.LUA_DUNGEONCOMMONSELECTIONGROUPCELL_NUMBER, completeNum, maxNum)

    local rootDungeonId = self.m_dungeonIds[1]
    local dungeonCfg = Tables.dungeonTable[rootDungeonId]
    self.view.nameTxt.text = dungeonCfg.tabGroupName

    self.view.maxNode.gameObject:SetActive(completeNum >= maxNum)

    self.view.redDot:InitRedDot("DungeonReadNormal", dungeonIds, nil, self:GetUICtrl().view.redDotScrollRect)
end

DungeonCommonSelectionGroupCell.TryGetSubCell = HL.Method(HL.String).Return(HL.Any) << function(self, dungeonId)
    local found = nil
    for i, v in ipairs(self.m_dungeonIds) do
        if v == dungeonId then
            found = self.m_cellCache:Get(i)
            break
        end
    end
    return found
end

DungeonCommonSelectionGroupCell._GetCompleteNum = HL.Method().Return(HL.Number) << function(self)
    local cnt = 0
    for _, dungeonId in ipairs(self.m_dungeonIds) do
        local isComplete = DungeonUtils.isDungeonPassed(dungeonId)
        if isComplete then
            cnt = cnt+1
        end
    end
    return cnt
end

DungeonCommonSelectionGroupCell.SetToggle = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.mainTog.isOn = isOn
end

HL.Commit(DungeonCommonSelectionGroupCell)
return DungeonCommonSelectionGroupCell

