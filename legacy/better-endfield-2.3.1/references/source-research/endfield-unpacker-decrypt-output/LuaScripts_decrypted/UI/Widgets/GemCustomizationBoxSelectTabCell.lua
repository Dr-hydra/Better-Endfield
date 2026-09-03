local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GemCustomizationBoxSelectTabCell = HL.Class('GemCustomizationBoxSelectTabCell', UIWidgetBase)

GemCustomizationBoxSelectTabCell.m_data = HL.Field(HL.Table)

GemCustomizationBoxSelectTabCell.m_btnClickCallback = HL.Field(HL.Function)


GemCustomizationBoxSelectTabCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.selectBtn.onClick:AddListener(function()
        self:_OnBtnClick()
    end)
end

GemCustomizationBoxSelectTabCell.InitGemCustomizationBoxSelectTabCell = HL.Method() << function(self)
    self:_FirstTimeInit()

    self.m_data = {}
    self.m_btnClickCallback = nil
end

GemCustomizationBoxSelectTabCell.SetBtnClickCallback = HL.Method(HL.Function) << function(self, callback)
    self.m_btnClickCallback = callback
end

GemCustomizationBoxSelectTabCell.SetupView = HL.Method(HL.Table) << function(self, data)
    self.m_data = data
    local stateCtrl = self.view.stateController

    if data["cannotSelect"] then

        self.view.nameTxt.text = Language.LUA_GEMCUSTOMIZATIONBOX_TAB_CANNOTSELECT
        stateCtrl:SetState("random")
        return
    end
    local currTabIndex = self.m_data["tabIndex"]

    local haveChooseTerm = false
    if self.m_data["selectInfo"]["selectTermIds"][currTabIndex] == nil then
        self.view.nameTxt.text = Language.LUA_GEMCUSTOMIZATIONBOX_TAB_WAITSELECT
    else
        local _, termCfg = Tables.gemTable:TryGetValue(self.m_data["selectInfo"]["selectTermIds"][currTabIndex])
        if not termCfg then
            logger.LogError("GemCustomizationBoxTermCell._SetupText: termCfg is nil, termId = " .. self.m_termId)
            return
        end

        local skillNameFormat = Language.LUA_GEM_CARD_SKILL_ACTIVE
        self.view.nameTxt:SetAndResolveTextStyle(string.format(skillNameFormat, termCfg.tagName))
        haveChooseTerm = true
    end

    if currTabIndex == self.m_data["selectInfo"]["selectTabIndex"] then

        if haveChooseTerm then
            stateCtrl:SetState("selectHaveTerm")
        else
            stateCtrl:SetState("selectNoTerm")
        end
    else

        if haveChooseTerm then
            stateCtrl:SetState("OtherSelect")
        else
            stateCtrl:SetState("unselect")
        end
    end
end

GemCustomizationBoxSelectTabCell._OnBtnClick = HL.Method() << function(self)
    if self.m_btnClickCallback ~= nil then
        self.m_btnClickCallback(self.m_data)
    end
end

HL.Commit(GemCustomizationBoxSelectTabCell)
return GemCustomizationBoxSelectTabCell

