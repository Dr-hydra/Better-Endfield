
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityBenefitsRegionPopup

ActivityBenefitsRegionPopupCtrl = HL.Class('ActivityBenefitsRegionPopupCtrl', uiCtrl.UICtrl)






ActivityBenefitsRegionPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_BENEFIT_REFRESH] = '_OnRefresh',
}

ActivityBenefitsRegionPopupCtrl.m_conditionCells = HL.Field(HL.Any)

ActivityBenefitsRegionPopupCtrl.m_cells = HL.Field(HL.Any)

ActivityBenefitsRegionPopupCtrl.m_firstCell = HL.Field(HL.Any)

local configName = {
    levelUp = 1,
    shop = 2,
}


ActivityBenefitsRegionPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.view.btnClose.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.mask.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.titleText.text = Language.LUA_ACTIVITY_BENEFITS_DOMAIN_DEVELOPMENT
    self:_RefreshInfo(arg)


    self.m_cells = UIUtils.genCellCache(self.view.reminderItemCell)
    self.m_cells:Refresh(#self.m_benefitInfo, function(cell, index)
        self:_OnUpdateCell(cell, index)
    end)


    if DeviceInfo.usingController then
        self:SetNaviTarget(self.m_firstCell.button)
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputBindingGroupMonoTarget.groupId })
    end
end

ActivityBenefitsRegionPopupCtrl._OnRefresh = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshInfo(arg)
    self.m_cells:Refresh(#self.m_benefitInfo, function(cell, index)
        self:_OnUpdateCell(cell, index)
    end)
end

ActivityBenefitsRegionPopupCtrl.m_benefitInfo = HL.Field(HL.Table)

ActivityBenefitsRegionPopupCtrl._RefreshInfo = HL.Method(HL.Table) << function(self, arg)

    local goodsIds = Tables.activityConst.ActivityBenefitsDomainShopGoodsIds
    local levelUpTicketTotal = arg[configName.levelUp].total - goodsIds.Count
    local levelUpDiamondTotal = arg[configName.shop].total
    local levelUpTicketObtain = arg[configName.levelUp].obtain
    local levelUpDiamondObtain = arg[configName.shop].obtain
    local ShopTicketTotal = goodsIds.Count
    local ShopTicketObtain = 0


    for i = 1, goodsIds.Count do
        local goodsId = goodsIds[CSIndex(i)]
        if GameInstance.player.shopSystem:GetBuyCountByGoodsId(goodsId) > 0 then
            ShopTicketObtain = ShopTicketObtain + 1
            levelUpTicketObtain = levelUpTicketObtain - 1
        end
    end


    self.m_benefitInfo = {
        {
            title = Language.LUA_ACTIVITY_BENEFITS_DOMAIN_LEVEL_UP,
            itemIds = Tables.activityConst.ActivityBenefitsDomainLevelUpItemIds,
            totalCount = {levelUpTicketTotal, levelUpDiamondTotal},
            obtainCount = {levelUpTicketObtain, levelUpDiamondObtain},
            isUnlocked = function()
                return Utils.isSystemUnlocked(GEnums.UnlockSystemType.DomainDevelopment)
            end,
        },
        {
            title = Language.LUA_ACTIVITY_BENEFITS_DOMAIN_SHOP,
            itemIds = Tables.activityConst.ActivityBenefitsDomainShopItemIds,
            totalCount = {ShopTicketTotal},
            obtainCount = {ShopTicketObtain},
            isUnlocked = function()
                if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.DomainShop) then
                    return false
                end
                for i = 1, Tables.activityConst.ActivityBenefitsDomainIds.Count do
                    if not DomainPOIUtils.checkCanOpenDomainShop(Tables.activityConst.ActivityBenefitsDomainIds[CSIndex(i)]) then
                        return false
                    end
                end
                return true
            end,
        }
    }


    for index, info in ipairs(self.m_benefitInfo) do
        self.m_benefitInfo[index].isComplete = true
        self.m_benefitInfo[index].sortId = 1
        self.m_benefitInfo[index].jumpId = Tables.activityConst.ActivityBenefitsDomainJumpIds[CSIndex(index)]
        for i = 1, #info.totalCount do
            if info.obtainCount[i] ~= info.totalCount[i] then
                self.m_benefitInfo[index].isComplete = false
                self.m_benefitInfo[index].sortId = 0
            end
        end
    end


    table.sort(self.m_benefitInfo, Utils.genSortFunction({"sortId"}, true))
end

ActivityBenefitsRegionPopupCtrl._OnUpdateCell = HL.Method(HL.Any,HL.Number) << function(self, cell, index)
    local info = self.m_benefitInfo[index]
    if index == 1 then
        self.m_firstCell = cell
    end

    cell.titleTxt.text = info.title

    local isComplete = info.isComplete

    local isUnlocked = info.isUnlocked()
    local jumpId = info.jumpId


    if isComplete then
        cell.stateController:SetState("Complete")
    elseif isUnlocked then
        cell.stateController:SetState("Goto")
        cell.btnGoto.onClick:AddListener(function()
            self:Close()
            Utils.jumpToSystem(jumpId)
        end)
    else
        cell.stateController:SetState("Locked")
        cell.lockNode.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_BENEFITS_SYSTEM_UNLOCKED)
        end)
    end


    local itemIds = info.itemIds
    cell.cache = cell.cache or UIUtils.genCellCache(cell.activityBenefitsRewardCell)
    cell.cache:Refresh(#itemIds, function(innerCell, innerIndex)
        local rewardInfo =
        {
            fromMain = false,
            itemId = itemIds[CSIndex(innerIndex)],
            obtain = info.obtainCount[innerIndex],
            total = info.totalCount[innerIndex],
            isBigReward = false,
            itemExtraInfo = {
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightMid,
                tipsPosTransform = innerCell.rectTransform,
                isSideTips = true,
            }
        }
        innerCell:InitActivityBenefitsRewardCell(rewardInfo)

        innerCell.view.lineImage.gameObject:SetActive(innerIndex > 1)
    end)
    cell.keyHint:SetAsLastSibling()


    if DeviceInfo.usingController then
        cell.selectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
end

ActivityBenefitsRegionPopupCtrl._Close = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

HL.Commit(ActivityBenefitsRegionPopupCtrl)
