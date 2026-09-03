local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopItemPopUp
ShopItemPopUpCtrl = HL.Class('ShopItemPopUpCtrl', uiCtrl.UICtrl)


ShopItemPopUpCtrl.m_info = HL.Field(HL.Table)

ShopItemPopUpCtrl.m_onComplete = HL.Field(HL.Function)

ShopItemPopUpCtrl.m_curPrice = HL.Field(HL.Number) << -1






ShopItemPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BUY_ITEM_SUCC] = 'OnBuyItemSucc',
    [MessageConst.ON_SHOP_REFRESH] = 'PlayAnimationOutAndClose',
    [MessageConst.ON_SHOP_GOODS_CONDITION_REFRESH] = 'PlayAnimationOutAndClose',
}


ShopItemPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.cancelBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)
    Notify(MessageConst.SHOW_SHOP_ITEM_POP_UP, true)
    self:BindInputPlayerAction("show_item_tips", function()
        self.view.item:ShowTips()
    end)

    self.view.emptyClick.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.m_info = args.info
    self.m_onComplete = args.onComplete
    self:_UpdateContent()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

ShopItemPopUpCtrl._UpdateContent = HL.Method() << function(self)
    local info = self.m_info

    local itemId = info.itemId

    local itemData = Tables.itemTable[itemId]
    if info.itemCount > 1 then
        self.view.nameTxt.text = string.format(Language.LUA_SHOP_ITEM_NAME, itemData.name, info.itemCount)
    else
        self.view.nameTxt.text = itemData.name
    end
    self.view.item:InitItem({ id = itemId, count = info.itemCount })
    self.view.icon_Circle.onClick:AddListener(function()
        self.view.emptyClick.gameObject:SetActive(false)
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.icon_Circle.transform,
            itemId = itemId,
            onClose = function()
                if self and self.view.emptyClick and not IsNull(self.view.emptyClick.gameObject) then
                    self.view.emptyClick.gameObject:SetActive(true)
                end
            end
        })
    end)
    UIUtils.setItemRarityImage(self.view.rarityLine, itemData.rarity)
    self.view.descTxt.text = itemData.desc

    self.view.limitCountText.text = info.count

    if info.discount and info.discount < 1 then


        self.view.originalPrice.text = info.price
        local discount = tonumber(string.format("%.2f", info.discount + 0.001))
        self.m_curPrice = math.floor(info.price * discount)

    else

        self.view.originalPrice.gameObject:SetActive(false)
        self.m_curPrice = info.price
    end
    self.view.priceTxt.text = self.m_curPrice
    local moneyItemData = Tables.itemTable[info.moneyId]
    local goodsData = Tables.shopGoodsTable[info.goodsTemplateId]
    local shopId = info.shopId
    local shopSystem = GameInstance.player.shopSystem

    local lock = not (shopSystem:CheckGoodsUnlocked(info.goodsTemplateId) and shopSystem:CheckShopUnlocked(shopId))
    if lock then
        self.view.buyNode.gameObject:SetActiveIfNecessary(false)
        self.view.lockNode.gameObject:SetActiveIfNecessary(true)

        local goodsTableData = Tables.shopGoodsTable[info.goodsTemplateId]
        self.view.lockDescText.text = goodsTableData.lockDesc
        self.view.confirmBtn.interactable = false
    else
        self.view.buyNode.gameObject:SetActiveIfNecessary(true)
        self.view.lockNode.gameObject:SetActiveIfNecessary(false)
        self.view.confirmBtn.interactable = true


        local maxBuyCount = math.max(1, math.floor(Utils.getItemCount(info.moneyId) / self.m_curPrice))

        maxBuyCount = math.min(maxBuyCount, info.count, self.view.config.MAX_BUY_COUNT)
        self.view.numberSelector:InitNumberSelector(1, 1, maxBuyCount, function(curNum)
            self:_OnCountChanged(curNum)
        end)
    end

    local resPath = string.format("Assets/Beyond/DynamicAssets/Gameplay/UI/Sprites/%s/%s.png", UIConst.UI_SPRITE_WALLET, moneyItemData.iconId .. "_02")
    if ResourceManager.CheckExists(resPath) then
        local tryIcon2 = self:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId .. "_02")
        if tryIcon2 then
            self.view.moneyIcon.sprite = tryIcon2
            self.view.costMoneyIcon.sprite = tryIcon2
        else
            self.view.moneyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
            self.view.costMoneyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
        end
    else
        self.view.moneyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
        self.view.costMoneyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
    end

    self.view.walletBarPlaceholder:InitWalletBarPlaceholder({ info.moneyId })

    local isCharItem = itemData.type == GEnums.ItemType.Char
    if not isCharItem then
        self.view.stockTxt.text = string.format("%s: %d", Language.LUA_SAFE_AREA_ITEM_COUNT_LABEL, Utils.getItemCount(itemId, true))
    end
    self.view.stockTxt.gameObject:SetActive(not isCharItem)
end

ShopItemPopUpCtrl._OnCountChanged = HL.Method(HL.Number) << function(self, number)
    local totalCost = self.m_curPrice * number
    self.view.costPriceTxt.text = totalCost
    local info = self.m_info
    if Utils.getItemCount(info.moneyId) < totalCost then
        self.view.costPriceTxt.color = self.view.config.COST_COLOR_RED
    else
        self.view.costPriceTxt.color = self.view.config.COST_COLOR_NORMAL
    end
end

ShopItemPopUpCtrl._OnClickConfirm = HL.Method() << function(self)
    local buyCount = self.view.numberSelector.curNumber
    local info = self.m_info
    if Utils.getItemCount(info.moneyId) < self.m_curPrice * buyCount then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SHOP_MONEY_NOT_ENOUGH)
        return
    end

    local inventorySystem = GameInstance.player.inventory
    if inventorySystem:IsPlaceInBag(info.itemId) then

        local itemTableData = Tables.itemTable[info.itemId]
        local stackCount = itemTableData.maxBackpackStackCount
        local totalCount = info.itemCount * buyCount

        local itemBag = inventorySystem.itemBag:GetOrFallback(Utils.getCurrentScope())
        local emptySlotCount = itemBag.slotCount - itemBag:GetUsedSlotCount()
        local sameItemCount = itemBag:GetCount(info.itemId)
        local itemSlotCount = math.ceil(sameItemCount / stackCount)

        local capacity
        if itemSlotCount > 0 then
            capacity = (emptySlotCount + itemSlotCount) * stackCount - sameItemCount
        else
            capacity = stackCount * emptySlotCount
        end

        if capacity < totalCount then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SHOP_BACKPACK_FULL)
            return
        end
    end


    GameInstance.player.shopSystem:BuyGoods(info.shopId, info.goodsId, buyCount)
    Notify(MessageConst.SHOP_WAIT_ANIMATION, true)
end

ShopItemPopUpCtrl.OnBuyItemSucc = HL.Method(HL.Any) << function(self,arg)
    local info = self.m_info
    local itemId = info.itemId
    local itemData = Tables.itemTable[itemId]

    local totalCount = info.itemCount * self.view.numberSelector.curNumber
    local items = {}
    if itemData.maxStackCount <= 1 and Utils.isItemInstType(itemId) then
        for i = 1, totalCount do
            local item = {
                id = itemId,
                count = 1,
            }
            table.insert(items, item)
        end
    else
        items = {
            {
                id = itemId,
                count = totalCount,
            }
        }
    end


    local onComplete = self.m_onComplete
    self:Close()

    if itemData.type == GEnums.ItemType.Char then
        local rewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(CS.Beyond.GEnums.RewardSourceType.Shop)
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            title = Language.LUA_BUY_ITEM_SUCC_TITLE,
            icon = "icon_common_rewards",
            items = items,
            chars = rewardPack.chars,
            onComplete = onComplete
        })
    elseif itemData.type == GEnums.ItemType.Weapon then
        local weapons = {}
        local weapon = {}
        weapon.weaponId = itemId
        weapon.rarity = Tables.itemTable:GetValue(itemId).rarity


        weapon.items = {}
        for _, item in pairs(items) do
            if item.type ~= GEnums.ItemType.Weapon then
                table.insert(weapon.items, item)
            end
        end

        weapon.isNew = Utils.getItemCount(itemId) == 0
        table.insert(weapons, weapon)
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            title = Language.LUA_BUY_ITEM_SUCC_TITLE,
            icon = "icon_common_rewards",
            items = items,
            onComplete = onComplete,
        })
    else
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            title = Language.LUA_BUY_ITEM_SUCC_TITLE,
            icon = "icon_common_rewards",
            items = items,
            onComplete = onComplete,
        })
    end
end

ShopItemPopUpCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.SHOP_WAIT_ANIMATION, false)
    Notify(MessageConst.SHOW_SHOP_ITEM_POP_UP, false)
    Notify(MessageConst.HIDE_ITEM_TIPS)
end
HL.Commit(ShopItemPopUpCtrl)
