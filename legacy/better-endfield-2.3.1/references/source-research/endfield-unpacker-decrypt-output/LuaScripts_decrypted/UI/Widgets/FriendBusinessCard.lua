local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FriendBusinessCard = HL.Class('FriendBusinessCard', UIWidgetBase)

FriendBusinessCard.m_roleId = HL.Field(HL.Number) << 0

FriendBusinessCard.m_preview = HL.Field(HL.Boolean) << false

FriendBusinessCard.m_hideUI = HL.Field(HL.Boolean) << false

FriendBusinessCard.m_isExpanded = HL.Field(HL.Boolean) << false

FriendBusinessCard.m_themeChange = HL.Field(HL.Boolean) << false

FriendBusinessCard.m_bId = HL.Field(HL.Any)


FriendBusinessCard._OnFirstTimeInit = HL.Override() << function(self)
    self.view.bgCloseBtn.onClick:RemoveAllListeners()
    self.view.bgCloseBtn.onClick:AddListener(function()
        if self.m_hideUI then
            self.m_hideUI = false
            self:_UpdateAllInfo()
            return
        end

        local success, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_roleId)
        if not success then
            logger.error("FriendBusinessCard.InitFriendBusinessCard 失败！因为没有找到好友信息！")
            return
        end

        if self.m_preview and not self.m_themeChange then
            self.m_isExpanded = false
            self.view.businessCardState:SetState(self.m_isExpanded and 'Expand' or 'Close')
            self.view.bgCloseBtn.gameObject:SetActiveIfNecessary(self.m_isExpanded or self.m_hideUI)
            if not (self.m_themeChange or self.m_roleId == GameInstance.player.roleId) then
                self.view.closeNode:PlayInAnimation()
            end
            return
        end

        if not friendInfo.expandFlag then
            return
        end

        if self.m_roleId == GameInstance.player.roleId then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_BUSINESS_CARD_STATE_CHANGE_COLLECTION)
            GameInstance.player.friendSystem:ExpandFlagModify(false)
        end

    end)

    self.view.arrowBtn.onClick:RemoveAllListeners()
    self.view.arrowBtn.onClick:AddListener(function()
        if self.m_preview and not self.m_themeChange then
            self.m_isExpanded = true
            self.view.businessCardState:SetState(self.m_isExpanded and 'Expand' or 'Close')
            self.view.bgCloseBtn.gameObject:SetActiveIfNecessary(self.m_isExpanded or self.m_hideUI)
            if self.m_isExpanded then
                self.view.rightNode:PlayInAnimation(function()
                    self.view.businessCardRoleNode:NaviToFirstChar()
                end)
            end
            return
        end

        Notify(MessageConst.SHOW_TOAST, Language.LUA_BUSINESS_CARD_STATE_CHANGE_NORMAL)
        GameInstance.player.friendSystem:ExpandFlagModify(true)
    end)

    self.view.themeChangeBtn.onClick:RemoveAllListeners()
    self.view.themeChangeBtn.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.FriendThemeChange)
    end)
    self.view.themeChangeRedDot:InitRedDot("NewBusinessCard", "")

    self.view.applyBtn.onClick:RemoveAllListeners()
    self.view.applyBtn.onClick:AddListener(function()
        if GameInstance.player.friendSystem:PlayerInBlackList(self.m_roleId) then
            local errorMsg = Tables.errorCodeTable:GetValue(1065)
            Notify(MessageConst.SHOW_TOAST, errorMsg.text)
            return
        end

        local stack = PhaseManager:GetPhaseStack()
        local phaseId = ""
        for i = 0, stack:Count() - 1 do
            local item = stack:Get(stack:TopIndex() - i)
            if item.phaseId ~= PhaseId.FriendBusinessCardPreview then
                phaseId = PhaseManager:GetPhaseName(item.phaseId)
                break
            end
        end
        local panelId = ""
        if phaseId == "Friend" then
            panelId = UIManager:IsOpen(PanelId.SearchNewFriendList) and "SearchNewFriendList" or "StrangerList"
        end
        GameInstance.player.friendSystem:AddFriend(self.m_roleId, phaseId , panelId, tostring(self.m_bId))
    end)

    self.view.shareBtn.onClick:RemoveAllListeners()
    self.view.shareBtn.onClick:AddListener(function()
        if GameInstance.player.friendSystem.isCommunicationRestricted then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TIP_PARENTAL_CONTROL_BUSINESS_CARD)
            return
        end
        UIManager:Open(PanelId.FriendBusinessCardPreview, { roleId = self.m_roleId, forceShare = true })
    end)
    if GameInstance.player.friendSystem.isCommunicationRestricted then
        self.view.shareBtn.gameObject:SetActive(false)
    end

    self.view.hideBtn.onClick:RemoveAllListeners()
    self.view.hideBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.FriendBusinessCardPreview, { roleId = self.m_roleId, fullScreen = false })
    end)

    self.view.signBtn.onClick:RemoveAllListeners()
    self.view.signBtn.onClick:AddListener(function()
        if self.m_preview or GameInstance.player.friendSystem.isCommunicationRestricted then
            return
        end

        FriendUtils.FRIEND_CELL_HEAD_FUNC.SIGNATURE_MODIFY().action()
    end)
end

FriendBusinessCard.InitFriendBusinessCard = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean, HL.String)) << function(self, roleId, preview, forceShare, themeChange, topicId)
    self:_FirstTimeInit()

    self.m_roleId = roleId
    if preview == nil then
        preview = roleId ~= GameInstance.player.roleId
    end
    self.m_themeChange = themeChange == true
    self.m_preview = preview
    InputManagerInst:ToggleGroup(self.view.inputBindingGroupMonoTarget.groupId, not themeChange)

    if forceShare then
        self.view.animationWrapper:SampleClip(self.view.animationWrapper.animationIn.name, self.view.animationWrapper.animationIn.length);
        self.view.animationWrapper:SampleClip(self.view.animationWrapper.animationInEasing.name, self.view.animationWrapper.animationInEasing.length);
        self.view.arrowBtn.gameObject:SetActive(false)
        self.view.personalInfoNode.view.playerUidTxtButton.gameObject:SetActive(false)
    end

    local success, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)

    if not success then
        logger.error("FriendBusinessCard.InitFriendBusinessCard 失败！因为没有找到好友信息！")
        return
    end
    local cfg = Tables.businessCardTopicTable:GetValue(themeChange == true and topicId or friendInfo.businessCardTopicId)
    if themeChange then
        self.m_isExpanded = cfg.expand
    else
        self.m_isExpanded = friendInfo.expandFlag
    end

    self:_UpdateAllInfo()

    self.view.businessCardState:SetState(self.m_isExpanded and 'Expand' or 'Close')
    if not forceShare then
        if self.m_isExpanded then
            if themeChange then
                self.view.businessCardRoleNode:NaviToFirstChar()
            else
                self.view.rightNode:PlayInAnimation(function()
                    self.view.businessCardRoleNode:NaviToFirstChar()
                end)
            end
        else
            if not (themeChange or self.m_roleId == GameInstance.player.roleId) then
                self.view.closeNode:PlayInAnimation()
            end
        end
    end


    if GameInstance.player.friendSystem:PlayerInBlackList(roleId) then
        logger.error("FriendBusinessCard.InitFriendBusinessCard 该玩家在黑名单中，roleId : " .. roleId)
        self.view.applyBtn.gameObject:SetActive(false)
    end
end

FriendBusinessCard._UpdateAllInfo = HL.Method() << function(self)


    local roleType = GameInstance.player.friendSystem:GetRoleTypeByRoleId(self.m_roleId)
    local stateName = roleType:ToString()
    if roleType == CS.Beyond.Gameplay.RoleType.Self and (self.m_preview or self.m_hideUI) then
        stateName = stateName .. 'Preview'
    end

    self.view.businessCardState:SetState(stateName)

    self.view.personalInfoNode:InitBusinessCardPersonalInfoNodeByRoleId(self.m_roleId, self.m_preview or self.m_hideUI)
    self.view.processNode:InitBusinessCardProcessNodeByRoleId(self.m_roleId)
    self.view.personalCollectionNode:InitBusinessCardPersonalCollectionNode(self.m_roleId)
    self.view.regionalNode:InitBusinessCardRegionalNodeByRoleId(self.m_roleId)

    self.view.businessCardRoleNode:InitBusinessCardRoleNode(self.m_roleId, self.m_preview or self.m_hideUI)
    self.view.businessCardMedalNode:InitBusinessCardMedalNode(self.m_roleId, roleType == CS.Beyond.Gameplay.RoleType.Self and not self.m_preview)
    self.view.seasonTowerNode:InitSeasonTowerNode(self.m_roleId)

    local success, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_roleId)
    if not success then
        logger.error("FriendBusinessCard.InitFriendBusinessCard 失败！因为没有找到好友信息！")
        return
    end
    if GameInstance.player.friendSystem.isCommunicationRestricted then
        self.view.signTxt.text = ""
        self.view.signBtn.gameObject:SetActive(false)
    else
        local interactable = not self.m_preview and roleType == CS.Beyond.Gameplay.RoleType.Self
        local signature = roleType == CS.Beyond.Gameplay.RoleType.Self and string.isEmpty(friendInfo.signature) and Language.LUA_BUSINESS_CARD_SIGNATURE_TIP .. "<image=\"ThemeIcon/icon_friend_revise\" width=40 height=52>" or
            interactable and friendInfo.signature .. "<image=\"ThemeIcon/icon_friend_revise\" width=40 height=52>" or friendInfo.signature
        self.view.signTxt:SetAndResolveTextStyle(signature)
        self.view.signBtn.interactable = interactable
        if interactable == false and string.isEmpty(signature) then
            self.view.signBtn.gameObject:SetActiveIfNecessary(false)
        else
            self.view.signBtn.gameObject:SetActiveIfNecessary(true)
        end
    end

    self.view.bgCloseBtn.gameObject:SetActiveIfNecessary(self.m_isExpanded or self.m_hideUI)
end

FriendBusinessCard.RefreshBirthdayInfo = HL.Method() << function(self)
    self.view.personalInfoNode:RefreshBirthdayInfo()
end

HL.Commit(FriendBusinessCard)
return FriendBusinessCard

