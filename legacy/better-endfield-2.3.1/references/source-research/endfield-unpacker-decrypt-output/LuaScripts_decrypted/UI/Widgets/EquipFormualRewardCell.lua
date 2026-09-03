local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquipFormualRewardCell = HL.Class('EquipFormualRewardCell', UIWidgetBase)


EquipFormualRewardCell._OnFirstTimeInit = HL.Override() << function(self)

end

EquipFormualRewardCell.InitEquipFormualRewardCell = HL.Method() << function(self)
    self:_FirstTimeInit()


end

HL.Commit(EquipFormualRewardCell)
return EquipFormualRewardCell

