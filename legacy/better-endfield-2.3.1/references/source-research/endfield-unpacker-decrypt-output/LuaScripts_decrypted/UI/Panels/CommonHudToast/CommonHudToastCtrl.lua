
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonHudToast
CommonHudToastCtrl = HL.Class('CommonHudToastCtrl', uiCtrl.UICtrl)

CommonHudToastCtrl.s_isInMainHud = HL.StaticField(HL.Boolean) << false

CommonHudToastCtrl.m_specialToastText = HL.Field(HL.String) << ""

CommonHudToastCtrl.m_battleWarningToastText = HL.Field(HL.String) << ""





CommonHudToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CommonHudToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.mailToast:InitToast()
    self.view.specialToast:InitToast()
    self.view.battleWarningToast:InitToast()

    self.view.mailToast.view.mailButton.onClick:AddListener(function()
        self:_OnMailButtonClick()
    end)
end











CommonHudToastCtrl.OnEnterMainHud = HL.StaticMethod() << function()
    CommonHudToastCtrl.AutoOpen(PANEL_ID, nil, true)
end

CommonHudToastCtrl.OnShowMailToast = HL.StaticMethod() << function()
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Mail) then
        return
    end

    if FocusModeUtils.isInFocusMode then
        return
    end

    local ctrl = CommonHudToastCtrl.AutoOpen(PANEL_ID, nil, true)
    if ctrl == nil then
        return
    end

    ctrl:ShowMailToast()
end

CommonHudToastCtrl.OnShowSpecialToast = HL.StaticMethod(HL.Any) << function(arg)
    local ctrl = CommonHudToastCtrl.AutoOpen(PANEL_ID, nil, true)
    if ctrl == nil then
        return
    end

    local text = ""
    if type(arg) == "string" then
        text = arg
    else
        text = unpack(arg)
    end

    ctrl.m_specialToastText = text

    ctrl:ShowSpecialToast()
end

CommonHudToastCtrl.OnShowBattleWarningToast = HL.StaticMethod(HL.Any) << function(arg)
    local ctrl = CommonHudToastCtrl.AutoOpen(PANEL_ID, nil, true)
    if ctrl == nil then
        return
    end

    local text = ""
    if type(arg) == "string" then
        text = arg
    else
        text = unpack(arg)
    end

    ctrl.m_battleWarningToastText = text

    ctrl:ShowBattleWarningToast()
end

CommonHudToastCtrl._OnMailButtonClick = HL.Method() << function(self)
    self.view.mailToast:HideToast()

    PhaseManager:OpenPhase(PhaseId.Mail)
end

CommonHudToastCtrl.ShowMailToast = HL.Method() << function(self)
    self.view.mailToast:ShowToast()
    AudioManager.PostEvent("au_ui_mail_receive")
end

CommonHudToastCtrl.ShowSpecialToast = HL.Method() << function(self)
    local specialToast = self.view.specialToast
    if specialToast == nil then
        return
    end

    specialToast.view.specialToastText:SetAndResolveTextStyle(self.m_specialToastText)
    specialToast:ShowToast()
end

CommonHudToastCtrl.ShowBattleWarningToast = HL.Method() << function(self)
    local battleWarningToast = self.view.battleWarningToast
    battleWarningToast.view.warningTxt:SetAndResolveTextStyle(self.m_battleWarningToastText)
    battleWarningToast:ShowToast()
end

HL.Commit(CommonHudToastCtrl)
