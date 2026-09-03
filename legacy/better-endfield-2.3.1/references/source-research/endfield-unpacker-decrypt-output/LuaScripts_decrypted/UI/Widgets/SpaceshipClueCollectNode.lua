local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SpaceshipClueCollectNode = HL.Class('SpaceshipClueCollectNode', UIWidgetBase)

SpaceshipClueCollectNode.m_spaceship = HL.Field(HL.Userdata)

SpaceshipClueCollectNode.m_isOpen = HL.Field(HL.Boolean) << false



SpaceshipClueCollectNode._OnFirstTimeInit = HL.Override() << function(self)
    SpaceshipUtils.InitMoneyLimitCell(self.view.moneyCell, Tables.spaceshipConst.creditItemId)
    self.view.operator.stateController:SetState("Operator")
    self.view.operator.rewardText.text = Tables.spaceshipConst.generateClueCreditRewardCount
    self.m_spaceship = GameInstance.player.spaceship

    local nowProgress = self.m_spaceship:GetNextStationClueNowProgress()
    self.view.operator.slider.value = nowProgress / Tables.spaceshipConst.clueCollectPointMaxValue
    self.view.operator.txtImg.text = UIUtils.getLeftTimeToSecond(self.m_spaceship:GetNextStationClueLeftTime())
    self.view.btnClose.onClick:AddListener(function()
        Notify(MessageConst.ON_POP_SPACESHIP_GUEST_ROOM_MAIN_PANEL)
    end)
    self.view.hotArea.onClick:AddListener(function()
        Notify(MessageConst.ON_POP_SPACESHIP_GUEST_ROOM_MAIN_PANEL)
    end)
    self:_TickProgress(nowProgress)
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            nowProgress = self.m_spaceship:GetNextStationClueNowProgress()
            self:_TickProgress(nowProgress)
        end
    end)

    self.view.guestroom.stateController:SetState("Guestroom")
    self.view.guestroom.rewardText.text = Tables.spaceshipConst.generateClueCreditRewardCount
    self.view.guestroom.btnReceive.onClick:AddListener(function()
        self.m_spaceship:GuestRoomRecvClue()
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_RECV_CLUE, function(args)
        local instId, isBase = unpack(args)
        if isBase then
            self:RefreshState()
        end
    end)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_CLUE_FULL, function(args)
        self:RefreshState()
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE, function()
        self:RefreshState()
    end)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_GUEST_ROOM_CLUE_REWARD_ITEM, function(args)
        local items, sources = unpack(args)
        SpaceshipUtils.ShowClueOutcomePopup(items, sources, self.view.moneyCell, nil)
    end)
end

SpaceshipClueCollectNode.InitSpaceshipClueCollectNode = HL.Method() << function(self)
    self:_FirstTimeInit()
    self:FadeIn()
    self:RefreshState()
end

SpaceshipClueCollectNode.RefreshState = HL.Method() << function(self)
    local clueData = self.m_spaceship:GetClueData()
    if not clueData then
        return
    end

    local operatorText
    if self.m_spaceship:IsClueSelfLimitBeenReached() and self.m_spaceship:GetNextStationClueNowProgress() >= Tables.spaceshipConst.clueCollectPointMaxValue then
        operatorText = Language.LUA_SPACESHIP_CLUE_STORAGE_LIMIT
    elseif self.m_spaceship:IsGuestRoomClueShutDown() then
        operatorText = Language.LUA_SPACESHIP_CLUE_ROLE_IS_NOT_WORKING
    elseif self.m_spaceship:IsGuestRoomClueIdle() then
        operatorText = Language.LUA_SPACESHIP_CLUE_NEED_ROLE_WORKING
    else
        operatorText = Language.LUA_SPACESHIP_CLUE_IS_COLLECTING
    end
    self.view.operator.txtContent.text = operatorText


    local guestRoomText
    if self.m_spaceship:IsClueSelfLimitBeenReached() then
        guestRoomText = Language.LUA_SPACESHIP_CLUE_STORAGE_LIMIT
    elseif clueData.dailyClueIndex ~= 0 then
        guestRoomText = Language.LUA_SPACESHIP_CLUE_CAN_COLLECT
    elseif self.m_spaceship:IsGuestRoomClueIdle() then
        guestRoomText = Language.LUA_SPACESHIP_CLUE_NEED_ROLE_WORKING
    else
        guestRoomText = Language.LUA_SPACESHIP_CLUE_FIXED_TIME_OUTPUT
    end
    self.view.guestroom.txtContent.text = guestRoomText

    local canNotReceive = clueData.dailyClueIndex == 0 or self.m_spaceship:IsClueSelfLimitBeenReached()
    self.view.guestroom.btnReceive.gameObject:SetActive(not canNotReceive)
    self.view.guestroom.disableState.gameObject:SetActive(canNotReceive)
    if clueData.dailyClueIndex ~= 0 then
        self.view.guestroom.clueCellStateController:SetState(clueData.dailyClueIndex)
        self.view.guestroom.clueCellStateController:SetState("NewClue")
    else
        self.view.guestroom.clueCellStateController:SetState("NoClue")
    end
    self.view.guestroom.txtImg.text = ""
    local format = "%s/%s"
    local clueCount = self.m_spaceship:GetClueSelfClueCount()
    if clueCount == Tables.spaceshipConst.selfClueStorageMaxCount then
        format = "<color=#e54545>%s</color>/%s"
    end
    self.view.txtCapacity.text = string.format(format, clueCount, Tables.spaceshipConst.selfClueStorageMaxCount)
end


SpaceshipClueCollectNode._TickProgress = HL.Method(HL.Number) << function(self, nowProgress)
    self.view.operator.slider.value = nowProgress / Tables.spaceshipConst.clueCollectPointMaxValue
    local time = self.m_spaceship:GetNextStationClueLeftTime()
    if time == -1 then
        time = Language.LUA_SPACESHIP_CLUE_EMPTY_TIME
        self.view.operator.txtImg.text = time
    else
        self.view.operator.txtImg.text = UIUtils.getLeftTimeToSecond(time)
    end
end

SpaceshipClueCollectNode.FadeIn = HL.Method() << function(self)
    if self.m_isOpen then
        return
    end
    self.m_isOpen = true
    self.view.animationWrapper:ClearTween()
    self.view.animationWrapper:PlayInAnimation()
end


SpaceshipClueCollectNode.FadeOut = HL.Method() << function(self)
    if not self.m_isOpen then
        return
    end
    self.m_isOpen = false
    self.view.animationWrapper:PlayOutAnimation(function()
        if self.m_isOpen then
            return
        end
        self.view.gameObject:SetActive(false)
    end)
end

HL.Commit(SpaceshipClueCollectNode)
return SpaceshipClueCollectNode

