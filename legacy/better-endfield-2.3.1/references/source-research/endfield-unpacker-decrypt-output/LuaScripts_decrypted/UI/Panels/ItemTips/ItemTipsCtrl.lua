local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ItemTips
ItemTipsCtrl = HL.Class('ItemTipsCtrl', uiCtrl.UICtrl)

local LIQUID_EMPTY_CAPACITY_TEXT_ID = "LUA_ITEM_TIPS_LIQUID_INFO_EMPTY_CAPACITY"
local LIQUID_FULL_CAPACITY_TEXT_ID = "LUA_ITEM_TIPS_LIQUID_INFO_FULL_CAPACITY"
local LIQUID_EMPTY_NAME_TEXT_ID = "LUA_ITEM_TIPS_LIQUID_INFO_EMPTY_NAME"

local OBTAIN_WAYS_NORMAL_TITLE_TEXT_ID = "ui_common_obtain_method"
local PRODUCT_NORMAL_TITLE_TEXT_ID = "ui_common_tips_main_product"
local PRODUCT_LIQUID_TITLE_TEXT_ID = "ui_common_tips_liquid_main_product"

local UIAutoCloseArea = CS.Beyond.UI.UIAutoCloseArea





ItemTipsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_ITEM_TIPS] = 'HideItemTips',
    [MessageConst.TOGGLE_ITEM_TIPS_AUTO_CLOSE] = 'ToggleItemTipsAutoClose',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = 'OnItemCountChanged',
    [MessageConst.ON_SYSTEM_DISPLAY_SIZE_CHANGED] = 'OnSystemSizeChange',
}

ItemTipsCtrl.m_itemId = HL.Field(HL.String) << ""

ItemTipsCtrl.m_itemCount = HL.Field(HL.Number) << -1

ItemTipsCtrl.m_instId = HL.Field(HL.Number) << 0

ItemTipsCtrl.m_data = HL.Field(HL.Userdata)

ItemTipsCtrl.m_autoCloseTime = HL.Field(HL.Number) << -1

ItemTipsCtrl.m_stopCheckClose = HL.Field(HL.Boolean) << false

ItemTipsCtrl.m_args = HL.Field(HL.Table)

ItemTipsCtrl.m_slotIndex = HL.Field(HL.Number) << -1

ItemTipsCtrl.m_slotCount = HL.Field(HL.Number) << -1

ItemTipsCtrl.m_productCellCache = HL.Field(HL.Forward("UIListCache"))

ItemTipsCtrl.m_tagCellCache = HL.Field(HL.Forward("UIListCache"))

ItemTipsCtrl.m_isShowBlueprintProduct = HL.Field(HL.Boolean) << true

ItemTipsCtrl.m_closeBinding = HL.Field(HL.Number) << -1

ItemTipsCtrl.m_isTipsClosing = HL.Field(HL.Boolean) << false

ItemTipsCtrl.m_controllerSyncPosCor = HL.Field(HL.Thread)

ItemTipsCtrl.m_lockToggleBindingId = HL.Field(HL.Number) << -1


ItemTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.maskBtn.onClick:AddListener(function()
        self:_CloseTips(false)
    end)

    self.view.placeButton.onClick:AddListener(function()
        self:_StartPlace()
    end)
    self.view.splitButton.onClick:AddListener(function()
        self:_ShowSplit()
    end)
    self.view.clearButton.onClick:AddListener(function()
        self:_ShowClearBottle()
    end)
    self.view.jumpButton.onClick:AddListener(function()
        self:_TryJump()
    end)
    self.view.useButton.onClick:AddListener(function()
        self:_UseItem()
    end)
    self.view.wikiBtn.onClick:AddListener(function()
        self:_ShowWiki()
    end)
    self.view.cantPenetrateBtn.onClick:AddListener(function()
        self:_CloseTips(false)
    end)

    self.view.detailScroll.onValueChanged:AddListener(function()
        self:_OnDetailScroll()
    end)

    self.m_closeBinding = self:BindInputPlayerAction("item_tips_close", function()
        self:_CloseTips(false)
    end)

    UIUtils.bindHyperlinkPopup(self, "ItemTips", self.view.inputGroup.groupId, "hyperlink_show_popup")

    self.m_extraSafeAreas = {}

    self.m_detailContentTopPadding = self.view.detailContentVerticalLayoutGroup.padding.top

    self.m_productCellCache = UIUtils.genCellCache(self.view.productNode.itemCell)
    self.m_tagCellCache = UIUtils.genCellCache(self.view.tagInfoNode.tagCell)

    self.view.itemObtainWays.view.selectableNaviGroup.canFocusFun = function()
        return not self:IsPlayingAnimationOut()
    end

    self.view.itemObtainWays.view.selectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self:_RefreshControllerKeyHint()
    end)
    self.view.itemObtainWays.view.selectableNaviGroup.onSetLayerSelectedTarget:RemoveAllListeners()
    self.view.itemObtainWays.view.selectableNaviGroup.onSetLayerSelectedTarget:AddListener(function(target)
        if NotNull(target) then
            self.view.detailScroll:AutoScrollToRectTransform(target.transform, true)
        end
    end)

    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        self:_InitTipsDebugBtn()
    else
        self.view.debugIDButton.gameObject:SetActive(false)
        self.view.countNode.debugAddButton.gameObject:SetActive(false)
    end

    self.m_lockToggleBindingId = self:BindInputPlayerAction("item_lock_toggle", function()
        self.view.lockToggle.view.toggle.isOn = not self.view.lockToggle.view.toggle.isOn
    end)
    self.view.itemFlagNavGroup.onIsFocusedChange:AddListener(function(isTopLayer)
        if isTopLayer then
            self.view.detailScroll.verticalNormalizedPosition = 1
        end
        self.view.itemFlagControllerFocusHintNode.gameObject:SetActive(not isTopLayer)
    end)



    self.view.higherLvObtainWay.gameObject:SetActive(false)
    local higherLvObtainWayObtainCellGO = CSUtils.CreateObject(self.view.itemObtainWays.view.obtainCell.gameObject, self.view.higherLvObtainWay)
    self.view.higherLvObtainWayObtainCell = Utils.wrapLuaNode(higherLvObtainWayObtainCellGO)

    self.view.itemDescNode.gameObject:SetActive(true)
end









































ItemTipsCtrl.ShowItemTips = HL.StaticMethod(HL.Table) << function(args)
    local isShowing = UIManager:IsShow(PANEL_ID)

    local self = UIManager:AutoOpen(PANEL_ID)
    if isShowing then
        self:_ClearArgs()
        if not self:IsPlayingAnimationOut() then
            self.m_cachedArgs = args
            self:_CloseTips()
        else

            if self.m_cachedArgs then
                if self.m_cachedArgs.onClose then
                    self.m_cachedArgs.onClose()
                end
            end
            self.m_cachedArgs = args
        end
    else
        self:_ShowTips(args)
    end
end

ItemTipsCtrl.OnSystemSizeChange = HL.Method() << function(self)
    if self.m_args then
        self:_ShowTips(self.m_args)
    end
end

ItemTipsCtrl.m_cachedArgs = HL.Field(HL.Table)

ItemTipsCtrl.HideItemTips = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    local cachedOnClose
    if self.m_cachedArgs then
        cachedOnClose = self.m_cachedArgs.onClose
        self.m_cachedArgs = nil
    end
    local skipAnim = args and args.skipAnim
    self:_CloseTips(skipAnim)
    if cachedOnClose then
        cachedOnClose()
    end
end



ItemTipsCtrl.m_autoClose = HL.Field(HL.Boolean) << true

ItemTipsCtrl.ToggleItemTipsAutoClose = HL.Method(HL.Any) << function(self, autoClose)
    logger.info("ToggleItemTipsAutoClose", inspect(autoClose))
    if type(autoClose) == "table" then
        self.m_autoClose = unpack(autoClose)
    else
        self.m_autoClose = autoClose
    end
end

ItemTipsCtrl.m_firstShow = HL.Field(HL.Boolean) << true
ItemTipsCtrl._ShowTips = HL.Method(HL.Table) << function(self, args)
    self:_ClearArgs()

    self.m_args = args
    self.m_cachedArgs = nil
    if args.safeArea then
        self:_AddItemTipsSafeArea(args.safeArea)
    end

    if self.m_args.includeInAutoCloseLayer ~= false then
        UIAutoCloseArea.AddToAutoCloseLayer(self.view.gameObject)
    end

    self.m_autoCloseTime = -1

    local itemId = args.itemId
    local instId = args.instId or 0
    self.m_itemCount = args.itemCount or -1
    if args.slotIndex then
        self.m_slotIndex = args.slotIndex
        local itemBundle = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope()).slots[args.slotIndex]
        itemId = itemBundle.id
        instId = itemBundle.instId
        self.m_slotCount = itemBundle.count
    else
        self.m_slotIndex = -1
        self.m_slotCount = -1
    end

    self:_RefreshContent(itemId, instId)

    if not string.isEmpty(args.prefixDesc) then
        self.view.prefixDescInfo.gameObject:SetActiveIfNecessary(true)
        self.view.prefixDescTxt.gameObject:SetActiveIfNecessary(true)
        self.view.prefixDescTxt.text = args.prefixDesc
    else
        self.view.prefixDescInfo.gameObject:SetActiveIfNecessary(false)
        self.view.prefixDescTxt.gameObject:SetActiveIfNecessary(false)
    end

    if args.autoClose ~= nil then
        self:ToggleItemTipsAutoClose(args.autoClose)
    else
        self:ToggleItemTipsAutoClose(true)
    end
    UIManager:SetTopOrder(PANEL_ID)

    local isFullScreen = self:_IsFullScreen()
    self:ChangeCurPanelBlockSetting(isFullScreen)

    InputManagerInst:ToggleBinding(self.m_closeBinding, not DeviceInfo.usingController or isFullScreen)

    local State = CS.Beyond.UI.CustomUIStyle.OverrideValidState
    self.view.maskBtnCustomUIStyle.overrideValidState = self.m_args.isSideTips and State.ForceNotValid or State.None

    self.view.cantPenetrateBtn.gameObject:SetActive(args.notPenetrate == true)

    self:PlayAnimationIn()

    if args.moveVirtualMouse then
        InputManagerInst:MoveVirtualMouseTo(self.view.content.transform, self.uiCamera)
    end

    self:_RefreshControllerKeyHint()
end

ItemTipsCtrl._RefreshControllerKeyHint = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        self.view.controllerHintBarCell.gameObject:SetActive(false)
        return
    end
    if not self.m_args then
        return
    end
    self.view.controllerHintBarCell.gameObject:SetActive(true)
    local groupIds
    if self.view.itemObtainWays.view.selectableNaviGroup.IsTopLayer then
        groupIds = { self.view.itemObtainWays.view.inputBindingGroupMonoTarget.groupId }
    else
        groupIds = { self.view.inputGroup.groupId }
        if self.m_args.keyHintGroupIds then
            groupIds = lume.concat(groupIds, self.m_args.keyHintGroupIds)
        end
    end
    self.view.controllerHintBarCell:InitControllerHintBarCell({ groupIds = groupIds, }, true)
end


ItemTipsCtrl._IsFullScreen = HL.Method().Return(HL.Boolean) << function(self)
    return InputManagerInst.usingController and not self.m_args.isSideTips
end

ItemTipsCtrl._ClearArgs = HL.Method() << function(self)
    if self.m_args then
        if self.m_args.onClose then
            self.m_args.onClose()
            self.m_args.onClose = nil
        end
        if self.m_args.safeArea then
            self:_RemoveItemTipsSafeArea(self.m_args.safeArea)
        end
    end
    self.m_args = nil
    self.m_itemId = ""
    self.m_instId = 0
    self.m_data = nil
    self.m_slotIndex = -1
    self.m_slotCount = -1
end

ItemTipsCtrl.OnShow = HL.Override() << function(self)
    self:_RegisterCallback()
end

ItemTipsCtrl.OnClose = HL.Override() << function(self)
    self:_ClearArgs()
    self:_ClearRegister()
end

ItemTipsCtrl.OnHide = HL.Override() << function(self)
    self:_ClearArgs()
    self:_ClearRegister()
end

ItemTipsCtrl._Update = HL.Method() << function(self)
    self:_CheckShouldAutoClose()
end




ItemTipsCtrl.m_craftCellCache = HL.Field(HL.Forward("UIListCache"))
ItemTipsCtrl._RefreshContent = HL.Method(HL.String, HL.Number) << function(self, itemId, instId)
    local data = Tables.itemTable:GetValue(itemId)
    local itemType = data.type
    local maxBackpackStackCount = data.maxBackpackStackCount

    self.m_itemId = itemId
    self.m_instId = instId
    self.m_data = data

    UIUtils.displayItemBasicInfos(self.view, self.loader, itemId, instId)

    self.view.portableDeviceTagNode:InitPortableDeviceTagNode(itemId)

    self.view.icon:InitItemIcon(itemId, true, instId)
    if self.view.icon.view.liquidIcon then
        self.view.icon.view.liquidIcon.gameObject:SetActive(false)
    end
    self.view.iconImageBlur:OnChangeSprite()
    self.view.earlyAccessNode.gameObject:SetActiveIfNecessary(not Utils.isInBlackbox() and FactoryUtils.isSkipUnlockedBuildingByItemId(itemId))

    self.view.itemObtainWays.gameObject:SetActive(true)
    self.view.itemObtainWays:InitItemObtainWays(itemId, instId, nil, nil,
        self.m_args.onBeforeJump, self.m_args.jumpExtraArgs)


    local isPD, pdData = Tables.itemPortableDeviceTable:TryGetValue(itemId)
    local nextLvObtainInfo
    if isPD and not string.isEmpty(pdData.nextLvItemId) then
        local craftInfoList = FactoryUtils.getItemCrafts(pdData.nextLvItemId)
        local cInfo = craftInfoList[1]
        if cInfo then
            nextLvObtainInfo = {
                name = Language.LUA_OBTAIN_WAYS_MANUAL_CRAFT_NAME,
                crafts = { cInfo },
                iconFolder = UIConst.UI_SPRITE_ITEM_TIPS,
                iconId = UIConst.UI_MANUALCRAFT_ICON_ID,
                phaseId = PhaseId.ManualCraft,
                phaseArgs = { jumpId = cInfo.craftId },
            }
        end
    end
    if nextLvObtainInfo then
        self.view.higherLvObtainWay.transform:SetAsLastSibling()
        self.view.higherLvObtainWay.gameObject:SetActive(true)
        self.view.itemObtainWays:RefreshObtainCell(self.view.higherLvObtainWayObtainCell, nextLvObtainInfo, -1)
    else
        self.view.higherLvObtainWay.gameObject:SetActive(false)
    end

    self.view.stateCtrl:SetState("default")
    self.view.itemDescNode:InitItemDescNode(itemId)
    self:_RefreshWeaponNode(itemId, instId, itemType)
    self:_RefreshEquipNode(itemId, instId, itemType)
    self:_RefreshGemNode(itemId, instId, itemType)
    self:_RefreshCountNode(itemId, itemType, data.showAllDepotCount)
    self:_RefreshTagInfoNode(itemId, itemType)
    self:_RefreshProductNode(itemId, false)
    self:_RefreshEmptyNode(itemId, itemType)
    self:_RefreshPickUpNode(itemId)
    self:_RefreshLiquidDischargeNode(itemId)
    self:_RefreshGasDischargeNode(itemId)
    self:_RefreshLiquidInfoNode(itemId)
    self:_RefreshGasInfoNode(itemId)
    self:_RefreshShareNode(itemId)
    self.view.tipsLimitedTimeNode:InitTipsLimitedTimeNode(itemId, instId or 0, nil)
    UIUtils.displayGiftItemTags(self.view.collectionTagNode, itemId)
    self.view.giftFeatureTagsNode:InitGiftFeatureTagsNode(itemId)

    self.view.wikiBtn.gameObject:SetActive(self:_GetNeedShowWikiBtn())

    local isEquip = itemType == GEnums.ItemType.Equip and instId and instId > 0
    local isExp = itemType == GEnums.ItemType.CardExp
    local isBusinessCardTopic = itemType == GEnums.ItemType.BusinessCardTopic
    local jumpText = ""
    if isExp then
        jumpText = Language.LUA_EXP_CARD_JUMP
    elseif isBusinessCardTopic then
        jumpText = Language.LUA_BUSINESS_CARD_TOPIC_JUMP
    elseif isEquip then
        jumpText = Language.LUA_EQUIP_JUMP
    end
    if not string.isEmpty(jumpText) then
        self.view.jumpButton.text = jumpText
    end

    local hasBottomButton
    local isUseItem, _ = Tables.useItemTable:TryGetValue(itemId)
    local count = Utils.getItemCount(itemId)
    local canUse = isUseItem and self.m_args.canUse and Utils.isSystemUnlocked(GEnums.UnlockSystemType.ItemUse) and count > 0
    if canUse and LuaSystemManager.factory.inTopView then
        if UIConst.DISABLED_USE_ITEM_ID_IN_TOP_VIEW[itemId] then
            canUse = false
        end
    end
    self.view.useButton.gameObject:SetActiveIfNecessary(canUse)
    local isBuilding, buildingId = FactoryUtils.isBuilding(itemId)
    local canPlace = self.m_args.canPlace and isBuilding and FactoryUtils.canPlaceBuildingOnCurRegion(buildingId)
    local canSplit = self.m_args.canSplit and maxBackpackStackCount > 1 and self.m_slotCount > 1
    local canJump = self:_CheckIfCanJump(itemId, itemType, instId)
    local isFullLiquidBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isFullGasBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
    local canClear = self.m_args.canClear and (isFullLiquidBottle or isFullGasBottle) and self.m_itemCount > 0
    self.view.placeButton.gameObject:SetActive(canPlace)
    self.view.splitButton.gameObject:SetActive(canSplit)
    self.view.jumpButton.gameObject:SetActive(canJump)
    self.view.bottomController:SetState(canJump and "JumpState" or "NormalState")
    self.view.dropButton.gameObject:SetActive(false)
    self.view.clearButton.gameObject:SetActive(canClear)
    if canClear then
        self.view.clearText.text = isFullLiquidBottle and Language.LUA_ITEM_TIPS_CLEAR_BUTTON_LIQUID or Language.LUA_ITEM_TIPS_CLEAR_BUTTON_GAS
    end
    hasBottomButton = canPlace or canSplit or canUse or canJump or canClear
    self.view.bottomButtons.gameObject:SetActive(hasBottomButton)

    self.view.detailScroll.normalizedPosition = Vector2(0, 1)
    self:_OnDetailScroll()
    local extraHeight = self.view.basicInfoNode.rect.size.y
    if hasBottomButton then
        extraHeight = extraHeight + self.view.bottomButtons.transform.rect.size.y
    end

    self.view.detailContentLayoutElement.enabled = false
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.detailContent)
    local detailContentHeight = self.view.detailContent.rect.size.y
    local detailShowHeight = lume.clamp(detailContentHeight, self.view.config.MIN_CONTENT_HEIGHT - extraHeight, self.view.config.MAX_CONTENT_HEIGHT - extraHeight)
    self.view.detailScrollLayoutElement.preferredHeight = detailShowHeight
    local canScroll = detailContentHeight > detailShowHeight
    if canScroll then

        self.view.detailContentLayoutElement.minHeight = detailShowHeight + self.m_detailContentTopPadding
        self.view.detailContentLayoutElement.enabled = true
        LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.detailContent)
    end
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.content)

    local padding = self.m_args.padding
    if not padding then
        padding = {
            bottom = 50 + (DeviceInfo.usingController and 50 or 0),
        }
    end

    local notchSize = CS.Beyond.DeviceInfoManager.NotchPaddingInCanvas(self.view.transform).x
    padding.left = (padding.left or 0) + notchSize
    padding.right = (padding.right or 0) + notchSize

    UIUtils.updateTipsPosition(self.view.content, self.m_args.transform, self.view.rectTransform, self.uiCamera, self.m_args.posType, padding)
    if self.m_args.isSideTips and DeviceInfo.usingController then
        self:_ClearCoroutine(self.m_controllerSyncPosCor)
        self.m_controllerSyncPosCor = self:_StartCoroutine(function()
            while true do
                coroutine.step()
                if self.m_args ~= nil then
                    UIUtils.updateTipsPosition(self.view.content, self.m_args.transform, self.view.rectTransform, self.uiCamera, self.m_args.posType, padding)
                end
            end
        end)
    end

    local isLockToggleVisible = self.view.lockToggle:InitLockToggle(itemId, instId)
    local isTrashToggleVisible = self.view.trashToggle:InitTrashToggle(itemId, instId)
    local isItemFlagNaviGroupVisible = isLockToggleVisible and isTrashToggleVisible
    InputManagerInst:ToggleBinding(self.m_lockToggleBindingId, isLockToggleVisible and not isItemFlagNaviGroupVisible)
    self.view.itemFlagNavGroup.enabled = isItemFlagNaviGroupVisible
    self.view.itemFlagControllerFocusHintNode.gameObject:SetActive(isItemFlagNaviGroupVisible)

    if BEYOND_DEBUG then
        self:_RefreshTipsDebugBtn()
    end
end

ItemTipsCtrl._RefreshPickUpNode = HL.Method(HL.String) << function(self, itemId)
    local isPickUp, _ = Tables.useItemTable:TryGetValue(itemId)
    self.view.pickUpNode.gameObject:SetActive(isPickUp)
    self.view.portableDeviceNode.gameObject:SetActive(Utils.isPortableDevice(itemId))
end

ItemTipsCtrl._RefreshLiquidDischargeNode = HL.Method(HL.String) << function(self, itemId)
    local isLiquid = Tables.liquidTable:ContainsKey(itemId)
    local canBeDischarge = FactoryUtils.getLiquidCanBeDischarge(itemId)
    self.view.emissionNode.gameObject:SetActiveIfNecessary(isLiquid and canBeDischarge)
    self.view.emissionForbidNode.gameObject:SetActiveIfNecessary(isLiquid and not canBeDischarge)
end

ItemTipsCtrl._RefreshGasDischargeNode = HL.Method(HL.String) << function(self, itemId)
    local isLiquid = Tables.liquidTable:ContainsKey(itemId)
    if isLiquid then
        return
    end

    local isGas = Tables.gasTable:ContainsKey(itemId)
    local canBeDischarge = FactoryUtils.getLiquidCanBeDischarge(itemId)
    self.view.emissionNode.gameObject:SetActiveIfNecessary(isGas and canBeDischarge)
    self.view.emissionForbidNode.gameObject:SetActiveIfNecessary(isGas and not canBeDischarge)
end

ItemTipsCtrl._RefreshWeaponNode = HL.Method(HL.String, HL.Number, HL.Userdata) << function(self, itemId, instId, itemType)
    local isWeapon = itemType == GEnums.ItemType.Weapon
    if not isWeapon then
        return
    end
    self.view.stateCtrl:SetState("weapon")
    UIUtils.displayWeaponInfo(self.view, self.loader, itemId, instId)
end

ItemTipsCtrl._RefreshEquipNode = HL.Method(HL.String, HL.Number, HL.Userdata) << function(self, itemId, instId, itemType)
    local isEquip = itemType == GEnums.ItemType.Equip
    local isEquipFormula = itemType == GEnums.ItemType.EquipFormula
    if not isEquip and not isEquipFormula then
        return
    end
    self.view.stateCtrl:SetState("equip")
    if isEquipFormula then
        local _, equipFormulaData = Tables.equipFormulaTable:TryGetValue(itemId)
        if equipFormulaData then
            itemId = equipFormulaData.outcomeEquipId
            instId = 0
        end
    end
    UIUtils.displayEquipInfo(self.view, self.loader, itemId, instId)
end

ItemTipsCtrl._RefreshGemNode = HL.Method(HL.String, HL.Number, HL.Userdata) << function(self, itemId, instId, itemType)
    local isGem = itemType == GEnums.ItemType.WeaponGem
    if not isGem then
        self.view.gemNode.gameObject:SetActive(false)
        self.view.weaponCompatibleNode.gameObject:SetActive(false)
        return
    end
    self.view.stateCtrl:SetState("weaponGem")
    UIUtils.displayWeaponGemInfo(self.view, self.loader, itemId, instId)
end

ItemTipsCtrl._RefreshCountNode = HL.Method(HL.String, HL.Userdata, HL.Boolean) << function(self, itemId, itemType, showAllDepotCount)
    local typeData = Tables.itemTypeTable[itemType:ToInt()]
    local forceNoCount = typeData.showCountInTips == false

    self.view.countNode.gameObject:SetActive(not forceNoCount)
    self.view.countSpace.gameObject:SetActive(not forceNoCount)

    if forceNoCount then
        return
    end

    if Utils.isInBlackbox() and not UIManager:IsShow(PanelId.RewardsPopUpForBlackBox) then

        self.view.countNode.gameObject:SetActiveIfNecessary(false)
        self.view.countSpace.gameObject:SetActiveIfNecessary(false)
        return
    end

    local isPlaceInBag = GameInstance.player.inventory:IsPlaceInBag(itemType)
    local isMoney = not isPlaceInBag and GameInstance.player.inventory:IsMoneyType(itemType)
    local isAdventureExp = itemType == GEnums.ItemType.AdventureExp
    local forceShowOwnCount = self.m_args.forceShowOwnCount
    if forceShowOwnCount then
        self.view.countNode.allOwnCountText.text = Utils.getItemCount(itemId, true, true)
    else
        if not isMoney then
            if isPlaceInBag then
                local bagCount = Utils.getBagItemCount(itemId)
                self.view.countNode.bagCountText.text = bagCount
                local isWeekRaidItem, weekRaidItemData = Tables.weekRaidItemTable:TryGetValue(itemId)
                if isWeekRaidItem then
                    forceShowOwnCount = true
                    if not string.isEmpty(weekRaidItemData.convertItemId) then
                        self.view.countNode.allOwnCountText.text = Utils.getItemCount(weekRaidItemData.convertItemId) + bagCount
                    else
                        self.view.countNode.allOwnCountText.text = bagCount
                    end
                else
                    if showAllDepotCount then
                        self.view.countNode.allDepotCountText.text = Utils.getAllFacDepotItemCount(itemId)
                    else
                        self.view.countNode.depotCountText.text = Utils.getDepotItemCount(itemId)
                    end
                end
            else
                self.view.countNode.valuableCountText.text = Utils.getDepotItemCount(itemId)
            end
        else
            self.view.countNode.valuableCountText.text = Utils.getItemCount(itemId)
        end
    end

    self.view.countNode.valuableInfoNode.gameObject:SetActiveIfNecessary(not forceShowOwnCount and not isPlaceInBag and not isAdventureExp)
    self.view.countNode.depotInfoNode.gameObject:SetActiveIfNecessary(not forceShowOwnCount and isPlaceInBag and not showAllDepotCount)
    self.view.countNode.allDepotInfoNode.gameObject:SetActiveIfNecessary(not forceShowOwnCount and isPlaceInBag and showAllDepotCount)
    self.view.countNode.bagInfoNode.gameObject:SetActiveIfNecessary(not forceShowOwnCount and isPlaceInBag)
    self.view.countNode.allOwnInfoNode.gameObject:SetActiveIfNecessary(forceShowOwnCount)

    self.view.countNode.gameObject:SetActiveIfNecessary(true)
    self.view.countSpace.gameObject:SetActiveIfNecessary(true)
end

ItemTipsCtrl._RefreshProductNode = HL.Method(HL.String, HL.Boolean).Return(HL.Boolean) << function(self, itemId, skipItemTable)
    local productIds = FactoryUtils.getItemProductItemList(itemId, skipItemTable)
    if productIds then
        self.view.productNode.gameObject:SetActive(true)
        local count = #productIds
        local isShowMoreDeco = count > self.view.config.MAX_BLUEPRINT_PRODUCT_COUNT
        local showCount = lume.clamp(count, 0, self.view.config.MAX_BLUEPRINT_PRODUCT_COUNT)

        self.m_productCellCache:Refresh(showCount, function(cell, index)
            local productId = productIds[index]
            local itemData = Tables.itemTable:GetValue(productId)
            cell.itemIcon:InitItemIcon(productId)
            UIUtils.setItemRarityImage(cell.rarityLine, itemData.rarity)
            UIUtils.setItemRarityImage(cell.rarityLight, itemData.rarity)
        end)
        self.view.productNode.moreDeco.transform:SetAsLastSibling()
        self.view.productNode.moreDeco.gameObject:SetActive(isShowMoreDeco)
    else
        self.view.productNode.gameObject:SetActive(false)
    end
    return productIds ~= nil
end

ItemTipsCtrl._RefreshEmptyNode = HL.Method(HL.String, HL.Userdata) << function(self, itemId, itemType)
    local isShowEmptyNode = self:_CheckIfShowEmptyNode(itemId, itemType)

    self.view.emptyNode.gameObject:SetActive(isShowEmptyNode)
end

ItemTipsCtrl._RefreshTagInfoNode = HL.Method(HL.String, HL.Userdata) << function(self, itemId, itemType)
    local isShowTagNode, tagList = self:_TryGetTagList(itemId, itemType)
    self.view.tagInfoNode.gameObject:SetActive(isShowTagNode)
    if isShowTagNode then
        local tagCount = tagList.Count
        self.m_tagCellCache:Refresh(math.min(tagCount, self.view.config.MAX_TAG_COUNT), function(cell, index)
            local tagId = tagList[CSIndex(index)]
            local tagData = Tables.factoryIngredientTagTable:GetValue(tagId)
            local isShowGreyBg = not (itemType == GEnums.ItemType.NormalBuilding or itemType == GEnums.ItemType.FuncBuilding)


            cell.whiteBG.gameObject:SetActive(not isShowGreyBg)
            cell.greyBG.gameObject:SetActive(isShowGreyBg)
            cell.text.text = tagData.tagLabel

            cell.gameObject.name = "Tag-" .. tagId
        end)
    end
end

ItemTipsCtrl._RefreshLiquidInfoNode = HL.Method(HL.String) << function(self, itemId)
    local liquidInfoNode = self.view.liquidInfoNode
    liquidInfoNode.gameObject:SetActive(false)
    self.view.itemObtainWays.view.obtainTitle.text = Language[OBTAIN_WAYS_NORMAL_TITLE_TEXT_ID]
    self.view.productNode.title.text = Language[PRODUCT_NORMAL_TITLE_TEXT_ID]

    local isSystemUnlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.GeneralAbilityFluidInteract)
    if not isSystemUnlocked then
        return
    end

    local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    if not isEmptyBottle and not isFullBottle then
        return
    end

    liquidInfoNode.emptyIcon.gameObject:SetActive(isEmptyBottle)
    liquidInfoNode.icon.gameObject:SetActive(isFullBottle)
    liquidInfoNode.emptyLine.gameObject:SetActive(isEmptyBottle)
    liquidInfoNode.rarityLine.gameObject:SetActive(isFullBottle)

    if isEmptyBottle then

        local emptyBottleData = Tables.emptyBottleTable[itemId]
        local capacity = emptyBottleData.liquidCapacity
        liquidInfoNode.capacityTxt.text = string.format(Language[LIQUID_EMPTY_CAPACITY_TEXT_ID], capacity)
        liquidInfoNode.nameTxt.text = Language[LIQUID_EMPTY_NAME_TEXT_ID]
    end

    if isFullBottle then
        local fullBottleData = Tables.fullBottleTable[itemId]
        local liquidItemId = fullBottleData.liquidId
        local liquidSuccess, liquidItemData = Tables.itemTable:TryGetValue(liquidItemId)
        if liquidSuccess then

            liquidInfoNode.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, liquidItemData.iconId)

            liquidInfoNode.nameTxt.text = liquidItemData.name

            local rarityColor = UIUtils.getItemRarityColor(liquidItemData.rarity)
            liquidInfoNode.rarityLine.color = rarityColor

            if not self:_RefreshProductNode(itemId, true) then
                self.view.productNode.title.text = Language[PRODUCT_LIQUID_TITLE_TEXT_ID]
                self:_RefreshProductNode(liquidItemId, false)
            end
        end


        local liquidCount = fullBottleData.liquidCapacity
        liquidInfoNode.capacityTxt.text = string.format(Language[LIQUID_FULL_CAPACITY_TEXT_ID], liquidCount)
    end

    liquidInfoNode.gameObject:SetActive(true)
end

ItemTipsCtrl._RefreshGasInfoNode = HL.Method(HL.String) << function(self, itemId)
    local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
    if not isEmptyBottle and not isFullBottle then
        return
    end

    local liquidInfoNode = self.view.liquidInfoNode
    liquidInfoNode.emptyIcon.gameObject:SetActive(isEmptyBottle)
    liquidInfoNode.icon.gameObject:SetActive(isFullBottle)
    liquidInfoNode.emptyLine.gameObject:SetActive(isEmptyBottle)
    liquidInfoNode.rarityLine.gameObject:SetActive(isFullBottle)

    if isEmptyBottle then

        local emptyBottleData = Tables.emptyGasJarTable[itemId]
        local capacity = emptyBottleData.gasCapacity
        liquidInfoNode.capacityTxt.text = string.format(Language[LIQUID_EMPTY_CAPACITY_TEXT_ID], capacity)
        liquidInfoNode.nameTxt.text = Language.LUA_ITEM_TIPS_GAS_EMPTY_NAME
    end

    if isFullBottle then
        local fullBottleData = Tables.fullGasJarTable[itemId]
        local gasItemId = fullBottleData.gasId
        local gasSuccess, gasItemData = Tables.itemTable:TryGetValue(gasItemId)
        if gasSuccess then
            liquidInfoNode.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, gasItemData.iconId)
            liquidInfoNode.nameTxt.text = gasItemData.name
            local rarityColor = UIUtils.getItemRarityColor(gasItemData.rarity)
            liquidInfoNode.rarityLine.color = rarityColor

            if not self:_RefreshProductNode(itemId, true) then
                self.view.productNode.title.text = Language[PRODUCT_LIQUID_TITLE_TEXT_ID]
                self:_RefreshProductNode(gasItemId, false)
            end
        end


        local gasCount = fullBottleData.gasCapacity
        liquidInfoNode.capacityTxt.text = string.format(Language[LIQUID_FULL_CAPACITY_TEXT_ID], gasCount)
    end

    liquidInfoNode.gameObject:SetActive(true)
end

ItemTipsCtrl._RefreshShareNode = HL.Method(HL.String) << function(self, itemId)
    local showShareNode = FactoryUtils.isItemSocialBuilding(itemId)
    self.view.shareNode.gameObject:SetActive(showShareNode)
end

ItemTipsCtrl._OnDetailContentSizeChanged = HL.Method() << function(self)
    self.view.detailContentLayoutElement.enabled = false
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.detailContent)
    local detailContentHeight = self.view.detailContent.rect.size.y
    local detailShowHeight = self.view.detailScrollLayoutElement.preferredHeight
    local canScroll = detailContentHeight > detailShowHeight
    if canScroll then

        self.view.detailContentLayoutElement.minHeight = detailShowHeight + self.m_detailContentTopPadding
        self.view.detailContentLayoutElement.enabled = true
        LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.detailContent)
    end
end

ItemTipsCtrl._UpdateCount = HL.Method() << function(self)
    self.view.countNode.depotCountText.text = Utils.getDepotItemCount(self.m_itemId)
    self.view.countNode.bagCountText.text = Utils.getBagItemCount(self.m_itemId)
end




ItemTipsCtrl._OnClickObtainCell = HL.Method() << function(self)
end



ItemTipsCtrl._CheckShouldAutoClose = HL.Method() << function(self)
    if self:_IsFullScreen() then
        return
    end

    if DeviceInfo.usingController then
        return
    end

    if self.m_autoCloseTime > 0 then
        if Time.unscaledTime >= self.m_autoCloseTime then
            self:_CloseTips()
            return
        end
    end

    if self.m_autoClose then
        local hasCloseAction, isClick = self:_HasActionOnOtherArea()
        if hasCloseAction and (self.m_args.includeInAutoCloseLayer == false
            or UIAutoCloseArea.IsTopAutoCloseLayer(self.view.gameObject)) then
            if not isClick then
                self:_CloseTips()
            else
                local isClickUp = InputManager.GetMouseButtonUp(0)
                if isClickUp then



                    self.m_autoCloseTime = Time.unscaledTime
                else
                    if self.m_autoCloseTime <= 0 then
                        self.m_autoCloseTime = Time.unscaledTime + self.view.config.AUTO_CLOSE_DELAY
                    end
                end
            end
        end
    end
end


ItemTipsCtrl._HasActionOnOtherArea = HL.Method().Return(HL.Boolean, HL.Opt(HL.Boolean)) << function(self)
    if self.m_stopCheckClose then
        return false
    end



    local scrolling = math.abs(InputManager.mouseScrollDelta.y) > 0
    if (InputManager.anyKeyDown and not InputManagerInst.usingController) or scrolling then
        local isInputValidInSafeArea = false
        if InputManager.IsLeftMouseDown() then
            isInputValidInSafeArea = true
        elseif InputManager.GetMouseButton(1) then
            isInputValidInSafeArea = true
        elseif scrolling then
            isInputValidInSafeArea = true
        end
        if not isInputValidInSafeArea then

            return true
        else

            local canvasSize = self.view.rectTransform.rect.size
            local mousePos = InputManager.mousePosition
            local isClickSelf = UIUtils.isScreenPosInRectTransform(mousePos, self.view.content, self.uiCamera)
            if not isClickSelf then
                for rect, _ in pairs(self.m_extraSafeAreas) do
                    local isSafe = UIUtils.isScreenPosInRectTransform(mousePos, rect, self.uiCamera)
                    if isSafe then
                        isClickSelf = true
                        break
                    end
                end
            end
            return not isClickSelf, true
        end
    end

    return false
end

ItemTipsCtrl._CloseTips = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    if UIManager:IsHide(PANEL_ID) or self.m_isTipsClosing then
        return
    end

    local onClose
    if self.m_args then
        if self.m_args.includeInAutoCloseLayer ~= false then
            UIAutoCloseArea.RemoveFromAutoCloseLayer(self.view.gameObject)
        end
        onClose = self.m_args.onClose
        self.m_args.onClose = nil
    end

    self:_ClearArgs()
    if skipAnim then
        self:Hide()
        self.m_isTipsClosing = false
        if onClose then
            onClose()
        end
    else
        self.m_isTipsClosing = true
        self:PlayAnimationOutWithCallback(function()
            self.m_isTipsClosing = false
            if self.m_cachedArgs == nil then
                self:Hide()
                if onClose then
                    onClose()
                end
            else
                if onClose then
                    onClose()
                end
                self:_ShowTips(self.m_cachedArgs)
            end
        end)
    end
end

ItemTipsCtrl.m_extraSafeAreas = HL.Field(HL.Table)

ItemTipsCtrl._AddItemTipsSafeArea = HL.Method(RectTransform) << function(self, rect)
    self.m_extraSafeAreas[rect] = true
end

ItemTipsCtrl._RemoveItemTipsSafeArea = HL.Method(RectTransform) << function(self, rect)
    self.m_extraSafeAreas[rect] = nil
end






ItemTipsCtrl.m_updateCor = HL.Field(HL.Thread)
ItemTipsCtrl._RegisterCallback = HL.Method() << function(self)
    self.m_updateCor = self:_StartCoroutine(function()
        coroutine.step()
        while true do
            coroutine.step()
            if self:IsShow() and not self:IsPlayingAnimationOut() then
                self:_Update()
            end
        end
    end)
end

ItemTipsCtrl._ClearRegister = HL.Method() << function(self)
    self:_ClearCoroutine(self.m_updateCor)
    self.m_updateCor = nil
    self:_ClearCoroutine(self.m_controllerSyncPosCor)
    self.m_controllerSyncPosCor = nil
end

ItemTipsCtrl.OnItemCountChanged = HL.Method(HL.Any) << function(self, arg)
    if string.isEmpty(self.m_itemId) then
        return
    end
    local itemId2DiffCount = unpack(arg)
    if itemId2DiffCount:ContainsKey(self.m_itemId) then
        self:_UpdateCount()
    end
end





ItemTipsCtrl._StartPlace = HL.Method() << function(self)
    Notify(MessageConst.FAC_ENTER_BUILDING_MODE, {
        itemId = self.m_itemId,
        slotIndex = self.m_args.slotIndex,
        fromDepot = self.m_args.fromDepot,
        fastEnter = true,
        skipMainHudAnim = true,
    })
    self:_CloseTips(true)
end

ItemTipsCtrl._TryJump = HL.Method() << function(self)
    local canJump, jumpFunction = self:_CheckIfCanJump(self.m_itemId, self.m_data.type, self.m_instId)
    if not canJump then
        return
    end

    if self.m_args.onBeforeJump then
        self.m_args.onBeforeJump()
    end

    jumpFunction(self, self.m_itemId, self.m_instId)
    if canJump then
        self:_CloseTips(true)
    end
end

ItemTipsCtrl._UseItem = HL.Method() << function(self)
    if UIUtils.useItemOnTip(self.m_itemId) then
        self:_CloseTips(true)
    end
end

ItemTipsCtrl._ShowSplit = HL.Method() << function(self)
    UIUtils.splitItem(self.m_slotIndex)
    self:Hide()
end

ItemTipsCtrl._ShowClearBottle = HL.Method() << function(self)
    UIManager:Open(PanelId.ClearBottlePopUp, {
        slotIndex = self.m_slotIndex,
        fromDepot = self.m_args.fromDepot,
        itemId = self.m_itemId,
        itemCount = self.m_itemCount,
    })
    self:Hide()
end

ItemTipsCtrl._ShowWiki = HL.Method() << function(self)
    if not PhaseManager:CheckCanOpenPhaseAndToast(PhaseId.Wiki) then
        return
    end

    if UIManager:ShouldBlockObtainWaysPhaseJump(PhaseId.Wiki) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED)
        return
    end
    local itemId = self:_GetShowWikiItemId()
    if self.m_args.onBeforeJump then
        self.m_args.onBeforeJump()
    end
    Notify(MessageConst.SHOW_WIKI_ENTRY, { itemId = itemId })
    self:_CloseTips(true)
end

ItemTipsCtrl._GetShowWikiItemId = HL.Method().Return(HL.String) << function(self)
    return self.m_itemId
end

ItemTipsCtrl._GetNeedShowWikiBtn = HL.Method().Return(HL.Boolean) << function(self)
    local wikiItemId = self:_GetShowWikiItemId()
    return WikiUtils.canShowWikiEntry(wikiItemId)
end



ItemTipsCtrl._TryGetTagList = HL.Method(HL.Any, HL.Any).Return(HL.Boolean, HL.Opt(HL.Any)) << function(self, itemId, itemType)
    return UIUtils.tryGetTagList(itemId, itemType)
end

ItemTipsCtrl._CheckIfCanJump = HL.Method(HL.String, HL.Userdata, HL.Opt(HL.Number)).Return(HL.Boolean, HL.Opt(HL.Function)) << function(self, itemId, itemType, instId)
    if self.m_args and self.m_args.noJump then
        return false
    end

    if UIManager:ShouldBlockObtainWaysJump() then
        return false
    end

    local _, chestData = Tables.usableItemChestTable:TryGetValue(itemId)
    if chestData and chestData.type == GEnums.ItemCaseType.SelfSelectedBP then
        return true, self._JumpToBattlePassWeaponCase
    end

    if itemType == GEnums.ItemType.BusinessCardTopic then
        return true, self._JumpToBusinessCardTopic
    end

    if not instId or instId <= 0 then
        return false
    end

    local isWeapon = itemType == GEnums.ItemType.Weapon
    if isWeapon then
        local weaponInst = CharInfoUtils.getWeaponByInstId(instId)
        if weaponInst ~= nil then
            return true, self._JumpToWeapon
        end
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

ItemTipsCtrl._JumpToWeaponGem = HL.Method(HL.String, HL.Number) << function(self, gemTemplateId, gemInstId)
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


    CharInfoUtils.openWeaponInfoBestWay({
        weaponTemplateId = weaponInst.templateId,
        weaponInstId = weaponInst.instId,
        pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.GEM
    })
end

ItemTipsCtrl._JumpToWeapon = HL.Method(HL.String, HL.Number) << function(self, weaponTemplateId, weaponInstId)
    CharInfoUtils.openWeaponInfoBestWay({
        weaponTemplateId = weaponTemplateId,
        weaponInstId = weaponInstId,
    })
end

ItemTipsCtrl._JumpToBusinessCardTopic = HL.Method(HL.String, HL.Number) << function(self, itemId, instId)

    for topicId , topicCfg in pairs(Tables.businessCardTopicTable) do
        if topicCfg.itemId == itemId then
            PhaseManager:GoToPhase(PhaseId.FriendThemeChange, { selectId = topicId })
            return
        end
    end

    Notify(MessageConst.SHOW_TOAST, Language.LUA_BUSINESS_CARD_TOPIC_NOT_FOUND)
end

ItemTipsCtrl._JumpToBattlePassWeaponCase = HL.Method(HL.String, HL.Number) << function(self, itemId, instId)
    UIManager:Open(PanelId.BattlePassWeaponCase, { itemId = itemId, isPreview = true })
end

ItemTipsCtrl._CheckIfShowEmptyNode = HL.Method(HL.Any, HL.Any).Return(HL.Boolean, HL.Opt(HL.Boolean, HL.Any, HL.Any)) << function(self, itemId, itemType)
    if FactoryUtils.isItemSocialBuilding(itemId) then
        return false
    end

    if itemType == GEnums.ItemType.Material or itemType == GEnums.ItemType.NormalBuilding or itemType == GEnums.ItemType.FuncBuilding then
        local isShowTagNode = self:_TryGetTagList(itemId, itemType)
        local isLiquidOrGasRelated = self:_CheckIsLiquidRelatedBottleItem(itemId) or self:_CheckIsGasRelatedBottleItem(itemId)
        return not isShowTagNode and not isLiquidOrGasRelated
    end


    local isPickUp, _ = Tables.useItemTable:TryGetValue(itemId)
    if isPickUp then
        return false
    end

    if itemType == GEnums.ItemType.EquipFormula then
        return false
    end

    if itemType == GEnums.ItemType.Weapon then
        return false
    end

    if itemType == GEnums.ItemType.WeaponGem then

        local showGemNode = self.view.gemNode.gameObject.activeSelf
        return not showGemNode
    end

    if itemType == GEnums.ItemType.Equip then
        return false
    end

    if itemType == GEnums.ItemType.PortableDevice then
        return false
    end

    return true
end

ItemTipsCtrl._CheckIsLiquidRelatedBottleItem = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.GeneralAbilityFluidInteract) then
        return false
    end
    return FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
        or FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
end

ItemTipsCtrl._CheckIsGasRelatedBottleItem = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.GeneralAbilityFluidInteract) then
        return false
    end
    return FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
        or FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
end



ItemTipsCtrl.m_detailContentTopPadding = HL.Field(HL.Number) << 0
ItemTipsCtrl._OnDetailScroll = HL.Method() << function(self)
    local y = math.max(0, self.m_detailContentTopPadding - self.view.detailContent.anchoredPosition.y)
    local percent = 1 - (y / self.m_detailContentTopPadding)
    self.view.contentAnimationWrapper:SampleClipAtPercent("item_tips_scroll", percent)

end

ItemTipsCtrl._ToggleProductContent = HL.Method() << function(self)
    self.m_isShowBlueprintProduct = not self.m_isShowBlueprintProduct

    self.view.productNode.productContent.gameObject:SetActive(self.m_isShowBlueprintProduct)

    self.view.productNode.indicatorOn.gameObject:SetActive(not self.m_isShowBlueprintProduct)
    self.view.productNode.indicatorOff.gameObject:SetActive(self.m_isShowBlueprintProduct)
end






if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
    ItemTipsCtrl._InitTipsDebugBtn = HL.Method() << function(self)
        self.view.debugIDButton.gameObject:SetActive(true)
        self.view.debugIDButton.onClick:AddListener(function()
            Unity.GUIUtility.systemCopyBuffer = self.m_itemId
            Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 已复制ID %s", self.m_itemId))
        end)
        self.view.debugIDButton.onLongPress:AddListener(function()
            if self.m_instId <= 0 then
                return
            end
            Unity.GUIUtility.systemCopyBuffer = tostring(self.m_instId)
            Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 已复制InstID %s", tostring(self.m_instId)))
        end)
        if BEYOND_DEBUG then
            self.view.countNode.debugAddButton.gameObject:SetActive(true)
            self.view.countNode.debugAddButton.onClick:AddListener(function()
                local itemData = Tables.itemTable[self.m_itemId]
                local isMoney = GameInstance.player.inventory:IsMoneyType(itemData.type)
                if isMoney then
                    local msg = CS.Proto.CS_GM_COMMAND()
                    msg.Command = "AddMoney " .. self.m_itemId .. " 1000000"
                    CS.Beyond.Network.NetBus.instance.defaultSender:Send(msg)
                    Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 支付宝到账 一百万%s", self.m_data.name))
                else
                    local msg = CS.Proto.CS_GM_COMMAND()
                    msg.Command = "AddItemToItemBagSystem " .. self.m_itemId .. " 50"
                    CS.Beyond.Network.NetBus.instance.defaultSender:Send(msg)
                    Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 已添加道具 %s 50个", self.m_data.name))
                end
            end)
            self.view.countNode.debugAddButton.onLongPress:AddListener(function()
                local itemData = Tables.itemTable[self.m_itemId]
                local isMoney = GameInstance.player.inventory:IsMoneyType(itemData.type)
                if isMoney then
                    local msg = CS.Proto.CS_GM_COMMAND()
                    msg.Command = "AddMoney " .. self.m_itemId .. " 1"
                    CS.Beyond.Network.NetBus.instance.defaultSender:Send(msg)
                    Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 支付宝到账 1 %s", self.m_data.name))
                else
                    local msg = CS.Proto.CS_GM_COMMAND()
                    msg.Command = "AddItemToItemBagSystem " .. self.m_itemId .. " 1"
                    CS.Beyond.Network.NetBus.instance.defaultSender:Send(msg)
                    Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 已添加道具 %s 1", self.m_data.name))
                end
            end)
        end
    end

    ItemTipsCtrl._RefreshTipsDebugBtn = HL.Method() << function(self)
        self.view.debugIDButton.gameObject:SetActive(not self.m_args.isSideTips or not DeviceInfo.usingController)
        self.view.countNode.debugAddButton.gameObject:SetActive(not self.m_args.isSideTips or not DeviceInfo.usingController)
    end
end




HL.Commit(ItemTipsCtrl)
