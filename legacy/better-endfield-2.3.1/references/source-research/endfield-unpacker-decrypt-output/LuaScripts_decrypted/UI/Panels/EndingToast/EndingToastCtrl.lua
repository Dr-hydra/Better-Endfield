
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.EndingToast
local PHASE_ID = PhaseId.EndingToast
EndingToastCtrl = HL.Class('EndingToastCtrl', uiCtrl.UICtrl)







EndingToastCtrl.s_messages = HL.StaticField(HL.Table) << {
}


EndingToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.fakeCloseBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    if BEYOND_DEBUG_COMMAND then
        UIUtils.bindInputEvent(CS.Beyond.Input.KeyboardKeyCode.Z, function()
            self.view.endingTost.gameObject:SetActive(false)
        end, nil, nil, self.view.inputGroup.groupId)
    end
end

EndingToastCtrl._OnShowEndingToast = HL.StaticMethod() << function()



    LuaSystemManager.mainHudActionQueue:AddRequest("EndingToast", function()
        local success = PhaseManager:OpenPhaseFast(PHASE_ID)
        if not success then
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "EndingToast")
        end
    end)
end

EndingToastCtrl.OnClose = HL.Override() << function(self)
end

HL.Commit(EndingToastCtrl)
