
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipGuestRoomClue
local PHASE_ID = PhaseId.SpaceshipGuestRoomClue
SpaceshipGuestRoomClueCtrl = HL.Class('SpaceshipGuestRoomClueCtrl', uiCtrl.UICtrl)

SpaceshipGuestRoomClueCtrl.m_roomId = HL.Field(HL.String) << ""

SpaceshipGuestRoomClueCtrl.m_spaceship = HL.Field(HL.Userdata)

SpaceshipGuestRoomClueCtrl.m_moveCam = HL.Field(HL.Boolean) << false

SpaceshipGuestRoomClueCtrl.m_isRemote = HL.Field(HL.Boolean) << false

SpaceshipGuestRoomClueCtrl.m_clearScreenKey = HL.Field(HL.Number) << -1

SpaceshipGuestRoomClueCtrl.m_panelStack = HL.Field(HL.Forward("Stack"))

SpaceshipGuestRoomClueCtrl.m_panelType = HL.Field(HL.Number) << SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Overview

SpaceshipGuestRoomClueCtrl.m_needCacheOutCome = HL.Field(HL.Boolean) << false

SpaceshipGuestRoomClueCtrl.m_outComeCache = HL.Field(HL.Table)

SpaceshipGuestRoomClueCtrl.m_subPanelGroupId = HL.Field(HL.Number) << -1

SpaceshipGuestRoomClueCtrl.m_nowNaviTarget = HL.Field(HL.Any)

SpaceshipGuestRoomClueCtrl.m_focusTabNode = HL.Field(HL.Any)

SpaceshipGuestRoomClueCtrl.m_creditLimit = HL.Field(HL.Number) << 0

SpaceshipGuestRoomClueCtrl.m_collectProgress = HL.Field(HL.Number) << -1

SpaceshipGuestRoomClueCtrl.m_needSettleInfo = HL.Field(HL.Boolean) << false

SpaceshipGuestRoomClueCtrl.m_isSettleInfo = HL.Field(HL.Boolean) << false

SpaceshipGuestRoomClueCtrl.m_inventoryWidget = HL.Field(HL.Any)

SpaceshipGuestRoomClueCtrl.m_inventoryGo = HL.Field(HL.Any)

SpaceshipGuestRoomClueCtrl.m_collectWidget = HL.Field(HL.Any)

SpaceshipGuestRoomClueCtrl.m_collectGo = HL.Field(HL.Any)

SpaceshipGuestRoomClueCtrl.m_receiveWidget = HL.Field(HL.Any)

SpaceshipGuestRoomClueCtrl.m_receiveGo = HL.Field(HL.Any)




SpaceshipGuestRoomClueCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SPACESHIP_ON_SYNC_ROOM_STATION] = "OnSpaceshipRoomStationSync",
    [MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE] = 'OnUpdateData',
    [MessageConst.ON_SPACESHIP_GUEST_ROOM_CLUE_EXCHANGE_EXPIRE] = '_OnExchangeExpire',
    [MessageConst.ON_SPACESHIP_GUEST_ROOM_CLUE_REWARD_ITEM] = 'OnSpaceshipGuestRoomClueRewardItem',
    [MessageConst.ON_POP_SPACESHIP_GUEST_ROOM_MAIN_PANEL] = 'PopPanel',
    [MessageConst.ON_PUSH_SPACESHIP_GUEST_ROOM_MAIN_PANEL] = 'OnPushGuestRoomCluePanel',
    [MessageConst.ON_ACTIVITY_NEW_DAY] = 'RefreshTime',
}


SpaceshipGuestRoomClueCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnCloseBtnClick()
    end)

    self.view.helpBtn.onClick:AddListener(function()
        UIManager:AutoOpen(PanelId.SpaceshipClueHelp)
    end)

    self.view.closeArea.onClick:AddListener(function()
        if self.m_panelStack:Peek() ~= SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Overview then
            self:PopPanel()
        end
    end)
    self.view.closeArea.gameObject:SetActive(false)
    self.m_spaceship = GameInstance.player.spaceship
    self:InitRightTab()
    self.m_moveCam = arg.moveCam == true
    self.m_isRemote = arg.isRemoteCamera == true
    self.m_clearScreenKey = arg.clearScreenKey or -1

    self.m_roomId = Tables.spaceshipConst.guestRoomClueExtensionId
    self.m_panelStack = require_ex("Common/Utils/DataStructure/Stack")()
    self:_InitRoomInfo()
    self:_PushPanel(SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Overview)
    self.view.tabNode.onIsFocusedChange:RemoveAllListeners()
    self.view.tabNode.onIsFocusedChange:AddListener(function(isFocused)
        if isFocused then
            if not self.m_focusTabNode then
                self.m_focusTabNode = self.view.collect.button
            end
            self:SetNaviTarget(self.m_focusTabNode)
        else
            if DeviceInfo.usingController then
                AudioAdapter.PostEvent("Au_UI_Button_Back")
            end
        end
    end)
    self.view.currencyNode.onIsFocusedChange:RemoveAllListeners()
    self.view.currencyNode.onIsFocusedChange:AddListener(function(isFocused)
        if isFocused then
            self.view.controllerFocusHintNode.gameObject:SetActive(false)
        elseif DeviceInfo.usingController then
            self.view.controllerFocusHintNode.gameObject:SetActive(true)
        end
    end)

    GameInstance.player.spaceship:GetClueInfo()
    self:PlayAnimationIn()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    local recoverState = arg and arg.recoverState or nil
    if recoverState then
        self:_TryRecoverState(recoverState)
        arg.recoverState = nil
    end
    if arg and arg.clueHelpOpen then
        UIManager:AutoOpen(PanelId.SpaceshipClueHelp)
        arg.clueHelpOpen = nil
    end
    if arg and arg.instructionBookId then
        UIManager:Open(PanelId.InstructionBook, arg.instructionBookId)
        arg.instructionBookId = nil
    end


end

SpaceshipGuestRoomClueCtrl.OnShow = HL.Override() << function(self)
    self.m_spaceship:SetNeedTickExpiredClue(true)
    self:_RefreshShowAnimState()
end

SpaceshipGuestRoomClueCtrl.OnHide = HL.Override() << function(self)

end

SpaceshipGuestRoomClueCtrl.OnClose = HL.Override() << function(self)
    self:_CloseSubPanelControllerSmallMenu()
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
    self.m_spaceship:SetNeedTickExpiredClue(false)
    if self.m_inventoryGo then
        GameObject.Destroy(self.m_inventoryGo)
        self.m_inventoryGo = nil
        self.m_inventoryWidget = nil
    end
    if self.m_collectGo then
        GameObject.Destroy(self.m_collectGo)
        self.m_collectGo = nil
        self.m_collectWidget = nil
    end
    if self.m_receiveGo then
        GameObject.Destroy(self.m_receiveGo)
        self.m_receiveGo = nil
        self.m_receiveWidget = nil
    end
end
SpaceshipGuestRoomClueCtrl.InitRightTab = HL.Method() << function(self)
    local clueData = self.m_spaceship:GetClueData()
    if not clueData then
        return
    end
    self.view.collect.button.onClick:AddListener(function()
        self:_PushPanel(SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Collect)
    end)

    local nowProgress = self.m_spaceship:GetNextStationClueNowProgress()
    self.view.collect.progress.fillAmount = nowProgress / Tables.spaceshipConst.clueCollectPointMaxValue
    local targetValue
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            nowProgress = self.m_spaceship:GetNextStationClueNowProgress()
            targetValue = nowProgress / Tables.spaceshipConst.clueCollectPointMaxValue
            if (targetValue >= 1 and self.m_collectProgress ~= targetValue) or (self.m_collectProgress >= 1 and self.m_collectProgress ~= targetValue) then
                self.m_collectProgress = targetValue
                if self.m_collectProgress == 1 and self.m_spaceship:IsClueSelfLimitBeenReached() then
                    Notify(MessageConst.ON_SPACESHIP_CLUE_FULL)
                end
                self:OnUpdateData()
            end
            self.view.collect.progress.fillAmount = targetValue
        end
    end)
    self.view.receive.button.onClick:AddListener(function()
        self:_PushPanel(SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Receive)
    end)

    self.view.giftClues.button.onClick:AddListener(function()
        self:_PushPanel(SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.GiftClues)
    end)

    self.view.inventory.button.onClick:AddListener(function()
        self:_PushPanel(SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Inventory)
    end)
end


SpaceshipGuestRoomClueCtrl._InitRoomInfo = HL.Method() << function(self)
    self.view.spaceshipRoomCommonInfo:InitSpaceshipRoomCommonInfo(self.m_roomId, self.m_moveCam, self.m_isRemote)
    self.view.guestroomCluesCenterNode:InitGuestroomCluesCenterNode(function(isFocused)
        if isFocused then
            self.view.controllerFocusHintNode.gameObject:SetActive(false)
        elseif DeviceInfo.usingController then
            self.view.controllerFocusHintNode.gameObject:SetActive(true)
        end
    end)
    self.m_creditLimit = SpaceshipUtils.InitMoneyLimitCell(self.view.creditCell, Tables.spaceshipConst.creditItemId)
    self:RefreshTime()
    SpaceshipUtils.InitMoneyLimitCell(self.view.infoTokenCell, Tables.spaceshipConst.infoTokenItemId)
    self:RefreshWorkState()
end

SpaceshipGuestRoomClueCtrl.RefreshTime = HL.Method() << function(self)
    self.view.resetMoneyTimeTxt:InitCountDownText(
        Utils.getNextCommonServerRefreshTime(),
        nil,
        function(leftTime)
            local curMoney = Utils.getItemCount(Tables.spaceshipConst.creditItemId)
            self.view.timeNode.gameObject:SetActive(curMoney > self.m_creditLimit)
            return UIUtils.getFullLeftTime(leftTime)
        end
    )
end

SpaceshipGuestRoomClueCtrl._RefreshGainNode = HL.Method(HL.String, HL.Number) << function(self, itemId, itemCount)
    local moneyNode
    if itemId == Tables.spaceshipConst.creditItemId then
        moneyNode = self.view.creditCell
    elseif itemId == Tables.spaceshipConst.infoTokenItemId then
        moneyNode = self.view.infoTokenCell
    else
        return
    end
    local gainNode = moneyNode.view.gainNode
    gainNode.animationWrapper:ClearTween()
    gainNode.gameObject:SetActive(true)
    gainNode.animationWrapper:PlayWithTween("spaceshipcontrolcentergain_in", function()
        gainNode.gameObject:SetActive(false)
    end)
    gainNode.creditTxt.text = string.format("+%d", itemCount)
    moneyNode:InitMoneyCell(itemId)
end

SpaceshipGuestRoomClueCtrl.RefreshWorkState = HL.Method() << function(self)
    local playDecoAnim = function()
        self.view.lightImage.gameObject:SetActive(true)
        self.view.lightImage:PlayInAnimation()
    end
    local endDecoAnim = function()
        self.view.lightImage:PlayOutAnimation()
    end
    local room = self.m_spaceship.rooms:get_Item(self.m_roomId)
    local isCharInRoom = room.stationedCharList.Count > 0
    if self.m_spaceship:IsGuestRoomClueShutDown() then
        self.view.bottomTipsCellStateController:SetState("StatePause")
        endDecoAnim()
    elseif isCharInRoom and (self.m_spaceship:IsGuestRoomClueWorking() or self.m_spaceship:IsGuestRoomClueCannotAutoRecv()) then
        self.view.bottomTipsCellStateController:SetState("Collect")
        playDecoAnim()
    else
        self.view.bottomTipsCellStateController:SetState("StateIdle")
        endDecoAnim()
    end
end

SpaceshipGuestRoomClueCtrl.OnSpaceshipGuestRoomClueRewardItem = HL.Method(HL.Any) << function(self, args)
    local items, sources = unpack(args)
    self:_ShowOutcomePopup(items, sources)
end
SpaceshipGuestRoomClueCtrl._ShowOutcomePopup = HL.Method(HL.Any, HL.Any) << function(self, csItems, source)
    local itemMap = {}
    self.m_outComeCache = self.m_outComeCache or {}
    for i = 0, csItems.Count - 1 do
        local item = csItems[i]

        if itemMap[item.id or item.Id] then
            local accCount = itemMap[item.id or item.Id]
            itemMap[item.id or item.Id] = accCount + (item.Count or 0) + (item.count or 0)
            if self.m_needCacheOutCome then
                self.m_outComeCache[item.id or item.Id] = accCount + (item.Count or 0) + (item.count or 0)
            end
        else
            itemMap[item.id or item.Id] = (item.Count or 0) + (item.count or 0)
            if self.m_needCacheOutCome then
                self.m_outComeCache[item.id or item.Id] = (item.Count or 0) + (item.count or 0)
            end
        end
    end
    if not self.m_needCacheOutCome then
        for id, count in pairs(itemMap) do
            if id == Tables.spaceshipConst.creditItemId then
                SpaceshipUtils.PlayMoneyNodeGainAnim(self.view.creditCell, id, count)
            elseif id == Tables.spaceshipConst.infoTokenItemId then
                SpaceshipUtils.PlayMoneyNodeGainAnim(self.view.infoTokenCell, id, count)
            end
        end
        local items = {}
        for id, count in pairs(itemMap) do
            table.insert(items, {
                id = id,
                count = count,
            })
        end
        if source == CS.Beyond.GEnums.RewardSourceType.OpenInfoExchangeReward then
            Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
                title = Language.LUA_DEFAULT_SYSTEM_REWARD_POP_UP_TITLE,
                items = items,
            })
        end
    end
end

SpaceshipGuestRoomClueCtrl._ShowCacheOutcomePopup = HL.Method() << function(self)
    if not self.m_needCacheOutCome then
        return
    end
    if self.m_needCacheOutCome and self.m_outComeCache then
        for id, count in pairs(self.m_outComeCache) do
            self:_RefreshGainNode(id, count)
        end
    end
    self.m_outComeCache = {}
end


SpaceshipGuestRoomClueCtrl.OnSpaceshipRoomStationSync = HL.Method() << function(self)
    self.view.guestroomCluesCenterNode:RefreshData()
    self:RefreshWorkState()
end

SpaceshipGuestRoomClueCtrl.OnUpdateData = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    self.view.guestroomCluesCenterNode:RefreshData()
    local clueData = self.m_spaceship:GetClueData()
    if not clueData then
        return
    end
    self.m_focusTabNode = nil
    if self.m_spaceship:CheckClueCollectionState() == CS.Beyond.Gameplay.SpaceshipClueState.WaitingPrice then
        self:_OnExchangeExpire()
    end
    local canCollect = clueData.dailyClueIndex ~= 0 or self.m_spaceship:IsGuestRoomClueCannotAutoRecv()
    if canCollect then
        self.m_focusTabNode = self.view.collect.button
    end
    self.view.collect.redDot.gameObject:SetActive(canCollect)
    local receiveData = {}
    for _,value in cs_pairs(clueData.preReceiveClues) do
        for _, clueData in cs_pairs(value) do
            if not (clueData.expireTs > 0 and clueData.expireTs < DateTimeUtils.GetCurrentTimestampBySeconds()) then
                table.insert(receiveData, clueData)
            end
        end
    end
    local canReceive = #receiveData > 0
    self.view.receive.redDot.gameObject:SetActive(canReceive)
    if self.m_focusTabNode ~= nil then
       return
    end
    if canReceive then
        self.m_focusTabNode = self.view.receive.button
    else
        self.m_focusTabNode = self.view.collect.button
    end
end

SpaceshipGuestRoomClueCtrl._OnExchangeExpire = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    if self.m_spaceship:CheckClueCollectionState() ~= CS.Beyond.Gameplay.SpaceshipClueState.WaitingPrice or self.m_isSettleInfo then
        return
    end
    if self.m_panelStack:Peek() ~= SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Overview then
        self.m_needSettleInfo = true
        return
    end
    self.m_spaceship:SettleInfoExchange()
    self.m_isSettleInfo = true
    self.m_needSettleInfo = false
end

SpaceshipGuestRoomClueCtrl.OnPushGuestRoomCluePanel = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    local type, arg = unpack(args)
    self:_PushPanel(type, arg)
end

SpaceshipGuestRoomClueCtrl._LazyCreateWidget = HL.Method(HL.String).Return(HL.Any, HL.Userdata) << function(self, prefabName)
    local path = "Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Spaceship/Widgets/" .. prefabName .. ".prefab"
    local prefab = self:LoadGameObject(path)
    local go = CSUtils.CreateObject(prefab, self.view.gameObject)
    go.name = prefabName
    go.transform.localScale = Vector3.one
    go.transform:GetComponent("RectTransform").anchoredPosition = Vector2.zero
    go:SetActive(false)
    return Utils.wrapLuaNode(go), go
end

SpaceshipGuestRoomClueCtrl._GetOrCreateInventoryWidget = HL.Method().Return(HL.Any) << function(self)
    if self.m_inventoryWidget then
        return self.m_inventoryWidget
    end
    self.m_inventoryWidget, self.m_inventoryGo = self:_LazyCreateWidget("SpaceshipGuestroomInventory")
    return self.m_inventoryWidget
end

SpaceshipGuestRoomClueCtrl._GetOrCreateCollectWidget = HL.Method().Return(HL.Any) << function(self)
    if self.m_collectWidget then
        return self.m_collectWidget
    end
    self.m_collectWidget, self.m_collectGo = self:_LazyCreateWidget("SpaceshipClueCollectNode")
    return self.m_collectWidget
end

SpaceshipGuestRoomClueCtrl._GetOrCreateReceiveWidget = HL.Method().Return(HL.Any) << function(self)
    if self.m_receiveWidget then
        return self.m_receiveWidget
    end
    self.m_receiveWidget, self.m_receiveGo = self:_LazyCreateWidget("SpaceshipGuestroomReceiveClues")
    return self.m_receiveWidget
end

SpaceshipGuestRoomClueCtrl._PushPanel = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, panelType, args)
    local panelTypeConst = SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE
    args = args or {}
    logger.info("SpaceshipGuestRoomClueCtrl._PushPanel", panelType)
    if panelType ~= panelTypeConst.Overview and
        panelType ~= panelTypeConst.Settlement and
        panelType ~= panelType ~= panelTypeConst.GiftClues then
        self.m_nowNaviTarget = InputManagerInst.controllerNaviManager.curTarget
    end
    if panelType == panelTypeConst.Collect then
        self.view.closeArea.gameObject:SetActive(true)
        local collectNode = self:_GetOrCreateCollectWidget()
        collectNode.gameObject:SetActive(true)
        collectNode:InitSpaceshipClueCollectNode()
        self:_SetNeedCacheOutCome(false)
        self.m_subPanelGroupId = collectNode.view.inputBindingGroupMonoTarget.groupId
    elseif panelType == panelTypeConst.Receive then
        self.view.closeArea.gameObject:SetActive(true)
        local receiveClues = self:_GetOrCreateReceiveWidget()
        receiveClues.gameObject:SetActive(true)
        receiveClues:InitSpaceshipGuestroomReceiveClues()
        if self.view.stateController.currentStateName ~= "HalfScreen" then
            self.view.stateController:SetState("HalfScreen")
            self.view.animationWrapper:ClearTween()
            self.view.animationWrapper:PlayWithTween("spaceshipguestroomclue_change")
        end
        self.m_subPanelGroupId = receiveClues.view.inputBindingGroupMonoTarget.groupId
    elseif panelType == panelTypeConst.GiftClues then
        if not PhaseManager:CheckCanOpenPhaseAndToast(PhaseId.SpaceshipRoomClueGift) or PhaseManager:CheckIsInTransition() then
            return
        end
        self.view.closeArea.gameObject:SetActive(true)
        Notify(MessageConst.SHOW_SPACESHIP_CLUE_GIFT)
    elseif panelType == panelTypeConst.Inventory then
        self:PopPanel(true, true)
        self.view.closeArea.gameObject:SetActive(true)
        local inventory = self:_GetOrCreateInventoryWidget()
        inventory.gameObject:SetActive(true)
        local index, instId, clueToggleTab = unpack(args)
        inventory:InitSpaceshipGuestroomInventory(function(idx)
            self.view.guestroomCluesCenterNode:SelectClueIndex(idx)
        end, clueToggleTab or 0 ,index or 0 , instId or 0)
        if self.view.stateController.currentStateName ~= "HalfScreen" then
            self.view.stateController:SetState("HalfScreen")
            self.view.animationWrapper:ClearTween()
            self.view.animationWrapper:PlayWithTween("spaceshipguestroomclue_change")
        end
        self.m_subPanelGroupId = inventory.view.inputBindingGroupMonoTarget.groupId
    elseif panelType == panelTypeConst.Settlement then
        PhaseManager:OpenPhase(PhaseId.SpaceshipRoomClueSettlement,nil,nil, true)
        self:_SetNeedCacheOutCome(true)
    elseif panelType == panelTypeConst.Overview then
        if self.view.stateController.currentStateName ~= "Center" then
            self.view.stateController:SetState("Center")
        end
    end

    if panelType ~= panelTypeConst.Settlement and panelType ~= panelTypeConst.Overview then
        self.view.tabNodeAnimationWrapper:PlayOutAnimation()
        self.view.topBar:PlayOutAnimation()
    end
    if self.m_subPanelGroupId ~= -1 and DeviceInfo.usingController and panelType ~= panelTypeConst.Settlement then
        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = PANEL_ID,
            isGroup = true,
            id = self.m_subPanelGroupId,
            hintPlaceholder = self.view.controllerHintPlaceholder,
            noHighlight = true,
            rectTransform = self.view.gameObject.transform
        })
    end

    if not self.m_panelStack:Contains(panelType) then
        self.m_panelStack:Push(panelType)
    end
end

SpaceshipGuestRoomClueCtrl.PopPanel = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, ignoreOverViewAction, ignoreInventory)
    local panelTypeConst = SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE
    if self.m_panelStack:Peek() == panelTypeConst.Overview then
        return
    end
    local popPanel = self.m_panelStack:Pop()
    local peekPanel = self.m_panelStack:Peek()
    logger.info(string.format("SpaceshipGuestRoomClueCtrl._PopPanel popPanel:%s peekPanel:%s", popPanel, peekPanel))

    if peekPanel == panelTypeConst.Overview and not ignoreOverViewAction then
        if self.m_needSettleInfo then
            self:_OnExchangeExpire()
        end
        self:_SetNeedCacheOutCome(false)
        if DeviceInfo.usingController then
            self.view.tabNode:ManuallyFocus()
        end
        if self.view.stateController.currentStateName ~= "Center" and self.view.stateController.currentStateName ~= nil then
            self.view.stateController:SetState("Center")
            self.view.animationWrapper:ClearTween()
            self.view.animationWrapper:PlayWithTween("spaceshipguestroomclue_exchange")
        end
        self.view.closeArea.gameObject:SetActive(false)
        self:_CloseSubPanelControllerSmallMenu()

        if self.m_nowNaviTarget then
            self:SetNaviTarget(self.m_nowNaviTarget)
            self.m_nowNaviTarget = nil
        end
    end

    if popPanel == panelTypeConst.Collect then
        if self.m_collectWidget then
            self.m_collectWidget:FadeOut()
        end
    elseif popPanel == panelTypeConst.Receive then
        if self.m_receiveWidget then
            self.m_receiveWidget:FadeOut()
        end
        self.view.guestroomCluesCenterNode:UnSelectClueIndex()
    elseif popPanel == panelTypeConst.Inventory then
        if not ignoreInventory then
            if self.m_inventoryWidget then
                self.m_inventoryWidget:FadeOut()
            end
            self.view.guestroomCluesCenterNode:UnSelectClueIndex()
        end
    elseif popPanel == panelTypeConst.Settlement then
        self.m_spaceship:ClearClueSettleClueInfo()
        self.m_isSettleInfo = false
    end

    if popPanel ~= panelTypeConst.Settlement then
        if popPanel == panelTypeConst.Inventory and ignoreInventory then
            return
        end
        self.view.tabNodeAnimationWrapper:PlayInAnimation()
        self.view.topBar:PlayInAnimation()
    end
end

SpaceshipGuestRoomClueCtrl._SetNeedCacheOutCome = HL.Method(HL.Boolean) << function(self, state)
    if state == self.m_needCacheOutCome then
        return
    end
    if not state then
        self:_ShowCacheOutcomePopup()
    end
    self.m_needCacheOutCome = state
end

SpaceshipGuestRoomClueCtrl._CloseSubPanelControllerSmallMenu = HL.Method() << function(self)
    if self.m_subPanelGroupId == -1 then
        return
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.m_subPanelGroupId)
    self.m_subPanelGroupId = -1
end

SpaceshipGuestRoomClueCtrl._OnCloseBtnClick = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

SpaceshipGuestRoomClueCtrl._RefreshShowAnimState = HL.Method() << function(self)
    local panelTypeConst = SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE
    if self.m_panelStack:Peek() == panelTypeConst.Receive or
        self.m_panelStack:Peek() == panelTypeConst.Inventory then
        self.view.stateController:SetState("HalfScreen")
        self.view.animationWrapper:ClearTween()
        self.view.animationWrapper:PlayWithTween("spaceshipguestroomclue_change")
    end
end

SpaceshipGuestRoomClueCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {
        moveCam = self.m_moveCam,
        clearScreenKey = self.m_clearScreenKey,
    }
    local currentPanel = self.m_panelStack:Peek()
    if currentPanel == SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Inventory then
        local inventory = self.m_inventoryWidget
        if inventory and inventory.m_isOpen then
            arg.recoverState = {
                currentPanel = currentPanel,
                stateControllerStateName = self.view.stateController.currentStateName,
                clueToggleTab = inventory.m_clueToggleTab,
                indexTab = inventory.m_indexTab,
                lastNaviTargetIndex = inventory.m_lastNaviTargetIndex,
            }
        end
    elseif currentPanel == SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Collect then
        arg.recoverState = {
            currentPanel = currentPanel,
            stateControllerStateName = self.view.stateController.currentStateName,
        }
    elseif currentPanel == SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Receive then
        arg.recoverState = {
            currentPanel = currentPanel,
            stateControllerStateName = self.view.stateController.currentStateName,
        }
    end
    local isTopPhase = PhaseManager:GetTopPhaseId() == PHASE_ID
    if isTopPhase then
        local isHelpOpen, helpCtrl = UIManager:IsOpen(PanelId.SpaceshipClueHelp)
        if isHelpOpen and helpCtrl and helpCtrl:IsShow() then
            arg.clueHelpOpen = true
        end
        local isBookOpen, bookCtrl = UIManager:IsOpen(PanelId.InstructionBook)
        if isBookOpen and bookCtrl and bookCtrl:IsShow() and bookCtrl.id then
            arg.instructionBookId = bookCtrl.id
        end
    end
    return arg
end

SpaceshipGuestRoomClueCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    local panelTypeConst = SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE
    local currentPanel = recoverState.currentPanel
    if currentPanel == panelTypeConst.Inventory then
        self:_PushPanel(panelTypeConst.Inventory, {
            recoverState.indexTab or 0,
            0,
            recoverState.clueToggleTab or 0,
        })
        local inventory = self.m_inventoryWidget
        if inventory and recoverState.lastNaviTargetIndex and recoverState.lastNaviTargetIndex > 1 then
            inventory.m_lastNaviTargetIndex = recoverState.lastNaviTargetIndex
        end
    elseif currentPanel == panelTypeConst.Collect then
        self:_PushPanel(panelTypeConst.Collect)
    elseif currentPanel == panelTypeConst.Receive then
        self:_PushPanel(panelTypeConst.Receive)
    end
end

HL.Commit(SpaceshipGuestRoomClueCtrl)
