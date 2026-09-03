local BattlePassUtils = {}

function BattlePassUtils.CheckBattlePassSeasonValid()
    local seasonData = GameInstance.player.battlePassSystem.seasonData
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    if string.isEmpty(seasonData.seasonId) or curServerTime < seasonData.openTime or curServerTime >= seasonData.closeTime then
        return false
    end
    if seasonData.closeInfo ~= nil and seasonData.closeInfo.isClose then
        return false
    end
    return true
end

function BattlePassUtils.CheckBattlePassPurchaseBlock()
    return BLOCK_BATTLEPASS_PURCHASE or (CashShopUtils.IsPS() and CashShopUtils.NoCashShopGoods())
end

function BattlePassUtils.GetSeasonLeftTime()
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return 0
    end
    local closeTime = GameInstance.player.battlePassSystem.seasonData.closeTime
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    if curServerTime > closeTime then
        return 0
    end
    return closeTime - curServerTime
end

function BattlePassUtils.GetSeasonLastDay()
    return UIUtils.getLeftTime(BattlePassUtils.GetSeasonLeftTime())
end

function BattlePassUtils.CheckBattlePassJumpAvail(jumpId)
    local hasJump, jumpInfo = Tables.systemJumpTable:TryGetValue(jumpId)
    if not hasJump then
        return false
    end
    if not lume.find(Tables.battlePassConst.jumpForbidGameMode, GameInstance.mode.modeType) then
        return true
    end
    if lume.find(Tables.battlePassConst.jumpForbidSystemWhiteList, jumpInfo.bindSystem) then
        return true
    end
    return false
end

function BattlePassUtils.GetBattlePassTrackName(trackType)
    local trackName = ''
    for _, trackData in pairs(Tables.battlePassTrackTable) do
        if trackData.trackType == trackType then
            trackName = trackData.name
            break
        end
    end
    return trackName
end

function BattlePassUtils.CheckBattlePassItemCanUse(itemId)
    local seasonValid = BattlePassUtils.CheckBattlePassSeasonValid()
    if not seasonValid then
        return false, Language.LUA_BATTLEPASS_TICKET_SEASON_INVALID
    end
    local hasTrackType, trackType = BattlePassUtils.GetBattlePassTicketTrackType(itemId)
    if not hasTrackType then
        return false, Language.LUA_BATTLEPASS_TICKET_TICKET_INVALID
    end
    local trackName = BattlePassUtils.GetBattlePassTrackName(trackType)
    local trackActivate = BattlePassUtils.CheckBattlePassTrackActive(trackType)
    if trackActivate then
        return false, string.format(Language.LUA_BATTLEPASS_TICKET_TRACK_INVALID, trackName)
    end
    return true
end

function BattlePassUtils.TryUseBattlePassItem(itemId, instId)
    local canUse, cantReason = BattlePassUtils.CheckBattlePassItemCanUse(itemId)
    if not canUse then
        Notify(MessageConst.SHOW_TOAST, cantReason)
    end
    local hasTrackType, trackType = BattlePassUtils.GetBattlePassTicketTrackType(itemId)
    if not hasTrackType then
        return
    end
    local trackName = BattlePassUtils.GetBattlePassTrackName(trackType)
    local itemName = Tables.itemTable[itemId].name
    local hintFormat = nil
    if trackType == GEnums.BPTrackType.PAY then
        hintFormat = Language.LUA_BATTLEPASS_TICKET_PAY_USE_HINT
    else
        hintFormat = Language.LUA_BATTLEPASS_TICKET_ORG_USE_HINT
    end
    Notify(MessageConst.SHOW_POP_UP, {
        content = string.format(hintFormat, itemName, trackName),
        onConfirm = function()
            if BattlePassUtils.TimeCloseToEnd() then
                Notify(MessageConst.SHOW_POP_UP,{
                    content = string.format(Language.LUA_BATTLEPASS_BUY_TRACK_SUBTITLE, BattlePassUtils.GetSeasonLastDay()),
                    onConfirm = function()
                        GameInstance.player.inventory:UseBattlePassItem(itemId, instId)
                    end
                })
                return
            end
            GameInstance.player.inventory:UseBattlePassItem(itemId, instId)
        end,
    })
end

function BattlePassUtils.GetBattlePassTicketTrackType(itemId)
    for _, orgItemId in pairs(Tables.battlePassConst.voucherOriginiumTrack) do
        if itemId == orgItemId then
            return true, GEnums.BPTrackType.ORIGINIUM
        end
    end
    for _, payItemId in pairs(Tables.battlePassConst.voucherPayTrack) do
        if itemId == payItemId then
            return true, GEnums.BPTrackType.PAY
        end
    end
    return false
end

function BattlePassUtils.CheckBattlePassTrackActive(trackType)
    local trackData = GameInstance.player.battlePassSystem.trackData
    for trackId, playerTrack in pairs(trackData.trackRewards) do
        local hasTrack, trackInfo = Tables.battlePassTrackTable:TryGetValue(trackId)
        if hasTrack and trackInfo.trackType == trackType then
            return true, playerTrack
        end
    end
    return false
end

function BattlePassUtils.GetBattlePassExpBoost()
    local expBoost = 0
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return expBoost
    end
    local trackTable = Tables.battlePassTrackTable
    local bpSystem = GameInstance.player.battlePassSystem
    for trackId, trackData in pairs(trackTable) do
        if bpSystem.trackData.trackRewards:ContainsKey(trackId) then
            expBoost = expBoost + trackData.bpExpUpRatio
        end
    end
    return expBoost
end

function BattlePassUtils.GetAdventureDailyBpExpBaseCount(rewardId)
    local rewardData = Tables.rewardTable[rewardId]
    local bpItemId = Tables.battlePassConst.bpExpItem
    local bpExpBaseCount = 0
    if rewardData ~= nil then
        for _, itemBundle in pairs(rewardData.itemBundles) do
            if itemBundle.id == bpItemId then
                bpExpBaseCount = itemBundle.count
                break
            end
        end
    end
    return bpExpBaseCount
end

function BattlePassUtils.CheckHasAvailBpPlanReward()
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false, -1
    end
    local bpSystem = GameInstance.player.battlePassSystem
    if bpSystem.firstCanObtainedLevel < 0 then
        return false, -1
    end
    return true, bpSystem.firstCanObtainedLevel
end

function BattlePassUtils.GetLastGainBpPlanLevel()
    local lastLevel = -1
    local trackData = GameInstance.player.battlePassSystem.trackData
    for _, playerTrack in pairs(trackData.trackRewards) do
        if playerTrack ~= nil then
            for _, level in pairs(playerTrack.rewardGainedLevel) do
                if level > lastLevel then
                    lastLevel = level
                end
            end
        end
    end
    if lastLevel > 0 then
        return true, lastLevel
    end
    return false
end

function BattlePassUtils.CheckHasCompletedTask()
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    for taskId, taskInfo in pairs(bpSystem.taskData.taskInfos) do
        if taskInfo.taskState == CS.Proto.BP_TASK_STATE.HasCompleted then
            return true
        end
    end
    return false
end

function BattlePassUtils.CheckLabelVisible(labelId)
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local hasLabel, playerLabel = bpSystem.taskData.taskLabels:TryGetValue(labelId)
    if not hasLabel then
        return false
    end
    local hasTaskLabelData, taskLabelData = Tables.battlePassTaskLabelTable:TryGetValue(labelId)
    if not hasTaskLabelData then
        return false
    end
    if playerLabel.closeInfo ~= nil and playerLabel.closeInfo.isClose then
        return false
    end

    local isConditionVisible = playerLabel.visibleConditions.Count <= 0 or playerLabel.visibleConditions:GetAndCondition()
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local isTimeVisible = curServerTime >= playerLabel.visibleTime

    return true, isTimeVisible, isConditionVisible
end

function BattlePassUtils.CheckLabelRedDot(labelId)
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local isValid, isTimeVisible, isConditionVisible = BattlePassUtils.CheckLabelVisible(labelId)
    if not isValid or not isTimeVisible or not isConditionVisible then
        return false
    end
    local isSubLabel, mainLabelEntry = Tables.battlePassTaskSubLabelMapTable:TryGetValue(labelId)
    if isSubLabel then
        local isParentLabelValid, isParentLabelTimeValid, isParentLabelConditionValid = BattlePassUtils.CheckLabelVisible(mainLabelEntry.parentLabelId)
        if not isParentLabelValid or not isParentLabelTimeValid or not isParentLabelConditionValid then
            return false
        end
    end
    local hasRedDot = false
    local redDotType = UIConst.RED_DOT_TYPE.New
    local hasSubLabel, subLabelData = Tables.battlePassTaskLabelMapTable:TryGetValue(labelId)
    if hasSubLabel then
        for _, subLabelInfo in pairs(subLabelData.subLabels) do
            local hasSubRedDot, subRedDotType = BattlePassUtils.CheckLabelRedDot(subLabelInfo.taskLabelID)
            if hasSubRedDot and subRedDotType == UIConst.RED_DOT_TYPE.New then
                hasRedDot = true
                redDotType = (redDotType == UIConst.RED_DOT_TYPE.Normal) and UIConst.RED_DOT_TYPE.Normal or UIConst.RED_DOT_TYPE.New
            end
            if hasSubRedDot and subRedDotType == UIConst.RED_DOT_TYPE.Normal then
                hasRedDot = true
                redDotType = UIConst.RED_DOT_TYPE.Normal
            end
        end
    else
        local bpSystem = GameInstance.player.battlePassSystem
        local hasLabel, labelInfo = bpSystem.taskData.taskLabels:TryGetValue(labelId)
        if hasLabel then
            for _, groupId in pairs(labelInfo.groupIds) do
                local hasGroupRedDot, groupRedDotType = BattlePassUtils.CheckGroupRedDot(groupId)
                if hasGroupRedDot and groupRedDotType == UIConst.RED_DOT_TYPE.New then
                    hasRedDot = true
                    redDotType = (redDotType == UIConst.RED_DOT_TYPE.Normal) and UIConst.RED_DOT_TYPE.Normal or UIConst.RED_DOT_TYPE.New
                end
                if hasGroupRedDot and groupRedDotType == UIConst.RED_DOT_TYPE.Normal then
                    hasRedDot = true
                    redDotType = UIConst.RED_DOT_TYPE.Normal
                end
            end
        end
    end
    if hasRedDot then
        return true, redDotType
    end
    return false
end

function BattlePassUtils.CheckGroupVisible(groupId)
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local hasGroup, playerGroup = bpSystem.taskData.taskGroups:TryGetValue(groupId)
    if not hasGroup then
        return false
    end
    local hasTaskGroupData, taskGroupData = Tables.battlePassTaskGroupTable:TryGetValue(groupId)
    if not hasTaskGroupData then
        return false
    end
    if playerGroup.closeInfo ~= nil and playerGroup.closeInfo.isClose then
        return false
    end
    local isConditionVisible = playerGroup.visibleConditions.Count <= 0 or playerGroup.visibleConditions:GetAndCondition()
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local isTimeVisible = curServerTime >= playerGroup.visibleTime and curServerTime < playerGroup.disableTime

    return true, isTimeVisible, isConditionVisible
end

function BattlePassUtils.CheckGroupEnable(groupId)

    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local hasGroup, playerGroup = bpSystem.taskData.taskGroups:TryGetValue(groupId)
    if not hasGroup then
        return false
    end
    if playerGroup.closeInfo ~= nil and playerGroup.closeInfo.isClose then
        return false
    end
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    return curServerTime < playerGroup.disableTime
end

function BattlePassUtils.CheckGroupRedDot(groupId)
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local isValid, isTimeVisible, isConditionVisible = BattlePassUtils.CheckGroupVisible(groupId)
    if not isValid or not isTimeVisible or not isConditionVisible then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local hasGroup, groupInfo = bpSystem.taskData.taskGroups:TryGetValue(groupId)
    local hasRedDot = false
    local redDotType = UIConst.RED_DOT_TYPE.New
    if hasGroup then
        for _, taskId in pairs(groupInfo.taskIds) do
            if BattlePassUtils.CheckTaskUnread(taskId) then
                hasRedDot = true
                redDotType = (redDotType == UIConst.RED_DOT_TYPE.Normal) and UIConst.RED_DOT_TYPE.Normal or UIConst.RED_DOT_TYPE.New
            end
            if BattlePassUtils.CheckTaskCompleted(taskId) then
                hasRedDot = true
                redDotType = UIConst.RED_DOT_TYPE.Normal
            end
        end
    end
    if hasRedDot then
        return true, redDotType
    end
    return false
end

function BattlePassUtils.CheckTaskCompleted(taskId)
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local isTaskValid, isTimeVisible, isConditionVisible = BattlePassUtils.CheckTaskVisible(taskId)
    if not isTaskValid or not isTimeVisible or not isConditionVisible then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local hasTask, taskInfo = bpSystem.taskData.taskInfos:TryGetValue(taskId)
    if hasTask then
        return taskInfo.taskState == CS.Proto.BP_TASK_STATE.HasCompleted
    end
    return false
end

function BattlePassUtils.CheckTaskVisible(taskId)
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local hasTask, playerTask = bpSystem.taskData.taskInfos:TryGetValue(taskId)
    if not hasTask then
        return false
    end
    local hasTaskData, taskData = Tables.battlePassTaskTable:TryGetValue(taskId)
    if not hasTaskData then
        return false
    end
    if playerTask.closeInfo ~= nil and playerTask.closeInfo.isClose then
        return false
    end
    if playerTask.taskState == CS.Proto.BP_TASK_STATE.Disable then
        return false
    end
    local isConditionVisible = playerTask.visibleConditions.Count <= 0 or playerTask.visibleConditions:GetAndCondition()
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local isTimeVisible = curServerTime >= playerTask.visibleTime
    return true, isTimeVisible, isConditionVisible
end

function BattlePassUtils.CheckTaskUnread(taskId)
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local isTaskValid, isTimeVisible, isConditionVisible = BattlePassUtils.CheckTaskVisible(taskId)
    if not isTaskValid or not isTimeVisible or not isConditionVisible then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    return bpSystem:IsTaskUnread(taskId)
end

function BattlePassUtils.GetShowingTaskIds()
    local taskIds = {}
    local bpSystem = GameInstance.player.battlePassSystem
    for taskId, taskInfo in pairs(bpSystem.taskData.taskInfos) do
        local isLabelValid = false
        if taskInfo.hostLabel ~= nil then
            local labelValid, labelTimeValid, labelConditionValid = BattlePassUtils.CheckLabelVisible(taskInfo.hostLabel.labelId)
            isLabelValid = labelValid and labelTimeValid and labelConditionValid
            local isSubLabel, mainLabelEntry = Tables.battlePassTaskSubLabelMapTable:TryGetValue(taskInfo.hostLabel.labelId)
            if isSubLabel then
                local isParentLabelValid, isParentLabelTimeValid, isParentLabelConditionValid = BattlePassUtils.CheckLabelVisible(mainLabelEntry.parentLabelId)
                if not isParentLabelValid or not isParentLabelTimeValid or not isParentLabelConditionValid then
                    isLabelValid = false
                end
            end
        end
        local isGroupValid = false
        if isLabelValid and taskInfo.hostGroup ~= nil then
            local groupValid, groupTimeValid, groupConditionValid = BattlePassUtils.CheckGroupVisible(taskInfo.hostGroup.taskGroupId)
            isGroupValid = groupValid and groupTimeValid and groupConditionValid
        end
        local isTaskValid = false
        if isLabelValid and isGroupValid then
            local valid, timeValid, conditionValid = BattlePassUtils.CheckTaskVisible(taskId)
            isTaskValid = valid and timeValid and conditionValid
        end
        if isTaskValid then
            table.insert(taskIds, taskId)
        end
    end
    return taskIds
end

function BattlePassUtils.CheckIsRewardGained(trackType, level)
    if not BattlePassUtils.CheckBattlePassSeasonValid() then
        return false
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local hasSeason, seasonInfo = Tables.battlePassSeasonTable:TryGetValue(bpSystem.seasonData.seasonId)
    if not hasSeason then
        return false
    end
    local hasLevelGroup, levelGroup = Tables.battlePassLevelTable:TryGetValue(seasonInfo.levelGroupId)
    if not hasLevelGroup then
        return false
    end
    local hasLevel, levelInfo = levelGroup.levelInfos:TryGetValue(level)
    if not hasLevel then
        return false
    end
    local rewardId = ''
    if trackType == GEnums.BPTrackType.FREE then
        rewardId = levelInfo.freeRewardId
    elseif trackType == GEnums.BPTrackType.ORIGINIUM then
        rewardId = levelInfo.originiumRewardId
    elseif trackType == GEnums.BPTrackType.PAY then
        rewardId = levelInfo.payRewardId
    end
    local hasOverrideLevel, overrideLevelGroup = Tables.battlePassOverrideLevelTable:TryGetValue(seasonInfo.ovrLvRewardGroupId)
    if hasOverrideLevel then
        local hasOverrideLevel, overrideLevelInfo = overrideLevelGroup.levelInfos:TryGetValue(level)
        if hasOverrideLevel then
            if trackType == GEnums.BPTrackType.FREE then
                rewardId = overrideLevelInfo.freeRewardId
            elseif trackType == GEnums.BPTrackType.ORIGINIUM then
                rewardId = overrideLevelInfo.originiumRewardId
            elseif trackType == GEnums.BPTrackType.PAY then
                rewardId = overrideLevelInfo.payRewardId
            end
        end
    end
    if string.isEmpty(rewardId) then
        return false
    end
    local trackData = GameInstance.player.battlePassSystem.trackData
    for trackId, playerTrack in pairs(trackData.trackRewards) do
        local hasTrack, trackInfo = Tables.battlePassTrackTable:TryGetValue(trackId)
        if hasTrack and trackInfo.trackType == trackType then
            if levelInfo.isRecurring then
                local isMaxLevel = bpSystem.levelData.currLevel >= seasonInfo.maxLevel
                local recruitAllTime = (isMaxLevel and levelInfo.toNextExp > 0) and (bpSystem.levelData.currExp // levelInfo.toNextExp) or 0
                local recruitTime = playerTrack.recurringTimes
                return recruitAllTime <= recruitTime
            else
                return playerTrack.rewardGainedLevel:Contains(level)
            end
        end
    end
    return false
end

function BattlePassUtils.CheckOriginiumEnough(level)
    local price = Tables.battlePassConst.buyLevelMoneyCnt
    local count = level
    local money = Utils.getItemCount(Tables.battlePassConst.buyOriginiumTrackMoneyID, false)
    return money >= price * count
end

function BattlePassUtils.GetExpGap(level1,exp1,level2,exp2)
    local gap = 0
    local seasonId = GameInstance.player.battlePassSystem.seasonData.seasonId
    local _, seasonData = Tables.battlePassSeasonTable:TryGetValue(seasonId)
    local levelInfos = Tables.battlePassLevelTable[seasonData.levelGroupId].levelInfos
    for i = level1 + 1,level2 do
        gap = gap + levelInfos[i].levelExp
    end
    gap = gap - exp1 + exp2
    return gap
end

function BattlePassUtils.GetOriginiumTrackInfo()
    return Tables.battlePassTrackTable[Tables.battlePassTrackTypeToIDTable[GEnums.BPTrackType.ORIGINIUM].bpTrackID]
end

function BattlePassUtils.GetPayTrackInfo()
    return Tables.battlePassTrackTable[Tables.battlePassTrackTypeToIDTable[GEnums.BPTrackType.PAY].bpTrackID]
end

function BattlePassUtils.CheckOriginiumTrackActive()
    return GameInstance.player.battlePassSystem.trackData.trackRewards:ContainsKey(Tables.battlePassTrackTypeToIDTable[GEnums.BPTrackType.ORIGINIUM].bpTrackID)
end

function BattlePassUtils.CheckPayTrackActive()
    return GameInstance.player.battlePassSystem.trackData.trackRewards:ContainsKey(Tables.battlePassTrackTypeToIDTable[GEnums.BPTrackType.PAY].bpTrackID)
end

function BattlePassUtils.GetOriginiumTrackRewardInfo()
    local seasonId = GameInstance.player.battlePassSystem.seasonData.seasonId
    local oriPreId = Tables.battlePassSeasonTable[seasonId].originiumPreviewGroupId
    return Tables.battlePassRewardPreviewTable[oriPreId].rewardInfos
end

function BattlePassUtils.GetPayTrackRewardInfo()
    local seasonId = GameInstance.player.battlePassSystem.seasonData.seasonId
    local payPreId = Tables.battlePassSeasonTable[seasonId].payPreviewGroupId
    return Tables.battlePassRewardPreviewTable[payPreId].rewardInfos
end

function BattlePassUtils.GetSeasonData()
    local seasonId = GameInstance.player.battlePassSystem.seasonData.seasonId
    local _, seasonData = Tables.battlePassSeasonTable:TryGetValue(seasonId)
    return seasonData
end

function BattlePassUtils.TimeCloseToEnd()
    local closeTime = GameInstance.player.battlePassSystem.seasonData.closeTime
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    return closeTime - curServerTime < Tables.battlePassConst.timeCloseToEnd
end

function BattlePassUtils.BuyPayTrack(onClose)
    if BattlePassUtils.CheckPayTrackActive() then
        return
    end

    local trackId = Tables.battlePassConst.buyPayTrackCashGoodsId
    local _, shopGroupData = Tables.cashShopGroupTable:TryGetValue(CashShopConst.CashShopCategoryType.Pack)
    if not shopGroupData or shopGroupData.cashShopIds.Count == 0 then
        logger.error("ShopRechargeCtrl.OnCreate no shop for id:", CashShopConst.CashShopCategoryType.Pack)
        return
    end

    if BattlePassUtils.TimeCloseToEnd() then
        Notify(MessageConst.SHOW_POP_UP,{
            content = string.format(Language.LUA_BATTLEPASS_BUY_TRACK_SUBTITLE, BattlePassUtils.GetSeasonLastDay()),
            onConfirm = function()
                UIManager:Close(PanelId.CommonPopUp)
                CashShopUtils.createOrder(trackId, CashShopUtils.GetCashShopIdByGoodsId(trackId), 1)
            end
        })
    else
        CashShopUtils.createOrder(trackId, CashShopUtils.GetCashShopIdByGoodsId(trackId), 1)
    end

    if onClose then
        onClose()
    end
end

function BattlePassUtils.BuyOriginiumTrack()
    if BattlePassUtils.CheckOriginiumTrackActive() then
        return
    end

    Notify(MessageConst.SHOW_POP_UP,{
        content = Language.LUA_BATTLEPASS_ORI_TRACK_BUY_TITLE,
        subContent = Language.LUA_BATTLEPASS_ORI_TRACK_BUY_SUBTITLE,
        onConfirm = function()
            UIManager:Close(PanelId.CommonPopUp)

            if Utils.getItemCount(Tables.battlePassConst.buyOriginiumTrackMoneyID, false) >= Tables.battlePassConst.buyOriginiumTrackMoneyCnt then
                if BattlePassUtils.TimeCloseToEnd() then

                    Notify(MessageConst.SHOW_POP_UP,{
                        content = Language.LUA_BATTLEPASS_ORI_TRACK_BUY_TITLE,
                        subContent = string.format(Language.LUA_BATTLEPASS_BUY_TRACK_SUBTITLE, BattlePassUtils.GetSeasonLastDay()),
                        onConfirm = function()
                            UIManager:Close(PanelId.CommonPopUp)
                            GameInstance.player.battlePassSystem:SendBuyOriginiumTrack()
                        end
                    })
                else

                    GameInstance.player.battlePassSystem:SendBuyOriginiumTrack()
                end
            else

                Notify(MessageConst.SHOW_POP_UP,{
                    content = Language.LUA_BATTLEPASS_ORIGINIUM_NOT_ENOUGH_TIPS,
                    onConfirm = function()
                        UIManager:Close(PanelId.CommonPopUp)
                        CashShopUtils.GotoCashShopRechargeTab()
                    end
                })
            end
        end
    })
end

function BattlePassUtils.AfterBuyPayTrack(ctrl, shouldClose, phaseId)






    local rewardPopupOpened = false
    ctrl:_StartCoroutine(function()
        while true do

            coroutine.step()
            if not rewardPopupOpened and UIManager:IsShow(PanelId.RewardsPopUpForSystem) then
                rewardPopupOpened = true
            elseif rewardPopupOpened and not UIManager:IsShow(PanelId.RewardsPopUpForSystem) then

                BattlePassUtils.ShowTrackReward(GEnums.BPTrackType.PAY, true,function()

                    if not BattlePassUtils.CheckOriginiumTrackActive() then

                        BattlePassUtils.ShowOriPlan(function()
                            if shouldClose then
                                if phaseId ~= nil then
                                    PhaseManager:PopPhase(phaseId)
                                else
                                    ctrl:PlayAnimationOutAndClose()
                                end
                            end
                        end)
                    else
                        if shouldClose then
                            if phaseId ~= nil then
                                PhaseManager:PopPhase(phaseId)
                            else
                                ctrl:PlayAnimationOutAndClose()
                            end
                        end
                    end
                end)
                break
            end
        end
    end)
end

function BattlePassUtils.ShowTrackReward(trackType, fromBuyPlan, onClose, onConfirm)

    local bpSystem = GameInstance.player.battlePassSystem
    local rewardBundle = {}
    local bpInfo = Tables.battlePassSeasonTable[bpSystem.seasonData.seasonId]
    local previewId = trackType == GEnums.BPTrackType.PAY and bpInfo.payPreviewGroupId or bpInfo.originiumPreviewGroupId
    local infos = Tables.battlePassRewardPreviewTable[previewId].rewardInfos
    for i = 1,#infos do
        local info = infos[CSIndex(i)]
        if info.finishLevel > 0 then
            table.insert(rewardBundle,{
                id = info.itemId,
                count = info.count,
                sortId = info.sortId,
            })
        end
    end
    table.sort(rewardBundle, Utils.genSortFunction({"sortId"}, true))


    local trackName = trackType == GEnums.BPTrackType.PAY and BattlePassUtils.GetPayTrackInfo().name or BattlePassUtils.GetOriginiumTrackInfo().name
    UIManager:Open(PanelId.BattlePassRecommend, {
        rewardBundle = rewardBundle,
        desc = string.format(Language.LUA_BATTLEPASS_AFTER_BUY_PAY_TRACK, trackName),
        fromBuyPlan = fromBuyPlan,
        onClose = function()
            if onClose ~= nil then
                onClose()
            end
        end,
        onConfirm = function()
            if onConfirm ~= nil then
                onConfirm()
            end
        end
    })
end

function BattlePassUtils.ShowOriPlan(onClose)
    Notify(MessageConst.SHOW_POP_UP, {
        content = string.format(Language.LUA_BATTLE_PASS_AFTER_BUY_PRO_DOUBLE_CONFIRM_TEXT, Tables.battlePassConst.buyOriginiumTrackMoneyCnt),
        onConfirm = function()

            PhaseManager:GoToPhase(PhaseId.BattlePassBuyPlan,{
                type = "Ori",
                onClose = onClose,
            })
        end
    })
end

function BattlePassUtils.GetBattlePassIntroVideoPath()
    local battlePassIntroFileName = Tables.battlePassConst.bpIntroVideoId
    return UIUtils.getUIVideoFullPath('BattlePass/' .. battlePassIntroFileName)
end

function BattlePassUtils.GetBattlePassOverrideLevelData(level, overrideLevelGroupId)
    local hasOverride, overrideLevelGroupData = Tables.battlePassOverrideLevelTable:TryGetValue(overrideLevelGroupId)
    if hasOverride then
        local hasOverrideLevel, overrideLevelData = overrideLevelGroupData.levelInfos:TryGetValue(level)
        if hasOverrideLevel then
            return overrideLevelData
        end
    end
    return nil
end



_G.BattlePassUtils = BattlePassUtils
return BattlePassUtils