local SSStatusBarBase = require_ex('UI/Widgets/SSStatusBarBase')

SSGrowCabinStatusBar = HL.Class('SSGrowCabinStatusBar', SSStatusBarBase)


local GC_STATUS_BAR_CONST = {
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


SSGrowCabinStatusBar.SetupSwitchStateHandleFunctions = HL.Override() << function(self)
    local RoomState = CS.Beyond.Gameplay.SpaceshipSystem.RoomState
    self.m_stateHandleFuncLut = { }
    self.m_stateHandleFuncLut[RoomState.Locked] = SSGrowCabinStatusBar.SwitchLockedState
    self.m_stateHandleFuncLut[RoomState.NotBuild] = SSGrowCabinStatusBar.SwitchNotBuildState
    self.m_stateHandleFuncLut[RoomState.Working] = SSGrowCabinStatusBar.SwitchWorkingState
    self.m_stateHandleFuncLut[RoomState.CanCollect] = SSGrowCabinStatusBar.SwitchCanCollectState
    self.m_stateHandleFuncLut[RoomState.Stopped] = SSGrowCabinStatusBar.SwitchStoppedState
    self.m_stateHandleFuncLut[RoomState.Idle] = SSGrowCabinStatusBar.SwitchIdleState
end

SSGrowCabinStatusBar.SetupView = HL.Override() << function(self)
    local hasValue
    local roomInfo
    hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(self.m_roomId)
    if hasValue and roomInfo then
        local roomTypeData = Tables.spaceshipRoomTypeTable[roomInfo.type]
        local roomTitle = roomTypeData.name
        self.view.titleText.text = roomTitle
        self.view.lockedTitleText.text = roomTitle
        self.view.lockedStatusText.text = Language.LUA_SS_GC_STATUS_BAR_LOCKED
    end
end

SSGrowCabinStatusBar.SwitchLockedState = HL.Method() << function(self)

    local animator = self.view.statusBar
    animator:SetInteger(GC_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, GC_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.LOCKED)
end
SSGrowCabinStatusBar.SwitchNotBuildState = HL.Method() << function(self)
    self.view.statusText.text = Language.LUA_SS_GC_STATUS_BAR_NOT_BUILD

    local animator = self.view.statusBar
    animator:SetInteger(GC_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, GC_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.NOT_BUILD)
end
SSGrowCabinStatusBar.SwitchWorkingState = HL.Method() << function(self)
    self.view.statusText.text = Language.LUA_SS_GC_STATUS_BAR_WORKING

    local animator = self.view.statusBar
    animator:SetInteger(GC_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, GC_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.WORKING)
end
SSGrowCabinStatusBar.SwitchCanCollectState = HL.Method() << function(self)
    self.view.statusText.text = Language.LUA_SS_GC_STATUS_BAR_CAN_COLLECT

    local animator = self.view.statusBar
    animator:SetInteger(GC_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, GC_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.CAN_COLLECT)
end
SSGrowCabinStatusBar.SwitchStoppedState = HL.Method() << function(self)
    self.view.statusText.text = Language.LUA_SS_GC_STATUS_BAR_STOPPED

    local animator = self.view.statusBar
    animator:SetInteger(GC_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, GC_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.STOPPED)
end
SSGrowCabinStatusBar.SwitchIdleState = HL.Method() << function(self)
    self.view.statusText.text = Language.LUA_SS_GC_STATUS_BAR_IDLE

    local animator = self.view.statusBar
    animator:SetInteger(GC_STATUS_BAR_CONST.ANIMATOR_STATE_NAME, GC_STATUS_BAR_CONST.ANIMATOR_STATE_VAL.IDLE)
end

HL.Commit(SSGrowCabinStatusBar)
return SSGrowCabinStatusBar

