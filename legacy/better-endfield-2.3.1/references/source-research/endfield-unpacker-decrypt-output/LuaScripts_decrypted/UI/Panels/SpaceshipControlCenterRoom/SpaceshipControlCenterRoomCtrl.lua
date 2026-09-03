
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipControlCenterRoom

SpaceshipControlCenterRoomCtrl = HL.Class('SpaceshipControlCenterRoomCtrl', uiCtrl.UICtrl)


SpaceshipControlCenterRoomCtrl.m_moveCam = HL.Field(HL.Boolean) << false






SpaceshipControlCenterRoomCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SPACESHIP_ON_ROOM_LEVEL_UP] = 'OnRoomLevelUp',
}


SpaceshipControlCenterRoomCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:RefreshAll()

    if arg and arg.moveCam then
        self.m_moveCam = true
    end
    if GameInstance.player.spaceship.isViewingFriend then
        self.view.main:SetState("Visitors")
    else
        self.view.main:SetState("Owner")
    end



end

SpaceshipControlCenterRoomCtrl.OnRoomLevelUp = HL.Method(HL.Any) << function(self, _)
    self:RefreshAll()
end

SpaceshipControlCenterRoomCtrl.RefreshAll = HL.Method() << function(self)
    self:_RefreshCCRoom()
    self:_RefreshOtherRooms()
    self:_RefreshUnlockArea()
end

SpaceshipControlCenterRoomCtrl._RefreshCCRoom = HL.Method() << function(self)
    local roomId = Tables.spaceshipConst.controlCenterRoomId
    local _, roomData = GameInstance.player.spaceship:TryGetRoom(roomId)
    local roomTypeData = Tables.spaceshipRoomTypeTable[roomData.type]
    local node = self.view.controlCenterNode
    node.button.onClick:RemoveAllListeners()
    node.button.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.SpaceshipStation, { roomId = roomId })
    end)
    node.upgradeBtn.onClick:RemoveAllListeners()
    node.upgradeBtn.onClick:AddListener(function()
        GameInstance.player.spaceship:SetCurSpaceshipRoomCamConfig(roomId, CS.Beyond.Gameplay.SpaceshipSystem.UPGRADE_CAM_BLEND_KEY)
        PhaseManager:OpenPhase(PhaseId.SpaceshipRoomUpgrade, {
            roomId = roomId,
            moveCam = true,
            isRemoteCamera = true
        })
    end)

    node.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, roomTypeData.icon)
    node.iconColorBg.color = UIUtils.getColorByString(roomTypeData.color)
    node.titleTxt.text = roomTypeData.name
    node.lvDotNode:InitLvDotNode(roomData.lv, roomData.maxLv)
    node.lvTxt.text = roomData.lv

    if not GameInstance.player.spaceship.isViewingFriend then
        local notMaxLv = roomData.lv < roomData.maxLv
        self.view.lvHint.gameObject:SetActive(notMaxLv)
        node.upgradeBtn.gameObject:SetActive(notMaxLv)
    else
        node.upgradeBtn.gameObject:SetActive(false)
        self.view.lvHint.gameObject:SetActive(false)
    end
end

SpaceshipControlCenterRoomCtrl._RefreshOtherRooms = HL.Method() << function(self)
    for roomId, _ in pairs(Tables.spaceshipEmptyRoomTable) do
        if roomId ~= Tables.spaceshipConst.controlCenterRoomId then
            if self.view[roomId] then
                self:_UpdateRoomCell(self.view[roomId] , roomId)
            end
        end
    end
end

SpaceshipControlCenterRoomCtrl._RefreshUnlockArea = HL.Method() << function(self)
    local succ, roomInfo = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.controlCenterRoomId)
    if not succ then
        return
    end
    local ccNeedLv = Tables.SpaceshipAreaUnlockNeedCenterLvTable:GetValue("construction_area_2")
    local ccLv = roomInfo.lv
    self.view.lockNode.gameObject:SetActive(ccLv < ccNeedLv)
    self.view.unlockNameTxt.text = string.format(Language.LUA_SPACESHIP_UNLOCK_AREA_LEVEL, ccNeedLv)
end

SpaceshipControlCenterRoomCtrl._UpdateRoomCell = HL.Method(HL.Table, HL.String) << function(self, cell, roomId)
    local succ, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    local arealocked = GameInstance.player.spaceship:IsRoomAreaLocked(roomId)
    cell.button.onClick:RemoveAllListeners()
    cell.nameTxt.text = Language.LUA_SPACESHIP_UN_BUILD_ROOM
    if arealocked then
        cell.simpleStateController:SetState("Locked")
        cell.button.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_ROOM_LOCKED)
        end)
        cell.iconColorBg.color = self.view.config.LOCKED_COLOR
    elseif succ then
        if roomInfo.type == GEnums.SpaceshipRoomType.GuestRoomClueExtension then
            cell.gameObject:SetActive(true)
        end
        cell.simpleStateController:SetState("Normal")
        cell.button.onClick:AddListener(function()
            if roomInfo.type == GEnums.SpaceshipRoomType.ControlCenter then
                PhaseManager:OpenPhase(PhaseId.SpaceshipStation, { roomId = roomId })
            else
                local phaseId = PhaseId[SpaceshipConst.ROOM_PHASE_ID_NAME_MAP_BY_TYPE[roomInfo.type]]
                PhaseManager:OpenPhase(phaseId,
                    {
                        roomId = roomId,
                        moveCam = false,
                        isRemoteCamera = true
                    })
            end
        end)
        local roomTypeData = Tables.spaceshipRoomTypeTable[roomInfo.type]
        cell.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, roomTypeData.icon)
        cell.nameTxt.text = SpaceshipUtils.getFormatCabinSerialNum(roomId, roomInfo.serialNum)
    else
        cell.simpleStateController:SetState("NotBuild")
        cell.button.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_ROOM_NOT_BUILD)
        end)
    end
end

SpaceshipControlCenterRoomCtrl.OnClose = HL.Override() << function(self)
    if self.m_moveCam then
        local clearScreenKey = GameInstance.player.spaceship:UndoMoveCamToSpaceshipRoom(Tables.spaceshipConst.controlCenterRoomId)
        if clearScreenKey and clearScreenKey ~= -1 then
            UIManager:RecoverScreen(clearScreenKey)
        end
    end
end

HL.Commit(SpaceshipControlCenterRoomCtrl)
