local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PuzzleChessboardConditionCell = HL.Class('PuzzleChessboardConditionCell', UIWidgetBase)


PuzzleChessboardConditionCell._OnFirstTimeInit = HL.Override() << function(self)
end

PuzzleChessboardConditionCell.InitPuzzleChessboardConditionCell = HL.Method(HL.Any, HL.Table) << function(self, eColor, data)
    self:_FirstTimeInit()

    local color = UIUtils.getPuzzleColorByColorType(eColor)
    self.view.done.color = color
    self.view.undone.color = color
    self.view.over.color = color


    self.view.over.gameObject:SetActiveIfNecessary(data.overflow)
    self.view.undone.gameObject:SetActiveIfNecessary(not data.done)
    self.view.done.gameObject:SetActiveIfNecessary(data.done)
end

HL.Commit(PuzzleChessboardConditionCell)
return PuzzleChessboardConditionCell

