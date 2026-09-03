local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

StarGroup = HL.Class('StarGroup', UIWidgetBase)

StarGroup.m_starCellCache = HL.Field(HL.Forward("UIListCache"))


StarGroup._OnFirstTimeInit = HL.Override() << function(self)
    self.m_starCellCache = UIUtils.genCellCache(self.view.starCell)
end

StarGroup.InitStarGroup = HL.Method(HL.Number) << function(self, count)
    self:_FirstTimeInit()

    self.m_starCellCache:Refresh(count, function(cell, index)
        cell.gameObject.name = "StarCell" .. index
    end)
end

HL.Commit(StarGroup)
return StarGroup
