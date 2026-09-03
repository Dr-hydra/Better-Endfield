local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ElitePolygon = HL.Class('ElitePolygon', UIWidgetBase)

ElitePolygon.m_eliteCells = HL.Field(HL.Table)


ElitePolygon._OnFirstTimeInit = HL.Override() << function(self)

    self.m_eliteCells = {}
    self.m_eliteCells[1] = self.view.cell1
    self.m_eliteCells[2] = self.view.cell2
    self.m_eliteCells[3] = self.view.cell3
end

ElitePolygon.InitElitePolygon = HL.Method(HL.Number) << function(self, eliteLevel)
    self:_FirstTimeInit()

    for eliteIndex, cell in ipairs(self.m_eliteCells) do
        cell.selected.gameObject:SetActive(eliteLevel >= eliteIndex)
    end

end

HL.Commit(ElitePolygon)
return ElitePolygon

