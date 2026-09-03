
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SnapshotCamera

SnapshotCameraCtrl = HL.Class('SnapshotCameraCtrl', uiCtrl.UICtrl)


local SnapshotCameraController = CS.Beyond.Gameplay.View.SnapshotCameraController


local RotationModeEnum = CS.Beyond.Gameplay.View.SnapshotCameraController.RotationMode


local snapshotSystem = GameInstance.player.snapshotSystem
local forbidToastColdDownTime = 3





SnapshotCameraCtrl.s_messages = HL.StaticField(HL.Table) << {

}




SnapshotCameraCtrl.m_curMoveDir = HL.Field(Vector3)

SnapshotCameraCtrl.m_updateKey = HL.Field(HL.Number) << -1

SnapshotCameraCtrl.m_isForbidCamMoveOrRotate = HL.Field(HL.Boolean) << false

SnapshotCameraCtrl.m_forbidToastColdDown = HL.Field(HL.Number) << 0






SnapshotCameraCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_curMoveDir = Vector3.zero
end

SnapshotCameraCtrl.OnClose = HL.Override() << function(self)
    self:_ClearRegisters()
end

SnapshotCameraCtrl.OnShow = HL.Override() << function(self)
    self:_AddRegisters()
end

SnapshotCameraCtrl.OnHide = HL.Override() << function(self)
    self:_ClearRegisters()
end

SnapshotCameraCtrl._AddRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Add("Tick", function()
        self:_UpdateCamera()
    end)

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

    self:BindInputPlayerAction("snapshot_controller_move_cam_plane_up", function()
        self:MoveCameraInPlane(Vector2(0, 1))
    end)
    self:BindInputPlayerAction("snapshot_controller_move_cam_plane_down", function()
        self:MoveCameraInPlane(Vector2(0, -1))
    end)
    self:BindInputPlayerAction("snapshot_controller_move_cam_plane_left", function()
        self:MoveCameraInPlane(Vector2(-1, 0))
    end)
    self:BindInputPlayerAction("snapshot_controller_move_cam_plane_right", function()
        self:MoveCameraInPlane(Vector2(1, 0))
    end)
end

SnapshotCameraCtrl._ClearRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)

    InputManagerInst.onStartSwipeTouchPanel:Remove(self.m_onStartSwipeTouchPanel)
    InputManagerInst.onSwipeTouchPanel:Remove(self.m_onSwipeTouchPanel)
    InputManagerInst.onEndSwipeTouchPanel:Remove(self.m_onEndSwipeTouchPanel)
end






SnapshotCameraCtrl._UpdateCamera = HL.Method() << function(self)
    if not snapshotSystem.camController then
        return
    end
    snapshotSystem.camController:AddCameraOffset(self.m_curMoveDir, Time.deltaTime)
    self.m_curMoveDir = Vector3.zero

    if DeviceInfo.usingController then
        local x = InputManagerInst:GetAxis("View X") * Time.deltaTime
        local y = InputManagerInst:GetAxis("View Y") * Time.deltaTime
        if x ~= 0 or y ~= 0 then
            self:SurroundMoveCamera(x, y)
        end
    end
end







SnapshotCameraCtrl.MoveCameraInPlane = HL.Method(Vector2) << function(self, dir)
    if self.m_isForbidCamMoveOrRotate then
        if self.m_forbidToastColdDown < Time.time then
            Notify(MessageConst.SHOW_TOAST, { Language.LUA_SNAPSHOT_FORBID_CAM_MOVE_OR_ROTATE, forbidToastColdDownTime })
            self.m_forbidToastColdDown = Time.time + forbidToastColdDownTime
        end
        return
    end
    local rollAlignedDir = self:GetRollAlignedDir(dir)
    self.m_curMoveDir.x = rollAlignedDir.x
    self.m_curMoveDir.y = rollAlignedDir.y

end


SnapshotCameraCtrl.RotateCamera = HL.Method(HL.Number, HL.Number) << function(self, deltaX, deltaY)
    if self.m_isForbidCamMoveOrRotate then
        if self.m_forbidToastColdDown < Time.time then
            Notify(MessageConst.SHOW_TOAST, { Language.LUA_SNAPSHOT_FORBID_CAM_MOVE_OR_ROTATE, forbidToastColdDownTime })
            self.m_forbidToastColdDown = Time.time + forbidToastColdDownTime
        end
        return
    end
    if not snapshotSystem.camController then
        return
    end
    snapshotSystem.camController.rotationMode = RotationModeEnum.PostRotate
    snapshotSystem.camController:OnInput(deltaX, deltaY)
end


SnapshotCameraCtrl.SurroundMoveCamera = HL.Method(HL.Number, HL.Number) << function(self, deltaX, deltaY)
    if self.m_isForbidCamMoveOrRotate then
        if self.m_forbidToastColdDown < Time.time then
            Notify(MessageConst.SHOW_TOAST, { Language.LUA_SNAPSHOT_FORBID_CAM_MOVE_OR_ROTATE, forbidToastColdDownTime })
            self.m_forbidToastColdDown = Time.time + forbidToastColdDownTime
        end
        return
    end
    if not snapshotSystem.camController then
        return
    end
    local rollAlignedDir = self:GetRollAlignedDir(Vector2(deltaX, deltaY))
    snapshotSystem.camController:OnInput(rollAlignedDir.x, rollAlignedDir.y)
end


SnapshotCameraCtrl.ZoomCamera = HL.Method(HL.Number) << function(self, delta)
    if not snapshotSystem.camController then
        return
    end
    snapshotSystem.camController.manualModule:ZoomCamera(delta)
end


SnapshotCameraCtrl.SetFocalLenCamera = HL.Method(HL.Number) << function(self, value)
    if not snapshotSystem.camController then
        return
    end
    CameraManager.mainCamera.focalLength = value
end


SnapshotCameraCtrl.SetApertureCamera = HL.Method(HL.Number) << function(self, value)
    if not snapshotSystem.camController then
        return
    end
    snapshotSystem.camController:SetAperture(value)
end

SnapshotCameraCtrl.GetCameraRotationState = HL.Method().Return(HL.Any) << function(self)
    if not snapshotSystem.camController then
        return nil
    end
    return snapshotSystem.camController:GetCameraRotation()
end

SnapshotCameraCtrl.SetCameraRotationState = HL.Method(HL.Any, HL.Boolean) << function(self, rotation, isResume)
    if not snapshotSystem.camController or rotation == nil then
        return
    end
    snapshotSystem.camController:SetCameraRotation(rotation, isResume)
end

SnapshotCameraCtrl.GetCameraParamFullSnapshot = HL.Method().Return(HL.Any) << function(self)
    if not snapshotSystem.camController then
        return nil
    end
    return snapshotSystem.camController:GetCameraParamFullSnapshot()
end

SnapshotCameraCtrl.RestoreCameraParamFullSnapshot = HL.Method(HL.Any) << function(self, snapshot)
    if not snapshotSystem.camController or snapshot == nil then
        return
    end
    snapshotSystem.camController:RestoreCameraParamFullSnapshot(snapshot)
end

SnapshotCameraCtrl.GetFocalLen = HL.Method().Return(HL.Number) << function(self)
    return CameraManager.mainCamera.focalLength
end

SnapshotCameraCtrl.GetAperture = HL.Method().Return(HL.Number) << function(self)
    return CameraManager.mainCamAdditionalData.physicalParameters.aperture
end

SnapshotCameraCtrl.GetZoomScale = HL.Method().Return(HL.Number) << function(self)
    if not snapshotSystem.camController then
        return 0
    end
    return snapshotSystem.camController:GetZoomScale()
end

SnapshotCameraCtrl.SetZoomScale = HL.Method(HL.Number) << function(self, value)
    if not snapshotSystem.camController then
        return
    end
    snapshotSystem.camController:SetZoomScale(value)
end

SnapshotCameraCtrl.GetCameraOffset = HL.Method().Return(Vector3) << function(self)
    if not snapshotSystem.camController then
        return Vector3.zero
    end
    return snapshotSystem.camController:GetCameraOffset()
end

SnapshotCameraCtrl.SetCameraOffset = HL.Method(Vector3) << function(self, offset)
    if not snapshotSystem.camController then
        return
    end
    snapshotSystem.camController:SetCameraOffset(offset)
end


SnapshotCameraCtrl.GetCameraRoll = HL.Method().Return(HL.Number) << function(self)
    if not snapshotSystem.camController then return 0 end
    return snapshotSystem.camController:GetCameraRoll()
end


SnapshotCameraCtrl.GetRollAlignedDir = HL.Method(Vector2).Return(Vector2) << function(self, dir)
    local rollRad = math.rad(self:GetCameraRoll())
    local cosRoll = math.cos(rollRad)
    local sinRoll = math.sin(rollRad)
    return Vector2(dir.x * cosRoll - dir.y * sinRoll, dir.x * sinRoll + dir.y * cosRoll)
end


SnapshotCameraCtrl.SetCameraRoll = HL.Method(HL.Number) << function(self, value)
    if not snapshotSystem.camController then return end
    snapshotSystem.camController:SetCameraRoll(value)
end






SnapshotCameraCtrl.m_onStartSwipeTouchPanel = HL.Field(HL.Function)

SnapshotCameraCtrl.m_onSwipeTouchPanel = HL.Field(HL.Function)

SnapshotCameraCtrl.m_onEndSwipeTouchPanel = HL.Field(HL.Function)

SnapshotCameraCtrl.m_touchPanelStartPos = HL.Field(Vector2)

SnapshotCameraCtrl.m_touchPanelStarted = HL.Field(HL.Boolean) << false

SnapshotCameraCtrl.m_lastTouchStartTime = HL.Field(HL.Number) << 0

SnapshotCameraCtrl._OnStartSwipeTouchPanel = HL.Method(Vector2) << function(self, pos)
    self.m_touchPanelStartPos = pos
    self.m_touchPanelStarted = false
    if Time.unscaledTime - self.m_lastTouchStartTime < 0.3 then

        self.m_lastTouchStartTime = 0
    else
        self.m_lastTouchStartTime = Time.unscaledTime
    end
end

SnapshotCameraCtrl._OnSwipeTouchPanel = HL.Method(Vector2, Vector2) << function(self, delta, pos)
    if not self.m_touchPanelStartPos then
        return
    end
    if not self.m_touchPanelStarted then
        if math.abs(pos.y - self.m_touchPanelStartPos.y) > 0.3 then
            self.m_touchPanelStarted = true
        end
    end
    if self.m_touchPanelStarted then
        self:ZoomCamera(delta.y * -200)
    end
end

SnapshotCameraCtrl._OnEndSwipeTouchPanel = HL.Method() << function(self)
    self.m_touchPanelStartPos = nil
    self.m_touchPanelStarted = false
end




SnapshotCameraCtrl.SetForbidMoveOrRotate = HL.Method(HL.Boolean) << function(self, isForbid)
    self.m_isForbidCamMoveOrRotate = isForbid
    logger.info("[SnapshotCameraCtrl] 设置拍照相机平移或旋转禁用状态，当前禁用：", isForbid)
end


HL.Commit(SnapshotCameraCtrl)
