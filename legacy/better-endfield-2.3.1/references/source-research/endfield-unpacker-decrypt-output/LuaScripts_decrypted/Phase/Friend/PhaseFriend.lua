local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.Friend
PhaseFriend = HL.Class('PhaseFriend', phaseBase.PhaseBase)

PhaseFriend.m_curPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseFriend.m_curPopupPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseFriend.m_panelItemDic = HL.Field(HL.Table)

PhaseFriend.m_popupPanelItemDic = HL.Field(HL.Table)

PhaseFriend.m_tabPanel = HL.Field(HL.Forward("PhasePanelItem"))





PhaseFriend.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_CHAR_QUERY] = { '_OnFriendCharQuery', false },
    [MessageConst.ON_FRIEND_BUSINESS_INFO_CHANGE] = { '_BindControllerHintPlaceHolder', true },
}



PhaseFriend._OnInit = HL.Override() << function(self)
    PhaseFriend.Super._OnInit(self)
end




PhaseFriend.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseFriend._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.arg == nil then
        self.arg = {}
    end

    self.m_panelItemDic = {}
    self.m_popupPanelItemDic = {}
    self.m_tabPanel = self:CreatePhasePanelItem(PanelId.FriendTab, self.arg)

    Notify(MessageConst.ON_CHANGE_FRIEND_TAB, self.arg)

    if self.arg and self.arg.needTab == false then
        if self.m_tabPanel then
            self.m_tabPanel.uiCtrl:Hide()
        end
    end

    self:_BindControllerHintPlaceHolder()

    if self.arg and self.arg.friendRoleDisplayArg then
        UIManager:Open(PanelId.FriendRoleDisplay, self.arg.friendRoleDisplayArg)
        self.arg.friendRoleDisplayArg = nil
    end

    if self.arg and self.arg.friendThemeChangeArg then
        UIManager:Open(PanelId.FriendThemeChange, self.arg.friendThemeChangeArg)
        self.arg.friendThemeChangeArg = nil
    end

    if self.arg and self.arg.friendBusinessCardPreviewArg then
        UIManager:Open(PanelId.FriendBusinessCardPreview, self.arg.friendBusinessCardPreviewArg)
        self.arg.friendBusinessCardPreviewArg = nil
    end


    if self.arg then
        self.arg.naviTargetActionMenuArg = nil
    end

    if self.arg and self.arg.commonPopUpArg then
        local commonPopUpArg = self.arg.commonPopUpArg
        if commonPopUpArg.content == Language.LUA_SPACESHIP_VISIT_LEAVE_RETURN_FRIEND_TIPS then
            commonPopUpArg.onConfirm = function()
                GameInstance.player.spaceship:LeaveVisitSpaceShip(true)
            end
            Notify(MessageConst.SHOW_POP_UP, commonPopUpArg)
        elseif commonPopUpArg.content == Language.LUA_SPACESHIP_VISIT_LEAVE_FRIEND_TIPS then
            commonPopUpArg.onConfirm = function()
                GameInstance.player.spaceship:LeaveVisitSpaceShip(false)
            end
            Notify(MessageConst.SHOW_POP_UP, commonPopUpArg)
        end
        self.arg.commonPopUpArg = nil
    end

    if self.arg and self.arg.commonShareArg then
        UIManager:Open(PanelId.CommonShare, self.arg.commonShareArg)
        self.arg.commonShareArg = nil
    end

    if self.arg and self.arg.instructionBookArg then
        UIManager:Open(PanelId.InstructionBook, self.arg.instructionBookArg)
        self.arg.instructionBookArg = nil
    end

    if self.arg and self.arg.friendBlackListArg then
        UIManager:Open(PanelId.FriendBlackList, self.arg.friendBlackListArg)
        self.arg.friendBlackListArg = nil
    end

    if self.arg and self.arg.friendRequestArg then
        UIManager:Open(PanelId.FriendRequest, self.arg.friendRequestArg)
        self.arg.friendRequestArg = nil
    end

    if self.arg and self.arg.searchNewFriendListArg then
        UIManager:Open(PanelId.SearchNewFriendList, self.arg.searchNewFriendListArg)
        self.arg.searchNewFriendListArg = nil
    end

    if self.arg and self.arg.friendHeadSelectedPopUpArg then
        UIManager:Open(PanelId.FriendHeadSelectedPopUp, self.arg.friendHeadSelectedPopUpArg)
        self.arg.friendHeadSelectedPopUpArg = nil
    end
end

PhaseFriend._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseFriend._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseFriend._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:_RefreshCurPanelTabBlockState()
end






PhaseFriend._OnActivated = HL.Override() << function(self)
end

PhaseFriend._OnDeActivated = HL.Override() << function(self)
end



PhaseFriend._OnRefresh = HL.Override() << function(self)
    if self.m_curPopupPanel ~= nil then
        self.m_curPopupPanel.uiCtrl:Close()
    end

    if self.arg and (self.arg.panelId or self.m_curPanelItem == nil or self.m_curPanelItem.uiCtrl.panelId ~= self.arg.panelId) then
        Notify(MessageConst.ON_CHANGE_FRIEND_TAB, self.arg)
    elseif self.m_curPanelItem then
        self.m_curPanelItem.uiCtrl:OnPhaseRefresh(self.arg)
    end

    if self.arg and self.arg.needTab == false then
        if self.m_tabPanel then
            self.m_tabPanel.uiCtrl:Hide()
        end
    end
end

PhaseFriend.OpenPopupPanel = HL.Method(HL.Number, HL.Any) << function(self, panelId, args)
    if panelId == nil then
        return
    end

    local panelItem
    if self.m_popupPanelItemDic[panelId] then
        panelItem = self.m_popupPanelItemDic[panelId]
    else
        panelItem = self:CreatePhasePanelItem(panelId, args)
        self.m_popupPanelItemDic[panelId] = panelItem
    end
    panelItem.uiCtrl:Show()
    self.m_curPopupPanel = panelItem
end

PhaseFriend.ClosePopupPanel = HL.Method(HL.Number) << function(self, panelId)
    if self.m_popupPanelItemDic[panelId] then
        self.m_popupPanelItemDic[panelId].uiCtrl:Close()
    end
    if self.m_curPanelItem then
        self.m_curPanelItem.uiCtrl:OnPhaseRefresh(self.arg)
    end
end

PhaseFriend.OnTabChange = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, panelId ,arg)
    if panelId == nil then
        return
    end

    self.arg = self.arg or {}
    self.arg.panelId = panelId

    if self.m_curPanelItem then
        self.m_curPanelItem.uiCtrl:Hide()
    end

    local panelItem
    if self.m_panelItemDic[panelId] then
        panelItem = self.m_panelItemDic[panelId]
        panelItem.uiCtrl:Show()
        panelItem.uiCtrl:OnPhaseRefresh(arg)
    else
        panelItem = self:CreatePhasePanelItem(panelId, arg)
        self.m_panelItemDic[panelId] = panelItem
    end
    self.m_curPanelItem = panelItem
    self:_BindControllerHintPlaceHolder()
end

PhaseFriend.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    if self.m_curPanelItem and self.m_curPanelItem.uiCtrl then
        local overrideArg = self.m_curPanelItem.uiCtrl:GetCurPhaseStateArg()
        if overrideArg ~= nil then
            arg = overrideArg
        end
    end
    arg.friendRoleDisplayArg = nil
    arg.friendThemeChangeArg = nil
    arg.friendBusinessCardPreviewArg = nil
    arg.naviTargetActionMenuArg = nil
    arg.commonPopUpArg = nil
    arg.commonShareArg = nil
    arg.instructionBookArg = nil
    arg.friendBlackListArg = nil
    arg.friendRequestArg = nil
    arg.searchNewFriendListArg = nil
    arg.friendHeadSelectedPopUpArg = nil
    if self.m_curPanelItem and self.m_curPanelItem.uiCtrl then
        arg.panelId = self.m_curPanelItem.uiCtrl.panelId
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.FriendRoleDisplay)
    if isOpen and UIManager:IsShow(PanelId.FriendRoleDisplay) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local friendRoleDisplayArg = ctrl:GetCurPhaseStateArg()
        if friendRoleDisplayArg then
            arg.friendRoleDisplayArg = friendRoleDisplayArg
        end
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.FriendThemeChange)
    if isOpen and UIManager:IsShow(PanelId.FriendThemeChange) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local friendThemeChangeArg = ctrl:GetCurPhaseStateArg()
        if friendThemeChangeArg then
            arg.friendThemeChangeArg = friendThemeChangeArg
        end
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.FriendBusinessCardPreview)
    if isOpen and UIManager:IsShow(PanelId.FriendBusinessCardPreview) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local friendBusinessCardPreviewArg = ctrl:GetCurPhaseStateArg()
        if friendBusinessCardPreviewArg then
            arg.friendBusinessCardPreviewArg = friendBusinessCardPreviewArg
        end
    end

    isOpen, ctrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if isOpen and UIManager:IsShow(PanelId.CommonPopUp) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local commonPopUpArg = ctrl:GetCurPhaseStateArg()
        if commonPopUpArg then
            arg.commonPopUpArg = commonPopUpArg
        end
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.CommonShare)
    if isOpen and UIManager:IsShow(PanelId.CommonShare) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local commonShareArg = ctrl:GetCurPhaseStateArg()
        if commonShareArg then
            arg.commonShareArg = commonShareArg
        end
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and UIManager:IsShow(PanelId.InstructionBook) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        arg.instructionBookArg = ctrl.id
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.FriendBlackList)
    if isOpen and UIManager:IsShow(PanelId.FriendBlackList) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local friendBlackListArg = ctrl:GetCurPhaseStateArg()
        if friendBlackListArg then
            arg.friendBlackListArg = friendBlackListArg
        end
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.FriendRequest)
    if isOpen and UIManager:IsShow(PanelId.FriendRequest) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local friendRequestArg = ctrl:GetCurPhaseStateArg()
        if friendRequestArg then
            arg.friendRequestArg = friendRequestArg
        end
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.SearchNewFriendList)
    if isOpen and UIManager:IsShow(PanelId.SearchNewFriendList) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local searchNewFriendListArg = ctrl:GetCurPhaseStateArg()
        if searchNewFriendListArg then
            arg.searchNewFriendListArg = searchNewFriendListArg
        end
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.FriendHeadSelectedPopUp)
    if isOpen and UIManager:IsShow(PanelId.FriendHeadSelectedPopUp) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local friendHeadSelectedPopUpArg = ctrl:GetCurPhaseStateArg()
        if friendHeadSelectedPopUpArg then
            arg.friendHeadSelectedPopUpArg = friendHeadSelectedPopUpArg
        end
    end
    arg.phase = nil
    return arg
end

PhaseFriend.SetTabBlockState = HL.Method(HL.Boolean) << function(self, isBlock)
    if self.m_tabPanel and self.m_tabPanel.uiCtrl then
        self.m_tabPanel.uiCtrl.view.inputGroup.enabled = not isBlock
    end
end

PhaseFriend._RefreshCurPanelTabBlockState = HL.Method() << function(self)
    if self.m_curPanelItem == nil or self.m_curPanelItem.uiCtrl == nil then
        return
    end

    local curCtrl = self.m_curPanelItem.uiCtrl
    if curCtrl.panelId == PanelId.FriendBusinessCardRoot then
        curCtrl:RefreshTabBlockState()
    else
        self:SetTabBlockState(false)
    end
end

PhaseFriend._BindControllerHintPlaceHolder = HL.Method() << function(self)
    if not self.m_tabPanel or not self.m_curPanelItem then
        return
    end

    local friendCtrl = self.m_tabPanel.uiCtrl
    local curCtrl = self.m_curPanelItem.uiCtrl
    if friendCtrl and curCtrl and curCtrl.view and DeviceInfo.inputType == DeviceInfo.InputType.Controller then
        curCtrl.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
            friendCtrl.view.inputGroup.groupId,
            curCtrl.view.inputGroup.groupId,
        })
    end
end

PhaseFriend.s_mainFriendCharTemplateId = HL.StaticField(HL.String) << ""

PhaseFriend._OnFriendCharQuery = HL.StaticMethod(HL.Table) << function(args)
    local roleId, charData = unpack(args)
    if charData == nil then
        logger.error("PhaseFriend._OnFriendCharQuery: charData is nil for roleId: " .. tostring(roleId))
        return
    end

    CharInfoUtils.openCharInfoBestWay({
        initCharInfoCreator = function(initCharTemplateId)
            local charInstIdList = {}
            local mainCharInfo
            initCharTemplateId = initCharTemplateId or PhaseFriend.s_mainFriendCharTemplateId
            for i = 0, charData.Count - 1 do
                local charInfo = GameInstance.player.charBag:CreateClientFriendCharInfo(charData[i], ScopeUtil.GetCurrentScope())
                if charInfo then
                    table.insert(charInstIdList, charInfo.instId)

                    if mainCharInfo == nil then
                        mainCharInfo = charInfo
                    end
                    if charInfo.templateId == initCharTemplateId  then
                        mainCharInfo = charInfo
                    end
                else
                    logger.error("PhaseFriend._OnFriendCharQuery: CreateClientFriendCharInfo failed for roleId: " .. tostring(roleId))
                end
            end
            return {
                instId = mainCharInfo.instId,
                templateId = mainCharInfo.templateId,
                isTrail = false,
                charInstIdList = charInstIdList,
            }
        end,
        onClose = function()
            GameInstance.player.charBag:ClearAllClientCharAndItemData()
        end,
    })
end

HL.Commit(PhaseFriend)

