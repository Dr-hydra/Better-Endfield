local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharTypeIcon = HL.Class('CharTypeIcon', UIWidgetBase)



CharTypeIcon.InitCharTypeIcon = HL.Method(HL.String) << function(self, charTypeId)
    self:_FirstTimeInit()

    local _, charTypeData = Tables.charTypeTable:TryGetValue(charTypeId)
    if charTypeData then
        self.view.icon:LoadSprite(UIConst.UI_SPRITE_CHAR_ELEMENT, charTypeData.icon)
        self.view.bg.color = UIUtils.getColorByString(charTypeData.color)
    else
        logger.error('CharTypeIcon.InitCharTypeIcon: charTypeId not found in charTypeTable', charTypeId)
    end
end

HL.Commit(CharTypeIcon)
return CharTypeIcon

