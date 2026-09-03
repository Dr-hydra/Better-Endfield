
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacBuildMode

FacBuildModeCtrl = HL.Class('FacBuildModeCtrl', uiCtrl.UICtrl)






FacBuildModeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_INTERACT_CONVEYOR_LOCAL_CHECKING_FAILED] = 'OnInteractConveyorLocalCheckingFailed',
    [MessageConst.ON_IN_FAC_MAIN_REGION_CHANGE] = 'OnInFacMainRegionChange',
    [MessageConst.FAC_LOCK_BUILD_POS] = 'FacLockBuildPos',
    [MessageConst.FAC_ON_BUILDING_BATCH_MOVED] = 'FacOnBuildingBatchMoved',
}




FacBuildModeCtrl.s_enableContinueBuild = HL.StaticField(HL.Boolean) << false

FacBuildModeCtrl.m_onClickScreen = HL.Field(HL.Function)

FacBuildModeCtrl.m_onPressScreen = HL.Field(HL.Function)

FacBuildModeCtrl.m_onReleaseScreen = HL.Field(HL.Function)

FacBuildModeCtrl.m_mode = HL.Field(HL.Number) << FacConst.FAC_BUILD_MODE.Normal

FacBuildModeCtrl.m_buildArgs = HL.Field(HL.Table)

FacBuildModeCtrl.m_itemData = HL.Field(HL.Userdata)

FacBuildModeCtrl.m_buildingNodeId = HL.Field(HL.Any)

FacBuildModeCtrl.m_buildingId = HL.Field(HL.String) << ""

FacBuildModeCtrl.m_beltId = HL.Field(HL.String) << ""

FacBuildModeCtrl.m_lastMouseWorldPos = HL.Field(HL.Userdata)

FacBuildModeCtrl.m_tickCor = HL.Field(HL.Thread)

FacBuildModeCtrl.m_sizeIndicator = HL.Field(HL.Table)

FacBuildModeCtrl.m_beltStartPreviewMark = HL.Field(HL.Table)

FacBuildModeCtrl.m_pipePreviewMark = HL.Field(HL.Table)

FacBuildModeCtrl.m_hideKey = HL.Field(HL.Number) << -1

FacBuildModeCtrl.m_workingRange = HL.Field(HL.Table)

FacBuildModeCtrl.m_fluidSprayRange = HL.Field(HL.Table)

FacBuildModeCtrl.m_battleRange = HL.Field(HL.Table)

FacBuildModeCtrl.m_isDragging = HL.Field(HL.Boolean) << false

FacBuildModeCtrl.m_draggingOffset = HL.Field(Vector3)

FacBuildModeCtrl.m_camState = HL.Field(HL.Any)

FacBuildModeCtrl.m_signResetBindingId = HL.Field(HL.Number) << -1

FacBuildModeCtrl.m_rpgBuildBindingGroupId = HL.Field(HL.Number) << -1

FacBuildModeCtrl.m_topViewBuildBindingGroupId = HL.Field(HL.Number) << -1


FacBuildModeCtrl.m_simpleFigureBackupValid = HL.Field(HL.Boolean) << false

FacBuildModeCtrl.m_simpleFigureModeBeforeBeltBuild = HL.Field(HL.Number) << FacConst.SIMPLE_FIGURE_MODE.None

FacBuildModeCtrl.s_radioTagHandle = HL.StaticField(HL.Any)





FacBuildModeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    for _, node in ipairs({ self.view.actionButtonsAsIcon, self.view.actionButtonsAsOption }) do
        node.exitButton.onClick:AddListener(function()
            self:_OnClickExitIcon()
        end)
        node.confirmButton.onClick:AddListener(function()
            self:_OnClickConfirm()
        end)
        node.rotateButton.onClick:AddListener(function()
            self:_RotateUnit()
        end)
        node.delButton.onClick:AddListener(function()
            self:_DelBuilding()
        end)
        if node.signResetButton ~= nil then
            node.signResetButton.onClick:AddListener(function()
                self:_OpenSignSettingPanel(true)
            end)
            node.signResetButton.gameObject:SetActiveIfNecessary(false)
        end
    end

    self.view.exitBeltModeButton.onClick:AddListener(function()
        self:_ExitCurMode(true)
    end)

    self.m_onClickScreen = function()
        self:_OnClickScreen()
    end
    self.m_onPressScreen = function()
        self:_OnPressScreen()
    end
    self.m_onReleaseScreen = function()
        self:_OnReleaseScreen()
    end

    self.m_onPlaceFinish = function()
        self:_OnPlaceFinish()
    end


    self.m_rpgBuildBindingGroupId = self:CreateInputGroup(self.view.inputGroup.groupId)
    self:BindInputPlayerAction("fac_build_confirm", function()
        if DeviceInfo.usingController then

            self:_OnClickConfirm()
        end
    end, self.m_rpgBuildBindingGroupId)
    self:BindInputPlayerAction("fac_build_cancel_alter", function()
        self:_ExitCurMode(true)
    end, self.m_rpgBuildBindingGroupId)


    self.m_topViewBuildBindingGroupId = self:CreateInputGroup(self.view.inputGroup.groupId)
    self:BindInputPlayerAction("fac_build_confirm_in_top_view", function()
        if DeviceInfo.usingController then

            self:_OnClickConfirm()
        end
    end, self.m_topViewBuildBindingGroupId)
    self:BindInputPlayerAction("fac_build_continuous_confirm", function()
        if DeviceInfo.usingController then

            self:_OnClickConfirm()
        end
    end, self.m_topViewBuildBindingGroupId)
    self:BindInputPlayerAction("fac_build_cancel", function()
        self:_ExitCurMode(true)
    end, self.m_topViewBuildBindingGroupId)

    self:BindInputPlayerAction("fac_disable_mouse1_sprint", function()

    end)
    self:BindInputPlayerAction("fac_rotate_device", function()
        self:_RotateUnit()
    end)
    self:BindInputPlayerAction("fac_build_mode_delete", function()
        self:_DelBuilding()
    end)
    self.m_signResetBindingId = self:BindInputPlayerAction("fac_build_sign_reset", function()
        self:_OpenSignSettingPanel(true)
    end)
    InputManagerInst:ToggleBinding(self.m_signResetBindingId, false)



    self.view.moveBuildingHint.gameObject:SetActive(false)

    do
        if not self.m_sizeIndicator then
            local prefab = self.loader:LoadGameObject(FacConst.BUILDING_SIZE_INDICATOR_PATH)
            local obj = self:_CreateWorldGameObject(prefab)
            self.m_sizeIndicator = Utils.wrapLuaNode(obj)
            obj.gameObject:SetActive(false)
        end
    end

    do
        if not self.m_beltStartPreviewMark then
            local prefab = self.loader:LoadGameObject(FacConst.BELT_START_PREVIEW_MARK_PREFAB_PATH)
            local obj = self:_CreateWorldGameObject(prefab)
            local mark = Utils.wrapLuaNode(obj)
            mark.gameObject:SetActive(false)
            mark.mesh.sharedMaterial = mark.mesh:GetInstantiatedMaterial()
            local cornerMeshMat = mark.cornerMesh1:GetInstantiatedMaterial()
            mark.cornerMesh1.sharedMaterial = cornerMeshMat
            mark.cornerMesh2.sharedMaterial = cornerMeshMat
            mark.cornerMesh3.sharedMaterial = cornerMeshMat
            mark.cornerMesh4.sharedMaterial = cornerMeshMat
            mark.mats = { mark.mesh.sharedMaterial, cornerMeshMat }
            self.m_beltStartPreviewMark = mark
        end
    end

    do
        if not self.m_pipePreviewMark then
            local prefab = self.loader:LoadGameObject(FacConst.PIPE_PREVIEW_MARK_PREFAB_PATH)
            local obj = self:_CreateWorldGameObject(prefab)
            local mark = Utils.wrapLuaNode(obj)
            mark.gameObject:SetActive(false)

            mark.mesh1.sharedMaterial = mark.mesh1:GetInstantiatedMaterial()
            mark.mesh2.sharedMaterial = mark.mesh2:GetInstantiatedMaterial()
            mark.mesh3.sharedMaterial = mark.mesh3:GetInstantiatedMaterial()
            local cornerMeshMat = mark.cornerMesh1:GetInstantiatedMaterial()
            mark.cornerMesh1.sharedMaterial = cornerMeshMat
            mark.cornerMesh2.sharedMaterial = cornerMeshMat
            mark.cornerMesh3.sharedMaterial = cornerMeshMat
            mark.cornerMesh4.sharedMaterial = cornerMeshMat
            mark.mats = { mark.mesh1.sharedMaterial, mark.mesh2.sharedMaterial, mark.mesh3.sharedMaterial, cornerMeshMat }
            self.m_pipePreviewMark = mark
        end
    end

    do
        if not self.m_fluidSprayRange then
            local prefab = self.loader:LoadGameObject(FacConst.FLUID_SPRAY_RANGE_EFFECT)
            local obj = self:_CreateWorldGameObject(prefab)
            self.m_fluidSprayRange = Utils.wrapLuaNode(obj)
            obj.gameObject:SetActive(false)
        end
    end

    self.view.hideToggle.toggle.onValueChanged:AddListener(function(isOn)
        self:_OnChangeHideToggle(isOn)
    end)
    self.view.continueBuildToggle.isOn = false
    self.view.continueBuildToggle.onValueChanged:AddListener(function(isOn)
        self:_OnChangeContinueToggle(isOn)
    end)

    self:_InitKeyHint()

    if BEYOND_DEBUG_COMMAND then
        self:BindInputEvent(CS.Beyond.Input.KeyboardKeyCode.C, function()
            self:DebugOutputPrepareBuildingPosInfo()
        end)
    end
end

FacBuildModeCtrl._Tick = HL.Method() << function(self)
    if not CS.UnityEngine.Application.isFocused then return end
    if self.m_mode == FacConst.FAC_BUILD_MODE.Normal then
        return
    end

    local isBelt = self.m_mode == FacConst.FAC_BUILD_MODE.Belt
    local isBuilding = self.m_mode == FacConst.FAC_BUILD_MODE.Building
    local isLogistic = self.m_mode == FacConst.FAC_BUILD_MODE.Logistic
    local isBlueprint = self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint

    if self.m_lockBuildPos then
        if isBelt then
            self:_UpdateBPStartPreviewMarkColor()
        end
        return
    end

    local curMousePos = self:_GetCurPointerPressPos()
    local camRay = CameraManager.mainCamera:ScreenPointToRay(curMousePos)
    local _, curMouseWorldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(camRay)
    local customGridCenter = curMouseWorldPos

    local usingDrag = self:_InDragMode()
    if self.m_isDragging then
        curMousePos = curMousePos + self.m_draggingOffset
    end

    local curMode, posChanged

    if isBuilding or isLogistic then
        curMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
        if not usingDrag or self.m_isDragging then

            GameInstance.remoteFactoryManager:GridPositionTriggered(curMousePos, 4)
            posChanged = true
        elseif usingDrag then

            local buildingCenterPos = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
            local buildingScreenPos = CameraManager.mainCamera:WorldToScreenPoint(buildingCenterPos)
            buildingScreenPos.z = 0
            local newScreenPos = Vector3.zero
            newScreenPos.x = lume.clamp(buildingScreenPos.x, self.view.config.DRAG_MODE_BUILDING_PADDING_X.x, Screen.width - self.view.config.DRAG_MODE_BUILDING_PADDING_X.y)
            newScreenPos.y = lume.clamp(buildingScreenPos.y, self.view.config.DRAG_MODE_BUILDING_PADDING_Y.x, Screen.height - self.view.config.DRAG_MODE_BUILDING_PADDING_Y.y)
            if newScreenPos ~= buildingScreenPos then
                GameInstance.remoteFactoryManager:GridPositionTriggered(newScreenPos, 4)
                buildingCenterPos = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
                posChanged = true
            end
            customGridCenter = buildingCenterPos
        end
        if not self.m_buildingNodeId and DeviceInfo.usingKeyboard then
            Notify(MessageConst.CHANGE_MOUSE_ICON_HINT, {
                name = "FacBuildModeCtrl-ContinuousBuild",
                type = self:_EnableContinueBuild() and UIConst.MOUSE_ICON_HINT.ContinuousBuild or UIConst.MOUSE_ICON_HINT.Default,
            })
        end
    elseif isBelt then
        curMode = GameInstance.remoteFactoryManager.interact.currentConveyorMode
        if usingDrag then
            if self.m_isDragging then
                GameInstance.remoteFactoryManager:GridPositionTriggered(curMousePos, 2)
                posChanged = true
            end
        else
            GameInstance.remoteFactoryManager:GridPositionTriggered(curMousePos, 4)
            posChanged = true
        end
        local hasStart = curMode.hasStart
        if hasStart ~= self.m_beltHasStartLastTick then
            self:_UpdateOnBeltHasStartChanged()
            if usingDrag then
                local mark = self:_IsPipe() and self.m_pipePreviewMark or self.m_beltStartPreviewMark
                mark.gameObject.gameObject:SetActive(hasStart)
            end
        end
        if hasStart and usingDrag then

            local beltEndWorldPos = curMode:GetDragHandlingPoint(Vector3.zero) + Vector3(0.5, 0, 0.5)
            beltEndWorldPos.y = curMouseWorldPos.y
            if not self.m_isDragging then
                customGridCenter = beltEndWorldPos
            end
            self:_UpdateBPStartPreviewMarkWithWorldPos(beltEndWorldPos)
        end
        if not usingDrag then
            self:_UpdateBPStartPreviewMarkWithWorldPos(curMouseWorldPos)
        end

        if usingDrag and LuaSystemManager.factory.inTopView then
            if GameInstance.remoteFactoryManager then
                local interact = GameInstance.remoteFactoryManager.interact
                if interact then
                    interact:SetTopViewCameraFactor(1 - LuaSystemManager.factory.m_topViewCamCtrl.curZoomPercent)
                end
            end
        end
    elseif isBlueprint then
        curMode = GameInstance.remoteFactoryManager.interact.currentBlueprintMode
        if not usingDrag or self.m_isDragging then

            GameInstance.remoteFactoryManager:GridPositionTriggered(curMousePos, 4)
            posChanged = true
            local gridPos = Unity.Vector2Int(curMode.preparedPosition.x, curMode.preparedPosition.z)
            if gridPos ~= self.m_lastBPGridPos then
                self:_SetBluePrintSelectGrids(gridPos)
            end
        elseif usingDrag then
            if not self.m_lastBPGridPos then
                posChanged = true
                self:_SetBluePrintSelectGrids(Unity.Vector2Int(curMode.preparedPosition.x, curMode.preparedPosition.z))
            else

                local buildingCenterPos = Vector3(curMode.preparedPosition.x + 0.5, curMode.preparedPosition.y, curMode.preparedPosition.z + 0.5)
                local buildingScreenPos = CameraManager.mainCamera:WorldToScreenPoint(buildingCenterPos)
                buildingScreenPos.z = 0
                local newScreenPos = Vector3.zero
                newScreenPos.x = lume.clamp(buildingScreenPos.x, self.view.config.DRAG_MODE_BUILDING_PADDING_X.x, Screen.width - self.view.config.DRAG_MODE_BUILDING_PADDING_X.y)
                newScreenPos.y = lume.clamp(buildingScreenPos.y, self.view.config.DRAG_MODE_BUILDING_PADDING_Y.x, Screen.height - self.view.config.DRAG_MODE_BUILDING_PADDING_Y.y)
                if newScreenPos ~= buildingScreenPos then
                    GameInstance.remoteFactoryManager:GridPositionTriggered(newScreenPos, 4)
                    posChanged = true
                    self:_SetBluePrintSelectGrids(Unity.Vector2Int(curMode.preparedPosition.x, curMode.preparedPosition.z))
                end
            end
        end
        customGridCenter = Unity.Vector3(curMode.preparedPosition.x, curMode.preparedPosition.y, curMode.preparedPosition.z)
        Notify(MessageConst.ON_BLUEPRINT_UPDATE)
    end

    if curMode then
        curMode.useCustomGridCenter = LuaSystemManager.factory.inTopView
        if curMode.useCustomGridCenter and posChanged then
            curMode.customGridCenter = customGridCenter
        end
    end

    self.m_lastMouseWorldPos = curMouseWorldPos
    self:_UpdateValidResult()
    self:_UpdateAutoConnectExtraHint()
    self:_NotifyPowerPoleTravelHint()

    if usingDrag then
        local targetWorldPos, targetSize
        if isBelt then
            targetWorldPos = customGridCenter
            targetSize = Vector3.zero
        elseif isBlueprint then
            targetWorldPos = Vector3(curMode.preparedPosition.x, curMode.preparedPosition.y, curMode.preparedPosition.z)
            targetSize = Vector3.zero
            local range = self.m_buildArgs.range
            local needReverse = curMode.preparedDirection % 2 == 1
            targetSize.x = needReverse and range.height or range.width
            targetSize.z = needReverse and range.width or range.height

        else
            local pos, rot = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
            targetWorldPos = pos
            if isBuilding then
                local data = Tables.factoryBuildingTable:GetValue(self.m_buildingId)
                targetSize = (rot.y % 180 == 0) and Vector3(data.range.width, 0, data.range.depth) or Vector3(data.range.depth, 0, data.range.width)
            else
                targetSize = Vector3.one
            end
        end
        local min = CameraManager.mainCamera:WorldToScreenPoint(targetWorldPos + targetSize / 2)
        local max = CameraManager.mainCamera:WorldToScreenPoint(targetWorldPos - targetSize / 2)
        local size = max - min
        size.x = math.abs(size.x)
        size.y = math.abs(size.y)
        min.x = math.min(min.x, max.x)
        min.y = math.min(min.y, max.y)
        max = min + size
        local targetScreenRect = Unity.Rect(min.x, Screen.height - (min.y + size.y), size.x, size.y)
        UIUtils.updateTipsPositionWithScreenRect(self.view.actionButtonsAsIcon.transform, targetScreenRect, self.view.transform,
                self.uiCamera, UIConst.UI_TIPS_POS_TYPE.FacTopViewBuildActionIcons, {
                    top = self.view.config.DRAG_MODE_BUILDING_PADDING_Y.x,
                    left = self.view.config.DRAG_MODE_BUILDING_PADDING_X.x,
                    right = self.view.config.DRAG_MODE_BUILDING_PADDING_X.y,
                    top = self.view.config.DRAG_MODE_BUILDING_PADDING_Y.x,
                    bottom = self.view.config.DRAG_MODE_BUILDING_PADDING_Y.y,
                })


        if isBelt then
            local dir = self.view.joystick.jsValue
            local needSwitch = LuaSystemManager.factory.canMoveCamTarget == false
            if needSwitch then
                LuaSystemManager.factory.canMoveCamTarget = true
            end
            LuaSystemManager.factory:MoveTopViewCamTarget(dir * 15 * Time.deltaTime)
            if needSwitch then
                LuaSystemManager.factory.canMoveCamTarget = false
            end
        end
    end
end

FacBuildModeCtrl.OnShow = HL.Override() << function(self)
    self:_AddRegister()
    CS.HG.Rendering.ScriptBridge.TAAUControlBridge.taauFastConverge = true
    Notify(MessageConst.TOGGLE_FORBID_ATTACK, { "FacBuildModeCtrl", true })
    self:FacLockBuildPos({ false })
    self.view.continueBuildToggle.isOn = FacBuildModeCtrl.s_enableContinueBuild
    self.view.actionButtonsAsOption.signResetButton.gameObject:SetActiveIfNecessary(false)
    self.view.miasmaIndicator:InitMiasmaIndicator()
end

FacBuildModeCtrl.OnHide = HL.Override() << function(self)
    self:FacLockBuildPos({ false })
    self:_OnReleaseScreen()
    self:_ClearRegister()
    CS.HG.Rendering.ScriptBridge.TAAUControlBridge.taauFastConverge = true
    Notify(MessageConst.TOGGLE_FORBID_ATTACK, { "FacBuildModeCtrl", false })

    Notify(MessageConst.CHANGE_MOUSE_ICON_HINT, {
        name = "FacBuildModeCtrl-ContinuousBuild",
        type = UIConst.MOUSE_ICON_HINT.Default,
    })

    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.FacBuildSystemActionConflictName)
end

FacBuildModeCtrl.OnClose = HL.Override() << function(self)
    self:_ExitCurMode(false, true, true)
    self:_ClearRegister()
    Notify(MessageConst.TOGGLE_FORBID_ATTACK, { "FacBuildModeCtrl", false })


    self.m_hideKey = UIManager:RecoverScreen(self.m_hideKey)

    GameInstance.remoteFactoryManager.inBuildMode = false

    Notify(MessageConst.CHANGE_MOUSE_ICON_HINT, {
        name = "FacBuildModeCtrl-ContinuousBuild",
        type = UIConst.MOUSE_ICON_HINT.Default,
    })

    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.FacBuildSystemActionConflictName)
end

FacBuildModeCtrl.ExtractHotSwitchRuntimeState = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_keyHintCells == nil and self.m_sizeIndicator == nil and self.m_beltStartPreviewMark == nil and self.m_pipePreviewMark == nil and self.m_fluidSprayRange == nil then
        return nil
    end

    local state = {
        keyHintCells = self.m_keyHintCells,
        sizeIndicator = self.m_sizeIndicator,
        beltStartPreviewMark = self.m_beltStartPreviewMark,
        pipePreviewMark = self.m_pipePreviewMark,
        fluidSprayRange = self.m_fluidSprayRange,
    }
    self.m_keyHintCells = nil
    self.m_sizeIndicator = nil
    self.m_beltStartPreviewMark = nil
    self.m_pipePreviewMark = nil
    self.m_fluidSprayRange = nil
    return state
end

FacBuildModeCtrl.RestoreHotSwitchRuntimeState = HL.Override(HL.Opt(HL.Any)) << function(self, state)
    if not state then
        return
    end
    self.m_keyHintCells = state.keyHintCells
    self.m_sizeIndicator = state.sizeIndicator
    self.m_beltStartPreviewMark = state.beltStartPreviewMark
    self.m_pipePreviewMark = state.pipePreviewMark
    self.m_fluidSprayRange = state.fluidSprayRange
end




FacBuildModeCtrl.EnterBlueprintMode = HL.StaticMethod(HL.Table) << function(args)
    FacBuildModeCtrl._EnterMode(args, FacConst.FAC_BUILD_MODE.Blueprint)
end

FacBuildModeCtrl._EnterMode = HL.StaticMethod(HL.Table, HL.Number) << function(args, mode)
    if Utils.isCurSquadAllDead() then
        return
    end

    if FacBuildModeCtrl._CheckBanAndToastSignInTopView(args.itemId) then
        return
    end

    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        if ctrl.m_mode ~= FacConst.FAC_BUILD_MODE.Normal then
            return
        end
    end

    if not FacBuildModeCtrl.CheckCanEnterAndShowToast() then
        return
    end

    if mode == FacConst.FAC_BUILD_MODE.Logistic then
        local itemId = args.itemId
        local logisticId = Tables.factoryItem2LogisticIdTable[itemId].logisticId
        local logisticData, isLiquid = FactoryUtils.getLogisticData(logisticId)
        if isLiquid and not FactoryUtils.isDomainSupportPipe() then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_DOMAIN_NOT_SUPPORT_PIPE)
            return
        end
    end


    if not GameInstance.player.systemActionConflictManager:TryStartSystemAction(Const.FacBuildSystemActionConflictName) then
        return
    end


    FacBuildModeCtrl.s_radioTagHandle = GameInstance.player.globalTagsSystem:AddGlobalTag(CS.Beyond.Gameplay.GlobalTagDefine.notStopRadioTags)
    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = "facBuildMode", isInMainHud = false })

    FacBuildModeCtrl._BeforeEnterBuildMode(args.skipMainHudAnim or false)

    local onClearScreenFinishAct = function(key)
        if GameInstance.player.systemActionConflictManager.curProcessingSystemAction ~= Const.FacBuildSystemActionConflictName then

            UIManager:RecoverScreen(key)

            Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = "facBuildMode", isInMainHud = true })
            if FacBuildModeCtrl.s_radioTagHandle then

                FacBuildModeCtrl.s_radioTagHandle:RemoveTag()
                FacBuildModeCtrl.s_radioTagHandle = nil
            end

            Notify(MessageConst.ON_BUILD_MODE_CHANGE, FacConst.FAC_BUILD_MODE.Normal)
            return
        end


        local self = FacBuildModeCtrl.AutoOpen(PANEL_ID, nil, true)
        self.m_hideKey = key
        if mode == FacConst.FAC_BUILD_MODE.Building then
            self:_EnterBuildingMode(args)
        elseif mode == FacConst.FAC_BUILD_MODE.Logistic then
            self:_EnterLogisticMode(args)
        elseif mode == FacConst.FAC_BUILD_MODE.Belt then
            if lume.isarray(args) then

                args = { beltId = args[1] }
            end
            self:_EnterBeltMode(args)
        elseif mode == FacConst.FAC_BUILD_MODE.Blueprint then
            self:_EnterBlueprintMode(args)
        end
        if args.triggerPressScreen then

            self:_StartCoroutine(function()
                coroutine.step()
                if self:IsShow() and InputManager.IsLeftMouseDown(true) then
                    self:_OnPressScreen()
                end
            end)
        end
        if DeviceInfo.usingKeyboard then
            self:_UpdateAutoMoveTopViewCamState()
        end
    end


    if args.fastEnter then
        local key = UIManager:ClearScreen(Const.RESERVE_PANEL_IDS_FOR_FAC_BUILD_MODE)
        onClearScreenFinishAct(key)
    else
        UIManager:ClearScreenWithOutAnimation(function(key)
            onClearScreenFinishAct(key)
        end, Const.RESERVE_PANEL_IDS_FOR_FAC_BUILD_MODE)
    end

    GameInstance.remoteFactoryManager.inBuildMode = true
end

FacBuildModeCtrl.EnterBuildingMode = HL.StaticMethod(HL.Table) << function(args)
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacMode) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_PLACE_BUILDING_LOCKED)
        return
    end
    FacBuildModeCtrl._EnterMode(args, FacConst.FAC_BUILD_MODE.Building)
end

FacBuildModeCtrl.EnterLogisticMode = HL.StaticMethod(HL.Table) << function(args)
    FacBuildModeCtrl._EnterMode(args, FacConst.FAC_BUILD_MODE.Logistic)
end

FacBuildModeCtrl.EnterBeltMode = HL.StaticMethod(HL.Table) << function(args)
    FacBuildModeCtrl._EnterMode(args, FacConst.FAC_BUILD_MODE.Belt)
end

FacBuildModeCtrl._BeforeEnterBuildMode = HL.StaticMethod(HL.Boolean) << function(skipMainHudAnim)
    Notify(MessageConst.HIDE_ITEM_TIPS)

    PhaseManager:ExitPhaseFastTo(PhaseId.Level, true)
    Notify(MessageConst.BEFORE_ENTER_BUILD_MODE, skipMainHudAnim)
end

FacBuildModeCtrl.CheckCanEnterAndShowToast = HL.StaticMethod().Return(HL.Boolean)<< function()
    local level = PhaseManager.m_openedPhaseSet[PhaseId.Level]
    if not level then
        return false
    end
    local csCheckResult = GameInstance.remoteFactoryManager:CheckEnterInteractMode()
    if csCheckResult == CS.Beyond.Gameplay.RemoteFactory.EnterInteractModeCheckResult.InvalidLevel then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CANT_ENTER_BUILD_MODE_WHEN_NO_FAC_REGION)
        return false
    end
    if level.isPlayerOutOfRangeManual then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CANT_ENTER_BUILD_MODE_WHEN_OUT_OF_RANGE_MANUAL)
        return false
    end
    if GameWorld.battle.isSquadInFight and not Utils.isInSettlementDefenseDefending() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CANT_ENTER_BUILD_MODE_WHEN_FIGHT)
        return false
    end
    return true
end






FacBuildModeCtrl.m_needCloseMiniPower = HL.Field(HL.Boolean) << false

FacBuildModeCtrl._AddRegister = HL.Method() << function(self)

    if not DeviceInfo.usingController then
        local touchPanel = UIManager.commonTouchPanel
        touchPanel.onClick:AddListener(self.m_onClickScreen)
        touchPanel.onPress:AddListener(self.m_onPressScreen)
        touchPanel.onRelease:AddListener(self.m_onReleaseScreen)
    end


    self:_Tick()
    self.m_tickCor = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_Tick()
        end
    end)
end

FacBuildModeCtrl._ClearRegister = HL.Method() << function(self)

    self.m_tickCor = self:_ClearCoroutine(self.m_tickCor)


    local touchPanel = UIManager.commonTouchPanel
    touchPanel.onClick:RemoveListener(self.m_onClickScreen)
    touchPanel.onPress:RemoveListener(self.m_onPressScreen)
    touchPanel.onRelease:RemoveListener(self.m_onReleaseScreen)
end




FacBuildModeCtrl._OnClickScreen = HL.Method() << function(self)
    if not DeviceInfo.usingTouch then
        self:_OnClickConfirm()
    elseif LuaSystemManager.factory.inTopView and self.m_mode == FacConst.FAC_BUILD_MODE.Belt and not GameInstance.remoteFactoryManager.interact.currentConveyorMode.hasStart and self.view.actionHint.gameObject.activeInHierarchy then
        self.view.actionHint:Play("facbuildmodeerrorhint_blink")
    end
end

FacBuildModeCtrl._OnClickConfirm = HL.Method() << function(self)
    if not FacBuildModeCtrl.s_enableConfirmBuild then
        return
    end

    local mainChar = GameInstance.mainCharPtr
    if mainChar then
        if mainChar.movementComponent and mainChar.movementComponent.moveMode == CS.Beyond.Gameplay.Core.MovementComponent.MoveMode.Plunge then

            return
        end
    end

    if self.m_mode == FacConst.FAC_BUILD_MODE.Building then
        self:_ConfirmBuilding()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        self:_ConfirmLogistic()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        if self.m_curBuildIsValid then
            if self.m_lockBuildPos then

                local isPipe = self:_IsPipe()
                local mark = isPipe and self.m_pipePreviewMark or self.m_beltStartPreviewMark
                local worldPos = mark.transform.position
                local voxelPos = GameInstance.remoteFactoryManager.visual:WorldToVoxel(worldPos)
                local gridPos = Unity.Vector2Int(math.floor(voxelPos.x), math.floor(voxelPos.z))
                GameInstance.remoteFactoryManager:GridPositionTriggered(gridPos, Vector2.zero, voxelPos.y, 0)
            else
                GameInstance.remoteFactoryManager:GridPositionTriggered(self:_GetCurPointerPressPos(), 0)
            end
            AudioAdapter.PostEvent("au_ui_fac_btn_belt_build_click")
        else
            AudioAdapter.PostEvent("au_ui_fac_unbuildable")
        end
        local hasStart = GameInstance.remoteFactoryManager.interact.currentConveyorMode.hasStart
        if not hasStart then
            Notify(MessageConst.SHOW_TOAST, self:_IsPipe() and Language.LUA_FAC_BUILD_PIPE_START_CLICK_ERROR or Language.LUA_FAC_BUILD_BELT_START_CLICK_ERROR)
            if self.view.errorHint.gameObject.activeInHierarchy then
                self.view.errorHint:Play("facbuildmodeerrorhint_blink")
            end
        end
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        self:_ConfirmBlueprint()
    end
end

FacBuildModeCtrl._OnPressScreen = HL.Method() << function(self)
    self.m_mobileDragBeltChecker = self:_ClearCoroutine(self.m_mobileDragBeltChecker)

    if self.m_mode == FacConst.FAC_BUILD_MODE.Normal then
        return
    end

    if not LuaSystemManager.factory.inTopView or not DeviceInfo.usingTouch then
        return
    end

    if Input.touchCount > 1 then

        return
    end

    local isBuilding = self.m_mode == FacConst.FAC_BUILD_MODE.Building
    local isBelt = self.m_mode == FacConst.FAC_BUILD_MODE.Belt
    local isLogistic = self.m_mode == FacConst.FAC_BUILD_MODE.Logistic
    local isBlueprint = self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint

    local curMousePos = self:_GetCurPointerPressPos()
    local camRay = CameraManager.mainCamera:ScreenPointToRay(curMousePos)
    local _, worldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(camRay)

    local dragTargetCenterPos, renderExist
    if isBuilding or isLogistic then
        dragTargetCenterPos, _, _, renderExist = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
    elseif isBelt then
        renderExist = true
        dragTargetCenterPos = GameInstance.remoteFactoryManager.interact.currentConveyorMode:GetDragHandlingPoint(worldPos) + Vector3(0.5, 0, 0.5)
    elseif isBlueprint then
        renderExist = true
        local mode = GameInstance.remoteFactoryManager.interact.currentBlueprintMode
        dragTargetCenterPos = Vector3(mode.preparedPosition.x, mode.preparedPosition.y, mode.preparedPosition.z)
    end
    if not renderExist then
        return
    end

    local conveyorModeValidResult = GameInstance.remoteFactoryManager:CheckValidConveyorStartByScreenPos(curMousePos, 1)


    local dragTargetCenterScreenPos = CameraManager.mainCamera:WorldToScreenPoint(dragTargetCenterPos)
    local halfMinSizeInScreen = self.view.config.MOBILE_MIN_DRAG_SIZE / 2 / self.view.transform.rect.width * Screen.width
    if (math.abs(curMousePos.x - dragTargetCenterScreenPos.x) > halfMinSizeInScreen or math.abs(curMousePos.y - dragTargetCenterScreenPos.y) > halfMinSizeInScreen) and (not conveyorModeValidResult) then
        local targetWorldRect, direction
        targetWorldRect = Unity.Rect()
        if isBuilding then
            local buildingData = Tables.factoryBuildingTable:GetValue(self.m_buildingId)
            local width = buildingData.range.width
            local depth = buildingData.range.depth
            targetWorldRect.size = Vector2(width, depth)
            direction = GameInstance.remoteFactoryManager.interact.currentBuildingMode.finalBuildingDirection
        elseif isBlueprint then
            local range = self.m_buildArgs.range
            targetWorldRect.size = Vector2(range.width, range.height)
            direction = GameInstance.remoteFactoryManager.interact.currentBlueprintMode.preparedDirection
        else
            targetWorldRect.size = Vector2.one
            direction = 0
        end
        if direction % 2 == 1 then
            targetWorldRect.size = Vector2(targetWorldRect.size.y, targetWorldRect.size.x)
        end
        local rectCenter = Vector2(dragTargetCenterPos.x, dragTargetCenterPos.z)
        if isBlueprint then
            if targetWorldRect.width % 2 == 1 then
                rectCenter.x = rectCenter.x + 0.5
            end
            if targetWorldRect.height % 2 == 1 then
                rectCenter.y = rectCenter.y + 0.5
            end
        end
        targetWorldRect.center = rectCenter




        if not targetWorldRect:Contains(worldPos:XZ()) then

            return
        end
    end

    local autoMoveCamExtraPadding
    if isBelt then
        if conveyorModeValidResult then
            LuaSystemManager.factory.canMoveCamTarget = false
            self.m_mobileDragBeltChecker = self:_StartCoroutine(function()
                while true do
                    coroutine.step()
                    if Input.touchCount > 1 then

                        LuaSystemManager.factory.canMoveCamTarget = true
                        self.m_mobileDragBeltChecker = nil
                        return
                    end

                    if (self:_GetCurPointerPressPos() - curMousePos).sqrMagnitude > 100 then
                        self.m_isDragging = true
                        UIManager.commonTouchPanel.enableZoom = false
                        self.m_draggingOffset = Vector3.zero
                        GameInstance.remoteFactoryManager:GridPositionTriggered(curMousePos, 1)
                        self:_UpdateAutoMoveTopViewCamState()
                        self.m_mobileDragBeltChecker = nil
                        return
                    end
                end
            end)
        end
    else
        self.m_isDragging = true
        UIManager.commonTouchPanel.enableZoom = false
        local targetCenterScreenPos
        if isBlueprint then

            targetCenterScreenPos = CameraManager.mainCamera:WorldToScreenPoint(dragTargetCenterPos + Vector3(0.5, 0, 0.5))
        else
            targetCenterScreenPos = CameraManager.mainCamera:WorldToScreenPoint(dragTargetCenterPos)
        end
        targetCenterScreenPos.z = 0
        self.m_draggingOffset = targetCenterScreenPos - curMousePos
        autoMoveCamExtraPadding = Unity.Vector4()
        if self.m_draggingOffset.x >= 0 then
            autoMoveCamExtraPadding.z = self.m_draggingOffset.x
        else
            autoMoveCamExtraPadding.y = -self.m_draggingOffset.x
        end
        if self.m_draggingOffset.y >= 0 then
            autoMoveCamExtraPadding.x = self.m_draggingOffset.y
        else
            autoMoveCamExtraPadding.w = -self.m_draggingOffset.y
        end
    end
    if self.m_isDragging then
        LuaSystemManager.factory.canMoveCamTarget = false
        self:_UpdateAutoMoveTopViewCamState(autoMoveCamExtraPadding)
    end
end


FacBuildModeCtrl.m_mobileDragBeltChecker = HL.Field(HL.Any)


FacBuildModeCtrl._OnReleaseScreen = HL.Method() << function(self)
    local curMousePos = self:_GetCurPointerPressPos()
    if self.m_isDragging then
        GameInstance.remoteFactoryManager:GridPositionTriggered(curMousePos, 3)
        self.m_isDragging = false
        UIManager.commonTouchPanel.enableZoom = true
        self.m_draggingOffset = nil
        LuaSystemManager.factory:ToggleAutoMoveTopViewCam()
    elseif self.m_mobileDragBeltChecker then

        self.m_mobileDragBeltChecker = self:_ClearCoroutine(self.m_mobileDragBeltChecker)
        if Input.touchCount <= 1 then
            GameInstance.remoteFactoryManager:GridPositionTriggered(curMousePos, 1)

            self:_StartTimer(0, function()
                GameInstance.remoteFactoryManager:GridPositionTriggered(curMousePos, 3)
            end)
        end
    end
    LuaSystemManager.factory.canMoveCamTarget = true
end






FacBuildModeCtrl.OnInFacMainRegionChange = HL.Method(HL.Boolean) << function(self, inMainRegion)
    if inMainRegion then
        return
    end

    if self.m_mode == FacConst.FAC_BUILD_MODE.Building then
        if FactoryUtils.canPlaceBuildingOnCurRegion(self.m_buildingId) then
            return
        end
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        local logisticData, isLiquid = FactoryUtils.getLogisticData(self.m_buildingId)
        if isLiquid then
            return
        end
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        if self:_IsPipe() then
            return
        end
    end
    self:_ExitCurMode(false, true)
end

FacBuildModeCtrl._SetCamState = HL.Method(HL.Opt(HL.String)) << function(self, camStateName)
    if LuaSystemManager.factory.inTopView then
        return
    end
    if not camStateName then
        if self.m_camState then
            self.m_camState = FactoryUtils.exitFacCamera(self.m_camState)
        end
        return
    end
    if self.m_camState then
        logger.error("self.m_camState Not Null", self.m_camState)
        return
    end
    self.m_camState = FactoryUtils.enterFacCamera(camStateName)
end

FacBuildModeCtrl._OnChangeHideToggle = HL.Method(HL.Boolean) << function(self, isOn)
    local fac = LuaSystemManager.factory
    local mode = FacConst.SIMPLE_FIGURE_MODE.None
    if isOn then
        mode = self:_IsPipe() and FacConst.SIMPLE_FIGURE_MODE.SimpleBeltFigure or FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure
    end
    fac:SetSimpleFigureMode(mode)
end






FacBuildModeCtrl.ExitCurModeForCS = HL.StaticMethod(HL.Opt(HL.Any)) << function()
    FacBuildModeCtrl.ExitCurMode(true)
end

FacBuildModeCtrl.ExitCurMode = HL.StaticMethod(HL.Opt(HL.Boolean)) << function(skipAnim)

    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.FacBuildSystemActionConflictName)

    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if not isOpen then
        return
    end

    local self = ctrl
    self:_ExitCurMode(false, skipAnim, true)
end

FacBuildModeCtrl._OnClickExitIcon = HL.Method() << function(self)
    if self.m_mode == FacConst.FAC_BUILD_MODE.Belt and self:_InDragMode() then

        local conveyorMode = GameInstance.remoteFactoryManager.interact.currentConveyorMode
        if conveyorMode then
            conveyorMode:TriggerConveyorKeyReset()
        end
    else
        self:_ExitCurMode(true)
    end
end

FacBuildModeCtrl._ExitCurMode = HL.Method(HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean)) << function(self, fromClick, skipAnim, forceExit)
    if not FacBuildModeCtrl.s_enableExitBuildMode and not forceExit then
        return
    end

    if self.m_mode == FacConst.FAC_BUILD_MODE.Normal then
        return
    end

    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.FacBuildSystemActionConflictName)

    if self.m_mode == FacConst.FAC_BUILD_MODE.Building then
        self:_CancelBuilding(skipAnim)
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        self:_CancelLogistic(skipAnim)
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        self:_ExitBeltMode(skipAnim)
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        if InputManagerInst.inChangingInputDevice then
            self:_ExitBlueprintMode(true)
            return
        end

        local targets = self.m_buildArgs.batchSelectTargets
        local showCreateHint = self.m_buildArgs.showCreateHint
        if fromClick and targets then

            self:_ExitBlueprintMode(true)
            Notify(MessageConst.FAC_ENTER_DESTROY_MODE, {
                fastEnter = true,
                batchSelectTargets = targets,
                showCreateHint = showCreateHint,
            })
            return
        end
        local csBPInst = self.m_buildArgs.csBPInst
        local bpSearchInfos = self.m_buildArgs.searchInfos
        if fromClick and csBPInst then

            local clearScreenKey = UIManager:ClearScreen({ PANEL_ID })
            self:_ExitBlueprintMode(true)
            PhaseManager:OpenPhaseFast(PhaseId.FacBlueprint, { csBPInst = csBPInst, bpSearchInfos = bpSearchInfos })
            UIManager:RecoverScreen(clearScreenKey)
            return
        end

        self:_ExitBlueprintMode(skipAnim)
    end
    AudioAdapter.PostEvent("au_sfx_ui_fac_buiding_off")
end

FacBuildModeCtrl._ClearArgs = HL.Method() << function(self)
    self.m_buildArgs = nil
    self.m_itemData = nil
    self.m_buildingNodeId = nil
    self.m_buildingId = ""
    self.m_beltId = ""
    self.m_lastMouseWorldPos = nil
    self:_SetCamState()
end

FacBuildModeCtrl._ExitMode = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    local oldMode = self.m_mode

    if oldMode == FacConst.FAC_BUILD_MODE.Blueprint then
        local mode =  GameInstance.remoteFactoryManager.interact.currentBlueprintMode
        mode.onPlaceBlueprint = mode.onPlaceBlueprint - self.m_onPlaceFinish
    elseif oldMode == FacConst.FAC_BUILD_MODE.Building or oldMode == FacConst.FAC_BUILD_MODE.Logistic then
        local mode =  GameInstance.remoteFactoryManager.interact.buildingMode
        mode.onPlaceBuilding = mode.onPlaceBuilding - self.m_onPlaceFinish
    end

    self.m_mode = FacConst.FAC_BUILD_MODE.Normal
    self.view.errorHint.gameObject:SetActiveIfNecessary(false)

    local onExit = self.m_buildArgs.onExit
    self:_ClearArgs()
    GameInstance.remoteFactoryManager.interact:ExitCurrentMode()
    LuaSystemManager.factory:ToggleAutoMoveTopViewCam()

    local exitAct = function()
        self:Hide()
        self.m_hideKey = UIManager:RecoverScreen(self.m_hideKey)

        Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = "facBuildMode", isInMainHud = true })
        if FacBuildModeCtrl.s_radioTagHandle then

            FacBuildModeCtrl.s_radioTagHandle:RemoveTag()
            FacBuildModeCtrl.s_radioTagHandle = nil
        end

        if onExit then
            onExit()
        end

        if oldMode == FacConst.FAC_BUILD_MODE.Building then
            Notify(MessageConst.ON_EXIT_BUILDING_MODE)
        elseif oldMode == FacConst.FAC_BUILD_MODE.Logistic then
            Notify(MessageConst.ON_EXIT_LOGISTIC_MODE)
        elseif oldMode == FacConst.FAC_BUILD_MODE.Belt then
            Notify(MessageConst.ON_EXIT_BELT_MODE)
        elseif oldMode == FacConst.FAC_BUILD_MODE.Blueprint then
            Notify(MessageConst.ON_EXIT_BLUEPRINT_MODE)
        end
        Notify(MessageConst.ON_BUILD_MODE_CHANGE, self.m_mode)
        GameInstance.remoteFactoryManager.inBuildMode = false
        if LuaSystemManager.factory.inTopView then
            LuaSystemManager.factory:ApplySimpleFigureToRenderer()
        end
    end
    if not skipAnim then
        self:PlayAnimationOutWithCallback(exitAct)
    else
        exitAct()
    end
end

FacBuildModeCtrl._UpdateCommonNodesOnEnterMode = HL.Method() << function(self)
    local isBuilding = self.m_mode == FacConst.FAC_BUILD_MODE.Building
    local isBelt = self.m_mode == FacConst.FAC_BUILD_MODE.Belt
    local isLogistic = self.m_mode == FacConst.FAC_BUILD_MODE.Logistic
    local isBlueprint = self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint
    local isPipe = self:_IsPipe()
    local usingDrag = self:_InDragMode()
    local inTopView = LuaSystemManager.factory.inTopView

    if isBelt and usingDrag then
        self.view.actionHint.gameObject:SetActive(true)
        self:_UpdateOnBeltHasStartChanged()
    else
        self.view.actionHint.gameObject:SetActive(false)
    end

    local node = (inTopView and self.view.actionButtonsAsIcon or self.view.actionButtonsAsOption)
    node.confirmButton.gameObject:SetActive(true)
    node.rotateButton.gameObject:SetActive(not isBelt and self:_CanRotate())
    node.rotateButton.text = isBelt and Language["ui_fac_common_logistic_rotate_mobile"] or Language["ui_fac_common_rotate_mobile"]
    node.delButton.gameObject:SetActive(self:_CanDelBuilding())
    if not inTopView then
        node.confirmText.text = isBelt and Language.LUA_FAC_BUILD_MODE_CONFIRM_BELT_START or Language.LUA_FAC_BUILD_CONFIRM_NORMAL
    end

    local beltUnlocked = GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedBelt

    local showHideToggle = inTopView and (isBelt or isPipe)
    if showHideToggle then
        if isBelt then
            showHideToggle = FactoryUtils.canShowPipe()
        else
            showHideToggle = beltUnlocked
        end
    end
    self.view.hideToggle.gameObject:SetActive(showHideToggle)
    if showHideToggle then
        if self.m_buildArgs.isFromChangeInputDevice then
            local rMode = LuaSystemManager.factory:GetSimpleFigureModeFromRenderer()
            local isToggleOn = isPipe and rMode == FacConst.SIMPLE_FIGURE_MODE.SimpleBeltFigure
                or (not isPipe) and rMode == FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure
            self.view.hideToggle.toggle:SetIsOnWithoutNotify(isToggleOn)
            LuaSystemManager.factory:SetSimpleFigureMode(rMode, true)
        else
            local fac = LuaSystemManager.factory
            local wantOn = isPipe and fac.simpleFigureMode == FacConst.SIMPLE_FIGURE_MODE.SimpleBeltFigure
                or (not isPipe) and fac.simpleFigureMode == FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure
            self.view.hideToggle.toggle:SetIsOnWithoutNotify(wantOn)
            self:_OnChangeHideToggle(wantOn)
        end
    else
        self.view.hideToggle.toggle:SetIsOnWithoutNotify(false)

        LuaSystemManager.factory:ClearSimpleFigureEcsOnly()
    end
    self.view.hideToggle.beltIcon.gameObject:SetActive(isPipe)
    self.view.hideToggle.pipeIcon.gameObject:SetActive(not isPipe)

    local showContinueBuild = inTopView and (isBuilding or isLogistic) and DeviceInfo.usingTouch and self.m_buildingNodeId == nil
    self.view.continueBuildToggle.gameObject:SetActive(showContinueBuild)
    self.view.actionButtonsAsIcon.continueBuildHint.gameObject:SetActive(showContinueBuild and self.view.continueBuildToggle.isOn)

    self.view.actionButtonsAsIcon.gameObject:SetActive(inTopView)
    self.view.actionButtonsAsOption.gameObject:SetActive(not inTopView)


    self.view.joystick.gameObject:SetActive(false)

    if not isBuilding and not isBlueprint then
        self.view.moveBuildingHint.gameObject:SetActive(false)
    end

    self.view.exitBeltModeButton.gameObject:SetActive(isBelt and usingDrag)

    if isBlueprint then
        if self.m_buildArgs.isMove then
            self.view.modeName.text = Language.LUA_FAC_BUILD_MODE_TITLE_BATCH_MOVE
        elseif self.m_buildArgs.csBPInst then
            self.view.modeName.text = Language.LUA_FAC_BUILD_MODE_TITLE_BLUEPRINT
        else
            self.view.modeName.text = Language.LUA_FAC_BUILD_MODE_TITLE_BATCH_COPY
        end
    else
        self.view.modeName.text = Language.LUA_FAC_BUILD_MODE_TITLE_NORMAL
    end
    self:_UpdatePendingHint()
    self.view.normalHint.gameObject:SetActive(false)
    self.view.warningHint.gameObject:SetActive(false)

    self.m_sizeIndicator.simpleStateController:SetState(inTopView and "TopView" or "Normal")

    if inTopView then
        GameInstance.mobileMotionManager:PostEventCommonShort()
    end

    InputManagerInst:ToggleGroup(self.m_topViewBuildBindingGroupId, inTopView)
    InputManagerInst:ToggleGroup(self.m_rpgBuildBindingGroupId, not inTopView)
end

FacBuildModeCtrl._UpdatePendingHint = HL.Method() << function(self)
    local isBlueprint = self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint
    local isPending = false
    if isBlueprint and not self.m_buildArgs.isMove then
        local bp = self.m_buildArgs.blueprint or self.m_buildArgs.csBPInst.info.bp or self.m_buildArgs[1]
        local needItems = {}
        for _, entry in pairs(bp.buildingNodes) do
            local itemId = FactoryUtils.getBuildingItemId(entry.templateId)
            if itemId then
                if needItems[itemId] then
                    needItems[itemId] = needItems[itemId] + 1
                else
                    needItems[itemId] = 1
                end
            end
        end
        for itemId, count in pairs(needItems) do
            if Utils.getItemCount(itemId, true, true) < count then
                isPending = true
                break
            end
        end
    end
    self.view.pendingHint.gameObject:SetActive(isPending)
end







local KeyHints = {
    newBuilding = {
        "fac_rotate_device",
        "fac_build_confirm",
        "fac_build_cancel_alter",
    },
    newBuildingInTopView = {
        "fac_rotate_device",
        "fac_build_confirm",
        "fac_build_continuous_confirm",
        "fac_build_cancel_alter",
    },
    oldBuilding = {
        "fac_build_mode_delete",
        "fac_rotate_device",
        "fac_build_confirm",
        "fac_build_cancel_alter",
    },
    blueprint = {
        "fac_rotate_device",
        "fac_build_confirm",
        "fac_build_cancel",
    },
    logistic = {
        "fac_rotate_device",
        "fac_build_confirm",
        "fac_build_cancel_alter",
    },
    logisticInTopView = {
        "fac_rotate_device",
        "fac_build_confirm",
        "fac_build_continuous_confirm",
        "fac_build_cancel_alter",
    },
    beltStart = {
        "fac_build_confirm_belt_start",
        "fac_build_rotate_belt",
        "fac_build_cancel_alter",
    },
    beltEnd = {
        "fac_build_confirm_belt_end",
        "fac_build_rotate_belt",
        "fac_build_cancel_alter",
    },
    pipeStart = {
        "fac_build_confirm_belt_start",
        "fac_build_rotate_pipe",
        "fac_build_cancel_alter",
    },
    pipeEnd = {
        "fac_build_confirm_belt_end",
        "fac_build_rotate_pipe",
        "fac_build_cancel_alter",
    },
    signBuilding = {
        "fac_rotate_device",
        "fac_build_confirm",
        "fac_build_sign_reset",
        "fac_build_cancel_alter",
    },
}

FacBuildModeCtrl.m_keyHintCells = HL.Field(HL.Forward('UIListCache'))

FacBuildModeCtrl._InitKeyHint = HL.Method() << function(self)
    self.m_keyHintCells = self.m_keyHintCells or UIUtils.genCellCache(self.view.keyHintCell)
end

FacBuildModeCtrl._RefreshKeyHint = HL.Method(HL.Opt(HL.Table)) << function(self, keyHint)
    if not keyHint then
        self.m_keyHintCells:Refresh(0)
        return
    end

    if DeviceInfo.usingController and LuaSystemManager.factory.inTopView then

        keyHint = {}
    end

    local count = #keyHint
    local preActionIds, preActionIdCount
    if LuaSystemManager.factory.inTopView then
        preActionIds = DeviceInfo.usingController and FacConst.FAC_TOP_VIEW_BASIC_ACTION_IDS_FOR_CONTROLLER or FacConst.FAC_TOP_VIEW_BASIC_ACTION_IDS
        preActionIdCount = #preActionIds
        count = count + preActionIdCount
    else
        preActionIds = { "cam_zoom_in_ct", }
        preActionIdCount = #preActionIds
        count = count + preActionIdCount
    end

    self.m_keyHintCells:Refresh(count, function(cell, index)
        local actionId
        if preActionIds then
            actionId = preActionIds[index] or keyHint[index - preActionIdCount]
        else
            actionId = keyHint[index]
        end
        if LuaSystemManager.factory.inTopView then
            if actionId == "fac_build_confirm" then
                actionId = "fac_build_confirm_in_top_view"
            elseif actionId == "fac_build_confirm_belt_start" then
                actionId = "fac_build_confirm_belt_start_in_top_view"
            end
        end
        cell.actionKeyHint:SetActionId(actionId)
        cell.gameObject.name = "KeyHint-" .. actionId
    end)
end






FacBuildModeCtrl.m_curBuildIsValid = HL.Field(HL.Boolean) << true

FacBuildModeCtrl._GetBuildingCheckResultHint = HL.Method(HL.Userdata).Return(HL.Boolean, HL.Opt(HL.String)) << function(self, checkResult)
    local valid, hint = checkResult.success, nil

    if not valid then
        if checkResult.skipChapterInvalidInDomain then
            hint = Language.LUA_FAC_BUILD_MODE_SKIP_CHAPTER_INVALID_IN_DOMAIN
        elseif checkResult.busLimited then
            hint = Language.LUA_FAC_BUILD_MODE_ROAD_ATTACH
        elseif checkResult.buildableInChapterLimitedTemplates and checkResult.buildableInChapterLimitedTemplates.Count > 0 then
            local tempId = checkResult.buildableInChapterLimitedTemplates[0].Item1
            local requireMode = checkResult.buildableInChapterLimitedTemplates[0].Item2
            hint = Language.LUA_FAC_BUILD_MODE_CHAPTER_BUILDABLE_LIMIT_DEFAULT
            if not string.isEmpty(tempId) then
                local buildingData = Tables.factoryBuildingTable:GetValue(tempId)
                if buildingData then
                    local displayString = buildingData.name
                    if requireMode ~= nil then
                        local succ, modeData = Tables.FactoryMachineCraftModeTable:TryGetValue(requireMode)
                        if succ then
                            displayString = string.format("%s(%s)", displayString, modeData.machineModeTypeName)
                        end
                    end
                    if self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
                        hint = string.format(Language.LUA_FAC_BUILD_MODE_CHAPTER_BUILDABLE_LIMIT_MULTI_FORMAT, displayString)
                    elseif checkResult.domainModeLimited then
                        hint = string.format(Language.LUA_FAC_BUILD_MODE_CHAPTER_MODE_LIMIT_FORMAT, GameInstance.remoteFactoryManager.currentChapterInfo.data.name, displayString)
                    else
                        local limitChapters = CSFactoryUtil.GetLimitedChapterNames(tempId)
                        local nameCount = limitChapters.Count
                        if nameCount == 1 then
                            hint = string.format(Language.LUA_FAC_BUILD_MODE_CHAPTER_BUILDABLE_LIMIT_FORMAT_1, limitChapters[0])
                        elseif nameCount == 2 then
                            hint = string.format(Language.LUA_FAC_BUILD_MODE_CHAPTER_BUILDABLE_LIMIT_FORMAT_2, limitChapters[0], limitChapters[1])
                        elseif nameCount >= 3 then
                            hint = string.format(Language.LUA_FAC_BUILD_MODE_CHAPTER_BUILDABLE_LIMIT_FORMAT_3, limitChapters[0], limitChapters[1])
                        end
                    end
                end
            end
        elseif checkResult.totalCountLimitedTemplates and checkResult.totalCountLimitedTemplates.Count > 0 then
            local tempId = checkResult.totalCountLimitedTemplates[0]
            local maxPriority = FactoryUtils.evaluateMultiBuildingLimitedPriority(tempId)
            for i = 1, checkResult.totalCountLimitedTemplates.Count - 1 do
                local curTempId = checkResult.totalCountLimitedTemplates[i]
                local priority = FactoryUtils.evaluateMultiBuildingLimitedPriority(curTempId)
                if priority > maxPriority then
                    tempId = curTempId
                    maxPriority = priority
                end
            end
            hint = Language.LUA_FAC_BUILD_MODE_CHAPTER_BUILDING_COUNT_LIMIT_DEFAULT
            if not string.isEmpty(tempId) then
                local buildingData = Tables.factoryBuildingTable:GetValue(tempId)
                if buildingData then
                    if self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
                        hint = string.format(Language.LUA_FAC_BUILD_MODE_CHAPTER_BUILDING_COUNT_LIMIT_MULTI_FORMAT, buildingData.name)
                    else
                        hint = string.format(Language.LUA_FAC_BUILD_MODE_CHAPTER_BUILDING_COUNT_LIMIT_FORMAT, buildingData.name)
                    end
                end
            end
        elseif checkResult.totalCountLimitedBuildingTypes and checkResult.totalCountLimitedBuildingTypes.Count > 0 then
            local buildingType = checkResult.totalCountLimitedBuildingTypes[0]
            local buildingTypeText = Language.LUA_FAC_BATCH_MODE_BUILDING_TYPE_TEXT_DEFAULT
            if buildingType == GEnums.FacBuildingType.Decorate:GetHashCode() then
                buildingTypeText = Language.LUA_FAC_BATCH_MODE_BUILDING_TYPE_TEXT_DECORATE
            end
            hint = string.format(Language.LUA_FAC_BUILD_MODE_BUILDING_TYPE_COUNT_LIMITED_FORMAT, buildingTypeText)
        elseif checkResult.bandwidthLimited then
            hint = Language.LUA_FAC_BUILD_MODE_ON_BANDWIDTH_MAX
        elseif checkResult.mainRegionLimited then
            if GameInstance.remoteFactoryManager.interact.isInBlueprintMode then
                hint = Language.LUA_FAC_BUILD_MODE_MAIN_REGION_LIMITED_BLUEPRINT
            else
                hint = Language.LUA_FAC_BUILD_MODE_MAIN_REGION_LIMITED
            end
        elseif checkResult.cropAreaLimited then
            hint = Language.LUA_FAC_BUILD_MODE_ON_CROP_AREA_ONLY
        elseif checkResult.cropCntLimited then
            hint = Language.LUA_FAC_BUILD_MODE_ON_CROP_COUNT_LIMITED
        elseif checkResult.travelPoleCountLimited then
            hint = Language.LUA_FAC_BUILD_MODE_ON_TRAVEL_POLE_COUNT_MAX
        elseif checkResult.battleCountLimited then
            hint = Language.LUA_FAC_BUILD_MODE_ON_BATTLE_COUNT_MAX
        elseif checkResult.mineLimited then
            hint = Language.LUA_FAC_BUILD_MODE_ON_MINE_ONLY
        elseif checkResult.mineTypeLimited then
            hint = Language.LUA_FAC_BUILD_MODE_ON_WRONG_MINE_TYPE
        elseif checkResult.mineLevelLimited then
            hint = Language.LUA_FAC_BUILD_MODE_MINE_LEVEL_LIMITED
        elseif checkResult.buildableRiftLimited then
            hint = Language.LUA_FAC_BUILD_MODE_IN_RIFT_LIMITED
        elseif checkResult.buildableWaterLimited then
            hint = Language.LUA_FAC_BUILD_MODE_IN_WATER_LIMITED
        elseif checkResult.buildableLimited then
            hint = Language.LUA_FAC_BUILD_MODE_IN_BUILDABLE_RANGE_BUT_HAS_INVALID_GRID
        elseif checkResult.occludedByArea then
            hint = Language.LUA_FAC_BUILD_MODE_OCCLUDED_BY_AREA
        elseif checkResult.crossDivisionBoundary then
            hint = Language.LUA_FAC_BUILD_MODE_CROSS_DIVISION_BOUNDARY
        elseif checkResult.overlayWithBlueprintNode
            or (checkResult.overlayNodes and checkResult.overlayNodes.Count > 0)
            or (checkResult.overlayPendingNodes and checkResult.overlayPendingNodes.Count > 0) then
            hint = Language.LUA_FAC_BUILD_MODE_BUILDING_OVERLAP
        elseif checkResult.outRanged then
            hint = Language.LUA_FAC_BUILD_MODE_OUT_OF_RANGE
        elseif checkResult.outOfHeight then
            hint = Language.LUA_FAC_BUILD_MODE_HEIGHT_OVER_TOOMUCH
        elseif checkResult.groundTooUneven then
            hint = Language.LUA_FAC_BUILD_MODE_GROUND_TOO_UNEVEN
        elseif checkResult.collideWithMap then
            hint = Language.LUA_FAC_BUILD_MODE_SPACE_HEIGHT_NOT_ENOUGH
        elseif checkResult.blockedByInter then
            hint = Language.LUA_FAC_BUILD_MODE_BLOCK_BY_DYNAMIC_ENTITY
        elseif checkResult.blockedByLoot then
            hint = Language.LUA_FAC_BUILD_MODE_BLOCK_BY_LOOT
        elseif checkResult.blockedByErosion then
            hint = Language.LUA_FAC_BUILD_MODE_BLOCK_WITH_EROSION
        elseif checkResult.pumpReachLiquidLimited then
            hint = Language.LUA_FAC_BUILD_MODE_PUMP_MUST_REACH_LIQUID
        elseif checkResult.pumpLiquidTypeNotMatched then
            local currentBuildingMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
            local liquidItemId = currentBuildingMode and currentBuildingMode.pumpingLiquidItemId or nil
            if self.m_buildingId and not self.m_buildingId:isEmpty() and liquidItemId and not string.isEmpty(liquidItemId) then
                local buildingData = GameInstance.remoteFactoryManager.staticData:QueryBuildingData(self.m_buildingId)
                local itemData = Tables.itemTable[liquidItemId]
                if buildingData and itemData then
                    hint = string.format(Language.LUA_FAC_BUILD_MODE_PUMP_LIQUID_TYPE_NOT_MATCH_FORMAT, buildingData.name, itemData.name)
                else
                    hint = Language.LUA_FAC_BUILD_MODE_PUMP_LIQUID_TYPE_NOT_MATCH_DEFAULT
                end
            else
                hint = Language.LUA_FAC_BUILD_MODE_PUMP_LIQUID_TYPE_NOT_MATCH_DEFAULT
            end
        elseif checkResult.dumpReachLiquidLimited then
            if GameInstance.remoteFactoryManager.system.core.progressStatus.riftFilling.enableRiftFilling then
                hint = Language.LUA_FAC_BUILD_MODE_DUMP_MUST_REACH_LIQUID_0N_RIFT_FILLING_UNLOCK
            else
                hint = Language.LUA_FAC_BUILD_MODE_DUMP_MUST_REACH_LIQUID
            end
        elseif checkResult.riftFillingNotEnabled then
            hint = Language.LUA_FAC_BUILD_MODE_RIFT_FILLING_NOT_ENABLED
        elseif checkResult.riftIsNotSuppressedYet then
            hint = Language.LUA_FAC_BUILD_MODE_RIFT_IS_NOT_SUPPRESSED_YET
        elseif checkResult.noSoilForWaterSpray then
            hint = Language.LUA_FAC_BUILD_MODE_NO_SOIL_IN_SPRAY
        elseif checkResult.moveAcrossScene then
            hint = Language.LUA_FAC_BUILD_MODE_MOVE_ACROSS_SCENE
        elseif checkResult.notAlongLogistic then
            hint = Language.LUA_FAC_BUILD_MODE_NOT_ALONG_LOGISTIC_DIRECTION
        elseif checkResult.notPutOnConveyor then
            if self.m_buildingId and not self.m_buildingId:isEmpty() then
                if Tables.factoryFluidValveTable:ContainsKey(self.m_buildingId) then
                    hint = Language.LUA_FAC_BUILD_MODE_VALVE_NOT_ON_CONVEYOR_FLUID
                end
            end
            if not hint then
                hint = Language.LUA_FAC_BUILD_MODE_VALVE_NOT_ON_CONVEYOR_BOX
            end
        elseif checkResult.minDistanceLimitNodes and checkResult.minDistanceLimitNodes.Count > 0 then
            hint = Language.LUA_FAC_BUILD_MODE_MIN_DISTANCE_LIMIT
        elseif checkResult.medicRangeOverlap then
            hint = Language.LUA_FAC_BUILD_MODE_MEDIC_RANGE_OVERLAP
        elseif checkResult.overlayMineIndex and checkResult.overlayMineIndex.Count > 0 then
            hint = Language.LUA_FAC_BUILD_MODE_MINE_OVERLAP
        elseif checkResult.freeBusNoConnectSource then
            hint = Language.LUA_FAC_BUILD_MODE_FREE_BUS_NO_CONNECTION_TO_START
        else
            hint = Language.LUA_FAC_BUILD_MODE_OTHERS
        end
    end
    return valid, hint
end

FacBuildModeCtrl._GetConveyorCheckResultHint = HL.Method(HL.Userdata, HL.Boolean).Return(HL.Boolean, HL.Opt(HL.String)) << function(self, checkResult, isPipe)
    local valid, hint = checkResult.success, nil

    if not valid then
        if checkResult.mainRegionLimited then
            if GameInstance.remoteFactoryManager.interact.isInBlueprintMode then
                hint = Language.LUA_FAC_BUILD_MODE_MAIN_REGION_LIMITED_BLUEPRINT
            else
                hint = Language.LUA_FAC_BUILD_MODE_MAIN_REGION_LIMITED
            end
        elseif checkResult.buildableLimited then
            hint = Language.LUA_FAC_BUILD_MODE_IN_BUILDABLE_RANGE_BUT_HAS_INVALID_GRID
        elseif checkResult.occludedByArea then
            hint = Language.LUA_FAC_BUILD_MODE_OCCLUDED_BY_AREA
        elseif checkResult.linkingNodeInvalidPlaced then
            hint = isPipe and Language.LUA_FAC_BUILD_MODE_PIPE_LINK_INVALID_PLACED_NODE or Language.LUA_FAC_BUILD_MODE_BELT_LINK_INVALID_PLACED_NODE
        elseif checkResult.hasSelfOverlay then
            hint = isPipe and Language.LUA_FAC_BUILD_MODE_PIPE_SELF_OVERLAP or Language.LUA_FAC_BUILD_MODE_BELT_SELF_OVERLAP
        elseif checkResult.pipeAngleLimited then
            hint = Language.LUA_FAC_BUILD_MODE_PIPE_ANGLE_LIMITED
        elseif (checkResult.overlayNodes and checkResult.overlayNodes.Count > 0)
            or (checkResult.overlayPendingNodes and checkResult.overlayPendingNodes.Count > 0) then
            hint = Language.LUA_FAC_BUILD_MODE_BUILDING_OVERLAP
        elseif checkResult.directionConflictLogisticUnits and checkResult.directionConflictLogisticUnits.Count > 0 then
            hint = isPipe and Language.LUA_FAC_BUILD_MODE_DIRECTION_CONFLICT_LOGISTIC_UNIT_PIPE or Language.LUA_FAC_BUILD_MODE_DIRECTION_CONFLICT_LOGISTIC_UNIT_BELT
        elseif checkResult.overLengthLimit then
            hint = isPipe and Language.LUA_FAC_BUILD_MODE_PIPE_OVER_LENGTH_LIMIT or Language.LUA_FAC_BUILD_MODE_BELT_OVER_LENGTH_LIMIT
        elseif checkResult.shapeInvalid then
            hint = isPipe and Language.LUA_FAC_BUILD_MODE_PIPE_SHAPE_INVALID or Language.LUA_FAC_BUILD_MODE_BELT_SHAPE_INVALID
        elseif checkResult.pipeStartModeLimited then
            hint = Language.LUA_FAC_BUILD_MODE_START_NODE_PIPE_MODE_LIMITED
        elseif checkResult.pipeEndModeLimited then
            hint = Language.LUA_FAC_BUILD_MODE_END_NODE_PIPE_MODE_LIMITED
        elseif checkResult.pipeStartPortLimited then
            hint = Language.LUA_FAC_BUILD_MODE_START_NODE_PIPE_PORT_LIMITED
        elseif checkResult.pipeEndPortLimited then
            hint = Language.LUA_FAC_BUILD_MODE_END_NODE_PIPE_PORT_LIMITED
        elseif checkResult.overlayMineIndex and checkResult.overlayMineIndex.Count > 0 then
            hint = Language.LUA_FAC_BUILD_MODE_MINE_OVERLAP
        elseif checkResult.domainModeLimited then
            local pipeData = Tables.factoryLiquidPipeTable:GetValue("log_pipe_01")
            local buildingName = pipeData and pipeData.pipeData.name or ""
            hint = string.format(
                Language.LUA_FAC_BUILD_MODE_CHAPTER_MODE_LIMIT_FORMAT,
                GameInstance.remoteFactoryManager.currentChapterInfo.data.name, buildingName
            )
        else
            hint = Language.LUA_FAC_BUILD_MODE_OTHERS
        end
    end
    return valid, hint
end

FacBuildModeCtrl._UpdateValidResult = HL.Method() << function(self)
    if self.m_mode == FacConst.FAC_BUILD_MODE.Normal then
        return
    end

    local inDragMode = self:_InDragMode()
    local valid, errorHint, moving = false, nil, false
    local actionHint, warningHint, normalHint
    local actionColor = "Black"
    if self.m_mode == FacConst.FAC_BUILD_MODE.Building or self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        local rst = GameInstance.remoteFactoryManager.interact.currentBuildingMode.addBuildingCheckResult
        valid, errorHint = self:_GetBuildingCheckResultHint(rst)
        if valid then
            local pos, rot = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
            CS.Beyond.Gameplay.Conditions.OnFacPrepareBuildingEnterArea.Trigger(self.m_buildingId, pos, rot.y)

            local buildingMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
            local autoConnect = buildingMode.autoConnectCandidateList
            local inFac = buildingMode.inMainRegion or false
            if autoConnect ~= nil and autoConnect.Count > 0 then
                local buildingData
                if self.m_buildingId and not string.isEmpty(self.m_buildingId) then
                    buildingData = GameInstance.remoteFactoryManager.staticData:QueryBuildingData(buildingMode.buildingId)
                end
                if
                    buildingData and
                    buildingData.type ~= GEnums.FacBuildingType.Hub:GetHashCode() and
                    buildingData.type ~= GEnums.FacBuildingType.SubHub:GetHashCode()
                then
                    actionHint = inFac and Language.LUA_POWER_AUTOCONNECT_INFAC_HINT or Language.LUA_POWER_AUTOCONNECT_BUILD_MODE_HINT
                end
            elseif FacConst.POLE_RANGE_EFFECT_MAP[self.m_buildingId] and inFac then
                actionHint = Language.LUA_POWER_AUTOCONNECT_INFAC_HINT
            end

            if not self:_CheckSignCanBuild() then
                actionHint = Language.LUA_MARKER_BUILD_COUNT_MAX
            end
            local envRel = buildingMode.envRelationInfo
            if envRel:NotFullyCovered() then
                if envRel:NotFullyCoveredPartial() then
                    actionHint = Language.LUA_BUILDING_ENV_NOT_FULLY_COVERED_BUT_PARTIAL
                    actionColor = "Yellow"
                end
            elseif envRel:FullyCovered() then
                local env = envRel:FullyCoveredEnv()
                local envDisplayConfig = FacConst.FAC_ENV_DISPLAY_CONFIG[env]
                local envText = Language.LUA_BUILDING_ENV_NONE
                if envDisplayConfig.textKey then
                    local envI18nResult, envI18nText = CS.Beyond.I18n.I18nUtils.TryGetText(envDisplayConfig.textKey)
                    if envI18nResult then
                        envText = envI18nText
                    end
                end
                actionHint = string.format(Language.LUA_BUILDING_ENV_FULLY_COVERED, envDisplayConfig.icon, envDisplayConfig.color, envText)
            end
        end
        moving = GameInstance.remoteFactoryManager.interact.currentBuildingMode.isMoving
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        local conveyorMode = GameInstance.remoteFactoryManager.interact.currentConveyorMode
        local rst = conveyorMode.checkResult
        local isPipe = self:_IsPipe()
        valid = conveyorMode.hasStart or conveyorMode.hasPrecalculatedPort
        if not valid then
            if not inDragMode then
                if conveyorMode.usePipePreview then
                    errorHint = Language.LUA_FAC_BUILD_MODE_PIPE_CANNOT_FROM_EMPTY
                else
                    errorHint = Language.LUA_FAC_BUILD_MODE_CONVEYOR_CANNOT_FROM_EMPTY
                end
            end
        end
        if valid then
            valid, errorHint = self:_GetConveyorCheckResultHint(rst, isPipe)
        end
        if valid then
            valid, errorHint = self:_GetBuildingCheckResultHint(GameInstance.remoteFactoryManager.interact.currentConveyorMode.additionalBuildingCheckResult)
        end
        if valid then
            local hasEnd, endPos
            if conveyorMode.hasStart then
                hasEnd, endPos = conveyorMode:GetEndPosVector3()
            end
            if hasEnd then
                endPos = endPos + Vector3(0.5, 0, 0.5)
                local endPos2 = (isPipe and self.m_pipePreviewMark or self.m_beltStartPreviewMark).transform.position
                if (endPos - endPos2):XZ().sqrMagnitude <= 0.1 then


                    CS.Beyond.Gameplay.Conditions.OnFacPrepareBuildingEnterArea.Trigger(self.m_beltId, endPos, 0)
                end
            else
                local mark = isPipe and self.m_pipePreviewMark or self.m_beltStartPreviewMark
                endPos = mark.transform.position
                CS.Beyond.Gameplay.Conditions.OnFacPrepareBuildingEnterArea.Trigger(self.m_beltId, endPos, 0)
            end
        end
        if valid and errorHint == nil then
            if conveyorMode:HasMismatchedEndPort() then
                if conveyorMode.usePipePreview then
                    warningHint = Language.LUA_FAC_BUILD_MODE_PIPE_END_PORT_REVERSE
                else
                    warningHint = Language.LUA_FAC_BUILD_MODE_CONVEYOR_END_PORT_REVERSE
                end
            elseif conveyorMode.hasStart then

                if not conveyorMode.endNodeHandler and not conveyorMode:HasEndJointNode() then
                    normalHint = isPipe and Language.LUA_FAC_BUILD_MODE_PIPE_END_IS_EMPTY or Language.LUA_FAC_BUILD_MODE_BELT_END_IS_EMPTY
                end
            end
        end
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        local curMode = GameInstance.remoteFactoryManager.interact.currentBlueprintMode
        local buildingRst = curMode.addBuildingCheckResult
        valid, errorHint = self:_GetBuildingCheckResultHint(buildingRst)
        if valid then
            local conveyorRst = curMode.addConveyorCheckResult
            valid, errorHint = self:_GetConveyorCheckResultHint(conveyorRst, false)
        end
        if valid then
            local pipeRst = curMode.addPipeCheckResult
            valid, errorHint = self:_GetConveyorCheckResultHint(pipeRst, true)
        end
        moving = curMode.isMoving
        if valid and self.m_buildArgs.isSystemBP then
            local dir = curMode.preparedDirection * 90
            local pos = Vector3(curMode.preparedPosition.x, curMode.preparedPosition.y, curMode.preparedPosition.z)
            CS.Beyond.Gameplay.Conditions.OnFacPrepareBuildingEnterArea.Trigger(self.m_buildArgs.sysBpKey, pos, dir)
        end
    end
    if errorHint then
        if not self.view.errorHint.gameObject.activeInHierarchy then
            UIUtils.PlayAnimationAndToggleActive(self.view.errorHint, true)
        end
        self.view.errorHintText:SetAndResolveTextStyle(errorHint)
    else
        if self.view.errorHint.gameObject.activeInHierarchy and self.view.errorHint.curState ~= UIConst.UI_ANIMATION_WRAPPER_STATE.Out then
            UIUtils.PlayAnimationAndToggleActive(self.view.errorHint, false)
        end
    end
    if inDragMode and self.m_mode == FacConst.FAC_BUILD_MODE.Belt then

        self.view.actionHint.gameObject:SetActive(errorHint == nil)
    else
        if actionHint then
            if not self.view.actionHint.gameObject.activeInHierarchy then
                UIUtils.PlayAnimationAndToggleActive(self.view.actionHint, true)
            end
            self.view.actionHintTxt:SetAndResolveTextStyle(actionHint)
            self.view.actionColorCtrl:SetState(actionColor)
        else
            if self.view.actionHint.gameObject.activeInHierarchy and self.view.actionHint.curState ~= UIConst.UI_ANIMATION_WRAPPER_STATE.Out then
                UIUtils.PlayAnimationAndToggleActive(self.view.actionHint, false)
            end
        end
    end
    if warningHint then
        if not self.view.warningHint.gameObject.activeInHierarchy then
            UIUtils.PlayAnimationAndToggleActive(self.view.warningHint, true)
        end
        self.view.warningHintTxt:SetAndResolveTextStyle(warningHint)
    else
        if self.view.warningHint.gameObject.activeInHierarchy and self.view.warningHint.curState ~= UIConst.UI_ANIMATION_WRAPPER_STATE.Out then
            UIUtils.PlayAnimationAndToggleActive(self.view.warningHint, false)
        end
    end
    if normalHint then
        if not self.view.normalHint.gameObject.activeInHierarchy then
            UIUtils.PlayAnimationAndToggleActive(self.view.normalHint, true)
        end
        self.view.normalHintTxt:SetAndResolveTextStyle(normalHint)
    else
        if self.view.normalHint.gameObject.activeInHierarchy and self.view.normalHint.curState ~= UIConst.UI_ANIMATION_WRAPPER_STATE.Out then
            UIUtils.PlayAnimationAndToggleActive(self.view.normalHint, false)
        end
    end
    self.m_curBuildIsValid = valid
    GameInstance.remoteFactoryManager:SetPreviewUnitState(valid, moving)
end

FacBuildModeCtrl._UpdateAutoConnectExtraHint = HL.Method() << function(self)

    local buildingMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
    if not buildingMode then
        return
    end
    local notInPower, noPower = false, false
    if FacConst.NEED_AUTO_CONNECT_EXTRA_HINT_BUILDING[self.m_buildingId] then
        local autoConnect = buildingMode.autoConnectCandidateList
        if autoConnect then
            notInPower = true
            local status = CS.Beyond.Gameplay.Factory.PowerAutoConnectStatus
            for i = 0, autoConnect.Count - 1 do
                local info = autoConnect[i]
                if info.Item5 == status.Outage then
                    noPower = true
                    notInPower = false
                    break
                elseif info.Item5 ~= status.LocalLink and info.Item5 ~= status.DistLimit then
                    notInPower = false
                end
            end
        end
    end
    local udpipeKeepConnect, udpipeNoConnect = false, false
    if buildingMode.isMoving then
        local udpipeConnectEntry = buildingMode.udpipeConnectEntry
        if udpipeConnectEntry then
            udpipeNoConnect = udpipeConnectEntry.outOfRange
            udpipeKeepConnect = not udpipeNoConnect
        end
    end

    local moving = buildingMode.isMoving
    self.view.moveBuildingHint.gameObject:SetActiveIfNecessary(moving or notInPower or noPower or udpipeNoConnect or udpipeKeepConnect)
    self.view.rebuildInfoTxt.gameObject:SetActiveIfNecessary(moving)
    self.view.noPowerTxt.gameObject:SetActiveIfNecessary(notInPower)
    self.view.noElectricity.gameObject:SetActiveIfNecessary(noPower)
    if self.view.keepConnected then
        self.view.keepConnected.gameObject:SetActiveIfNecessary(udpipeKeepConnect)
    end
    if self.view.noConnected then
        self.view.noConnected.gameObject:SetActiveIfNecessary(udpipeNoConnect)
    end
end






FacBuildModeCtrl.GetRecoverBuildStateOnChangeDevice = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    local interact = GameInstance.remoteFactoryManager and GameInstance.remoteFactoryManager.interact
    if not interact then
        return nil
    end
    if self.m_mode == FacConst.FAC_BUILD_MODE.Building or self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        local curMode = interact.currentBuildingMode
        if not curMode then
            return nil
        end
        return {
            initDir = curMode.finalBuildingDirection,
            controllerMouseWorldPos = curMode:GetPreviewRenderInfo(),
        }
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        local curMode = interact.currentBlueprintMode
        if not curMode then
            return nil
        end
        return {
            initDir = curMode.preparedDirection,
            controllerMouseWorldPos = Vector3(curMode.preparedPosition.x, curMode.preparedPosition.y, curMode.preparedPosition.z),
        }
    end
    return nil
end

FacBuildModeCtrl._EnterBuildingMode = HL.Method(HL.Table) << function(self, args)
    if self.m_mode == FacConst.FAC_BUILD_MODE.Building then
        if self.m_buildArgs.onExit then
            self.m_buildArgs.onExit()
        end
        self:_SetCamState()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        self:_ExitLogisticMode()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        self:_ExitBeltMode()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        self:_ExitBlueprintMode()
    end

    self.m_isDragging = false
    UIManager.commonTouchPanel.enableZoom = true


    self.m_buildArgs = args
    local buildingId, itemData
    local nodeId = args.nodeId

    local initGridPos = args.initGridPos
    if initGridPos then
        initGridPos = Unity.Vector3Int(math.floor(initGridPos.x), math.floor(initGridPos.y), math.floor(initGridPos.z))
    else
        local mousePos
        if args.initMousePos then
            mousePos = Vector3(args.initMousePos.x, args.initMousePos.y, 0)
        else
            mousePos = self:_InDragMode() and Vector3(Screen.width / 2, Screen.height / 2, 0) or self:_GetCurPointerPressPos()
        end

        local camRay = CameraManager.mainCamera:ScreenPointToRay(mousePos)
        local _, initWorldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(camRay)
        initGridPos = GameInstance.remoteFactoryManager.visual:WorldToVoxel(initWorldPos)
        initGridPos = Unity.Vector3Int(math.floor(initGridPos.x), math.floor(initGridPos.y), math.floor(initGridPos.z))
    end

    if nodeId then

        self.m_buildingNodeId = nodeId
        local node = FactoryUtils.getBuildingNodeHandler(nodeId)
        buildingId = node.templateId
        itemData = FactoryUtils.getBuildingItemData(buildingId)
        self:_RefreshKeyHint(KeyHints.oldBuilding)
        local face = args.initDir or lume.round(node.transform.direction.y / 90)
        if not args.initMousePos and DeviceInfo.usingTouch and LuaSystemManager.factory.inTopView then


            local bData = Tables.factoryBuildingTable[buildingId]
            local rectInt = CS.UnityEngine.RectInt(bData.range.x, bData.range.z, bData.range.width, bData.range.depth)
            local offset = CS.Beyond.Gameplay.RemoteFactory.GridMath.CenterMatchOffset(rectInt, face)
            initGridPos = node.transform.position - Unity.Vector3Int(offset.x, 0, offset.y)
        end
        GameInstance.remoteFactoryManager.interact:SwitchToBuildingModeAsMove(nodeId, initGridPos, face)
        self.view.moveBuildingHint.gameObject:SetActive(true)
    else

        self.m_buildingNodeId = nil
        local itemId = args.itemId
        itemData = Tables.itemTable[itemId]
        local buildingItemData = Tables.factoryBuildingItemTable[itemId]
        buildingId = buildingItemData.buildingId
        self:_RefreshKeyHint(LuaSystemManager.factory.inTopView and KeyHints.newBuildingInTopView or KeyHints.newBuilding)
        local initDir = args.initDir
        if initDir == nil then
            initDir = math.floor(((CameraManager.mainCamera.transform.eulerAngles.y + 45) % 360) / 90) % 4
        end

        if args.initDir == nil and FacConst.SIGN_BUILDING_EXTRA_SETTING_PANEL[buildingId] then
            initDir = (initDir + 1) % 4
        end
        GameInstance.remoteFactoryManager.interact:SwitchToBuildingMode(buildingId, initGridPos, initDir)
        self.view.moveBuildingHint.gameObject:SetActive(false)
        local mode = GameInstance.remoteFactoryManager.interact.buildingMode
        mode.onPlaceBuilding = mode.onPlaceBuilding - self.m_onPlaceFinish
        mode.onPlaceBuilding = mode.onPlaceBuilding + self.m_onPlaceFinish
    end
    self.m_buildingId = buildingId
    self.m_itemData = itemData

    self:_UpdateValidResult()
    self:_UpdateAutoConnectExtraHint()

    self.m_mode = FacConst.FAC_BUILD_MODE.Building
    local bData = Tables.factoryBuildingTable[self.m_buildingId]

    do
        if self.m_workingRange then
            if self.m_workingRange.gameObject then
                GameObject.Destroy(self.m_workingRange.gameObject)
            end
            self.m_workingRange = nil
        end
        local effectPath = nil
        if bData then
            if bData.type == GEnums.FacBuildingType.PowerPole or bData.type == GEnums.FacBuildingType.PowerDiffuser then
                effectPath = FacConst.POLE_RANGE_EFFECT_MAP[self.m_buildingId]
            elseif bData.type == GEnums.FacBuildingType.EnvGenWithActivator then
                effectPath = FacConst.VAPORIZER_RANGE_EFFECT
            end
        end
        if effectPath then
            local prefab = self.loader:LoadGameObject(effectPath)
            local obj = self:_CreateWorldGameObject(prefab)
            self.m_workingRange = Utils.wrapLuaNode(obj)
            obj.gameObject:SetActive(false)
        end
    end

    do
        if self.m_battleRange then
            if self.m_battleRange.gameObject then
                GameObject.Destroy(self.m_battleRange.gameObject)
            end
            self.m_battleRange = nil
        end
        local battleData = GameInstance.remoteFactoryManager.staticData:QueryBattleData(self.m_buildingId)
        if battleData then
            local effectPath = string.format(FacConst.EFFECT_PREFAB_PATH_FORMAT, battleData.attackRangeEffect)
            local prefab = self.loader:LoadGameObject(effectPath)
            local obj = self:_CreateWorldGameObject(prefab)
            self.m_battleRange = Utils.wrapLuaNode(obj)
            obj.gameObject:SetActive(false)
        end
    end
    self:_UpdateBuildingFollowerState(true)

    self:_SetCamState(bData.buildCamState)

    Notify(MessageConst.ON_BUILD_MODE_CHANGE, self.m_mode)
    Notify(MessageConst.ON_ENTER_BUILDING_MODE, itemData.id)
    self:_NotifyPowerPoleTravelHint()
    self:_UpdateCommonNodesOnEnterMode()

    if not self:_InDragMode() then


        self:_Tick()
    end

    self:_ProcessSignBuildSetting()
end

FacBuildModeCtrl._ExitBuildingMode = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    if self.m_mode ~= FacConst.FAC_BUILD_MODE.Building then
        return
    end

    self.m_sizeIndicator.followerObject.getTargetPosInfo = nil
    self.m_sizeIndicator.gameObject:SetActiveIfNecessary(false)
    if self.m_workingRange then
        self.m_workingRange.followerObject.getTargetPosInfo = nil
        if self.m_workingRange.gameObject then
            GameObject.Destroy(self.m_workingRange.gameObject)
        end
        self.m_workingRange = nil
    end
    if self.m_battleRange then
        self.m_battleRange.followerObject.getTargetPosInfo = nil
        if self.m_battleRange.gameObject then
            GameObject.Destroy(self.m_battleRange.gameObject)
        end
        self.m_battleRange = nil
    end
    self.m_fluidSprayRange.followerObject.getTargetPosInfo = nil
    self.m_fluidSprayRange.gameObject:SetActive(false)
    self:_ResetMoveBuildingHintState()

    self:_ExitMode(skipAnim)
end

FacBuildModeCtrl._ResetMoveBuildingHintState = HL.Method() << function(self)
    self.view.moveBuildingHint.gameObject:SetActiveIfNecessary(false)
    self.view.rebuildInfoTxt.gameObject:SetActive(true)
    self.view.noPowerTxt.gameObject:SetActive(false)
    self.view.noElectricity.gameObject:SetActive(false)
    if self.view.keepConnected then
        self.view.keepConnected.gameObject:SetActiveIfNecessary(false)
    end
    if self.view.noConnected then
        self.view.noConnected.gameObject:SetActiveIfNecessary(false)
    end
end


FacBuildModeCtrl._TryExitLinkWireOnBuildingMove = HL.Method(HL.Any) << function(self, moveTarget)
    local linkBrain = GameWorld.gameMechManager.linkWireBrain
    if not linkBrain.isLinking then
        return
    end
    local sourceNodeId = linkBrain.currentLinkSourceNodeId
    if sourceNodeId == 0 then
        return
    end
    if (type(moveTarget) == "number" and sourceNodeId == moveTarget)
        or (type(moveTarget) == "table" and moveTarget[sourceNodeId]) then
        linkBrain:EndLinkWithCancel(true)
    end
end

FacBuildModeCtrl._NotifyPowerPoleTravelHint = HL.Method() << function(self)
    if self.m_mode ~= FacConst.FAC_BUILD_MODE.Building then
        return
    end

    local pos, rot = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
    if not self.m_buildingNodeId then
        Notify(MessageConst.ON_BUILD_POWER_POLE_TRAVEL_HINT, {
            buildingTypeId = self.m_buildingId,
            position = pos
        })
    else
        Notify(MessageConst.ON_MOVE_POWER_POLE_TRAVEL_HINT, {
            buildingTypeId = self.m_buildingId,
            position = pos,
            nodeId = self.m_buildingNodeId,
        })
    end
end

FacBuildModeCtrl._ConfirmBuilding = HL.Method() << function(self)
    if not self:_CheckSignCanBuild() then
        self.m_lockBuildPos = true
        UIManager:AutoOpen(PanelId.FacMarkerManagePopup, {
            onClose = function()
                self.m_lockBuildPos = false
            end
        })
        return
    end

    self:_Tick()
    if not self.m_curBuildIsValid then
        Notify(MessageConst.SHOW_TOAST, self.view.errorHintText.text)
        AudioAdapter.PostEvent("au_ui_fac_unbuildable")
        return
    end

    local buildingItemId = self.m_itemData.id
    local mousePos = self:_GetCurPointerPressPos()
    GameInstance.remoteFactoryManager:GridPositionTriggered(mousePos, 0)
    local isNewBuilding = self.m_buildingNodeId == nil

    if isNewBuilding then
        if self:_EnableContinueBuild() then
            local count = Utils.getItemCount(buildingItemId)
            if count <= 1 then
                self:_ExitBuildingMode()
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BUILD_CONTINUOUS_STOPPED)
            end
        else
            self:_ExitBuildingMode()
        end
    else
        self:_ExitBuildingMode()
        AudioAdapter.PostEvent("au_ui_fac_building_move_set")
    end
end

FacBuildModeCtrl._CancelBuilding = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    local onCancel = self.m_buildArgs.onCancel
    if onCancel then
        onCancel()
    end

    self:_ExitBuildingMode(skipAnim)
end

FacBuildModeCtrl._DelBuilding = HL.Method() << function(self)
    if not self:_CanDelBuilding() then
        return
    end

    GameInstance.player.remoteFactory.core:Message_OpDismantle(Utils.getCurrentChapterId(), self.m_buildingNodeId, false, function()
        self:_ExitBuildingMode()
    end)
end

FacBuildModeCtrl._CanDelBuilding = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_mode ~= FacConst.FAC_BUILD_MODE.Building then
        return false
    end

    local nodeId = self.m_buildingNodeId
    if not nodeId then
        return false
    end

    return FactoryUtils.canDelBuilding(self.m_buildingNodeId)
end

FacBuildModeCtrl._RotateUnit = HL.Method() << function(self)
    if not self:_CanRotate() then
        return
    end
    GameInstance.remoteFactoryManager.interact:InputKeyRotation(true)
    self:_UpdateBuildingFollowerState(false)
    if DeviceInfo.usingKeyboard then
        self:_UpdateAutoMoveTopViewCamState()
    end
    AudioAdapter.PostEvent("au_ui_building_turn")
    if self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        self:_SetBluePrintSelectGrids(self.m_lastBPGridPos)
    end
end

FacBuildModeCtrl._CanRotate = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_mode == FacConst.FAC_BUILD_MODE.Normal then
        return false
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        if FacConst.FAC_VALVE_NODE_IDS[self.m_buildingId] then
            return false
        end
    end
    return true
end

FacBuildModeCtrl._UpdateAutoMoveTopViewCamState = HL.Method(HL.Opt(HL.Any)) << function(self, extraPadding)
    if not LuaSystemManager.factory.inTopView then
        return
    end
    local size
    if self.m_mode == FacConst.FAC_BUILD_MODE.Building then
        size = Vector2.one








    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        size = Vector2.one
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        size = Vector2.one
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        size = Vector2.one







    end
    LuaSystemManager.factory:ToggleAutoMoveTopViewCam(size, extraPadding)
end

FacBuildModeCtrl._UpdateBuildingFollowerState = HL.Method(HL.Boolean) << function(self, isInit)
    if self.m_mode ~= FacConst.FAC_BUILD_MODE.Building then
        return
    end

    local data = Tables.factoryBuildingTable:GetValue(self.m_buildingId)
    self.m_sizeIndicator.gameObject:SetActiveIfNecessary(true)
    self.m_sizeIndicator.followerObject.getTargetPosInfo = function(pos, rot)
        pos, rot = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
        return pos, rot
    end
    local scale = Vector3(data.range.width, data.modelHeight, data.range.depth)
    local reverseScale = Vector3(1 / scale.x, 1 / scale.y, 1 / scale.z)
    self.m_sizeIndicator.transform.localScale = scale
    self.m_sizeIndicator.transform:DoActionOnChildren(function(childTrans)
        childTrans.localScale = reverseScale
    end)

    if isInit then
        if self.m_workingRange then
            local extSizeW = 0
            local extSizeH = 0
            local powerPoleData = GameInstance.remoteFactoryManager.staticData:QueryPowerPoleData(self.m_buildingId)
            if powerPoleData and GameInstance.remoteFactoryManager.powerPoleConditionQuery:QueryDiffuserEnabled(powerPoleData) then
                local poleData = Tables.factoryPowerPoleTable[self.m_buildingId]
                extSizeW = poleData.rangeExtend.x
                extSizeH = poleData.rangeExtend.z
            else
                local vaporizerData = GameInstance.remoteFactoryManager.staticData:QueryVaporizerData(self.m_buildingId)
                if vaporizerData then
                    extSizeW = vaporizerData.rangeExtend.x
                    extSizeH = vaporizerData.rangeExtend.z
                end
            end
            if extSizeW > 0 or extSizeH > 0 then
                self.m_workingRange.gameObject:SetActive(true)
                self.m_workingRange.followerObject.getTargetPosInfo = function(pos, rot)
                    pos, rot = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
                    return pos, rot
                end
            end
        end
    end

    if isInit and data.type == GEnums.FacBuildingType.FluidSpray then
        local fluidSprayData = Tables.factoryFluidSprayTable[self.m_buildingId]
        local localCenterPosX = fluidSprayData.squirterOffset.x + fluidSprayData.squirterRange.x * 0.5 - data.range.width * 0.5
        local localCenterPosY = fluidSprayData.squirterOffset.y
        local localCenterPosZ = fluidSprayData.squirterOffset.z + fluidSprayData.squirterRange.z * 0.5 - data.range.depth * 0.5
        self.m_fluidSprayRange.gameObject:SetActive(true)
        self.m_fluidSprayRange.followerObject.getTargetPosInfo = function(pos, rot)
            pos, rot = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
            local q = Quaternion.Euler(rot.x, rot.y, rot.z)
            local m = Unity.Matrix4x4.TRS(pos, q, Vector3.one)
            pos = m:MultiplyPoint3x4(Vector3(localCenterPosX, localCenterPosY, localCenterPosZ))
            return pos, rot
        end
    end

    if isInit and data.type == GEnums.FacBuildingType.Battle then
        local battleData = GameInstance.remoteFactoryManager.staticData:QueryBattleData(self.m_buildingId)
        local range0 = battleData.attackRangeSize0
        local range1 = battleData.attackRangeSize1
        local scaleX = 1
        local scaleZ = 1
        local shapeType = battleData.rangeShapeType
        if shapeType == CS.Beyond.Gameplay.RemoteFactory.BattleShapeType.Circle then

            scaleX = range0 / 8
            scaleZ = scaleX
        elseif shapeType == CS.Beyond.Gameplay.RemoteFactory.BattleShapeType.Rect then

            scaleX = range1 / 2

            scaleZ = range0 / 6
        end
        if scaleX > 0 and scaleZ > 0 then
            self.m_battleRange.gameObject:SetActive(true)
            self.m_battleRange.transform.localScale = Vector3(scaleX, 1, scaleZ)
            self.m_battleRange.followerObject.getTargetPosInfo = function(pos, rot)
                pos, rot = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
                return pos, rot
            end
        end
    end
end







FacBuildModeCtrl._ProcessSignBuildSetting = HL.Method() << function(self)
    local isSign = FacConst.SIGN_BUILDING_EXTRA_SETTING_PANEL[self.m_buildingId] or false
    local isMoving = self.m_buildingNodeId ~= nil
    self.view.actionButtonsAsOption.signResetButton.gameObject:SetActiveIfNecessary(isSign and not isMoving)
    InputManagerInst:ToggleBinding(self.m_signResetBindingId, isSign and not isMoving)
    if isSign then
        if isMoving then
            local icons = GameInstance.remoteFactoryManager:GetSignBuildingIcons(self.m_buildingNodeId)
            GameInstance.remoteFactoryManager:SetPreviewSignBuildingIcon(icons[0], icons[1], icons[2])
        else
            self:_RefreshKeyHint(KeyHints.signBuilding)
            self:_OpenSignSettingPanel(false)
        end
    end
end

FacBuildModeCtrl._OpenSignSettingPanel = HL.Method(HL.Boolean) << function(self, reset)
    self.m_lockBuildPos = true
    self.view.actionButtonsAsOption.gameObject:SetActive(false)
    self.view.errorHint.gameObject:SetActiveIfNecessary(false)
    self.view.actionHint.gameObject:SetActiveIfNecessary(false)
    if self.m_sizeIndicator then
        self.m_sizeIndicator.gameObject:SetActive(false)
    end
    if reset then
        UIManager:AutoOpen(PanelId.FacMarkerConfirm, {
            reset = reset,
            onConfirm = function()
                self.m_lockBuildPos = false
                self.view.actionButtonsAsOption.gameObject:SetActive(true)
                if self.m_sizeIndicator then
                    self.m_sizeIndicator.gameObject:SetActive(true)
                end
            end,
            onClose = function()
                self.m_lockBuildPos = false
                self.view.actionButtonsAsOption.gameObject:SetActive(true)
                if self.m_sizeIndicator then
                    self.m_sizeIndicator.gameObject:SetActive(true)
                end
            end
        })
    else
        UIManager:AutoOpen(PanelId.FacMarkerConfirm, {
            reset = reset,
            onConfirm = function()
                self.m_lockBuildPos = false
                self.view.actionButtonsAsOption.gameObject:SetActive(true)
                if self.m_sizeIndicator then
                    self.m_sizeIndicator.gameObject:SetActive(true)
                end
            end,
            onClose = function()
                self:_ExitCurMode(true)
            end
        })
    end
end

FacBuildModeCtrl._CheckSignCanBuild = HL.Method().Return(HL.Boolean) << function(self)
    local isSign = FacConst.SIGN_BUILDING_EXTRA_SETTING_PANEL[self.m_buildingId]
    if isSign then
        local curNum = FactoryUtils.getPlayerAllMarkerBuildingNodeInfo()
        local maxNum = Tables.factoryConst.signNodeCountLimit
        local detla = self.m_buildingNodeId == nil and 1 or 0
        return curNum + detla <= maxNum
    end
    return true
end

FacBuildModeCtrl._CheckBanAndToastSignInTopView = HL.StaticMethod().Return(HL.Boolean) << function(itemId)
    local ban = false
    if FacConst.SIGN_BUILDING_BAN_IN_TOPVIEW[itemId] then
        if GameInstance.player.gameSettingSystem.forbiddenFactorySign then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BAN_SIGN_SDK_FORBIDDEN)
            ban = true
        elseif LuaSystemManager.factory.inTopView then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BAN_SIGN_IN_TOP_VIEW)
            ban = true
        end
    end
    return ban
end






FacBuildModeCtrl._EnterBlueprintMode = HL.Method(HL.Table) << function(self, args)
    if self.m_mode == FacConst.FAC_BUILD_MODE.Building then
        if self.m_buildArgs.onExit then
            self.m_buildArgs.onExit()
        end
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        self:_ExitLogisticMode()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        self:_ExitBeltMode()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        self:_ExitBlueprintMode()
    end

    self.m_isDragging = false
    UIManager.commonTouchPanel.enableZoom = true


    self.m_buildArgs = args
    self.m_lastBPGridPos = nil

    local initGridPos
    if args.initGridPos then
        initGridPos = args.initGridPos
    else
        local mousePos
        if args.initMousePos then
            mousePos = Vector3(args.initMousePos.x, args.initMousePos.y, 0)
        else
            mousePos = self:_InDragMode() and Vector3(Screen.width / 2, Screen.height / 2, 0) or self:_GetCurPointerPressPos()
        end
        local camRay = CameraManager.mainCamera:ScreenPointToRay(mousePos)
        local _, initWorldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(camRay)
        initGridPos = GameInstance.remoteFactoryManager.visual:WorldToVoxel(initWorldPos)
    end
    initGridPos = Unity.Vector3Int(math.floor(initGridPos.x), math.floor(initGridPos.y), math.floor(initGridPos.z))

    self:_RefreshKeyHint(KeyHints.blueprint)

    local interact = GameInstance.remoteFactoryManager.interact
    local initDir = args.initDir or 0
    if args.blueprint then

        interact:SwitchToBlueprintMode(args.blueprint, initGridPos, initDir, args.isMove)
        Notify(MessageConst.FAC_TOP_VIEW_SET_BLUEPRINT_ICONS, args.blueprint)
    elseif args.csBPInst then

        interact:SwitchToBlueprintModeWidthServerBlueprint(args.csBPInst, initGridPos, initDir)
        if args.csBPInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys then
            self.m_buildArgs.isSystemBP = true
            self.m_buildArgs.sysBpKey = args.csBPInst.param.sysBpKey
        elseif args.csBPInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Preset then
            self.m_buildArgs.isSystemBP = true
            self.m_buildArgs.sysBpKey = args.csBPInst.param.presetBpKey
        end
        Notify(MessageConst.FAC_TOP_VIEW_SET_BLUEPRINT_ICONS, args.csBPInst.info.bp)
    elseif args[1] then

        interact:SwitchToBlueprintModeWidthServerBlueprint(args[1], initGridPos, initDir)
    end
    local mode = interact.currentBlueprintMode
    if mode.onPlaceBlueprint then
        mode.onPlaceBlueprint = mode.onPlaceBlueprint - self.m_onPlaceFinish
        mode.onPlaceBlueprint = mode.onPlaceBlueprint + self.m_onPlaceFinish
    else
        mode.onPlaceBlueprint = self.m_onPlaceFinish
    end

    self.view.moveBuildingHint.gameObject:SetActive(args.isMove == true)

    self:_AdjustCameraForBlueprint(true)

    self.m_mode = FacConst.FAC_BUILD_MODE.Blueprint

    Notify(MessageConst.ON_BUILD_MODE_CHANGE, self.m_mode)
    Notify(MessageConst.ON_ENTER_BLUEPRINT_MODE)
    self:_NotifyPowerPoleTravelHint()
    if args.isMove then
        self:_TryExitLinkWireOnBuildingMove(args.batchSelectTargets)
    end
    self:_UpdateCommonNodesOnEnterMode()

    if not self:_InDragMode() then


        self:_Tick()
    end
end

FacBuildModeCtrl._AdjustCameraForBlueprint = HL.Method(HL.Boolean) << function(self, isEnterBlueprint)
    local camCtrl = LuaSystemManager.factory.m_topViewCamCtrl
    if not isEnterBlueprint then
        camCtrl:ResetCameraAdjustForRange()
        return
    end

    local range = self.m_buildArgs.range
    if not range then
        return
    end
    local needReverse = lume.round(LuaSystemManager.factory.topViewCamTarget.eulerAngles.y) % 180 ~= 0
    camCtrl:AdjustCameraForRange(needReverse and range.height or range.width, needReverse and range.width or range.height, function(pos)
        return FactoryUtils.clampTopViewCamTargetPosition(pos)
    end)
end

FacBuildModeCtrl._ExitBlueprintMode = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    Notify(MessageConst.FAC_TOP_VIEW_SET_BLUEPRINT_ICONS)
    self:_ExitMode(skipAnim)
    self:_AdjustCameraForBlueprint(false)
end

FacBuildModeCtrl.FacOnBuildingBatchMoved = HL.Method() << function(self)
    if not self.m_buildArgs then
        return
    end
    local targets = self.m_buildArgs.batchSelectTargets
    self:_ExitCurMode(true)
    if targets then
        Notify(MessageConst.FAC_UPDATE_TOP_VIEW_BUILDING_INFOS, targets)
    end
end

FacBuildModeCtrl._ConfirmBlueprint = HL.Method() << function(self)
    self:_Tick()
    if not self.m_curBuildIsValid then
        Notify(MessageConst.SHOW_TOAST, self.view.errorHintText.text)
        AudioAdapter.PostEvent("au_ui_fac_unbuildable")
        return
    end

    local mousePos = self:_GetCurPointerPressPos()
    GameInstance.remoteFactoryManager:GridPositionTriggered(mousePos, 0)
end

FacBuildModeCtrl.m_lastBPGridPos = HL.Field(HL.Any)

FacBuildModeCtrl._SetBluePrintSelectGrids = HL.Method(CS.UnityEngine.Vector2Int) << function(self, gridPos)
    self.m_lastBPGridPos = gridPos
    local range = self.m_buildArgs.range
    local curMode = GameInstance.remoteFactoryManager.interact.currentBlueprintMode
    local dir = curMode.preparedDirection
    local needReverse = dir % 2 == 1
    local width = needReverse and range.height or range.width
    local height = needReverse and range.width or range.height
    local rectInt = CS.UnityEngine.RectInt(gridPos.x - math.floor(width / 2), gridPos.y - math.floor(height / 2), width, height)
    CSFactoryUtil.SetSelectGrids(rectInt, CS.Beyond.Gameplay.Factory.GlobalSharedData.MapGridRendererData.MapGridInfo.SelectType.BLUEPRINT)

    local bpOriWorldPos = Vector3(rectInt.center.x, curMode.preparedPosition.y, rectInt.center.y)
    if dir == 0 then
        bpOriWorldPos = bpOriWorldPos - Vector3(width / 2, 0, height / 2)
    elseif dir == 1 then
        bpOriWorldPos = bpOriWorldPos - Vector3(width / 2, 0, -height / 2)
    elseif dir == 2 then
        bpOriWorldPos = bpOriWorldPos + Vector3(width / 2, 0, height / 2)
    elseif dir == 3 then
        bpOriWorldPos = bpOriWorldPos + Vector3(width / 2, 0, -height / 2)
    end
    Notify(MessageConst.FAC_TOP_VIEW_SET_BLUEPRINT_ICON_POS, { bpOriWorldPos, dir })
end






FacBuildModeCtrl._EnterLogisticMode = HL.Method(HL.Table) << function(self, args)
    if self.m_mode == FacConst.FAC_BUILD_MODE.Building then
        self:_ExitBuildingMode()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        if self.m_buildArgs.onExit then
            self.m_buildArgs.onExit()
        end
        self:_SetCamState()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        self:_ExitBeltMode()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        self:_ExitBlueprintMode()
    end

    self.m_isDragging = false
    UIManager.commonTouchPanel.enableZoom = true


    self.m_buildArgs = args
    local itemId = args.itemId
    local itemData = Tables.itemTable[itemId]
    local logisticId = Tables.factoryItem2LogisticIdTable[itemId].logisticId

    local initGridPos = args.initGridPos
    if initGridPos then
        initGridPos = Unity.Vector3Int(lume.round(initGridPos.x), lume.round(initGridPos.y), lume.round(initGridPos.z))
    else
        local mousePos
        if args.initMousePos then
            mousePos = Vector3(args.initMousePos.x, args.initMousePos.y, 0)
        else
            mousePos = self:_InDragMode() and Vector3(Screen.width / 2, Screen.height / 2, 0) or self:_GetCurPointerPressPos()
        end

        local camRay = CameraManager.mainCamera:ScreenPointToRay(mousePos)
        local _, initWorldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(camRay)
        initGridPos = GameInstance.remoteFactoryManager.visual:WorldToVoxel(initWorldPos)
        initGridPos = Unity.Vector3Int(lume.round(initGridPos.x), lume.round(initGridPos.y), lume.round(initGridPos.z))
    end

    GameInstance.remoteFactoryManager.interact:SwitchToBuildingMode(logisticId, initGridPos, args.initDir or 0)
    local mode = GameInstance.remoteFactoryManager.interact.buildingMode
    mode.onPlaceBuilding = mode.onPlaceBuilding - self.m_onPlaceFinish
    mode.onPlaceBuilding = mode.onPlaceBuilding + self.m_onPlaceFinish

    self.m_buildingId = logisticId
    self.m_itemData = itemData
    self.m_mode = FacConst.FAC_BUILD_MODE.Logistic

    local keyHint = LuaSystemManager.factory.inTopView and KeyHints.logisticInTopView or KeyHints.logistic
    if not self:_CanRotate() then

        local tmp = {}
        for _, v in ipairs(keyHint) do
            if v ~= "fac_rotate_device" then
                table.insert(tmp, v)
            end
        end
        keyHint = tmp
    end
    self:_RefreshKeyHint(keyHint)

    self:_UpdateValidResult()

    local logisticData = FactoryUtils.getLogisticData(logisticId)
    self:_SetCamState(logisticData.buildCamState)

    Notify(MessageConst.ON_BUILD_MODE_CHANGE, self.m_mode)
    Notify(MessageConst.ON_ENTER_LOGISTIC_MODE, itemData.id)
    self:_UpdateCommonNodesOnEnterMode()
end

FacBuildModeCtrl._ExitLogisticMode = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    self:_ExitMode(skipAnim)
end

FacBuildModeCtrl._ConfirmLogistic = HL.Method() << function(self)
    self:_Tick()
    if not self.m_curBuildIsValid then
        Notify(MessageConst.SHOW_TOAST, self.view.errorHintText.text)
        AudioAdapter.PostEvent("au_ui_fac_unbuildable")
        return
    end

    local mousePos = self:_GetCurPointerPressPos()
    GameInstance.remoteFactoryManager:GridPositionTriggered(mousePos, 0)

    if not self:_EnableContinueBuild() then
        self:_ExitLogisticMode()
    end
end

FacBuildModeCtrl._CancelLogistic = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    local onCancel = self.m_buildArgs.onCancel
    if onCancel then
        onCancel()
    end

    self:_ExitLogisticMode(skipAnim)
end






FacBuildModeCtrl._EnterBeltMode = HL.Method(HL.Table) << function(self, args)
    local prevBuildMode = self.m_mode
    if self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        if self.m_buildArgs.onExit then
            self.m_buildArgs.onExit()
        end
        self:_SetCamState()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        self:_ExitLogisticMode()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Building then
        self:_ExitBuildingMode()
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint then
        self:_ExitBlueprintMode()
    end

    self.m_isDragging = false
    UIManager.commonTouchPanel.enableZoom = true
    self.m_beltHasStartLastTick = false

    self.m_mode = FacConst.FAC_BUILD_MODE.Belt
    self.m_buildArgs = args
    self.m_beltId = args.beltId

    local isPipe = self:_IsPipe()
    local mark = isPipe and self.m_pipePreviewMark or self.m_beltStartPreviewMark

    if self:_InDragMode() then
        GameInstance.remoteFactoryManager:SetupConveyorInteractMode(CS.Beyond.Gameplay.RemoteFactory.ConveyorInteractMode.TraceDrag)
        mark.gameObject:SetActive(false)
        GameInstance.remoteFactoryManager.interact:SwitchToConveyorMode(self.m_beltId)
    else
        GameInstance.remoteFactoryManager:SetupConveyorInteractMode(CS.Beyond.Gameplay.RemoteFactory.ConveyorInteractMode.Precise)
        mark.gameObject:SetActive(true)
        GameInstance.remoteFactoryManager.interact:SwitchToConveyorMode(self.m_beltId)
        self:_UpdateBPStartPreviewMark(self:_GetCurPointerPressPos(), true)
    end
    self:_RefreshKeyHint(isPipe and KeyHints.pipeStart or KeyHints.beltStart)

    self:_UpdateValidResult()

    if LuaSystemManager.factory.inTopView then
        if prevBuildMode ~= FacConst.FAC_BUILD_MODE.Belt then
            self.m_simpleFigureBackupValid = true
            self.m_simpleFigureModeBeforeBeltBuild = LuaSystemManager.factory.simpleFigureMode
            local autoMode = isPipe and FacConst.SIMPLE_FIGURE_MODE.SimpleBeltFigure or FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure
            LuaSystemManager.factory.simpleFigureMode = autoMode
        end
    else
        self.m_simpleFigureBackupValid = false
    end

    Notify(MessageConst.ON_BUILD_MODE_CHANGE, self.m_mode)
    Notify(MessageConst.ON_ENTER_BELT_MODE)
    self:_UpdateCommonNodesOnEnterMode()

    if isPipe then
        local data = Tables.factoryLiquidPipeTable[self.m_beltId].pipeData
        self:_SetCamState(data.buildCamState)
    else
        local data = Tables.factoryGridBeltTable[self.m_beltId].beltData
        self:_SetCamState(data.buildCamState)
    end

    if DeviceInfo.usingTouch then
        if LuaSystemManager.factory.inTopView then
            self.view.actionButtonsAsIcon.gameObject:SetActive(false)
        end
    end
end

FacBuildModeCtrl._ExitBeltMode = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    local mark = self:_IsPipe() and self.m_pipePreviewMark or self.m_beltStartPreviewMark
    mark.transform:DOKill()
    mark.gameObject:SetActive(false)

    if not InputManagerInst.inChangingInputDevice then
        if LuaSystemManager.factory.inTopView and self.m_simpleFigureBackupValid then
            self.m_simpleFigureBackupValid = false
            LuaSystemManager.factory:SetSimpleFigureMode(self.m_simpleFigureModeBeforeBeltBuild)
        else
            LuaSystemManager.factory:ApplySimpleFigureToRenderer()
        end
    end

    if DeviceInfo.usingTouch then
        LuaSystemManager.factory.canMoveCamTarget = true
    end

    self:_ExitMode(skipAnim)
end


FacBuildModeCtrl.m_bpStartPreviewMarkLastPos = HL.Field(HL.Userdata)

FacBuildModeCtrl.m_bpStartPreviewMarkLastColor = HL.Field(Color)


FacBuildModeCtrl._UpdateBPStartPreviewMark = HL.Method(Vector3, HL.Opt(HL.Boolean)) << function(self, curMousePos, isInit)
    local camRay = CameraManager.mainCamera:ScreenPointToRay(curMousePos)
    local _, worldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(camRay)
    self:_UpdateBPStartPreviewMarkWithWorldPos(worldPos, isInit)
end

FacBuildModeCtrl._UpdateBPStartPreviewMarkWithWorldPos = HL.Method(Vector3, HL.Opt(HL.Boolean)) << function(self, worldPos, isInit)
    local visual = GameInstance.remoteFactoryManager.visual
    local beltPos = visual:WorldToBeltGrid(worldPos)
    local roundedWorldPos = visual:BeltGridToWorld(Vector2(lume.round(beltPos.x), lume.round(beltPos.y)))
    roundedWorldPos.y = worldPos.y
    local isPipe = self:_IsPipe()
    local mark = isPipe and self.m_pipePreviewMark or self.m_beltStartPreviewMark
    local trans = mark.transform
    if isInit then
        trans.position = roundedWorldPos
        self.m_bpStartPreviewMarkLastPos = nil
    else
        if not self.m_bpStartPreviewMarkLastPos or (roundedWorldPos - self.m_bpStartPreviewMarkLastPos).sqrMagnitude >= 0.01 then
            trans.position = roundedWorldPos
            self.m_bpStartPreviewMarkLastPos = roundedWorldPos
            local needAudio
            if DeviceInfo.usingTouch then
                needAudio = not LuaSystemManager.factory.inTopView or self.m_isDragging
            else
                needAudio = true
            end
            if needAudio then
                AudioAdapter.PostEvent("au_ui_belt_move")
            end
        end
    end
    self:_UpdateBPStartPreviewMarkColor(isInit)
end

FacBuildModeCtrl._UpdateBPStartPreviewMarkColor = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local isPipe = self:_IsPipe()
    local mark = isPipe and self.m_pipePreviewMark or self.m_beltStartPreviewMark
    local color
    if self.m_curBuildIsValid then
        color = mark.config.NORMAL_COLOR
    else
        color = mark.config.INVALID_COLOR
    end
    if isInit or color ~= self.m_bpStartPreviewMarkLastColor then
        self.m_bpStartPreviewMarkLastColor = color
        for _, mat in ipairs(mark.mats) do
            mat:SetColor("_TintColor", color)
        end
    end
end


FacBuildModeCtrl.OnInteractConveyorLocalCheckingFailed = HL.Method(HL.Table) << function(self, args)
    self:_UpdateValidResult()

    if not self.m_curBuildIsValid then
        Notify(MessageConst.SHOW_TOAST, self.view.errorHintText.text)
    end
end


FacBuildModeCtrl.m_beltHasStartLastTick = HL.Field(HL.Boolean) << false

FacBuildModeCtrl._UpdateOnBeltHasStartChanged = HL.Method() << function(self)
    local hasStart = GameInstance.remoteFactoryManager.interact.currentConveyorMode.hasStart
    self.m_beltHasStartLastTick = hasStart
    local isPipe = self:_IsPipe()
    local inTopView = LuaSystemManager.factory.inTopView
    if inTopView then
        if hasStart then
            self.view.actionHintTxt:SetAndResolveTextStyle(isPipe and Language.LUA_FAC_BUILD_MODE_DRAW_PIPE_HINT_WITH_START or Language.LUA_FAC_BUILD_MODE_DRAW_BELT_HINT_WITH_START)
        else
            self.view.actionHintTxt:SetAndResolveTextStyle(isPipe and Language.LUA_FAC_BUILD_MODE_DRAW_PIPE_HINT or Language.LUA_FAC_BUILD_MODE_DRAW_BELT_HINT)
        end
        self.view.actionButtonsAsIcon.gameObject:SetActive(hasStart)
        self.view.exitBeltModeButton.gameObject:SetActive(not hasStart)
    else
        self.view.actionButtonsAsOption.rotateButton.gameObject:SetActive(hasStart)
        self.view.actionButtonsAsOption.confirmText.text = hasStart and Language.LUA_FAC_BUILD_MODE_CONFIRM_BELT_END or Language.LUA_FAC_BUILD_MODE_CONFIRM_BELT_START
    end
    if isPipe then
        self:_RefreshKeyHint(hasStart and KeyHints.pipeEnd or KeyHints.pipeStart)
    else
        self:_RefreshKeyHint(hasStart and KeyHints.beltEnd or KeyHints.beltStart)
    end
end






FacBuildModeCtrl._GetCurPointerPressPos = HL.Method().Return(Vector3) << function(self)

    if self.m_lockBuildPos then
        return CameraManager.mainCamera:WorldToScreenPoint(self.m_lastMouseWorldPos)
    end
    if LuaSystemManager.factory.inTopView then
        if DeviceInfo.usingTouch then
            return UIManager.commonTouchPanel.touchPos:XY()
        else
            return InputManager.mousePosition
        end
    else
        if not InputManager.cursorVisible or DeviceInfo.usingTouch then
            return Vector3(Screen.width / 2, Screen.height / 2, 0)
        end
        return InputManager.mousePosition
    end
end

FacBuildModeCtrl._InDragMode = HL.Method().Return(HL.Boolean) << function(self)

    return DeviceInfo.usingTouch and LuaSystemManager.factory.inTopView
end

FacBuildModeCtrl._IsPipe = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_beltId == FacConst.PIPE_ID
end

FacBuildModeCtrl.DebugOutputPrepareBuildingPosInfo = HL.Method() << function(self)
    local id, pos, dir
    if self.m_mode == FacConst.FAC_BUILD_MODE.Building or self.m_mode == FacConst.FAC_BUILD_MODE.Logistic then
        local rot
        pos, rot = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
        id = self.m_buildingId
        dir = rot.y
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Belt then
        local mark = isPipe and self.m_pipePreviewMark or self.m_beltStartPreviewMark
        id = self.m_beltId
        pos = mark.transform.position
        dir = 0
    elseif self.m_mode == FacConst.FAC_BUILD_MODE.Blueprint and self.m_buildArgs.isSystemBP then
        local curMode = GameInstance.remoteFactoryManager.interact.currentBlueprintMode
        id = self.m_buildArgs.sysBpKey
        dir = curMode.preparedDirection * 90
        pos = Vector3(curMode.preparedPosition.x, curMode.preparedPosition.y, curMode.preparedPosition.z)
    else
        return
    end
    local info = string.format("%s %s %d", id, pos:ToString(), dir)
    Unity.GUIUtility.systemCopyBuffer = info
    Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 已复制 %s", info))
    logger.error("DebugOutputPrepareBuildingPosInfo", info)
end






FacBuildModeCtrl.s_enableConfirmBuild = HL.StaticField(HL.Boolean) << true

FacBuildModeCtrl.SetEnableConfirmBuild = HL.StaticMethod(HL.Table) << function(args)
    local enable = unpack(args)
    FacBuildModeCtrl.s_enableConfirmBuild = enable
end

FacBuildModeCtrl.s_enableExitBuildMode = HL.StaticField(HL.Boolean) << true

FacBuildModeCtrl.SetEnableExitBuildMode = HL.StaticMethod(HL.Table) << function(args)
    local enable = unpack(args)
    FacBuildModeCtrl.s_enableExitBuildMode = enable
end

FacBuildModeCtrl.m_lockBuildPos = HL.Field(HL.Boolean) << false

FacBuildModeCtrl.FacLockBuildPos = HL.Method(HL.Table) << function(self, arg)
    local isLock = unpack(arg)
    self.m_lockBuildPos = isLock
end






FacBuildModeCtrl._OnChangeContinueToggle = HL.Method(HL.Boolean) << function(self, isOn)
    FacBuildModeCtrl.s_enableContinueBuild = isOn
    UIUtils.PlayAnimationAndToggleActive(self.view.actionButtonsAsIcon.continueBuildHint, isOn)
end

FacBuildModeCtrl._EnableContinueBuild = HL.Method().Return(HL.Boolean) << function(self)
    if not LuaSystemManager.factory.inTopView then
        return false
    end
    if DeviceInfo.usingKeyboard then
        return InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.LeftControl)
    elseif DeviceInfo.usingController then
        return InputManagerInst:GetKey(CS.Beyond.Input.GamepadKeyCode.LT)
    end
    return FacBuildModeCtrl.s_enableContinueBuild
end


FacBuildModeCtrl.m_onPlaceFinish = HL.Field(HL.Function)

FacBuildModeCtrl._OnPlaceFinish = HL.Method() << function(self)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BUILD_CONTINUOUS_SUCCESS)
    if DeviceInfo.usingTouch and LuaSystemManager.factory.inTopView and FacBuildModeCtrl.s_enableContinueBuild then
        self:_StartCoroutine(function()
            coroutine.step()
            coroutine.step()
            GameInstance.remoteFactoryManager:SyncJobFence()
            self:_AutoMoveOnContinueBuildSucc()
        end)
    end
    self:_UpdatePendingHint()
    GameInstance.remoteFactoryManager:SyncJobFence()
end

FacBuildModeCtrl._AutoMoveOnContinueBuildSucc = HL.Method() << function(self)
    if not DeviceInfo.usingTouch or not GameInstance.remoteFactoryManager.interact.currentBuildingMode then

        return
    end

    local worldPos = GameInstance.remoteFactoryManager.interact.currentBuildingMode:GetPreviewRenderInfo()
    worldPos = worldPos + CameraManager.mainCamera.transform.right
    local voxelPos = GameInstance.remoteFactoryManager.visual:WorldToVoxel(worldPos)
    local gridPos = Unity.Vector2Int(math.floor(voxelPos.x), math.floor(voxelPos.z))
    GameInstance.remoteFactoryManager:GridPositionTriggered(gridPos, Vector2.zero, voxelPos.y, 4)
end




HL.Commit(FacBuildModeCtrl)
