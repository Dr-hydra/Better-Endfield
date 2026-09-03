
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DialogSkipPopUp
DialogSkipPopUpCtrl = HL.Class('DialogSkipPopUpCtrl', uiCtrl.UICtrl)







DialogSkipPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}

DialogSkipPopUpCtrl.m_needRecoverAuto = HL.Field(HL.Boolean) << false


DialogSkipPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    arg = arg or {}
    local confirmMessage = arg.confirmMessage or MessageConst.SKIP_DIALOG
    local cancelMessage = arg.cancelMessage or MessageConst.HIDE_DIALOG_SKIP_POP_UP

    self.view.confirmButton.onClick:RemoveAllListeners()
    self.view.confirmButton.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:_TryRecoverAutoMode()
            self:Notify(confirmMessage)
        end)
    end)

    self.view.cancelButton.onClick:RemoveAllListeners()
    self.view.cancelButton.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:_TryRecoverAutoMode()
            self:Notify(cancelMessage)
        end)
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end








DialogSkipPopUpCtrl.RefreshSummary = HL.Method(HL.Any) << function(self, summaryId)
    self.view.subText.text = Language.LUA_CONFIRM_SKIP_DIALOG
    if string.isEmpty(summaryId) then
        self.view.contentText.gameObject:SetActive(false)
    else
        local res, text = Tables.dialogSummaryTable:TryGetValue(summaryId)
        self.view.contentText.gameObject:SetActive(res)
        if res then
            self.view.contentText:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(text))
        end
    end
end

DialogSkipPopUpCtrl.SetCloseRecoverAuto = HL.Method() << function(self)
    self.m_needRecoverAuto = true
end

DialogSkipPopUpCtrl._TryRecoverAutoMode = HL.Method() << function(self)
    if self.m_needRecoverAuto then
        GameWorld.dialogManager:SetAutoMode(true)
        GameWorld.dialogTimelineManager:SetAutoMode(true)
    end
end

HL.Commit(DialogSkipPopUpCtrl)
