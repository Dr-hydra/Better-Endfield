local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharInfoSkillCell = HL.Class('CharInfoSkillCell', UIWidgetBase)


CharInfoSkillCell._OnFirstTimeInit = HL.Override() << function(self)
end

CharInfoSkillCell.skills = HL.Field(HL.Table)

CharInfoSkillCell.m_charInfo = HL.Field(HL.Table)

CharInfoSkillCell.onClickCallback = HL.Field(HL.Function)

CharInfoSkillCell.InitCharInfoSkillCell = HL.Method(HL.Table, HL.Opt(HL.Function)) << function(self, info, callback)
    self:_FirstTimeInit()

    self.skills = info
    self.view.single.buttonSkill.view.button.onClick:RemoveAllListeners()
    self.view.plural.buttonSkill1.view.button.onClick:RemoveAllListeners()
    self.view.plural.buttonSkill2.view.button.onClick:RemoveAllListeners()
    self.onClickCallback = callback
    self:_Refresh()
end

CharInfoSkillCell._OnClick = HL.Method() << function(self)
    if self.onClickCallback then
        self.onClickCallback(self.skills)
    end
end

CharInfoSkillCell._Refresh = HL.Method() << function(self)
    local single = #self.skills == 1
    self.view.single.gameObject:SetActive(not not single)
    self.view.plural.gameObject:SetActive(not single)

    if single then
        self.view.single.buttonSkill:InitCharInfoSkillButton(self.skills[1], function()
            self:_OnClick()
        end)
        self.view.single.buttonSkillShadow:InitCharInfoSkillButton(self.skills[1], function()
            self:_OnClick()
        end)
        self.view.single.buttonSkill:RefreshRedDot(self.m_charInfo)
    else
        for i = 1, #self.skills do
            local buttonSkillShadow = self.view.plural[string.format("buttonSkillShadow%d", i)]
            local buttonSkill = self.view.plural[string.format("buttonSkill%d", i)]
            buttonSkillShadow:InitCharInfoSkillButton(self.skills[i], function()
                self:_OnClick()
            end)
            buttonSkill:InitCharInfoSkillButton(self.skills[i], function()
                self:_OnClick()
            end)

            buttonSkill:RefreshRedDot(self.m_charInfo)
        end
    end
end

CharInfoSkillCell.InitSkill = HL.Method(HL.Table) << function(self, data)
    self.skills = data.skills
    self.m_charInfo = data.charInfo
    self:_Refresh()
end

CharInfoSkillCell.SetSelect = HL.Method(HL.Boolean) << function(self, select)
    local single = #self.skills == 1
    if single then
        self.view.single.buttonSkill.view.imageSelect2.gameObject:SetActive(select)
    else
        self.view.plural.select.gameObject:SetActive(select)
    end
end

HL.Commit(CharInfoSkillCell)
return CharInfoSkillCell

