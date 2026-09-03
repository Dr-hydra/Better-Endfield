local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

LevelBreakNode = HL.Class('LevelBreakNode', UIWidgetBase)

LevelBreakNode.m_breakCellCache = HL.Field(HL.Forward("UIListCache"))



LevelBreakNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_breakCellCache = UIUtils.genCellCache(self.view.breakCell)
end

LevelBreakNode.InitLevelBreakNodeSimple = HL.Method(HL.Number, HL.Number, HL.Opt(HL.Boolean)) << function(self, curBreakStage, maxBreakStage, showNextStage)
    self:_FirstTimeInit()

    self.m_breakCellCache:Refresh(maxBreakStage, function(cell, index)
        local hadBreak = curBreakStage >= index

        cell.normal.gameObject:SetActive(not hadBreak)
        cell.done.gameObject:SetActive(hadBreak)
        cell.breaking.gameObject:SetActive(showNextStage and curBreakStage + 1 == index)
    end)
end

LevelBreakNode.InitLevelBreakNode = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Table)) << function(self, curBreakStage, showNextBreakStage, customBreakInfoList)
    self:_FirstTimeInit()
    local breakInfoList = customBreakInfoList or CharInfoUtils.getPlayerBreakInfoList()

    self.m_breakCellCache:Refresh(#breakInfoList, function(cell, index)
        local breakInfo = breakInfoList[index]
        local isHide = breakInfo.isHide

        if isHide then
            cell.gameObject:SetActive(false)
            return
        end

        local hadBreak = curBreakStage >= index

        cell.normal.gameObject:SetActive(not hadBreak)
        cell.done.gameObject:SetActive(hadBreak)
        cell.breaking.gameObject:SetActive(showNextBreakStage and (curBreakStage + 1 == index))
    end)
end

HL.Commit(LevelBreakNode)
return LevelBreakNode

