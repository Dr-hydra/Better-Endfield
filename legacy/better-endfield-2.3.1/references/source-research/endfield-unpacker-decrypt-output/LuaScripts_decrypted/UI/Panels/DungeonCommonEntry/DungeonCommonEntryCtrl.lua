
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonCommonEntry
local PHASE_ID = PhaseId.DungeonEntry

local CustomGenDungeonSeriesTabInfoFunc = {
    [DungeonConst.DUNGEON_CATEGORY.CharTutorial] = '_GenDungeonCharTutorialTabInfos',
    [DungeonConst.DUNGEON_CATEGORY.HighDifficulty] = '_GenDungeonHighDifficultyTabInfos',
}

local CustomFindFirstSelectDungeonFunc = {
    [DungeonConst.DUNGEON_CATEGORY.CharTutorial] = '_FindFirstSelectCharTutorial',
}

DungeonCommonEntryCtrl = HL.Class('DungeonCommonEntryCtrl', uiCtrl.UICtrl)

DungeonCommonEntryCtrl.m_dungeonSeriesId = HL.Field(HL.String) << ""

DungeonCommonEntryCtrl.m_curTabIndex = HL.Field(HL.Number) << 1

DungeonCommonEntryCtrl.m_curSelectedDungeonId = HL.Field(HL.String) << ""

DungeonCommonEntryCtrl.m_dungeonTabCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonCommonEntryCtrl.m_curSelectedCell = HL.Field(HL.Any)

DungeonCommonEntryCtrl.m_tabDungeonIds = HL.Field(HL.Table)

DungeonCommonEntryCtrl.m_haveHardMode = HL.Field(HL.Boolean) << false

DungeonCommonEntryCtrl.m_fromDialog = HL.Field(HL.Boolean) << false

DungeonCommonEntryCtrl.m_arg = HL.Field(HL.Table)





DungeonCommonEntryCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DUNGEON_DIRECTLY_GET_REWARD] = 'OnDirectlyGetReward',
}


DungeonCommonEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self.m_curTabIndex = 1
    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.m_fromDialog = arg.fromDialog or false
    self.m_dungeonSeriesId = arg.dungeonSeriesId

    if not string.isEmpty(arg.dungeonId) then
        self.m_curSelectedDungeonId = arg.dungeonId
    end

    local needRecover = false
    if lume.find(DungeonConst.UI_RESTORE_DUNGEON_CATEGORY, Tables.dungeonSeriesTable[self.m_dungeonSeriesId].gameCategory) ~= nil then
        needRecover = true
    end

    if self.m_arg.enterDungeonCallback == nil then
        self.m_arg.enterDungeonCallback = function(enterDungeonId)
            if needRecover then
                LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId)
            end

            if self.m_fromDialog then
                Notify(MessageConst.DIALOG_CHANGE_NEXT_INDEX, { phaseId = PHASE_ID, nextIndex = 1 })
                PhaseManager:PopPhase(PHASE_ID)
            end
        end
    end

    self.m_dungeonTabCellCache = UIUtils.genCellCache(self.view.dungeonSelectionCell)

    self:_InitDungeonSeriesInfo()
    self:_InitDungeonTabs()
    self:_RefreshCommonInfo(true)

    self:_InitController()

    self.view.dungeonCommonInfo.view.directlyGetRewardBtn.onClick:AddListener(function()
        self:_OnClickDirectlyGetRewardBtn()
    end)

    CS.Beyond.Gameplay.Conditions.OnDungeonCommonEntryPanelOpen.Trigger(self.m_dungeonSeriesId, false)
end

DungeonCommonEntryCtrl.OnAnimationInFinished = HL.Override() << function(self)
    CS.Beyond.Gameplay.Conditions.OnDungeonCommonEntryPanelOpen.Trigger(self.m_dungeonSeriesId, true)
end

DungeonCommonEntryCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateNaviTarget()
end





DungeonCommonEntryCtrl.OnClose = HL.Override() << function(self)
    UIManager:Close(PanelId.CommonEnemyPopup)
end

DungeonCommonEntryCtrl._InitDungeonSeriesInfo = HL.Method() << function(self)

    local dungeonSeriesCfg = Tables.dungeonSeriesTable[self.m_dungeonSeriesId]
    self.view.titleTxt.text = dungeonSeriesCfg.name

    self:_InitRaid()

    self:_InitAchievement()
end

DungeonCommonEntryCtrl._InitRaid = HL.Method() << function(self)


    self.m_haveHardMode = Tables.dungeonRaidTable:TryGetValue(Tables.dungeonSeriesTable[self.m_dungeonSeriesId].includeDungeonIds[0])
    if self.m_haveHardMode then
        if string.isEmpty(self.m_curSelectedDungeonId) then
            local dungeonInfos = HighDifficultyUtils.GetSeriesInfo(self.m_dungeonSeriesId)


            for index, dungeonInfo in ipairs(dungeonInfos) do
                if not dungeonInfo.raidUnlocked then
                    self.m_curSelectedDungeonId = dungeonInfo.normalId
                    break
                end
            end


            if string.isEmpty(self.m_curSelectedDungeonId) then
                for index, dungeonInfo in ipairs(dungeonInfos) do

                    if not dungeonInfo.raidPassed or index == #dungeonInfos then
                        self.m_curSelectedDungeonId = dungeonInfo.raidId
                        break
                    end
                end
            end
        end


        local info = self.view.dungeonCommonInfo
        info.view.hardModeNode.gameObject:SetActive(true)
        self:_RefreshHardTog(self.m_curSelectedDungeonId)
        self.view.dungeonCommonInfo.view.hardTog.onValueChanged:AddListener(function(isOn)
            local isRaid, raidData = Tables.dungeonRaidTable:TryGetValue(self.m_curSelectedDungeonId)
            if isRaid then
                self.m_curSelectedDungeonId = raidData.RelatedLevel
            elseif isOn then
                self.m_curSelectedDungeonId = Tables.dungeonRaid2NormalTable[self.m_curSelectedDungeonId]
            else
                self.m_curSelectedDungeonId = Tables.dungeonNormal2RaidTable[self.m_curSelectedDungeonId]
            end

            info:RefreshDungeonCommonInfo(self.m_curSelectedDungeonId)
            info.view.hardTogStateController:SetState(isOn and "On" or "Off")
            if isOn then
                AudioAdapter.PostEvent("Au_UI_Toast_HighDifficultyHint")
            end
        end)
    end
end

DungeonCommonEntryCtrl._InitAchievement = HL.Method() << function(self)
    local achievementId
    local hasCfg, dungeonCfg = Tables.dungeonTable:TryGetValue(self.m_curSelectedDungeonId)
    local needShowAchievement = false
    if hasCfg then
        needShowAchievement = dungeonCfg.dungeonCategory ~= "dungeon_challenge"
    end
    if needShowAchievement then
        if Tables.HighDifficultyGameIdToSeriesIdTable:TryGetValue(self.m_curSelectedDungeonId) then

            local highDifficultySeriesId = Tables.HighDifficultyGameIdToSeriesIdTable[self.m_curSelectedDungeonId].seriesId
            achievementId = Tables.HighDifficultySeriesTable[highDifficultySeriesId].achieveId
        else

        end
    end

    if achievementId then
        self.view.dungeonMedalCell:InitCommonMedalNode(achievementId)
    end
    self.view.etchedSealNode.gameObject:SetActive(not string.isEmpty(achievementId))
end

DungeonCommonEntryCtrl._InitDungeonTabs = HL.Method() << function(self)

    self:_GenDungeonTabInfos()


    self:_FindFirstSelectDungeonTab()

    local tabCount = #self.m_tabDungeonIds
    local dungeonCfg = Tables.dungeonTable[self.m_tabDungeonIds[tabCount]]
    local charRelated = not string.isEmpty(dungeonCfg.relatedCharId)
    local showSelectionNode = charRelated or tabCount > 1
    if showSelectionNode then
        self.m_dungeonTabCellCache:Refresh(tabCount, function(cell, luaIndex)
            local dungeonId = self.m_tabDungeonIds[luaIndex]
            self:_UpdateTabCell(cell, dungeonId, luaIndex)
        end)
        self.m_curSelectedCell:SetSelected(true)
        LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.dungeonSelectionNode.transform)
        self.view.dungeonSelectionNode:AutoScrollToRectTransform(self.m_curSelectedCell.gameObject.transform, true)
    end
    self.view.dungeonSelectionNode.gameObject:SetActiveIfNecessary(showSelectionNode)
end

DungeonCommonEntryCtrl._GenDungeonTabInfos = HL.Method() << function(self)
    self.m_tabDungeonIds = {}
    local dungeonSeriesCfg = Tables.dungeonSeriesTable[self.m_dungeonSeriesId]
    local funcName = CustomGenDungeonSeriesTabInfoFunc[dungeonSeriesCfg.gameCategory]
    if funcName then
        self[funcName](self)
    else
        for _, dungeonId in pairs(dungeonSeriesCfg.includeDungeonIds) do
            table.insert(self.m_tabDungeonIds, dungeonId)
        end
    end
end

DungeonCommonEntryCtrl._GenDungeonCharTutorialTabInfos = HL.Method() << function(self)
    local dungeonSeriesCfg = Tables.dungeonSeriesTable[self.m_dungeonSeriesId]
    for _, dungeonId in pairs(dungeonSeriesCfg.includeDungeonIds) do


        local isUnlock = DungeonUtils.isDungeonUnlock(dungeonId)
        if isUnlock then
            table.insert(self.m_tabDungeonIds, dungeonId)
        end
    end

    table.sort(self.m_tabDungeonIds, function(a, b)
        local aPass = DungeonUtils.isDungeonPassed(a)
        local bPass = DungeonUtils.isDungeonPassed(b)
        if aPass and bPass or not aPass and not bPass then
            return Tables.dungeonTable[a].sortId < Tables.dungeonTable[b].sortId
        end

        return not aPass and bPass
    end)
end

DungeonCommonEntryCtrl._GenDungeonHighDifficultyTabInfos = HL.Method() << function(self)
    if not self.m_haveHardMode then
        return
    end


    local seriesInfo = HighDifficultyUtils.GetSeriesInfo(self.m_dungeonSeriesId)
    for _, dungeonInfo in ipairs(seriesInfo) do
        table.insert(self.m_tabDungeonIds, dungeonInfo.normalId)
    end
end

DungeonCommonEntryCtrl._FindFirstSelectDungeonTab = HL.Method() << function(self)

    if not string.isEmpty(self.m_curSelectedDungeonId) then
        return
    end

    local dungeonSeriesCfg = Tables.dungeonSeriesTable[self.m_dungeonSeriesId]


    local funcName = CustomFindFirstSelectDungeonFunc[dungeonSeriesCfg.gameCategory]
    if funcName then
        self[funcName](self)
    end

    if string.isEmpty(self.m_curSelectedDungeonId) then


        for luaIndex = #self.m_tabDungeonIds, 1, -1 do
            local dungeonId = self.m_tabDungeonIds[luaIndex]
            local isUnlock = DungeonUtils.isDungeonUnlock(dungeonId)
            if isUnlock then
                self.m_curSelectedDungeonId = dungeonId
                break
            end
        end
    end


    if string.isEmpty(self.m_curSelectedDungeonId) then
        self.m_curSelectedDungeonId = self.m_tabDungeonIds[1]
    end
end

DungeonCommonEntryCtrl._FindFirstSelectCharTutorial = HL.Method() << function(self)

    if string.isEmpty(self.m_curSelectedDungeonId) then
        self.m_curSelectedDungeonId = self.m_tabDungeonIds[1]
    end
end


DungeonCommonEntryCtrl._UpdateTabCell = HL.Method(HL.Any, HL.String, HL.Number) << function(self, cell, dungeonId,
                                                                                            luaIndex)
    cell:InitDungeonCommonSelectionCell(dungeonId, function()
        self.m_curTabIndex = luaIndex
        if self.m_haveHardMode then
            local normalId = dungeonId
            local raidId = Tables.dungeonRaidTable[normalId].RelatedLevel
            local isRaid = DungeonUtils.isDungeonUnlock(raidId)
            self:_OnDungeonTabClick(cell, isRaid and raidId or normalId)
        else
            self:_OnDungeonTabClick(cell, dungeonId)
        end
    end)
    cell.gameObject.name = dungeonId
    if self.m_curSelectedDungeonId == dungeonId then
        self.m_curSelectedCell = cell
    elseif self.m_haveHardMode and dungeonId == Tables.dungeonRaidTable[self.m_curSelectedDungeonId].RelatedLevel then
        self.m_curSelectedCell = cell
    end
end

DungeonCommonEntryCtrl._GenCustomArgs = HL.Method().Return(HL.Table) << function(self)
    return self.m_arg
end

DungeonCommonEntryCtrl._OnBtnCloseClick = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

DungeonCommonEntryCtrl._OnDungeonTabClick = HL.Method(HL.Any, HL.String)
        << function(self, cell, dungeonId)
    if self.m_curSelectedDungeonId == dungeonId then
        return
    end
    if Tables.dungeonRaidTable:ContainsKey(self.m_curSelectedDungeonId) and
        Tables.dungeonRaidTable[self.m_curSelectedDungeonId].RelatedLevel == dungeonId then
        return
    end

    if self.m_haveHardMode then
        self:_RefreshHardTog(dungeonId)
    end

    local preCell = self.m_curSelectedCell
    self.m_curSelectedCell = cell
    self.m_curSelectedDungeonId = dungeonId

    preCell:SetSelected(false)
    cell:SetSelected(true)

    self:_RefreshCommonInfo(false)
end

DungeonCommonEntryCtrl._RefreshHardTog = HL.Method(HL.String) << function(self, dungeonId)
    local info = self.view.dungeonCommonInfo
    local raidInfo = Tables.dungeonRaidTable[dungeonId]
    local relatedDungeonId = raidInfo.RelatedLevel



    local raidDungeonId = raidInfo.isRaid and dungeonId or relatedDungeonId
    local isRaidUnlocked = GameInstance.dungeonManager:IsDungeonUnlocked(raidDungeonId)
    local isHardTogOn = raidInfo.isRaid and isRaidUnlocked

    info.view.hardTog.isOn = isHardTogOn
    if isRaidUnlocked then
        info.view.hardModeNode:SetState("Enabled")
        if isHardTogOn then
            AudioAdapter.PostEvent("Au_UI_Toast_HighDifficultyHint")
        end
        self:_StartCoroutine(function()
            info.view.hardTogStateController:SetState(isHardTogOn and "On" or "Off")
        end)
    else
        info.view.hardModeNode:SetState("Disabled")
        info.view.fakeHardTogBtn.onClick:RemoveAllListeners()
        info.view.fakeHardTogBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST,Language.LUA_DUNGEON_HARD_MODE_CANT_TOG)
        end)
        self:_StartCoroutine(function()
            info.view.hardTogStateController:SetState("Off")
        end)
    end

end

DungeonCommonEntryCtrl._RefreshCommonInfo = HL.Method(HL.Boolean) << function(self, isInit)
    if isInit then
        self.view.dungeonCommonInfo:InitDungeonCommonInfo(self:_GenCustomArgs())
    end
    self.view.dungeonCommonInfo:RefreshDungeonCommonInfo(self.m_curSelectedDungeonId)
    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(self.m_curSelectedDungeonId)
    if succ then
        local path = dungeonCfg.dungeonPicPath
        self.view.dungeonBG:LoadSprite(UIConst.UI_SPRITE_DUNGEON, path)
        self.view.maskImg:LoadSprite(UIConst.UI_SPRITE_DUNGEON, path.."_bg")
    end

    local dungeonSeriesCfg = Tables.dungeonSeriesTable[self.m_dungeonSeriesId]
    local canShowDirectlyGetReward = dungeonSeriesCfg.gameCategory == DungeonConst.DUNGEON_CATEGORY.CharTutorial
    if canShowDirectlyGetReward then
        local canDirectlyGetReward = dungeonCfg.canDirectlyGetReward
        local dungeonMgr = GameInstance.dungeonManager
        local manuallyPassed = dungeonMgr:IsDungeonManuallyPassed(self.m_curSelectedDungeonId)
        local hasFirstPassReward = not string.isEmpty(dungeonCfg.firstPassRewardId)
        local hasExtraPassReward = not string.isEmpty(dungeonCfg.extraRewardId)
        local firstRewardGained = dungeonMgr:IsDungeonFirstPassRewardGained(self.m_curSelectedDungeonId)
        local extraRewardGained = dungeonMgr:IsDungeonExtraRewardGained(self.m_curSelectedDungeonId)
        local isUnlock = DungeonUtils.isDungeonUnlock(self.m_curSelectedDungeonId)

        local state = "HideNode"
        if isUnlock and not manuallyPassed then
            if not canDirectlyGetReward then
                state = "NeedManual"
            elseif hasFirstPassReward and not firstRewardGained or
                hasExtraPassReward and not extraRewardGained then
                state = "CanDirectlyGetReward"
            end
        end
        self.view.dungeonCommonInfo.view.directlyGetRewardNode:SetState(state)
    else
        self.view.dungeonCommonInfo.view.directlyGetRewardNode:SetState("HideNode")
    end
end

DungeonCommonEntryCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self:_UpdateNaviTarget()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

DungeonCommonEntryCtrl._UpdateNaviTarget = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    if self.m_dungeonTabCellCache:GetCount() > 1 then
        self:SetNaviTarget(self.m_curSelectedCell.view.clickBtn)
    end
end

DungeonCommonEntryCtrl._OnClickDirectlyGetRewardBtn = HL.Method() << function(self)

    if not string.isEmpty(GameWorld.worldInfo.curSubGameId) then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_INVALID_SYSTEM_COMMON_DESCRIPTION)
        return
    end

    local dungeonCfg = Tables.dungeonTable[self.m_curSelectedDungeonId]
    local hintText = string.format(Language["ui_fac_tech_tree_blackbox_complete_confirm"], dungeonCfg.dungeonName)

    self:Notify(MessageConst.SHOW_POP_UP, {
        content = hintText,
        onConfirm = function()
            GameInstance.dungeonManager:SendReqDirectlyGetReward(self.m_curSelectedDungeonId)
        end,
    })
end

DungeonCommonEntryCtrl.OnDirectlyGetReward = HL.Method(HL.Any) << function(self, arg)
    self.m_dungeonTabCellCache:Refresh(#self.m_tabDungeonIds, function(cell, luaIndex)
        local dungeonId = self.m_tabDungeonIds[luaIndex]
        self:_UpdateTabCell(cell, dungeonId, luaIndex)
    end)
    self.m_curSelectedCell:SetSelected(true)
    self:_RefreshCommonInfo(false)
    self:SetNaviTarget(self.m_curSelectedCell.view.clickBtn)

    local RewardSourceType = CS.Beyond.GEnums.RewardSourceType
    local firstPassRewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(RewardSourceType.DungeonFirstPass)
    local items = {}
    if firstPassRewardPack and firstPassRewardPack.rewardSourceType == RewardSourceType.DungeonFirstPass then
        for _, itemBundle in pairs(firstPassRewardPack.itemBundleList) do
            local _, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemCfg then
                table.insert(items, { id = itemBundle.id,
                                      count = itemBundle.count,
                                      sortId1 = itemCfg.sortId1,
                                      sortId2 = itemCfg.sortId2 })
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
    })
end

DungeonCommonEntryCtrl.GetCurSelectDungeonId = HL.Method().Return(HL.String) << function(self)
    return self.m_curSelectedDungeonId
end

DungeonCommonEntryCtrl.GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    return self.view.dungeonCommonInfo:GetRecoverPopupStateArg()
end

DungeonCommonEntryCtrl.TryRecoverPopupState = HL.Method(HL.Any) << function(self, popupState)
    self.view.dungeonCommonInfo:TryRecoverPopupState(popupState)
end

HL.Commit(DungeonCommonEntryCtrl)



