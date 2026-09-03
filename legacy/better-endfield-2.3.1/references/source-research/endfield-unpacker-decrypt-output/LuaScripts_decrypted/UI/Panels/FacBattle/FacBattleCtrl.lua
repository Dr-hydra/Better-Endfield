local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacBattle
FacBattleCtrl = HL.Class('FacBattleCtrl', uiCtrl.UICtrl)

local ChargingMode = FacConst.BattleBuildingChargingMode

local CHARGING_MODE_TO_PANEL_MODE_MAP = {
    [ChargingMode.Battery] = "Battery",
    [ChargingMode.PowerNet] = "PowerNet",
    [ChargingMode.Overload] = "Overload",
    [ChargingMode.Closed] = "Closed",
    [ChargingMode.Shared] = "Shared",
}

local CHARGING_FORBIDDEN_TEXT_ID_MAP = {
    [ChargingMode.PowerNet] = "ui_fac_battle_building_forbid_battery_power",
    [ChargingMode.Overload] = "ui_fac_battle_building_forbid_battery_overload",
    [ChargingMode.Shared] = "ui_fac_battle_building_forbid_battery_shared",
}
local CHARGING_BATTERY_EMPTY_STATE_TEXT_ID = "ui_fac_battle_building_no_energy_source"
local CHARGING_BATTERY_STATE_TEXT_ID = "ui_fac_battle_building_energy_source_battery"
local CHARGING_POWER_NET_STATE_TEXT_ID = "ui_fac_battle_building_energy_source_power"
local CHARGING_SHARED_STATE_TEXT_ID = "ui_fac_battle_building_energy_source_shared"

local FILL_AMOUNT_TWEEN_DURATION = 0.5

FacBattleCtrl.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_Battle)

FacBattleCtrl.m_currentChargingMode = HL.Field(HL.Number) << 0

FacBattleCtrl.m_initialRefreshChargingMode = HL.Field(HL.Number) << -1

FacBattleCtrl.m_currentBuildingState = HL.Field(GEnums.FacBuildingState)

FacBattleCtrl.m_lastChargingEnergy = HL.Field(HL.Number) << 0

FacBattleCtrl.m_lastBatteryEnergy = HL.Field(HL.Number) << 0

FacBattleCtrl.m_chargingTweenInitialized = HL.Field(HL.Boolean) << false

FacBattleCtrl.m_chargingTween = HL.Field(HL.Userdata)

FacBattleCtrl.m_batteryTweenInitialized = HL.Field(HL.Boolean) << false

FacBattleCtrl.m_batteryTween = HL.Field(HL.Userdata)

FacBattleCtrl.m_batteryItemIdMap = HL.Field(HL.Table)

FacBattleCtrl.m_normalDescription = HL.Field(HL.String) << ""

FacBattleCtrl.m_overloadDescription = HL.Field(HL.String) << ""

FacBattleCtrl.m_updateThread = HL.Field(HL.Thread)

FacBattleCtrl.m_isInCharging = HL.Field(HL.Boolean) << false






FacBattleCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacBattleCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo

    self:_InitBattleStaticData()

    self.view.buildingCommon:InitBuildingCommon(self.m_buildingInfo, {
        onStateChanged = function(state)
            self.m_currentBuildingState = state
        end
    })

    self.m_initialRefreshChargingMode = self:_GetNextChargingMode()
    self.view.inventoryArea:InitInventoryArea({
        customOnUpdateCell = function(cell, itemBundle)
            self:_RefreshInventoryItemCell(cell, itemBundle)
        end,
        onStateChange = function()
            self:_RefreshNaviGroupSwitcherInfos()
        end
    })

    self.view.facCacheRepository:InitFacCacheRepository({
        cache = self.m_buildingInfo.cache,
        isInCache = true,
        cacheIndex = 1,
        slotCount = 1,
        onRepoInitializeFinish = function()
            self.view.cacheNaviGroup:NaviToThisGroup()
        end
    })

    self.view.autoFillButton.onClick:AddListener(function()
        self:_OnAutoFillButtonClicked()
    end)

    self:_InitBattleBasicContent()
    self:_InitBattleUpdateThread()
    self:_InitBattleController()
end

FacBattleCtrl.OnClose = HL.Override() << function(self)
    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)

    if self.m_chargingTween ~= nil then
        self.m_chargingTween:Kill(false)
        self.m_chargingTween = nil
    end
    if self.m_batteryTween ~= nil then
        self.m_batteryTween:Kill(false)
        self.m_batteryTween = nil
    end
end

FacBattleCtrl._InitBattleBasicContent = HL.Method() << function(self)
    local buildingId = self.m_buildingInfo.nodeHandler.templateId
    local success, battleBuildingData = Tables.factoryBattleTable:TryGetValue(buildingId)
    if not success then
        return
    end

    self.view.costText.text = string.format("%d", battleBuildingData.attackCost)

    self:_RefreshBatteryText()
end

FacBattleCtrl._InitBattleStaticData = HL.Method() << function(self)

    self.m_batteryItemIdMap = {}
    for itemId, energy in pairs(Tables.factoryBatteryItemTable) do
        self.m_batteryItemIdMap[itemId] = energy
    end

    local success, battleBuildingData = Tables.factoryBattleTable:TryGetValue(self.m_buildingInfo.nodeHandler.templateId)
    if success then
        self.m_normalDescription = battleBuildingData.normalDesc
        self.m_overloadDescription = battleBuildingData.overloadDesc
    end
end



FacBattleCtrl._InitBattleUpdateThread = HL.Method() << function(self)

    self.m_lastChargingEnergy = self.m_buildingInfo.battle.energyCurrent
    self.m_lastBatteryEnergy = self.m_buildingInfo.batteryBurn.energyCurrent


    self:_UpdateAndRefreshBattleChargingState()

    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateAndRefreshBattleChargingState()
        end
    end)
end

FacBattleCtrl._GetNextChargingMode = HL.Method().Return(HL.Number) << function(self)
    local nextChargingMode = self.m_currentChargingMode
    local powerNetPowerEnough = true
    local isOthersSocialBuilding = FactoryUtils.isOthersSocialBuilding(self.m_buildingInfo.nodeId)
    if isOthersSocialBuilding then
        nextChargingMode = ChargingMode.Shared
    elseif self.view.buildingCommon.lastState == GEnums.FacBuildingState.Closed then
        nextChargingMode = ChargingMode.Closed
    else
        if self.m_buildingInfo.battle.inOverloading then
            if self.m_currentChargingMode ~= ChargingMode.Overload then

                nextChargingMode = ChargingMode.Overload
            end
        else
            local isInPowerNet = self.m_buildingInfo.nodeHandler.power.inPower
            if isInPowerNet then
                local powerInfo = FactoryUtils.getCurRegionPowerInfo()
                if powerInfo ~= nil then
                    if powerInfo.powerSaveMax > 0 and powerInfo.powerSaveCurrent <= 0 and powerInfo.powerGen < powerInfo.powerCost and not FactoryUtils.getIsInBackupPower() then
                        isInPowerNet = false
                        powerNetPowerEnough = false
                    end
                end
            end
            if isInPowerNet then
                if self.m_currentChargingMode ~= ChargingMode.PowerNet then

                    nextChargingMode = ChargingMode.PowerNet
                end
            else
                if self.m_currentChargingMode ~= ChargingMode.Battery then

                    nextChargingMode = ChargingMode.Battery
                end
            end
        end
    end

    return nextChargingMode
end

FacBattleCtrl._GetIsBatteryCacheEnabledState = HL.Method(HL.Number).Return(HL.Boolean) << function(self, mode)
    local isInBatteryMode = mode == ChargingMode.Battery
    local isClosed = mode == ChargingMode.Closed
    return isInBatteryMode or isClosed
end

FacBattleCtrl._UpdateAndRefreshBattleChargingState = HL.Method() << function(self)
    local nextChargingMode = self:_GetNextChargingMode()

    if nextChargingMode ~= self.m_currentChargingMode then
        local needRefreshInventory = nextChargingMode == ChargingMode.Closed or self.m_currentChargingMode == ChargingMode.Closed
        self.m_currentChargingMode = nextChargingMode
        self:_RefreshChargingMode(nextChargingMode)



    end

    if self.m_currentChargingMode == ChargingMode.Battery then

        self:_RefreshBatteryState()
        if not powerNetPowerEnough then
            self:_RefreshBatteryText()
        end
    end

    self:_RefreshChargingEnergy()
    self:_RefreshIsInChargingState()
    self:_RefreshChargingAnimState()
end

FacBattleCtrl._RefreshChargingMode = HL.Method(HL.Number) << function(self, mode)
    local isInBatteryMode = mode == ChargingMode.Battery
    local isInPowerNet = mode == ChargingMode.PowerNet
    local isInOverload = mode == ChargingMode.Overload
    local isShared = mode == ChargingMode.Shared

    local stateName = isShared and "Disabled" or "Normal"
    self.view.itemStateCtrl:SetState(stateName)
    self.view.inventoryArea:SetState(stateName)


    local cacheSlotList = self.view.facCacheRepository:GetRepositorySlotList()
    local cacheEnabled = self:_GetIsBatteryCacheEnabledState(mode)
    for _, cacheSlot in ipairs(cacheSlotList) do
        cacheSlot:SetState(stateName)
        cacheSlot:RefreshSlotBlockState(not cacheEnabled)
    end


    if not isInBatteryMode and CHARGING_FORBIDDEN_TEXT_ID_MAP[mode] ~= nil then
        self.view.chargingForbiddenText.text = Language[CHARGING_FORBIDDEN_TEXT_ID_MAP[mode]]
    end
    if isInPowerNet then
        self.view.chargingStateText.text = Language[CHARGING_POWER_NET_STATE_TEXT_ID]
        self.view.normalTipsStateCtrl:SetState("Charging")
    end
    if isShared then
        self.view.chargingStateText.text = Language[CHARGING_SHARED_STATE_TEXT_ID]
        self.view.normalTipsStateCtrl:SetState("Charging")
    end
    local description = isInOverload and self.m_overloadDescription or self.m_normalDescription
    self.view.skillText:SetAndResolveTextStyle(description)


    self.view.buildingCommon.view.overloadStateNode.gameObject:SetActiveIfNecessary(isInOverload)
    self.view.buildingCommon.view.stateNode.gameObject:SetActiveIfNecessary(not isInOverload)


    if isInOverload then
        if not self.view.overloadTipsAnim.gameObject.activeSelf then
            self.view.overloadTipsAnim.gameObject:SetActive(true)
        end
    else
        if self.view.overloadTipsAnim.gameObject.activeSelf then
            UIUtils.PlayAnimationAndToggleActive(self.view.overloadTipsAnim, false)
        end
    end


    if string.isEmpty(self.view.mainController.curStateName) then
        self.view.energyAnim:ClearTween(false)
        self.view.energyAnim:PlayWithTween("facbattleclose_out")
        self.view.mainController:SetState(CHARGING_MODE_TO_PANEL_MODE_MAP[mode])
    else
        self.view.energyAnim:ClearTween(false)
        self.view.energyAnim:PlayWithTween("facbattleclose_in", function()
            self.view.mainController:SetState(CHARGING_MODE_TO_PANEL_MODE_MAP[mode])
            self.view.energyAnim:PlayWithTween("facbattleclose_out")
        end)
    end
end

FacBattleCtrl._RefreshBatteryState = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceRefresh)
    local energyCurrent = self.m_buildingInfo.batteryBurn.energyCurrent
    local energyLoaded = self.m_buildingInfo.batteryBurn.energyLoaded
    self.view.batteryNode.currentText.text = string.format("%d", energyCurrent)
    self.view.batteryNode.loadedText.text = string.format("%d", energyLoaded)

    local targetAmount = energyLoaded == 0 and 0 or energyCurrent / energyLoaded

    if self.m_batteryTweenInitialized then
        if self.m_lastBatteryEnergy ~= energyCurrent or forceRefresh then
            if (self.m_lastBatteryEnergy < energyCurrent and energyCurrent < energyLoaded) or forceRefresh then
                self.view.batteryNode.batteryFill.fillAmount = 1.0
            end
            if self.m_batteryTween ~= nil then
                self.m_batteryTween:Kill(false)
            end

            local duration = energyCurrent == energyLoaded and FILL_AMOUNT_TWEEN_DURATION / 4.0 or FILL_AMOUNT_TWEEN_DURATION
            self.m_batteryTween = DOTween.To(function()
                return self.view.batteryNode.batteryFill.fillAmount
            end, function(amount)
                self.view.batteryNode.batteryFill.fillAmount = amount
            end, targetAmount, duration)
        end
    else

        self.view.batteryNode.batteryFill.fillAmount = targetAmount
        self.m_batteryTweenInitialized = true
    end

    if self.m_lastBatteryEnergy > 0 and energyCurrent <= 0 then
        self:_RefreshBatteryText()
    end

    self.m_lastBatteryEnergy = energyCurrent
end

FacBattleCtrl._RefreshBatteryText = HL.Method() << function(self)
    local items = self.m_buildingInfo.cache.items
    local textId = CHARGING_BATTERY_EMPTY_STATE_TEXT_ID
    local normalTipsStateName = "Empty"

    local isBatteryInCache = false
    if items ~= nil and items.Count > 0 then
        for _, count in pairs(items) do
            if count > 0 then
                isBatteryInCache = true
                break
            end
        end
    end

    local energyCurrent = self.m_buildingInfo.batteryBurn.energyCurrent
    if isBatteryInCache or energyCurrent > 0 then
        textId = CHARGING_BATTERY_STATE_TEXT_ID
        normalTipsStateName = "Charging"
    end

    self.view.chargingStateText.text = Language[textId]
    self.view.normalTipsStateCtrl:SetState(normalTipsStateName)
end

FacBattleCtrl._RefreshChargingEnergy = HL.Method() << function(self)
    local energyCurrent = self.m_buildingInfo.battle.energyCurrent
    local energyMax = self.m_buildingInfo.battle.energyMax
    local targetAmount = energyMax == 0 and 0 or energyCurrent / energyMax

    if self.m_currentChargingMode == ChargingMode.Battery then
        if energyCurrent > self.m_lastChargingEnergy and
            energyCurrent - self.m_lastChargingEnergy == self.m_buildingInfo.batteryBurn.energyLoaded then

            self:_RefreshBatteryState(true)
        end
    end
    self.view.chargingFillTarget.fillAmount = targetAmount

    if self.m_chargingTweenInitialized then
        if self.m_lastChargingEnergy ~= energyCurrent then
            if self.m_chargingTween ~= nil then
                self.m_chargingTween:Kill(false)
            end

            self.m_chargingTween = DOTween.To(function()
                return self.view.chargingFill.fillAmount
            end, function(amount)
                self.view.chargingFill.fillAmount = amount
                self.view.fillArrow.localEulerAngles = Vector3(0, 0, -360.0 * amount)
            end, targetAmount, FILL_AMOUNT_TWEEN_DURATION):OnComplete(function()
                if targetAmount >= 1 then
                    self.view.energyAnim:PlayWithTween("facbattle_done")
                end
            end)
            AudioManager.PostEvent(DataManager.towerDefenseCommonConfig.towerChargeAudioEvent)
        end
    else

        self.view.chargingFill.fillAmount = targetAmount
        self.view.fillArrow.localEulerAngles = Vector3(0, 0, -360.0 * targetAmount)
        self.m_chargingTweenInitialized = true
    end

    self.m_lastChargingEnergy = energyCurrent

    self.view.currentEnergyText.text = string.format("%d", energyCurrent)
    self.view.maxEnergyText.text = string.format("%d", energyMax)
end

FacBattleCtrl._RefreshIsInChargingState = HL.Method() << function(self)
    local isRunning = self.view.buildingCommon.lastState == GEnums.FacBuildingState.Normal
    self.m_isInCharging = false

    if not isRunning then
        return
    end

    if self.m_lastChargingEnergy == self.m_buildingInfo.battle.energyMax then
        return
    end

    if self.m_currentChargingMode == ChargingMode.Battery then
        if self.m_buildingInfo.batteryBurn.energyCurrent <= 0 then
            return
        end
    end

    self.m_isInCharging = true
end

FacBattleCtrl._RefreshChargingAnimState = HL.Method() << function(self)
    self.view.overloadNode.iconElectricCircle.gameObject:SetActive(self.m_isInCharging)
    self.view.powerNetNode.iconElectricCircle.gameObject:SetActive(self.m_isInCharging)
    self.view.batteryNode.iconElectricCircle.gameObject:SetActive(self.m_isInCharging)
end






FacBattleCtrl._OnAutoFillButtonClicked = HL.Method() << function(self)
    GameInstance.player.remoteFactory.core:Message_QuickPutBattery(
        Utils.getCurrentChapterId(),
        self.m_buildingInfo.cache.componentId
    )
end






FacBattleCtrl._RefreshInventoryItemCell = HL.Method(HL.Userdata, HL.Any) << function(self, cell, itemBundle)
    if cell == nil or itemBundle == nil then
        return
    end


    local isBattery = self.m_batteryItemIdMap[itemBundle.id] ~= nil
    local isEmpty = string.isEmpty(itemBundle.id)
    cell.view.forbiddenMask.gameObject:SetActiveIfNecessary(not isBattery and not isEmpty)
    cell.view.dragItem.enabled = isBattery
    cell.view.dropItem.enabled = isBattery or isEmpty
end

FacBattleCtrl._RefreshInventoryItemCellsOnStateChange = HL.Method() << function(self)
    self.view.inventoryArea.view.itemBag.view.itemBagContent:Refresh()
    if self.view.inventoryArea.inSafeZone then
        self.view.inventoryArea.view.depot.view.depotContent:RefreshAll()
    end
end






FacBattleCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacBattleCtrl._InitBattleController = HL.Method() << function(self)
    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)
    self:_RefreshNaviGroupSwitcherInfos()
end

FacBattleCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {
        { naviGroup = self.view.cacheNaviGroup },
    }
    self.view.inventoryArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)

    local isOthersSocialBuilding = FactoryUtils.isOthersSocialBuilding(self.m_buildingInfo.nodeId)
    self.m_naviGroupSwitcher:ToggleActive(not isOthersSocialBuilding)
end




HL.Commit(FacBattleCtrl)
