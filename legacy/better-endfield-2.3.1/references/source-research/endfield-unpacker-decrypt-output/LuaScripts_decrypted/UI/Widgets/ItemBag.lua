
local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemBag = HL.Class('ItemBag', UIWidgetBase)


ItemBag.itemBagContent = HL.Field(HL.Forward("ItemBagContent"))


ItemBag._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_ITEM_BAG_CHANGED, function(args)
        self:_UpdateCount()
    end)
    self:RegisterMessage(MessageConst.ON_ITEM_BAG_LIMIT_CHANGED, function()
        self:_UpdateCount()
    end)
    self:RegisterMessage(MessageConst.ON_SYNC_INVENTORY, function()
        self:_UpdateCount()
    end)

    self.itemBagContent = self.view.itemBagContent
end

ItemBag.InitItemBag = HL.Method(HL.Opt(HL.Function, HL.Table)) << function(self, onClickItemAction, otherArgs)
    self:_FirstTimeInit()

    self.itemBagContent:InitItemBagContent(onClickItemAction, otherArgs)
    self:_UpdateCount()
end

ItemBag._UpdateCount = HL.Method() << function(self)
    local bag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())
    self.view.countTxt.text = string.format("%d/%d", bag:GetUsedSlotCount(), bag.slotCount)
end


HL.Commit(ItemBag)
return ItemBag
