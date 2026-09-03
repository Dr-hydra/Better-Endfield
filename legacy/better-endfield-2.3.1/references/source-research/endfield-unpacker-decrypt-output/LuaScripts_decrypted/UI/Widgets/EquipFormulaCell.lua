local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquipFormulaCell = HL.Class('EquipFormulaCell', UIWidgetBase)


EquipFormulaCell._OnFirstTimeInit = HL.Override() << function(self)

end

EquipFormulaCell.InitEquipFormulaCell = HL.Method() << function(self)
    self:_FirstTimeInit()


end

HL.Commit(EquipFormulaCell)
return EquipFormulaCell

