local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local ChatType = CS.Beyond.Gameplay.SNSFriendChatSystem.ChatType

FriendDialogContent = HL.Class('FriendDialogContent', UIWidgetBase)


FriendDialogContent.m_mainPanel = HL.Field(HL.Any)

FriendDialogContent.m_friendInfo = HL.Field(HL.Any)

FriendDialogContent.m_getDialogCellFunc = HL.Field(HL.Function)

FriendDialogContent.m_showRoleId = HL.Field(HL.Number) << 0

FriendDialogContent.m_messages = HL.Field(HL.Any)

FriendDialogContent.m_csIndex2DialogCell = HL.Field(HL.Table)

FriendDialogContent.m_msgIndex2DialogCell = HL.Field(HL.Table)

FriendDialogContent.m_updateShareInfoTime = HL.Field(HL.Table)

FriendDialogContent.m_readTickHandle = HL.Field(HL.Number) << -1

FriendDialogContent.m_readTickTime = HL.Field(HL.Number) << 0

FriendDialogContent.m_showRangeX = HL.Field(HL.Number) << 0

FriendDialogContent.m_showRangeY = HL.Field(HL.Number) << 0

FriendDialogContent.m_canJumpIn = HL.Field(HL.Boolean) << false

FriendDialogContent.m_canManuallyFocus = HL.Field(HL.Table)

FriendDialogContent.m_nextManuallyFocusIndex = HL.Field(HL.Number) << -1

FriendDialogContent.m_executeJumpIn = HL.Field(HL.Boolean) << false

FriendDialogContent.m_nextToBottomRoleId = HL.Field(HL.Number) << -1

FriendDialogContent.m_curShowMessageCount = HL.Field(HL.Number) << 0

FriendDialogContent.m_curShowMaxMsgIndex = HL.Field(HL.Number) << 0

FriendDialogContent.m_curWindowMsgMaxIndex = HL.Field(HL.Number) << -1

FriendDialogContent.m_updateAllMessagesInShow = HL.Field(HL.Boolean) << false

FriendDialogContent.m_addTopMsgFlag = HL.Field(HL.Boolean) << false

FriendDialogContent.m_addTopMsgTime = HL.Field(HL.Number) << 0

FriendDialogContent.m_curRootState = HL.Field(HL.Any)

FriendDialogContent.m_playAnimMsgIndex = HL.Field(HL.Number) << -1

local ShowLoadingInterval = 0.2
local AddMessageInterval = 0.1
local UPDATE_INTERVAL = 10
local AllContent = {
    Normal = "Normal",
    NotSelected = "NotSelected",
    NoMessages = "NoMessages",
}


FriendDialogContent._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.RECV_FRIEND_CHAT_CONTENT, function(args)
        self:_OnRecvFriendChatContent(args)
    end)

    self:RegisterMessage(MessageConst.RECV_FRIEND_SEND_CHAT_NOTIFY, function(args)
        self:_OnRecvSendChatNotify(args)
    end)

    self:RegisterMessage(MessageConst.RECV_ADD_LUA_SHOW_MESSAGES, function(args)
        self:_OnRecvLuaShowMessages(args)
    end)

    self:RegisterMessage(MessageConst.FRIEND_CHAT_UPDATE_BLUEPRINT, function(args)
        self:_FriendChatUpdateBlueprint(args)
    end)

    self:RegisterMessage(MessageConst.FRIEND_CHAT_UPDATE_SOCIAL_BUILDING, function(args)
        self:_FriendChatUpdateSocialBuilding(args)
    end)

    self:RegisterMessage(MessageConst.FRIEND_CHAT_SCROLL_LIST_TO_INDEX, function(args)
        self:_DialogScrollToIndex(args)
    end)

    self:RegisterMessage(MessageConst.FRIEND_CHAT_FOCUS_MESSAGE, function()
        self:FocusFirstMessage()
    end)

end

FriendDialogContent._FriendChatUpdateBlueprint = HL.Method(HL.Any) << function(self, args)
    local targetRoleId, msgIndex = unpack(args)
    if targetRoleId == self.m_showRoleId then
        self:UpdateMessagesByMsgIndex(msgIndex)
    end
end


FriendDialogContent._FriendChatUpdateSocialBuilding = HL.Method(HL.Any) << function(self, args)
    local targetRoleId, msgIndex = unpack(args)
    if targetRoleId == self.m_showRoleId then
        self:UpdateMessagesByMsgIndex(msgIndex)
    end
end


FriendDialogContent._DialogScrollToIndex = HL.Method(HL.Number) << function(self, index)
    self.view.dialogScrollList:ScrollToIndex(index)
end

FriendDialogContent._OnRecvFriendChatContent = HL.Method(HL.Any) << function(self, args)
    local targetRoleId = unpack(args)
    if targetRoleId == self.m_showRoleId then
        self:UpdateAllMessages("RecvNewMsgInOpen")
    end
end


FriendDialogContent._OnRecvLuaShowMessages = HL.Method(HL.Any) << function(self, args)
    local targetRoleId = unpack(args)
    if targetRoleId == self.m_showRoleId then
        self:UpdateAllMessages("RecvLuaShowMessages")
    end
end

FriendDialogContent._OnRecvSendChatNotify = HL.Method(HL.Any) << function(self, args)
    local targetRoleId = unpack(args)
    if targetRoleId == self.m_showRoleId then
        GameInstance.player.friendChatSystem:QueryChatContent(self.m_showRoleId)
    end
end

FriendDialogContent.OnFriendRemakeNameModify = HL.Method() << function(self)
    self:UpdateNameInfos()
end


FriendDialogContent.InitFriendDialogContent = HL.Method(HL.Any) << function(self, mainPanel)
    self.m_mainPanel = mainPanel
    self:_FirstTimeInit()
    self.m_updateShareInfoTime = {}
    self:ClearDialogScrollListData()
    self.m_readTickTime = 0
    self.m_readTickHandle = LuaUpdate:Add("Tick", function(deltaTime)
        self:_ReadTick(deltaTime)
    end)

    self:UpdateContentState(AllContent.NotSelected)
    self:UpdateFriendInfo(nil)

    self.view.nameDetailsBtn.onClick:AddListener(function()
        self:_ClickNameDetailsBtn()
    end)

    self:InitDialogScrollList()
    self.view.newMessageBtn.onClick:AddListener(function()
        self:_ClickNewMessageBtn()
    end)
    self.view.loadingMessageNode.gameObject:SetActive(false)
    self.view.friendDialogueSendArea:InitFriendDialogueSendArea(self)

end

FriendDialogContent.ClearDialogScrollListData = HL.Method() << function(self)
    self.m_csIndex2DialogCell = {}
    self.m_canManuallyFocus = {}
    self.m_msgIndex2DialogCell = {}
end

FriendDialogContent.InitDialogScrollList = HL.Method() << function(self)

    self:ClearDialogScrollListData()
    self.m_getDialogCellFunc = UIUtils.genCachedCellFunction(self.view.dialogScrollList)
    self.view.dialogScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        local cell = self.m_getDialogCellFunc(gameObject)
        if self.m_messages.Count <= csIndex then
            return
        end
        local message = self.m_messages[csIndex]
        self.m_csIndex2DialogCell[csIndex] = cell
        self.m_msgIndex2DialogCell[message.msgIndex] = cell
        cell:InitSNSFriendDialogContentCell(self.m_showRoleId, message, self.view.dialogContentNaviGroup, function()





        end)

        self.m_canManuallyFocus[csIndex] = cell:CheckCanJumpIn()

        if self.m_nextManuallyFocusIndex ~= -1 and self.m_nextManuallyFocusIndex == csIndex then
            if cell:CheckCanJumpIn() then
                cell:SetTargetNode()
            end
            self.m_nextManuallyFocusIndex = -1
        end
    end)

    self:UpdateAllMessages("normal")
end

FriendDialogContent.UpdateMessagesByMsgIndex = HL.Method(HL.Number) << function(self, msgIndex)
    if self.m_msgIndex2DialogCell ~= nil and self.m_msgIndex2DialogCell[msgIndex] ~= nil then
        self.m_msgIndex2DialogCell[msgIndex]:UpdateDataShowInfo()
    end
end

FriendDialogContent.UpdateAllMessages = HL.Method(HL.String) << function(self, updateMode)
    if self.m_showRoleId == 0 then
        self:UpdateContentState(AllContent.NotSelected)
        return
    end

    local success, friendInfo = GameInstance.player.friendSystem.friendInfoDic:TryGetValue(self.m_showRoleId)
    if not success or not friendInfo.init then
        self:UpdateContentState(AllContent.NoMessages)
        return
    end


    if self.m_mainPanel:IsHide() then
        self.m_updateAllMessagesInShow = true
        return
    end

    local messages = GameInstance.player.friendChatSystem.luaShowMessages
    if messages and messages.Count > 0 then
        self.m_messages = messages
        self:ClearDialogScrollListData()
        self.m_showRangeX = 0
        self.m_showRangeY = 0
        GameInstance.player.friendChatSystem:UpdateTimeShowByRoleId()
        GameInstance.player.friendChatSystem:UpdateSocialBuildingRepeatInfo()
        self:UpdateContentState(AllContent.Normal)
        self.m_curShowMessageCount = messages.Count
        if messages.Count > 0 then
            self.m_curShowMaxMsgIndex = messages[messages.Count - 1].msgIndex
        end

        if self.m_nextToBottomRoleId == self.m_showRoleId then
            self.view.dialogScrollList:UpdateCount(self.m_curShowMessageCount, messages.Count-1)
            self.view.dialogScrollList:SetCurrentStep(self.view.dialogScrollList:GetLastScrollStep())
            self.m_nextToBottomRoleId = -1
            return
        end
        self.m_nextToBottomRoleId = -1
        local newestMessage = self.m_messages[messages.Count-1]
        if updateMode == "RecvNewMsgInOpen" then
            if self.m_curWindowMsgMaxIndex < self.m_curShowMaxMsgIndex - 2 then
                if newestMessage.ownerId == GameInstance.player.roleId then
                    self.m_playAnimMsgIndex = newestMessage.msgIndex
                    AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_MyselfNode_Open")
                    self.view.dialogScrollList:UpdateCount(self.m_curShowMessageCount, messages.Count-1)
                    self.view.dialogScrollList:SetCurrentStep(self.view.dialogScrollList:GetLastScrollStep())
                else
                    self.view.dialogScrollList:UpdateCount(self.m_curShowMessageCount)
                end
            else
                self.m_playAnimMsgIndex = newestMessage.msgIndex
                if newestMessage.ownerId == GameInstance.player.roleId then
                    AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_MyselfNode_Open")
                else
                    AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_OtherNode_Open")
                end
                self.view.dialogScrollList:UpdateCount(self.m_curShowMessageCount, messages.Count-1)
                self.view.dialogScrollList:SetCurrentStep(self.view.dialogScrollList:GetLastScrollStep())
            end
        elseif updateMode == "RecvLuaShowMessages" then
            self.view.dialogScrollList:UpdateCount(self.m_curShowMessageCount, -1)
        else
            self.view.dialogScrollList:UpdateCount(self.m_curShowMessageCount, messages.Count-1)
            self.view.dialogScrollList:SetCurrentStep(self.view.dialogScrollList:GetLastScrollStep())
        end

        if self.m_playAnimMsgIndex ~= -1 then
            local cell = self.m_msgIndex2DialogCell[self.m_playAnimMsgIndex]
            if cell then
                cell:PlayInAnimation()
            end
            self.m_playAnimMsgIndex = -1
        end
    else
        self:UpdateContentState(AllContent.NoMessages)

    end
end

FriendDialogContent.UpdateContentState = HL.Method(HL.String) << function(self, state)
    self.m_curRootState = state
    self.view.rootStateController:SetState(state)
end

FriendDialogContent.UpdateFriendInfo = HL.Method(HL.Any) << function(self, friendInfo)
    self.m_friendInfo = friendInfo
    self:UpdateNameInfos()
end


FriendDialogContent.UpdateNameInfos = HL.Method() << function(self)
    local friendInfo = self.m_friendInfo
    self.m_curWindowMsgMaxIndex = -1

    if friendInfo == nil then
        self.m_showRoleId = 0
        GameInstance.player.friendChatSystem:SetLuaShowRoleId(self.m_showRoleId)
        self.view.pS5NameNode.gameObject:SetActive(false)
        self.view.psNameTxt.text = ""
        self.view.notesNameTxt.text = ""
        self.view.nameMarkPre.text = ""
        self.view.nameMarkPost.text = ""
        self.view.personalityTxt.text = ""
    else
        self.m_showRoleId = friendInfo.roleId
        GameInstance.player.friendChatSystem:SetLuaShowRoleId(self.m_showRoleId)
        self.view.friendDialogueSendArea:UpdateShowRoleId(self.m_showRoleId)

        self.view.pS5NameNode.gameObject:SetActive(not string.isEmpty(friendInfo.psName))
        self.view.psNameTxt.text = friendInfo.psName

        local nameStr = string.format(Language.LUA_FRIEND_NAME, friendInfo.name, friendInfo.shortId)
        if friendInfo.name == nil then
            nameStr = ""
        end
        if friendInfo.remakeName and not string.isEmpty(friendInfo.remakeName) then
            self.view.notesNameTxt.gameObject:SetActive(true)
            self.view.nameMarkPre.gameObject:SetActive(false)
            self.view.nameMarkPost.gameObject:SetActive(true)
            self.view.notesNameTxt.text = friendInfo.remakeName
            self.view.nameMarkPost.text = nameStr
        else
            self.view.notesNameTxt.gameObject:SetActive(false)
            self.view.nameMarkPre.gameObject:SetActive(true)
            self.view.nameMarkPost.gameObject:SetActive(false)
            self.view.nameMarkPre.text = nameStr
        end

        self.view.personalityTxt.text = friendInfo.signature
    end
end

FriendDialogContent._ClickNameDetailsBtn = HL.Method() << function(self)
    if self.m_showRoleId == 0 then
        return
    end

    local id = self.m_showRoleId
    local args = {
        transform = self.view.nameDetailsBtnRectTransform,
        useSmallContent = true,
        useRightTitle = true,
        cellHeight = FriendUtils.CELL_HEIGHT,
    }

    local CloseChat = function(id)
        return {
            text = Language.LUA_FRIEND_CLOSE_CHAT,
            action = function()
                Notify(MessageConst.FRIEND_CHAT_PLAYER_DELETE_LIST_CELL)
                GameInstance.player.friendChatSystem:CloseChat(id)
            end
        }
    end

    if DeviceInfo.inputType == DeviceInfo.InputType.Controller then
        args.actions = {



            [1] = FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(id),
            [2] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REMARK_MODIFY(id),
            [3] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REMOVE_FRIEND(id),
            [4] = FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id),
            [5] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT(id),
            [6] = CloseChat(id),
        }
    else
        args.actions = {
            [1] = FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(id),
            [2] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REMARK_MODIFY(id),
            [3] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REMOVE_FRIEND(id),
            [4] = FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id),
            [5] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT(id),
            [6] = CloseChat(id),
        }
    end
    if BEYOND_DEBUG then
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ROLE_ID(id))
    end
    Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
end


FriendDialogContent._ReadTick = HL.Method(HL.Number) << function(self, deltaTime)
    if self.m_showRoleId == 0 or self.m_curRootState == AllContent.NotSelected or self.m_curRootState == AllContent.NoMessages then
        self.view.newMessageBtn.gameObject:SetActive(false)
        return
    end

    local chatInfo = GameInstance.player.friendChatSystem:GetChatInfo(self.m_showRoleId)
    if chatInfo then
        if chatInfo.unReadNum > 0
            and self.m_curWindowMsgMaxIndex ~= -1 and self.m_curWindowMsgMaxIndex < chatInfo.newestIndex - 2 then
            self.view.newMessageBtn.gameObject:SetActive(true)
        else
            self.view.newMessageBtn.gameObject:SetActive(false)
        end
    else
        self.view.newMessageBtn.gameObject:SetActive(false)
    end

    self.m_readTickTime = 0
    local res = self.view.dialogScrollList:GetShowRange()

    self.m_showRangeX = res.x
    self.m_showRangeY = res.y

    if self.m_messages == nil then
        return
    end

    if self.view.dialogScrollRect.content.anchoredPosition.y < -10 then
        if self.m_messages.Count > 0 and GameInstance.player.friendChatSystem.luaShowMinIndex > 0
            and GameInstance.player.friendChatSystem.luaShowMinIndex < 300 then

            self.m_addTopMsgTime = self.m_addTopMsgTime + deltaTime
            if self.m_addTopMsgTime > AddMessageInterval then
                self.m_addTopMsgFlag = true
            end

            if self.m_addTopMsgTime > ShowLoadingInterval then
                if not self.view.loadingMessageNode.gameObject.activeSelf  then
                    self.view.loadingMessageNode.gameObject:SetActive(true)
                end
            end
        end
    else
        if self.m_addTopMsgFlag then
            self:_TryQueryAddMessage()
            self.m_addTopMsgTime = 0
            self.m_addTopMsgFlag = false
        end
    end


    if self.m_messages ~= nil and self.m_showRangeY >= 0 and self.m_messages.Count > self.m_showRangeY then
        local curShowMaxMessageContent = self.m_messages[self.m_showRangeY]
        if curShowMaxMessageContent ~= nil then
            self.m_curWindowMsgMaxIndex = curShowMaxMessageContent.msgIndex
            GameInstance.player.friendChatSystem:SetChatRead(self.m_showRoleId, curShowMaxMessageContent.msgIndex)
        end
    end

    local canJumpIn = false
    if self.m_showRangeX >= 0 and self.m_showRangeY >= 0 then
        local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        for csIndex = self.m_showRangeX, self.m_showRangeY do
            local cell = self.m_csIndex2DialogCell[csIndex]
            if cell then
                if self.m_updateShareInfoTime[cell.m_msgIndex] == nil then
                    self.m_updateShareInfoTime[cell.m_msgIndex] = curTime
                    cell:UpdateInfoFromServer(self.m_showRoleId)
                else
                    if curTime - self.m_updateShareInfoTime[cell.m_msgIndex] > UPDATE_INTERVAL then
                        self.m_updateShareInfoTime[cell.m_msgIndex] = curTime
                        cell:UpdateInfoFromServer(self.m_showRoleId)
                    end
                end
            end
        end

        for csIndex = self.m_showRangeX, self.m_showRangeY do
            local cell = self.m_csIndex2DialogCell[csIndex]
            if cell ~= nil and cell:CheckCanJumpIn() then
                canJumpIn = true
                break
            end
        end
    end

    self.m_canJumpIn = canJumpIn and not self.view.friendDialogueSendArea:CheckOpenSelectPanel()
    if self.m_canJumpIn and self.m_executeJumpIn then
        self.m_executeJumpIn = false
        self:FocusFirstMessage()
    end
end


FriendDialogContent._TryQueryAddMessage = HL.Method() << function(self)
    self.view.loadingMessageNode.gameObject:SetActive(false)
    GameInstance.player.friendChatSystem:TopAddLuaShowMessages()
end


FriendDialogContent._ClickNewMessageBtn = HL.Method() << function(self)


    if DeviceInfo.usingController then
        if self.m_mainPanel.controllerInMessageItem then
            self.m_mainPanel:ExitMessageItemNaviGroup()
        end
    end

    self:UpdateAllMessages("normal")
end


FriendDialogContent.FocusFirstMessage = HL.Method() << function(self)
    if self.m_showRangeX >= 0 and self.m_showRangeY >= 0 then
        local number = 0
        for csIndex = self.m_showRangeX, self.m_showRangeY do
            local cell = self.m_csIndex2DialogCell[csIndex]
            if cell ~= nil and cell:CheckCanJumpIn() then
                number = number + 1
                cell:SetTargetNode()
                break
            end
        end
    end
end

FriendDialogContent.ManuallyFocusMessageUp = HL.Method() << function(self)
    local searchMin = math.max(self.m_showRangeX - 50, 0)
    for csIndex = self.m_showRangeX, searchMin, -1 do
        if self.m_canManuallyFocus[csIndex] then
            self.m_nextManuallyFocusIndex = csIndex
            self:_DialogScrollToIndex(csIndex)
            return
        end
    end
end

FriendDialogContent.ManuallyFocusMessageDown = HL.Method() << function(self)
    local searchMax = math.min(self.m_showRangeY + 50, self.m_messages.Count - 1)
    for csIndex = self.m_showRangeY, searchMax do
        if self.m_canManuallyFocus[csIndex] then
            self.m_nextManuallyFocusIndex = csIndex
            self:_DialogScrollToIndex(csIndex)
            return
        end
    end
end

FriendDialogContent.CustomOnShow = HL.Method() << function(self)
    if self.m_updateAllMessagesInShow then
        self.m_updateAllMessagesInShow = false
        self:UpdateAllMessages("normal")
    end
end

FriendDialogContent.CustomOnHide = HL.Method() << function(self)

end


FriendDialogContent.CustomOnClose = HL.Method() << function(self)
    if self.m_readTickHandle > 0 then
        self.m_readTickHandle = LuaUpdate:Remove(self.m_readTickHandle)
    end
end

HL.Commit(FriendDialogContent)
return FriendDialogContent

