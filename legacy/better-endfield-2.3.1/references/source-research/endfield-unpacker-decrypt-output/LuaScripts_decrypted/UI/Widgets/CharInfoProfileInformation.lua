local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharInfoProfileInformation = HL.Class('CharInfoProfileInformation', UIWidgetBase)


CharInfoProfileInformation.m_starIconCellCache = HL.Field(HL.Forward("UIListCache"))


CharInfoProfileInformation._OnFirstTimeInit = HL.Override() << function(self)
    self.m_starIconCellCache = UIUtils.genCellCache(self.view.starIcon)
    self:_RegisterPlayAnimationOut()
end

CharInfoProfileInformation.InitCharInfoProfileInformation = HL.Method(HL.Table) << function(self, charInfo)
    self:_FirstTimeInit()
    local templateId = charInfo.templateId
    local tbData = CharInfoUtils.getCharTableData(templateId)
    self.view.nameText.text = tbData.name
    self.m_starIconCellCache:Refresh(tbData.rarity)
end

HL.Commit(CharInfoProfileInformation)
return CharInfoProfileInformation

