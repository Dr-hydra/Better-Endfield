local SSStatusBarBase = require_ex('UI/Widgets/SSStatusBarBase')

SSRoomInfoPanel = HL.Class('SSRoomInfoPanel', SSStatusBarBase)
local RoomState = CS.Beyond.Gameplay.SpaceshipSystem.RoomState

SSRoomInfoPanel.m_roomType = HL.Field(GEnums.SpaceshipRoomType)

local STATE_VAL = {
    Making = "Making",
    PendingCollection = "PendingCollection",
    Stopping = "Stopping",
    Freeing = "Freeing",
    CluesCollecting = "CluesCollecting",
    PendingCollectionClues = "PendingCollectionClues",
    PendingCommunication = "PendingCommunication",
    Moving = "Moving",
    PendingBuild = "PendingBuild",
    UnOpen = "UnOpen",
    Growing = "Growing",
    None = "None",
}

local STATUS_BAR_CONST = {
    ANIMATOR_STATE_VAL = {
        [RoomState.Locked] = 0,
        [RoomState.NotBuild] = 1,
        [RoomState.Working] = 2,
        [RoomState.CanCollect] = 3,
        [RoomState.Stopped] = 4,
        [RoomState.Idle] = 5,
        [RoomState.WaitForExchange] = 3,
    },
    ANIMATOR_STATE_NAME = "state",
    ROOM_TYPE_VAL = {
        [GEnums.SpaceshipRoomType.ManufacturingStation] = "ManufacturingStation",
        [GEnums.SpaceshipRoomType.GuestRoom] = "GuestRoom",
        [GEnums.SpaceshipRoomType.CommandCenter] = "CommandCenter",
        [GEnums.SpaceshipRoomType.FlexibleTypeA] = "FlexibleTypeA",
        [GEnums.SpaceshipRoomType.FlexibleTypeB] = "FlexibleTypeB",
        [GEnums.SpaceshipRoomType.GrowCabin] = "GrowCabin",
        [GEnums.SpaceshipRoomType.GuestRoomClueExtension] = "GuestRoom",
    },
    ROOM_TYPE_STATE_VAL = {
        [GEnums.SpaceshipRoomType.ManufacturingStation] = {
            [RoomState.Locked] = STATE_VAL.UnOpen,
            [RoomState.NotBuild] = STATE_VAL.PendingBuild,
            [RoomState.Working] = STATE_VAL.Making,
            [RoomState.CanCollect] = STATE_VAL.PendingCollection,
            [RoomState.Stopped] = STATE_VAL.Stopping,
            [RoomState.Idle] = STATE_VAL.Freeing,
        },
        [GEnums.SpaceshipRoomType.GrowCabin] = {
            [RoomState.Locked] = STATE_VAL.UnOpen,
            [RoomState.NotBuild] = STATE_VAL.PendingBuild,
            [RoomState.Working] = STATE_VAL.Growing,
            [RoomState.CanCollect] = STATE_VAL.PendingCollection,
            [RoomState.Stopped] = STATE_VAL.Stopping,
            [RoomState.Idle] = STATE_VAL.Freeing,
        },
        [GEnums.SpaceshipRoomType.GuestRoom] = {
            [RoomState.Locked] = STATE_VAL.None,
            [RoomState.NotBuild] = STATE_VAL.None,
            [RoomState.Working] = STATE_VAL.None,
            [RoomState.CanCollect] = STATE_VAL.None,
            [RoomState.Stopped] = STATE_VAL.None,
            [RoomState.Idle] = STATE_VAL.None,
        },
        [GEnums.SpaceshipRoomType.GuestRoomClueExtension] = {
            [RoomState.Locked] = STATE_VAL.UnOpen,
            [RoomState.NotBuild] = STATE_VAL.None,
            [RoomState.Working] = STATE_VAL.CluesCollecting,
            [RoomState.CanCollect] = STATE_VAL.PendingCollection,
            [RoomState.Stopped] = STATE_VAL.Stopping,
            [RoomState.Idle] = STATE_VAL.Freeing,
            [RoomState.WaitForExchange] = STATE_VAL.PendingCommunication,
        },
        [GEnums.SpaceshipRoomType.CommandCenter] = {
            [RoomState.Locked] = STATE_VAL.None,
            [RoomState.NotBuild] = STATE_VAL.None,
            [RoomState.Working] = STATE_VAL.None,
            [RoomState.CanCollect] = STATE_VAL.None,
            [RoomState.Stopped] = STATE_VAL.None,
            [RoomState.Idle] = STATE_VAL.None,
        },
        [GEnums.SpaceshipRoomType.FlexibleTypeA] = {
            [RoomState.Locked] = STATE_VAL.UnOpen,
            [RoomState.NotBuild] = STATE_VAL.PendingBuild,
        },
        [GEnums.SpaceshipRoomType.FlexibleTypeB] = {
            [RoomState.Locked] = STATE_VAL.UnOpen,
            [RoomState.NotBuild] = STATE_VAL.PendingBuild,
        },
    }
}

SSRoomInfoPanel.SetupSwitchStateHandleFunctions = HL.Override() << function(self)
    self.m_stateHandleFuncLut = {}
end

SSRoomInfoPanel.DefaultStateHandle = HL.Override(HL.String, CS.Beyond.Gameplay.SpaceshipSystem.RoomState) << function(self, roomId, roomState)
    self:SetupView()
    self.view.content:SetState(STATUS_BAR_CONST.ROOM_TYPE_VAL[self.m_roomType])
    self.view.statusNode:SetState(STATUS_BAR_CONST.ROOM_TYPE_STATE_VAL[self.m_roomType][roomState])
    self.view.statusBar:SetInteger(STATUS_BAR_CONST.ANIMATOR_STATE_NAME, STATUS_BAR_CONST.ANIMATOR_STATE_VAL[roomState])
    local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    if hasValue then
        self.view.titleText.text = SpaceshipUtils.getFormatCabinSerialNum(roomId, roomInfo.serialNum)
    end
end

SSRoomInfoPanel.SetupView = HL.Override() << function(self)

    local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(self.m_roomId)
    if not hasValue or not roomInfo then
        local emptyRoomData = Tables.spaceshipEmptyRoomTable[self.m_roomId]
        self.m_roomType = emptyRoomData.roomType
    else
        self.m_roomType = roomInfo.roomType
    end
end

HL.Commit(SSRoomInfoPanel)
return SSRoomInfoPanel

