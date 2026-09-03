
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SNSHud
local RED_DOT_NAME = "SNSHudEntry"
SNSHudCtrl = HL.Class('SNSHudCtrl', uiCtrl.UICtrl)

SNSHudCtrl.m_chatId = HL.Field(HL.String) << ""

SNSHudCtrl.m_dialogId = HL.Field(HL.String) << ""

SNSHudCtrl.m_friendChat = HL.Field(HL.Boolean) << false

SNSHudCtrl.m_chatRoleId = HL.Field(HL.Number) << 0

SNSHudCtrl.m_effectLoopCor = HL.Field(HL.Thread)

SNSHudCtrl.m_newSNSNoticeQueue = HL.Field(HL.Forward("Queue"))

SNSHudCtrl.m_curSNSNoticeShowingData = HL.Field(HL.Table)





SNSHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SNS_NORMAL_DIALOG_ADD] = 'OnSNSNormalDialogAdd',


    [MessageConst.RECV_FRIEND_SEND_CHAT_NOTIFY] = 'OnSNSFriendChatAdd',
}


SNSHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_newSNSNoticeQueue = require_ex("Common/Utils/DataStructure/Queue")()
    self.view.entryBtn.onClick:AddListener(function()
        self:_OnClickEntryBtn()
    end)

    self.view.bgNode.onClick:AddListener(function()
        self:_OnClickJumpPhaseSNS()
    end)

    self.view.layout.onClick:AddListener(function()
        self:_OnClickJumpPhaseSNS()
    end)

    self.view.snsRedDot:InitRedDot(RED_DOT_NAME)
    self.view.noticeNode.gameObject:SetActive(false)
    self.view.entryBtn.gameObject:SetActive(self:_ShowEntryBtn())
end

SNSHudCtrl.OnShow = HL.Override() << function(self)
    if self.m_curSNSNoticeShowingData then
        return
    end
    self.view.noticeNode.gameObject:SetActive(false)
    self.view.entryBtn.gameObject:SetActive(self:_ShowEntryBtn())
end


SNSHudCtrl.OnClose = HL.Override() << function(self)
    if self.m_effectLoopCor then
        self.m_effectLoopCor = self:_ClearCoroutine(self.m_effectLoopCor)
    end
end

SNSHudCtrl._StartShowingNotice = HL.Method(HL.Table) << function(self, data)
    self.m_curSNSNoticeShowingData = data
    local dialogId = data.dialogId
    local chatRoleId = data.chatRoleId
    if not string.isEmpty(dialogId) then
        self:_ShowBarkerNotice(data)
    elseif not string.isEmpty(chatRoleId) then
        self:_ShowFriendNotice(data)
    end
end

SNSHudCtrl._OnOneNoticeShowingFinished = HL.Method() << function(self)
    if self.m_newSNSNoticeQueue:Empty() then
        self.m_curSNSNoticeShowingData = nil

        self.m_chatId = ""
        self.m_dialogId = ""
        self.m_friendChat = false
        self.m_chatRoleId = 0
        self:_OnNewSNSNoticeQueueEmpty()
    else
        local data = self.m_newSNSNoticeQueue:Pop()
        self:_StartShowingNotice(data)
    end
end

SNSHudCtrl._CustomPlayAudio = HL.Method(HL.String) << function(self, audioEvent)
    if not UIManager:IsShow(PANEL_ID) then
        return
    end
    AudioAdapter.PostEvent(audioEvent)
end

SNSHudCtrl._ShowBarkerNotice = HL.Method(HL.Table) << function(self, data)
    RedDotManager:TriggerUpdate(RED_DOT_NAME)
    local chatId = data.chatId
    local dialogId = data.dialogId

    self.m_chatId = chatId
    self.m_dialogId = dialogId

    local chatCfg = Tables.sNSChatTable[chatId]
    local dialogCfg = Tables.sNSDialogTable[dialogId]
    if not string.isEmpty(dialogCfg.topicId) then

        self.view.newTopicIcon.gameObject:SetActive(true)
        self.view.newMsgHeadIcon.gameObject:SetActive(false)
        local snsTopicCfg = Tables.sNSDialogTopicTable[dialogCfg.topicId]
        self.view.newMsgTxt.text = string.format(Language.LUA_SNS_NEW_TOPIC_DIALOG_DESC_FORMAT, snsTopicCfg.topicName)


        self.m_dialogId = GameInstance.player.sns.chatInfoDic:get_Item(chatId).topicDialogUniqueId
    else
        local text
        if not string.isEmpty(dialogCfg.relatedMissionId) then

            text = Language.LUA_SNS_NEW_MISSION_DIALOG_DESC
        else
            text = chatCfg.chatType == GEnums.SNSChatType.Group and Language.LUA_SNS_NOTICE_GROUP_MSG_DESC
                    or Language.LUA_SNS_NOTICE_PERSON_MSG_DESC
        end
        self.view.newTopicIcon.gameObject:SetActive(false)
        self.view.newMsgHeadIcon.gameObject:SetActive(true)
        self.view.newMsgTxt.text = text

        self.view.newMsgHeadIcon:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, chatCfg.icon)
    end

    self.view.barkerHeadRoot.gameObject:SetActive(true)
    self.view.friendHeadRoot.gameObject:SetActive(false)

    self.view.entryBtn.gameObject:SetActive(false)
    self.view.noticeNode.gameObject:SetActive(true)
    local anim = self.view.noticeNewMsg
    if anim then
        local loopTimes = self.view.config.NOTICE_LOOP_TIMES
        local loopClipLength = anim:GetLoopClipLength()
        local inClipLength = anim:GetInClipLength()
        local outClipLength = anim:GetOutClipLength()

        self:_CustomPlayAudio("Au_UI_Popup_SNSNoticeHudPanel_Open")
        self.m_effectLoopCor = self:_StartCoroutine(function()
            coroutine.wait(inClipLength)
            for i = 1, loopTimes do
                self:_CustomPlayAudio("Au_UI_Event_SNSNoticeHudPanel_Breathe")
                coroutine.wait(loopClipLength)
            end
            anim:PlayOutAnimation()
            coroutine.wait(outClipLength)

            self.view.noticeNode.gameObject:SetActive(false)
            self.view.entryBtn.gameObject:SetActive(self:_ShowEntryBtn())
            self.m_chatId = ""
            self.m_dialogId = ""
            self:_OnOneNoticeShowingFinished()
        end)
    end
end

SNSHudCtrl._ShowFriendNotice = HL.Method(HL.Table) << function(self, data)
    RedDotManager:TriggerUpdate(RED_DOT_NAME)


    self.view.entryBtn.gameObject:SetActive(false)
    self.view.noticeNode.gameObject:SetActive(true)


    self.view.barkerHeadRoot.gameObject:SetActive(false)
    self.view.friendHeadRoot.gameObject:SetActive(true)

    self.view.newMsgTxt.text = Language.LUA_CHAT_NEW_MESSAGE_MAIN_HUD_TIP_TEXT
    self.m_chatRoleId = data.chatRoleId
    self.m_friendChat = true

    self.m_effectLoopCor = self:_StartCoroutine(function()
        coroutine.wait(self.view.config.NOTICE_DURATION)
        self.view.noticeNode.gameObject:SetActive(false)
        self.view.entryBtn.gameObject:SetActive(self:_ShowEntryBtn())
        self.m_chatRoleId = 0
        self.m_friendChat = false
        self:_OnOneNoticeShowingFinished()
    end)
end

SNSHudCtrl._OnClickEntryBtn = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.SNS)
end

SNSHudCtrl._OnClickJumpPhaseSNS = HL.Method() << function(self)
    local args
    if self.m_friendChat then
        args = { roleId = self.m_chatRoleId }
    else
        args = { dialogId = self.m_dialogId }
    end

    PhaseManager:OpenPhase(PhaseId.SNS, args, function()
        self.m_friendChat = false
        self.m_chatRoleId = 0
        self.m_chatId = ""
        self.m_dialogId = ""

        self.m_curSNSNoticeShowingData = nil
        self.m_newSNSNoticeQueue:Clear()
        self:_OnNewSNSNoticeQueueEmpty()
    end)
end

SNSHudCtrl._ShowEntryBtn = HL.Method().Return(HL.Boolean) << function(self)

    if DeviceInfo.usingController then
        return false
    end

    if not PhaseManager:IsPhaseUnlocked(PhaseId.SNS) then
        return false
    end



    return true
end

SNSHudCtrl._OnNewSNSNoticeQueueEmpty = HL.Method() << function(self)
    ClientDataManagerInst:SaveUserData("SNS")
end

SNSHudCtrl.OnSNSNormalDialogAdd = HL.Method(HL.Any) << function(self, args)
    if not PhaseManager:IsPhaseUnlocked(PhaseId.SNS) then

        return
    end


    if Utils.isForbidden(ForbidType.HideSNSHud) then
        return
    end

    local _, dialogId = unpack(args)
    local noticeType = Tables.sNSDialogTable[dialogId].noticeType
    if noticeType == GEnums.SNSNewDialogNoticeType.None then
        return
    end

    local chatId, dialogId = unpack(args)
    local data = {
        chatId = chatId,
        dialogId = dialogId
    }

    LuaSystemManager.mainHudActionQueue:AddRequest("SNSNormalNotice", function()

        local succ, self = UIManager:IsOpen(PANEL_ID)
        if not succ then
            return
        end

        if self:IsShow() and not self.m_curSNSNoticeShowingData then
            self:_StartShowingNotice(data)
        else
            self.m_newSNSNoticeQueue:Push(data)
        end
    end)
end

SNSHudCtrl.OnSNSForceDialogAdd = HL.Method(HL.Any) << function(self, args)
    if not PhaseManager:IsPhaseUnlocked(PhaseId.SNS) then
        return
    end

    RedDotManager:TriggerUpdate(RED_DOT_NAME)
end

SNSHudCtrl.OnSNSFriendChatAdd = HL.Method(HL.Any) << function(self, args)
    if not PhaseManager:IsPhaseUnlocked(PhaseId.SNS) then
        return
    end

    local chatRoleId = unpack(args)
    local data = {
        chatRoleId = chatRoleId,
    }

    LuaSystemManager.mainHudActionQueue:AddRequest("SNSNormalNotice", function()
        if self.m_friendChat then

            return
        end


        local succ, self = UIManager:IsOpen(PANEL_ID)
        if not succ then
            return
        end

        local chatInfo = GameInstance.player.friendChatSystem:GetChatInfo(data.chatRoleId)
        if not chatInfo then

            return
        end
        if chatInfo and chatInfo.unReadNum == 0 then

            return
        end

        if self:IsShow() and not self.m_curSNSNoticeShowingData then
            self:_StartShowingNotice(data)
        else
            self.m_newSNSNoticeQueue:Push(data)
        end
    end)
end

HL.Commit(SNSHudCtrl)
