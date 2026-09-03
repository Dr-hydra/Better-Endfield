local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacMachineCrafter
FacMachineCrafterCtrl = HL.Class('FacMachineCrafterCtrl', uiCtrl.UICtrl)

local START_CACHE_COUNT = 1
local MAX_CACHE_COUNT = 4

local SMART_ALERT_FUNCTION_NAME_LIST = {
    "_CheckAlertNoPowerCondition",
    "_CheckAlertNoPowerWithDiffuserCondition",
    "_CheckAlertNoPowerWithoutDiffuserCondition",
    "_CheckAlertCanBeOpenedCondition",
    "_CheckAlertActivatorCostInsufficientCondition",
    "_CheckAlertFillingNeedUnlockGasCondition",
    "_CheckAlertFluidInputEmptyCondition",
    "_CheckAlertNormalInputEmptyCondition",
    "_CheckAlertDismantlerNeedUnlockGasCondition",
    "_CheckAlertInputInvalidFormulaCondition",
    "_CheckAlertFormulaNeedEnvCondition",
    "_CheckAlertOutputCacheFullWithPipeCondition",
    "_CheckAlertOutputCacheFullWithoutPipeCondition",
    "_CheckAlertOutputCacheFullWithBeltCondition",
    "_CheckAlertOutputCacheFullWithoutBeltCondition",
    "_CheckAlertInputCacheFullCondition",
    "_CheckAlertFluidOutputMultiBlockedCondition",
    "_CheckAlertNormalOutputMultiBlockedCondition",
    "_CheckAlertFluidInputMultiBlockedCondition",
    "_CheckAlertFluidInputSingleBlockedCondition",
    "_CheckAlertNormalInputMultiBlockedCondition",
    "_CheckAlertNormalInputSingleBlockedCondition",
}







FacMachineCrafterCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FAC_MACHINE_MODE_CHANGE_SYNC_PORT] = '_OnModeChangeSyncPort',
    [MessageConst.FAC_ON_DEL_TIME_LIMITED_FORMULA] = "_OnFormulaDelete",
}

FacMachineCrafterCtrl.m_nodeId = HL.Field(HL.Any)

FacMachineCrafterCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_Producer)

FacMachineCrafterCtrl.m_onBuildingFormulaChanged = HL.Field(HL.Function)

FacMachineCrafterCtrl.m_lastProgressFormulaId = HL.Field(HL.String) << ""

FacMachineCrafterCtrl.m_skipFirstRefreshFormula = HL.Field(HL.Boolean) << true

FacMachineCrafterCtrl.m_isInventoryLocked = HL.Field(HL.Boolean) << false

FacMachineCrafterCtrl.m_noNormalOutputCache = HL.Field(HL.Boolean) << false

FacMachineCrafterCtrl.m_needInversePipe = HL.Field(HL.Boolean) << false

FacMachineCrafterCtrl.m_smartAlertTargetTransformCache = HL.Field(HL.Table)

FacMachineCrafterCtrl.m_smartAlertConditionDataCache = HL.Field(HL.Table)

FacMachineCrafterCtrl.m_isCacheAreaInitialized = HL.Field(HL.Boolean) << false

FacMachineCrafterCtrl.m_needRefreshPortState = HL.Field(HL.Boolean) << false

FacMachineCrafterCtrl.m_layoutData = HL.Field(HL.Table)

FacMachineCrafterCtrl.m_cacheInCanDropLevel = HL.Field(HL.Number) << 0

FacMachineCrafterCtrl.m_cacheOutCanDropLevel = HL.Field(HL.Number) << 0


FacMachineCrafterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId


    self:_ProcessLayoutData()
    self.m_noNormalOutputCache = self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] == 0
    self.m_needInversePipe = FacConst.FAC_PRODUCER_NEED_INVERSE_PIPE[self.m_uiInfo.nodeHandler.templateId] ~= nil
    self.view.inventoryArea:InitInventoryArea({
        onStateChange = function()
            self:_RefreshNaviGroupSwitcherInfos()
        end,
        customOnUpdateCell = function(cell, itemBundle)
            self:_RefreshInventoryItemCell(cell, itemBundle)
        end,
        customSetActionMenuArgs = function(actionMenuArgs)
            actionMenuArgs.cacheArea = self.view.cacheArea
        end,
        hasFluidInCache = self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] > 0 or self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] > 0,
        hasGasInCache = self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas] > 0 or self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] > 0,
        noNormalCache = self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] == 0,
        lockFormulaId = FactoryUtils.getMachineCraftLockFormulaId(self.m_uiInfo.nodeId),
    })
    self.m_isInventoryLocked = FactoryUtils.isBuildingInventoryLocked(nodeId)
    self.view.inventoryArea:LockInventoryArea(self.m_isInventoryLocked)


    self.view.functionBtn.gameObject:SetActiveIfNecessary(not self.m_noNormalOutputCache)
    self.view.formulaNode.view.decoNode.gameObject:SetActiveIfNecessary(self.m_noNormalOutputCache)
    self.view.gainBtn.onClick:AddListener(function()
        self.view.cacheArea:GainAreaOutItems()
    end)


    self:_OnExpendTypeMachineInit()


    self:_StartCoroutine(function()
        while true do
            if self.m_isClosed then
                return
            end
            coroutine.step()
            self.view.facProgressNode:UpdateProgress(self.m_uiInfo.producer.currentProgress)
            self:_OnExpendTypeMachineUpdate()
            if not self.m_noNormalOutputCache then
                self:_UpdateGainButtonState()
            end
        end
    end)


    self.view.formulaNode:InitFormulaNode(self.m_uiInfo)
    self.m_onBuildingFormulaChanged = function()
        self:_RefreshFormulaInfo()
    end
    self.m_uiInfo.onFormulaChanged:AddListener(self.m_onBuildingFormulaChanged)


    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {
        onStateChanged = function(state)
            self:_OnExpendTypeStateUpdate(state)
        end,
        onEnvChanged = function(env)
            self:_RefreshFormulaInfo()
        end,
        smartAlertFuncNameList = SMART_ALERT_FUNCTION_NAME_LIST,
        targetCtrlInstance = self
    })
    self.view.buildingCommon.isModeEnvRelated = FactoryUtils.checkIsBuildingModeEnvRelated(self.m_uiInfo.buildingId, self.m_uiInfo.formulaMan.currentMode)
    self:_RefreshCrafterWidth()


    self.view.cachePipe:InitFacCachePipe(self.m_uiInfo, {
        needModeSwitch = true,
        needInversePipe = self.m_needInversePipe,
    })
    self:_ChangePipeSpacingWithCacheSlotCount()


    self.view.machineToggle:InitMachineToggle(self.m_uiInfo, {
        preSwitchMode = function()
            if self.m_isClosed then
                return
            end
            self.view.buildingCommon.smartAlertChangeCachePauseUpdate = true
        end,
        postSwitchMode = function()
            if self.m_isClosed then
                return
            end
            self:_PostSwitchMode()
        end,
    })
    if self.view.machineToggle.gameObject.activeSelf then
        self.view.buildingCommon.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
            extraBtnInfos = {
                {
                    button = self.view.formulaNode.view.openBtn,
                    sprite = self.view.formulaNode.view.openBtnIcon.sprite,
                    textId = "key_hint_fac_machine_toggle_formula",
                    priority = 2.2,
                },
                {
                    name = "MachineToggle",
                    action = function()
                        self.view.machineToggle:ControllerSideMenuClick()
                    end,
                    getText = function()
                        return self.view.machineToggle:GetControllerSideMenuText()
                    end,
                    sprite = self.view.machineToggle:GetControllerSideMenuSprite(),
                    priority = 2.1,
                },
            }
        })
    end


    self.view.cacheAreaCanvasGroup.alpha = 0
    self.view.cacheArea:InitFacCacheArea({
        buildingInfo = self.m_uiInfo,
        inChangedCallback = function(cacheItems)

            if self.m_skipFirstRefreshFormula then
                self.m_skipFirstRefreshFormula = false
                return
            end
            self:_RefreshFormulaInfo()
        end,
        outChangedCallback = function(cacheItems)
        end,
        onInitializeFinished = function()
            self.view.cacheAreaCanvasGroup.alpha = 1
            self.m_isCacheAreaInitialized = true
            self:_InitCacheBelt()


            self:_InitFacMachineCrafterController()


            self:_RefreshFormulaInfo()
            self:_UpdateSmartAlertCache()
        end,
        onIsInCacheAreaNaviGroup = function(isIn)
            self.view.contentBindingGroup.enabled = isIn
        end
    })


    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)
    end

FacMachineCrafterCtrl.OnClose = HL.Override() << function(self)
    self.view.buildingCommon:ClearSmartAlertUpdate()
    self.m_uiInfo.onFormulaChanged:RemoveListener(self.m_onBuildingFormulaChanged)
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_nodeId)
end

FacMachineCrafterCtrl.OnShow = HL.Override() << function(self)
    if self.m_needRefreshPortState then
        self.m_needRefreshPortState = false
        self.view.cacheBelt:RefreshBeltCellsState()
        self.view.cachePipe:RefreshPipeCellsState()
    end
end

FacMachineCrafterCtrl.OnHide = HL.Override() << function(self)
    self.m_needRefreshPortState = true
end

FacMachineCrafterCtrl.OnAnimationInFinished = HL.Override() << function(self)
end




FacMachineCrafterCtrl._OnExpendTypeMachineInit = HL.Virtual() << function(self)

end

FacMachineCrafterCtrl._OnExpendTypeMachineUpdate = HL.Virtual() << function(self)

end

FacMachineCrafterCtrl._OnExpendTypeStateUpdate = HL.Virtual(HL.Userdata) << function(self, state)
    self:_RefreshChangeState(state)
end




FacMachineCrafterCtrl._RefreshInventoryItemCell = HL.Method(HL.Userdata, HL.Any) << function(self, cell, itemBundle)
    if cell == nil or itemBundle == nil then
        return
    end

    if self.m_cacheInCanDropLevel == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then

        return
    end

    local itemId = itemBundle.id

    local canDrop = FactoryUtils.isEmptyBottleOrJarItem(itemId, self.m_cacheInCanDropLevel)
        or FactoryUtils.isFullBottleOrJarItem(itemId, self.m_cacheInCanDropLevel)
        or FactoryUtils.isEmptyBottleOrJarItem(itemId, self.m_cacheOutCanDropLevel)
    local isEmpty = string.isEmpty(itemBundle.id)

    if not canDrop and not isEmpty then
        cell.view.forbiddenMask.gameObject:SetActiveIfNecessary(true)
        cell.view.dropItem.enabled = false
    end
    if not canDrop then
        cell.view.dragItem.enabled = false
    end
end




FacMachineCrafterCtrl._GetMachineFormulaId = HL.Method().Return(HL.String) << function(self)
    local lockFormulaId = FactoryUtils.getMachineCraftLockFormulaId(self.m_uiInfo.nodeId)
    if not string.isEmpty(lockFormulaId) then
        return lockFormulaId
    end

    if not string.isEmpty(self.m_uiInfo.formulaId) then
        return self.m_uiInfo.formulaId
    end

    local matchedFormulaId = self:_GetCurrentMatchedFormulaId()
    if not string.isEmpty(matchedFormulaId) then
        return matchedFormulaId
    end

    if not string.isEmpty(self.m_lastProgressFormulaId) then
        return self.m_lastProgressFormulaId
    end

    return self.m_uiInfo.lastFormulaId
end

FacMachineCrafterCtrl._RefreshFormulaInfo = HL.Method() << function(self)
    if not self.m_isCacheAreaInitialized then
        return
    end
    if self.m_lastProgressFormulaId and (not self.m_lastProgressFormulaId:isEmpty()) then
        if not GameInstance.player.remoteFactory.core:IsFormulaVisible(self.m_lastProgressFormulaId) then
            self.m_lastProgressFormulaId = ""
        end
    end


    local id = self:_GetMachineFormulaId()
    local isFormulaMissing = string.isEmpty(id)

    if isFormulaMissing then
        self.view.formulaNode:RefreshDisplayFormula()
        self.view.facProgressNode:InitFacProgressNode(-1, -1)
        self.view.facProgressNode:SwitchAudioPlayingState(false)
        self.m_lastProgressFormulaId = id
        self.view.cacheArea:ChangedFormula(id, self.m_uiInfo.lastFormulaId)
        return
    end

    if id == self.m_lastProgressFormulaId then
        return
    end

    local machineCraftData = Tables.factoryMachineCrafterTable:GetValue(self.m_uiInfo.buildingId)
    local formulaGroupId
    for index = 0, machineCraftData.modeMap.Count - 1 do
        local mapData = machineCraftData.modeMap[index]
        if mapData ~= nil and mapData.modeName == self.m_uiInfo.formulaMan.currentMode then
            formulaGroupId = mapData.groupName
            break
        end
    end
    local craftInfo = FactoryUtils.parseMachineCraftData(id, formulaGroupId)
    local craftData = Tables.factoryMachineCraftTable:GetValue(id)
    local time = craftInfo.time

    self.view.formulaNode:RefreshDisplayFormula(craftInfo)
    self.view.cacheArea:ChangedFormula(id, self.m_uiInfo.lastFormulaId)

    local colorStr = ""
    self.view.facProgressNode:InitFacProgressNode(
        time,
        craftData.totalProgress * FacConst.CRAFT_PROGRESS_MULTIPLIER,
        colorStr,
        function()
            self.view.cacheArea:PlayArrowAnimation()
            AudioAdapter.PostEvent("au_ui_fac_yield")
        end,
        function()
            self:_PlayProgressFinishedAnimation()
        end
    )
    self.m_lastProgressFormulaId = id

    self.view.facProgressNode:SwitchAudioPlayingState(not string.isEmpty(self.m_uiInfo.formulaId))
end

FacMachineCrafterCtrl._GetCurrentMatchedFormulaId = HL.Method().Return(HL.String) << function(self)
    local itemList = {}
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local normalCache = self.m_uiInfo:GetCache(i, true, false)
        if normalCache and normalCache.items.Count > 0 then
            for itemId, _ in cs_pairs(normalCache.items) do
                table.insert(itemList, itemId)
            end
        end
        local liquidCache = self.m_uiInfo:GetCache(i, true, true)
        if liquidCache and liquidCache.items.Count > 0 then
            for itemId, _ in cs_pairs(liquidCache.items) do
                table.insert(itemList, itemId)
            end
        end
    end

    local currEnv
    if self.m_uiInfo.envReceiver ~= nil then
        currEnv = self.m_uiInfo.envReceiver.currentEnv
    end

    return FactoryUtils.getMatchedFormulaIdByItemList(
        self.m_uiInfo.buildingId,
        self.m_uiInfo.formulaMan.currentMode,
        itemList,
        currEnv
    )
end

FacMachineCrafterCtrl._RefreshCrafterWidth = HL.Method() << function(self)
    local isWide = self.view.buildingCommon.bgRatio > 1


    local cfg = self.view.config

    self.view.cacheBelt.view.inBeltGroup.anchoredPosition = Vector2(
        isWide and cfg.WIDE_IN_BELT_POS_X or cfg.NORMAL_IN_BELT_POS_X,
        self.view.cacheBelt.view.inBeltGroup.anchoredPosition.y
    )
    self.view.cacheBelt.view.outBeltGroup.anchoredPosition = Vector2(
        isWide and cfg.WIDE_OUT_BELT_POS_X or cfg.NORMAL_OUT_BELT_POS_X,
        self.view.cacheBelt.view.inBeltGroup.anchoredPosition.y
    )

    local inWidth = isWide and cfg.WIDE_IN_LINE_WIDTH or cfg.NORMAL_IN_LINE_WIDTH
    local outWidth = isWide and cfg.WIDE_OUT_LINE_WIDTH or cfg.NORMAL_OUT_LINE_WIDTH
    self.view.cacheArea.view.inRepositoryList.repository1.view.slotCell.view.itemSlot.view.facLineCell:ChangeLineWidth(inWidth)
    self.view.cacheArea.view.inRepositoryList.repository2.view.slotCell.view.itemSlot.view.facLineCell:ChangeLineWidth(inWidth)
    self.view.cacheArea.view.outRepositoryList.repository1.view.slotCell.view.itemSlot.view.facLineCell:ChangeLineWidth(outWidth)
    self.view.cacheArea.view.outRepositoryList.repository2.view.slotCell.view.itemSlot.view.facLineCell:ChangeLineWidth(outWidth)
end

FacMachineCrafterCtrl._RefreshChangeState = HL.Method(HL.Userdata) << function(self, state)
    local useStateText = FactoryUtils.refreshStateNodeByState(self.view.facStateNode, self.view.facProgressNode, state)
    self.view.cacheArea:RefreshAreaBlockState(state == GEnums.FacBuildingState.Blocked)
    if not useStateText then
        self.view.facProgressNode:SwitchAudioPlayingState(state == GEnums.FacBuildingState.Normal)
    end
end






FacMachineCrafterCtrl._InitCacheBelt = HL.Method() << function(self)
    self.view.cacheBeltCanvasGroup.alpha = 0
    self.view.cacheBelt:InitFacCacheBelt(self.m_uiInfo, {
        noGroup = false,
        inEndSlotGroupGetter = function()
            return self.view.cacheArea:GetAreaInRepositoryNormalSlotGroup()
        end,
        outEndSlotGroupGetter = function()
            return self.view.cacheArea:GetAreaOutRepositoryNormalSlotGroup()
        end,
        onInitializeFinished = function()
            self.view.cacheBeltCanvasGroup.alpha = 1
            self.view.cacheArea:InitAreaNaviTarget()
        end
    })
end

FacMachineCrafterCtrl._UpdateGainButtonState = HL.Method() << function(self)
    local findItem = false

    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local cache = self.m_uiInfo:GetCache(i, false, false)
        if cache and cache.operationItemsInfo.Count > 0 then
            findItem = true
            break
        end






    end

    self.view.gainBtn.interactable = findItem and not self.m_isInventoryLocked
end






FacMachineCrafterCtrl._PlayProgressFinishedAnimation = HL.Method() << function(self)
    local normalSlotList = self.view.cacheArea:GetAreaInRepositoryNormalSlotGroup()
    local liquidSlotList = self.view.cacheArea:GetAreaInRepositoryFluidSlotGroup()

    if normalSlotList ~= nil then
        for _, slotGroup in ipairs(normalSlotList) do
            for _, slot in ipairs(slotGroup) do
                slot:PlaySlotAnimation("itemslot_arrow_loop")
            end
        end
    end

    if liquidSlotList ~= nil then
        for _, slotGroup in ipairs(liquidSlotList) do
            for _, slot in ipairs(slotGroup) do
                slot:PlaySlotAnimation("liquidslot_arrow_loop")
            end
        end
    end
end






FacMachineCrafterCtrl._OnModeChangeSyncPort = HL.Method(HL.Any) << function(self, args)
    local nodeId = unpack(args)
    if nodeId == self.m_nodeId then

        self.view.cacheArea:RefreshCacheArea()
        self.view.cacheBelt:RefreshCacheBelt()
        self.view.cachePipe:RefreshCachePipe()
    end
end

FacMachineCrafterCtrl._OnFormulaDelete = HL.Method(HL.Any) << function(self, args)
    self:_RefreshFormulaInfo()
end

FacMachineCrafterCtrl._PostSwitchMode = HL.Method() << function(self)
    self.m_uiInfo:Update(true)
    self.m_lastProgressFormulaId = ""
    self.m_uiInfo:ClearProducerLastValidFormulaId()
    self:_RefreshFormulaInfo()
    self.view.cacheArea:RefreshCacheArea()
    self.view.cacheBelt:RefreshCacheBelt()
    self.view.cachePipe:RefreshCachePipe()
    self.view.formulaNode:RefreshRedDot()
    self:_ProcessLayoutData()
    self:_UpdateSmartAlertCache()
    self:_ChangePipeSpacingWithCacheSlotCount()
    self.view.inventoryArea:SetBuildingHasFluidCache(self.m_smartAlertConditionDataCache.hasFluidCache)
    self.view.inventoryArea:SetBuildingNoNormalCache(not self.m_smartAlertConditionDataCache.hasItemCache)
    self.view.functionBtn.gameObject:SetActiveIfNecessary(not self.m_noNormalOutputCache)
    self.view.formulaNode.view.decoNode.gameObject:SetActiveIfNecessary(self.m_noNormalOutputCache)
    if self.view.buildingCommon.smartAlertDynamicNode ~= nil then
        self.view.buildingCommon.smartAlertDynamicNode:ForceUpdateAlertPosition()
    end
    self.view.buildingCommon.isModeEnvRelated = FactoryUtils.checkIsBuildingModeEnvRelated(self.m_uiInfo.buildingId, self.m_uiInfo.formulaMan.currentMode)
    if self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) or self.view.cacheArea:CheckRepoNaviTargetTopLayer(false) then
        self.view.cacheArea:InitAreaNaviTarget()
    end
end

FacMachineCrafterCtrl._ChangePipeSpacingWithCacheSlotCount = HL.Method() << function(self)

    local oneLiquidIn = self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] == 0 and
        self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] + self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas] + self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] == 1
    local oneLiquidOut = self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] == 0 and
        self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] + self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas] + self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] == 1
    self.view.cachePipe:ChangePipeSpacingY(true, oneLiquidIn)
    self.view.cachePipe:ChangePipeSpacingY(false, oneLiquidOut)


    self.view.cachePipe:ChangePipeLineColor(true, #self.m_layoutData.liquidIncomeCaches < 2)
    self.view.cachePipe:ChangePipeLineColor(false, #self.m_layoutData.liquidOutcomeCaches < 2)
end

FacMachineCrafterCtrl._ProcessLayoutData = HL.Method() << function(self)
    self.m_layoutData = FactoryUtils.getMachineCraftCacheLayoutData(self.m_nodeId)

    if self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] > 0 then
        self.m_cacheInCanDropLevel = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal
    elseif self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] > 0 then
        self.m_cacheInCanDropLevel = FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid
    else
        self.m_cacheInCanDropLevel = 0
        if self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] > 0 then
            self.m_cacheInCanDropLevel = self.m_cacheInCanDropLevel + FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid
        end
        if self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas] > 0 then
            self.m_cacheInCanDropLevel = self.m_cacheInCanDropLevel + FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas
        end
    end
    if self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] > 0 then
        self.m_cacheOutCanDropLevel = FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid
    else
        self.m_cacheOutCanDropLevel = 0
        if self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] > 0 then
            self.m_cacheOutCanDropLevel = self.m_cacheOutCanDropLevel + FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid
        end
        if self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas] > 0 then
            self.m_cacheOutCanDropLevel = self.m_cacheOutCanDropLevel + FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas
        end
    end
end






FacMachineCrafterCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacMachineCrafterCtrl._InitFacMachineCrafterController = HL.Method() << function(self)
    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)

    self:_RefreshNaviGroupSwitcherInfos()

    InputManagerInst:ChangeParent(
        true,
        self.view.buildingCommon.view.closeButton.groupId,
        self.view.inputGroup.groupId
    )
end

FacMachineCrafterCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {}
    self.view.cacheArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.view.inventoryArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end






FacMachineCrafterCtrl._UpdateSmartAlertCache = HL.Method() << function(self)
    self.m_smartAlertTargetTransformCache = {}
    self.m_smartAlertTargetTransformCache.inBelt = {}
    if self.view.cacheBelt.m_inBeltList then
        local tempCount = self.view.cacheBelt.m_inBeltList:GetCount()
        for index = 1, tempCount do
            local cell = self.view.cacheBelt.m_inBeltList:GetItem(index)
            self.m_smartAlertTargetTransformCache.inBelt[index] = cell.transform
        end
    end
    self.m_smartAlertTargetTransformCache.outBelt = {}
    if self.view.cacheBelt.m_outBeltList then
        local tempCount = self.view.cacheBelt.m_outBeltList:GetCount()
        for index = 1, tempCount do
            local cell = self.view.cacheBelt.m_outBeltList:GetItem(index)
            self.m_smartAlertTargetTransformCache.outBelt[index] = cell.transform
        end
        self.m_smartAlertTargetTransformCache.lastOutBelt = self.m_smartAlertTargetTransformCache.outBelt[tempCount]
    end
    self.m_smartAlertTargetTransformCache.inPipe = {}
    if self.m_needInversePipe then
        self.m_smartAlertTargetTransformCache.inPipe[1] = self.view.cachePipe.view.pipeCell2.smartAlertNode.transform
        self.m_smartAlertTargetTransformCache.inPipe[2] = self.view.cachePipe.view.pipeCell1.smartAlertNode.transform
    else
        self.m_smartAlertTargetTransformCache.inPipe[1] = self.view.cachePipe.view.pipeCell1.smartAlertNode.transform
        self.m_smartAlertTargetTransformCache.inPipe[2] = self.view.cachePipe.view.pipeCell2.smartAlertNode.transform
    end
    self.m_smartAlertTargetTransformCache.outPipe = {}
    if self.m_needInversePipe then
        self.m_smartAlertTargetTransformCache.outPipe[1] = self.view.cachePipe.view.pipeCell4.smartAlertNode.transform
        self.m_smartAlertTargetTransformCache.outPipe[2] = self.view.cachePipe.view.pipeCell3.smartAlertNode.transform
    else
        self.m_smartAlertTargetTransformCache.outPipe[1] = self.view.cachePipe.view.pipeCell3.smartAlertNode.transform
        self.m_smartAlertTargetTransformCache.outPipe[2] = self.view.cachePipe.view.pipeCell4.smartAlertNode.transform
    end
    self.m_smartAlertTargetTransformCache.normalInput = {}
    local repoList = self.view.cacheArea:GetAreaInRepositoryNormalSlotGroup()
    for repoIndex, slotList in ipairs(repoList) do
        self.m_smartAlertTargetTransformCache.normalInput[repoIndex] = {}
        for _, slot in ipairs(slotList) do
            table.insert(self.m_smartAlertTargetTransformCache.normalInput[repoIndex], slot.transform)
        end
    end
    self.m_smartAlertTargetTransformCache.fluidInput = {}
    repoList = self.view.cacheArea:GetAreaInRepositoryFluidSlotGroup()
    for repoIndex, slotList in ipairs(repoList) do
        self.m_smartAlertTargetTransformCache.fluidInput[repoIndex] = {}
        for _, slot in ipairs(slotList) do
            table.insert(self.m_smartAlertTargetTransformCache.fluidInput[repoIndex], slot.transform)
        end
    end
    self.m_smartAlertTargetTransformCache.normalOutput = {}
    repoList = self.view.cacheArea:GetAreaOutRepositoryNormalSlotGroup()
    for repoIndex, slotList in ipairs(repoList) do
        self.m_smartAlertTargetTransformCache.normalOutput[repoIndex] = {}
        for _, slot in ipairs(slotList) do
            table.insert(self.m_smartAlertTargetTransformCache.normalOutput[repoIndex], slot.transform)
        end
    end
    self.m_smartAlertTargetTransformCache.fluidOutput = {}
    repoList = self.view.cacheArea:GetAreaOutRepositoryFluidSlotGroup()
    for repoIndex, slotList in ipairs(repoList) do
        self.m_smartAlertTargetTransformCache.fluidOutput[repoIndex] = {}
        for _, slot in ipairs(slotList) do
            table.insert(self.m_smartAlertTargetTransformCache.fluidOutput[repoIndex], slot.transform)
        end
    end
    self.m_smartAlertTargetTransformCache.state = self.view.buildingCommon.view.stateNode.transform

    self.m_smartAlertConditionDataCache = {}
    self.m_smartAlertConditionDataCache.effectiveFormula = {}
    local crafts = FactoryUtils.getBuildingCraftsWithNodeId(self.m_nodeId)
    if crafts then
        for _, craft in ipairs(crafts) do
            if craft.incomes then
                for i = 1, #craft.incomes do
                    self.m_smartAlertConditionDataCache.effectiveFormula[craft.incomes[i].id] = true
                end
            end
        end
    end
    if self.m_layoutData then
        self.m_smartAlertConditionDataCache.hasItemCache = self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] > 0
        self.m_smartAlertConditionDataCache.hasFluidCache = self.m_layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] > 0

        self.m_noNormalOutputCache = self.m_layoutData.outSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] == 0
    end
    self.m_smartAlertConditionDataCache.machineName = Tables.factoryBuildingTable:GetValue(self.m_uiInfo.buildingId).name
    self.m_smartAlertConditionDataCache.hasBelt = GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedBelt and
        GameInstance.remoteFactoryManager:IsFacNodeInMainRegion(
            self.m_uiInfo.nodeHandler.belongChapter.chapterId,
            self.m_nodeId
        )
    self.m_smartAlertConditionDataCache.hasPipe = GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedPipe

    self.m_smartAlertConditionDataCache.fillingAndDismantlerUnlockGasMode = not GameInstance.player.facTechTreeSystem:NodeIsLocked("tech_jinlong_4_filling_and_dismantler_mode_2")

    self.view.buildingCommon.smartAlertChangeCachePauseUpdate = false
end

FacMachineCrafterCtrl._CheckAlertNormalInputSingleBlockedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Normal then
        return false
    end
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local normalCache = self.m_uiInfo:GetCache(i, true, false)
        if normalCache and normalCache.blockedMismatchItems.Count > 0 then
            local itemIdOrMultiTag, blockPort
            for i = 0, normalCache.blockedMismatchItems.Count - 1 do
                blockPort = normalCache.blockedMismatchItems[i].portIndex
                if not itemIdOrMultiTag then
                    itemIdOrMultiTag = normalCache.blockedMismatchItems[i].itemId
                elseif itemIdOrMultiTag ~= normalCache.blockedMismatchItems[i].itemId then
                    return false
                end
            end

            if itemIdOrMultiTag then
                local checkOpen = DeviceInfo.usingController and
                    self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                    self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                local alertInfo = {
                    condition = GEnums.FacSmartAlertType.NormalInputSingleBlocked,
                    targetTransform = self.m_smartAlertTargetTransformCache.inBelt[LuaIndex(blockPort)],
                    args = {},
                    checkRefresh = itemIdOrMultiTag .. tostring(blockPort),
                    defaultOpen = checkOpen
                }
                table.insert(alertInfo.args, UIUtils.getItemName(itemIdOrMultiTag))
                table.insert(alertInfo.args, self.m_smartAlertConditionDataCache.machineName)
                table.insert(alertInfo.args, self.m_smartAlertConditionDataCache.machineName)
                return true, alertInfo
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertNormalInputMultiBlockedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Normal then
        return false
    end
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local normalCache = self.m_uiInfo:GetCache(i, true, false)
        if normalCache and normalCache.blockedMismatchItems.Count > 0 then
            local itemIdOrMultiTag, blockPort
            for i = 0, normalCache.blockedMismatchItems.Count - 1 do
                blockPort = normalCache.blockedMismatchItems[i].portIndex
                if not itemIdOrMultiTag then
                    itemIdOrMultiTag = normalCache.blockedMismatchItems[i].itemId
                elseif itemIdOrMultiTag ~= normalCache.blockedMismatchItems[i].itemId then
                    itemIdOrMultiTag = true
                end
            end

            if itemIdOrMultiTag == true then
                local checkOpen = DeviceInfo.usingController and
                    self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                    self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                local alertInfo = {
                    condition = GEnums.FacSmartAlertType.NormalInputMultiBlocked,
                    targetTransform = self.m_smartAlertTargetTransformCache.inBelt[LuaIndex(blockPort)],
                    args = {},
                    checkRefresh = tostring(blockPort),
                    defaultOpen = checkOpen
                }
                table.insert(alertInfo.args, self.m_smartAlertConditionDataCache.machineName)
                table.insert(alertInfo.args, self.m_smartAlertConditionDataCache.machineName)
                return true, alertInfo
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertFluidInputSingleBlockedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Normal then
        return false
    end
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local liquidCache = self.m_uiInfo:GetCache(i, true, true)
        if liquidCache and liquidCache.blockedMismatchItems.Count > 0 then
            local itemIdOrMultiTag, blockPort
            for i = 0, liquidCache.blockedMismatchItems.Count - 1 do
                blockPort = liquidCache.blockedMismatchItems[i].portIndex
                if not itemIdOrMultiTag then
                    itemIdOrMultiTag = liquidCache.blockedMismatchItems[i].itemId
                elseif itemIdOrMultiTag ~= liquidCache.blockedMismatchItems[i].itemId then
                    return false
                end
            end

            if itemIdOrMultiTag then
                local checkOpen = DeviceInfo.usingController and
                    self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                    self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                local alertInfo = {
                    condition = GEnums.FacSmartAlertType.FluidInputSingleBlocked,
                    targetTransform = self.m_smartAlertTargetTransformCache.inPipe[LuaIndex(blockPort)],
                    args = {},
                    checkRefresh = itemIdOrMultiTag .. tostring(blockPort),
                    defaultOpen = checkOpen
                }
                table.insert(alertInfo.args, UIUtils.getItemName(itemIdOrMultiTag))
                table.insert(alertInfo.args, self.m_smartAlertConditionDataCache.machineName)
                table.insert(alertInfo.args, self.m_smartAlertConditionDataCache.machineName)
                return true, alertInfo
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertFluidInputMultiBlockedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Normal then
        return false
    end
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local liquidCache = self.m_uiInfo:GetCache(i, true, true)
        if liquidCache and liquidCache.blockedMismatchItems.Count > 0 then
            local itemIdOrMultiTag, blockPort
            for i = 0, liquidCache.blockedMismatchItems.Count - 1 do
                blockPort = liquidCache.blockedMismatchItems[i].portIndex
                if not itemIdOrMultiTag then
                    itemIdOrMultiTag = liquidCache.blockedMismatchItems[i].itemId
                elseif itemIdOrMultiTag ~= liquidCache.blockedMismatchItems[i].itemId then
                    itemIdOrMultiTag = true
                end
            end

            if itemIdOrMultiTag == true then
                local checkOpen = DeviceInfo.usingController and
                    self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                    self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                local alertInfo = {
                    condition = GEnums.FacSmartAlertType.FluidInputMultiBlocked,
                    targetTransform = self.m_smartAlertTargetTransformCache.inPipe[LuaIndex(blockPort)],
                    args = {},
                    checkRefresh = tostring(blockPort),
                    defaultOpen = checkOpen
                }
                table.insert(alertInfo.args, self.m_smartAlertConditionDataCache.machineName)
                table.insert(alertInfo.args, self.m_smartAlertConditionDataCache.machineName)
                return true, alertInfo
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertNormalOutputMultiBlockedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Normal then
        return false
    end
    local _, outBeltInfoList = FactoryUtils.getBuildingPortState(self.m_nodeId, false)
    if outBeltInfoList then
        local blockPort = 0
        for i = 1, #outBeltInfoList do
            if outBeltInfoList[i].isBlock then
                blockPort = i
            end
        end
        if blockPort > 0 then
            local checkOpen = DeviceInfo.usingController and
                self.view.cacheArea:CheckRepoNaviTargetTopLayer(false) and
                self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()

            local alertInfo = {
                condition = GEnums.FacSmartAlertType.NormalOutputMultiBlocked,
                targetTransform = self.m_smartAlertTargetTransformCache.outBelt[blockPort],
                checkRefresh = tostring(blockPort),
                defaultOpen = checkOpen
            }
            return true, alertInfo
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertFluidOutputMultiBlockedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Normal then
        return false
    end
    local _, outPipeInfoList = FactoryUtils.getBuildingPortState(self.m_nodeId, true)
    if outPipeInfoList then
        local blockPort = 0
        for i = 1, #outPipeInfoList do
            if outPipeInfoList[i].isBlock then
                blockPort = i
            end
        end
        if blockPort > 0 then
            local checkOpen = DeviceInfo.usingController and
                self.view.cacheArea:CheckRepoNaviTargetTopLayer(false) and
                self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()

            local alertInfo = {
                condition = GEnums.FacSmartAlertType.FluidOutputMultiBlocked,
                targetTransform = self.m_smartAlertTargetTransformCache.outPipe[blockPort],
                checkRefresh = tostring(blockPort),
                defaultOpen = checkOpen
            }
            return true, alertInfo
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertInputCacheFullCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Normal then
        return false
    end
    local inBeltInfoList = FactoryUtils.getBuildingPortState(self.m_nodeId, false)
    local inPipeInfoList = FactoryUtils.getBuildingPortState(self.m_nodeId, true)
    if inBeltInfoList then
        for beltIndex, beltInfo in ipairs(inBeltInfoList) do
            if beltInfo.isBlock then
                for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
                    local normalCache = self.m_uiInfo:GetCache(i, true, false)
                    if normalCache and normalCache.items.Count > 0 then
                        for itemId, itemCount in cs_pairs(normalCache.items) do
                            local facItemSuccess, facItemData = Tables.factoryItemTable:TryGetValue(itemId)
                            if facItemSuccess then

                                if itemCount >= facItemData.buildingBufferStackLimit - 1 then
                                    local _, csIndex = normalCache.itemOrderMap:TryGetValue(itemId)
                                    local checkOpen = DeviceInfo.usingController and
                                        self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                                        self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                                    local alertInfo = {
                                        condition = GEnums.FacSmartAlertType.InputCacheFull,
                                        targetTransform = self.m_smartAlertTargetTransformCache.normalInput[i][LuaIndex(csIndex)],
                                        checkRefresh = "normal" .. tostring(i) .. tostring(csIndex),
                                        defaultOpen = checkOpen
                                    }
                                    return true, alertInfo
                                end
                            end
                        end
                    end
                end
                break
            end
        end
    end
    if inPipeInfoList then
        for pipeIndex, pipeInfo in ipairs(inPipeInfoList) do
            if pipeInfo.isBlock then
                for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
                    local liquidCache = self.m_uiInfo:GetCache(i, true, true)
                    if liquidCache and liquidCache.items.Count > 0 then
                        for itemId, itemCount in cs_pairs(liquidCache.items) do
                            local facItemSuccess, facItemData = Tables.factoryItemTable:TryGetValue(itemId)
                            if facItemSuccess then

                                if itemCount >= facItemData.buildingBufferStackLimit - 1 then
                                    local _, csIndex = liquidCache.itemOrderMap:TryGetValue(itemId)
                                    local checkOpen = DeviceInfo.usingController and
                                        self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                                        self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                                    local alertInfo = {
                                        condition = GEnums.FacSmartAlertType.InputCacheFull,
                                        targetTransform = self.m_smartAlertTargetTransformCache.fluidInput[i][LuaIndex(csIndex)],
                                        checkRefresh = "fluid" .. tostring(i) .. tostring(csIndex),
                                        defaultOpen = checkOpen
                                    }
                                    return true, alertInfo
                                end
                            end
                        end
                    end
                end
                break
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertOutputCacheFullWithoutBeltCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Blocked then
        return false
    end
    if self.m_smartAlertConditionDataCache.hasBelt then
        return false
    end
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local normalCache = self.m_uiInfo:GetCache(i, false, false)
        if normalCache and normalCache.items.Count > 0 then
            for itemId, itemCount in cs_pairs(normalCache.items) do
                local facItemSuccess, facItemData = Tables.factoryItemTable:TryGetValue(itemId)
                if facItemSuccess then
                    if itemCount >= facItemData.buildingBufferStackLimit then
                        local _, csIndex = normalCache.itemOrderMap:TryGetValue(itemId)
                        local checkOpen = DeviceInfo.usingController and
                            self.view.cacheArea:CheckRepoNaviTargetTopLayer(false) and
                            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()

                        local alertInfo = {
                            condition = GEnums.FacSmartAlertType.OutputCacheFullWithoutBelt,
                            targetTransform = self.m_smartAlertTargetTransformCache.normalOutput[i][LuaIndex(csIndex)],
                            args = {},
                            checkRefresh = itemId .. tostring(i) .. tostring(csIndex),
                            defaultOpen = checkOpen
                        }
                        table.insert(alertInfo.args, UIUtils.getItemName(itemId))
                        table.insert(alertInfo.args, UIUtils.getItemName(itemId))
                        return true, alertInfo
                    end
                end
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertOutputCacheFullWithBeltCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Blocked then
        return false
    end
    if not self.m_smartAlertConditionDataCache.hasBelt then
        return false
    end
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local normalCache = self.m_uiInfo:GetCache(i, false, false)
        if normalCache and normalCache.items.Count > 0 then
            for itemId, itemCount in cs_pairs(normalCache.items) do
                local facItemSuccess, facItemData = Tables.factoryItemTable:TryGetValue(itemId)
                if facItemSuccess then
                    if itemCount >= facItemData.buildingBufferStackLimit then
                        local _, csIndex = normalCache.itemOrderMap:TryGetValue(itemId)
                        local checkOpen = DeviceInfo.usingController and
                            self.view.cacheArea:CheckRepoNaviTargetTopLayer(false) and
                            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()

                        local alertInfo = {
                            condition = GEnums.FacSmartAlertType.OutputCacheFullWithBelt,
                            targetTransform = self.m_smartAlertTargetTransformCache.lastOutBelt,
                            args = {},
                            checkRefresh = itemId,
                            defaultOpen = checkOpen
                        }
                        table.insert(alertInfo.args, UIUtils.getItemName(itemId))
                        table.insert(alertInfo.args, UIUtils.getItemName(itemId))
                        return true, alertInfo
                    end
                end
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertOutputCacheFullWithoutPipeCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Blocked then
        return false
    end
    if self.m_smartAlertConditionDataCache.hasPipe then
        return false
    end
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local liquidCache = self.m_uiInfo:GetCache(i, false, true)
        if liquidCache and liquidCache.items.Count > 0 then
            for itemId, itemCount in cs_pairs(liquidCache.items) do
                local facItemSuccess, facItemData = Tables.factoryItemTable:TryGetValue(itemId)
                if facItemSuccess then
                    if itemCount >= facItemData.buildingBufferStackLimit then
                        local _, csIndex = liquidCache.itemOrderMap:TryGetValue(itemId)
                        local checkOpen = DeviceInfo.usingController and
                            self.view.cacheArea:CheckRepoNaviTargetTopLayer(false) and
                            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()

                        local alertInfo = {
                            condition = GEnums.FacSmartAlertType.OutputCacheFullWithoutPipe,
                            targetTransform = self.m_smartAlertTargetTransformCache.fluidOutput[i][LuaIndex(csIndex)],
                            args = {},
                            checkRefresh = itemId .. tostring(i) .. tostring(csIndex),
                            defaultOpen = checkOpen
                        }
                        table.insert(alertInfo.args, UIUtils.getItemName(itemId))
                        table.insert(alertInfo.args, UIUtils.getItemName(itemId))
                        return true, alertInfo
                    end
                end
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertOutputCacheFullWithPipeCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Blocked then
        return false
    end
    if not self.m_smartAlertConditionDataCache.hasPipe then
        return false
    end
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local liquidCache = self.m_uiInfo:GetCache(i, false, true)
        if liquidCache and liquidCache.items.Count > 0 then
            for itemId, itemCount in cs_pairs(liquidCache.items) do
                local facItemSuccess, facItemData = Tables.factoryItemTable:TryGetValue(itemId)
                if facItemSuccess then
                    if itemCount >= facItemData.buildingBufferStackLimit then
                        local _, csIndex = liquidCache.itemOrderMap:TryGetValue(itemId)
                        local pipeIndex = math.min(i + csIndex, 2)
                        local checkOpen = DeviceInfo.usingController and
                            self.view.cacheArea:CheckRepoNaviTargetTopLayer(false) and
                            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()

                        local alertInfo = {
                            condition = GEnums.FacSmartAlertType.OutputCacheFullWithPipe,
                            targetTransform = self.m_smartAlertTargetTransformCache.outPipe[pipeIndex],
                            args = {},
                            checkRefresh = itemId .. tostring(pipeIndex),
                            defaultOpen = checkOpen
                        }
                        table.insert(alertInfo.args, UIUtils.getItemName(itemId))
                        table.insert(alertInfo.args, UIUtils.getItemName(itemId))
                        return true, alertInfo
                    end
                end
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertInputInvalidFormulaCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Idle then
        return false
    end
    local effectiveFormulaItemMap = self.m_smartAlertConditionDataCache.effectiveFormula
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local normalCache = self.m_uiInfo:GetCache(i, true, false)
        if normalCache and normalCache.items.Count > 0 then
            for itemId, _ in cs_pairs(normalCache.items) do
                if not effectiveFormulaItemMap[itemId] then
                    local _, csIndex = normalCache.itemOrderMap:TryGetValue(itemId)
                    local checkOpen = DeviceInfo.usingController and
                        self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                        self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                    local alertInfo = {
                        condition = GEnums.FacSmartAlertType.InputInvalidFormula,
                        targetTransform = self.m_smartAlertTargetTransformCache.normalInput[i][LuaIndex(csIndex)],
                        checkRefresh = "normal" .. tostring(i) .. tostring(csIndex),
                        defaultOpen = checkOpen
                    }
                    return true, alertInfo
                end
            end
        end
        local liquidCache = self.m_uiInfo:GetCache(i, true, true)
        if liquidCache and liquidCache.items.Count > 0 then
            for itemId, _ in cs_pairs(liquidCache.items) do
                if not effectiveFormulaItemMap[itemId] then
                    local _, csIndex = liquidCache.itemOrderMap:TryGetValue(itemId)
                    local checkOpen = DeviceInfo.usingController and
                        self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                        self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                    local alertInfo = {
                        condition = GEnums.FacSmartAlertType.InputInvalidFormula,
                        targetTransform = self.m_smartAlertTargetTransformCache.fluidInput[i][LuaIndex(csIndex)],
                        checkRefresh = "fluid" .. tostring(i) .. tostring(csIndex),
                        defaultOpen = checkOpen
                    }
                    return true, alertInfo
                end
            end
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertNormalInputEmptyCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Idle then
        return false
    end
    if not self.m_smartAlertConditionDataCache.hasItemCache then
        return false
    end
    local itemEmpty = true
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local normalCache = self.m_uiInfo:GetCache(i, true, false)
        if normalCache and normalCache.items.Count > 0 then
            itemEmpty = false
            break
        end
    end
    if itemEmpty then
        local checkOpen = DeviceInfo.usingController and
            self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.NormalInputEmpty,
            targetTransform = self.m_smartAlertTargetTransformCache.normalInput[1][1],
            defaultOpen = checkOpen
        }
        return true, alertInfo
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertFluidInputEmptyCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Idle then
        return false
    end
    if not self.m_smartAlertConditionDataCache.hasFluidCache then
        return false
    end
    local fluidEmpty = true
    for i = START_CACHE_COUNT, MAX_CACHE_COUNT do
        local liquidCache = self.m_uiInfo:GetCache(i, true, true)
        if liquidCache and liquidCache.items.Count > 0 then
            fluidEmpty = false
            break
        end
    end
    if fluidEmpty then
        local checkOpen = DeviceInfo.usingController and
            self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.FluidInputEmpty,
            targetTransform = self.m_smartAlertTargetTransformCache.fluidInput[1][1],
            defaultOpen = checkOpen
        }
        return true, alertInfo
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertCanBeOpenedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Closed then
        return false
    end
    local node = self.m_uiInfo.nodeHandler
    if node and node.power and node.power.powerCost then
        local curCost = node.power.powerCost
        local powerInfo = FactoryUtils.getCurRegionPowerInfo()
        local powerCost = powerInfo.powerCost
        local powerGen = powerInfo.powerGen
        if powerCost + curCost <= powerGen then
            local checkOpen = DeviceInfo.usingController and
                self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
            local alertInfo = {
                condition = GEnums.FacSmartAlertType.CanBeOpened,
                targetTransform = self.m_smartAlertTargetTransformCache.state,
                defaultOpen = checkOpen
            }
            return true, alertInfo
        end
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertNoPowerWithoutDiffuserCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.NotInPowerNet then
        return false
    end
    if self.m_uiInfo.inPowerRangeDiffusers.Count <= 0 then
        local checkOpen = DeviceInfo.usingController and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.NoPowerWithoutDiffuser,
            targetTransform = self.m_smartAlertTargetTransformCache.state,
            defaultOpen = checkOpen
        }
        return true, alertInfo
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertNoPowerWithDiffuserCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.NotInPowerNet then
        return false
    end
    if self.m_uiInfo.inPowerRangeDiffusers.Count > 0 then
        local checkOpen = DeviceInfo.usingController and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.NoPowerWithDiffuser,
            targetTransform = self.m_smartAlertTargetTransformCache.state,
            defaultOpen = checkOpen
        }
        return true, alertInfo
    end
    return false
end

FacMachineCrafterCtrl._CheckAlertNoPowerCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.NoPower then
        return false
    end
    local checkOpen = DeviceInfo.usingController and
        self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
    local alertInfo = {
        condition = GEnums.FacSmartAlertType.NoPower,
        targetTransform = self.m_smartAlertTargetTransformCache.state,
        defaultOpen = checkOpen
    }
    return true, alertInfo
end

FacMachineCrafterCtrl._CheckAlertFillingNeedUnlockGasCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Idle or self.m_smartAlertConditionDataCache.fillingAndDismantlerUnlockGasMode then
        return false
    end







    if self.m_uiInfo.nodeHandler.templateId == "filling_powder_mc_1" then
        local inPipeInfoList = FactoryUtils.getBuildingPortState(self.m_nodeId, true)
        if inPipeInfoList then
            for pipeIndex, pipeInfo in ipairs(inPipeInfoList) do
                if pipeInfo.isBlock then
                    local blockGas = false
                    local liquidCache = self.m_uiInfo:GetCache(1, true, true)
                    if liquidCache then
                        if liquidCache.blockedMismatchItems.Count == 0 then
                            if liquidCache.items.Count > 0 then
                                for itemId, itemCount in cs_pairs(liquidCache.items) do
                                    local facItemSuccess, facItemData = Tables.factoryItemTable:TryGetValue(itemId)
                                    if facItemSuccess then
                                        if itemCount < facItemData.buildingBufferStackLimit then
                                            blockGas = true
                                        end
                                    end
                                end
                            else
                                blockGas = true
                            end
                        else
                            for i = 0, liquidCache.blockedMismatchItems.Count - 1 do
                                if Tables.gasTable:ContainsKey(liquidCache.blockedMismatchItems[i].itemId) then
                                    blockGas = true
                                    break
                                end
                            end
                        end
                    end
                    if blockGas then
                        local checkOpen = DeviceInfo.usingController and
                            self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                        local alertInfo = {
                            condition = GEnums.FacSmartAlertType.FillingNeedUnlockGas,
                            targetTransform = self.m_smartAlertTargetTransformCache.inPipe[1],
                            defaultOpen = checkOpen
                        }
                        return true, alertInfo
                    end
                end
            end
        end
    end

    return false
end

FacMachineCrafterCtrl._CheckAlertDismantlerNeedUnlockGasCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Idle or self.m_smartAlertConditionDataCache.fillingAndDismantlerUnlockGasMode then
        return false
    end






    if self.m_uiInfo.nodeHandler.templateId == "dismantler_1" then
        local normalCache = self.m_uiInfo:GetCache(1, true, false)
        if normalCache and normalCache.items.Count > 0 then
            for itemId, _ in cs_pairs(normalCache.items) do
                if FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas) then
                    local checkOpen = DeviceInfo.usingController and
                        self.view.cacheArea:CheckRepoNaviTargetTopLayer(true) and
                        self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
                    local alertInfo = {
                        condition = GEnums.FacSmartAlertType.DismantlerNeedUnlockGas,
                        targetTransform = self.m_smartAlertTargetTransformCache.normalInput[1][1],
                        defaultOpen = checkOpen
                    }
                    return true, alertInfo
                end
            end
        end
    end

    return false
end

FacMachineCrafterCtrl._CheckAlertActivatorCostInsufficientCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.InActive then
        return false
    end





    if self.m_uiInfo.activatorCost ~= nil and self.view.activatorNodes ~= nil then
        if self.m_uiInfo.activatorCost.currentBufCnt * 6 < self.m_activatorNeedCount then
            local checkOpen = DeviceInfo.usingController and
                self.view.activatorNodes.naviGroup.IsTopLayer and
                self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
            local alertInfo = {
                condition = GEnums.FacSmartAlertType.ActivatorCostInsufficient,
                targetTransform = self.view.activatorNodes.item.transform,
                args = {self.m_consumeItemName},
                defaultOpen = checkOpen
            }
            return true, alertInfo
        end
    end

    return false
end

FacMachineCrafterCtrl._CheckAlertFormulaNeedEnvCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Idle or string.isEmpty(self.m_lastProgressFormulaId) then
        return false
    end





    local machineCraftData = Tables.factoryMachineCrafterTable:GetValue(self.m_uiInfo.buildingId)
    local formulaGroupId
    for index = 0, machineCraftData.modeMap.Count - 1 do
        local mapData = machineCraftData.modeMap[index]
        if mapData ~= nil and mapData.modeName == self.m_uiInfo.formulaMan.currentMode then
            formulaGroupId = mapData.groupName
            break
        end
    end
    local craftInfo = FactoryUtils.parseMachineCraftData(self.m_lastProgressFormulaId, formulaGroupId)
    if craftInfo.env ~= nil and craftInfo.env ~= GEnums.FacEnvGenEnvType.None and craftInfo.env:GetHashCode() ~= self.view.buildingCommon.currEnvState then
        local envName = Language["ui_fac_vaporizer_env_" .. string.lower(craftInfo.env:ToString())]
        local checkOpen = DeviceInfo.usingController and
            not self.view.buildingCommon.view.buttonsNaviGroup.IsTopLayer and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.FormulaNeedEnv,
            targetTransform = self.view.buildingCommon.view.envStateController.transform,
            args = {envName},
            defaultOpen = checkOpen
        }
        return true, alertInfo
    end

    return false
end




HL.Commit(FacMachineCrafterCtrl)
