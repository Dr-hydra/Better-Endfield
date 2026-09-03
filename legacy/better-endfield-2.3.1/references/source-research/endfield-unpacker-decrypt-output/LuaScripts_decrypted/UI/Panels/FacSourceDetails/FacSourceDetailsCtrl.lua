local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacSourceDetails

FacSourceDetailsCtrl = HL.Class('FacSourceDetailsCtrl', uiCtrl.UICtrl)










FacSourceDetailsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_CELL_INFO_CHANGE] = "_UpdateView",
}

FacSourceDetailsCtrl.m_arg = HL.Field(HL.Table)

FacSourceDetailsCtrl.m_nodeId = HL.Field(HL.Number) << -1

FacSourceDetailsCtrl.m_social = HL.Field(CS.Beyond.Gameplay.RemoteFactory.ServerChapterInfo.ComponentHandler.Payload_Social)

FacSourceDetailsCtrl.m_stabilitySliderTween = HL.Field(HL.Any)


FacSourceDetailsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.likeBtn.onClick:AddListener(function()
        self:_LikeSocialBuilding()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    Notify(MessageConst.HIDE_COMMON_HOVER_TIP, { noAnimation = true })

    self.m_arg = arg
    self.m_nodeId = arg.nodeId
    self.m_social = FactoryUtils.getBuildingComponentPayload_Social(arg.nodeId)
end

FacSourceDetailsCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView(true)
end

FacSourceDetailsCtrl.OnClose = HL.Override() << function(self)
    self:_StopStabilityAnimation()
end

FacSourceDetailsCtrl.GetTargetNodeId = HL.Method().Return(HL.Number) << function(self)
    return self.m_nodeId
end

FacSourceDetailsCtrl._UpdateView = HL.Method(HL.Opt(HL.Boolean)) << function(self, updateStability)
    local social = self.m_social
    local preset = social.preset
    local ownerId = social.ownerId

    local node = FactoryUtils.getBuildingNodeHandler(self.m_nodeId)
    local buildingData = Tables.factoryBuildingTable:GetValue(node.templateId)

    self.view.npcListCell.rectTransform.gameObject:SetActive(preset)
    self.view.friendListCell.view.gameObject:SetActive(not preset)
    local ownerName = ""
    if preset then
        local npcData = self.m_arg.npcData
        ownerName = npcData.name
        self.view.npcListCell.commonPlayerHead:InitCommonPlayerHead(npcData.avatarPath, npcData.avatarFramePath,
            false, 0, npcData.name, npcData.signature)
    else
        local ownerInfo = self.m_arg.ownerInfo
        ownerName = ownerInfo and ownerInfo.name or ""
        local config = FriendUtils.getFriendCellInitConfigByRoleId(ownerId)
        config = config and lume.copy(config) or {}
        config.stateName = FriendUtils.FRIEND_CELL_INIT_CONFIG.Stranger.stateName
        self.view.friendListCell:RefreshFriendListCell(ownerId, config, "")
        self.view.friendListCell.m_buildNodeId = self.m_nodeId
        self.view.friendListCell.view.commonPlayerHead:SetClick(function()
            Notify(MessageConst.ON_OPEN_BUSINESS_CARD_PREVIEW, { roleId = ownerId, bId = self.m_nodeId })
        end)
    end
    self.view.titleText.text = string.isEmpty(ownerName)
        and buildingData.name
        or string.format(Language.LUA_FAC_SOCIAL_BUILDING_SOURCE_TITLE, ownerName, buildingData.name)

    if updateStability then
        self.view.stabilitySlider.value = FactoryUtils.getSocialBuildingStability(self.m_nodeId)
    end
    self.view.likeValueText.text = social.like

    local canLike = FactoryUtils.canLikeSocialBuilding(self.m_nodeId)
    self.view.likeBtn.interactable = canLike
    self.view.likeBtnStateCtrl:SetState(canLike and "NormalState" or "DisableState")
    Notify(MessageConst.REFRESH_CONTROLLER_HINT)
end

FacSourceDetailsCtrl._LikeSocialBuilding = HL.Method() << function(self)
    FactoryUtils.likeSocialBuilding(self.m_nodeId, function()
        if self.m_isClosed then
            return
        end
        self:_PlayStabilityAnimation()
        self:_UpdateView(false)
    end)
end

FacSourceDetailsCtrl._PlayStabilityAnimation = HL.Method() << function(self)
    self:_StopStabilityAnimation()

    self:PlayAnimation("facsourcedetails_repairdone")

    local newStability = FactoryUtils.getSocialBuildingStability(self.m_nodeId)
    self.m_stabilitySliderTween = DOTween.To(function()
        return self.view.stabilitySlider.value
    end, function(value)
        self.view.stabilitySlider.value = value
    end, newStability, self.view.config.STABILITY_SLIDER_TWEEN_DURATION)
end

FacSourceDetailsCtrl._StopStabilityAnimation = HL.Method() << function(self)
    if self.m_stabilitySliderTween then
        self.m_stabilitySliderTween:Kill()
        self.m_stabilitySliderTween = nil
    end
end

HL.Commit(FacSourceDetailsCtrl)
