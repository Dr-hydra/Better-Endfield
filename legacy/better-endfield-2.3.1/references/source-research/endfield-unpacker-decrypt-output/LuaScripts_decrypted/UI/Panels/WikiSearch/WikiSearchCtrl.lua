local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiSearch

WikiSearchCtrl = HL.Class('WikiSearchCtrl', uiCtrl.UICtrl)

local STATE_NAME = {
    SEARCH = "search",
    RESULT = "result",
    EMPTY = "empty",
}

local CONTENT_IN_ANIM = "wiki_search_content_in"
local CONTENT_OUT_ANIM = "wiki_search_content_out"





WikiSearchCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHECK_SENSITIVE_SUCCESS] = '_OnCheckSensitiveSuccess',
}

WikiSearchCtrl.m_isShowingResult = HL.Field(HL.Boolean) << false

WikiSearchCtrl.m_arg = HL.Field(HL.Table)

WikiSearchCtrl.m_pendingResumeState = HL.Field(HL.Any)

WikiSearchCtrl.m_checkSensitiveKeyword = HL.Field(HL.Any)




WikiSearchCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg or {}
    self:_InitController()
    self.m_readWikiEntries = {}
    self.view.btnBackBlack.onClick:AddListener(function()

        self:_ClearSearch(true)
        self:PlayAnimationOutAndHide()
    end)
    self.view.detailsBtn.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            Notify(MessageConst.SHOW_WIKI_ENTRY, { wikiEntryId = self.m_curWikiEntryShowData.wikiEntryData.id })
        end)
    end)

    UIUtils.initSearchInput(self.view.inputField, {
        clearBtn = self.view.clearBtn,
        searchBtn = self.view.searchBtn,
        onInputValueChanged = function(text)
            local hasContent = not string.isEmpty(self.view.inputField.text)
            self.view.searchBtn.interactable = hasContent
        end,
        onInputSubmit = function()
            self:_OnSearchBtnClicked()
        end,
        onInputFocused = function()
            local isEmpty = string.isEmpty(self.view.inputField.text)
            self.view.searchBtn.interactable = not isEmpty
            if not self.m_isShowingResult then
                self:_ShowHistory()
            end
        end,
        onInputEndEdit = function()
            local isPsController = Utils.checkIsPSDevice()
            if isPsController then
                self:_OnSearchBtnClicked()
            end
            self.view.inputField:DeactivateInputField(true)
            self.view.searchNodeNaviGroup:ManuallyStopFocus()
        end,
        onClearClick = function()
            self:_ClearSearch(false, false, true)
        end,
        onSearchClick = function()
            self:_OnSearchBtnClicked()
        end,
    })
    self.view.inputField.onValidateCharacterLimit = I18nUtils.GetRealTextByLengthLimit
    self.view.inputField.onGetTextLength = I18nUtils.GetTextRealLength
    self:_ClearSearch(false, true)
    self:_ApplyResumeState(arg and arg.resumeState or nil)
end

WikiSearchCtrl.OnShow = HL.Override() << function(self)
    self.view.selectableNaviGroup:NaviToThisGroup()
    local resumeState = self.m_pendingResumeState or (self.m_arg and self.m_arg.resumeState or nil)
    if resumeState and resumeState.isShowingResult == true and not self.m_isShowingResult then
        self:_RestoreSearchResult(resumeState.keyword, resumeState.selectedEntryId)
    end
    if string.isEmpty(self.view.inputField.text) then
        if DeviceInfo.usingController then
            self:_StartCoroutine(function()
                self.view.searchNodeNaviGroup:ManuallyFocus()
            end)
        else
            self.view.inputField:ActivateInputField()
        end
        self:_ShowHistory()
    else
        if DeviceInfo.usingController then
            self.view.leftNaviGroup:NaviToThisGroup()
        end
    end
    self.m_pendingResumeState = nil
    if self.m_arg then
        self.m_arg.resumeState = nil
    end
end

WikiSearchCtrl.OnHide = HL.Override() << function (self)
    AudioAdapter.PostEvent("Au_UI_Popup_Common_Large_Close")
    Notify(MessageConst.QUICK_HIDE_FULL_SCREEN_SCENE_BLUR)
end

WikiSearchCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    arg.resumeState = {
        keyword = self.view.inputField.text,
        isShowingResult = self.m_isShowingResult == true,
        selectedEntryId = self.m_curWikiEntryShowData and self.m_curWikiEntryShowData.wikiEntryData.id or nil,
    }
    return arg
end

WikiSearchCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState or string.isEmpty(resumeState.keyword) then
        return
    end
    self.view.inputField:SetTextWithoutNotify(resumeState.keyword)
    self.view.searchBtn.interactable = true
    if resumeState.isShowingResult == true and self.m_phase then
        self:_RestoreSearchResult(resumeState.keyword, resumeState.selectedEntryId)
    else
        self.m_pendingResumeState = lume.deepCopy(resumeState)
    end
end

WikiSearchCtrl._RestoreSearchResult = HL.Method(HL.String, HL.Opt(HL.String)) << function(self, keyword, selectedEntryId)
    self.m_phase.curSearchKeyword = keyword
    Notify(MessageConst.ON_WIKI_SEARCH_KEYWORD_CHANGED, keyword)
    self.m_isShowingResult = true
    local resultItems, resultTutorials = self:_GetSearchResult(keyword)
    if #resultItems == 0 and #resultTutorials == 0 then
        self.view.emptyTxt.text = string.format(Language.LUA_WIKI_SEARCH_NOT_FOUND_FORMAT, keyword)
        self.view.stateCtrl:SetState(STATE_NAME.EMPTY)
        return
    end
    self:_RefreshResult(resultItems, resultTutorials, selectedEntryId)
    self:_RestoreSelectedItem(selectedEntryId)
end

WikiSearchCtrl._RestoreSelectedItem = HL.Method(HL.Opt(HL.String)) << function(self, selectedEntryId)
    if string.isEmpty(selectedEntryId) then
        return
    end
    if self.m_groupItemsCache then
        for luaIndex = 1, self.m_groupItemsCache:GetCount() do
            local groupCell = self.m_groupItemsCache:GetItem(luaIndex)
            if groupCell and groupCell.GetCellByEntryId then
                local itemCell, entryShowData = groupCell:GetCellByEntryId(selectedEntryId)
                if itemCell and entryShowData then
                    self:_SetItemSelected(itemCell, entryShowData)
                    self:SetNaviTarget(itemCell.view.button)
                    return
                end
            end
        end
    end
    if self.m_groupTutorialsCache then
        for luaIndex = 1, self.m_groupTutorialsCache:GetCount() do
            local groupCell = self.m_groupTutorialsCache:GetItem(luaIndex)
            if groupCell and groupCell.GetCellByEntryId then
                local itemCell, entryShowData = groupCell:GetCellByEntryId(selectedEntryId)
                if itemCell and entryShowData then
                    self:_SetItemSelected(itemCell, entryShowData)
                    self:SetNaviTarget(itemCell.view.btn)
                    return
                end
            end
        end
    end
end



WikiSearchCtrl._OnSearchBtnClicked = HL.Method() << function(self)
    local keyword = string.trim(self.view.inputField.text)
    self.view.inputField.text = keyword
    if DeviceInfo.usingController then
        self.view.searchNodeNaviGroup:ManuallyStopFocus()
    end
    if string.isEmpty(keyword) or keyword == self.m_phase.curSearchKeyword then
        return
    end
    if I18nUtils.GetTextRealLength(keyword) <= 1 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_WIKI_SEARCH_KEYWORD_TOO_SHORT)
        return
    end
    self.view.inputField:DeactivateInputField(true)
    self.m_checkSensitiveKeyword = keyword
    GameInstance.player.wikiSystem:CheckSensitive(keyword)
end

WikiSearchCtrl._OnCheckSensitiveSuccess = HL.Method() << function(self)
    if not self.m_checkSensitiveKeyword then
        return
    end
    local keyword = self.m_checkSensitiveKeyword
    self.m_checkSensitiveKeyword = nil
    self:_Search(keyword)
end

WikiSearchCtrl._Search = HL.Method(HL.String) << function(self, keyword)
    self.m_phase.curSearchKeyword = keyword
    Notify(MessageConst.ON_WIKI_SEARCH_KEYWORD_CHANGED, keyword)
    self.m_isShowingResult = true
    local resultItems, resultTutorials = self:_GetSearchResult(keyword)
    if #resultItems == 0 and #resultTutorials == 0 then
        self.view.emptyTxt.text = string.format(Language.LUA_WIKI_SEARCH_NOT_FOUND_FORMAT, keyword)
        self.view.stateCtrl:SetState(STATE_NAME.EMPTY)
        return
    end
    WikiUtils.addHistorySearchKeyword(keyword)
    self:_RefreshResult(resultItems, resultTutorials)
    AudioAdapter.PostEvent("Au_UI_Popup_Common_Large_Open")
end

WikiSearchCtrl._ClearSearch = HL.Method(HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean)) << function(self, isClosed, showHistory, activeInput)
    if self.m_phase then
        self.m_phase.curSearchKeyword = ""
    end
    self.view.inputField.text = ""
    Notify(MessageConst.ON_WIKI_SEARCH_KEYWORD_CHANGED, "")
    if isClosed == true then
        return
    end
    if activeInput then
        if DeviceInfo.usingController then
            self.view.searchNodeNaviGroup:ManuallyFocus()
        else
            self.view.inputField:ActivateInputField()
        end
    end

    if showHistory then
        self:_ShowHistory()
    end
end

WikiSearchCtrl._ShowHistory = HL.Method() << function(self)
    local function show()
        self.view.stateCtrl:SetState(STATE_NAME.SEARCH)
        self.m_isShowingResult = false
        self:_RefreshHistory()
        self:_MarkWikiEntryRead()
    end

    if self.m_isShowingResult then
        self:PlayAnimation(CONTENT_OUT_ANIM, show)
    else
        show()
    end
end





WikiSearchCtrl._GetSearchResult = HL.Method(HL.String).Return(HL.Table, HL.Table) << function(self, keyword)

    local resultItems = {}

    local resultTutorials = {}
    local hasValue
    for categoryId, _ in pairs(Tables.wikiCategoryTable) do
        local categoryResult = {}
        local wikiGroupDataList = Tables.wikiGroupTable[categoryId]
        for _, wikiGroupData in pairs(wikiGroupDataList.list) do
            local _, wikiEntryList = Tables.wikiEntryTable:TryGetValue(wikiGroupData.groupId)
            if wikiEntryList then
                for _, wikiEntryId in pairs(wikiEntryList.list) do
                    if GameInstance.player.wikiSystem:GetWikiEntryState(wikiEntryId) ~=
                        CS.Beyond.Gameplay.WikiSystem.EWikiEntryState.Locked then
                        local _, wikiEntryData = Tables.wikiEntryDataTable:TryGetValue(wikiEntryId)
                        local entryDesc
                        if not string.isEmpty(wikiEntryData.refItemId) then

                            local itemData
                            hasValue, itemData = Tables.itemTable:TryGetValue(wikiEntryData.refItemId)
                            if hasValue then
                                entryDesc = itemData.name
                            end
                        elseif not string.isEmpty(wikiEntryData.refMonsterTemplateId) then

                            local enemyDisplayInfoData
                            hasValue, enemyDisplayInfoData = Tables.enemyTemplateDisplayInfoTable:TryGetValue(wikiEntryData.refMonsterTemplateId)
                            if hasValue then
                                entryDesc = enemyDisplayInfoData.name
                            end
                        else
                            entryDesc = wikiEntryData.desc
                        end
                        if entryDesc and string.find(string.lower(entryDesc), string.lower(keyword), 1, true) then
                            table.insert(categoryResult, {
                                wikiCategoryType = categoryId,
                                wikiGroupData = wikiGroupData,
                                wikiEntryData = wikiEntryData,
                            })
                        end
                    end
                end
            end
        end
        if #categoryResult > 0 then
            local result = categoryId == WikiConst.EWikiCategoryType.Tutorial and resultTutorials or resultItems
            table.insert(result, {
                categoryId = categoryId,
                categoryResult = categoryResult,
            })
        end
    end
    return resultItems, resultTutorials
end

WikiSearchCtrl.m_groupItemsCache = HL.Field(HL.Forward("UIListCache"))

WikiSearchCtrl.m_groupTutorialsCache = HL.Field(HL.Forward("UIListCache"))

WikiSearchCtrl._RefreshResult = HL.Method(HL.Table, HL.Table, HL.Opt(HL.String)) << function(self, resultItems, resultTutorials, selectedEntryId)
    self:_MarkWikiEntryRead()
    self.view.stateCtrl:SetState(STATE_NAME.RESULT)
    local contentPosition = self.view.scrollContent.localPosition
    contentPosition.y = 0
    self.view.scrollContent.localPosition = contentPosition
    self:PlayAnimation(CONTENT_IN_ANIM)

    local isFirstSelected = nil
    if not self.m_groupItemsCache then
        self.m_groupItemsCache = UIUtils.genCellCache(self.view.wikiSearchGroupItems)
    end
    self.m_groupItemsCache:Refresh(#resultItems, function(cell, luaIndex)
        isFirstSelected = true
        local wikiSearchResult = resultItems[luaIndex]
        cell:InitWikiSearchGroupItems(wikiSearchResult, function(itemCell, entryShowData)
            self:_SetItemSelected(itemCell, entryShowData)
        end, self.m_readWikiEntries, luaIndex == 1)
        cell.view.transform:SetAsLastSibling()
    end)

    if not self.m_groupTutorialsCache then
        self.m_groupTutorialsCache = UIUtils.genCellCache(self.view.wikiSearchGroupTutorials)
    end
    self.m_groupTutorialsCache:Refresh(#resultTutorials, function(cell, luaIndex)
        local wikiSearchResult = resultTutorials[luaIndex]
        cell:InitWikiSearchGroupTutorials(wikiSearchResult, function(itemCell, entryShowData)
            self:_SetItemSelected(itemCell, entryShowData)
        end, not isFirstSelected and luaIndex == 1, selectedEntryId)
        cell.view.transform:SetAsLastSibling()
    end)
end

WikiSearchCtrl.m_selectedItem = HL.Field(HL.Userdata)

WikiSearchCtrl._SetItemSelected = HL.Method(HL.Userdata, HL.Table) << function(self, itemCell, entryShowData)
    if self.m_selectedItem then
        self.m_selectedItem:SetSelected(false, true)
    end
    if itemCell then
        itemCell:SetSelected(true, true)
        self.m_selectedItem = itemCell
    end
    local entryId = entryShowData.wikiEntryData.id
    if WikiUtils.isWikiEntryUnread(entryId) then
        GameInstance.player.wikiSystem:MarkWikiEntryRead({ entryId })
    end
    self:_RefreshDetails(entryShowData)
    self:_ScrollToSelectedItem(itemCell)
end

WikiSearchCtrl._ScrollToSelectedItem = HL.Method(HL.Userdata) << function(self, itemCell)
    if not itemCell then
        return
    end
    local targetTransform = nil
    if itemCell.view then
        targetTransform = itemCell.view.transform
    end
    if not targetTransform then
        targetTransform = itemCell.transform
    end
    if not targetTransform then
        return
    end
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.scrollContent)
    self.view.scrollView:AutoScrollToRectTransform(targetTransform, true)
end

WikiSearchCtrl.m_curWikiEntryShowData = HL.Field(HL.Table)

WikiSearchCtrl._RefreshDetails = HL.Method(HL.Table) << function(self, wikiEntryShowData)
    self.m_curWikiEntryShowData = wikiEntryShowData
    self.view.wikiItemInfo:InitWikiItemInfo({
        wikiEntryShowData = wikiEntryShowData,
        itemImg = self.view.itemImg,
        wikiGuideMediaCell = self.view.wikiGuideMediaCell,
        hideDetailBtn = true,
    })
end



WikiSearchCtrl.m_historyCache = HL.Field(HL.Forward("UIListCache"))

WikiSearchCtrl._RefreshHistory = HL.Method() << function(self)
    local historyKeywords = WikiUtils.getHistorySearchKeywords()
    local hasHistory = historyKeywords and #historyKeywords > 0
    self.view.historyNode.gameObject:SetActive(hasHistory)
    if not hasHistory then
        return
    end

    if not self.m_historyCache then
        self.m_historyCache = UIUtils.genCellCache(self.view.historyCell)
    end
    self.m_historyCache:Refresh(#historyKeywords, function(cell, luaIndex)
        local keyword = historyKeywords[luaIndex]
        cell.nameTxt.text = keyword
        cell.btn.onClick:RemoveAllListeners()
        cell.btn.onClick:AddListener(function()
            self.view.inputField.text = keyword
            self:_Search(keyword)
        end)
    end)
end





WikiSearchCtrl.m_readWikiEntries = HL.Field(HL.Table)

WikiSearchCtrl._MarkWikiEntryRead = HL.Method() << function(self)
    if self.m_readWikiEntries then
        local entryIdList = {}
        for entryId, _ in pairs(self.m_readWikiEntries) do
            table.insert(entryIdList, entryId)
        end
        GameInstance.player.wikiSystem:MarkWikiEntryRead(entryIdList)
        self.m_readWikiEntries = {}
    end
end






WikiSearchCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.searchNodeNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if isFocused then
            self.view.inputField:ActivateInputField()
            InputManagerInst:ChangeParent(true, self.view.searchBtnInputGroup.groupId, self.view.searchNodeInputGroup.groupId)
        else
            self.view.inputField:DeactivateInputField()
            if self.view.historyNode.gameObject.activeSelf and self.m_historyCache and self.m_historyCache:GetCount() > 0 then
                self:SetNaviTarget(self.m_historyCache:Get(1).btn)
            end
            InputManagerInst:ChangeParent(true, self.view.searchBtnInputGroup.groupId, self.view.inputGroup.groupId)
        end
    end)
end



HL.Commit(WikiSearchCtrl)
