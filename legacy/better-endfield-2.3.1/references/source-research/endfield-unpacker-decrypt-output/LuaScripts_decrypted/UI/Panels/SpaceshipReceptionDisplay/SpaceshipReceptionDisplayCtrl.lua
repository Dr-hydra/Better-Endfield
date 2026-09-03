
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipReceptionDisplay
local PHASE_ID = PhaseId.SpaceshipReceptionDisplay
SpaceshipReceptionDisplayCtrl = HL.Class('SpaceshipReceptionDisplayCtrl', uiCtrl.UICtrl)

SpaceshipReceptionDisplayCtrl.m_infoList = HL.Field(HL.Table)

SpaceshipReceptionDisplayCtrl.m_filteredInfoList = HL.Field(HL.Table)

SpaceshipReceptionDisplayCtrl.m_selectedTags = HL.Field(HL.Table)

SpaceshipReceptionDisplayCtrl.m_isSave = HL.Field(HL.Boolean) << false

SpaceshipReceptionDisplayCtrl.GetCell = HL.Field(HL.Function)

SpaceshipReceptionDisplayCtrl.cell2Select = HL.Field(HL.Table)

SpaceshipReceptionDisplayCtrl.m_select2Cell = HL.Field(HL.Table)

SpaceshipReceptionDisplayCtrl.m_maxSelectNum = HL.Field(HL.Number) << 1

SpaceshipReceptionDisplayCtrl.m_nowSelectNum = HL.Field(HL.Number) << 0

SpaceshipReceptionDisplayCtrl.m_pictureRedDots = HL.Field(HL.Table)

SpaceshipReceptionDisplayCtrl.m_nowNaviPictureCell = HL.Field(HL.Userdata)

SpaceshipReceptionDisplayCtrl.m_sortIsIncremental = HL.Field(HL.Boolean) << false

SpaceshipReceptionDisplayCtrl.m_sortOptData = HL.Field(HL.Table)

SpaceshipReceptionDisplayCtrl.m_redDotShowCor = HL.Field(HL.Thread)

SpaceshipReceptionDisplayCtrl.m_viewDetailBindingId = HL.Field(HL.Number) << 0






SpaceshipReceptionDisplayCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SPACESHIP_GUEST_ROOM_SET_CHAR_PICTURE_LIST] = '_OnSavePicture',
    [MessageConst.ON_READ_NEW_SS_PICTURE] = '_UpdateRedDotByInfoList',
    [MessageConst.ON_SPACESHIP_HEAD_NAVI_TARGET_CHANGE] = 'OnCellNaviTargetChange',
}

SpaceshipReceptionDisplayCtrl.ResetPicture = HL.StaticMethod() << function()
    local picIdsList = GameInstance.player.spaceship:GetScreenWallPicIds()
    local picIdsTable = {}
    if picIdsList and picIdsList.Count > 0 then
        for i = CSIndex(1), CSIndex(picIdsList.Count) do
            local pictureId = picIdsList[i]
            local succ, data = Tables.pictureGenderTable:TryGetValue(pictureId)
            if succ and tonumber(GameInstance.player.playerInfoSystem.gender) ~= data.gender then
                pictureId = data.reversePictureId
            end
            table.insert(picIdsTable, pictureId)
        end
        GameInstance.player.spaceship:ChangeGuestRoomScreenWallPics(picIdsTable)
    end
end


SpaceshipReceptionDisplayCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        if not self.m_isSave then
            Notify(MessageConst.SHOW_POP_UP,{
                content = Language.LUA_SS_POSTER_UN_SAVE_CLOSE_POPUP,
                onConfirm = function()
                    PhaseManager:PopPhase(PHASE_ID)
                end
            })
        else
            PhaseManager:PopPhase(PHASE_ID)
        end
    end)

    self.view.btnSave.onClick:AddListener(function()
        local tmpTable = {}
        local tmpPicTable = {}
        for _, cellIndex in pairs(self.m_select2Cell) do
            local item = self.m_filteredInfoList[cellIndex]
            local picId = item.posterData.pictureId
            if not tmpPicTable[picId] then
                table.insert(tmpTable, picId)
                tmpPicTable[picId] = true
            end
        end


        local guestRoomId = Tables.spaceshipConst.guestRoomId
        local guestRoomTypeStr = tostring(GEnums.SpaceshipRoomType.GuestRoom)
        local picIdsList = GameInstance.player.spaceship:GetScreenWallPicIds()
        local beforeIds = {}
        local afterIds = {}
        local places = {}
        for i = 1, Tables.spaceshipConst.charPictureMaxCount do
            local prevId = (picIdsList and i <= picIdsList.Count) and picIdsList[CSIndex(i)] or nil
            local afterId = tmpTable[i]
            if prevId ~= afterId then
                table.insert(beforeIds, prevId == nil and '' or prevId)
                table.insert(afterIds, afterId == nil and '' or afterId)
                table.insert(places, tostring(i))
            end
        end
        if #places > 0 then
            EventLogManagerInst:GameEvent_PersonalDecoration(beforeIds, afterIds, places, "screen_wall", guestRoomTypeStr, guestRoomId)
        end


        GameInstance.player.spaceship:ChangeGuestRoomScreenWallPics(tmpTable)
        self:SetSaveState(true)
    end)

    self.view.resetBtn.onClick:AddListener(function()
        if self.m_nowSelectNum > 0 then
            self:SetSaveState(false)
        end
        self:ShowSelectItems()
    end)

    self.GetCell = UIUtils.genCachedCellFunction(self.view.pictureScroll)
    self.view.pictureScroll.onSelectedCell:AddListener(function(obj, csIndex)
        local cellIndex = LuaIndex(csIndex)
        self:OnClickItem(cellIndex)
    end)

    self.view.pictureScroll.onUpdateCell:AddListener(function(object, index)
        local cellIndex = LuaIndex(index)
        self:OnUpdateCell(object, cellIndex)
    end)

    self.m_viewDetailBindingId = self:BindInputPlayerAction("ss_view_detail_picture", function()
        if self.m_nowNaviPictureCell then
            self.m_nowNaviPictureCell:OpenPicturePanel()
        end
    end)

    self.m_selectedTags = {}
    self.m_select2Cell = {}
    self.cell2Select = {}
    self.m_filteredInfoList = {}
    self.m_infoList = {}
    self.m_maxSelectNum = Tables.spaceshipConst.charPictureMaxCount
    local charPotentialIndex2Infos, _ = CharInfoUtils.GetAllCharPotentialInfos()
    if #charPotentialIndex2Infos >= 1 then
        self.view.commonState:SetState("Normal")
    else
        self.view.commonState:SetState("Null")
    end

    self:_InitSortNode()
    self:_InitFilterNode()
    self:UpdatePictureItems(charPotentialIndex2Infos)
    local picIdsList = GameInstance.player.spaceship:GetScreenWallPicIds()
    local picIdsTable = {}
    if picIdsList and picIdsList.Count > 0 then
        for i = CSIndex(1), CSIndex(picIdsList.Count) do
            table.insert(picIdsTable, picIdsList[i])
        end
    end
    local recoverState = arg and arg.recoverState or nil
    if recoverState then
        self:_TryRecoverState(recoverState)
    else
        self:ShowSelectItems(picIdsTable)
        self:SetSaveState(true)
    end
    self:_TryRecoverPicturePopup(arg and arg.picturePopupState or nil)
    self.m_redDotShowCor = self:_StartCoroutine(function()
        while true do
            coroutine.wait(0.2)
            self:_UpdateRedDot()
        end
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

SpaceshipReceptionDisplayCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local recoverState = {
        selectedPictureIds = self:_GetSelectedPictureIdList(),
        sortSelectedIndex = self.view.sortNode:GetCurSelectedIndex(),
        sortIsIncremental = self.view.sortNode.isIncremental,
        isSave = self.m_isSave,
    }
    if self.view.filterBtn.m_args then
        recoverState.filterTags = lume.deepCopy(self.view.filterBtn.m_args.selectedTags)
    end
    local selectedIndex = self.view and self.view.pictureScroll and LuaIndex(self.view.pictureScroll.curSelectedIndex) or 0
    local selectedInfo = selectedIndex > 0 and self.m_filteredInfoList[selectedIndex] or nil
    if selectedInfo and selectedInfo.posterData then
        recoverState.currentPictureId = selectedInfo.posterData.pictureId
    elseif self.m_nowNaviPictureCell and self.m_nowNaviPictureCell.m_pictureId then
        recoverState.currentPictureId = self.m_nowNaviPictureCell.m_pictureId
    end
    local arg = {
        recoverState = recoverState,
    }
    local isOpen, popupCtrl = UIManager:IsOpen(PanelId.ReceptionDisplayPicture)
    if isOpen and popupCtrl and popupCtrl:IsShow() and PhaseManager:GetTopPhaseId() == PHASE_ID and popupCtrl.GetRecoverStateArg then
        arg.picturePopupState = popupCtrl:GetRecoverStateArg()
    end
    return arg
end

SpaceshipReceptionDisplayCtrl.OnClose = HL.Override() << function(self)

end

SpaceshipReceptionDisplayCtrl.OnIntScreen = HL.StaticMethod() << function()
    PhaseManager:OpenPhase(PHASE_ID)
end

SpaceshipReceptionDisplayCtrl.OnCellNaviTargetChange = HL.Method(HL.Opt(HL.Userdata)) << function(self, cell)
    self.m_nowNaviPictureCell = cell
end

SpaceshipReceptionDisplayCtrl.OnUpdateCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local cell = self:GetCellByIndex(index)
    local item = self.m_filteredInfoList[index]

    cell:InitSSPictureCell(item.posterData.pictureId, self.m_filteredInfoList, function(arg)
        self:OnClickItem(index)
    end)
    cell:SelectIndex(self.cell2Select[index])
    local isOld = GameInstance.player.spaceship:GetPictureRedDotReadState(item.posterData.pictureId)
    cell:UpdateRedDotState(not isOld)
    if index == 1 then
        self:SetNaviTarget(cell.view.pictureBtn)
    end
    self:_UpdateRedDot()
end

SpaceshipReceptionDisplayCtrl.OnClickItem = HL.Method(HL.Number) << function(self, cellIndex)
    local cell = self:GetCellByIndex(cellIndex)
    local cellSelectIndex = self:_GetCellSelectIndex(cellIndex)

    GameInstance.player.spaceship:ReadPictureRedDot(self.m_filteredInfoList[cellIndex].posterData.pictureId)
    cell:UpdateRedDotState(false)

    if self.m_nowSelectNum >= self.m_maxSelectNum and self.cell2Select[cellIndex] == nil then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_WALL_LIMIT_MAX)
        return
    end

    if cellSelectIndex > 0 then
        local index = self.cell2Select[cellIndex]
        self.m_select2Cell[index] = nil
        self.cell2Select[cellIndex] = nil
        self.m_filteredInfoList[cellIndex].selectIndex = nil
        self.m_nowSelectNum = self.m_nowSelectNum - 1
        cell:SelectIndex()
    elseif self.m_nowSelectNum < self.m_maxSelectNum then
        local curIndex = self:_GetNextIndex()
        self.m_select2Cell[curIndex] = cellIndex
        self.cell2Select[cellIndex] = curIndex
        self.m_filteredInfoList[cellIndex].selectIndex = curIndex
        self.m_nowSelectNum = curIndex
    end
    local itemList = self:_UpdateMultiSelect()
    self:UpdatePicCount(#itemList, self.m_maxSelectNum)
    self:SetSaveState(false)
end

SpaceshipReceptionDisplayCtrl.UpdatePicCount = HL.Method(HL.Number, HL.Number) << function(self, nowNum, maxNum)
    self.view.picCountTxt.text = string.format("%d<size=26>/%d</size>", nowNum, maxNum)
    self.view.lvDotNode:InitLvDotNode(nowNum, maxNum)
end

SpaceshipReceptionDisplayCtrl._UpdateMultiSelect = HL.Method(HL.Opt(HL.Boolean)).Return(HL.Table) << function(self)
    local result = {}
    local itemList = {}
    for _, cellIndex in pairs(self.m_select2Cell) do
        table.insert(result, cellIndex)
    end

    self.cell2Select = {}
    self.m_select2Cell = {}

    for index, cellIndex in pairs(result) do
        local cell = self:GetCellByIndex(cellIndex)
        self.cell2Select[cellIndex] = index
        self.m_select2Cell[index] = cellIndex
        if cell then
            cell:SelectIndex(index)
        end
        local item = self.m_filteredInfoList[cellIndex]
        table.insert(itemList, item)
    end
    return itemList
end

SpaceshipReceptionDisplayCtrl.ShowSelectItems = HL.Method(HL.Opt(HL.Table)) << function(self, items)

    self.cell2Select = {}
    self.m_select2Cell = {}
    if items then
        self.m_nowSelectNum = #items
        for index, picId in pairs(items) do
            local cellIndex = self:_GetIndexByPicId(picId)
            self.cell2Select[cellIndex] = index
            self.m_select2Cell[index] = cellIndex
        end
    else
        self.m_nowSelectNum = 0
    end
    self:UpdatePicCount(self.m_nowSelectNum, self.m_maxSelectNum)
    self:_ShowMultiItems()
    self:_OnFilterConfirm(self.m_selectedTags)
end

SpaceshipReceptionDisplayCtrl._ShowMultiItems = HL.Method() << function(self)
    for cellIndex = 1, self.view.pictureScroll.count do
        local cell = self:GetCellByIndex(cellIndex)
        if cell then
            cell:SelectIndex(self.cell2Select[cellIndex])
        end
    end
end

SpaceshipReceptionDisplayCtrl._GetIndexByPicId = HL.Method(HL.Any).Return(HL.Number) << function(self, pictureId)
    for index = 1, #self.m_filteredInfoList do
        local charItem = self.m_filteredInfoList[index]
        if charItem.posterData.pictureId == pictureId then
            return index
        end
    end
    return -1;
end

SpaceshipReceptionDisplayCtrl._GetNextIndex = HL.Method().Return(HL.Number) << function(self)
    for index = 1, self.m_maxSelectNum do
        if not Utils.isInclude(self.cell2Select, index) then
            return index
        end
    end
    return -1
end

SpaceshipReceptionDisplayCtrl.GetCellByIndex = HL.Method(HL.Number).Return(HL.Forward("SSPictureCell")) << function(self, cellIndex)
    local go = self.view.pictureScroll:Get(CSIndex(cellIndex))
    local cell = nil
    if go then
        cell = self.GetCell(go)
    end
    return cell
end

SpaceshipReceptionDisplayCtrl._GetCellSelectIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, cellIndex)
    if self.cell2Select[cellIndex] ~= nil and self.cell2Select[cellIndex] > 0 then
        return self.cell2Select[cellIndex]
    else
        return -1
    end
end

SpaceshipReceptionDisplayCtrl.SetSaveState = HL.Method(HL.Boolean) << function(self, isSave)
    if self.m_isSave == isSave then
        return
    end
    if isSave then
        self.view.btnSaveRoot:SetState("DisableState")
    else
        self.view.btnSaveRoot:SetState("NormalState")
    end
    self.m_isSave = isSave
end

SpaceshipReceptionDisplayCtrl._InitSortNode = HL.Method() << function(self)
    self.view.sortNode:InitSortNode(UIConst.SS_PICTURE_SORT_OPTION, function(optData, isIncremental)
        local filteredList = self.m_filteredInfoList
        filteredList = self:_ApplySort(filteredList, optData, isIncremental)
        self:_RefreshPicList(true, filteredList)
    end, nil, false, true, self.view.filterBtn)
end

SpaceshipReceptionDisplayCtrl._InitFilterNode = HL.Method() << function(self)
    local filterArgs = {
        tagGroups = FilterUtils.generateConfig_POSTER_PICTURE(),
        selectedTags = self.m_selectedTags,
        onConfirm = function(tags)
            self:_OnFilterConfirm(tags)
        end,
        getResultCount = function(tags)
            return self:_OnFilterGetCount(tags)
        end,
        sortNodeWidget = self.view.sortNode,
    }
    self.view.filterBtn:InitFilterBtn(filterArgs)
end


SpaceshipReceptionDisplayCtrl._SortData = HL.Method(HL.Table, HL.Table, HL.Boolean) << function(self, itemList, keys, isIncremental)
    if itemList then
        table.sort(itemList, Utils.genSortFunction(keys, isIncremental))
        self.m_sortIsIncremental = isIncremental
        self.m_sortOptData = keys
    end
end

SpaceshipReceptionDisplayCtrl._OnFilterConfirm = HL.Method(HL.Table) << function(self, tags)
    self.m_selectedTags = tags or {}
    local filteredList = self.m_infoList
    filteredList = self:_ApplySort(filteredList, self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)
    filteredList = self:_ApplyFilter(filteredList, self.m_selectedTags)
    if #filteredList > 0 then
        self.view.commonState:SetState("Normal")
    elseif #self.m_infoList == 0 then
        self.view.commonState:SetState("Null")
    else
        self.view.commonState:SetState("ScreenNull")
    end
    if self.m_viewDetailBindingId > 0 then
        InputManagerInst:ToggleBinding(self.m_viewDetailBindingId, #filteredList > 0)
    end
    self.m_filteredInfoList = filteredList
    self:_RefreshPicList(true, filteredList)
end

SpaceshipReceptionDisplayCtrl.UpdatePictureItems = HL.Method(HL.Table) << function(self, items)
    self.m_infoList = lume.deepCopy(items)
    self.m_filteredInfoList = lume.deepCopy(items)
    self:_OnFilterConfirm(self.m_selectedTags)
    self.view.sortNode:SortCurData()
end

SpaceshipReceptionDisplayCtrl._OnFilterGetCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local resultCount = 0
    for _, itemInfo in pairs(self.m_infoList) do
        if FilterUtils.checkIfPassFilter(itemInfo, tags) then
            resultCount = resultCount + 1
        end
    end
    return resultCount
end
SpaceshipReceptionDisplayCtrl._ApplySort = HL.Method(HL.Table, HL.Table, HL.Boolean).Return(HL.Table)
    << function(self, itemInfoList, optData, isIncremental)
    local tempPictureIdList = {}
    for selectIndex, cellIndex in pairs(self.m_select2Cell) do
        local item = itemInfoList[cellIndex]
        if item then
            local picId = item.posterData.pictureId
            tempPictureIdList[picId] = selectIndex
        end
    end
    for _, itemInfo in pairs(itemInfoList) do
        local picId = itemInfo.posterData.pictureId
        if tempPictureIdList[picId] then
            itemInfo.selectSlot = -tempPictureIdList[picId]
            itemInfo.selectSlotReverse = tempPictureIdList[picId]
        else
            itemInfo.selectSlot = math.mininteger
            itemInfo.selectSlotReverse = math.maxinteger
        end
    end

    local keys = isIncremental and optData.reverseKeys or optData.keys
    self:_SortData(itemInfoList, keys, isIncremental)
    self.m_select2Cell = {}
    self.cell2Select = {}
    for cellIndex, info in pairs(itemInfoList) do
        local cellPicId = info.posterData.pictureId
        local selectIndex = tempPictureIdList[cellPicId]
        if selectIndex then
            self.m_select2Cell[selectIndex] = cellIndex
            self.cell2Select[cellIndex] = selectIndex
        end
    end
    return itemInfoList
end

SpaceshipReceptionDisplayCtrl._ApplyFilter = HL.Method(HL.Table, HL.Table).Return(HL.Table)
    << function(self, itemInfoList, selectedTags)
    local filteredList = {}
    local tempPictureIdList = {}
    for selectIndex, cellIndex in pairs(self.m_select2Cell) do
        local item = itemInfoList[cellIndex]
        local picId = item.posterData.pictureId
        tempPictureIdList[picId] = selectIndex
    end

    for _, itemInfo in pairs(itemInfoList) do
        if FilterUtils.checkIfPassFilter(itemInfo, selectedTags) or
            tempPictureIdList[itemInfo.posterData.pictureId] ~= nil
        then
            table.insert(filteredList, itemInfo)
        end
    end
    self:_SortData(itemInfoList, self.m_sortOptData, self.m_sortIsIncremental)

    self.m_select2Cell = {}
    self.cell2Select = {}
    for cellIndex, info in pairs(filteredList) do
        local cellPicId = info.posterData.pictureId
        local selectIndex = tempPictureIdList[cellPicId]
        if selectIndex then
            self.m_select2Cell[selectIndex] = cellIndex
            self.cell2Select[cellIndex] = selectIndex
        end
    end

    return filteredList
end




SpaceshipReceptionDisplayCtrl._RefreshPicList = HL.Method(HL.Opt(HL.Boolean, HL.Table)) << function(self, setTop, targetItems)
    local count = #targetItems
    self.view.pictureScroll:UpdateCount(count, setTop or false)
    self:_UpdateRedDotByInfoList()
end

SpaceshipReceptionDisplayCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    local sortNode = self.view.sortNode
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
    self.m_selectedTags = filterTags
    if self.view.filterBtn.m_args then
        self.view.filterBtn.m_args.selectedTags = lume.deepCopy(filterTags)
    end

    local selectedPictureIds = {}
    if recoverState.selectedPictureIds then
        for _, pictureId in ipairs(recoverState.selectedPictureIds) do
            if self:_GetIndexByPicId(pictureId) > 0 then
                table.insert(selectedPictureIds, pictureId)
            end
        end
    else
        local picIdsList = GameInstance.player.spaceship:GetScreenWallPicIds()
        if picIdsList and picIdsList.Count > 0 then
            for i = CSIndex(1), CSIndex(picIdsList.Count) do
                table.insert(selectedPictureIds, picIdsList[i])
            end
        end
    end
    self:ShowSelectItems(selectedPictureIds)
    if recoverState.isSave == nil then
        self:SetSaveState(true)
    else
        self:SetSaveState(recoverState.isSave == true)
    end
    sortNode:UpdateDeviceState()
    self:_TryRecoverPictureScrollState(recoverState.currentPictureId)
end

SpaceshipReceptionDisplayCtrl._GetSelectedPictureIdList = HL.Method().Return(HL.Table) << function(self)
    local selectedPictureIds = {}
    for selectIndex = 1, self.m_maxSelectNum do
        local cellIndex = self.m_select2Cell[selectIndex]
        local item = cellIndex and self.m_filteredInfoList[cellIndex] or nil
        if item and item.posterData then
            table.insert(selectedPictureIds, item.posterData.pictureId)
        end
    end
    return selectedPictureIds
end

SpaceshipReceptionDisplayCtrl._TryRecoverPictureScrollState = HL.Method(HL.Opt(HL.Any)) << function(self, pictureId)
    if pictureId == nil then
        return
    end
    local targetIndex = self:_GetIndexByPicId(pictureId)
    if targetIndex <= 0 then
        return
    end
    self.view.pictureScroll:SetSelectedIndex(CSIndex(targetIndex), false, true, true)
end

SpaceshipReceptionDisplayCtrl._TryRecoverPicturePopup = HL.Method(HL.Opt(HL.Table)) << function(self, popupState)
    if popupState == nil then
        return
    end
    local isOpen, popupCtrl = UIManager:IsOpen(PanelId.ReceptionDisplayPicture)
    if isOpen and popupCtrl and popupCtrl:IsShow() then
        return
    end
    local pictureId = popupState.pictureId
    if pictureId == nil and popupState.currentIndex and popupState.currentIndex > 0 then
        local info = self.m_filteredInfoList[popupState.currentIndex]
        pictureId = info and info.posterData and info.posterData.pictureId or nil
    end
    if pictureId == nil then
        return
    end
    UIManager:AutoOpen(PanelId.ReceptionDisplayPicture, {
        pictureId = pictureId,
        pictureList = self.m_filteredInfoList,
    })
end

SpaceshipReceptionDisplayCtrl._OnSavePicture = HL.Method() << function(self)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_SS_POSTER_SAVE_POPUP)
end

SpaceshipReceptionDisplayCtrl._UpdateRedDotByInfoList = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    self.m_pictureRedDots = {}
    for index, info in ipairs(self.m_filteredInfoList) do
        local cellPicId = info.posterData.pictureId
        local isOld = GameInstance.player.spaceship:GetPictureRedDotReadState(cellPicId)
        if not isOld then
            self.m_pictureRedDots[index] = cellPicId
        end
    end
    self:_UpdateRedDot()
end

SpaceshipReceptionDisplayCtrl._UpdateRedDot = HL.Method() << function(self)
    if not self.m_pictureRedDots then
        self:_UpdateRedDotByInfoList()
    end
    local start, final = self:_GetShowingCellStartEnd()
    local needRedDot = false
    for i, picId in pairs(self.m_pictureRedDots) do
        if i > LuaIndex(final)  then
            needRedDot = true
            break
        end
    end
    self.view.redDot.gameObject:SetActive(needRedDot)
end

SpaceshipReceptionDisplayCtrl._GetShowingCellStartEnd = HL.Method().Return(HL.Number,HL.Number) << function(self)
    local totalCount = lume.count(self.m_filteredInfoList)
    local scrollRect = self.view.pictureScroll.transform.rect
    local cellRect = self.view.ssPictureCell.gameObject.transform.rect
    local scrollSpace = self.view.pictureScroll.space
    local rowCount = math.floor(scrollRect.width / (cellRect.width + scrollSpace.x))
    local calCount = math.ceil(totalCount / rowCount)
    local showCount = math.floor(math.min(totalCount, (scrollRect.height) /  (cellRect.height + scrollSpace.y))) * rowCount
    local scrollNormal = math.min(math.max((self.view.container.transform.anchoredPosition.y / self.view.container.rect.height), 0), 1)
    local start = math.ceil(calCount * scrollNormal * rowCount)
    local final = start + showCount
    return start, final
end

HL.Commit(SpaceshipReceptionDisplayCtrl)
