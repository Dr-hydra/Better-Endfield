
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SNSBarkerSide
local PHASE_ID = PhaseId.SNSBarkerSide
SNSBarkerSideCtrl = HL.Class('SNSBarkerSideCtrl', uiCtrl.UICtrl)

SNSBarkerSideCtrl.m_chatId = HL.Field(HL.String) << ""

SNSBarkerSideCtrl.m_dialogId = HL.Field(HL.String) << ""





SNSBarkerSideCtrl.s_messages = HL.StaticField(HL.Table) << {
}


SNSBarkerSideCtrl.InterruptForceSNS = HL.StaticMethod() << function()




end

SNSBarkerSideCtrl.OnForceDialogPanelOpen = HL.StaticMethod(HL.Any) << function(args)

    PhaseManager:OpenPhase(PHASE_ID, args, nil, true)
end


SNSBarkerSideCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickBtnClose()
    end)

    local chatId, dialogId = unpack(arg)
    self.m_chatId = chatId
    self.m_dialogId = dialogId

    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    end

    self.view.finishNode.gameObject:SetActive(false)
    self.view.snsDialogContentCore.view.optionsNode.gameObject:SetActive(false)
end

SNSBarkerSideCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self:StartDialog(self.m_chatId, self.m_dialogId)
end





SNSBarkerSideCtrl.OnClose = HL.Override() << function(self)
    UIManager:Close(PanelId.SNSNoticeForceToast)
    GameInstance.player.sns:EndForceDialog(false)
end

SNSBarkerSideCtrl.StartDialog = HL.Method(HL.String, HL.String) << function(self, chatId, dialogId)
    GameInstance.player.sns:ReadDialog(dialogId)
    self.view.snsDialogContentCore:InitSNSDialogContentCore(chatId, dialogId, function()
        AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_Options_Open")
        self.view.finishNode.gameObject:SetActive(true)
    end)
end

SNSBarkerSideCtrl._OnClickBtnClose = HL.Method() << function(self)
    self.view.snsDialogContentCore:OnClickSidePanelFinishBtn()
    PhaseManager:PopPhase(PHASE_ID)
end



SNSBarkerSideCtrl.ReturnToFocusCell = HL.Method() << function(self)

    self:SetNaviTarget(nil)
end

SNSBarkerSideCtrl.GetPanelType = HL.Method().Return(HL.Number) << function(self)
    return SNSUtils.PanelType.SidePanel
end



HL.Commit(SNSBarkerSideCtrl)
