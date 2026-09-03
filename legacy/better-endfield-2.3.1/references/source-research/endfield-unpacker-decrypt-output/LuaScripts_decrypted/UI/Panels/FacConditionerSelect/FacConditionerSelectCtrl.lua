
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacConditionerSelect

FacConditionerSelectCtrl = HL.Class('FacConditionerSelectCtrl', uiCtrl.UICtrl)

local REFRESH_INTERVAL_CHANGE_COUNT = 5
local REFRESH_FAST_INTERVAL = 0.1
local REFRESH_INTERVAL = 0.5
local REFRESH_CONTROLLER_RATE = 32
local SPEED_LINE_POSY = 120
local COLOR_TEXT_FORMAT = "<color=#FF0000>%s</color>/%s"

local SPEED_LIMITED_TECH_TREE_ID = "tech_jinlong_4_conditioner_maxflow"





FacConditionerSelectCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacConditionerSelectCtrl.m_nodeId = HL.Field(HL.Any)

FacConditionerSelectCtrl.m_fluidType = HL.Field(HL.Boolean) << false

FacConditionerSelectCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo)

FacConditionerSelectCtrl.m_unionArgs = HL.Field(HL.Any)

FacConditionerSelectCtrl.m_updateThread = HL.Field(HL.Thread)

FacConditionerSelectCtrl.m_curValveNode = HL.Field(HL.Table)

FacConditionerSelectCtrl.m_curEmptyBtn = HL.Field(HL.Any)

FacConditionerSelectCtrl.m_blackBoxLockItem = HL.Field(HL.Boolean) << false

FacConditionerSelectCtrl.m_addBtnPressCoroutine = HL.Field(HL.Thread)

FacConditionerSelectCtrl.m_reduceBtnPressCoroutine = HL.Field(HL.Thread)

FacConditionerSelectCtrl.m_addSpeedBtnPressCoroutine = HL.Field(HL.Thread)

FacConditionerSelectCtrl.m_reduceSpeedBtnPressCoroutine = HL.Field(HL.Thread)

FacConditionerSelectCtrl.m_cacheShowItemTipsBindingId = HL.Field(HL.Number) << -1

FacConditionerSelectCtrl.m_btnPressFastChangeRate = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_countSliderStep = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_countSliderMin = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_countSliderMax = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_speedLimitedUnlock = HL.Field(HL.Boolean) << false

FacConditionerSelectCtrl.m_speedSliderStep = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_speedSliderDefault = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_speedSliderMin = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_speedSliderMax = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_speedCountAll = HL.Field(HL.Number) << 1

FacConditionerSelectCtrl.m_speedCountMax = HL.Field(HL.Number) << 1


FacConditionerSelectCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId

    local logisticData = FactoryUtils.getLogisticData(self.m_uiInfo.nodeHandler.templateId)
    local buildingData = { nodeId = nodeId }
    setmetatable(buildingData, { __index = logisticData })
    self.view.buildingCommon:InitBuildingCommon(nil, {
        data = buildingData,
        customRightButtonOnClicked = function()
            if not FactoryUtils.canDelBuilding(self.m_nodeId, true) then
                return
            end
            PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
            GameInstance.player.remoteFactory.core:Message_OpDismantle(Utils.getCurrentChapterId(), self.m_nodeId)
        end
    })

    local buildingitemData = Tables.itemTable:GetValue(logisticData.itemId)
    self.view.descText.text = buildingitemData.desc

    self.m_unionArgs = self.m_uiInfo.boxValve or self.m_uiInfo.fluidValve
    self.m_fluidType = self.m_uiInfo.boxValve == nil

    self:_InitValveNode()
    self:_InitBlackBoxState()
    self:_RefreshSelectItem(true)

    self:SetNaviTarget(self.view.naviDir)

    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            if not string.isEmpty(self.m_unionArgs.selectedItemId) then
                local showThroughCount = self.m_unionArgs.currentPassed and self.m_unionArgs.currentPassed > 0
                self.view.throughCountTxt.gameObject:SetActiveIfNecessary(showThroughCount)
                self.view.throughCountEmpty.gameObject:SetActiveIfNecessary(not showThroughCount)
                if showThroughCount then
                    self.view.throughCountTxt.text = self.m_unionArgs.currentPassed
                end

                if self.m_unionArgs.speedLimitEnable then
                    self.view.speedSituationNode.speedNumTxt.text = self.m_unionArgs.currentSpeedCount * 6
                    local lineCount = self.m_unionArgs.historySpeedCount.Count
                    if lineCount > 0 then
                        local line = {}
                        for i = 0, lineCount - 1 do
                            table.insert(line, self.m_unionArgs.historySpeedCount[i] / self.m_speedSliderMax)
                        end
                        self.view.speedSituationNode.speedLine:InitBrokenLine(line, lineCount)
                    else
                        self.view.speedSituationNode.speedLine:InitBrokenLine()
                    end
                end
            end
        end
    end)

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(self.m_nodeId)
end

FacConditionerSelectCtrl.OnClose = HL.Override() << function(self)
    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)
    self.m_addBtnPressCoroutine = self:_ClearCoroutine(self.m_addBtnPressCoroutine)
    self.m_reduceBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceBtnPressCoroutine)
    self.m_addSpeedBtnPressCoroutine = self:_ClearCoroutine(self.m_addSpeedBtnPressCoroutine)
    self.m_reduceSpeedBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceSpeedBtnPressCoroutine)
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_nodeId)
end

FacConditionerSelectCtrl._InitValveNode = HL.Method() << function(self)
    self.m_curValveNode = self.m_fluidType and self.view.liquidNode or self.view.solidityNode
    self.m_curEmptyBtn = self.m_fluidType and self.view.liquiditemEmptyBtn or self.view.itemEmptyBtn
    self.m_curValveNode.gameObject:SetActiveIfNecessary(true)
    self.m_curEmptyBtn.gameObject:SetActiveIfNecessary(true)
    self.view.buildingCommon.view.machineBg.gameObject:SetActiveIfNecessary(not self.m_fluidType)
    self.view.buildingCommon.view.pipeBg.gameObject:SetActiveIfNecessary(self.m_fluidType)

    self.m_countSliderStep = Tables.factoryConst.facValveCountSliderStep
    self.m_countSliderMin = Tables.factoryConst.facValveCountSliderMin
    self.m_countSliderMax = Tables.factoryConst.facValveCountSliderMax
    self.m_speedSliderStep = self.m_fluidType and Tables.factoryConst.facFluidValveSpeedSliderStep or Tables.factoryConst.facBoxValveSpeedSliderStep
    self.m_speedSliderDefault = self.m_fluidType and Tables.factoryConst.facFluidValveSpeedSliderDefault or Tables.factoryConst.facBoxValveSpeedSliderDefault
    self.m_speedSliderMin = self.m_fluidType and Tables.factoryConst.facFluidValveSpeedSliderMin or Tables.factoryConst.facBoxValveSpeedSliderMin
    self.m_speedSliderMax = self.m_fluidType and Tables.factoryConst.facFluidValveSpeedSliderMax or Tables.factoryConst.facBoxValveSpeedSliderMax

    if Utils.isInBlackbox() then
        if self.m_uiInfo.nodeHandler
            and self.m_uiInfo.nodeHandler.predefinedParam
            and self.m_uiInfo.nodeHandler.predefinedParam.valve
            and self.m_uiInfo.nodeHandler.predefinedParam.valve.isSpeedLimitedTechUnlock then
            self.m_speedLimitedUnlock = true
        else
            self.m_speedLimitedUnlock = false
        end
    else
        self.m_speedLimitedUnlock = not GameInstance.player.facTechTreeSystem:NodeIsLocked(SPEED_LIMITED_TECH_TREE_ID)
    end

    if DeviceInfo.usingController then
        self.m_btnPressFastChangeRate = REFRESH_CONTROLLER_RATE
    end

    if self.m_fluidType then
        self.m_curValveNode.facCachePipe:InitFacCachePipe(self.m_uiInfo, {
            useSinglePipe = true,
            stateRefreshCallback = function(portInfo)
                self:_RefreshPortBlockState(portInfo.isBlock)
            end
        })
    else
        self.m_curValveNode.facCacheBelt:InitFacCacheBelt(self.m_uiInfo, {
            noGroup = true,
            stateRefreshCallback = function(portInfo)
                self:_RefreshPortBlockState(portInfo.isBlock)
            end
        })
    end

    self.view.resetBtn.onClick:AddListener(function()
        GameInstance.player.remoteFactory.core:Message_ResetValveRecord(ScopeUtil.GetCurrentChapterId(), self.m_unionArgs.componentId)
    end)
    self.view.switchBtn.onClick:AddListener(function()
        self:_ShowSelectPanel()
    end)
    self.m_curEmptyBtn.onClick:AddListener(function()
        self:_ShowSelectPanel()
    end)
    self.view.commonToggle:InitCommonToggle(function(isOn)
        GameInstance.player.remoteFactory.core:Message_SetValveLimit(
            ScopeUtil.GetCurrentChapterId(),
            self.m_unionArgs.componentId,
            isOn,
            self.view.ratedCountSlider.value * self.m_countSliderStep,
            function()
                self:_RefreshThroughCountState()
            end)
    end, self.m_unionArgs.enabled, true)

    self.view.ratedCountMinTxt.text = tostring(self.m_countSliderMin * self.m_countSliderStep)
    self.view.ratedCountMaxTxt.text = tostring(self.m_countSliderMax * self.m_countSliderStep)
    self.view.ratedCountSlider.minValue = self.m_countSliderMin
    self.view.ratedCountSlider.maxValue = self.m_countSliderMax
    self.view.ratedCountSlider.onValueChanged:AddListener(function(newNum)
        self:_OnSliderChanged(newNum)
    end)
    self.view.ratedCountSlider.onEndDragSlider:AddListener(function(newNum)
        GameInstance.player.remoteFactory.core:Message_SetValveLimit(
            ScopeUtil.GetCurrentChapterId(),
            self.m_unionArgs.componentId,
            true,
            newNum * self.m_countSliderStep)
    end)
    self.view.ratedCountSlider.onClickSlider:AddListener(function(newNum)
        GameInstance.player.remoteFactory.core:Message_SetValveLimit(
            ScopeUtil.GetCurrentChapterId(),
            self.m_unionArgs.componentId,
            true,
            newNum * self.m_countSliderStep)
    end)
    self.view.ratedCountMinBtn.onPressStart:AddListener(function()
        if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
            return
        end
        self.view.ratedCountSlider.value = self.view.ratedCountSlider.value - 1
        self.m_reduceBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceBtnPressCoroutine)
        self.m_reduceBtnPressCoroutine = self:_StartCoroutine(function()
            local refreshCount = 0
            while true do
                local fastMode = refreshCount >= REFRESH_INTERVAL_CHANGE_COUNT
                local refreshInterval = fastMode and REFRESH_FAST_INTERVAL or REFRESH_INTERVAL
                local changeNum = fastMode and self.m_btnPressFastChangeRate or 1
                coroutine.wait(refreshInterval)
                if not self.view.ratedCountMinBtn.groupEnabled or self.view.ratedCountSlider.value <= self.view.ratedCountSlider.minValue then
                    self.m_reduceBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceBtnPressCoroutine)
                end
                self.view.ratedCountSlider.value = self.view.ratedCountSlider.value - changeNum
                refreshCount = refreshCount + 1
            end
        end)
    end)
    self.view.ratedCountMinBtn.onPressEnd:AddListener(function()
        self.m_reduceBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceBtnPressCoroutine)
        GameInstance.player.remoteFactory.core:Message_SetValveLimit(
            ScopeUtil.GetCurrentChapterId(),
            self.m_unionArgs.componentId,
            true,
            self.view.ratedCountSlider.value * self.m_countSliderStep)
    end)
    self.view.ratedCountMaxBtn.onPressStart:AddListener(function()
        if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
            return
        end
        self.view.ratedCountSlider.value = self.view.ratedCountSlider.value + 1
        self.m_addBtnPressCoroutine = self:_ClearCoroutine(self.m_addBtnPressCoroutine)
        self.m_addBtnPressCoroutine = self:_StartCoroutine(function()
            local refreshCount = 0
            while true do
                local fastMode = refreshCount >= REFRESH_INTERVAL_CHANGE_COUNT
                local refreshInterval = fastMode and REFRESH_FAST_INTERVAL or REFRESH_INTERVAL
                local changeNum = fastMode and self.m_btnPressFastChangeRate or 1
                coroutine.wait(refreshInterval)
                if not self.view.ratedCountMaxBtn.groupEnabled or self.view.ratedCountSlider.value <= self.view.ratedCountSlider.minValue then
                    self.m_addBtnPressCoroutine = self:_ClearCoroutine(self.m_addBtnPressCoroutine)
                end
                self.view.ratedCountSlider.value = self.view.ratedCountSlider.value + changeNum
                refreshCount = refreshCount + 1
            end
        end)
    end)
    self.view.ratedCountMaxBtn.onPressEnd:AddListener(function()
        self.m_addBtnPressCoroutine = self:_ClearCoroutine(self.m_addBtnPressCoroutine)
        GameInstance.player.remoteFactory.core:Message_SetValveLimit(
            ScopeUtil.GetCurrentChapterId(),
            self.m_unionArgs.componentId,
            true,
            self.view.ratedCountSlider.value * self.m_countSliderStep)
    end)
    self.view.ratedCountSlider.value = self.m_unionArgs.valvePassed / self.m_countSliderStep


    self.view.manageNode.gameObject:SetActiveIfNecessary(self.m_speedLimitedUnlock)
    self.view.speedLimitNode.gameObject:SetActiveIfNecessary(self.m_speedLimitedUnlock)
    if self.m_speedLimitedUnlock then
        self.view.speedSituationNode.topNumTxt.text = self.m_speedSliderMax * self.m_speedSliderStep
        self.view.managerBtn.onClick:AddListener(function()
            self:_ShowManagePanel()
        end)
        self.m_speedCountAll, self.m_speedCountMax = FactoryUtils.getValveSpeedLimitedCount()
        if self.m_speedCountAll >= self.m_speedCountMax then
            self.view.managerNumTxt.text = string.format(COLOR_TEXT_FORMAT, self.m_speedCountAll, self.m_speedCountMax)
        else
            self.view.managerNumTxt.text = self.m_speedCountAll .. "/" .. self.m_speedCountMax
        end
        self.view.speedLimitNode.commonToggle:InitCommonToggle(function(isOn)
            if isOn and self.m_speedCountAll >= self.m_speedCountMax then
                self.view.speedLimitNode.commonToggle:SetValue(false, true)
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_VALVE_SPEED_LIMITED_MAX_TIPS)
                return
            end
            GameInstance.player.remoteFactory.core:Message_SetValveSpeedLimit(
                ScopeUtil.GetCurrentChapterId(),
                self.m_unionArgs.componentId,
                isOn,
                self.view.speedLimitNode.slider.value * self.m_speedSliderStep,
                function()
                    self:_RefreshSpeedLimitState()
                end)
        end, self.m_unionArgs.speedLimitEnable, true)
        self.view.speedLimitNode.minText.text = tostring(self.m_speedSliderMin * self.m_speedSliderStep)
        self.view.speedLimitNode.maxText.text = tostring(self.m_speedSliderMax * self.m_speedSliderStep)
        self.view.speedLimitNode.slider.minValue = self.m_speedSliderMin
        self.view.speedLimitNode.slider.maxValue = self.m_speedSliderMax
        self.view.speedLimitNode.slider.onValueChanged:AddListener(function(newNum)
            self:_OnSpeedSliderChanged(newNum)
        end)
        self.view.speedLimitNode.slider.onEndDragSlider:AddListener(function(newNum)
            GameInstance.player.remoteFactory.core:Message_SetValveSpeedLimit(
                ScopeUtil.GetCurrentChapterId(),
                self.m_unionArgs.componentId,
                true,
                newNum * self.m_speedSliderStep)
        end)
        self.view.speedLimitNode.slider.onClickSlider:AddListener(function(newNum)
            GameInstance.player.remoteFactory.core:Message_SetValveSpeedLimit(
                ScopeUtil.GetCurrentChapterId(),
                self.m_unionArgs.componentId,
                true,
                newNum * self.m_speedSliderStep)
        end)
        self.view.speedLimitNode.minButton.onPressStart:AddListener(function()
            if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
                return
            end
            self.view.speedLimitNode.slider.value = self.view.speedLimitNode.slider.value - 1
            self.m_reduceSpeedBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceSpeedBtnPressCoroutine)
            self.m_reduceSpeedBtnPressCoroutine = self:_StartCoroutine(function()
                local refreshCount = 0
                while true do
                    local fastMode = refreshCount >= REFRESH_INTERVAL_CHANGE_COUNT
                    local refreshInterval = fastMode and REFRESH_FAST_INTERVAL or REFRESH_INTERVAL
                    local changeNum = 1
                    coroutine.wait(refreshInterval)
                    if not self.view.speedLimitNode.minButton.groupEnabled or self.view.speedLimitNode.slider.value <= self.view.speedLimitNode.slider.minValue then
                        self.m_reduceSpeedBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceSpeedBtnPressCoroutine)
                    end
                    self.view.speedLimitNode.slider.value = self.view.speedLimitNode.slider.value - changeNum
                    refreshCount = refreshCount + 1
                end
            end)
        end)
        self.view.speedLimitNode.minButton.onPressEnd:AddListener(function()
            self.m_reduceSpeedBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceSpeedBtnPressCoroutine)
            GameInstance.player.remoteFactory.core:Message_SetValveSpeedLimit(
                ScopeUtil.GetCurrentChapterId(),
                self.m_unionArgs.componentId,
                true,
                self.view.speedLimitNode.slider.value * self.m_speedSliderStep)
        end)
        self.view.speedLimitNode.maxButton.onPressStart:AddListener(function()
            if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
                return
            end
            self.view.speedLimitNode.slider.value = self.view.speedLimitNode.slider.value + 1
            self.m_addSpeedBtnPressCoroutine = self:_ClearCoroutine(self.m_addSpeedBtnPressCoroutine)
            self.m_addSpeedBtnPressCoroutine = self:_StartCoroutine(function()
                local refreshCount = 0
                while true do
                    local fastMode = refreshCount >= REFRESH_INTERVAL_CHANGE_COUNT
                    local refreshInterval = fastMode and REFRESH_FAST_INTERVAL or REFRESH_INTERVAL
                    local changeNum = 1
                    coroutine.wait(refreshInterval)
                    if not self.view.speedLimitNode.maxButton.groupEnabled or self.view.speedLimitNode.slider.value <= self.view.speedLimitNode.slider.minValue then
                        self.m_addSpeedBtnPressCoroutine = self:_ClearCoroutine(self.m_addSpeedBtnPressCoroutine)
                    end
                    self.view.speedLimitNode.slider.value = self.view.speedLimitNode.slider.value + changeNum
                    refreshCount = refreshCount + 1
                end
            end)
        end)
        self.view.speedLimitNode.maxButton.onPressEnd:AddListener(function()
            self.m_addSpeedBtnPressCoroutine = self:_ClearCoroutine(self.m_addSpeedBtnPressCoroutine)
            GameInstance.player.remoteFactory.core:Message_SetValveSpeedLimit(
                ScopeUtil.GetCurrentChapterId(),
                self.m_unionArgs.componentId,
                true,
                self.view.speedLimitNode.slider.value * self.m_speedSliderStep)
        end)
        if self.m_unionArgs.speedLimitValue < 0 then
            self.view.speedLimitNode.slider.value = self.m_speedSliderDefault
            self:_OnSpeedSliderChanged(self.m_speedSliderDefault)
        else
            self.view.speedLimitNode.slider.value = self.m_unionArgs.speedLimitValue
            self:_OnSpeedSliderChanged(self.m_unionArgs.speedLimitValue)
        end
    end

    self.m_cacheShowItemTipsBindingId = InputManagerInst:CreateBindingByActionId("show_item_tips", function()
        local itemId = self.m_unionArgs.selectedItemId
        local itemExist = not string.isEmpty(itemId)
        if itemExist then
            self.m_curValveNode.item:ShowTips()
        end
    end, self.view.naviDirGroup.groupId)
    InputManagerInst:ToggleBinding(self.m_cacheShowItemTipsBindingId, false)
end

FacConditionerSelectCtrl._InitBlackBoxState = HL.Method() << function(self)
    if not Utils.isInBlackbox()
        or not self.m_uiInfo.nodeHandler
        or not self.m_uiInfo.nodeHandler.predefinedParam
        or not self.m_uiInfo.nodeHandler.predefinedParam.valve then
        return
    end

    self.m_blackBoxLockItem = self.m_uiInfo.nodeHandler.predefinedParam.valve.isItemLocked
    local lockSlider = self.m_uiInfo.nodeHandler.predefinedParam.valve.isItemCountLocked
    local lockToggle = self.m_uiInfo.nodeHandler.predefinedParam.valve.isItemPassLocked

    if self.m_blackBoxLockItem then
        self.view.lockItemNode.gameObject:SetActiveIfNecessary(true)
    else
        self.view.lockItemNode:PlayOutAnimation(function()
            self.view.lockItemNode.gameObject:SetActiveIfNecessary(false)
        end)
    end
    self.view.unlockItemNode.gameObject:SetActiveIfNecessary(not self.m_blackBoxLockItem)
    self.view.sliderNode.gameObject:SetActiveIfNecessary(not lockSlider)
    self.view.lockSliderNode.gameObject:SetActiveIfNecessary(lockSlider)
    self.view.lLockIcon.gameObject:SetActiveIfNecessary(lockToggle)
    self.view.rLockIcon.gameObject:SetActiveIfNecessary(lockToggle)
    self.view.commonToggle:ToggleInteractable(not lockToggle)

    local lockSpeedSlider = self.m_uiInfo.nodeHandler.predefinedParam.valve.limitSpeedLocked
    local lockSpeedToggle = self.m_uiInfo.nodeHandler.predefinedParam.valve.isSpeedLimitedLocked
    self.view.speedLimitNode.sliderNode.gameObject:SetActiveIfNecessary(not lockSpeedSlider)
    self.view.speedLimitNode.lockSliderNode.gameObject:SetActiveIfNecessary(lockSpeedSlider)
    self.view.speedLimitNode.lLockIcon.gameObject:SetActiveIfNecessary(lockSpeedToggle)
    self.view.speedLimitNode.rLockIcon.gameObject:SetActiveIfNecessary(lockSpeedToggle)
    self.view.speedLimitNode.commonToggle:ToggleInteractable(not lockSpeedToggle)
end

FacConditionerSelectCtrl._OnSliderChanged = HL.Method(HL.Number) << function(self, number)
    self.view.ratedCountTxt.text = tostring(math.floor(number * self.m_countSliderStep))
end

FacConditionerSelectCtrl._OnSpeedSliderChanged = HL.Method(HL.Number) << function(self, number)
    self.view.speedLimitNode.speedText.text = tostring(math.floor(number * self.m_speedSliderStep))
    if number > 0 then
        self.view.speedLimitNode.speedText.color = self.view.config.NORMAL_SPEED_COLOR
    else
        self.view.speedLimitNode.speedText.color = self.view.config.ZERO_SPEED_COLOR
    end
    self.view.speedSituationNode.lineImage.transform.anchoredPosition = Vector2(self.view.speedSituationNode.lineImage.transform.anchoredPosition.x, number * SPEED_LINE_POSY / self.m_speedSliderMax)
end

FacConditionerSelectCtrl._RefreshPortBlockState = HL.Method(HL.Boolean) << function(self, isBlock)
    local state = isBlock and GEnums.FacBuildingState.Blocked or GEnums.FacBuildingState.Normal
    self.view.buildingCommon:ChangeBuildingStateDisplay(state)
end

FacConditionerSelectCtrl._RefreshSelectItem = HL.Method(HL.Opt(HL.Boolean)) << function(self, init)
    if IsNull(self.view.gameObject) then
        return
    end
    self.m_uiInfo:Update()
    local itemId = self.m_unionArgs.selectedItemId
    local itemExist = not string.isEmpty(itemId)

    self.m_curValveNode.item.gameObject:SetActiveIfNecessary(itemExist)
    self.view.passedNode.gameObject:SetActiveIfNecessary(itemExist)
    self.view.passedTitleNode.gameObject:SetActiveIfNecessary(itemExist)
    self.view.speedSituationNode.gameObject:SetActiveIfNecessary(itemExist and self.m_speedLimitedUnlock)
    self.view.switchText.text = itemExist and Language["key_hint_fac_unloader_replace_item"] or Language["key_hint_fac_unloader_add_item"]
    self.view.switchIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, itemExist and "icon_tips_replace" or "icon_tips_add")
    InputManagerInst:ToggleBinding(self.m_cacheShowItemTipsBindingId, itemExist)
    if itemExist then
        if not init then
            GameInstance.player.remoteFactory.core:Message_ResetValveRecord(ScopeUtil.GetCurrentChapterId(), self.m_unionArgs.componentId)
        end
        self.m_curValveNode.item:InitItem({id = itemId, count = 1}, true)
        self.view.commonToggle:SetValue(self.m_unionArgs.enabled, true)
        self:_RefreshThroughCountState()

        if self.m_speedLimitedUnlock then
            self.view.speedLimitNode.commonToggle:SetValue(self.m_unionArgs.speedLimitEnable, true)
        end
        self:_RefreshSpeedLimitState()
    else

        self.m_speedCountAll, self.m_speedCountMax = FactoryUtils.getValveSpeedLimitedCount()
        if self.m_speedCountAll >= self.m_speedCountMax then
            self.view.managerNumTxt.text = string.format(COLOR_TEXT_FORMAT, self.m_speedCountAll, self.m_speedCountMax)
        else
            self.view.managerNumTxt.text = self.m_speedCountAll .. "/" .. self.m_speedCountMax
        end
    end
end

FacConditionerSelectCtrl._RefreshThroughCountState = HL.Method() << function(self)
    if IsNull(self.view.gameObject) then
        return
    end
    self.m_uiInfo:Update()
    local open = self.m_unionArgs.enabled
    self.view.openNode.gameObject:SetActiveIfNecessary(open)
    self.view.closeNode.gameObject:SetActiveIfNecessary(not open)
    self.view.tipsNode.gameObject:SetActiveIfNecessary(not open)

    local showThroughCount = self.m_unionArgs.currentPassed and self.m_unionArgs.currentPassed > 0
    self.view.throughCountTxt.gameObject:SetActiveIfNecessary(showThroughCount)
    self.view.throughCountEmpty.gameObject:SetActiveIfNecessary(not showThroughCount)
    if showThroughCount then
        self.view.throughCountTxt.text = self.m_unionArgs.currentPassed
    end
end

FacConditionerSelectCtrl._RefreshSpeedLimitState = HL.Method() << function(self)
    if IsNull(self.view.gameObject) or not self.m_speedLimitedUnlock then
        return
    end
    self.m_uiInfo:Update()
    local open = self.m_unionArgs.speedLimitEnable
    self.view.speedLimitNode.openNode.gameObject:SetActiveIfNecessary(open)
    self.view.speedLimitNode.closeNode.gameObject:SetActiveIfNecessary(not open)
    self.view.speedSituationNode.stateController:SetState(open and "Normal" or "Empty")
    if open then
        if self.view.speedSituationNode.arrowAnim.curTween == nil then
            self.view.speedSituationNode.arrowAnim:PlayLoopAnimation()
        else
            self.view.speedSituationNode.arrowAnim.curTween.handler.onComplete = function()
                self.view.speedSituationNode.arrowAnim:PlayLoopAnimation()
            end
        end
        self.view.speedSituationNode.lineImage.gameObject:SetActiveIfNecessary(true)
        self.view.speedSituationNode.lineImage.transform.anchoredPosition = Vector2(self.view.speedSituationNode.lineImage.transform.anchoredPosition.x, self.m_unionArgs.speedLimitValue * SPEED_LINE_POSY / self.m_speedSliderMax)
        self.view.speedSituationNode.speedNumTxt.text = self.m_unionArgs.currentSpeedCount * 6
    else
        if self.view.speedSituationNode.arrowAnim.curTween == nil then
            self.view.speedSituationNode.arrowAnim:PlayOutAnimation()
        else
            self.view.speedSituationNode.arrowAnim.curTween.handler.onStepComplete = function()
                self.view.speedSituationNode.arrowAnim:PlayOutAnimation()
            end
        end
        self.view.speedSituationNode.lineImage.gameObject:SetActiveIfNecessary(false)
    end

    self.m_speedCountAll, self.m_speedCountMax = FactoryUtils.getValveSpeedLimitedCount()
    if self.m_speedCountAll >= self.m_speedCountMax then
        self.view.managerNumTxt.text = string.format(COLOR_TEXT_FORMAT, self.m_speedCountAll, self.m_speedCountMax)
    else
        self.view.managerNumTxt.text = self.m_speedCountAll .. "/" .. self.m_speedCountMax
    end

    local lineCount = self.m_unionArgs.historySpeedCount.Count
    if lineCount > 0 and open then
        local line = {}
        for i = 0, lineCount - 1 do
            table.insert(line, self.m_unionArgs.historySpeedCount[i] / self.m_speedSliderMax)
        end
        self.view.speedSituationNode.speedLine:InitBrokenLine(line, lineCount)
    else
        self.view.speedSituationNode.speedLine:InitBrokenLine()
    end
end

FacConditionerSelectCtrl._ShowSelectPanel = HL.Method() << function(self)
    if self.m_blackBoxLockItem then
        return
    end
    UIManager:AutoOpen(PanelId.FacConditioner, {
        selectItemId = self.m_unionArgs.selectedItemId,
        onClickItem = function(itemId)
            if self.m_unionArgs.selectedItemId == itemId then
                itemId = ""
            end
            GameInstance.player.remoteFactory.core:Message_SetValveItem(
                ScopeUtil.GetCurrentChapterId(),
                self.m_unionArgs.componentId,
                itemId,
                function()
                    self:_RefreshSelectItem()
                end)
        end,
        isFluid = self.m_fluidType,
    })
    UIManager:SetTopOrder(PanelId.FacConditioner)
end

FacConditionerSelectCtrl._ShowManagePanel = HL.Method() << function(self)
    UIManager:AutoOpen(PanelId.FacConditionerManage, {
        onClose = function()
            self.view.speedLimitNode.commonToggle:SetValue(self.m_unionArgs.speedLimitEnable, true)
            self:_RefreshSpeedLimitState()
        end,
    })
    UIManager:SetTopOrder(PanelId.FacConditionerManage)
end


HL.Commit(FacConditionerSelectCtrl)
