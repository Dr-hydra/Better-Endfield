
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendRequest

FriendRequestCtrl = HL.Class('FriendRequestCtrl', uiCtrl.UICtrl)

FriendRequestCtrl.m_friendList = HL.Field(HL.Table)

FriendRequestCtrl.m_arg = HL.Field(HL.Any)

FriendRequestCtrl.m_friendInitArg = HL.Field(HL.Table)

FriendRequestCtrl.m_emptyTipString = HL.Field(HL.String) << ""

FriendRequestCtrl.m_recoverState = HL.Field(HL.Table)





FriendRequestCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_REQUEST_INFO_SYNC] = 'OnSync',
    [MessageConst.ON_FRIEND_INFO_SYNC] = 'OnSync',
    [MessageConst.ON_FRIEND_CELL_INFO_CHANGE] = 'OnCellChange',
    [MessageConst.ON_SEND_MSG_FREQUENCY_ERROR] = 'OnFrequencyError',
}


FriendRequestCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})






    self.m_arg = arg
    self.m_recoverState = arg and arg.friendRequestState or nil
    self:_ChooseInitMethod()
end

FriendRequestCtrl._ChooseInitMethod = HL.Method() << function(self)
    local arg = self.m_arg
    if arg and arg.onShareClick then
        self:_InitFriendShare()
    else
        self:_InitFriendRequest()
    end
    self:_Refresh(true, true)
end

FriendRequestCtrl._InitFriendRequest = HL.Method() << function(self)
    GameInstance.player.friendSystem:SyncFriendRequestSimpleInfo()

    local initArg = FriendUtils.FRIEND_CELL_INIT_CONFIG.FriendRequest
    self.m_friendInitArg = initArg

    self.view.titleContentText.text = Language.LUA_FRIEND_POPUP_REQUEST
    self.view.friendList:InitFriendListCtrl(initArg)



    local function _onCloseAndRefresh()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            local _, friendCtrl = UIManager:IsOpen(PanelId.FriendList)
            if friendCtrl then
                friendCtrl:TryRefresh()
            end
        end)
    end
    self.view.btnClose.onClick:AddListener(_onCloseAndRefresh)
    self.view.bgImage.onClick:AddListener(_onCloseAndRefresh)
    self.m_emptyTipString = Language.LUA_FRIEND_REQUEST_EMPTY
    self:_ApplyRecoverState()
end

FriendRequestCtrl._InitFriendShare = HL.Method() << function(self)
    local arg = self.m_arg
    self.view.friendNumber.gameObject:SetActive(false)
    GameInstance.player.friendSystem:SyncFriendSimpleInfo()
    local initArg = FriendUtils.FRIEND_CELL_INIT_CONFIG.Share
    self.m_friendInitArg = initArg
    initArg.customCheckFriend = arg.customCheckFriend
    initArg.onShareClick = arg.onShareClick
    initArg.onSearchChange = function(str)
        self.view.clearBtn.gameObject:SetActiveIfNecessary(not (string.isEmpty(str)))
    end
    initArg.isFilter = true
    initArg.friendList = self.m_friendList

    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.bgImage.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.friendList:InitFriendListCtrl(initArg)
    self.view.titleContentText.text = Language.LUA_FRIEND_POPUP_VIEW
    self.m_emptyTipString = Language.LUA_FRIEND_NO_FRIEND
    self:_ApplyRecoverState()
end

FriendRequestCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    local friendRequestState = {}
    if sortNode then
        friendRequestState.sortState = {
            selectedIndex = sortNode:GetCurSelectedIndex(),
            isIncremental = sortNode.isIncremental == true,
        }
    end
    arg.friendRequestState = friendRequestState
    return arg
end

FriendRequestCtrl.OnSync = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh()
    GameInstance.player.friendSystem:SaveWaitAccept()
end

FriendRequestCtrl.OnCellChange = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh(true)
end

FriendRequestCtrl.OnFrequencyError = HL.Method() << function(self)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_SEND_REQUEST_FREQUENCY_LIMIT)
end

FriendRequestCtrl._UpdateCache = HL.Method() << function(self)
    self.m_friendList = {}
    local friendSystem = GameInstance.player.friendSystem
    local index = 1
    if self.m_friendInitArg then
        local friendInfoDic = friendSystem:GetDictInfo(self.m_friendInitArg.infoDicIndex)
        local customCheckFriend = self.m_friendInitArg.customCheckFriend
        for _, friendInfo in cs_pairs(friendInfoDic) do
            local valid = customCheckFriend == nil or customCheckFriend(friendInfo)
            if valid then
                self.m_friendList[index] = FriendUtils.friendInfo2SortInfo(friendInfo)
                index = index + 1
            end
        end
    end
end

FriendRequestCtrl._Refresh = HL.Method(HL.Opt(HL.Boolean,HL.Boolean)) << function(self, stayTop, loading)
    local friendSystem = GameInstance.player.friendSystem

    local maxRequestFriendCount = Tables.globalConst.friendRequestListLenMax
    self.view.countText.text =  string.format("%d/%d", friendSystem.currentRequestFriendCount, maxRequestFriendCount)
    self.view.fullTipRoot.gameObject:SetActiveIfNecessary(friendSystem.currentRequestFriendCount == maxRequestFriendCount)

    if stayTop == true and self.m_friendList ~= nil and #self.m_friendList > 0 then
        self.view.friendList:RefreshInfoStayPos(self.m_friendList)
    else
        self.view.friendList:RefreshInfo(self.m_friendList, true, self.m_emptyTipString, loading)
    end
end

FriendRequestCtrl._ApplyRecoverState = HL.Method() << function(self)
    local sortState = self.m_recoverState and self.m_recoverState.sortState or nil
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    if sortState and sortNode and sortNode.view and sortNode.view.mobilePCNode and sortNode.view.mobilePCNode.dropDown then
        local optionCount = #self.view.friendList.m_sortOptions
        if optionCount > 0 then
            local optionIndex = math.max(1, math.min(sortState.selectedIndex or 1, optionCount))
            sortNode.isIncremental = sortState.isIncremental == true
            sortNode:RefreshIncremental()
            sortNode.view.mobilePCNode.dropDown:SetSelected(CSIndex(optionIndex), true, false)
            sortNode:OnSortChanged()
        end
    end
    if sortNode then
        sortNode:UpdateDeviceState()
    end
end

FriendRequestCtrl.IsShareMode = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_arg ~= nil and self.m_arg.onShareClick ~= nil
end





FriendRequestCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end




HL.Commit(FriendRequestCtrl)
