local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SnapshotJoystick

SnapshotJoystickCtrl = HL.Class('SnapshotJoystickCtrl', uiCtrl.UICtrl)






SnapshotJoystickCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SNAPSHOT_PLAYER_MOVE_MODE] = 'SetPlayerMoveMode',
    [MessageConst.SNAPSHOT_CAMERA_MOVE_MODE] = 'SetCameraMoveMode',
    [MessageConst.SNAPSHOT_INNER_FORBID_PLAYER_MOVE] = 'OnInnerForbidPlayerMove',

    [MessageConst.FORBID_SYSTEM_CHANGED] = 'OnForbidSystemChanged',
    [MessageConst.ON_GAME_SETTING_CHANGED] = 'OnGameSettingChanged',
}


local snapshotSystem = GameInstance.player.snapshotSystem
local forbidToastColdDownTime = 3
local JOYSTICK_TOUCH_ROUTE = {
    None = 0,
    CharWalk = 1,
    SnapshotCharPanMove = 2,
    SnapshotCharPanMoveBlockedByDrag = 3,
}

local GAMEPAD_ROTATE_STICK_DEAD_SQ = 0.04



SnapshotJoystickCtrl.m_updateKey = HL.Field(HL.Number) << -1

SnapshotJoystickCtrl.m_isPlayerMoveMode = HL.Field(HL.Boolean) << true

SnapshotJoystickCtrl.m_cameraCtrl = HL.Field(HL.Forward("SnapshotCameraCtrl"))

SnapshotJoystickCtrl.m_isForbidPlayerMoveFromSnapshot = HL.Field(HL.Boolean) << false

SnapshotJoystickCtrl.m_forbidToastColdDown = HL.Field(HL.Number) << 0

SnapshotJoystickCtrl.m_joystickTouchRoute = HL.Field(HL.Number) << JOYSTICK_TOUCH_ROUTE.None

SnapshotJoystickCtrl.isInGamepadRotateSubMode = HL.Field(HL.Boolean) << false






SnapshotJoystickCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_cameraCtrl = self.m_phase.snapshotCameraPanel.uiCtrl
    self.m_isPlayerMoveMode = not snapshotSystem.isCameraMoveMode
    self.isInGamepadRotateSubMode = false

    self.view.joystick.onTouchStart:AddListener(function()
        self:_onJoystickTouchStart()
    end)
    self.view.joystick.onTouchEnd:AddListener(function()
        self:_onJoystickTouchEnd()
    end)
    self.view.joystick.onToggleAutoSprint:AddListener(function(isAutoSprint)
        self:_ToggleAutoSprint(isAutoSprint)
    end)

    self.view.sprintBtn.onPressStart:AddListener(function()
        self:_OnPressSprint()
    end)
    self.view.sprintBtn.onPressEnd:AddListener(function()
        self:_OnReleaseSprint()
    end)
    if DeviceInfo.usingController then
        UIUtils.bindInputPlayerAction("snapshot_gamepad_start_rotate_char", function()
            self:_TryEnterGamepadRotateSubMode()
        end, self.view.inputGroup.groupId)
        UIUtils.bindInputPlayerAction("snapshot_gamepad_end_rotate_char", function()
            self:_ExitGamepadRotateSubMode()
        end, self.view.inputGroup.groupId)
    end

    self.view.joystick.walkRation = DataManager.movementSetting.walkRunStickRatio
    self.view.sprintBtn.gameObject:SetActive(not GameInstance.player.forbidSystem:IsForbidden(ForbidType.ForbidSprint))
    self:OnForbidJoystick(self.m_phase.isForbidJoystick)

    self:_UpdateWalkRunRation()
end

SnapshotJoystickCtrl.OnShow = HL.Override() << function(self)
    self:_AddRegisters()
end

SnapshotJoystickCtrl.OnHide = HL.Override() << function(self)
    self:_ClearRegisters()
end


SnapshotJoystickCtrl.OnClose = HL.Override() << function(self)
    self:_ClearRegisters()
end




SnapshotJoystickCtrl._AddRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Add("Tick", function()
        self:_UpdateMove()
    end)
end

SnapshotJoystickCtrl._ClearRegisters = HL.Method() << function(self)
    self.isInGamepadRotateSubMode = false
    self:_EndMoveCharJoystickControl()
    self:_StopCharWalkAndJoystickSprint()
    self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.None
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
end




SnapshotJoystickCtrl._onJoystickTouchStart = HL.Method() << function(self)
    self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.None
    local isSelectMoveChar = snapshotSystem:GetMoveSelectedSlotIndex() >= 0
    if snapshotSystem.isFirstPersonMode and not isSelectMoveChar then
        return
    end
    if not self.m_isPlayerMoveMode then
        return
    end
    if snapshotSystem.isMoveCharMovingByDrag then
        self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMoveBlockedByDrag
        self:_StopCharWalkAndJoystickSprint()
        return
    end
    if self:_TryBeginMoveCharJoystickControl() then
        return
    end
    if self.m_isPlayerMoveMode then

        self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.CharWalk
        GameInstance.playerController:ProduceMoveCommand()
    end
end

SnapshotJoystickCtrl._onJoystickTouchEnd = HL.Method() << function(self)
    if self.m_joystickTouchRoute == JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMove then
        self:_EndMoveCharJoystickControl()
        return
    end
    if self.m_joystickTouchRoute == JOYSTICK_TOUCH_ROUTE.CharWalk then

        GameInstance.playerController:ConsumeMoveCommand()
    end
    self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.None
end

SnapshotJoystickCtrl._UpdateMove = HL.Method() << function(self)
    local isForbid = false
    local isSelectMoveChar = snapshotSystem:GetMoveSelectedSlotIndex() >= 0
    if isSelectMoveChar then
        isForbid = self.m_phase.isForbidJoystick
    else
        isForbid = snapshotSystem.isFirstPersonMode or self.m_phase.isForbidJoystick
    end
    if isForbid then
        self.isInGamepadRotateSubMode = false
        self:_EndMoveCharJoystickControl()
        return
    end
    local dir = self.view.joystick.jsValue
    if dir == Vector2.zero then
        return
    end
    if not self.m_isPlayerMoveMode then

        self.m_cameraCtrl:MoveCameraInPlane(dir)
        return
    end

    local isForbidMoveFromSys = GameInstance.player.forbidSystem:IsForbidden(ForbidType.ForbidMove)
    if self.m_isForbidPlayerMoveFromSnapshot or isForbidMoveFromSys then
        if self.m_forbidToastColdDown < Time.time then
            Notify(MessageConst.SHOW_TOAST, { Language.LUA_SNAPSHOT_FORBID_PLAYER_MOVE, forbidToastColdDownTime })
            self.m_forbidToastColdDown = Time.time + forbidToastColdDownTime
        end
        return
    end

    if snapshotSystem.isMoveCharMovingByDrag then
        if self.m_joystickTouchRoute == JOYSTICK_TOUCH_ROUTE.CharWalk then
            self:_StopCharWalkAndJoystickSprint()
            self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMoveBlockedByDrag
        end
        return
    end

    if self:_ShouldJoystickMoveSelectedChar() then

        if self.isInGamepadRotateSubMode then
            local rollAlignedDir = self.m_cameraCtrl:GetRollAlignedDir(dir)
            self:_UpdateMoveCharByJoystickRotate(rollAlignedDir)
            return
        end

        if self.m_joystickTouchRoute == JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMove then
            snapshotSystem:UpdateMoveCharByJoystick(dir)
        elseif self.m_joystickTouchRoute == JOYSTICK_TOUCH_ROUTE.CharWalk then
            self:_StopCharWalkAndJoystickSprint()
            if snapshotSystem:BeginMoveCharByJoystick() then
                self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMove
                snapshotSystem:UpdateMoveCharByJoystick(dir)
            end
        end
        return
    end
    if self.m_joystickTouchRoute ~= JOYSTICK_TOUCH_ROUTE.CharWalk then
        return
    end

    self:_StopMainControlCustomActionOnPlayerInput(true)
    GameInstance.playerController:UpdateMoveCommand(dir)
end

SnapshotJoystickCtrl.SetPlayerMoveMode = HL.Method() << function(self)
    self.m_isPlayerMoveMode = true
end

SnapshotJoystickCtrl.SetCameraMoveMode = HL.Method() << function(self)
    self.m_isPlayerMoveMode = false
    self:_StopCharWalkAndJoystickSprint()
end

SnapshotJoystickCtrl._OnPressSprint = HL.Method() << function(self)
    if snapshotSystem.isFirstPersonMode then
        return
    end
    if Utils.isForbidden(ForbidType.ForbidMove) or self.m_isForbidPlayerMoveFromSnapshot then
        return
    end
    if self:_ShouldBlockSprintForMoveChar() then
        GameInstance.playerController:OnSprintReleased()
        GameInstance.playerController:OnJoystickSprint(false)
        return
    end
    self:_StopMainControlCustomActionOnPlayerInput(false)
    GameInstance.playerController:OnSprintPressed()
end

SnapshotJoystickCtrl._OnReleaseSprint = HL.Method() << function(self)
    if snapshotSystem.isFirstPersonMode then
        return
    end
    GameInstance.playerController:OnSprintReleased()
end

SnapshotJoystickCtrl._ToggleAutoSprint = HL.Method(HL.Boolean) << function(self, isAutoSprint)
    if snapshotSystem.isFirstPersonMode then
        return
    end
    if self:_ShouldBlockSprintForMoveChar() then
        GameInstance.playerController:OnJoystickSprint(false)
        return
    end
    if not CS.Beyond.GameSetting.controllerCachedAutoSprint then
        return
    end
    GameInstance.playerController:OnJoystickSprint(isAutoSprint)
end

SnapshotJoystickCtrl._UpdateWalkRunRation = HL.Method() << function(self)
    self.view.joystick.walkRation = CS.Beyond.GameSetting.controllerCachedWalkRunRatio
end






SnapshotJoystickCtrl._ShouldJoystickMoveSelectedChar = HL.Method().Return(HL.Boolean) << function(self)



    local selectedSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if selectedSlot < 0 then
        return false
    end
    if DeviceInfo.usingController then
        return true
    end
    return false
end



SnapshotJoystickCtrl._TryBeginMoveCharJoystickControl = HL.Method().Return(HL.Boolean) << function(self)
    if not self:_ShouldJoystickMoveSelectedChar() then
        return false
    end
    if not snapshotSystem:CanMoveCharByJoystick() then
        self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMoveBlockedByDrag
        return true
    end
    if snapshotSystem:BeginMoveCharByJoystick() then
        self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMove
        self:_StopCharWalkAndJoystickSprint()
    else
        self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.None
    end
    return true
end


SnapshotJoystickCtrl._TryEnterGamepadRotateSubMode = HL.Method() << function(self)
    local snapshotPanelCtrl = self.m_phase.snapshotPanel and self.m_phase.snapshotPanel.uiCtrl
    if snapshotPanelCtrl == nil or not snapshotPanelCtrl:IsInGamepadMoveRotateMode() then
        return
    end
    if self.isInGamepadRotateSubMode then
        return
    end

    self.isInGamepadRotateSubMode = true
    self:_EndMoveCharJoystickControl()
    self:_StopCharWalkAndJoystickSprint()
    self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.None
end


SnapshotJoystickCtrl._ExitGamepadRotateSubMode = HL.Method() << function(self)
    if not self.isInGamepadRotateSubMode then
        return
    end
    self.isInGamepadRotateSubMode = false
end


SnapshotJoystickCtrl._UpdateMoveCharByJoystickRotate = HL.Method(Vector2) << function(self, dir)
    if dir.sqrMagnitude < GAMEPAD_ROTATE_STICK_DEAD_SQ then
        return
    end

    local yaw = self:_CalcGamepadRotateYaw(dir)
    if yaw ~= nil then
        snapshotSystem:SetRotateCharYaw(yaw)
    end
end


SnapshotJoystickCtrl._CalcGamepadRotateYaw = HL.Method(Vector2).Return(HL.Opt(HL.Number)) << function(self, stick)
    local camera = CameraManager.mainCamera
    if IsNull(camera) then
        return nil
    end

    local fwd = camera.transform.forward
    fwd = Vector3(fwd.x, 0, fwd.z)
    if fwd.sqrMagnitude < 1e-6 then

        local up = camera.transform.up
        fwd = Vector3(up.x, 0, up.z)
    end

    fwd = fwd.normalized
    local right = Vector3.Cross(Vector3.up, fwd).normalized
    local dir = right * stick.x + fwd * stick.y
    return math.atan(dir.x, dir.z) * 180 / math.pi
end


SnapshotJoystickCtrl._EndMoveCharJoystickControl = HL.Method() << function(self)
    if self.m_joystickTouchRoute ~= JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMove then
        return
    end
    snapshotSystem:EndMoveCharByJoystick()
    self.m_joystickTouchRoute = JOYSTICK_TOUCH_ROUTE.None
end


SnapshotJoystickCtrl._ShouldBlockSprintForMoveChar = HL.Method().Return(HL.Boolean) << function(self)
    if snapshotSystem.isMoveCharMovingByDrag then
        return true
    end
    if self.m_joystickTouchRoute == JOYSTICK_TOUCH_ROUTE.SnapshotCharPanMove then
        return true
    end
    return self:_ShouldJoystickMoveSelectedChar()
end

SnapshotJoystickCtrl._StopCharWalkAndJoystickSprint = HL.Method() << function(self)
    GameInstance.playerController:UpdateMoveCommand(Vector2.zero)
    GameInstance.playerController:ConsumeMoveCommand()
    GameInstance.playerController:OnSprintReleased()
    GameInstance.playerController:OnJoystickSprint(false)
end


SnapshotJoystickCtrl._StopMainControlCustomActionOnPlayerInput = HL.Method(HL.Boolean) << function(self, showMoveToast)
    local squadManager = GameInstance.player.squadManager
    local mainCharacter = GameInstance.playerController.mainCharacter
    local mainCharSlotIndex = squadManager:GetMemberIndex(mainCharacter)
    if mainCharSlotIndex < 0 or not snapshotSystem:IsPlayingAction(mainCharSlotIndex) then
        return
    end

    snapshotSystem:StopAction(mainCharSlotIndex)

    if showMoveToast then
        local charCfg = Tables.characterTable[mainCharacter.templateData.id]
        Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_SNAPSHOT_ACTION_RESET_BY_MOVE_TOAST, charCfg.name))
    end


    local snapshotPanelCtrl = self.m_phase.snapshotPanel and self.m_phase.snapshotPanel.uiCtrl
    if snapshotPanelCtrl then
        snapshotPanelCtrl:_OnMainControlActionInterrupted(mainCharSlotIndex)
    end
end

SnapshotJoystickCtrl.OnForbidSystemChanged = HL.Method(HL.Any) << function(self, args)
    local forbidType, isForbid = unpack(args)
    if forbidType == ForbidType.ForbidMove then
        self:OnInnerForbidPlayerMove(isForbid)
        if isForbid then
            if self.m_isPlayerMoveMode then
                GameInstance.playerController:UpdateMoveCommand(Vector2.zero)
            end
        end
    end

    if forbidType == ForbidType.ForbidSprint then
        if isForbid then
            self.view.sprintBtn.gameObject:SetActive(false)
            self:_OnReleaseSprint()
        else
            self.view.sprintBtn.gameObject:SetActive(true)
        end
    end
end

SnapshotJoystickCtrl.OnForbidJoystick = HL.Method(HL.Boolean) << function(self, isForbid)
    if isForbid then
        self:_EndMoveCharJoystickControl()
        if self.m_isPlayerMoveMode then
            self:_StopCharWalkAndJoystickSprint()
        end
    end
    self.view.mainCanvasGroup.alpha = isForbid and 0 or 1
    self.view.graphicRaycaster.enabled = not isForbid
end

SnapshotJoystickCtrl.OnInnerForbidPlayerMove = HL.Method(HL.Boolean) << function(self, isForbid)
    self.m_isForbidPlayerMoveFromSnapshot = isForbid
end

SnapshotJoystickCtrl.OnGameSettingChanged = HL.Method(HL.Number) << function(self, reason)
    if reason == UIConst.GameSettingChangeReason.Default then
        self:_UpdateWalkRunRation()
    end
end


HL.Commit(SnapshotJoystickCtrl)
