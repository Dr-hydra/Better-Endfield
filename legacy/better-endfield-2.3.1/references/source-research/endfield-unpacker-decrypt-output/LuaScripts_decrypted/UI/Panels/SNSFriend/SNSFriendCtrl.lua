local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SNSFriend
SNSFriendCtrl = HL.Class('SNSFriendCtrl', uiCtrl.UICtrl)

SNSFriendCtrl.m_getFriendCell = HL.Field(HL.Function)

SNSFriendCtrl.m_csIndex2friendInfo = HL.Field(HL.Table)

SNSFriendCtrl.m_csIndex2friendCell = HL.Field(HL.Table)

SNSFriendCtrl.m_selectedCsIndex = HL.Field(HL.Number) << -1

SNSFriendCtrl.m_selectedRoleId = HL.Field(HL.Number) << -1

SNSFriendCtrl.m_nextOpenRoleId = HL.Field(HL.Number) << -1

SNSFriendCtrl.friendSystem = HL.Field(CS.Beyond.Gameplay.FriendSystem)

SNSFriendCtrl.m_infoTickHandle = HL.Field(HL.Number) << -1


SNSFriendCtrl.m_initSetTarget = HL.Field(HL.Boolean) << false

SNSFriendCtrl.messageSendAreaJumpIn = HL.Field(HL.Number) << -1

SNSFriendCtrl.messageSendAreaJumpOut = HL.Field(HL.Number) << -1

SNSFriendCtrl.messageSendAreaUseArrowJumpIn = HL.Field(HL.Number) << -1

SNSFriendCtrl.messageSendAreaUseArrowJumpOut = HL.Field(HL.Number) << -1

SNSFriendCtrl.messageItemJumpIn = HL.Field(HL.Number) << -1

SNSFriendCtrl.messageItemJumpOut = HL.Field(HL.Number) << -1

SNSFriendCtrl.messageItemPrev = HL.Field(HL.Number) << -1

SNSFriendCtrl.messageItemPost = HL.Field(HL.Number) << -1

SNSFriendCtrl.controllerInRightArea = HL.Field(HL.Boolean) << false

SNSFriendCtrl.controllerInMessageItem = HL.Field(HL.Boolean) << false

SNSFriendCtrl.m_requestFriendInfoIds = HL.Field(HL.Table)

SNSFriendCtrl.m_requestInfoIndex = HL.Field(HL.Number) << 1

SNSFriendCtrl.m_requestHandle = HL.Field(HL.Number) << -1

SNSFriendCtrl.m_requestTime = HL.Field(HL.Number) << 1

SNSFriendCtrl.m_onPlayerAddListCell = HL.Field(HL.Boolean) << false

SNSFriendCtrl.m_onPlayerDeleteListCell = HL.Field(HL.Boolean) << false

SNSFriendCtrl.m_controllerBackFlag = HL.Field(HL.Boolean) << false





SNSFriendCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SYNC_FRIEND_CHAT_ALL_INFO] = 'OnSyncFriendChatAllInfo',
    [MessageConst.ON_CLICK_OPEN_FRIEND_CHAT] = 'OnClickOpenFriendChat',
    [MessageConst.FRIEND_CHAT_MSG_READ] = 'OnMsgReadChange',
    [MessageConst.ON_FRIEND_REMAKE_NAME_MODIFY] = 'OnFriendRemakeNameModify',
    [MessageConst.FRIEND_CHAT_PLAYER_ADD_LIST_CELL] = 'OnPlayerAddListCell',
    [MessageConst.FRIEND_CHAT_PLAYER_DELETE_LIST_CELL] = 'OnPlayerDeleteListCell',
}

local LeftFriendState = {
    Selected = "Selected",
    Normal = "Normal"
}

local RequestBatchNum = 10


SNSFriendCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg ~= nil and arg.roleId then
        self.m_nextOpenRoleId = arg.roleId
    end

    GameInstance.player.friendChatSystem:InitRecentTextUniq()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.m_csIndex2friendInfo = {}
    self.view.friendDialogContent:InitFriendDialogContent(self)
    self.view.btnCommonFilter.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.Friend, {
            panelId = PanelId.FriendList,
            needClose = true,
            needTab = false,
            stateName = "Chat",
            cellStateName = "ChatBtn",
            title = Language.LUA_CREATE_CHAT_PANEL_TITLE
        })







    end)

    self.m_getFriendCell = UIUtils.genCachedCellFunction(self.view.friendScrollList)

    self.view.friendScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_UpdateLeftFriendCell(self.m_getFriendCell(obj), csIndex)
    end)

    self.view.friendScrollList.onSelectedCell:AddListener(function(gameObject, csIndex)
        self:OnClickLeftFriendCell(csIndex)
    end)

    self:BindInputPlayerAction("common_cancel", function()
        self.view.friendDialogContent.view.friendDialogueSendArea.view.emotionListNavi:ManuallyStopFocus()
        self.view.friendDialogContent.view.friendDialogueSendArea:CloseAllSelectPanel(true)
    end, self.view.friendDialogContent.view.friendDialogueSendArea.view.emotionListGroupMono.groupId)

    self:BindInputPlayerAction("common_cancel", function()
        self.view.friendDialogContent.view.friendDialogueSendArea.view.textMessageListNavi:ManuallyStopFocus()
        self.view.friendDialogContent.view.friendDialogueSendArea:CloseAllSelectPanel(true)
    end, self.view.friendDialogContent.view.friendDialogueSendArea.view.textMessageListGroupMono.groupId)

    self:BindInputPlayerAction("common_cancel", function()
        self.view.friendDialogContent.view.friendDialogueSendArea.view.shareScrollListNavi:ManuallyStopFocus()
        self.view.friendDialogContent.view.friendDialogueSendArea:CloseAllSelectPanel(true)
    end, self.view.friendDialogContent.view.friendDialogueSendArea.view.shareScrollListGroupMono.groupId)


    self.messageSendAreaJumpIn = self:BindInputPlayerAction("friend_chat_send_area_jump_in", function()
        if not self.controllerInRightArea then
            self.view.rightAreaNaviGroup:ManuallyFocus()
        end
    end)


    self.messageSendAreaJumpOut = self:BindInputPlayerAction("friend_chat_send_area_jump_out", function()
        if self.controllerInRightArea then
            self.m_controllerBackFlag = true
            self.view.rightAreaNaviGroup:ManuallyStopFocus()
        end
    end, self.view.rightAreaGropuMono.groupId)


    self.messageSendAreaUseArrowJumpIn = self:BindInputPlayerAction("friend_chat_send_area_use_arrow_jump_in", function()
        if not self.controllerInRightArea then
            self.view.rightAreaNaviGroup:ManuallyFocus()
        end
    end)

    self.messageItemJumpIn = self:BindInputPlayerAction("friend_chat_message_item_jump_in", function()
        self.controllerInMessageItem = true
        self.view.friendDialogContent.m_executeJumpIn = true
        InputManagerInst:ToggleBinding(self.messageItemJumpOut, true)
        self.view.friendDialogContent.view.dialogScrollRect.controllerScrollEnabled = false
    end, self.view.rightAreaGropuMono.groupId)

    self.messageItemJumpOut = self:BindInputPlayerAction("friend_chat_message_item_jump_out", function()
        self:ExitMessageItemNaviGroup()
    end, self.view.friendDialogContent.view.dialogContentGroupMono.groupId)

    self.view.friendDialogContent.view.dialogContentNaviGroup.onDefaultNaviFailed:RemoveAllListeners()
    self.view.friendDialogContent.view.dialogContentNaviGroup.onDefaultNaviFailed:AddListener(function(dir)
        if dir == CS.UnityEngine.UI.NaviDirection.Up then
            self.view.friendDialogContent:ManuallyFocusMessageUp()
        elseif dir == CS.UnityEngine.UI.NaviDirection.Down then
            self.view.friendDialogContent:ManuallyFocusMessageDown()
        end
    end)

    if DeviceInfo.usingController then
        self.view.rightAreaNaviGroup.onDefaultNaviFailed:RemoveAllListeners()
        self.view.rightAreaNaviGroup.onDefaultNaviFailed:AddListener(function(dir)
            if dir == CS.UnityEngine.UI.NaviDirection.Left then
                if self.controllerInRightArea then
                    self.m_controllerBackFlag = true
                    self.view.rightAreaNaviGroup:ManuallyStopFocus()
                end
            end
        end)

        self.view.rightAreaNaviGroup.onIsFocusedChange:RemoveAllListeners()
        self.view.rightAreaNaviGroup.onIsFocusedChange:AddListener(function(isFocus)
            self.view.rightMask.gameObject:SetActive(isFocus)
            self.view.friendDialogContent.view.friendDialogueSendArea:CloseAllSelectPanel()
            self.controllerInMessageItem = false
            self.controllerInRightArea = isFocus

            if isFocus then
                InputManagerInst:ToggleBinding(self.messageSendAreaJumpOut, true)
            else
                InputManagerInst:ToggleBinding(self.messageSendAreaJumpOut, false)
            end
        end)
        self.view.friendDialogContent.view.dialogScrollRect.controllerScrollEnabled = true
    end

    InputManagerInst:ToggleBinding(self.messageSendAreaJumpIn, false)
    InputManagerInst:ToggleBinding(self.messageSendAreaJumpOut, false)
    InputManagerInst:ToggleBinding(self.messageSendAreaUseArrowJumpIn, false)

    InputManagerInst:ToggleBinding(self.messageItemJumpIn, false)
    InputManagerInst:ToggleBinding(self.messageItemJumpOut, false)

    self.m_infoTickHandle = LuaUpdate:Add("Tick", function(deltaTime)
        self:_UpdateInfoTick(deltaTime)
    end)

    if FriendUtils.isPsnPlatform() then
        GameInstance.player.friendChatSystem:QuestChatListSync()
    else
        self:_PreUpdateLeftFriendCell()
    end

end

SNSFriendCtrl._PreUpdateLeftFriendCell = HL.Method() << function(self)
    self.m_requestFriendInfoIds = {}
    self.m_requestInfoIndex = 1

    for i = 0, GameInstance.player.friendChatSystem.luaShowValidRoleIds.Count - 1 do
        local roleId = GameInstance.player.friendChatSystem.luaShowValidRoleIds[i]
        local success, friendInfo = GameInstance.player.friendSystem.friendInfoDic:TryGetValue(roleId)
        if not success or not friendInfo.init then
            table.insert(self.m_requestFriendInfoIds, roleId)
        end
    end

    local ids = self:_GetNextPageNotInitIds()

    if #ids > 0 then
        local friendDicIndex = FriendUtils.FRIEND_CELL_INIT_CONFIG.Friend.infoDicIndex
        GameInstance.player.friendSystem:SyncFriendInfo(friendDicIndex, ids, function()
            self.m_requestHandle = LuaUpdate:Add("Tick", function(deltaTime)
                self:_RequestTick(deltaTime)
            end)

            self:_UpdateFriendScrollList()
        end)
    else
        self:_UpdateFriendScrollList()
    end
end

SNSFriendCtrl._UpdateFriendScrollList = HL.Method() << function(self)
    self.m_csIndex2friendInfo = {}
    self.m_csIndex2friendCell = {}
    if not IsNull(self.view.friendScrollList) then
        self.view.friendScrollList.onGraduallyShowFinish:RemoveAllListeners()
        self.view.friendScrollList.onGraduallyShowFinish:AddListener(function()
            if GameInstance.player.friendChatSystem.luaShowValidRoleIds.Count > 0 then
                self.view.leftListEmpty.gameObject:SetActive(false)
                if self.m_nextOpenRoleId ~= -1 then
                    self:OpenFriendChatByRoleId(self.m_nextOpenRoleId)
                    self.m_nextOpenRoleId = -1
                elseif self.m_onPlayerDeleteListCell then
                    self.m_onPlayerDeleteListCell = false
                    if GameInstance.player.friendChatSystem.luaShowValidRoleIds.Count > self.m_selectedCsIndex then
                        self:OnClickLeftFriendCell(self.m_selectedCsIndex)
                    else
                        self:OnClickLeftFriendCell(self.m_selectedCsIndex - 1)
                    end

                    if self.controllerInRightArea then
                        self:_ExitRightAreaNaviGroup()
                    end
                elseif GameInstance.player.friendChatSystem:CheckValidChat(self.m_selectedRoleId)  then
                    if not self.m_onPlayerAddListCell  then
                        self:OpenFriendChatByRoleId(self.m_selectedRoleId)
                    end
                else
                    self.view.friendScrollList:ScrollToIndex(0, true)
                    self:OnClickLeftFriendCell(0)
                end

                if not self.controllerInRightArea then
                    InputManagerInst:ToggleBinding(self.messageSendAreaJumpIn, true)
                    InputManagerInst:ToggleBinding(self.messageSendAreaUseArrowJumpIn, true)
                end
            else
                self.m_selectedCsIndex = -1
                self.m_selectedRoleId = -1
                if self.controllerInRightArea then
                    self:_ExitRightAreaNaviGroup()
                end
                self.view.leftListEmpty.gameObject:SetActive(true)
                self.view.friendDialogContent:UpdateFriendInfo(nil)
                self.view.friendDialogContent:UpdateAllMessages("normal")
                InputManagerInst:ToggleBinding(self.messageSendAreaJumpIn, false)
                InputManagerInst:ToggleBinding(self.messageSendAreaUseArrowJumpIn, false)
            end
            self:UpdateNumRedDot()
        end)
        self.view.friendScrollList:UpdateCount(GameInstance.player.friendChatSystem.luaShowValidRoleIds.Count)
    end
end

SNSFriendCtrl.ExitMessageItemNaviGroup = HL.Method() << function(self)
    self.controllerInMessageItem = false
    self.view.friendDialogContent.view.dialogContentNaviGroup:ManuallyStopFocus()
    InputManagerInst:ToggleBinding(self.messageItemJumpOut, false)
    self.view.friendDialogContent.view.dialogScrollRect.controllerScrollEnabled = true
end

SNSFriendCtrl._ExitRightAreaNaviGroup = HL.Method() << function(self)
    self.view.rightMask.gameObject:SetActive(false)
    self.view.friendDialogContent.view.friendDialogueSendArea:CloseAllSelectPanel()
    self.controllerInMessageItem = false
    self.controllerInRightArea = false
    self.view.rightAreaNaviGroup:ManuallyStopFocus()
    InputManagerInst:ToggleBinding(self.messageSendAreaJumpOut, false)
end

SNSFriendCtrl._GetNextPageNotInitIds = HL.Method().Return(HL.Table) << function(self)
    local ids = {}
    for i = 1, RequestBatchNum do
        if self.m_requestInfoIndex <= #self.m_requestFriendInfoIds then
            table.insert(ids, self.m_requestFriendInfoIds[self.m_requestInfoIndex])
            self.m_requestInfoIndex = self.m_requestInfoIndex + 1
        end
    end

    return ids
end

SNSFriendCtrl._RequestTick = HL.Method(HL.Number) << function(self, deltaTime)
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

    local friendDicIndex = FriendUtils.FRIEND_CELL_INIT_CONFIG.Friend.infoDicIndex
    GameInstance.player.friendSystem:SyncFriendInfo(friendDicIndex, ids)
end

SNSFriendCtrl.GetRoleIdByCsIndex = HL.Method(HL.Number).Return(HL.Any) << function(self, csIndex)
    if csIndex < 0 then
        return nil
    end
    local chatRoleIds = GameInstance.player.friendChatSystem.luaShowValidRoleIds
    if chatRoleIds.Count > csIndex then
        return chatRoleIds[csIndex]
    end
    return nil
end

SNSFriendCtrl._UpdateLeftFriendCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    self.m_csIndex2friendCell[csIndex] = cell
    local chatRoleIds = GameInstance.player.friendChatSystem.luaShowValidRoleIds
    local roleId = chatRoleIds[csIndex]
    local success, friendInfo = GameInstance.player.friendSystem.friendInfoDic:TryGetValue(roleId)

    local onClickFun = function(clickIndex)
        if self.m_controllerBackFlag then
            self:OpenFriendChatByRoleId(self.m_selectedRoleId)
        else
            self:OnClickLeftFriendCell(clickIndex)
        end
    end

    if success and friendInfo.init then
        self.m_csIndex2friendInfo[csIndex] = friendInfo
        cell:InitContactFriendCell(roleId, friendInfo, csIndex, onClickFun)
    else
        cell:InitEmptyFriendCell(roleId, csIndex, onClickFun)
    end

    if csIndex == self.m_selectedCsIndex then
        cell.view.stateController:SetState(LeftFriendState.Selected)
    else
        cell.view.stateController:SetState(LeftFriendState.Normal)
    end

    if csIndex == 0 and not self.m_initSetTarget then
        self.m_initSetTarget = true
        self.view.rightMask.gameObject:SetActive(false)
        self:SetNaviTarget(cell.view.button)
    end
    cell.view.animationWrapper:PlayInAnimation()

end

SNSFriendCtrl.OnClickLeftFriendCell = HL.Method(HL.Number) << function(self, csIndex)
    if csIndex < 0 then
        csIndex = 0
    end
    if self:GetRoleIdByCsIndex(csIndex) == nil then
        return
    end

    if self.m_selectedRoleId == self:GetRoleIdByCsIndex(csIndex) and not self.m_onPlayerAddListCell then
        if self.m_selectedCsIndex ~= csIndex then
            self.m_selectedCsIndex = csIndex
            local cell = self.m_csIndex2friendCell[csIndex]
            if cell == nil then
                return
            end

            for key, value in pairs(self.m_csIndex2friendCell) do
                value.view.stateController:SetState(LeftFriendState.Normal)
            end

            cell.view.stateController:SetState(LeftFriendState.Selected)

            if DeviceInfo.usingController then
                if not self.controllerInRightArea and not self.controllerInMessageItem then
                    local selectCell = self.m_csIndex2friendCell[self.m_selectedCsIndex]
                    if selectCell then
                        self.view.rightMask.gameObject:SetActive(false)
                        self:SetNaviTarget(selectCell.view.button)
                    end
                end
            end
        end

        if self.m_controllerBackFlag then
            if DeviceInfo.usingController then
                local selectCell = self.m_csIndex2friendCell[self.m_selectedCsIndex]
                if selectCell then
                    self.view.rightMask.gameObject:SetActive(false)
                    self:SetNaviTarget(selectCell.view.button)
                end
            end
            self.m_controllerBackFlag = false
        end
        return
    end

    self.view.friendDialogContent.view.animationWrapper:PlayInAnimation()

    self.m_selectedCsIndex = csIndex
    self.m_selectedRoleId = self:GetRoleIdByCsIndex(csIndex)

    local cell = self.m_csIndex2friendCell[csIndex]
    if cell == nil then
        return
    end
    self.m_onPlayerAddListCell = false

    for key, value in pairs(self.m_csIndex2friendCell) do
        value.view.stateController:SetState(LeftFriendState.Normal)
    end

    cell.view.stateController:SetState(LeftFriendState.Selected)

    local chatRoleIds = GameInstance.player.friendChatSystem.luaShowValidRoleIds
    local roleId = chatRoleIds[csIndex]
    local success, friendInfo = GameInstance.player.friendSystem.friendInfoDic:TryGetValue(roleId)
    if success then
        self.view.friendDialogContent:UpdateFriendInfo(friendInfo)
    else
        self.view.friendDialogContent:UpdateFriendInfo(nil)
    end

    if DeviceInfo.usingController then
        if not self.controllerInRightArea and not self.controllerInMessageItem then
            local selectCell = self.m_csIndex2friendCell[self.m_selectedCsIndex]
            if selectCell then
                self.view.rightMask.gameObject:SetActive(false)
                self:SetNaviTarget(selectCell.view.button)
            end
        end
    end

    local chatInfo = GameInstance.player.friendChatSystem:GetChatInfo(roleId)
    if chatInfo then
        if chatInfo.shouldQuestMessage then
            self.view.friendDialogContent.m_nextToBottomRoleId = roleId
            GameInstance.player.friendChatSystem:QueryChatContent(roleId)
        else
            self.view.friendDialogContent:UpdateAllMessages("normal")
        end
    end
end


SNSFriendCtrl.OpenFriendChatByRoleId = HL.Method(HL.Any) << function(self, openRoleId)
    local openIndex = -1
    for i = 0, GameInstance.player.friendChatSystem.luaShowValidRoleIds.Count - 1 do
        if openRoleId == GameInstance.player.friendChatSystem.luaShowValidRoleIds[i] then
            openIndex = i
        end
    end
    if openIndex ~= -1 then
        self.view.friendScrollList:ScrollToIndex(openIndex, true)
        self:OnClickLeftFriendCell(openIndex)
    else
        if GameInstance.player.friendChatSystem.luaShowValidRoleIds.Count > 0 then
            self.view.friendScrollList:ScrollToIndex(0, true)
            self:OnClickLeftFriendCell(0)
        end
    end
end

SNSFriendCtrl.OnMsgReadChange = HL.Method(HL.Any) << function(self, args)
    self:UpdateNumRedDot()
end

SNSFriendCtrl.OnFriendRemakeNameModify = HL.Method() << function(self)
    if not self.m_csIndex2friendCell then
        return
    end

    for csIndex, cell in pairs(self.m_csIndex2friendCell) do
        cell:UpdateTileTxt()
    end
    self.view.friendDialogContent:OnFriendRemakeNameModify()
end


SNSFriendCtrl.OnPlayerDeleteListCell = HL.Method() << function(self)
    self.m_onPlayerDeleteListCell = true
end

SNSFriendCtrl.OnPlayerAddListCell = HL.Method() << function(self)
    self.m_onPlayerAddListCell = true
end

SNSFriendCtrl.OnSwitchOn = HL.Method(HL.Boolean) << function(self, isOn)
    if DeviceInfo.usingController then
        if not self.m_csIndex2friendCell then
            return
        end

        if isOn and self.m_selectedCsIndex ~= nil then
            local selectCell = self.m_csIndex2friendCell[self.m_selectedCsIndex]
            if selectCell then
                self:SetNaviTarget(selectCell.view.button)
            end
        end

        if isOn then

            local phase = self.m_phase
            phase:ToggleBasicPanelCloseBtn(true)
        end
    end
end

SNSFriendCtrl.UpdateNumRedDot = HL.Method() << function(self)
    if not self.m_csIndex2friendCell then
        return
    end

    local res = self.view.friendScrollList:GetShowRange()
    local showRangeX = res.x
    local showRangeY = res.y
    if showRangeX >= 0 and showRangeY >= 0 then
        for csIndex = showRangeX, showRangeY do
            local cell = self.m_csIndex2friendCell[csIndex]
            if cell then
                local chatRoleIds = GameInstance.player.friendChatSystem.luaShowValidRoleIds
                if chatRoleIds.Count > csIndex then
                    local roleId = chatRoleIds[csIndex]
                    local success, friendInfo = GameInstance.player.friendSystem.friendInfoDic:TryGetValue(roleId)
                    if success and friendInfo.init then
                        local chatInfo = GameInstance.player.friendChatSystem:GetChatInfo(roleId)
                        if chatInfo and self.m_selectedRoleId ~= self:GetRoleIdByCsIndex(csIndex) and chatInfo.unReadNum > 0 then
                            cell.view.redDotLayout.gameObject:SetActive(true)
                            cell.view.redDotTxt.text = chatInfo.unReadNum
                        else
                            cell.view.redDotLayout.gameObject:SetActive(false)
                        end
                    else
                        cell.view.redDotLayout.gameObject:SetActive(false)
                    end
                end
            end
        end
    end
end

SNSFriendCtrl.OnClickOpenFriendChat = HL.Method(HL.Any) << function(self, args)
    local openRoleId = unpack(args)

    if self.m_selectedRoleId ~= openRoleId then
        self.m_nextOpenRoleId = openRoleId
    end
end


SNSFriendCtrl.OnSyncFriendChatAllInfo = HL.Method() << function(self)
    self:_PreUpdateLeftFriendCell()
end

SNSFriendCtrl.OnShow = HL.Override() << function(self)
    SNSFriendCtrl.Super.OnShow(self)
end


SNSFriendCtrl._UpdateInfoTick = HL.Method(HL.Number) << function(self, deltaTime)

    if self.controllerInRightArea and not self.controllerInMessageItem
        and self.view.friendDialogContent.m_canJumpIn then
        InputManagerInst:ToggleBinding(self.messageItemJumpIn, true)
    else
        InputManagerInst:ToggleBinding(self.messageItemJumpIn, false)
    end
end

SNSFriendCtrl.OnShow = HL.Override() << function(self)
    self.view.friendDialogContent:CustomOnShow()
end

SNSFriendCtrl.OnHide = HL.Override() << function(self)
    self.view.friendDialogContent:CustomOnHide()
end

SNSFriendCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendChatSystem:ClearLuaShowRoleId()
    if self.m_requestHandle > 0 then
        self.m_requestHandle = LuaUpdate:Remove(self.m_requestHandle)
    end

    self.view.friendDialogContent:CustomOnClose()
    if self.m_infoTickHandle > 0 then
        self.m_infoTickHandle = LuaUpdate:Remove(self.m_infoTickHandle)
    end
    GameInstance.player.friendChatSystem:SaveRecentTextUniq()
    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(SNSFriendCtrl)
