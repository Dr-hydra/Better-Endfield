local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiGroupItemList = HL.Class('WikiGroupItemList', UIWidgetBase)

local CELL_COUNT_PER_ROW = 3
local ITEM_ROW_SIZE = 130
local TITLE_ROW_SIZE = 68

WikiGroupItemList.m_getGroupItemsCell = HL.Field(HL.Function)

WikiGroupItemList.m_wikiGroupShowDataList = HL.Field(HL.Table)

WikiGroupItemList.m_onItemClicked = HL.Field(HL.Function)

WikiGroupItemList.m_onGetSelectedEntryShowData = HL.Field(HL.Function)

WikiGroupItemList.m_lastSelectedEntryId = HL.Field(HL.Any)

WikiGroupItemList.m_btnExpandList = HL.Field(HL.Userdata)

WikiGroupItemList.m_btnClose = HL.Field(HL.Userdata)

WikiGroupItemList.m_isRefreshed = HL.Field(HL.Boolean) << false

WikiGroupItemList.m_wikiItemInfo = HL.Field(HL.Userdata)

WikiGroupItemList.m_isPreviewMode = HL.Field(HL.Boolean) << false

WikiGroupItemList.m_isClosing = HL.Field(HL.Boolean) << false


WikiGroupItemList._OnFirstTimeInit = HL.Override() << function(self)
    self.m_getGroupItemsCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.getCellSize = function(csIndex)
        local luaIndex = LuaIndex(csIndex)
        local cellCount = 0
        for _, wikiGroupShowData in pairs(self.m_wikiGroupShowDataList) do
            local nextCellCount = cellCount + wikiGroupShowData.uiCellCount
            if luaIndex == cellCount + 1 then
                return TITLE_ROW_SIZE
            elseif luaIndex > cellCount and luaIndex <= nextCellCount then
                return ITEM_ROW_SIZE
            end
            cellCount = nextCellCount
        end
    end
    self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        local luaIndex = LuaIndex(csIndex)
        local targetWikiGroupShowData
        local startIndex, endIndex, cellCount = 0, 0, 0
        for _, wikiGroupShowData in pairs(self.m_wikiGroupShowDataList) do
            local nextCellCount = cellCount + wikiGroupShowData.uiCellCount
            if luaIndex == cellCount + 1 then

                targetWikiGroupShowData = wikiGroupShowData
                break
            elseif luaIndex > cellCount and luaIndex <= nextCellCount then
                targetWikiGroupShowData = wikiGroupShowData
                local itemCellCountInGroup = luaIndex - cellCount - 1
                startIndex = (itemCellCountInGroup - 1) * CELL_COUNT_PER_ROW + 1
                endIndex = math.min(startIndex + CELL_COUNT_PER_ROW - 1, #wikiGroupShowData.wikiEntryShowDataList)
                break
            end
            cellCount = nextCellCount
        end

        local cell = self.m_getGroupItemsCell(object)
        cell.gameObject.name = csIndex
        local selectedItemWidget = cell:InitWikiGroupItems(targetWikiGroupShowData, startIndex, endIndex,
            self.m_onGetSelectedEntryShowData, function(itemWidget, wikiEntryShowData)
                self:_OnItemClicked(itemWidget, wikiEntryShowData)
            end, self.m_readWikiEntries, self.m_isPreviewMode)
        if selectedItemWidget then
            self:_SetItemSelected(selectedItemWidget)
            self:SetNaviTarget(selectedItemWidget.view.button)
        end
    end)

    if self.m_btnExpandList then
        self.m_btnExpandList.onClick:AddListener(function()
            self.view.gameObject:SetActive(true)
            self.m_btnExpandList.gameObject:SetActive(false)
            self:_Refresh()
            if self.m_wikiItemInfo then
                self.m_wikiItemInfo.view.animationWrapper:PlayInAnimation()
            end
        end)

    end

    if self.view.triangleBtn then
        self.view.triangleBtn.onClick:AddListener(function()
            self:_OnCloseBtnClicked()
        end)
    end

    if self.view.autoCloseArea then
        self.view.autoCloseArea.onTriggerAutoClose:AddListener(function()
            self:_OnCloseBtnClicked()
        end)
    end
end









WikiGroupItemList.InitWikiGroupItemList = HL.Method(HL.Table) << function(self, args)
    self.m_btnExpandList = args.btnExpandList
    self.m_onItemClicked = args.onItemClicked
    self.m_onGetSelectedEntryShowData = args.onGetSelectedEntryShowData
    self.m_wikiItemInfo = args.wikiItemInfo
    self.m_isPreviewMode = args.isPreviewMode == true
    self:_ProcessWikiGroupShowDataList(args.wikiGroupShowDataList)

    self.m_readWikiEntries = {}
    self:_FirstTimeInit()

    self.view.gameObject:SetActive(not args.isInitHidden)
    if not args.isInitHidden then
        self:_Refresh()
    end
    self.m_isClosing = false
end

WikiGroupItemList.GetResumeState = HL.Method().Return(HL.Table) << function(self)
    local selectedEntryShowData = self.m_onGetSelectedEntryShowData and self.m_onGetSelectedEntryShowData() or nil
    return {
        isExpanded = self.view.gameObject.activeSelf,
        selectedEntryId = selectedEntryShowData and selectedEntryShowData.wikiEntryData.id or self.m_lastSelectedEntryId,
    }
end

WikiGroupItemList.ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState or resumeState.isExpanded ~= true or not self.m_btnExpandList then
        return
    end
    self.view.gameObject:SetActive(true)
    self.m_btnExpandList.gameObject:SetActive(false)
    self:_Refresh()
    if self.m_wikiItemInfo then
        self.m_wikiItemInfo.view.animationWrapper:PlayInAnimation()
    end
end

WikiGroupItemList._OnDisable = HL.Override() << function(self)
    self:_MarkWikiEntryRead()
end

WikiGroupItemList._OnDestroy = HL.Override() << function(self)
    self:_MarkWikiEntryRead()
end

WikiGroupItemList._ProcessWikiGroupShowDataList = HL.Method(HL.Table) << function(self, wikiGroupShowDataList)
    self.m_wikiGroupShowDataList = wikiGroupShowDataList
    for _, wikiGroupShowData in pairs(self.m_wikiGroupShowDataList) do
        wikiGroupShowData.uiCellCount = math.ceil(#wikiGroupShowData.wikiEntryShowDataList / CELL_COUNT_PER_ROW) + 1
    end
end

WikiGroupItemList._Refresh = HL.Method() << function(self)
    if self.m_isRefreshed then
        local selectedEntryShowData = self.m_onGetSelectedEntryShowData and self.m_onGetSelectedEntryShowData()
        if selectedEntryShowData and selectedEntryShowData.wikiEntryData.id ~= self.m_lastSelectedEntryId then
            local scrollToIndex = self:_GetScrollToIndex()
            self.view.scrollList:ScrollToIndex(scrollToIndex, true)
            self.m_lastSelectedEntryId = selectedEntryShowData.wikiEntryData.id

            local groupItemsCell = self.m_getGroupItemsCell(self.view.scrollList:Get(scrollToIndex))
            local itemCell = groupItemsCell:GetCellByEntryId(selectedEntryShowData.wikiEntryData.id)
            if itemCell then
                self:_SetItemSelected(itemCell)
                if DeviceInfo.usingController then
                    self:SetNaviTarget(itemCell.view.button)
                end
            end
        else



        end
        return
    end
    self:_MarkWikiEntryRead()
    local allCellCount = 0
    for _, wikiGroupShowData in pairs(self.m_wikiGroupShowDataList) do
        allCellCount = allCellCount + wikiGroupShowData.uiCellCount
    end
    self.view.scrollList:UpdateCount(allCellCount, self:_GetScrollToIndex())
    local selectedEntryShowData = self.m_onGetSelectedEntryShowData and self.m_onGetSelectedEntryShowData()
    if selectedEntryShowData then
        self.m_lastSelectedEntryId = selectedEntryShowData.wikiEntryData.id
    end
    self.m_isRefreshed = true
end

WikiGroupItemList._GetScrollToIndex = HL.Method().Return(HL.Number) << function(self)
    local selectedEntryShowData = self.m_onGetSelectedEntryShowData and self.m_onGetSelectedEntryShowData()
    if selectedEntryShowData then
        local cellCount = 0
        for _, groupData in ipairs(self.m_wikiGroupShowDataList) do
            for i, entryData in ipairs(groupData.wikiEntryShowDataList) do
                if entryData.wikiEntryData.id == selectedEntryShowData.wikiEntryData.id then
                    return cellCount + math.ceil(i / CELL_COUNT_PER_ROW)
                end
            end
            cellCount = cellCount + groupData.uiCellCount
        end
    end
    return 0
end

WikiGroupItemList._ScrollToSelected = HL.Method() << function(self)
    self.view.scrollList:ScrollToIndex(self:_GetScrollToIndex(), true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
end

WikiGroupItemList._OnItemClicked = HL.Method(HL.Userdata, HL.Table) << function(self, itemWidget, wikiEntryShowData)
    self.m_lastSelectedEntryId = wikiEntryShowData.wikiEntryData.id
    if self.m_onItemClicked then
        self.m_onItemClicked(wikiEntryShowData)
    end
    self:_SetItemSelected(itemWidget)
    GameInstance.player.wikiSystem:MarkWikiEntryRead({ wikiEntryShowData.wikiEntryData.id })
end

WikiGroupItemList.m_selectedItem = HL.Field(HL.Userdata)

WikiGroupItemList._SetItemSelected = HL.Method(HL.Userdata) << function(self, itemWidget)
    if self.m_selectedItem then
        self.m_selectedItem:SetSelected(false)
    end
    if itemWidget then
        itemWidget:SetSelected(true)
        self.m_selectedItem = itemWidget
    end
end

WikiGroupItemList._OnCloseBtnClicked = HL.Method(HL.Opt(HL.Boolean)) << function(self, isFast)
    local function close()
        self.m_isClosing = false
        self.view.gameObject:SetActive(false)
        self.m_btnExpandList.gameObject:SetActive(true)
        if self.m_wikiItemInfo then
            self.m_wikiItemInfo.view.animationWrapper:PlayInAnimation()
        end
    end

    if isFast then
        close()
        return
    end

    if self.m_isClosing then
        return
    end
    self.m_isClosing = true

    self.view.animWrapper:PlayOutAnimation(close)
    if self.m_wikiItemInfo then
        self.m_wikiItemInfo.view.animationWrapper:PlayOutAnimation()
    end
end





WikiGroupItemList.m_readWikiEntries = HL.Field(HL.Table)

WikiGroupItemList._MarkWikiEntryRead = HL.Method() << function(self)
    if self.m_readWikiEntries and not self.m_isPreviewMode then
        local entryIdList = {}
        for entryId, _ in pairs(self.m_readWikiEntries) do
            table.insert(entryIdList, entryId)
        end
        GameInstance.player.wikiSystem:MarkWikiEntryRead(entryIdList)
        self.m_readWikiEntries = {}
    end
end



HL.Commit(WikiGroupItemList)
return WikiGroupItemList

