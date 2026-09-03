local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacDumper

local DumperState = {
    None = "None",
    Stopped = "Stopped",
    Normal = "Normal",
    Paused = "Paused",
    Full = "Full",
    Invalid = "Invalid",
}

local DumperRiftState = {
    None = "None",
    Full = "FullDesc",
    Invalid = "InvalidDesc",
    Pause = "PausedDesc",
    Wait = "WaitingForLoading",
}

local ContainerState = {
    None = "None",
    Empty = "Empty",
    Normal = "Normal",
}

local TipsState = {
    None = "None",
    Normal = "Normal",
    CannotDischarge = "CannotDischarge",
    Invalid = "Invalid",
}

local DumperPipeState = {
    None = "None",
    Normal = "Normal",
    Blocked = "Blocked",
}

local SMART_ALERT_FUNCTION_NAME_LIST = {
    "_CheckAlertNoPowerCondition",
    "_CheckAlertNoPowerWithDiffuserCondition",
    "_CheckAlertNoPowerWithoutDiffuserCondition",
    "_CheckAlertCanBeOpenedCondition",
    "_CheckAlertDiffTypeLiquidCannotDumpedCondition",
    "_CheckAlertLiquidTypeCannotDumpedCondition",
    "_CheckAlertDiffTypeLiquidCannotDumpedInVolumeRiftCondition",
}

FacDumperCtrl = HL.Class('FacDumperCtrl', uiCtrl.UICtrl)

FacDumperCtrl.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_FluidPumpOut)

FacDumperCtrl.m_targetContainerInfo = HL.Field(CS.Beyond.Gameplay.Factory.FactoryUtil.FluidContainerInfo)

FacDumperCtrl.m_riftVolumeInfo = HL.Field(CS.Beyond.Gameplay.Factory.FactoryUtil.RiftVolumeInfo)

FacDumperCtrl.m_isVolumeRift = HL.Field(HL.Boolean) << false

FacDumperCtrl.m_targetContainer = HL.Field(HL.Userdata)

FacDumperCtrl.m_updateThread = HL.Field(HL.Thread)

FacDumperCtrl.m_currContainerItemId = HL.Field(HL.String) << ""

FacDumperCtrl.m_currContainerItemCount = HL.Field(HL.Number) << -1

FacDumperCtrl.m_currCacheItemId = HL.Field(HL.String) << ""

FacDumperCtrl.m_currCacheItemCount = HL.Field(HL.Number) << -1

FacDumperCtrl.m_outSpeed = HL.Field(HL.Number) << -1

FacDumperCtrl.m_dumperState = HL.Field(HL.String) << ""

FacDumperCtrl.m_containerState = HL.Field(HL.String) << ""

FacDumperCtrl.m_tipsState = HL.Field(HL.String) << ""

FacDumperCtrl.m_dumperPipeState = HL.Field(HL.String) << ""

FacDumperCtrl.m_isContainerItemChanged = HL.Field(HL.Boolean) << true

FacDumperCtrl.m_isContainerItemIncreased = HL.Field(HL.Boolean) << false

FacDumperCtrl.m_isInfinite = HL.Field(HL.Boolean) << false

FacDumperCtrl.m_maxAmount = HL.Field(HL.Number) << -1





FacDumperCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacDumperCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo

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
        hasFluidInCache = true,
    })

    self.view.facCachePipe:InitFacCachePipe(self.m_buildingInfo, {
        useSinglePipe = true,
    })

    self.view.facCacheRepository:InitFacCacheRepository({
        cache = self.m_buildingInfo.cachePumpOut,
        isInCache = true,
        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid,
        cacheIndex = 1,
        slotCount = 1,
    })

    self.view.buildingCommon:InitBuildingCommon(self.m_buildingInfo, {
        onStateChanged = function(state)
            self:_RefreshDumperPipeAnimRunningState()
        end,
        smartAlertFuncNameList = SMART_ALERT_FUNCTION_NAME_LIST,
        targetCtrlInstance = self
    })

    self:_InitFacMachineCrafterController()
    self.view.targetContainerNode.liquidBg:InitFacLiquidBg()
    self.view.content:NaviToThisGroup()

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(self.m_buildingInfo.nodeId)

    self:_UpdateSmartAlertCache()
    self:_InitDumperTargetContainer()
    self:_InitDumperUpdateThread()
end

FacDumperCtrl.OnClose = HL.Override() << function(self)
    self.view.buildingCommon:ClearSmartAlertUpdate()
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_buildingInfo.nodeId)
end

FacDumperCtrl._GetDumperCacheItemData = HL.Method().Return(HL.String, HL.Number) << function(self)
    for itemId, itemCount in pairs(self.m_buildingInfo.cachePumpOut.items) do
        return itemId, itemCount
    end
    return "", 0
end




FacDumperCtrl._InitDumperTargetContainer = HL.Method() << function(self)
    local targetNodeId = self.m_buildingInfo.fluidPumpOut.targetNodeId
    local targetHandler = CSFactoryUtil.GetNodeHandlerByNodeId(targetNodeId)
    self.m_isVolumeRift = false
    if targetHandler ~= nil then
        local riftVolumeId = CS.System.UInt64.Parse(targetHandler.instKey)
        self.m_isVolumeRift = GameInstance.remoteFactoryManager:QueryRiftVolumeState(riftVolumeId)

        local component = FactoryUtils.getBuildingComponentHandlerAtPos(targetHandler, GEnums.FCComponentPos.FluidContainer)
        if component ~= nil then
            self.m_targetContainer = component.fluidContainer
        end
    end

    if self.m_isVolumeRift then
        self.m_riftVolumeInfo = CSFactoryUtil.GetRiftVolumeInfo(targetNodeId)
        self.m_isInfinite = self.m_riftVolumeInfo.isInfinite
        self.m_maxAmount = self.m_riftVolumeInfo.maxAmount

        self:_RefreshRiftVolumeBasicInfo()
    else
        local targetInfo = CSFactoryUtil.GetFluidContainerInfo(targetNodeId)
        self.m_targetContainerInfo = targetInfo
        self.m_isInfinite = targetInfo.isInfinite
        self.m_maxAmount = targetInfo.maxAmount
        self:_RefreshTargetContainerBasicContent()
    end
end

FacDumperCtrl._InitDumperUpdateThread = HL.Method() << function(self)
    self:_UpdateAndRefreshAll()
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateAndRefreshAll()
        end
    end)
end






FacDumperCtrl._IsCacheEmpty = HL.Method().Return(HL.Boolean) << function(self)
    return string.isEmpty(self.m_currCacheItemId) or self.m_currCacheItemCount == 0
end

FacDumperCtrl._IsContainerEmpty = HL.Method().Return(HL.Boolean) << function(self)
    return string.isEmpty(self.m_currContainerItemId) or self.m_currContainerItemCount == 0
end

FacDumperCtrl._GetDumperState = HL.Method().Return(HL.String) << function(self)
    if self:_IsCacheEmpty() then
        return self.m_outSpeed > 0 and DumperState.Normal or DumperState.Stopped
    else
        if self.m_outSpeed == 0 and self.view.buildingCommon.lastState ~= GEnums.FacBuildingState.Idle then
            if self.m_currContainerItemCount == self.m_maxAmount then
                return DumperState.Full
            else
                if self.m_currCacheItemId == self.m_currContainerItemId or string.isEmpty(self.m_currContainerItemId) then
                    return DumperState.Paused
                else
                    return DumperState.Invalid
                end
            end
        else
            return DumperState.Normal
        end
    end
end

FacDumperCtrl._GetContainerState = HL.Method().Return(HL.String) << function(self)
    if self:_IsContainerEmpty() then
        return ContainerState.Empty
    else
        return ContainerState.Normal
    end
end

FacDumperCtrl._GetTipsState = HL.Method().Return(HL.String) << function(self)
    if not string.isEmpty(self.m_currCacheItemId) and not FactoryUtils.getLiquidCanBeDischarge(self.m_currCacheItemId) then
        return TipsState.CannotDischarge
    else
        if not string.isEmpty(self.m_currCacheItemId) and not string.isEmpty(self.m_currContainerItemId) then
            if self.m_currCacheItemId ~= self.m_currContainerItemId then
                return TipsState.Invalid
            else
                return TipsState.Normal
            end
        else
            if self.m_isVolumeRift and not string.isEmpty(self.m_currCacheItemId) then
                if self.m_currCacheItemId ~= self.m_riftVolumeInfo.itemId then
                    return TipsState.Invalid
                end
            end
            return TipsState.Normal
        end
    end
end

FacDumperCtrl._GetDumperPipeState = HL.Method().Return(HL.String) << function(self)
    if self.m_outSpeed == 0 and not self:_IsCacheEmpty() then
        return DumperPipeState.Blocked
    else
        return DumperPipeState.Normal
    end
end






FacDumperCtrl._UpdateAndRefreshAll = HL.Method() << function(self)
    self:_UpdateDumperData()
    self:_UpdateAndRefreshDumperState()
    self:_UpdateAndRefreshContainerState()
    self:_UpdateAndRefreshTipsState()
    self:_UpdateAndRefreshDumperPipeState()

    self:_RefreshContainerItem()
    self:_RefreshOutSpeed()
    self:_RefreshLiquidBg()
end

FacDumperCtrl._UpdateDumperData = HL.Method() << function(self)
    local holdItem = self.m_targetContainer.holdItem
    local containerItemId = holdItem == nil and "" or holdItem.id
    local containerItemCount = holdItem == nil and 0 or holdItem.count
    local cacheItemId, cacheItemCount = self:_GetDumperCacheItemData()

    self.m_isContainerItemChanged = self.m_currContainerItemId ~= containerItemId
    self.m_isContainerItemIncreased = self.m_currContainerItemCount >= 0 and self.m_currContainerItemCount < containerItemCount
    self.m_currContainerItemId = containerItemId
    self.m_currContainerItemCount = containerItemCount
    self.m_currCacheItemId = cacheItemId
    self.m_currCacheItemCount = cacheItemCount

    self.m_outSpeed = self.m_buildingInfo.fluidPumpOut.lastRoundPumpCount
end

FacDumperCtrl._UpdateAndRefreshDumperState = HL.Method() << function(self)
    local state = self.m_isVolumeRift and self:_GetRiftState() or self:_GetDumperState()
    if state == self.m_dumperState then
        return
    end

    if self.m_isVolumeRift then
        if state == DumperRiftState.None then
            self.view.fractureNode.descNodes.gameObject:SetActiveIfNecessary(false)
        else
            self.view.fractureNode.descNodes:SetState(state)
            self.view.fractureNode.descNodes.gameObject:SetActiveIfNecessary(true)
        end
    else
        self.view.contentController:SetState(state)
    end
    self.m_dumperState = state
end

FacDumperCtrl._UpdateAndRefreshContainerState = HL.Method() << function(self)
    local state = self:_GetContainerState()
    if state == self.m_containerState then
        return
    end

    self.view.targetContainerNode.stateController:SetState(state)
    self.m_containerState = state

    local isEmpty = state == ContainerState.Empty
    UIUtils.PlayAnimationAndToggleActive(self.view.targetContainerNode.emptyAnim, isEmpty)
end

FacDumperCtrl._UpdateAndRefreshTipsState = HL.Method() << function(self)
    local state = self:_GetTipsState()
    if state == self.m_tipsState then
        return
    end

    if self.m_isVolumeRift then
        local isShown = state ~= TipsState.Normal
        UIUtils.PlayAnimationAndToggleActive(self.view.fractureTipsText, isShown)
    else
        if state == TipsState.CannotDischarge then
            self.view.tipsTxt.text = string.format(Language.LUA_LIQUID_CANT_DISCHARGE_IN_DUMPER, UIUtils.getItemName(self.m_currCacheItemId))
        elseif state == TipsState.Invalid then
            self.view.tipsTxt.text = Language["ui_fac_liquid_storage_different_pause"]
        end
        local isShown = state ~= TipsState.Normal
        UIUtils.PlayAnimationAndToggleActive(self.view.tipsAnim, isShown)
    end

    self.m_tipsState = state
end

FacDumperCtrl._UpdateAndRefreshDumperPipeState = HL.Method() << function(self)
    local state = self:_GetDumperPipeState()
    if state == self.m_dumperPipeState then
        return
    end

    self.view.dumperPipeNode.stateController:SetState(state)
    local isBlocked = state == DumperPipeState.Blocked
    UIUtils.PlayAnimationAndToggleActive(self.view.dumperPipeNode.blockedAnim, isBlocked)
    self.m_dumperPipeState = state
    self:_RefreshDumperPipeAnimRunningState()
end

FacDumperCtrl._RefreshTargetContainerBasicContent = HL.Method() << function(self)
    if self.m_targetContainerInfo == nil then
        return
    end


    self.view.targetContainerNode.targetNameText.text = self.m_targetContainerInfo.name


    local isInfinite = self.m_isInfinite
    self.view.targetContainerNode.maxCountText.text = isInfinite and
        Language.LUA_ITEM_INFINITE_COUNT or
        string.format("%d", self.m_maxAmount)

    local success, tableData = Tables.factoryFluidPumpOutTable:TryGetValue(self.m_buildingInfo.nodeHandler.templateId)
    if success then
        self.view.maxSpeedText.text = string.format("%d", tableData.maximumSuply)
    end

    self.view.targetContainerNode.gameObject:SetActiveIfNecessary(true)
    self.view.fractureNode.gameObject:SetActiveIfNecessary(false)
end

FacDumperCtrl._RefreshContainerItem = HL.Method() << function(self)
    if not self.m_isContainerItemChanged then
        local isInfinite = self.m_isInfinite

        if self.m_isVolumeRift then
            self:_RefreshRiftProgress(self.m_currContainerItemCount)
        else
            self.view.targetContainerNode.currentCountText.text = isInfinite and
                Language.LUA_ITEM_INFINITE_COUNT or
                string.format("%d", self.m_currContainerItemCount)
        end

        if self.m_isContainerItemIncreased then
            self.view.dumperPipeNode.itemAnim:PlayWithTween("dumper_item_changed")
        end

        return
    end

    local itemData = {
        id = self.m_currContainerItemId,
    }
    self.view.targetContainerNode.targetItem:InitItem(itemData, true)
    if DeviceInfo.usingController then
        self.view.targetContainerNode.targetItem:SetEnableHoverTips(false)
    end

    local success, tableData = Tables.itemTable:TryGetValue(self.m_currContainerItemId)
    if success then
        self.view.targetContainerNode.itemNameText.text = tableData.name
    end
end

FacDumperCtrl._RefreshOutSpeed = HL.Method() << function(self)
    self.view.currSpeedText.text = string.format("%d", self.m_outSpeed)
    self.view.currSpeedText.color = self.m_outSpeed > 0 and
        self.view.config.NORMAL_SPEED_COLOR or
        self.view.config.STOPPED_SPEED_COLOR
end

FacDumperCtrl._RefreshInventoryItemCell = HL.Method(HL.Userdata, HL.Any) << function(self, cell, itemBundle)
    if cell == nil or itemBundle == nil then
        return
    end


    local itemId = itemBundle.id
    local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isBottle = isEmptyBottle or isFullBottle
    local isEmpty = string.isEmpty(itemBundle.id)

    if not isBottle and not isEmpty then
        cell.view.forbiddenMask.gameObject:SetActiveIfNecessary(true)
        cell.view.dropItem.enabled = false
    end
    if not isBottle then
        cell.view.dragItem.enabled = false
    end
end

FacDumperCtrl._RefreshDumperPipeAnimRunningState = HL.Method() << function(self)
    local isRunning = self.view.buildingCommon.lastState == GEnums.FacBuildingState.Normal
    local animName = isRunning and "dumper_decoarrow_loop" or "dumper_decoarrow_defult"
    if self.view.dumperPipeNode.normalAnim.curStateName == animName then
        return
    end
    self.view.dumperPipeNode.normalAnim:PlayWithTween(animName)
end

FacDumperCtrl._RefreshLiquidBg = HL.Method() << function(self)
    local count = 0
    local height = 0
    if not string.isEmpty(self.m_currContainerItemId) then
        count = self.m_currContainerItemCount
        if self.m_isInfinite then
            height = 0
        else
            local maxCount = self.m_maxAmount
            height = count / maxCount
        end
    end

    self.view.targetContainerNode.liquidBg:RefreshLiquidHeight(height)
end





FacDumperCtrl._RefreshRiftVolumeBasicInfo = HL.Method() << function(self)
    if self.m_riftVolumeInfo == nil then
        return
    end

    local success, tableData = Tables.factoryFluidPumpOutTable:TryGetValue(self.m_buildingInfo.nodeHandler.templateId)
    if success then
        self.view.maxSpeedText.text = string.format("%d", tableData.maximumSuply)
    end

    if self.m_riftVolumeInfo.status == CS.Beyond.Gameplay.Core.RiftVolume.RiftStatus.FrsFilled then
        self.view.fractureNode.stateController:SetState("Empty")
        self.view.fractureNode.descNodes.gameObject:SetActiveIfNecessary(false)
    elseif self.m_riftVolumeInfo.status == CS.Beyond.Gameplay.Core.RiftVolume.RiftStatus.FrsSuppressed then
        self.view.fractureNode.stateController:SetState("Normal")
        local itemId = self.m_riftVolumeInfo.itemId
        local _, itemCfg = Tables.itemTable:TryGetValue(itemId)
        self.view.fractureNode.itemNameText.text = itemCfg.name
        self.view.fractureNode.targetItem:InitItem({id =itemId}, true)

        self:_RefreshRiftProgress(self.m_riftVolumeInfo.amount)
    else
        return
    end

    self.view.targetContainerNode.gameObject:SetActiveIfNecessary(false)
    self.view.fractureNode.gameObject:SetActiveIfNecessary(true)
end

FacDumperCtrl._RefreshRiftProgress = HL.Method(HL.Number) << function(self, amount)
    self.view.fractureNode.progressTxt.text = string.format("%d/%d", amount, self.m_maxAmount)
    local progress = amount / self.m_maxAmount
    self.view.fractureNode.percentageTxt.text = string.format("%.1f%%", math.floor(progress * 1000) / 10)
    self.view.fractureNode.progress.fillAmount = progress
end

FacDumperCtrl._GetRiftState = HL.Method().Return(HL.String) << function(self)
    if self.m_riftVolumeInfo.status == CS.Beyond.Gameplay.Core.RiftVolume.RiftStatus.FrsFilled then
        return DumperRiftState.None
    elseif self.m_currContainerItemCount == self.m_maxAmount then
        return DumperRiftState.Full
    elseif not string.isEmpty(self.m_currCacheItemId) and self.m_currCacheItemId ~= self.m_riftVolumeInfo.itemId then
        return DumperRiftState.Invalid
    elseif self.m_outSpeed == 0 then
        return self.m_currContainerItemCount > 0 and DumperRiftState.Pause or DumperRiftState.Wait
    else
        return DumperRiftState.None
    end
end






FacDumperCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacDumperCtrl._InitFacMachineCrafterController = HL.Method() << function(self)
    self.view.content.getDefaultSelectableFunc = function()
        return self.view.facCacheRepository.m_slotList:GetItem(1).view.liquidItemSlot.view.item.view.button
    end
    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)

    self:_RefreshNaviGroupSwitcherInfos()
end

FacDumperCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {
        {
            naviGroup = self.view.content,
            text = Language.LUA_INV_NAVI_SWITCH_TO_MACHINE,
            forceDefault = true,
        }
    }
    self.view.inventoryArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end






FacDumperCtrl.m_smartAlertTargetTransformCache = HL.Field(HL.Table)

FacDumperCtrl._UpdateSmartAlertCache = HL.Method() << function(self)
    self.m_smartAlertTargetTransformCache = {}

    self.m_smartAlertTargetTransformCache.state = self.view.buildingCommon.view.stateNode.transform
    local list = self.view.facCacheRepository:GetRepositorySlotList()
    if list[1] ~= nil then
        self.m_smartAlertTargetTransformCache.fluidCache = list[1].transform
    end
end

FacDumperCtrl._CheckAlertCanBeOpenedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Closed then
        return false
    end
    local node = self.m_buildingInfo.nodeHandler
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

FacDumperCtrl._CheckAlertNoPowerWithoutDiffuserCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.NotInPowerNet then
        return false
    end
    if self.m_buildingInfo.inPowerRangeDiffusers.Count <= 0 then
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

FacDumperCtrl._CheckAlertNoPowerWithDiffuserCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.NotInPowerNet then
        return false
    end
    if self.m_buildingInfo.inPowerRangeDiffusers.Count > 0 then
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

FacDumperCtrl._CheckAlertNoPowerCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
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

FacDumperCtrl._CheckAlertLiquidTypeCannotDumpedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.m_isVolumeRift then
        return false
    end
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Blocked then
        return false
    end
    local state = self:_GetTipsState()
    if state == TipsState.CannotDischarge then
        local checkOpen = DeviceInfo.usingController and
            self.view.content.IsTopLayer and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.LiquidTypeCannotDumped,
            targetTransform = self.m_smartAlertTargetTransformCache.fluidCache,
            args = {},
            checkRefresh = self.m_currCacheItemId,
            defaultOpen = checkOpen
        }
        table.insert(alertInfo.args, UIUtils.getItemName(self.m_currCacheItemId))
        table.insert(alertInfo.args, UIUtils.getItemName(self.m_currCacheItemId))
        return true, alertInfo
    end
    return false
end

FacDumperCtrl._CheckAlertDiffTypeLiquidCannotDumpedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.m_isVolumeRift then
        return false
    end

    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Blocked then
        return false
    end
    local state = self:_GetTipsState()
    if state == TipsState.Invalid then
        local checkOpen = DeviceInfo.usingController and
            self.view.content.IsTopLayer and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.DiffTypeLiquidCannotDumped,
            targetTransform = self.m_smartAlertTargetTransformCache.fluidCache,
            args = {},
            checkRefresh = self.m_currContainerItemId .. self.m_currCacheItemId,
            defaultOpen = checkOpen
        }
        table.insert(alertInfo.args, UIUtils.getItemName(self.m_currContainerItemId))
        table.insert(alertInfo.args, UIUtils.getItemName(self.m_currCacheItemId))
        return true, alertInfo
    end
    return false
end

FacDumperCtrl._CheckAlertDiffTypeLiquidCannotDumpedInVolumeRiftCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if not self.m_isVolumeRift then
        return false
    end

    if self.m_riftVolumeInfo.status == CS.Beyond.Gameplay.Core.RiftVolume.RiftStatus.FrsFilled then
        return false
    end

    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.Blocked then
        return false
    end
    local state = self:_GetTipsState()
    if state == TipsState.Invalid or state == TipsState.CannotDischarge then
        local checkOpen = DeviceInfo.usingController and
            self.view.content.IsTopLayer and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.TargetTypeCannotDumped,
            targetTransform = self.m_smartAlertTargetTransformCache.fluidCache,
            args = {},
            checkRefresh = self.m_riftVolumeInfo.itemId .. self.m_currCacheItemId,
            defaultOpen = checkOpen
        }
        table.insert(alertInfo.args, UIUtils.getItemName(self.m_riftVolumeInfo.itemId))
        table.insert(alertInfo.args, UIUtils.getItemName(self.m_currCacheItemId))
        return true, alertInfo
    end
    return false
end



HL.Commit(FacDumperCtrl)
