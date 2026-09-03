local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipRoomClueSchedule
local PHASE_ID = PhaseId.SpaceshipRoomClueSchedule
SpaceshipRoomClueScheduleCtrl = HL.Class('SpaceshipRoomClueScheduleCtrl', uiCtrl.UICtrl)






SpaceshipRoomClueScheduleCtrl.s_messages = HL.StaticField(HL.Table) << {

}

SpaceshipRoomClueScheduleCtrl.m_getFriendCell = HL.Field(HL.Function)

SpaceshipRoomClueScheduleCtrl.m_requestHandle = HL.Field(HL.Number) << -1

SpaceshipRoomClueScheduleCtrl.m_requestTime = HL.Field(HL.Number) << 1

SpaceshipRoomClueScheduleCtrl.m_requestIdList = HL.Field(HL.Table)

SpaceshipRoomClueScheduleCtrl.m_needRequestIdDict = HL.Field(HL.Table)

SpaceshipRoomClueScheduleCtrl.m_alreadyRequestIdDict = HL.Field(HL.Table)

SpaceshipRoomClueScheduleCtrl.m_requestLuaIndex = HL.Field(HL.Number) << 1

SpaceshipRoomClueScheduleCtrl.m_showFriendIds = HL.Field(HL.Table)

SpaceshipRoomClueScheduleCtrl.m_csIndex2Cell = HL.Field(HL.Table)

SpaceshipRoomClueScheduleCtrl.m_friendJumpInAction = HL.Field(HL.Number) << -1

SpaceshipRoomClueScheduleCtrl.m_friendJumpOutAction = HL.Field(HL.Number) << -1

SpaceshipRoomClueScheduleCtrl.m_inFriendGroup = HL.Field(HL.Boolean) << false

SpaceshipRoomClueScheduleCtrl.m_recoverControllerInfo = HL.Field(HL.Boolean) << false

SpaceshipRoomClueScheduleCtrl.m_controllerFocusCsIndex = HL.Field(HL.Number) << -1

local RequestBatchNum = 10



SpaceshipRoomClueScheduleCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_requestIdList = {}
    self.m_needRequestIdDict = {}
    self.m_alreadyRequestIdDict = {}

    self.view.closeButton.enabled = true
    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.inviteButton.enabled = true
    self.view.inviteButton.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.Friend, {
            panelId = PanelId.FriendList,
            needClose = true,
            needTab = false,
            stateName = "SpaceShipClueInvite",
            title = Language.LUA_CLUE_SCHEDULE_INVITE_FRIEND_PANEL_TITLE
        })
    end)

    self.m_getFriendCell = UIUtils.genCachedCellFunction(self.view.scrollList)

    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_UpdateFriendCell(self.m_getFriendCell(obj), csIndex)
    end)

    self.view.stateNode:SetState("Empty")
    self.m_showFriendIds = {}

    local collectionData = GameInstance.player.spaceship:GetClueCollectionRoomSpecialData()
    if collectionData ~= nil then
        for k, v in pairs(collectionData.joinClueExchangeFriendRoleIds) do
            table.insert(self.m_showFriendIds, v)
        end
    end



    self.view.clueCommunicateFriendTxt.text = string.format(Language.LUA_SPACESHIP_CLUE_COMMUNICATE_TEXT, #self.m_showFriendIds)

    local creditCount = 0
    local infoRewardCount = 0
    local clueData = GameInstance.player.spaceship:GetClueData()
    if clueData ~= nil then
        local success, data = Tables.spaceshipGuestRoomClueLvTable:TryGetValue(clueData.clueRoomLevel)
        if success then
            local calcRoleCount = math.min(data.maxCalcRoleCount, #self.m_showFriendIds)
            local extraCredit = math.min(data.maxExtraCredit, data.extraCreditPerRole * calcRoleCount)
            creditCount = extraCredit

            local extraInfoReward = math.min(data.maxExtraInfo, data.extraInfoPerRole * calcRoleCount)
            infoRewardCount = extraInfoReward
        end
    end

    self.view.awardNumTxt01.text = infoRewardCount
    self.view.awardNumTxt02.text = creditCount

    self.m_friendJumpInAction = self:BindInputPlayerAction("spaceship_clue_schedule_friend_focus", function()
        if not self.m_inFriendGroup then
            local res = self.view.scrollList:GetShowRange()
            for csIndex = res.x, res.y do
                local cell = self.m_csIndex2Cell[csIndex]
                if cell ~= nil then
                    self.view.scrollListSelectableNaviGroup:ManuallyFocus()
                    self:SetNaviTarget(cell.contactFriendCell.view.button)
                    self.m_inFriendGroup = true
                    self.view.closeButton.enabled = false
                    self.view.inviteButton.enabled = false
                    self.view.inviteKeyHint.gameObject:SetActive(false)
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
            self.view.closeButton.enabled = true
            self.view.inviteButton.enabled = true
            self.view.inviteKeyHint.gameObject:SetActive(true)
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
    end
end

SpaceshipRoomClueScheduleCtrl._HandleFriendInfo = HL.Method() << function(self)
    self.m_requestIdList = {}

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
            self:UpdateFriendCells()
        end)
    else
        self:UpdateFriendCells()
    end
end

SpaceshipRoomClueScheduleCtrl.UpdateFriendCells = HL.Method() << function(self)
    self.view.stateNode:SetState("Normal")
    self.m_csIndex2Cell = {}
    self.view.scrollList:UpdateCount(#self.m_showFriendIds)
    InputManagerInst:ToggleBinding(self.m_friendJumpInAction, true)
    InputManagerInst:ToggleBinding(self.m_friendJumpOutAction, false)
end

SpaceshipRoomClueScheduleCtrl._UpdateFriendCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local luaIndex = LuaIndex(csIndex)
    local roleId = self.m_showFriendIds[luaIndex]

    self.m_csIndex2Cell[csIndex] = cell

    local haveData = false
    local friendInfo = nil
    local success, info = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if success and info.init then
        haveData = true
        friendInfo = info
    end
    cell.contactFriendCell.view.state = "None"
    if haveData and friendInfo.init then
        local clickFun = function(clickIndex)
            if GameInstance.player.friendSystem:PlayerInBlackList(roleId) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_CLUE_FRIEND_IN_BLACK_LIST)
                return
            end
            FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD(roleId).action()
        end
        cell.contactFriendCell.view.state = "Normal"
        cell.contactFriendCell:InitContactFriendCell(roleId, friendInfo, csIndex, clickFun, clickFun)
    else
        local clickFun = function(clickIndex)
            if GameInstance.player.friendSystem:PlayerInBlackList(roleId) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_CLUE_FRIEND_IN_BLACK_LIST)
                return
            end
        end

        if self.m_needRequestIdDict[roleId] and not self.m_alreadyRequestIdDict[roleId] then
            cell.contactFriendCell.view.state = "Loading"
            cell.contactFriendCell:InitLoadingFriendCell(roleId, csIndex, clickFun)
        else
            cell.contactFriendCell.view.state = "Empty"
            cell.contactFriendCell:InitEmptyFriendCell(roleId, csIndex, clickFun)
        end
    end

    if #self.m_showFriendIds == 1 then
        cell.stateController:SetState("MoveMid")
    else
        cell.stateController:SetState("MoveNormal")
    end

    if luaIndex == 1 then
        cell.keyHint.gameObject:SetActive(true)
    else
        cell.keyHint.gameObject:SetActive(false)
    end

    cell.contactFriendCell.view.button.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.m_controllerFocusCsIndex = csIndex
        end
    end


end

SpaceshipRoomClueScheduleCtrl._RequestTick = HL.Method(HL.Number) << function(self, deltaTime)
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


SpaceshipRoomClueScheduleCtrl._SyncSocialFriendInfoPost = HL.Method() << function(self)
    local res = self.view.scrollList:GetShowRange()
    for csIndex = res.x, res.y do
        local cell = self.m_csIndex2Cell[csIndex]
        if cell ~= nil then
            if cell.contactFriendCell.view.state == "Loading" then
                local roleId = self.m_showFriendIds[csIndex + 1]
                if self.m_alreadyRequestIdDict[roleId] then
                    self:_UpdateFriendCell(cell, csIndex)
                end
            end
        end
    end
end

SpaceshipRoomClueScheduleCtrl._GetNextPageNotInitIds = HL.Method().Return(HL.Table) << function(self)
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

SpaceshipRoomClueScheduleCtrl.ShowSpaceshipClueSchedule = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    PhaseManager:OpenPhase(PHASE_ID)
end

SpaceshipRoomClueScheduleCtrl.OnClose = HL.Override() << function(self)
    if self.m_requestHandle > 0 then
        self.m_requestHandle = LuaUpdate:Remove(self.m_requestHandle)
    end
    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(SpaceshipRoomClueScheduleCtrl)
