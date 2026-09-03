local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemSlot = HL.Class('ItemSlot', UIWidgetBase)

ItemSlot.item = HL.Field(HL.Forward('Item'))

ItemSlot.supportQuickMovingHalfItem = HL.Field(HL.Boolean) << false


ItemSlot._OnFirstTimeInit = HL.Override() << function(self)
    self.item = self.view.item
end

ItemSlot.InitItemSlot = HL.Method(HL.Opt(HL.Any, HL.Any, HL.String, HL.Boolean)) <<
function(self, itemBundle, onClick, limitId, clickableEvenEmpty)
    self:_FirstTimeInit()

    self.item.view.button.longPressImg = nil
    self.view.pressHintImg.gameObject:SetActive(false)

    self.view.lockNode.gameObject:SetActive(false)
    self.view.item.gameObject:SetActive(true)

    self.item:InitItem(itemBundle, onClick, limitId, clickableEvenEmpty)

    local isEmpty = itemBundle == nil or itemBundle.id == ""
    if self.view.emptyNode then
        self.view.emptyNode.gameObject:SetActive(isEmpty)
    end

    if isEmpty then
        self.view.dragItem.enabled = false

        self.view.dropItem:ClearEvents()
        self.view.dragItem:ClearEvents()
        return
    end

    self.view.dropItem:ClearEvents()
    self.view.dragItem:ClearEvents()
    self.view.dragItem.enabled = true
    self.view.dragItem.dragPivot = DeviceInfo.usingTouch and self.config.DRAG_PIVOT_FOR_MOBILE or self.config.DRAG_PIVOT_FOR_PC
    self.item.view.button.longPressHintTextId = "virtual_mouse_hint_drag"

    self.view.dragItem.onUpdateDragObject:AddListener(function(dragObj)
        local dragHelper = self.view.dragItem.luaTable[1]
        dragHelper.isHalfDragging = self.supportQuickMovingHalfItem and UIUtils.isQuickMovingHalfItem()
        local dragItem = UIWidgetManager:Wrap(dragObj)
        dragItem:InitItem({ id = itemBundle.id, count = dragHelper:GetCount() })
    end)
end

ItemSlot.InitLockSlot = HL.Method() << function(self)
    self:_FirstTimeInit()

    self.item.view.button.longPressImg = nil
    self.view.pressHintImg.gameObject:SetActive(false)
    self.view.item.gameObject:SetActive(false)
    self.view.lockNode.gameObject:SetActive(true)

    self.view.dragItem.enabled = false

    self.view.lockNode.onClick:RemoveAllListeners()
    self.view.lockNode.onClick:AddListener(function()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_BAG_SLOT_LOCKED)
    end)

    self.view.dropItem.onDropEvent:RemoveAllListeners()
    self.view.dragItem.onBeginDragEvent:RemoveAllListeners()
    self.view.dragItem.onDragEvent:RemoveAllListeners()
    self.view.dragItem.onDragEventWhenCantStartDrag:RemoveAllListeners()
    self.view.dragItem.onEndDragEvent:RemoveAllListeners()
    self.view.dragItem.onUpdateDragObject:RemoveAllListeners()
end

ItemSlot.InitPressDrag = HL.Method() << function(self)
    if DeviceInfo.usingTouch then
        self:InitPressDragForTouch()
    end
end

ItemSlot.InitPressDragForTouch = HL.Method() << function(self)
    self.item.view.button.longPressImg = self.view.pressHintImg

    if not self.view.dragItem.inDragging then
        self.view.dragItem.canStartDrag = false
    end

    self.item.view.button.onLongPress:AddListener(function(eventData)
        if not self.view.dragItem.enabled or self.view.dragItem.canStartDrag then
            return
        end
        self.view.dragItem.canStartDrag = true
        self.view.dragItem:OnBeginDrag(eventData)
        GameInstance.mobileMotionManager:PostEventCommonShort()
    end)

    local startPressTime
    local needCheckIsDragItem
    self.item.view.button.onPressStart:AddListener(function(eventData)
        startPressTime = Time.unscaledTime
        needCheckIsDragItem = true
    end)
    self.view.dragItem.onDragEventWhenCantStartDrag:AddListener(function(eventData)
        if not needCheckIsDragItem then
            return
        end
        if self.view.dragItem.canStartDrag then
            needCheckIsDragItem = false
            return
        end
        local delta = eventData.position - eventData.pressPosition
        if delta.sqrMagnitude >= self.view.config.MOBILE_AUTO_CHECK_IS_DRAG_ITEM_MAX_DIST_SQR then
            needCheckIsDragItem = false
            local isDragItem = math.abs(delta.x) > math.abs(delta.y)
            if isDragItem then
                self.view.dragItem.canStartDrag = true
                self.view.dragItem:OnBeginDrag(eventData)
                GameInstance.mobileMotionManager:PostEventCommonShort()
            end
            return
        end
        if Time.unscaledTime >= startPressTime + self.item.view.button.longPressTime then
            needCheckIsDragItem = false
            return
        end
    end)

    self.view.dragItem.onEndDragEvent:AddListener(function(eventData)
        self.view.dragItem.canStartDrag = false
    end)

    self.item.view.button.onPressEnd:AddListener(function(eventData)
        if (not eventData.dragging) and self.view.dragItem.inDragging then


            self.view.dragItem:OnEndDrag(eventData)
        end
    end)
end

ItemSlot._GetQuickDropTarget = HL.Method().Return(HL.Opt(HL.Userdata)) << function(self)
    if not self.view.dragItem.luaTable then
        return
    end

    local dragHelper = self.view.dragItem.luaTable[1]
    local UIDropHelper = require_ex("Common/Utils/UI/UIDropHelper")
    local maxPriority, targetDropHelper


    for dropHelper, _ in pairs(UIDropHelper.s_dropAreas) do
        if not maxPriority or dropHelper.dropPriority > maxPriority then
            local checkTarget = dropHelper.info.quickDropCheckGameObject or dropHelper.uiDropItem.gameObject
            if dropHelper.uiDropItem.enabled and checkTarget.activeInHierarchy and dropHelper:Accept(dragHelper) then
                maxPriority = dropHelper.dropPriority
                targetDropHelper = dropHelper
            end
        end
    end

    return targetDropHelper
end

ItemSlot.QuickDrop = HL.Method() << function(self)
    if DeviceInfo.usingController and GameInstance.player.guide.isInForceGuide and not GameInstance.player.guide.isInHelperGuideStep then
        if not InputManager.instance.guideUseActionIds:Contains("common_quick_drop") then


            return
        end
    end
    local targetDropHelper = self:_GetQuickDropTarget()
    if targetDropHelper then
        local dragHelper = self.view.dragItem.luaTable[1]
        local shouldEnterSelectMode = targetDropHelper.info.onDropItem(nil, dragHelper)
        if shouldEnterSelectMode then


            local args = self.item.actionMenuArgs
            if args.cacheArea and args.dragHelper then
                args.cacheArea:NaviTargetMoveToInCacheSlot(self.item, args.dragHelper, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal)
            end
        end
    end
end

ItemSlot.IsQuickDropTargetValid = HL.Method().Return(HL.Boolean) << function(self)
    return self:_GetQuickDropTarget() ~= nil
end

ItemSlot.SetAsNaviTarget = HL.Method() << function(self)
    self.item:SetAsNaviTarget()
end

ItemSlot.PlayDropAnimation = HL.Method() << function(self)
    self.view.animationWrapper:Play("itemslot_item_drop")
end

ItemSlot.SetDropHighlighted = HL.Method(HL.Boolean) << function(self, active)
    Notify(MessageConst.ON_TOGGLE_ITEM_SLOT_DROP_HIGHLIGHT, {
        active,
        CS.Beyond.UI.CommonDropHintType.Square,
        self.view.transform:TransformPoint(self.view.transform.rect.center:XY()),
        self.view.transform.parent,
        self,
    })
end




ItemSlot.m_colorSlotActivated = HL.Field(HL.Boolean) << false

ItemSlot.UpdateSlotColorBGByItemId = HL.Method(CS.Beyond.Gameplay.InventorySystem.ItemBag, HL.Number, HL.String, HL.Boolean) << function(self, itemBag, csIndex, itemId, showDropHint)
    local isColoredSlot = csIndex >= 0 and csIndex < itemBag.coloredSlotNum
    local controller = self.view.coloredSlotStateController
    if not isColoredSlot then
        self.m_colorSlotActivated = false
        controller:SetState("NotColored")
        controller:SetState("NotInDrop")
        return
    end
    local isActive = itemBag:IsColoredSlotActive(itemId, csIndex)
    self.m_colorSlotActivated = isActive
    controller:SetState(isActive and "ColoredActive" or "ColoredNormal")
    controller:SetState(showDropHint and "InDrop" or "NotInDrop")
end


ItemSlot.PlayColoredSlotActivatedAnimation = HL.Method() << function(self)
    self.item.view.spTypeIconNode.animationWrapper:PlayInAnimation()
end




HL.Commit(ItemSlot)
return ItemSlot
