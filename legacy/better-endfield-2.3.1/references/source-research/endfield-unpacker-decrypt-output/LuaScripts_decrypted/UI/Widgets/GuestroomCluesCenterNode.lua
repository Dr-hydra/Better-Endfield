local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GuestroomCluesCenterNode = HL.Class('GuestroomCluesCenterNode', UIWidgetBase)

GuestroomCluesCenterNode.m_spaceship = HL.Field(HL.Userdata)

GuestroomCluesCenterNode.m_onIsFocusedChange = HL.Field(HL.Function)

GuestroomCluesCenterNode.m_showClueDetailBindingId = HL.Field(HL.Number) << -1

GuestroomCluesCenterNode.m_nowNaviClueCell = HL.Field(HL.Table)

GuestroomCluesCenterNode.m_selectNode = HL.Field(HL.Table)

GuestroomCluesCenterNode.m_timeCor = HL.Field(HL.Thread)


GuestroomCluesCenterNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.btnExchangeData.onClick:AddListener(function()
        Notify(MessageConst.SHOW_SPACESHIP_CLUE_SCHEDULE)
    end)

    self.view.btnExchange.onClick:AddListener(function()
        local clueData = self.m_spaceship:GetClueData()
        if not clueData or clueData.infoExchangeInstId ~= 0 then
            logger.error("已有尚未结束的线索交流待结算")
            return
        end
        self.m_spaceship:OpenInfoExchangeClue()
    end)
    self.view.selectableNaviGroup.onIsFocusedChange:RemoveAllListeners()
    self.view.selectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if isFocused then
            local clueNode
            for i = 1, Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount do
                if self.m_spaceship:CheckClueCanPlaceByIndex(i) then
                    clueNode = self.view["clue" .. i]
                    break
                end
            end
            if not clueNode then
                for i = 1, Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount do
                    if not self.m_spaceship:GetSpaceshipRoomClueDataByCluePlaceIndex(i) then
                        clueNode = self.view["clue" .. i]
                        break
                    end
                end
            end
            if not clueNode then
                clueNode = self.view.clue1
            end
            self:SetNaviTarget(clueNode.inputBindingGroupNaviDecorator)
            InputManagerInst:ToggleBinding(self.m_showClueDetailBindingId, true)
        else
            self.m_nowNaviClueCell = nil
            InputManagerInst:ToggleBinding(self.m_showClueDetailBindingId, false)
            if DeviceInfo.usingController then
                AudioAdapter.PostEvent("Au_UI_Button_Back")
            end
        end
        if self.m_onIsFocusedChange then
            self.m_onIsFocusedChange(isFocused)
        end
    end)

    self.m_showClueDetailBindingId = UIUtils.bindInputPlayerAction("common_confirm", function()
        if self.m_nowNaviClueCell then
            self.m_nowNaviClueCell.clueCellButton.onClick:Invoke()
        end
    end, self.view.inputBindingGroupMonoTarget.groupId)
    InputManagerInst:ToggleBinding(self.m_showClueDetailBindingId, false)
end

GuestroomCluesCenterNode.InitGuestroomCluesCenterNode = HL.Method(HL.Any) << function(self, onFocus)
    self:_FirstTimeInit()
    self.m_onIsFocusedChange = onFocus
    self.m_spaceship = GameInstance.player.spaceship
    self:RefreshBottomState()
    self:RefreshCluesState(true)
end

GuestroomCluesCenterNode.RefreshData = HL.Method() << function(self)
    self:RefreshBottomState()
    self:RefreshCluesState(false)
end

GuestroomCluesCenterNode.RefreshBottomState = HL.Method() << function(self)

    local hasValue, roomInfo = self.m_spaceship:TryGetRoom(Tables.spaceshipConst.guestRoomClueExtensionId)
    local clueData = self.m_spaceship:GetClueData()
    if not clueData or not hasValue then
        return
    end
    local nowLevel = roomInfo.lv
    local levelTable = SpaceshipUtils.getRoomLvTableByType(roomInfo.type)
    if self.m_spaceship:IsGuestRoomClueAllPlace()
        and self.m_spaceship:CheckClueCollectionState() == CS.Beyond.Gameplay.SpaceshipClueState.Collection then
        self.view.iconImg:SampleClipAtPercent("spaceshiproomclueintell_loop", 1)
        self.view.btnNode:SetState("WaitForExchange")
        local levelData = levelTable[nowLevel]
        self.view.exchangeCreditText.text = levelData.baseCreditReward
        self.view.exchangeInfoText.text = levelData.baseInfoReward
    elseif self.m_spaceship:CheckClueCollectionIsExchangeState() then
        self.view.iconImg:PlayLoopAnimation()
        local levelData = levelTable[clueData.clueRoomLevel]
        local joinFriendCount = self.m_spaceship:GetClueCollectionRoomSpecialData().joinClueExchangeFriendRoleIds.Count or 0
        if joinFriendCount == 0 then
            self.view.btnNode:SetState("EtcFriendParticipate")
        else
            self.view.btnNode:SetState("Exchange")
        end
        self.view.exchangeCreditText.text = math.min(joinFriendCount * levelData.extraCreditPerRole, levelData.maxExtraCredit)
        self.view.exchangeInfoText.text = math.min(joinFriendCount * levelData.extraInfoPerRole, levelData.maxExtraInfo)
        self.view.exchangeFriendHelpText.text = joinFriendCount
        self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(math.max(clueData.infoExchangeExpireTs - DateTimeUtils.GetCurrentTimestampBySeconds(), 0))
        self:_ClearCoroutine(self.m_timeCor)
        self.m_timeCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
                self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(math.max(clueData.infoExchangeExpireTs - DateTimeUtils.GetCurrentTimestampBySeconds(), 0))
            end
        end)
    else
        self.view.iconImg:SampleClipAtPercent("spaceshiproomclueintell_loop", 1)
        self.view.btnNode:SetState("Collecting")
        local levelData = levelTable[nowLevel]
        self.view.exchangeCreditText.text = levelData.baseCreditReward
        self.view.exchangeInfoText.text = levelData.baseInfoReward
    end
end


GuestroomCluesCenterNode.RefreshCluesState = HL.Method(HL.Boolean) << function(self, isInit)
    local WARNING_TIME = 3600 * 24

    for i = 1, Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount do
        local clueNode = self.view["clue" .. i]
        local roomClueData = self.m_spaceship:GetSpaceshipRoomClueDataByCluePlaceIndex(i)
        if roomClueData then
            clueNode.stateController:SetState("Clue")
            clueNode.tagTime.gameObject:SetActive(roomClueData.expireTs ~= 0)
            if roomClueData.expireTs ~= 0 then
                clueNode.timeNode:StartTickLimitTime(roomClueData.expireTs, WARNING_TIME)
            end
        else
            clueNode.stateController:SetState("EmptyState")
            clueNode.tagTime.gameObject:SetActive(false)
        end
        clueNode.redDot.gameObject:SetActive(self.m_spaceship:CheckClueCanPlaceByIndex(i))
        clueNode.clueCellButton.onClick:RemoveAllListeners()
        clueNode.clueCellButton.onClick:AddListener(function()
            Notify(MessageConst.ON_PUSH_SPACESHIP_GUEST_ROOM_MAIN_PANEL,
                {SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Inventory, { i, roomClueData and roomClueData.instId or 0} })
        end)
        clueNode.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:RemoveAllListeners()
        clueNode.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
            if select then
                self.m_nowNaviClueCell = clueNode
            end
            if roomClueData then
                InputManagerInst:SetBindingText(self.m_showClueDetailBindingId, Language.LUA_CLUE_REPLACE_KEY_HINT)
            else
                InputManagerInst:SetBindingText(self.m_showClueDetailBindingId, Language.LUA_CLUE_FILL_IN_KEY_HINT)
            end
        end)
        for i = 1, Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount do
            local clueLineNode = self.view["clueLine" .. i]
            local clueNode1 = self.view["clue" .. i]
            local clueNode2
            if i + 1 > Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount then
                clueNode2 = self.view["clue1"]
            else
                clueNode2 = self.view["clue" .. (i + 1)]
            end
            if clueNode1 and clueNode1.stateController.currentStateName == "Clue" and clueNode2 and clueNode2.stateController.currentStateName == "Clue" then
                if not clueLineNode.gameObject.activeSelf then
                    clueLineNode.gameObject:SetActive(true)
                    if not isInit then
                        clueLineNode:PlayInAnimation()
                    end
                end
            else
                clueLineNode.gameObject:SetActive(false)
            end
        end
    end
end

GuestroomCluesCenterNode.SelectClueIndex = HL.Method(HL.Number) << function(self, index)
    if index <= 0 or index > Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount then
        self:UnSelectClueIndex()
        return
    end
    local clueNode = self.view["clue" .. index]
    if not clueNode then
        return
    end
    self:UnSelectClueIndex()
    self.m_selectNode = clueNode
    self.m_selectNode.selectedBG.gameObject:SetActive(true)
end

GuestroomCluesCenterNode.UnSelectClueIndex = HL.Method() << function(self)
    if self.m_selectNode then
        self.m_selectNode.selectedBG.gameObject:SetActive(false)
        self.m_selectNode = nil
    end
end

HL.Commit(GuestroomCluesCenterNode)
return GuestroomCluesCenterNode

