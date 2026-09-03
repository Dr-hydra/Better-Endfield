
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipControlCenter
local PHASE_ID = PhaseId.SpaceshipControlCenter

local HelpedStateByRoomType = {
    [GEnums.SpaceshipRoomType.ControlCenter] = "MoodRecovery",
    [GEnums.SpaceshipRoomType.ManufacturingStation] = "FastMade",
    [GEnums.SpaceshipRoomType.GrowCabin] = "ExtraProducts",
    [GEnums.SpaceshipRoomType.CommandCenter] = "FastAction",
    [GEnums.SpaceshipRoomType.GuestRoomClueExtension] = "ExtraRewards",
}

SpaceshipControlCenterCtrl = HL.Class('SpaceshipControlCenterCtrl', uiCtrl.UICtrl)

SpaceshipControlCenterCtrl.m_roomCells = HL.Field(HL.Forward('UIListCache'))

SpaceshipControlCenterCtrl.m_roomIndexTab = HL.Field(HL.Table)

SpaceshipControlCenterCtrl.m_nowNaviRoomCell = HL.Field(HL.Table)

SpaceshipControlCenterCtrl.m_visitorHelpBindingId = HL.Field(HL.Number) << -1

SpaceshipControlCenterCtrl.m_isFriend = HL.Field(HL.Boolean) << false

SpaceshipControlCenterCtrl.m_isFirstShow = HL.Field(HL.Boolean) << true

SpaceshipControlCenterCtrl.m_roomId = HL.Field(HL.String) << ""

SpaceshipControlCenterCtrl.m_moveCam = HL.Field(HL.Boolean) << false

SpaceshipControlCenterCtrl.m_clearScreenKey = HL.Field(HL.Number) << -1





SpaceshipControlCenterCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SPACESHIP_ON_SYNC_ROOM_STATION] = 'OnSyncRoomStation',
    [MessageConst.SPACESHIP_ON_ROOM_LEVEL_UP] = '_RefreshRooms',
    [MessageConst.ON_SPACESHIP_HELP_ROOM] = 'OnSSHelpRoom',
    [MessageConst.ON_SPACESHIP_USE_HELP_CREDIT] = 'OnUseHelpCredit',
    [MessageConst.ON_SPACESHIP_GROW_CABIN_HARVEST] = "OnSpaceshipGrowCabinHelped",
    [MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY] = "OnRefreshFriendData",
    [MessageConst.ON_SPACESHIP_JOIN_FRIEND_INFO_EXCHANGE] = "OnRefreshFriendData",
    [MessageConst.ON_SPACESHIP_GUEST_ROOM_CLUE_REWARD_ITEM] = 'OnSpaceshipGuestRoomClueRewardItem',
    [MessageConst.ON_SPACESHIP_CLUE_INFO_SYNC] = 'OnRefreshFriendData',
    [MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_COLLECT] = "OnSpaceshipManufacturingStationCollectOneKeyHarvest",
    [MessageConst.ON_SPACESHIP_ONE_KEY_HARVEST_FINISH] = "OnSpaceshipOneKeyHarvestFinish",
}

SpaceshipControlCenterCtrl.OpenControlCenter = HL.StaticMethod() << function()
    PhaseManager:OpenPhase(PhaseId.SpaceshipControlCenter)
end


SpaceshipControlCenterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_roomId = arg and arg.roomId or Tables.spaceshipConst.controlCenterRoomId
    self.m_moveCam = arg and arg.moveCam == true or false
    self.m_clearScreenKey = arg and arg.clearScreenKey or -1

    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.SpaceshipControlCenter)
        self.view.ssBacklogNode:PlayOutAnimation()
    end)
    if not GameInstance.player.spaceship.isViewingFriend then
        self:BindInputPlayerAction("ss_open_control_center", function()
            PhaseManager:PopPhase(PhaseId.SpaceshipControlCenter)
        end)
    end

    self.view.reportBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.SpaceshipDailyReport)
    end)
    if GameInstance.player.spaceship.isViewingFriend then
        self.view.assistCtrlNode:SetState("Visitors")
        self.m_isFriend = true
        self.view.moneyCell.view.gainNode.gameObject:SetActive(false)
        self:_SetFriendState()
    else
        self.view.assistCtrlNode:SetState("Owner")
        self.m_isFriend = false
        self:_SetSelfState()
    end
    self.m_roomCells = UIUtils.genCellCache(self.view.roomNode.roomCell)
    self:_RefreshRooms()
    self.view.redDotArrow:InitRedDot("SSControlCenterRoot", self.m_roomIndexTab)
    if GameInstance.player.spaceship.isViewingFriend and self.m_visitorHelpBindingId == -1 then
        self.m_visitorHelpBindingId = self:BindInputPlayerAction("ss_help_friend_room", function()
            if self.m_nowNaviRoomCell then
                AudioManager.PostEvent("Au_UI_Button_Produce")
                GameInstance.player.spaceship:SpaceshipHelpRoom(self.m_nowNaviRoomCell.roomId)
            end
        end)
    end
    self:SetDefaultNaviTarget()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self:_TryRecoverFriendHelpPopup(arg and arg.friendHelpPopupState or nil)
end

SpaceshipControlCenterCtrl.OnClose = HL.Override() << function(self)
    local clearScreenKey
    if self.m_moveCam then
        clearScreenKey = GameInstance.player.spaceship:UndoMoveCamToSpaceshipRoom(self.m_roomId)
        if clearScreenKey and clearScreenKey ~= -1 then
            UIManager:RecoverScreen(clearScreenKey)
        end
    end
    if self.m_clearScreenKey ~= -1 then
        if self.m_clearScreenKey ~= clearScreenKey then
            UIManager:RecoverScreen(self.m_clearScreenKey)
        end
        self.m_clearScreenKey = -1
    end
end

SpaceshipControlCenterCtrl.OnShow = HL.Override() << function(self)
end

SpaceshipControlCenterCtrl._TryRecoverFriendHelpPopup = HL.Method(HL.Opt(HL.Any)) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.roomId) then
        return
    end
    local isOpen, popupCtrl = UIManager:IsOpen(PanelId.SpaceShipFriendHelpList)
    if isOpen and popupCtrl and popupCtrl:IsShow() and popupCtrl.m_roomId == popupState.roomId then
        return
    end
    UIManager:AutoOpen(PanelId.SpaceShipFriendHelpList, {
        roomId = popupState.roomId,
    })
end


SpaceshipControlCenterCtrl.SetDefaultNaviTarget = HL.Method() << function(self)
    if not GameInstance.player.spaceship.isViewingFriend then
        self:SetNaviTarget(self.view.control_center.ownerButton)
    else
        self:SetNaviTarget(self.view.control_center.visitorsNodeInputBindingGroupNaviDecorator)
    end
end


SpaceshipControlCenterCtrl.OnRoomCellNaviTargetChange = HL.Method(HL.Opt(HL.Table, HL.Boolean, HL.String))
    << function(self, cell, canHelp, roomId)
    self.m_nowNaviRoomCell = cell
    self.m_nowNaviRoomCell.roomId = roomId
    self:_RefreshBinding(canHelp, roomId)
end

SpaceshipControlCenterCtrl._RefreshBinding = HL.Method(HL.Boolean, HL.String) << function(self, canHelp, roomId)
    if not self.m_nowNaviRoomCell or self.m_nowNaviRoomCell.roomId ~= roomId then
        return
    end
    if self.m_visitorHelpBindingId ~= -1 then
        InputManagerInst:ToggleBinding(self.m_visitorHelpBindingId, canHelp)
        if roomId == Tables.spaceshipConst.guestRoomClueExtensionId then
            InputManagerInst:SetBindingText(self.m_visitorHelpBindingId, Language.ui_spaceship_controlcenter_visitornode_swapbtn_swaptext)
        else
            InputManagerInst:SetBindingText(self.m_visitorHelpBindingId, Language.key_hint_ss_help_friend_room)
        end
    end
end


SpaceshipControlCenterCtrl._SetSelfState = HL.Method() << function(self)
    if GameInstance.player.spaceship.isViewingFriend then
        return
    end
    self.view.friendAssistNode:InitFriendAssistNode(Tables.spaceshipConst.controlCenterRoomId)
end


SpaceshipControlCenterCtrl._SetFriendState = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    if not spaceship.isViewingFriend then
        return
    end
    local rewardItem = Tables.rewardTable[Tables.spaceshipConst.visitorHelpRewardId].itemBundles[CSIndex(1)]
    local itemId = rewardItem.id
    SpaceshipUtils.InitMoneyLimitCell(self.view.moneyCell, itemId)
    local friendInfo = spaceship:GetFriendRoleInfo()
    if friendInfo then
        self.view.playerNameNode:InitSocializeFriendName(friendInfo.roleId)
    end

    local clueData = spaceship:GetClueData(CS.Beyond.Gameplay.SpaceshipEnums.SpaceshipDataType.Self)
    self.view.assistHintNode.swapMessNode.gameObject:SetActive(clueData ~= nil)
    if clueData then
        local swapTime = Tables.spaceshipConst.joinInfoExchangeCountLimit - (clueData.joinFriendExchangeCnt or 0)
        self.view.assistHintNode.swapTimesTxt.text = string.format("%d/%d", swapTime, Tables.spaceshipConst.joinInfoExchangeCountLimit)
    end

    self.view.assistHintNode.detailBtn.onClick:RemoveAllListeners()
    self.view.assistHintNode.detailBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "control_center_help_rule")
    end)
    self:_SetTimeText()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_SetTimeText()
        end
    end)
    local isHelpDone = spaceship.helpOtherCount >= Tables.spaceshipConst.helpOthersCountLimit
    self.view.assistHintNode.dayLimitNode:SetState(isHelpDone and "tomorrow" or "today")
    self.view.assistHintNode.assistTimesTxt.text = string.format("%d/%d", spaceship.helpOtherRemainCount, Tables.spaceshipConst.helpOthersCountLimit)
end

SpaceshipControlCenterCtrl.OnSSHelpRoom = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    local roomId = arg and arg[1] or Tables.spaceshipConst.controlCenterRoomId
    local success = arg and arg[2] or false
    self:_RefreshGainNode(roomId, success)
    self:_SetFriendState()
end

SpaceshipControlCenterCtrl.OnUseHelpCredit = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    self:_SetSelfState()
end

SpaceshipControlCenterCtrl.OnRefreshFriendData = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    if not self.m_isFriend then
        return
    end
    self:_SetFriendState()
    self:_RefreshRooms()
end

SpaceshipControlCenterCtrl.OnSpaceshipGuestRoomClueRewardItem = HL.Method(HL.Any) << function(self, args)
    if GameInstance.player.spaceship:IsWaitingForHandleOneKeyHarvest() then
        self:OnSpaceshipGrowCabinOneKeyHarvest(args)
        return
    end
    local items, sources = unpack(args)
    SpaceshipUtils.ShowClueOutcomePopup(items, sources, self.view.moneyCell, nil)
end

SpaceshipControlCenterCtrl.OnSpaceshipOneKeyHarvestFinish = HL.Method(HL.Any) << function(self, args)
    local items = unpack(args)
    if not items or items.Count == 0 then
        return
    end
    self:_ShowOutcomePopupForOneKeyHarvest("", items)
end

SpaceshipControlCenterCtrl._ShowOutcomePopupForOneKeyHarvest = HL.Method(HL.String, HL.Any) << function(self, title, csItems)
    local items = {}
    for i = 0, csItems.Count - 1 do
        local item = csItems[i]
        local needShowHelp = Tables.spaceshipGrowCabinOutCome2MaterialTable:ContainsKey(item.id)
        table.insert(items, {
            id = item.id,
            count = (item.Count or 0) + (item.count or 0),
            needShowHelp = needShowHelp,
        })
    end
    if #items == 0 then
        return
    end
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = title,
        items = items,
    })
end

SpaceshipControlCenterCtrl._RefreshGainNode = HL.Method(HL.String, HL.Boolean) << function(self, roomId, success)
    if success then
        local rewardItem = Tables.rewardTable[Tables.spaceshipConst.visitorHelpRewardId].itemBundles[CSIndex(1)]
        local itemCount = rewardItem.count
        local itemId = rewardItem.id
        SpaceshipUtils.PlayMoneyNodeGainAnim(self.view.moneyCell, itemId, itemCount)
    end
    local rooms = GameInstance.player.spaceship:GetRoomsWithSort()
    local itemIndex = 1
    for i = CSIndex(1),  CSIndex(rooms.Count) do
        local info = rooms[i]
        local roomId = rooms[i].id
        if info.type == GEnums.SpaceshipRoomType.GuestRoom then
            goto continue
        end
        if info.type ~= GEnums.SpaceshipRoomType.ControlCenter then
            self:_RefreshRoomGainNode(self.m_roomCells:GetItem(itemIndex) , roomId, true)
            itemIndex = itemIndex + 1
        else
            self:_RefreshRoomGainNode(self.view[roomId] , roomId, true)
        end
        ::continue::
    end
end

SpaceshipControlCenterCtrl._RefreshRoomGainNode = HL.Method(HL.Table, HL.String, HL.Opt(HL.Boolean)) << function(self, cell, roomId, refreshNode)
    cell.visitorsNode.gainNode.gameObject:SetActive(true)

    local itemCount = 0
    if roomId == Tables.spaceshipConst.guestRoomClueExtensionId then
        local clueData = GameInstance.player.spaceship:GetClueData(CS.Beyond.Gameplay.SpaceshipEnums.SpaceshipDataType.Friend)
        if clueData ~= nil then
            local success, data = Tables.spaceshipGuestRoomClueLvTable:TryGetValue(clueData.clueRoomLevel)
            if success then
                itemCount = data.creditFriendJoinExchangeGain
            end
        end
    else
        local rewardItem = Tables.rewardTable[Tables.spaceshipConst.visitorHelpRewardId].itemBundles[CSIndex(1)]
        itemCount = rewardItem.count
    end
    cell.visitorsNode.creditTxt.text = string.format("+%d", itemCount)
    if refreshNode then
        self:_RefreshVisitorNode(cell, roomId)
    end
end

SpaceshipControlCenterCtrl._SetTimeText = HL.Method() << function(self)
    local targetTime = Utils.getNextCommonServerRefreshTime()
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftSec = targetTime - curTime
    self.view.assistHintNode.refreshTimesTxt.text = UIUtils.getLeftTimeToSecond(leftSec)
end

SpaceshipControlCenterCtrl.OnSyncRoomStation = HL.Method() << function(self)
    self:_RefreshRooms()
end

SpaceshipControlCenterCtrl._RefreshRooms = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    local roomCount = GameInstance.player.spaceship.rooms.Count - 1
    local hasValue, _ = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.guestRoomId)
    if hasValue then
        roomCount = roomCount - 1
    end
    self.m_roomCells:Refresh(roomCount)
    local itemIndex = 1
    self.m_roomIndexTab = {}

    local rooms = GameInstance.player.spaceship:GetRoomsWithSort()
    for i = CSIndex(1),  CSIndex(rooms.Count) do
        local info = rooms[i]
        local roomId = rooms[i].id
        if info.type == GEnums.SpaceshipRoomType.GuestRoom then
            goto continue
        end
        if info.type ~= GEnums.SpaceshipRoomType.ControlCenter then
            self:_UpdateRoomCell(self.m_roomCells:GetItem(itemIndex) , roomId)
            self.m_roomIndexTab[roomId] = itemIndex
            itemIndex = itemIndex + 1
        else
            self:_UpdateRoomCell(self.view[roomId] , roomId)
        end
        ::continue::
    end
    if self.m_isFriend then
        return
    end
    local succ, roomInfo = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.controlCenterRoomId)
    if not succ then
        return
    end
    self.view.roomEffectInfoNode:InitSSRoomEffectInfoNode({
        attrsMap = roomInfo.attrsMap,
        color = SpaceshipUtils.getRoomColor(roomInfo.id),
    })
end


SpaceshipControlCenterCtrl._UpdateRoomCell = HL.Method(HL.Table, HL.String) << function(self, cell, roomId)
    local succ, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    if not succ or not cell then
        return
    end
    local roomTypeData = Tables.spaceshipRoomTypeTable[roomInfo.type]
    cell.ownerButton.enabled = true
    cell.ownerButton.onClick:RemoveAllListeners()
    cell.ownerButton.onClick:AddListener(function()
        if roomInfo.type == GEnums.SpaceshipRoomType.ControlCenter then
            PhaseManager:OpenPhase(PhaseId.SpaceshipStation, { roomId = roomId })
        else
            local phaseId = PhaseId[SpaceshipConst.ROOM_PHASE_ID_NAME_MAP_BY_TYPE[roomInfo.type]]
            PhaseManager:OpenPhase(phaseId, {
                roomId = roomId,
                moveCam = true,
                isRemoteCamera = true
            })
        end
    end)

    local node = cell.contentNode
    node.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, roomTypeData.icon)
    node.iconColorBG.color = UIUtils.getColorByString(roomTypeData.color)
    node.nameTxt.text = SpaceshipUtils.getFormatCabinSerialNum(roomId, roomInfo.serialNum)
    node.lvDotNode:InitLvDotNode(roomInfo.lv, roomInfo.maxLv, node.iconColorBG.color)
    self:_UpdateRoomCellStation(cell, roomInfo)
    cell.simpleStateController:SetState("Normal")
    cell.visitorsNode.helpBtn.onClick:RemoveAllListeners()
    cell.visitorsNode.helpBtn.onClick:AddListener(function()
        GameInstance.player.spaceship:SpaceshipHelpRoom(roomId)
    end)
    cell.visitorsNode.swapBtn.onClick:RemoveAllListeners()
    cell.visitorsNode.swapBtn.onClick:AddListener(function()
        GameInstance.player.spaceship:SpaceshipHelpRoom(roomId)
    end)

    if GameInstance.player.spaceship.isViewingFriend then
        cell.redDot.gameObject:SetActive(false)
    else
        if roomInfo.type == GEnums.SpaceshipRoomType.GrowCabin then
            cell.redDot:InitRedDot("SSGrowCabin", roomId)
        elseif roomInfo.type == GEnums.SpaceshipRoomType.ManufacturingStation then
            cell.redDot:InitRedDot("SSManufacturingStation", roomId)
        elseif roomInfo.type == GEnums.SpaceshipRoomType.GuestRoomClueExtension then
            cell.redDot:InitRedDot("SSGuestRoomClue")
        else
            cell.redDot.gameObject:SetActive(false)
        end
    end
    self:_RefreshVisitorNode(cell, roomId)
end

SpaceshipControlCenterCtrl._RefreshVisitorNode = HL.Method(HL.Table, HL.String) << function(self, cell, roomId)
    local spaceship = GameInstance.player.spaceship
    if not spaceship.isViewingFriend then
        return
    end
    local succ, roomInfo = spaceship:TryGetRoom(roomId)
    if not succ then
        return
    end

    local friendInfo = spaceship:GetFriendRoleInfo()
    local isHelped = spaceship:CheckIsHelpOther(friendInfo.roleId, roomId)
    local unableHelp = spaceship:CheckUnableHelpByRoomId(roomId)
    local SpaceshipDataType = CS.Beyond.Gameplay.SpaceshipEnums.SpaceshipDataType
    local clueData = spaceship:GetClueData(SpaceshipDataType.Self)
    if isHelped then
        cell.visitorsNode.visitorsNode:SetState(HelpedStateByRoomType[roomInfo.type])
        self:_RefreshRoomGainNode(cell, roomId)
    elseif unableHelp and roomId == Tables.spaceshipConst.guestRoomClueExtensionId then
        if clueData then
            if spaceship:GetJoinFriendCount(SpaceshipDataType.Self) >= Tables.spaceshipConst.joinInfoExchangeCountLimit then
                cell.visitorsNode.visitorsNode:SetState("GuestroomClueCantHelp")
            else
                cell.visitorsNode.visitorsNode:SetState("DisableOpen")
            end
        else
            cell.visitorsNode.visitorsNode:SetState("GuestroomClueLocked")
        end
    elseif unableHelp then
        cell.visitorsNode.visitorsNode:SetState("DisableAssist")
    elseif roomId == Tables.spaceshipConst.guestRoomClueExtensionId then
        cell.visitorsNode.visitorsNode:SetState("MessSwap")
    else
        cell.visitorsNode.visitorsNode:SetState("AssistHelp")
    end
    local canHelp = not isHelped and not unableHelp
    if self.m_visitorHelpBindingId ~= -1 then
        self:_RefreshBinding(canHelp, roomId)
    end
    cell.visitorsNodeInputBindingGroupNaviDecorator.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            canHelp = not spaceship:CheckIsHelpOther(friendInfo.roleId, roomId) and not unableHelp
            self:OnRoomCellNaviTargetChange(cell, canHelp, roomId)
        end
    end
end

SpaceshipControlCenterCtrl.OnSpaceshipGrowCabinHelped = HL.Method(HL.Any) << function(self, args)
    if not GameInstance.player.spaceship.isViewingFriend then
        self:OnSpaceshipGrowCabinOneKeyHarvest(args)
        return
    end
    local title = Language.LUA_SPACESHIP_ROOM_GROW_CABIN_HELP_OUTCOME_POPUP_TITLE
    local items = unpack(args)
    self:_ShowOutcomePopup(title, items)
end


SpaceshipControlCenterCtrl._ShowOutcomePopup = HL.Method(HL.String, HL.Any) << function(self, title, csItems)
    local itemMap = {}
    local isWaitingHandle = GameInstance.player.spaceship:IsWaitingForHandleOneKeyHarvest()
    for i = 0, csItems.Count - 1 do
        local item = csItems[i]

        if item.id == Tables.spaceshipConst.creditItemId and not isWaitingHandle then
            goto continue
        end

        if itemMap[item.id or item.Id] then
            local accCount = itemMap[item.id or item.Id]
            itemMap[item.id or item.Id] = accCount + (item.Count or 0) + (item.count or 0)
        else
            itemMap[item.id or item.Id] = (item.Count or 0) + (item.count or 0)
        end
        ::continue::
    end

    local items = {}

    for id, count in pairs(itemMap) do
        local needShowHelp = Tables.spaceshipGrowCabinOutCome2MaterialTable:ContainsKey(id)
        table.insert(items, {
            id = id,
            count = count,
            needShowHelp = needShowHelp,
        })
    end
    if #items == 0 then
        return
    end
    if isWaitingHandle then
        for i, v in ipairs(items) do
            GameInstance.player.spaceship:AddHarvestItem(v.id, v.count)
        end
        return
    end
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = title,
        items = items,
    })
end



SpaceshipControlCenterCtrl._UpdateRoomCellStation = HL.Method(HL.Table, CS.Beyond.Gameplay.SpaceshipSystem.Room) << function(self, cell, roomInfo)
    local node = cell.contentNode
    if not node.m_charCells then
        node.m_charCells = UIUtils.genCellCache(node.charCell)
    end

    local maxCount = roomInfo.maxLvStationCount
    local curMaxCount = roomInfo.maxStationCharNum
    local curCount = roomInfo.stationedCharList.Count
    node.m_charCells:Refresh(maxCount, function(charCell, index)
        if index <= curCount then
            charCell.view.simpleStateController:SetState("Normal")
            charCell:InitSSCharHeadCell({
                charId = roomInfo.stationedCharList[CSIndex(index)],
                targetRoomId = roomInfo.id,
            })
        elseif index <= curMaxCount then
            charCell.view.simpleStateController:SetState("Empty")
        else
            charCell.view.simpleStateController:SetState("Locked")
        end
    end)

    node.countTxt.text = string.format("%d/%d", curCount, curMaxCount)
end


SpaceshipControlCenterCtrl.OnSpaceshipManufacturingStationCollectOneKeyHarvest = HL.Method(HL.Any) << function(self, args)
    local _, itemId, count = unpack(args)
    if GameInstance.player.spaceship:IsWaitingForHandleOneKeyHarvest() then
        GameInstance.player.spaceship:AddHarvestItem(itemId, count)
    end
end

SpaceshipControlCenterCtrl.OnSpaceshipGrowCabinOneKeyHarvest = HL.Method(HL.Any) << function(self, args)
    if not GameInstance.player.spaceship:IsWaitingForHandleOneKeyHarvest() then
        return
    end
    local title = Language.LUA_SPACESHIP_ROOM_GROW_CABIN_SOW_OUTCOME_POPUP_TITLE
    local items = unpack(args)
    self:_ShowOutcomePopup(title, items)
end

HL.Commit(SpaceshipControlCenterCtrl)
