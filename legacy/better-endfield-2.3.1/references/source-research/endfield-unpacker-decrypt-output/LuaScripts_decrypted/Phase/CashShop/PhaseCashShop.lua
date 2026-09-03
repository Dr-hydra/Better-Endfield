
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.CashShop
PhaseCashShop = HL.Class('PhaseCashShop', phaseBase.PhaseBase)

local TabPanelIds = {
    [CashShopConst.CashShopCategoryType.Recommend] = PanelId.ShopRecommend,
    [CashShopConst.CashShopCategoryType.Recharge] = PanelId.ShopRecharge,
    [CashShopConst.CashShopCategoryType.Pack] = PanelId.ShopGiftPackEmpty,
    [CashShopConst.CashShopCategoryType.Weapon] = PanelId.ShopWeapon,
    [CashShopConst.CashShopCategoryType.Token] = PanelId.ShopToken,
    [CashShopConst.CashShopCategoryType.Credit] = PanelId.SpaceshipCreditShop,
}
local MainPanelId = PanelId.CashShop
local SDK_PRODUCT_INFO_TIMEOUT = 5
local UPDATE_SDK_PRODUCT_INFO_INTERVAL = 10

PhaseCashShop.cashShopCtrl = HL.Field(HL.Any)

PhaseCashShop.currCategoryId = HL.Field(HL.String) << ""



PhaseCashShop.m_backToRecommendPanelTabId = HL.Field(HL.String) << ""

PhaseCashShop.m_haveShowPsStoreLogo = HL.Field(HL.Boolean) << false

PhaseCashShop.m_storeShowPsStoreLogo = HL.Field(HL.Boolean) << false


PhaseCashShop.m_needGameEvent = HL.Field(HL.Boolean) << false

PhaseCashShop.m_enterButton = HL.Field(HL.String) << ""

PhaseCashShop.m_enterPanel = HL.Field(HL.String) << ""

PhaseCashShop.m_modifyPanelItemFrameCount = HL.Field(HL.Number) << 0





PhaseCashShop.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.OPEN_LEVEL_PHASE] = { 'OnOpenLevelPhase', false },
    [MessageConst.ON_CASH_SHOP_ORDER_SETTLE] = { 'OnOrderSettle', false },
    [MessageConst.ON_SC_PAY_FREQUENCY_LIMIT_MODIFY] = { 'OnFreeCashShopBuySucc', false },
    [MessageConst.ON_SDK_MASK_HIDE] = { 'TryPopOrderSettle', false },
    [MessageConst.ON_ACCEPT_ORDERS] = { '_OnAcceptOrders', false },
    [MessageConst.ON_PAY_ERROR] = { '_OnPayError', false },
    [MessageConst.ON_START_WEB_APPLICATION] = { '_OnStartPayment', true },
    [MessageConst.ON_CLOSE_WEB_APPLICATION] = { '_OnClosePayment', true },
    [MessageConst.CASH_SHOP_CHOOSE_GIFTPACK_TAB_BY_GOODSID] = { '_ChooseGiftpackByGoodsId', true },
}

PhaseCashShop.m_cashShopSystem = HL.Field(HL.Userdata)









PhaseCashShop._OnInit = HL.Override() << function(self)
    self.m_needGameEvent = true
    self.m_enterPanel = lume.trim((lume.split(tostring(PhaseManager.curPhase), "#"))[1])

    PhaseCashShop.Super._OnInit(self)
    self.m_cashShopSystem = GameInstance.player.cashShopSystem
end

PhaseCashShop._InitAllPhaseItems = HL.Override() << function(self)
    local arg = self.arg or {}
    if arg.enter_button then
        self.m_enterButton = arg.enter_button
    end
    arg.phase = self
    self:CreatePhasePanelItem(PanelId.CashShop, arg)
    arg.cashShopId = nil
end


PhaseCashShop.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {}
    arg.shopGroupId = self.currCategoryId
    local panelId = TabPanelIds[self.currCategoryId]
    local ctrl = self.m_panel2Item[panelId].uiCtrl

    ctrl:SetCashShopStateArg(arg)

    if not string.isEmpty(self.m_backToRecommendPanelTabId) then
        arg.backToRecommendPanelTabId = self.m_backToRecommendPanelTabId
    end
    return arg
end


PhaseCashShop.OpenCategory = HL.Method(HL.String, HL.Opt(HL.String))
    << function(self, categoryId, cashShopId)
    self.currCategoryId = categoryId
    local panelId = TabPanelIds[categoryId]
    if panelId then
        for panelId, panelItem in pairs(self.m_panel2Item) do
            if panelId ~= MainPanelId then
                self:RemovePhasePanelItemByIdWrapper(panelId)
            end
        end
        local arg = self.arg or {}
        arg.phase = self
        arg.cashShopId = cashShopId
        self:CreateOrShowPhasePanelItemWrapper(panelId, arg)
        UIManager:SetTopOrder(MainPanelId)





        Notify(MessageConst.ON_CASH_SHOP_OPEN_CATEGORY)






        local newPanelItem = self.m_panel2Item[panelId]
        if newPanelItem and newPanelItem.uiCtrl then
            newPanelItem.uiCtrl:OnAfterCategoryTopOrdered()
        end
    end
end



PhaseCashShop.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if not fastMode and transitionType == PhaseConst.EPhaseState.TransitionIn then
        if not UNITY_EDITOR then
            self.m_cashShopSystem:RequestPlatformData()


            local prodInfo = CS.Beyond.SDK.SDKPayUtils.s_globalProdInfoProc
            if prodInfo then
                if not prodInfo:IsSuc() then

                    local networkMask = CS.Beyond.Network.NetworkMask.instance
                    local maskKey = networkMask:AddMask("SDK_PRODUCT_INFO", SDK_PRODUCT_INFO_TIMEOUT, 1)
                    local startTime = Time.realtimeSinceStartup
                    prodInfo:PrepareProdInfo(true)
                    while prodInfo.keepWaiting do
                        coroutine.step()
                        if Time.realtimeSinceStartup - startTime > SDK_PRODUCT_INFO_TIMEOUT then
                            logger.error("SDK GetProductInfoProcess Timeout")
                            break
                        end
                    end
                    networkMask:RemoveMask(maskKey)
                else
                    if Time.realtimeSinceStartup - prodInfo.lastUpdateTime > UPDATE_SDK_PRODUCT_INFO_INTERVAL then
                        prodInfo:PrepareProdInfo(false)
                    end
                end
            end
        end
    end
end

PhaseCashShop._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseCashShop._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseCashShop._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_storeShowPsStoreLogo = self.m_haveShowPsStoreLogo
    self:HidePsStore()
end

PhaseCashShop._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_storeShowPsStoreLogo then
        self.m_storeShowPsStoreLogo = false
        self:ShowPsStore()
    end
end






PhaseCashShop._OnActivated = HL.Override() << function(self)
end

PhaseCashShop._OnDeActivated = HL.Override() << function(self)
end

PhaseCashShop._OnDestroy = HL.Override() << function(self)
    self:HidePsStore()
    PhaseCashShop.Super._OnDestroy(self)
end








PhaseCashShop.CreateOrShowPhasePanelItemWrapper = HL.Method(HL.Number, HL.Opt(HL.Any)).Return(HL.Forward("PhasePanelItem")) << function(self, panelId, arg)
    if PhaseCashShop.CheckIsPop() then
        logger.error("[cashshop] 在phase pop时尝试CreateOrShowPhasePanelItem，已直接return")
        return nil
    end


    self.m_modifyPanelItemFrameCount = Time.frameCount
    return self:CreateOrShowPhasePanelItem(panelId, arg)
end






PhaseCashShop.RemovePhasePanelItemByIdWrapper = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, panelId, arg)
    if PhaseCashShop.CheckIsPop() then
        local ctrlName = ""
        if self.m_panel2Item[panelId] and self.m_panel2Item[panelId].uiCtrl then
            ctrlName = tostring(self.m_panel2Item[panelId].uiCtrl)
        end
        logger.error("[cashshop] 在phase pop时尝试RemovePhasePanelItemById，已直接return, 想要Remove的UICtrl是:" .. ctrlName)
        return
    end


    self.m_modifyPanelItemFrameCount = Time.frameCount
    self:RemovePhasePanelItemById(panelId, arg)
end

PhaseCashShop.CheckIsPop = HL.StaticMethod().Return(HL.Boolean) << function()
    if PhaseManager.m_curState == Const.PhaseState.Pop and
        PhaseManager.curPhase.phaseId ~= PHASE_ID then
        return true
    else
        return false
    end
end



PhaseCashShop.s_orderSettleQueue = HL.StaticField(HL.Forward("Queue"))

PhaseCashShop.s_freeCashShopRewardQueue = HL.StaticField(HL.Forward("Queue"))

PhaseCashShop.s_webOrderList = HL.StaticField(HL.Table)

PhaseCashShop.OnOpenLevelPhase = HL.StaticMethod() << function()
    if PhaseCashShop.s_webOrderList and next(PhaseCashShop.s_webOrderList) then
        PhaseCashShop._AddMainHudActionQuest()
    end
end

PhaseCashShop.OnOrderSettle = HL.StaticMethod(HL.Table) << function(arg)

    if PhaseManager:IsOpen(PHASE_ID) then
        GameInstance.player.cashShopSystem:RequestPlatformData()
    end


    local orderSettle = unpack(arg)
    if orderSettle.IsWeb then
        if PhaseCashShop.s_webOrderList == nil then
            PhaseCashShop.s_webOrderList = {}
        end
        table.insert(PhaseCashShop.s_webOrderList, orderSettle)
        PhaseCashShop._AddMainHudActionQuest()

    else
        if UIManager:IsShow(PanelId.SDKApplicationMask) or UIManager:IsShow(PanelId.RewardsPopUpForSystem) then
            if PhaseCashShop.s_orderSettleQueue == nil then
                PhaseCashShop.s_orderSettleQueue = require_ex("Common/Utils/DataStructure/Queue")()
            end
            PhaseCashShop.s_orderSettleQueue:Push(orderSettle)
        else
            CashShopUtils.showOrderSettle(orderSettle, PhaseCashShop.TryPopOrderSettle)
        end
    end
end

PhaseCashShop.OnFreeCashShopBuySucc = HL.StaticMethod() << function()
    local freeBuyInfo = CashShopUtils.popFreeCashShopBuy()
    if not freeBuyInfo then
        return
    end
    local rewardItems = CashShopUtils.getFreeCashShopRewardItems(freeBuyInfo.goodsId, freeBuyInfo.count)
    if #rewardItems == 0 then
        return
    end
    local rewardArg = {
        items = rewardItems,
        onComplete = function()
            PhaseCashShop.TryPopOrderSettle()
        end,
    }
    if UIManager:IsShow(PanelId.SDKApplicationMask) or UIManager:IsShow(PanelId.RewardsPopUpForSystem) then
        if PhaseCashShop.s_freeCashShopRewardQueue == nil then
            PhaseCashShop.s_freeCashShopRewardQueue = require_ex("Common/Utils/DataStructure/Queue")()
        end
        PhaseCashShop.s_freeCashShopRewardQueue:Push(rewardArg)
        return
    end
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        items = rewardArg.items,
        onComplete = rewardArg.onComplete,
    })
end

PhaseCashShop._AddMainHudActionQuest = HL.StaticMethod() << function()
    if LuaSystemManager.mainHudActionQueue:HasRequest("CashShopOrderSettle") then
        return
    end
    LuaSystemManager.mainHudActionQueue:AddRequest("CashShopOrderSettle", function()
        local function showWebOrderSettles()
            if PhaseCashShop.s_webOrderList == nil or #PhaseCashShop.s_webOrderList == 0 then
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "CashShopOrderSettle")
                return
            end
            local interrupt = {
                interruptMessage = { MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE },
            }
            CashShopUtils.showWebOrderSettles(PhaseCashShop.s_webOrderList, function()
                showWebOrderSettles()
            end, interrupt)
        end


        if not GameInstance.player.mission:IsMissionCompleted("e0m0") then
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "CashShopOrderSettle")
            return
        end

        showWebOrderSettles()
    end)
end

PhaseCashShop._OnAcceptOrders = HL.StaticMethod(HL.Table) << function(orderIds)
    if PhaseCashShop.s_webOrderList == nil then
        return
    end
    for _, orderId in pairs(orderIds) do
        for i, orderSettle in ipairs(PhaseCashShop.s_webOrderList) do
            if orderSettle.OrderId == orderId then
                table.remove(PhaseCashShop.s_webOrderList, i)
                break
            end
        end
    end
end

PhaseCashShop._OnStartPayment = HL.Method(HL.Table) << function(self, arg)
    local key = unpack(arg)
    if key ~= CS.Beyond.SDK.PaymentEasyAccess.MASK_KEY_PAYMENT then
        return
    end

    self.m_storeShowPsStoreLogo = self.m_haveShowPsStoreLogo
    self:HidePsStore()
end

PhaseCashShop._OnClosePayment = HL.Method(HL.Table) << function(self, arg)
    local key = unpack(arg)
    if key ~= CS.Beyond.SDK.PaymentEasyAccess.MASK_KEY_PAYMENT then
        return
    end
    if self.m_storeShowPsStoreLogo then
        self.m_storeShowPsStoreLogo = false
        self:ShowPsStore()
    end
end

PhaseCashShop.TryPopOrderSettle = HL.StaticMethod() << function()
    if PhaseCashShop.s_orderSettleQueue ~= nil and
        PhaseCashShop.s_orderSettleQueue:Count() > 0 then
        local orderSettle = PhaseCashShop.s_orderSettleQueue:Pop()
        CashShopUtils.showOrderSettle(orderSettle, function()
            PhaseCashShop.TryPopOrderSettle()
        end)
    elseif PhaseCashShop.s_freeCashShopRewardQueue ~= nil and
        PhaseCashShop.s_freeCashShopRewardQueue:Count() > 0 then
        local rewardArg = PhaseCashShop.s_freeCashShopRewardQueue:Pop()
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardArg)
    end
end

PhaseCashShop._OnPayError = HL.StaticMethod(HL.Table) << function(arg)

    GameInstance.player.cashShopSystem:RequestPlatformData()
    local errorMsg = unpack(arg)
    Notify(MessageConst.SHOW_POP_UP, {
        content = errorMsg,
        hideCancel = true,
    })
end



PhaseCashShop.OnClickCloseButton = HL.Method() << function(self)
    if string.isEmpty(self.m_backToRecommendPanelTabId) then
        if self.m_modifyPanelItemFrameCount == Time.frameCount then
            logger.error("[cashshop] 在Create或Remove PhasePanelItem的同帧尝试退出phaseCashShop，已直接return")
            return
        end

        local ret = PhaseManager:PopPhase(PhaseId.CashShop)

        if not ret then
            local logStr = ""
            for _, item in pairs(self.m_panel2Item) do
                logStr = logStr .. tostring(item.uiCtrl) .. ","
            end
            logger.important(CS.Beyond.EnableLogType.DevOnly,
                "[cashshop] PhaseCashShop 没有正常退出, 当前打开的uiCtrl有 " .. logStr)
        end

        return
    end
    self:Refresh({
        shopGroupId = CashShopConst.CashShopCategoryType.Recommend
    })

    local panelItem = self.m_panel2Item[PanelId.ShopRecommend]
    if panelItem then

        panelItem.uiCtrl:SetCurrTabId(self.m_backToRecommendPanelTabId)
        self.m_backToRecommendPanelTabId = ""
        Notify(MessageConst.CASH_SHOP_REFRESH_CLOSE_BTN_UI)
    end
end



PhaseCashShop.OpenGiftpackCategoryAndOpenDetailPanel = HL.Method(HL.String, HL.String, HL.Opt(HL.Boolean))
    << function(self, shopGoodsId, recommendId, openDetailPanel)
    if openDetailPanel == nil then
        openDetailPanel = true
    end


    if self.state ~= PhaseConst.EPhaseState.Activated then
        return
    end

    self.m_backToRecommendPanelTabId = self.m_panel2Item[PanelId.ShopRecommend].uiCtrl:GetCurrTabId()
    Notify(MessageConst.CASH_SHOP_REFRESH_CLOSE_BTN_UI)
    self:Refresh({
        shopGroupId = CashShopConst.CashShopCategoryType.Pack,
    })

    local panelItem = self.m_panel2Item[PanelId.ShopGiftPackEmpty]
    if panelItem then

        local shopId = panelItem.uiCtrl:ChooseTabByGoodsId(shopGoodsId, openDetailPanel)

        EventLogManagerInst:GameEvent_RecommendRedirect(
            shopId,
            CashShopConst.CashShopCategoryType.Pack,
            recommendId
        )
    end
end

PhaseCashShop.OpenWeaponCategoryAndOpenDetailPanel = HL.Method(HL.Any, HL.String)
    << function(self, shopGoodsData, recommendId)

    if self.state ~= PhaseConst.EPhaseState.Activated then
        return
    end

    self.m_backToRecommendPanelTabId = self.m_panel2Item[PanelId.ShopRecommend].uiCtrl:GetCurrTabId()
    Notify(MessageConst.CASH_SHOP_REFRESH_CLOSE_BTN_UI)
    self:Refresh({
        shopGroupId = CashShopConst.CashShopCategoryType.Weapon
    })

    local panelItem = self.m_panel2Item[PanelId.ShopWeapon]
    if panelItem then
        local shopCtrl = panelItem.uiCtrl
        shopCtrl:ChooseLimitedWeaponPool(shopGoodsData)

        EventLogManagerInst:GameEvent_RecommendRedirect(
            shopGoodsData.shopId,
            CashShopConst.CashShopCategoryType.Weapon,
            recommendId
        )
    end
end


PhaseCashShop.OpenGiftpackCategoryByCashShopId = HL.Method(HL.String, HL.Opt(HL.String))
    << function(self, cashShopId, recommendId)

    if self.state ~= PhaseConst.EPhaseState.Activated then
        return
    end


    self.m_backToRecommendPanelTabId = self.m_panel2Item[PanelId.ShopRecommend].uiCtrl:GetCurrTabId()
    Notify(MessageConst.CASH_SHOP_REFRESH_CLOSE_BTN_UI)
    self:Refresh({
        shopGroupId = CashShopConst.CashShopCategoryType.Pack
    })

    local panelItem = self.m_panel2Item[PanelId.ShopGiftPackEmpty]
    if panelItem then

        panelItem.uiCtrl:ChooseTabByCashShopId(cashShopId)

        EventLogManagerInst:GameEvent_RecommendRedirect(
            cashShopId,
            CashShopConst.CashShopCategoryType.Pack,
            recommendId or ""
        )
    end
end

PhaseCashShop.ClearBackToRecommendPanel = HL.Method() << function(self)
    self.m_backToRecommendPanelTabId = ""
    Notify(MessageConst.CASH_SHOP_REFRESH_CLOSE_BTN_UI)
end

PhaseCashShop.ShowPsStore = HL.Method() << function(self)
    if self.m_haveShowPsStoreLogo then
        return
    end
    self.m_haveShowPsStoreLogo = true
    CashShopUtils.ShowPsStore()
end

PhaseCashShop.HidePsStore = HL.Method() << function(self)
    if not self.m_haveShowPsStoreLogo then
        return
    end
    self.m_haveShowPsStoreLogo = false
    CashShopUtils.HidePsStore()
end

PhaseCashShop._ChooseGiftpackByGoodsId = HL.Method(HL.Any) << function(self, arg)
    self:Refresh({
        shopGroupId = CashShopConst.CashShopCategoryType.Pack
    })

    local goodsId = arg.cashGoodsId
    local item = self.m_panel2Item[PanelId.ShopGiftPackEmpty]

    if item then
        item.uiCtrl:ChooseTabByGoodsId(goodsId, false)
    end
end

HL.Commit(PhaseCashShop)
