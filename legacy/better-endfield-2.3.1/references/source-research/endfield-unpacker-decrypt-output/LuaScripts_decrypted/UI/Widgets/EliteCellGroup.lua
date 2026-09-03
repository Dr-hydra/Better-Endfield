local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EliteCellGroup = HL.Class('EliteCellGroup', UIWidgetBase)

EliteCellGroup.m_eliteCellCache = HL.Field(HL.Forward("UIListCache"))

EliteCellGroup._OnFirstTimeInit = HL.Override() << function(self)
    self.m_eliteCellCache = UIUtils.genCellCache(self.view.eliteCell)
end

EliteCellGroup.InitEliteCellGroup = HL.Method(HL.Number) << function(self, eliteStage)
    self:_FirstTimeInit()

    local maxBreakStage = Tables.characterConst.maxBreak
    self.m_eliteCellCache:Refresh(maxBreakStage, function(cell, index)
        if index <= eliteStage then
            cell.icon.color = self.view.config.CELL_COLOR_FILL
        else
            cell.icon.color = self.view.config.CELL_COLOR_EMPTY
        end
    end)
end

HL.Commit(EliteCellGroup)
return EliteCellGroup

