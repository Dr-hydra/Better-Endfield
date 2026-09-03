local ItemSlot = require_ex("UI/Widgets/ItemSlot")

LiquidGasItemSlot = HL.Class('LiquidGasItemSlot', ItemSlot)

LiquidGasItemSlot.m_itemType = HL.Field(HL.Number) << 0


LiquidGasItemSlot._OnFirstTimeInit = HL.Override() << function(self)
    LiquidGasItemSlot.Super._OnFirstTimeInit(self)
    self.view.facLiquidBg:InitFacLiquidBg()
end

LiquidGasItemSlot.InitLiquidGasItemSlot = HL.Method(HL.Opt(HL.Any, HL.Any, HL.String, HL.Boolean)) <<
    function(self, itemBundle, onClick, limitId, clickableEvenEmpty)
    self:_OnFirstTimeInit()
    LiquidGasItemSlot.Super.InitItemSlot(self, itemBundle, onClick, limitId, clickableEvenEmpty)
end

LiquidGasItemSlot.SetAcceptDrop = HL.Method(HL.Opt(HL.Boolean, HL.String, HL.Any)) << function(self, active, itemId, dropType)
    if active then
        self.view.dropItem.enabled = true
        self:SetHintActive(true, itemId, dropType)
    else
        self.view.dropItem.enabled = false
    end
end

LiquidGasItemSlot.SetHintActive = HL.Method(HL.Opt(HL.Boolean, HL.String, HL.Any)) << function(self, active, itemId, dropType)
    if active then
        self.view.dropHintImg.gameObject:SetActiveIfNecessary(true)
        if dropType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid then
            self:_RefreshLiquidItemSlotDropHintText(itemId)
        elseif dropType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas then
            self:_RefreshGasItemSlotDropHintText(itemId)
        elseif dropType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid then
            self:_RefreshLiquidItemSlotDropHintText(itemId)
            self:_RefreshGasItemSlotDropHintText(itemId)
        end
    else
        self.view.dropHintImg.gameObject:SetActiveIfNecessary(false)
    end
end

LiquidGasItemSlot.SetItemType = HL.Method(HL.Any) << function(self, itemType)
    self.m_itemType = itemType

    self.view.facLiquidBg.gameObject:SetActiveIfNecessary(itemType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    self.view.facGasBg.gameObject:SetActiveIfNecessary(itemType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
end

LiquidGasItemSlot.RefreshLiquidHeight = HL.Method(HL.Number) << function(self, height)
    if self.m_itemType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid then
        self.view.facLiquidBg:RefreshLiquidHeight(height)
    elseif self.m_itemType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas then
        self.view.facGasBg:RefreshGasHeight(height)
    else
        if string.isEmpty(self.item.id) then
            self.view.facLiquidBg.gameObject:SetActiveIfNecessary(false)
            self.view.facGasBg.gameObject:SetActiveIfNecessary(false)
            self.view.facLiquidBg:RefreshLiquidHeight(0)
            self.view.facGasBg:RefreshGasHeight(0)
        else
            local isLiquid = Tables.liquidTable:ContainsKey(self.item.id)
            self.view.facLiquidBg.gameObject:SetActiveIfNecessary(isLiquid)
            self.view.facGasBg.gameObject:SetActiveIfNecessary(not isLiquid)
            if isLiquid then
                self.view.facLiquidBg:RefreshLiquidHeight(height)
            else
                self.view.facGasBg:RefreshGasHeight(height)
            end
        end
    end
end

LiquidGasItemSlot._RefreshLiquidItemSlotDropHintText = HL.Method(HL.String) << function(self, itemId)
    local isEmptyBottle, isFullBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid), FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    if not isEmptyBottle and not isFullBottle then
        return
    end

    local text = isEmptyBottle and Language.LUA_ITEM_ACTION_FILL_LIQUID or Language.LUA_ITEM_ACTION_DUMP_LIQUID
    self.view.dropHintText.text = text
end

LiquidGasItemSlot._RefreshGasItemSlotDropHintText = HL.Method(HL.String) << function(self, itemId)
    local isEmptyBottle, isFullBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas), FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
    if not isEmptyBottle and not isFullBottle then
        return
    end
    local text = isEmptyBottle and Language.LUA_ITEM_ACTION_FILL_GAS or Language.LUA_ITEM_ACTION_DUMP_GAS
    self.view.dropHintText.text = text
end

HL.Commit(LiquidGasItemSlot)
return LiquidGasItemSlot

