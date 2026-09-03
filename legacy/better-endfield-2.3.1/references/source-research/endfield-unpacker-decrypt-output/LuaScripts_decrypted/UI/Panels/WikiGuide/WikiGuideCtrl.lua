
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiGuide
WikiGuideCtrl = HL.Class('WikiGuideCtrl', uiCtrl.UICtrl)







WikiGuideCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.WIKI_SELECT_ENTRY] = '_OnWikiSelectEntry',
}

local WIKI_GUIDE_ENTRY_CELL_PART_HEIGHT = {
    WIKI_GUIDE_ENTRY_BASE_HEIGHT = 100,
    WIKI_GUIDE_ENTRY_LOCK_TIP_HEIGHT = 30,
    WIKI_GUIDE_ENTRY_SPLIT_LINE_HEIGHT = 18,
    WIKI_GUIDE_ENTRY_SPACING_HEIGHT = 9
}


WikiGuideCtrl.m_typeTabCache = HL.Field(HL.Forward("UIListCache"))

WikiGuideCtrl.m_entryListCache = HL.Field(HL.Function)

WikiGuideCtrl.m_getMediaCell = HL.Field(HL.Function)

WikiGuideCtrl.m_pageIndexToggleCache = HL.Field(HL.Forward("UIListCache"))

WikiGuideCtrl.m_refBtnCache = HL.Field(HL.Forward("UIListCache"))






WikiGuideCtrl.m_latestUnlockCnt = HL.Field(HL.Number) << 0


WikiGuideCtrl.m_showingLatestUnlock = HL.Field(HL.Boolean) << false


WikiGuideCtrl.m_wikiGroupShowDataList = HL.Field(HL.Table)

WikiGuideCtrl.m_arg = HL.Field(HL.Table)


WikiGuideCtrl.m_allGroupEntryList = HL.Field(HL.Table)


WikiGuideCtrl.m_entryListByGroup = HL.Field(HL.Table)


WikiGuideCtrl.m_showingEntryList = HL.Field(HL.Table)

WikiGuideCtrl.m_showingEntryCnt = HL.Field(HL.Number) << 0


WikiGuideCtrl.m_showingEntryData = HL.Field(HL.Table)
WikiGuideCtrl.m_currentGroupId = HL.Field(HL.String) << ""


WikiGuideCtrl.m_selectedIndex = HL.Field(HL.Number) << 0

WikiGuideCtrl.m_pendingNaviTargetIndex = HL.Field(HL.Number) << 0


WikiGuideCtrl.m_toShowDetail = HL.Field(HL.Table)








WikiGuideCtrl.m_pagesByEntryId = HL.Field(HL.Table)

WikiGuideCtrl.m_showingPageList = HL.Field(HL.Table)

WikiGuideCtrl.m_isShowingLastPage = HL.Field(HL.Boolean) << false

WikiGuideCtrl.m_isShowingFirstPage = HL.Field(HL.Boolean) << false



WikiGuideCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_arg = args or {}
    self:_InitController()

    self.m_typeTabCache = UIUtils.genCellCache(self.view.typeCellTemplate)


    local entryList = self.view.entryList
    self.m_entryListCache = UIUtils.genCachedCellFunction(entryList)
    entryList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_entryListCache(obj)
        local luaIndex = LuaIndex(csIndex)

        local entryId = self.m_showingEntryList[luaIndex].wikiEntryData.id
        if WikiUtils.isWikiEntryUnread(entryId) then
            self.m_readWikiEntries[entryId] = true
        end
        cell.wikiTutorialTab:InitWikiTutorialTab(self.m_showingEntryList[luaIndex], function()
            self:SetSelectedEntryIndex(luaIndex)
        end)
        local isSelected = luaIndex == self.m_selectedIndex
        cell.wikiTutorialTab:SetSelected(isSelected)

        cell.lockTipsNode.gameObject:SetActive(self.m_showingLatestUnlock and csIndex == 0)
        cell.cutOffRuleNode.gameObject:SetActive(self.m_showingLatestUnlock and csIndex + 1 == self.m_latestUnlockCnt)
        cell.redDot:InitRedDot("WikiGuideEntry", self.m_showingEntryList[LuaIndex(csIndex)].wikiEntryData.id, nil, self.view.redDotScrollRect)
    end)
    entryList.onGraduallyShowFinish:AddListener(function()
        self:_TryNaviToSelectedEntry()
    end)
    entryList.getCellSize = function(csIndex)
        local showLockTip = self.m_showingLatestUnlock and csIndex == 0
        local showSplitLine = self.m_showingLatestUnlock and csIndex + 1 == self.m_latestUnlockCnt
        local cellHeight = WIKI_GUIDE_ENTRY_CELL_PART_HEIGHT.WIKI_GUIDE_ENTRY_BASE_HEIGHT
        if showLockTip then
            cellHeight = cellHeight + WIKI_GUIDE_ENTRY_CELL_PART_HEIGHT.WIKI_GUIDE_ENTRY_LOCK_TIP_HEIGHT
        end
        if showSplitLine then
            cellHeight = cellHeight + WIKI_GUIDE_ENTRY_CELL_PART_HEIGHT.WIKI_GUIDE_ENTRY_SPLIT_LINE_HEIGHT
            cellHeight = cellHeight + WIKI_GUIDE_ENTRY_CELL_PART_HEIGHT.WIKI_GUIDE_ENTRY_SPACING_HEIGHT
        end
        return cellHeight
    end
    self.view.redDotScrollRect.getRedDotStateAt = function(index)
        return self:_GetRedDotStateAt(index)
    end



    local mediaList = self.view.guideMediaNode.mediaList
    self.m_getMediaCell = UIUtils.genCachedCellFunction(mediaList)
    mediaList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateMediaCell(obj, csIndex)
    end)
    mediaList.onCenterIndexChanged:AddListener(function(oldIndex, newIndex)
        self:_OnUpdateCurrentPageIndex(newIndex)
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})


    self.m_pageIndexToggleCache = UIUtils.genCellCache(self.view.guideMediaNode.indexToggle)

    self.m_refBtnCache = UIUtils.genCellCache(self.view.wikiRefBtn)


    self.m_pagesByEntryId = {}
    for _, pageData in pairs(Tables.wikiTutorialPageTable) do
        local pageList = self.m_pagesByEntryId[pageData.tutorialId]
        if not pageList then
            pageList = {}
            self.m_pagesByEntryId[pageData.tutorialId] = pageList
        end
        local curPageData = pageList[pageData.order]
        if curPageData then
            if WikiUtils.IsTutorialPageCurrentInputTypeSpecific(pageData.id) then
                pageList[pageData.order] = pageData
            end
        else
            if WikiUtils.IsTutorialPageCurrentInputTypeValid(pageData.id) then
                pageList[pageData.order] = pageData
            end
        end
    end


    self.view.guideMediaNode.leftButton.onClick:AddListener(function()
        if self.m_isShowingFirstPage then
            self:SetSelectedEntryIndex(self.m_selectedIndex - 1, true, true)
            return
        end
        self:SwitchPage( self.view.guideMediaNode.mediaList.centerIndex - 1)
    end)
    self.view.guideMediaNode.rightButton.onClick:AddListener(function()
        if self.m_isShowingLastPage then
            self:SetSelectedEntryIndex(self.m_selectedIndex + 1, true, true)
            return
        end
        self:SwitchPage(self.view.guideMediaNode.mediaList.centerIndex + 1)
    end)

    self.m_selectedIndex = 0
    self.m_toShowDetail = args
    self.m_readWikiEntries = {}

    self:InitView()
    self:RefreshTop()
    self.m_phase:ActiveCommonSceneItem(true)
    self:_PlayDecoAnim(true)
end

WikiGuideCtrl.OnClose = HL.Override() << function(self)
    self:_MarkWikiEntryRead()
    UIManager:Close(PanelId.WikiGuideTips)
    self:GameEventLogExit()
end

WikiGuideCtrl.OnShow = HL.Override() << function(self)
    self.m_toShowDetail = self:GetCurPhaseStateArg()
    self:InitView()
    if self.m_phase then
        self.m_phase:ActiveCommonSceneItem(true)
    end
    self:_PlayDecoAnim(true)
    self:GameEventLogEnter()
end

WikiGuideCtrl.OnHide = HL.Override() << function(self)
    self:_MarkWikiEntryRead()
    self:GameEventLogExit()
end

WikiGuideCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    WikiGuideCtrl.Super._OnPlayAnimationOut(self)
    self:_PlayDecoAnim(false)
end

WikiGuideCtrl.m_readWikiEntries = HL.Field(HL.Table)

WikiGuideCtrl._MarkWikiEntryRead = HL.Method() << function(self)
    if self.m_readWikiEntries then
        local entryIdList = {}
        for entryId, _ in pairs(self.m_readWikiEntries) do
            table.insert(entryIdList, entryId)
        end
        GameInstance.player.wikiSystem:MarkWikiEntryRead(entryIdList)
        self.m_readWikiEntries = {}
    end
end

WikiGuideCtrl.InitView = HL.Method() << function(self)
    self.m_wikiGroupShowDataList = WikiUtils.getWikiGroupShowDataList(WikiConst.EWikiCategoryType.Tutorial)

    self.m_allGroupEntryList = {}
    self.m_entryListByGroup = {}


    local latestUnlockMaxNum = Tables.globalConst.wikiLatestUnlockNum

    local latestUnlockEntryIds = GameInstance.player.wikiSystem:GetLatestUnlockedEntryIds()
    local realCnt = latestUnlockEntryIds.Count
    if realCnt > latestUnlockMaxNum then
        realCnt = latestUnlockMaxNum
    end
    self.m_latestUnlockCnt = realCnt
    local lut = {}

    for i = 1, realCnt do
        local entryId = latestUnlockEntryIds[CSIndex(i)]
        if entryId then
            lut[entryId] = realCnt - i + 1
        end
    end


    local normalIndex = realCnt + 1
    for _, groupData in pairs(self.m_wikiGroupShowDataList) do
        self.m_entryListByGroup[groupData.wikiGroupData.groupId] = groupData.wikiEntryShowDataList
        for _, entryData in pairs(groupData.wikiEntryShowDataList) do
            local latestUnlockIndex = lut[entryData.wikiEntryData.id]
            if latestUnlockIndex then
                self.m_allGroupEntryList[latestUnlockIndex] = entryData
            else
                self.m_allGroupEntryList[normalIndex] = entryData
                normalIndex = normalIndex + 1
            end
        end
    end


    self.m_typeTabCache:Refresh(#self.m_wikiGroupShowDataList, function(cell, luaIndex)
        local groupData = self.m_wikiGroupShowDataList[luaIndex]
        cell.titleTxt.text = groupData.wikiGroupData.groupName
        cell.icon:LoadSprite(UIConst.UI_SPRITE_WIKI_GROUP, groupData.wikiGroupData.iconId)
        cell.offIcon:LoadSprite(UIConst.UI_SPRITE_WIKI_GROUP, groupData.wikiGroupData.iconId)
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:SwitchToGroup(groupData.wikiGroupData.groupId, nil, true)
                self:_RefreshTypeCellLineImg(luaIndex)
            end
        end)
        cell.redDot:InitRedDot("WikiGroup", groupData.wikiGroupData.groupId)
        cell.lineImage.gameObject:SetActive(luaIndex ~= #self.m_wikiGroupShowDataList)
    end)
    self.view.typeCellAll.titleTxt.text = Language.LUA_WIKI_TUTORIAL_ALL_TYPE
    self.view.typeCellAll.toggle.onValueChanged:RemoveAllListeners()
    self.view.typeCellAll.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:SwitchToGroup(nil, nil, true)
        end
    end)
    self.view.typeCellAll.redDot:InitRedDot("WikiCategory", WikiConst.EWikiCategoryType.Tutorial)

   self:Refresh(self.m_toShowDetail)
end

WikiGuideCtrl._RefreshTypeCellLineImg = HL.Method(HL.Number) << function(self, selectedIndex)
    local typeTabCnt = self.m_typeTabCache:GetCount()
    for i = 1, typeTabCnt do
        local cell = self.m_typeTabCache:GetItem(i)
        cell.lineImage.gameObject:SetActive(i ~= typeTabCnt and i ~= selectedIndex - 1)
    end

end


WikiGuideCtrl.Refresh = HL.Method(HL.Table) << function(self, args)
    self.m_arg = args or {}

    if args then
        self.m_showingEntryData = args.wikiEntryShowData
    else
        self.m_showingEntryData = self.m_allGroupEntryList[1]
    end

    local resumeState = args and args.resumeState or nil
    if resumeState then
        self:_ApplyResumeState(resumeState)
    else

        self.view.typeCellAll.toggle:SetIsOnWithoutNotify(true)
        self:SwitchToGroup()
    end
    if self.m_arg then
        self.m_arg.resumeState = nil
    end
end


WikiGuideCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    arg.categoryType = WikiConst.EWikiCategoryType.Tutorial
    arg.wikiEntryShowData = self.m_showingEntryData
    arg.wikiGroupShowDataList = self.m_wikiGroupShowDataList
    arg.resumeState = {
        groupId = self:_GetCurrentGroupId(),
        selectedEntryId = self.m_showingEntryData and self.m_showingEntryData.wikiEntryData.id or nil,
        pageIndex = self.view.guideMediaNode.mediaList.centerIndex,
    }
    return arg
end

WikiGuideCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    local groupRestored = false
    if not string.isEmpty(resumeState.groupId) then
        for luaIndex = 1, self.m_typeTabCache:GetCount() do
            local groupData = self.m_wikiGroupShowDataList[luaIndex]
            if groupData and groupData.wikiGroupData.groupId == resumeState.groupId then
                local cell = self.m_typeTabCache:GetItem(luaIndex)
                if cell then
                    cell.toggle:SetIsOnWithoutNotify(true)
                end
                self:SwitchToGroup(resumeState.groupId, false)
                self:_RefreshTypeCellLineImg(luaIndex)
                groupRestored = true
                break
            end
        end
    end
    if not groupRestored then
        self.view.typeCellAll.toggle:SetIsOnWithoutNotify(true)
        self:SwitchToGroup(nil, false)
    end
    local entryRestored = false
    if not string.isEmpty(resumeState.selectedEntryId) and self.m_showingEntryList then
        for luaIndex, entryShowData in ipairs(self.m_showingEntryList) do
            if entryShowData.wikiEntryData.id == resumeState.selectedEntryId then
                self:SetSelectedEntryIndex(luaIndex, true, true)
                entryRestored = true
                break
            end
        end
    end
    if not entryRestored and self.m_selectedIndex > 0 then
        self:SetSelectedEntryIndex(self.m_selectedIndex, true, true)
    end
    local pageIndex = resumeState.pageIndex or 0
    if self.m_showingPageList and #self.m_showingPageList > 0 then
        pageIndex = lume.clamp(pageIndex, 0, #self.m_showingPageList - 1)
        self:SwitchPage(pageIndex)
    end
end

WikiGuideCtrl._GetCurrentGroupId = HL.Method().Return(HL.String) << function(self)
    return self.m_currentGroupId or ""
end

WikiGuideCtrl.SwitchToGroup = HL.Method(HL.Opt(HL.String, HL.Boolean, HL.Boolean)) << function(self, groupId, setNaviTarget, selectFirst)
    self:_MarkWikiEntryRead()
    self.m_selectedIndex = -1
    self.m_currentGroupId = groupId or ""

    self.m_showingLatestUnlock = not groupId and self.m_latestUnlockCnt > 0

    local entryListToShow = groupId and self.m_entryListByGroup[groupId] or self.m_allGroupEntryList
    local selectIndex = 1
    if not selectFirst and (not groupId or self.m_showingEntryData.wikiGroupData.groupId == groupId) then

        for index, entryData in pairs(entryListToShow) do
            if entryData.wikiEntryData.id == self.m_showingEntryData.wikiEntryData.id then
                selectIndex = index
                break
            end
        end
    end

    self:RefreshEntryList(entryListToShow, CSIndex(selectIndex))
    self:SetSelectedEntryIndex(selectIndex, nil, setNaviTarget ~= false)
end


WikiGuideCtrl.RefreshEntryList = HL.Method(HL.Table, HL.Opt(HL.Number)) << function(self, targetList, selectCsIndex)
    self.m_showingEntryList = targetList
    self.m_showingEntryCnt = #targetList
    if selectCsIndex then
        self.view.entryList:UpdateCount(self.m_showingEntryCnt, selectCsIndex, true, false, false, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
    else
        self.view.entryList:UpdateCount(self.m_showingEntryCnt, false, true)
    end
end

WikiGuideCtrl._TryNaviToSelectedEntry = HL.Method(HL.Opt(HL.Number)).Return(HL.Boolean) << function(self, luaIndex)
    if not DeviceInfo.usingController then
        return false
    end
    local targetIndex = luaIndex
    if not targetIndex or targetIndex <= 0 then
        targetIndex = self.m_pendingNaviTargetIndex
    end
    if not targetIndex or targetIndex <= 0 then
        targetIndex = self.m_selectedIndex
    end
    if not targetIndex or targetIndex <= 0 then
        return false
    end
    local entryGo = self.view.entryList:Get(CSIndex(targetIndex))
    local entryCell = entryGo and self.m_entryListCache(entryGo) or nil
    if not entryCell then
        self.m_pendingNaviTargetIndex = targetIndex
        return false
    end
    self.m_pendingNaviTargetIndex = 0
    self:SetNaviTarget(entryCell.wikiTutorialTab.view.btn)
    return true
end


WikiGuideCtrl.SetSelectedEntryIndex = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, luaIndex, scrollToEntry, setNaviTarget)
    if self.m_selectedIndex == luaIndex then
        local entryGo = self.view.entryList:Get(CSIndex(luaIndex))
        local entryCell = entryGo and self.m_entryListCache(entryGo) or nil
        if scrollToEntry == true then
            if not entryCell then
                self.view.entryList:ScrollToIndex(CSIndex(luaIndex))
            else
                self.view.entryListScrollRect:AutoScrollToRectTransform(entryGo.transform)
            end
        end
        if setNaviTarget == true then
            self.m_pendingNaviTargetIndex = luaIndex
            self:_TryNaviToSelectedEntry(luaIndex)
        end
        return
    end
    if self.m_selectedIndex > 0 then
        self:SetEntryCellSelected(self.m_entryListCache(self.view.entryList:Get(CSIndex(self.m_selectedIndex))), false)
    end
    self.m_selectedIndex = luaIndex
    local entryGo = self.view.entryList:Get(CSIndex(self.m_selectedIndex))
    local entryCell = self.m_entryListCache(entryGo)
    self:SetEntryCellSelected(entryCell, true)
    self:RefreshContent(self.m_showingEntryList[luaIndex])
    if scrollToEntry == true then
        if not entryCell then
            self.view.entryList:ScrollToIndex(CSIndex(self.m_selectedIndex))
        else
            self.view.entryListScrollRect:AutoScrollToRectTransform(entryGo.transform)
        end
    end
    if setNaviTarget == true then
        self.m_pendingNaviTargetIndex = luaIndex
        self:_TryNaviToSelectedEntry(luaIndex)
    end

    local entryId = self.m_showingEntryList[luaIndex].wikiEntryData.id
    if WikiUtils.isWikiEntryUnread(entryId) then
        GameInstance.player.wikiSystem:MarkWikiEntryRead({ entryId })
    end
    local entryShowData = self.m_showingEntryList[luaIndex]

    EventLogManagerInst:GameEvent_WikiEntry(entryShowData.wikiCategoryType,
        entryShowData.wikiEntryData.groupId, entryShowData.wikiEntryData.id)
end


WikiGuideCtrl.SetEntryCellSelected = HL.Method(HL.Table, HL.Boolean) << function(self, cell, selected)
    if not cell then
        return
    end
    cell.wikiTutorialTab:SetSelected(selected, true)
end




WikiGuideCtrl.RefreshContent = HL.Method(HL.Table) << function(self, entryShowData)
    self.m_showingEntryData = entryShowData
    self:RefreshTop()
    local entryId = self.m_showingEntryData.wikiEntryData.id
    self.m_showingPageList = self.m_pagesByEntryId[entryId]
    local pageCnt = #self.m_showingPageList


    self.m_pageIndexToggleCache:Refresh(pageCnt, function(cell, luaIndex)
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:SwitchPage(CSIndex(luaIndex))
            end
        end)
    end)


    self.view.guideMediaNode.mediaList:UpdateCount(pageCnt, true)
    self.view.centerAnimWrapper:ClearTween(false)
    self.view.centerAnimWrapper:PlayInAnimation()

    self:_OnUpdateCurrentPageIndex(0)
end


WikiGuideCtrl.SwitchPage = HL.Method(HL.Number) << function(self, pageCsIndex)
    local mediaNode = self.view.guideMediaNode
    mediaNode.mediaList:ScrollToIndex(pageCsIndex)
    self.view.centerAnimWrapper:ClearTween(false)
    self.view.centerAnimWrapper:PlayInAnimation()
end


WikiGuideCtrl._OnUpdateMediaCell = HL.Method(GameObject, HL.Number) << function(self, obj, csIndex)
    local pageData = self.m_showingPageList[LuaIndex(csIndex)]
    local cell = self.m_getMediaCell(obj)
    cell:InitWikiGuideMediaCell(pageData.id)
end


WikiGuideCtrl._OnUpdateCurrentPageIndex = HL.Method(HL.Number) << function(self, csIndex)
    if csIndex < 0 then
        return
    end
    local pageData = self.m_showingPageList[LuaIndex(csIndex)]
    local mediaNode = self.view.guideMediaNode

    Notify(MessageConst.ON_STOP_HOVER_LINK)
    mediaNode.titleTxt:SetAndResolveTextStyle(pageData.title)
    mediaNode.contentTxt:SetAndResolveTextStyle(InputManager.ParseTextActionId(pageData.content))

    local unlockedTips = {}

    local wikiSystem = GameInstance.player.wikiSystem
    for _, wikiEntryId in pairs(pageData.refWikiEntryIds) do
        if wikiSystem:GetWikiEntryState(wikiEntryId) ~= CS.Beyond.Gameplay.WikiSystem.EWikiEntryState.Locked then
            table.insert(unlockedTips, wikiEntryId)
        end
    end
    local refBtnCnt = #unlockedTips
    self.m_refBtnCache:Refresh(refBtnCnt, function(cell, refBtnLuaIndex)
        cell:InitWikiRefBtn(unlockedTips[refBtnLuaIndex])
    end)
    self.view.wikiRefTitle.gameObject:SetActive(refBtnCnt ~= 0)
    self.view.leftNode.gameObject:SetActive(refBtnCnt ~= 0)
    if DeviceInfo.usingController then
        self.view.wikiRefNaviGroup.enabled = refBtnCnt ~= 0
        self.view.controllerFocusHintNode.gameObject:SetActive(refBtnCnt ~= 0)
    end

    self.m_pageIndexToggleCache:GetItem(LuaIndex(csIndex)).toggle:SetIsOnWithoutNotify(true)
    self.m_isShowingFirstPage = csIndex == 0
    self.m_isShowingLastPage = csIndex + 1 == #self.m_showingPageList
    mediaNode.leftButton.interactable = self.m_selectedIndex > 1 or not self.m_isShowingFirstPage
    mediaNode.rightButton.interactable = not (self.m_isShowingLastPage and self.m_selectedIndex >= self.m_showingEntryCnt)
end


WikiGuideCtrl.RefreshTop = HL.Method() << function(self)
    if not self.m_phase then
        return
    end
    local wikiTopArgs = {
        phase = self.m_phase,
        panelId = PANEL_ID,
        categoryType = self.m_showingEntryData.wikiCategoryType,
        wikiEntryShowData = self.m_showingEntryData
    }
    self.view.top:InitWikiTop(wikiTopArgs)
end

WikiGuideCtrl._PlayDecoAnim = HL.Method(HL.Boolean) << function(self, isIn)
    if self.m_phase then
        self.m_phase:PlayDecoAnim(isIn and "wiki_uideco_grouppanel_in" or "wiki_uideco_grouppanel_out")
    end
end

WikiGuideCtrl._GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_showingEntryList then
        return 0
    end
    local wikiEntryShowData = self.m_showingEntryList[luaIndex]
    if not wikiEntryShowData then
        return 0
    end
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("WikiGuideEntry", wikiEntryShowData.wikiEntryData.id)
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end

WikiGuideCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.wikiRefNaviGroup.onIsFocusedChange:AddListener(function(isTopLayer)
        self.view.controllerFocusHintNode.gameObject:SetActive(not isTopLayer and self.view.wikiRefNaviGroup.enabled)
        if not isTopLayer then
            Notify(MessageConst.HIDE_WIKI_REF_TIPS)
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
    UIUtils.bindHyperlinkPopup(self, "wiki_guide_detail", self.view.inputGroup.groupId)
end



WikiGuideCtrl.m_enterTime = HL.Field(HL.Number) << -1

WikiGuideCtrl.GameEventLogEnter = HL.Method() << function(self)
    self.m_enterTime = Time.realtimeSinceStartup
    EventLogManagerInst:GameEvent_WikiCategory(true, WikiConst.EWikiCategoryType.Tutorial, 0)
end

WikiGuideCtrl.GameEventLogExit = HL.Method() << function(self)
    if self.m_enterTime < 0 then
        return
    end
    local stayTime = Time.realtimeSinceStartup - self.m_enterTime
    self.m_enterTime = -1
    EventLogManagerInst:GameEvent_WikiCategory(false, WikiConst.EWikiCategoryType.Tutorial, stayTime)
end



HL.Commit(WikiGuideCtrl)
