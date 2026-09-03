local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPowerStation
FacPowerStationCtrl = HL.Class('FacPowerStationCtrl', uiCtrl.UICtrl)







FacPowerStationCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacPowerStationCtrl.m_nodeId = HL.Field(HL.Any)

FacPowerStationCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_BurnPower)

FacPowerStationCtrl.m_powerInfo = HL.Field(HL.Userdata)

FacPowerStationCtrl.m_curBurningFuelId = HL.Field(HL.String) << ""



FacPowerStationCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {
        onStateChanged = function(state)
            self.view.facProgressNode:SwitchAudioPlayingState(state == GEnums.FacBuildingState.Normal)
        end
    })
    self.view.inventoryArea:InitInventoryArea({
        onStateChange = function()
            self:_RefreshNaviGroupSwitcherInfos()
        end,
        customSetActionMenuArgs = function(actionMenuArgs)
            actionMenuArgs.cacheRepo = self.view.facCacheRepository
        end
    })
    self.view.inventoryArea:LockInventoryArea(FactoryUtils.isBuildingInventoryLocked(nodeId))

    self.view.facCacheRepository:InitFacCacheRepository({
        cache = self.m_uiInfo.fuelCache,
        isInCache = true,
        cacheIndex = 1,
        slotCount = 1,
        onRepoInitializeFinish = function()
            self.view.cacheNaviGroup:NaviToThisGroup()
        end
    })
    self.view.facCacheBelt:InitFacCacheBelt(self.m_uiInfo, {
        noGroup = true,
    })

    self.view.formulaNode:InitFormulaNode(self.m_uiInfo)

    self.view.facProgressNode:InitFacProgressNode(-1, -1)

    self:_InitPowerInfo()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_RefreshPowerInfo()
        end
    end)

    self:_UpdateProgress()
    self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_UpdateProgress()
        end
    end)

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)

    self:_InitPowerStationController()
end

FacPowerStationCtrl.OnClose = HL.Override() << function(self)
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_nodeId)
end

FacPowerStationCtrl._InitPowerInfo = HL.Method() << function(self)
    self.m_powerInfo = FactoryUtils.getCurRegionPowerInfo()
    self.view.maxRestPowerText.text = string.format("/%d", self.m_powerInfo.powerSaveMax)
    self:_RefreshPowerInfo()
end

FacPowerStationCtrl._RefreshPowerInfo = HL.Method() << function(self)
    local powerInfo = self.m_powerInfo

    local isFuelEmpty = string.isEmpty(self.m_uiInfo.burningItemId)
    if not isFuelEmpty then
        local data = Tables.factoryFuelItemTable:GetValue(self.m_uiInfo.burningItemId)
        self.view.currentPowerText.text = string.format("%d", data.powerProvide)
    end
    self.view.currentPowerInfoNode.gameObject:SetActive(not isFuelEmpty)
    self.view.currentPowerInfoEmptyNode.gameObject:SetActive(isFuelEmpty)

    local isPowerGen = powerInfo.powerGen > 0
    if isPowerGen then
        self.view.allProvidePowerText.text = string.format("%d", powerInfo.powerGen)
    end
    self.view.allProvidePowerInfoNode.gameObject:SetActive(isPowerGen)
    self.view.allProvidePowerInfoEmptyNode.gameObject:SetActive(not isPowerGen)

    local isPowerSaved = powerInfo.powerSaveCurrent > 0
    if isPowerSaved then
        self.view.restPowerText.text = string.format("%d", powerInfo.powerSaveCurrent)
    end
    self.view.restPowerInfoNode.gameObject:SetActive(isPowerSaved)
    self.view.restPowerInfoEmptyNode.gameObject:SetActive(not isPowerSaved)
end

FacPowerStationCtrl._UpdateProgress = HL.Method() << function(self)
    local fuelId = self.m_uiInfo.burningItemId
    local isFuelEmpty = string.isEmpty(fuelId)
    local isBurningFuelIdDirty = self.m_curBurningFuelId ~= fuelId

    self.m_curBurningFuelId = fuelId

    local data, totalProgress
    if not isFuelEmpty then
        data = Tables.factoryFuelItemTable:GetValue(fuelId)
        totalProgress = data.fuelEnergy * FacConst.CRAFT_PROGRESS_MULTIPLIER
    end

    if isBurningFuelIdDirty then
        if isFuelEmpty then
            self.view.facProgressNode:InitFacProgressNode(-1, -1)
            self.view.formulaNode:RefreshDisplayFormula()
            self.view.facProgressNode:SwitchAudioPlayingState(false)
        else

            local productionTime
            if self.m_uiInfo.burnPower.progressDecreasePerMS > 0 then
                productionTime = totalProgress / (self.m_uiInfo.burnPower.progressDecreasePerMS * 1000)
            else
                productionTime = -1
            end
            self.view.facProgressNode:InitFacProgressNode(productionTime, totalProgress)

            local targetCraftInfo = FactoryUtils.getBuildingProcessingCraft(self.m_uiInfo)
            self.view.formulaNode:RefreshDisplayFormula(targetCraftInfo)
            self.view.facProgressNode:SwitchAudioPlayingState(true)
        end
    end

    if not isFuelEmpty then
        self.view.facProgressNode:UpdateProgress(totalProgress - self.m_uiInfo.currentProgress)
    end
end



FacPowerStationCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacPowerStationCtrl._InitPowerStationController = HL.Method() << function(self)
    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)
    self:_RefreshNaviGroupSwitcherInfos()
end

FacPowerStationCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {
        { naviGroup = self.view.cacheNaviGroup },
    }
    self.view.inventoryArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end




HL.Commit(FacPowerStationCtrl)
