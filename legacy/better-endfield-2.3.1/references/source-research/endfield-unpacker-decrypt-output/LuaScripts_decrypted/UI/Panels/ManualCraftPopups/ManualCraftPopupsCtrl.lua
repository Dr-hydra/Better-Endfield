
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ManualCraftPopups
local PHASE_ID = PhaseId.ManualCraftPopups
ManualCraftPopupsCtrl = HL.Class('ManualCraftPopupsCtrl', uiCtrl.UICtrl)

ManualCraftPopupsCtrl.m_itemCellList = HL.Field(HL.Forward("UIListCache"))

ManualCraftPopupsCtrl.m_unlockItemList = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_unlockItemRewardMap = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_unlockItemIdCellMap = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_facManualCraftSystem = HL.Field(HL.Any)

ManualCraftPopupsCtrl.m_inventorySystem = HL.Field(HL.Any)

ManualCraftPopupsCtrl.m_selectIndex = HL.Field(HL.Number) << 0

ManualCraftPopupsCtrl.m_previewIndex = HL.Field(HL.Number) << 0

ManualCraftPopupsCtrl.m_controllerItemId = HL.Field(HL.Any) << nil

ManualCraftPopupsCtrl.m_showTipsBindingId = HL.Field(HL.Number) << -1

ManualCraftPopupsCtrl.m_rewardCellList = HL.Field(HL.Forward("UIListCache"))

ManualCraftPopupsCtrl.m_previewCellList = HL.Field(HL.Forward("UIListCache"))

ManualCraftPopupsCtrl.m_getDefaultCell = HL.Field(HL.Function)

ManualCraftPopupsCtrl.m_getSelectCell = HL.Field(HL.Function)

ManualCraftPopupsCtrl.m_needRefreshReward = HL.Field(HL.Boolean) << false

ManualCraftPopupsCtrl.m_sortMode = HL.Field(HL.Number) << 1

ManualCraftPopupsCtrl.m_sortIncremental = HL.Field(HL.Boolean) << false

ManualCraftPopupsCtrl.m_activeScroll = HL.Field(HL.Boolean) << false

ManualCraftPopupsCtrl.m_jumpRewardFlag = HL.Field(HL.Boolean) << false

ManualCraftPopupsCtrl.m_initControllerSelected = HL.Field(HL.Boolean) << false
ManualCraftPopupsCtrl.m_getCurPhaseStateFlag = HL.Field(HL.Boolean) << false

ManualCraftPopupsCtrl.m_bindSubCache = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_filterSetting = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_realData = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_jumpItem = HL.Field(HL.String) << ""

ManualCraftPopupsCtrl.m_jumpIndex = HL.Field(HL.Number) << 0

ManualCraftPopupsCtrl.m_jumpUnLock = HL.Field(HL.Boolean) << false

ManualCraftPopupsCtrl.m_listRedDotHandle = HL.Field(HL.Number) << -1

ManualCraftPopupsCtrl.m_redDotUpCache = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_redDotDownCache = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_luaIndex2ItemCell = HL.Field(HL.Table)

ManualCraftPopupsCtrl.m_mainCellInAnim = HL.Field(HL.Boolean) << false

ManualCraftPopupsCtrl.m_tickValidTime = HL.Field(HL.Number) << 0

ManualCraftPopupsCtrl.m_scrollToIndexFlag = HL.Field(HL.Boolean) << false

ManualCraftPopupsCtrl.m_activeScrollTimer = HL.Field(HL.Number) << -1

ManualCraftPopupsCtrl.m_inMainPanel = HL.Field(HL.Boolean) << true







ManualCraftPopupsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_MANUAL_CRAFT_LEVEL_UP] = 'OnGetNewItemLevelUp',
    [MessageConst.ON_MANUAL_CRAFT_GET_ITEM_REWARD] = 'OnGetReward',
    [MessageConst.ON_GET_MANUAL_REWARD] = '_OnGetReward'
}


local LUA_FIRST_INDEX = 1
local GATHER_MAP_VALID_COUNT = 1
local ShowRewardsType = {
    RECEIVED = "RECEIVED",
    AVAILABLE = "AVAILABLE",
    NOT_AVAILABLE = "NOT_AVAILABLE",
}

local ACTIVE_TIMER_VALUE = 0.1


ManualCraftPopupsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    if arg ~= nil and arg.jumpItem ~= nil then
        self.m_jumpItem = arg.jumpItem
    end

    self.view.selectPanel.gameObject:SetActive(false)
    self.m_facManualCraftSystem = GameInstance.player.facManualCraft
    self.m_inventorySystem = GameInstance.player.inventory

    self.m_bindSubCache = {}
    self.m_selectIndex = 0
    self.m_unlockItemList = nil
    self.m_unlockItemRewardMap = {}
    self.m_unlockItemIdCellMap = {}
    self.view.leftArrow.onClick:AddListener(function()
        if self.m_inMainPanel then
            return
        end
        if self.m_selectIndex > 1 then
            self:_UpdateActiveScroll()
            self.m_selectIndex = self.m_selectIndex - 1
            AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
            self:_UpdateSelectView(false)
            self.view.selectPanel:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):Play("manualcraftpopups_select_switch")
        end
    end)

    self.view.rightArrow.onClick:AddListener(function()
        if self.m_inMainPanel then
            return
        end
        if self.m_selectIndex < #self.m_unlockItemList then
            self:_UpdateActiveScroll()
            self.m_selectIndex = self.m_selectIndex + 1
            AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
            self:_UpdateSelectView(false)
            self.view.selectPanel:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):Play("manualcraftpopups_select_switch")
        end
    end)

    self.view.btnBack.onClick:AddListener(function()
        if self.m_jumpUnLock then
            PhaseManager:PopPhase(PHASE_ID)
        else
            if self.view.selectPanel.gameObject.activeSelf then
                self:_UpdateDefaultView(self.m_needRefreshReward)
            else
                PhaseManager:PopPhase(PHASE_ID)
            end
        end
    end)

    self.view.returnBtn.onClick:AddListener(function()
        if DeviceInfo.usingController then
            self.view.contentCardListNavi:ManuallyStopFocus()
        end
        self:_UpdateDefaultView(self.m_needRefreshReward)
    end)

    self.view.btnCommon.onClick:AddListener(function()
        local nowLockData = self.m_unlockItemList[self.m_selectIndex]
        local nowCount = self.m_facManualCraftSystem:GetItemAccumulateCount(nowLockData.itemId)
        local list = CS.Beyond.Lua.UtilsForLua.GetStringList()
        for i = 0, nowLockData.rewardList.Count - 1 do
            local rewardId = nowLockData.rewardList[i]
            local condition = nowLockData.unlockCondition[i]
            local unlockId = nowLockData.unlockIdList[i]
            local haveGet = self.m_facManualCraftSystem:CheckHaveGetReward(unlockId)
            if nowCount >= condition and not haveGet then
                list:Add(unlockId)
            end
        end
        self.m_facManualCraftSystem:ReqManuallyUnlock(Utils.getCurrentScope(), list)
    end)

    self.m_getDefaultCell = UIUtils.genCachedCellFunction(self.view.itemList)
    self.m_getSelectCell = UIUtils.genCachedCellFunction(self.view.previewScrollList)

    self.view.itemList.onUpdateCell:AddListener(function(gameObject, index)
        self:_UpdateDefaultCell((gameObject), index)
    end)

    self.view.previewScrollList.getCurSelectedIndex = function()
        return CSIndex(self.m_previewIndex)
    end

    self.view.previewScrollList.onCellSelectedChanged:AddListener(function(obj, csIndex, isSelected)
        if obj == nil then
            return
        end
        self.m_previewIndex = LuaIndex(csIndex)
    end)


    self.view.previewScrollList.onUpdateCell:AddListener(function(gameObject, index)
        self:_UpdateSelectCell((gameObject), index)
    end)

    self.view.previewScrollList:SetPaddingRight(self.view.previewScrollList.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)).rect.width / 2 - 200)
    self.view.previewScrollList:SetPaddingLeft(self.view.previewScrollList.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)).rect.width / 2 - 200)


    self.view.previewScrollList.onSelectedCell:AddListener(function(object, index)
        if object == nil then
            return
        end
        self:_UpdateSelectCell((object), index)
    end)

    self.m_unlockItemList = {}
    for k, v in cs_pairs(self.m_facManualCraftSystem.manufactureUnlockDataMap) do
        table.insert(self.m_unlockItemList, v)
    end
    self.m_sortIncremental = true


    self.m_filterSetting = {}
    local list = self.m_facManualCraftSystem:GetAllDomainData()
    for i = 0 , list.Count - 1 do
        local domainData = list[i]
        table.insert(self.m_filterSetting, {id = domainData.domainId, domainName = domainData.domainName, defaultIsOn = true,name = domainData.domainName,isOn = true})
    end


    local recoverSelectedItemId = arg and arg.selectedItemId
    local needRecoverSelectPanel = arg and arg.recoverSelectPanel == true
    self:_RecoverState(arg)

    self:_SortAndFilter()

    for jumpIndex, v in pairs(self.m_unlockItemList) do
        if self.m_jumpItem == v.itemId then
            self.m_jumpUnLock = true
            self.m_jumpIndex = jumpIndex
        end
    end

    local validNum = 0
    for i = 1,#self.m_filterSetting do
        local filterNum = self:_FilterBtnGetResCount({self.m_filterSetting[i]})
        if filterNum > 0 then
            validNum = validNum + 1
        end
    end

    if validNum > 1 then
        self.view.sortNodeUp:InitSortNode(UIConst.ManualCraftPopupsSortOptions, function(optData, isIncremental)
            self.m_sortMode = optData.sortMode
            self.m_sortIncremental = isIncremental
            self:_UpdateDefaultView(true)
        end, self.m_sortMode - 1, self.m_sortIncremental, true, self.view.filterBtn)
    else
        self.view.sortNodeUp:InitSortNode(UIConst.ManualCraftPopupsSortOptions, function(optData, isIncremental)
            self.m_sortMode = optData.sortMode
            self.m_sortIncremental = isIncremental
            self:_UpdateDefaultView(true)
        end, self.m_sortMode - 1, self.m_sortIncremental, true)
    end


    self.view.filterBtn:InitFilterBtn({
        tagGroups = {{tags = self.m_filterSetting}} ,
        selectedTags = self:_GetSelectedFilterTags(),
        onConfirm = function(tags)
            self:_FilterBtnConfirm(tags)
            self.m_sortMode = self.view.sortNodeUp:GetCurSortData().sortMode
            self.m_sortIncremental = self.view.sortNodeUp.isIncremental
            self:_UpdateDefaultView(true)
        end,
        getResultCount = function(tags)
            return self:_FilterBtnGetResCount(tags)
        end,
        sortNodeWidget = self.view.sortNodeUp,
    })


    self.view.previewScrollList.onCenterIndexChanged:AddListener(function(oldIndex, newIndex)
        if self.m_inMainPanel then
            return
        end
        if self.m_selectIndex ~= newIndex + 1 and not self.m_activeScroll then
            self.m_selectIndex = newIndex + 1
            AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
            self:_UpdateSelectView(false)
            return
        end
    end)

    self.view.itemList.onGraduallyShowFinish:AddListener(function()
        self.m_mainCellInAnim = false
        self.m_needRefreshReward = false
    end)

    self.view.previewScrollList.onScrollEnd:AddListener(function()
        if self.m_scrollToIndexFlag then
            self.m_scrollToIndexFlag = false
            self.m_activeScroll = false
            self.view.previewScrollList:ScrollToIndex(self.m_selectIndex - 1)
        end
    end)

    local recoveredSelectPanel = needRecoverSelectPanel and not string.isEmpty(recoverSelectedItemId) and self:_RecoverSelectPanelByItemId(recoverSelectedItemId)
    if not recoveredSelectPanel and self.m_jumpUnLock then
        self.m_selectIndex = self.m_jumpIndex
        self.m_previewIndex = self.m_jumpIndex
        self.m_controllerItemId = self.m_jumpItem
        self:_UpdateActiveScroll()
        self:_UpdateSelectView(false)
        self.view.selectPanel:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):Play("manualcraftpopups_select_in")
    elseif not recoveredSelectPanel then
        self:_UpdateDefaultView(true)
    end

    self.m_listRedDotHandle = LuaUpdate:Add("TailTick", function(deltaTime)
        self:_TickUpdate(deltaTime)
    end)
    self.m_tickValidTime = 0
end

ManualCraftPopupsCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    self.m_initControllerSelected = false
    self.m_getCurPhaseStateFlag = true
    local filterIds = {}
    for _, filterInfo in ipairs(self.m_filterSetting) do
        if filterInfo.isOn then
            table.insert(filterIds, filterInfo.id)
        end
    end
    local selectedItemId
    if not self.m_inMainPanel and self.m_selectIndex and self.m_unlockItemList and self.m_unlockItemList[self.m_selectIndex] then
        selectedItemId = self.m_unlockItemList[self.m_selectIndex].itemId
    end
    return {
        sortMode = self.m_sortMode,
        sortIncremental = self.m_sortIncremental,
        filterIds = filterIds,
        selectedItemId = selectedItemId,
        recoverSelectPanel = not self.m_inMainPanel and not string.isEmpty(selectedItemId),
    }
end

ManualCraftPopupsCtrl._RecoverState = HL.Method(HL.Opt(HL.Table)) << function(self, recoverState)
    if not recoverState then
        return
    end
    if recoverState.sortMode then
        self.m_sortMode = recoverState.sortMode
    end
    if recoverState.sortIncremental ~= nil then
        self.m_sortIncremental = recoverState.sortIncremental
    end
    if recoverState.filterIds then
        local selectedFilterIdMap = {}
        for _, filterId in ipairs(recoverState.filterIds) do
            selectedFilterIdMap[filterId] = true
        end
        for _, filterInfo in ipairs(self.m_filterSetting) do
            filterInfo.isOn = selectedFilterIdMap[filterInfo.id] == true
        end
    end
end

ManualCraftPopupsCtrl._GetSelectedFilterTags = HL.Method().Return(HL.Table) << function(self)
    local selectedFilter = {}
    for _, filterInfo in ipairs(self.m_filterSetting) do
        if filterInfo.isOn then
            table.insert(selectedFilter, filterInfo)
        end
    end
    return selectedFilter
end

ManualCraftPopupsCtrl._RecoverSelectPanelByItemId = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    for index, item in ipairs(self.m_unlockItemList) do
        if item.itemId == itemId then
            self.m_selectIndex = index
            self.m_previewIndex = index
            self.m_controllerItemId = item.itemId
            self:_UpdateActiveScroll()
            self:_UpdateSelectView(false)
            self.view.selectPanel:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):Play("manualcraftpopups_select_in")
            return true
        end
    end
    return false
end

ManualCraftPopupsCtrl._UpdateActiveScroll = HL.Method() << function(self)
    self.m_activeScroll = true
    self.m_scrollToIndexFlag = true

    self.m_activeScrollTimer = self:_ClearTimer(self.m_activeScrollTimer)
    self.m_activeScrollTimer = self:_StartTimer(ACTIVE_TIMER_VALUE, function()
        self.m_activeScroll = false
    end)

end


ManualCraftPopupsCtrl._TickUpdate = HL.Method(HL.Number) << function(self, deltaTime)
    if self.m_tickValidTime > 0 then
        self.m_tickValidTime = self.m_tickValidTime - deltaTime
        self:_SetListUpRedDotVisible(false)
        self:_SetListDownRedDotVisible(false)
        return
    end

    self:_UpdateListUpRedDot()
    self:_UpdateListDownRedDot()
end

ManualCraftPopupsCtrl._UpdateListUpRedDot = HL.Method() << function(self)
    if not self.view.itemList.gameObject.activeSelf then
        self:_SetListUpRedDotVisible(false)
        return
    end

    local res = self.view.itemList:GetShowRange()
    local showRangeX = res.x
    local showRangeY = res.y

    local checkShowLuaIndex = math.min(showRangeX + 4, showRangeY + 1)
    for luaIndex = checkShowLuaIndex, showRangeX + 1, -1 do
        if self.m_unlockItemList[luaIndex] ~= nil and self.m_redDotUpCache[luaIndex] ~= nil and self.m_redDotUpCache[luaIndex] > 0 then
            local cell = self.m_luaIndex2ItemCell[luaIndex]
            if cell then
                local pos2 = cell.redDot.gameObject.transform.position
                local screen2 = self.uiCamera:WorldToScreenPoint(pos2)
                local uiPos, isInside = UIUtils.screenPointToUI(Vector2(screen2.x,screen2.y), self.uiCamera, self.view.itemList.gameObject.transform)
                if not self.view.itemList.gameObject.transform.rect:Contains(uiPos) then
                    self:_SetListUpRedDotVisible(true)
                    return
                end
            end
        end
    end

    local checkLuaIndex = showRangeX
    if #self.m_unlockItemList >= checkLuaIndex and checkLuaIndex >= 1 then
        if self.m_redDotUpCache[checkLuaIndex] ~= nil and self.m_redDotUpCache[checkLuaIndex] > 0 then
            self:_SetListUpRedDotVisible(true)
            return
        end
    end

    self:_SetListUpRedDotVisible(false)
end


ManualCraftPopupsCtrl._UpdateListDownRedDot = HL.Method() << function(self)
    if not self.view.itemList.gameObject.activeSelf then
        self:_SetListDownRedDotVisible(false)
        return
    end

    local res = self.view.itemList:GetShowRange()
    local showRangeX = res.x
    local showRangeY = res.y

    for luaIndex = showRangeX + 7, showRangeY + 1 do
        if self.m_unlockItemList[luaIndex] ~= nil and self.m_redDotDownCache[luaIndex] ~= nil and self.m_redDotDownCache[luaIndex] > 0 then
            local cell = self.m_luaIndex2ItemCell[luaIndex]
            if cell then
                local pos2 = cell.redDot.gameObject.transform.position
                local screen2 = self.uiCamera:WorldToScreenPoint(pos2)
                local uiPos, isInside = UIUtils.screenPointToUI(Vector2(screen2.x,screen2.y), self.uiCamera, self.view.itemList.gameObject.transform)
                if not self.view.itemList.gameObject.transform.rect:Contains(uiPos) then
                    self:_SetListDownRedDotVisible(true)
                    return
                end
            end
        end
    end

    if showRangeY >= 8 then
        local checkLuaIndex = showRangeY + 2
        if #self.m_unlockItemList >= checkLuaIndex then
            if self.m_redDotDownCache[checkLuaIndex] ~= nil and self.m_redDotDownCache[checkLuaIndex] > 0 then
                self:_SetListDownRedDotVisible(true)
                return
            end
        end
    end

    self:_SetListDownRedDotVisible(false)
end

ManualCraftPopupsCtrl._SetListUpRedDotVisible = HL.Method(HL.Boolean) << function(self, visible)
    if visible then
        if not self.view.redDotListUp.gameObject.activeSelf then
            self.view.redDotListUp.gameObject:SetActive(true)
        end
    else
        if self.view.redDotListUp.gameObject.activeSelf then
            self.view.redDotListUp.gameObject:SetActive(false)
        end
    end
end

ManualCraftPopupsCtrl._SetListDownRedDotVisible = HL.Method(HL.Boolean) << function(self, visible)
    if visible then
        if not self.view.redDotListDown.gameObject.activeSelf then
            self.view.redDotListDown.gameObject:SetActive(true)
        end
    else
        if self.view.redDotListDown.gameObject.activeSelf then
            self.view.redDotListDown.gameObject:SetActive(false)
        end
    end
end


ManualCraftPopupsCtrl._FilterBtnConfirm = HL.Method(HL.Any) << function(self, tags)
    for i = 1, #self.m_filterSetting do
        self.m_filterSetting[i].isOn = false
    end

    if tags ~= nil then
        for i = 1,#tags do
            for j = 1,#self.m_filterSetting do
                if self.m_filterSetting[j].id == tags[i].id then
                    self.m_filterSetting[j].isOn = true
                end
            end
        end
    end
end

ManualCraftPopupsCtrl._FilterBtnGetResCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local noSelect = #tags == 0
    if noSelect then
        return #self.m_unlockItemList
    end

    local count = 0
    for i = 1, #self.m_realData do
        local itemId = self.m_realData[i].itemId
        local nowCount = self.m_facManualCraftSystem:GetItemAccumulateCount(itemId)
        if nowCount > 0 then
            if noSelect then
                count = count + 1
            else
                for j = 1, #tags do
                    if Tables.itemGatherTextTable:ContainsKey(itemId) then
                        local gather = Tables.itemGatherTextTable:GetValue(itemId)
                        if gather.domainId == tags[j].id then
                            count = count + 1
                            break
                        end
                    end
                end
            end
        end
    end
    return count
end

ManualCraftPopupsCtrl._SortAndFilter = HL.Method() << function(self)
    if not self.m_realData then
        self.m_realData = self.m_unlockItemList
    end
    self.m_unlockItemList = self.m_realData
    local noSelect = true
    for i = 1, #self.m_filterSetting do
        if self.m_filterSetting[i].isOn then
            noSelect = false
            break
        end
    end
    local sortData = {}

    for i = 1, #self.m_unlockItemList do
        local itemId = self.m_unlockItemList[i].itemId
        local nowCount = self.m_facManualCraftSystem:GetItemAccumulateCount(itemId)
        if nowCount > 0 then
            if noSelect then
                table.insert(sortData, self.m_unlockItemList[i])
            else
                for j = 1, #self.m_filterSetting do
                    if Tables.itemGatherTextTable:ContainsKey(itemId) then
                        local gather = Tables.itemGatherTextTable:GetValue(itemId)
                        if self.m_filterSetting[j].isOn and gather.domainId == self.m_filterSetting[j].id then
                            table.insert(sortData, self.m_unlockItemList[i])
                            break
                        end
                    end
                end
            end
        end
    end

    local nowSelectItem = nil
    if self.m_selectIndex then
        nowSelectItem = self.m_unlockItemList[self.m_selectIndex]
    end
    table.sort(sortData, function(a,b)
        local itemId1 = a.itemId
        local itemId2 = b.itemId

        local canGet1 = self.m_facManualCraftSystem:CheckHaveRewardByItemNoGet(itemId1) > 0
        local canGet2 = self.m_facManualCraftSystem:CheckHaveRewardByItemNoGet(itemId2) > 0

        if canGet1 ~= canGet2 then
            return canGet1
        end

        local allGet1 = self.m_facManualCraftSystem:CheckAllGet(itemId1)
        local allGet2 = self.m_facManualCraftSystem:CheckAllGet(itemId2)

        if allGet1 ~= allGet2 then
            return not allGet1
        end

        local itemData1 = Tables.itemTable:GetValue(itemId1)
        local itemData2 = Tables.itemTable:GetValue(itemId2)
        local sortKeys = UIConst.ManualCraftPopupsSortOptions[self.m_sortMode].sortKeys

        for i = 1, #sortKeys do
            local key = sortKeys[i]
            local value1 = itemData1[key]
            local value2 = itemData2[key]
            if value1 ~= value2 then
                if self.m_sortIncremental then
                    return value1 < value2
                else
                    return value1 > value2
                end
            end
        end

        return false
    end)

    self.m_unlockItemList = sortData

    if nowSelectItem then
        for i = 1, #self.m_unlockItemList do
            if self.m_unlockItemList[i].itemId == nowSelectItem.itemId then
                self.m_selectIndex = i
                break
            end
        end
    end


end


ManualCraftPopupsCtrl._UpdateDefaultView = HL.Method(HL.Boolean) << function(self, needSortAndFilter)
    self.m_tickValidTime = 0.1
    self.view.selectPanel:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):Play("manualcraftpopups_select_out", function()
        self.view.selectPanel.gameObject:SetActive(false)
    end)
    self.view.defaultScrollbarVertical.gameObject:SetActive(true)
    self.view.bottomBg.gameObject:SetActive(true)
    self.m_inMainPanel = true
    self.view.itemList.gameObject:SetActive(true)
    self.view.btnBack.gameObject:SetActive(true)
    if needSortAndFilter then
        self:_SortAndFilter()
    end

    self.m_luaIndex2ItemCell = {}
    self.m_redDotUpCache = {}
    self.m_redDotDownCache = {}
    local redNum = 0
    for luaIndex = #self.m_unlockItemList, 1, -1 do
        local item = self.m_unlockItemList[luaIndex]
        if item ~= nil then
            local active, rdType, expireTs = RedDotManager:GetRedDotState("ManualCraftReward", {itemId = item.itemId})
            if active then
                redNum = redNum + 1
            end
        end
        self.m_redDotDownCache[luaIndex] = redNum
    end

    redNum = 0
    for luaIndex = 1, #self.m_unlockItemList do
        local item = self.m_unlockItemList[luaIndex]
        if item ~= nil then
            local active, rdType, expireTs = RedDotManager:GetRedDotState("ManualCraftReward", {itemId = item.itemId})
            if active then
                redNum = redNum + 1
            end
        end
        self.m_redDotUpCache[luaIndex] = redNum
    end


    self.m_mainCellInAnim = true
    if DeviceInfo.usingController then
        if needSortAndFilter then
            if #self.m_unlockItemList > 0 then
                self.m_controllerItemId = self.m_unlockItemList[1].itemId
            end
        end

        if #self.m_unlockItemList > 0 or self.m_getCurPhaseStateFlag then
            self.m_getCurPhaseStateFlag = false
            self.m_controllerItemId = self.m_unlockItemList[1].itemId
        end

        local controllerBackIndex = -1
        for i = 1, #self.m_unlockItemList do
            local item = self.m_unlockItemList[i]
            if self.m_controllerItemId == item.itemId then
                controllerBackIndex = i - 1
            end
        end
        if controllerBackIndex ~= -1 then
            self.view.itemList:ScrollToIndex(controllerBackIndex)
        else
            if #self.m_unlockItemList > 0 then
                self.m_controllerItemId = self.m_unlockItemList[1].itemId
            end
            controllerBackIndex = 0
        end
        self.view.itemList:UpdateCount(#self.m_unlockItemList, controllerBackIndex)
    else
        self.view.itemList:UpdateCount(#self.m_unlockItemList)
    end

    self.view.sortNodeUp.gameObject:SetActive(true)
    self.view.emptyStateTextNode.gameObject:SetActive(true)

    local gatherMap = {}
    for i = 1, #self.m_realData do
        local itemId = self.m_realData[i].itemId
        local nowCount = self.m_facManualCraftSystem:GetItemAccumulateCount(itemId)
        if Tables.itemGatherTextTable:ContainsKey(itemId) then
            local gather = Tables.itemGatherTextTable:GetValue(itemId)
            if nowCount > 0 then
                gatherMap[gather.domainId] = true
            end
        end
    end

    local count = 0
    for k,v in pairs(gatherMap) do
        count = count + 1
    end
    if count > GATHER_MAP_VALID_COUNT then
        if DeviceInfo.usingController then
            self.view.filterBtn.gameObject:SetActive(false)
        else
            self.view.filterBtn.gameObject:SetActive(true)
        end
    else
        self.view.filterBtn.gameObject:SetActive(false)
    end

    if self.m_selectIndex then
        self:_UpdateActiveScroll()
        self.view.previewScrollList:ScrollToIndex(self.m_selectIndex - 1)
    end

end

ManualCraftPopupsCtrl._UpdateDefaultCell = HL.Method(GameObject, HL.Number) << function(self, gameObject, csIndex)
    local index = LuaIndex(csIndex)
    local cell = self.m_getDefaultCell(gameObject)
    self.m_luaIndex2ItemCell[index] = cell
    local item = self.m_unlockItemList[index]
    local itemData = Tables.itemTable:GetValue(item.itemId)
    cell.gameObject.name = "CardsCell_" .. item.itemId

    cell.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
    cell.commodityText.text = itemData.name
    UIUtils.setItemRarityImage(cell.qualityColor, itemData.rarity)
    local needRefresh = self.m_needRefreshReward

    if not self.m_unlockItemRewardMap[cell] then
        self.m_unlockItemRewardMap[cell] = UIUtils.genCellCache(cell.cell)
        needRefresh = true
    end

    if not self.m_unlockItemIdCellMap[cell] then
        self.m_unlockItemIdCellMap[cell] = item.itemId
    else
        if self.m_unlockItemIdCellMap[cell] ~= item.itemId then
            needRefresh = true
            self.m_unlockItemIdCellMap[cell] = item.itemId
        end
    end

    local _, data = self.m_facManualCraftSystem.manufactureUnlockDataMap:TryGetValue(item.itemId)
    local allGet = self.m_facManualCraftSystem:CheckAllGet(item.itemId)

    cell.redDot:InitRedDot("ManualCraftReward", {itemId = item.itemId})
    if allGet then
        cell.finishBgNode.gameObject:SetActive(true)
    else
        cell.finishBgNode.gameObject:SetActive(false)
    end
    local path = Tables.itemGatherTextTable:GetValue(item.itemId).icon
    cell.icon:LoadSprite(UIConst.UI_SPRITE_MANUAL_CRAFT_TYPE_ICON, path)

    local unlockRewardList = self.m_unlockItemRewardMap[cell]
    if needRefresh then
        unlockRewardList:Refresh(data.rewardList.Count, function(rewardCell, rewardIndex)
            rewardCell.gameObject:SetActive(true)
            rewardIndex = rewardIndex - 1
            local rewardId = data.rewardList[rewardIndex]
            local count = data.rewardCount[rewardIndex]
            local condition = data.unlockCondition[rewardIndex]
            local unlockId = data.unlockIdList[rewardIndex]
            local haveGet = self.m_facManualCraftSystem:CheckHaveGetReward(unlockId)
            rewardCell.gameObject:SetActive(true)
            rewardCell.itemBigBlack:InitItem({id = rewardId, count = count}, false)
            local nowCount = self.m_facManualCraftSystem:GetItemAccumulateCount(data.itemId)


            rewardCell.notAvailable.gameObject:SetActive(not haveGet and nowCount < condition)
            rewardCell.availableEffect.gameObject:SetActive(not haveGet and nowCount >= condition)
            rewardCell.availableEffect.gameObject:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):PlayLoopAnimation()
            rewardCell.rewardedCover.gameObject:SetActive(haveGet)
            self:_UpdateProgress(rewardCell.manufactureProgress, data, rewardIndex)
            if data.rewardList.Count == rewardIndex + 1 then

            end
        end)
    end

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if self.m_mainCellInAnim then
            return
        end
        self.m_selectIndex = index
        self.m_previewIndex = index
        self.m_controllerItemId = item.itemId

        self:_UpdateActiveScroll()
        self:_UpdateSelectView(false)
        self.view.selectPanel:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):Play("manualcraftpopups_select_in")
    end)

    if self.m_initControllerSelected then
        if self.m_controllerItemId == item.itemId then
            self:SetNaviTarget(cell.button)
        end
    else
        if index == LUA_FIRST_INDEX then
            self.m_initControllerSelected = true
            self:SetNaviTarget(cell.button)
        end
    end

end


ManualCraftPopupsCtrl._UpdateSelectView = HL.Method(HL.Boolean) << function(self, needSortAndFilter)
    self.m_inMainPanel = false
    self.view.itemList.gameObject:SetActive(false)
    self.view.selectPanel.gameObject:SetActive(true)
    self.view.sortNodeUp.gameObject:SetActive(false)
    self.view.emptyStateTextNode.gameObject:SetActive(false)

    self.view.filterBtn.gameObject:SetActive(false)
    self.view.bottomBg.gameObject:SetActive(false)

    if self.m_jumpUnLock then
        self.view.btnBack.gameObject:SetActive(true)
        self.view.returnBtn.gameObject:SetActive(false)
    else
        self.view.btnBack.gameObject:SetActive(false)
        self.view.returnBtn.gameObject:SetActive(true)
    end

    self.view.leftArrow.interactable = self.m_selectIndex > 1
    self.view.rightArrow.interactable = self.m_selectIndex < #self.m_unlockItemList
    self.view.defaultScrollbarVertical.gameObject:SetActive(false)
    if needSortAndFilter then
        self:_SortAndFilter()
    end
    self:_UpdateActiveScroll()
    local itemId = self.m_unlockItemList[self.m_selectIndex].itemId
    self.m_rewardCellList = self.m_rewardCellList or UIUtils.genCellCache(self.view.rewardCell)
    local _, data = self.m_facManualCraftSystem.manufactureUnlockDataMap:TryGetValue(self.m_unlockItemList[self.m_selectIndex].itemId)
    self.view.previewScrollList:UpdateCount(#self.m_unlockItemList)
    self.view.previewScrollList:ScrollToIndex(self.m_selectIndex - 1)
    local allChildCount = self.view.progressBar.transform.childCount

    local haveGet = self.m_facManualCraftSystem:CheckHaveRewardByItemNoGet(itemId)
    local allGet = self.m_facManualCraftSystem:CheckHaveRewardByItemAllGet(itemId)
    self.view.btnCommon.gameObject:SetActive(haveGet > 0)
    self.view.completBtn.gameObject:SetActive(allGet)

    for i = 0, allChildCount - 1 do
        self.view.progressBar.transform:GetChild(i).gameObject:SetActive(false)
    end

    local gatherText = Tables.itemGatherTextTable:GetValue(itemId).desc
    self.view.previewText:SetAndResolveTextStyle(gatherText)
    self.m_jumpRewardFlag = false
    self.m_rewardCellList:Refresh(data.rewardList.Count, function(cell, luaIndex)
        cell.gameObject.name = "RewardCell_" .. luaIndex
        local index = CSIndex(luaIndex)
        local rewardId = data.rewardList[index]
        local count = data.rewardCount[index]
        local condition = data.unlockCondition[index]
        local unlockId = data.unlockIdList[index]
        local nowCount = self.m_facManualCraftSystem:GetItemAccumulateCount(data.itemId)

        local haveGetReward = self.m_facManualCraftSystem:CheckHaveGetReward(unlockId);

        local showType = ShowRewardsType.RECEIVED

        cell.receivedNode.gameObject:SetActive(false)
        cell.available.gameObject:SetActive(false)
        cell.notAvailable.gameObject:SetActive(false)

        if haveGetReward then
            showType = ShowRewardsType.RECEIVED
            cell.receivedNode.gameObject:SetActive(true)
        else
            if nowCount >= condition then
                showType = ShowRewardsType.AVAILABLE
                cell.available.gameObject:SetActive(true)
            else
                showType = ShowRewardsType.NOT_AVAILABLE
                cell.notAvailable.gameObject:SetActive(true)
            end
        end

        cell.receivedNode.finish.gameObject:SetActive(haveGetReward)

        cell.available.receiveBtn.onClick:RemoveAllListeners()
        cell.available.receiveBtn.onClick:AddListener(function()
            self.m_facManualCraftSystem:ReqManuallyUnlock(Utils.getCurrentScope(), unlockId)
        end)
        local progressCount = self.m_facManualCraftSystem:GetItemAccumulateCount(data.itemId)
        self.view.progressText.text = progressCount

        local view = {}
        self.view.progressBar.transform:GetChild(index):GetComponent(typeof(CS.Beyond.Lua.LuaReference)):BindToLua(view)
        self:_UpdateProgress(view, data, index)

        local itemDaa = {id = rewardId, count = count}
        local activeNode = cell.available
        if progressCount < condition then
            activeNode = cell.notAvailable
        elseif haveGetReward then
            activeNode = cell.receivedNode
        else
            activeNode = cell.available
        end

        if activeNode.itemBigBlack then
            activeNode.itemBigBlack:InitItem(itemDaa, true)
        end
        activeNode.commodityText.text = Tables.itemTable:GetValue(rewardId).name

        local refreshKeyHint = function(active)
            cell.receivedNode.keyHintR3.gameObject:SetActive(active)
            cell.available.keyHintR3.gameObject:SetActive(active)
            cell.notAvailable.keyHintR3.gameObject:SetActive(active)
        end

        cell.naviDecorator.onIsNaviTargetChanged = function(active)
            refreshKeyHint(active)
        end

        refreshKeyHint(cell.naviDecorator.isNaviTarget)

        if self.m_jumpRewardFlag == false then
            if index == 0 then
                self:SetNaviTarget(cell.naviDecorator)
                refreshKeyHint(cell.naviDecorator.isNaviTarget)
            end
            if showType == ShowRewardsType.AVAILABLE then
                self.m_jumpRewardFlag = true
                self:SetNaviTarget(cell.naviDecorator)
                refreshKeyHint(cell.naviDecorator.isNaviTarget)
            end
        end

        local groupId = cell.inputGroup.groupId
        local alreadyBindingId = self.m_bindSubCache[groupId]
        if alreadyBindingId ~= nil then
            InputManagerInst:DeleteBinding(alreadyBindingId)
            self.m_bindSubCache[groupId] = nil
        end

        self.m_bindSubCache[groupId] = InputManagerInst:CreateBindingByActionId("show_item_tips", function()
            if showType == ShowRewardsType.RECEIVED then
                cell.receivedNode.itemBigBlack:ShowTips()
            elseif showType == ShowRewardsType.AVAILABLE then
                cell.available.itemBigBlack:ShowTips()
            elseif showType == ShowRewardsType.NOT_AVAILABLE then
                cell.notAvailable.itemBigBlack:ShowTips()
            end

            cell.receivedNode.keyHintR3.gameObject:SetActive(false)
            cell.available.keyHintR3.gameObject:SetActive(false)
            cell.notAvailable.keyHintR3.gameObject:SetActive(false)

        end, groupId)
    end)
    self.view.previewScrollList:GetComponent(typeof(CS.UnityEngine.UI.ScrollRect)).horizontal = false

end

ManualCraftPopupsCtrl._UpdateSelectCell = HL.Method(GameObject, HL.Number) << function(self, cell, index)
    if cell == nil then
        logger.error("cell == nil  index:"..index)
        return
    end

    index = index + 1
    cell = self.m_getSelectCell(cell)
    local previewId = self.m_unlockItemList[index].itemId

    if cell == nil then
        logger.error("cell == nil  index:"..index)
    else
        if cell.select == nil then
            logger.error("cell.select == nil  index:"..index)
        end
    end

    local activeNode = cell.select

    if self.m_selectIndex == index then
        cell.select.gameObject:SetActive(true)
        cell.unselected.gameObject:SetActive(false)
        activeNode = cell.select
    else
        cell.select.gameObject:SetActive(false)
        cell.unselected.gameObject:SetActive(true)
        activeNode = cell.unselected
    end
    local itemData = Tables.itemTable:GetValue(previewId)
    activeNode.commodityText.text = itemData.name
    activeNode.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
    UIUtils.setItemRarityImage(activeNode.qualityColor, itemData.rarity)
    local path = Tables.itemGatherTextTable:GetValue(previewId).icon
    cell.select.typeIcon1:LoadSprite(UIConst.UI_SPRITE_MANUAL_CRAFT_TYPE_ICON, path)
    cell.unselected.typeIcon2:LoadSprite(UIConst.UI_SPRITE_MANUAL_CRAFT_TYPE_ICON, path)

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if self.m_mainCellInAnim then
            return
        end
        if self.m_inMainPanel then
            return
        end
        if self.m_selectIndex == index then
            return
        end
        AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
        self.m_selectIndex = index
        self.m_facManualCraftSystem:ReadRewardByItem(previewId)
        self:_UpdateSelectView(false)
        self.view.selectPanel:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):Play("manualcraftpopups_select_switch")
    end)

    cell.redDot:InitRedDot("ManualCraftReward", {itemId = self.m_unlockItemList[index].itemId})

end

ManualCraftPopupsCtrl._UpdateProgress = HL.Method(HL.Any, HL.Any, HL.Number) << function(self, cell, unlockData, index)
    cell.gameObject:SetActive(true)
    cell.text.text = tostring(unlockData.unlockCondition[index])
    local nowCount = self.m_facManualCraftSystem:GetItemAccumulateCount(unlockData.itemId)
    local isUnlock = nowCount >= unlockData.unlockCondition[index]
    cell.selected.gameObject:SetActive(isUnlock)
    cell.selectedLine.gameObject:SetActive(false)
    cell.text.color = isUnlock and Color.black or Color.white

    if cell.defalutLine then
        cell.defalutLine.gameObject:SetActive(false)
        if index < unlockData.rewardList.Count - 1 then
            cell.defalutLine.gameObject:SetActive(true)
            if cell.line then
                cell.line.gameObject:SetActive(true)
            end
        end
    end
    if index < unlockData.rewardList.Count - 1 then
        cell.selectedLine.gameObject:SetActive(true)
        local lastCount = index ~= 0 and  unlockData.unlockCondition[index] or 0
        if nowCount == 1 then
            nowCount = 0
        end
        cell.selectedLine.fillAmount = (nowCount - lastCount) / (unlockData.unlockCondition[index + 1] - lastCount)
    end

    if unlockData.rewardList.Count < 5 and index == unlockData.rewardList.Count - 1 and cell.line then
        cell.line.gameObject:SetActive(false)
    end
end

ManualCraftPopupsCtrl._GetItemCount = HL.Method(HL.String).Return(HL.Any) << function(self, itemId)
    if Utils.isInSafeZone() or self:_IsValuableItem(itemId) then
        return self.m_inventorySystem:GetItemCount(Utils.getCurrentScope(), Utils.getCurrentChapterId(), itemId)
    else
        return self.m_inventorySystem:GetItemCountInBag(Utils.getCurrentScope(), itemId)
    end
end

ManualCraftPopupsCtrl._IsValuableItem = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    local itemData = Tables.itemTable[itemId]
    local valuableDepotType = itemData.valuableTabType
    if valuableDepotType ~= CS.Beyond.GEnums.ItemValuableDepotType.Factory then
        return true
    else
        return false
    end
end

ManualCraftPopupsCtrl.OnGetNewItemLevelUp = HL.Method(HL.Any) << function(self, args)
    self.m_needRefreshReward = true
    PhaseManager:OpenPhase(PhaseId.ManualcraftUpgradePopup,{itemList = args})
    if self.view.selectPanel.gameObject.activeSelf then
        self:_UpdateSelectView(false)
    else
        self:_UpdateDefaultView(true)
    end
end

ManualCraftPopupsCtrl._OnGetReward = HL.Method(HL.Any) << function(self, newItems)
    self.m_needRefreshReward = true
    self:OnGetReward(newItems)
end

ManualCraftPopupsCtrl.OnGetReward = HL.Method(HL.Any) << function(self, newItems)
    local idList = newItems[1]
    local info = {
        title = Language.LUA_FAC_MANUAL_CRAFT_REWARD,
        onComplete = function()
            if self.view.selectPanel.gameObject.activeSelf then
                self:_UpdateSelectView(false)
            else
                self:_UpdateDefaultView(true)
            end
        end,
    }
    info.items = {}
    local levelUp = {}
    for i = 0, idList.Count - 1 do
        local id = idList[i]
        local data = Tables.factoryManualCraftFormulaUnlockTable:GetValue(id)
        local itemId = data.rewardItemId1
        local count = data.rewardItemCount1
        local itemData = Tables.itemTable:GetValue(itemId)

        table.insert(info.items, {
            id = itemId,
            count = count,
        })

        if itemData.type == CS.Beyond.GEnums.ItemType.FormulaLevelUp then
            table.insert(levelUp, id)
        end
    end

    if #levelUp > 0 then
        if #info.items > 0 then
            info.onComplete = function()
                Notify(MessageConst.ON_MANUAL_CRAFT_LEVEL_UP, levelUp)
            end
        else
            Notify(MessageConst.ON_MANUAL_CRAFT_LEVEL_UP, levelUp)
        end
    end

    if #info.items > 0 then
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, info)
    end
end





ManualCraftPopupsCtrl.OnClose = HL.Override() << function(self)
    self.view.previewScrollList.onScrollEnd:RemoveAllListeners()
    if self.m_listRedDotHandle > 0 then
        self.m_listRedDotHandle = LuaUpdate:Remove(self.m_listRedDotHandle)
    end
    Notify(MessageConst.ON_MANUAL_CRAFT_POPUP_PANEL_CLOSE)
end




HL.Commit(ManualCraftPopupsCtrl)
