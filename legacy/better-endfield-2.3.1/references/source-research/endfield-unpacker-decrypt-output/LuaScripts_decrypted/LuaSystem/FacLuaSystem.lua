local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
FacLuaSystem = HL.Class('FacLuaSystem', LuaSystemBase.LuaSystemBase)



FacLuaSystem.inDestroyMode = HL.Field(HL.Boolean) << false

FacLuaSystem.interactPanelCtrl = HL.Field(HL.Forward('FacBuildingInteractCtrl'))

FacLuaSystem.m_disableSwitchModeParams = HL.Field(HL.Userdata)

FacLuaSystem.FacLuaSystem = HL.Constructor() << function(self)
    self:RegisterMessage(MessageConst.FAC_TOGGLE_TOP_VIEW, function(arg)
        local active = false
        local fastMode
        if type(arg) == "table" then
            active, fastMode = unpack(arg)
        else
            active = arg
        end
        self:ToggleTopView(active, fastMode)
    end)

    self:RegisterMessage(MessageConst.ON_TELEPORT_TO, function(arg)
        if self.inTopView then
            self:ToggleTopView(false, true)
        end
        self:SetFacBuildListSelectDecoBuilding(false)
    end)
    self:RegisterMessage(MessageConst.ALL_CHARACTER_DEAD, function(arg)
        if self.inTopView then
            self:ToggleTopView(false, true)
        end
    end)
    self:RegisterMessage(MessageConst.ON_REPATRIATE, function(arg)
        if self.inTopView then
            self:ToggleTopView(false, true)
        end
    end)

    self:RegisterMessage(MessageConst.FORBID_SYSTEM_CHANGED, function(args)
        local type, active = unpack(args)
        if type == ForbidType.ForbidFactoryMode then
            if active then
                LuaSystemManager.factory:AddFactoryModeRequest({ false, "ForbidFactoryMode" })
            else
                LuaSystemManager.factory:RemoveFactoryModeRequest("ForbidFactoryMode")
            end
        end
    end)

    self:RegisterMessage(MessageConst.ON_ENTER_FAC_MAIN_REGION, function(arg)
        CS.Beyond.Gameplay.Audio.AudioRemoteFactoryBridge.OnEnterFactoryMainRegionChanged(true, arg)
        self:StartCheckPowerNotEnoughAudio()
    end)
    self:RegisterMessage(MessageConst.ON_EXIT_FAC_MAIN_REGION, function(arg)
        CS.Beyond.Gameplay.Audio.AudioRemoteFactoryBridge.OnEnterFactoryMainRegionChanged(false, arg)
        self:StopCheckPowerNotEnoughAudio()
        self:ToggleTopView(false, true)
    end)

    self:RegisterMessage(MessageConst.ON_BUILD_MODE_CHANGE, function(arg)
        self:_CheckCommonClearScreenForMode()
    end)
    self:RegisterMessage(MessageConst.ON_FAC_DESTROY_MODE_CHANGE, function(arg)
        self:_CheckCommonClearScreenForMode()
    end)

    self:RegisterMessage(MessageConst.ON_CONFIRM_CHANGE_INPUT_DEVICE_TYPE, function(arg)
        Notify(MessageConst.FAC_BUILD_EXIT_CUR_MODE, true)
        Notify(MessageConst.FAC_EXIT_DESTROY_MODE, true)
        self:ToggleTopView(false, true)
    end)

    self:_InitFactoryMode()
    self:_InitFacBackupViewedState()

    self.batchSelectTargets = {}
    self.m_pipeHighlightRequests = {}
    self.m_activePipeHighlights = {}

    self.m_disableSwitchModeParams = CS.Beyond.Gameplay.DisableSwitchModeForbidParams(CS.Beyond.Gameplay.DisableSwitchModeForbidParams.ForbidStyle.ShowEmptyBtn)
end

FacLuaSystem.OnInit = HL.Override() << function(self)
end

FacLuaSystem.OnRelease = HL.Override() << function(self)
    self.m_pipeHighlightRequests = {}
    self:_ApplyPipeHighlight()
    self:ToggleTopView(false, true)
    if self.m_topViewCamCtrl then
        self.m_topViewCamCtrl.onZoom = nil
        CameraManager:RemoveCameraController(self.m_topViewCamCtrl)
        self.m_topViewCamCtrl = nil
    end
    if self.topViewCamTarget then
        GameObject.Destroy(self.topViewCamTarget.gameObject)
        self.topViewCamTarget = nil
    end
    if self.topViewControllerMouseMoveTarget then
        GameObject.Destroy(self.topViewControllerMouseMoveTarget.gameObject)
        self.topViewControllerMouseMoveTarget = nil
    end
end





FacLuaSystem.inTopView = HL.Field(HL.Boolean) << false

FacLuaSystem.isTopViewHideUIMode = HL.Field(HL.Boolean) << false

FacLuaSystem.topViewCamTarget = HL.Field(Transform)

FacLuaSystem.topViewControllerMouseMoveTarget = HL.Field(Transform)

FacLuaSystem.m_topViewCamCtrl = HL.Field(CS.Beyond.Gameplay.View.FacTopViewCameraController)

FacLuaSystem.m_topViewCor = HL.Field(HL.Thread)


FacLuaSystem.ToggleTopView = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, fastMode)
    if active and (Utils.isCurSquadAllDead() or not Utils.isInFacMainRegion()) then

        return
    end

    if active == self.inTopView then
        return
    end

    if not GameWorld.worldInfo.inFactoryMode then
        return
    end

    if not fastMode then

        local succ = GameInstance.player.systemActionConflictManager:TryStartSystemAction(Const.FacTopViewSystemActionConflictName)
        if not succ then
            return
        end
    end

    self.m_topViewCor = self:_ClearCoroutine(self.m_topViewCor)
    if not active and not fastMode then

        Notify(MessageConst.PREPARE_BLOCK_GLITCH_TRANSITION)
        Notify(MessageConst.ADD_COMMON_BLOCK_MASK, "FacTopView")
        self.m_topViewCor = self:_StartCoroutine(function()
            coroutine.waitForRenderDone()
            self:_InternalToggleTopView(active, fastMode)
        end)
        return
    end

    self:_InternalToggleTopView(active, fastMode)
end

FacLuaSystem._InternalToggleTopView = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, fastMode)
    logger.info("FacLuaSystem._InternalToggleTopView", active, fastMode)

    Notify(MessageConst.REMOVE_COMMON_BLOCK_MASK, "FacTopView")

    Notify(MessageConst.FAC_EXIT_DESTROY_MODE)
    Notify(MessageConst.FAC_BUILD_EXIT_CUR_MODE)

    self.inTopView = active
    self:ResetSimpleFigureForTopView()
    if self.isTopViewHideUIMode then
        self:ToggleTopViewHideUIMode(false)
    end

    if active then
        self:AddFactoryModeRequest({ true, "FacTopView" })
    else
        self:RemoveFactoryModeRequest("FacTopView")
    end

    Notify(MessageConst.TOGGLE_FORBID_ATTACK, { "FacTopView", active })
    GameInstance.player.forbidSystem:SetForbid(ForbidType.DisableSwitchMode, "FacTopView", active, self.m_disableSwitchModeParams)

    GameInstance.playerController:OnToggleFactoryTopView(active)
    GameInstance.remoteFactoryManager:ChangeTopViewState(active)



    local _, panel = UIManager:IsOpen(PanelId.LevelCamera)
    local levelCamCtrl
    if panel then

        levelCamCtrl = panel.m_curFreeLookCamCtrl
    end
    if active then
        if not self.topViewCamTarget then
            self.topViewCamTarget = GameObject("TopViewCamTarget").transform
            GameObject.DontDestroyOnLoad(self.topViewCamTarget.gameObject)
            self.topViewControllerMouseMoveTarget = GameObject("TopViewControllerMouseMoveTarget").transform
            GameObject.DontDestroyOnLoad(self.topViewControllerMouseMoveTarget.gameObject)
        end
        local topViewCamCtrl = CameraManager:LoadPersistentController("FacTopViewCamera")
        topViewCamCtrl:SetTarget(self.topViewCamTarget)
        local mainCharRoot = GameInstance.playerController.mainCharacter.rootCom
        local duration = topViewCamCtrl:StartEnterTween(mainCharRoot.transform.position, fastMode)
        self.topViewControllerMouseMoveTarget.position = mainCharRoot.transform.position
        if duration > 0 then
            Notify(MessageConst.SHOW_BLOCK_INPUT_PANEL, duration)
        end
        self.m_topViewCamCtrl = topViewCamCtrl
        self.m_topViewCamCtrl.onZoom = function(zoomPercent)
            Notify(MessageConst.ON_FAC_TOP_VIEW_CAM_ZOOM, zoomPercent)
        end

        CSFactoryUtil.ChangeFacEcsCullingSetting(120, 100)
    else
        self:ToggleAutoMoveTopViewCam()
        if self.m_topViewCamCtrl then
            self.m_topViewCamCtrl.onZoom = nil
            if levelCamCtrl then

                levelCamCtrl.param:SetHorizontalAngle(self.m_topViewCamCtrl.transform.eulerAngles.y, false)
                levelCamCtrl.param:SetVerticalValue(0.5, false)
            end
            CameraManager:RemoveCameraController(self.m_topViewCamCtrl)
            self.m_topViewCamCtrl = nil
            if not fastMode then
                Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
            end
        end
        CSFactoryUtil.ResetFacEcsCullingSetting()
    end

    if active then
        if fastMode then
            Notify(MessageConst.ON_TOGGLE_FAC_TOP_VIEW, true)
        else
            UIManager:PreloadPersistentPanelAsset(PanelId.FacTopView)
            UIManager:PreloadPersistentPanelAsset(PanelId.FacTopViewLowerCfg)
            UIManager:PreloadPersistentPanelAsset(PanelId.FacTopViewBuildingInfo)
            UIManager:ClearScreenWithOutAnimation(function(clearScreenKey)
                self:_StartTimer(0.6, function()
                    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.FacTopViewSystemActionConflictName)
                    if self.inTopView then
                        Notify(MessageConst.ON_TOGGLE_FAC_TOP_VIEW, true)
                    end
                    UIManager:RecoverScreen(clearScreenKey)
                end)
            end)
        end
    else
        Notify(MessageConst.ON_TOGGLE_FAC_TOP_VIEW, false)
        GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.FacTopViewSystemActionConflictName)
    end

    EventLogManagerInst:GameEvent_FactoryTopViewSwitch(active)
end

FacLuaSystem.ToggleTopViewHideUIMode = HL.Method(HL.Boolean) << function(self, active)
    self.isTopViewHideUIMode = active
    Notify(MessageConst.ON_FAC_TOP_VIEW_HIDE_UI_MODE_CHANGE, active)
end

FacLuaSystem.simpleFigureMode = HL.Field(HL.Number) << FacConst.SIMPLE_FIGURE_MODE.None

FacLuaSystem.SetSimpleFigureMode = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, mode, skipNotify)
    self.simpleFigureMode = mode
    self:ApplySimpleFigureToRenderer()
    if not skipNotify then
        Notify(MessageConst.ON_FAC_SIMPLE_FIGURE_MODE_CHANGE, mode)
    end
end

FacLuaSystem._PushSimpleFigureModeToEcs = HL.Method(HL.Number) << function(self, mode)
    local Evt = CS.Beyond.Gameplay.Factory.EvtLogisticFigureRenderer
    local bit
    if mode == FacConst.SIMPLE_FIGURE_MODE.None then
        bit = Evt.S_NONE
    elseif mode == FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure then
        bit = Evt.S_PIPE
    else
        bit = Evt.S_CONVEYOR
    end
    GameInstance.remoteFactoryManager:ToggleLogisticFigure(bit)
end

FacLuaSystem.ApplySimpleFigureToRenderer = HL.Method() << function(self)
    self:_PushSimpleFigureModeToEcs(self.simpleFigureMode)
end


FacLuaSystem.ClearSimpleFigureEcsOnly = HL.Method() << function(self)
    self:_PushSimpleFigureModeToEcs(FacConst.SIMPLE_FIGURE_MODE.None)
end


FacLuaSystem.ResetSimpleFigureForTopView = HL.Method() << function(self)
    self.simpleFigureMode = FacConst.SIMPLE_FIGURE_MODE.None
    self:_PushSimpleFigureModeToEcs(FacConst.SIMPLE_FIGURE_MODE.None)
    Notify(MessageConst.ON_FAC_SIMPLE_FIGURE_MODE_CHANGE, self.simpleFigureMode)
end


FacLuaSystem.GetSimpleFigureModeFromRenderer = HL.Method().Return(HL.Number) << function(self)
    local simpleConveyor, simplePipe = GameInstance.remoteFactoryManager:GetSimpleFigureInfo()
    if simplePipe and simpleConveyor then
        return FacConst.SIMPLE_FIGURE_MODE.None
    end
    if simplePipe then
        return FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure
    end
    if simpleConveyor then
        return FacConst.SIMPLE_FIGURE_MODE.SimpleBeltFigure
    end
    return FacConst.SIMPLE_FIGURE_MODE.None
end


FacLuaSystem.GetSimpleFigureExcludeBeltPipeForBatchSelect = HL.Method().Return(HL.Boolean, HL.Boolean) << function(self)
    return GameInstance.remoteFactoryManager:GetSimpleFigureInfo()
end









FacLuaSystem.canMoveCamTarget = HL.Field(HL.Boolean) << true

FacLuaSystem.topViewControllerMouseMoveTargetChanged = HL.Field(HL.Boolean) << false

FacLuaSystem.MoveTopViewCamTarget = HL.Method(Vector2) << function(self, dir)
    if dir == Vector2.zero then
        return
    end
    if IsNull(self.m_topViewCamCtrl) then
        return
    end
    if not self.canMoveCamTarget then
        return
    end
    local target = self.topViewCamTarget
    if not target then
        return
    end
    if DeviceInfo.usingController then
        if IsNull(InputManagerInst.customControllerMouseTrans) then
            return
        end
    end

    local settingSpeed = DataManager.gameplayCameraSetting.cameraSettingTopViewControlSpeedCurve:Evaluate(
        CS.Beyond.GameSetting.controllerCachedCameraTopViewSpeed
    )
    local zoomScale = 1 + (1 - self.m_topViewCamCtrl.curZoomPercent)
    dir = dir * settingSpeed * zoomScale
    local camTrans = CameraManager.mainCamera.transform
    local realDir = dir.x * camTrans.right + dir.y * camTrans.up
    realDir.y = 0
    local moveDif = realDir.normalized * dir.magnitude
    if DeviceInfo.usingController and not InputManagerInst:GetKey(CS.Beyond.Input.GamepadKeyCode.RT) then
        local canvasRect = UIManager.uiCanvasRect.rect
        local halfCanvasSize = Vector2(canvasRect.width, canvasRect.height) / 2
        local mouseScreenPos = (InputManagerInst.customControllerMouseTrans.anchoredPosition + halfCanvasSize) * Screen.width / canvasRect.width
        local oldPosFromMouse = CameraManager.mainCamera:ScreenToWorldPoint(Vector3(mouseScreenPos.x, mouseScreenPos.y, CameraManager.mainCamera.transform.position.y - target.position.y))
        local oldPos = self.topViewControllerMouseMoveTarget.position
        if (oldPos - oldPosFromMouse).sqrMagnitude >= 1 then



            oldPos = oldPosFromMouse
        end
        local newPos = oldPos + moveDif * 0.8
        self.topViewControllerMouseMoveTarget.position = newPos
        self.topViewControllerMouseMoveTargetChanged = true
        local curScreenWorldRect = CSFactoryUtil.GetCurScreenWorldRect(FacConst.FAC_TOP_VIEW_CAM_DRAG_PADDING)
        if not curScreenWorldRect:Contains(newPos:XZ()) then
            local rectForTarget = Unity.Rect(newPos.x - curScreenWorldRect.width / 2, newPos.z - curScreenWorldRect.height / 2, curScreenWorldRect.width, curScreenWorldRect.height)
            local targetNewPos = target.position
            targetNewPos.x = lume.clamp(targetNewPos.x, rectForTarget.xMin, rectForTarget.xMax)
            targetNewPos.z = lume.clamp(targetNewPos.z, rectForTarget.yMin, rectForTarget.yMax)
            target.position = FactoryUtils.clampTopViewCamTargetPosition(targetNewPos, target.position)
            Notify(MessageConst.ON_FAC_TOP_VIEW_CAM_TARGET_MOVED)
        end
    else
        local oldPos = target.position
        target.position = FactoryUtils.clampTopViewCamTargetPosition(oldPos + moveDif, oldPos)
        if DeviceInfo.usingController then

            self.topViewControllerMouseMoveTarget.position = self.topViewControllerMouseMoveTarget.position + (target.position - oldPos)
            self.topViewControllerMouseMoveTargetChanged = true
        end
        Notify(MessageConst.ON_FAC_TOP_VIEW_CAM_TARGET_MOVED)
        if self.m_autoMoveTopViewCamRelatedSize then
            self.m_stopAutoMoveTopViewCamOnce = true
        end
    end
end

FacLuaSystem.m_autoMoveTopViewCamRelatedSize = HL.Field(HL.Any)

FacLuaSystem.m_autoMoveTopViewCamPadding = HL.Field(HL.Any)

FacLuaSystem.m_stopAutoMoveTopViewCamOnce = HL.Field(HL.Boolean) << false

FacLuaSystem.m_autoMoveTopViewCamUpdateKey = HL.Field(HL.Any)

FacLuaSystem.ToggleAutoMoveTopViewCam = HL.Method(HL.Opt(Vector2, Vector4)) << function(self, size, extraPadding)
    self.m_autoMoveTopViewCamRelatedSize = size
    if size then
        local padding = CSFactoryUtil.Padding(150, 200, 200, 150)
        if extraPadding then
            padding.top = padding.top + extraPadding.x
            padding.left = padding.left + extraPadding.y
            padding.right = padding.right + extraPadding.z
            padding.bottom = padding.bottom + extraPadding.w
        end
        self.m_autoMoveTopViewCamPadding = padding
    end

    self.m_stopAutoMoveTopViewCamOnce = false
    if size then
        if not self.m_autoMoveTopViewCamUpdateKey then
            self.m_autoMoveTopViewCamUpdateKey = LuaUpdate:Add("LateTick", function()
                self:_CalcAutoMoveTopViewCam()
            end)
        end
    else
        LuaUpdate:Remove(self.m_autoMoveTopViewCamUpdateKey)
        self.m_autoMoveTopViewCamUpdateKey = nil
    end
end

FacLuaSystem._CalcAutoMoveTopViewCam = HL.Method() << function(self)
    if not CS.UnityEngine.Application.isFocused then return end
    if self.m_stopAutoMoveTopViewCamOnce then
        self.m_stopAutoMoveTopViewCamOnce = false
        return
    end
    local mousePos = InputManager.mousePosition
    local cam = CameraManager.mainCamera
    local target = self.topViewCamTarget
    local posFromMouse = cam:ScreenToWorldPoint(Vector3(mousePos.x, mousePos.y, cam.transform.position.y - target.position.y))
    local curScreenWorldRect = CSFactoryUtil.GetCurScreenWorldRect(self.m_autoMoveTopViewCamPadding)
    local size = self.m_autoMoveTopViewCamRelatedSize
    local center = curScreenWorldRect.center
    curScreenWorldRect.width = math.max(curScreenWorldRect.width - size.x, 0)
    curScreenWorldRect.height = math.max(curScreenWorldRect.height - size.y, 0)
    curScreenWorldRect.center = center
    if not curScreenWorldRect:Contains(posFromMouse:XZ()) then
        local rectForTarget = Unity.Rect(posFromMouse.x - curScreenWorldRect.width / 2, posFromMouse.z - curScreenWorldRect.height / 2, curScreenWorldRect.width, curScreenWorldRect.height)
        local targetNewPos = target.position
        targetNewPos.x = lume.clamp(targetNewPos.x, rectForTarget.xMin, rectForTarget.xMax)
        targetNewPos.z = lume.clamp(targetNewPos.z, rectForTarget.yMin, rectForTarget.yMax)
        local diff = targetNewPos - target.position
        if diff == Vector3.zero then
            return
        end
        local maxDist = Time.deltaTime * FacConst.FAC_TOP_VIEW_AUTO_MOVE_CAM_SPD
        local curDist = diff.magnitude
        if curDist > maxDist then
            targetNewPos = target.position + diff / curDist * maxDist
        end
        target.position = FactoryUtils.clampTopViewCamTargetPosition(targetNewPos, target.position)
        Notify(MessageConst.ON_FAC_TOP_VIEW_CAM_TARGET_MOVED)
    end
end

FacLuaSystem.RotateTopViewCam = HL.Method() << function(self)
    if not self.m_topViewCamCtrl then
        return
    end
    local angle = math.floor((self.topViewCamTarget.eulerAngles.y - 45) / 90) * 90
    self.topViewCamTarget:DORotate(Vector3(90, angle, 0), 0.5)
    AudioAdapter.PostEvent("Au_UI_Button_Building_Turn")
end

FacLuaSystem.GetTopViewCamZoomValue = HL.Method().Return(HL.Number) << function(self)
    if not self.m_topViewCamCtrl then
        return -1
    end
    return self.m_topViewCamCtrl.curZoom
end

FacLuaSystem.SetTopViewCamZoomValue = HL.Method(HL.Number) << function(self, zoomValue)
    if not self.m_topViewCamCtrl then
        return
    end
    self.m_topViewCamCtrl:SetZoomImmediately(zoomValue)
end









FacLuaSystem.inBatchSelectMode = HL.Field(HL.Boolean) << false


FacLuaSystem.inDragSelectBatchMode = HL.Field(HL.Boolean) << false

FacLuaSystem.isReverseBatchSelect = HL.Field(HL.Boolean) << false

FacLuaSystem.ChangeIsReverseSelect = HL.Method(HL.Boolean) << function(self, isReverse)
    self.isReverseBatchSelect = isReverse
end











FacLuaSystem.batchSelectTargets = HL.Field(HL.Table)

FacLuaSystem.GetBlueprintFromBatchSelectTargets = HL.Method().Return(HL.Opt(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint)) << function(self)
    local targets = self.batchSelectTargets
    if not next(targets) then
        return
    end
    local builder = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintBuilder()
    if not builder.valid then
        return
    end
    for id, info in pairs(targets) do
        if info == true then
            builder:AppendWholeNode(id)
        else
            for k, _ in pairs(info) do
                builder:AppendConveyorUnit(id, k)
            end
        end
    end
    return builder:Build()
end

FacLuaSystem.GetCurBatchSelectTargetCount = HL.Method().Return(HL.Number) << function(self)
    local count = 0
    if self.batchSelectTargets then
        for _, _ in pairs(self.batchSelectTargets) do
            count = count + 1
        end
    end
    return count
end







FacLuaSystem.m_checkPowerNotEnoughAudioTimerId = HL.Field(HL.Number) << -1
FacLuaSystem.StartCheckPowerNotEnoughAudio = HL.Method() << function(self)
    self.m_checkPowerNotEnoughAudioTimerId = self:_StartTimer(Tables.factoryConst.checkPowerNotEnoughAudioDelay, function()
        local powerInfo = FactoryUtils.getCurRegionPowerInfo()
        if powerInfo ~= nil then
            local powerCost = powerInfo.powerCost
            local powerGen = powerInfo.powerGen
            if powerCost > powerGen then
                CS.Beyond.Gameplay.Audio.AudioRemoteFactoryAnnouncement.Announcement("au_fac_announcement_low_power")
            end
        end
    end)
end
FacLuaSystem.StopCheckPowerNotEnoughAudio = HL.Method() << function(self)
    self.m_checkPowerNotEnoughAudioTimerId = self:_ClearTimer(self.m_checkPowerNotEnoughAudioTimerId)
end






FacLuaSystem.m_facModeRequestStack = HL.Field(HL.Forward('Stack'))

FacLuaSystem.m_facModeRequestMap = HL.Field(HL.Table)

FacLuaSystem.m_disableFacModeChangeToast = HL.Field(HL.Boolean) << false

FacLuaSystem.inFacModeBeforeEnterDungeon = HL.Field(HL.Any)

FacLuaSystem.lastMapIsDungeon = HL.Field(HL.Boolean) << false



FacLuaSystem._InitFactoryMode = HL.Method() << function(self)
    self.m_facModeRequestMap = {}
    self.m_facModeRequestStack = require_ex("Common/Utils/DataStructure/Stack")()

    self:RegisterMessage(MessageConst.FORCE_SET_FAC_MODE, function(arg)
        local toFacMode = unpack(arg)
        self:ClearAndSetFactoryMode(toFacMode)
    end)
end


FacLuaSystem.AddFactoryModeRequest = HL.Method(HL.Table) << function(self, args)
    local toFacMode, requestName, forceUpdate = unpack(args)
    local oldValue = self.m_facModeRequestMap[requestName]
    if oldValue == nil then
        self.m_facModeRequestStack:Push(requestName)
    end
    self.m_facModeRequestMap[requestName] = toFacMode
    self:_UpdateFactoryMode(forceUpdate)
end

FacLuaSystem.RemoveFactoryModeRequest = HL.Method(HL.String) << function(self, requestName)
    local oldValue = self.m_facModeRequestMap[requestName]
    if oldValue == nil then
        return
    end
    self.m_facModeRequestStack:Delete(requestName)
    self.m_facModeRequestMap[requestName] = nil
    self:_UpdateFactoryMode()
end

FacLuaSystem.GetFactoryModeOfRequest = HL.Method(HL.String).Return(HL.Opt(HL.Boolean)) << function(self, requestName)
    return self.m_facModeRequestMap[requestName]
end

FacLuaSystem.ClearAndSetFactoryMode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, toFacMode, noToast)
    self.m_facModeRequestStack:Clear()
    self.m_facModeRequestMap = {}
    self.m_disableFacModeChangeToast = noToast == true
    self:AddFactoryModeRequest({ toFacMode, "Player", true })
    self.m_disableFacModeChangeToast = false
end

FacLuaSystem._UpdateFactoryMode = HL.Method(HL.Opt(HL.Boolean)).Return(HL.Boolean) << function(self, forceUpdate)
    local inFacMode
    local requestName = self.m_facModeRequestStack:Peek()
    if requestName then
        inFacMode = self.m_facModeRequestMap[requestName]
    else
        inFacMode = false
    end
    local curInFacMode = GameWorld.worldInfo.inFactoryMode
    if inFacMode == curInFacMode and not forceUpdate then
        return false
    end
    self:_SetFactoryMode(inFacMode)
    return true
end

FacLuaSystem._SetFactoryMode = HL.Method(HL.Boolean) << function(self, inFacMode)
    Notify(MessageConst.FAC_EXIT_DESTROY_MODE, true)


    local isPhaseLevelOpened = PhaseManager:IsOpen(PhaseId.Level)

    if isPhaseLevelOpened then
        local hidePanels = inFacMode and Const.BATTLE_MODE_ONLY_PANELS or Const.FACTORY_MODE_ONLY_PANELS
        for _, panelId in pairs(hidePanels) do
            UIManager:Hide(panelId)
        end
    end
    GameWorld.worldInfo.inFactoryMode = inFacMode
    if not inFacMode then
        Notify(MessageConst.ON_EXIT_FACTORY_MODE)
    end

    if isPhaseLevelOpened then
        local showPanels = inFacMode and Const.FACTORY_MODE_ONLY_PANELS or Const.BATTLE_MODE_ONLY_PANELS
        for _, panelId in pairs(showPanels) do
            UIManager:AutoOpen(panelId)
        end
    end

    if inFacMode then
        Notify(MessageConst.ON_ENTER_FACTORY_MODE)
    end
    Notify(MessageConst.ON_FAC_MODE_CHANGE, inFacMode)
    GameWorld.hudFadeManager:SetPreventFadeState(CS.Beyond.HudFadeType.FacMode, inFacMode)

    if not self.m_disableFacModeChangeToast then
        AudioAdapter.PostEvent(inFacMode and "au_ui_fac_mode_on" or "au_ui_fac_mode_off")
    end

    EventLogManagerInst:SetGameEventCommonData_CFactoryMode(inFacMode)

    local placeType = CS.Beyond.SDK.EventLogManager.FactoryModePlaceType.Outter
    local isOpen, phaseLevel = PhaseManager:IsOpen(PhaseId.Level)
    if isOpen then
        if phaseLevel.mainRegionPanelIndex ~= -1 then
            local info = GameInstance.remoteFactoryManager.system.core:GetHubInfoAt(GameWorld.worldInfo.curLevelIdNum, phaseLevel.mainRegionPanelIndex)
            if info then
                if info.isSub then
                    placeType = CS.Beyond.SDK.EventLogManager.FactoryModePlaceType.Sub
                else
                    placeType = CS.Beyond.SDK.EventLogManager.FactoryModePlaceType.Main
                end
            end
        end
    end
    EventLogManagerInst:GameEvent_FactoryModeSwitch(inFacMode, GameWorld.worldInfo.curLevelId, Utils.getCurDomainId(), placeType)
end







FacLuaSystem.commonClearScreenKeyForMode = HL.Field(HL.Number) << -1

FacLuaSystem._CheckCommonClearScreenForMode = HL.Method() << function(self)

    self:_StartTimer(0, function()
        self:_UpdateCommonClearScreenForMode()
    end)
end

FacLuaSystem._UpdateCommonClearScreenForMode = HL.Method() << function(self)
    local needClearScreen = self.inTopView and (self.inDestroyMode or FactoryUtils.isInBuildMode())
    if needClearScreen then
        if self.commonClearScreenKeyForMode > 0 then
            return
        end
        self.commonClearScreenKeyForMode = UIManager:ClearScreen(Const.ALL_RESERVE_PANEL_IDS_FOR_FAC_MODE_IN_TOP_VIEW)
    else
        if self.commonClearScreenKeyForMode == -1 then
            return
        end
        self.commonClearScreenKeyForMode = UIManager:RecoverScreen(self.commonClearScreenKeyForMode)
    end
end






FacLuaSystem.m_pipeHighlightRequests = HL.Field(HL.Table)

FacLuaSystem.m_activePipeHighlights = HL.Field(HL.Table)

FacLuaSystem.RequestPipeHighlight = HL.Method(HL.String, HL.Number, HL.Number) << function(self, source, nodeId, unitIndex)
    self.m_pipeHighlightRequests[source] = { nodeId = nodeId, unitIndex = unitIndex }
    self:_ApplyPipeHighlight()
end

FacLuaSystem.ReleasePipeHighlight = HL.Method(HL.String) << function(self, source)
    self.m_pipeHighlightRequests[source] = nil
    self:_ApplyPipeHighlight()
end

FacLuaSystem._ApplyPipeHighlight = HL.Method() << function(self)

    local wanted = {}
    for _, req in pairs(self.m_pipeHighlightRequests) do
        local t = wanted[req.nodeId]
        if not t then
            t = {}
            wanted[req.nodeId] = t
        end
        t[req.unitIndex] = true
    end

    local allNodeIds = {}
    for nodeId in pairs(self.m_activePipeHighlights) do allNodeIds[nodeId] = true end
    for nodeId in pairs(wanted) do allNodeIds[nodeId] = true end

    for nodeId in pairs(allNodeIds) do
        local oldUnits = self.m_activePipeHighlights[nodeId]
        local newUnits = wanted[nodeId]
        local same = true
        if oldUnits and newUnits then
            for ui in pairs(oldUnits) do
                if not newUnits[ui] then same = false; break end
            end
            if same then
                for ui in pairs(newUnits) do
                    if not oldUnits[ui] then same = false; break end
                end
            end
        else
            same = false
        end
        if not same then
            if oldUnits then
                GameInstance.remoteFactoryManager:SoloSelect(nodeId, false)
            end
            if newUnits then
                for unitIndex in pairs(newUnits) do
                    GameInstance.remoteFactoryManager:SoloSelect(nodeId, true, unitIndex)
                end
            end
        end
    end

    self.m_activePipeHighlights = wanted
end




local BACKUP_POWER_TOAST_MAIN_HUD_QUEUE_TYPE = "BackupPowerCheck"

FacLuaSystem.m_backupPowerViewed = HL.Field(HL.Table)

FacLuaSystem.m_backupPowerInUseToasted = HL.Field(HL.Table)
FacLuaSystem.m_backupPowerPendingToast = HL.Field(HL.Table)
FacLuaSystem.m_loginBackupChecked = HL.Field(HL.Table)

FacLuaSystem._InitFacBackupViewedState = HL.Method() << function(self)
    self.m_backupPowerViewed = {}
    self.m_backupPowerInUseToasted = {}
    self.m_backupPowerPendingToast = {}
    self.m_loginBackupChecked = {}

    self:RegisterMessage(MessageConst.ON_BACKUP_STATE_CHANGED, function(arg)
        local chapterId = unpack(arg)
        local isInUse
        if FactoryUtils.getIsInBackupPower(chapterId) then
            isInUse = true
        elseif FactoryUtils.getIsBackupPowerUnderRecovery(chapterId) then
            isInUse = false
        else
            return
        end

        if not self.m_backupPowerPendingToast[chapterId] then
            self.m_backupPowerPendingToast[chapterId] = {}
        end
        self.m_backupPowerPendingToast[chapterId] = {isInUse}

        LuaSystemManager.mainHudActionQueue:AddRequest(BACKUP_POWER_TOAST_MAIN_HUD_QUEUE_TYPE, function(_)
            self:CheckBackupPowerAndToast()
        end)
    end)

    self:RegisterMessage(MessageConst.ON_PHASE_LEVEL_ON_TOP, function(arg)
        LuaSystemManager.mainHudActionQueue:AddRequest(BACKUP_POWER_TOAST_MAIN_HUD_QUEUE_TYPE, function(_)
            self:CheckBackupPowerAndToast()
        end)
    end)
end

FacLuaSystem.CheckBackupPowerAndToast = HL.Method() << function(self)
      local top = PhaseManager:GetTopPhaseId()
      if top ~= PhaseId.Level or Utils.isInDungeon() then
          return
      end

      local chapterId = Utils.getCurrentChapterId()
      local inUse

      if not self.m_loginBackupChecked[chapterId] then
        if FactoryUtils.getIsInBackupPower(chapterId) then
             inUse = true
         else
             return
         end
      else
          local pendingToast = self.m_backupPowerPendingToast[chapterId]
          if pendingToast == nil then
              return
          end
          inUse = unpack(pendingToast)
      end

      local domainId = ScopeUtil.ChapterIdInt2Str(chapterId)
      local _, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
      local domainName = domainCfg.domainName
      if inUse then
          if not self:IsBackupPowerToasted(chapterId, true) then
              Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_FAC_BACKUP_POWER_IN_USE_TOAST,domainName))
             self:SetBackupPowerToasted(chapterId, true)
          end
      else
          if not self:IsBackupPowerToasted(chapterId, false) then
              Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_FAC_BACKUP_POWER_USE_UP_TOAST,domainName))
              self:SetBackupPowerToasted(chapterId, false)
          end
      end

      self.m_loginBackupChecked[chapterId] = true
      self.m_backupPowerPendingToast[chapterId] = nil
end

FacLuaSystem.IsBackupPowerViewed = HL.Method(HL.Number).Return(HL.Boolean) << function(self, chapterId)
    local lastStartTs = FactoryUtils.getRegionPowerInfoByChapterId(chapterId).backupLastStartTs
    local id = chapterId .. "_" .. lastStartTs
    return self.m_backupPowerViewed[id] ~= nil and self.m_backupPowerViewed[id] or false
end

FacLuaSystem.SetBackupPowerViewed = HL.Method(HL.Number) << function(self, chapterId)
    if self:IsBackupPowerViewed(chapterId) then
        return
    end
    local lastStartTs = FactoryUtils.getRegionPowerInfoByChapterId(chapterId).backupLastStartTs
    local id = chapterId .. "_" .. lastStartTs
    self.m_backupPowerViewed[id] = true
    Notify(MessageConst.FAC_BACKUP_POWER_VIEWED)
end

FacLuaSystem.IsBackupPowerToasted = HL.Method(HL.Number, HL.Boolean).Return(HL.Boolean) << function(self, chapterId, isInUse)
    local lastStartTs = FactoryUtils.getRegionPowerInfoByChapterId(chapterId).backupLastStartTs
    local lastStopTs = FactoryUtils.getRegionPowerInfoByChapterId(chapterId).backupLastStopTs
    local id
    if isInUse then
        id = chapterId .. "_" .. lastStartTs
    else
        id = chapterId .. "_" .. lastStopTs
    end
    return self.m_backupPowerInUseToasted[id] ~= nil and self.m_backupPowerInUseToasted[id] or false
end

FacLuaSystem.SetBackupPowerToasted = HL.Method(HL.Number, HL.Boolean) << function(self, chapterId, isInUse)
    if self:IsBackupPowerToasted(chapterId, isInUse) then
        return
    end
    local lastStartTs = FactoryUtils.getRegionPowerInfoByChapterId(chapterId).backupLastStartTs
    local lastStopTs = FactoryUtils.getRegionPowerInfoByChapterId(chapterId).backupLastStopTs
    local id
    if isInUse then
        id = chapterId .. "_" .. lastStartTs
    else
        id = chapterId .. "_" .. lastStopTs
    end
    self.m_backupPowerInUseToasted[id] = true
end




FacLuaSystem.m_facBuildListSelectIsShowDecoBuilding = HL.Field(HL.Boolean) << false

FacLuaSystem.SetFacBuildListSelectDecoBuilding = HL.Method(HL.Boolean) << function(self, isShowDecoBuilding)
    self.m_facBuildListSelectIsShowDecoBuilding = isShowDecoBuilding
end

FacLuaSystem.FacBuildListSelectIsShowDecoBuilding = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_facBuildListSelectIsShowDecoBuilding
end




HL.Commit(FacLuaSystem)
return FacLuaSystem
