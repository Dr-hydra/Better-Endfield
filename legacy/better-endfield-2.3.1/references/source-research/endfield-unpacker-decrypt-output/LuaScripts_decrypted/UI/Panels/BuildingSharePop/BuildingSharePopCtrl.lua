local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BuildingSharePop

BuildingSharePopCtrl = HL.Class('BuildingSharePopCtrl', uiCtrl.UICtrl)






BuildingSharePopCtrl.s_messages = HL.StaticField(HL.Table) << {

}

BuildingSharePopCtrl.m_genBuildingCellFunc = HL.Field(HL.Function)

BuildingSharePopCtrl.m_onClickReplace = HL.Field(HL.Function)


BuildingSharePopCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.m_genBuildingCellFunc = UIUtils.genCachedCellFunction(self.view.buildingList)
    self.view.buildingList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateBuildingCell(self.m_genBuildingCellFunc(object), LuaIndex(csIndex))
    end)
    self.view.buildingList.onGraduallyShowFinish:AddListener(function()
        self:_SetNaviTarget()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    if arg then
        self.m_onClickReplace = arg.onClickReplace
    end
end

BuildingSharePopCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()
end

BuildingSharePopCtrl._UpdateView = HL.Method() << function(self)
    local remoteFactorySystem = GameInstance.player.remoteFactory
    remoteFactorySystem:UpdateReceivedSocialBuildingOrder()
    local buildingCount = remoteFactorySystem.receivedSocialBuildingCount
    self.view.buildingList:UpdateCount(buildingCount)


    self:_SyncPlayerInfos()
end

BuildingSharePopCtrl._SetNaviTarget = HL.Method() << function(self)
    local remoteFactorySystem = GameInstance.player.remoteFactory
    local buildingCount = remoteFactorySystem.receivedSocialBuildingCount
    if buildingCount > 0 then
        local firstObject = self.view.buildingList:Get(0)
        local firstCell = firstObject and self.m_genBuildingCellFunc(firstObject) or nil
        if firstCell then
            self:SetNaviTarget(firstCell.naviDecorator)
        end
    end
end

BuildingSharePopCtrl._OnUpdateBuildingCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local remoteFactorySystem = GameInstance.player.remoteFactory
    local buildingInfo = remoteFactorySystem:GetReceivedSocialBuildingInfo(CSIndex(luaIndex))
    local nodeId = buildingInfo.nodeId
    local chapterId = buildingInfo.chapterId
    local nodeHandler = FactoryUtils.getBuildingNodeHandler(nodeId, chapterId)
    if not nodeHandler then
        logger.error("[SocialBuilding] Received social building not found, nodeId: " .. tostring(nodeId))
        cell.transform.localScale = Vector3.zero
        return
    end
    cell.transform.localScale = Vector3.one

    local buildingId = nodeHandler.templateId
    local buildingData = Tables.factoryBuildingTable:GetValue(buildingId)
    cell.buildingNameText.text = buildingData.name

    self:_UpdatePlayerName(cell, buildingInfo)

    local levelId = buildingInfo.levelId
    local levelDescExisted, levelDescData = Tables.levelDescTable:TryGetValue(levelId)
    local levelName = ""
    if levelDescExisted then
        levelName = levelDescData.showName
    end
    local levelBasicExisted, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(levelId)
    local domainName = ""
    if levelBasicExisted then
        local domainData = Tables.domainDataTable:GetValue(levelBasicInfo.domainName)
        domainName = domainData.domainName
    end
    cell.mapLevelNameText.text = string.format("%s-%s", levelName, domainName)

    local stabilityValue = buildingInfo.stability
    cell.stabilityValueText.text = string.format("%.0f%%", stabilityValue * 100)

    cell.buildingImage:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_IMAGE, string.format("image_%s", buildingId))

    cell.replaceBtn.onClick:RemoveAllListeners()
    cell.replaceBtn.onClick:AddListener(function()
        if self.m_onClickReplace then
            local success, errorMsg = xpcall(self.m_onClickReplace, debug.traceback, buildingInfo.levelId, buildingInfo.nodeId, buildingInfo.ownerId)
            if not success then
                logger.error("Invoke replace callback failed, error: " .. tostring(errorMsg))
            end
        end
        self:PlayAnimationOutAndClose()
    end)

    cell.receiveDateTimeText.text = Utils.timestampToDateYMDHM(buildingInfo.createTimestamp)
end

BuildingSharePopCtrl._UpdatePlayerName = HL.Method(HL.Table, HL.Userdata) << function(self, cell, buildingInfo)
    self:_UpdatePlayerName_Internal(buildingInfo.ownerId, cell.ownerNameText,
        Language.LUA_FRIEND_NAME, Language.LUA_FRIEND_REMAKE_NAME)
    if buildingInfo.ownerId == buildingInfo.sharedRoleId then
        cell.sharedNameText.text = ""
    else
        self:_UpdatePlayerName_Internal(buildingInfo.sharedRoleId, cell.sharedNameText,
            Language.LUA_FRIEND_SOCIAL_BUILDING_SHARED_NAME, Language.LUA_FRIEND_SOCIAL_BUILDING_SHARED_REMAKE_NAME)
    end
end

BuildingSharePopCtrl._UpdatePlayerName_Internal = HL.Method(HL.Number, HL.Userdata, HL.String, HL.String)
    << function(self, roleId, nameText, nameFormat, remakeNameFormat)
    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if not success or not playerInfo.init then
        nameText.text = ""
        return
    end

    local playerName
    if string.isEmpty(playerInfo.remakeName) then
        playerName = string.format(nameFormat, playerInfo.name, playerInfo.shortId)
    else
        playerName = string.format(remakeNameFormat, playerInfo.remakeName, playerInfo.name, playerInfo.shortId)
    end
    nameText.text = playerName
end


local SYNC_BATCH_SIZE = 10

BuildingSharePopCtrl._SyncPlayerInfos = HL.Method() << function(self)
    local remoteFactorySystem = GameInstance.player.remoteFactory
    local buildingCount = remoteFactorySystem.receivedSocialBuildingCount
    if buildingCount <= 0 then
        return
    end

    local roleCount = 0
    local roleIds = {}
    local seenRoleIds = {}
    for i = 0, buildingCount - 1 do
        local buildingInfo = remoteFactorySystem:GetReceivedSocialBuildingInfo(i)
        if not seenRoleIds[buildingInfo.ownerId] then
            roleCount = roleCount + 1
            roleIds[roleCount] = buildingInfo.ownerId
            seenRoleIds[buildingInfo.ownerId] = true
        end
        if not seenRoleIds[buildingInfo.sharedRoleId] then
            roleCount = roleCount + 1
            roleIds[roleCount] = buildingInfo.sharedRoleId
            seenRoleIds[buildingInfo.sharedRoleId] = true
        end
    end



    if roleCount > 0 then
        self:_SyncPlayerInfos_Internal(roleIds, 1)
    end
end

BuildingSharePopCtrl._SyncPlayerInfos_Internal = HL.Method(HL.Table, HL.Number) << function(self, roleIds, fromIndex)
    if fromIndex > #roleIds then
        return
    end

    local toIndex = math.min(fromIndex + SYNC_BATCH_SIZE - 1, #roleIds)
    local batch = {}
    for i = fromIndex, toIndex do
        batch[i - fromIndex + 1] = roleIds[i]
    end

    GameInstance.player.friendSystem:SyncSocialFriendInfo(batch, function()
        if self.m_isClosed then
            return
        end
        self:_UpdatePlayerNames()
        self:_SyncPlayerInfos_Internal(roleIds, toIndex + 1)
    end)
end

BuildingSharePopCtrl._UpdatePlayerNames = HL.Method() << function(self)
    self.view.buildingList:UpdateShowingCells(function(csIndex, object)
        local cell = self.m_genBuildingCellFunc(object)

        local remoteFactorySystem = GameInstance.player.remoteFactory
        local buildingInfo = remoteFactorySystem:GetReceivedSocialBuildingInfo(csIndex)


        self:_UpdatePlayerName(cell, buildingInfo)
    end)
end

BuildingSharePopCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(BuildingSharePopCtrl)
