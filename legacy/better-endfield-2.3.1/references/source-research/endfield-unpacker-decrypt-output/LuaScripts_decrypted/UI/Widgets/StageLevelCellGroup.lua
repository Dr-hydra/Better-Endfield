local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

StageLevelCellGroup = HL.Class('StageLevelCellGroup', UIWidgetBase)

StageLevelCellGroup.m_cellCache = HL.Field(HL.Forward("UIListCache"))

StageLevelCellGroup._OnFirstTimeInit = HL.Override() << function(self)
    self.m_cellCache = UIUtils.genCellCache(self.view.cell)
end

StageLevelCellGroup.InitStageLevelCellGroup = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, count, isLock, isActive)
    self:_FirstTimeInit()

    self.m_cellCache:Refresh(count, function(cell, index)
        if isLock then
            cell.image.color = self.view.config.COLOR_LOCK
        else
            if isActive then
                cell.image.color = self.view.config.COLOR_ACTIVE
            else
                cell.image.color = self.view.config.COLOR_DEFAULT
            end
        end
    end)
end

StageLevelCellGroup.InitStageLevelCellGroupByPassiveNodeList = HL.Method(HL.Number, HL.Table, HL.Opt(HL.Boolean)) << function(self, charInstId, allPassiveNodeList, hideWhenNoActive)
    self:_FirstTimeInit()

    local anyActive = false
    self.m_cellCache:Refresh(#allPassiveNodeList, function(cell, index)
        local passiveCfg = allPassiveNodeList[index]
        local isActive = CharInfoUtils.getPassiveSkillNodeStatus(charInstId, passiveCfg.nodeId)

        if isActive then
            anyActive = true
            cell.image.color = self.view.config.COLOR_DEFAULT
        else
            cell.image.color = self.view.config.COLOR_LOCK
        end
    end)
    if hideWhenNoActive then
        self.view.gameObject:SetActive(anyActive)
    end
end

HL.Commit(StageLevelCellGroup)
return StageLevelCellGroup

