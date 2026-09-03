local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Inventory
InventoryCtrl = HL.Class('InventoryCtrl', uiCtrl.UICtrl)






InventoryCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_EXIT_FACTORY_MODE] = 'AutoCloseSelfOnInterrupt',
    [MessageConst.ON_ENTER_FACTORY_MODE] = 'AutoCloseSelfOnInterrupt',
    [MessageConst.ON_SQUAD_INFIGHT_CHANGED] = 'AutoCloseSelfOnInterrupt',
    [MessageConst.ON_TELEPORT_SQUAD] = 'AutoCloseSelfOnInterrupt',
    [MessageConst.DEAD_ZONE_ROLLBACK] = 'AutoCloseSelfOnInterrupt',
    [MessageConst.ALL_CHARACTER_DEAD] = 'AutoCloseSelfOnInterrupt',

    [MessageConst.ON_START_UI_DRAG] = 'OnOtherStartDragItem',
    [MessageConst.ON_END_UI_DRAG] = 'OnOtherEndDragItem',

    [MessageConst.ON_CHANGE_THROW_MODE] = 'OnChangeThrowMode',
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = "OnSystemUnlock",

    [MessageConst.ON_ITEM_BAG_ABANDON_IN_BAG_SUCC] = "OnItemBagAbandonInBagSucc",

    [MessageConst.ON_CHANGE_SPACESHIP_DOMAIN_ID] = 'OnChangeSpaceshipDomainId',
    [MessageConst.ON_ITEM_BAG_TOGGLE_ABANDON_DROP] = 'OnToggleAbandonDropValid',
}


InventoryCtrl.m_shouldHidePanelsOnShow = HL.Field(HL.Boolean) << false

InventoryCtrl.m_depotInited = HL.Field(HL.Boolean) << false

InventoryCtrl.m_opened = HL.Field(HL.Boolean) << true

InventoryCtrl.m_isFocusingInventory = HL.Field(HL.Boolean) << false

InventoryCtrl.m_abandonItemDropHelper = HL.Field(HL.Forward('UIDropHelper'))

InventoryCtrl.m_oriPaddingBottom = HL.Field(HL.Number) << 0

InventoryCtrl.m_abandonValid = HL.Field(HL.Boolean) << true



InventoryCtrl.m_waitInitNaviTarget = HL.Field(HL.Boolean) << true

InventoryCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))




InventoryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        self:_OnClickClose()
    end)

    self.view.manualCraftRedDot:InitRedDot("ManualCraftBtn")
    self.view.manualCraftBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.ManualCraft)
    end)

    self.view.switchDepotBtn.onClick:AddListener(function()
        self:_OnClickSwitchDepot()
    end)

    self.view.sortButton.onClick:AddListener(function()
        self:_OnClickSortBtn()
    end)

    local inWeekRaid = Utils.isInWeekRaid()
    if inWeekRaid then
        self.m_weekRaidConvertRate = GameInstance.player.weekRaidSystem.ItemValueRate
        self.view.stateController:SetState("NoDepot")
        self.view.stateController:SetState("WeekRaid")
        self.view.weekRaidTitleBar.btnClose.onClick:AddListener(function()
            self:_OnClickClose()
        end)
        self.view.weekRaidTitleBar.helpBtn.onClick:AddListener(function()
            UIManager:Open(PanelId.InstructionBook, "week_raid_item_bag")
        end)
        local blurRoot = GameObject("blur")
        blurRoot.transform:SetParent(self.view.transform, false)

        local blur = blurRoot:AddComponent(typeof(CS.Beyond.UI.FullScreenSceneBlurMarker))
        blur.useWhiteBlur = true
    else
        self.view.stateController:SetState("NotWeekRaid")
    end

    self.view.itemBag:InitItemBag(function(itemId, cell, csIndex)
        self:_OnClickItem(itemId, cell, csIndex)
    end, {
        canPlace = true,
        canSplit = true,
        canUse = true,
        canClear = true,
        customOnUpdateCell = function(cell, itemBundle, csIndex)
            self:_CustomOnUpdateItemBagCell(cell, itemBundle, csIndex)
        end,
    })
    self.m_oriPaddingBottom = self.view.itemBag.itemBagContent.view.itemList:GetPadding().bottom

    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)

    self:_InitQuickStash()
    self:_InitDestroyNode()

    if inWeekRaid then
        self.view.walletBarPlaceholder.gameObject:SetActive(false)
    else
        self.view.walletBarPlaceholder.gameObject:SetActive(true)
        self.view.walletBarPlaceholder:InitWalletBarPlaceholder(JsonConst.INVENTORY_MONEY_IDS)
    end

    self.m_abandonItemDropHelper = UIUtils.initUIDropHelper(self.view.abandonItemMask, {
        isAbandon = true,
        acceptTypes = UIConst.ABANDON_ITEM_DROP_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            self:_OnAbandonItem(dragHelper)
        end,
    })
    self.view.abandonItemMask.gameObject:SetActive(false)


    self:BindInputPlayerAction("close_inventory", function()
        self:_OnClickClose()
    end)

    self.m_moveModeBindingGroupId = InputManagerInst:CreateGroup(self.view.itemBag.itemBagContent.view.inputBindingGroupMonoTarget.groupId)
    local exitModeBindingId = self:BindInputPlayerAction("common_cancel", function()
        self:_ToggleMoveMode(false, true)
    end, self.m_moveModeBindingGroupId)
    InputManagerInst:SetBindingText(exitModeBindingId, Language.LUA_EXIT_INV_MOVE_MODE)

    self.m_moveItemQuickMoveBindingId = self:BindInputPlayerAction("inv_item_bag_move_item_quick_move", function()
        self:_MoveItemQuickMove()
    end, self.m_moveModeBindingGroupId)
    InputManagerInst:ToggleGroup(self.m_moveModeBindingGroupId, false)
    self.view.moveHintItem.gameObject:SetActive(false)
    self.view.moveItemFrame.gameObject:SetActive(false)
end

InventoryCtrl.OnClose = HL.Override() << function(self)
    if self.m_isFocusingInventory then
        GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), true, {1})
        self.m_isFocusingInventory = false
    end
    self:_ResumeWorld()
end

InventoryCtrl.OnHide = HL.Override() << function(self)
    self:_ResetDragState()
    if self.m_isFocusingInventory then
        GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), true, {1})
        self.m_isFocusingInventory = false
    end
    self:_ResumeWorld()
end

InventoryCtrl.OnShow = HL.Override() << function(self)
    self.m_opened = true

    self.view.manualCraftBtn.gameObject:SetActive(PhaseManager:CheckCanOpenPhase(PhaseId.ManualCraft))

    if Utils.isInFight() then

        self:PlayAnimationOutWithCallback(function()
            self.m_opened = false
            PhaseManager:ExitPhaseFast(PhaseId.Inventory)
        end)
        return
    end

    if Utils.isInSafeZone() then
        self.m_isFocusingInventory = true
        GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), false, {1})
    end

    if Utils.isInBlackbox() then
        self.view.walletBarPlaceholder.gameObject:SetActiveIfNecessary(false)
    end

    if Utils.isInWeekRaid() then
        self:_FreezeWorld()
    end

    if self.view.depot.gameObject.activeInHierarchy then
        AudioAdapter.PostEvent("Au_UI_Menu_InventoryWarePanel_Open")
    else
        AudioAdapter.PostEvent("Au_UI_Menu_InventoryPanel_Open")
    end

    self:_RefreshWeekRaidBottomNode()

    self:_InitControllerSideMenuBtn()
end

InventoryCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if not string.isEmpty(self.m_targetItemId) then
        self:_GotoItem(self.m_targetItemId)
        self.m_targetItemId = ""
    end
end




InventoryCtrl.OpenInventoryPanel = HL.Method(HL.Opt(HL.String)) << function(self, itemId)
    if itemId then
        self.m_targetItemId = itemId
    end
    self:_Refresh()
    self:_ToggleDestroyMode(false, true)
    self:_ToggleMoveMode(false)
    self.view.depot:ToggleDestroyMode(false, true)
end

InventoryCtrl.ResetOnClose = HL.Method() << function(self)
    self:_ToggleDestroyMode(false, true)
    self:_ToggleMoveMode(false)
    self.view.depot:ToggleDestroyMode(false, true)
    InputManagerInst.controllerNaviManager:TryRemoveLayer(self.naviGroup)

    if DeviceInfo.usingController then
        local facQuickBarNaviGroup = self.view.facQuickBarPlaceHolder:GetNaviGroup()
        if facQuickBarNaviGroup then
            self:_ChangeFacQuickBarNaviPartner(facQuickBarNaviGroup, true)
        end
    end

    self.m_opened = false
    self:Hide()

    self.view.itemBag.itemBagContent:ReadCurShowingItems()
    self.view.depot.view.depotContent:ReadCurShowingItems()

    self.view.itemBag.itemBagContent:StopUpdate(true)
    self.view.depot.view.depotContent:StopUpdate(true)
end

InventoryCtrl.OnOtherStartDragItem = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if self:IsHide() then
        return
    end
    if not DeviceInfo.usingController then
        self.view.itemBagButtons.alpha = self.view.config.DRAGGING_BUTTON_ALPHA
        self.view.itemBagButtonsInputBindingGroupMonoTarget.enabled = false
        self.view.depot.view.bottomNode.alpha = self.view.config.DRAGGING_BUTTON_ALPHA
        self.view.depotBottomNodeInputBindingGroupMonoTarget.enabled = false
    end
    if UIUtils.isTypeDropValid(dragHelper, UIConst.ABANDON_ITEM_DROP_ACCEPT_INFO) then
        self.view.abandonItemMask.gameObject:SetActive(true)
    end
end

InventoryCtrl.OnOtherEndDragItem = HL.Method(HL.Opt(HL.Forward('UIDragHelper'))) << function(self, dragHelper)
    if self:IsHide() then
        return
    end
    self:_ResetDragState()
end

InventoryCtrl._ResetDragState = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        self.view.itemBagButtons.alpha = 1
        self.view.itemBagButtonsInputBindingGroupMonoTarget.enabled = true
        self.view.depot.view.bottomNode.alpha = 1
        self.view.depotBottomNodeInputBindingGroupMonoTarget.enabled = true
    end
    self.view.abandonItemMask.gameObject:SetActive(false)
end


InventoryCtrl.OnChangeThrowMode = HL.Method(HL.Table) << function(self, args)
    local data = unpack(args)
    local inThrowMode = data.valid
    if inThrowMode then
        self:_OnClickClose()
    end
end

InventoryCtrl.OnSystemUnlock = HL.Method(HL.Table) << function(self, arg)
    local systemIndex = unpack(arg)
    if systemIndex == GEnums.UnlockSystemType.ManualCraft then
        self.view.manualCraftBtn.gameObject:SetActive(true)
    elseif systemIndex == GEnums.UnlockSystemType.ColoredBagItem then
        self:_RefreshQuickStashDataAndCell()
        if self.view.depot.m_inInit then
            self:_InitDepot()
        end
    end
end

InventoryCtrl.AutoCloseSelfOnInterrupt = HL.Method(HL.Opt(HL.Any)) << function(self)
    if not self:IsShow(true) then
        return
    end


    Notify(MessageConst.HIDE_NAVI_TARGET_ACTION_MENU)
    self:_OnClickClose()
end

InventoryCtrl.OnChangeSpaceshipDomainId = HL.Method(HL.Any) << function(self, _)
    self:_InitDepot()
    self:_RefreshSwitchDepotState()
end




InventoryCtrl._UpdateMouseHint = HL.Method() << function(self)
    if not DeviceInfo.usingKeyboard or self.m_inDestroyMode or Utils.isDepotManualInOutLocked() then
        self.view.moveItemMouseHintNode.gameObject:SetActive(false)
        return
    end
    self.view.moveItemMouseHintNode.gameObject:SetActive(true)

    local hasDepot = self.view.depotNode.gameObject.activeSelf
    self.view.moveItemMouseHintNode.moveAllHint.gameObject:SetActive(hasDepot)
    self.view.moveItemMouseHintNode.moveGridHint.gameObject:SetActive(hasDepot)
    self.view.moveItemMouseHintNode.moveHalfHint.gameObject:SetActive(true)
    self.view.moveItemMouseHintNode.moveHalfHint:SetText(hasDepot and Language.LUA_INVENTORY_MOVE_HALT_WITH_DEPOT or Language.LUA_INVENTORY_MOVE_HALT_WITHOUT_DEPOT)
end

InventoryCtrl._ChangeFacQuickBarNaviPartner = HL.Method(HL.Any, HL.Boolean) << function(self, facQuickBarNaviGroup, isAdd)
    if not facQuickBarNaviGroup then
        return
    end
    local batNaviGroup = self.view.itemBag.itemBagContent.view.itemListSelectableNaviGroup
    local depotNaviGroup = self.view.depot.view.depotContent.view.itemListSelectableNaviGroup
    facQuickBarNaviGroup:TryChangeNaviPartnerOnUp(batNaviGroup, isAdd)
    facQuickBarNaviGroup:TryChangeNaviPartnerOnUp(depotNaviGroup, isAdd)
    batNaviGroup:TryChangeNaviPartnerOnDown(facQuickBarNaviGroup, isAdd)
    depotNaviGroup:TryChangeNaviPartnerOnDown(facQuickBarNaviGroup, isAdd)
end

InventoryCtrl._Refresh = HL.Method() << function(self)
    self.m_waitInitNaviTarget = true
    local naviGroupInfos = {}
    table.insert(naviGroupInfos, {
        naviGroup = self.view.itemBag.itemBagContent.view.itemListSelectableNaviGroup,
        text = Language.LUA_INV_NAVI_SWITCH_TO_ITEM_BAG,
    })

    self:_RefreshMissionItemIds()

    if not self.m_weekRaidConvertRate then
        if Utils.isInSafeZone() then
            self.view.stateController:SetState("HasDepot")

            if not self.m_depotInited then
                self:_InitDepot()
                self.m_depotInited = true
            else
                if string.isEmpty(self.m_targetItemId) then
                    self.view.depot.depotContent.view.itemList:SetTop()
                end
                self.view.depot.depotContent.m_missionItemIds = self.m_missionItemIds
            end
            self.view.depot.view.depotContent:StartUpdate()
            table.insert(naviGroupInfos, {
                naviGroup = self.view.depot.depotContent.view.itemListSelectableNaviGroup,
                text = Language.LUA_INV_NAVI_SWITCH_TO_DEPOT,
            })

            self.view.blockDepotManualInOutNode.gameObject:SetActive(Utils.isDepotManualInOutLocked())

            self:_RefreshSwitchDepotState()
        else
            self.view.stateController:SetState("NoDepot")
        end
    end

    self.view.itemBag.itemBagContent.m_missionItemIds = self.m_missionItemIds
    self.view.itemBag.itemBagContent:StartUpdate()
    if string.isEmpty(self.m_targetItemId) then
        self.view.itemBag.itemBagContent.view.itemList:SetTop()
    end
    self:_RefreshQuickStash()

    local showFacQuickBar = Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacMode) and not self.m_weekRaidConvertRate
    self.view.facQuickBarPlaceHolder.gameObject:SetActive(showFacQuickBar)
    if showFacQuickBar then
        self.view.facQuickBarPlaceHolder:InitFacQuickBarPlaceHolder({
            controllerSwitchArgs = {
                hintPlaceholder = self.view.controllerHintPlaceholder,
            }
        })
        if DeviceInfo.usingController then
            local facQuickBarNaviGroup = self.view.facQuickBarPlaceHolder:GetNaviGroup()
            if facQuickBarNaviGroup then
                table.insert(naviGroupInfos, {
                    naviGroup = facQuickBarNaviGroup,
                    text = Language.LUA_NAVI_SWITCH_TO_FAC_QUICK_BAR,
                })
                self:_ChangeFacQuickBarNaviPartner(facQuickBarNaviGroup, true)
            end
        end
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId, self.view.facQuickBarPlaceHolder:GetInputBindingGroupId()})
    else
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    end

    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end

InventoryCtrl._InitDepot = HL.Method() << function(self)
    self.view.depot:InitDepot(GEnums.ItemValuableDepotType.Factory, nil, {
        canPlace = true,
        canSplit = false,
        canClear = true,

        missionItemIds = self.m_missionItemIds,

        itemMoveTarget = UIConst.ITEM_MOVE_TARGET.ItemBag,
        onToggleDestroyMode = function(active)
            self.m_naviGroupSwitcher:ToggleActive(not active and not self.m_weekRaidConvertRate)
        end,
        customOnUpdateCell = function(cell, itemBundle, luaIndex)
            if itemBundle.count and itemBundle.count > 0 and cell:IsQuickDropTargetValid() then
                cell.item:AddHoverBinding("common_quick_drop", function()
                    cell:QuickDrop()
                end)
            end
            cell.item.canSetQuickBar = true
        end,
    })
end

InventoryCtrl._RefreshSwitchDepotState = HL.Method() << function(self)
    if Utils.isInSpaceShip() then
        local depotInChapter = GameInstance.player.inventory.factoryDepot:GetOrFallback(Utils.getCurrentScope())
        self.view.switchDepotBtn.gameObject:SetActive(depotInChapter.Count > 1)

        local domainId = GameInstance.player.inventory.spaceshipDomainId
        local domainData = Tables.domainDataTable:GetValue(domainId)
        self.view.switchDepotIcon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, domainData.domainIcon)
        self.view.switchDepotLineImg.color = UIUtils.getColorByString(domainData.domainColor)
    else
        self.view.switchDepotBtn.gameObject:SetActive(false)
    end
end

InventoryCtrl._OnClickClose = HL.Method() << function(self)
    if not self.m_opened then

        return
    end
    local succ = PhaseManager:PopPhase(PhaseId.Inventory)
    if succ then
        self.m_opened = false
    end
end

InventoryCtrl._OnDropItem = HL.Method(HL.Userdata, HL.Forward('UIDragHelper')) << function(self, eventData, dragHelper)
    local depotContent = self.view.depot.view.depotContent
    if depotContent.dropHelper:Accept(dragHelper) then
        depotContent.dropHelper.uiDropItem.onDropEvent:Invoke(eventData)
        return
    end
end

InventoryCtrl._OnClickItem = HL.Method(HL.String, HL.Any, HL.Number) << function(self, itemId, cell, csIndex)
    cell.item:Read()

    if self.m_inMoveMode then
        local inventory = GameInstance.player.inventory
        local itemBundle = self.view.itemBag.itemBagContent.m_itemBag.slots[self.m_moveSlotIndex]
        UIUtils.playItemDropAudio(itemBundle.id)
        inventory:MoveInItemBag(Utils.getCurrentScope(), self.m_moveSlotIndex, csIndex)
        self:_ToggleMoveMode(false)
        return
    end

    if not self.m_inDestroyMode then
        if DeviceInfo.usingController then
            cell.item:ShowActionMenu()
            return
        elseif DeviceInfo.usingKeyboard then
            local canQuickDrop = self.view.depot.gameObject.activeInHierarchy and not Utils.isDepotManualInOutLocked()
            if canQuickDrop then
                local mode = UIUtils.getMovingItemMode()
                if mode then
                    GameInstance.player.inventory:ItemBagMoveToFactoryDepot(Utils.getCurrentScope(), Utils.getCurrentChapterId(), csIndex, mode)
                    return
                end
            end
        end
        cell.item:ShowTips()
        return
    end

    local showTips = not self.m_destroyInfo[csIndex] and not DeviceInfo.usingController
    self:_OnClickItemInDestroyMode(csIndex)
    if showTips then
        local posInfo = {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.itemBag.transform,
            isSideTips = true,
        }
        cell.item.canPlace = false
        cell.item.canSplit = false
        cell.item.canUse = false
        cell.item:ShowTips(posInfo, function()
            if self.m_showingDestroyItemCSIndex == csIndex then
                self.m_showingDestroyItemCSIndex = -1
            end
        end)
    end
end

InventoryCtrl._OnClickSortBtn = HL.Method() << function(self)
    GameInstance.player.inventory:SortItemBag(Utils.getCurrentScope())
end

InventoryCtrl._OnClickSwitchDepot = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.FacDepotSwitching)
end




InventoryCtrl.m_quickStashSettingInfo = HL.Field(HL.Table)

InventoryCtrl.m_quickStashCells = HL.Field(HL.Forward('UIListCache'))

InventoryCtrl._InitQuickStash = HL.Method() << function(self)
    local quickStashNode = self.view.quickStashNode

    quickStashNode.settingCell.gameObject:SetActive(false)

    quickStashNode.confirmBtn.onClick:AddListener(function()
        self:_QuickStash()
    end)
    quickStashNode.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "item_bag_quick_stash")
    end)
    quickStashNode.settingBtn.onClick:AddListener(function()
        self:_ToggleQuickStashSetting(not self.view.quickStashNode.settingList.gameObject.activeSelf)
    end)
    quickStashNode.closeBtn.onClick:AddListener(function()
        self:_ToggleQuickStashSetting(false)
    end)
    quickStashNode.autoCloseArea.onTriggerAutoClose:AddListener(function()
        if DeviceInfo.usingController then
            return
        end
        self:_ToggleQuickStashSetting(false)
    end)

    quickStashNode.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            self:_ToggleQuickStashSetting(false)
        end
        quickStashNode.backHint.gameObject:SetActive(isFocused and DeviceInfo.usingController)
    end)

    self.m_quickStashCells = UIUtils.genCellCache(quickStashNode.settingCell)
    self:_RefreshQuickStashDataAndCell()

    self:_ToggleQuickStashSetting(false, true)
end

InventoryCtrl._RefreshQuickStashDataAndCell = HL.Method() << function(self)
    self.m_quickStashSettingInfo = {}
    table.insert(self.m_quickStashSettingInfo, {
        index = 1,
        name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_ORE_AND_PRODUCT,
        icon = "icon_item_type_ore_and_product",
        showingType = GEnums.ItemShowingType.Ore,
        showingType2 = GEnums.ItemShowingType.Product,
        extraCheckFunc = function(itemId)
            return not UIConst.ITEM_BAG_QUICK_STASH_USEFUL_ITEMS[itemId]
        end,
        defaultIsOn = true,
    })
    table.insert(self.m_quickStashSettingInfo, {
        index = 2,
        name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_PLANT,
        showingType = GEnums.ItemShowingType.Plant,
        defaultIsOn = true,
    })
    table.insert(self.m_quickStashSettingInfo, {
        index = 8,
        name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_USEFUL_ITEMS,
        icon = "icon_item_type_useful_items",
        showingType = GEnums.ItemShowingType.Product,
        extraCheckFunc = function(itemId)
            return not Utils.isPortableDevice(itemId)
        end,
        extraCheckFunc = function(itemId)
            return UIConst.ITEM_BAG_QUICK_STASH_USEFUL_ITEMS[itemId]
        end,
        defaultIsOn = false,
    })
    if Utils.isSystemUnlocked(GEnums.UnlockSystemType.ColoredBagItem) then
        table.insert(self.m_quickStashSettingInfo, {
            index = 9,
            name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_PORTABLE_DEVICE,
            showingType = GEnums.ItemShowingType.PortableDevice,
            extraCheckFunc = function(itemId, csIndex)
                return Utils.isPortableDevice(itemId) and not self.view.itemBag.itemBagContent.m_itemBag:IsColoredSlotActive(itemId, csIndex)
            end,
            icon = "icon_item_type_portable_device",
            defaultIsOn = false,
        })
    end
    table.insert(self.m_quickStashSettingInfo, {
        index = 3,
        name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_DOODAD,
        showingType = GEnums.ItemShowingType.Doodad,
        defaultIsOn = true,
    })
    table.insert(self.m_quickStashSettingInfo, {
        index = 4,
        name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_NURTURANCE,
        showingType = GEnums.ItemShowingType.Nurturance,
        defaultIsOn = true,
    })
    table.insert(self.m_quickStashSettingInfo, {
        index = 5,
        name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_USABLE,
        showingType = GEnums.ItemShowingType.Usable,
        defaultIsOn = false,
    })
    table.insert(self.m_quickStashSettingInfo, {
        index = 6,
        name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_PRODUCER,
        showingType = GEnums.ItemShowingType.Producer,
        extraCheckFunc = function(itemId)
            local itemData = Tables.itemTable[itemId]
            return itemData.type == GEnums.ItemType.NormalBuilding or itemData.type == GEnums.ItemType.SpecialBuilding
        end,
        defaultIsOn = true,
    })
    table.insert(self.m_quickStashSettingInfo, {
        index = 7,
        name = Language.LUA_INVENTORY_QUICK_STASH_TYPE_FUNC_BUILDING,
        showingType = GEnums.ItemShowingType.Producer,
        extraCheckFunc = function(itemId)
            local itemData = Tables.itemTable[itemId]
            return itemData.type == GEnums.ItemType.FuncBuilding
        end,
        icon = "icon_item_type_tools_building_func",
        defaultIsOn = false,
    })

    local settingValue = GameInstance.player.inventory.itemBagBatchMoveFlag
    local useDefault = settingValue == 0
    for _, info in ipairs(self.m_quickStashSettingInfo) do
        local index = info.index
        if useDefault then
            info.isOn = info.defaultIsOn
        else
            info.isOn = ((settingValue >> index) & 1) == 1
        end
    end

    self.m_quickStashCells:Refresh(#self.m_quickStashSettingInfo, function(cell, index)
        local info = self.m_quickStashSettingInfo[index]
        cell.toggle.isOn = info.isOn
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            self:_ToggleQuickStashSettingCell(index, isOn)
        end)
        cell.topLineImg.enabled = index ~= 1
        cell.selectName.text = info.name
        cell.notSelectName.text = info.name
        local icon = info.icon
        if not icon then
            local data = Tables.itemShowingTypeTable[info.showingType:GetHashCode()]
            icon = data.icon
        end
        cell.selectIcon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, icon)
        cell.notSelectIcon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, icon)
        cell.gameObject.name = "Cell_" .. index
    end)
end

InventoryCtrl._RefreshQuickStash = HL.Method() << function(self)
    local canQuickStash = Utils.isInSafeZone()
    self.view.quickStashNode.gameObject:SetActive(canQuickStash)
    if canQuickStash then
        self:_RefreshQuickStashDataAndCell()
    end
end

InventoryCtrl._QuickStash = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        self:_ToggleQuickStashSetting(false)
    end

    if Utils.isDepotManualInOutLocked() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_DEPOT_MANUAL_IN_OUT_LOCKED)
        return
    end

    local stashItemIndexList = {}
    local validTypes = {}
    local typeList = {}
    local processAct = function(info, showingType)
        if not info.extraCheckFunc then
            validTypes[showingType] = true
        else
            if not validTypes[showingType] then
                validTypes[showingType] = { info.extraCheckFunc }
            else
                table.insert(validTypes[showingType], info.extraCheckFunc)
            end
        end
        table.insert(typeList, tostring(info.showingType))
    end
    for _, info in ipairs(self.m_quickStashSettingInfo) do
        if info.isOn then
            processAct(info, info.showingType)
            if info.showingType2 then
                processAct(info, info.showingType2)
            end
        end
    end
    for csIndex, itemBundle in pairs(self.view.itemBag.itemBagContent.m_itemBag.slots) do
        local id = itemBundle.id
        if not string.isEmpty(id) then
            local itemData = Tables.itemTable:GetValue(id)
            local valid = validTypes[itemData.showingType]
            if valid then
                if valid == true then
                    table.insert(stashItemIndexList, csIndex)
                else
                    for _, func in ipairs(valid) do
                        if func(id, csIndex) then
                            table.insert(stashItemIndexList, csIndex)
                            break
                        end
                    end
                end
            end
        end
    end
    if #stashItemIndexList == 0 then
        return
    end
    self.view.itemBag.itemBagContent:ReadCurShowingItems()
    self.view.depot.depotContent:ReadCurShowingItems()
    GameInstance.player.inventory:ItemBagMoveToFactoryDepot(Utils.getCurrentScope(), Utils.getCurrentChapterId(), stashItemIndexList)

    EventLogManagerInst:GameEvent_BagBatchManage(typeList)
end

InventoryCtrl._ToggleQuickStashSettingCell = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, active)
    local info = self.m_quickStashSettingInfo[index]
    if active == nil then
        active = not info.isOn
    end
    info.isOn = active
end

InventoryCtrl._ToggleQuickStashSetting = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, noAnimation)
    local quickStashNode = self.view.quickStashNode
    quickStashNode.autoCloseArea.enabled = active

    if quickStashNode.settingList.gameObject.activeSelf == active then
        return
    end

    if not active then
        self:_SaveCurQuickStashSetting()
    end

    quickStashNode.settingList.transform:DOKill()
    if noAnimation then
        quickStashNode.settingList.gameObject:SetActive(active)
    else
        quickStashNode.settingList.gameObject:SetActive(true)
        if active then
            quickStashNode.settingList:PlayInAnimation()
        else
            quickStashNode.settingList:PlayOutAnimation(function()
                quickStashNode.settingList.gameObject:SetActive(false)
            end)
        end
    end

    quickStashNode.settingBtn.gameObject:SetActive(not active)
    quickStashNode.closeBtn.gameObject:SetActive(active)

    if DeviceInfo.usingController then
        if active then
            quickStashNode.naviGroup:ManuallyFocus()
        else
            quickStashNode.naviGroup:ManuallyStopFocus()
        end
    end

    AudioAdapter.PostEvent(active and "au_ui_menu_sequence_open" or "au_ui_menu_sequence_close")
end

InventoryCtrl._SaveCurQuickStashSetting = HL.Method() << function(self)
    local value = 1
    for _, info in ipairs(self.m_quickStashSettingInfo) do
        value = value + (info.isOn and (1 << info.index) or 0)
    end
    GameInstance.player.inventory:SetItemBagBatchMoveFlag(value)
end







InventoryCtrl.m_inDestroyMode = HL.Field(HL.Boolean) << false

InventoryCtrl.m_showingDestroyItemCSIndex = HL.Field(HL.Number) << -1

InventoryCtrl.m_destroyInfo = HL.Field(HL.Table)

InventoryCtrl._InitDestroyNode = HL.Method() << function(self)
    self.view.destroyBtn.onClick:AddListener(function()
        self:_ToggleDestroyMode(true)
    end)
    self.view.destroyBtnDisable.onClick:AddListener(function()
        self:_PreventEnterDestroyMode()
    end)
    local destroyBtnEnable = not GameInstance.player.inventory:IsForbidDestroyItem(Utils.getCurrentScope())
    self.view.destroyBtn.gameObject:SetActive(destroyBtnEnable)
    self.view.destroyBtnDisable.gameObject:SetActive(not destroyBtnEnable)

    local node = self.view.destroyNode
    node.gameObject:SetActive(false)
    node.backBtn.onClick:AddListener(function()
        self:_ToggleDestroyMode(false)
    end)
    node.confirmBtn.onClick:AddListener(function()
        self:_ConfirmDestroy()
    end)
    self.m_destroyInfo = {}
    self:_ToggleDestroyMode(false, true)
end

InventoryCtrl._UpdateItemBlockMask = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)

    if csIndex >= self.view.itemBag.itemBagContent.m_itemBag.slots.Count then
        return
    end

    local button = cell.item.view.button
    if self.m_inMoveMode then
        local isTarget = csIndex == self.m_moveSlotIndex
        cell.view.blockMask.gameObject:SetActiveIfNecessary(isTarget)
        cell.item.view.destroySelectNode.gameObject:SetActive(false)
        local itemBundle = self.view.itemBag.itemBagContent.m_itemBag.slots[csIndex]
        button.clickHintTextId = (isTarget or string.isEmpty(itemBundle.id)) and "LUA_ITEM_ACTION_MOVE_IN_ITEM_BAG_PLACE" or "LUA_ITEM_ACTION_MOVE_IN_ITEM_BAG_SWAP"
        InputManagerInst:SetBindingText(button.hoverConfirmBindingId, Language[button.clickHintTextId])
        return
    end
    cell.view.dragItem.disableDrag = self.m_inDestroyMode
    local showMask = false
    if self.m_inDestroyMode then
        local inventory = GameInstance.player.inventory
        local itemBundle = self.view.itemBag.itemBagContent.m_itemBag.slots[csIndex]
        if not string.isEmpty(itemBundle.id) then
            showMask = not inventory:CanDestroyItem(Utils.getCurrentScope(), itemBundle.id)
        end
        button.clickHintTextId = self.m_destroyInfo[csIndex] and "virtual_mouse_hint_unselect" or "virtual_mouse_hint_select"
        button.longPressHintTextId = nil
    else
        button.clickHintTextId = DeviceInfo.usingController and "key_hint_item_open_action_menu" or "virtual_mouse_hint_item_tips"
        button.longPressHintTextId = "virtual_mouse_hint_drag"
        cell.item.view.destroySelectNode.gameObject:SetActive(false)
    end
    InputManagerInst:SetBindingText(button.hoverConfirmBindingId, Language[button.clickHintTextId])
    cell.view.blockMask.gameObject:SetActiveIfNecessary(showMask)
end

InventoryCtrl._PreventEnterDestroyMode = HL.Method() << function(self)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_BLACKBOX_FORBID_DROP_ITEM)
end

InventoryCtrl._ToggleDestroyMode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, noAnimation)
    if active then

        local forbid = GameInstance.player.inventory:IsForbidDestroyItem(Utils.getCurrentScope())
        if forbid then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_BLACKBOX_FORBID_DROP_ITEM)
            return
        end
    end
    self.view.closeButton.gameObject:SetActive(not active)

    if noAnimation then
        if active then
           self.view.itemBagNode:SampleToInAnimationEnd()
        else
           self.view.itemBagNode:SampleToOutAnimationEnd()
        end
        self.view.itemBagButtons.gameObject:SetActive(not active)
        self.view.destroyNode.gameObject:SetActive(active)
    else
        if active then
            self.view.itemBagNode:PlayInAnimation()
        else
            self.view.itemBagNode:PlayOutAnimation()
        end
        AudioAdapter.PostEvent(active and "au_ui_menu_destroy_open" or "au_ui_menu_destroy_close")
    end

    self.view.itemBag.itemBagContent.view.itemListSelectableNaviGroup.enablePartner = not active
    self.view.itemBag.itemBagContent.view.itemList:SetPaddingBottom(active and self.m_oriPaddingBottom + 100 or self.m_oriPaddingBottom)

    self.m_inDestroyMode = active
    if active then
        self.m_destroyInfo = {}
        self.m_showingDestroyItemCSIndex = -1
        self.view.destroyNode.confirmBtn.gameObject:SetActive(false)

        if DeviceInfo.usingController then
            self:_RefreshAllShowingCellsHoverTipsEnabledState()

            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = PANEL_ID,
                isGroup = true,
                id = self.view.itemBagNodeInputBindingGroupMonoTarget.groupId,
                hintPlaceholder = self.view.destroyNode.controllerHintPlaceholder,
                rectTransform = self.view.destroyNode.transform,
                noHighlight = true,
            })

            if not self.view.itemBag.itemBagContent.view.itemListSelectableNaviGroup.IsTopLayer then
                self:_NaviToPart(true, true)
            end
        end
    else
        local info = self.m_destroyInfo
        self.m_destroyInfo = {}
        for csIndex, _ in pairs(info) do
            self:_UpdateItemDestroySelect(csIndex)
        end

        if DeviceInfo.usingController then
            self:_RefreshAllShowingCellsHoverTipsEnabledState()

            Notify(MessageConst.HIDE_ITEM_TIPS)
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.itemBagNodeInputBindingGroupMonoTarget.groupId)
        end
    end


    self.view.itemBag.itemBagContent.view.itemList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.view.itemBag.itemBagContent.m_getCell(obj)
        self:_UpdateItemBlockMask(cell, csIndex)
        cell.item.canPlace = not active
        cell.item.canSplit = not active
        cell.item.canUse = not active
    end)

    self.m_naviGroupSwitcher:ToggleActive(not active and not self.m_weekRaidConvertRate)
    self:_UpdateMouseHint()
end

InventoryCtrl._OnClickItemInDestroyMode = HL.Method(HL.Number) << function(self, csIndex)
    local lastDestroyInfoEmpty = next(self.m_destroyInfo) == nil
    if self.m_destroyInfo[csIndex] then

        self.m_destroyInfo[csIndex] = nil
        self:_UpdateItemDestroySelect(csIndex)

        if self.m_showingDestroyItemCSIndex >= 0 then
            self.m_showingDestroyItemCSIndex = -1
        end
    else
        local inventory = GameInstance.player.inventory
        local itemBundle = self.view.itemBag.itemBagContent.m_itemBag.slots[csIndex]
        if inventory:CanDestroyItem(Utils.getCurrentScope(), itemBundle.id) then
            self.m_destroyInfo[csIndex] = itemBundle.count
            self:_UpdateItemDestroySelect(csIndex)

            if self.m_showingDestroyItemCSIndex < 0 then
            end
            self.m_showingDestroyItemCSIndex = csIndex
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_CANT_DROP_BECAUSE_TYPE)

            if self.m_showingDestroyItemCSIndex >= 0 then
                self.m_showingDestroyItemCSIndex = -1
            end
        end
    end

    local currDestroyInfoEmpty = next(self.m_destroyInfo) == nil
    self.view.destroyNode.confirmBtn.gameObject:SetActive(not currDestroyInfoEmpty)

    if lastDestroyInfoEmpty and not currDestroyInfoEmpty then
        self.view.destroyNode.animationWrapper:PlayWithTween("inventory_itembag_destroy_deco_in")
    end
    if not lastDestroyInfoEmpty and currDestroyInfoEmpty then
        self.view.destroyNode.animationWrapper:PlayWithTween("inventory_itembag_destroy_deco_out")
    end
end

InventoryCtrl._UpdateItemDestroySelect = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, csIndex, cell)
    if not cell then
        cell = self.view.itemBag.itemBagContent:GetCell(LuaIndex(csIndex))
        if not cell then
            return
        end
    end
    local selectCount = self.m_destroyInfo[csIndex]
    if selectCount then
        cell.item.view.destroySelectNode.gameObject:SetActive(true)
        cell.item.view.button.clickHintTextId = "key_hint_common_unselect"
    else
        cell.item.view.destroySelectNode.gameObject:SetActive(false)
        cell.item.view.button.clickHintTextId = "key_hint_common_select"
    end
    InputManagerInst:SetBindingText(cell.item.view.button.hoverConfirmBindingId, Language[cell.item.view.button.clickHintTextId])
end

InventoryCtrl._OnChangeItemDestroyCount = HL.Method(HL.Number, HL.Number) << function(self, csIndex, newCount)
    self.m_destroyInfo[csIndex] = newCount
    self:_UpdateItemDestroySelect(csIndex)
end

InventoryCtrl._CustomOnUpdateItemBagCell = HL.Method(HL.Forward("ItemSlot"), HL.Opt(HL.Userdata, HL.Number)) << function(self, cell, itemBundle, csIndex)
    cell.view.cachedCommonQuickDropBindingId = nil

    if DeviceInfo.usingController then
        if csIndex == 0 then
            if self.m_waitInitNaviTarget then
                self:_NaviToPart(true, false)
                self.m_waitInitNaviTarget = false
            end
        end

        cell.item:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.itemBag.transform,
        })

        self:_RefreshNaviTargetItemState(csIndex, cell, cell.item.view.button.isNaviTarget, itemBundle)
    end
    cell.item.view.button.onIsNaviTargetChanged = function(active)
        self:_RefreshNaviTargetItemState(csIndex, cell, active, itemBundle)
        if active and cell.view.cachedCommonQuickDropBindingId then
            InputManagerInst:ToggleBinding(cell.view.cachedCommonQuickDropBindingId, cell:IsQuickDropTargetValid())
        end
    end
    self:_TryDisableHoverBindingOnEmptyItem(cell, itemBundle)

    cell.item.canDestroy = true
    cell.item.canSetQuickBar = true
    if cell.item.actionMenuArgs then
        cell.item.actionMenuArgs.extraButtons = { self.view.sortButton, self.view.destroyBtn }
        if itemBundle then
            cell.item.actionMenuArgs.startMoveAct = function()
                self:_ToggleMoveMode(true)
            end
        end
    end

    cell.item.canPlace = not self.m_inDestroyMode
    cell.item.canSplit = not self.m_inDestroyMode
    cell.item.canUse = not self.m_inDestroyMode

    if not itemBundle then
        return
    end

    if self.m_weekRaidConvertRate then

        local isRaidItem, raidItemData = Tables.weekRaidItemTable:TryGetValue(itemBundle.id)
        if isRaidItem and raidItemData.convertGoldNum > 0 then
            cell.view.weekRaidValueTxt.text = math.floor(raidItemData.convertGoldNum * self.m_weekRaidConvertRate)
            cell.view.weekRaidNode.gameObject:SetActive(true)
            cell.item.view.count.gameObject:SetActive(false)
        else
            cell.view.weekRaidNode.gameObject:SetActive(false)
        end
    else
        cell.view.weekRaidNode.gameObject:SetActive(false)
    end

    self:_UpdateItemBlockMask(cell, csIndex)
    if self.m_inMoveMode and string.isEmpty(itemBundle.id) then


        cell.item.view.button.enabled = true
        cell.item.view.button.onClick:RemoveAllListeners()
        cell.item.view.button.onClick:AddListener(function()
            self:_OnClickItem("", cell, csIndex)
        end)
    end
    if not self.m_inDestroyMode then
        if itemBundle.count and itemBundle.count > 0 then
            cell.view.cachedCommonQuickDropBindingId = cell.item:AddHoverBinding("common_quick_drop", function()
                cell:QuickDrop()
            end)
            InputManagerInst:ToggleBinding(cell.view.cachedCommonQuickDropBindingId, cell:IsQuickDropTargetValid())
            cell.item:AddHoverBinding("inv_item_bag_start_move_item", function()
                self:_ToggleMoveMode(true)
            end)
        end
        return
    end
    self:_UpdateItemDestroySelect(csIndex, cell)
end

InventoryCtrl._TryDisableHoverBindingOnEmptyItem = HL.Method(HL.Forward("ItemSlot"), HL.Opt(HL.Userdata)) << function(self, cell, itemBundle)
    if self.m_inMoveMode then
        return
    end
    if not itemBundle or not itemBundle.count or itemBundle.count == 0 then
        InputManagerInst:ToggleBinding(cell.item.view.button.hoverConfirmBindingId, false)
    end
end

InventoryCtrl._TryDisableItemHoverBindingOnDestroyMode = HL.Method(HL.Forward("ItemSlot")) << function(self, cell)
    InputManagerInst:ToggleGroup(cell.item.view.button.hoverBindingGroupId, not self.m_inDestroyMode and not self.m_inMoveMode)
end

InventoryCtrl._ShowTipsOnNaviTargetInDestroyMode = HL.Method(HL.Number, HL.Forward("ItemSlot"), HL.Opt(HL.Any)) << function(self, csIndex, cell, itemBundle)
    if not self.m_inDestroyMode then
        return
    end

    local itemId = itemBundle ~= nil and itemBundle.id or ""
    local showTips = not string.isEmpty(itemId)
    if showTips then
        if not cell.item.showingTips then
            local posInfo = {
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
                tipsPosTransform = self.view.itemBag.transform,
                isSideTips = true,
            }
            cell.item.canPlace = false
            cell.item.canSplit = false
            cell.item.canUse = false
            cell.item.canClear = false
            cell.item:ShowTips(posInfo, function()
                if self.m_showingDestroyItemCSIndex == csIndex then
                    self.m_showingDestroyItemCSIndex = -1
                end
            end)
        end
    else
        Notify(MessageConst.HIDE_ITEM_TIPS)
    end
end

InventoryCtrl._ConfirmDestroy = HL.Method() << function(self)
    Notify(MessageConst.HIDE_ITEM_TIPS)
    local inventory = GameInstance.player.inventory
    local items = {}
    for csIndex, _ in pairs(self.m_destroyInfo) do
        table.insert(items, csIndex)
    end
    inventory:AbandonItemInItemBag(Utils.getCurrentScope(), items)
end

InventoryCtrl._OnAbandonItem = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if not self.m_abandonValid then
        return
    end
    local inventory = GameInstance.player.inventory
    local itemId = dragHelper:GetId()
    if not inventory:CanDestroyItem(Utils.getCurrentScope(), itemId) then
        return
    end
    inventory:AbandonItemInItemBag(Utils.getCurrentScope(), {dragHelper.info.csIndex})
end

InventoryCtrl.OnItemBagAbandonInBagSucc = HL.Method() << function(self)
    if self.m_inDestroyMode then
        self:_ToggleDestroyMode(false)
    end
    Notify(MessageConst.SHOW_TOAST, Language.LUA_ABANDON_ITEM_IN_BAG_SUCC)
    AudioAdapter.PostEvent("Au_UI_Event_Inventory_Destory_Success")
    self:_RefreshWeekRaidBottomNode()
end

InventoryCtrl.OnToggleAbandonDropValid = HL.Method(HL.Any) << function(self, args)
    self.m_abandonValid = unpack(args)
end






InventoryCtrl.m_targetItemId = HL.Field(HL.String) << ''

InventoryCtrl._GotoItem = HL.Method(HL.String) << function(self, itemId)

    local index = self.view.itemBag.itemBagContent.m_itemBag:GetFirstSlotIndex(itemId)
    if index >= 0 then
        local content = self.view.itemBag.itemBagContent
        local scrollList = content.view.itemList
        scrollList:SkipGraduallyShow()
        scrollList:ScrollToIndex(index, true)
        local cell = content.m_getCell(LuaIndex(index))
        if cell then
            self:_OnClickItem(itemId, cell, index)
        end
        return
    end


    local content = self.view.depot.depotContent
    if not content or not content.gameObject.activeInHierarchy then

        return
    end
    local scrollList = content.view.itemList
    local luaIndex = content:GetItemIndex(itemId)
    if luaIndex then
        scrollList:SkipGraduallyShow()
        scrollList:ScrollToIndex(CSIndex(luaIndex), true)
        local cell = content.m_getCell(luaIndex)
        if cell then
            content:_OnClickItem(luaIndex)
        end
    end
end






InventoryCtrl.m_missionItemIds = HL.Field(HL.Table)

InventoryCtrl._RefreshMissionItemIds = HL.Method() << function(self)
    if Utils.isInBlackbox() then
        self.m_missionItemIds = {}
        return
    end
    local missionItemIds = {}
    local idSet = GameInstance.player.mission:GetTrackingMissionProcessingQuestNeedItems()
    if idSet ~= nil then
        for _, id in pairs(idSet) do
            missionItemIds[id] = true
        end
    end
    self.m_missionItemIds = missionItemIds
end






InventoryCtrl._InitControllerSideMenuBtn = HL.Method() << function(self)
    local extraBtnInfos = {}
    table.insert(extraBtnInfos, {
        name = "InventoryItemBagAbandon",
        action = function()
            self:_ToggleDestroyMode(true)
        end,
        textId = "LUA_CONTROLLER_INV_ITEM_BAG_ABANDON",
        priority = 3.1,
    })
    if Utils.isInSafeZone() then
        table.insert(extraBtnInfos, {
            name = "InventoryDepotDestroy",
            action = function()
                self.view.depot:ToggleDestroyMode(true, false)
            end,
            textId = "LUA_CONTROLLER_INV_DEPOT_DESTROY",
            priority = 3.2,
        })
    end

    self.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
        extraBtnInfos = extraBtnInfos
    })
end

InventoryCtrl._NaviToPart = HL.Method(HL.Boolean, HL.Boolean) << function(self, toItemBag, toTop)
    if toItemBag then
        if toTop then
            self.view.itemBag.itemBagContent.view.itemList:SetTop()
        end
        local cell = self.view.itemBag.itemBagContent:GetCell(1)
        if cell then
            cell:SetAsNaviTarget()
        end
    else
        if toTop then
            self.view.depot.depotContent.view.itemList:SetTop()
        end
        self.view.depot:SetAsNaviTarget()
    end
end

InventoryCtrl._RefreshNaviTargetItemState = HL.Method(HL.Number, HL.Forward("ItemSlot"), HL.Boolean, HL.Opt(HL.Any)) << function(self, csIndex, cell, active, itemBundle)
    if active then
        self:_TryDisableHoverBindingOnEmptyItem(cell, itemBundle)
        self:_TryDisableItemHoverBindingOnDestroyMode(cell)
        self:_ShowTipsOnNaviTargetInDestroyMode(csIndex, cell, itemBundle)
        self:_RefreshCellHoverTipsEnabledState(cell)
    end

    if self.m_inMoveMode then
        local itemBag = self.view.itemBag.itemBagContent.m_itemBag
        local moveItemId = itemBag.slots[self.m_moveSlotIndex].id
        local itemId
        if active then
            self.view.moveHintItem.view.followerObject.target = cell.gameObject.transform
            itemId = moveItemId
        else
            itemId = itemBag.slots[csIndex].id
        end
        cell:UpdateSlotColorBGByItemId(itemBag, csIndex, itemId, Utils.isPortableDevice(moveItemId))
    end
end

InventoryCtrl._RefreshAllShowingCellsHoverTipsEnabledState = HL.Method() << function(self)
    self.view.itemBag.itemBagContent.view.itemList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.view.itemBag.itemBagContent.m_getCell(obj)
        self:_RefreshCellHoverTipsEnabledState(cell)
    end)
end

InventoryCtrl._RefreshCellHoverTipsEnabledState = HL.Method(HL.Forward("ItemSlot")) << function(self, cell)
    cell.view.item:SetEnableHoverTips(not self.m_inDestroyMode and not self.m_inMoveMode)
end






InventoryCtrl.m_weekRaidConvertRate = HL.Field(HL.Any)

InventoryCtrl._RefreshWeekRaidBottomNode = HL.Method() << function(self)
    if not self.m_weekRaidConvertRate then
        return
    end
    local itemBag = self.view.itemBag.itemBagContent.m_itemBag
    local value = 0
    for _, itemBundle in pairs(itemBag.slots) do
        if itemBundle.count > 0 then
            local isRaidItem, raidItemData = Tables.weekRaidItemTable:TryGetValue(itemBundle.id)
            if isRaidItem and raidItemData.convertGoldNum > 0 then
                value = value + raidItemData.convertGoldNum
            end
        end
    end
    self.view.weekRaidBottomNode.valueTxt.text = math.floor(value * self.m_weekRaidConvertRate)
end


InventoryCtrl.m_timeScaleHandler = HL.Field(HL.Number) << 0

InventoryCtrl._FreezeWorld = HL.Method() << function(self)
    self:_ResumeWorld()
    self.m_timeScaleHandler = Utils.FreezeWorldByUI()
    GameWorld.subGameManager:TryPauseGameTime(GEnums.GameTimeFreezeReason.UI)
end

InventoryCtrl._ResumeWorld = HL.Method() << function(self)
    if self.m_timeScaleHandler > 0 then
        Utils.ResumeWorldByUI(self.m_timeScaleHandler)
        self.m_timeScaleHandler = 0
        GameWorld.subGameManager:TryResumeGameTime(GEnums.GameTimeFreezeReason.UI)
    end
end






InventoryCtrl.m_inMoveMode = HL.Field(HL.Boolean) << false

InventoryCtrl.m_moveSlotIndex = HL.Field(HL.Number) << -1

InventoryCtrl.m_moveModeBindingGroupId = HL.Field(HL.Number) << -1

InventoryCtrl.m_moveItemQuickMoveBindingId = HL.Field(HL.Number) << -1

InventoryCtrl._ToggleMoveMode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, isCancel)
    if self.m_inMoveMode == active then
        return
    end

    local itemBag = self.view.itemBag.itemBagContent.m_itemBag

    local content = self.view.itemBag.itemBagContent
    local itemList = content.view.itemList
    content.view.itemListSelectableNaviGroup.enablePartner = not active

    self.m_inMoveMode = active
    local nextIndex = -1
    local showColoredSlotDropHint = active
    if active then
        self.m_moveSlotIndex = itemList:GetNaviManagerTargetIndex()
        if self.m_moveSlotIndex == -1 then
            self.m_moveSlotIndex = 0
            logger.error("InventoryCtrl._ToggleMoveMode self.m_moveSlotIndex is -1")
        end
        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = PANEL_ID,
            isGroup = true,
            id = content.view.inputBindingGroupMonoTarget.groupId,
            rectTransform = self.view.itemBag.itemBagContent.transform,

            noHighlight = true,
            hintPlaceholder = self.view.controllerHintPlaceholder,
        })
        local itemBundle = itemBag.slots[self.m_moveSlotIndex]
        InputManagerInst:ToggleBinding(self.m_moveItemQuickMoveBindingId, Utils.isPortableDevice(itemBundle.id))
        showColoredSlotDropHint = Utils.isPortableDevice(itemBundle.id)
        self.view.moveHintItem:InitItem(itemBundle)

        nextIndex = self.m_moveSlotIndex + 1
        if nextIndex == itemBag.slots.Count then
            nextIndex = self.m_moveSlotIndex - 1
        end
    else
        if isCancel then
            nextIndex = self.m_moveSlotIndex
        end
        self.m_moveSlotIndex = -1
        Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, content.view.inputBindingGroupMonoTarget.groupId)
        self.view.moveHintItem.view.followerObject.target = nil
    end
    self.view.moveHintItem.gameObject:SetActive(active)
    self.view.moveItemFrame.gameObject:SetActive(active)


    itemList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.view.itemBag.itemBagContent:GetCell(obj)
        self:_UpdateItemBlockMask(cell, csIndex)
        self:_RefreshCellHoverTipsEnabledState(cell)
        if csIndex < itemBag.slots.Count then
            local itemBundle = itemBag.slots[csIndex]
            if string.isEmpty(itemBundle.id) then


                cell.item.view.button.enabled = true
                cell.item.view.button.onClick:RemoveAllListeners()
                cell.item.view.button.onClick:AddListener(function()
                    self:_OnClickItem("", cell, csIndex)
                end)
            end
            cell:UpdateSlotColorBGByItemId(itemBag, csIndex, itemBundle.id, showColoredSlotDropHint)
        end
    end)

    self.m_naviGroupSwitcher:ToggleActive(not active and not self.m_weekRaidConvertRate)
    InputManagerInst:ToggleGroup(self.m_moveModeBindingGroupId, active)

    if nextIndex >= 0 then
        local cell = self.view.itemBag.itemBagContent:GetCell(LuaIndex(nextIndex))
        if not cell then
            self.view.itemBag.itemBagContent.view.itemList:ScrollToIndex(nextIndex, true)
            cell = self.view.itemBag.itemBagContent:GetCell(LuaIndex(nextIndex))
        end
        if cell then
            cell:SetAsNaviTarget()
        end
    elseif not active then


        local naviIndex = itemList:GetNaviManagerTargetIndex()
        if naviIndex >= 0 then
            local cell = self.view.itemBag.itemBagContent:GetCell(LuaIndex(naviIndex))
            if cell then
                self:_TryDisableItemHoverBindingOnDestroyMode(cell)
            end
        end
    end
end

InventoryCtrl._MoveItemQuickMove = HL.Method() << function(self)
    self:_NaviToPart(true, true)
end





HL.Commit(InventoryCtrl)
