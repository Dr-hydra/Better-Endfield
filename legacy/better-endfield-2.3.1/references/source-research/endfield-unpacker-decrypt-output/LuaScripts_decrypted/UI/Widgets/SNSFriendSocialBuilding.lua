local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSFriendSocialBuilding = HL.Class('SNSFriendSocialBuilding', UIWidgetBase)

SNSFriendSocialBuilding.m_isValid = HL.Field(HL.Boolean) << false

SNSFriendSocialBuilding.m_message = HL.Field(HL.Any)

SNSFriendSocialBuilding.curState = HL.Field(HL.Any) << nil

local SynchronizedState = {
    Normal = "Normal",
    Synchronized = "Synchronized",
    InValid = "InValid",
}

SNSFriendSocialBuilding.InitSNSFriendSocialBuilding = HL.Method(HL.Any, HL.Any) << function(self, message, dialogContentNaviGroup)
    self.curState = nil
    self.m_message = message
    self:UpdateSocialBuildingShow(message)
    self.view.currentCellButton.onClick:RemoveAllListeners()
    self.view.currentCellButton.onClick:AddListener(function()
        self:_OnClickBtn()
    end)
    self.view.expiredCell.onClick:RemoveAllListeners()
    self.view.expiredCell.onClick:AddListener(function()
        self:_OnClickExpiredBtn()
    end)
end

SNSFriendSocialBuilding.SetTargetNode = HL.Method() << function(self)
    self:SetNaviTarget(self.view.inputBindingGroupNaviDecorator)
end

SNSFriendSocialBuilding.UpdateSocialBuildingShow = HL.Method(HL.Any) << function(self, message)
    self.m_message = message
    local creatorId = self.m_message.sbCreatorId
    local creatorNodeId = self.m_message.sbCreatorNodeId
    local levelId = self.m_message.sbLevelId
    local chapterId = self.m_message.sbChapterId
    if creatorId == GameInstance.player.roleId then
        if self.m_message.isSelfAndNoRepeatSocialBuild then
            local existed, markInstId = GameInstance.player.mapManager:GetFacMarkInstIdByNodeId(chapterId, creatorNodeId)
            if existed then
                self:SetState(SynchronizedState.Normal)
            else
                self:SetState(SynchronizedState.InValid)
            end
        else
            self:SetState(SynchronizedState.InValid)
        end
    else
        if self.m_message.isFriendAndNoRepeatSocialBuild then
            local received, socialBuildingInfo = GameInstance.player.remoteFactory:IsSocialBuildingReceived(creatorId, levelId, creatorNodeId)
            if received then
                self:SetState(SynchronizedState.Synchronized)
            else
                self:SetState(SynchronizedState.Normal)
            end
        else
            self:SetState(SynchronizedState.InValid)
        end
    end

    local templateStr = self.m_message.sbTemplateId
    local success, buildingData = Tables.factoryBuildingTable:TryGetValue(templateStr)
    local itemData = FactoryUtils.getBuildingItemData(templateStr)
    if buildingData then
        self.view.buildTxt.text = buildingData.name
        if itemData then
            self.view.iconImg:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
        end
    else
        self.view.buildTxt.text = ""
    end

    if self.m_message.sbName ~= nil and self.m_message.sbShortId ~= nil then
        if self.m_message.sbIsDeleted then
            self.view.roleNameTxt.text = Language.LUA_FRIEND_SOCIAL_BUILDING_NO_ACCOUNT_NAME
        else
            self.view.roleNameTxt.text = self.m_message.sbName .. "#" .. self.m_message.sbShortId
        end
    else
        self.view.roleNameTxt.text = "..."
    end

    local levelStr = self.m_message.sbLevelId
    local hasValue, levelDescData = Tables.levelDescTable:TryGetValue(levelStr)
    local sceneName = hasValue and levelDescData.showName or ""

    local levelBasicSuccess, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(levelStr)
    if levelBasicInfo then
        self.view.levelTxt.text = Tables.domainDataTable[levelBasicInfo.domainName].domainName.."-"..sceneName
    else
        self.view.levelTxt.text = ""
    end

end


SNSFriendSocialBuilding.CheckCanJumpIn = HL.Method().Return(HL.Boolean)<< function(self)
    if not self.m_message then
        return false
    end

    if self.curState == SynchronizedState.Normal
        or self.curState == SynchronizedState.Synchronized
        or self.curState == SynchronizedState.InValid then
        return true
    end

    return false
end

SNSFriendSocialBuilding.SetState = HL.Method(HL.String) << function(self, state)
    self.curState = state
    self.view.stateController:SetState(state)
end


SNSFriendSocialBuilding._OnClickBtn = HL.Method() << function(self)
    if not GameInstance.player.systemUnlockManager:IsSystemUnlockByType(GEnums.UnlockSystemType.FacSocial) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAT_JUMP_UNLOCK_SOCIAL_BUILDING_TOAST)
        return
    end

    if GameInstance.player.spaceship.isViewingFriend then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_SOCIAL_BUILDING_JUMP_SPACESHIP_TIP)
        return
    end

    local message = self.m_message
    local creatorId = message.sbCreatorId
    local creatorNodeId = message.sbCreatorNodeId
    local chapterId = message.sbChapterId
    local levelId = message.sbLevelId
    if creatorId == GameInstance.player.roleId then

        if message.isSelfAndNoRepeatSocialBuild then
            local existed, markInstId = GameInstance.player.mapManager:GetFacMarkInstIdByNodeId(chapterId, creatorNodeId)
            if existed then

                MapUtils.openMap(markInstId, levelId)
            else
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_NOT_EXISTED)
            end
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_INVALID_TOAST)
        end
    else

        local received, socialBuildingInfo = GameInstance.player.remoteFactory:IsSocialBuildingReceived(creatorId, levelId, creatorNodeId)
        if received then

            local existed, markInstId = GameInstance.player.mapManager:GetFacMarkInstIdByNodeId(chapterId, socialBuildingInfo.nodeId)
            if existed then

                MapUtils.openMap(markInstId, levelId)
            end
        else

            if not MapUtils.checkIsValidLevelId(levelId) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_FAILED_LEVEL_LOCKED)
                return
            end

            local levelConfigExisted, levelConfig = Utils.getLevelConfig(levelId)
            if not levelConfigExisted or not GameWorld.mapRegionManager:CheckPosInActiveMistMap(levelConfig.mapIdStr, message.sbPos) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_FAILED_IN_MIST)
                return
            end

            local buildingId = message.sbTemplateId
            local techLayerId, techLayerUnlocked = FactoryUtils.isSocialBuildingTechLayerUnlocked(buildingId)
            if not techLayerUnlocked then
                if techLayerId then
                    local techLayerData = Tables.facSTTLayerTable:GetValue(techLayerId)
                    local toastText = string.format(Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_FAILED_TECH_LAYER_LOCKED, techLayerData.name)
                    Notify(MessageConst.SHOW_TOAST, toastText)
                end
                return
            end


            local position = message.sbPos
            GameInstance.player.friendChatSystem:SendSocialBuildingCheckArea(levelId, position, function(enabled)
                if not enabled then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_FAILED_AREA_NOT_ENABLED)
                    return
                end

                local addMarkSuccess = GameInstance.player.mapManager:AddSocialBuildingMarks(message.targetRoleId, message.msgIndex,
                    buildingId, levelId, position, creatorNodeId, creatorId)
                if not addMarkSuccess then
                    return
                end

                MapUtils.openMap(GameInstance.player.mapManager.socialBuildingMarkInstId, levelId, {
                    onMapPanelClose = function()
                        GameInstance.player.mapManager:RemoveSocialBuildingMarks()
                    end,
                })
            end)
        end
    end
end


SNSFriendSocialBuilding._OnClickExpiredBtn = HL.Method() << function(self)
    if not GameInstance.player.systemUnlockManager:IsSystemUnlockByType(GEnums.UnlockSystemType.FacSocial) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAT_JUMP_UNLOCK_SOCIAL_BUILDING_TOAST)
        return
    end
    local message = self.m_message
    local creatorId = message.sbCreatorId
    if creatorId == GameInstance.player.roleId then

        if message.isSelfAndNoRepeatSocialBuild then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_NOT_EXISTED)
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_INVALID_TOAST)
        end
    else
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_RECEIVE_SOCIAL_BUILDING_INVALID_TOAST)
    end
end


HL.Commit(SNSFriendSocialBuilding)
return SNSFriendSocialBuilding

