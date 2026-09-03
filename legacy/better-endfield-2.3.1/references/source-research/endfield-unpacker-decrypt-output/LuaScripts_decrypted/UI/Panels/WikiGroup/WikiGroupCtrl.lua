
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiGroup

WikiGroupCtrl = HL.Class('WikiGroupCtrl', uiCtrl.UICtrl)






WikiGroupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

WikiGroupCtrl.m_wikiGroupShowDataList = HL.Field(HL.Table)



WikiGroupCtrl.m_categoryType = HL.Field(HL.String) << ""

WikiGroupCtrl.m_detailPanelId = HL.Field(HL.Number) << 0

WikiGroupCtrl.m_args = HL.Field(HL.Table)

WikiGroupCtrl.m_activeScrollListCenter = HL.Field(HL.Any)









WikiGroupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitController()
    self.m_readWikiEntries = {}
    self.view.itemScrollListCenter.gameObject:SetActive(true)
    self.view.monsterScrollListCenter.gameObject:SetActive(false)
    self.m_activeScrollListCenter = self.view.itemScrollListCenter
    self.view.leftRedDotScrollRect.getRedDotStateAt = function(index)
        return self:_GetTabRedDotStateAt(index)
    end
    self.view.itemRedDotScrollRect.getRedDotStateAt = function(index)
        return self:_GetItemRedDotStateAt(index)
    end
    self.view.monsterRedDotScrollRect.getRedDotStateAt = function(index)
        return self:_GetItemRedDotStateAt(index)
    end
    self:Refresh(arg)

    self:_RefreshTop()
    self.m_phase:ActiveCommonSceneItem(true)
    self:_PlayDecoAnim(true)
end

WikiGroupCtrl.OnShow = HL.Override() << function(self)
    if self.m_phase and (self.m_phase.m_currentWikiGroupArgs.categoryType ~= self.m_args.categoryType or
        self.m_phase.m_currentWikiGroupArgs.wikiEntryShowData ~= self.m_args.wikiEntryShowData) then
        self:Refresh(self.m_phase.m_currentWikiGroupArgs)
        self:_RefreshTop()
        self.m_phase:ActiveCommonSceneItem(true)
    end
    self:_PlayDecoAnim(true)
end

WikiGroupCtrl.OnHide = HL.Override() << function(self)
    self:_MarkWikiEntryRead()
end

WikiGroupCtrl.OnClose = HL.Override() << function(self)
    self:_MarkWikiEntryRead()
end

WikiGroupCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    WikiGroupCtrl.Super._OnPlayAnimationOut(self)
    self:_PlayDecoAnim(false)
end



WikiGroupCtrl.Refresh = HL.Method(HL.Table) << function(self, args)
    self.m_args = args
    self.m_detailPanelId = args.detailPanelId
    self:_SwitchCategoryType(args.categoryType)

    self.m_wikiGroupShowDataList = WikiUtils.getWikiGroupShowDataList(args.categoryType, nil, args.includeLocked)
    self:_RefreshTab()
end

WikiGroupCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_args and lume.deepCopy(self.m_args) or {}
    local selectedGroupData = self.m_wikiGroupShowDataList and self.m_wikiGroupShowDataList[self.m_selectedIndex] or nil
    arg.wikiEntryShowData = nil
    arg.resumeState = {
        selectedGroupId = selectedGroupData and selectedGroupData.wikiGroupData.groupId or nil,
        focusTab = true,
    }
    return arg
end

WikiGroupCtrl._SwitchCategoryType = HL.Method(HL.String) << function(self, categoryType)
    self.m_categoryType = categoryType
    if self.m_categoryType ==  WikiConst.EWikiCategoryType.Monster then
        self:_SwitchActiveScrollList(self.view.monsterScrollListCenter)
    else
        self:_SwitchActiveScrollList(self.view.itemScrollListCenter)
    end
end

WikiGroupCtrl._SwitchActiveScrollList = HL.Method(HL.Any) << function(self, scrollListToActivate)
    if self.m_activeScrollListCenter == scrollListToActivate then
        return
    end
    self.m_activeScrollListCenter.gameObject:SetActive(false)
    self.m_activeScrollListCenter = scrollListToActivate
    self.m_activeScrollListCenter.gameObject:SetActive(true)
end

WikiGroupCtrl._RefreshTop = HL.Method() << function(self)

    local wikiTopArgs = {
        phase = self.m_phase,
        panelId = PANEL_ID,
        categoryType = self.m_categoryType,
    }
    self.view.top:InitWikiTop(wikiTopArgs)
end

WikiGroupCtrl._PlayDecoAnim = HL.Method(HL.Boolean) << function(self, isIn)
    if self.m_phase then
        self.m_phase:PlayDecoAnim(isIn and "wiki_uideco_grouppanel_in" or "wiki_uideco_grouppanel_out")
    end
end



WikiGroupCtrl.m_getTabCell = HL.Field(HL.Function)

WikiGroupCtrl.m_selectedIndex = HL.Field(HL.Number) << 0

WikiGroupCtrl._RefreshTab = HL.Method() << function(self)
    if self.m_getTabCell == nil then
        self.m_getTabCell = UIUtils.genCachedCellFunction(self.view.scrollListLeft)
        self.view.scrollListLeft.onUpdateCell:AddListener(function(object, csIndex)
            local tabCell = self.m_getTabCell(object)
            local wikiGroupShowData = self.m_wikiGroupShowDataList[LuaIndex(csIndex)]
            tabCell.titleNormalTxt.text = wikiGroupShowData.wikiGroupData.groupName
            tabCell.titleSelectTxt.text = wikiGroupShowData.wikiGroupData.groupName
            tabCell.normalIconImg:LoadSprite(UIConst.UI_SPRITE_WIKI_GROUP, wikiGroupShowData.wikiGroupData.iconId)
            tabCell.selectIconImg:LoadSprite(UIConst.UI_SPRITE_WIKI_GROUP, wikiGroupShowData.wikiGroupData.iconId)
            local isSelected = self.m_selectedIndex == LuaIndex(csIndex)
            self:_SetTabCellSelected(tabCell, isSelected)
            tabCell.btn.onClick:RemoveAllListeners()
            tabCell.btn.onClick:AddListener(function()

                if DeviceInfo.usingController then
                    if self.m_naviTabIndex > 0 then
                        return
                    end
                end

                self:_SetSelectedIndex(LuaIndex(csIndex))
            end)
            tabCell.redDot:InitRedDot("WikiGroup", wikiGroupShowData.wikiGroupData.groupId, nil, self.view.leftRedDotScrollRect)
        end)
        self.view.scrollListLeft.onGraduallyShowFinish:AddListener(function()
            if not DeviceInfo.usingController then
                return
            end
            local selectedTabCell = self.m_getTabCell(self.view.scrollListLeft:Get(CSIndex(self.m_selectedIndex)))
            if selectedTabCell then
                if not self.m_args.wikiEntryShowData then
                    self:SetNaviTarget(selectedTabCell.btn)
                end
            end
        end)
    end

    local selectedIndex = 1
    local resumeState = self.m_args and self.m_args.resumeState or nil
    if resumeState and not string.isEmpty(resumeState.selectedGroupId) then
        for i, groupShowData in ipairs(self.m_wikiGroupShowDataList) do
            if resumeState.selectedGroupId == groupShowData.wikiGroupData.groupId then
                selectedIndex = i
                break
            end
        end
    elseif self.m_args.wikiEntryShowData then
        for i, groupShowData in ipairs(self.m_wikiGroupShowDataList) do
            if self.m_args.wikiEntryShowData.wikiGroupData.groupId == groupShowData.wikiGroupData.groupId then
                selectedIndex = i
                break
            end
        end
    end
    self.view.scrollListLeft:UpdateCount(#self.m_wikiGroupShowDataList, CSIndex(selectedIndex), false, false, PhaseManager.isRecovering)
    if self.m_args.wikiEntryShowData then

        self.view.leftNaviGroup:NaviToThisGroup()
        self.view.centerNaviGroup:ManuallyFocus()
    end
    self:_SetSelectedIndex(selectedIndex)
    if DeviceInfo.usingController and resumeState and resumeState.focusTab == true then
        local selectedTabCell = self.m_getTabCell(self.view.scrollListLeft:Get(CSIndex(selectedIndex)))
        if selectedTabCell then
            self:SetNaviTarget(selectedTabCell.btn)
        end
    end
    if self.m_args then
        self.m_args.resumeState = nil
    end
end

WikiGroupCtrl._SetSelectedIndex = HL.Method(HL.Number) << function(self, selectedIndex)
    if self.m_selectedIndex == selectedIndex then
        return
    end
    self:_SetTabCellSelected(self.m_getTabCell(self.view.scrollListLeft:Get(CSIndex(self.m_selectedIndex))), false, true)
    self.m_selectedIndex = selectedIndex
    self:_SetTabCellSelected(self.m_getTabCell(self.view.scrollListLeft:Get(CSIndex(selectedIndex))), true, true)
    local wikiGroupShowData = self.m_wikiGroupShowDataList[selectedIndex]
    self:_RefreshScrollListCenter(wikiGroupShowData)
end

WikiGroupCtrl._SetTabCellSelected = HL.Method(HL.Table, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, cell, isSelected, playAnim)
    if not cell then
        return
    end
    cell.normalNode.gameObject:SetActive(not isSelected)
    if playAnim then
        UIUtils.PlayAnimationAndToggleActive(cell.selectAnimWrapper, isSelected)
    else
        cell.selectNode.gameObject:SetActive(isSelected)
    end
end





WikiGroupCtrl.m_getItemCell = HL.Field(HL.Function)

WikiGroupCtrl.m_getMonsterCell = HL.Field(HL.Function)

WikiGroupCtrl.m_wikiEntryShowDataList = HL.Field(HL.Table)

WikiGroupCtrl.m_ignoreScrollListAnim = HL.Field(HL.Boolean) << false

WikiGroupCtrl.m_isBackNaviSelected = HL.Field(HL.Boolean) << false

WikiGroupCtrl.m_selectedItemIndex = HL.Field(HL.Number) << 0

WikiGroupCtrl._RefreshScrollListCenter = HL.Method(HL.Table) << function(self, wikiGroupShowData)
    self:_MarkWikiEntryRead()
    self.m_wikiEntryShowDataList = wikiGroupShowData.wikiEntryShowDataList
    self:_BindCellFunction()
    local selectedIndex = 1
    if self.m_args.wikiEntryShowData then
        for i, entryShowData in ipairs(self.m_wikiEntryShowDataList) do
            if self.m_args.wikiEntryShowData.wikiEntryData.id == entryShowData.wikiEntryData.id then
                selectedIndex = i
                self.m_ignoreScrollListAnim = true
                break
            end
        end
    end
    self.m_selectedItemIndex = selectedIndex
    self.m_activeScrollListCenter:UpdateCount(#self.m_wikiEntryShowDataList, CSIndex(selectedIndex), false, false, self.m_ignoreScrollListAnim)
    if self.m_ignoreScrollListAnim then
        self:_NaviToSelectedItem()
    end
    self.m_ignoreScrollListAnim = false
end

WikiGroupCtrl._BindCellFunction = HL.Method() << function(self)
    if self.m_categoryType == WikiConst.EWikiCategoryType.Monster then
        if self.m_getMonsterCell then
            return
        end
        self.m_getMonsterCell = UIUtils.genCachedCellFunction(self.view.monsterScrollListCenter)
        self.view.monsterScrollListCenter.onUpdateCell:AddListener(function(object, csIndex)

            local monsterCell = self.m_getMonsterCell(object)
            local wikiEntryShowData = self.m_wikiEntryShowDataList[LuaIndex(csIndex)]
            monsterCell:InitMonster(wikiEntryShowData.wikiEntryData.refMonsterTemplateId, function()
                self:_MarkWikiEntryRead()

                local args = {
                    categoryType = self.m_categoryType,
                    wikiEntryShowData = wikiEntryShowData,
                    wikiGroupShowDataList = self.m_wikiGroupShowDataList
                }
                self.view.changeEffect:PlayOutAnimation()
                self:PlayAnimationOutWithCallback(function()
                    self.m_phase:OpenCategory(self.m_categoryType, args)
                end)
            end)
            if DeviceInfo.usingController then
                monsterCell:SetEnableHoverTips(false)
            end
            local entryId = wikiEntryShowData.wikiEntryData.id
            monsterCell.redDot:InitRedDot("WikiEntry", entryId, nil, self.view.monsterRedDotScrollRect)
            if WikiUtils.isWikiEntryUnread(entryId) then
                self.m_readWikiEntries[entryId] = true
            end
        end)
        self.view.monsterScrollListCenter.onGraduallyShowFinish:AddListener(function()
            self:_NaviToSelectedItem()
        end)
    else
        if self.m_getItemCell then
            return
        end
        self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.itemScrollListCenter)
        self.view.itemScrollListCenter.onUpdateCell:AddListener(function(object, csIndex)

            local itemCell = self.m_getItemCell(object)
            local wikiEntryShowData = self.m_wikiEntryShowDataList[LuaIndex(csIndex)]
            itemCell:InitItem({ id = wikiEntryShowData.wikiEntryData.refItemId }, function()
                self:_MarkWikiEntryRead()

                local args = {
                    categoryType = self.m_categoryType,
                    wikiEntryShowData = wikiEntryShowData,
                    wikiGroupShowDataList = self.m_wikiGroupShowDataList
                }
                self.view.changeEffect:PlayOutAnimation()
                self:PlayAnimationOutWithCallback(function()
                    self.m_phase:OpenCategory(self.m_categoryType, args)
                end)
            end)
            local entryId = wikiEntryShowData.wikiEntryData.id
            itemCell.view.redDot:InitRedDot("WikiEntry", entryId, nil, self.view.itemRedDotScrollRect)
            if DeviceInfo.usingController then
                itemCell:SetEnableHoverTips(false)
            end
            if itemCell.view.potentialStar then
                itemCell.view.potentialStar.gameObject:SetActive(false)
            end
            if itemCell.view.lockedNode then
                itemCell.view.lockedNode.gameObject:SetActive(not wikiEntryShowData.isUnlocked)
            end
            if WikiUtils.isWikiEntryUnread(entryId) then
                self.m_readWikiEntries[entryId] = true
            end
        end)
        self.view.itemScrollListCenter.onGraduallyShowFinish:AddListener(function()
            self:_NaviToSelectedItem()
        end)
    end
end





WikiGroupCtrl.m_readWikiEntries = HL.Field(HL.Table)

WikiGroupCtrl._MarkWikiEntryRead = HL.Method() << function(self)
    if self.m_readWikiEntries then
        local entryIdList = {}
        for entryId, _ in pairs(self.m_readWikiEntries) do
            table.insert(entryIdList, entryId)
        end
        GameInstance.player.wikiSystem:MarkWikiEntryRead(entryIdList)
        self.m_readWikiEntries = {}
    end
end

WikiGroupCtrl._GetTabRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_wikiGroupShowDataList then
        return 0
    end
    local wikiGroupShowData = self.m_wikiGroupShowDataList[luaIndex]
    if not wikiGroupShowData then
        return 0
    end
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("WikiGroup", wikiGroupShowData.wikiGroupData.groupId)
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end

WikiGroupCtrl._GetItemRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_wikiEntryShowDataList then
        return 0
    end
    local wikiEntryShowData = self.m_wikiEntryShowDataList[luaIndex]
    if not wikiEntryShowData then
        return 0
    end
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("WikiEntry", wikiEntryShowData.wikiEntryData.id)
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end





WikiGroupCtrl.m_naviTabIndex = HL.Field(HL.Number) << 0

WikiGroupCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.centerNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        if not isTopLayer then
            self.m_naviTabIndex = self.m_selectedIndex
        end
    end)
    self.view.leftNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        if isTopLayer then
            self:_StartCoroutine(function()
                if self.m_naviTabIndex > 0 then
                    coroutine.step()
                    if not self.view.leftNaviGroup.IsTopLayer then
                        return
                    end
                    local tabCell = self.m_getTabCell(self.view.scrollListLeft:Get(CSIndex(self.m_naviTabIndex)))
                    self.m_naviTabIndex = 0
                    if tabCell then
                        self:SetNaviTarget(tabCell.btn)
                    end

                end
            end)
        end
    end)
    self.view.centerNaviGroup.getDefaultSelectableFunc = function()
        if self.m_categoryType == WikiConst.EWikiCategoryType.Monster then
            if self.m_getMonsterCell then
                local monsterCell = self.m_getMonsterCell(self.view.monsterScrollListCenter:Get(CSIndex(1)))
                if monsterCell then
                    return monsterCell.view.button
                end
            end
        else
            if self.m_getItemCell then
                local itemCell = self.m_getItemCell(self.view.itemScrollListCenter:Get(CSIndex(1)))
                if itemCell then
                    return itemCell.view.button
                end
            end
        end
    end
end

WikiGroupCtrl._NaviToSelectedItem = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    if self.m_categoryType == WikiConst.EWikiCategoryType.Monster then
        local selectedMonsterCell = self.m_getMonsterCell(self.view.monsterScrollListCenter:Get(CSIndex(self.m_selectedItemIndex)))
        if not self.m_isBackNaviSelected and selectedMonsterCell and self.m_args.wikiEntryShowData then
            self:SetNaviTarget(selectedMonsterCell.view.button)
            self.m_isBackNaviSelected = true
        end
    else
        local selectedItemCell = self.m_getItemCell(self.view.itemScrollListCenter:Get(CSIndex(self.m_selectedItemIndex)))
        if not self.m_isBackNaviSelected and selectedItemCell and self.m_args.wikiEntryShowData then
            self:SetNaviTarget(selectedItemCell.view.button)
            self.m_isBackNaviSelected = true
        end
    end
end



HL.Commit(WikiGroupCtrl)
