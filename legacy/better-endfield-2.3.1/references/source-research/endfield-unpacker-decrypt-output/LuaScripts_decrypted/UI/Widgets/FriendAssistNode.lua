local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local ignoreRedDotRoomType = {
    [GEnums.SpaceshipRoomType.GrowCabin] = true,
}

FriendAssistNode = HL.Class('FriendAssistNode', UIWidgetBase)


FriendAssistNode._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY, function()
        if GameInstance.player.spaceship.isViewingFriend then
            return
        end
        self:UpdateData()
    end)
end

FriendAssistNode.m_assistCells = HL.Field(HL.Forward('UIListCache'))

FriendAssistNode.m_roomId = HL.Field(HL.String) << ""


FriendAssistNode.InitFriendAssistNode = HL.Method(HL.String) << function(self, roomId)
    self:_FirstTimeInit()
    self.m_roomId = roomId
    local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(self.m_roomId)
    if not hasValue  then
        logger.error("飞船舱室room id不存在")
        return
    end
    self:UpdateData()
    self.view.assistBtn.onClick:AddListener(function()
        UIManager:AutoOpen(PanelId.SpaceShipFriendHelpList, {roomId = roomId})
    end)
end

FriendAssistNode.UpdateData = HL.Method() << function(self)
    local beHelpedCreditLeft, beAssistTime = GameInstance.player.spaceship:GetCabinAssistedTime(self.m_roomId)
    local _, data = GameInstance.player.spaceship:TryGetRoom(self.m_roomId)
    self.view.redDot.gameObject:SetActive(not ignoreRedDotRoomType[data.roomType] and beHelpedCreditLeft > 0)

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

        self:_SetTimeText()
        self:_StartCoroutine(function()
            while true do
                coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
                self:_SetTimeText()
            end
        end)
    else
        for index = 1, helpLimit do
            UpdateCell(self.m_assistCells:GetItem(index), index)
        end
    end
end

FriendAssistNode._SetTimeText = HL.Method() << function(self)
    local targetTime = Utils.getNextCommonServerRefreshTime()
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(targetTime - curTime)
end

HL.Commit(FriendAssistNode)
return FriendAssistNode

