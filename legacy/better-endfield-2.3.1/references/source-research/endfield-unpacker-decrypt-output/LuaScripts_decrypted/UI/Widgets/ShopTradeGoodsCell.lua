local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ShopTradeGoodsCell = HL.Class('ShopTradeGoodsCell', UIWidgetBase)


local refreshTimeInterval = 1

ShopTradeGoodsCell.m_info = HL.Field(HL.Table)

ShopTradeGoodsCell.m_tickKey = HL.Field(HL.Number) << -1

ShopTradeGoodsCell.m_nextRefreshTime = HL.Field(HL.Number) << 0

ShopTradeGoodsCell.m_selectCount = HL.Field(HL.Number) << 0




ShopTradeGoodsCell._OnFirstTimeInit = HL.Override() << function(self)
end

ShopTradeGoodsCell._OnEnable = HL.Override() << function(self)
    self.view.animationWrapper:PlayInAnimation()
    self:_StartTickRefreshTime()
end

ShopTradeGoodsCell._OnDisable = HL.Override() << function(self)
    self.view.animationWrapper:PlayOutAnimation()
    self:_EndTickRefreshTime()
end

ShopTradeGoodsCell._OnDestroy = HL.Override() << function(self)
    self.view.animationWrapper:ClearTween(false)
    self:_EndTickRefreshTime()
end


ShopTradeGoodsCell.InitShopTradeGoodsCellCommonMode = HL.Method(HL.Table) << function(self, info)
    self.m_info = info
    self:_FirstTimeInit()
    self.view.redDot:InitRedDot("ShopSeeGoodsInfo", { goodsId = info.goodsId })
    self:_UpdateReadInfo(info.goodsId)
    local leftSec = DomainShopUtils.getNextServerRefreshTimeLeftSecByType(info.refreshType)

    local state = "Common"
    self.view.basicStateCtrl:SetState(state)
    self:_RefreshCenterInfo(state)
    self:_SetSelectCount(0)
    self.view.priceStateCtrl:SetState(info.discount == 0 and "Normal" or "HasDiscount")
    self:SetSelectState(false)

    self:_RefreshNextRefreshTime(leftSec)
    self:_RefreshCommonShopRemainLimitCount(info.remainLimitCount)
    self:_RefreshItemUI(info)
    local discountTxt = string.format("-%.0f", info.discount * 100)
    self.view.discountTxt.text = discountTxt
    self.view.currentPriceTxt.text = UIUtils.getNumString(info.curPrice, true)
    self.view.originalPriceTxt.text = UIUtils.getNumString(info.originPrice, true)

    self.view.selectBtn.onClick:RemoveAllListeners()
    self.view.selectBtn.onClick:AddListener(function()
        local goodsData = GameInstance.player.shopSystem:GetShopGoodsData(info.shopId, info.goodsId)
        local uiCtrl = self:GetUICtrl()
        CashShopUtils.OpenShopDetailPanel(goodsData, uiCtrl)
    end)
    self.view.animationWrapper:PlayInAnimation()
end

ShopTradeGoodsCell.InitShopTradeGoodsCellRandomMode = HL.Method(HL.Table, HL.Boolean) << function(self, info, isMyPositionMode)
    self.m_info = info
    self:_FirstTimeInit()
    self.view.redDot:InitRedDot("ShopSeeGoodsInfo", { goodsId = info.goodsId })
    self:_UpdateReadInfo(info.goodsId)
    local curPrice
    local profitRatio
    local basicStateName
    if isMyPositionMode then
        basicStateName = "RandomMyPosition"
        curPrice = info.positionAvgPrice
        profitRatio = info.profitRatio
        self:_RefreshCountdown(0)
    else
        basicStateName = "Random"
        curPrice = info.todayPrice
        profitRatio = info.priceDiffRatio
        local leftSec = DomainShopUtils.getNextServerRefreshTimeLeftSecByType(info.refreshType)
        self:_RefreshNextRefreshTime(leftSec)
    end

    self.view.basicStateCtrl:SetState(basicStateName)
    self:_SetSelectCount(0)
    self.view.priceStateCtrl:SetState("Normal")
    self:SetSelectState(false)

    self:_RefreshRemainLimitCount(info.remainLimitCount)
    self:_RefreshItemUI(info)
    self:_RefreshCenterInfo(basicStateName, profitRatio, info.itemCount)
    self.view.currentPriceTxt.text = UIUtils.getNumString(curPrice, true)

    self.view.selectBtn.onClick:RemoveAllListeners()
    self.view.selectBtn.onClick:AddListener(function()
        local goodsData = GameInstance.player.shopSystem:GetShopGoodsData(info.shopId, info.goodsId)
        UIManager:Open(PanelId.ShopTradeItem, {
            goodsData = goodsData,
            isDefaultSell = isMyPositionMode,
        })
    end)
    self.view.animationWrapper:PlayInAnimation()
end

ShopTradeGoodsCell.InitShopTradeGoodsCellFriendMode = HL.Method(HL.Table) << function(self, info)
    self.m_info = info
    self:_FirstTimeInit()
    self.view.redDot:InitRedDot("ShopSeeGoodsInfo", { goodsId = info.goodsId })
    self:_UpdateReadInfo(info.goodsId)

    local state = "RandomMyPosition"
    self.view.basicStateCtrl:SetState(state)
    self:_SetSelectCount(0)
    self.view.priceStateCtrl:SetState("Normal")
    self:_RefreshCountdown(0)
    self:SetSelectState(false)

    self:_RefreshRemainLimitCount(-1)
    self:_RefreshItemUI(info)
    self:_RefreshCenterInfo(state, info.priceDiffRatio, info.itemCount)
    self.view.currentPriceTxt.text = UIUtils.getNumString(info.todayPrice, true)

    self.view.selectBtn.onClick:RemoveAllListeners()
    self.view.selectBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.ShopTradeItem, {
            goodsData = info.friendGoodsData,
            isSellOnly = true,
        })
    end)
    self.view.animationWrapper:PlayInAnimation()
end

ShopTradeGoodsCell.InitShopTradeGoodsCellBulkSellMode = HL.Method(HL.Table, HL.Number, HL.Function) << function(self, info, luaIndex, onClick)
    self.m_info = info
    self:_FirstTimeInit()
    self.view.redDot:InitRedDot("ShopSeeGoodsInfo", { goodsId = info.goodsId })
    self:_UpdateReadInfo(info.goodsId)

    local state = "RandomMyPosition"
    self.view.basicStateCtrl:SetState(state)
    self:_SetSelectCount(0)
    self.view.priceStateCtrl:SetState("Normal")
    self:_RefreshCountdown(0)
    self:SetSelectState(false)

    self:SetNameBgVisible(false)
    self:_RefreshRemainLimitCount(-1)
    self:_RefreshItemUI(info)
    self:_RefreshCenterInfo(state, info.profitRatio, info.itemCount)
    self.view.currentPriceTxt.text = UIUtils.getNumString(info.todayPrice, true)

    self.view.selectBtn.onClick:RemoveAllListeners()
    self.view.selectBtn.onClick:AddListener(function()
        onClick(luaIndex)
    end)
    InputManagerInst:SetBindingText(self.view.selectBtn.hoverConfirmBindingId, Language.LUA_DOMAIN_SHOP_BULK_SELL_CONFIRM_CELL_KEY_HINT)
    self.view.animationWrapper:PlayInAnimation()
end



ShopTradeGoodsCell.InitCommonShopGoodsCellCommonMode = HL.Method(HL.Table) << function(self, info)

    self:_FirstTimeInit()
    self.m_info = info
    self.view.redDot:InitRedDot("CommonShopSeeGoodsInfo", { goodsId = info.goodsId })
    if not info.isLocked then
        self:_UpdateReadInfo(info.goodsId)
    end
    local leftSec = DomainShopUtils.getNextServerRefreshTimeLeftSecByType(info.refreshType)

    local state = "Common"
    self.view.basicStateCtrl:SetState(state)
    self:_RefreshCenterInfo(state)
    self:_SetSelectCount(0)
    self.view.priceStateCtrl:SetState(info.discount == 0 and "Normal" or "HasDiscount")
    self:_ToggleLock(info.isLocked)
    self:SetSelectState(false)

    self:_RefreshNextRefreshTime(leftSec)
    self:_RefreshCommonShopRemainLimitCount(info.remainLimitCount)
    self:_RefreshItemUI(info)
    self:SetNameBgVisible(false)
    local discountTxt = string.format("-%.0f", info.discount * 100)
    self.view.discountTxt.text = discountTxt
    self.view.currentPriceTxt.text = UIUtils.getNumString(info.curPrice, true)
    self.view.originalPriceTxt.text = UIUtils.getNumString(info.originPrice, true)

    self.view.selectBtn.onClick:RemoveAllListeners()
    self.view.selectBtn.onClick:AddListener(function()
        local goodsData = GameInstance.player.shopSystem:GetShopGoodsData(info.shopId, info.goodsId)
        local uiCtrl = self:GetUICtrl()
        CashShopUtils.OpenShopDetailPanel(goodsData, uiCtrl)
    end)
    InputManagerInst:SetBindingText(self.view.selectBtn.hoverConfirmBindingId, Language.LUA_SHOP_SELECT_ITEM)
    if info.refreshTag == "normal" then
        self.view.animationWrapper:PlayInAnimation()
    end
end





ShopTradeGoodsCell._StartTickRefreshTime = HL.Method() << function(self)
    self.m_tickKey = LuaUpdate:Remove(self.m_tickKey)
    self.m_tickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        if not self.m_info then
            return
        end
        if Time.time >= self.m_nextRefreshTime then
            self.m_nextRefreshTime = Time.time + refreshTimeInterval
            local leftSec = DomainShopUtils.getNextServerRefreshTimeLeftSecByType(self.m_info.refreshType)
            self:_RefreshNextRefreshTime(leftSec)
        end
    end)
end

ShopTradeGoodsCell._EndTickRefreshTime = HL.Method() << function(self)
    self.m_tickKey = LuaUpdate:Remove(self.m_tickKey)
end

ShopTradeGoodsCell._RefreshNextRefreshTime = HL.Method(HL.Number) << function(self, leftSec)
    self:_RefreshCountdown(leftSec)
end

ShopTradeGoodsCell._RefreshCommonShopRemainLimitCount = HL.Method(HL.Number) << function(self, remainLimitCount)
    if remainLimitCount < 0 then
        if remainLimitCount == -1 then
            self.view.limitNumberNode.gameObject:SetActive(true)
            self.view.limitCountTxt.text = "∞"
            self.view.limitCountTxt.color = self.view.config.INVENTORY_NORMAL_COLOR
            self:_ToggleSoldOut(false)
        else
            self.view.limitNumberNode.gameObject:SetActive(false)
            self:_ToggleSoldOut(true)
        end
    else
        self.view.limitNumberNode.gameObject:SetActive(true)
        self.view.limitCountTxt.text = remainLimitCount
        if remainLimitCount == 0 then
            self.view.limitCountTxt.color = self.view.config.INVENTORY_RED_COLOR
        else
            self.view.limitCountTxt.color = self.view.config.INVENTORY_NORMAL_COLOR
        end

        self:_ToggleSoldOut(remainLimitCount == 0 and true or false)
    end
end


ShopTradeGoodsCell._RefreshRemainLimitCount = HL.Method(HL.Number) << function(self, remainLimitCount)
    if remainLimitCount < 0 then
        self.view.limitNumberNode.gameObject:SetActive(false)
        self:_ToggleSoldOut(false)
    else
        self.view.limitNumberNode.gameObject:SetActive(true)
        self.view.limitCountTxt.text = remainLimitCount
        if remainLimitCount == 0 then
            self.view.limitCountTxt.color = self.view.config.INVENTORY_RED_COLOR
        else
            self.view.limitCountTxt.color = self.view.config.INVENTORY_NORMAL_COLOR
        end
        self:_ToggleSoldOut(remainLimitCount == 0 and true or false)
    end
end

ShopTradeGoodsCell._RefreshItemUI = HL.Method(HL.Table) << function(self, info)
    self.view.itemIcon:InitItemIcon(info.itemId, true)
    self:_RefreshItemBundleCount(info.itemBundleCount)

    self.view.moneyIconImg:LoadSprite(UIConst.UI_SPRITE_WALLET, info.moneyIcon)
    self.view.rarityImg.color = UIUtils.getItemRarityColor(info.itemRarity)
    self.view.itemNameTxt.text = info.itemName

    local itemData = Tables.itemTable[info.itemId]
    if itemData and itemData.type == GEnums.ItemType.PhotoAnim then
        local isStatic = DomainShopUtils.IsPhotoAnimActionStatic(info.itemId)
        self.view.actionIcon.gameObject:SetActive(not isStatic)
    else
        self.view.actionIcon.gameObject:SetActive(false)
    end
end

ShopTradeGoodsCell.SetSelectCount = HL.Method(HL.Number) << function(self, count)

    self:_SetSelectCount(count)
end

ShopTradeGoodsCell._UpdateReadInfo = HL.Method(HL.String) << function(self, goodsId)
    GameInstance.player.shopSystem:RecordSeeGoodsId(goodsId)
end

ShopTradeGoodsCell.SetNameBgVisible = HL.Method(HL.Boolean) << function(self, visible)
    self.view.nameBg.gameObject:SetActive(visible)
end

ShopTradeGoodsCell.SetSelectState = HL.Method(HL.Boolean) << function(self, isSelect)
    if isSelect then
        self:_ToggleSelectBorder(true)
        if self.m_selectCount == 0 then
            InputManagerInst:SetBindingText(self.view.selectBtn.hoverConfirmBindingId, Language.LUA_DOMAIN_SHOP_BULK_SELL_CONFIRM_CELL_KEY_HINT)
        else
            InputManagerInst:SetBindingText(self.view.selectBtn.hoverConfirmBindingId, Language.LUA_DOMAIN_SHOP_BULK_SELL_CANCEL_CELL_KEY_HINT)
        end
    else
        self:_ToggleSelectBorder(false)
        InputManagerInst:SetBindingText(self.view.selectBtn.hoverConfirmBindingId, Language.LUA_DOMAIN_SHOP_BULK_SELL_CONFIRM_CELL_KEY_HINT)
    end
end


ShopTradeGoodsCell._ToggleLock = HL.Method(HL.Boolean) << function(self, active)
    if not self.view.lock then
        if not active then
            self.view.tagList:SetState("NotLock")
            return
        end
        local prefab = LuaSystemManager.shopTradeGoodsCellPrefabSystem.lockNodePrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.lockParent.transform)
        self.view.lock = obj.transform
    end
    self.view.tagList:SetState(active and "Lock" or "NotLock")
    self.view.lock.gameObject:SetActiveIfNecessary(active)
end

ShopTradeGoodsCell._ToggleSoldOut = HL.Method(HL.Boolean) << function(self, active)
    if not self.view.soldOut then
        if not active then
            self.view.tagList:SetState("NotSellOut")
            return
        end
        local prefab = LuaSystemManager.shopTradeGoodsCellPrefabSystem.soldOutPrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.lockParent.transform)
        self.view.soldOut = obj.transform
    end
    self.view.tagList:SetState(active and "SellOut" or "NotSellOut")
    self.view.soldOut.gameObject:SetActiveIfNecessary(active)
end

ShopTradeGoodsCell._SetSelectCount = HL.Method(HL.Number) << function(self, count)
    self.m_selectCount = count
    if not self.view.bulkOperation then
        if count == 0 then
            return
        end
        local prefab = LuaSystemManager.shopTradeGoodsCellPrefabSystem.bulkOperationPrefab
        local borderPrefab = LuaSystemManager.shopTradeGoodsCellPrefabSystem.bulkBorderPrefab
        if not prefab or not borderPrefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.bulkOperationRoot.transform)
        self.view.bulkOperation = Utils.wrapLuaNode(obj)
        local borderObj = CSUtils.CreateObject(borderPrefab, self.view.animRoot.transform)
        borderObj.transform:SetAsFirstSibling()
        self.view.bulkBorder = borderObj.transform
    end
    local showCount = count ~= 0
    self.view.bulkOperation.gameObject:SetActiveIfNecessary(showCount)
    self.view.bulkBorder.gameObject:SetActiveIfNecessary(showCount)
    if showCount then
        self.view.bulkOperation.selectCountTxt.text = count
    end
end

ShopTradeGoodsCell._ToggleSelectBorder = HL.Method(HL.Boolean) << function(self, active)
    if not self.view.selectBorder then
        if not active then
            return
        end
        local prefab = LuaSystemManager.shopTradeGoodsCellPrefabSystem.selectBorderPrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.animRoot.transform)
        self.view.selectBorder = obj.transform
    end
    self.view.selectBorder.gameObject:SetActiveIfNecessary(active)
end

ShopTradeGoodsCell._RefreshCenterInfo = HL.Method(HL.String, HL.Opt(HL.Number, HL.Number)) << function(self, state, ratio, ownCount)
    local active = state ~= "Common"
    if not self.view.centerInfo then
        if not active then
            return
        end
        local prefab = LuaSystemManager.shopTradeGoodsCellPrefabSystem.centerInfoPrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.animRoot.transform)
        obj.transform:SetSiblingIndex(self.view.lockParent.transform:GetSiblingIndex() - 1)
        self.view.centerInfo = Utils.wrapLuaNode(obj)
    end
    self.view.centerInfo.gameObject:SetActiveIfNecessary(active)
    if active then
        self.view.centerInfo.simpleStateController:SetState(state)
        if ratio then
            self.view.centerInfo.profitRatioTxt.text = math.abs(ratio)
            self.view.centerInfo.profitArrow:SetState(DomainShopUtils.getProfitArrowStateName(ratio))
        end
        if ownCount then
            self.view.centerInfo.myPositionCountTxt.text = UIUtils.getNumString(ownCount)
        end
    end
end

ShopTradeGoodsCell._RefreshItemBundleCount = HL.Method(HL.Number) << function(self, count)
    local active = count > 1
    if not self.view.itemBundleCount then
        if not active then
            return
        end
        local prefab = LuaSystemManager.shopTradeGoodsCellPrefabSystem.itemBundleCountPrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.animRoot.transform)
        obj.transform:SetSiblingIndex(self.view.lockParent.transform:GetSiblingIndex() - 1)
        self.view.itemBundleCount = Utils.wrapLuaNode(obj)
    end
    self.view.itemBundleCount.gameObject:SetActiveIfNecessary(active)
    if active then
        self.view.itemBundleCount.bundleCountTxt.text = UIUtils.getNumString(count)
    end
end

ShopTradeGoodsCell._RefreshCountdown = HL.Method(HL.Number) << function(self, leftSec)
    local active = leftSec > 0
    if not self.view.countDown then
        if not active then
            return
        end
        local prefab = LuaSystemManager.shopTradeGoodsCellPrefabSystem.countdownPrefab
        if not prefab then
            return
        end
        local obj = CSUtils.CreateObject(prefab, self.view.leftTopLayout.transform)
        obj.transform:SetAsFirstSibling()
        self.view.countDown = Utils.wrapLuaNode(obj)
    end
    self.view.countDown.gameObject:SetActiveIfNecessary(active)
    if active then
        self.view.countDown.refreshTimeTxt.text = UIUtils.getShortLeftTime(leftSec)
        if leftSec <= Const.SEC_PER_DAY then
            self.view.countDown.stateController:SetState("StrongWarning")
        elseif leftSec <= (Const.SEC_PER_DAY * 3) then
            self.view.countDown.stateController:SetState("Warning")
        else
            self.view.countDown.stateController:SetState("Normal")
        end
    end
end

HL.Commit(ShopTradeGoodsCell)
return ShopTradeGoodsCell

