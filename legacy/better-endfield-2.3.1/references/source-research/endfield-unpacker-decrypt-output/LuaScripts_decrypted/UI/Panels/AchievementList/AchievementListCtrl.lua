
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AchievementList

AchievementListCtrl = HL.Class('AchievementListCtrl', uiCtrl.UICtrl)






AchievementListCtrl.s_messages = HL.StaticField(HL.Table) << {

}

AchievementListCtrl.m_filterCellCache = HL.Field(HL.Forward("UIListCache"))

AchievementListCtrl.m_getCategoryCellFunc = HL.Field(HL.Function)

AchievementListCtrl.m_getAchievementCellFunc = HL.Field(HL.Function)

AchievementListCtrl.m_categoryDataSource = HL.Field(HL.Any) << nil

AchievementListCtrl.m_categoryFilteredData = HL.Field(HL.Any) << nil

AchievementListCtrl.m_filteredDataCount = HL.Field(HL.Number) << 0

AchievementListCtrl.m_filteredAchievementMap = HL.Field(HL.Any) << nil

AchievementListCtrl.m_selectCategoryIndex = HL.Field(HL.Number) << 1

AchievementListCtrl.m_selectGroupIndex = HL.Field(HL.Number) << 1

AchievementListCtrl.m_filterType = HL.Field(HL.Number) << 1

AchievementListCtrl.m_searchKey = HL.Field(HL.String) << ''

AchievementListCtrl.m_viewedAchievements = HL.Field(HL.Any) << nil

AchievementListCtrl.m_isFold = HL.Field(HL.Boolean) << false

local ALL_FILTER_TYPE = 1
local OBTAIN_FILTER_TYPE = 2
local NOT_OBTAIN_FILTER_TYPE = 3

local FILTER_CONFIGS = {
    [ALL_FILTER_TYPE] = {
        icon = "achievement_tab_icon01",
        text = "ui_achv_list_all",
        filter = function(achievementInfo)
            return true
        end
    },
    [OBTAIN_FILTER_TYPE] = {
        icon = "achievement_tab_icon02",
        text = "ui_achv_list_obtained",
        filter = function(achievementInfo)
            if achievementInfo == nil or achievementInfo.achievementPlayerInfo == nil then
                return false
            end
            return achievementInfo.achievementPlayerInfo.level > 0
        end
    },
    [NOT_OBTAIN_FILTER_TYPE] = {
        icon = "achievement_tab_icon03",
        text = "ui_achv_list_not_obtained",
        hideRedDot = true,
        filter = function(achievementInfo)
            if achievementInfo == nil or achievementInfo.achievementPlayerInfo == nil then
                return true
            end
            return achievementInfo.achievementPlayerInfo.level <= 0
        end
    },
}

local SHOWING_RED_DOT = 1


AchievementListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local recoverState = self:_ProcessRecoverStateArg(arg)
    self:_InitViews()
    self:_LoadData()
    self:_RecoverState(recoverState)
    self:_RenderViews(true)
    self:_RecoverSearchNode(recoverState)
    self:_TryRecoverCategoryScroll(recoverState)
    self:_TryRecoverDetailPopup(recoverState)
end





AchievementListCtrl.OnClose = HL.Override() << function(self)
    self:_ClearShowedRedDot()
end




AchievementListCtrl.ShowAchievement = HL.StaticMethod(HL.Opt(HL.String)) << function(arg)
    local ctrl = AchievementListCtrl.AutoOpen(PANEL_ID, nil, false)
    if string.isEmpty(arg) then
        logger.error("[Achievement] AchievementListCtrl: Invalid arg: " .. tostring(arg))
        return
    end
    local focus = ctrl:_TryFocusAchievement(arg)
    if not focus then
        logger.error("[Achievement] AchievementListCtrl: Cannot Find Achievement To Focus " .. tostring(arg))
    end
end

AchievementListCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.helpBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_INTRO, "achievement")
    end)

    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.m_filterCellCache = UIUtils.genCellCache(self.view.leftTabs.tabCell)
    self.m_filterCellCache:Refresh(#FILTER_CONFIGS, function(cell, luaIndex)
        local iconPath = UIConst.UI_SPRITE_ACHIEVEMENT
        local iconName = FILTER_CONFIGS[luaIndex].icon
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn and self.m_filterType ~= luaIndex then
                self:_SetFilter(luaIndex)
            end
        end)
        cell.selectedIcon:LoadSprite(iconPath, iconName)
        cell.defaultIcon:LoadSprite(iconPath, iconName)
    end)
    self.m_filterCellCache:GetItem(self.m_filterType).toggle.isOn = true

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
    self:_ResetSearch()

    InputManagerInst:ToggleGroup(self.view.textInputBindingGroup.groupId, true)
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
                id = self.view.textInputBindingGroup.groupId,
                rectTransform = self.view.inputField.transform,
                noHighlight = true,
                hintPlaceholder = self.view.controllerHintPlaceholder,
            })
        end,
        onInputEndEdit = function()
            if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
                return
            end
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.textInputBindingGroup.groupId)
            self.view.inputField:DeactivateInputField(true)
        end,
        onClearClick = function()
            self:_ResetSearch()
        end,
    })

    self.view.leftNaviGroup.onIsTopLayerChanged:RemoveAllListeners()
    self.view.leftNaviGroup.onIsTopLayerChanged:AddListener(function(isTop)
        if isTop then
            self:_RefreshViews()
        end
    end)
    self.view.rightNaviGroup.getDefaultSelectableFunc = function()
        self.view.achievementList:ScrollToIndex(0, true)
        local topCell = self.m_getAchievementCellFunc(1)
        return topCell.button
    end

    self.view.categoryRedDotScrollRect.getRedDotStateAt = function(csIndex)
        if FILTER_CONFIGS[self.m_filterType].hideRedDot then
            return 0
        end
        local categoryInfo = self.m_categoryFilteredData[LuaIndex(csIndex)]
        local suc, categoryData = Tables.achievementTypeTable:TryGetValue(categoryInfo.data.categoryId)
        if not suc then
            return 0
        end
        for _, achievementData in pairs(Tables.achievementTable) do
            if achievementData ~= nil then
                for _, groupData in pairs(categoryData.achievementGroupData) do
                    if achievementData.groupId == groupData.groupId
                        and GameInstance.player.achievementSystem:IsAchievementUnread(achievementData.achieveId) then
                        return UIConst.RED_DOT_TYPE.New
                    end
                end
            end
        end
        return 0
    end

    self.view.contentRedDotScrollRect.getRedDotStateAt = function(csIndex)
        if FILTER_CONFIGS[self.m_filterType].hideRedDot then
            return 0
        end
        local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
        if categoryInfo == nil then
            return 0
        end
        local groupInfo = categoryInfo.filteredGroups[self.m_selectGroupIndex]
        if groupInfo == nil then
            return 0
        end
        local achievementInfo = groupInfo.filteredInfos[LuaIndex(csIndex)]
        if achievementInfo == nil then
            return 0
        end
        local achievementData = achievementInfo.achievementData
        local achievementId = achievementData.achieveId
        if GameInstance.player.achievementSystem:IsAchievementUnread(achievementId) then
            return UIConst.RED_DOT_TYPE.New
        end
        return 0
    end

    self:BindInputPlayerAction("common_horizontal_focus_right", function()
        self.view.rightNaviGroup:ManuallyFocus()
    end)
    self:BindInputPlayerAction("common_horizontal_stop_focus_left", function()
        self.view.rightNaviGroup:ManuallyStopFocus()
    end, self.view.rightListScroll.groupId)
end

AchievementListCtrl._LoadData = HL.Method() << function(self)
    self.m_categoryDataSource = {}

    self.m_categoryDataSource = AchievementUtils.loadAchievementData(true)

    self:_LoadFilteredData()
    self:_ResetSelectIndex()
end

AchievementListCtrl._LoadFilteredData = HL.Method() << function(self)
    self.m_filteredDataCount = 0
    self.m_categoryFilteredData, self.m_filteredAchievementMap = AchievementUtils.filterAchievementData(self.m_categoryDataSource, function(achievementInfo, filteredInfos, showNoObtain)
        return self:_FilterAchievement(achievementInfo, filteredInfos, showNoObtain)
    end)
end

AchievementListCtrl._ProcessRecoverStateArg = HL.Method(HL.Opt(HL.Any)).Return(HL.Opt(HL.Any)) << function(self, arg)
    if type(arg) ~= "table" then
        return nil
    end
    if type(arg.filterType) == "number" and FILTER_CONFIGS[arg.filterType] ~= nil then
        self.m_filterType = arg.filterType
    end
    if type(arg.searchKey) == "string" then
        self.m_searchKey = arg.searchKey
    end
    return arg
end

AchievementListCtrl._RecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local categoryCount = self.m_categoryFilteredData and #self.m_categoryFilteredData or 0
    if categoryCount <= 0 then
        return
    end
    local recoverCategoryIndex = type(recoverState.categoryIndex) == "number" and recoverState.categoryIndex or self.m_selectCategoryIndex
    recoverCategoryIndex = math.max(1, math.min(recoverCategoryIndex, categoryCount))
    self.m_selectCategoryIndex = recoverCategoryIndex
    local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
    if categoryInfo == nil then
        self.m_selectCategoryIndex = 1
        self.m_selectGroupIndex = 1
        return
    end
    local groupCount = categoryInfo.filteredGroups and #categoryInfo.filteredGroups or 0
    if groupCount <= 0 then
        self.m_selectGroupIndex = 1
        return
    end
    local recoverGroupIndex = type(recoverState.groupIndex) == "number" and recoverState.groupIndex or self.m_selectGroupIndex
    self.m_selectGroupIndex = math.max(1, math.min(recoverGroupIndex, groupCount))
end

AchievementListCtrl._FilterAchievement = HL.Method(HL.Any, HL.Any, HL.Boolean).Return(HL.Boolean) << function(self, achievementInfo, filteredInfos, showNoObtain)
    local isObtained = achievementInfo.achievementPlayerInfo ~= nil
        and achievementInfo.achievementPlayerInfo.level >= achievementInfo.achievementData.initLevel
    if not showNoObtain and not isObtained then
        return false
    end
    local displayTimeId = achievementInfo.achievementData.displayTimeId
    if not string.isEmpty(displayTimeId) and not Utils.isCurTimeInTimeIdRange(displayTimeId) and not isObtained then
        return false
    end
    if not FILTER_CONFIGS[self.m_filterType].filter(achievementInfo) then
        return false
    end
    local isSearch = not string.isEmpty(self.m_searchKey)
    local isInclude, replaceName = self:_IsFilteredBySearchKey(achievementInfo.achievementData.name)
    if isSearch and not isInclude then
        return false
    end
    achievementInfo.showName = isSearch and replaceName or achievementInfo.achievementData.name
    table.insert(filteredInfos, achievementInfo)
    self.m_filteredDataCount = self.m_filteredDataCount + 1
    return true
end

AchievementListCtrl._ResetSelectIndex = HL.Method() << function(self)
    self.m_selectCategoryIndex = 1
    self.m_selectGroupIndex = 1
    self.m_isFold = false
end

AchievementListCtrl._RecoverSearchNode = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    self.view.inputField.text = self.m_searchKey
end

AchievementListCtrl._GetDetailPopupRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, ctrl = UIManager:IsOpen(PanelId.AchievementDetailPopup)
    if not isOpen or not ctrl:IsShow() then
        return
    end
    return ctrl:GetRecoverStateArg()
end

AchievementListCtrl._TryRecoverDetailPopup = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil or recoverState.detailPopupArg == nil then
        return
    end
    local isOpen = UIManager:IsOpen(PanelId.AchievementDetailPopup)
    if isOpen then
        return
    end
    UIManager:Open(PanelId.AchievementDetailPopup, recoverState.detailPopupArg)
end

AchievementListCtrl._TryRecoverCategoryScroll = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil or recoverState.categoryIndex == nil then
        return
    end
    self.view.categoryList:ScrollToIndex(CSIndex(self.m_selectCategoryIndex), true)
    self.view.categoryList:UpdateShowingCells(function(csIndex, obj)
        self:_RenderCategory(self.m_getCategoryCellFunc(obj), LuaIndex(csIndex))
    end)
end

AchievementListCtrl._IsFilteredBySearchKey = HL.Method(HL.String).Return(HL.Boolean, HL.String) << function(self, name)
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

AchievementListCtrl._RenderViews = HL.Method(HL.Opt(HL.Boolean, HL.Number)) << function(self, isInit, focusIndex)
    isInit = isInit == true
    local achievementSystem = GameInstance.player.achievementSystem
    self:_ClearShowedRedDot()

    local isSearchMode = not string.isEmpty(self.m_searchKey)
    self.view.filterTxt.text = I18nUtils.GetText(FILTER_CONFIGS[self.m_filterType].text)
    self.view.searchTxt.text = I18nUtils.GetText("ui_achv_list_search_result") .. self.m_filteredDataCount
    self.view.searchTxt.gameObject:SetActive(isSearchMode)

    local filteredDataCount = #self.m_categoryFilteredData
    local needFocus = focusIndex ~= nil

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
    if needFocus then
        self.view.categoryList:ScrollToIndex(CSIndex(self.m_selectCategoryIndex), true)
        self.view.categoryList:UpdateShowingCells(function(csIndex, obj)
            self:_RenderCategory(self.m_getCategoryCellFunc(obj), LuaIndex(csIndex))
        end)
    end

    if self.m_categoryFilteredData and filteredDataCount > 0 then
        local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
        if categoryInfo ~= nil and categoryInfo.haveSub then
            local groupInfos = categoryInfo.filteredGroups
            self.view.stateCtrl:SetState("HaveSub")
            self.view.subTxt.text = groupInfos[self.m_selectGroupIndex].data.groupName
        else
            self.view.stateCtrl:SetState("NoSub")
        end
    else
        self.view.stateCtrl:SetState("SearchNull")
    end
    local achievementCount = 0
    local categoryInfo = self.m_categoryFilteredData[self.m_selectCategoryIndex]
    if categoryInfo ~= nil then
        local groupInfo = categoryInfo.filteredGroups[self.m_selectGroupIndex]
        if groupInfo ~= nil then
            achievementCount = #groupInfo.filteredInfos
        end
    end

    if needFocus then
        self.view.achievementList:UpdateCount(achievementCount, focusIndex, false, false, true)
        if DeviceInfo.usingController then
            self.view.achievementList:UpdateShowingCells(function(csIndex, obj)
                local luaIndex = LuaIndex(csIndex)
                local cell = self.m_getAchievementCellFunc(obj)
                self:_RenderAchievement(cell, luaIndex)
                if luaIndex == focusIndex then
                    self:SetNaviTarget(cell.button)
                end
            end)
        end
    else
        self.view.achievementList:UpdateCount(achievementCount, true)
    end
end

AchievementListCtrl._RefreshViews = HL.Method() << function(self)
    self.view.categoryList:UpdateShowingCells(function(csIndex, obj)
        self:_RenderCategory(self.m_getCategoryCellFunc(obj), LuaIndex(csIndex), true)
    end)
end

AchievementListCtrl._RenderCategory = HL.Method(HL.Any, HL.Number, HL.Opt(HL.Boolean)) << function(self, cell, luaIndex, isRefresh)
    local categoryInfo = self.m_categoryFilteredData[luaIndex]
    if categoryInfo == nil then
        return
    end
    isRefresh = isRefresh == true
    local isSearchMode = not string.isEmpty(self.m_searchKey)
    local hideRedDot = FILTER_CONFIGS[self.m_filterType].hideRedDot == true
    cell:InitAchievementCategoryCell(categoryInfo, luaIndex, {
        selectCategoryIndex = self.m_selectCategoryIndex,
        selectGroupIndex = self.m_selectGroupIndex,
        isSearchMode = isSearchMode,
        needSetNavi = true,
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
            local groupInfo = categoryInfo.filteredGroups[groupIndex]
            if groupInfo == nil then
                return
            end
            groupCell.redDot:InitRedDot("AchievementGroup", groupInfo.data.groupId)
            if isSearchMode or hideRedDot then
                groupCell.redDotHolder.alpha = 0
            else
                groupCell.redDotHolder.alpha = 1
            end
        end,
    })
    cell.view.redDot:InitRedDot("AchievementCategory", categoryInfo.data.categoryId, nil, self.view.categoryRedDotScrollRect)
    if isSearchMode or hideRedDot then
        cell.view.redDotHolder.gameObject:SetActive(false)
    else
        cell.view.redDotHolder.gameObject:SetActive(true)
    end
end

AchievementListCtrl._RenderAchievement = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local achievementSystem = GameInstance.player.achievementSystem
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

    local achievementData = achievementInfo.achievementData
    local achievementId = achievementData.achieveId
    local maxLevel = 1
    for i, levelInfo in pairs(achievementData.levelInfos) do
        maxLevel = math.max(maxLevel, levelInfo.achieveLevel)
    end
    local playerInfo = achievementInfo.achievementPlayerInfo
    local playerLevel = (playerInfo == nil) and 0 or playerInfo.level
    local playerPlated = playerInfo ~= nil and playerInfo.isPlated
    local playerObtainTs = (playerInfo == nil) and -1 or playerInfo.obtainTs
    local timeInfo = achievementInfo.achievementTimeInfo
    local curTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    local timeLimit = timeInfo ~= nil
    local isObtained = playerLevel >= achievementData.initLevel
    local canUpgrade = isObtained and achievementData.canBeUpgraded and playerLevel < maxLevel
    local canPlate = isObtained and achievementData.canBePlated and not playerPlated
    local canRare = achievementData.applyRareEffect
    local isRare = playerLevel >= Tables.achievementConst.levelDisplayEffect
    local haveObtainLevel, obtainLevelInfo = achievementData.levelInfos:TryGetValue(achievementData.initLevel)
    local isSearchMode = not string.isEmpty(self.m_searchKey)
    if not isSearchMode and achievementSystem:IsAchievementUnread(achievementId) then
        self:_CollectShowedRedDot(achievementId)
    end

    cell.name.text = achievementInfo.showName
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if GameInstance.player.achievementSystem:IsAchievementUnread(achievementId) then
            GameInstance.player.achievementSystem:ReadAchievement(achievementId)
        end
        UIManager:Open(PanelId.AchievementDetailPopup, {achievementId = achievementId})
    end)
    local hideRedDot = FILTER_CONFIGS[self.m_filterType].hideRedDot == true
    cell.redDot:InitRedDot("AchievementItem", achievementId, nil, self.view.contentRedDotScrollRect)
    if isSearchMode or hideRedDot then
        cell.redDotHolder.gameObject:SetActive(false)
    else
        cell.redDotHolder.gameObject:SetActive(true)
    end
    if isObtained then
        local medalBundle = {
            achievementId = achievementData.achieveId,
            level = playerLevel,
            isPlated = playerPlated,
            isRare = canRare
        }
        cell.medal:InitMedal(medalBundle)
    end
    cell.stateCtrl:SetState(isObtained and "Acquired" or "Unattained")
    cell.stateCtrl:SetState(canUpgrade and "PromoteReforge" or (canPlate and "PromoteCladding" or "PromoteNull"))
    cell.stateCtrl:SetState(canRare and (isRare and "QualifyPossess" or "QualifyNotPossess") or "QualifyNull")
    if isObtained then
        cell.obtainTimeTxt.text = Utils.timestampToDateYMD(playerObtainTs)
        local currLevelText = ''
        local haveCurrLevelInfo, currLevelInfo = achievementData.levelInfos:TryGetValue(playerLevel)
        if haveCurrLevelInfo then
            currLevelText = currLevelInfo.completeDesc
        end
        cell.descTxt.text = UIUtils.resolveTextCinematic(currLevelText)
    else
        cell.descTxt.text = UIUtils.resolveTextCinematic(achievementData.desc)
    end
    if timeLimit then
        if curTs < timeInfo.openTime then
            cell.stateCtrl:SetState("TimeLimit")
            cell.timeLimitTxt.text = I18nUtils.GetText("ui_achv_list_can_not_obtain")
        else
            if timeInfo.closeTime <= 0 then
                cell.stateCtrl:SetState("TimeUnlimit")
            else
                cell.stateCtrl:SetState("TimeLimit")
                if curTs < timeInfo.closeTime then
                    cell.timeLimitTxt.text = string.format(I18nUtils.GetText("ui_achv_list_obtain_close"), UIUtils.getShortLeftTime(timeInfo.closeTime - curTs))
                else
                    cell.timeLimitTxt.text = I18nUtils.GetText("ui_achv_list_can_not_obtain")
                end
            end
        end
    else
        cell.stateCtrl:SetState("TimeUnlimit")
    end
    local conditionCount = 0
    if haveObtainLevel and obtainLevelInfo ~= nil then
        local condition = obtainLevelInfo.conditions[0]
        conditionCount = #obtainLevelInfo.conditions
        local conditionText = condition.desc
        if not isObtained then
            local conditionProgress, conditionTarget = self:_CalcAchievementCondition(obtainLevelInfo.conditions, playerInfo, achievementData.specialProgress)
            conditionText = conditionText .. string.format(Language.LUA_ACHIEVEMENT_CONDITION_TARGET, conditionProgress, conditionTarget)
        end
        cell.conditionTxt.text = conditionText
    end
    cell.moreCondition.gameObject:SetActive(not isObtained and (achievementData.canBeUpgraded or achievementData.canBePlated))
end

AchievementListCtrl._CalcAchievementCondition = HL.Method(HL.Any, HL.Any, HL.Any).Return(HL.Number, HL.Number) << function(self, conditions, playerInfo, isSpecial)
    local progress = 0
    local target = 0
    if isSpecial then
        return 0, 1
    end
    for _, condition in pairs(conditions) do
        if playerInfo ~= nil and playerInfo.condition ~= nil then
            local suc, playerConditionVal = playerInfo.condition.conditionVals:TryGetValue(condition.conditionId)
            if suc then
                progress = progress + playerConditionVal
            end
        end
        target = target + condition.progressToCompare
    end
    return progress, target
end

AchievementListCtrl._SetFilter = HL.Method(HL.Number) << function(self, filterType)
    self.m_filterType = filterType
    self:_LoadFilteredData()
    self:_ResetSelectIndex()
    self:_RenderViews(true)
end

AchievementListCtrl._SetSearchKey = HL.Method(HL.String) << function(self, searchKey)
    if self.m_searchKey == searchKey then
        return
    end
    self.m_searchKey = searchKey
    self:_LoadFilteredData()
    self:_ResetSelectIndex()
    self:_RenderViews(true)
end

AchievementListCtrl._ResetSearch = HL.Method() << function(self)
    self.view.inputField.text = ''
end

AchievementListCtrl._SetSelectIndex = HL.Method(HL.Number, HL.Number, HL.Opt(HL.Number)) << function(self, categoryIndex, groupIndex, achievementIndex)
    if achievementIndex == nil and categoryIndex == self.m_selectCategoryIndex and groupIndex == self.m_selectGroupIndex then
        return
    end
    local prevCategory = self.m_selectCategoryIndex
    self.m_selectCategoryIndex = categoryIndex
    self.m_selectGroupIndex = groupIndex
    if achievementIndex ~= nil then
        self:_RenderViews(false, achievementIndex)
    else
        self:_RenderViews(false)
    end
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

AchievementListCtrl._TryFocusAchievement = HL.Method(HL.String).Return(HL.Boolean) << function(self, achievementId)
    local needReset = false
    if self.m_filterType ~= ALL_FILTER_TYPE then
        self.m_filterType = ALL_FILTER_TYPE
        needReset = true
    end
    if not string.isEmpty(self.m_searchKey) then
        self:_ResetSearch()
        needReset = true
    end
    if needReset then
        self:_LoadFilteredData()
    end
    local achievementIndexInfo = self.m_filteredAchievementMap[achievementId]
    if achievementIndexInfo == nil then
        return false
    end
    self:_SetSelectIndex(achievementIndexInfo.categoryIndex, achievementIndexInfo.groupIndex, achievementIndexInfo.achievementIndex)
    return true
end

AchievementListCtrl._CollectShowedRedDot = HL.Method(HL.String) << function(self, achievementId)
    if self.m_viewedAchievements == nil then
        self.m_viewedAchievements = {}
    end
    self.m_viewedAchievements[achievementId] = SHOWING_RED_DOT
end

AchievementListCtrl._ClearShowedRedDot = HL.Method() << function(self)
    if self.m_viewedAchievements == nil then
        return
    end
    local achievementSystem = GameInstance.player.achievementSystem
    for id, flag in pairs(self.m_viewedAchievements) do
        if flag == SHOWING_RED_DOT and achievementSystem:IsAchievementUnread(id) then
            achievementSystem:ReadAchievement(id)
        end
    end
    self.m_viewedAchievements = {}
end

AchievementListCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        filterType = self.m_filterType,
        searchKey = self.m_searchKey,
        categoryIndex = self.m_selectCategoryIndex,
        groupIndex = self.m_selectGroupIndex,
        detailPopupArg = self:_GetDetailPopupRecoverState(),
    }
end

HL.Commit(AchievementListCtrl)
