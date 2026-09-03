local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GuideSlideScreen

GuideSlideScreenCtrl = HL.Class('GuideSlideScreenCtrl', uiCtrl.UICtrl)






GuideSlideScreenCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_GUIDE_SLIDE_SCREEN_PANEL] = 'HideGuideSlideScreenPanel',
}


GuideSlideScreenCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

GuideSlideScreenCtrl.ShowGuideSlideScreenPanel = HL.StaticMethod() << function()
    UIManager:AutoOpen(PANEL_ID)
end

GuideSlideScreenCtrl.HideGuideSlideScreenPanel = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

HL.Commit(GuideSlideScreenCtrl)
