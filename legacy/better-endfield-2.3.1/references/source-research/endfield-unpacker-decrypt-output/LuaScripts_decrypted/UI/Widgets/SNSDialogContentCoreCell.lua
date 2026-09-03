local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local SNSDialogContentType = GEnums.SNSDialogContentType

local LoadingState = {
    None = 0,
    Bubble = 1,
    ContentIn = 2,
    Interval = 3,
    Finish = 4,
}

local ContentType2WidgetName = {
    [SNSDialogContentType.Text] = "Text",
    [SNSDialogContentType.Image] = "Pic",
    [SNSDialogContentType.Sticker] = "Sticker",
    [SNSDialogContentType.Video] = "Video",
    [SNSDialogContentType.Voice] = "Voice",
    [SNSDialogContentType.Item] = "Item",
    [SNSDialogContentType.System] = "SystemMsg",
    [SNSDialogContentType.Card] = "Card",
    [SNSDialogContentType.PRTS] = "PRTS",
    [SNSDialogContentType.Vote] = "Vote",
    [SNSDialogContentType.Task] = "Task",
}

local EndLineWidgetName = "EndLine"

SNSDialogContentCoreCell = HL.Class('SNSDialogContentCoreCell', UIWidgetBase)

SNSDialogContentCoreCell.m_endLineWidget = HL.Field(HL.Any)

SNSDialogContentCoreCell.m_curActiveWidget = HL.Field(HL.Forward("SNSContentBase"))

SNSDialogContentCoreCell.m_contentType2Widget = HL.Field(HL.Table)

SNSDialogContentCoreCell.m_contentInfo = HL.Field(HL.Table)


SNSDialogContentCoreCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_contentType2Widget = {}
end

SNSDialogContentCoreCell.InitSNSDialogContentCoreCell = HL.Method(HL.Table) << function(self, contentInfo)
    self:_FirstTimeInit()
    self:_DisableAllWidgets()

    self.m_contentInfo = contentInfo

    local dialogId = contentInfo.dialogId

    local dialogCfg = Tables.sNSDialogTable[dialogId]
    local rawDialogCfg = dialogCfg.dialogContentData
    local contentCfg = rawDialogCfg[contentInfo.contentId]
    local contentType = contentCfg.contentType

    local isContentSystem = contentType == SNSDialogContentType.System or
            contentType == SNSDialogContentType.Task or
            contentCfg.isEnd
    local isSelf = contentCfg.speaker == Tables.sNSConst.myselfSpeaker
    local preContentId = contentInfo.preContentId
    local isFirst
    if not rawDialogCfg:ContainsKey(preContentId) then



        isFirst = true
    else
        isFirst = rawDialogCfg[preContentId].optionType ~= GEnums.SNSDialogOptionType.None or
                rawDialogCfg[preContentId].speaker ~= contentCfg.speaker
    end


    self.view.systemContentNode.gameObject:SetActive(isContentSystem)
    self.view.selfNode.gameObject:SetActive(not isContentSystem and isSelf)
    self.view.otherNode.gameObject:SetActive(not isContentSystem and not isSelf)

    if not isContentSystem then

        local node = isSelf and self.view.selfNode or self.view.otherNode

        local chatId = dialogCfg.chatId
        local chatCfg = Tables.sNSChatTable[chatId]
        local isGroup = chatCfg.chatType == GEnums.SNSChatType.Group
        local isGroupOwner = isGroup and chatCfg.owner == contentCfg.speaker

        node.isFirstPlaceholder.gameObject:SetActive(isFirst)
        node.headIconNode.gameObject:SetActive(isFirst)
        node.title.gameObject:SetActive(isFirst and isGroup)
        node.groupOwnerNode.gameObject:SetActive(isGroupOwner and isFirst)

        if isFirst then
            local speaker = contentCfg.speaker
            local chatCfg = isSelf and {} or Tables.sNSChatTable[speaker]
            local iconName = isSelf and SNSUtils.getEndminCharHeadIcon() or chatCfg.icon
            local nameText = isSelf and SNSUtils.getPlayerNameOrPlaceholder() or chatCfg.name
            node.headIcon:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, iconName)
            node.nameTxt.text = nameText
        end
    end

    if not contentInfo.isLoaded then
        if self.view.animationWrapper then
            self.view.animationWrapper:PlayInAnimation()
        end

        local loadingState
        if contentInfo.loadingState then
            loadingState = contentInfo.loadingState
        elseif not isSelf and not isContentSystem then

            loadingState = LoadingState.Bubble
        else

            loadingState = LoadingState.ContentIn
        end

        contentInfo.loadingState = loadingState
    else
        contentInfo.loadingState = LoadingState.Finish
        self:InitContent()
    end
end

SNSDialogContentCoreCell.ShowAdditiveResult = HL.Method(HL.String, HL.Number, HL.Boolean, HL.Function).Return(HL.Number)
        << function(self, dialogId, additiveResultIndex, skipAnim, onCellSizeChange)
    if not self.m_curActiveWidget then
        return -1
    end

    local duration
    if self.m_curActiveWidget:IsTypeVote() then

        local vote = self.m_curActiveWidget
        duration = vote:ShowVoteResult(dialogId, additiveResultIndex, skipAnim, function(csIndex)
            LayoutRebuilder.ForceRebuildLayoutImmediate(self.rectTransform)
            onCellSizeChange(csIndex, self.rectTransform.rect.height)
        end)
    elseif self.m_curActiveWidget:HasEmojiComp() then

        local withEmojiComp = self.m_curActiveWidget
        duration = withEmojiComp:ShowEmojiCommentResult(dialogId, additiveResultIndex, skipAnim, function(csIndex)
            LayoutRebuilder.ForceRebuildLayoutImmediate(self.rectTransform)
            onCellSizeChange(csIndex, self.rectTransform.rect.height)
        end)
    else
        logger.error("[sns] showAdditiveResult fail, type not support", self.m_contentInfo.dialogId,
                     self.m_contentInfo.contentId)
        duration = -1
    end
    return duration
end

SNSDialogContentCoreCell.ShowEmojiCommentOption = HL.Method() << function(self)
    if self.m_curActiveWidget then

        local widget = self.m_curActiveWidget
        widget:TryShowEmojiComment()
    end
end

SNSDialogContentCoreCell.ShowLoadingNode = HL.Method() << function(self)
    self.view.otherNode.loadingNode.gameObject:SetActive(true)
end

SNSDialogContentCoreCell.InitContent = HL.Method() << function(self)
    self.view.otherNode.loadingNode.gameObject:SetActive(false)

    local contentInfo = self.m_contentInfo
    local callback

    local dialogId = contentInfo.dialogId

    local rawDialogCfg = Tables.sNSDialogTable[dialogId].dialogContentData
    local contentCfg = rawDialogCfg[contentInfo.contentId]
    local contentType = contentCfg.contentType

    local isContentSystem = contentType == SNSDialogContentType.System or
            contentType == SNSDialogContentType.Task or
            contentCfg.isEnd
    local isSelf = contentCfg.speaker == Tables.sNSConst.myselfSpeaker

    local notifyCellSizeChange = function()
        LayoutRebuilder.ForceRebuildLayoutImmediate(self.rectTransform)
        Notify(MessageConst.ON_SNS_CONTENT_CORE_CELL_SIZE_CHANGED, { self.m_contentInfo.contentCellCSIndex,
                                                                     self.rectTransform.rect.height })
    end

    local audioEvent
    if isContentSystem then

        local node = self.view.systemContentNode
        if contentCfg.isEnd then
            if contentInfo.forceSystemMsg == true then
                local forceType = SNSDialogContentType.System

                local widget = self.m_contentType2Widget[forceType]
                if widget == nil then
                    widget = self:_CreateWidget(ContentType2WidgetName[forceType], node.transform)
                end
                self.m_contentType2Widget[forceType] = widget
                self.m_curActiveWidget = widget

                widget:InitSNSContentBase(contentInfo, callback, notifyCellSizeChange)
                widget:ManuallyUpdateSNSContentSystemMsg(Language[contentInfo.langKey])
            else
                if not self.m_endLineWidget then
                    self.m_endLineWidget = self:_CreateWidget(EndLineWidgetName, node.transform)
                end

                self.m_endLineWidget:InitSNSContentBase(contentInfo, callback, notifyCellSizeChange)
                self.m_curActiveWidget = self.m_endLineWidget
            end

        else

            local widget = self.m_contentType2Widget[contentType]
            if widget == nil then
                widget = self:_CreateWidget(ContentType2WidgetName[contentType], node.transform)
            end
            widget:InitSNSContentBase(contentInfo, callback, notifyCellSizeChange)

            self.m_contentType2Widget[contentType] = widget
            self.m_curActiveWidget = widget
        end
        if contentCfg.contentType == SNSDialogContentType.Task then
            audioEvent = "Au_UI_Popup_SNSDialogContent_Mission_Open"
        else
            audioEvent = "Au_UI_Event_SNSContentEndLine_Open"
        end
    else

        local node = isSelf and self.view.selfNode or self.view.otherNode

        local widget = self.m_contentType2Widget[contentType]
        if widget == nil then
            widget = self:_CreateWidget(ContentType2WidgetName[contentType], node.content.transform)
        else
            widget.transform:SetParent(node.content.transform)
        end
        widget:InitSNSContentBase(contentInfo, callback, notifyCellSizeChange)

        self.m_contentType2Widget[contentType] = widget
        self.m_curActiveWidget = widget

        audioEvent = isSelf and "Au_UI_Popup_SNSDialogContent_MyselfNode_Open" or
                "Au_UI_Popup_SNSDialogContent_OtherNode_Open"
    end

    if self.m_curActiveWidget then

        self.m_curActiveWidget.gameObject:SetActive(true)
    end

    if not contentInfo.isLoaded and not string.isEmpty(audioEvent) then
        AudioAdapter.PostEvent(audioEvent)
    end
end

SNSDialogContentCoreCell.GetLoadingTime = HL.Method(HL.Number).Return(HL.Number) << function(self, ratio)
    local contentInfo = self.m_contentInfo
    local dialogId = contentInfo.dialogId

    local rawDialogCfg = Tables.sNSDialogTable[dialogId].dialogContentData
    local contentCfg = rawDialogCfg[contentInfo.contentId]
    local contentType = contentCfg.contentType

    local isSelf = contentCfg.speaker == Tables.sNSConst.myselfSpeaker
    local loadingTime
    if isSelf then
        loadingTime = 0.1
    else
        if contentType == GEnums.SNSDialogContentType.Text then
            loadingTime = SNSUtils.getTextLoadingTime(contentCfg.content,
                                               self.config.MIN_LOADING_TIME,
                                               self.config.MAX_LOADING_TIME,
                                               self.config.MIN_STR_LENGTH,
                                               self.config.MAX_STR_LENGTH,
                                               self.config.LOADING_TIME_CURVE)
        else
            loadingTime = self.config.OTHER_WIDGET_LOADING_TIME
        end
    end
    return loadingTime / ratio
end

SNSDialogContentCoreCell.GetContentInTime = HL.Method(HL.Number).Return(HL.Number) << function(self, ratio)

    return 0.3 / ratio
end

SNSDialogContentCoreCell.GetIntervalTime = HL.Method(HL.Number).Return(HL.Number) << function(self, ratio)
    return self.config.INTERVAL_TIME_BETWEEN_CONTENT / ratio
end

SNSDialogContentCoreCell._CreateWidget = HL.Method(HL.String, HL.Any).Return(HL.Any)
        << function(self, widgetName, parentNode)
    local go = self:_CreateGameObject(widgetName, parentNode.transform)
    return Utils.wrapLuaNode(go)
end

SNSDialogContentCoreCell._CreateGameObject = HL.Method(HL.String, Transform).Return(GameObject)
        << function(self, widgetName, parentNode)
    local path = string.format(UIConst.UI_SNS_DIALOG_CONTENT_WIDGETS_PATH, widgetName)
    local goAsset = self:LoadGameObject(path)
    local go = CSUtils.CreateObject(goAsset, parentNode)
    go.transform.localScale = Vector3.one
    go.transform.localPosition = Vector3.zero
    go.transform.localRotation = Quaternion.identity

    return go
end

SNSDialogContentCoreCell._DisableAllWidgets = HL.Method() << function(self)
    for contentType, widget in pairs(self.m_contentType2Widget) do
        widget.gameObject:SetActive(false)
    end

    if self.m_endLineWidget then
        self.m_endLineWidget.gameObject:SetActive(false)
    end
end



SNSDialogContentCoreCell.CanSetTarget = HL.Method().Return(HL.Boolean) << function(self)
    if not self.m_curActiveWidget then
        return false
    end

    return self.m_curActiveWidget:GetNaviTarget() ~= nil
end

SNSDialogContentCoreCell.GetNaviTarget = HL.Method().Return(HL.Any) << function(self)
    if not self.m_curActiveWidget then
        return false
    end

    return self.m_curActiveWidget:GetNaviTarget()
end



HL.Commit(SNSDialogContentCoreCell)
return SNSDialogContentCoreCell

