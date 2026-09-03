
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendBusinessCardPreview
local PHASE_ID = PhaseId.FriendBusinessCardPreview

FriendBusinessCardPreviewCtrl = HL.Class('FriendBusinessCardPreviewCtrl', uiCtrl.UICtrl)


FriendBusinessCardPreviewCtrl.TryStartBusinessCardPreview = HL.StaticMethod(HL.Table) << function(arg)
    if arg and arg.roleId then

        local id = arg.roleId
        if id == GameInstance.player.roleId then
            UIManager:Open(PanelId.FriendBusinessCardPreview, { roleId = id })
            return
        end

        if GameInstance.player.friendSystem:PlayerInBlackList(id) then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TIP_IN_BLACK_LIST_CANNOT_VIEW_BUSINESS_CARD)
            return
        end


        if FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem.isPSNOnly and not GameInstance.player.friendSystem:IsPsnFriend(id) then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TIP_PSN_BUSINESS_CARD)
            return
        end


        if GameInstance.player.friendSystem.isCommunicationRestricted then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TIP_PARENTAL_CONTROL_BUSINESS_CARD)
            return
        end

        logger.info('FriendBusinessCardPreviewCtrl.TryStartBusinessCardPreview: Syncing friend info for roleId: ' .. arg.roleId)
        GameInstance.player.friendSystem:SyncFriendInfoById(arg.roleId, function()
            if arg.isPhase == nil or arg.isPhase == true then
                PhaseManager:GoToPhase(PHASE_ID, arg)
            else
                UIManager:Open(PANEL_ID, arg)
            end
        end)
    else
        logger.error('FriendBusinessCardPreviewCtrl.TryStartBusinessCardPreview: roleId is required in arg')
    end
end






FriendBusinessCardPreviewCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_BUSINESS_INFO_CHANGE] = 'OnFriendBusinessInfoChange',
}

FriendBusinessCardPreviewCtrl.m_roleId = HL.Field(HL.Number) << 0

FriendBusinessCardPreviewCtrl.m_panel = HL.Field(HL.Userdata)

FriendBusinessCardPreviewCtrl.m_businessCard = HL.Field(HL.Forward('FriendBusinessCard'))

FriendBusinessCardPreviewCtrl.m_forceShare = HL.Field(HL.Boolean) << false

FriendBusinessCardPreviewCtrl.m_arg = HL.Field(HL.Table)

FriendBusinessCardPreviewCtrl.m_desiredExpanded = HL.Field(HL.Boolean) << false


FriendBusinessCardPreviewCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg or {}
    local hasDesiredExpandedArg = arg ~= nil and arg.isExpanded ~= nil

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self:_Close()
    end)

    self.view.shareBtn.onClick:RemoveAllListeners()
    self.view.shareBtn.onClick:AddListener(function()

        self.view.btnClose.gameObject:SetActive(false)
        self.view.shareBtn.gameObject:SetActive(false)
        local beforeSize = self.view.mask2d.transform.sizeDelta
        local beforeAnchorMin = self.view.mask2d.transform.anchorMin
        local beforeAnchorMax = self.view.mask2d.transform.anchorMax
        local beforePivot = self.view.mask2d.transform.pivot
        local beforePos = self.view.mask2d.transform.anchoredPosition
        local beforeScale = self.view.businessCardScale.localScale
        self.view.mask2d.enabled = false
        self.view.mask2d.transform.anchoredPosition = Vector2.zero
        self.view.mask2d.transform.sizeDelta = Vector2.zero
        self.view.mask2d.transform.anchorMin = Vector2.zero
        self.view.mask2d.transform.anchorMax = Vector2.one
        self.view.mask2d.transform.pivot = Vector2(0.5, 0.5)
        self.view.businessCardScale.localScale = Vector3.one

        Notify(MessageConst.SHOW_COMMON_SHARE_PANEL, {
            type = "BusinessCard",
            showPlayerInfo = false,
            showPlayerInfoToggle = false,
            needEdge = false,
            onCaptureEnd = function()

                self.view.btnClose.gameObject:SetActive(true)
                self.view.shareBtn.gameObject:SetActive(true)
                self.view.mask2d.enabled = true
                self.view.mask2d.transform.sizeDelta = beforeSize
                self.view.mask2d.transform.anchorMin = beforeAnchorMin
                self.view.mask2d.transform.anchorMax = beforeAnchorMax
                self.view.mask2d.transform.pivot = beforePivot
                self.view.mask2d.transform.anchoredPosition = beforePos
                self.view.businessCardScale.localScale = beforeScale
                if arg.forceShare == true then
                    self:_Close()
                end
            end,
        })
    end)

    if arg and arg.roleId then
        self.m_roleId = arg.roleId
    else
        logger.error('FriendBusinessCardRootCtrl.OnCreate: roleId is required in arg')
        return
    end

    local fullScreen = arg.fullScreen ~= false
    if fullScreen then
        self.view.mask2d.enabled = false
        self.view.mask2d.transform.anchoredPosition = Vector2.zero
        self.view.mask2d.transform.sizeDelta = Vector2.zero
        self.view.mask2d.transform.anchorMin = Vector2.zero
        self.view.mask2d.transform.anchorMax = Vector2.one
        self.view.mask2d.transform.pivot = Vector2(0.5, 0.5)
        self.view.businessCardScale.transform.anchoredPosition = Vector2.zero
        self.view.businessCardScale.localScale = Vector3.one
        self.view.businessCardScale.transform.sizeDelta = Vector2.zero
        self.view.shareBtn.gameObject:SetActive(false)
    end


    self.view.shareBtn.gameObject:SetActive(self.m_roleId == GameInstance.player.roleId and not GameInstance.player.friendSystem.isCommunicationRestricted)

    local success , friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_roleId)
    if not success then
        logger.error('FriendBusinessCardRootCtrl.OnCreate: Failed to get friend info for roleId: ' .. self.m_roleId)

        friendInfo = GameInstance.player.friendSystem.SelfInfo
    end

    if self.m_panel then
        CSUtils.ClearUIComponents(self.m_panel)
        GameObject.DestroyImmediate(self.m_panel)
    end

    local businessCardId = friendInfo.businessCardTopicId

    if businessCardId == nil or businessCardId == '' then
        logger.error('FriendBusinessCardRootCtrl.OnCreate: businessCardId is nil or empty for roleId: ' .. self.m_roleId)
        return
    end

    local success, cfg = Tables.businessCardTopicTable:TryGetValue(businessCardId)

    if not success then
        logger.error('FriendBusinessCardRootCtrl.OnCreate: Failed to get business card config for id: ' .. businessCardId)
        return
    end

    local path = string.format(UIConst.UI_BUSINESS_CARD_PREFAB_PATH , cfg.panelPrefab)
    local prefab = self:LoadGameObject(path)

    self.m_panel = CSUtils.CreateObject(prefab, self.view.businessCardScale)

    self.m_businessCard = Utils.wrapLuaNode(self.m_panel)
    self.m_forceShare = arg.forceShare == true
    self.m_businessCard:InitFriendBusinessCard(self.m_roleId, true ,self.m_forceShare)
    self.m_businessCard.m_bId = arg.bId
    if hasDesiredExpandedArg then
        self.m_desiredExpanded = arg.isExpanded == true
    else
        self:_CaptureDesiredExpanded()
    end
    if arg.forceShare == true then
        self.animationWrapper:SampleClip(self.animationWrapper.animationIn.name, self.animationWrapper.animationIn.length);
        self.view.shareBtn.onClick:Invoke(nil)
    else
        self.m_businessCard.view.animationWrapper:PlayInAnimation()
        self:_SyncExpandToWidget()
    end
end

FriendBusinessCardPreviewCtrl.OnFriendBusinessInfoChange = HL.Method() << function(self)
    self:_CaptureDesiredExpanded()
    self.m_businessCard:InitFriendBusinessCard(self.m_roleId, true ,self.m_forceShare)
    self:_SyncExpandToWidget()
end

FriendBusinessCardPreviewCtrl._Close = HL.Method() << function(self)
    if PhaseManager:IsOpen(PHASE_ID) then
        PhaseManager:PopPhase(PHASE_ID)
    else
        self:PlayAnimationOutAndClose()
    end
end

FriendBusinessCardPreviewCtrl._CaptureDesiredExpanded = HL.Method() << function(self)
    if self.m_businessCard == nil then
        return
    end
    self.m_desiredExpanded = self.m_businessCard.m_isExpanded == true
end

FriendBusinessCardPreviewCtrl._SyncExpandToWidget = HL.Method() << function(self)
    if self.m_businessCard == nil or self.m_forceShare then
        return
    end
    local isExpanded = self.m_desiredExpanded == true
    self.m_businessCard.m_isExpanded = isExpanded
    self.m_businessCard.view.businessCardState:SetState(isExpanded and 'Expand' or 'Close')
    self.m_businessCard.view.bgCloseBtn.gameObject:SetActiveIfNecessary(isExpanded or self.m_businessCard.m_hideUI)
end

FriendBusinessCardPreviewCtrl.OnShow = HL.Override() << function(self)
    self:_CaptureDesiredExpanded()
    self.m_businessCard:InitFriendBusinessCard(self.m_roleId, true ,self.m_forceShare)
    self:_SyncExpandToWidget()
end

FriendBusinessCardPreviewCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_arg == nil then
        return nil
    end
    local arg = lume.deepCopy(self.m_arg)
    arg.isRestoreFromChangeInputDevice = true
    arg.isExpanded = self.m_businessCard ~= nil and self.m_businessCard.m_isExpanded == true
    return arg
end








HL.Commit(FriendBusinessCardPreviewCtrl)
