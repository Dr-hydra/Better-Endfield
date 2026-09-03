local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

InvalidBuildingCell = HL.Class('InvalidBuildingCell', UIWidgetBase)


InvalidBuildingCell._OnFirstTimeInit = HL.Override() << function(self)

end

InvalidBuildingCell.InitInvalidBuildingCell = HL.Method() << function(self)
    self:_FirstTimeInit()


end

HL.Commit(InvalidBuildingCell)
return InvalidBuildingCell

