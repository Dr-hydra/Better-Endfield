
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopMonthlyPass3D

local DECO_ICON_PATH_1 = "item_monthlypass_icon01"
local DECO_ICON_PATH_2 = "item_monthlypass_icon02"

local PanelType = {
    Shop = 1,
    DailyPopup = 2,
}

ShopMonthlyPass3DCtrl = HL.Class('ShopMonthlyPass3DCtrl', uiCtrl.UICtrl)

ShopMonthlyPass3DCtrl.m_currPageType = HL.Field(HL.Any) << PanelType.Shop


ShopMonthlyPass3DCtrl.m_rewardList1 = HL.Field(HL.Table)


ShopMonthlyPass3DCtrl.m_rewardList2 = HL.Field(HL.Table)


ShopMonthlyPass3DCtrl.m_rewardList3 = HL.Field(HL.Table)


ShopMonthlyPass3DCtrl.m_remainDayNumber = HL.Field(HL.Number) << 0

ShopMonthlyPass3DCtrl.m_rewardCell1 = HL.Field(HL.Forward("UIListCache"))

ShopMonthlyPass3DCtrl.m_rewardCell2 = HL.Field(HL.Forward("UIListCache"))

ShopMonthlyPass3DCtrl.m_rewardCell3 = HL.Field(HL.Forward("UIListCache"))





ShopMonthlyPass3DCtrl.s_messages = HL.StaticField(HL.Table) << {

}








ShopMonthlyPass3DCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg.isDailyPopup then

        self:ChangePanelCfg("clearedPanel", false)
    else
        self:ChangePanelCfg("clearedPanel", true)
    end

    self.m_currPageType = arg.isDailyPopup and PanelType.DailyPopup or PanelType.Shop
    if arg.remainDayNumber ~= nil then
        self.m_remainDayNumber = arg.remainDayNumber
    else
        self.m_remainDayNumber = GameInstance.player.monthlyPassSystem:GetRemainValidDays()
    end

    self.m_rewardCell1 = UIUtils.genCellCache(self.view.itemCellState)
    self.m_rewardCell2 = UIUtils.genCellCache(self.view.acquireItemCell)
    self.m_rewardCell3 = UIUtils.genCellCache(self.view.itemCell)


    local main = self.view.main
    local rect = main.gameObject:GetComponent("RectTransform")
    if self.m_currPageType == PanelType.DailyPopup then
        if not DeviceInfo.usingTouch then
            rect.localPosition = Vector3(180, 42, 0)
            rect.localEulerAngles = Vector3(10, -11.2, -4.5)
        else
            rect.localPosition = Vector3(180, 34, 200)
            rect.localEulerAngles = Vector3(10, -11.2, -4.5)
        end
        self.view.shopMonthlyPass3DPanel:SetState("Acquire")
    elseif self.m_currPageType == PanelType.Shop then
        if not DeviceInfo.usingTouch then
            rect.localPosition = Vector3(256, -52, 222)
            rect.localEulerAngles = Vector3(10, -11.2, -4.5)
            self.view.shopMonthlyPass3DPanel:SetState("ShopPc")
        else
            rect.localPosition = Vector3(208, -80, 810)
            rect.localEulerAngles = Vector3(10, -11.2, -4.5)
            self.view.shopMonthlyPass3DPanel:SetState("ShopTouch")
        end
    end

    local dailyRewardInfoList = CashShopUtils.GetMonthlyPassDailyRewardInfoList()

    if self.m_currPageType == PanelType.DailyPopup then
        self.m_rewardList1 = dailyRewardInfoList
        self.view.leftListNode:SetState("DailyPopup")
        self:_RefreshDailyPopupUI()
    else
        self.m_rewardList1 = dailyRewardInfoList
        self.m_rewardList2 = CashShopUtils.GetMonthlyPassMultiplyRewardInfoList()
        self.m_rewardList3 = CashShopUtils.GetMonthlyPassImmediateRewardInfoList()
        self.view.leftListNode:SetState("Shop")
        self:_RefreshShopUI()
    end
end

ShopMonthlyPass3DCtrl.OnShow = HL.Override() << function(self)
    self.animationWrapper:PlayInAnimation()
end








ShopMonthlyPass3DCtrl._RefreshDailyPopupUI = HL.Method() << function(self)
    local RectOffset = CS.UnityEngine.RectOffset
    local padding = RectOffset()
    padding.left = 0
    padding.right = 0
    padding.top = 20
    padding.bottom = 20
    self.view.leftItemEntry.padding = padding
    self.view.leftItemEntry.spacing = 60

    self.view.entry01Txt.text = Language.LUA_MONTHLYPASS_LEFT_TITLE_1_DAILY
    self.view.entry03Txt.text = Language.LUA_MONTHLYPASS_LEFT_TITLE_2_DAILY
    local haveGotDailyReward = self.m_phase and self.m_phase.m_haveGotReward or false
    if haveGotDailyReward then
        self.view.contentState:SetState("AcquireAfter")
    else
        self.view.contentState:SetState("AcquireBefore")
    end
    self.m_rewardCell1:Refresh(#self.m_rewardList1, function(cell, index)
        local uiStateCtrl = cell.stateController
        if haveGotDailyReward then
            uiStateCtrl:SetState("AcquireAfter")
        else
            uiStateCtrl:SetState("AcquireBefore")
        end
        local itemInfo = self.m_rewardList1[index]
        local succ, itemData = Tables.ItemTable:TryGetValue(itemInfo.rewardId)
        if succ then
            cell.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
            cell.itemIconShadown:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
        end
        cell.itemNumTxt.text = itemInfo.number
        cell.itemNumTxt2.text = itemInfo.number

        if index == 1 then
            cell.itemDecoIconShadown:LoadSprite(UIConst.UI_SPRITE_SHOP_MONTHLY_PASS, DECO_ICON_PATH_1)
            cell.itemDecoIcon:LoadSprite(UIConst.UI_SPRITE_SHOP_MONTHLY_PASS, DECO_ICON_PATH_1)
        elseif index == 2 then
            cell.itemDecoIconShadown:LoadSprite(UIConst.UI_SPRITE_SHOP_MONTHLY_PASS, DECO_ICON_PATH_2)
            cell.itemDecoIcon:LoadSprite(UIConst.UI_SPRITE_SHOP_MONTHLY_PASS, DECO_ICON_PATH_2)
        end
    end)
    self.view.daysNumTxt.text = tostring(self.m_remainDayNumber)
    self.view.daysNumTxtShadown.text = tostring(self.m_remainDayNumber)
    if self.m_remainDayNumber <= Tables.CashShopConst.MonthlyCardShortThresholdDay then
        self.view.daysNumTxt.color = self.view.config.DAY_COLOR_SHORT_DATED
    else
        self.view.daysNumTxt.color = self.view.config.DAY_COLOR_NORMAL
    end
end

ShopMonthlyPass3DCtrl.PlayGotDailyReward = HL.Method() << function(self)
    local haveGotDailyReward = self.m_phase and self.m_phase.m_haveGotReward or false
    if haveGotDailyReward then
        self.view.contentState:SetState("AcquireAfter")
        self.view.aniAll:PlayInAnimation()
    end
    self.m_rewardCell1:Refresh(#self.m_rewardList1, function(cell, index)
        local uiStateCtrl = cell.stateController
        local animWrapper = cell.animationWrapper
        if haveGotDailyReward then
            uiStateCtrl:SetState("AcquireAfter")
            animWrapper:PlayInAnimation()
        else
            uiStateCtrl:SetState("AcquireBefore")
        end
    end)
end

ShopMonthlyPass3DCtrl._RefreshShopUI = HL.Method() << function(self)
    local RectOffset = CS.UnityEngine.RectOffset
    local padding = RectOffset()
    padding.left = 0
    padding.right = 0
    padding.top = 0
    padding.bottom = 0
    self.view.leftItemEntry.padding = padding
    self.view.leftItemEntry.spacing = 30

    self.view.contentState:SetState("Shop")

    self.view.entry01Txt.text = Language.LUA_MONTHLYPASS_LEFT_TITLE_1_SHOP
    self.view.entry03Txt.text = Language.LUA_MONTHLYPASS_LEFT_TITLE_2_SHOP
    self.m_rewardCell1:Refresh(#self.m_rewardList1, function(cell, index)
        local uiStateCtrl = cell.stateController
        uiStateCtrl:SetState("Shop")
        local itemInfo = self.m_rewardList1[index]
        local succ, itemData = Tables.ItemTable:TryGetValue(itemInfo.rewardId)
        if succ then
            cell.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
            cell.itemIconShadown:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
        end
        cell.itemNumTxt.text = itemInfo.number
        cell.itemNumTxt2.text = itemInfo.number
    end)
    self.m_rewardCell2:Refresh(#self.m_rewardList2, function(cell, index)
        local itemInfo = self.m_rewardList2[index]
        local succ, itemData = Tables.ItemTable:TryGetValue(itemInfo.rewardId)
        if succ then
            cell.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
        end
        cell.itemNumTxt.text = itemInfo.number
        cell.itemNumTxt2.text = itemInfo.number
    end)
    self.m_rewardCell3:Refresh(#self.m_rewardList3, function(cell, index)
        local itemInfo = self.m_rewardList3[index]
        local succ, itemData = Tables.ItemTable:TryGetValue(itemInfo.rewardId)
        if succ then
            cell.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
            cell.itemIconShadown:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
        end
        cell.itemNumTxt.text = itemInfo.number
        cell.itemNumTxt2.text = itemInfo.number
    end)
end

HL.Commit(ShopMonthlyPass3DCtrl)
