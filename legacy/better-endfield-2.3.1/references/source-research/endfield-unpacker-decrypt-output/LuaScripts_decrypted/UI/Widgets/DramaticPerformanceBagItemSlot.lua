local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DramaticPerformanceBagItemSlot = HL.Class('DramaticPerformanceBagItemSlot', UIWidgetBase)



DramaticPerformanceBagItemSlot.m_dragHelper = HL.Field(HL.Forward('UIDragHelper'))

DramaticPerformanceBagItemSlot.m_dropHelper = HL.Field(HL.Forward('UIDropHelper'))

DramaticPerformanceBagItemSlot.m_itemInfo = HL.Field(HL.Table)

DramaticPerformanceBagItemSlot.m_onDropItemCallback = HL.Field(HL.Function)





DramaticPerformanceBagItemSlot._OnFirstTimeInit = HL.Override() << function(self)
    self:_InitSlotDrag()
end

DramaticPerformanceBagItemSlot.InitDramaticPerformanceBagItemSlot = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()
    self.m_itemInfo = args
    self.m_onDropItemCallback = args.onDropItem
    self:_RefreshSlotContent()
    self:_InitDrop()
end




DramaticPerformanceBagItemSlot._RefreshSlotContent = HL.Method() << function(self)
    local info = self.m_itemInfo
    local isEmpty = string.isEmpty(info.id)
    local itemSlot = self.view.itemSlot
    local sourceType = self.m_itemInfo.sourceType

    itemSlot.view.dragItem.enabled = not isEmpty and info.allowDrag
    if isEmpty then
        itemSlot:InitItemSlot()
    else
        itemSlot:InitItemSlot(info, function()
            self:_OnClickItemSlot(itemSlot, info)
        end)
        itemSlot.item:AddHoverBinding("show_item_tips", function()
            itemSlot.item:ShowTips()
            self:_SetItemTipsFakeItemCount()
        end)

        self.m_dragHelper = UIUtils.initUIDragHelper(itemSlot.view.dragItem, {
            source = sourceType,
            type = info.type,
            id = info.id,
            itemId = info.id,
            count = info.count,
        })
        itemSlot:InitPressDrag()
    end

    itemSlot.item.actionMenuArgs = {
        source = sourceType,
        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid,
        dragHelper = self.m_dragHelper,
        cacheArea = self.m_itemInfo.cacheArea
    }
    itemSlot.item.customChangeActionMenuFunc = self.m_itemInfo.customChangeActionMenuFunc
end




DramaticPerformanceBagItemSlot._InitSlotDrag = HL.Method() << function(self)
    self:RegisterMessage(MessageConst.ON_START_UI_DRAG, function(dragHelper)
        self:_OnStartUiDrag(dragHelper)
    end)
    self:RegisterMessage(MessageConst.ON_END_UI_DRAG, function(dragHelper)
        self:_OnEndUiDrag(dragHelper)
    end)
end

DramaticPerformanceBagItemSlot._OnStartUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if dragHelper == nil then
        return
    end
    local itemSlot = self.view.itemSlot
    local canAcceptDrop = self:_CanAcceptDrop(dragHelper)
    itemSlot.view.dropItem.enabled = canAcceptDrop
    if canAcceptDrop then
        itemSlot.view.dropHintNode.gameObject:SetActiveIfNecessary(true)
    end
end

DramaticPerformanceBagItemSlot._OnEndUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if dragHelper == nil then
        return
    end
    local itemSlot = self.view.itemSlot
    itemSlot.view.dropItem.enabled = false
    if self:_CanAcceptDrop(dragHelper) then
        itemSlot.view.dropHintNode.gameObject:SetActiveIfNecessary(false)
    end
end

DramaticPerformanceBagItemSlot.ForbidDrag = HL.Method() << function(self)
    self.m_itemInfo.allowDrag = false
    self.view.itemSlot.view.dragItem.enabled = false
end




DramaticPerformanceBagItemSlot._InitDrop = HL.Method() << function(self)
    local itemSlot = self.view.itemSlot
    self.m_dropHelper = UIUtils.initUIDropHelper(itemSlot.view.dropItem, {
        acceptTypes = {
            sources = {
                self.m_itemInfo.acceptType
            },
            types = {
                self.m_itemInfo.type
            },
        },
        onDropItem = function(eventData, dragHelper)
            if self:_CanAcceptDrop(dragHelper) then
                self:_OnDropItem(dragHelper)
            end
        end,
        onToggleHighlight = function(active)
            self:_MobileRefreshDropHighlight(active)
        end
    })
end

DramaticPerformanceBagItemSlot._OnDropItem = HL.Method(HL.Forward('UIDragHelper'), HL.Opt(CS.Proto.ITEM_MOVE_MODE)) << function(self, dragHelper)
    local dragInfo = dragHelper.info
    local itemSlot = self.view.itemSlot
    itemSlot:InitItemSlot(dragInfo)
    itemSlot.view.dragItem.enabled = not string.isEmpty(dragInfo.itemId) and self.m_itemInfo.allowDrag
    logger.info("[DramaticPerformanceBagItemSlot] drop item")
    if self.m_onDropItemCallback then
        self.m_onDropItemCallback()
    end
end




DramaticPerformanceBagItemSlot._OnClickItemSlot = HL.Method(HL.Forward('ItemSlot'), HL.Table) << function(self, itemSlot, info)
    if DeviceInfo.usingController then
        self.m_itemInfo.cacheArea:NaviTargetMoveToInCacheSlot(self, self.m_dragHelper, false)
        itemSlot.item.view.button.interactable = false
        return
    end

    itemSlot.item:SetSelected(true)
    itemSlot.item:ShowTips(nil, function()
        itemSlot.item:SetSelected(false)
    end)
    self:_SetItemTipsFakeItemCount()
end

DramaticPerformanceBagItemSlot._CanAcceptDrop = HL.Method(HL.Forward('UIDragHelper')).Return(HL.Boolean) << function(self, dragHelper)
    return self.m_dropHelper:Accept(dragHelper)
end

DramaticPerformanceBagItemSlot._MobileRefreshDropHighlight = HL.Method(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingTouch then
        return
    end

    local itemSlot = self.view.itemSlot
    if not itemSlot.view.dropItem.enabled then
        return
    end

    itemSlot.view.dropHintNode.gameObject:SetActiveIfNecessary(not active)
end

DramaticPerformanceBagItemSlot._SetItemTipsFakeItemCount = HL.Method() << function(self)
    local isOpen, itemTipsCtrl = UIManager:IsOpen(PanelId.ItemTips)
    if isOpen then
        itemTipsCtrl.view.countNode.valuableCountText.text = 1
    end
end


HL.Commit(DramaticPerformanceBagItemSlot)
return DramaticPerformanceBagItemSlot

