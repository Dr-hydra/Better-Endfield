
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonBottomToast

CommonBottomToastCtrl = HL.Class('CommonBottomToastCtrl', uiCtrl.UICtrl)






CommonBottomToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}

CommonBottomToastCtrl.OnShowToast = HL.StaticMethod(HL.Any) << function(text)
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if type(text) == "string" then
        text = (text)
    else
        text = unpack(text)
    end
    if isOpen then
        UIManager:Close(PANEL_ID)
    end
    UIManager:Open(PANEL_ID, text)
end

CommonBottomToastCtrl.OnCloseToast = HL.StaticMethod() << function()
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        ctrl.view.timeHint:Play("commontoastnew_out", function()
            UIManager:Close(PANEL_ID)
        end)
    end

end


CommonBottomToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.text.text = arg
end














HL.Commit(CommonBottomToastCtrl)
