
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.QuickEquipTacticalItem

QuickEquipTacticalItemCtrl = HL.Class('QuickEquipTacticalItemCtrl', uiCtrl.UICtrl)






QuickEquipTacticalItemCtrl.s_messages = HL.StaticField(HL.Table) << {

}

QuickEquipTacticalItemCtrl.m_tacticalItemId = HL.Field(HL.String) << ""

QuickEquipTacticalItemCtrl.m_getSquadCell = HL.Field(HL.Function)









QuickEquipTacticalItemCtrl.m_squadCellDataList = HL.Field(HL.Table)





QuickEquipTacticalItemCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_tacticalItemId = arg.tacticalItemId

    self:_InitAction()
    self:_InitController()
    self:_SetMainTacticalItem()
    self:_SetSquadTacticalItem()
end

QuickEquipTacticalItemCtrl._InitAction = HL.Method() << function(self)
    self.view.topNode.btnClose.onClick:AddListener(function()
        if self:_isDirty() then
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_CHANGE_NOT_SAVED_CONFIRM,
                onConfirm = function()
                    self:PlayAnimationOutAndClose()
                end,
            })
        else
            self:PlayAnimationOutAndClose()
        end
    end)
    self.view.rightNode.btnCommon.onClick:AddListener(function()
        self:_SaveAndClose()
    end)
end

QuickEquipTacticalItemCtrl._SetMainTacticalItem = HL.Method() << function(self)
    local itemId = self.m_tacticalItemId
    local cell = self.view.medicineNode
    local itemCfg = Tables.itemTable:GetValue(itemId)
    local itemCount = Utils.getBagItemCount(itemId)
    local useDesc = UIUtils.getItemUseDesc(itemId)
    local equipDesc = UIUtils.getItemEquippedDesc(itemId)
    cell.name.text = itemCfg.name
    CSUtils.UIContainerResize(cell.starInfoLayout, itemCfg.rarity)
    cell.storageCountTxt.text = itemCount
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemCfg.iconId)
    cell.useDescTxt:SetAndResolveTextStyle(useDesc)
    cell.equipDescTxt:SetAndResolveTextStyle(equipDesc)
    UIUtils.setItemRarityImage(cell.rarityColor, itemCfg.rarity)
end

QuickEquipTacticalItemCtrl._SetSquadTacticalItem = HL.Method() << function(self)
    self.m_getSquadCell = UIUtils.genCachedCellFunction(self.view.rightNode.scrollList)
    self.m_squadCellDataList = {}
    local squadSlots = GameInstance.player.squadManager.curSquad.slots
    for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        if i <= squadSlots.Count then
            local slot = squadSlots[CSIndex(i)]
            table.insert(self.m_squadCellDataList, {
                isEmpty = false,
                squadSlot = slot,
            })
        else
            table.insert(self.m_squadCellDataList, {
                isEmpty = true,
            })
        end
    end
    self.view.rightNode.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        self:_UpdateSquadCell(self.m_getSquadCell(object), LuaIndex(csIndex))
    end)
    self.view.rightNode.scrollList:UpdateCount(#self.m_squadCellDataList)
    self:_UpdateSaveBtn()
end

QuickEquipTacticalItemCtrl._UpdateSquadCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local data = self.m_squadCellDataList[index]
    data.cell = cell
    cell.stateController:SetState(data.isEmpty and 'empty' or 'normal')
    if data.isEmpty then
        return
    end

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnSquadCellClicked(index)
    end)

    cell.charHeadCellLongHpBar:InitCharFormationHeadCell({
        instId = data.squadSlot.charInstId,
        templateId = data.squadSlot.charId,
    })
    local isAlive = data.squadSlot.character ~= nil and data.squadSlot.character.abilityCom.alive
    cell.charHeadCellLongHpBar.view.charHeadBar.gameObject:SetActive(isAlive)
    if isAlive then
        local abilityCom = data.squadSlot.character.abilityCom
        local currentHpPct = abilityCom.hp / abilityCom.maxHp
        cell.charHeadCellLongHpBar.view.curHpFill.fillAmount = currentHpPct
    end

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(data.squadSlot.charInstId)
    local curTacticalItemId = charInst.tacticalItemId
    data.curTacticalItemId = curTacticalItemId
    self:_UpdateCellItemDetail(cell, curTacticalItemId)

    local _, targetItemCfg = Tables.itemTable:TryGetValue(self.m_tacticalItemId)
    if targetItemCfg then
        cell.medicineNode.newItem.iconImage:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, targetItemCfg.iconId)
        UIUtils.setItemRarityImage(cell.medicineNode.newItem.lineImage, targetItemCfg.rarity)
    end
    local isEquipped = curTacticalItemId == self.m_tacticalItemId
    data.isEquippedBefore = isEquipped
    self:_SetSquadCellEquipped(index, isEquipped)

    if DeviceInfo.usingController and index == 1 then
        self:SetNaviTarget(cell.button)
    end
    InputManagerInst:SetBindingText(cell.button.hoverConfirmBindingId, data.isEquipped and
        Language.LUA_QUICK_EQUIP_TACTICAL_ITEM_OFF or Language.LUA_QUICK_EQUIP_TACTICAL_ITEM_ON)
end

QuickEquipTacticalItemCtrl._UpdateCellItemDetail = HL.Method(HL.Table, HL.String, HL.Opt(HL.Boolean)) << function(
    self, cell, itemId, isEquipped)
    local hasCurItem, curItemCfg = Tables.itemTable:TryGetValue(itemId)
    local stateName = isEquipped and 'equipped' or (hasCurItem and 'normal' or 'empty')
    cell.detailNode.stateController:SetState(stateName)
    if curItemCfg then
        if not isEquipped then
            cell.medicineNode.currentItem.iconImage:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, curItemCfg.iconId)
            UIUtils.setItemRarityImage(cell.medicineNode.currentItem.lineImage, curItemCfg.rarity)
        end

        UIUtils.setItemRarityImage(cell.detailNode.rarityColor, curItemCfg.rarity)
        cell.detailNode.name.text = curItemCfg.name
        local itemCount = Utils.getBagItemCount(itemId)
        cell.detailNode.carryNode.storageCountTxt.text = itemCount
        cell.detailNode.carryNode.stateController:SetState(itemCount > 0 and 'normal' or 'empty')
        local useDesc = UIUtils.getItemUseDesc(itemId)
        local equipDesc = UIUtils.getItemEquippedDesc(itemId)
        cell.detailNode.useTxt:SetAndResolveTextStyle(useDesc)
        cell.detailNode.equipTxt:SetAndResolveTextStyle(equipDesc)
    end
end

QuickEquipTacticalItemCtrl._OnSquadCellClicked = HL.Method(HL.Number) << function(self, index)
    local cellData = self.m_squadCellDataList[index]
    self:_SetSquadCellEquipped(index, not cellData.isEquipped)
    InputManagerInst:SetBindingText(cellData.cell.button.hoverConfirmBindingId, cellData.isEquipped and
        Language.LUA_QUICK_EQUIP_TACTICAL_ITEM_OFF or Language.LUA_QUICK_EQUIP_TACTICAL_ITEM_ON)
    self:_UpdateSaveBtn()
end

QuickEquipTacticalItemCtrl._SetSquadCellEquipped = HL.Method(HL.Number, HL.Boolean) << function(self, index, isEquipped)
    local cellData = self.m_squadCellDataList[index]
    cellData.cell.detailNode.wearNode.stateController:SetState(isEquipped and 'equipped' or 'normal')
    cellData.isEquipped = isEquipped
    cellData.cell.medicineNode.newNode.gameObject:SetActive(isEquipped and cellData.curTacticalItemId ~= self.m_tacticalItemId)
    if cellData.curTacticalItemId == self.m_tacticalItemId then
        cellData.cell.medicineNode.currentItem.gameObject:SetActive(isEquipped)
    end
    local equippedItemId = cellData.curTacticalItemId
    if isEquipped then
        equippedItemId = self.m_tacticalItemId
    else
        if cellData.curTacticalItemId == self.m_tacticalItemId then
            equippedItemId = ""
        else
            equippedItemId = cellData.curTacticalItemId
        end
    end
    self:_UpdateCellItemDetail(cellData.cell, equippedItemId, isEquipped)
end

QuickEquipTacticalItemCtrl._isDirty = HL.Method().Return(HL.Boolean) << function(self)
    local isDirty = false
    for _, data in ipairs(self.m_squadCellDataList) do
        if not data.isEmpty and data.isEquipped ~= data.isEquippedBefore then
            isDirty = true
            break
        end
    end
    return isDirty
end

QuickEquipTacticalItemCtrl._UpdateSaveBtn = HL.Method() << function(self)
    local isDirty = self:_isDirty()
    self.view.rightNode.btnStateCtrl:SetState(isDirty and 'Normal' or 'Disabled')
end

QuickEquipTacticalItemCtrl._SaveAndClose = HL.Method() << function(self)
    for _, data in ipairs(self.m_squadCellDataList) do
        if not data.isEmpty and data.isEquipped ~= data.isEquippedBefore then
            GameInstance.player.charBag:ChangeTactical(data.squadSlot.charInstId, data.isEquipped and self.m_tacticalItemId or "")
        end
    end
    self:PlayAnimationOutAndClose()
    Notify(MessageConst.SHOW_TOAST, Language.LUA_QUICK_EQUIP_SAVED)
end

QuickEquipTacticalItemCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    UIUtils.bindHyperlinkPopup(self, "TacticalItem", self.view.inputGroup.groupId)
end

HL.Commit(QuickEquipTacticalItemCtrl)