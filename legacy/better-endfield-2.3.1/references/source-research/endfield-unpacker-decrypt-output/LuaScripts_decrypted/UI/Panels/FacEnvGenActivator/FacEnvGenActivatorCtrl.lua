
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacEnvGenActivator

FacEnvGenActivatorCtrl = HL.Class('FacEnvGenActivatorCtrl', uiCtrl.UICtrl)

local DECO_STATE = {
    NotActivated = "NotActivated",
    Available = "Available",
    Closed = "Closed",
    NoElectricity = "NoElectricity",
    NoNetWork = "NoNetWork",
}

local SMART_ALERT_FUNCTION_NAME_LIST = {
    "_CheckAlertNoPowerCondition",
    "_CheckAlertNoPowerWithDiffuserCondition",
    "_CheckAlertNoPowerWithoutDiffuserCondition",
    "_CheckAlertCanBeOpenedCondition",
    "_CheckAlertEnvGenInputInvalidGasCondition",
    "_CheckAlertEnvGenInputGasInsufficientCondition",
    "_CheckAlertEnvGenNoInputGasCondition",
}





FacEnvGenActivatorCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacEnvGenActivatorCtrl.m_nodeId = HL.Field(HL.Any)

FacEnvGenActivatorCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_EnvGenerator)

FacEnvGenActivatorCtrl.m_updateThread = HL.Field(HL.Thread)

FacEnvGenActivatorCtrl.m_envGenData = HL.Field(HL.Table)

FacEnvGenActivatorCtrl.m_lastItemId = HL.Field(HL.String) << ""

FacEnvGenActivatorCtrl.m_lockItemId = HL.Field(HL.String) << ""

FacEnvGenActivatorCtrl.m_machineInfos = HL.Field(HL.Table)

FacEnvGenActivatorCtrl.m_machineCacheList = HL.Field(HL.Forward('UIListCache'))

FacEnvGenActivatorCtrl.m_activatorMaxCount = HL.Field(HL.Number) << 30


FacEnvGenActivatorCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId

    self.m_machineCacheList = UIUtils.genCellCache(self.view.machineCell)

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {
        smartAlertFuncNameList = SMART_ALERT_FUNCTION_NAME_LIST,
        targetCtrlInstance = self
    })

    self.view.facCachePipe:InitFacCachePipe(self.m_uiInfo, {
        useSinglePipe = true,
    })

    self.m_envGenData = {}
    local envGenCfg = Tables.factoryVaporizerTable:GetValue(self.m_uiInfo.nodeHandler.templateId)
    for _, groupData in pairs(envGenCfg.groups) do
        self.m_envGenData[groupData.consumeItem] = {
            consumeNeed = groupData.consumeRate,
            consumeMax = groupData.consumeRateUpperLimit,
        }
    end

    if self.m_uiInfo.nodeHandler.predefinedParam ~= nil
        and self.m_uiInfo.nodeHandler.predefinedParam.envGenWithActivator ~= nil
        and self.m_uiInfo.nodeHandler.predefinedParam.envGenWithActivator.lockEnvFromItemId ~= nil then
        self.m_lockItemId = self.m_uiInfo.nodeHandler.predefinedParam.envGenWithActivator.lockEnvFromItemId
    end

    self.view.formulaNode:InitFormulaNode(self.m_uiInfo)

    self:_UpdateMachineNode()
    self:_UpdateConsumeItem(true)
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_UpdateConsumeItem(false)
            self:_RefreshMachineAllNodeInfo()
        end
    end)

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.contentRect)
    local scrollRectYEnable = self.view.scrollRect.rect.size.y < self.view.contentRect.rect.size.y
    self.view.controllerHint.gameObject:SetActiveIfNecessary(scrollRectYEnable)

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)
end

FacEnvGenActivatorCtrl.OnClose = HL.Override() << function(self)
    self.view.buildingCommon:ClearSmartAlertUpdate()
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_nodeId)
end

FacEnvGenActivatorCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, isActive)
    self.view.controllerHint.enabled = isActive
end

FacEnvGenActivatorCtrl._RefreshMachineAllNodeInfo = HL.Method() << function(self)
    if next(self.m_machineInfos) == nil then
        return
    end

    local cellCount = self.m_machineCacheList:GetCount()
    for index = 1, cellCount do
        local cell = self.m_machineCacheList:GetItem(index)
        self:_RefreshMachineNodeInfo(cell, index)
    end
end

FacEnvGenActivatorCtrl._UpdateConsumeItem = HL.Method(HL.Boolean) << function(self, isInit)
    local itemId
    if string.isEmpty(self.m_lockItemId) then
        itemId = self.m_uiInfo.activatorCost.currentItemId
    else
        itemId = self.m_lockItemId
    end

    local curConsumeCount = self.m_uiInfo.activatorCost.currentBufCnt * 6
    if itemId ~= self.m_lastItemId or isInit then
        self.m_lastItemId = itemId
        if string.isEmpty(itemId) then
            self.view.pointerAndBar.gameObject:SetActiveIfNecessary(false)
            self.view.item.gameObject:SetActiveIfNecessary(false)
            self.view.emptyState.gameObject:SetActiveIfNecessary(true)
            self.view.consumeMax.gameObject:SetActiveIfNecessary(false)
            self.view.averageFlowNumTxt.color = self.view.config.EMPTY_SPEED_COLOR
            if isInit or self.view.item.view.button.isNaviTarget then
                self:SetNaviTarget(self.view.emptyState)
            end
        else
            self.view.pointerAndBar.gameObject:SetActiveIfNecessary(true)
            self.view.item.gameObject:SetActiveIfNecessary(true)
            self.view.emptyState.gameObject:SetActiveIfNecessary(false)
            self.view.consumeMax.gameObject:SetActiveIfNecessary(true)
            local consumeNeed = self.m_envGenData[itemId].consumeNeed
            self.m_activatorMaxCount = self.m_envGenData[itemId].consumeMax
            self.view.consumeBar.fillAmount = consumeNeed / self.m_activatorMaxCount
            self.view.item:InitItem({ id = itemId }, function()
                if DeviceInfo.usingController then
                    self.view.item:ShowActionMenu()
                    return
                end
                self.view.item:SetSelected(true)
                self.view.item:ShowTips(nil, function()
                    self.view.item:SetSelected(false)
                end)
            end)
            self.view.item.actionMenuArgs = {}
            InputManagerInst:SetBindingText(self.view.item.view.button.hoverConfirmBindingId, Language["key_hint_item_open_action_menu"])
            self.view.consumeMax.text = self.m_activatorMaxCount
            self.view.activatorPointer:InitActivatorPointer(self.m_activatorMaxCount, curConsumeCount)
            self.view.averageFlowNumTxt.color = self.view.config.NORMAL_SPEED_COLOR
            if isInit or self.view.emptyState.isNaviTarget then
                self:SetNaviTarget(self.view.item.view.button)
            end
        end
        if string.isEmpty(self.m_lockItemId) then
            local targetCraftInfo = FactoryUtils.getBuildingProcessingCraft(self.m_uiInfo)
            self.view.formulaNode:RefreshDisplayFormula(targetCraftInfo)
        else
            local crafts = FactoryUtils.getBuildingCraftsWithNodeId(self.m_nodeId, true)
            for _, craftInfo in pairs(crafts) do
                if craftInfo.incomes[1].id == self.m_lockItemId then
                    self.view.formulaNode:RefreshDisplayFormula(craftInfo)
                    break
                end
            end
        end
    end

    self.view.activatorPointer:RefreshConsumePointer(curConsumeCount)
    self.view.averageFlowNumTxt.text = curConsumeCount

    local envState = GEnums.FacEnvGenEnvType.__CastFrom(self.m_uiInfo.envGenerator.currentEnv)
    self.view.envGeneratorState:SetState(envState:ToString())

    if self.m_uiInfo.activatorCost.inActive and envState ~= GEnums.FacEnvGenEnvType.None then
        self.view.consumeDecoState:SetState(DECO_STATE.Available)
    elseif self.view.buildingCommon.lastState == GEnums.FacBuildingState.NoPower then
        self.view.consumeDecoState:SetState(DECO_STATE.NoElectricity)
        self.view.emptyStateTxt.text = Language.LUA_FACTORY_ENV_GEN_NO_POWER_TIPS
    elseif self.view.buildingCommon.lastState == GEnums.FacBuildingState.NotInPowerNet then
        self.view.consumeDecoState:SetState(DECO_STATE.NoNetWork)
        self.view.emptyStateTxt.text = Language.LUA_FACTORY_ENV_GEN_NOT_IN_POWER_NET_TIPS
    elseif self.view.buildingCommon.lastState == GEnums.FacBuildingState.Closed then
        self.view.consumeDecoState:SetState(DECO_STATE.Closed)
        self.view.emptyStateTxt.text = Language.LUA_FACTORY_ENV_GEN_CLOSED_TIPS
    else
        self.view.consumeDecoState:SetState(DECO_STATE.NotActivated)
        if string.isEmpty(itemId) then
            self.view.emptyStateTxt.text = Language.LUA_FACTORY_ENV_GEN_NO_VALID_GAS_TIPS
        else
            self.view.emptyStateTxt.text = Language.LUA_FACTORY_ENV_GEN_NOT_ENOUGH_GAS_TIPS
        end
    end
end

FacEnvGenActivatorCtrl._UpdateMachineNode = HL.Method() << function(self)
    local infos = {}
    local linkedNodes = self.m_uiInfo.envGenerator.coveredNodes
    local nodeCount = 0
    for _, nodeHandler in pairs(linkedNodes) do
        local soilComponent = nodeHandler:GetComponentInPosition(GEnums.FCComponentPos.Soil:GetHashCode())
        local soil = nil
        if soilComponent ~= nil and soilComponent.soil ~= nil then
            soil = soilComponent.soil
        end
        table.insert(infos, {
            handler = nodeHandler,
            soil = soil,
        })
        nodeCount = nodeCount + 1
    end
    self.m_machineInfos = infos
    self.view.emptyInfo.gameObject:SetActiveIfNecessary(nodeCount <= 0)
    self.view.content.gameObject:SetActiveIfNecessary(nodeCount > 0)
    self.m_machineCacheList:Refresh(nodeCount, function(cell, index)
        self:_RefreshMachineNodeBasicContent(cell, index)
        self:_RefreshMachineNodeInfo(cell, index)
    end)
end

FacEnvGenActivatorCtrl._RefreshMachineNodeBasicContent = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local handler = self.m_machineInfos[index].handler
    local data = Tables.factoryBuildingTable:GetValue(handler.templateId)
    cell.name.text = data.name
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, data.iconOnPanel)
    cell.bgEven.gameObject:SetActiveIfNecessary(index % 2 == 1)
end

FacEnvGenActivatorCtrl._RefreshMachineNodeInfo = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local soilCom = self.m_machineInfos[index].soil
    local handler = self.m_machineInfos[index].handler
    if soilCom == nil then
        local state = FactoryUtils.getBuildingStateType(handler.nodeId)
        if state == GEnums.FacBuildingState.NotInPowerNet or
            state == GEnums.FacBuildingState.Blocked or
            state == GEnums.FacBuildingState.NoPower or
            state == GEnums.FacBuildingState.Closed or
            state == GEnums.FacBuildingState.Idle or
            state == GEnums.FacBuildingState.Normal then
            cell.stateController:SetState(state:ToString())
        end
    else
        local step = soilCom.stepCursor
        local success, stepsData = Tables.plantingDataTable:TryGetValue(handler.templateId)
        if not success then
            return
        end
        local stepData = stepsData.plantingSteps[step]
        local stepType = stepData.plantingStepType
        if stepType == GEnums.PlantingStepType.Water then
            local valueIntList = stepData.plantingStepParameter.valueIntList
            local totalProgress = valueIntList[valueIntList.Count - 1]
            local currProgress = soilCom.waterGot
            if currProgress == 0 then
                cell.stateController:SetState("Water")
            else
                if self.m_uiInfo.envGenerator.currentEnv == GEnums.FacEnvGenEnvType.Humidity:GetHashCode() then
                    cell.stateController:SetState("Progress")
                else
                    cell.stateController:SetState("Pause")
                end
                cell.progressBar.fillAmount = currProgress / totalProgress
            end
        elseif stepType == GEnums.PlantingStepType.Reclaim then
            cell.stateController:SetState("Reclaim")
        elseif stepType == GEnums.PlantingStepType.Harvest then
            cell.stateController:SetState("Harvest")
        else
            cell.stateController:SetState("Growth")
        end
    end
end




FacEnvGenActivatorCtrl._CheckAlertCanBeOpenedCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
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
                targetTransform = self.view.buildingCommon.view.stateNode.transform,
                defaultOpen = checkOpen
            }
            return true, alertInfo
        end
    end
    return false
end

FacEnvGenActivatorCtrl._CheckAlertNoPowerWithoutDiffuserCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.NotInPowerNet then
        return false
    end
    if self.m_uiInfo.inPowerRangeDiffusers.Count <= 0 then
        local checkOpen = DeviceInfo.usingController and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.NoPowerWithoutDiffuser,
            targetTransform = self.view.buildingCommon.view.stateNode.transform,
            defaultOpen = checkOpen
        }
        return true, alertInfo
    end
    return false
end

FacEnvGenActivatorCtrl._CheckAlertNoPowerWithDiffuserCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.NotInPowerNet then
        return false
    end
    if self.m_uiInfo.inPowerRangeDiffusers.Count > 0 then
        local checkOpen = DeviceInfo.usingController and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.NoPowerWithDiffuser,
            targetTransform = self.view.buildingCommon.view.stateNode.transform,
            defaultOpen = checkOpen
        }
        return true, alertInfo
    end
    return false
end

FacEnvGenActivatorCtrl._CheckAlertNoPowerCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.NoPower then
        return false
    end
    local checkOpen = DeviceInfo.usingController and
        self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
    local alertInfo = {
        condition = GEnums.FacSmartAlertType.NoPower,
        targetTransform = self.view.buildingCommon.view.stateNode.transform,
        defaultOpen = checkOpen
    }
    return true, alertInfo
end

FacEnvGenActivatorCtrl._CheckAlertEnvGenInputInvalidGasCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.InActive then
        return false
    end






    if self.m_uiInfo.activatorCost.currentBufCnt == 0 then
        local inPipeInfoList, _ = FactoryUtils.getBuildingPortState(self.m_nodeId, true)
        if inPipeInfoList and inPipeInfoList[1] and inPipeInfoList[1].isBlock then
            local checkOpen = DeviceInfo.usingController and
                self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
            local alertInfo = {
                condition = GEnums.FacSmartAlertType.EnvGenInputInvalidGas,
                targetTransform = self.view.facCachePipe.view.singleInCell.rectTransform,
                defaultOpen = checkOpen
            }
            return true, alertInfo
        end
    end

    return false
end

FacEnvGenActivatorCtrl._CheckAlertEnvGenInputGasInsufficientCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.InActive then
        return false
    end





    if self.m_uiInfo.activatorCost.currentBufCnt > 0 then
        local checkOpen = DeviceInfo.usingController and
            self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        local alertInfo = {
            condition = GEnums.FacSmartAlertType.EnvGenInputGasInsufficient,
            targetTransform = self.view.item.transform,
            defaultOpen = checkOpen
        }
        return true, alertInfo
    end

    return false
end

FacEnvGenActivatorCtrl._CheckAlertEnvGenNoInputGasCondition = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.Table)) << function(self, state)
    if self.view.buildingCommon.smartAlertChangeCachePauseUpdate or state ~= GEnums.FacBuildingState.InActive then
        return false
    end






    if self.m_uiInfo.activatorCost.currentBufCnt == 0 then
        local inPipeInfoList, _ = FactoryUtils.getBuildingPortState(self.m_nodeId, true)
        if inPipeInfoList and inPipeInfoList[1] and not inPipeInfoList[1].isBlock then
            local checkOpen = DeviceInfo.usingController and
                self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
            local alertInfo = {
                condition = GEnums.FacSmartAlertType.EnvGenNoInputGas,
                targetTransform = self.view.item.transform,
                defaultOpen = checkOpen
            }
            return true, alertInfo
        end
    end

    return false
end



HL.Commit(FacEnvGenActivatorCtrl)
