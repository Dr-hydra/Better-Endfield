local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipRoomClueSettlement
local PHASE_ID = PhaseId.SpaceshipRoomClueSettlement
SpaceshipRoomClueSettlementCtrl = HL.Class('SpaceshipRoomClueSettlementCtrl', uiCtrl.UICtrl)






SpaceshipRoomClueSettlementCtrl.s_messages = HL.StaticField(HL.Table) << {

}

SpaceshipRoomClueSettlementCtrl.m_getFriendCell = HL.Field(HL.Function)

SpaceshipRoomClueSettlementCtrl.m_requestHandle = HL.Field(HL.Number) << -1

SpaceshipRoomClueSettlementCtrl.m_requestTime = HL.Field(HL.Number) << 1

SpaceshipRoomClueSettlementCtrl.m_requestIdList = HL.Field(HL.Table)

SpaceshipRoomClueSettlementCtrl.m_needRequestIdDict = HL.Field(HL.Table)

SpaceshipRoomClueSettlementCtrl.m_alreadyRequestIdDict = HL.Field(HL.Table)

SpaceshipRoomClueSettlementCtrl.m_requestLuaIndex = HL.Field(HL.Number) << 1

SpaceshipRoomClueSettlementCtrl.m_showFriendIds = HL.Field(HL.Table)

SpaceshipRoomClueSettlementCtrl.m_showListInfo = HL.Field(HL.Table)

SpaceshipRoomClueSettlementCtrl.m_creditCount = HL.Field(HL.Number) << -1

SpaceshipRoomClueSettlementCtrl.m_infoCount = HL.Field(HL.Number) << -1

SpaceshipRoomClueSettlementCtrl.m_friendJumpInAction = HL.Field(HL.Number) << -1

SpaceshipRoomClueSettlementCtrl.m_friendJumpOutAction = HL.Field(HL.Number) << -1

SpaceshipRoomClueSettlementCtrl.m_inFriendGroup = HL.Field(HL.Boolean) << false

SpaceshipRoomClueSettlementCtrl.m_csIndex2Cell = HL.Field(HL.Table)

local RequestBatchNum = 10

SpaceshipRoomClueSettlementCtrl.ShowSpaceshipClueSettlement = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    Notify(MessageConst.ON_PUSH_SPACESHIP_GUEST_ROOM_MAIN_PANEL, {SpaceshipConst.GUEST_ROOM_CLUE_PANEL_TYPE.Settlement})
end


SpaceshipRoomClueSettlementCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_requestIdList = {}
    self.m_needRequestIdDict = {}
    self.m_alreadyRequestIdDict = {}

    self.view.confirmBtn.enabled = true
    self.view.confirmBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
        Notify(MessageConst.ON_POP_SPACESHIP_GUEST_ROOM_MAIN_PANEL)
    end)

    self.m_getFriendCell = UIUtils.genCachedCellFunction(self.view.scrollList)

    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_UpdateScrollCell(self.m_getFriendCell(obj), csIndex)
    end)

    self.view.stateNode:SetState("Loading")

    self.m_creditCount = 0
    self.m_infoCount = 0
    local success, data = GameInstance.player.spaceship:GetClueSettleClueInfo()
    if success then
        local haveExtraCredit, extraCredit = data.extraRewardMap:TryGetValue(Tables.spaceshipConst.creditItemId)
        if haveExtraCredit then
            self.m_creditCount = self.m_creditCount + extraCredit
        end
        local haveExtraInfo, extraInfo = data.extraRewardMap:TryGetValue(Tables.spaceshipConst.infoTokenItemId)
        if haveExtraInfo then
            self.m_infoCount = self.m_infoCount + extraInfo
        end
    end

    self.m_showFriendIds = {}
    if success then
        if data and data.friendRoleIds then
            for k, v in pairs(data.friendRoleIds) do
                table.insert(self.m_showFriendIds, v)
            end
        end
    end




    self.m_friendJumpInAction = self:BindInputPlayerAction("spaceship_clue_schedule_friend_focus", function()
        if not self.m_inFriendGroup then
            local res = self.view.scrollList:GetShowRange()
            for csIndex = res.x, res.y do
                local cell = self.m_csIndex2Cell[csIndex]
                if cell ~= nil and cell.canJump then
                    self.view.scrollListSelectableNaviGroup:ManuallyFocus()
                    self:SetNaviTarget(cell._contactFriendCell1.view.button)
                    self.m_inFriendGroup = true
                    self.view.confirmBtn.enabled = false
                    self.view.confirmKeyHint.gameObject:SetActive(false)
                    InputManagerInst:ToggleBinding(self.m_friendJumpInAction, false)
                    InputManagerInst:ToggleBinding(self.m_friendJumpOutAction, true)
                    break
                end
            end
        end
    end)

    self.m_friendJumpOutAction = self:BindInputPlayerAction("spaceship_clue_schedule_friend_unfocus", function()
        if self.m_inFriendGroup then
            self.view.scrollListSelectableNaviGroup:ManuallyStopFocus()
            self.view.confirmBtn.enabled = true
            self.view.confirmKeyHint.gameObject:SetActive(true)
            self.m_inFriendGroup = false
            InputManagerInst:ToggleBinding(self.m_friendJumpInAction, true)
            InputManagerInst:ToggleBinding(self.m_friendJumpOutAction, false)
            if DeviceInfo.usingController then
                AudioAdapter.PostEvent("Au_UI_Button_Back")
            end
        end
    end, self.view.scrollListInputBindingGroupMonoTarget.groupId)

    InputManagerInst:ToggleBinding(self.m_friendJumpInAction, false)
    InputManagerInst:ToggleBinding(self.m_friendJumpOutAction, false)


    if #self.m_showFriendIds > 0 then
        self:_HandleFriendInfo()
    else
        self.view.stateNode:SetState("Empty")
        local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.guestRoomClueExtensionId)
        if hasValue and roomInfo then
            local roomLevel = roomInfo.lv
            local levelTable = SpaceshipUtils.getRoomLvTableByType(roomInfo.type)
            self.view.topPeopleNumTxt.text = levelTable[roomLevel].maxCalcRoleCount
            self.view.clueAwardNumTxt.text = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, levelTable[roomLevel].extraInfoPerRole)
            self.view.creditAwardNumTxt.text = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, levelTable[roomLevel].extraCreditPerRole)
        else
            self.view.topPeopleNumTxt.text = 0
            self.view.clueAwardNumTxt.text = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, 0)
            self.view.creditAwardNumTxt.text = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, 0)
        end
    end
end

SpaceshipRoomClueSettlementCtrl._HandleFriendInfo = HL.Method() << function(self)

    for _, roleId in pairs(self.m_showFriendIds) do
        local success, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
        if not success or not friendInfo.init then
            table.insert(self.m_requestIdList, roleId)
            self.m_needRequestIdDict[roleId] = true
        end
    end

    local ids = self:_GetNextPageNotInitIds()
    if #ids > 0 then
        GameInstance.player.friendSystem:SyncSocialFriendInfo(ids, function()
            self.m_requestHandle = LuaUpdate:Add("Tick", function(deltaTime)
                self:_RequestTick(deltaTime)
            end)
            self:UpdateScrollListInfo()
        end)
    else
        self:UpdateScrollListInfo()
    end
end

SpaceshipRoomClueSettlementCtrl.UpdateScrollListInfo = HL.Method() << function(self)
    self.view.stateNode:SetState("Normal")

    self:_UpdateShowListInfo()
    self.m_csIndex2Cell = {}
    self.view.scrollList:UpdateCount(#self.m_showListInfo)

    InputManagerInst:ToggleBinding(self.m_friendJumpInAction, true)
    InputManagerInst:ToggleBinding(self.m_friendJumpOutAction, false)
end

SpaceshipRoomClueSettlementCtrl._GetNextPageNotInitIds = HL.Method().Return(HL.Table) << function(self)
    local ids = {}
    for i = 1, RequestBatchNum do
        if self.m_requestLuaIndex <= #self.m_requestIdList then
            local roleId = self.m_requestIdList[self.m_requestLuaIndex]
            table.insert(ids, roleId)
            self.m_alreadyRequestIdDict[roleId] = true
            self.m_requestLuaIndex = self.m_requestLuaIndex + 1
        end
    end

    return ids
end

SpaceshipRoomClueSettlementCtrl._RequestTick = HL.Method(HL.Number) << function(self, deltaTime)
    self.m_requestTime = self.m_requestTime + deltaTime
    if self.m_requestTime < 1 then
        return
    end
    self.m_requestTime = 0

    local ids = self:_GetNextPageNotInitIds()
    if #ids == 0 then
        if self.m_requestHandle > 0 then
            self.m_requestHandle = LuaUpdate:Remove(self.m_requestHandle)
        end
        return
    end

    GameInstance.player.friendSystem:SyncSocialFriendInfo(ids, function()
        self:_SyncSocialFriendInfoPost()
    end)
end

SpaceshipRoomClueSettlementCtrl._SyncSocialFriendInfoPost = HL.Method() << function(self)
    local res = self.view.scrollList:GetShowRange()
    for csIndex = res.x, res.y do
        local cell = self.m_csIndex2Cell[csIndex]
        if cell ~= nil and cell.m_showInfo ~= nil and cell.m_showInfo.type == "FriendCells" then
            if cell._contactFriendCell1 and cell._contactFriendCell1.view.state == "Loading" then
                local roleId = cell.m_showInfo.roleId1
                if self.m_alreadyRequestIdDict[roleId] then
                    self:_UpdateFriendCell(cell, csIndex, 1, cell.m_showInfo)
                end
            end

            if cell._contactFriendCell2 and cell._contactFriendCell2.view.state == "Loading" then
                local roleId = cell.m_showInfo.roleId2
                if self.m_alreadyRequestIdDict[roleId] then
                    self:_UpdateFriendCell(cell, csIndex, 2, cell.m_showInfo)
                end
            end
        end
    end
end

SpaceshipRoomClueSettlementCtrl._UpdateShowListInfo = HL.Method() << function(self)
    self.m_showListInfo = {}

    local titleNodeAward = {
        type = "titleNodeAward",
    }
    table.insert(self.m_showListInfo, titleNodeAward)

    local awardNode = {
        type = "awardNode",
    }
    table.insert(self.m_showListInfo, awardNode)

    local titleNodeFriend = {
        type = "titleNodeFriend",
    }
    table.insert(self.m_showListInfo, titleNodeFriend)

    local showControllerTip = true
    for i = 1, #self.m_showFriendIds do
        if i % 2 == 0 then
            local FriendCells = {
                type = "FriendCells",
                roleNum = 2,
                roleId1 = self.m_showFriendIds[i - 1],
                roleId2 = self.m_showFriendIds[i],
                showControllerTip = showControllerTip,
            }
            showControllerTip = false
            table.insert(self.m_showListInfo, FriendCells)
        end
    end
    if #self.m_showFriendIds % 2 == 1 then
        local FriendCells = {
            type = "FriendCells",
            roleNum = 1,
            roleId1 = self.m_showFriendIds[#self.m_showFriendIds],
            roleId2 = 0,
            showControllerTip = showControllerTip,
        }
        table.insert(self.m_showListInfo, FriendCells)
    end
end

SpaceshipRoomClueSettlementCtrl._GetOrCreateNode = HL.Method(HL.Any, HL.String, HL.Any, HL.String).Return(HL.Any) << function(self, cell, cacheKey, templateGo, goName)
    if cell[cacheKey] then
        return cell[cacheKey]
    end
    local go = CSUtils.CreateObject(templateGo, cell.gameObject)
    go.name = goName
    go.transform.localScale = Vector3.one
    go:SetActive(false)
    local widget = Utils.wrapLuaNode(go)
    cell[cacheKey] = widget
    return widget
end

SpaceshipRoomClueSettlementCtrl._GetOrCreateFriendCellWidget = HL.Method(HL.Any, HL.Number).Return(HL.Any) << function(self, cell, index)
    local key = "_contactFriendCell" .. index
    if cell[key] then
        return cell[key]
    end
    local go = CSUtils.CreateObject(self.view.contactFriendCellTemplate.gameObject, cell.friendCells.gameObject)
    go.name = "ContactFriendCell" .. index
    go.transform.localScale = Vector3.one
    local rt = go:GetComponent("RectTransform")
    if index == 1 then
        rt.anchoredPosition = Vector2(-290, -1)
    else
        rt.anchoredPosition = Vector2(291, -1)
    end
    go:SetActive(false)
    local widget = Utils.wrapLuaNode(go)
    cell[key] = widget
    local btn = widget.view and widget.view.button
    if btn then
        btn.naviGroup = nil
    end
    return widget
end

SpaceshipRoomClueSettlementCtrl._UpdateScrollCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local luaIndex = LuaIndex(csIndex)
    local m_showInfo = self.m_showListInfo[luaIndex]
    self.m_csIndex2Cell[csIndex] = cell
    cell.canJump = false
    if cell._titleNodeAward then
        cell._titleNodeAward.gameObject:SetActive(false)
    end
    if cell._awardNode then
        cell._awardNode.gameObject:SetActive(false)
    end
    if cell._titleNodeFriend then
        cell._titleNodeFriend.gameObject:SetActive(false)
    end
    cell.friendCells.gameObject:SetActive(false)
    if cell._contactFriendCell1 then
        cell._contactFriendCell1.gameObject:SetActive(false)
        cell._contactFriendCell1.view.state = "None"
    end
    if cell._contactFriendCell2 then
        cell._contactFriendCell2.gameObject:SetActive(false)
        cell._contactFriendCell2.view.state = "None"
    end
    cell.m_showInfo = m_showInfo

    if m_showInfo.type == "FriendCells" then
        cell.friendCells.gameObject:SetActive(true)
        cell.keyHint.gameObject:SetActive(m_showInfo.showControllerTip)
        if m_showInfo.roleNum == 2 then
            self:_UpdateFriendCell(cell, csIndex, 1, m_showInfo)
            self:_UpdateFriendCell(cell, csIndex, 2, m_showInfo)
        elseif m_showInfo.roleNum == 1 then
            self:_UpdateFriendCell(cell, csIndex, 1, m_showInfo)
        end
    elseif m_showInfo.type == "titleNodeAward" then
        local node = self:_GetOrCreateNode(cell, "_titleNodeAward", self.view.titleNodeAwardTemplate.gameObject, "TitleNodeAward")
        node.gameObject:SetActive(true)
    elseif m_showInfo.type == "awardNode" then
        local node = self:_GetOrCreateNode(cell, "_awardNode", self.view.awardNodeTemplate.gameObject, "AwardNode")
        node.gameObject:SetActive(true)
        node.clueAwardNumTxt.text = self.m_infoCount
        node.creditAwardNumTxt.text = self.m_creditCount
        node.warningNode.gameObject:SetActive(false)
    elseif m_showInfo.type == "titleNodeFriend" then
        local node = self:_GetOrCreateNode(cell, "_titleNodeFriend", self.view.titleNodeFriendTemplate.gameObject, "TitleNodeFriend")
        node.gameObject:SetActive(true)
        node.friendParticipateText.text = string.format(Language.LUA_SPACESHIP_CLUE_SETTLEMENT_FRIEND_NUM, #self.m_showFriendIds )
    end

end

SpaceshipRoomClueSettlementCtrl._UpdateFriendCell = HL.Method(HL.Any, HL.Number, HL.Number, HL.Any) << function(self, cell, csIndex, sortNum, showInfo)
    cell.canJump = true
    local friendCell = nil
    local roleId = nil
    if sortNum == 1 then
        friendCell = self:_GetOrCreateFriendCellWidget(cell, 1)
        friendCell.gameObject:SetActive(true)
        roleId = showInfo.roleId1
    elseif sortNum == 2 then
        friendCell = self:_GetOrCreateFriendCellWidget(cell, 2)
        friendCell.gameObject:SetActive(true)
        roleId = showInfo.roleId2
    end
    if not friendCell then
        return
    end

    local haveData = false
    local friendInfo = nil
    local success, info = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if success and info.init then
        haveData = true
        friendInfo = info
    end

    if haveData and friendInfo.init then
        local clickFun = function(clickIndex)
            if GameInstance.player.friendSystem:PlayerInBlackList(roleId) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_CLUE_FRIEND_IN_BLACK_LIST)
                return
            end
            FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD(roleId).action()
        end
        friendCell.view.state = "Normal"
        friendCell:InitContactFriendCell(roleId, friendInfo, csIndex, clickFun, clickFun)
    else
        local clickFun = function(clickIndex)
            if GameInstance.player.friendSystem:PlayerInBlackList(roleId) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_CLUE_FRIEND_IN_BLACK_LIST)
                return
            end
        end

        if self.m_needRequestIdDict[roleId] and not self.m_alreadyRequestIdDict[roleId] then
            friendCell.view.state = "Loading"
            friendCell:InitLoadingFriendCell(roleId, csIndex, clickFun)
        else
            friendCell.view.state = "Empty"
            friendCell:InitEmptyFriendCell(roleId, csIndex, clickFun)
        end
    end
end

SpaceshipRoomClueSettlementCtrl.OnClose = HL.Override() << function(self)
    if self.m_requestHandle > 0 then
        self.m_requestHandle = LuaUpdate:Remove(self.m_requestHandle)
    end
    GameInstance.player.friendSystem:ClearSyncCallback()
end


HL.Commit(SpaceshipRoomClueSettlementCtrl)
