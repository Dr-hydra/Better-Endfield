local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiSearchGroupItems = HL.Class('WikiSearchGroupItems', UIWidgetBase)

WikiSearchGroupItems.m_itemCache = HL.Field(HL.Forward("UIListCache"))

WikiSearchGroupItems.m_monsterCache = HL.Field(HL.Forward("UIListCache"))

WikiSearchGroupItems.m_wikiSearchResult = HL.Field(HL.Table)


WikiSearchGroupItems._OnFirstTimeInit = HL.Override() << function(self)
    self.m_itemCache = UIUtils.genCellCache(self.view.itemBigBlack)
    self.m_monsterCache = UIUtils.genCellCache(self.view.monsterBigBlack)
end

WikiSearchGroupItems.InitWikiSearchGroupItems = HL.Method(HL.Table, HL.Function, HL.Table, HL.Opt(HL.Boolean)) << function(
    self, wikiSearchResult, onItemClicked, readWikiEntries, isFirstClicked)
    self:_FirstTimeInit()
    self.m_wikiSearchResult = wikiSearchResult

    self.view.titleTxt.text = Tables.wikiCategoryTable[wikiSearchResult.categoryId].categoryName
    local refreshFunc = function(cell, luaIndex)
        local entryShowData = wikiSearchResult.categoryResult[luaIndex]
        local entryId = entryShowData.wikiEntryData.id
        if wikiSearchResult.categoryId == WikiConst.EWikiCategoryType.Monster then
            cell:InitMonster(entryShowData.wikiEntryData.refMonsterTemplateId, function()
                if onItemClicked then
                    onItemClicked(cell, entryShowData)
                end
            end)
            if DeviceInfo.usingController then
                cell:SetEnableHoverTips(false)
            end
            cell.view.redDot.view.content.gameObject:SetActive(WikiUtils.isWikiEntryUnread(entryId))
        else
            cell:InitItem({ id = entryShowData.wikiEntryData.refItemId }, function()
                if onItemClicked then
                    onItemClicked(cell, entryShowData)
                end
            end)
            if DeviceInfo.usingController then
                cell:SetEnableHoverTips(false)
            end
            if cell.view.potentialStar then
                cell.view.potentialStar.gameObject:SetActive(false)
            end

            if cell.view.levelNode then
                cell.view.levelNode.gameObject:SetActive(false)
            end
            cell:UpdateRedDot("WikiEntry", entryId)
        end

        if WikiUtils.isWikiEntryUnread(entryId) then
            readWikiEntries[entryId] = true
        end
        if isFirstClicked and luaIndex == 1 then
            if onItemClicked then
                onItemClicked(cell, entryShowData)
            end
            self:SetNaviTarget(cell.view.button)
        end
    end

    if wikiSearchResult.categoryId == WikiConst.EWikiCategoryType.Monster then
        self.m_itemCache:Refresh(0)
        self.m_monsterCache:Refresh(#wikiSearchResult.categoryResult, refreshFunc)
    else
        self.m_monsterCache:Refresh(0)
        self.m_itemCache:Refresh(#wikiSearchResult.categoryResult, refreshFunc)
    end
end

WikiSearchGroupItems.GetCellByEntryId = HL.Method(HL.String).Return(HL.Opt(HL.Any, HL.Table)) << function(self, entryId)
    if string.isEmpty(entryId) or not self.m_wikiSearchResult then
        return nil, nil
    end
    local cache = self.m_wikiSearchResult.categoryId == WikiConst.EWikiCategoryType.Monster and self.m_monsterCache or self.m_itemCache
    for luaIndex, entryShowData in ipairs(self.m_wikiSearchResult.categoryResult) do
        if entryShowData.wikiEntryData.id == entryId then
            return cache:GetItem(luaIndex), entryShowData
        end
    end
    return nil, nil
end

HL.Commit(WikiSearchGroupItems)
return WikiSearchGroupItems

