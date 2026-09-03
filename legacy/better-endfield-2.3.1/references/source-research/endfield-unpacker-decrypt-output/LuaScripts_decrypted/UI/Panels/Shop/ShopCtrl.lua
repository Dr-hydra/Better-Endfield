local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Shop
local PHASE_ID = PhaseId.Shop
local shopSystem = GameInstance.player.shopSystem
ShopCtrl = HL.Class('ShopCtrl', uiCtrl.UICtrl)

ShopCtrl.m_shopGroupId = HL.Field(HL.String) << ""

ShopCtrl.m_activityShopInfo = HL.Field(HL.Any)

ShopCtrl.m_shopId = HL.Field(HL.String) << ""

ShopCtrl.m_goodsInfos = HL.Field(HL.Table)

ShopCtrl.m_goods = HL.Field(HL.Table)

ShopCtrl.m_soldOut = HL.Field(HL.Table)

ShopCtrl.m_needPlaySoldOut = HL.Field(HL.Table)

ShopCtrl.m_needPlayUnlock = HL.Field(HL.Table)

ShopCtrl.m_waitAnimation = HL.Field(HL.Boolean) << false

ShopCtrl.m_getCellFunc = HL.Field(HL.Function)

ShopCtrl.m_haveControllerTarget = HL.Field(HL.Boolean) << false

ShopCtrl.m_lastBuyGoods = HL.Field(HL.Table)

ShopCtrl.m_conditionSyncGoods = HL.Field(HL.Table)

ShopCtrl.unlockedShopSheets = HL.Field(HL.Table)

ShopCtrl.index2TabCell = HL.Field(HL.Table)

ShopCtrl.m_tabCells = HL.Field(HL.Forward("UIListCache"))

ShopCtrl.m_moneyInfo = HL.Field(HL.Table)

ShopCtrl.m_unlockToastFunc = HL.Field(HL.Any)





ShopCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SHOP_REFRESH] = '_OnShopRefresh',
    [MessageConst.ON_SHOP_FREQUENCY_LIMIT_CHANGE] = 'OnLimitChange',
    [MessageConst.ON_SHOP_GOODS_LOCK_CHANGE] = 'OnConditionChange',
    [MessageConst.ON_BUY_ITEM_SUCC] = 'OnBuyItemSucc',
    [MessageConst.AFTER_ON_BUY_ITEM_SUCC] = 'OnAfterBuyItemSucc',

    [MessageConst.SHOP_WAIT_ANIMATION] = 'WaitAnimation',
    [MessageConst.SHOW_SHOP_ITEM_POP_UP] = 'SetMoneyCell',

    [MessageConst.ON_SHOP_GOODS_CONDITION_SYNC] = 'OnGoodsConditionSync',
    [MessageConst.ON_SHOP_GOODS_MANUAL_REFRESH] = '_OnShopGoodsManualRefresh',
}

ShopCtrl._OnShopRefresh = HL.Virtual() << function(self)
    if self.m_waitAnimation then
        return
    end
    self.view.scrollList:SkipGraduallyShow()
    self:_RefreshSheetTabs(self.m_shopId)
    self.view.scrollList:SkipGraduallyShow()
end



ShopCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.domainTopMoneyTitle.view.closeBtn.onClick:AddListener(function()
        Notify(MessageConst.HIDE_ITEM_TIPS)
        AudioAdapter.PostEvent("Au_UI_Menu_ShopPanel_Close")
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.topMoneyTitle.closeBtn.onClick:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Menu_ShopPanel_Close")
        PhaseManager:PopPhase(PHASE_ID)
    end)
    UIManager:ToggleBlockObtainWaysJump("common_shop", true)

    self.m_needPlaySoldOut = {}
    self.m_needPlayUnlock = {}
    self.m_lastBuyGoods = {}
    self.m_conditionSyncGoods = {}

    local shopGroupId, shopId
    if type(arg) == "table" then
        shopGroupId = arg.shopGroupId
        shopId = arg.shopId
        self.m_unlockToastFunc = arg.unlockToastFunc
    else
        shopGroupId = arg
    end

    if shopGroupId == nil or type(shopGroupId) ~= "string" then
        logger.error(ELogChannel.UI, "打开商店界面参数错误")
        return
    end

    self.m_shopGroupId = shopGroupId
    local shopGroupData = shopSystem:GetShopGroupData(self.m_shopGroupId)
    self.m_shopId = shopId or shopGroupData.shopIdList[0]
    local _, activityShopInfo = Tables.ActivityShopAdditionalTable:TryGetValue(self.m_shopGroupId)
    self.m_activityShopInfo = activityShopInfo or nil

    local groupUnlock = shopSystem:CheckShopGroupUnlocked(shopGroupId)
    local shopUnlock = shopSystem:CheckShopUnlocked(self.m_shopId)

    if self.m_activityShopInfo ~= nil then

        local activityId = self.m_activityShopInfo.activityId
        self.view.domainTopMoneyTitle.gameObject:SetActive(false)
        if self.m_activityShopInfo.hasHelpComponent then
            self.view.topMoneyTitle.helpNode.gameObject:SetActive(true)
            self.view.topMoneyTitle.helpBtn.onClick:AddListener(function()
                local activityConfig = Tables.activityTable[activityId]
                local instructionId = activityConfig.instructionId
                UIManager:Open(PanelId.InstructionBook, instructionId)
            end)
        else
            self.view.topMoneyTitle.helpNode.gameObject:SetActive(false)
        end
        self:_RefreshTopMoneyTitle(self.m_activityShopInfo.activityMoneyId)
        self.view.bgBanner:LoadSprite(UIConst.UI_SPRITE_SHOP_COMMON, self.m_activityShopInfo.banner)

    else
        self.view.topMoneyTitle.gameObject:SetActive(false)
        self:_RefreshDomainTopMoneyTitle()
    end

    if not groupUnlock or not shopUnlock then
        Notify(MessageConst.SHOW_POP_UP,{content = Language.LUA_SHOP_NOT_UNLOCK,hideCancel = true, onConfirm = function()
            PhaseManager:PopPhase(PHASE_ID)
        end})

        return
    end
    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, index)
        self:_RefreshContentCell(self.m_getCellFunc(obj), LuaIndex(index), "normal")
    end)

    self.view.scrollList.onGraduallyShowFinish:RemoveAllListeners()
    self.view.scrollList.onGraduallyShowFinish:AddListener(function()
        self.view.sortNode:SetEnableFilterBtn(true)
    end)


    local sortOptions = {
        {
            name = Language.LUA_SHOP_SORT_RARITY,
            keys = { "rarity", "sortId", "id" }
        },
        {
            name = Language.LUA_SHOP_SORT_PRICE,
            keys = { "price", "sortId", "id" }
        },
        {
            name = Language.LUA_SHOP_SORT_DEFAULT,
            keys = { "sortId", "id" },
        },
    }
    self.view.sortNode:InitSortNode(sortOptions, function(data, isIncremental)
        self:_ApplySortOption(data, isIncremental)
    end, #sortOptions - 1, false, true)

    self.view.sortNode.gameObject:SetActive(false)
    self:_RefreshSheetTabs(self.m_shopId)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    local resumeOpenPanel = type(arg) == "table" and arg.resumeOpenPanel or nil
    if resumeOpenPanel then

        for _, panelInfo in ipairs(resumeOpenPanel) do
            self:_RestorePopupByResumeState(panelInfo)
        end
    end
end

ShopCtrl.InitShopTabs = HL.Method(HL.String, HL.String) << function(self, shopGroupId, curShopId)
    local shopGroupData = GameInstance.player.shopSystem:GetShopGroupData(shopGroupId)
    self.unlockedShopSheets = {}
    for i, shopId in pairs(shopGroupData.shopIdList) do
        local isUnlocked = shopSystem:CheckShopUnlocked(shopId)

        local shopTableData = Tables.shopTable[shopId]
        if isUnlocked or shopTableData.isShowWhenLock then
            table.insert(self.unlockedShopSheets, shopId)
        end
    end

    if #self.unlockedShopSheets > 1 then
        self.view.tabKeyHintPrev.gameObject:SetActive(true)
        self.view.tabKeyHintPost.gameObject:SetActive(true)
    else
        self.view.tabKeyHintPrev.gameObject:SetActive(false)
        self.view.tabKeyHintPost.gameObject:SetActive(false)
    end

    self.index2TabCell = {}
    self.m_tabCells:Refresh(#self.unlockedShopSheets, function(cell, index)
        local shopId = self.unlockedShopSheets[index]
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)


            if self.m_unlockToastFunc ~= nil then
                local isUnlocked = shopSystem:CheckShopUnlocked(shopId)
                if not isUnlocked then
                    self.m_unlockToastFunc(shopId)
                    self:SelectTab(1)
                    return
                end
            end
            if isOn and curShopId ~= shopId then
                AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
                shopSystem:SetGoodsIdSee()
                self:_RefreshSheetTabs(shopId)
            end
        end)
        self.index2TabCell[index] = cell
        cell.toggle.isOn = shopId == curShopId
        local shopData = Tables.shopTable[shopId]
        cell.tabNameText.text = shopData.shopName
        cell.tabNameDecoText.text = shopData.shopEnName
        cell.tabIconImg:LoadSprite(UIConst.UI_SPRITE_INVENTORY,shopData.iconId)
        cell.tabIconDecoImg:LoadSprite(UIConst.UI_SPRITE_INVENTORY,shopData.iconId)
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            local isUnlocked = shopSystem:CheckShopUnlocked(shopId)
            if not isUnlocked and self.m_unlockToastFunc ~= nil then
                self.m_unlockToastFunc(shopId)
                return
            end
            self:SelectTab(index)
        end)
    end)
end


ShopCtrl.SelectTab = HL.Method(HL.Number) << function(self, index)
    for searchIndex, cell in ipairs(self.index2TabCell) do
        cell.toggle.isOn = false
    end
    if self.index2TabCell[index] then
        self.index2TabCell[index].toggle.isOn = true
    end
end

ShopCtrl._RefreshTopMoneyTitle = HL.Method(HL.String) << function(self, moneyId)
    self.view.topMoneyTitle.gameObject:SetActive(true)
    local shopGroupTableData = Tables.shopGroupTable[self.m_shopGroupId]
    self.view.topMoneyTitle.titleTxt.text = shopGroupTableData.shopGroupName

    if self.m_activityShopInfo ~= nil then
        local titleColor = UIUtils.getColorByString(self.m_activityShopInfo.titleColor)
        self.view.topMoneyTitle.titleTxt.color = titleColor
        self.view.topMoneyTitle.txtSolidus1.color = titleColor
        self.view.topMoneyTitle.txtSolidus2.color = titleColor
    end
    local moneyInfos = {}
    table.insert(moneyInfos, {
        moneyId = moneyId,
        showLimit = false,
        clearRuleType = GEnums.MoneyClearRuleType.None,
    })
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.topMoneyTitle.topBar)
    self.view.topMoneyTitle.walletBarPlaceholder:InitWalletBarPlaceholderDetailed(moneyInfos)

    local moneyItemCfg = Utils.tryGetTableCfg(Tables.itemTable, moneyId)
    self.m_moneyInfo = {
        moneyId = moneyId,
        moneyIcon = moneyItemCfg and moneyItemCfg.iconId or "",
    }
end

ShopCtrl._RefreshDomainTopMoneyTitle = HL.Method() << function(self)
    local success, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(GameWorld.worldInfo.curLevelId)
    local dataSucc, domainDevData = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(levelBasicInfo.domainName)
    if not dataSucc then
        logger.error("DomainGradeCtrl._UpdateMoneyCell cant find domainDevData: ", levelBasicInfo.domainName)
        return
    end


    local shopGroupTableData = Tables.shopGroupTable[self.m_shopGroupId]
    self.view.domainTopMoneyTitle:SetTitleText(shopGroupTableData.shopGroupName)

    local goldItemId = domainDevData.domainDataCfg.domainGoldItemId
    self.view.domainTopMoneyTitle.gameObject:SetActive(true)
    self.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(levelBasicInfo.domainName)

    local moneyItemCfg = Utils.tryGetTableCfg(Tables.itemTable, goldItemId)
    self.m_moneyInfo = {
        moneyId = goldItemId,
        moneyIcon = moneyItemCfg and moneyItemCfg.iconId or "",
    }
end

ShopCtrl._RefreshSheetTabs = HL.Virtual(HL.String) << function(self, curShopId)
    self.m_shopId = curShopId
    self:_RefreshTimeCountDown()

    self.m_tabCells = self.m_tabCells or UIUtils.genCellCache(self.view.marketTabLayout)
    self:InitShopTabs(self.m_shopGroupId, self.m_shopId)


    local shopTableData = Tables.shopTable[self.m_shopId]


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
            }
            local remainCount = shopSystem:GetRemainCountByGoodsId(self.m_shopId, goodsId)
            if remainCount > 0 then
                table.insert(self.m_goods, info)
            else
                if remainCount == -1 then
                    table.insert(self.m_goods, info)
                else
                    table.insert(self.m_soldOut, info)
                end
            end
        end
    end

    self:_ApplySortOption()
end

ShopCtrl._ApplySortOption = HL.Method(HL.Opt(HL.Table, HL.Boolean)) << function(self, sortData, isIncremental)
    sortData = sortData or self.view.sortNode:GetCurSortData()
    if isIncremental == nil then
        isIncremental = self.view.sortNode.isIncremental
    end

    table.sort(self.m_goods, Utils.genSortFunction(sortData.keys, isIncremental))

    self.m_goodsInfos = {}
    for i, v in ipairs(self.m_goods) do
        table.insert(self.m_goodsInfos, v)
    end
    for i, v in ipairs(self.m_soldOut) do
        table.insert(self.m_goodsInfos, v)
    end
    self:_RefreshContent()
end

ShopCtrl._RefreshContent = HL.Method() << function(self)
    self.m_haveControllerTarget = false
    self.view.sortNode:SetEnableFilterBtn(false)
    self.view.scrollList:UpdateCount(#self.m_goodsInfos)
end

ShopCtrl._RefreshContentCell = HL.Virtual(HL.Any, HL.Number, HL.String) << function(self, cell, luaIndex, refreshTag)
    local goodsId = self.m_goodsInfos[luaIndex].id
    local goodsData = shopSystem:GetShopGoodsData(self.m_shopId, goodsId)

    local goodsTableData = Tables.shopGoodsTable[goodsData.goodsTemplateId]
    local displayItem = UIUtils.getRewardFirstItem(goodsTableData.rewardId)

    local remainCount = shopSystem:GetRemainCountByGoodsId(self.m_shopId, goodsId)

    if luaIndex == 1 and not self.m_haveControllerTarget then
        local firstCell = self.m_getCellFunc(1)
        if firstCell then
            local hasSortNode = self.m_activityShopInfo==nil or self.m_activityShopInfo.hasSortComponent
            self.view.sortNode.gameObject:SetActive(hasSortNode)
            self:SetNaviTarget(firstCell.view.selectBtn)
            self.m_haveControllerTarget = true
        end
    end
    local goodsTplId = goodsData.goodsTemplateId
    local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsTplId)
    local discount = 1 - goodsData.discount
    local discountedRatio = goodsData.discount
    local remainLimitCount = shopSystem:GetRemainCountByGoodsId(self.m_shopId, goodsTplId)

    local itemBundle = UIUtils.getRewardFirstItem(goodsCfg.rewardId)
    local itemId = itemBundle.id
    local hasCfg, itemCfg = Tables.itemTable:TryGetValue(itemId)
    local goodsInfo = {

        shopId = self.m_shopId,
        goodsId = goodsId,

        originPrice = goodsCfg.price,
        discount = discount,
        curPrice = lume.round(goodsCfg.price * discountedRatio, 1),
        remainLimitCount = remainLimitCount,
        refreshType = goodsCfg.limitCountRefreshType,
        orgLimitCount = goodsCfg.limitCount,

        itemId = itemId,
        itemName = itemCfg.name,
        itemCount = Utils.getItemCount(itemId, true, true),
        itemBundleCount = itemBundle.count,
        itemIcon = itemCfg.iconId,
        itemRarity = itemCfg.rarity,
        moneyId = self.m_moneyInfo.moneyId,
        moneyIcon = self.m_moneyInfo.moneyIcon,

        remainLimitSort = remainLimitCount == 0 and 1 or 0,
        raritySort = -itemCfg.rarity,
        priceRatioSort = -discount,
        sortId = goodsCfg.sortId,
        isLocked = self:CheckGoodsUnlocked(goodsId),
        refreshTag = refreshTag,
    }
    cell:InitCommonShopGoodsCellCommonMode(goodsInfo)
end

ShopCtrl.CheckGoodsUnlocked = HL.Virtual(HL.String).Return(HL.Boolean) << function(self, goodsId)
    return not shopSystem:CheckGoodsUnlocked(goodsId)
end


ShopCtrl._RefreshTimeCountDown = HL.Virtual() << function(self)

    local shopTableData = Tables.shopTable[self.m_shopId]
    local state
    if shopTableData.shopRefreshCycleType == GEnums.ShopRefreshCycleType.None then
        if self.m_activityShopInfo and self.m_activityShopInfo.showCloseCd then
            local activityId = self.m_activityShopInfo.activityId
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)

            if not activityData then
                self.view.timeNode.gameObject:SetActiveIfNecessary(false)
                return
            end
            local shopEndTime = activityData.endTime
            if shopEndTime then
                state = "CloseCd"
                self.view.titleStateText.text = self.m_activityShopInfo.closeCdText
                self.view.countDownText:InitCountDownText(shopEndTime, nil, function(leftTime)
                    local timeText = UIUtils.getLeftTime(leftTime)
                    return I18nUtils.CombineStringWithLanguageSpilt("", timeText)
                end)
            end
        end
    else
        state = "RefreshCd"
        self.view.countDownText:InitCountDownText(self:_CalculateTargetTime(shopTableData.shopRefreshCycleType), function()
            self:_RefreshTimeCountDown()
        end, function(leftTime)
            local timeText = UIUtils.getLeftTime(leftTime)
            return I18nUtils.CombineStringWithLanguageSpilt("", timeText)
        end)
    end

    if state then
        self.view.timeNode.gameObject:SetActiveIfNecessary(true)
        self.view.timeStateController:SetState(state)
    else
        self.view.timeNode.gameObject:SetActiveIfNecessary(false)
    end
end



ShopCtrl._CalculateTargetTime = HL.Method(GEnums.ShopRefreshCycleType).Return(HL.Number) << function(self, refreshCycleType)
    local time = self:_CalculateServerTargetTime(refreshCycleType)
    return time
end

ShopCtrl._CalculateServerTargetTime = HL.Method(GEnums.ShopRefreshCycleType).Return(HL.Number) << function(self, refreshCycleType)
    if refreshCycleType == GEnums.ShopRefreshCycleType.Daily then
        return Utils.getNextCommonServerRefreshTime()
    elseif refreshCycleType == GEnums.ShopRefreshCycleType.Weekly then
        return Utils.getNextWeeklyServerRefreshTime()
    elseif refreshCycleType == GEnums.ShopRefreshCycleType.Monthly then
        return Utils.getNextMonthlyServerRefreshTime()
    end
end

ShopCtrl.OnLimitChange = HL.Method(HL.Any) << function(self, data)
    local goods, left
    if type(data) == "table" then
        goods,left = unpack(data)
    end
    for i, v in ipairs(self.m_goodsInfos) do
        if v.id == goods then
            if left == 0 then
                table.insert(self.m_needPlaySoldOut, goods)
            end
            break
        end
    end
end

ShopCtrl.OnConditionChange = HL.Method(HL.Any) << function(self, data)
    local goods, unlock
    if type(data) == "table" then
        goods,unlock = unpack(data)
    end

    for i, v in ipairs(self.m_goodsInfos) do
        if v.id == goods then
            if unlock then
                table.insert(self.m_needPlayUnlock, goods)
            end
            break
        end
    end
end

ShopCtrl.OnBuyItemSucc = HL.Virtual(HL.Any) << function(self, arg)
    local goodsId = arg[1].GoodsId
    table.insert(self.m_lastBuyGoods, goodsId)
end

ShopCtrl.OnAfterBuyItemSucc = HL.Virtual() << function(self)
    for i, v in ipairs(self.m_goodsInfos) do
        for j, id in ipairs(self.m_needPlaySoldOut) do
            if v.id == id then
                local cell = self.m_getCellFunc(i)
                if cell then
                    cell:PlaySoldOutAnimation()
                end
                break
            end
        end

    end

    for i, v in ipairs(self.m_needPlaySoldOut) do
        for j, data in ipairs(self.m_goodsInfos) do
            if v == data.id then
                local cell = self.m_getCellFunc(j)
                if cell then
                    self:_RefreshContentCell(cell, j, "BuyItemSucc")
                end
            end
        end
    end

    for i, v in ipairs(self.m_needPlayUnlock) do
        for j, data in ipairs(self.m_goodsInfos) do
            if v == data.id then
                local cell = self.m_getCellFunc(j)
                if cell then
                    self:_RefreshContentCell(cell, j, "BuyItemSucc")
                end
            end
        end
    end
    self.m_needPlaySoldOut = {}
    self.m_needPlayUnlock = {}
    for i, v in ipairs(self.m_lastBuyGoods) do
        for j, data in ipairs(self.m_goodsInfos) do
            if v == data.id then
                local cell = self.m_getCellFunc(j)
                if cell then
                    self:_RefreshContentCell(cell, j, "BuyItemSucc")
                end
            end
        end
    end
    self.m_lastBuyGoods = {}
end

ShopCtrl.OnGoodsConditionSync = HL.Method(HL.Any) << function(self, goodsId)
    table.insert(self.m_conditionSyncGoods, goodsId)
    for i, v in ipairs(self.m_conditionSyncGoods) do
        for j, data in ipairs(self.m_goodsInfos) do
            if v == data.id then
                local cell = self.m_getCellFunc(j)
                if cell then
                    self:_RefreshContentCell(cell, j, "ConditionSync")
                end
            end
        end
    end
    self.m_conditionSyncGoods = {}
end

ShopCtrl.WaitAnimation = HL.Method(HL.Boolean) << function(self, state)
    self.m_waitAnimation = state
end

ShopCtrl.SetMoneyCell = HL.Virtual(HL.Boolean) << function(self, arg)

end

ShopCtrl._CollectPopupResumeState = HL.Method().Return(HL.Table) << function(self)
    local resumeOpenPanel = {}
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return resumeOpenPanel
    end


    local isInstructionOpen, instructionBookCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isInstructionOpen and instructionBookCtrl then
        table.insert(resumeOpenPanel, {
            panelId = PanelId.InstructionBook,
            arg = instructionBookCtrl.id,
        })
    end


    local isDetailOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isDetailOpen and shopDetailCtrl then
        local popupArg = shopDetailCtrl:GetCurPhaseStateArg()
        if popupArg then
            table.insert(resumeOpenPanel, {
                panelId = PanelId.ShopDetail,
                arg = popupArg,
            })
        end
    end
    return resumeOpenPanel
end

ShopCtrl._RestorePopupByResumeState = HL.Method(HL.Table) << function(self, panelInfo)
    if not panelInfo or panelInfo.panelId == nil then
        return
    end

    if panelInfo.panelId == PanelId.ShopDetail and self.m_phase then
        self.m_phase:CreatePhasePanelItem(panelInfo.panelId, panelInfo.arg)
        return
    end
    UIManager:Open(panelInfo.panelId, panelInfo.arg)
end

ShopCtrl._OnShopGoodsManualRefresh = HL.Virtual() << function(self)

end

ShopCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)

    return {
        shopGroupId = self.m_shopGroupId,
        shopId = self.m_shopId,
        unlockToastFunc = self.m_unlockToastFunc,
        resumeOpenPanel = self:_CollectPopupResumeState(),
    }
end

ShopCtrl.OnClose = HL.Override() << function(self)
    UIManager:ToggleBlockObtainWaysJump("common_shop", false)
    shopSystem:SetGoodsIdSee()
end


HL.Commit(ShopCtrl)
