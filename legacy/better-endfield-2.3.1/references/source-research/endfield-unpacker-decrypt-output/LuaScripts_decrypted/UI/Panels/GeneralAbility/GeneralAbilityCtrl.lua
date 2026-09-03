local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GeneralAbility
local GeneralAbilityType = GEnums.GeneralAbilityType
local AbilityState = CS.Beyond.Gameplay.GeneralAbilitySystem.AbilityState
local PlayerController = CS.Beyond.Gameplay.Core.PlayerController
local UnlockSystemType = GEnums.UnlockSystemType
local ForbidStyle = CS.Beyond.Gameplay.GeneralAbilityForbidParams.ForbidStyle
local TempAbilityActiveState = CS.Beyond.Gameplay.GeneralAbilitySystem.TempAbilityActiveState

local MainState = {
    MobileMoveState = "MobileMoveState",
    MobileMoveCancelState = "MobileMoveCancelState",
    PC = "Pc",
    Controller = "Controller",
}

local SelectingNodeState = {
    None = "None",
    NormalState = "NormalState",
    TempAbilityEmpty = "TempAbilityEmpty",
    TempAbilityExist = "TempAbilityExist",

    ShowSelectedNode = "ShowSelectedNode",
    HideSelectedNode = "HideSelectedNode",

    MobileSelectingState = "MobileSelectingState",
    MobileCancelState = "MobileCancelState",

    HighLightNormalColor = "HighLightNormalColor",
    HighLightDisableColor = "HighLightDisableColor",
}

local SelectingMidState = {
    None = "None",
    InitialState = "InitialState",
    SelectState = "SelectState",
    CancelState = "CancelState",
    DisableState = "DisableState",
}


GeneralAbilityCtrl = HL.Class('GeneralAbilityCtrl', uiCtrl.UICtrl)

local SELECTED_ABILITY_TYPE_CLIENT_LOCAL_DATA_KEY = "selected_general_ability"
local INVALID_ABILITY_TYPE = -1
local PRESS_ANIMATION_NAME = "generalability_press"
local RELEASE_ANIMATION_NAME = "generalability_release"
local RIGHT_ABILITY_VALID_ANIMATION_NAME = "generalability_unlock"
local SELECTOR_NORMAL_ANIMATION_NAME = "generalability_selector_cell_default"
local SELECTOR_HOVER_ANIMATION_NAME = "generalability_selector_cell_highlight"
local MOBILE_CANCEL_PANEL_VALID_ANIMATION_NAME = "generalabilitydelete_red"
local MOBILE_CANCEL_PANEL_INVALID_ANIMATION_NAME = "generalabilitydelete_normal"
local MOBILE_CANCEL_SELECTOR_VALID_ANIMATION_NAME = "generalabilityselectorlist_red"
local MOBILE_CANCEL_SELECTOR_INVALID_ANIMATION_NAME = "generalabilityselectorlist_normal"

local RIGHT_TEMP_ABILITY_IN = "generalability_temp_ability_in"
local RIGHT_TEMP_ABILITY_LOOP = "generalability_temp_ability_loop"
local RIGHT_TEMP_ABILITY_DEFAULT = "generalability_temp_ability_default"

local SELECTOR_CANCEL_ACTION_ID = "general_ability_selector_quit"
local SELECTOR_CLICK_ACTION_ID = "general_ability_selector_click"

local PC_VALID_OP_VALUE = 10000
local MAX_SELECTED_LUA_ID = 8
local FORBID_SELECT_ICON_ALPHA = 0.3
local NOT_FORBID_SELECT_ICON_ALPHA = 1

local RIGHT_STICK_DEAD_ZONE_VALUE = 0.5

local MOBILE_HOVER_ANGLE_RANGE = {
    { 0, 45 },
    { 45, 90 },
    { 90, 135 },
    { 135, 180 },
    { 180, 225 },
    { 225, 270 },
    { 270, 315 },
    { 315, 360 },
}

local BAN_MAP_TOAST = "ui_toast_generalability_cannot_choose"
local MOBILE_SELECTOR_OFFSET_ANGLE = 67.5
local CONTROLLER_SELECTOR_OFFSET_ANGLE = 112.5
local PC_SELECTOR_OFFSET_ANGLE = 112.5
local CONTROLLER_INVALID_ANGLE = -1
GeneralAbilityCtrl.m_isValid = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_abilityRegisterConfig = HL.Field(HL.Table)

GeneralAbilityCtrl.m_abilityCells = HL.Field(HL.Forward("UIListCache"))

GeneralAbilityCtrl.m_abilityDataList = HL.Field(HL.Table)

GeneralAbilityCtrl.m_tempAbilityDataList = HL.Field(HL.Table)

GeneralAbilityCtrl.m_tempSelectCell = HL.Field(HL.Any)

GeneralAbilityCtrl.m_tempRightDecoCell = HL.Field(HL.Any)

GeneralAbilityCtrl.m_abilityDataMap = HL.Field(HL.Table)

GeneralAbilityCtrl.m_lastSelectedAbilityType = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_selectedAbilityType = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_tipsAbilityType = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_screenOutVersion = HL.Field(HL.Number) << 0
GeneralAbilityCtrl.m_screenOutCallbackVersion = HL.Field(HL.Number) << 0

GeneralAbilityCtrl.m_isInPool = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_isInGasPool = HL.Field(HL.Boolean) << false;

GeneralAbilityCtrl.m_selectedAbilityPressTick = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_unLockNormalNum = HL.Field(HL.Number) << 0

GeneralAbilityCtrl.m_pressRTime = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_openStartTime = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_openSelectorTime = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_mobileHoverCancelFlag = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_mobileOpenSelector = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_isSelectorShown = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_decoCells = HL.Field(HL.Forward("UIListCache"))

GeneralAbilityCtrl.m_clickEnabled = HL.Field(HL.Boolean) << true

GeneralAbilityCtrl.m_longClickEnabled = HL.Field(HL.Boolean) << true

GeneralAbilityCtrl.m_hoverSelectorType = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_canReleaseCloseSelector = HL.Field(HL.Boolean) << true

GeneralAbilityCtrl.m_selectorCancelBinding = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_selectorClickBinding = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_isSwitchTipShown = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_mobileSelectorTick = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_mobileBtnScreenPosition = HL.Field(Vector2)

GeneralAbilityCtrl.m_mobileHoverAbilityIndex = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_mobileMoveCircleSqr = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_mobileCancelCircleSqr = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_mobileIsInCancelCircle = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_mobileUseAbilityFlag = HL.Field(HL.Boolean) << true

GeneralAbilityCtrl.m_clearScreenKey = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_screenOutFlag = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_needLateRecoverScreen = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_controllerTick = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_currentStickPushed = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_pcSelectValid = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_currentArrowAngle = HL.Field(HL.Number) << 0

GeneralAbilityCtrl.m_controllerHoverIndex = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_iconOverrideTable = HL.Field(HL.Table) << nil

GeneralAbilityCtrl.m_pcTick = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_triggerTickHandle = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_tempAbilityTailTickId = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_interactFacNodeId = HL.Field(HL.Any)

GeneralAbilityCtrl.m_triggerTable = HL.Field(HL.Table)

GeneralAbilityCtrl.m_pcArrowAngle = HL.Field(HL.Number) << 0

GeneralAbilityCtrl.m_lastPcArrowAngle = HL.Field(HL.Number) << 0

GeneralAbilityCtrl.m_pcHoverIndex = HL.Field(HL.Number) << -1

GeneralAbilityCtrl.m_pressStartPos = HL.Field(HL.Any)

GeneralAbilityCtrl.m_changeKeyBinding = HL.Field(HL.Table)

GeneralAbilityCtrl.startPress = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_delayUpdateSelectorFlag = HL.Field(HL.Boolean) << false

GeneralAbilityCtrl.m_tempAnimLoopActive = HL.Field(HL.Boolean) << true





GeneralAbilityCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.GENERAL_ABILITY_SYSTEM_CHANGED] = '_OnSystemChanged',
    [MessageConst.GENERAL_ABILITY_FORBID_SELECT_CHANGED] = '_OnForbidSelectChanged',
    [MessageConst.TEMP_ABILITY_SYSTEM_CHANGED] = '_OnTempAbilityChanged',
    [MessageConst.GENERAL_ABILITY_SYSTEM_FORCE_SELECT] = '_OnForceSelectAbility',
    [MessageConst.ON_ENTER_LIQUID_POOL_NEARBY_AREA] = '_OnEnterLiquidPoolNearbyArea',
    [MessageConst.ON_LEAVE_LIQUID_POOL_NEARBY_AREA] = '_OnLeaveLiquidPoolNearbyArea',
    [MessageConst.ON_ENTER_GAS_POOL_NEARBY_AREA] = "_OnEnterGasPoolNearbyArea",
    [MessageConst.ON_LEAVE_GAS_POOL_NEARBY_AREA] = "_OnLeaveGasPoolNearbyArea",
    [MessageConst.ON_WATER_INTERACT] = "_OnWaterInteract",
    [MessageConst.TRY_INTERACT_GAS] = "_TryInteractGas",
    [MessageConst.ON_GENERAL_ABILITY_USE] = '_OnGeneralAbilityUse',
    [MessageConst.ON_GENERAL_ABILITY_STATE_CHANGE] = '_OnGeneralAbilityStateChange',
    [MessageConst.TOGGLE_GENERAL_ABILITY_CLICK] = '_ToggleGeneralAbilityClick',
    [MessageConst.ON_ABILITY_UPDATE_SELECTED_TYPE] = '_UpdateSelectedType',
    [MessageConst.TOGGLE_GENERAL_ABILITY_LONG_CLICK] = '_ToggleGeneralAbilityLongClick',
    [MessageConst.TOGGLE_GENERAL_ABILITY_CLOSE_WHEEL] = '_ToggleGeneralAbilityCloseWheel',
    [MessageConst.SET_GENERAL_ABILITY_RELEASE_CLOSE] = '_ToggleGeneralAbilityClick',
    [MessageConst.SWITCH_GENERAL_ABILITY_DEBUG_TYPE] = 'SwitchDebugTempAbility',
    [MessageConst.CLEAR_GENERAL_ABILITY_DEBUG_TYPE] = 'ClearDebugTempAbility',
    [MessageConst.GENERAL_ABILITY_OVERRIDE_ICON] = 'GeneralAbilityOverrideIcon',
    [MessageConst.ON_TOGGLE_UI_ACTION] = 'OnToggleUiAction',
    [MessageConst.ACTIVE_BUILDING_LIKE] = 'OnActiveBuildingLike',
    [MessageConst.ON_CONFIRM_CHANGE_INPUT_DEVICE_TYPE] = '_OnChangeInputDeviceType',
    [MessageConst.ON_CHANGE_INPUT_DEVICE_TYPE_FINISHED] = '_OnChangeInputDeviceTypeFinished',
    [MessageConst.GENERAL_ABILITY_CHANGE_KEY_BINDING] = 'GeneralAbilityChangeKeyBinding',
    [MessageConst.GENERAL_ABILITY_ACTIVE_TEMP_ANIM_LOOP] = '_OnActiveTempAnimLoop',
}


GeneralAbilityCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_selectorCancelBinding = self:BindInputPlayerAction(SELECTOR_CANCEL_ACTION_ID, function()
        self:_OnBackButtonClicked()
    end)
    self.m_selectorClickBinding = self:BindInputPlayerAction(SELECTOR_CLICK_ACTION_ID, function()

        self:_OnLeftButtonClicked()
    end)
    InputManagerInst:ToggleBinding(self.m_selectorCancelBinding, false)
    InputManagerInst:ToggleBinding(self.m_selectorClickBinding, false)

    self.view.selectedAbilityButton.onPressStart:AddListener(function()
        if Utils.isCurSquadAllDead() then

            return
        end
        self.startPress = true
        self:_StartPress()
    end)

    self.view.selectedAbilityButton.onPressEnd:AddListener(function()
        if Utils.isCurSquadAllDead() then

            self:_ClearRPress()
            self:_RefreshWheelShownState(false)
            self.startPress = false
            return
        end
        if self.startPress then
            self:_StopPress()
            self.startPress = false
        end
    end)

    self.view.selectedAbilityNodeDragHandler.onDrag:AddListener(function(eventData)
        Notify(MessageConst.MOVE_LEVEL_CAMERA, eventData.delta)
    end)

    self.m_abilityCells = UIUtils.genCellCache(self.view.abilityCell)
    self.m_decoCells = UIUtils.genCellCache(self.view.decoCell)

    self.m_openStartTime = self.view.config.ABILITY_USE_PRESS_DURATION
    self.m_openSelectorTime = self.view.config.ABILITY_SWITCH_PRESS_DURATION

    self.m_iconOverrideTable = {}
    self.m_abilityDataList = {}
    self:_InitAll()
    self:_InitMobileNodes()

    self.view.selectorAnim.gameObject:SetActive(false)

    self:_RefreshMainInputState()

    self.m_triggerTable = {}
    self.m_triggerTickHandle = LuaUpdate:Add("Tick", function(deltaTime)
        self:_UpdateTrigger()
    end)

    self.m_changeKeyBinding = {}




end

GeneralAbilityCtrl.OnClose = HL.Override() << function(self)
    if self.m_triggerTickHandle ~= -1 then
        self.m_triggerTickHandle = LuaUpdate:Remove(self.m_triggerTickHandle)
    end
    self:_RemoveTempAbilityTailTick()
end

GeneralAbilityCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshGasAndWaterTipShownState()
end



GeneralAbilityCtrl.OnHide = HL.Override() << function(self)
    self:_ClearRPress()
    self:_RefreshWheelShownState(false)
end


GeneralAbilityCtrl._UpdateTrigger = HL.Method() << function(self)
    if self.m_triggerTable["OnGeneralAbilityHover"] ~= nil then
        CS.Beyond.Gameplay.Conditions.OnGeneralAbilityHover.Trigger(self.m_triggerTable["OnGeneralAbilityHover"])
        self.m_triggerTable["OnGeneralAbilityHover"] = nil
    end
    if self.m_triggerTable["OnGeneralAbilityUse"] ~= nil then
        CS.Beyond.Gameplay.Conditions.OnGeneralAbilityUse.Trigger()
        self.m_triggerTable["OnGeneralAbilityUse"] = nil
    end
end


GeneralAbilityCtrl._IsAllowedInPlayerController = HL.Method().Return(HL.Boolean) << function(self)
    return GameInstance.playerController:IsPlayerActionEnabled(PlayerController.InputActionType.GeneralAbility)
end




GeneralAbilityCtrl._OnSystemChanged = HL.Method() << function(self)
    self:_InitAll()
end

GeneralAbilityCtrl._OnForbidSelectChanged = HL.Method() << function(self)
    local selectedType = self:_GetSelectedType()
    local abilityData = self.m_abilityDataMap[selectedType]
    if abilityData and abilityData.isForbidSelect then
        self:_ResetSelectedType()
    end

    if not self.m_isSelectorShown then
        self.m_delayUpdateSelectorFlag = true
    end
end

GeneralAbilityCtrl._UpdateSelectedType = HL.Method() << function(self)
    self:_InitSelectedType()
end

GeneralAbilityCtrl._OnTempAbilityChanged = HL.Method() << function(self)
    if not self.m_tempSelectCell then
        self:_InitAll()
        return
    end

    self:_InitAbilityData()
    if self.m_unLockNormalNum ~= 0 or self:_IsHaveTempAbility() then
        self:_UpdateSelectorCellInfo(self.m_tempSelectCell, MAX_SELECTED_LUA_ID)
        if self.m_isSelectorShown then
            self.m_mobileHoverAbilityIndex = INVALID_ABILITY_TYPE
            self.m_controllerHoverIndex = INVALID_ABILITY_TYPE
            self.m_lastPcArrowAngle = CONTROLLER_INVALID_ANGLE
            self.m_pcHoverIndex = INVALID_ABILITY_TYPE
        end
        self:_TempAbilityAnim(self.m_isSelectorShown)
        self.m_isValid = true
        self:_RefreshMainVisible()
    else
        if self.m_isSelectorShown then
            self.m_mobileHoverAbilityIndex = INVALID_ABILITY_TYPE
            self.m_controllerHoverIndex = INVALID_ABILITY_TYPE
            self.m_lastPcArrowAngle = CONTROLLER_INVALID_ANGLE
            self.m_pcHoverIndex = INVALID_ABILITY_TYPE
        end
        self.m_isValid = false
        self:_RefreshMainVisible()
    end

end

GeneralAbilityCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if not active then
        self:_RefreshWheelShownState(false)
    end
end

GeneralAbilityCtrl._OnForceSelectAbility = HL.Method(HL.Any) << function(self, args)
    local type, needHighlight = unpack(args)
    self:_SetSelectedType(type:GetHashCode(), true)
end

GeneralAbilityCtrl._OnTempAbilityActiveStateChanged = HL.Method(HL.Any) << function(self, args)
    local type, isActive = unpack(args)
    if not isActive and self.m_selectedAbilityType == type:GetHashCode() then
        self:_InitSelectedType()
    end
end

GeneralAbilityCtrl._OnGeneralAbilityUse = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    local abilityType, eventArgs = unpack(args)
    local generalAbilitySystem = GameInstance.player.generalAbilitySystem
    local haveCallBack = generalAbilitySystem:HaveAbilityUseFunc(abilityType)
    if haveCallBack == nil then
        return
    end

    local success, tableData = Tables.generalAbilityTable:TryGetValue(abilityType)
    if not success then
        return
    end

    if not string.isEmpty(tableData.useItem) and not generalAbilitySystem:IsIgnoreItemCheck(abilityType)  then
        self:_UseAbilityItem(tableData.useItem)
    else
        generalAbilitySystem:UseAbilityCallBack(abilityType, eventArgs)
    end
end

GeneralAbilityCtrl._OnGeneralAbilityStateChange = HL.Method(HL.Table) << function(self, args)
    local abilityType, fromState, toState = unpack(args)
    local isTempAbility = GameInstance.player.generalAbilitySystem:IsTempAbility(abilityType:GetHashCode())
    local isSelectedTempAbility = GameInstance.player.generalAbilitySystem:IsTempAbility(self.m_selectedAbilityType)
    if isTempAbility then

        self:_UpdateAvailableTempAbility()
        self:_UpdateSelectorCellInfo(self.m_tempSelectCell, MAX_SELECTED_LUA_ID)
    end
    local currData = self.m_abilityDataMap[abilityType:GetHashCode()]
    if currData ~= nil then
        currData.isForbidSelect = toState == AbilityState.ForbiddenSelect
        local currDeco = self.m_decoCells:GetItem(currData.index)
        if currDeco ~= nil then
            local selected = abilityType:GetHashCode() == self.m_selectedAbilityType
            if isTempAbility and isSelectedTempAbility then
                if not selected then

                    return
                elseif toState ~= AbilityState.Idle then

                    for _, abilityData in ipairs(self.m_tempAbilityDataList) do
                        local abilityType = abilityData.type
                        if abilityType ~= self.m_selectedAbilityType
                            and abilityData.abilityRuntimeData.state == AbilityState.Idle then
                            self:_SetSelectedType(abilityType, true)
                            return
                        end
                    end
                end
            end
            self:_RefreshDecoVisible(currData.index, currDeco, selected)
        end
        if self.m_isSelectorShown then
            self:_UpdateSelectorCellInfo(self.m_abilityCells:Get(currData.index), currData.index)
        end
    end
end

GeneralAbilityCtrl._OnSetGeneralAbilityReleaseClose = HL.Method(HL.Any) << function(self, args)
    self.m_canReleaseCloseSelector = unpack(args)
end

GeneralAbilityCtrl._ToggleGeneralAbilityClick = HL.Method(HL.Any) << function(self, args)
    self.m_clickEnabled = unpack(args)
end

GeneralAbilityCtrl._ToggleGeneralAbilityLongClick = HL.Method(HL.Any) << function(self, args)
    self.m_longClickEnabled = unpack(args)
end

GeneralAbilityCtrl._ToggleGeneralAbilityCloseWheel = HL.Method(HL.Any) << function(self, args)
    local selectedType = unpack(args)
    self:_RefreshWheelShownState(false)
    self:_OnSelectByType(selectedType)
end

GeneralAbilityCtrl._OnChangeInputDeviceType = HL.Method(HL.Any) << function(self, args)
    if GameUtil.mainCharacter == nil then
        return
    end
    local customAbilityCom = GameUtil.mainCharacter.customAbilityCom
    if customAbilityCom:IsInCustomAbility() then
        customAbilityCom:TryEndAbility_ByChangeInputDeviceType()
    end
end

GeneralAbilityCtrl._OnChangeInputDeviceTypeFinished = HL.Method(HL.Any) << function(self, args)
    self:_RefreshMainInputState()
end

GeneralAbilityCtrl._RefreshMainInputState = HL.Method() << function(self)
    if DeviceInfo.usingKeyboard then
        self.view.mainStateController:SetState(MainState.PC)
    elseif DeviceInfo.usingController then
        self.view.mainStateController:SetState(MainState.Controller)
    end
end




GeneralAbilityCtrl._TryEnterWaterDroneAbility = HL.StaticMethod() << function()
    local mainCharacter = GameUtil.mainCharacter
    if mainCharacter == nil then
        return
    end
    if mainCharacter.customAbilityCom.hasWaterDroneInfiniteLiquidTag then
        mainCharacter.customAbilityCom:TryEnterWaterDroneAbility_ByInfinityTag()
    else
        mainCharacter.customAbilityCom:TryEnterWaterDroneAbility_ByItem()
    end
end

GeneralAbilityCtrl._InitAll = HL.Method(HL.Opt(HL.Boolean)) << function(self)
    if self:_ShouldResetTempSelect() then
        self.m_selectedAbilityType = INVALID_ABILITY_TYPE
        GameInstance.player.generalAbilitySystem.selectGeneralAbility = INVALID_ABILITY_TYPE
    end

    self:_InitAbilityData()
    if next(self.m_abilityDataList) then
        self:_InitSelectorCells()
        self:_InitDecoCells()
        self:_InitSelectedType()
        self.m_isValid = true
        self:_RefreshMainVisible()
    else
        self.m_isValid = false
        self:_RefreshMainVisible()
    end
end

GeneralAbilityCtrl._InitAbilityData = HL.Method() << function(self)
    self.m_abilityDataMap = {}
    self:_UpdateNormalAbilityData()
    self:_UpdateTempAbilityData()

    self.m_isInPool = GameWorld.waterSensorSystem.isNearbyFactoryWater
    self.m_isInGasPool = GameWorld.gameMechManager.interactiveFactoryGasBrain.inGasArea
    self:_UpdateGasAndFluidInteractState()
end

GeneralAbilityCtrl._ShouldResetTempSelect = HL.Method().Return(HL.Boolean)  << function(self)
    local shouldResetSelect = true
    if self.m_selectedAbilityType ~= INVALID_ABILITY_TYPE
        and GameInstance.player.generalAbilitySystem:IsTempAbility(self.m_selectedAbilityType)
        and self.m_abilityDataMap[self.m_selectedAbilityType] ~= nil
        and self.m_abilityDataMap[self.m_selectedAbilityType].abilityRuntimeData.isTempActive then
        shouldResetSelect = false
    end
    return shouldResetSelect
end


GeneralAbilityCtrl._UpdateNormalAbilityData = HL.Method() << function(self)
    for index = 1, MAX_SELECTED_LUA_ID - 1 do
        self.m_abilityDataList[index] = nil
    end

    local abilityDataList = {}
    self.m_unLockNormalNum = 0

    for abilityType, abilityTableData in pairs(Tables.generalAbilityTable) do
        if abilityTableData.unlockSystemType ~= UnlockSystemType.None then
            local abilityRuntimeData = GameInstance.player.generalAbilitySystem:GetAbilityRuntimeDataByType(abilityType)
            local abilityState = abilityRuntimeData.state
            local blackbox = GameWorld.worldInfo.curLevel.levelData.blackbox
            if blackbox and blackbox.generalAbility then
                for idx = 0, blackbox.generalAbility.customAbilityStates.Count - 1 do
                    local customEntry = blackbox.generalAbility.customAbilityStates[idx]
                    if customEntry.abilityType == abilityTableData.generalAbilityType then
                        abilityState = customEntry.customState
                        break
                    end
                end
            end
            local data = {
                abilityRuntimeData = abilityRuntimeData,
                type = abilityType,
                sortId = abilityTableData.sortId,
                name = abilityTableData.name,
                isForbidSelect = abilityState == AbilityState.ForbiddenSelect,
            }

            if abilityState ~= AbilityState.None and abilityState ~= AbilityState.Locked then
                table.insert(abilityDataList, data)
                self.m_abilityDataMap[abilityType] = data
                self.m_unLockNormalNum = self.m_unLockNormalNum + 1
            end
        end
    end

    table.sort(abilityDataList, Utils.genSortFunction({ "sortId" }, true))


    for index = 1, #abilityDataList do
        self.m_abilityDataList[index] = abilityDataList[index]
        self.m_abilityDataList[index].index = index
    end

end

GeneralAbilityCtrl._UpdateTempAbilityData = HL.Method() << function(self)
    self.m_tempAbilityDataList = {}
    self.m_abilityDataList[MAX_SELECTED_LUA_ID] = nil

    for abilityType, abilityTableData in pairs(Tables.generalAbilityTable) do
        if abilityTableData.unlockSystemType == UnlockSystemType.None then
            self.m_abilityDataMap[abilityType] = nil

            local abilityRuntimeData = GameInstance.player.generalAbilitySystem:GetAbilityRuntimeDataByType(abilityType)
            local abilityState = abilityRuntimeData.state
            local data = {
                abilityRuntimeData = abilityRuntimeData,
                type = abilityType,
                sortId = abilityTableData.priority,
                name = abilityTableData.name,
                isForbidSelect = abilityState == AbilityState.ForbiddenSelect,
                forbidReasonTextId = abilityRuntimeData.forbidSelectToastId,
            }
            if abilityRuntimeData.isTempActive then
                data.isTempAbility = true
                data.index = MAX_SELECTED_LUA_ID
                table.insert(self.m_tempAbilityDataList, data)
                self.m_abilityDataMap[abilityType] = data
            end
        end
    end

    table.sort(self.m_tempAbilityDataList, Utils.genSortFunction({ "sortId" }, true))

    self:_UpdateAvailableTempAbility()
end

GeneralAbilityCtrl._UpdateAvailableTempAbility = HL.Method() << function(self)
    if self:_IsHaveTempAbility() then
        local firstIdleAbilityData = self.m_tempAbilityDataList[1]
        if #self.m_tempAbilityDataList > 1 then
            for _, abilityData in ipairs(self.m_tempAbilityDataList) do
                if abilityData.abilityRuntimeData.state == AbilityState.Idle then
                    firstIdleAbilityData = abilityData
                    break
                end
            end
        end
        self.m_abilityDataList[MAX_SELECTED_LUA_ID] = firstIdleAbilityData
        self.m_abilityDataList[MAX_SELECTED_LUA_ID].index = MAX_SELECTED_LUA_ID
    end
end


GeneralAbilityCtrl._IsHaveTempAbility = HL.Method().Return(HL.Boolean) << function(self)
    return #self.m_tempAbilityDataList > 0
end


GeneralAbilityCtrl._InitSelectorCells = HL.Method() << function(self)
    self.m_tempSelectCell = nil
    self.m_hoverSelectorType = INVALID_ABILITY_TYPE
    self:_RefreshAllDecoInitState()
    self.m_abilityCells:Refresh(self.view.config.CELL_MAX_COUNT, function(cell, luaIndex)
        local angle = self.view.config.CELL_START_ANGLE - (luaIndex - 1) * self.view.config.CELL_INTERVAL_ANGLE
        cell.transform.localEulerAngles = Vector3(0, 0, angle)
        cell.rotateNode.transform.localEulerAngles = Vector3(0, 0, -angle)
        self:_UpdateSelectorCellInfo(cell, luaIndex)
    end)
end

GeneralAbilityCtrl._UpdateSelectorCellInfo = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local abilityData = self.m_abilityDataList[luaIndex]
    if abilityData ~= nil then
        abilityData.cell = cell
        cell.ability:SetOverrideIcon(self.m_iconOverrideTable[abilityData.type])
        cell.ability:InitGeneralAbilityCell(abilityData.type, false, true)
        cell.ability.ignoreStateChangeEvent = true
        cell.shadowAbility:SetOverrideIcon(self.m_iconOverrideTable[abilityData.type])
        cell.shadowAbility:InitGeneralAbilityCell(abilityData.type, true, true)
        cell.shadowAbility.ignoreStateChangeEvent = true
        cell.shadowAbility.view.gameObject:SetActive(true)
        cell.gameObject.name = "Ability_".."Type"..abilityData.type

        if luaIndex == MAX_SELECTED_LUA_ID then
            self.m_tempSelectCell = cell
            self:_UpdateTempAbilityState(true)
        else
            cell.uiStateCtrl:SetState(SelectingNodeState.NormalState)
        end
        if abilityData.isForbidSelect then
            cell.ability.view.normalNodeCanvasGroup.alpha = FORBID_SELECT_ICON_ALPHA
            cell.shadowAbility.view.normalNodeCanvasGroup.alpha = FORBID_SELECT_ICON_ALPHA
            cell.uiStateCtrl:SetState(SelectingNodeState.HighLightDisableColor)
        else
            cell.ability.view.normalNodeCanvasGroup.alpha = NOT_FORBID_SELECT_ICON_ALPHA
            cell.shadowAbility.view.normalNodeCanvasGroup.alpha = NOT_FORBID_SELECT_ICON_ALPHA
            cell.uiStateCtrl:SetState(SelectingNodeState.HighLightNormalColor)
        end
    else
        cell.ability:SetOverrideIcon(nil)
        cell.ability:InitGeneralAbilityCell()
        cell.ability.ignoreStateChangeEvent = true
        cell.shadowAbility.view.gameObject:SetActive(false)
        cell.gameObject.name = "Ability_".."None"..luaIndex
        if luaIndex == MAX_SELECTED_LUA_ID then
            self.m_tempSelectCell = cell
            self:_UpdateTempAbilityState(false)
        end
    end

    cell.button.enabled = abilityData ~= nil
    cell.animationWrapper:PlayWithTween(SELECTOR_NORMAL_ANIMATION_NAME)

end

GeneralAbilityCtrl._InitSelectedType = HL.Method() << function(self)
    if not self:_ShouldResetTempSelect() then
        return
    end

    local selectedType = self:_GetSelectedType()
    if selectedType == INVALID_ABILITY_TYPE then

        self:_ResetSelectedType()
        return
    end

    if self.m_abilityDataMap[selectedType] == nil then

        self:_ResetSelectedType()
        return
    end

    if self.m_abilityDataMap[selectedType].isForbidSelect then
        self:_ResetSelectedType()
    else
        self:_SetSelectedType(selectedType, false)
    end
end

GeneralAbilityCtrl._InitDecoCells = HL.Method() << function(self)
    self.m_decoCells:Refresh(self.view.config.CELL_MAX_COUNT, function(cell, luaIndex)
        local angle = self.view.config.DECO_START_ANGLE - (luaIndex - 1) * self.view.config.CELL_INTERVAL_ANGLE
        cell.transform.localEulerAngles = Vector3(0, 0, angle)
        if luaIndex == MAX_SELECTED_LUA_ID then
            self.m_tempRightDecoCell = cell
        end
        self:_RefreshDecoVisible(luaIndex, cell, false)
    end)
end






GeneralAbilityCtrl._UpdateTempAbilityState = HL.Method(HL.Boolean) << function(self, exist)
    if self.m_tempSelectCell then
        if self:_IsHaveTempAbility() then
            self.m_tempSelectCell.uiStateCtrl:SetState(SelectingNodeState.TempAbilityExist)
        else
            self.m_tempSelectCell.uiStateCtrl:SetState(SelectingNodeState.TempAbilityEmpty)
        end
    end

end

GeneralAbilityCtrl._TempAbilityAnim = HL.Method(HL.Boolean) << function(self, isShown)
    if self.m_tempSelectCell == nil then
        return
    end

    if self:_IsHaveTempAbility() then
        if isShown then
            local tempAbilityData = self.m_abilityDataList[MAX_SELECTED_LUA_ID]
            if tempAbilityData ~= nil and tempAbilityData.abilityRuntimeData.state ~= AbilityState.ForbiddenUse then
                self.m_tempSelectCell.rotateNodeAnimationWrapper:PlayWithTween("generalability_temp_in", function()
                    self.m_tempSelectCell.rotateNodeAnimationWrapper:PlayWithTween("generalability_temp_loop")
                end)
            end
        else
            self.m_tempSelectCell.rotateNodeAnimationWrapper:PlayWithTween("generalability_temp_out")

        end
    else
        self.m_tempSelectCell.rotateNodeAnimationWrapper:PlayWithTween("generalability_temp_out")
    end
end



GeneralAbilityCtrl._TempAbilityTailTick = HL.Method() << function(self)
    if LuaSystemManager.factory.inTopView then
        return
    end

    if not self.m_isSelectorShown then
        self.m_interactFacNodeId = nil
        return
    end

    self:_UpdateInteractTarget()
end

GeneralAbilityCtrl._UpdateInteractTarget = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, isPreview, forceUpdate)
    if FactoryUtils.isInBuildMode() then
        return
    end

    local succ, info = CSFactoryUtil.GetShouldInteractFacEntityUsingAsyncRst()
    local foundBuilding = succ and info.valid and info.nodeId > 0
    if foundBuilding then
        local nodeId = info.nodeId
        if self.m_interactFacNodeId ~= nodeId then
            if FactoryUtils.isOthersSocialBuilding(nodeId) then

                local canLike = FactoryUtils.canLikeSocialBuilding(nodeId)
                GameInstance.player.generalAbilitySystem:ActivateTempAbility(GeneralAbilityType.BuildingLike, canLike, nodeId)
                local abilityState = canLike and AbilityState.Idle or AbilityState.ForbiddenUse
                GameInstance.player.generalAbilitySystem:SwitchAbilityStateByType(GeneralAbilityType.BuildingLike, abilityState)
            else

                GameInstance.player.generalAbilitySystem:DeactivateTempAbility(GeneralAbilityType.BuildingLike)
            end
        end

        self.m_interactFacNodeId = nodeId
    else
        GameInstance.player.generalAbilitySystem:DeactivateTempAbility(GeneralAbilityType.BuildingLike)
        self.m_interactFacNodeId = nil
    end
end








GeneralAbilityCtrl._OnLeftButtonClicked = HL.Method() << function(self)
    if DeviceInfo.usingKeyboard then
        if self.m_pcHoverIndex ~= INVALID_ABILITY_TYPE then
            self:_OnSelectorClicked(self.m_pcHoverIndex)
        end
    end
end

GeneralAbilityCtrl._OnBackButtonClicked = HL.Method() << function(self)

    self:_RefreshWheelShownState(false)
end

GeneralAbilityCtrl._OnUseAbility = HL.Method() << function(self)
    self.m_triggerTable["OnGeneralAbilityUse"] = true
    if not self.m_clickEnabled then
        return
    end

    if self:_IsAllowedInPlayerController() then
        local useType = Utils.intToEnum(typeof(CS.Beyond.GEnums.GeneralAbilityType), self:_GetSelectedType())

        if GameInstance.player.generalAbilitySystem:IsTempAbility(useType) then
            local abilityRuntimeData = GameInstance.player.generalAbilitySystem:GetAbilityRuntimeDataByType(useType)

            if not abilityRuntimeData.isTempActive then
                return
            end
            if abilityRuntimeData.tempAbilityActiveState == TempAbilityActiveState.NotAvailableUse then
                return
            end
        end

        GameInstance.player.generalAbilitySystem:UseAbilityByType(useType)
    end
end

GeneralAbilityCtrl._OnRLongPressed = HL.Method() << function(self)
    GameInstance.mobileMotionManager:PostEventCommonShort()
    self:_RefreshWheelShownState(self:_IsAllowedInPlayerController())
end

GeneralAbilityCtrl._OnSelectorClicked = HL.Method(HL.Number) << function(self, luaIndex)
    if Utils.isCurSquadAllDead() then

        return
    end
    if not self.m_clickEnabled then
        return
    end

    local abilityData = self.m_abilityDataList[luaIndex]
    if abilityData == nil then
        return
    end
    self:_RefreshWheelShownState(false)
    self:_OnSelectByType(abilityData.type)
end

GeneralAbilityCtrl._OnSelectByType = HL.Method(HL.Number) << function(self, type)
    if self.m_screenOutCallbackVersion ~= self.m_screenOutVersion then
        self.m_screenOutCallbackVersion = self.m_screenOutVersion
        return
    end

    local data = self.m_abilityDataMap[type]
    if data ~= nil then
        if data.isForbidSelect then
            local isGet = false
            local textVal = nil
            local toastText = nil
            if data.abilityRuntimeData.forbidReasonTextId ~= nil and #data.abilityRuntimeData.forbidReasonTextId > 0 then
                toastText = data.abilityRuntimeData.forbidReasonTextId
            else
                toastText = BAN_MAP_TOAST
            end
            isGet, textVal = CS.Beyond.I18n.I18nUtils.TryGetText(toastText)
            if isGet and textVal ~= nil then
                Notify(MessageConst.SHOW_TOAST, textVal)
            else
                if isGet == false then
                    logger.error("无法获取 本地化 配置项 key "..toastText)
                end
            end
        else
            self:_SetSelectedType(type, true)
            if self.m_mobileUseAbilityFlag then
                EventLogManagerInst:GameEvent_General_competence_switch(tostring(type))
                self:_OnUseAbility()
            end
        end
    end
end







GeneralAbilityCtrl.GeneralAbilityOverrideIcon = HL.Method(HL.Any) << function(self, info)
    if self.m_iconOverrideTable == nil then
        return
    end

    local type, iconKey = unpack(info)
    if iconKey ~= nil and not string.isEmpty(iconKey) then
        local success, tableData = Tables.BattleBossOverrideIconTable:TryGetValue(iconKey)
        if success then
            self.m_iconOverrideTable[type:GetHashCode()] = tableData.iconId
        end
    else
        self.m_iconOverrideTable[type:GetHashCode()] = nil
    end
end


GeneralAbilityCtrl._RefreshDecoVisible = HL.Method(HL.Number, HL.Table, HL.Boolean) << function(self, luaIndex, cell, isShow)
    cell.bg.gameObject:SetActive(false)
    cell.selected.gameObject:SetActive(false)
    cell.temp.gameObject:SetActive(false)
    cell.tempActive.gameObject:SetActive(false)

    if luaIndex == MAX_SELECTED_LUA_ID then
        if self:_IsHaveTempAbility() then
            local abilityType = self.m_abilityDataList[MAX_SELECTED_LUA_ID].type
            local abilityRuntimeData = GameInstance.player.generalAbilitySystem:GetAbilityRuntimeDataByType(abilityType)
            local abilityStateIdle = abilityRuntimeData.state == AbilityState.Idle
            cell.bg.gameObject:SetActive(true)
            cell.temp.gameObject:SetActive(abilityStateIdle)
            if isShow then
                cell.selected.gameObject:SetActive(true)
                cell.tempActive.gameObject:SetActive(abilityStateIdle)
            end
        else
            cell.bg.gameObject:SetActive(true)
        end
    else
        if isShow then
            cell.bg.gameObject:SetActive(true)
            cell.selected.gameObject:SetActive(true)
        else
            cell.bg.gameObject:SetActive(true)
        end
    end
end


GeneralAbilityCtrl._RefreshCellHoverState = HL.Method(HL.Table, HL.Number, HL.Boolean) << function(
    self, cell, luaIndex, isHover)
    if not self.startPress then
        return
    end

    local abilityData = self.m_abilityDataList[luaIndex]
    if cell == nil or abilityData == nil then
        return
    end

    if isHover then
        cell.uiStateCtrl:SetState(SelectingNodeState.ShowSelectedNode)
    else
        cell.uiStateCtrl:SetState(SelectingNodeState.HideSelectedNode)
    end

    cell.animationWrapper:PlayWithTween(isHover and SELECTOR_HOVER_ANIMATION_NAME or SELECTOR_NORMAL_ANIMATION_NAME)
    if isHover or self.m_mobileHoverCancelFlag then
        self:_RefreshMidHoverInfo(abilityData.type)
        self.m_hoverSelectorType = abilityData.type
    else
        self:_RefreshMidHoverInfo(INVALID_ABILITY_TYPE)
        self.m_hoverSelectorType = INVALID_ABILITY_TYPE
    end
end

GeneralAbilityCtrl._RefreshMidHoverInfo = HL.Method(HL.Number) << function(self, type)
    if DeviceInfo.usingController and self.m_currentStickPushed == false then
        self.view.middleStateCtrl:SetState(SelectingMidState.InitialState)
        return
    end

    if DeviceInfo.usingKeyboard and self.m_pcSelectValid == false then
        self.view.middleStateCtrl:SetState(SelectingMidState.InitialState)
        return
    end

    if type == INVALID_ABILITY_TYPE then
        self.view.middleStateCtrl:SetState(SelectingMidState.InitialState)
    else
        local abilityData = self.m_abilityDataMap[type]
        if abilityData ~= nil then
            self.m_triggerTable["OnGeneralAbilityHover"] = abilityData.type
            self.view.hoverAbilityNameTxt.text = abilityData.name
            if self.m_mobileHoverCancelFlag then
                self.view.middleStateCtrl:SetState(SelectingMidState.CancelState)
            else
                if abilityData.isForbidSelect then
                    AudioAdapter.PostEvent("Au_UI_Hover_DisableGeneralAbility")
                    self.view.middleStateCtrl:SetState(SelectingMidState.DisableState)
                else
                    AudioAdapter.PostEvent("Au_UI_Hover_GeneralAbility")
                    self.view.middleStateCtrl:SetState(SelectingMidState.SelectState)
                end
            end
        end
    end
end

GeneralAbilityCtrl._RefreshWheelShownState = HL.Method(HL.Boolean) << function(self, isShown)
    if isShown and self.m_delayUpdateSelectorFlag then
        self.m_delayUpdateSelectorFlag = false
        for i = 1, self.m_abilityCells:GetCount() do
            self:_UpdateSelectorCellInfo(self.m_abilityCells:Get(i), i)
        end
    end
    if self:IsHide() then
        if self.startPress then
            self.startPress = false
            self.view.middleAnim.gameObject:SetActive(false)
            self.view.selectorAnim.gameObject:SetActive(false)
            self:_RefreshMidPanelShowInfo(false)
            self:_CheckRecoverScreen(false)
            self:_MobileOnSelectorShownStateChanged(false)
            if not DeviceInfo.usingTouch then
                self.view.selectedCanvasGroup.alpha = 1
            end
        end
        return
    end

    if self.m_isSelectorShown == isShown then
        return
    end

    UIUtils.PlayAnimationAndToggleActive(self.view.middleAnim, isShown)
    UIUtils.PlayAnimationAndToggleActive(self.view.selectorAnim, isShown, function()
        GameInstance.player.generalAbilitySystem.isInSelectMode = isShown
    end)
    self:_TempAbilityAnim(isShown)

    local abilityData = self.m_abilityDataMap[self:_GetSelectedType()]

    self:_RefreshMidPanelShowInfo(isShown)

    self.m_isSelectorShown = isShown
    if not DeviceInfo.usingTouch then
        self.view.selectedCanvasGroup.alpha = isShown and 0.1 or 1
        if isShown then
            self:ChangePanelCfg("realMouseMode", Types.EPanelMouseMode.NeedShow)
            if abilityData ~= nil and abilityData.cell ~= nil then
                InputManagerInst:MoveMouseTo(self.view.mainRect, self.uiCamera)
            end
        else
            self:ChangePanelCfg("realMouseMode", Types.EPanelMouseMode.NotNeedShow)
        end
    end

    InputManagerInst:ToggleBinding(self.m_selectorCancelBinding, isShown)
    InputManagerInst:ToggleBinding(self.m_selectorClickBinding, isShown)
    local isOpen, panel = UIManager:IsOpen(PanelId.MainHud)
    if isOpen then
        panel:CheckNormalAttackBtn(not isShown)
    end
    self:_CheckRecoverScreen(isShown)
    self:_MobileOnSelectorShownStateChanged(isShown)
end

GeneralAbilityCtrl._RefreshMidPanelShowInfo = HL.Method(HL.Boolean) << function(self, isShown)
    if not isShown then
        self.view.middleStateCtrl:SetState(SelectingMidState.InitialState)
        self.m_mobileHoverCancelFlag = false
        self.m_hoverSelectorType = INVALID_ABILITY_TYPE
    end

    for index = 1, self.view.config.CELL_MAX_COUNT do
        local cell = self.m_abilityCells:GetItem(index)
        if cell ~= nil then
            cell.uiStateCtrl:SetState(SelectingNodeState.HideSelectedNode)
            cell.uiStateCtrl:SetState(SelectingNodeState.MobileSelectingState)
        end
    end

    if isShown then
        self.m_interactFacNodeId = nil
        self.m_tempAbilityTailTickId = LuaUpdate:Add("TailTick", function()
            self:_TempAbilityTailTick()
        end)

        if DeviceInfo.usingController then
            self:_InitControllerTick()
        elseif DeviceInfo.usingKeyboard then
            self:_InitPCTick()
        end
    else
        self:_RemoveControllerTick()
        self:_RemovePCTick()
        self:_RemoveTempAbilityTailTick()
    end
end

GeneralAbilityCtrl._RemoveTempAbilityTailTick = HL.Method() << function(self)
    if self.m_tempAbilityTailTickId ~= -1 then
        self.m_tempAbilityTailTickId = LuaUpdate:Remove(self.m_tempAbilityTailTickId)
    end
end


GeneralAbilityCtrl._InitPCTick = HL.Method() << function(self)
    self.m_pcArrowAngle = CONTROLLER_INVALID_ANGLE
    self.m_lastPcArrowAngle = CONTROLLER_INVALID_ANGLE
    self.m_pcHoverIndex = INVALID_ABILITY_TYPE
    self.m_needLateRecoverScreen = false

    self.m_pcTick = LuaUpdate:Add("Tick", function(deltaTime)
        if self:IsShow() then
            self:_UpdatePCAngle()
            self:_UpdatePCSelectItemState()
        end
    end)
end


GeneralAbilityCtrl._RemovePCTick = HL.Method() << function(self)
    if self.m_pcTick ~= -1 then
        self.m_pcTick = LuaUpdate:Remove(self.m_pcTick)
    end
end


GeneralAbilityCtrl._UpdatePCAngle = HL.Method() << function(self)
    local mousePos = InputManager.mousePosition
    local uiPos, isInside = UIUtils.screenPointToUI(Vector2(mousePos.x,mousePos.y), self.uiCamera, self.view.mainRect)
    local disX = math.abs(uiPos.x)
    local disY = math.abs(uiPos.y)
    if disX * disX + disY * disY < PC_VALID_OP_VALUE then
        self.m_pcSelectValid = false
        return
    end

    local angle = lume.angle(uiPos.x, uiPos.y, 0, 0) / math.pi * 180
    angle = angle + PC_SELECTOR_OFFSET_ANGLE
    if angle < 0 then
        angle = angle + 360
    end

    self.m_pcSelectValid = true
    self.m_pcArrowAngle = angle
end

GeneralAbilityCtrl._UpdatePCSelectItemState = HL.Method() << function(self)
    if self.m_pcSelectValid == false then
        if self.m_pcHoverIndex ~= INVALID_ABILITY_TYPE then
            local lastCell = self.m_abilityCells:GetItem(self.m_pcHoverIndex)
            self:_RefreshCellHoverState(lastCell, self.m_pcHoverIndex, false)
            self.m_pcHoverIndex = INVALID_ABILITY_TYPE
        end
        return
    end

    local currentIndex = self.m_pcHoverIndex

    if self.m_lastPcArrowAngle ~= self.m_pcArrowAngle then
        self.m_lastPcArrowAngle = self.m_pcArrowAngle
        for index, range in ipairs(MOBILE_HOVER_ANGLE_RANGE) do

            if self.m_pcArrowAngle >= range[1] and self.m_pcArrowAngle < range[2] then
                currentIndex = index
                break
            end
        end
    end


    if currentIndex ~= self.m_pcHoverIndex then
        if self.m_pcHoverIndex ~= INVALID_ABILITY_TYPE then
            local lastCell = self.m_abilityCells:GetItem(self.m_pcHoverIndex)
            self:_RefreshCellHoverState(lastCell, self.m_pcHoverIndex, false)
        end
        local cell = self.m_abilityCells:GetItem(currentIndex)
        self:_RefreshCellHoverState(cell, currentIndex, true)
    end
    self.m_pcHoverIndex = currentIndex
end


GeneralAbilityCtrl._CheckRecoverScreen = HL.Method(HL.Boolean) << function(self, isShown)
    if isShown and not self.m_screenOutFlag then
        if not GameInstance.player.systemActionConflictManager:TryStartSystemAction(Const.PowerPoleFastTravelActionConflictName) then
            return
        end

        self.m_needLateRecoverScreen = false
        self.m_screenOutFlag = true
        self.m_screenOutVersion = self.m_screenOutVersion + 1
        UIManager:ClearScreenWithOutAnimation(function(clearScreenKey)
            self.m_screenOutCallbackVersion = self.m_screenOutCallbackVersion + 1
            self.m_clearScreenKey = clearScreenKey
            if self.m_needLateRecoverScreen then
                self:_RecoverScreen()
            end
            GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.PowerPoleFastTravelActionConflictName)
        end, { PANEL_ID, PanelId.Joystick })

        TimerManager:StartTimer(1,function()
            self.m_screenOutCallbackVersion = self.m_screenOutVersion
        end)
    end
    if not isShown then
        GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.PowerPoleFastTravelActionConflictName)
    end

    if not isShown and self.m_screenOutFlag then
        self.m_screenOutFlag = false
        if self.m_clearScreenKey > 0 then
            self:_RecoverScreen()
        else
            self.m_needLateRecoverScreen = true
        end
    end
end

GeneralAbilityCtrl._RecoverScreen = HL.Method() << function(self)
    UIManager:RecoverScreen(self.m_clearScreenKey)
    self.m_clearScreenKey = -1
end



GeneralAbilityCtrl._RefreshAllDecoInitState = HL.Method() << function(self)
    for index = 1, self.view.config.CELL_MAX_COUNT do
        local deco = self.m_decoCells:GetItem(index)
        if deco ~= nil then
            self:_RefreshDecoVisible(index, deco, false)
        end
    end
end

GeneralAbilityCtrl._RefreshMainVisible = HL.Method() << function(self)
    if not self.m_isValid then
        self.view.main.gameObject:SetActive(false)
        return
    end

    local isForbidden = GameInstance.player.forbidSystem:IsForbidden(ForbidType.HideGeneralAbility)
    if isForbidden then
        self.view.main.gameObject:SetActive(false)
        return
    end

    self.view.main.gameObject:SetActive(true)
end


GeneralAbilityCtrl._PlayAnimRightAbility = HL.Method(HL.String) << function(self, animName)
    if self.view.selectedAbilityAnim.curStateName == animName then
        return
    end

    if animName == RIGHT_TEMP_ABILITY_IN then
        self.view.selectedAbilityAnim:PlayWithTween(RIGHT_TEMP_ABILITY_IN, function()
            self.view.selectedAbilityAnim:PlayWithTween(RIGHT_TEMP_ABILITY_LOOP)
        end)
    else
        if self.view.selectedAbilityAnim.curStateName == RIGHT_TEMP_ABILITY_IN or self.view.selectedAbilityAnim.curStateName == RIGHT_TEMP_ABILITY_LOOP then
            self.view.selectedAbilityAnim:PlayWithTween(RIGHT_TEMP_ABILITY_DEFAULT,function()
                self.view.selectedAbilityAnim:PlayWithTween(animName)
            end)
        else
            self.view.selectedAbilityAnim:PlayWithTween(animName)
        end
    end
end

GeneralAbilityCtrl._OnActiveTempAnimLoop = HL.Method(HL.Boolean) << function(self, isActive)
    self.m_tempAnimLoopActive = isActive
    if isActive then
        local data = self.m_abilityDataMap[self.m_selectedAbilityType]
        if data ~= nil and data.isTempAbility and data.abilityRuntimeData.state ~= AbilityState.ForbiddenUse then
            self:_PlayAnimRightAbility(RIGHT_TEMP_ABILITY_LOOP)
        end
    else
        local stateName = self.view.selectedAbilityAnim.curStateName
        if stateName == RIGHT_TEMP_ABILITY_LOOP or stateName == RIGHT_TEMP_ABILITY_IN then
            self:_PlayAnimRightAbility(RIGHT_TEMP_ABILITY_DEFAULT)
        end
    end
end

GeneralAbilityCtrl._RefreshGasAndWaterTipShownState = HL.Method() << function(self)
    if not GameInstance.player.generalAbilitySystem:CheckUnlock(GeneralAbilityType.FluidInteract) or
        not GameInstance.player.generalAbilitySystem:CheckCanSelect(GeneralAbilityType.FluidInteract)  then
        for index = 1, self.view.config.CELL_MAX_COUNT do
            local cell = self.m_abilityCells:GetItem(index)
            if cell ~= nil then
                cell.waterFillingNode.gameObject:SetActive(false)
            end
        end
        self.view.switchTipsIcon.gameObject:SetActiveIfNecessary(false)
        return
    end

    for index = 1, self.view.config.CELL_MAX_COUNT do
        local cell = self.m_abilityCells:GetItem(index)
        if cell ~= nil then
            cell.waterFillingNode.gameObject:SetActive(false)
        end

        local abilityData = self.m_abilityDataList[index]
        if abilityData ~= nil and self.m_tipsAbilityType == GeneralAbilityType.FluidInteract:GetHashCode()
            and abilityData.type == GeneralAbilityType.FluidInteract:GetHashCode() and abilityData.isForbidSelect ~= true then
            if self.m_isInGasPool then
                cell.waterFillingIcon:LoadSprite(UIConst.UI_SPRITE_GENERAL_ABILITY, self.view.config.GAS_FILLING_ICON)
            else
                cell.waterFillingIcon:LoadSprite(UIConst.UI_SPRITE_GENERAL_ABILITY, self.view.config.WATER_FILLING_ICON)
            end
            cell.waterFillingNode.gameObject:SetActive(true)
        end
    end

    local isShown = false
    if self.m_tipsAbilityType == GeneralAbilityType.FluidInteract:GetHashCode() and self.m_tipsAbilityType ~= self.m_selectedAbilityType then
        isShown = true
        local data = self.m_abilityDataMap[self.m_tipsAbilityType]
        if data ~= nil and data.isForbidSelect then
            isShown = false
        end
    end

    if GeneralAbilityType.FluidInteract:GetHashCode() == self.m_selectedAbilityType then
        if self.m_isInPool or self.m_isInGasPool then
            self.view.selectedAbility.view.lockedNode.gameObject:SetActiveIfNecessary(false)
        else
            self.view.selectedAbility.view.lockedNode.gameObject:SetActiveIfNecessary(true)
        end
    end

    if isShown then
        if self.m_isInGasPool then
            self.view.switchTipsIcon:LoadSprite(UIConst.UI_SPRITE_GENERAL_ABILITY, self.view.config.GAS_FILLING_ICON)
            self.view.switchTipsIconGlow:LoadSprite(UIConst.UI_SPRITE_GENERAL_ABILITY, self.view.config.GAS_FILLING_ICON)
        else
            self.view.switchTipsIcon:LoadSprite(UIConst.UI_SPRITE_GENERAL_ABILITY, self.view.config.WATER_FILLING_ICON)
            self.view.switchTipsIconGlow:LoadSprite(UIConst.UI_SPRITE_GENERAL_ABILITY, self.view.config.WATER_FILLING_ICON)
        end
    end
    self.view.switchTipsIcon.gameObject:SetActiveIfNecessary(isShown)
    self.m_isSwitchTipShown = isShown
end






GeneralAbilityCtrl._ResetSelectedType = HL.Method() << function(self)
    local initialType

    for index = 1, MAX_SELECTED_LUA_ID do
        local data = self.m_abilityDataList[index]
        if data ~= nil and data.isForbidSelect == false then
            if initialType == nil then
                initialType = data.type
            end

            if data.type == GeneralAbilityType.Scan:GetHashCode() then
                initialType = data.type
                break
            end
        end
    end

    if initialType ~= nil then
        self:_SetSelectedType(initialType, false)
    end
end

GeneralAbilityCtrl._GetSelectedType = HL.Method().Return(HL.Number) << function(self)
    local _, value = ClientDataManagerInst:GetInt(SELECTED_ABILITY_TYPE_CLIENT_LOCAL_DATA_KEY, false,
                                                     INVALID_ABILITY_TYPE)
    return self.m_selectedAbilityType == INVALID_ABILITY_TYPE and value or self.m_selectedAbilityType
end

GeneralAbilityCtrl._SetSelectedType = HL.Method(HL.Number, HL.Boolean) << function(self, type, needSave)
    if GameInstance.player.generalAbilitySystem:IsTempAbility(self.m_lastSelectedAbilityType) then
        self:_PlayAnimRightAbility(RIGHT_TEMP_ABILITY_DEFAULT)
    end

    if self.m_lastSelectedAbilityType ~= type then
        self.m_lastSelectedAbilityType = type
    end

    self.view.selectedAbility:SetOverrideIcon(self.m_iconOverrideTable[type])
    self.view.selectedAbility:InitGeneralAbilityCell(type, false, false, function(isValid, fromState)
        local abilityRuntimeData = GameInstance.player.generalAbilitySystem:GetAbilityRuntimeDataByType(type)
        if abilityRuntimeData ~= nil then
            local isTempAbility = GameInstance.player.generalAbilitySystem:IsTempAbility(abilityRuntimeData.type)
            if isTempAbility then
                if isValid then
                    self:_PlayAnimRightAbility(RIGHT_TEMP_ABILITY_IN)
                else
                    self:_PlayAnimRightAbility(RIGHT_TEMP_ABILITY_DEFAULT)
                end
            else
                if isValid and fromState ~= AbilityState.None then
                    self:_PlayAnimRightAbility(RIGHT_ABILITY_VALID_ANIMATION_NAME)
                end
            end
        end
    end)
    self.view.selectedAbility:SetCustomCDFillImage(self.view.selectedAbility.view.fillImage)
    local lastType = self.m_selectedAbilityType
    self.m_selectedAbilityType = type
    GameInstance.player.generalAbilitySystem.selectGeneralAbility = type

    local lastData = self.m_abilityDataMap[lastType]
    if lastData ~= nil then
        local lastDeco = self.m_decoCells:GetItem(lastData.index)
        if lastDeco ~= nil then
            self:_RefreshDecoVisible(lastData.index, lastDeco, false)
        end
    elseif GameInstance.player.generalAbilitySystem:IsTempAbility(lastType) then

        self:_RefreshDecoVisible(MAX_SELECTED_LUA_ID, self.m_tempRightDecoCell, false)
    end

    local currData = self.m_abilityDataMap[type]
    if currData ~= nil then
        local currDeco = self.m_decoCells:GetItem(currData.index)
        if currDeco ~= nil then
            self:_RefreshDecoVisible(currData.index, currDeco, true)
        end
    end

    self:_RefreshGasAndWaterTipShownState()

    if needSave and not GameInstance.player.generalAbilitySystem:IsTempAbility(type) then
        ClientDataManagerInst:SetInt(SELECTED_ABILITY_TYPE_CLIENT_LOCAL_DATA_KEY, type, false)
    end
end

GeneralAbilityCtrl._StartPress = HL.Method() << function(self)
    self:_ClearRPress()
    GameInstance.player.generalAbilitySystem.isRPressed = true
    self.m_pressRTime = 0

    local isOpen, guideCtrl = UIManager:IsOpen(PanelId.Guide)
    if isOpen then
        self.m_pressStartPos = nil
    else
        self.m_pressStartPos = self.view.selectedAbilityButton.curPressPos
    end

    if not self:_IsAllowedInPlayerController() then
        return
    end

    local screenPosition = self.uiCamera:WorldToScreenPoint(self.view.selectedAbilityNode.position)
    self.m_mobileBtnScreenPosition = Vector2(screenPosition.x, screenPosition.y)

    if self.m_longClickEnabled then
        self.m_selectedAbilityPressTick = LuaUpdate:Add("Tick", function(deltaTime)
            if self.m_unLockNormalNum == 0 then
                return
            end

            local pressPos = self.view.selectedAbilityButton.curPressPos
            if self.m_pressStartPos ~= nil then
                local dragThreshold = self.view.selectedAbilityNodeDragHandler.dragThreshold
                if (pressPos - self.m_pressStartPos).sqrMagnitude > dragThreshold * dragThreshold then
                    self:_ClearRPress()
                    return
                end
            end

            self.m_pressRTime = self.m_pressRTime + deltaTime
            if self.m_pressRTime >= self.m_openStartTime then
                local progress = self.view.pressProgress

                if not progress.gameObject.activeSelf then
                    progress.gameObject:SetActive(true)
                end
                progress.fillAmount = (self.m_pressRTime - self.m_openStartTime) /
                    (self.m_openSelectorTime - self.m_openStartTime)

                if self.m_pressRTime >= self.m_openSelectorTime then
                    self:_OnRLongPressed()
                    self:_ClearRPress()
                end
            end
        end)
    end


    self:_PlayAnimRightAbility(PRESS_ANIMATION_NAME)
end

GeneralAbilityCtrl._StopPress = HL.Method() << function(self)
    if not self.m_isSelectorShown and not DeviceInfo.usingController then
        local pressPos = self.view.selectedAbilityButton.curPressPos
        if self.m_pressStartPos ~= nil then
            local dragThreshold = self.view.selectedAbilityNodeDragHandler.dragThreshold
            if (pressPos - self.m_pressStartPos).sqrMagnitude > dragThreshold * dragThreshold then
                self:_ClearRPress()
                local lastData = self.m_abilityDataMap[self.m_selectedAbilityType]
                if lastData ~= nil and lastData.isTempAbility and lastData.abilityRuntimeData.state ~= AbilityState.ForbiddenUse and self.m_tempAnimLoopActive then
                    self:_PlayAnimRightAbility(RIGHT_TEMP_ABILITY_LOOP)
                else
                    self:_PlayAnimRightAbility(RELEASE_ANIMATION_NAME)
                end
                return
            end
        end
    end

    if self.m_pressRTime < self.m_openStartTime then
        if self.m_mobileOpenSelector then
            self.m_mobileOpenSelector = false
        else
            self:_OnUseAbility()
        end
    end
    self:_ClearRPress()

    if self.m_canReleaseCloseSelector and self.m_isSelectorShown then
        local lastType = self.m_hoverSelectorType
        self:_RefreshWheelShownState(false)
        if DeviceInfo.usingTouch then
            if self.m_mobileUseAbilityFlag then
                if lastType ~= INVALID_ABILITY_TYPE then
                    self:_OnSelectByType(lastType)
                end
            else
                AudioAdapter.PostEvent("Au_UI_Button_MoGeneralAbilityCancel")
            end
        elseif DeviceInfo.usingKeyboard then
            if lastType ~= INVALID_ABILITY_TYPE then
                self:_OnSelectByType(lastType)
            end
        end
    end

    local lastData = self.m_abilityDataMap[self.m_selectedAbilityType]
    if lastData ~= nil and lastData.isTempAbility and lastData.abilityRuntimeData.state ~= AbilityState.ForbiddenUse and self.m_tempAnimLoopActive then
        self:_PlayAnimRightAbility(RIGHT_TEMP_ABILITY_LOOP)
    else
        self:_PlayAnimRightAbility(RELEASE_ANIMATION_NAME)
    end
end

GeneralAbilityCtrl._ClearRPress = HL.Method() << function(self)
    GameInstance.player.generalAbilitySystem.isRPressed = false
    if self.m_selectedAbilityPressTick ~= -1 then
        self.m_selectedAbilityPressTick = LuaUpdate:Remove(self.m_selectedAbilityPressTick)
    end
    self.view.pressProgress.gameObject:SetActive(false)
end

GeneralAbilityCtrl._OnEnterLiquidPoolNearbyArea = HL.Method() << function(self)
    self.m_tipsAbilityType = GeneralAbilityType.FluidInteract:GetHashCode()
    self.m_isInPool = true
    self:_UpdateGasAndFluidInteractState()
    self:_RefreshGasAndWaterTipShownState()
end

GeneralAbilityCtrl._OnLeaveLiquidPoolNearbyArea = HL.Method() << function(self)
    if self.m_isInGasPool == false then
        self.m_tipsAbilityType = INVALID_ABILITY_TYPE
    end
    self.m_isInPool = false
    self:_UpdateGasAndFluidInteractState()
    self:_RefreshGasAndWaterTipShownState()
end

GeneralAbilityCtrl._OnEnterGasPoolNearbyArea = HL.Method() << function(self)
    self.m_isInGasPool = true;
    self.m_tipsAbilityType = GeneralAbilityType.FluidInteract:GetHashCode()

    self:_UpdateGasAndFluidInteractState()
    self:_RefreshGasAndWaterTipShownState()
end

GeneralAbilityCtrl._OnLeaveGasPoolNearbyArea = HL.Method() << function(self)
    self.m_isInGasPool = false
    if self.m_isInPool == false then
        self.m_tipsAbilityType = INVALID_ABILITY_TYPE
    end

    self:_UpdateGasAndFluidInteractState()
    self:_RefreshGasAndWaterTipShownState()
end

GeneralAbilityCtrl._OnWaterInteract = HL.Method(HL.Table) << function(self, args)

    if self.m_isInGasPool then
        return
    end

    local waterNodeId = unpack(args)
    Notify(MessageConst.SHOW_LIQUID_POOL, {waterNodeId, 0, false})
end

GeneralAbilityCtrl._TryInteractGas = HL.Method(HL.Table) << function(self, args)
    local shortId, itemId = unpack(args)

    Notify(MessageConst.SHOW_GAS_COLLECTION_PANEL, {
        gasItemId = itemId,
        gasCoreId = shortId
    })
end

GeneralAbilityCtrl._UpdateGasAndFluidInteractState = HL.Method() << function(self)
    local fluidType = GeneralAbilityType.FluidInteract:GetHashCode()

    local abilityRuntimeData = GameInstance.player.generalAbilitySystem:GetAbilityRuntimeDataByType(fluidType)
    if abilityRuntimeData ~= nil and abilityRuntimeData.state == AbilityState.Locked then
        return
    end

    local data = self.m_abilityDataMap[fluidType]
    if data ~= nil and  data.isForbidSelect then
        return
    end

    if self.m_isInGasPool or self.m_isInPool then
        GameInstance.player.generalAbilitySystem:SwitchAbilityStateByType(fluidType, AbilityState.Idle)
        abilityRuntimeData.forbidUseToastId = ""
    else
        GameInstance.player.generalAbilitySystem:SwitchAbilityStateByType(fluidType, AbilityState.ForbiddenUse)

        if GameInstance.player.guide:IsGuideCompleted(Tables.factoryConst.unlockGasCollectionGuideId) then
            abilityRuntimeData.forbidUseToastId = "LUA_GENERAL_ABILITY_FLUID_OR_GAS_INTERACT_FORBIDDEN"
        else
            abilityRuntimeData.forbidUseToastId = "LUA_GENERAL_ABILITY_FLUID_INTERACT_FORBIDDEN"
        end
    end
end

GeneralAbilityCtrl._UseAbilityItem = HL.Method(HL.String) << function(self, itemId)
    GameInstance.player.inventory:UseItem(Utils.getCurrentScope(), itemId)
end






GeneralAbilityCtrl._InitMobileNodes = HL.Method() << function(self)
    self.view.mobileMovePanel.gameObject:SetActive(false)
    self.view.mobileCancelPanel.gameObject:SetActive(false)
end

GeneralAbilityCtrl._MobileOnSelectorShownStateChanged = HL.Method(HL.Boolean) << function(self, isShown)
    if not DeviceInfo.usingTouch then
        return
    end

    self.view.mobileCancelPanel.animationWrapper:PlayWithTween(MOBILE_CANCEL_PANEL_INVALID_ANIMATION_NAME)

    self.view.mobileMovePanel.gameObject:SetActive(isShown)
    self.view.mobileCancelPanel.gameObject:SetActive(isShown)

    if isShown and self.m_mobileSelectorTick < 0 then
        self:_MobileClearSelectorState()
        self:_MobileTick()
        self.m_mobileSelectorTick = LuaUpdate:Add("Tick", function(deltaTime)
            self:_MobileTick()
        end)
    end

    if not isShown and self.m_mobileSelectorTick > 0 then
        self.m_mobileSelectorTick = LuaUpdate:Remove(self.m_mobileSelectorTick)
    end
end


GeneralAbilityCtrl._MobileClearSelectorState = HL.Method() << function(self)
    local screenPosition = self.uiCamera:WorldToScreenPoint(self.view.selectedAbilityNode.position)
    self.m_mobileBtnScreenPosition = Vector2(screenPosition.x, screenPosition.y)
    self.m_mobileHoverAbilityIndex = INVALID_ABILITY_TYPE

    local moveHalfRectWidth = self.view.mobileMovePanel.moveRect.rect.width / 2
    self.m_mobileMoveCircleSqr = moveHalfRectWidth * moveHalfRectWidth

    local cancelHalfRectWidth = self.view.mobileCancelPanel.rectTransform.rect.width / 2
    self.m_mobileCancelCircleSqr = cancelHalfRectWidth * cancelHalfRectWidth

    self.m_mobileIsInCancelCircle = false
    self.m_mobileUseAbilityFlag = true
    self.m_needLateRecoverScreen = false
    for index = 1, self.view.config.CELL_MAX_COUNT do
        local cell = self.m_abilityCells:GetItem(index)
        self:_RefreshCellHoverState(cell, index, false)
    end
end

GeneralAbilityCtrl._MobileTick = HL.Method() << function(self)

    local pressPosition = self.view.selectedAbilityButton.curPressPos
    local centerPosition = self.m_mobileBtnScreenPosition
    local angle = lume.angle(centerPosition.x, centerPosition.y, pressPosition.x, pressPosition.y) / math.pi * 180
    angle = angle - MOBILE_SELECTOR_OFFSET_ANGLE

    if angle < 0 then
        angle = angle + 360
    end

    local currentIndex = self.m_mobileHoverAbilityIndex
    for index, range in ipairs(MOBILE_HOVER_ANGLE_RANGE) do

        if angle >= range[1] and angle < range[2] then
            currentIndex = index
            break
        end
    end


    local cancelRect = self.view.mobileCancelPanel.rectTransform
    local targetPositionInCancelRect = UIUtils.screenPointToUI(pressPosition, self.uiCamera, cancelRect)
    local isInCancelCircle = targetPositionInCancelRect.sqrMagnitude <= self.m_mobileCancelCircleSqr
    if isInCancelCircle ~= self.m_mobileIsInCancelCircle then
        local hoverCell = self.m_abilityCells:GetItem(self.m_mobileHoverAbilityIndex)
        self.m_mobileHoverCancelFlag = isInCancelCircle
        self.m_mobileUseAbilityFlag = not isInCancelCircle
        self:_RefreshCellHoverState(hoverCell, self.m_mobileHoverAbilityIndex, not isInCancelCircle)
        local cancelAnimName = isInCancelCircle and MOBILE_CANCEL_PANEL_VALID_ANIMATION_NAME or MOBILE_CANCEL_PANEL_INVALID_ANIMATION_NAME
        self.view.mobileCancelPanel.animationWrapper:PlayWithTween(cancelAnimName)
        local selectorAnimName = isInCancelCircle and MOBILE_CANCEL_SELECTOR_VALID_ANIMATION_NAME or MOBILE_CANCEL_SELECTOR_INVALID_ANIMATION_NAME
        if isInCancelCircle then
            self.view.selectorListAnim:PlayWithTween(selectorAnimName)
            AudioAdapter.PostEvent("Au_UI_Hover_MoGeneralAbilityCancel")
        end
        for index = 1, self.view.config.CELL_MAX_COUNT do
            local cell = self.m_abilityCells:GetItem(index)
            if cell ~= nil then
                if isInCancelCircle then
                    cell.uiStateCtrl:SetState(SelectingNodeState.MobileCancelState)
                else
                    cell.uiStateCtrl:SetState(SelectingNodeState.MobileSelectingState)
                end
            end
        end
    end


    if currentIndex ~= self.m_mobileHoverAbilityIndex and not isInCancelCircle then
        if self.m_mobileHoverAbilityIndex > 0 then
            local lastCell = self.m_abilityCells:GetItem(self.m_mobileHoverAbilityIndex)
            self:_RefreshCellHoverState(lastCell, self.m_mobileHoverAbilityIndex, false)
        end
        local cell = self.m_abilityCells:GetItem(currentIndex)
        self:_RefreshCellHoverState(cell, currentIndex, true)
    end


    local moveRect = self.view.mobileMovePanel.moveRect
    local inMoveRect = UIUtils.screenPointToUI(pressPosition, self.uiCamera, moveRect)
    if inMoveRect.sqrMagnitude <= self.m_mobileMoveCircleSqr then
        local clampPositionX = inMoveRect.x
        local clampPositionY = inMoveRect.y
        self.view.mobileMovePanel.moveBtnRectTransform.anchoredPosition = Vector2(clampPositionX, clampPositionY)
    else
        local t = math.sqrt(self.m_mobileMoveCircleSqr / inMoveRect.sqrMagnitude)
        local clampPositionX = t * inMoveRect.x
        local clampPositionY = t * inMoveRect.y
        self.view.mobileMovePanel.moveBtnRectTransform.anchoredPosition = Vector2(clampPositionX, clampPositionY)
    end

    self.m_mobileIsInCancelCircle = isInCancelCircle
    self.m_mobileHoverAbilityIndex = currentIndex
end







GeneralAbilityCtrl._InitControllerTick = HL.Method() << function(self)
    self.m_currentArrowAngle = CONTROLLER_INVALID_ANGLE
    self.m_controllerHoverIndex = INVALID_ABILITY_TYPE
    self.m_needLateRecoverScreen = false

    self.m_controllerTick = LuaUpdate:Add("Tick", function(deltaTime)
        if self:IsShow() then
            self:_UpdateAngle()
            self:_UpdateSelectItemState()
            self:_UpdateQuickMenuState()
        end
    end)

end


GeneralAbilityCtrl._RemoveControllerTick = HL.Method() << function(self)
    if self.m_controllerTick ~= -1 then
        self.m_controllerTick = LuaUpdate:Remove(self.m_controllerTick)
    end
end

GeneralAbilityCtrl._UpdateAngle = HL.Method() << function(self)
    local stickValue = InputManagerInst:GetGamepadStickValue(false)
    if stickValue.magnitude < RIGHT_STICK_DEAD_ZONE_VALUE then
        self.m_currentStickPushed = false
        return
    end

    local angle = lume.angle(stickValue.x, stickValue.y, 0, 0) / math.pi * 180
    angle = angle + CONTROLLER_SELECTOR_OFFSET_ANGLE
    if angle < 0 then
        angle = angle + 360
    end

    self.m_currentArrowAngle = angle
    self.m_currentStickPushed = true
end

GeneralAbilityCtrl._UpdateSelectItemState = HL.Method() << function(self)
    if not self.m_currentStickPushed then
        return
    end

    local currentIndex = self.m_controllerHoverIndex
    for index, range in ipairs(MOBILE_HOVER_ANGLE_RANGE) do

        if self.m_currentArrowAngle >= range[1] and self.m_currentArrowAngle < range[2] then
            currentIndex = index
            break
        end
    end


    if currentIndex ~= self.m_controllerHoverIndex then
        if self.m_controllerHoverIndex > 0 then
            local lastCell = self.m_abilityCells:GetItem(self.m_controllerHoverIndex)
            self:_RefreshCellHoverState(lastCell, self.m_controllerHoverIndex, false)
        end
        local cell = self.m_abilityCells:GetItem(currentIndex)
        self:_RefreshCellHoverState(cell, currentIndex, true)
    end
    self.m_controllerHoverIndex = currentIndex

end

GeneralAbilityCtrl._UpdateQuickMenuState = HL.Method() << function(self)
    local rightStickValue = InputManagerInst:GetGamepadStickValue(false)
    local useRightStick = rightStickValue.x ~= 0 or rightStickValue.y ~= 0

    if not useRightStick then
        if self.m_controllerHoverIndex ~= INVALID_ABILITY_TYPE then
            self:_OnSelectorClicked(self.m_controllerHoverIndex)
        end
    end
end

GeneralAbilityCtrl.OnActiveBuildingLike = HL.Method(HL.Any) << function(self, args)
    local nodeId = unpack(args)
    FactoryUtils.likeSocialBuilding(nodeId, function()
        if self.m_interactFacNodeId ~= nodeId then
            return
        end
        FactoryUtils.updateBuildingLikeAbilityState(nodeId)
    end)
end







if BEYOND_DEBUG then
    local DEBUG_TEMP_ABILITY_CLIENT_LOCAL_DATA_KEY = "debug_temp_ability"

    local DEBUG_TEMP_ABILITY_ON_USE_CALLBACK_MAP = {
        [GeneralAbilityType.Snapshot:GetHashCode()] = function()
            PhaseManager:OpenPhase(PhaseId.Snapshot)
        end,
        [GeneralAbilityType.WaterGun:GetHashCode()] = function()
            CS.Beyond.Scripts.GmCommands.GmCommands.SpawnWaterGun()
        end,
        [GeneralAbilityType.BattleBoss:GetHashCode()] = function()
            GameInstance.playerController:ClickGeneralAbilityBattleBoss()
        end,
        [GeneralAbilityType.BuildingLike:GetHashCode()] = function()

        end,
    }

    GeneralAbilityCtrl._InitDebugTempAbility = HL.Method() << function(self)
        local _, value = ClientDataManagerInst:GetInt(DEBUG_TEMP_ABILITY_CLIENT_LOCAL_DATA_KEY, false,
            INVALID_ABILITY_TYPE)
        if value ~= nil and value ~= INVALID_ABILITY_TYPE then
            self:_SwitchToDebugTempAbility(value)
        end
    end

    GeneralAbilityCtrl._SwitchToDebugTempAbility = HL.Method(HL.Number) << function(self, abilityType)
        local callback = DEBUG_TEMP_ABILITY_ON_USE_CALLBACK_MAP[abilityType]
        GameInstance.player.generalAbilitySystem:ActivateTempAbilityLegacy(abilityType, callback)
        self:_SetSelectedType(abilityType, false)
    end

    GeneralAbilityCtrl.SwitchDebugTempAbility = HL.Method(HL.Table) << function(self, args)
        self:ClearDebugTempAbility()
        local abilityType = unpack(args)
        ClientDataManagerInst:SetInt(DEBUG_TEMP_ABILITY_CLIENT_LOCAL_DATA_KEY, abilityType:GetHashCode(), false)
        self:_SwitchToDebugTempAbility(abilityType:GetHashCode())
    end

    GeneralAbilityCtrl.ClearDebugTempAbility = HL.Method() << function(self)
        local _, value = ClientDataManagerInst:GetInt(DEBUG_TEMP_ABILITY_CLIENT_LOCAL_DATA_KEY, false,
            INVALID_ABILITY_TYPE)
        if value ~= nil and value ~= INVALID_ABILITY_TYPE then
            ClientDataManagerInst:DeleteKey(DEBUG_TEMP_ABILITY_CLIENT_LOCAL_DATA_KEY)
            GameInstance.player.generalAbilitySystem:DeactivateTempAbility(value)
        end
    end
end






GeneralAbilityCtrl.GeneralAbilityChangeKeyBinding = HL.Method(HL.Table) << function(self, args)
    local useAlterKeyBinding, sourceKey = unpack(args)
    if useAlterKeyBinding then
        self.m_changeKeyBinding[sourceKey] = useAlterKeyBinding;
    else
        self.m_changeKeyBinding[sourceKey] = nil;
    end
    local count = 0
    for _ in pairs(self.m_changeKeyBinding) do
        count = count + 1
    end
    local finalUseAlterKeyBinding = count > 0;
    local pressActionId, releaseActionId
    if finalUseAlterKeyBinding then
        pressActionId = "general_ability_press_alter"
        releaseActionId = "general_ability_release_alter"
    else
        pressActionId = "general_ability_press"
        releaseActionId = "general_ability_release"
    end
    self.view.selectedAbilityButton.onPressStart:ChangeBindingPlayerAction(pressActionId)
    self.view.selectedAbilityButton.onPressEnd:ChangeBindingPlayerAction(releaseActionId)
    self.startPress = false
    self:_ClearRPress()
    self:_RefreshWheelShownState(false)
end

GeneralAbilityCtrl.OnToggleUiAction = HL.Method(HL.Table) << function(self, args)
    self.startPress = false
    self:_ClearRPress()
    self:_RefreshWheelShownState(false)
end



HL.Commit(GeneralAbilityCtrl)
