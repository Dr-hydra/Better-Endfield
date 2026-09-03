local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PanelAttributeCell = HL.Class('PanelAttributeCell', UIWidgetBase)

PanelAttributeCell.info = HL.Field(HL.Table)

PanelAttributeCell.data = HL.Field(HL.Userdata)




PanelAttributeCell._OnFirstTimeInit = HL.Override() << function(self)

end










PanelAttributeCell.InitPanelAttributeCell = HL.Method(HL.Any) << function(self, info)
    self:_FirstTimeInit()
    local attributeType = info.attributeType
    local showValue = info.showValue
    local showName = info.showName
    local spriteName = UIConst.UI_ATTRIBUTE_ICON_PREFIX .. Const.ATTRIBUTE_TYPE_2_ATTRIBUTE_DATA_KEY[attributeType]
    local hasGemAddOn = info and info.extraInfo and info.extraInfo.hasGemAddOn

    self.view.imageIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, spriteName)
    self.view.textNum.color = hasGemAddOn and self.view.config.GEM_EFFECT_COLOR or self.view.config.COLOR_ORIGIN
    self.view.textName.text = showName
    self.view.textNum.text =  "+" .. showValue
end

HL.Commit(PanelAttributeCell)
return PanelAttributeCell
