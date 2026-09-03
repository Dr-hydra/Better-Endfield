local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ClearBottlePopUp

ClearBottlePopUpCtrl = HL.Class('ClearBottlePopUpCtrl', uiCtrl.UICtrl)

local CAPACITY_TEXT_ID = "LUA_ITEM_TIPS_LIQUID_INFO_FULL_CAPACITY"
local CLEAR_TIPS_TEXT_ID = "LUA_CLEAR_BOTTLE_POPUP_TIPS_TEXT"

ClearBottlePopUpCtrl.m_itemBagIndex = HL.Field(HL.Number) << -1

ClearBottlePopUpCtrl.m_fromDepot = HL.Field(HL.Boolean) << false

ClearBottlePopUpCtrl.m_itemId = HL.Field(HL.String) << ""

ClearBottlePopUpCtrl.m_itemCount = HL.Field(HL.Number) << -1





ClearBottlePopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ClearBottlePopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.m_itemBagIndex = arg.slotIndex or 0
    self.m_itemId = arg.itemId
    self.m_itemCount = arg.itemCount
    if arg.fromDepot == nil then
        self.m_fromDepot = false
    else
        self.m_fromDepot = arg.fromDepot
    end

    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnConfirmBtnClicked()
    end)
    self.view.cancelBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.contentNaviGroup.getDefaultSelectableFunc = function()
        return self.view.sourceItem.view.button
    end

    self:_RefreshPopupContent()
end

ClearBottlePopUpCtrl._OnConfirmBtnClicked = HL.Method() << function(self)
    local scope = Utils.getCurrentScope()
    local chapterId = Utils.getCurrentChapterId()
    if self.m_fromDepot then
        GameInstance.player.inventory:DumpBottleInDepot(self.m_itemId, self.m_itemCount, scope, chapterId)
    else
        GameInstance.player.inventory:DumpBottleInItemBag(self.m_itemBagIndex, self.m_itemCount, scope)
    end
    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(self.m_itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    if isFullBottle then
        AudioAdapter.PostEvent("Au_UI_Event_WaterDown_Small")
    else
        AudioAdapter.PostEvent("Au_UI_Event_GasDown_Small")
    end
    self:PlayAnimationOutAndClose()
end

ClearBottlePopUpCtrl._RefreshPopupContent = HL.Method() << function(self)
    local fullBottleSuccess, fullBottleData = Tables.fullBottleTable:TryGetValue(self.m_itemId)
    local isGas = false
    if not fullBottleSuccess then
        fullBottleSuccess, fullBottleData = Tables.fullGasJarTable:TryGetValue(self.m_itemId)
        if not fullBottleSuccess then
            return
        end
        isGas = true;
    end

    local bottleItemId = isGas and fullBottleData.emptyJarId or fullBottleData.emptyBottleId
    local itemId = isGas and fullBottleData.gasId or fullBottleData.liquidId
    local bottleCapacity = isGas and fullBottleData.gasCapacity or fullBottleData.liquidCapacity

    local bottleSuccess, bottleData = Tables.itemTable:TryGetValue(bottleItemId)
    local itemSuccess, itemData = Tables.itemTable:TryGetValue(itemId)
    if not bottleSuccess or not itemSuccess then
        return
    end


    self.view.bottleNameTxt.text = bottleData.name
    self.view.bottleRarityLine.color = UIUtils.getItemRarityColor(bottleData.rarity)
    self.view.bottleItemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, bottleData.iconId)


    local liquidInfoNode = self.view.liquidInfoNode
    liquidInfoNode.nameTxt.text = itemData.name
    local liquidCount = bottleCapacity * self.m_itemCount
    liquidInfoNode.capacityTxt.text = string.format(Language[CAPACITY_TEXT_ID], liquidCount)
    liquidInfoNode.rarityLine.color = UIUtils.getItemRarityColor(itemData.rarity)
    liquidInfoNode.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)


    self.view.tipsTxt.text = string.format(Language[CLEAR_TIPS_TEXT_ID], liquidCount, itemData.name)
    self.view.titleText.text = isGas and Language.LUA_ITEM_TIPS_EMPTY_GAS_TITLE or Language.LUA_ITEM_TIPS_EMPTY_LIQUID_TITLE
    self.view.liquidPouring.gameObject:SetActiveIfNecessary(not isGas)
    self.view.gasDumping.gameObject:SetActiveIfNecessary(isGas)


    self.view.sourceItem:InitItem({ id = self.m_itemId, count = self.m_itemCount }, true)
    self.view.sourceItem:SetExtraInfo({
        isSideTips = DeviceInfo.usingController,
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
    })
    self.view.targetItem:InitItem({ id = bottleItemId, count = self.m_itemCount }, true)
    self.view.targetItem:SetExtraInfo({
        isSideTips = DeviceInfo.usingController,
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
    })
end

HL.Commit(ClearBottlePopUpCtrl)
