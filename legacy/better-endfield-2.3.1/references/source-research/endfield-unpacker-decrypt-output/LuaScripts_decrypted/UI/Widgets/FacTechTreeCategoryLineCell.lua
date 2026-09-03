local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacTechTreeCategoryLineCell = HL.Class('FacTechTreeCategoryLineCell', UIWidgetBase)

FacTechTreeCategoryLineCell.m_layerId = HL.Field(HL.String) << ""


FacTechTreeCategoryLineCell._OnFirstTimeInit = HL.Override() << function(self)

end

FacTechTreeCategoryLineCell.InitFacTechTreeCategoryLineCell = HL.Method(HL.Table) << function(self, categoryLine)
    self:_FirstTimeInit()

    self.m_layerId = categoryLine.layerId

    local width = categoryLine.width
    local height = categoryLine.height
    local posX = categoryLine.posX
    local posY = categoryLine.posY

    self.view.rectTransform.sizeDelta = Vector2(width, height)
    self.view.transform.localPosition = Vector3(posX, posY)

    self:Refresh()
end

FacTechTreeCategoryLineCell.Refresh = HL.Method() << function(self)
    local layerLocked = GameInstance.player.facTechTreeSystem:LayerIsLocked(self.m_layerId)
    self.view.whiteLine.gameObject:SetActiveIfNecessary(layerLocked)
    self.view.blackLine.gameObject:SetActiveIfNecessary(not layerLocked)
end

HL.Commit(FacTechTreeCategoryLineCell)
return FacTechTreeCategoryLineCell

