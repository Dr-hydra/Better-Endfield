local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacSewageTreatExporter

local ExportInfoState = {
    Paused = "Paused",
    Processing = "Processing",
}

local ProduceInfoState = {
    Paused = "Paused",
    Processing = "Processing",
}


local PROGRESS_AMOUNT_TWEEN_DURATION = 0.3
local INVALID_TIME_TEXT = "--"

FacSewageTreatExporterCtrl = HL.Class('FacSewageTreatExporterCtrl', uiCtrl.UICtrl)





FacSewageTreatExporterCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacSewageTreatExporterCtrl.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_SewageTreatExport)

FacSewageTreatExporterCtrl.m_treatItemId = HL.Field(HL.String) << ""

FacSewageTreatExporterCtrl.m_produceItemId = HL.Field(HL.String) << ""

FacSewageTreatExporterCtrl.m_produceItemData = HL.Field(HL.Table)

FacSewageTreatExporterCtrl.m_lastValidItemId = HL.Field(HL.String) << ""

FacSewageTreatExporterCtrl.m_isItemDirty = HL.Field(HL.Boolean) << false

FacSewageTreatExporterCtrl.m_updateThread = HL.Field(HL.Thread)

FacSewageTreatExporterCtrl.m_exportProgressTween = HL.Field(HL.Userdata)

FacSewageTreatExporterCtrl.m_lastProgressFillAmount = HL.Field(HL.Number) << -1

FacSewageTreatExporterCtrl.m_exportConsumeItemCount = HL.Field(HL.Number) << -1

FacSewageTreatExporterCtrl.m_exportProduceItemCount = HL.Field(HL.Number) << -1

FacSewageTreatExporterCtrl.m_currExportInfoState = HL.Field(HL.String) << ""


FacSewageTreatExporterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo

    self:_InitSewageTreatExportStaticData()

    self.view.inventoryArea:InitInventoryArea({
        customOnUpdateCell = function(cell, itemBundle)
            self:_RefreshInventoryItemCell(cell, itemBundle)
        end,
        onStateChange = function(state)
            self:_RefreshNaviGroupSwitcherInfos()
        end,
        hasFluidInCache = true,
    })

    local crafts = FactoryUtils.getBuildingCrafts(self.m_buildingInfo.buildingId)
    self.view.facCacheRepository:InitFacCacheRepository({
        cache = self.m_buildingInfo.fluidCache,
        isInCache = false,
        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid,
        cacheIndex = 1,
        slotCount = 1,
        formulaId = crafts[1].craftId,
        fakeFormulaDataList = crafts,
        outRepoCanDrop = true,
    })

    self.view.facCachePipe:InitFacCachePipe(self.m_buildingInfo, {
        useSinglePipe = true,
    })

    self.view.buildingCommon:InitBuildingCommon(self.m_buildingInfo, {
        onStateChanged = function(state)
            self:_RefreshChangeState(state)
            self:_RefreshSewageTreatExporterTargetFormula(state)
            if state == GEnums.FacBuildingState.Idle then
                self:_ClearSewageTreatExporterItemData()
            end
        end,
    })

    self:_InitExportInfo()
    self:_InitSewageTreatExporterFormulaNode()
    self:_InitSewageTreatExporterUpdateThread()

    self:_InitFacSewageTreatExporterController()

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(self.m_buildingInfo.nodeId)
end

FacSewageTreatExporterCtrl.OnClose = HL.Override() << function(self)
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_buildingInfo.nodeId)

    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)

    if self.m_exportProgressTween ~= nil then
        self.m_exportProgressTween:Kill(false)
        self.m_exportProgressTween = nil
    end
end

FacSewageTreatExporterCtrl._InitSewageTreatExportStaticData = HL.Method() << function(self)
    local importSuccess, importCfg = Tables.factorySewageTreatImportTable:TryGetValue(FacConst.FAC_SEWAGE_TREAT_IMPORTER_BUILDING_ID)
    if importSuccess then
        for index = 0, importCfg.liquidable.Count - 1 do
            local itemId = importCfg.liquidable[index]
            self.m_treatItemId = itemId
        end
    end

    local exportSuccess, exportCfg = Tables.factorySewageTreatExportTable:TryGetValue(self.m_buildingInfo.buildingId)
    if exportSuccess then
        self.m_produceItemId = exportCfg.productItemId
        self.m_exportConsumeItemCount = exportCfg.countCost
        self.m_exportProduceItemCount = exportCfg.countProduce
    end
end

FacSewageTreatExporterCtrl._InitSewageTreatExporterUpdateThread = HL.Method() << function(self)
    self:_UpdateAndRefreshAll()
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateAndRefreshAll()
        end
    end)
end

FacSewageTreatExporterCtrl._UpdateAndRefreshAll = HL.Method() << function(self)
    self:_UpdateSewageTreatExporterCacheItemData()
    self:_RefreshExportInfo()
    self:_RefreshProduceInfo()
    if self.m_isItemDirty then
        self:_RefreshSewageTreatExporterTargetFormula()
        self.m_isItemDirty = false
    end
end




FacSewageTreatExporterCtrl._UpdateSewageTreatExporterCacheItemData = HL.Method() << function(self)
    if self.m_produceItemData == nil then
        self.m_produceItemData = {}
    end

    self.m_produceItemData.id = ""
    self.m_produceItemData.count = 0
    for itemId, itemCount in pairs(self.m_buildingInfo.fluidCache.items) do
        self.m_produceItemData.id = itemId
        self.m_produceItemData.count = itemCount
    end

    if string.isEmpty(self.m_produceItemData.id) then
        self.m_produceItemData.id = self.m_buildingInfo.produceItemId
    end

    if self.m_lastValidItemId ~= self.m_produceItemData.id then
        self.m_isItemDirty = true
    end
    self.m_lastValidItemId = self.m_produceItemData.id
end

FacSewageTreatExporterCtrl._ClearSewageTreatExporterItemData = HL.Method() << function(self)
    self.m_lastValidItemId = ""
    self.m_produceItemData = {
        id = "",
        count = 0,
    }
    self.m_isItemDirty = true
end






FacSewageTreatExporterCtrl._RefreshInventoryItemCell = HL.Method(HL.Userdata, HL.Any) << function(self, cell, itemBundle)
    if cell == nil or itemBundle == nil then
        return
    end


    local itemId = itemBundle.id
    local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isBottle = isEmptyBottle or isFullBottle
    local isEmpty = string.isEmpty(itemBundle.id)
    local needMask = (not isBottle or not isEmptyBottle) and not isEmpty
    cell.view.forbiddenMask.gameObject:SetActive(needMask)
    cell.view.dragItem.enabled = isEmptyBottle and not isEmpty
    cell.view.dropItem.enabled = not needMask
end






FacSewageTreatExporterCtrl._InitExportInfo = HL.Method() << function(self)

    local exportInfoNode = self.view.exportInfoNode
    exportInfoNode.item:InitItem({ id = self.m_treatItemId }, true)
    local itemData = Tables.itemTable:GetValue(self.m_treatItemId)
    exportInfoNode.itemNameTxt.text = itemData.name
end

FacSewageTreatExporterCtrl._RefreshExportInfo = HL.Method() << function(self)
    local exportInfoNode = self.view.exportInfoNode

    local speedCount = self.m_buildingInfo.sewageTreatProduct.lastSecCost
    exportInfoNode.unitNumTxt.text = string.format("%d", speedCount)

    local currState = speedCount <= 0 and ExportInfoState.Paused or ExportInfoState.Processing
    if self.m_currExportInfoState ~= currState then
        exportInfoNode.stateController:SetState(currState)
        local animName = currState == ExportInfoState.Processing and "facsewagetreatleftarrow_loop" or "facsewagetreatleftarrow_default"
        exportInfoNode.arrowAnim:PlayWithTween(animName)
    end
    self.m_currExportInfoState = currState

    exportInfoNode.pausedIcon.gameObject:SetActive(speedCount <= 0 or self.view.buildingCommon.lastState == GEnums.FacBuildingState.Blocked)

    local progressAmount = self.m_buildingInfo.sewageTreatProduct.currentProgress / self.m_buildingInfo.totalProgress
    if self.m_lastProgressFillAmount < 0 then

        exportInfoNode.barFillImage.fillAmount = progressAmount
    else
        if self.m_lastProgressFillAmount ~= progressAmount then
            if self.m_lastProgressFillAmount ~= 1 and self.m_lastProgressFillAmount > progressAmount then
                progressAmount = 1
            end

            if self.m_exportProgressTween ~= nil then
                self.m_exportProgressTween:Kill(false)
                self.m_exportProgressTween = nil
            end

            self.m_exportProgressTween = DOTween.To(function()
                return exportInfoNode.barFillImage.fillAmount
            end, function(amount)
                exportInfoNode.barFillImage.fillAmount = amount
            end, progressAmount, PROGRESS_AMOUNT_TWEEN_DURATION):OnComplete(function()
                self.m_exportProgressTween = nil
            end)
        end
    end
    self.m_lastProgressFillAmount = progressAmount
end






FacSewageTreatExporterCtrl._RefreshProduceInfo = HL.Method() << function(self)
    local produceInfoNode = self.view.produceInfoNode

    produceInfoNode.unitNumTxt.text = tostring(1)

    local currConsumeSpeedCount = self.m_buildingInfo.sewageTreatProduct.lastSecCost
    if currConsumeSpeedCount <= 0 or self.view.buildingCommon.lastState == GEnums.FacBuildingState.Blocked then
        produceInfoNode.stateController:SetState(ProduceInfoState.Paused)
        produceInfoNode.timeNumberTxt.text = INVALID_TIME_TEXT
    else
        produceInfoNode.stateController:SetState(ProduceInfoState.Processing)
        local currProduceNeedTime = self.m_exportConsumeItemCount / currConsumeSpeedCount
        local currNormalizedProduceNeedTime = currProduceNeedTime / self.m_exportProduceItemCount
        produceInfoNode.timeNumberTxt.text = string.format("%d", math.ceil(currNormalizedProduceNeedTime))
    end
end

FacSewageTreatExporterCtrl._RefreshChangeState = HL.Method(HL.Userdata) << function(self, state)
    FactoryUtils.refreshStateNodeByState(self.view.facStateNode, self.view.produceInfoNode, state)
end






FacSewageTreatExporterCtrl._InitSewageTreatExporterFormulaNode = HL.Method() << function(self)
    self.view.formulaNode:InitFormulaNode(self.m_buildingInfo)
    self:_RefreshSewageTreatExporterTargetFormula()
end

FacSewageTreatExporterCtrl._RefreshSewageTreatExporterTargetFormula = HL.Method(HL.Opt(HL.Userdata)) << function(self, state)
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






FacSewageTreatExporterCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacSewageTreatExporterCtrl._InitFacSewageTreatExporterController = HL.Method() << function(self)
    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)

    self:_RefreshNaviGroupSwitcherInfos()

    self.view.contentNaviGroup.getDefaultSelectableFunc = function()
        return self.view.facCacheRepository:GetFirstSlotNaviTarget()
    end
    self.view.contentNaviGroup:NaviToThisGroup()
end

FacSewageTreatExporterCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
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




HL.Commit(FacSewageTreatExporterCtrl)
