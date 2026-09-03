
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendBlackList

FriendBlackListCtrl = HL.Class('FriendBlackListCtrl', uiCtrl.UICtrl)

FriendBlackListCtrl.m_friendList = HL.Field(HL.Table)

FriendBlackListCtrl.m_isPsnFriend = HL.Field(HL.Boolean) << false

FriendBlackListCtrl.m_recoverState = HL.Field(HL.Table)





FriendBlackListCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BLACK_LIST_INFO_SYNC] = 'OnSync',
    [MessageConst.ON_FRIEND_CELL_INFO_CHANGE] = 'OnCellChange',
}


FriendBlackListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_recoverState = arg and arg.blackListState or nil
    self.m_isPsnFriend = self.m_recoverState and self.m_recoverState.isPsnFriend == true or false
    self.view.friendList.m_isPsnTab = self.m_isPsnFriend
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.bgBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.m_friendList = {}
    GameInstance.player.friendSystem:SyncBlackList()
    local initArg = lume.deepCopy(FriendUtils.FRIEND_CELL_INIT_CONFIG.Black)
    initArg.onSonyTabChange = function(isPsnFriend)

        self.m_isPsnFriend = isPsnFriend
        if self.m_isPsnFriend then
            self:_UpdateCache()
            self:_Refresh()
        else
            GameInstance.player.friendSystem:SyncBlackList()
            self.m_friendList = {}
            self:_Refresh(false, true)
        end
    end
    self.view.friendList:InitFriendListCtrl(initArg)
    self:_ApplyRecoverState()
end

FriendBlackListCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {}
    arg.blackListState = {
        isPsnFriend = self.m_isPsnFriend == true,
    }
    return arg
end

FriendBlackListCtrl.OnSync = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh()
end

FriendBlackListCtrl.OnCellChange = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh(true)
end

FriendBlackListCtrl._UpdateCache = HL.Method() << function(self)
    self.m_friendList = {}
    local friendSystem = GameInstance.player.friendSystem
    local index = 1
    local infoDict = self.m_isPsnFriend and friendSystem.psnBlackListFriendList or friendSystem.blackListInfoDic
    for _, friendInfo in cs_pairs(infoDict) do
        self.m_friendList[index] = FriendUtils.friendInfo2SortInfo(friendInfo, self.view.friendList.SearchSort)
        index = index + 1
    end
end

FriendBlackListCtrl._Refresh = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, stayTop, loading)
    local friendSystem = GameInstance.player.friendSystem

    local maxBlackListCount = Tables.globalConst.friendBlackListMaxLen
    if self.m_isPsnFriend then
        self.view.countText.text =  string.format("%d", #self.m_friendList)
    else
        self.view.countText.text =  string.format("%d/%d", friendSystem.currentBlackListCount, maxBlackListCount)
    end

    loading = loading or false

    if stayTop == true then
        self.view.friendList:RefreshInfoStayPos(self.m_friendList)
    else
        self.view.friendList:RefreshInfo(self.m_friendList,false, Language.LUA_BLACK_LIST_EMPTY_TIP, loading)
    end
end

FriendBlackListCtrl._ApplyRecoverState = HL.Method() << function(self)
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    if sortNode then
        sortNode:UpdateDeviceState()
    end
end





FriendBlackListCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end




HL.Commit(FriendBlackListCtrl)
