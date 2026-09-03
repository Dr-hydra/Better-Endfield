local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local uQuaternion = CS.Unity.Mathematics.quaternion
local uMath = CS.Unity.Mathematics.math
local GeneralAbilityType = GEnums.GeneralAbilityType
local AbilityState = CS.Beyond.Gameplay.GeneralAbilitySystem.AbilityState
local PANEL_ID = PanelId.FacBuildingInteract
FacBuildingInteractCtrl = HL.Class('FacBuildingInteractCtrl', uiCtrl.UICtrl)

local logisticInteractSampleOffsets = {
    {1, 0}, {0, 0}, {1, 1}, {1, -1},
    {2, 0}, {2, 1}, {2, -1},
    {3, 0}, {3, 1}, {3, -1},
}



local INVALID_INTERACT_BUILDING_LIST = {
    ["log_pipe_repeater_1"] = true,
}

local INTERACT_SOURCE_ID_BELT = "LogisticBelt"
local INTERACT_SOURCE_ID_DELETE_ALL_BELT = "DeleteAllBelt"
local INTERACT_SOURCE_ID_PIPE = "LogisticPipe"
local INTERACT_SOURCE_ID_DELETE_ALL_PIPE = "DeleteAllLogisticPipe"

local INTERACT_ICON_COMMON = "btn_common_exchange_icon"
local INTERACT_ICON_DELETE = "btn_del_building_icon"
local INTERACT_ICON_DELETE_SOCIAL = "btn_del_social_building_icon"
local INTERACT_ICON_DELETE_ALL = "btn_del_all_building_icon"





FacBuildingInteractCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_BUILD_MODE_CHANGE] = 'OnBuildModeChange',
    [MessageConst.ON_FAC_DESTROY_MODE_CHANGE] = 'OnFacDestroyModeChange',
    [MessageConst.BEFORE_EXIT_DESTROY_MODE] = 'BeforeExitDestroyMode',

    [MessageConst.FAC_UPDATE_INTERACT_OPTION] = 'UpdateInteractOption',
    [MessageConst.ON_TOGGLE_FAC_TOP_VIEW] = 'OnToggleFacTopView',
    [MessageConst.FAC_ON_NODE_REMOVED] = 'OnBuildingRemoved',
    [MessageConst.FAC_ON_PENDING_SLOTS_REMOVED] = 'OnPendingSlotsRemoved',
    [MessageConst.ON_FAC_TOP_VIEW_CAM_TARGET_MOVED] = 'OnFacTopViewCamTargetMoved',
    [MessageConst.ON_FAC_TOP_VIEW_CAM_ZOOM] = 'OnFacTopViewCamZoom',

    [MessageConst.FAC_BLOCK_OTHER_HUB_UNLOADER_INTERACT] = 'FacBlockOtherHubUnloaderInteract',

    [MessageConst.FAC_STOP_DRAG_IN_BATCH_MODE] = 'FacStopDragInBatchMode',
    [MessageConst.ACTIVE_BUILDING_LIKE] = 'OnActiveBuildingLike',
    [MessageConst.FAC_ON_UDPIPE_INTERACT_STATE_CHANGED] = '_OnUdpipeInteractStateChanged',
}

FacBuildingInteractCtrl.m_buildingInteractHighlightEffect = HL.Field(HL.Table)

FacBuildingInteractCtrl.m_subBuildingInteractHighlightEffect = HL.Field(HL.Table)

FacBuildingInteractCtrl.m_logisticInteractHighlightEffect = HL.Field(HL.Table)

FacBuildingInteractCtrl.m_pipeInteractHighlightEffect = HL.Field(HL.Table)

FacBuildingInteractCtrl.m_hoverInteractHighlightEffect = HL.Field(HL.Table)



FacBuildingInteractCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local isUsingCache = self.m_buildingInteractHighlightEffect ~= nil
    self.m_onClickScreen = function(eventData)
        self:_OnClickScreen(eventData)
    end
    self.m_onRightClickScreen = function(eventData)
        self:_OnRightClickScreen(eventData)
    end
    self.m_onLongPressScreen = function(eventData)
        self:_OnLongPressScreen(eventData)
    end
    self.m_onDragScreen = function(eventData)
        self:_OnDragScreen(eventData)
    end
    self.m_onDragScreenBegin = function(pos)
        self:_OnDragScreenBegin(pos)
    end
    self.m_onDragScreenEnd = function(pos)
        self:_OnDragScreenEnd(pos)
    end
    self.m_onPressScreen = function(eventData)
        self:_OnPressScreen(eventData)
    end
    self.m_onReleaseScreen = function(eventData)
        self:_OnReleaseScreen(eventData)
    end

    self.view.batchToggle.onValueChanged:AddListener(function(isOn)
        self:_OnChangeDragBatchToggle(isOn)
    end)

    if not isUsingCache then
        self:_InitInteractHighlightEffects()
    end

    LuaSystemManager.factory.interactPanelCtrl = self
    self.m_curHighlightedSlotIds = {}

    self:_InitBatchController()
    self:_InitFakeInteractOption()

    self.view.batchNode.gameObject:SetActive(false)
    self.view.longPressHint.gameObject:SetActive(false)


end

FacBuildingInteractCtrl.OnShow = HL.Override() << function(self)
    self:_AddRegister()
    if not LuaSystemManager.factory.inTopView then
        self:_UpdateInteractTarget(false, true)
    end

    Notify(MessageConst.CHANGE_MOUSE_ICON_HINT, {
        name = "FacBuildingInteract-BatchMode",
        type = LuaSystemManager.factory.inBatchSelectMode and UIConst.MOUSE_ICON_HINT.Frame or UIConst.MOUSE_ICON_HINT.Default,
    })
end
FacBuildingInteractCtrl.OnHide = HL.Override() << function(self)
    self:_ClearRegister()
    CSFactoryUtil.DispatchFactoryBuildingApproachSelectedChanged(self.m_interactFacNodeId, nil)
    self:_SetHighlightedPipeNode()

    Notify(MessageConst.CHANGE_MOUSE_ICON_HINT, {
        name = "FacBuildingInteract-BatchMode",
        type = UIConst.MOUSE_ICON_HINT.Default,
    })
end
FacBuildingInteractCtrl.OnClose = HL.Override() << function(self)
    self:_RemoveInteractOption()
    self:_ClearRegister()
end

FacBuildingInteractCtrl._InitInteractHighlightEffects = HL.Method() << function(self)
    do
        local prefab = self.loader:LoadGameObject(FacConst.BUILDING_INTERACT_BUILDING_INDICATOR_PATH)
        self.m_buildingInteractHighlightEffect = Utils.wrapLuaNode(self:_CreateWorldGameObject(prefab))
        self.m_buildingInteractHighlightEffect.gameObject.name = "BuildingInteractHighlightEffect"
        self.m_buildingInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    end

    do
        local prefab = self.loader:LoadGameObject(FacConst.BUILDING_INTERACT_NORMAL_INDICATOR_PATH)
        self.m_subBuildingInteractHighlightEffect = Utils.wrapLuaNode(self:_CreateWorldGameObject(prefab))
        self.m_subBuildingInteractHighlightEffect.gameObject.name = "SubBuildingInteractHighlightEffect"
        self.m_subBuildingInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)

        self.m_logisticInteractHighlightEffect = Utils.wrapLuaNode(self:_CreateWorldGameObject(prefab))
        self.m_logisticInteractHighlightEffect.gameObject.name = "LogisticInteractHighlightEffect"
        self.m_logisticInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    end

    do
        local prefab = self.loader:LoadGameObject(FacConst.BUILDING_INTERACT_HOVER_INDICATOR_PATH)
        self.m_hoverInteractHighlightEffect = Utils.wrapLuaNode(self:_CreateWorldGameObject(prefab))
        self.m_hoverInteractHighlightEffect.gameObject.name = "HoverInteractHighlightEffect"
        self.m_hoverInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    end

    do
        local prefab = self.loader:LoadGameObject(FacConst.BUILDING_INTERACT_PIPE_INDICATOR_PATH)
        self.m_pipeInteractHighlightEffect = Utils.wrapLuaNode(self:_CreateWorldGameObject(prefab))
        self.m_pipeInteractHighlightEffect.gameObject.name = "PipeInteractHighlightEffect"
        self.m_pipeInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    end
end

FacBuildingInteractCtrl.ExtractHotSwitchRuntimeState = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if not self.m_buildingInteractHighlightEffect and
        not self.m_subBuildingInteractHighlightEffect and
        not self.m_logisticInteractHighlightEffect and
        not self.m_pipeInteractHighlightEffect and
        not self.m_hoverInteractHighlightEffect then
        return nil
    end

    local state = {
        buildingInteractHighlightEffect = self.m_buildingInteractHighlightEffect,
        subBuildingInteractHighlightEffect = self.m_subBuildingInteractHighlightEffect,
        logisticInteractHighlightEffect = self.m_logisticInteractHighlightEffect,
        pipeInteractHighlightEffect = self.m_pipeInteractHighlightEffect,
        hoverInteractHighlightEffect = self.m_hoverInteractHighlightEffect,
    }

    self.m_buildingInteractHighlightEffect = nil
    self.m_subBuildingInteractHighlightEffect = nil
    self.m_logisticInteractHighlightEffect = nil
    self.m_pipeInteractHighlightEffect = nil
    self.m_hoverInteractHighlightEffect = nil

    return state
end

FacBuildingInteractCtrl.RestoreHotSwitchRuntimeState = HL.Override(HL.Opt(HL.Any)) << function(self, state)
    if not state then
        return
    end

    self.m_buildingInteractHighlightEffect = state.buildingInteractHighlightEffect
    self.m_subBuildingInteractHighlightEffect = state.subBuildingInteractHighlightEffect
    self.m_logisticInteractHighlightEffect = state.logisticInteractHighlightEffect
    self.m_pipeInteractHighlightEffect = state.pipeInteractHighlightEffect
    self.m_hoverInteractHighlightEffect = state.hoverInteractHighlightEffect
end

FacBuildingInteractCtrl.m_hoverGridRectInt = HL.Field(CS.UnityEngine.RectInt)

FacBuildingInteractCtrl._TailTick = HL.Method() << function(self)
    if not CS.UnityEngine.Application.isFocused then return end
    if LuaSystemManager.factory.inBatchSelectMode then
        if not DeviceInfo.usingTouch then
            local screenPos = InputManager.mousePosition
            local _, worldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(CameraManager.mainCamera:ScreenPointToRay(screenPos))
            CSFactoryUtil.SetHoverGrid(CS.UnityEngine.Vector2Int(math.floor(worldPos.x), math.floor(worldPos.z)))
        end

        local curScreenWorldRect = CSFactoryUtil.GetCurScreenWorldRect(CSFactoryUtil.Padding(0, 0, 0, 0), 6.5)
        local posXMin = math.floor(curScreenWorldRect.xMin)
        local posZMin = math.floor(curScreenWorldRect.yMin)
        local posXMax = math.ceil(curScreenWorldRect.xMax)
        local posZMax = math.ceil(curScreenWorldRect.yMax)
        local width = posXMax - posXMin
        local height = posZMax - posZMin
        local rectInt = CS.UnityEngine.RectInt(posXMin, posZMin, width, height)
        if not (self.m_hoverGridRectInt and self.m_hoverGridRectInt:Equals(rectInt)) then
            GameInstance.remoteFactoryManager.visual:UpdateAndShowGrid3D(rectInt)
            self.m_hoverGridRectInt = rectInt
        end

        GameInstance.remoteFactoryManager.batchSelect:TryUpdateRange()
    end

    if DeviceInfo.usingTouch and LuaSystemManager.factory.inTopView then

        return
    end
    self:_UpdateInteractTarget(LuaSystemManager.factory.inTopView)
end

FacBuildingInteractCtrl.OnToggleFacTopView = HL.Method(HL.Boolean) << function(self, active)
    self:_RemoveInteractOption()
    self.m_hoverInteractHighlightEffect.gameObject:SetActive(false)
    self.m_buildingInteractHighlightEffect.gameObject:SetActive(false)
    self.m_logisticInteractHighlightEffect.gameObject:SetActive(false)
    self.m_pipeInteractHighlightEffect.gameObject:SetActive(false)
    self.m_subBuildingInteractHighlightEffect.gameObject:SetActive(false)

    local pipeMarkShowAsBox = active and DeviceInfo.usingTouch
    self.m_pipeInteractHighlightEffect.content.gameObject:SetActive(not pipeMarkShowAsBox)
    self.m_pipeInteractHighlightEffect.boxMarkNode.gameObject:SetActive(pipeMarkShowAsBox)
end

FacBuildingInteractCtrl.UpdateInteractOption = HL.Method(HL.Opt(HL.Boolean)) << function(self, force)
    if force or self:IsShow() then
        self:_UpdateInteractTarget()
    end
end



FacBuildingInteractCtrl._OnUdpipeInteractStateChanged = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    self:_UpdateInteractTarget(LuaSystemManager.factory.inTopView, true)
end




FacBuildingInteractCtrl.m_tailTickId = HL.Field(HL.Number) << -1

FacBuildingInteractCtrl.m_slowlyUpdateCor = HL.Field(HL.Thread)


FacBuildingInteractCtrl._AddRegister = HL.Method() << function(self)
    local touchPanel = UIManager.commonTouchPanel
    touchPanel.onClick:AddListener(self.m_onClickScreen)
    touchPanel.onRightClick:AddListener(self.m_onRightClickScreen)
    touchPanel.onLongPress:AddListener(self.m_onLongPressScreen)
    touchPanel.onDrag:AddListener(self.m_onDragScreen)
    touchPanel.onDragBegin:AddListener(self.m_onDragScreenBegin)
    touchPanel.onDragEnd:AddListener(self.m_onDragScreenEnd)
    touchPanel.onPress:AddListener(self.m_onPressScreen)
    touchPanel.onRelease:AddListener(self.m_onReleaseScreen)

    self.m_tailTickId = LuaUpdate:Add("TailTick", function()
        self:_TailTick()
    end, true)

    self.m_slowlyUpdateCor = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_SlowlyUpdate()
        end
    end)
end

FacBuildingInteractCtrl._ClearRegister = HL.Method() << function(self)
    local touchPanel = UIManager.commonTouchPanel
    touchPanel.onClick:RemoveListener(self.m_onClickScreen)
    touchPanel.onRightClick:RemoveListener(self.m_onRightClickScreen)
    touchPanel.onLongPress:RemoveListener(self.m_onLongPressScreen)
    touchPanel.onDrag:RemoveListener(self.m_onDragScreen)
    touchPanel.onDragBegin:RemoveListener(self.m_onDragScreenBegin)
    touchPanel.onDragEnd:RemoveListener(self.m_onDragScreenEnd)
    touchPanel.onPress:RemoveListener(self.m_onPressScreen)
    touchPanel.onRelease:RemoveListener(self.m_onReleaseScreen)

    self.m_tailTickId = LuaUpdate:Remove(self.m_tailTickId)

    self:_StopPressHint()
    self:_StopBatchControllerDragCor()

    self.m_slowlyUpdateCor = self:_ClearCoroutine(self.m_slowlyUpdateCor)
    self.m_dragTargetsInBatchModeUpdateKey = LuaUpdate:Remove(self.m_dragTargetsInBatchModeUpdateKey)
end







FacBuildingInteractCtrl.m_onClickScreen = HL.Field(HL.Function)

FacBuildingInteractCtrl._OnClickScreen = HL.Method(HL.Userdata, HL.Opt(HL.Boolean)) << function(self, eventData, forceSelectFirstTarget)
    if not LuaSystemManager.factory.inTopView then
        return
    end

    if FactoryUtils.isInBuildMode() then
        return
    end

    Notify(MessageConst.FAC_TOPVIEW_HIDE_MULTI_TARGET_MENU)

    if LuaSystemManager.factory.inBatchSelectMode then
        self:_ClickScreenInBatchMode()
    else
        self:_UpdateInteractTarget(true, true)
        self:_UpdateFakeInteractOption()

        if LuaSystemManager.factory.inDestroyMode then
            self:_OnClickFakeInteractOption()
        else
            local targets = self:_CollectAllInteractTargets()
            if #targets == 0 then
                self:_RemoveInteractOption()
            elseif #targets == 1 or forceSelectFirstTarget then
                targets[1].action()
            else
                local screenRect = self:_CalcInteractScreenRect()
                Notify(MessageConst.FAC_TOPVIEW_SHOW_MULTI_TARGET_MENU, {
                    targets = targets,
                    screenRect = screenRect,
                })
            end
        end
    end
end

FacBuildingInteractCtrl._ClickScreenInBatchMode = HL.Method() << function(self)
    self:_UpdateInteractTarget(true, true)
    local nodeId = self.m_interactPipeNodeId or self.m_interactFacNodeId
    if nodeId then
        if Utils.isInBlackbox() and CSFactoryUtil.IsPreplacedBuilding(nodeId) then

            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BATCH_MODE_CLICK_BLACKBOX_NODE)
            return
        end
    end
    local unitIndex
    local selectSingleGrid
    if DeviceInfo.usingKeyboard then
        selectSingleGrid = InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.LeftControl)
    elseif DeviceInfo.usingController then
        selectSingleGrid = InputManagerInst:GetKey(CS.Beyond.Input.GamepadKeyCode.LB)
    end
    if self.m_interactPipeNodeId then
        if selectSingleGrid then
            unitIndex = self.m_interactPipeUnitIndex
        end
    elseif not nodeId and self.m_interactLogisticPos then
        local succ, targetNodeId
        succ, targetNodeId, unitIndex = GameInstance.remoteFactoryManager:TrySampleConveyor(self.m_interactLogisticPos)
        nodeId = targetNodeId
        if not selectSingleGrid then
            unitIndex = nil
        end
    end
    if not nodeId then
        return
    end
    if FactoryUtils.isInvalidBuilding(nodeId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_INVALID_BUILDING_OP_DISABLE_TOAST)
        return
    end
    local slotId = FactoryUtils.getPendingBuildingNodeSlotId(nodeId)
    if slotId then
        unitIndex = nil
    end
    if not unitIndex then
        local isAdd = LuaSystemManager.factory.batchSelectTargets[nodeId] ~= true
        if slotId then

            CSFactoryUtil.AddAllSlotNodes(slotId)
            for _, v in pairs(CSFactoryUtil.s_tmpNodeIdList) do
                self:_SelectBatchTarget(v, isAdd, nil)
            end
        else
            self:_SelectBatchTarget(nodeId, isAdd, nil)
        end
    else
        local indexList = LuaSystemManager.factory.batchSelectTargets[nodeId]
        if not indexList then

            self:_SelectBatchTarget(nodeId, true, unitIndex)
        else
            local isAdd
            if indexList == true then
                isAdd = false
            else
                isAdd = indexList[unitIndex] == nil
            end
            self:_SelectBatchTarget(nodeId, isAdd, unitIndex)
        end
    end
end

FacBuildingInteractCtrl.m_onRightClickScreen = HL.Field(HL.Function)

FacBuildingInteractCtrl._OnRightClickScreen = HL.Method(HL.Userdata) << function(self, eventData)
    if not LuaSystemManager.factory.inTopView then
        return
    end







end

FacBuildingInteractCtrl.m_onLongPressScreen = HL.Field(HL.Function)

FacBuildingInteractCtrl._OnLongPressScreen = HL.Method(HL.Userdata) << function(self, eventData)
    if not LuaSystemManager.factory.inTopView then
        return
    end
    if FactoryUtils.isInBuildMode() then
        return
    end
    if LuaSystemManager.factory.inDestroyMode then
        if DeviceInfo.usingTouch then

            self.view.batchToggle.isOn = true
            GameInstance.mobileMotionManager:PostEventCommonShort()
        elseif DeviceInfo.usingKeyboard then
            if next(LuaSystemManager.factory.batchSelectTargets) then
                local _, desCtrl = UIManager:IsOpen(PanelId.FacDestroyMode)
                desCtrl:TryStartBatchMove()
            end
        end
        return
    end
    if DeviceInfo.usingTouch then
        self:_UpdateInteractTarget(false, true)
    end
    local nodeId = self.m_interactFacNodeId
    if not nodeId or not self.m_interactFacNodeIdIsBuilding then
        if DeviceInfo.usingTouch then
            self:_UpdateFakeInteractOption()
        end
        return
    end

    if FactoryUtils.canMoveBuilding(nodeId, true) then
        local args = { nodeId = nodeId }
        if DeviceInfo.usingTouch then


            args.triggerPressScreen = true
        end
        Notify(MessageConst.FAC_ENTER_BUILDING_MODE, args)
    end
end

FacBuildingInteractCtrl.m_onDragScreen = HL.Field(HL.Function)

FacBuildingInteractCtrl._OnDragScreen = HL.Method(HL.Userdata) << function(self, eventData)
    if LuaSystemManager.factory.inDragSelectBatchMode then
        if not InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse2) then
            self:_OnDragInBatchMode(eventData)
        end
    end
end


FacBuildingInteractCtrl.m_onDragScreenBegin = HL.Field(HL.Function)

FacBuildingInteractCtrl._OnDragScreenBegin = HL.Method(Vector2) << function(self, pos)
    if LuaSystemManager.factory.inDragSelectBatchMode then
        if not InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse2) then
            self:_OnDragBeginInBatchMode(pos)
        end
    end
    self:_StopPressHint()
end


FacBuildingInteractCtrl.m_onDragScreenEnd = HL.Field(HL.Function)

FacBuildingInteractCtrl._OnDragScreenEnd = HL.Method(HL.Opt(Vector2)) << function(self, pos)
    if LuaSystemManager.factory.inDragSelectBatchMode then
        self:_OnDragEndInBatchMode()
        if DeviceInfo.usingTouch then

            self.view.batchToggle.isOn = false
        end
    end
end

local pressHintDelay = 0.2
local pressDuration = 0.5
FacBuildingInteractCtrl.m_onPressScreen = HL.Field(HL.Function)

FacBuildingInteractCtrl._OnPressScreen = HL.Method(HL.Userdata) << function(self, eventData)
    if not LuaSystemManager.factory.inTopView then
        return
    end
    if FactoryUtils.isInBuildMode() then
        return
    end
    local isLongPressToDragHint
    if LuaSystemManager.factory.inDestroyMode then
        if DeviceInfo.usingTouch then

            self:_StopPressHint()
            isLongPressToDragHint = true
        elseif DeviceInfo.usingKeyboard then

            local _, desCtrl = UIManager:IsOpen(PanelId.FacDestroyMode)
            if not desCtrl:CheckBatchActionValid(true, true) then
                return
            end
            if next(LuaSystemManager.factory.batchSelectTargets) == nil then
                return
            end
            self:_StopPressHint()
        else
            return
        end
    else

        if DeviceInfo.usingTouch then
            self:_UpdateInteractTarget(false, true)
        end
        self:_StopPressHint()
        if not self.m_interactFacNodeId or not self.m_interactFacNodeIdIsBuilding then
            return
        end
        if not FactoryUtils.canMoveBuilding(self.m_interactFacNodeId) then
            return
        end
    end
    self.m_pressHintCor = self:_StartCoroutine(function()
        local targetTime = Time.unscaledTime + pressHintDelay
        repeat
            coroutine.step()
            if Input.touchCount > 1 then

                self:_StopPressHint()
                return
            end
        until (Time.unscaledTime >= targetTime)

        local hint = self.view.longPressHint
        hint.longPressToDragHint.gameObject:SetActive(isLongPressToDragHint)
        hint.gameObject:SetActive(true)

        local canvasRect = self.view.transform.rect
        local ratio = canvasRect.width / Screen.width
        local pos = (InputManager.mousePosition * ratio):XY()
        local padding = self.view.config.LONG_PRESS_TO_DRAG_HINT_PADDING
        pos.x = lume.clamp(pos.x, padding.z, canvasRect.width - padding.w)
        pos.y = lume.clamp(pos.y, padding.y, canvasRect.height - padding.x)
        hint.transform.anchoredPosition = pos

        hint.image.fillAmount = 0
        hint.image:DOFillAmount(1, pressDuration - pressHintDelay):OnComplete(function()
            if isLongPressToDragHint then
                hint.animationWrapper:PlayOutAnimation()
            else
                self:_StopPressHint()
            end
        end)

        targetTime = Time.unscaledTime + pressDuration - pressHintDelay
        repeat
            coroutine.step()
            if Input.touchCount > 1 then

                self:_StopPressHint()
                return
            end
        until (Time.unscaledTime >= targetTime)
    end)
end

FacBuildingInteractCtrl.m_onReleaseScreen = HL.Field(HL.Function)

FacBuildingInteractCtrl._OnReleaseScreen = HL.Method(HL.Userdata) << function(self, eventData)
    if not LuaSystemManager.factory.inTopView then
        return
    end
    if FactoryUtils.isInBuildMode() then
        return
    end
    if LuaSystemManager.factory.inDestroyMode then
        if DeviceInfo.usingTouch then
            self:_StopPressHint()
            self.view.batchToggle.isOn = false
        elseif DeviceInfo.usingKeyboard then
            self:_StopPressHint()
        end
    else
        self:_StopPressHint()
        if DeviceInfo.usingTouch then
            self:_RemoveInteractOption()
        end
    end
end

FacBuildingInteractCtrl._StopPressHint = HL.Method() << function(self)
    self.m_pressHintCor = self:_ClearCoroutine(self.m_pressHintCor)
    self.view.longPressHint.gameObject:SetActive(false)
    self.view.longPressHint.image:DOKill()
end

FacBuildingInteractCtrl.m_pressHintCor = HL.Field(HL.Thread)






FacBuildingInteractCtrl.OnBuildModeChange = HL.Method(HL.Number) << function(self, mode)
    if mode ~= FacConst.FAC_BUILD_MODE.Normal then
        self:_RemoveInteractOption()
    end
    CSFactoryUtil.ClearSelectGrids()
    CSFactoryUtil.ClearHoverGrid()
    self.m_hoverGridRectInt = nil
end

FacBuildingInteractCtrl.OnFacDestroyModeChange = HL.Method(HL.Boolean) << function(self, inDestroyMode)

    local reserveBatchMode
    local _, desPanelCtrl = UIManager:IsOpen(PanelId.FacDestroyMode)
    if not inDestroyMode and InputManagerInst.inChangingInputDevice then

        reserveBatchMode = true
    elseif inDestroyMode and desPanelCtrl.m_args.isFromChangeInputDevice then

        reserveBatchMode = true
    end

    self:_ClearBeltHoverHint()
    if LuaSystemManager.factory.inTopView and DeviceInfo.usingController then

        self:_UpdateInteractTarget(LuaSystemManager.factory.inTopView)
    else
        self:_RemoveInteractOption()
        self:_UpdateInteractTarget(LuaSystemManager.factory.inTopView, true)
    end

    self:_ResetBatch(inDestroyMode)
    self.view.batchNode:ClearTween(false)
    local isBatch = inDestroyMode and LuaSystemManager.factory.inTopView
    if isBatch then

        self.view.batchNode.gameObject:SetActive(true)
        if not reserveBatchMode then
            self:_ChangeBatchMode(true)
        end
    else
        self.view.batchNode.gameObject:SetActive(false)
        if not reserveBatchMode then
            self:_ChangeBatchMode(false)
        end
    end
    InputManagerInst:ToggleGroup(self.m_batchControllerBindingGroupId, isBatch and DeviceInfo.usingController)

    CSFactoryUtil.ClearSelectGrids()
    CSFactoryUtil.ClearHoverGrid()
    self.m_hoverGridRectInt = nil
end

FacBuildingInteractCtrl.BeforeExitDestroyMode = HL.Method() << function(self)

    self.view.batchNode:PlayOutAnimation()
end

FacBuildingInteractCtrl.OnBuildingRemoved = HL.Method(HL.Table) << function(self, arg)

    self:_RemoveInteractOption()
    if LuaSystemManager.factory.inBatchSelectMode then
        self:_ClearAllBatchTargets()
    end
end

FacBuildingInteractCtrl.OnPendingSlotsRemoved = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    self:_RemoveInteractOption()
    if LuaSystemManager.factory.inBatchSelectMode then
        self:_ClearAllBatchTargets()
    end
end

FacBuildingInteractCtrl._OnInteractFactory = HL.Method(HL.Table) << function(self, option)
    local buildingNodeId = option.buildingNodeId
    if not string.isEmpty(buildingNodeId) then

        if LuaSystemManager.factory.inDestroyMode then
            if Utils.isInSettlementDefenseDefending() then
                if GameInstance.player.towerDefenseSystem.towerDefenseGame:IsPreBattleBuilding(buildingNodeId) then

                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FORBID_DESTROY_PRE_BATTLE_BUILDING)
                    return
                end
            end
            local noConfirm = true
            local hintText
            local isOthersSocialBuilding = FactoryUtils.isOthersSocialBuilding(buildingNodeId)
            if isOthersSocialBuilding then
                noConfirm = false
                hintText = Language.LUA_FAC_DEL_SOCIAL_BUILDING_CONFIRM
            end
            FactoryUtils.delBuilding(buildingNodeId, nil, noConfirm, hintText)
            return
        end
        local curChapter = FactoryUtils.getCurChapterInfo()
        local node = curChapter:GetNode(buildingNodeId)
        local preventOpen = false
        if node and node.nodeType == GEnums.FCNodeType.SubHub:GetHashCode() then
            if not node.power.inPower then
                preventOpen = true
                Notify(MessageConst.SHOW_TOAST, Language.lang_int_jumpmachine_toast)
            end
        end
        if not preventOpen then
            if not option.subBuildingIndex then
                self:_OpenBuildingPanel(buildingNodeId)
            else
                if self.m_onlyValidHubUnloaderIndex > 0 then
                    if option.subBuildingIndex ~= self.m_onlyValidHubUnloaderIndex then
                        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_HUB_UNLOADER_BLOCKED)
                        return
                    end
                end
                self:_OpenBuildingPanel(buildingNodeId, { subIndex = option.subBuildingIndex }, "unloader_1")
            end
        end
    end

    local nodeId = option.nodeId
    if nodeId then

        if LuaSystemManager.factory.inDestroyMode then
            if not FactoryUtils.canDelBuilding(option.nodeId, true) then
                return
            end
            local core = GameInstance.player.remoteFactory.core
            if option.isAll or not option.unitIndex then
                core:Message_OpDismantle(Utils.getCurrentChapterId(), option.nodeId)
            else
                GameInstance.remoteFactoryManager:DismantleUnitFromConveyor(Utils.getCurrentChapterId(), option.nodeId, option.unitIndex)
            end
            AudioAdapter.PostEvent("au_int_belt_remove_short")
        else
            if FactoryUtils.isPendingBuildingNode(nodeId) then
                Notify(MessageConst.FAC_OPEN_LOGISTIC_PANEL, { nodeId = option.nodeId, index = option.unitIndex })
            else
                local nodeHandler = FactoryUtils.getBuildingNodeHandler(nodeId)
                if nodeHandler ~= nil and not INVALID_INTERACT_BUILDING_LIST[nodeHandler.templateId] then
                    Notify(MessageConst.FAC_OPEN_LOGISTIC_PANEL, { nodeId = option.nodeId, index = option.unitIndex })
                end
            end
        end
    end
end

FacBuildingInteractCtrl._OpenBuildingPanel = HL.Method(HL.Opt(HL.Any, HL.Table, HL.String)).Return(HL.Opt(HL.Number))
<< function(self, nodeId, customArg, buildingId)
    Notify(MessageConst.FAC_OPEN_BUILDING_PANEL, {
        nodeId = nodeId,
        customArg = customArg,
        panelBuildingDataId = buildingId,
    })
end



FacBuildingInteractCtrl._IsUdPipeConnected = HL.Method(HL.Number).Return(HL.Boolean) << function(self, nodeId)
    local nodeHandler = FactoryUtils.getBuildingNodeHandler(nodeId)
    if nodeHandler == nil then
        return false
    end
    local cpt = nodeHandler:GetComponentInPosition(GEnums.FCComponentPos.FluidUdPipe:GetHashCode())
    return cpt ~= nil and cpt.valid and cpt.udPipe.connectComponent ~= nil
end






FacBuildingInteractCtrl._AppendDebugSuffix = HL.Method(HL.String, HL.Number, HL.Number).Return(HL.String)
<< function(self, text, chapterId, nodeId)
    if not CS.Beyond.Gameplay.Factory.BuildingName.s_showDebugNodeId then
        return text
    end
    return text .. " (ch:" .. chapterId .. ", nid:" .. nodeId .. ")"
end

FacBuildingInteractCtrl._RemoveInteractOption = HL.Method() << function(self)
    CSFactoryUtil.DispatchFactoryBuildingApproachSelectedChanged(self.m_interactFacNodeId, nil)
    self.m_interactFacNodeId = nil
    self.m_interactFacOverlapNodeIds = nil
    self.m_selectedInteractFacNodeId = nil
    self.m_buildingUseDefaultOption = nil
    self.m_interactSubBuildingIndex = -1
    if self.m_interactLogisticPos then
        self:_ToggleBeltHoverHint(self.m_interactLogisticPos, false)
        self.m_interactLogisticPos = nil
    end
    self.m_selectedInteractSubBuildingIndex = -1
    self.m_selectedInteractLogisticPos = nil
    self.m_selectedInteractPipeNodeId = nil
    self.m_interactPipeNodeId = nil
    self:_SetHighlightedPipeNode()

    self.m_hoverInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    self.m_buildingInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    self.m_logisticInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    self.m_pipeInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    self.m_subBuildingInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
    self.view.listNode.gameObject:SetActiveIfNecessary(false)
    self.view.hoverInfoTextNode.gameObject:SetActiveIfNecessary(false)

    self:_UpdatePendingSlotHighlight(0, 0, 0)

    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
        sourceId = "MainBuilding",
    })
    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
        sourceId = "SubBuilding",
    })
    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
        sourceId = INTERACT_SOURCE_ID_BELT,
    })
    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
        sourceId = INTERACT_SOURCE_ID_DELETE_ALL_BELT,
    })
    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
        sourceId = INTERACT_SOURCE_ID_DELETE_ALL_PIPE,
    })
    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
        sourceId = INTERACT_SOURCE_ID_PIPE,
    })

    GameWorld.interactiveFacWrapperManager:OnFacBuildingInteractOptionRemoveAll()


    GameInstance.player.generalAbilitySystem:DeactivateTempAbility(GeneralAbilityType.BuildingLike)
end

FacBuildingInteractCtrl._SetEffect = HL.Method(HL.Table, CS.UnityEngine.Vector3, HL.Number, HL.Opt(CS.UnityEngine.Vector3, CS.UnityEngine.Vector3))
        << function(self, effect, pos, offsetY, rot, scale)
    if DeviceInfo.usingTouch and LuaSystemManager.factory.inTopView then
        return
    end
    pos.y = pos.y + offsetY
    effect.transform.position = pos
    effect.transform.eulerAngles = rot or Vector3.zero
    scale = scale or Vector3.one
    effect.transform.localScale = scale
    for k = 1, 4 do
        effect["corner" .. k].transform.localScale = Vector3(1 / scale.x, 1, 1 / scale.z)
    end
    if effect.gameObject.activeSelf then
        return
    end
    effect.gameObject:SetActive(true)

    for k = 1, 4 do
        effect["effect" .. k]:Update(0)
    end
end

FacBuildingInteractCtrl._SetBoxEffect = HL.Method(HL.Table, CS.UnityEngine.Vector3, HL.Opt(CS.UnityEngine.Vector3, HL.String)) << function(self, effect, pos, rot, buildingId)
    if DeviceInfo.usingTouch and LuaSystemManager.factory.inTopView then
        return
    end
    local scale, reverseScale
    if buildingId then
        local data = Tables.factoryBuildingTable:GetValue(buildingId)
        scale = Vector3(data.range.width + 0.3, data.modelHeight, data.range.depth + 0.3)
        reverseScale = Vector3(1 / scale.x, 1 / scale.y, 1 / scale.z)
    else
        scale = Vector3.one
        reverseScale = Vector3.one
    end
    effect.transform.position = pos
    effect.transform.eulerAngles = rot or Vector3.zero
    effect.transform.localScale = scale
    for k = 1, 8 do
        effect["corner" .. k].transform.localScale = reverseScale
    end
    if effect.gameObject.activeSelf then
        return
    end
    effect.gameObject:SetActive(true)

    for k = 1, 8 do
        effect["effect" .. k]:Update(0)
    end
end








FacBuildingInteractCtrl._GetGridUnitFromWorldPos = HL.Method(Vector2, HL.Number).Return(HL.Table) << function(self, worldPos, sampleType)
    local gridPos = Unity.Vector2Int(lume.round(worldPos.x), lume.round(worldPos.y))

    local success, nodeId, unitIndex, unitTemplateId
    if sampleType == FacConst.FAC_SAMPLE_TYPE.Belt then
        success, nodeId, unitIndex, unitTemplateId = GameInstance.remoteFactoryManager:TrySampleConveyor(gridPos)
    elseif sampleType == FacConst.FAC_SAMPLE_TYPE.Pipe then
        success, nodeId, unitIndex, unitTemplateId = GameInstance.remoteFactoryManager:TrySamplePipe(gridPos)
    end

    return {
        success = success,
        nodeId = nodeId,
        unitIndex = unitIndex,
        unitTemplateId = unitTemplateId,
    }
end






FacBuildingInteractCtrl.m_interactFacNodeId = HL.Field(HL.Any)


FacBuildingInteractCtrl.m_interactFacOverlapNodeIds = HL.Field(HL.Any)

FacBuildingInteractCtrl.m_interactFacNodeIdIsBuilding = HL.Field(HL.Boolean) << false

FacBuildingInteractCtrl.m_buildingUseDefaultOption = HL.Field(HL.Any)

FacBuildingInteractCtrl.m_interactSubBuildingIndex = HL.Field(HL.Number) << -1

FacBuildingInteractCtrl.m_interactLogisticPos = HL.Field(CS.UnityEngine.Vector2Int)

FacBuildingInteractCtrl.m_interactPipeNodeId = HL.Field(HL.Any)

FacBuildingInteractCtrl.m_interactPipeUnitIndex = HL.Field(HL.Any)

FacBuildingInteractCtrl.m_delayedPipeNodeInfo = HL.Field(HL.Table)

FacBuildingInteractCtrl.m_lastDetectWorldPos = HL.Field(HL.Any)


FacBuildingInteractCtrl._UpdateInteractTarget = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, isPreview, forceUpdate)
    if FactoryUtils.isInBuildMode() then
        return
    end

    local hasTarget = false
    local isClickMode = LuaSystemManager.factory.inTopView
    local chapterId = Utils.getCurrentChapterId()

    if isClickMode and DeviceInfo.usingKeyboard and not UIManager.commonTouchPanel.isPointerEntered then

        if self.m_interactFacNodeId or self.m_interactLogisticPos or self.m_interactPipeNodeId then
            self:_RemoveInteractOption()
        end
        return
    end


    local playerPos, playerForward, playerRight
    local maxDist, maxAngle
    if not isClickMode then
        local playerTrans = GameUtil.playerTrans
        playerPos = playerTrans.position
        playerForward = playerTrans.forward
        playerRight = playerTrans.right

        if LuaSystemManager.factory.inDestroyMode then
            maxDist = self.view.config.BUILDING_INTERACT_RANGE + 1
        else
            maxDist = self.view.config.BUILDING_INTERACT_RANGE
        end
        maxAngle = self.view.config.BUILDING_INTERACT_ANGLE
    else
        local curMousePos = InputManager.mousePosition
        local camRay = CameraManager.mainCamera:ScreenPointToRay(curMousePos)
        local _, worldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(camRay)
        if DeviceInfo.usingController then




            local objPos = LuaSystemManager.factory.topViewControllerMouseMoveTarget.position
            if (objPos - worldPos):XZ().sqrMagnitude <= 0.1 then
                worldPos = objPos
            end
        end
        playerPos = worldPos
        self.m_lastDetectWorldPos = worldPos
        playerForward = Vector3.forward
        playerRight = Vector3.right

        maxDist = 0
        maxAngle = -1
    end




    local useAsyncRst = not (isClickMode or LuaSystemManager.factory.inDestroyMode)


    local foundBuilding, targetBuildingNodeId, targetBuildingTemplateId, targetBuildingPosition, targetBuildingRotation, targetBuildingAdjustMapHeight, subBuildingInfo
    local targetBuildingChapterId, pipeChapterId, beltChapterId
    local csOverlappingNodeIds
    if useAsyncRst then

        local succ, info = CSFactoryUtil.GetShouldInteractFacEntityUsingAsyncRst()
        foundBuilding = succ and info.valid and info.nodeId > 0
        if foundBuilding then
            targetBuildingNodeId = info.nodeId
            targetBuildingChapterId = info.chapterId
            targetBuildingTemplateId = info.templateName
            targetBuildingPosition = info.position:ToVector3()
            targetBuildingRotation = info.rotation:ToVector3()
            targetBuildingAdjustMapHeight = info.adjustMapHeight
            if info.subIndex >= 0 then
                subBuildingInfo = {
                    subIndex = info.subIndex,
                    position = info.subPosition:ToVector3(),
                    rotation = info.subRotation:ToVector3(),
                    dist = info.subDist,
                }
            end
        end
    else
        foundBuilding, targetBuildingNodeId, targetBuildingTemplateId, targetBuildingPosition, targetBuildingRotation, targetBuildingAdjustMapHeight, subBuildingInfo, csOverlappingNodeIds
            = CSFactoryUtil.GetShouldInteractFacEntity(LuaSystemManager.factory.inDestroyMode, maxDist, maxAngle, playerPos, playerForward, isClickMode, isClickMode)
    end


    local overlapList = nil
    if csOverlappingNodeIds and csOverlappingNodeIds.Count > 0 then
        overlapList = {}
        for i = 0, csOverlappingNodeIds.Count - 1 do
            overlapList[#overlapList + 1] = csOverlappingNodeIds[i]
        end
    end
    self.m_interactFacOverlapNodeIds = overlapList

    local buildingPendingSlotId = foundBuilding and CSFactoryUtil.GetBlueprintSlotId(chapterId, targetBuildingNodeId) or 0
    local isPendingBuilding = buildingPendingSlotId > 0

    if foundBuilding then
        hasTarget = true


        local nodeId = targetBuildingNodeId
        local isBuilding, buildingData = Tables.factoryBuildingTable:TryGetValue(targetBuildingTemplateId)
        local buildingChanged, useDefaultOption
        if not isClickMode then
            if isPendingBuilding then
                useDefaultOption = true
            else
                useDefaultOption = CSFactoryUtil.ShouldShowBuildingUIInteractOption(nodeId)
            end
            buildingChanged = not self.m_interactFacNodeId or self.m_interactFacNodeId ~= nodeId or self.m_buildingUseDefaultOption ~= useDefaultOption
        else
            buildingChanged = not self.m_interactFacNodeId or self.m_interactFacNodeId ~= nodeId
        end
        local isHub = isBuilding and (buildingData.type == GEnums.FacBuildingType.Hub or buildingData.type == GEnums.FacBuildingType.SubHub)
        local isXLoader = isBuilding and (buildingData.type == GEnums.FacBuildingType.Loader or buildingData.type == GEnums.FacBuildingType.Unloader)
        local isUdPipe = isBuilding and (buildingData.type == GEnums.FacBuildingType.UdPipeLoader or buildingData.type == GEnums.FacBuildingType.UdPipeUnloader)

        local needUpdateBuildingEffect = false
        if buildingChanged or forceUpdate then
            self.m_interactFacNodeIdIsBuilding = isBuilding
            if not isClickMode then
                local isInvalidBuilding = FactoryUtils.isInvalidBuilding(nodeId)

                local args = {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = "MainBuilding",
                    sortId = 200,
                    buildingNodeId = nodeId,
                    templateId = targetBuildingTemplateId,
                    icon = LuaSystemManager.factory.inDestroyMode
                        and (FactoryUtils.isOthersSocialBuilding(nodeId) and INTERACT_ICON_DELETE_SOCIAL or INTERACT_ICON_DELETE)
                        or (isInvalidBuilding and INTERACT_ICON_DELETE or INTERACT_ICON_COMMON),
                    isDel = isInvalidBuilding,
                }
                if isPendingBuilding then
                    args.text = FactoryUtils.getPendingSlotName(buildingPendingSlotId)
                    args.action = function()
                        self:_OnInteractFactory({ buildingNodeId = nodeId })
                    end
                elseif isInvalidBuilding then
                    if isBuilding then
                        args.text = string.format(Language.LUA_FACTORY_INVALID_BUILDING_INTERACT, buildingData.name)
                    else
                        local unitData = FactoryUtils.getLogisticData(targetBuildingTemplateId)
                        args.text = string.format(Language.LUA_FACTORY_INVALID_BUILDING_INTERACT, unitData.name)
                    end
                    args.action = function()
                        FactoryUtils.delBuilding(nodeId)
                    end
                elseif isBuilding then
                    args.text = buildingData.name


                    if isUdPipe and not LuaSystemManager.factory.inDestroyMode and self:_IsUdPipeConnected(nodeId) then
                        args.text = string.format(Language.LUA_FAC_UDPIPE_INTERACT_CONNECTED, buildingData.name)
                    end
                    args.action = function()
                        self:_OnInteractFactory({ buildingNodeId = nodeId })
                    end
                    if not LuaSystemManager.factory.inDestroyMode then
                        args.longPressAction = function()
                            if FactoryUtils.canMoveBuilding(nodeId, true) then
                                Notify(MessageConst.FAC_ENTER_BUILDING_MODE, { nodeId = nodeId })
                            else
                                Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_MOVE_NOT_ALLOWED)
                            end
                        end
                    end
                else
                    local unitData = FactoryUtils.getLogisticData(targetBuildingTemplateId)
                    args.text = unitData.name
                    args.action = function()
                        self:_OnInteractFactory({ nodeId = nodeId })
                    end
                end
                if useAsyncRst then
                    args.text = self:_AppendDebugSuffix(args.text, targetBuildingChapterId, nodeId)
                end

                if LuaSystemManager.factory.inDestroyMode then
                    args.isDel = true
                end

                if isBuilding and not isPendingBuilding and not isInvalidBuilding then
                    if not self.m_interactFacNodeId then
                        GameWorld.interactiveFacWrapperManager:OnFacBuildingInteractOptionAdded(nodeId)
                    else
                        GameWorld.interactiveFacWrapperManager:OnFacBuildingInteractOptionUpdate(nodeId)
                    end
                    if not args.isDel then

                        if FactoryUtils.isOthersSocialBuilding(nodeId) then

                            local canLike = FactoryUtils.canLikeSocialBuilding(nodeId)
                            GameInstance.player.generalAbilitySystem:ActivateTempAbility(GeneralAbilityType.BuildingLike, canLike, nodeId)
                            local abilityState = canLike and AbilityState.Idle or AbilityState.ForbiddenUse
                            GameInstance.player.generalAbilitySystem:SwitchAbilityStateByType(GeneralAbilityType.BuildingLike, abilityState)
                        else

                            GameInstance.player.generalAbilitySystem:DeactivateTempAbility(GeneralAbilityType.BuildingLike)
                        end
                    end
                else
                    if self.m_interactFacNodeId then
                        GameWorld.interactiveFacWrapperManager:OnFacBuildingInteractOptionRemove(self.m_interactFacNodeId)
                    end
                end

                if useDefaultOption then
                    if isHub then
                        if Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacHub) then
                            Notify(MessageConst.ADD_INTERACT_OPTION, args)
                        else

                            Notify(MessageConst.REMOVE_INTERACT_OPTION, args)
                        end
                    else
                        Notify(MessageConst.ADD_INTERACT_OPTION, args)
                    end
                else
                    if args.isDel then
                        if CSFactoryUtil.IsSoil(nodeId) then
                            Notify(MessageConst.ADD_INTERACT_OPTION, args)
                        else
                            Notify(MessageConst.REMOVE_INTERACT_OPTION, args)
                        end
                    else
                        Notify(MessageConst.REMOVE_INTERACT_OPTION, args)
                    end
                end
            end

            needUpdateBuildingEffect = true
        end


        local newSubIndex = -1
        local subBuildingChanged, subBuildingPos, subBuildingSize, subBuildingRot, subNodeId, subBuildingTemplateId, subBuildingYOffset
        if not isPendingBuilding and isBuilding and not LuaSystemManager.factory.inDestroyMode then
            if isHub and subBuildingInfo then
                local portUnlocked = GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedPort
                if portUnlocked then
                    local minSubDist = isClickMode and 1 or self.view.config.SUB_BUILDING_SET_TOP_DIST
                    if subBuildingInfo.dist <= minSubDist then
                        newSubIndex = LuaIndex(subBuildingInfo.subIndex)
                        minSubDist = subBuildingInfo.dist
                        subBuildingPos = subBuildingInfo.position
                        subBuildingSize = Vector3.one
                        subBuildingRot = subBuildingInfo.rotation
                    end
                    subBuildingChanged = newSubIndex ~= -1 or (self.m_interactSubBuildingIndex ~= -1)
                end
            elseif isXLoader and not isClickMode then

                if buildingChanged or forceUpdate then
                    local cptPos = buildingData.type == GEnums.FacBuildingType.Loader and GEnums.FCComponentPos.BusLoader or GEnums.FCComponentPos.Selector
                    local syncNode = FactoryUtils.getBuildingNodeHandler(nodeId)
                    local cpt = syncNode:GetComponentInPosition(cptPos:GetHashCode())
                    local attachedBus = buildingData.type == GEnums.FacBuildingType.Loader and cpt.busLoader.attachedBus or cpt.selector.attachedBus
                    if cpt and attachedBus.Count > 0 then

                        local busNode = attachedBus[0]
                        subBuildingTemplateId = busNode.templateId
                        local succ, busData = Tables.factoryBuildingTable:TryGetValue(subBuildingTemplateId)
                        if succ then
                            newSubIndex = 1
                            subNodeId = busNode.nodeId
                            subBuildingPos = busNode.transform.worldPosition
                            local adjustHeight = FactoryUtils.queryVoxelRangeHeightAdjust(subBuildingPos.x, subBuildingPos.y, subBuildingPos.z)
                            subBuildingPos.y = adjustHeight
                            subBuildingSize = Vector3(busData.range.width, 1, busData.range.depth)
                            subBuildingRot = busNode.transform.worldRotation
                            subBuildingChanged = true
                            subBuildingYOffset = FacConst.BUILDING_SELECT_EFFECT_OFFSET[busData.type]
                        end
                    end
                else

                    subBuildingChanged = false
                    newSubIndex = self.m_interactSubBuildingIndex
                end
            end
        end
        if subBuildingChanged == nil then
            subBuildingChanged = self.m_interactSubBuildingIndex ~= -1
        end
        if subBuildingChanged or forceUpdate then
            local effect = isPreview and self.m_hoverInteractHighlightEffect or self.m_subBuildingInteractHighlightEffect
            if newSubIndex >= 0 then
                if isPreview and isHub then

                    needUpdateBuildingEffect = false
                end
                subBuildingYOffset = subBuildingYOffset or FacConst.DEFAULT_BUILDING_SELECT_EFFECT_OFFSET
                self:_SetEffect(effect, subBuildingPos, subBuildingYOffset, Vector3(subBuildingRot.x, subBuildingRot.y, subBuildingRot.z), subBuildingSize)
            else
                if isPreview then
                    needUpdateBuildingEffect = true
                else
                    effect.gameObject:SetActiveIfNecessary(false)
                end
            end

            if not isClickMode then
                local args = {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = "SubBuilding",
                    templateId = subBuildingTemplateId or targetBuildingTemplateId,
                    buildingNodeId = subNodeId or nodeId,
                    subBuildingIndex = newSubIndex,
                }
                local msg
                if newSubIndex == -1 then
                    msg = MessageConst.REMOVE_INTERACT_OPTION
                elseif isHub then
                    if not GameWorld.gameMechManager.linkWireBrain.isLinking then
                        args.sortId = -100
                    else
                        args.sortId = 100
                    end
                    args.setTopAsSelectedWhenSort = true
                    if self.m_interactSubBuildingIndex == -1 then
                        msg = MessageConst.ADD_INTERACT_OPTION
                    else
                        msg = MessageConst.UPDATE_INTERACT_OPTION
                        args.needReSort = true
                    end

                    local inputName = I18nUtils.CombineStringWithLanguageSpilt(Language.LUA_FAC_HUB_INPUT, newSubIndex)
                    args.text = string.format("<color=#fff100>%s</color>", inputName)
                    if useAsyncRst then
                        args.text = self:_AppendDebugSuffix(args.text, targetBuildingChapterId, nodeId)
                    end
                    args.icon = "btn_fac_hub_port"
                    args.action = function()
                        self:_OnInteractFactory({
                            buildingNodeId = nodeId,
                            subBuildingIndex = newSubIndex,
                        })
                    end
                elseif isXLoader then
                    args.sortId = 300
                    args.needReSort = true
                    if self.m_interactSubBuildingIndex == -1 then
                        msg = MessageConst.ADD_INTERACT_OPTION
                    else
                        msg = MessageConst.UPDATE_INTERACT_OPTION
                    end
                    args.text = Tables.factoryBuildingTable[subBuildingTemplateId].name
                    if useAsyncRst then
                        args.text = self:_AppendDebugSuffix(args.text, targetBuildingChapterId, nodeId)
                    end
                    args.action = function()
                        self:_OnInteractFactory({
                            buildingNodeId = subNodeId,
                        })
                    end
                end
                Notify(msg, args)
            end
        end


        if needUpdateBuildingEffect then
            local pos = targetBuildingPosition
            pos.y = targetBuildingAdjustMapHeight
            local rot = targetBuildingRotation
            local xScale = buildingData and buildingData.range.width or 1
            local zScale = buildingData and buildingData.range.depth or 1
            local effect = isPreview and self.m_hoverInteractHighlightEffect or self.m_buildingInteractHighlightEffect
            local buildingEffectOffsetY = FacConst.DEFAULT_BUILDING_SELECT_EFFECT_OFFSET
            if isBuilding and FacConst.BUILDING_SELECT_EFFECT_OFFSET[buildingData.type] ~= nil then
                buildingEffectOffsetY = FacConst.BUILDING_SELECT_EFFECT_OFFSET[buildingData.type]
            end
            self:_SetEffect(effect, pos, buildingEffectOffsetY, rot, Vector3(xScale, 1, zScale))

            if not isPreview then
                local useCone = FacConst.BUILDING_SELECT_EFFECT_USE_CONE_NODE_IDS[targetBuildingTemplateId]
                effect.normalNode.gameObject:SetActive(not useCone)
                effect.coneNode.gameObject:SetActive(useCone)
                if useCone then
                    local ray = CS.UnityEngine.Ray(pos, Vector3.down)
                    local succ, terrainPos = CSFactoryUtil.SampleLevelRegionPointWithRay(ray)
                    local dist = succ and (pos.y - terrainPos.y) or 3
                    effect.dot.localPosition = Vector3(0, -dist, 0)
                    effect.line.localScale = Vector3(1, dist + effect.line.localPosition.y, 1)
                end
                CSFactoryUtil.DispatchFactoryBuildingApproachSelectedChanged(self.m_interactFacNodeId, nodeId)
            end
            if not isPreview and CSFactoryUtil.hideBuildingInteractHighlightEffect then
                self.m_buildingInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
            end
        end

        self.m_interactFacNodeId = nodeId
        self.m_buildingUseDefaultOption = useDefaultOption
        self.m_interactSubBuildingIndex = newSubIndex
    else
        if self.m_interactFacNodeId or forceUpdate then
            if not isClickMode then
                Notify(MessageConst.REMOVE_INTERACT_OPTION, {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = "MainBuilding",
                })
                if self.m_interactFacNodeId then
                    GameWorld.interactiveFacWrapperManager:OnFacBuildingInteractOptionRemove(self.m_interactFacNodeId)

                    if FactoryUtils.isOthersSocialBuilding(self.m_interactFacNodeId) then
                        GameInstance.player.generalAbilitySystem:DeactivateTempAbility(GeneralAbilityType.BuildingLike)
                    end
                end

                Notify(MessageConst.REMOVE_INTERACT_OPTION, {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = "SubBuilding",
                })
            end

            if not isPreview then
                self.m_buildingInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
                self.m_subBuildingInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
                CSFactoryUtil.DispatchFactoryBuildingApproachSelectedChanged(self.m_interactFacNodeId, nil)
            end
            self.m_interactFacNodeId = nil
            self.m_buildingUseDefaultOption = nil
            self.m_interactSubBuildingIndex = -1
        end
    end


    local needUpdateHoverHint


    local pipeNodeId, pipeGridUnit, pipeGridPos
    if isClickMode then
        if LuaSystemManager.factory:GetSimpleFigureModeFromRenderer() ~= FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure then
            pipeGridPos = GameInstance.remoteFactoryManager.visual:WorldToBeltGrid(playerPos)
            pipeGridUnit = self:_GetGridUnitFromWorldPos(pipeGridPos, FacConst.FAC_SAMPLE_TYPE.Pipe)
            pipeGridPos = Unity.Vector2Int(lume.round(pipeGridPos.x), lume.round(pipeGridPos.y))
            if pipeGridUnit.success then
                pipeNodeId = pipeGridUnit.nodeId
            end
        else
            pipeGridUnit = { success = false }
        end
    else
        local success, nodeId, unitIndex
        if useAsyncRst then

            local valid, info = CSFactoryUtil.GetShouldInteractLogisticUsingAsyncRst(false)
            success = valid and info.valid and info.nodeId > 0
            if success then
                nodeId = info.nodeId
                unitIndex = info.uintIndex
                pipeGridPos = Unity.Vector2Int(math.floor(info.position.x), math.floor(info.position.z))
                pipeChapterId = info.chapterId
            end
        else
            success, pipeGridPos, nodeId, unitIndex = CSFactoryUtil.GetShouldInteractLogistic(false, LuaSystemManager.factory.inDestroyMode, true)
        end

        if success then
            local shouldShow, shouldUpdateInfo = false, true
            if LuaSystemManager.factory.inDestroyMode then
                shouldShow = true
            else

                if self.m_delayedPipeNodeInfo then
                    if self.m_delayedPipeNodeInfo.nodeId == nodeId and self.m_delayedPipeNodeInfo.unitIndex == unitIndex then
                        shouldUpdateInfo = false
                        shouldShow = Time.unscaledTime >= self.m_delayedPipeNodeInfo.delayEndTime
                    end
                end
            end
            if shouldShow then
                pipeGridUnit = {
                    success = success,
                    nodeId = nodeId,
                    unitIndex = unitIndex,
                }
                pipeNodeId = nodeId
            elseif shouldUpdateInfo then
                self.m_delayedPipeNodeInfo = {
                    nodeId = nodeId,
                    unitIndex = unitIndex,
                    delayEndTime = Time.unscaledTime + self.view.config.PIPE_OPTION_DELAY
                }
            end
        else
            self.m_delayedPipeNodeInfo = nil
        end
    end

    if pipeNodeId then
        hasTarget = true
    end
    local pipePendingSlotId = pipeNodeId and CSFactoryUtil.GetBlueprintSlotId(chapterId, pipeNodeId) or 0
    local isPendingPipe = pipePendingSlotId > 0
    if isPendingPipe and (pipePendingSlotId == buildingPendingSlotId or (LuaSystemManager.factory.inDestroyMode and not isClickMode)) then

        pipeNodeId = nil
        pipePendingSlotId = 0
    end

    if forceUpdate or pipeNodeId ~= self.m_interactPipeNodeId or (pipeNodeId and isPreview) or
            (pipeGridUnit and pipeGridUnit.unitIndex ~= self.m_interactPipeUnitIndex) then
        if pipeNodeId then

            local height = CSFactoryUtil.GetPipeUnitHeight(pipeNodeId, pipeGridUnit.unitIndex)
            local worldPos = Vector3(pipeGridPos.x + 0.5, height, pipeGridPos.y + 0.5)
            if isPreview then
                self:_SetEffect(self.m_hoverInteractHighlightEffect, worldPos, 0)
            else
                self.m_pipeInteractHighlightEffect.transform.position = worldPos
                local ray = CS.UnityEngine.Ray(worldPos, Vector3.down)
                local succ, terrainPos = CSFactoryUtil.SampleLevelRegionPointWithRay(ray)
                local dist = succ and (worldPos.y - terrainPos.y) or 3
                self.m_pipeInteractHighlightEffect.dot.localPosition = Vector3(0, -dist, 0)
                self.m_pipeInteractHighlightEffect.line.localScale = Vector3(1, dist + self.m_pipeInteractHighlightEffect.line.localPosition.y, 1)
                self.m_pipeInteractHighlightEffect.gameObject:SetActive(true)
            end

            self:_SetHighlightedPipeNode(pipeNodeId, pipeGridUnit.unitIndex)

            if not isClickMode then

                local isInvalidBuilding = FactoryUtils.isInvalidBuilding(pipeNodeId)
                local interactArgs = {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = INTERACT_SOURCE_ID_PIPE,
                    templateId = pipeGridUnit.unitTemplateId,
                    isDel = LuaSystemManager.factory.inDestroyMode or isInvalidBuilding,

                    text = isPendingPipe and FactoryUtils.getPendingSlotName(pipePendingSlotId) or Language.LUA_FAC_PIPE_INTERACT_OPTION,
                    icon = LuaSystemManager.factory.inDestroyMode and INTERACT_ICON_DELETE or INTERACT_ICON_COMMON,

                    action = function()
                        if isInvalidBuilding then
                            FactoryUtils.delBuilding(pipeNodeId)
                        elseif LuaSystemManager.factory.inDestroyMode then
                            self:_OnInteractFactory({ nodeId = pipeGridUnit.nodeId, unitIndex = pipeGridUnit.unitIndex })
                        else
                            self:_OnInteractFactory({ nodeId = pipeGridUnit.nodeId, unitIndex = pipeGridUnit.unitIndex })
                        end
                    end,
                    sortId = 500,
                }
                if isInvalidBuilding then
                    interactArgs.text = string.format(Language.LUA_FACTORY_INVALID_BUILDING_INTERACT, interactArgs.text)
                end
                if useAsyncRst then
                    interactArgs.text = self:_AppendDebugSuffix(interactArgs.text, pipeChapterId, pipeNodeId)
                end
                Notify(MessageConst.ADD_INTERACT_OPTION, interactArgs)

                if LuaSystemManager.factory.inDestroyMode then
                    Notify(MessageConst.ADD_INTERACT_OPTION, {
                        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                        sourceId = INTERACT_SOURCE_ID_DELETE_ALL_PIPE,
                        templateId = pipeGridUnit.unitTemplateId,
                        isDel = true,

                        text = string.format(Language.DEL_ALL_BELT_FORMAT, Language.LUA_FAC_PIPE_INTERACT_OPTION),
                        icon = INTERACT_ICON_DELETE_ALL,

                        action = function()
                            self:_OnInteractFactory({ nodeId = pipeGridUnit.nodeId, isAll = true })
                        end,
                        sortId = 499,
                    })
                else
                    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
                        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                        sourceId = INTERACT_SOURCE_ID_DELETE_ALL_PIPE,
                    })
                end
            end
        else

            if not isClickMode then

                Notify(MessageConst.REMOVE_INTERACT_OPTION, {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = INTERACT_SOURCE_ID_PIPE,
                })
                Notify(MessageConst.REMOVE_INTERACT_OPTION, {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = INTERACT_SOURCE_ID_DELETE_ALL_PIPE,
                })
            end
            if not isPreview then
                self.m_pipeInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
            end

            self:_SetHighlightedPipeNode()
        end
        self.m_interactPipeNodeId = pipeNodeId
        self.m_interactPipeUnitIndex = pipeGridUnit and pipeGridUnit.unitIndex
        needUpdateHoverHint = true
        if not pipeNodeId and isClickMode and isPreview and not forceUpdate then


            self.m_interactFacNodeId = nil
            self.m_interactFacOverlapNodeIds = nil
        end
    end



    local logisticPos
    local beltGridUnit
    if isClickMode then
        if LuaSystemManager.factory:GetSimpleFigureModeFromRenderer() ~= FacConst.SIMPLE_FIGURE_MODE.SimpleBeltFigure then
            local beltPos = GameInstance.remoteFactoryManager.visual:WorldToBeltGrid(playerPos)
            beltGridUnit = self:_GetGridUnitFromWorldPos(beltPos, FacConst.FAC_SAMPLE_TYPE.Belt)
            if beltGridUnit.success then
                logisticPos = Unity.Vector2Int(lume.round(beltPos.x), lume.round(beltPos.y))
            end
        else
            beltGridUnit = { success = false }
        end
    else
        local success, nodeId, unitIndex
        if useAsyncRst then

            local valid, info = CSFactoryUtil.GetShouldInteractLogisticUsingAsyncRst(true)
            success = valid and info.valid and info.nodeId > 0
            if success then
                nodeId = info.nodeId
                unitIndex = info.uintIndex
                logisticPos = Unity.Vector2Int(math.floor(info.position.x), math.floor(info.position.z))
                beltChapterId = info.chapterId
            end
        else
            success, logisticPos, nodeId, unitIndex = CSFactoryUtil.GetShouldInteractLogistic(true, LuaSystemManager.factory.inDestroyMode, true)
        end
        beltGridUnit = {
            success = success,
            nodeId = nodeId,
            unitIndex = unitIndex,
        }
    end

    local beltPendingSlotId = beltGridUnit.success and CSFactoryUtil.GetBlueprintSlotId(chapterId, beltGridUnit.nodeId) or 0
    local isPendingBelt = beltPendingSlotId > 0
    if isPendingBelt and (beltPendingSlotId == buildingPendingSlotId or beltPendingSlotId == pipePendingSlotId or (LuaSystemManager.factory.inDestroyMode and not isClickMode)) then

        beltGridUnit.success = false
        beltPendingSlotId = 0
    end

    if beltGridUnit.success then
        hasTarget = true

        local logisticName
        if isPendingBelt then
            logisticName = FactoryUtils.getPendingSlotName(beltPendingSlotId)
        else
            local chapterInfo = FactoryUtils.getCurChapterInfo()
            local nodeHandler = chapterInfo:GetNode(beltGridUnit.nodeId)
            if nodeHandler == nil then
                logger.error("can not find node id = ", beltGridUnit.nodeId)
            end
            local _, logisticData = Tables.factoryGridBeltTable:TryGetValue(nodeHandler.templateId)
            if not logisticData then
                logger.error("No factoryGridBeltData", nodeHandler.templateId, beltGridUnit, "logisticPos", logisticPos, "playerPos", playerPos)
            else
                logisticName = logisticData.beltData.name
            end
        end

        local lastPos = self.m_interactLogisticPos
        if forceUpdate or not lastPos or (lastPos.x ~= logisticPos.x) or (lastPos.y ~= logisticPos.y) then
            self.m_interactLogisticPos = logisticPos
            needUpdateHoverHint = true
            if self:_NeedShowHoverHint() then
                if lastPos then
                    self:_ToggleBeltHoverHint(lastPos, false)
                end
                self:_ToggleBeltHoverHint(logisticPos, true)
            end

            local unitHeight = FactoryUtils.queryVoxelRangeHeightAdjust(logisticPos.x, CSFactoryUtil.GetBeltHeight(beltGridUnit.nodeId), logisticPos.y)
            local worldPos = Vector3(logisticPos.x + 0.5, unitHeight, logisticPos.y + 0.5)
            local effect = isPreview and self.m_hoverInteractHighlightEffect or self.m_logisticInteractHighlightEffect
            self:_SetEffect(effect, worldPos, 0.2)

            if not isClickMode then
                local args = {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = INTERACT_SOURCE_ID_BELT,
                    action = function()
                        self:_OnInteractFactory({ nodeId = beltGridUnit.nodeId, unitIndex = beltGridUnit.unitIndex, logisticPos = logisticPos, })
                    end,
                    icon = LuaSystemManager.factory.inDestroyMode and INTERACT_ICON_DELETE or INTERACT_ICON_COMMON,
                }
                local delAllArgs
                if LuaSystemManager.factory.inDestroyMode then
                    args.isDel = true
                    args.sortId = -100
                    args.text = logisticName
                    delAllArgs = {
                        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                        sourceId = INTERACT_SOURCE_ID_DELETE_ALL_BELT,
                        isDel = true,
                        text = string.format(Language.DEL_ALL_BELT_FORMAT, logisticName),
                        icon = INTERACT_ICON_DELETE_ALL,
                        action = function()
                            self:_OnInteractFactory({ nodeId = beltGridUnit.nodeId, unitIndex = beltGridUnit.unitIndex, isAll = true })
                        end,
                        sortId = -200,
                    }
                else
                    args.sortId = 300
                    args.text = logisticName
                    if useAsyncRst then
                        args.text = self:_AppendDebugSuffix(args.text, beltChapterId, beltGridUnit.nodeId)
                    end
                end
                if delAllArgs then
                    Notify(MessageConst.ADD_INTERACT_OPTION, delAllArgs)
                else
                    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
                        type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                        sourceId = INTERACT_SOURCE_ID_DELETE_ALL_BELT,
                    })
                end
                Notify(MessageConst.ADD_INTERACT_OPTION, args)
            end
        end
    else
        if forceUpdate or self.m_interactLogisticPos then

            if not isClickMode then
                Notify(MessageConst.REMOVE_INTERACT_OPTION, {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = INTERACT_SOURCE_ID_BELT,
                })
                Notify(MessageConst.REMOVE_INTERACT_OPTION, {
                    type = CS.Beyond.Gameplay.Core.InteractOptionType.Factory,
                    sourceId = INTERACT_SOURCE_ID_DELETE_ALL_BELT,
                })
            end

            if self:_NeedShowHoverHint() and self.m_interactLogisticPos then
                self:_ClearBeltHoverHint()
            end

            self.m_interactLogisticPos = nil
            needUpdateHoverHint = true
            if not isPreview then
                self.m_logisticInteractHighlightEffect.gameObject:SetActiveIfNecessary(false)
            end
        end
    end











    if isClickMode then
        if DeviceInfo.usingController then

            local curGridPos = GameInstance.remoteFactoryManager.visual:WorldToVoxel(playerPos)
            curGridPos = Vector3(math.floor(curGridPos.x) + 0.5, curGridPos.y, math.floor(curGridPos.z) + 0.5)
            if isPreview and not hasTarget then

                self.m_hoverInteractHighlightEffect.gameObject:SetActive(true)
                local worldPos = GameInstance.remoteFactoryManager.visual:VoxelToWorld(curGridPos)
                if self.m_hoverInteractHighlightEffect.transform.position:XZ() ~= worldPos:XZ() or self.m_hoverInteractHighlightEffect.transform.localScale ~= Vector3.one then
                    self:_SetEffect(self.m_hoverInteractHighlightEffect, worldPos, 0.2)
                end
            end
            if curGridPos ~= self.m_lastControllerHoverGridPos then
                self.m_lastControllerHoverGridPos = curGridPos
                AudioAdapter.PostEvent("au_ui_fac_checkerboard_controller")
            end
        else

            if not hasTarget then
                self.m_hoverInteractHighlightEffect.gameObject:SetActive(false)
            end
        end
    end

    self:_UpdatePendingSlotHighlight(buildingPendingSlotId, pipePendingSlotId, beltPendingSlotId)
end

FacBuildingInteractCtrl.OnActiveBuildingLike = HL.Method(HL.Any) << function(self, args)
    local nodeId = unpack(args)
    FactoryUtils.likeSocialBuilding(nodeId, function()
        if self.m_interactFacNodeId ~= nodeId then
            return
        end
        FactoryUtils.updateBuildingLikeAbilityState(nodeId)
    end)
end


FacBuildingInteractCtrl.m_lastControllerHoverGridPos = HL.Field(HL.Any)

FacBuildingInteractCtrl.m_curHighlightedPipeNodeId = HL.Field(HL.Any)

FacBuildingInteractCtrl.m_curHighlightedPipeUnitIndex = HL.Field(HL.Any)

FacBuildingInteractCtrl._SetHighlightedPipeNode = HL.Method(HL.Opt(HL.Number, HL.Number)) << function(self, pipeNodeId, unitIndex)
    if self.m_curHighlightedPipeNodeId == pipeNodeId and self.m_curHighlightedPipeUnitIndex == unitIndex then
        return
    end
    if pipeNodeId and not (DeviceInfo.usingTouch and LuaSystemManager.factory.inTopView) then
        LuaSystemManager.factory:RequestPipeHighlight("hover", pipeNodeId, unitIndex)
    else
        LuaSystemManager.factory:ReleasePipeHighlight("hover")
    end
    self.m_curHighlightedPipeNodeId = pipeNodeId
    self.m_curHighlightedPipeUnitIndex = unitIndex
end


FacBuildingInteractCtrl.m_curHighlightedSlotIds = HL.Field(HL.Table)

FacBuildingInteractCtrl._UpdatePendingSlotHighlight = HL.Method(HL.Number, HL.Number, HL.Number) << function(self, buildingSlotId, pipeSlotId, beltSlotId)
    local newIds = {}
    newIds[buildingSlotId] = true
    newIds[pipeSlotId] = true
    newIds[beltSlotId] = true
    newIds[0] = nil
    local chapterId = Utils.getCurrentChapterId()
    for id, _ in pairs(self.m_curHighlightedSlotIds) do
        if not newIds[id] then

            CSFactoryUtil.SetPendingSlotSelected(chapterId, id, false)
        end
    end
    for id, _ in pairs(newIds) do
        if not self.m_curHighlightedSlotIds[id] then

            CSFactoryUtil.SetPendingSlotSelected(chapterId, id, true)
        end
    end
    self.m_curHighlightedSlotIds = newIds
end






FacBuildingInteractCtrl.m_selectedInteractFacNodeId = HL.Field(HL.Any)

FacBuildingInteractCtrl.m_selectedInteractFacNodeIdIsBuilding = HL.Field(HL.Boolean) << false

FacBuildingInteractCtrl.m_selectedInteractSubBuildingIndex = HL.Field(HL.Number) << -1

FacBuildingInteractCtrl.m_selectedInteractLogisticPos = HL.Field(CS.UnityEngine.Vector2Int)

FacBuildingInteractCtrl.m_selectedInteractPipeNodeId = HL.Field(HL.Any)


FacBuildingInteractCtrl._InitFakeInteractOption = HL.Method() << function(self)
    self.view.optionItem.button.onClick:AddListener(function()
        self:_OnClickFakeInteractOption()
    end)
    self.view.listNode.gameObject:SetActive(false)
end

FacBuildingInteractCtrl._UpdateFakeInteractOption = HL.Method() << function(self)
    if self.m_selectedInteractFacNodeId == self.m_interactFacNodeId
            and self.m_selectedInteractSubBuildingIndex == self.m_interactSubBuildingIndex
            and self.m_selectedInteractLogisticPos == self.m_interactLogisticPos
            and (self.m_selectedInteractPipeNodeId == self.m_interactPipeNodeId and not self.m_interactPipeNodeId) then
        return
    end

    self.m_selectedInteractFacNodeId = self.m_interactFacNodeId
    self.m_selectedInteractSubBuildingIndex = self.m_interactSubBuildingIndex
    self.m_selectedInteractLogisticPos = self.m_interactLogisticPos
    self.m_selectedInteractPipeNodeId = self.m_interactPipeNodeId
    self.m_selectedInteractFacNodeIdIsBuilding = self.m_interactFacNodeIdIsBuilding










































































end

FacBuildingInteractCtrl._OnClickFakeInteractOption = HL.Method(HL.Opt(HL.Boolean)) << function(self, isAll)
    if self.m_selectedInteractPipeNodeId then
        self:_OnInteractFactory({
            nodeId = self.m_selectedInteractPipeNodeId,
            unitIndex = self.m_interactPipeUnitIndex,
        })
    elseif self.m_selectedInteractFacNodeId then
        if self.m_selectedInteractFacNodeIdIsBuilding then
            if self.m_selectedInteractSubBuildingIndex >= 0 then
                self:_OnInteractFactory({
                    buildingNodeId = self.m_selectedInteractFacNodeId,
                    subBuildingIndex = self.m_selectedInteractSubBuildingIndex,
                })
            else
                self:_OnInteractFactory({ buildingNodeId = self.m_selectedInteractFacNodeId })
            end
        else
            self:_OnInteractFactory({ nodeId = self.m_selectedInteractFacNodeId })
        end
    elseif self.m_selectedInteractLogisticPos then
        local _, nodeId, unitIndex = GameInstance.remoteFactoryManager:TrySampleConveyor(self.m_selectedInteractLogisticPos)
        self:_OnInteractFactory({
            nodeId = nodeId,
            unitIndex = unitIndex,
            logisticPos = self.m_selectedInteractLogisticPos,
            isAll = isAll,
        })
    end
    self:_RemoveInteractOption()
end

FacBuildingInteractCtrl._CollectAllInteractTargets = HL.Method().Return(HL.Table) << function(self)
    local targets = {}
    local seenPendingSlotIds = {}

    local detectPos = self.m_lastDetectWorldPos
    local chapterId = Utils.getCurrentChapterId()

    if self.m_selectedInteractPipeNodeId then
        local pipeNodeId = self.m_selectedInteractPipeNodeId
        local pipeUnitIndex = self.m_interactPipeUnitIndex
        local pipeGridPos = GameInstance.remoteFactoryManager.visual:WorldToBeltGrid(detectPos)
        local height = CSFactoryUtil.GetPipeUnitHeight(pipeNodeId, pipeUnitIndex)
        local pipePendingSlotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, pipeNodeId)
        local isPendingPipe = pipePendingSlotId > 0
        if not isPendingPipe or not seenPendingSlotIds[pipePendingSlotId] then
            if isPendingPipe then
                seenPendingSlotIds[pipePendingSlotId] = true
            end
            table.insert(targets, {
                type = "pipe",
                name = isPendingPipe and FactoryUtils.getPendingSlotName(pipePendingSlotId) or Language.LUA_FAC_PIPE_INTERACT_OPTION,
                effectInfo = {
                    nodeId = pipeNodeId,
                    unitIndex = pipeUnitIndex,
                    worldPos = Vector3(lume.round(pipeGridPos.x) + 0.5, height, lume.round(pipeGridPos.y) + 0.5),
                },
                action = function()
                    self:_OnInteractFactory({
                        nodeId = pipeNodeId,
                        unitIndex = pipeUnitIndex,
                    })
                    self:_RemoveInteractOption()
                end,
            })
        end
    end




    if self.m_interactFacOverlapNodeIds then
        for _, overlapNodeId in ipairs(self.m_interactFacOverlapNodeIds) do
            local overlapPendingSlotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, overlapNodeId)
            local isOverlapPending = overlapPendingSlotId > 0
            if isOverlapPending then
                if not seenPendingSlotIds[overlapPendingSlotId] then
                    seenPendingSlotIds[overlapPendingSlotId] = true
                    local capturedNodeId = overlapNodeId
                    table.insert(targets, {
                        type = "building",
                        name = FactoryUtils.getPendingSlotName(overlapPendingSlotId),
                        effectInfo = {
                            worldPos = detectPos,
                        },
                        action = function()
                            self:_OnInteractFactory({ buildingNodeId = capturedNodeId })
                            self:_RemoveInteractOption()
                        end,
                    })
                end
            else
                local handler = FactoryUtils.getBuildingNodeHandler(overlapNodeId)
                if handler then
                    local _, buildingData = Tables.factoryBuildingTable:TryGetValue(handler.templateId)
                    local isBuilding = buildingData ~= nil
                    local targetName
                    if isBuilding then
                        targetName = buildingData.name
                    else
                        local unitData = FactoryUtils.getLogisticData(handler.templateId)
                        targetName = unitData and unitData.name
                    end
                    if targetName then
                        local effectWorldPos = CSFactoryUtil.GetBuildingModelPosition(handler)
                        local capturedNodeId = overlapNodeId
                        local capturedIsBuilding = isBuilding
                        table.insert(targets, {
                            type = "building",
                            name = targetName,
                            effectInfo = {
                                worldPos = effectWorldPos,
                            },
                            action = function()
                                if capturedIsBuilding then
                                    self:_OnInteractFactory({ buildingNodeId = capturedNodeId })
                                else
                                    self:_OnInteractFactory({ nodeId = capturedNodeId })
                                end
                                self:_RemoveInteractOption()
                            end,
                        })
                    end
                end
            end
        end
    end

    if self.m_selectedInteractFacNodeId then
        local nodeId = self.m_selectedInteractFacNodeId
        local buildingPendingSlotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, nodeId)
        local isPendingBuilding = buildingPendingSlotId > 0

        if isPendingBuilding then

            if not seenPendingSlotIds[buildingPendingSlotId] then
                seenPendingSlotIds[buildingPendingSlotId] = true
                table.insert(targets, {
                    type = "building",
                    name = FactoryUtils.getPendingSlotName(buildingPendingSlotId),
                    effectInfo = {
                        worldPos = detectPos,
                    },
                    action = function()
                        self:_OnInteractFactory({ buildingNodeId = nodeId })
                        self:_RemoveInteractOption()
                    end,
                })
            end
        else

            local isBuilding = self.m_selectedInteractFacNodeIdIsBuilding
            local subIndex = self.m_selectedInteractSubBuildingIndex
            local handler = FactoryUtils.getBuildingNodeHandler(nodeId)
            local effectWorldPos = CSFactoryUtil.GetBuildingModelPosition(handler)
            local targetName
            if isBuilding then
                if subIndex >= 0 then
                    targetName = Language.LUA_FAC_HUB_INPUT .. subIndex
                else
                    local buildingData = Tables.factoryBuildingTable[handler.templateId]
                    targetName = buildingData.name
                end
            else
                local unitData = FactoryUtils.getLogisticData(handler.templateId)
                targetName = unitData.name
            end
            table.insert(targets, {
                type = "building",
                name = targetName,
                effectInfo = {
                    worldPos = effectWorldPos,
                },
                action = function()
                    if isBuilding then
                        if subIndex >= 0 then
                            self:_OnInteractFactory({
                                buildingNodeId = nodeId,
                                subBuildingIndex = subIndex,
                            })
                        else
                            self:_OnInteractFactory({ buildingNodeId = nodeId })
                        end
                    else
                        self:_OnInteractFactory({ nodeId = nodeId })
                    end
                    self:_RemoveInteractOption()
                end,
            })
        end
    end

    if self.m_selectedInteractLogisticPos then
        local logisticPos = self.m_selectedInteractLogisticPos
        local chapterInfo = FactoryUtils.getCurChapterInfo()
        local succ, beltNodeId, beltUnitIndex = GameInstance.remoteFactoryManager:TrySampleConveyor(logisticPos)
        if succ and beltNodeId then
            local beltPendingSlotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, beltNodeId)
            local isPendingBelt = beltPendingSlotId > 0
            if not isPendingBelt or not seenPendingSlotIds[beltPendingSlotId] then
                if isPendingBelt then
                    seenPendingSlotIds[beltPendingSlotId] = true
                end
                local beltName
                if isPendingBelt then
                    beltName = FactoryUtils.getPendingSlotName(beltPendingSlotId)
                else
                    local handler = chapterInfo:GetNode(beltNodeId)
                    beltName = Tables.factoryGridBeltTable:GetValue(handler.templateId).beltData.name
                end
                local unitHeight = FactoryUtils.queryVoxelRangeHeightAdjust(logisticPos.x, CSFactoryUtil.GetBeltHeight(beltNodeId), logisticPos.y)
                table.insert(targets, {
                    type = "belt",
                    name = beltName,
                    effectInfo = {
                        worldPos = Vector3(logisticPos.x + 0.5, unitHeight, logisticPos.y + 0.5),
                    },
                    action = function()
                        self:_OnInteractFactory({
                            nodeId = beltNodeId,
                            unitIndex = beltUnitIndex,
                            logisticPos = logisticPos,
                        })
                        self:_RemoveInteractOption()
                    end,
                })
            end
        end
    end

    return targets
end

FacBuildingInteractCtrl._CalcInteractScreenRect = HL.Method().Return(HL.Userdata) << function(self)
    local worldPoints
    if DeviceInfo.usingTouch and LuaSystemManager.factory.inTopView and self.m_lastDetectWorldPos then
        local wp = self.m_lastDetectWorldPos
        local cx = math.floor(wp.x) + 0.5
        local cz = math.floor(wp.z) + 0.5
        worldPoints = {
            Vector3(cx - 0.5, wp.y, cz - 0.5),
            Vector3(cx + 0.5, wp.y, cz - 0.5),
            Vector3(cx - 0.5, wp.y, cz + 0.5),
            Vector3(cx + 0.5, wp.y, cz + 0.5),
        }
    else
        local effect = self.m_hoverInteractHighlightEffect
        worldPoints = {
            effect.corner1.transform.position,
            effect.corner2.transform.position,
            effect.corner3.transform.position,
            effect.corner4.transform.position,
        }
    end
    local cam = CameraManager.mainCamera
    local sp = cam:WorldToScreenPoint(worldPoints[1])
    local minX, minY, maxX, maxY = sp.x, sp.y, sp.x, sp.y
    for i = 2, #worldPoints do
        sp = cam:WorldToScreenPoint(worldPoints[i])
        minX = math.min(minX, sp.x)
        minY = math.min(minY, sp.y)
        maxX = math.max(maxX, sp.x)
        maxY = math.max(maxY, sp.y)
    end
    return Unity.Rect(minX, Screen.height - maxY, maxX - minX, maxY - minY)
end

FacBuildingInteractCtrl.OnFacTopViewCamTargetMoved = HL.Method() << function(self)
    self:_RemoveTouchInteractOption()
    Notify(MessageConst.FAC_TOPVIEW_HIDE_MULTI_TARGET_MENU)
    if self.m_dragTargetsInBatchModeUpdateKey > 0 then
        self.m_needUpdateDragTargetsInBatchMode = true
    end
end

FacBuildingInteractCtrl.OnFacTopViewCamZoom = HL.Method(HL.Number) << function(self, _)
    self:_RemoveTouchInteractOption()
    Notify(MessageConst.FAC_TOPVIEW_HIDE_MULTI_TARGET_MENU)
end

FacBuildingInteractCtrl._RemoveTouchInteractOption = HL.Method() << function(self)
    if self.m_selectedInteractFacNodeId or self.m_selectedInteractLogisticPos or self.m_selectedInteractPipeNodeId then
        self:_RemoveInteractOption()
    end
end







FacBuildingInteractCtrl.m_oldBatchSelectedTargetIds = HL.Field(HL.Table)

FacBuildingInteractCtrl._ResetBatch = HL.Method(HL.Boolean) << function(self, inDestroyMode)
    self.view.batchSelectFrame.gameObject:SetActive(false)
    self.view.batchToggle.isOn = inDestroyMode and LuaSystemManager.factory.inTopView and DeviceInfo.usingKeyboard
    LuaSystemManager.factory:ChangeIsReverseSelect(false)
end

FacBuildingInteractCtrl._ChangeBatchMode = HL.Method(HL.Boolean) << function(self, isBatch)
    LuaSystemManager.factory.inBatchSelectMode = isBatch
    self:_ClearAllBatchTargets()
    Notify(MessageConst.FAC_ON_TOGGLE_BATCH_MODE, isBatch)

    Notify(MessageConst.CHANGE_MOUSE_ICON_HINT, {
        name = "FacBuildingInteract-BatchMode",
        type = isBatch and UIConst.MOUSE_ICON_HINT.Frame or UIConst.MOUSE_ICON_HINT.Default,
    })

    self:_ClearBeltHoverHint()

    if isBatch then
        GameInstance.remoteFactoryManager.batchSelect:EnterBatchSelect()
    else
        GameInstance.remoteFactoryManager.batchSelect:ExitBatchSelect()
    end
end

FacBuildingInteractCtrl._OnChangeDragBatchToggle = HL.Method(HL.Boolean) << function(self, isBatch)
    LuaSystemManager.factory.inDragSelectBatchMode = isBatch
    if isBatch then
        if DeviceInfo.usingTouch then

            UIManager.commonTouchPanel.enableZoom = false
        end
    else
        if DeviceInfo.usingTouch then
            UIManager.commonTouchPanel.enableZoom = true
        end
        self:_OnDragEndInBatchMode()
    end
end

FacBuildingInteractCtrl._ClearAllBatchTargets = HL.Method() << function(self)
    local curChapterId = Utils.getCurrentChapterId()
    for nodeId, _ in pairs(LuaSystemManager.factory.batchSelectTargets) do
        GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.MOVING, false)
        if FactoryUtils.isPendingBuildingNode(nodeId) then
            GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.TO_BE_BUILD, true)
        end
    end
    LuaSystemManager.factory.batchSelectTargets = {}
    GameInstance.remoteFactoryManager.batchSelect:ClearTargets()
end


FacBuildingInteractCtrl.m_dragStartWorldPos = HL.Field(HL.Any)

FacBuildingInteractCtrl.m_dragTargetsInBatchModeUpdateKey = HL.Field(HL.Number) << -1

FacBuildingInteractCtrl.m_needUpdateDragTargetsInBatchMode = HL.Field(HL.Boolean) << false

FacBuildingInteractCtrl._OnDragBeginInBatchMode = HL.Method(Vector2) << function(self, pos)
    if DeviceInfo.usingKeyboard then

        LuaSystemManager.factory:ChangeIsReverseSelect(InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1))
    end
    self.m_oldBatchSelectedTargetIds = {}
    for id, info in pairs(LuaSystemManager.factory.batchSelectTargets) do
        self.m_oldBatchSelectedTargetIds[id] = lume.copy(info)
    end
    local _
    _, self.m_dragStartWorldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(CameraManager.mainCamera:ScreenPointToRay(pos:XY()))
    LuaSystemManager.factory:ToggleAutoMoveTopViewCam(Vector2.one)
    Notify(MessageConst.FAC_ON_DRAG_BEGIN_IN_BATCH_MODE)

    self.view.batchSelectFrame.gameObject:SetActive(true)
    self.view.batchSelectFrame:SetState(LuaSystemManager.factory.isReverseBatchSelect and "Reverse" or "Normal")

    self.m_needUpdateDragTargetsInBatchMode = false
    self.m_dragTargetsInBatchModeUpdateKey = LuaUpdate:Remove(self.m_dragTargetsInBatchModeUpdateKey)
    self.m_dragTargetsInBatchModeUpdateKey = LuaUpdate:Add("TailTick", function()
        if self.m_needUpdateDragTargetsInBatchMode then
            self.m_needUpdateDragTargetsInBatchMode = false
            self:_UpdateDragTargetsInBatchMode()
        end
    end)

    self:_StopPressHint()




    InputManagerInst:ToggleBinding(UIManager.commonTouchPanel.onClick.bindingId, false)
end

FacBuildingInteractCtrl._OnDragEndInBatchMode = HL.Method() << function(self)
    if DeviceInfo.usingKeyboard then
        LuaSystemManager.factory:ChangeIsReverseSelect(false)
    end
    self.view.batchSelectFrame.gameObject:SetActive(false)
    self.m_oldBatchSelectedTargetIds = {}
    self.m_dragStartWorldPos = nil
    LuaSystemManager.factory:ToggleAutoMoveTopViewCam()
    Notify(MessageConst.FAC_ON_DRAG_END_IN_BATCH_MODE)

    self.m_dragTargetsInBatchModeUpdateKey = LuaUpdate:Remove(self.m_dragTargetsInBatchModeUpdateKey)




    InputManagerInst:ToggleBinding(UIManager.commonTouchPanel.onClick.bindingId, true)
end

FacBuildingInteractCtrl.IsDraggingInBatchMode = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_dragTargetsInBatchModeUpdateKey > 0
end

FacBuildingInteractCtrl._OnDragInBatchMode = HL.Method(HL.Opt(CS.UnityEngine.EventSystems.PointerEventData)) << function(self, eventData)
    self.m_needUpdateDragTargetsInBatchMode = true
    if eventData then
        self.m_dragPos = eventData.position
    end
end

FacBuildingInteractCtrl.m_dragPos = HL.Field(Vector2)

FacBuildingInteractCtrl._UpdateDragTargetsInBatchMode = HL.Method() << function(self)
    local startWorldPos = self.m_dragStartWorldPos
    local endMousePos
    if DeviceInfo.usingTouch and self.m_dragPos then
        endMousePos = self.m_dragPos:XY()
        self.m_dragPos = nil
    else
        endMousePos = InputManager.mousePosition
    end
    local _, endWorldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(CameraManager.mainCamera:ScreenPointToRay(endMousePos))



    local posXMin = math.floor(math.min(startWorldPos.x, endWorldPos.x))
    local posZMin = math.floor(math.min(startWorldPos.z, endWorldPos.z))
    local posXMax = math.ceil(math.max(startWorldPos.x, endWorldPos.x))
    local posZMax = math.ceil(math.max(startWorldPos.z, endWorldPos.z))


    local frame = self.view.batchSelectFrame.transform
    local uiPosA = UIUtils.objectPosToUI(Vector3(posXMin, startWorldPos.y, posZMin), self.uiCamera, self.view.transform)
    local uiPosB = UIUtils.objectPosToUI(Vector3(posXMax, startWorldPos.y, posZMax), self.uiCamera, self.view.transform)
    local uiSize = uiPosB - uiPosA
    frame.anchoredPosition = Vector2(math.min(uiPosA.x, uiPosB.x), math.max(uiPosA.y, uiPosB.y))
    frame.sizeDelta = Vector2(math.abs(uiSize.x), math.abs(uiSize.y))

    local startUIPos = UIUtils.objectPosToUI(startWorldPos, self.uiCamera, self.view.transform)
    local endUIPos = UIUtils.objectPosToUI(endWorldPos, self.uiCamera, self.view.transform)
    local iconPosState
    if startUIPos.x <= endUIPos.x then
        iconPosState = startUIPos.y >= endUIPos.y and "LeftTop" or "LeftBottom"
    else
        iconPosState = startUIPos.y >= endUIPos.y and "RightTop" or "RightBottom"
    end
    self.view.batchSelectFrame:SetState(iconPosState)


    local excludeBelt, excludePipe = LuaSystemManager.factory:GetSimpleFigureExcludeBeltPipeForBatchSelect()
    local targetNodeIds, beltInfos = CSFactoryUtil.GetFacEntityInRect(LuaSystemManager.factory.inDestroyMode, startWorldPos, endWorldPos, excludeBelt, excludePipe, true, Utils.isInBlackbox(), true)
    local targetNodeIdTbl = {}
    for _, v in pairs(targetNodeIds) do
        if not FactoryUtils.isInvalidBuilding(v) then
            targetNodeIdTbl[v] = true
        end
    end
    for k, v in pairs(beltInfos) do
        local t = Utils.csList2Table(v)
        if not FactoryUtils.isInvalidBuilding(k) then
            targetNodeIdTbl[k] = t
        end
    end

    if LuaSystemManager.factory.isReverseBatchSelect then



        for nodeId, oldInfo in pairs(self.m_oldBatchSelectedTargetIds) do
            local delInfo = targetNodeIdTbl[nodeId]
            if not delInfo then

                if oldInfo == true then
                    self:_SelectBatchTarget(nodeId, true)
                else
                    for unitIndex, _ in pairs(oldInfo) do
                        self:_SelectBatchTarget(nodeId, true, unitIndex)
                    end
                end
            elseif delInfo ~= true then

                if oldInfo == true then
                    local length = CSFactoryUtil.GetConveyorLength(Utils.getCurrentChapterId(), nodeId)
                    for unitIndex = 0, length -1 do
                        if not delInfo[unitIndex] then
                            self:_SelectBatchTarget(nodeId, true, unitIndex)
                        end
                    end
                else

                    for unitIndex, _ in pairs(oldInfo) do
                        if not delInfo[unitIndex] then
                            self:_SelectBatchTarget(nodeId, true, unitIndex)
                        end
                    end
                end
            end
        end


        for nodeId, info in pairs(targetNodeIdTbl) do
            if info == true then
                self:_SelectBatchTarget(nodeId, false)
            else
                for unitIndex, _ in pairs(info) do
                    self:_SelectBatchTarget(nodeId, false, unitIndex)
                end
            end
        end
    else



        local delInfos = {}
        for nodeId, info in pairs(LuaSystemManager.factory.batchSelectTargets) do
            local info1, info2 = targetNodeIdTbl[nodeId], self.m_oldBatchSelectedTargetIds[nodeId]
            if not info1 and not info2 then
                delInfos[nodeId] = true
            elseif info1 ~= true and info2 ~= true then
                local delInfo = {}
                local length = CSFactoryUtil.GetConveyorLength(Utils.getCurrentChapterId(), nodeId)
                for k = 0, length -1 do
                    if not ((info1 and info1[k]) or (info2 and info2[k])) then
                        delInfo[k] = true
                    end
                end
                delInfos[nodeId] = delInfo
            end
        end
        for nodeId, info in pairs(delInfos) do
            if info == true then
                self:_SelectBatchTarget(nodeId, false)
            else
                for unitIndex, _ in pairs(info) do
                    self:_SelectBatchTarget(nodeId, false, unitIndex)
                end
            end
        end

        for nodeId, info in pairs(targetNodeIdTbl) do
            if info == true then
                self:_SelectBatchTarget(nodeId, true)
            else
                for unitIndex, _ in pairs(info) do
                    self:_SelectBatchTarget(nodeId, true, unitIndex)
                end
            end
        end
    end
end


FacBuildingInteractCtrl.m_isInBlackbox = HL.Field(HL.Boolean) << false

FacBuildingInteractCtrl._SelectBatchTarget = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, nodeId, isAdd, unitIndex)

    if unitIndex then
        GameInstance.remoteFactoryManager.batchSelect:ChangeBatchTarget(nodeId, isAdd, unitIndex)
    else
        GameInstance.remoteFactoryManager.batchSelect:ChangeBatchTarget(nodeId, isAdd)
    end

    local targets = LuaSystemManager.factory.batchSelectTargets
    local curChapterId = Utils.getCurrentChapterId()
    local info = targets[nodeId]
    local isChanged
    if not unitIndex then

        if isAdd then
            if info ~= true then
                local nodeInst = GameInstance.remoteFactoryManager.currentChapterInfo:GetNode(nodeId)
                local placeInst = GameInstance.remoteFactoryManager.currentChapterInfo:GetNodeIncludingPending(nodeId)
                if (nodeInst or (placeInst and placeInst.belongSlot)) then
                    isChanged = true
                    targets[nodeId] = true
                    GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.MOVING, true)
                end
            end
        else
            if info then
                isChanged = true
                targets[nodeId] = nil
                GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.MOVING, false)
                if FactoryUtils.isPendingBuildingNode(nodeId) then

                    GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.TO_BE_BUILD, true)
                end
            end
        end
    else

        if isAdd then
            if info == true then
                return
            elseif not info then
                info = {}
                targets[nodeId] = info
            elseif info[unitIndex] then
                return
            end
            isChanged = true
            info[unitIndex] = true
            GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.MOVING, true, unitIndex)
        else
            if not info then
                return
            elseif info == true then
                isChanged = true
                info = {}
                targets[nodeId] = info
                local length = CSFactoryUtil.GetConveyorLength(curChapterId, nodeId)
                for k = 0, length - 1 do
                    if k ~= unitIndex then
                        info[k] = true

                        GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.MOVING, true, k)
                    else
                        GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.MOVING, false, unitIndex)
                    end
                end
            elseif not info[unitIndex] then
                return
            else
                isChanged = true
                info[unitIndex] = nil
                if not next(info) then
                    targets[nodeId] = nil
                end
                GameInstance.remoteFactoryManager:SetNodeBlueprintEffectType(curChapterId, nodeId, CS.Beyond.Gameplay.Factory.BlueprintState.EffectType.MOVING, false, unitIndex)
            end
        end
    end

    Notify(MessageConst.FAC_ON_TOGGLE_BATCH_TARGET)
    if isChanged then
        if isAdd then
            AudioAdapter.PostEvent("Au_UI_Menu_FacBuildModePanel_Open")
        else
            AudioAdapter.PostEvent("Au_UI_Menu_FacBuildModePanel_Close")
        end
    end
end







FacBuildingInteractCtrl._NeedShowHoverHint = HL.Method().Return(HL.Boolean) << function(self)
    return LuaSystemManager.factory.inTopView and not LuaSystemManager.factory.inBatchSelectMode
end

FacBuildingInteractCtrl._ToggleBeltHoverHint = HL.Method(CS.UnityEngine.Vector2Int, HL.Boolean) << function(self, pos, isActive)










end

FacBuildingInteractCtrl._UpdatePipeHoverHint = HL.Method() << function(self)






end

FacBuildingInteractCtrl._ClearBeltHoverHint = HL.Method() << function(self)
    if self.m_interactLogisticPos then
        self:_ToggleBeltHoverHint(self.m_interactLogisticPos, false)
    end
    self.view.hoverInfoTextNode.gameObject:SetActive(false)
end

FacBuildingInteractCtrl._SlowlyUpdate = HL.Method() << function(self)

end

FacBuildingInteractCtrl._UpdateHoverInfoText = HL.Method() << function(self)
    if not self.view.hoverInfoTextNode.gameObject.activeInHierarchy then
        return
    end

    local manager = GameInstance.remoteFactoryManager
    local name, itemId, _
    if self.m_interactLogisticPos then
        local succ, nodeId, unitIndex = manager:TrySampleConveyor(self.m_interactLogisticPos)
        local chapterInfo = FactoryUtils.getCurChapterInfo()
        local nodeHandler = chapterInfo:GetNode(nodeId)
        name = Tables.factoryGridBeltTable:GetValue(nodeHandler.templateId).beltData.name
        _, itemId = manager:GetItemInLogisticPos(chapterInfo.chapterId, nodeId, self.m_interactLogisticPos)
    elseif self.m_interactPipeNodeId then
        local nodeId = self.m_interactPipeNodeId
        local chapterInfo = FactoryUtils.getCurChapterInfo()
        local nodeHandler = chapterInfo:GetNode(nodeId)
        name = Tables.factoryLiquidPipeTable:GetValue(nodeHandler.templateId).pipeData.name
        _, itemId = manager:GetItemInPipe(chapterInfo.chapterId, nodeId)
    end
    if string.isEmpty(itemId) then
        self.view.hoverInfoTextNode.text.text = string.format(Language.LUA_FAC_DES_HOVER_INFO_NO_ITEM, name)
    else
        local itemData = Tables.itemTable[itemId]
        self.view.hoverInfoTextNode.text.text = string.format(Language.LUA_FAC_DES_HOVER_INFO, name, itemData.name)
    end
end






FacBuildingInteractCtrl.m_batchControllerBindingGroupId = HL.Field(HL.Number) << -1

FacBuildingInteractCtrl.m_batchControllerDragCor = HL.Field(HL.Thread)

FacBuildingInteractCtrl.m_batchControllerDragCorIsReverseDrag = HL.Field(HL.Boolean) << false

FacBuildingInteractCtrl._InitBatchController = HL.Method() << function(self)
    self.m_batchControllerBindingGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)
    self:BindInputPlayerAction("fac_top_view_batch_drag_ct_start", function()
        self:_StartBatchControllerDragCor(false)
    end, self.m_batchControllerBindingGroupId)
    self:BindInputPlayerAction("fac_top_view_batch_drag_ct_end", function()
        if self.m_batchControllerDragCorIsReverseDrag == false then
            self:_StopBatchControllerDragCor()
        end
    end, self.m_batchControllerBindingGroupId)
    self:BindInputPlayerAction("fac_top_view_batch_drag_reverse_ct_start", function()
        self:_StartBatchControllerDragCor(true)
    end, self.m_batchControllerBindingGroupId)
    self:BindInputPlayerAction("fac_top_view_batch_drag_reverse_ct_end", function()
        if self.m_batchControllerDragCorIsReverseDrag == true then
            self:_StopBatchControllerDragCor()
        end
    end, self.m_batchControllerBindingGroupId)
    InputManagerInst:ToggleGroup(self.m_batchControllerBindingGroupId, false)
end

FacBuildingInteractCtrl._StartBatchControllerDragCor = HL.Method(HL.Boolean) << function(self, isReverse)
    if self.m_batchControllerDragCor then
        return
    end
    self.m_batchControllerDragCorIsReverseDrag = isReverse
    LuaSystemManager.factory:ChangeIsReverseSelect(isReverse)
    local startPos = InputManager.mousePosition
    self.m_batchControllerDragCor = self:_StartCoroutine(function()
        local curPos
        while true do
            coroutine.step()
            curPos = InputManager.mousePosition
            if (startPos - curPos).sqrMagnitude >= UIConst.COMMON_UI_DRAG_MIN_SQR_DIST then
                break
            end
        end
        self:_OnDragBeginInBatchMode(startPos:XY())
        self:_OnDragInBatchMode()
        local lastPos = curPos
        while true do
            coroutine.step()
            curPos = InputManager.mousePosition
            if (curPos - lastPos).sqrMagnitude >= UIConst.COMMON_UI_DRAG_MIN_SQR_DIST then
                lastPos = curPos
                self:_OnDragInBatchMode()
            end
        end
    end)
end

FacBuildingInteractCtrl._StopBatchControllerDragCor = HL.Method() << function(self)
    if not self.m_batchControllerDragCor then
        return
    end
    self.m_batchControllerDragCor = self:_ClearCoroutine(self.m_batchControllerDragCor)
    self:_OnDragEndInBatchMode()
    LuaSystemManager.factory:ChangeIsReverseSelect(false)
end




FacBuildingInteractCtrl.m_onlyValidHubUnloaderIndex = HL.Field(HL.Number) << -1

FacBuildingInteractCtrl.FacBlockOtherHubUnloaderInteract = HL.Method(HL.Table) << function(self, arg)
    local targetIndex = unpack(arg)
    self.m_onlyValidHubUnloaderIndex = targetIndex
end

FacBuildingInteractCtrl.FacStopDragInBatchMode = HL.Method() << function(self)
    self:_StopBatchControllerDragCor()
    self:_OnDragScreenEnd()
end


HL.Commit(FacBuildingInteractCtrl)
