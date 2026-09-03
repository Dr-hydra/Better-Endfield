
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainGradeSourceInstruction

DomainGradeSourceInstructionCtrl = HL.Class('DomainGradeSourceInstructionCtrl', uiCtrl.UICtrl)

local domainDevelopmentSystem = GameInstance.player.domainDevelopmentSystem





DomainGradeSourceInstructionCtrl.s_messages = HL.StaticField(HL.Table) << {

}



DomainGradeSourceInstructionCtrl.m_domainId = HL.Field(HL.String) << ""

DomainGradeSourceInstructionCtrl.m_DegreeSourceInfo = HL.Field(HL.Table)

DomainGradeSourceInstructionCtrl.m_genSourceCells = HL.Field(HL.Forward("UIListCache"))




DomainGradeSourceInstructionCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_domainId = arg
    self:_InitUI()

    local _, domainData = domainDevelopmentSystem.domainDevDataDic:TryGetValue(self.m_domainId)
    local curExp = domainData.exp
    self.m_DegreeSourceInfo = {}
    for sourceType, value in cs_pairs(domainData.degreeSource) do
        local hasCfg, cfg = Tables.domainDegreeSourceTable:TryGetValue(sourceType)
        if hasCfg then
            local sourceInfo = {
                value = value,
                name = cfg.sourceName,

                order = sourceType:GetHashCode(),
            }
            table.insert(self.m_DegreeSourceInfo, sourceInfo)
        end
    end
    table.sort(self.m_DegreeSourceInfo, Utils.genSortFunction({ "order"}))

    self.view.curDomainExpTxt.text = curExp
    local sourceCount = #self.m_DegreeSourceInfo
    if sourceCount < 1 then
        self.m_genSourceCells:Refresh(1, function(cell, luaIndex)
            cell.sourceState:SetState("EmptyState")
        end)
    else
        self.m_genSourceCells:Refresh(sourceCount, function(cell, luaIndex)
            cell.sourceState:SetState("NoramlState")
            local info = self.m_DegreeSourceInfo[luaIndex]
            cell.sourceNameTxt.text = info.name
            cell.numTxt.text = info.value
        end)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end






DomainGradeSourceInstructionCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.m_genSourceCells = UIUtils.genCellCache(self.view.sourceCell)
end


HL.Commit(DomainGradeSourceInstructionCtrl)
