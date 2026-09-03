local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FriendDialogueSendArea = HL.Class('FriendDialogueSendArea', UIWidgetBase)

FriendDialogueSendArea.friendDialogContent = HL.Field(HL.Any)

FriendDialogueSendArea.m_getTextTabCell = HL.Field(HL.Function)

FriendDialogueSendArea.m_getTextMessageCell = HL.Field(HL.Function)

FriendDialogueSendArea.m_getEmotionTabCell = HL.Field(HL.Function)

FriendDialogueSendArea.m_getEmotionListCell = HL.Field(HL.Function)

FriendDialogueSendArea.m_getShareCell = HL.Field(HL.Function)

FriendDialogueSendArea.m_openTextMessage = HL.Field(HL.Boolean) << false

FriendDialogueSendArea.m_openEmoticon = HL.Field(HL.Boolean) << false

FriendDialogueSendArea.m_openShare = HL.Field(HL.Boolean) << false

FriendDialogueSendArea.m_showRoleId = HL.Field(HL.Number) << 0

FriendDialogueSendArea.friendChatSystem = HL.Field(HL.Any)

FriendDialogueSendArea.m_csIndex2textTabCell = HL.Field(HL.Table)

FriendDialogueSendArea.m_curTextTabCsIndex = HL.Field(HL.Number) << -1

FriendDialogueSendArea.m_curTextTabFocusCsIndex = HL.Field(HL.Number) << -1

FriendDialogueSendArea.m_csIndex2emotionTabCell = HL.Field(HL.Table)

FriendDialogueSendArea.m_recentTextUniqList = HL.Field(HL.Table)

FriendDialogueSendArea.m_curEmotionTabCsIndex = HL.Field(HL.Number) << -1

FriendDialogueSendArea.m_curEmotionTabFocusCsIndex = HL.Field(HL.Number) << -1

FriendDialogueSendArea.m_backBinding = HL.Field(HL.Number) << -1

FriendDialogueSendArea.m_recordTab2TextCsIndex = HL.Field(HL.Table)

FriendDialogueSendArea.m_recordTab2EmotionCsIndex = HL.Field(HL.Table)

FriendDialogueSendArea.m_recordShareIndex = HL.Field(HL.Number) << -1


local TextMessageTabState = {
    Selected = "Selected",
    Normal = "Normal"
}

local EmotionTabState = {
    Selected = "Selected",
    Normal = "Normal"
}

local PanelBtnState = {
    Selected = "Selected",
    Normal = "Normal"
}

FriendDialogueSendArea.InitFriendDialogueSendArea = HL.Method(HL.Any) << function(self, friendDialogContent)
    self.friendDialogContent = friendDialogContent
    self.friendChatSystem = GameInstance.player.friendChatSystem
    self.m_csIndex2textTabCell = {}
    self.m_csIndex2emotionTabCell = {}


    self.m_recordTab2TextCsIndex = {}
    self.m_recordTab2EmotionCsIndex = {}
    self.m_recordShareIndex = 0

    self:_FirstTimeInit()
    self:CloseAllSelectPanel()
    self:UpdateTextMessagePanelBind()
    self:UpdateEmotionPanelBind()
    self:UpdateSharePanelBind()

    self.view.btnTextMessage.onClick:RemoveAllListeners()
    self.view.btnTextMessage.onClick:AddListener(function()
        self:_ClickTextMessage()
    end)

    self.view.btnEmotion.onClick:RemoveAllListeners()
    self.view.btnEmotion.onClick:AddListener(function()
        self:_ClickEmoticon()
    end)

    self.view.btnShare.onClick:RemoveAllListeners()
    self.view.btnShare.onClick:AddListener(function()
        self:_ClickShare()
    end)

    self.view.bgTouch.onClick:RemoveAllListeners()
    self.view.bgTouch.onClick:AddListener(function()
        self:CloseAllSelectPanel(true)
    end)
end

FriendDialogueSendArea.UpdateShowRoleId = HL.Method(HL.Number) << function(self, roleId)
    self.m_showRoleId = roleId
end

FriendDialogueSendArea.CheckOpenSelectPanel = HL.Method().Return(HL.Boolean)<< function(self)
    return self.m_openTextMessage or self.m_openEmoticon or self.m_openShare
end

FriendDialogueSendArea._ClickTextMessage = HL.Method() << function(self)
    if self.m_openTextMessage then
        self:CloseAllSelectPanel(true)
    else
        AudioAdapter.PostEvent("Au_UI_Popup_Common_Small_Open")
        self:CloseAllSelectPanel()
        self.friendDialogContent.view.dialogScrollRect.controllerScrollEnabled = false
        self.friendDialogContent.view.nameDetailsBtnKeyHint.gameObject:SetActive(false)
        self.friendDialogContent.view.nameDetailsBtn.enabled = false
        local scrollTo = self.m_curTextTabCsIndex
        if self.m_curTextTabCsIndex == -1 then
            scrollTo = 0
        end
        self.view.textMessagePanel.textTabScrollList:UpdateCount(self.friendChatSystem.m_chatTabNames.Count + 1, scrollTo)
        self.view.textMessagePanel.gameObject:SetActive(true)
        self.view.btnTextMessageStateController:SetState(PanelBtnState.Selected)
        self:_SelectTextTab(self.m_curTextTabCsIndex)
        self.view.bgTouch.gameObject:SetActive(true)
        self.m_openTextMessage = true
    end
end

FriendDialogueSendArea._ClickEmoticon = HL.Method() << function(self)
    if self.m_openEmoticon then
        self:CloseAllSelectPanel(true)
    else
        AudioAdapter.PostEvent("Au_UI_Popup_Common_Small_Open")
        self:CloseAllSelectPanel()
        self.friendDialogContent.view.dialogScrollRect.controllerScrollEnabled = false
        self.friendDialogContent.view.nameDetailsBtnKeyHint.gameObject:SetActive(false)
        self.friendDialogContent.view.nameDetailsBtn.enabled = false
        local scrollTo = self.m_curEmotionTabCsIndex
        if self.m_curEmotionTabCsIndex == -1 then
            scrollTo = 0
        end
        self.view.emotionPanel.emotionTabScrollList:UpdateCount(self.friendChatSystem.m_emotionTabNames.Count, scrollTo)
        self.view.emotionPanel.gameObject:SetActive(true)
        self.view.btnEmotionStateController:SetState(PanelBtnState.Selected)
        self:_SelectEmotionTab(self.m_curEmotionTabCsIndex)
        self.view.bgTouch.gameObject:SetActive(true)
        self.m_openEmoticon = true
    end
end

FriendDialogueSendArea._ClickShare = HL.Method() << function(self)
    if self.m_openShare then
        self:CloseAllSelectPanel(true)
    else
        AudioAdapter.PostEvent("Au_UI_Popup_Common_Small_Open")
        self:CloseAllSelectPanel()
        self.friendDialogContent.view.dialogScrollRect.controllerScrollEnabled = false
        self.friendDialogContent.view.nameDetailsBtnKeyHint.gameObject:SetActive(false)
        self.friendDialogContent.view.nameDetailsBtn.enabled = false
        self.view.sharePanel.gameObject:SetActive(true)
        self.view.btnShareStateController:SetState(PanelBtnState.Selected)
        self.view.bgTouch.gameObject:SetActive(true)
        self.m_openShare = true
        self.view.sharePanel.shareScrollList:UpdateCount(2)
    end
end

FriendDialogueSendArea.CloseAllSelectPanel = HL.Method(HL.Opt(HL.Boolean)) << function(self, playerAudio)
    local playerCloseAudio = playerAudio == true
    self.friendDialogContent.view.dialogScrollRect.controllerScrollEnabled = true
    self.friendDialogContent.view.nameDetailsBtnKeyHint.gameObject:SetActive(true)
    self.friendDialogContent.view.nameDetailsBtn.enabled = true
    self.view.bgTouch.gameObject:SetActive(false)

    if self.m_openTextMessage then
        if playerCloseAudio then
            AudioAdapter.PostEvent("Au_UI_Popup_Common_Small_Close")
        end
        self.view.textMessageListNavi:ManuallyStopFocus()
    end
    if self.m_openEmoticon then
        if playerCloseAudio then
            AudioAdapter.PostEvent("Au_UI_Popup_Common_Small_Close")
        end
        self.view.emotionListNavi:ManuallyStopFocus()
    end
    if self.m_openShare then
        if playerCloseAudio then
            AudioAdapter.PostEvent("Au_UI_Popup_Common_Small_Close")
        end
        self.view.shareScrollListNavi:ManuallyStopFocus()
    end

    self.view.textMessagePanel.gameObject:SetActive(false)
    self.m_openTextMessage = false
    self.view.emotionPanel.gameObject:SetActive(false)
    self.m_openEmoticon = false
    self.view.sharePanel.gameObject:SetActive(false)
    self.m_openShare = false
    self.view.btnTextMessageStateController:SetState(PanelBtnState.Normal)
    self.view.btnEmotionStateController:SetState(PanelBtnState.Normal)
    self.view.btnShareStateController:SetState(PanelBtnState.Normal)
    Notify(MessageConst.FAC_ON_UPDATE_FRIEND_CHAT_STATE, false)
end



FriendDialogueSendArea.UpdateTextMessagePanelBind = HL.Method() << function(self)
    self.m_getTextTabCell = UIUtils.genCachedCellFunction(self.view.textMessagePanel.textTabScrollList)
    self.m_getTextMessageCell = UIUtils.genCachedCellFunction(self.view.textMessagePanel.textMessageList)
    self.m_csIndex2textTabCell = {}
    self.view.textMessagePanel.textTabScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateTextTabCell(self.m_getTextTabCell(obj), csIndex)
    end)
    self.view.textMessagePanel.textTabScrollList.onCellSelectedChanged:AddListener(function(obj, csIndex, isSelected)
        if isSelected then
            AudioAdapter.PostEvent("Au_UI_Toggle_Type_On")
            self:_SelectTextTab(csIndex)
        end
    end)

    self.view.textMessagePanel.textMessageList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getTextMessageCell(obj)
        if self.m_curTextTabCsIndex == 0 then
            local uniqId = GameInstance.player.friendChatSystem:GetRecentTextUniqByIndex(csIndex)
            local haveInfo, info = GameInstance.player.friendChatSystem.m_uniq2chatText:TryGetValue(uniqId)
            if haveInfo then
                local haveText, tableData = Tables.friendChatTextTable:TryGetValue(info.textName)
                if haveText then
                    cell.messageTxt.text = tableData.messageText
                    cell.button.onClick:RemoveAllListeners()
                    cell.button.onClick:AddListener(function()
                        GameInstance.player.friendChatSystem:SendChatText(self.m_showRoleId, uniqId)
                        self:CloseAllSelectPanel()
                    end)
                else
                    cell.messageTxt.text = ""
                    cell.button.onClick:RemoveAllListeners()
                end
            end
        else
            local tabName = self.friendChatSystem.m_index2chatTabName[self.m_curTextTabCsIndex]
            local succ, chatTexts = self.friendChatSystem.m_tab2chatTexts:TryGetValue(tabName)
            if succ and chatTexts.Count > csIndex then
                local uniqId = chatTexts[csIndex].uniqId

                local haveText, tableData = Tables.friendChatTextTable:TryGetValue(chatTexts[csIndex].textName)
                if haveText then
                    cell.messageTxt.text = tableData.messageText
                    cell.button.onClick:RemoveAllListeners()
                    cell.button.onClick:AddListener(function()
                        GameInstance.player.friendChatSystem:SendChatText(self.m_showRoleId, uniqId)
                        GameInstance.player.friendChatSystem:AddRecentTextUniq(uniqId)
                        self:CloseAllSelectPanel()
                    end)
                else
                    cell.button.onClick:RemoveAllListeners()
                end
            end
        end







        if csIndex == self.m_curTextTabFocusCsIndex then
            self:SetNaviTarget(cell.button)
        end
    end)
end

FriendDialogueSendArea._SelectTextTab = HL.Method(HL.Number) << function(self, csIndex)
    if csIndex == -1 then
        if GameInstance.player.friendChatSystem.recentValidTextNum > 0 then
            csIndex = 0
        else
            csIndex = 1
        end
    end

    for key, value in pairs(self.m_csIndex2textTabCell) do
        value.tabStateController:SetState(TextMessageTabState.Normal)
    end

    local cell = self.m_csIndex2textTabCell[csIndex]
    cell.tabStateController:SetState(TextMessageTabState.Selected)

    if csIndex == 0 then
        self.m_curTextTabCsIndex = csIndex
        self.m_curTextTabFocusCsIndex = 0
        self.view.textMessagePanel.textMessageList:UpdateCount(GameInstance.player.friendChatSystem.recentValidTextNum, 0)

        if GameInstance.player.friendChatSystem.recentValidTextNum > 0 then
            self.view.textMessagePanel.textMessageEmptyNode.gameObject:SetActive(false)
        else
            self.view.textMessagePanel.textMessageEmptyNode.gameObject:SetActive(true)
        end
    else
        local tabName = self.friendChatSystem.m_index2chatTabName[csIndex]
        local succ, chatTexts = self.friendChatSystem.m_tab2chatTexts:TryGetValue(tabName)
        if succ then
            self.m_curTextTabCsIndex = csIndex
            cell.redDot.gameObject:SetActive(false)
            self.friendChatSystem:SetTextTabRedDotRecord(tabName)

            self.m_curTextTabFocusCsIndex = 0







            self.view.textMessagePanel.textMessageList:UpdateCount(chatTexts.Count, self.m_curTextTabFocusCsIndex)
        end

        if chatTexts.Count > 0 then
            self.view.textMessagePanel.textMessageEmptyNode.gameObject:SetActive(false)
        else
            self.view.textMessagePanel.textMessageEmptyNode.gameObject:SetActive(true)
        end
    end
end

FriendDialogueSendArea._OnUpdateTextTabCell = HL.Method(HL.Table, HL.Number) << function(self, cell, csIndex)

    if csIndex == 0 then
        cell.tabSelectedTxt.text = Language.LUA_FRIEND_TEXT_RECENT_TAB_TEXT
        cell.tabNormalTxt.text = Language.LUA_FRIEND_TEXT_RECENT_TAB_TEXT
        cell.redDot.gameObject:SetActive(false)
    else
        local tabName = self.friendChatSystem.m_index2chatTabName[csIndex]
        local success, tableData = Tables.FriendChatTabTextTable:TryGetValue(tabName)
        if success then
            cell.tabSelectedTxt.text = tableData.tabText
            cell.tabNormalTxt.text = tableData.tabText
            cell.redDot.gameObject:SetActive(not self.friendChatSystem:GetTextTabRedDotRecord(tabName))
        else
            cell.tabSelectedTxt.text = ""
            cell.tabNormalTxt.text = ""
            cell.redDot.gameObject:SetActive(false)
        end
    end

    self.m_csIndex2textTabCell[csIndex] = cell

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if self.m_curTextTabCsIndex == csIndex then
            return
        end

        AudioAdapter.PostEvent("Au_UI_Toggle_Type_On")
        self:_SelectTextTab(csIndex)
    end)
end







FriendDialogueSendArea.UpdateEmotionPanelBind = HL.Method() << function(self)

    self.view.emotionPanel.emoticonEmptyNode.gameObject:SetActive(false)

    self.m_getEmotionTabCell = UIUtils.genCachedCellFunction(self.view.emotionPanel.emotionTabScrollList)
    self.m_getEmotionListCell = UIUtils.genCachedCellFunction(self.view.emotionPanel.emotionList)

    self.view.emotionPanel.emotionTabScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateEmotionTab(self.m_getEmotionTabCell(obj), csIndex)
    end)
    self.view.emotionPanel.emotionTabScrollList.onCellSelectedChanged:AddListener(function(obj, csIndex, isSelected)
        if isSelected then
            AudioAdapter.PostEvent("Au_UI_Toggle_Type_On")
            self:_SelectEmotionTab(csIndex)
        end
    end)

    self.view.emotionPanel.emotionList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getEmotionListCell(obj)
        local tabName = self.friendChatSystem.m_index2emotionTabName[self.m_curEmotionTabCsIndex]

        local succ, chatEmotions = self.friendChatSystem.m_tab2chatEmotions:TryGetValue(tabName)
        if succ then
            local emotionImgPath = chatEmotions[csIndex].emotionImgPath
            local uniqId = chatEmotions[csIndex].uniqId
            cell.iconImg:LoadSprite(emotionImgPath)
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                GameInstance.player.friendChatSystem:SendChatEmoji(self.m_showRoleId, uniqId)
                self:CloseAllSelectPanel()
            end)
        end







        if csIndex == self.m_curEmotionTabFocusCsIndex then
            self:SetNaviTarget(cell.button)
        end
    end)

end

FriendDialogueSendArea._OnUpdateEmotionTab = HL.Method(HL.Table, HL.Number) << function(self, cell, csIndex)

    local tabName = self.friendChatSystem.m_index2emotionTabName[csIndex]

    local success, tableData = Tables.friendChatTabEmotionTable:TryGetValue(tabName)
    if success then
        cell.tabSelectedIcon:LoadSprite(tableData.tabImgPath)
        cell.tabNormalIcon:LoadSprite(tableData.tabImgPath)
    end

    cell.redDot.gameObject:SetActive(not self.friendChatSystem:GetEmotionTabRedDotRecord(tabName))
    self.m_csIndex2emotionTabCell[csIndex] = cell
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if self.m_curEmotionTabCsIndex == csIndex then
            return
        end
        AudioAdapter.PostEvent("Au_UI_Toggle_Type_On")
        self:_SelectEmotionTab(csIndex)
    end)
end

FriendDialogueSendArea._SelectEmotionTab = HL.Method(HL.Number) << function(self, csIndex)
    if csIndex == -1 then
        csIndex = 0
    end

    for key, value in pairs(self.m_csIndex2emotionTabCell) do
        value.tabStateController:SetState(EmotionTabState.Normal)
    end

    local cell = self.m_csIndex2emotionTabCell[csIndex]
    cell.tabStateController:SetState(EmotionTabState.Selected)

    local tabName = self.friendChatSystem.m_index2emotionTabName[csIndex]
    local succ, chatTexts = self.friendChatSystem.m_tab2chatEmotions:TryGetValue(tabName)
    if succ then
        self.m_curEmotionTabCsIndex = csIndex
        cell.redDot.gameObject:SetActive(false)
        self.friendChatSystem:SetEmotionTabRedDotRecord(tabName)

        self.m_curEmotionTabFocusCsIndex = 0







        self.view.emotionPanel.emotionList:UpdateCount(chatTexts.Count, self.m_curEmotionTabFocusCsIndex)
    end
end




FriendDialogueSendArea.UpdateSharePanelBind = HL.Method() << function(self)
    self.m_getShareCell = UIUtils.genCachedCellFunction(self.view.sharePanel.shareScrollList)
    self.view.sharePanel.shareScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getShareCell(obj)
        local luaIndex = LuaIndex(csIndex)
        if luaIndex == 1 then
            cell.shareTitle.text = Language.LUA_FRIEND_SHARE_PANEL_BLUE_PRINT
            cell.shareIcon:LoadSprite("SNS/Friend/sns_friend_share_blueprint")
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                self:CloseAllSelectPanel()
                self:_SelectBluePrint()
            end)

        elseif luaIndex == 2 then
            cell.shareTitle.text = Language.LUA_FRIEND_SHARE_PANEL_PLACEHOLDER
            cell.shareIcon:LoadSprite("SNS/Friend/sns_friend_share_placeholder")
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                self:CloseAllSelectPanel()
                self:_SelectSocialBuilding()
            end)
        end

        cell.button.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.m_recordShareIndex = csIndex
            end
        end

        if csIndex == self.m_recordShareIndex then
            self:SetNaviTarget(cell.button)
        end
    end)
end

FriendDialogueSendArea._SelectBluePrint = HL.Method() << function(self)
    if not GameInstance.player.systemUnlockManager:IsSystemUnlockByType(GEnums.UnlockSystemType.FacBlueprint) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAT_JUMP_UNLOCK_BLUEPRINT_TOAST)
        return
    end
    if PhaseManager:IsOpen(PhaseId.FacBlueprint) then
        PhaseManager:ExitPhaseFast(PhaseId.FacBlueprint)
    end
    PhaseManager:GoToPhase(PhaseId.FacBlueprint,{ friendSharing = true, roleId = self.m_showRoleId })
end

FriendDialogueSendArea._SelectSocialBuilding = HL.Method() << function(self)
    if not GameInstance.player.systemUnlockManager:IsSystemUnlockByType(GEnums.UnlockSystemType.FacSocial) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAT_JUMP_UNLOCK_SOCIAL_BUILDING_TOAST)
        return
    end
    UIManager:AutoOpen(PanelId.FacMarkerManagePopup, { roleId = self.m_showRoleId })
end




HL.Commit(FriendDialogueSendArea)
return FriendDialogueSendArea

