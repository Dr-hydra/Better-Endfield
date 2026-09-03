
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonSettlementPopup

local LevelScriptTaskType = CS.Beyond.Gameplay.LevelScriptTaskType
local RewardSourceType = CS.Beyond.GEnums.RewardSourceType

local PanelState = {
    ShowResult = 1,
    ShowRewards = 2,
}

local RESULT_2_REWARDS_CLIP = "dungeonsettlementpopup_change"

DungeonSettlementPopupCtrl = HL.Class('DungeonSettlementPopupCtrl', uiCtrl.UICtrl)






DungeonSettlementPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_STAMINA_CHANGED] = 'OnStaminaChanged',
}

DungeonSettlementPopupCtrl.s_cachedCompleteResult = HL.StaticField(HL.Table)

DungeonSettlementPopupCtrl.OnDungeonComplete = HL.StaticMethod(HL.Any) << function(args)
    local isNewTimeRecord, curGameTimeRecord = unpack(args)
    if DungeonSettlementPopupCtrl.s_cachedCompleteResult == nil then
        DungeonSettlementPopupCtrl.s_cachedCompleteResult = {}
    end

    DungeonSettlementPopupCtrl.s_cachedCompleteResult.isNewTimeRecord = isNewTimeRecord
    DungeonSettlementPopupCtrl.s_cachedCompleteResult.curGameTimeRecord = curGameTimeRecord
end

DungeonSettlementPopupCtrl.OnShowDungeonResult = HL.StaticMethod(HL.Any) << function(args)
    local dungeonId, leaveTimeDuration, useStaminaReduce = unpack(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("DungeonSettlement", function()
        if not Utils.isInDungeon() then


            logger.error(ELogChannel.Dungeon, "error, try to open settlement out of dungeon")
            return
        end


        PhaseManager:ExitPhaseFastTo(PhaseId.Level)


        local ctrl = UIManager:AutoOpen(PANEL_ID)
        ctrl:StartSettlement(dungeonId)

        if useStaminaReduce then

            ActivityUtils.showStaminaReduceProgress()
        end

    end, function()
        UIManager:Close(PANEL_ID)
    end)
end

DungeonSettlementPopupCtrl.m_panelState = HL.Field(HL.Number) << -1

DungeonSettlementPopupCtrl.m_dungeonId = HL.Field(HL.String) << ""

DungeonSettlementPopupCtrl.m_charInfoTable = HL.Field(HL.Table)

DungeonSettlementPopupCtrl.m_mainCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonSettlementPopupCtrl.m_extraCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonSettlementPopupCtrl.m_charHeadCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonSettlementPopupCtrl.m_getRewardItemCellFunc = HL.Field(HL.Function)

DungeonSettlementPopupCtrl.m_items = HL.Field(HL.Table)

DungeonSettlementPopupCtrl.m_canCloseSelf = HL.Field(HL.Boolean) << false

DungeonSettlementPopupCtrl.m_hasShowResultState = HL.Field(HL.Boolean) << false

DungeonSettlementPopupCtrl.m_leaveTick = HL.Field(HL.Number) << -1


DungeonSettlementPopupCtrl.m_pausedGameTimeForDoubleBattle = HL.Field(HL.Boolean) << false


DungeonSettlementPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnEmpty.onClick:AddListener(function()
        self:_OnBtnEmptyClick()
    end)

    self.view.btnConfirm.onClick:AddListener(function()
        self:_OnBtnEmptyClick()
    end)

    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.view.btnLeaveDungeon.onClick:AddListener(function()
        self:_OnBtnLeaveDungeonClick()
    end)

    self.view.btnRestartDungeon.onClick:AddListener(function()
        self:_OnBtnRestartDungeonClick()
    end)

    self.m_charInfoTable = {}
    self.m_mainCellCache = UIUtils.genCellCache(self.view.mainCell)
    self.m_extraCellCache = UIUtils.genCellCache(self.view.extraCell)
    self.m_getRewardItemCellFunc = UIUtils.genCachedCellFunction(self.view.rewardsScrollList)
    self.view.rewardsScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_OnUpdateCell(gameObject, csIndex)
    end)

    self.view.rewardsScrollList.onGraduallyShowFinish:AddListener(function()
        self:_OnGraduallyShowFinish()
    end)

    self:_InitController()
end

DungeonSettlementPopupCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.ON_DUNGEON_SETTLEMENT_OPENED)
end

DungeonSettlementPopupCtrl.OnHide = HL.Override() << function(self)
    Notify(MessageConst.ON_DUNGEON_SETTLEMENT_CLOSED)
end

DungeonSettlementPopupCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.HIDE_ITEM_TIPS)
    Notify(MessageConst.ON_DUNGEON_SETTLEMENT_CLOSED)

    if self.m_leaveTick then
        self.m_leaveTick = LuaUpdate:Remove(self.m_leaveTick)
    end


    self:_TryResumeGameTime()
end

DungeonSettlementPopupCtrl.OnAnimationInFinished = HL.Override() << function(self)
    local obj = self.view.rewardsScrollList:Get(0)
    if obj then
        local cell = self.m_getRewardItemCellFunc(obj)
        if cell then
            InputManagerInst:MoveVirtualMouseTo(cell.transform, self.uiCamera)
        end
    end
end

DungeonSettlementPopupCtrl._OnGraduallyShowFinish = HL.Method() << function(self)
    self.view.btnEmpty.gameObject:SetActive(false)
    self:_CheckCanCloseSelf()

    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder.gameObject:SetActive(true)
        self.view.focusItemKeyHint.gameObject:SetActive(true)
        local firstItemGo = self.view.rewardsScrollList:Get(0)
        if firstItemGo then
            self.view.focusItemKeyHint.transform.position = firstItemGo.transform.position
            local keyHintPos = self.view.focusItemKeyHint.transform.localPosition
            keyHintPos = keyHintPos + self.view.config.FOCUS_REWARDS_OFFSET
            self.view.focusItemKeyHint.transform.localPosition = keyHintPos
        end
    end
end

DungeonSettlementPopupCtrl._OnBtnEmptyClick = HL.Method() << function(self)
    if self.m_panelState == PanelState.ShowResult then
        self.m_panelState = PanelState.ShowRewards
        self:_ToggleRewardsState(true)
        self.animationWrapper:Play(RESULT_2_REWARDS_CLIP)
        self:_UpdateRewardsState()
        self:_TryResumeGameTime()
    elseif self.m_panelState == PanelState.ShowRewards then
        self:_OnClickSkipGraduallyShow()
    end
end

DungeonSettlementPopupCtrl._OnBtnCloseClick = HL.Method() << function(self)
    if not self.m_canCloseSelf then
        return
    end


    self:PlayAnimationOutWithCallback(function()
        self:Close()
        UIManager:Show(PanelId.DungeonCharTimeHint)
    end)
end

DungeonSettlementPopupCtrl._UpdateRewardsState = HL.Method() << function(self)
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local gameCategory = dungeonCfg.dungeonCategory
    local gameMechanicCategoryCfg = Tables.gameMechanicCategoryTable[gameCategory]

    self.view.btnRestartDungeon.gameObject:SetActiveIfNecessary(gameMechanicCategoryCfg.canReChallengeAfterReward)

    local rewardsCount = #self.m_items
    self.view.rewardsList.gameObject:SetActiveIfNecessary(rewardsCount > 0)
    self.view.emptyRewardNode.gameObject:SetActiveIfNecessary(rewardsCount == 0)
    self.view.rewardsScrollList:UpdateCount(rewardsCount, true)

    self.view.btnNode.gameObject:SetActiveIfNecessary(not self.m_canCloseSelf)
    self.view.infoDeco.gameObject:SetActiveIfNecessary(self.m_canCloseSelf)

    local showStaminaNode = DungeonUtils.isDungeonCostStamina(self.m_dungeonId)
    if showStaminaNode then
        self:_RefreshCostStamina()
    end
    self.view.staminaNode.gameObject:SetActiveIfNecessary(showStaminaNode)

    self.view.btnEmpty.gameObject:SetActiveIfNecessary(true)
end

DungeonSettlementPopupCtrl._RefreshCostStamina = HL.Method() << function(self)


    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local isHunterMode = DungeonUtils.isDungeonHasHunterMode(self.m_dungeonId)
    local costStamina = isHunterMode and dungeonCfg.hunterModeCostStamina or dungeonCfg.costStamina

    UIUtils.updateStaminaNode(self.view.staminaNode, {
        costStamina = ActivityUtils.getRealStaminaCost(costStamina),
        descStamina = Language["ui_dungeon_details_ap_refresh"],
        delStamina = ActivityUtils.hasStaminaReduceCount() and costStamina or nil
    })
end

DungeonSettlementPopupCtrl._ToggleRewardsState = HL.Method(HL.Boolean) << function(self, isRewardState)
    self.view.rewardsNode.gameObject:SetActiveIfNecessary(isRewardState)
    self.view.infoNode.gameObject:SetActiveIfNecessary(not isRewardState)


    self.view.btnNode.gameObject:SetActiveIfNecessary(isRewardState)
    self.view.titleTxt.text = isRewardState and Language.LUA_DUNGEON_SETTLEMENT_REWARDS_TITLE
            or Language.LUA_DUNGEON_SETTLEMENT_RESULT_TITLE
end

DungeonSettlementPopupCtrl._OnBtnRestartDungeonClick = HL.Method() << function(self)
    local restartFunc = function()
        self:PlayAnimationOutWithCallback(function()
            GameInstance.dungeonManager.curDungeonLikeSubGame:SendReStart(true)

            self:Close()
        end)
    end

    local isCostStamina, costStamina = DungeonUtils.isDungeonCostStamina(self.m_dungeonId)
    if isCostStamina then
        local cntStamina = GameInstance.player.inventory.curStamina
        local realCostStamina = ActivityUtils.getRealStaminaCost(costStamina)
        if cntStamina < realCostStamina then
            DungeonUtils.TryShowDungeonInsufficientStaminaPopup(self.m_dungeonId, restartFunc)
        else
            restartFunc()
        end
    else
        restartFunc()
    end
end

DungeonSettlementPopupCtrl._OnBtnLeaveDungeonClick = HL.Method() << function(self)

    self:Notify(MessageConst.HIDE_ITEM_TIPS)



    GameInstance.dungeonManager:LeaveDungeon()
end

DungeonSettlementPopupCtrl._OnClickSkipGraduallyShow = HL.Method() << function(self)
    if self.m_hasShowResultState then
        self.animationWrapper:SampleClipAtPercent(RESULT_2_REWARDS_CLIP, 1)
    else
        self.view.luaPanel.animationWrapper:SkipInAnimation()
    end

    self.view.rewardsScrollList:SkipGraduallyShow()
end

DungeonSettlementPopupCtrl._OnUpdateCell = HL.Method(GameObject, HL.Number) << function(self, go, csIndex)
    local cell = self.m_getRewardItemCellFunc(go)
    local index = LuaIndex(csIndex)
    local itemBundle = self.m_items[index]
    cell:InitItem(itemBundle, true)
    UIUtils.setRewardItemRarityGlow(cell, UIUtils.getItemRarity(itemBundle.id))
    cell:SetExtraInfo({
        isSideTips = DeviceInfo.usingController,
    })
end

DungeonSettlementPopupCtrl._GetRewardItems = HL.Method().Return(HL.Table) << function(self)
    local items = {}
    local firstPassRewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(RewardSourceType.DungeonFirstPass)
    if firstPassRewardPack and firstPassRewardPack.rewardSourceType == RewardSourceType.DungeonFirstPass then
        for _, itemBundle in pairs(firstPassRewardPack.itemBundleList) do
            local _, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemCfg then
                table.insert(items, {id = itemBundle.id,
                                     count = itemBundle.count,
                                     typeId = 4,
                                     typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.First,
                                     sortId1 = itemCfg.sortId1,
                                     sortId2 = itemCfg.sortId2,})
            end
        end
    end

    local extraRewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(RewardSourceType.DungeonExtraReward)
    if extraRewardPack and extraRewardPack.rewardSourceType == RewardSourceType.DungeonExtraReward then
        for _, itemBundle in pairs(extraRewardPack.itemBundleList) do
            local _, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemCfg then
                table.insert(items, {id = itemBundle.id,
                                     count = itemBundle.count,
                                     typeId = 3,
                                     typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.Extra,
                                     sortId1 = itemCfg.sortId1,
                                     sortId2 = itemCfg.sortId2,})
            end
        end
    end

    local mainRewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(RewardSourceType.DungeonMainReward)
    if mainRewardPack and mainRewardPack.rewardSourceType == RewardSourceType.DungeonMainReward then
        for _, itemBundle in pairs(mainRewardPack.itemBundleList) do
            local _, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemCfg then
                local typeTag = self:_TryGetRewardTypeTag(itemBundle.id)
                local typeId = typeTag == DungeonConst.DUNGEON_REWARD_TAG_STATE.Regular and 2 or 1
                table.insert(items, {id = itemBundle.id,
                                     count = itemBundle.count,
                                     typeId = typeId,
                                     typeTag = typeTag,
                                     sortId1 = itemCfg.sortId1,
                                     sortId2 = itemCfg.sortId2,})
            end
        end
    end

    local sortKeys = UIConst.COMMON_ITEM_SORT_KEYS
    table.insert(sortKeys, 1, "typeId")
    table.sort(items, Utils.genSortFunction(sortKeys))
    return items
end

DungeonSettlementPopupCtrl._TryGetRewardTypeTag = HL.Method(HL.String).Return(HL.Opt(HL.String))
        << function(self, itemId)
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]




    local realRewardCfg
    local hasRecord, subGameRecord = GameInstance.player.subGameSys:TryGetSubGameRecord(self.m_dungeonId)
    if not string.isEmpty(dungeonCfg.hunterModeRewardId) then
        realRewardCfg = Tables.rewardTable[dungeonCfg.hunterModeRewardId]
    elseif hasRecord then
        if subGameRecord.customRewardIndex == 0 then
            realRewardCfg = Tables.rewardTable[dungeonCfg.rewardId]
        elseif subGameRecord.customRewardIndex == 1 then
            realRewardCfg = Tables.rewardTable[dungeonCfg.customRewardId]
        end
    end

    if realRewardCfg == nil then
        return
    end

    for _, itemBundle in pairs(realRewardCfg.itemBundles) do
        if itemBundle.id == itemId then
            return DungeonConst.DUNGEON_REWARD_TAG_STATE.Regular
        end
    end

    for _, itemBundle in pairs(realRewardCfg.probItemBundles) do
        if itemBundle.id == itemId then
            return DungeonConst.DUNGEON_REWARD_TAG_STATE.Random
        end
    end
end

DungeonSettlementPopupCtrl._UpdateResultState = HL.Method() << function(self)
    local succ, subGameData = DataManager.subGameInstDataTable:TryGetValue(self.m_dungeonId)
    if not succ then
        return
    end


    local showTimeInfo = subGameData.hasTimer or subGameData.hasTimeLimit
    if showTimeInfo then
        local curGameTimeRecord = DungeonSettlementPopupCtrl.s_cachedCompleteResult.curGameTimeRecord
        local isNewTimeRecord = DungeonSettlementPopupCtrl.s_cachedCompleteResult.isNewTimeRecord

        self.view.curGameTimeTxt.text = UIUtils.getLeftTimeToSecond(math.floor(curGameTimeRecord / 1000))
        self.view.newTimeRecord.gameObject:SetActiveIfNecessary(isNewTimeRecord)
    end
    self.view.timeInfoNode.gameObject:SetActiveIfNecessary(showTimeInfo)

    local trackingMgr = GameWorld.levelScriptTaskTrackingManager

    if self:_IsFromDoubleAssaultBattle() then

        self.m_mainCellCache:Refresh(1, function(cell, luaIndex)
            self:_UpdateDungeonNameCell(cell)
        end)
        self.view.mainInfoNode.gameObject:SetActiveIfNecessary(true)
    else
        local mainTask = trackingMgr.mainTask
        local mainTaskCount = mainTask ~= nil and mainTask.objectives.Length or 0
        self.m_mainCellCache:Refresh(mainTaskCount, function(cell, luaIndex)
            self:_UpdateGoalCell(cell, luaIndex, LevelScriptTaskType.Main)
        end)
        self.view.mainInfoNode.gameObject:SetActiveIfNecessary(mainTaskCount > 0)
    end


    local extraTask = trackingMgr.extraTask
    local extraTaskCount = extraTask ~= nil and extraTask.objectives.Length or 0
    self.m_extraCellCache:Refresh(extraTaskCount, function(cell, luaIndex)
        self:_UpdateGoalCell(cell, luaIndex, LevelScriptTaskType.Extra)
    end)
    self.view.extraInfoNode.gameObject:SetActiveIfNecessary(extraTaskCount > 0)


    local sceneId = GameWorld.worldInfo.curLevelId
    local gainedRewardChestNum, maxRewardChestNum = DungeonUtils.getDungeonChestCount(sceneId)
    local hasChest = maxRewardChestNum > 0
    self.view.chestInfoNode.gameObject:SetActive(hasChest)
    if hasChest then
        self.view.chestInfoNode.chestNumTxt.text = string.format("%d/%d", gainedRewardChestNum, maxRewardChestNum)
    end

    if #self.m_charInfoTable > 0 then
        self.m_charHeadCellCache = UIUtils.genCellCache(self.view.charHeadCell)
        self.m_charHeadCellCache:Refresh(#self.m_charInfoTable, function(cell, luaIndex)
            local charInfo = self.m_charInfoTable[luaIndex]

            cell:InitCharFormationHeadCell({
                instId = charInfo.charInstId,
                templateId = charInfo.charId,
                noHpBar = false,
                isTrail = CharInfoUtils.checkIsCardInTrail(charInfo.charInstId),
            })
            local isAlive = charInfo.character ~= nil and charInfo.character.abilityCom.alive
            if isAlive then
                cell.view.charHeadBar.gameObject:SetActive(true)
                cell.view.addHpFill.gameObject:SetActive(false)
                cell.view.totalAddHpFill.gameObject:SetActive(false)
                cell.view.curHpFill.gameObject:SetActive(true)
                local abilityCom = charInfo.character.abilityCom
                cell.view.curHpFill.fillAmount = abilityCom.hp / abilityCom.maxHp
            end
        end)
        self.view.charInfoNode.gameObject:SetActive(true)
    else
        self.view.charInfoNode.gameObject:SetActive(false)
    end
end

DungeonSettlementPopupCtrl._UpdateGoalCell = HL.Method(HL.Any, HL.Number, CS.Beyond.Gameplay.LevelScriptTaskType)
        << function(self, cell, luaIndex, taskType)
    local trackingTask = GameWorld.levelScriptTaskTrackingManager:GetTaskByType(taskType)
    local csIndex = CSIndex(luaIndex)
    local obj = trackingTask.objectives[csIndex]
    local finished = obj.isCompleted

    local success, descText, progressText = trackingTask:TryGetValueObjectiveDescription(obj)
    cell.finishedIcon.gameObject:SetActiveIfNecessary(finished)
    cell.normalIcon.gameObject:SetActiveIfNecessary(not finished)
    cell.goalTxt.text = descText
    cell.stateTxt.text = progressText

    local succ, objTrackingInfo = trackingTask.extraInfo.trackingInfoDict:TryGetValue(obj.objectiveEnum)
    cell.stateTxt.gameObject:SetActiveIfNecessary(succ and objTrackingInfo.needFormatProgress)
end


DungeonSettlementPopupCtrl._UpdateDungeonNameCell = HL.Method(HL.Any) << function(self, cell)
    local _, dungeonCfg = Tables.dungeonTable:TryGetValue(self.m_dungeonId)
    local dungeonName = dungeonCfg and dungeonCfg.dungeonName or ""
    cell.goalTxt.text = string.format(Language.LUA_DUNGEON_SETTLEMENT_LEVEL_FINISHED, dungeonName)
    cell.stateTxt.gameObject:SetActive(false)
    cell.finishedIcon.gameObject:SetActive(true)
    cell.normalIcon.gameObject:SetActive(true)
end


DungeonSettlementPopupCtrl._TryGetSourceActivity = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    for aid, activityDungeonData in pairs(Tables.activityDungeonTable) do
        if activityDungeonData and activityDungeonData.gameMap and activityDungeonData.gameMap:ContainsKey(self.m_dungeonId) then
            local activity = GameInstance.player.activitySystem:GetActivity(aid)
            if activity then
                return activity
            end
        end
    end
    return nil
end


DungeonSettlementPopupCtrl._IsFromDoubleAssaultBattle = HL.Method().Return(HL.Boolean) << function(self)
    local activity = self:_TryGetSourceActivity()
    if activity == nil then
        return false
    end
    return GEnums.ActivityType.__CastFrom(activity.type) == GEnums.ActivityType.DoubleBattle
end


DungeonSettlementPopupCtrl._NeedFreezeGameTime = HL.Method().Return(HL.Boolean) << function(self)
    return self:_IsFromDoubleAssaultBattle()
end

DungeonSettlementPopupCtrl._CheckCanCloseSelf = HL.Method() << function(self)
    if not self.m_canCloseSelf then
        return
    end
    self.view.btnClose.gameObject:SetActiveIfNecessary(true)
end

DungeonSettlementPopupCtrl.StartSettlement = HL.Method(HL.String) << function(self, dungeonId)
    self.m_dungeonId = dungeonId


    local sourceActivity = self:_TryGetSourceActivity()
    if sourceActivity and sourceActivity.charTeam then
        local squad = GameInstance.player.squadManager.curSquad.slots
        for i = 0, squad.Count - 1 do
            local slot = squad[i]
            table.insert(self.m_charInfoTable, slot)
        end
    end


    if self:_NeedFreezeGameTime() then
        GameWorld.subGameManager:TryPauseGameTime(GEnums.GameTimeFreezeReason.UI)
        self.m_pausedGameTimeForDoubleBattle = true
    end


    self.m_items = self:_GetRewardItems()

    local needStamina = DungeonUtils.isDungeonCostStamina(self.m_dungeonId)
    if needStamina then
        local ids = { Tables.dungeonConst.staminaItemId }
        local doubleTicket = Tables.dungeonConst.doubleStaminaTicketItemId
        local hasGotDoubleTicket = GameInstance.player.inventory:IsItemGot(doubleTicket)
        if hasGotDoubleTicket then
            table.insert(ids, 1, doubleTicket)
        end
        self.view.walletBarPlaceholder:InitWalletBarPlaceholder(ids)
    end


    local needShowResult = DungeonUtils.isDungeonChallenge(self.m_dungeonId) or (self.m_charInfoTable ~= nil and #self.m_charInfoTable > 0)
    self.m_hasShowResultState = needShowResult

    self.m_canCloseSelf = DungeonUtils.isDungeonChar(self.m_dungeonId)
    self.view.btnEmpty.gameObject:SetActiveIfNecessary(self.m_canCloseSelf)
    self.view.btnClose.gameObject:SetActiveIfNecessary(false)

    if needShowResult then
        self.m_panelState = PanelState.ShowResult
    else
        self.m_panelState = PanelState.ShowRewards
    end

    if self.m_panelState == PanelState.ShowResult then
        self:_ToggleRewardsState(false)
        self:_UpdateResultState()
    elseif self.m_panelState == PanelState.ShowRewards then
        self:_ToggleRewardsState(true)
        self:_UpdateRewardsState()
        self:_TryResumeGameTime()
    end

    if self.m_canCloseSelf then
        UIManager:Open(PanelId.DungeonCharTimeHint)
        UIManager:Hide(PanelId.DungeonCharTimeHint)
    else

        self.m_leaveTick = DungeonUtils.startSubGameLeaveTick(function(leftTime)
            self.view.leaveTxt.text = tostring(leftTime) .. Language.LUA_LEAVE_DUNGEON_TEXT
        end)
    end
end

DungeonSettlementPopupCtrl.OnStaminaChanged = HL.Method() << function(self)
    self:_RefreshCostStamina()
end

DungeonSettlementPopupCtrl._TryResumeGameTime = HL.Method() << function(self)
    if self.m_pausedGameTimeForDoubleBattle then
        self.m_pausedGameTimeForDoubleBattle = false
        GameWorld.subGameManager:TryResumeGameTime(GEnums.GameTimeFreezeReason.UI)
    end
end

DungeonSettlementPopupCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.focusItemKeyHint.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    local rewardNaviGroup = self.view.rewardsScrollList.gameObject:GetComponent("UISelectableNaviGroup")
    rewardNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

HL.Commit(DungeonSettlementPopupCtrl)