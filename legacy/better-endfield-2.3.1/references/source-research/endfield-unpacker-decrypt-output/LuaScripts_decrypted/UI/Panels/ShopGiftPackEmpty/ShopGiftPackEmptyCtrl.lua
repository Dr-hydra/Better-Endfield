
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopGiftPackEmpty

local MONTHLY_PASS_CASHSHOPID = "MCard"
local NORMAL_CASHSHOP_GIFTPACK_PANEL_ID = PanelId.ShopPackage
local ALL_SHOP_ID = "All"


local USE_CASH_SHOP_IDS ={
    MCard = 1,
    Seasonal_Rec_pack = 2,
    Permanent_pack = 3,
    Newbie_pack = 4,
    SP_weapon_supply = 5,
}

ShopGiftPackEmptyCtrl = HL.Class('ShopGiftPackEmptyCtrl', uiCtrl.UICtrl)

ShopGiftPackEmptyCtrl.m_tabDataList = HL.Field(HL.Table)

ShopGiftPackEmptyCtrl.m_currTabCashShopId = HL.Field(HL.String) << ""


ShopGiftPackEmptyCtrl.m_isControllerTarget = HL.Field(HL.Boolean) << false


ShopGiftPackEmptyCtrl.m_allGiftPackGoodsByGroup = HL.Field(HL.Table)

ShopGiftPackEmptyCtrl.m_getTabCellFunc = HL.Field(HL.Function)

ShopGiftPackEmptyCtrl.m_needNaviGoodsId = HL.Field(HL.String) << ""


ShopGiftPackEmptyCtrl.m_isInTabClickFunc = HL.Field(HL.Boolean) << false


ShopGiftPackEmptyCtrl.m_haveSeenGoodsId = HL.Field(HL.Table)





ShopGiftPackEmptyCtrl.m_pendingAfterTopOrdered = HL.Field(HL.Table)





ShopGiftPackEmptyCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SDK_PRODUCT_INFO_UPDATE] = '_OnSdkProductInfoUpdate',
    [MessageConst.ON_CASH_SHOP_PLATFORM_DATA_REFRESH] = '_OnCashShopPlatformDataRefresh',
    [MessageConst.ON_SC_PAY_FREQUENCY_LIMIT_MODIFY] = '_OnCashShopFrequencyLimitDataRefresh',
    [MessageConst.CASH_SHOP_NEW_OPEN_GOODS] = '_OnCashShopNewOpenGoods',
    [MessageConst.ON_READ_CASH_SHOP_GOODS] = '_OnReadCashShopGoods',
}


ShopGiftPackEmptyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_phase:ShowPsStore()

    self.m_getTabCellFunc = UIUtils.genCachedCellFunction(self.view.cashShopVerticalTabList.scrollList)
    self.view.cashShopVerticalTabList.scrollList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getTabCellFunc(obj)
        local tabData = self.m_tabDataList[LuaIndex(index)]
        cell.cellNameTxt.text = tabData.name
        cell.cellNameShadownTxt.text = tabData.Name
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_OnTabClick(tabData, true)
            end
        end)

        cell.stateController:SetState("NOIcon")

        self:_SetupTabTag(cell, tabData)
    end)

    self.m_haveSeenGoodsId = {}

    self:_InitShortCut()

    self:_InitData()
    self:_RefreshUI()

    self:_ProcessArg(arg)
end

ShopGiftPackEmptyCtrl._ProcessArg = HL.Method(HL.Table) << function(self, arg)
    if arg ~= nil and arg.goodsId ~= nil and not string.isEmpty(arg.goodsId) then
        local goodsId = arg.goodsId
        arg.goodsId = nil
        local cashShopId = arg.cashShopId
        arg.cashShopId = nil


        self.m_pendingAfterTopOrdered = self.m_pendingAfterTopOrdered or {}
        table.insert(self.m_pendingAfterTopOrdered, function()
            self:ChooseTabByGoodsId(goodsId, true, cashShopId)
        end)
    elseif arg ~= nil and arg.cashShopId ~= nil and not string.isEmpty(arg.cashShopId) then
        local cashShopId = arg.cashShopId
        arg.cashShopId = nil
        self:ChooseTabByCashShopId(cashShopId, nil, true, arg.cashGoodsId)


        if arg.showInstructionBook then
            arg.showInstructionBook = nil
            self.m_pendingAfterTopOrdered = self.m_pendingAfterTopOrdered or {}
            table.insert(self.m_pendingAfterTopOrdered, function()
                UIManager:Open(PanelId.InstructionBook, "ShopPackage_All")
            end)
        end
    else
        if string.isEmpty(self.m_currTabCashShopId) then
            self:_SetTabByIndex(1, nil, true)
        end
    end
end

ShopGiftPackEmptyCtrl.SetCashShopStateArg = HL.Method(HL.Table) << function(self, arg)
    local isDetailOpen, detailCtrl = UIManager:IsOpen(PanelId.ShopGiftPackDetails)
    local isMonthlyDetailOpen, monthlyDetailCtrl = UIManager:IsOpen(PanelId.ShopMonthlyDetail)
    if isDetailOpen then

        local goodsId = detailCtrl:GetGoodsId()
        arg.goodsId = goodsId
        arg.cashShopId = self.m_currTabCashShopId
    elseif isMonthlyDetailOpen then

        local goodsId = monthlyDetailCtrl:GetGoodsId()
        arg.goodsId = goodsId
        arg.cashShopId = self.m_currTabCashShopId
    else

        arg.cashShopId = self.m_currTabCashShopId

        if UIManager:IsOpen(PanelId.InstructionBook) then
            arg.showInstructionBook = true
        end
    end
end




ShopGiftPackEmptyCtrl.OnAfterCategoryTopOrdered = HL.Method() << function(self)
    if not self.m_pendingAfterTopOrdered then
        return
    end
    local list = self.m_pendingAfterTopOrdered
    self.m_pendingAfterTopOrdered = nil
    for _, action in ipairs(list) do
        action()
    end
end

ShopGiftPackEmptyCtrl._SetupTabTag = HL.Method(HL.Any, HL.Table) << function(self, cell, tabData)


    local goodsIds = {}
    for _, cashGoodsInfo in ipairs(tabData.cashGoodsInfos) do
        table.insert(goodsIds, cashGoodsInfo.goodsId)
    end
    local isNew = CashShopUtils.CheckCashShopNewCashGoodsRedDot(goodsIds)
    cell.cellTagNode.tagNew.gameObject:SetActive(isNew)

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


ShopGiftPackEmptyCtrl._InitShortCut = HL.Method() << function(self)
    self:BindInputPlayerAction("cashshop_giftpack_goto_right", function()
        self:_OnGoRight()
    end, self.view.cashShopVerticalTabList.groupTarget.groupId)

    self:BindInputPlayerAction("cashshop_giftpack_goto_right_2", function()
        self:_OnGoRight()
    end, self.view.cashShopVerticalTabList.groupTarget.groupId)
end

ShopGiftPackEmptyCtrl._InitData = HL.Method() << function(self)
    self.m_allGiftPackGoodsByGroup = CashShopUtils.GetAllGiftPackGoodsByGroup()
    self.m_tabDataList = {}

    local cashShopTabList = {}
    local monthlyPassShopGoodsInfo = nil
    local allCashGoodsInfos = {}
    for _, groupData in ipairs(self.m_allGiftPackGoodsByGroup) do
        local cashShopId = groupData.cashShopId
        local cashGoodsInfos = {}
        for _, goodsInfo in ipairs(groupData.goodsInfos) do
            local info = goodsInfo
            local _, goodsDataCfg = Tables.GiftpackCashShopGoodsDataTable:TryGetValue(info.goodsId)
            if goodsDataCfg == nil then
                logger.error("[cashshop] Tables.GiftpackCashShopGoodsDataTable 缺少配置：" .. info.goodsId)
            else
                info.cashShopPriority = groupData.clientShowData.priority
                info.isMonthlyPass = cashShopId == MONTHLY_PASS_CASHSHOPID
                info.priority = goodsDataCfg and goodsDataCfg.priority or 100
                info.cashShopId = groupData.cashShopId
                info.cashShopDynamicPriority = groupData.clientShowData.dynamicPriority
                info.dynamicTag = goodsDataCfg.dynamicTag
                info.dynamicPriority = goodsDataCfg.dynamicPriority
                local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(info.goodsId)
                info.soldOutSortValue = canBuy and 0 or 1

                table.insert(cashGoodsInfos, info)
                table.insert(allCashGoodsInfos, info)
                if cashShopId == MONTHLY_PASS_CASHSHOPID then
                    monthlyPassShopGoodsInfo = info
                end
            end
        end

        local haveCfgTabData, cfgTabData = Tables.CashshopShopTabDataTable:TryGetValue(cashShopId)
        local tagList = {}
        if haveCfgTabData then
            for _, tag in pairs(cfgTabData.tagList) do
                table.insert(tagList, tag)
            end
        end

        local tabData = {
            cashShopId = cashShopId,
            priority = groupData.clientShowData.priority,
            isMonthlyPass = cashShopId == MONTHLY_PASS_CASHSHOPID,
            name = groupData.clientShowData.shopName,
            cashGoodsInfos = cashGoodsInfos,
            clientShowData = groupData.clientShowData,
            tagList = tagList,
        }
        table.insert(cashShopTabList, tabData)
    end

    local haveCfgTabData, cfgTabData = Tables.CashshopShopTabDataTable:TryGetValue(ALL_SHOP_ID)
    local tagList = {}
    if haveCfgTabData then
        for _, tag in pairs(cfgTabData.tagList) do
            table.insert(tagList, tag)
        end
    end
    local allTabData = {
        cashShopId = ALL_SHOP_ID,
        priority = 0,
        isMonthlyPass = false,
        name = Language.LUA_CASH_SHOP_GIFTPACK_ALL_TAB_NAME,
        cashGoodsInfos = allCashGoodsInfos,
        tagList = tagList,
    }
    table.insert(cashShopTabList, allTabData)

    for _, tabData in ipairs(cashShopTabList) do
        if tabData.isMonthlyPass then

            tabData.allSoldOut = not CashShopUtils.CheckCanBuyMonthlyPass()
        else
            local soldOutCount = 0
            for _, goodsInfo in ipairs(tabData.cashGoodsInfos) do
                local goodsId = goodsInfo.goodsId
                local limitGoodsData = GameInstance.player.cashShopSystem:GetPlatformLimitGoodsData(goodsId)
                if limitGoodsData ~= nil and limitGoodsData.limitType == CS.Beyond.Gameplay.CashShopSystem.EPlatformLimitGoodsType.Common then
                    local limitCount = limitGoodsData.limitCount
                    local purchaseCount = limitGoodsData.purchaseCount
                    if limitCount <= purchaseCount then
                        soldOutCount = soldOutCount + 1
                    end
                end
            end
            tabData.allSoldOut = soldOutCount == #tabData.cashGoodsInfos
        end

        if tabData.allSoldOut and tabData.clientShowData and tabData.clientShowData.setBottomWhenAllSoldOut then
            tabData.soldOutSortValue = 1
        else
            tabData.soldOutSortValue = 0
        end
    end

    table.sort(cashShopTabList, Utils.genSortFunction({ "soldOutSortValue", "priority" }, true))
    self.m_tabDataList = cashShopTabList
end

ShopGiftPackEmptyCtrl._RefreshUI = HL.Method() << function(self)
    self.view.cashShopVerticalTabList.scrollList:UpdateCount(#self.m_tabDataList)
end

ShopGiftPackEmptyCtrl._UpdateTabList = HL.Method() << function(self)
    self.view.cashShopVerticalTabList.scrollList:UpdateShowingCells(function(index, obj)
        local cell = self.m_getTabCellFunc(obj)
        local tabData = self.m_tabDataList[LuaIndex(index)]

        self:_SetupTabTag(cell, tabData)
    end)
end

ShopGiftPackEmptyCtrl._SetTabByIndex = HL.Method(HL.Int, HL.Opt(HL.Boolean, HL.Boolean, HL.String))
    << function(self, index, naviTarget, onCreate, cashGoodsId)
    if naviTarget == nil then
        naviTarget = true
    end
    if #self.m_tabDataList >= index then
        local obj = self.view.cashShopVerticalTabList.scrollList:Get(CSIndex(index))
        local cell = self.m_getTabCellFunc(obj)
        self.m_isControllerTarget = true
        cell.toggle:SetIsOnWithoutNotify(true)
        self:_OnTabClick(self.m_tabDataList[index], false, onCreate, cashGoodsId)
        if naviTarget then
            self:SetNaviTarget(cell.toggle)
        end
    end
end


ShopGiftPackEmptyCtrl._OnTabClick = HL.Method(HL.Table, HL.Boolean, HL.Opt(HL.Boolean, HL.String)) << function(self, tabData, userClick, onCreate, cashGoodsId)
    if self.m_currTabCashShopId == tabData.cashShopId then
        logger.info("click same tab")
        return
    end

    if self.m_isInTabClickFunc then
        return
    end
    self.m_isInTabClickFunc = true


    if userClick and not string.isEmpty(self.m_currTabCashShopId) then
        local currTabData = self:_GetTabDataByCashShopId(self.m_currTabCashShopId)
        if self.m_currTabCashShopId == MONTHLY_PASS_CASHSHOPID then

            local goodsIds = {}
            for _, info in ipairs(currTabData.cashGoodsInfos) do
                table.insert(goodsIds, info.goodsId)
            end
            GameInstance.player.cashShopSystem:ReadCashGoods(goodsIds)
        else
            if self.m_phase.m_panel2Item[PanelId.ShopPackage] ~= nil then
                local packageCtrl = self.m_phase.m_panel2Item[PanelId.ShopPackage].uiCtrl

                packageCtrl:UpdateSeeGoods(self.m_haveSeenGoodsId)
                GameInstance.player.cashShopSystem:ReadCashGoods(self.m_haveSeenGoodsId)
            end
        end
        self.m_haveSeenGoodsId = {}
    end

    if self.m_currTabCashShopId == MONTHLY_PASS_CASHSHOPID then
        self.m_phase:RemovePhasePanelItemByIdWrapper(PanelId.ShopMonthlyPass)
        self.m_phase:RemovePhasePanelItemByIdWrapper(PanelId.ShopMonthlyPass3D)
        self.m_phase:RemovePhasePanelItemByIdWrapper(PanelId.CashShopKrTips)
    else
        self.m_phase:RemovePhasePanelItemByIdWrapper(NORMAL_CASHSHOP_GIFTPACK_PANEL_ID)
    end

    if tabData.cashShopId == MONTHLY_PASS_CASHSHOPID then
        self.m_phase:CreateOrShowPhasePanelItemWrapper(PanelId.ShopMonthlyPass,
            {
                isRecommend = false,
                goodsId = tabData.cashGoodsInfos[1].goodsId,
                cashShopId = tabData.cashShopId,
            })
        self.m_phase:CreateOrShowPhasePanelItemWrapper(PanelId.ShopMonthlyPass3D,
            {
                isDailyPopup = false,
            })
        self.m_phase:CreateOrShowPhasePanelItemWrapper(PanelId.CashShopKrTips)
    else

        if tabData.cashShopId == Tables.cashShopConst.SpecialGiftPackShopId then
            CashShopUtils.TryCloseSpecialGiftPopup()
        end
        self.m_phase:CreateOrShowPhasePanelItemWrapper(NORMAL_CASHSHOP_GIFTPACK_PANEL_ID,
            {
                tabData = tabData,
                phase = self.m_phase,
                emptyCtrl = self,
                naviGoodsId = self.m_needNaviGoodsId,
                playAnimationIn = onCreate and true or false,
                cashGoodsId = cashGoodsId,
            })
        self.m_needNaviGoodsId = ""
    end

    if self.m_phase.m_needGameEvent then
        self.m_phase.m_needGameEvent = false
        EventLogManagerInst:GameEvent_ShopEnter(
            self.m_phase.m_enterButton,
            self.m_phase.m_enterPanel,
            tabData.cashShopId,
            CashShopConst.CashShopCategoryType.Pack,
            ""
        )
    else
        EventLogManagerInst:GameEvent_ShopPageView(
            tabData.cashShopId,
            CashShopConst.CashShopCategoryType.Pack,
            ""
        )
    end

    self.m_currTabCashShopId = tabData.cashShopId
    UIManager:SetTopOrder(PanelId.CashShop)
    UIManager:SetTopOrder(PanelId.ShopGiftPackEmpty)

    Notify(MessageConst.ON_CASH_SHOP_PACK_SET_TOP)


    if UIManager:IsShow(PanelId.RewardsPopUpForSystem) then
        UIManager:SetTopOrder(PanelId.RewardsPopUpForSystem)
    end

    self.m_isInTabClickFunc = false
end

ShopGiftPackEmptyCtrl._GetTabDataByCashShopId = HL.Method(HL.String).Return(HL.Any) << function(self, cashShopId)
    for _, tabData in ipairs(self.m_tabDataList) do
        if tabData.cashShopId == cashShopId then
            return tabData
        end
    end
    return nil
end



ShopGiftPackEmptyCtrl.ChooseTabByGoodsId = HL.Method(HL.String, HL.Boolean, HL.Opt(HL.String)).Return(HL.String)
    << function(self, goodsId, openDetailPanel, cashShopId)
    local foundTabData = nil
    local foundTabIndex = 0

    if cashShopId then
        local v, k = lume.match(self.m_tabDataList, function(tabData)
            return tabData.cashShopId == cashShopId
        end)
        if v then
            foundTabData = v
            foundTabIndex = k
        end
    end


    if foundTabData == nil then
        local v, k = lume.match(self.m_tabDataList, function(tabData)

            if tabData.cashShopId == ALL_SHOP_ID then
                return false
            end
            local cashGoodsInfos = tabData.cashGoodsInfos
            local foundGoodsInfo = lume.match(cashGoodsInfos, function(info)
                return info.goodsId == goodsId
            end)
            if foundGoodsInfo then
                return true
            end
        end)
        if v then
            foundTabData = v
            foundTabIndex = k
        end
    end

    if foundTabData ~= nil then
        self:_SetTabByIndex(foundTabIndex)




        if openDetailPanel then
            local foundInfo = lume.match(foundTabData.cashGoodsInfos, function(info)
                return info.goodsId == goodsId
            end)
            local isMonthlyPassGoods = foundInfo and foundInfo.isMonthlyPass
            if isMonthlyPassGoods then
                if foundTabData.cashShopId == ALL_SHOP_ID then
                    UIManager:Open(PanelId.ShopMonthlyDetail, {
                        goodsId = goodsId,
                        goodsInfo = foundInfo,
                    })
                else

                    CashShopUtils.TryBuyMonthlyPass(goodsId, foundTabData.cashShopId)
                end
            else
                UIManager:Open(PanelId.ShopGiftPackDetails, {
                    goodsId = goodsId,
                    cashShopId = foundInfo.cashShopId,
                })
            end
        end
        return foundTabData.cashShopId
    end
    return ""
end

ShopGiftPackEmptyCtrl.ChooseTabByCashShopId = HL.Method(HL.String, HL.Opt(HL.Boolean, HL.Boolean, HL.String))
    << function(self, cashShopId, naviTarget, onCreate, cashGoodsId)
    local foundTabData = nil
    local foundTabIndex = 0
    for i = 2, #self.m_tabDataList do
        local tabData = self.m_tabDataList[i]
        if tabData.cashShopId == cashShopId then
            foundTabData = tabData
            foundTabIndex = i
            break
        end
        if foundTabData ~= nil then
            break
        end
    end

    if foundTabData ~= nil then
        self:_SetTabByIndex(foundTabIndex, naviTarget, onCreate, cashGoodsId)
    else

        self:_SetTabByIndex(1, naviTarget, onCreate, cashGoodsId)
    end
end

ShopGiftPackEmptyCtrl._OnReceiveRefreshMsg = HL.Method() << function(self)
    logger.info("ShopGiftPackEmptyCtrl: 收到msg，刷新页面")

    local topPhaseId = PhaseManager:GetTopPhaseId()
    if topPhaseId ~= PhaseId.CashShop then
        logger.info("PhaseId.CashShop 不是最上层的phase, 不刷新")
        return
    end

    self:_InitData()
    self:_RefreshUI()

    if self.m_phase.m_panel2Item[PanelId.ShopPackage] ~= nil then
        local packageCtrl = self.m_phase.m_panel2Item[PanelId.ShopPackage].uiCtrl

        local goodsId = packageCtrl:GetCurrNaviGoodsId()
        self.m_needNaviGoodsId = goodsId
        InputManagerInst:ToggleGroup(self.view.cashShopVerticalTabList.groupTarget.groupId, false)

        packageCtrl:UpdateSeeGoods(self.m_haveSeenGoodsId)
    else
        self.m_needNaviGoodsId = ""
    end

    local prevCashShopId = self.m_currTabCashShopId
    self.m_currTabCashShopId = ""
    if not string.isEmpty(prevCashShopId) then

        self:ChooseTabByCashShopId(prevCashShopId, string.isEmpty(self.m_needNaviGoodsId))
    end
    if string.isEmpty(self.m_currTabCashShopId) then
        self:_SetTabByIndex(1)
    end

    Notify(MessageConst.ON_CASH_SHOP_RECEIVE_REFRESH_MSG)
end

ShopGiftPackEmptyCtrl._OnGoRight = HL.Method() << function(self)

    if self.m_phase.m_panel2Item[PanelId.ShopPackage] then
        logger.info("ShopGiftPackEmptyCtrl: _OnGoRight 被触发")
        InputManagerInst:ToggleGroup(self.view.cashShopVerticalTabList.groupTarget.groupId, false)
        local rightCtrl = self.m_phase.m_panel2Item[PanelId.ShopPackage].uiCtrl
        local succ = rightCtrl:TargetFirstCell()
        if not succ then
            InputManagerInst:ToggleGroup(self.view.cashShopVerticalTabList.groupTarget.groupId, true)
        end
    end

end

ShopGiftPackEmptyCtrl.NaviTargetCurrTab = HL.Method() << function(self)
    logger.info("ShopGiftPackEmptyCtrl: NaviTargetCurrTab")

    local groupId = self.view.cashShopVerticalTabList.groupTarget.groupId


    InputManagerInst:ToggleGroup(groupId, false)

    self:_StartCoroutine(function()
        coroutine.step()
        InputManagerInst:ToggleGroup(groupId, true)

        local foundTabData = nil
        local foundTabIndex = 0
        for i = 1, #self.m_tabDataList do
            local tabData = self.m_tabDataList[i]
            if tabData.cashShopId == self.m_currTabCashShopId then
                foundTabData = tabData
                foundTabIndex = i
                break
            end
            if foundTabData ~= nil then
                break
            end
        end

        if foundTabData ~= nil then
            local obj = self.view.cashShopVerticalTabList.scrollList:Get(CSIndex(foundTabIndex))
            local cell = self.m_getTabCellFunc(obj)
            self:SetNaviTarget(cell.toggle)
        end
    end)
end


ShopGiftPackEmptyCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.CASH_SHOP_SHOW_WALLET_BAR, {
        moneyIds = {Tables.globalConst.originiumItemId, Tables.globalConst.diamondItemId},
    })
end




ShopGiftPackEmptyCtrl.OnClose = HL.Override() << function(self)
end






ShopGiftPackEmptyCtrl._OnSdkProductInfoUpdate = HL.Method() << function(self)
    self:_OnReceiveRefreshMsg()
end

ShopGiftPackEmptyCtrl._OnCashShopPlatformDataRefresh = HL.Method() << function(self)
    self:_OnReceiveRefreshMsg()
end

ShopGiftPackEmptyCtrl._OnCashShopFrequencyLimitDataRefresh = HL.Method() << function(self)
    self:_OnReceiveRefreshMsg()
end

ShopGiftPackEmptyCtrl._OnCashShopNewOpenGoods = HL.Method() << function(self)
    self:_OnReceiveRefreshMsg()
end

ShopGiftPackEmptyCtrl._OnReadCashShopGoods = HL.Method() << function(self)
    self:_UpdateTabList()
end



HL.Commit(ShopGiftPackEmptyCtrl)
