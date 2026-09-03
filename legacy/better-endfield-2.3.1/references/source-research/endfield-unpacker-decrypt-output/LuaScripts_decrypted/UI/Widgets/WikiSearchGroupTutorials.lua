local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiSearchGroupTutorials = HL.Class('WikiSearchGroupTutorials', UIWidgetBase)

WikiSearchGroupTutorials.m_tutorialCache = HL.Field(HL.Forward("UIListCache"))

WikiSearchGroupTutorials.m_wikiSearchResult = HL.Field(HL.Table)


WikiSearchGroupTutorials._OnFirstTimeInit = HL.Override() << function(self)
    self.m_tutorialCache = UIUtils.genCellCache(self.view.wikiTutorialTab)
end

WikiSearchGroupTutorials.InitWikiSearchGroupTutorials = HL.Method(HL.Table, HL.Function, HL.Opt(HL.Boolean, HL.String)) << function(
    self, wikiSearchResult, onItemClicked, isFirstClicked, selectedEntryId)
    self:_FirstTimeInit()
    self.m_wikiSearchResult = wikiSearchResult

    self.m_tutorialCache:GraduallyRefresh(#wikiSearchResult.categoryResult, self.config.GRADUALLY_SHOW_TIME, function(cell, luaIndex)
        local entryShowData = wikiSearchResult.categoryResult[luaIndex]
        cell:InitWikiTutorialTab(entryShowData, function()
            if onItemClicked then
                onItemClicked(cell, entryShowData)
            end
        end)
        local entryId = entryShowData.wikiEntryData.id
        cell.view.redDot:InitRedDot("WikiGuideEntry", entryId)

        if not string.isEmpty(selectedEntryId) and selectedEntryId == entryId then
            if onItemClicked then
                onItemClicked(cell, entryShowData)
            end
            self:SetNaviTarget(cell.view.btn)
        elseif isFirstClicked and luaIndex == 1 then
            if onItemClicked then
                onItemClicked(cell, entryShowData)
                if WikiUtils.isWikiEntryUnread(entryId) then
                    GameInstance.player.wikiSystem:MarkWikiEntryRead({ entryId })
                end
            end
            self:SetNaviTarget(cell.view.btn)
        end
    end)
end

WikiSearchGroupTutorials.GetCellByEntryId = HL.Method(HL.String).Return(HL.Opt(HL.Any, HL.Table)) << function(self, entryId)
    if string.isEmpty(entryId) or not self.m_wikiSearchResult then
        return nil, nil
    end
    for luaIndex, entryShowData in ipairs(self.m_wikiSearchResult.categoryResult) do
        if entryShowData.wikiEntryData.id == entryId then
            return self.m_tutorialCache:GetItem(luaIndex), entryShowData
        end
    end
    return nil, nil
end

HL.Commit(WikiSearchGroupTutorials)
return WikiSearchGroupTutorials

