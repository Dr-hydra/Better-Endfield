
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')

local PANEL_STATE = {
    CHAR = "Char",
    WEAPON = "Weapon",
}


local WEAPON_INDEX = {
     [GEnums.WeaponType.Wand] = 1,
     [GEnums.WeaponType.Sword] = 2,
     [GEnums.WeaponType.Claymores] = 3,
     [GEnums.WeaponType.Lance] = 4,
     [GEnums.WeaponType.Pistol] = 5,
}

local WEAPON_TYPE_TO_INDEX = {
    GEnums.WeaponType.Wand,
    GEnums.WeaponType.Sword,
    GEnums.WeaponType.Claymores,
    GEnums.WeaponType.Lance,
    GEnums.WeaponType.Pistol,
}

local MAX_WEAPON_INDEX = 5

local WEAPON_MAX_NUM = {
    [GEnums.WeaponType.Wand] = Tables.spaceshipConst.wandExhibitionMaxCount,
    [GEnums.WeaponType.Sword] = Tables.spaceshipConst.swordExhibitionMaxCount,
    [GEnums.WeaponType.Claymores] = Tables.spaceshipConst.claymoresExhibitionMaxCount,
    [GEnums.WeaponType.Lance] = Tables.spaceshipConst.lanceExhibitionMaxCount,
    [GEnums.WeaponType.Pistol] = Tables.spaceshipConst.pistolExhibitionMaxCount,
}

local TOP_BAR_STATE = {
    [GEnums.WeaponType.Wand] = "Wand",
    [GEnums.WeaponType.Sword] = "Sword",
    [GEnums.WeaponType.Claymores] = "Claymores",
    [GEnums.WeaponType.Lance] = "Lance",
    [GEnums.WeaponType.Pistol] = "Pistol",
}

SSReceptionRoomPosterCtrl = HL.Class('SSReceptionRoomPosterCtrl', uiCtrl.UICtrl)

SSReceptionRoomPosterCtrl.m_curState = HL.Field(HL.String) << ""

SSReceptionRoomPosterCtrl.m_curWeaponType = HL.Field(GEnums.WeaponType)

SSReceptionRoomPosterCtrl.m_cameraSlotInfos = HL.Field(HL.Table)

SSReceptionRoomPosterCtrl.m_selectInsIdList = HL.Field(HL.Table)

SSReceptionRoomPosterCtrl.m_curSlotIndex = HL.Field(HL.Number) << 1

SSReceptionRoomPosterCtrl.m_unSaveInsIdList = HL.Field(HL.Table)

SSReceptionRoomPosterCtrl.m_isSave = HL.Field(HL.Boolean) << true

SSReceptionRoomPosterCtrl.m_switchRoleThread = HL.Field(HL.Thread)

SSReceptionRoomPosterCtrl.m_level = HL.Field(CS.Beyond.Gameplay.Core.SpaceShipGameLevel)






SSReceptionRoomPosterCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SPACESHIP_GUEST_ROOM_WEAPON_CHANGE] = '_OnSaveWeapon',
    [MessageConst.ON_SPACESHIP_GUEST_ROOM_CHAR_CHANGE] = '_OnSaveChar',
}



SSReceptionRoomPosterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local recoverState
    if type(arg) == "table" and (arg.recoverState ~= nil or arg.weaponType ~= nil) then
        self.m_curWeaponType = arg.weaponType
        recoverState = arg.recoverState
    else
        self.m_curWeaponType = arg
    end
    self.view.btnClose.onClick:AddListener(function()
        if not self.m_isSave then
            Notify(MessageConst.SHOW_POP_UP,{
                content = Language.LUA_SS_POSTER_UN_SAVE_CLOSE_POPUP,
                onConfirm = function()
                    if self.m_curState == PANEL_STATE.WEAPON then
                        self:_SetWeaponPoster(self.m_unSaveInsIdList)
                    else
                        self:_SetCharPoster(self.m_unSaveInsIdList)
                    end
                    PhaseManager:PopPhase(PhaseManager:GetTopPhaseId())
                end
            })
        else
            PhaseManager:PopPhase(PhaseManager:GetTopPhaseId())
        end
    end)

    self.view.saveBtn.onClick:AddListener(function()
        self:_OnClickSaveBtn()
    end)
    self.view.resetBtn.onClick:AddListener(function()
        self:_OnClickResetBtn()
    end)

    self.view.rightArrowBtn.onClick:AddListener(function()
        self:_OnClickSwitchBtn(false)
    end)

    self.view.leftArrowBtn.onClick:AddListener(function()
        self:_OnClickSwitchBtn(true)
    end)
    local _, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
    self.m_level = level
    local success, rootDisplay = GameInstance.dataManager.levelMountPointTable:TryGetData(GameWorld.worldInfo.curLevelId)
    if success then
        local _, node = rootDisplay.subRootByType:TryGetValue(CS.Beyond.Gameplay.LevelMountPointType.CharacterWall)
        if node then
            self.m_level:HideCharacters(node.mountPoint.position, 15)
        end
    end

    self:_SetMainState()
    if recoverState then
        self:_TryRecoverState(recoverState)
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

SSReceptionRoomPosterCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local listWidget = self.m_curState == PANEL_STATE.WEAPON and self.view.weaponScrollList or self.view.charScrollList
    local sortNode = listWidget and listWidget.view and listWidget.view.sortNode or nil
    local filterBtn = listWidget and listWidget.view and listWidget.view.filterBtn or nil
    local recoverState = {
        selectedInstIdList = {},
        isSave = self.m_isSave,
    }
    for _, item in ipairs(self.m_selectInsIdList or {}) do
        if item and item.instId ~= nil then
            table.insert(recoverState.selectedInstIdList, item.instId)
        end
    end
    if sortNode then
        recoverState.sortSelectedIndex = sortNode:GetCurSelectedIndex()
        recoverState.sortIsIncremental = sortNode.isIncremental
    end
    if filterBtn and filterBtn.m_args then
        recoverState.filterTags = lume.deepCopy(filterBtn.m_args.selectedTags)
    end
    if self.m_curState == PANEL_STATE.WEAPON then
        recoverState.weaponType = self.m_curWeaponType
    end
    return {
        weaponType = self.m_curWeaponType,
        recoverState = recoverState,
    }
end


SSReceptionRoomPosterCtrl.OnShow = HL.Override() << function(self)
    if self.m_curWeaponType then
        self.m_level:SetWeaponWallActiveState(self.m_curWeaponType, true)
    end
end

SSReceptionRoomPosterCtrl.OnHide = HL.Override() << function(self)

end

SSReceptionRoomPosterCtrl.OnClose = HL.Override() << function(self)
    self:_ClearCamera()
    self.m_level:ShowCharacters()
    Notify(MessageConst.SET_SPACESHIP_CHAR_POSTER_SERIAL_NUMBER, false)
    if self.m_curWeaponType then
        self.m_level:SetWeaponWallActiveState(self.m_curWeaponType, false)
    end
end


SSReceptionRoomPosterCtrl._SetMainState = HL.Method() << function(self)
    if self.m_curWeaponType then
        self.m_curState = PANEL_STATE.WEAPON
        self.view.main:SetState(self.m_curState)
        self.m_curSlotIndex = WEAPON_INDEX[self.m_curWeaponType]
        self:RefreshWeaponScroll()
        self:_InitCamera()
        self:_RefreshArrowState()
    else
        self.m_curState = PANEL_STATE.CHAR
        self.view.main:SetState(self.m_curState)
        self.m_curSlotIndex = 1
        self:RefreshCharScroll()
        self:_InitCamera()
        Notify(MessageConst.SET_SPACESHIP_CHAR_POSTER_SERIAL_NUMBER, true)
    end
    self:SetSaveState(true)
end

SSReceptionRoomPosterCtrl._InitCamera = HL.Method() << function(self)
    self.m_cameraSlotInfos = {}
    local cameraConfig = GameInstance.dataManager.spaceshipCameraConfig

    if self.m_curState == PANEL_STATE.WEAPON then
        for type, index in pairs(WEAPON_INDEX) do
            local targetSuccess, targetData = cameraConfig.spaceshipWeaponWallCameraConfig:TryGetValue(type)
            if targetSuccess then
                self.m_cameraSlotInfos[index] = targetData
            end
        end
    else
        local currentAspect = Screen.width / Screen.height
        local config = cameraConfig.spaceshipCharWallCameraConfig
        local ultraWideConfig = config.spaceshipUltraWideConfig
        local squareConfig = config.spaceshipSquareConfig
        local standardConfig = config.spaceshipStandardConfig

        currentAspect = math.max(squareConfig.aspect, math.min(ultraWideConfig.aspect, currentAspect))
        local targetAspect = (currentAspect - ultraWideConfig.aspect) / (squareConfig.aspect - ultraWideConfig.aspect)
        local targetData = {}
        if targetAspect < config.standardRange.x  or targetAspect > config.standardRange.y then
            targetData.targetPosition = lume.lerp(ultraWideConfig.targetPosition, squareConfig.targetPosition, targetAspect)
            targetData.targetRotation = lume.lerp(ultraWideConfig.targetRotation, squareConfig.targetRotation, targetAspect)
        else
            targetData.targetPosition = standardConfig.targetPosition
            targetData.targetRotation = standardConfig.targetRotation
        end
        targetData.targetFOV = config.targetFOV
        targetData.blendData = {}
        targetData.blendData.blendTime = config.blendData.blendTime
        targetData.blendData.blendStyle = config.blendData.blendStyle
        targetData.blendData.blendCurve = config.blendData.blendCurve
        self.m_cameraSlotInfos[self.m_curSlotIndex] = targetData
    end
    self:_UpdateCameraState()
end


SSReceptionRoomPosterCtrl._ClearCamera = HL.Method() << function(self)
    local cameraConfig = GameInstance.dataManager.spaceshipCameraConfig
    if self.m_curState == PANEL_STATE.WEAPON then
        CameraUtils.DoCommonTempBlendOut(
            cameraConfig.spaceshipWeaponWallExitBlendData.blendTime,
            cameraConfig.spaceshipWeaponWallExitBlendData.blendStyle,
            cameraConfig.spaceshipWeaponWallExitBlendData.blendCurve
        )
    else
        CameraUtils.DoCommonTempBlendOut(
            cameraConfig.spaceshipCharWallExitBlendData.blendTime,
            cameraConfig.spaceshipCharWallExitBlendData.blendStyle,
            cameraConfig.spaceshipCharWallExitBlendData.blendCurve
        )
    end
end


SSReceptionRoomPosterCtrl._OnClickSwitchBtn = HL.Method(HL.Boolean) <<function(self, isLeft)
    local switchFunc = function()
        if isLeft then
            self.m_curSlotIndex = self.m_curSlotIndex - 1
            if self.m_curSlotIndex <= 1 then
                self.m_curSlotIndex = 1
            end
        else
            self.m_curSlotIndex = self.m_curSlotIndex + 1
            if self.m_curSlotIndex >= MAX_WEAPON_INDEX then
                self.m_curSlotIndex = MAX_WEAPON_INDEX
            end
        end
        self:SetSaveState(true)
        self:_RefreshArrowState()
    end

    if not self.m_isSave then
        Notify(MessageConst.SHOW_POP_UP,{
            content = Language.LUA_SS_POSTER_UN_SAVE_SWITCH_POPUP,
            onConfirm = function()
                self:_SetWeaponPoster(self.m_unSaveInsIdList)
                switchFunc()
            end
        })
    else
        switchFunc()
    end
end


SSReceptionRoomPosterCtrl._RefreshArrowState = HL.Method() << function(self)
    self.view.leftArrowBtn.gameObject:SetActive(self.m_curSlotIndex > 1)
    self.view.rightArrowBtn.gameObject:SetActive(self.m_curSlotIndex < MAX_WEAPON_INDEX)
    self:_UpdateCameraState()
    self.m_level:SetWeaponWallActiveState(self.m_curWeaponType, false)
    self.m_curWeaponType = WEAPON_TYPE_TO_INDEX[self.m_curSlotIndex]
    self.m_level:SetWeaponWallActiveState(self.m_curWeaponType, true)
    self:RefreshWeaponScroll()
end

SSReceptionRoomPosterCtrl.RefreshWeaponScroll = HL.Method() << function(self)
    if self.m_curState ~= PANEL_STATE.WEAPON then
        return
    end
    self.view.topBar:SetState(TOP_BAR_STATE[self.m_curWeaponType])
    self.m_selectInsIdList = {}
    local ids = GameInstance.player.spaceship:GetWeaponInstIdsByType(self.m_curWeaponType)
    if ids then
        for i = 1, ids.Count do
            local res, itemBundle = GameInstance.player.inventory:TryGetWeaponInst(Utils.getCurrentScope(), ids[CSIndex(i)])
            if res then
                local templateId = itemBundle.id
                local instId = itemBundle.instId or 0
                local instData = itemBundle.instData
                local _, itemCfg = Tables.itemTable:TryGetValue(templateId)

                if not itemCfg then
                    logger.error("Can't get itemCfg for templateId: " .. templateId)
                else
                    local itemInfo = FilterUtils.processWeapon(templateId, instId)
                    itemInfo.itemCfg = itemCfg
                    itemInfo.itemInst = instData
                    table.insert(self.m_selectInsIdList, itemInfo)
                end
            end
        end
    end
    self.view.weaponScrollList:InitWeaponPosterScrollList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.CHAR_INFO_WEAPON,
        defaultSelected = self.m_selectInsIdList,
        filter_weaponType = self.m_curWeaponType,
        select_num = WEAPON_MAX_NUM[self.m_curWeaponType],
        onClickItem = function(weaponList)
            self:_WeaponListChangeSelectIndex(weaponList)
        end
    })
    self.m_unSaveInsIdList = lume.deepCopy(self.m_selectInsIdList)
end

SSReceptionRoomPosterCtrl.SetSaveState = HL.Method(HL.Boolean) << function(self, isSave)
    if self.m_isSave == isSave then
        self.view.saveBtn.gameObject:SetActive(not self.m_isSave)
        return
    end
    if isSave then
        self.m_unSaveInsIdList = lume.deepCopy(self.m_selectInsIdList)
        self.view.saveBtn.gameObject:SetActive(false)
    else
        self.view.saveBtn.gameObject:SetActive(true)
    end
    self.m_isSave = isSave
end


SSReceptionRoomPosterCtrl.RefreshCharScroll = HL.Method() << function(self)
    if self.m_curState ~= PANEL_STATE.CHAR then
        return
    end
    self.view.topBar:SetState("Operators")
    self.m_selectInsIdList = {}
    local ids = GameInstance.player.spaceship:GetCharWallCharTemplateIds()
    if ids then
        for i = 1, ids.Count do
            local serverCharInfo = CharInfoUtils.getPlayerCharInfoByTemplateId(ids[CSIndex(i)], GEnums.CharType.Default)
            local info = CharInfoUtils.getSingleCharInfoList(serverCharInfo.instId)
            table.insert(self.m_selectInsIdList, info[1])
        end
    end

    local info = {
        selectNum = Tables.spaceshipConst.charExhibitionMaxCount,
        mode = UIConst.CharListMode.MultiSelect,
    }
    self.view.charScrollList:InitCharPosterScrollList(info)
    self.view.charScrollList:SetUpdateCellFunc(nil, function(select, cellIndex, charItem, charItemList, charInfoList)
        self:_CharListChangeSelectIndex(select, cellIndex, charItem, charItemList, charInfoList)
    end)
    self.view.charScrollList:UpdateCharItems(CharInfoUtils.getCharInfoList())
    self.view.charScrollList:ShowSelectChars(self.m_selectInsIdList, false, true)
    self.m_unSaveInsIdList = lume.deepCopy(self.m_selectInsIdList)
end

SSReceptionRoomPosterCtrl._CharListChangeSelectIndex = HL.Method(HL.Boolean, HL.Number, HL.Table, HL.Table, HL.Table)
    << function(self, select, cellIndex, charItem, charItemList, charInfoList)
    self:_SetCharPoster(charItemList)
    self.m_selectInsIdList = {}
    for _, item in ipairs(charItemList) do
        table.insert(self.m_selectInsIdList, item)
    end
    self.view.charScrollList:ShowSelectChars(self.m_selectInsIdList)
    self:SetSaveState(false)
end

SSReceptionRoomPosterCtrl._SetCharPoster = HL.Method(HL.Table) << function(self, targetItemList)
    if self.m_switchRoleThread ~= nil then
        CoroutineManager:ClearCoroutine(self.m_switchRoleThread)
        self.m_switchRoleThread = nil
    end
    local switchTime = #self.m_selectInsIdList > #targetItemList and self.view.config.CHAR_POSTER_SWITCH_TIME or 0
    self.m_switchRoleThread = CoroutineManager:StartCoroutine(function()
        for index = 1, Tables.spaceshipConst.charExhibitionMaxCount do
            local info = {}
            info.index = index
            if targetItemList[index] then
                info.charId = targetItemList[index].templateId
            end
            Notify(MessageConst.SET_SPACESHIP_CHAR_POSTER, info)
            coroutine.wait(switchTime)
        end
        CoroutineManager:ClearCoroutine(self.m_switchRoleThread)
        self.m_switchRoleThread = nil
    end)
end

SSReceptionRoomPosterCtrl._SetWeaponPoster = HL.Method(HL.Table) << function(self, targetItemList)
    for index = 1, WEAPON_MAX_NUM[self.m_curWeaponType] do
        local info = {}
        if targetItemList[index] then
            info.id = targetItemList[index].indexId
            info.index = index
        end
        self.m_level.weaponWallItemManager:SetItemToSlot(self.m_curWeaponType, CSIndex(index), info.id or -1)
        Notify(MessageConst.SET_SPACESHIP_WEAPON_POSTER, info)
    end
end

SSReceptionRoomPosterCtrl._WeaponListChangeSelectIndex = HL.Method(HL.Table) << function(self, itemList)
    self:_SetWeaponPoster(itemList)
    self.m_selectInsIdList = {}
    for _, item in ipairs(itemList) do
        table.insert(self.m_selectInsIdList, item)
    end
    self:SetSaveState(false)
end

SSReceptionRoomPosterCtrl._UpdateCameraState = HL.Method() << function(self)
    local targetData = self.m_cameraSlotInfos[self.m_curSlotIndex]
    CameraUtils.DoCommonTempBlendIn(
        targetData.targetPosition,
        targetData.targetRotation,
        targetData.targetFOV,
        targetData.blendData.blendTime,
        targetData.blendData.blendStyle,
        targetData.blendData.blendCurve,
        false, false
    )
end

SSReceptionRoomPosterCtrl._OnClickSaveBtn = HL.Method() << function(self)
    local ids = {}
    local guestRoomId = Tables.spaceshipConst.guestRoomId
    local guestRoomTypeStr = tostring(GEnums.SpaceshipRoomType.GuestRoom)
    if self.m_curState == PANEL_STATE.CHAR then
        for i, info in ipairs(self.m_selectInsIdList) do
            table.insert(ids, info.templateId)
        end


        local beforeIds = {}
        local afterIds = {}
        local places = {}
        local prevIds = GameInstance.player.spaceship:GetCharWallCharTemplateIds()
        for i = 1, Tables.spaceshipConst.charExhibitionMaxCount do
            local prevId = (prevIds ~= nil and i <= prevIds.Count) and prevIds[CSIndex(i)] or nil
            local afterId = ids[i]
            if prevId ~= afterId then
                table.insert(beforeIds, prevId == nil and '' or prevId)
                table.insert(afterIds, afterId == nil and '' or afterId)
                table.insert(places, tostring(i))
            end
        end
        if #places > 0 then
            EventLogManagerInst:GameEvent_PersonalDecoration(beforeIds, afterIds, places, "character_wall", guestRoomTypeStr, guestRoomId)
        end

        GameInstance.player.spaceship:ChangeGuestRoomCharWallChars(ids)
    elseif self.m_curState == PANEL_STATE.WEAPON then
        for i, info in ipairs(self.m_selectInsIdList) do
            table.insert(ids, info.instId)
        end


        local beforeIds = {}
        local afterIds = {}
        local places = {}
        local prevIds = GameInstance.player.spaceship:GetWeaponInstIdsByType(self.m_curWeaponType)
        for i = 1, WEAPON_MAX_NUM[self.m_curWeaponType] do
            local prevInstId = (prevIds ~= nil and i <= prevIds.Count) and prevIds[CSIndex(i)] or nil
            local _, prevBundle = GameInstance.player.inventory:TryGetWeaponInst(Utils.getCurrentScope(), prevInstId)
            local prevId = prevBundle ~= nil and prevBundle.id or nil
            local afterInstId = ids[i]
            local _, afterBundle = GameInstance.player.inventory:TryGetWeaponInst(Utils.getCurrentScope(), afterInstId)
            local afterId = afterBundle ~= nil and afterBundle.id or nil
            if prevId ~= afterId then
                table.insert(beforeIds, prevId == nil and '' or prevId)
                table.insert(afterIds, afterId == nil and '' or afterId)
                table.insert(places, tostring(self.m_curWeaponType) .. tostring(i))
            end
        end
        if #places > 0 then
            EventLogManagerInst:GameEvent_PersonalDecoration(beforeIds, afterIds, places, "weapon_wall", guestRoomTypeStr, guestRoomId)
        end

        GameInstance.player.spaceship:ChangeGuestRoomWeaponWallWeapons(self.m_curWeaponType, ids)
    end
    self:SetSaveState(true)
end

SSReceptionRoomPosterCtrl._OnSaveWeapon = HL.Method() << function(self)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_SS_POSTER_SAVE_POPUP)
end

SSReceptionRoomPosterCtrl._OnSaveChar = HL.Method() << function(self)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_SS_POSTER_SAVE_POPUP)
end

SSReceptionRoomPosterCtrl._OnClickResetBtn = HL.Method() << function(self)
    if #self.m_selectInsIdList == 0 then
        return
    end
    if self.m_curState == PANEL_STATE.CHAR then
        self:_SetCharPoster({})
        self.m_selectInsIdList = {}
        self.view.charScrollList:ShowSelectChars(self.m_selectInsIdList)
    elseif self.m_curState == PANEL_STATE.WEAPON then
        for index = 1, WEAPON_MAX_NUM[self.m_curWeaponType] do
            local info = {}
            info.index = index
            self.m_level.weaponWallItemManager:SetItemToSlot(self.m_curWeaponType, CSIndex(index), -1)
            Notify(MessageConst.SET_SPACESHIP_WEAPON_POSTER, info)
        end
        self.m_selectInsIdList = {}
        self.view.weaponScrollList:ShowSelectItems(self.m_selectInsIdList)
    end
    self:SetSaveState(false)
end

SSReceptionRoomPosterCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    if self.m_curState == PANEL_STATE.WEAPON and recoverState.weaponType ~= nil and recoverState.weaponType ~= self.m_curWeaponType then
        self.m_curWeaponType = recoverState.weaponType
        self.m_curSlotIndex = WEAPON_INDEX[self.m_curWeaponType] or self.m_curSlotIndex
        self:_RefreshArrowState()
    end

    if self.m_curState == PANEL_STATE.WEAPON then
        self:_RecoverWeaponPosterState(recoverState)
    else
        self:_RecoverCharPosterState(recoverState)
    end
    if recoverState.isSave == false then
        self:SetSaveState(false)
    else
        self:SetSaveState(true)
    end
end

SSReceptionRoomPosterCtrl._BuildRecoverSelectedItemList = HL.Method(HL.Opt(HL.Table, HL.Table)).Return(HL.Table) << function(self, sourceItems, selectedInstIdList)
    local selectedItems = {}
    if not sourceItems or not selectedInstIdList then
        return selectedItems
    end
    local itemLookup = {}
    for _, item in ipairs(sourceItems) do
        if item and item.instId ~= nil then
            itemLookup[item.instId] = item
        end
    end
    for _, instId in ipairs(selectedInstIdList) do
        local item = itemLookup[instId]
        if item then
            table.insert(selectedItems, item)
        end
    end
    return selectedItems
end

SSReceptionRoomPosterCtrl._RestoreListSortFilterState = HL.Method(HL.Any, HL.Table) << function(self, listWidget, recoverState)
    if not listWidget or not listWidget.view then
        return
    end
    local sortNode = listWidget.view.sortNode
    local filterBtn = listWidget.view.filterBtn
    local sortSelectedIndex = recoverState.sortSelectedIndex
    if sortSelectedIndex and sortNode and sortNode.m_sortOptions and sortNode.view and sortNode.view.mobilePCNode and sortNode.view.mobilePCNode.dropDown then
        local optionCount = #sortNode.m_sortOptions
        if optionCount > 0 then
            sortSelectedIndex = math.max(1, math.min(sortSelectedIndex, optionCount))
            sortNode.isIncremental = recoverState.sortIsIncremental == true
            sortNode:RefreshIncremental()
            sortNode.view.mobilePCNode.dropDown:SetSelected(CSIndex(sortSelectedIndex), true, false)
        end
    end
    local filterTags = recoverState.filterTags and lume.deepCopy(recoverState.filterTags) or {}
    listWidget.m_selectedTags = filterTags
    if filterBtn and filterBtn.m_args then
        filterBtn.m_args.selectedTags = lume.deepCopy(filterTags)
    end
    if sortNode then
        sortNode:UpdateDeviceState()
    end
end

SSReceptionRoomPosterCtrl._RecoverWeaponPosterState = HL.Method(HL.Table) << function(self, recoverState)
    local listWidget = self.view.weaponScrollList
    local selectedItems = self:_BuildRecoverSelectedItemList(listWidget and listWidget.m_itemInfoList or nil, recoverState.selectedInstIdList)
    self.m_selectInsIdList = selectedItems
    self:_SetWeaponPoster(selectedItems)
    self:_RestoreListSortFilterState(listWidget, recoverState)
    listWidget.m_curSelectItemInfos = selectedItems
    listWidget:_OnFilterConfirm(listWidget.m_selectedTags)
    listWidget:ShowSelectItems(selectedItems)
end

SSReceptionRoomPosterCtrl._RecoverCharPosterState = HL.Method(HL.Table) << function(self, recoverState)
    local listWidget = self.view.charScrollList
    local selectedItems = self:_BuildRecoverSelectedItemList(listWidget and listWidget.m_charItems or nil, recoverState.selectedInstIdList)
    self.m_selectInsIdList = selectedItems
    self:_SetCharPoster(selectedItems)
    self:_RestoreListSortFilterState(listWidget, recoverState)
    listWidget:ShowSelectChars(selectedItems, false, true)
end

HL.Commit(SSReceptionRoomPosterCtrl)
