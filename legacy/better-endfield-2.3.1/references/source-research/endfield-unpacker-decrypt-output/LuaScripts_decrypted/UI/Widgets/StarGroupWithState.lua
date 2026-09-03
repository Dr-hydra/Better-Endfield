local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

StarGroupWithState = HL.Class('StarGroupWithState', UIWidgetBase)

StarGroupWithState.m_starCellCache = HL.Field(HL.Forward("UIListCache"))


StarGroupWithState._OnFirstTimeInit = HL.Override() << function(self)
    self.m_starCellCache = self.m_starCellCache or UIUtils.genCellCache(self.view.starCell)
end

StarGroupWithState.InitStarGroupWithState = HL.Method(HL.Number, HL.Number, HL.String, HL.String)
    << function(self, totalNum, currentNum, fillState, emptyState)
    self:_FirstTimeInit()

    self.m_starCellCache:Refresh(totalNum, function(cell, index)
        cell.gameObject.name = "StarCell" .. index
        local state = index <= currentNum and fillState or emptyState
        cell.stateController:SetState(state)
    end)
end

HL.Commit(StarGroupWithState)
return StarGroupWithState

