
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendlyTips
local PHASE_ID = PhaseId.FriendlyTips

FriendlyTipsCtrl = HL.Class('FriendlyTipsCtrl', uiCtrl.UICtrl)







FriendlyTipsCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FriendlyTipsCtrl.m_callback = HL.Field(HL.Any)

FriendlyTipsCtrl.m_inited = HL.Field(HL.Boolean) << false

FriendlyTipsCtrl.m_isCloseCalled = HL.Field(HL.Boolean) << false

FriendlyTipsCtrl.OnOpen = HL.StaticMethod(HL.Table) << function(arg)
    PhaseManager:OpenPhaseFast(PHASE_ID, arg)
end

FriendlyTipsCtrl.OnShow = HL.Override() << function(self)
    self.view.mediaDeco:PlayInAnimation(function()
        self.m_inited = true
    end)
end


FriendlyTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_inited = false
    local callback = unpack(arg)
    self.m_callback = callback

    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickCloseBtn()
    end)
    self.view.btnCloseFake.onClick:AddListener(function()
        self:_OnClickCloseBtn()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

FriendlyTipsCtrl._OnClickCloseBtn = HL.Method() << function(self)
    if not self.m_inited then
        return
    end
    if self.m_isCloseCalled then
        return
    end

    self.m_isCloseCalled = true
    self.view.mediaDeco:PlayOutAnimation(function()
        self:Exit()
    end)
end

FriendlyTipsCtrl.Exit = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
    if self.m_callback then
        self.m_callback()
    end
end

FriendlyTipsCtrl._OnOpenFriendlyTips = HL.StaticMethod(HL.Table) << function(arg)
    PhaseManager:OpenPhaseFast(PHASE_ID, arg)
end

HL.Commit(FriendlyTipsCtrl)
