
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopToken

local TAB_CONFIG = {

    shop_pay_green_1 = {
        icon = "item_gachabyproducts_weaponticket",
        checkPotentialExchangeRedDot = true,
    },

    shop_pay_yellow_1 = {
        icon = "item_gachabyproducts_charticket",
    },

    shop_pay_purple_1 = {
        icon = "item_gachabyproducts_potentialticket",
    },
}

ShopTokenCtrl = HL.Class('ShopTokenCtrl', uiCtrl.UICtrl)






ShopTokenCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SHOP_REFRESH] = '_OnShopRefresh',
    [MessageConst.ON_BUY_ITEM_SUCC] = '_OnBuyItemSucc',
    [MessageConst.ON_SHOP_GOODS_CONDITION_REFRESH] = '_OnShopRefresh',
}

ShopTokenCtrl.m_getItemCell = HL.Field(HL.Function)

ShopTokenCtrl.m_getTabCell = HL.Field(HL.Function)

ShopTokenCtrl.m_goodsDataList = HL.Field(HL.Userdata)

ShopTokenCtrl.m_shopSystem = HL.Field(HL.Userdata)

ShopTokenCtrl.m_shopIdToTabCell = HL.Field(HL.Table)

ShopTokenCtrl.m_shopDataList = HL.Field(HL.Table)

ShopTokenCtrl.m_currShopId = HL.Field(HL.String) << ""


ShopTokenCtrl.m_currNaviIndex = HL.Field(HL.Int) << 0

ShopTokenCtrl.m_isNaviLeft = HL.Field(HL.Boolean) << true

ShopTokenCtrl.m_needSetNaviTargetGoodsId = HL.Field(HL.String) << ""


ShopTokenCtrl.m_currSeenRange = HL.Field(HL.Table)


ShopTokenCtrl.m_haveSeenGoodsId = HL.Field(HL.Table)

ShopTokenCtrl.m_showExchange = HL.Field(HL.Boolean) << false



ShopTokenCtrl.m_pendingAfterTopOrdered = HL.Field(HL.Table)


ShopTokenCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_shopSystem = GameInstance.player.shopSystem
    self:_InitUICallback()
    self:_InitShortCut()
    self:_InitContentList()
    self:_InitTabList()
    self:_ProcessArg(arg)

    self.m_phase:HidePsStore()
end

ShopTokenCtrl.OnShow = HL.Override() << function(self)
    if DeviceInfo.usingController then
        InputManagerInst:ToggleGroup(self.view.verticalTabList.groupTarget.groupId, self.m_isNaviLeft)
        InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, not self.m_isNaviLeft)
    end

    self:UpdateAll()

    if self.m_phase.m_needGameEvent then
        self.m_phase.m_needGameEvent = false
        EventLogManagerInst:GameEvent_ShopEnter(
            self.m_phase.m_enterButton,
            self.m_phase.m_enterPanel,
            "",
            CashShopConst.CashShopCategoryType.Token,
            ""
        )
    end
end

ShopTokenCtrl.OnClose = HL.Override() << function(self)
    self:_UpdateSeeGoods()
    self:_SetCurrGoodsRead()
end

ShopTokenCtrl._InitShortCut = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self:BindInputPlayerAction("cashshop_giftpack_goto_right", function()
        self:_OnGoRightList()
    end, self.view.verticalTabList.groupTarget.groupId)

    self:BindInputPlayerAction("cashshop_giftpack_goto_right_2", function()
        self:_OnGoRightList()
    end, self.view.verticalTabList.groupTarget.groupId)

    self:BindInputPlayerAction("cashshop_giftpack_goto_left", function()
        InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, false)
        self:_NaviTargetCurrTab()
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

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
        self.view.inputGroup.groupId,
        self.m_phase.cashShopCtrl.view.inputGroup.groupId,
    })
end

ShopTokenCtrl.UpdateAll = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    self:Refresh(self.m_goodsDataList)
end

ShopTokenCtrl._OnShopRefresh = HL.Method() << function(self)
    local isOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isOpen then
        shopDetailCtrl:TryClose()
    end

    local shop = self.m_shopSystem:GetShopData(self.m_currShopId)
    local goodList = shop:GetOpenGoodList()

    if DeviceInfo.usingController then
        self:_RefreshAndRecoverNaviTarget(goodList)
    else
        self:Refresh(goodList)
    end
end

ShopTokenCtrl._OnBuyItemSucc = HL.Method(HL.Any) << function(self, arg)
    if DeviceInfo.usingController then
        self:_RefreshAndRecoverNaviTarget(self.m_goodsDataList)
    else
        self:Refresh(self.m_goodsDataList)
    end
end


ShopTokenCtrl._RefreshAndRecoverNaviTarget = HL.Method(HL.Any) << function(self, goodsDataList)
    if not self.m_isNaviLeft and self.m_currNaviIndex > 0 and self.m_currNaviIndex < self.m_goodsDataList.Count then
        local currNaviGoodsId = self.m_goodsDataList[CSIndex(self.m_currNaviIndex)].goodsId
        self.m_needSetNaviTargetGoodsId = currNaviGoodsId
    end


    self:Refresh(goodsDataList)
end

ShopTokenCtrl.Refresh = HL.Method(HL.Any, HL.Opt(HL.Number)) << function(self, goodsDataList, fastScrollToIndex)
    if goodsDataList == nil then
        Notify(MessageConst.CASH_SHOP_SHOW_WALLET_BAR, {
            moneyIds = {},
        })
        return
    end


    if goodsDataList.Count > 0 then
        local firstGoodsData = goodsDataList[CSIndex(1)]
        local templateId = firstGoodsData.goodsTemplateId
        local goodsTableData = Tables.shopGoodsTable[templateId]
        local moneyId = goodsTableData.moneyId
        Notify(MessageConst.CASH_SHOP_SHOW_WALLET_BAR, {
            moneyIds = { moneyId },
        })
    end


    local showExchange = true
    if self.m_currShopId == "shop_pay_yellow_1" then
        showExchange = false
    end
    self.m_showExchange = showExchange
    self.view.redemptionVoucherNode.gameObject:SetActive(showExchange)
    self.view.tokensInfo.gameObject:SetActive(not showExchange)

    self.m_goodsDataList = goodsDataList
    self.m_shopSystem:SortGoodsList(goodsDataList)


    self:_UpdateSeeGoods()

    if fastScrollToIndex then
        self.view.scrollList:UpdateCount(self.m_goodsDataList.Count, fastScrollToIndex)
    else
        self.view.scrollList:UpdateCount(self.m_goodsDataList.Count)
    end

end

ShopTokenCtrl._InitTabList = HL.Method() << function(self)
    local _, shopGroupData = Tables.shopGroupTable:TryGetValue(CashShopConst.CashShopCategoryType.Token)
    if not shopGroupData then
        return
    end
    self.m_shopIdToTabCell = {}
    self.m_shopDataList = {}
    for _, shopId in pairs(shopGroupData.shopIds) do
        local _, shopData = Tables.shopTable:TryGetValue(shopId)
        if shopData then
            table.insert(self.m_shopDataList, shopData)
        end
    end
    table.sort(self.m_shopDataList, Utils.genSortFunction({"shopGroupNumber"}, true))
    self.m_getTabCell = UIUtils.genCachedCellFunction(self.view.verticalTabList.scrollList)
    self.view.verticalTabList.scrollList.onUpdateCell:AddListener(function(obj, index)

        local cell = self.m_getTabCell(obj)
        local shopData = self.m_shopDataList[LuaIndex(index)]
        cell.cellNameTxt.text = shopData.shopName
        cell.cellNameShadownTxt.text = shopData.shopName
        cell.stateController:SetState("Icon")
        local tabCfg = TAB_CONFIG[shopData.shopId]
        cell.iconImg:LoadSprite(UIConst.UI_SPRITE_ITEM, tabCfg.icon)

        local shop = self.m_shopSystem:GetShopData(shopData.shopId)
        self.m_currShopId = shopData.shopId
        local goodList = shop:GetOpenGoodList()
        local goodsIds = {}
        for _, goodsData in pairs(goodList) do
            table.insert(goodsIds, goodsData.goodsId)
        end
        local redDotArg = {
            goodsIds = goodsIds,
            checkPotentialExchange = tabCfg.checkPotentialExchangeRedDot
        }
        cell.redDot:InitRedDot("CashShopToken", redDotArg)

        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_UpdateSeeGoods()
                self:_SetCurrGoodsRead()

                local shop = self.m_shopSystem:GetShopData(shopData.shopId)
                self.m_currShopId = shopData.shopId
                local goodList = shop:GetOpenGoodList()
                self:Refresh(goodList, 0)
            end
        end)
        self.m_shopIdToTabCell[shopData.shopId] = cell
    end)
    self.view.verticalTabList.scrollList:UpdateCount(#self.m_shopDataList)
    self.m_currShopId = self.m_shopDataList[1].shopId
end

ShopTokenCtrl._InitUICallback = HL.Method() << function(self)
    local exchangeMaterialBtn = self.view.redemptionVoucherNode.voucherList
    exchangeMaterialBtn.onClick:AddListener(function()
        CashShopUtils.TryOpenShopTokenExchangePopUpPanel()
    end)
    self.view.redemptionVoucherNode.redDot:InitRedDot("CashShopHaveCharPotentialExchange")
    self.view.tokensInfo.tipsButton.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "ShopToken_" .. self.m_currShopId)
    end)
    self.view.redemptionVoucherNode.tipsButton.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "ShopTokenExchangePopUp")
    end)

    self.m_currSeenRange = {}
    self.m_haveSeenGoodsId = {}
    self.view.scroll.onValueChanged:AddListener(function(data)
        self:_UpdateSeeRange()
    end)
end

ShopTokenCtrl._InitContentList = HL.Method() << function(self)
    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getItemCell(obj)
        cell:InitCashShopItem(self.m_goodsDataList[(index)])
        cell.view.click.customBindingViewLabelText = Language.LUA_CASH_SHOP_TOKEN_PANEL_BUTTON_BUTTON_KEY_HINT

        if not string.isEmpty(self.m_needSetNaviTargetGoodsId) and self.m_goodsDataList[(index)].goodsId == self.m_needSetNaviTargetGoodsId then
            self.m_needSetNaviTargetGoodsId = ""
            self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)
            self.m_currNaviIndex = LuaIndex(index)
        end
    end)
    self.view.scrollList.onGraduallyShowFinish:AddListener(function()
        self.m_needSetNaviTargetGoodsId = ""
    end)
end

ShopTokenCtrl._ProcessArg = HL.Method(HL.Any) << function(self, arg)
    local shopId = self.m_shopDataList[1].shopId
    if arg and not string.isEmpty(arg.shopId) then
        shopId = arg.shopId
        arg.shopId = nil
    end
    local tabCell = self.m_shopIdToTabCell[shopId]
    if tabCell then
        tabCell.toggle:SetIsOnWithoutNotify(true)
        self.m_currShopId = shopId
        self:SetNaviTarget(tabCell.toggle)
        local shop = self.m_shopSystem:GetShopData(shopId)
        local goodList = shop:GetOpenGoodList()
        self:Refresh(goodList)
    end
    if arg and not string.isEmpty(arg.goodsId) then
        local goodsId = arg.goodsId
        arg.goodsId = nil
        for i = 0, self.m_goodsDataList.Count - 1 do
            if self.m_goodsDataList[i].goodsId == goodsId then
                local goodsData = self.m_goodsDataList[i]
                self.m_pendingAfterTopOrdered = self.m_pendingAfterTopOrdered or {}
                table.insert(self.m_pendingAfterTopOrdered, function()
                    CashShopUtils.OpenShopDetailPanel(goodsData, self)


                    if arg.CategoryTokenShopDetailNumberSelect then
                        local value = arg.CategoryTokenShopDetailNumberSelect
                        arg.CategoryTokenShopDetailNumberSelect = nil
                        local isOpen, detailDialog = UIManager:IsOpen(PanelId.ShopDetail)
                        if isOpen and detailDialog then
                            detailDialog:SetNumberSelectorValue(value)
                        end
                    end
                end)
            end
        end
    end

    if arg.CategoryTokenOpenExchange then
        arg.CategoryTokenOpenExchange = nil
        self.m_pendingAfterTopOrdered = self.m_pendingAfterTopOrdered or {}
        table.insert(self.m_pendingAfterTopOrdered, function()
            CashShopUtils.TryOpenShopTokenExchangePopUpPanel()
        end)
    end

    if arg.showInstructionBook then
        arg.showInstructionBook = nil
        self.m_pendingAfterTopOrdered = self.m_pendingAfterTopOrdered or {}
        table.insert(self.m_pendingAfterTopOrdered, function()
            if self.m_showExchange then
                UIManager:Open(PanelId.InstructionBook, "ShopTokenExchangePopUp")
            else
                UIManager:Open(PanelId.InstructionBook, "ShopToken_" .. self.m_currShopId)
            end
        end)
    end
end


ShopTokenCtrl.OnAfterCategoryTopOrdered = HL.Method() << function(self)
    if not self.m_pendingAfterTopOrdered then
        return
    end
    local list = self.m_pendingAfterTopOrdered
    self.m_pendingAfterTopOrdered = nil
    for _, action in ipairs(list) do
        action()
    end
end

ShopTokenCtrl.SetCashShopStateArg = HL.Method(HL.Table) << function(self, arg)

    arg.shopId = self.m_currShopId
    local isOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isOpen then
        local info = shopDetailCtrl:GetInfo()
        local value = shopDetailCtrl:GetNumberSelectorValue()
        arg.goodsId = info.goodsId
        arg.CategoryTokenShopDetailNumberSelect = value
    end


    local _, exchangeCtrl = UIManager:IsOpen(PanelId.ShopTokenExchangePopUp)
    if exchangeCtrl then
        arg.CategoryTokenOpenExchange = true
    end

    if UIManager:IsOpen(PanelId.InstructionBook) then
        arg.showInstructionBook = true
    end
end

ShopTokenCtrl._UpdateSeeRange = HL.Method() << function(self)
    local range = self.view.scrollList:GetShowRange()
    local currX = range.x
    local currY = range.y
    if self.m_currSeenRange.x == nil or self.m_currSeenRange.x > currX then
        self.m_currSeenRange.x = currX
    end
    if self.m_currSeenRange.y == nil or self.m_currSeenRange.y < currY then
        self.m_currSeenRange.y = currY
    end
end

ShopTokenCtrl._UpdateSeeGoods = HL.Method() << function(self)

    self:_UpdateSeeRange()

    if self.m_currSeenRange.x and self.m_currSeenRange.y then
        local beginIndex = self.m_currSeenRange.x
        local endIndex = self.m_currSeenRange.y
        for i = beginIndex, endIndex do
            if i >= 0 and i < self.m_goodsDataList.Count then
                local goodsData = self.m_goodsDataList[i]
                if goodsData then
                    local goodsId = goodsData.goodsId
                    if lume.find(self.m_haveSeenGoodsId, goodsId) == nil then
                        table.insert(self.m_haveSeenGoodsId, goodsId)
                    end
                end
            end
        end
    end
    self.m_currSeenRange.x = nil
    self.m_currSeenRange.y = nil
end

ShopTokenCtrl._SetCurrGoodsRead = HL.Method() << function(self)
    for _, goodsId in ipairs(self.m_haveSeenGoodsId) do
        GameInstance.player.shopSystem:RecordSeeGoodsId(goodsId)
    end
    self.m_haveSeenGoodsId = {}
    GameInstance.player.shopSystem:SetGoodsIdSee()
end

ShopTokenCtrl._SetSingleGoodsReadByGoodsId = HL.Method(HL.String) << function(self, goodsId)
    GameInstance.player.shopSystem:SetSingleGoodsIdSee(goodsId)
end

ShopTokenCtrl._SetSingleGoodsReadByIndex = HL.Method(HL.Number) << function(self, luaIndex)
    local csIndex = CSIndex(luaIndex)
    if csIndex >= 0 and csIndex < self.m_goodsDataList.Count then
        local goodsId = self.m_goodsDataList[CSIndex(luaIndex)].goodsId
        GameInstance.player.shopSystem:SetSingleGoodsIdSee(goodsId)
    end
end

ShopTokenCtrl._OnGoRightList = HL.Method() << function(self)
    logger.info("ShopTokenCtrl._OnGoRightList 被触发")
    InputManagerInst:ToggleGroup(self.view.verticalTabList.groupTarget.groupId, false)
    self:TargetFirstCell()
    self.m_isNaviLeft = false
end

ShopTokenCtrl.TargetFirstCell = HL.Method() << function(self)
    logger.info("ShopTokenCtrl: TargetFirstCell")

    InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, true)

    local range = self.view.scrollList:GetShowRange()
    local firstCell = self.m_getItemCell(self.view.scrollList:Get(range.x))
    self:SetNaviTarget(firstCell.view.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = LuaIndex(range.x)
    self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
end

ShopTokenCtrl._NaviTargetCurrTab = HL.Method() << function(self)
    logger.info("ShopTokenCtrl: _NaviTargetCurrTab")

    local currTabCell = self.m_shopIdToTabCell[self.m_currShopId]
    self:SetNaviTarget(currTabCell.toggle)
    self.m_isNaviLeft = true

    InputManagerInst:ToggleGroup(self.view.verticalTabList.groupTarget.groupId, true)
end

ShopTokenCtrl._OnGoLeft = HL.Method() << function(self)
    logger.info("ShopTokenCtrl: _OnGoLeft")

    local currIndex = self.m_currNaviIndex

    local countPerLine = self.view.scrollList.countPerLine
    if currIndex and currIndex % countPerLine == 1 then
        InputManagerInst:ToggleGroup(self.view.scrollGroupTarget.groupId, false)
        self.m_currNaviIndex = 0
        self:_NaviTargetCurrTab()
        logger.info("ShopTokenCtrl: _OnGoLeft: NaviTargetCurrTab, currIndex is: " .. currIndex)
    else
        local targetCell = self.m_getItemCell(self.view.scrollList:Get(CSIndex(currIndex - 1)))
        if targetCell == nil then
            return
        end
        self:SetNaviTarget(targetCell.view.inputBindingGroupNaviDecorator)
        self.m_currNaviIndex = self.m_currNaviIndex - 1
        self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
    end
end

ShopTokenCtrl._OnGoUp = HL.Method() << function(self)
    local countPerLine = self.view.scrollList.countPerLine
    local targetIndex = self.m_currNaviIndex - countPerLine
    if targetIndex <= 0 then
        return
    end

    local targetCell = self.m_getItemCell(self.view.scrollList:Get(CSIndex(targetIndex)))
    if targetCell == nil then
        return
    end
    self:SetNaviTarget(targetCell.view.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = targetIndex
    self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
end

ShopTokenCtrl._OnGoRight = HL.Method() << function(self)
    local targetIndex = self.m_currNaviIndex + 1
    if targetIndex > self.m_goodsDataList.Count then
        return
    end

    local targetCell = self.m_getItemCell(self.view.scrollList:Get(CSIndex(targetIndex)))
    if targetCell == nil then
        return
    end
    self:SetNaviTarget(targetCell.view.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = targetIndex
    self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
end

ShopTokenCtrl._OnGoDown = HL.Method() << function(self)
    local countPerLine = self.view.scrollList.countPerLine
    local targetIndex = self.m_currNaviIndex + countPerLine
    if targetIndex > self.m_goodsDataList.Count then

        local lineNumber = (self.m_goodsDataList.Count // countPerLine) +
            ((self.m_goodsDataList.Count % countPerLine > 0) and 1 or 0)
        if self.m_currNaviIndex <= countPerLine * (lineNumber - 1) then
            targetIndex = self.m_goodsDataList.Count
        else
            return
        end
    end

    local targetCell = self.m_getItemCell(self.view.scrollList:Get(CSIndex(targetIndex)))
    if targetCell == nil then
        return
    end
    self:SetNaviTarget(targetCell.view.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = targetIndex
    self:_SetSingleGoodsReadByIndex(self.m_currNaviIndex)
end

HL.Commit(ShopTokenCtrl)
