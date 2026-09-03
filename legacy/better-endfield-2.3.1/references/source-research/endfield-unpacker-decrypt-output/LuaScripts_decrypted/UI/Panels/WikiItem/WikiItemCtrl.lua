local wikiDetailBaseCtrl = require_ex('UI/Panels/WikiDetailBase/WikiDetailBaseCtrl')
local PANEL_ID = PanelId.WikiItem
WikiItemCtrl = HL.Class('WikiItemCtrl', wikiDetailBaseCtrl.WikiDetailBaseCtrl)



local SHOW_CRAFT_TREE_GROUP_TABLE = {
    wiki_group_item_nature = true,
    wiki_group_item_material = true,
    wiki_group_item_product = true,
    wiki_group_item_usable = true,
}



WikiItemCtrl.s_overrideMessages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_UNLOCK_TECH_TREE_UI] = '_FacOnUnlockTechTreeUI',
}



WikiItemCtrl.OnShow = HL.Override() << function(self)
    WikiItemCtrl.Super.OnShow(self)
    self:_PlayBgDecoAnim()
end

WikiItemCtrl.OnClose = HL.Override() << function(self)
    self:GameEventLogExit()
end

WikiItemCtrl.GetPanelId = HL.Override().Return(HL.Number) << function(self)
    return PANEL_ID
end

WikiItemCtrl._FacOnUnlockTechTreeUI = HL.Method(HL.Table) << function(self, args)
    self:_RefreshRight()
end

WikiItemCtrl._RefreshCenter = HL.Override() << function(self)
    WikiItemCtrl.Super._RefreshCenter(self)
    local _, itemData = Tables.itemTable:TryGetValue(self.m_wikiEntryShowData.wikiEntryData.refItemId)
    self.view.itemIcon.view.gameObject:SetActive(itemData ~= nil)
    if itemData then
        self.view.itemIcon:InitItemIcon(itemData.id, true)
    end
    if self.m_phase then
        self.m_phase:ActiveCommonSceneItem(true)
    end
end

WikiItemCtrl.m_isBtnInited = HL.Field(HL.Boolean) << false

WikiItemCtrl._RefreshRight = HL.Override() << function(self)
    local view = self.view.right
    if not self.m_isBtnInited then
        self.m_isBtnInited = true
        view.viewBtn.onClick:AddListener(function()
            self:_StartCoroutine(function()
                coroutine.step()
                self.view.right.naviGroup:ManuallyStopFocus()
                self.view.right.controllerFocusHintNode.gameObject:SetActive(true)
            end)
            self.m_phase:CreatePhasePanelItem(PanelId.WikiCraftingTree, {
                wikiEntryShowData = self.m_wikiEntryShowData,
                forceShowBackBtn = true,
            })
        end)
    end
    view.viewBtn.gameObject:SetActive(SHOW_CRAFT_TREE_GROUP_TABLE[self.m_wikiEntryShowData.wikiGroupData.groupId] == true)

    LayoutRebuilder.ForceRebuildLayoutImmediate(view.scrollRect.content)

    local itemId = self.m_wikiEntryShowData.wikiEntryData.refItemId
    view.itemObtainWaysForWiki:InitItemObtainWays(itemId, nil, self.m_itemTipsPosInfo, function(cell, craftCellView)
        self:_OnClickRightItemCell(cell, craftCellView)
    end)
    local lastCraftFirstSelectable, lastCraftNaviGroup = self:_InitItemObtainWaysController(view.itemObtainWaysForWiki)
    view.itemAsInput.gameObject:SetActive(true)
    view.itemAsInput:InitItemAsInput{
        itemId = itemId,
        itemTipsPosInfo = self.m_itemTipsPosInfo,
        onClickItem = function(cell, craftCellView)
            self:_OnClickRightItemCell(cell, craftCellView)
        end,
    }

    for i = 1, view.itemAsInput.m_obtainCells:GetCount() do
        local obtainCell = view.itemAsInput.m_obtainCells:GetItem(i)
        obtainCell.content.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.view.right.scrollRect:ScrollToNaviTarget(obtainCell.content)
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end
        obtainCell.content.useExplicitNaviSelect = true
        if lastCraftFirstSelectable and obtainCell.content.isActiveAndEnabled then
            obtainCell.content.banExplicitOnUp = false
            obtainCell.content:SetExplicitSelectOnUp(lastCraftFirstSelectable)
            lastCraftFirstSelectable.useExplicitNaviSelect = true
            lastCraftFirstSelectable.banExplicitOnLeft = true
            lastCraftFirstSelectable.banExplicitOnRight = true
            lastCraftFirstSelectable.banExplicitOnUp = true
            lastCraftFirstSelectable.banExplicitOnDown = false
            lastCraftFirstSelectable:SetExplicitSelectOnDown(obtainCell.content)
        else
            obtainCell.content.banExplicitOnUp = true
        end

        if obtainCell.content.isActiveAndEnabled and lastCraftNaviGroup then
            obtainCell.titleNaviGroup.naviPartnerOnUp:Clear()
            obtainCell.titleNaviGroup.naviPartnerOnDown:Clear()
            lastCraftNaviGroup.naviPartnerOnDown:Add(obtainCell.titleNaviGroup)
            obtainCell.titleNaviGroup.naviPartnerOnUp:Add(lastCraftNaviGroup)
            lastCraftNaviGroup = obtainCell.titleNaviGroup
        end

        local firstItemSelectable
        for j = 1, obtainCell.craftCells:GetCount() do
            local craftCell = obtainCell.craftCells:GetItem(j)
            if DeviceInfo.usingController then
                InputManagerInst:ToggleBinding(craftCell.pinBtn.view.pinToggle.toggleBindingId, false)
            end
            craftCell.pinKeyHint.gameObject:SetActive(false)

            for k = 1, craftCell.itemCells:GetCount() do
                local selectable = craftCell.itemCells:GetItem(k).view.button
                selectable.useExplicitNaviSelect = false
                selectable.onIsNaviTargetChanged = function(isTarget)
                    self:_OnRightItemIsNaviTargetChanged(isTarget, selectable, craftCell)
                end
                if firstItemSelectable == nil then
                    firstItemSelectable = selectable
                end
                if k == 1 then
                    lastCraftFirstSelectable = selectable
                end
            end

            craftCell.selectableNaviGroup.naviPartnerOnUp:Clear()
            craftCell.selectableNaviGroup.naviPartnerOnDown:Clear()
            if lastCraftNaviGroup then
                if j == 1 then
                    if not obtainCell.content.isActiveAndEnabled then
                        craftCell.selectableNaviGroup.naviPartnerOnUp:Add(lastCraftNaviGroup)
                        lastCraftNaviGroup.naviPartnerOnDown:Add(craftCell.selectableNaviGroup)
                    end
                else
                    craftCell.selectableNaviGroup.naviPartnerOnUp:Add(lastCraftNaviGroup)
                    lastCraftNaviGroup.naviPartnerOnDown:Add(craftCell.selectableNaviGroup)
                end
            end

            lastCraftNaviGroup = craftCell.selectableNaviGroup
        end
        if firstItemSelectable and obtainCell.content.isActiveAndEnabled then
            obtainCell.content.banExplicitOnDown = false
            obtainCell.content:SetExplicitSelectOnDown(firstItemSelectable)
            firstItemSelectable.useExplicitNaviSelect = true
            firstItemSelectable.banExplicitOnLeft = true
            firstItemSelectable.banExplicitOnRight = true
            firstItemSelectable.banExplicitOnDown = true
            firstItemSelectable.banExplicitOnUp = false
            firstItemSelectable:SetExplicitSelectOnUp(obtainCell.content)
        else
            obtainCell.content.banExplicitOnDown = true
        end
    end


    local isWeaponPotentialItem, itemData = Tables.weaponPotentialUpItemTable:TryGetValue(itemId)
    view.itemWeaponPotential.gameObject:SetActive(isWeaponPotentialItem)
    if isWeaponPotentialItem then
        view.itemWeaponPotential.itemCellCache = view.itemWeaponPotential.itemCellCache or
            UIUtils.genCellCache(view.itemWeaponPotential.itemCell)

        view.itemWeaponPotential.itemCellCache:Refresh(#itemData.weaponIds, function(cell, index)
            cell:InitItem({id = itemData.weaponIds[CSIndex(index)]}, function(itemBundle)
                self:_OnClickRightItemCell(cell)
            end)
            cell.view.button.onIsNaviTargetChanged = function(isTarget)
                self:_OnRightItemIsNaviTargetChanged(isTarget, cell.view.button)
            end
            if self.m_itemTipsPosInfo then
                cell:SetExtraInfo(self.m_itemTipsPosInfo)
            end
        end)
        view.itemAsInput.gameObject:SetActive(false)
    end


    if view.giftFeatureTagsNode:InitGiftFeatureTagsNode(itemId) then
        view.itemAsInput.gameObject:SetActive(false)
    end

    local isFocusEnabled = view.itemAsInput.m_obtainCells:GetCount() > 0 or
        ((view.itemObtainWaysForWiki.m_obtainCells:GetCount() > 0 or view.itemObtainWaysForWiki.view.emptyNode.gameObject.activeSelf) and
            view.itemObtainWaysForWiki.view.gameObject.activeSelf) or
        isWeaponPotentialItem
    view.naviGroup.enabled = isFocusEnabled
    self.view.right.controllerFocusHintNode.gameObject:SetActive(isFocusEnabled)
end



WikiItemCtrl._PlayBgDecoAnim = HL.Method() << function(self)
    if self.m_phase then
        self.m_phase:ActiveCommonSceneItem(true)
        self.m_phase:PlayDecoAnim("wiki_uideco_grouptocommonpanel")
    end
end

HL.Commit(WikiItemCtrl)