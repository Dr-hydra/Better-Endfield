local ItemType2DepotConfig = {
    [GEnums.ItemValuableDepotType.SpecialItem] = {
        infoProcessFuncName = "processItemDefault",
        isUnlocked = true,
        getSortOptions = function()
            return {
                {
                    name = Language.LUA_DEPOT_SORT_OPTION_DEFAULT,
                    keys = { "sortId1", "sortId2", "rarity", "id" },
                },
                {
                    name = Language.LUA_DEPOT_SORT_OPTION_RARITY,
                    keys = { "rarity", "sortId1", "sortId2", "id" },
                },
            }
        end,
        isNormalDestroy = true,
        infoStateName = "default",
    },
    [GEnums.ItemValuableDepotType.CommercialItem] = {
        infoProcessFuncName = "processItemDefault",
        isUnlocked = true,
        getSortOptions = function()
            return {
                {
                    name = Language.LUA_DEPOT_SORT_OPTION_DEFAULT,
                    keys = { "sortId1", "sortId2", "rarity", "id" },
                },
                {
                    name = Language.LUA_DEPOT_SORT_OPTION_RARITY,
                    keys = { "rarity", "sortId1", "sortId2", "id" },
                },
            }
        end,
        extraDisplayInfoFuncName = "displayCommercialItemInfo",
        infoStateName = "default",
        tabRedDotName = "ValuableDepotTabCommercialItem",
    },
    [GEnums.ItemValuableDepotType.MissionItem] = {
        infoProcessFuncName = "processItemDefault",
        isUnlocked = true,
        getSortOptions = function()
            return {
                {
                    name = Language.LUA_DEPOT_SORT_OPTION_DEFAULT,
                    keys = { "newOrder", "sortId1", "sortId2", "id" },
                }
            }
        end,
        infoStateName = "default",
    },
    [GEnums.ItemValuableDepotType.Weapon] = {
        infoProcessFuncName = "processWeapon",
        systemUnlockType = GEnums.UnlockSystemType.Weapon,
        getSortOptions = function()
            return UIConst.WEAPON_SORT_OPTION
        end,
        contentFilterOptionFuncName = "generateConfig_DEPOT_WEAPON",
        destroyFilterOptionFuncName = "generateConfig_DEPOT_WEAPON_DESTROY",
        extraDisplayInfoFuncName = "displayWeaponInfo",
        infoStateName = "weapon",
        isWeaponDestroy = true,
    },
    [GEnums.ItemValuableDepotType.WeaponGem] = {
        infoProcessFuncName = "processWeaponGem",
        systemUnlockType = GEnums.UnlockSystemType.Weapon,
        getSortOptions = function()

            return {
                {
                    name = Language.LUA_DEPOT_SORT_OPTION_RARITY,
                    keys = { "gemPerfectMatchSort", "isEquippedSort", "lockedIndex", "rarity", "sortId1", "sortId2", "id", "instId" },
                },
            }
        end,
        contentFilterOptionFuncName = "generateConfig_DEPOT_GEM",
        extraDisplayInfoFuncName = "displayWeaponGemInfo",
        destroyFilterOptionFuncName = "generateConfig_DEPOT_GEM_DESTROY",
        infoStateName = "weaponGem",
        isGemDestroy = true,
    },
    [GEnums.ItemValuableDepotType.Equip] = {
        infoProcessFuncName = "processEquip",
        systemUnlockType = GEnums.UnlockSystemType.Equip,
        getSortOptions = function()
            return {
                {
                    name = Language.LUA_DEPOT_SORT_OPTION_RARITY,
                    keys = { "rarity", "minWearLv", "equipEnhanceLevel", "sortId1", "sortId2", "id" },
                },
            }
        end,
        contentFilterOptionFuncName = "generateConfig_DEPOT_EQUIP",
        destroyFilterOptionFuncName = "generateConfig_DEPOT_EQUIP_DESTROY",
        isEquipDestroy = true,
        extraDisplayInfoFuncName = "displayEquipInfo",
        infoStateName = "equip",
    },
}
local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ValuableDepot
ValuableDepotCtrl = HL.Class('ValuableDepotCtrl', uiCtrl.UICtrl)


local inventorySystem = GameInstance.player.inventory







ValuableDepotCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_VALUABLE_DEPOT_CHANGED] = 'OnValuableDepotChanged',
    [MessageConst.ON_ITEM_LOCKED_STATE_CHANGED] = 'OnItemLockedStateChanged',
    [MessageConst.ON_EQUIP_RECYCLE] = 'OnEquipRecycle',
    [MessageConst.ON_GEM_DISMANTLE] = 'OnGemDismantle',
    [MessageConst.ON_WEAPON_GEM_WISH_LIST_CHANGED] = 'OnWeaponGemWishListChanged',
    [MessageConst.ON_WEAPON_RECYCLE] = 'OnWeaponRecycle',
    [MessageConst.ON_WEAPON_BATCH_LOCK] = 'OnWeaponBatchLock',
    [MessageConst.ON_LT_ITEM_EXPIRE] = 'OnLTItemExpire',
    [MessageConst.ON_LT_ITEM_EXPIRE_CONFIRM_RSP] = 'ShowLTItemExpirePopup',
    [MessageConst.ON_USE_ITEM] = 'OnUseItem',
    [MessageConst.ON_BATTLE_PASS_TICKET_REWARD] = 'OnBPTicketReward',
}

ValuableDepotCtrl.m_curTabIndex = HL.Field(HL.Number) << 1

ValuableDepotCtrl.m_curItemIndex = HL.Field(HL.Number) << 1

ValuableDepotCtrl.m_inDestroyMode = HL.Field(HL.Boolean) << false



ValuableDepotCtrl.m_suppressSelectedCell = HL.Field(HL.Boolean) << false

ValuableDepotCtrl.m_tabCells = HL.Field(HL.Forward('UIListCache'))

ValuableDepotCtrl.m_tabsInfo = HL.Field(HL.Table)

ValuableDepotCtrl.m_initDepotType = HL.Field(HL.Any)

ValuableDepotCtrl.m_clearScreenKeyOnClose = HL.Field(HL.Number) << -1

ValuableDepotCtrl.m_shouldClearScreenOnOpen = HL.Field(HL.Boolean) << false

ValuableDepotCtrl.m_curTabAllItemList = HL.Field(HL.Table)

ValuableDepotCtrl.m_curShowItemList = HL.Field(HL.Table)

ValuableDepotCtrl.m_curShowCount = HL.Field(HL.Number) << 0

ValuableDepotCtrl.m_curContentFilterConfigs = HL.Field(HL.Table)

ValuableDepotCtrl.m_curDestroyFilterConfigs = HL.Field(HL.Table)

ValuableDepotCtrl.m_getItemCell = HL.Field(HL.Function)

ValuableDepotCtrl.m_selectItemInfoWhenHide = HL.Field(HL.Table)

ValuableDepotCtrl.m_selectTabInfoWhenHide = HL.Field(HL.Table)

ValuableDepotCtrl.m_oriPaddingBottom = HL.Field(HL.Number) << 0

ValuableDepotCtrl.m_getPreviewItemCell = HL.Field(HL.Function)

ValuableDepotCtrl.m_recoverState = HL.Field(HL.Table)

ValuableDepotCtrl.m_recoverSelectIndex = HL.Field(HL.Number) << -1





ValuableDepotCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local itemId, instId, subPanelArg = self:_ProcessCreateArg(arg)
    if self.m_shouldClearScreenOnOpen then
        self.m_clearScreenKeyOnClose = UIManager:ClearScreen({ PANEL_ID })
    end
    local ltItemExpirePopupArg = arg and arg.ltItemExpirePopupArg or nil
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.ValuableDepot)
    end)
    self:BindInputPlayerAction("common_open_valuable_depot", function()
        PhaseManager:PopPhase(PhaseId.ValuableDepot)
    end, self.view.btnClose.groupId)

    self.m_readItemIds = {}
    self.m_readItemInstIds = {}

    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.itemScrollList)
    self.view.itemScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getItemCell(obj), LuaIndex(csIndex))
    end)
    self.view.itemScrollList.onSelectedCell:AddListener(function(obj, csIndex)
        self:_OnClickItem(LuaIndex(csIndex), nil, true)
    end)
    self.view.itemScrollList.getCurSelectedIndex = function()
        return CSIndex(self.m_curItemIndex)
    end
    self.m_oriPaddingBottom = self.view.itemScrollList:GetPadding().bottom

    self.view.itemInfoNode.wikiBtn.onClick:AddListener(function()
        self:_ShowWiki()
    end)

    self.view.bottomNode.btnGemEnhance.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.GemEnhance)
    end)
    self.view.bottomNode.btnGemWishlist.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.GemWishlist)
    end)
    self.view.bottomNode.btnEquipTech.onClick:AddListener(function()
        local equipInstId = self.m_curItemIndex > 0 and self.m_curShowItemList[self.m_curItemIndex].instId or nil
        PhaseManager:OpenPhase(PhaseId.EquipTech, { isEnhance = true, equipInstId = equipInstId })
    end)

    self.m_tabCells = UIUtils.genCellCache(self.view.tabs.tabCell)

    self:_InitController()
    self:_InitDepotConfigs()
    self:_InitDestroyNode()

    self:_RefreshTabsInfo(itemId, instId)
    self:_ProcessSubPanelArg(subPanelArg)
    self:_TryRecoverLTItemExpirePopup(ltItemExpirePopupArg)
end

ValuableDepotCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self:CheckLTItemExpire()
end

ValuableDepotCtrl.OnShow = HL.Override() << function(self)
    if self.m_selectItemInfoWhenHide and self.m_selectTabInfoWhenHide then
        self:_RecollectItemBundles(self.m_selectTabInfoWhenHide.type)
        self:_RefreshTabsInfo(self.m_selectItemInfoWhenHide.id, self.m_selectItemInfoWhenHide.instId, true)
    end
end

ValuableDepotCtrl.OnHide = HL.Override() << function(self)
    local curSelectItemInfo = self.m_curShowItemList[self.m_curItemIndex]
    if curSelectItemInfo then
        self.m_selectItemInfoWhenHide = curSelectItemInfo
    end

    local curSelectTabInfo = self.m_tabsInfo[self.m_curTabIndex]
    if curSelectTabInfo then
        self.m_selectTabInfoWhenHide = curSelectTabInfo
    end

    if self.m_inDestroyMode then
        self:_ToggleDestroyMode(false, true)
    end
end

ValuableDepotCtrl.OnClose = HL.Override() << function(self)
    self:_ReadCurShowingItems()
    if self.m_clearScreenKeyOnClose and self.m_clearScreenKeyOnClose >= 0 then
        UIManager:RecoverScreen(self.m_clearScreenKeyOnClose)
        self.m_clearScreenKeyOnClose = -1
    end
end

ValuableDepotCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    self.view.destroyNode.numberSelector.view.keyHintLeft.gameObject:SetActive(active)
    self.view.destroyNode.numberSelector.view.keyHintRight.gameObject:SetActive(active)
end

ValuableDepotCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local sortSelectedIndex
    local sortIsIncremental
    local currentSelectedItem = self.m_curShowItemList and self.m_curShowItemList[self.m_curItemIndex] or nil
    local destroyState = self:_BuildDestroyRecoverState()
    local ltItemExpirePopupArg = self:_GetLTItemExpirePopupRecoverState()
    if self.view and self.view.bottomNode and self.view.bottomNode.sortNode then
        sortSelectedIndex = self.view.bottomNode.sortNode:GetCurSelectedIndex()
        sortIsIncremental = self.view.bottomNode.sortNode.isIncremental
    end
    return {
        tabIndex = self.m_curTabIndex,
        filterSelectedTags = lume.deepCopy(self.m_curContentFilterConfigs or {}),
        sortSelectedIndex = sortSelectedIndex,
        sortIsIncremental = sortIsIncremental,
        itemId = currentSelectedItem and currentSelectedItem.id or nil,
        instId = currentSelectedItem and currentSelectedItem.instId or nil,
        inDestroyMode = self.m_inDestroyMode,
        destroyState = destroyState,
        subPanelArg = self:_GetSubPanelArg(),
        ltItemExpirePopupArg = ltItemExpirePopupArg,
        waitRecoverSelectIndex = self.m_curItemIndex,
        shouldClearScreenOnOpen = self.m_shouldClearScreenOnOpen,
    }
end

ValuableDepotCtrl._BuildDestroyRecoverState = HL.Method().Return(HL.Table) << function(self)
    if not self.m_inDestroyMode then
        return {}
    end
    local destroyState = {
        selectedTags = lume.deepCopy(self.m_curDestroyFilterConfigs or {}),
        selectedItems = {},
        isExpandOpen = self.view.destroyNode.expandToggle.isOn == true,
        expandSelectedRealId = self.m_destroyCountItemRealId,
        filterPopupState = self:_BuildDestroyFilterPopupRecoverState(),
    }
    for tabIndex, infos in ipairs(self.m_destroyInfo or {}) do
        for _, info in pairs(infos) do
            table.insert(destroyState.selectedItems, {
                tabIndex = tabIndex,
                realId = info.realId,
                id = info.id,
                instId = info.instId,
                count = info.count,
                selectCount = info.selectCount,
            })
        end
    end
    table.sort(destroyState.selectedItems, Utils.genSortFunction({ "tabIndex", "realId" }, true))
    return destroyState
end

ValuableDepotCtrl._BuildDestroyFilterPopupRecoverState = HL.Method().Return(HL.Table) << function(self)
    if not self.m_inDestroyMode then
        return {}
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.CommonFilter)
    if not isOpen then
        return {}
    end
    return {
        isOpen = true,
        selectedTags = lume.deepCopy(ctrl.m_filterSelectedTags or {}),
    }
end

ValuableDepotCtrl._GetLTItemExpirePopupRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PhaseId.ValuableDepot then
        return
    end
    local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if not isOpen or not commonPopUpCtrl:IsShow() then
        return
    end
    local recoverArg = commonPopUpCtrl:GetCurPhaseStateArg()
    if not self:_IsLTItemExpirePopupArg(recoverArg) then
        return
    end
    return recoverArg
end

ValuableDepotCtrl._IsLTItemExpirePopupArg = HL.Method(HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, arg)
    if arg == nil or type(arg) ~= "table" or string.isEmpty(arg.content) then
        return false
    end
    return arg.content == Language.LUA_LIMIT_ITEM_EXPIRE_POPUP_TITLE
end

ValuableDepotCtrl._ProcessCreateArg = HL.Method(HL.Any).Return(HL.Opt(HL.String, HL.Any, HL.Any)) << function(self, arg)
    local itemId
    local instId
    local subPanelArg
    self.m_initDepotType = nil
    self.m_clearScreenKeyOnClose = -1
    self.m_shouldClearScreenOnOpen = false
    if type(arg) == "table" then
        local recoverTabIndex = arg.tabIndex
        self.m_curTabIndex = recoverTabIndex or 0
        self.m_initDepotType = arg.depotType
        self.m_clearScreenKeyOnClose = arg.clearScreenKey or -1
        self.m_shouldClearScreenOnOpen = arg.shouldClearScreenOnOpen == true
        subPanelArg = arg.subPanelArg
        self.m_recoverState = {
            tabIndex = recoverTabIndex,
            filterSelectedTags = lume.deepCopy(arg.filterSelectedTags or {}),
            sortSelectedIndex = arg.sortSelectedIndex,
            sortIsIncremental = arg.sortIsIncremental,
            itemId = arg.itemId,
            instId = arg.instId,
            inDestroyMode = arg.inDestroyMode == true,
            destroyState = lume.deepCopy(arg.destroyState or {}),
            waitRecoverSelectIndex = arg.waitRecoverSelectIndex or -1
        }
    else
        itemId = arg
        self.m_recoverState = nil
    end
    return itemId, instId, subPanelArg
end

ValuableDepotCtrl._GetSubPanelArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, ctrl = UIManager:IsOpen(PanelId.CommonBatchLock)
    if isOpen then
        local subPanelArg = ctrl:GetCurPhaseStateArg()
        if not subPanelArg then
            return
        end
        subPanelArg.isCommonBatchLock = true
        return subPanelArg
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.BattlePassWeaponCase)
    if isOpen then
        local subPanelArg = ctrl:GetCurPhaseStateArg()
        if not subPanelArg then
            return
        end
        subPanelArg.isBattlePassWeaponCase = true
        return subPanelArg
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.StaminaPotion)
    if isOpen then
        local subPanelArg = ctrl:GetCurPhaseStateArg()
        if not subPanelArg then
            return
        end
        subPanelArg.isStaminaPotion = true
        return subPanelArg
    end
    isOpen, ctrl = UIManager:IsOpen(PanelId.MapDetectPopUp)
    if isOpen then
        local subPanelArg = ctrl:GetCurPhaseStateArg()
        if not subPanelArg then
            return
        end
        subPanelArg.isMapDetectPopUp = true
        return subPanelArg
    end
end

ValuableDepotCtrl._ProcessSubPanelArg = HL.Method(HL.Opt(HL.Any)) << function(self, subPanelArg)
    if not subPanelArg then
        return
    end
    if subPanelArg.isCommonBatchLock then
        UIManager:Open(PanelId.CommonBatchLock, subPanelArg)
    elseif subPanelArg.isBattlePassWeaponCase then
        UIManager:Open(PanelId.BattlePassWeaponCase, subPanelArg)
    elseif subPanelArg.isStaminaPotion then
        UIManager:Open(PanelId.StaminaPotion, subPanelArg)
    elseif subPanelArg.isMapDetectPopUp then
        UIManager:Open(PanelId.MapDetectPopUp, subPanelArg)
    end
end

ValuableDepotCtrl._TryRecoverLTItemExpirePopup = HL.Method(HL.Opt(HL.Any)) << function(self, ltItemExpirePopupArg)
    if ltItemExpirePopupArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PhaseId.ValuableDepot then
        return
    end
    if not self:_IsLTItemExpirePopupArg(ltItemExpirePopupArg) then
        return
    end
    local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if isOpen and commonPopUpCtrl:IsShow() then
        return
    end
    self:Notify(MessageConst.SHOW_POP_UP, ltItemExpirePopupArg)
end





ValuableDepotCtrl._InitDepotConfigs = HL.Method() << function(self)
    for _, config in pairs(ItemType2DepotConfig) do
        if config.contentFilterOptionFuncName then
            config.contentFilterOptions = FilterUtils[config.contentFilterOptionFuncName]()
        end
        if config.destroyFilterOptionFuncName then
            config.destroyFilterOptions = FilterUtils[config.destroyFilterOptionFuncName]()
        end
    end
end

ValuableDepotCtrl._RefreshTabsInfo = HL.Method(HL.Opt(HL.String, HL.Any, HL.Boolean)) << function(self, itemId, instId, isFast)
    local tabInfos = {}
    for _, v in pairs(Tables.valuableDepot) do
        if not v.isHidden and self:_CheckIfTabUnlocked(v.type) then
            local depotConfig = ItemType2DepotConfig[v.type]
            table.insert(tabInfos, {
                type = v.type,
                data = v,
                name = v.name,
                sortId = v.sortId,
                icon = v.icon,
                redDot = depotConfig.tabRedDotName,
            })
        end
    end
    table.sort(tabInfos, Utils.genSortFunction({ "sortId" }, true))
    self.m_tabsInfo = tabInfos

    if itemId then
        local vType = Utils.getItemValuableDepotType(itemId)
        for k, v in ipairs(tabInfos) do
            if v.type == vType then
                self.m_curTabIndex = k
                break
            end
        end
    elseif self.m_initDepotType then
        for k, v in ipairs(tabInfos) do
            if v.type == self.m_initDepotType then
                self.m_curTabIndex = k
                if self.m_recoverState then
                    self.m_recoverState.tabIndex = k
                end
                break
            end
        end
        self.m_initDepotType = nil
    end

    if #tabInfos <= 0 then
        return
    end
    if self.m_curTabIndex < 1 or self.m_curTabIndex > #tabInfos then
        self.m_curTabIndex = 1
    end

    self.m_tabCells:Refresh(#tabInfos, function(cell, index)
        local info = tabInfos[index]
        UIUtils.setTabIcons(cell, UIConst.UI_SPRITE_INVENTORY, info.icon)
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.isOn = index == self.m_curTabIndex
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                if self.m_curTabIndex == index then
                    return
                end
                self:_ReadCurShowingItems()
                self:_OnClickTab(index)
            end
        end)
        cell.gameObject.name = "Tab-" .. info.type:GetHashCode()

        if string.isEmpty(info.redDot) then
            cell.redDot:InitRedDot("ValuableDepotTabCommon", info.type)
        else
            cell.redDot:InitRedDot(info.redDot)
        end
    end)
    self:_OnClickTab(self.m_curTabIndex, itemId, instId, isFast)
end

ValuableDepotCtrl._OnClickTab = HL.Method(HL.Number, HL.Opt(HL.String, HL.Any, HL.Boolean)) << function(self, index, itemId, instId, isFast)
    local info = self.m_tabsInfo[index]
    if info == nil then
        return
    end
    self.m_curTabIndex = index
    local recoverState
    if self.m_recoverState and self.m_recoverState.tabIndex == index then
        recoverState = self.m_recoverState
        self.m_recoverSelectIndex = self.m_recoverState.waitRecoverSelectIndex or -1
        self.m_recoverState = nil
    else
        self.m_recoverSelectIndex = -1
    end
    self.m_curContentFilterConfigs = recoverState and recoverState.filterSelectedTags or {}
    self.m_curDestroyFilterConfigs = recoverState and recoverState.destroyState and recoverState.destroyState.selectedTags or {}

    local depotConfig = ItemType2DepotConfig[info.type]
    self.view.bottomNode.filterBtn.gameObject:SetActive(
        depotConfig.contentFilterOptions and next(depotConfig.contentFilterOptions) ~= nil and not DeviceInfo.usingController)
    self.view.bottomNode.filterBtn:InitFilterBtn({
        tagGroups = depotConfig.contentFilterOptions,
        selectedTags = self.m_curContentFilterConfigs,
        onConfirm = function(tags)
            self.m_curContentFilterConfigs = tags
            self.m_curItemIndex = 1
            self:_ApplyFilter()
            self:_ApplySort(self.view.bottomNode.sortNode:GetCurSortData(), self.view.bottomNode.sortNode.isIncremental)
            self:_SetSelectedIndex()
            self:_RefreshItemList(true)
        end,
        getResultCount = function(tags)
            return self:_GetContentFilterResultCount(tags)
        end,
        sortNodeWidget = self.view.bottomNode.sortNode,
    })
    local sortSelectedCsIndex = 0
    if recoverState and type(recoverState.sortSelectedIndex) == "number" then
        sortSelectedCsIndex = CSIndex(recoverState.sortSelectedIndex)
    end
    local sortIsIncremental = recoverState and recoverState.sortIsIncremental or nil
    self.view.bottomNode.sortNode:InitSortNode(depotConfig.getSortOptions(), function(optData, isIncremental)
        self:_ApplySort(optData, isIncremental)
        self:_SetSelectedIndex()
        self:_RefreshItemList(true)
    end, sortSelectedCsIndex, sortIsIncremental, true, self.view.bottomNode.filterBtn)

    local isGemTab = info.type == GEnums.ItemValuableDepotType.WeaponGem
    local isGemEnhanceBtnVisible = isGemTab and Utils.isSystemUnlocked(GEnums.UnlockSystemType.GemEnhance)
    self.view.bottomNode.btnGemEnhance.gameObject:SetActive(isGemEnhanceBtnVisible)
    self.view.bottomNode.btnGemWishlist.gameObject:SetActive(isGemTab and PhaseManager:IsPhaseUnlocked(PhaseId.GemWishlist))
    local isEquipTechBtnVisible = info.type == GEnums.ItemValuableDepotType.Equip and
        Utils.isSystemUnlocked(GEnums.UnlockSystemType.EquipProduce) and
        Utils.isSystemUnlocked(GEnums.UnlockSystemType.EquipEnhance)
    self.view.bottomNode.btnEquipTech.gameObject:SetActive(isEquipTechBtnVisible)
    local isRecycleBtnVisible = depotConfig.isEquipDestroy or depotConfig.isGemDestroy or depotConfig.isWeaponDestroy
    self.view.bottomNode.desEquipBtn.gameObject:SetActive(isRecycleBtnVisible)
    self.view.bottomNode.destroyBtn.gameObject:SetActive(depotConfig.isNormalDestroy)

    self:_RecollectItemBundles(info.type)
    if self.m_inDestroyMode and (depotConfig.isWeaponDestroy or depotConfig.isGemDestroy) then
        self:_ApplyDestroySelectableFilter()
        self.m_pendingDestroyNaviFirst = DeviceInfo.usingController and self.m_curShowCount > 0
    end

    self.view.tabTitleTxt.text = info.name
    self.view.capacityTxt.text = string.format(Language.LUA_DEPOT_CAPACITY, #self.m_curTabAllItemList, info.data.gridLimit)

    local recoverScrollIndex = 0
    if self.m_inDestroyMode then
        self.m_curItemIndex = -1
    else
        if recoverState then
            self.m_curItemIndex = -1
            self:_RefreshItemInfo(true)
            self:_SetSelectedIndex(recoverState.itemId, recoverState.instId)
            recoverScrollIndex = self.m_curItemIndex
        else
            self:_SetSelectedIndex(itemId, instId)
        end
    end

    self:_RefreshItemList(true, true, isFast)
    self:_TryRecoverScrollToSelectedItem(recoverScrollIndex)
    self:_TryRecoverDestroyState(recoverState)
end

ValuableDepotCtrl._RecollectItemBundles = HL.Method(HL.Any) << function(self, itemType)
    local allItems = self:_GetAllItemBundlesInDepot(itemType)
    self.m_curTabAllItemList = allItems

    self:_ApplyFilter()
    self:_ApplySort(self.view.bottomNode.sortNode:GetCurSortData(), self.view.bottomNode.sortNode.isIncremental)
end

ValuableDepotCtrl._SetSelectedIndex = HL.Method(HL.Opt(HL.Any, HL.Any)) << function(self, itemId, instId)
    self.m_curItemIndex = math.min(1, self.m_curShowCount)
    if itemId then
        for k, v in ipairs(self.m_curShowItemList) do
            if v.id == itemId and (not instId or v.instId == instId) then
                self.m_curItemIndex = k
                break
            end
        end
    end
end

ValuableDepotCtrl._GetAllItemBundlesInDepot = HL.Method(HL.Opt(HL.Userdata, HL.Table)).Return(HL.Table) << function(self, depotType, rst)
    rst = rst or {}
    local depot = GameInstance.player.inventory.valuableDepots[depotType]:GetOrFallback(Utils.getCurrentScope())
    local depotConfig = ItemType2DepotConfig[depotType]
    local infoProcessFunc = FilterUtils[depotConfig.infoProcessFuncName]

    for id, bundle in cs_pairs(depot.normalItems) do
        local info = infoProcessFunc(id)
        if info then
            info.count = bundle.count
            table.insert(rst, info)
        end
    end
    for instId, bundle in cs_pairs(depot.instItems) do
        local info = infoProcessFunc(bundle.id, instId)
        if info then
            info.count = bundle.count
            table.insert(rst, info)
        end
    end
    return rst
end

ValuableDepotCtrl._ApplySort = HL.Method(HL.Table, HL.Boolean) << function(self, option, isIncremental)
    local curSelectItemInfo = self.m_curShowItemList[self.m_curItemIndex]
    table.sort(self.m_curTabAllItemList, Utils.genSortFunction(option.keys, isIncremental))
    table.sort(self.m_curShowItemList, Utils.genSortFunction(option.keys, isIncremental))
    for k, v in ipairs(self.m_curShowItemList) do
        if v == curSelectItemInfo then
            self.m_curItemIndex = k
            break
        end
    end
end

ValuableDepotCtrl._ApplyFilter = HL.Method() << function(self)
    local curTabAllItemList = self.m_curTabAllItemList
    local curFilterConfigs = self.m_curContentFilterConfigs

    if (not curFilterConfigs) or (not next(curFilterConfigs)) then
        self.m_curShowItemList = curTabAllItemList
        self.m_curShowCount = #curTabAllItemList
        return
    end

    local filteredItemList = {}
    for _, itemInfo in pairs(curTabAllItemList) do
        if FilterUtils.checkIfPassFilter(itemInfo, curFilterConfigs) then
            table.insert(filteredItemList, itemInfo)
        end
    end
    self.m_curShowItemList = filteredItemList
    self.m_curShowCount = #filteredItemList
end

ValuableDepotCtrl._ApplyDestroySelectableFilter = HL.Method() << function(self)
    local filteredItemList = {}

    for _, itemInfo in ipairs(self.m_curShowItemList) do
        if self:_ShouldShowInDestroyMode(itemInfo) then
            table.insert(filteredItemList, itemInfo)
        end
    end
    self.m_curShowItemList = filteredItemList
    self.m_curShowCount = #filteredItemList
end

ValuableDepotCtrl._ShouldShowInDestroyMode = HL.Method(HL.Table).Return(HL.Boolean) << function(self, itemInfo)
    local depotConfig = ItemType2DepotConfig[self.m_tabsInfo[self.m_curTabIndex].type]
    if depotConfig and depotConfig.isWeaponDestroy and itemInfo.data.type == GEnums.ItemType.Weapon then
        local inventory = GameInstance.player.inventory
        local scope = Utils.getCurrentScope()
        if inventory:IsEquipped(scope, itemInfo.id, itemInfo.instId) then
            return false
        end

        local weaponInst = itemInfo.instId and itemInfo.instId > 0 and CharInfoUtils.getWeaponByInstId(itemInfo.instId) or nil
        if not weaponInst then
            return false
        end

        local hasFedExp = (weaponInst.exp and weaponInst.exp > 0) or (weaponInst.weaponLv and weaponInst.weaponLv > 1)
        local hasGem = weaponInst.attachedGemInstId and weaponInst.attachedGemInstId > 0
        local hasPotentialUpgrade = weaponInst.refineLv and weaponInst.refineLv > 0
        local isSixStar = itemInfo.rarity and itemInfo.rarity >= 6
        return not (hasFedExp or hasGem or hasPotentialUpgrade or isSixStar)
    end
    if depotConfig and depotConfig.isGemDestroy and itemInfo.data.type == GEnums.ItemType.WeaponGem then
        local inventory = GameInstance.player.inventory
        local scope = Utils.getCurrentScope()
        if not inventory:CanDestroyItem(scope, itemInfo.id) or inventory:IsEquipped(scope, itemInfo.id, itemInfo.instId) then
            return false
        end

        local isMaxRarityGem = itemInfo.rarity and itemInfo.rarity >= 5
        if isMaxRarityGem then
            return false
        end

        local isPerfectMatch = UIUtils.getGemWishListPerfectMatch(itemInfo.instId)
        local isLocked = inventory:IsItemLocked(scope, itemInfo.id, itemInfo.instId)

        return not (isLocked and isPerfectMatch)
    end

    local isBlocked = self:_GetDestroyBlockToast(itemInfo)
    return not isBlocked
end

ValuableDepotCtrl._GetContentFilterResultCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    if not tags or not next(tags) then
        return
    end
    local count = 0
    for itemIndex, itemInfo in pairs(self.m_curTabAllItemList) do
        if FilterUtils.checkIfPassFilter(itemInfo, tags) then
            count = count + 1
        end
    end
    return count
end

ValuableDepotCtrl._RefreshItemList = HL.Method(HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean)) << function(self, noRead, setTop, isFast)
    logger.info("_RefreshItemList")
    local count = #self.m_curShowItemList
    local isEmpty = count == 0
    local pendingDestroyNaviFirst = self.m_pendingDestroyNaviFirst
    if isFast then
        self.view.itemScrollList:UpdateCount(count, self.m_curItemIndex, false, false, true)
    else
        self.view.itemScrollList:UpdateCount(count, setTop == true)
    end
    self.view.emptyNode.gameObject:SetActive(isEmpty)
    self.view.itemScrollList.gameObject:SetActive(not isEmpty)
    self.view.itemInfoNode.gameObject:SetActive(not isEmpty)
    if isEmpty then
        self.view.itemInfoNode.animation:SampleToOutAnimationEnd()
    else
        self.view.itemInfoNode.animation:SampleToInAnimationEnd()
    end
    if not isEmpty then
        if self.m_inDestroyMode then
            if pendingDestroyNaviFirst then
                self:_OnClickItem(1, true, true)
            else
                self:_OnClickItem(-1, true)
            end
        else
            self:_OnClickItem(self.m_curItemIndex, noRead)
        end
    end
    if DeviceInfo.usingController and isEmpty then
        self.view.itemListNaviGroup:SetLayerSelectedTarget(nil, false)
    end
end

ValuableDepotCtrl._TryRecoverScrollToSelectedItem = HL.Method(HL.Number) << function(self, recoverScrollIndex)
    if recoverScrollIndex <= 0 then
        return
    end
    local csIndex = CSIndex(recoverScrollIndex)
    self.view.itemScrollList:ScrollToIndex(csIndex, true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Center, true)
end

ValuableDepotCtrl._TryRecoverDestroyState = HL.Method(HL.Opt(HL.Table)) << function(self, recoverState)
    if not recoverState or not recoverState.inDestroyMode then
        return
    end

    local destroyState = recoverState.destroyState or {}
    local recoverDestroyTargetRealId = destroyState.expandSelectedRealId or ""
    self:_ToggleDestroyMode(true, false)
    if self.m_recoverSelectIndex > self.m_curShowCount then
        self.m_recoverSelectIndex = -1
    elseif self.m_recoverSelectIndex > 0 then
        self:_TryRecoverScrollToSelectedItem(self.m_recoverSelectIndex)
    end

    self.m_destroyInfo = {}
    for tabIndex = 1, #self.m_tabsInfo do
        self.m_destroyInfo[tabIndex] = {}
    end

    self.m_destroyCount = 0
    for _, info in ipairs(destroyState.selectedItems or {}) do
        local tabDestroyInfo = self.m_destroyInfo[info.tabIndex]
        if tabDestroyInfo then
            tabDestroyInfo[info.realId] = {
                realId = info.realId,
                id = info.id,
                instId = info.instId,
                count = info.count,
                selectCount = info.selectCount,
            }
            self.m_destroyCount = self.m_destroyCount + 1
        end
    end

    self:_UpdateDestroySelectTotalCount(true)
    for index = 1, self.view.itemScrollList.count do
        local cell = self.m_getItemCell(index)
        local info = cell and self.m_curShowItemList[index] or nil
        if cell and info then
            self:_UpdateItemBlockMask(cell, info)
            self:_UpdateItemCellDestroySelectPart(index, cell)
        end
    end

    self:_TryRecoverDestroySelectExpandState(destroyState)
    self:_TryRecoverDestroyFilterPopupState(destroyState)

    if not string.isEmpty(recoverDestroyTargetRealId) then
        self:_SetDestroyCountTarget(recoverDestroyTargetRealId)
    end
end

ValuableDepotCtrl._TryRecoverDestroySelectExpandState = HL.Method(HL.Opt(HL.Table)) << function(self, destroyState)
    local node = self.view.destroyNode
    local selectedItems = destroyState and destroyState.selectedItems or {}
    local shouldExpand = destroyState and destroyState.isExpandOpen == true and #selectedItems > 0
    self:_ToggleDestroySelectExpand(shouldExpand, true)
    node.expandToggle.isOn = shouldExpand
    if not shouldExpand then
        return
    end

    local targetIndex = 1

    local targetRealId = destroyState.expandSelectedRealId
    if not string.isEmpty(targetRealId) then
        for index, info in ipairs(self.m_destroyExpandItemList) do
            if info.realId == targetRealId then
                targetIndex = index
                break
            end
        end
    end

    local targetInfo = self.m_destroyExpandItemList[targetIndex]
    if not targetInfo then
        return
    end

    node.selectScrollList:ScrollToIndex(targetIndex, true)
    self:_SetDestroyCountTarget(targetInfo.realId)
    local cell = self.m_getExpandItemCell(targetIndex)
    if DeviceInfo.usingController and cell then
        self:SetNaviTarget(cell.view.button)
    end
end

ValuableDepotCtrl._TryRecoverDestroyFilterPopupState = HL.Method(HL.Opt(HL.Table)) << function(self, destroyState)
    local filterPopupState = destroyState and destroyState.filterPopupState or nil
    if not filterPopupState or not filterPopupState.isOpen then
        return
    end
    local info = self.m_tabsInfo[self.m_curTabIndex]
    local depotConfig = info and ItemType2DepotConfig[info.type] or nil
    if not depotConfig or not depotConfig.destroyFilterOptions then
        return
    end
    self:_OpenDestroyFilterPanel(depotConfig, filterPopupState.selectedTags)
end

ValuableDepotCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local info = self.m_curShowItemList[index]
    local isEquip = info.data.type == GEnums.ItemType.Equip
    cell:InitItem(info, function()
        if self.m_suppressSelectedCell then
            return
        end
        self:_OnClickItem(index)
    end)
    if DeviceInfo.usingController then
        cell:SetEnableHoverTips(false)
    end

    local isGem = info.data.type == GEnums.ItemType.WeaponGem
    if isGem and info.instId and info.instId > 0 then
        local isPerfectMatch = UIUtils.getGemWishListPerfectMatch(info.instId)
        cell:ShowGemPerfectIcon(isPerfectMatch)
    end

    local isSelected = index == self.m_curItemIndex
    if self.m_recoverSelectIndex > 0 then
        cell:SetSelected(isSelected)
        if index == self.m_recoverSelectIndex then
            if DeviceInfo.usingController then
                self:SetNaviTarget(cell.view.button)
            end
            self.m_curItemIndex = self.m_recoverSelectIndex
            self:_RefreshItemInfo(false)
            self.m_recoverSelectIndex = -1
        end
    else
        cell:SetSelected(isSelected and not DeviceInfo.usingController)
        if isSelected and cell.view.button ~= InputManagerInst.controllerNaviManager.curTarget then
            if DeviceInfo.usingController then
                self.m_suppressSelectedCell = true
                self:SetNaviTarget(cell.view.button)
                self.m_suppressSelectedCell = false
            end
        end
    end

    cell.view.imageCharMask.gameObject:SetActive(isEquip)
    if isEquip then
        local equipDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Equip]:GetOrFallback(Utils.getCurrentScope())
        local equipInstDict = equipDepot.instItems
        local _, equipInst = equipInstDict:TryGetValue(info.instId)
        local equippedCardInstId = equipInst.instData.equippedCharServerId
        local isEquipped = equippedCardInstId and equippedCardInstId > 0
        cell.view.count.gameObject:SetActive(false)
        cell.view.imageCharMask.gameObject:SetActive(isEquipped)
        if isEquipped then
            local charEntityInfo = CharInfoUtils.getPlayerCharInfoByInstId(equippedCardInstId)
            local charTemplateId = charEntityInfo.templateId
            local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charTemplateId
            cell.view.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
        end
    end

    local isWeapon = info.data.type == GEnums.ItemType.Weapon
    if isWeapon then
        local weaponDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Weapon]:GetOrFallback(Utils.getCurrentScope())
        local weaponInstDict = weaponDepot.instItems
        local _, weaponInst = weaponInstDict:TryGetValue(info.instId)
        local equippedCardInstId = weaponInst.instData.equippedCharServerId
        local isEquipped = equippedCardInstId and equippedCardInstId > 0
        cell.view.count.gameObject:SetActive(false)
        cell.view.imageCharMask.gameObject:SetActive(isEquipped)
        if isEquipped then
            local charEntityInfo = CharInfoUtils.getPlayerCharInfoByInstId(equippedCardInstId)
            local charTemplateId = charEntityInfo.templateId
            local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charTemplateId
            cell.view.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
        end
    end

    local isWeaponGem = info.data.type == GEnums.ItemType.WeaponGem
    local isWeaponGemEquipped = false
    if isWeaponGem then
        local weaponGemDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.WeaponGem]:GetOrFallback(Utils.getCurrentScope())
        local weaponGemInstDict = weaponGemDepot.instItems
        local _, weaponGemInst = weaponGemInstDict:TryGetValue(info.instId)
        isWeaponGemEquipped = weaponGemInst.instData.weaponInstId > 0
    end
    cell.view.gemEquipped.gameObject:SetActive(isWeaponGemEquipped)

    cell.gameObject.name = "Item-" .. info.id

    if info.data.valuableDepotRedDot then
        cell:UpdateRedDot("ValuableDepotItem", { id = info.id, instId = info.instId })
    else
        cell:UpdateRedDot()
    end
    self:_UpdateItemBlockMask(cell, info)
    cell.view.button:ChangeActionOnSetNaviTarget(self.m_inDestroyMode and ActionOnSetNaviTarget.PressConfirmTriggerOnClick or ActionOnSetNaviTarget.AutoTriggerOnClick)
    if self.m_pendingDestroyNaviFirst and index == 1 then
        self.m_pendingDestroyNaviFirst = false
        self:SetNaviTarget(cell.view.button)
    end
    cell.view.button.onHoverChange:RemoveAllListeners()
    cell.view.button.onHoverChange:AddListener(function(isHover)
        if isHover and DeviceInfo.usingController and self.m_inDestroyMode then
            self:_OnClickItem(index, nil, true)
        end
    end)

    if cell.redDot.curIsActive then
        if info.instId then
            self.m_readItemInstIds[info.instId] = true
        else
            self.m_readItemIds[info.id] = true
        end
    end

    if not self.m_inDestroyMode then
        cell.view.multiSelectMark.gameObject:SetActive(false)
        cell.view.redMultiSelectMark.gameObject:SetActive(false)
        return
    end
    self:_UpdateItemCellDestroySelectPart(index, cell)
end

ValuableDepotCtrl._OnClickItem = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, index, noRead, justNavi)
    if self.m_recoverSelectIndex > 0 then
        return
    end

    if not noRead then
        self:_ReadItem(self.m_curItemIndex)
    end

    local cell = self.m_getItemCell(self.m_curItemIndex)
    if cell then
        cell:SetSelected(false)
    end

    local isSame = self.m_curItemIndex == index

    self.m_curItemIndex = index
    if index > 0 then
        cell = self.m_getItemCell(self.m_curItemIndex)
        if cell then
            cell:SetSelected(true and not DeviceInfo.usingController)
            if DeviceInfo.usingController then
                self:SetNaviTarget(cell.view.button)
            end
        end
        if self.m_inDestroyMode then
            self:_ClickItemInDestroyMode(index, justNavi)
        end
    end

    self:_RefreshItemInfo(isSame)

    if index <= 0 then
        return
    end

    if not noRead then
        self:_ReadItem(index)
    end


    local info = self.m_curShowItemList[index]
    local id = info.id
    if Tables.itemTable:TryGetValue(id) and Tables.itemTable[id].valuableDepotRedDot then
        RedDotUtils.setNewObtainedImportantValuableDepotItem(id, false)
    end
end

ValuableDepotCtrl._AutoFillDestroyList = HL.Method(HL.Number) << function(self, tabIndex)
    self.m_destroyInfo[tabIndex] = {}
    self.m_destroyCount = 0

    local curFilterConfigs = self.m_curDestroyFilterConfigs
    if not curFilterConfigs or not next(curFilterConfigs) then
        return
    end

    local showItemList = self.m_curShowItemList
    local isLack = false
    for itemIndex, itemInfo in pairs(showItemList) do
        if self.m_destroyCount >= self:_GetDestroyMaxCount() then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_DEPOT_DES_AUTO_FILL_REACH_MAX)
            if isLack then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_DEPOT_DES_AUTO_FILL_HAS_LACK)
            end
            return
        end
        if FilterUtils.checkIfPassFilter(itemInfo, curFilterConfigs) then
            local isBlocked = self:_GetDestroyBlockToast(itemInfo)
            if isBlocked then
                isLack = true
            else
                self:_MarkItemDestroy(itemIndex)
            end
        end
    end
    if isLack then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_DEPOT_DES_AUTO_FILL_HAS_LACK)
    end
end

ValuableDepotCtrl._GetAutoFillDestroyResultCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    if not tags or not next(tags) then
        return
    end
    local count = 0
    for itemIndex, itemInfo in pairs(self.m_curShowItemList) do



        if FilterUtils.checkIfPassFilter(itemInfo, tags) then
            count = count + 1
        end
    end
    return count
end

ValuableDepotCtrl._RefreshItemInfo = HL.Method(HL.Boolean) << function(self, noAnimation)
    local node = self.view.itemInfoNode
    if self.m_curItemIndex < 0 then
        if noAnimation then
            node.animation:SampleToOutAnimationEnd()
            node.content.gameObject:SetActive(false)
            node.emptyNode.gameObject:SetActive(true)
            node.stateCtrl:SetState("default")
        else
            node.animation:PlayOutAnimation(function()
                node.content.gameObject:SetActive(false)
                node.emptyNode.gameObject:SetActive(true)
                node.stateCtrl:SetState("default")
            end)
        end
        return
    elseif self.m_curItemIndex == 0 then

        node.animation:SampleToOutAnimationEnd()
        node.content.gameObject:SetActive(false)
        node.emptyNode.gameObject:SetActive(true)
        return
    end
    node.content.gameObject:SetActive(true)
    node.emptyNode.gameObject:SetActive(false)
    if not noAnimation then
        node.animation:SampleToOutAnimationEnd()
        node.animation:PlayInAnimation()
    end
    local info = self.m_curShowItemList[self.m_curItemIndex]
    if node.weaponCompatibleNode then
        node.weaponCompatibleNode.gameObject:SetActive(false)
    end
    UIUtils.displayItemBasicInfos(node, self.loader, info.id, info.instId)
    node.itemDescNode:InitItemDescNode(info.id)

    self.view.itemInfoNode.wikiBtn.gameObject:SetActive(WikiUtils.canShowWikiEntry(info.id))

    local depotConfig = ItemType2DepotConfig[self.m_tabsInfo[self.m_curTabIndex].type]
    node.stateCtrl:SetState(depotConfig.infoStateName)
    if depotConfig.extraDisplayInfoFuncName then
        UIUtils[depotConfig.extraDisplayInfoFuncName](node, self.loader, info.id, info.instId)
    end

    local canJump, jumpFunction = self:_CheckIfCanJump(info.id, info.data.type, info.instId or 0)
    self.view.itemInfoNode.jumpBtn.gameObject:SetActive(canJump)
    self.view.itemInfoNode.jumpBtn.onClick:RemoveAllListeners()
    self.view.itemInfoNode.jumpBtn.onClick:AddListener(function()
        jumpFunction(self, info.id, info.instId or 0)
    end)
    node.itemObtainWays:InitItemObtainWays(info.id, info.instId, nil, nil, function()
        if DeviceInfo.usingController then
            node.itemObtainWays.view.selectableNaviGroup:ManuallyStopFocus()
        end
    end)
    local isLockToggleVisible = self.view.itemInfoNode.lockToggle:InitLockToggle(info.id, info.instId or 0)
    local isTrashToggleVisible = self.view.itemInfoNode.trashToggle:InitTrashToggle(info.id, info.instId or 0)
    local isItemFlagNaviGroupVisible = isLockToggleVisible and isTrashToggleVisible
    InputManagerInst:ToggleBinding(self.m_lockToggleBindingId, isLockToggleVisible and not isItemFlagNaviGroupVisible)
    self.view.itemInfoNode.itemFlagNaviGroup.enabled = isItemFlagNaviGroupVisible
    self.view.itemInfoNode.itemFlagControllerFocusHintNode.gameObject:SetActive(isItemFlagNaviGroupVisible)
    self.view.itemInfoNode.lockToggleKeyHint.gameObject:SetActive(not isItemFlagNaviGroupVisible)

    if DeviceInfo.usingController and node.itemObtainWays.view.selectableNaviGroup.IsTopLayer then

        node.itemObtainWays.view.selectableNaviGroup:ManuallyStopFocus()
    else
        self.view.itemInfoNode.detailScroll:ScrollTo(Vector2(0, 0), false)
    end

    local canUse, useFunc = self:_CheckIfCanUse(info.id, info.instId or 0)
    self.view.itemInfoNode.useBtn.gameObject:SetActive(canUse)
    self.view.itemInfoNode.useBtn.onClick:RemoveAllListeners()
    self.view.itemInfoNode.useBtn.onClick:AddListener(function()
        useFunc(self, info.id)
    end)

    local showTips, tipText = self:_CheckIfShowTips(info.id)
    self.view.itemInfoNode.promptNode.gameObject:SetActive(showTips)
    if showTips then
        self.view.itemInfoNode.promptTxt:SetAndResolveTextStyle(tipText)
    end

    self.view.itemInfoNode.tipsLimitedTimeNode:InitTipsLimitedTimeNode(info.id, info.instId or 0)

    UIUtils.displayGiftItemTags(self.view.itemInfoNode.collectionTagNode, info.id)
    self.view.itemInfoNode.giftFeatureTagsNode:InitGiftFeatureTagsNode(info.id)
end

ValuableDepotCtrl._CheckIfTabUnlocked = HL.Method(HL.Userdata).Return(HL.Boolean) << function(self, itemType)
    local depotConfig = ItemType2DepotConfig[itemType]
    if depotConfig.isUnlocked ~= nil then
        return depotConfig.isUnlocked
    end

    if depotConfig.systemUnlockType then
        return Utils.isSystemUnlocked(depotConfig.systemUnlockType)
    end

    return false
end

ValuableDepotCtrl._RefreshCurrentTabKeepingSelection = HL.Method() << function(self)

    local currentSelectedItem = nil
    if self.m_curItemIndex > 0 and self.m_curShowItemList[self.m_curItemIndex] then
        currentSelectedItem = {
            id = self.m_curShowItemList[self.m_curItemIndex].id,
            instId = self.m_curShowItemList[self.m_curItemIndex].instId
        }
    end


    self:_OnClickTab(self.m_curTabIndex)

    if currentSelectedItem then

        local foundIndex = 0
        for i, item in ipairs(self.m_curShowItemList) do
            if item.id == currentSelectedItem.id and
                (not currentSelectedItem.instId or item.instId == currentSelectedItem.instId) then
                foundIndex = i
                break
            end
        end


        if foundIndex > 0 then
            self.m_curItemIndex = foundIndex
            self:_RefreshItemList(true)
        end
    end
end

ValuableDepotCtrl.OnValuableDepotChanged = HL.Method(HL.Table) << function(self, args)
    local depotType = unpack(args)
    if depotType ~= self.m_tabsInfo[self.m_curTabIndex].type then
        return
    end

    self:_RefreshCurrentTabKeepingSelection()
end

ValuableDepotCtrl.OnWeaponGemWishListChanged = HL.Method() << function(self)
    local curTabInfo = self.m_tabsInfo[self.m_curTabIndex]
    if not curTabInfo or curTabInfo.type ~= GEnums.ItemValuableDepotType.WeaponGem then
        return
    end

    self:_RefreshCurrentTabKeepingSelection()
end

ValuableDepotCtrl._ShowWiki = HL.Method() << function(self)
    local itemInfo = self.m_curShowItemList[self.m_curItemIndex]
    if itemInfo and itemInfo.id then
        Notify(MessageConst.SHOW_WIKI_ENTRY, { itemId = itemInfo.id })
    end
end





ValuableDepotCtrl.m_destroyCount = HL.Field(HL.Number) << 0

ValuableDepotCtrl.m_destroyInfo = HL.Field(HL.Table)

ValuableDepotCtrl.m_getExpandItemCell = HL.Field(HL.Function)

ValuableDepotCtrl.m_destroyExpandItemList = HL.Field(HL.Table)

ValuableDepotCtrl.m_destroyCountItemRealId = HL.Field(HL.String) << ""

ValuableDepotCtrl.m_pendingDestroyNaviFirst = HL.Field(HL.Boolean) << false

ValuableDepotCtrl._InitDestroyNode = HL.Method() << function(self)
    self.view.bottomNode.destroyBtn.onClick:AddListener(function()
        self:_ToggleDestroyMode(true, false)
    end)
    self.view.bottomNode.desEquipBtn.onClick:AddListener(function()
        self:_ToggleDestroyMode(true, false)
    end)

    local node = self.view.destroyNode
    self.view.animation:SampleToOutAnimationEnd()
    self.view.destroyNode.hintTxtNode.gameObject:SetActive(true)

    node.backBtn.onClick:AddListener(function()
        self:_ToggleDestroyMode(false, false)
    end)
    node.normalRightNode.confirmBtn.onClick:AddListener(function()
        self:_ConfirmDestroy()
    end)
    node.equipRightNode.confirmBtn.onClick:AddListener(function()
        self:_ConfirmDestroy()
    end)
    node.expandToggle.isOn = false
    node.expandToggle.onValueChanged:AddListener(function(isOn)
        self:_ToggleDestroySelectExpand(isOn)
    end)
    node.closeExpandBtn.onClick:AddListener(function()
        node.expandToggle.isOn = false
    end)
    node.lockManageBtn.onClick:AddListener(function()
        self:_EnterWeaponLockManageMode()
    end)

    self.m_getExpandItemCell = UIUtils.genCachedCellFunction(node.selectScrollList)
    node.selectScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateExpandCell(self.m_getExpandItemCell(obj), LuaIndex(csIndex))
    end)

    self.m_destroyInfo = {}
end

ValuableDepotCtrl._GetDestroyBlockToast = HL.Method(HL.Table).Return(HL.Boolean, HL.Opt(HL.String)) << function(self, itemInfo)
    local inventory = GameInstance.player.inventory
    local scope = Utils.getCurrentScope()
    local depotConfig = ItemType2DepotConfig[self.m_tabsInfo[self.m_curTabIndex].type]
    local isWeaponDestroy = depotConfig and depotConfig.isWeaponDestroy == true
    local isGemDestroy = depotConfig and depotConfig.isGemDestroy == true
    if not isWeaponDestroy and not inventory:CanDestroyItem(scope, itemInfo.id) then
        return true, Language.LUA_ITEM_CANT_DESTROY_BECAUSE_TYPE
    end
    if inventory:IsEquipped(scope, itemInfo.id, itemInfo.instId) then
        return true, Language.LUA_ITEM_CANT_DESTROY_BECAUSE_USING
    end
    if inventory:IsItemLocked(scope, itemInfo.id, itemInfo.instId) then
        return true, Language.LUA_ITEM_CANT_DESTROY_BECAUSE_LOCK
    end

    if isWeaponDestroy and itemInfo.data.type == GEnums.ItemType.Weapon then
        local weaponInst = itemInfo.instId and itemInfo.instId > 0 and CharInfoUtils.getWeaponByInstId(itemInfo.instId) or nil
        if not weaponInst then
            return true, Language.LUA_ITEM_CANT_DESTROY_BECAUSE_TYPE
        end

        local hasFedExp = (weaponInst.exp and weaponInst.exp > 0) or (weaponInst.weaponLv and weaponInst.weaponLv > 1)
        local hasGem = weaponInst.attachedGemInstId and weaponInst.attachedGemInstId > 0
        local hasPotentialUpgrade = weaponInst.refineLv and weaponInst.refineLv > 0
        local isSixStar = itemInfo.rarity and itemInfo.rarity >= 6
        if hasFedExp or hasGem or hasPotentialUpgrade or isSixStar then
            return true, Language.LUA_ITEM_CANT_DESTROY_BECAUSE_TYPE
        end
    end
    if isGemDestroy and itemInfo.data.type == GEnums.ItemType.WeaponGem then

        local isMaxRarityGem = itemInfo.rarity and itemInfo.rarity >= 5
        if isMaxRarityGem then
            return true, Language.LUA_ITEM_CANT_DESTROY_BECAUSE_TYPE
        end
    end

    return false
end

ValuableDepotCtrl._UpdateItemBlockMask = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    local showMask = false
    local inventory = GameInstance.player.inventory
    if self.m_inDestroyMode then
        if info.instId then
            showMask = not inventory:CanDestroyItem(Utils.getCurrentScope(), info.id, info.instId)
        else
            showMask = not inventory:CanDestroyItem(Utils.getCurrentScope(), info.id)
        end


        if info.data.type == GEnums.ItemType.WeaponGem then
            showMask = not inventory:CanDestroyItem(Utils.getCurrentScope(), info.id) or
                inventory:IsEquipped(Utils.getCurrentScope(), info.id, info.instId) or
                inventory:IsItemLocked(Utils.getCurrentScope(), info.id, info.instId)
        end


        if info.data.type == GEnums.ItemType.Weapon then
            showMask = self:_GetDestroyBlockToast(info)
        end

        local desInfo = self.m_destroyInfo[self.m_curTabIndex][info.realId]
        cell.view.button.customBindingViewLabelText = desInfo and Language.LUA_VALUABLE_DEPOT_DESTROY_UNSELECT_KEY_HINT
            or Language.LUA_VALUABLE_DEPOT_DESTROY_SELECT_KEY_HINT
    else
        cell.view.button.customBindingViewLabelText = ''
    end
    cell.view.blockMask.gameObject:SetActiveIfNecessary(showMask)
end

ValuableDepotCtrl._ToggleDestroyMode = HL.Method(HL.Boolean, HL.Boolean) << function(self, active, noAnimation)
    local node = self.view.destroyNode
    local infos = self.m_tabsInfo[self.m_curTabIndex]
    local depotConfig = ItemType2DepotConfig[infos.type]
    local shouldFilterDestroyList = depotConfig.isWeaponDestroy or depotConfig.isGemDestroy

    self.view.topNode.tabsPCInputBindingGroup.enabled = not active
    if DeviceInfo.usingController then
        self.view.itemInfoNode.detailScrollInputBindingGroupMonoTarget.enabled = not active
    end

    if active then
        node.quickInputBtn.onClick:RemoveAllListeners()
        if depotConfig.destroyFilterOptions then
            node.quickInputBtn.gameObject:SetActive(true)
            node.quickInputBtn.onClick:AddListener(function()
                self:_OpenDestroyFilterPanel(depotConfig)
            end)
        else
            node.quickInputBtn.gameObject:SetActive(false)
        end
        if depotConfig.isNormalDestroy then
            node.simpleStateController:SetState("Normal")
            node.rightNode = node.normalRightNode
        elseif depotConfig.isEquipDestroy then
            node.simpleStateController:SetState("Equip")
            node.rightNode = node.equipRightNode
        elseif depotConfig.isGemDestroy then
            node.simpleStateController:SetState("Gem")
            node.rightNode = node.equipRightNode
        elseif depotConfig.isWeaponDestroy then
            node.simpleStateController:SetState("Weapon")
            node.rightNode = node.equipRightNode
        end

        node.lockManageBtn.gameObject:SetActive(depotConfig.isWeaponDestroy == true)

        if noAnimation then
            self.view.animation:SampleToInAnimationEnd()
        else
            self.view.animation:PlayInAnimation()
        end
    else
        if noAnimation then
            self.view.animation:SampleToOutAnimationEnd()
        else
            self.view.animation:PlayOutAnimation()
        end
    end

    self.m_tabCells:Update(function(cell, index)
        cell.toggle.interactable = not active
        cell.canvasGroup.alpha = (not active or index == self.m_curTabIndex) and 1 or 0.3
    end)

    self.m_inDestroyMode = active

    if not active then
        self.m_pendingDestroyNaviFirst = false
        if shouldFilterDestroyList then
            self:_ApplyFilter()
            self:_ApplySort(self.view.bottomNode.sortNode:GetCurSortData(), self.view.bottomNode.sortNode.isIncremental)
            self:_SetSelectedIndex()
            self:_RefreshItemList(true, true)
        end
        local desInfos = self.m_destroyInfo[self.m_curTabIndex]
        self.m_destroyInfo = {}
        for realId, info in pairs(desInfos) do
            local k, v = self:_GetIndexFromRealId(realId)
            if k then
                self:_UpdateItemCellDestroySelectPart(k)
            end
        end
        if self.m_curItemIndex <= 0 then
            self:_OnClickItem(math.min(1, self.m_curShowCount))
        end
        self.view.walletBarPlaceholder.gameObject:SetActive(true)
        node.backBtn.gameObject:SetActive(true)
    else
        self.m_destroyInfo = {}
        for k = 1, #self.m_tabsInfo do
            self.m_destroyInfo[k] = {}
        end
        self.m_destroyCount = 0
        self:_UpdateDestroySelectTotalCount(true)
        if shouldFilterDestroyList then
            self:_ApplyDestroySelectableFilter()
            self.m_pendingDestroyNaviFirst = DeviceInfo.usingController and self.m_curShowCount > 0
            self:_RefreshItemList(true, true)
        end
        if not DeviceInfo.usingController then
            self:_OnClickItem(-1)
        end
    end


    for k = 1, self.view.itemScrollList.count do
        local cell = self.m_getItemCell(k)
        if cell then
            local info = self.m_curShowItemList[k]
            self:_UpdateItemBlockMask(cell, info)
            cell.view.button:ChangeActionOnSetNaviTarget(
                active and ActionOnSetNaviTarget.PressConfirmTriggerOnClick or ActionOnSetNaviTarget.AutoTriggerOnClick)
        end
    end

    self:_ToggleDestroySelectExpand(false, true)

    self.view.itemScrollList:SetPaddingBottom(active and self.m_oriPaddingBottom + 150 or self.m_oriPaddingBottom)
end

ValuableDepotCtrl._OpenDestroyFilterPanel = HL.Method(HL.Table, HL.Opt(HL.Table)) << function(self, depotConfig, selectedTags)
    Notify(MessageConst.SHOW_COMMON_FILTER, {
        tagGroups = depotConfig.destroyFilterOptions,
        selectedTags = lume.deepCopy(selectedTags or self.m_curDestroyFilterConfigs or {}),
        onConfirm = function(tags)
            self.m_curDestroyFilterConfigs = tags
            self:_AutoFillDestroyList(self.m_curTabIndex)
            self:_RefreshItemList(true)
            self:_UpdateDestroySelectTotalCount()
        end,
        getResultCount = function(tags)
            return self:_GetAutoFillDestroyResultCount(tags)
        end,
    })
end

ValuableDepotCtrl._ClickItemInDestroyMode = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, fromNavigation)
    local node = self.view.destroyNode
    local itemInfo = self.m_curShowItemList[index]
    local realId = itemInfo.realId

    if self.m_destroyInfo[self.m_curTabIndex][realId] then
        if not fromNavigation then

            self.m_destroyInfo[self.m_curTabIndex][realId] = nil
            self.m_destroyCount = self.m_destroyCount - 1

            self:_UpdateItemCellDestroySelectPart(index)
            self:_SetDestroyCountTarget("")
        else
            self:_SetDestroyCountTarget(realId)
        end
    else
        if not fromNavigation then
            local isBlocked, toast = self:_GetDestroyBlockToast(itemInfo)
            if isBlocked then
                Notify(MessageConst.SHOW_TOAST, toast)
                self:_SetDestroyCountTarget("")
            elseif self.m_destroyCount >= self:_GetDestroyMaxCount() then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_CANT_DESTROY_BECAUSE_SELECTED_MAX)
                self:_SetDestroyCountTarget("")
            else
                self.m_destroyInfo[self.m_curTabIndex][realId] = {
                    realId = itemInfo.realId,
                    id = itemInfo.id,
                    instId = itemInfo.instId,
                    count = itemInfo.count,
                    selectCount = itemInfo.count,
                }
                self.m_destroyCount = self.m_destroyCount + 1

                self:_UpdateItemCellDestroySelectPart(index)
                self:_UpdateItemCountInExpandList(realId)
                self:_SetDestroyCountTarget(realId)
            end
        else
            self:_SetDestroyCountTarget("")
        end
    end

    if not fromNavigation then
        self:_UpdateDestroySelectTotalCount()
    end
end

ValuableDepotCtrl._MarkItemDestroy = HL.Method(HL.Number) << function(self, index)
    local node = self.view.destroyNode
    local itemInfo = self.m_curShowItemList[index]
    local realId = itemInfo.realId

    local isBlocked = self:_GetDestroyBlockToast(itemInfo)
    if isBlocked then
        return
    end
    if self.m_destroyCount >= self:_GetDestroyMaxCount() then
        return
    end

    self.m_destroyInfo[self.m_curTabIndex][realId] = {
        realId = itemInfo.realId,
        id = itemInfo.id,
        instId = itemInfo.instId,
        count = itemInfo.count,
        selectCount = itemInfo.count,
    }
    self.m_destroyCount = self.m_destroyCount + 1
end

ValuableDepotCtrl._SetDestroyCountTarget = HL.Method(HL.String) << function(self, realId)
    local desInfo
    local index = self:_GetIndexFromRealId(realId)
    if index then
        desInfo = self.m_destroyInfo[self.m_curTabIndex][realId]
    else
        for _, infos in ipairs(self.m_destroyInfo) do
            for k, v in pairs(infos) do
                if k == realId then
                    desInfo = v
                    break
                end
            end
            if desInfo then
                break
            end
        end
    end
    if not desInfo then

        realId = ""
    end

    local oldIsEmpty = string.isEmpty(self.m_destroyCountItemRealId)
    local newIsEmpty = string.isEmpty(realId)
    self.m_destroyCountItemRealId = realId
    local node = self.view.destroyNode
    if newIsEmpty then
        node.numberSelector.gameObject:SetActive(false)
        return
    end

    if desInfo.instId then
        node.numberSelector.gameObject:SetActive(false)
    else
        node.numberSelector.gameObject:SetActive(true)
        node.numberSelector:InitNumberSelector(desInfo.selectCount, 1, desInfo.count, function(newCount)
            self:_OnChangeItemDestroyCount(realId, newCount)
        end)
    end
end

ValuableDepotCtrl._GetIndexFromRealId = HL.Method(HL.String).Return(HL.Opt(HL.Number, HL.Table)) << function(self, realId)
    for k, v in ipairs(self.m_curShowItemList) do
        if v.realId == realId then
            return k, v
        end
    end
end

ValuableDepotCtrl._UpdateItemCellDestroySelectPart = HL.Method(HL.Opt(HL.Number, HL.Userdata, HL.Table)) << function(self, index, cell, desExpandInfo)
    if not cell then
        cell = self.m_getItemCell(index)
        if not cell then
            return
        end
    end

    local desInfo, itemInfo
    if index then
        itemInfo = self.m_curShowItemList[index]
        if not itemInfo then
            cell.view.multiSelectMark.gameObject:SetActive(false)
            cell.view.redMultiSelectMark.gameObject:SetActive(false)
            return
        end
        desInfo = self.m_inDestroyMode and self.m_destroyInfo[self.m_curTabIndex][itemInfo.realId]
    elseif desExpandInfo then
        desInfo = self.m_destroyInfo[desExpandInfo.tabIndex][desExpandInfo.realId]
    end

    if desInfo then
        if itemInfo then
            cell.view.count.text = string.format("<color=#%s>%s</color>/%s", UIConst.COUNT_RED_COLOR_STR, UIUtils.getNumString(desInfo.selectCount), UIUtils.getNumString(itemInfo.count))
        else

            cell.view.count.text = string.format(UIConst.COLOR_STRING_FORMAT, UIConst.COUNT_RED_COLOR_STR, UIUtils.getNumString(desInfo.selectCount))
        end
        if not desExpandInfo then
            InputManagerInst:SetBindingText(cell.view.button.hoverConfirmBindingId, Language.LUA_VALUABLE_DEPOT_DESTROY_UNSELECT_KEY_HINT)
        end
    else
        cell:UpdateCount(itemInfo.count)
        if not desExpandInfo then
            InputManagerInst:SetBindingText(cell.view.button.hoverConfirmBindingId, Language.LUA_VALUABLE_DEPOT_DESTROY_SELECT_KEY_HINT)
        end
    end

    if itemInfo then
        local depotConfig = ItemType2DepotConfig[self.m_tabsInfo[self.m_curTabIndex].type]
        local mark = (depotConfig.isEquipDestroy or depotConfig.isGemDestroy or depotConfig.isWeaponDestroy) and cell.view.multiSelectMark or cell.view.redMultiSelectMark
        if desInfo then
            mark.gameObject:SetActive(true)
        else
            mark.gameObject:SetActive(false)
        end
    end
end

ValuableDepotCtrl._OnChangeItemDestroyCount = HL.Method(HL.String, HL.Number) << function(self, realId, newCount)
    for _, infos in ipairs(self.m_destroyInfo) do
        for k, v in pairs(infos) do
            if k == realId then
                v.selectCount = newCount
            end
        end
    end
    local index = self:_GetIndexFromRealId(realId)
    if index then
        self:_UpdateItemCellDestroySelectPart(index)
    end
    self:_UpdateItemCountInExpandList(realId)
end

ValuableDepotCtrl._UpdateItemCountInExpandList = HL.Method(HL.String) << function(self, realId)
    if not self.view.destroyNode.expandToggle.isOn then
        return
    end
    for k, v in ipairs(self.m_destroyExpandItemList) do
        if v.realId == realId then
            local expandCell = self.m_getExpandItemCell(k)
            if expandCell then
                self:_UpdateItemCellDestroySelectPart(nil, expandCell, v)
            end
            return
        end
    end
end

ValuableDepotCtrl._GetDestroyMaxCount = HL.Method().Return(HL.Number) << function(self)
    local tabInfo = self.m_tabsInfo[self.m_curTabIndex]
    if tabInfo then
        local depotConfig = ItemType2DepotConfig[tabInfo.type]
        if depotConfig then
            if depotConfig.isGemDestroy then
                return Tables.GlobalConst.depotDestroyMaxCountGem
            elseif depotConfig.isEquipDestroy then
                return Tables.GlobalConst.depotDestroyMaxCountEquip
            elseif depotConfig.isWeaponDestroy then
                return Tables.GlobalConst.depotDestroyMaxCountWeapon
            end
        end
    end
    return 100
end

ValuableDepotCtrl._UpdateDestroySelectTotalCount = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local node = self.view.destroyNode
    node.selectCountTxt.text = string.format(Language.LUA_DEPOT_DESTROY_COUNT, self.m_destroyCount, self:_GetDestroyMaxCount())
    local showBtn = self.m_destroyCount > 0
    local rightNode = node.rightNode
    if not rightNode.animationWrapper then
        rightNode.confirmBtn.gameObject:SetActive(showBtn)
        rightNode.disabledBtn.gameObject:SetActive(not showBtn)
    else
        rightNode.disabledBtn.gameObject:SetActive(not showBtn)
        if isInit then
            rightNode.previewNode.gameObject:SetActive(showBtn)
            rightNode.confirmBtn.gameObject:SetActive(showBtn)
        elseif showBtn ~= rightNode.previewNode.gameObject.activeSelf then

            rightNode.previewNode.gameObject:SetActive(true)
            rightNode.confirmBtn.gameObject:SetActive(true)
            if showBtn then
                rightNode.animationWrapper:PlayInAnimation(function()
                    rightNode.previewNode.gameObject:SetActive(showBtn)
                    rightNode.confirmBtn.gameObject:SetActive(showBtn)
                end)
            else
                rightNode.animationWrapper:PlayOutAnimation(function()
                    rightNode.previewNode.gameObject:SetActive(showBtn)
                    rightNode.confirmBtn.gameObject:SetActive(showBtn)
                end)
            end
        end
        if showBtn then
            self.m_getPreviewItemCell = self.m_getPreviewItemCell or UIUtils.genCachedCellFunction(rightNode.previewItemScrollList)
            local depotConfig = ItemType2DepotConfig[self.m_tabsInfo[self.m_curTabIndex].type]
            local previewItems
            if depotConfig.isEquipDestroy then
                previewItems = self:_GetDesEquipReturnItems(self.m_curTabIndex)
            elseif depotConfig.isGemDestroy then
                previewItems = self:_GetDesGemReturnItems(self.m_curTabIndex)
            elseif depotConfig.isWeaponDestroy then
                previewItems = self:_GetDesWeaponReturnItems(self.m_curTabIndex)
            end
            rightNode.previewItemScrollList.onUpdateCell:RemoveAllListeners()
            rightNode.previewItemScrollList.onUpdateCell:AddListener(function(obj, csIndex)
                local cell = self.m_getPreviewItemCell(obj)
                cell:InitItem(previewItems[LuaIndex(csIndex)], true)
                cell:SetExtraInfo({
                    isSideTips = DeviceInfo.usingController,
                })
                if DeviceInfo.usingController then
                    cell:SetEnableHoverTips(false)
                end
            end)
            local previewItemCount = previewItems and #previewItems or 0
            rightNode.previewItemScrollList:UpdateCount(previewItemCount)
        end
    end
end

ValuableDepotCtrl._GetDesEquipReturnItems = HL.Method(HL.Number).Return(HL.Table) << function(self, tabIndex)
    local itemMap = {}
    local ratio = Tables.equipTechConst.equipRecycleRatio
    local destroyItemInfos = self.m_destroyInfo[tabIndex]
    for _, info in pairs(destroyItemInfos) do
        local formulaId = Tables.equipFormulaReverseTable[info.id]
        local equipInst = info.instId and info.instId > 0 and CharInfoUtils.getEquipByInstId(info.instId) or nil
        if equipInst then
            local costGoldStringId = equipInst.craftCostGoldId
            local craftCostGoldNum = equipInst.craftCostGoldNum
            if craftCostGoldNum <= 0 or costGoldStringId == nil then
                local chainInfo = EquipTechUtils.GetDefaultFormulaChain(formulaId)
                if chainInfo then
                    costGoldStringId = chainInfo.costGoldId
                    craftCostGoldNum = chainInfo.costGoldNum
                end
            end

            if itemMap[costGoldStringId] then
                itemMap[costGoldStringId] = itemMap[costGoldStringId] + craftCostGoldNum
            else
                itemMap[costGoldStringId] = craftCostGoldNum
            end
        end
    end
    local items = {}
    for itemId, count in pairs(itemMap) do
        local newCount = math.floor(count * ratio)
        if newCount > 0 then
            table.insert(items, { id = itemId, count = newCount })
        end
    end
    return items
end

ValuableDepotCtrl._GetDesGemReturnItems = HL.Method(HL.Number).Return(HL.Table) << function(self, tabIndex)
    local itemMap = {}
    local destroyItemInfos = self.m_destroyInfo[tabIndex]
    for _, info in pairs(destroyItemInfos) do
        local _, itemData = Tables.itemTable:TryGetValue(info.id)
        local gemInst = CharInfoUtils.getGemByInstId(info.instId)
        if itemData and gemInst and not string.isEmpty(gemInst.domainId) then
            local _, gemDismantleData = Tables.gemDismantleTable:TryGetValue(itemData.rarity)
            if gemDismantleData then
                local _, gemDismantleDomainData = gemDismantleData.list:TryGetValue(gemInst.domainId)
                if gemDismantleDomainData then
                    if itemMap[gemDismantleDomainData.itemId] then
                        itemMap[gemDismantleDomainData.itemId] = itemMap[gemDismantleDomainData.itemId] + gemDismantleDomainData.itemNum
                    else
                        itemMap[gemDismantleDomainData.itemId] = gemDismantleDomainData.itemNum
                    end
                    if itemMap[gemDismantleDomainData.goldId] then
                        itemMap[gemDismantleDomainData.goldId] = itemMap[gemDismantleDomainData.goldId] + gemDismantleDomainData.goldNum
                    else
                        itemMap[gemDismantleDomainData.goldId] = gemDismantleDomainData.goldNum
                    end
                end
            end
        end
    end
    local items = {}
    for itemId, count in pairs(itemMap) do
        if count > 0 then
            table.insert(items, { id = itemId, count = count })
        end
    end
    return items
end








ValuableDepotCtrl._GetDesWeaponReturnItems = HL.Method(HL.Number).Return(HL.Table) << function(self, tabIndex)
    local destroyItemInfos = self.m_destroyInfo[tabIndex]

    local totalExp = 0
    for _, info in pairs(destroyItemInfos) do
        local _, itemCfg = Tables.itemTable:TryGetValue(info.id)
        local weaponInst = info.instId and info.instId > 0 and CharInfoUtils.getWeaponByInstId(info.instId) or nil
        if itemCfg and weaponInst then
            totalExp = totalExp + WeaponUtils.CalcItemExp(itemCfg, weaponInst)
        end
    end
    totalExp = math.floor(totalExp)

    local expItems = {}
    for i = 1, Tables.characterConst.weaponExpItem.Count do
        local itemId = Tables.characterConst.weaponExpItem[CSIndex(i)]
        local _, expItemCfg = Tables.itemTable:TryGetValue(itemId)
        if expItemCfg then
            local _, rarityCfg = Tables.weaponExpItemTable:TryGetValue(expItemCfg.rarity)
            if rarityCfg and rarityCfg.itemExp > 0 then
                table.insert(expItems, { id = itemId, itemExp = rarityCfg.itemExp })
            end
        end
    end
    table.sort(expItems, function(a, b) return a.itemExp > b.itemExp end)

    local items = {}
    local remaining = totalExp
    for _, expItem in ipairs(expItems) do
        if remaining >= expItem.itemExp then
            local count = math.floor(remaining / expItem.itemExp)
            remaining = remaining - count * expItem.itemExp
            table.insert(items, { id = expItem.id, count = count })
        end
    end
    return items
end

ValuableDepotCtrl._ToggleDestroySelectExpand = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, fastMode)
    self.view.walletBarPlaceholder.gameObject:SetActive(not active)

    local node = self.view.destroyNode
    local info = self.m_tabsInfo[self.m_curTabIndex]
    local depotConfig = ItemType2DepotConfig[info.type]

    node.backBtn.gameObject:SetActive(not active)
    node.hintTxtNode.gameObject:SetActive(not active)
    node.lockManageBtn.gameObject:SetActive(not active and depotConfig.isWeaponDestroy == true)
    node.quickInputBtn.gameObject:SetActive(depotConfig.destroyFilterOptions and not active)

    if active then
        node.selectInfoNode.gameObject:SetActive(true)
    elseif fastMode then
        node.selectInfoNode.gameObject:SetActive(false)
    else
        node.selectInfoNode:PlayOutAnimation(function()
            node.selectInfoNode.gameObject:SetActive(false)
        end)
    end

    if DeviceInfo.usingController then
        node.expandToggle.enabled = not active
        node.equipRightNode.focusKeyHint:SetActionId(active and "" or "valuable_depot_focus_item")
    end

    if not active and self.m_inDestroyMode then
        self.m_destroyExpandItemList = {}
        self:_SetDestroyCountTarget(info and info.realId or "")
        return
    end

    self:_RefreshDestroySelectExpandList()
    if active then
        local cell = self.m_getExpandItemCell(1)
        if cell then
            self:SetNaviTarget(cell.view.button)
            self:_SetDestroyCountTarget(self.m_destroyExpandItemList[1].realId)
        else

            self:SetNaviTarget(node.itemCell.view.button)
        end
    end
end

ValuableDepotCtrl._RefreshDestroySelectExpandList = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    local node = self.view.destroyNode
    self.m_destroyExpandItemList = {}
    for tabIndex, infos in ipairs(self.m_destroyInfo) do
        local needFindIndex = tabIndex == self.m_curTabIndex
        for realId, _ in pairs(infos) do
            if needFindIndex then
                local k, v = self:_GetIndexFromRealId(realId)
                if k then
                    table.insert(self.m_destroyExpandItemList, {
                        tabIndex = tabIndex,
                        index = k,
                        realId = realId,
                    })
                end
            else
                table.insert(self.m_destroyExpandItemList, {
                    tabIndex = tabIndex,
                    realId = realId,
                })
            end
        end
    end
    table.sort(self.m_destroyExpandItemList, Utils.genSortFunction({ "index" }, true))
    node.selectScrollList:UpdateCount(#self.m_destroyExpandItemList, false, false, false, skipAnim == true)
    self:_SetDestroyCountTarget("")
end

ValuableDepotCtrl._OnUpdateExpandCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local info = self.m_destroyExpandItemList[index]
    if not info then
        return
    end
    local realId = info.realId
    local desInfo = self.m_destroyInfo[info.tabIndex] and self.m_destroyInfo[info.tabIndex][info.realId]
    if not desInfo then
        return
    end
    cell:InitItem(desInfo, function()
        self:_SetDestroyCountTarget(realId)
        if not DeviceInfo.usingController then
            cell:ShowTips({
                safeArea = self.view.destroyNode.numberSelector.rectTransform,
                padding = { bottom = self.view.destroyNode.bottomNode.transform.rect.size.y + 20 },
                isSideTips = true,
            }, function()
                if self.m_destroyCountItemRealId == realId then
                    self:_SetDestroyCountTarget("")
                end
            end)
        end
    end)

    if DeviceInfo.usingController then
        cell:AddHoverBinding("inv_depot_cancel_des_select", function()
            self:_OnClickExpandItemDelBtn(index)
        end)
    end

    cell.view.deleteBtn.onClick:RemoveAllListeners()
    cell.view.deleteBtn.onClick:AddListener(function()
        self:_OnClickExpandItemDelBtn(index)
    end)
    self:_UpdateItemCellDestroySelectPart(nil, cell, info)
    cell.view.button.clickHintTextId = "virtual_mouse_hint_item_tips"
    cell.view.deleteBtn.gameObject:SetActive(true)
end

ValuableDepotCtrl._OnClickExpandItemDelBtn = HL.Method(HL.Number) << function(self, index)
    local info = self.m_destroyExpandItemList[index]
    if info.index then
        self:_ClickItemInDestroyMode(info.index)
    else
        self.m_destroyInfo[info.tabIndex][info.realId] = nil
        self.m_destroyCount = self.m_destroyCount - 1
        self:_UpdateDestroySelectTotalCount()
        self:_SetDestroyCountTarget("")
    end
    self:_RefreshDestroySelectExpandList(true)
    if info.index == self.m_curItemIndex then
        self:_OnClickItem(-1)
    end
    if DeviceInfo.usingController then
        local curCount = #self.m_destroyExpandItemList
        if curCount > 0 then
            local newTargetIndex = index
            if newTargetIndex > curCount then
                newTargetIndex = index - 1
                if newTargetIndex > 0 then
                    self.view.destroyNode.selectScrollList:ScrollToIndex(newTargetIndex, true)
                    local cell = self.m_getExpandItemCell(newTargetIndex)
                    if cell then
                        self:SetNaviTarget(cell.view.button)
                    end
                end
            end
        end
    end
end

ValuableDepotCtrl.OnEquipRecycle = HL.Method(HL.Table) << function(self, arg)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_RECYCLE_SUCC)
end

ValuableDepotCtrl.OnGemDismantle = HL.Method(HL.Table) << function(self, args)
    local refundItems, refundMoney = unpack(args)
    local items = {}
    for _, itemInfo in cs_pairs(refundItems) do
        table.insert(items, {
            id = itemInfo.Id,
            count = itemInfo.Count,
        })
    end
    for _, itemInfo in cs_pairs(refundMoney) do
        table.insert(items, {
            id = itemInfo.Id,
            count = itemInfo.Count,
        })
    end

    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        icon = "icon_recycle_rewards",
        title = Language.LUA_GEM_DISMANTLE_RESULT_TITLE,
        items = items,
    })
end

ValuableDepotCtrl.OnWeaponRecycle = HL.Method(HL.Table) << function(self, args)
    local itemList = unpack(args)
    local items = {}
    if itemList then
        for id, count in cs_pairs(itemList) do
            table.insert(items, {
                id = id,
                count = count,
            })
        end
    end

    if #items > 0 then
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            icon = "icon_recycle_rewards",
            title = Language.LUA_GEM_DISMANTLE_RESULT_TITLE,
            items = items,
        })
    else
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_RECYCLE_SUCC)
    end
end

ValuableDepotCtrl.OnWeaponBatchLock = HL.Method(HL.Table) << function(self, args)

    if self.m_curTabIndex and self.m_curTabIndex > 0 then
        self:_OnClickTab(self.m_curTabIndex)
    end
end

ValuableDepotCtrl._ConfirmDestroy = HL.Method() << function(self)
    local items = {}
    local itemDelInfo = {}
    local instDelInfo = {}
    for tabIndex, infos in pairs(self.m_destroyInfo) do
        itemDelInfo[tabIndex] = {}
        instDelInfo[tabIndex] = {}
        for _, info in pairs(infos) do
            if info.instId and info.instId > 0 then
                table.insert(instDelInfo[tabIndex], info.instId)
            else
                itemDelInfo[tabIndex][info.id] = info.selectCount
            end
            table.insert(items, {
                id = info.id,
                count = info.selectCount,
                instId = info.instId,
            })
        end
    end
    table.sort(items, Utils.genSortFunction({ "id" }, true))

    local depotConfig = ItemType2DepotConfig[self.m_tabsInfo[self.m_curTabIndex].type]
    if depotConfig.isEquipDestroy then
        UIManager:Open(PanelId.DesEquipPopUp, {
            items = items,
            returnItems = self:_GetDesEquipReturnItems(self.m_curTabIndex),
            onConfirm = function()
                GameInstance.player.inventory:RecycleEquip(instDelInfo[self.m_curTabIndex])
                self:_ToggleDestroyMode(false, false)
            end,
        })
    elseif depotConfig.isGemDestroy then
        UIManager:Open(PanelId.DesEquipPopUp, {
            items = items,
            returnItems = self:_GetDesGemReturnItems(self.m_curTabIndex),
            onConfirm = function()
                GameInstance.player.inventory:RecycleGem(instDelInfo[self.m_curTabIndex])
                self:_ToggleDestroyMode(false, false)
            end,
        })
    elseif depotConfig.isWeaponDestroy then
        UIManager:Open(PanelId.DesEquipPopUp, {
            items = items,
            returnItems = self:_GetDesWeaponReturnItems(self.m_curTabIndex),
            onConfirm = function()
                GameInstance.player.inventory:RecycleWeapon(instDelInfo[self.m_curTabIndex])
                self:_ToggleDestroyMode(false, false)
            end,
        })
    else
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_DESTROY_ITEM_CONFIRM_TEXT,
            warningContent = Language.LUA_DESTROY_ITEM_CONFIRM_WARNING_TEXT,
            items = items,
            onConfirm = function()
                for tabIndex, tabInfo in ipairs(self.m_tabsInfo) do
                    local itemInfos = itemDelInfo[tabIndex]
                    local instIds = instDelInfo[tabIndex]
                    if next(itemInfos) or next(instIds) then
                        GameInstance.player.inventory:DestroyInDepot(Utils.getCurrentScope(), tabInfo.type, itemInfos, instIds)
                    end
                end
                self:_ToggleDestroyMode(false, false)
            end,
        })
    end
end




ValuableDepotCtrl._EnterWeaponLockManageMode = HL.Method() << function(self)
    UIManager:Open(PanelId.CommonBatchLock, {
        unlockHintText = Language.ui_valuabledepot_weapon_unlock_hint,
        unlockActions = {
            {
                id = CS.Proto.WEAPON_BATCH_LOCK_MODE.UnlockAllFiveStar,
                labelText = Language.ui_valuabledepot_weapon_unlock_all_fivestar,
            },
            {
                id = CS.Proto.WEAPON_BATCH_LOCK_MODE.UnlockFiveStarBeyondPotentialCap,
                labelText = Language.ui_valuabledepot_weapon_unlock_maxpotential,
            },
        },
        lockActions = {
            {
                id = CS.Proto.WEAPON_BATCH_LOCK_MODE.LockAllFiveStar,
                labelText = Language.ui_valuabledepot_weapon_lock_all_fivestar,
            },
        },
        onConfirm = function(mode)
            GameInstance.player.inventory:WeaponBatchLock(mode)
        end,
    })
end

ValuableDepotCtrl.EnterWeaponDestroyMode = HL.Method() << function(self)
    local weaponTabIndex = -1
    for index, info in ipairs(self.m_tabsInfo or {}) do
        if info.type == GEnums.ItemValuableDepotType.Weapon then
            weaponTabIndex = index
            break
        end
    end
    if weaponTabIndex <= 0 then
        return
    end
    if self.m_curTabIndex ~= weaponTabIndex then
        self:_ReadCurShowingItems()
        local weaponTabCell = self.m_tabCells and self.m_tabCells:GetItem(weaponTabIndex)
        if weaponTabCell and weaponTabCell.toggle then
            weaponTabCell.toggle.isOn = true
        end
        self:_OnClickTab(weaponTabIndex)
    end
    if not self.m_inDestroyMode then
        self:_ToggleDestroyMode(true, false)
    end
end

ValuableDepotCtrl.OnItemLockedStateChanged = HL.Method(HL.Table) << function(self, args)
    if not self.m_inDestroyMode then
        return
    end
    local id, instId, isLocked = unpack(args)
    local itemInfo = self.m_curShowItemList[self.m_curItemIndex]
    if itemInfo and itemInfo.id == id and itemInfo.instId == instId then
        local cell = self.m_getItemCell(self.m_curItemIndex)
        if cell then
            self:_UpdateItemBlockMask(cell, itemInfo)
        end
        self:_ClickItemInDestroyMode(self.m_curItemIndex)
    end
end





ValuableDepotCtrl._JumpToWeaponGem = HL.Method(HL.String, HL.Number) << function(self, gemTemplateId, gemInstId)
    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    if not gemInst then
        return
    end

    local attachedWeaponInstId = gemInst.weaponInstId
    if not attachedWeaponInstId then
        return
    end

    local weaponInst = CharInfoUtils.getWeaponByInstId(attachedWeaponInstId)
    if not weaponInst then
        return
    end

    local fadeTimeBoth = UIConst.CHAR_INFO_TRANSITION_BLACK_SCREEN_DURATION
    local dynamicFadeData = UIUtils.genDynamicBlackScreenMaskData("ValuableDepot->WeaponInfo", fadeTimeBoth, fadeTimeBoth, function()
        self.view.itemScrollList:UpdateCount(0)
        CharInfoUtils.openWeaponInfoBestWay({
            weaponTemplateId = weaponInst.templateId,
            weaponInstId = weaponInst.instId,
            pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.GEM
        })
    end)
    GameAction.ShowBlackScreen(dynamicFadeData)
end

ValuableDepotCtrl._JumpToWeapon = HL.Method(HL.String, HL.Number) << function(self, weaponTemplateId, weaponInstId)
    local fadeTimeBoth = UIConst.CHAR_INFO_TRANSITION_BLACK_SCREEN_DURATION
    local dynamicFadeData = UIUtils.genDynamicBlackScreenMaskData("ValuableDepot->WeaponInfo", fadeTimeBoth, fadeTimeBoth, function()

        CharInfoUtils.openWeaponInfoBestWay({
            weaponTemplateId = weaponTemplateId,
            weaponInstId = weaponInstId,
        })

        self.view.itemScrollList:UpdateCount(0)

    end)
    dynamicFadeData.notHideCursor = true
    GameAction.ShowBlackScreen(dynamicFadeData)
end

ValuableDepotCtrl._CheckIfCanJump = HL.Method(HL.String, HL.Userdata, HL.Opt(HL.Number)).Return(HL.Boolean, HL.Opt(HL.Function)) << function(self, itemId, itemType, instId)
    if not instId or instId <= 0 then
        return false
    end

    local isWeapon = itemType == GEnums.ItemType.Weapon
    if isWeapon then
        return true, self._JumpToWeapon
    end

    local isWeaponGem = itemType == GEnums.ItemType.WeaponGem
    if not isWeaponGem then
        return false
    end

    local gemInst = CharInfoUtils.getGemByInstId(instId)
    if not gemInst then
        return false
    end

    local weaponInstId = gemInst.weaponInstId
    if not weaponInstId or weaponInstId <= 0 then
        return false
    end

    return true, self._JumpToWeaponGem
end

ValuableDepotCtrl._CheckIfCanUse = HL.Method(HL.String, HL.Int).Return(HL.Boolean, HL.Opt(HL.Function)) << function(self, itemId, instId)
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    local itemType = itemData.type

    if itemType == GEnums.ItemType.APItem or itemType == GEnums.ItemType.APLimitItem then
        return true, function()
            PhaseManager:OpenPhase(PhaseId.StaminaPopUp, { itemId = itemId, instId = instId, isQuickExchange = true })
        end
    end

    if itemType == GEnums.ItemType.APFeedIn then
        return true, function()
            UIManager:Open(PanelId.StaminaPotion, itemId)
        end
    end

    if itemType == GEnums.ItemType.ItemCase then
        local useFunc = function()
            local _, chestData = Tables.usableItemChestTable:TryGetValue(itemId)
            local caseType = GEnums.ItemCaseType.SelfSelected
            if chestData then
                caseType = chestData.type
            end

            if caseType == GEnums.ItemCaseType.SelfSelectedBP then
                UIManager:Open(PanelId.BattlePassWeaponCase, { itemId = itemId })
            elseif caseType == GEnums.ItemCaseType.SelfSelectedChar then
                local arg = {
                    chestItemId = itemId,
                    isFromChest = true,
                }
                UIManager:Open(PanelId.GachaOptional, arg)
            else
                PhaseManager:OpenPhase(PhaseId.UsableItemChest, { itemId = itemId })
            end
        end
        return true, useFunc
    end

    if itemType == GEnums.ItemType.MapDetector then
        local state = false
        local useFunc = function()
            UIManager:Open(PanelId.MapDetectPopUp, itemId)
        end
        if not GameWorld.mapRegionManager:IsUnlockAllMistMapInLevel(GameWorld.worldInfo.curLevelId) then
            state = false
        else
            state = true
        end
        return state, useFunc
    end

    if itemType == GEnums.ItemType.GemLockedTermBox then
        local useFunc = function()
            UIManager:Open(PanelId.GemCustomizationBox, itemId)
        end
        return true, useFunc
    end

    if itemType == GEnums.ItemType.MonthlycardItem then
        local useFunc = function()
            CashShopUtils.TryUseMonthlyItem(itemId, instId)
        end
        return true, useFunc
    end

    if itemType == GEnums.ItemType.BPTicketLTItem then
        local canUse, cantReason = BattlePassUtils.CheckBattlePassItemCanUse(itemId)
        if not canUse then
            return false
        end
        local useFunc = function()
            BattlePassUtils.TryUseBattlePassItem(itemId, instId)
        end
        return true, useFunc
    end

    if itemType == GEnums.ItemType.RenameCardItem then
        local useFunc = function()
            UIManager:Open(PanelId.RenameCardPopup, { itemId = itemId, instId = instId })
        end
        return true, useFunc
    end

    return false
end

ValuableDepotCtrl._CheckIfShowTips = HL.Method(HL.String).Return(HL.Boolean, HL.Opt(HL.String)) << function(self, itemId)
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    if itemData.type == GEnums.ItemType.MapDetector then
        local state = false
        local text = ""
        if not GameWorld.mapRegionManager:IsUnlockAllMistMapInLevel(GameWorld.worldInfo.curLevelId) then
            state = true
            text = Language.LUA_MAP_USE_DETECT_MIST_LOCKED_TOAST
        end
        return state, text
    end

    if itemData.type == GEnums.ItemType.BPTicketLTItem then
        local canUse, cantReason = BattlePassUtils.CheckBattlePassItemCanUse(itemId)
        if not canUse then
            return true, cantReason
        end
    end

    return false
end

ValuableDepotCtrl._UpdateDecoIcons = HL.Method(HL.Opt(HL.String)) << function(self, id)
    if not id or string.isEmpty(id) then
        return
    end

    local data = Tables.itemTable:GetValue(id)
    self.view.icon:LoadSprite(self.view.config.USE_BIG_ICON and UIConst.UI_SPRITE_ITEM_BIG or UIConst.UI_SPRITE_ITEM, data.iconId)
end





ValuableDepotCtrl._ReadItem = HL.Method(HL.Number) << function(self, index)
    if index <= 0 then
        return
    end
    local info = self.m_curShowItemList[index]
    if not info then
        return
    end
    if info.instId then
        GameInstance.player.inventory:ReadNewItem(info.id, info.instId)
    else
        GameInstance.player.inventory:ReadNewItem(info.id)
    end
end

ValuableDepotCtrl.m_readItemIds = HL.Field(HL.Table)

ValuableDepotCtrl.m_readItemInstIds = HL.Field(HL.Table)

ValuableDepotCtrl._ReadCurShowingItems = HL.Method() << function(self)
    local tabInfo = self.m_tabsInfo[self.m_curTabIndex]
    if not tabInfo then
        return
    end

    if not next(self.m_readItemIds) and not next(self.m_readItemInstIds) then
        return
    end

    local itemIds = {}
    for k, _ in pairs(self.m_readItemIds) do
        table.insert(itemIds, k)
    end
    self.m_readItemIds = {}

    local instIds = {}
    for k, _ in pairs(self.m_readItemInstIds) do
        table.insert(instIds, k)
    end
    self.m_readItemInstIds = {}

    GameInstance.player.inventory:ReadNewItems(itemIds, tabInfo.type, instIds)
end


ValuableDepotCtrl.CheckLTItemExpire = HL.Method() << function(self)
    if inventorySystem.waitConfirmExpireLTItemDict.Count <= 0 then
        return
    end

    local recordIds = {}
    for recordId, _ in cs_pairs(inventorySystem.waitConfirmExpireLTItemDict) do
        table.insert(recordIds, recordId)
    end
    inventorySystem:SendConfirmLTItemsExpireReq(recordIds)
end

ValuableDepotCtrl.OnLTItemExpire = HL.Method(HL.Any) << function(self, arg)
    local recordId = unpack(arg)
    inventorySystem:SendConfirmLTItemsExpireReq({ recordId })
end

ValuableDepotCtrl.OnUseItem = HL.Method(HL.Any) << function(self, arg)
    local itemId, result = unpack(arg)
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    local itemType = itemData.type
    if itemType == GEnums.ItemType.MonthlycardItem then
        Notify(MessageConst.SHOW_CASH_SHOP_TOAST,
            { text = Language.LUA_CASHSHOP_MONTHLYCARD_ON_USE_ITEM_SUCC})
    elseif itemType == GEnums.ItemType.BPTicketLTItem then
        if UIManager:IsOpen(PanelId.RewardsPopUpForSystem) then
            self.m_bpTicketItemId = itemId
            return
        end
        self:TryPopupBp(itemId)
    end
end



ValuableDepotCtrl.m_bpTicketItemId = HL.Field(HL.String) << ''

ValuableDepotCtrl.OnBPTicketReward = HL.Method(HL.Any) << function(self, args)
    local bundles = unpack(args)
    local rewardPanelArg = {
        items = bundles,
        onComplete = function()
            local bpTicketItemId = self.m_bpTicketItemId
            self.m_bpTicketItemId = ''
            if not string.isEmpty(bpTicketItemId) then
                self:TryPopupBp(bpTicketItemId)
            end
        end,
    }
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardPanelArg)
end

ValuableDepotCtrl.TryPopupBp = HL.Method(HL.String) << function(self, itemId)
    local hasTrack, trackType = BattlePassUtils.GetBattlePassTicketTrackType(itemId)
    if not hasTrack or trackType == nil then
        return
    end
    BattlePassUtils.ShowTrackReward(trackType, false, nil, function()
        local phaseArg = {
            panelId = 'BattlePassPlan',
        }
        if hasTrack then
            phaseArg.panelArgs = {
                showTrackUnlockType = trackType,
            }
        end
        PhaseManager:GoToPhase(PhaseId.BattlePass, phaseArg)
    end)
end


ValuableDepotCtrl.ShowLTItemExpirePopup = HL.Method(HL.Any) << function(self, arg)
    local itemInfos = {}
    local itemInfoMap = {}
    local recordIds = unpack(arg)
    local idCount = recordIds.Count - 1
    for i = 0, idCount do
        local recordId = recordIds[i]
        local hasValue, itemBundleList = inventorySystem.waitConfirmExpireLTItemDict:TryGetValue(recordId)
        if hasValue then
            local itemListMaxIndex = itemBundleList.Count - 1
            for i = 0, itemListMaxIndex do
                local itemBundle = itemBundleList[i]
                local itemId = itemBundle.Id
                local itemCount = itemBundle.Count
                local itemInfo = itemInfoMap[itemId]
                if itemInfo == nil then
                    local itemCfg = Tables.itemTable[itemId]
                    itemInfo = {
                        id = itemId,
                        count = itemCount,

                        sortId1 = itemCfg.sortId1,
                        sortId2 = itemCfg.sortId2,
                    }
                    itemInfoMap[itemId] = itemInfo
                    table.insert(itemInfos, itemInfo)
                else
                    itemInfo.count = itemInfo.count + itemCount
                end
            end
        else
            logger.error("过期物品Record数据丢失！RecordId：" .. recordId)
        end
    end
    table.sort(itemInfos, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))

    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_LIMIT_ITEM_EXPIRE_POPUP_TITLE,
        items = itemInfos,
        hideCancel = true,
    })
end






ValuableDepotCtrl.m_lockToggleBindingId = HL.Field(HL.Number) << -1

ValuableDepotCtrl._InitController = HL.Method() << function(self)
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(JsonConst.VALUABLE_DEPOT_MONEY_IDS)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_lockToggleBindingId = self:BindInputPlayerAction("item_lock_toggle", function()
        self.view.itemInfoNode.lockToggle.view.toggle.isOn = not self.view.itemInfoNode.lockToggle.view.toggle.isOn
    end)

    self.view.itemInfoNode.itemFlagNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        self.view.itemInfoNode.itemFlagControllerFocusHintNode.gameObject:SetActive(not isTopLayer)
    end)
    self.view.destroyNode.equipRightNode.previewItemNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
    self.view.itemListNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        local selectedCell = self.m_getItemCell(self.view.itemScrollList:Get(CSIndex(self.m_curItemIndex)))
        if selectedCell then
            selectedCell:SetSelected(not isTopLayer)
        end
    end)
    UIUtils.bindHyperlinkPopup(self, "ValuableDepot", self.view.inputGroup.groupId)
end






ValuableDepotCtrl.OnItemCountChanged = HL.StaticMethod(HL.Table) << function(args)

    if args == nil then
        return
    end
    local itemDict = unpack(args)
    if itemDict.Count == 0 then
        return
    end
    for id, diffCount in cs_pairs(itemDict) do
        if Tables.itemTable:TryGetValue(id) and Tables.itemTable[id].valuableDepotRedDot and diffCount > 0 then
            RedDotUtils.setNewObtainedImportantValuableDepotItem(id, true)
        end
    end
end
HL.Commit(ValuableDepotCtrl)
