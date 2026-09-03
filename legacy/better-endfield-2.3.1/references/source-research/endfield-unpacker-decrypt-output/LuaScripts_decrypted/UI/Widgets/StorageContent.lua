local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

StorageContent = HL.Class('StorageContent', UIWidgetBase)





StorageContent.m_getCell = HL.Field(HL.Function)

StorageContent.m_storage = HL.Field(CS.Beyond.Gameplay.RemoteFactory.FBUtil.GridBox)

StorageContent.m_onClickItemAction = HL.Field(HL.Function)

StorageContent.m_waitInitNaviTarget = HL.Field(HL.Boolean) << true





StorageContent._OnFirstTimeInit = HL.Override() << function(self)
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.itemList, function(object)
        return UIWidgetManager:Wrap(object)
    end)
    self.view.itemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(self.m_getCell(object), csIndex)
    end)

    self.view.dropHint.gameObject:SetActive(false)
    self.view.dropMask.gameObject:SetActive(false)
    self:RegisterMessage(MessageConst.ON_START_UI_DRAG, function(dragHelper)
        if UIUtils.isTypeDropValid(dragHelper, UIConst.FACTORY_STORAGER_DROP_ACCEPT_INFO) and dragHelper.info.storage ~= self.m_storage then
            self.view.quickDropButton.onClick:RemoveAllListeners()
            self.view.quickDropButton.onClick:AddListener(function()
                self:_OnDropItem(-1, dragHelper)
            end)
            self.view.dropHint.gameObject:SetActive(true)
            self.view.dropMask.gameObject:SetActive(true)
            self:_ShowMobileDragHelper(dragHelper)
        end
    end)
    self:RegisterMessage(MessageConst.ON_END_UI_DRAG, function(dragHelper)
        if UIUtils.isTypeDropValid(dragHelper, UIConst.FACTORY_STORAGER_DROP_ACCEPT_INFO) and dragHelper.info.storage ~= self.m_storage then
            self.view.dropHint.gameObject:SetActive(false)
            self.view.dropMask.gameObject:SetActive(false)
            Notify(MessageConst.HIDE_ITEM_DRAG_HELPER)
        end
    end)

    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ITEM_BAG_TO_GRID_BOX, function(args)
        self:_OnMoveItemToStorage(args)
    end)
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ITEM_DEPOT_TO_GRID_BOX, function(args)
        self:_OnMoveItemToStorage(args)
    end)

    UIUtils.initUIDropHelper(self.view.dropMask, {
        acceptTypes = UIConst.FACTORY_STORAGER_DROP_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            self:_OnDropItem(-1, dragHelper)
        end,
        isDropArea = true,
        quickDropCheckGameObject = self.gameObject,
        dropPriority = 1,
    })
end


StorageContent.InitStorageContent = HL.Method(CS.Beyond.Gameplay.RemoteFactory.FBUtil.GridBox, HL.Opt(HL.Function, HL.Table)) <<
function(self, storage, onClickItemAction, otherArgs)
    otherArgs = otherArgs or {}

    self:_FirstTimeInit()

    self.m_storage = storage
    self.m_onClickItemAction = otherArgs.onClickItemAction
    self.m_waitInitNaviTarget = true

    self:Refresh()


    self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_TryUpdateContent()
        end
    end)
end

StorageContent.Refresh = HL.Method() << function(self)
    self.view.itemList:UpdateCount(self.m_storage.items.Count)
end

StorageContent._TryUpdateContent = HL.Method() << function(self)
    for k, v in pairs(self.m_storage.items) do
        local cell = self.m_getCell(LuaIndex(k))
        if cell then
            local id, count = v.Item1, v.Item2
            if cell.item.id ~= id or cell.item.count ~= count then
                self:_OnUpdateCell(cell, k)
            end
        end
    end
end

StorageContent._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local item = self.m_storage.items[csIndex]
    item = {
        id = item.Item1,
        count = item.Item2,
    }

    local isEmpty = string.isEmpty(item.id)
    if isEmpty then
        cell.gameObject.name = "Item__" .. csIndex
    else
        cell.gameObject.name = "Item_" .. item.id
    end

    cell:InitItemSlot(item, function()
        self:_OnClickItem(csIndex)
    end)
    cell.supportQuickMovingHalfItem = true

    if not isEmpty then
        local data = Tables.itemTable:GetValue(item.id)
        local dragHelper = UIUtils.initUIDragHelper(cell.view.dragItem, {
            source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage,
            type = data.type,

            storage = self.m_storage,
            csIndex = csIndex,

            itemId = item.id,
            count = item.count,
        })
        cell:InitPressDrag()
        cell.item.view.button.clickHintTextId = "virtual_mouse_hint_item_tips"
    end
    UIUtils.initUIDropHelper(cell.view.dropItem, {
        acceptTypes = UIConst.FACTORY_STORAGER_DROP_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            self:_OnDropItem(csIndex, dragHelper)
        end,
    })


    if not isEmpty then
        cell.item:AddHoverBinding("common_quick_drop", function()
            self.view.dropMask.enabled = false
            cell:QuickDrop()
            self.view.dropMask.enabled = true
        end)

        cell.item.actionMenuArgs = {
            source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage,
            componentId = self.m_storage.componentId,
            csIndex = csIndex,
        }
        InputManagerInst:SetBindingText(cell.item.view.button.hoverConfirmBindingId, Language["key_hint_item_open_action_menu"])
    end
    if self.m_waitInitNaviTarget and csIndex == 0 then
        cell:SetAsNaviTarget()
        self.m_waitInitNaviTarget = false
    end
    local action = not isEmpty and ActionOnSetNaviTarget.PressConfirmTriggerOnClick or ActionOnSetNaviTarget.None
    cell.item.view.button:ChangeActionOnSetNaviTarget(action)
end

StorageContent._OnDropItem = HL.Method(HL.Number, HL.Forward('UIDragHelper')) << function(self, csIndex, dragHelper)
    local source = dragHelper.source
    local dragInfo = dragHelper.info
    local core = GameInstance.player.remoteFactory.core
    local componentId = self.m_storage.componentId
    if csIndex < 0 then
        for k, v in pairs(self.m_storage.items) do
            local cell = self.m_getCell(LuaIndex(k))
            if cell then
                if string.isEmpty(v.Item1) then
                    csIndex = k
                    break
                end
            end
        end
    end
    if csIndex < 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_STORAGE_NO_EMPTY_SLOT)
        return
    end

    local mode = dragHelper.isHalfDragging and CS.Proto.ITEM_MOVE_MODE.HalfGrid or CS.Proto.ITEM_MOVE_MODE.Normal

    if source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage then

        core:Message_OpGridBoxInnerMove(Utils.getCurrentChapterId(), componentId, dragInfo.csIndex, csIndex)
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then

        core:Message_OpMoveItemDepotToGridBox(Utils.getCurrentChapterId(), dragInfo.itemId, componentId, csIndex, mode)
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then

        core:Message_OpMoveItemBagToGridBox(Utils.getCurrentChapterId(), dragInfo.csIndex, componentId, csIndex, mode)
    end

    if dragInfo.itemId then
        local cptHandler = FactoryUtils.getBuildingComponentHandler(componentId)
        local buildingNode = cptHandler.belongNode
        local worldPos = GameInstance.remoteFactoryManager.visual:BuildingGridToWorld(
                Vector2(buildingNode.transform.position.x, buildingNode.transform.position.z))
        local curItems = {}
        for _, v in pairs(self.m_storage.items) do
            local id, count = v.Item1, v.Item2
            if not string.isEmpty(id) then
                if not curItems[id] then
                    curItems[id] = count
                else
                    curItems[id] = curItems[id] + count
                end
            end
        end
        EventLogManagerInst:GameEvent_FactoryItemPush(buildingNode.nodeId, buildingNode.templateId,
                GameInstance.remoteFactoryManager.currentSceneName, worldPos,
                dragInfo.itemId, dragInfo.count, curItems)
    end
end

StorageContent._OnClickItem = HL.Method(HL.Number) << function(self, csIndex)
    local item = self.m_storage.items[csIndex]
    local id, count = item.Item1, item.Item2
    local cell = self.m_getCell(LuaIndex(csIndex))

    if self.m_onClickItemAction then
        self.m_onClickItemAction(id)
    elseif not string.isEmpty(id) then
        if DeviceInfo.usingController then
            cell.item:ShowActionMenu()
            return
        end

        if DeviceInfo.usingKeyboard then
            local mode = UIUtils.getMovingItemMode()
            if mode and not Utils.isDepotManualInOutLocked() then
                local inventoryArea = self:GetUICtrl().view.inventoryArea
                if inventoryArea then
                    local isMoveToItemBag = inventoryArea.m_isItemBag
                    if isMoveToItemBag then
                        GameInstance.player.remoteFactory.core:Message_OpMoveItemGridBoxToBag(Utils.getCurrentChapterId(), self.m_storage.componentId, csIndex, 0, mode)
                    else
                        GameInstance.player.remoteFactory.core:Message_OpMoveItemGridBoxToDepot(Utils.getCurrentChapterId(), self.m_storage.componentId, csIndex, mode)
                    end
                    return
                end
            end
        end
        cell.item:ShowTips()
    end
end

StorageContent._ShowMobileDragHelper = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if not DeviceInfo.usingTouch then
        return
    end

    if not self.view.gameObject.activeInHierarchy then
        return
    end

    local dragInfo = dragHelper.info
    local moveAct
    if dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then
        moveAct = function(mode)
            GameInstance.player.remoteFactory.core:Message_OpMoveItemDepotToGridBox(Utils.getCurrentChapterId(), dragInfo.itemId, self.m_storage.componentId, 0, mode)
            dragHelper.uiDragItem:OnEndDrag(nil)
        end
    elseif dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
        moveAct = function(mode)
            GameInstance.player.remoteFactory.core:Message_OpMoveItemBagToGridBox(Utils.getCurrentChapterId(), dragInfo.csIndex, self.m_storage.componentId, 0, mode)
            dragHelper.uiDragItem:OnEndDrag(nil)
        end
    else
        return
    end

    local args = {
        isLeft = false,
        actions = {
            {
                text = Language.LUA_MOBILE_ITEM_DRAG_GRID_TO_STORAGE,
                icon = "icon_common_move_to_machine",
                action = function()
                    moveAct(CS.Proto.ITEM_MOVE_MODE.Grid)
                end
            },
            {
                text = Language.LUA_MOBILE_ITEM_DRAG_ALL,
                icon = "icon_common_move_all",
                action = function()
                    moveAct(CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                end
            },
            {
                text = Language.LUA_MOBILE_ITEM_DRAG_HALF,
                icon = "icon_common_move_half",
                action = function()
                    moveAct(CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                end
            },
        }
    }
    Notify(MessageConst.SHOW_ITEM_DRAG_HELPER, args)
end


StorageContent._OnMoveItemToStorage = HL.Method(HL.Table) << function(self, args)
    local toComponentId, toGridIndex, itemId = unpack(args)
    if self.m_storage.componentId ~= toComponentId then
        return
    end

    for _, index in cs_pairs(toGridIndex) do

        local cell = self.m_getCell(LuaIndex(index))
        if cell then
            cell:PlayDropAnimation()
        end
    end
    UIUtils.playItemDropAudio(itemId)
end


HL.Commit(StorageContent)
return StorageContent
