local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

UIFoodNotesCell = HL.Class('UIFoodNotesCell', UIWidgetBase)


UIFoodNotesCell._OnFirstTimeInit = HL.Override() << function(self)

end

UIFoodNotesCell.InitUIFoodNotesCell = HL.Method() << function(self)
    self:_FirstTimeInit()



end

HL.Commit(UIFoodNotesCell)
return UIFoodNotesCell

