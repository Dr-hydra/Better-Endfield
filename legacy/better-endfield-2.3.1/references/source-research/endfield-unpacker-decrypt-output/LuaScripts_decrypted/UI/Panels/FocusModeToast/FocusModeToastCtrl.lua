
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FocusModeToast

FocusModeToastCtrl = HL.Class('FocusModeToastCtrl', uiCtrl.UICtrl)






FocusModeToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FocusModeToastCtrl.ShowToast = HL.StaticMethod(HL.Table) << function(arg)
    local isStart = unpack(arg)
    UIManager:Close(PANEL_ID)
    UIManager:AutoOpen(PANEL_ID, isStart)
end


FocusModeToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.txtToast.text = arg and
        Language.LUA_ENTER_FOCUS_MODE_TOAST or Language.LUA_EXIT_FOCUS_MODE_TOAST
    self:_StartTimer(self.view.config.SHOW_DURATION, function()
        self:PlayAnimationOutAndClose()
    end)
end

HL.Commit(FocusModeToastCtrl)
