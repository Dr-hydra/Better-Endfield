
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiEquipSuit

WikiEquipSuitCtrl = HL.Class('WikiEquipSuitCtrl', uiCtrl.UICtrl)

local SWITCH_ANIM_NAME = "wiki_equipsuit_switch"





WikiEquipSuitCtrl.s_messages = HL.StaticField(HL.Table) << {

}

WikiEquipSuitCtrl.m_wikiGroupShowDataList = HL.Field(HL.Table)



WikiEquipSuitCtrl.m_categoryType = HL.Field(HL.String) << ""

WikiEquipSuitCtrl.m_detailPanelId = HL.Field(HL.Number) << 0

WikiEquipSuitCtrl.m_args = HL.Field(HL.Table)




WikiEquipSuitCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitController()
    self.m_readWikiEntries = {}
    self.view.leftRedDotScrollRect.getRedDotStateAt = function(index)
        return self:_GetTabRedDotStateAt(index)
    end
    self.view.itemRedDotScrollRect.getRedDotStateAt = function(index)
        return self:_GetItemRedDotStateAt(index)
    end
    self:Refresh(arg)

    self:_RefreshTop()
    self.m_phase:ActiveCommonSceneItem(true)
    self:_PlayDecoAnim(true)
end

WikiEquipSuitCtrl.OnShow = HL.Override() << function(self)
    if self.m_phase and self.m_phase.m_currentWikiGroupArgs.wikiEntryShowData ~= self.m_args.wikiEntryShowData then
        self:Refresh(self.m_phase.m_currentWikiGroupArgs)
        self:_RefreshTop()
        self.m_phase:ActiveCommonSceneItem(true)
    end
    self:_PlayDecoAnim(true)
end

WikiEquipSuitCtrl.OnHide = HL.Override() << function(self)
    self:_MarkWikiEntryRead()
end

WikiEquipSuitCtrl.OnClose = HL.Override() << function(self)
    self:_MarkWikiEntryRead()
end

WikiEquipSuitCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    WikiEquipSuitCtrl.Super._OnPlayAnimationOut(self)
    self:_PlayDecoAnim(false)
end



WikiEquipSuitCtrl.Refresh = HL.Method(HL.Table) << function(self, args)
    self.m_args = args
    self.m_categoryType = args.categoryType
    self.m_detailPanelId = args.detailPanelId

    self.m_wikiGroupShowDataList = WikiUtils.getWikiGroupShowDataList(args.categoryType)
    self:_RefreshTab()
end

WikiEquipSuitCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_args and lume.deepCopy(self.m_args) or {}
    local selectedGroupData = self.m_wikiGroupShowDataList and self.m_wikiGroupShowDataList[self.m_selectedIndex] or nil
    arg.wikiEntryShowData = nil
    arg.resumeState = {
        selectedGroupId = selectedGroupData and selectedGroupData.wikiGroupData.groupId or nil,
        focusTab = true,
    }
    return arg
end

WikiEquipSuitCtrl._RefreshTop = HL.Method() << function(self)

    local wikiTopArgs = {
        phase = self.m_phase,
        panelId = PANEL_ID,
        categoryType = self.m_categoryType,
    }
    self.view.top:InitWikiTop(wikiTopArgs)
end

WikiEquipSuitCtrl._PlayDecoAnim = HL.Method(HL.Boolean) << function(self, isIn)
    if self.m_phase then
        self.m_phase:PlayDecoAnim(isIn and "wiki_uideco_grouppanel_in" or "wiki_uideco_grouppanel_out")
    end
end



WikiEquipSuitCtrl.m_getTabCell = HL.Field(HL.Function)

WikiEquipSuitCtrl.m_selectedIndex = HL.Field(HL.Number) << 0

WikiEquipSuitCtrl.m_ignoreTabListAnim = HL.Field(HL.Boolean) << false

WikiEquipSuitCtrl._RefreshTab = HL.Method(HL.Opt(HL.Boolean)) << function(self)
    if self.m_getTabCell == nil then
        self.m_getTabCell = UIUtils.genCachedCellFunction(self.view.scrollListLeft)
        self.view.scrollListLeft.onUpdateCell:AddListener(function(object, csIndex)
            local tabCell = self.m_getTabCell(object)
            local wikiGroupShowData = self.m_wikiGroupShowDataList[LuaIndex(csIndex)]
            tabCell.titleNormalTxt.text = wikiGroupShowData.wikiGroupData.groupName
            tabCell.titleSelectTxt.text = wikiGroupShowData.wikiGroupData.groupName

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
            self:_NaviToSelectedTab()
        end)
    end

    local selectedIndex = 1
    local resumeState = self.m_args and self.m_args.resumeState or nil
    if resumeState and not string.isEmpty(resumeState.selectedGroupId) then
        for i, groupShowData in ipairs(self.m_wikiGroupShowDataList) do
            if resumeState.selectedGroupId == groupShowData.wikiGroupData.groupId then
                self.m_ignoreTabListAnim = true
                selectedIndex = i
                break
            end
        end
    elseif self.m_args.wikiEntryShowData then
        for i, groupShowData in ipairs(self.m_wikiGroupShowDataList) do
            if self.m_args.wikiEntryShowData.wikiGroupData.groupId == groupShowData.wikiGroupData.groupId then
                self.m_ignoreTabListAnim = true
                selectedIndex = i
                break
            end
        end
    end
    self.view.scrollListLeft:UpdateCount(#self.m_wikiGroupShowDataList, CSIndex(selectedIndex), false, false, self.m_ignoreTabListAnim)
    if self.m_ignoreTabListAnim then
        self:_NaviToSelectedTab(selectedIndex)
    end
    if self.m_args.wikiEntryShowData then

        self.view.leftNaviGroup:NaviToThisGroup()
        self.view.centerNaviGroup:ManuallyFocus()
    end
    self:_SetSelectedIndex(selectedIndex)
    if DeviceInfo.usingController and resumeState and resumeState.focusTab == true then
        self:_NaviToSelectedTab(selectedIndex)
    end
    self.m_ignoreTabListAnim = false
    self.m_selectedItemIndex = 1
    if self.m_args then
        self.m_args.resumeState = nil
    end
end

WikiEquipSuitCtrl._SetSelectedIndex = HL.Method(HL.Number) << function(self, selectedIndex)
    if self.m_selectedIndex == selectedIndex then
        return
    end
    self:_SetTabCellSelected(self.m_getTabCell(self.view.scrollListLeft:Get(CSIndex(self.m_selectedIndex))), false, true)
    self.m_selectedIndex = selectedIndex
    self:_SetTabCellSelected(self.m_getTabCell(self.view.scrollListLeft:Get(CSIndex(selectedIndex))), true, true)
    local wikiGroupShowData = self.m_wikiGroupShowDataList[selectedIndex]
    self:_RefreshRight(wikiGroupShowData)
    self.view.animationWrapper:Play(SWITCH_ANIM_NAME)
end

WikiEquipSuitCtrl._SetTabCellSelected = HL.Method(HL.Table, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, cell, isSelected, playAnim)
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





WikiEquipSuitCtrl.m_getSuitEffectCell = HL.Field(HL.Function)

WikiEquipSuitCtrl.m_getItemCell = HL.Field(HL.Function)

WikiEquipSuitCtrl.m_wikiEntryShowDataList = HL.Field(HL.Table)

WikiEquipSuitCtrl.m_suitData = HL.Field(HL.Userdata)

WikiEquipSuitCtrl.m_ignoreItemListAnim = HL.Field(HL.Boolean) << false

WikiEquipSuitCtrl.m_isBackNaviSelected = HL.Field(HL.Boolean) << false

WikiEquipSuitCtrl.m_selectedItemIndex = HL.Field(HL.Number) << 0

WikiEquipSuitCtrl._RefreshRight = HL.Method(HL.Table) << function(self, wikiGroupShowData)
    self.view.skillEffectTitleNode:PlayInAnimation()
    self.view.weaponTitleNode:PlayInAnimation()

    local hasSuit, suitDataList = Tables.equipSuitTable:TryGetValue(wikiGroupShowData.wikiGroupData.groupId)
    if hasSuit then
        self.m_suitData = suitDataList.list[0]
    end


    self.view.skillEffectNode.gameObject:SetActive(hasSuit)
    if self.m_suitData then
        self.view.txtDec.DescTxt:SetAndResolveTextStyle(CharInfoUtils.getSkillDesc(self.m_suitData.skillID, self.m_suitData.skillLv))
    end


    if self.m_suitData then
        self.view.suitIconImg:LoadSprite(UIConst.UI_SPRITE_EQUIPMENT_LOGO_BIG, self.m_suitData.suitLogoName)
    end


    self.m_wikiEntryShowDataList = wikiGroupShowData.wikiEntryShowDataList
    if not self.m_getItemCell then
        self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.scrollListWeapon)
        self.view.scrollListWeapon.onUpdateCell:AddListener(function(object, csIndex)

            local itemCell = self.m_getItemCell(object)
            local wikiEntryShowData = self.m_wikiEntryShowDataList[LuaIndex(csIndex)]
            itemCell:InitItem({ id = wikiEntryShowData.wikiEntryData.refItemId }, function()

                local args = {
                    categoryType = self.m_categoryType,
                    wikiEntryShowData = wikiEntryShowData,
                    wikiGroupShowDataList = self.m_wikiGroupShowDataList
                }
                self:PlayAnimationOutWithCallback(function()
                    self.m_phase:OpenCategory(self.m_categoryType, args)
                end)
            end)
            local entryId = wikiEntryShowData.wikiEntryData.id
            itemCell.view.redDot:InitRedDot("WikiEntry", entryId, nil, self.view.itemRedDotScrollRect)
            if itemCell.view.levelNode then
                itemCell.view.levelNode.gameObject:SetActive(false)
            end
            if WikiUtils.isWikiEntryUnread(entryId) then
                self.m_readWikiEntries[entryId] = true
            end
        end)
        self.view.scrollListWeapon.onGraduallyShowFinish:AddListener(function()
            self:_NaviToSelectedItem()
        end)
    end

    self:_MarkWikiEntryRead()

    local selectedIndex = 1
    if self.m_args.wikiEntryShowData then
        for i, entryShowData in ipairs(self.m_wikiEntryShowDataList) do
            if self.m_args.wikiEntryShowData.wikiEntryData.id == entryShowData.wikiEntryData.id then
                selectedIndex = i
                self.m_ignoreItemListAnim = true
                break
            end
        end
    end
    self.m_selectedItemIndex = selectedIndex
    self.view.scrollListWeapon:UpdateCount(#self.m_wikiEntryShowDataList, CSIndex(selectedIndex), false, false, self.m_ignoreItemListAnim)
    if self.m_ignoreItemListAnim then
        self:_NaviToSelectedItem()
    end
    self.m_ignoreItemListAnim = false
end





WikiEquipSuitCtrl.m_readWikiEntries = HL.Field(HL.Table)

WikiEquipSuitCtrl._MarkWikiEntryRead = HL.Method() << function(self)
    if self.m_readWikiEntries then
        local entryIdList = {}
        for entryId, _ in pairs(self.m_readWikiEntries) do
            table.insert(entryIdList, entryId)
        end
        GameInstance.player.wikiSystem:MarkWikiEntryRead(entryIdList)
        self.m_readWikiEntries = {}
    end
end

WikiEquipSuitCtrl._GetTabRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
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

WikiEquipSuitCtrl._GetItemRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
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





WikiEquipSuitCtrl.m_naviTabIndex = HL.Field(HL.Number) << 0

WikiEquipSuitCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    UIUtils.bindHyperlinkPopup(self, "wiki_group_equip_suit", self.view.inputGroup.groupId)
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
        if self.m_getItemCell then
            local itemCell = self.m_getItemCell(self.view.scrollListWeapon:Get(CSIndex(self.m_selectedItemIndex)))
            if itemCell then
                return itemCell.view.button
            end
        end
    end
end

WikiEquipSuitCtrl._NaviToSelectedItem = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    local selectedItemCell = self.m_getItemCell(self.view.scrollListWeapon:Get(CSIndex(self.m_selectedItemIndex)))
    if not self.m_isBackNaviSelected and selectedItemCell and self.m_args.wikiEntryShowData then
        self:SetNaviTarget(selectedItemCell.view.button)
        self.m_isBackNaviSelected = true
    end
end

WikiEquipSuitCtrl._NaviToSelectedTab = HL.Method(HL.Opt(HL.Number)) << function(self, selectedIndex)
    if not DeviceInfo.usingController then
        return
    end
    if not selectedIndex then
        selectedIndex = self.m_selectedIndex
    end
    local selectedTabCell = self.m_getTabCell(self.view.scrollListLeft:Get(CSIndex(selectedIndex)))
    if selectedTabCell then
        if self.m_args.wikiEntryShowData then
            self:SetNaviTarget(selectedTabCell.btn)
        else
            self:SetNaviTarget(selectedTabCell.btn)
        end
    end
end



HL.Commit(WikiEquipSuitCtrl)
