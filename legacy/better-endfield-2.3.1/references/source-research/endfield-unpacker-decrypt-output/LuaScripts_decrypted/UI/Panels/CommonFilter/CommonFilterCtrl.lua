local SelectMode =
{
    None = "None",
    Filter = "Filter",
    Sort = "Sort",
}

local ShowMode =
{
    None = "None",
    Multi = "Multi",
    Single = "Single",
}
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonFilter
CommonFilterCtrl = HL.Class('CommonFilterCtrl', uiCtrl.UICtrl)

CommonFilterCtrl.ShowCommonFilter = HL.StaticMethod(HL.Table) << function(args)

    self = UIManager:AutoOpen(PANEL_ID)
    self:_Init(args)
end





CommonFilterCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CommonFilterCtrl.m_getFilterTagGroupCell = HL.Field(HL.Function)

CommonFilterCtrl.m_getSortTagGroupCell = HL.Field(HL.Function)

CommonFilterCtrl.m_filterSelectedTags = HL.Field(HL.Table)

CommonFilterCtrl.m_filterTagGroups = HL.Field(HL.Table)

CommonFilterCtrl.m_tags = HL.Field(HL.Table)

CommonFilterCtrl.m_args = HL.Field(HL.Table)

CommonFilterCtrl.m_filterIsGroup = HL.Field(HL.Boolean) << false

CommonFilterCtrl.m_titlePaddingTop = HL.Field(HL.Number) << 0

CommonFilterCtrl.m_noTitlePaddingTop = HL.Field(HL.Number) << 0

CommonFilterCtrl.m_sortSelectedTag = HL.Field(HL.Table)

CommonFilterCtrl.m_sortSelectedIndex = HL.Field(HL.Number) << 0

CommonFilterCtrl.m_originalOptions = HL.Field(HL.Table)

CommonFilterCtrl.m_originalSortTag = HL.Field(HL.Table)

CommonFilterCtrl.m_sortOptions = HL.Field(HL.Table)

CommonFilterCtrl.m_sortTagGroups = HL.Field(HL.Table)

CommonFilterCtrl.m_curSelectMode = HL.Field(HL.String) << SelectMode.Filter

CommonFilterCtrl.m_showMode = HL.Field(HL.String) << ShowMode.None

CommonFilterCtrl.m_naviTargetInitialized = HL.Field(HL.Boolean) << false

CommonFilterCtrl.m_sortNode = HL.Field(HL.Any)


CommonFilterCtrl.m_naviTargetInfo = HL.Field(HL.Table)


CommonFilterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        self:_CloseSelf()
    end)
    self.view.mask.onClick:AddListener(function()
        self:_CloseSelf()
    end)
    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)
    self.view.resetBtn.onClick:AddListener(function()
        self:_OnClickReset()
    end)

    self.m_titlePaddingTop = self.view.tagGroupCell.gridLayoutGroup.padding.top
    self.m_noTitlePaddingTop = self.m_titlePaddingTop - self.view.tagGroupCell.titleNode.transform.rect.height

    self.m_getFilterTagGroupCell = UIUtils.genCachedCellFunction(self.view.scrollListFilter)
    self.view.scrollListFilter.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateFilterTagGroupCell(self.m_getFilterTagGroupCell(obj), LuaIndex(csIndex))
    end)

    self.m_getSortTagGroupCell = UIUtils.genCachedCellFunction(self.view.scrollListSort)
    self.view.scrollListSort.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateSortTagGroupCell(self.m_getSortTagGroupCell(obj), LuaIndex(csIndex))
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    Notify(MessageConst.ON_OPEN_COMMON_FILTER)
end

















CommonFilterCtrl._Init = HL.Method(HL.Table) << function(self, args)
    self:_InitData(args)
end
CommonFilterCtrl._InitData = HL.Method(HL.Table) << function(self, args)
    self.m_args = args

    self.m_naviTargetInitialized = false

    if args.tagGroups then
        self.m_filterTagGroups = args.tagGroups or {}
        self.m_filterSelectedTags = args.selectedTags and lume.copy(args.selectedTags) or {}
        local filterGroupCount = #self.m_filterTagGroups or 0
        self.m_filterIsGroup = filterGroupCount > 1

        self.m_originalOptions = lume.copy(self.m_filterSelectedTags)

        self.m_curSelectMode = SelectMode.Filter
    end



    if args.sortNodeWidget and DeviceInfo.usingController then
        local sortNode = args.sortNodeWidget
        self.m_sortOptions = sortNode.m_sortOptions
        self.m_sortSelectedIndex = CSIndex(sortNode:GetCurSelectedIndex() or 1)
        self.m_sortTagGroups = {}
        for i = 1, #self.m_sortOptions * 2 do
            local groupIndex = math.ceil(i / 2)
            local indexPos = (i % 2) == 1 and 1 or 2
            self.m_sortTagGroups[i] = lume.deepCopy(self.m_sortOptions[groupIndex])
            if indexPos == 1 then
                self.m_sortTagGroups[i].isIncremental = false
            else
                self.m_sortTagGroups[i].isIncremental = true
            end
        end
        self.m_sortSelectedTag = sortNode:GetCurSortData()
        self.m_sortSelectedTag.isIncremental = sortNode.isIncremental
        self.m_sortNode = sortNode
        self.m_originalSortTag = lume.deepCopy(self.m_sortSelectedTag)
        self.m_curSelectMode = SelectMode.Sort
    end


    if args.sortNodeWidget and args.tagGroups and DeviceInfo.usingController then
        self.m_showMode = ShowMode.Multi
        self.m_curSelectMode = SelectMode.Sort
        self.view.titleMulti.sortState.onClick:AddListener(function()
            if self.m_curSelectMode == SelectMode.Sort then
                return
            end
            self.m_curSelectMode = SelectMode.Sort
            self.m_naviTargetInitialized = false
            self:_ReFreshMultiBtnState()
        end)

        self.view.titleMulti.filterState.onClick:AddListener(function()
            if self.m_curSelectMode == SelectMode.Filter then
                return
            end
            self.m_curSelectMode = SelectMode.Filter
            self.m_naviTargetInitialized = false
            self:_ReFreshMultiBtnState()
        end)
    else
        self.m_showMode = ShowMode.Single
        if args.sortNodeWidget and DeviceInfo.usingController then
            self.view.titleSingle.titleTxt.text = Language.LUA_COMMON_SORT_TITLE
        else
            self.view.titleSingle.titleTxt.text = Language.LUA_COMMON_FILTER_TITLE
        end
    end
    self:_ReFreshMultiBtnState()
    self.view.titleSingle.gameObject:SetActive(self.m_showMode ~= ShowMode.Multi)
    self.view.titleMulti.gameObject:SetActive(self.m_showMode == ShowMode.Multi)
end

CommonFilterCtrl._ReFreshMultiBtnState = HL.Method() << function(self)
    local isFilter = self.m_curSelectMode == SelectMode.Filter
    if isFilter then
        self.view.titleMulti.filterStateLuaReference.select:PlayInAnimation()
        self.view.titleMulti.sortStateLuaReference.select:PlayOutAnimation()
    else
        self.view.titleMulti.sortStateLuaReference.select:PlayInAnimation()
        self.view.titleMulti.filterStateLuaReference.select:PlayOutAnimation()
    end
    self.view.titleMulti.filterStateLuaReference.unSelect.gameObject:SetActive(not isFilter)
    self.view.titleMulti.sortStateLuaReference.unSelect.gameObject:SetActive(isFilter)

    self.view.filterResultNode.gameObject:SetActive(isFilter)
    if self.m_showMode == ShowMode.Multi then
        if isFilter then
            self.view.scrollListFilter.gameObject:SetActive(true)
            self.view.listNode:PlayWithTween("commonfilter_list_switch_out")
        else
            self.view.scrollListSort.gameObject:SetActive(true)
            self.view.listNode:PlayWithTween("commonfilter_list_switch_in")
        end
    else
        self.view.scrollListFilter.gameObject:SetActive(isFilter)
        self.view.scrollListSort.gameObject:SetActive(not isFilter)
    end
    self:_UpdateState()
end

CommonFilterCtrl._UpdateState = HL.Method() << function(self)
    if self.m_curSelectMode == SelectMode.Filter then
        self:_UpdateResultCount()
        self.m_naviTargetInfo = nil
        if self.m_filterSelectedTags and next(self.m_filterSelectedTags) then
            for groupIndex, groupInfo in ipairs(self.m_filterTagGroups) do
                for tagIdx, tagInfo in ipairs(groupInfo.tags) do
                    if self:_GetSelectedTagIndex(tagInfo) > 0 then
                        if (self.m_naviTargetInfo and self.m_naviTargetInfo.groupIndex < groupIndex) or
                            (self.m_naviTargetInfo and self.m_naviTargetInfo.groupIndex == groupIndex and self.m_naviTargetInfo.tagIndex < tagIdx) then
                                goto continue
                        end
                        self.m_naviTargetInfo = { groupIndex = groupIndex, tagIndex = tagIdx }
                        ::continue::
                    end
                end
            end
        end
        if not self.m_naviTargetInfo then
            self.m_naviTargetInfo = { groupIndex = 1, tagIndex = 1 }
        end
        self.view.scrollListFilter:UpdateCount(#self.m_filterTagGroups)
        self.m_naviTargetInitialized = false
        self.view.scrollListFilter:ScrollToIndex(CSIndex(self.m_naviTargetInfo.groupIndex), true)
        self.m_naviTargetInitialized = true
    elseif self.m_curSelectMode == SelectMode.Sort then
        local targetIndex = 1
        if self.m_sortSelectedTag then
            for i, tagInfo in ipairs(self.m_sortTagGroups) do
                if self.m_sortSelectedTag.name == tagInfo.name
                    and self.m_sortSelectedTag.isIncremental == tagInfo.isIncremental then
                    targetIndex = i
                    break
                end
            end
        end
        self.view.scrollListSort:UpdateCount(#self.m_sortTagGroups, false, false, false, true)
        self.m_naviTargetInfo = nil
        self.m_naviTargetInitialized = false
        self.view.scrollListSort:ScrollToIndex(CSIndex(targetIndex), true)
        self.m_naviTargetInitialized = true
    end
end

CommonFilterCtrl._OnCellSelectedChanged = HL.Method(HL.Table, HL.Boolean, HL.Boolean) << function(self, cell, isSelect, active)
    local bindingId = cell.toggle.hoverConfirmBindingId
    if self.m_curSelectMode == SelectMode.Sort then
        InputManagerInst:SetBindingText(bindingId, Language.LUA_COMMON_FILTER_SELECT_KEY_HINT)
        if active then
            InputManagerInst:ToggleBinding(bindingId, not isSelect)
        end
    elseif self.m_curSelectMode == SelectMode.Filter then
        if isSelect then
            InputManagerInst:SetBindingText(bindingId, Language.LUA_COMMON_FILTER_CANCEL_SELECT_KEY_HINT)
        else
            InputManagerInst:SetBindingText(bindingId, Language.LUA_COMMON_FILTER_SELECT_KEY_HINT)
        end
    end
    if not active then
        InputManagerInst:ToggleBinding(bindingId, false)
    end
end

CommonFilterCtrl._OnUpdateSortTagGroupCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    if not self.m_sortSelectedTag or self.m_curSelectMode ~= SelectMode.Sort then

        return
    end
    local tagInfo = self.m_sortTagGroups[index]
    local ascendingText = tagInfo.ascendingSortTitle or Language[self.m_sortNode.config.SORT_ASCENDING_TEXT] or Language.LUA_COMMON_SORT_ASCENDING
    local descendingText =  tagInfo.descendingSortTitle or Language[self.m_sortNode.config.SORT_DESCENDING_TEXT] or Language.LUA_COMMON_SORT_DESCENDING
    local subText = tagInfo.isIncremental and ascendingText or descendingText
    cell.name.text = string.format(subText, tagInfo.name)
    cell.toggle.onValueChanged:RemoveAllListeners()
    local isOn = self.m_sortSelectedTag.name == tagInfo.name and self.m_sortSelectedTag.isIncremental == tagInfo.isIncremental
    cell.toggle.isOn = isOn
    cell.check.gameObject:SetActive(isOn)
    cell.toggle.onIsNaviTargetChanged = nil
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self.m_sortSelectedTag = tagInfo
            self.m_sortSelectedIndex = math.floor((index - 1) / 2)
            self:_UpdateModState()
        end
        cell.check.gameObject:SetActive(isOn)
        self:_OnCellSelectedChanged(cell, isOn, true)
    end)
    if not self.m_naviTargetInitialized then
        if index == 1 then
            self:SetNaviTarget(cell.toggle)
        end
        if isOn then
            self:SetNaviTarget(cell.toggle)
        end
    end
    cell.toggle.onIsNaviTargetChanged = function(active)
        self:_OnCellSelectedChanged(cell, cell.toggle.isOn, active)
    end
    self:_OnCellSelectedChanged(cell, cell.toggle.isOn, cell.toggle.isNaviTarget)

end

CommonFilterCtrl._UpdateModState = HL.Method() << function(self)
    if not self.m_originalSortTag then
        return
    end
    local sortModState = self.m_originalSortTag.name ~= self.m_sortSelectedTag.name or
        self.m_originalSortTag.isIncremental ~= self.m_sortSelectedTag.isIncremental

    local filterModState = false
    local originalTagNames = {}

    if self.m_filterSelectedTags and self.m_originalOptions then
        for _, originTag in ipairs(self.m_originalOptions) do
            originalTagNames[originTag.name] = true
        end

        if #self.m_originalOptions ~= #self.m_filterSelectedTags then
            filterModState = true
        end
        if not filterModState then
            for _, nowTag in ipairs(self.m_filterSelectedTags) do
                if not originalTagNames[nowTag.name] then
                    filterModState = true
                    break
                end
            end
        end
    end


    self.view.sortModifiedMark.gameObject:SetActive(sortModState)
    self.view.filterModifiedMark.gameObject:SetActive(filterModState)
end

CommonFilterCtrl._OnUpdateFilterTagGroupCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    if not self.m_filterTagGroups or self.m_curSelectMode ~= SelectMode.Filter then

        return
    end
    local tagGroupInfo = self.m_filterTagGroups[index]
    if self.m_filterIsGroup then
        cell.titleText.text = tagGroupInfo.title
        cell.titleNode.gameObject:SetActive(true)
        cell.gridLayoutGroup.padding.top = self.m_titlePaddingTop
    else
        cell.titleNode.gameObject:SetActive(false)
        cell.gridLayoutGroup.padding.top = self.m_noTitlePaddingTop
    end
    if not cell.tagCells then
        cell.tagCells = UIUtils.genCellCache(cell.tagCell)
    end
    cell.tagCells:Refresh(#tagGroupInfo.tags, function(tagCell, tagIndex)
        local tagInfo = tagGroupInfo.tags[tagIndex]
        self:_UpdateTagCell(tagCell, tagInfo)
        if not self.m_naviTargetInitialized
            and self.m_naviTargetInfo
            and self.m_naviTargetInfo.groupIndex == index
            and self.m_naviTargetInfo.tagIndex == tagIndex then
            self:SetNaviTarget(tagCell.toggle)
        end
    end)
end


CommonFilterCtrl._GetSelectedTagIndex = HL.Method(HL.Table).Return(HL.Number) << function(self, tagInfo)
    for index, selectedTagInfo in ipairs(self.m_filterSelectedTags) do
        if FilterUtils.isSameTagInfo(selectedTagInfo, tagInfo) then
            return index
        end
    end
    return 0
end

CommonFilterCtrl._UpdateTagCell = HL.Method(HL.Table, HL.Table) << function(self, cell, tagInfo)
    cell.name.text = tagInfo.name
    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.isOn = self:_GetSelectedTagIndex(tagInfo) > 0
    cell.toggle.onIsNaviTargetChanged = nil
    cell.toggle.onIsNaviTargetChanged = function(active)
        self:_OnCellSelectedChanged(cell, cell.toggle.isOn, active)
    end

    cell.toggle.onValueChanged:AddListener(function(isOn)
        self:_OnClickTagCell(tagInfo, isOn)
        self:_UpdateModState()
        self:_OnCellSelectedChanged(cell, isOn, true)
    end)
    self:_OnCellSelectedChanged(cell, cell.toggle.isOn, cell.toggle.isNaviTarget)
end

CommonFilterCtrl._OnClickTagCell = HL.Method(HL.Table, HL.Boolean) << function(self, tagInfo, isOn)
    local index = self:_GetSelectedTagIndex(tagInfo)
    if isOn then
        if index <= 0 then
            table.insert(self.m_filterSelectedTags, tagInfo)
        end
    else
        if index > 0 then
            table.remove(self.m_filterSelectedTags, index)
        end
    end
    self:_UpdateResultCount()
end

CommonFilterCtrl._UpdateResultCount = HL.Method() << function(self)
    local getResultCount = self.m_args.getResultCount
    if not getResultCount or not next(self.m_filterSelectedTags) or self.m_curSelectMode ~= SelectMode.Filter then
        self.view.filterResultNode.gameObject:SetActive(false)
        return
    end
    self.view.filterResultNode.gameObject:SetActive(true)
    self.view.filterResultCount.text = getResultCount(self.m_filterSelectedTags)
end

CommonFilterCtrl._OnClickConfirm = HL.Method() << function(self)
    local sortNode = self.m_args.sortNodeWidget
    local isMultiMode = self.m_showMode == ShowMode.Multi

    if self.m_curSelectMode == SelectMode.Filter or isMultiMode then
        local onConfirm = self.m_args.onConfirm
        local selectedTags = next(self.m_filterSelectedTags) and self.m_filterSelectedTags or nil
        onConfirm(selectedTags)
        if sortNode then
            sortNode:UpdateDeviceState()
        end
        if not isMultiMode then
            self:_CloseSelf()
            return
        end
    end

    if (self.m_curSelectMode == SelectMode.Sort or isMultiMode) and sortNode then
        if sortNode.isIncremental ~= self.m_sortSelectedTag.isIncremental then
            sortNode.isIncremental = self.m_sortSelectedTag.isIncremental
            sortNode:RefreshIncremental()
            sortNode:OnSortChanged()
        end

        sortNode.view.mobilePCNode.dropDown:SetSelected(self.m_sortSelectedIndex)
        sortNode:UpdateDeviceState()
    end
    self:_CloseSelf()
end

CommonFilterCtrl._OnClickReset = HL.Method() << function(self)
    if self.m_curSelectMode == SelectMode.Filter then
        self.m_filterSelectedTags = {}
        self.view.scrollListFilter:UpdateCount(#self.m_filterTagGroups)
        self:_UpdateResultCount()
    elseif self.m_curSelectMode == SelectMode.Sort then
        self.m_sortSelectedTag = self.m_sortTagGroups[1]
        self.m_sortSelectedIndex = CSIndex(1)
        self.view.scrollListSort:UpdateCount(#self.m_sortTagGroups)
    end
    self:_UpdateModState()
end

CommonFilterCtrl._CloseSelf = HL.Method() << function(self)
    self.m_args = nil
    self.m_tags = nil
    self.m_filterTagGroups = nil
    self.m_filterSelectedTags = nil
    self.m_sortSelectedIndex = 0
    self.m_sortSelectedTag = nil
    self:PlayAnimationOutAndClose()
    Notify(MessageConst.ON_CLOSE_COMMON_FILTER)
end

HL.Commit(CommonFilterCtrl)