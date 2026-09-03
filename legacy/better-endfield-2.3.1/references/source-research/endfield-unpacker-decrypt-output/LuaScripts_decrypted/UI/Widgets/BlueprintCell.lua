local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BlueprintCell = HL.Class('BlueprintCell', UIWidgetBase)


BlueprintCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.button.onClick:AddListener(function()
        self.m_onClick()
    end)
end

BlueprintCell.m_onClick = HL.Field(HL.Function)

BlueprintCell.m_showStatus = HL.Field(HL.Boolean) << false

BlueprintCell.m_inst = HL.Field(HL.Any)

BlueprintCell.InitBlueprintCell = HL.Method(HL.Table) << function(self, arg)
    self.m_inst = arg.inst
    local onClick = arg.onClick
    self.m_showStatus = arg.showStatus
    self:_FirstTimeInit()

    self.m_onClick = onClick

    local bpInfo = self.m_inst.csInst.info
    self.view.icon:InitBlueprintIcon(bpInfo.icon.icon, bpInfo.icon.baseColor)

    self.view.nameTxt.text = bpInfo.name
    self.view.delMark.gameObject:SetActive(self.m_inst.isDel == true)
    self.view.redDot:InitRedDot("SingleBlueprint", self.m_inst.id)
    self.view.gameObject.name = "BP_"..self.m_inst.csInst.param:ToString()

    local reviewStatus = self.m_inst.csInst.reviewStatus
    if self.m_inst.csInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys or self.m_inst.csInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift then
        reviewStatus = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.Approved
    end
    self:RefreshCellState(reviewStatus)
end

BlueprintCell.RefreshCellState = HL.Method(HL.Any) << function(self, reviewStatus)
    if self.m_showStatus then
        local csInst = self.m_inst.csInst
        local bpInfo = csInst.info
        local isExpired = false
        if bpInfo.bp.timeLimitedFormulas then
            logger.info(bpInfo.bp.timeLimitedFormulas, bpInfo.bp.timeLimitedFormulas.Count)
            for _, formulaIdInt in pairs(bpInfo.bp.timeLimitedFormulas) do
                if FactoryUtils.isExpiredTimeLimitedFormula(CS.Beyond.Cfg.Tables.formulaIdToStr:GetValue(formulaIdInt)) then
                    isExpired = true
                    break
                end
            end
        end
        if not isExpired then
            if csInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys or csInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift then
                csInst.reviewStatus = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.Approved
            end
        end
        self.view.inAuditNode.gameObject:SetActive(not isExpired and csInst.reviewStatus == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.InProgress)
        self.view.canShareNode.gameObject:SetActive(not isExpired and csInst.reviewStatus == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.Approved)
        self.view.timeLimitedExpiredNode.gameObject:SetActive(isExpired)
    else
        self.view.inAuditNode.gameObject:SetActive(false)
        self.view.canShareNode.gameObject:SetActive(false)
        self.view.timeLimitedExpiredNode.gameObject:SetActive(false)
    end
end


HL.Commit(BlueprintCell)
return BlueprintCell
