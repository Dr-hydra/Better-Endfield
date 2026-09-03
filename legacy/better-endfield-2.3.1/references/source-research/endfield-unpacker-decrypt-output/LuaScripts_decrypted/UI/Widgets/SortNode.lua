local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SortNode = HL.Class('SortNode', UIWidgetBase)

SortNode.isIncremental = HL.Field(HL.Boolean) << false

SortNode.m_tmpNoCallback = HL.Field(HL.Boolean) << false

SortNode.m_sortOptions = HL.Field(HL.Table)

SortNode.m_onSortChanged = HL.Field(HL.Function)

SortNode.m_curCSOptionIndex = HL.Field(HL.Number) << -1

SortNode.m_filterBtn = HL.Field(HL.Userdata)

SortNode.m_onToggleOptList = HL.Field(HL.Function)

SortNode.m_changeIncrementalBindingId = HL.Field(HL.Number) << -1

SortNode.m_enableFilterBtn = HL.Field(HL.Boolean) << true



SortNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.mobilePCNode.isIncrementalButton.onClick:AddListener(function()
        self:_ToggleIncremental()
    end)

    self.view.mobilePCNode.dropDown:Init(function(index, option, isSelected)
        local sortOption = self.m_sortOptions[LuaIndex(index)]
        if sortOption then
            option:SetText(self.m_sortOptions[LuaIndex(index)].name)
        end
    end, function(index)
        if not self.m_tmpNoCallback then
            self:OnSortChanged()
        end
    end)
    self.view.mobilePCNode.dropDown.onToggleOptList:AddListener(function(active)
        self:_OnToggleOptList(active)
    end)
end

SortNode._ToggleIncremental = HL.Method() << function(self)
    self.isIncremental = not self.isIncremental
    self:RefreshIncremental()
    self:OnSortChanged()
end

SortNode._OnToggleOptList = HL.Method(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingController then
        return
    end






































end

SortNode.InitSortNode = HL.Method(HL.Table, HL.Function, HL.Opt(HL.Number, HL.Boolean, HL.Boolean, HL.Userdata)) <<
function(self, sortOptions, onSortChanged, curCSOptionIndex, curIsIncremental, noCallback, filterBtn)
    self:_FirstTimeInit()
    if curIsIncremental == nil then
        self.isIncremental = self.config.DEFAULT_IS_INCREMENTAL
    else
        self.isIncremental = curIsIncremental
    end
    self.m_onSortChanged = onSortChanged
    self.m_sortOptions = sortOptions
    self.m_onToggleOptList = nil
    self.m_curCSOptionIndex = curCSOptionIndex or 0
    self:RefreshIncremental()
    self.m_tmpNoCallback = noCallback == true
    self.m_filterBtn = filterBtn
    if sortOptions ~= nil and next(sortOptions) ~= nil then
        if curCSOptionIndex then
            self.view.mobilePCNode.dropDown:Refresh(#self.m_sortOptions, curCSOptionIndex)
        else
            self.view.mobilePCNode.dropDown:Refresh(#self.m_sortOptions)
        end
    end

    self.m_tmpNoCallback = false
    self.view.mobilePCNode.dropDown:SetSelected(curCSOptionIndex, true, false)
    self.view.controllerNode.filterBtn.onClick:AddListener(function()
        if not self.m_enableFilterBtn then
            return
        end
        if self.m_filterBtn and not self.m_filterBtn.m_args then
            logger.error("SortNode的初始化需要在FilterBtn之后，否则会导致CommonFilterPanel表现不正确")
        end
        if self.m_filterBtn and self.m_filterBtn.m_args then
            self.m_filterBtn:_OpenFilterPanel()
        else
            local args = {}
            args.sortNodeWidget = self
            Notify(MessageConst.SHOW_COMMON_FILTER, args)
        end
    end)
    self:UpdateDeviceState()
end

SortNode.SetEnableFilterBtn = HL.Method(HL.Boolean) << function(self, enable)
    self.m_enableFilterBtn = enable
end


SortNode.SetOnToggleOptListCallback = HL.Method(HL.Function) << function(self, callback)
    self.m_onToggleOptList = callback
end

SortNode.OnSortChanged = HL.Method() << function(self)
    local sortOptData = self:GetCurSortData()
    self.m_onSortChanged(sortOptData, self.isIncremental)
end

SortNode.GetCurSortData = HL.Method().Return(HL.Table) << function(self)
    local data = self.m_sortOptions[LuaIndex(self.view.mobilePCNode.dropDown.selectedIndex)]
    return data
end

SortNode.GetCurSelectedIndex = HL.Method().Return(HL.Number) << function(self)
    return LuaIndex(self.view.mobilePCNode.dropDown.selectedIndex)
end


SortNode.GetCurSortKeys = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    local sortOptData = self:GetCurSortData()
    if self.isIncremental or not sortOptData.reverseKeys then
        return sortOptData.keys
    else
        return sortOptData.reverseKeys
    end
end

SortNode.SortCurData = HL.Method() << function(self)
    if self.m_sortOptions then
        self:OnSortChanged()
    end
end

SortNode.RefreshIncremental = HL.Method() << function(self)
    self.view.mobilePCNode.isIncrementalButton.text = self.isIncremental and Language.LUA_SORT_NODE_UP or Language.LUA_SORT_NODE_DOWN
    self.view.mobilePCNode.orderImage.transform.localScale = Vector3(1, self.isIncremental and -1 or 1, 1)
end

SortNode.UpdateDeviceState = HL.Method() << function(self)
    if DeviceInfo.usingController then
        if self.m_filterBtn then
            self.m_filterBtn.transform.localScale = Vector3(0,0,0)
        end

        local ascendingText = self:GetCurSortData().ascendingSortTitle or Language[self.config.SORT_ASCENDING_TEXT] or Language.LUA_COMMON_SORT_ASCENDING
        local descendingText =  self:GetCurSortData().descendingSortTitle or Language[self.config.SORT_DESCENDING_TEXT] or Language.LUA_COMMON_SORT_DESCENDING
        local subText = self.isIncremental and ascendingText or descendingText
        self.view.controllerNode.text.text = string.format(subText, self:GetCurSortData().name)
        if not self.m_filterBtn or not self.m_filterBtn.m_args or not self.m_filterBtn.m_args.tagGroups then
            self.view.controllerNode.normalState.gameObject:SetActive(false)
            self.view.controllerNode.selectedState.gameObject:SetActive(false)
        else
            local selectedTags = self.m_filterBtn.m_args.selectedTags
            local isSelected = (selectedTags and next(selectedTags)) ~= nil
            self.view.controllerNode.normalState.gameObject:SetActive(not isSelected)
            self.view.controllerNode.selectedState.gameObject:SetActive(isSelected)
        end
    else
        if self.m_filterBtn then
            self.m_filterBtn.transform.localScale = Vector3(1,1,1)
        end
    end
end

HL.Commit(SortNode)
return SortNode
