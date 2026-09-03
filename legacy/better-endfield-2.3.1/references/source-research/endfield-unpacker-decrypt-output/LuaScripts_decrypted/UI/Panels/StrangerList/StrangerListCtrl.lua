local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.StrangerList

StrangerListCtrl = HL.Class('StrangerListCtrl', uiCtrl.UICtrl)






StrangerListCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_STRANGER_LIST_INFO_SYNC] = 'OnSync',
    [MessageConst.ON_FRIEND_CELL_INFO_CHANGE] = 'OnCellChange',
    [MessageConst.ON_FRIEND_NEW_FRIEND_SEARCH_CONTENT_CHANGE] = 'OnSearchChange',
    [MessageConst.ON_CHANGE_INPUT_DEVICE_TYPE_FINISHED] = '_OnChangeInputDeviceTypeFinished',
}

StrangerListCtrl.m_strangerList = HL.Field(HL.Table)

StrangerListCtrl.m_recoverState = HL.Field(HL.Table)

StrangerListCtrl.m_switchCooldownDeadline = HL.Field(HL.Number) << 0


StrangerListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_recoverState = arg and arg.strangerListState or nil

    local initArg = FriendUtils.FRIEND_CELL_INIT_CONFIG.Stranger;
    initArg.onSearchChange = function(str)
        self.view.pasteBtn.gameObject:SetActive(string.isEmpty(str))
    end
    self.view.friendList:InitFriendListCtrl(initArg)

    if not PhaseManager.isRecovering then
        GameInstance.player.friendSystem.strangerLisListInfoDic:Clear()
        GameInstance.player.friendSystem:SwitchNewStranger()
    end
    self:_UpdateCache()

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
            if string.isEmpty(self.view.inputField.text) then
                Notify(MessageConst.SHOW_TOAST, Language.CS_FRIEND_SEARCH_KEY_EMPTY)
                return
            end
            UIManager:AutoOpen(PanelId.SearchNewFriendList, self.view.inputField.text)
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
            if string.isEmpty(self.view.inputField.text) then
                Notify(MessageConst.SHOW_TOAST, Language.CS_FRIEND_SEARCH_KEY_EMPTY)
                return
            end
            UIManager:AutoOpen(PanelId.SearchNewFriendList, self.view.inputField.text)
        end,
    })

    self.view.countDownText.gameObject:SetActive(false)
    self.view.switchBtn.onClick:RemoveAllListeners()
    self.view.switchBtn.onClick:AddListener(function()
        GameInstance.player.friendSystem:SwitchNewStranger()
        self:_StartSwitchCooldown()
    end)
    if not PhaseManager.isRecovering then
        self:_StartSwitchCooldown()
    end
    self:_Refresh(true)
    self:_ApplyRecoverState(self.m_recoverState)
end

StrangerListCtrl._StartInput = HL.Method() << function(self)
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
        panelId = PANEL_ID,
        isGroup = true,
        id = self.view.textInputBindingGroup.groupId,
        hintPlaceholder = self.view.controllerHintPlaceholder,
        rectTransform = self.view.whiteSelectNode,
    })
    self.m_phase:SetTabBlockState(true)
end

StrangerListCtrl._EndInput = HL.Method() << function(self)
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.textInputBindingGroup.groupId)
    self.view.inputField:DeactivateInputField(true)
    self.view.friendList:NaviToFirstCell()
    self.m_phase:SetTabBlockState(false)
end

StrangerListCtrl._UpdateCache = HL.Method() << function(self)
    self.m_strangerList = {}

    local friendSystem = GameInstance.player.friendSystem
    local index = 1
    for _, friendInfo in cs_pairs(friendSystem.strangerLisListInfoDic) do
        self.m_strangerList[index] = FriendUtils.friendInfo2SortInfo(friendInfo, self.view.friendList.SearchSort)
        index = index + 1
    end
end

StrangerListCtrl._Refresh = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, loading, stayPos)
    loading = loading or false

    if stayPos then
        self.view.friendList:RefreshInfoStayPos(self.m_strangerList)
    else
        self.view.friendList:RefreshInfo(self.m_strangerList, false, Language.LUA_ADD_FRIEND_EMPTY_TIP, loading)
    end
    self.view.friendList:OnChangeInputField(self.view.inputField.text)
end

StrangerListCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_phase and self.m_phase.arg and lume.deepCopy(self.m_phase.arg) or {}
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    local strangerListState = {
        inputText = self.view and self.view.inputField and self.view.inputField.text or "",
        isInputFocused = self.view and self.view.inputField and self.view.inputField.isFocused or false,
    }
    if sortNode then
        strangerListState.sortState = {
            selectedIndex = sortNode:GetCurSelectedIndex(),
            isIncremental = sortNode.isIncremental == true,
        }
    end
    if self.m_switchCooldownDeadline and self.m_switchCooldownDeadline > 0 then
        strangerListState.switchCooldownDeadline = self.m_switchCooldownDeadline
    end
    arg.strangerListState = strangerListState
    arg.phase = nil
    return arg
end

StrangerListCtrl._ApplyRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local sortState = recoverState.sortState
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
    self:_RestoreSwitchCooldown(recoverState)
    self:_RestoreSearchInput(recoverState)
end

StrangerListCtrl._StartSwitchCooldown = HL.Method(HL.Opt(HL.Number)) << function(self, deadline)
    deadline = deadline or (DateTimeUtils.GetCurrentTimestampBySeconds() + 10)
    self.m_switchCooldownDeadline = deadline
    self.view.switchBtn.gameObject:SetActive(false)
    self.view.switchTimeBtn.gameObject:SetActive(true)
    self.view.countDownText.gameObject:SetActive(true)
    self.view.rootUIState:SetState('ActiveState')
    self.view.countDownText:InitCountDownText(deadline, function()
        self:_EndSwitchCooldown()
    end, UIUtils.getSecondsLeftTime)
end

StrangerListCtrl._EndSwitchCooldown = HL.Method() << function(self)
    self.m_switchCooldownDeadline = 0
    self.view.switchBtn.gameObject:SetActive(true)
    self.view.switchTimeBtn.gameObject:SetActive(false)
    self.view.countDownText.gameObject:SetActive(false)
    self.view.rootUIState:SetState('NormalState')
end

StrangerListCtrl._RestoreSwitchCooldown = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local deadline = recoverState.switchCooldownDeadline
    if deadline and deadline > DateTimeUtils.GetCurrentTimestampBySeconds() then
        self:_StartSwitchCooldown(deadline)
    else
        self:_EndSwitchCooldown()
    end
end

StrangerListCtrl._RestoreSearchInput = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
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

StrangerListCtrl.OnCellChange = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh(false, true)
end

StrangerListCtrl.OnSync = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh(false, false)
end

StrangerListCtrl.OnSearchChange = HL.Method(HL.String) << function(self, str)
    self.view.inputField.text = str
end

StrangerListCtrl.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, args)
    self.m_recoverState = args and args.strangerListState or nil
    self:_UpdateCache()
    self:_Refresh()
    self:_ApplyRecoverState(self.m_recoverState)
end

StrangerListCtrl._OnChangeInputDeviceTypeFinished = HL.Method(HL.Table) << function(self, args)
    if not self:IsShow() then
        return
    end
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    if sortNode then
        sortNode:UpdateDeviceState()
    end
end



StrangerListCtrl.OnShow = HL.Override() << function(self)
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

StrangerListCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end




HL.Commit(StrangerListCtrl)
