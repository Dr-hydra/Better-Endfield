local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquipSuitBuildNode = HL.Class('EquipSuitBuildNode', UIWidgetBase)


EquipSuitBuildNode._OnFirstTimeInit = HL.Override() << function(self)

end

EquipSuitBuildNode.InitEquipSuitBuildNode = HL.Method() << function(self)
    self:_FirstTimeInit()


end

HL.Commit(EquipSuitBuildNode)
return EquipSuitBuildNode

