local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

LvDotNode = HL.Class('LvDotNode', UIWidgetBase)


LvDotNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_lvDotCells = UIUtils.genCellCache(self.view.lvDotCell)
end

LvDotNode.m_lvDotCells = HL.Field(HL.Forward('UIListCache'))

LvDotNode.InitLvDotNode = HL.Method(HL.Number, HL.Number, HL.Opt(Color)) << function(self, curLv, maxLv, color)
    self:_FirstTimeInit()

    self.m_lvDotCells:Refresh(maxLv, function(cell, index)
        local reached = index <= curLv
        cell.empty.gameObject:SetActive(not reached)
        cell.full.gameObject:SetActive(reached)
        if color then
            cell.full.color = color
        end
    end)
    self.view.lvTxt.text = curLv
end

HL.Commit(LvDotNode)
return LvDotNode
