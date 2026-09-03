local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

TrashToggle = HL.Class('TrashToggle', UIWidgetBase)

TrashToggle.itemId = HL.Field(HL.String) << ""

TrashToggle.instId = HL.Field(HL.Number) << 0



TrashToggle._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_ITEM_TRASH_STATE_CHANGED, function(arg)
        self:_OnItemTrashStateChanged(arg)
    end)

    self.view.toggle.onValueChanged:AddListener(function(isOn)
        self:_TrashItem(isOn)
    end)
end

TrashToggle.InitTrashToggle = HL.Method(HL.String, HL.Number).Return(HL.Boolean) << function(self, itemId, instId)
    self:_FirstTimeInit()

    self.itemId = ""
    if not instId or instId <= 0 or not (GameInstance.player.inventory:TryGetInstItem(Utils.getCurrentScope(), instId)) then
        self.view.gameObject:SetActive(false)
        return false
    end

    local itemData = Tables.itemTable[itemId]
    if itemData == nil then
        self.view.gameObject:SetActive(false)
        return false
    end

    local itemType = itemData.type
    if itemType == GEnums.ItemType.WeaponGem then
        self.view.gameObject:SetActive(true)
    else
        self.view.gameObject:SetActive(false)
        return false
    end

    self.itemId, self.instId = itemId, instId
    self:_UpdateTrashState()

    return true
end

TrashToggle._TrashItem = HL.Method(HL.Boolean) << function(self, isOn)
    if string.isEmpty(self.itemId) then
        return
    end

    local isItemTrash = GameInstance.player.inventory:IsItemTrash(Utils.getCurrentScope(), self.itemId, self.instId)
    if isItemTrash ~= isOn then
        GameInstance.player.inventory:TrashItem(Utils.getCurrentScope(), self.itemId, self.instId, isOn)
    end
end

TrashToggle._OnItemTrashStateChanged = HL.Method(HL.Table) << function(self, args)
    if string.isEmpty(self.itemId) then
        return
    end
    self:_UpdateTrashState()
end

TrashToggle._UpdateTrashState = HL.Method() << function(self)
    local isTrash = GameInstance.player.inventory:IsItemTrash(Utils.getCurrentScope(), self.itemId, self.instId)
    self.view.toggle:SetIsOnWithoutNotify(isTrash)
    if DeviceInfo.usingController then
        self:_UpdateControllerKeyHint(isTrash)
    end
end

TrashToggle._UpdateControllerKeyHint = HL.Method(HL.Boolean) << function(self, isTrash)
    local bindingId = self.view.toggle.hoverConfirmBindingId
    if bindingId > 0 then
        InputManagerInst:SetBindingText(bindingId, isTrash and
            Language.LUA_ITEM_UNTRASH_KEY_HINT or Language.LUA_ITEM_TRASH_KEY_HINT)
    end
end

HL.Commit(TrashToggle)
return TrashToggle

