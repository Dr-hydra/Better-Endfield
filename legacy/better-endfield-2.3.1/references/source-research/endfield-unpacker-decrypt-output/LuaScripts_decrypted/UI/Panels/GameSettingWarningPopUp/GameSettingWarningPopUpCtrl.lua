local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GameSettingWarningPopUp

GameSettingWarningPopUpCtrl = HL.Class('GameSettingWarningPopUpCtrl', uiCtrl.UICtrl)






GameSettingWarningPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GameSettingWarningPopUpCtrl.m_onForceConfirm = HL.Field(HL.Function)

GameSettingWarningPopUpCtrl.m_onConfirm = HL.Field(HL.Function)


GameSettingWarningPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.forceConfirmButton.onClick:AddListener(function()
        self:_OnClickButton(self.m_onForceConfirm)
    end)
    self.view.confirmButton.onClick:AddListener(function()
        self:_OnClickButton(self.m_onConfirm)
    end)


    self:BindInputPlayerAction("common_cancel_no_hint", function()
        AudioAdapter.PostEvent("Au_UI_Button_Cancel")
        self:PlayAnimationOutAndClose()
    end)

    self.m_onForceConfirm = arg.onForceConfirm
    self.m_onConfirm = arg.onConfirm

    self.view.contentText.text = arg.content or ""
    self.view.warningContentText:SetAndResolveTextStyle(arg.warningContent or "")

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GameSettingWarningPopUpCtrl._OnClickButton = HL.Method(HL.Function) << function(self, callback)
    if callback then
        callback()
    end
    self:PlayAnimationOutAndClose()
end

HL.Commit(GameSettingWarningPopUpCtrl)
