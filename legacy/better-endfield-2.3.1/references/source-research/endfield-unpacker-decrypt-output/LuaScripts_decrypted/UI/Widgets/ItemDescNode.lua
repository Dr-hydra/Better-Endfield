local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemDescNode = HL.Class('ItemDescNode', UIWidgetBase)


ItemDescNode._OnFirstTimeInit = HL.Override() << function(self)

end

ItemDescNode.InitItemDescNode = HL.Method(HL.String, HL.Opt(HL.Table)) << function(self, itemId, args)
    self:_FirstTimeInit()

    local itemCfg = Tables.itemTable:GetValue(itemId)

    self.view.defaultDesc:SetAndResolveTextStyle(itemCfg.desc)

    local isManualCraftItem = false
    local isTacticalItem = false
    local manualCraftId
    if itemCfg.type == GEnums.ItemType.FormulaUnlock then
        isManualCraftItem, manualCraftId = Tables.factoryManualCraftReverseTable:TryGetValue(itemId)
        if isManualCraftItem then
            local _, craftData = Tables.factoryManualCraftTable:TryGetValue(manualCraftId)
            if craftData and #craftData.outcomes > 0 then
                itemId = craftData.outcomes[0].id
            end
        end
    end
    self.view.decoSplitLine.gameObject:SetActive(isManualCraftItem)

    local isUseItem, useItemCfg = Tables.useItemTable:TryGetValue(itemId)
    if isUseItem and not useItemCfg.isValuableDepot then
        isTacticalItem = true
    end
    self.view.tacticalItemTitle.gameObject:SetActive(isTacticalItem)
    self.view.tacticalItemDesc.gameObject:SetActive(isTacticalItem)
    self.view.defaultDesc.gameObject:SetActive(not isTacticalItem or isManualCraftItem)

    if isTacticalItem then
        self.view.tacticalItemDesc:SetAndResolveTextStyle(UIUtils.getItemUseDesc(itemId))
    end

    local isEquipUnlock = Utils.isSystemUnlocked(GEnums.UnlockSystemType.Equip)
    local isEquipItem, equipItemCfg = Tables.equipItemTable:TryGetValue(itemId)
    local showEquipDesc = isEquipUnlock and isEquipItem
    self.view.equipItemTitle.gameObject:SetActive(showEquipDesc)
    self.view.equipItemDesc.gameObject:SetActive(showEquipDesc)
    if showEquipDesc then
        self.view.equipItemDesc:SetAndResolveTextStyle(UIUtils.getItemEquippedDesc(itemId))
    end

    self.view.decoDesc.gameObject:SetActive(not showEquipDesc)
    self.view.decoDesc.text = itemCfg.decoDesc
end

HL.Commit(ItemDescNode)
return ItemDescNode

