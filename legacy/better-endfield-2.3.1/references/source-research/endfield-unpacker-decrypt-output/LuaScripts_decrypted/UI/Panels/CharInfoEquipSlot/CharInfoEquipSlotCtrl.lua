local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoEquipSlot
CharInfoEquipSlotCtrl = HL.Class('CharInfoEquipSlotCtrl', uiCtrl.UICtrl)

CharInfoEquipSlotCtrl.m_charInfo = HL.Field(HL.Table)

CharInfoEquipSlotCtrl.m_curMainControlTab = HL.Field(HL.Number) << UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

CharInfoEquipSlotCtrl.m_equipType2equipCell = HL.Field(HL.Table)

CharInfoEquipSlotCtrl.m_curSelectSlotIndex = HL.Field(HL.Number) << -1

CharInfoEquipSlotCtrl.m_scMainAttrCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoEquipSlotCtrl.m_fcAttrCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoEquipSlotCtrl.m_extraAttrCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoEquipSlotCtrl.m_tryAttributes = HL.Field(HL.Any)

CharInfoEquipSlotCtrl.m_inSlotMode = HL.Field(HL.Boolean) << true

CharInfoEquipSlotCtrl.state = HL.Field(HL.Number) << UIConst.CHAR_INFO_EQUIP_STATE.Normal








CharInfoEquipSlotCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE] = 'OnSelectCharChange',
    [MessageConst.ON_PUT_ON_EQUIP] = 'OnPutOnEquip',
    [MessageConst.ON_PUT_OFF_EQUIP] = 'OnPutOffEquip',
    [MessageConst.ON_SELECT_SLOT_CHANGE] = 'OnSelectSlotChange',
    [MessageConst.ON_TACTICAL_ITEM_CHANGE] = 'OnTacticalItemChange',
    [MessageConst.ON_CHAR_INFO_SELECT_EQUIP_CHANGE] = 'OnSelectEquipChange',
    [MessageConst.ON_CHAR_INFO_SELECT_TACTICAL_CHANGE] = 'OnSelectTacticalItemChange',
    [MessageConst.ON_CHAR_INFO_EQUIP_TOGGLE_COMPARE_MASK] = 'OnEquipToggleCompareMask',

}


CharInfoEquipSlotCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitController()
    self:_InitActionEvent()

    local initCharInfo = arg.initCharInfo or CharInfoUtils.getLeaderCharInfo()
    local mainControlTab = arg.mainControlTab or UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

    self.m_charInfo = initCharInfo
    self.m_curMainControlTab = mainControlTab
    self.m_tryAttributes = CS.Beyond.Gameplay.Core.Attributes:CreateDirectly()

    self:_ToggleSlotNode(true)
    self:_RefreshEquipSlotGroup(self.m_curSelectSlotIndex)

    if DeviceInfo.usingController then
        self.m_phase:_ActiveEquipPageNavi(self, true)
    end
end

CharInfoEquipSlotCtrl.OnClose = HL.Override() << function(self)
    if DeviceInfo.usingController then
        self.m_phase:_ActiveEquipPageNavi(self, false)
    end
end

CharInfoEquipSlotCtrl._ToggleSlotNode = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.centerNode.gameObject:SetActive(isOn)
    self.view.detailNode.gameObject:SetActive(not isOn)

    self.m_inSlotMode = isOn
    InputManagerInst:ToggleGroup(self.m_changeEquipTypeInputGroupId, not isOn)
end

CharInfoEquipSlotCtrl._InitActionEvent = HL.Method() << function(self)
    local equipType2equipCell = {}
    equipType2equipCell[UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY] = self.view.equipBody
    equipType2equipCell[UIConst.CHAR_INFO_EQUIP_SLOT_MAP.HAND] = self.view.equipHand
    equipType2equipCell[UIConst.CHAR_INFO_EQUIP_SLOT_MAP.EDC_1] = self.view.equipEDC_1
    equipType2equipCell[UIConst.CHAR_INFO_EQUIP_SLOT_MAP.EDC_2] = self.view.equipEDC_2
    equipType2equipCell[UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL] = self.view.equipTactical


    self.m_equipType2equipCell = equipType2equipCell

    for slotIndex, cell in pairs(equipType2equipCell) do
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            self:_OnClickSlotCell(slotIndex)
        end)
    end
    self.view.commonEmptyButton.onClick:AddListener(function()
        self:Notify(MessageConst.CHAR_INFO_EMPTY_BUTTON_CLICK)
    end)

    self.m_scMainAttrCellCache = UIUtils.genCellCache(self.view.abilityValueNode.scMainAttrCell)

    self.m_fcAttrCellCache = UIUtils.genCellCache(self.view.abilityValueNode.fcAttrCell)

    self.m_extraAttrCellCache = UIUtils.genCellCache(self.view.abilityValueNode.extraAttrCell)

    self:BindInputPlayerAction("char_equip_slot_next", function()
        if not self.view.detailNode.gameObject.activeSelf then
            return
        end

        local newIndex = self:_GetLoopedSlotIndex(self.m_curSelectSlotIndex + 1)
        AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")

        self:_OnClickSlotCell(newIndex)
    end, self.m_changeEquipTypeInputGroupId)

    self:BindInputPlayerAction("char_equip_slot_previous", function()
        if not self.view.detailNode.gameObject.activeSelf then
            return
        end

        local newIndex = self:_GetLoopedSlotIndex(self.m_curSelectSlotIndex - 1)
        AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")

        self:_OnClickSlotCell(newIndex)
    end, self.m_changeEquipTypeInputGroupId)
end

CharInfoEquipSlotCtrl._GetLoopedSlotIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, index)

    if index < UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY then
        return UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL
    elseif index > UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL then
        return UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY
    end

    return index
end

CharInfoEquipSlotCtrl._RefreshEquipSlotGroup = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, curSelectSlotIndex, needRefresh)
    local instId = self.m_charInfo.instId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    local equips = charInst.equipCol

    self.m_curSelectSlotIndex = curSelectSlotIndex

    for slotIndex, config in pairs(UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG) do
        local isSelectSlot = curSelectSlotIndex == slotIndex
        local isTacticalItem = config.isTacticalItem
        local cell = self.m_equipType2equipCell[slotIndex]
        cell.redDot:InitRedDot("Equip", { instId, slotIndex })
        if isTacticalItem then
            self:_RefreshTacticalSlot(cell, isSelectSlot)
        else
            local equipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[slotIndex].equipIndex
            local hasValue, equipInstId = equips:TryGetValue(equipIndex)
            self:_RefreshEquipSlot(cell, equipInstId, isSelectSlot, needRefresh and isSelectSlot)
        end
    end
end

CharInfoEquipSlotCtrl._RefreshTacticalSlot = HL.Method(HL.Table, HL.Opt(HL.Boolean)) << function(self, cell, isSelectSlot)
    if not cell then
        return
    end

    local instId = self.m_charInfo.instId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    local hasValue = charInst.tacticalItemId and (not string.isEmpty(charInst.tacticalItemId))

    cell.selectedMark.gameObject:SetActive(isSelectSlot)
    cell.iconRaw.gameObject:SetActive(hasValue)
    cell.plusIcon.gameObject:SetActive(not hasValue)
    cell.colorDeco.gameObject:SetActive(hasValue)

    cell.lock.gameObject:SetActive(false)

    cell.textNode.gameObject:SetActive(hasValue)
    cell.equippedNode.gameObject:SetActive(hasValue)
    if not hasValue then
        return
    end

    local itemCount = GameInstance.player.inventory:GetTacticalItemCount(
        Utils.getCurrentScope(), charInst.tacticalItemId, instId)

    cell.numText.text = itemCount

    cell.iconRaw.gameObject:SetActive(hasValue)
    local itemCfg = Tables.itemTable:GetValue(charInst.tacticalItemId)
    cell.iconRaw:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemCfg.iconId)

    UIUtils.setItemRarityImage(cell.colorDeco, itemCfg.rarity)

    cell.count.text = itemCount
end

CharInfoEquipSlotCtrl._RefreshEquipSlot = HL.Method(HL.Table, HL.Opt(HL.Number, HL.Boolean, HL.Boolean))
    << function(self, cell, equipInstId, isSelectSlot, needRefresh)
    if not cell then
        return
    end

    if cell.equipEnhanceNode then
        cell.equipEnhanceNode:InitEquipEnhanceNode({
            equipInstId = equipInstId,
        })
    end

    local hasValue = equipInstId > 0
    cell.selectedMark.gameObject:SetActive(isSelectSlot)
    cell.iconRaw.gameObject:SetActive(hasValue)
    cell.plusIcon.gameObject:SetActive(not hasValue)
    cell.colorDeco.gameObject:SetActive(hasValue)
    cell.equipmentLogo.gameObject:SetActive(false)
    cell.verticalStarGroup.gameObject:SetActive(hasValue)
    if not hasValue then
        return
    end

    local equipInst = CharInfoUtils.getEquipByInstId(equipInstId)
    local hasEquip = equipInst ~= nil
    cell.iconRaw.gameObject:SetActive(hasEquip)
    if hasEquip then
        local equipTemplateId = equipInst.templateId
        local _, equipData = Tables.equipTable:TryGetValue(equipTemplateId)
        if equipData.partType:ToInt() > 3 then

            cell.gameObject.SetActive(false)
        end

        local data = Tables.itemTable:GetValue(equipTemplateId)
        cell.iconRaw:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, data.iconId)
        cell.equipmentLogo.gameObject:SetActive(false)

        if needRefresh then
            cell.iconRaw:GetComponent("Animation"):Play("equipmentitem_icon_in")
        end
    else
        cell.equipmentLogo.gameObject:SetActive(false)
    end

    local equipTemplateId = equipInst.templateId
    local itemData = Tables.itemTable:GetValue(equipTemplateId)
    local rarity = itemData.rarity
    UIUtils.setItemRarityImage(cell.colorDeco, rarity)

    cell.verticalStarGroup:InitStarGroup(rarity)
end


CharInfoEquipSlotCtrl.RefreshCharInfo = HL.Method(HL.Table) << function(self, charInfo)
    self.m_charInfo = charInfo
    self:_RefreshEquipSlotGroup(self.m_curSelectSlotIndex)
end

CharInfoEquipSlotCtrl.OnPutOnEquip = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshEquipSlotGroup(self.m_curSelectSlotIndex, true)

    self.view.detailNodeAnimation:Play("charinfoequipslot_replacementequip_in")
end

CharInfoEquipSlotCtrl.OnPutOffEquip = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshEquipSlotGroup(self.m_curSelectSlotIndex, true)

    self.view.detailNodeAnimation:Play("charinfoequipslot_replacementequip_in")
end

CharInfoEquipSlotCtrl.OnTacticalItemChange = HL.Method(HL.Table) << function(self, arg)
    local itemId = unpack(arg)

    self:_RefreshEquipSlotGroup(self.m_curSelectSlotIndex, true)
end


CharInfoEquipSlotCtrl.OnSelectSlotChange = HL.Method(HL.Number) << function(self, index)
    self:_OnClickSlotCell(index)
end

CharInfoEquipSlotCtrl.OnSelectTacticalItemChange = HL.Method(HL.Table) << function(self, arg)
    local itemId = arg.itemId
    local slotIndex = arg.slotIndex
    self.view.abilityValueNode.gameObject:SetActive(false)
    if self.view.tacticalNode.gameObject.activeSelf then
        self.view.tacticalNode.animationWrapper:PlayInAnimation()
    else
        self.view.tacticalNode.gameObject:SetActive(true)
    end

    local hasSelectTactical = itemId ~= nil and not string.isEmpty(itemId)
    self.view.tacticalNode.drugNode.gameObject:SetActive(hasSelectTactical)
    self.view.tacticalNode.emptyNode.gameObject:SetActive(not hasSelectTactical)
    if not hasSelectTactical then
        return
    end

    local itemCfg = Tables.itemTable:GetValue(itemId)

    self.view.tacticalNode.tacticalIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemId)
    self.view.tacticalNode.tacticalNameTxt.text = itemCfg.name
end

CharInfoEquipSlotCtrl.OnEquipToggleCompareMask = HL.Method(HL.Boolean) << function(self, isOn)






    if not self.m_inSlotMode then
        UIUtils.PlayAnimationAndToggleActive(self.view.detailNode, not isOn)
    end
end

CharInfoEquipSlotCtrl.OnSelectEquipChange = HL.Method(HL.Table) << function(self, arg)
    local equipInstId = arg.equipInstId
    local slotIndex = arg.slotIndex

    self.view.abilityValueNode.gameObject:SetActive(true)
    self.view.tacticalNode.gameObject:SetActive(false)

    self.view.abilityValueNode.scrollView.gameObject:SetActive(true)
    self.view.abilityValueNode.emptyNode.gameObject:SetActive(false)


    local charInstId = self.m_charInfo.instId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local wearingEquipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[slotIndex].equipIndex
    local _, wearingEquipInstId = charInst.equipCol:TryGetValue(wearingEquipIndex)

    local charAttrFrom = CharInfoUtils.getCharFinalAttributes(charInstId)

    CS.Beyond.Gameplay.EquipUtil.TryGetCharTryOnEquipAttributes(charInstId, wearingEquipInstId, equipInstId, self.m_tryAttributes)
    local charAttrTo = CharInfoUtils.getCharFinalAttributesFromSpecificCache(self.m_tryAttributes)


    local scMainAttrFromShowList = CharInfoUtils.generateSCMainAttrShowInfoList(charAttrFrom, false, UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.EQUIP_FULL_ATTR)


    local scMainAttrToShowList = CharInfoUtils.generateSCMainAttrShowInfoList(charAttrTo, false, UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.EQUIP_FULL_ATTR)


    local fcAttrFromShowList = CharInfoUtils.generateFCAttrShowInfoList(charAttrFrom, false, UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.EQUIP_FULL_ATTR)


    local fcAttrToShowList = CharInfoUtils.generateFCAttrShowInfoList(charAttrTo, false, UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.EQUIP_FULL_ATTR)


    local scSubAttrFromShowList = CharInfoUtils.generateSCSubAttrShowInfoList(charAttrFrom, false, UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.EQUIP_FULL_ATTR)


    local scSubAttrToShowList = CharInfoUtils.generateSCSubAttrShowInfoList(charAttrTo, false, UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.EQUIP_FULL_ATTR)



    local comboScSubAttrShowDict = {}
    for _, info in ipairs(scSubAttrFromShowList) do
        comboScSubAttrShowDict[info.attributeType] = {
            fromInfo = info,
            toInfo = AttributeUtils.generateEmptyAttributeShowInfo(info.attributeType),
        }
    end

    for _, info in ipairs(scSubAttrToShowList) do
        local comboInfo = comboScSubAttrShowDict[info.attributeType]
        if comboInfo then
            comboInfo.toInfo = info
        else
            comboScSubAttrShowDict[info.attributeType] = {
                fromInfo = AttributeUtils.generateEmptyAttributeShowInfo(info.attributeType),
                toInfo = info
            }
        end
    end

    local comboShowList = {}
    for _, comboInfo in pairs(comboScSubAttrShowDict) do
        table.insert(comboShowList, comboInfo)
    end
    table.sort(comboShowList, function(a, b)
        return a.fromInfo.sortOrder < b.fromInfo.sortOrder
    end)

    self.m_scMainAttrCellCache:Refresh(#scMainAttrFromShowList, function(cell, index)
        local fromInfo = scMainAttrFromShowList[index]
        local toInfo = scMainAttrToShowList[index]
        self:_RefreshAttrCell(cell, fromInfo, toInfo)
    end)

    self.m_fcAttrCellCache:Refresh(#fcAttrFromShowList, function(cell, index)
        local fromInfo = fcAttrFromShowList[index]
        local toInfo = fcAttrToShowList[index]
        self:_RefreshAttrCell(cell, fromInfo, toInfo)
    end)

    self.m_extraAttrCellCache:Refresh(#comboShowList, function(cell, index)
        local fromInfo = comboShowList[index].fromInfo
        local toInfo = comboShowList[index].toInfo
        self:_RefreshAttrCell(cell, fromInfo, toInfo)
    end)
end

CharInfoEquipSlotCtrl._RefreshAttrCell = HL.Method(HL.Table, HL.Table, HL.Table) << function(self, cell, fromAttrInfo, toAttrInfo)
    local hasIconName = not string.isEmpty(fromAttrInfo.iconName)
    cell.icon.gameObject:SetActive(hasIconName == true)
    if hasIconName then
        cell.icon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, fromAttrInfo.iconName)
    end
    cell.attrTxt.text = fromAttrInfo.showName
    cell.toTxt.text = toAttrInfo.showValue

    cell.upIcon.gameObject:SetActive(false)
    cell.downIcon.gameObject:SetActive(false)


    local hasDiff = fromAttrInfo.showValue ~= toAttrInfo.showValue
    cell.fromTxt.gameObject:SetActive(hasDiff)
    cell.arrowNode.gameObject:SetActive(hasDiff)



    if hasDiff then
        cell.upIcon.gameObject:SetActive(fromAttrInfo.modifiedValue < toAttrInfo.modifiedValue)
        cell.downIcon.gameObject:SetActive(fromAttrInfo.modifiedValue > toAttrInfo.modifiedValue)
        cell.fromTxt.text = fromAttrInfo.showValue
    end


    local attrShowCfg = fromAttrInfo.attrShowCfg
    if attrShowCfg.equipFullAttrDisplayType == GEnums.AttributeDisplayType.OnlyDiff then
        cell.gameObject:SetActive(hasDiff)
        if not hasDiff then
            return
        end
    end



    if cell.iconShadow then
        local templateId = self.m_charInfo.templateId
        local charCfg = Tables.characterTable[templateId]
        local isMainAttrType = fromAttrInfo.attributeType == charCfg.mainAttrType
        local isSubAttrType = fromAttrInfo.attributeType == charCfg.subAttrType

        cell.bgImage.gameObject:SetActive(isMainAttrType or isSubAttrType)
        if isMainAttrType or isSubAttrType then
            cell.bgImage.color = isMainAttrType and self.view.config.ATTR_BG_COLOR_MAIN or self.view.config.ATTR_BG_COLOR_SUB
        end

        cell.icon.color = (isMainAttrType or isSubAttrType) and self.view.config.ATTR_ICON_COLOR_MAIN or self.view.config.ATTR_ICON_COLOR_OTHER

        cell.iconShadow.gameObject:SetActive(isMainAttrType or isSubAttrType)
        cell.iconShadow.color = (isMainAttrType or isSubAttrType) and self.view.config.ATTR_SHADOW_COLOR_MAIN or self.view.config.ATTR_SHADOW_COLOR_SUB
    end
end

CharInfoEquipSlotCtrl.OnCharInfoToggleExpandList = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.commonEmptyButton.gameObject:SetActive(isOn)
end

CharInfoEquipSlotCtrl.OnSelectCharChange = HL.Method(HL.Table) << function(self, charInfo)
    self.m_charInfo = charInfo
    self:_RefreshEquipSlotGroup(self.m_curSelectSlotIndex)
end

CharInfoEquipSlotCtrl.OnSelectWhenEnabled = HL.Method(HL.Number) << function(self, tabType)
    self:_OnClickSlotCell(UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY)
end

CharInfoEquipSlotCtrl._OnClickSlotCell = HL.Method(HL.Number) << function(self, slotIndex)
    local isInFight = Utils.isInFight()
    if isInFight then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_IN_FIGHT_FORBID_INTERACT_TOAST)
        return
    end






    local isTrailCard = not CharInfoUtils.isCharDevAvailable(self.m_charInfo.instId)
    if isTrailCard then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_EQUIP_TRAIL_FORBID)
        return
    end


    if slotIndex == self.m_curSelectSlotIndex then
        return
    end

    if slotIndex == UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL then
        self.view.tacticalNode.gameObject:SetActive(true)
        self.view.abilityValueNode.gameObject:SetActive(false)
    else
        self.view.tacticalNode.gameObject:SetActive(false)
        self.view.abilityValueNode.gameObject:SetActive(true)
    end

    self:Notify(MessageConst.CHAR_INFO_SELECT_EQUIP_SLOT_CHANGE, {
        slotIndex = slotIndex
    })

    self:Notify(MessageConst.CHAR_INFO_EQUIP_SECOND_OPEN)
    self:_RefreshEquipSlotGroup(slotIndex)
end

CharInfoEquipSlotCtrl.SetState = HL.Method(HL.Number) << function(self, state)
    if self.state == state  then
        return
    end

    local wrapper = self.animationWrapper
    if state == UIConst.CHAR_INFO_EQUIP_STATE.Normal then
        wrapper:PlayWithTween("charinfoequip3d_out")
        self:_RefreshEquipSlotGroup(-1)
        self:_ToggleSlotNode(true)
    else
        wrapper:SkipInAnimation()
        wrapper:PlayWithTween("charinfoequip3d_in")
        self:_ToggleSlotNode(false)
    end

    self.state = state
end


CharInfoEquipSlotCtrl.OnShow = HL.Override() << function(self)
    if self.state == UIConst.CHAR_INFO_EQUIP_STATE.Detail then
        self.animationWrapper:SkipInAnimation()
    end
    self:RefreshCharInfo(self.m_charInfo)
end



CharInfoEquipSlotCtrl.m_changeEquipTypeInputGroupId = HL.Field(HL.Number) << -1

CharInfoEquipSlotCtrl._InitController = HL.Method() << function(self)
    self.m_changeEquipTypeInputGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)
end



HL.Commit(CharInfoEquipSlotCtrl)
