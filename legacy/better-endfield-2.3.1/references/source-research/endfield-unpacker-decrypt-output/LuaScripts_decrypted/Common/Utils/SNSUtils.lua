local SNSUtils = {}

SNSUtils.UI_SNS_EMOJI_PATH = "SNS/Emoji/"
SNSUtils.UI_SNS_EMOJI_FORMAT = "<image=\"%s\">"

SNSUtils.PIC_STANDARD_SIZE = Vector2(480, 270)

SNSUtils.PanelType = {
    SidePanel = 1,
    FullScreenPanel = 2,
}

SNSUtils.SNS_CATEGORY = "SNS"
SNSUtils.NORMAL_TAB_READ = "normal_tab_read"
SNSUtils.MISSION_TAB_READ = "mission_tab_read"




function SNSUtils.processDialogIds(chatId, dialogId, isTopic)
    local resultIds = {}

    local chatCfg = Tables.sNSChatTable[chatId]
    if chatCfg.isSettlementChannel then

        local sns = GameInstance.player.sns
        local dialogVOs = {}
        local chatInfo = sns.chatInfoDic:get_Item(chatId)
        for _, excludeMissionDialogId in pairs(chatInfo.dialogIds) do
            local dialogInfo = sns.dialogInfoDic:get_Item(excludeMissionDialogId)
            table.insert(dialogVOs, {
                dialogId = excludeMissionDialogId,
                timestamp = dialogInfo.timestamp,
                sortId1 = dialogInfo.isRead and 0 or 1,
                sortId2 = dialogInfo.isEnd and 0 or 1,
            })
        end
        table.sort(dialogVOs, Utils.genSortFunction({ "sortId1", "sortId2", "timestamp" }))

        for i = #dialogVOs, 1, -1 do


            local dialogId = dialogVOs[i].dialogId
            local latestDialogId = resultIds[#resultIds]
            local validate = latestDialogId == nil or
                    sns.dialogInfoDic:ContainsKey(latestDialogId) and sns.dialogInfoDic:get_Item(latestDialogId).isEnd
            if validate then
                table.insert(resultIds, dialogId)
            end
        end
    elseif isTopic then


        local dialogVOs = {}
        local sns = GameInstance.player.sns

        local chatInfo = sns.chatInfoDic:get_Item(chatId)
        local dialogInfoDic = sns.dialogInfoDic
        for _, topicDialogId in pairs(chatInfo.topicDialogIds) do
            local dialogInfo = dialogInfoDic:get_Item(topicDialogId)
            if dialogInfo.isEnd or dialogInfo.isReading then
                table.insert(dialogVOs, {
                    dialogId = topicDialogId,
                    finishTimestamp = dialogInfo.finishTimestamp,
                    sortId1 = dialogInfo.isRead and 0 or 1,
                    sortId2 = dialogInfo.isEnd and 0 or 1,
                })
            end
        end
        table.sort(dialogVOs, Utils.genSortFunction({ "sortId1", "sortId2", "finishTimestamp" }))

        for i = #dialogVOs, 1, -1 do
            table.insert(resultIds, dialogVOs[i].dialogId)
        end
    else
        table.insert(resultIds, dialogId)
    end

    return resultIds
end

function SNSUtils.getPlayerNameOrPlaceholder()
    local playerName = Utils.getPlayerName()
    playerName = string.isEmpty(playerName) and Language.LUA_SNS_ENDMIN_NAME_PLACEHOLDER or playerName
    return playerName
end

function SNSUtils.resolveTextPlayerName(text)
    local targetText = string.gsub(text, UIConst.PLAYER_NAME_FORMATTER, SNSUtils.getPlayerNameOrPlaceholder())
    return targetText
end

function SNSUtils.resolveEmojiFormat(text)
    local path = SNSUtils.UI_SNS_EMOJI_PATH
    return string.gsub(text, "<image=\"(.-)\">", function(emojiName)
        if string.sub(emojiName, 1, string.len(path)) == path then

            return string.format(SNSUtils.UI_SNS_EMOJI_FORMAT, emojiName)
        else
            return string.format(SNSUtils.UI_SNS_EMOJI_FORMAT, path..emojiName)
        end
    end)
end

function SNSUtils.resolveTextStyleWithPlayerName(text)

    local withPlayerName = SNSUtils.resolveTextPlayerName(text)

    local processEmoji = SNSUtils.resolveEmojiFormat(withPlayerName)

    local returnContent = UIUtils.resolveTextGender(processEmoji)
    return returnContent
end

function SNSUtils.getEndminCharHeadIcon()
    local curEndminCharTemplateId = CS.Beyond.Gameplay.CharUtils.curEndminCharTemplateId
    return UIConst.UI_CHAR_HEAD_PREFIX .. curEndminCharTemplateId
end

function SNSUtils.getDiffPicNameByGender(contentParam)
    if contentParam.Count == 0 then
        logger.warn("no pic name")
        return ""
    end


    if contentParam.Count == 1 then
        return contentParam[0]
    end

    local gender = Utils.getPlayerGender()
    return gender == CS.Proto.GENDER.GenMale and contentParam[0] or contentParam[1]
end

function SNSUtils.getTextLoadingTime(content, minTime, maxTime, minStrLength, maxStrLength, curve)

    local content = string.gsub(content, "(<image=.->)", function(emoji)
        return "_"
    end)

    local contentLength = string.utf8len(content)
    if contentLength >= maxStrLength then
        return maxTime
    end

    if contentLength <= minStrLength then
        return minTime
    end

    local relative = (contentLength - minStrLength) / (maxStrLength - minStrLength)
    local eva = curve:Evaluate(relative)
    return eva * (maxTime - minTime) + minTime
end

function SNSUtils.getFirstContent(dialogId)
    if not Tables.sNSDialogTable:ContainsKey(dialogId) then
        logger.error(string.format("sns sNSDialogTable doesn't have dialogId:%s", dialogId))
        return ""
    end

    local dialogCfg = Tables.sNSDialogTable[dialogId]
    local dialogContents = dialogCfg.dialogContentData

    return dialogContents[Tables.sNSConst.snsDialogStartContentId].content
end

function SNSUtils.findLatestContent(dialogId)
    if not GameInstance.player.sns.dialogInfoDic:ContainsKey(dialogId) then

        logger.error(string.format("sns dialogInfoDic doesn't have dialogId:%s", dialogId))
        return ""
    end

    if not Tables.sNSDialogTable:ContainsKey(dialogId) then

        logger.error(string.format("sns sNSDialogTable doesn't have dialogId:%s", dialogId))
        return ""
    end

    local dialogInfo = GameInstance.player.sns.dialogInfoDic:get_Item(dialogId)
    local dialogCfg = Tables.sNSDialogTable[dialogId]
    local dialogContents = dialogCfg.dialogContentData

    if not Tables.sNSChatTable:ContainsKey(dialogCfg.chatId) then
        logger.error(string.format("sns sNSChatTable doesn't have chatId:%s", dialogCfg.chatId))
        return ""
    end

    local curContentId = dialogInfo.pureContent and Tables.sNSConst.snsDialogStartContentId or dialogInfo.curContentId
    if not dialogContents:ContainsKey(curContentId) then
        logger.error(string.format("sns dialogInfo:%s doesn't have curContentId:%s", dialogId, curContentId))
        return ""
    end

    local curContent = dialogContents[curContentId]

    local contentInValid = function(content)
        return content.isEnd or (content.optionType ~= GEnums.SNSDialogOptionType.None and content.contentId ~= Tables.sNSConst.snsDialogStartContentId) or
                (content.optionType == GEnums.SNSDialogOptionType.None and content.contentType == GEnums.SNSDialogContentType.EmojiResult)
    end

    local inValid = contentInValid(curContent)
    while inValid do
        if not dialogContents:ContainsKey(curContent.preContentId) then
            logger.error(string.format("sns dialogInfo:%s content:%s doesn't have valid preContentId:%s", dialogId,
                                       curContent.contentId, curContent.preContentId))
            return ""
        end
        curContent = dialogContents[curContent.preContentId]
        inValid = contentInValid(curContent)
    end

    local contentPrefix = ""
    local content = ""
    local curContentType = curContent.contentType
    local chatCfg = Tables.sNSChatTable[dialogCfg.chatId]
    local isGroup = chatCfg.chatType == GEnums.SNSChatType.Group

    if curContent.optionType ~= GEnums.SNSDialogOptionType.None then

        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_WRITING
    elseif curContentType == GEnums.SNSDialogContentType.Text then
        content = curContent.content
    elseif curContentType == GEnums.SNSDialogContentType.Image then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Image
    elseif curContentType == GEnums.SNSDialogContentType.Sticker then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Sticker
    elseif curContentType == GEnums.SNSDialogContentType.Video then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Video
    elseif curContentType == GEnums.SNSDialogContentType.Voice then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Voice
    elseif curContentType == GEnums.SNSDialogContentType.Item then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Item
    elseif curContentType == GEnums.SNSDialogContentType.Card then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Card
    elseif curContentType == GEnums.SNSDialogContentType.Moment then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Moment
    elseif curContentType == GEnums.SNSDialogContentType.PRTS then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_PRTS
    elseif curContentType == GEnums.SNSDialogContentType.Vote then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Vote .. curContent.content
    elseif curContentType == GEnums.SNSDialogContentType.Task then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_Task
    elseif curContentType == GEnums.SNSDialogContentType.System then
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_System
    else
        content = Language.LUA_SNS_SUB_DIALOG_CELL_SHOW_CONTENT_COMPONENT_TBD
    end



    content = SNSUtils.resolveTextStyleWithPlayerName(content)
    if isGroup and curContentType ~= GEnums.SNSDialogContentType.System
            and curContentType ~= GEnums.SNSDialogContentType.Task
    then
        if curContent.speaker == Tables.sNSConst.myselfSpeaker then
            contentPrefix = Language.LUA_SNS_SUB_DIALOG_CELL_GROUP_SHOW_CONTENT_MYSELF
        else
            contentPrefix = Tables.sNSChatTable[curContent.speaker].name
        end

        content = string.format(Language.LUA_SNS_SUB_DIALOG_CELL_GROUP_SHOW_CONTENT_FORMAT, contentPrefix, content)
    end

    return content
end

function SNSUtils.regulatePicSizeDelta(picSprite)
    if picSprite == nil then
        return SNSUtils.PIC_STANDARD_SIZE
    end

    local width = picSprite.rect.width
    local height = picSprite.rect.height

    local xRate = width / SNSUtils.PIC_STANDARD_SIZE.x
    local yRate = height / SNSUtils.PIC_STANDARD_SIZE.y

    if xRate < 1 and yRate < 1 then
        return Vector2(width, height)
    else
        local rate = xRate > yRate and xRate or yRate
        return Vector2(width / rate, height / rate)
    end
end

function SNSUtils.getShowingTopicDialogInfos(chatId)

    local sns = GameInstance.player.sns
    local chatInfo = sns.chatInfoDic:get_Item(chatId)
    local dialogInfoDic = sns.dialogInfoDic

    local topicInfos = {}
    for _, dialogId in pairs(chatInfo.topicDialogIds) do
        local dialogInfo = dialogInfoDic:get_Item(dialogId)
        local dialogCfg = Tables.sNSDialogTable[dialogId]
        if dialogInfo.pureContent then
            local topicId = dialogCfg.topicId
            table.insert(topicInfos, {
                topicId = topicId,
                dialogId = dialogId,
                sortId = Tables.sNSDialogTopicTable[topicId].sortId,
                timestamp = dialogInfo.timestamp,
            })
        end
    end
    table.sort(topicInfos, Utils.genSortFunction({"sortId","timestamp"}))

    local showingCount = math.min(#topicInfos, 3)
    local showingTopicDialogInfos = {}
    for i = 1, showingCount do
        table.insert(showingTopicDialogInfos, topicInfos[i])
    end

    return showingTopicDialogInfos
end

function SNSUtils.isSNSDialogLevelUpRelated(dialogId)
    return lume.find(Tables.sNSConst.forceDialogsAfterLevelUp, dialogId) ~= nil
end


_G.SNSUtils = SNSUtils
return SNSUtils