local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ChallengeDungeon
local PHASE_ID = PhaseId.ChallengeDungeon
ChallengeDungeonCtrl = HL.Class('ChallengeDungeonCtrl', uiCtrl.UICtrl)


local activitySystem = GameInstance.player.activitySystem

local dungeonManager = GameInstance.dungeonManager
local DungeonStateEnum = {
    Lock = 0,
    Unlock = 1,
    Complete = 2,
    PerfectComplete = 3,
}





ChallengeDungeonCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_SUB_GAME_UNLOCK] = '_OnSubGameUnlock',
    [MessageConst.ON_ACTIVITY_GAME_ENTRANCE_UNLOCK] = '_OnSeriesUnlock',
    [MessageConst.ON_SYSTEM_DISPLAY_SIZE_CHANGED] = '_OnDisplaySizeChanged',
}



ChallengeDungeonCtrl.m_info = HL.Field(HL.Table)

ChallengeDungeonCtrl.m_getTabCellFunc = HL.Field(HL.Function)

ChallengeDungeonCtrl.m_getDungeonCellFunc = HL.Field(HL.Function)

ChallengeDungeonCtrl.m_batchMarkListCache = HL.Field(HL.Forward("UIListCache"))

ChallengeDungeonCtrl.m_updateTimeCor = HL.Field(HL.Thread)

ChallengeDungeonCtrl.m_readRedDotDungeonTempList = HL.Field(HL.Table)

ChallengeDungeonCtrl.m_naviCellIndex = HL.Field(HL.Number) << 0




ChallengeDungeonCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_UpdateData()
    self:_RefreshAllUI()

    ActivityUtils.backToMainHudWhenActivityClosed(self, self.m_info.activityId)
end

ChallengeDungeonCtrl.OnClose = HL.Override() << function(self)
    self.m_updateTimeCor = self:_ClearCoroutine(self.m_updateTimeCor)

    local seriesInfo = self.m_info.seriesInfos[self.m_info.curSelectSeriesIndex]
    if ActivityUtils.isNewGameEntranceSeries(seriesInfo.seriesId) then
        ActivityUtils.setFalseNewGameEntranceSeries(seriesInfo.seriesId)
    end
    self:_TryClearReadDungeon()
end

ChallengeDungeonCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self:_AutoNavi()
end

ChallengeDungeonCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if not self.m_info then
        return nil
    end
    return {
        activityId = self.m_info.activityId,
        defaultSeriesIndex = self.m_info.curSelectSeriesIndex,
        defaultDungeonIndex = self.m_naviCellIndex,
    }
end



ChallengeDungeonCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_info = {
        activityId = "",
        curSelectSeriesIndex = -1,
        seriesInfos = {},
    }
    if type(arg) == "string" then
        self.m_info.activityId = arg
    else
        self.m_info.activityId = arg.activityId
        self.m_info.curSelectSeriesIndex = arg.defaultSeriesIndex or 0
        self.m_naviCellIndex = arg.defaultDungeonIndex or 0
    end

    self.m_readRedDotDungeonTempList = {}

    self:_InitSeriesInfo()
end
















ChallengeDungeonCtrl._InitSeriesInfo = HL.Method() << function(self)
    local _, seriesCfg = Tables.activityGameEntranceSeriesTable:TryGetValue(self.m_info.activityId)
    for seriesId, seriesSingleCfg in pairs(seriesCfg.seriesMap) do
        local canShow = false

        local activityData = activitySystem:GetActivity(self.m_info.activityId)
        if activityData ~= nil then

            local hasData, seriesData = activityData.seriesDataMap:TryGetValue(seriesId)
            if seriesData then
                canShow = seriesData.OpenTime <= DateTimeUtils.GetCurrentTimestampBySeconds()
            end
        end
        if canShow then
            local seriesInfo = {
                seriesId = seriesId,
                seriesName = seriesSingleCfg.name,
                sortId = seriesSingleCfg.sortId,
                allDungeonInfos = {

                },
                dungeonBatchInfos = {

                },
                totalDungeonCount = 0,
                achievementId = seriesSingleCfg.achieveId,

                isUnlock = false,
                openTime = "",
                perfectCompleteCount = 0,
                hasBatchIsLock = false,
                lockBatchIndex = 0,
            }
            table.insert(self.m_info.seriesInfos, seriesInfo)

            self:_InitAllDungeonInfo(seriesInfo)
            self:_InitDungeonBatchInfo(seriesInfo)
        end
    end
    table.sort(self.m_info.seriesInfos, Utils.genSortFunction({ "sortId" }, true))
end














ChallengeDungeonCtrl._InitAllDungeonInfo = HL.Method(HL.Table) << function(self, seriesInfo)
    local seriesId = seriesInfo.seriesId
    local _, activityGameCfg = Tables.activityGameEntranceGameTable:TryGetValue(seriesId)
    for _, gameCfg in pairs(activityGameCfg.gameList) do
        local dungeonId = gameCfg.gameId
        local _, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
        if dungeonCfg then

            local rewardInfos = {}

            local _, firstRewardCfg = Tables.rewardTable:TryGetValue(dungeonCfg.firstPassRewardId)
            if firstRewardCfg then
                for _, item in pairs(firstRewardCfg.itemBundles) do
                    local itemCfg = Tables.itemTable[item.id]
                    local rewardInfo = {
                        id = item.id,
                        count = item.count,
                        isFirst = true,
                        gainedSortId = 0,
                        typeSortId = 1,
                        raritySort = -itemCfg.rarity,
                        sortId1 = -itemCfg.sortId1,
                        sortId2 = itemCfg.sortId2,
                    }
                    table.insert(rewardInfos, rewardInfo)
                end
            end

            local _, extraRewardCfg = Tables.rewardTable:TryGetValue(dungeonCfg.extraRewardId)
            if extraRewardCfg then
                for _, item in pairs(extraRewardCfg.itemBundles) do
                    local itemCfg = Tables.itemTable[item.id]
                    local rewardInfo = {
                        id = item.id,
                        count = item.count,
                        isFirst = false,
                        gainedSortId = 0,
                        typeSortId = 2,
                        raritySort = -itemCfg.rarity,
                        sortId1 = -itemCfg.sortId1,
                        sortId2 = itemCfg.sortId2,
                    }
                    table.insert(rewardInfos, rewardInfo)
                end
            end

            local dungeonInfo = {
                dungeonId = dungeonId,
                sceneId = dungeonCfg.sceneId,
                dungeonName = dungeonCfg.dungeonName,
                timeOffset = gameCfg.timeOffset,
                rewardInfos = rewardInfos,
                hasFirstReward = firstRewardCfg ~= nil,
                hasExtraReward = extraRewardCfg ~= nil,

                dungeonState = DungeonStateEnum.Lock,
                unlockTime = 0,
                collectChestNum = 0,
                maxChestNum = 0,

                sortId = gameCfg.sortId,
            }
            table.insert(seriesInfo.allDungeonInfos, dungeonInfo)
        else
            logger.error(string.format("解密活动对应副本不存在，系列id：%s，副本id：%s", seriesId, dungeonId))
        end
    end
    seriesInfo.totalDungeonCount = #seriesInfo.allDungeonInfos
    table.sort(seriesInfo.allDungeonInfos, Utils.genSortFunction({ "sortId" }, true))
end









ChallengeDungeonCtrl._InitDungeonBatchInfo = HL.Method(HL.Table) << function(self, seriesInfo)
    local curBatchTimeOffset = -1

    local dungeonBatchInfo
    local allPreBatchDungeonCount = 0

    for _, dungeonInfo in pairs(seriesInfo.allDungeonInfos) do
        if curBatchTimeOffset ~= dungeonInfo.timeOffset then
            curBatchTimeOffset = dungeonInfo.timeOffset
            if dungeonBatchInfo ~= nil then
                allPreBatchDungeonCount = allPreBatchDungeonCount + #dungeonBatchInfo.dungeonInfos
            end
            dungeonBatchInfo = {
                timeOffset = curBatchTimeOffset,
                allPreBatchDungeonCount = allPreBatchDungeonCount,
                dungeonInfos = {},

                unlockTime = dungeonInfo.unlockTime,
            }
            table.insert(seriesInfo.dungeonBatchInfos, dungeonBatchInfo)
        end
        table.insert(dungeonBatchInfo.dungeonInfos, dungeonInfo)
    end
end

ChallengeDungeonCtrl._UpdateData = HL.Method() << function(self)
    local latestUnlockSeriesIndex = 0
    for index, seriesInfo in pairs(self.m_info.seriesInfos) do
        self:_UpdateSeriesInfo(seriesInfo)
        if seriesInfo.isUnlock then
            latestUnlockSeriesIndex = index
        end
    end
    if self.m_info.curSelectSeriesIndex < 1 then
        self.m_info.curSelectSeriesIndex = math.max(1, latestUnlockSeriesIndex)
    end
end

ChallengeDungeonCtrl._UpdateSeriesInfo = HL.Method(HL.Table) << function(self, seriesInfo)
    local seriesId = seriesInfo.seriesId

    local activityData = activitySystem:GetActivity(self.m_info.activityId)
    if activityData == nil then
        logger.error("ActivityData为空！ActivityId：" .. self.m_info.activityId)
        return
    end

    local hasData, seriesData = activityData.seriesDataMap:TryGetValue(seriesId)
    if not hasData then
        logger.error("副本活动数据缺失，系列id：" .. seriesId)
        return
    end

    seriesInfo.isUnlock = activitySystem:IsGameEntranceSeriesUnlock(seriesId)
    seriesInfo.openTime = seriesData.OpenTime


    self:_UpdateAllDungeonInfo(seriesInfo)

    self:_UpdateDungeonBatchInfo(seriesInfo)
end

ChallengeDungeonCtrl._UpdateAllDungeonInfo = HL.Method(HL.Table) << function(self, seriesInfo)
    seriesInfo.perfectCompleteCount = 0
    local rewardSortKeys = UIConst.COMMON_ITEM_SORT_KEYS
    table.insert(rewardSortKeys, 1, "typeSortId")
    table.insert(rewardSortKeys, 1, "gainedSortId")

    for _, dungeonInfo in pairs(seriesInfo.allDungeonInfos) do
        local dungeonId = dungeonInfo.dungeonId

        local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        dungeonInfo.unlockTime = seriesInfo.openTime + dungeonInfo.timeOffset * Const.SEC_PER_HOUR
        local isUnlock = dungeonManager:IsDungeonUnlocked(dungeonId) and curTime >= dungeonInfo.unlockTime
        local isComplete = dungeonManager:IsDungeonPassed(dungeonId)
        dungeonInfo.collectChestNum, dungeonInfo.maxChestNum = DungeonUtils.getDungeonChestCount(dungeonInfo.sceneId)
        local isGetFirstReward = not dungeonInfo.hasFirstReward or dungeonManager:IsDungeonFirstPassRewardGained(dungeonId)
        local isGetExtraReward = not dungeonInfo.hasExtraReward or dungeonManager:IsDungeonExtraRewardGained(dungeonId)
        local isGetAllChest = (dungeonInfo.maxChestNum < 1 or dungeonInfo.collectChestNum >= dungeonInfo.maxChestNum)
        local isPerfectComplete = isComplete and isGetFirstReward and isGetExtraReward and isGetAllChest
        if isUnlock then
            if isComplete then
                if isPerfectComplete then
                    dungeonInfo.dungeonState = DungeonStateEnum.PerfectComplete
                    seriesInfo.perfectCompleteCount = seriesInfo.perfectCompleteCount + 1
                else
                    dungeonInfo.dungeonState = DungeonStateEnum.Complete
                end
            else
                dungeonInfo.dungeonState = DungeonStateEnum.Unlock
            end
        else
            dungeonInfo.dungeonState = DungeonStateEnum.Lock
        end


        for _, rewardInfo in pairs(dungeonInfo.rewardInfos) do
            if rewardInfo.isFirst then
                rewardInfo.isGet = isGetFirstReward
                rewardInfo.gainedSortId = isGetFirstReward and 1 or 0
            else
                rewardInfo.isGet = isGetExtraReward
                rewardInfo.gainedSortId = isGetExtraReward and 1 or 0
            end
        end
        table.sort(dungeonInfo.rewardInfos, Utils.genSortFunction(rewardSortKeys, true))
    end
end

ChallengeDungeonCtrl._UpdateDungeonBatchInfo = HL.Method(HL.Table) << function(self, seriesInfo)
    local firstLockIndex = -1
    local batchCount = #seriesInfo.dungeonBatchInfos
    for index = 1, batchCount do

        local dungeonBatchInfo = seriesInfo.dungeonBatchInfos[index]
        local isLock = dungeonBatchInfo.dungeonInfos[1].dungeonState == DungeonStateEnum.Lock
        if isLock then
            firstLockIndex = index
            break
        end
    end
    seriesInfo.hasBatchIsLock = firstLockIndex > 0
    seriesInfo.lockBatchIndex = firstLockIndex
end



ChallengeDungeonCtrl._InitUI = HL.Method() << function(self)
    self.view.commonTopTitlePanel.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_getTabCellFunc = UIUtils.genCachedCellFunction(self.view.tabList)
    self.view.tabList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getTabCellFunc(obj)
        self:_RefreshTabCell(cell, LuaIndex(csIndex))
    end)
    self.m_getDungeonCellFunc = UIUtils.genCachedCellFunction(self.view.dungeonList)
    self.view.dungeonList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getDungeonCellFunc(obj)
        self:_RefreshDungeonCell(cell, LuaIndex(csIndex))
    end)
    self.m_batchMarkListCache = UIUtils.genCellCache(self.view.overviewNode.batchMark)
    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:_GetRedDotStateAt(csIndex)
    end

    local preActionId = self.view.keyHintLeft.actionId
    local nextActionId = self.view.keyHintRight.actionId
    self:BindInputPlayerAction(preActionId, function()
        local count = #self.m_info.seriesInfos
        local newIndex = (self.m_info.curSelectSeriesIndex + count - 2) % count + 1
        if newIndex ~= self.m_info.curSelectSeriesIndex then
            self:_OnChangeSeriesTab(newIndex)
            AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
            self.view.tabList:ScrollToIndex(CSIndex(newIndex))
        end
    end)
    self:BindInputPlayerAction(nextActionId, function()
        local count = #self.m_info.seriesInfos
        local newIndex = self.m_info.curSelectSeriesIndex % count + 1
        if newIndex ~= self.m_info.curSelectSeriesIndex then
            self:_OnChangeSeriesTab(newIndex)
            AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
            self.view.tabList:ScrollToIndex(CSIndex(newIndex))
        end
    end)
end

ChallengeDungeonCtrl._RefreshAllUI = HL.Method() << function(self)
    self.view.tabList:UpdateCount(math.max(#self.m_info.seriesInfos, 2))
    self:_RefreshContentUI(self.m_info.curSelectSeriesIndex)
end

ChallengeDungeonCtrl._RefreshContentUI = HL.Method(HL.Number) << function(self, seriesIndex)
    self.m_info.curSelectSeriesIndex = seriesIndex

    local seriesInfo = self.m_info.seriesInfos[seriesIndex]

    local progress = seriesInfo.perfectCompleteCount / seriesInfo.totalDungeonCount
    local overviewNode = self.view.overviewNode
    overviewNode.seriesNameTxt.text = seriesInfo.seriesName
    overviewNode.progressBar.fillAmount = progress
    overviewNode.curProgTxt.text = seriesInfo.perfectCompleteCount
    overviewNode.maxProgTxt.text = seriesInfo.totalDungeonCount

    local batchCount = #seriesInfo.dungeonBatchInfos

    self.m_batchMarkListCache:Refresh(batchCount, function(cell, luaIndex)
        local rotation = cell.transform.localEulerAngles

        local dungeonBatchInfo = seriesInfo.dungeonBatchInfos[luaIndex]
        rotation.z = dungeonBatchInfo.allPreBatchDungeonCount / seriesInfo.totalDungeonCount * -360
        cell.transform.localEulerAngles = rotation
    end)

    overviewNode.dungeonMedal:InitCommonMedalNode(seriesInfo.achievementId)

    self.m_updateTimeCor = self:_ClearCoroutine(self.m_updateTimeCor)
    if seriesInfo.isUnlock then
        self.view.mainStateCtrl:SetState("Unlock")
        local count = 0
        if seriesInfo.hasBatchIsLock then

            local dungeonBatchInfo = seriesInfo.dungeonBatchInfos[seriesInfo.lockBatchIndex]
            count = dungeonBatchInfo.allPreBatchDungeonCount + 1
        else
            count = seriesInfo.totalDungeonCount
        end
        self.view.dungeonList:UpdateCount(count, self.m_naviCellIndex)
    else
        self.view.mainStateCtrl:SetState("Lock")
    end

    self.m_updateTimeCor = self:_ClearCoroutine(self.m_updateTimeCor)
    self.view.keyHintContent.gameObject:SetActive(#self.m_info.seriesInfos > 1)

    if ActivityUtils.isNewGameEntranceSeries(seriesInfo.seriesId) then
        ActivityUtils.setFalseNewGameEntranceSeries(seriesInfo.seriesId)
    end
end

ChallengeDungeonCtrl._RefreshTabCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    if luaIndex > #self.m_info.seriesInfos then
        cell.stateCtrl:SetState("Empty")
        cell.btn.enabled = false
        cell.redDot:Stop()
        return
    end

    local seriesInfo = self.m_info.seriesInfos[luaIndex]

    cell.btn.enabled = true
    cell.nameTxt.text = seriesInfo.seriesName

    if seriesInfo.isUnlock then
        cell.stateCtrl:SetState("Unlock")
        if seriesInfo.perfectCompleteCount >= seriesInfo.totalDungeonCount then
            cell.stateCtrl:SetState("AllComplete")
        end
    else
        cell.stateCtrl:SetState("Lock")
    end

    cell.btn.onClick:RemoveAllListeners()
    cell.btn.onClick:AddListener(function()
        self:_OnChangeSeriesTab(luaIndex)
    end)

    local isSelect = self.m_info.curSelectSeriesIndex == luaIndex
    if isSelect then
        cell.animationWrapper:SampleClipAtPercent("challengedungeonselected_in", 1)
    else
        cell.animationWrapper:SampleClipAtPercent("challengedungeonselected_out", 1)
    end

    cell.redDot:InitRedDot("ActivityNormalChallengeSeries", seriesInfo.seriesId)
end

ChallengeDungeonCtrl._RefreshDungeonCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local seriesIndex = self.m_info.curSelectSeriesIndex

    local seriesInfo = self.m_info.seriesInfos[seriesIndex]

    local dungeonInfo = seriesInfo.allDungeonInfos[luaIndex]
    local dungeonId = dungeonInfo.dungeonId
    local isUnread = GameInstance.player.subGameSys:IsGameUnread(dungeonId)

    if dungeonInfo.dungeonState == DungeonStateEnum.Lock then
        cell.stateController:SetState("Lock")

        self.m_updateTimeCor = self:_ClearCoroutine(self.m_updateTimeCor)
        self.m_updateTimeCor = self:_StartCoroutine(function()
            while true do
                local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
                if curTime <= dungeonInfo.unlockTime then
                    local leftTime = dungeonInfo.unlockTime - curTime
                    cell.unlockTimeTxt.text = UIUtils.getLeftTime(leftTime)
                else
                    self.m_updateTimeCor = self:_ClearCoroutine(self.m_updateTimeCor)
                    cell.unlockTimeTxt.text = UIUtils.getLeftTime(0)
                end
                coroutine.wait(1)
            end
        end)
    else
        cell.stateController:SetState("Unlock")
        cell.titleTxt.text = dungeonInfo.dungeonName
        cell.gameObject.name = dungeonId
        if dungeonInfo.dungeonState == DungeonStateEnum.PerfectComplete then
            cell.stateController:SetState("PerfectComplete")
        elseif dungeonInfo.dungeonState == DungeonStateEnum.Complete then
            cell.stateController:SetState("Complete")
        else
            cell.stateController:SetState("UnComplete")
        end

        if cell.getRewardCellFunc == nil then

            cell.getRewardCellFunc = UIUtils.genCellCache(cell.rewardCell)
        end
        cell.getRewardCellFunc:Refresh(#dungeonInfo.rewardInfos, function(rewardCell, rewardLuaIndex)

            local rewardInfo = dungeonInfo.rewardInfos[rewardLuaIndex]
            rewardCell:InitItem(rewardInfo, function()
                UIUtils.showItemSideTips(rewardCell)
            end)
            rewardCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
            if DeviceInfo.usingController then
                rewardCell:SetEnableHoverTips(false)
            else
                rewardCell:SetEnableHoverTips(true)
            end

            rewardCell.view.rewardedCover.gameObject:SetActive(rewardInfo.isGet)
            rewardCell.view.extraTagStateCtrl.gameObject:SetActive(not rewardInfo.isGet)
            if not rewardInfo.isGet then
                if rewardInfo.isFirst then
                    rewardCell.view.extraTagStateCtrl:SetState("First")
                else
                    rewardCell.view.extraTagStateCtrl:SetState("Extra")
                end
            end
        end)
        local firstCell = cell.getRewardCellFunc:Get(1)
        cell.keyHintContent:SetParent(firstCell.view.transform)
        cell.keyHintContent.anchoredPosition = Vector2(-74, 0)
        cell.naviDecorator.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.m_naviCellIndex = CSIndex(luaIndex)
            end
        end

        if dungeonInfo.maxChestNum > 0 then
            cell.chestNumNode.gameObject:SetActive(true)
            cell.chestNumTxt.text = dungeonInfo.collectChestNum .. "/" .. dungeonInfo.maxChestNum
        else
            cell.chestNumNode.gameObject:SetActive(false)
        end

        cell.gotoBtn.onClick:RemoveAllListeners()
        cell.gotoBtn.onClick:AddListener(function()
            local enterDungeonCallback = function(enterDungeonId)
                LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId)
            end
            Notify(MessageConst.ON_OPEN_DUNGEON_ENTRY_PANEL, { dungeonId, enterDungeonCallback })
            if isUnread then
                GameInstance.player.subGameSys:SendSubGameListRead({ dungeonId })
            end
        end)
    end

    cell.redDot:InitRedDot("AdventureDungeonCell", { dungeonId }, nil, self.view.redDotScrollRect)
    if isUnread then
        table.insert(self.m_readRedDotDungeonTempList, dungeonId)
    end

    cell.animationWrapper:Play("challengedungeoncell_in")
end



ChallengeDungeonCtrl._OnSubGameUnlock = HL.Method(HL.Any) << function(self, arg)
    local dungeonId = unpack(arg)
    if DungeonUtils.isDungeonChallenge(dungeonId) then
        self:_UpdateData()
        self:_RefreshAllUI()
    end
end

ChallengeDungeonCtrl._OnSeriesUnlock = HL.Method(HL.Any) << function(self, arg)
    local seriesId = unpack(arg)
    for _, seriesInfo in pairs(self.m_info.seriesInfos) do
        if seriesInfo.seriesId == seriesId then
            self:_UpdateData()
            self:_RefreshAllUI()
            break
        end
    end
end

ChallengeDungeonCtrl._OnChangeSeriesTab = HL.Method(HL.Number) << function(self, luaIndex)
    local oldIndex = self.m_info.curSelectSeriesIndex
    if oldIndex ~= luaIndex then
        local oldObj = self.view.tabList:Get(CSIndex(oldIndex))
        local oldCell = self.m_getTabCellFunc(oldObj)
        if oldCell then
            oldCell.animationWrapper:Play("challengedungeonselected_out")
        end
        local newObj = self.view.tabList:Get(CSIndex(luaIndex))
        local newCell = self.m_getTabCellFunc(newObj)
        if newCell then
            newCell.animationWrapper:Play("challengedungeonselected_in")
        end
        self.m_naviCellIndex = 0
        self:_RefreshContentUI(luaIndex)
        self.view.dungeonNodeAniWrapper:Play("challengedungeonlist_change")

        self:_TryClearReadDungeon()
        self:_AutoNavi()
    end
end

ChallengeDungeonCtrl._TryClearReadDungeon = HL.Method() << function(self)
    if #self.m_readRedDotDungeonTempList > 0 then
        GameInstance.player.subGameSys:SendSubGameListRead(self.m_readRedDotDungeonTempList)
    end
    self.m_readRedDotDungeonTempList = {}
end

ChallengeDungeonCtrl._OnDisplaySizeChanged = HL.Method() << function(self)
    self:_RefreshAllUI()
end

ChallengeDungeonCtrl._AutoNavi = HL.Method() << function(self)
    local seriesInfo = self.m_info.seriesInfos[self.m_info.curSelectSeriesIndex]
    if seriesInfo and seriesInfo.isUnlock then
        local obj = self.view.dungeonList:Get(self.m_naviCellIndex)
        local firstCell = self.m_getDungeonCellFunc(obj)
        if firstCell then
            self:SetNaviTarget(firstCell.naviDecorator)
        end
    end
end

ChallengeDungeonCtrl._GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, csIndex)
    local luaIndex = CSIndex(csIndex)
    local seriesInfo = self.m_info.seriesInfos[self.m_info.curSelectSeriesIndex]
    if not seriesInfo then
        return 0
    end

    if luaIndex < 1 or luaIndex > #seriesInfo.allDungeonInfos then
        return 0
    end
    local dungeonInfo = seriesInfo.allDungeonInfos[luaIndex]
    if not dungeonInfo then
        return 0
    end
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("AdventureDungeonCell", { dungeonInfo.dungeonId })
    if hasRedDot then
        return redDotType
    else
        return 0
    end
end


HL.Commit(ChallengeDungeonCtrl)
