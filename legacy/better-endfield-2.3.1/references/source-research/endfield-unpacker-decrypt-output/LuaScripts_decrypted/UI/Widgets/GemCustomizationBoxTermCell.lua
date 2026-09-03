local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GemCustomizationBoxTermCell = HL.Class('GemCustomizationBoxTermCell', UIWidgetBase)

GemCustomizationBoxTermCell.m_termId = HL.Field(HL.String) << ""


GemCustomizationBoxTermCell._OnFirstTimeInit = HL.Override() << function(self)
    if self.view.tagBtn then
        self.view.tagBtn.onClick:AddListener(function()
            self:_OnBtnClick()
        end)
    end
end

GemCustomizationBoxTermCell.InitGemCustomizationBoxTermCell = HL.Method(HL.String) << function(self, termId)
    self:_FirstTimeInit()

    self.m_termId = termId
end

GemCustomizationBoxTermCell.RefreshUI = HL.Method(HL.String, HL.Table, HL.Boolean)
    << function(self, termId, currSelectInfo, canSelectGroup)
    self.m_termId = termId
    local currSelectTabIndex = currSelectInfo["selectTabIndex"]
    local selectTabIndex = GemCustomizationBoxTermCell.CheckTermIdIsSelected(termId, currSelectInfo)
    if selectTabIndex == currSelectTabIndex then

        self.view.stateController:SetState("select")
        self.view.tagBtn.customBindingViewLabelText = Language.LUA_GEMCUSTOMIZATIONBOX_TERMBTN_NOSELECT
    elseif selectTabIndex ~= 0 then

        self.view.stateController:SetState("unselect")
        self.view.tagBtn.customBindingViewLabelText = Language.LUA_GEMCUSTOMIZATIONBOX_TERMBTN_SELECTGREY
    elseif canSelectGroup == false then

        self.view.stateController:SetState("random")
    else

        self.view.stateController:SetState("normal")
        self.view.tagBtn.customBindingViewLabelText = Language.LUA_GEMCUSTOMIZATIONBOX_TERMBTN_SELECT
    end
    self:_SetupText();
end

GemCustomizationBoxTermCell.RefreshUIInPreviewMode = HL.Method(HL.String)
    << function(self, termId)
    self.m_termId = termId
    self.view.stateController:SetState("random")
    self:_SetupText();
end

GemCustomizationBoxTermCell.GetTermId = HL.Method().Return(HL.String) << function(self)
    return self.m_termId
end

GemCustomizationBoxTermCell._SetupText = HL.Method() << function(self)
    local _, termCfg = Tables.gemTable:TryGetValue(self.m_termId)
    if not termCfg then
        logger.LogError("GemCustomizationBoxTermCell._SetupText: termCfg is nil, termId = " .. self.m_termId)
        return
    end

    local skillNameFormat = Language.LUA_GEM_CARD_SKILL_ACTIVE
    self.view.name:SetAndResolveTextStyle(string.format(skillNameFormat, termCfg.tagName))
end

GemCustomizationBoxTermCell._OnBtnClick = HL.Method() << function(self)
    Notify(MessageConst.ON_GEMCUSTOMIZATIONBOX_TERM_SELECT, self.m_termId)
end


GemCustomizationBoxTermCell.CheckTermIdIsSelected = HL.StaticMethod(HL.String, HL.Table).Return(HL.Number)
    << function(termId, selectInfo)
    for i = 1, 3 do
        if selectInfo["selectTermIds"] ~= nil then
            local selectTermId = selectInfo["selectTermIds"][i]
            if selectTermId == termId then
                return i
            end
        end
    end
    return 0
end

HL.Commit(GemCustomizationBoxTermCell)
return GemCustomizationBoxTermCell

