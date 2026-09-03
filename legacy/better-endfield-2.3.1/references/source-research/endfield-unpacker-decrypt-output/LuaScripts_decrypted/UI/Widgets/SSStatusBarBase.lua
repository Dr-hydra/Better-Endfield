local LevelWorldUIBase = require_ex('UI/Widgets/LevelWorldUIBase')

SSStatusBarBase = HL.Class('SSStatusBarBase', LevelWorldUIBase)

SSStatusBarBase.m_stateHandleFuncLut = HL.Field(HL.Table)

SSStatusBarBase.m_roomId = HL.Field(HL.String) << ""

SSStatusBarBase.m_roomIds = HL.Field(HL.Table)


SSStatusBarBase.m_currentState = HL.Field(CS.Beyond.Gameplay.SpaceshipSystem.RoomState)


SSStatusBarBase._OnFirstTimeInit = HL.Override() << function(self)
    self:SetupSwitchStateHandleFunctions()
end

SSStatusBarBase.InitLevelWorldUi = HL.Override(HL.Any) << function(self, args)
    self:_FirstTimeInit()
    if not string.isEmpty(self.m_roomId) then
        self:OnLevelWorldUiReleased()
    end
    self.m_roomIds = {}
    for i = CSIndex(1), CSIndex(args.Count) do
        table.insert(self.m_roomIds, args[i])
    end
    self:RefreshRoomId()
    self.m_currentState = CS.Beyond.Gameplay.SpaceshipSystem.RoomState.Locked
    self:SetupView()
    local args = {}
    args.roomId = self.m_roomId
    args.statusBar = self
    Notify(MessageConst.SS_REGISTER_STATUS_BAR, args)
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.SpaceshipSystem) then
        self.gameObject:SetActiveIfNecessary(false)
    end
end

SSStatusBarBase.RefreshRoomId = HL.Virtual() << function(self)
    local spaceshipSystem = GameInstance.player.spaceship
    local lastRoomId = self.m_roomId
    self.m_roomIds = self.m_roomIds or {}

    for i = #self.m_roomIds, 1, -1 do
        local isBuild = spaceshipSystem:IsRoomBuild(self.m_roomIds[i])
        if isBuild then
            if self.m_roomIds[i] ~= self.m_roomId then
                Notify(MessageConst.SS_UNREGISTER_STATUS_BAR, self.m_roomId)
                self.m_roomId = self.m_roomIds[i]
                local args = {}
                args.statusBar = self
                args.roomId = self.m_roomId
                Notify(MessageConst.SS_REGISTER_STATUS_BAR, args)
            end
            return
        end
    end

    self.m_roomId = self.m_roomIds[1]

    if not string.isEmpty(lastRoomId) and self.m_roomId ~= lastRoomId  then
        self.m_currentState = CS.Beyond.Gameplay.SpaceshipSystem.RoomState.Locked
        self:SetupView()
        local args = {}
        args.roomId = self.m_roomId
        args.statusBar = self
        Notify(MessageConst.SS_REGISTER_STATUS_BAR, args)
    end
end



SSStatusBarBase.SetupSwitchStateHandleFunctions = HL.Virtual() << function(self)

end

SSStatusBarBase.SetupView = HL.Virtual() << function(self)

end

SSStatusBarBase.OnLevelWorldUiReleased = HL.Override() << function(self)
    Notify(MessageConst.SS_UNREGISTER_STATUS_BAR, self.m_roomId)
end

SSStatusBarBase.SwitchRoomState = HL.Method(HL.String, CS.Beyond.Gameplay.SpaceshipSystem.RoomState) << function(self, roomId, roomState)
    if self.m_stateHandleFuncLut and self.m_stateHandleFuncLut[roomState] then

        self.m_stateHandleFuncLut[roomState](self, roomId)
    else
        self:DefaultStateHandle(roomId, roomState)
    end
    self.m_currentState = roomState
end

SSStatusBarBase.DefaultStateHandle = HL.Virtual(HL.String, CS.Beyond.Gameplay.SpaceshipSystem.RoomState) << function(self, roomId, roomState)

end

HL.Commit(SSStatusBarBase)
return SSStatusBarBase

