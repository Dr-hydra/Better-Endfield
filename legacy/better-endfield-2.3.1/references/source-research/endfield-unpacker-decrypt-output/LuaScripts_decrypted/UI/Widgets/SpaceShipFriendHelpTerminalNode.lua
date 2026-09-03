local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local stateByRoomType = {
    [GEnums.SpaceshipRoomType.ControlCenter] = "ControlCenter",
    [GEnums.SpaceshipRoomType.ManufacturingStation] = "ManufacturingStation",
    [GEnums.SpaceshipRoomType.GrowCabin] = "GrowCabin",
}

SpaceShipFriendHelpTerminalNode = HL.Class('SpaceShipFriendHelpTerminalNode', UIWidgetBase)

SpaceShipFriendHelpTerminalNode.m_assistCells = HL.Field(HL.Forward('UIListCache'))

SpaceShipFriendHelpTerminalNode.m_panel = HL.Field(HL.Userdata)

SpaceShipFriendHelpTerminalNode.m_roomId = HL.Field(HL.String) << ""



SpaceShipFriendHelpTerminalNode._OnFirstTimeInit = HL.Override() << function(self)
    self:_SetTimeText()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_SetTimeText()
        end
    end)

    self.view.detailBtn.onClick:AddListener(function()
        self.view.tipsNode.gameObject:SetActive(true)
        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = self:GetPanelId(),
            isGroup = true,
            id = self.view.tipsNodeInputBindingGroupMonoTarget.groupId,
            rectTransform = self.view.tipsNode.gameObject.transform,
            noHighlight = true,
        })
    end)

    self.view.tipsBg.onClick:AddListener(function()
        self.view.tipsNode.gameObject:SetActive(false)
        Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.tipsNodeInputBindingGroupMonoTarget.groupId)
    end)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY, function()
        if GameInstance.player.spaceship.isViewingFriend then
            return
        end
        self:UpdateState()
    end)
end

SpaceShipFriendHelpTerminalNode.UpdateState = HL.Method() << function(self)
    local beHelpedCreditLeft, beAssistTime = GameInstance.player.spaceship:GetCabinAssistedTime(self.m_roomId)
    local helpLimit = SpaceshipUtils.getRoomHelpLimit(self.m_roomId)
    local UpdateCell = function(cell, index)
        if index <= beHelpedCreditLeft then
            cell.stateController:SetState("CanUseHelp")
        elseif index <= beAssistTime then
            cell.stateController:SetState("UsedHelp")
        else
            cell.stateController:SetState("None")
        end
    end

    if not self.m_assistCells then
        self.m_assistCells = UIUtils.genCellCache(self.view.stateNode)
        self.m_assistCells:Refresh(helpLimit, function(cell, index)
            UpdateCell(cell, index)
        end)
    else
        for index = 1, helpLimit do
            UpdateCell(self.m_assistCells:GetItem(index), index)
        end
    end
end


SpaceShipFriendHelpTerminalNode.InitSpaceShipFriendHelpTerminalNode = HL.Method(HL.String, HL.Any) << function(self, roomId, panel)
    self.m_panel = panel
    self.m_roomId = roomId
    self:_FirstTimeInit()
    local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    if not hasValue then
        return
    end
    local roomTypeData = Tables.spaceshipRoomTypeTable[roomInfo.type]
    self.view.stateController:SetState(stateByRoomType[roomInfo.type])
    self.view.descTxt.text = roomTypeData.helpDesc
    self.view.platTxt.text = roomTypeData.name
    self:UpdateState()
end

SpaceShipFriendHelpTerminalNode._SetTimeText = HL.Method() << function(self)
    local targetTime = Utils.getNextCommonServerRefreshTime()
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(targetTime - curTime)
end


HL.Commit(SpaceShipFriendHelpTerminalNode)
return SpaceShipFriendHelpTerminalNode

