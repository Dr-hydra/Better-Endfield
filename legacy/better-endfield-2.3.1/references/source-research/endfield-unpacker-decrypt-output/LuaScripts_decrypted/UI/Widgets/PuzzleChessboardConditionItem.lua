local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PuzzleChessboardConditionItem = HL.Class('PuzzleChessboardConditionItem', UIWidgetBase)

PuzzleChessboardConditionItem.m_group1ConditionCells = HL.Field(HL.Forward("UIListCache"))

PuzzleChessboardConditionItem.m_group2ConditionCells = HL.Field(HL.Forward("UIListCache"))


PuzzleChessboardConditionItem._OnFirstTimeInit = HL.Override() << function(self)
    self.m_group1ConditionCells = UIUtils.genCellCache(self.view.rectangleNode.colorGroup1.conditionCell)
    self.m_group2ConditionCells = UIUtils.genCellCache(self.view.rectangleNode.colorGroup2.conditionCell)
end

PuzzleChessboardConditionItem.InitPuzzleChessboardConditionItem = HL.Method(HL.Table, HL.Number, HL.Number, HL.Number)
        << function(self, eColor2ConditionsTbl, cellSize, spacing, gridNum)
    self:_FirstTimeInit()

    self:UpdateContent(eColor2ConditionsTbl, cellSize, spacing, gridNum)
    self:Toggle(true)
end

PuzzleChessboardConditionItem.UpdateContent = HL.Method(HL.Table, HL.Number, HL.Number, HL.Number)
        << function(self, eColor2ConditionsTbl, cellSize, spacing, gridNum)
    local overflow = false
    local empty = true
    local index = 1
    for eColor, conditionsTbl in pairs(eColor2ConditionsTbl) do
        local colorGroupId = 'colorGroup' .. index

        local rectangleNode = self.view.rectangleNode
        rectangleNode.colorGroup1.gameObject:SetActiveIfNecessary(index >= 1)
        rectangleNode.colorGroup2.gameObject:SetActiveIfNecessary(index >= 2)

        local rectangleGroup = rectangleNode[colorGroupId]
        if rectangleGroup then
            rectangleGroup.overBG.gameObject:SetActiveIfNecessary(conditionsTbl.overflow)
        end

        local conditions = conditionsTbl.conditions
        local count = #conditions
        if count >= 0 then
            if index == 1 then
                self.m_group1ConditionCells:Refresh(count, function(cell, index)
                    cell:InitPuzzleChessboardConditionCell(eColor, conditions[index])
                end)
            elseif index == 2 then
                self.m_group2ConditionCells:Refresh(count, function(cell, index)
                    cell:InitPuzzleChessboardConditionCell(eColor, conditions[index])
                end)
            end

            if count > 0 then
                empty = false
            end
        end


        local numberNode = self.view.numberNode
        numberNode.colorGroup1.gameObject:SetActiveIfNecessary(index >= 1)
        numberNode.colorGroup2.gameObject:SetActiveIfNecessary(index >= 2)
        numberNode.line.gameObject:SetActiveIfNecessary(index >= 2)

        local numberGroup = numberNode[colorGroupId]
        if numberGroup then
            local stateCount = conditionsTbl.stateCount
            numberGroup.overflow.gameObject:SetActiveIfNecessary(stateCount < 0)
            numberGroup.correct.gameObject:SetActiveIfNecessary(stateCount == 0)
            local alpha = stateCount == 0 and self.config.NUMBER_NODE_CORRECT_ALPHA or 1
            numberGroup.canvasGroup.alpha = alpha

            numberGroup.txtNum.text = conditionsTbl.rawCount

            local color = UIUtils.getPuzzleColorByColorType(eColor)
            numberGroup.txtNum.color = color
            numberGroup.correct.color = color
        end

        overflow = overflow or conditionsTbl.overflow
        index = index + 1
    end

    local calcSizeDelta
    local size = (cellSize + spacing) * gridNum

    calcSizeDelta = Vector2(0, size)






    self.view.overflow.sizeDelta = calcSizeDelta
    self.view.overflow.gameObject:SetActiveIfNecessary(overflow)
    self.view.emptyNode.gameObject:SetActiveIfNecessary(empty)
    self.view.contentNode.gameObject:SetActiveIfNecessary(not empty)
end

PuzzleChessboardConditionItem.Toggle = HL.Method(HL.Boolean) << function(self, rectangle)
    self.view.rectangleNode.gameObject:SetActiveIfNecessary(rectangle)
    self.view.numberNode.gameObject:SetActiveIfNecessary(not rectangle)
end

HL.Commit(PuzzleChessboardConditionItem)
return PuzzleChessboardConditionItem

