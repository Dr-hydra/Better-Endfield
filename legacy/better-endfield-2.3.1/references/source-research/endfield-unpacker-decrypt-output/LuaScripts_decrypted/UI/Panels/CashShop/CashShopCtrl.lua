
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CashShop

local recommend_group_id = "shop_pay_recommend"

CashShopCtrl = HL.Class('CashShopCtrl', uiCtrl.UICtrl)

local AllTabCategory = {
    CashShopConst.CashShopCategoryType.Recharge,
    CashShopConst.CashShopCategoryType.Pack,
    CashShopConst.CashShopCategoryType.Weapon,
    CashShopConst.CashShopCategoryType.Token,
    CashShopConst.CashShopCategoryType.Credit,
}





CashShopCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CASH_SHOP_SHOW_WALLET_BAR] = 'ShowWalletBar',
    [MessageConst.CASH_SHOP_REFRESH_CLOSE_BTN_UI] = '_RefreshUICloseBtn',
}

CashShopCtrl.m_tabCategoryData = HL.Field(HL.Table)

CashShopCtrl.m_showRecommendTab = HL.Field(HL.Boolean) << true

CashShopCtrl.m_tabCellCache = HL.Field(HL.Forward("UIListCache"))

CashShopCtrl.m_categoryIdToTabCell = HL.Field(HL.Table)

CashShopCtrl.m_showWalletBarArg = HL.Field(HL.Table)


CashShopCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitAction()
    self:_InitData()
    self:_SetupUI()
    self.m_phase.cashShopCtrl = self
    self:_ProcessArg(arg)
end

CashShopCtrl.OnShow = HL.Override() << function(self)
    local wrapper = self.animationWrapper
    if wrapper then
        wrapper:PlayInAnimation(function()
            self:_ShowWalletBarCore()
        end)
    end
end

CashShopCtrl.OnClose = HL.Override() << function(self)
    CashShopUtils.TryCloseSpecialGiftPopup()
end

CashShopCtrl.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, arg)



    self:_CloseAllDialogPanel()

    local _, walletBarCtrl = UIManager:IsOpen(PanelId.WalletBar)
    if walletBarCtrl then
        walletBarCtrl:StopFocus()
    end

    self:_ProcessArg(arg)
end

CashShopCtrl.OnSortingOrderChange = HL.Override(HL.Number, HL.Boolean) << function(self, order, isInit)
    CashShopCtrl.Super.OnSortingOrderChange(self, order, isInit)
    self.view.walletBarPlaceholder.gameObject:SetActive(false)
    self.view.walletBarPlaceholder.gameObject:SetActive(true)
end

CashShopCtrl._InitAction = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        self.m_phase:OnClickCloseButton()
    end)
    self.view.backBtn.onClick:AddListener(function()
        self.m_phase:OnClickCloseButton()
    end)

    self.m_categoryIdToTabCell = {}
    self.view.tabSpCell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_ClickCategory(CashShopConst.CashShopCategoryType.Recommend)
        end
    end)
    self.m_categoryIdToTabCell[CashShopConst.CashShopCategoryType.Recommend] = self.view.tabSpCell

    self.m_tabCellCache = UIUtils.genCellCache(self.view.tabCell)
end

CashShopCtrl._InitData = HL.Method() << function(self)
    local shopCategoryTypeList = CashShopUtils.InitCategoryTypeList()



    if CashShopUtils.IsPS() then
        if CashShopUtils.NoCashShopGoods() then


            local index1 = lume.find(shopCategoryTypeList, CashShopConst.CashShopCategoryType.Recommend)
            if index1 then
                table.remove(shopCategoryTypeList, index1)
            end
            local index2 = lume.find(shopCategoryTypeList, CashShopConst.CashShopCategoryType.Recharge)
            if index2 then
                table.remove(shopCategoryTypeList, index2)
            end
            local index3 = lume.find(shopCategoryTypeList, CashShopConst.CashShopCategoryType.Pack)
            if index3 then
                table.remove(shopCategoryTypeList, index3)
            end
        end
    end


    local index = lume.find(shopCategoryTypeList, CashShopConst.CashShopCategoryType.Recommend)
    if index then
        table.remove(shopCategoryTypeList, index)
        self.m_showRecommendTab = true
    else
        self.m_showRecommendTab = false
    end

    self.m_tabCategoryData = shopCategoryTypeList
end

CashShopCtrl._SetupUI = HL.Method() << function(self)
    self.view.tabSpCell.gameObject:SetActive(self.m_showRecommendTab)
    local _, recommendShopGroupData = Tables.shopGroupTable:TryGetValue(recommend_group_id)
    if recommendShopGroupData then
        self.view.tabSpCell.slcIcon:LoadSprite(UIConst.UI_SPRITE_CASH_SHOP_CATEGORY, recommendShopGroupData.icon)
        self.view.tabSpCell.tabSlcText.text = recommendShopGroupData.shopGroupName
    end


    self.m_tabCellCache:Refresh(#self.m_tabCategoryData, function(cell, index)
        local categoryId = self.m_tabCategoryData[index]
        cell.gameObject.name = categoryId
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_ClickCategory(categoryId)
            end
        end)
        local _, shopGroupData = Tables.shopGroupTable:TryGetValue(categoryId)
        if shopGroupData then
            cell.normalIcon:LoadSprite(UIConst.UI_SPRITE_CASH_SHOP_CATEGORY, shopGroupData.icon)
            cell.tabNormalText.text = shopGroupData.shopGroupName
        end
        self.m_categoryIdToTabCell[categoryId] = cell

        if categoryId == CashShopConst.CashShopCategoryType.Pack then
            local shopGoodsIds = CashShopUtils.GetAllGiftPackGoodsIds()
            cell.redDot:InitRedDot("CashShopNewCashGoods", shopGoodsIds)
        elseif categoryId == CashShopConst.CashShopCategoryType.Token then
            local allTokenGoodsIds = CashShopUtils.GetAllTokenGoods()
            cell.redDot:InitRedDot("CashShopTokenNormal", allTokenGoodsIds)
        elseif categoryId == CashShopConst.CashShopCategoryType.Credit then
            cell.redDot:InitRedDot("CashShopCreditShopGetCredit")
        end
    end)
end

CashShopCtrl._RefreshUICloseBtn = HL.Method() << function(self)
    local canBackToRecommend = not string.isEmpty(self.m_phase.m_backToRecommendPanelTabId)
    self.view.btnClose.gameObject:SetActive(not canBackToRecommend)
    self.view.backBtn.gameObject:SetActive(canBackToRecommend)
end

CashShopCtrl._ProcessArg = HL.Method(HL.Any) << function(self, arg)







    local hopeCategoryId = CashShopConst.CashShopCategoryType.Recommend
    if arg and not string.isEmpty(arg.shopGroupId) then
        hopeCategoryId = arg.shopGroupId
    end


    local existCategoryId = false
    if hopeCategoryId == CashShopConst.CashShopCategoryType.Recommend then
        existCategoryId = self.m_showRecommendTab
    else
        local found = lume.find(self.m_tabCategoryData, hopeCategoryId)
        existCategoryId = found ~= nil
    end


    local categoryId = nil
    if not existCategoryId then

        if self.m_showRecommendTab then
            categoryId = CashShopConst.CashShopCategoryType.Recommend
        else
            categoryId = self.m_tabCategoryData[1]
        end
    else
        categoryId = hopeCategoryId
    end


    if CashShopUtils.IsPS() then
        if CashShopUtils.IsCashShopCategory(hopeCategoryId) and
            CashShopUtils.IsShopCategory(categoryId) and
            CashShopUtils.NoCashShopGoods() then

            logger.info("[CashShop] 显示ps empty store")
            GameInstance.player.cashShopSystem:ShowPsEmptyStore()
        end
    end

    local tabCell = self.m_categoryIdToTabCell[categoryId]
    if tabCell then
        tabCell.toggle:SetIsOnWithoutNotify(true)
        arg.cashShopCtrl = self
        if arg and arg.cashShopId ~= nil and not string.isEmpty(arg.cashShopId) then
            self.m_phase:OpenCategory(categoryId, arg.cashShopId)
        else
            self.m_phase:OpenCategory(categoryId)
        end
    end

    if arg.backToRecommendPanelTabId then
        self.m_phase.m_backToRecommendPanelTabId = arg.backToRecommendPanelTabId
        arg.backToRecommendPanelTabId = nil
        self:_RefreshUICloseBtn()
    end
end

CashShopCtrl.ShowWalletBar = HL.Method(HL.Table) << function(self, arg)
    self.m_showWalletBarArg = arg
    self:_ShowWalletBarCore()
end

CashShopCtrl._ShowWalletBarCore = HL.Method() << function(self)
    if self.m_showWalletBarArg == nil then
        return
    end

    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(self.m_showWalletBarArg.moneyIds, false, true)

    self.view.timeNode.gameObject:SetActive(self.m_showWalletBarArg.showTimeNode)
    if self.m_showWalletBarArg.time then
        self.view.timeNode.timeNumText:InitCountDownText(
            self.m_showWalletBarArg.time,
            self.m_showWalletBarArg.timeCompleteCallback)
    end
end

CashShopCtrl._ClickCategory = HL.Method(HL.String) << function(self, categoryId)
    if self.m_phase.currCategoryId == categoryId then
        return
    end
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder({})
    self.m_phase:OpenCategory(categoryId)
    self.m_phase:ClearBackToRecommendPanel()
end

CashShopCtrl._CloseAllDialogPanel = HL.Method() << function(self)
    local isOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isOpen then
        shopDetailCtrl:TryClose()
    end
end

HL.Commit(CashShopCtrl)
