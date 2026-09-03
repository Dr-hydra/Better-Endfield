
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonBlackOut

CommonBlackOutCtrl = HL.Class('CommonBlackOutCtrl', uiCtrl.UICtrl)






CommonBlackOutCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CommonBlackOutCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end


CommonBlackOutCtrl.m_args = HL.Field(HL.Table)









CommonBlackOutCtrl.StartCommonBlackOut = HL.StaticMethod(HL.Table) << function(args)

    local self = UIManager:AutoOpen(PANEL_ID)
    self.m_args = args
    self:_StartTransition()
end

CommonBlackOutCtrl._StartTransition = HL.Method() << function(self)
    self.view.mask.color = Color(0, 0, 0, 0)
    self.view.mask:DOKill()
    local t = self.view.mask:DOFade(1, self.m_args.fadeInTime or 0.3)
    t:OnComplete(function()
        self:_OnFadeInComplete()
    end)
end

CommonBlackOutCtrl._OnFadeInComplete = HL.Method() << function(self)
    if self.m_args.onFadeInComplete then
        self.m_args.onFadeInComplete()
    end
    if self.m_args.fadeOutTime and self.m_args.fadeOutTime > 0 then
        local t = self.view.mask:DOFade(0, self.m_args.fadeOutTime)
        t:OnComplete(function()
            self:_OnFadeOutComplete()
        end)
    else
        self:_OnFadeOutComplete()
    end
end

CommonBlackOutCtrl._OnFadeOutComplete = HL.Method() << function(self)
    local onComplete = self.m_args.onFadeOutComplete
    if onComplete then
        onComplete()
    end
    self:_StartCoroutine(function()


        coroutine.step()
        coroutine.step()
        self:Close()
    end)
end

HL.Commit(CommonBlackOutCtrl)
