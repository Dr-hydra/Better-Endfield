local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacMixPool
local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

local MainState = {
    None = "None",
    Normal = "Normal",
    Select = "Select",
}

local CacheItemSlotState = {
    None = "None",
    Empty = "Empty",
    Normal = "Normal",
    Locked = "Locked",
    Blocked = "Blocked",
    Dimmed = "Dimmed",
}

local SelectorState = {
    None = "None",
    Empty = "Empty",
    Normal = "Normal",
}

local CenterState = {
    None = "None",
    Blocked = "Blocked",
    Normal = "Normal",
}

local ArrowState = {
    None = "None",
    Active = "Active",
    Inactive = "Inactive",
}

local NaviDir = {
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4,
}
















local FIND_DEFAULT_CACHE_NAVI_TARGET_PRIORITY = { 2, 4, 1, 5, 3 }
local EXPANSION_FIND_DEFAULT_CACHE_NAVI_TARGET_PRIORITY = { 3, 5, 1, 7, 2, 8, 4, 6 }
local SELECTOR_LEFT_CACHE_TARGET_MAP = { 3, 3, 3 }
local EXPANSION_SELECTOR_LEFT_CACHE_TARGET_MAP = { 4, 6, 4 }

local BUILDINGCOMMON_MOVE_BTN_NAVI_DOWN = 1
local BUILDINGCOMMON_DEL_BTN_NAVI_DOWN = 2

local DEFAULT_SELECTOR_NAVI_INDEX_PRIORITY = { 3, 1, 2 }

FacMixPoolCtrl = HL.Class('FacMixPoolCtrl', uiCtrl.UICtrl)

local CACHE_ITEM_SLOT_VIEW_NAME_FORMAT = "itemSlot%d"
local CACHE_INPUT_ARROW_VIEW_NAME_FORMAT = "inputNode%d"
local CACHE_OUTPUT_ARROW_VIEW_NAME_FORMAT = "outputNode%d"
local MAX_POOL_CACHE_SLOT_COUNT = 5
local MAX_EXPANSION_POOL_CACHE_SLOT_COUNT = 8
local MAIN_SELECT_MODE_IN_ANIM_NAME = {
    "mixpool_select_in",
    "mixpool_select_in_Space_Bottom",
    "mixpool_select_in_Space_Top"
}
local MAIN_SELECT_MODE_OUT_ANIM_NAME = "mixpool_select_out"

local ARROW_NODE_ANIM_REFRESH_MAX_COUNT = 4
local ARROW_NODE_ANIM_REFRESH_VIEW_NAME_FORMAT = "arrow%d"

local EXPANSION_MIX_POOL_TEMPLATE_ID = "mix_pool_2"

FacMixPoolCtrl.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_FluidReaction)

FacMixPoolCtrl.m_onCacheChanged = HL.Field(HL.Function)

FacMixPoolCtrl.m_cacheItemDataList = HL.Field(HL.Table)

FacMixPoolCtrl.m_cacheItemIdToIndexMap = HL.Field(HL.Table)

FacMixPoolCtrl.m_nextValidIndex = HL.Field(HL.Number) << -1

FacMixPoolCtrl.m_inputItemList = HL.Field(HL.Table)

FacMixPoolCtrl.m_outputItemList = HL.Field(HL.Table)

FacMixPoolCtrl.m_selectorConfig = HL.Field(HL.Table)

FacMixPoolCtrl.m_selectModeIndex = HL.Field(HL.Number) << -1

FacMixPoolCtrl.m_selectModeItemId = HL.Field(HL.String) << ""

FacMixPoolCtrl.m_lastHoverTipsItemTag = HL.Field(HL.String) << ""

FacMixPoolCtrl.m_stopHoverTips = HL.Field(HL.Boolean) << false

FacMixPoolCtrl.m_isInSelectMode = HL.Field(HL.Boolean) << false

FacMixPoolCtrl.m_formulaIdList = HL.Field(HL.Table)

FacMixPoolCtrl.m_blockedFormulaIdList = HL.Field(HL.Table)

FacMixPoolCtrl.m_isExpansionPool = HL.Field(HL.Boolean) << false

FacMixPoolCtrl.m_cacheNode = HL.Field(HL.Table)





FacMixPoolCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_NAVI_TO_MIXPOOL_TARGET_ITEM] = "_OnActionNaviToTarget",
}


FacMixPoolCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo
    self.m_cacheItemDataList = {}
    self.m_cacheItemIdToIndexMap = {}

    self.view.facCacheBelt:InitFacCacheBelt(self.m_buildingInfo, { noGroup = true })

    self.view.facCachePipe:InitFacCachePipe(self.m_buildingInfo, { needInversePipe = true })

    self:_InitPoolCache()

    self.view.buildingCommon:InitBuildingCommon(self.m_buildingInfo, {
        onStateChanged = function(state)
            self:_RefreshPoolCacheArrowsRunningState()
            self:_RefreshPoolFormulaRunningAnimState()
        end
    })

    self:_InitPoolFormula()
    self:_InitPoolSelector()

    self:_UpdateAndRefreshAll()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateAndRefreshAll()
        end
    end)

    self:_InitMixPoolController()
    self:_TryRecoverState(arg and arg.recoverState)
end

FacMixPoolCtrl.OnClose = HL.Override() << function(self)
    self:_ClearPoolCache()
end

FacMixPoolCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if not self.m_isInSelectMode or self.m_selectorConfig == nil or self.m_selectorConfig[self.m_selectModeIndex] == nil then
        return nil
    end
    return {
        selectModeIndex = self.m_selectModeIndex,
        selectModeItemId = self.m_selectModeItemId,
    }
end

FacMixPoolCtrl._TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local selectorIndex = recoverState.selectModeIndex
    if selectorIndex == nil or self.m_selectorConfig == nil or self.m_selectorConfig[selectorIndex] == nil then
        return
    end
    self:_OnEnterPoolSelectMode(selectorIndex)
    local selectItemId = recoverState.selectModeItemId or ""
    self:_SetAndRefreshPoolSelectModeSelectorState(selectItemId)
    self:_RefreshPoolCacheSelectModeState(true)
    if DeviceInfo.usingController and not string.isEmpty(selectItemId) then
        local cacheIndex = self.m_cacheItemIdToIndexMap[selectItemId]
        local itemSlot = cacheIndex and self:_GetPoolCacheItemSlotByIndex(cacheIndex) or nil
        if itemSlot ~= nil then
            self:SetNaviTarget(itemSlot.button)
        end
    end
end




FacMixPoolCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if not active then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
        self.m_lastHoverTipsItemTag = ""
    end
    self.m_stopHoverTips = not active
end

FacMixPoolCtrl._UpdateAndRefreshAll = HL.Method() << function(self)
    self:_UpdatePoolFormulaDataList()
    self:_UpdatePoolCacheItemDataList()
    self:_RefreshPoolCacheItemSlotList()
    self:_RefreshPoolSelectorList()
    self:_RefreshPoolCacheSlotHighlightState()
    self:_RefreshPoolFormulaState()

    if self.m_isInSelectMode then
        self:_RefreshPoolCacheSelectModeState(true)
    end

    if DeviceInfo.usingController then
        self:_RefreshShowItemTipsBindingState()
    end
end




FacMixPoolCtrl._GetPoolMaxSlotCount = HL.Method().Return(HL.Number) << function(self)
    return self.m_isExpansionPool and MAX_EXPANSION_POOL_CACHE_SLOT_COUNT or MAX_POOL_CACHE_SLOT_COUNT
end

FacMixPoolCtrl._InitPoolCache = HL.Method() << function(self)
    self.m_onCacheChanged = function(changedItems, hasNewOrRemove)
        self:_OnPoolCacheChanged(changedItems, hasNewOrRemove)
    end
    self.m_buildingInfo.cache.onCacheChanged:AddListener(self.m_onCacheChanged)
    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(self.m_buildingInfo.nodeId)

    self.m_isExpansionPool = self.m_buildingInfo.nodeHandler.templateId == EXPANSION_MIX_POOL_TEMPLATE_ID
    self.m_cacheNode = self.m_isExpansionPool and self.view.expansionCacheNode or self.view.cacheNode
    self.view.cacheNode.gameObject:SetActive(not self.m_isExpansionPool)
    self.view.expansionCacheNode.gameObject:SetActive(self.m_isExpansionPool)


    self.view.autoClearNode.gameObject:SetActiveIfNecessary(self.m_isExpansionPool)
    if self.m_isExpansionPool then
        self.view.autoClearNode.autoToggle:SetIsOnWithoutNotify(self.m_buildingInfo.fluidReaction.autoClearCacheWhenAllBlock)
        self.view.autoClearNode.autoToggle.onValueChanged:AddListener(function(isOn)
            GameInstance.player.remoteFactory.core:Message_SetFluidReactionAutoClearCacheWhenAllBlock(
                ScopeUtil.GetCurrentChapterId(),
                self.m_buildingInfo.fluidReaction.componentId,
                isOn)
        end)
        self.view.autoClearNode.tipsBtn.onClick:AddListener(function()
            self.view.autoClearNode.tipsInfoNode.gameObject:SetActiveIfNecessary(true)
            if DeviceInfo.usingController then
                Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                    panelId = PANEL_ID,
                    isGroup = true,
                    id = self.view.autoClearNode.tipsInfoNode.groupId,
                    hintPlaceholder = self.view.buildingCommon.view.controllerHintPlaceholder,
                    rectTransform = self.view.autoClearNode.tipsInfoNode.transform,
                    noHighlight = true,
                })
            end
        end)
        self.view.autoClearNode.tipsMaskBtn.onClick:AddListener(function()
            self.view.autoClearNode.tipsInfoNode.gameObject:SetActiveIfNecessary(false)
            if DeviceInfo.usingController then
                Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.autoClearNode.tipsInfoNode.groupId)
            end
        end)
    end


    self:_ClearPoolCacheItemDataList()
end

FacMixPoolCtrl._ClearPoolCache = HL.Method() << function(self)
    self.m_buildingInfo.cache.onCacheChanged:RemoveListener(self.m_onCacheChanged)
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_buildingInfo.nodeId)
end

FacMixPoolCtrl._ClearPoolCacheItemDataList = HL.Method() << function(self)
    for index = 1, self:_GetPoolMaxSlotCount() do
        if self.m_cacheItemDataList[index] == nil then
            self.m_cacheItemDataList[index] = {
                id = "",
                count = 0,
            }
        end
    end
end

FacMixPoolCtrl._OnPoolCacheChanged = HL.Method(HL.Userdata, HL.Boolean) << function(self, changedItems, hasNewOrRemove)
    self:_UpdateAndRefreshAll()
end

FacMixPoolCtrl._UpdatePoolCacheItemDataList = HL.Method() << function(self)
    local items = self.m_buildingInfo.cache.items
    local itemOrderMap = self.m_buildingInfo.cache.itemOrderMap

    local dirtyItemIdList = {}
    for id, count in cs_pairs(items) do
        local orderSuccess, csIndex = itemOrderMap:TryGetValue(id)
        if orderSuccess then
            local luaIndex = LuaIndex(csIndex)

            local lastIndex = self.m_cacheItemIdToIndexMap[id]
            if lastIndex ~= nil and lastIndex ~= luaIndex then

                self.m_cacheItemDataList[lastIndex] = {
                    id = "",
                    count = 0,
                }
            end

            self.m_cacheItemDataList[luaIndex] = {
                id = id,
                count = count,
            }
            dirtyItemIdList[id] = true
        end
    end


    for _, itemData in pairs(self.m_cacheItemDataList) do
        local itemId = itemData.id
        if not dirtyItemIdList[itemId] then
            itemData.count = 0
        end
    end

    self:_RecordItemIdToIndexMap()

    local fillFunction = function(itemList)
        for itemId, _ in pairs(itemList) do
            if self.m_nextValidIndex > self.m_buildingInfo.cache.size then
                break
            end
            if self.m_cacheItemIdToIndexMap[itemId] == nil then

                self.m_cacheItemDataList[self.m_nextValidIndex] = {
                    id = itemId,
                    count = 0,
                }
            end
        end
    end

    fillFunction(self.m_inputItemList)
    fillFunction(self.m_outputItemList)

    self:_RecordItemIdToIndexMap()
end

FacMixPoolCtrl._RecordItemIdToIndexMap = HL.Method() << function(self)
    self.m_cacheItemIdToIndexMap = {}
    local maxIndex = self.m_buildingInfo.cache.size + 1
    self.m_nextValidIndex = maxIndex
    for index, itemData in ipairs(self.m_cacheItemDataList) do
        local itemId = itemData.id
        if not string.isEmpty(itemData.id) then
            self.m_cacheItemIdToIndexMap[itemId] = index

            local nextIndex = index + 1
            if nextIndex < maxIndex and nextIndex < self.m_nextValidIndex then
                local nextItemData = self.m_cacheItemDataList[nextIndex]
                if string.isEmpty(nextItemData.id) then
                    self.m_nextValidIndex = nextIndex
                end
            end
        end
    end
end

FacMixPoolCtrl._RefreshPoolCacheItemSlotList = HL.Method() << function(self)
    for index = 1, self:_GetPoolMaxSlotCount() do
        self:_RefreshPoolCacheItemSlot(index)
        self:_RefreshPoolCacheArrowState(index)
    end
end

FacMixPoolCtrl._RefreshPoolCacheItemSlot = HL.Method(HL.Number) << function(self, index)
    local itemData = self.m_cacheItemDataList[index]
    if itemData == nil then
        return
    end

    local itemSlot = self:_GetPoolCacheItemSlotByIndex(index)
    if itemSlot == nil then
        return
    end

    if index > self.m_buildingInfo.cache.size then
        self:_RefreshPoolCacheItemState(index, CacheItemSlotState.Locked)
        return
    end

    local itemId = itemData.id
    if string.isEmpty(itemId) then
        self:_RefreshPoolCacheItemState(index, CacheItemSlotState.Empty)
        return
    end

    itemSlot.item:InitItem(itemData)
    itemSlot.button.onClick:RemoveAllListeners()
    itemSlot.button.onClick:AddListener(function()
        self:_OnClickPoolCacheItemSlot(index)
    end)
    itemSlot.button.onHoverChange:RemoveAllListeners()
    itemSlot.button.onHoverChange:AddListener(function(isHover)
        self:_OnHoverPoolCacheItemSlot(index, isHover)
    end)

    local itemCount = itemData.count
    if itemCount > 0 then
        local success, data = Tables.factoryItemTable:TryGetValue(itemId)
        if success then
            local maxStackBuffer = data.buildingBufferStackLimit
            local state = itemCount < maxStackBuffer and CacheItemSlotState.Normal or CacheItemSlotState.Blocked
            self:_RefreshPoolCacheItemState(index, state)
        end
    else
        self:_RefreshPoolCacheItemState(index, CacheItemSlotState.Dimmed)
    end
end

FacMixPoolCtrl._RefreshPoolCacheArrowState = HL.Method(HL.Number) << function(self, index)
    local itemData = self.m_cacheItemDataList[index]
    if itemData == nil then
        return
    end

    local itemId = itemData.id
    local inputArrow = self.m_cacheNode.inputArrowList[string.format(CACHE_INPUT_ARROW_VIEW_NAME_FORMAT, index)]
    local outputArrow = self.m_cacheNode.outputArrowList[string.format(CACHE_OUTPUT_ARROW_VIEW_NAME_FORMAT, index)]

    if index > self.m_buildingInfo.cache.size then
        inputArrow.gameObject:SetActive(false)
        outputArrow.gameObject:SetActive(false)
        return
    end

    local isInput = self.m_inputItemList[itemId]
    local isOutput = self.m_outputItemList[itemId]
    inputArrow.gameObject:SetActive(true)
    outputArrow.gameObject:SetActive(true)

    local inArrowState = isInput and ArrowState.Active or ArrowState.Inactive
    inputArrow.stateController:SetState(inArrowState)

    local outArrowState = isOutput and ArrowState.Active or ArrowState.Inactive
    outputArrow.stateController:SetState(outArrowState)

    self:_RefreshPoolCacheArrowRunningState(index)
end

FacMixPoolCtrl._RefreshPoolCacheArrowsRunningState = HL.Method() << function(self)
    for index = 1, self:_GetPoolMaxSlotCount() do
        if index <= self.m_buildingInfo.cache.size then
            self:_RefreshPoolCacheArrowRunningState(index)
        end
    end
end

FacMixPoolCtrl._RefreshPoolCacheArrowRunningState = HL.Method(HL.Number) << function(self, index)
    local inputArrow = self.m_cacheNode.inputArrowList[string.format(CACHE_INPUT_ARROW_VIEW_NAME_FORMAT, index)]
    local outputArrow = self.m_cacheNode.outputArrowList[string.format(CACHE_OUTPUT_ARROW_VIEW_NAME_FORMAT, index)]

    local isRunning = self.view.buildingCommon.lastState == GEnums.FacBuildingState.Normal

    local stateRefreshFunc = function(arrow, isIn)
        if isRunning then
            local animName = isIn and "facmixpoolwhitenormalbg_loop" or "facmixpoolnormalbg_loop"
            if arrow.animationWrapper.curStateName ~= animName then
                arrow.animationWrapper:PlayWithTween(animName)
            end
            arrow.isRunning = true
        else
            local animName = isIn and "facmixpoolwhitenormalbg_default" or "facmixpoolnormalbg_default"
            if arrow.animationWrapper.curStateName ~= animName then
                arrow.animationWrapper:PlayWithTween(animName)
            end
            arrow.isRunning = false
        end
        for arrowIndex = 1, ARROW_NODE_ANIM_REFRESH_MAX_COUNT do
            local arrowNode = arrow[string.format(ARROW_NODE_ANIM_REFRESH_VIEW_NAME_FORMAT, arrowIndex)]
            arrowNode.gameObject:SetActive(isRunning)
        end
        arrow.staticArrow.gameObject:SetActive(not isRunning)
    end

    if inputArrow.stateController.curStateName == ArrowState.Active then
        stateRefreshFunc(inputArrow, true)
    end

    if outputArrow.stateController.curStateName == ArrowState.Active then
        stateRefreshFunc(outputArrow, false)
    end
end

FacMixPoolCtrl._RefreshPoolCacheItemState = HL.Method(HL.Number, HL.String) << function(self, index, state)
    local cacheItemList = self.m_cacheNode.cacheItemList
    local itemSlot = cacheItemList[string.format(CACHE_ITEM_SLOT_VIEW_NAME_FORMAT, index)]
    if itemSlot == nil then
        return
    end

    local stateChanged = itemSlot.controller.curStateName ~= nil and itemSlot.controller.curStateName ~= state
    local isNormalOrBlockedState = state == CacheItemSlotState.Normal or state == CacheItemSlotState.Blocked
    if stateChanged and isNormalOrBlockedState then
        local animName = state == CacheItemSlotState.Normal and "facmixpoolitemblocked_out" or "facmixpoolitemblocked_in"
        itemSlot.animationWrapper:PlayWithTween(animName, function()
            if state == CacheItemSlotState.Normal then
                itemSlot.controller:SetState(state)
            end
        end)
        if state == CacheItemSlotState.Blocked then
            itemSlot.controller:SetState(state)
        end
    else
        itemSlot.controller:SetState(state)
    end

    itemSlot.item.view.count.color = state == CacheItemSlotState.Blocked and
        self.view.config.CACHE_SLOT_BLOCKED_COUNT_COLOR or
        Color.white
end

FacMixPoolCtrl._GetPoolCacheItemSlotByIndex = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    return self.m_cacheNode.cacheItemList[string.format(CACHE_ITEM_SLOT_VIEW_NAME_FORMAT, index)]
end

FacMixPoolCtrl._GetPoolCacheItemDataById = HL.Method(HL.String).Return(HL.Any) << function(self, itemId)
    for _, itemData in pairs(self.m_cacheItemDataList) do
        if itemData.id == itemId then
            return itemData
        end
    end
    return nil
end

FacMixPoolCtrl._OnClickPoolCacheItemSlot = HL.Method(HL.Number) << function(self, index)
    local itemData = self.m_cacheItemDataList[index]
    if itemData == nil then
        return
    end

    local itemSlot = self.m_cacheNode.cacheItemList[string.format(CACHE_ITEM_SLOT_VIEW_NAME_FORMAT, index)]
    if itemSlot == nil then
        return
    end

    AudioAdapter.PostEvent("Au_UI_Button_Item")

    if self.m_isInSelectMode then
        self:_SetAndRefreshPoolSelectModeSelectorState(itemData.id)
    else
        itemSlot.highlightBg.gameObject:SetActive(true)
        itemSlot.item:ShowTips(nil, function()
            if self.m_isClosed then
                return
            end
            itemSlot.highlightBg.gameObject:SetActive(false)
        end)
    end
end

FacMixPoolCtrl._OnHoverPoolCacheItemSlot = HL.Method(HL.Number, HL.Boolean) << function(self, index, isHover)
    if not isHover then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
    end

    local itemData = self.m_cacheItemDataList[index]
    if itemData == nil then
        return
    end

    local itemSlot = self.m_cacheNode.cacheItemList[string.format(CACHE_ITEM_SLOT_VIEW_NAME_FORMAT, index)]
    if itemSlot == nil then
        return
    end

    if itemSlot.item.showingTips then
        return
    end

    if isHover then
        Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
            itemId = itemData.id,
            delay = DeviceInfo.usingController and 0 or self.view.config.HOVER_TIP_SHOW_DELAY,
            targetRect = itemSlot.item.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
        })
    end
end






FacMixPoolCtrl._InitPoolFormula = HL.Method() << function(self)
    self.m_cacheNode.formulaButton.onClick:AddListener(function()
        Notify(MessageConst.FAC_SHOW_FORMULA, {
            nodeId = self.m_buildingInfo.nodeId,
            buildingId = self.m_buildingInfo.buildingId,
            isMachineCrafterFormula = true,
            belongingCanvasGroup = self.view.canvasGroup,
            highlightFormulaIdList = self.m_formulaIdList,
            blockFormulaIdList = self.m_blockedFormulaIdList,
            machineCrafterFormulaMode = "liquid",
        })
    end)

    if not Utils.isInBlackbox() then
        self.m_cacheNode.redDot:InitRedDot("BuildingFormula", {
            buildingId = self.m_buildingInfo.buildingId,
            modeName = FacConst.FAC_FORMULA_MODE_MAP.LIQUID
        })
        self.m_cacheNode.redDot.gameObject:SetActive(true)
    else
        self.m_cacheNode.redDot.gameObject:SetActive(false)
    end
end

FacMixPoolCtrl._UpdatePoolFormulaDataList = HL.Method() << function(self)
    local formulaList = self.m_buildingInfo.fluidReaction.formulas
    self.m_inputItemList = {}
    self.m_outputItemList = {}
    for _, formula in cs_pairs(formulaList) do
        local formulaId = formula.formulaId
        local success, formulaData = Tables.factoryMachineCraftTable:TryGetValue(formulaId)
        if success then
            local ingredients = formulaData.ingredients
            for ingredientIdx = 0, ingredients.Count - 1 do
                local ingredient = ingredients[ingredientIdx]
                local inputItemList = ingredient.group
                for inputBundleIdx = 0, inputItemList.Count - 1 do
                    local inputItemBundle = inputItemList[inputBundleIdx]
                    self.m_inputItemList[inputItemBundle.id] = true
                end
            end
            local outcomes = formulaData.outcomes
            for outcomeIdx = 0, outcomes.Count - 1 do
                local outcome = outcomes[outcomeIdx]
                local outputItemList = outcome.group
                for outputBundleIdx = 0, outputItemList.Count - 1 do
                    local outputItemBundle = outputItemList[outputBundleIdx]
                    self.m_outputItemList[outputItemBundle.id] = true
                end
            end
        end
    end

    self.m_formulaIdList = {}
    self.m_blockedFormulaIdList = {}
    for _, formulaData in cs_pairs(self.m_buildingInfo.fluidReaction.formulas) do
        local formulaId = formulaData.formulaId
        table.insert(self.m_formulaIdList, formulaId)
        if self.m_buildingInfo:IsBlockedFormula(formulaId) then
            table.insert(self.m_blockedFormulaIdList, formulaId)
        end
    end
end

FacMixPoolCtrl._RefreshPoolFormulaState = HL.Method() << function(self)
    local state = next(self.m_blockedFormulaIdList) == nil and CenterState.Normal or CenterState.Blocked
    if self.m_cacheNode.centerController.curStateName ~= nil and self.m_cacheNode.centerController.curStateName ~= state then
        local animName = state == CenterState.Blocked and "facmixpoolblocked_in" or "facmixpoolblocked_out"
        self.m_cacheNode.centerAnimationWrapper:PlayWithTween(animName, function()
            self:_RefreshPoolFormulaRunningAnimState()
        end)
    end
    self.m_cacheNode.centerController:SetState(state)
end

FacMixPoolCtrl._RefreshPoolFormulaRunningAnimState = HL.Method() << function(self)
    local isRunning = self.view.buildingCommon.lastState == GEnums.FacBuildingState.Normal
    local animName = isRunning and "facmixpoolblocked_loop" or "facmixpoolblocked_gray"
    self.m_cacheNode.centerAnimationWrapper:PlayWithTween(animName)
end






FacMixPoolCtrl._InitPoolSelector = HL.Method() << function(self)
    self:_InitPoolSelectorConfig()
    self:_InitPoolSelectorButtons()
    self.view.mainController:SetState(MainState.Normal)
end

FacMixPoolCtrl._InitPoolSelectorConfig = HL.Method() << function(self)











    local selectorNode = self.view.selectorNode
    self.m_selectorConfig = {
        {
            viewSelector = selectorNode.selector1,
            compSelector = self.m_buildingInfo.selector1,
            viewPortNodeList = {
                self.view.facCacheBelt.view.outBeltGroup,
                self.view.facCacheBelt.view.outFacLineDrawer,
                self.view.facCacheBelt.view.outEndLineCell,
            },
            isFluid = false,
        },
        {
            viewSelector = selectorNode.selector2,
            compSelector = self.m_buildingInfo.selector2,
            viewPortNodeList = {
                self.view.facCachePipe.view.pipeCell4
            },
            isFluid = true,
        },
        {
            viewSelector = selectorNode.selector3,
            compSelector = self.m_buildingInfo.selector3,
            viewPortNodeList = {
                self.view.facCachePipe.view.pipeCell3
            },
            isFluid = true,
        },
    }
end

FacMixPoolCtrl._InitPoolSelectorButtons = HL.Method() << function(self)
    for selectorIndex, selectorInfo in ipairs(self.m_selectorConfig) do
        local viewSelector = selectorInfo.viewSelector
        viewSelector.selectButton.onClick:AddListener(function()
            self:_OnEnterPoolSelectMode(selectorIndex)
        end)
        viewSelector.selectButton.onHoverChange:AddListener(function(isHover)
            self:_OnHoverSelectorSelectButton(selectorIndex, isHover)
        end)
        viewSelector.switchButton.onClick:AddListener(function()
            self:_OnEnterPoolSelectMode(selectorIndex)
        end)
    end

    self.view.selectModeNode.confirmButton.onClick:AddListener(function()
        self:_OnClickSelectModeConfirmBtn()
    end)
    self.view.selectModeNode.cancelButton.onClick:AddListener(function()
        self:_SetAndRefreshPoolSelectModeSelectorState("")
    end)
    self.view.selectBackBtn.onClick:AddListener(function()
        self:_OnLeavePoolSelectMode()
    end)
    self.view.selectBackMaskBtn.onClick:AddListener(function()
        self:_OnLeavePoolSelectMode()
    end)
end

FacMixPoolCtrl._OnClickSelectModeConfirmBtn = HL.Method() << function(self)
    local selectorInfo = self.m_selectorConfig[self.m_selectModeIndex]
    if selectorInfo == nil then
        return
    end

    self.m_buildingInfo.sender:Message_OpSetSelectTarget(
        Utils.getCurrentChapterId(),
        selectorInfo.compSelector.componentId,
        self.m_selectModeItemId,
        function()
            self.m_buildingInfo:Update()
            self:_UpdateAndRefreshAll()
            self:_OnLeavePoolSelectMode()
        end
    )
end

FacMixPoolCtrl._OnHoverSelectorSelectButton = HL.Method(HL.Number, HL.Boolean) << function(self, selectorIndex, isHover)
    if not isHover then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
    end

    local selectorInfo = self.m_selectorConfig[selectorIndex]
    local viewSelector = selectorInfo.viewSelector
    if string.isEmpty(viewSelector.item.id) then
        return
    end

    if viewSelector.item.showingTips then
        return
    end

    if isHover then
        Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
            itemId = viewSelector.item.id,
            delay = DeviceInfo.usingController and 0 or self.view.config.HOVER_TIP_SHOW_DELAY,
            targetRect = viewSelector.item.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
        })
    end
end

FacMixPoolCtrl._RefreshPoolSelectorList = HL.Method() << function(self)
    for selectorIndex = 1, #self.m_selectorConfig do
        self:_RefreshPoolSelectorState(selectorIndex)
        self:_RefreshPoolSelectorButtonController(selectorIndex)
    end
end

FacMixPoolCtrl._RefreshPoolSelectorState = HL.Method(HL.Number) << function(self, selectorIndex)
    local selectorInfo = self.m_selectorConfig[selectorIndex]
    if selectorInfo == nil then
        return
    end

    local viewSelector, compSelector = selectorInfo.viewSelector, selectorInfo.compSelector
    local selectItemId = compSelector.selectItemId
    if string.isEmpty(selectItemId) then
        viewSelector.lineCell:ChangeLineColor(self.view.config.INACTIVE_SELECTOR_LINE_COLOR)
        viewSelector.stateController:SetState(SelectorState.Empty)
        return
    end

    local itemData = self:_GetPoolCacheItemDataById(selectItemId)
    if itemData == nil then
        itemData = { id = selectItemId, count = 0 }
    end

    if viewSelector.item.id == selectItemId then
        viewSelector.item:UpdateCountSimple(itemData.count)
    else
        viewSelector.item:InitItem(itemData, true)
    end
    local blocked = false
    if itemData.count > 0 then
        local success, data = Tables.factoryItemTable:TryGetValue(selectItemId)
        if success then
            blocked = itemData.count >= data.buildingBufferStackLimit
        end
    end
    viewSelector.item.view.count.color = blocked and self.view.config.CACHE_SLOT_BLOCKED_COUNT_COLOR or Color.white

    viewSelector.lineCell:ChangeLineColor(self.view.config.ACTIVE_SELECTOR_LINE_COLOR)
    viewSelector.stateController:SetState(SelectorState.Normal)
end

FacMixPoolCtrl._OnEnterPoolSelectMode = HL.Method(HL.Number) << function(self, selectorIndex)
    local currSelectorInfo = self.m_selectorConfig[selectorIndex]
    if currSelectorInfo == nil then
        return
    end

    if string.isEmpty(currSelectorInfo.compSelector.selectItemId) then
        AudioAdapter.PostEvent("Au_UI_Button_Common")
    else
        AudioAdapter.PostEvent("Au_UI_Button_Reset")
    end

    self.m_selectModeIndex = selectorIndex
    for index, selectorInfo in ipairs(self.m_selectorConfig) do
        for _, node in ipairs(selectorInfo.viewPortNodeList) do
            node.gameObject:SetActive(selectorIndex == index)
        end
    end

    local typeMatched = false
    for index = 1, self.m_buildingInfo.cache.size do
        local itemData = self.m_cacheItemDataList[index]
        typeMatched = currSelectorInfo.isFluid == FactoryUtils.isFactoryItemFluid(itemData.id) and not string.isEmpty(itemData.id)
        if typeMatched then
            break
        end
    end
    self.view.nopProductTips.gameObject:SetActive(not typeMatched)

    self.view.mainController:SetState(MainState.Select)
    self.view.mainAnimation:PlayWithTween(MAIN_SELECT_MODE_IN_ANIM_NAME[selectorIndex])

    self.view.selectModeNode.selector.normal.gameObject:SetActive(not currSelectorInfo.isFluid)
    self.view.selectModeNode.selector.fluid.gameObject:SetActive(currSelectorInfo.isFluid)
    self:_SetAndRefreshPoolSelectModeSelectorState(currSelectorInfo.compSelector.selectItemId)
    self:_RefreshPoolCacheSelectModeState(true, true)

    self.view.facCacheBelt:SetCacheBeltSingleState(true)
    self.view.facCachePipe:SetCachePipeSingleState(true)

    AudioAdapter.PostEvent("Au_UI_Popup_Common_Medium_Open")
    self.view.buildingCommon.view.controllerSideMenuBtn.gameObject:SetActive(false)

    self:_FindFittingCacheToNaviOnSwitchMode(true)

    self.m_isInSelectMode = true
end

FacMixPoolCtrl._OnLeavePoolSelectMode = HL.Method() << function(self)
    for _, selectorInfo in ipairs(self.m_selectorConfig) do
        for _, node in ipairs(selectorInfo.viewPortNodeList) do
            node.gameObject:SetActive(true)
        end
    end

    self.view.nopProductTips.gameObject:SetActive(false)

    self.view.mainAnimation:PlayWithTween(MAIN_SELECT_MODE_OUT_ANIM_NAME, function()
        self.view.mainController:SetState(MainState.Normal)
        CS.Beyond.Gameplay.Conditions.OnMixPoolSelectFinish.Trigger()
    end)
    self:_RefreshPoolCacheSelectModeState(false, true)

    self.view.facCacheBelt:SetCacheBeltSingleState(false)
    self.view.facCachePipe:SetCachePipeSingleState(false)
    self.view.facCachePipe:RefreshPipeCellsState()

    self:_RefreshPoolCacheArrowsRunningState()

    AudioAdapter.PostEvent("Au_UI_Popup_Common_Medium_Close")
    self.view.buildingCommon.view.controllerSideMenuBtn.gameObject:SetActive(true)

    self:_FindFittingCacheToNaviOnSwitchMode(false)

    self.m_isInSelectMode = false
end

FacMixPoolCtrl._RefreshPoolCacheSelectModeState = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isInSelectMode, forceRefresh)
    local currSelectorInfo = self.m_selectorConfig[self.m_selectModeIndex]
    if isInSelectMode and currSelectorInfo == nil then
        return
    end

    local isFluid = currSelectorInfo.isFluid
    for index = 1, self.m_buildingInfo.cache.size do
        local itemSlot = self:_GetPoolCacheItemSlotByIndex(index)
        if itemSlot ~= nil then
            if isInSelectMode then
                local itemData = self.m_cacheItemDataList[index]
                local typeMatched = isFluid == FactoryUtils.isFactoryItemFluid(itemData.id) and not string.isEmpty(itemData.id)
                itemSlot.selectModeBg.gameObject:SetActive(typeMatched)

                if DeviceInfo.usingController then
                    local diffItem = self.m_selectModeItemId ~= itemData.id
                    if typeMatched and diffItem then
                        itemSlot.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.PressConfirmTriggerOnClick)
                        local bindingText = string.isEmpty(self.m_selectModeItemId) and Language["key_hint_common_select"] or Language.LUA_MIXPOOL_ACTION_CONFIRM_SWITCH
                        InputManagerInst:SetBindingText(itemSlot.button.hoverConfirmBindingId, bindingText)
                    else
                        itemSlot.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
                    end
                else
                    itemSlot.button.enabled = typeMatched
                end

                itemSlot.canvasGroup.color = typeMatched and
                    self.view.config.NORMAL_SLOT_COLOR or
                    self.view.config.SELECT_INVALID_COLOR

                if forceRefresh then
                    itemSlot.highlightBg.gameObject:SetActive(
                        not string.isEmpty(itemData.id) and
                            itemData.id == currSelectorInfo.compSelector.selectItemId
                    )
                end
            else
                itemSlot.selectModeBg.gameObject:SetActive(false)
                itemSlot.highlightBg.gameObject:SetActive(false)

                if DeviceInfo.usingController then
                    itemSlot.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
                else
                    itemSlot.button.enabled = true
                end

                itemSlot.canvasGroup.color = self.view.config.NORMAL_SLOT_COLOR

                if forceRefresh then
                    itemSlot.highlightBg.gameObject:SetActive(false)
                end
            end
        end
    end
end

FacMixPoolCtrl._SetAndRefreshPoolSelectModeSelectorState = HL.Method(HL.String) << function(self, selectItemId)
    local currSelectorInfo = self.m_selectorConfig[self.m_selectModeIndex]
    if currSelectorInfo == nil then
        return
    end

    self.m_selectModeItemId = selectItemId
    local selectModeNode = self.view.selectModeNode

    self:_RefreshPoolCacheSlotHighlightState()

    local item = currSelectorInfo.isFluid and selectModeNode.selector.fluidItem or selectModeNode.selector.normalItem
    selectModeNode.selector.fluidItem.gameObject:SetActive(currSelectorInfo.isFluid)
    selectModeNode.selector.normalItem.gameObject:SetActive(not currSelectorInfo.isFluid)
    if string.isEmpty(selectItemId) then
        item.gameObject:SetActive(false)
        selectModeNode.info.gameObject:SetActive(false)
        selectModeNode.empty.gameObject:SetActive(true)
        return
    end

    item:InitItem({ id = selectItemId, count = 1 }, false)

    local success, itemData = Tables.itemTable:TryGetValue(selectItemId)
    if success then
        selectModeNode.nameText.text = itemData.name
        selectModeNode.descText:SetAndResolveTextStyle(itemData.desc)
    end

    item.gameObject:SetActive(true)
    selectModeNode.info.gameObject:SetActive(true)
    selectModeNode.empty.gameObject:SetActive(false)
end

FacMixPoolCtrl._RefreshPoolCacheSlotHighlightState = HL.Method() << function(self)
    if not self.m_isInSelectMode then
        return
    end

    local selectItemId = self.m_selectModeItemId
    for index = 1, self.m_buildingInfo.cache.size do
        local slot = self:_GetPoolCacheItemSlotByIndex(index)
        local data = self.m_cacheItemDataList[index]
        slot.highlightBg.gameObject:SetActive(data.id == selectItemId and not string.isEmpty(selectItemId))
    end
end






FacMixPoolCtrl.m_disableNaviCache = HL.Field(HL.Boolean) << false

FacMixPoolCtrl.m_showItemTipsBindingId = HL.Field(HL.Number) << -1

FacMixPoolCtrl.m_naviCacheSlotIndex = HL.Field(HL.Number) << -1

FacMixPoolCtrl.m_naviSelectorIndex = HL.Field(HL.Number) << -1

FacMixPoolCtrl._InitMixPoolController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end


    if self.m_isExpansionPool then
        local moveDownSlot = self:_GetPoolCacheItemSlotByIndex(BUILDINGCOMMON_MOVE_BTN_NAVI_DOWN)
        local delDownSlot = self:_GetPoolCacheItemSlotByIndex(BUILDINGCOMMON_DEL_BTN_NAVI_DOWN)
        self.view.buildingCommon.view.moveButton:SetExplicitSelectOnDown(moveDownSlot.button)
        self.view.buildingCommon.view.delButton:SetExplicitSelectOnDown(delDownSlot.button)
    else
        local downSlot = self:_GetPoolCacheItemSlotByIndex(BUILDINGCOMMON_MOVE_BTN_NAVI_DOWN)
        self.view.buildingCommon.view.moveButton:SetExplicitSelectOnDown(downSlot.button)
        self.view.buildingCommon.view.delButton:SetExplicitSelectOnDown(downSlot.button)
    end
    self.view.buildingCommon.view.moveButton.onIsNaviTargetChanged = function(isNaviTarget)
        if isNaviTarget then
            self.m_naviCacheSlotIndex = -1
            self.m_naviSelectorIndex = -1
        end
    end
    self.view.buildingCommon.view.delButton.onIsNaviTargetChanged = function(isNaviTarget)
        if isNaviTarget then
            self.m_naviCacheSlotIndex = -1
            self.m_naviSelectorIndex = -1
        end
    end

    local targetMap = self.m_isExpansionPool and EXPANSION_SELECTOR_LEFT_CACHE_TARGET_MAP or SELECTOR_LEFT_CACHE_TARGET_MAP
    for selectorIndex = 1, #self.m_selectorConfig do
        local viewSelector = self.m_selectorConfig[selectorIndex].viewSelector
        local targetCacheIndex = targetMap[selectorIndex]
        local targetCacheSlot = self:_GetPoolCacheItemSlotByIndex(targetCacheIndex)
        viewSelector.selectButton:SetExplicitSelectOnLeft(targetCacheSlot.button)
        viewSelector.selectButton.onIsNaviTargetChanged = function(isNaviTarget)
            if isNaviTarget then
                self.m_naviCacheSlotIndex = -1
                self.m_naviSelectorIndex = selectorIndex
            end
            viewSelector.keyHint.gameObject:SetActive(isNaviTarget)
        end
        viewSelector.keyHint.gameObject:SetActive(false)
    end

    local findNavi = DEFAULT_SELECTOR_NAVI_INDEX_PRIORITY[1]
    for _, selectorIndex in ipairs(DEFAULT_SELECTOR_NAVI_INDEX_PRIORITY) do
        local compSelector = self.m_selectorConfig[selectorIndex].compSelector
        local selectItemId = compSelector.selectItemId
        if not string.isEmpty(selectItemId) then
            findNavi = selectorIndex
            break
        end
    end
    if self.m_selectorConfig[findNavi] then
        self:SetNaviTarget(self.m_selectorConfig[findNavi].viewSelector.selectButton)
    end

    for cacheIndex = 1, self:_GetPoolMaxSlotCount() do
        local cacheSlot = self:_GetPoolCacheItemSlotByIndex(cacheIndex)
        cacheSlot.button.onIsNaviTargetChanged = function(isNaviTarget)
            cacheSlot.controllerLight.gameObject:SetActive(isNaviTarget)
            if isNaviTarget then
                self.m_naviCacheSlotIndex = cacheIndex
                self.m_naviSelectorIndex = -1
            end
        end
    end

    self.m_showItemTipsBindingId = self:BindInputPlayerAction("show_item_tips", function()
        if self.m_naviCacheSlotIndex > 0 then
            local itemSlot = self:_GetPoolCacheItemSlotByIndex(self.m_naviCacheSlotIndex)
            itemSlot.item:ShowTips()
        end
        if self.m_naviSelectorIndex > 0 then
            local selectorInfo = self.m_selectorConfig[self.m_naviSelectorIndex]
            selectorInfo.viewSelector.item:ShowTips()
        end
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
    end)
end

FacMixPoolCtrl._RefreshShowItemTipsBindingState = HL.Method() << function(self)
    local itemEmpty = true
    if self.m_naviCacheSlotIndex > 0 then
        local itemData = self.m_cacheItemDataList[self.m_naviCacheSlotIndex]
        itemEmpty = itemData == nil or string.isEmpty(itemData.id)
    end
    if self.m_naviSelectorIndex > 0 then
        local selectorInfo = self.m_selectorConfig[self.m_naviSelectorIndex]
        itemEmpty = string.isEmpty(selectorInfo.compSelector.selectItemId)
    end
    InputManagerInst:ToggleBinding(self.m_showItemTipsBindingId, not self.m_isInSelectMode and not itemEmpty)
end

FacMixPoolCtrl._OnActionNaviToTarget = HL.Method(HL.Any) << function(self, args)
    local targetIndex = unpack(args)
    if self.m_disableNaviCache or targetIndex < 0 or targetIndex > self:_GetPoolMaxSlotCount() then
        return
    end

    if self.m_naviCacheSlotIndex == 0 and targetIndex ~= 0 then
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.selectorNode.naviGroup)
        local itemSlot = self:_GetPoolCacheItemSlotByIndex(targetIndex)
        itemSlot.controllerLight.gameObject:SetActive(true)
        self.m_naviCacheSlotIndex = targetIndex
        return
    end

    local oldTarget = self:_GetPoolCacheItemSlotByIndex(self.m_naviCacheSlotIndex)
    oldTarget.controllerLight.gameObject:SetActive(false)
    if targetIndex ~= 0 then
        local newTarget = self:_GetPoolCacheItemSlotByIndex(targetIndex)
        newTarget.controllerLight.gameObject:SetActive(true)
    end

    self.m_naviCacheSlotIndex = targetIndex
end

FacMixPoolCtrl._FindFittingCacheToNaviOnSwitchMode = HL.Method(HL.Boolean) << function(self, enterSelectMode)
    if not DeviceInfo.usingController then
        return
    end

    local currSelectorInfo = self.m_selectorConfig[self.m_selectModeIndex]
    if currSelectorInfo == nil then
        return
    end

    local findNaviPriority = self.m_isExpansionPool and EXPANSION_FIND_DEFAULT_CACHE_NAVI_TARGET_PRIORITY or FIND_DEFAULT_CACHE_NAVI_TARGET_PRIORITY
    if enterSelectMode then
        local isFluid = currSelectorInfo.isFluid
        for _, index in ipairs(findNaviPriority) do
            local itemSlot = self:_GetPoolCacheItemSlotByIndex(index)
            if itemSlot ~= nil then
                local itemData = self.m_cacheItemDataList[index]
                local typeMatched = isFluid == FactoryUtils.isFactoryItemFluid(itemData.id) and not string.isEmpty(itemData.id)
                if typeMatched then
                    InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.selectorNode.naviGroup)
                    self:SetNaviTarget(itemSlot.button)
                    return
                end
            end
        end

        UIUtils.changeAndTrySetNaviBindingType(self.view.selectorNode.naviGroup, CS.UnityEngine.UI.NavigationBindingType.InValid)
        self.m_disableNaviCache = true
    else
        self:SetNaviTarget(currSelectorInfo.viewSelector.selectButton)
        UIUtils.changeAndTrySetNaviBindingType(self.view.selectorNode.naviGroup, CS.UnityEngine.UI.NavigationBindingType.AllDirections)
        self.m_disableNaviCache = false
    end
end

FacMixPoolCtrl._RefreshPoolSelectorButtonController = HL.Method(HL.Number) << function(self, selectorIndex)
    local selectorInfo = self.m_selectorConfig[selectorIndex]
    if selectorInfo == nil then
        return
    end

    local viewSelector, compSelector = selectorInfo.viewSelector, selectorInfo.compSelector
    local selectItemId = compSelector.selectItemId
    if string.isEmpty(selectItemId) then
        InputManagerInst:ToggleBinding(selectorInfo.showTipsBindingId, false)
        InputManagerInst:SetBindingText(viewSelector.selectButton.hoverConfirmBindingId, Language.LUA_MIXPOOL_ACTION_SELECT_PRODUCT)
    else
        InputManagerInst:ToggleBinding(selectorInfo.showTipsBindingId, true)
        InputManagerInst:SetBindingText(viewSelector.selectButton.hoverConfirmBindingId, Language.LUA_MIXPOOL_ACTION_SWITCH_PRODUCT)
    end
end




HL.Commit(FacMixPoolCtrl)
