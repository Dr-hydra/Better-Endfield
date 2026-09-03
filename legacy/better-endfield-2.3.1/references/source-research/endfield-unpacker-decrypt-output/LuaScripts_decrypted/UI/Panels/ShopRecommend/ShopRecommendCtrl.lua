
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopRecommend
















local tabDataPrototypeList = {
    BattlePass = {
        Panel = PanelId.ShopChoicenessBattlePass,
        ShowPsStore = false,
        isCashGoods = true,
        CheckShowFunc = function()
            return Utils.isSystemUnlocked(GEnums.UnlockSystemType.BPSystem) and BattlePassUtils.CheckBattlePassSeasonValid() and not BattlePassUtils.CheckPayTrackActive()
        end
    },
    MonthlyPass = {
        GetPanelIdsFunc = function()
            return {
                PanelId.ShopMonthlyPass,
                PanelId.ShopMonthlyPass3D,
            }
        end,
        OverrideCreatePanelFunc = function(self, tabData)
            self.m_phase:CreateOrShowPhasePanelItemWrapper(PanelId.ShopMonthlyPass,
                {
                    isRecommend = true,
                    goodsId = tabData.cashGoodsIds[1],
                    recommendId = tabData.id,
                })
            self.m_phase:CreateOrShowPhasePanelItemWrapper(PanelId.ShopMonthlyPass3D,
                {
                    isDailyPopup = false,
                })
            self.m_phase:CreateOrShowPhasePanelItemWrapper(PanelId.CashShopKrTips)
        end,
        OverrideDestroyPanelFunc = function(self)
            self.m_phase:RemovePhasePanelItemByIdWrapper(PanelId.ShopMonthlyPass)
            self.m_phase:RemovePhasePanelItemByIdWrapper(PanelId.ShopMonthlyPass3D)
            self.m_phase:RemovePhasePanelItemByIdWrapper(PanelId.CashShopKrTips)
        end,
        CheckBottomFunc = function()
            local canBuy = CashShopUtils.CheckCanBuyMonthlyPass()
            return not canBuy
        end,
        ShowPsStore = true,
        isCashGoods = true,
    },
    NewBieGift = {
        Panel = PanelId.ShopChoicenessGiftBag,
        CheckShowFunc = function(tabData)
            local goodsIds = tabData.cashGoodsIds

            local haveCanBuy = false
            for _, goodsId in ipairs(goodsIds) do
                local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId) and
                    CashShopUtils.CheckCashShopGoodsIsOpen(goodsId)
                if canBuy then
                    haveCanBuy = true
                    break
                end
            end
            return haveCanBuy
        end,
        ShowPsStore = true,
        isCashGoods = true,
    },
    DynamicGift = {
        Panel = PanelId.ShopDynamicGift,
        CheckShowFunc = function(tabData)
            local goodsIds = tabData.cashGoodsIds

            local haveCanBuy = false
            for _, goodsId in ipairs(goodsIds) do
                local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId) and
                    CashShopUtils.CheckCashShopGoodsIsOpen(goodsId)
                if canBuy then
                    haveCanBuy = true
                    break
                end
            end
            return haveCanBuy
        end,
        ShowPsStore = true,
        isCashGoods = true,
    },
    NewBieGiftGroup = {
        Panel = PanelId.ShopChoicenessGroupBag,
        CheckShowFunc = function(tabData)
            local goodsIds = tabData.cashGoodsIds

            local haveCanBuy = false
            for _, goodsId in ipairs(goodsIds) do
                local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId) and
                    CashShopUtils.CheckCashShopGoodsIsOpen(goodsId)
                if canBuy then
                    haveCanBuy = true
                    break
                end
            end
            return haveCanBuy
        end,
        ShowPsStore = true,
        isCashGoods = true,
        OnNaviGoRightFunc = function(self)
            local rightCtrl = self.m_phase.m_panel2Item[PanelId.ShopChoicenessGroupBag].uiCtrl
            local ret = rightCtrl:OnRecommendSetNaviTarget()
            return ret
        end,
        CheckNaviGoRightFunc = function(self)
            local rightCtrl = self.m_phase.m_panel2Item[PanelId.ShopChoicenessGroupBag].uiCtrl
            local ret = rightCtrl:CheckRecommendSetNaviTarget()
            return ret
        end,
    },
    Gift = {
        Panel = PanelId.ShopChoicenessGiftBag,
        ShowPsStore = true,
        isCashGoods = true,
    },
    Weapon = {
        Panel = PanelId.ShopChoicenessWeapon,
        CheckRedDotFunc = function(tabData)
            local _, box = GameInstance.player.shopSystem:GetNowUpWeaponData()
            if box == nil or box.Count <= 0 then
                return false
            end
            if #tabData.cashGoodsIds <= 0 then
                return false
            end
            local goodsId = tabData.cashGoodsIds[1]
            local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsId)
            if not goodsCfg then
                logger.error("【商城推荐页-武器池推荐tab】 goods表中不存在该id：" .. goodsId)
                return false
            end
            local isNew = GameInstance.player.shopSystem:IsNewGoodsId(goodsId)
            return isNew
        end,
        CheckShowFunc = function(tabData)
            local _, box = GameInstance.player.shopSystem:GetNowUpWeaponData()
            if box == nil or box.Count <= 0 then
                return false
            end
            if #tabData.cashGoodsIds <= 0 then
                return false
            end
            local goodsId = tabData.cashGoodsIds[1]
            local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsId)
            if not goodsCfg then
                logger.error("【商城推荐页-武器池推荐tab】 goods表中不存在该id：" .. goodsId)
                return false
            end
            local poolId = goodsCfg.weaponGachaPoolId

            local _, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
            if not poolInfo or not poolInfo.isOpenValid then
                return false
            end
            for _, singleBox in pairs(box) do
                if singleBox.goodsTemplateId == goodsId then
                    return true
                end
            end
            return false
        end,
    },
    GiftGroup = {
        Panel = PanelId.ShopChoicenessGroupBag,

        ShowPsStoreFunc = function(tabData)
            for _, goodsId in ipairs(tabData.cashGoodsIds) do
                local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId) and
                    CashShopUtils.CheckCashShopGoodsIsOpen(goodsId)
                if canBuy then
                    return true
                end
            end
            return false
        end,
        isCashGoods = true,
        OnNaviGoRightFunc = function(self)
            local rightCtrl = self.m_phase.m_panel2Item[PanelId.ShopChoicenessGroupBag].uiCtrl
            local ret = rightCtrl:OnRecommendSetNaviTarget()
            return ret
        end,
        CheckNaviGoRightFunc = function(self)
            local rightCtrl = self.m_phase.m_panel2Item[PanelId.ShopChoicenessGroupBag].uiCtrl
            local ret = rightCtrl:CheckRecommendSetNaviTarget()
            return ret
        end,
    },
    CustomGift = {
        Panel = PanelId.ShopRecommendEmptyGiftPack,
        CheckShowFunc = function(tabData)
            local goodsIds = tabData.cashGoodsIds

            local haveCanBuy = false
            for _, goodsId in ipairs(goodsIds) do
                local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId) and
                    CashShopUtils.CheckCashShopGoodsIsOpen(goodsId)
                if canBuy then
                    haveCanBuy = true
                    break
                end
            end
            return haveCanBuy
        end,
        ShowPsStore = true,
        isCashGoods = true,
    },
}

ShopRecommendCtrl = HL.Class('ShopRecommendCtrl', uiCtrl.UICtrl)














ShopRecommendCtrl.m_tabDataList = HL.Field(HL.Table)



ShopRecommendCtrl.m_showTabDataList = HL.Field(HL.Table)

ShopRecommendCtrl.m_currTabId = HL.Field(HL.String) << ""

ShopRecommendCtrl.m_getTabCellFunc = HL.Field(HL.Function)

ShopRecommendCtrl.m_goRightGroup = HL.Field(HL.Any)





ShopRecommendCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SDK_PRODUCT_INFO_UPDATE] = '_OnReceiveRefreshMsg',
    [MessageConst.ON_CASH_SHOP_PLATFORM_DATA_REFRESH] = '_OnReceiveRefreshMsg',
    [MessageConst.ON_READ_CASH_SHOP_GOODS] = '_OnReceiveRefreshMsg',
    [MessageConst.ON_SHOP_GOODS_SEE_GOODS_INFO_CHANGE] = '_OnShopGoodsSeeGoodsInfoChanged',
}




ShopRecommendCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_BindUI()
    self:_InitTabData()
    self:_InitShortCut()
    self:_RefreshShowTabData()

    self:_ProcessArg(arg)
end

ShopRecommendCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.CASH_SHOP_SHOW_WALLET_BAR, {
        moneyIds = {Tables.globalConst.originiumItemId, Tables.globalConst.diamondItemId},
    })
end




ShopRecommendCtrl.OnClose = HL.Override() << function(self)
end





ShopRecommendCtrl._BindUI = HL.Method() << function(self)
    self.m_getTabCellFunc = UIUtils.genCachedCellFunction(self.view.cashShopVerticalTabList.scrollList)
    self.view.cashShopVerticalTabList.scrollList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getTabCellFunc(obj)
        local tabData = self.m_showTabDataList[LuaIndex(index)]
        self:_SetupUIVerticalTab(cell, tabData)
    end)
end

ShopRecommendCtrl._InitShortCut = HL.Method() << function(self)
    self.m_goRightGroup = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)

    self:BindInputPlayerAction("cashshop_giftpack_goto_right", function()
        self:_OnGoRight()
    end, self.m_goRightGroup)

    self:BindInputPlayerAction("cashshop_giftpack_goto_right_2", function()
        self:_OnGoRight()
    end, self.m_goRightGroup)
end

ShopRecommendCtrl._ProcessArg = HL.Method(HL.Table) << function(self, arg)


    local tabId = ""
    if arg and not string.isEmpty(arg.CategoryRecommendTabId) then
        tabId = arg.CategoryRecommendTabId
        arg.CategoryRecommendTabId = ""
    end

    self.view.cashShopVerticalTabList.scrollList:UpdateCount(#self.m_showTabDataList)

    if string.isEmpty(tabId) then
        self:_SetTabByIndex(1)
    else
        self:SetCurrTabId(tabId)
    end
end


ShopRecommendCtrl._InitTabData = HL.Method() << function(self)
    self.m_tabDataList = {}
    for id, data in pairs(Tables.CashShopRecommendTable) do

        local tabData = self:_CreateRumtimeTabData(id, data)
        if tabData then
            table.insert(self.m_tabDataList, tabData)
        end
    end
end


ShopRecommendCtrl._CreateRumtimeTabData = HL.Method(HL.Any, HL.Any).Return(HL.Any) << function(self, id, data)
    if data == nil then
        _, data = Tables.cashShopRecommendTable:TryGetValue(id)
        if data == nil then
            logger.error("ShopRecommendCtrl._CreateRumtimeTabData: 检查id传入是否有问题: " .. id)
            return nil
        end
    end

    data = data

    local foundPrototype = tabDataPrototypeList[data.type]
    if foundPrototype == nil then
        logger.error("[CashShop]配置了没有实现的类型: " .. id)
        return nil
    end

    local cashGoodsIds = {}
    for _, goodsId in pairs(data.cashGoodsIdList) do
        table.insert(cashGoodsIds, goodsId)
    end

    local setNewPriorityCashGoodsIdList = {}
    for _, goodsId in pairs(data.setNewPriorityCashGoodsIdList) do
        table.insert(setNewPriorityCashGoodsIdList, goodsId)
    end

    local haveCfgTabData, cfgTabData = Tables.CashshopShopTabDataTable:TryGetValue(id)
    local tagList = {}
    if haveCfgTabData then
        for _, tag in pairs(cfgTabData.tagList) do
            table.insert(tagList, tag)
        end
    end


    local tabData = {
        id = id,
        type = data.type,
        Name = data.name,
        cashGoodsIds = cashGoodsIds,
        Priority = data.priority,
        tagList = tagList,
        prefabName = data.prefabName,
        setNewPriorityCashGoodsIdList = setNewPriorityCashGoodsIdList,
        newPriority = data.newPriority,
    }
    for k, v in pairs(foundPrototype) do
        tabData[k] = v
    end

    return tabData
end


ShopRecommendCtrl._RefreshShowTabData = HL.Method() << function(self)
    self.m_showTabDataList = {}
    local topShowList = {}
    local bottomShowList = {}
    for _, tabData in ipairs(self.m_tabDataList) do
        local showFunc = tabData.CheckShowFunc
        if showFunc == nil or showFunc(tabData) == true then
            local bottomFunc = tabData.CheckBottomFunc
            if bottomFunc == nil or bottomFunc() == false then
                table.insert(topShowList, tabData)
            else
                table.insert(bottomShowList, tabData)
            end
        else
            logger.info(string.format("[ShopRecommendCtrl] id:[%s] Name:[%s] CheckShowFunc未通过，不显示", tabData.id, tabData.Name))
        end
    end

    for _, tabData in ipairs(topShowList) do
        self:_TryChangeTabDataPriority(tabData)
    end
    for _, tabData in ipairs(bottomShowList) do
        self:_TryChangeTabDataPriority(tabData)
    end

    table.sort(topShowList, Utils.genSortFunction({ "Priority" }, true))
    table.sort(bottomShowList, Utils.genSortFunction({ "Priority" }, true))
    for _, tabData in ipairs(topShowList) do
        table.insert(self.m_showTabDataList, tabData)
    end
    for _, tabData in ipairs(bottomShowList) do
        table.insert(self.m_showTabDataList, tabData)
    end

    local willRemoveIdx = nil
    for i, tabData in ipairs(self.m_showTabDataList) do
        if tabData.type == "NewBieGiftGroup" then
            local found = nil
            for _, foundTabData in ipairs(self.m_showTabDataList) do
                if foundTabData.type == "NewBieGift" then
                    found = foundTabData
                    break
                end
            end
            if found ~= nil then
                willRemoveIdx = i
                break
            end
        end
    end
    if willRemoveIdx ~= nil then
        table.remove(self.m_showTabDataList, willRemoveIdx)
    end
end


ShopRecommendCtrl._TryChangeTabDataPriority = HL.Method(HL.Any) << function(self, tabData)

    tabData = tabData
    local setNewPriorityCashGoodsIdList = tabData.setNewPriorityCashGoodsIdList
    if #setNewPriorityCashGoodsIdList == 0 then
        return
    end


    local haveCanBuy = false
    for _, goodsId in ipairs(setNewPriorityCashGoodsIdList) do
        local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId) and
            CashShopUtils.CheckCashShopGoodsIsOpen(goodsId)
        if canBuy then
            haveCanBuy = true
            break
        end
    end

    if haveCanBuy then
        local newPriority = tabData.newPriority
        logger.info(string.format("ShopRecommendCtrl: tabData: %s 的priority发生变化: %s -> %s", tabData.id, tabData.Priority, newPriority))
        tabData.Priority = newPriority
    end
end





ShopRecommendCtrl._SetupUIVerticalTab = HL.Method(HL.Any, HL.Any) << function(self, cell, tabData)

    tabData = tabData
    cell.cellNameTxt.text = tabData.Name
    cell.cellNameShadownTxt.text = tabData.Name
    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_OnTabClick(tabData)
        end
    end)

    cell.stateController:SetState("NOIcon")




    local isNew = false
    if tabData.CheckRedDotFunc then
        isNew = tabData.CheckRedDotFunc(tabData)
        cell.cellTagNode.tagNew.gameObject:SetActive(isNew)
    elseif tabData.isCashGoods then
        local goodsIds = { }
        for _, goodsId in ipairs(tabData.cashGoodsIds) do
            local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId) and
                CashShopUtils.CheckCashShopGoodsIsOpen(goodsId)
            if canBuy then
                table.insert(goodsIds, goodsId)
            end
        end
        isNew = CashShopUtils.CheckCashShopNewCashGoodsRedDot(goodsIds)
        cell.cellTagNode.tagNew.gameObject:SetActive(isNew)
    end

    if isNew then
        return
    end
    local tagList = tabData.tagList
    local tagRoot = cell.cellTagNode
    for _, tagId in pairs(tagList) do
        local tagData = Tables.CashShopGiftPackTagTable[tagId]
        local style = tagData.style
        local value = tagData.value
        local tagCell = tagRoot[style]
        if tagCell ~= nil then
            tagCell.gameObject:SetActive(true)

            local haveValue = not string.isEmpty(value)
            local tagText = tagCell.tagText
            local line = tagCell.lineImg
            if tagText ~= nil then
                tagText.gameObject:SetActive(haveValue)
                tagText.text = value
            end
            if line ~= nil then
                line.gameObject:SetActive(haveValue)
            end
        end
    end
end





ShopRecommendCtrl.SetCashShopStateArg = HL.Method(HL.Table) << function(self, arg)
    arg.CategoryRecommendTabId = self.m_currTabId
end



ShopRecommendCtrl.OnAfterCategoryTopOrdered = HL.Method() << function(self)
end

ShopRecommendCtrl.NaviTargetCurrTab = HL.Method() << function(self)
    InputManagerInst:ToggleGroup(self.m_goRightGroup, true)

    local foundTabData = nil
    local foundIndex = 0
    for i = 1, #self.m_showTabDataList do
        local tabData = self.m_showTabDataList[i]
        if tabData.id == self.m_currTabId then
            foundTabData = tabData
            foundIndex = i
            break
        end
        if foundTabData ~= nil then
            break
        end
    end

    if foundTabData ~= nil then
        local obj = self.view.cashShopVerticalTabList.scrollList:Get(CSIndex(foundIndex))
        local cell = self.m_getTabCellFunc(obj)
        self:SetNaviTarget(cell.toggle)
    end
end

ShopRecommendCtrl.GetCurrTabId = HL.Method().Return(HL.String) << function(self)
    return self.m_currTabId
end

ShopRecommendCtrl.SetCurrTabId = HL.Method(HL.String) << function(self, tabId)
    local foundTabData = nil
    local foundIndex = 0
    for i = 1, #self.m_showTabDataList do
        local tabData = self.m_showTabDataList[i]
        if tabData.id == tabId then
            foundTabData = tabData
            foundIndex = i
            break
        end
        if foundTabData ~= nil then
            break
        end
    end
    if foundTabData ~= nil then
        self:_SetTabByIndex(foundIndex)
    end
end

ShopRecommendCtrl.ForceAddRuntimeTabDataToBottom = HL.Method(HL.Any) << function(self, id)
    logger.info("ShopRecommendCtrl: ForceAddRuntimeTabDataToBottom: " .. id)
    local tabData = self:_CreateRumtimeTabData(id)
    if tabData then
        table.insert(self.m_showTabDataList, tabData)
        self:_RefreshUI()
    end
end





ShopRecommendCtrl._OnGoRight = HL.Method() << function(self)
    local ret = false

    local tabData = self:_GetTabDataById(self.m_currTabId)
    if tabData.OnNaviGoRightFunc then
        ret = tabData.OnNaviGoRightFunc(self)
    end

    if ret then
        InputManagerInst:ToggleGroup(self.m_goRightGroup, false)
    end
end

ShopRecommendCtrl._CheckCanGoRight = HL.Method().Return(HL.Boolean) << function(self)
    local ret = false

    local tabData = self:_GetTabDataById(self.m_currTabId)
    if tabData.CheckNaviGoRightFunc then
        ret = tabData.CheckNaviGoRightFunc(self)
    end

    return ret
end

ShopRecommendCtrl._GetTabDataById = HL.Method(HL.String).Return(HL.Table) << function(self, id)
    for _, tabData in ipairs(self.m_tabDataList) do
        if tabData.id == id then
            return tabData
        end
    end
    return nil
end

ShopRecommendCtrl._RefreshUI = HL.Method() << function(self)
    self.view.cashShopVerticalTabList.scrollList:UpdateCount(#self.m_showTabDataList)
end

ShopRecommendCtrl._SetTabByIndex = HL.Method(HL.Int) << function(self, index)
    if #self.m_showTabDataList >= index then
        local obj = self.view.cashShopVerticalTabList.scrollList:Get(CSIndex(index))
        local cell = self.m_getTabCellFunc(obj)
        if DeviceInfo.usingController then
            self:SetNaviTarget(cell.toggle)
        else
            cell.toggle:SetIsOnWithoutNotify(true)
            self:_OnTabClick(self.m_showTabDataList[index])
        end
    end
end

ShopRecommendCtrl._OnTabClick = HL.Method(HL.Table) << function(self, tabData)
    if self.m_currTabId == tabData.id then
        logger.info("click same tab")
        return
    end


    local oldTabData = self:_GetTabDataById(self.m_currTabId)
    if oldTabData ~= nil then
        if oldTabData.OverrideDestroyPanelFunc ~= nil then
            oldTabData.OverrideDestroyPanelFunc(self)
        else
            self.m_phase:RemovePhasePanelItemByIdWrapper(oldTabData.Panel)
        end
    end

    if tabData.OverrideCreatePanelFunc ~= nil then
        tabData.OverrideCreatePanelFunc(self, tabData)
    else
        self.m_phase:CreateOrShowPhasePanelItemWrapper(tabData.Panel, tabData)
    end
    self.m_currTabId = tabData.id
    UIManager:SetTopOrder(PanelId.ShopRecommend)
    UIManager:SetTopOrder(PanelId.CashShop)


    self:_RefreshControllerHintPlaceHolder()


    if tabData.ShowPsStoreFunc ~= nil then
        local show = tabData.ShowPsStoreFunc(tabData)
        if show then
            self.m_phase:ShowPsStore()
        else
            self.m_phase:HidePsStore()
        end
    else
        if tabData.ShowPsStore then
            self.m_phase:ShowPsStore()
        else
            self.m_phase:HidePsStore()
        end
    end

    if self.m_phase.m_needGameEvent then
        self.m_phase.m_needGameEvent = false
        EventLogManagerInst:GameEvent_ShopEnter(
            self.m_phase.m_enterButton,
            self.m_phase.m_enterPanel,
            "",
            CashShopConst.CashShopCategoryType.Recommend,
            tabData.id
        )
    else
        EventLogManagerInst:GameEvent_ShopPageView(
            "",
            CashShopConst.CashShopCategoryType.Recommend,
            tabData.id
        )
    end


    if UIManager:IsShow(PanelId.RewardsPopUpForSystem) then
        UIManager:SetTopOrder(PanelId.RewardsPopUpForSystem)
    end
end

ShopRecommendCtrl._RefreshControllerHintPlaceHolder = HL.Method() << function(self)
    local args = {
        self.view.inputGroup.groupId,
        self.m_phase.cashShopCtrl.view.inputGroup.groupId,
    }

    local canGoRight = self:_CheckCanGoRight()
    InputManagerInst:ToggleGroup(self.m_goRightGroup, canGoRight)
    if canGoRight then
        table.insert(args, self.m_goRightGroup)
    end

    local tabData = self:_GetTabDataById(self.m_currTabId)
    if tabData.GetPanelIdsFunc then
        local panelIds = tabData.GetPanelIdsFunc()
        for _, panelId in ipairs(panelIds) do
            table.insert(args, self.m_phase.m_panel2Item[panelId].uiCtrl.view.inputGroup.groupId)
        end
    else
        table.insert(args, self.m_phase.m_panel2Item[tabData.Panel].uiCtrl.view.inputGroup.groupId)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(args)
end





ShopRecommendCtrl._OnReceiveRefreshMsg = HL.Method() << function(self)
    logger.info("ShopRecommendCtrl: 收到msg，刷新页面")
    self:_InitTabData()
    self:_RefreshShowTabData()
    self:_RefreshUI()
end

ShopRecommendCtrl._OnShopGoodsSeeGoodsInfoChanged = HL.Method(HL.Any) << function(self, arg)
    logger.info("ShopRecommendCtrl: 收到msg ON_SHOP_GOODS_SEE_GOODS_INFO_CHANGE，刷新页面")
    self:_InitTabData()
    self:_RefreshShowTabData()
    self:_RefreshUI()
end






HL.Commit(ShopRecommendCtrl)
