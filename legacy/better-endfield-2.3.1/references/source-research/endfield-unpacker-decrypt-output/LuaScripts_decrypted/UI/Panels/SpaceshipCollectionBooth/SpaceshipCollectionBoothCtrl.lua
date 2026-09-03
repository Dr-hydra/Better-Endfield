
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipCollectionBooth
local PHASE_ID = PhaseId.SpaceshipCollectionBooth
SpaceshipCollectionBoothCtrl = HL.Class('SpaceshipCollectionBoothCtrl', uiCtrl.UICtrl)






SpaceshipCollectionBoothCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SPACESHIP_ON_SHOWCASE_MODIFY] = 'SpaceshipOnShowcaseModify',
}


SpaceshipCollectionBoothCtrl.m_showingItemList = HL.Field(HL.Boolean) << false

SpaceshipCollectionBoothCtrl.m_slotCells = HL.Field(HL.Forward('UIListCache'))

SpaceshipCollectionBoothCtrl.m_getItemCell = HL.Field(HL.Function)

SpaceshipCollectionBoothCtrl.m_level = HL.Field(CS.Beyond.Gameplay.Core.SpaceShipGameLevel)


SpaceshipCollectionBoothCtrl.OpenSpaceshipShowcasePanel = HL.StaticMethod() << function()
    PhaseManager:OpenPhase(PHASE_ID)
end


SpaceshipCollectionBoothCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.backBtn.onClick:AddListener(function()
        self:_TryExecute(function()
            self:_ToggleItemList(false)
        end, Language.LUA_SS_SHOWCASE_EXIT_SLOT_NOT_SAVE_HINT)
    end)
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "spaceship_showcase")
    end)

    self.view.leftArrowBtn.onClick:AddListener(function()
        self:_TryExecute(function()
            self:_OnClickSlot((CSIndex(self.m_curSlotIndex) - 1) % #self.m_slotInfos + 1)
        end, Language.LUA_SS_SHOWCASE_CHANGE_SLOT_NOT_SAVE_HINT)
    end)
    self.view.rightArrowBtn.onClick:AddListener(function()
        self:_TryExecute(function()
            self:_OnClickSlot((CSIndex(self.m_curSlotIndex) + 1) % #self.m_slotInfos + 1)
        end, Language.LUA_SS_SHOWCASE_CHANGE_SLOT_NOT_SAVE_HINT)
    end)

    self.view.itemInfoNode.placementBtn.onClick:AddListener(function()
        self:_ToggleItemList(true)
    end)
    self.view.itemInfoNode.replaceBtn.onClick:AddListener(function()
        self:_ToggleItemList(true)
    end)
    self.view.itemInfoNode.confirmBtn.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)

    self.m_slotCells = UIUtils.genCellCache(self.view.slotCell)

    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.itemScrollList)
    self.view.itemScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateItemCell(self.m_getItemCell(obj), LuaIndex(csIndex))
    end)

    do
        local prefab = self.loader:LoadGameObject(SpaceshipConst.SHOWCASE_LOWER_SLOT_EFFECT_PREFAB)
        self.m_lowerSlotEffect = self:_CreateWorldGameObject(prefab)
        self.m_lowerSlotEffect:SetActive(false)
    end
    do
        local prefab = self.loader:LoadGameObject(SpaceshipConst.SHOWCASE_UPPER_SLOT_EFFECT_PREFAB)
        self.m_upperSlotEffect = self:_CreateWorldGameObject(prefab)
        self.m_upperSlotEffect:SetActive(false)
    end

    local _, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
    self.m_level = level
    self.m_lvData = level.data
    self:_InitCamera()
    level:HideCharacters(self.m_lvData.showcaseRootPos, self.view.config.HIDE_CHAR_RADIUS)

    self:_InitItemInfos()
    self:_RefreshSlots()
    do
        local playerPos = GameUtil.playerPos - self.m_lvData.showcaseRootPos
        local nearSlotIndex, minDist
        for k, v in pairs(self.m_lvData.showcaseSlotLocalPosInfoList) do
            local dist = (playerPos - v.Item1).magnitude
            if not nearSlotIndex or dist < minDist then
                nearSlotIndex = LuaIndex(k)
                minDist = dist
            end
        end
        self:_OnClickSlot(nearSlotIndex, true)
    end
    self.view.emptyCell.placeStateController:SetState("Empty")
    self.view.emptyCell.button.onClick:AddListener(function()
        if self.m_showingItemList then
            self:_OnClickItem(0)
        end
    end)

    self:_ToggleItemList(false, true)
    local recoverState = arg and arg.recoverState or nil
    if recoverState then
        self:_TryRecoverState(recoverState)
        arg.recoverState = nil
    end
    if arg and arg.instructionBookId then
        UIManager:Open(PanelId.InstructionBook, arg.instructionBookId)
        arg.instructionBookId = nil
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    local data = CS.HG.Rendering.Runtime.HGDepthOfFieldData(CS.HG.Rendering.Runtime.HGDepthOfFieldType.Circle, 0, 0, 0,
            self.view.config.CAM_DOF_FAR_FOCUS_START, self.view.config.CAM_DOF_FAR_FOCUS_END, self.view.config.CAM_DOF_FAR_RADIUS)
    Utils.enableCameraDOF(data)
end

SpaceshipCollectionBoothCtrl.OnShow = HL.Override() << function(self)
    self:_SetCurItemAsNaviTarget()
end

SpaceshipCollectionBoothCtrl.OnClose = HL.Override() << function(self)
    Utils.disableCameraDOF()
    self:_ClearCamera()
    local _, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
    level:ShowCharacters()
end

SpaceshipCollectionBoothCtrl._SetCurItemAsNaviTarget = HL.Method() << function(self)
    if self.m_showingItemList then
        if self.m_curItemIndex == 0 then
            self:SetNaviTarget(self.view.emptyCell.button)
        else
            self.view.itemScrollList:ScrollToIndex(self.m_curItemIndex, true)
            self:SetNaviTarget(self.m_getItemCell(self.m_curItemIndex).button)
        end
    else
        local cell = self.m_slotCells:Get(self.m_curSlotIndex)
        if cell and cell.toggle then
            self:SetNaviTarget(cell.toggle)
        end
    end
end

SpaceshipCollectionBoothCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {
        recoverState = {
            slotId = self:_GetCurSlotId(),
            showingItemList = self.m_showingItemList,
        }
    }
    if self.m_showingItemList then
        arg.recoverState.isEmptySelected = self.m_curItemIndex == 0
        arg.recoverState.selectedItemId = self:_GetCurChooseItemId()
    end
    if PhaseManager:GetTopPhaseId() == PHASE_ID then
        local isBookOpen, bookCtrl = UIManager:IsOpen(PanelId.InstructionBook)
        if isBookOpen and bookCtrl and bookCtrl:IsShow() and bookCtrl.id == "spaceship_showcase" then
            arg.instructionBookId = bookCtrl.id
        end
    end
    return arg
end

SpaceshipCollectionBoothCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    local recoverSlotIndex = self:_GetRecoverSlotIndex(recoverState)
    self:_OnClickSlot(recoverSlotIndex, true)
    if not recoverState.showingItemList then
        return
    end
    self:_ToggleItemList(true, true)
    local recoverItemIndex = self:_GetRecoverItemIndex(recoverState)
    if recoverItemIndex ~= self.m_curItemIndex then
        self:_OnClickItem(recoverItemIndex)
    else
        self:_UpdateContent()
    end
    self:_SetCurItemAsNaviTarget()
end

SpaceshipCollectionBoothCtrl._GetRecoverSlotIndex = HL.Method(HL.Table).Return(HL.Number) << function(self, recoverState)
    local recoverSlotId = recoverState.slotId
    if string.isEmpty(recoverSlotId) then
        return self.m_curSlotIndex
    end
    for index, info in ipairs(self.m_slotInfos) do
        if info.id == recoverSlotId then
            return index
        end
    end
    return self.m_curSlotIndex
end

SpaceshipCollectionBoothCtrl._GetRecoverItemIndex = HL.Method(HL.Table).Return(HL.Number) << function(self, recoverState)
    if recoverState.isEmptySelected then
        return 0
    end
    local recoverItemId = recoverState.selectedItemId
    if not string.isEmpty(recoverItemId) then
        for index, info in ipairs(self.m_itemInfos) do
            if info.id == recoverItemId then
                return index
            end
        end
    end
    return self:_GetCurItemIndex()
end

SpaceshipCollectionBoothCtrl.SpaceshipOnShowcaseModify = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_SS_SHOWCASE_SAVE_SUCC)
    self:_RefreshItemList()
end

SpaceshipCollectionBoothCtrl._TryExecute = HL.Method(HL.Function, HL.String) << function(self, action, hint)
    if not self.m_showingItemList or self:_IsCurChooseIsCurrentPlaced() then
        action()
    else
        Notify(MessageConst.SHOW_POP_UP, {
            content = hint,
            onConfirm = function()
                action()
                self.m_level.showcaseItemManager:RefreshItems()
                self:_UpdateCameraState()
            end
        })
    end
end




SpaceshipCollectionBoothCtrl.m_slotInfos = HL.Field(HL.Table)

SpaceshipCollectionBoothCtrl.m_curSlotIndex = HL.Field(HL.Number) << 1

SpaceshipCollectionBoothCtrl._RefreshSlots = HL.Method() << function(self)
    local slotTable = Tables.spaceshipShowcaseTable
    self.m_slotInfos = {}
    for k, v in pairs(slotTable) do
        table.insert(self.m_slotInfos, {
            id = k,
            data = v,
            sortId = v.sortId,
        })
    end
    table.sort(self.m_slotInfos, Utils.genSortFunction({"sortId"}, true))

    self.m_slotCells:Refresh(#self.m_slotInfos, function(cell, index)
        local info = self.m_slotInfos[index]
        cell.nameTxt.text = info.data.name
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function()
            self:_OnClickSlot(index)
        end)
        self:_UpdateSlotState(index, cell, info)
    end)
end

SpaceshipCollectionBoothCtrl._UpdateSlotState = HL.Method(HL.Number, HL.Opt(HL.Table, HL.Table)) << function(self, index, cell, info)
    if not cell then
        cell = self.m_slotCells:Get(index)
        info = self.m_slotInfos[index]
    end
    if index == self.m_curSlotIndex then
        cell.toggle:SetIsOnWithoutNotify(true)
        cell.stateController:SetState("Choose")
    else
        cell.toggle:SetIsOnWithoutNotify(false)
        local isEmpty = string.isEmpty(GameInstance.player.spaceship:GetShowcaseItemAt(info.id))
        cell.stateController:SetState(isEmpty and "Empty" or "Normal")
    end
end

SpaceshipCollectionBoothCtrl._OnClickSlot = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, isInit)
    if index == self.m_curSlotIndex and not isInit then
        return
    end
    local oldIndex = self.m_curSlotIndex
    self.m_curSlotIndex = index
    self:_UpdateSlotState(oldIndex)
    self:_UpdateSlotState(index)
    self:_RefreshItemList()
    self:_UpdateAreaInfo()
    self:_SetCurItemAsNaviTarget()
    self:_UpdateCameraState(isInit)
    self:_UpdateSlotEffect()
    AudioAdapter.PostEvent("au_int_system_spaceship_showcase_light")
end

SpaceshipCollectionBoothCtrl._UpdateAreaInfo = HL.Method() << function(self)
    local node = self.view.areaInfoNode
    for k, v in ipairs(self.m_slotInfos) do
        local cell = node["slot" .. k]
        cell.nameTxt.text = v.data.name
        local isCurrent = k == self.m_curSlotIndex
        if isCurrent then
            cell.stateController:SetState("Choose")
            node.curSlotTxt.text = v.data.name
        else
            local hasItem = GameInstance.player.spaceship:GetShowcaseItemAt(v.id) ~= nil
            cell.stateController:SetState(hasItem and "Exhibits" or "NoExhibits")
        end
    end
end






SpaceshipCollectionBoothCtrl.m_itemInfos = HL.Field(HL.Table)

SpaceshipCollectionBoothCtrl.m_curItemIndex = HL.Field(HL.Number) << 0

SpaceshipCollectionBoothCtrl._InitItemInfos = HL.Method() << function(self)
    self.m_itemInfos = {}
    local list = Tables.itemListByTypeTable[GEnums.ItemType.SpaceshipExhibition].list
    for _, id in pairs(list) do
        if Utils.getItemCount(id) > 0 then
            local data = Tables.itemTable[id]
            table.insert(self.m_itemInfos, {
                id = id,
                data = data,
                showcaseItemData = Tables.spaceshipShowcaseItemTable[id],
                sortId1 = data.sortId1,
                sortId2 = data.sortId2,
                rarity = data.rarity,
            })
        end
    end
end

SpaceshipCollectionBoothCtrl._UpdateItemInfoOrder = HL.Method() << function(self)
    local slotInfo = self.m_slotInfos[self.m_curSlotIndex]
    for _, v in ipairs(self.m_itemInfos) do
        local slotId = GameInstance.player.spaceship:GetItemBelongShowcaseId(v.id)
        v.isCurrentSortId = slotId == slotInfo.id and 0 or 1
        v.usedSortId = string.isEmpty(slotId) and 0 or 1
    end
    table.sort(self.m_itemInfos, Utils.genSortFunction({ "isCurrentSortId", "usedSortId", "sortId1", "sortId2", "rarity" }, true))
end

SpaceshipCollectionBoothCtrl._ToggleItemList = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, isInit)
    self.m_showingItemList = active

    self.view.backBtn.gameObject:SetActive(active)
    self.view.closeBtn.gameObject:SetActive(not active)
    if isInit then
        self.view.itemListNode.gameObject:SetActive(active)
        self.view.slotListNode.gameObject:SetActive(not active)
    else
        self.view.itemListNode.gameObject:SetActive(true)
        self.view.slotListNode.gameObject:SetActive(true)
        if active then
            self.view.bottomNode:PlayInAnimation(function()
                self.view.itemListNode.gameObject:SetActive(true)
                self.view.slotListNode.gameObject:SetActive(false)
            end)
        else
            self.view.bottomNode:PlayOutAnimation(function()
                self.view.itemListNode.gameObject:SetActive(false)
                self.view.slotListNode.gameObject:SetActive(true)
            end)
        end
    end

    if active then
        self:_SetCurItemAsNaviTarget()
        self:_UpdateContent()
    else
        if not isInit then
            local index = self:_GetCurItemIndex()
            if index ~= self.m_curItemIndex then
                self:_OnClickItem(index)
            else
                self:_UpdateContent()
            end
            self:_SetCurItemAsNaviTarget()
        end
    end

    self.view.arrowNode:SetState(DeviceInfo.usingTouch and (active and "MobileExhibitNode" or "MobileBoothNode") or "Normal")
end

SpaceshipCollectionBoothCtrl._RefreshItemList = HL.Method() << function(self)
    self:_UpdateItemInfoOrder()
    self.m_curItemIndex = self:_GetCurItemIndex()
    self:_UpdateItemChooseState(0)
    self.view.emptyCell.isCurrentHint.gameObject:SetActive(self.m_curItemIndex == 0)
    self.view.itemScrollList:UpdateCount(#self.m_itemInfos, true)
    self:_SetCurItemAsNaviTarget()
    self:_OnClickItem(self.m_curItemIndex)
end

SpaceshipCollectionBoothCtrl._GetCurItemIndex = HL.Method().Return(HL.Number) << function(self)
    local curItemId = GameInstance.player.spaceship:GetShowcaseItemAt(self:_GetCurSlotId())
    if curItemId then
        for k, v in ipairs(self.m_itemInfos) do
            if v.id == curItemId then
                return k
            end
        end
    end
    return 0
end

SpaceshipCollectionBoothCtrl._OnUpdateItemCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)

    local itemCell = cell
    local info = self.m_itemInfos[index]

    itemCell.icon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, info.data.iconId)
    itemCell.nameTxt.text = info.data.name
    self:_UpdateTagNode(itemCell.collectionTagNode, info.id)
    itemCell.button.onClick:RemoveAllListeners()
    itemCell.button.onClick:AddListener(function()
        if self.m_showingItemList then
            self:_OnClickItem(index)
        end
    end)

    local slotId = GameInstance.player.spaceship:GetItemBelongShowcaseId(info.id)
    local isCurrent = slotId == self:_GetCurSlotId()
    itemCell.isCurrentHint.gameObject:SetActive(isCurrent)
    if not slotId then
        itemCell.placeStateController:SetState("Normal")
    else
        itemCell.placeStateController:SetState(isCurrent and "Normal" or "AlreadyPlaced")
        itemCell.slotTxt.text = string.format(Language.LUA_SS_SHOWCASE_SLOT_FORMAT, Tables.spaceshipShowcaseTable[slotId].name)
    end
    self:_UpdateItemChooseState(index, itemCell)
end

SpaceshipCollectionBoothCtrl._UpdateTagNode = HL.Method(HL.Table, HL.Opt(HL.String)) << function(self, tagNode, itemId)
    if not tagNode.m_tagCells then
        tagNode.m_tagCells = UIUtils.genCellCache(tagNode.tagCell)
    end
    if string.isEmpty(itemId) then
        tagNode.m_tagCells:Refresh(1, function(cell, _)
            cell.stateController:SetState("Empty")
        end)
        return
    end
    local data = Tables.spaceshipShowcaseItemTable[itemId]

    local tagShowData = {}
    for i, tagId in pairs(data.tagIds) do
        local tagData = Tables.tagDataTable[tagId]
        if tagData and not tagData.hideTag then
            table.insert(tagShowData,
                {
                    tagId = tagId,
                    tagName = tagData.tagName
                })
        end
    end
    tagNode.m_tagCells:Refresh(#tagShowData, function(cell, index)
        local tagData = tagShowData[index]
        cell.stateController:SetState("Normal")
        cell.nameTxt.text = tagData.tagName
    end)
end

SpaceshipCollectionBoothCtrl._OnClickItem = HL.Method(HL.Number) << function(self, index)
    local oldIndex = self.m_curItemIndex
    self.m_curItemIndex = index
    self:_UpdateItemChooseState(oldIndex)
    self:_UpdateItemChooseState(index)
    self:_UpdateContent()
    self.m_level.showcaseItemManager:PreviewItemAt(self:_GetCurChooseItemId(), self:_GetCurSlotId())
    self:_UpdateCameraState()
end

SpaceshipCollectionBoothCtrl._UpdateItemChooseState = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, index, cell)
    if index == 0 then
        self.view.emptyCell.chooseStateController:SetState(index == self.m_curItemIndex and "EmptyChoose" or "NotChoose")
    else
        cell = cell or self.m_getItemCell(index)
        if cell then
            cell.chooseStateController:SetState(index == self.m_curItemIndex and "NormalChoose" or "NotChoose")
        end
    end
end






SpaceshipCollectionBoothCtrl._UpdateContent = HL.Method() << function(self)
    local curPlacedItemId = GameInstance.player.spaceship:GetShowcaseItemAt(self:_GetCurSlotId())
    local itemId
    if self.m_showingItemList then
        if self.m_curItemIndex > 0 then
            itemId = self.m_itemInfos[self.m_curItemIndex].id
        end
    else
        itemId = curPlacedItemId
    end
    local node = self.view.itemInfoNode
    if itemId then
        local itemData = Tables.itemTable[itemId]
        node.nameTxt.text = itemData.name
        node.descTxt.text = itemData.decoDesc
        self:_UpdateTagNode(node.tagNode, itemId)
    else
        node.nameTxt.text = Language.LUA_SS_SHOWCASE_EMPTY_NANE
        node.descTxt.text = Language.LUA_SS_SHOWCASE_EMPTY_DESC
        self:_UpdateTagNode(node.tagNode)
    end
    node.placementBtn.gameObject:SetActive(not self.m_showingItemList and not itemId)
    node.replaceBtn.gameObject:SetActive(not self.m_showingItemList and itemId ~= nil)
    node.isCurrentBtn.gameObject:SetActive(self.m_showingItemList and curPlacedItemId == itemId)
    node.confirmBtn.gameObject:SetActive(self.m_showingItemList and curPlacedItemId ~= itemId)
end

SpaceshipCollectionBoothCtrl._OnClickConfirm = HL.Method() << function(self)
    local curSlotId = self:_GetCurSlotId()
    local itemId
    if self.m_curItemIndex > 0 then
        itemId = self.m_itemInfos[self.m_curItemIndex].id
        local oriSlotId = GameInstance.player.spaceship:GetItemBelongShowcaseId(itemId)
        if oriSlotId == curSlotId then
            return
        end
        if oriSlotId ~= nil then
            local oriSlotName = Tables.spaceshipShowcaseTable[oriSlotId].name
            local curSlotName = Tables.spaceshipShowcaseTable[curSlotId].name
            Notify(MessageConst.SHOW_POP_UP, {
                content = string.format(Language.LUA_SS_SHOWCASE_CONFIRM_MOVE_PLACED_ITEM, oriSlotName, curSlotName),
                onConfirm = function()

                    self:_EventLogOnConfirm(itemId)

                    GameInstance.player.spaceship:ModifySpaceshipShowcase(curSlotId, itemId)
                end
            })
            return
        end
    end


    self:_EventLogOnConfirm(itemId)

    GameInstance.player.spaceship:ModifySpaceshipShowcase(curSlotId, itemId)
end

SpaceshipCollectionBoothCtrl._EventLogOnConfirm = HL.Method(HL.Opt(HL.String)) << function(self, itemId)

    local curSlotId = self:_GetCurSlotId()
    local beforeIds = {}
    local afterIds = {}
    local places = {}
    local curPlacedItemId = GameInstance.player.spaceship:GetShowcaseItemAt(curSlotId)
    table.insert(beforeIds, curPlacedItemId == nil and '' or curPlacedItemId)
    table.insert(afterIds, itemId == nil and '' or itemId)
    table.insert(places, curSlotId)
    EventLogManagerInst:GameEvent_PersonalDecoration(beforeIds, afterIds, places, "show_case", nil, nil)
end



SpaceshipCollectionBoothCtrl._GetCurSlotId = HL.Method().Return(HL.String) << function(self)
    return self.m_slotInfos[self.m_curSlotIndex].id
end

SpaceshipCollectionBoothCtrl._GetCurChooseItemId = HL.Method().Return(HL.Opt(HL.String)) << function(self)
    if self.m_curItemIndex > 0 then
        return self.m_itemInfos[self.m_curItemIndex].id
    end
    return nil
end

SpaceshipCollectionBoothCtrl._IsCurChooseIsCurrentPlaced = HL.Method().Return(HL.Boolean) << function(self)
    local chooseItemId = self:_GetCurChooseItemId()
    local curPlacedItemId = GameInstance.player.spaceship:GetShowcaseItemAt(self:_GetCurSlotId())
    return chooseItemId == curPlacedItemId
end





SpaceshipCollectionBoothCtrl.m_camCtrl = HL.Field(CS.Beyond.Gameplay.View.SimpleCameraController)

SpaceshipCollectionBoothCtrl.m_camTransposer = HL.Field(CS.Cinemachine.CinemachineTransposer)

SpaceshipCollectionBoothCtrl.m_camComposer = HL.Field(CS.Cinemachine.CinemachineComposer)

SpaceshipCollectionBoothCtrl.m_camTarget = HL.Field(CS.UnityEngine.Transform)

SpaceshipCollectionBoothCtrl.m_lvData = HL.Field(CS.Beyond.Gameplay.SpaceShipSpecificData)

SpaceshipCollectionBoothCtrl.m_camTween = HL.Field(HL.Any)

SpaceshipCollectionBoothCtrl._InitCamera = HL.Method() << function(self)
    self.m_camTarget = self:_CreateEmptyWorldGameObject("SpaceshipShowcaseTarget").transform
    self.m_camTarget.position = self.m_lvData.showcaseRootPos

    self.m_camCtrl = CameraManager:LoadPersistentController("SpaceshipShowcaseCamera")
    local vCam = self.m_camCtrl.virtualCamera
    vCam.Follow = self.m_camTarget
    vCam.LookAt = self.m_camTarget
    self.m_camTransposer = CSUtils.GetCinemachineTransposer(vCam)
    self.m_camComposer = CSUtils.GetCinemachineComposer(vCam)
end

SpaceshipCollectionBoothCtrl._ClearCamera = HL.Method() << function(self)
    self:_ClearCamTween()
    GameObject.Destroy(self.m_camTarget.gameObject)
    self.m_camTarget = nil
    CameraManager:RemoveCameraController(self.m_camCtrl)
    self.m_camCtrl = nil
    self.m_camTransposer = nil
    self.m_camComposer = nil
end

SpaceshipCollectionBoothCtrl._UpdateCameraState = HL.Method(HL.Opt(HL.Boolean)) << function(self, noTween)
    local slotInfo = self.m_lvData.showcaseSlotLocalPosInfoList[CSIndex(self.m_curSlotIndex)]
    local localPos, localRot = slotInfo.Item1, slotInfo.Item2

    local itemId = self:_GetCurChooseItemId()
    local curItem = itemId and self.m_level.showcaseItemManager:GetLoadedItem(itemId)


    local newRotation = Quaternion.LookRotation(Vector3(localPos.x, 0, localPos.z), Vector3.up)


    local bodyOffsetY = localPos.y + (curItem and curItem.followExtraOffsetY or self.view.config.EMPTY_SLOT_FOLLOW_EXTRA_OFFSET_Y)
    local aimOffsetY = localPos.y + (curItem and curItem.aimExtraOffsetY or self.view.config.EMPTY_SLOT_AIM_EXTRA_OFFSET_Y)


    local bodyOffsetZ = localPos:XZ().magnitude + (curItem and curItem.followExtraOffsetZ or self.view.config.EMPTY_SLOT_FOLLOW_EXTRA_OFFSET_Z)

    local newBodyOffset = Vector3(0, bodyOffsetY, bodyOffsetZ)
    local newAimOffset = Vector3(0, aimOffsetY, 0)
    self:_ClearCamTween()
    if noTween then
        self.m_camTarget.rotation = newRotation
        self.m_camTransposer.m_FollowOffset = newBodyOffset
        self.m_camComposer.m_TrackedObjectOffset = newAimOffset
    else
        local oldAngleY = self.m_camTarget.eulerAngles.y
        local angleY = newRotation.eulerAngles.y
        if math.abs(angleY - oldAngleY) > 180 then
            if angleY > oldAngleY then
                oldAngleY = oldAngleY + 360
            else
                oldAngleY = oldAngleY - 360
            end
        end

        local oldBodyOffset = self.m_camTransposer.m_FollowOffset
        local oldAimOffset = self.m_camComposer.m_TrackedObjectOffset

        local percent = 0
        self.m_camTween = DOTween.To(function()
            return percent
        end, function(value)
            percent = value
            self.m_camTarget.eulerAngles = Vector3(0, lume.lerp(oldAngleY, angleY, percent), 0)
            self.m_camTransposer.m_FollowOffset = lume.lerp(oldBodyOffset, newBodyOffset, percent)
            self.m_camComposer.m_TrackedObjectOffset = lume.lerp(oldAimOffset, newAimOffset, percent)
        end, 1, self.view.config.CAM_MOVE_DURATION)
        self.m_camTween:SetEase(CS.DG.Tweening.Ease.OutQuint)
    end
end

SpaceshipCollectionBoothCtrl._ClearCamTween = HL.Method() << function(self)
    if self.m_camTween then
        self.m_camTween:Kill()
        self.m_camTween = nil
    end
end




SpaceshipCollectionBoothCtrl.m_lowerSlotEffect = HL.Field(GameObject)

SpaceshipCollectionBoothCtrl.m_upperSlotEffect = HL.Field(GameObject)

SpaceshipCollectionBoothCtrl._UpdateSlotEffect = HL.Method() << function(self)
    local useLower, pos, rot = unpack(SpaceshipConst.SHOWCASE_SLOT_EFFECT_INFO[self.m_curSlotIndex])
    self.m_lowerSlotEffect:SetActive(useLower)
    self.m_upperSlotEffect:SetActive(not useLower)
    local effect = useLower and self.m_lowerSlotEffect or self.m_upperSlotEffect
    effect.transform.position = pos
    effect.transform.eulerAngles = rot
end


HL.Commit(SpaceshipCollectionBoothCtrl)
