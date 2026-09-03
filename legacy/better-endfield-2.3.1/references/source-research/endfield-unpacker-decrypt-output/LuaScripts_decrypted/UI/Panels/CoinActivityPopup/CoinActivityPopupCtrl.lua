
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CoinActivityPopup

CoinActivityPopupCtrl = HL.Class('CoinActivityPopupCtrl', uiCtrl.UICtrl)





CoinActivityPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CoinActivityPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    local confirmButton = self.view.confirmButton
    confirmButton.onClick:AddListener(function()
        self:Close()
        if arg.onConfirm then
            arg.onConfirm()
        end
    end)

    local cancelButton = self.view.cancelButton
    cancelButton.onClick:AddListener(function()
        self:Close()
    end)
    self.view.setBtn.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            PhaseManager:OpenPhaseFast(PhaseId.GameSetting)
        end)
    end)

    local integralNumTxt = self.view.integralNumTxt
    integralNumTxt.text = arg.score and arg.score or "0"
end











HL.Commit(CoinActivityPopupCtrl)
