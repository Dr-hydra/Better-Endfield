
local ShopCtrl = require_ex('UI/Panels/Shop/ShopCtrl')
HL.Forward("ShopCtrl")
local PANEL_ID = PanelId.SpaceshipShop
local PHASE_ID = PhaseId.SpaceshipShop
local shopSystem = GameInstance.player.shopSystem
local SSSHOP_DETAIL_CLIENT_DATA_MANAGER_LAST_SEEN_TIMESTAMP_KEY = "SSShopDetailLastSeenRefresh"

SpaceshipShopCtrl = HL.Class('SpaceshipShopCtrl', ShopCtrl.ShopCtrl)

SpaceshipShopCtrl.m_shopGroupList = HL.Field(HL.Any)


SpaceshipShopCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        AudioManager.PostEvent("Au_UI_Menu_SpaceshipShopPanel_Close")
        PhaseManager:PopPhase(PHASE_ID)
    end)
    UIManager:ToggleBlockObtainWaysJump("space_ship_shop", true)
    self.m_needPlaySoldOut = {}
    self.m_needPlayUnlock = {}
    self.m_lastBuyGoods = {}
    local curTimeStamp = DateTimeUtils.GetCurrentTimestampBySeconds()
    ClientDataManagerInst:SetString(SSSHOP_DETAIL_CLIENT_DATA_MANAGER_LAST_SEEN_TIMESTAMP_KEY, tostring(curTimeStamp), false)

    local shopGroupId, shopId
    if type(arg) == "table" then
        shopGroupId = arg.shopGroupId
        shopId = arg.shopId
    else
        shopGroupId = arg
    end
    self.m_shopGroupList = shopSystem:GetShopListByType(CS.Beyond.GEnums.ShopGroupType.Spaceship, false)
    for i = 0, self.m_shopGroupList.Count - 1 do
        local unlock = shopSystem:CheckShopGroupUnlocked(self.m_shopGroupList[i].shopGroupId)
        if not unlock then
            self.m_shopGroupList:RemoveAt(i)
            i = i - 1
        end
    end

    if shopGroupId == nil then
        self.m_shopGroupId = self.m_shopGroupList[0].shopGroupId
    else
        self.m_shopGroupId = shopGroupId
        if not shopSystem:GetShopGroupData(self.m_shopGroupId) then
            self.m_shopGroupId = self.m_shopGroupList[0].shopGroupId
        end
    end
    self.view.titleText.text = Language.LUA_SPACE_SHOP_TITLE
    local shopGroupData = shopSystem:GetShopGroupData(self.m_shopGroupId)

    local shopGroupTableData = Tables.shopGroupTable[self.m_shopGroupId]
    if shopId then
        self.m_shopId = shopId
    end

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, index)
        self:_RefreshContentCell(self.m_getCellFunc(obj), LuaIndex(index))
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

    self:RefreshSpaceShipSheetTabs(self.m_shopGroupId, self.m_shopId)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.view.btnDown.onClick:AddListener(function()
        self:_SwitchPage(1)
    end)

    self.view.btnUpper.onClick:AddListener(function()
        self:_SwitchPage(-1)
    end)

    self.view.scrollList.onGraduallyShowFinish:AddListener(function()
        local rect = self.view.scrollList.transform:GetComponent(typeof(CS.Beyond.UI.UIScrollRect))
        rect.vertical = rect.verticalScrollbar.gameObject.activeSelf
        if self.m_needShowUnlock then
            for i, v in ipairs(self.m_goodsInfos) do
                local cell = self.m_getCellFunc(i)
                if cell then
                    cell:PlayLockAnimation()
                end
            end
            self.m_needShowUnlock = false
        end
    end)
end

SpaceshipShopCtrl.PlaySwitchTabAnimation = HL.Method(HL.String) << function(self, groupId)
    local curGroupIndex = 0
    for i = 0, self.m_shopGroupList.Count - 1 do
        if self.m_shopGroupList[i].shopGroupId == self.m_shopGroupId then
            curGroupIndex = i
            break
        end
    end
    local newGroupIndex = 0
    for i = 0, self.m_shopGroupList.Count - 1 do
        if self.m_shopGroupList[i].shopGroupId == groupId then
            newGroupIndex = i
            break
        end
    end

    if curGroupIndex > newGroupIndex then
        self.view.shopTitle:GetComponent("UIAnimationWrapper"):Play("shopship_left")
    else
        self.view.shopTitle:GetComponent("UIAnimationWrapper"):Play("shopship_right")
    end
end

SpaceshipShopCtrl.InitTab = HL.Method() << function(self)
    self.view.tabsMobile:InitShopTabsForSwitchShopGroup(self.m_shopGroupList, self.m_shopGroupId, function(groupId)
        self:PlaySwitchTabAnimation(groupId)
        self:RefreshSpaceShipSheetTabs(groupId)
    end)
    self.view.tabsPC:InitShopTabsForSwitchShopGroup(self.m_shopGroupList, self.m_shopGroupId, function(groupId)
        self:PlaySwitchTabAnimation(groupId)
        self:RefreshSpaceShipSheetTabs(groupId)
    end)
end

SpaceshipShopCtrl._SwitchPage = HL.Method(HL.Number) << function(self, diff)
    local index = 0
    local shopGroupData = shopSystem:GetShopGroupData(self.m_shopGroupId)

    for i = 0, shopGroupData.shopIdList.Count - 1 do
        if shopGroupData.shopIdList[i] == self.m_shopId then
            index = i
            break
        end
    end

    self:RefreshSpaceShipSheetTabs(self.m_shopGroupId, shopGroupData.shopIdList[index + diff])

    index = index + diff
    self:_UpdateBtnState(index)
end

SpaceshipShopCtrl._UpdateBtnState = HL.Method(HL.Number) << function(self, index)
    local groupData = shopSystem:GetShopGroupData(self.m_shopGroupId)
    self.view.btnDown.interactable = index < groupData.shopIdList.Count - 1
    local isUnlockCycleShop = shopSystem:CheckShopUnlocked(groupData.shopIdList[groupData.shopIdList.Count - 1 ])

    if index == groupData.shopIdList.Count - 2 then
        self.view.btnDown.interactable = isUnlockCycleShop
    end

    if index == groupData.shopIdList.Count - 1 and isUnlockCycleShop then
        self.view.btnDown.gameObject:SetActive(false)
        self.view.btnUpper.gameObject:SetActive(false)
    else
        self.view.btnDown.gameObject:SetActive(true)
        self.view.btnUpper.gameObject:SetActive(true)
    end
    self.view.btnUpper.gameObject:SetActive(index > 0 and not(index == groupData.shopIdList.Count - 1 and isUnlockCycleShop))

end


SpaceshipShopCtrl.RefreshSpaceShipSheetTabs = HL.Method(HL.String, HL.Opt(HL.String)) << function(self, curGroupId, shopId)
    local groupData = shopSystem:GetShopGroupData(curGroupId)

    if not groupData then
        self:InitTab()
        return
    end


    local index = 0
    if shopId == nil or shopId == "" then
        for i = 0, groupData.shopIdList.Count - 1 do
            local shopUnlock, text = shopSystem:CheckShopUnlocked(groupData.shopIdList[i])
            if not shopUnlock then
                local index = i > 0 and i - 1 or 0
                shopId = groupData.shopIdList[index]
                break
            end
        end
    end
    if string.isEmpty(shopId) then
        shopId = groupData.shopIdList[groupData.shopIdList.Count - 1]
    end

    for i = 0, groupData.shopIdList.Count - 1 do
        if groupData.shopIdList[i] == shopId then
            index = i
            break
        end
    end


    self.m_shopGroupId = curGroupId
    self:InitTab()

    self.m_shopId = string.isEmpty(shopId) and groupData.shopIdList[0] or shopId
    self:_UpdateBtnState(index)
    self:_RefreshTimeCountDown()

    local shopData = Tables.shopTable:GetValue(self.m_shopId)
    local shopUnlock, text = shopSystem:CheckShopUnlocked(self.m_shopId)

    local goodsData = shopSystem:GetShopData(self.m_shopId)
    for goodsId, goodsData in pairs(goodsData.goodsDic) do
        local goodsTableData = Tables.shopGoodsTable[goodsData.goodsTemplateId]
        local moneyId = goodsTableData.moneyId
        self.view.moneyText.text = string.format("%d/%d", Utils.getItemCount(moneyId, true), Tables.MoneyConfigTable[moneyId].MoneyClearLimit)

        self.view.moneyCell:InitMoneyCell(moneyId, true, false, true)

        self.view.countDownText_1.gameObject:SetActive(true)
        self.view.layout_Time.gameObject:SetActive(true)
        break
    end

    if not shopUnlock then
        local conditionInfo = shopData.unlockConditions[0]
        self.view.shopLockTips.text = conditionInfo.desc
        self.view.shopLockTips.color = self.view.config.redColor
        self.view.lockProgress.gameObject:SetActive(false)
        self.view.deco.gameObject:SetActive(false)
        if index ~= 0 then
            local allCount, soldCount = self:_GetSellProgress(groupData.shopIdList[index - 1])
            self.view.lockProgress.text = string.format("%d/%d", allCount, soldCount)
        end
    else
        self.view.deco.gameObject:SetActive(true)
        if index ~= groupData.shopIdList.Count - 1 then
            self.view.lockProgress.gameObject:SetActive(true)
            local allCount, soldCount = self:_GetSellProgress(self.m_shopId)
            self.view.lockProgress.text = string.format("%d/%d", allCount, soldCount)
            self.view.shopLockTips.text = Language.LUA_SPACE_SHOP_LOCK_NEXT
            self.view.shopLockTips.color = self.view.config.whiteColor
        end
    end


    local shopTableData = Tables.shopTable[self.m_shopId]
    self.view.shopSheetName.text = shopTableData.shopName
    self.view.shopTitle.text = Tables.shopGroupTable[(self.m_shopGroupId)].shopGroupName
    self.view.shopTitleChange.text = Tables.shopGroupTable[(self.m_shopGroupId)].shopGroupName
    local shopData = shopSystem:GetShopData(self.m_shopId)

    self.m_goods = {}
    self.m_soldOut = {}


    for goodsId, goodsData in pairs(shopData.goodsDic) do
        local isUnlocked = shopSystem:CheckGoodsUnlocked(goodsId) and shopSystem:CheckShopUnlocked(self.m_shopId)

        local goodsTableData = Tables.shopGoodsTable[goodsData.goodsTemplateId]
        if true then
            local itemBundle = UIUtils.getRewardFirstItem(goodsTableData.rewardId)

            local itemTableData = Tables.itemTable[itemBundle.id]
            local info = {
                id = goodsId,
                rarity = itemTableData.rarity,
                price = goodsTableData.price * goodsData.discount,
                sortId = goodsTableData.sortId,
                isLocked = not isUnlocked,
            }
            if self.m_needShowUnlock then
                info.isLocked = true
            end
            if shopSystem:GetRemainCountByGoodsId(self.m_shopId, goodsId) > 0 then
                table.insert(self.m_goods, info)
            else
                table.insert(self.m_soldOut, info)
            end
        end
    end
    self:_ApplySortOption()
end

SpaceshipShopCtrl._RefreshTimeCountDown = HL.Override() << function(self)

    self.view.timeNode.gameObject:SetActiveIfNecessary(true)
    self.view.countDownText_1:InitCountDownText(self:_GetMoneyTime(),



        nil,
        function(time)
            return self:_GetMoneyTimeText(time)
        end
    )


    local nowIndex = 0
    local groupData = shopSystem:GetShopGroupData(self.m_shopGroupId)
    for i = 0, groupData.shopIdList.Count - 1 do
        if groupData.shopIdList[i] == self.m_shopId then
            nowIndex = i
            break
        end
    end
    local shopGroupData = shopSystem:GetShopGroupData(self.m_shopGroupId)
    local isUnlockCycleShop = shopSystem:CheckShopUnlocked(shopGroupData.shopIdList[shopGroupData.shopIdList.Count - 1 ])
    isUnlockCycleShop = true
    self.view.countDownText_2.gameObject:SetActive(nowIndex == groupData.shopIdList.Count - 1 and isUnlockCycleShop)
    self.view.shopLockTips.gameObject:SetActive((nowIndex == groupData.shopIdList.Count - 1 and not isUnlockCycleShop) or (nowIndex ~= groupData.shopIdList.Count - 1 ))
    self.view.lockProgress.gameObject:SetActive(self.view.shopLockTips.gameObject.activeSelf)
    self.view.countDownText_2:InitCountDownText(self:_CalculateTargetTime(GEnums.ShopRefreshCycleType.Weekly),



        nil,
        function(time)
            return string.format(Language.LUA_SPACE_SHOP_REFRESH_COUNTDOWN, UIUtils.getLeftTime(time))
        end
    )
end

SpaceshipShopCtrl._RefreshSheetTabs = HL.Override(HL.String) << function(self, curShopId)

end

SpaceshipShopCtrl._GetMoneyTime = HL.Method().Return(HL.Number) << function(self)
    return Utils.getNextWeeklyServerRefreshTime()
end

SpaceshipShopCtrl._GetMoneyTimeText = HL.Method(HL.Number).Return(HL.Any) << function(self, leftTime)
    if leftTime > 24 * 3600 then
        return tostring(string.format(Language.LUA_SPACE_SHOP_REFRESH_TWO_DAY, math.floor(leftTime / (24 * 3600)) + 1))
    elseif leftTime < 24 * 3600 and leftTime > 3600 then
        return tostring(string.format(Language.LUA_SPACE_SHOP_REFRESH_ONE_DAY, math.floor(leftTime / 3600)))
    else
        return tostring(string.format(Language.LUA_SPACE_SHOP_REFRESH_AT_ONCE, math.max(1 ,math.floor(leftTime / 60))))
    end
end



SpaceshipShopCtrl._GetSellProgress = HL.Method(HL.String).Return(HL.Number, HL.Number) << function(self, shopId)









    local shopData = shopSystem:GetShopData(shopId)
    local allCount = 0
    local soldCount = 0
    for goodsId, goodsData in pairs(shopData.goodsDic) do
        allCount = allCount + 1
        if shopSystem:GetRemainCountByGoodsId(shopId, goodsId) == 0 then
            soldCount = soldCount + 1
        end
    end
    return soldCount, allCount
end



SpaceshipShopCtrl.CheckGoodsUnlocked = HL.Override(HL.String).Return(HL.Boolean) << function(self, goodsId)
    local shopSystem = GameInstance.player.shopSystem
    return not (shopSystem:CheckGoodsUnlocked(goodsId) and shopSystem:CheckShopUnlocked(self.m_shopId))
end

SpaceshipShopCtrl._OnShopRefresh = HL.Override() << function(self)
    if self.m_waitAnimation then
        return
    else

    end
    self:RefreshSpaceShipSheetTabs(self.m_shopGroupId, self.m_shopId)
    self.view.scrollList:SkipGraduallyShow()
    self.view.emptyClick.gameObject:SetActiveIfNecessary(false)
end

SpaceshipShopCtrl.OnAfterBuyItemSucc = HL.Override() << function(self)

    local sellOut, allCount = self:_GetSellProgress(self.m_shopId)
    local groupData = shopSystem:GetShopGroupData(self.m_shopGroupId)
    local nowIndex = 0
    for i = 0, groupData.shopIdList.Count - 1 do
        if groupData.shopIdList[i] == self.m_shopId then
            nowIndex = i
            break
        end
    end
    local needCallSuper = true
    if sellOut == allCount and nowIndex <= groupData.shopIdList.Count - 2 then
        needCallSuper = false
        Notify(MessageConst.SHOW_POP_UP,{content = Language.LUA_SPACE_SHOP_UNLOCK_NEXT_TIPS,hideCancel = true, onConfirm = function()
            self.m_needShowUnlock = true
            self:_SwitchPage(1)
        end})
    end

    self.view.deco.gameObject:SetActive(true)
    if nowIndex ~= groupData.shopIdList.Count - 1 then
        self.view.lockProgress.gameObject:SetActive(true)
        local allCount, soldCount = self:_GetSellProgress(self.m_shopId)
        self.view.lockProgress.text = string.format("%d/%d", allCount, soldCount)
        self.view.shopLockTips.text = Language.LUA_SPACE_SHOP_LOCK_NEXT
        self.view.shopLockTips.color = self.view.config.whiteColor
    end

    if needCallSuper then
        SpaceshipShopCtrl.Super.OnAfterBuyItemSucc(self)
    end
end

SpaceshipShopCtrl.SetMoneyCell = HL.Override(HL.Boolean) << function(self, arg)
    self.view.moneyCell.gameObject:SetActiveIfNecessary(not arg)
end








SpaceshipShopCtrl.OnClose = HL.Override() << function(self)
    UIManager:ToggleBlockObtainWaysJump("space_ship_shop", false)
end




HL.Commit(SpaceshipShopCtrl)
