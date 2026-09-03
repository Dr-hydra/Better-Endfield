local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SpaceshipRoomFormulaList = HL.Class('SpaceshipRoomFormulaList', UIWidgetBase)

SpaceshipRoomFormulaList.m_tabCellCache = HL.Field(HL.Forward("UIListCache"))

SpaceshipRoomFormulaList.m_curTabIndex = HL.Field(HL.Number) << -1

SpaceshipRoomFormulaList.m_formulaTabInfos = HL.Field(HL.Table)

SpaceshipRoomFormulaList.m_sortOptions = HL.Field(HL.Table)

SpaceshipRoomFormulaList.m_onUpdateCell = HL.Field(HL.Function)

SpaceshipRoomFormulaList.m_curFormulaList = HL.Field(HL.Table)

SpaceshipRoomFormulaList.m_getFormulaCellFunc = HL.Field(HL.Function)

SpaceshipRoomFormulaList.m_onRefreshFormulaListFunc = HL.Field(HL.Function)

SpaceshipRoomFormulaList.m_onClickTabFunc = HL.Field(HL.Function)

SpaceshipRoomFormulaList.m_curSelectId = HL.Field(HL.String) << ""

SpaceshipRoomFormulaList.m_curSelectCell = HL.Field(HL.Any)

SpaceshipRoomFormulaList.m_onCellClick = HL.Field(HL.Function)

SpaceshipRoomFormulaList.m_onSortChanged = HL.Field(HL.Function)

SpaceshipRoomFormulaList.m_setDefaultInfo = HL.Field(HL.Table)

SpaceshipRoomFormulaList.m_isIniting = HL.Field(HL.Boolean) << false



SpaceshipRoomFormulaList._OnFirstTimeInit = HL.Override() << function(self)
    self.m_tabCellCache = UIUtils.genCellCache(self.view.tabs.tabCell)
    self.m_getFormulaCellFunc = UIUtils.genCachedCellFunction(self.view.formulaScrollList)
    self.view.formulaScrollListSelectableNaviGroup.getDefaultSelectableFunc = function()
        local targetCell = self.m_curSelectCell
        if targetCell and targetCell.view and targetCell.view.button then
            return targetCell.view.button
        end
        local firstCell = self.m_getFormulaCellFunc(1)
        return firstCell and firstCell.view and firstCell.view.button or nil
    end

    self.view.formulaScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_OnFormulaCellUpdate(gameObject, csIndex)
    end)

    self.view.formulaScrollList.onGraduallyShowFinish:RemoveAllListeners()
    self.view.formulaScrollList.onGraduallyShowFinish:AddListener(function()
        if self.m_setDefaultInfo then
            local id = self.m_setDefaultInfo.id
            self.m_setDefaultInfo = nil
            if id ~= nil then
                self:_SetDefaultSelect(id)
            end
        end
    end)
end



































SpaceshipRoomFormulaList.InitSpaceshipRoomFormulaList = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    local defaultTabIndex = args.defaultTabIndex
    if defaultTabIndex and defaultTabIndex >= 1 and defaultTabIndex <= #args.formulas then
        self.m_curTabIndex = defaultTabIndex
    else
        self.m_curTabIndex = 1
    end

    self.m_formulaTabInfos = args.formulas
    self.m_sortOptions = args.sortOptions
    self.m_onCellClick = args.onCellClick
    self.m_onSortChanged = args.onSortChanged
    self.m_tabCellCache:Refresh(#self.m_formulaTabInfos, function(cell, luaIndex)
        self:_OnFormulaTabCellUpdate(cell, luaIndex)
    end)
    self:_OnClickTab(self.m_curTabIndex, true)

    self.m_isIniting = true
    self.view.sortNode:InitSortNode(self.m_sortOptions, function(optionData, isIncremental)
        self:_OnSortChanged(optionData, isIncremental)
        self:RefreshFormulaList()
    end, args.selectedSortOptionCsIndex, args.isIncremental)
    self.view.sortNode:SortCurData()
    self.m_isIniting = false

    local defaultId = args.defaultSelectFormulaId or ''
    local isRecovery = not string.isEmpty(defaultId)
    self:RefreshFormulaList(isRecovery, isRecovery and defaultId or nil)

    if not isRecovery then
        self:_SetDefaultSelect('', true)
    end

    if isRecovery and self.m_curSelectCell and self.m_curSelectCell.view and self.m_curSelectCell.view.button then
        self:SetNaviTarget(self.m_curSelectCell.view.button)
    else
        self.view.formulaScrollListSelectableNaviGroup:NaviToThisGroup()
    end
end

SpaceshipRoomFormulaList._SetDefaultSelect = HL.Method(HL.Opt(HL.String, HL.Boolean)) << function(self, defaultSelectFormulaId, force)
    if self.m_setDefaultInfo and self.m_setDefaultInfo.needWaitGraduallyShow and not force then
        self.m_setDefaultInfo.id = defaultSelectFormulaId or ''
        return
    end
    if defaultSelectFormulaId == '' then
        self.view.formulaScrollList:SetTop()
        local cell = self.m_getFormulaCellFunc(1)
        local info = self.m_curFormulaList[1]
        self:_OnClickCell(cell, info)
    else
        for luaIndex, info in ipairs(self.m_curFormulaList) do
            if info.formulaId == defaultSelectFormulaId then
                self.m_setDefaultInfo = {}
                self.m_setDefaultInfo.needWaitGraduallyShow = false
                self.m_setDefaultInfo.id = defaultSelectFormulaId
                self.view.formulaScrollList:ScrollToIndex(CSIndex(luaIndex), true)
                self.m_setDefaultInfo = nil
                local cell = self.m_getFormulaCellFunc(luaIndex)
                self:_OnClickCell(cell, info)
                break
            end
        end
    end
end

SpaceshipRoomFormulaList._OnFormulaTabCellUpdate = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local tabInfo = self.m_formulaTabInfos[luaIndex]

    cell.gameObject.name = "SpaceshipRoomFormulaTabCell_" .. luaIndex
    cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_WORKSHOP_CRAFT_TYPE_ICON, tabInfo.iconName)
    cell.toggle.isOn = luaIndex == self.m_curTabIndex
    cell.selected.gameObject:SetActiveIfNecessary(luaIndex == self.m_curTabIndex)
    cell.toggle.onValueChanged:AddListener(function(isOn)
        cell.selected.gameObject:SetActiveIfNecessary(isOn)
        if isOn then
            self:_OnClickTab(luaIndex, false)
        end
    end)
end

SpaceshipRoomFormulaList._OnFormulaCellUpdate = HL.Method(HL.Any, HL.Number) << function(self, gameObject, csIndex)
    local luaIndex = LuaIndex(csIndex)
    local cell = self.m_getFormulaCellFunc(gameObject)
    local info = self.m_curFormulaList[luaIndex]

    local selected = self.m_curSelectId == info.formulaId
    if selected then
        self.m_curSelectCell = cell
    end
    cell:InitSSFormulaCell(info, function()
        self:_OnClickCell(cell, info)
    end)
    cell:SetSelected(selected, true)
end

SpaceshipRoomFormulaList._OnClickCell = HL.Method(HL.Forward("SSFormulaCell"), HL.Table) << function(self, cell, info)
    if self.m_curSelectId == info.formulaId then
        return
    end

    if self.m_curSelectCell then
        self.m_curSelectCell:SetSelected(false, false)
    end
    self.m_curSelectCell = cell
    self.m_curSelectId = info.formulaId
    if cell then
        cell:SetSelected(true, false)
    end

    if self.m_onCellClick then
        self.m_onCellClick(info)
    end
end

SpaceshipRoomFormulaList._OnClickTab = HL.Method(HL.Number, HL.Boolean) << function(self, luaIndex, isInit)
    self.m_curTabIndex = luaIndex

    local formulaTabInfo = self.m_formulaTabInfos[luaIndex]
    self.m_curFormulaList = formulaTabInfo.list
    self.view.tabTitleTxt.text = formulaTabInfo.tabName

    if not isInit then
        if self.m_onClickTabFunc then
            self.m_onClickTabFunc()
        end
        self:RefreshFormulaList()
    end
end

SpaceshipRoomFormulaList.RefreshFormulaList = HL.Method(HL.Opt(HL.Boolean, HL.String)) << function(self, skipGraduallyShow, defaultSelectFormulaId)
    if self.m_isIniting then
        return
    end
    if skipGraduallyShow and not string.isEmpty(defaultSelectFormulaId) then
        if self.m_curSelectCell then
            self.m_curSelectCell:SetSelected(false, false)
        end
        self.m_curSelectId = defaultSelectFormulaId
        self.m_curSelectCell = nil
        self.m_setDefaultInfo = nil
        local fastScrollToIndex = 0
        local targetInfo = nil
        for luaIndex, info in ipairs(self.m_curFormulaList) do
            if info.formulaId == defaultSelectFormulaId then
                fastScrollToIndex = CSIndex(luaIndex)
                targetInfo = info
                break
            end
        end
        self.view.formulaScrollList:UpdateCount(#self.m_curFormulaList, fastScrollToIndex, false, false, true)
        if targetInfo and self.m_onCellClick then
            self.m_onCellClick(targetInfo)
        end
    else
        self.view.formulaScrollList:UpdateCount(#self.m_curFormulaList)
        self.m_setDefaultInfo = {}
        self.m_setDefaultInfo.needWaitGraduallyShow = true
        self:_SetDefaultSelect('', false)
    end
end

SpaceshipRoomFormulaList._OnSortChanged = HL.Method(HL.Table, HL.Boolean) << function(self, optionData, isIncremental)
    self:_SortData(optionData.keys, isIncremental)

    self.m_curFormulaList = self.m_formulaTabInfos[self.m_curTabIndex].list

    if self.m_onSortChanged then
        local curSelectedOptionsCsIndex = CSIndex(self.view.sortNode:GetCurSelectedIndex())
        self.m_onSortChanged(curSelectedOptionsCsIndex, isIncremental)
    end
end

SpaceshipRoomFormulaList._SortData = HL.Method(HL.Table, HL.Boolean) << function(self, keys, isIncremental)

    local needSortByUnlock = true
    for _, v in ipairs(self.m_formulaTabInfos) do
        if needSortByUnlock ~= nil then

            local unlockedFormula = {}
            local lockedFormula = {}
            for _, formula in ipairs(v.list) do
                if formula.isUnlock then
                    table.insert(unlockedFormula, formula)
                else
                    table.insert(lockedFormula, formula)
                end
            end
            local sortFunc = Utils.genSortFunction(keys, isIncremental)
            table.sort(unlockedFormula, sortFunc)
            table.sort(lockedFormula, sortFunc)
            v.list = lume.concat(unlockedFormula, lockedFormula)
        else
            table.sort(v.list, Utils.genSortFunction(keys, isIncremental))
        end


        local workingFormula
        local workingFormulaIndex
        for index, formula in ipairs(v.list) do
            if formula.isWorkingSortId == 0 then
                workingFormulaIndex = index
                workingFormula = formula
            end
        end

        if workingFormula then
            table.remove(v.list, workingFormulaIndex)
            table.insert(v.list, 1, workingFormula)
        end
    end

end

SpaceshipRoomFormulaList.UpdateFormulaTabInfos = HL.Method(HL.Table) << function(self, newFormulaTabInfos)
    self.m_formulaTabInfos = newFormulaTabInfos
end


HL.Commit(SpaceshipRoomFormulaList)
return SpaceshipRoomFormulaList
