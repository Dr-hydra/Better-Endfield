local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SpaceshipGuestroomReceiveClues = HL.Class('SpaceshipGuestroomReceiveClues', UIWidgetBase)

SpaceshipGuestroomReceiveClues.m_onClose = HL.Field(HL.Function)

SpaceshipGuestroomReceiveClues.m_getCellFunc = HL.Field(HL.Function)

SpaceshipGuestroomReceiveClues.m_isOpen = HL.Field(HL.Boolean) << false

SpaceshipGuestroomReceiveClues.m_data = HL.Field(HL.Table)

SpaceshipGuestroomReceiveClues.m_batches = HL.Field(HL.Table)

SpaceshipGuestroomReceiveClues.m_currentBatchIndex = HL.Field(HL.Number) << 0

SpaceshipGuestroomReceiveClues.m_Index2Cell = HL.Field(HL.Table)

SpaceshipGuestroomReceiveClues.m_cellInstId2Index = HL.Field(HL.Table)

SpaceshipGuestroomReceiveClues.m_lastNaviTargetIndex = HL.Field(HL.Number) << 1


SpaceshipGuestroomReceiveClues._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_RECV_FRIEND_CLUE, function()
        self:_UpdateSpaceshipGuestroomReceiveCluesCache()
        self:_UpdateSpaceshipGuestroomReceiveCluesView()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CLUE_COLLECT_ALL_SUCCESS_TIP)
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE, function()
        self:_UpdateRecvNumber()
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_GUEST_ROOM_CLUE_REWARD_ITEM, function(args)
        local items, sources = unpack(args)
        SpaceshipUtils.ShowClueOutcomePopup(items, sources, self.view.moneyCell, nil)
    end)

    SpaceshipUtils.InitMoneyLimitCell(self.view.moneyCell, Tables.spaceshipConst.creditItemId)

    self.view.helpSmallBtn.onClick:RemoveAllListeners()
    self.view.helpSmallBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "clues")
    end)

    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        Notify(MessageConst.ON_POP_SPACESHIP_GUEST_ROOM_MAIN_PANEL)
        self:FadeOut()
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_HEAD_NAVI_TARGET_CHANGE, function(cell)
        if not self.m_isOpen then
            return
        end
        cell.view.clueCell.hintTextId = "LUA_CLUE_RECEIVE_HINT"
        self.m_lastNaviTargetIndex = self.m_cellInstId2Index[cell.m_clueCellData.instId] or 1
    end)

    self.view.collectAllBtn.onClick:RemoveAllListeners()
    self.view.collectAllBtn.onClick:AddListener(function()

        local hasExpired = false
        local hasValid = false
        local instIdList = {}
        for _, clueData in pairs(self.m_data) do
            if clueData.expireTs > 0 and clueData.expireTs < DateTimeUtils.GetCurrentTimestampBySeconds() then

                hasExpired = true
            else
                hasValid = true
                table.insert(instIdList, clueData.instId)
            end
        end
        if not hasValid or hasExpired then
            self:_UpdateSpaceshipGuestroomReceiveCluesCache()
            self:_UpdateSpaceshipGuestroomReceiveCluesView()
        end
        if not hasValid then

            Notify(MessageConst.SHOW_TOAST, Language.LUA_CLUE_COLLECT_EXPIRE_TIP)
            return
        end
        if hasExpired then

            Notify(MessageConst.SHOW_TOAST, Language.LUA_CLUE_COLLECT_PART_EXPIRE_TIP)
        end

        GameInstance.player.spaceship:RecvFriendClue(instIdList)
    end)

    self.view.inviteBtn.onClick:RemoveAllListeners()
    self.view.inviteBtn.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.Friend, {
            panelId = PanelId.FriendList,
            needClose = true,
            needTab = false,
            stateName = "SpaceShipClueInvite",
            title = Language.LUA_CLUE_SCHEDULE_INVITE_FRIEND_PANEL_TITLE
        })
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:RemoveAllListeners()
    self.view.scrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        local cell = self.m_getCellFunc(gameObject)
        local luaIndex = LuaIndex(csIndex)
        self.m_Index2Cell[luaIndex] = cell
        for i, v in ipairs(self.m_data) do
            self.m_cellInstId2Index[v.instId] = i
        end
        cell:InitGuestroomCluesCell(self.m_data[luaIndex], function()

            if self.m_data[luaIndex].expireTs < DateTimeUtils.GetCurrentTimestampBySeconds() then

                Notify(MessageConst.SHOW_TOAST, Language.LUA_CLUE_COLLECT_EXPIRE_TIP)
                self:_UpdateSpaceshipGuestroomReceiveCluesCache()
                self:_UpdateSpaceshipGuestroomReceiveCluesView()
                return
            end
            GameInstance.player.spaceship:RecvFriendClue({ self.m_data[luaIndex].instId })
        end)
    end)
end

SpaceshipGuestroomReceiveClues.InitSpaceshipGuestroomReceiveClues = HL.Method() << function(self)
    self:_FirstTimeInit()
    GameInstance.player.spaceship:GetClueInfo()
    self:FadeIn()
    self:_UpdateSpaceshipGuestroomReceiveCluesCache()


    local allRoleIds = {}
    for _, clueData in pairs(self.m_data) do
        table.insert(allRoleIds, clueData.fromRoleId)
    end

    local uniqueRoleIds = {}
    local added = {}
    for _, id in ipairs(allRoleIds) do
        local success , friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(id)
        if not added[id] and (not success or not friendInfo.shortId) then
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
    self:_UpdateRecvNumber()
    self.m_batches = batches
    self.m_currentBatchIndex = 0
    self.view.loadingNode.gameObject:SetActive(true)
    self:_ProcessNextBatch()
end

SpaceshipGuestroomReceiveClues._ProcessNextBatch = HL.Method() << function(self)
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

SpaceshipGuestroomReceiveClues._UpdateRecvNumber = HL.Method() << function(self)
    local isRecvCount = GameInstance.player.spaceship:GetRecvClueCount()
    local recvList = Tables.spaceshipConst.recvFriendClueAddCreditList
    local recvReward = 0
    if recvList.Count > isRecvCount then
        recvReward = recvList[isRecvCount]
    end
    self.view.numberTxt.text = recvReward
end

SpaceshipGuestroomReceiveClues._OnAllBatchesCompleted = HL.Method() << function(self)
    self:_UpdateSpaceshipGuestroomReceiveCluesView(true)
    self.view.loadingNode.gameObject:SetActive(false)
    self.view.selectableNaviGroup:NaviToThisGroup()
    self.m_batches = {}
    self.m_currentBatchIndex = 0
end


SpaceshipGuestroomReceiveClues._UpdateSpaceshipGuestroomReceiveCluesCache = HL.Method() << function(self)
    local data = GameInstance.player.spaceship:GetClueCollectionRoomSpecialData()
    local dict = data.clueData.preReceiveClues
    self.m_data = {}

    for _,value in cs_pairs(dict) do
        for _, clueData in cs_pairs(value) do

            if not (clueData.expireTs > 0 and clueData.expireTs < DateTimeUtils.GetCurrentTimestampBySeconds()) then
                table.insert(self.m_data, clueData)
            end
        end
    end

    table.sort(self.m_data, function(a, b)
        return a.expireTs < b.expireTs
    end)
end

SpaceshipGuestroomReceiveClues._UpdateSpaceshipGuestroomReceiveCluesView = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    self.m_Index2Cell = {}
    self.m_cellInstId2Index = {}
    self.view.scrollList:UpdateCount(#self.m_data, false, false, false, not isInit)
    self.view.emptyNode.gameObject:SetActive(#self.m_data == 0)
    if not isInit then
        self:Navi2LastNaviTarget()
    else
        self.view.scrollList:SetTop()
        self:NaviToFirstCell()
    end
end

SpaceshipGuestroomReceiveClues.Navi2LastNaviTarget = HL.Method() << function(self)
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

SpaceshipGuestroomReceiveClues.NaviToFirstCell = HL.Method() << function(self)
    local cell = self.m_getCellFunc(self.view.scrollList:Get(0))
    if not cell then
        return
    end
    self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)
end

SpaceshipGuestroomReceiveClues.FadeIn = HL.Method() << function(self)
    if self.m_isOpen then
        return
    end
    self.m_isOpen = true
    self.view.animationWrapper:ClearTween()
    self.view.animationWrapper:PlayInAnimation()
end

SpaceshipGuestroomReceiveClues.FadeOut = HL.Method() << function(self)
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

SpaceshipGuestroomReceiveClues._OnDestroy = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(SpaceshipGuestroomReceiveClues)
return SpaceshipGuestroomReceiveClues

