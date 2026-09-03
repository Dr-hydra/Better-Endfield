
local SpaceshipShopBaseCtrl = require_ex('UI/Panels/SpaceshipCreditShop/SpaceshipShopBaseCtrl')
HL.Forward("SpaceshipShopBaseCtrl")
local PANEL_ID = PanelId.SpaceshipCreditShop
local PHASE_ID = PhaseId.SpaceshipCreditShop
local shopGroupId = "shop_spaceship_credit"
local shopSystem = GameInstance.player.shopSystem

SpaceshipCreditShopCtrl = HL.Class('SpaceshipCreditShopCtrl', SpaceshipShopBaseCtrl.SpaceshipShopBaseCtrl)

SpaceshipCreditShopCtrl.m_moneyId = HL.Field(HL.String) << ""

SpaceshipCreditShopCtrl.m_haveOpenRefreshPopup = HL.Field(HL.Boolean) << false

SpaceshipCreditShopCtrl.m_refreshBtnCallback = HL.Field(HL.Function)



SpaceshipCreditShopCtrl.m_pendingAfterTopOrdered = HL.Field(HL.Table)

SpaceshipCreditShopCtrl.OpenSpaceshipCreditShopPanel = HL.StaticMethod() << function()
    PhaseManager:GoToPhase(PhaseId.CashShop, { shopGroupId = CashShopConst.CashShopCategoryType.Credit })
end


SpaceshipCreditShopCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.tipsBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.ShopCreditPointsPopUp)
    end)

    self.view.creditBtn.functionBtn.onClick:AddListener(function()

        local canGetCredit = not GameInstance.player.spaceship:IsCreditAwardAllReceived()
        if canGetCredit then
            GameInstance.player.spaceship:RecvVisitListReward()
        end
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, index)
        self:_RefreshContentCell(self.m_getCellFunc(obj), LuaIndex(index))
    end)

    self:_SetWalletBarAndTime()

    self.view.creditBtn.redDot:InitRedDot("CashShopCreditShopGetCredit")


    self:_StartCoroutine(function()
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
            self.view.inputGroup.groupId,
            self.m_phase.m_panel2Item[PanelId.CashShop].uiCtrl.view.inputGroup.groupId,
        })
    end)

    self:_InitData()

    local firstCell = self.m_getCellFunc(self.view.scrollList:Get(0))
    if firstCell ~= nil then
        self:SetNaviTarget(firstCell.view.inputBindingGroupNaviDecorator)
    end

    self.m_phase:HidePsStore()

    self:_ProcessArg(arg)
end

SpaceshipCreditShopCtrl.OnShow = HL.Override() << function(self)
    if self.m_phase.m_needGameEvent then
        self.m_phase.m_needGameEvent = false
        EventLogManagerInst:GameEvent_ShopEnter(
            self.m_phase.m_enterButton,
            self.m_phase.m_enterPanel,
            "",
            CashShopConst.CashShopCategoryType.Credit,
            ""
        )
    end

    GameInstance.player.spaceship:QueryVisitInfo()
end

SpaceshipCreditShopCtrl.OnHide = HL.Override() << function(self)
end

SpaceshipCreditShopCtrl.OnClose = HL.Override() << function(self)
end

SpaceshipCreditShopCtrl._RefreshUI = HL.Method() << function(self)
    local getCreditBtnStateCtrl = self.view.creditBtn.btnState

    local canGetCredit = not GameInstance.player.spaceship:IsCreditAwardAllReceived()
    if canGetCredit then
        getCreditBtnStateCtrl:SetState("NormalState")
    else
        getCreditBtnStateCtrl:SetState("DarkState")
    end
end

SpaceshipCreditShopCtrl._OnShopRefresh = HL.Override() << function(self)
    SpaceshipCreditShopCtrl.Super._OnShopRefresh(self)
    self.view.creditBtn.redDot:InitRedDot("CashShopCreditShopGetCredit")
end

SpaceshipCreditShopCtrl._InitData = HL.Method() << function(self)
    if not shopSystem:CheckShopGroupUnlocked(shopGroupId) then
        logger.error(string.format("飞船信用商店groupid:%s没有解锁", shopGroupId))
        return
    end
    self.m_needPlaySoldOut = {}
    self.m_needPlayUnlock = {}
    self.m_lastBuyGoods = {}
    self.m_shopGroupId = shopGroupId
    local groupData = shopSystem:GetShopGroupData(shopGroupId)
    for i = groupData.shopIdList.Count - 1, 0, -1 do
        local shopTableData = Tables.shopTable:GetValue(groupData.shopIdList[i])
        local shopUnlock = shopSystem:CheckShopUnlocked(shopTableData.shopId)
        if shopUnlock then
            self.m_shopId = shopTableData.shopId
            break
        end
    end
    if self.m_shopId == "" then
        logger.error("所有的飞船信用商店都没有解锁")
        return
    end

    local shopData = shopSystem:GetShopData(self.m_shopId)
    for goodsId, goodsData in pairs(shopData.goodsDic) do
        local goodsTableData = Tables.shopGoodsTable[goodsData.goodsTemplateId]
        local moneyId = goodsTableData.moneyId
        self.m_moneyId = moneyId
        break
    end

    self:_RefreshSheetTabs(self.m_shopId)
end

SpaceshipCreditShopCtrl._RefreshTimeCountDown = HL.Override() << function(self)

    local shopTableData = Tables.shopTable[self.m_shopId]
    if shopTableData.shopRefreshCycleType == GEnums.ShopRefreshCycleType.None then
        self.view.refreshTime.gameObject:SetActiveIfNecessary(false)
    else
        self.view.refreshTime.gameObject:SetActiveIfNecessary(true)
        self.view.refreshTimesNumTxt:InitCountDownText(self:_CalculateTargetTime(shopTableData.shopRefreshCycleType), function()
            self:_RefreshTimeCountDown()
        end)
    end
end

SpaceshipCreditShopCtrl._SetWalletBarAndTime = HL.Method() << function(self)
    local numberLimit = Tables.MoneyConfigTable:GetValue(Tables.CashShopConst.CreditTabMoneyId).MoneyClearLimit
    local itemCount = GameInstance.player.inventory:GetItemCount(
        Utils.getCurrentScope(), Utils.getCurrentChapterId(), Tables.CashShopConst.CreditTabMoneyId)
    local time = Utils.getNextCommonServerRefreshTime()
    logger.info(string.format("[SpaceshipCreditShop] item: %s / %s, time: %s",
        itemCount, numberLimit, time))
    Notify(MessageConst.CASH_SHOP_SHOW_WALLET_BAR, {
        moneyIds = {Tables.CashShopConst.CreditTabMoneyId},
        showTimeNode = (itemCount > numberLimit),
        time = time,
        timeCompleteCallback = function()
            self:_SetWalletBarAndTime()
        end
    })
end

SpaceshipCreditShopCtrl._RefreshSheetTabs = HL.Override(HL.String) << function(self, curShopId)
    self.m_shopId = curShopId
    self:_RefreshTimeCountDown()
    local shopData = shopSystem:GetShopData(self.m_shopId)
    self.m_goods = {}
    self.m_soldOut = {}
    for goodsId, goodsData in pairs(shopData.goodsDic) do
        local isUnlocked = shopSystem:CheckGoodsUnlocked(goodsId)

        local goodsTableData = Tables.shopGoodsTable[goodsData.goodsTemplateId]
        if isUnlocked or goodsTableData.isShowWhenLock then
            local itemBundle = UIUtils.getRewardFirstItem(goodsTableData.rewardId)

            local itemTableData = Tables.itemTable[itemBundle.id]
            local info = {
                id = goodsId,
                rarity = itemTableData.rarity,
                price = goodsTableData.price * goodsData.discount,
                sortId = goodsTableData.sortId,
                discountSortId = goodsData.discount,
            }
            if shopSystem:GetRemainCountByGoodsId(self.m_shopId, goodsId) > 0 then
                table.insert(self.m_goods, info)
            else
                table.insert(self.m_soldOut, info)
            end
        end
    end

    table.sort(self.m_goods, Utils.genSortFunction({"discountSortId", "id"}, true))
    table.sort(self.m_soldOut, Utils.genSortFunction({"discountSortId", "id"}, true))
    self.m_goodsInfos = {}
    for i, v in ipairs(self.m_goods) do
        table.insert(self.m_goodsInfos, v)
    end
    for i, v in ipairs(self.m_soldOut) do
        table.insert(self.m_goodsInfos, v)
    end

    self:_RefreshContent()

    self:_OnShopGoodsManualRefresh()


    local canGetCredit = not GameInstance.player.spaceship:IsCreditAwardAllReceived()
    self.view.creditBtn.gameObject:SetActive(canGetCredit)
    self.view.noCreditPoint.gameObject:SetActive(not canGetCredit)
end

SpaceshipCreditShopCtrl._GetRefreshCost = HL.Method(HL.Userdata).Return(HL.Number, HL.Number)
    << function(self, tableData)
    local costCount = tableData.costItemCount1
    local haveCount = Utils.getItemCount(tableData.costItemId1)
    return costCount, haveCount
end

SpaceshipCreditShopCtrl._ManualRefreshGoods = HL.Method(HL.Number, HL.Userdata) << function(self, remainCount, tableData)
    local costItemData = Tables.itemTable:GetValue(tableData.costItemId1)
    self.m_haveOpenRefreshPopup = true
    Notify(MessageConst.SHOW_POP_UP, {
        content = string.format(Language.LUA_SHOP_MANUAL_REFRESH_TITLE, tableData.costItemCount1),
        subContent = string.format(Language.LUA_SHOP_MANUAL_REFRESH_SUB_TITLE, remainCount),
        onConfirm = function()
            local costCount = tableData.costItemCount1
            local haveCount = Utils.getItemCount(tableData.costItemId1)
            if costCount > haveCount then
                Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_SHOP_BUY_MONEY_NOT_ENOUGH, costItemData.name))
                return
            end
            shopSystem:SendShopManualRefresh(self.m_shopId)
            self.m_haveOpenRefreshPopup = false
        end,
        onCancel = function()
            self.m_haveOpenRefreshPopup = false
        end,
        moneyInfo = {
            moneyIds = {Tables.CashShopConst.CreditTabMoneyId},
            useItemIcon = false,
            showLimit = true,
        }
    })
end

SpaceshipCreditShopCtrl._OnShopGoodsManualRefresh = HL.Override() << function(self)
    local manualRefreshTable = Tables.ShopManualRefreshTable:GetValue(self.m_shopGroupId)
    local limitCount = manualRefreshTable.list.Count
    local useCount = shopSystem:GetManualRefreshCountByShopGroupId(self.m_shopGroupId)
    local remainCount = limitCount - useCount
    self.view.remainingTimesText.gameObject:SetActive(remainCount > 0)
    self.view.refreshBtn.gameObject:SetActive(remainCount > 0)
    self.view.emptyNode.gameObject:SetActive(remainCount == 0)
    if remainCount > 0 then
        self.view.remainingTimesText.text = string.format(Language.LUA_SHOP_CREDIT_REMAIN_REFRESH,
            remainCount, limitCount)
        local refreshTable = manualRefreshTable.list[useCount]
        self.view.refreshBtn.functionBtn.onClick:RemoveAllListeners()
        self.m_refreshBtnCallback = function()
            self:_ManualRefreshGoods(remainCount, refreshTable)
        end
        self.view.refreshBtn.functionBtn.onClick:AddListener(self.m_refreshBtnCallback)

        local costCount, haveCount = self:_GetRefreshCost(refreshTable)
        self.view.refreshBtn.functionTxt.text = costCount
        self.view.refreshBtn.functionTxt.color = (costCount <= haveCount)
            and UIUtils.getColorByString("444444") or Color.red
    end
end

SpaceshipCreditShopCtrl._OnWalletChanged = HL.Override(HL.Table) << function(self, args)
    logger.info("[shop] 货币变化，重刷UI")

    self:_SetWalletBarAndTime()

    self:_OnShopGoodsManualRefresh()

    local canGetCredit = not GameInstance.player.spaceship:IsCreditAwardAllReceived()
    self.view.creditBtn.gameObject:SetActive(canGetCredit)
    self.view.noCreditPoint.gameObject:SetActive(not canGetCredit)
end

SpaceshipCreditShopCtrl._OnReceiveVisitInfo = HL.Override() << function(self)
    logger.info("[shop] 访客数据变化，重刷UI")

    local canGetCredit = not GameInstance.player.spaceship:IsCreditAwardAllReceived()
    self.view.creditBtn.gameObject:SetActive(canGetCredit)
    self.view.noCreditPoint.gameObject:SetActive(not canGetCredit)
end

SpaceshipCreditShopCtrl._OnGetCredit = HL.Override(HL.Table) << function(self, args)
    GameInstance.player.spaceship:QueryVisitInfo()
end

SpaceshipCreditShopCtrl._ProcessArg = HL.Method(HL.Any) << function(self, arg)
    if arg and not string.isEmpty(arg.goodsId) then
        for _, v in ipairs(self.m_goodsInfos) do
            if v.id == arg.goodsId then
                local goodsData = shopSystem:GetShopGoodsData(self.m_shopId, arg.goodsId)
                self.m_pendingAfterTopOrdered = self.m_pendingAfterTopOrdered or {}
                table.insert(self.m_pendingAfterTopOrdered, function()
                    self.m_phase:CreatePhasePanelItem(PanelId.ShopDetail, goodsData)
                end)
            end
        end
    end

    if arg and arg.spaceshipCreditShopRefreshPopup then
        arg.spaceshipCreditShopRefreshPopup = nil
        self.m_refreshBtnCallback()
    end
end


SpaceshipCreditShopCtrl.OnAfterCategoryTopOrdered = HL.Method() << function(self)
    if not self.m_pendingAfterTopOrdered then
        return
    end
    local list = self.m_pendingAfterTopOrdered
    self.m_pendingAfterTopOrdered = nil
    for _, action in ipairs(list) do
        action()
    end
end

SpaceshipCreditShopCtrl.SetCashShopStateArg = HL.Method(HL.Table) << function(self, arg)

    local isOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isOpen then
        local info = shopDetailCtrl:GetInfo()
        arg.goodsId = info.goodsId
    end

    if self.m_haveOpenRefreshPopup then
        arg.spaceshipCreditShopRefreshPopup = true
    end
end

HL.Commit(SpaceshipCreditShopCtrl)
