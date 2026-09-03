local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiGroupItems = HL.Class('WikiGroupItems', UIWidgetBase)

WikiGroupItems.m_entryIds = HL.Field(HL.Table)

WikiGroupItems.m_rootNode = HL.Field(HL.Userdata)

WikiGroupItems.m_itemCellCache = HL.Field(HL.Forward("UIListCache"))

WikiGroupItems.m_monsterCellCache = HL.Field(HL.Forward("UIListCache"))

WikiGroupItems.m_curCellCache = HL.Field(HL.Forward("UIListCache"))

WikiGroupItems._OnFirstTimeInit = HL.Override() << function(self)

end



WikiGroupItems.InitWikiGroupItems = HL.Method(HL.Table, HL.Number, HL.Number, HL.Function, HL.Function, HL.Table, HL.Boolean).Return(HL.Userdata) << function(
    self, wikiGroupShowData, startIndex, endIndex, onGetSelectedEntryShowData, onItemClicked, readWikiEntries, isPreviewMode)
    self:_FirstTimeInit()
    local isTitle = startIndex == 0 and endIndex == 0
    local isMonster = wikiGroupShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Monster

    self.view.titleNode.gameObject:SetActive(isTitle)
    self.view.itemNode.gameObject:SetActive(not isTitle and not isMonster)
    self.view.monsterNode.gameObject:SetActive(not isTitle and isMonster)
    if isTitle then
        if not string.isEmpty(wikiGroupShowData.customTitle) then
            self.view.titleTxt.text = wikiGroupShowData.customTitle
            self.view.iconImg.gameObject:SetActive(false)
        else
            self.view.titleTxt.text = wikiGroupShowData.wikiGroupData.groupName
            if wikiGroupShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Equip then
                self.view.iconImg.gameObject:SetActive(false)
            else
                self.view.iconImg:LoadSprite(UIConst.UI_SPRITE_WIKI_GROUP, wikiGroupShowData.wikiGroupData.iconId)
            end
        end
        return nil
    end

    local selectedCell = nil
    local selectedEntryShowData = onGetSelectedEntryShowData and onGetSelectedEntryShowData()
    local itemCount = endIndex - startIndex + 1
    local rootNode
    local initFuncName
    local getInitParamFunc
    if isMonster then
        rootNode = self.view.monsterNode
        initFuncName = "InitMonster"
        getInitParamFunc = function(wikiEntryShowData)
            return wikiEntryShowData.wikiEntryData.refMonsterTemplateId
        end
        self.m_monsterCellCache = self.m_monsterCellCache or UIUtils.genCellCache(self.view.monster)
        self.m_curCellCache = self.m_monsterCellCache
    else
        rootNode = self.view.itemNode
        initFuncName = "InitItem"
        getInitParamFunc = function(wikiEntryShowData)
            return { id = wikiEntryShowData.wikiEntryData.refItemId }
        end
        self.m_itemCellCache = self.m_itemCellCache or UIUtils.genCellCache(self.view.item)
        self.m_curCellCache = self.m_itemCellCache
    end
    self.m_rootNode = rootNode
    self.m_entryIds = {}
    self.m_curCellCache:Refresh(itemCount, function(cell, index)
        local wikiEntryShowData = wikiGroupShowData.wikiEntryShowDataList[startIndex + index - 1]
        cell[initFuncName](cell, getInitParamFunc(wikiEntryShowData), function()
            onItemClicked(cell, wikiEntryShowData)
        end)
        if DeviceInfo.usingController then
            cell:SetEnableHoverTips(false)
        end
        if cell.view.lockedNode then
            cell.view.lockedNode.gameObject:SetActive(not wikiEntryShowData.isUnlocked)
        end
        if cell.view.potentialStar then
            cell.view.potentialStar.gameObject:SetActive(false)
        end
        if cell.view.levelNode then
            cell.view.levelNode.gameObject:SetActive(false)
        end
        if selectedEntryShowData and wikiEntryShowData.wikiEntryData.id == selectedEntryShowData.wikiEntryData.id then
            cell:SetSelected(true)
            selectedCell = cell;
        end
        if isPreviewMode then
            cell.view.notObtainedNode.gameObject:SetActive(not wikiEntryShowData.isUnlocked)
        else
            local entryId = wikiEntryShowData.wikiEntryData.id
            if isMonster then
                cell.redDot:InitRedDot("WikiEntry", entryId)
            else
                cell:UpdateRedDot("WikiEntry", entryId)
            end
            if WikiUtils.isWikiEntryUnread(entryId) then
                readWikiEntries[entryId] = true
            end
            table.insert(self.m_entryIds, entryId)
        end
    end)

    return selectedCell
end

WikiGroupItems.GetCellByEntryId = HL.Method(HL.String).Return(HL.Any) << function(self, targetEntryId)
    for i, entryId in ipairs(self.m_entryIds) do
        if entryId == targetEntryId then
            return self.m_curCellCache:Get(i)
        end
    end
end

HL.Commit(WikiGroupItems)
return WikiGroupItems

