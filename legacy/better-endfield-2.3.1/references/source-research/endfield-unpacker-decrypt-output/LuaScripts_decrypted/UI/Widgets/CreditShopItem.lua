local ShopItemWidget = require_ex('UI/Widgets/ShopItem')

CreditShopItem = HL.Class('CreditShopItem', ShopItemWidget)

CreditShopItem.InitShopItem = HL.Override(HL.Table) << function(self, info)
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

            self.view.countTxt.text = count
        end
        self.view.layout.gameObject:SetActiveIfNecessary(true)
    else
        self.view.nameTxt.text = itemData.name
        local parentSize = self.view.nameTxt.transform.parent.sizeDelta
        self.view.nameTxt.transform.sizeDelta = Vector2(parentSize.x, parentSize.y)
        self.view.countTxt.gameObject:SetActiveIfNecessary(false)
        self.view.layout.gameObject:SetActiveIfNecessary(false)
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


HL.Commit(CreditShopItem)
return CreditShopItem
