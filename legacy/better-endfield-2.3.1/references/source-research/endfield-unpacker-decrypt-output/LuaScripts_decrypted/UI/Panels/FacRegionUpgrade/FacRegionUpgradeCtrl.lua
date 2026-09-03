local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacRegionUpgrade
local PHASE_ID = PhaseId.FacRegionUpgrade
local RegionBoundEffectType = {
    Left = 1,
    Right = 2,
    Bottom = 3,
    Top = 4,
}
FacRegionUpgradeCtrl = HL.Class('FacRegionUpgradeCtrl', uiCtrl.UICtrl)

local WALLET_ICON_NAME_FORMAT = "%s_black"





FacRegionUpgradeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_WALLET_CHANGED] = '_OnWalletChanged',
    [MessageConst.ON_FAC_REGION_UPGRADE_DATA_CHANGED] = '_OnUpgradeDataChanged',
    [MessageConst.FAC_ON_UNLOCK_TECH_TREE_UI] = '_OnTechTreeStateChanged',
}

FacRegionUpgradeCtrl.m_levelId = HL.Field(HL.String) << ""

FacRegionUpgradeCtrl.m_isLoadFinished = HL.Field(HL.Boolean) << false

FacRegionUpgradeCtrl.m_purchaseEnabled = HL.Field(HL.Boolean) << true

FacRegionUpgradeCtrl.m_regionIndex = HL.Field(HL.Number) << -1

FacRegionUpgradeCtrl.m_selectItemId = HL.Field(HL.String) << ""

FacRegionUpgradeCtrl.m_purchaseItemId = HL.Field(HL.String) << ""

FacRegionUpgradeCtrl.m_regionItemDataList = HL.Field(HL.Table)

FacRegionUpgradeCtrl.m_regionItemCells = HL.Field(HL.Forward('UIListCache'))

FacRegionUpgradeCtrl.m_busItemDataList = HL.Field(HL.Table)

FacRegionUpgradeCtrl.m_busItemCells = HL.Field(HL.Forward('UIListCache'))

FacRegionUpgradeCtrl.m_itemDataGetter = HL.Field(HL.Table)

FacRegionUpgradeCtrl.m_regionSceneDataList = HL.Field(HL.Table)

FacRegionUpgradeCtrl.m_currencyItemId = HL.Field(HL.String) << ""

FacRegionUpgradeCtrl.m_currencyItemIconId = HL.Field(HL.String) << ""

FacRegionUpgradeCtrl.m_currencyItemSprite = HL.Field(HL.Userdata)

FacRegionUpgradeCtrl.m_currencyItemName = HL.Field(HL.String) << ""

FacRegionUpgradeCtrl.m_currencyCount = HL.Field(HL.Number) << -1

FacRegionUpgradeCtrl.m_isAllPurchased = HL.Field(HL.Boolean) << false

FacRegionUpgradeCtrl.m_actionDescCells = HL.Field(HL.Forward('UIListCache'))

FacRegionUpgradeCtrl.m_actionBusFreeCells = HL.Field(HL.Forward('UIListCache'))

FacRegionUpgradeCtrl.m_conditionDescCells = HL.Field(HL.Forward('UIListCache'))

FacRegionUpgradeCtrl.m_regionState = HL.Field(HL.Userdata)

FacRegionUpgradeCtrl.m_regionEffectDataGetter = HL.Field(HL.Table)

FacRegionUpgradeCtrl.m_regionEffectInitialized = HL.Field(HL.Boolean) << false

FacRegionUpgradeCtrl.m_busEffectDataGetter = HL.Field(HL.Table)

FacRegionUpgradeCtrl.m_busEffectInitialized = HL.Field(HL.Boolean) << false

FacRegionUpgradeCtrl.m_purchasedAnimTimer = HL.Field(HL.Number) << -1


FacRegionUpgradeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_levelId = arg.levelId
    self.m_regionIndex = arg.regionIndex
    self.m_purchaseEnabled = true
    self.m_isLoadFinished = false

    self.view.btnClose.onClick:AddListener(function()

        if not self.m_isLoadFinished then
            return
        end
        if not PhaseManager:CanPopPhase(PHASE_ID) then
            return
        end
        self.view.luaPanel:BlockAllInput()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.btnNode.purchaseBtn.onClick:AddListener(function()
        self:_OnPurchaseSelectedItem()
    end)

    self:_InitCurrencyItem()
    self:_InitItemDetailList()
    self:_InitRegionSceneData()


    local recoverState = arg and arg.recoverState
    self:_TryRecoverState(recoverState)
end

FacRegionUpgradeCtrl.OnClose = HL.Override() << function(self)
    self:_ClearPurchasedAnimTimer()
end

FacRegionUpgradeCtrl.OnLoadFinished = HL.Method() << function(self)
    self.m_isLoadFinished = true
    self:_InitRegionUpgradeController()
end



FacRegionUpgradeCtrl._InitCurrencyItem = HL.Method() << function(self)
    local levelSuccess, levelConfig = DataManager.levelBasicInfoTable:TryGetValue(self.m_levelId)
    if not levelSuccess then
        return
    end

    local domainId = levelConfig.domainName
    local domainSuccess, domainTableData = Tables.domainDataTable:TryGetValue(domainId)
    if not domainSuccess then
        return
    end

    self.m_currencyItemId = domainTableData.domainGoldItemId
    local itemData = Tables.itemTable:GetValue(self.m_currencyItemId)
    self.m_currencyItemName = itemData.name
    self.m_currencyItemIconId = itemData.iconId
    self.m_currencyItemSprite = self:LoadSprite(UIConst.UI_SPRITE_WALLET, string.format(WALLET_ICON_NAME_FORMAT, self.m_currencyItemIconId))

    self:_UpdateCurrencyCount()

    self.view.walletBarPlaceholder:InitWalletBarPlaceholder({ self.m_currencyItemId })
end

FacRegionUpgradeCtrl._InitItemDetailList = HL.Method() << function(self)
    self.m_regionItemCells = UIUtils.genCellCache(self.view.regionItemCell)
    self.m_busItemCells = UIUtils.genCellCache(self.view.busItemCell)
    self.m_actionDescCells = UIUtils.genCellCache(self.view.actionDescCell)
    self.m_actionBusFreeCells = UIUtils.genCellCache(self.view.buildCell)
    self.m_conditionDescCells = UIUtils.genCellCache(self.view.conditionDescCell)

    self.view.leftNode.gameObject:SetActive(false)

    self:_UpdateAndRefreshAllUpgradeItems()
    self:_RefreshBtnNodeState()
    self:_RefreshBusNodeState()
end

FacRegionUpgradeCtrl._TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState and not string.isEmpty(recoverState.selectedItemId) then
        self:_StartCoroutine(function()
            coroutine.step()

            local itemData = self.m_itemDataGetter[recoverState.selectedItemId]
            if itemData ~= nil and not itemData.isPurchased then
                self:_SelectUpgradeItemCell(recoverState.selectedItemId)
            end
        end)
    end
end






FacRegionUpgradeCtrl._OnWalletChanged = HL.Method(HL.Any) << function(self, eventArgs)
    local changeId = unpack(eventArgs)
    if changeId ~= self.m_currencyItemId then
        return
    end

    self:_UpdateCurrencyCount()
    self:_RefreshAllItemCellsCurrencyTextColor()
    self:_RefreshBtnNodeState()
end

FacRegionUpgradeCtrl._OnPurchaseSelectedItem = HL.Method() << function(self)
    if string.isEmpty(self.m_selectItemId) or not self.m_purchaseEnabled then
        return
    end
    self.m_purchaseItemId = self.m_selectItemId
    self:_OnPurchaseSelectItemBefore()
    GameInstance.player.remoteFactory.panelStore:SendBuyGood(self.m_selectItemId)
end

FacRegionUpgradeCtrl._OnTechTreeStateChanged = HL.Method(HL.Table) << function(self, args)
    self:_RefreshBusNodeState()

    if not DeviceInfo.usingController then
        self:_SelectUpgradeItemCell()
    end
end

FacRegionUpgradeCtrl._OnUpgradeDataChanged = HL.Method() << function(self)
    local purchasedCountGetFunc = function()
        local purchasedCount = 0
        for _, data in pairs(self.m_itemDataGetter) do
            if data.isPurchased then
                purchasedCount = purchasedCount + 1
            end
        end
        return purchasedCount
    end

    local lastPurchasedCount = purchasedCountGetFunc()
    self:_UpdateAndRefreshAllUpgradeItems()
    local currPurchasedCount = purchasedCountGetFunc()

    if currPurchasedCount > lastPurchasedCount then
        local data = self.m_itemDataGetter[self.m_purchaseItemId]
        local busFreeActionList = {}
        local regionId, index
        for i = 0, data.actions.Count - 1 do
            local action = data.actions[i]
            if action == GEnums.GameActionEnum.FactoryAddBusFreeLimitCnt then
                local params = data.actionParamsList[i].actionParams
                if not regionId then
                    regionId = params[0]
                    index = tonumber(params[2])
                end
                table.insert(busFreeActionList, {
                    buildingId = params[1],
                    count = tonumber(params[3]),
                })
            end
        end
        if #busFreeActionList > 0 then
            local oldData = FactoryUtils.getFreeBusLimitsInfo(regionId, index)
            UIManager:AutoOpen(PanelId.FacHongsBusUpgradePopup, {
                popItemList = busFreeActionList,
                busFreeData = oldData,
                onClose = function()
                    self:_OnPurchaseSelectItemAfter(function()
                        self:_ResetRegionEffects()
                        self:_ResetBusEffects()
                        self:_FindNextItemCellToSelect()
                    end)
                end,
            })
        else
            self:_OnPurchaseSelectItemAfter(function()
                self:_ResetRegionEffects()
                self:_ResetBusEffects()
                self:_FindNextItemCellToSelect()
            end)
        end
    end
end






FacRegionUpgradeCtrl._GetConditionCompleted = HL.Method(GEnums.ConditionType, HL.Table).Return(HL.Boolean) << function(self, condition, args)
    if condition == GEnums.ConditionType.CheckFacPanelStoreGoodDone then
        local itemData = self.m_itemDataGetter[args[1]]
        return itemData.isPurchased
    elseif condition == GEnums.ConditionType.CheckUnlockTech then
        local techNodeId = args[1]
        return not GameInstance.player.facTechTreeSystem:NodeIsLocked(techNodeId)
    end

    return false
end

FacRegionUpgradeCtrl._GetConditionDescText = HL.Method(GEnums.ConditionType, HL.Table).Return(HL.String) << function(self, condition, args)
    local success, conditionDescData = Tables.facPanelStoreConditionDescTable:TryGetValue(condition)
    if not success then
        return ""
    end

    if condition == GEnums.ConditionType.CheckFacPanelStoreGoodDone then
        local itemData = self.m_itemDataGetter[args[1]]
        return string.format(conditionDescData.conditionDesc, itemData.name)
    elseif condition == GEnums.ConditionType.CheckUnlockTech then
        local techNodeId = args[1]
        local techNodeData = Tables.facSTTNodeTable:GetValue(techNodeId)
        return string.format(conditionDescData.conditionDesc, techNodeData.name)
    end

    return ""
end

FacRegionUpgradeCtrl._GetItemDataListByType = HL.Method(GEnums.FacPanelStoreGoodType).Return(HL.Table) << function(self, type)
    if type == GEnums.FacPanelStoreGoodType.RegionLevelUp then
        return self.m_regionItemDataList
    elseif type == GEnums.FacPanelStoreGoodType.BusPlace then
        return self.m_busItemDataList
    end
    return nil
end

FacRegionUpgradeCtrl._GetItemSelectCallbackByType = HL.Method(HL.String, GEnums.FacPanelStoreGoodType).Return(HL.Function) << function(self, id, type)
    if type == GEnums.FacPanelStoreGoodType.RegionLevelUp then
        return function(selected)
            self:_OnRegionLevelUpItemSelectedStateChange(id, selected)
        end
    elseif type == GEnums.FacPanelStoreGoodType.BusPlace then
        return function(selected)
            self:_OnBusPlaceItemSelectedStateChange(id, selected)
        end
    end
    return nil
end






FacRegionUpgradeCtrl._UpdateCurrencyCount = HL.Method() << function(self)
    self.m_currencyCount = GameInstance.player.inventory:GetItemCount(Utils.getCurrentScope(), Utils.getCurrentChapterId(), self.m_currencyItemId)
end

FacRegionUpgradeCtrl._UpdateAndRefreshAllUpgradeItems = HL.Method() << function(self)
    self:_UpdateUpgradeItemDataList()
    self:_RefreshUpgradeItemCells()
    self:_RefreshAllItemCellsCurrencyTextColor()
end

FacRegionUpgradeCtrl._UpdateUpgradeItemDataList = HL.Method() << function(self)
    local system = GameInstance.player.remoteFactory.panelStore
    local csRegionData = system:GetPanelStore(self.m_levelId, self.m_regionIndex)
    if csRegionData == nil then
        return
    end

    self.m_regionItemDataList = {}
    self.m_busItemDataList = {}
    self.m_itemDataGetter = {}

    self.m_isAllPurchased = true
    for id, csItemData in cs_pairs(csRegionData.goods) do
        local success, tableItemData = Tables.factoryPanelStoreTable:TryGetValue(id)
        if success then
            local dataList = self:_GetItemDataListByType(tableItemData.goodType)
            local isPurchased = csItemData.state == GEnums.FCPanelStoreGoodState.Done
            local data = {
                id = id,
                type = tableItemData.goodType,
                name = tableItemData.name,
                purchaseCost = tableItemData.cost,
                state = csItemData.state,
                sortId = tableItemData.sortId,
                regionId = tableItemData.regionId,
                busFreeShowCounts = tableItemData.busFreeShowCounts,
                actions = tableItemData.actions,
                actionParamsList = tableItemData.actionParamsList,
                conditions = tableItemData.conditions,
                conditionParamsList = tableItemData.conditionParamsList,
                isPurchased = isPurchased,
                isPurchasedSort = isPurchased and 1 or 0,
                onSelectCallback = self:_GetItemSelectCallbackByType(id, tableItemData.goodType),
            }
            table.insert(dataList, data)
            self.m_itemDataGetter[id] = data

            if not isPurchased then
                self.m_isAllPurchased = false
            end
        end
    end

    local sortFunc = Utils.genSortFunction({ "isPurchasedSort", "sortId" }, true)
    table.sort(self.m_regionItemDataList, sortFunc)
    table.sort(self.m_busItemDataList, sortFunc)

    self:_UpdateRegionItemDataList()
    self:_UpdateBusItemDataList()
end

FacRegionUpgradeCtrl._UpdateRegionItemDataList = HL.Method() << function(self)
    for _, data in ipairs(self.m_regionItemDataList) do
        for actionIndex = 0, data.actions.Count - 1 do
            local action = data.actions[actionIndex]
            if action == GEnums.GameActionEnum.FactoryRegionLevelUp then
                data.upgradeLevel = tonumber(data.actionParamsList[actionIndex].actionParams[1])
            end
        end
    end
end

FacRegionUpgradeCtrl._UpdateBusItemDataList = HL.Method() << function(self)
    for _, data in ipairs(self.m_busItemDataList) do
        for actionIndex = 0, data.actions.Count - 1 do
            local action = data.actions[actionIndex]
            if action == GEnums.GameActionEnum.FactoryBuildingPlaceFromPredefine then
                data.instKey = data.actionParamsList[actionIndex].actionParams[1]
            end
        end
    end
end

FacRegionUpgradeCtrl._RefreshUpgradeItemCells = HL.Method() << function(self)
    self.m_regionItemCells:Refresh(#self.m_regionItemDataList, function(cell, index)
        self:_RefreshUpgradeItemCell(cell, self.m_regionItemDataList[index])
    end)
    self.m_busItemCells:Refresh(#self.m_busItemDataList, function(cell, index)
        self:_RefreshUpgradeItemCell(cell, self.m_busItemDataList[index])
    end)
end

FacRegionUpgradeCtrl._RefreshUpgradeItemCell = HL.Method(HL.Table, HL.Table) << function(self, cell, data)
    cell.nameTxt.text = data.name
    cell.namePurchasedTxt.text = data.name
    cell.currencyIcon.sprite = self.m_currencyItemSprite
    cell.purchaseCostTxt.text = tostring(data.purchaseCost)

    if data.state == GEnums.FCPanelStoreGoodState.Ready then
        cell.itemStateController:SetState("Normal")
    elseif data.state == GEnums.FCPanelStoreGoodState.Done then
        cell.itemStateController:SetState("Purchased")
    elseif data.state == GEnums.FCPanelStoreGoodState.Lock then
        cell.itemStateController:SetState("Locked")
    end

    cell.selectStateController:SetState("Unselected")
    cell.animationWrapper:PlayWithTween("upgrade_cell_default")

    cell.button.onClick:RemoveAllListeners()
    if not data.isPurchased then
        cell.button.onClick:AddListener(function()
            if self.m_selectItemId == data.id then
                return
            end
            self:_SelectUpgradeItemCell(data.id)
        end)
        cell.button.enabled = true
    else
        cell.button.enabled = false
    end

    cell.gameObject.name = data.id
    data.cell = cell
end

FacRegionUpgradeCtrl._RefreshAllItemCellsCurrencyTextColor = HL.Method() << function(self)
    for _, data in pairs(self.m_itemDataGetter) do
        local cell = data.cell
        local purchaseValid = data.state == GEnums.FCPanelStoreGoodState.Ready and data.purchaseCost <= self.m_currencyCount
        cell.purchaseCostTxt.color = purchaseValid and
            self.view.config.PURCHASE_COST_NORMAL_COLOR or
            self.view.config.PURCHASE_COST_NOT_ENOUGH_COLOR
    end
end

FacRegionUpgradeCtrl._RefreshActionAndConditionInfo = HL.Method() << function(self)
    self.view.leftNode.gameObject:SetActive(false)

    if string.isEmpty(self.m_selectItemId) then
        return
    end

    local data = self.m_itemDataGetter[self.m_selectItemId]
    if data.actions.Count <= 0 then
        return
    end

    local busFreeActionList = {}
    local normalActionList = {}
    local busFreeShowIndex = 0
    for i = 0, data.actions.Count - 1 do
        local action = data.actions[i]
        if action == GEnums.GameActionEnum.FactoryAddBusFreeLimitCnt then
            local params = data.actionParamsList[i].actionParams
            table.insert(busFreeActionList, {
                regionId = params[0],
                buildingId = params[1],
                index = tonumber(params[2]),
                count = tonumber(params[3]),
            })
            if busFreeShowIndex < data.busFreeShowCounts.Count then
                busFreeActionList[LuaIndex(busFreeShowIndex)].showCount = data.busFreeShowCounts[busFreeShowIndex]
            end
            busFreeShowIndex = busFreeShowIndex + 1
        else
            table.insert(normalActionList, action)
        end
    end

    self.m_actionDescCells:Refresh(#normalActionList, function(cell, index)
        local action = normalActionList[index]
        local success, actionDescData = Tables.facPanelStoreActionDescTable:TryGetValue(action)
        if success then
            cell.descTxt.text = actionDescData.actionDesc
        end
        cell.lineImage.gameObject:SetActive(index > 1)
    end)
    self.m_actionBusFreeCells:Refresh(#busFreeActionList, function(cell, index)
        local busInfo = busFreeActionList[index]
        local showCount = busInfo.showCount or 0
        cell.stateController:SetState(showCount == 0 and "unlock" or "uplevel")
        cell.oldTxt.text = showCount
        cell.newText.text = showCount + busInfo.count
        cell.iconImg:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_IMAGE, string.format("image_%s", busInfo.buildingId))
        local success, buildingData = Tables.factoryBuildingTable:TryGetValue(busInfo.buildingId)
        if success then
            cell.contentText.text = buildingData.name
        end
    end)

    if data.state == GEnums.FCPanelStoreGoodState.Lock then
        self.m_conditionDescCells:Refresh(data.conditions.Count, function(cell, index)
            local condition = data.conditions[CSIndex(index)]
            local conditionParams = data.conditionParamsList[CSIndex(index)].conditionParams
            local conditionArgs = {}
            for paramIndex = 1, conditionParams.Count do
                table.insert(conditionArgs, conditionParams[CSIndex(paramIndex)])
            end
            local conditionCompleted = self:_GetConditionCompleted(condition, conditionArgs)
            cell.stateController:SetState(conditionCompleted and "Completed" or "Uncompleted")
            cell.descTxt.text = self:_GetConditionDescText(condition, conditionArgs)
        end)
        self.view.conditionNode.gameObject:SetActive(true)
    else
        self.view.conditionNode.gameObject:SetActive(false)
    end

    self.view.leftNode.gameObject:SetActive(true)
end

FacRegionUpgradeCtrl._RefreshBtnNodeState = HL.Method() << function(self)
    local btnNode = self.view.btnNode
    if self.m_isAllPurchased then
        btnNode.stateController:SetState("AllPurchased")
        return
    end

    if string.isEmpty(self.m_selectItemId) then
        btnNode.stateController:SetState("Unselected")
        return
    end

    local data = self.m_itemDataGetter[self.m_selectItemId]
    if data.state == GEnums.FCPanelStoreGoodState.Lock then
        btnNode.stateController:SetState("Locked")
        return
    end

    if self.m_currencyCount >= data.purchaseCost then
        btnNode.stateController:SetState("Normal")
    else
        btnNode.stateController:SetState("NotEnoughCurrency")
        btnNode.currencyTxt:SetAndResolveTextStyle(string.format(
            Language.LUA_FAC_UPGRADE_CURRENCY_NOT_ENOUGH_HINT_TEXT,
            self.m_currencyItemIconId,
            self.m_currencyItemName))
    end
end

FacRegionUpgradeCtrl._RefreshBusNodeState = HL.Method() << function(self)
    local isFacTechTreeUnlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacTechTree)
    if not isFacTechTreeUnlocked then
        self.view.busStateController:SetState("TechTreeLocked")
        return
    end

    local techTreeNodeId = FacConst.FAC_BUS_TECH_TREE_NODE_IDS[Utils.getCurDomainId()]
    if GameInstance.player.facTechTreeSystem:NodeIsLocked(techTreeNodeId) then
        self.view.busStateController:SetState("TechTreeNodeLocked")
        local nodeData = Tables.facSTTNodeTable:GetValue(techTreeNodeId)
        self.view.techTreeUnlockedTxt.text = string.format(Language.LUA_GOTO_BUS_TECH_TREE_NODE, nodeData.name)
        self.view.techTreeBtn.onClick:AddListener(function()
            PhaseManager:OpenPhase(PhaseId.FacTechTree, { techId = techTreeNodeId })
        end)
        return
    end

    self.view.busStateController:SetState("Normal")
end






FacRegionUpgradeCtrl._SelectUpgradeItemCell = HL.Method(HL.Opt(HL.String)) << function(self, selectItemId)
    selectItemId = selectItemId or ""

    local lastItemData = self.m_itemDataGetter[self.m_selectItemId]
    if lastItemData ~= nil then
        lastItemData.cell.selectStateController:SetState("Unselected")
        lastItemData.cell.animationWrapper:PlayWithTween("upgrade_cell_default")
        lastItemData.onSelectCallback(false)
    end

    local currItemData = self.m_itemDataGetter[selectItemId]
    if currItemData ~= nil then
        currItemData.cell.selectStateController:SetState("Selected")
        currItemData.cell.animationWrapper:PlayWithTween("upgrade_cell_selected")
        currItemData.onSelectCallback(true)
    end

    self.m_selectItemId = selectItemId

    self:_RefreshBtnNodeState()
    self:_RefreshActionAndConditionInfo()
    if string.isEmpty(self.m_selectItemId) then
        self:BlendOutCameraFromSelectItemTarget(false)

        if self.naviGroup.IsTopLayer then
            self:SetNaviTarget(nil)
        end
    else
        self:BlendInCameraToSelectItemTarget()
    end
end

FacRegionUpgradeCtrl._FindNextItemCellToSelect = HL.Method() << function(self)
    if string.isEmpty(self.m_selectItemId) then
        return
    end

    local findTargetAndSelect = function(dataList)
        for _, data in ipairs(dataList) do
            if not data.isPurchased then
                if DeviceInfo.usingController then
                    self:SetNaviTarget(data.cell.button)
                else
                    self:_SelectUpgradeItemCell(data.id)
                end
                return true
            end
        end
        return false
    end


    local lastItemData = self.m_itemDataGetter[self.m_selectItemId]
    if lastItemData ~= nil then
        local dataList = self:_GetItemDataListByType(lastItemData.type)
        if findTargetAndSelect(dataList) then
            return
        end
    end


    if findTargetAndSelect(self.m_regionItemDataList) then
        return
    end
    if findTargetAndSelect(self.m_busItemDataList) then
        return
    end


    self:_SelectUpgradeItemCell()
end

FacRegionUpgradeCtrl._OnRegionLevelUpItemSelectedStateChange = HL.Method(HL.String, HL.Boolean) << function(self, id, selected)
    local data = self.m_itemDataGetter[id]
    if data == nil or data.upgradeLevel == nil then
        return
    end

    if self.m_regionEffectDataGetter ~= nil and self.m_regionEffectDataGetter[data.upgradeLevel] ~= nil then
        local effectData = self.m_regionEffectDataGetter[data.upgradeLevel]
        effectData.effectController:SetSelectState(selected)
    end
end

FacRegionUpgradeCtrl._OnBusPlaceItemSelectedStateChange = HL.Method(HL.String, HL.Boolean) << function(self, id, selected)
    local data = self.m_itemDataGetter[id]
    if data == nil or data.instKey == nil then
        return
    end

    if self.m_busEffectDataGetter ~= nil and self.m_busEffectDataGetter[data.instKey] ~= nil then
        local effectData = self.m_busEffectDataGetter[data.instKey]
        effectData.effectController:SetSelectState(selected)
    end
end

FacRegionUpgradeCtrl.BlendInCameraToSelectItemTarget = HL.Method() << function(self)
    local cameraConfig = DataManager.facRegionUpgradeCameraConfig
    local targetSuccess, targetData = cameraConfig.targetData:TryGetValue(self.m_selectItemId)
    if not targetSuccess then
        return
    end

    CameraUtils.DoCommonTempBlendIn(
        targetData.targetPosition,
        targetData.targetRotation,
        targetData.targetFOV,
        targetData.blendData.blendTime,
        targetData.blendData.blendStyle,
        targetData.blendData.blendCurve,
        false, false
    )
end

FacRegionUpgradeCtrl.BlendOutCameraFromSelectItemTarget = HL.Method(HL.Boolean) << function(self, fastMode)
    local blendTime = fastMode and 0 or DataManager.facRegionUpgradeCameraConfig.exitTargetBlendData.blendTime
    CameraUtils.DoCommonTempBlendOut(
        blendTime,
        DataManager.facRegionUpgradeCameraConfig.exitTargetBlendData.blendStyle,
        DataManager.facRegionUpgradeCameraConfig.exitTargetBlendData.blendCurve
    )
end

FacRegionUpgradeCtrl.GetSelectItemId = HL.Method().Return(HL.String) << function(self)
    return self.m_selectItemId
end

FacRegionUpgradeCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    if string.isEmpty(self.m_selectItemId) then
        return nil
    end
    return {
        selectedItemId = self.m_selectItemId,
    }
end






FacRegionUpgradeCtrl._InitRegionSceneData = HL.Method() << function(self)
    local sceneInfo = GameInstance.remoteFactoryManager.system.core:GetSceneInfoByName(self.m_levelId)
    if sceneInfo == nil then
        return
    end

    local regionState = sceneInfo:GetCoreZoneState(self.m_regionIndex)
    if regionState == nil then
        return
    end

    local rangeList = CSFactoryUtil.GetSceneCoreZoneRangeList(self.m_levelId, self.m_regionIndex)
    local height = regionState.data.fenceHeight
    self.m_regionSceneDataList = {}
    for level = 1, regionState.data.maxMapLevel do
        local range = rangeList[CSIndex(level)]
        local rangeCube = range[CSIndex(1)]
        self.m_regionSceneDataList[level] = {
            leftBottom = Vector3(rangeCube.x, height, rangeCube.z),
            rightTop = Vector3(rangeCube.x + rangeCube.width, height, rangeCube.z + rangeCube.depth),
        }
    end
    self.m_regionState = regionState
end

FacRegionUpgradeCtrl.InitRegionEffects = HL.Method(HL.Table) << function(self, effectList)
    self.m_regionEffectDataGetter = {}
    if effectList == nil then
        return
    end

    for level, effect in ipairs(effectList) do
        local object = effect.effectObject
        local controller = object.transform:GetComponent("FacRegionUpgradeEffectController")
        local sceneData = self.m_regionSceneDataList[level]
        object.transform.position = (sceneData.leftBottom + sceneData.rightTop) / 2
        self.m_regionEffectDataGetter[level] = {
            effectObject = object,
            effectController = controller
        }
    end

    self:_ResetRegionEffects()

    self.m_regionEffectInitialized = true
    self:_OnRegionLevelUpItemSelectedStateChange(self.m_selectItemId, true)
end

FacRegionUpgradeCtrl.GetBusEffectInstKeyList = HL.Method().Return(HL.Table) << function(self)
    local instKeyList = {}
    for _, data in ipairs(self.m_busItemDataList) do
        for actionIndex = 0, data.actions.Count - 1 do
            local action = data.actions[actionIndex]
            if action == GEnums.GameActionEnum.FactoryBuildingPlaceFromPredefine then
                local instKey = data.actionParamsList[actionIndex].actionParams[1]
                table.insert(instKeyList, instKey)
            end
        end
    end
    return instKeyList
end

FacRegionUpgradeCtrl.InitBusEffects = HL.Method(HL.Table) << function(self, effectList)
    self.m_busEffectDataGetter = {}
    local sceneData = GameInstance.remoteFactoryManager.staticData:QuerySceneData(self.m_levelId)
    for instKey, effectData in pairs(effectList) do
        local busData = sceneData:QuerySceneBusData(instKey)
        local object = effectData.effectObject
        local controller = object.transform:GetComponent("FacRegionUpgradeEffectController")
        object.transform.position = busData.worldPosition
        object.transform.rotation = Quaternion.Euler(busData.worldRotation)
        self.m_busEffectDataGetter[instKey] = {
            effectObject = object,
            effectController = controller
        }
    end

    self:_ResetBusEffects()

    self.m_busEffectInitialized = true
    self:_OnBusPlaceItemSelectedStateChange(self.m_selectItemId, true)
end

FacRegionUpgradeCtrl._ResetRegionEffects = HL.Method() << function(self)
    local lastPurchasedLevel = 1
    for _, data in ipairs(self.m_regionItemDataList) do
        if data.upgradeLevel ~= nil and data.isPurchased then
            if lastPurchasedLevel <= data.upgradeLevel then
                lastPurchasedLevel = data.upgradeLevel
            end
        end
    end


    for level, effectData in ipairs(self.m_regionEffectDataGetter) do
        effectData.effectController:SetUpgradeState(level <= self.m_regionState.level)
        effectData.effectController:SetVisibleState(level >= lastPurchasedLevel)
    end
end

FacRegionUpgradeCtrl._ResetBusEffects = HL.Method() << function(self)
    for _, data in ipairs(self.m_busItemDataList) do
        if data.instKey ~= nil then
            local effectData = self.m_busEffectDataGetter[data.instKey]
            if effectData ~= nil then
                effectData.effectController:SetUpgradeState(data.isPurchased)
                effectData.effectController:SetVisibleState(not data.isPurchased)
            end
        end
    end
end






FacRegionUpgradeCtrl._OnPurchaseSelectItemBefore = HL.Method() << function(self)
    local itemData = self.m_itemDataGetter[self.m_purchaseItemId]
    if itemData == nil then
        return
    end

    self.m_purchaseEnabled = false

    UIUtils.PlayAnimationAndToggleActive(self.view.mainAnim, false, function()
        if DeviceInfo.usingController then
            self:SetNaviTarget(nil)
        end
    end)

    if itemData.type == GEnums.FacPanelStoreGoodType.RegionLevelUp then
        self:_OnPurchaseRegionItemBefore()
    elseif itemData.type == GEnums.FacPanelStoreGoodType.BusPlace then
        self:_OnPurchaseBusItemBefore()
    end

    self.view.luaPanel:BlockAllInput()
end

FacRegionUpgradeCtrl._OnPurchaseRegionItemBefore = HL.Method() << function(self)
    if not self.m_regionEffectInitialized then
        return
    end

    GameInstance.remoteFactoryManager.visual:ShowFence()
    GameInstance.remoteFactoryManager.visual:BeginFenceBlending(self.m_levelId, self.m_regionIndex)
    GameWorld.gameMechManager.powerPoleBrain:ClearConnectionDict()

    local purchaseLevel = self.m_itemDataGetter[self.m_purchaseItemId].upgradeLevel
    local lastPurchasedLevel = purchaseLevel - 1
    local lastPurchaseEffectData = self.m_regionEffectDataGetter[lastPurchasedLevel]
    if lastPurchaseEffectData ~= nil then
        lastPurchaseEffectData.effectController:SetVisibleState(false)
    end
end

FacRegionUpgradeCtrl._OnPurchaseBusItemBefore = HL.Method() << function(self)
    if not self.m_busEffectInitialized then
        return
    end

    local purchaseInstKey = self.m_itemDataGetter[self.m_purchaseItemId].instKey
    local purchaseInstEffectData = self.m_busEffectDataGetter[purchaseInstKey]
    if purchaseInstEffectData ~= nil then
        purchaseInstEffectData.effectController:SetVisibleState(false)
    end

    CS.Beyond.Gameplay.Actions.GameAction.FacShowForceUpdate(true)
end

FacRegionUpgradeCtrl._OnPurchaseSelectItemAfter = HL.Method(HL.Function) << function(self, onPlayFinished)
    local itemData = self.m_itemDataGetter[self.m_purchaseItemId]
    if itemData == nil then
        return
    end

    local onPlayFinishCallback = function()
        UIUtils.PlayAnimationAndToggleActive(self.view.mainAnim, true)
        onPlayFinished()
        self.m_purchaseItemId = ""
        self.m_purchaseEnabled = true
        self.view.luaPanel:RecoverAllInput()
    end

    self:_ClearPurchasedAnimTimer()

    if itemData.type == GEnums.FacPanelStoreGoodType.RegionLevelUp then
        self:_OnPurchaseRegionItemAfter(onPlayFinishCallback)
    elseif itemData.type == GEnums.FacPanelStoreGoodType.BusPlace then
        self:_OnPurchaseBusItemAfter(onPlayFinishCallback)
    end
end

FacRegionUpgradeCtrl._OnPurchaseRegionItemAfter = HL.Method(HL.Function) << function(self, onPlayFinished)
    if not self.m_regionEffectInitialized then
        onPlayFinished()
        return
    end

    local purchaseLevel = self.m_itemDataGetter[self.m_purchaseItemId].upgradeLevel
    local purchaseEffectData = self.m_regionEffectDataGetter[purchaseLevel]
    if purchaseEffectData ~= nil then
        purchaseEffectData.effectController:SetVisibleState(false)
    end

    GameInstance.remoteFactoryManager.visual:PlayFenceBlending(self.view.config.REGION_UPGRADE_BLENDING_DURATION)
    local timerDuration = self.view.config.REGION_UPGRADE_BLENDING_DURATION + self.view.config.REGION_UPGRADE_BLENDING_END_DELAY_DURATION
    self.m_purchasedAnimTimer = self:_StartTimer(timerDuration, function()
        GameInstance.remoteFactoryManager.visual:EndFenceBlending()
        GameInstance.remoteFactoryManager.visual:HideFence()

        self:_ClearPurchasedAnimTimer()
        onPlayFinished()
    end)
end

FacRegionUpgradeCtrl._OnPurchaseBusItemAfter = HL.Method(HL.Function) << function(self, onPlayFinished)

    CS.Beyond.Gameplay.Actions.GameAction.FacShowForceUpdate(false)
    if not self.m_busEffectInitialized then
        onPlayFinished()
        return
    end

    local timerDuration = self.view.config.BUS_UPGRADE_BLENDING_DURATION
    self.m_purchasedAnimTimer = self:_StartTimer(timerDuration, function()
        self:_ClearPurchasedAnimTimer()
        onPlayFinished()
    end)
end

FacRegionUpgradeCtrl._ClearPurchasedAnimTimer = HL.Method() << function(self)
    if self.m_purchasedAnimTimer <= 0 then
        return
    end
    self.m_purchasedAnimTimer = self:_ClearTimer(self.m_purchasedAnimTimer)
end






FacRegionUpgradeCtrl._InitRegionUpgradeController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self:_FindFirstItemCellToSelectInController()
end

FacRegionUpgradeCtrl._FindFirstItemCellToSelectInController = HL.Method() << function(self)
    local selectedItemData = self.m_itemDataGetter[self.m_selectItemId]
    if selectedItemData ~= nil and selectedItemData.cell ~= nil then
        self:SetNaviTarget(selectedItemData.cell.button)
        return
    end


    local findList = { self.m_regionItemDataList, self.m_busItemDataList }
    for _, dataList in ipairs(findList) do
        for _, data in ipairs(dataList) do
            if not data.isPurchased then
                self:SetNaviTarget(data.cell.button)
                return
            end
        end
    end
end




HL.Commit(FacRegionUpgradeCtrl)
