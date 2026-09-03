
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonCommonEntry
local PHASE_ID = PhaseId.DungeonEntry

DungeonTrainEntryCtrl = HL.Class('DungeonTrainEntryCtrl', uiCtrl.UICtrl)

DungeonTrainEntryCtrl.m_dungeonSeriesId = HL.Field(HL.String) << ""

DungeonTrainEntryCtrl.m_curTabIndex = HL.Field(HL.Number) << 1

DungeonTrainEntryCtrl.m_curSelectedDungeonId = HL.Field(HL.String) << ""

DungeonTrainEntryCtrl.m_dungeonTabGroupCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonTrainEntryCtrl.m_tabGroups = HL.Field(HL.Table)

DungeonTrainEntryCtrl.m_curSelectedCell = HL.Field(HL.Any)

DungeonTrainEntryCtrl.m_tabDungeonIds = HL.Field(HL.Table)

DungeonTrainEntryCtrl.m_fromDialog = HL.Field(HL.Boolean) << false

DungeonTrainEntryCtrl.m_arg = HL.Field(HL.Table)





DungeonTrainEntryCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DUNGEON_DIRECTLY_GET_REWARD] = 'OnDirectlyGetReward',
}


DungeonTrainEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self.m_curTabIndex = 1
    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)
    self.view.dungeonCommonInfo.view.directlyGetRewardBtn.onClick:AddListener(function()
        self:_OnClickDirectlyGetRewardBtn()
    end)

    self.m_fromDialog = arg.fromDialog or false
    self.m_dungeonSeriesId = arg.dungeonSeriesId

    if not string.isEmpty(arg.dungeonId) then
        self.m_curSelectedDungeonId = arg.dungeonId
    end

    self.m_dungeonTabGroupCellCache = UIUtils.genCellCache(self.view.dungeonSelectionGroupCell)

    self:_InitDungeonSeriesInfo()
    self:_InitDungeonTabs()
    self:_RefreshCommonInfo(true)

    self:_InitController()

    CS.Beyond.Gameplay.Conditions.OnDungeonCommonEntryPanelOpen.Trigger(self.m_dungeonSeriesId, false)
end

DungeonTrainEntryCtrl.OnAnimationInFinished = HL.Override() << function(self)
    CS.Beyond.Gameplay.Conditions.OnDungeonCommonEntryPanelOpen.Trigger(self.m_dungeonSeriesId, true)

    if not DeviceInfo.usingController then
        return
    end

    self:SetNaviTarget(self.m_curSelectedCell.view.clickBtn)
end

DungeonTrainEntryCtrl._InitDungeonSeriesInfo = HL.Method() << function(self)

    local dungeonSeriesCfg = Tables.dungeonSeriesTable[self.m_dungeonSeriesId]
    self.view.titleTxt.text = dungeonSeriesCfg.name
end

DungeonTrainEntryCtrl._InitDungeonTabs = HL.Method() << function(self)

    self:_GenDungeonTabInfos()

    local haveGroup, tabGroups = DungeonUtils.groupDungeonsByCondition(self.m_tabDungeonIds)
    self.m_tabGroups = tabGroups


    self:_FindFirstSelectTrain()

    self.m_dungeonTabGroupCellCache:Refresh(#self.m_tabGroups, function(cell, luaIndex)
        self:_OnRefreshTabGroupCell(cell, luaIndex, true)
    end)
end

DungeonTrainEntryCtrl._OnRefreshTabGroupCell = HL.Method(HL.Any, HL.Number, HL.Boolean) << function(self, cell, luaIndex, isInit)
    local tabGroup = self.m_tabGroups[luaIndex]
    cell:InitDungeonCommonSelectionGroupCell(tabGroup, function(selectCell, selectDungeonId)
        self:_OnDungeonTabClick(selectCell, selectDungeonId)
    end)
    cell.gameObject.name = "DungeonGroup-"..luaIndex
    local found = cell:TryGetSubCell(self.m_curSelectedDungeonId)
    if found ~= nil then
        self.m_curSelectedCell = found
        self.m_curSelectedCell:SetSelected(true)
        cell:SetToggle(true)
    else
        if isInit and not PhaseManager.isRecovering then
            local isUsingController = (DeviceInfo.inputType == DeviceInfo.InputType.Controller)
            cell:SetToggle(false or isUsingController)
        end
    end
end

DungeonTrainEntryCtrl._GenDungeonTabInfos = HL.Method() << function(self)
    self.m_tabDungeonIds = {}
    local dungeonSeriesCfg = Tables.dungeonSeriesTable[self.m_dungeonSeriesId]
    for _, dungeonId in pairs(dungeonSeriesCfg.includeDungeonIds) do
        table.insert(self.m_tabDungeonIds, dungeonId)
    end
end

DungeonTrainEntryCtrl._FindFirstSelectTrain = HL.Method() << function(self)
    if not string.isEmpty(self.m_curSelectedDungeonId) then
        return
    end

    for _, tab in ipairs(self.m_tabGroups) do
        for _, dungeonId in ipairs(tab) do
            local isUnlock = DungeonUtils.isDungeonUnlock(dungeonId)
            local isComplete = DungeonUtils.isDungeonPassed(dungeonId)
            if isUnlock and not isComplete then
                self.m_curSelectedDungeonId = dungeonId
                return
            end
        end
    end

    for i = #self.m_tabGroups, 1, -1 do
        local tab = self.m_tabGroups[i]
        for j = #tab, 1, -1 do
            local dungeonId = tab[j]
            local isComplete = DungeonUtils.isDungeonPassed(dungeonId)
            if isComplete then
                self.m_curSelectedDungeonId = dungeonId
                return
            end
        end
    end

    if #self.m_tabGroups >= 1 and #(self.m_tabGroups[1]) >= 1 then
        self.m_curSelectedDungeonId = self.m_tabGroups[1][1]
    end
end


DungeonTrainEntryCtrl._UpdateTabCell = HL.Method(HL.Any, HL.String, HL.Number) << function(self, cell, dungeonId,
                                                                                            luaIndex)
    cell:InitDungeonCommonSelectionCell(dungeonId, function()
        self.m_curTabIndex = luaIndex
        self:_OnDungeonTabClick(cell, dungeonId)
    end)
    cell.gameObject.name = dungeonId
    if self.m_curSelectedDungeonId == dungeonId then
        self.m_curSelectedCell = cell
    end
end

DungeonTrainEntryCtrl._GenCustomArgs = HL.Method().Return(HL.Table) << function(self)
    return self.m_arg
end

DungeonTrainEntryCtrl._OnBtnCloseClick = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

DungeonTrainEntryCtrl._OnDungeonTabClick = HL.Method(HL.Any, HL.String)
        << function(self, cell, dungeonId)
    if self.m_curSelectedDungeonId == dungeonId then
        return
    end

    local preCell = self.m_curSelectedCell
    self.m_curSelectedCell = cell
    self.m_curSelectedDungeonId = dungeonId

    preCell:SetSelected(false)
    cell:SetSelected(true)

    self:_RefreshCommonInfo(false)
end

DungeonTrainEntryCtrl._RefreshCommonInfo = HL.Method(HL.Boolean) << function(self, isInit)
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
end

DungeonTrainEntryCtrl._OnClickDirectlyGetRewardBtn = HL.Method() << function(self)

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

DungeonTrainEntryCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

DungeonTrainEntryCtrl.OnDirectlyGetReward = HL.Method(HL.Any) << function(self, arg)
    self.m_dungeonTabGroupCellCache:Refresh(#self.m_tabGroups, function(cell, luaIndex)
        self:_OnRefreshTabGroupCell(cell, luaIndex, false)
    end)
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

DungeonTrainEntryCtrl.GetCurSelectDungeonId = HL.Method().Return(HL.String) << function(self)
    return self.m_curSelectedDungeonId
end

DungeonTrainEntryCtrl.GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local popupState = self.view.dungeonCommonInfo:GetRecoverPopupStateArg()
    if popupState ~= nil then
        return popupState
    end

    local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if not isOpen or not commonPopUpCtrl:IsShow() then
        return
    end

    local recoverArg = commonPopUpCtrl:GetCurPhaseStateArg()
    if recoverArg == nil or string.isEmpty(recoverArg.content) then
        return
    end

    local dungeonCfg = Tables.dungeonTable[self.m_curSelectedDungeonId]
    if dungeonCfg == nil then
        return
    end



    local expectContent = string.format(Language["ui_fac_tech_tree_blackbox_complete_confirm"], dungeonCfg.dungeonName)
    if recoverArg.content == expectContent then
        return {
            popupType = "DirectlyGetRewardConfirm",
        }
    end
end

DungeonTrainEntryCtrl.TryRecoverPopupState = HL.Method(HL.Any) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return
    end

    if popupState.popupType == "DirectlyGetRewardConfirm" then
        local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
        if isOpen and commonPopUpCtrl:IsShow() then
            return
        end


        self:_OnClickDirectlyGetRewardBtn()
        return
    end

    self.view.dungeonCommonInfo:TryRecoverPopupState(popupState)
end

HL.Commit(DungeonTrainEntryCtrl)
