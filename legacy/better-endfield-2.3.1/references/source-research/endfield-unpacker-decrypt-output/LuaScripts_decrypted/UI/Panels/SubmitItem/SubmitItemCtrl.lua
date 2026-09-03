local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SubmitItem
local PHASE_ID = PhaseId.SubmitItem
SubmitItemCtrl = HL.Class('SubmitItemCtrl', uiCtrl.UICtrl)

local SubmitType = {
    Common = 0,
    CommonAndInst = 1,
    AnyAndInst = 2,
    AnyAndNoInst = 3,
}







SubmitItemCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SUBMIT_ITEM] = 'OnSubmitItem',
    [MessageConst.ON_SUBMIT_ITEM_FAILED] = 'OnSubmitItemFailed',
}

SubmitItemCtrl.m_submitItemsNormal = HL.Field(HL.Table)

SubmitItemCtrl.m_submitItemsSelect = HL.Field(HL.Table)

SubmitItemCtrl.m_selectItemBundle = HL.Field(HL.Table)

SubmitItemCtrl.m_controllerSelectCache = HL.Field(HL.Table)

SubmitItemCtrl.m_controllerToggleSelectBindingId = HL.Field(HL.Number) << -1

SubmitItemCtrl.m_info = HL.Field(HL.Table)

SubmitItemCtrl.m_normalItemListCache = HL.Field(HL.Forward("UIListCache"))

SubmitItemCtrl.m_selectItemListCache = HL.Field(HL.Forward("UIListCache"))

SubmitItemCtrl.m_submitType = HL.Field(HL.Number) << SubmitType.Common

SubmitItemCtrl.m_waitingClosePanel = HL.Field(HL.Boolean) << false

SubmitItemCtrl.m_isSubmitting = HL.Field(HL.Boolean) << false


SubmitItemCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:ClosePanel(1)
    end)
    self.view.btnSubmit.onClick:AddListener(function()
        self:_OnClickSubmit()
    end)

    self.view.selectListMask.onClick:AddListener(function()
        self:_CloseItemSelectList()
    end)

    self.view.selectListMask.gameObject:SetActiveIfNecessary(false)
    self.view.left.gameObject:SetActiveIfNecessary(false)

    local submitId = arg.submitId
    local questId = arg.questId
    local objId = arg.objId
    if not questId then
        questId = ""
    end
    if not objId then
        objId = 0
    end
    self.m_info = {
        submitId = submitId,
        questId = questId,
        objId = objId,
        fromDialog = arg.fromDialog,
        actionData = arg.actionData
    }

    local data = Tables.submitItem[submitId]
    if data.name ~= nil and data.name ~= "" then
        self.view.title.text = data.name
    else
        self.view.title.text = Language[self.view.config.TITLE_TEXT_ID]
    end
    if data.icon ~= nil and data.icon ~= "" then
        self.view.icon:LoadSprite(data.icon)
    else
        self.view.icon:LoadSprite(self.view.config.TITLE_ICON_PATH)
    end

    self.m_submitItemsNormal = {}
    self.m_submitItemsSelect = {}
    self.m_controllerSelectCache = {}
    local itemData = {
        id = "",
        count = 0,
        instId = 0
    }
    self.m_selectItemBundle = {
        itemData,
    }
    for _, v in pairs(data.paramData) do
        if v.type == GEnums.SubmitTermType.Common then
            local itemId = v.paramList[0].valueStringList[0]
            if Utils.isItemInstType(itemId) then
                self.m_submitType = SubmitType.CommonAndInst
            else
                self.m_submitType = SubmitType.Common
            end

            local needCount = v.paramList[1].valueIntList[0]
            local itemBundle = Tables.itemTable[itemId]
            if itemBundle ~= nil and Utils.isItemInstType(itemId) then
                if self.m_selectItemBundle[1].id == itemId then
                    self.m_selectItemBundle[1].count = self.m_selectItemBundle[1].count + needCount
                else
                    if self.m_selectItemBundle[1].id == "" then
                        self.m_selectItemBundle[1].id = itemId
                        self.m_selectItemBundle[1].count = needCount
                        self.m_selectItemBundle[1].name = itemBundle.name
                    end
                end
            else
                self.m_selectItemBundle = {}
                table.insert(self.m_submitItemsNormal, {
                    id = itemId,
                    count = v.paramList[1].valueIntList[0],
                })
            end
        elseif v.type == GEnums.SubmitTermType.AnyItem then
            local itemIds = v.paramList[0].valueStringList
            local needCount = v.paramList[1].valueIntList[0]
            self.m_selectItemBundle = {}
            for _, itemId in pairs(itemIds) do
                if Utils.isItemInstType(itemId) then
                    self.m_submitType = SubmitType.AnyAndInst
                else
                    self.m_submitType = SubmitType.AnyAndNoInst
                end
                local itemBundle = Tables.itemTable[itemId]
                table.insert(self.m_selectItemBundle, {
                    id = itemId,
                    count = needCount,
                    name = itemBundle.name,
                })
            end
        end

    end

    self.m_normalItemListCache = UIUtils.genCellCache(self.view.centerNormal.itemCellNormal)
    self.m_normalItemListCache:Refresh(#self.m_submitItemsNormal, function(cell, index)
        cell.item:InitItem(self.m_submitItemsNormal[index], true)
        cell.item:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.itemTips.transform,
            isSideTips = true,
        })
        cell.item:SetEnableHoverTips(not DeviceInfo.usingController)
    end)

    self.view.centerNormal.normalNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)

    self.view.leftNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            self:_CloseItemSelectList()
        end
    end)

    local totalCount = self:_GetSelectItemBundleCount()
    self.m_selectItemListCache = UIUtils.genCellCache(self.view.centerSelect.itemCellSelect)
    self.m_selectItemListCache:Refresh(totalCount, function(cell, _)
        cell.item:InitItem({})
        cell.add.onClick:RemoveAllListeners()
        cell.add.onClick:AddListener(function()
            self:_OpenItemSelectList()
        end)
    end)

    if totalCount > 0 then

        local bindingId = self:BindInputPlayerAction("submit_item_select_item", function()
            self:_OpenItemSelectList()
        end, self.view.right.groupId)
    end


    self.m_controllerToggleSelectBindingId = self:BindInputPlayerAction("submit_item_toggle_select", function()
        self:_OnControllerConfirmSelect()
    end, self.view.left.groupId)
    InputManagerInst:ToggleBinding(self.m_controllerToggleSelectBindingId, false)

    self:_UpdateSelectText()
    self:_UpdateCount()

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_UpdateCount()
        end
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

SubmitItemCtrl._GetSelectItemBundleCount = HL.Method().Return(HL.Number) << function(self)
    local totalCount = 0
    for _, itemData in pairs(self.m_selectItemBundle) do
        if totalCount == 0 then
            local itemId = itemData.id
            local isInst = Utils.isItemInstType(itemId)
            if isInst then
                totalCount = totalCount + itemData.count
            else

                totalCount = 1
                break
            end
        end
    end

    return totalCount
end

SubmitItemCtrl._OnClickSubmit = HL.Method() << function(self)
    if self.m_isSubmitting then
        return
    end
    self.m_isSubmitting = true

    local enoughNormal = true
    self.m_normalItemListCache:Update(function(cell, index)
        local bundle = self.m_submitItemsNormal[index]
        local count = SubmitItemCtrl._GetItemCount(bundle.id)
        local isLack = count < bundle.count
        cell.item:UpdateCountSimple(bundle.count, isLack)
        if isLack then
            enoughNormal = false
        end
    end)


    local enoughSelect = true
    self.m_selectItemListCache:Update(function(cell, index)
        local bundle = self.m_submitItemsSelect[index]
        if bundle == nil or bundle.instId == 0 then
            enoughSelect = false
        end
    end)

    if not enoughNormal or not enoughSelect then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SUBMIT_ITEM_INSUFFICENT_TIP)
        self.m_isSubmitting = false
        return
    end

    local selectInstIds, selectItemIds = nil, nil
    if self:_GetSelectItemBundleCount() ~= 0 then
        if self.m_submitType == SubmitType.AnyAndInst or self.m_submitType == SubmitType.CommonAndInst then
            selectInstIds = {}
            for _, v in pairs(self.m_submitItemsSelect) do
                table.insert(selectInstIds, v.instId)
            end
        elseif self.m_submitType == SubmitType.AnyAndNoInst then
            selectItemIds = {}
            for _, v in pairs(self.m_submitItemsSelect) do
                table.insert(selectItemIds, v.id)
            end
        end
    end

    local syncAndWaitSubmit = self:_Submit(selectInstIds, selectItemIds)
    if not syncAndWaitSubmit then
        self.m_isSubmitting = false
        self:ClosePanel(0)
    end
end

SubmitItemCtrl._Submit = HL.Method(HL.Table, HL.Table).Return(HL.Boolean) << function(self, selectInstIds, selectItemIds)
    local syncSubmitToServerImmediately = false
    if self.m_info.fromDialog then
        if self.m_info.actionData and self.m_info.actionData.syncSubmitToServerImmediately then

            syncSubmitToServerImmediately = true
        elseif GameWorld.dialogManager.isPlaying then

            GameWorld.dialogManager:RegisterPendingSubmission(CS.Beyond.Gameplay.InventoryItemSubmitter(
                Utils.getCurrentScope(),
                Utils.getCurrentChapterId(),
                self.m_info.submitId,
                self.m_info.questId,
                self.m_info.objId,
                selectInstIds,
                selectItemIds
            ))
            syncSubmitToServerImmediately = false
        else
            Notify(MessageConst.SHOW_TOAST, Language["ui_msc_submit_fail"])
            syncSubmitToServerImmediately = false
        end
    else

        syncSubmitToServerImmediately = true
    end

    if syncSubmitToServerImmediately then
        GameInstance.player.inventory:SubmitItem(
            Utils.getCurrentScope(),
            Utils.getCurrentChapterId(),
            self.m_info.submitId,
            self.m_info.questId,
            self.m_info.objId,
            selectInstIds,
            selectItemIds
        )
    end


    return syncSubmitToServerImmediately
end

SubmitItemCtrl._OpenItemSelectList = HL.Method() << function(self)
    if self.view.left.gameObject.activeSelf then
        return
    end
    self.view.left.gameObject:SetActiveIfNecessary(true)
    self.view.selectListMask.gameObject:SetActiveIfNecessary(true)

    local data = Tables.submitItem[self.m_info.submitId]
    local selectHintDes = data.selectHintDes
    local title
    if string.isEmpty(selectHintDes) then
        if self.m_submitType == SubmitType.AnyAndInst then
            title = Language.LUA_SUBMIT_COMMON_HINT
        elseif self.m_submitType == SubmitType.AnyAndNoInst then
            title = string.format(Language.LUA_SUBMIT_SELECT_HINT, self:_GetSelectItemBundleCount())
        else
            title = string.format(Language.LUA_ITEM_SELECT_LIST_TITLE_TEMPLATE, self.m_selectItemBundle[1].name)
        end
    else
        title = selectHintDes
    end

    local itemIds = {}
    for _, itemData in pairs(self.m_selectItemBundle) do
        table.insert(itemIds, itemData.id)
    end

    self.view.itemSelectList:InitItemSelectList(itemIds, self.m_submitItemsSelect, function(itemBundle, cell, index)
        self:_OnSelectItem(itemBundle, cell, index)
    end, title, function(itemId, instId)
        self:OnItemLockedStateChanged(itemId, instId)
    end)


    if DeviceInfo.usingController then
        if #self.view.itemSelectList.items > 0 then
            self.view.leftNaviGroup:ManuallyFocus();
        else
            local bindingId = self:BindInputPlayerAction("common_back", function()
                Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.left.groupId)
            end, self.view.left.groupId)

            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = PANEL_ID,
                isGroup = true,
                id = self.view.left.groupId,
                hintPlaceholder = self.view.controllerHintPlaceholder,
                rectTransform = self.view.left.transform,
                noHighlight = true,
                onClose = function()
                    InputManagerInst:DeleteBinding(bindingId)
                    self:_CloseItemSelectList()
                end,
            })
        end
    end
end

SubmitItemCtrl._CloseItemSelectList = HL.Method() << function(self)
    if not self.view.left.gameObject.activeSelf then
        return
    end

    if self.view.leftAnimationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then

        return
    end

    Notify(MessageConst.HIDE_ITEM_TIPS)
    self.view.selectListMask.gameObject:SetActiveIfNecessary(false)

    self.view.leftAnimationWrapper:PlayOutAnimation(function()
        self.view.left.gameObject:SetActiveIfNecessary(false)
    end)
end

SubmitItemCtrl._OnSelectItem = HL.Method(HL.Table, HL.Any, HL.Number) << function(self, itemBundle, cell, index)
    if DeviceInfo.usingController then
        self.m_controllerSelectCache = {itemBundle, cell, index}
        local keyHintText = Language["key_hint_submit_item_toggle_select_true"]
        if self:_GetItemSelectIndex(itemBundle) > 0 then
            keyHintText = Language["key_hint_submit_item_toggle_select_false"]
        end
        InputManagerInst:SetBindingText(self.m_controllerToggleSelectBindingId, keyHintText)
        InputManagerInst:ToggleBinding(self.m_controllerToggleSelectBindingId, true)
    else
        self:_RealSelectItem(itemBundle, cell, index)
    end
end

SubmitItemCtrl._OnControllerConfirmSelect = HL.Method() << function(self)
    if next(self.m_controllerSelectCache) ~= nil then
        self:_RealSelectItem(unpack(self.m_controllerSelectCache))
        AudioAdapter.PostEvent("Au_UI_Button_Item")
        local itemBundle = self.m_controllerSelectCache[1]
        local keyHintText = Language["key_hint_submit_item_toggle_select_true"]
        if self:_GetItemSelectIndex(itemBundle) > 0 then
            keyHintText = Language["key_hint_submit_item_toggle_select_false"]
        end
        InputManagerInst:SetBindingText(self.m_controllerToggleSelectBindingId, keyHintText)
    end
end

SubmitItemCtrl._GetItemSelectIndex = HL.Method(HL.Table).Return(HL.Number) << function(self, itemBundle)
    for i = #self.m_submitItemsSelect, 1, -1 do
        local bundle = self.m_submitItemsSelect[i]
        if self.m_submitType == SubmitType.AnyAndInst or self.m_submitType == SubmitType.CommonAndInst then
            if bundle.instId == itemBundle.instId then
                return i
            end

        elseif self.m_submitType == SubmitType.AnyAndNoInst then
            if bundle.id == itemBundle.id then
                return i
            end
        end
    end

    return -1
end

SubmitItemCtrl._RealSelectItem = HL.Method(HL.Table, HL.Any, HL.Number) << function(self, itemBundle, cell, index)

    local i = self:_GetItemSelectIndex(itemBundle)
    if i > 0 then
        table.remove(self.m_submitItemsSelect, i)
        cell.view.toggle.gameObject:SetActiveIfNecessary(false)
        self:_UpdateCount()
        return
    end

    local selectCount = 0
    if self.m_submitType == SubmitType.CommonAndInst then
        for _,data in pairs(self.m_submitItemsSelect) do
            if data then
                selectCount = selectCount + 1
            end
        end
    else
        selectCount = #self.m_submitItemsSelect
    end


    local totalCount = self:_GetSelectItemBundleCount()
    local id = cell.id
    if not Utils.isItemInstType(id) and cell.count <= 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SUBMIT_ITEM_SELECT_NOT_ENOUGH)
        return
    end

    if selectCount >= totalCount then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SUBMIT_ITEM_SELECT_MAX_COUNT_TIP)
        return
    end

    local isLocked = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemBundle.id, itemBundle.instId)
    if isLocked then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SUBMIT_ITEM_LOCKED_TIP)
        return
    end

    cell.view.toggle.gameObject:SetActiveIfNecessary(true)
    local selectItem = {
        id = itemBundle.id,
        count = 1,
        instId = itemBundle.instId
    }

    table.insert(self.m_submitItemsSelect, selectItem)
    self:_UpdateCount()
end

SubmitItemCtrl._UpdateSelectText = HL.Method() << function(self)
    local totalCount = self:_GetSelectItemBundleCount()
    local hasSelectItem = totalCount > 0

    if hasSelectItem then
        self.view.centerSelect.textSelect.gameObject:SetActiveIfNecessary(hasSelectItem)
        local data = Tables.submitItem[self.m_info.submitId]
        local hintDes = data.selectHintDes
        local title
        if string.isEmpty(hintDes) then
            if self.m_submitType == SubmitType.AnyAndInst then
                title = Language.LUA_SUBMIT_COMMON_HINT
            elseif self.m_submitType == SubmitType.AnyAndNoInst then
                title = string.format(Language.LUA_SUBMIT_SELECT_HINT, self:_GetSelectItemBundleCount())
            else
                local submitItemData = Tables.itemTable:GetValue(self.m_selectItemBundle[1].id)
                local rarityColorStr = Tables.rarityColorTable[submitItemData.rarity].color
                title = string.format(
                        Language.LUA_SUBMIT_ITEM_SELECT_TIP,
                        totalCount,
                        submitItemData.name,
                        rarityColorStr)
            end

        else
            title = hintDes
        end

        self.view.centerSelect.textSelect:SetAndResolveTextStyle(title)
    end
end

SubmitItemCtrl._RefreshCenter = HL.Method() << function(self)
    local totalCount = self:_GetSelectItemBundleCount()
    local hasSelectItem = totalCount > 0
    local hasNormalItem = #self.m_submitItemsNormal > 0
    self.view.centerSelect.gameObject:SetActiveIfNecessary(hasSelectItem)
    self.view.centerNormal.gameObject:SetActiveIfNecessary(hasNormalItem)
end

SubmitItemCtrl._GetItemCount = HL.StaticMethod(HL.String).Return(HL.Number) << function(itemId)
    local itemData = Tables.itemTable:GetValue(itemId)
    local typeData = Tables.itemTypeTable:GetValue(itemData.type:GetHashCode())
    if typeData.storageSpace == GEnums.ItemStorageSpace.ValuableDepot then
        local count = Utils.getItemCount(itemId)
        return count
    end

    return GameInstance.player.inventory:GetItemCountInBag(Utils.getCurrentScope(), itemId)
end

SubmitItemCtrl._UpdateCount = HL.Method() << function(self)
    self:_RefreshCenter()
    local enoughNormal = true

    self.m_normalItemListCache:Update(function(cell, index)
        local bundle = self.m_submitItemsNormal[index]
        local count = SubmitItemCtrl._GetItemCount(bundle.id)
        local isLack = count < bundle.count
        cell.item:UpdateCountSimple(bundle.count, isLack)
        UIUtils.setItemStorageCountText(cell.storageNode, bundle.id, bundle.count)
        if isLack then
            enoughNormal = false
        end
    end)



    local enoughSelect = true
    self.m_selectItemListCache:Update(function(cell, index)
        local bundle = self.m_submitItemsSelect[index]
        if bundle == nil or bundle.instId == 0 then
            enoughSelect = false
            cell.item:InitItem({ })
            cell.add.gameObject:SetActiveIfNecessary(true)
            cell.storageNode.gameObject:SetActive(false)
        else
            cell.item:InitItem(bundle, function()
                self:_OpenItemSelectList()
            end, "", true)
            cell.add.gameObject:SetActiveIfNecessary(false)
            if self.m_submitType == SubmitType.AnyAndNoInst then
                cell.storageNode.gameObject:SetActive(true)
                UIUtils.setItemStorageCountText(cell.storageNode, bundle.id, bundle.count)
            else
                cell.storageNode.gameObject:SetActive(false)
            end
        end
    end)
end

SubmitItemCtrl.OnSubmitItem = HL.Method(HL.Any) << function(self, submitId)
    self.m_isSubmitting = false
    if (submitId[1] == self.m_info.submitId) then
        self:ClosePanel(0)
    else
        logger.info("SubmitItemCtrl.OnSubmitItem: submitId not match", submitId, self.m_info.submitId)
    end
end

SubmitItemCtrl.OnSubmitItemFailed = HL.Method(HL.Any) << function(self, submitId)
    self.m_isSubmitting = false
    if (submitId[1] == self.m_info.submitId) then
        self:PlayAnimationOutWithCallback(function()
            self:ClosePanel(0)
        end)
        logger.error("SubmitItemCtrl.OnSubmitItemFailed: submit failed for submitId ", submitId)
    end
end

SubmitItemCtrl.OnItemLockedStateChanged = HL.Method(HL.String, HL.Number) << function(self, itemId, instId)
    local selectedItems = self.m_submitItemsSelect
    local needRefresh = false
    for i = #selectedItems, 1, -1 do
        if (selectedItems[i].instId == instId) then
            table.remove(selectedItems, i)
            needRefresh = true
        end
    end

    if needRefresh then
        self:_UpdateCount()
    end
end

SubmitItemCtrl.ShowPanel = HL.StaticMethod(HL.Any) << function(arg)
    arg = unpack(arg) or arg
    PhaseManager:OpenPhase(PHASE_ID, arg)
end

SubmitItemCtrl.ClosePanel = HL.Method(HL.Number) << function(self, nextChunkIndex)
    if self.m_waitingClosePanel then
        return
    end

    Notify(MessageConst.DIALOG_CHANGE_NEXT_INDEX, { phaseId = PHASE_ID, nextIndex = nextChunkIndex, })
    if PhaseManager:CanPopPhase(PHASE_ID) then
        PhaseManager:PopPhase(PHASE_ID)
        return
    end

    self.m_waitingClosePanel = true
    self:_StartCoroutine(function()
        coroutine.waitCondition(function()
            return self.m_isClosed or PhaseManager:CanPopPhase(PHASE_ID)
        end)

        if self.m_isClosed then
            return
        end

        self.m_waitingClosePanel = false
        PhaseManager:PopPhase(PHASE_ID)
    end)
end

HL.Commit(SubmitItemCtrl)
