
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceShipCharPoster
local SLOT_NAME = "receptionRoomCharPosterSlot"
local MAX_SLOT = Tables.spaceshipConst.charExhibitionMaxCount
local EMPTY_CHAR_ID = "chr_empty"
SpaceShipCharPosterCtrl = HL.Class('SpaceShipCharPosterCtrl', uiCtrl.UICtrl)

SpaceShipCharPosterCtrl.m_charPosterView = HL.Field(HL.Table)

SpaceShipCharPosterCtrl.m_slot2charId = HL.Field(HL.Table)

SpaceShipCharPosterCtrl.m_charId2slot = HL.Field(HL.Table)






SpaceShipCharPosterCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SET_SPACESHIP_CHAR_POSTER] = 'SetSingleCharPoster',
    [MessageConst.ON_CHAR_FRIENDSHIP_CHANGED] = 'FriendshipSlotRefresh',
    [MessageConst.ON_CHAR_FRIENDSHIP_SEND_CHAR_GIFT_SUCCESS] = 'FriendshipSlotRefresh',

    [MessageConst.ON_CHAR_POTENTIAL_UNLOCK] = 'OnCharPotentialUnlock',
    [MessageConst.SET_SPACESHIP_CHAR_POSTER_SERIAL_NUMBER] = 'SetSerialNumberNode',
    [MessageConst.ON_CONFIRM_GENDER] = 'ResetCharPoster',
    [MessageConst.ON_CHAR_POTENTIAL_UNLOCK] = 'ResetCharPosterUI',
    [MessageConst.ON_SPACESHIP_VISIT_FRIEND] = 'ResetCharPoster',
}

SpaceShipCharPosterCtrl.OpenSpaceshipCharPoster = HL.StaticMethod() << function()
    if not Utils.isInSpaceShip() or UIManager:IsOpen(PANEL_ID) then
        return
    end
    UIManager:AutoOpen(PANEL_ID)
end


SpaceShipCharPosterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitCharPoster()
end

SpaceShipCharPosterCtrl.OnShow = HL.Override() << function(self)

end
SpaceShipCharPosterCtrl.OnHide = HL.Override() << function(self)

end
SpaceShipCharPosterCtrl.OnClose = HL.Override() << function(self)

end

SpaceShipCharPosterCtrl._InitCharPoster = HL.Method() << function(self)
    local charPosterPrefab = self:LoadGameObject(string.format(SpaceshipConst.SCENE_UI_PREFAB_PATH, SpaceshipConst.CHAR_POSTER_UI_NAME))
    local charPosterGo = self:_CreateWorldGameObject(charPosterPrefab, true)
    local succ, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
    if not succ then
        return
    end
    charPosterGo.transform:SetParent(level.spaceShipGameLevelRoot.transform, false)
    local success, rootDisplay = GameInstance.dataManager.levelMountPointTable:TryGetData(GameWorld.worldInfo.curLevelId)
    if success then
        local _, node = rootDisplay.subRootByType:TryGetValue(CS.Beyond.Gameplay.LevelMountPointType.CharacterWall)
        if node then
            charPosterGo.transform.position = node.mountPoint.position
            charPosterGo.transform.rotation = Quaternion.Euler(node.mountPoint.rotation)
        end
    end

    self.m_charPosterView = Utils.wrapLuaNode(charPosterGo)
    self.m_slot2charId = {}
    self.m_charId2slot = {}
    for i = 1, MAX_SLOT do
        local info = {index = i}
        self:SetSingleCharPoster(info)
        self:SetSingleCharPoster(info)
    end
    local charIds = GameInstance.player.spaceship:GetCharWallCharTemplateIds()
    if charIds then
        for index = 1, charIds.Count do
            local info = {}
            info.index = index
            info.charId = charIds[CSIndex(index)]
            self:SetSingleCharPoster(info)
        end
    end
end
SpaceShipCharPosterCtrl.SetSerialNumberNode = HL.Method(HL.Boolean) << function(self, visible)
    self.m_charPosterView.serialNumberNode.gameObject:SetActive(visible)
end

SpaceShipCharPosterCtrl.ResetCharPosterUI = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    local charTemplateIds = GameInstance.player.spaceship:GetCharWallCharTemplateIds()
    if charTemplateIds then
        for i = 1, charTemplateIds.Count do
            local serverCharInfo = CharInfoUtils.getPlayerCharInfoByTemplateId(charTemplateIds[CSIndex(i)], GEnums.CharType.Default)
            local info = CharInfoUtils.getSingleCharInfoList(serverCharInfo.instId)
            if info then
                self:SetSingleSlotUI(i, info[1].templateId)
            end
        end
    end
end

SpaceShipCharPosterCtrl.ResetCharPoster = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    local idInfos = {}
    local charTemplateIds = GameInstance.player.spaceship:GetCharWallCharTemplateIds()
    if charTemplateIds then
        for i = 1, charTemplateIds.Count do
            local serverCharInfo = CharInfoUtils.getPlayerCharInfoByTemplateId(charTemplateIds[CSIndex(i)], GEnums.CharType.Default)
            local info = CharInfoUtils.getSingleCharInfoList(serverCharInfo.instId)
            table.insert(idInfos, info[1])
        end
        for index = 1, Tables.spaceshipConst.charExhibitionMaxCount do
            local info = {}
            info.index = index
            if idInfos[index] then
                info.charId = idInfos[index].templateId
            end
            self:SetSingleCharPoster(info)
        end

        if not GameInstance.player.spaceship.isViewingFriend then
            local ids = {}
            for i, info in ipairs(idInfos) do
                table.insert(ids, info.templateId)
            end
            GameInstance.player.spaceship:ChangeGuestRoomCharWallChars(ids)
        end
    end
end

SpaceShipCharPosterCtrl.SetSingleCharPoster = HL.Method(HL.Opt(HL.Table))
    << function(self, args)
    local charId
    local slotIndex
    if not args then
        return
    end
    if args and args.charId then
        charId = args.charId
    end
    if args and args.index then
        slotIndex = args.index
    end
    if self.m_slot2charId[slotIndex] and self.m_slot2charId[slotIndex] == charId then
        return
    end
    if not charId then
        self:_EnqueueSingleCharPoster(slotIndex, EMPTY_CHAR_ID, true)
        return
    end

    if self.m_slot2charId[slotIndex] and self.m_slot2charId[slotIndex] ~= charId then
        self:_EnqueueSingleCharPoster(slotIndex, charId, true)
        return
    end

    if not self.m_slot2charId[slotIndex] then
        self:_EnqueueSingleCharPoster(slotIndex, charId, true)
        return
    end
end

SpaceShipCharPosterCtrl._EnqueueSingleCharPoster = HL.Method(HL.Number, HL.String, HL.Boolean)
    << function(self, slotIndex, charId, playPeekAnim)
    if self.m_slot2charId[slotIndex] and self.m_slot2charId[slotIndex] == charId then
        return
    end
    local slotNode = self.m_charPosterView["slot".. CSIndex(slotIndex)]
    if charId ~= EMPTY_CHAR_ID then
        self.m_slot2charId[slotIndex] = charId
        self.m_charId2slot[charId] = slotIndex
        slotNode.animationWrapper:ClearTween()
        slotNode.gameObject:SetActive(true)
        slotNode.animationWrapper:PlayInAnimation()

        self:SetSingleSlotUI(slotIndex, charId)
    else
        if self.m_slot2charId[slotIndex] then
            self.m_charId2slot[self.m_slot2charId[slotIndex]] = slotIndex
        end
        self.m_slot2charId[slotIndex] = nil
        slotNode.animationWrapper:ClearTween()
        slotNode.animationWrapper:PlayOutAnimation(function()
            slotNode.gameObject:SetActive(false)
        end)
    end
    slotIndex = CSIndex(slotIndex)
    local tex = self.loader:LoadTexture(string.format(SpaceshipConst.CHAR_POSTER_TEXTURE_PATH, charId))
    self.m_charPosterView.receptionRoomCharPosterMesh[SLOT_NAME .. slotIndex].receptionRoomCharPosterHelper:Enqueue(tex)
    if playPeekAnim then
        self.m_charPosterView.receptionRoomCharPosterMesh[SLOT_NAME .. slotIndex].receptionRoomCharPosterHelper:PlayPeekAnim()
    end

    if not self.m_slot2charId[slotIndex] and charId == EMPTY_CHAR_ID then
        return
    end

    if playPeekAnim then
        if charId == EMPTY_CHAR_ID or (self.m_slot2charId[slotIndex] and charId ~= EMPTY_CHAR_ID) then
            AudioAdapter.PostEvent("Au_UI_Event_CharPost_Disappear")
        elseif charId ~= EMPTY_CHAR_ID and not self.m_slot2charId[slotIndex] then
            AudioAdapter.PostEvent("Au_UI_Event_CharPost_Appear")
        end
    end
end

SpaceShipCharPosterCtrl._DequeueSingleCharPoster = HL.Method(HL.Number) << function(self, slotIndex)
    if not self.m_slot2charId[slotIndex] then
        return
    end
    self.m_slot2charId[slotIndex] = nil
    slotIndex = CSIndex(slotIndex)
    self.m_charPosterView.receptionRoomCharPosterMesh[SLOT_NAME .. slotIndex].receptionRoomCharPosterHelper:Dequeue()
end

SpaceShipCharPosterCtrl.SetSingleSlotUI = HL.Method(HL.Number, HL.String) << function(self, slotIndex, charId)
    local slot = self.m_charPosterView["slot".. CSIndex(slotIndex)]
    local potentialLevel = 0
    if GameInstance.player.spaceship.isViewingFriend then
        if charId == Tables.globalConst.maleCharID or charId == Tables.globalConst.femaleCharID then
            charId = Tables.globalConst.endminVirtualCharId
        end
        local charExtraData = GameInstance.player.spaceship:GetFriendExtraCharData(charId)
        slot.potentialStar.view.gameObject:SetActive(charExtraData ~= nil)
        slot.friendshipNode.view.gameObject:SetActive(charExtraData ~= nil)
        if charExtraData then
            slot.potentialStar:InitCharPotentialStarByLevel(charExtraData.potentialLevel)
            slot.friendshipNode:InitFriendshipNodeByFriendShipValue(charExtraData.friendShipValue)
            potentialLevel = charExtraData.potentialLevel
        end
    else
        local serverCharInfo = CharInfoUtils.getPlayerCharInfoByTemplateId(charId, GEnums.CharType.Default)
        slot.potentialStar:InitCharPotentialStar(serverCharInfo.instId)
        slot.friendshipNode:InitFriendshipNode(serverCharInfo.instId)
        potentialLevel = serverCharInfo.potentialLevel
    end

    for i = 0, UIConst.CHAR_MAX_POTENTIAL do
        local index = i + 1
        local potentialCellName = 'cell' .. index
        slot[potentialCellName].gameObject:SetActive(i <= potentialLevel)
    end
end

SpaceShipCharPosterCtrl.FriendshipSlotRefresh = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    for slotIndex = 1, MAX_SLOT do
        local charId = self.m_slot2charId[slotIndex]
        if charId then
            local slot = self.m_charPosterView["slot".. CSIndex(slotIndex)]
            local serverCharInfo = CharInfoUtils.getPlayerCharInfoByTemplateId(charId, GEnums.CharType.Default)
            slot.friendshipNode:InitFriendshipNode(serverCharInfo.instId)
        end
    end
end

SpaceShipCharPosterCtrl.OnCharPotentialUnlock = HL.Method(HL.Table) << function(self, args)
    local charInstId, level = unpack(args)
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local slotIndex
    if charInfo then
        slotIndex = self.m_charId2slot[charInfo.charId]
    end
    if slotIndex then
        local slot = self.m_charPosterView["slot".. CSIndex(slotIndex)]
        slot.potentialStar:InitCharPotentialStar(charInstId)
        for i = 0, UIConst.CHAR_MAX_POTENTIAL do
            local index = i + 1
            local potentialCellName = 'cell' .. index
            slot[potentialCellName].gameObject:SetActive(i <= charInfo.potentialLevel)
        end
    end
end
SpaceShipCharPosterCtrl.OnClose = HL.Override() << function(self)
    CSUtils.ClearUIComponents(self.m_charPosterView.gameObject)
    GameObject.DestroyImmediate(self.m_charPosterView.gameObject)
    self.m_charPosterView = nil
end

HL.Commit(SpaceShipCharPosterCtrl)