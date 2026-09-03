
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SNSNoticeForceToast

SNSNoticeForceToastCtrl = HL.Class('SNSNoticeForceToastCtrl', uiCtrl.UICtrl)

SNSNoticeForceToastCtrl.m_chatId = HL.Field(HL.String) << ""

SNSNoticeForceToastCtrl.m_dialogId = HL.Field(HL.String) << ""

SNSNoticeForceToastCtrl.OnShowSNSNewDialogToast = HL.StaticMethod(HL.Table) << function(arg)
    SNSNoticeForceToastCtrl.AutoOpen(PANEL_ID, arg, true)
end





SNSNoticeForceToastCtrl.s_messages = HL.StaticField(HL.Table) << {
}


SNSNoticeForceToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local chatId, dialogId = unpack(arg)

    self.m_chatId = chatId
    self.m_dialogId = dialogId

    self:_InitInfo()
end








SNSNoticeForceToastCtrl._InitInfo = HL.Method() << function(self)
    local chatCfg = Tables.sNSChatTable[self.m_chatId]
    local str = string.format(Language.LUA_SNS_FORCE_DIALOG_NOTICE_MSG, chatCfg.name)
    local icon = chatCfg.icon

    self.view.descTxt.text = str
    self.view.headIcon:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, icon)

    self.view.newTownTalk.gameObject:SetActive(false)
end

HL.Commit(SNSNoticeForceToastCtrl)
