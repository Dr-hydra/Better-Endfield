local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local Style = {
    DefaultMode = "DefaultMode",
    HunterMode = "HunterMode",
    HardMode = "HardMode",

    Lock = "Lock",
    Unlock = "Unlock",

    DefaultPassState = "DefaultPassState",
    Pass = "Pass",
    PerfectPass = "PerfectPass",
}

local HunterModeInstructionId = "hunter_mode"

DungeonCommonInfo = HL.Class('DungeonCommonInfo', UIWidgetBase)

DungeonCommonInfo.m_dungeonId = HL.Field(HL.String) << ""

DungeonCommonInfo.m_customArgs = HL.Field(HL.Table)

DungeonCommonInfo.m_detailRewardData = HL.Field(HL.Table)

DungeonCommonInfo.m_rewardCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonCommonInfo.m_dungeonGoalCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonCommonInfo.m_charAttributeCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonCommonInfo.m_paramBlackboard = HL.Field(CS.Beyond.Blackboard)
DungeonCommonInfo.m_paramBlackboardFormatData = HL.Field(CS.Beyond.Gameplay.BlackboardFormatData)


DungeonCommonInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.m_rewardCellCache = UIUtils.genCellCache(self.view.rewardCell)
    self.m_dungeonGoalCellCache = UIUtils.genCellCache(self.view.dungeonGoalCell)
    self.m_charAttributeCellCache = UIUtils.genCellCache(self.view.attriNode)
    self.m_paramBlackboard = CS.Beyond.Blackboard()
    self.m_paramBlackboardFormatData = CS.Beyond.Gameplay.BlackboardFormatData(self.m_paramBlackboard)

    self.view.tipsBtn.onClick:AddListener(function()
        self:_OnTipsBtnClick()
    end)

    self.view.btnEnemyDetails.onClick:AddListener(function()
        self:_OnBtnEnemyDetailsClick()
    end)

    self.view.btnRewardDetails.onClick:AddListener(function()
        self:_OnBtnRewardDetailsClick()
    end)

    self.view.btnSelectReward.onClick:AddListener(function()
        self:_OnBtnSelectRewardClick()
    end)

    self.view.btnDungeonEntry.onClick:AddListener(function()
        self:_OnBtnDungeonEntryClick()
    end)

    self.view.btnUnlockMultiCondition.onClick:AddListener(function()
        self:_OnBtnUnlockConditionClick()
    end)

    self:RegisterMessage(MessageConst.ON_STAMINA_CHANGED, function()
        self:_RefreshBottomStamina()
    end)

    self:RegisterMessage(MessageConst.ON_SUB_GAME_CUSTOM_REWARD_CHANGE, function()
        self:_RefreshDungeonRewards()
    end)

    local ids = { Tables.dungeonConst.staminaItemId }
    local doubleStaminaTicketItemId = Tables.dungeonConst.doubleStaminaTicketItemId
    if GameInstance.player.inventory:IsItemGot(doubleStaminaTicketItemId) then
        table.insert(ids, 1, doubleStaminaTicketItemId)
    end
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(ids)
end

DungeonCommonInfo.InitDungeonCommonInfo = HL.Method(HL.Table) << function(self, customArgs)
    self:_FirstTimeInit()
    self.m_customArgs = customArgs
end

DungeonCommonInfo.RefreshDungeonCommonInfo = HL.Method(HL.String) << function(self, dungeonId)
    self.m_dungeonId = dungeonId

    self:_RefreshCommonInfo()
    self:_RefreshWalletBar()

    self.view.animation:ClearTween()
    self.view.animation:PlayInAnimation()
end

DungeonCommonInfo._RefreshCommonInfo = HL.Method() << function(self)
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local dungeonMgr = GameInstance.dungeonManager
    local isPass = DungeonUtils.isDungeonPassed(self.m_dungeonId)
    local isUnlock = DungeonUtils.isDungeonUnlock(self.m_dungeonId)
    local hasFirstReward = not string.isEmpty(dungeonCfg.firstPassRewardId)
    local firstRewardGained = dungeonMgr:IsDungeonFirstPassRewardGained(self.m_dungeonId)
    local hasExtraReward = not string.isEmpty(dungeonCfg.extraRewardId)
    local extraRewardGained = dungeonMgr:IsDungeonExtraRewardGained(self.m_dungeonId)

    self.m_paramBlackboard:Clear()
    if dungeonCfg.paramList then
        for i = 1, dungeonCfg.paramList.Count do
            local param = dungeonCfg.paramList[CSIndex(i)]
            self.m_paramBlackboard:Assign(param.key, param.value)
        end
    end


    local hasHunterMode = DungeonUtils.isDungeonHasHunterMode(self.m_dungeonId)

    local hunterModeOpen = DungeonUtils.isHunterModeUnlocked()

    local haveHardMode = Tables.dungeonRaidTable:TryGetValue(self.m_dungeonId) or Tables.dungeonRaid2NormalTable:TryGetValue(self.m_dungeonId) or Tables.dungeonNormal2RaidTable:TryGetValue(self.m_dungeonId)
    local styleState = Style.DefaultMode
    if hasHunterMode then
        styleState = Style.HunterMode
    elseif haveHardMode then
        styleState = Style.HardMode
    else
        styleState = Style.DefaultMode
    end

    self.view.rightNode:SetState(styleState)


    self.view.rightNode:SetState(isUnlock and Style.Unlock or Style.Lock)


    self.view.dungeonTitleTxt.text = dungeonCfg.dungeonName

    local hasRecord, subGameRecord = GameInstance.player.subGameSys:TryGetSubGameRecord(self.m_dungeonId)
    local succ, subGameData = DataManager.subGameInstDataTable:TryGetValue(self.m_dungeonId)
    local hasTimeComp = succ and (subGameData.hasTimer or subGameData.hasTimeLimit)
    local showTimeInfo = Tables.dungeonTypeTable[dungeonCfg.dungeonCategory].showTimeRecord and hasTimeComp

    self.view.timeRecordNode.gameObject:SetActive(showTimeInfo)
    self.view.timeTxt.text = (hasRecord and isPass) and
            UIUtils.getLeftTimeToSecond(math.floor(subGameRecord.bestPassTime / 1000)) or "--:--:--"


    local hasLocation = not string.isEmpty(dungeonCfg.levelId)
    if hasLocation then
        local locationText = DungeonUtils.getEntryLocation(dungeonCfg.levelId, true)
        self.view.locationTxt.text = locationText
    end
    self.view.locationNode.gameObject:SetActive(hasLocation)


    local hasRecommendLv = dungeonCfg.recommendLv > 0
    if hasRecommendLv then
        self.view.recommendLvTxt.text = string.format("LV.%d", dungeonCfg.recommendLv)
    end
    self.view.recommendLv.gameObject:SetActive(hasRecommendLv)


    self.view.dungeonDescTxt:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(dungeonCfg.dungeonDesc))


    local hasFeature = DungeonUtils.isDungeonHasFeatureInfo(self.m_dungeonId)
    if hasFeature then
        self.view.featureTxt:SetAndResolveTextStyle(
            CS.Beyond.Gameplay.FormatUtils.FormatBattleText(dungeonCfg.featureDesc, self.m_paramBlackboardFormatData))
    end
    self.view.feature.gameObject:SetActive(hasFeature)


    local hasChar = not string.isEmpty(dungeonCfg.relatedCharId)
    if hasChar then
        self.view.charImg:LoadSprite(UIConst.UI_SPRITE_HOR_CHAR_HEAD,
                                     CSCharUtils.GetCharTemplateId(dungeonCfg.relatedCharId))
        local charCfg = Tables.characterTable[dungeonCfg.relatedCharId]
        local tagCount = #charCfg.charBattleTagIds
        self.m_charAttributeCellCache:Refresh(tagCount, function(cell, index)
            local _, tagName = Tables.charBattleTagTable:TryGetValue(charCfg.charBattleTagIds[CSIndex(index)])
            if tagName then
                cell.attriTxt.text = tagName
            end
        end)
    end
    self.view.charInfo.gameObject:SetActive(hasChar)



    local isTrain = DungeonUtils.isDungeonTrain(self.m_dungeonId)
    local goalTxt = isTrain and dungeonCfg.mainGoalDesc or dungeonCfg.extraGoalDesc
    local complete = isTrain and firstRewardGained or extraRewardGained
    local goalTxtTbl = DungeonUtils.getListByStr(goalTxt)
    if #goalTxtTbl > 0 then
        self.m_dungeonGoalCellCache:Refresh(#goalTxtTbl, function(cell, luaIndex)
            cell.goalTxt:SetAndResolveTextStyle(goalTxtTbl[luaIndex])
            cell.normalIcon.gameObject:SetActive(not complete)
            cell.finishedIcon.gameObject:SetActive(complete)
        end)
    end

    self.view.dungeonGoalInfoNode.gameObject:SetActive(#goalTxtTbl > 0)
    self.view.awardsTxt.gameObject:SetActive(not isTrain)
    self.view.trainTxt.gameObject:SetActive(isTrain)


    local sceneId = dungeonCfg.sceneId
    local gainedRewardChestNum, maxRewardChestNum = DungeonUtils.getDungeonChestCount(sceneId)
    local hasChest = not string.isEmpty(sceneId) and maxRewardChestNum > 0
    if hasChest then
        self.view.rewardChestNode.chestTxt.text = string.format("%d/%d", gainedRewardChestNum, maxRewardChestNum)
    end
    self.view.rewardChestNode.gameObject:SetActive(hasChest)


    local isPerfectPass = (not hasFirstReward or firstRewardGained) and
            (not hasExtraReward or extraRewardGained) and
            (not hasChest or gainedRewardChestNum >= maxRewardChestNum) and
            DungeonUtils.isDungeonChallenge(self.m_dungeonId)
    local passState = Style.DefaultPassState
    if isPerfectPass then
        passState = Style.PerfectPass
    elseif isPass then
        passState = Style.Pass
    else
        passState = Style.DefaultPassState
    end
    self.view.rightNode:SetState(passState)


    local hasEnemy = dungeonCfg.enemyIds.Count > 0
    self.view.enemyNode.gameObject:SetActive(hasEnemy)


    self:_RefreshDungeonRewards()

    local hasCustomReward = not string.isEmpty(dungeonCfg.customRewardId)
    self.view.rewardNode:SetState(hasCustomReward and "CustomReward" or "NormalReward ")

    self.view.materialDecoImage.gameObject:SetActive(hasCustomReward)
    self:_RefreshBottomStamina()


    if isUnlock then
        local showCostStamina = hasHunterMode and hunterModeOpen or not hasHunterMode and dungeonCfg.costStamina > 0
        local showCantGetHunterModeReward = hasHunterMode and not hunterModeOpen

        self.view.staminaNode.gameObject:SetActive(showCostStamina)
        self.view.hunterModeLockNode.gameObject:SetActive(showCantGetHunterModeReward)


        self.view.btnDungeonEntry.text = DungeonUtils.getEntryText(self.m_dungeonId)

        self.view.lockedNode.gameObject:SetActive(false)
        self.view.lockedSpNode.gameObject:SetActive(false)
    else

        if not dungeonCfg.unlockSpCond then


            local uncompletedConditionId = DungeonUtils.getUncompletedConditionIds(self.m_dungeonId)
            local multiUnComplete = #uncompletedConditionId > 1
            local haveCanJumpCond = false
            for _, conditionId in ipairs(uncompletedConditionId) do
                local canJump = DungeonUtils.getConditionCanJump(self.m_dungeonId, conditionId)
                if canJump then
                    haveCanJumpCond = true
                end
            end

            self.view.lockedNode.gameObject:SetActive(not multiUnComplete and not haveCanJumpCond)
            self.view.lockedSpNode.gameObject:SetActive(multiUnComplete or haveCanJumpCond)
            if not multiUnComplete and not haveCanJumpCond then

                local conditionId = uncompletedConditionId[1]
                local gameMechanicConditionCfg = Tables.gameMechanicConditionTable[conditionId]
                self.view.lockedTxt.text = gameMechanicConditionCfg.desc
            else

                if multiUnComplete then
                    self.view.lockedText.text = Language.ui_dungeon_entry_condition_many
                else
                    local conditionId = uncompletedConditionId[1]
                    local gameMechanicConditionCfg = Tables.gameMechanicConditionTable[conditionId]
                    self.view.lockedText.text = gameMechanicConditionCfg.desc
                end
            end
        else
            self.view.lockedNode.gameObject:SetActive(false)
            self.view.lockedSpNode.gameObject:SetActive(false)
        end
    end
    self.view.unlockedNode.gameObject:SetActive(isUnlock)
    if haveHardMode then
        local isHardRaid , hardRaidData = Tables.dungeonRaidTable:TryGetValue(self.m_dungeonId)
        local stateName = ""
        if isHardRaid then
            if hardRaidData.isRaid then
                stateName = "On"
            else
                stateName = "Off"
            end
        else

             local isRaid2Normal = Tables.dungeonRaid2NormalTable:TryGetValue(self.m_dungeonId)
             if isRaid2Normal then
                stateName = "On"
             else
                stateName = "Off"
             end
        end
        self.view.hardTogStateController:SetState(stateName)
    else
        self.view.hardModeNode.gameObject:SetActive(false)
    end
end

DungeonCommonInfo._GenRewardInfo = HL.Method(HL.String, HL.Number, HL.Number, HL.Boolean, HL.Boolean, HL.String,
                                             HL.Opt(HL.Number)).Return(HL.Table)
        << function(self, typeTag, groupId, rewardTypeSortId, locked, gained, itemId, itemCount)
    local itemCfg = Tables.itemTable[itemId]
    return {
        id = itemId,
        count = itemCount,
        locked = locked,
        gained = gained,
        typeTag = typeTag,

        lockedSortId = locked and 0 or 1,
        gainedSortId = gained and 0 or 1,
        groupId = groupId,
        rewardTypeSortId = rewardTypeSortId,
        sortId1 = itemCfg.sortId1,
        sortId2 = itemCfg.sortId2,
    }
end

DungeonCommonInfo._RefreshDungeonRewards = HL.Method() << function(self)
    local dungeonMgr = GameInstance.dungeonManager
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local rewards = {}


    local hasFirstReward = not string.isEmpty(dungeonCfg.firstPassRewardId)
    if hasFirstReward then
        local firstRewardGained = dungeonMgr:IsDungeonFirstPassRewardGained(self.m_dungeonId)
        local rewardsCfg = Tables.rewardTable[dungeonCfg.firstPassRewardId]
        for _, itemBundle in pairs(rewardsCfg.itemBundles) do
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.First, -1, -1, false,
                                               firstRewardGained, itemBundle.id, itemBundle.count)
            table.insert(rewards, reward)
        end
    end


    local hasExtraReward = not string.isEmpty(dungeonCfg.extraRewardId)
    if hasExtraReward then
        local extraRewardGained = dungeonMgr:IsDungeonExtraRewardGained(self.m_dungeonId)
        local rewardsCfg = Tables.rewardTable[dungeonCfg.extraRewardId]
        for _, itemBundle in pairs(rewardsCfg.itemBundles) do
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Extra, -2, -2, false,
                                               extraRewardGained, itemBundle.id, itemBundle.count)
            table.insert(rewards, reward)
        end
    end


    local hasHunterModeRewardId = not string.isEmpty(dungeonCfg.hunterModeRewardId)
    if hasHunterModeRewardId then
        local isHunterModeUnlocked = DungeonUtils.isHunterModeUnlocked()
        local rewardCfg = Tables.rewardTable[dungeonCfg.hunterModeRewardId]

        for _, itemBundle in pairs(rewardCfg.itemBundles) do
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Regular, -3, -3,
                                               not isHunterModeUnlocked, false, itemBundle.id)
            table.insert(rewards, reward)
        end


        for _, itemBundle in pairs(rewardCfg.probItemBundles) do
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Random, -3, -4,
                                               not isHunterModeUnlocked, false, itemBundle.id)
            table.insert(rewards, reward)
        end
    end



    local realRewardId
    local hasRecord, subGameRecord = GameInstance.player.subGameSys:TryGetSubGameRecord(self.m_dungeonId)
    if not string.isEmpty(dungeonCfg.customRewardId) then
        realRewardId = (hasRecord and subGameRecord.customRewardIndex == 1) and dungeonCfg.customRewardId or dungeonCfg.rewardId
    else
        realRewardId = dungeonCfg.rewardId
    end
    local hasRecycleReward = not string.isEmpty(realRewardId)
    if hasRecycleReward then
        local rewardsCfg = Tables.rewardTable[realRewardId]
        for _, itemBundle in pairs(rewardsCfg.itemBundles) do
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Regular, -5, -5, false,
                                               false, itemBundle.id, itemBundle.count)
            table.insert(rewards, reward)
        end
    end

    local sortKeys = UIConst.COMMON_ITEM_SORT_KEYS
    table.insert(sortKeys, 1, "rewardTypeSortId")
    table.insert(sortKeys, 1, "gainedSortId")
    table.sort(rewards, Utils.genSortFunction(sortKeys))

    local groupFlag
    self.m_rewardCellCache:Refresh(#rewards, function(cell, luaIndex)
        local reward = rewards[luaIndex]
        cell.itemSmall:InitItem(reward, true)
        cell.getNode.gameObject:SetActive(reward.gained == true)
        cell.lockNode.gameObject:SetActive(reward.locked == true)
        cell.lineNode.gameObject:SetActive(reward.groupId ~= groupFlag)
        groupFlag = reward.groupId
    end)
    self.view.rewardNode.gameObject:SetActive(#rewards > 0)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.container)
    self.view.rewardList.normalizedPosition = Vector2(0, 0)
end

DungeonCommonInfo._RefreshBottomStamina = HL.Method() << function(self)
    if not DungeonUtils.isDungeonUnlock(self.m_dungeonId) then
        self.view.staminaLaveNode.gameObject:SetActive(false)
        return
    end
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local isDungeonCostStamina = DungeonUtils.isDungeonCostStamina(self.m_dungeonId)

    local hasHunterMode = DungeonUtils.isDungeonHasHunterMode(self.m_dungeonId)
    local costStamina = hasHunterMode and dungeonCfg.hunterModeCostStamina or dungeonCfg.costStamina
    local activityInfo = ActivityUtils.getStaminaReduceInfo()
    local canReduceStamina = isDungeonCostStamina and ActivityUtils.hasStaminaReduceCount()

    UIUtils.updateStaminaNode(self.view.staminaNode, {
        costStamina = ActivityUtils.getRealStaminaCost(costStamina),
        descStamina = Language["ui_dungeon_details_ap_reuse"],
        delStamina = canReduceStamina and costStamina or nil
    })
    self.view.staminaNode.gameObject:SetActive(isDungeonCostStamina)

    self.view.laveNumTxt.text = string.format("%d/%d", activityInfo.totalCount - activityInfo.usedCount,
                                              activityInfo.totalCount)
    self.view.staminaLaveNode.gameObject:SetActive(canReduceStamina)
end

DungeonCommonInfo._RefreshWalletBar = HL.Method() << function(self)

    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local showWalletBar = DungeonUtils.isDungeonHasHunterMode(self.m_dungeonId) or dungeonCfg.costStamina > 0
    self.view.walletBarPlaceholder.gameObject:SetActive(showWalletBar)
end

DungeonCommonInfo._OnTipsBtnClick = HL.Method() << function(self)
    UIManager:Open(PanelId.InstructionBook, HunterModeInstructionId)
end

DungeonCommonInfo._OnBtnRewardDetailsClick = HL.Method() << function(self)
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local hasOptionReward = not string.isEmpty(dungeonCfg.customRewardId)
    local openPanelId = hasOptionReward and PanelId.DungeonRewardSelectPopup or PanelId.CommonRewardDetailsPopup


    local firstPartRewards = self.m_detailRewardData
    if firstPartRewards == nil or next(firstPartRewards) == nil then
        firstPartRewards = DungeonUtils.genFirstPartRewardsInfo(self.m_dungeonId)
    end
    local args = hasOptionReward and { dungeonId = self.m_dungeonId } or {
        firstPartRewards = firstPartRewards,
        firstPartRewardsTitle = DungeonUtils.getRewardsDetailFirstRowTitle(self.m_dungeonId),
        secondPartRewards = DungeonUtils.genSecondPartRewardsInfo(self.m_dungeonId),
        secondPartRewardsTitle = DungeonUtils.getRewardsDetailSecondRowTitle(self.m_dungeonId),
    }
    UIManager:AutoOpen(openPanelId, args)
end

DungeonCommonInfo._OnBtnSelectRewardClick = HL.Method() << function(self)
    UIManager:AutoOpen(PanelId.DungeonRewardSelectPopup, { dungeonId = self.m_dungeonId })
end

DungeonCommonInfo.SetRewardDetailsData = HL.Method(HL.Table) << function(self, data)
    self.m_detailRewardData = data
end

DungeonCommonInfo._OnBtnEnemyDetailsClick = HL.Method(HL.Opt(HL.Number)) << function(self, initSelectEnemyLuaIndex)
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local dungeonTypeCfg = Tables.dungeonTypeTable[dungeonCfg.dungeonCategory]
    local enemyPopupArg = {
        title = dungeonTypeCfg.enemyInfoTitle,
        enemyListTitle = Language["ui_dungeon_enemy_popup_info_list"],
        enemyInfoTitle = Language["ui_dungeon_enemy_popup_info_desc"],
        enemyIds = dungeonCfg.enemyIds,
        enemyLevels = dungeonCfg.enemyLevels,
        hideDamageTakenInfo = (dungeonCfg.dungeonCategory == "dungeon_highdifficulty")
    }
    if initSelectEnemyLuaIndex ~= nil then
        enemyPopupArg.initSelectEnemyLuaIndex = initSelectEnemyLuaIndex
    end
    UIManager:AutoOpen(PanelId.CommonEnemyPopup, enemyPopupArg)
end

DungeonCommonInfo.GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl.id == HunterModeInstructionId then
        return {
            popupType = "HunterModeInstructionBook",
        }
    end

    local isRewardSelectOpen, rewardSelectCtrl = UIManager:IsOpen(PanelId.DungeonRewardSelectPopup)
    if isRewardSelectOpen then
        return {
            popupType = "RewardDetails",
        }
    end

    local isRewardDetailsOpen, rewardDetailsCtrl = UIManager:IsOpen(PanelId.CommonRewardDetailsPopup)
    if isRewardDetailsOpen then
        return {
            popupType = "RewardDetails",
        }
    end

    local isEnemyPopupOpen, enemyPopupCtrl = UIManager:IsOpen(PanelId.CommonEnemyPopup)
    if isEnemyPopupOpen then
        local popupState = {
            popupType = "EnemyDetails",
        }
        if HL.TryGet(enemyPopupCtrl, "GetCurSelectEnemyLuaIndex") then
            local selectedEnemyLuaIndex = enemyPopupCtrl:GetCurSelectEnemyLuaIndex()
            if selectedEnemyLuaIndex > 0 then
                popupState.selectedEnemyLuaIndex = selectedEnemyLuaIndex
            end
        end
        return popupState
    end

    local isUnlockPopupOpen, unlockPopupCtrl = UIManager:IsOpen(PanelId.DungeonUnlockConditionPopup)
    if isUnlockPopupOpen then
        return {
            popupType = "UnlockCondition",
        }
    end
end


DungeonCommonInfo.TryRecoverPopupState = HL.Method(HL.Any) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return
    end

    if popupState.popupType == "HunterModeInstructionBook" then
        local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
        if isOpen and instructionCtrl.id == HunterModeInstructionId then
            return
        end
        self:_OnTipsBtnClick()
        return
    end

    if popupState.popupType == "RewardDetails" then
        local isRewardSelectOpen, rewardSelectCtrl = UIManager:IsOpen(PanelId.DungeonRewardSelectPopup)
        if isRewardSelectOpen then
            return
        end
        local isRewardDetailsOpen, rewardDetailsCtrl = UIManager:IsOpen(PanelId.CommonRewardDetailsPopup)
        if isRewardDetailsOpen then
            return
        end
        self:_OnBtnRewardDetailsClick()
        return
    end

    if popupState.popupType == "EnemyDetails" then
        local isOpen, enemyPopupCtrl = UIManager:IsOpen(PanelId.CommonEnemyPopup)
        if isOpen then
            return
        end
        local initSelectEnemyLuaIndex = popupState.selectedEnemyLuaIndex
        self:_OnBtnEnemyDetailsClick(initSelectEnemyLuaIndex)
        return
    end

    if popupState.popupType == "UnlockCondition" then
        local isOpen, unlockPopupCtrl = UIManager:IsOpen(PanelId.DungeonUnlockConditionPopup)
        if isOpen then
            return
        end
        self:_OnBtnUnlockConditionClick()
    end
end

DungeonCommonInfo._OnBtnDungeonEntryClick = HL.Method() << function(self)
    local isCostStamina, costStamina = DungeonUtils.isDungeonCostStamina(self.m_dungeonId)
    local realCostStamina = ActivityUtils.getRealStaminaCost(costStamina)
    if isCostStamina and realCostStamina > GameInstance.player.inventory.curStamina then
        DungeonUtils.TryShowDungeonInsufficientStaminaPopup(self.m_dungeonId, function()
            self:_OpenCharFormation()
        end)
    else
        self:_OpenCharFormation()
    end
end

DungeonCommonInfo._OpenCharFormation = HL.Virtual() << function(self)
    PhaseManager:GoToPhase(PhaseId.CharFormation, {
        dungeonId = self.m_dungeonId,
        enterDungeonCallback = self.m_customArgs and self.m_customArgs.enterDungeonCallback or nil,
        enterConfirmCallback = self.m_customArgs and self.m_customArgs.enterConfirmCallback or nil,
    })
end

DungeonCommonInfo._OnBtnUnlockConditionClick = HL.Method() << function(self)
    local uncompletedConditionIds = DungeonUtils.getUncompletedConditionIds(self.m_dungeonId)
    if #uncompletedConditionIds > 1 then
        UIManager:AutoOpen(PanelId.DungeonUnlockConditionPopup, { dungeonId = self.m_dungeonId })
    else
        DungeonUtils.diffActionByConditionId(uncompletedConditionIds[1])
    end
end

HL.Commit(DungeonCommonInfo)
return DungeonCommonInfo

