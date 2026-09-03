local SocialBuildingSource = CS.Beyond.Gameplay.Factory.SocialBuildingSource

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSocialBuilding

MapMarkDetailSocialBuildingCtrl = HL.Class('MapMarkDetailSocialBuildingCtrl', uiCtrl.UICtrl)





MapMarkDetailSocialBuildingCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FAC_SOCIAL_BUILDING_RECEIVED] = "_OnFacSocialBuildingReceived",
    [MessageConst.ON_FAC_RECEIVED_SOCIAL_BUILDING_DATA_UPDATED] = "_OnFacReceivedSocialBuildingDataUpdated",
}

MapMarkDetailSocialBuildingCtrl.m_existing = HL.Field(HL.Boolean) << false

MapMarkDetailSocialBuildingCtrl.m_markRuntimeData = HL.Field(HL.Userdata)

MapMarkDetailSocialBuildingCtrl.m_nodeId = HL.Field(HL.Number) << -1

MapMarkDetailSocialBuildingCtrl.m_social = HL.Field(CS.Beyond.Gameplay.RemoteFactory.ServerChapterInfo.ComponentHandler.Payload_Social)

MapMarkDetailSocialBuildingCtrl.m_isOthersSocialBuilding = HL.Field(HL.Boolean) << false

MapMarkDetailSocialBuildingCtrl.m_ownerId = HL.Field(HL.Number) << -1


MapMarkDetailSocialBuildingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local markInstId = arg.markInstId
    local commonArgs = {
        markInstId = markInstId,
        bigBtnActive = true,
    }

    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    self.m_markRuntimeData = markRuntimeData
    local nodeId = markRuntimeData.nodeId
    self.m_existing = nodeId ~= nil
    if self.m_existing then

        self.m_nodeId = nodeId
        local social, source = FactoryUtils.getSocialBuildingDetails(nodeId, self.m_markRuntimeData.chapterId)
        self.m_social = social
        self.m_isOthersSocialBuilding = source == SocialBuildingSource.Others
        self.m_ownerId = source == SocialBuildingSource.Mine and GameInstance.player.roleId or social.ownerId

        if self.m_isOthersSocialBuilding then
            local canDel = true
            local node = FactoryUtils.getBuildingNodeHandler(nodeId, self.m_markRuntimeData.chapterId)
            if node then
                canDel = not CSFactoryUtil.CheckIsBuildingMoveAndDelLocked(node.templateId, node.instKey, false)
            end
            if canDel then
                self.view.deleteBtn.gameObject:SetActive(true)
                self.view.deleteBtn.onClick:AddListener(function()
                    self:_DeleteSocialBuilding()
                end)
            else
                self.view.deleteBtn.gameObject:SetActive(false)
            end
        else
            self.view.deleteBtn.gameObject:SetActive(false)
        end
    else

        self.m_isOthersSocialBuilding = true
        self.m_ownerId = markRuntimeData.ownerId

        commonArgs.bigBtnCallback = function()
            self:_ReceiveSocialBuilding()
        end
        commonArgs.bigBtnText = Language.LUA_MAP_MARK_DETAIL_SOCIAL_BUILDING_RECEIVE_BTN_TEXT
        commonArgs.bigBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.TELEPORT

        self.view.deleteBtn.gameObject:SetActive(false)
    end

    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)

    if self.m_existing and not self.m_isOthersSocialBuilding then
        self.view.leftBtn.gameObject:SetActiveIfNecessary(true)
        self.view.leftBtn.onClick:AddListener(function()
            self:_DeleteSelfBuilding()
        end)
    else
        self.view.leftBtn.gameObject:SetActiveIfNecessary(false)
    end
end

MapMarkDetailSocialBuildingCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()
end

MapMarkDetailSocialBuildingCtrl._UpdateView = HL.Method() << function(self)
    local active = self.m_isOthersSocialBuilding
    self.view.receiveSocialBuildingTips.gameObject:SetActive(false)
    self.view.sourceNode.gameObject:SetActive(false)
    self.view.stabilityNode.gameObject:SetActive(active)

    if active then
        self:_UpdateSocialBuildingView()
    end
end

MapMarkDetailSocialBuildingCtrl._UpdateSocialBuildingView = HL.Method() << function(self)

    local ownerId = self.m_ownerId
    if self.m_existing and self.m_social.preset then

        local success, npcData = Tables.factorySocialBuildingNpcTable:TryGetValue(ownerId)
        if not success then
            logger.error("[MapMarkDetail] SocialBuilding: Npc data not found, npcId: " .. tostring(ownerId))
            return
        end

        self.view.sourceNode.gameObject:SetActive(true)
        self.view.npcCell.gameObject:SetActive(true)
        self.view.contactFriendCell.gameObject:SetActive(false)

        self.view.npcCell.playerHead:InitCommonPlayerHead(npcData.avatarPath, npcData.avatarFramePath,
            false, 0, npcData.name, nil)
    elseif not self:_UpdateOwnerView_Player(ownerId) then

        GameInstance.player.friendSystem:SyncSocialFriendInfo({ ownerId }, function()
            if self.m_isClosed then
                return
            end
            if not self:_UpdateOwnerView_Player(ownerId) then
                logger.info("[MapMarkDetail] SocialBuilding: Owner info not found, roleId: " .. tostring(ownerId))
                return
            end
        end)
    end

    if self.m_existing then

        self.view.stabilityTitleText.text = Language.LUA_MAP_MARK_DETAIL_SOCIAL_BUILDING_STABILITY_TITLE
        local stabilityValue = FactoryUtils.getSocialBuildingStability(self.m_nodeId, self.m_markRuntimeData.chapterId)
        self.view.stabilityValueText.text = string.format("%.0f%%", stabilityValue * 100)
    else

        self:_UpdateReceivedSocialBuildingCount()
    end
end

MapMarkDetailSocialBuildingCtrl._UpdateOwnerView_Player = HL.Method(HL.Number).Return(HL.Boolean) << function(self, ownerId)
    local success, ownerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(ownerId)
    if not success or not ownerInfo.init then
        return false
    end

    self.view.sourceNode.gameObject:SetActive(true)
    self.view.npcCell.gameObject:SetActive(false)
    self.view.contactFriendCell.gameObject:SetActive(true)

    self.view.contactFriendCell:InitContactFriendCell(ownerId, ownerInfo, 0, nil)
    local onClickAvatar = FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(ownerId).action
    self.view.contactFriendCell.view.playerHead:SetClick(onClickAvatar)
    return true
end

MapMarkDetailSocialBuildingCtrl._UpdateReceivedSocialBuildingCount = HL.Method() << function(self)
    local remoteFactorySystem = GameInstance.player.remoteFactory
    self.view.stabilityTitleText.text = Language.LUA_MAP_MARK_DETAIL_SOCIAL_BUILDING_RECEPTION_TITLE
    local buildingCount = remoteFactorySystem.receivedSocialBuildingCount
    local buildingMaxCount = remoteFactorySystem.receivedSocialBuildingMaxCount
    local isBuildingReceivable = buildingCount < buildingMaxCount
    local receptionValueTextFormat = isBuildingReceivable
        and "%s/%s"
        or Language.LUA_MAP_MARK_DETAIL_SOCIAL_BUILDING_RECEPTION_VALUE_LIMIT
    self.view.stabilityValueText.text = string.format(receptionValueTextFormat, buildingCount, buildingMaxCount)
    self.view.receiveSocialBuildingTips.gameObject:SetActive(not isBuildingReceivable)
end

MapMarkDetailSocialBuildingCtrl._ReceiveSocialBuilding = HL.Method() << function(self)

    local remoteFactorySystem = GameInstance.player.remoteFactory
    local buildingCount = remoteFactorySystem.receivedSocialBuildingCount
    local buildingMaxCount = remoteFactorySystem.receivedSocialBuildingMaxCount
    local isBuildingReceivable = buildingCount < buildingMaxCount
    if isBuildingReceivable then

        self:_SendReceiveSocialBuilding()
    else

        UIManager:Open(PanelId.BuildingSharePop, {
            onClickReplace = function(buildingLevelId, buildingNodeId, buildingOwnerId)
                self:_SendReceiveSocialBuilding(buildingLevelId, buildingNodeId, buildingOwnerId)
            end
        })
    end
end

MapMarkDetailSocialBuildingCtrl._SendReceiveSocialBuilding = HL.Method(HL.Opt(HL.String, HL.Number, HL.Number))
    << function(self, buildingLevelId, buildingNodeId, buildingOwnerId)
    local markRuntimeData = self.m_markRuntimeData
    local chatRoleId = markRuntimeData.chatRoleId
    local chatMsgIndex = markRuntimeData.chatMsgIndex
    if buildingLevelId and buildingNodeId and buildingOwnerId then
        GameInstance.player.friendChatSystem:SendReceiveSocialBuilding(chatRoleId, chatMsgIndex, buildingLevelId, buildingNodeId, buildingOwnerId)
    else
        GameInstance.player.friendChatSystem:SendReceiveSocialBuilding(chatRoleId, chatMsgIndex)
    end
end

MapMarkDetailSocialBuildingCtrl._DeleteSocialBuilding = HL.Method() << function(self)

    if GameWorld.gameMechManager.travelPoleBrain:CanOpenMiniMap() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
        return
    end

    FactoryUtils.delBuilding(self.m_nodeId, function()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_DEL_BUILDING_SUCCESS)
        if self.m_isClosed then
            return
        end
        self:PlayAnimationOutAndClose()
    end, true, nil, self.m_markRuntimeData.chapterId)
end

MapMarkDetailSocialBuildingCtrl._DeleteSelfBuilding = HL.Method() << function(self)
    if GameWorld.gameMechManager.travelPoleBrain:CanOpenMiniMap() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
        return
    end

    FactoryUtils.delBuilding(self.m_nodeId, function()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_DEL_TOAST)
        if self.m_isClosed then
            return
        end
        self:PlayAnimationOutAndClose()
    end, true, nil, self.m_markRuntimeData.chapterId, true)
end









MapMarkDetailSocialBuildingCtrl._OnFacSocialBuildingReceived = HL.Method(HL.Table) << function(self, args)
    local isAdd = unpack(args)
    if isAdd then

        self:_UpdateSocialBuildingState()
    else





        if not self.m_existing then
            self:_UpdateReceivedSocialBuildingCount()
        end
    end
end

MapMarkDetailSocialBuildingCtrl._OnFacReceivedSocialBuildingDataUpdated = HL.Method(HL.Table) << function(self, args)

    self:_UpdateSocialBuildingState()
end

MapMarkDetailSocialBuildingCtrl._UpdateSocialBuildingState = HL.Method() << function(self)




    if self.m_existing then
        return
    end

    local markRuntimeData = self.m_markRuntimeData
    local ownerId = markRuntimeData.ownerId
    local levelId = markRuntimeData.levelId
    local ownerNodeId = markRuntimeData.ownerNodeId
    local received, socialBuildingInfo = GameInstance.player.remoteFactory:IsSocialBuildingReceived(ownerId, levelId, ownerNodeId)
    if not received then
        return
    end


    local chapterId = socialBuildingInfo.chapterId
    local nodeId = socialBuildingInfo.nodeId
    local success, markInstId = GameInstance.player.mapManager:GetFacMarkInstIdByNodeId(chapterId, nodeId)
    if not success then
        return
    end


    GameInstance.player.mapManager:RemoveSocialBuildingMarks()
    MapUtils.openMap(markInstId, levelId)


    Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_SUCCESS)
end

MapMarkDetailSocialBuildingCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(MapMarkDetailSocialBuildingCtrl)
