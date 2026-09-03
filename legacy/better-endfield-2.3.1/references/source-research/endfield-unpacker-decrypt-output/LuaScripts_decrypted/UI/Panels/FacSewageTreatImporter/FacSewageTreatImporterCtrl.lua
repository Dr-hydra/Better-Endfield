local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacSewageTreatImporter

local InfoState = {
    Processing = "Processing",
    Paused = "Paused",
}

FacSewageTreatImporterCtrl = HL.Class('FacSewageTreatImporterCtrl', uiCtrl.UICtrl)





FacSewageTreatImporterCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacSewageTreatImporterCtrl.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_SewageTreatImport)

FacSewageTreatImporterCtrl.m_updateThread = HL.Field(HL.Thread)

FacSewageTreatImporterCtrl.m_validLiquidIds = HL.Field(HL.Table)

FacSewageTreatImporterCtrl.m_sewageItemData = HL.Field(HL.Table)

FacSewageTreatImporterCtrl.m_lastValidItemId = HL.Field(HL.String) << ""

FacSewageTreatImporterCtrl.m_lastConsumeItemId = HL.Field(HL.String) << ""

FacSewageTreatImporterCtrl.m_isItemDirty = HL.Field(HL.Boolean) << false

FacSewageTreatImporterCtrl.m_treatItemId = HL.Field(HL.String) << ""

FacSewageTreatImporterCtrl.m_progressInitThread = HL.Field(HL.Thread)

FacSewageTreatImporterCtrl.m_progressUpdateThread = HL.Field(HL.Thread)

FacSewageTreatImporterCtrl.m_needRefreshProgress = HL.Field(HL.Boolean) << false

FacSewageTreatImporterCtrl.m_isPipeBlocked = HL.Field(HL.Boolean) << false

FacSewageTreatImporterCtrl.m_currTreatInfoState = HL.Field(HL.String) << ""

FacSewageTreatImporterCtrl.m_dropHelper = HL.Field(HL.Forward('UIDropHelper'))


FacSewageTreatImporterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo
    self.view.tipsTextNode.gameObject:SetActive(false)
    self.view.facStateNode.gameObject:SetActive(false)

    self:_InitSewageTreatImportStaticData()

    self.view.inventoryArea:InitInventoryArea({
        customOnUpdateCell = function(cell, itemBundle)
            self:_RefreshInventoryItemCell(cell, itemBundle)
        end,
        onStateChange = function(state)
            self:_RefreshNaviGroupSwitcherInfos()
        end,
        hasFluidInCache = true,
    })

    self.view.facCachePipe:InitFacCachePipe(self.m_buildingInfo, {
        useSinglePipe = true,
        stateRefreshCallback = function(pipeInfo)
            self:_OnPipeStateChanged(pipeInfo)
        end
    })

    local crafts = FactoryUtils.getBuildingCrafts(self.m_buildingInfo.buildingId)
    self.view.facCacheRepository:InitFacCacheRepository({
        cache = self.m_buildingInfo.fluidCache,
        isInCache = true,
        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid,
        cacheIndex = 1,
        slotCount = 1,
        formulaId = crafts[1].craftId,
        fakeFormulaDataList = crafts
    })

    self.view.buildingCommon:InitBuildingCommon(self.m_buildingInfo, {
        onStateChanged = function(state)
            self:_RefreshChangeState(state)
            self:_RefreshSewageTreatImporterTargetFormula(state)
            if state == GEnums.FacBuildingState.Idle then
                self:_ClearSewageTreatImporterItemData()
            end
            self:_RefreshSewageTreatImporterTipsVisibleState(state)
        end,
        nameIndex = self:_GetSewageTreatImporterIndex()
    })

    self:_InitSewageTreatImporterFormulaNode()
    self:_InitTreatConsumeInfo()
    self:_InitSewageTreatImporterUpdateThread()
    self:_InitSewageTreatImporterProgressInitThread()

    self:_InitFacSewageTreatImporterController()

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(self.m_buildingInfo.nodeId)
end

FacSewageTreatImporterCtrl.OnClose = HL.Override() << function(self)
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_buildingInfo.nodeId)

    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)
    self.m_progressUpdateThread = self:_ClearCoroutine(self.m_progressUpdateThread)
end

FacSewageTreatImporterCtrl._InitSewageTreatImportStaticData = HL.Method() << function(self)
    self.m_validLiquidIds = {}
    local success, cfg = Tables.factorySewageTreatImportTable:TryGetValue(self.m_buildingInfo.buildingId)
    if not success then
        return
    end

    for index = 0, cfg.liquidable.Count - 1 do
        local itemId = cfg.liquidable[index]
        self.m_validLiquidIds[itemId] = true
        self.m_treatItemId = itemId
    end
end

FacSewageTreatImporterCtrl._InitSewageTreatImporterUpdateThread = HL.Method() << function(self)
    self:_UpdateAndRefreshAll()
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateAndRefreshAll()
        end
    end)
end

FacSewageTreatImporterCtrl._GetSewageTreatImporterIndex = HL.Method().Return(HL.Number) << function(self)
    local instKey = self.m_buildingInfo.nodeHandler.instKey
    for _, cfg in pairs(Tables.factorySewageTreatPlantStoreTable) do
        for levelIndex = 0, cfg.levelList.Count - 1 do
            local levelData = cfg.levelList[levelIndex]
            for paramIndex = 0, levelData.actionParams.Count - 1 do
                if levelData.actionParams[paramIndex] == instKey then
                    return LuaIndex(levelIndex)
                end
            end
        end
    end
    return 1
end

FacSewageTreatImporterCtrl._UpdateAndRefreshAll = HL.Method() << function(self)
    self:_UpdateSewageTreatImporterCacheItemData()
    self:_RefreshTreatConsumeInfo()
    if self.m_isItemDirty then
        self:_RefreshSewageTreatImporterTargetFormula()
        self:_RefreshSewageTreatImporterProgressNode()
        self:_RefreshSewageTreatImporterTipsVisibleState()
        self.m_isItemDirty = false
    end
end




FacSewageTreatImporterCtrl._UpdateSewageTreatImporterCacheItemData = HL.Method() << function(self)
    if self.m_sewageItemData == nil then
        self.m_sewageItemData = {}
    end

    self.m_sewageItemData.id = ""
    self.m_sewageItemData.count = 0

    for itemId, itemCount in pairs(self.m_buildingInfo.fluidCache.items) do
        self.m_sewageItemData.id = itemId
        self.m_sewageItemData.count = itemCount
    end

    if string.isEmpty(self.m_sewageItemData.id) then
        self.m_sewageItemData.id = self.m_buildingInfo.consumeItemId
    end

    if self.m_lastValidItemId ~= self.m_sewageItemData.id then
        self.m_isItemDirty = true
    end
    if self.m_lastConsumeItemId ~= self.m_buildingInfo.consumeItemId then
        self.m_isItemDirty = true
    end

    self.m_lastValidItemId = self.m_sewageItemData.id
    self.m_lastConsumeItemId = self.m_buildingInfo.consumeItemId
end

FacSewageTreatImporterCtrl._ClearSewageTreatImporterItemData = HL.Method() << function(self)
    self.m_lastValidItemId = ""
    self.m_sewageItemData = {
        id = "",
        count = 0,
    }
    self.m_isItemDirty = true
end

FacSewageTreatImporterCtrl._OnPipeStateChanged = HL.Method(HL.Table) << function(self, pipeInfo)
    if pipeInfo == nil then
        return
    end

    self.m_isPipeBlocked = pipeInfo.isBlock
    self:_RefreshSewageTreatImporterTipsVisibleState()
end

FacSewageTreatImporterCtrl._RefreshSewageTreatImporterTipsVisibleState = HL.Method(HL.Opt(HL.Userdata)) << function(self, state)
    if self.m_sewageItemData == nil then
        return
    end

    local cacheItemId = self.m_sewageItemData.id
    state = state and state or self.view.buildingCommon.lastState
    local isValidState = state and
        state ~= GEnums.FacBuildingState.Closed and
        state ~= GEnums.FacBuildingState.NotInPowerNet and
        state ~= GEnums.FacBuildingState.NoPower
    local needShowTips = self.m_isPipeBlocked and
        isValidState and
        string.isEmpty(cacheItemId) and
        not self.m_validLiquidIds[cacheItemId]
    UIUtils.PlayAnimationAndToggleActive(self.view.tipsTextNode.animationWrapper, needShowTips)
end






FacSewageTreatImporterCtrl._RefreshInventoryItemCell = HL.Method(HL.Userdata, HL.Any) << function(self, cell, itemBundle)
    if cell == nil or itemBundle == nil then
        return
    end


    local itemId = itemBundle.id
    local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isBottle = isEmptyBottle or isFullBottle
    local isEmpty = string.isEmpty(itemBundle.id)
    local needMask = not isBottle and not isEmpty
    if isFullBottle then
        local fullBottleData = Tables.fullBottleTable[itemId]
        local liquidItemId = fullBottleData.liquidId
        needMask = needMask or not self.m_validLiquidIds[liquidItemId]
    end
    if needMask then
        cell.view.forbiddenMask.gameObject:SetActive(true)
    end
    cell.view.dragItem.enabled = not needMask and not isEmpty
    cell.view.dropItem.enabled = not needMask


    if isBottle then
        cell.item.customChangeActionMenuFunc = function(actionMenuInfos)
            local dropAction = {}
            if isEmptyBottle then
                dropAction.text = Language.LUA_ITEM_ACTION_FILL_LIQUID
            else
                dropAction.text = Language.LUA_ITEM_ACTION_DUMP_LIQUID
            end
            dropAction.action = function()
                local dragHelper = cell.item.actionMenuArgs.dragHelper
                self.view.facCacheRepository:TryDropItemToRepository(dragHelper)
            end
            table.insert(actionMenuInfos, 1, dropAction)
        end
    end
end






FacSewageTreatImporterCtrl._InitTreatConsumeInfo = HL.Method() << function(self)

    local treatInfoNode = self.view.treatInfoNode
    treatInfoNode.item:InitItem({ id = self.m_treatItemId }, true)
    local itemData = Tables.itemTable:GetValue(self.m_treatItemId)
    treatInfoNode.itemNameTxt.text = itemData.name
end

FacSewageTreatImporterCtrl._RefreshTreatConsumeInfo = HL.Method() << function(self)
    local treatInfoNode = self.view.treatInfoNode
    local consumeItemId = self.m_buildingInfo.consumeItemId
    local infoState = InfoState.Paused
    if not string.isEmpty(consumeItemId) and self.view.buildingCommon.lastState == GEnums.FacBuildingState.Normal then
        infoState = InfoState.Processing
    end
    if infoState == self.m_currTreatInfoState then
        return
    end


    treatInfoNode.stateController:SetState(infoState)

    local animName = infoState == InfoState.Processing and "facsewagetreatleftarrow_loop" or "facsewagetreatleftarrow_default"
    treatInfoNode.arrowAnim:PlayWithTween(animName)

    self.m_currTreatInfoState = infoState
end






FacSewageTreatImporterCtrl._InitSewageTreatImporterProgressInitThread = HL.Method() << function(self)
    self:_UpdateSewageTreatImporterProgressInitializedState()
    self.m_progressInitThread = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_UpdateSewageTreatImporterProgressInitializedState()
        end
    end)
end

FacSewageTreatImporterCtrl._InitSewageTreatImporterProgressUpdateThread = HL.Method() << function(self)
    self:_RefreshSewageTreatImporterProgress()
    self.m_progressUpdateThread = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_RefreshSewageTreatImporterProgress()
        end
    end)
end

FacSewageTreatImporterCtrl._UpdateSewageTreatImporterProgressInitializedState = HL.Method() << function(self)
    if self.m_buildingInfo.fluidConsume.progressIncrPerMS == 0 then


        self.view.facProgressNode:InitFacProgressNode(0, 0, nil, nil, nil, nil, true)
        return
    end

    self:_RefreshSewageTreatImporterProgressNode()

    self:_InitSewageTreatImporterProgressUpdateThread()
    self.m_progressInitThread = self:_ClearCoroutine(self.m_progressInitThread)
end

FacSewageTreatImporterCtrl._RefreshSewageTreatImporterProgressNode = HL.Method() << function(self)
    if string.isEmpty(self.m_lastValidItemId) or self.m_buildingInfo.fluidConsume.progressIncrPerMS == 0 then
        self:_StopSewageTreatImporterProgressRefresh()
    else
        local totalProgress = self.m_buildingInfo.totalProgress
        local time = totalProgress / (self.m_buildingInfo.fluidConsume.progressIncrPerMS * 1000)
        self.view.facProgressNode:InitFacProgressNode(time, totalProgress, nil, nil, nil, nil, true)
        self.m_needRefreshProgress = true
    end
end

FacSewageTreatImporterCtrl._RefreshSewageTreatImporterProgress = HL.Method() << function(self)
    if not self.m_needRefreshProgress then
        return
    end

    self.view.facProgressNode:UpdateProgress(self.m_buildingInfo.fluidConsume.currentProgress)
end

FacSewageTreatImporterCtrl._StopSewageTreatImporterProgressRefresh = HL.Method() << function(self)
    self.view.facProgressNode:InitFacProgressNode(0, 0, nil, nil, nil, nil, true)
    self.m_needRefreshProgress = false
end

FacSewageTreatImporterCtrl._RefreshChangeState = HL.Method(HL.Userdata) << function(self, state)
    FactoryUtils.refreshStateNodeByState(self.view.facStateNode, self.view.facProgressNode, state)
end






FacSewageTreatImporterCtrl._InitSewageTreatImporterFormulaNode = HL.Method() << function(self)
    self.view.formulaNode:InitFormulaNode(self.m_buildingInfo)
    self:_RefreshSewageTreatImporterTargetFormula()
end

FacSewageTreatImporterCtrl._RefreshSewageTreatImporterTargetFormula = HL.Method(HL.Opt(HL.Userdata)) << function(self, state)
    local targetCraftInfo = FactoryUtils.getBuildingProcessingCraft(self.m_buildingInfo)
    if state == nil then
        state = self.view.buildingCommon.lastState
    end
    if state == GEnums.FacBuildingState.Closed then
        targetCraftInfo = nil
    end
    self.view.formulaNode:RefreshDisplayFormula(targetCraftInfo)
    self.view.facCacheRepository:UpdateRepositoryFormula(targetCraftInfo ~= nil and targetCraftInfo.craftId or "")
end






FacSewageTreatImporterCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacSewageTreatImporterCtrl._InitFacSewageTreatImporterController = HL.Method() << function(self)
    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)

    self:_RefreshNaviGroupSwitcherInfos()

    self.view.contentNaviGroup.getDefaultSelectableFunc = function()
        return self.view.facCacheRepository:GetFirstSlotNaviTarget()
    end
    self.view.contentNaviGroup:NaviToThisGroup()
end

FacSewageTreatImporterCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {
        {
            naviGroup = self.view.contentNaviGroup,
            text = Language.LUA_INV_NAVI_SWITCH_TO_MACHINE,
            forceDefault = true,
        }
    }
    self.view.inventoryArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end




HL.Commit(FacSewageTreatImporterCtrl)
