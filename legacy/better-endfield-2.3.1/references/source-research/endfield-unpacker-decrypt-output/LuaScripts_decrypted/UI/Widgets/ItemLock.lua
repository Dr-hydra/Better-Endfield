local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemLock = HL.Class('ItemLock', UIWidgetBase)

ItemLock.itemId = HL.Field(HL.String) << ""

ItemLock.instId = HL.Field(HL.Number) << 0


ItemLock._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_ITEM_FLAG_CHANGED, function(arg)
        self:_OnItemFlagChanged(arg)
    end)
end

ItemLock.InitItemLock = HL.Method(HL.Opt(HL.String, HL.Number)) << function(self, itemId, instId)
    self.itemId = ""

    if not instId or instId <= 0 then
        self.view.gameObject:SetActive(false)
        return
    end

    local itemData = Tables.itemTable[itemId]
    if itemData == nil then
        self.view.gameObject:SetActive(false)
        return
    end

    local itemType = itemData.type
    if itemType == GEnums.ItemType.Weapon or itemType == GEnums.ItemType.WeaponGem or itemType == GEnums.ItemType.Equip then
        self.view.gameObject:SetActive(true)
    else
        self.view.gameObject:SetActive(false)
        return
    end

    self:_FirstTimeInit()

    self.itemId, self.instId = itemId, instId
    local isLock = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemId, instId)
    local isTrash = GameInstance.player.inventory:IsItemTrash(Utils.getCurrentScope(), itemId, instId)
    self:_UpdateState(isLock, isTrash)
end

ItemLock._OnItemFlagChanged = HL.Method(HL.Table) << function(self, arg)
    if string.isEmpty(self.itemId) then
        return
    end

    local itemId, instId = unpack(arg)

    local isCurItem = self.itemId == itemId and self.instId == instId
    if not isCurItem then
        return
    end
    local isLock = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemId, instId)
    local isTrash = GameInstance.player.inventory:IsItemTrash(Utils.getCurrentScope(), itemId, instId)
    self:_UpdateState(isLock, isTrash)
end

ItemLock._UpdateState = HL.Method(HL.Boolean, HL.Boolean) << function(self, isLock, isTrash)
    local stateName = "normal"
    if isLock then
        stateName = "lock"
    elseif isTrash then
        stateName = "trash"
    end
    self.view.stateController:SetState(stateName)
end

HL.Commit(ItemLock)
return ItemLock
