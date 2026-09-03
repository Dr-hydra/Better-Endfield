
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BlackboxEntry
local PHASE_ID = PhaseId.BlackboxEntry
local RewardSourceType = CS.Beyond.GEnums.RewardSourceType

BlackboxEntryCtrl = HL.Class('BlackboxEntryCtrl', uiCtrl.UICtrl)

BlackboxEntryCtrl.m_arg = HL.Field(HL.Any)

BlackboxEntryCtrl.m_rewardCellCache = HL.Field(HL.Forward("UIListCache"))

BlackboxEntryCtrl.m_mainGoalCellCache = HL.Field(HL.Forward("UIListCache"))

BlackboxEntryCtrl.m_extraGoalCellCache = HL.Field(HL.Forward("UIListCache"))

BlackboxEntryCtrl.m_preDependencyCellCache = HL.Field(HL.Forward("UIListCache"))

BlackboxEntryCtrl.m_preDependenciesListFoldOut = HL.Field(HL.Boolean) << false

BlackboxEntryCtrl.m_packageId = HL.Field(HL.String) << ""

BlackboxEntryCtrl.m_allBlackboxInfos = HL.Field(HL.Table)

BlackboxEntryCtrl.m_curBlackboxInfos = HL.Field(HL.Table)

BlackboxEntryCtrl.m_curSelectCell = HL.Field(HL.Any)

BlackboxEntryCtrl.m_curSelectedBlackboxId = HL.Field(HL.String) << ""

BlackboxEntryCtrl.m_genBlackboxCellFunc = HL.Field(HL.Function)

BlackboxEntryCtrl.m_cachedSelectedTags = HL.Field(HL.Table)

BlackboxEntryCtrl.m_filterArgs = HL.Field(HL.Table)





BlackboxEntryCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DUNGEON_DIRECTLY_GET_REWARD] = 'OnBlackboxDirectlyGetReward',
}


BlackboxEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.view.btnEnter.onClick:AddListener(function()
        self:_OnBtnEnterClick()
    end)

    self.view.btnMap.onClick:AddListener(function()
        self:_OnBtnMapClick()
    end)

    self.view.btnPreDependencies.onClick:AddListener(function()
        self:_OnBtnPreDependenciesClick()
    end)

    self.view.btnRewardDetails.onClick:AddListener(function()
        UIManager:AutoOpen(PanelId.CommonRewardDetailsPopup, {
            firstPartRewards = DungeonUtils.genFirstPartRewardsInfo(self.m_curSelectedBlackboxId),
            secondPartRewards = DungeonUtils.genSecondPartRewardsInfo(self.m_curSelectedBlackboxId),
            secondPartRewardsTitle = DungeonUtils.getRewardsDetailSecondRowTitle(self.m_curSelectedBlackboxId),
        })
    end)

    self.view.btnCloseDependency.onClick:AddListener(function()
        self:_PreDependenciesFoldOut(false)
    end)

    self.view.btnCommonFilter.button.onClick:AddListener(function()
        self:_OnBtnFilterClick()
    end)

    self.view.blackboxScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_UpdateBlackboxCell(gameObject, csIndex)
    end)

    self.view.blackboxScrollList.onGraduallyShowFinish:AddListener(function()
        self:_OnGraduallyShowFinish()
    end)

    self.view.directlyGetRewardBtn.onClick:AddListener(function()
        self:_OnClickDirectlyGetRewardBtn()
    end)

    self.m_rewardCellCache = UIUtils.genCellCache(self.view.rewardCell)
    self.m_mainGoalCellCache = UIUtils.genCellCache(self.view.mainGoalCell)
    self.m_extraGoalCellCache = UIUtils.genCellCache(self.view.extraGoalCell)
    self.m_preDependencyCellCache = UIUtils.genCellCache(self.view.preDependencyCell)
    self.m_genBlackboxCellFunc = UIUtils.genCachedCellFunction(self.view.blackboxScrollList)

    self.m_packageId = arg.packageId
    self.m_curSelectedBlackboxId = arg.blackboxId or ""
    if not string.isEmpty(arg.resumeBlackboxId) then
        self.m_curSelectedBlackboxId = arg.resumeBlackboxId
        arg.resumeBlackboxId = nil
    end


    if self.view.redDotScrollRect then
        self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
            return self:GetRedDotStateAt(csIndex)
        end
    end

    self:_Init()
    self:_InitController()


    if arg.resumeSelectedTags then
        self:_OnFilterConfirm(arg.resumeSelectedTags)
        arg.resumeSelectedTags = nil
    end


    if arg.resumeRewardDetailsPopup then
        UIManager:AutoOpen(PanelId.CommonRewardDetailsPopup, {
            firstPartRewards = DungeonUtils.genFirstPartRewardsInfo(self.m_curSelectedBlackboxId),
            secondPartRewards = DungeonUtils.genSecondPartRewardsInfo(self.m_curSelectedBlackboxId),
            secondPartRewardsTitle = DungeonUtils.getRewardsDetailSecondRowTitle(self.m_curSelectedBlackboxId),
        })
        arg.resumeRewardDetailsPopup = nil
    end
end

BlackboxEntryCtrl._OnBtnCloseClick = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

BlackboxEntryCtrl._Init = HL.Method() << function(self)
    local packageCfg = Tables.facSTTGroupTable[self.m_packageId]
    local costPointItemCfg = Tables.itemTable[packageCfg.costPointType]
    local blackboxIds = packageCfg.blackboxIds

    self.view.seriesNameTxt.text = packageCfg.groupName
    self.view.rewardDescTxt.text = string.format(Language.LUA_BLACKBOX_COMPLETE_REWARD_FORMAT, costPointItemCfg.name)

    FactoryUtils.updateFacTechTreeTechPointNode(self.view.facTechPointBar, self.m_packageId)

    self.m_filterArgs = FactoryUtils.genFilterBlackboxArgs(self.m_packageId, function(selectedTags)
        self:_OnFilterConfirm(selectedTags)
    end)
    self.m_allBlackboxInfos = FactoryUtils.getBlackboxInfoTbl(blackboxIds, true)
    self.m_curBlackboxInfos = self.m_allBlackboxInfos
    self.m_curSelectedBlackboxId = string.isEmpty(self.m_curSelectedBlackboxId)
            and self.m_curBlackboxInfos[1].blackboxId or self.m_curSelectedBlackboxId
    local luaIndex = self:_FindLuaIndexInBlackboxInfos(self.m_curSelectedBlackboxId)
    self.view.blackboxScrollList:UpdateCount(#self.m_curBlackboxInfos, CSIndex(luaIndex))

    self:_ReadBlackbox(self.m_curSelectedBlackboxId)
    self:_RefreshDetails()
    self:_RefreshPreDependencies()
    self:_PreDependenciesFoldOut(false)
end

BlackboxEntryCtrl._RefreshDetails = HL.Method() << function(self)
    local gameMechanicData = Tables.gameMechanicTable[self.m_curSelectedBlackboxId]
    local dungeonCfg = Tables.dungeonTable[self.m_curSelectedBlackboxId]
    local isUnlock = DungeonUtils.isDungeonUnlock(self.m_curSelectedBlackboxId)
    local isActive = GameInstance.dungeonManager:IsDungeonActive(self.m_curSelectedBlackboxId)
    local dungeonMgr = GameInstance.dungeonManager
    local mainRewardGained = dungeonMgr:IsDungeonFirstPassRewardGained(self.m_curSelectedBlackboxId)
    local extraRewardGained = dungeonMgr:IsDungeonExtraRewardGained(self.m_curSelectedBlackboxId)

    self.view.detailsContent.gameObject:SetActiveIfNecessary(isActive and isUnlock)
    self.view.locked.gameObject:SetActiveIfNecessary(isActive and not isUnlock)
    self.view.notActivated.gameObject:SetActiveIfNecessary(not isActive)
    self.view.btnEnter.gameObject:SetActiveIfNecessary(isActive and isUnlock)
    self.view.btnMap.gameObject:SetActiveIfNecessary(not isActive)

    self.view.titleTxt.text = isActive and dungeonCfg.dungeonName or Language.LUA_FAC_TECH_TREE_BLACK_BOX_TBD
    self.view.positionTxt.text = DungeonUtils.getEntryLocation(dungeonCfg.levelId, false)
    self.view.positionNode.gameObject:SetActiveIfNecessary(not isActive)

    local manuallyPassed = dungeonMgr:IsDungeonManuallyPassed(self.m_curSelectedBlackboxId)
    self.view.titleCompleteNodeState:SetState(manuallyPassed and "ManuallyPassed" or "NotManuallyPassed")

    if not isUnlock then
        local conditionId = gameMechanicData.conditionIds[0]
        local conditionData = Tables.gameMechanicConditionTable[conditionId]
        self.view.unlockTxt:SetAndResolveTextStyle(conditionData.desc)
    else

        self.view.descTxt:SetAndResolveTextStyle(dungeonCfg.dungeonDesc)
        self.view.featureTxt:SetAndResolveTextStyle(dungeonCfg.featureDesc)


        local mainGoalTxt = DungeonUtils.getListByStr(dungeonCfg.mainGoalDesc)
        self.m_mainGoalCellCache:Refresh(#mainGoalTxt, function(cell, index)
            self:_UpdateGoalCell(cell, mainGoalTxt[index], mainRewardGained)
        end)
        self.view.mainTaskNode.gameObject:SetActiveIfNecessary(#mainGoalTxt > 0)
        self.view.mainUndone.gameObject:SetActiveIfNecessary(not mainRewardGained)
        self.view.mainDone.gameObject:SetActiveIfNecessary(mainRewardGained)

        local extraGoalTxt = DungeonUtils.getListByStr(dungeonCfg.extraGoalDesc)
        self.m_extraGoalCellCache:Refresh(#extraGoalTxt, function(cell, index)
            self:_UpdateGoalCell(cell, extraGoalTxt[index], extraRewardGained)
        end)
        self.view.extraTaskNode.gameObject:SetActiveIfNecessary(#extraGoalTxt > 0)
        self.view.extraUndone.gameObject:SetActiveIfNecessary(not extraRewardGained)
        self.view.extraDone.gameObject:SetActiveIfNecessary(extraRewardGained)



        local rewardItemBundles = {}
        local rewardId = gameMechanicData.firstPassRewardId
        local findReward, mainRewardData = Tables.rewardTable:TryGetValue(rewardId)
        if findReward then
            for _, itemBundle in pairs(mainRewardData.itemBundles) do
                local itemId = itemBundle.id
                local succ, itemCfg = Tables.itemTable:TryGetValue(itemId)
                if succ then
                    table.insert(rewardItemBundles, { id = itemId,
                                                      count = itemBundle.count,
                                                      done = mainRewardGained and 1 or 0,
                                                      isMain = 1,
                                                      sortId1 = itemCfg.sortId1,
                                                      sortId2 = itemCfg.sortId2, })
                else
                    logger.error("配置的RewardId中的ItemId在Item表中找不到", itemId, rewardId)
                end
            end
        elseif not string.isEmpty(rewardId) then
            logger.error("配置的首通奖励RewardId在Reward表中找不到：", rewardId)
        end

        local extraRewardId = gameMechanicData.extraRewardId
        local findExtraReward, extraRewardData = Tables.rewardTable:TryGetValue(extraRewardId)
        if findExtraReward then
            for _, itemBundle in pairs(extraRewardData.itemBundles) do
                local itemId = itemBundle.id
                local succ, itemCfg = Tables.itemTable:TryGetValue(itemId)
                if succ then
                    table.insert(rewardItemBundles, { id = itemId,
                                                      count = itemBundle.count,
                                                      done = extraRewardGained and 1 or 0,
                                                      isMain = 0,
                                                      sortId1 = itemCfg.sortId1,
                                                      sortId2 = itemCfg.sortId2, })
                else
                    logger.error("配置的RewardId中的ItemId在Item表中找不到", itemId, rewardId)

                end
            end
        elseif not string.isEmpty(extraRewardId) then
            logger.error("配置的附加奖励RewardId在Reward表中找不到：", extraRewardId)
        end
        local sortKeys = UIConst.COMMON_ITEM_SORT_KEYS
        table.insert(sortKeys, 1, "isMain")
        table.insert(sortKeys, 1, "done")
        table.sort(rewardItemBundles, Utils.genSortFunction(sortKeys))

        local maxRewardDisplayNum = 3
        local displayRewardNum = math.min(#rewardItemBundles, maxRewardDisplayNum)
        self.m_rewardCellCache:Refresh(displayRewardNum, function(rewardCell, luaIdx)
            local bundle = rewardItemBundles[luaIdx]
            rewardCell.item:InitItem(bundle, true)
            rewardCell.getNode.gameObject:SetActive(bundle.done == 1)
            rewardCell.extraTag.gameObject:SetActive(bundle.isMain == 0)
        end)
    end

    local dungeonFactoryCfg = Tables.dungeonFactoryTable[self.m_curSelectedBlackboxId]
    local canDirectlyGetReward = dungeonFactoryCfg.canDirectlyGetRewardForBlackbox
    local hasFirstPassReward = not string.isEmpty(dungeonCfg.firstPassRewardId)
    local hasExtraPassReward = not string.isEmpty(dungeonCfg.extraRewardId)

    local state
    if not canDirectlyGetReward and not manuallyPassed then
        state = "CantDirectlyGetReward"
    elseif hasFirstPassReward and not mainRewardGained or
            hasExtraPassReward and not extraRewardGained then
        state = "CanDirectlyGetReward"
    else
        state = "Normal"
    end
    self.view.detailsContent:SetState(state)
end

BlackboxEntryCtrl._RefreshPreDependencies = HL.Method() << function(self)
    local blackboxData = Tables.dungeonFactoryTable[self.m_curSelectedBlackboxId]
    local preDependencies = FactoryUtils.getBlackboxInfoTbl(blackboxData.preDependencies, true)
    local hasPreDependencies = #preDependencies > 0
    if hasPreDependencies then
        self.m_preDependencyCellCache:Refresh(#preDependencies, function(cell, luaIndex)
            self:_OnUpdatePreDependencyCell(cell, preDependencies[luaIndex])
        end)
        self.view.preDependenciesRedDot:InitRedDot("BlackboxPreDependencies", blackboxData.preDependencies)
    end
    local isUnlock = DungeonUtils.isDungeonUnlock(self.m_curSelectedBlackboxId)
    local isActive = GameInstance.dungeonManager:IsDungeonActive(self.m_curSelectedBlackboxId)
    self.view.preDependenciesNode.gameObject:SetActiveIfNecessary(hasPreDependencies and isUnlock and isActive)
end

BlackboxEntryCtrl._OnUpdatePreDependencyCell = HL.Method(HL.Forward("BlackboxSelectionCell"), HL.Table)
        << function(self, cell, info)
    cell:InitBlackboxSelectionCell(info.blackboxId, function()
        self:_OnPreDependencyCellClick(info.blackboxId)
    end, "BlackboxSelectionCellPassed")
end

BlackboxEntryCtrl._FindLuaIndexInBlackboxInfos = HL.Method(HL.String).Return(HL.Opt(HL.Number))
        << function(self, blackboxId)
    local luaIndex
    for index, blackboxInfo in ipairs(self.m_curBlackboxInfos) do
        if blackboxInfo.blackboxId == blackboxId then
            luaIndex = index
        end
    end
    return luaIndex
end




BlackboxEntryCtrl.GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_curBlackboxInfos then
        return 0
    end

    local blackboxInfo = self.m_curBlackboxInfos[luaIndex]
    if not blackboxInfo then
        return 0
    end

    local hasRedDot, redDotType, expireTs = RedDotManager:GetRedDotState("BlackboxSelectionCellRead", blackboxInfo.blackboxId)
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end

BlackboxEntryCtrl._OnPreDependencyCellClick = HL.Method(HL.String) << function(self, blackboxId)
    local luaIndex = self:_FindLuaIndexInBlackboxInfos(blackboxId)
    if luaIndex == nil then
        return
    end

    self.m_curSelectedBlackboxId = blackboxId
    self:_RefreshDetails()
    self:_RefreshPreDependencies()
    self:_PreDependenciesFoldOut(false)

    self.m_curSelectCell:SetSelected(false)

    self.view.blackboxScrollList:ScrollToIndex(CSIndex(luaIndex), true)

    local cell = self.m_genBlackboxCellFunc(luaIndex)
    if cell then

        self.m_curSelectCell = cell
        cell:SetSelected(true)
    end

    if DeviceInfo.usingController then
        self:SetNaviTarget(cell.view.button)
    end
end

BlackboxEntryCtrl._OnBtnEnterClick = HL.Method() << function(self)
    if Utils.isCurSquadAllDead() then

        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH)
        return
    end

    local blackboxData = Tables.dungeonFactoryTable[self.m_curSelectedBlackboxId]
    local dungeonMgr = GameInstance.dungeonManager
    local needConfirm = false
    for _, preDependency in pairs(blackboxData.preDependencies) do
        if not dungeonMgr:IsDungeonPassed(preDependency) then
            needConfirm = true
            break
        end
    end

    local uiRestore = function()
        LuaSystemManager.uiRestoreSystem:AddRequest(self.m_curSelectedBlackboxId)
    end

    if needConfirm then
        local content = Language.LUA_BLACKBOX_START_CONFIRM_DESC.."\n"
                ..Language.LUA_BLACKBOX_START_SUB_CONFIRM_DESC
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = content,
            onConfirm = function()
                if GameInstance.dungeonManager:TryReqEnterDungeon(self.m_curSelectedBlackboxId) then
                    uiRestore()
                end
            end,
        })
    else
        if GameInstance.dungeonManager:TryReqEnterDungeon(self.m_curSelectedBlackboxId) then
            uiRestore()
        end
    end
end

BlackboxEntryCtrl._OnBtnMapClick = HL.Method() << function(self)
    local dungeonCfg = Tables.dungeonTable[self.m_curSelectedBlackboxId]
    local _, instId = GameInstance.player.mapManager:GetMapMarkInstId(GEnums.MarkType.BlackBox, dungeonCfg.dungeonSeriesId)
    MapUtils.openMap(instId)
end

BlackboxEntryCtrl._OnBtnPreDependenciesClick = HL.Method() << function(self)
    self:_PreDependenciesFoldOut(not self.m_preDependenciesListFoldOut)
end

BlackboxEntryCtrl._OnClickDirectlyGetRewardBtn = HL.Method() << function(self)

    if not string.isEmpty(GameWorld.worldInfo.curSubGameId) then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_INVALID_SYSTEM_COMMON_DESCRIPTION)
        return
    end

    local dungeonCfg = Tables.dungeonTable[self.m_curSelectedBlackboxId]
    local hintText = string.format(Language["ui_fac_tech_tree_blackbox_complete_confirm"], dungeonCfg.dungeonName)

    self:Notify(MessageConst.SHOW_POP_UP, {
        content = hintText,
        onConfirm = function()
            GameInstance.dungeonManager:SendReqDirectlyGetReward(self.m_curSelectedBlackboxId)
        end,
    })
end

BlackboxEntryCtrl._PreDependenciesFoldOut = HL.Method(HL.Boolean) << function(self, foldOut)
    self.m_preDependenciesListFoldOut = foldOut

    self.view.preDependencies.gameObject:SetActiveIfNecessary(foldOut)

    if DeviceInfo.usingController then
        if foldOut then
            self.view.preDependenciesList:ManuallyFocus()
        else
            self.view.preDependenciesList:ManuallyStopFocus()
        end
    end
end

BlackboxEntryCtrl._UpdateGoalCell = HL.Method(HL.Any, HL.String, HL.Boolean)
        << function(self, cell, text, done)

    done = false

    cell.descTxt:SetAndResolveTextStyle(text)
    cell.done.gameObject:SetActiveIfNecessary(done)
    cell.undone.gameObject:SetActiveIfNecessary(not done)
end

BlackboxEntryCtrl._UpdateBlackboxCell = HL.Method(GameObject, HL.Number) << function(self, gameObject, csIndex)

    local cell = self.m_genBlackboxCellFunc(gameObject)
    local luaIndex = LuaIndex(csIndex)
    local blackboxInfo = self.m_curBlackboxInfos[luaIndex]

    cell:InitBlackboxSelectionCell(blackboxInfo.blackboxId, function()
        self:_OnBlackboxCellClick(cell, luaIndex)
    end, "BlackboxSelectionCellRead")
    cell:SetSelected(self.m_curSelectedBlackboxId == blackboxInfo.blackboxId, true)
    cell:PlayAnimationIn()
    cell.gameObject.name = blackboxInfo.blackboxId
    if self.m_curSelectedBlackboxId == blackboxInfo.blackboxId then
        self.m_curSelectCell = cell
    end
end

BlackboxEntryCtrl._OnGraduallyShowFinish = HL.Method() << function(self)
    if DeviceInfo.usingController then
        local luaIndex = self:_FindLuaIndexInBlackboxInfos(self.m_curSelectedBlackboxId)

        local cell = self.m_genBlackboxCellFunc(luaIndex)
        self:SetNaviTarget(cell.view.button)
    end
end

BlackboxEntryCtrl._OnBlackboxCellClick = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local blackboxInfo = self.m_curBlackboxInfos[luaIndex]


    if not blackboxInfo or blackboxInfo.blackboxId == self.m_curSelectedBlackboxId then
        return
    end

    local preCell = self.m_curSelectCell
    self.m_curSelectCell = cell
    self.m_curSelectedBlackboxId = blackboxInfo.blackboxId

    preCell:SetSelected(false)
    cell:SetSelected(true)

    self:_ReadBlackbox(blackboxInfo.blackboxId)
    self:_RefreshDetails()
    self:_RefreshPreDependencies()
    self:_PreDependenciesFoldOut(false)

    local wrapper = self.animationWrapper
    wrapper:ClearTween()
    wrapper:Play("blackboxentry_change")
end

BlackboxEntryCtrl._OnBtnFilterClick = HL.Method() << function(self)
    self.m_filterArgs.selectedTags = self.m_cachedSelectedTags
    self:Notify(MessageConst.SHOW_COMMON_FILTER, self.m_filterArgs)
end

BlackboxEntryCtrl._OnFilterConfirm = HL.Method(HL.Table) << function(self, selectedTags)

    selectedTags = selectedTags or {}
    self.m_cachedSelectedTags = selectedTags

    local ids = FactoryUtils.getFilterBlackboxIds(self.m_packageId, selectedTags)
    self.m_curBlackboxInfos = FactoryUtils.getBlackboxInfoTbl(ids, true)

    local luaIndex = self:_FindLuaIndexInBlackboxInfos(self.m_curSelectedBlackboxId)
    if luaIndex == nil then
        if #self.m_curBlackboxInfos > 0 then
            luaIndex = 1
            self.m_curSelectedBlackboxId = self.m_curBlackboxInfos[1].blackboxId
        else
            luaIndex = 0
            self.m_curSelectedBlackboxId = ""
        end
    end
    if DeviceInfo.usingController then

        self:ClearNaviTarget()
    end
    self.view.blackboxScrollList:UpdateCount(#self.m_curBlackboxInfos, luaIndex > 0 and CSIndex(luaIndex) or -1)


    local hasFilterResult = #ids > 0
    self.view.blackboxScrollList.gameObject:SetActiveIfNecessary(hasFilterResult)
    self.view.filterResultEmpty.gameObject:SetActiveIfNecessary(not hasFilterResult)
    self.view.blackboxDetailsNode.gameObject:SetActiveIfNecessary(hasFilterResult)
    local hasFilter = #selectedTags > 0
    self.view.btnCommonFilter.normalNode.gameObject:SetActiveIfNecessary(not hasFilter)
    self.view.btnCommonFilter.existNode.gameObject:SetActiveIfNecessary(hasFilter)

    local percent = hasFilterResult and 1 or 0
    local wrapper = self.animationWrapper
    wrapper:SampleClipAtPercent("blackboxentry_change", percent)
    if not hasFilterResult then


        local canvsGroup = self.view.filterResultEmpty.gameObject:GetComponent("CanvasGroup")
        if canvsGroup then
            canvsGroup.alpha = 1
        end
    end

    if not string.isEmpty(self.m_curSelectedBlackboxId) then
        self:_RefreshDetails()
    end
end

BlackboxEntryCtrl._ReadBlackbox = HL.Method(HL.String) << function(self, blackboxId)
    local dungeonMgr = GameInstance.dungeonManager
    if not dungeonMgr:IsDungeonActive(blackboxId) then
        return
    end

    if dungeonMgr:IsBlackboxRead(blackboxId) then
        return
    end

    dungeonMgr:ReadBlackbox(blackboxId)
end

BlackboxEntryCtrl.OnBlackboxDirectlyGetReward = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshDetails()

    local curSelectLuaIndex
    for luaIndex, blackboxInfo in ipairs(self.m_curBlackboxInfos) do
        if blackboxInfo.blackboxId == self.m_curSelectedBlackboxId then
            curSelectLuaIndex = luaIndex
            break
        end
    end

    if not curSelectLuaIndex then
        self.view.blackboxScrollList:UpdateCount(#self.m_curBlackboxInfos)
    else
        self.view.blackboxScrollList:UpdateCount(#self.m_curBlackboxInfos, CSIndex(curSelectLuaIndex))
    end


    local isOpen, ctr = UIManager:IsOpen(PanelId.AdventureBlackbox)
    if isOpen then
        ctr:_RefreshAllUI()
    end

    local firstPassRewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(RewardSourceType.DungeonFirstPass)
    local items = {}
    local needTriggerSysBlueprint = false
    if firstPassRewardPack and firstPassRewardPack.rewardSourceType == RewardSourceType.DungeonFirstPass then
        for _, itemBundle in pairs(firstPassRewardPack.itemBundleList) do
            local _, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemCfg then






                table.insert(items, { id = itemBundle.id,
                                      count = itemBundle.count,
                                      sortId1 = itemCfg.sortId1,
                                      sortId2 = itemCfg.sortId2 })
                if itemCfg.type == GEnums.ItemType.SysBluePrint then
                    needTriggerSysBlueprint = true
                end
            end
        end
    end

    local extraRewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(RewardSourceType.DungeonExtraReward)
    if extraRewardPack and extraRewardPack.rewardSourceType == RewardSourceType.DungeonExtraReward then
        for _, itemBundle in pairs(extraRewardPack.itemBundleList) do
            local _, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemCfg then






                if #items > 0 then
                    local cacheExitItem
                    for _, exitItem in ipairs(items) do
                        if exitItem.id == itemBundle.id then
                            cacheExitItem = exitItem
                            break
                        end
                    end

                    if cacheExitItem ~= nil then
                        local curCount = cacheExitItem.count
                        cacheExitItem.count = curCount + itemBundle.count
                    else
                        table.insert(items, { id = itemBundle.id,
                                              count = itemBundle.count,
                                              sortId1 = itemCfg.sortId1,
                                              sortId2 = itemCfg.sortId2, })
                    end
                else
                    table.insert(items, { id = itemBundle.id,
                                          count = itemBundle.count,
                                          sortId1 = itemCfg.sortId1,
                                          sortId2 = itemCfg.sortId2, })
                end
            end
        end
    end



    table.sort(items, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {

        items = items,
        onGraduallyShowFinishItemList = function()
            local succ, dungeonFactoryCfg = Tables.dungeonFactoryTable:TryGetValue(self.m_curSelectedBlackboxId)
            if needTriggerSysBlueprint and succ and not string.isEmpty(dungeonFactoryCfg.afterDirectlyGetRewardGuideGroupId) then


                GameAction.ManuallyStartGuideGroup(dungeonFactoryCfg.afterDirectlyGetRewardGuideGroupId)
            end
        end
    })
end

BlackboxEntryCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = lume.deepCopy(self.m_arg)
    arg.resumeBlackboxId = self.m_curSelectedBlackboxId

    if self.m_cachedSelectedTags and #self.m_cachedSelectedTags > 0 then
        arg.resumeSelectedTags = self.m_cachedSelectedTags
    end

    local isRewardDetailsOpen = UIManager:IsOpen(PanelId.CommonRewardDetailsPopup) and PhaseManager:GetTopPhaseId() == PHASE_ID
    if isRewardDetailsOpen then
        arg.resumeRewardDetailsPopup = true
    end
    return arg
end



BlackboxEntryCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end



HL.Commit(BlackboxEntryCtrl)
