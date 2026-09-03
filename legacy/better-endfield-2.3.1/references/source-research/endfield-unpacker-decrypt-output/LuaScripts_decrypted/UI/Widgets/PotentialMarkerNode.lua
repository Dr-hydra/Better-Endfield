local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PotentialMarkerNode = HL.Class('PotentialMarkerNode', UIWidgetBase)


PotentialMarkerNode._OnFirstTimeInit = HL.Override() << function(self)

end

PotentialMarkerNode.InitPotentialMarkerNode = HL.Method() << function(self)
    self:_FirstTimeInit()


end

HL.Commit(PotentialMarkerNode)
return PotentialMarkerNode

