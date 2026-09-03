local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local GUEST_ROOM_CLUE_TYPE =
{
    [0] = CS.Beyond.Gameplay.GuestRoomClueType.Self | CS.Beyond.Gameplay.GuestRoomClueType.Receive,
    [1] = CS.Beyond.Gameplay.GuestRoomClueType.Self,
    [2] = CS.Beyond.Gameplay.GuestRoomClueType.Receive,
    [3] = CS.Beyond.Gameplay.GuestRoomClueType.PreReceive,
}
SpaceshipGuestroomInventory = HL.Class('SpaceshipGuestroomInventory', UIWidgetBase)

SpaceshipGuestroomInventory.m_onIndexChange = HL.Field(HL.Function)

SpaceshipGuestroomInventory.m_clueToggleTab = HL.Field(HL.Number) << 0

SpaceshipGuestroomInventory.m_indexTab = HL.Field(HL.Number) << 0

SpaceshipGuestroomInventory.m_currentClueInstId = HL.Field(HL.Number) << 0

SpaceshipGuestroomInventory.m_cacheData = HL.Field(HL.Table)

SpaceshipGuestroomInventory.m_cellInstId2Index = HL.Field(HL.Table)

SpaceshipGuestroomInventory.m_Index2Cell = HL.Field(HL.Table)

SpaceshipGuestroomInventory.m_batches = HL.Field(HL.Table)

SpaceshipGuestroomInventory.m_currentBatchIndex = HL.Field(HL.Number) << 0

SpaceshipGuestroomInventory.m_isOpen = HL.Field(HL.Boolean) << false

SpaceshipGuestroomInventory.m_getCellFunc = HL.Field(HL.Function)

SpaceshipGuestroomInventory.m_lastNaviTargetIndex = HL.Field(HL.Number) << 1

SpaceshipGuestroomInventory.m_loadingFinish = HL.Field(HL.Boolean) << false



SpaceshipGuestroomInventory._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_OPEN_INFO_EXCHANGE, function()
        if not self.m_isOpen then
            return
        end
        self:_UpdateCacheData()
        self:_UpdateView()
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_SELF_CLUE_DELETE, function()
        if not self.m_isOpen then
            return
        end
        self:_RefreshCacheData()
        self:_UpdateView()
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_HEAD_NAVI_TARGET_CHANGE, function(cell)
        if not self.m_isOpen then
            return
        end
        if not cell.m_clueCellData then
            return
        end
        local succ, data = Tables.spaceshipClueDataTable:TryGetValue(cell.m_clueCellData.clueId)
        if not succ then
            return
        end
        local hintTextId
        if GameInstance.player.spaceship:CheckIndexHasBeenPlaced(data.clueType) then
            hintTextId = "LUA_CLUE_REPLACE_KEY_HINT"
        else
            hintTextId = "LUA_CLUE_FILL_IN_KEY_HINT"
        end
        cell.view.clueCell.hintTextId = hintTextId

        self.m_lastNaviTargetIndex = self.m_cellInstId2Index[cell.m_clueCellData.instId] or 1
    end)

    SpaceshipUtils.InitMoneyLimitCell(self.view.moneyCell, Tables.spaceshipConst.creditItemId)
    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        Notify(MessageConst.ON_POP_SPACESHIP_GUEST_ROOM_MAIN_PANEL)
        self.view.scrollView:UpdateCount(0)
    end)

    for i = 0, 2 do
        self.view['clueToggle' .. i].onValueChanged:RemoveAllListeners()
        self.view['clueToggle' .. i].onValueChanged:AddListener(function(isOn)
            if isOn then
                self.m_clueToggleTab = i
                self:_UpdateCacheData()
                self:_UpdateView(true)
                self.view.scrollView:SetTop()
                self:NaviToFirstCell()
            end
        end)
    end

    for i = 0, Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount do
        self.view['toggle' .. i .. 'Cell'].onValueChanged:RemoveAllListeners()
        self.view['toggle' .. i .. 'Cell'].onValueChanged:AddListener(function(isOn)
            if isOn then
                self.m_indexTab = i
                self:_UpdateCacheData()
                self:_UpdateView(true)
                self.view.scrollView:SetTop()
                self:NaviToFirstCell()
                if self.m_onIndexChange then
                    self.m_onIndexChange(self.m_indexTab)
                end
            end
        end)
    end

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollView)
    self.view.scrollView.onGraduallyShowFinish:RemoveAllListeners()
    self.view.scrollView.onGraduallyShowFinish:AddListener(function()
        if DeviceInfo.usingController and self.m_loadingFinish and self.m_isOpen then
            self.view.selectableNaviGroup:NaviToThisGroup()
            if self.m_lastNaviTargetIndex > 1 then
                self:Navi2LastNaviTarget()
            else
                self:NaviToFirstCell()
            end
        end
    end)

    self.view.scrollView.onUpdateCell:RemoveAllListeners()
    self.view.scrollView.onUpdateCell:AddListener(function(gameObject, csIndex)
        local cell = self.m_getCellFunc(gameObject)
        local luaIndex = LuaIndex(csIndex)
        local instId = self.m_cacheData[luaIndex].instId
        self.m_cellInstId2Index[instId] = luaIndex
        self.m_Index2Cell[luaIndex] = cell
        for i, v in ipairs(self.m_cacheData) do
            self.m_cellInstId2Index[v.instId] = i
        end
        cell:InitGuestroomCluesCell(self.m_cacheData[luaIndex], function(hasBeenPlaced)
            local spaceship = GameInstance.player.spaceship
            local clueInstId = self.m_cacheData[luaIndex].instId
            local expireTs = self.m_cacheData[luaIndex].expireTs
            if expireTs > 0 and expireTs < DateTimeUtils.GetCurrentTimestampBySeconds() then
                self:_RefreshCacheData()
                self:_UpdateView()
                Notify(MessageConst.SHOW_TOAST, Language.LUA_CLUE_COLLECT_EXPIRE_TIP)
                return
            end
            if hasBeenPlaced then
                spaceship:CancelPlaceClue(clueInstId)
            else
                spaceship:PlaceClue(instId)
            end
            self:_RefreshCacheData()
            self:_UpdateView()
        end)
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_GUEST_ROOM_CLUE_REWARD_ITEM, function(args)
        local items, sources = unpack(args)
        SpaceshipUtils.ShowClueOutcomePopup(items, sources, self.view.moneyCell, nil)
    end)
end

SpaceshipGuestroomInventory.InitSpaceshipGuestroomInventory = HL.Method(HL.Opt(HL.Function, HL.Number, HL.Number, HL.Number)) << function(self, onIndexChange, toggleTab, indexTab, currentClueInstId)
    self:_FirstTimeInit()
    self:FadeIn()
    self.m_onIndexChange = onIndexChange
    self.m_lastNaviTargetIndex = 1
    self.m_currentClueInstId = currentClueInstId or 0
    self.m_clueToggleTab = toggleTab or 0
    self.m_indexTab = indexTab or 0
    self.m_loadingFinish = false
    self.view['clueToggle' .. self.m_clueToggleTab].isOn = true
    self.view['toggle' .. self.m_indexTab .. 'Cell'].isOn = true
    if self.m_onIndexChange then
        self.m_onIndexChange(self.m_indexTab)
    end

    self:_UpdateCacheData()
    local allRoleIds = {}
    for _, clueData in pairs(self.m_cacheData) do
        table.insert(allRoleIds, clueData.fromRoleId)
    end

    local uniqueRoleIds = {}
    local added = {}
    for _, id in ipairs(allRoleIds) do
        local success , friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(id)
        if not added[id] and (not success or string.isEmpty(friendInfo.name)) then
            table.insert(uniqueRoleIds, id)
            added[id] = true
        end
    end
    local batchSize = 10
    local batches = {}
    for i = 1, #uniqueRoleIds, batchSize do
        local batch = {}
        for j = i, math.min(i + batchSize - 1, #uniqueRoleIds) do
            table.insert(batch, uniqueRoleIds[j])
        end
        table.insert(batches, batch)
    end

    self.m_batches = batches
    self.m_currentBatchIndex = 0
    self.view.scrollView.gameObject:SetActive(false)
    self.view.loadingNode.gameObject:SetActive(true)
    self:_ProcessNextBatch()
end

SpaceshipGuestroomInventory._ProcessNextBatch = HL.Method() << function(self)
    self.m_currentBatchIndex = self.m_currentBatchIndex + 1
    if self.m_currentBatchIndex > #self.m_batches then
        self:_OnAllBatchesCompleted()
        return
    end

    local currentBatch = self.m_batches[self.m_currentBatchIndex]
    GameInstance.player.friendSystem:SyncSocialFriendInfo(currentBatch, function(removeId)
        self:_ProcessNextBatch()
    end)
end

SpaceshipGuestroomInventory._OnAllBatchesCompleted = HL.Method() << function(self)
    self.view.loadingNode.gameObject:SetActive(false)
    self.view.scrollView.gameObject:SetActive(true)
    self.m_batches = {}
    self.m_currentBatchIndex = 0
    self.m_loadingFinish = true
    self:_UpdateView(true)
    if self.m_lastNaviTargetIndex > 1 and #self.m_cacheData > 0 then
        local scrollIndex = math.min(self.m_lastNaviTargetIndex, #self.m_cacheData)
        self.m_lastNaviTargetIndex = scrollIndex
        self.view.scrollView:ScrollToIndex(CSIndex(scrollIndex), true)
        if DeviceInfo.usingController then
            self:Navi2LastNaviTarget()
        end
    else
        self.view.scrollView:SetTop()
    end
end


SpaceshipGuestroomInventory.NaviToFirstCell = HL.Method() << function(self)
    if not DeviceInfo.usingController or not self.m_isOpen then
        return
    end
    local cell = self.m_getCellFunc(self.view.scrollView:Get(0))
    if not cell then
        return
    end
    self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)
end


SpaceshipGuestroomInventory.Navi2LastNaviTarget = HL.Method() << function(self)
    if not DeviceInfo.usingController or not self.m_isOpen then
        return
    end
    while self.m_lastNaviTargetIndex > 0 do
        local cell = self.m_Index2Cell[self.m_lastNaviTargetIndex]
        if cell then
            self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)
            return
        end
        self.m_lastNaviTargetIndex = self.m_lastNaviTargetIndex - 1
    end
end

SpaceshipGuestroomInventory._UpdateCacheData = HL.Method() << function(self)
    self.m_cacheData = {}
    local data = GameInstance.player.spaceship:GetCluesByIndex(self.m_indexTab, GUEST_ROOM_CLUE_TYPE[self.m_clueToggleTab])
    if data == nil then
        return
    end

    for _, clueData in pairs(data) do
        table.insert(self.m_cacheData, clueData)
    end

    local clueData = GameInstance.player.spaceship:GetClueData()
    if not clueData then
        return
    end
    table.sort(self.m_cacheData, function(a, b)
        local hasAPlaced, _ = clueData.placedClueStatusReverse:TryGetValue(a.instId)
        local hasBPlaced, _ = clueData.placedClueStatusReverse:TryGetValue(b.instId)
        local aData = Tables.spaceshipClueDataTable:GetValue(a.clueId)
        local bData = Tables.spaceshipClueDataTable:GetValue(b.clueId)
        if hasAPlaced and not hasBPlaced then
            return true
        elseif not hasAPlaced and hasBPlaced then
            return false
        elseif hasAPlaced and hasBPlaced then
            if aData and bData then
                return aData.clueType < bData.clueType
            end
        end
        if aData and bData and aData.clueType ~= bData.clueType then
            return aData.clueType < bData.clueType
        end

        local receiveCluesA, instDictA = clueData.receiveClues:TryGetValue(a.clueId)
        local receiveCluesB, instDictB = clueData.receiveClues:TryGetValue(b.clueId)

        local isAReceived = receiveCluesA and instDictA:TryGetValue(a.instId)
        local isBReceived = receiveCluesB and instDictB:TryGetValue(b.instId)

        local selfCluesA, selfInstDictA = clueData.selfClues:TryGetValue(a.clueId)
        local selfCluesB, selfInstDictB = clueData.selfClues:TryGetValue(b.clueId)

        local isASelfOwned = selfCluesA and selfInstDictA:TryGetValue(a.instId)
        local isBSelfOwned = selfCluesB and selfInstDictB:TryGetValue(b.instId)

        if isAReceived and not isBReceived then
            return true
        elseif not isAReceived and isBReceived then
            return false
        elseif isAReceived and isBReceived then
            local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
            local aRemainTime = a.expireTs - currentTime
            local bRemainTime = b.expireTs - currentTime
            aRemainTime = math.max(0, aRemainTime)
            bRemainTime = math.max(0, bRemainTime)
            return aRemainTime < bRemainTime
        end
        if isASelfOwned and isBSelfOwned then
            return a.getTs > b.getTs
        end

        return a.instId < b.instId
    end)
end

SpaceshipGuestroomInventory._RefreshCacheData = HL.Method() << function(self)
    if not self.m_cacheData then
        self:_UpdateCacheData()
        return
    end
    local clueData = GameInstance.player.spaceship:GetClueData()
    if not clueData then
        return
    end

    local tempCacheData = {}
    for i, data in ipairs(self.m_cacheData) do
        local haveData, clueId = clueData.clueInstId2Index:TryGetValue(data.instId)
        if haveData then
            table.insert(tempCacheData, data)
        end
    end
    self.m_cacheData = tempCacheData
end


SpaceshipGuestroomInventory._UpdateView = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    if #self.m_cacheData == 0 then
        self.view.emptyNode.gameObject:SetActive(true)
        if self.m_indexTab == 0 then
            self.view.emptyText.text = Language.LUA_NO_CLUE_TIPS
        else
            local succ, data = Tables.spaceshipClueDataIndex2IdTable:TryGetValue(self.m_indexTab)
            if succ then
                self.view.emptyText.text = string.format(Language.LUA_NO_CLUE_TYPE_TIPS, data.name)
            end
        end
    else
        self.view.emptyNode.gameObject:SetActive(false)
    end
    self.m_cellInstId2Index = {}
    self.m_Index2Cell = {}
    self.view.scrollView:UpdateCount(#self.m_cacheData, false, false, false, not isInit)
    if not isInit then
        self:Navi2LastNaviTarget()
    end
    local data = GameInstance.player.spaceship:GetCluesByIndex(0, CS.Beyond.Gameplay.GuestRoomClueType.Self)
    local count = data == nil and 0 or data.Count
    self.view.countTxt.text = string.format("%d/%d", count, Tables.spaceshipConst.selfClueStorageMaxCount)
end

SpaceshipGuestroomInventory.FadeIn = HL.Method() << function(self)
    if self.m_isOpen then
        return
    end
    self.m_isOpen = true
    self.view.gameObject:SetActive(true)
    self.view.animationWrapper:PlayInAnimation()
end

SpaceshipGuestroomInventory.FadeOut = HL.Method() << function(self)
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

SpaceshipGuestroomInventory._OnDestroy = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(SpaceshipGuestroomInventory)
return SpaceshipGuestroomInventory

