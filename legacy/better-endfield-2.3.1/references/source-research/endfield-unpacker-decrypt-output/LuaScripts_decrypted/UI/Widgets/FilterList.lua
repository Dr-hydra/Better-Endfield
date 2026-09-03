local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FilterList = HL.Class('FilterList', UIWidgetBase)

FilterList.m_onConfirm = HL.Field(HL.Function)

FilterList.m_selectedTags = HL.Field(HL.Table)

FilterList.m_selectedTagsBefore = HL.Field(HL.Table)

FilterList.m_tagGroupCellCache = HL.Field(HL.Forward("UIListCache"))

FilterList.m_tagGroups = HL.Field(HL.Table)



FilterList._OnFirstTimeInit = HL.Override() << function(self)
    self.view.btnCommon.onClick:AddListener(function()
        if self.m_onConfirm then
            self.m_onConfirm(lume.copy(self.m_selectedTags))
        end
        self.m_selectedTagsBefore = lume.copy(self.m_selectedTags)
    end)

    self.view.mask.onClick:AddListener(function()
        self:_CloseSelf()
    end)

    self.m_tagGroupCellCache = UIUtils.genCellCache(self.view.tagGroupCell)
    self.view.resetBtn.onClick:AddListener(function()
        self.m_selectedTags = {}
        self:_RefreshFilterList()
    end)

    self:BindInputPlayerAction("common_open_filter", function()
        self:_CloseSelf()
    end)

    self:_InitNavigation()
end

FilterList._CloseSelf = HL.Method() << function(self)
    self.m_selectedTags = lume.copy(self.m_selectedTagsBefore)
    self:_RefreshFilterList()
    self.view.gameObject:SetActive(false)
    AudioAdapter.PostEvent("au_ui_menu_side_close")
end

FilterList.InitFilterListWithTags = HL.Method(HL.Table, HL.Function, HL.Opt(HL.Table)) << function(self, tags, onConfirm, selectedTags)
    local tagGroups = {}
    table.insert(tagGroups, {
        tags = tags
    })

    self:InitFilterListWithTagGroups(tagGroups, onConfirm, selectedTags)
end

FilterList.InitFilterListWithTagGroups = HL.Method(HL.Table, HL.Function, HL.Opt(HL.Table)) << function(self, tagGroups, onConfirm, selectedTags)
    self:_FirstTimeInit()

    self.m_selectedTags = {}
    if selectedTags then
        for i = 1, #selectedTags do
            table.insert(self.m_selectedTags,selectedTags[i])
        end
    end
    self.m_selectedTagsBefore = {}
    self.m_onConfirm = onConfirm
    self.m_tagGroups = tagGroups

    self:_RefreshFilterList()
end

FilterList._RefreshFilterList = HL.Method() << function(self)
    local tagGroups = self.m_tagGroups
    self.m_tagGroupCellCache:Refresh(#tagGroups, function(cell, index)
        local tagGroupInfo = tagGroups[index]
        self:_RefreshTagGroupCell(cell, tagGroupInfo)
    end)
end

FilterList._RefreshTagGroupCell = HL.Method(HL.Table, HL.Table) << function(self, groupCell, tagGroupInfo)
    if not groupCell.m_tagCellCache then
        groupCell.m_tagCellCache = UIUtils.genCellCache(groupCell.tagCell)
    end

    local tags = tagGroupInfo.tags or {}
    local groupTitle = tagGroupInfo.title
    groupCell.title.gameObject:SetActive(groupTitle ~= nil)
    if groupTitle then
        groupCell.titleText.text = groupTitle
    end

    local tagCount = #tags
    groupCell.m_tagCellCache:Refresh(tagCount, function(tagCell, index)
        local tagInfo = tags[index]
        local isEnd = index == tagCount
        self:_RefreshTagCell(tagCell, tagInfo, isEnd)
    end)
end

FilterList._RefreshTagCell = HL.Method(HL.Any, HL.Table, HL.Boolean) << function(self, cell, tagInfo, isEnd)
    cell.tagTxt.text = tagInfo.name
    cell.tagTxtS.text = tagInfo.name
    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            if not lume.find(self.m_selectedTags, tagInfo) then
                table.insert(self.m_selectedTags, tagInfo)
            end
        else
            if lume.find(self.m_selectedTags,tagInfo) then
                lume.remove(self.m_selectedTags, tagInfo)
            end
        end
    end)
    cell.toggle.isOn = lume.find(self.m_selectedTags,tagInfo)
    cell.decoLine.gameObject:SetActive(not isEnd)
end




FilterList.m_curNaviIndex = HL.Field(HL.Number) << 1

FilterList._InitNavigation = HL.Method() << function(self)
    InputManagerInst:CreateBindingByActionId("common_navigation_up_no_hint", function()
        self:_NavigateSelected(-1)
    end, self.view.inputBindingGroupMonoTarget.groupId)
    InputManagerInst:CreateBindingByActionId("common_navigation_down", function()
        self:_NavigateSelected(1)
    end, self.view.inputBindingGroupMonoTarget.groupId)
    InputManagerInst:CreateBindingByActionId("common_select", function()
        self:_SelectCurNavigation()
    end, self.view.inputBindingGroupMonoTarget.groupId)
end

FilterList._NavigateSelected = HL.Method(HL.Number) << function(self, offset)

    local count = 0
    for _, v in ipairs(self.m_tagGroups) do
        for __, vv in ipairs(v.tags) do
            count = count + 1
        end
    end
    self.m_curNaviIndex = lume.clamp(self.m_curNaviIndex + offset, 1, count)
    self:_RefreshNavigateSelected()
    AudioAdapter.PostEvent("au_ui_g_select")
end

FilterList._SelectCurNavigation = HL.Method() << function(self)
    local count = 0
    for k, v in ipairs(self.m_tagGroups) do
        for kk, vv in ipairs(v.tags) do
            count = count + 1
            if count == self.m_curNaviIndex then
                local cell = self.m_tagGroupCellCache:Get(k).m_tagCellCache:Get(kk)
                cell.toggle.isOn = not cell.toggle.isOn
                cell.toggle:PlayAudio()
                return
            end
        end
    end
end

FilterList._RefreshNavigateSelected = HL.Method() << function(self)
    local count = 0
    self.m_tagGroupCellCache:Update(function(groupCell)
        groupCell.m_tagCellCache:Update(function(cell)
            count = count + 1
            local isSelected = count == self.m_curNaviIndex
            cell.controllerSelectedHintNode.gameObject:SetActive(isSelected)
        end)
    end)
end





FilterList._OnEnable = HL.Override() << function(self)
    AudioAdapter.PostEvent("au_ui_menu_side_open")

    if not DeviceInfo.usingController or self.m_isFirstTimeInit then
        return
    end

    Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
        panelId = self:GetPanelId(),
        isGroup = true,
        id = self.view.inputBindingGroupMonoTarget.groupId,
        hintPlaceholder = self.view.controllerHintPlaceholder,
        rectTransform = self.view.main.transform,
    })

    self.m_curNaviIndex = 1
    self:_RefreshNavigateSelected()
end

FilterList._OnDisable = HL.Override() << function(self)
    if not DeviceInfo.usingController or self.m_isFirstTimeInit then
        return
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputBindingGroupMonoTarget.groupId)
end

FilterList._OnDestroy = HL.Override() << function(self)
    if not DeviceInfo.usingController or self.m_isFirstTimeInit then
        return
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputBindingGroupMonoTarget.groupId)
end


HL.Commit(FilterList)
return FilterList
