
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacGasMiner
FacGasMinerCtrl = HL.Class('FacGasMinerCtrl', uiCtrl.UICtrl)

local MAX_MINE_PROGRESS_EFFICIENCY = 100

FacGasMinerCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_Collector)

FacGasMinerCtrl.m_nodeId = HL.Field(HL.Any)

FacGasMinerCtrl.m_cache = HL.Field(HL.Userdata)

FacGasMinerCtrl.m_progressInitUpdateThread = HL.Field(HL.Thread)

FacGasMinerCtrl.m_progressUpdateThread = HL.Field(HL.Thread)





FacGasMinerCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacGasMinerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId


    self:_InitBasicInfo()


    self:_InitInventoryArea()


    self:_InitFacCacheRepository()


    self:_RefreshFormulaInfo()


    self:_InitProgressNode()

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {
        onStateChanged = function(state)
            self:_RefreshChangeState(state)
        end
    })


    self:_InitController()
end

FacGasMinerCtrl._InitInventoryArea = HL.Method() << function(self)
    self.view.inventoryArea:InitInventoryArea({
        customOnUpdateCell = function(cell, itemBundle)
            self:_RefreshInventoryItemCell(cell, itemBundle)
        end,
        customSetActionMenuArgs = function(actionMenuArgs)
            actionMenuArgs.cacheRepo = self.view.facCacheRepository
        end,
        onStateChange = function()
            self:_RefreshNaviGroupSwitcherInfos()
        end,
        hasGasInCache = true
    })
end

FacGasMinerCtrl._RefreshInventoryItemCell = HL.Method(HL.Userdata, HL.Any) << function(self, cell, itemBundle)
    if cell == nil or itemBundle == nil then
        return
    end


    local itemId = itemBundle.id
    local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
    if not isEmptyBottle then
        cell.view.forbiddenMask.gameObject:SetActiveIfNecessary(true)
        cell.view.dropItem.enabled = false
        cell.view.dragItem.enabled = false
    end
end

FacGasMinerCtrl._InitBasicInfo = HL.Method() << function(self)
    local protoData = self.m_uiInfo.protoData
    if (protoData == nil) or (protoData.mineNameId == nil) then
        self.view.infoNode.sourceNameText.text = ""
    else
        self.view.infoNode.sourceNameText.text = protoData.mineNameId:GetText()
    end

    local success, data = Tables.itemTable:TryGetValue(self.m_uiInfo.collectingItemId)
    if success then
        self.view.infoNode.itemNameText.text = data.name
    end

    local mineLevel = self.m_uiInfo.mineLevel
    local isHighEfficiency = mineLevel == CS.Beyond.Gameplay.LevelDoodadGroupData.EDoodadGroupLevel.LevelFour

    self.view.infoNode.highEfficiencyText.gameObject:SetActive(isHighEfficiency)
    self.view.infoNode.highNode.gameObject:SetActive(isHighEfficiency)
    self.view.infoNode.lowNode.gameObject:SetActive(not isHighEfficiency)
end

FacGasMinerCtrl._RefreshChangeState = HL.Method(HL.Userdata) << function(self, state)
    FactoryUtils.refreshStateNodeByState(self.view.facStateNode, self.view.facProgressNode, state)
end

FacGasMinerCtrl._InitFacCacheRepository = HL.Method() << function(self)
    self.view.facCacheRepository:InitFacCacheRepository({
        cache = self.m_uiInfo.cache,
        isInCache = true,
        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas,
        cacheIndex = 1,
        slotCount = 1,
        fakeFormulaDataList = FactoryUtils.getBuildingCrafts(self.m_uiInfo.buildingId),
        disableDump = true,
    })
    self.view.facCachePipe:InitFacCachePipe(self.m_uiInfo, {
        useSinglePipe = true,
    })
    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(self.m_uiInfo.nodeId)
end

FacGasMinerCtrl._RefreshFormulaInfo = HL.Method() << function(self)
    self.view.formulaNode:InitFormulaNode(self.m_uiInfo)
    local efficiency = self.m_uiInfo.speedPercentage
    local mineLevel = self.m_uiInfo.mineLevel
    local isHighEfficiency = mineLevel == CS.Beyond.Gameplay.LevelDoodadGroupData.EDoodadGroupLevel.LevelFour
    local targetCraftInfo = FactoryUtils.getBuildingProcessingCraft(self.m_uiInfo)
    if targetCraftInfo == nil then
        self.view.formulaNode:RefreshDisplayFormula()
        self.view.facCacheRepository:UpdateRepositoryFormula("")
        return
    end

    local timeColor = isHighEfficiency and
        self.view.config.HIGH_FORMULA_TIME_COLOR or
        self.view.config.NORMAL_FORMULA_TIME_COLOR

    if efficiency > 0 then
        local extraSpeed = MAX_MINE_PROGRESS_EFFICIENCY / efficiency
        targetCraftInfo.time = targetCraftInfo.time * extraSpeed
        self.view.formulaNode:RefreshDisplayFormula(targetCraftInfo, timeColor)
        self.view.formulaNode:SetExtraFormulaSpeed(extraSpeed)
    else
        self.view.formulaNode:RefreshDisplayFormula(targetCraftInfo)
    end

    self.view.facCacheRepository:UpdateRepositoryFormula(targetCraftInfo.craftId)
    self.view.facProgressNode.view.gameObject:SetActive(efficiency > 0)
end

FacGasMinerCtrl._InitProgressNode = HL.Method() << function(self)
    self:_UpdateProgressInitializedState()
    self.m_progressInitUpdateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateProgressInitializedState()
        end
    end)
end

FacGasMinerCtrl._UpdateProgressInitializedState = HL.Method() << function(self)
    if self.m_uiInfo.collector.progressIncreasePerMS == 0 then
        self.view.facProgressNode:InitFacProgressNode(0, 0)
        return
    end

    local totalProgress = self.m_uiInfo.totalProgress
    local time = totalProgress / (self.m_uiInfo.collector.progressIncreasePerMS * 1000)
    local colorStr = ""
    self.view.facProgressNode:InitFacProgressNode(time, totalProgress, colorStr)
    self.view.facProgressNode:UpdateProgress(self.m_uiInfo.currentProgress)
    self.view.facProgressNode:SwitchAudioPlayingState(true)
    self:_StartProgressUpdateThread()
    self.m_progressInitUpdateThread = self:_ClearCoroutine(self.m_progressInitUpdateThread)
end

FacGasMinerCtrl._StartProgressUpdateThread = HL.Method() << function(self)
    self.m_progressUpdateThread = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_UpdateProgressState()
        end
    end)
end

FacGasMinerCtrl._UpdateProgressState = HL.Method() << function(self)
    local currentProgress = self.m_uiInfo.currentProgress
    self.view.facProgressNode:UpdateProgress(currentProgress)
end



FacGasMinerCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacGasMinerCtrl._InitController = HL.Method() << function(self)
    self.view.facCacheRepository.view.slotListSelectableNaviGroup:NaviToThisGroup()

    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)

    self:_RefreshNaviGroupSwitcherInfos()
end

FacGasMinerCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {
        {
            naviGroup = self.view.facCacheRepository.view.slotListSelectableNaviGroup
        }
    }
    self.view.inventoryArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end



HL.Commit(FacGasMinerCtrl)
