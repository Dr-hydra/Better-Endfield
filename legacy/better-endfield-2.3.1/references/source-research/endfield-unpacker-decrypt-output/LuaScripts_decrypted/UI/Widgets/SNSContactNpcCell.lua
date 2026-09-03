local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSContactNpcCell = HL.Class('SNSContactNpcCell', UIWidgetBase)

SNSContactNpcCell.m_subDialogCellCache = HL.Field(HL.Forward("UIListCache"))

SNSContactNpcCell.m_isFoldOut = HL.Field(HL.Boolean) << false

SNSContactNpcCell.m_onNpcCellClick = HL.Field(HL.Function)

SNSContactNpcCell.m_chatVO = HL.Field(HL.Table)


SNSContactNpcCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_subDialogCellCache = UIUtils.genCellCache(self.view.subDialogCell)

    self.view.foldOut.onClick:AddListener(function()
        self:_OnFoldButtonClick()
    end)

    self.view.foldBtn.onClick:AddListener(function()
        self:_OnFoldButtonClick()
    end)
end

SNSContactNpcCell.InitSNSContactNpcCell = HL.Method(HL.Table, HL.Boolean, HL.Function, HL.Function)
        << function(self, chatVO, defaultFoldOut, onNpcCellClick, dialogCellRefreshFunc)
    self:_FirstTimeInit()

    self.m_chatVO = chatVO
    self.m_onNpcCellClick = onNpcCellClick

    local chatId = chatVO.chatId
    local chatCfg = Tables.sNSChatTable[chatId]
    local hasChatInfo, chatInfo = GameInstance.player.sns.chatInfoDic:TryGetValue(chatId)

    self.view.topicTips.gameObject:SetActive(hasChatInfo and chatInfo.hasTopicToStart)

    self.view.name:SetAndResolveTextStyle(chatCfg.name)
    self.view.icon:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, chatCfg.icon)

    local isGroup = chatCfg.chatType == GEnums.SNSChatType.Group
    self.view.official.gameObject:SetActiveIfNecessary(isGroup and chatCfg.tagType == GEnums.SNSGroupDialogTagType.Official)
    self.view.external.gameObject:SetActiveIfNecessary(isGroup and chatCfg.tagType == GEnums.SNSGroupDialogTagType.External)

    self.view.redDot:InitRedDot("SNSContactNpcCell", chatId)
    self.view.topicRedDot:InitRedDot("SNSContactNpcCellTopic", chatId)

    local subDialogVOs = self:_GenSubDialogCellVOs()
    local isSettlementChannel = isGroup and chatCfg.isSettlementChannel
    local count = isSettlementChannel and 1 or #subDialogVOs
    self.m_subDialogCellCache:Refresh(count, function(cell, index)
        local dialogVO = subDialogVOs[index]
        dialogCellRefreshFunc(cell, chatId, dialogVO.dialogId, index)
    end)

    self.m_isFoldOut = defaultFoldOut
    self:_RefreshFoldOutIcon()
end

SNSContactNpcCell.ToggleFoldOut = HL.Method() << function(self)
    self.m_isFoldOut = not self.m_isFoldOut
    self:_RefreshFoldOutIcon()
end

SNSContactNpcCell._OnFoldButtonClick = HL.Method() << function(self)
    if self.m_onNpcCellClick then
        self.m_onNpcCellClick()
    end

    self:ToggleFoldOut()
end

SNSContactNpcCell._RefreshFoldOutIcon = HL.Method() << function(self)
    self.view.foldIconUp.gameObject:SetActiveIfNecessary(self.m_isFoldOut)
    self.view.foldIconDown.gameObject:SetActiveIfNecessary(not self.m_isFoldOut)
end

SNSContactNpcCell._GenSubDialogCellVOs = HL.Method().Return(HL.Table) << function(self)
    local sns = GameInstance.player.sns

    local dialogVOs = {}
    for _, dialogId in pairs(self.m_chatVO.dialogIds) do
        local dialogInfo = sns.dialogInfoDic:get_Item(dialogId)
        local dialogCfg = Tables.sNSDialogTable[dialogId]

        if string.isEmpty(dialogCfg.topicId) then
            local dialogVO = {}
            dialogVO.dialogId = dialogId
            dialogVO.timestamp = dialogInfo.timestamp
            dialogVO.sortId1 = dialogInfo.isRead and 0 or 1
            dialogVO.sortId2 = dialogInfo.isEnd and 0 or 1
            table.insert(dialogVOs, dialogVO)
        end
    end
    table.sort(dialogVOs, Utils.genSortFunction({ "sortId1", "sortId2", "timestamp" }))



    if self.m_chatVO.hasTopic then
        local chatInfo = sns.chatInfoDic:get_Item(self.m_chatVO.chatId)
        table.insert(dialogVOs, 1, {
            dialogId = chatInfo.topicDialogUniqueId,
        })
    end

    return dialogVOs
end

HL.Commit(SNSContactNpcCell)
return SNSContactNpcCell

