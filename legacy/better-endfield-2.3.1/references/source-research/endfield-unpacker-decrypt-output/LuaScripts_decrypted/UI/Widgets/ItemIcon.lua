local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemIcon = HL.Class('ItemIcon', UIWidgetBase)


ItemIcon._OnFirstTimeInit = HL.Override() << function(self)
end

ItemIcon.m_itemId = HL.Field(HL.Any)

ItemIcon.m_instId = HL.Field(HL.Any)

ItemIcon.showRarity = HL.Field(HL.Boolean) << true

ItemIcon.InitItemIcon = HL.Method(HL.Opt(HL.String, HL.Boolean, HL.Number)) << function(self, itemId, isBig, instId)
    self:_FirstTimeInit()

    if self.m_itemId == itemId and self.m_instId == instId then
        return
    end

    self.m_itemId = itemId
    self.m_instId = instId
    local itemData = Tables.itemTable[itemId]
    self.view.icon:LoadSprite(isBig and UIConst.UI_SPRITE_ITEM_BIG or UIConst.UI_SPRITE_ITEM, itemData.iconId)
    self:_RefreshGemAddOnNode(isBig == true, itemData, instId)
    self:_UpdateLiquidOrGasIcon()

    local compositeId = itemData.iconCompositeId
    if string.isEmpty(compositeId) then
        self.showRarity = true
        self.view.bg.gameObject:SetActiveIfNecessary(false)
        self.view.mark.gameObject:SetActiveIfNecessary(false)
        self:_UpdateTrans()
    else
        local compositeData = Tables.itemIconCompositeTable[compositeId]
        self.showRarity = compositeData.showRarity
        local bg
        if compositeData.bgIcons.Count >= itemData.rarity then
            bg = compositeData.bgIcons[itemData.rarity - 1]
        end
        if string.isEmpty(bg) then
            self.view.bg.gameObject:SetActiveIfNecessary(false)
        else
            self.view.bg.gameObject:SetActiveIfNecessary(true)
            self.view.bg:LoadSprite(isBig and UIConst.UI_SPRITE_ITEM_COMPOSITE_DECO_BIG or UIConst.UI_SPRITE_ITEM_COMPOSITE_DECO, bg)
        end
        local mark = compositeData.markIcon
        if string.isEmpty(mark) then
            self.view.mark.gameObject:SetActiveIfNecessary(false)
        else
            self.view.mark.gameObject:SetActiveIfNecessary(true)
            self.view.mark:LoadSprite(isBig and UIConst.UI_SPRITE_ITEM_COMPOSITE_DECO_BIG or UIConst.UI_SPRITE_ITEM_COMPOSITE_DECO, mark)
        end
        self:_UpdateTrans(compositeData.iconTransType)
    end
end

ItemIcon._UpdateTrans = HL.Method(HL.Opt(GEnums.ItemIconTransType)) << function(self, transType)
    local trans = self.view.icon.transform
    if transType == GEnums.ItemIconTransType.Formula then
        trans.localScale = Vector3.one * self.view.config.FORMULA_ICON_SCALE
        trans.pivot = self.view.config.FORMULA_ICON_PIVOT
        trans.anchoredPosition = Vector2.zero
    else
        trans.localScale = Vector3.one
        trans.pivot = Vector2.one / 2
        trans.anchoredPosition = Vector2.zero
    end
    if transType == GEnums.ItemIconTransType.ItemBigGem then
        if self.view.gemAttrIcon then
            self.view.gemAttrIcon.transform.anchoredPosition3D = self.view.config.ITEM_BIG_GEM_ICON_STANDARD_OFFSET * trans.rect.width / 100
        else
            trans.anchoredPosition3D = self.view.config.ITEM_BIG_GEM_ICON_STANDARD_OFFSET * trans.rect.width / 100
        end
    end
end

local DEFAULT_GEM_SPRITE = "icon_wpngem_00"
ItemIcon._RefreshGemAddOnNode = HL.Method(HL.Boolean, HL.Any, HL.Opt(HL.Number)) << function(self, isBig, itemData, instId)
    local gemAttrIconPath
    if instId and instId > 0 then
        local itemType = itemData.type
        if itemType == GEnums.ItemType.WeaponGem then
            local leadTermId = CharInfoUtils.getGemLeadSkillTermId(instId)
            local leadTermCfg
            local hasTermIcon = false
            if leadTermId then
                leadTermCfg = Tables.gemTable:GetValue(leadTermId)
            end
            hasTermIcon = leadTermCfg and not string.isEmpty(leadTermCfg.tagIcon)
            gemAttrIconPath = hasTermIcon and leadTermCfg.tagIcon or DEFAULT_GEM_SPRITE
        end
    end

    if not self.view.gemAttrIcon then
        if not gemAttrIconPath then
            return
        end
        local obj = CSUtils.CreateObject(LuaSystemManager.itemPrefabSystem.gemAttrIconPrefab, self.view.transform)
        obj.name = "GemAttrIcon"
        obj.transform.localScale = Vector3.one
        obj.transform.pivot = Vector2.one / 2
        obj.transform.anchorMin = Vector2.zero
        obj.transform.anchorMax = Vector2.one
        obj.transform.offsetMin = Vector2.zero
        obj.transform.offsetMax = Vector2.zero
        obj.transform.anchoredPosition3D = Vector3.zero
        self.view.gemAttrIcon = obj:GetComponent("UIImage")
    end

    if gemAttrIconPath then
        self.view.gemAttrIcon.transform.anchoredPosition3D = Vector3.zero
        self.view.gemAttrIcon.gameObject:SetActive(true)
        self.view.gemAttrIcon:LoadSprite(isBig and UIConst.UI_SPRITE_ITEM_BIG or UIConst.UI_SPRITE_ITEM, gemAttrIconPath)
    else
        self.view.gemAttrIcon.gameObject:SetActive(false)
    end
end

ItemIcon.SetAlpha = HL.Method(HL.Number) << function(self, alpha)
    self.view.canvasGroup.alpha = alpha
end

ItemIcon._UpdateLiquidOrGasIcon = HL.Method() << function(self)

    local icon
    local isFullBottle, fullBottleData = Tables.fullBottleTable:TryGetValue(self.m_itemId)
    if isFullBottle then
        local liquidData = Tables.itemTable[fullBottleData.liquidId]
        icon = liquidData.iconId
    else
        isFullBottle, fullBottleData = Tables.fullGasJarTable:TryGetValue(self.m_itemId)
        if isFullBottle then
            local gasData = Tables.itemTable[fullBottleData.gasId]
            icon = gasData.iconId
        end
    end
    if not self.view.liquidIcon then
        if not icon then
            return
        end
        local obj = CSUtils.CreateObject(LuaSystemManager.itemPrefabSystem.liquidIconPrefab, self.view.transform)
        obj.name = "LiquidIcon"
        obj.transform.localScale = Vector3.one
        local center = Vector2.one / 2
        obj.transform.pivot = center
        obj.transform.anchorMin = center
        obj.transform.anchorMax = center
        obj.transform.anchoredPosition = Vector2.zero

        local size = 80 * self.view.transform.rect.width / 180
        obj.transform.sizeDelta = Vector2(size, size)
        self.view.liquidIcon = obj:GetComponent("UIImage")
    end
    if icon then
        self.view.liquidIcon.gameObject:SetActive(true)
        self.view.liquidIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, icon)
    else
        self.view.liquidIcon.gameObject:SetActive(false)
    end
end

HL.Commit(ItemIcon)
return ItemIcon
