
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.LeadingCharacter
local PHASE_ID = PhaseId.LeadingCharacter
LeadingCharacterCtrl = HL.Class('LeadingCharacterCtrl', uiCtrl.UICtrl)







LeadingCharacterCtrl.s_messages = HL.StaticField(HL.Table) << {

}


LeadingCharacterCtrl.m_callback = HL.Field(HL.Any)

LeadingCharacterCtrl.m_inited = HL.Field(HL.Boolean) << false

LeadingCharacterCtrl.m_isCloseCalled = HL.Field(HL.Boolean) << false

LeadingCharacterCtrl.OnOpen = HL.StaticMethod(HL.Table) << function(arg)
    PhaseManager:OpenPhaseFast(PHASE_ID, arg)
end

LeadingCharacterCtrl.OnShow = HL.Override() << function(self)
    self.view.mediaDeco:PlayInAnimation(function()
        self.m_inited = true
    end)
end


LeadingCharacterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_inited = false
    local callback = unpack(arg)
    self.m_callback = callback

    self.view.btnClose.onClick:AddListener(function()
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
    end)
end

LeadingCharacterCtrl.Exit = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
    if self.m_callback then
        self.m_callback()
    end
end

HL.Commit(LeadingCharacterCtrl)
