local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipRoomClueGift
local PHASE_ID = PhaseId.SpaceshipRoomClueGift
SpaceshipRoomClueGiftCtrl = HL.Class('SpaceshipRoomClueGiftCtrl', uiCtrl.UICtrl)






SpaceshipRoomClueGiftCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_INFO_SYNC] = 'OnSync',
    [MessageConst.ON_FRIEND_CELL_INFO_CHANGE] = 'OnCellChange',
    [MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE] = 'OnSpaceshipClueInfoChange',
    [MessageConst.ON_SPACESHIP_PRESENT_FRIEND_CLUE] = 'OnSpaceshipPresentFriendClue',
    [MessageConst.ON_SPACESHIP_GUEST_ROOM_CLUE_REWARD_ITEM] = 'OnClueRewardItem',
}

SpaceshipRoomClueGiftCtrl.m_getClueCell = HL.Field(HL.Function)

SpaceshipRoomClueGiftCtrl.friendArg = HL.Field(HL.Table)

SpaceshipRoomClueGiftCtrl.m_friendList = HL.Field(HL.Table)

SpaceshipRoomClueGiftCtrl.m_isInitFriend = HL.Field(HL.Boolean) << false

SpaceshipRoomClueGiftCtrl.m_selectedClueId = HL.Field(HL.Number) << -1

SpaceshipRoomClueGiftCtrl.m_luaIndex2ClueId = HL.Field(HL.Table)

SpaceshipRoomClueGiftCtrl.m_clueId2ClueCell = HL.Field(HL.Table)

SpaceshipRoomClueGiftCtrl.m_clueId2HaveClues = HL.Field(HL.Table)

SpaceshipRoomClueGiftCtrl.m_initController = HL.Field(HL.Boolean) << false

SpaceshipRoomClueGiftCtrl.m_lastSendInfo = HL.Field(HL.Table)

SpaceshipRoomClueGiftCtrl.friendListJumpIn = HL.Field(HL.Number) << 0

SpaceshipRoomClueGiftCtrl.friendListArrowJumpIn = HL.Field(HL.Number) << 0

SpaceshipRoomClueGiftCtrl.m_inFriendListGroup = HL.Field(HL.Boolean) << false

SpaceshipRoomClueGiftCtrl.m_pendingInputText = HL.Field(HL.String) << ""


SpaceshipRoomClueGiftCtrl.ShowSpaceshipClueGift = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    PhaseManager:OpenPhase(PHASE_ID)
end



SpaceshipRoomClueGiftCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.awardNumTxt01.text = Tables.SpaceshipConst.presentClueCreditBaseRewardCount
    self.view.awardNumTxt02.text = Tables.SpaceshipConst.presentClueCreditBaseRewardCount + Tables.SpaceshipConst.presentUnownedClueCreditExtraRewardCount

    self.view.commonTopTitlePanel.btnBack.onClick:RemoveAllListeners()
    self.view.commonTopTitlePanel.btnBack.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    SpaceshipUtils.InitMoneyLimitCell(self.view.commonTopTitlePanel.moneyCell, Tables.spaceshipConst.creditItemId)

    self.m_getClueCell = UIUtils.genCachedCellFunction(self.view.clueScrollList)

    self.view.clueScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_UpdateClueCell(self.m_getClueCell(obj), csIndex)
    end)

    self:_UpdateClueId2HaveClues()

    local noZeroCount = 0
    self.m_luaIndex2ClueId = {}

    for i = 1, Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount do
        local num = 0
        if self.m_clueId2HaveClues[i] ~= nil then
            num = #self.m_clueId2HaveClues[i]
        end
        if num == 0 then
            table.insert(self.m_luaIndex2ClueId, i, i)
        else
            noZeroCount = noZeroCount + 1
            table.insert(self.m_luaIndex2ClueId, noZeroCount, i)
        end
    end

    local leftClueCount = Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount
    if leftClueCount > 0 then
        self.m_clueId2ClueCell = {}
        self.view.leftState:SetState("VisitorValid")
        self.view.clueScrollList:UpdateCount(leftClueCount)
        self:_selectClue(1, true)
    else
        self.view.leftState:SetState("VisitorNull")
    end

    self:_InitInputField()
    self:_InitFriendList()


    self.friendListJumpIn = self:BindInputPlayerAction("spaceship_clue_send_friend_list_focus", function()
        self:ControllerFriendListJumpIn()
    end)

    self.friendListArrowJumpIn = self:BindInputPlayerAction("friend_chat_send_area_use_arrow_jump_in", function()
        self:ControllerFriendListJumpIn()
    end)

    self.view.friendList.view.selectableNaviGroup.onIsFocusedChange:RemoveAllListeners()
    self.view.friendList.view.selectableNaviGroup.onIsFocusedChange:AddListener(function(isFocus)
        InputManagerInst:ToggleBinding(self.friendListJumpIn, not isFocus)
        InputManagerInst:ToggleBinding(self.friendListArrowJumpIn, not isFocus)
    end)

    self.view.friendList.view.selectableNaviGroup.getDefaultSelectableFunc = function()
        return self.view.friendList:GetClueGiftNaviToFirstCell()
    end

    InputManagerInst:ToggleBinding(self.friendListJumpIn, false)
    InputManagerInst:ToggleBinding(self.friendListArrowJumpIn, false)

    local recoverState = arg and arg.recoverState or nil
    if recoverState then
        self:_TryRecoverState(recoverState)
        arg.recoverState = nil
    end
end

SpaceshipRoomClueGiftCtrl.ControllerFriendListJumpIn = HL.Method() << function(self)
    if not self.m_inFriendListGroup then
        self.m_inFriendListGroup = true
        self.view.friendList.view.selectableNaviGroup:ManuallyFocus()
    end
end

SpaceshipRoomClueGiftCtrl.OnClueRewardItem = HL.Method(HL.Any) << function(self, args)
    local items, sources = unpack(args)
    SpaceshipUtils.ShowClueOutcomePopup(items, sources, self.view.commonTopTitlePanel.moneyCell, nil)
end

SpaceshipRoomClueGiftCtrl.OnSpaceshipClueInfoChange = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    self:_UpdateClueId2HaveClues()
end

SpaceshipRoomClueGiftCtrl._UpdateClueId2HaveClues = HL.Method() << function(self)
    local clueData = GameInstance.player.spaceship:GetClueData()
    self.m_clueId2HaveClues = {}
    for i = 1, 7 do
        self.m_clueId2HaveClues[i] = {}
        local use = false
        local useInstId = nil
        if clueData ~= nil then
            use, useInstId = clueData.placedClueStatus:TryGetValue(i)
        end
        local selfClues = GameInstance.player.spaceship:GetCluesByIndex(i, CS.Beyond.Gameplay.GuestRoomClueType.Self)
        if selfClues ~= nil then
            for instId, value in pairs(selfClues) do
                if not use or instId ~= useInstId then
                    table.insert(self.m_clueId2HaveClues[i], instId)
                end
            end
        end

        if self.m_clueId2ClueCell ~= nil then
            local cell = self.m_clueId2ClueCell[i]
            if cell ~= nil then
                local num = 0
                if self.m_clueId2HaveClues[i] ~= nil then
                    num = #self.m_clueId2HaveClues[i]
                end
                cell.inventoryNumTxt.text = num

                if i == self.m_selectedClueId then
                    if num > 0 then
                        cell.stateController:SetState("Select")
                    else
                        cell.stateController:SetState("Mask_select")
                    end
                else
                    if num > 0 then
                        cell.stateController:SetState("Normal")
                    else
                        cell.stateController:SetState("Mask")
                    end
                end
            end
        end
    end

    if self.m_selectedClueId ~= -1 then
        local selectedCell = self.m_clueId2ClueCell and self.m_clueId2ClueCell[self.m_selectedClueId]
        if selectedCell then
            local num = 0
            if self.m_clueId2HaveClues[self.m_selectedClueId] ~= nil then
                num = #self.m_clueId2HaveClues[self.m_selectedClueId]
            end
            selectedCell.inventoryNumTxt.text = num
            if num > 0 then
                selectedCell.stateController:SetState("Select")
            else
                selectedCell.stateController:SetState("Mask_select")
            end
        end
    end
end

SpaceshipRoomClueGiftCtrl._UpdateClueCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local luaIndex = LuaIndex(csIndex)

    local clueId = self.m_luaIndex2ClueId[luaIndex]
    self.m_clueId2ClueCell[clueId] = cell

    if clueId == 1 then
        cell.stateController:SetState("Color01")
    elseif clueId == 2 then
        cell.stateController:SetState("Color02")
    elseif clueId == 3 then
        cell.stateController:SetState("Color03")
    elseif clueId == 4 then
        cell.stateController:SetState("Color04")
    elseif clueId == 5 then
        cell.stateController:SetState("Color05")
    elseif clueId == 6 then
        cell.stateController:SetState("Color06")
    elseif clueId == 7 then
        cell.stateController:SetState("Color07")
    end

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_selectClue(luaIndex, false)
    end)

    local num = 0
    if self.m_clueId2HaveClues[clueId] ~= nil then
        num = #self.m_clueId2HaveClues[clueId]
    end

    cell.inventoryNumTxt.text = num

    if self.m_selectedClueId == clueId then
        if num > 0 then
            cell.stateController:SetState("Select")
        else
            cell.stateController:SetState("Mask_select")
        end
    else
        if num > 0 then
            cell.stateController:SetState("Normal")
        else
            cell.stateController:SetState("Mask")
        end
    end

    if not self.m_initController and luaIndex == 1 then
        self.m_initController = true
        self:SetNaviTarget(cell.button)
    else
        if self.m_selectedClueId == clueId then
            self:SetNaviTarget(cell.button)
        end
    end
end

SpaceshipRoomClueGiftCtrl._selectClue = HL.Method(HL.Number, HL.Boolean) << function(self, luaIndex, isInit)
    local curClueId = self.m_luaIndex2ClueId[luaIndex]
    if self.m_selectedClueId == curClueId then
        return
    end
    self:_ClearInput()
    local num = 0
    if self.m_clueId2HaveClues[self.m_selectedClueId] ~= nil then
        num = #self.m_clueId2HaveClues[self.m_selectedClueId]
    end
    local lastSelectedCell = self.m_clueId2ClueCell[self.m_selectedClueId]
    if lastSelectedCell then
        if num > 0 then
            lastSelectedCell.stateController:SetState("Normal")
        else
            lastSelectedCell.stateController:SetState("Mask")
        end
        lastSelectedCell.animationWrapper:Play("spaceshiproomcluegiftcluecell_selectout")
    end

    self.m_selectedClueId = curClueId

    local curSelectedCell = self.m_clueId2ClueCell[self.m_selectedClueId]
    num = 0
    if self.m_clueId2HaveClues[self.m_selectedClueId] ~= nil then
        num = #self.m_clueId2HaveClues[self.m_selectedClueId]
    end
    if curSelectedCell then
        if num > 0 then
            curSelectedCell.stateController:SetState("Select")
        else
            curSelectedCell.stateController:SetState("Mask_select")
        end
        curSelectedCell.animationWrapper:Play("spaceshiproomcluegiftcluecell_selectin")
        if not isInit then
            AudioAdapter.PostEvent("Au_UI_Toggle_Common_On")
        end
    end
    if self.m_isInitFriend then
        self.friendArg.selectedClueId = self.m_selectedClueId
        self.friendArg.onGiftBtnEnable = num > 0
        self:OnSync()
    else
        self:_InitFriendList()
    end
end

SpaceshipRoomClueGiftCtrl._InitFriendList = HL.Method() << function(self)
    if self.m_isInitFriend then
        return
    end

    local num = 0
    local lastSelectedCell = self.m_clueId2ClueCell[self.m_selectedClueId]
    if lastSelectedCell then
        if self.m_clueId2HaveClues[self.m_selectedClueId] ~= nil then
            num = #self.m_clueId2HaveClues[self.m_selectedClueId]
        end
    end

    self.friendArg = {
        stateName = "SpaceshipClueGift",
        maxLen = Tables.globalConst.friendListPageMaxLen,
        hideSignature = true,
        onGiftBtnEnable = num > 0,
        clueGiftNaviModel = true,
        clueGiftNaviFun = function()
            self.m_inFriendListGroup = false
            InputManagerInst:ToggleBinding(self.friendListJumpIn, true)
            InputManagerInst:ToggleBinding(self.friendListArrowJumpIn, true)
        end,
        onGiftBtnClick = function(roleId, sendSuccessAction)
            local curClues = self.m_clueId2HaveClues[self.m_selectedClueId]
            if curClues == nil or #curClues == 0 then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_SEND_CLUE_NO_HAVE_TOAST)
                return
            end

            self.m_lastSendInfo = {
                roleId = roleId,
                clueId = self.m_selectedClueId,
                sendSuccessAction = sendSuccessAction,
            }

            GameInstance.player.spaceship:PresentFriendClue(roleId, curClues[1])
        end,
        onPlayerClick = function(roleId)
            FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(roleId).action()
        end,
        isFilter = true,
        customSortFun = function(friendList)
            self:_UpdateCache(friendList)
            return self.m_friendList
        end,
        sortOptions = {
            {
                name = Language.LUA_FRIEND_LAST_DATE_TIME,
                keys = { "isCurrentShip", "searchSort", "lastDateTime", "adventureLevel", "addFriendTime", "helpFlag", "roleId" },
            },
        },
        selectedClueId = self.m_selectedClueId,
        onSearchChange = function(str)
            self.view.clearBtn.gameObject:SetActiveIfNecessary(not (string.isEmpty(str)))
            self.view.searchResult.gameObject:SetActiveIfNecessary(not (string.isEmpty(str)))
        end
    }

    self.view.friendList:InitFriendListCtrl(self.friendArg)
    GameInstance.player.friendSystem:SyncFriendSimpleInfo()
    self:Loading()
    self.m_isInitFriend = true
end

SpaceshipRoomClueGiftCtrl._InitInputField = HL.Method() << function(self)
    UIUtils.initSearchInput(self.view.friendList.view.inputField, {
        clearBtn = self.view.clearBtn,
        onInputFocused = function()
            self.view.friendList.view.inputField.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_FOCUS_WIDTH, self.view.friendList.view.inputField.transform.sizeDelta.y)
            self.view.inputBgImage.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_BG_FOCUS_WIDTH, self.view.inputBgImage.transform.sizeDelta.y)
            self.view.clearBtn.transform.anchoredPosition = Vector2(self.view.config.CLEAR_BTN_FOCUS_POS, self.view.clearBtn.transform.localPosition.y)
            self:_StartInput()
            self.view.clearBtn.gameObject:SetActiveIfNecessary(not (string.isEmpty(self.view.friendList.view.inputField.text)))
            self.view.searchResult.gameObject:SetActiveIfNecessary(not (string.isEmpty(str)))
            self.view.inputNode:Play("friendblacklistipput_in")
        end,
        onInputEndEdit = function()
            if string.isEmpty(self.view.friendList.view.inputField.text) then
                self.view.friendList.view.inputField.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_WIDTH, self.view.friendList.view.inputField.transform.sizeDelta.y)
                self.view.inputBgImage.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_BG_WIDTH, self.view.inputBgImage.transform.sizeDelta.y)
                self.view.clearBtn.transform.anchoredPosition = Vector2(self.view.config.CLEAR_BTN_POS, self.view.clearBtn.transform.localPosition.y)
                self.view.clearBtn.gameObject:SetActiveIfNecessary(false)
                self.view.searchResult.gameObject:SetActiveIfNecessary(false)
                self.view.inputNode:Play("friendblacklistipput_out")
            end
            self:_EndInput()
        end,
        onInputValueChanged = function(str)
            self.view.friendList:OnChangeInputField(str)
        end,
        onClearClick = function()
            self:_ClearInput()
        end,
    })
end

SpaceshipRoomClueGiftCtrl._StartInput = HL.Method() << function(self)
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

SpaceshipRoomClueGiftCtrl._EndInput = HL.Method() << function(self)
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.textInputBindingGroup.groupId)
    self.view.friendList.view.inputField:DeactivateInputField(true)
end

SpaceshipRoomClueGiftCtrl._ClearInput = HL.Method() << function(self)
    self.view.friendList.view.inputField.text = ""
end

SpaceshipRoomClueGiftCtrl.OnSync = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh(false)
    if not string.isEmpty(self.m_pendingInputText) then
        self.view.friendList.view.inputField.text = self.m_pendingInputText
        self.view.clearBtn.gameObject:SetActiveIfNecessary(true)
        self.view.searchResult.gameObject:SetActiveIfNecessary(true)
        self.m_pendingInputText = ""
    elseif not string.isEmpty(self.view.friendList.view.inputField.text) then
        self.view.friendList:OnChangeInputField(self.view.friendList.view.inputField.text)
    end
end

SpaceshipRoomClueGiftCtrl.OnCellChange = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh(false, true)
end

SpaceshipRoomClueGiftCtrl.OnSpaceshipPresentFriendClue = HL.Method() << function(self)
    self:_UpdateClueId2HaveClues()
    if self.m_lastSendInfo ~= nil then




        self.m_lastSendInfo.sendSuccessAction()
    end
    self.m_lastSendInfo = nil
end

SpaceshipRoomClueGiftCtrl.Loading = HL.Method() << function(self)
    self.m_friendList = {}
    self:_Refresh(true)
end

SpaceshipRoomClueGiftCtrl._UpdateCache = HL.Method(HL.Opt(HL.Any)) << function(self, filterList)
    local infoDict = GameInstance.player.friendSystem.friendInfoDic
    local inSevenDayDict = {}
    local outSevenDayDict = {}

    if filterList ~= nil then
        infoDict = {}
        for i, info in pairs(filterList) do
            local success, friendInfo = GameInstance.player.friendSystem.friendInfoDic:TryGetValue(info.roleId)
            if success then
                infoDict[info.roleId] = friendInfo
            end
        end
        for _, friendInfo in pairs(infoDict) do
            if friendInfo.clueRoomUnlock then
                local preSevenDayTime = DateTimeUtils.GetCurrentTimestampBySeconds() - 24 * 3600 * 7
                if friendInfo.lastDateTime > preSevenDayTime then
                    self:_CreateInSevenDay(inSevenDayDict, friendInfo)
                else
                    self:_CreateOutSevenDay(outSevenDayDict, friendInfo)
                end
            end
        end
    else
        for _, friendInfo in cs_pairs(infoDict) do
            if friendInfo.clueRoomUnlock then
                local preSevenDayTime = DateTimeUtils.GetCurrentTimestampBySeconds() - 24 * 3600 * 7
                if friendInfo.lastDateTime > preSevenDayTime then
                    self:_CreateInSevenDay(inSevenDayDict, friendInfo)
                else
                    self:_CreateOutSevenDay(outSevenDayDict, friendInfo)
                end
            end
        end
    end


    table.sort(inSevenDayDict, Utils.genSortFunctionWithIgnore(
        {
            "needCurSelectClueId",
            "haveClueNum",
            "SortFirstNeedClueId",
            "lastDateTime",
            "adventureLevel",
            "addFriendSort",
        }, false, {})
    )

    table.sort(outSevenDayDict, Utils.genSortFunctionWithIgnore(
        {
            "lastDateTime",
            "adventureLevel",
            "addFriendSort",
        }, false, {})
    )

    self.m_friendList = {}
    for key, value in pairs(inSevenDayDict) do
        table.insert(self.m_friendList, value)
    end
    for key, value in pairs(outSevenDayDict) do
        table.insert(self.m_friendList, value)
    end
end

SpaceshipRoomClueGiftCtrl._CreateInSevenDay = HL.Method(HL.Table, HL.Any) << function(self, inSevenDayDict, friendInfo)

    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local clueDict = {}
    local haveClueNum = 0
    local firstNeedClueId = 10

    for i = 1, Tables.spaceshipConst.spaceshipGuestRoomClueTypeTotalCount do
        local hasVal, value = friendInfo.hostClueStatus:TryGetValue(i)
        if hasVal then
            if value == 0 or value > curTime then
                clueDict[i] = true
                haveClueNum = haveClueNum + 1
            else
                if firstNeedClueId == 10 then
                    firstNeedClueId = i
                end
            end
        else
            if firstNeedClueId == 10 then
                firstNeedClueId = i
            end
        end
    end

    local selectedClueId = self.m_selectedClueId
    if self.m_selectedClueId == -1 then
        selectedClueId = self.m_luaIndex2ClueId[1]
    end

    local needCurSelectClueId = 0
    if clueDict[selectedClueId] ~= true then
        needCurSelectClueId = 1
    end

    local createFriend = {
        roleId = friendInfo.roleId,
        name = friendInfo.name,
        needCurSelectClueId = needCurSelectClueId,
        haveClueNum = haveClueNum,
        SortFirstNeedClueId = 10 - firstNeedClueId,
        lastDateTime = friendInfo.lastDateTime,

        addFriendSort = -friendInfo.addOrRequestTime,
        addFriendTime = friendInfo.addOrRequestTime,
        adventureLevel = friendInfo.adventureLevel,
        accountId = friendInfo.psnData ~= nil and friendInfo.psnData.AccountId or "",
        helpFlag = friendInfo.helpFlag:GetHashCode(),
        isCurrentShip = friendInfo.roleId == GameInstance.player.spaceship:GetFriendRoleInfo().roleId and 1 or 0,
    }
    table.insert(inSevenDayDict, createFriend)

end

SpaceshipRoomClueGiftCtrl._CreateOutSevenDay = HL.Method(HL.Table, HL.Any) << function(self, outSevenDayDict, friendInfo)
    local createFriend = {
        roleId = friendInfo.roleId,
        name = friendInfo.name,
        lastDateTime = friendInfo.lastDateTime,

        addFriendTime = friendInfo.addOrRequestTime,
        addFriendSort = -friendInfo.addOrRequestTime,
        adventureLevel = friendInfo.adventureLevel,
        accountId = friendInfo.psnData ~= nil and friendInfo.psnData.AccountId or "",
        helpFlag = friendInfo.helpFlag:GetHashCode(),
        isCurrentShip = friendInfo.roleId == GameInstance.player.spaceship:GetFriendRoleInfo().roleId and 1 or 0,
    }
    table.insert(outSevenDayDict, createFriend)
end

SpaceshipRoomClueGiftCtrl._Refresh = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, loading, stayPos)
    self.view.friendCountTxt.text = string.format("%d/%d", #self.m_friendList, Tables.globalConst.friendListLenMax)
    if stayPos then
        self.view.friendList:RefreshInfoStayPos(self.m_friendList)
    else
        self.view.friendList:RefreshInfo(self.m_friendList, true, Language.LUA_SPACESHIP_SEND_NO_FRIEND_TIP, loading)
    end

    if not self.m_inFriendListGroup then
        InputManagerInst:ToggleBinding(self.friendListJumpIn, #self.m_friendList > 0)
        InputManagerInst:ToggleBinding(self.friendListArrowJumpIn, #self.m_friendList > 0)
    end
end

SpaceshipRoomClueGiftCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {}
    local recoverState = {}
    local hasState = false

    if self.m_selectedClueId and self.m_selectedClueId ~= -1 then
        recoverState.selectedClueId = self.m_selectedClueId
        hasState = true
    end

    local inputField = self.view.friendList.view.inputField
    if inputField and inputField.text and not string.isEmpty(inputField.text) then
        recoverState.inputText = inputField.text
        hasState = true
    end

    if hasState then
        arg.recoverState = recoverState
    end
    return arg
end

SpaceshipRoomClueGiftCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    if recoverState.inputText and not string.isEmpty(recoverState.inputText) then
        self.m_pendingInputText = recoverState.inputText
    end

    if recoverState.selectedClueId and recoverState.selectedClueId ~= self.m_selectedClueId then
        for luaIndex = 1, #self.m_luaIndex2ClueId do
            if self.m_luaIndex2ClueId[luaIndex] == recoverState.selectedClueId then
                self:_selectClue(luaIndex, true)
                break
            end
        end
    end
end

SpaceshipRoomClueGiftCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.ON_POP_SPACESHIP_GUEST_ROOM_MAIN_PANEL)
end

HL.Commit(SpaceshipRoomClueGiftCtrl)
