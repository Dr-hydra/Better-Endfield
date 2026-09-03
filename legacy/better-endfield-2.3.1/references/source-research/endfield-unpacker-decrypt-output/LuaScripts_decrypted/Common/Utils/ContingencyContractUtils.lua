local ContingencyContractUtils = {}


ContingencyContractUtils.MAX_ROW_COUNT = 3


function ContingencyContractUtils.GetTagParseInfos(gameId)

    local tagInfos = {}
    local allTagInfosMap = {}
    local tagKeyInfos = {}
    local tagConflictInfos = {}
    local unlockScoreInfos = {}
    local resultInfos = {
        tagInfos = tagInfos,
        allTagInfosMap = allTagInfosMap,
        tagKeyInfos = tagKeyInfos,
        tagConflictInfos = tagConflictInfos,
        unlockScoreInfos = unlockScoreInfos,
    }


    local hasCfg, ccCfg = Tables.contingencyContractTable:TryGetValue(gameId)
    if not hasCfg then
        return resultInfos
    end

    for column, ccGroupCfg in pairs(ccCfg.contractGroupMap) do
        for row = 1, ContingencyContractUtils.MAX_ROW_COUNT do

            local _, singleTagCfg = ccGroupCfg.contractMap:TryGetValue(row)

            local tagEffectCfg
            hasCfg = false
            if singleTagCfg then
                hasCfg, tagEffectCfg = Tables.ccTagTable:TryGetValue(singleTagCfg.tagId)
            end
            local info = ContingencyContractUtils._GetTagInfoTemplate()


            if not tagEffectCfg then

                info.isEmpty = true
                info.tagId = -ContingencyContractUtils.GetCellIndex(column, row)
            else

                local tagId = singleTagCfg.tagId
                local keyId = singleTagCfg.keyId
                local groupId = singleTagCfg.groupId
                info.isEmpty = false
                info.tagId = tagId
                info.level = row
                info.groupId = groupId
                info.conflictId = singleTagCfg.conflictId
                info.keyId = keyId
                info.unlockScore = singleTagCfg.unlockScore
                info.unlockStage = singleTagCfg.unlockActivityStage
                info.canPreview = singleTagCfg.canPreview
                for i, lockId in pairs(singleTagCfg.lockIds) do
                    table.insert(info.lockIds, lockId)
                end

                info.tagName = tagEffectCfg.name
                info.romanNumSuffix = tagEffectCfg.romanNumSuffix
                if string.isEmpty(info.romanNumSuffix) then
                    info.tagFullName = info.tagName
                else
                    info.tagFullName = string.format(Language.LUA_CONTINGENCY_CONTRACT_TAG_NAME_FORMAT, info.tagName, info.romanNumSuffix)
                end
                info.icon = tagEffectCfg.icon

                local formatData = CS.Beyond.Gameplay.ContingencyContractFormatData(tagId)
                info.desc = formatData:GetFormattedDesc()

                if UNITY_EDITOR and ContingencyContractUtils.IsRemoveCCTagLimit then
                    info.keyId = ""
                    info.unlockStage = ""
                    info.lockIds = {}
                else

                    if not string.isEmpty(keyId) then
                        local keyInfo = tagKeyInfos[keyId]
                        if not keyInfo then
                            keyInfo = ContingencyContractUtils._GetKeyLockInfoTemplate(keyId)
                            tagKeyInfos[keyId] = keyInfo
                        end
                        keyInfo.keyTags[tagId] = info
                        table.insert(keyInfo.keyTagsList, info)
                    end
                    if #info.lockIds > 0 then
                        for i, lockId in pairs(info.lockIds) do
                            local lockInfo = tagKeyInfos[lockId]
                            if not lockInfo then
                                lockInfo = ContingencyContractUtils._GetKeyLockInfoTemplate(lockId)
                                tagKeyInfos[lockId] = lockInfo
                            end
                            lockInfo.lockTags[tagId] = info
                        end
                        table.sort(info.lockIds)
                    end

                end


                local conflictId = singleTagCfg.conflictId
                if not string.isEmpty(conflictId) then
                    local conflictInfo = tagConflictInfos[conflictId]
                    if not conflictInfo then
                        conflictInfo = {
                            curJoinedTag = nil,
                            conflictTagsList = {},
                        }
                        tagConflictInfos[conflictId] = conflictInfo
                    end
                    table.insert(conflictInfo.conflictTagsList, info)
                end



                if info.unlockScore > 0 then
                    local unlockScoreInfo = unlockScoreInfos[info.unlockScore]
                    if not unlockScoreInfo then
                        unlockScoreInfo = {
                            score = info.unlockScore,
                            minCol = column,
                            maxCol = column,
                        }
                        unlockScoreInfos[info.unlockScore] = unlockScoreInfo
                    else
                        unlockScoreInfo.minCol = math.min(unlockScoreInfo.minCol, column)
                        unlockScoreInfo.maxCol = math.max(unlockScoreInfo.maxCol, column)
                    end
                end

            end



            if tagInfos[column] == nil then
                tagInfos[column] = {}
            end
            tagInfos[column][row] = info
            allTagInfosMap[info.tagId] = info



            info.column = column
            info.row = row
            info.cellIndex = ContingencyContractUtils.GetCellIndex(column, row)

            local tempInfo
            if row > 1 then
                tempInfo = tagInfos[column][row - 1]
                if tempInfo then
                    info.upTag = tempInfo
                    tempInfo.downTag = info
                end
            end

            if row < 3 then
                tempInfo = tagInfos[column][row + 1]
                if tempInfo then
                    info.downTag = tempInfo
                    tempInfo.upTag = info
                end
            end

            if tagInfos[column - 1] then
                tempInfo = tagInfos[column - 1][row]
                if tempInfo then
                    info.leftTag = tempInfo
                    tempInfo.rightTag = info
                end
            end

            if tagInfos[column + 1] then
                tempInfo = tagInfos[column + 1][row]
                if tempInfo then
                    info.rightTag = tempInfo
                    tempInfo.leftTag = info
                end
            end

        end
    end

    return resultInfos
end


function ContingencyContractUtils._GetTagInfoTemplate()
    return {

        isEmpty = false,

        tagId = 0,
        level = 0,
        groupId = 0,
        conflictId = "",
        keyId = "",
        lockIds = {},
        unlockStage = "",
        canPreview = false,

        tagName = "",
        romanNumSuffix = "",
        tagFullName = "",
        icon = "",
        desc = "",

        joinedSeq = 0,
        isUnlockByStage = false,
        hasFirstPassReward = true,
        useKeyCount = 0,

        column = 0,
        row = 0,
        cellIndex = 0,
        upTag = nil,
        downTag = nil,
        leftTag = nil,
        rightTag = nil,
    }
end


function ContingencyContractUtils._GetKeyLockInfoTemplate(keyId)
    local _, keyCfg = Tables.contingencyContractKeyLockTable:TryGetValue(keyId)
    return {
        keyId = keyId,
        keyName = keyCfg.keyName,
        lockName = keyCfg.lockName,
        color = UIUtils.getColorByString(keyCfg.color),
        keyDecoImg = keyCfg.keyDecoImg,
        keyTags = {},
        keyTagsList = {},
        lockTags = {},

        curJoinedKeyTags = {},
        curJoinedLockTags = {},
    }
end

function ContingencyContractUtils.GetCellIndex(column, row)
    return (column - 1) * ContingencyContractUtils.MAX_ROW_COUNT + row
end

function ContingencyContractUtils.GetColumnRow(luaIndex)
    local column = (luaIndex - 1) // ContingencyContractUtils.MAX_ROW_COUNT + 1
    local row = (luaIndex - 1) % ContingencyContractUtils.MAX_ROW_COUNT + 1
    return column, row
end






function ContingencyContractUtils.SortTagIdsByGridPos(gameId, tagIds)
    local sorted = {}
    if not tagIds then
        return sorted
    end
    for _, tagId in ipairs(tagIds) do
        table.insert(sorted, tagId)
    end

    local hasCfg, ccCfg = Tables.contingencyContractTable:TryGetValue(gameId)
    if not hasCfg or not ccCfg then
        return sorted
    end

    local tagIdToPos = {}
    for column, ccGroupCfg in pairs(ccCfg.contractGroupMap) do
        for row = 1, ContingencyContractUtils.MAX_ROW_COUNT do
            local hasTag, singleTagCfg = ccGroupCfg.contractMap:TryGetValue(row)
            if hasTag and singleTagCfg then
                tagIdToPos[singleTagCfg.tagId] = { column = column, row = row }
            end
        end
    end

    local function getSortKey(tagId)
        local pos = tagIdToPos[tagId]
        if pos then
            return pos.column, pos.row
        end
        if UNITY_EDITOR then
            logger.error(string.format("ContingencyContractUtils.SortTagIdsByGridPos: tagId [%s] not found in gameId [%s]", tostring(tagId), tostring(gameId)))
        end
        return math.huge, math.huge
    end

    table.sort(sorted, function(a, b)
        local colA, rowA = getSortKey(a)
        local colB, rowB = getSortKey(b)
        if colA ~= colB then
            return colA < colB
        end
        return rowA < rowB
    end)

    return sorted
end


function ContingencyContractUtils.CreateStageToTagMap()
    local stageToTagMap = {}
    for _, contractData in pairs(Tables.contingencyContractTable) do
        for _, groupData in pairs(contractData.contractGroupMap) do
            for _, singleData in pairs(groupData.contractMap) do
                local unlockStage = singleData.unlockActivityStage
                local tagId = singleData.tagId
                if not string.isEmpty(unlockStage) then
                    if not stageToTagMap[unlockStage] then
                        stageToTagMap[unlockStage] = {}
                    end
                    table.insert(stageToTagMap[unlockStage], tagId)
                end
            end
        end
    end
    return stageToTagMap
end




ContingencyContractUtils.GenShareCodeSourceType = {
    DungeonClear = 0,
    DungeonFail = 1,
    Normal = 2,
}



local _BASE32_ALPHABET = "0123456789ABCDEFGHJKMNPQRSTVWXYZ"
local _BASE8_ALPHABET = "01234567"
local _BASE32_MAP = {}
for i = 1, #_BASE32_ALPHABET do
    _BASE32_MAP[_BASE32_ALPHABET:sub(i, i)] = i - 1
end

local function _GetShareCodeConfig(gameId)
    local hasCfg, ccCfg = Tables.contingencyContractTable:TryGetValue(gameId)
    if not hasCfg or not ccCfg then
        return false, nil, 0, 0
    end

    local hasActivityCfg, ccActivityCfg = Tables.activityContingencyContractTable:TryGetValue(ccCfg.activityId)
    if not hasActivityCfg then
        return false, nil, 0, 0
    end

    local tagMaxColumn = ccActivityCfg.tagMaxColumn
    local shareCodeLength = math.ceil(tagMaxColumn * ContingencyContractUtils.MAX_ROW_COUNT / 5)
    return true, ccCfg, tagMaxColumn, shareCodeLength
end





local function _ConvertBase(digits, fromBase, toBase)
    if #digits == 0 then
        return { 0 }
    end

    local working = {}
    for i = 1, #digits do
        working[i] = digits[i]
    end

    local result = {}
    while #working > 0 do
        local nextWorking = {}
        local remainder = 0
        for i = 1, #working do
            local cur = remainder * fromBase + working[i]
            local quotient = math.floor(cur / toBase)
            remainder = cur % toBase
            if quotient > 0 or #nextWorking > 0 then
                table.insert(nextWorking, quotient)
            end
        end

        table.insert(result, 1, remainder)
        working = nextWorking
    end

    return result
end




local function _DigitsToString(digits, alphabet)
    local sb = {}
    for i = 1, #digits do
        sb[i] = alphabet:sub(digits[i] + 1, digits[i] + 1)
    end
    return table.concat(sb)
end





local function _StringToDigits(str, map)
    local digits = {}
    for i = 1, #str do
        local ch = str:sub(i, i)
        ch = ch:upper()
        local value = map[ch]
        if value == nil then
            return false, {}
        end
        table.insert(digits, value)
    end
    return true, digits
end





function ContingencyContractUtils.GenShareText(gameId, tagIds, totalScore, source)
    local suc, result = ContingencyContractUtils.GenShareCode(gameId, tagIds)
    if not suc then
        return false, ""
    end

    return true, ContingencyContractUtils.WrapShareText(result, totalScore, source)
end


function ContingencyContractUtils.WrapShareText(shareCode, totalScore, source)
    local resultText = string.format("<%s>", shareCode)
    if source == ContingencyContractUtils.GenShareCodeSourceType.Normal then
        resultText = string.format(Language.LUA_CONTINGENCY_CONTRACT_SHARE_CODE_WRAP_TEXT_NORMAL, totalScore, resultText)
    elseif source == ContingencyContractUtils.GenShareCodeSourceType.DungeonClear then
        resultText = string.format(Language.LUA_CONTINGENCY_CONTRACT_SHARE_CODE_WRAP_TEXT_DUNGEON_CLEAR, totalScore, resultText)
    elseif source == ContingencyContractUtils.GenShareCodeSourceType.DungeonFail then
        resultText = string.format(Language.LUA_CONTINGENCY_CONTRACT_SHARE_CODE_WRAP_TEXT_DUNGEON_FAIL, totalScore, resultText)
    end
    return resultText
end


function ContingencyContractUtils.GenShareCode(gameId, tagIds)
    local hasCfg, ccCfg, tagMaxColumn, shareCodeLength = _GetShareCodeConfig(gameId)
    if not hasCfg then
        return false, ""
    end


    local selectedTagSet = {}
    for _, tagId in pairs(tagIds) do
        selectedTagSet[tagId] = true
    end

    local columnValues = {}
    for column, ccGroupCfg in pairs(ccCfg.contractGroupMap) do
        for row = 1, ContingencyContractUtils.MAX_ROW_COUNT do
            local hasTag, singleTagCfg = ccGroupCfg.contractMap:TryGetValue(row)
            if hasTag and singleTagCfg and selectedTagSet[singleTagCfg.tagId] then
                local weight = 1 << (row - 1)
                columnValues[column] = (columnValues[column] or 0) + weight
            end
        end
    end


    local octalDigits = {}
    for col = 1, tagMaxColumn do
        local value = columnValues[col] or 0

        value = lume.clamp(value, 0, 7)
        octalDigits[tagMaxColumn - col + 1] = value
    end


    local base32Digits = _ConvertBase(octalDigits, 8, 32)
    local shareCode = _DigitsToString(base32Digits, _BASE32_ALPHABET)
    if #shareCode < shareCodeLength then
        shareCode = string.rep("0", shareCodeLength - #shareCode) .. shareCode
    end

    return true, shareCode
end



function ContingencyContractUtils.AnalyzeShareText(gameId, shareText)
    if type(shareText) ~= "string" then
        return false, {}, 0
    end

    local leftPos = string.find(shareText, "<", 1, true)
    local rightPos = leftPos and string.find(shareText, ">", leftPos + 1, true) or nil


    if not leftPos or not rightPos or leftPos >= rightPos then
        return ContingencyContractUtils.AnalyzeShareCode(gameId, string.upper(shareText))
    end
    if string.find(shareText, "<", leftPos + 1, true) or string.find(shareText, ">", rightPos + 1, true) then
        return false, {}, 0
    end

    local shareCode = string.sub(shareText, leftPos + 1, rightPos - 1)
    if shareCode == "" then
        return false, {}, 0
    end

    return ContingencyContractUtils.AnalyzeShareCode(gameId, string.upper(shareCode))
end


function ContingencyContractUtils.AnalyzeShareCode(gameId, shareCode, noUnlockToast)
    local hasCfg, ccCfg, tagMaxColumn, shareCodeLength = _GetShareCodeConfig(gameId)
    if not hasCfg then
        if not noUnlockToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
        end
        return false, {}, 0
    end

    shareCode = shareCode or ""
    local ok, base32Digits = _StringToDigits(shareCode, _BASE32_MAP)
    if not ok or #base32Digits == 0 or #shareCode ~= shareCodeLength then
        if not noUnlockToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
        end
        return false, {}, 0
    end

    local octalDigits = _ConvertBase(base32Digits, 32, 8)

    local octalStr = _DigitsToString(octalDigits, _BASE8_ALPHABET)
    if #octalStr > tagMaxColumn then

        if not noUnlockToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
        end
        return false, {}, 0
    end
    if #octalStr < tagMaxColumn then
        octalStr = string.rep("0", tagMaxColumn - #octalStr) .. octalStr
    end

    local resultTagIds = {}
    local totalScore = 0
    for col = 1, tagMaxColumn do
        local digitIndex = tagMaxColumn - col + 1
        local digitChar = octalStr:sub(digitIndex, digitIndex)
        local value = tonumber(digitChar) or 0
        local hasGroup, ccGroupCfg = ccCfg.contractGroupMap:TryGetValue(col)
        if hasGroup then
            for row = 1, ContingencyContractUtils.MAX_ROW_COUNT do
                local weight = 1 << (row - 1)
                if value & weight ~= 0 then
                    local fail = true
                    local hasTag, singleTagCfg = ccGroupCfg.contractMap:TryGetValue(row)
                    if hasTag then
                        local hasTagCfg, tagCfg = Tables.ccTagTable:TryGetValue(singleTagCfg.tagId)
                        if hasTagCfg then
                            table.insert(resultTagIds, singleTagCfg.tagId)
                            totalScore = totalScore + tagCfg.score
                            fail = false
                        end
                    end

                    if fail then
                        if not noUnlockToast then
                            Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
                        end
                        return false, {}, 0
                    end
                end
            end
        end
    end

    local isValid = ContingencyContractUtils.CheckJoinedTagListValid(gameId, resultTagIds, noUnlockToast)
    if not isValid then

        return false, {}, 0
    end

    return true, resultTagIds, totalScore
end


function ContingencyContractUtils.CheckJoinedTagListValid(gameId, joinedTagIds, noUnlockToast)
    if #joinedTagIds <= 0 then
        return true
    end
    local hasCfg, ccCfg = Tables.contingencyContractTable:TryGetValue(gameId)
    if not hasCfg then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
        return false
    end
    local activityId = ccCfg.activityId
    local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
    if not activityData then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
        return false
    end

    local ccGameData = GameInstance.player.contingencyContractSystem:GetCcGameData(gameId)
    if not ccGameData then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
        return false
    end


    local joinedTagIdMap = {}
    for _, tagId in pairs(joinedTagIds) do
        joinedTagIdMap[tagId] = true
    end
    local infosTable = ContingencyContractUtils.GetTagParseInfos(gameId)
    local allTagInfosMap = infosTable.allTagInfosMap
    local keyInfos = infosTable.tagKeyInfos
    local conflictInfos = infosTable.tagConflictInfos

    for _, tagId in pairs(joinedTagIds) do
        local tagInfo = allTagInfosMap[tagId]
        if not tagInfo then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
            return false
        end

        if not string.isEmpty(tagInfo.unlockStage) then
            if not ActivityUtils.isStageUnlockMultiConditionStageActivity(activityData, tagInfo.unlockStage) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
                return false
            end
        end

        if #tagInfo.lockIds > 0 then
            for _, lockId in pairs(tagInfo.lockIds) do
                local lockInfo = keyInfos[lockId]
                local hasKeyJoined = false
                for keyTagId, keyTagInfo in pairs(lockInfo.keyTags) do
                    if keyTagId ~= tagId and joinedTagIdMap[keyTagId] then
                        hasKeyJoined = true
                        break
                    end
                end
                if not hasKeyJoined then

                    Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SELECT_TAG_NOT_VALID_TOAST)
                    return false
                end
            end
        end

        local conflictId = tagInfo.conflictId
        if not string.isEmpty(conflictId) then
            local conflictInfo = conflictInfos[conflictId]
            for _, conflictTagInfo in pairs(conflictInfo.conflictTagsList) do
                if conflictTagInfo.tagId ~= tagId and joinedTagIdMap[conflictTagInfo.tagId] then

                    Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_ERROR_CODE_TIP)
                    return false
                end
            end
        end

        if tagInfo.unlockScore > ccGameData.historyBestScore then
            if not noUnlockToast then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SELECT_TAG_NOT_VALID_TOAST)
            end
            return false
        end
    end
    return true
end





function ContingencyContractUtils.CreateGroupToTaskMap(activityId)
    local taskInfoDict = Tables.activityConditionalMultiStageTaskConfigTable[activityId].TaskConfigMap
    local groupToTaskMap = {}
    for _, taskConfigData in pairs(taskInfoDict) do
        local groupId = taskConfigData.taskGroupId
        local taskId = taskConfigData.taskId
        if not string.isEmpty(groupId) then
            if not groupToTaskMap[groupId] then
                groupToTaskMap[groupId] = {}
            end
            table.insert(groupToTaskMap[groupId], taskId)
        end
    end
    return groupToTaskMap
end



function ContingencyContractUtils.CheckVisibilityDungeonInfoBtn()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    return not string.isEmpty(dungeonId) and DungeonUtils.isDungeonContract(dungeonId)
end

function ContingencyContractUtils.OnClickDungeonInfoBtnContingencyContract()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if not succ then
        logger.error("ContingencyContractUtils.OnClickDungeonInfoBtnContingencyContract invalid dungeonId", dungeonId)
        return
    end
    DungeonUtils.onClickDungeonInfoBtnContingencyContract(dungeonId)
end

function ContingencyContractUtils.GetTotalScoreByGamaId(gameId)
    local ccSystem = GameInstance.player.contingencyContractSystem
    local ccData = ccSystem:GetCcGameData(gameId)
    local totalScore = 0
    for _, tagId in pairs(ccData.curSelectTagList) do
        local succ, tagCfg = Tables.ccTagTable:TryGetValue(tagId)
        if succ then
            totalScore = totalScore + tagCfg.score
        end
    end
    return totalScore
end

function ContingencyContractUtils.UpdateMainHudInfoBtn(mainHudView)
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if not succ then
        return
    end
    mainHudView.topLeftBtns.startCountTxt.text = ContingencyContractUtils.GetTotalScoreByGamaId(dungeonId)
end


ContingencyContractUtils.IsRemoveCCTagLimit = false



_G.ContingencyContractUtils = ContingencyContractUtils
return ContingencyContractUtils