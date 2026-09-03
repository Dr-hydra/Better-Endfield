local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CashShopItemTag = HL.Class('CashShopItemTag', UIWidgetBase)


CashShopItemTag._OnFirstTimeInit = HL.Override() << function(self)

end

CashShopItemTag.m_goodsData = HL.Field(HL.Any)

CashShopItemTag.m_arg = HL.Field(HL.Any)

CashShopItemTag.m_targetTime = HL.Field(HL.Number) << 0

local TAGS = {"newNode", "discountNode", "restrictionNode", "timeNode"}

CashShopItemTag.InitCashShopItemTag = HL.Method(HL.Any) << function(self, arg)
    self:_FirstTimeInit()

    CoroutineManager:ClearAllCoroutine(self)
    self.m_targetTime = 0

    self.m_arg = arg

    if arg.isShop then
        local goodsData = arg.goodsData
        self.m_goodsData = goodsData
        local hideRemainCount = arg.hideRemainCount
        self:_SetupUIShopGoods(goodsData, hideRemainCount)
    end

    if arg.isCashShop then
        self:_SetupUICashShopGoods(arg.shopGoodsInfo)
    end
end


CashShopItemTag._SetupUIShopGoods = HL.Method(HL.Any, HL.Opt(HL.Boolean)) << function(self, goodsData, hideRemainCount)
    local goodsTemplateId = goodsData.goodsTemplateId
    local hasCfg, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsTemplateId)
    local isGachaGoodsAndHideLeftTime = false
    if hasCfg and not string.isEmpty(goodsCfg.weaponGachaPoolId) then
        local isRealTime, _ = CashShopUtils.GetGachaWeaponPoolCloseTimeInfo(goodsCfg.weaponGachaPoolId)
        isGachaGoodsAndHideLeftTime = not isRealTime
    end

    if isGachaGoodsAndHideLeftTime then
        self:_SetTime(false)
    else
        local leftTime = GameInstance.player.shopSystem:GetWeaponGoodsTimeLimit(goodsData)
        if leftTime > -1 then
            self:UpdateTime()
            self:_StartCoroutine(function()
                coroutine.wait(1)
                self:UpdateTime()
            end)
        else

            leftTime = goodsData.closeTimeStamp - DateTimeUtils.GetCurrentTimestampBySeconds()
            if leftTime > 0 then
                self.m_targetTime = goodsData.closeTimeStamp
                self:UpdateTime()
                self:_StartCoroutine(function()
                    coroutine.wait(1)
                    self:UpdateTime()
                end)
            else
                self:_SetTime(false)
            end
        end
    end

    if hideRemainCount then
        self:_SetRestriction(false)
    else
        local limitBuy = GameInstance.player.shopSystem:GetRemainCountByGoodsId(goodsData.shopId, goodsTemplateId)
        if limitBuy >= 0 then
            self:_SetRestriction(true, Language.ui_shop_token_stock, tostring(limitBuy))
        elseif limitBuy ==-1 then
            self:_SetRestriction(true, Language.ui_shop_token_stock, "∞")
        else
            self:_SetRestriction(false)
        end
    end

    if goodsData.discount and goodsData.discount < 1 then
        self:_SetDiscount(true, string.format("-%d", math.floor((1 - goodsData.discount) * 100 + 0.5)))
    else
        self:_SetDiscount(false)
    end

    local isNew = GameInstance.player.shopSystem:IsNewGoodsId(self.m_goodsData.goodsId)
    local hideNew = self.m_arg.hideNew or false
    self:_ToggleNew(isNew and not hideNew)
end


CashShopItemTag._SetupUICashShopGoods = HL.Method(HL.Any) << function(self, goodsInfo)
    self:_SetAllTagInactive()
    local goodsId = goodsInfo.goodsId
    local goodsData = goodsInfo.goodsData

    local closeTimeStamp = goodsData.closeTimeStamp
    self.m_targetTime = closeTimeStamp
    local leftTime = closeTimeStamp - DateTimeUtils.GetCurrentTimestampBySeconds()
    local hideTime = self.m_arg.hideTime or false
    if closeTimeStamp ~= 0 and leftTime > -1 and not hideTime then
        self:UpdateTimeByTargetTs()
        self:_StartCoroutine(function()
            coroutine.wait(1)
            self:UpdateTimeByTargetTs()
        end)
    else
        self:_SetTime(false)
    end


    local limitGoodsData = GameInstance.player.cashShopSystem:GetPlatformLimitGoodsData(goodsId)
    local hideRestriction = self.m_arg.hideRestriction or false
    if limitGoodsData ~= nil and
        limitGoodsData.limitType == CS.Beyond.Gameplay.CashShopSystem.EPlatformLimitGoodsType.Common and
        not hideRestriction then
        local _, cfg = Tables.giftpackCashShopGoodsDataTable:TryGetValue(goodsId)
        local limitCount = limitGoodsData.limitCount
        local purchaseCount = limitGoodsData.purchaseCount
        local remain = tostring(limitCount - purchaseCount)
        if cfg ~= nil then
            local text = CashShopUtils.GetRestrictionTagTextByLimitType(cfg.availRefresh)
            self:_SetRestriction(true, text, remain)
        else
            self:_SetRestriction(true, nil, remain)
        end
    else
        self:_SetRestriction(false)
    end


    self:_SetDiscount(false)

    local succ, giftpackGoodsData = Tables.GiftpackCashShopGoodsDataTable:TryGetValue(goodsId)
    if succ then
        local tagList = giftpackGoodsData.tagList
        for _, tagId in pairs(tagList) do
            local tagData = Tables.CashShopGiftPackTagTable[tagId]
            local style = tagData.style
            local value = tagData.value
            self:_SetTag(style, value)
        end
    end


    local isNew = GameInstance.player.cashShopSystem:IsNewGoods(goodsId)
    self:_ToggleNew(isNew)
end

CashShopItemTag.UpdateTime = HL.Method() << function(self)
    local goodsData = self.m_goodsData
    local leftTime = GameInstance.player.shopSystem:GetWeaponGoodsTimeLimit(goodsData)
    if leftTime <= -1 and self.m_targetTime ~= 0 then
        leftTime = self.m_targetTime - DateTimeUtils.GetCurrentTimestampBySeconds()
    end

    if leftTime > -1 then
        local stateName
        if leftTime > 3600 * 24 * 3 then
            stateName = "Green"
        elseif leftTime <= 3600 * 24 * 3 and leftTime > 3600 * 24 then
            stateName = "Yellow"
        else
            stateName = "Red"
        end
        self:_SetTime(true, stateName, UIUtils.getShortLeftTime(leftTime))
    else
        self:_SetTime(false)
    end
end

CashShopItemTag.UpdateTimeByTargetTs = HL.Method() << function(self)
    local closeTimeStamp = self.m_targetTime
    local leftTime = closeTimeStamp - DateTimeUtils.GetCurrentTimestampBySeconds()
    if leftTime > -1 then
        local stateName
        if leftTime > 3600 * 24 * 3 then
            stateName = "Green"
        elseif leftTime <= 3600 * 24 * 3 and leftTime > 3600 * 24 then
            stateName = "Yellow"
        else
            stateName = "Red"
        end
        self:_SetTime(true, stateName, UIUtils.getShortLeftTime(leftTime))
    else
        self:_SetTime(false)
    end
end

CashShopItemTag._SetAllTagInactive = HL.Method() << function(self)
    local left = self.view.tagLeft.transform
    local right = self.view.tagRight.transform
    for i = 0, left.childCount - 1 do
        left:GetChild(i).gameObject:SetActive(false)
    end
    for i = 0, right.childCount - 1 do
        right:GetChild(i).gameObject:SetActive(false)
    end
end

CashShopItemTag._ToggleNew = HL.Method(HL.Boolean) << function(self, active)
    if not self.view.newNode then
        if not active then
            return
        end
        local prefab = LuaSystemManager.cashShopItemPrefabSystem.newNodePrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.tagRight.transform)
        obj.transform:SetAsFirstSibling()
        self.view.newNode = Utils.wrapLuaNode(obj)
    end
    self.view.newNode.gameObject:SetActive(active)
end

CashShopItemTag._SetDiscount = HL.Method(HL.Boolean, HL.Opt(HL.String)) << function(self, active, discount)
    if not self.view.discountNode then
        if not active then
            return
        end
        local prefab = LuaSystemManager.cashShopItemPrefabSystem.discountPrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.tagRight.transform)
        self.view.discountNode = Utils.wrapLuaNode(obj)
    end
    self.view.discountNode.gameObject:SetActive(active)
    if active then
        self.view.discountNode.txtDiscount.text = discount
    end
end

CashShopItemTag._SetRestriction = HL.Method(HL.Boolean, HL.Opt(HL.String, HL.String)) << function(self, active, text, numText)
    if not self.view.restrictionNode then
        if not active then
            return
        end
        local prefab = LuaSystemManager.cashShopItemPrefabSystem.restrictionPrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.tagLeft.transform)
        self.view.restrictionNode = Utils.wrapLuaNode(obj)
    end
    self.view.restrictionNode.gameObject:SetActive(active)
    if active then
        if text then
            self.view.restrictionNode.shopRestrictionText.text = text
        end
        if numText then
            self.view.restrictionNode.shopRestrictionNumText.text = numText
        end
    end
end

CashShopItemTag._SetTime = HL.Method(HL.Boolean, HL.Opt(HL.String, HL.String)) << function(self, active, state, time)
    if not self.view.timeNode then
        if not active then
            return
        end
        local prefab = LuaSystemManager.cashShopItemPrefabSystem.timePrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.tagLeft.transform)
        obj.transform:SetAsFirstSibling()
        self.view.timeNode = Utils.wrapLuaNode(obj)
    end
    self.view.timeNode.gameObject:SetActive(active)
    if active then
        self.view.timeNode.stateController:SetState(state)
        self.view.timeNode.txtTime.text = time
    end
end

CashShopItemTag._SetTag = HL.Method(HL.String, HL.Opt(HL.String)) << function(self, tag, text)
    local tagCell = self.view[tag]
    if tagCell == nil then
        local prefab = LuaSystemManager.cashShopItemPrefabSystem[tag]
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.tagRight.transform)
        self.view[tag] = Utils.wrapLuaNode(obj)
        tagCell = self.view[tag]
    end
    if tagCell ~= nil then
        tagCell.gameObject:SetActive(true)

        local haveValue = not string.isEmpty(text)
        local tagText = tagCell.tagText
        local line = tagCell.lineImg
        if tagText ~= nil then
            tagText.gameObject:SetActive(haveValue)
            tagText.text = text
        end
        if line ~= nil then
            line.gameObject:SetActive(haveValue)
        end
    end
end

CashShopItemTag._SetDisable = HL.Method(HL.Boolean) << function(self, disable)
    local state = disable and "Disable" or "Normal"
    for _, tag in ipairs(TAGS) do
        local cell = self.view[tag]
        if cell ~= nil and cell.stateController ~= nil then
            cell.stateController:SetState(state)
        end
    end
    for _, tagInfo in pairs(Tables.CashShopGiftPackTagTable) do
        local cell = self.view[tagInfo.style]
        if cell ~= nil and cell.stateController ~= nil then
            cell.stateController:SetState(state)
        end
    end
end

HL.Commit(CashShopItemTag)
return CashShopItemTag

