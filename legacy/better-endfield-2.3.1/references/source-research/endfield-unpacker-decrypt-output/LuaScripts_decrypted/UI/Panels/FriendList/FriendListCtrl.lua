local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendList

FriendListCtrl = HL.Class('FriendListCtrl', uiCtrl.UICtrl)

FriendListCtrl.m_friendList = HL.Field(HL.Table)

FriendListCtrl.friendSystem = HL.Field(CS.Beyond.Gameplay.FriendSystem)

FriendListCtrl.m_isPsnFriend = HL.Field(HL.Boolean) << false

FriendListCtrl.m_isVisit = HL.Field(HL.Boolean) << false

FriendListCtrl.m_needRefresh = HL.Field(HL.Boolean) << false

FriendListCtrl.m_isInputFieldExpended = HL.Field(HL.Boolean) << false

FriendListCtrl.m_skipClearInputOnShow = HL.Field(HL.Boolean) << false

FriendListCtrl.m_pendingRecoverState = HL.Field(HL.Table)

FriendListCtrl.m_forceRefreshWhenInputBlocked = HL.Field(HL.Boolean) << false





FriendListCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_INFO_SYNC] = 'OnSync',
    [MessageConst.ON_FRIEND_INFO_PSN_SYNC] = 'OnPSNSync',
    [MessageConst.ON_FRIEND_CELL_INFO_CHANGE] = 'OnCellChange',
    [MessageConst.ON_FRIEND_ADD_NOTIFY] = 'OnFriendAddNotify',
    [MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE] = '_RefreshVisitCount',
    [MessageConst.ON_CHANGE_INPUT_DEVICE_TYPE_FINISHED] = '_OnChangeInputDeviceTypeFinished',

}

FriendListCtrl.OpenVisitFriendList = HL.StaticMethod() << function()

    local friendInfo = GameInstance.player.spaceship:GetFriendRoleInfo()










    PhaseManager:GoToPhase(PhaseId.Friend, {
        panelId = PANEL_ID,
        needClose = true,
        needTab = false,
        stateName = "SpaceShip",
        title = Language.LUA_CREATE_VISIT_FRIEND_PANEL_TITLE,
    })
end

FriendListCtrl.OnPSNError = HL.Method() << function(self)
    if self.m_isPsnFriend then
        self.friendSystem:SyncPsnFriendListSimple()
    end
end

FriendListCtrl.OnSync = HL.Method() << function(self)
    if self.m_isPsnFriend then
       return
    end
    if not self.view.gameObject.activeInHierarchy then
        self.m_needRefresh = true
        return
    end
    if self.view.inputGroup.groupEnabled == false and not GameInstance.player.guide.isInForceGuide then
        if self.m_forceRefreshWhenInputBlocked then
            self.m_forceRefreshWhenInputBlocked = false
            self:_UpdateCache()
            self:_Refresh(false)
            return
        end



        self.m_needRefresh = true
        return
    end
    self.m_forceRefreshWhenInputBlocked = false
    self:_UpdateCache()
    self:_Refresh(false)
end

FriendListCtrl.OnPSNSync = HL.Method() << function(self)
    if self.m_isPsnFriend == false then
       return
    end
    if not self.view.gameObject.activeInHierarchy then
        self.m_needRefresh = true
        return
    end
    if self.view.inputGroup.groupEnabled == false and not GameInstance.player.guide.isInForceGuide then

        self.m_needRefresh = true
        return
    end
    self.m_forceRefreshWhenInputBlocked = false
    self:_UpdateCache()
    self:_Refresh(false)
end

FriendListCtrl.OnCellChange = HL.Method() << function(self)
    if not self.view.gameObject.activeInHierarchy then
        self.m_needRefresh = true
        return
    end
    if self.view.inputGroup.groupEnabled == false and not GameInstance.player.guide.isInForceGuide then
        if self.m_forceRefreshWhenInputBlocked then
            self.m_forceRefreshWhenInputBlocked = false
            self:_UpdateCache()
            self:_Refresh(false)
            return
        end

        self.m_needRefresh = true
        return
    end
    self.m_forceRefreshWhenInputBlocked = false
    self:_UpdateCache()
    local inputText = self.view.inputField.text
    if string.isEmpty(inputText) then
        self:_Refresh(false, true)
        return
    end

    self:_RefreshListHeader()
    self.view.friendList:OnChangeInputField(inputText)
end


FriendListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.friendTipBtn.onClick:RemoveAllListeners()
    self.view.friendTipBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "friend")
    end)

    self.view.blackListBtn.onClick:RemoveAllListeners()
    self.view.blackListBtn.onClick:AddListener(function()
        self:_OnBlackListBtnClick()
    end)
    self.view.friendRequestBtn.onClick:RemoveAllListeners()
    self.view.friendRequestBtn.onClick:AddListener(function()
        self:_OnFriendRequestBtnClick()
    end)

    self.view.endFriendShipBtn.onClick:RemoveAllListeners()
    self.view.endFriendShipBtn.onClick:AddListener(function()
        self:_ConfirmLeaveVisitSpaceShip()
    end)
    self.view.returnBtn.onClick:AddListener(function()
        self:_ConfirmLeaveVisitSpaceShip(true)
    end)

    UIUtils.initSearchInput(self.view.friendList.view.inputField, {
        clearBtn = self.view.clearBtn,
        onInputFocused = function()
            self:_RefreshInputFieldVisualState(true)
            self:_StartInput()
            self.view.clearBtn.gameObject:SetActiveIfNecessary(not (string.isEmpty(self.view.inputField.text)))
            self.view.searchResult.gameObject:SetActiveIfNecessary(not (string.isEmpty(self.view.inputField.text)))
        end,
        onInputEndEdit = function()
            self:_RefreshInputFieldVisualState(false)
            self:_EndInput()
        end,
        onClearClick = function()
            self:_ClearInput()
        end,
    })

    self.view.clearBtn.gameObject:SetActiveIfNecessary(false)
    self.view.searchResult.gameObject:SetActiveIfNecessary(false)

    if arg == nil then
        arg = {}
    end
    arg.needSync = false
    self:OnPhaseRefresh(arg)
    if (arg == nil or (arg.stateName == nil or arg.stateName == "Normal")) and not self.m_isVisit then
        CS.Beyond.Gameplay.Conditions.OnNormalFriendPanelOpen.Trigger()
    end

    self.view.redDot:InitRedDot("NewFriendRequest")
    GameInstance.player.spaceship:GetClueInfo()
    self:Loading()
end

FriendListCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, arg)
    self.friendSystem = GameInstance.player.friendSystem
    self.m_isPsnFriend = false
    self.view.friendList.m_isPsnTab = false
    self.m_pendingRecoverState = arg and arg.friendListState or nil
    self.m_skipClearInputOnShow = self.m_pendingRecoverState ~= nil
    self.m_forceRefreshWhenInputBlocked = self.m_pendingRecoverState ~= nil
    if self.m_isPsnFriend then
        self.friendSystem:SyncPsnFriendListSimple()
    else
        self.friendSystem:SyncFriendSimpleInfo()
    end

    local initArg = lume.deepCopy(FriendUtils.FRIEND_CELL_INIT_CONFIG.Friend)
    initArg.onSearchChange = function(str)
        self.view.clearBtn.gameObject:SetActiveIfNecessary(not (string.isEmpty(str)))
        self.view.searchResult.gameObject:SetActiveIfNecessary(not (string.isEmpty(str)))
    end
    initArg.isFilter = true

    if arg ~= nil and arg.cellStateName ~= nil then
        initArg.stateName = arg.cellStateName
    end

    if arg ~= nil and arg.stateName ~= nil then
        self.view.uiState:SetState(arg.stateName)
    else
        self.view.uiState:SetState("Normal")
    end
    self.m_isVisit = (arg ~= nil and arg.stateName == "SpaceShip")
    if (arg == nil or (arg.stateName == nil or arg.stateName == "Normal")) and not self.m_isVisit then
        initArg.onSonyTabChange = function(isPsnFriend)
            self:Loading()
            self.m_isPsnFriend = isPsnFriend
            if self.m_isPsnFriend then
                self.friendSystem:SyncPsnFriendListSimple()
            else
                self.friendSystem:SyncFriendSimpleInfo()
            end
        end
    end

    self.view.friendList:InitFriendListCtrl(initArg)
    self:_ApplyRecoverState(self.m_pendingRecoverState)

    self.view.btnClose.gameObject:SetActive(false)
    if arg ~= nil and  arg.needClose then
        self.view.btnClose.gameObject:SetActive(true)
        self.view.btnClose.onClick:AddListener(function()
            PhaseManager:PopPhase(PhaseId.Friend)
        end)
    end

    self.view.titleTxt.gameObject:SetActive(false)
    if arg ~= nil and arg.title then
        self.view.titleTxt.gameObject:SetActive(true)
        self.view.titleTxt.text = arg.title
    end

    if arg ~= nil and arg.needSync ~= false then
        self:OnSync()
    end

    self.view.endFriendShipBtn.gameObject:SetActive(GameInstance.player.spaceship.isViewingFriend)
    self.view.returnBtn.gameObject:SetActive(GameInstance.player.spaceship.isViewingFriend)

    self:Loading()
end

FriendListCtrl._StartInput = HL.Method() << function(self)
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
    self.m_phase:SetTabBlockState(true)
end

FriendListCtrl._EndInput = HL.Method() << function(self)
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.textInputBindingGroup.groupId)
    self.view.inputField:DeactivateInputField(true)
    self.view.friendList:NaviToFirstCell()
    self.m_phase:SetTabBlockState(false)
end

FriendListCtrl._ClearInput = HL.Method() << function(self)
    self.view.inputField.text = ""
end

FriendListCtrl._RefreshInputFieldVisualState = HL.Method(HL.Boolean) << function(self, preferFocus)
    local hasText = not string.isEmpty(self.view.inputField.text)
    local shouldExpand = preferFocus or hasText
    if shouldExpand then
        if self.m_isInputFieldExpended == false then
            self.view.inputNode:Play("friendblacklistipput_in")
        end
        self.view.friendList.view.inputField.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_FOCUS_WIDTH, self.view.friendList.view.inputField.transform.sizeDelta.y)
        self.view.inputBgImage.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_BG_FOCUS_WIDTH, self.view.inputBgImage.transform.sizeDelta.y)
        self.view.clearBtn.transform.anchoredPosition = Vector2(self.view.config.CLEAR_BTN_FOCUS_POS, self.view.clearBtn.transform.localPosition.y)
    else
        if self.m_isInputFieldExpended then
            self.view.inputNode:Play("friendblacklistipput_out")
        end
        self.view.friendList.view.inputField.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_WIDTH, self.view.friendList.view.inputField.transform.sizeDelta.y)
        self.view.inputBgImage.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_BG_WIDTH, self.view.inputBgImage.transform.sizeDelta.y)
        self.view.clearBtn.transform.anchoredPosition = Vector2(self.view.config.CLEAR_BTN_POS, self.view.clearBtn.transform.localPosition.y)
    end
    self.view.clearBtn.gameObject:SetActiveIfNecessary(hasText)
    self.view.searchResult.gameObject:SetActiveIfNecessary(hasText)
    self.m_isInputFieldExpended = shouldExpand
end

FriendListCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_phase and self.m_phase.arg and lume.deepCopy(self.m_phase.arg) or {}
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    local friendListState = {
        inputText = self.view and self.view.inputField and self.view.inputField.text or "",
        isInputFocused = self.view and self.view.inputField and self.view.inputField.isFocused or false,
    }
    if sortNode then
        friendListState.sortState = {
            selectedIndex = sortNode:GetCurSelectedIndex(),
            isIncremental = sortNode.isIncremental,
        }
    end
    arg.friendListState = friendListState
    arg.phase = nil
    return arg
end

FriendListCtrl.Loading = HL.Method() << function(self)
    self.m_friendList = {}
    self:_Refresh(true)
end

FriendListCtrl._UpdateCache = HL.Method() << function(self)
    self.m_friendList = {}
    local index = 1
    local infoDict = self.m_isPsnFriend and self.friendSystem.psnFriendList or self.friendSystem.friendInfoDic
    local findCurrentVisitFriendInfo = false
    for _, friendInfo in cs_pairs(infoDict) do
        if not self.m_isVisit or friendInfo.guestRoomUnlock == true then
            self.m_friendList[index] = FriendUtils.friendInfo2SortInfo(friendInfo, self.view.friendList.SearchSort)
            index = index + 1
        end
        if self.m_isVisit and GameInstance.player.spaceship.isViewingFriend and friendInfo.roleId == GameInstance.player.spaceship:GetFriendRoleInfo().roleId then
            findCurrentVisitFriendInfo = true
        end
    end

    if self.m_isVisit and GameInstance.player.spaceship.isViewingFriend and not findCurrentVisitFriendInfo then
        local friendInfo = GameInstance.player.spaceship:GetFriendRoleInfo()
        if friendInfo ~= nil then
            self.m_friendList[index] = {
                roleId = friendInfo.roleId,
                name = "",
                lastDateTime = 0,

                addFriendTime = 0,
                adventureLevel = 0,
                roleType = CS.Beyond.Gameplay.RoleType.Unknown:GetHashCode(),
                searchSort = self.view.friendList.SearchSort,
                accountId = "",
                helpFlag = 0,
                isCurrentShip = 1,
            }
        end
    end
end

FriendListCtrl._ConfirmLeaveVisitSpaceShip = HL.Method(HL.Opt(HL.Boolean)) << function(self, backToSelf)
    local friendInfo = GameInstance.player.spaceship:GetFriendRoleInfo()
    GameInstance.player.friendSystem:SyncFriendInfoById(friendInfo.roleId, function()
        Notify(MessageConst.SHOW_POP_UP, {
            content = backToSelf and Language.LUA_SPACESHIP_VISIT_LEAVE_RETURN_FRIEND_TIPS or Language.LUA_SPACESHIP_VISIT_LEAVE_FRIEND_TIPS,
            onConfirm = function()
                GameInstance.player.spaceship:LeaveVisitSpaceShip(backToSelf)
            end})
    end)
end

FriendListCtrl._OnBlackListBtnClick = HL.Method() << function(self)
    UIManager:AutoOpen(PanelId.FriendBlackList)
end

FriendListCtrl._OnFriendRequestBtnClick = HL.Method() << function(self)
    UIManager:AutoOpen(PanelId.FriendRequest)
end


FriendListCtrl._RefreshVisitCount = HL.Method() << function(self)
    self.view.clueCountTxt.gameObject:SetActive(GameInstance.player.spaceship:GetClueData(CS.Beyond.Gameplay.SpaceshipEnums.SpaceshipDataType.Self) ~= nil)
    self.view.clueCountTxt.text = string.format("%d/%d", self.friendSystem.currentClueShareCount, self.friendSystem.maxClueShareCount)
    self.view.visitorHelpTxt.text = string.format("%d/%d", self.friendSystem.currentShipVisitorHelpCount, self.friendSystem.maxShipVisitorHelpCount)
end

FriendListCtrl._RefreshListHeader = HL.Method() << function(self)
    if self.m_isVisit or self.m_isPsnFriend then
        self.view.friendCountTxt.text = string.format("%d", #self.m_friendList)
    else
        self.view.friendCountTxt.text = string.format("%d/%d", self.friendSystem.currentFriendCount, Tables.globalConst.friendListLenMax)
    end

    self:_RefreshVisitCount()

    local hasValue, _ = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.guestRoomId)
    self.view.shipRoot.gameObject:SetActiveIfNecessary(hasValue)
end

FriendListCtrl._Refresh = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, loading, stayPos)
    self:_RefreshListHeader()

    if stayPos then
        self.view.friendList:RefreshInfoStayPos(self.m_friendList)
    else
        self.view.friendList:RefreshInfo(self.m_friendList, true, nil, loading)
    end


    local inputText = self.view.inputField.text
    if not FriendUtils.isPsnPlatform() and not string.isEmpty(inputText) then
        self.view.friendList:OnChangeInputField(inputText)
    end
end

FriendListCtrl._OnChangeInputDeviceTypeFinished = HL.Method(HL.Table) << function(self, args)
    if not self:IsShow() then
        return
    end
    local inputType = args and args.inputType or nil
    local sortNode = self.view and self.view.friendList and self.view.friendList.view and self.view.friendList.view.sortNode
    if sortNode then
        sortNode:UpdateDeviceState()
    end
    local preferFocus = self.view.inputField.isFocused and inputType ~= DeviceInfo.InputType.Controller
    self:_RefreshInputFieldVisualState(preferFocus)
end

FriendListCtrl._ApplyRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
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
end

FriendListCtrl._RestoreSearchInput = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local inputText = recoverState.inputText or ""
    local needFocus = recoverState.isInputFocused == true
    self.view.inputField.text = inputText
    self:_RefreshInputFieldVisualState(needFocus)
    if needFocus then
        self.view.inputField:Select()
        self.view.inputField:ActivateInputField()
    end
end

FriendListCtrl.TryRefresh = HL.Method() << function(self)
    if self.view.gameObject.activeInHierarchy and self.m_needRefresh then
        self.m_needRefresh = false
        self.m_forceRefreshWhenInputBlocked = false
        self:_UpdateCache()
        self:_Refresh(false)
    end
end

FriendListCtrl.OnFriendAddNotify = HL.Method() << function(self)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_NEW_FRIEND_ADD_TOAST)
end

FriendListCtrl.OnShow = HL.Override() << function(self)
    self.view.listFullPrompt.gameObject:SetActiveIfNecessary(self.friendSystem.isReadFullFriendRequestInfo == false and self.friendSystem.currentRequestFriendCount >= Tables.globalConst.friendRequestListLenMax)
    local recoverState = self.m_pendingRecoverState
    if self.m_skipClearInputOnShow then
        self.m_skipClearInputOnShow = false
        self:_StartCoroutine(function()
            coroutine.step()
            if IsNull(self.view.gameObject) then
                return
            end
            self:_RestoreSearchInput(recoverState)
            self.m_pendingRecoverState = nil
        end)
    else
        self:_ClearInput()
        self:_RefreshInputFieldVisualState(false)
    end
    self.friendSystem.isReadFullFriendRequestInfo = true

    self:TryRefresh()
end
FriendListCtrl.OnHide = HL.Override() << function(self)
end
FriendListCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(FriendListCtrl)
