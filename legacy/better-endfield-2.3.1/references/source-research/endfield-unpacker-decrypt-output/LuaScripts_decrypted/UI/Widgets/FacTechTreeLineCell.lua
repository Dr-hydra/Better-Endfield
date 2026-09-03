local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacTechTreeLineCell = HL.Class('FacTechTreeLineCell', UIWidgetBase)

FacTechTreeLineCell.m_lineInfo = HL.Field(HL.Table)


FacTechTreeLineCell._OnFirstTimeInit = HL.Override() << function(self)

end

FacTechTreeLineCell.InitFacTechTreeLineCell = HL.Method(HL.Table) << function(self, lineInfo)
    self:_FirstTimeInit()
    self.m_lineInfo = lineInfo
    local upX = lineInfo.upX
    local upY = lineInfo.upY
    local downX = lineInfo.downX
    local downY = lineInfo.downY
    local lineWeight = lineInfo.lineWeight
    local yDis = lineInfo.yDis
    self.view.lineUpCell.rectTransform.sizeDelta = Vector2(lineWeight, (yDis + lineWeight) / 2)
    self.view.lineUpCell.transform.localPosition = Vector3(upX, upY)
    self.view.lineDownCell.rectTransform.sizeDelta = Vector2(lineWeight, math.abs(downY - upY) - (yDis + lineWeight) / 2)
    self.view.lineDownCell.transform.localPosition = Vector3(downX, downY)
    self.view.lineHorizonCell.rectTransform.sizeDelta = Vector2(math.abs(upX - downX) + lineWeight, lineWeight)
    self.view.lineHorizonCell.transform.localPosition = Vector3((downX + upX) / 2, upY - yDis / 2)
    self:Refresh()

    self.view.gameObject.name = "Line-"..lineInfo.techId
end

FacTechTreeLineCell.Refresh = HL.Method() << function(self)
    self:_Refresh(self.view.lineHorizonCell)
    self:_Refresh(self.view.lineUpCell)
    self:_Refresh(self.view.lineDownCell)
end

FacTechTreeLineCell._Refresh = HL.Method(HL.Table) << function(self, item)
    local lineInfo = self.m_lineInfo
    local techId = lineInfo.techId
    local techTreeSystem = GameInstance.player.facTechTreeSystem
    local isUnlock = not techTreeSystem:NodeIsLocked(techId)

    item.light.gameObject:SetActiveIfNecessary(isUnlock)
    item.normal.gameObject:SetActiveIfNecessary(not isUnlock)
end

HL.Commit(FacTechTreeLineCell)
return FacTechTreeLineCell

