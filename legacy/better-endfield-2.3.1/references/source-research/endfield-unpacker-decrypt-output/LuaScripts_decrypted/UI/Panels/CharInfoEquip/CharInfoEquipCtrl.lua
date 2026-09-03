local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoEquip
CharInfoEquipCtrl = HL.Class('CharInfoEquipCtrl', uiCtrl.UICtrl)







CharInfoEquipCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE] = 'OnSelectCharChange',
    [MessageConst.CHAR_INFO_SELECT_EQUIP_SLOT_CHANGE] = 'OnSelectEquipSlotChange',
    [MessageConst.CHAR_INFO_EMPTY_BUTTON_CLICK] = 'OnCommonEmptyButtonClick',
    [MessageConst.ON_PUT_ON_EQUIP] = 'OnPutOnEquip',
    [MessageConst.ON_PUT_OFF_EQUIP] = 'OnPutOffEquip',
    [MessageConst.ON_TACTICAL_ITEM_CHANGE] = 'OnTacticalItemChange',
    [MessageConst.ON_EQUIP_PRODUCE] = 'SetEquipListDirty',
    [MessageConst.ON_EQUIP_ENHANCE] = 'SetEquipListDirty',
}

CharInfoEquipCtrl.m_arg = HL.Field(HL.Table)

CharInfoEquipCtrl.m_charInfo = HL.Field(HL.Table)

CharInfoEquipCtrl.m_curMainControlTab = HL.Field(HL.Number) << UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

CharInfoEquipCtrl.m_compareNodeCellCache = HL.Field(HL.Table)

CharInfoEquipCtrl.m_curSelectSlotIndex = HL.Field(HL.Number) << -1

CharInfoEquipCtrl.m_curCompareEquipInstId = HL.Field(HL.Number) << 0

CharInfoEquipCtrl.m_curCompareTacticalItemId = HL.Field(HL.String) << ""

CharInfoEquipCtrl.m_isInCompare = HL.Field(HL.Boolean) << false

CharInfoEquipCtrl.m_suitTipNameCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoEquipCtrl.state = HL.Field(HL.Number) << UIConst.CHAR_INFO_EQUIP_STATE.Normal

CharInfoEquipCtrl.m_effectCor = HL.Field(HL.Thread)

CharInfoEquipCtrl.m_tabCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoEquipCtrl.m_equipListDirty = HL.Field(HL.Boolean) << false


CharInfoEquipCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    local initCharInfo = arg.initCharInfo or CharInfoUtils.getLeaderCharInfo()
    local mainControlTab = arg.mainControlTab or UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

    self.m_charInfo = initCharInfo
    self.m_curMainControlTab = mainControlTab

    self.view.backButton.gameObject:SetActive(false)

    self:_CleanUpCache()
    self:_InitActionEvent()
    self.view.commonItemList.gameObject:SetActive(false)
    self.view.equipDetailNode.leftNode.gameObject:SetActive(false)
    self.view.tacticalDetailNode.leftNode.gameObject:SetActive(false)
    self.view.btnEmpty.gameObject:SetActive(false)

    self.view.bgMask.gameObject:SetActive(false)
    self:_ToggleCompareMask(false)
    self:_InitController()
    self:_ProcessStateArg(self.m_arg.stateArg)
    self.m_arg.stateArg = nil
end

CharInfoEquipCtrl.OnShow = HL.Override() << function(self)
    if self.state == UIConst.CHAR_INFO_EQUIP_STATE.Normal then
        self:_RefreshRightPanel({
            charInfo = self.m_charInfo,
        })
    elseif self.state == UIConst.CHAR_INFO_EQUIP_STATE.Detail then
        if self.m_equipListDirty and self.m_curSelectSlotIndex >= 0 then
            self:_RefreshEquipList(self.m_curSelectSlotIndex)
            self.m_equipListDirty = false
        end
    end
end

CharInfoEquipCtrl.OnSelectCharChange = HL.Method(HL.Table) << function(self, charInfo)
    self.m_charInfo = charInfo
    self:_CleanUpCache()
    self:_RefreshRightPanel({
        charInfo = charInfo,
        slotIndex = self.m_curSelectSlotIndex,
        compareEquipInstId = self.m_curCompareEquipInstId,
    })
end

CharInfoEquipCtrl._RefreshRightPanel = HL.Method(HL.Table)
    << function(self, args)

    local charInfo = args.charInfo
    local slotIndex = args.slotIndex

    self:_RefreshEquipDetailPanel(args)
    self:_RefreshTacticalDetailPanel(args)

    local hasSelectSlot = slotIndex ~= nil and slotIndex >= 0
    local showCommon = not hasSelectSlot
    self.view.commonNode.gameObject:SetActive(showCommon)
    if showCommon then
        self:_RefreshCommonNode(charInfo)
    end
end

CharInfoEquipCtrl._RefreshEquipDetailPanel = HL.Method(HL.Table) << function(self, args)
    local charInfo = args.charInfo
    local slotIndex = args.slotIndex
    local hasSelectSlot = slotIndex ~= nil and slotIndex >= 0
    local isEquipSlot = slotIndex ~= UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInfo.instId)

    local compareEquipInstId = args.compareEquipInstId
    if compareEquipInstId == nil or compareEquipInstId <= 0 then
        if slotIndex ~= nil and slotIndex >= 0 then
            local equipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[slotIndex].equipIndex
            local _, slotEquipInstId = charInst.equipCol:TryGetValue(equipIndex)
            compareEquipInstId = slotEquipInstId
        end
    end

    local hasCompareEquip = compareEquipInstId ~= nil and compareEquipInstId > 0
    self.view.equipDetailNode.gameObject:SetActive(isEquipSlot and hasCompareEquip)
    if hasSelectSlot and isEquipSlot then
        self:_InnerRefreshEquipDetailNode(charInfo, slotIndex, compareEquipInstId)
    end


    if isEquipSlot and not hasCompareEquip then
        self:_ToggleCompareMask(false)
    end
end

CharInfoEquipCtrl._RefreshTacticalDetailPanel = HL.Method(HL.Table) << function(self, args)
    local charInfo = args.charInfo
    local slotIndex = args.slotIndex
    local compareTacticalId = args.compareTacticalId
    local hasSelectSlot = slotIndex ~= nil and slotIndex >= 0
    local isTacticalSlot = slotIndex == UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL

    local hasCompareTactical = compareTacticalId ~= nil and not string.isEmpty(compareTacticalId)
    self.view.tacticalDetailNode.gameObject:SetActive(isTacticalSlot and hasCompareTactical)
    if hasSelectSlot and isTacticalSlot then
        self:_InnerRefreshTacticalDetailPanel(charInfo, compareTacticalId)
    end


    if isTacticalSlot and not hasCompareTactical then
        self:_ToggleCompareMask(false)
    end
end

CharInfoEquipCtrl._InnerRefreshTacticalDetailPanel = HL.Method(HL.Table, HL.Opt(HL.String))
    << function(self, charInfo, compareTacticalId)

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInfo.instId)
    local equippedTacticalId = charInst.tacticalItemId

    if equippedTacticalId then
        self:_RefreshTacticalCell(self.view.tacticalDetailNode.leftNode, equippedTacticalId)
    end

    if compareTacticalId then
        self:_RefreshTacticalCell(self.view.tacticalDetailNode.rightNode, compareTacticalId)
    end

    local hasCompareTactical = compareTacticalId ~= nil and not string.isEmpty(compareTacticalId)
    local hasEquippedTactical = equippedTacticalId ~= nil and not string.isEmpty(equippedTacticalId)
    local canCompare = hasCompareTactical and hasEquippedTactical
    local isSameTactical = equippedTacticalId == compareTacticalId
    local showCompareNode = canCompare and self.m_isInCompare and not isSameTactical

    local canRemove = isSameTactical
    local canReplace = canCompare and (not isSameTactical)
    local canWear = hasCompareTactical and (not hasEquippedTactical)
    self.view.tacticalDetailNode.rightNode.btnRemove.gameObject:SetActive(canRemove)
    self.view.tacticalDetailNode.rightNode.btnReplace.gameObject:SetActive(canReplace)
    self.view.tacticalDetailNode.rightNode.btnEquip.gameObject:SetActive(canWear)
    self.view.tacticalDetailNode.rightNode.btnJump.gameObject:SetActive(false)

    local showCompareBtn = canCompare and not isSameTactical and not self.m_isInCompare
    UIUtils.PlayAnimationAndToggleActive(self.view.tacticalDetailNode.compareButtonAnimationWrapper, showCompareBtn)
    self.view.tacticalDetailNode.shrinkButton.gameObject:SetActive(canCompare and not isSameTactical and self.m_isInCompare)
    self.view.tacticalDetailNode.leftNode.gameObject:SetActive(showCompareNode)
    self:_ToggleCompareMask(showCompareNode)
end

CharInfoEquipCtrl._ToggleCompareMask = HL.Method(HL.Boolean) << function(self, isOn)
    UIUtils.PlayAnimationAndToggleActive(self.view.bgMask, isOn)

    Notify(MessageConst.ON_CHAR_INFO_EQUIP_TOGGLE_COMPARE_MASK, isOn)
end

CharInfoEquipCtrl._RefreshTacticalCell = HL.Method(HL.Table, HL.String) << function(self, cell, itemId)
    if itemId == nil or string.isEmpty(itemId) then
        return
    end

    local itemHasChange = cell.lastItemId == nil or cell.lastItemId ~= itemId
    local itemCfg = Tables.itemTable:GetValue(itemId)
    local itemCount = Utils.getBagItemCount(itemId)
    local useDesc = UIUtils.getItemUseDesc(itemId)
    local equipDesc = UIUtils.getItemEquippedDesc(itemId)
    cell.name.text = itemCfg.name
    cell.starGroup:InitStarGroup(itemCfg.rarity)
    cell.amountText.text = itemCount
    cell.effectText:SetAndResolveTextStyle(equipDesc)
    cell.descText.text = itemCfg.decoDesc
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemCfg.iconId)
    cell.useTxt:SetAndResolveTextStyle(useDesc)
    cell.equipTxt:SetAndResolveTextStyle(equipDesc)

    cell.lastItemId = itemId

    if itemHasChange then
        cell.animationWrapper:ClearTween()
        cell.animationWrapper:PlayInAnimation()
    end

    UIUtils.setItemRarityImage(cell.qualityColor, itemCfg.rarity)

    cell.countBG.color = itemCount > 0 and cell.config.COLOR_COUNT_BG_DEFAULT or cell.config.COLOR_COUNT_BG_EMPTY
end

CharInfoEquipCtrl._InnerRefreshEquipDetailNode = HL.Method(HL.Table, HL.Number, HL.Opt(HL.Number)) << function(self, charInfo, slotIndex, compareEquipInstId)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInfo.instId)
    local equipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[slotIndex].equipIndex
    local _, curEquipInstId = charInst.equipCol:TryGetValue(equipIndex)
    local hasCompareEquip = compareEquipInstId ~= nil and compareEquipInstId > 0

    local hasWearEquip = curEquipInstId ~= nil and curEquipInstId > 0
    local isSameEquip = curEquipInstId == compareEquipInstId
    local canCompare = hasCompareEquip and hasWearEquip and not isSameEquip
    local showCompareNode = canCompare and self.m_isInCompare

    local reachWearTierLimit = true
    if hasCompareEquip then
        local equipInstanceData = CharInfoUtils.getEquipByInstId(compareEquipInstId)
        local equipTemplateId = equipInstanceData.templateId
        local _, itemCfg = Tables.itemTable:TryGetValue(equipTemplateId)
        local charInstInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInfo.instId)
        reachWearTierLimit = charInstInfo.equipTierLimit >= itemCfg.rarity
        self:_RefreshBtnJump(self.view.equipDetailNode.rightNode, reachWearTierLimit, itemCfg, charInstInfo.templateId)
    else
        self.view.equipDetailNode.rightNode.btnJump.gameObject:SetActive(false)
    end

    UIUtils.PlayAnimationAndToggleActive(self.view.equipDetailNode.leftNode.animationWrapper, showCompareNode)
    self.view.equipDetailNode.compareButton.gameObject:SetActive(canCompare and not self.m_isInCompare)
    self.view.equipDetailNode.shrinkButton.gameObject:SetActive(canCompare and self.m_isInCompare)
    self:_ToggleCompareMask(showCompareNode)
    self.view.btnEmpty.gameObject:SetActive(showCompareNode)

    self:_RefreshEquipBasicNode(self.view.equipDetailNode.rightNode, charInfo.instId, compareEquipInstId, slotIndex)
    if canCompare then
        self:_RefreshEquipBasicNode(self.view.equipDetailNode.leftNode, charInfo.instId, curEquipInstId, slotIndex)
    end

    local canRemove = isSameEquip or (not hasCompareEquip) and reachWearTierLimit
    local canReplace = canCompare and reachWearTierLimit
    local canWear = hasCompareEquip and (not hasWearEquip) and reachWearTierLimit
    self.view.equipDetailNode.rightNode.btnRemove.gameObject:SetActive(canRemove)
    self.view.equipDetailNode.rightNode.btnReplace.gameObject:SetActive(canReplace)
    self.view.equipDetailNode.rightNode.btnEquip.gameObject:SetActive(canWear)

    self.view.equipDetailNode.rightNode.btnRemove.interactable = reachWearTierLimit
    self.view.equipDetailNode.rightNode.btnReplace.interactable = reachWearTierLimit
    self.view.equipDetailNode.rightNode.btnEquip.interactable = reachWearTierLimit
end

CharInfoEquipCtrl._RefreshBtnJump = HL.Method(HL.Table, HL.Boolean, HL.Opt(HL.Any, HL.Any)) << function(self, cell, reachWearTierLimit, compareEquipItemCfg, charTemplateId)
    cell.btnJump.gameObject:SetActive(false)
    cell.btnJump.gameObject:SetActive(not reachWearTierLimit)

    if not reachWearTierLimit then
        local equipTierLimit = compareEquipItemCfg.rarity
        local nodeId = CharInfoUtils.getQualifiedEquipBreakNodeIdByEquipTierLimit(equipTierLimit)
        local charGrowthDict = CharInfoUtils.getCharGrowthData(charTemplateId)
        local breakDetail = charGrowthDict.charBreakCostMap[nodeId]


        cell.btnJump.text = string.format(Language.LUA_CHAR_INFO_TALENT_UPGRADE_EQUIP_LOCK_HINT, breakDetail.name)
        cell.btnJump.gameObject:SetActive(true)
        cell.btnJump.onClick:RemoveAllListeners()
        cell.btnJump.onClick:AddListener(function()
            self:JumpToTalent(nodeId)
        end)
        return
    end
end

CharInfoEquipCtrl._RefreshEquipBasicNode = HL.Method(HL.Table, HL.Number, HL.Opt(HL.Number, HL.Number)) << function(self, cell, charInstId, equipInstId, slotIndex)
    if not equipInstId or equipInstId <= 0 then
        return
    end

    local equipInst = CharInfoUtils.getEquipByInstId(equipInstId)
    local equipTemplateId = equipInst.templateId
    local _, equipCfg = Tables.equipTable:TryGetValue(equipTemplateId)
    local itemCfg = Tables.itemTable:GetValue(equipTemplateId)
    local isEquipChange = cell.lastEquipInstId == nil or cell.lastEquipInstId ~= equipInstId

    cell.starGroup:InitStarGroup(itemCfg.rarity)
    cell.name.text = itemCfg.name
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemCfg.iconId)
    cell.minWearLv.text = equipCfg.minWearLv
    cell.weaponAttributeNode:InitEquipAttributeNode(equipInstId)
    cell.lockToggle:InitLockToggle(equipInst.templateId, equipInst.instId)
    UIUtils.setItemRarityImage(cell.qualityColor, itemCfg.rarity)

    local equipType = equipCfg.partType
    local equipTypeName = Language[UIConst.CHAR_INFO_EQUIP_TYPE_TILE_PREFIX .. LuaIndex(equipType:ToInt())]
    local partSpriteName = UIConst.EQUIP_TYPE_TO_INVERSE_ICON_NAME[equipType]
    cell.equipTypeName.text = equipTypeName
    cell.equipTypeIcon:LoadSprite(UIConst.UI_SPRITE_EQUIP_PART_ICON, partSpriteName)
    cell.equipSuitNode:InitEquipSuitNode(equipTemplateId, charInstId, equipInstId, slotIndex)
    cell.lastEquipInstId = equipInstId

    if isEquipChange then
        cell.animationWrapper:ClearTween()
        cell.animationWrapper:PlayInAnimation()
    end
end

CharInfoEquipCtrl._RefreshCommonNode = HL.Method(HL.Table) << function(self, charInfo)

    local charInstId = charInfo.instId

    self.view.commonNode.weaponAttributeNode:InitEquipAttributeFullNode(charInstId)
    self.view.commonNode.suitTitle.titleText.text = Language.LUA_CHAR_INFO_EQUIP_SUIT_TITLE
    self.view.commonNode.equipSuitNode:InitEquipSuitNodeByCharInstId(charInstId)

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInfo.instId)
    local equippedTacticalId = charInst.tacticalItemId
    local hasTactical = equippedTacticalId and not string.isEmpty(equippedTacticalId)
    self.view.commonNode.tacticalEquipDesc.gameObject:SetActive(hasTactical)
    self.view.commonNode.tacticalEquipTitle.gameObject:SetActive(hasTactical)
    self.view.commonNode.tacticalUseTitle.gameObject:SetActive(hasTactical)
    self.view.commonNode.tacticalUseDesc.gameObject:SetActive(hasTactical)
    self.view.commonNode.tacticalEmptyNode.gameObject:SetActive(not hasTactical)
    if hasTactical then
        self.view.commonNode.tacticalUseDesc:SetAndResolveTextStyle(UIUtils.getItemUseDesc(equippedTacticalId))
        self.view.commonNode.tacticalEquipDesc:SetAndResolveTextStyle(UIUtils.getItemEquippedDesc(equippedTacticalId))
    end

end


CharInfoEquipCtrl.OnSelectEquipSlotChange = HL.Method(HL.Table) << function(self, arg)
    self.state = UIConst.CHAR_INFO_EQUIP_STATE.Detail
    self:_CleanUpCache()

    local slotIndex = arg.slotIndex
    self.m_curSelectSlotIndex = slotIndex
    self.m_isInCompare = false

    self.view.rightNode.gameObject:SetActive(true)
    self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, true)
    self:_RefreshTabCellCache(slotIndex)
    self:_ToggleEquipList(true)
    self:_RefreshEquipList(slotIndex)
    self.view.enhanceBtn.gameObject:SetActive(slotIndex ~= UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL and Utils.isSystemUnlocked(GEnums.UnlockSystemType.EquipEnhance))

    if self.view.commonItemList.m_curSelectIndex <= 0 then
        self:_RefreshRightPanel({
            charInfo = self.m_charInfo,
            slotIndex = slotIndex,
        })
        if slotIndex == UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL then
            Notify(MessageConst.ON_CHAR_INFO_SELECT_TACTICAL_CHANGE, {
                slotIndex = slotIndex,
            })
        else
            Notify(MessageConst.ON_CHAR_INFO_SELECT_EQUIP_CHANGE, {
                slotIndex = slotIndex,
            })
        end
    end
end

CharInfoEquipCtrl.OnCompareEquipChange = HL.Method(HL.Opt(HL.Number)) << function(self, equipInstId)
    self.m_curCompareEquipInstId = equipInstId
    self:_RefreshRightPanel({
        charInfo = self.m_charInfo,
        slotIndex = self.m_curSelectSlotIndex,
        compareEquipInstId = equipInstId,
    })
    self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, true)
    self:Notify(MessageConst.ON_CHAR_INFO_SELECT_EQUIP_CHANGE, {
        slotIndex = self.m_curSelectSlotIndex,
        equipInstId = equipInstId
    })
end

CharInfoEquipCtrl.OnToggleEquipCompare = HL.Method(HL.Boolean) << function(self, isOn)
    self:_ToggleEquipSlotGroup(not isOn)
end

CharInfoEquipCtrl.OnChangeEquip = HL.Method(HL.Table) << function(self, arg)
    local equipInstId = arg.equipInstId
    local charInstId = self.m_charInfo.instId
    local slotIndex = self.m_curSelectSlotIndex
    local equipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[slotIndex].equipIndex

    if equipInstId > 0 then
        GameInstance.player.charBag:PutOnEquip(charInstId, equipIndex, equipInstId);
    else
        GameInstance.player.charBag:PutOffEquip(charInstId, equipIndex);
    end
end

CharInfoEquipCtrl._OnChangeTactical = HL.Method(HL.String) << function(self, itemId)
    GameInstance.player.charBag:ChangeTactical(self.m_charInfo.instId, itemId);

end

CharInfoEquipCtrl.OnReplaceEquip = HL.Method() << function(self)
    local confirmText = self:_GetConfirmText(self.m_curCompareEquipInstId)
    if string.isEmpty(confirmText) then
        self:OnChangeEquip({
            equipInstId = self.m_curCompareEquipInstId,
        })
    else
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = confirmText,
            onConfirm = function()
                self:OnChangeEquip({
                    equipInstId = self.m_curCompareEquipInstId,
                })
            end
        })
    end
end

CharInfoEquipCtrl.JumpToTalent = HL.Method(HL.String) << function(self, nodeId)

    self:Notify(MessageConst.CHAR_INFO_EQUIP_SECOND_CLOSE)
    self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, false)

    self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.TALENT,
        isFast = true,
        showGlitch = true,
        extraArg = {
            showCharBreakNodeId = nodeId,
        }
    })
end

CharInfoEquipCtrl.JumpToUpgrade = HL.Method() << function(self)

    self:Notify(MessageConst.CHAR_INFO_EQUIP_SECOND_CLOSE)

    self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE,
        isFast = true,
        showGlitch = true,
    })
end

CharInfoEquipCtrl.OnPutOnEquip = HL.Method(HL.Table) << function(self, arg)
    local newOwner, msg, switch = unpack(arg)

    if switch then
        AudioAdapter.PostEvent("au_ui_equip_change")
    elseif self.m_curSelectSlotIndex == UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY then
        AudioAdapter.PostEvent("au_ui_equip_puton_clothes")
    elseif self.m_curSelectSlotIndex == UIConst.CHAR_INFO_EQUIP_SLOT_MAP.HAND then
        AudioAdapter.PostEvent("au_ui_equip_puton_equipment")
    else
        AudioAdapter.PostEvent("au_ui_equip_puton_accessories")
    end

    Utils.triggerVoice("chrbark_gear", self.m_charInfo.templateId)

    local lastSelectIndexId = self.view.commonItemList.m_curSelectId
    self.m_isInCompare = false

    self.view.commonItemList:RefreshAllCells()

    if self.state == UIConst.CHAR_INFO_EQUIP_STATE.Detail then
        self:OnCompareEquipChange(lastSelectIndexId)
    end
end

CharInfoEquipCtrl.OnPutOffEquip = HL.Method(HL.Table) << function(self, arg)
    AudioAdapter.PostEvent("au_ui_equip_unload_equipment")

    local newOwner, msg, switch = unpack(arg)

    if self.m_curSelectSlotIndex == UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY then
        AudioAdapter.PostEvent("au_ui_equip_unload_equipment_clothes")
    elseif self.m_curSelectSlotIndex == UIConst.CHAR_INFO_EQUIP_SLOT_MAP.HAND then
        AudioAdapter.PostEvent("au_ui_equip_unload_equipment_equipment")
    else
        AudioAdapter.PostEvent("au_ui_equip_unload_equipment_accessories")
    end

    local lastSelectIndexId = self.view.commonItemList.m_curSelectId

    self.m_isInCompare = false

    if self.state == UIConst.CHAR_INFO_EQUIP_STATE.Detail then
        self.view.commonItemList:RefreshAllCells()
        self:OnCompareEquipChange(lastSelectIndexId)
    end
end

CharInfoEquipCtrl.OnTacticalItemChange = HL.Method(HL.Table) << function(self, arg)
    local itemId = unpack(arg)

    local lastSelectIndexId = self.view.commonItemList.m_curSelectId

    self.m_isInCompare = false

    if self.state == UIConst.CHAR_INFO_EQUIP_STATE.Detail then
        self.view.commonItemList:RefreshAllCells()
        self:OnSelectTacticalItemChange(lastSelectIndexId)
    end
end

CharInfoEquipCtrl.OnSelectTacticalItemChange = HL.Method(HL.Opt(HL.String)) << function(self, itemId)
    self.m_curCompareTacticalItemId = itemId

    self:_RefreshRightPanel({
        charInfo = self.m_charInfo,
        slotIndex = self.m_curSelectSlotIndex,
        compareTacticalId = itemId,
    })
    self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, true)
    self:Notify(MessageConst.ON_CHAR_INFO_SELECT_TACTICAL_CHANGE, {
        slotIndex = self.m_curSelectSlotIndex,
        itemId = itemId
    })
end



CharInfoEquipCtrl._InitActionEvent = HL.Method() << function(self)
    local isTrailCard = not CharInfoUtils.isCharDevAvailable(self.m_charInfo.instId)
    self.m_tabCellCache = UIUtils.genCellCache(self.view.tabGroup.tabCell)

    self.view.backButton.onClick:AddListener(function()
        self:OnCommonEmptyButtonClick()
        self:_CloseEquipDetail()
        self:Notify(MessageConst.CHAR_INFO_EQUIP_SECOND_CLOSE)
    end)
    self.view.commonNode.switchEquipButton.gameObject:SetActive(not isTrailCard)
    self.view.commonNode.switchEquipButton.onClick:AddListener(function()
        local isInFight = Utils.isInFight()
        if isInFight then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_IN_FIGHT_FORBID_INTERACT_TOAST)
            return
        end

        if isTrailCard then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_EQUIP_TRAIL_FORBID)
            return
        end

        self:Notify(MessageConst.ON_SELECT_SLOT_CHANGE, UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY)
    end)
    self.view.equipDetailNode.compareButton.onClick:AddListener(function()
        self:_ShowCompare(true)
    end)
    self.view.equipDetailNode.shrinkButton.onClick:AddListener(function()
        self:_ShowCompare(false)
    end)
    self.view.tacticalDetailNode.compareButton.onClick:AddListener(function()
        self:_ShowCompare(true)
    end)
    self.view.tacticalDetailNode.shrinkButton.onClick:AddListener(function()
        self:_ShowCompare(false)
    end)

    self.view.equipDetailNode.rightNode.btnRemove.onClick:AddListener(function()
        self:OnChangeEquip({
            equipInstId = 0,
        })
    end)
    self.view.equipDetailNode.rightNode.btnReplace.onClick:AddListener(function()
        self:OnReplaceEquip()
    end)
    self.view.equipDetailNode.rightNode.btnEquip.onClick:AddListener(function()
        self:OnReplaceEquip()
    end)


    self.view.tacticalDetailNode.rightNode.btnRemove.onClick:AddListener(function()
        self:_OnChangeTactical("")
    end)
    self.view.tacticalDetailNode.rightNode.btnReplace.onClick:AddListener(function()
        self:_OnChangeTactical(self.m_curCompareTacticalItemId)
    end)
    self.view.tacticalDetailNode.rightNode.btnEquip.onClick:AddListener(function()
        self:_OnChangeTactical(self.m_curCompareTacticalItemId)
    end)
    self.view.btnEmpty.onClick:AddListener(function()
        self:OnCommonEmptyButtonClick()
    end)
    self.view.commonNode.manufactureBtn.gameObject:SetActive(Utils.isSystemUnlocked(GEnums.UnlockSystemType.EquipProduce) and not isTrailCard)
    self.view.commonNode.produceBtn.onClick:AddListener(function()
        local equipSlotPanelPhaseItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfoEquipSlot)
        if equipSlotPanelPhaseItem and equipSlotPanelPhaseItem.uiCtrl:IsPlayingAnimationIn() then
            equipSlotPanelPhaseItem.uiCtrl.animationWrapper:SkipInAnimation()
        end
        PhaseManager:GoToPhase(PhaseId.EquipTech)
    end)

    self.view.enhanceBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.EquipTech, {
            isEnhance = true,
            equipInstId = self.m_curCompareEquipInstId,
        })
    end)
end

CharInfoEquipCtrl._GetConfirmText = HL.Method(HL.Int).Return(HL.Any) << function(self, equipInstId)
    local equipInstanceData = CharInfoUtils.getEquipByInstId(equipInstId)
    local text
    if equipInstanceData and equipInstanceData.equippedCharServerId > 0
        and self.m_charInfo.instId ~= equipInstanceData.equippedCharServerId then
        local equipTemplateId = equipInstanceData.templateId
        local _, itemCfg = Tables.itemTable:TryGetValue(equipTemplateId)
        local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(equipInstanceData.equippedCharServerId)
        local charName = Tables.characterTable[charInfo.templateId].name
        text = string.format(Language.LUA_EQUIP_REPLACE_CONFIRM, itemCfg.name, charName)
    end
    return text
end

CharInfoEquipCtrl.SetEquipListDirty = HL.Method(HL.Any) << function(self, arg)
    self.m_equipListDirty = true
end

CharInfoEquipCtrl._RefreshEquipList = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, curSelectSlotIndex)
    local showEquipList = self.state == UIConst.CHAR_INFO_EQUIP_STATE.Detail
    if not showEquipList then
        return
    end

    if curSelectSlotIndex >= 0 then
        local isTacticalItem = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[curSelectSlotIndex].isTacticalItem

        if isTacticalItem then
            self:_RefreshCommonTacticalItemList()
        else
            self:_RefreshCommonEquipList(curSelectSlotIndex)
        end
    end
end

CharInfoEquipCtrl._RefreshCommonTacticalItemList = HL.Method() << function(self)
    local charInstId = self.m_charInfo.instId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    local curSortCSIndex, curSortIsIncremental, selectedTags, skipGraduallyShow
    if self.m_arg.stateArg then
        curSortCSIndex = self.m_arg.stateArg.curSortCSIndex
        curSortIsIncremental = self.m_arg.stateArg.curSortIsIncremental
        selectedTags = self.m_arg.stateArg.selectedTags
        skipGraduallyShow = true
    end

    self.view.commonItemList:InitCommonItemList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.CHAR_INFO_TACTICAL_ITEM,
        defaultSelectedIndex = 1,
        selectedIndexId = charInst.tacticalItemId,
        filter_isFound = true,
        itemCount_onlyBag = true,
        refreshItemAddOn = function(cell, itemInfo)
            self:_RefreshItemCellAddOn(cell, itemInfo)
        end,
        onClickItem = function(args)
            local realClick = args.realClick
            local nextCell = args.nextCell
            local curCell = args.curCell
            local itemInfo = args.itemInfo

            if itemInfo then
                self:OnSelectTacticalItemChange(itemInfo.id)
            end

            if curCell then
                curCell.item.view.button.clickHintTextId = ""
            end
        end,
        enableKeyboardNavi = true,
        curSortCSIndex = curSortCSIndex,
        curSortIsIncremental = curSortIsIncremental,
        selectedTags = selectedTags,
        skipGraduallyShow = skipGraduallyShow,
    })



end

CharInfoEquipCtrl._RefreshCommonEquipList = HL.Method(HL.Number) << function(self, selectSlotIndex)
    local slotPartType = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[selectSlotIndex].slotPartType
    local charInstId = self.m_charInfo.instId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local maxWearLimit = charInst.equipTierLimit
    local charInstId = self.m_charInfo.instId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local equipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[selectSlotIndex].equipIndex
    local _, equipInstId = charInst.equipCol:TryGetValue(equipIndex)

    local curSortCSIndex, curSortIsIncremental, selectedTags, skipGraduallyShow
    if self.m_arg.stateArg then
        curSortCSIndex = self.m_arg.stateArg.curSortCSIndex
        curSortIsIncremental = self.m_arg.stateArg.curSortIsIncremental
        selectedTags = self.m_arg.stateArg.selectedTags
        skipGraduallyShow = true
    end

    self.view.commonItemList:InitCommonItemList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.CHAR_INFO_EQUIP,
        defaultSelectedIndex = 1,
        selectedIndexId = equipInstId,
        filter_equipType = slotPartType,
        refreshItemAddOn = function(cell, itemInfo)
            self:_RefreshEquipCellAddOn(cell, itemInfo)
        end,
        onClickItem = function(args)
            local realClick = args.realClick
            local nextCell = args.nextCell
            local curCell = args.curCell
            local itemInfo = args.itemInfo

            if itemInfo and self.m_curSelectSlotIndex >= 0 then
                self:OnCompareEquipChange(itemInfo.instId)
            end

            if curCell then
                curCell.item.view.button.clickHintTextId = ""
            end
        end,
        maxWearLimit = maxWearLimit,
        enableKeyboardNavi = true,
        curSortCSIndex = curSortCSIndex,
        curSortIsIncremental = curSortIsIncremental,
        selectedTags = selectedTags,
        skipGraduallyShow = skipGraduallyShow,
    })


end

CharInfoEquipCtrl._RefreshItemCellAddOn = HL.Method(HL.Table, HL.Table) << function(self, cell, itemInfo)
    cell.currentSelected.gameObject:SetActive(false)
    cell.disableMark.gameObject:SetActive(false)

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInfo.instId)
    local isEquippedItem = charInst.tacticalItemId == itemInfo.id
    UIUtils.PlayAnimationAndToggleActive(cell.imageCharMask, isEquippedItem)
    if isEquippedItem then
        local charTemplateId = charInst.templateId
        local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charTemplateId
        cell.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
    end
end

CharInfoEquipCtrl._RefreshEquipCellAddOn = HL.Method(HL.Table, HL.Table) << function(self, cell, itemInfo)
    local equipTemplateId = itemInfo.id
    local _, itemCfg = Tables.itemTable:TryGetValue(equipTemplateId)
    local equipInst = CharInfoUtils.getEquipByInstId(itemInfo.instId)
    local equipTemplate = Tables.equipTable:GetValue(equipTemplateId)
    local minWearLv = equipTemplate.minWearLv


    if equipInst == nil then
        return
    end

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInfo.instId)
    local belowMaxRarity = charInst.equipTierLimit < itemCfg.rarity

    cell.currentSelected.gameObject:SetActive(false)
    cell.disableMark.gameObject:SetActive(belowMaxRarity)



    local equippedCardInstId = equipInst.equippedCharServerId
    local isEquipped = equippedCardInstId and equippedCardInstId > 0

    UIUtils.PlayAnimationAndToggleActive(cell.imageCharMask, isEquipped)

    if isEquipped then
        local charEntityInfo = CharInfoUtils.getPlayerCharInfoByInstId(equippedCardInstId)
        local charTemplateId = charEntityInfo.templateId
        local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charTemplateId

        cell.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
    end
end

CharInfoEquipCtrl._CloseEquipDetail = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self.state = UIConst.CHAR_INFO_EQUIP_STATE.Normal
    self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, false)

    self:_CleanUpCache()
    InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.commonItemList.view.scrollRect.naviGroup)

    self:_ToggleEquipList(false)
end

local EQUIP_TAB_CONFIG = {
    [1] = {
        slotType = UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY,
        icon = "icon_equipmenttype_01",
    },
    [2] = {
        slotType = UIConst.CHAR_INFO_EQUIP_SLOT_MAP.HAND,
        icon = "icon_equipmenttype_02",
    },
    [3] = {
        slotType = UIConst.CHAR_INFO_EQUIP_SLOT_MAP.EDC_1,
        icon = "icon_equipmenttype_03",
    },
    [4] = {
        slotType = UIConst.CHAR_INFO_EQUIP_SLOT_MAP.EDC_2,
        icon = "icon_equipmenttype_04",

    },
    [5] = {
        slotType = UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL,
        isTactical = true,
        icon = "icon_equipmenttype_05",

    },
}

CharInfoEquipCtrl._ToggleEquipList = HL.Method(HL.Boolean) << function(self, isOn)
    local afterTransition = function()
        self.view.backButton.gameObject:SetActive(isOn)
        self.view.commonItemList.gameObject:SetActive(isOn)
        self.view.tabGroup.gameObject:SetActive(isOn)
        self.view.enhanceBtn.gameObject:SetActive(isOn and Utils.isSystemUnlocked(GEnums.UnlockSystemType.EquipEnhance))
    end

    local wrapper = self.animationWrapper
    if isOn and not self.view.commonItemList.view.gameObject.activeSelf then
        afterTransition()
        wrapper:PlayInAnimation()
    elseif not isOn and self.view.commonItemList.view.gameObject.activeSelf then

        self.view.luaPanel:BlockAllInput()
        local equipSlotPanelPhaseItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfoEquipSlot)
        if equipSlotPanelPhaseItem then
            equipSlotPanelPhaseItem.uiCtrl.view.luaPanel:BlockAllInput()
        end
        wrapper:PlayOutAnimation(function()
            self.view.luaPanel:RecoverAllInput()
            local equipSlotPanelPhaseItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfoEquipSlot)
            if equipSlotPanelPhaseItem then
                equipSlotPanelPhaseItem.uiCtrl.view.luaPanel:RecoverAllInput()
            end
            afterTransition()
            self:_RefreshRightPanel({
                charInfo = self.m_charInfo
            })
        end)
    end
    if DeviceInfo.usingController and not isOn then
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.commonItemList.view.scrollRect.naviGroup)
    end
end

CharInfoEquipCtrl._RefreshTabCellCache = HL.Method(HL.Number) << function(self, curSelectSlotIndex)
    self.m_tabCellCache:Refresh(#EQUIP_TAB_CONFIG, function(cell, index)
        local tabConfig = EQUIP_TAB_CONFIG[index]
        local isSelected = curSelectSlotIndex == tabConfig.slotType
        cell.icon:LoadSprite(UIConst.UI_SPRITE_EQUIP, tabConfig.icon)
        cell.default.gameObject:SetActive(not isSelected)
        cell.selectNode.gameObject:SetActive(isSelected)

        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            self:Notify(MessageConst.ON_SELECT_SLOT_CHANGE, tabConfig.slotType)
        end)
        cell.redDot:InitRedDot("Equip", { self.m_charInfo.instId, tabConfig.slotType })
    end)
end


CharInfoEquipCtrl._OnClickSuitButton = HL.Method(HL.Table) << function(self, suitTipsInfo)
    local tipCell = self.view.equipmentTipCell

    tipCell.gameObject:SetActive(true)
    local suitId = suitTipsInfo.suitId
    local hasValue, equipSuitClientDataList = Tables.equipSuitTable:TryGetValue(suitId)
    local equipList = equipSuitClientDataList.equipList

    local curEquips = {}
    local charInstId = self.m_charInfo.instId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    for _, equipInstId in pairs(charInst.equipCol) do
        local equipInstanceData = CharInfoUtils.getEquipByInstId(equipInstId)
        local equipTemplateId = equipInstanceData.templateId
        curEquips[equipTemplateId] = true
    end

    self.m_suitTipNameCellCache:Refresh(equipList.Count, function(cell, luaIndex)
        local equipId = equipList[CSIndex(luaIndex)]
        local _, data = Tables.equipTable:TryGetValue(equipId)

        cell.text.color =  self.view.config.SUIT_DISABLE_ICON

        cell.text.text = data.name
        cell.gameObject:SetActive(true)
    end)

    LayoutRebuilder.ForceRebuildLayoutImmediate(tipCell.transform)

    UIUtils.updateTipsPosition(tipCell.transform, suitTipsInfo.transform, self.view.rectTransform, self
        .uiCamera, UIConst.UI_TIPS_POS_TYPE.LeftDown)
end

CharInfoEquipCtrl.OnCommonEmptyButtonClick = HL.Method(HL.Opt(HL.Userdata)) << function(self, _)
    if not self.view.commonItemList.gameObject.activeSelf then
        return
    end

    if self.view.equipmentTipCell.gameObject.activeSelf then
        self.view.equipmentTipCell.gameObject:SetActive(false)
        return
    end

    self.view.equipmentTipCell.gameObject:SetActive(false)
    if self.view.equipDetailNode.shrinkButton.gameObject.activeSelf or
        self.view.tacticalDetailNode.shrinkButton.gameObject.activeSelf then
        self:_ShowCompare(false)
    end
end

CharInfoEquipCtrl._ShowCompare = HL.Method(HL.Boolean) << function(self, inCompare)
    self.m_isInCompare = inCompare

    if self.state == UIConst.CHAR_INFO_EQUIP_STATE.Detail then
        if self.m_curCompareEquipInstId ~= nil and self.m_curCompareEquipInstId > 0 then
            self:_RefreshRightPanel({
                charInfo = self.m_charInfo,
                slotIndex = self.m_curSelectSlotIndex,
                compareEquipInstId = self.m_curCompareEquipInstId,
            })
            self.view.equipDetailNode.compareButton.gameObject:SetActive(not inCompare)
            self.view.equipDetailNode.shrinkButton.gameObject:SetActive(inCompare)
        elseif self.m_curCompareTacticalItemId ~= nil and not string.isEmpty(self.m_curCompareTacticalItemId) then
            self:_RefreshRightPanel({
                charInfo = self.m_charInfo,
                slotIndex = self.m_curSelectSlotIndex,
                compareTacticalId = self.m_curCompareTacticalItemId,
            })
            self.view.tacticalDetailNode.compareButton.gameObject:SetActive(not inCompare)
            self.view.tacticalDetailNode.shrinkButton.gameObject:SetActive(inCompare)
        end
    end

    self.view.btnEmpty.gameObject:SetActive(inCompare)
    self.view.keyHintTabLeft.gameObject:SetActive(not inCompare)
    self.view.keyHintTabRight.gameObject:SetActive(not inCompare)
end

CharInfoEquipCtrl._CleanUpCache = HL.Method() << function(self)
    self.m_curSelectSlotIndex = -1
    self.m_curCompareEquipInstId = 0
    self.m_isInCompare = false
end



CharInfoEquipCtrl._InitController = HL.Method() << function(self)
    local charInfoPanelPhaseItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfo)
    local equipSlotPanelPhaseItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfoEquipSlot)
    if charInfoPanelPhaseItem and equipSlotPanelPhaseItem then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(
            {self.view.inputGroup.groupId, charInfoPanelPhaseItem.uiCtrl.view.inputGroup.groupId,
             equipSlotPanelPhaseItem.uiCtrl.view.inputGroup.groupId})
    else
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    end
    UIUtils.bindHyperlinkPopup(self, "CharInfoEquip", self.view.inputGroup.groupId)
end

CharInfoEquipCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg = {
        isDetail = self.state == UIConst.CHAR_INFO_EQUIP_STATE.Detail,
        isInCompare = self.m_isInCompare,
        curSelectSlotIndex = self.m_curSelectSlotIndex,
        curSortCSIndex = self.view.commonItemList.view.sortNode:GetCurSelectedIndex() - 1,
        curSortIsIncremental = self.view.commonItemList.view.sortNode.isIncremental,
        selectedTags = self.view.commonItemList.m_selectedTags,
        curSelectedIndex = self.view.commonItemList:IsAnyItemSelecting() and self.view.commonItemList:GetCurSelectIndex() or nil
    }
    return arg
end

CharInfoEquipCtrl._ProcessStateArg = HL.Method(HL.Table) << function(self, arg)
    if arg == nil or not arg.isDetail then
        return
    end
    self:Notify(MessageConst.ON_SELECT_SLOT_CHANGE, arg.curSelectSlotIndex)
    self.view.commonItemList:SetSelectedIndex(arg.curSelectedIndex, false)
    if arg.isInCompare then
        self:_ShowCompare(true)
    end
end



HL.Commit(CharInfoEquipCtrl)
