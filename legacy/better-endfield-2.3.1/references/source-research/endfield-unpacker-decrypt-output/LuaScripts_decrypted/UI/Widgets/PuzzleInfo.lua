local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PuzzleInfo = HL.Class('PuzzleInfo', UIWidgetBase)

PuzzleInfo.m_puzzleCells = HL.Field(HL.Forward("UIListCache"))

PuzzleInfo.m_puzzleCellData = HL.Field(HL.Table)

PuzzleInfo.m_puzzleCellSize = HL.Field(HL.Number) << -1


PuzzleInfo._OnFirstTimeInit = HL.Override() << function(self)

    self.m_puzzleCells = UIUtils.genCellCache(self.view.puzzleCell)
end

PuzzleInfo.InitPuzzleInfo = HL.Method(HL.Table, HL.Number) << function(self, data, size)
    self:_FirstTimeInit()


    self.view.infoBtn.gameObject:SetActive(not data.playerHold)
    self.view.puzzleCell.color = data.playerHold and UIUtils.getPuzzleColorByColorType(data.color) or Color.black
    UIUtils.changeAlpha(self.view.puzzleCell, data.playerHold and self.config.IMAGE_ALPHA or 1)

    self.m_puzzleCellData = data
    self.m_puzzleCellSize = size
    self.m_puzzleCells:Refresh(#data.rotateBlocks, function(cell, index)
        self:_UpdateCells(cell, index)
    end)
end

PuzzleInfo._UpdateCells = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local data = self.m_puzzleCellData.rotateBlocks[index]

    local rect = cell.gameObject:GetComponent('RectTransform')
    rect.anchoredPosition = Vector2(data.x, data.y) * self.m_puzzleCellSize
    rect.localScale = Vector3.one
end

HL.Commit(PuzzleInfo)
return PuzzleInfo

