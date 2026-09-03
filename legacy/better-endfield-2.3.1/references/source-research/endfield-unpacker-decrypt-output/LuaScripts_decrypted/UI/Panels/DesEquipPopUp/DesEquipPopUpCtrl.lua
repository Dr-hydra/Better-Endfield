
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DesEquipPopUp

DesEquipPopUpCtrl = HL.Class('DesEquipPopUpCtrl', uiCtrl.UICtrl)






DesEquipPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DesEquipPopUpCtrl.m_args = HL.Field(HL.Table)

DesEquipPopUpCtrl.m_getItemCell = HL.Field(HL.Function)

DesEquipPopUpCtrl.m_getReturnItemCell = HL.Field(HL.Function)

DesEquipPopUpCtrl.m_isKeyHintPosSet = HL.Field(HL.Boolean) << false


DesEquipPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_args = args

    self:_InitController()

    self.view.confirmButton.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)
    self.view.cancelButton.onClick:AddListener(function()
        self:_OnClickCancel()
    end)

    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.itemScrollList)
    self.view.itemScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateItemCell(self.m_getItemCell(obj), LuaIndex(csIndex))
    end)
    self.view.itemScrollList:UpdateCount(#self.m_args.items)
    local focusKeyHintPos = Vector3(math.max(-135 * #self.m_args.items / 2 -30 , -1080), 80, 0)
    self.view.focusKeyHint.localPosition = focusKeyHintPos

    self.m_getReturnItemCell = UIUtils.genCachedCellFunction(self.view.returnItemScrollList)
    self.view.returnItemScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateReturnItemCell(self.m_getReturnItemCell(obj), LuaIndex(csIndex))
    end)
    self.view.returnItemScrollList:UpdateCount(#self.m_args.returnItems)

    local needReturnOverHint = false
    for _, itemInfo in ipairs(self.m_args.returnItems) do
        local shouldCheck = true
        local _, itemData = Tables.itemTable:TryGetValue(itemInfo.id)
        if itemData then
            local _, typeData = Tables.itemTypeTable:TryGetValue(itemData.type)
            if typeData and typeData.storageSpace == GEnums.ItemStorageSpace.Isolate then
                shouldCheck = false
            end
        end
        if shouldCheck then
            local canPutInItem = GameInstance.player.inventory:CanItemBagOrValuableDepotPutInItem(Utils.getCurrentScope(), itemInfo.id, itemInfo.count)
            if not canPutInItem then
                needReturnOverHint = true
                break
            end
        end
    end
    self.view.returnOverHint.gameObject:SetActive(needReturnOverHint)
end

DesEquipPopUpCtrl._OnUpdateItemCell = HL.Method(HL.Forward("Item"), HL.Number) << function(self, cell, index)
    cell:InitItem(self.m_args.items[index], true)
    cell:SetExtraInfo({
        isSideTips = DeviceInfo.usingController,
    })
    if DeviceInfo.usingController then
        cell:SetEnableHoverTips(false)
    end
end

DesEquipPopUpCtrl._OnUpdateReturnItemCell = HL.Method(HL.Forward("Item"), HL.Number) << function(self, cell, index)
    cell:InitItem(self.m_args.returnItems[index], true)
    cell:SetExtraInfo({
        isSideTips = DeviceInfo.usingController,
    })
    if DeviceInfo.usingController then
        cell:SetEnableHoverTips(false)
    end
end

DesEquipPopUpCtrl._OnClickConfirm = HL.Method() << function(self)
    local args = self.m_args
    self:PlayAnimationOutWithCallback(function()
        self:Close()
        if args.onConfirm then
            args.onConfirm()
        end
    end)
end

DesEquipPopUpCtrl._OnClickCancel = HL.Method() << function(self)
    local onCancel = self.m_args.onCancel
    self:PlayAnimationOutWithCallback(function()
        self:Close()
        if onCancel then
            onCancel()
        end
    end)
end

DesEquipPopUpCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.itemNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end


HL.Commit(DesEquipPopUpCtrl)
