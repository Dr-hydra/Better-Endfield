local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityFreeMonthlyPass3D

local DECO_ICON_PATH_1 = "item_monthlypass_icon01"
local DECO_ICON_PATH_2 = "item_monthlypass_icon02"

local PanelType = {
    Shop = 1,
    DailyPopup = 2,
}

ActivityFreeMonthlyPass3DCtrl = HL.Class('ActivityFreeMonthlyPass3DCtrl', uiCtrl.UICtrl)

ActivityFreeMonthlyPass3DCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityFreeMonthlyPass3DCtrl.m_currPageType = HL.Field(HL.Any) << PanelType.Shop


ActivityFreeMonthlyPass3DCtrl.m_rewardList1 = HL.Field(HL.Table)


ActivityFreeMonthlyPass3DCtrl.m_rewardList2 = HL.Field(HL.Table)

ActivityFreeMonthlyPass3DCtrl.m_rewardCell1 = HL.Field(HL.Forward("UIListCache"))

ActivityFreeMonthlyPass3DCtrl.m_rewardCell2 = HL.Field(HL.Forward("UIListCache"))





ActivityFreeMonthlyPass3DCtrl.s_messages = HL.StaticField(HL.Table) << {

}








ActivityFreeMonthlyPass3DCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.m_currPageType = arg.isDailyPopup and PanelType.DailyPopup or PanelType.Shop

    self.m_rewardCell1 = UIUtils.genCellCache(self.view.itemCellState)
    self.m_rewardCell2 = UIUtils.genCellCache(self.view.acquireItemCell)


    self:_SetUIPos()

    local dailyRewardInfoList = ActivityUtils.CalendarCheckInGetDailyReward(self.m_activityId)

    if self.m_currPageType == PanelType.DailyPopup then
        self.m_rewardList1 = dailyRewardInfoList
        self.view.leftListNode:SetState("DailyPopup")
        self:_RefreshDailyPopupUI()
    else
        self.m_rewardList1 = dailyRewardInfoList
        self.m_rewardList2 = ActivityUtils.CalendarCheckInGetAllReward(self.m_activityId)
        self.view.leftListNode:SetState("Shop")
        self:_RefreshShopUI()
    end


    local _, haveGotDailyReward, _ = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)
    if haveGotDailyReward then
        self.animationWrapper:SetAnimationInClip("freeactivityshopmonthlypassreceive_in_part_0")
        self.animationWrapper:SetAnimationInEasingClip("freeactivityshopmonthlypassreceive_in_part_1")
        self.animationWrapper:SetAnimationLoopClip("freeactivityshopmonthlypassreceive_loop")
    end
    self.animationWrapper:PlayInAnimation()
end











ActivityFreeMonthlyPass3DCtrl.PlayGotDailyReward = HL.Method(HL.Any) << function(self, endCallback)
    if self.m_currPageType == PanelType.DailyPopup then
        self.view.contentState:SetState("AcquireAfter")
    end
    self.view.aniAll:PlayInAnimation(function()

        self.animationWrapper:PlayWithTween("freeactivityshopmonthlypassreceive_loop", nil, CS.Beyond.UI.UIConst.AnimationState.Loop)
    end)
    self.m_rewardCell1:Refresh(#self.m_rewardList1, function(cell, index)
        local uiStateCtrl = cell.stateController
        local animWrapper = cell.animationWrapper
        uiStateCtrl:SetState("AcquireAfter")
        if index == 1 then
            animWrapper:PlayInAnimation(function()
                if endCallback then
                    endCallback()
                end
            end)
        else
            animWrapper:PlayInAnimation()
        end
    end)
end


ActivityFreeMonthlyPass3DCtrl.SampleToAnimBegin = HL.Method() << function(self)
    if self.m_currPageType == PanelType.DailyPopup then
        self.view.contentState:SetState("AcquireBefore")
    end
    self.view.aniAll:SampleToInAnimationBegin()
    self.m_rewardCell1:Refresh(#self.m_rewardList1, function(cell, index)
        local uiStateCtrl = cell.stateController
        local animWrapper = cell.animationWrapper
        uiStateCtrl:SetState("AcquireBefore")
        animWrapper:SampleToInAnimationBegin()
    end)

    self.animationWrapper:PlayWithTween("shopmonthlypass_loop", nil, CS.Beyond.UI.UIConst.AnimationState.Loop)
end





ActivityFreeMonthlyPass3DCtrl._SetUIPos = HL.Method() << function(self)

    local main = self.view.main
    local rect = main.gameObject:GetComponent("RectTransform")
    if self.m_currPageType == PanelType.DailyPopup then
        if not DeviceInfo.isMobile then
            rect.localPosition = Vector3(80, 50, 200)
            rect.localEulerAngles = Vector3(10, -11.2, -4.5)
        else
            rect.localPosition = Vector3(80, 50, 200)
            rect.localEulerAngles = Vector3(10, -11.2, -4.5)
        end
        self.view.shopMonthlyPass3DPanel:SetState("Acquire")
    elseif self.m_currPageType == PanelType.Shop then
        if not DeviceInfo.isMobile then
            rect.localPosition = Vector3(296, 6, 219)
            rect.localEulerAngles = Vector3(10, -11.2, -4.5)
            self.view.shopMonthlyPass3DPanel:SetState("ShopPc")
        else
            if DeviceInfo.usingController then

                rect.localPosition = Vector3(388, 6, 460)
                rect.localEulerAngles = Vector3(10, -11.2, -4.5)
            else

                rect.localPosition = Vector3(430, -42, 910)
                rect.localEulerAngles = Vector3(10, -11.2, -4.5)
            end
            self.view.shopMonthlyPass3DPanel:SetState("ShopTouch")
        end
    end
end

ActivityFreeMonthlyPass3DCtrl._RefreshDailyPopupUI = HL.Method() << function(self)
    local RectOffset = CS.UnityEngine.RectOffset
    local padding = RectOffset()
    padding.left = 0
    padding.right = 0
    padding.top = 20
    padding.bottom = 20
    self.view.leftItemEntry.padding = padding
    self.view.leftItemEntry.spacing = 60

    local _, haveGotDailyReward, _ = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)
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
        if haveGotDailyReward then
            cell.animationWrapper:SampleToInAnimationEnd()
        end
    end)
    if haveGotDailyReward then
        self.view.aniAll:SampleToInAnimationEnd()
    end
end

ActivityFreeMonthlyPass3DCtrl._RefreshShopUI = HL.Method() << function(self)
    local RectOffset = CS.UnityEngine.RectOffset
    local padding = RectOffset()
    padding.left = 0
    padding.right = 0
    padding.top = 0
    padding.bottom = 0
    self.view.leftItemEntry.padding = padding
    self.view.leftItemEntry.spacing = 30

    self.view.contentState:SetState("Shop")

    local haveGotReward = false
    _, haveGotReward, _ = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)

    if haveGotReward then
        if self.m_currPageType == PanelType.DailyPopup then
            self.view.contentState:SetState("AcquireAfter")
        end
        self.view.aniAll:SampleClip("shopmonthlysatrpass_receive_part_1", 1.0)
    end

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

        if haveGotReward then
            local animWrapper = cell.animationWrapper
            uiStateCtrl:SetState("AcquireAfter")
            animWrapper:SampleToInAnimationEnd()
        end
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


end






HL.Commit(ActivityFreeMonthlyPass3DCtrl)
