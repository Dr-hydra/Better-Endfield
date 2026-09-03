local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.LevelCamera
LevelCameraCtrl = HL.Class('LevelCameraCtrl', uiCtrl.UICtrl)



local CAM_SPEED_NO_CURSOR = {
    ['speed_x'] = 0.9,
    ['speed_y'] = 0.63
}






LevelCameraCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.TOGGLE_LEVEL_CAMERA_MOVE] = 'ToggleCameraMove',
    [MessageConst.ON_TOGGLE_INTERACT_OPTION_SCROLL] = 'OnToggleInteractOptionScroll',

    [MessageConst.ENTER_LEVEL_HALF_SCREEN_PANEL_MODE] = 'EnterLevelHalfScreenPanelMode',
    [MessageConst.EXIT_LEVEL_HALF_SCREEN_PANEL_MODE] = 'ExitLevelHalfScreenPanelMode',

    [MessageConst.FAC_ON_DRAG_BEGIN_IN_BATCH_MODE] = 'FacOnDragBeginInBathMode',
    [MessageConst.FAC_ON_DRAG_END_IN_BATCH_MODE] = 'FacOnDragEndInBathMode',
    [MessageConst.ON_DRAG_SPRINT_BTN] = '_MoveCamera',
    [MessageConst.ON_DRAG_WATER_DRONE_JOYSTICK] = '_MoveCamera',

    [MessageConst.MOVE_LEVEL_CAMERA] = '_MoveCamera',
    [MessageConst.ZOOM_LEVEL_CAMERA] = 'ZoomCamera',
}




LevelCameraCtrl.m_curFreeLookCamCtrl = HL.Field(CS.Beyond.Gameplay.View.LevelCameraController)

LevelCameraCtrl.m_onRightMouseButtonPress = HL.Field(HL.Function)

LevelCameraCtrl.m_onDrag = HL.Field(HL.Function)

LevelCameraCtrl.m_onZoom = HL.Field(HL.Function)

LevelCameraCtrl.m_updateKey = HL.Field(HL.Number) << -1

LevelCameraCtrl.m_disableCameraZoom = HL.Field(HL.Boolean) << false

LevelCameraCtrl.m_disableCameraMoveKeys = HL.Field(HL.Table)

LevelCameraCtrl.m_onStartSwipeTouchPanel = HL.Field(HL.Function)

LevelCameraCtrl.m_onSwipeTouchPanel = HL.Field(HL.Function)

LevelCameraCtrl.m_onEndSwipeTouchPanel = HL.Field(HL.Function)





LevelCameraCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_disableCameraMoveKeys = {}
    self:_LoadLevelCamera()
end

LevelCameraCtrl.OnShow = HL.Override() << function(self)
    self:_AddRegisters()
end

LevelCameraCtrl.OnHide = HL.Override() << function(self)
    self:_ClearRegisters()
end

LevelCameraCtrl.OnClose = HL.Override() << function(self)
    self:_ClearRegisters()
end




LevelCameraCtrl._AddRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Add("Tick", function()
        self:_UpdateCamera()
    end)

    local touchPanel = UIManager.commonTouchPanel
    if not self.m_onZoom then
        self.m_onZoom = function(delta)
            self:_OnTouchPanelZoom(delta)
        end
    end
    touchPanel.onZoom:AddListener(self.m_onZoom)
    if BEYOND_DEBUG then
        if not self.m_onRightMouseButtonPress then
            self.m_onRightMouseButtonPress = function(delta)
                self:_MoveCamera(delta)
            end
        end
        touchPanel.onRightMouseButtonPress:AddListener(self.m_onRightMouseButtonPress)
    end
    if not self.m_onDrag then
        self.m_onDrag = function(eventData)
            self:_MoveCamera(eventData.delta)
        end
    end
    touchPanel.onDrag:AddListener(self.m_onDrag)

    if not self.m_onStartSwipeTouchPanel then
        self.m_onStartSwipeTouchPanel = function(pos)
            self:_OnStartSwipeTouchPanel(pos)
        end
        self.m_onSwipeTouchPanel = function(delta, pos)
            self:_OnSwipeTouchPanel(delta, pos)
        end
        self.m_onEndSwipeTouchPanel = function()
            self:_OnEndSwipeTouchPanel()
        end
    end
    InputManagerInst.onStartSwipeTouchPanel:Add(self.m_onStartSwipeTouchPanel)
    InputManagerInst.onSwipeTouchPanel:Add(self.m_onSwipeTouchPanel)
    InputManagerInst.onEndSwipeTouchPanel:Add(self.m_onEndSwipeTouchPanel)
end

LevelCameraCtrl._ClearRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)

    local touchPanel = UIManager.commonTouchPanel
    touchPanel.onZoom:RemoveListener(self.m_onZoom)
    if BEYOND_DEBUG then
        touchPanel.onRightMouseButtonPress:RemoveListener(self.m_onRightMouseButtonPress)
    end
    touchPanel.onDrag:RemoveListener(self.m_onDrag)

    InputManagerInst.onStartSwipeTouchPanel:Remove(self.m_onStartSwipeTouchPanel)
    InputManagerInst.onSwipeTouchPanel:Remove(self.m_onSwipeTouchPanel)
    InputManagerInst.onEndSwipeTouchPanel:Remove(self.m_onEndSwipeTouchPanel)
end




LevelCameraCtrl._LoadLevelCamera = HL.Method() << function(self)
    if self.m_curFreeLookCamCtrl then
        return
    end
    local camCtrl = CameraManager:GetMainLevelCameraController()
    if camCtrl == nil then
        camCtrl = CameraManager:LoadPersistentController("LevelCamera")
    end
    local mainCharRoot = GameInstance.playerController.mainCharacter.rootCom
    camCtrl:SetFollow(mainCharRoot.transform)
    self.m_curFreeLookCamCtrl = camCtrl

    local normalCam = camCtrl.levelVirtualCamera
    self.m_normalCameraBody = normalCam:GetCinemachineComponent(CS.Cinemachine.CinemachineCore.Stage.Body)
    self.m_normalCameraAim = normalCam:GetCinemachineComponent(CS.Cinemachine.CinemachineCore.Stage.Aim)





    self.m_camConfig = {
        luaCustomConfig = camCtrl.transform:GetComponent("LuaCustomConfig")
    }
    UIUtils.initLuaCustomConfig(self.m_camConfig)
    self.m_camConfig = self.m_camConfig.config
end

LevelCameraCtrl.ToggleCameraMove = HL.Method(HL.Table) << function(self, arg)
    local key, enable = unpack(arg)
    if enable then
        self.m_disableCameraMoveKeys[key] = nil
    else
        self.m_disableCameraMoveKeys[key] = true
    end
end

LevelCameraCtrl.CanMoveCam = HL.Method().Return(HL.Boolean) << function(self)
    return self.view.inputGroup.internalEnabled and next(self.m_disableCameraMoveKeys) == nil
end

LevelCameraCtrl.OnToggleInteractOptionScroll = HL.Method(HL.Boolean) << function(self, enable)
    if DeviceInfo.usingTouch then
        return
    end
    self.m_disableCameraZoom = enable
end

LevelCameraCtrl.FacOnDragBeginInBathMode = HL.Method() << function(self)
    self.m_disableCameraZoom = true
end

LevelCameraCtrl.FacOnDragEndInBathMode = HL.Method() << function(self)
    self.m_disableCameraZoom = false
end

LevelCameraCtrl._UpdateCamera = HL.Method() << function(self)
    if InputManager.cursorVisible then
        return
    end

    if not UIManager.commonTouchPanel.groupEnabled then
        return
    end

    if DeviceInfo.usingKeyboard then
        local x = InputManagerInst:GetAxis("Mouse X") * CAM_SPEED_NO_CURSOR.speed_x
        local y = InputManagerInst:GetAxis("Mouse Y") * CAM_SPEED_NO_CURSOR.speed_y

        if BEYOND_DEBUG_COMMAND and CS.Beyond.TimeManager.s_GmEnableHighFrequencyTick then
            x = x * CS.Beyond.TimeManager.s_GmTimeSlice / Time.deltaTime
            y = y * CS.Beyond.TimeManager.s_GmTimeSlice / Time.deltaTime
        end

        if x ~= 0 or y ~= 0 then
            local delta = Vector2(x, y)
            self:_MoveCamera(delta)
        end
    end

    if DeviceInfo.usingController then
        local x = InputManagerInst:GetAxis("View X") * Time.deltaTime
        local y = InputManagerInst:GetAxis("View Y") * Time.deltaTime

        if BEYOND_DEBUG_COMMAND and CS.Beyond.TimeManager.s_GmEnableHighFrequencyTick then
            x = x * CS.Beyond.TimeManager.s_GmTimeSlice / Time.deltaTime
            y = y * CS.Beyond.TimeManager.s_GmTimeSlice / Time.deltaTime
        end

        if x ~= 0 or y ~= 0 then
            local delta = Vector2(x, y)
            self:_MoveCamera(delta)
        end
    end
end

LevelCameraCtrl._MoveCamera = HL.Method(HL.Userdata) << function(self, delta)
    if UNITY_EDITOR and DeviceInfo.usingTouch then


        if math.abs(delta.x) > 500 or math.abs(delta.y) > 300 then
            return
        end
    end

    if not self:CanMoveCam() then
        return
    end

    if DeviceInfo.usingKeyboard then



        if delta.x == 0 then
            delta.x = InputManagerInst:GetAxis("Mouse X") * CAM_SPEED_NO_CURSOR.speed_x
        end
        if delta.y == 0 then
            delta.y = InputManagerInst:GetAxis("Mouse Y") * CAM_SPEED_NO_CURSOR.speed_y
        end
    end

    if InputManagerInst.usingController then

        CameraManager:OnInput(delta.x, delta.y)
    else
        CameraManager:OnInput(UIUtils.getNormalizedScreenX(delta.x), UIUtils.getNormalizedScreenY(delta.y))
    end
end

LevelCameraCtrl._OnTouchPanelZoom = HL.Method(HL.Number) << function(self, delta)
    if LuaSystemManager.factory.inTopView then
        if DeviceInfo.usingKeyboard then
            delta = delta * self.view.config.TOP_VIEW_ZOOM_SPD_MOUSE
        elseif DeviceInfo.usingTouch then
            delta = delta * self.view.config.TOP_VIEW_ZOOM_SPD_TOUCH
        elseif DeviceInfo.usingController then
            delta = delta * self.view.config.TOP_VIEW_ZOOM_SPD_CONTROLLER
        end
    end
    self:ZoomCamera(delta)
end


LevelCameraCtrl.ZoomCamera = HL.Method(HL.Number) << function(self, delta)
    if not self:CanMoveCam() or self.m_disableCameraZoom then
        return
    end
    Utils.zoomCamera(delta)
end




LevelCameraCtrl.m_normalCameraBody = HL.Field(HL.Userdata)

LevelCameraCtrl.m_normalCameraAim = HL.Field(HL.Userdata)







LevelCameraCtrl.m_cameraOffsetXCor = HL.Field(HL.Thread)

LevelCameraCtrl.m_camConfig = HL.Field(HL.Table)

LevelCameraCtrl.ToggleHalfScreenPanelCameraOffset = HL.Method(HL.Boolean) << function(self, active)
    if LuaSystemManager.factory.inTopView then
        return
    end

    self.m_cameraOffsetXCor = CoroutineManager:ClearCoroutine(self.m_cameraOffsetXCor)
    local targetOffsetX = active and self.m_camConfig.CAM_SCREEN_X_OFFSET or 0
    local oldOffsetX = self.m_normalCameraBody.ShoulderOffset.x
    self.m_cameraOffsetXCor = CoroutineManager:StartCoroutine(function()
        local time = 0
        while true do
            time = time + Time.deltaTime
            local ratio = self.m_camConfig.CAM_SCREEN_X_CURVE:Evaluate(time)
            if math.abs(1 - ratio) <= 0.00001 then
                self:_SetCamerasOffset(targetOffsetX)
                break
            end
            local x = lume.lerp(oldOffsetX, targetOffsetX, ratio)
            self:_SetCamerasOffset(x)
            coroutine.step()
        end
    end, self)
end

LevelCameraCtrl._SetCamerasOffset = HL.Method(HL.Number) << function(self, xValue)
    self:_SetCameraBodyOffset(self.m_normalCameraBody, xValue)
    self:_SetCameraAimOffset(self.m_normalCameraAim, xValue)


end

LevelCameraCtrl._SetCameraBodyOffset = HL.Method(HL.Userdata, HL.Number) << function(self, target, xValue)
    local offset = target.ShoulderOffset
    offset.x = xValue
    target.ShoulderOffset = offset
end

LevelCameraCtrl._SetCameraAimOffset = HL.Method(HL.Userdata, HL.Number) << function(self, target, xValue)
    local offset = target.m_TrackedObjectOffset
    offset.x = xValue
    target.m_TrackedObjectOffset = offset
end






LevelCameraCtrl.m_inHalfScreenPanelMode = HL.Field(HL.Boolean) << false

LevelCameraCtrl.EnterLevelHalfScreenPanelMode = HL.Method() << function(self)
    if self.m_inHalfScreenPanelMode then
        return
    end
    self.m_inHalfScreenPanelMode = true
    self:ToggleHalfScreenPanelCameraOffset(true)
end

LevelCameraCtrl.ExitLevelHalfScreenPanelMode = HL.Method() << function(self)
    if not self.m_inHalfScreenPanelMode then
        return
    end
    self.m_inHalfScreenPanelMode = false
    self:ToggleHalfScreenPanelCameraOffset(false)
end






LevelCameraCtrl.m_touchPanelStartPos = HL.Field(Vector2)

LevelCameraCtrl.m_touchPanelStarted = HL.Field(HL.Boolean) << false

LevelCameraCtrl.m_lastTouchStartTime = HL.Field(HL.Number) << 0

LevelCameraCtrl._OnStartSwipeTouchPanel = HL.Method(Vector2) << function(self, pos)

    self.m_touchPanelStartPos = pos
    self.m_touchPanelStarted = false
    if Time.unscaledTime - self.m_lastTouchStartTime < 0.3 then
        CS.Beyond.Gameplay.View.CameraUtils.RecenterCamera()
        self.m_lastTouchStartTime = 0
    else
        self.m_lastTouchStartTime = Time.unscaledTime
    end
end

LevelCameraCtrl._OnSwipeTouchPanel = HL.Method(Vector2, Vector2) << function(self, delta, pos)

    if not self.m_touchPanelStartPos then
        return
    end
    if not self.m_touchPanelStarted then
        if math.abs(pos.y - self.m_touchPanelStartPos.y) > 0.3 then
            self.m_touchPanelStarted = true
        end
    end
    if self.m_touchPanelStarted then
        if UIUtils.IsPhaseLevelOnTop() then
            self:ZoomCamera(delta.y * -200)
        end
    end
end

LevelCameraCtrl._OnEndSwipeTouchPanel = HL.Method() << function(self)

    self.m_touchPanelStartPos = nil
    self.m_touchPanelStarted = false
end




HL.Commit(LevelCameraCtrl)
