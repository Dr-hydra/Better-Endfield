local SSStatusBarBase = require_ex('UI/Widgets/SSStatusBarBase')

SSManufacturingStatusBar = HL.Class('SSManufacturingStatusBar', SSStatusBarBase)


local MFR_STATUS_BAR_CONST = {
    ANIMATOR_STATE_NAME = "state",
    ANIMATOR_STATE_VAL = {
        LOCKED = 0,
        NOT_BUILD = 1,
        WORKING = 2,
        CAN_COLLECT = 3,
        STOPPED = 4,
        IDLE = 5,
    },
}

SSManufacturingStatusBar.SetupSwitchStateHandleFunctions = HL.Override() << function(self)
    local RoomState = CS.Beyond.Gameplay.SpaceshipSystem.RoomState
    self.m_stateHandleFuncLut = { }
    self.m_stateHandleFuncLut[RoomState.Locked] = SSManufacturingStatusBar.SwitchLockedState
    self.m_stateHandleFuncLut[RoomState.NotBuild] = SSManufacturingStatusBar.SwitchNotBuildState
    self.m_stateHandleFuncLut[RoomState.Working] = SSManufacturingStatusBar.SwitchWorkingState
    self.m_stateHandleFuncLut[RoomState.CanCollect] = SSManufacturingStatusBar.SwitchCanCollectState
    self.m_stateHandleFuncLut[RoomState.Stopped] = SSManufacturingStatusBar.SwitchStoppedState
    self.m_stateHandleFuncLut[RoomState.Idle] = SSManufacturingStatusBar.SwitchIdleState
end

SSManufacturingStatusBar.SetupView = HL.Override() << function(self)

    local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(self.m_roomId)
    if hasValue and roomInfo then
        local roomTypeData = Tables.spaceshipRoomTypeTable[roomInfo.type]
        local roomTitle = roomTypeData.name
        self.view.titleText.text = roomTitle
        self.view.lockedTitleText.text = roomTitle
    end
    self.view.lockedStatusText.text = Language.LUA_SS_MFR_STATUS_BAR_LOCKED
end

SSManufacturingStatusBar.SwitchLockedState = HL.Method() << function(self)
    self.view.notUnlocked.gameObject:SetActive(true)
    self.view.statusBar.gameObject:SetActive(false)

    local animator = self.view.statusBar
    animator:SetInteger(MFR_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, MFR_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.LOCKED)
end

SSManufacturingStatusBar.SwitchUnlocked = HL.Method() << function(self)
    if self.m_currentState == CS.Beyond.Gameplay.SpaceshipSystem.RoomState.Locked then

        self.view.notUnlocked.gameObject:SetActive(false)
        self.view.statusBar.gameObject:SetActive(true)
    end
end

SSManufacturingStatusBar.SwitchNotBuildState = HL.Method() << function(self)
    self:SwitchUnlocked()
    self.view.statusText.text = Language.LUA_SS_MFR_STATUS_BAR_NOT_BUILD

    local animator = self.view.statusBar
    animator:SetInteger(MFR_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, MFR_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.NOT_BUILD)
end
SSManufacturingStatusBar.SwitchWorkingState = HL.Method() << function(self)
    self:SwitchUnlocked()
    self.view.statusText.text = Language.LUA_SS_MFR_STATUS_BAR_WORKING

    local animator = self.view.statusBar
    animator:SetInteger(MFR_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, MFR_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.WORKING)
end
SSManufacturingStatusBar.SwitchCanCollectState = HL.Method() << function(self)
    self:SwitchUnlocked()
    self.view.statusText.text = Language.LUA_SS_MFR_STATUS_BAR_CAN_COLLECT

    local animator = self.view.statusBar
    animator:SetInteger(MFR_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, MFR_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.CAN_COLLECT)
end
SSManufacturingStatusBar.SwitchStoppedState = HL.Method() << function(self)
    self:SwitchUnlocked()
    self.view.statusText.text = Language.LUA_SS_MFR_STATUS_BAR_STOPPED

    local animator = self.view.statusBar
    animator:SetInteger(MFR_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, MFR_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.STOPPED)
end
SSManufacturingStatusBar.SwitchIdleState = HL.Method() << function(self)
    self:SwitchUnlocked()
    self.view.statusText.text = Language.LUA_SS_MFR_STATUS_BAR_IDLE

    local animator = self.view.statusBar
    animator:SetInteger(MFR_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, MFR_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.IDLE)
end

HL.Commit(SSManufacturingStatusBar)
return SSManufacturingStatusBar