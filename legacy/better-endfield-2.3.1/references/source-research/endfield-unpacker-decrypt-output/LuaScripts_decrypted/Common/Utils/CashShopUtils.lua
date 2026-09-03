local CashShopUtils = {}




function CashShopUtils.TryUseMonthlyItem(itemId, instId)
    local canBuy = CashShopUtils.CheckCanBuyMonthlyPass()
    if not canBuy then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CANNOT_USE_MONTHLYCARDITEM)
        return
    end
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_CASHSHOP_MONTHLYCARD_ITEM_POP_UP_CONTENT,
        subContent = string.format(Language.LUA_CASHSHOP_MONTHLYCARD_ITEM_POP_UP_SUBCONTENT,
            GameInstance.player.monthlyPassSystem:GetRemainValidDays()),
        onConfirm = function()
            GameInstance.player.inventory:UseMonthlyCardItem(itemId, instId)
        end
    })
end

function CashShopUtils.GetMonthlyPassName()
    local id = Tables.CashShopConst.currentMonthlycardId
    local succ, goodsData = Tables.CashShopGoodsTable:TryGetValue(id)
    if succ then
        local name = goodsData.goodsName
        return name
    else
        return ""
    end
end

function CashShopUtils.CheckCanBuyMonthlyPass()
    local remainValidDays = GameInstance.player.monthlyPassSystem:GetRemainValidDays()
    if remainValidDays + Tables.CashShopConst.countMonthlycardCycle <= Tables.CashShopConst.maxAccumulateDays then
        return true
    else
        return false
    end
end


function CashShopUtils.TryBuyMonthlyPass(goodsId, shopId)
    local canBuy = CashShopUtils.CheckCanBuyMonthlyPass()
    if not canBuy then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CANNOT_BUY_MONTHLYPASS)
        return
    end
    EventLogManagerInst:GameEvent_GoodsViewClick(
        "2",
        shopId or "MCard",
        CashShopConst.CashShopCategoryType.Pack,
        goodsId
    )
    CashShopUtils.createOrder(goodsId, CashShopUtils.GetCashShopIdByGoodsId(goodsId), 1)
end

function CashShopUtils.MonthlyCardRemainDayIsShort()
    local remainValidDays = GameInstance.player.monthlyPassSystem:GetRemainValidDays()
    return remainValidDays <= Tables.CashShopConst.MonthlyCardShortThresholdDay
end

function CashShopUtils.GetMonthlyPassDailyRewardInfoList()
    local rewardInfoList = {}
    local monthlyPassId = Tables.CashShopConst.currentMonthlycardId
    local succ, cfg = Tables.ShopMonthlyPassRewardTable:TryGetValue(monthlyPassId)
    if not succ then
        return rewardInfoList
    end
    if cfg.rewardCount1 > 0 then
        table.insert(rewardInfoList, {
            rewardId = cfg.rewardId1,
            number = cfg.rewardCount1
        })
    end
    if cfg.rewardCount2 > 0 then
        table.insert(rewardInfoList, {
            rewardId = cfg.rewardId2,
            number = cfg.rewardCount2
        })
    end
    if cfg.rewardCount3 > 0 then
        table.insert(rewardInfoList, {
            rewardId = cfg.rewardId3,
            number = cfg.rewardCount3
        })
    end
    return rewardInfoList
end

function CashShopUtils.GetMonthlyPassMultiplyRewardInfoList()
    local baseList = CashShopUtils.GetMonthlyPassDailyRewardInfoList()
    local number = Tables.CashShopConst.countMonthlycardCycle
    local ret = {}
    for _, rewardInfo in ipairs(baseList) do
        table.insert(ret, {
            rewardId = rewardInfo.rewardId,
            number = rewardInfo.number * number
        })
    end
    return ret
end

function CashShopUtils.GetMonthlyPassImmediateRewardInfoList()
    local rewardInfoList = {}
    local monthlyPassId = Tables.CashShopConst.currentMonthlycardId
    local succ, cfg = Tables.CashShopGoodsTable:TryGetValue(monthlyPassId)
    if not succ then
        return rewardInfoList
    end
    local rewardId = cfg.rewardId
    local rewardsCfg = Tables.rewardTable[rewardId]
    for _, itemBundle in pairs(rewardsCfg.itemBundles) do
        local itemId = itemBundle.id
        local count = itemBundle.count
        table.insert(rewardInfoList, {
            rewardId = itemId,
            number = count
        })
    end
    return rewardInfoList
end

function CashShopUtils.TryAddMonthlyPassToMainHUDQueue()
    if GameInstance.player.monthlyPassSystem:GetNeedShowDailyPopupTimestamps().Count == 0 then
        logger.info("[cashshop] 没有可以弹的月卡popup")
    else
        if LuaSystemManager.mainHudActionQueue and not LuaSystemManager.mainHudActionQueue:HasRequest("MonthlyPassPopup") then
            LuaSystemManager.mainHudActionQueue:AddRequest("MonthlyPassPopup", function()
                logger.info("[cashshop] 准备弹月卡popup")

                local needShowTimeStamps = GameInstance.player.monthlyPassSystem:GetNeedShowDailyPopupTimestamps()
                if needShowTimeStamps.Count == 0 then
                    logger.info("[cashshop] 没有可以弹的月卡popup")
                    Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "MonthlyPassPopup")
                else
                    local needShowTimeStampsTable = {}
                    for _, ts in pairs(needShowTimeStamps) do
                        table.insert(needShowTimeStampsTable, ts)
                    end

                    local success = PhaseManager:OpenPhaseFast(PhaseId.ShopMonthlyPassPopUp, {
                        ShowTimeStamps = needShowTimeStampsTable,
                        EndCallback = function()
                            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "MonthlyPassPopup")
                        end
                    })
                    if not success then
                        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "MonthlyPassPopup")
                    end
                end
            end)
            logger.info("[cashshop] mainHudActionQueue中添加MonthlyPassPopup成功")
        else
            logger.info("[cashshop] mainHudActionQueue中已添加MonthlyPassPopup")
        end
    end
end





CashShopUtils.s_pendingFreeCashShopBuyList = {}




function CashShopUtils.getGoodsPriceText(goodsId, useSDKFormat)
    local haveCfg, cfg = Tables.giftpackCashShopGoodsDataTable:TryGetValue(goodsId)
    if haveCfg and cfg.isFree then
        return Language.LUA_CASH_SHOP_GOODS_FREE_PRICE
    end

    local cashShopSystem = GameInstance.player.cashShopSystem

    local productInfo = cashShopSystem:GetSDKProductInfo(goodsId)
    if productInfo and not string.isEmpty(productInfo.display_price) then
        local displayPrice = productInfo.display_price
        if useSDKFormat then
            return displayPrice
        end

        local sym, amt, isFront = cashShopSystem:ParseCurrency(displayPrice)
        if sym and amt then
            if isFront then
                return string.format("<size=60%%>%s</size><size=20%%> </size>%s", sym, amt)
            else
                return string.format("%s<size=20%%> </size><size=60%%>%s</size>", amt, sym)
            end
        else
            return displayPrice
        end
    else

        local isCNY = I18nUtils.curEnvLang == GEnums.EnvLang.CN
        local _, goodsData = Tables.cashShopGoodsTable:TryGetValue(goodsId)
        if goodsData then
            local function formatPrice(symbol, price, useSDKFormat)
                local priceStr = (price == math.floor(price)) and string.format("%d", price) or string.format("%.2f", price)
                if useSDKFormat then
                    return string.format("%s %s", symbol, priceStr)
                end
                return string.format("<size=60%%>%s</size><size=20%%> </size>%s", symbol, priceStr)
            end

            if isCNY then
                return formatPrice("¥", goodsData.priceCNY, useSDKFormat)
            else
                return formatPrice("$", goodsData.priceUSD, useSDKFormat)
            end
        else
            return ""
        end
    end
end





function CashShopUtils.createOrder(goodsId, cashShopId, count)

    local cashShopSystem = GameInstance.player.cashShopSystem
    local haveCfg, cfg = Tables.giftpackCashShopGoodsDataTable:TryGetValue(goodsId)
    if haveCfg and cfg.isFree then

        CashShopUtils.recordFreeCashShopBuy(goodsId, count)
        cashShopSystem:CreateFreeOrder(goodsId, cashShopId, count)
        return
    end

    if CashShopUtils.IsPS() then
        local content = ""
        local name = CashShopUtils.GetCashGoodsName(goodsId)

        local productInfo = cashShopSystem:GetSDKProductInfo(goodsId)
        if productInfo then
            content = productInfo.desc
        end
        UIManager:Open(PanelId.CommonBook, {
            title = name and name or Language.LUA_CASH_SHOP_PS_ITEM_DIALOG_TIILE,
            content = content,
            onConfirm = function()
                cashShopSystem:CreateOrder(goodsId, cashShopId, count)
            end,
        })
    else
        cashShopSystem:CreateOrder(goodsId, cashShopId, count)
    end
end



function CashShopUtils.acceptOrders(orderIds)
    GameInstance.player.cashShopSystem:AcceptOrder(orderIds)
    Notify(MessageConst.ON_ACCEPT_ORDERS, orderIds)
end





function CashShopUtils.showOrderSettle(orderSettle, onClose, interrupt)
    local function showReward()
        local rewardItems = CashShopUtils.getOrderSettleRewardItems(orderSettle)
        if #rewardItems == 0 then
            CashShopUtils.acceptOrders({ orderSettle.OrderId })
            if onClose then
                onClose()
            end
            return
        end
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            items = rewardItems,
            onComplete = function()
                CashShopUtils.acceptOrders({ orderSettle.OrderId })
                if onClose then
                    onClose()
                end
            end,
            interrupt = interrupt,
        })
    end

    if orderSettle.DuplicateCnt > 0 then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_CASH_SHOP_ORDER_REBATE_TIPS,
            hideCancel = true,
            toggleInMainHud = true,
            onConfirm = function()
                showReward()
            end,
            interrupt = interrupt
        })
    else
        showReward()
    end
end






function CashShopUtils.showOrderSettleList(orderSettleList, onClose, webTips, interrupt)
    local function showReward()
        local allRewardItems = {}
        local orderIds = {}


        for _, orderSettle in ipairs(orderSettleList) do
            local rewardItems = CashShopUtils.getOrderSettleRewardItems(orderSettle)
            for _, item in ipairs(rewardItems) do
                table.insert(allRewardItems, item)
            end
            table.insert(orderIds, orderSettle.OrderId)
        end

        if #allRewardItems == 0 then
            CashShopUtils.acceptOrders(orderIds)
            if onClose then
                onClose()
            end
            return
        end

        local function showSystemRewards()
            Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
                items = allRewardItems,
                onComplete = function()
                    CashShopUtils.acceptOrders(orderIds)
                    if onClose then
                        onClose()
                    end
                end,
                interrupt = interrupt,
            })
        end

        if not string.isEmpty(webTips) then
            Notify(MessageConst.SHOW_POP_UP, {
                content = webTips,
                hideCancel = true,
                toggleInMainHud = true,
                onConfirm = showSystemRewards,
                interrupt = interrupt,
            })
        else
            showSystemRewards()
        end
    end

    local hasDuplicateOrder = false
    for _, orderSettle in ipairs(orderSettleList) do
        if orderSettle.DuplicateCnt > 0 then
            hasDuplicateOrder = true
            break
        end
    end

    if hasDuplicateOrder then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_CASH_SHOP_ORDER_REBATE_TIPS,
            hideCancel = true,
            toggleInMainHud = true,
            onConfirm = function()
                showReward()
            end,
            interrupt = interrupt,
        })
    else
        showReward()
    end
end



function CashShopUtils.recordFreeCashShopBuy(goodsId, count)
    table.insert(CashShopUtils.s_pendingFreeCashShopBuyList, {
        goodsId = goodsId,
        count = count,
    })
end


function CashShopUtils.popFreeCashShopBuy()
    if #CashShopUtils.s_pendingFreeCashShopBuyList == 0 then
        return nil
    end
    return table.remove(CashShopUtils.s_pendingFreeCashShopBuyList, 1)
end





function CashShopUtils.getFreeCashShopRewardItems(goodsId, count)
    local result = {}
    local _, goodsData = Tables.cashShopGoodsTable:TryGetValue(goodsId)
    if not goodsData or string.isEmpty(goodsData.rewardId) then
        return result
    end
    local _, rewardTableData = Tables.rewardTable:TryGetValue(goodsData.rewardId)
    if not rewardTableData then
        return result
    end
    local buyCount = count or 1
    for _, itemBundle in pairs(rewardTableData.itemBundles) do
        table.insert(result, { id = itemBundle.id, count = itemBundle.count * buyCount })
    end
    return result
end




function CashShopUtils.getOrderSettleRewardItems(orderSettle)
    local result = {}
    local bonusCount = orderSettle.OnceBonusCnt
    local normalCount = orderSettle.Quantity - orderSettle.OnceBonusCnt - orderSettle.DuplicateCnt
    local _, goodsData = Tables.cashShopGoodsTable:TryGetValue(orderSettle.GoodsId)
    if not goodsData then
        logger.error("CashShopUtils.showOrderSettle no goods for id:", orderSettle.GoodsId)
        return result
    end
    local bonusRewardId
    local rewardTimes = 1
    local _, rechargeData = Tables.cashShopRechargeTable:TryGetValue(orderSettle.GoodsId)
    if rechargeData then
        bonusRewardId = rechargeData.bonusRewardId
        rewardTimes = rechargeData.rewardTimes
    end

    local itemTable = {}
    local _, rewardTableData = Tables.rewardTable:TryGetValue(goodsData.rewardId)
    if rewardTableData then

        for _, itemBundle in pairs(rewardTableData.itemBundles) do
            local count = itemBundle.count * normalCount + bonusCount * itemBundle.count * rewardTimes
            if itemTable[itemBundle.id] == nil then
                itemTable[itemBundle.id] = count
            else
                itemTable[itemBundle.id] = itemTable[itemBundle.id] + count
            end
        end
    end

    if not string.isEmpty(bonusRewardId) and normalCount > 0 then
        local _, rewardTableData = Tables.rewardTable:TryGetValue(bonusRewardId)
        if rewardTableData then

            for _, itemBundle in pairs(rewardTableData.itemBundles) do
                local count = itemBundle.count * normalCount
                if itemTable[itemBundle.id] == nil then
                    itemTable[itemBundle.id] = count
                else
                    itemTable[itemBundle.id] = itemTable[itemBundle.id] + count
                end
            end
        end
    end
    for id, count in pairs(itemTable) do
        if count > 0 then
            table.insert(result, { id = id, count = count })
        end
    end
    return result
end


function CashShopUtils.haveRemainOrders()
    return GameInstance.player.cashShopSystem.remindOrderList.Count > 0
end




function CashShopUtils.tryShowRemainOrderList(endCallback, interrupt)

    local remainOrders = GameInstance.player.cashShopSystem.remindOrderList
    local normalOrders = {}
    local webOrders = {}
    for i = 0, remainOrders.Count - 1 do
        local orderSettle = remainOrders[i]
        if orderSettle.IsWeb then
            table.insert(webOrders, orderSettle)
        else
            table.insert(normalOrders, orderSettle)
        end
    end

    local function endCallbackWrap()
        Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = CashShopConst.RemainOrderMainHudKey, isInMainHud = true })
        if endCallback then
            endCallback()
        end
    end
    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = CashShopConst.RemainOrderMainHudKey, isInMainHud = false })


    CashShopUtils.showNormalOrderSettles(normalOrders, function()

        CashShopUtils.showWebOrderSettles(webOrders, endCallbackWrap, interrupt)
    end, interrupt)
end





function CashShopUtils.showNormalOrderSettles(orderSettleList, endCallback, interrupt)
    local function showNext()
        if not next(orderSettleList) then
            if endCallback then
                endCallback()
            end
            return
        end

        local orderSettle = table.remove(orderSettleList, 1)
        CashShopUtils.showOrderSettle(orderSettle, function()
            showNext()
        end, interrupt)
    end

    showNext()
end





function CashShopUtils.showWebOrderSettles(orderSettleList, endCallback, interrupt)
    if not next(orderSettleList) then
        if endCallback then
            endCallback()
        end
        return
    end


    local webOrderList = {}
    local rechargeOrder = {
        tips = CashShopConst.WebOrderSettleTips[GEnums.CashGoodsType.Recharge],
        orderSettleList = {},
    }
    for _, orderSettle in ipairs(orderSettleList) do
        if orderSettle.IsWeb then
            local _, goodsData = Tables.cashShopGoodsTable:TryGetValue(orderSettle.GoodsId)
            if goodsData then
                if goodsData.goodsType == GEnums.CashGoodsType.Recharge then
                    table.insert(rechargeOrder.orderSettleList, orderSettle)
                else
                    table.insert(webOrderList, {
                        tips = CashShopConst.WebOrderSettleTips[goodsData.goodsType] or "",
                        orderSettleList = { orderSettle },
                    })
                end
            end
        end
    end
    if #rechargeOrder.orderSettleList > 0 then
        table.insert(webOrderList, 1, rechargeOrder)
    end
    local function showNext()
        if not next(webOrderList) then
            if endCallback then
                endCallback()
            end
            return
        end

        local webOrder = table.remove(webOrderList, 1)
        CashShopUtils.showOrderSettleList(webOrder.orderSettleList, function()
            showNext()
        end, webOrder.tips, interrupt)
    end
    showNext()
end





function CashShopUtils.GetCashGoodsName(id)
    local succ, cfg = Tables.CashShopGoodsTable:TryGetValue(id)
    if not succ then
        return ""
    end
    return cfg.goodsName
end



function CashShopUtils.GetCashGoodsStartDateAndTime(id)
    local data = GameInstance.player.cashShopSystem:GetGoodsData(id)
    local ts = data.openTimeStamp
    if ts == 0 then
        return nil, nil
    end
    local dateTime = DateTimeUtils.TimeStamp2ServerTime(ts)
    return string.format("%s/%s", dateTime.Month, dateTime.Day),
        dateTime:ToString("HH:mm")
end

function CashShopUtils.GetCashGoodsEndDateAndTime(id)
    local data = GameInstance.player.cashShopSystem:GetGoodsData(id)
    local ts = data.closeTimeStamp
    if ts == 0 then
        return nil, nil
    end
    local dateTime = DateTimeUtils.TimeStamp2ServerTime(ts)
    return string.format("%s/%s", dateTime.Month, dateTime.Day),
        dateTime:ToString("HH:mm")
end


function CashShopUtils.GetCashGoodsImage(id)
    local succ, cfg = Tables.CashShopGoodsTable:TryGetValue(id)
    if succ then
        return UIConst.UI_SPRITE_SHOP_GROUP_BAG, cfg.iconId
    end
    return nil, nil
end

function CashShopUtils.GetGiftpackBigIcon(id)
    local succ, cfg = Tables.GiftpackCashShopGoodsDataTable:TryGetValue(id)
    if succ then
        return UIConst.UI_SPRITE_SHOP_GROUP_BAG, cfg.bigIcon
    end
    return nil, nil
end

function CashShopUtils.GetGiftpackBigBg(id)
    local succ, cfg = Tables.GiftpackCashShopGoodsDataTable:TryGetValue(id)
    if succ then
        return UIConst.UI_SPRITE_SHOP_GROUP_BAG, cfg.bigBg
    end
    return nil, nil
end


function CashShopUtils.CheckCashShopGoodsIsOpen(id)
    local goodsData = GameInstance.player.cashShopSystem:GetGoodsData(id)
    if goodsData == nil then
        return false
    end

    return true
end



function CashShopUtils.CheckCanBuyCashShopGoods(id)
    local goodsData = GameInstance.player.cashShopSystem:GetGoodsData(id)
    if goodsData == nil then
        return false
    end

    local canBuy = goodsData:CheckCanBuy()
    return canBuy
end

function CashShopUtils.GetRestrictionTagTextByLimitType(type)
    if type == CS.Beyond.GEnums.CashShopAvailRefreshType.None then
        return Language.LUA_CASH_SHOP_TAG_RESTRICTION_None
    end
    if type == CS.Beyond.GEnums.CashShopAvailRefreshType.Daily then
        return Language.LUA_CASH_SHOP_TAG_RESTRICTION_Daily
    end
    if type == CS.Beyond.GEnums.CashShopAvailRefreshType.Weekly then
        return Language.LUA_CASH_SHOP_TAG_RESTRICTION_Weekly
    end
    if type == CS.Beyond.GEnums.CashShopAvailRefreshType.Monthly then
        return Language.LUA_CASH_SHOP_TAG_RESTRICTION_Monthly
    end
    if type == CS.Beyond.GEnums.CashShopAvailRefreshType.SubVersion then
        return Language.LUA_CASH_SHOP_TAG_RESTRICTION_SubVersion
    end
    if type == CS.Beyond.GEnums.CashShopAvailRefreshType.Version then
        return Language.LUA_CASH_SHOP_TAG_RESTRICTION_Version
    end
end


function CashShopUtils.GetAllGiftPackGoodsByGroup()
    local ret = {}

    for cashShopId, showData in pairs(Tables.GiftpackCashShopClientShowDataTable) do
        if cashShopId ~= "BP" then

            local shopData = GameInstance.player.cashShopSystem:GetShopData(cashShopId)
            if shopData ~= nil and shopData:IsOpen() then
                local goodsList = GameInstance.player.cashShopSystem:GetGoodsList(shopData)
                local goodsInfos = {}
                local canBuyGoodsCount = 0
                for i = 0, goodsList.Count - 1 do
                    local goodsInfo = {
                        goodsId = goodsList[i].goodsId,
                        goodsData = goodsList[i],
                    }
                    local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsList[i].goodsId)

                    local _, giftpackCfg = Tables.giftpackCashShopGoodsDataTable:TryGetValue( goodsList[i].goodsId)
                    if not (giftpackCfg and giftpackCfg.dontShowWhenSellOut and not canBuy) then
                        table.insert(goodsInfos, goodsInfo)
                        if canBuy then
                            canBuyGoodsCount = canBuyGoodsCount + 1
                        end
                    end
                end
                if #goodsInfos > 0 and

                    (not CashShopUtils.IsOnceCashShop(cashShopId) or canBuyGoodsCount > 0) then
                    table.insert(ret, {
                        cashShopId = cashShopId,
                        shopData = shopData,
                        clientShowData = showData,
                        goodsInfos = goodsInfos,
                    })
                end
            end
        end
    end
    return ret
end


function CashShopUtils.GetAllGiftPackGoodsIds()
    local ret = {}
    local allGroupData = CashShopUtils.GetAllGiftPackGoodsByGroup()
    for _, groupData in ipairs(allGroupData) do
        for _, goodsInfo in ipairs(groupData.goodsInfos) do
            table.insert(ret, goodsInfo.goodsId)
        end
    end
    return ret
end


function CashShopUtils.CheckAllGiftPackIsNew()
    local ids = CashShopUtils.GetAllGiftPackGoodsIds()
    local new = CashShopUtils.CheckCashShopNewCashGoodsRedDot(ids)
    return new
end

function CashShopUtils.GetCashShopIdByGoodsId(goodsId)
    local data = Tables.CashShopGoodsTable[goodsId]
    return data.cashShopId
end

function CashShopUtils.CheckCashShopNewCashGoodsRedDot(goodsIds)
    for _, goodsId in ipairs(goodsIds) do
        local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId)
        local isNew = GameInstance.player.cashShopSystem:IsNewGoods(goodsId)
        if canBuy and isNew then
            return true
        end
    end
    return false
end


function CashShopUtils.IsOnceCashShop(cashShopId)
    local onceCashShopIds = Tables.CashShopConst.OnceCashShopIds
    for _, id in pairs(onceCashShopIds) do
        if cashShopId == id then
            return true
        end
    end
    return false
end

function CashShopUtils.ShowKrUrlBtn()
    local isKR = I18nUtils.curEnvLang == GEnums.EnvLang.KR and CS.Beyond.SDK.SDKConsts.IsOverseaVersion()
    return isKR
end




function CashShopUtils.TryOpenSpecialGiftPopup()
    local packId = GameInstance.player.cashShopSystem.waitShowSpecialGiftPackId
    if string.isEmpty(packId) then
        return
    end

    UIManager:AutoOpen(PanelId.ShopSpecialGiftPackPopup, packId)
end


function CashShopUtils.CheckHaveSpecialGiftShop()
    local ret = false
    local retGoodsId = nil
    local specialShopId = Tables.cashShopConst.SpecialGiftPackShopId

    local cashShopData = GameInstance.player.cashShopSystem:GetShopData(specialShopId)
    if cashShopData ~= nil and cashShopData:IsOpen() then
        local goodsList = GameInstance.player.cashShopSystem:GetGoodsList(cashShopData)

        for _, goodsData in pairs(goodsList) do
            if goodsData:IsOpen() then

                local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsData.goodsId)
                if canBuy then
                    ret = true
                    if retGoodsId == nil or
                        goodsData.goodsId == GameInstance.player.cashShopSystem.waitShowSpecialGiftPackId then
                        retGoodsId = goodsData.goodsId
                    end
                end
            end
        end
    end
    return ret, retGoodsId
end

function CashShopUtils.TryFadeSpecialGiftPopup()

    local isOpen, ctrl = UIManager:IsOpen(PanelId.ShopSpecialGiftPackPopup)
    if isOpen then
        ctrl:Fade()
    end
end

function CashShopUtils.TryCloseSpecialGiftPopup()

    local isOpen, ctrl = UIManager:IsOpen(PanelId.ShopSpecialGiftPackPopup)
    if isOpen then
        ctrl:Exit()
    end
end




function CashShopUtils.TabDataShowPsStore(tabData)
    if tabData.ShowPsStoreFunc ~= nil then
        local show = tabData.ShowPsStoreFunc(tabData)
        return show
    else
        return tabData.ShowPsStore
    end
end






function CashShopUtils.GetRemainPoolGotCount(poolId)
    local poolInfo = GameInstance.player.gacha.poolInfos:get_Item(poolId)
    local poolData = poolInfo.data
    local typeData = Tables.gachaWeaponPoolTypeTable[poolData.type]
    if poolInfo.upGotCount > 0 or poolInfo.hardGuaranteeProgress > typeData.hardGuarantee then

        return -1
    else
        return math.ceil((typeData.hardGuarantee - poolInfo.hardGuaranteeProgress) / 10)
    end
end












function CashShopUtils.GetGachaWeaponLoopRewardInfo(poolId)
    local gachaPoolCfg = Tables.gachaWeaponPoolTable[poolId]
    local gachaTypeCfg = Tables.gachaWeaponPoolTypeTable[gachaPoolCfg.type]
    local _, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
    local loopRewardInfoCount = gachaPoolCfg.intervalAutoRewardIds.Count
    local startPullCount = gachaTypeCfg.intervalAutoRewardStartPullCount
    local perPullCount = gachaTypeCfg.intervalAutoRewardPerPullCount
    if loopRewardInfoCount == 0 then
        logger.error(string.format("武器卡池[%s]累抽奖励为空！请检查配置", poolId))
        return nil
    end

    local curRound = poolInfo.loopCumulateRewardReceivedRounds
    local curRoundRemoveLoop = curRound % loopRewardInfoCount
    local curPullCount = poolInfo.totalPullCount
    local curPullCountRemoveLoop = curPullCount <= startPullCount and
        curPullCount or
        (curPullCount - startPullCount) % (loopRewardInfoCount * perPullCount) + startPullCount

    local needPullCountTable = {}
    for i = 1, loopRewardInfoCount do
        local needCount = 0
        if curRoundRemoveLoop >= i then
            needCount = startPullCount + (i + loopRewardInfoCount) * perPullCount - curPullCountRemoveLoop
        else
            needCount = startPullCount + i * perPullCount - curPullCountRemoveLoop
        end
        table.insert(needPullCountTable, needCount)
    end

    local infos = {}
    for i = 1, loopRewardInfoCount do
        local rewardId = gachaPoolCfg.intervalAutoRewardIds[CSIndex(i)]
        local rewardItems = UIUtils.getRewardItems(rewardId)
        local itemId = rewardItems[1].id
        local itemCfg = Tables.itemTable[itemId]

        local info = {
            rewardId = rewardId,
            itemId = itemId,
            name = itemCfg.name,
            remainNeedPullCount = math.ceil(needPullCountTable[i] / 10),
            isWeaponItemCase = itemCfg.type == GEnums.ItemType.ItemCase,

            loopRewardTagName = gachaPoolCfg.loopRewardShowTag,
            loopRewardTagTitle = gachaPoolCfg.loopRewardShowTitle,
        }
        table.insert(infos, info)
    end
    return infos
end


function CashShopUtils.ShowWikiWeaponPreview(poolId, weaponId)

    local poolInfo = GameInstance.player.gacha.poolInfos:get_Item(poolId)
    local poolData = poolInfo.data
    local contentData = Tables.gachaWeaponPoolContentTable[poolId]

    local upWeaponGroupData = {
        title = Language.LUA_WEAPON_PREVIEW_UP_TITLE,
        weaponIds = {}
    }
    for _, weaponId in pairs(poolData.upWeaponIds) do
        table.insert(upWeaponGroupData.weaponIds, weaponId)
    end
    local possibleWeaponGroupData = {
        title = Language.LUA_WEAPON_PREVIEW_POSSIBLE_TITLE,
        weaponIds = {}
    }
    for _, v in pairs(contentData.list) do
        table.insert(possibleWeaponGroupData.weaponIds, v.itemId)
    end

    WikiUtils.showWeaponPreview({
        weaponId = weaponId,
        weaponGroups = {
            upWeaponGroupData,
            possibleWeaponGroupData
        },
    })
end


function CashShopUtils.TryGetBuyGachaWeaponGoodsCostInfo(shopId, goodsId)
    local buyCount = 1
    local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsId)
    local hasGachaCfg, weaponGachaCfg = Tables.gachaWeaponPoolTable:TryGetValue(goodsCfg.weaponGachaPoolId)
    if not hasGachaCfg then
        return nil
    end
    local info = {
        costTicketId = "",
        costTicketCount = 0,
        curTicketCount = 0,
        ticketEnough = false,

        costMoneyId = "",
        costMoneyCount = 0,
        curMoneyCount = 0,
        moneyEnough = false,
    }
    if not string.isEmpty(weaponGachaCfg.ticketGachaTenLt) then
        info.costTicketId = weaponGachaCfg.ticketGachaTenLt
        info.costTicketCount = buyCount
        info.curTicketCount = Utils.getItemCount(info.costTicketId)
        info.ticketEnough = info.costTicketCount ~= 0 and info.curTicketCount >= info.costTicketCount
    end

    local goodsData = GameInstance.player.shopSystem:GetShopGoodsData(shopId, goodsId)
    info.costMoneyId = goodsCfg.moneyId
    info.costMoneyCount = math.floor(goodsCfg.price * buyCount * goodsData.discount)
    info.curMoneyCount = Utils.getItemCount(info.costMoneyId)
    info.moneyEnough = info.curMoneyCount >= info.costMoneyCount

    return info
end


function CashShopUtils.TryGetWeaponByWeaponId(weaponId)

    local _, upBox, _ = GameInstance.player.shopSystem:GetNowUpWeaponData()
    local ret, func = CashShopUtils.TryGetWeaponByWeaponIdInBox(weaponId, upBox)
    if ret then
        return ret, func
    end

    local _, weeklyBox, weeklyGoods = GameInstance.player.shopSystem:GetWeeklyWeaponData()
    ret, func = CashShopUtils.TryGetWeaponByWeaponIdInGoods(weaponId, weeklyGoods)
    if ret then
        return ret, func
    end

    local _, dailyBox, dailyGoods = GameInstance.player.shopSystem:GetDailyWeaponData()
    ret, func = CashShopUtils.TryGetWeaponByWeaponIdInGoods(weaponId, dailyGoods)
    if ret then
        return ret, func
    end

    local _, box, goods = GameInstance.player.shopSystem:GetPermanentWeaponShopData()
    ret, func = CashShopUtils.TryGetWeaponByWeaponIdInBox(weaponId, box)
    if ret then
        return ret, func
    end
    ret, func = CashShopUtils.TryGetWeaponByWeaponIdInGoods(weaponId, goods)
    if ret then
        return ret, func
    end

    return false
end

function CashShopUtils.TryGetWeaponByWeaponIdInBox(weaponId, upBox)
    if upBox ~= nil and upBox.Count >= 1 then
        for i = 0, upBox.Count - 1 do
            local boxData = upBox[i]
            local goodsCfg = Tables.shopGoodsTable[boxData.goodsTemplateId]
            local poolId = goodsCfg.weaponGachaPoolId
            local _, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
            if poolInfo then
                local weaponPoolCfg = Tables.gachaWeaponPoolTable[poolId]
                local upWeaponId = weaponPoolCfg.upWeaponIds[0]
                if upWeaponId == weaponId then
                    local func = function()
                        PhaseManager:GoToPhase(PhaseId.CashShop, {
                            shopGroupId = CashShopConst.CashShopCategoryType.Weapon,
                            goodsId = boxData.goodsId,
                        })
                    end
                    return true, func
                end
            end
        end
    end

    return false
end

function CashShopUtils.TryGetWeaponByWeaponIdInGoods(weaponId, goodsList)
    if goodsList ~= nil then
        for _, goods in pairs(goodsList) do
            local goodsTableData = Tables.shopGoodsTable:GetValue(goods.goodsId)
            local displayItem = UIUtils.getRewardFirstItem(goodsTableData.rewardId)
            local itemId = displayItem.id
            if itemId == weaponId then
                local func = function()
                    PhaseManager:GoToPhase(PhaseId.CashShop, {
                        shopGroupId = CashShopConst.CashShopCategoryType.Weapon,
                        goodsId = goods.goodsId,
                    })
                end
                return true, func
            end
        end
    end

    return false
end



function CashShopUtils.GetGachaWeaponPoolCloseTimeShowDesc(poolId)
    local closeTimeDesc = ""
    local leftTimeDesc = ""

    local isRealTime, resultValue = CashShopUtils.GetGachaWeaponPoolCloseTimeInfo(poolId)
    if isRealTime then
        local closeTime = resultValue
        local leftTime = closeTime - DateTimeUtils.GetCurrentTimestampBySeconds()
        closeTimeDesc = Utils.appendUTC(Utils.timestampToDateMDHM(closeTime))
        leftTimeDesc = UIUtils.getShortLeftTime(leftTime)
    else
        local remainIndex = resultValue
        closeTimeDesc = string.format(Language.LUA_GACHA_WEAPON_POOL_REMAIN_INDEX_COUNT_DOWN, remainIndex)
        leftTimeDesc = closeTimeDesc
    end

    return isRealTime, closeTimeDesc, leftTimeDesc
end



function CashShopUtils.GetGachaWeaponPoolCloseTimeInfo(poolId)
    local isRealTime = false
    local resultValue = 0

    local csGachaSys = GameInstance.player.gacha


    local curIndex = 0
    local curIndexIsTop = false
    local _, box, _ = GameInstance.player.shopSystem:GetNowUpWeaponData()
    local count = box == nil and 0 or box.Count
    for i = 0, count - 1 do

        local goodsData = box[i]
        local goodsId = goodsData.goodsTemplateId
        local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsId)
        local _, weaponGachaCfg = Tables.gachaWeaponPoolTable:TryGetValue(goodsCfg.weaponGachaPoolId)
        if weaponGachaCfg.index > curIndex then
            curIndex = weaponGachaCfg.index
            local clientTopTimeId = weaponGachaCfg.clientTopTimeId
            curIndexIsTop = Utils.isCurTimeInTimeIdRange(clientTopTimeId)
        end
    end


    local _, gachaWeaponCfg = Tables.gachaWeaponPoolTable:TryGetValue(poolId)
    local index = gachaWeaponCfg.index
    local remainIndex = math.max(0, gachaWeaponCfg.finalIndex - curIndex + 1)


    if index > 0 and not curIndexIsTop then

        isRealTime = false
        resultValue = remainIndex - 1
    elseif index > 0 and remainIndex > 1 then

        isRealTime = false
        resultValue = remainIndex
    else


        local _, poolData = csGachaSys.poolInfos:TryGetValue(poolId)
        if poolData then
            isRealTime = true
            resultValue = poolData.closeTime
        end
    end

    return isRealTime, resultValue
end



function CashShopUtils.HasGachaWeaponTokenGiftGoodsCanBuy()
    for goodsId, cfg in pairs(Tables.cashShopHasWeaponGoldPackTable) do
        if CashShopUtils.CheckCashShopGoodsIsOpen(goodsId) and CashShopUtils.CheckCanBuyCashShopGoods(goodsId) then
            return true
        end
    end
    return false
end




function CashShopUtils.GetAllTokenGoods()
    local _, shopGroupData = Tables.shopGroupTable:TryGetValue(CashShopConst.CashShopCategoryType.Token)
    if not shopGroupData then
        return {}
    end
    local ret = {}
    for _, shopId in pairs(shopGroupData.shopIds) do
        local _, shopData = Tables.shopTable:TryGetValue(shopId)
        if shopData then
            local shop = GameInstance.player.shopSystem:GetShopData(shopData.shopId)
            local goodsList = shop:GetOpenGoodList()
            for _, goodsData in pairs(goodsList) do
                table.insert(ret, goodsData.goodsId)
            end
        end
    end
    return ret
end


function CashShopUtils.HaveCharPotentialExchange()
    local charList = GameInstance.player.charBag.charList
    for _, charInfo in pairs(charList) do
        local templateId = charInfo.templateId
        local currentPotentialLevel = charInfo.potentialLevel
        local succ, characterPotentialList = Tables.characterPotentialTable:TryGetValue(templateId)
        if succ and characterPotentialList then

            local maxPotentialLevel = characterPotentialList.potentialUnlockBundle.Count
            local materialId = characterPotentialList.firstItemId
            local getCount = Utils.getItemCount(materialId)

            local redundant = currentPotentialLevel + getCount - maxPotentialLevel

            if redundant > 0 then
                return true
            end
        end
    end

    return false
end


function CashShopUtils.TryOpenShopTokenExchangePopUpPanel()
    local redundantItemInfo = {}

    local charList = GameInstance.player.charBag.charList
    for _, charInfo in pairs(charList) do
        local charInstId = charInfo.instId
        local templateId = charInfo.templateId
        local currentPotentialLevel = charInfo.potentialLevel
        local succ, characterPotentialList = Tables.characterPotentialTable:TryGetValue(templateId)
        if succ and characterPotentialList then

            local maxPotentialLevel = characterPotentialList.potentialUnlockBundle.Count

            local unlockData = characterPotentialList.potentialUnlockBundle[0]
            local materialId = unlockData.itemIds[0]
            local getCount = Utils.getItemCount(materialId)

            local redundant = currentPotentialLevel + getCount - maxPotentialLevel

            if redundant > 0 then
                logger.info(string.format("%s已满潜,itemID:%s,多出来%s个",
                    templateId, materialId, redundant))
                local getItemDataSucc, itemData = Tables.itemTable:TryGetValue(materialId)
                if getItemDataSucc then
                    table.insert(redundantItemInfo, {
                        itemId = materialId,
                        count = redundant,
                        rarity = itemData.rarity,
                        itemData = itemData,
                        sortId1 = -itemData.sortId1,
                        sortId2 = -itemData.sortId2,
                    })
                else
                    logger.error("缺少数据 " .. materialId .. " 注意拉新。")
                end
            end
        end
    end

    table.sort(redundantItemInfo, Utils.genSortFunction({ "rarity", "sortId1", "sortId2"}, false))

    if #redundantItemInfo == 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SHOP_TOKEN_NO_REDUNDANT_CHAR_POTENTIAL_MATERIAL)
    else
        UIManager:Open(PanelId.ShopTokenExchangePopUp, {
            redundantItemInfo = redundantItemInfo,
        })
    end
end







function CashShopUtils.HaveSpaceshipCreditShop()
    local unlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.SpaceshipGuestRoom)
    if not unlocked then
        return false
    end

    local shopGroupId = "shop_spaceship_credit"
    local shopSystem = GameInstance.player.shopSystem
    local groupData = shopSystem:GetShopGroupData(shopGroupId)
    for i = groupData.shopIdList.Count - 1, 0, -1 do
        local shopTableData = Tables.shopTable:GetValue(groupData.shopIdList[i])
        local shopUnlock = shopSystem:CheckShopUnlocked(shopTableData.shopId)
        if shopUnlock then
            return true
        end
    end

    return false
end

function CashShopUtils.CheckSpaceshipCreditShopDailyGetCreditRedDot()

    if not CashShopUtils.HaveSpaceshipCreditShop() then
        return false
    end

    local succ, value = GameInstance.player.globalVar:TryGetServerVar(GEnums.ServerGameVarEnum.SpaceShipDailyCreditReward)
    return value == 0
end


function CashShopUtils.CheckSpaceshipCreditShopGetCreditRedDot()

    if not CashShopUtils.HaveSpaceshipCreditShop() then
        return false
    end

    local succ, value = GameInstance.player.globalVar:TryGetServerVar(GEnums.ServerGameVarEnum.SpaceShipDailyCreditReward)
    if value == 0 then
        return true
    end


    local all = 0

    local visitRecord = GameInstance.player.spaceship:GetRoomVisitRecord()
    local recvedCreditCnt = visitRecord.today.recvedCreditCnt
    local totalCreditCnt = visitRecord.today.totalCreditCnt
    local today = totalCreditCnt - recvedCreditCnt

    local recvedCreditCntYesterday = visitRecord.yesterday.recvedCreditCnt
    local totalCreditCntYesterday = visitRecord.yesterday.totalCreditCnt
    local yesterday = totalCreditCntYesterday - recvedCreditCntYesterday

    all = today + yesterday

    local numberLimit = nil
    local _, cfg = Tables.MoneyConfigTable:TryGetValue(Tables.CashShopConst.CreditTabMoneyId)
    if cfg ~= nil then
        numberLimit = cfg.MoneyClearLimit
    end

    local curr = GameInstance.player.inventory:GetItemCount(
        Utils.getCurrentScope(), Utils.getCurrentChapterId(), Tables.CashShopConst.CreditTabMoneyId)

    if numberLimit ~= nil then

        if curr <= numberLimit and curr + all > numberLimit then
            return true
        end
    end

    return false
end



function CashShopUtils.GetDisplayPrice(originPrice, discount)

    local roundedDiscount = math.floor(discount * 100 + 0.5)

    local val = (originPrice * roundedDiscount) / 100

    return math.floor(val)
end






function CashShopUtils.InitCategoryTypeList()
    local ret = nil




    if CS.Beyond.GlobalOptions.instance.auditing then
        if CashShopUtils.IsPS() then

            if not GameInstance.player.mission:IsMissionCompleted("e0m2") then
                ret = {
                    CashShopConst.CashShopCategoryType.Recharge,
                    CashShopConst.CashShopCategoryType.Weapon,
                    CashShopConst.CashShopCategoryType.Token,
                    CashShopConst.CashShopCategoryType.Credit,
                }
            else
                ret = {
                    CashShopConst.CashShopCategoryType.Recharge,
                    CashShopConst.CashShopCategoryType.Pack,
                    CashShopConst.CashShopCategoryType.Weapon,
                    CashShopConst.CashShopCategoryType.Token,
                    CashShopConst.CashShopCategoryType.Credit,
                }
            end
        else
            ret = {
                CashShopConst.CashShopCategoryType.Recharge,
            }
        end
    end

    if ret == nil then
        ret = {
            CashShopConst.CashShopCategoryType.Recommend,
            CashShopConst.CashShopCategoryType.Recharge,
            CashShopConst.CashShopCategoryType.Pack,
            CashShopConst.CashShopCategoryType.Weapon,
            CashShopConst.CashShopCategoryType.Token,
            CashShopConst.CashShopCategoryType.Credit,
        }
    end


    if CashShopUtils.HaveSpaceshipCreditShop() == false then
        local index = lume.find(ret, CashShopConst.CashShopCategoryType.Credit)
        if index then
            table.remove(ret, index)
        end
    end

    return ret
end

function CashShopUtils.NoCashShopGoods()
    local count = GameInstance.player.cashShopSystem.sdkProductInfoDic.Count
    return count == 0
end





function CashShopUtils.CheckCanOpenPhase(arg)

    local shopGroupId = arg and arg.shopGroupId or nil
    if shopGroupId == nil then
        return true
    end

    local tabList = CashShopUtils.InitCategoryTypeList()
    local index = lume.find(tabList, shopGroupId)
    if index then
        return true
    else
        return false, Language.LUA_CASH_SHOP_BLOCK_TIPS
    end
end

function CashShopUtils.GotoCashShopRechargeTab()
    PhaseManager:GoToPhase(PhaseId.CashShop, { shopGroupId = CashShopConst.CashShopCategoryType.Recharge })
end


function CashShopUtils.OpenShopDetailPanel(info, uiCtrl)
    local isOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isOpen then
        if shopDetailCtrl.m_phase then
            shopDetailCtrl.m_phase:RemovePhasePanelItemById(PanelId.ShopDetail)
        else
            UIManager:Close(PanelId.ShopDetail)
        end
    end

    if uiCtrl and uiCtrl.m_phase then
        uiCtrl.m_phase:CreatePhasePanelItem(PanelId.ShopDetail, info)
    else
        UIManager:Open(PanelId.ShopDetail, info)
    end
end

function CashShopUtils.ShowKrUrl()
    UIManager:Open(PanelId.InstructionBook, "shop_recharge_kr")
end






function CashShopUtils.IsPS()
    if UNITY_PS4 or UNITY_PS5 then
        return true
    end
    return false
end

function CashShopUtils.ShowPsStore()
    GameInstance.player.cashShopSystem:ShowPsStoreIcon()
end

function CashShopUtils.HidePsStore()
    GameInstance.player.cashShopSystem:HidePsStoreIcon()
end


function CashShopUtils.IsCashShopCategory(categoryId)
    if categoryId == CashShopConst.CashShopCategoryType.Recommend or
        categoryId == CashShopConst.CashShopCategoryType.Recharge or
        categoryId == CashShopConst.CashShopCategoryType.Pack then
        return true
    end
end


function CashShopUtils.IsShopCategory(categoryId)
    local isCashShopCategory = CashShopUtils.IsCashShopCategory(categoryId)
    return not isCashShopCategory
end






function CashShopUtils.LogGiftpackShop(targetCashShopId)
    local logContent = "currTs:" .. tostring(DateTimeUtils.GetCurrentTimestampBySeconds()) .. "\n"
    for cashShopId, showData in pairs(Tables.GiftpackCashShopClientShowDataTable) do
        if cashShopId == targetCashShopId then

            local shopData = GameInstance.player.cashShopSystem:GetShopData(cashShopId)
            if shopData == nil then
                logContent = logContent .. string.format(
                    "shopId:%s, 未找到shopData.\n", cashShopId)
            else
                logContent = logContent .. string.format(
                    "shopId:%s, isOpen:%s.\n", cashShopId, shopData:IsOpen())
                local goodsList = GameInstance.player.cashShopSystem:GetGoodsList(shopData)
                logContent = logContent .. string.format(
                    "GetGoodsList().Count:%s.\n", goodsList.Count)
                if true then

                    local list = shopData.goodsList
                    if list.Count > 0 then
                        for _, goodsData in pairs(list) do
                            local isOpen = goodsData:IsOpen()
                            logContent = logContent .. string.format(
                                "\n\n%s的isOpen:%s\n",
                                goodsData.goodsId, isOpen)


                            if not isOpen then
                                local openTs = goodsData.openTimeStamp
                                local closeTs = goodsData.closeTimeStamp
                                local currTs = DateTimeUtils.GetCurrentTimestampBySeconds()
                                logContent = logContent .. string.format(
                                    "openTs:%s. (%s)\n", openTs, DateTimeUtils.TimeStamp2ServerTime(openTs):ToString("yyyy-MM-dd HH:mm:ss"))
                                logContent = logContent .. string.format(
                                    "closeTs:%s. (%s)\n", closeTs, DateTimeUtils.TimeStamp2ServerTime(closeTs):ToString("yyyy-MM-dd HH:mm:ss"))
                                if openTs ~= 0 and currTs < openTs then
                                    logContent = logContent .. string.format(
                                        "%s不显示因为%s < %s.\n",
                                        goodsData.goodsId, currTs, openTs)
                                elseif closeTs ~= 0 and currTs > closeTs then
                                    logContent = logContent .. string.format(
                                        "%s不显示因为%s > %s.\n",
                                        goodsData.goodsId, currTs, closeTs)
                                else
                                    logContent = logContent .. string.format(
                                        "%s不显示可能因为condition.\n",
                                        goodsData.goodsId)
                                    local succ, cfg = Tables.GiftpackCashShopGoodsDataTable:TryGetValue(goodsData.goodsId)
                                    if succ then
                                        local operator = cfg.conditionOpType
                                        logContent = logContent .. string.format(
                                            "condition operator:%s.\n",
                                            operator)
                                    end
                                    local conditionDicValues = goodsData.conditionDic
                                    logContent = logContent .. string.format(
                                        "goodsData.conditionDic.Count:%s.\n",
                                        conditionDicValues.Count)
                                    for _, value in pairs(conditionDicValues) do
                                        local id = value.conditionId
                                        local flag = value.flag
                                        logContent = logContent .. string.format(
                                            "conditionID:%s, Flag:%s.\n",
                                            id, flag)
                                    end
                                end
                            end

                        end
                    end
                end
            end
        end
    end
    UIManager:Open(PanelId.CommonBook, {
        title = "debug",
        content = logContent,
        onConfirm = function()

        end,
    })
end


function CashShopUtils.RecommendPanelAddRuntimeTabDataAndRefresh(id)
    local _, ctrl = UIManager:IsOpen(PanelId.ShopRecommend)
    if ctrl == nil then
        logger.error("推荐页还未打开！")
        return
    end

    ctrl:ForceAddRuntimeTabDataToBottom(id)
end




_G.CashShopUtils = CashShopUtils
return CashShopUtils
