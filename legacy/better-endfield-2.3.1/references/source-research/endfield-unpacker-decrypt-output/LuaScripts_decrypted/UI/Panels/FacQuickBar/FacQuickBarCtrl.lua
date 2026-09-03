local QuickBarItemType = FacConst.QuickBarItemType

local autoCalcOrderUICtrl = require_ex('UI/Panels/Base/AutoCalcOrderUICtrl')
local PANEL_ID = PanelId.FacQuickBar
FacQuickBarCtrl = HL.Class('FacQuickBarCtrl', autoCalcOrderUICtrl.AutoCalcOrderUICtrl)






FacQuickBarCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.HIDE_FAC_QUICK_BAR] = 'HideFacQuickBar',

    [MessageConst.PLAY_FAC_QUICK_BAR_OUT_ANIM] = 'PlayOutAnim',
    [MessageConst.ON_BLOCK_KEYBOARD_EVENT_PANEL_ORDER_CHANGED] = 'PanelOrderChanged',

    [MessageConst.ON_ITEM_COUNT_CHANGED] = 'OnItemCountChanged',
    [MessageConst.ON_SET_IN_SAFE_ZONE] = 'OnSetInSafeZone',

    [MessageConst.ON_QUICK_BAR_CHANGED] = 'OnQuickBarChanged',

    [MessageConst.ON_START_UI_DRAG] = 'OnOtherStartDragItem',
    [MessageConst.ON_END_UI_DRAG] = 'OnOtherEndDragItem',

    [MessageConst.FAC_ON_BELT_UNLOCKED] = 'OnBeltUnlocked',
    [MessageConst.FAC_ON_PIPE_UNLOCKED] = 'OnPipeUnlocked',

    [MessageConst.NAVI_TO_FAC_QUICK_BAR] = 'NaviToFacQuickBar',
    [MessageConst.START_SET_BUILDING_ON_FAC_QUICK_BAR] = 'StartSetBuildingOnFacQuickBar',
    [MessageConst.START_SWITCH_SLOT_ON_FAC_QUICK_BAR] = 'StartSwitchSlotOnFacQuickBar',

    [MessageConst.QUICK_DROP_TO_FAC_QUICK_BAR] = '_OnQuickDropItemToQuickBar',
    [MessageConst.ON_NEW_SCOPE_INFO_RECEIVED] = '_OnNewScopeInfoReceived',

    [MessageConst.FAC_TOGGLE_CAN_DEACTIVE_QUICK_BAR] = 'ToggleCanDeactiveQuickBar',

    [MessageConst.ON_CHANGE_SPACESHIP_DOMAIN_ID] = 'OnChangeSpaceshipDomainId',
}

FacQuickBarCtrl.m_typeCells = HL.Field(HL.Forward("UIListCache"))

FacQuickBarCtrl.m_itemCells = HL.Field(HL.Forward("UIListCache"))


FacQuickBarCtrl.maxItemCount = HL.Const(HL.Number) << 9

FacQuickBarCtrl.m_useActiveAction = HL.Field(HL.Boolean) << false


FacQuickBarCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_attachedPanels = {}
    self.m_itemCells = UIUtils.genCellCache(self.view.itemSlot)
    self.m_typeCells = UIUtils.genCellCache(self.view.typeTabCell)

    self:_InitBeltNode()
    self:_InitPipeNode()
    self:_InitController()
    self.view.facQuickBarClearDropZone:InitFacQuickBarClearDropZone()
end

FacQuickBarCtrl.OnShow = HL.Override() << function(self)
    FacQuickBarCtrl.Super.OnShow(self)
    self.view.dropHint.gameObject:SetActive(false)
    self.view.facQuickBarClearDropZone.gameObject:SetActive(false)
end

FacQuickBarCtrl.OnHide = HL.Override() << function(self)
    FacQuickBarCtrl.Super.OnHide(self)
    if DeviceInfo.usingController then
        if not string.isEmpty(self.m_curSettingBuildingItemId) then
            self:_ExitSetBuilding()
        end


        self:_ToggleCellActionOnSetNaviTarget(true)
        if self.m_useActiveAction then






            self.view.main:ManuallyStopFocus()
        end
    end
end

FacQuickBarCtrl.OnClose = HL.Override() << function(self)
    FacQuickBarCtrl.Super.OnClose(self)
end

FacQuickBarCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, isActive)
end

FacQuickBarCtrl._RefreshContent = HL.Method() << function(self)
    self:_InitDataInfo()
    self:_RefreshTypes()
end




FacQuickBarCtrl.m_typeInfos = HL.Field(HL.Table)

FacQuickBarCtrl._InitDataInfo = HL.Method() << function(self)
    local remoteFactoryCore = GameInstance.player.remoteFactory.core
    local curChapterInfo = remoteFactoryCore:GetCurrentChapterInfo()
    if not curChapterInfo then




        self.view.beltNode.gameObject:SetActive(false)
        self.view.pipeNode.gameObject:SetActive(false)
        self.view.decoLine.gameObject:SetActive(false)
        return
    end
    local typeInfos = {}

    local isInFacMainRegion = Utils.isInFacMainRegion()
    local isInSettlementDefenseDefending = Utils.isInSettlementDefenseDefending()

    local fcType = GEnums.FCQuickBarType.Inner
    local quickBarList = remoteFactoryCore.isTempQuickBarActive and
        remoteFactoryCore:GetCurrentTempQuickBar() or
        curChapterInfo:GetQuickBar(fcType)

    local needShowBelt = self.m_arg.showBelt and not isInSettlementDefenseDefending and isInFacMainRegion and GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedBelt
    self.view.beltNode.gameObject:SetActive(needShowBelt)
    local needShowPipe = self.m_arg.showPipe and not isInSettlementDefenseDefending and FactoryUtils.canShowPipe()
    self.view.pipeNode.gameObject:SetActive(needShowPipe)
    self.view.decoLine.gameObject:SetActive(needShowBelt or needShowPipe)

    local needShowBuildBtn = self.m_arg.showBuildBtn
        and (GameInstance.mode.modeType == GEnums.GameModeType.Default or Utils.isInBlackbox())
        and Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacHub)
    self.view.buildBtn.gameObject:SetActive(needShowBuildBtn)
    self.view.buildBtn.onClick:RemoveAllListeners()
    if needShowBuildBtn then
        self.view.buildBtn.onClick:AddListener(function()
            self:_OpenBuildList()
        end)
    end

    local typeData = Tables.factoryQuickBarTypeTable:GetValue("custom")
    local typeInfo = {
        data = typeData,
        priority = typeData.priority,
        fcType = fcType,
        items = {},
        canDrop = true,
    }
    for _, id in pairs(quickBarList) do
        local info = {
            itemId = id,
            isCustomQuickBarItem = true,
        }
        if not string.isEmpty(id) then
            local buildingData = FactoryUtils.getItemBuildingData(id)
            if buildingData then
                info.type = QuickBarItemType.Building
                info.onlyShowOnMain = buildingData.onlyShowOnMain
            else
                info.type = QuickBarItemType.Logistic
                if FacConst.FLUID_LOGISTIC_ITEMS[id] then
                    info.onlyShowOnMain = false
                else
                    info.onlyShowOnMain = true
                end
            end
        end
        table.insert(typeInfo.items, info)
    end
    table.insert(typeInfos, typeInfo)

    table.sort(typeInfos, Utils.genSortFunction({"priority"}))
    self.m_typeInfos = typeInfos
end






FacQuickBarCtrl.m_selectedTypeIndex = HL.Field(HL.Number) << 1
FacQuickBarCtrl._RefreshTypes = HL.Method() << function(self)
    if not self.m_typeInfos then
        return
    end
    local count = #self.m_typeInfos
    self.m_selectedTypeIndex = math.min(math.max(self.m_selectedTypeIndex, 1), count)
    self.m_typeCells:Refresh(count, function(cell, index)
        local info = self.m_typeInfos[index]

        local name = info.data.name
        cell.default.icon:LoadSprite(info.data.icon)
        cell.default.nameTxt.text = name
        cell.selected.icon:LoadSprite(info.data.icon)
        cell.selected.nameTxt.text = name

        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.isOn = index == self.m_selectedTypeIndex
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_OnClickType(index)
            end
        end)

        cell.gameObject.name = "TypeTabCell_" .. info.data.id
    end)
    self:_RefreshItemList()
end

FacQuickBarCtrl._OnClickType = HL.Method(HL.Number) << function(self, index)
    self.m_selectedTypeIndex = index
    self:_RefreshItemList()
end





FacQuickBarCtrl._RefreshItemList = HL.Method() << function(self)
    if self.m_selectedTypeIndex == 0 then
        self.m_itemCells:Refresh(0)
        return
    end

    local info = self.m_typeInfos[self.m_selectedTypeIndex]
    self.m_itemCells:Refresh(#info.items, function(cell, index)
        self:_UpdateCell(cell, index)
    end)
end

FacQuickBarCtrl._UpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local item = self.m_typeInfos[self.m_selectedTypeIndex].items[index]
    local itemId = item.itemId
    local isEmpty = string.isEmpty(itemId)
    local count
    local isBuilding = item.type == QuickBarItemType.Building
    if isBuilding then
        count = isEmpty and 0 or Utils.getItemCount(itemId)
    end


    local itemCell = cell.view.item
    itemCell.m_enableHoverTips = DeviceInfo.usingKeyboard or not self.m_useActiveAction
    cell:InitItemSlot({
        id = itemId,
        count = count,
    }, function()
        if DeviceInfo.usingController then
            if not self.m_useActiveAction then
                if string.isEmpty(self.m_curSettingBuildingItemId) then

                    itemCell:ShowActionMenu(false, UIConst.UI_TIPS_POS_TYPE.RightDown)
                else
                    self:_OnClickItem(index)
                end
            end
        else
            self:_OnClickItem(index)
        end
    end, nil, true)
    if itemCell.customShowTipsFunc == nil then
        itemCell.customShowTipsFunc = function()
            if not string.isEmpty(self.m_curSettingBuildingItemId) then
                self.view.setBuildingHint.gameObject:SetActive(false)
            end
        end
        itemCell.customHideTipsFunc = function()
            if self.m_isClosed or IsNull(self.view.main) then
                return
            end
            if not string.isEmpty(self.m_curSettingBuildingItemId) then
                self.view.setBuildingHint.gameObject:SetActive(true)
            end
            if self.m_useActiveAction then
                local keyCode = InputManagerInst:GetActionGamepadKeyCode("fac_activate_quick_bar", false, true);
                if not InputManagerInst:GetKey(keyCode) then
                    self.view.main:ManuallyStopFocus()
                end
            end
        end
    end
    if string.isEmpty(self.m_curSettingBuildingItemId) then
        itemCell:AddHoverBinding("fac_quick_bar_controller_build", function()
            self:_OnClickItem(index)
        end)
    end

    if cell.view.setBuildingHint then

        if string.isEmpty(self.m_curSettingBuildingItemId) and self.m_switchSlotFromIndex < 0 then
            cell.view.setBuildingHint.gameObject:SetActive(false)
        else
            cell.view.setBuildingHint.gameObject:SetActive(itemCell.view.button.isNaviTarget)
            itemCell.view.button.onHoverChange:AddListener(function(isHover)
                cell.view.setBuildingHint.gameObject:SetActive(isHover and not (string.isEmpty(self.m_curSettingBuildingItemId) and self.m_switchSlotFromIndex < 0))
            end)
        end
    end

    itemCell.canPlace = true
    if isEmpty or count == 0 then
        itemCell.canPlace = false
    end
    itemCell.actionMenuArgs = {
        source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar,
        canSwitch = self.m_arg.controllerSwitchArgs ~= nil,
        fromIndex = index,
    }

    cell.item.view.button.onDoubleClick:RemoveAllListeners()
    if isBuilding then
        local isSkipBuilding = FactoryUtils.isSkipBuildingInvalidInDomainByItemId(itemId)
        local hasCraft = Tables.FactoryItemAsHubCraftOutcomeTable:TryGetValue(itemId)
        if isSkipBuilding then

            cell.item.view.button.onDoubleClick:AddListener(function()
                if count == 0 then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_SKIP_BUILDING_CANNOT_CRAFT_IN_DOMAIN)
                end
            end)
        elseif hasCraft then
            cell.item.view.button.onDoubleClick:AddListener(function()
                if count == 0 then
                    Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { selectedId = itemId })
                end
            end)
        elseif FactoryUtils.isDecoBuildingItem(itemId) then
            cell.item.view.button.onDoubleClick:AddListener(function()
                if count == 0 then
                    Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { selectedId = itemId })
                end
            end)
        end
    end

    cell.item.view.nameNode.gameObject:SetActive(false)
    if cell.view.hoverKeyHint then
        cell.view.hoverKeyHint.gameObject:SetActive(false)
    end

    cell.item.view.button.onIsNaviTargetChanged = function(active)
        if active then
            self.m_currentNaviIndex = index
            self:_RefreshCellHoverBinding(cell, index)
        end


        local showHoverHint = self.m_useActiveAction and active
        cell.item.view.nameNode.gameObject:SetActive(showHoverHint)
        if cell.view.hoverKeyHint then
            cell.view.hoverKeyHint.gameObject:SetActive(showHoverHint)
        end

        if cell.view.setBuildingHint then

            local inSetting = not string.isEmpty(self.m_curSettingBuildingItemId) or self.m_switchSlotFromIndex > 0
            cell.view.setBuildingHint.gameObject:SetActive(inSetting and active)
        end
    end
    self:_RefreshCellHoverBinding(cell, index)

    local actionId = "fac_use_quick_item_" .. index
    cell.item.view.button.onClick:ChangeBindingPlayerAction(actionId)

    cell.gameObject.name = "Item_" .. (isEmpty and index or itemId)

    if not isEmpty then
        local data = Tables.itemTable:GetValue(itemId)
        if item.isCustomQuickBarItem then
            UIUtils.initUIDragHelper(cell.view.dragItem, {
                source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar,
                type = data.type,
                csIndex = CSIndex(index),
                itemId = itemId,
                onEndDrag = function(enterObj, enterDrop, eventData)
                    self:_OnQuickBarEndDrag(index, enterObj, enterDrop, eventData)
                end,
                onDropTargetChanged = function(enterObj, dropHelper)
                    local dragObj = cell.view.dragItem.curDragObj
                    if not dragObj then
                        return
                    end
                    local dragItem = dragObj:GetComponent("LuaUIWidget").table[1]
                    if not dropHelper or not dropHelper.info.isQuickBarClearDropZone then
                        dragItem.view.clearNode.gameObject:SetActive(false)
                        return
                    end
                    dragItem.view.clearNode.gameObject:SetActive(true)
                end,
            })
        else
            UIUtils.initUIDragHelper(cell.view.dragItem, {
                source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar,
                type = data.type,
                itemId = itemId,
                onEndDrag = function(enterObj, enterDrop, eventData)
                    self:_OnQuickBarEndDrag(index, enterObj, enterDrop, eventData)
                end
            })
        end

        if DeviceInfo.usingController then
            cell:InitPressDrag()
        else
            cell.item:OpenLongPressTips()
        end
    end
    if item.isCustomQuickBarItem then
        cell.view.dropItem.enabled = true
        UIUtils.initUIDropHelper(cell.view.dropItem, {
            acceptTypes = UIConst.FACTORY_QUICK_BAR_DROP_ACCEPT_INFO,
            onDropItem = function(eventData, dragHelper)
                self:_OnDropItem(index, dragHelper)
            end,
        })
    else
        cell.view.dropItem.enabled = false
    end

    if DeviceInfo.usingController then
        cell.item:SetExtraInfo({
            tipsPosTransform = cell.view.controllerTipsRect,
        })
    end

    cell.item.view.nameNode.gameObject:SetActive(false)
end

FacQuickBarCtrl._RefreshCellHoverBinding = HL.Method(HL.Forward("ItemSlot"), HL.Number) << function(self, cell, index)
    local button = cell.item.view.button
    local item = self.m_typeInfos[self.m_selectedTypeIndex].items[index]
    local itemId = item.itemId
    local isEmpty = string.isEmpty(itemId)
    local count
    if item.type == QuickBarItemType.Building then
        count = isEmpty and 0 or Utils.getItemCount(itemId)
    end

    if not string.isEmpty(self.m_curSettingBuildingItemId) then
        InputManagerInst:ToggleBinding(button.hoverConfirmBindingId, false)
        InputManagerInst:SetBindingText(button.hoverConfirmBindingId, nil)
        return
    end

    if self.m_arg.controllerSwitchArgs ~= nil then
        if self.m_switchSlotFromIndex > 0 or isEmpty then
            InputManagerInst:ToggleBinding(button.hoverConfirmBindingId, false)
        end
        InputManagerInst:SetBindingText(button.hoverConfirmBindingId, Language["key_hint_item_open_action_menu"])
        return
    end

    if isEmpty or count == 0 then
        InputManagerInst:ToggleBinding(button.hoverConfirmBindingId, false)
        return
    end

    InputManagerInst:SetBindingText(button.hoverConfirmBindingId, Language.LUA_ITEM_ACTION_PLACE)
end

FacQuickBarCtrl._OnClickItem = HL.Method(HL.Number, HL.Opt(Vector2)) << function(self, index, mousePosition)
    self:_BuildItem(index, mousePosition)
end

FacQuickBarCtrl._BuildItem = HL.Method(HL.Number, HL.Opt(Vector2)) << function(self, index, mousePosition)
    local item = self.m_typeInfos[self.m_selectedTypeIndex].items[index]
    if item == nil or string.isEmpty(item.itemId) then
        return
    end

    local isBuilding = item.type == QuickBarItemType.Building
    if item.onlyShowOnMain then
        if item.type ~= GEnums.FacBuildingType.Hub:GetHashCode() and not Utils.isInFacMainRegion() then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_CANT_BUILD_IN_NOT_MAIN_REGION)
            return
        end
    end

    if isBuilding then
        local itemId = item.itemId
        local count = Utils.getItemCount(itemId)
        if count > 0 then
            local args = {
                itemId = itemId,
                initMousePos = mousePosition,
            }
            Notify(MessageConst.FAC_ENTER_BUILDING_MODE, args)
            return
        else
            if FactoryUtils.isSkipUnlockedBuildingByItemId(itemId) then

                Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_NO_JUMP)
            else
                local hasCraft = Tables.FactoryItemAsHubCraftOutcomeTable:TryGetValue(itemId)
                local showJumpToast = hasCraft and not DeviceInfo.usingController
                local isDecoBuilding = FactoryUtils.isDecoBuildingItem(itemId)
                local text = DeviceInfo.usingTouch and Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_TOUCH or Language.LUA_FAC_QUICK_BAR_COUNT_ZERO
                if isDecoBuilding then
                    showJumpToast = not DeviceInfo.usingController
                    text = DeviceInfo.usingTouch and Language.LUA_FAC_QUICK_BAR_DECO_COUNT_ZERO_TOUCH or Language.LUA_FAC_QUICK_BAR_DECO_COUNT_ZERO
                end
                Notify(MessageConst.SHOW_TOAST, showJumpToast and text or Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_NO_JUMP)
            end
            return
        end
    else
        if item.type == QuickBarItemType.Belt then
            Notify(MessageConst.FAC_ENTER_BELT_MODE, {beltId = item.id})
        elseif item.type == QuickBarItemType.Logistic then
            Notify(MessageConst.FAC_ENTER_LOGISTIC_MODE, {itemId = item.itemId})
        end
    end
end





FacQuickBarCtrl._CanDrop = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_typeInfos[self.m_selectedTypeIndex].canDrop == true
end

FacQuickBarCtrl.OnOtherStartDragItem = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if not self:IsShow() then
        return
    end
    if not self:_CanDrop() then
        return
    end
    if not dragHelper.cantDrop and UIUtils.isTypeDropValid(dragHelper, UIConst.FACTORY_QUICK_BAR_DROP_ACCEPT_INFO) then
        if dragHelper.source ~= UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar then
            self.view.dropHint.gameObject:SetActive(true)
            self.view.dropHint.transform:SetAsLastSibling()
        end
    else
        self.view.cantDropHint.gameObject:SetActive(true)
        if string.isEmpty(dragHelper.cantDropText) then
            self.view.cantDropText.text = Language["ui_fac_quick_bar_notport"]
        else
            self.view.cantDropText.text = dragHelper.cantDropText
        end
        self.view.cantDropHint.transform:SetAsLastSibling()
    end
    self.view.beltNode.notDropHint.gameObject:SetActive(true)
    self.view.pipeNode.notDropHint.gameObject:SetActive(true)
end

FacQuickBarCtrl.OnOtherEndDragItem = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if not dragHelper.cantDrop and UIUtils.isTypeDropValid(dragHelper, UIConst.FACTORY_QUICK_BAR_DROP_ACCEPT_INFO) then
        self.view.dropHint.gameObject:SetActive(false)
    else
        self.view.cantDropHint.gameObject:SetActive(false)
    end
    self.view.beltNode.notDropHint.gameObject:SetActive(false)
    self.view.pipeNode.notDropHint.gameObject:SetActive(false)
end

FacQuickBarCtrl._OnDropItem = HL.Method(HL.Number, HL.Forward('UIDragHelper')) << function(self, index, dragHelper)
    local csIndex = CSIndex(index)
    local source = dragHelper.source
    local fcType = self.m_typeInfos[self.m_selectedTypeIndex].fcType
    local remoteFactoryCore = GameInstance.player.remoteFactory.core
    if source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar then

        local fromCSIndex = dragHelper.info.csIndex
        if remoteFactoryCore.isTempQuickBarActive then
            remoteFactoryCore:SwitchTempQuickBarItem(fromCSIndex, csIndex)
        else
            GameInstance.player.remoteFactory:SendMoveQuickBar(fcType, 0, fromCSIndex, csIndex)
        end

        UIUtils.playItemDropAudio(dragHelper:GetId())
        AudioAdapter.PostEvent("au_ui_common_put_down")
    elseif not dragHelper.cantDrop then

        local itemId = dragHelper:GetId()
        if remoteFactoryCore.isTempQuickBarActive then
            remoteFactoryCore:MoveItemToTempQuickBar(itemId, csIndex)
        else
            GameInstance.player.remoteFactory:SendSetQuickBar(fcType, 0, csIndex, itemId)
        end

        UIUtils.playItemDropAudio(dragHelper:GetId())
        AudioAdapter.PostEvent("au_ui_common_put_down")
    end
end

FacQuickBarCtrl.OnQuickBarChanged = HL.Method() << function(self)
    if not self.m_arg then
        return
    end
    self:_InitDataInfo()
    self:_RefreshTypes()
end

FacQuickBarCtrl._OnQuickBarEndDrag = HL.Method(HL.Number, HL.Opt(HL.Userdata, HL.Forward('UIDropHelper'), HL.Any)) << function(self, index, enterObj, enterDrop, eventData)
    if not eventData then
        return
    end
    if enterDrop then
        return
    end
    if enterObj ~= UIManager.commonTouchPanel.gameObject then
        return
    end

    self:_OnClickItem(index, eventData.position)
end





FacQuickBarCtrl.m_arg = HL.Field(HL.Table)

FacQuickBarCtrl.ShowFacQuickBar = HL.StaticMethod(HL.Table) << function(arg)
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacMode) then
        return
    end
    local self = FacQuickBarCtrl.AutoOpen(PANEL_ID, nil, true)



    if DeviceInfo.usingController and self.m_lastAttachPanelId > 0 and self.m_lastAttachPanelId ~= arg.panelId then
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.main)
    end
    self.m_lastAttachPanelId = arg.panelId

    self.m_arg = arg
    self.m_useActiveAction = arg.useActiveAction == true
    self:_AttachToPanel(arg)
    self:_RefreshContent()
    self:_RefreshControllerSettingOnShow()
end

FacQuickBarCtrl.HideFacQuickBar = HL.Method(HL.Number) << function(self, panelId)
    self:_CustomHide(panelId)
end

FacQuickBarCtrl.CustomSetPanelOrder = HL.Override(HL.Opt(HL.Number, HL.Table)) << function(self, maxOrder, args)
    self.m_curArgs = args
    self:SetSortingOrder(maxOrder, false)
    self:UpdateInputGroupState()
end





FacQuickBarCtrl.OnSetInSafeZone = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    if self:IsHide() then
        return
    end
    self:_InitDataInfo()
    self:_RefreshItemList()
end

FacQuickBarCtrl.OnItemCountChanged = HL.Method(HL.Table) << function(self, args)
    if not self.m_typeInfos then
        return
    end
    if self:IsHide() then
        return
    end
    local itemId2DiffCount = unpack(args)
    local items = self.m_typeInfos[self.m_selectedTypeIndex].items
    for k, v in ipairs(items) do
        if itemId2DiffCount:ContainsKey(v.itemId) then
            local cell = self.m_itemCells:GetItem(k)
            local count = Utils.getItemCount(v.itemId)
            cell.item:UpdateCount(count)
        end
    end
end

FacQuickBarCtrl._OnQuickDropItemToQuickBar = HL.Method(HL.Any) << function(self, itemId)
    if type(itemId) == "table" then
        itemId = unpack(itemId)
    end
    local info = self.m_typeInfos[self.m_selectedTypeIndex]
    local fcType = info.fcType
    local targetIndex
    for index, barItemData in ipairs(info.items) do
        local barItemId = barItemData.itemId
        if barItemId == itemId then
            return
        end
        if targetIndex == nil and string.isEmpty(barItemId) then
            targetIndex = index
        end
    end
    if targetIndex == nil then
        return
    end

    local success, itemData = Tables.itemTable:TryGetValue(itemId)
    if not success then
        return
    end
    local findType = false
    for _, type in ipairs(UIConst.FACTORY_QUICK_BAR_DROP_ACCEPT_INFO.types) do
        if type == itemData.type then
            findType = true
            break
        end
    end
    if not findType then
        return
    end

    local remoteFactoryCore = GameInstance.player.remoteFactory.core
    local csIndex = CSIndex(targetIndex)
    if remoteFactoryCore.isTempQuickBarActive then
        remoteFactoryCore:MoveItemToTempQuickBar(itemId, csIndex)
    else
        GameInstance.player.remoteFactory:SendSetQuickBar(fcType, 0, csIndex, itemId)
    end
end

FacQuickBarCtrl._OnNewScopeInfoReceived = HL.Method(HL.Any) << function(self, scopeName)
    self:_RefreshContent()
end






FacQuickBarCtrl.OnBeltUnlocked = HL.Method() << function(self)
    if self:IsShow() then
        self:_InitDataInfo()
        self:_RefreshTypes()
    end
end

FacQuickBarCtrl._InitBeltNode = HL.Method() << function(self)
    self:_InitLogisticNode(self.view.beltNode, FacConst.BELT_ITEM_ID, function()
        self:_OnClickBelt()
    end)
end

FacQuickBarCtrl._OnClickBelt = HL.Method() << function(self)
    Notify(MessageConst.FAC_ENTER_BELT_MODE, { beltId = FacConst.BELT_ID })
end






FacQuickBarCtrl.OnPipeUnlocked = HL.Method() << function(self)
    if self:IsShow() then
        self:_InitDataInfo()
        self:_RefreshTypes()
    end
end

FacQuickBarCtrl._InitPipeNode = HL.Method() << function(self)
    self:_InitLogisticNode(self.view.pipeNode, FacConst.PIPE_ITEM_ID, function()
        self:_OnClickPipe()
    end)
end

FacQuickBarCtrl._OnClickPipe = HL.Method() << function(self)
    Notify(MessageConst.FAC_ENTER_BELT_MODE, { beltId = FacConst.PIPE_ID })
end

FacQuickBarCtrl._InitLogisticNode = HL.Method(HL.Table, HL.String, HL.Function) << function(self, node, itemId, onClick)
    node.notDropHint.gameObject:SetActive(false)

    local item = node.item
    item:SetEnableHoverTips(DeviceInfo.usingKeyboard)
    item:InitItem({ id = itemId }, onClick)
    item:AddHoverBinding("fac_quick_bar_controller_build", onClick)
    item:OpenLongPressTips()
    item.canPlace = true
    item.actionMenuArgs = {
        source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar,
    }

    if node.itemDragHandler then
        node.itemDragHandler.onDrag:AddListener(function(eventData)
            Notify(MessageConst.MOVE_LEVEL_CAMERA, eventData.delta)
        end)
    end

    local btn = item.view.button
    btn.onHoverChange:AddListener(function(isHover)
        if DeviceInfo.usingController then
            node.keyHint.gameObject:SetActive(not isHover)
            item.view.nameNode.gameObject:SetActive(isHover)
            node.hoverKeyHint.gameObject:SetActive(isHover)
        end
    end)
    item.view.nameNode.gameObject:SetActive(false)
    if node.hoverKeyHint then
        node.hoverKeyHint.gameObject:SetActive(false)
    end
end






FacQuickBarCtrl.m_setBuildingBindingGroupId = HL.Field(HL.Number) << -1

FacQuickBarCtrl.m_isSetSlot = HL.Field(HL.Boolean) << false

FacQuickBarCtrl._InitSetBindings = HL.Method() << function(self)
    self.m_setBuildingBindingGroupId = InputManagerInst:CreateGroup(self.view.mainInputBindingGroupMonoTarget.groupId)
    self:BindInputPlayerAction("common_confirm", function()
        self:_OnConfirmSetInController()
    end, self.m_setBuildingBindingGroupId)
    self:BindInputPlayerAction("common_back", function()
        self:_OnCancelSetInController()
    end, self.m_setBuildingBindingGroupId)
    InputManagerInst:ToggleGroup(self.m_setBuildingBindingGroupId, false)
end

FacQuickBarCtrl._OnConfirmSetInController = HL.Method() << function(self)
    AudioAdapter.PostEvent("Au_UI_Item_Put_Producer")
    if self.m_isSetSlot then
        self:_OnConfirmSwitchSlot()
    else
        self:_SetBuildingOnFacQuickBar()
    end
end

FacQuickBarCtrl._OnCancelSetInController = HL.Method() << function(self)
    if self.m_isSetSlot then
        self:_OnCancelSwitchSlot()
    else
        self:_ExitSetBuilding()
    end
end

FacQuickBarCtrl._OnEnterSetModeInController = HL.Method(HL.Table) << function(self, args)
    UIUtils.changeAndTrySetNaviBindingType(self.view.main, CS.UnityEngine.UI.NavigationBindingType.HorizontalOnly)
    InputManagerInst:ToggleGroup(self.m_setBuildingBindingGroupId, true)
    InputManagerInst:ToggleGroup(self.view.buildBtnInputBindingGroupMonoTarget.groupId, false)

    Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
        panelId = PANEL_ID,
        isGroup = true,
        id = self.view.mainInputBindingGroupMonoTarget.groupId,
        hintPlaceholder = self.m_arg.controllerSwitchArgs.hintPlaceholder,
        rectTransform = self.view.rectTransform,
        noHighlight = true,
    })

    self:_RefreshContent()
end

FacQuickBarCtrl._OnExitSetModeInController = HL.Method(HL.Boolean) << function(self, removeLayer)
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.mainInputBindingGroupMonoTarget.groupId)
    if removeLayer then
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.naviGroup)
    end
    InputManagerInst:ToggleGroup(self.m_setBuildingBindingGroupId, false)
    InputManagerInst:ToggleGroup(self.view.buildBtnInputBindingGroupMonoTarget.groupId, true)
    UIUtils.changeAndTrySetNaviBindingType(self.view.main, CS.UnityEngine.UI.NavigationBindingType.AllDirections)
end

FacQuickBarCtrl._OnDefaultNaviFailed = HL.Method(CS.UnityEngine.UI.NaviDirection) << function(self, dir)
    if not string.isEmpty(self.m_curSettingBuildingItemId) then
        return
    end
    if dir == Unity.UI.NaviDirection.Up and not self.m_useActiveAction then
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.naviGroup)
    end
end

FacQuickBarCtrl.m_curSettingBuildingItemId = HL.Field(HL.String) << ''

FacQuickBarCtrl.StartSetBuildingOnFacQuickBar = HL.Method(HL.Table) << function(self, args)
    local itemId = args.itemId
    self.m_curSettingBuildingItemId = itemId
    self.view.setBuildingHint.gameObject:SetActive(true)
    self.m_isSetSlot = false

    self:_OnEnterSetModeInController(args)

    local curInfo = self.m_typeInfos[self.m_selectedTypeIndex]
    for k, v in ipairs(curInfo.items) do
        if v.itemId == itemId then
            local cell = self.m_itemCells:Get(k)
            cell:SetAsNaviTarget()
            return
        end
    end
    local cell = self.m_itemCells:Get(1)
    cell:SetAsNaviTarget()
end

FacQuickBarCtrl._SetBuildingOnFacQuickBar = HL.Method() << function(self)
    if string.isEmpty(self.m_curSettingBuildingItemId) then




        return
    end
    local fcType = self.m_typeInfos[self.m_selectedTypeIndex].fcType
    GameInstance.player.remoteFactory:SendSetQuickBar(fcType, 0, CSIndex(self.m_currentNaviIndex), self.m_curSettingBuildingItemId)
    self:_ExitSetBuilding()
end

FacQuickBarCtrl._ExitSetBuilding = HL.Method() << function(self)
    self:_OnExitSetModeInController(true)

    self.m_curSettingBuildingItemId = ""
    self.view.setBuildingHint.gameObject:SetActive(false)
    self:_RefreshContent()
end

FacQuickBarCtrl.m_switchSlotFromIndex = HL.Field(HL.Number) << -1

FacQuickBarCtrl.StartSwitchSlotOnFacQuickBar = HL.Method(HL.Table) << function(self, args)
    self.m_isSetSlot = true
    self.m_switchSlotFromIndex = args.fromIndex

    self:_OnEnterSetModeInController(args)


    local fromCell = self.m_itemCells:GetItem(self.m_switchSlotFromIndex)
    fromCell.item.view.button.enabled = false
    fromCell.item:SetSelected(true)

    local initNaviCell = self.m_itemCells:GetItem(self.m_switchSlotFromIndex ~= 1 and 1 or 2)
    initNaviCell.item:SetAsNaviTarget()
end

FacQuickBarCtrl._ExitSwitchSlot = HL.Method(HL.Boolean) << function(self, isConfirm)
    self:_OnExitSetModeInController(false)

    local fromCell = self.m_itemCells:GetItem(self.m_switchSlotFromIndex)
    self.m_switchSlotFromIndex = -1

    fromCell.item.view.button.enabled = true
    fromCell.item:SetSelected(false)
    if isConfirm then
        local currCell = self.m_itemCells:GetItem(self.m_currentNaviIndex)
        InputManagerInst:ToggleBinding(currCell.item.view.button.hoverConfirmBindingId, true)
    else
        self:SetNaviTarget(fromCell.item.view.button)
        InputManagerInst:ToggleBinding(fromCell.item.view.button.hoverConfirmBindingId, true)
    end
end

FacQuickBarCtrl._OnConfirmSwitchSlot = HL.Method() << function(self)
    local fcType = self.m_typeInfos[self.m_selectedTypeIndex].fcType
    GameInstance.player.remoteFactory:SendMoveQuickBar(fcType, 0, CSIndex(self.m_switchSlotFromIndex), CSIndex(self.m_currentNaviIndex))
    self:_ExitSwitchSlot(true)
end

FacQuickBarCtrl._OnCancelSwitchSlot = HL.Method() << function(self)
    self:_ExitSwitchSlot(false)
end






FacQuickBarCtrl.m_manualFocusBindingId = HL.Field(HL.Number) << -1

FacQuickBarCtrl.m_manualUnFocusBindingId = HL.Field(HL.Number) << -1

FacQuickBarCtrl.m_currentNaviIndex = HL.Field(HL.Number) << -1

FacQuickBarCtrl.m_zoomCamGroupId = HL.Field(HL.Number) << 0

FacQuickBarCtrl.m_lastCellsActionOnSetNaviTarget = HL.Field(HL.Table)

FacQuickBarCtrl.m_lastAttachPanelId = HL.Field(HL.Number) << -1

FacQuickBarCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.main.onIsTopLayerChanged:AddListener(function(isTopLayer)
        self:_OnIsTopLayerChanged(isTopLayer)
    end)
    self.view.main.getDefaultSelectableFunc = function()
        return self.m_itemCells:Get(1).item.view.button
    end
    self.view.main.onDefaultNaviFailed:AddListener(function(dir)
        self:_OnDefaultNaviFailed(dir)
    end)
    self.view.main.focusPanelSortingOrder = UIManager:GetBaseOrder(Types.EPanelOrderTypes.PopUp) - 1

    self.m_manualFocusBindingId = self:BindInputPlayerAction("fac_activate_quick_bar", function()
        self:_ToggleCellActionOnSetNaviTarget(false)
        local curTarget = self.view.main.lastFocusNaviTarget
        if curTarget and not curTarget.gameObject.activeInHierarchy then
            self.view.main:ClearLastFocusNaviTarget()
        end
        self.view.main:ManuallyFocus()
    end, self.view.mainInputBindingGroupMonoTarget.groupId)
    self.m_manualUnFocusBindingId = self:BindInputPlayerAction("fac_deactivate_quick_bar", function()
        self:_ToggleCellActionOnSetNaviTarget(true)
        self.view.main:ManuallyStopFocus()
    end, self.view.mainInputBindingGroupMonoTarget.groupId)

    self.m_zoomCamGroupId = InputManagerInst:CreateGroup(self.view.mainInputBindingGroupMonoTarget.groupId)
    UIUtils.bindControllerCamZoom(self.m_zoomCamGroupId)
    InputManagerInst:ToggleGroup(self.m_zoomCamGroupId, false)

    self:_InitSetBindings()
end

FacQuickBarCtrl._ToggleCellActionOnSetNaviTarget = HL.Method(HL.Boolean) << function(self, active)
    if not active or self.m_lastCellsActionOnSetNaviTarget == nil or next(self.m_lastCellsActionOnSetNaviTarget) == nil then
        self.m_lastCellsActionOnSetNaviTarget = {}
        for index = 1, self.m_itemCells:GetCount() do
            local cell = self.m_itemCells:GetItem(index)
            self.m_lastCellsActionOnSetNaviTarget[index] = cell.item.view.button.actionOnSetNaviTarget
        end
    end

    for index = 1, self.m_itemCells:GetCount() do
        local cell = self.m_itemCells:GetItem(index)
        local action = active and self.m_lastCellsActionOnSetNaviTarget[index] or CS.Beyond.Input.ActionOnSetNaviTarget.None
        cell.item.view.button:ChangeActionOnSetNaviTarget(action)
    end
end

FacQuickBarCtrl._OnIsTopLayerChanged = HL.Method(HL.Boolean) << function(self, isTopLayer)
    InputManagerInst:ToggleGroup(self.m_zoomCamGroupId, isTopLayer and self.m_useActiveAction)
    if not self.m_useActiveAction then
        return
    end

    if isTopLayer then


        local keyCode = InputManagerInst:GetActionGamepadKeyCode("fac_activate_quick_bar", false, true);
        if not InputManagerInst:GetKey(keyCode) then
            self:_StartTimer(0, function()
                self.view.main:ManuallyStopFocus()
            end)
            return
        end
    end

    self.view.barNodeSimpleActiveAnimationHelper.isActive = isTopLayer
    self.view.activeKeyHint.gameObject:SetActive(not isTopLayer)
    Notify(MessageConst.TOGGLE_HIDE_INTERACT_OPTION_LIST, { "FacQuickBar", isTopLayer })
    local isOpen, jsPanelCtrl = UIManager:IsOpen(PanelId.Joystick)
    if isOpen then

        local jsGroupId = jsPanelCtrl.view.joystick.groupId
        if isTopLayer then
            InputManagerInst:ChangeParent(true, jsGroupId, self.view.mainInputBindingGroupMonoTarget.groupId)
        else
            InputManagerInst:ChangeParent(true, jsGroupId, jsPanelCtrl.view.inputGroup.groupId)
        end
    end
end

FacQuickBarCtrl._RefreshControllerSettingOnShow = HL.Method() << function(self)
    if DeviceInfo.usingTouch then
        return
    end
    local useActiveAction = DeviceInfo.usingController and self.m_useActiveAction
    self.view.barNodeSimpleActiveAnimationHelper.enabled = useActiveAction
    if not useActiveAction then
        self.view.barNodeAnimation:Play("facquickbar_default")
    end
    self.view.activeKeyHint.gameObject:SetActive(useActiveAction)
    InputManagerInst:ToggleBinding(self.m_manualFocusBindingId, useActiveAction)
    InputManagerInst:ToggleBinding(self.m_manualUnFocusBindingId, useActiveAction)
    local naviType = useActiveAction and CS.UnityEngine.UI.NavigationBindingType.RightJsHorizontalOnly or CS.UnityEngine.UI.NavigationBindingType.AllDirections
    UIUtils.changeAndTrySetNaviBindingType(self.view.main, naviType)
end

FacQuickBarCtrl.ToggleCanDeactiveQuickBar = HL.Method(HL.Table) << function(self, args)
    local canDeactive = unpack(args)
    if not self.m_useActiveAction then
        return
    end
    InputManagerInst:ToggleBinding(self.m_manualUnFocusBindingId, canDeactive)
    if canDeactive then
        self.view.main:ManuallyStopFocus()
    end
end

FacQuickBarCtrl.NaviToFacQuickBar = HL.Method() << function(self)
    if not self:IsShow() then
        return
    end
    local cell = self.m_itemCells:Get(1)
    cell:SetAsNaviTarget()
end



FacQuickBarCtrl.OnChangeSpaceshipDomainId = HL.Method(HL.Any) << function(self, _)
    self:OnQuickBarChanged()
end

FacQuickBarCtrl._OpenBuildList = HL.Method() << function(self)
    Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, {showLastType = true})
end

HL.Commit(FacQuickBarCtrl)
