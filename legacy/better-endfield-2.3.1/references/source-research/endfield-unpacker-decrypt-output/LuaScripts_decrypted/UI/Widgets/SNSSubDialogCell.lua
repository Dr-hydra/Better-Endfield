local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSSubDialogCell = HL.Class('SNSSubDialogCell', UIWidgetBase)

SNSSubDialogCell.m_chatId = HL.Field(HL.String) << ""

SNSSubDialogCell.m_dialogId = HL.Field(HL.String) << ""

SNSSubDialogCell.m_onClickSubCellFunc = HL.Field(HL.Function)

SNSSubDialogCell.m_isTopic = HL.Field(HL.Boolean) << false


SNSSubDialogCell._OnFirstTimeInit = HL.Override() << function(self)







    self:RegisterMessage(MessageConst.ON_SNS_DIALOG_MODIFY, function(args)
        local chatId, dialogId, contentId = unpack(args)
        self:_OnSNSDialogModify(chatId, dialogId, contentId)
    end)

    self:RegisterMessage(MessageConst.ON_READ_SNS_DIALOG, function(args)
        local dialogId = unpack(args)
        self:_OnReadSNSDialog(dialogId)
    end)

    self.view.button.onClick:AddListener(function()
        self:_OnClickSubCell()
    end)
end

SNSSubDialogCell._OnEnable = HL.Override() << function(self)
    if self.m_isFirstTimeInit then
        return
    end

    self:_RefreshTopicRedDotState()
end

SNSSubDialogCell._OnSNSDialogModify = HL.Method(HL.String, HL.String, HL.Number) << function(self, chatId, dialogId, contentId)

    if not self.m_isTopic and dialogId ~= self.m_dialogId then
        return
    end


    if self.m_isTopic and chatId ~= self.m_chatId then
        return
    end

    self:_RefreshDialogInfo(contentId)
end

SNSSubDialogCell._OnReadSNSDialog = HL.Method(HL.String) << function(self, dialogId)
    if not self.m_isTopic then
        return
    end

    local succ, snsDialogCfg = Tables.sNSDialogTable:TryGetValue(dialogId)
    if not succ or snsDialogCfg.chatId ~= self.m_chatId then
        return
    end

    self:_RefreshTopicRedDotState()
end

SNSSubDialogCell._RefreshDialogInfo = HL.Method(HL.Opt(HL.Number)) << function(self, contentId)
    local isEnd
    local dialogId
    local content
    if self.m_isTopic then

        local chatInfo = GameInstance.player.sns.chatInfoDic:get_Item(self.m_chatId)

        isEnd = chatInfo.hasTopic and chatInfo.allTopicFinished

        local inProgressTopicId = chatInfo.inProgressTopicId
        if not string.isEmpty(inProgressTopicId) then
            dialogId = inProgressTopicId
        elseif chatInfo.hasTopicToStart then
            local chatCfg = Tables.sNSChatTable[self.m_chatId]
            local text = Language.LUA_SNS_TOPIC_CAN_TO_START
            if chatCfg.charGender == GEnums.SNSCharGender.Female then
                text = Language.LUA_SNS_TOPIC_CAN_TO_START_F
            elseif chatCfg.charGender == GEnums.SNSCharGender.Other then
                text = Language.LUA_SNS_TOPIC_CAN_TO_START_O
            end
            content = text
        else
            content = Language.LUA_SNS_TOPIC_ALL_FINISH
        end
    else

        isEnd = GameInstance.player.sns:DialogHasEnd(self.m_dialogId)

        dialogId = self.m_dialogId
    end

    if not string.isEmpty(dialogId) then

        local latestContent = SNSUtils.findLatestContent(dialogId)
        if not string.isEmpty(latestContent) then
            self.view.normalTxt:SetAndResolveTextStyle(latestContent)
            self.view.selectTxt:SetAndResolveTextStyle(latestContent)
        end
    elseif not string.isEmpty(content) then


        self.view.normalTxt.text = content
        self.view.selectTxt.text = content
    else
        logger.error("[sns] SNSSubDialogCell._RefreshDialogInfo no text", self.m_chatId, self.m_dialogId)
    end

    self.view.stateController:SetState(self.m_isTopic and "Topic" or "Normal")
    self.view.stateController:SetState(isEnd and "Finish" or "Progress")
end

SNSSubDialogCell._RefreshTopicRedDotState = HL.Method() << function(self)
    local redState = RedDotManager:GetRedDotState("SNSContactNpcCellTopic", self.m_chatId)
    local loopClip = self.view.topicNormalIcon.animationLoop
    if redState then



        self.view.topicNormalIcon:PlayLoopAnimation()
    else
        self.view.topicNormalIcon:SampleClip(loopClip.name, 0)
    end
end

SNSSubDialogCell._OnClickSubCell = HL.Method() << function(self)
    if self.m_onClickSubCellFunc then
        self.m_onClickSubCellFunc(self.m_chatId, self.m_dialogId, self)
    end
end

SNSSubDialogCell.InitSNSSubDialogCell = HL.Method(HL.String, HL.String, HL.Function)
        << function(self, chatId, dialogId, onClickSubCell)
    self:_FirstTimeInit()

    local dialogCfg = Tables.sNSDialogTable[dialogId]
    local isTopic = not string.isEmpty(dialogCfg.topicId)

    self.m_isTopic = isTopic
    self.m_chatId = chatId
    self.m_dialogId = dialogId
    self.m_onClickSubCellFunc = onClickSubCell

    self.view.gameObject.name = dialogId

    self:_RefreshDialogInfo()

    if isTopic then
        self.view.redDot.gameObject:SetActive(false)
        self:_RefreshTopicRedDotState()
    else
        self.view.redDot:InitRedDot("SNSNormalDialogSubCell", dialogId)
    end
end

SNSSubDialogCell.SetSelected = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, selected, isInit)
    if isInit == true then
        self.view.selectNode.gameObject:SetActiveIfNecessary(selected)
        self.view.normalNode.gameObject:SetActiveIfNecessary(not selected)
        if selected then
            self.view.animationWrapper:SampleToInAnimationEnd()
        else
            self.view.animationWrapper:SampleToOutAnimationEnd()
        end
    else
        if selected then
            self.view.animationWrapper:PlayInAnimation()
        else
            self.view.animationWrapper:PlayOutAnimation()
        end
    end
end


HL.Commit(SNSSubDialogCell)
return SNSSubDialogCell

