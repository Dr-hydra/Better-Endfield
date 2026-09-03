local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

SNSContentVote = HL.Class('SNSContentVote', SNSContentBase)

SNSContentVote.m_voteResultCellCache = HL.Field(HL.Forward("UIListCache"))

SNSContentVote.m_headIconCacheDic = HL.Field(HL.Table)


SNSContentVote._OnFirstTimeInit = HL.Override() << function(self)
    self.m_voteResultCellCache = UIUtils.genCellCache(self.view.voteResultCell)
    self.m_headIconCacheDic = {}
end

SNSContentVote._OnSNSContentInit = HL.Override() << function(self)
    self.view.voteResultNode.gameObject:SetActive(false)
    self.view.titleTxt.color = self.view.config.TEXT_SPECIAL_COLOR
    self.view.icon.color = self.view.config.TEXT_SPECIAL_COLOR

    self.view.titleTxt.text = self.m_contentCfg.content

    local dialogId = self.m_contentInfo.dialogId
    local additiveCSIndex = self.m_contentInfo.additiveCSIndex
    if additiveCSIndex then

        self:ShowVoteResult(dialogId, additiveCSIndex, true)
    end
end

SNSContentVote._UpdateVoteResultCell = HL.Method(HL.Any, HL.Table, HL.String, HL.Number)
        << function(self, cell, voteInfo, selectOptionId, totalCount)
    local info = voteInfo
    local count = info.count or (info.chatIds and #info.chatIds) or 0
    local isSelected = voteInfo.id == selectOptionId
    if isSelected then
        count = count + 1
        cell.nameText.text = Language.LUA_SNS_VOTE_SELECTED .. info.name
    else
        cell.nameText.text = info.name
    end

    if self.m_headIconCacheDic[cell] == nil then
        self.m_headIconCacheDic[cell] = UIUtils.genCellCache(cell.iconCell)
    end
    local displayCount = (info.chatIds and #info.chatIds or 0) + (isSelected and 1 or 0)
    self.m_headIconCacheDic[cell]:Refresh(math.min(displayCount, 3), function(iconCell, iconIndex)
        if isSelected then
            if iconIndex == 1 then

                iconCell.headIcon.spriteName = SNSUtils.getEndminCharHeadIcon()
                return
            else
                iconIndex = iconIndex - 1
            end
        end
        local chatId = info.chatIds[iconIndex]

        local succ, chatTableData = Tables.sNSChatTable:TryGetValue(chatId)
        iconCell.headIcon.spriteName = succ and chatTableData.icon or ""
    end)

    if count > 3 then
        cell.numText.gameObject:SetActiveIfNecessary(true)
        cell.numText.text = "+" .. tostring(count - math.min(#info.chatIds, 3))
    else
        cell.numText.gameObject:SetActiveIfNecessary(false)
    end

    if isSelected then
        cell.bar.color = self.view.config.TEXT_SPECIAL_COLOR
    else
        cell.bar.color = self.view.config.TEXT_NORMAL_COLOR
    end
    cell.bar.fillAmount = count / totalCount
end

SNSContentVote.ShowVoteResult = HL.Method(HL.String, HL.Number, HL.Opt(HL.Boolean, HL.Function)).Return(HL.Number)
        << function(self, dialogId, additiveResultIndex, skipAnim, onCellSizeChange)
    self.view.titleTxt.color = self.view.config.TEXT_NORMAL_COLOR
    self.view.icon.color = self.view.config.TEXT_NORMAL_COLOR

    local dialogInfo = GameInstance.player.sns.dialogInfoDic:get_Item(dialogId)

    local contentLinkNode = dialogInfo.contentLink[additiveResultIndex]

    local contentCfg = Tables.sNSDialogTable[dialogId].dialogContentData[contentLinkNode.contentId]
    if contentCfg.optionType ~= GEnums.SNSDialogOptionType.Vote then
        logger.warn("SNSContentVote.ShowVoteResult but optionType ~= GEnums.SNSDialogOptionType.Vote")
        return -1
    end


    local voteInfos = {}

    local optionIds = contentCfg.dialogOptionIds
    local selectVoteId = contentLinkNode.optionResult
    local totalCount = 1

    for i = 0, optionIds.Count - 1 do
        local optionId = optionIds[i]
        local option = Tables.sNSDialogOptionTable[optionId]
        local voteInfo = {}
        voteInfo.id = optionId
        voteInfo.name = option.optionDesc
        voteInfo.chatIds = {}
        voteInfo.luaIndex = LuaIndex(i)
        for j = 0, option.optionNPCIds.Count - 1 do
            table.insert(voteInfo.chatIds, option.optionNPCIds[j])
        end
        voteInfo.count = string.isEmpty(option.optionNPCCount) and 0 or tonumber(option.optionNPCCount)
        totalCount = totalCount + math.max(voteInfo.count, #voteInfo.chatIds)
        table.insert(voteInfos, voteInfo)
    end

    local resultCellCount = math.min(#voteInfos, 3)
    if skipAnim then
        self.m_voteResultCellCache:Refresh(resultCellCount, function(cell, luaIndex)
            local voteInfo = voteInfos[luaIndex]
            self:_UpdateVoteResultCell(cell, voteInfo, selectVoteId, totalCount)
        end)
    else

        self.m_voteResultCellCache:Refresh(resultCellCount, function(cell, luaIndex)
            local voteInfo = voteInfos[luaIndex]
            self:_UpdateVoteResultCell(cell, voteInfo, selectVoteId, totalCount)
        end)
    end

    self.view.voteResultNode.gameObject:SetActive(true)
    return -1
end

SNSContentVote.IsTypeVote = HL.Override().Return(HL.Boolean) << function(self)
    return true
end

HL.Commit(SNSContentVote)
return SNSContentVote