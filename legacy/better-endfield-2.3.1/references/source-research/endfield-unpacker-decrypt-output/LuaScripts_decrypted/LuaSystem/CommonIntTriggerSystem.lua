local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
CommonIntTriggerSystem = HL.Class('CommonIntTriggerSystem', LuaSystemBase.LuaSystemBase)



CommonIntTriggerSystem.OnInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.COMPONENT_CALL_LUA_UI_ON, function(args)
        self:CallLuaUI(args, true)
    end)
    self:RegisterMessage(MessageConst.COMPONENT_CALL_LUA_UI_OFF, function(args)
        self:CallLuaUI(args, false)
    end)
end

CommonIntTriggerSystem.OnRelease = HL.Override() << function(self)
    if self.m_spaceshipRoomMsgGroups ~= nil then
        for _, groupKey in pairs(self.m_spaceshipRoomMsgGroups) do
            MessageManager:UnregisterAll(groupKey)
        end
        self.m_spaceshipRoomMsgGroups = {}
    end
end

CommonIntTriggerSystem.CallLuaUI = HL.Method(HL.Table, HL.Boolean) << function(self, args, isOn)
    local argList, camConfigsCSCS = unpack(args)
    local name = argList[0]
    local funcName = name .. (isOn and "_ON" or "_OFF")
    local func = self[funcName]
    if not func then
        logger.error("No Func", funcName, name, args)
        return
    end

    local count = argList.Count - 1

    if count == 0 then
        func(self, camConfigsCSCS)
    elseif count == 1 then
        func(self, argList[1], camConfigsCSCS)
    elseif count == 2 then
        func(self, argList[1], argList[2], camConfigsCSCS)
    elseif count == 3 then
        func(self, argList[1], argList[2], argList[3], camConfigsCSCS)
    elseif count == 4 then
        func(self, argList[1], argList[2], argList[3], argList[4], camConfigsCSCS)
    elseif count == 5 then
        func(self, argList[1], argList[2], argList[3], argList[4], argList[5], camConfigsCSCS)
    elseif count == 6 then
        func(self, argList[1], argList[2], argList[3], argList[4], argList[5], argList[6], camConfigsCSCS)
    end
    AudioManager.PostEvent("au_int_template_slience")
end






CommonIntTriggerSystem.m_curSpaceshipRoomCamConfigs = HL.Field(HL.Table)

CommonIntTriggerSystem.m_curSpaceshipRoomCamStack = HL.Field(HL.Table)

CommonIntTriggerSystem.m_spaceshipRoomMsgGroups = HL.Field(HL.Table)


CommonIntTriggerSystem._RefreshSpaceshipRoomOptions = HL.Method(HL.String) << function(self, roomId)
    local unlocked, room = GameInstance.player.spaceship:TryGetRoom(roomId)
    if not unlocked or GameInstance.player.spaceship.isViewingFriend then
        return
    end
    local roomType = room.type
    local roomTypeData = Tables.spaceshipRoomTypeTable[roomType]

    local roomText = roomTypeData.viewOptName
    local roomIcon = roomTypeData.icon
    if GameInstance.player.spaceship:HaveRoomProductToCollect(roomType, roomId) then
        roomText = "<color=#fede00>" .. roomText .. "</color>"
        roomIcon = roomIcon .. "_yellow"
    end
    Notify(MessageConst.UPDATE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Spaceship,
        sourceId = roomId,
        text = roomText,
        icon = roomIcon,
        subIndex = 1,
    })

    local isMaxLv = room.lv >= room.maxLv
    local upgradeText = isMaxLv and roomTypeData.maxLvOptName or roomTypeData.upgradeOptName
    local upgradeIcon = isMaxLv and "btn_common_exchange_icon" or "ss_room_upgrade_int_icon"
    if GameInstance.player.spaceship:IsRoomCanLevelUp(roomId) then
        upgradeText = "<color=#fede00>" .. upgradeText .. "</color>"
        upgradeIcon = upgradeIcon .. "_yellow"
    end
    Notify(MessageConst.UPDATE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Spaceship,
        sourceId = roomId,
        text = upgradeText,
        icon = upgradeIcon,
        subIndex = 2,
    })
end

CommonIntTriggerSystem._RegisterSpaceshipRoomRefresh = HL.Method(HL.String) << function(self, roomId)
    self:_UnregisterSpaceshipRoomRefresh(roomId)
    local groupKey = {}
    self.m_spaceshipRoomMsgGroups = self.m_spaceshipRoomMsgGroups or {}
    self.m_spaceshipRoomMsgGroups[roomId] = groupKey

    local refreshMsgs = {
        MessageConst.SPACESHIP_ON_ROOM_LEVEL_UP,
        MessageConst.ON_ITEM_BAG_CHANGED,
        MessageConst.ON_WALLET_CHANGED,
        MessageConst.ON_VALUABLE_DEPOT_CHANGED,
        MessageConst.ON_FACTORY_DEPOT_CHANGED,
    }
    for _, msg in ipairs(refreshMsgs) do
        MessageManager:Register(msg, function()
            self:_RefreshSpaceshipRoomOptions(roomId)
        end, groupKey)
    end
end

CommonIntTriggerSystem._UnregisterSpaceshipRoomRefresh = HL.Method(HL.String) << function(self, roomId)
    if not self.m_spaceshipRoomMsgGroups then
        return
    end
    local groupKey = self.m_spaceshipRoomMsgGroups[roomId]
    if groupKey then
        MessageManager:UnregisterAll(groupKey)
        self.m_spaceshipRoomMsgGroups[roomId] = nil
    end
end

CommonIntTriggerSystem.SpaceshipRoom_ON = HL.Method(HL.String, HL.Opt(HL.Any)) << function(self, roomId, camConfigsCS)
    if not UIManager:IsOpen(PanelId.InteractOption) then
        return
    end
    local unlocked, room = GameInstance.player.spaceship:TryGetRoom(roomId)
    if not unlocked or GameInstance.player.spaceship.isViewingFriend then
        return
    end

    local roomType = room.type
    local sourceId = roomId
    local phaseId = PhaseId[SpaceshipConst.ROOM_PHASE_ID_NAME_MAP_BY_TYPE[roomType]]
    local roomTypeData = Tables.spaceshipRoomTypeTable[roomType]
    local roomName = roomTypeData.name

    local roomText = roomTypeData.viewOptName
    local roomIcon = roomTypeData.icon
    if GameInstance.player.spaceship:HaveRoomProductToCollect(roomType, roomId) then
        roomText = "<color=#fede00>" .. roomText .. "</color>"
        roomIcon = roomIcon .. "_yellow"
    end
    local openInteractOptArgs = {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Spaceship,
        sourceId = sourceId,
        text = roomText,
        action = function()
            local phaseArgs = {
                roomId = roomId,
                moveCam = true,
            }
            if not PhaseManager:CheckCanOpenPhaseAndToast(phaseId, phaseArgs) or PhaseManager:CheckIsInTransition() then
                return
            end

            local startPhaseId = PhaseManager:GetTopPhaseId()
            GameInstance.player.spaceship:SetCurSpaceshipRoomCamConfig(roomId, CS.Beyond.Gameplay.SpaceshipSystem.DEFAULT_CAM_BLEND_KEY)
            GameInstance.player.spaceship:MoveCamToSpaceshipRoom(roomId)

            local clearScreenKey = UIManager:ClearScreen()
            phaseArgs.clearScreenKey = clearScreenKey
            TimerManager:StartTimer(0.5, function()
                local canOpenPhase = PhaseManager:GetTopPhaseId() == startPhaseId and not PhaseManager:CheckIsInTransition()
                    and PhaseManager:CheckCanOpenPhase(phaseId, phaseArgs, true)
                if not canOpenPhase then
                    GameInstance.player.spaceship:UndoMoveCamToSpaceshipRoom(roomId)
                    if clearScreenKey and clearScreenKey ~= -1 then
                        UIManager:RecoverScreen(clearScreenKey)
                    end
                    return
                end
                PhaseManager:OpenPhase(phaseId, phaseArgs)
            end)
        end,
        icon = roomIcon,
        subIndex = 1,
        sortId = 1,
        roomName = roomName,
    }
    Notify(MessageConst.ADD_INTERACT_OPTION, openInteractOptArgs)

    local isMaxLv = room.lv >= room.maxLv
    local upgradeText = isMaxLv and roomTypeData.maxLvOptName or roomTypeData.upgradeOptName
    local upgradeIcon = isMaxLv and "btn_common_exchange_icon" or "ss_room_upgrade_int_icon"
    if GameInstance.player.spaceship:IsRoomCanLevelUp(roomId) then
        upgradeText = "<color=#fede00>" .. upgradeText .. "</color>"
        upgradeIcon = upgradeIcon .. "_yellow"
    end
    local upgradeInteractOptArgs = {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Spaceship,
        sourceId = sourceId,
        text = upgradeText,
        action = function()
            GameInstance.player.spaceship:SetCurSpaceshipRoomCamConfig(roomId, CS.Beyond.Gameplay.SpaceshipSystem.UPGRADE_CAM_BLEND_KEY)
            PhaseManager:OpenPhase(PhaseId.SpaceshipRoomUpgrade, {
                roomId = roomId,
                moveCam = true,
            })
        end,
        icon = upgradeIcon,
        subIndex = 2,
        sortId = 1,
    }
    Notify(MessageConst.ADD_INTERACT_OPTION, upgradeInteractOptArgs)

    self:_RegisterSpaceshipRoomRefresh(roomId)
end

CommonIntTriggerSystem.SpaceshipRoom_OFF = HL.Method(HL.String, HL.Opt(HL.Any)) << function(self, roomId, camConfigsCS)
    self:_UnregisterSpaceshipRoomRefresh(roomId)

    local unlocked, room = GameInstance.player.spaceship:TryGetRoom(roomId)
    if not unlocked or GameInstance.player.spaceship.isViewingFriend then
        return
    end
    local sourceId = roomId
    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Spaceship,
        sourceId = sourceId,
        subIndex = 1,
    })
    Notify(MessageConst.REMOVE_INTERACT_OPTION, {
        type = CS.Beyond.Gameplay.Core.InteractOptionType.Spaceship,
        sourceId = sourceId,
        subIndex = 2,
    })
end






HL.Commit(CommonIntTriggerSystem)
return CommonIntTriggerSystem
