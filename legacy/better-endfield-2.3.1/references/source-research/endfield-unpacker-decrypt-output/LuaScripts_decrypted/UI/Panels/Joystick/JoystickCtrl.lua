local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Joystick
JoystickCtrl = HL.Class('JoystickCtrl', uiCtrl.UICtrl)







JoystickCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_TOGGLE_DEBUG_FLY] = "OnToggleDebugFly",
    [MessageConst.ON_TOGGLE_VIRTUAL_MOUSE] = 'OnToggleVirtualMouse',
    [MessageConst.ON_GAME_SETTING_CHANGED] = 'OnGameSettingChanged',
    [MessageConst.FORBID_SYSTEM_CHANGED] = 'OnForbidSystemChange',
}

JoystickCtrl.m_update = HL.Field(HL.Function)


JoystickCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if BEYOND_DEBUG then

        self:BindInputEvent(CS.Beyond.Input.KeyboardKeyCode.F5, function()
            InputManagerInst:ToggleDebugCursorMode()
        end, "a")

        if CS.Beyond.DebugDefines.disableF5Mode then
            if InputManagerInst.isDebugForceShow then
                InputManagerInst:ToggleDebugCursorMode()
            end
        end

        if DeviceInfo.usingTouch then
            if InputManagerInst.isDebugForceShow then
                InputManagerInst:ToggleDebugCursorMode()
            end
        end
    end

    self.m_update = function()
        self:_Update()
    end

    self:BindInputPlayerAction("common_toggle_walk", function()
        self:_ToggleWalk()
    end)

    self.view.joystick.onTouchStart:AddListener(function()
        GameInstance.playerController:ProduceMoveCommand()
    end)

    self.view.joystick.onTouchEnd:AddListener(function()
        GameInstance.playerController:ConsumeMoveCommand()
    end)

    self.view.joystick.onToggleAutoSprint:AddListener(function(isAutoSprint)
        self:_ToggleAutoSprint(isAutoSprint)
    end)

    self:_UpdateWalkRunRation()
    self:_UpdateMoveForbidStatus()
end

JoystickCtrl.OnShow = HL.Override() << function(self)
    self:_AddRegisters()
end

JoystickCtrl.OnHide = HL.Override() << function(self)
    self:_ClearRegisters()
end


JoystickCtrl.OnClose = HL.Override() << function(self)
    self:_ClearRegisters()
    if BEYOND_DEBUG and GameInstance.playerController.inFlyMode then
        CS.Beyond.Gameplay.Core.PlayerController.ToggleFlyingMode()
    end
end

JoystickCtrl.OnGameSettingChanged = HL.Method(HL.Number) << function(self, reason)
    if reason == UIConst.GameSettingChangeReason.Default then
        self:_UpdateWalkRunRation()
    end
end

JoystickCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, inputEnabled)
    if inputEnabled then

        self:_StartCoroutine(function()
            coroutine.step()
            if self.view.inputGroup.groupEnabled then
                self.view.joystick:CheckShouldActive()
                if self.view.joystick.active then
                    UIManager.commonTouchPanel:DeActiveTouch(self.view.joystick.activeTouchId)
                end
            end
        end)
    end
end



JoystickCtrl._AddRegisters = HL.Method() << function(self)
    if InputManagerInst.afterCheckInput then
        InputManagerInst.afterCheckInput = InputManagerInst.afterCheckInput + self.m_update
    else
        InputManagerInst.afterCheckInput = self.m_update
    end
end

JoystickCtrl._ClearRegisters = HL.Method() << function(self)
    GameInstance.playerController:UpdateMoveCommand(Vector2.zero)
    if InputManagerInst.afterCheckInput then
        InputManagerInst.afterCheckInput = InputManagerInst.afterCheckInput - self.m_update
    end
end






JoystickCtrl.m_isMoveForbid = HL.Field(HL.Boolean) << false

JoystickCtrl._Update = HL.Method() << function(self)
    self:_UpdateMove()
end

JoystickCtrl._UpdateMove = HL.Method() << function(self)
    if not self:CanPlayerMove() then
        return
    end

    local dir = self.view.joystick.jsValue
    if LuaSystemManager.factory.inTopView then
        if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.LeftControl) then

            return
        end
        local spd = InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.LeftShift) and 35 or 15
        LuaSystemManager.factory:MoveTopViewCamTarget(dir * spd * Time.deltaTime)
    else
        GameInstance.playerController:UpdateMoveCommand(dir)
    end
end

JoystickCtrl._ToggleWalk = HL.Method() << function(self)
    if FactoryUtils.isInTopView() then
        return
    end
    GameInstance.playerController:ToggleWalk()
end

JoystickCtrl._ToggleAutoSprint = HL.Method(HL.Boolean) << function(self, isAutoSprint)
    if not CS.Beyond.GameSetting.controllerCachedAutoSprint then
        return
    end
    GameInstance.playerController:OnJoystickSprint(isAutoSprint)
end

JoystickCtrl._UpdateWalkRunRation = HL.Method() << function(self)
    self.view.joystick.walkRation = CS.Beyond.GameSetting.controllerCachedWalkRunRatio
end

JoystickCtrl.OnForbidSystemChange = HL.Method(HL.Any) << function(self, args)
    local forbidType, isForbid = unpack(args)
    if forbidType == ForbidType.ForbidMove then
        if isForbid then
            GameInstance.playerController:UpdateMoveCommand(Vector2.zero)
        end
    end
    self:_UpdateMoveForbidStatus()
end

JoystickCtrl.OnToggleVirtualMouse = HL.Method(HL.Table) << function(self, args)
    local isActive = unpack(args)
    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidMove, "VirtualMouse", isActive);
end

JoystickCtrl._UpdateMoveForbidStatus = HL.Method() << function(self)
    self.m_isMoveForbid = Utils.isForbidden(ForbidType.ForbidMove)
end

JoystickCtrl.CanPlayerMove = HL.Method().Return(HL.Boolean) << function(self)
    return not self.m_isMoveForbid
end






JoystickCtrl.m_flyModeUpPressKey = HL.Field(HL.Number) << -1

JoystickCtrl.m_flyModeUpReleaseKey = HL.Field(HL.Number) << -1

JoystickCtrl.m_flyModeDownPressKey = HL.Field(HL.Number) << -1

JoystickCtrl.m_flyModeDownReleaseKey = HL.Field(HL.Number) << -1

JoystickCtrl.OnToggleDebugFly = HL.Method() << function(self)
    if BEYOND_DEBUG_COMMAND then
        local inFlyMode = GameInstance.playerController.inFlyMode
        self:DeleteInputBinding(self.m_flyModeUpPressKey)
        self:DeleteInputBinding(self.m_flyModeUpReleaseKey)
        self:DeleteInputBinding(self.m_flyModeDownPressKey)
        self:DeleteInputBinding(self.m_flyModeDownReleaseKey)

        if inFlyMode then

            self.m_flyModeDownPressKey = self:BindInputPlayerAction("common_debug_fly_down_start", function()
                GameInstance.playerController:ToggleFly(-1.0, true)
            end)


            self.m_flyModeDownReleaseKey = self:BindInputPlayerAction("common_debug_fly_down_end", function()
                GameInstance.playerController:ToggleFly(-1.0, false)
            end)


            self.m_flyModeUpPressKey = self:BindInputPlayerAction("common_debug_fly_up_start", function()
                GameInstance.playerController:ToggleFly(1.0, true)
            end)


            self.m_flyModeUpReleaseKey = self:BindInputPlayerAction("common_debug_fly_up_end", function()
                GameInstance.playerController:ToggleFly(1.0, false)
            end)
        end

        Notify(MessageConst.SHOW_TOAST, inFlyMode and "角色飞行模式 开" or "角色飞行模式 关")
    end
end




HL.Commit(JoystickCtrl)
