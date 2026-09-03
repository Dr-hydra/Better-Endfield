local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PlaceholderBaseWidget = HL.Class('PlaceholderBaseWidget', UIWidgetBase)

PlaceholderBaseWidget.m_panelId = HL.Field(HL.Number) << -1

PlaceholderBaseWidget.m_playAnimationOutMsg = HL.Field(HL.Number) << -1

PlaceholderBaseWidget.m_showMsg = HL.Field(HL.Number) << -1

PlaceholderBaseWidget.m_hideMsg = HL.Field(HL.Number) << -1


PlaceholderBaseWidget._OnFirstTimeInit = HL.Override() << function(self)
    self:_RegisterPlayAnimationOut()
end

PlaceholderBaseWidget._InitPlaceholder = HL.Virtual(HL.Opt(HL.Table)) << function(self, args)
    self:_FirstTimeInit()

    self.m_panelId = self:GetLuaPanel().panelId
    if self.gameObject.activeInHierarchy then
        self:_Show()
    else
        self:_Hide()
    end
end

PlaceholderBaseWidget._Show = HL.Method() << function(self)
    if self.m_panelId <= 0 then
        return
    end
    Notify(self.m_showMsg, self:GetArgs())
end

PlaceholderBaseWidget._Hide = HL.Method() << function(self)
    if self.m_panelId <= 0 then
        return
    end
    Notify(self.m_hideMsg, self:GetHideArgs())
end

PlaceholderBaseWidget.GetHideArgs = HL.Virtual().Return(HL.Any) << function(self)
    return self.m_panelId
end

PlaceholderBaseWidget.IsEmpty = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_panelId <= 0
end

PlaceholderBaseWidget._OnEnable = HL.Override() << function(self)
    self:_Show()
end

PlaceholderBaseWidget._OnDisable = HL.Override() << function(self)
    self:_Hide()
end

PlaceholderBaseWidget._OnDestroy = HL.Override() << function(self)
    self:_Hide()
end

PlaceholderBaseWidget.PlayAnimationOut = HL.Override() << function(self)
    Notify(self.m_playAnimationOutMsg, self.m_panelId)
end

PlaceholderBaseWidget.GetArgs = HL.Virtual().Return(HL.Table) << function(self)
end


HL.Commit(PlaceholderBaseWidget)
return PlaceholderBaseWidget
