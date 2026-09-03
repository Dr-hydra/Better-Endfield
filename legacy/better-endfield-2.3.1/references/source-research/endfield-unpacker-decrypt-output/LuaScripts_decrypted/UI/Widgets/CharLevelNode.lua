local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharLevelNode = HL.Class('CharLevelNode', UIWidgetBase)

CharLevelNode.m_expCells = HL.Field(HL.Forward("UIListCache"))


CharLevelNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_expCells = UIUtils.genCellCache(self.view.expCell)


end

CharLevelNode.InitCharLevelNode = HL.Method(HL.Number) << function(self, instId)
    self:_FirstTimeInit()

    local curExp, levelUpExp, curLevel, maxLevel, expCards = CharInfoUtils.getCharExpInfo(instId)

    local fillCount = math.floor((curExp / levelUpExp) * self.view.config.CELL_COUNT)
    self.m_expCells:Refresh(self.view.config.CELL_COUNT, function(cell, index)
        cell.image.color = index < fillCount and self.view.config.CELL_COLOR_FILL or self.view.config.CELL_COLOR_EMPTY
    end)

    self.view.charEliteMarker:InitCharEliteMarker(instId)
    self.view.maxLevelText.text = maxLevel
    self.view.levelText.text = curLevel
end

HL.Commit(CharLevelNode)
return CharLevelNode

