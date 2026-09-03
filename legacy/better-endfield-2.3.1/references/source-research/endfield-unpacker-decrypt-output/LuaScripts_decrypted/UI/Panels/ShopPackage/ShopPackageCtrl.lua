
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopPackage

ShopPackageCtrl = HL.Class('ShopPackageCtrl', uiCtrl.UICtrl)

ShopPackageCtrl.m_shopGoodsInfos = HL.Field(HL.Table)

ShopPackageCtrl.m_getCellFunc = HL.Field(HL.Function)


ShopPackageCtrl.m_isControllerTarget = HL.Field(HL.Boolean) << false

ShopPackageCtrl.m_currNaviIndex = HL.Field(HL.Int) << 1

ShopPackageCtrl.m_needResetCurrNavi = HL.Field(HL.Boolean) << false

ShopPackageCtrl.m_currDynamicTagGoodsId = HL.Field(HL.String) << ""


ShopPackageCtrl.m_currSeenRange = HL.Field(HL.Table)

ShopPackageCtrl.m_latestCloseCor = HL.Field(HL.Any)





ShopPackageCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_READ_CASH_SHOP_GOODS] = '_UpdateContent',
    [MessageConst.ON_OPEN_CASH_SHOP_DETAILS] = '_OnOpenDetailsPanel',
    [MessageConst.ON_CLOSE_CASH_SHOP_DETAILS] = '_OnCloseDetailsPanel',
}


ShopPackageCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_shopGoodsInfos = arg.tabData.cashGoodsInfos


    table.sort(self.m_shopGoodsInfos, Utils.genSortFunction({ "soldOutSortValue", "priority" }, true))

    self.view.buyillustrateBtn.onClick:AddListener(function()

        UIManager:Open(PanelId.InstructionBook, "ShopPackage_All")
    end)

    self.view.cashShopKrTips:InitCashShopKrTips()

    self:_InitShortCut()

    if not DeviceInfo.usingTouch then
        self.view.contentScroll:SetSpace(Vector2(25, 40))
    else
        self.view.contentScroll:SetSpace(Vector2(40, 40))
    end

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.contentScroll)
    self.view.contentScroll.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getCellFunc(obj)
        local shopGoodsInfo = self.m_shopGoodsInfos[LuaIndex(index)]
        self:_SetupCellView(cell, shopGoodsInfo)

        if self.m_needResetCurrNavi and LuaIndex(index) == self.m_currNaviIndex then
            self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
            self.m_needResetCurrNavi = false
        end
    end)

    self.m_currSeenRange = {}
    self.view.scroll.onValueChanged:AddListener(function(data)
        self:_UpdateSeeRange()
    end)

    self:SetCurrNaviByGoodsId(arg.naviGoodsId)
    if arg.naviGoodsId ~= nil and not string.isEmpty(arg.naviGoodsId) then
        self.m_needResetCurrNavi = true
        InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, true)
    end

    self:_ComputeCurrDynamicTag()
    self:_SetGoodsCloseRefreshCoroutine()

    self.view.contentScroll:UpdateCount(#self.m_shopGoodsInfos)

    local cashShopCtrl = self.m_phase.cashShopCtrl
    if cashShopCtrl == nil then
        cashShopCtrl = self.m_phase.m_panel2Item[PanelId.CashShop].uiCtrl
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
        self.view.inputGroup.groupId,
        arg.emptyCtrl.view.inputGroup.groupId,
        cashShopCtrl.view.inputGroup.groupId,
    })

    if arg.playAnimationIn then
        self:PlayAnimationIn()
    end
end

ShopPackageCtrl.OnShow = HL.Override() << function(self)
end




ShopPackageCtrl.OnClose = HL.Override() << function(self)
    if self.m_latestCloseCor then
        self:_ClearCoroutine(self.m_latestCloseCor)
        self.m_latestCloseCor = nil
    end
end




ShopPackageCtrl._InitShortCut = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self:BindInputPlayerAction("cashshop_giftpack_goto_left", function()
        InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, false)
        local leftCtrl = self.m_phase.m_panel2Item[PanelId.ShopGiftPackEmpty].uiCtrl
        leftCtrl:NaviTargetCurrTab()
    end, self.view.scrollGroupTarget.groupId)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_left", function()
        self:_OnGoLeft()
    end, self.view.scrollGroupTarget.groupId)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_up", function()
        self:_OnGoUp()
    end, self.view.scrollGroupTarget.groupId)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_right", function()
        self:_OnGoRight()
    end, self.view.scrollGroupTarget.groupId)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_down", function()
        self:_OnGoDown()
    end, self.view.scrollGroupTarget.groupId)

    InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, false)
end

ShopPackageCtrl._SetupCellView = HL.Method(HL.Table, HL.Table) << function(self, cell, shopGoodsInfo)
    local shopGoodsId = shopGoodsInfo.goodsId
    local succ, shopGoodsData = Tables.CashShopGoodsTable:TryGetValue(shopGoodsId)
    if not succ then
        logger.error("找不到商品配置:" .. shopGoodsId)
        return
    end

    local stateCtrl = cell.stateController
    local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(shopGoodsId)
    if canBuy then
        stateCtrl:SetState("Sell")
    else
        stateCtrl:SetState("SellOut")
    end
    if shopGoodsData.goodsType == GEnums.CashGoodsType.MonthlyCard then
        stateCtrl:SetState("MonthlyPass")
        self:_SetupViewMonthlyPass(cell, shopGoodsId)
    elseif not string.isEmpty(self:_GetCellHintText(shopGoodsId)) then
        stateCtrl:SetState("DeadlineNode")
        local hintText = self:_GetCellHintText(shopGoodsId)
        cell.deadlineNode.deadlineTxt.text = hintText
    else
        stateCtrl:SetState("Other")
    end

    self:_SetupCellViewTag(cell, shopGoodsInfo)

    local showDynamicTag = shopGoodsInfo.goodsId == self.m_currDynamicTagGoodsId
    self:_SetupCellDynamicTag(cell, showDynamicTag)

    if shopGoodsData.goodsType == GEnums.CashGoodsType.MonthlyCard then
        if showDynamicTag then
            stateCtrl:SetState("MonthlyUp")
        else
            stateCtrl:SetState("MonthlyDown")
        end
    end

    if not string.isEmpty(self:_GetCellHintText(shopGoodsId)) then
        if showDynamicTag then
            stateCtrl:SetState("MonthlyUp")
        else
            stateCtrl:SetState("MonthlyDown")
        end
    end

    local name = CashShopUtils.GetCashGoodsName(shopGoodsId)
    cell.groupBagNameTxt.text = name
    cell.groupBagNameShadownTxt.text = name
    cell.sellNumberTxt.text = CashShopUtils.getGoodsPriceText(shopGoodsId)

    local path, imageName = CashShopUtils.GetCashGoodsImage(shopGoodsId)
    cell.pattern:LoadSprite(path, imageName)

    local _, cfg = Tables.GiftpackCashShopGoodsDataTable:TryGetValue(shopGoodsId)
    if cfg and not string.isEmpty(cfg.bg) then
        cell.bg:LoadSprite(UIConst.UI_SPRITE_SHOP_GROUP_BAG, cfg.bg)
    end

    if cfg and not string.isEmpty(cfg.anchorCashGoodsId) and not CashShopUtils.IsPS() then
        local anchorPriceStr = CashShopUtils.getGoodsPriceText(cfg.anchorCashGoodsId, true)
        cell.originNumTxt.gameObject:SetActive(true)
        cell.originNumTxt.text = anchorPriceStr
    else
        cell.originNumTxt.gameObject:SetActive(false)
    end

    cell.contentBtn.onClick:RemoveAllListeners()
    cell.contentBtn.onClick:AddListener(function()

        GameInstance.player.cashShopSystem:ReadCashGoods(shopGoodsId)

        EventLogManagerInst:GameEvent_GoodsViewClick(
            "1",
            shopGoodsInfo.cashShopId,
            CashShopConst.CashShopCategoryType.Pack,
            shopGoodsId
        )

        if shopGoodsData.goodsType == GEnums.CashGoodsType.MonthlyCard then
            UIManager:Open(PanelId.ShopMonthlyDetail, {
                goodsId = shopGoodsId,
                goodsInfo = shopGoodsInfo,
            })
        else
            UIManager:Open(PanelId.ShopGiftPackDetails, {
                goodsId = shopGoodsId,
                cashShopId = shopGoodsInfo.cashShopId,
            })
        end
    end)
    cell.contentBtn.customBindingViewLabelText = Language.LUA_CASH_SHOP_PACKAGE_PANEL_BUTTON_BUTTON_KEY_HINT
end


ShopPackageCtrl._SetupCellViewTag = HL.Method(HL.Any, HL.Table) << function(self, cell, shopGoodsInfo)
    local tagWidget = cell.cashShopItemTag
    tagWidget:InitCashShopItemTag({
        isCashShop = true,
        shopGoodsInfo = shopGoodsInfo,
    })
end


ShopPackageCtrl._SetupCellDynamicTag = HL.Method(HL.Any, HL.Boolean, HL.Opt(HL.Boolean))
    << function(self, cell, active, onlyAudio)
    if onlyAudio then
        cell.recommendTag.audioNode.enabled = active
    else
        cell.recommendTag.gameObject:SetActive(active)
        cell.recommendTagTop.gameObject:SetActive(active)
        cell.recommendTag.audioNode.enabled = active
        if active then
            local textId = cell.recommendTag.recommendTxt.textId
            local content = Language[textId]
            cell.recommendTag.bgRecommendColorLong.gameObject:SetActive(string.utf8len(content) >= 5)
            cell.recommendTag.bgRecommendColorShort.gameObject:SetActive(string.utf8len(content) < 5)
        end
    end
end


ShopPackageCtrl._GetCellHintText = HL.Method(HL.Any).Return(HL.String) << function(self, cashGoodsId)
    local _, cfg = Tables.cashShopHintTextTable:TryGetValue(cashGoodsId)
    if cfg then
        return cfg.hintText
    end
    return ""
end

ShopPackageCtrl._SetupViewMonthlyPass = HL.Method(HL.Table, HL.String) << function(self, cell, goodsId)
    local remainValidDays = GameInstance.player.monthlyPassSystem:GetRemainValidDays()
    cell.monthlyPassNode.gameObject:SetActiveIfNecessary(remainValidDays > 0)
    local text = Language.ui_shop_monthlycard_days_remain_count
        .. string.format("<size=200%%> %s </size>", remainValidDays)
        .. Language.ui_shop_monthlycard_days
    cell.dayNumberTxt.text = text
end

ShopPackageCtrl._UpdateContent = HL.Method() << function(self)
    logger.info("ShopPackageCtrl: 收到msg，刷新content")

    self.view.contentScroll:UpdateShowingCells(function(index, obj)
        local cell = self.m_getCellFunc(obj)
        local shopGoodsInfo = self.m_shopGoodsInfos[LuaIndex(index)]
        self:_SetupCellViewTag(cell, shopGoodsInfo)

        local showDynamicTag = shopGoodsInfo.goodsId == self.m_currDynamicTagGoodsId
        self:_SetupCellDynamicTag(cell, showDynamicTag)
    end)
end

ShopPackageCtrl._OnOpenDetailsPanel = HL.Method() << function(self)
    self.view.contentScroll:UpdateShowingCells(function(index, obj)
        local cell = self.m_getCellFunc(obj)

        self:_SetupCellDynamicTag(cell, false, true)
    end)
end

ShopPackageCtrl._OnCloseDetailsPanel = HL.Method() << function(self)
    self.view.contentScroll:UpdateShowingCells(function(index, obj)
        local cell = self.m_getCellFunc(obj)
        local shopGoodsInfo = self.m_shopGoodsInfos[LuaIndex(index)]
        local showDynamicTag = shopGoodsInfo.goodsId == self.m_currDynamicTagGoodsId
        self:_SetupCellDynamicTag(cell, showDynamicTag)
    end)
end

ShopPackageCtrl.TargetFirstCell = HL.Method().Return(HL.Boolean) << function(self)
    logger.info("ShopPackageCtrl: TargetFirstCell")

    local range = self.view.contentScroll:GetShowRange()
    local firstCell = self.m_getCellFunc(self.view.contentScroll:Get(range.x))
    if firstCell then
        InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, true)
        self:SetNaviTarget(firstCell.inputBindingGroupNaviDecorator)
        self.m_currNaviIndex = LuaIndex(range.x)
        self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
        return true
    else
        return false
    end

end

ShopPackageCtrl._OnGoLeft = HL.Method() << function(self)
    logger.info("ShopPackageCtrl: _OnGoLeft")

    local currIndex = self.m_currNaviIndex

    local countPerLine = self.view.contentScroll.countPerLine
    if currIndex and currIndex % countPerLine == 1 then
        InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, false)
        local leftCtrl = self.m_phase.m_panel2Item[PanelId.ShopGiftPackEmpty].uiCtrl
        leftCtrl:NaviTargetCurrTab()
        logger.info("ShopPackageCtrl: _OnGoLeft: NaviTargetCurrTab, currIndex is: " .. currIndex)
    else
        local targetCell = self.m_getCellFunc(self.view.contentScroll:Get(CSIndex(currIndex - 1)))
        self:SetNaviTarget(targetCell.inputBindingGroupNaviDecorator)
        self.m_currNaviIndex = self.m_currNaviIndex - 1
        self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
    end
end

ShopPackageCtrl._OnGoUp = HL.Method() << function(self)
    local countPerLine = self.view.contentScroll.countPerLine
    local targetIndex = self.m_currNaviIndex - countPerLine
    if targetIndex <= 0 then
        return
    end

    local targetCell = self.m_getCellFunc(self.view.contentScroll:Get(CSIndex(targetIndex)))
    self:SetNaviTarget(targetCell.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = targetIndex
    self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
end

ShopPackageCtrl._OnGoRight = HL.Method() << function(self)
    local targetIndex = self.m_currNaviIndex + 1
    if targetIndex > #self.m_shopGoodsInfos then
        return
    end

    local targetCell = self.m_getCellFunc(self.view.contentScroll:Get(CSIndex(targetIndex)))
    self:SetNaviTarget(targetCell.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = targetIndex
    self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
end

ShopPackageCtrl._OnGoDown = HL.Method() << function(self)
    local countPerLine = self.view.contentScroll.countPerLine
    local targetIndex = self.m_currNaviIndex + countPerLine
    if targetIndex > #self.m_shopGoodsInfos then

        local lineNumber = (#self.m_shopGoodsInfos // countPerLine) +
            ((#self.m_shopGoodsInfos % countPerLine > 0) and 1 or 0)
        if self.m_currNaviIndex <= countPerLine * (lineNumber - 1) then
            targetIndex = #self.m_shopGoodsInfos
        else
            return
        end
    end

    local targetCell = self.m_getCellFunc(self.view.contentScroll:Get(CSIndex(targetIndex)))
    self:SetNaviTarget(targetCell.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = targetIndex
    self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
end

ShopPackageCtrl.GetCurrNaviGoodsId = HL.Method().Return(HL.String) << function(self)
    if self.m_currNaviIndex > #self.m_shopGoodsInfos then
        return ""
    else
        return self.m_shopGoodsInfos[self.m_currNaviIndex].goodsId
    end
end

ShopPackageCtrl.SetCurrNaviByGoodsId = HL.Method(HL.String) << function(self, goodsId)
    if goodsId ~= nil then
        for index, info in ipairs(self.m_shopGoodsInfos) do
            if info.goodsId == goodsId then
                self.m_currNaviIndex = index
                return
            end
        end
    end

    self.m_currNaviIndex = 1
end

ShopPackageCtrl._SetSingleGoodsReadByIndex = HL.Method(HL.Number) << function(self, luaIndex)
    local info = self.m_shopGoodsInfos[luaIndex]
    if info ~= nil then
        local goodsId = info.goodsId
        GameInstance.player.cashShopSystem:ReadCashGoods(goodsId)
    end
end


ShopPackageCtrl._ComputeCurrDynamicTag = HL.Method() << function(self)
    if self.m_shopGoodsInfos == nil or #self.m_shopGoodsInfos == 0 then
        self.m_currDynamicTagGoodsId = ""
        return
    end
    local maxDynamicPriorityGoodsInfo = lume.reduce(self.m_shopGoodsInfos, function(max, info)
        if max.soldOutSortValue ~= info.soldOutSortValue then
            return info.soldOutSortValue < max.soldOutSortValue and info or max
        elseif max.dynamicTag ~= info.dynamicTag then
            return info.dynamicTag and info or max
        elseif max.dynamicPriority ~= info.dynamicPriority then
            return (info.dynamicPriority < max.dynamicPriority) and info or max
        else
            return max
        end
    end)

    if maxDynamicPriorityGoodsInfo.soldOutSortValue == 1 or maxDynamicPriorityGoodsInfo.dynamicTag == false then
        self.m_currDynamicTagGoodsId = ""
    else
        self.m_currDynamicTagGoodsId = maxDynamicPriorityGoodsInfo.goodsId
    end
end


ShopPackageCtrl._SetGoodsCloseRefreshCoroutine = HL.Method() << function(self)
    local latestWillCloseInfo = nil
    for _, info in ipairs(self.m_shopGoodsInfos) do

        local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(info.goodsId)
        if canBuy and (info.goodsData.closeTimeStamp > DateTimeUtils.GetCurrentTimestampBySeconds())then
            if latestWillCloseInfo == nil or latestWillCloseInfo.goodsData.closeTimeStamp > info.goodsData.closeTimeStamp then
                latestWillCloseInfo = info
            end
        end
    end

    if latestWillCloseInfo == nil then
        return
    end

    local leftTime = latestWillCloseInfo.goodsData.closeTimeStamp - DateTimeUtils.GetCurrentTimestampBySeconds()
    logger.info("ShopPackageCtrl._SetGoodsCloseRefreshCoroutine: " .. leftTime)
    if self.m_latestCloseCor then
        self:ClearCoroutine(self.m_latestCloseCor)
    end
    self.m_latestCloseCor = self:_StartCoroutine(function()
        coroutine.wait(leftTime + 1)
        Notify(MessageConst.CASH_SHOP_NEW_OPEN_GOODS)
    end)
end

ShopPackageCtrl._UpdateSeeRange = HL.Method() << function(self)
    local range = self.view.contentScroll:GetShowRange()
    local currX = range.x
    local currY = range.y
    if self.m_currSeenRange.x == nil or self.m_currSeenRange.x > currX then
        self.m_currSeenRange.x = currX
    end
    if self.m_currSeenRange.y == nil or self.m_currSeenRange.y < currY then
        self.m_currSeenRange.y = currY
    end
end


ShopPackageCtrl.UpdateSeeGoods = HL.Method(HL.Table) << function(self, seeGoodsId)

    self:_UpdateSeeRange()

    if self.m_currSeenRange.x and self.m_currSeenRange.y then
        local beginIndex = self.m_currSeenRange.x
        local endIndex = self.m_currSeenRange.y
        for i = beginIndex, endIndex do
            local info = self.m_shopGoodsInfos[LuaIndex(i)]
            if info then
                local goodsId = info.goodsId
                if lume.find(seeGoodsId, goodsId) == nil then
                    table.insert(seeGoodsId, goodsId)
                end
            end
        end
    end
    self.m_currSeenRange.x = nil
    self.m_currSeenRange.y = nil
end

HL.Commit(ShopPackageCtrl)
