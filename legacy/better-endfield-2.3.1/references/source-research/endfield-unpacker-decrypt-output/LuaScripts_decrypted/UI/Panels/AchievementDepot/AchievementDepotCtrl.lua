
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AchievementDepot

AchievementDepotCtrl = HL.Class('AchievementDepotCtrl', uiCtrl.UICtrl)






AchievementDepotCtrl.s_messages = HL.StaticField(HL.Table) << {

}

AchievementDepotCtrl.m_getCategoryCellFunc = HL.Field(HL.Function)

AchievementDepotCtrl.m_getAchievementCellFunc = HL.Field(HL.Function)

AchievementDepotCtrl.m_filterArgs = HL.Field(HL.Table)

AchievementDepotCtrl.m_categoryDataSource = HL.Field(HL.Any) << nil

AchievementDepotCtrl.m_sourceAchievementMap = HL.Field(HL.Any) << nil

AchievementDepotCtrl.m_categoryFilteredData = HL.Field(HL.Any) << nil

AchievementDepotCtrl.m_filteredDataCount = HL.Field(HL.Number) << 0

AchievementDepotCtrl.m_filteredAchievementMap = HL.Field(HL.Any) << nil

AchievementDepotCtrl.m_selectCategoryIndex = HL.Field(HL.Number) << 1

AchievementDepotCtrl.m_selectGroupIndex = HL.Field(HL.Number) << 1

AchievementDepotCtrl.m_searchKey = HL.Field(HL.String) << ''

AchievementDepotCtrl.m_selectedFilterTags = HL.Field(HL.Table)

AchievementDepotCtrl.m_editSelected = HL.Field(HL.Table)

AchievementDepotCtrl.m_selectCount = HL.Field(HL.Number) << 0

AchievementDepotCtrl.m_categorySelectCountInfo = HL.Field(HL.Table)

AchievementDepotCtrl.m_groupSelectCountInfo = HL.Field(HL.Table)

AchievementDepotCtrl.m_depotLimit = HL.Field(HL.Number) << 0

AchievementDepotCtrl.m_args = HL.Field(HL.Any)

AchievementDepotCtrl.m_isFold = HL.Field(HL.Boolean) << false




AchievementDepotCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local recoverState = args and args.recoverState or nil
    self.m_args = args
    self:_InitViews()
    self:_LoadData(args and args.depot)
    self:_TryRecoverState(recoverState)
    self:_RenderViews(true)
end








AchievementDepotCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.btnBack.onClick:RemoveAllListeners()
    self.view.btnBack.onClick:AddListener(function()
        self:_SaveEditData()
    end)

    self.m_getCategoryCellFunc = UIUtils.genCachedCellFunction(self.view.categoryList)
    self.view.categoryList.onUpdateCell:RemoveAllListeners()
    self.view.categoryList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RenderCategory(self.m_getCategoryCellFunc(obj), LuaIndex(csIndex))
    end)
    self.view.categoryList.getCellSize = function(csIndex)
        local luaIndex = LuaIndex(csIndex)
        local categoryInfo = self.m_categoryFilteredData[luaIndex]
        if categoryInfo == nil then
            return 0
        end
        if categoryInfo.haveSub then
            return self.view.config.CATEGORY_CELL_HEIGHT + self.view.config.CATEGORY_GROUP_CELL_HEIGHT * #categoryInfo.filteredGroups
        end
        return self.view.config.CATEGORY_CELL_HEIGHT
    end

    self.m_getAchievementCellFunc = UIUtils.genCachedCellFunction(self.view.achievementList)
    self.view.achievementList.onUpdateCell:RemoveAllListeners()
    self.view.achievementList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RenderAchievement(self.m_getAchievementCellFunc(obj), LuaIndex(csIndex))
    end)

    self.view.clearBtn.gameObject:SetActive(false)
    self.view.inputField.text = ''

    UIUtils.initSearchInput(self.view.inputField, {
        clearBtn = self.view.clearBtn,
        onInputValueChanged = function(input)
            local trimedInput = string.trim(input)
            if trimedInput ~= self.m_searchKey then
                self:_SetSearchKey(trimedInput)
            end
        end,
        onInputFocused = function()
            if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
                return
            end
            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = PANEL_ID,
                isGroup = true,
                id = self.view.inputFieldInputBindingGroupMonoTarget.groupId,
                rectTransform = self.view.inputField.transform,
                noHighlight = true,
                hintPlaceholder = self.view.controllerHintPlaceholder,
            })
        end,
        onInputEndEdit = function()
            if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
                return
            end
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputFieldInputBindingGroupMonoTarget.groupId)
            self.view.inputField:DeactivateInputField(true)
        end,
        onClearClick = function()
            self.view.inputField.text = ''
        end,
    })

    self.m_selectedFilterTags = {}
    self.m_filterArgs = self:_GenFilterArgs()
    self.view.btnCommonFilterNew.button.onClick:RemoveAllListeners()
    self.view.btnCommonFilterNew.button.onClick:AddListener(function()
        self:Notify(MessageConst.SHOW_COMMON_FILTER, self.m_filterArgs)
    end)

    self.view.resetBtn.onClick:RemoveAllListeners()
    self.view.resetBtn.onClick:AddListener(function()
        self:_ResetDepot()
    end)

    self.view.saveBtn.onClick:RemoveAllListeners()
    self.view.saveBtn.onClick:AddListener(function()
        self:_SaveEditData()
    end)

    self.view.rightListScroll.getDefaultSelectableFunc = function()
        self.view.achievementList:ScrollToIndex(0, true)
        local firstCell = self.m_getAchievementCellFunc(1)
        return firstCell.button
    end

    self.view.focusHelperRight.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.view.rightListScroll:ManuallyStopFocus()
        end
    end
    self.view.focusHelperLeft.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.view.rightListScroll:ManuallyFocus()
        end
    end
    self.view.rightListScroll.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            local categoryCell = self.m_getCategoryCellFunc(self.m_selectCategoryIndex)
            local naviTarget = categoryCell.view.button
            local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
            if categoryInfo.haveSub then
                naviTarget = categoryCell.m_cacheCell:Get(self.m_selectGroupIndex).button
            end
            self:SetNaviTarget(naviTarget)
        end
    end)
end

AchievementDepotCtrl._LoadData = HL.Method(HL.Any) << function(self, currDepot)
    self.m_categoryDataSource = {}
    self.m_editSelected = {}
    self.m_sourceAchievementMap = {}
    self.m_selectCount = 0

    self.m_categoryDataSource, self.m_sourceAchievementMap = AchievementUtils.loadAchievementData()

    if currDepot ~= nil then
        self:_LoadDepot(currDepot)
    end

    self.m_depotLimit = Tables.achievementConst.maxDisplayDepotCount

    self:_LoadFilteredData()
    self:_ResetSelectIndex()
end

AchievementDepotCtrl._LoadDepot = HL.Method(HL.Any) << function(self, currDepot)
    for _, achievementId in pairs(currDepot) do
        if self.m_editSelected[achievementId] == nil then
            self.m_editSelected[achievementId] = true
            self.m_selectCount = self.m_selectCount + 1
        end
    end
end

AchievementDepotCtrl._UpdateEditSelectCountInfo = HL.Method() << function(self)
    self.m_groupSelectCountInfo = {}
    self.m_categorySelectCountInfo = {}
    if self.m_filteredAchievementMap == nil then
        return
    end
    for achievementId, flag in pairs(self.m_editSelected) do
        if flag == nil or flag ~= true then
            goto continue
        end

        local achievementIndexInfo = self.m_filteredAchievementMap[achievementId]
        if achievementIndexInfo == nil then
            goto continue
        end

        local categoryIndex = achievementIndexInfo.categoryIndex
        if categoryIndex ~= nil then
            if self.m_categorySelectCountInfo[categoryIndex] == nil then
                self.m_categorySelectCountInfo[categoryIndex] = 0
            end
            self.m_categorySelectCountInfo[categoryIndex] = self.m_categorySelectCountInfo[categoryIndex] + 1
            if self.m_groupSelectCountInfo[categoryIndex] == nil then
                self.m_groupSelectCountInfo[categoryIndex] = {}
            end
            local groupIndex = achievementIndexInfo.groupIndex
            if groupIndex ~= nil then
                if self.m_groupSelectCountInfo[categoryIndex][groupIndex] == nil then
                    self.m_groupSelectCountInfo[categoryIndex][groupIndex] = 0
                end
                self.m_groupSelectCountInfo[categoryIndex][groupIndex] = self.m_groupSelectCountInfo[categoryIndex][groupIndex] + 1
            end
        end
        ::continue::
    end
end

AchievementDepotCtrl._LoadFilteredData = HL.Method() << function(self)
    self.m_filteredDataCount = 0
    self.m_categoryFilteredData, self.m_filteredAchievementMap = AchievementUtils.filterAchievementData(self.m_categoryDataSource, function(achievementInfo, filteredInfos, showNoObtain)
        return self:_FilterAchievement(achievementInfo, filteredInfos, showNoObtain)
    end)
    self:_UpdateEditSelectCountInfo()
end

AchievementDepotCtrl._ResetEditSelect = HL.Method() << function(self)
    self.m_editSelected = {}
    self.m_selectCount = 0
end

AchievementDepotCtrl._ResetSelectIndex = HL.Method() << function(self)
    self.m_selectCategoryIndex = 1
    self.m_selectGroupIndex = 1
end

AchievementDepotCtrl._FilterAchievement = HL.Method(HL.Any, HL.Any, HL.Boolean).Return(HL.Boolean) << function(self, achievementInfo, filteredInfos, showNoObtain)
    local isObtained = achievementInfo.achievementPlayerInfo ~= nil
        and achievementInfo.achievementPlayerInfo.level >= achievementInfo.achievementData.initLevel
    if not showNoObtain and not isObtained then
        return false
    end
    local isSearch = not string.isEmpty(self.m_searchKey)
    if isSearch then
        local isInclude, repName = self:_IsFilteredBySearchKey(achievementInfo.achievementData.name)
        if not isInclude then
            return false
        end
        achievementInfo.repName = repName
    else
        if self.m_selectedFilterTags and next(self.m_selectedFilterTags) then
            if not FilterUtils.checkIfPassFilter(achievementInfo, self.m_selectedFilterTags) then
                return false
            end
        end
    end
    table.insert(filteredInfos, achievementInfo)
    self.m_filteredDataCount = self.m_filteredDataCount + 1
    return true
end

AchievementDepotCtrl._IsFilteredBySearchKey = HL.Method(HL.String).Return(HL.Boolean, HL.String) << function(self, name)
    if string.isEmpty(self.m_searchKey) then
        return true, name
    end
    if string.isEmpty(name) then
        return false, name
    end
    local key = self.m_searchKey
    local rep = string.format(Language.LUA_ACHIEVEMENT_NAME_SEARCH_REPLACE, key)
    rep = rep:gsub("%%", "%%%%")
    local pattern = key:gsub("([%(%)%.%%%+%-%*%?%[%]%^%$])", "%%%1")
    local nameStr, repCount = string.gsub(name, pattern, rep)
    if repCount > 0 then
        return true, nameStr
    end
    return false, name
end

AchievementDepotCtrl._RenderViews = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local isSearchMode = not string.isEmpty(self.m_searchKey)
    if isSearchMode then
        self.view.searchTxt.text = I18nUtils.GetText("ui_achv_list_search_result") .. self.m_filteredDataCount
    end

    local filteredDataCount = #self.m_categoryFilteredData
    isInit = isInit == true

    self.m_isFold = false
    self.view.categoryList:UpdateCount(filteredDataCount, isInit, true)
    if filteredDataCount ~= 0 then
        if DeviceInfo.usingController then
            if isInit then
                self.view.categoryList:FoldAll(true)
            end
        else
            self.view.categoryList:FoldAll(false)
            self.view.categoryList:ToggleByState(CSIndex(self.m_selectCategoryIndex), true, true)
        end
    end

    local hasFilter = self.m_selectedFilterTags ~= nil and #self.m_selectedFilterTags > 0
    self.view.btnCommonFilterNew.normalNode.gameObject:SetActiveIfNecessary(not hasFilter)
    self.view.btnCommonFilterNew.existNode.gameObject:SetActiveIfNecessary(hasFilter)

    local state = "Normal"
    if isSearchMode and filteredDataCount > 0 then
        state = "Searching"
    elseif isSearchMode and filteredDataCount <= 0 then
        state = "SearchNull"
    elseif not isSearchMode and filteredDataCount <= 0 then
        state = "FiltrateNull"
    end
    self.view.stateCtrl:SetState(state)

    local achievementCount = 0
    local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
    if categoryInfo ~= nil then
        local groupInfo = categoryInfo.filteredGroups[self.m_selectGroupIndex]
        if groupInfo ~= nil then
            achievementCount = #groupInfo.filteredInfos
        end
    end
    self.view.achievementList:UpdateCount(achievementCount, true)

    self.view.selectTxt.text = string.format(Language.LUA_ACHIEVEMENT_DEPOT_SELECT_TEXT_FORMAT, self.m_selectCount)
end

AchievementDepotCtrl._RefreshViews = HL.Method() << function(self)
    self:_RefreshCategoryView()
    self.view.achievementList:UpdateShowingCells(function(csIndex, obj)
        self:_RenderAchievement(self.m_getAchievementCellFunc(obj), LuaIndex(csIndex))
    end)
    self.view.selectTxt.text = string.format(Language.LUA_ACHIEVEMENT_DEPOT_SELECT_TEXT_FORMAT, self.m_selectCount, self.m_depotLimit)
end

AchievementDepotCtrl._RefreshCategoryView = HL.Method() << function(self)
    self.view.categoryList:UpdateShowingCells(function(csIndex, obj)
        self:_RenderCategory(self.m_getCategoryCellFunc(obj), LuaIndex(csIndex), true)
    end)
end

AchievementDepotCtrl._RenderCategory = HL.Method(HL.Any, HL.Number, HL.Opt(HL.Boolean)) << function(self, cell, luaIndex, isRefresh)
    local categoryInfo = self.m_categoryFilteredData[luaIndex]
    if categoryInfo == nil then
        return
    end
    isRefresh = isRefresh == true
    local selected = luaIndex == self.m_selectCategoryIndex
    local isSearchMode = not string.isEmpty(self.m_searchKey)
    local haveSub = categoryInfo.haveSub
    local needExpand = (selected and haveSub) or DeviceInfo.usingController
    local count = self.m_categorySelectCountInfo[luaIndex]
    local showCount = not (haveSub and needExpand) and count ~= nil and count > 0
    cell:InitAchievementCategoryCell(categoryInfo, luaIndex, {
        selectCategoryIndex = self.m_selectCategoryIndex,
        selectGroupIndex = self.m_selectGroupIndex,
        isSearchMode = isSearchMode,
        needSetNavi = not isRefresh,
        isFold = self.m_isFold,
        onCategoryClick = function(categoryIndex)
            local selected = categoryIndex == self.m_selectCategoryIndex
            if not DeviceInfo.usingController and categoryInfo.haveSub then
                AudioAdapter.PostEvent((self.m_isFold or not selected) and "Au_UI_Toggle_AchieveDropDown_On" or "Au_UI_Toggle_AchieveDropDown_Off")
            else
                AudioAdapter.PostEvent("Au_UI_Toggle_Common_On")
            end
            if self.m_selectCategoryIndex == categoryIndex and not DeviceInfo.usingController then
                self.view.categoryList:ToggleByState(CSIndex(categoryIndex), self.m_isFold)
                self.m_isFold = not self.m_isFold
                cell:UpdateArrow(self.m_isFold)
                return
            end
            self:_SetSelectIndex(categoryIndex, 1)
        end ,
        onGroupClick = function(categoryIndex, groupIndex)
            if categoryIndex == self.m_selectCategoryIndex and groupIndex == self.m_selectGroupIndex then
                return
            end
            AudioAdapter.PostEvent("Au_UI_Toggle_Common_On")
            self:_SetSelectIndex(categoryIndex, groupIndex)
        end ,
        onGroupCellRender = function(groupCell, groupIndex)
            local count = 0
            if self.m_groupSelectCountInfo[luaIndex] ~= nil and self.m_groupSelectCountInfo[luaIndex][groupIndex] ~= nil then
                count = self.m_groupSelectCountInfo[luaIndex][groupIndex]
            end
            groupCell.countNode.gameObject:SetActive(count > 0)
            if count > 0 then
                groupCell.countTxt.text = count
            end

            self.view.categoryList:ToggleByState(CSIndex(self.m_selectCategoryIndex), true)
            self.m_isFold = false
            cell:UpdateArrow(self.m_isFold)
        end,
    })
    cell.view.countNode.gameObject:SetActive(showCount)
    if showCount then
        cell.view.countTxt.text = count
    end
end

AchievementDepotCtrl._RenderAchievement = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
    if categoryInfo == nil then
        return
    end
    local groupInfo = categoryInfo.filteredGroups[self.m_selectGroupIndex]
    if groupInfo == nil then
        return
    end
    local achievementInfo = groupInfo.filteredInfos[luaIndex]
    if achievementInfo == nil then
        return
    end

    local isSearchMode = not string.isEmpty(self.m_searchKey)
    local achievementData = achievementInfo.achievementData
    local achievementId = achievementData.achieveId
    local isSelected = self.m_editSelected[achievementId] ~= nil and self.m_editSelected[achievementId] == true
    local playerInfo = achievementInfo.achievementPlayerInfo
    local playerLevel = (playerInfo == nil) and 0 or playerInfo.level
    local playerPlated = playerInfo ~= nil and playerInfo.isPlated
    local canRare = achievementData.applyRareEffect
    local medalBundle = {
        achievementId = achievementId,
        level = playerLevel,
        isPlated = playerPlated,
        isRare = canRare
    }

    cell.gameObject.name = achievementId
    if isSearchMode then
        cell.name.text = achievementInfo.repName
    else
        cell.name.text = achievementData.name
    end
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnAchievementSelect(achievementId)
    end)
    cell.medal:InitMedal(medalBundle)
    cell.stateCtrl:SetState(isSelected and "Select" or "Normal")
end

AchievementDepotCtrl._OnAchievementSelect = HL.Method(HL.String) << function(self, achievementId)
    local isSelected = self.m_editSelected[achievementId] ~= nil and self.m_editSelected[achievementId] == true
    if isSelected then
        self.m_editSelected[achievementId] = nil
        self.m_selectCount = self.m_selectCount - 1
    elseif self.m_selectCount < self.m_depotLimit then
        self.m_editSelected[achievementId] = true
        self.m_selectCount = self.m_selectCount + 1
    else
        Notify(MessageConst.SHOW_TOAST, I18nUtils.GetText("ui_achv_edit_add_choose_limit"))
        return
    end
    self:_UpdateEditSelectCountInfo()
    self:_RefreshViews()
end

AchievementDepotCtrl._SetSearchKey = HL.Method(HL.String) << function(self, searchKey)
    if self.m_searchKey == searchKey then
        return
    end
    self.m_searchKey = searchKey
    self:_LoadFilteredData()
    self:_ResetSelectIndex()
    self:_RenderViews(true)
end

AchievementDepotCtrl._SetSelectIndex = HL.Method(HL.Number, HL.Number) << function(self, categoryIndex, groupIndex)
    if categoryIndex == self.m_selectCategoryIndex and groupIndex == self.m_selectGroupIndex then
        return
    end
    local prevCategory = self.m_selectCategoryIndex
    self.m_selectCategoryIndex = categoryIndex
    self.m_selectGroupIndex = groupIndex
    self:_RenderViews()
    if prevCategory ~= self.m_selectCategoryIndex and not DeviceInfo.usingController then
        if not self.m_isFold then
            self.view.categoryList:ToggleByState(CSIndex(prevCategory), false, true)
            self.m_isFold = true
        end
        local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
        if categoryInfo.haveSub then
            self.view.categoryList:ToggleByState(CSIndex(self.m_selectCategoryIndex), true)
            self.m_isFold = false
        end
    end
end

AchievementDepotCtrl._GenFilterArgs = HL.Method().Return(HL.Any) << function(self)
    return {
        tagGroups = FilterUtils.generateConfig_ACHIEVEMENT_MEDAL(),
        selectedTags = self.m_selectedFilterTags,
        onConfirm = function(tags)
            self.m_filterArgs.selectedTags = tags
            self.m_selectedFilterTags = tags
            self:_LoadFilteredData()
            self:_ResetSelectIndex()
            self:_RenderViews(true)
        end,
        getResultCount = function(tags)
            return self:_GetFilteredCount(tags)
        end,
    }
end

AchievementDepotCtrl._GetFilteredCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    if not tags or not next(tags) then
        return
    end
    local count = 0
    for _, achievementInfo in pairs(self.m_sourceAchievementMap) do
        if achievementInfo ~= nil and FilterUtils.checkIfPassFilter(achievementInfo, tags) then
            count = count + 1
        end
    end
    return count
end

AchievementDepotCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    return {
        selectedAchievementIds = self:_GetSelectedAchievementRecoverState(),
        selectCategoryIndex = self.m_selectCategoryIndex,
        selectGroupIndex = self.m_selectGroupIndex,
        searchKey = self.m_searchKey,
        selectedFilterTags = lume.deepCopy(self.m_selectedFilterTags),
    }
end

AchievementDepotCtrl._GetSelectedAchievementRecoverState = HL.Method().Return(HL.Table) << function(self)
    local selectedAchievementIds = {}
    for achievementId, flag in pairs(self.m_editSelected) do
        if flag == true then
            table.insert(selectedAchievementIds, achievementId)
        end
    end
    return selectedAchievementIds
end

AchievementDepotCtrl._TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    self:_TryRecoverEditSelected(recoverState.selectedAchievementIds)
    self:_TryRecoverFilterState(recoverState)
    self:_TryRecoverSelectIndex(recoverState)
end

AchievementDepotCtrl._TryRecoverEditSelected = HL.Method(HL.Opt(HL.Any)) << function(self, selectedAchievementIds)
    if selectedAchievementIds == nil then
        return
    end
    self.m_editSelected = {}
    self.m_selectCount = 0
    for _, achievementId in ipairs(selectedAchievementIds) do
        if not string.isEmpty(achievementId) and self.m_editSelected[achievementId] == nil then
            self.m_editSelected[achievementId] = true
            self.m_selectCount = self.m_selectCount + 1
        end
    end
    self:_UpdateEditSelectCountInfo()
end

AchievementDepotCtrl._TryRecoverFilterState = HL.Method(HL.Any) << function(self, recoverState)
    self.m_searchKey = recoverState.searchKey or ''
    self.view.inputField.text = self.m_searchKey
    self.m_selectedFilterTags = lume.deepCopy(recoverState.selectedFilterTags or {})
    self.m_filterArgs.selectedTags = self.m_selectedFilterTags
    self:_LoadFilteredData()
end

AchievementDepotCtrl._TryRecoverSelectIndex = HL.Method(HL.Any) << function(self, recoverState)
    local categoryCount = #self.m_categoryFilteredData
    if categoryCount <= 0 then
        self:_ResetSelectIndex()
        return
    end
    self.m_selectCategoryIndex = math.min(math.max(recoverState.selectCategoryIndex or 1, 1), categoryCount)
    local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
    local groupCount = categoryInfo ~= nil and #categoryInfo.filteredGroups or 0
    if groupCount <= 0 then
        self.m_selectGroupIndex = 1
        return
    end
    self.m_selectGroupIndex = math.min(math.max(recoverState.selectGroupIndex or 1, 1), groupCount)
end

AchievementDepotCtrl._ResetDepot = HL.Method() << function(self)
    Notify(MessageConst.SHOW_POP_UP, {
        content = I18nUtils.GetText("ui_achv_edit_reset_choose_confirm"),
        onConfirm = function()
            self:_ResetEditSelect()
            self:_UpdateEditSelectCountInfo()
            self:_RefreshViews()
        end,
    })
end

AchievementDepotCtrl._SaveEditData = HL.Method() << function(self)
    if self.m_args ~= nil and self.m_args.onConfirm ~= nil then
        self.m_args.onConfirm(self.m_editSelected)
    end
    self:PlayAnimationOutAndClose()
end

HL.Commit(AchievementDepotCtrl)
