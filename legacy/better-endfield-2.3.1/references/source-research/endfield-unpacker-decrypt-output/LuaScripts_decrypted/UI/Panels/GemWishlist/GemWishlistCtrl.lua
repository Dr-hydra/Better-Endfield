local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GemWishlist
local PHASE_ID = PhaseId.GemWishlist



































GemWishlistCtrl = HL.Class('GemWishlistCtrl', uiCtrl.UICtrl)


local WeaponState = {
    Init = 1,
    Max = 2,
    Gem = 3,
}

local SAVE_CLICK_CD = 1



GemWishlistCtrl.m_getCellFunc = HL.Field(HL.Function)



GemWishlistCtrl.m_allWeaponList = HL.Field(HL.Table)



GemWishlistCtrl.m_displayWeaponList = HL.Field(HL.Table)



GemWishlistCtrl.m_weaponInfoMap = HL.Field(HL.Table)



GemWishlistCtrl.m_selectedWeaponId = HL.Field(HL.String) << ""



GemWishlistCtrl.m_checkedWeapons = HL.Field(HL.Table)



GemWishlistCtrl.m_savedCheckedWeapons = HL.Field(HL.Table)



GemWishlistCtrl.m_filterConfigs = HL.Field(HL.Table)



GemWishlistCtrl.m_filterOptions = HL.Field(HL.Table)



GemWishlistCtrl.m_maxCheckCount = HL.Field(HL.Number) << 5



GemWishlistCtrl.m_haveInitNaviTarget = HL.Field(HL.Boolean) << false


GemWishlistCtrl.m_toggleCheckBindingId = HL.Field(HL.Number) << -1



GemWishlistCtrl.m_curWeaponState = HL.Field(HL.Number) << WeaponState.Init



GemWishlistCtrl.m_nextSaveClickTime = HL.Field(HL.Number) << 0






GemWishlistCtrl.s_messages = HL.StaticField(HL.Table) << {
}





GemWishlistCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData()


    local recoverState = arg and arg.displayWeaponIds and arg or nil
    if recoverState then
        self.m_checkedWeapons = recoverState.checkedWeapons or {}
        self.m_filterConfigs = recoverState.filterConfigs or {}
        self.m_selectedWeaponId = recoverState.selectedWeaponId or ""
        local displayList = {}
        for _, id in ipairs(recoverState.displayWeaponIds) do
            local info = self.m_weaponInfoMap[id]
            if info then
                table.insert(displayList, info)
            end
        end
        if #displayList > 0 then
            self.m_displayWeaponList = displayList
        end
        local ws = recoverState.weaponState
        if type(ws) == "number" and ws >= WeaponState.Init and ws <= WeaponState.Gem then
            self.m_curWeaponState = ws
        end
    end

    self:_BindUI()

    if recoverState then
        self:_SyncSelectionAfterListChange()
    else
        self:_RefreshUI()
    end
end





GemWishlistCtrl.CheckAllWeapons = HL.Method() << function(self)
    for _, info in ipairs(self.m_displayWeaponList) do
        self.m_checkedWeapons[info.id] = true
    end
    self.view.contentNode.scrollList:UpdateCount(#self.m_displayWeaponList, false)
    self:_RefreshSelectCountTxt()
    self:_RefreshItemInfo()
    self:_RefreshControllerToggleHint()
end








GemWishlistCtrl._BindUI = HL.Method() << function(self)
    self.view.commonTopTitleNode.btnClose.onClick:RemoveAllListeners()
    self.view.commonTopTitleNode.btnClose.onClick:AddListener(function()
        self:_TryClose()
    end)


    self.view.commonTopTitleNode.commonMoreToggle:InitCommonToggleGroup({
        toggleDataList = {
            { name = Language.ui_CharInfo_init },
            { name = Language.ui_CharInfo_max_level },
            { name = Language.ui_WikiWeapon_gem_equiped },
        },
        onToggleIsOn = function(index)
            self:_RefreshWeaponState(index)
        end,
        defaultIndex = self.m_curWeaponState,
        defaultNotCall = true,
    })


    self.view.contentNode.itemInfoNode.wikiBtn.onClick:RemoveAllListeners()
    self.view.contentNode.itemInfoNode.wikiBtn.onClick:AddListener(function()
        if self.m_selectedWeaponId and self.m_selectedWeaponId ~= "" then
            Notify(MessageConst.SHOW_WIKI_ENTRY, { itemId = self.m_selectedWeaponId })
        end
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.contentNode.scrollList)
    self.view.contentNode.scrollList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getCellFunc(obj)
        self:_SetupCell(cell, index + 1)

        if not self.m_haveInitNaviTarget and DeviceInfo.usingController then
            self.m_haveInitNaviTarget = true
            self:SetNaviTarget(cell.itemBig.view.button)
        end
    end)

    self.m_filterOptions = FilterUtils.generateConfig_DEPOT_WEAPON()

    for _, group in ipairs(self.m_filterOptions) do
        if group.tags then
            for i = #group.tags, 1, -1 do
                local tag = group.tags[i]
                if tag.groupType == "WeaponRarity" and tag.param == 3 then
                    table.remove(group.tags, i)
                end
            end
        end
    end
    self.view.bottomNode.filterBtn:InitFilterBtn({
        tagGroups = self.m_filterOptions,
        selectedTags = self.m_filterConfigs or {},
        onConfirm = function(tags)
            self.m_filterConfigs = tags
            self:_BuildDisplayList()
            self:_SyncSelectionAfterListChange()
        end,
        getResultCount = function(tags)
            return self:_GetFilterResultCount(tags)
        end,
    })

    self.view.bottomNode.cancelSelectBtn.onClick:RemoveAllListeners()
    self.view.bottomNode.cancelSelectBtn.onClick:AddListener(function()
        self:_OnReset()
    end)

    self.view.bottomNode.saveBtn.onClick:RemoveAllListeners()
    self.view.bottomNode.saveBtn.onClick:AddListener(function()
        self:_OnSave()
    end)

    self.view.bottomNode.functionBtn.onClick:RemoveAllListeners()
    self.view.bottomNode.functionBtn.onClick:AddListener(function()
        self:_OnFillUpClick()
    end)

    if DeviceInfo.usingController then
        self.m_toggleCheckBindingId = self:BindInputPlayerAction("gem_wishlist_select_cell", function()
            self:_OnControllerToggleCheck()
        end, self.view.inputGroup.groupId)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self:_RefreshControllerToggleHint()
end




GemWishlistCtrl._InitData = HL.Method() << function(self)
    self.m_maxCheckCount = self.view.config.MAX_SELECT_NUM

    self.m_checkedWeapons = {}
    self.m_savedCheckedWeapons = {}
    self.m_filterConfigs = {}
    self.m_allWeaponList = {}
    self.m_weaponInfoMap = {}

    for weaponId, weaponCfg in pairs(Tables.weaponBasicTable) do
        local hasValue, itemCfg = Tables.itemTable:TryGetValue(weaponId)

        if hasValue and itemCfg.rarity > 3 then
            local weaponItemInfo = {
                id = weaponId,
                count = 0,
                forceHidePotentialStar = true,
                isEquipped = false,
                isOwned = false,
                equippedSort = 0,
                ownedSort = 0,
                rarity = itemCfg.rarity,
                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
                weaponType = weaponCfg.weaponType,
            }
            table.insert(self.m_allWeaponList, weaponItemInfo)
            self.m_weaponInfoMap[weaponId] = weaponItemInfo
        end
    end

    local weaponDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Weapon]:GetOrFallback(Utils.getCurrentScope())
    local weaponInstDict = weaponDepot.instItems
    for instId, instItemBundle in cs_pairs(weaponInstDict) do
        local weaponItemInfo = self.m_weaponInfoMap[instItemBundle.id]
        if weaponItemInfo then
            weaponItemInfo.isOwned = true
            weaponItemInfo.ownedSort = 1
            local equippedCardInstId = instItemBundle.instData.equippedCharServerId
            local isEquipped = equippedCardInstId and equippedCardInstId > 0
            if isEquipped then
                weaponItemInfo.isEquipped = true
                weaponItemInfo.equippedSort = 1
            end
        end
    end


    table.sort(self.m_allWeaponList, Utils.genSortFunction({ "rarity", "equippedSort", "ownedSort", "sortId1", "sortId2", "id" }))

    local serverWishList = GameInstance.player.inventory.weaponGemWishList
    for _, weaponId in pairs(serverWishList) do
        if self.m_weaponInfoMap[weaponId] then
            self.m_checkedWeapons[weaponId] = true
            self.m_savedCheckedWeapons[weaponId] = true
        end
    end

    if #self.m_allWeaponList > 0 then
        self.m_selectedWeaponId = self.m_allWeaponList[1].id
    end

    self:_BuildDisplayList()
end




GemWishlistCtrl._BuildDisplayList = HL.Method() << function(self)
    local hasFilter = self.m_filterConfigs and next(self.m_filterConfigs)
    if not hasFilter then
        self.m_displayWeaponList = self.m_allWeaponList
        return
    end

    local displayList = {}
    for _, info in ipairs(self.m_allWeaponList) do
        if self.m_checkedWeapons[info.id] then
            table.insert(displayList, info)
        elseif FilterUtils.checkIfPassFilter(info, self.m_filterConfigs) then
            table.insert(displayList, info)
        end
    end
    self.m_displayWeaponList = displayList
end




GemWishlistCtrl._RefreshUI = HL.Method() << function(self)
    self.view.contentNode.scrollList:UpdateCount(#self.m_displayWeaponList)
    self:_RefreshSelectCountTxt()
    self:_RefreshItemInfo()
    self:_RefreshControllerToggleHint()
end




GemWishlistCtrl._SyncSelectionAfterListChange = HL.Method() << function(self)
    local targetIndex = nil
    for i, info in ipairs(self.m_displayWeaponList) do
        if info.id == self.m_selectedWeaponId then
            targetIndex = i
            break
        end
    end

    if not targetIndex then
        if #self.m_displayWeaponList > 0 then
            self.m_selectedWeaponId = self.m_displayWeaponList[1].id
            targetIndex = 1
        else
            self.m_selectedWeaponId = ""
        end
    end

    self:_RefreshUI()

    if targetIndex then
        self.view.contentNode.scrollList:ScrollToIndex(CSIndex(targetIndex), true)
        if DeviceInfo.usingController then
            local cell = self.m_getCellFunc(targetIndex)
            if cell then
                self:SetNaviTarget(cell.itemBig.view.button)
            end
        end
    end
end






GemWishlistCtrl._SetupCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_displayWeaponList[luaIndex]
    if not info then
        return
    end

    cell.itemBig:InitItem(info, function()
        self:_OnClickWeapon(luaIndex)
    end)

    if info.isEquipped then
        cell.extraTag:SetState("Equipped")
    elseif info.isOwned then
        cell.extraTag:SetState("Owned")
    else
        cell.extraTag:SetState("Empty")
    end

    local isSelected = info.id == self.m_selectedWeaponId
    cell.itemBig:SetSelected(isSelected)

    local isChecked = self.m_checkedWeapons[info.id] == true
    cell.multiSelectMark.gameObject:SetActive(isChecked)
end





GemWishlistCtrl._OnClickWeapon = HL.Method(HL.Number) << function(self, luaIndex)
    local info = self.m_displayWeaponList[luaIndex]
    if not info then
        return
    end

    self.m_selectedWeaponId = info.id

    if DeviceInfo.usingController then
        self.view.contentNode.scrollList:UpdateCount(#self.m_displayWeaponList, false)
        self:_RefreshItemInfo()
        self:_RefreshControllerToggleHint()
        return
    end

    if self.m_checkedWeapons[info.id] then
        self.m_checkedWeapons[info.id] = nil
    else
        if self:_GetCheckedCount() >= self.m_maxCheckCount then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_WISHLIST_MAX_TOAST)
        else
            self.m_checkedWeapons[info.id] = true
        end
    end

    self.view.contentNode.scrollList:UpdateCount(#self.m_displayWeaponList, false)
    self:_RefreshSelectCountTxt()
    self:_RefreshItemInfo()
    self:_RefreshControllerToggleHint()
end




GemWishlistCtrl._RefreshItemInfo = HL.Method() << function(self)
    local node = self.view.contentNode.itemInfoNode
    if not self.m_selectedWeaponId or self.m_selectedWeaponId == "" then
        node.gameObject:SetActive(false)
        return
    end
    node.gameObject:SetActive(true)
    UIUtils.displayItemBasicInfos(node, self.loader, self.m_selectedWeaponId, nil)
    node.itemDescNode:InitItemDescNode(self.m_selectedWeaponId)
    node.stateCtrl:SetState("weapon")
    self:_RefreshWeaponState(self.m_curWeaponState)
    node.itemObtainWays:InitItemObtainWays(self.m_selectedWeaponId, nil)
    node.lockToggle.gameObject:SetActive(false)
    node.trashToggle.gameObject:SetActive(false)
    node.wikiBtn.gameObject:SetActive(WikiUtils.canShowWikiEntry(self.m_selectedWeaponId))
end






GemWishlistCtrl._GetWeaponShowData = HL.Method(HL.String, HL.Number).Return(HL.Table) << function(self, templateId, weaponState)
    local maxLevel, initMaxLevel, breakThroughCount, maxBreakthroughLevel, maxRefineLevel = 0, 0, 0, 0, 0

    local _, weaponBasicData = Tables.weaponBasicTable:TryGetValue(templateId)
    if weaponBasicData then
        maxLevel = weaponBasicData.maxLv
        local _, weaponBreakThroughDetailList = Tables.weaponBreakThroughTemplateTable:TryGetValue(weaponBasicData.breakthroughTemplateId)
        if weaponBreakThroughDetailList then
            breakThroughCount = #weaponBreakThroughDetailList.list
            if breakThroughCount > 1 then
                initMaxLevel = weaponBreakThroughDetailList.list[1].breakthroughLv
                maxBreakthroughLevel = breakThroughCount - 1
            end
        end
        local _, weaponTalentDetailList = Tables.weaponTalentTemplateTable:TryGetValue(weaponBasicData.talentTemplateId)
        if weaponTalentDetailList then
            maxRefineLevel = #weaponTalentDetailList.list
        end
    end

    local isMaxLevel = weaponState ~= WeaponState.Init
    return {
        templateId = templateId,
        level = isMaxLevel and maxLevel or 1,
        maxLevel = isMaxLevel and maxLevel or initMaxLevel,
        breakthroughLevel = isMaxLevel and maxBreakthroughLevel or 0,
        maxBreakthroughLevel = maxBreakthroughLevel,
        refineLevel = isMaxLevel and maxRefineLevel or 0,
    }
end





GemWishlistCtrl._RefreshWeaponState = HL.Method(HL.Number) << function(self, weaponState)
    self.m_curWeaponState = weaponState
    if not self.m_selectedWeaponId or self.m_selectedWeaponId == "" then
        return
    end

    local node = self.view.contentNode.itemInfoNode
    local templateId = self.m_selectedWeaponId
    local itemData = Tables.itemTable[templateId]
    local showData = self:_GetWeaponShowData(templateId, weaponState)
    local isMax = weaponState ~= WeaponState.Init
    local isGemMax = weaponState == WeaponState.Gem

    node.starGroup:InitStarGroup(itemData.rarity)
    node.potentialStar:InitWeaponPotentialStar(showData.refineLevel)
    if node.potentialStar.view.breakthroughBg then
        node.potentialStar.view.breakthroughBg.gameObject:SetActive(not isMax)
    end
    node.tipWeaponLevelNode:InitTipWeaponLevelNodeNoInst(showData.level, showData.maxLevel, showData.breakthroughLevel, showData.maxBreakthroughLevel)
    node.weaponAttributeNode:InitWeaponAttributeNodeByTemplateId(templateId, isMax)
    node.weaponSkillNode:InitWeaponSkillNodeByTemplateId(templateId, showData.breakthroughLevel, showData.refineLevel, isGemMax)

    local gemInst
    if isGemMax then
        gemInst = CS.Beyond.Gameplay.InventorySystem.CreateWeaponPerfectGemInst(templateId)
    end
    node.weaponGemSlimNode:InitWeaponGemSlimeNodeByInst(gemInst)

    if node.equippedNode then
        node.equippedNode.gameObject:SetActive(false)
    end
end




GemWishlistCtrl._RefreshSelectCountTxt = HL.Method() << function(self)
    self.view.bottomNode.selectCountTxt.text = self:_GetCheckedCount() .. "/" .. self.m_maxCheckCount
end


GemWishlistCtrl._RefreshControllerToggleHint = HL.Method() << function(self)
    if not DeviceInfo.usingController or self.m_toggleCheckBindingId < 0 then
        return
    end

    local bindingText = ""
    if self.m_selectedWeaponId and self.m_selectedWeaponId ~= "" then
        bindingText = self.m_checkedWeapons[self.m_selectedWeaponId]
            and Language.key_hint_common_unselect
            or Language.key_hint_common_select
    end
    InputManagerInst:SetBindingText(self.m_toggleCheckBindingId, bindingText)
end




GemWishlistCtrl._GetCheckedCount = HL.Method().Return(HL.Number) << function(self)
    local count = 0
    for _ in pairs(self.m_checkedWeapons) do
        count = count + 1
    end
    return count
end





GemWishlistCtrl._GetFilterResultCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    if not tags or not next(tags) then
        return #self.m_allWeaponList
    end
    local count = 0
    for _, info in ipairs(self.m_allWeaponList) do
        if self.m_checkedWeapons[info.id] or FilterUtils.checkIfPassFilter(info, tags) then
            count = count + 1
        end
    end
    return count
end




GemWishlistCtrl._IsDirty = HL.Method().Return(HL.Boolean) << function(self)
    local savedCount = 0
    for id in pairs(self.m_savedCheckedWeapons) do
        savedCount = savedCount + 1
        if not self.m_checkedWeapons[id] then
            return true
        end
    end
    return savedCount ~= self:_GetCheckedCount()
end




GemWishlistCtrl._TryClose = HL.Method() << function(self)
    if not self:_IsDirty() then
        PhaseManager:PopPhase(PhaseId.GemWishlist)
        return
    end
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_GEM_WISHLIST_QUIT_NO_SAVE_CONFIRM,
        onConfirm = function()
            PhaseManager:PopPhase(PhaseId.GemWishlist)
        end,
    })
end




GemWishlistCtrl._OnSave = HL.Method() << function(self)
    local curTime = CS.UnityEngine.Time.unscaledTime
    if curTime < self.m_nextSaveClickTime then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TOAST_SWITCH_STRANGER_CD)
        return
    end
    self.m_nextSaveClickTime = curTime + SAVE_CLICK_CD

    local weaponIds = {}
    for id in pairs(self.m_checkedWeapons) do
        table.insert(weaponIds, id)
    end
    GameInstance.player.inventory:SetWeaponGemWishList(weaponIds)
    self.m_savedCheckedWeapons = {}
    for id in pairs(self.m_checkedWeapons) do
        self.m_savedCheckedWeapons[id] = true
    end
    Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_WISHLIST_SAVE_TOAST)
end




GemWishlistCtrl._OnReset = HL.Method() << function(self)
    self.m_checkedWeapons = {}
    self.view.contentNode.scrollList:UpdateCount(#self.m_displayWeaponList, false)
    self:_RefreshSelectCountTxt()
    self:_RefreshItemInfo()
    self:_RefreshControllerToggleHint()
end




GemWishlistCtrl._OnFillUpClick = HL.Method() << function(self)
    local checkedCount = self:_GetCheckedCount()
    if checkedCount >= self.m_maxCheckCount then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_WISHLIST_MAX_TOAST)
        return
    end

    for _, info in ipairs(self.m_displayWeaponList) do
        if checkedCount >= self.m_maxCheckCount then
            break
        end
        if not self.m_checkedWeapons[info.id] then
            self.m_checkedWeapons[info.id] = true
            checkedCount = checkedCount + 1
        end
    end

    self.view.contentNode.scrollList:UpdateCount(#self.m_displayWeaponList, false)
    self:_RefreshSelectCountTxt()
    self:_RefreshControllerToggleHint()
end




GemWishlistCtrl._OnControllerToggleCheck = HL.Method() << function(self)
    if not self.m_selectedWeaponId or self.m_selectedWeaponId == "" then
        return
    end

    if self.m_checkedWeapons[self.m_selectedWeaponId] then
        self.m_checkedWeapons[self.m_selectedWeaponId] = nil
    else
        if self:_GetCheckedCount() >= self.m_maxCheckCount then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_WISHLIST_MAX_TOAST)
            return
        end
        self.m_checkedWeapons[self.m_selectedWeaponId] = true
    end

    self.view.contentNode.scrollList:UpdateCount(#self.m_displayWeaponList, false)
    self:_RefreshSelectCountTxt()
    self:_RefreshControllerToggleHint()
end



GemWishlistCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local displayWeaponIds = {}
    for _, info in ipairs(self.m_displayWeaponList) do
        table.insert(displayWeaponIds, info.id)
    end
    return {
        selectedWeaponId = self.m_selectedWeaponId,
        checkedWeapons = lume.deepCopy(self.m_checkedWeapons),
        filterConfigs = lume.deepCopy(self.m_filterConfigs),
        displayWeaponIds = displayWeaponIds,
        weaponState = self.m_curWeaponState,
    }
end



HL.Commit(GemWishlistCtrl)
