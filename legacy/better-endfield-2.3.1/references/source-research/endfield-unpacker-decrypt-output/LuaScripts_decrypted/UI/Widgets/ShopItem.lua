local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ShopItem = HL.Class('ShopItem', UIWidgetBase)

ShopItem.m_info = HL.Field(HL.Table)


ShopItem._OnFirstTimeInit = HL.Override() << function(self)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        self:_OnClick()
    end)
    self.view.button.clickHintTextId = "virtual_mouse_shop_buy_item"

    self:RegisterMessage(MessageConst.ON_WALLET_CHANGED, function(msgArg)
        self:UpdateMoney()
    end)
    self:RegisterMessage(MessageConst.ON_ITEM_COUNT_CHANGED, function(msgArg)
        self:UpdateMoney()
    end)
end











ShopItem.InitShopItem = HL.Virtual(HL.Table) << function(self, info)
    self:_FirstTimeInit()

    self.m_info = info

    local itemId = info.itemId
    local itemData = Tables.itemTable[itemId]
    local count = info.itemCount
    local name = itemData.name
    if count > 1 then
        local nameSize = self.view.nameTxt:GetPreferredValues(name)
        local countSize = self.view.countTxt:GetPreferredValues(" x" .. count)
        local totalSize = self.view.nameTxt:GetPreferredValues(name .. " x" .. count)
        local parentSize = self.view.nameTxt.transform.parent.sizeDelta
        if totalSize.x <= parentSize.x and totalSize.y <= self.view.nameTxt.transform.sizeDelta.y then
            self.view.countTxt.gameObject:SetActiveIfNecessary(false)
            self.view.nameTxt.transform.sizeDelta = Vector2(nameSize.x + countSize.x, nameSize.y)
            self.view.nameTxt.text = name .. " x" .. count
        else
            self.view.countTxt.gameObject:SetActiveIfNecessary(true)
            self.view.nameTxt.text = name
            self.view.nameTxt.transform.sizeDelta = Vector2(parentSize.x / 2, 100)

            self.view.countTxt.text = " x" .. count
        end
    else
        self.view.nameTxt.text = itemData.name
        local parentSize = self.view.nameTxt.transform.parent.sizeDelta
        self.view.nameTxt.transform.sizeDelta = Vector2(parentSize.x, parentSize.y)
        self.view.countTxt.gameObject:SetActiveIfNecessary(false)
    end

    self.view.icon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
    if self.view.rarity ~= nil then
        UIUtils.setItemRarityImage(self.view.rarity, itemData.rarity)
    end
    UIUtils.setItemRarityImage(self.view.rarityLine, itemData.rarity)

    count = info.count
    if count ~= nil then
        self.view.remainingTxt.text = string.format(Language.LUA_SHOP_ITEM_REMAIN_FORMAT, count)
        self.view.remainingTxt.gameObject:SetActiveIfNecessary(true)
    else
        self.view.remainingTxt.gameObject:SetActiveIfNecessary(false)
    end

    self.view.soldOutNode.gameObject:SetActive(info.isSoldOut)

    self.view.lockedNode.gameObject:SetActive(info.isLocked)

    local moneyItemData = Tables.itemTable[info.moneyId]
    local resPath = string.format("Assets/Beyond/DynamicAssets/Gameplay/UI/Sprites/%s/%s.png", UIConst.UI_SPRITE_WALLET, moneyItemData.iconId .. "_02")
    if ResourceManager.CheckExists(resPath) then
        local tryIcon2 = self:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId .. "_02")
        if tryIcon2 then
            self.view.moneyIcon.sprite = tryIcon2
        else
            self.view.moneyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
        end
    else
        self.view.moneyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
    end

    self:UpdateMoney()
end

ShopItem._OnClick = HL.Method() << function(self)
    local info = self.m_info
    if info.isSoldOut then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SHOP_GOODS_SOLD_OUT)
        return
    end
    UIManager:Open(PanelId.ShopItemPopUp, {
        info = self.m_info,
        onComplete = function()
            Notify(MessageConst.AFTER_ON_BUY_ITEM_SUCC)
        end
    })
end

ShopItem.PlayLockAnimation = HL.Method() << function(self)
    self.view.lockedNode:Play("shop_lockedout", function()
        self.view.lockedNode:SampleToInAnimationBegin()
        self.view.lockedNode.gameObject:SetActive(false)
        self.m_info.isLocked = false
    end)
end

ShopItem.UpdateMoney = HL.Method() << function(self)
    local info = self.m_info
    local realPrice = info.price
    if info.discount and info.discount < 1 then
        self.view.discount.gameObject:SetActive(true)
        self.view.originalPrice.gameObject:SetActiveIfNecessary(true)
        self.view.discountTxt.text = string.format("-%d<size=60%%>%%</size>", math.floor((1 - info.discount) * 100 + 0.5))
        self.view.originalPrice.text = info.price
        local discount = tonumber(string.format("%.2f", info.discount + 0.001))
        realPrice = math.floor(info.price * discount)
        self.view.price.text = realPrice
    else
        self.view.discount.gameObject:SetActive(false)
        self.view.originalPrice.gameObject:SetActiveIfNecessary(false)
        self.view.price.text = info.price
        realPrice = info.price
    end

    local nowMoney = Utils.getItemCount(info.moneyId)
    if realPrice > nowMoney then
        self.view.price.color = self.view.config.RED_COLOR
    else
        self.view.price.color = self.view.config.NORMAL_COLOR
    end
end

ShopItem.PlaySoldOutAnimation = HL.Method() << function(self)
    self.view.soldOutNode.gameObject:SetActive(true)
    self.view.soldOutNode:Play("shop_soldoutin", function()
        Notify(MessageConst.SHOP_ITEM_SOLD_OUT_ANIMATION_END)
    end)
end

HL.Commit(ShopItem)
return ShopItem
