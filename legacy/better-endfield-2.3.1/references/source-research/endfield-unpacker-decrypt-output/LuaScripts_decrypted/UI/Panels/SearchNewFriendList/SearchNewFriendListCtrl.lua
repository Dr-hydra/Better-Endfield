local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SearchNewFriendList

SearchNewFriendListCtrl = HL.Class('SearchNewFriendListCtrl', uiCtrl.UICtrl)

SearchNewFriendListCtrl.m_searchList = HL.Field(HL.Table)

SearchNewFriendListCtrl.m_searchKey = HL.Field(HL.String) << ""

SearchNewFriendListCtrl.m_recoverState = HL.Field(HL.Table)





SearchNewFriendListCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SEARCH_FRIEND_END] = 'OnSearchFriendEnd',
    [MessageConst.ON_FRIEND_CELL_INFO_CHANGE] = 'OnCellChange',
    [MessageConst.ON_CHANGE_INPUT_DEVICE_TYPE_FINISHED] = '_OnChangeInputDeviceTypeFinished',
}


SearchNewFriendListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local panelArg = type(arg) == "table" and arg or nil
    self.m_recoverState = panelArg and panelArg.searchNewFriendListState or nil

    local initArg = FriendUtils.FRIEND_CELL_INIT_CONFIG.NewFriendSearch;
    initArg.onSearchChange = function(str)
        self.view.pasteBtn.gameObject:SetActive(string.isEmpty(str))
    end
    self.view.friendList:InitFriendListCtrl(initArg)

    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()

        Notify(MessageConst.ON_FRIEND_NEW_FRIEND_SEARCH_CONTENT_CHANGE, self.view.inputField.text)
    end)
    self.view.pasteBtn.onClick:RemoveAllListeners()
    self.view.pasteBtn.onClick:AddListener(function()
        if Unity.GUIUtility.systemCopyBuffer:match("^%d+$") ~= nil then
            self.view.inputField.text = Unity.GUIUtility.systemCopyBuffer
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TOAST_NOT_UID)
        end
    end)

    UIUtils.initSearchInput(self.view.inputField, {
        clearBtn = self.view.clearBtn,
        searchBtn = self.view.searchBtn,
        onInputValueChanged = function(str)
            self.view.friendList:OnChangeInputField(str)
        end,
        onInputSubmit = function()
            self:_OnSearch()
            self:_EndInput()
        end,
        onInputFocused = function()
            self:_StartInput()
        end,
        onInputEndEdit = function()
            self:_EndInput()
        end,
        onClearClick = function()
            self.view.inputField.text = ""
        end,
        onSearchClick = function()
            self:_OnSearch()
        end,
    })

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self:BindInputPlayerAction("common_back", function()
        if self.view.inputField.isFocused then
            self:_EndInput()
        else
            self:PlayAnimationOutAndClose()

            Notify(MessageConst.ON_FRIEND_NEW_FRIEND_SEARCH_CONTENT_CHANGE, self.view.inputField.text)
        end
    end, self.view.textInputBindingGroup.groupId)


    self:_Loading()
    self.m_searchKey = panelArg and panelArg.searchKey or arg
    self.view.inputField.text = self.m_searchKey
    GameInstance.player.friendSystem:SearchNewFriend(self.m_searchKey)
    self:_ApplyRecoverState()
end

SearchNewFriendListCtrl._Loading = HL.Method() << function(self)
    self.m_searchList = {}
    self.view.text.text = string.format(Language.LUA_FRIEND_SEARCH_COUNT, self.m_searchKey, #self.m_searchList)
    self.view.friendList:RefreshInfo(self.m_searchList, true, Language.LUA_FRIEND_SEARCHING, true)
end

SearchNewFriendListCtrl._OnSearch = HL.Method() << function(self)
    self.m_searchKey = self.view.inputField.text
    GameInstance.player.friendSystem:SearchNewFriend(self.m_searchKey)
end

SearchNewFriendListCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    local arg = {
        searchKey = self.m_searchKey,
        searchNewFriendListState = {
            inputText = self.view and self.view.inputField and self.view.inputField.text or "",
            isInputFocused = self.view and self.view.inputField and self.view.inputField.isFocused or false,
        }
    }
    if sortNode then
        arg.searchNewFriendListState.sortState = {
            selectedIndex = sortNode:GetCurSelectedIndex(),
            isIncremental = sortNode.isIncremental == true,
        }
    end
    return arg
end

SearchNewFriendListCtrl._StartInput = HL.Method() << function(self)
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
        panelId = PANEL_ID,
        isGroup = true,
        id = self.view.textInputBindingGroup.groupId,
        hintPlaceholder = self.view.controllerHintPlaceholder,
        rectTransform = self.view.textInputBindingGroup.transform,
    })
end

SearchNewFriendListCtrl._EndInput = HL.Method() << function(self)
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.textInputBindingGroup.groupId)
    self.view.inputField:DeactivateInputField(true)
    self.view.friendList:NaviToFirstCell()
end

SearchNewFriendListCtrl.OnSearchFriendEnd = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh()
end

SearchNewFriendListCtrl.OnCellChange = HL.Method() << function(self)
    self:_UpdateCache()
    self.view.friendList:RefreshInfoStayPos(self.m_searchList)
    self.view.friendList:OnChangeInputField(self.view.inputField.text)
end

SearchNewFriendListCtrl._UpdateCache = HL.Method() << function(self)
    self.m_searchList = {}

    local friendSystem = GameInstance.player.friendSystem
    local index = 1
    for _, friendInfo in cs_pairs(friendSystem.newFriendSearchListInfoDic) do
        self.m_searchList[index] = FriendUtils.friendInfo2SortInfo(friendInfo, self.view.friendList.SearchSort)
        index = index + 1
    end
end

SearchNewFriendListCtrl._Refresh = HL.Method() << function(self)
    self.view.text.text = string.format(Language.LUA_FRIEND_SEARCH_COUNT, self.m_searchKey, #self.m_searchList)
    self.view.friendList:RefreshInfo(self.m_searchList, true, Language.LUA_FRIEND_NO_SEARCH_FRIEND)
    self.view.friendList:OnChangeInputField(self.view.inputField.text)
end

SearchNewFriendListCtrl._ApplyRecoverState = HL.Method() << function(self)
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
    self:_RestoreSearchInput(self.m_recoverState)
end

SearchNewFriendListCtrl._RestoreSearchInput = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local inputText = recoverState.inputText or ""
    local needFocus = recoverState.isInputFocused == true
    self.view.inputField.text = inputText
    if not string.isEmpty(inputText) then
        self.view.friendList:OnChangeInputField(inputText)
    end
    if needFocus then
        self.view.inputField:Select()
        self.view.inputField:ActivateInputField()
    end
end

SearchNewFriendListCtrl._OnChangeInputDeviceTypeFinished = HL.Method(HL.Table) << function(self, args)
    if not self:IsShow() then
        return
    end
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    if sortNode then
        sortNode:UpdateDeviceState()
    end
end

SearchNewFriendListCtrl.OnShow = HL.Override() << function(self)
    local recoverState = self.m_recoverState
    if recoverState ~= nil then
        self:_StartCoroutine(function()
            coroutine.step()
            if IsNull(self.view.gameObject) then
                return
            end
            self:_RestoreSearchInput(recoverState)
        end)
    end
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    InputManagerInst:ToggleGroup(self.view.textInputBindingGroup.groupId, true)
end

SearchNewFriendListCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(SearchNewFriendListCtrl)
