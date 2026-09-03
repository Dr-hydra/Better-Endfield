local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

SNSContentWithEmojiComp = HL.Class('SNSContentWithEmojiComp', SNSContentBase)

SNSContentWithEmojiComp.m_emojiResultCellCache = HL.Field(HL.Forward("UIListCache"))

SNSContentWithEmojiComp.m_emojiCommentCellCache = HL.Field(HL.Forward("UIListCache"))


SNSContentWithEmojiComp._OnFirstTimeInit = HL.Override() << function(self)
    self.m_emojiResultCellCache = UIUtils.genCellCache(self.view.emojiResultCell)
    self.m_emojiCommentCellCache = UIUtils.genCellCache(self.view.emojiCommentCell)
end

SNSContentWithEmojiComp._OnSNSContentInit = HL.Override() << function(self)
    self:_InitEmojiComponent()
    local dialogId = self.m_contentInfo.dialogId
    local additiveCSIndex = self.m_contentInfo.additiveCSIndex
    if additiveCSIndex then
        self:ShowEmojiCommentResult(dialogId, additiveCSIndex, true)
    end
end

SNSContentWithEmojiComp._InitEmojiComponent = HL.Method() << function(self)
    local showResult = self.m_contentInfo.additiveCSIndex ~= nil
    self.view.emojiResultNode.gameObject:SetActive(showResult)

    self.view.emojiCommentListNode.gameObject:SetActive(false)

    if self.view.emojiCommentBubbleNode then
        local showBubble = self.m_contentInfo.showBubble == true and self.m_contentInfo.additiveCSIndex == nil
        if showBubble then
            self.view.emojiBubbleBtn.onClick:RemoveAllListeners()
            self.view.emojiBubbleBtn.onClick:AddListener(function()
                self.view.emojiCommentListNode.gameObject:SetActive(true)
                self.view.emojiCommentBubbleNode.gameObject:SetActive(false)
                AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_SelectEmoji_Open")
                if DeviceInfo.usingController then
                    self:SetNaviTarget(self.m_emojiCommentCellCache:Get(1).button)
                end
            end)

            self.view.emojiCommentListNode.onTriggerAutoClose:RemoveAllListeners()
            self.view.emojiCommentListNode.onTriggerAutoClose:AddListener(function()
                self.view.emojiCommentListNode.gameObject:SetActive(false)
                self.view.emojiCommentBubbleNode.gameObject:SetActive(true)
                AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_SelectEmoji_Close")
                if DeviceInfo.usingController then
                    self:SetNaviTarget(self.view.emojiBubbleBtn)
                end
            end)

            local args = self.m_contentInfo.showBubbleArgs
            local number = args.number
            local refreshFunc = args.refreshFunc
            self.m_emojiCommentCellCache:Refresh(number, function(cell, luaIndex)
                refreshFunc(cell, luaIndex)

                cell.button.onClick:AddListener(function()
                    self.view.emojiCommentListNode.gameObject:SetActive(false)
                end)
            end)
        end

        self.view.emojiCommentBubbleNode.gameObject:SetActive(showBubble)
    end
end

SNSContentWithEmojiComp._UpdateEmojiResultCell = HL.Method(HL.Any, HL.Table, HL.String) << function(self, cell, emojiInfo,
                                                                                           selectEmojiId)
    local info = emojiInfo
    local emojiId = info.emojiId
    cell.emoji:LoadSprite(UIConst.UI_SPRITE_SNS_EMOJI, emojiId)
    local text
    if emojiId == selectEmojiId then

        text = SNSUtils.getPlayerNameOrPlaceholder()
    else
        text = ""
    end
    for i, chatId in ipairs(info.chatIds) do

        local chatTableData = Tables.sNSChatTable[chatId]
        if i > 1 or emojiId == selectEmojiId then
            text = text .. Language.LUA_SNS_LIKE_NAME_SEPARATOR
        end
        text = text .. chatTableData.name
    end
    local count = info.count or 0
    if count > #info.chatIds then
        text = text .. string.format(Language.LUA_SNS_LIKE_NAME_MORE, count - #info.chatIds)
    end
    cell.resultTxt.text = text
end

SNSContentWithEmojiComp.TryShowEmojiComment = HL.Method() << function(self)
    AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_Options_Open")
    self:_InitEmojiComponent()
end

SNSContentWithEmojiComp.ShowEmojiCommentResult = HL.Method(HL.String, HL.Number, HL.Opt(HL.Boolean, HL.Function)).Return(HL.Number)
        << function(self, dialogId, additiveResultIndex, skipAnim, onCellSizeChange)

    self.view.emojiResultNode.gameObject:SetActive(true)
    self.view.emojiCommentListNode.gameObject:SetActive(false)
    self.view.emojiCommentBubbleNode.gameObject:SetActive(false)

    local rawDialogCfg = Tables.sNSDialogTable[dialogId].dialogContentData
    local dialogInfo = GameInstance.player.sns.dialogInfoDic:get_Item(dialogId)

    local contentLinkNode = dialogInfo.contentLink[additiveResultIndex]

    local contentCfg = rawDialogCfg[contentLinkNode.contentId]
    if contentCfg.optionType ~= GEnums.SNSDialogOptionType.EmojiComment and
            contentCfg.contentType ~= GEnums.SNSDialogContentType.EmojiResult then
        logger.error("SNSContentWithEmojiComp.ShowEmojiCommentResult but optionType ~= GEnums.SNSDialogOptionType.EmojiComment and contentType ~= GEnums.SNSDialogContentType.EmojiResult", dialogId, contentLinkNode.contentId)
        return -1
    end

    local getEmojiResultInfos = function(results, isOption)
        local emojiResultInfos = {}
        for i = 1, #results do
            local result = results[i]
            local emojiResultInfo = {}
            emojiResultInfo.emojiId = isOption and result.optionResPath or result.emojiResPath
            emojiResultInfo.chatIds = {}
            local chatIdStartIndex = isOption and 0 or 1
            local chatIdEndIndex = isOption and result.optionNPCIds.Count - 1 or #result.npcIds
            for j = chatIdStartIndex, chatIdEndIndex do
                table.insert(emojiResultInfo.chatIds, isOption and result.optionNPCIds[j] or result.npcIds[j])
            end
            emojiResultInfo.count = isOption and result.optionNPCCount or result.npcCount
            table.insert(emojiResultInfos, emojiResultInfo)
        end

        return emojiResultInfos
    end


    local results
    local isOption
    local selectEmojiId
    if contentCfg.optionType == GEnums.SNSDialogOptionType.EmojiComment then

        results = {}
        for i = 0, contentCfg.dialogOptionIds.Count - 1 do
            selectEmojiId = Tables.sNSDialogOptionTable[contentLinkNode.optionResult].optionResPath
            isOption = true
            local dialogOptionId = contentCfg.dialogOptionIds[i]
            local optionCfg = Tables.sNSDialogOptionTable[dialogOptionId]

            if optionCfg.optionNPCIds.Count ~= 0 or selectEmojiId == optionCfg.optionResPath then
                table.insert(results, Tables.sNSDialogOptionTable[dialogOptionId])
            end
        end
    else

        local jsonStr = contentCfg.contentParams
        results = Utils.stringJsonToTable(jsonStr)
        selectEmojiId = ""
        isOption = false
    end
    local emojiResultInfos = getEmojiResultInfos(results, isOption)

    local duration
    local count = #emojiResultInfos
    if skipAnim then
        self.m_emojiResultCellCache:Refresh(count, function(cell, luaIndex)
            local emojiInfo = emojiResultInfos[luaIndex]
            self:_UpdateEmojiResultCell(cell, emojiInfo, selectEmojiId)
        end)
        duration = -1
    else
        local waitTime = self.view.emojiResultCell.animationWrapper:GetInClipLength()
        self.m_emojiResultCellCache:GraduallyRefresh(count, waitTime, function(cell, luaIndex)
            local emojiInfo = emojiResultInfos[luaIndex]
            self:_UpdateEmojiResultCell(cell, emojiInfo, selectEmojiId)
            cell.animationWrapper:PlayInAnimation()
            AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_Emoji_Open")
            onCellSizeChange(self.m_contentInfo.contentCellCSIndex)
        end)
        duration = (count + 1) * waitTime + 1
    end
    return duration
end

SNSContentWithEmojiComp.HasEmojiComp = HL.Override().Return(HL.Boolean) << function(self)
    return true
end


SNSContentWithEmojiComp.CanSetTarget = HL.Override().Return(HL.Boolean) << function(self)
    return self.view.emojiCommentListNode.gameObject.activeInHierarchy or
            self.view.emojiCommentBubbleNode.gameObject.activeInHierarchy
end

SNSContentWithEmojiComp.GetNaviTarget = HL.Override().Return(HL.Any) << function(self)
    if self.view.emojiCommentBubbleNode.gameObject.activeInHierarchy then
        return self.view.emojiBubbleBtn
    end

    if self.view.emojiCommentListNode.gameObject.activeInHierarchy then
        return self.m_emojiCommentCellCache:Get(1).button
    end

    return nil
end



HL.Commit(SNSContentWithEmojiComp)
return SNSContentWithEmojiComp

