local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharPosterScrollList = HL.Class('CharPosterScrollList', UIWidgetBase)

local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget








CharPosterScrollList.info = HL.Field(HL.Table)


CharPosterScrollList.m_selectNum = HL.Field(HL.Number) << -1


CharPosterScrollList.m_mode = HL.Field(HL.Number) << -1


CharPosterScrollList.m_charNum = HL.Field(HL.Number) << 0

CharPosterScrollList.m_originSingleSelect = HL.Field(HL.Number) << 0

CharPosterScrollList.curSingleSelect = HL.Field(HL.Number) << 0


CharPosterScrollList.cell2Select = HL.Field(HL.Table)

CharPosterScrollList.m_select2Cell = HL.Field(HL.Table)

CharPosterScrollList.m_charItems = HL.Field(HL.Table)

CharPosterScrollList.m_onCharListChanged = HL.Field(HL.Function)

CharPosterScrollList.GetCell = HL.Field(HL.Function)

CharPosterScrollList.m_clickFunc = HL.Field(HL.Function)

CharPosterScrollList.m_updateFunc = HL.Field(HL.Function)

CharPosterScrollList.m_naviTargetInitialized = HL.Field(HL.Boolean) << false

CharPosterScrollList.m_selectedTags = HL.Field(HL.Table)

CharPosterScrollList.m_sortOptData = HL.Field(HL.Table)

CharPosterScrollList.m_sortIsIncremental = HL.Field(HL.Boolean) << false

CharPosterScrollList.m_filteredInfoList = HL.Field(HL.Table)


CharPosterScrollList._OnFirstTimeInit = HL.Override() << function(self)
    self.GetCell = UIUtils.genCachedCellFunction(self.view.charScrollList)
    self.view.charScrollList.onSelectedCell:AddListener(function(obj, csIndex)
        local cellIndex = LuaIndex(csIndex)
        if self.m_mode == UIConst.CharListMode.Single then
            self:OnClickItem(cellIndex)
        end
    end)
    self.view.charScrollList.getCurSelectedIndex = function()
        return CSIndex(self.curSingleSelect)
    end
    self.view.charScrollList.onUpdateCell:AddListener(function(object, index)
        self:OnUpdateCell(object, LuaIndex(index))
        if self.m_updateFunc then
            self.m_updateFunc(object, LuaIndex(index))
        end
    end)
    self:_InitSortNode()
    self:_InitFilterNode()
end

CharPosterScrollList.InitCharPosterScrollList = HL.Method(HL.Table, HL.Opt(HL.Function)) << function(self, info, onCharListChanged)
    self:_InitData(info)
    self:_FirstTimeInit()
    self.m_onCharListChanged = onCharListChanged
end

CharPosterScrollList._InitData = HL.Method(HL.Table) << function(self, info)
    self.info = info or {}
    self.m_selectNum = info.selectNum or 1
    if self.m_selectNum > 1 then

        self.m_mode = UIConst.CharListMode.MultiSelect
    else
        self.m_mode = UIConst.CharListMode.Single
    end

    self.m_charNum = 0
    self.cell2Select = {}
    self.m_select2Cell = {}
    self.m_charItems = {}
    self.m_filteredInfoList = {}
    self.m_originSingleSelect = 0
    self.curSingleSelect = 0
    self.m_mode = info.mode or UIConst.CharListMode.MultiSelect
    self.m_naviTargetInitialized = false
end

CharPosterScrollList._InitSortNode = HL.Method() << function(self)
    self.view.sortNode:InitSortNode(UIConst.CHAR_POSTER_LIST_SORT_OPTION, function(optData, isIncremental)
        local filteredList = self.m_filteredInfoList
        self.m_naviTargetInitialized = false
        filteredList = self:_ApplySort(filteredList, optData, isIncremental)
        self:_RefreshCharList(true, filteredList)
    end, nil, false, true, self.view.filterBtn)
end

CharPosterScrollList._ApplySort = HL.Method(HL.Table, HL.Table, HL.Boolean).Return(HL.Table)
    << function(self, itemInfoList, optData, isIncremental)
    local tmpTable = {}
    local tmpSingleInstId = 0

    if self.m_mode == UIConst.CharListMode.Single then
        local cell = self:GetCellByIndex(self.curSingleSelect)
        if cell then
            tmpSingleInstId = cell.charInfo.instId
        end
    else
        for selectIndex, cellIndex in pairs(self.m_select2Cell) do
            local charItem = self.m_filteredInfoList[cellIndex]
            local instId = charItem.instId
            tmpTable[instId] = selectIndex
        end
    end
    for _, itemInfo in pairs(itemInfoList) do
        if itemInfo.instId == tmpSingleInstId then
            itemInfo.selectSlot = 1
            itemInfo.selectSlotReverse = 1
        elseif tmpTable[itemInfo.instId] then
            itemInfo.selectSlot = tmpTable[itemInfo.instId]
            itemInfo.selectSlotReverse = -tmpTable[itemInfo.instId]
        else
            itemInfo.selectSlot = math.maxinteger
            itemInfo.selectSlotReverse = math.mininteger
        end
    end
    local keys = isIncremental and optData.keys or optData.reverseKeys
    self:_SortData(keys, isIncremental)
    self.m_select2Cell = {}
    self.cell2Select = {}
    for cellIndex, info in pairs(itemInfoList) do
        local cellInstId = info.instId
        local selectIndex = tmpTable[cellInstId]
        if selectIndex then
            self.m_select2Cell[selectIndex] = cellIndex
            self.cell2Select[cellIndex] = selectIndex
        end

        if tmpSingleInstId > 0 and tmpSingleInstId == cellInstId then
            self.curSingleSelect = cellIndex
        end
    end

    return itemInfoList
end
CharPosterScrollList._ApplyFilter = HL.Method(HL.Table, HL.Table).Return(HL.Table)
    << function(self, itemInfoList, selectedTags)
    local filteredList = {}

    local tmpTable = {}
    local tmpSingleInstId = 0

    if self.m_mode == UIConst.CharListMode.Single then
        local cell = self:GetCellByIndex(self.curSingleSelect)
        if cell then
            tmpSingleInstId = cell.charInfo.instId
        end
    else
        for selectIndex, cellIndex in pairs(self.m_select2Cell) do
            local charItem = itemInfoList[cellIndex]
            local instId = charItem.instId
            tmpTable[instId] = selectIndex
        end
    end

    for _, itemInfo in pairs(itemInfoList) do
        if FilterUtils.checkIfPassFilter(itemInfo, selectedTags) or
            itemInfo.instId == tmpSingleInstId or tmpTable[itemInfo.instId] ~= nil
        then
            table.insert(filteredList, itemInfo)
        end
    end
    self:_SortData(self.m_sortOptData, self.m_sortIsIncremental)
    self.m_select2Cell = {}
    self.cell2Select = {}
    for cellIndex, info in pairs(filteredList) do
        local cellInstId = info.instId
        local selectIndex = tmpTable[cellInstId]
        if selectIndex then
            self.m_select2Cell[selectIndex] = cellIndex
            self.cell2Select[cellIndex] = selectIndex
        end

        if tmpSingleInstId > 0 and tmpSingleInstId == cellInstId then
            self.curSingleSelect = cellIndex
        end
    end

    return filteredList
end


CharPosterScrollList._InitFilterNode = HL.Method() << function(self)
    local filterArgs = {
        tagGroups = FilterUtils.generateConfig_DEPOT_CHAR(),
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

CharPosterScrollList._OnFilterConfirm = HL.Method(HL.Table) << function(self, tags)
    self.m_selectedTags = tags or {}
    self.m_filteredInfoList = self.m_charItems
    local filteredList = self.m_charItems
    self.m_naviTargetInitialized = false
    filteredList = self:_ApplySort(filteredList, self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)
    filteredList = self:_ApplyFilter(filteredList, self.m_selectedTags)
    self.m_filteredInfoList = filteredList
    self:_RefreshCharList(true, filteredList)
end


CharPosterScrollList._OnFilterGetCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local resultCount = 0
    for _, itemInfo in pairs(self.m_charItems) do
        if FilterUtils.checkIfPassFilter(itemInfo, tags) then
            resultCount = resultCount + 1
        end
    end
    return resultCount
end


CharPosterScrollList._GetCharIndex = HL.Method(HL.Any).Return(HL.Number) << function(self, charInstId)
    for index = 1, #self.m_filteredInfoList do
        local charItem = self.m_filteredInfoList[index]
        if charItem.instId == charInstId then
            return index
        end
    end
    return -1;
end

CharPosterScrollList._SortData = HL.Method(HL.Table, HL.Boolean) << function(self, keys, isIncremental)
    if self.m_filteredInfoList then
        table.sort(self.m_filteredInfoList, Utils.genSortFunction(keys, isIncremental))
        self.m_sortIsIncremental = isIncremental
        self.m_sortOptData = keys
    end
end

CharPosterScrollList._RefreshCharList = HL.Method(HL.Opt(HL.Boolean, HL.Table)) << function(self, setTop, targetItems)
    targetItems = targetItems or self.m_charItems
    local count = #targetItems
    local isEmpty = count <= 0
    self.view.emptyNode.gameObject:SetActive(isEmpty)
    self.view.charScrollList.gameObject:SetActive(not isEmpty)
    self.view.charScrollList:UpdateCount(count, setTop or false)
    if self.m_onCharListChanged then
        self.m_onCharListChanged(targetItems)
    end
end

CharPosterScrollList._ShowMultiChars = HL.Method(HL.Opt(HL.Boolean)) << function(self, playAnim)
    for cellIndex = 1, self.view.charScrollList.count do
        local cell = self:GetCellByIndex(cellIndex)
        if cell then
            cell:SetMultiSelect(self.cell2Select[cellIndex], playAnim)
        end
    end
end

CharPosterScrollList._ShowSingleChars = HL.Method(HL.Opt(HL.Boolean)) << function(self, playAnim)
    for cellIndex = 1, self.view.charScrollList.count do
        local cell = self:GetCellByIndex(cellIndex)
        if cell then
            cell:SetSingleModeSelected(true, playAnim)
        end
    end
end

CharPosterScrollList._UpdateMultiSelect = HL.Method(HL.Opt(HL.Boolean)).Return(HL.Table, HL.Table) << function(self, playAnim)
    local result = {}
    local charItemList = {}
    local charInfoList = {}
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
            cell:SetMultiSelect(index, playAnim)
        end
        local charItem = self.m_filteredInfoList[cellIndex]
        self:_UpdateSlotIndex(charItem, index)
        table.insert(charItemList, charItem)

        local charInfo = {
            charId = charItem.templateId,
            charInstId = charItem.instId,
            isLocked = charItem.isLocked,
            isTrail = charItem.isTrail,
            isReplaceable = charItem.isReplaceable
        }
        table.insert(charInfoList, charInfo)
    end
    return charItemList, charInfoList
end

CharPosterScrollList._GetNextIndex = HL.Method().Return(HL.Number) << function(self)
    for index = 1, self.m_selectNum do
        if not Utils.isInclude(self.cell2Select, index) then
            return index
        end
    end
    return -1
end

CharPosterScrollList._RefreshMode = HL.Method() << function(self)
    local singleSelected = self.m_mode == UIConst.CharListMode.Single
    for cellIndex, _ in pairs(self.cell2Select) do
        local cell = self:GetCellByIndex(cellIndex)
        if cell then
            cell:SetSingleModeSelected(singleSelected)
        end
    end

    if self.m_mode == UIConst.CharListMode.Single then
        local cell = self:GetCellByIndex(self.curSingleSelect)
        if cell then
            cell:SetSingleSelect(true)
            self:SetNaviTarget(cell.view.button)
        end

        if DeviceInfo.usingController and self.curSingleSelect <= 0 then
            self:_StartCoroutine(function()
                coroutine.step()
                local cell = self:GetCellByIndex(1)
                if cell then
                    self:SetNaviTarget(cell.view.button)
                end
            end)
        end
    end
end




CharPosterScrollList.SetUpdateCellFunc = HL.Method(HL.Opt(HL.Function, HL.Function)) << function(self, updateFunc, clickFunc)
    self.m_updateFunc = updateFunc
    self.m_clickFunc = clickFunc
end

CharPosterScrollList.OnUpdateCell = HL.Method(HL.Userdata, HL.Number, HL.Opt(HL.Function)) << function(self, object, index)
    local cell = self:GetCellByIndex(index)
    local item = self.m_filteredInfoList[index]

    cell:InitCharFormationHeadCell(item, function(arg)
        self:OnClickItem(index)
    end, true)

    cell:RefreshExInfo(self.info)

    local naviTargetCell

    if self.m_mode == UIConst.CharListMode.Single then
        cell.view.button:ChangeActionOnSetNaviTarget(ActionOnSetNaviTarget.AutoTriggerOnClick)
        cell:SetSingleModeSelected(true, false)
        cell:SetSingleSelect(self.curSingleSelect == index)
        if self.curSingleSelect == index then
            naviTargetCell = cell
        end

        local selectedCharInfo = self.info.selectedCharInfo
        local isUnavailable = false
        if selectedCharInfo and selectedCharInfo.isLocked then

            if (not selectedCharInfo.isReplaceable or item.templateId ~= selectedCharInfo.charId) and
                selectedCharInfo.charInstId ~= item.instId then
                isUnavailable = true
            end
        else

            if self.info.lockedTeamData then
                for _, char in pairs(self.info.lockedTeamData.chars) do
                    if char.isLocked and char.charId == item.templateId and char.charInstId ~= item.instId then
                        isUnavailable = true
                        break
                    end
                end
            end
        end
        cell:SetUnavailable(isUnavailable)
    else
        cell.view.button:ChangeActionOnSetNaviTarget(ActionOnSetNaviTarget.PressConfirmTriggerOnClick)
        cell:SetMultiSelect(self.cell2Select[index], false)
        if index == 1 then
            naviTargetCell = cell
        end

        local isUnavailable = false

        if self.info.lockedTeamData then
            for _, char in pairs(self.info.lockedTeamData.chars) do
                if char.isLocked and not char.isReplaceable and
                    char.charId == item.templateId and char.charInstId ~= item.instId then
                    isUnavailable = true
                    break
                end
            end
        end

        cell:SetUnavailable(isUnavailable)
    end

    if not self.m_naviTargetInitialized and naviTargetCell then
        self.m_naviTargetInitialized = true
        self:_StartCoroutine(function()
            coroutine.step()
            self:SetNaviTarget(naviTargetCell.view.button)
        end)
    end
end

CharPosterScrollList.GetCellByIndex = HL.Method(HL.Number).Return(HL.Forward("CharFormationHeadCell")) << function(self, cellIndex)
    local go = self.view.charScrollList:Get(CSIndex(cellIndex))
    local cell = nil
    if go then
        cell = self.GetCell(go)
    end

    return cell
end

CharPosterScrollList.ShowSelectChars = HL.Method(HL.Table, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, items, playAnim, refreshScroll)

    self.cell2Select = {}
    self.m_select2Cell = {}
    self.m_charNum = #items
    for index, charItem in pairs(items) do
        local cellIndex = self:_GetCharIndex(charItem.instId)
        self.cell2Select[cellIndex] = index
        self.m_select2Cell[index] = cellIndex
    end
    if refreshScroll then
        self:_OnFilterConfirm(self.m_selectedTags)
        self.view.sortNode:SortCurData()
    end

    if self.m_mode == UIConst.UIConst.CharListMode.MultiSelect then
        self:_ShowMultiChars(playAnim)
    else
        self:_ShowSingleChars(playAnim)
    end
end

CharPosterScrollList.SetMode = HL.Method(HL.Any, HL.Any) << function(self, mode, charInstId)
    self.m_naviTargetInitialized = false
    self.m_mode = mode
    self.curSingleSelect = self:_GetCharIndex(charInstId)


    self.m_originSingleSelect = self.curSingleSelect
    self:_RefreshMode()
end

CharPosterScrollList.GetEmpty = HL.Method().Return(HL.Boolean) << function(self)
    local empty
    if self.m_mode == UIConst.UIConst.CharListMode.MultiSelect then
        empty = self.m_charNum <= 0
    else
        empty = self.curSingleSelect <= 0
    end
    return empty
end

CharPosterScrollList.UpdateCharItems = HL.Method(HL.Table) << function(self, items)
    self.m_charItems = lume.deepCopy(items)
    self.m_filteredInfoList = lume.deepCopy(items)
    self:_OnFilterConfirm(self.m_selectedTags)
    self.view.sortNode:SortCurData()
end

CharPosterScrollList._GetCellSelectIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, cellIndex)
    if self.cell2Select[cellIndex] ~= nil and self.cell2Select[cellIndex] > 0 then
        return self.cell2Select[cellIndex]
    else
        return -1
    end
end

CharPosterScrollList._RefreshSingleSelect = HL.Method(HL.Number) << function(self, cellIndex)
    if self.curSingleSelect > 0 then
        local oldCell = self:GetCellByIndex(self.curSingleSelect)
        if oldCell then
            oldCell:SetSingleSelect(false)
        end
    end

    local cell = self:GetCellByIndex(cellIndex)
    cell:SetSingleSelect(true)
    self.curSingleSelect = cellIndex
end

CharPosterScrollList.OnClickItem = HL.Method(HL.Number, HL.Opt(HL.Function, HL.Boolean)) << function(self, cellIndex, playAnim)
    local cell = self:GetCellByIndex(cellIndex)
    local cellSelectIndex = self:_GetCellSelectIndex(cellIndex)

    if cell.isUnavailable then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_TEAM_FORMATION_CAN_NOT_REPLACE)
        return
    end

    if self.m_mode == UIConst.CharListMode.Single then

        if self.curSingleSelect == cellIndex then
            return
        end

        self:_RefreshSingleSelect(cellIndex)
        if self.m_clickFunc then
            self.m_clickFunc(true, cellIndex, cell.info)
        end
        self.m_charNum = 1

    else

        if cell.info.isLocked and cell.info.isReplaceable then
            local replaceSelectedIndex = nil
            for selectedIndex, index in pairs(self.m_select2Cell) do
                local selectedCharItem = self.m_filteredInfoList[index]
                if selectedCharItem and selectedCharItem.templateId == cell.info.templateId then
                    replaceSelectedIndex = selectedIndex
                end
            end
            if replaceSelectedIndex then
                Notify(MessageConst.SHOW_POP_UP, {
                    content = Language.LUA_TEAM_FORMATION_REPLACE_CHAR,
                    onConfirm = function()
                        local replaceCell = self:GetCellByIndex(self.m_select2Cell[replaceSelectedIndex])
                        self.m_select2Cell[replaceSelectedIndex] = cellIndex
                        self.cell2Select[cellIndex] = replaceSelectedIndex
                        self.m_filteredInfoList[cellIndex].selectIndex = replaceSelectedIndex

                        if replaceCell then
                            replaceCell:SetMultiSelect(nil, playAnim)
                            self:_UpdateSlotIndex(self.m_filteredInfoList[cellIndex], nil)
                        end
                        local charItemList, charInfoList = self:_UpdateMultiSelect(playAnim)
                        if self.m_clickFunc then
                            self.m_clickFunc(false, cellIndex, cell.info, charItemList, charInfoList)
                        end
                    end,
                })
                return
            end
        end


        if self.m_charNum >= self.m_selectNum and self.cell2Select[cellIndex] == nil then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_FORMATION_MAX_CHAR)
            return
        end

        if cellSelectIndex > 0 then
            local index = self.cell2Select[cellIndex]
            self.m_select2Cell[index] = nil
            self.cell2Select[cellIndex] = nil
            self.m_filteredInfoList[cellIndex].selectIndex = nil
            self.m_charNum = self.m_charNum - 1
            cell:SetMultiSelect(nil, playAnim)
            self:_UpdateSlotIndex(self.m_filteredInfoList[cellIndex], nil)
        elseif self.m_charNum < self.m_selectNum then
            local curIndex = self:_GetNextIndex()
            self.m_select2Cell[curIndex] = cellIndex
            self.cell2Select[cellIndex] = curIndex
            self.m_filteredInfoList[cellIndex].selectIndex = curIndex
            self.m_charNum = curIndex
        end
        local charItemList, charInfoList = self:_UpdateMultiSelect(playAnim)

        if self.m_clickFunc then
            self.m_clickFunc(false, cellIndex, cell.info, charItemList, charInfoList)
        end
    end
end

CharPosterScrollList._UpdateSlotIndex = HL.Method(HL.Table, HL.Any) << function(self, charItem, selectedIndex)
    if selectedIndex then
        charItem.slotIndex = selectedIndex
        charItem.slotReverseIndex = Const.BATTLE_SQUAD_MAX_CHAR_NUM - selectedIndex
    else
        charItem.slotIndex = Const.BATTLE_SQUAD_MAX_CHAR_NUM + 1
        charItem.slotReverseIndex = -1
    end
end




HL.Commit(CharPosterScrollList)
return CharPosterScrollList