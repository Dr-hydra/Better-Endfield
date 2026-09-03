
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacUnlockFormulaToast

FacUnlockFormulaToastCtrl = HL.Class('FacUnlockFormulaToastCtrl', uiCtrl.UICtrl)






FacUnlockFormulaToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacUnlockFormulaToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.specialToast:InitToast()
end








FacUnlockFormulaToastCtrl.OnShowFormulaToast = HL.StaticMethod(HL.Any) << function(arg)
    local ctrl = FacUnlockFormulaToastCtrl.AutoOpen(PANEL_ID, nil, true)
    if ctrl == nil then
        return
    end

    local text = ""
    if type(arg) == "string" then
        text = arg
    else
        text = unpack(arg)
    end

    ctrl:ShowFormulaToast(text)
end

FacUnlockFormulaToastCtrl.ShowFormulaToast = HL.Method(HL.String) << function(self, text)
    self.view.specialToast.view.specialToastText:SetAndResolveTextStyle(text)
    self.view.specialToast:ShowToast()
end




HL.Commit(FacUnlockFormulaToastCtrl)
